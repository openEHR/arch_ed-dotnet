'
'
'	component:   "openEHR Archetype Project"
'	description: "Builds all XML Archetypes"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Archetype.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'
'Option Strict On
Option Explicit On 
Imports XMLParser

Namespace ArchetypeEditor.XML_Classes

    Public Class XML_Archetype
        Inherits Archetype

        'Builds all archetypes at present

        Private mXmlArchetype As XMLParser.ARCHETYPE
        Private mArchetypeParser As XMLParser.XmlArchetypeParser
        Private mAomFactory As XMLParser.AomFactory

        Private Structure ReferenceToResolve
            Dim Element As RmElement
            Dim Attribute As XMLParser.C_ATTRIBUTE
            Dim Index As Integer
        End Structure

        Protected ReferencesToResolve As ArrayList = New ArrayList

        Public Overrides Property ConceptCode() As String
            Get
                Return mXmlArchetype.concept
            End Get
            Set(ByVal Value As String)
                mXmlArchetype.concept = Value
            End Set
        End Property

        Public Overrides ReadOnly Property ArchetypeAvailable() As Boolean
            Get
                Return mArchetypeParser.ArchetypeAvailable
            End Get
        End Property

        Public Overrides Property Archetype_ID() As ArchetypeID
            Get
                Try
                    Return mArchetypeID
                Catch
                    Debug.Assert(False)
                    Return Nothing
                End Try
            End Get
            Set(ByVal Value As ArchetypeID)
                SetArchetypeId(Value)
            End Set
        End Property

        Public Overrides Sub UpdateArchetypeId() 'Forces changes made to ArchetypeID to be updated in parser
            SetArchetypeId(Archetype_ID)
        End Sub

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
                If Not mXmlArchetype.parent_archetype_id Is Nothing Then
                    Return mXmlArchetype.parent_archetype_id.value
                Else
                    Return ""
                End If
            End Get
            Set(ByVal Value As String)
                If mXmlArchetype.parent_archetype_id Is Nothing Then
                    mXmlArchetype.parent_archetype_id = New XMLParser.ARCHETYPE_ID
                End If
                mXmlArchetype.parent_archetype_id.value = Value
            End Set
        End Property

        Public Overrides ReadOnly Property SourceCode() As String
            Get
                Return mArchetypeParser.Serialise
            End Get
        End Property

        Public Overrides ReadOnly Property SerialisedArchetype(ByVal a_format As String) As String
            Get
                If a_format.ToLowerInvariant() = "xml" Then
                    MakeParseTree()
                    Return mArchetypeParser.Serialise
                Else
                    Debug.Assert(False, "Cannot return format '" + a_format + " from XML parser!")
                    Return "Error - " + a_format + " is not available for XML parser"
                End If
            End Get
        End Property

        Public Overrides ReadOnly Property Paths(ByVal LanguageCode As String, ByVal parserIsSynchronised As Boolean, Optional ByVal Logical As Boolean = False) As String()
            Get
                Dim list As System.Collections.ArrayList

                ' must call the prepareToSave to ensure it is accurate
                If Not Filemanager.Master.FileLoading AndAlso Not parserIsSynchronised Then
                    MakeParseTree()
                End If

                ' showing the task with logical paths takes a lot of space
                If Logical Then
                    list = mArchetypeParser.LogicalPaths(LanguageCode)
                Else
                    list = mArchetypeParser.PhysicalPaths()
                End If

                Return list.ToArray(GetType(String))
            End Get
        End Property

        Public Overrides Sub Specialise(ByVal shortConceptName As String, ByRef ontology As OntologyManager)
            mArchetypeParser.SpecialiseArchetype(shortConceptName)
            ' Update the GUI tables with the new term
            ontology.UpdateTerm(New XML_Term(mArchetypeParser.Ontology.TermDefinition(ontology.LanguageCode, mXmlArchetype.concept)))
            mArchetypeID.Concept &= "-" & shortConceptName
        End Sub

        Public Sub RemoveUnusedCodes()
            mArchetypeParser.Ontology.RemoveUnusedCodes()
        End Sub

        Public Shared Function HasAttributeName(ByVal complexObject As XMLParser.C_COMPLEX_OBJECT, ByVal attributeName As String) As Boolean
            If Not complexObject.attributes Is Nothing Then
                For Each attr As XMLParser.C_ATTRIBUTE In complexObject.attributes
                    If attr.rm_attribute_name = attributeName Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Function

        Public Shared Sub RemoveAttribute(ByVal complexObject As XMLParser.C_COMPLEX_OBJECT, ByVal attributeName As String)
            Dim i As Integer

            For i = 0 To complexObject.attributes.Length - 1
                Dim attr As XMLParser.C_ATTRIBUTE = complexObject.attributes(i)

                If attr.rm_attribute_name = attributeName Then
                    Exit For
                End If
            Next

            Array.Clear(complexObject.attributes, i, 1)
        End Sub

        Protected Sub SetArchetypeId(ByVal an_archetype_id As ArchetypeID)
            Try
                If Not mArchetypeParser.ArchetypeAvailable Then
                    mArchetypeParser.NewArchetype(an_archetype_id.ToString(), sPrimaryLanguageCode, Main.Instance.DefaultLanguageCodeSet)
                    mXmlArchetype = mArchetypeParser.Archetype
                    mArchetypeParser.SetDefinitionId(mXmlArchetype.concept)
                    setDefinition()
                Else
                    ' does this involve a change in the entity (affects the GUI a great deal!)
                    If mXmlArchetype.archetype_id.value.Contains(an_archetype_id.ReferenceModelEntity) Then
                        Debug.Assert(False, "Not handled")
                        ' will need to reset the GUI to the new entity
                        setDefinition()
                    End If

                    mXmlArchetype.archetype_id.value = an_archetype_id.ToString()
                End If

                ' set the internal variable last in case errors
                mArchetypeID = an_archetype_id
            Catch e As Exception
                Debug.Assert(False, "Error setting archetype id")
                Beep()
            End Try
        End Sub

        Protected Sub ArchetypeID_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles mArchetypeID.ArchetypeID_Changed
            SetArchetypeId(CType(sender, ArchetypeID))
        End Sub

        Private Function MakeAssertion(ByVal id As String, ByVal expression As String) As XMLParser.ASSERTION
            Dim id_expression_leaf, id_pattern_expression_leaf As XMLParser.EXPR_LEAF
            Dim match_operator As XMLParser.EXPR_BINARY_OPERATOR
            Dim assert As New XMLParser.ASSERTION

            Debug.Assert((Not id Is Nothing) And (id <> ""))

            id_expression_leaf = New XMLParser.EXPR_LEAF()
            id_expression_leaf.type = "String"
            id_expression_leaf.item = id
            id_expression_leaf.reference_type = "attribute"

            id_pattern_expression_leaf = New XMLParser.EXPR_LEAF()
            id_pattern_expression_leaf.type = "C_STRING"
            id_pattern_expression_leaf.reference_type = "constraint"

            Dim c_s As New XMLParser.C_STRING()
            c_s.pattern = expression

            id_pattern_expression_leaf.item = c_s

            match_operator = New XMLParser.EXPR_BINARY_OPERATOR()
            match_operator.type = "Boolean"
            match_operator.operator = Global.XMLParser.OPERATOR_KIND.Item2007
            match_operator.left_operand = id_expression_leaf
            match_operator.right_operand = id_pattern_expression_leaf

            assert.expression = match_operator

            Return assert
        End Function

        Private Function MakeCardinality(ByVal c As RmCardinality, Optional ByVal IsOrdered As Boolean = True) As XMLParser.CARDINALITY
            Dim cardObj As XMLParser.CARDINALITY

            cardObj = New XMLParser.CARDINALITY()
            cardObj.interval = New XMLParser.IntervalOfInteger()

            With cardObj.interval
                .lower = c.MinCount
                .lowerSpecified = True
                .lower_included = c.IncludeLower
                .lower_includedSpecified = True
                .upper_unbounded = c.IsUnbounded

                If Not c.IsUnbounded Then
                    .upper = c.MaxCount
                    .upperSpecified = True
                    .upper_included = c.IncludeUpper
                    .upper_includedSpecified = True
                End If

                ' Validate Interval PostConditions 
                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
            End With

            cardObj.is_ordered = c.Ordered
            Return cardObj
        End Function

        Private Function MakeOccurrences(ByVal c As RmCardinality) As XMLParser.IntervalOfInteger
            Dim an_interval As New XMLParser.IntervalOfInteger()

            With an_interval
                .lower = c.MinCount
                .lowerSpecified = True
                .lower_included = c.IncludeLower
                .lower_includedSpecified = True
                .upper_unbounded = c.IsUnbounded

                If Not c.IsUnbounded Then
                    .upper = c.MaxCount
                    .upperSpecified = True
                    .upper_included = c.IncludeUpper
                    .upper_includedSpecified = True
                Else
                    .upper_unbounded = True
                End If

                ' Validate Interval PostConditions
                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
            End With

            'an_interval.includes_maximum = True
            'an_interval.minimum = 0
            'If c.IsUnbounded Then
            '    an_interval.includes_minimum = c.IncludeLower
            '    an_interval.minimum = c.MinCount
            'Else
            '    an_interval.includes_minimum = c.IncludeLower
            '    an_interval.minimum = c.MinCount
            '    an_interval.includes_maximum = c.IncludeUpper
            '    an_interval.maximum = c.MaxCount
            'End If

            Return an_interval
        End Function

        Private Sub BuildCodedText(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal constraintId As String)
            Dim codedText As XMLParser.C_COMPLEX_OBJECT
            Dim code As XMLParser.C_ATTRIBUTE
            Dim term As New XMLParser.CONSTRAINT_REF

            codedText = mAomFactory.MakeComplexObject(attribute, "DV_CODED_TEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
            code = mAomFactory.MakeSingleAttribute(codedText, "defining_code", attribute.existence)
            term.rm_type_name = "CODE_PHRASE"
            term.node_id = ""
            term.occurrences = MakeOccurrences(New RmCardinality(1, 1))
            term.reference = constraintId
            mAomFactory.add_object(code, term)
        End Sub

        Private Sub BuildCodedTextInternal(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal codePhrase As CodePhrase, ByVal assumedValue As String)
            Dim codedText As XMLParser.C_COMPLEX_OBJECT
            Dim code As XMLParser.C_ATTRIBUTE
            Dim term As New XMLParser.C_CODE_PHRASE

            term.terminology_id = New XMLParser.TERMINOLOGY_ID
            codedText = mAomFactory.MakeComplexObject(attribute, "DV_CODED_TEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
            code = mAomFactory.MakeSingleAttribute(codedText, "defining_code", attribute.existence)
            term.rm_type_name = "CODE_PHRASE"
            term.node_id = ""
            term.occurrences = MakeOccurrences(New RmCardinality(1, 1))

            If codePhrase.Codes.Count > 0 Then
                term.terminology_id.value = codePhrase.TerminologyID
                ReDim term.code_list(codePhrase.Codes.Count - 1)

                For i As Integer = 0 To codePhrase.Codes.Count - 1
                    term.code_list(i) = codePhrase.Codes(i)
                Next

                If assumedValue <> "" Then
                    term.assumed_value = New XMLParser.CODE_PHRASE
                    term.assumed_value.code_string = assumedValue
                    term.assumed_value.terminology_id = New XMLParser.TERMINOLOGY_ID
                    term.assumed_value.terminology_id.value = codePhrase.TerminologyID
                End If
            Else
                term.terminology_id = New XMLParser.TERMINOLOGY_ID
                term.terminology_id.value = codePhrase.TerminologyID
            End If

            mAomFactory.add_object(code, term)
        End Sub

        Private Sub BuildPlainText(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal TermList As Collections.Specialized.StringCollection)
            Dim plain_text As XMLParser.C_COMPLEX_OBJECT
            Dim value_rel_node As XMLParser.C_ATTRIBUTE
            Dim cString As XMLParser.C_STRING
            Dim xmlSimple As XMLParser.C_PRIMITIVE_OBJECT

            plain_text = mAomFactory.MakeComplexObject(attribute, "DV_TEXT", "", MakeOccurrences(New RmCardinality(1, 1)))

            If TermList.Count > 0 Then
                Dim i As Integer
                value_rel_node = mAomFactory.MakeSingleAttribute(plain_text, "value", attribute.existence)
                cString = New XMLParser.C_STRING()
                cString.list = Array.CreateInstance(GetType(String), TermList.Count)

                For i = 0 To TermList.Count - 1
                    cString.list(i) = TermList.Item(i)
                Next

                xmlSimple = mAomFactory.MakePrimitiveObject(value_rel_node, cString)
            End If
        End Sub

        Private Sub DuplicateHistory(ByVal rm As RmStructureCompound, ByRef RelNode As XMLParser.C_ATTRIBUTE)

            Dim xmlHistory, xmlEvent As XMLParser.C_COMPLEX_OBJECT
            Dim a As XMLParser.C_ATTRIBUTE
            Dim an_event As RmEvent
            Dim rm_1 As RmStructureCompound
            Dim a_history As RmHistory

            For Each rm_1 In CType(cDefinition, ArchetypeDefinition).Data
                If rm_1.Type = StructureType.History Then
                    a_history = CType(rm_1, RmHistory)
                    xmlHistory = mAomFactory.MakeComplexObject( _
                        RelNode, _
                        ReferenceModel.RM_StructureName(StructureType.History), _
                        a_history.NodeId, _
                        MakeOccurrences(a_history.Occurrences))

                    If Not a_history.HasNameConstraint Then
                        a = mAomFactory.MakeSingleAttribute(xmlHistory, "name", rm.Existence.XmlExistence)
                        BuildText(a, a_history.NameConstraint)
                    End If

                    If a_history.isPeriodic Then
                        Dim durationConstraint As New Constraint_Duration
                        a = mAomFactory.MakeSingleAttribute(xmlHistory, "period", rm.Existence.XmlExistence)
                        durationConstraint.MinMaxValueUnits = a_history.PeriodUnits

                        'Set max and min to offset value
                        durationConstraint.MinimumValue = a_history.Period
                        durationConstraint.HasMinimum = True
                        durationConstraint.MaximumValue = a_history.Period
                        durationConstraint.HasMaximum = True
                        BuildDuration(a, durationConstraint)
                    End If

                    ' now build the events
                    If a_history.Children.Count > 0 Then
                        a = mAomFactory.MakeMultipleAttribute( _
                            xmlHistory, _
                            "events", _
                            MakeCardinality(a_history.Children.Cardinality), a_history.Children.Existence.XmlExistence)

                        an_event = a_history.Children.Item(0)
                        xmlEvent = mAomFactory.MakeComplexObject( _
                            a, _
                            ReferenceModel.RM_StructureName(StructureType.Event), _
                            an_event.NodeId, _
                            MakeOccurrences(an_event.Occurrences))

                        Select Case an_event.EventType
                            Case RmEvent.ObservationEventType.PointInTime
                                If an_event.hasFixedOffset Then
                                    Dim durationConstraint As New Constraint_Duration
                                    a = mAomFactory.MakeSingleAttribute(xmlEvent, "offset", rm.Existence.XmlExistence)
                                    durationConstraint.MinMaxValueUnits = an_event.OffsetUnits

                                    'Set max and min to offset value
                                    durationConstraint.MinimumValue = an_event.Offset
                                    durationConstraint.HasMinimum = True
                                    durationConstraint.MaximumValue = an_event.Offset
                                    durationConstraint.HasMaximum = True
                                    BuildDuration(a, durationConstraint)
                                End If
                            Case RmEvent.ObservationEventType.Interval
                                If Not an_event.AggregateMathFunction Is Nothing AndAlso an_event.AggregateMathFunction.Codes.Count > 0 Then
                                    a = mAomFactory.MakeSingleAttribute(xmlEvent, "math_function", rm.Existence.XmlExistence)
                                    BuildCodedTextInternal(a, an_event.AggregateMathFunction, "")
                                End If

                                If an_event.hasFixedDuration Then
                                    Dim durationConstraint As New Constraint_Duration
                                    a = mAomFactory.MakeSingleAttribute(xmlEvent, "width", rm.Existence.XmlExistence)
                                    durationConstraint.MinMaxValueUnits = an_event.WidthUnits

                                    'Set max and min to offset value
                                    durationConstraint.MinimumValue = an_event.Width
                                    durationConstraint.HasMinimum = True
                                    durationConstraint.MaximumValue = an_event.Width
                                    durationConstraint.HasMaximum = True
                                    BuildDuration(a, durationConstraint)
                                End If
                        End Select

                        ' runtime name
                        If an_event.HasNameConstraint Then
                            a = mAomFactory.MakeSingleAttribute(xmlEvent, "name", rm.Existence.XmlExistence)
                            BuildText(a, an_event.NameConstraint)
                        End If

                        ' data
                        a = mAomFactory.MakeSingleAttribute(xmlEvent, "data", rm.Existence.XmlExistence)

                        Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject( _
                            a, _
                            ReferenceModel.RM_StructureName(rm.Type), _
                            rm.NodeId, MakeOccurrences(New RmCardinality(1, 1)))

                        BuildStructure(rm, objNode)
                        Exit Sub 'FIME: Spaghetti code!
                    End If ' at least one child
                End If
            Next
        End Sub

        Private Sub BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As XMLParser.C_ATTRIBUTE, ByVal rmState As RmStructureCompound)
            Dim events As Object()
            Dim history_event As XMLParser.C_COMPLEX_OBJECT
            Dim a As XMLParser.C_ATTRIBUTE
            Dim embeddedState As Boolean = False

            events = BuildHistory(a_history, RelNode)

            Dim a_rm As RmStructure = Nothing

            If rmState.Children.Count > 0 Then
                a_rm = rmState.Children.Items(0)
            End If

            If events.Length > 0 AndAlso Not a_rm Is Nothing Then
                Dim path As String = "?"

                For i As Integer = 0 To events.Length - 1
                    history_event = CType(events(i), XMLParser.C_COMPLEX_OBJECT)
                    a = mAomFactory.MakeSingleAttribute(history_event, "state", rmState.Children.Existence.XmlExistence)

                    'First event has the structure
                    If i = 0 Then
                        If a_rm.Type = StructureType.Slot Then
                            embeddedState = True
                            BuildSlotFromRm(a, a_rm)
                        Else
                            Dim objNode As XMLParser.C_COMPLEX_OBJECT
                            objNode = mAomFactory.MakeComplexObject(a, ReferenceModel.RM_StructureName(a_rm.Type), a_rm.NodeId, MakeOccurrences(New RmCardinality(1, 1)))
                            BuildStructure(a_rm, objNode)
                            path = GetPathOfNode(a_rm.NodeId)
                        End If
                    Else
                        If embeddedState Then
                            BuildSlotFromRm(a, a_rm)
                        Else
                            'create a reference
                            Dim ref_xmlRefNode As XMLParser.ARCHETYPE_INTERNAL_REF

                            If Not path = "?" Then
                                ref_xmlRefNode = mAomFactory.MakeArchetypeRef(a, ReferenceModel.RM_StructureName(a_rm.Type), path)
                            Else
                                Debug.Assert(False, "Error with path")
                            End If
                        End If
                    End If
                Next
            End If
        End Sub

        Private Function BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As XMLParser.C_ATTRIBUTE) As Object()
            Dim xmlHistory, xmlEvent As XMLParser.C_COMPLEX_OBJECT
            Dim events_rel_node As New XMLParser.C_MULTIPLE_ATTRIBUTE()
            Dim a As XMLParser.C_ATTRIBUTE
            Dim an_event As RmEvent
            Dim dataPath As String = Nothing
            Dim array_list_events As New ArrayList

            Try
                xmlHistory = mAomFactory.MakeComplexObject( _
                    RelNode, _
                    StructureType.History.ToString.ToUpper(System.Globalization.CultureInfo.InvariantCulture), _
                    a_history.NodeId, _
                    MakeOccurrences(a_history.Occurrences))

                If a_history.HasNameConstraint Then
                    a = New XMLParser.C_SINGLE_ATTRIBUTE()
                    a.rm_attribute_name = "name"
                    BuildText(a, a_history.NameConstraint)
                End If

                If a_history.isPeriodic Then
                    Dim durationConstraint As New Constraint_Duration
                    a = mAomFactory.MakeSingleAttribute(xmlHistory, "period", a_history.Existence.XmlExistence)
                    durationConstraint.MinMaxValueUnits = a_history.PeriodUnits

                    'Set max and min to offset value
                    durationConstraint.MinimumValue = a_history.Period
                    durationConstraint.HasMinimum = True
                    durationConstraint.MaximumValue = a_history.Period
                    durationConstraint.HasMaximum = True
                    BuildDuration(a, durationConstraint)
                End If

                ' now build the events
                events_rel_node = mAomFactory.MakeMultipleAttribute( _
                    xmlHistory, _
                    "events", _
                    MakeCardinality(a_history.Children.Cardinality), a_history.Children.Existence.XmlExistence)

                For i As Integer = 0 To a_history.Children.Count - 1
                    an_event = a_history.Children.Item(i)
                    xmlEvent = mAomFactory.MakeComplexObject( _
                        ReferenceModel.RM_StructureName(an_event.Type), _
                        an_event.NodeId, _
                        MakeOccurrences(an_event.Occurrences))

                    ' add to the array list to return from function
                    array_list_events.Add(xmlEvent)

                    'Add the object to the attribute
                    mAomFactory.add_object(events_rel_node, xmlEvent)

                    Select Case an_event.Type
                        Case StructureType.Event
                            ' do nothing...
                        Case StructureType.PointEvent
                            If an_event.hasFixedOffset Then
                                Dim durationConstraint As New Constraint_Duration
                                a = mAomFactory.MakeSingleAttribute(xmlEvent, "offset", an_event.Existence.XmlExistence)
                                durationConstraint.MinMaxValueUnits = an_event.OffsetUnits

                                'Set max and min to offset value
                                durationConstraint.MinimumValue = an_event.Offset
                                durationConstraint.HasMinimum = True
                                durationConstraint.MaximumValue = an_event.Offset
                                durationConstraint.HasMaximum = True
                                BuildDuration(a, durationConstraint)
                            End If
                        Case StructureType.IntervalEvent
                            If Not an_event.AggregateMathFunction Is Nothing AndAlso an_event.AggregateMathFunction.Codes.Count > 0 Then
                                a = mAomFactory.MakeSingleAttribute(xmlEvent, "math_function", an_event.Existence.XmlExistence)
                                BuildCodedTextInternal(a, an_event.AggregateMathFunction, "")
                            End If

                            If an_event.hasFixedDuration Then
                                Dim durationConstraint As New Constraint_Duration
                                a = mAomFactory.MakeSingleAttribute(xmlEvent, "width", an_event.Existence.XmlExistence)
                                durationConstraint.MinMaxValueUnits = an_event.WidthUnits

                                'Set max and min to offset value
                                durationConstraint.MinimumValue = an_event.Width
                                durationConstraint.HasMinimum = True
                                durationConstraint.MaximumValue = an_event.Width
                                durationConstraint.HasMaximum = True
                                BuildDuration(a, durationConstraint)
                            End If
                    End Select

                    If an_event.HasNameConstraint Then
                        a = mAomFactory.MakeSingleAttribute(xmlEvent, "name", an_event.Existence.XmlExistence)
                        BuildText(a, an_event.NameConstraint)
                    End If

                    If Not a_history.Data Is Nothing Then
                        a = mAomFactory.MakeSingleAttribute(xmlEvent, "data", an_event.Existence.XmlExistence)
                        Dim typeName As String = ReferenceModel.RM_StructureName(a_history.Data.Type)

                        If dataPath Is Nothing Then
                            BuildStructure(a_history.Data, mAomFactory.MakeComplexObject(a, typeName, a_history.Data.NodeId, MakeOccurrences(New RmCardinality(1, 1))))
                            dataPath = GetPathOfNode(a_history.Data.NodeId)
                        Else
                            mAomFactory.MakeArchetypeRef(a, typeName, dataPath)
                        End If
                    End If
                Next

            Catch ex As Exception
                Debug.Assert(False)
            End Try

            Return array_list_events.ToArray()
        End Function

        Private Sub BuildCluster(ByVal Cluster As RmCluster, ByRef RelNode As XMLParser.C_ATTRIBUTE)
            Dim clusterXmlObj As XMLParser.C_COMPLEX_OBJECT
            Dim a As XMLParser.C_ATTRIBUTE
            Dim rm As RmStructure

            clusterXmlObj = mAomFactory.MakeComplexObject(RelNode, ReferenceModel.RM_StructureName(StructureType.Cluster), Cluster.NodeId, MakeOccurrences(Cluster.Occurrences))

            If Cluster.HasNameConstraint Then
                a = mAomFactory.MakeSingleAttribute(clusterXmlObj, "name", Cluster.Existence.XmlExistence)
                BuildText(a, Cluster.NameConstraint)
            End If

            If Cluster.Children.Count > 0 Then
                a = mAomFactory.MakeMultipleAttribute(clusterXmlObj, "items", MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered), Cluster.Children.Existence.XmlExistence)
                Dim index As Integer = 0

                For Each rm In Cluster.Children.Items
                    If rm.Type = StructureType.Cluster Then
                        BuildCluster(rm, a)
                        index += 1
                    ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                        BuildElementOrReference(rm, a, index)
                        index += 1
                    ElseIf rm.Type = StructureType.Slot Then
                        BuildSlotFromRm(a, rm)
                        index += 1
                    Else
                        Debug.Assert(False, "Type not handled")
                    End If
                Next
            End If
        End Sub

        Private Sub BuildProportion(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal cp As Constraint_Proportion)
            Dim proportion As XMLParser.C_COMPLEX_OBJECT
            Dim fraction As XMLParser.C_ATTRIBUTE

            proportion = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(cp.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))

            If cp.Numerator.HasMaximum Or cp.Numerator.HasMinimum Then
                fraction = mAomFactory.MakeSingleAttribute(proportion, "numerator", attribute.existence)
                BuildReal(fraction, cp.Numerator)
            End If

            If cp.HasDenominator And (cp.Denominator.HasMaximum Or cp.Denominator.HasMinimum) Then
                fraction = mAomFactory.MakeSingleAttribute(proportion, "denominator", attribute.existence)
                BuildReal(fraction, cp.Denominator)
            End If

            If cp.IsIntegralSet Then
                'There is a restriction on whether the instance will be integral or not
                fraction = mAomFactory.MakeSingleAttribute(proportion, "is_integral", attribute.existence)
                Dim b As New XMLParser.C_BOOLEAN

                If cp.IsIntegral Then
                    b.false_valid = False
                    b.true_valid = True
                Else
                    b.false_valid = True
                    b.true_valid = False
                End If

                mAomFactory.MakePrimitiveObject(fraction, b)
            End If

            If Not cp.AllowsAllTypes Then
                Dim integerConstraint As New XMLParser.C_INTEGER
                fraction = mAomFactory.MakeSingleAttribute(proportion, "type", attribute.existence)
                Dim allowedTypes As New ArrayList

                For i As Integer = 0 To 4
                    If cp.IsTypeAllowed(i) Then
                        allowedTypes.Add(i)
                    End If
                Next

                integerConstraint.list = allowedTypes.ToArray(GetType(Int32))
                mAomFactory.MakePrimitiveObject(fraction, integerConstraint)
            End If
        End Sub

        Private Sub BuildReal(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal ct As Constraint_Count)
            Dim magnitude As New XMLParser.C_REAL

            If ct.HasMaximum Or ct.HasMinimum Then
                magnitude.range = New XMLParser.IntervalOfReal
            End If

            If ct.HasMinimum Then
                magnitude.range.lower = ct.MinimumValue
                magnitude.range.lowerSpecified = True
                magnitude.range.lower_included = ct.IncludeMinimum
                magnitude.range.lower_includedSpecified = True
            Else
                magnitude.range.lower_unbounded = True
            End If

            If ct.HasMaximum Then
                magnitude.range.upper = ct.MaximumValue
                magnitude.range.upperSpecified = True
                magnitude.range.upper_included = ct.IncludeMaximum
                magnitude.range.upper_includedSpecified = True
            Else
                magnitude.range.upper_unbounded = True
            End If

            If ct.HasAssumedValue Then
                magnitude.assumed_valueSpecified = True
                magnitude.assumed_value = CSng(ct.AssumedValue)
            End If

            mAomFactory.MakePrimitiveObject(attribute, magnitude)

            ' Validate Interval PostConditions
            With magnitude.range
                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
            End With
        End Sub

        Private Sub BuildCount(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal ct As Constraint_Count)
            Dim a As XMLParser.C_ATTRIBUTE
            Dim xmlCount As XMLParser.C_COMPLEX_OBJECT
            Dim magnitude As XMLParser.C_PRIMITIVE_OBJECT

            xmlCount = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(ct.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))

            If ct.HasMaximum Or ct.HasMinimum Then
                ' set the magnitude constraint
                a = mAomFactory.MakeSingleAttribute(xmlCount, "magnitude", attribute.existence)
                Dim c_int As New XMLParser.C_INTEGER

                If ct.HasMaximum Or ct.HasMinimum Then
                    c_int.range = New XMLParser.IntervalOfInteger

                    If ct.HasMaximum Then
                        c_int.range.upper = CInt(ct.MaximumValue)
                        c_int.range.upperSpecified = True
                        c_int.range.upper_included = ct.IncludeMaximum
                        c_int.range.upper_includedSpecified = True
                    Else
                        c_int.range.upper_unbounded = True
                    End If

                    If ct.HasMinimum Then
                        c_int.range.lower = CInt(ct.MinimumValue)
                        c_int.range.lowerSpecified = True
                        c_int.range.lower_included = ct.IncludeMinimum
                        c_int.range.lower_includedSpecified = True
                    Else
                        c_int.range.lower_unbounded = True
                    End If
                End If

                If ct.HasAssumedValue Then
                    c_int.assumed_valueSpecified = True
                    c_int.assumed_value = CInt(ct.AssumedValue)
                End If

                magnitude = mAomFactory.MakePrimitiveObject(a, c_int)

                With c_int.range
                    Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                    Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                    Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                    Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                    Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                    Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                End With
            End If
        End Sub

        Private Sub BuildDateTime(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal dt As Constraint_DateTime)
            Dim a As XMLParser.C_ATTRIBUTE
            Dim o As XMLParser.C_COMPLEX_OBJECT
            Dim cd As XMLParser.C_PRIMITIVE
            Dim xmlDateTime As New XMLParser.C_PRIMITIVE_OBJECT
            Dim allowAll As Boolean = False

            Select Case dt.TypeofDateTimeConstraint
                Case 11                 ' Allow all
                    Dim dtc As New XMLParser.C_DATE_TIME
                    cd = dtc
                    allowAll = True
                Case 12                 ' Full date time
                    Dim dtc As New XMLParser.C_DATE_TIME
                    dtc.pattern = "YYYY-MM-DDTHH:MM:SS"
                    cd = dtc
                Case 13                 'Partial Date time
                    Dim dtc As New XMLParser.C_DATE_TIME
                    dtc.pattern = "YYYY-MM-DDTHH:??:??"
                    cd = dtc
                Case 14                 'Date only
                    Dim dtc As New XMLParser.C_DATE
                    cd = dtc
                    allowAll = True
                Case 15                'Full date
                    Dim dtc As New XMLParser.C_DATE
                    dtc.pattern = "YYYY-MM-DD"
                    cd = dtc
                Case 16                'Partial date
                    Dim dtc As New XMLParser.C_DATE
                    dtc.pattern = "YYYY-??-XX"
                    cd = dtc
                Case 17                'Partial date with month
                    Dim dtc As New XMLParser.C_DATE
                    dtc.pattern = "YYYY-MM-??"
                    cd = dtc
                Case 18                'TimeOnly
                    Dim dtc As New XMLParser.C_TIME
                    cd = dtc
                    allowAll = True
                Case 19                 'Full time
                    Dim dtc As New XMLParser.C_TIME
                    dtc.pattern = "HH:MM:SS"
                    cd = dtc
                Case 20                'Partial time
                    Dim dtc As New XMLParser.C_TIME
                    dtc.pattern = "HH:??:XX"
                    cd = dtc
                Case 21                'Partial time with minutes
                    Dim dtc As New XMLParser.C_TIME
                    dtc.pattern = "HH:MM:??"
                    cd = dtc
                Case Else
                    Debug.Assert(False, "Not handled")
                    Return
            End Select

            Dim a_type As String = ""
            Select Case cd.GetType().ToString().ToLowerInvariant()
                Case "xmlparser.c_date_time"
                    a_type = "DV_DATE_TIME"
                Case "xmlparser.c_date"
                    a_type = "DV_DATE"
                Case "xmlparser.c_time"
                    a_type = "DV_TIME"
            End Select

            o = mAomFactory.MakeComplexObject(attribute, a_type, "", MakeOccurrences(New RmCardinality(1, 1)))

            If Not allowAll Then
                a = mAomFactory.MakeSingleAttribute(o, "value", attribute.existence)
                mAomFactory.MakePrimitiveObject(a, cd)
            End If
        End Sub

        Protected Sub BuildSlotFromRm(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal a_slot As RmSlot)
            Dim slot As New XMLParser.ARCHETYPE_SLOT
            slot.rm_type_name = ReferenceModel.RM_StructureName(a_slot.SlotConstraint.RM_ClassType)
            slot.occurrences = MakeOccurrences(a_slot.Occurrences)

            If a_slot.NodeId Is Nothing Then
                slot.node_id = ""
            Else
                slot.node_id = a_slot.NodeId
            End If

            BuildSlot(slot, a_slot.SlotConstraint)
            mAomFactory.add_object(attribute, slot)
        End Sub

        Private Sub BuildSlot(ByRef slot As XMLParser.ARCHETYPE_SLOT, ByVal sl As Constraint_Slot)
            If sl.hasSlots Then
                Dim pattern As New System.Text.StringBuilder()
                Dim rmNamePrefix As String = ReferenceModel.ReferenceModelName & "-"
                Dim classPrefix As String = rmNamePrefix & ReferenceModel.RM_StructureName(sl.RM_ClassType) & "\."

                If sl.IncludeAll Then
                    mAomFactory.AddIncludeToSlot(slot, MakeAssertion("archetype_id/value", ".*"))
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
                        mAomFactory.AddIncludeToSlot(slot, MakeAssertion("archetype_id/value", pattern.ToString()))
                    End If
                ElseIf sl.Exclude.Items.GetLength(0) > 0 Then
                    ' have specific exclusions but no inclusions
                    mAomFactory.AddIncludeToSlot(slot, MakeAssertion("archetype_id/value", ".*"))
                End If

                pattern = New System.Text.StringBuilder()

                If sl.ExcludeAll Then
                    mAomFactory.AddExcludeToSlot(slot, MakeAssertion("archetype_id/value", ".*"))
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
                        mAomFactory.AddExcludeToSlot(slot, MakeAssertion("archetype_id/value", pattern.ToString))
                    End If
                End If
            Else
                mAomFactory.AddIncludeToSlot(slot, MakeAssertion("archetype_id/value", ".*"))
            End If
        End Sub

        Private Sub BuildDuration(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Duration)
            Dim an_object As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(c.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))
            Dim objNode As XMLParser.C_PRIMITIVE_OBJECT
            Dim d As New XMLParser.C_DURATION

            Dim durationISO As New Duration()

            If c.HasMaximum Or c.HasMinimum Then
                d.range = New XMLParser.IntervalOfDuration()
                durationISO.ISO_Units = Main.ISO_TimeUnits.GetIsoUnitForDuration(c.MinMaxValueUnits)

                If c.HasMinimum Then
                    durationISO.GUI_duration = CInt(c.MinimumValue)
                    d.range.lower = durationISO.ISO_duration
                    d.range.lower_included = c.IncludeMinimum
                    d.range.lower_includedSpecified = True
                Else
                    d.range.lower_unbounded = True
                End If

                If c.HasMaximum Then
                    durationISO.GUI_duration = CInt(c.MaximumValue)
                    d.range.upper = durationISO.ISO_duration
                    d.range.upper_included = c.IncludeMaximum
                    d.range.upper_includedSpecified = True
                Else
                    d.range.upper_unbounded = True
                End If

                With d.range
                    Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                    Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                    Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                    Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                End With
            End If

            If Not String.IsNullOrEmpty(c.AllowableUnits) AndAlso c.AllowableUnits <> "PYMWDTHMS" Then
                d.pattern = c.AllowableUnits
            End If

            If Not d.range Is Nothing Or Not String.IsNullOrEmpty(d.pattern) Or Not String.IsNullOrEmpty(d.assumed_value) Then
                Dim a As XMLParser.C_SINGLE_ATTRIBUTE = mAomFactory.MakeSingleAttribute(an_object, "value", attribute.existence)
                objNode = mAomFactory.MakePrimitiveObject(a, d)
            End If
        End Sub

        Private Sub BuildQuantity(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal q As Constraint_Quantity)
            Dim cQuantity As New XMLParser.C_DV_QUANTITY
            cQuantity.rm_type_name = "DV_QUANTITY"
            cQuantity.node_id = ""
            cQuantity.occurrences = MakeOccurrences(New RmCardinality(1, 1))
            mAomFactory.add_object(attribute, cQuantity)

            If Not q.IsNull Then
                Debug.Assert(q.IsCoded)

                Dim cp As New XMLParser.CODE_PHRASE
                cp.code_string = q.OpenEhrCode.ToString()
                cp.terminology_id = New XMLParser.TERMINOLOGY_ID
                cp.terminology_id.value = "openehr"
                cQuantity.property = cp

                If q.HasUnits Then
                    cQuantity.list = Array.CreateInstance(GetType(XMLParser.C_QUANTITY_ITEM), q.Units.Count)

                    For i As Integer = 1 To q.Units.Count
                        Dim unit As Constraint_QuantityUnit = q.Units(i)
                        Dim magnitude As XMLParser.IntervalOfReal = Nothing
                        Dim cUnit As XMLParser.C_QUANTITY_ITEM = New XMLParser.C_QUANTITY_ITEM
                        cUnit.units = unit.Unit

                        If unit.HasMaximum Or unit.HasMinimum Then
                            magnitude = New XMLParser.IntervalOfReal

                            If unit.HasMaximum Then
                                magnitude.upper = unit.MaximumRealValue
                                magnitude.upperSpecified = True
                                magnitude.upper_included = unit.IncludeMaximum
                                magnitude.upper_includedSpecified = True
                            Else
                                magnitude.upper_unbounded = True
                            End If

                            If unit.HasMinimum Then
                                magnitude.lower = unit.MinimumRealValue
                                magnitude.lowerSpecified = True
                                magnitude.lower_included = unit.IncludeMinimum
                                magnitude.lower_includedSpecified = True
                            Else
                                magnitude.lower_unbounded = True
                            End If

                            ' Validate Interval PostConditions
                            With magnitude
                                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                            End With
                        End If

                        If Not magnitude Is Nothing Then
                            cUnit.magnitude = magnitude
                        End If

                        If unit.HasAssumedValue Then
                            cQuantity.assumed_value = New XMLParser.DV_QUANTITY
                            cQuantity.assumed_value.units = unit.Unit
                            cQuantity.assumed_value.magnitude = unit.AssumedValue
                            cQuantity.assumed_value.precision = unit.Precision
                        End If

                        If unit.Precision > -1 Then
                            cUnit.precision = New XMLParser.IntervalOfInteger
                            cUnit.precision.lower = unit.Precision
                            cUnit.precision.upper = unit.Precision
                            cUnit.precision.lower_included = True
                            cUnit.precision.upper_included = True
                            cUnit.precision.lowerSpecified = True
                            cUnit.precision.upperSpecified = True
                            cUnit.precision.lower_includedSpecified = True
                            cUnit.precision.upper_includedSpecified = True
                        End If

                        'vb collection is base 1, cQuantity.list is base 0
                        cQuantity.list(i - 1) = cUnit
                    Next
                End If
            End If
        End Sub

        Private Sub BuildBoolean(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal b As Constraint_Boolean)
            Dim o As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(b.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))
            Dim a As XMLParser.C_SINGLE_ATTRIBUTE = mAomFactory.MakeSingleAttribute(o, "value", attribute.existence)
            Dim c_bool As New XMLParser.C_BOOLEAN

            If b.TrueFalseAllowed Then
                c_bool.false_valid = True
                c_bool.true_valid = True
                mAomFactory.MakePrimitiveObject(a, c_bool)
            ElseIf b.TrueAllowed Then
                c_bool.false_valid = False
                c_bool.true_valid = True
                mAomFactory.MakePrimitiveObject(a, c_bool)
            ElseIf b.FalseAllowed Then
                c_bool.false_valid = True
                c_bool.true_valid = False
                mAomFactory.MakePrimitiveObject(a, c_bool)
            End If

            If b.HasAssumedValue Then
                c_bool.assumed_value = b.AssumedValue
                c_bool.assumed_valueSpecified = True
            End If
        End Sub

        Private Sub BuildOrdinal(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal o As Constraint_Ordinal)
            Dim c_value As XMLParser.C_DV_ORDINAL
            Dim o_v As OrdinalValue

            c_value = New XMLParser.C_DV_ORDINAL()
            c_value.rm_type_name = "DV_ORDINAL"
            c_value.list = Array.CreateInstance(GetType(XMLParser.DV_ORDINAL), o.OrdinalValues.Count)
            c_value.node_id = ""
            c_value.occurrences = MakeOccurrences(New RmCardinality(1, 1))

            If o.OrdinalValues.Count > 0 Then
                Dim i As Integer = 0
                For Each o_v In o.OrdinalValues
                    If o_v.InternalCode <> Nothing Then
                        Dim xmlO As New XMLParser.DV_ORDINAL

                        '1 value int
                        xmlO.value = o_v.Ordinal.ToString()

                        '1 symbol DV_CODED_TEXT    
                        xmlO.symbol = New XMLParser.DV_CODED_TEXT
                        xmlO.symbol.defining_code = New XMLParser.CODE_PHRASE
                        xmlO.symbol.defining_code.code_string = o_v.InternalCode
                        xmlO.symbol.defining_code.terminology_id = New XMLParser.TERMINOLOGY_ID
                        xmlO.symbol.defining_code.terminology_id.value = "local"
                        xmlO.symbol.value = ""

                        c_value.list(i) = xmlO
                        i += 1
                    End If
                Next
            End If

            If o.HasAssumedValue Then
                c_value.assumed_value = New XMLParser.DV_ORDINAL
                c_value.assumed_value.symbol = New XMLParser.DV_CODED_TEXT
                c_value.assumed_value.symbol.defining_code = New XMLParser.CODE_PHRASE
                c_value.assumed_value.symbol.defining_code.code_string = o.AssumedValue_CodeString
                c_value.assumed_value.symbol.defining_code.terminology_id = New XMLParser.TERMINOLOGY_ID
                c_value.assumed_value.symbol.defining_code.terminology_id.value = o.AssumedValue_TerminologyId
                c_value.assumed_value.symbol.value = ""
                c_value.assumed_value.value = CStr(o.AssumedValue)
            End If

            mAomFactory.add_object(attribute, c_value)
        End Sub

        Private Sub BuildText(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal t As Constraint_Text)
            Select Case t.TypeOfTextConstraint
                Case TextConstraintType.Terminology
                    If t.ConstraintCode <> "" Then
                        BuildCodedText(attribute, t.ConstraintCode)
                    End If
                Case TextConstraintType.Internal
                    BuildCodedTextInternal(attribute, t.AllowableValues, CStr(t.AssumedValue))
                Case TextConstraintType.Text
                    BuildPlainText(attribute, t.AllowableValues.Codes)
            End Select
        End Sub

        Protected Function GetPathOfNode(ByVal NodeId As String) As String
            Dim result As String = ""

            For Each s As String In mArchetypeParser.PhysicalPaths()
                If s.EndsWith(NodeId & "]") Then
                    result = s
                    Exit For
                End If
            Next

            Return result
        End Function

        Private Sub BuildInterval(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Interval)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(c.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))

            'Upper of type T
            Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(objNode, "upper", attribute.existence)
            BuildElementConstraint(a, c.UpperLimit)

            'Lower of type T
            a = mAomFactory.MakeSingleAttribute(objNode, "lower", attribute.existence)
            BuildElementConstraint(a, c.LowerLimit)

            'For a date or time interval, we need to set the actual class type of the lower limit
            If c.Kind = ConstraintKind.Interval_DateTime AndAlso Not a Is Nothing AndAlso Not a.children Is Nothing Then
                Dim s As String = CType(a.children(0), XMLParser.C_COMPLEX_OBJECT).rm_type_name

                If s <> "DV_DATE_TIME" Then
                    Dim i As Integer = attribute.children.Length

                    While i > 0
                        i = i - 1
                        Dim o As XMLParser.C_COMPLEX_OBJECT = CType(attribute.children(i), XMLParser.C_COMPLEX_OBJECT)

                        If o.rm_type_name = "DV_INTERVAL<DV_DATE_TIME>" Then
                            i = 0
                            o.rm_type_name = "DV_INTERVAL<" & s & ">"
                        End If
                    End While
                End If
            End If
        End Sub

        Private Sub BuildMultiMedia(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_MultiMedia)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT
            Dim code_rel_node As XMLParser.C_ATTRIBUTE
            Dim ca_Term As XMLParser.C_CODE_PHRASE

            objNode = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(c.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))
            code_rel_node = mAomFactory.MakeSingleAttribute(objNode, "media_type", attribute.existence)
            ca_Term = New XMLParser.C_CODE_PHRASE
            ca_Term.rm_type_name = "CODE_PHRASE"
            ca_Term.node_id = ""
            ca_Term.occurrences = MakeOccurrences(New RmCardinality(1, 1))
            ca_Term.terminology_id = New XMLParser.TERMINOLOGY_ID
            ca_Term.terminology_id.value = c.AllowableValues.TerminologyID

            If c.AllowableValues.TerminologyID = "" Then
                ca_Term.terminology_id.value = "local"
            End If

            If c.AllowableValues.Codes.Count > 0 Then
                ca_Term.code_list = Array.CreateInstance(GetType(String), c.AllowableValues.Codes.Count)
                c.AllowableValues.Codes.CopyTo(ca_Term.code_list, 0)
            End If

            mAomFactory.add_object(code_rel_node, ca_Term)
        End Sub

        Private Sub BuildURI(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_URI)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT

            If c.EhrUriOnly Then
                objNode = mAomFactory.MakeComplexObject(attribute, "DV_EHR_URI", "", MakeOccurrences(New RmCardinality(1, 1)))
            Else
                objNode = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(c.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))
            End If

            If Not String.IsNullOrEmpty(c.RegularExpression) Then
                'Add a constraint to C_STRING
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(objNode, "value", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.RegularExpression
                mAomFactory.MakePrimitiveObject(a, cSt)
            End If
        End Sub

        Private Sub BuildParsable(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Parsable)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(c.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))

            If c.AllowableFormalisms.Count > 0 Then
                'Add a constraint to C_STRING
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(objNode, "formalism", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.list = c.AllowableFormalisms.ToArray()
                mAomFactory.MakePrimitiveObject(a, cSt)
            End If
        End Sub

        Private Sub BuildIdentifier(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Identifier)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, ReferenceModel.RM_DataTypeName(c.Kind), "", MakeOccurrences(New RmCardinality(1, 1)))

            If c.IssuerRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(objNode, "issuer", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.IssuerRegex
                mAomFactory.MakePrimitiveObject(a, cSt)
            End If

            If c.TypeRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(objNode, "type", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.TypeRegex
                mAomFactory.MakePrimitiveObject(a, cSt)
            End If

            If c.IDRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(objNode, "id", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.IDRegex
                mAomFactory.MakePrimitiveObject(a, cSt)
            End If
        End Sub

        Private Sub BuildChoice(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Choice)
            For Each constraint As Constraint In c.Constraints
                Dim slotConstraint As Constraint_Slot = TryCast(constraint, Constraint_Slot)

                If Not slotConstraint Is Nothing Then
                    Dim slot As New RmSlot()
                    slot.SlotConstraint = slotConstraint
                    slot.Occurrences.IsUnbounded = True
                    BuildSlotFromRm(attribute, slot)
                Else
                    BuildElementConstraint(attribute, constraint)
                End If
            Next
        End Sub

        Private Sub BuildElementConstraint(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint)
            ' cannot have a value with no constraint on datatype
            Debug.Assert(c.Kind <> ConstraintKind.Any)

            Select Case c.Kind
                Case ConstraintKind.Quantity
                    BuildQuantity(attribute, c)

                Case ConstraintKind.Boolean
                    BuildBoolean(attribute, c)

                Case ConstraintKind.Text
                    BuildText(attribute, c)

                Case ConstraintKind.Ordinal
                    BuildOrdinal(attribute, c)

                Case ConstraintKind.Any
                    Debug.Assert(False, "Need to check this is set to the true")

                Case ConstraintKind.Proportion
                    BuildProportion(attribute, c)

                Case ConstraintKind.Count
                    BuildCount(attribute, c)

                Case ConstraintKind.DateTime
                    BuildDateTime(attribute, c)

                Case ConstraintKind.Multiple
                    BuildChoice(attribute, CType(c, Constraint_Choice))

                Case ConstraintKind.Interval_Count, ConstraintKind.Interval_Quantity, ConstraintKind.Interval_DateTime
                    BuildInterval(attribute, c)

                Case ConstraintKind.MultiMedia
                    BuildMultiMedia(attribute, c)

                Case ConstraintKind.URI
                    BuildURI(attribute, c)

                Case ConstraintKind.Duration
                    BuildDuration(attribute, c)

                    'Case ConstraintType.Currency
                    '    BuildCurrency(attribute, c)

                Case ConstraintKind.Identifier
                    BuildIdentifier(attribute, c)

                Case ConstraintKind.Parsable
                    BuildParsable(attribute, c)

                Case Else
                    Debug.Assert(False, String.Format("{0} constraint type is not handled", c.ToString()))
            End Select
        End Sub

        Protected Sub BuildElementOrReference(ByVal Element As RmElement, ByRef RelNode As XMLParser.C_ATTRIBUTE, ByVal index As Integer)
            If Element.Type = StructureType.Reference Then
                Dim path As String = GetPathOfNode(Element.NodeId)

                If path <> "" Then
                    Dim refXmlRefNode As XMLParser.ARCHETYPE_INTERNAL_REF = mAomFactory.MakeArchetypeRef(RelNode, "ELEMENT", path)
                    refXmlRefNode.occurrences = MakeOccurrences(Element.Occurrences)
                Else
                    'the origin of the reference has not been added yet
                    Dim ref As ReferenceToResolve
                    ref.Element = Element
                    ref.Attribute = RelNode
                    ref.Index = index
                    ReferencesToResolve.Add(ref)
                End If
            Else
                Dim elementXmlObj As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(RelNode, _
                    ReferenceModel.RM_StructureName(StructureType.Element), Element.NodeId, MakeOccurrences(Element.Occurrences))

                If Element.HasNameConstraint Then
                    Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(elementXmlObj, "name", Element.Existence.XmlExistence)
                    BuildText(a, Element.NameConstraint)
                End If

                If Element.Constraint.Kind <> ConstraintKind.Any Then
                    Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(elementXmlObj, "value", Element.Existence.XmlExistence)
                    BuildElementConstraint(a, Element.Constraint)
                End If

                'Check for constraint on Flavours of Null
                If Element.HasNullFlavourConstraint() Then
                    Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(elementXmlObj, "null_flavour", New RmExistence(0, 1).XmlExistence)
                    BuildCodedTextInternal(a, Element.ConstrainedNullFlavours, "")
                End If
            End If
        End Sub

        Protected Sub ResolveReferences()
            For Each ref As ReferenceToResolve In ReferencesToResolve
                Dim path As String = GetPathOfNode(ref.Element.NodeId)

                If path <> "" Then
                    Dim refXmlRefNode As XMLParser.ARCHETYPE_INTERNAL_REF = mAomFactory.MakeArchetypeRef(ref.Attribute, "ELEMENT", path)

                    For i As Integer = ref.Attribute.children.Length - 2 To ref.Index Step -1
                        ref.Attribute.children(i + 1) = ref.Attribute.children(i)
                    Next

                    ref.Attribute.children(ref.Index) = refXmlRefNode
                    refXmlRefNode.occurrences = MakeOccurrences(ref.Element.Occurrences)
                Else
                    'reference element no longer exists so build it as an element
                    BuildElementOrReference(ref.Element.Copy(), ref.Attribute, ref.Index)
                End If
            Next

            ReferencesToResolve.Clear()
        End Sub

        Private Sub BuildStructure(ByVal rmStruct As RmStructureCompound, ByRef objNode As XMLParser.C_COMPLEX_OBJECT)
            Dim a As XMLParser.C_ATTRIBUTE
            Dim rm As RmStructure
            Dim index As Integer = 0

            ' preconditions
            Debug.Assert(rmStruct.NodeId <> "") ' anonymous

            ' now make sure there are some contents to the structure and if not set it to anyallowed
            If rmStruct.Children.Count > 0 Then
                Select Case rmStruct.Type
                    Case StructureType.Single
                        a = mAomFactory.MakeSingleAttribute(objNode, "item", rmStruct.Existence.XmlExistence)
                        rm = rmStruct.Children.Items(0)

                        If rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                            BuildElementOrReference(rm, a, 0)
                        ElseIf rm.Type = StructureType.Slot Then
                            BuildSlotFromRm(a, rm)
                        Else
                            Debug.Assert(False, "Type not handled")
                        End If
                    Case StructureType.List
                        a = mAomFactory.MakeMultipleAttribute(objNode, "items", _
                            MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered), CType(rmStruct, RmStructureCompound).Children.Existence.XmlExistence)

                        For Each rm In rmStruct.Children.Items
                            If rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                BuildElementOrReference(rm, a, index)
                                index += 1
                            ElseIf rm.Type = StructureType.Slot Then
                                BuildSlotFromRm(a, rm)
                                index += 1
                            Else
                                Debug.Assert(False, "Type not handled")
                            End If
                        Next
                    Case StructureType.Tree
                        a = mAomFactory.MakeMultipleAttribute(objNode, "items", _
                            MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered), CType(rmStruct, RmStructureCompound).Children.Existence.XmlExistence)

                        For Each rm In rmStruct.Children.Items
                            If rm.Type = StructureType.Cluster Then
                                BuildCluster(rm, a)
                                index += 1
                            ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                BuildElementOrReference(rm, a, index)
                                index += 1
                            ElseIf rm.Type = StructureType.Slot Then
                                BuildSlotFromRm(a, rm)
                                index += 1
                            Else
                                Debug.Assert(False, "Type not handled")
                            End If
                        Next
                    Case StructureType.Table
                        Dim table As RmTable = CType(rmStruct, RmTable)
                        Dim b As New XMLParser.C_BOOLEAN
                        b.assumed_valueSpecified = False
                        a = mAomFactory.MakeSingleAttribute(objNode, "rotated", rmStruct.Existence.XmlExistence)

                        b.true_valid = table.isRotated
                        b.false_valid = Not table.isRotated
                        mAomFactory.MakePrimitiveObject(a, b)

                        ' set number of row if not one
                        If table.NumberKeyColumns > 0 Then
                            Dim rh As New XMLParser.C_INTEGER
                            rh.range = New XMLParser.IntervalOfInteger
                            a = mAomFactory.MakeSingleAttribute(objNode, "number_key_columns", rmStruct.Existence.XmlExistence)

                            rh.range.lower = table.NumberKeyColumns
                            rh.range.lower_included = True
                            rh.range.lower_includedSpecified = True
                            rh.range.lowerSpecified = True

                            rh.range.upper_included = True
                            rh.range.upper_includedSpecified = True
                            rh.range.upper = table.NumberKeyColumns
                            rh.range.upperSpecified = True

                            ' Validate Interval PostConditions
                            With rh.range
                                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                            End With

                            mAomFactory.MakePrimitiveObject(a, rh)
                        End If

                        a = mAomFactory.MakeMultipleAttribute(objNode, "rows", MakeCardinality(New RmCardinality(rmStruct.Occurrences), True), rmStruct.Existence.XmlExistence)
                        BuildCluster(rmStruct.Children.Items(0), a)
                End Select
            End If

            ResolveReferences()
        End Sub

        Protected Sub BuildSubjectOfData(ByVal subject As RelatedParty, ByVal root_node As XMLParser.C_COMPLEX_OBJECT)
            Dim objnode As XMLParser.C_COMPLEX_OBJECT
            Dim a As XMLParser.C_ATTRIBUTE
            Dim a_relationship As XMLParser.C_ATTRIBUTE

            a = mAomFactory.MakeSingleAttribute(root_node, "subject", New RmExistence(1).XmlExistence)
            objnode = mAomFactory.MakeComplexObject(a, "PARTY_RELATED", "", MakeOccurrences(New RmCardinality(1, 1)))
            a_relationship = mAomFactory.MakeSingleAttribute(objnode, "relationship", New RmExistence(1).XmlExistence)
            BuildCodedTextInternal(a_relationship, subject.Relationship, "")
        End Sub

        Private Sub BuildSection(ByVal rmChildren As Children, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeMultipleAttribute(xmlObj, "items", _
                MakeCardinality(rmChildren.Cardinality, rmChildren.Cardinality.Ordered), rmChildren.Existence.XmlExistence)

            For Each a_structure As RmStructure In rmChildren
                If a_structure.Type = StructureType.SECTION Then
                    Dim new_section As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(a, "SECTION", a_structure.NodeId, MakeOccurrences(a_structure.Occurrences))

                    If a_structure.HasNameConstraint Then
                        a = mAomFactory.MakeSingleAttribute(new_section, "name", rmChildren.Existence.XmlExistence)
                        BuildText(a, a_structure.NameConstraint)
                    End If

                    If CType(a_structure, RmSection).Children.Count > 0 Then
                        BuildSection(CType(a_structure, RmSection).Children, new_section)
                    End If
                ElseIf a_structure.Type = StructureType.Slot Then
                    BuildSlotFromRm(a, a_structure)
                Else
                    Debug.Assert(False)
                End If
            Next
        End Sub

        Private Sub BuildComposition(ByVal Rm As RmComposition, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(xmlObj, "category", New RmExistence(1).XmlExistence)

            Dim t As New Constraint_Text
            t.TypeOfTextConstraint = TextConstraintType.Terminology ' coded_text
            t.AllowableValues.TerminologyID = "openehr"

            If Rm.IsPersistent Then
                t.AllowableValues.Codes.Add("431") ' persistent
            Else
                t.AllowableValues.Codes.Add("433") ' event
            End If

            BuildCodedTextInternal(a, t.AllowableValues, "")

            Dim eventContext As XMLParser.C_COMPLEX_OBJECT = Nothing

            If Rm.HasParticipations Then
                a = mAomFactory.MakeSingleAttribute(xmlObj, "context", New RmExistence(1).XmlExistence)
                eventContext = mAomFactory.MakeComplexObject(a, "EVENT_CONTEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
                BuildParticipations(eventContext, Rm.Participations)
            End If

            ' Deal with the content and context
            If Rm.Data.Count > 0 Then

                For Each a_structure As RmStructure In Rm.Data
                    Select Case a_structure.Type
                        Case StructureType.List, StructureType.Single, StructureType.Table, StructureType.Tree
                            Dim new_structure As XMLParser.C_COMPLEX_OBJECT

                            If eventContext Is Nothing Then
                                a = mAomFactory.MakeSingleAttribute(xmlObj, "context", New RmExistence(1).XmlExistence)
                                eventContext = mAomFactory.MakeComplexObject(a, "EVENT_CONTEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
                            End If

                            a = mAomFactory.MakeSingleAttribute(eventContext, "other_context", New RmExistence(1).XmlExistence)
                            new_structure = mAomFactory.MakeComplexObject(a, ReferenceModel.RM_StructureName(a_structure.Type), a_structure.NodeId, MakeOccurrences(New RmCardinality(1, 1)))
                            BuildStructure(a_structure, new_structure)
                        Case StructureType.Slot
                            If eventContext Is Nothing Then
                                a = mAomFactory.MakeSingleAttribute(xmlObj, "context", New RmExistence(1).XmlExistence)
                                eventContext = mAomFactory.MakeComplexObject(a, "EVENT_CONTEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
                            End If

                            a = mAomFactory.MakeSingleAttribute(eventContext, "other_context", New RmExistence(1).XmlExistence)
                            BuildSlotFromRm(a, a_structure)
                        Case StructureType.SECTION
                            If CType(a_structure, RmSection).Children.Count > 0 Then
                                a = mAomFactory.MakeMultipleAttribute(xmlObj, "content", MakeCardinality(Rm.Data.Cardinality), a_structure.Existence.XmlExistence)

                                For Each slot As RmSlot In CType(a_structure, RmSection).Children
                                    BuildSlotFromRm(a, slot)
                                Next
                            End If
                        Case Else
                            Debug.Assert(False)
                    End Select
                Next
            End If
        End Sub

        Protected Sub BuildRootElement(ByVal element As RmElement, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' N.B.: Writing the name constraint has already been done in the caller.

            If Not element.Constraint Is Nothing AndAlso element.Constraint.Kind <> ConstraintKind.Any Then
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(xmlObj, "value", element.Existence.XmlExistence)
                BuildElementConstraint(a, element.Constraint)
            End If
        End Sub

        Protected Sub BuildRootCluster(ByVal Cluster As RmCluster, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            If Cluster.Children.Count > 0 Then
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeMultipleAttribute(xmlObj, "items", MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered), Cluster.Children.Existence.XmlExistence)
                Dim index As Integer = 0

                For Each rm As RmStructure In Cluster.Children.Items
                    If rm.Type = StructureType.Cluster Then
                        BuildCluster(rm, a)
                        index += 1
                    ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                        BuildElementOrReference(rm, a, index)
                        index += 1
                    ElseIf rm.Type = StructureType.Slot Then
                        BuildSlotFromRm(a, rm)
                        index += 1
                    Else
                        Debug.Assert(False, "Type not handled")
                    End If
                Next
            End If

            ResolveReferences()
        End Sub

        Private Sub BuildRootSection(ByVal section As RmSection, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            If section.Children.Count > 0 Then
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeMultipleAttribute(xmlObj, "items", _
                    MakeCardinality(section.Children.Cardinality, section.Children.Cardinality.Ordered), section.Children.Existence.XmlExistence)

                For Each rm As RmStructure In section.Children
                    If rm.Type = StructureType.SECTION Then
                        Dim newSection As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject("SECTION", rm.NodeId, MakeOccurrences(rm.Occurrences))

                        If rm.HasNameConstraint Then
                            Dim anotherAttribute As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(newSection, "name", rm.Existence.XmlExistence)
                            BuildText(anotherAttribute, rm.NameConstraint)
                        End If

                        If CType(rm, RmSection).Children.Count > 0 Then
                            BuildSection(CType(rm, RmSection).Children, newSection)
                        End If

                        mAomFactory.add_object(a, newSection)
                    ElseIf rm.Type = StructureType.Slot Then
                        BuildSlotFromRm(a, rm)
                    End If
                Next
            End If
        End Sub

        Private Sub BuildStructure(ByVal rm As RmStructureCompound, ByVal an_adlArchetype As XMLParser.C_COMPLEX_OBJECT, ByVal attribute_name As String)
            Dim a As XMLParser.C_ATTRIBUTE

            If CType(rm.Children.Items(0), RmStructure).Type = StructureType.Slot Then
                a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, attribute_name, rm.Existence.XmlExistence)
                BuildSlotFromRm(a, rm.Children.Items(0))
            Else
                a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, attribute_name, rm.Children.Existence.XmlExistence)
                Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject( _
                    a, _
                    ReferenceModel.RM_StructureName(rm.Children.Items(0).Type), _
                    rm.Children.Items(0).NodeId, MakeOccurrences(New RmCardinality(1, 1)))

                BuildStructure(rm.Children.Items(0), objNode)
            End If
        End Sub

        Private Sub BuildProtocol(ByVal rm As RmStructure, ByVal an_adlArchetype As XMLParser.C_COMPLEX_OBJECT)
            Dim a As XMLParser.C_ATTRIBUTE
            Dim rmStructComp As RmStructureCompound

            If rm.Type = StructureType.Slot Then
                a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "protocol", rm.Existence.XmlExistence)
                BuildSlotFromRm(a, rm)
            Else
                rmStructComp = CType(rm, RmStructureCompound)
                If rmStructComp.Children.Count > 0 Then
                    a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "protocol", rmStructComp.Children.Existence.XmlExistence)
                    ' only 1 protocol allowed
                    Dim protocolRm As RmStructure = rmStructComp.Children.Items(0)

                    If protocolRm.Type = StructureType.Slot Then
                        BuildSlotFromRm(a, CType(protocolRm, RmSlot))
                    Else
                        Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject( _
                            a, _
                            ReferenceModel.RM_StructureName(rmStructComp.Children.Items(0).Type), _
                            rmStructComp.Children.Items(0).NodeId, MakeOccurrences(New RmCardinality(1, 1)))

                        BuildStructure(CType(protocolRm, RmStructureCompound), objNode)
                    End If
                End If
            End If
        End Sub

        Private Sub BuildWorkFlowStep(ByVal rm As RmPathwayStep, ByVal attribute As XMLParser.C_ATTRIBUTE)
            Dim a_state, a_step As XMLParser.C_ATTRIBUTE
            Dim objNode As XMLParser.C_COMPLEX_OBJECT
            Dim code_phrase As New CodePhrase

            objNode = mAomFactory.MakeComplexObject(attribute, "ISM_TRANSITION", rm.NodeId, MakeOccurrences(New RmCardinality(1, 1)))
            a_state = mAomFactory.MakeSingleAttribute(objNode, "current_state", attribute.existence)
            code_phrase.TerminologyID = "openehr"
            code_phrase.Codes.Add((CInt(rm.StateType)).ToString)

            If rm.HasAlternativeState Then
                code_phrase.Codes.Add(CInt(rm.AlternativeState).ToString)
            End If

            BuildCodedTextInternal(a_state, code_phrase, "")
            a_step = mAomFactory.MakeSingleAttribute(objNode, "careflow_step", attribute.existence)
            code_phrase = New CodePhrase
            code_phrase.Codes.Add(rm.NodeId)  ' local is default terminology, node_id of rm is same as term code of name
            BuildCodedTextInternal(a_step, code_phrase, "")
        End Sub

        Private Sub BuildPathway(ByVal rm As RmStructureCompound, ByVal arch_def As XMLParser.C_COMPLEX_OBJECT)
            If rm.Children.Count > 0 Then
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "ism_transition", rm.Existence.XmlExistence)

                For Each pathway_step As RmPathwayStep In rm.Children
                    BuildWorkFlowStep(pathway_step, a)
                Next
            End If
        End Sub

        Private Sub BuildActivity(ByVal rm As RmActivity, ByVal attribute As XMLParser.C_ATTRIBUTE)
            Dim o As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, "ACTIVITY", rm.NodeId, MakeOccurrences(rm.Occurrences))
            Dim escapedString As String = rm.ArchetypeId

            If escapedString <> "" Then
                Dim i As Integer = escapedString.IndexOf("\")

                'Must have at least one escaped . or it is not valid unless it is the end
                If i < 0 Or i = escapedString.Length - 1 Then
                    escapedString = escapedString.Replace(".", "\.")
                End If

                escapedString = ReferenceModel.ReferenceModelName & "-ACTION\." + escapedString
                attribute = mAomFactory.MakeSingleAttribute(o, "action_archetype_id", rm.Children.Existence.XmlExistence)
                Dim c_s As New XMLParser.C_STRING
                c_s.pattern = escapedString
                mAomFactory.MakePrimitiveObject(attribute, c_s)
            End If

            For Each rm_struct As RmStructure In rm.Children
                attribute = mAomFactory.MakeSingleAttribute(o, "description", rm.Children.Existence.XmlExistence)

                Select Case rm_struct.Type
                    Case StructureType.List, StructureType.Single, StructureType.Tree, StructureType.Table
                        Dim EIF_struct As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(attribute, _
                            ReferenceModel.RM_StructureName(rm_struct.Type), _
                            rm_struct.NodeId, MakeOccurrences(New RmCardinality(1, 1)))

                        BuildStructure(CType(rm_struct, RmStructureCompound), EIF_struct)

                    Case StructureType.Slot
                        ' this allows a structure to be archetyped at this point
                        Debug.Assert(CType(rm_struct, RmStructure).Type = StructureType.Slot)
                        BuildSlotFromRm(attribute, rm_struct)
                End Select
            Next
        End Sub

        Private Sub BuildInstruction(ByVal data As RmChildren)
            For Each rm As RmStructureCompound In data
                Select Case rm.Type
                    Case StructureType.Activities

                        'ToDo: Set cardinality on this attribute
                        Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeMultipleAttribute(mXmlArchetype.definition, _
                            "activities", MakeCardinality(New RmCardinality(0)), _
                            New RmExistence(1).XmlExistence)

                        For Each activity As RmActivity In rm.Children
                            BuildActivity(activity, a)
                        Next
                    Case StructureType.Protocol
                        BuildProtocol(rm, mXmlArchetype.definition)
                    Case Else
                        Debug.Assert(False, rm.Type.ToString() & " - Type under INSTRUCTION not handled")
                End Select
            Next
        End Sub

        Private Sub BuildAction(ByVal rm As RmStructureCompound, ByVal a_definition As XMLParser.C_COMPLEX_OBJECT)
            Dim action_spec As RmStructure
            Dim a As XMLParser.C_ATTRIBUTE
            Dim o As XMLParser.C_COMPLEX_OBJECT

            If rm.Children.Items.Length > 0 Then
                a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "description", rm.Existence.XmlExistence)
                action_spec = rm.Children.Items(0)

                Select Case action_spec.Type
                    Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                        o = mAomFactory.MakeComplexObject(a, _
                            ReferenceModel.RM_StructureName(action_spec.Type), _
                            rm.Children.Items(0).NodeId, MakeOccurrences(New RmCardinality(1, 1)))

                        BuildStructure(action_spec, o)

                    Case StructureType.Slot
                        ' allows action to be specified in another archetype
                        BuildSlotFromRm(a, CType(action_spec, RmSlot))
                End Select
            End If
        End Sub

        Public Overridable Sub MakeParseTree()
            If Not mSynchronised Then
                Dim rm As RmStructureCompound
                Dim a As XMLParser.C_ATTRIBUTE

                'reset the XML definition to make it again
                mXmlArchetype.definition.attributes = Nothing
                mXmlArchetype.definition.occurrences = MakeOccurrences(New RmCardinality(1, 1))

                'pick up the description data
                If TypeOf mDescription Is ADL_Classes.ADL_Description Then
                    mXmlArchetype.description = New XML_Description(CType(mDescription, ADL_Classes.ADL_Description)).XML_Description
                Else
                    mXmlArchetype.description = (CType(mDescription, XML_Description).XML_Description)
                End If

                If Not mTranslationDetails Is Nothing AndAlso mTranslationDetails.Count > 0 Then
                    Dim xmlTranslationDetails As XMLParser.TRANSLATION_DETAILS() = Array.CreateInstance(GetType(XMLParser.TRANSLATION_DETAILS), mTranslationDetails.Count)

                    If TypeOf mTranslationDetails.Values(0) Is ADL_TranslationDetails Then
                        'Need to convert to XML
                        For i As Integer = 0 To mTranslationDetails.Count - 1
                            xmlTranslationDetails(i) = New XML_TranslationDetails(mTranslationDetails.Values(i)).XmlTranslation
                        Next
                    Else
                        For i As Integer = 0 To mTranslationDetails.Count - 1
                            xmlTranslationDetails(i) = CType(mTranslationDetails.Values(i), XML_TranslationDetails).XmlTranslation
                        Next
                    End If

                    mXmlArchetype.translations = xmlTranslationDetails
                End If

                If Not cDefinition Is Nothing Then
                    mAomFactory = New XMLParser.AomFactory()

                    If cDefinition.HasNameConstraint Then
                        a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "name", New RmExistence(1).XmlExistence)
                        BuildText(a, cDefinition.NameConstraint)
                    End If

                    Select Case cDefinition.Type

                        Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                            If New C_COMPLEX_OBJECT_PROXY(mXmlArchetype.definition).Any_Allowed AndAlso CType(cDefinition, ArchetypeDefinition).Data.Count > 0 Then
                                'This can arise if the archetype has been saved with no children then
                                'items have been added later - this is percular to Tree, List and Table.
                                mXmlArchetype.definition.occurrences = MakeOccurrences(New RmCardinality(0))
                            End If

                            BuildStructure(cDefinition, mXmlArchetype.definition)

                        Case StructureType.Cluster
                            BuildRootCluster(cDefinition, mXmlArchetype.definition)

                        Case StructureType.Element
                            BuildRootElement(cDefinition, mXmlArchetype.definition)

                        Case StructureType.SECTION
                            BuildRootSection(cDefinition, mXmlArchetype.definition)

                        Case StructureType.COMPOSITION
                            BuildComposition(cDefinition, mXmlArchetype.definition)

                        Case StructureType.EVALUATION, StructureType.ENTRY
                            BuildEntryAttributes(CType(cDefinition, RmEntry), mXmlArchetype.definition)

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.State
                                        BuildStructure(rm, mXmlArchetype.definition, "state")

                                    Case StructureType.Protocol
                                        BuildProtocol(rm, mXmlArchetype.definition)

                                    Case StructureType.Data
                                        BuildStructure(rm, mXmlArchetype.definition, "data")
                                End Select
                            Next

                        Case StructureType.ADMIN_ENTRY
                            BuildEntryAttributes(CType(cDefinition, RmEntry), mXmlArchetype.definition)
                            a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "data", New RmExistence(1).XmlExistence)
                            Dim items() As RmStructure = CType(cDefinition, ArchetypeDefinition).Data.Items

                            If items.Length > 0 Then
                                Dim struct As RmStructureCompound = CType(items(0), RmStructureCompound)

                                If struct.Children.Items.Length > 0 Then
                                    struct = struct.Children.Items(0)
                                    BuildStructure(struct, mAomFactory.MakeComplexObject(a, ReferenceModel.RM_StructureName(struct.Type), struct.NodeId, MakeOccurrences(New RmCardinality(1, 1))))
                                End If
                            End If

                        Case StructureType.OBSERVATION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), mXmlArchetype.definition)

                            'Add state to each event so need to be sure of requirements
                            Dim state_to_be_added As Boolean = True
                            Dim rm_state As RmStructureCompound = Nothing
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

                                        If rm.Children.Items.Length > 0 Then
                                            a_rm = rm.Children.Items(0)
                                        End If

                                        If a_rm Is Nothing OrElse a_rm.Type <> StructureType.History Then
                                            rm_state = rm
                                        Else
                                            a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "state", a_rm.Existence.XmlExistence)

                                            ' can have EventSeries for each state
                                            BuildHistory(a_rm, a)
                                        End If
                                End Select
                            Next

                            'Add the data
                            If Not rm_data Is Nothing Then
                                a = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "data", rm_data.Existence.XmlExistence)

                                For Each a_rm As RmStructureCompound In rm_data.Children.Items
                                    Select Case a_rm.Type
                                        Case StructureType.History
                                            If Not rm_state Is Nothing Then
                                                BuildHistory(a_rm, a, rm_state)
                                            Else
                                                BuildHistory(a_rm, a)
                                            End If
                                        Case Else
                                            Debug.Assert(False) '?OBSOLETE
                                            Dim objNode As XMLParser.C_COMPLEX_OBJECT
                                            objNode = mAomFactory.MakeComplexObject(a, ReferenceModel.RM_StructureName(a_rm.Type), a_rm.NodeId, MakeOccurrences(New RmCardinality(1, 1)))
                                            BuildStructure(a_rm, objNode)
                                    End Select
                                Next
                            End If

                            If Not rm_protocol Is Nothing Then
                                BuildProtocol(rm_protocol, mXmlArchetype.definition)
                            End If

                        Case StructureType.INSTRUCTION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), mXmlArchetype.definition)
                            BuildInstruction(CType(cDefinition, ArchetypeDefinition).Data)

                        Case StructureType.ACTION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), mXmlArchetype.definition)

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.ISM_TRANSITION
                                        BuildPathway(rm, mXmlArchetype.definition)
                                    Case StructureType.ActivityDescription
                                        BuildAction(rm, mXmlArchetype.definition)
                                    Case StructureType.Slot
                                        ' this allows a structure to be archetyped at this point
                                        Debug.Assert(CType(rm.Children.Items(0), RmStructure).Type = StructureType.Slot)
                                        BuildStructure(rm, mXmlArchetype.definition)
                                    Case StructureType.Protocol
                                        BuildProtocol(rm, mXmlArchetype.definition)
                                End Select
                            Next
                    End Select

                    If HasLinkConstraints() Then
                        BuildLinks(Definition.RootLinks, mXmlArchetype.definition)
                    End If

                    mSynchronised = True
                End If
            End If
        End Sub

        Sub BuildLinks(ByVal cLinks As System.Collections.Generic.List(Of RmLink), ByVal cObject As XMLParser.C_COMPLEX_OBJECT)
            Dim linksAttribute As XMLParser.C_ATTRIBUTE = mAomFactory.MakeMultipleAttribute(cObject, "links", MakeCardinality(New RmCardinality(0)), New RmExistence(0).XmlExistence)
            Dim a As XMLParser.C_ATTRIBUTE

            For Each l As RmLink In cLinks
                If l.HasConstraint Then
                    Dim linkObject As XMLParser.C_COMPLEX_OBJECT = _
                        mAomFactory.MakeComplexObject(linksAttribute, "LINK")

                    If l.Meaning.TypeOfTextConstraint <> TextConstraintType.Text Then
                        a = mAomFactory.MakeSingleAttribute(linkObject, "meaning", New RmExistence().XmlExistence)
                        BuildText(a, l.Meaning)
                    End If

                    If l.LinkType.TypeOfTextConstraint <> TextConstraintType.Text Then
                        a = mAomFactory.MakeSingleAttribute(linkObject, "type", New RmExistence().XmlExistence)
                        BuildText(a, l.LinkType)
                    End If

                    If l.Target.RegularExpression <> String.Empty Then
                        a = mAomFactory.MakeSingleAttribute(linkObject, "target".ToLowerInvariant, New RmExistence().XmlExistence)
                        BuildURI(a, l.Target)
                    End If
                End If
            Next
        End Sub

        Sub BuildEntryAttributes(ByVal anEntry As RmEntry, ByVal archetypeDefinition As XMLParser.C_COMPLEX_OBJECT)
            If anEntry.SubjectOfData.Relationship.Codes.Count > 0 Then
                BuildSubjectOfData(anEntry.SubjectOfData, archetypeDefinition)
            End If

            If CType(cDefinition, RmEntry).ProviderIsMandatory Then
                Dim o As XMLParser.C_COMPLEX_OBJECT
                Dim a As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(archetypeDefinition, "provider", New RmExistence(1).XmlExistence)
                o = mAomFactory.MakeComplexObject(a, "PARTY_PROXY", "", MakeOccurrences(New RmCardinality(1, 1)))
            End If

            BuildParticipations(mXmlArchetype.definition, CType(cDefinition, RmEntry).OtherParticipations)
        End Sub

        Protected Sub BuildParticipations(ByVal aRmClass As XMLParser.C_COMPLEX_OBJECT, ByVal participations As RmStructureCompound)
            If participations.Children.Count > 0 Then
                Dim participationAttribute As XMLParser.C_ATTRIBUTE
                participationAttribute = mAomFactory.MakeMultipleAttribute(aRmClass, participations.NodeId, MakeCardinality(participations.Children.Cardinality), New RmExistence(1).XmlExistence)

                For Each p As RmParticipation In participations.Children
                    BuildParticipation(participationAttribute, p)
                Next
            End If
        End Sub

        Protected Sub BuildParticipation(ByVal attribute As XMLParser.C_ATTRIBUTE, ByVal participation As RmParticipation)
            Dim cObject As XMLParser.C_COMPLEX_OBJECT
            cObject = mAomFactory.MakeComplexObject(attribute, "PARTICIPATION", "", MakeOccurrences(participation.Occurrences))

            If participation.MandatoryDateTime Then
                Dim timeAttrib As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(cObject, "time", New RmExistence(1).XmlExistence)
            End If

            If participation.ModeSet.Codes.Count > 0 Then
                BuildCodedTextInternal(mAomFactory.MakeSingleAttribute(cObject, "mode", New RmExistence(1).XmlExistence), participation.ModeSet, "")
            End If

            If participation.FunctionConstraint.TypeOfTextConstraint <> TextConstraintType.Text Then
                Dim constraintAttribute As XMLParser.C_ATTRIBUTE
                constraintAttribute = mAomFactory.MakeSingleAttribute(cObject, "function", New RmExistence(1).XmlExistence)

                If participation.FunctionConstraint.TypeOfTextConstraint = TextConstraintType.Internal Then
                    BuildCodedTextInternal(constraintAttribute, participation.FunctionConstraint.AllowableValues, "")
                Else
                    BuildCodedText(constraintAttribute, participation.FunctionConstraint.ConstraintCode)
                End If
            End If
        End Sub

        Sub New(ByVal an_XML_Parser As XMLParser.XmlArchetypeParser, ByVal an_ArchetypeID As ArchetypeID, ByVal primary_language As String)
            ' call to create a brand new archetype
            MyBase.New(primary_language, an_ArchetypeID)

            mArchetypeParser = an_XML_Parser
            ' make the new archetype

            Try
                mArchetypeParser.NewArchetype(an_ArchetypeID.ToString, sPrimaryLanguageCode, Main.Instance.DefaultLanguageCodeSet)
                mXmlArchetype = mArchetypeParser.Archetype
                mDescription = New XML_Description(mXmlArchetype.description, primary_language)
            Catch
                Debug.Assert(False)
                ''FIXME raise error
            End Try
        End Sub

        Sub New(ByVal a_parser As XMLParser.XmlArchetypeParser)
            ' Used in Export or SaveAs only
            MyBase.New(a_parser.Archetype.original_language.code_string)

            mXmlArchetype = a_parser.Archetype
            mArchetypeParser = a_parser

            If Not mXmlArchetype.archetype_id Is Nothing Then
                mArchetypeID = New ArchetypeID(mXmlArchetype.archetype_id.value)
            End If

            ' get the parent ID
            If Not mXmlArchetype.parent_archetype_id Is Nothing Then
                sParentArchetypeID = mXmlArchetype.parent_archetype_id.value
            End If

            'this is the one
            mDescription = New XML_Description(mXmlArchetype.description, a_parser.Archetype.original_language.code_string)

            If Not mXmlArchetype.translations Is Nothing Then
                For Each t As XMLParser.TRANSLATION_DETAILS In mXmlArchetype.translations
                    mTranslationDetails.Add(t.language.code_string, New XML_TranslationDetails(t))
                Next
            End If

            Select Case mArchetypeID.ReferenceModelEntity
                Case StructureType.COMPOSITION
                    cDefinition = New RmComposition()
                    cDefinition.RootNodeId = mXmlArchetype.concept
                Case StructureType.SECTION
                    cDefinition = New RmSection(mXmlArchetype.concept)
                Case StructureType.List, StructureType.Tree, StructureType.Single
                    cDefinition = New RmStructureCompound(mXmlArchetype.concept, mArchetypeID.ReferenceModelEntity)
                Case StructureType.Table
                    cDefinition = New RmTable(mXmlArchetype.concept)
                Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION, StructureType.ADMIN_ENTRY, StructureType.ACTION
                    cDefinition = New RmEntry(mArchetypeID.ReferenceModelEntity)
                    cDefinition.RootNodeId = mXmlArchetype.concept
                Case StructureType.Cluster
                    cDefinition = New RmCluster(mXmlArchetype.concept)
                Case StructureType.Element
                    cDefinition = New RmElement(mXmlArchetype.concept)
                Case Else
                    Debug.Assert(False)
            End Select

            sLifeCycle = mXmlArchetype.description.lifecycle_state
        End Sub

        Sub New(ByVal a_parser As XMLParser.XmlArchetypeParser, ByVal a_filemanager As FileManagerLocal)

            ' call to create an in memory archetype from the XML parser
            MyBase.New(a_parser.Archetype.original_language.code_string)

            Try
                mXmlArchetype = a_parser.Archetype
                mArchetypeParser = a_parser

                If Not mXmlArchetype.archetype_id Is Nothing Then
                    mArchetypeID = New ArchetypeID(mXmlArchetype.archetype_id.value)
                End If

                If Not mXmlArchetype.parent_archetype_id Is Nothing Then
                    sParentArchetypeID = mXmlArchetype.parent_archetype_id.value
                End If

                ReferenceModel.SetArchetypedClass(mArchetypeID.ReferenceModelEntity)

                mDescription = New XML_Description(mXmlArchetype.description, a_parser.Archetype.original_language.code_string)

                If Not mXmlArchetype.translations Is Nothing Then
                    For Each t As XMLParser.TRANSLATION_DETAILS In mXmlArchetype.translations
                        mTranslationDetails.Add(t.language.code_string, New XML_TranslationDetails(t))
                    Next
                End If

                Select Case mArchetypeID.ReferenceModelEntity
                    Case StructureType.COMPOSITION
                        cDefinition = New XML_COMPOSITION(mXmlArchetype.definition, a_filemanager)
                    Case StructureType.SECTION
                        cDefinition = New XML_SECTION(mXmlArchetype.definition, a_filemanager)
                    Case StructureType.List, StructureType.Tree, StructureType.Single
                        cDefinition = New RmStructureCompound(mXmlArchetype.definition, a_filemanager)
                    Case StructureType.Table
                        cDefinition = New RmTable(mXmlArchetype.definition, a_filemanager)
                    Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION, StructureType.ADMIN_ENTRY, StructureType.ACTION
                        cDefinition = New XML_ENTRY(mXmlArchetype.definition, a_filemanager)
                    Case StructureType.Cluster
                        cDefinition = New RmCluster(mXmlArchetype.definition, a_filemanager)
                    Case StructureType.Element
                        cDefinition = New XML_RmElement(mXmlArchetype.definition, a_filemanager)
                    Case Else
                        Debug.Assert(False)
                End Select

                If Not mXmlArchetype.definition.attributes Is Nothing Then
                    For Each attrib As XMLParser.C_ATTRIBUTE In mXmlArchetype.definition.attributes
                        If attrib.rm_attribute_name.ToLowerInvariant = "links" Then
                            For Each complexObject As XMLParser.C_COMPLEX_OBJECT In attrib.children
                                Dim link As New RmLink
                                For Each linkAttribute As XMLParser.C_ATTRIBUTE In complexObject.attributes
                                    Select Case linkAttribute.rm_attribute_name.ToLowerInvariant
                                        Case "meaning"
                                            If linkAttribute.children.Length > 0 Then
                                                link.Meaning = XML_RmElement.ProcessText(linkAttribute.children(0))
                                            End If
                                        Case "type"
                                            If linkAttribute.children.Length > 0 Then
                                                link.LinkType = XML_RmElement.ProcessText(linkAttribute.children(0))
                                            End If
                                        Case "target"
                                            Dim aURI As XMLParser.C_COMPLEX_OBJECT
                                            aURI = CType(linkAttribute.children(0), XMLParser.C_COMPLEX_OBJECT)
                                            If aURI.rm_type_name.ToLowerInvariant = "dv_ehr_uri" Then
                                                link.Target = CType(XML_RmElement.ProcessUri(aURI), Constraint_URI)
                                            Else
                                                Debug.Assert(False, String.Format("Attribute '{0}' not handled", aURI.rm_type_name))
                                            End If
                                    End Select
                                Next

                                cDefinition.RootLinks.Add(link)
                            Next
                        End If
                    Next
                End If

                sLifeCycle = mXmlArchetype.description.lifecycle_state
            Catch ex As Exception
                Debug.Assert(True)
            End Try
        End Sub

        Protected Sub New(ByVal primary_language As String)
            MyBase.New(primary_language)
            mDescription = New XML_Description(mXmlArchetype.description, primary_language)
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
'The Original Code is XML_Archetype.vb.
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
