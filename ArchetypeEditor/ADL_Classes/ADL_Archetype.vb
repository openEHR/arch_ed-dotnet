'
'
'	component:   "openEHR Archetype Project"
'	description: "Builds all ADL Archetypes"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Option Explicit On
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel
Imports EiffelList = EiffelSoftware.Library.Base.structures.list
Imports AM = XMLParser.OpenEhr.V1.Its.Xml.AM
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes

    Public Class ADL_Archetype
        Inherits Archetype

        'Builds all archetypes at present

        Protected adlArchetype As openehr.openehr.am.archetype.ARCHETYPE
        Protected adlEngine As openehr.adl_parser.syntax.adl.ADL_ENGINE
        Protected mAomFactory As openehr.openehr.am.archetype.constraint_model.CONSTRAINT_MODEL_FACTORY

        Protected Structure ReferenceToResolve
            Dim Element As RmElement
            Dim Attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim Index As Integer
        End Structure

        Protected ReferencesToResolve As ArrayList = New ArrayList

        Public Overrides Property ConceptCode() As String
            Get
                Return adlArchetype.concept.to_cil
            End Get
            Set(ByVal Value As String)
                adlArchetype.set_concept(Eiffel.String(Value))
                adlArchetype.definition.set_object_id(adlArchetype.concept)

                System.Diagnostics.Debug.Assert(ConceptCode = Value)
                System.Diagnostics.Debug.Assert(adlEngine.archetype.concept.to_cil = Value)
                System.Diagnostics.Debug.Assert(adlArchetype.definition.node_id.to_cil = Value)
            End Set
        End Property

        Public Overrides Property Uid() As String
            Get
                Dim result As String = Nothing

                If Not adlArchetype.uid Is Nothing Then
                    result = adlArchetype.uid.ToString
                End If

                Return result
            End Get
            Set(ByVal Value As String)
                If Not String.IsNullOrEmpty(Value) Then
                    adlArchetype.set_uid(Eiffel.String(Value))
                End If
            End Set
        End Property

        Public Overrides ReadOnly Property ArchetypeAvailable() As Boolean
            Get
                Return adlEngine.archetype_available
            End Get
        End Property

        Public Overrides ReadOnly Property Archetype_ID() As ArchetypeID
            Get
                Try
                    Return mArchetypeID
                Catch
                    Debug.Assert(False)
                    Return Nothing
                End Try
            End Get
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
                Return adlArchetype.parent_archetype_id.as_string.to_cil
            End Get
            Set(ByVal Value As String)
                Dim archId As openehr.openehr.rm.support.identification.ARCHETYPE_ID
                archId = openehr.openehr.rm.support.identification.Create.ARCHETYPE_ID.make_from_string(Eiffel.String(Value))
                adlArchetype.set_parent_archetype_id(archId)
            End Set
        End Property

        Public Overrides ReadOnly Property SerialisedArchetype() As String
            Get
                MakeParseTree()

                Try
                    RemoveUnusedCodes()
                    SetArchetypeDigest()
                    adlEngine.serialise(Eiffel.String("adl"))
                    Return adlEngine.serialised_archetype.to_cil
                Catch e As System.Reflection.TargetInvocationException
                    If Not e.InnerException Is Nothing Then
                        MessageBox.Show(e.InnerException.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    Return AE_Constants.Instance.ErrorSaving
                Catch e As Exception
                    Dim errorMessage As String = "Error serialising archetype." & vbCrLf & vbCrLf & e.Message

                    If Not e.InnerException Is Nothing Then
                        errorMessage &= vbCrLf & e.InnerException.Message
                    End If

                    MessageBox.Show(errorMessage, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return AE_Constants.Instance.ErrorSaving
                End Try
            End Get
        End Property

        Public Overrides ReadOnly Property Paths(ByVal LanguageCode As String, ByVal parserIsSynchronised As Boolean, Optional ByVal Logical As Boolean = False) As String()
            Get
                Dim list As EiffelList.ARRAYED_LIST_REFERENCE
                Dim i As Integer

                ' must call the prepareToSave to ensure it is accurate
                If Not Filemanager.Master.FileLoading AndAlso Not parserIsSynchronised Then
                    MakeParseTree()
                End If

                ' showing the task with logical paths takes a lot of space
                If Logical Then
                    list = adlArchetype.logical_paths(Eiffel.String(LanguageCode))
                Else
                    list = adlArchetype.physical_paths()
                End If

                Dim s(list.upper - 1) As String

                For i = list.lower() To list.upper()
                    s(i - 1) = CType(list.i_th(i), EiffelKernel.string.STRING_8).to_cil()
                Next

                Return s
            End Get
        End Property

        Public Overrides Sub Specialise(ByVal shortConceptName As String, ByRef ontology As OntologyManager)
            adlEngine.specialise_archetype(Eiffel.String(shortConceptName))
            ' Update the GUI tables with the new term
            ontology.UpdateTerm(New ADL_Term(adlEngine.ontology.term_definition(Eiffel.String(ontology.LanguageCode), adlArchetype.concept)))
            mArchetypeID.Concept &= "-" & shortConceptName
        End Sub

        Public Sub RemoveUnusedCodes()
            adlArchetype.remove_ontology_unused_codes()
        End Sub

        Protected Sub SetArchetypeId(ByVal an_archetype_id As ArchetypeID)
            Dim id As openehr.openehr.rm.support.identification.ARCHETYPE_ID
            id = openehr.openehr.rm.support.identification.Create.ARCHETYPE_ID.make_from_string(Eiffel.String(an_archetype_id.ToString))

            Try
                If Not adlEngine.archetype_available Then
                    adlEngine.create_new_archetype(id.rm_originator, id.rm_name, id.rm_entity, Eiffel.String(sPrimaryLanguageCode))
                    adlArchetype = adlEngine.archetype
                    adlArchetype.definition.set_object_id(adlArchetype.concept)
                    setDefinition()
                Else
                    ' does this involve a change in the entity (affects the GUI a great deal!)
                    If Not id.rm_entity.is_equal(adlArchetype.archetype_id.rm_entity) Then
                        Debug.Assert(False, "RM entity " & id.rm_entity.to_cil & " does not match " & adlArchetype.archetype_id.rm_entity.to_cil & ": changing the RM entity is not yet implemented.")
                        ' will need to reset the GUI to the new entity
                        setDefinition()
                    End If
                End If

                adlArchetype.set_archetype_id(id)
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

        Protected Function MakeAssertion(ByVal id As String, ByVal expression As String) As openehr.openehr.am.archetype.assertion.ASSERTION
            Dim id_expression_leaf, id_pattern_expression_leaf As openehr.openehr.am.archetype.assertion.EXPR_LEAF
            Dim match_operator As openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR

            Debug.Assert((Not id Is Nothing) And (id <> ""))
            id_expression_leaf = mAomFactory.create_expr_leaf_archetype_ref(Eiffel.String(id))
            id_pattern_expression_leaf = mAomFactory.create_expr_leaf_constraint(mAomFactory.create_c_string_make_from_regexp(Eiffel.String(expression)))

            match_operator = mAomFactory.create_expr_binary_operator_node(openehr.openehr.am.archetype.assertion.Create.OPERATOR_KIND.make_from_string(Eiffel.String("matches")), id_expression_leaf, id_pattern_expression_leaf)

            Return mAomFactory.create_assertion(match_operator, Nothing)
        End Function

        Protected Function MakeCardinality(ByVal c As RmCardinality, Optional ByVal IsOrdered As Boolean = True) As openehr.openehr.am.archetype.constraint_model.CARDINALITY
            Dim result As openehr.openehr.am.archetype.constraint_model.CARDINALITY

            If c.IsUnbounded Then
                result = mAomFactory.create_cardinality_make_upper_unbounded(c.MinCount)
            Else
                result = mAomFactory.create_cardinality_make_bounded(c.MinCount, c.MaxCount)
            End If

            If Not c.Ordered Then
                result.set_unordered()
            End If

            Return result
        End Function

        Protected Function MakeOccurrences(ByVal c As RmCardinality) As openehr.common_libs.basic.INTERVAL_INTEGER_32
            If c.IsUnbounded Then
                Return mAomFactory.create_c_integer_make_upper_unbounded(c.MinCount, c.IncludeLower).interval
            Else
                Return mAomFactory.create_c_integer_make_bounded(c.MinCount, c.MaxCount, c.IncludeLower, c.IncludeUpper).interval
            End If
        End Function

        Protected Function MakeExistence(ByVal e As RmExistence) As openehr.common_libs.basic.INTERVAL_INTEGER_32
            Return mAomFactory.create_c_integer_make_bounded(e.MinCount, e.MaxCount, True, True).interval
        End Function

        Protected Sub BuildCodedText(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal constraintId As String)
            Dim codedText As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim code As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim term As openehr.openehr.am.archetype.constraint_model.CONSTRAINT_REF

            codedText = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_CODED_TEXT"))
            code = mAomFactory.create_c_attribute_single(codedText, Eiffel.String("defining_code"))
            term = openehr.openehr.am.archetype.constraint_model.Create.CONSTRAINT_REF.make(Eiffel.String(constraintId))
            code.put_child(term)
        End Sub

        Protected Sub BuildCodedTextInternal(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal codePhrase As CodePhrase, ByVal assumedValue As String)
            Dim codedText As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim code As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim term As openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE

            codedText = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_CODED_TEXT"))
            code = mAomFactory.create_c_attribute_single(codedText, Eiffel.String("defining_code"))

            If codePhrase.Codes.Count > 0 Then
                term = mAomFactory.create_c_code_phrase_from_pattern(code, Eiffel.String(codePhrase.Phrase))

                If assumedValue <> "" Then
                    term.set_assumed_value(openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string(Eiffel.String("local::" & assumedValue)))
                End If
            Else
                term = openehr.openehr.am.openehr_profile.data_types.text.Create.C_CODE_PHRASE.make_from_terminology_id(Eiffel.String(codePhrase.TerminologyID))
                code.put_child(term)
            End If
        End Sub

        ''SRH: Stopped building plain text constraints in archetypes
        'HKF: reinstated to be able to read old archetypes
        Protected Sub BuildPlainText(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal TermList As Collections.Specialized.StringCollection)
            Dim plain_text As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim value_rel_node As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
            Dim cadlSimple As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

            plain_text = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_TEXT"))

            If TermList.Count > 0 Then
                Dim i As Integer
                value_rel_node = mAomFactory.create_c_attribute_single(plain_text, Eiffel.String("value"))
                cString = mAomFactory.create_c_string_make_from_string(Eiffel.String(TermList.Item(0)))

                For i = 1 To TermList.Count - 1
                    cString.add_string(Eiffel.String(TermList.Item(i)))
                Next

                cadlSimple = mAomFactory.create_c_primitive_object(value_rel_node, cString)
            End If
        End Sub

        Private Sub DuplicateHistory(ByVal rm As RmStructureCompound, ByRef RelNode As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Dim cadlHistory, cadlEvent As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim an_event As RmEvent
            Dim rm_1 As RmStructureCompound
            Dim a_history As RmHistory

            For Each rm_1 In CType(cDefinition, ArchetypeDefinition).Data
                If rm_1.Type = StructureType.History Then
                    a_history = CType(rm_1, RmHistory)
                    cadlHistory = mAomFactory.create_c_complex_object_identified(RelNode, Eiffel.String(ReferenceModel.RM_StructureName(StructureType.History)), Eiffel.String(a_history.NodeId))
                    cadlHistory.set_occurrences(MakeOccurrences(a_history.Occurrences))
                    If Not a_history.HasNameConstraint Then
                        a = mAomFactory.create_c_attribute_single(cadlHistory, Eiffel.String("name"))
                        BuildText(a, a_history.NameConstraint)
                    End If
                    If a_history.isPeriodic Then
                        Dim period As New Constraint_Duration

                        a = mAomFactory.create_c_attribute_single(cadlHistory, Eiffel.String("period"))
                        period.MinMaxValueUnits = a_history.PeriodUnits
                        'Set max and min to offset value
                        period.MinimumValue = a_history.Period
                        period.HasMinimum = True
                        period.MaximumValue = a_history.Period
                        period.HasMaximum = True
                        BuildDuration(a, period)
                    End If

                    ' now build the events
                    If a_history.Children.Count > 0 Then
                        a = mAomFactory.create_c_attribute_multiple(cadlHistory, Eiffel.String("events"), MakeCardinality(a_history.Children.Cardinality))
                        an_event = a_history.Children.Item(0)
                        cadlEvent = mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(StructureType.Event)), Eiffel.String(an_event.NodeId))
                        cadlEvent.set_occurrences(MakeOccurrences(an_event.Occurrences))
                        Select Case an_event.EventType
                            Case RmEvent.ObservationEventType.PointInTime
                                If an_event.hasFixedOffset Then
                                    Dim offset As New Constraint_Duration

                                    a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("offset"))
                                    offset.MinMaxValueUnits = an_event.OffsetUnits
                                    'Set max and min to offset value
                                    offset.MinimumValue = an_event.Offset
                                    offset.HasMinimum = True
                                    offset.MaximumValue = an_event.Offset
                                    offset.HasMaximum = True
                                    BuildDuration(a, offset)
                                End If
                            Case RmEvent.ObservationEventType.Interval
                                If Not an_event.AggregateMathFunction Is Nothing AndAlso an_event.AggregateMathFunction.Codes.Count > 0 Then
                                    a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("math_function"))
                                    BuildCodedTextInternal(a, an_event.AggregateMathFunction, "")
                                End If

                                If an_event.hasFixedDuration Then
                                    Dim fixedDuration As New Constraint_Duration

                                    a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("width"))
                                    fixedDuration.MinMaxValueUnits = an_event.WidthUnits
                                    'Set max and min to offset value
                                    fixedDuration.MinimumValue = an_event.Width
                                    fixedDuration.HasMinimum = True
                                    fixedDuration.MaximumValue = an_event.Width
                                    fixedDuration.HasMaximum = True
                                    BuildDuration(a, fixedDuration)
                                End If
                        End Select

                        ' runtime name
                        If an_event.HasNameConstraint Then
                            a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("name"))
                            BuildText(a, an_event.NameConstraint)
                        End If

                        ' data
                        a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("data"))
                        Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

                        objNode = mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(rm.Type)), Eiffel.String(rm.NodeId))
                        BuildStructure(rm, objNode)

                        Exit Sub 'FIXME: Remove spaghetti code!
                    End If ' at least one child
                End If
            Next
        End Sub

        Private Sub BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal rmState As RmStructureCompound)
            Dim events As Object()
            Dim history_event As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim embeddedState As Boolean = False

            events = BuildHistory(a_history, RelNode)

            Dim a_rm As RmStructure = Nothing

            If rmState.Children.Count > 0 Then
                a_rm = rmState.Children.Items(0)
            End If

            If events.Length > 0 AndAlso Not a_rm Is Nothing Then
                Dim path As openehr.common_libs.structures.object_graph.path.OG_PATH = Nothing

                For i As Integer = 0 To events.Length - 1
                    history_event = CType(events(i), openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                    a = mAomFactory.create_c_attribute_single(history_event, Eiffel.String("state"))

                    'First event has the structure
                    If i = 0 Then
                        If a_rm.Type = StructureType.Slot Then
                            embeddedState = True
                            BuildSlotFromRm(a, a_rm)
                        Else
                            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                            objNode = mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(a_rm.Type)), Eiffel.String(a_rm.NodeId))
                            BuildStructure(a_rm, objNode)
                            path = GetPathOfNode(a_rm.NodeId)
                        End If
                    Else
                        If embeddedState Then
                            BuildSlotFromRm(a, a_rm)
                        Else
                            'create a reference
                            Dim ref_cadlRefNode As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF

                            If Not path Is Nothing Then
                                ref_cadlRefNode = mAomFactory.create_archetype_internal_ref(a, Eiffel.String(ReferenceModel.RM_StructureName(a_rm.Type)), path.as_string)
                            End If
                        End If
                    End If

                    a.set_existence(MakeExistence(rmState.Children.Existence))
                Next
            End If
        End Sub

        Private Function BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE) As Object()
            Dim cadlHistory, cadlEvent As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim events_rel_node As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim an_event As RmEvent
            Dim dataPath As openehr.common_libs.structures.object_graph.path.OG_PATH = Nothing
            Dim array_list_events As New ArrayList

            cadlHistory = mAomFactory.create_c_complex_object_identified(RelNode, Eiffel.String(StructureType.History.ToString.ToUpper(System.Globalization.CultureInfo.InvariantCulture)), Eiffel.String(a_history.NodeId))
            cadlHistory.set_occurrences(MakeOccurrences(a_history.Occurrences))

            If a_history.HasNameConstraint Then
                a = mAomFactory.create_c_attribute_single(cadlHistory, Eiffel.String("name"))
                BuildText(a, a_history.NameConstraint)
            End If

            If a_history.isPeriodic Then
                Dim period As New Constraint_Duration

                a = mAomFactory.create_c_attribute_single(cadlHistory, Eiffel.String("period"))
                period.MinMaxValueUnits = a_history.PeriodUnits
                'Set max and min to offset value
                period.MinimumValue = a_history.Period
                period.HasMinimum = True
                period.MaximumValue = a_history.Period
                period.HasMaximum = True
                BuildDuration(a, period)
            End If

            ' now build the events
            events_rel_node = mAomFactory.create_c_attribute_multiple(cadlHistory, Eiffel.String("events"), MakeCardinality(a_history.Children.Cardinality))

            For Each an_event In a_history.Children
                cadlEvent = mAomFactory.create_c_complex_object_identified(events_rel_node, Eiffel.String(ReferenceModel.RM_StructureName(an_event.Type)), Eiffel.String(an_event.NodeId))
                'remember the events to return these
                array_list_events.Add(cadlEvent)

                cadlEvent.set_occurrences(MakeOccurrences(an_event.Occurrences))

                Select Case an_event.Type
                    Case StructureType.Event
                        ' do nothing...
                    Case StructureType.PointEvent
                        If an_event.hasFixedOffset Then
                            Dim offset As New Constraint_Duration

                            a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("offset"))
                            offset.MinMaxValueUnits = an_event.OffsetUnits
                            'Set max and min to offset value
                            offset.MinimumValue = an_event.Offset
                            offset.HasMinimum = True
                            offset.MaximumValue = an_event.Offset
                            offset.HasMaximum = True
                            BuildDuration(a, offset)
                        End If
                    Case StructureType.IntervalEvent
                        If Not an_event.AggregateMathFunction Is Nothing AndAlso an_event.AggregateMathFunction.Codes.Count > 0 Then
                            a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("math_function"))
                            BuildCodedTextInternal(a, an_event.AggregateMathFunction, "")
                        End If

                        If an_event.hasFixedDuration Then
                            Dim fixedDuration As New Constraint_Duration

                            a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("width"))
                            fixedDuration.MinMaxValueUnits = an_event.WidthUnits
                            'Set max and min to offset value
                            fixedDuration.MinimumValue = an_event.Width
                            fixedDuration.HasMinimum = True
                            fixedDuration.MaximumValue = an_event.Width
                            fixedDuration.HasMaximum = True
                            BuildDuration(a, fixedDuration)
                        End If
                End Select

                If an_event.HasNameConstraint Then
                    a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("name"))
                    BuildText(a, an_event.NameConstraint)
                End If

                If Not a_history.Data Is Nothing Then
                    a = mAomFactory.create_c_attribute_single(cadlEvent, Eiffel.String("data"))
                    Dim typeName As EiffelKernel.string.STRING_8 = Eiffel.String(ReferenceModel.RM_StructureName(a_history.Data.Type))

                    If dataPath Is Nothing Then
                        BuildStructure(a_history.Data, mAomFactory.create_c_complex_object_identified(a, typeName, Eiffel.String(a_history.Data.NodeId)))
                        dataPath = GetPathOfNode(a_history.Data.NodeId)
                    Else
                        mAomFactory.create_archetype_internal_ref(a, typeName, dataPath.as_string)
                    End If
                End If
            Next

            Return array_list_events.ToArray()
        End Function

        Protected Sub BuildCluster(ByVal Cluster As RmCluster, ByRef RelNode As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Dim clusterCadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim rm As RmStructure

            clusterCadlObj = mAomFactory.create_c_complex_object_identified(RelNode, Eiffel.String(ReferenceModel.RM_StructureName(StructureType.Cluster)), Eiffel.String(Cluster.NodeId))
            clusterCadlObj.set_occurrences(MakeOccurrences(Cluster.Occurrences))

            If Cluster.HasNameConstraint Then
                a = mAomFactory.create_c_attribute_single(clusterCadlObj, Eiffel.String("name"))
                BuildText(a, Cluster.NameConstraint)
            End If

            If Cluster.Children.Count > 0 Then
                a = mAomFactory.create_c_attribute_multiple(clusterCadlObj, Eiffel.String("items"), MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered))
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

        Private Sub BuildProportion(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal cp As Constraint_Proportion)
            Dim proportion As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim fraction As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            proportion = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(cp.Kind)))

            If cp.Numerator.HasMaximum Or cp.Numerator.HasMinimum Or cp.Numerator.Precision <> -1 Then
                fraction = mAomFactory.create_c_attribute_single(proportion, Eiffel.String("numerator"))
                BuildReal(fraction, cp.Numerator)
            End If

            If cp.HasDenominator And (cp.Denominator.HasMaximum Or cp.Denominator.HasMinimum) Then
                fraction = mAomFactory.create_c_attribute_single(proportion, Eiffel.String("denominator"))
                BuildReal(fraction, cp.Denominator)
            End If

            If cp.IsIntegralSet Then
                'There is a restriction on whether the instance will be integral or not
                fraction = mAomFactory.create_c_attribute_single(proportion, Eiffel.String("is_integral"))

                If cp.IsIntegral Then
                    mAomFactory.create_c_primitive_object(fraction, mAomFactory.create_c_boolean_make_true())
                Else
                    mAomFactory.create_c_primitive_object(fraction, mAomFactory.create_c_boolean_make_false())
                End If
            End If

            If Not cp.AllowsAllTypes Then
                Dim integerConstraint As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER
                fraction = mAomFactory.create_c_attribute_single(proportion, Eiffel.String("type"))
                Dim allowedTypes As EiffelList.LIST_INTEGER_32 = mAomFactory.create_integer_list()

                For i As Integer = 0 To 4
                    If cp.IsTypeAllowed(i) Then
                        allowedTypes.extend(i)
                    End If
                Next

                integerConstraint = openehr.openehr.am.archetype.constraint_model.primitive.Create.C_INTEGER.make_list(allowedTypes)
                mAomFactory.create_c_primitive_object(fraction, integerConstraint)
            End If
        End Sub

        Protected Sub BuildReal(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal ct As Constraint_Real)
            Dim magnitude As openehr.openehr.am.archetype.constraint_model.primitive.C_REAL = Nothing

            If ct.HasMaximum And ct.HasMinimum Then
                magnitude = mAomFactory.create_c_real_make_bounded(ct.MinimumRealValue, ct.MaximumRealValue, ct.IncludeMinimum, ct.IncludeMaximum)
            ElseIf ct.HasMaximum Then
                magnitude = mAomFactory.create_c_real_make_lower_unbounded(ct.MaximumRealValue, ct.IncludeMaximum)
            ElseIf ct.HasMinimum Then
                magnitude = mAomFactory.create_c_real_make_upper_unbounded(ct.MinimumRealValue, ct.IncludeMinimum)
            End If

            If Not magnitude Is Nothing Then
                mAomFactory.create_c_primitive_object(attribute, magnitude)

                If ct.Precision > -1 Then
                    'TODO: set precision on C_REAL?
                    'magnitude.set_precision(ct.Precision)
                End If

                If ct.HasAssumedValue Then
                    Dim ref As EiffelKernel.dotnet.REAL_32_REF = EiffelKernel.dotnet.Create.REAL_32_REF.default_create
                    ref.set_item(CType(ct.AssumedValue, Single))
                    magnitude.set_assumed_value(ref)
                End If
            End If
        End Sub

        Protected Sub BuildCount(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal ct As Constraint_Count)
            Dim cadlCount As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(ct.Kind)))
            Dim magnitude As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER = Nothing

            If ct.HasMaximum And ct.HasMinimum Then
                magnitude = mAomFactory.create_c_integer_make_bounded(ct.MinimumValue, ct.MaximumValue, ct.IncludeMinimum, ct.IncludeMaximum)
            ElseIf ct.HasMaximum Then
                magnitude = mAomFactory.create_c_integer_make_lower_unbounded(ct.MaximumValue, ct.IncludeMaximum)
            ElseIf ct.HasMinimum Then
                magnitude = mAomFactory.create_c_integer_make_upper_unbounded(ct.MinimumValue, ct.IncludeMinimum)
            End If

            If Not magnitude Is Nothing Then
                mAomFactory.create_c_primitive_object(mAomFactory.create_c_attribute_single(cadlCount, Eiffel.String("magnitude")), magnitude)

                If ct.HasAssumedValue Then
                    Dim ref As EiffelKernel.INTEGER_32_REF = EiffelKernel.Create.INTEGER_32_REF.default_create
                    ref.set_item(CType(ct.AssumedValue, Integer))
                    magnitude.set_assumed_value(ref)
                End If
            End If
        End Sub

        Private Sub BuildDateTime(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal dt As Constraint_DateTime)
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim s As String
            Dim dtType As String = ""
            Dim cadlDateTime As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
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
                    o = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_DATE_TIME"))

                    If Not allowAll Then
                        a = mAomFactory.create_c_attribute_single(o, Eiffel.String("value"))
                        cadlDateTime = mAomFactory.create_c_primitive_object(a, mAomFactory.create_c_date_time_make_pattern(Eiffel.String(s)))
                    End If
                Case "d"
                    o = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_DATE"))

                    If Not allowAll Then
                        a = mAomFactory.create_c_attribute_single(o, Eiffel.String("value"))
                        cadlDateTime = mAomFactory.create_c_primitive_object(a, mAomFactory.create_c_date_make_pattern(Eiffel.String(s)))
                    End If
                Case "t"
                    o = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_TIME"))

                    If Not allowAll Then
                        a = mAomFactory.create_c_attribute_single(o, Eiffel.String("value"))
                        cadlDateTime = mAomFactory.create_c_primitive_object(a, mAomFactory.create_c_time_make_pattern(Eiffel.String(s)))
                    End If
            End Select
        End Sub

        Protected Sub BuildSlotFromRm(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal rm As RmSlot)
            Dim slot As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT

            If rm.NodeId = "" Then
                slot = mAomFactory.create_archetype_slot_anonymous(attribute, Eiffel.String(ReferenceModel.RM_StructureName(rm.SlotConstraint.RM_ClassType)))
            Else
                slot = mAomFactory.create_archetype_slot_identified(attribute, Eiffel.String(ReferenceModel.RM_StructureName(rm.SlotConstraint.RM_ClassType)), Eiffel.String(rm.NodeId))
            End If

            slot.set_occurrences(MakeOccurrences(rm.Occurrences))
            BuildSlot(slot, rm.SlotConstraint)
        End Sub

        Protected Sub BuildSlot(ByVal slot As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT, ByVal c As Constraint_Slot)
            If c.hasSlots Then
                Dim pattern As New System.Text.StringBuilder()
                Dim rmNamePrefix As String = ReferenceModel.ReferenceModelName & "-"
                Dim classPrefix As String = rmNamePrefix & ReferenceModel.RM_StructureName(c.RM_ClassType) & "\."

                If c.IncludeAll Then
                    slot.add_include(MakeAssertion("archetype_id/value", ".*"))
                ElseIf c.Include.Items.GetLength(0) > 0 Then
                    For Each s As String In c.Include
                        If pattern.Length > 0 Then
                            pattern.Append("|")
                        End If

                        If Not s.StartsWith(rmNamePrefix) Then
                            pattern.Append(classPrefix)
                        End If

                        pattern.Append(s)
                    Next

                    If pattern.Length > 0 Then
                        slot.add_include(MakeAssertion("archetype_id/value", pattern.ToString()))
                    End If
                ElseIf c.Exclude.Items.GetLength(0) > 0 Then
                    ' have specific exclusions but no inclusions
                    slot.add_include(MakeAssertion("archetype_id/value", ".*"))
                End If

                pattern = New System.Text.StringBuilder()

                If c.ExcludeAll Then
                    slot.add_exclude(MakeAssertion("archetype_id/value", ".*"))
                Else
                    For Each s As String In c.Exclude
                        If pattern.Length > 0 Then
                            pattern.Append("|")
                        End If

                        If Not s.StartsWith(rmNamePrefix) Then
                            pattern.Append(classPrefix)
                        End If

                        pattern.Append(s)
                    Next

                    If pattern.Length > 0 Then
                        slot.add_exclude(MakeAssertion("archetype_id/value", pattern.ToString()))
                    End If
                End If

                Debug.Assert(slot.has_excludes Or slot.has_includes)
            Else
                slot.add_include(MakeAssertion("archetype_id/value", ".*"))
            End If
        End Sub

        Protected Sub BuildDuration(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_Duration)
            Dim pattern As EiffelKernel.string.STRING_8 = Nothing
            Dim lower As EiffelKernel.string.STRING_8 = Nothing
            Dim upper As EiffelKernel.string.STRING_8 = Nothing

            If c.AllowableUnits <> "" And c.AllowableUnits <> "PYMWDTHMS" Then
                pattern = Eiffel.String(c.AllowableUnits)
            End If

            Dim durationIso As New Duration

            If c.HasMinimum Then
                durationIso.SetIsoDuration(c.MinimumValue, c.MinMaxValueUnits)
                lower = Eiffel.String(durationIso.ISO_duration)
            End If

            If c.HasMaximum Then
                durationIso.SetIsoDuration(c.MaximumValue, c.MinMaxValueUnits)
                upper = Eiffel.String(durationIso.ISO_duration)
            End If

            Dim o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(c.Kind)))

            If pattern IsNot Nothing Or lower IsNot Nothing Or upper IsNot Nothing Then
                Dim d As openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION = mAomFactory.create_c_duration_make(pattern, lower, upper, c.IncludeMinimum, c.IncludeMaximum)

                If c.HasAssumedValue Then
                    durationIso.SetIsoDuration(c.AssumedValue, c.MinMaxValueUnits)
                    d.set_assumed_value(openehr.common_libs.date_time.Create.ISO8601_DURATION.make_from_string(Eiffel.String(durationIso.ISO_duration)))
                End If

                mAomFactory.create_c_primitive_object(mAomFactory.create_c_attribute_single(o, Eiffel.String("value")), d)
            End If
        End Sub

        Protected Sub BuildQuantity(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal q As Constraint_Quantity)
            Dim cadlQuantity As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_QUANTITY = mAomFactory.create_c_dv_quantity(attribute)

            If Not q.IsNull Then
                Debug.Assert(q.IsCoded)

                cadlQuantity.set_property(openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string(Eiffel.String(q.PhysicalPropertyAsString)))

                If q.HasUnits Then
                    For Each unit As Constraint_QuantityUnit In q.Units
                        Dim magnitude As openehr.common_libs.basic.INTERVAL_REAL_32 = Nothing
                        Dim precision As openehr.common_libs.basic.INTERVAL_INTEGER_32 = Nothing

                        If unit.HasMaximum Or unit.HasMinimum Then
                            If unit.HasMaximum And unit.HasMinimum Then
                                magnitude = mAomFactory.create_real_interval_make_bounded(unit.MinimumRealValue, unit.MaximumRealValue, unit.IncludeMinimum, unit.IncludeMaximum)
                            ElseIf unit.HasMaximum Then
                                magnitude = mAomFactory.create_real_interval_make_lower_unbounded(unit.MaximumRealValue, unit.IncludeMaximum)
                            ElseIf unit.HasMinimum Then
                                magnitude = mAomFactory.create_real_interval_make_upper_unbounded(unit.MinimumRealValue, unit.IncludeMinimum)
                            End If
                        End If

                        If unit.HasAssumedValue Then
                            cadlQuantity.set_assumed_value_from_units_magnitude(Eiffel.String(unit.Unit), unit.AssumedValue, unit.Precision)
                        End If

                        If unit.Precision > -1 Then
                            precision = mAomFactory.create_integer_interval_make_bounded(unit.Precision, unit.Precision, True, True)
                        End If

                        cadlQuantity.add_unit_constraint(Eiffel.String(unit.Unit), magnitude, precision)
                    Next
                End If
            End If
        End Sub

        Private Sub BuildBoolean(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal b As Constraint_Boolean)
            Dim o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(b.Kind)))
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(o, Eiffel.String("value"))

            Dim c_value As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

            If b.TrueFalseAllowed Then
                c_value = mAomFactory.create_c_primitive_object(a, mAomFactory.create_c_boolean_make_true_false())
            ElseIf b.TrueAllowed Then
                c_value = mAomFactory.create_c_primitive_object(a, mAomFactory.create_c_boolean_make_true())
            Else
                Debug.Assert(b.FalseAllowed, "Must have FalseAllowed as true if gets to here")
                c_value = mAomFactory.create_c_primitive_object(a, mAomFactory.create_c_boolean_make_false())
            End If

            If b.HasAssumedValue Then
                Dim bool As EiffelKernel.BOOLEAN_REF = EiffelKernel.Create.BOOLEAN_REF.default_create
                bool.set_item(b.AssumedValue)
                c_value.item.set_assumed_value(bool)
            End If
        End Sub

        Protected Sub BuildOrdinal(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal o As Constraint_Ordinal)
            Dim c_value As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL
            Dim o_v As OrdinalValue
            c_value = mAomFactory.create_c_dv_ordinal(attribute)

            If o.OrdinalValues.Count > 0 Then
                For Each o_v In o.OrdinalValues
                    If o_v.InternalCode <> Nothing Then
                        Dim cadlO As openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL
                        cadlO = mAomFactory.create_ordinal(o_v.Ordinal, Eiffel.String("local::" & o_v.InternalCode))
                        c_value.add_item(cadlO)

                        If o.HasAssumedValue And o_v.Ordinal = CInt(o.AssumedValue) Then
                            c_value.set_assumed_value_from_integer(CInt(o.AssumedValue))
                        End If
                    End If
                Next
            End If
        End Sub

        Protected Sub BuildText(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal t As Constraint_Text)
            Select Case t.TypeOfTextConstraint
                Case TextConstraintType.Terminology
                    If t.ConstraintCode <> "" Then
                        BuildCodedText(attribute, t.ConstraintCode)
                    End If
                Case TextConstraintType.Internal
                    BuildCodedTextInternal(attribute, t.AllowableValues, t.AssumedValue)
                Case TextConstraintType.Text
                    BuildPlainText(attribute, t.AllowableValues.Codes)
            End Select
        End Sub

        Protected Function GetPathOfNode(ByVal NodeId As String) As openehr.common_libs.structures.object_graph.path.OG_PATH
            Dim result As openehr.common_libs.structures.object_graph.path.OG_PATH = Nothing
            Dim paths As EiffelList.LIST_REFERENCE = adlArchetype.physical_paths

            For i As Integer = 1 To paths.count
                Dim s As String = paths.i_th(i).out.to_cil

                If s.EndsWith(NodeId & "]") Then
                    result = openehr.common_libs.structures.object_graph.path.Create.OG_PATH.make_from_string(Eiffel.String(s))
                    Exit For
                End If
            Next

            Return result
        End Function

        Private Sub BuildInterval(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_Interval)
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            ' Interval<T>

            objNode = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(c.Kind)))

            'Upper of type T
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("upper"))
            BuildElementConstraint(a, c.UpperLimit)

            'Lower of type T
            a = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("lower"))
            BuildElementConstraint(a, c.LowerLimit)

            'For a date or time interval, we need to set the actual class type of the lower limit
            If c.Kind = ConstraintKind.Interval_DateTime Then
                Dim s As String = CType(a.children.i_th(1), openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).rm_type_name.to_cil

                If s <> "DV_DATE_TIME" Then
                    Dim i As Integer = attribute.children.count

                    While i > 0
                        Dim o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = CType(attribute.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                        i = i - 1

                        If o.rm_type_name.to_cil = "DV_INTERVAL<DV_DATE_TIME>" Then
                            i = 0
                            o._set_rm_type_name(Eiffel.String("DV_INTERVAL<" & s & ">"))
                        End If
                    End While
                End If
            End If
        End Sub

        Private Sub BuildMultiMedia(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_MultiMedia)
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim code_rel_node As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim ca_Term As openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE

            objNode = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(c.Kind)))
            code_rel_node = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("media_type"))

            If c.AllowableValues.Codes.Count > 0 Then
                ca_Term = mAomFactory.create_c_code_phrase_from_pattern(code_rel_node, Eiffel.String(c.AllowableValues.Phrase))
            Else
                ca_Term = openehr.openehr.am.openehr_profile.data_types.text.Create.C_CODE_PHRASE.make_from_terminology_id(Eiffel.String(c.AllowableValues.TerminologyID))
                code_rel_node.put_child(ca_Term)
            End If
        End Sub

        Private Sub BuildURI(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_URI)
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

            If c.EhrUriOnly Then
                objNode = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_EHR_URI"))
            Else
                objNode = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(c.Kind)))
            End If

            If c.RegularExpression <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("value"))
                Dim cSt As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
                cSt = mAomFactory.create_c_string_make_from_regexp(Eiffel.String(c.RegularExpression))
                mAomFactory.create_c_primitive_object(a, cSt)
            End If
        End Sub

        Private Sub BuildParsable(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_Parsable)
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            objNode = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("DV_PARSABLE"))

            If c.AllowableFormalisms.Count > 0 Then
                'Add a constraint to C_STRING
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("formalism"))
                Dim cSt As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
                cSt = mAomFactory.create_c_string_make_from_string(Eiffel.String(c.AllowableFormalisms.Item(0)))

                For i As Integer = 1 To c.AllowableFormalisms.Count - 1
                    cSt.add_string(Eiffel.String(c.AllowableFormalisms.Item(i)))
                Next

                mAomFactory.create_c_primitive_object(a, cSt)
            End If
        End Sub

        Private Sub BuildIdentifier(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_Identifier)
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            objNode = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String(ReferenceModel.RM_DataTypeName(c.Kind)))

            If c.IssuerRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("issuer"))
                Dim cSt As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
                cSt = mAomFactory.create_c_string_make_from_regexp(Eiffel.String(c.IssuerRegex))
                mAomFactory.create_c_primitive_object(a, cSt)
            End If

            If c.TypeRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("type"))
                Dim cSt As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
                cSt = mAomFactory.create_c_string_make_from_regexp(Eiffel.String(c.TypeRegex))
                mAomFactory.create_c_primitive_object(a, cSt)
            End If

            If c.IDRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("id"))
                Dim cSt As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
                cSt = mAomFactory.create_c_string_make_from_regexp(Eiffel.String(c.IDRegex))
                mAomFactory.create_c_primitive_object(a, cSt)
            End If
        End Sub

        Private Sub BuildChoice(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint_Choice)
            For Each constraint As Constraint In c.Constraints
                Dim slotConstraint As Constraint_Slot = TryCast(constraint, Constraint_Slot)

                If Not slotConstraint Is Nothing Then
                    BuildSlot(mAomFactory.create_archetype_slot_anonymous(attribute, Eiffel.String(ReferenceModel.RM_StructureName(slotConstraint.RM_ClassType))), slotConstraint)
                Else
                    BuildElementConstraint(attribute, constraint)
                End If
            Next
        End Sub

        Protected Sub BuildElementConstraint(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal c As Constraint)
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

                Case ConstraintKind.Interval_Count, ConstraintKind.Interval_Quantity, ConstraintKind.Interval_DateTime, ConstraintKind.Interval_Duration
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

        Protected Sub BuildElementOrReference(ByVal Element As RmElement, ByRef RelNode As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal index As Integer)
            If Element.Type = StructureType.Reference Then
                Dim path As openehr.common_libs.structures.object_graph.path.OG_PATH = GetPathOfNode(Element.NodeId)

                If Not path Is Nothing Then
                    Dim refCadlRefNode As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF
                    refCadlRefNode = mAomFactory.create_archetype_internal_ref(RelNode, Eiffel.String("ELEMENT"), path.as_string)
                    refCadlRefNode.set_occurrences(MakeOccurrences(Element.Occurrences))
                Else
                    'the origin of the reference has not been added yet
                    Dim ref As ReferenceToResolve
                    ref.Element = Element
                    ref.Attribute = RelNode
                    ref.Index = index
                    ReferencesToResolve.Add(ref)
                End If
            Else
                Dim elementCadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                elementCadlObj = mAomFactory.create_c_complex_object_identified(RelNode, Eiffel.String(ReferenceModel.RM_StructureName(StructureType.Element)), Eiffel.String(Element.NodeId))
                elementCadlObj.set_occurrences(MakeOccurrences(Element.Occurrences))

                If Element.HasNameConstraint Then
                    Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(elementCadlObj, Eiffel.String("name"))
                    BuildText(a, Element.NameConstraint)
                End If

                If Element.Constraint.Kind <> ConstraintKind.Any Then
                    Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(elementCadlObj, Eiffel.String("value"))
                    BuildElementConstraint(a, Element.Constraint)
                End If

                'Check for constraint on Flavours of Null
                If Element.HasNullFlavourConstraint() Then
                    Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(elementCadlObj, Eiffel.String("null_flavour"))
                    a.set_existence(mAomFactory.create_c_integer_make_bounded(0, 1, True, True).interval)
                    BuildCodedTextInternal(a, Element.ConstrainedNullFlavours, "")
                End If
            End If
        End Sub

        Protected Sub ResolveReferences()
            For Each ref As ReferenceToResolve In ReferencesToResolve
                Dim path As openehr.common_libs.structures.object_graph.path.OG_PATH = GetPathOfNode(ref.Element.NodeId)

                If Not path Is Nothing Then
                    'The following is based on refCadlRefNode = mAomFactory.create_archetype_internal_ref(ref.Attribute, Eiffel.String("ELEMENT"), path.as_string)
                    Dim refCadlRefNode As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF
                    refCadlRefNode = openehr.openehr.am.archetype.constraint_model.Create.ARCHETYPE_INTERNAL_REF.make(Eiffel.String("ELEMENT"), path.as_string)
                    ref.Attribute.children.insert(refCadlRefNode, ref.Index + 1)
                    refCadlRefNode.set_parent(ref.Attribute)

                    'The following would be equivalent to og.put_child_left(refCadlRefNode.representation, some child at ref.Index + 1) ...
                    Dim og As openehr.common_libs.structures.object_graph.OG_ATTRIBUTE_NODE
                    og = CType(ref.Attribute.representation, openehr.common_libs.structures.object_graph.OG_ATTRIBUTE_NODE)
                    og.children.put(refCadlRefNode.representation, refCadlRefNode.representation.node_id)
                    refCadlRefNode.representation.set_parent(og)
                    og.children_ordered.go_i_th(ref.Index + 1)
                    og.children_ordered.put_left(refCadlRefNode.representation)
                    og.children_sorted.extend(refCadlRefNode.representation)

                    refCadlRefNode.set_occurrences(MakeOccurrences(ref.Element.Occurrences))
                Else
                    'reference element no longer exists so build it as an element
                    BuildElementOrReference(ref.Element.Copy(), ref.Attribute, ref.Index)
                End If
            Next

            ReferencesToResolve.Clear()
        End Sub

        Private Sub BuildStructure(ByVal rmStruct As RmStructureCompound, ByRef objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim rm As RmStructure
            Dim index As Integer = 0

            ' preconditions
            Debug.Assert(rmStruct.NodeId <> "") ' anonymous

            ' now make sure there are some contents to the structure and if not set it to anyallowed
            If rmStruct.Children.Count > 0 Then
                Select Case rmStruct.Type
                    Case StructureType.Single
                        a = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("item"))
                        rm = rmStruct.Children.Items(0)

                        If rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                            BuildElementOrReference(rm, a, 0)
                        ElseIf rm.Type = StructureType.Slot Then
                            BuildSlotFromRm(a, rm)
                        Else
                            Debug.Assert(False, "Type not handled")
                        End If
                    Case StructureType.List
                        a = mAomFactory.create_c_attribute_multiple(objNode, Eiffel.String("items"), _
                            MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered))

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
                        a = mAomFactory.create_c_attribute_multiple(objNode, Eiffel.String("items"), MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered))

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
                        Dim b As openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN
                        Dim rh As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER

                        ' set is rotated
                        a = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("rotated"))

                        If table.isRotated Then
                            b = mAomFactory.create_c_boolean_make_true()
                        Else
                            b = mAomFactory.create_c_boolean_make_false()
                        End If

                        mAomFactory.create_c_primitive_object(a, b)

                        ' set number of row if not one
                        If table.NumberKeyColumns > 0 Then
                            a = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("number_key_columns"))
                            rh = mAomFactory.create_c_integer_make_bounded(table.NumberKeyColumns, table.NumberKeyColumns, True, True)
                            mAomFactory.create_c_primitive_object(a, rh)
                        End If

                        a = mAomFactory.create_c_attribute_multiple(objNode, Eiffel.String("rows"), MakeCardinality(New RmCardinality(rmStruct.Occurrences), True))
                        BuildCluster(rmStruct.Children.Items(0), a)
                End Select
            End If

            ResolveReferences()
        End Sub

        Protected Sub BuildSubjectOfData(ByVal subject As RelatedParty, ByVal root_node As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            Dim objnode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim a_relationship As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            a = mAomFactory.create_c_attribute_single(root_node, Eiffel.String("subject"))
            objnode = openehr.openehr.am.archetype.constraint_model.Create.C_COMPLEX_OBJECT.make_anonymous(Eiffel.String("PARTY_RELATED"))
            a.put_child(objnode)
            a_relationship = mAomFactory.create_c_attribute_single(objnode, Eiffel.String("relationship"))
            BuildCodedTextInternal(a_relationship, subject.Relationship, "")
        End Sub

        Protected Sub BuildParticipation(ByVal attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal participation As RmParticipation)
            Dim cObject As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            cObject = mAomFactory.create_c_complex_object_anonymous(attribute, Eiffel.String("PARTICIPATION"))
            cObject.set_occurrences(MakeOccurrences(participation.Occurrences))

            If participation.MandatoryDateTime Then
                Dim timeAttrib As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(cObject, Eiffel.String("time"))
            End If

            If participation.ModeSet.Codes.Count > 0 Then
                BuildCodedTextInternal(mAomFactory.create_c_attribute_single(cObject, Eiffel.String("mode")), participation.ModeSet, "")
            End If

            If participation.FunctionConstraint.TypeOfTextConstraint <> TextConstraintType.Text Then
                Dim constraintAttribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                constraintAttribute = mAomFactory.create_c_attribute_single(cObject, Eiffel.String("function"))

                If participation.FunctionConstraint.TypeOfTextConstraint = TextConstraintType.Internal Then
                    BuildCodedTextInternal(constraintAttribute, participation.FunctionConstraint.AllowableValues, "")
                Else
                    BuildCodedText(constraintAttribute, participation.FunctionConstraint.ConstraintCode)
                End If
            End If
        End Sub

        Protected Sub BuildParticipations(ByVal aRmClass As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal participations As RmStructureCompound)
            If participations.Children.Count > 0 Then
                Dim participationAttribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                participationAttribute = mAomFactory.create_c_attribute_multiple(aRmClass, Eiffel.String(participations.NodeId), MakeCardinality(participations.Children.Cardinality))

                For Each p As RmParticipation In participations.Children
                    BuildParticipation(participationAttribute, p)
                Next
            End If
        End Sub

        Protected Sub BuildSection(ByVal rmChildren As Children, ByVal cadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            a = mAomFactory.create_c_attribute_multiple(cadlObj, Eiffel.String("items"), MakeCardinality(rmChildren.Cardinality, rmChildren.Cardinality.Ordered))
            'SRH - 11 Feb 2009 - EDT 514 - set the minimum cardinality of cluster and structures to 1
            'As cardinality is expressed in the RM as 1..* this forces existence at the moment to be 1..1 - however this is not the intent.
            'an_attribute.set_existence(MakeExistence(rmChildren.Existence))

            For Each a_structure As RmStructure In rmChildren
                If a_structure.Type = StructureType.SECTION Then
                    Dim new_section As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

                    new_section = openehr.openehr.am.archetype.constraint_model.Create.C_COMPLEX_OBJECT.make_identified(Eiffel.String("SECTION"), Eiffel.String(a_structure.NodeId))
                    new_section.set_occurrences(MakeOccurrences(a_structure.Occurrences))

                    If a_structure.HasNameConstraint Then
                        Dim another_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                        another_attribute = mAomFactory.create_c_attribute_single(new_section, Eiffel.String("name"))
                        BuildText(another_attribute, a_structure.NameConstraint)
                    End If

                    If CType(a_structure, RmSection).Children.Count > 0 Then
                        BuildSection(CType(a_structure, RmSection).Children, new_section)
                    End If

                    a.put_child(new_section)
                ElseIf a_structure.Type = StructureType.Slot Then
                    BuildSlotFromRm(a, a_structure)
                Else
                    Debug.Assert(False)
                End If
            Next
        End Sub

        Private Sub BuildComposition(ByVal rm As RmComposition, ByVal CadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(CadlObj, Eiffel.String("category"))
            BuildCodedTextInternal(a, rm.CategoryCodePhrase, "")

            Dim eventContext As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = Nothing

            If rm.HasParticipations Then
                a = mAomFactory.create_c_attribute_single(CadlObj, Eiffel.String("context"))
                eventContext = mAomFactory.create_c_complex_object_anonymous(a, Eiffel.String("EVENT_CONTEXT"))
                BuildParticipations(eventContext, rm.Participations)
            End If

            ' Deal with the content and context
            For Each child As RmStructure In rm.Data
                Select Case child.Type
                    Case StructureType.List, StructureType.Single, StructureType.Table, StructureType.Tree
                        If eventContext Is Nothing Then
                            a = mAomFactory.create_c_attribute_single(CadlObj, Eiffel.String("context"))
                            eventContext = mAomFactory.create_c_complex_object_anonymous(a, Eiffel.String("EVENT_CONTEXT"))
                        End If

                        a = mAomFactory.create_c_attribute_single(eventContext, Eiffel.String("other_context"))
                        BuildStructure(child, mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(child.Type)), Eiffel.String(child.NodeId)))

                    Case StructureType.Slot
                        If eventContext Is Nothing Then
                            a = mAomFactory.create_c_attribute_single(CadlObj, Eiffel.String("context"))
                            eventContext = mAomFactory.create_c_complex_object_anonymous(a, Eiffel.String("EVENT_CONTEXT"))
                        End If

                        a = mAomFactory.create_c_attribute_single(eventContext, Eiffel.String("other_context"))
                        BuildSlotFromRm(a, child)

                    Case StructureType.SECTION
                        If CType(child, RmSection).Children.Count > 0 Then
                            a = mAomFactory.create_c_attribute_multiple(CadlObj, Eiffel.String("content"), MakeCardinality(CType(child, RmSection).Children.Cardinality, CType(child, RmSection).Children.Cardinality.Ordered))

                            For Each slot As RmSlot In CType(child, RmSection).Children
                                BuildSlotFromRm(a, slot)
                            Next
                        End If

                    Case Else
                        Debug.Assert(False)
                End Select
            Next
        End Sub

        Protected Sub BuildRootElement(ByVal element As RmElement, ByVal CadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            ' N.B.: Writing the name constraint has already been done in the caller.

            If Not element.Constraint Is Nothing AndAlso element.Constraint.Kind <> ConstraintKind.Any Then
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(CadlObj, Eiffel.String("value"))
                BuildElementConstraint(a, element.Constraint)
            End If
        End Sub

        Protected Sub BuildRootCluster(ByVal Cluster As RmCluster, ByVal CadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            If Cluster.Children.Count > 0 Then
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_multiple(CadlObj, Eiffel.String("items"), MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered))
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

        Protected Sub BuildRootSection(ByVal section As RmSection, ByVal CadlObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            If section.Children.Count > 0 Then
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_multiple(CadlObj, Eiffel.String("items"), MakeCardinality(section.Children.Cardinality, section.Children.Cardinality.Ordered))

                For Each rm As RmStructure In section.Children
                    If rm.Type = StructureType.SECTION Then
                        Dim newSection As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = openehr.openehr.am.archetype.constraint_model.Create.C_COMPLEX_OBJECT.make_identified(Eiffel.String("SECTION"), Eiffel.String(rm.NodeId))
                        newSection.set_occurrences(MakeOccurrences(rm.Occurrences))

                        If rm.HasNameConstraint Then
                            Dim anotherAttribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(newSection, Eiffel.String("name"))
                            BuildText(anotherAttribute, rm.NameConstraint)
                        End If

                        If CType(rm, RmSection).Children.Count > 0 Then
                            BuildSection(CType(rm, RmSection).Children, newSection)
                        End If

                        a.put_child(newSection)
                    ElseIf rm.Type = StructureType.Slot Then
                        BuildSlotFromRm(a, rm)
                    End If
                Next
            End If
        End Sub

        Private Sub BuildStructure(ByVal rm As RmStructureCompound, ByVal archetype As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal attributeName As String)
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String(attributeName))

            If rm.Children.Count > 0 Then
                Dim child As RmStructure = rm.Children.Items(0)

                If child.Type = StructureType.Slot Then
                    BuildSlotFromRm(a, child)
                Else
                    BuildStructure(child, mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(child.Type)), Eiffel.String(child.NodeId)))
                End If
            End If

            If attributeName = "state" Or attributeName = "protocol" Then
                a.set_existence(MakeExistence(rm.Children.Existence))
            End If
        End Sub

        Private Sub BuildProtocol(ByVal rm As RmStructureCompound, ByVal an_adlArchetype As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            If rm.Children.Count > 0 Then
                Dim rmStruct As RmStructure = rm.Children.Items(0)
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

                If rmStruct.Type = StructureType.Slot Then
                    a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("protocol"))
                    BuildSlotFromRm(a, CType(rmStruct, RmSlot))
                Else
                    a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("protocol"))
                    ' only 1 protocol allowed
                    Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

                    objNode = mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(rmStruct.Type)), Eiffel.String(rmStruct.NodeId))
                    BuildStructure(CType(rmStruct, RmStructureCompound), objNode)
                End If

                a.set_existence(MakeExistence(rm.Children.Existence))
            End If
        End Sub

        Private Sub BuildWorkFlowStep(ByVal rm As RmPathwayStep, ByVal an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Dim a_state, a_step As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim code_phrase As New CodePhrase

            objNode = mAomFactory.create_c_complex_object_identified(an_attribute, Eiffel.String("ISM_TRANSITION"), Eiffel.String(rm.NodeId))
            a_state = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("current_state"))
            code_phrase.TerminologyID = "openehr"
            code_phrase.Codes.Add((CInt(rm.StateType)).ToString)

            If rm.HasAlternativeState Then
                code_phrase.Codes.Add(CInt(rm.AlternativeState).ToString)
            End If

            BuildCodedTextInternal(a_state, code_phrase, "")
            a_step = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("careflow_step"))
            code_phrase = New CodePhrase
            code_phrase.Codes.Add(rm.NodeId)  ' local is default terminology, node_id of rm is same as term code of name
            BuildCodedTextInternal(a_step, code_phrase, "")
        End Sub

        Private Sub BuildPathway(ByVal rm As RmStructureCompound, ByVal arch_def As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            If rm.Children.Count > 0 Then
                a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("ism_transition"))

                For Each pathway_step As RmPathwayStep In rm.Children
                    BuildWorkFlowStep(pathway_step, a)
                Next
            End If
        End Sub

        Private Sub BuildActivity(ByVal rm As RmActivity, ByVal an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = mAomFactory.create_c_complex_object_identified(an_attribute, Eiffel.String("ACTIVITY"), Eiffel.String(rm.NodeId))
            objNode.set_occurrences(MakeOccurrences(rm.Occurrences))

            Dim escapedString As String = rm.ArchetypeId

            If escapedString <> "" Then
                Dim i As Integer = escapedString.IndexOf("\")

                'Must have at least one escaped . or it is not valid unless it is the end
                If i < 0 Or i = escapedString.Length - 1 Then
                    escapedString = escapedString.Replace(".", "\.")
                End If

                escapedString = ReferenceModel.ReferenceModelName & "-ACTION\." + escapedString
                an_attribute = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("action_archetype_id"))
                mAomFactory.create_c_primitive_object(an_attribute, mAomFactory.create_c_string_make_from_regexp(Eiffel.String(escapedString)))
            End If

            For Each rm_struct As RmStructure In rm.Children
                an_attribute = mAomFactory.create_c_attribute_single(objNode, Eiffel.String("description"))
                Select Case rm_struct.Type
                    Case StructureType.List, StructureType.Single, StructureType.Tree, StructureType.Table
                        Dim EIF_struct As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                        EIF_struct = mAomFactory.create_c_complex_object_identified(an_attribute, _
                            Eiffel.String(ReferenceModel.RM_StructureName(rm_struct.Type)), _
                            Eiffel.String(rm_struct.NodeId))

                        BuildStructure(CType(rm_struct, RmStructureCompound), EIF_struct)

                    Case StructureType.Slot
                        ' this allows a structure to be archetyped at this point
                        Debug.Assert(CType(rm_struct, RmStructure).Type = StructureType.Slot)
                        BuildSlotFromRm(an_attribute, rm_struct)
                End Select
            Next
        End Sub

        Private Sub BuildInstruction(ByVal data As RmChildren)
            For Each rm As RmStructureCompound In data
                Select Case rm.Type
                    Case StructureType.Activities

                        'ToDo: Set cardinality on this attribute
                        Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_multiple(adlArchetype.definition, Eiffel.String("activities"), MakeCardinality(New RmCardinality(0)))

                        For Each activity As RmActivity In rm.Children
                            BuildActivity(activity, a)
                        Next
                    Case StructureType.Protocol
                        BuildProtocol(rm, adlArchetype.definition)
                    Case Else
                        Debug.Assert(False, rm.Type.ToString() & " - Type under INSTRUCTION not handled")
                End Select
            Next
        End Sub

        Private Sub BuildAction(ByVal rm As RmStructureCompound, ByVal a_definition As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            Dim action_spec As RmStructure
            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

            If rm.Children.Items.Length > 0 Then
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("description"))
                action_spec = rm.Children.Items(0)

                Select Case action_spec.Type
                    Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                        objNode = mAomFactory.create_c_complex_object_identified(a, _
                            Eiffel.String(ReferenceModel.RM_StructureName(action_spec.Type)), _
                            Eiffel.String(rm.Children.Items(0).NodeId))

                        BuildStructure(action_spec, objNode)

                    Case StructureType.Slot
                        ' allows action to be specified in another archetype
                        BuildSlotFromRm(a, CType(action_spec, RmSlot))
                End Select
            End If
        End Sub

        Public Overridable Sub MakeParseTree()
            If Not mSynchronised Then
                Dim rm As RmStructureCompound
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

                'reset the ADL definition to make it again
                adlArchetype.reset_definition()

                'pick up the description data
                adlArchetype.set_description(CType(mDescription, ADL_Description).ADL_Description)

                If Not adlArchetype.translations Is Nothing Then
                    adlArchetype.translations.clear_all()
                End If

                For Each transDetail As TranslationDetails In mTranslationDetails.Values
                    Dim t As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS = CType(transDetail, ADL_TranslationDetails).ADL_Translation
                    adlArchetype.add_translation(t, t.language.code_string)
                Next

                If Not cDefinition Is Nothing Then
                    mAomFactory = adlEngine.constraint_model_factory

                    If cDefinition.HasNameConstraint Then
                        a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("name"))
                        BuildText(a, cDefinition.NameConstraint)
                    End If

                    Select Case cDefinition.Type

                        Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                            If adlArchetype.definition.any_allowed AndAlso CType(cDefinition, ArchetypeDefinition).Data.Count > 0 Then
                                'This can arise if the archetype has been saved with no children then
                                'items have been added later - this is percular to Tree, List and Table.
                                adlArchetype.definition.set_occurrences(MakeOccurrences(New RmCardinality(0)))
                            End If

                            BuildStructure(cDefinition, adlArchetype.definition)

                        Case StructureType.Cluster
                            BuildRootCluster(cDefinition, adlArchetype.definition)

                        Case StructureType.Element
                            BuildRootElement(cDefinition, adlArchetype.definition)

                        Case StructureType.SECTION
                            BuildRootSection(cDefinition, adlArchetype.definition)

                        Case StructureType.COMPOSITION
                            BuildComposition(cDefinition, adlArchetype.definition)

                        Case StructureType.EVALUATION, StructureType.ENTRY
                            BuildEntryAttributes(CType(cDefinition, RmEntry), adlArchetype.definition)

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.State
                                        BuildStructure(rm, adlArchetype.definition, "state")

                                    Case StructureType.Protocol
                                        BuildProtocol(rm, adlArchetype.definition)

                                    Case StructureType.Data
                                        BuildStructure(rm, adlArchetype.definition, "data")
                                End Select
                            Next

                        Case StructureType.ADMIN_ENTRY
                            BuildEntryAttributes(CType(cDefinition, RmEntry), adlArchetype.definition)
                            a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("data"))
                            Dim items() As RmStructure = CType(cDefinition, ArchetypeDefinition).Data.Items

                            If items.Length > 0 Then
                                Dim struct As RmStructureCompound = CType(items(0), RmStructureCompound)

                                If struct.Children.Items.Length > 0 Then
                                    struct = struct.Children.Items(0)
                                    BuildStructure(struct, mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(struct.Type)), Eiffel.String(struct.NodeId)))
                                End If
                            End If

                        Case StructureType.OBSERVATION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), adlArchetype.definition)

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

                                        If rm.Children.Items.Length > 0 Then
                                            a_rm = rm.Children.Items(0)
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
                                a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("data"))

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
                                            Dim objNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                                            objNode = mAomFactory.create_c_complex_object_identified(a, Eiffel.String(ReferenceModel.RM_StructureName(a_rm.Type)), Eiffel.String(a_rm.NodeId))
                                            BuildStructure(a_rm, objNode)
                                    End Select
                                Next
                            End If

                            If Not rm_state_history Is Nothing Then
                                a = mAomFactory.create_c_attribute_single(adlArchetype.definition, Eiffel.String("state"))
                                BuildHistory(rm_state_history, a)
                            End If

                            If Not rm_protocol Is Nothing Then
                                BuildProtocol(rm_protocol, adlArchetype.definition)
                            End If

                        Case StructureType.INSTRUCTION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), adlArchetype.definition)
                            BuildInstruction(CType(cDefinition, ArchetypeDefinition).Data)

                        Case StructureType.ACTION
                            BuildEntryAttributes(CType(cDefinition, RmEntry), adlArchetype.definition)

                            For Each rm In CType(cDefinition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.ISM_TRANSITION
                                        BuildPathway(rm, adlArchetype.definition)
                                    Case StructureType.ActivityDescription
                                        BuildAction(rm, adlArchetype.definition)
                                    Case StructureType.Slot
                                        ' this allows a structure to be archetyped at this point
                                        Debug.Assert(CType(rm.Children.Items(0), RmStructure).Type = StructureType.Slot)
                                        BuildStructure(rm, adlArchetype.definition)
                                    Case StructureType.Protocol
                                        BuildProtocol(rm, adlArchetype.definition)
                                End Select
                            Next
                    End Select

                    If HasLinkConstraints() Then
                        BuildLinks(Definition.RootLinks, adlArchetype.definition)
                    End If

                    mSynchronised = True
                End If
            End If
        End Sub

        Public Sub SetArchetypeDigest()
            Dim digest As String = AM.ArchetypeModelBuilder.ArchetypeDigest(GetCanonicalArchetype())
            adlArchetype.description.add_other_detail(Eiffel.String(AM.ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID), Eiffel.String(digest))
        End Sub

        Public Function GetCanonicalArchetype() As XMLParser.ARCHETYPE
            Return XMLParser.OpenEhr.V1.Its.Xml.AM.ArchetypeModelBuilder.CanonicalArchetype(AM.ArchetypeModelBuilder.Build(adlArchetype, New AM.CloneConstraintVisitor()))
        End Function

        Sub BuildLinks(ByVal cLinks As System.Collections.Generic.List(Of RmLink), ByVal cObject As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            Dim linksAttribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_multiple(cObject, Eiffel.String("links"), MakeCardinality(New RmCardinality(0)))
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            For Each l As RmLink In cLinks
                If l.HasConstraint Then
                    Dim linkObject As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = mAomFactory.create_c_complex_object_anonymous(linksAttribute, Eiffel.String("LINK"))

                    If l.Meaning.TypeOfTextConstraint <> TextConstraintType.Text Then
                        a = mAomFactory.create_c_attribute_single(linkObject, Eiffel.String("meaning"))
                        BuildText(a, l.Meaning)
                    End If

                    If l.LinkType.TypeOfTextConstraint <> TextConstraintType.Text Then
                        a = mAomFactory.create_c_attribute_single(linkObject, Eiffel.String("type"))
                    End If

                    If l.Target.RegularExpression <> String.Empty Then
                        a = mAomFactory.create_c_attribute_single(linkObject, Eiffel.String("target"))
                        BuildURI(a, l.Target)
                    End If
                End If
            Next
        End Sub

        Sub BuildEntryAttributes(ByVal anEntry As RmEntry, ByVal archetypeDefinition As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            If anEntry.SubjectOfData.Relationship.Codes.Count > 0 Then
                BuildSubjectOfData(anEntry.SubjectOfData, archetypeDefinition)
            End If

            If anEntry.ProviderIsMandatory Then
                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = mAomFactory.create_c_attribute_single(archetypeDefinition, Eiffel.String("provider"))
                Dim o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = openehr.openehr.am.archetype.constraint_model.Create.C_COMPLEX_OBJECT.make_anonymous(Eiffel.String("PARTY_PROXY"))
                o.set_occurrences(MakeOccurrences(New RmCardinality(1, 1)))
                a.put_child(o)
            End If

            If anEntry.HasOtherParticipations Then
                BuildParticipations(adlArchetype.definition, CType(cDefinition, RmEntry).OtherParticipations)
            End If
        End Sub

        Sub New(ByRef an_ADL_ENGINE As openehr.adl_parser.syntax.adl.ADL_ENGINE, ByVal an_ArchetypeID As ArchetypeID, ByVal primary_language As String)
            ' call to create a brand new archetype
            MyBase.New(primary_language, an_ArchetypeID)
            adlEngine = an_ADL_ENGINE
            ' make the new archetype

            Dim id As openehr.openehr.rm.support.identification.ARCHETYPE_ID
            id = openehr.openehr.rm.support.identification.Create.ARCHETYPE_ID.make_from_string(Eiffel.String(an_ArchetypeID.ToString))

            Try
                adlEngine.create_new_archetype(id.rm_originator, id.rm_name, id.rm_entity, Eiffel.String(sPrimaryLanguageCode))
                adlArchetype = adlEngine.archetype
                adlArchetype.set_archetype_id(id)
                adlArchetype.definition.set_object_id(adlArchetype.concept)
            Catch
                Debug.Assert(False)
                ''FIXME raise error
            End Try

            mDescription = New ADL_Description(adlEngine.archetype.original_language.code_string.to_cil) ' nothing to pass
        End Sub

        Sub New(ByRef anArchetype As openehr.openehr.am.archetype.ARCHETYPE, ByRef an_ADL_Engine As openehr.adl_parser.syntax.adl.ADL_ENGINE, ByVal a_filemanager As FileManagerLocal)
            ' call to create an in memory archetype from the ADL parser
            MyBase.New(anArchetype.ontology.primary_language.to_cil)

            adlArchetype = anArchetype
            adlEngine = an_ADL_Engine
            mArchetypeID = New ArchetypeID(adlArchetype.archetype_id.as_string.to_cil)
            ReferenceModel.SetArchetypedClass(mArchetypeID.ReferenceModelEntity)

            If Not adlArchetype.parent_archetype_id Is Nothing Then
                sParentArchetypeID = adlArchetype.parent_archetype_id.as_string.to_cil
            End If

            mDescription = New ADL_Description(adlArchetype.description)

            If Not adlArchetype.translations Is Nothing AndAlso adlArchetype.translations.count > 0 Then
                ' add translation details
                adlArchetype.translations.start()

                Do While Not adlArchetype.translations.off
                    Dim transDetails As ADL_TranslationDetails = New ADL_TranslationDetails(CType(adlArchetype.translations.item_for_iteration, openehr.openehr.rm.common.resource.TRANSLATION_DETAILS))
                    mTranslationDetails.Add(transDetails.Language, transDetails)
                    adlArchetype.translations.forth()
                Loop
            End If

            Select Case mArchetypeID.ReferenceModelEntity
                Case StructureType.COMPOSITION
                    cDefinition = New ADL_Composition(adlArchetype.definition, a_filemanager)
                    cDefinition.RootNodeId = adlArchetype.concept.to_cil
                Case StructureType.SECTION
                    cDefinition = New ADL_SECTION(adlArchetype.definition, a_filemanager)
                Case StructureType.List, StructureType.Tree, StructureType.Single
                    cDefinition = New RmStructureCompound(adlArchetype.definition, a_filemanager)
                Case StructureType.Table
                    cDefinition = New RmTable(adlArchetype.definition, a_filemanager)
                Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION, StructureType.ADMIN_ENTRY, StructureType.ACTION
                    cDefinition = New ADL_ENTRY(adlArchetype.definition, a_filemanager)
                    cDefinition.RootNodeId = adlArchetype.concept.to_cil
                Case StructureType.Cluster
                    cDefinition = New RmCluster(adlArchetype.definition, a_filemanager)
                Case StructureType.Element
                    cDefinition = New ADL_RmElement(adlArchetype.definition, a_filemanager)
                Case Else
                    Debug.Assert(False)
            End Select

            'get the bit with the life cycle version - not possible at the moment
            Dim y() As String = adlArchetype.archetype_id.as_string.to_cil.Split(".")

            If y.Length > 2 Then
                For i As Integer = 2 To y.Length - 1
                    sLifeCycle = sLifeCycle & y(i)
                Next
            End If
        End Sub

        Protected Sub New(ByVal primary_language As String)
            MyBase.New(primary_language)
        End Sub

        Sub New(ByRef an_ADL_ENGINE As openehr.adl_parser.syntax.adl.ADL_ENGINE)
            ' call use in create for export only
            MyBase.New(an_ADL_ENGINE.ontology.primary_language.to_cil)
            adlEngine = an_ADL_ENGINE
            ' make the new archetype

            adlArchetype = adlEngine.archetype
            mArchetypeID = New ArchetypeID(adlArchetype.archetype_id.as_string.to_cil)

            If Not adlArchetype.parent_archetype_id Is Nothing Then
                sParentArchetypeID = adlArchetype.parent_archetype_id.as_string.to_cil
            End If

            ' root of definition set to at0000 by default, may be another code
            adlArchetype.definition.set_object_id(adlArchetype.concept)

            mDescription = New ADL_Description(adlArchetype.original_language.code_string.to_cil) ' nothing to pass

            Select Case mArchetypeID.ReferenceModelEntity
                Case StructureType.COMPOSITION
                    cDefinition = New RmComposition()
                    cDefinition.RootNodeId = adlArchetype.concept.to_cil
                Case StructureType.SECTION
                    cDefinition = New RmSection(adlArchetype.concept.to_cil)
                Case StructureType.List, StructureType.Tree, StructureType.Single
                    cDefinition = New RmStructureCompound(adlArchetype.concept.to_cil, mArchetypeID.ReferenceModelEntity)
                Case StructureType.Table
                    cDefinition = New RmTable(adlArchetype.concept.to_cil)
                Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION, StructureType.ADMIN_ENTRY, StructureType.ACTION
                    cDefinition = New RmEntry(mArchetypeID.ReferenceModelEntity)
                    cDefinition.RootNodeId = adlArchetype.concept.to_cil
                Case StructureType.Cluster
                    cDefinition = New RmCluster(adlArchetype.concept.to_cil)
                Case Else
                    Debug.Assert(False)
            End Select
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
