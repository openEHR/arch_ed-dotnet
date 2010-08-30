'
'
'	component:   "openEHR Archetype Project"
'	description: "Builds all ADL Archetypes"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Archetype.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports EiffelList = EiffelSoftware.Library.Base.Structures.List
Imports AM = XMLParser.OpenEhr.V1.Its.Xml.AM
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes

    Public Class ADL_Archetype
        Inherits Archetype

        Protected archetypeParser As AdlParser.ArchetypeParser
        Protected aomFactory As AdlParser.CFactory

        Public ReadOnly Property DifferentialArchetype() As AdlParser.DifferentialArchetype
            Get
                Return archetypeParser.DifferentialArchetype
            End Get
        End Property

        Public ReadOnly Property FlatArchetype() As AdlParser.FlatArchetype
            Get
                Return archetypeParser.FlatArchetype
            End Get
        End Property

        Public ReadOnly Property DifferentialDefinition() As AdlParser.CComplexObject
            Get
                Debug.Assert(Not DifferentialArchetype Is Nothing)
                Return DifferentialArchetype.Definition
            End Get
        End Property

        Protected Structure ReferenceToResolve
            Dim Element As RmElement
            Dim Attribute As AdlParser.CAttribute
            Dim Index As Integer
        End Structure

        Protected ReferencesToResolve As ArrayList = New ArrayList

        Public Overrides Property ConceptCode() As String
            Get
                Return DifferentialArchetype.Concept.ToCil
            End Get
            Set(ByVal Value As String)
                DifferentialDefinition.SetNodeId(DifferentialArchetype.Concept)

                System.Diagnostics.Debug.Assert(ConceptCode = Value)
                System.Diagnostics.Debug.Assert(DifferentialDefinition.NodeId.ToCil = Value)
            End Set
        End Property

        Public Overrides ReadOnly Property ArchetypeAvailable() As Boolean
            Get
                Return Not DifferentialArchetype Is Nothing
            End Get
        End Property

        Public Overrides Property LifeCycle() As String
            Get
                Return sLifeCycle
            End Get
            Set(ByVal Value As String)
                sLifeCycle = Value
            End Set
        End Property

        Public Overrides Property ParentArchetype() As String
            Get
                Return DifferentialArchetype.ParentArchetypeId.AsString.ToCil
            End Get
            Set(ByVal Value As String)
                DifferentialArchetype.SetParentArchetypeId(AdlParser.Create.ArchetypeId.MakeFromString(Eiffel.String(Value)))
            End Set
        End Property

        Public Overrides ReadOnly Property SourceCode() As String
            Get
                Dim result As String = Nothing

                If Not archetypeParser.SelectedArchetype Is Nothing Then
                    result = archetypeParser.SelectedArchetype.FlatText.ToCil
                End If

                Return result
            End Get
        End Property

        Public Overrides ReadOnly Property SerialisedArchetype(ByVal format As String) As String
            Get
                Dim result As String = Nothing
                MakeParseTree()
                Rebuild()

                Try
                    SetArchetypeDigest()
                    result = archetypeParser.SerialisedArchetype(Eiffel.String(format)).ToCil
                Catch e As System.Reflection.TargetInvocationException
                    If Not e.InnerException Is Nothing Then
                        MessageBox.Show(e.InnerException.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    result = AE_Constants.Instance.Error_saving
                Catch e As Exception
                    Dim errorMessage As String = "Error serialising archetype." & vbCrLf & vbCrLf & e.Message

                    If Not e.InnerException Is Nothing Then
                        errorMessage &= vbCrLf & e.InnerException.Message
                    End If

                    MessageBox.Show(errorMessage, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    result = AE_Constants.Instance.Error_saving
                End Try

                Return result
            End Get
        End Property

        Public Overrides ReadOnly Property Paths(ByVal LanguageCode As String, ByVal parserIsSynchronised As Boolean, Optional ByVal Logical As Boolean = False) As String()
            Get
                Dim list As EiffelList.ArrayedListReference
                Dim i As Integer

                ' must call the prepareToSave to ensure it is accurate
                If Not Filemanager.Master.FileLoading AndAlso Not parserIsSynchronised Then
                    MakeParseTree()
                End If

                Rebuild()

                ' showing the task with logical paths takes a lot of space
                If Logical Then
                    list = FlatArchetype.LogicalPaths(Eiffel.String(LanguageCode), False)
                Else
                    list = FlatArchetype.PhysicalPaths
                End If

                Dim s(list.Upper - 1) As String

                For i = list.Lower To list.Upper
                    s(i - 1) = CType(list.ITh(i), EiffelKernel.String_8).ToCil
                Next

                Return s
            End Get
        End Property

        Public Overrides Sub Specialise(ByVal conceptShortName As String, ByRef ontology As OntologyManager)
            ' TODO: archetypeParser.CreateNewSpecialisedArchetype(Eiffel.String(ConceptShortName))

            ' Update the GUI tables with the new term
            ontology.UpdateTerm(New ADL_Term(DifferentialArchetype.Ontology.TermDefinition(Eiffel.String(ontology.LanguageCode), DifferentialArchetype.Concept)))
            mArchetypeID.Concept &= "-" & conceptShortName
        End Sub

        Public Sub RemoveUnusedCodes()
            DifferentialArchetype.RemoveOntologyUnusedCodes()
        End Sub

        Protected Overrides Sub SetArchetypeId(ByVal value As ArchetypeID)
            Dim id As AdlParser.ArchetypeId = AdlParser.Create.ArchetypeId.MakeFromString(Eiffel.String(value.ToString))

            Try
                If DifferentialArchetype Is Nothing Then
                    archetypeParser.CreateNewArchetype(id, Eiffel.String(sPrimaryLanguageCode))
                    DifferentialDefinition.SetNodeId(DifferentialArchetype.Concept)
                    SetDefinition()
                ElseIf Not id.RmEntity.IsEqual(DifferentialArchetype.ArchetypeId.RmEntity) Then
                    ' does this involve a change in the entity (affects the GUI a great deal!)
                    Debug.Assert(False, "RM entity " & id.RmEntity.ToCil & " does not match " & DifferentialArchetype.ArchetypeId.RmEntity.ToCil & ": changing the RM entity is not yet implemented.")
                    ' will need to reset the GUI to the new entity
                    SetDefinition()
                End If

                DifferentialArchetype.SetArchetypeId(id)
                ' set the internal variable last in case errors
                mArchetypeID = value
            Catch e As Exception
                Debug.Assert(False, "Error setting archetype id")
                Beep()
            End Try
        End Sub

        Protected Sub ArchetypeID_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles mArchetypeID.ArchetypeID_Changed
            SetArchetypeId(CType(sender, ArchetypeID))
        End Sub

        Protected Function MakeAssertion(ByVal id As String, ByVal expression As String) As AdlParser.Assertion
            Dim id_expression_leaf, id_pattern_expression_leaf As AdlParser.ExprLeaf
            Dim match_operator As AdlParser.ExprBinaryOperator

            Debug.Assert((Not id Is Nothing) And (id <> ""))
            id_expression_leaf = aomFactory.CreateExprLeafArchetypeRef(Eiffel.String(id))
            id_pattern_expression_leaf = aomFactory.CreateExprLeafConstraint(MakeCStringMakeFromRegexp(expression))
            match_operator = aomFactory.CreateExprBinaryOperatorNode(AdlParser.Create.OperatorKind.MakeFromString(Eiffel.String("matches")), id_expression_leaf, id_pattern_expression_leaf)

            Return aomFactory.CreateAssertion(match_operator, Nothing)
        End Function

        Protected Function MakeCStringMakeFromRegexp(ByVal s As String) As AdlParser.CString
            Return aomFactory.CreateCStringMakeFromRegexp(Eiffel.String(s))
        End Function

        Protected Function MakeCAttributeSingle(ByVal parent As AdlParser.CComplexObject, ByVal name As String) As AdlParser.CAttribute
            Return aomFactory.CreateCAttributeSingle(parent, Eiffel.String(name))
        End Function

        Protected Function MakeCAttributeMultiple(ByVal parent As AdlParser.CComplexObject, ByVal name As String, ByVal cardinality As RmCardinality) As AdlParser.CAttribute
            Dim result As AdlParser.CAttribute = aomFactory.CreateCAttributeMultiple(parent, Eiffel.String(name))
            Dim c As AdlParser.Cardinality

            If cardinality.IsUnbounded Then
                c = aomFactory.CreateCardinalityMakeUpperUnbounded(cardinality.MinCount)
            Else
                c = aomFactory.CreateCardinalityMakeBounded(cardinality.MinCount, cardinality.MaxCount)
            End If

            If Not cardinality.Ordered Then
                c.SetUnordered()
            End If

            result.SetCardinality(c)
            Return result
        End Function

        Protected Function MakeCPrimitiveObject(ByVal parent As AdlParser.CAttribute, ByVal item As AdlParser.CPrimitive) As AdlParser.CPrimitiveObject
            Return aomFactory.CreateCPrimitiveObject(parent, item)
        End Function

        Protected Function MakeCComplexObjectAnonymous(ByVal parent As AdlParser.CAttribute, ByVal typeName As String) As AdlParser.CComplexObject
            Return aomFactory.CreateCComplexObjectAnonymous(parent, Eiffel.String(typeName))
        End Function

        Protected Function MakeCComplexObjectIdentified(ByVal parent As AdlParser.CAttribute, ByVal typeName As String, ByVal nodeId As String) As AdlParser.CComplexObject
            Return aomFactory.CreateCComplexObjectIdentified(parent, Eiffel.String(typeName), Eiffel.String(nodeId))
        End Function

        Protected Function MakeArchetypeInternalRef(ByVal parent As AdlParser.CAttribute, ByVal typeName As String, ByVal path As AdlParser.OgPath) As AdlParser.ArchetypeInternalRef
            Return aomFactory.CreateArchetypeInternalRef(parent, Eiffel.String(typeName), path.AsString)
        End Function

        Protected Function MakeOccurrences(ByVal c As RmCardinality) As AdlParser.MultiplicityInterval
            Dim result As AdlParser.MultiplicityInterval

            If c.IsUnbounded Then
                result = AdlParser.Create.MultiplicityInterval.MakeUpperUnbounded(c.MinCount)
            Else
                result = AdlParser.Create.MultiplicityInterval.MakeBounded(c.MinCount, c.MaxCount)
            End If

            Return result
        End Function

        Protected Function MakeExistence(ByVal e As RmExistence) As AdlParser.MultiplicityInterval
            Return AdlParser.Create.MultiplicityInterval.MakeBounded(e.MinCount, e.MaxCount)
        End Function

        Protected Overloads Sub BuildCodedText(ByVal parent As AdlParser.CAttribute, ByVal ConstraintID As String)
            Dim coded_text As AdlParser.CComplexObject
            Dim code_rel_node As AdlParser.CAttribute
            Dim ca_Term As AdlParser.ConstraintRef

            coded_text = MakeCComplexObjectAnonymous(parent, "DV_CODED_TEXT")
            code_rel_node = MakeCAttributeSingle(coded_text, "defining_code")
            ca_Term = AdlParser.Create.ConstraintRef.Make(Eiffel.String(ConstraintID))
            code_rel_node.PutChild(ca_Term)
        End Sub

        Protected Overloads Sub BuildCodedText(ByVal parent As AdlParser.CAttribute, ByVal a_CodePhrase As CodePhrase, ByVal assumedValue As String)
            Dim coded_text As AdlParser.CComplexObject
            Dim code_rel_node As AdlParser.CAttribute
            Dim ca_Term As AdlParser.CCodePhrase

            coded_text = MakeCComplexObjectAnonymous(parent, "DV_CODED_TEXT")
            code_rel_node = MakeCAttributeSingle(coded_text, "defining_code")

            If a_CodePhrase.Codes.Count > 0 Then
                ca_Term = aomFactory.CreateCCodePhraseFromPattern(code_rel_node, Eiffel.String(a_CodePhrase.Phrase))

                If assumedValue <> "" Then
                    ca_Term.SetAssumedValue(AdlParser.Create.CodePhrase.MakeFromString(Eiffel.String("local::" & assumedValue)))
                End If
            Else
                ca_Term = AdlParser.Create.CCodePhrase.MakeFromTerminologyId(Eiffel.String(a_CodePhrase.TerminologyID))
                code_rel_node.PutChild(ca_Term)
            End If
        End Sub

        Protected Sub BuildPlainText(ByVal parent As AdlParser.CAttribute, ByVal terms As Collections.Specialized.StringCollection)
            Dim plainText As AdlParser.CComplexObject = MakeCComplexObjectAnonymous(parent, "DV_TEXT")

            If terms.Count > 0 Then
                Dim i As Integer
                Dim cString As AdlParser.CString = aomFactory.CreateCStringMakeFromString(Eiffel.String(terms.Item(0)))

                For i = 1 To terms.Count - 1
                    cString.AddString(Eiffel.String(terms.Item(i)))
                Next

                MakeCPrimitiveObject(MakeCAttributeSingle(plainText, "value"), cString)
            End If
        End Sub

        Private Sub DuplicateHistory(ByVal rm As RmStructureCompound, ByRef RelNode As AdlParser.CAttribute)
            Dim cadlHistory, cadlEvent As AdlParser.CComplexObject
            Dim attribute As AdlParser.CAttribute
            Dim an_event As RmEvent
            Dim rm_1 As RmStructureCompound
            Dim a_history As RmHistory

            For Each rm_1 In CType(cDefinition, ArchetypeDefinition).Data
                If rm_1.Type = StructureType.History Then
                    a_history = CType(rm_1, RmHistory)
                    cadlHistory = MakeCComplexObjectIdentified(RelNode, ReferenceModel.RM_StructureName(StructureType.History), a_history.NodeId)
                    cadlHistory.SetOccurrences(MakeOccurrences(a_history.Occurrences))

                    If Not a_history.HasNameConstraint Then
                        attribute = MakeCAttributeSingle(cadlHistory, "name")
                        BuildText(attribute, a_history.NameConstraint)
                    End If
                    If a_history.isPeriodic Then
                        Dim period As New Constraint_Duration

                        attribute = MakeCAttributeSingle(cadlHistory, "period")
                        period.MinMaxValueUnits = a_history.PeriodUnits
                        'Set max and min to offset value
                        period.MinimumValue = a_history.Period
                        period.HasMinimum = True
                        period.MaximumValue = a_history.Period
                        period.HasMaximum = True
                        BuildDuration(attribute, period)
                    End If

                    ' now build the events
                    If a_history.Children.Count > 0 Then
                        attribute = MakeCAttributeMultiple(cadlHistory, "events", a_history.Children.Cardinality)
                        an_event = a_history.Children.Item(0)
                        cadlEvent = MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(StructureType.Event), an_event.NodeId)
                        cadlEvent.SetOccurrences(MakeOccurrences(an_event.Occurrences))

                        Select Case an_event.EventType
                            Case RmEvent.ObservationEventType.PointInTime
                                If an_event.hasFixedOffset Then
                                    Dim offset As New Constraint_Duration

                                    attribute = MakeCAttributeSingle(cadlEvent, "offset")
                                    offset.MinMaxValueUnits = an_event.OffsetUnits
                                    'Set max and min to offset value
                                    offset.MinimumValue = an_event.Offset
                                    offset.HasMinimum = True
                                    offset.MaximumValue = an_event.Offset
                                    offset.HasMaximum = True
                                    BuildDuration(attribute, offset)
                                End If
                            Case RmEvent.ObservationEventType.Interval

                                If an_event.AggregateMathFunction.Codes.Count > 0 Then
                                    attribute = MakeCAttributeSingle(cadlEvent, "math_function")
                                    BuildCodedText(attribute, an_event.AggregateMathFunction, "")
                                End If

                                If an_event.hasFixedDuration Then
                                    Dim fixedDuration As New Constraint_Duration

                                    attribute = MakeCAttributeSingle(cadlEvent, "width")
                                    fixedDuration.MinMaxValueUnits = an_event.WidthUnits
                                    'Set max and min to offset value
                                    fixedDuration.MinimumValue = an_event.Width
                                    fixedDuration.HasMinimum = True
                                    fixedDuration.MaximumValue = an_event.Width
                                    fixedDuration.HasMaximum = True
                                    BuildDuration(attribute, fixedDuration)

                                End If
                        End Select

                        ' runtime name
                        If an_event.HasNameConstraint Then
                            attribute = MakeCAttributeSingle(cadlEvent, "name")
                            BuildText(attribute, an_event.NameConstraint)
                        End If

                        ' data
                        attribute = MakeCAttributeSingle(cadlEvent, "data")
                        Dim objNode As AdlParser.CComplexObject = MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(rm.Type), rm.NodeId)
                        BuildStructure(rm, objNode)

                        Exit Sub
                    End If ' at least one child
                End If
            Next
        End Sub

        Private Sub BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As AdlParser.CAttribute, ByVal rmState As RmStructureCompound)
            Dim events As Object()
            Dim history_event As AdlParser.CComplexObject
            Dim attribute As AdlParser.CAttribute
            Dim embeddedState As Boolean = False

            events = BuildHistory(a_history, RelNode)

            Dim a_rm As RmStructure = Nothing

            If rmState.Children.Count > 0 Then
                a_rm = rmState.Children.items(0)
            End If

            If events.Length > 0 AndAlso Not a_rm Is Nothing Then
                Dim path As AdlParser.OgPath = Nothing

                For i As Integer = 0 To events.Length - 1
                    history_event = CType(events(i), AdlParser.CComplexObject)
                    attribute = MakeCAttributeSingle(history_event, "state")

                    'First event has the structure
                    If i = 0 Then
                        If a_rm.Type = StructureType.Slot Then
                            embeddedState = True
                            BuildSlotFromAttribute(attribute, a_rm)
                        Else
                            BuildStructure(a_rm, MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(a_rm.Type), a_rm.NodeId))
                            path = Me.GetPathOfNode(a_rm.NodeId)
                        End If
                    Else
                        If embeddedState Then
                            BuildSlotFromAttribute(attribute, a_rm)
                        ElseIf Not path Is Nothing Then
                            MakeArchetypeInternalRef(attribute, ReferenceModel.RM_StructureName(a_rm.Type), path)
                        End If
                    End If

                    attribute.SetExistence(MakeExistence(rmState.Children.Existence))
                Next
            End If
        End Sub

        Private Function BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As AdlParser.CAttribute) As Object()
            Dim cadlHistory, cadlEvent As AdlParser.CComplexObject
            Dim events_rel_node As AdlParser.CAttribute
            Dim attribute As AdlParser.CAttribute
            Dim an_event As RmEvent
            Dim dataPath As AdlParser.OgPath = Nothing
            Dim array_list_events As New ArrayList

            cadlHistory = MakeCComplexObjectIdentified(RelNode, StructureType.History.ToString.ToUpper(System.Globalization.CultureInfo.InvariantCulture), a_history.NodeId)
            cadlHistory.SetOccurrences(MakeOccurrences(a_history.Occurrences))

            If a_history.HasNameConstraint Then
                attribute = MakeCAttributeSingle(cadlHistory, "name")
                BuildText(attribute, a_history.NameConstraint)
            End If

            If a_history.isPeriodic Then
                Dim period As New Constraint_Duration

                attribute = MakeCAttributeSingle(cadlHistory, "period")
                period.MinMaxValueUnits = a_history.PeriodUnits
                'Set max and min to offset value
                period.MinimumValue = a_history.Period
                period.HasMinimum = True
                period.MaximumValue = a_history.Period
                period.HasMaximum = True
                BuildDuration(attribute, period)
            End If

            ' now build the events
            events_rel_node = MakeCAttributeMultiple(cadlHistory, "events", a_history.Children.Cardinality)

            For Each an_event In a_history.Children
                cadlEvent = MakeCComplexObjectIdentified(events_rel_node, ReferenceModel.RM_StructureName(an_event.Type), an_event.NodeId)
                'remember the events to return these
                array_list_events.Add(cadlEvent)
                cadlEvent.SetOccurrences(MakeOccurrences(an_event.Occurrences))

                Select Case an_event.Type
                    Case StructureType.Event
                        ' do nothing...
                    Case StructureType.PointEvent
                        If an_event.hasFixedOffset Then
                            Dim offset As New Constraint_Duration

                            attribute = MakeCAttributeSingle(cadlEvent, "offset")
                            offset.MinMaxValueUnits = an_event.OffsetUnits
                            'Set max and min to offset value
                            offset.MinimumValue = an_event.Offset
                            offset.HasMinimum = True
                            offset.MaximumValue = an_event.Offset
                            offset.HasMaximum = True
                            BuildDuration(attribute, offset)
                        End If
                    Case StructureType.IntervalEvent
                        If an_event.AggregateMathFunction.Codes.Count > 0 Then
                            attribute = MakeCAttributeSingle(cadlEvent, "math_function")
                            BuildCodedText(attribute, an_event.AggregateMathFunction, "")
                        End If

                        If an_event.hasFixedDuration Then
                            Dim fixedDuration As New Constraint_Duration

                            attribute = MakeCAttributeSingle(cadlEvent, "width")
                            fixedDuration.MinMaxValueUnits = an_event.WidthUnits
                            'Set max and min to offset value
                            fixedDuration.MinimumValue = an_event.Width
                            fixedDuration.HasMinimum = True
                            fixedDuration.MaximumValue = an_event.Width
                            fixedDuration.HasMaximum = True
                            BuildDuration(attribute, fixedDuration)
                        End If
                End Select

                If an_event.HasNameConstraint Then
                    attribute = MakeCAttributeSingle(cadlEvent, "name")
                    BuildText(attribute, an_event.NameConstraint)
                End If

                If Not a_history.Data Is Nothing Then
                    attribute = MakeCAttributeSingle(cadlEvent, "data")
                    Dim typeName As String = ReferenceModel.RM_StructureName(a_history.Data.Type)

                    If dataPath Is Nothing Then
                        BuildStructure(a_history.Data, MakeCComplexObjectIdentified(attribute, typeName, a_history.Data.NodeId))
                        dataPath = GetPathOfNode(a_history.Data.NodeId)
                    Else
                        MakeArchetypeInternalRef(attribute, typeName, dataPath)
                    End If
                End If
            Next

            Return array_list_events.ToArray()
        End Function

        Protected Sub BuildCluster(ByVal Cluster As RmCluster, ByRef RelNode As AdlParser.CAttribute)
            Dim cluster_cadlObj As AdlParser.CComplexObject
            Dim attribute As AdlParser.CAttribute
            Dim rm As RmStructure

            cluster_cadlObj = MakeCComplexObjectIdentified(RelNode, ReferenceModel.RM_StructureName(StructureType.Cluster), Cluster.NodeId)
            cluster_cadlObj.SetOccurrences(MakeOccurrences(Cluster.Occurrences))

            If Cluster.HasNameConstraint Then
                attribute = MakeCAttributeSingle(cluster_cadlObj, "name")
                BuildText(attribute, Cluster.NameConstraint)
            End If

            If Cluster.Children.Count > 0 Then
                attribute = MakeCAttributeMultiple(cluster_cadlObj, "items", Cluster.Children.Cardinality)
                Dim index As Integer

                For Each rm In Cluster.Children.items
                    If rm.Type = StructureType.Cluster Then
                        BuildCluster(rm, attribute)
                        index += 1
                    ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                        BuildElementOrReference(rm, attribute, index)
                        index += 1
                    ElseIf rm.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(attribute, rm)
                        index += 1
                    Else
                        Debug.Assert(False, "Type not handled")
                    End If
                Next
            End If
        End Sub

        Private Sub BuildProportion(ByVal parent As AdlParser.CAttribute, ByVal cp As Constraint_Proportion)
            Dim RatioObject As AdlParser.CComplexObject
            Dim fraction_attribute As AdlParser.CAttribute

            RatioObject = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(cp.Type))

            If cp.Numerator.HasMaximum Or cp.Numerator.HasMinimum Or cp.Numerator.Precision <> -1 Then
                fraction_attribute = MakeCAttributeSingle(RatioObject, "numerator")
                BuildReal(fraction_attribute, cp.Numerator)
            End If

            If cp.Denominator.HasMaximum Or cp.Denominator.HasMinimum Then
                fraction_attribute = MakeCAttributeSingle(RatioObject, "denominator")
                BuildReal(fraction_attribute, cp.Denominator)
            End If

            If cp.IsIntegralSet Then
                'There is a restriction on whether the instance will be integral or not
                fraction_attribute = MakeCAttributeSingle(RatioObject, "is_integral")

                If cp.IsIntegral Then
                    MakeCPrimitiveObject(fraction_attribute, aomFactory.CreateCBooleanMakeTrue())
                Else
                    MakeCPrimitiveObject(fraction_attribute, aomFactory.CreateCBooleanMakeFalse())
                End If
            End If

            If Not cp.AllowAllTypes Then
                Dim integerConstraint As AdlParser.CInteger
                Dim integerList As EiffelList.ListInteger_32

                fraction_attribute = MakeCAttributeSingle(RatioObject, "type")
                integerList = aomFactory.CreateIntegerList()

                For i As Integer = 0 To 4
                    If cp.IsTypeAllowed(i) Then
                        integerList.Extend(i)
                    End If
                Next

                integerConstraint = AdlParser.Create.CInteger.MakeList(integerList)
                MakeCPrimitiveObject(fraction_attribute, integerConstraint)
            End If
        End Sub

        Protected Sub BuildReal(ByVal parent As AdlParser.CAttribute, ByVal ct As Constraint_Real)
            Dim magnitude As AdlParser.CPrimitiveObject

            If ct.HasMaximum And ct.HasMinimum Then
                magnitude = MakeCPrimitiveObject(parent, aomFactory.CreateCRealMakeBounded(ct.MinimumRealValue, ct.MaximumRealValue, ct.IncludeMinimum, ct.IncludeMaximum))
            ElseIf ct.HasMaximum Then
                magnitude = MakeCPrimitiveObject(parent, aomFactory.CreateCRealMakeLowerUnbounded(ct.MaximumRealValue, ct.IncludeMaximum))
            ElseIf ct.HasMinimum Then
                magnitude = MakeCPrimitiveObject(parent, aomFactory.CreateCRealMakeUpperUnbounded(ct.MinimumRealValue, ct.IncludeMinimum))
            Else
                Debug.Assert(False)
                Return
            End If

            If ct.Precision > -1 Then
                'Need set precision on C_REAL
                'magnitude.set_precision(ct.Precision)
            End If

            If ct.HasAssumedValue Then
                magnitude.SetAssumedValue(CType(ct.AssumedValue, Single))
            End If
        End Sub

        Protected Sub BuildCount(ByVal parent As AdlParser.CAttribute, ByVal ct As Constraint_Count)
            Dim cadlCount As AdlParser.CComplexObject = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(ct.Type))
            Dim magnitude As AdlParser.CInteger = Nothing

            If ct.HasMaximum And ct.HasMinimum Then
                magnitude = aomFactory.CreateCIntegerMakeBounded(ct.MinimumValue, ct.MaximumValue, ct.IncludeMinimum, ct.IncludeMaximum)
            ElseIf ct.HasMaximum Then
                magnitude = aomFactory.CreateCIntegerMakeLowerUnbounded(ct.MaximumValue, ct.IncludeMaximum)
            ElseIf ct.HasMinimum Then
                magnitude = aomFactory.CreateCIntegerMakeUpperUnbounded(ct.MinimumValue, ct.IncludeMinimum)
            End If

            If Not magnitude Is Nothing Then
                MakeCPrimitiveObject(MakeCAttributeSingle(cadlCount, "magnitude"), magnitude)

                If ct.HasAssumedValue Then
                    Dim int_ref As EiffelKernel.Integer_32Ref = EiffelKernel.Create.Integer_32Ref.DefaultCreate
                    int_ref.SetItem(CType(ct.AssumedValue, Integer))
                    magnitude.SetAssumedValue(int_ref)
                End If
            End If
        End Sub

        Private Sub BuildDateTime(ByVal parent As AdlParser.CAttribute, ByVal dt As Constraint_DateTime)
            Dim attribute As AdlParser.CAttribute
            Dim an_object As AdlParser.CComplexObject
            Dim s As String
            Dim dtType As String = ""
            Dim cadlDateTime As AdlParser.CPrimitiveObject
            Dim allowAll As Boolean = False

            Select Case dt.TypeofDateTimeConstraint
                Case 11                 ' Allow all
                    s = "yyyy-??-??T??:??:??"
                    dtType = "dt"
                    allowAll = True
                Case 12                 ' Full date time
                    s = "yyyy-mm-ddTHH:MM:SS"
                    dtType = "dt"
                Case 13                 'Partial Date time
                    s = "yyyy-mm-ddTHH:??:??"
                    dtType = "dt"
                Case 14                 'Date only
                    s = "yyyy-??-??"
                    dtType = "d"
                    allowAll = True
                Case 15                'Full date
                    s = "yyyy-mm-dd"
                    dtType = "d"
                Case 16                'Partial date
                    s = "yyyy-??-XX"
                    dtType = "d"
                Case 17                'Partial date with month
                    s = "yyyy-mm-??"
                    dtType = "d"
                Case 18                'TimeOnly
                    s = "HH:??:??"
                    dtType = "t"
                    allowAll = True
                Case 19                 'Full time
                    s = "HH:MM:SS"
                    dtType = "t"
                Case 20                'Partial time
                    s = "HH:??:XX"
                    dtType = "t"
                Case 21                'Partial time with minutes
                    s = "HH:MM:??"
                    dtType = "t"
                Case Else
                    Throw New ApplicationException("Invalid date time type")
            End Select

            Select Case dtType
                Case "dt"
                    an_object = MakeCComplexObjectAnonymous(parent, "DV_DATE_TIME")

                    If Not allowAll Then
                        attribute = MakeCAttributeSingle(an_object, "value")
                        cadlDateTime = MakeCPrimitiveObject(attribute, aomFactory.CreateCDateTimeMakePattern(Eiffel.String(s)))
                    End If
                Case "d"
                    an_object = MakeCComplexObjectAnonymous(parent, "DV_DATE")

                    If Not allowAll Then
                        attribute = MakeCAttributeSingle(an_object, "value")
                        cadlDateTime = MakeCPrimitiveObject(attribute, aomFactory.CreateCDateMakePattern(Eiffel.String(s)))
                    End If
                Case "t"
                    an_object = MakeCComplexObjectAnonymous(parent, "DV_TIME")

                    If Not allowAll Then
                        attribute = MakeCAttributeSingle(an_object, "value")
                        cadlDateTime = MakeCPrimitiveObject(attribute, aomFactory.CreateCTimeMakePattern(Eiffel.String(s)))
                    End If
            End Select

        End Sub

        Protected Sub BuildSlotFromAttribute(ByVal parent As AdlParser.CAttribute, ByVal a_slot As RmSlot)
            Dim slot As AdlParser.ArchetypeSlot
            Dim typeName As EiffelKernel.String_8 = Eiffel.String(ReferenceModel.RM_StructureName(a_slot.SlotConstraint.RM_ClassType))

            If a_slot.NodeId = String.Empty Then
                slot = aomFactory.CreateArchetypeSlotAnonymous(parent, typeName)
            Else
                slot = aomFactory.CreateArchetypeSlotIdentified(parent, typeName, Eiffel.String(a_slot.NodeId))
            End If

            slot.SetOccurrences(MakeOccurrences(a_slot.Occurrences))
            BuildSlot(slot, a_slot.SlotConstraint)
        End Sub

        Protected Sub BuildSlot(ByVal slot As AdlParser.ArchetypeSlot, ByVal sl As Constraint_Slot)
            If sl.hasSlots Then
                Dim pattern As New System.Text.StringBuilder()
                Dim rmNamePrefix As String = ReferenceModel.ReferenceModelName & "-"
                Dim classPrefix As String = rmNamePrefix & ReferenceModel.RM_StructureName(sl.RM_ClassType) & "\."

                If sl.IncludeAll Then
                    slot.AddInclude(MakeAssertion("archetype_id/value", ".*"))
                ElseIf sl.Include.Items.GetLength(0) > 0 Then
                    For Each s As String In sl.Include
                        If pattern.Length > 0 Then
                            pattern.Append("|")
                        End If

                        If Not s.StartsWith(rmNamePrefix) Then
                            pattern.Append(classPrefix)
                        End If

                        pattern.Append(s)
                    Next

                    If pattern.Length > 0 Then
                        slot.AddInclude(MakeAssertion("archetype_id/value", pattern.ToString()))
                    End If
                ElseIf sl.Exclude.Items.GetLength(0) > 0 Then
                    ' have specific exclusions but no inclusions
                    slot.AddInclude(MakeAssertion("archetype_id/value", ".*"))
                End If

                pattern = New System.Text.StringBuilder()

                If sl.ExcludeAll Then
                    slot.AddExclude(MakeAssertion("archetype_id/value", ".*"))
                Else
                    For Each s As String In sl.Exclude
                        If pattern.Length > 0 Then
                            pattern.Append("|")
                        End If

                        If Not s.StartsWith(rmNamePrefix) Then
                            pattern.Append(classPrefix)
                        End If

                        pattern.Append(s)
                    Next

                    If pattern.Length > 0 Then
                        slot.AddExclude(MakeAssertion("archetype_id/value", pattern.ToString()))
                    End If
                End If

                Debug.Assert(slot.HasExcludes Or slot.HasIncludes)
            Else
                slot.AddInclude(MakeAssertion("archetype_id/value", ".*"))
            End If
        End Sub

        Private Sub BuildDuration(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint_Duration)
            Dim durationIso As New Duration
            Dim pattern As EiffelKernel.String_8 = Nothing
            Dim lower As EiffelKernel.String_8 = Nothing
            Dim upper As EiffelKernel.String_8 = Nothing

            If c.AllowableUnits <> String.Empty And c.AllowableUnits <> "PYMWDTHMS" Then
                pattern = Eiffel.String(c.AllowableUnits)
            End If

            If c.HasMinimum Then
                durationIso.ISO_Units = OceanArchetypeEditor.ISO_TimeUnits.GetIsoUnitForDuration(c.MinMaxValueUnits)
                durationIso.GUI_duration = CInt(c.MinimumValue)
                lower = Eiffel.String(durationIso.ISO_duration)
            End If

            If c.HasMaximum Then
                durationIso.ISO_Units = OceanArchetypeEditor.ISO_TimeUnits.GetIsoUnitForDuration(c.MinMaxValueUnits)
                durationIso.GUI_duration = CInt(c.MaximumValue)
                upper = Eiffel.String(durationIso.ISO_duration)
            End If

            Dim an_object As AdlParser.CComplexObject
            an_object = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(c.Type))

            If Not pattern Is Nothing Or Not lower Is Nothing Or Not upper Is Nothing Then
                Dim d As AdlParser.CDuration = aomFactory.CreateCDurationMake(pattern, lower, upper, c.IncludeMinimum, c.IncludeMaximum)
                MakeCPrimitiveObject(MakeCAttributeSingle(an_object, "value"), d)
            End If
        End Sub

        Protected Sub BuildQuantity(ByVal parent As AdlParser.CAttribute, ByVal q As Constraint_Quantity)
            Dim cadlQuantity As AdlParser.CDvQuantity = aomFactory.CreateCDvQuantity(parent)
            ' set the property constraint - it should be present

            If Not q.IsNull Then
                Debug.Assert(q.IsCoded)
                cadlQuantity.SetProperty(AdlParser.Create.CodePhrase.MakeFromString(Eiffel.String(q.PhysicalPropertyAsString)))

                If q.has_units Then
                    Dim unit As Constraint_QuantityUnit

                    For Each unit In q.Units
                        Dim magnitude As AdlParser.IntervalReal_32 = Nothing
                        Dim precision As AdlParser.IntervalInteger_32 = Nothing

                        If unit.HasMaximum Or unit.HasMinimum Then
                            If unit.HasMaximum And unit.HasMinimum Then
                                magnitude = aomFactory.CreateRealIntervalMakeBounded(unit.MinimumRealValue, unit.MaximumRealValue, unit.IncludeMinimum, unit.IncludeMaximum)
                            ElseIf unit.HasMaximum Then
                                magnitude = aomFactory.CreateRealIntervalMakeLowerUnbounded(unit.MaximumRealValue, unit.IncludeMaximum)
                            ElseIf unit.HasMinimum Then
                                magnitude = aomFactory.CreateRealIntervalMakeUpperUnbounded(unit.MinimumRealValue, unit.IncludeMinimum)
                            End If
                        End If

                        If unit.HasAssumedValue Then
                            cadlQuantity.SetAssumedValueFromUnitsMagnitude(Eiffel.String(unit.Unit), unit.AssumedValue, unit.Precision)
                        End If

                        ' Now set the precision (has to be an interval)
                        If unit.Precision > -1 Then
                            precision = aomFactory.CreateIntegerIntervalMakeBounded(unit.Precision, unit.Precision, True, True)
                        End If

                        cadlQuantity.AddUnitConstraint(Eiffel.String(unit.Unit), magnitude, precision)
                    Next
                End If
            End If
        End Sub

        Private Sub BuildBoolean(ByVal parent As AdlParser.CAttribute, ByVal b As Constraint_Boolean)
            Dim attribute As AdlParser.CAttribute = MakeCAttributeSingle(MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(b.Type)), "value")
            Dim c_value As AdlParser.CPrimitiveObject

            If b.TrueFalseAllowed Then
                c_value = MakeCPrimitiveObject(attribute, aomFactory.CreateCBooleanMakeTrueFalse())
            ElseIf b.TrueAllowed Then
                c_value = MakeCPrimitiveObject(attribute, aomFactory.CreateCBooleanMakeTrue())
            Else
                Debug.Assert(b.FalseAllowed, "Must have FalseAllowed as true if gets to here")
                c_value = MakeCPrimitiveObject(attribute, aomFactory.CreateCBooleanMakeFalse())
            End If

            If b.hasAssumedValue Then
                Dim assumed As EiffelKernel.BooleanRef = EiffelKernel.Create.BooleanRef.DefaultCreate
                assumed.SetItem(b.AssumedValue)
                c_value.Item.SetAssumedValue(assumed)
            End If
        End Sub

        Protected Sub BuildOrdinal(ByVal parent As AdlParser.CAttribute, ByVal o As Constraint_Ordinal)
            Dim c_value As AdlParser.CDvOrdinal
            Dim o_v As OrdinalValue
            c_value = aomFactory.CreateCDvOrdinal(parent)

            If o.OrdinalValues.Count > 0 Then
                For Each o_v In o.OrdinalValues
                    If o_v.InternalCode <> Nothing Then
                        Dim cadlO As AdlParser.Ordinal
                        cadlO = aomFactory.CreateOrdinal(o_v.Ordinal, Eiffel.String("local::" & o_v.InternalCode))
                        c_value.AddItem(cadlO)

                        If o.HasAssumedValue And o_v.Ordinal = CInt(o.AssumedValue) Then
                            c_value.SetAssumedValue(CInt(o.AssumedValue))
                        End If
                    End If
                Next
            End If
        End Sub

        Protected Sub BuildText(ByVal parent As AdlParser.CAttribute, ByVal t As Constraint_Text)
            Select Case t.TypeOfTextConstraint
                Case TextConstrainType.Terminology
                    If t.ConstraintCode <> "" Then
                        BuildCodedText(parent, t.ConstraintCode)
                    End If
                Case TextConstrainType.Internal
                    BuildCodedText(parent, t.AllowableValues, t.AssumedValue)
                Case TextConstrainType.Text
                    BuildPlainText(parent, t.AllowableValues.Codes)
            End Select
        End Sub

        Protected Function GetPathOfNode(ByVal NodeId As String) As AdlParser.OgPath
            Dim tablePaths As EiffelList.ListReference
            Dim path As AdlParser.OgPath = Nothing
            Dim s As String
            Dim i As Integer

            Rebuild()
            tablePaths = FlatArchetype.PhysicalPaths

            For i = 1 To tablePaths.Count
                s = tablePaths.ITh(i).Out.ToCil

                If s.EndsWith(NodeId & "]") Then
                    path = AdlParser.Create.OgPath.MakeFromString(Eiffel.String(s))
                    Exit For
                End If
            Next

            Return path
        End Function

        Private Sub BuildInterval(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint_Interval)
            Dim objNode As AdlParser.CComplexObject
            ' Interval<T>

            objNode = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(c.Type))

            'Upper of type T
            Dim attribute As AdlParser.CAttribute
            attribute = MakeCAttributeSingle(objNode, "upper")
            BuildElementConstraint(attribute, c.UpperLimit)

            'Lower of type T
            attribute = MakeCAttributeSingle(objNode, "lower")
            BuildElementConstraint(attribute, c.LowerLimit)

            'For a date or time interval, we need to set the actual class type of the lower limit
            If c.Type = ConstraintType.Interval_DateTime Then
                Dim s As String = CType(attribute.Children.ITh(1), AdlParser.CComplexObject).RmTypeName.ToCil

                If s <> "DV_DATE_TIME" Then
                    Dim i As Integer = parent.Children.Count

                    While i > 0
                        Dim o As AdlParser.CComplexObject = CType(parent.Children.ITh(i), AdlParser.CComplexObject)

                        If o.RmTypeName.ToCil <> "DV_INTERVAL<DV_DATE_TIME>" Then
                            i = i - 1
                        Else
                            i = 0
                            o._set_RmTypeName(Eiffel.String("DV_INTERVAL<" & s & ">"))
                        End If
                    End While
                End If
            End If
        End Sub

        Private Sub BuildMultiMedia(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint_MultiMedia)
            Dim objNode As AdlParser.CComplexObject
            Dim code_rel_node As AdlParser.CAttribute
            Dim ca_Term As AdlParser.CCodePhrase

            objNode = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(c.Type))

            code_rel_node = MakeCAttributeSingle(objNode, "media_type")

            If c.AllowableValues.Codes.Count > 0 Then
                ca_Term = aomFactory.CreateCCodePhraseFromPattern(code_rel_node, Eiffel.String(c.AllowableValues.Phrase))
            Else
                ca_Term = AdlParser.Create.CCodePhrase.MakeFromTerminologyId(Eiffel.String(c.AllowableValues.TerminologyID))
                code_rel_node.PutChild(ca_Term)
            End If
        End Sub

        Private Sub BuildURI(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint_URI)
            Dim objNode As AdlParser.CComplexObject

            If c.EhrUriOnly Then
                objNode = MakeCComplexObjectAnonymous(parent, "DV_EHR_URI")
            Else
                objNode = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(c.Type))
            End If

            If c.RegularExpression <> Nothing Then
                'Add a constraint to C_STRING
                Dim attribute As AdlParser.CAttribute
                attribute = MakeCAttributeSingle(objNode, "value")
                MakeCPrimitiveObject(attribute, MakeCStringMakeFromRegexp(c.RegularExpression))
            End If
        End Sub

        Private Sub BuildParsable(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint_Parsable)
            Dim objNode As AdlParser.CComplexObject = MakeCComplexObjectAnonymous(parent, "DV_PARSABLE")

            If c.AllowableFormalisms.Count > 0 Then
                'Add a constraint to C_STRING
                Dim attribute As AdlParser.CAttribute
                attribute = MakeCAttributeSingle(objNode, "formalism")
                Dim cSt As AdlParser.CString
                cSt = aomFactory.CreateCStringMakeFromString(Eiffel.String(c.AllowableFormalisms.Item(0)))

                For i As Integer = 1 To c.AllowableFormalisms.Count - 1
                    cSt.AddString(Eiffel.String(c.AllowableFormalisms.Item(i)))
                Next

                MakeCPrimitiveObject(attribute, cSt)
            End If
        End Sub

        Private Sub BuildIdentifier(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint_Identifier)
            Dim objNode As AdlParser.CComplexObject = MakeCComplexObjectAnonymous(parent, ReferenceModel.RM_DataTypeName(c.Type))

            If c.IssuerRegex <> Nothing Then
                MakeCPrimitiveObject(MakeCAttributeSingle(objNode, "issuer"), MakeCStringMakeFromRegexp(c.IssuerRegex))
            End If

            If c.TypeRegex <> Nothing Then
                MakeCPrimitiveObject(MakeCAttributeSingle(objNode, "type"), MakeCStringMakeFromRegexp(c.TypeRegex))
            End If

            If c.IDRegex <> Nothing Then
                MakeCPrimitiveObject(MakeCAttributeSingle(objNode, "id"), MakeCStringMakeFromRegexp(c.IDRegex))
            End If
        End Sub

        Protected Sub BuildElementConstraint(ByVal parent As AdlParser.CAttribute, ByVal c As Constraint)
            ' cannot have a value with no constraint on datatype
            Debug.Assert(c.Type <> ConstraintType.Any)

            Select Case c.Type
                Case ConstraintType.Quantity
                    BuildQuantity(parent, c)

                Case ConstraintType.Boolean
                    BuildBoolean(parent, c)

                Case ConstraintType.Text
                    BuildText(parent, c)

                Case ConstraintType.Ordinal
                    BuildOrdinal(parent, c)

                Case ConstraintType.Any
                    Debug.Assert(False, "Need to check this is set to the true")
                    'parent.parent.set_any_allowed()

                Case ConstraintType.Proportion
                    BuildProportion(parent, c)

                Case ConstraintType.Count
                    BuildCount(parent, c)

                Case ConstraintType.DateTime
                    BuildDateTime(parent, c)

                    'Case ConstraintType.Slot
                    '    BuildSlot(parent, c, New RmCardinality)

                Case ConstraintType.Multiple
                    For Each a_constraint As Constraint In CType(c, Constraint_Choice).Constraints
                        BuildElementConstraint(parent, a_constraint)
                    Next

                Case ConstraintType.Interval_Count, ConstraintType.Interval_Quantity, ConstraintType.Interval_DateTime
                    BuildInterval(parent, c)

                Case ConstraintType.MultiMedia
                    BuildMultiMedia(parent, c)

                Case ConstraintType.URI
                    BuildURI(parent, c)

                Case ConstraintType.Duration
                    BuildDuration(parent, c)

                    'Case ConstraintType.Currency
                    '    BuildCurrency(parent, c)

                Case ConstraintType.Identifier
                    BuildIdentifier(parent, c)

                Case ConstraintType.Parsable
                    BuildParsable(parent, c)

                Case Else
                    Debug.Assert(False, String.Format("{0} constraint type is not handled", c.ToString()))
            End Select
        End Sub

        Protected Sub BuildElementOrReference(ByVal Element As RmElement, ByRef RelNode As AdlParser.CAttribute, ByVal index As Integer)
            Dim parent As AdlParser.CAttribute

            If Element.Type = StructureType.Reference Then
                Dim path As AdlParser.OgPath = GetPathOfNode(Element.NodeId)

                If Not path Is Nothing Then
                    Dim ref_cadlRefNode As AdlParser.ArchetypeInternalRef
                    ref_cadlRefNode = MakeArchetypeInternalRef(RelNode, "ELEMENT", path)
                    ref_cadlRefNode.SetOccurrences(MakeOccurrences(Element.Occurrences))
                Else
                    'the origin of the reference has not been added yet
                    Dim ref As ReferenceToResolve

                    ref.Element = Element
                    ref.Attribute = RelNode
                    ref.Index = index

                    ReferencesToResolve.Add(ref)
                End If
            Else
                Dim element_cadlObj As AdlParser.CComplexObject

                element_cadlObj = MakeCComplexObjectIdentified(RelNode, ReferenceModel.RM_StructureName(StructureType.Element), Element.NodeId)
                element_cadlObj.SetOccurrences(MakeOccurrences(Element.Occurrences))

                If Element.HasNameConstraint Then
                    BuildText(MakeCAttributeSingle(element_cadlObj, "name"), Element.NameConstraint)
                End If

                If Element.Constraint.Type <> ConstraintType.Any Then
                    parent = MakeCAttributeSingle(element_cadlObj, "value")
                    BuildElementConstraint(parent, Element.Constraint)
                End If

                'Check for constraint on Flavours of Null
                If Element.HasNullFlavourConstraint() Then
                    Dim null_flavour_attribute As AdlParser.CAttribute
                    null_flavour_attribute = MakeCAttributeSingle(element_cadlObj, "null_flavour")
                    null_flavour_attribute.SetExistence(AdlParser.Create.MultiplicityInterval.MakeBounded(0, 1))
                    BuildCodedText(null_flavour_attribute, Element.ConstrainedNullFlavours, "")
                End If
            End If
        End Sub

        Private Sub BuildStructure(ByVal rmStruct As RmStructureCompound, ByRef objNode As AdlParser.CComplexObject)
            Dim attribute As AdlParser.CAttribute
            Dim rm As RmStructure

            ' preconditions
            Debug.Assert(rmStruct.NodeId <> "") ' anonymous

            ' now make sure there are some contents to the structure
            ' and if not set it to anyallowed
            If rmStruct.Children.Count > 0 Then
                Select Case rmStruct.Type '.TypeName
                    Case StructureType.Single ' "SINGLE"
                        attribute = MakeCAttributeSingle(objNode, "item")

                        Dim rmStr As RmStructure = rmStruct.Children.items(0)
                        If rmStr.Type = StructureType.Element Or rmStr.Type = StructureType.Reference Then
                            BuildElementOrReference(rmStr, attribute, 0)
                        ElseIf rmStr.Type = StructureType.Slot Then
                            BuildSlotFromAttribute(attribute, rmStr)
                        Else
                            Debug.Assert(False, "Type not handled")
                        End If

                    Case StructureType.List ' "LIST"
                        attribute = MakeCAttributeMultiple(objNode, "items", CType(rmStruct, RmStructureCompound).Children.Cardinality)
                        Dim index As Integer = 0

                        For Each rm In rmStruct.Children.items
                            If rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                BuildElementOrReference(rm, attribute, index)
                                index += 1
                            ElseIf rm.Type = StructureType.Slot Then
                                BuildSlotFromAttribute(attribute, rm)
                                index += 1
                            Else
                                Debug.Assert(False, "Type not handled")
                            End If
                        Next
                    Case StructureType.Tree ' "TREE"
                        attribute = MakeCAttributeMultiple(objNode, "items", CType(rmStruct, RmStructureCompound).Children.Cardinality)
                        Dim index As Integer = 0

                        For Each rm In rmStruct.Children.items
                            If rm.Type = StructureType.Cluster Then
                                BuildCluster(rm, attribute)
                                index += 1
                            ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                BuildElementOrReference(rm, attribute, index)
                                index += 1
                            ElseIf rm.Type = StructureType.Slot Then
                                BuildSlotFromAttribute(attribute, rm)
                                index += 1
                            Else
                                Debug.Assert(False, "Type not handled")
                            End If
                        Next
                    Case StructureType.Table ' "TABLE"
                        Dim table As RmTable
                        Dim b As AdlParser.CBoolean
                        Dim rh As AdlParser.CInteger

                        table = CType(rmStruct, RmTable)
                        ' set is rotated
                        attribute = MakeCAttributeSingle(objNode, "rotated")

                        If table.isRotated Then
                            b = aomFactory.CreateCBooleanMakeTrue()
                        Else
                            b = aomFactory.CreateCBooleanMakeFalse()
                        End If

                        MakeCPrimitiveObject(attribute, b)

                        ' set number of row if not one
                        If table.NumberKeyColumns > 0 Then
                            attribute = MakeCAttributeSingle(objNode, "number_key_columns")
                            rh = aomFactory.CreateCIntegerMakeBounded(table.NumberKeyColumns, table.NumberKeyColumns, True, True)
                            MakeCPrimitiveObject(attribute, rh)
                        End If

                        attribute = MakeCAttributeMultiple(objNode, "rows", New RmCardinality(rmStruct.Occurrences))
                        BuildCluster(rmStruct.Children.items(0), attribute)
                End Select
            End If

            If ReferencesToResolve.Count > 0 Then
                Dim ref_cadlRefNode As AdlParser.ArchetypeInternalRef
                Dim path As AdlParser.OgPath

                For Each ref As ReferenceToResolve In ReferencesToResolve
                    path = GetPathOfNode(ref.Element.NodeId)

                    If Not path Is Nothing Then
                        'The following code needs to be updated to allow the insertion of a reference
                        'if it appears before its defining node

                        'ref_cadlRefNode = AdlParser.Create.ARCHETYPE_INTERNAL_REF.make(Eiffel.String("ELEMENT"), path.as_string)
                        'ref.Attribute.children.insert(ref_cadlRefNode, ref.Index + 1) 'Eiffel has 1 based collections

                        'The following line (1 only) will need to be turned off)

                        ref_cadlRefNode = MakeArchetypeInternalRef(ref.Attribute, "ELEMENT", path)
                        ref_cadlRefNode.SetOccurrences(MakeOccurrences(ref.Element.Occurrences))
                    Else
                        'reference element no longer exists so build it as an element
                        Dim new_element As RmElement = ref.Element.Copy()
                        BuildElementOrReference(new_element, ref.Attribute, ref.Index)
                    End If
                Next

                ReferencesToResolve.Clear()
            End If
        End Sub

        Protected Sub BuildSubjectOfData(ByVal subject As RelatedParty, ByVal root_node As AdlParser.CComplexObject)
            Dim objnode As AdlParser.CComplexObject
            Dim attribute As AdlParser.CAttribute
            Dim a_relationship As AdlParser.CAttribute

            attribute = MakeCAttributeSingle(root_node, "subject")
            objnode = AdlParser.Create.CComplexObject.MakeAnonymous(Eiffel.String("PARTY_RELATED"))
            attribute.PutChild(objnode)
            a_relationship = MakeCAttributeSingle(objnode, "relationship")
            BuildCodedText(a_relationship, subject.Relationship, "")
        End Sub

        Protected Sub BuildParticipation(ByVal attribute As AdlParser.CAttribute, ByVal participation As RmParticipation)
            Dim cObject As AdlParser.CComplexObject
            cObject = MakeCComplexObjectAnonymous(attribute, "PARTICIPATION")
            cObject.SetOccurrences(MakeOccurrences(participation.Occurrences))

            If participation.MandatoryDateTime Then
                Dim timeAttrib As AdlParser.CAttribute = MakeCAttributeSingle(cObject, "time")
            End If

            If participation.ModeSet.Codes.Count > 0 Then
                BuildCodedText(MakeCAttributeSingle(cObject, "mode"), participation.ModeSet, "")
            End If

            If participation.FunctionConstraint.TypeOfTextConstraint <> TextConstrainType.Text Then
                Dim constraintAttribute As AdlParser.CAttribute
                constraintAttribute = MakeCAttributeSingle(cObject, "function")

                If participation.FunctionConstraint.TypeOfTextConstraint = TextConstrainType.Internal Then
                    BuildCodedText(constraintAttribute, participation.FunctionConstraint.AllowableValues, "")
                Else
                    BuildCodedText(constraintAttribute, participation.FunctionConstraint.ConstraintCode)
                End If
            End If
        End Sub

        Protected Sub BuildParticipations(ByVal aRmClass As AdlParser.CComplexObject, ByVal participations As RmStructureCompound)
            If participations.Children.Count > 0 Then
                Dim participationAttribute As AdlParser.CAttribute = MakeCAttributeMultiple(aRmClass, participations.NodeId, participations.Children.Cardinality)

                For Each p As RmParticipation In participations.Children
                    BuildParticipation(participationAttribute, p)
                Next
            End If
        End Sub

        Protected Sub BuildSection(ByVal rmChildren As Children, ByVal cadlObj As AdlParser.CComplexObject)
            ' Build a section, runtimename is already done
            Dim attribute As AdlParser.CAttribute = MakeCAttributeMultiple(cadlObj, "items", rmChildren.Cardinality)

            For Each a_structure As RmStructure In rmChildren
                If a_structure.Type = StructureType.SECTION Then
                    Dim new_section As AdlParser.CComplexObject = AdlParser.Create.CComplexObject.MakeIdentified(Eiffel.String("SECTION"), Eiffel.String(a_structure.NodeId))
                    new_section.SetOccurrences(MakeOccurrences(a_structure.Occurrences))

                    If a_structure.HasNameConstraint Then
                        BuildText(MakeCAttributeSingle(new_section, "name"), a_structure.NameConstraint)
                    End If

                    If CType(a_structure, RmSection).Children.Count > 0 Then
                        BuildSection(CType(a_structure, RmSection).Children, new_section)
                    End If

                    attribute.PutChild(new_section)
                ElseIf a_structure.Type = StructureType.Slot Then
                    BuildSlotFromAttribute(attribute, a_structure)
                Else
                    Debug.Assert(False)
                End If
            Next
        End Sub

        Private Sub BuildComposition(ByVal Rm As RmComposition, ByVal CadlObj As AdlParser.CComplexObject)
            Dim attribute As AdlParser.CAttribute = MakeCAttributeSingle(CadlObj, "category")
            Dim t As New Constraint_Text
            t.TypeOfTextConstraint = TextConstrainType.Terminology ' coded_text
            t.AllowableValues.TerminologyID = "openehr"

            If Rm.IsPersistent Then
                t.AllowableValues.Codes.Add("431") ' persistent
            Else
                t.AllowableValues.Codes.Add("433") ' event
            End If

            BuildCodedText(attribute, t.AllowableValues, "")

            Dim eventContext As AdlParser.CComplexObject = Nothing

            If Rm.HasParticipations Then
                attribute = MakeCAttributeSingle(CadlObj, "context")
                eventContext = MakeCComplexObjectAnonymous(attribute, "EVENT_CONTEXT")
                BuildParticipations(eventContext, Rm.Participations)
            End If

            ' Deal with the content and context
            If Rm.Data.Count > 0 Then
                For Each a_structure As RmStructure In Rm.Data
                    Select Case a_structure.Type
                        Case StructureType.List, StructureType.Single, StructureType.Table, StructureType.Tree
                            Dim new_structure As AdlParser.CComplexObject

                            If eventContext Is Nothing Then
                                attribute = MakeCAttributeSingle(CadlObj, "context")
                                eventContext = MakeCComplexObjectAnonymous(attribute, "EVENT_CONTEXT")
                            End If

                            attribute = MakeCAttributeSingle(eventContext, "other_context")
                            new_structure = MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(a_structure.Type), a_structure.NodeId)
                            BuildStructure(a_structure, new_structure)

                        Case StructureType.Slot
                            If eventContext Is Nothing Then
                                attribute = MakeCAttributeSingle(CadlObj, "context")
                                eventContext = MakeCComplexObjectAnonymous(attribute, "EVENT_CONTEXT")
                            End If

                            attribute = MakeCAttributeSingle(eventContext, "other_context")
                            BuildSlotFromAttribute(attribute, a_structure)

                        Case StructureType.SECTION
                            If CType(a_structure, RmSection).Children.Count > 0 Then
                                attribute = MakeCAttributeMultiple(CadlObj, "content", CType(a_structure, RmSection).Children.Cardinality)

                                For Each slot As RmSlot In CType(a_structure, RmSection).Children
                                    BuildSlotFromAttribute(attribute, slot)
                                Next
                            End If

                        Case Else
                            Debug.Assert(False)
                    End Select
                Next
            End If
        End Sub

        Protected Sub BuildRootElement(ByVal element As RmElement, ByVal CadlObj As AdlParser.CComplexObject)
            If Not element.Constraint Is Nothing Then
                If element.Constraint.Type <> ConstraintType.Any Then
                    BuildElementConstraint(MakeCAttributeSingle(CadlObj, "value"), element.Constraint)
                End If
            End If
        End Sub

        Protected Sub BuildRootCluster(ByVal rm As RmCluster, ByVal CadlObj As AdlParser.CComplexObject)
            If rm.Children.Count > 0 Then
                Dim attribute As AdlParser.CAttribute = MakeCAttributeMultiple(CadlObj, "items", rm.Children.Cardinality)
                Dim index As Integer = 0

                For Each child As RmStructure In rm.Children.items
                    If child.Type = StructureType.Cluster Then
                        BuildCluster(child, Attribute)
                        index += 1
                    ElseIf child.Type = StructureType.Element Or child.Type = StructureType.Reference Then
                        BuildElementOrReference(child, Attribute, index)
                        index += 1
                    ElseIf child.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(Attribute, child)
                        index += 1
                    Else
                        Debug.Assert(False, "Type not handled")
                    End If
                Next
            End If

            If ReferencesToResolve.Count > 0 Then
                Dim ref_cadlRefNode As AdlParser.ArchetypeInternalRef
                Dim path As AdlParser.OgPath

                For Each ref As ReferenceToResolve In ReferencesToResolve
                    path = GetPathOfNode(ref.Element.NodeId)

                    If Not path Is Nothing Then
                        ref_cadlRefNode = MakeArchetypeInternalRef(ref.Attribute, "ELEMENT", path)
                        ref_cadlRefNode.SetOccurrences(MakeOccurrences(ref.Element.Occurrences))
                    Else
                        'reference element no longer exists so build it as an element
                        Dim new_element As RmElement = ref.Element.Copy()

                        BuildElementOrReference(new_element, ref.Attribute, ref.Index)
                    End If
                Next

                ReferencesToResolve.Clear()
            End If
        End Sub

        Protected Sub BuildRootSection(ByVal rm As RmSection, ByVal CadlObj As AdlParser.CComplexObject)
            ' Build a section, runtimename is already done
            If rm.Children.Count > 0 Then
                Dim attribute As AdlParser.CAttribute = MakeCAttributeMultiple(CadlObj, "items", rm.Children.Cardinality)

                For Each a_structure As RmStructure In rm.Children
                    If a_structure.Type = StructureType.SECTION Then
                        Dim new_section As AdlParser.CComplexObject = AdlParser.Create.CComplexObject.MakeIdentified(Eiffel.String("SECTION"), Eiffel.String(a_structure.NodeId))
                        new_section.SetOccurrences(MakeOccurrences(a_structure.Occurrences))

                        If a_structure.HasNameConstraint Then
                            BuildText(MakeCAttributeSingle(new_section, "name"), a_structure.NameConstraint)
                        End If

                        If CType(a_structure, RmSection).Children.Count > 0 Then
                            BuildSection(CType(a_structure, RmSection).Children, new_section)
                        End If

                        attribute.PutChild(new_section)
                    ElseIf a_structure.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(attribute, a_structure)
                    Else
                        Debug.Assert(False)
                    End If
                Next
            End If
        End Sub

        Private Sub BuildStructure(ByVal rm As RmStructureCompound, ByVal adlArchetype As AdlParser.CComplexObject, ByVal attributeName As String)
            Dim attribute As AdlParser.CAttribute = MakeCAttributeSingle(DifferentialDefinition, attributeName)

            If rm.Children.Count > 0 Then
                If CType(rm.Children.items(0), RmStructure).Type = StructureType.Slot Then
                    BuildSlotFromAttribute(attribute, rm.Children.items(0))
                Else
                    BuildStructure(rm.Children.items(0), MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(rm.Children.items(0).Type), rm.Children.items(0).NodeId))
                End If
            End If

            If attributeName = "state" Or attributeName = "protocol" Then
                attribute.SetExistence(MakeExistence(rm.Children.Existence))
            End If
        End Sub

        Private Sub BuildProtocol(ByVal rm As RmStructureCompound, ByVal adlArchetype As AdlParser.CComplexObject)
            If rm.Children.Count > 0 Then
                Dim rmStruct As RmStructure = rm.Children.items(0)
                Dim attribute As AdlParser.CAttribute

                If rmStruct.Type = StructureType.Slot Then
                    attribute = MakeCAttributeSingle(DifferentialDefinition, "protocol")
                    BuildSlotFromAttribute(attribute, CType(rmStruct, RmSlot))
                Else
                    attribute = MakeCAttributeSingle(DifferentialDefinition, "protocol")
                    ' only 1 protocol allowed
                    BuildStructure(CType(rmStruct, RmStructureCompound), MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(rmStruct.Type), rmStruct.NodeId))
                End If

                attribute.SetExistence(MakeExistence(rm.Children.Existence))
            End If
        End Sub

        Private Sub BuildWorkFlowStep(ByVal rm As RmPathwayStep, ByVal attribute As AdlParser.CAttribute)
            Dim a_state, a_step As AdlParser.CAttribute
            Dim objNode As AdlParser.CComplexObject
            Dim code_phrase As New CodePhrase

            objNode = MakeCComplexObjectAnonymous(attribute, "ISM_TRANSITION")
            a_state = MakeCAttributeSingle(objNode, "current_state")
            code_phrase.TerminologyID = "openehr"
            code_phrase.Codes.Add((CInt(rm.StateType)).ToString)

            If rm.HasAlternativeState Then
                code_phrase.Codes.Add(CInt(rm.AlternativeState).ToString)
            End If

            BuildCodedText(a_state, code_phrase, "")

            a_step = MakeCAttributeSingle(objNode, "careflow_step")
            code_phrase = New CodePhrase
            code_phrase.Codes.Add(rm.NodeId)  ' local is default terminology, node_id of rm is same as term code of name
            BuildCodedText(a_step, code_phrase, "")
        End Sub

        Private Sub BuildPathway(ByVal rm As RmStructureCompound, ByVal arch_def As AdlParser.CComplexObject)
            Dim attribute As AdlParser.CAttribute

            If rm.Children.Count > 0 Then
                attribute = MakeCAttributeSingle(DifferentialDefinition, "ism_transition")

                For Each pathway_step As RmPathwayStep In rm.Children
                    BuildWorkFlowStep(pathway_step, attribute)
                Next
            End If
        End Sub

        Private Sub BuildActivity(ByVal rm As RmActivity, ByVal attribute As AdlParser.CAttribute)
            Dim objNode As AdlParser.CComplexObject = MakeCComplexObjectIdentified(attribute, "ACTIVITY", rm.NodeId)
            objNode.SetOccurrences(MakeOccurrences(rm.Occurrences))

            Dim escapedString As String = rm.ArchetypeId

            If escapedString <> "" Then
                Dim i As Integer = escapedString.IndexOf("\")

                'Must have at least one escaped . or it is not valid unless it is the end
                If i < 0 Or i = escapedString.Length - 1 Then
                    escapedString = escapedString.Replace(".", "\.")
                End If

                escapedString = ReferenceModel.ReferenceModelName & "-ACTION\." + escapedString
                attribute = MakeCAttributeSingle(objNode, "action_archetype_id")
                MakeCPrimitiveObject(attribute, MakeCStringMakeFromRegexp(escapedString))
            End If

            For Each rm_struct As RmStructure In rm.Children
                attribute = MakeCAttributeSingle(objNode, "description")

                Select Case rm_struct.Type
                    Case StructureType.List, StructureType.Single, StructureType.Tree, StructureType.Table
                        Dim EIF_struct As AdlParser.CComplexObject = MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(rm_struct.Type), rm_struct.NodeId)
                        BuildStructure(CType(rm_struct, RmStructureCompound), EIF_struct)

                    Case StructureType.Slot
                        ' this allows a structure to be archetyped at this point
                        Debug.Assert(CType(rm_struct, RmStructure).Type = StructureType.Slot)
                        BuildSlotFromAttribute(attribute, rm_struct)
                End Select
            Next
        End Sub

        Private Sub BuildInstruction(ByVal data As RmChildren)
            For Each rm As RmStructureCompound In data
                Select Case rm.Type
                    Case StructureType.Activities
                        'ToDo: Set cardinality on this attribute
                        Dim attribute As AdlParser.CAttribute = MakeCAttributeMultiple(DifferentialDefinition, "activities", New RmCardinality(0))

                        For Each activity As RmActivity In rm.Children
                            BuildActivity(activity, attribute)
                        Next
                    Case StructureType.Protocol
                        BuildProtocol(rm, DifferentialDefinition)
                    Case Else
                        Debug.Assert(False, rm.Type.ToString() & " - Type under INSTRUCTION not handled")
                End Select
            Next
        End Sub

        Private Sub BuildAction(ByVal rm As RmStructureCompound, ByVal a_definition As AdlParser.CComplexObject)
            Dim action_spec As RmStructure
            Dim attribute As AdlParser.CAttribute
            Dim objNode As AdlParser.CComplexObject

            If rm.Children.items.Length > 0 Then
                attribute = MakeCAttributeSingle(DifferentialDefinition, "description")
                action_spec = rm.Children.items(0)

                Select Case action_spec.Type
                    Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                        objNode = MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(action_spec.Type), rm.Children.items(0).NodeId)
                        BuildStructure(action_spec, objNode)
                    Case StructureType.Slot
                        ' allows action to be specified in another archetype
                        Dim slot As RmSlot = CType(action_spec, RmSlot)
                        BuildSlotFromAttribute(attribute, slot)
                End Select
            End If
        End Sub

        Public Overridable Sub MakeParseTree()
            If Not mSynchronised Then
                Dim rm As RmStructureCompound

                DifferentialArchetype.ResetDefinition()
                DifferentialArchetype.SetDescription(CType(mDescription, ADL_Description).ADL_Description)

                If Not DifferentialArchetype.Translations Is Nothing AndAlso Not DifferentialArchetype.Translations.Content Is Nothing Then
                    DifferentialArchetype.Translations.ClearAll()
                End If

                For Each transDetail As TranslationDetails In mTranslationDetails.Values
                    DifferentialArchetype.AddTranslation(CType(transDetail, ADL_TranslationDetails).ADL_Translation)
                Next

                If Not cDefinition Is Nothing Then
                    aomFactory = archetypeParser.ConstraintModelFactory

                    If cDefinition.hasNameConstraint Then
                        BuildText(MakeCAttributeSingle(DifferentialDefinition, "name"), cDefinition.NameConstraint)
                    End If

                    Debug.Assert(ReferenceModel.IsValidArchetypeDefinition(cDefinition.Type))

                    Select Case cDefinition.Type
                        Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                            If DifferentialDefinition.AnyAllowed AndAlso CType(cDefinition, ArchetypeDefinition).Data.Count > 0 Then
                                'This can arise if the archetype has been saved with no children then
                                'items have been added later - this is peculiar to Tree, List and Table.
                                DifferentialDefinition.SetOccurrences(MakeOccurrences(New RmCardinality(0)))
                            End If

                            BuildStructure(cDefinition, DifferentialDefinition)

                        Case StructureType.Cluster
                            BuildRootCluster(cDefinition, DifferentialDefinition)

                        Case StructureType.Element
                            BuildRootElement(cDefinition, DifferentialDefinition)

                        Case StructureType.SECTION
                            BuildRootSection(cDefinition, DifferentialDefinition)

                        Case StructureType.COMPOSITION
                            BuildComposition(cDefinition, DifferentialDefinition)

                        Case StructureType.EVALUATION, StructureType.ENTRY
                            BuildEntryAttributes(CType(cDefinition, RmEntry), DifferentialDefinition)

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.State
                                        BuildStructure(rm, DifferentialDefinition, "state")
                                    Case StructureType.Protocol
                                        BuildProtocol(rm, DifferentialDefinition)
                                    Case StructureType.Data
                                        BuildStructure(rm, DifferentialDefinition, "data")
                                End Select
                            Next

                        Case StructureType.ADMIN_ENTRY
                            BuildEntryAttributes(CType(cDefinition, RmEntry), DifferentialDefinition)

                            Try
                                Dim rmStruct As RmStructureCompound = CType(CType(cDefinition, ArchetypeDefinition).Data.items(0), RmStructureCompound).Children.items(0)
                                BuildStructure(rmStruct, MakeCComplexObjectIdentified(MakeCAttributeSingle(DifferentialDefinition, "data"), ReferenceModel.RM_StructureName(rmStruct.Type), rmStruct.NodeId))
                            Catch
                                'ToDo - process error
                                Debug.Assert(False, "Error building structure")
                            End Try

                        Case StructureType.OBSERVATION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), DifferentialDefinition)

                            'Add state to each event so need to be sure of requirements
                            Dim state_to_be_added As Boolean = True
                            Dim rm_state As RmStructureCompound = Nothing
                            Dim rm_state_history As RmStructureCompound = Nothing
                            Dim rm_data As RmStructureCompound = Nothing
                            Dim rm_protocol As RmStructureCompound = Nothing

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.Protocol
                                        rm_protocol = rm
                                    Case StructureType.Data
                                        'remember the data structure
                                        rm_data = rm
                                    Case StructureType.State
                                        'for the moment saving the state data on the first event EventSeries if there is one
                                        Dim a_rm As RmStructure = Nothing

                                        If rm.Children.items.Length > 0 Then
                                            a_rm = rm.Children.items(0)
                                        End If

                                        If a_rm Is Nothing OrElse a_rm.Type <> StructureType.History Then
                                            rm_state = rm
                                        Else
                                            ' can have EventSeries for each state
                                            rm_state_history = a_rm
                                        End If
                                End Select
                            Next

                            'Add the data
                            If Not rm_data Is Nothing Then
                                Dim attribute As AdlParser.CAttribute = MakeCAttributeSingle(DifferentialDefinition, "data")

                                For Each a_rm As RmStructureCompound In rm_data.Children.items
                                    Select Case a_rm.Type '.TypeName
                                        Case StructureType.History
                                            If Not rm_state Is Nothing Then
                                                BuildHistory(a_rm, attribute, rm_state)
                                            Else
                                                BuildHistory(a_rm, attribute)
                                            End If
                                        Case Else
                                            Debug.Assert(False) '?OBSOLETE
                                            Dim objNode As AdlParser.CComplexObject
                                            objNode = MakeCComplexObjectIdentified(attribute, ReferenceModel.RM_StructureName(a_rm.Type), a_rm.NodeId)
                                            BuildStructure(a_rm, objNode)
                                    End Select
                                Next
                            End If

                            If Not rm_state_history Is Nothing Then
                                BuildHistory(rm_state_history, MakeCAttributeSingle(DifferentialDefinition, "state"))
                            End If

                            If Not rm_protocol Is Nothing Then
                                BuildProtocol(rm_protocol, DifferentialDefinition)
                            End If

                        Case StructureType.INSTRUCTION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), DifferentialDefinition)
                            BuildInstruction(CType(cDefinition, ArchetypeDefinition).Data)

                        Case StructureType.ACTION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), DifferentialDefinition)

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.ISM_TRANSITION
                                        BuildPathway(rm, DifferentialDefinition)
                                    Case StructureType.ActivityDescription
                                        BuildAction(rm, DifferentialDefinition)
                                    Case StructureType.Slot
                                        ' this allows a structure to be archetyped at this point
                                        Debug.Assert(CType(rm.Children.items(0), RmStructure).Type = StructureType.Slot)
                                        BuildStructure(rm, DifferentialDefinition)
                                    Case StructureType.Protocol
                                        BuildProtocol(rm, DifferentialDefinition)
                                End Select
                            Next
                    End Select

                    If HasLinkConstraints() Then
                        BuildLinks(Definition.RootLinks, DifferentialDefinition)
                    End If

                    mSynchronised = True
                End If
            End If
        End Sub

        Public Sub Rebuild()
            Dim arch As AdlParser.ArchRepArchetype = archetypeParser.SelectedArchetype

            If Not arch Is Nothing Then
                arch.SignalDifferentialEdited()
                arch.DifferentialArchetype.Rebuild()
                arch.DifferentialArchetype.Synchronise()
                arch.Validate()
                arch.DifferentialArchetype.SetAdlVersion(Eiffel.String("1.4"))
                arch.FlatArchetype.SetAdlVersion(arch.DifferentialArchetype.AdlVersion)
                archetypeParser.AppRoot.SetAdlVersionForFlatOutput(arch.DifferentialArchetype.AdlVersion)
            End If
        End Sub

        Public Sub SetArchetypeDigest()
            Dim digest As String = AM.ArchetypeModelBuilder.ArchetypeDigest(CanonicalArchetype())
            FlatArchetype.Description.AddOtherDetail(Eiffel.String(AM.ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID), Eiffel.String(digest))
        End Sub

        Public Function CanonicalArchetype() As XMLParser.ARCHETYPE
            Return XMLParser.OpenEhr.V1.Its.Xml.AM.ArchetypeModelBuilder.CanonicalArchetype(AM.ArchetypeModelBuilder.Build(FlatArchetype, New AM.CloneConstraintVisitor()))
        End Function

        Sub BuildLinks(ByVal cLinks As System.Collections.Generic.List(Of RmLink), ByVal cObject As AdlParser.CComplexObject)
            Dim linksAttribute As AdlParser.CAttribute = MakeCAttributeMultiple(cObject, "links", New RmCardinality(0))

            For Each l As RmLink In cLinks
                If l.HasConstraint Then
                    Dim linkObject As AdlParser.CComplexObject = MakeCComplexObjectAnonymous(linksAttribute, "LINK")

                    If l.Meaning.TypeOfTextConstraint <> TextConstrainType.Text Then
                        BuildText(MakeCAttributeSingle(linkObject, "meaning"), l.Meaning)
                    End If

                    If l.LinkType.TypeOfTextConstraint <> TextConstrainType.Text Then
                        MakeCAttributeSingle(linkObject, "type")
                    End If

                    If l.Target.RegularExpression <> String.Empty Then
                        BuildURI(MakeCAttributeSingle(linkObject, "target"), l.Target)
                    End If
                End If
            Next
        End Sub

        Sub BuildEntryAttributes(ByVal anEntry As RmEntry, ByVal archetypeDefinition As AdlParser.CComplexObject)
            If anEntry.SubjectOfData.Relationship.Codes.Count > 0 Then
                BuildSubjectOfData(anEntry.SubjectOfData, archetypeDefinition)
            End If

            If anEntry.ProviderIsMandatory Then
                Dim attribute As AdlParser.CAttribute = MakeCAttributeSingle(archetypeDefinition, "provider")
                Dim objnode As AdlParser.CComplexObject = AdlParser.Create.CComplexObject.MakeAnonymous(Eiffel.String("PARTY_PROXY"))
                objnode.SetOccurrences(MakeOccurrences(New RmCardinality(1, 1)))
                attribute.PutChild(objnode)
            End If

            If anEntry.HasOtherParticipations Then
                BuildParticipations(DifferentialDefinition, CType(cDefinition, RmEntry).OtherParticipations)
            End If
        End Sub

        Sub New(ByRef parser As AdlParser.ArchetypeParser, ByVal archetypeID As ArchetypeID, ByVal primaryLanguage As String)
            ' call to create a brand new archetype
            MyBase.New(primaryLanguage, archetypeID)
            archetypeParser = parser

            Dim id As AdlParser.ArchetypeId = AdlParser.Create.ArchetypeId.MakeFromString(Eiffel.String(archetypeID.ToString))
            archetypeParser.CreateNewArchetype(id, Eiffel.String(sPrimaryLanguageCode))
            DifferentialDefinition.SetNodeId(DifferentialArchetype.Concept)
            mDescription = New ADL_Description(DifferentialArchetype.OriginalLanguage.CodeString.ToCil)
        End Sub

        Sub New(ByRef parser As AdlParser.ArchetypeParser, ByVal fileManager As FileManagerLocal)
            ' call to create an in memory archetype from the ADL parser
            MyBase.New(fileManager.OntologyManager.PrimaryLanguageCode)
            archetypeParser = parser
            Dim arch As AdlParser.Archetype = FlatArchetype

            If Not arch Is Nothing Then
                mArchetypeID = New ArchetypeID(arch.ArchetypeId.AsString.ToCil)
                ReferenceModel.SetArchetypedClass(mArchetypeID.ReferenceModelEntity)

                If Not arch.ParentArchetypeId Is Nothing Then
                    sParentArchetypeID = arch.ParentArchetypeId.AsString.ToCil
                End If

                mDescription = New ADL_Description(arch.Description)

                If Not arch.Translations Is Nothing Then
                    arch.Translations.Start()

                    Do While Not arch.Translations.Off
                        Dim details As ADL_TranslationDetails = New ADL_TranslationDetails(CType(arch.Translations.ItemForIteration, AdlParser.TranslationDetails))
                        mTranslationDetails.Add(details.Language, details)
                        arch.Translations.Forth()
                    Loop
                End If

                Select Case mArchetypeID.ReferenceModelEntity
                    Case StructureType.COMPOSITION
                        cDefinition = New ADL_COMPOSITION(arch.Definition, fileManager)
                        cDefinition.RootNodeId = arch.Concept.ToCil
                    Case StructureType.SECTION
                        cDefinition = New ADL_SECTION(arch.Definition, fileManager)
                    Case StructureType.List, StructureType.Tree, StructureType.Single
                        cDefinition = New RmStructureCompound(arch.Definition, fileManager)
                    Case StructureType.Table
                        cDefinition = New RmTable(arch.Definition, fileManager)
                    Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION, StructureType.ADMIN_ENTRY, StructureType.ACTION
                        cDefinition = New ADL_ENTRY(arch.Definition, fileManager)
                        cDefinition.RootNodeId = arch.Concept.ToCil
                    Case StructureType.Cluster
                        cDefinition = New RmCluster(arch.Definition, fileManager)
                    Case StructureType.Element
                        cDefinition = New ADL_RmElement(arch.Definition, fileManager)
                    Case Else
                        Debug.Assert(False)
                End Select
            End If

            'get the bit with the life cycle version - not possible at the moment
            Dim y() As String = mArchetypeID.ToString.Split(".")

            If y.Length > 2 Then
                Dim i As Integer

                For i = 2 To y.Length - 1
                    sLifeCycle = sLifeCycle & y(i)
                Next
            End If
        End Sub

    End Class
End Namespace



'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is ADL_Archetype.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'
