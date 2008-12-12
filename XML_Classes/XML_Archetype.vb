'
'
'	component:   "openEHR Archetype Project"
'	description: "Builds all XML Archetypes"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
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
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel

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
        End Structure

        Protected ReferencesToResolve As ArrayList = New ArrayList

        Public Overrides Property ConceptCode() As String
            Get
                Return mXmlArchetype.concept 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
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

        'JAR: 23MAY2007, EDT-16 Validate Archetype Id against file name
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
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'Return mXmlArchetype.parent_archetype_id
                If Not mXmlArchetype.parent_archetype_id Is Nothing Then
                    Return mXmlArchetype.parent_archetype_id.value
                Else
                    Return ""
                End If
            End Get
            Set(ByVal Value As String)
                'mXmlArchetype.parent_archetype_id = value
                If (mXmlArchetype.parent_archetype_id Is Nothing) Then
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
                    Me.MakeParseTree()
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
                If (Not Filemanager.Master.FileLoading) AndAlso (Not parserIsSynchronised) Then
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

        Public Overrides Sub Specialise(ByVal ConceptShortName As String, ByRef The_Ontology As OntologyManager)
            Dim a_term As RmTerm

            mArchetypeParser.SpecialiseArchetype(ConceptShortName)
            ' Update the GUI tables with the new term
            a_term = New XML_Term(mArchetypeParser.Ontology.TermDefinition(The_Ontology.LanguageCode, mXmlArchetype.concept))
            The_Ontology.UpdateTerm(a_term)
            Me.mArchetypeID.Concept &= "-" & ConceptShortName
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
                    mArchetypeParser.NewArchetype(an_archetype_id.ToString(), sPrimaryLanguageCode, OceanArchetypeEditor.DefaultLanguageCodeSet)
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
                    mXmlArchetype.archetype_id.value = an_archetype_id.ToString() 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
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
            ' HKF: EDT-415
            'c_s.pattern = "/" + expression + "/"
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

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1 
            With cardObj.interval

                'If (Not c.IsUnbounded) Then
                .lower = c.MinCount
                .lowerSpecified = True
                .lower_included = c.IncludeLower
                .lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1                    
                'End If

                .upper_unbounded = c.IsUnbounded
                If (Not c.IsUnbounded) Then
                    .upper = c.MaxCount
                    .upperSpecified = True
                    .upper_included = c.IncludeUpper
                    .upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                End If

                ' Validate Interval PostConditions 
                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
            End With

            'original code
            'cardObj.interval.includes_maximum = True
            'cardObj.interval.includes_minimum = True
            'cardObj.interval.minimum = c.MinCount

            'If Not c.IsUnbounded Then
            '    cardObj.interval.maximum = CStr(c.MaxCount)
            'End If

            cardObj.is_ordered = c.Ordered
            'If c.Ordered Then
            '    cardObj.is_ordered = True
            'Else
            '    cardObj.is_ordered = False
            'End If

            Return cardObj

        End Function

        Private Function MakeOccurrences(ByVal c As RmCardinality) As XMLParser.IntervalOfInteger
            Dim an_interval As New XMLParser.IntervalOfInteger()

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            With an_interval
                '.lower = 0                
                'If (Not c.IsUnbounded) Then
                .lower = c.MinCount
                .lowerSpecified = True
                .lower_included = c.IncludeLower
                .lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1                    
                'End If

                .upper_unbounded = c.IsUnbounded
                If (Not c.IsUnbounded) Then
                    .upper = c.MaxCount
                    .upperSpecified = True
                    .upper_included = c.IncludeUpper
                    .upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
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

        Private Overloads Sub BuildCodedText(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal ConstraintID As String)
            Dim coded_text As XMLParser.C_COMPLEX_OBJECT
            Dim code_rel_node As XMLParser.C_ATTRIBUTE
            Dim ca_Term As XMLParser.CONSTRAINT_REF

            'coded_text = mAomFactory.MakeComplexObject(value_attribute, "DV_CODED_TEXT")
            coded_text = mAomFactory.MakeComplexObject(value_attribute, "DV_CODED_TEXT", "", MakeOccurrences(New RmCardinality(1, 1)))

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'code_rel_node = mAomFactory.MakeSingleAttribute(coded_text, "defining_code")
            code_rel_node = mAomFactory.MakeSingleAttribute(coded_text, "defining_code", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            ca_Term = New XMLParser.CONSTRAINT_REF()
            'ca_Term.rm_type_name = "External constraint" 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            ca_Term.rm_type_name = "CODE_PHRASE"
            ca_Term.node_id = ""
            ca_Term.occurrences = MakeOccurrences(New RmCardinality(1, 1))
            ca_Term.reference = ConstraintID
            mAomFactory.add_object(code_rel_node, ca_Term)
        End Sub

        Private Overloads Sub BuildCodedText(ByRef ObjNode As XMLParser.C_COMPLEX_OBJECT, ByVal RunTimeName As String)
            Dim coded_text As XMLParser.C_COMPLEX_OBJECT
            Dim code_rel_node, name_rel_node As XMLParser.C_ATTRIBUTE
            Dim ca_Term As XMLParser.CONSTRAINT_REF

            'name_rel_node = mAomFactory.MakeSingleAttribute(ObjNode, "name")
            name_rel_node = mAomFactory.MakeSingleAttribute(ObjNode, "name", New RmExistence(1).XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'coded_text = mAomFactory.MakeComplexObject(name_rel_node, "DV_CODED_TEXT")
            coded_text = mAomFactory.MakeComplexObject(name_rel_node, "DV_CODED_TEXT", "", MakeOccurrences(New RmCardinality(1, 1)))

            'code_rel_node = mAomFactory.MakeSingleAttribute(coded_text, "defining_code")
            code_rel_node = mAomFactory.MakeSingleAttribute(coded_text, "defining_code", New RmExistence(1).XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            ca_Term = New XMLParser.CONSTRAINT_REF()
            'ca_Term.rm_type_name = "External constraint" 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            ca_Term.rm_type_name = "CODE_PHRASE"
            ca_Term.node_id = ""
            ca_Term.occurrences = MakeOccurrences(New RmCardinality(1, 1))
            ca_Term.reference = RunTimeName
            mAomFactory.add_object(code_rel_node, ca_Term)
        End Sub

        Private Overloads Sub BuildCodedText(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal a_CodePhrase As CodePhrase, Optional ByVal an_assumed_value As String = "", Optional ByVal assumed_value_terminology As String = "")
            Dim coded_text As XMLParser.C_COMPLEX_OBJECT
            Dim code_rel_node As XMLParser.C_ATTRIBUTE
            Dim ca_Term As New XMLParser.C_CODE_PHRASE

            ca_Term.terminology_id = New XMLParser.TERMINOLOGY_ID 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'coded_text = mAomFactory.MakeComplexObject(value_attribute, "DV_CODED_TEXT")
            coded_text = mAomFactory.MakeComplexObject(value_attribute, "DV_CODED_TEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
            ca_Term.rm_type_name = "CODE_PHRASE"
            ca_Term.node_id = ""
            ca_Term.occurrences = MakeOccurrences(New RmCardinality(1, 1))


            'code_rel_node = mAomFactory.MakeSingleAttribute(coded_text, "defining_code") 
            code_rel_node = mAomFactory.MakeSingleAttribute(coded_text, "defining_code", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            If a_CodePhrase.Codes.Count > 0 Then

                'ca_Term.terminology_id = a_CodePhrase.TerminologyID 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                ca_Term.terminology_id.value = a_CodePhrase.TerminologyID.ToString()

                ca_Term.code_list = Array.CreateInstance(GetType(String), a_CodePhrase.Codes.Count)
                For i As Integer = 0 To a_CodePhrase.Codes.Count - 1
                    ca_Term.code_list(i) = a_CodePhrase.Codes(i)
                Next
                If an_assumed_value <> "" Then
                    'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    'ca_Term.assumed_value = an_assumed_value              
                    ca_Term.assumed_value = New XMLParser.CODE_PHRASE
                    ca_Term.assumed_value.code_string = an_assumed_value
                    If assumed_value_terminology <> "" Then
                        ca_Term.assumed_value.terminology_id = New XMLParser.TERMINOLOGY_ID
                        ca_Term.assumed_value.terminology_id.value = assumed_value_terminology
                    End If
                End If
            Else
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                ca_Term.terminology_id = New XMLParser.TERMINOLOGY_ID
                ca_Term.terminology_id.value = a_CodePhrase.TerminologyID
            End If
            mAomFactory.add_object(code_rel_node, ca_Term)
        End Sub

        ''SRH: 5Aug2007 - remove ability to set free text lists in archetypes - only in templates
        'HKF: reinstated to be able to read old archetypes
        Private Sub BuildPlainText(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal TermList As Collections.Specialized.StringCollection)
            Dim plain_text As XMLParser.C_COMPLEX_OBJECT
            Dim value_rel_node As XMLParser.C_ATTRIBUTE
            Dim cString As XMLParser.C_STRING
            Dim xmlSimple As XMLParser.C_PRIMITIVE_OBJECT

            'plain_text = mAomFactory.MakeComplexObject(value_attribute, "DV_TEXT")
            plain_text = mAomFactory.MakeComplexObject(value_attribute, "DV_TEXT", "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            If TermList.Count > 0 Then
                Dim i As Integer
                'value_rel_node = mAomFactory.MakeSingleAttribute(plain_text, "value")
                value_rel_node = mAomFactory.MakeSingleAttribute(plain_text, "value", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                cString = New XMLParser.C_STRING()
                cString.list = Array.CreateInstance(GetType(String), TermList.Count)
                For i = 0 To TermList.Count - 1
                    cString.list(i) = TermList.Item(i)
                Next
                xmlSimple = mAomFactory.MakePrimitiveObject(value_rel_node, cString)
                'Else 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                '    plain_text.any_allowed = True
            End If

        End Sub

        Private Sub DuplicateHistory(ByVal rm As RmStructureCompound, ByRef RelNode As XMLParser.C_ATTRIBUTE)

            Dim xmlHistory, xmlEvent As XMLParser.C_COMPLEX_OBJECT
            Dim an_attribute As XMLParser.C_ATTRIBUTE
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
                        'an_attribute = mAomFactory.MakeSingleAttribute(xmlHistory, "name")
                        an_attribute = mAomFactory.MakeSingleAttribute(xmlHistory, "name", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        BuildText(an_attribute, a_history.NameConstraint)
                    End If
                    If a_history.isPeriodic Then
                        Dim durationConstraint As New Constraint_Duration

                        'an_attribute = mAomFactory.MakeSingleAttribute(xmlHistory, "period")
                        an_attribute = mAomFactory.MakeSingleAttribute(xmlHistory, "period", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        durationConstraint.MinMaxValueUnits = a_history.PeriodUnits
                        'Set max and min to offset value
                        durationConstraint.MinimumValue = a_history.Period
                        durationConstraint.HasMinimum = True
                        durationConstraint.MaximumValue = a_history.Period
                        durationConstraint.HasMaximum = True
                        BuildDuration(an_attribute, durationConstraint)
                    End If

                    ' now build the events
                    If a_history.Children.Count > 0 Then
                        an_attribute = mAomFactory.MakeMultipleAttribute( _
                            xmlHistory, _
                            "events", _
                            MakeCardinality(a_history.Children.Cardinality), a_history.Children.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                        'a_history.Children.Count)

                        an_event = a_history.Children.Item(0)
                        xmlEvent = mAomFactory.MakeComplexObject( _
                            an_attribute, _
                            ReferenceModel.RM_StructureName(StructureType.Event), _
                            an_event.NodeId, _
                            MakeOccurrences(an_event.Occurrences))

                        Select Case an_event.EventType
                            Case RmEvent.ObservationEventType.PointInTime
                                If an_event.hasFixedOffset Then
                                    Dim durationConstraint As New Constraint_Duration

                                    'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "offset")
                                    an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "offset", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                    durationConstraint.MinMaxValueUnits = an_event.OffsetUnits
                                    'Set max and min to offset value
                                    durationConstraint.MinimumValue = an_event.Offset
                                    durationConstraint.HasMinimum = True
                                    durationConstraint.MaximumValue = an_event.Offset
                                    durationConstraint.HasMaximum = True
                                    BuildDuration(an_attribute, durationConstraint)
                                End If
                            Case RmEvent.ObservationEventType.Interval

                                If an_event.AggregateMathFunction <> "" Then
                                    'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "math_function")
                                    an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "math_function", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                    Dim a_code_phrase As CodePhrase = New CodePhrase
                                    a_code_phrase.FirstCode = an_event.AggregateMathFunction
                                    a_code_phrase.TerminologyID = "openehr"
                                    BuildCodedText(an_attribute, a_code_phrase)
                                End If

                                If an_event.hasFixedDuration Then
                                    Dim durationConstraint As New Constraint_Duration

                                    'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "width")
                                    an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "width", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                    durationConstraint.MinMaxValueUnits = an_event.WidthUnits
                                    'Set max and min to offset value
                                    durationConstraint.MinimumValue = an_event.Width
                                    durationConstraint.HasMinimum = True
                                    durationConstraint.MaximumValue = an_event.Width
                                    durationConstraint.HasMaximum = True
                                    BuildDuration(an_attribute, durationConstraint)
                                End If
                        End Select

                        ' runtime name
                        If an_event.HasNameConstraint Then
                            'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "name")
                            an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "name", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            BuildText(an_attribute, an_event.NameConstraint)
                        End If

                        ' data
                        'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "data")
                        an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "data", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        Dim objNode As XMLParser.C_COMPLEX_OBJECT

                        'objNode = mAomFactory.MakeComplexObject( _
                        '    an_attribute, _
                        '    ReferenceModel.RM_StructureName(rm.Type), _
                        '    rm.NodeId)

                        objNode = mAomFactory.MakeComplexObject( _
                            an_attribute, _
                            ReferenceModel.RM_StructureName(rm.Type), _
                            rm.NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                        BuildStructure(rm, objNode)

                        Exit Sub

                    End If ' at least one child
                End If
            Next

        End Sub

        Private Sub BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As XMLParser.C_ATTRIBUTE, ByVal rmState As RmStructureCompound)
            Dim events As Object()
            Dim history_event As XMLParser.C_COMPLEX_OBJECT
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim embeddedState As Boolean = False

            Try
                events = BuildHistory(a_history, RelNode)

                Dim a_rm As RmStructure = Nothing

                If rmState.Children.Count > 0 Then
                    a_rm = rmState.Children.items(0)
                End If

                If events.Length > 0 AndAlso Not a_rm Is Nothing Then
                    Dim path As String = "?"
                    For i As Integer = 0 To events.Length - 1
                        history_event = CType(events(i), XMLParser.C_COMPLEX_OBJECT)
                        'an_attribute = mAomFactory.MakeSingleAttribute(history_event, "state")
                        an_attribute = mAomFactory.MakeSingleAttribute(history_event, "state", a_history.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                        'First event has the structure
                        If i = 0 Then
                            If a_rm.Type = StructureType.Slot Then
                                embeddedState = True
                                BuildSlotFromAttribute(an_attribute, a_rm)
                            Else

                                Dim objNode As XMLParser.C_COMPLEX_OBJECT
                                'objNode = mAomFactory.MakeComplexObject(an_attribute, ReferenceModel.RM_StructureName(a_rm.Type), a_rm.NodeId)
                                objNode = mAomFactory.MakeComplexObject(an_attribute, ReferenceModel.RM_StructureName(a_rm.Type), a_rm.NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                                BuildStructure(a_rm, objNode)
                                path = Me.GetPathOfNode(a_rm.NodeId)
                            End If
                        Else
                            If embeddedState Then
                                BuildSlotFromAttribute(an_attribute, a_rm)
                            Else
                                'create a reference
                                Dim ref_xmlRefNode As XMLParser.ARCHETYPE_INTERNAL_REF
                                If Not path = "?" Then
                                    ref_xmlRefNode = mAomFactory.MakeArchetypeRef(an_attribute, ReferenceModel.RM_StructureName(a_rm.Type), path)
                                Else
                                    Debug.Assert(False, "Error with path")
                                End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Debug.Assert(False)
            End Try
        End Sub

        Private Function BuildHistory(ByVal a_history As RmHistory, ByRef RelNode As XMLParser.C_ATTRIBUTE) As Object()
            Dim xmlHistory, xmlEvent As XMLParser.C_COMPLEX_OBJECT
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim events_rel_node As New XMLParser.C_MULTIPLE_ATTRIBUTE()
            Dim an_event As RmEvent
            Dim data_processed As Boolean
            Dim data_path As String = ""
            Dim array_list_events As New ArrayList

            Try
                xmlHistory = mAomFactory.MakeComplexObject( _
                    RelNode, _
                    StructureType.History.ToString.ToUpper(System.Globalization.CultureInfo.InvariantCulture), _
                    a_history.NodeId, _
                    MakeOccurrences(a_history.Occurrences))

                If a_history.HasNameConstraint Then
                    an_attribute = New XMLParser.C_SINGLE_ATTRIBUTE()
                    an_attribute.rm_attribute_name = "name"
                    BuildText(an_attribute, a_history.NameConstraint)
                End If

                If a_history.isPeriodic Then
                    Dim durationConstraint As New Constraint_Duration

                    'an_attribute = mAomFactory.MakeSingleAttribute(xmlHistory, "period")
                    an_attribute = mAomFactory.MakeSingleAttribute(xmlHistory, "period", a_history.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                    durationConstraint.MinMaxValueUnits = a_history.PeriodUnits
                    'Set max and min to offset value
                    durationConstraint.MinimumValue = a_history.Period
                    durationConstraint.HasMinimum = True
                    durationConstraint.MaximumValue = a_history.Period
                    durationConstraint.HasMaximum = True
                    BuildDuration(an_attribute, durationConstraint)
                End If

                ' now build the events

                events_rel_node = mAomFactory.MakeMultipleAttribute( _
                    xmlHistory, _
                    "events", _
                    MakeCardinality(a_history.Children.Cardinality), a_history.Children.Existence.XmlExistence)
                'a_history.Children.Count)

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

                                'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "offset")
                                an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "offset", an_event.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                                durationConstraint.MinMaxValueUnits = an_event.OffsetUnits
                                'Set max and min to offset value
                                durationConstraint.MinimumValue = an_event.Offset
                                durationConstraint.HasMinimum = True
                                durationConstraint.MaximumValue = an_event.Offset
                                durationConstraint.HasMaximum = True
                                BuildDuration(an_attribute, durationConstraint)
                            End If
                        Case StructureType.IntervalEvent

                            If an_event.AggregateMathFunction <> "" Then
                                'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "math_function")
                                an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "math_function", an_event.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                Dim a_code_phrase As CodePhrase = New CodePhrase
                                a_code_phrase.FirstCode = an_event.AggregateMathFunction
                                a_code_phrase.TerminologyID = "openehr"
                                BuildCodedText(an_attribute, a_code_phrase)
                            End If

                            If an_event.hasFixedDuration Then
                                Dim durationConstraint As New Constraint_Duration

                                'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "width")
                                an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "width", an_event.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                durationConstraint.MinMaxValueUnits = an_event.WidthUnits
                                'Set max and min to offset value
                                durationConstraint.MinimumValue = an_event.Width
                                durationConstraint.HasMinimum = True
                                durationConstraint.MaximumValue = an_event.Width
                                durationConstraint.HasMaximum = True
                                BuildDuration(an_attribute, durationConstraint)
                            End If
                    End Select

                    ' runtime name
                    If an_event.HasNameConstraint Then
                        'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "name")
                        an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "name", an_event.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        BuildText(an_attribute, an_event.NameConstraint)
                    End If

                    ' data
                    'an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "data")
                    an_attribute = mAomFactory.MakeSingleAttribute(xmlEvent, "data", an_event.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    If Not data_processed Then
                        If Not a_history.Data Is Nothing Then
                            Dim objNode As XMLParser.C_COMPLEX_OBJECT

                            'objNode = mAomFactory.MakeComplexObject( _
                            '    an_attribute, _
                            '    ReferenceModel.RM_StructureName(a_history.Data.Type), _
                            '    a_history.Data.NodeId)

                            objNode = mAomFactory.MakeComplexObject( _
                                an_attribute, _
                                ReferenceModel.RM_StructureName(a_history.Data.Type), _
                                a_history.Data.NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                            BuildStructure(a_history.Data, objNode)

                            data_path = GetPathOfNode(a_history.Data.NodeId)
                        End If
                        data_processed = True
                    Else
                        mAomFactory.MakeArchetypeRef(an_attribute, ReferenceModel.RM_StructureName(a_history.Data.Type), data_path)
                    End If
                Next

            Catch ex As Exception
                Debug.Assert(False)
            End Try

            Return array_list_events.ToArray()
        End Function
        'JAR
        Private Sub BuildCluster(ByVal Cluster As RmCluster, ByRef RelNode As XMLParser.C_ATTRIBUTE)
            Dim cluster_xmlObj As XMLParser.C_COMPLEX_OBJECT
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim rm As RmStructure

            cluster_xmlObj = mAomFactory.MakeComplexObject( _
                RelNode, _
                ReferenceModel.RM_StructureName(StructureType.Cluster), _
                Cluster.NodeId, _
                MakeOccurrences(Cluster.Occurrences))

            If Cluster.HasNameConstraint Then
                'an_attribute = mAomFactory.MakeSingleAttribute(cluster_xmlObj, "name")
                an_attribute = mAomFactory.MakeSingleAttribute(cluster_xmlObj, "name", Cluster.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                BuildText(an_attribute, Cluster.NameConstraint)
            End If

            If Cluster.Children.Count > 0 Then
                an_attribute = mAomFactory.MakeMultipleAttribute( _
                    cluster_xmlObj, _
                    "items", _
                MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered), Cluster.Children.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered)) ', _
                'Cluster.Children.Count)

                For Each rm In Cluster.Children.items
                    If rm.Type = StructureType.Cluster Then
                        BuildCluster(rm, an_attribute)
                    ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                        BuildElementOrReference(rm, an_attribute)
                    ElseIf rm.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(an_attribute, rm)
                    Else
                        Debug.Assert(False, "Type not handled")
                    End If
                Next
                'Else 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                '    cluster_xmlObj.any_allowed = True
            End If
        End Sub

        Protected Sub BuildRootCluster(ByVal Cluster As RmCluster, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            ' CadlObj.SetObjectId(EiffelKernel.Create.STRING_8.make_from_cil(Rm.NodeId))

            If Cluster.Children.Count > 0 Then
                'an_attribute = mAomFactory.MakeMultipleAttribute(xmlObj, "items", MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered))
                an_attribute = mAomFactory.MakeMultipleAttribute(xmlObj, "items", MakeCardinality(Cluster.Children.Cardinality, Cluster.Children.Cardinality.Ordered), Cluster.Children.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                For Each Rm As RmStructure In Cluster.Children.items
                    If Rm.Type = StructureType.Cluster Then
                        BuildCluster(Rm, an_attribute)
                    ElseIf Rm.Type = StructureType.Element Or Rm.Type = StructureType.Reference Then
                        BuildElementOrReference(Rm, an_attribute)
                    ElseIf Rm.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(an_attribute, Rm)
                    Else
                        Debug.Assert(False, "Type not handled")
                    End If
                Next
            End If

            If ReferencesToResolve.Count > 0 Then
                Dim ref_xmlRefNode As XMLParser.ARCHETYPE_INTERNAL_REF
                Dim path As String

                For Each ref As ReferenceToResolve In ReferencesToResolve

                    path = GetPathOfNode(ref.Element.NodeId)
                    If Not path Is Nothing Then
                        ref_xmlRefNode = mAomFactory.MakeArchetypeRef(ref.Attribute, "ELEMENT", path)
                        ref_xmlRefNode.occurrences = MakeOccurrences(ref.Element.Occurrences)
                    Else
                        'reference element no longer exists so build it as an element
                        Dim new_element As RmElement = ref.Element.Copy()
                        BuildElementOrReference(new_element, ref.Attribute)
                    End If

                Next
                ReferencesToResolve.Clear()
            End If

        End Sub

        Protected Sub BuildRootElement(ByVal an_element As RmElement, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a element

            If an_element.HasNameConstraint Then
                Dim an_attribute As XMLParser.C_ATTRIBUTE

                'an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "name")
                an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "name", an_element.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                BuildText(an_attribute, an_element.NameConstraint)
            End If

            If an_element.Constraint Is Nothing OrElse an_element.Constraint.Type = ConstraintType.Any Then
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'If xmlObj.attributes Is Nothing OrElse xmlObj.attributes.Length = 0 Then
                '    xmlObj.any_allowed = True
                'End If
            Else
                Dim value_attribute As XMLParser.C_ATTRIBUTE

                'value_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "value")
                value_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "value", an_element.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                BuildElementConstraint(xmlObj, value_attribute, an_element.Constraint)
            End If

        End Sub

        Private Sub BuildProportion(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal cp As Constraint_Proportion)
            Dim RatioObject As XMLParser.C_COMPLEX_OBJECT
            Dim fraction_attribute As XMLParser.C_ATTRIBUTE

            'RatioObject = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(cp.Type))
            RatioObject = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(cp.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            If cp.Numerator.HasMaximum Or cp.Numerator.HasMinimum Then
                'fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "numerator")
                fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "numerator", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                BuildReal(fraction_attribute, cp.Numerator)
            End If
            If cp.Denominator.HasMaximum Or cp.Denominator.HasMinimum Then
                'fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "denominator")
                fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "denominator", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                BuildReal(fraction_attribute, cp.Denominator)
            End If

            If cp.IsIntegralSet Then
                'There is a restriction on whether the instance will be integral or not
                'fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "is_integral")
                fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "is_integral", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                Dim boolConstraint As New Constraint_Boolean
                If cp.IsIntegral Then
                    boolConstraint.TrueAllowed = True
                Else
                    boolConstraint.FalseAllowed = True
                End If
                BuildBoolean(fraction_attribute, boolConstraint)
            End If

            If Not cp.AllowAllTypes Then
                Dim integerConstraint As New XMLParser.C_INTEGER

                'fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "type")
                fraction_attribute = mAomFactory.MakeSingleAttribute(RatioObject, "type", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                Dim allowedTypes As New ArrayList

                For i As Integer = 0 To 4
                    If cp.IsTypeAllowed(i) Then
                        'allowedTypes.Add(i.ToString) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        allowedTypes.Add(i)
                    End If
                Next

                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1 exception raised string to integer
                'integerConstraint.list = allowedTypes.ToArray(GetType(String))
                integerConstraint.list = allowedTypes.ToArray(GetType(Int32))

                mAomFactory.MakePrimitiveObject(fraction_attribute, integerConstraint)
            End If

        End Sub

        Private Sub BuildReal(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal ct As Constraint_Count)
            Dim magnitude As XMLParser.C_PRIMITIVE_OBJECT
            Dim cReal As New XMLParser.C_REAL

            If ct.HasMaximum Or ct.HasMinimum Then
                cReal.range = New XMLParser.IntervalOfReal
            End If

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            If ct.HasMinimum Then
                cReal.range.lower = ct.MinimumValue
                cReal.range.lowerSpecified = True
                cReal.range.lower_included = ct.IncludeMinimum
                cReal.range.lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1 
            Else
                cReal.range.lower_unbounded = True
            End If

            If ct.HasMaximum Then
                cReal.range.upper = ct.MaximumValue
                cReal.range.upperSpecified = True
                cReal.range.upper_included = ct.IncludeMaximum
                cReal.range.upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            Else
                cReal.range.upper_unbounded = True
            End If

            'If ct.HasMaximum And ct.HasMinimum Then
            '    cReal.range.minimum = ct.MinimumValue
            '    cReal.range.minimumSpecified = True
            '    cReal.range.maximum = ct.MaximumValue
            '    cReal.range.maximumSpecified = True
            '    cReal.range.includes_minimum = ct.IncludeMinimum
            '    cReal.range.includes_maximum = ct.IncludeMaximum
            'ElseIf ct.HasMaximum Then
            '    cReal.range.maximum = ct.MaximumValue
            '    cReal.range.maximumSpecified = True
            '    cReal.range.includes_maximum = ct.IncludeMaximum
            'ElseIf ct.HasMinimum Then
            '    cReal.range.minimum = ct.MinimumValue
            '    cReal.range.minimumSpecified = True
            '    cReal.range.includes_minimum = ct.IncludeMinimum
            'End If

            If ct.HasAssumedValue Then
                cReal.assumed_valueSpecified = True
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                cReal.assumed_value = CSng(ct.AssumedValue)
                'cReal.assumed_value = ct.AssumedValue.ToString()
            End If

            magnitude = mAomFactory.MakePrimitiveObject(value_attribute, cReal)

            ' Validate Interval PostConditions
            With cReal.range
                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
            End With
        End Sub


        Private Sub BuildCount(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal ct As Constraint_Count)
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim xmlCount As XMLParser.C_COMPLEX_OBJECT
            Dim magnitude As XMLParser.C_PRIMITIVE_OBJECT

            'xmlCount = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(ct.Type))
            xmlCount = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(ct.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            If ct.HasMaximum Or ct.HasMinimum Then
                ' set the magnitude constraint
                'an_attribute = mAomFactory.MakeSingleAttribute(xmlCount, "magnitude")
                an_attribute = mAomFactory.MakeSingleAttribute(xmlCount, "magnitude", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                Dim c_int As New XMLParser.C_INTEGER

                If ct.HasMaximum Or ct.HasMinimum Then
                    c_int.range = New XMLParser.IntervalOfInteger 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                    If ct.HasMaximum Then
                        c_int.range.upper = CInt(ct.MaximumValue)
                        c_int.range.upperSpecified = True
                        c_int.range.upper_included = ct.IncludeMaximum
                        c_int.range.upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    Else
                        c_int.range.upper_unbounded = True
                    End If

                    If ct.HasMinimum Then
                        c_int.range.lower = CInt(ct.MinimumValue)
                        c_int.range.lowerSpecified = True
                        c_int.range.lower_included = ct.IncludeMinimum
                        c_int.range.lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1                    
                    Else
                        c_int.range.lower_unbounded = True
                    End If
                End If

                'The following statement will never occur due to outer IF statement
                'If Not ct.HasMaximum And Not ct.HasMinimum Then
                '    Debug.Assert(False)
                '    'xmlCount.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1                    
                '    Return
                'End If

                'If ct.HasMaximum And ct.HasMinimum Then
                '    c_int.range.minimum = ct.MinimumValue
                '    c_int.range.maximum = ct.MaximumValue
                '    c_int.range.includes_minimum = ct.IncludeMinimum
                '    c_int.range.includes_maximum = ct.IncludeMaximum
                'ElseIf ct.HasMaximum Then
                '    c_int.range.maximum = ct.MaximumValue
                '    c_int.range.includes_maximum = ct.IncludeMaximum
                'ElseIf ct.HasMinimum Then
                '    c_int.range.minimum = ct.MinimumValue
                '    c_int.range.includes_minimum = ct.IncludeMinimum
                'Else
                '    Debug.Assert(False)
                '    'xmlCount.any_allowed = True 
                '    Return
                'End If

                If ct.HasAssumedValue Then
                    'c_int.assumed_value = ct.AssumedValue.ToString()
                    c_int.assumed_valueSpecified = True
                    c_int.assumed_value = CInt(ct.AssumedValue)
                End If

                magnitude = mAomFactory.MakePrimitiveObject(an_attribute, c_int)

                'Else 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                '    xmlCount.any_allowed = True

                ' Validate Interval PostConditions
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

        Private Sub BuildDateTime(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal dt As Constraint_DateTime)

            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim an_object As XMLParser.C_COMPLEX_OBJECT
            Dim cd As XMLParser.C_PRIMITIVE
            Dim xmlDateTime As New XMLParser.C_PRIMITIVE_OBJECT

            Select Case dt.TypeofDateTimeConstraint
                Case 11                 ' Allow all
                    Dim dtc As New XMLParser.C_DATE_TIME
                    dtc.pattern = "YYYY-??-??T??:??:??"
                    cd = dtc
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
                    dtc.pattern = "YYYY-??-??"
                    cd = dtc
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
                    dtc.pattern = "HH:??:??"
                    cd = dtc
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
            'an_object = mAomFactory.MakeComplexObject(value_attribute, a_type)
            an_object = mAomFactory.MakeComplexObject(value_attribute, a_type, "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'an_attribute = mAomFactory.MakeSingleAttribute(an_object, "value")
            an_attribute = mAomFactory.MakeSingleAttribute(an_object, "value", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            mAomFactory.MakePrimitiveObject(an_attribute, cd)

        End Sub

        Private Sub BuildSlotFromAttribute(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal a_slot As RmSlot)
            Dim slot As New XMLParser.ARCHETYPE_SLOT
            slot.rm_type_name = ReferenceModel.RM_StructureName(a_slot.SlotConstraint.RM_ClassType)
            slot.occurrences = MakeOccurrences(a_slot.Occurrences)
            If a_slot.NodeId Is Nothing Then
                slot.node_id = ""
            Else
                slot.node_id = a_slot.NodeId
            End If

            BuildSlot(slot, a_slot.SlotConstraint)
            mAomFactory.add_object(value_attribute, slot)
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

        Private Sub BuildDuration(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Duration)

            'Dim an_object As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type))
            Dim an_object As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'Dim an_attribute As XMLParser.C_SINGLE_ATTRIBUTE = mAomFactory.MakeSingleAttribute(an_object, "value")
            Dim an_attribute As XMLParser.C_SINGLE_ATTRIBUTE = mAomFactory.MakeSingleAttribute(an_object, "value", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            Dim objNode As XMLParser.C_PRIMITIVE_OBJECT
            Dim d As New XMLParser.C_DURATION

            Dim durationISO As New Duration()

            If c.HasMaximum Or c.HasMinimum Then
                d.range = New XMLParser.IntervalOfDuration()
                durationISO.ISO_Units = OceanArchetypeEditor.ISO_TimeUnits.GetIsoUnitForDuration(c.MinMaxValueUnits)

                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                If c.HasMinimum Then
                    durationISO.GUI_duration = CInt(c.MinimumValue)
                    d.range.lower = durationISO.ISO_duration
                    d.range.lower_included = c.IncludeMinimum
                    d.range.lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                Else
                    d.range.lower_unbounded = True
                End If

                If c.HasMaximum Then
                    durationISO.GUI_duration = CInt(c.MaximumValue)
                    d.range.upper = durationISO.ISO_duration
                    d.range.upper_included = c.IncludeMaximum
                    d.range.upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                Else
                    d.range.upper_unbounded = True
                End If

                'If c.HasMaximum And c.HasMinimum Then
                '    durationISO.GUI_duration = CInt(c.MaximumValue)
                '    d.range.maximum = durationISO.ISO_duration
                '    durationISO.GUI_duration = CInt(c.MinimumValue)
                '    d.range.minimum = durationISO.ISO_duration
                '    d.range.includes_maximum = c.IncludeMaximum
                '    d.range.includes_minimum = c.IncludeMinimum
                'ElseIf c.HasMinimum Then
                '    durationISO.GUI_duration = CInt(c.MinimumValue)
                '    d.range.minimum = durationISO.ISO_duration
                '    d.range.includes_minimum = c.IncludeMinimum
                'Else 'Has maximum
                '    durationISO.GUI_duration = CInt(c.MaximumValue)
                '    d.range.maximum = durationISO.ISO_duration
                '    d.range.includes_maximum = c.IncludeMaximum
                'End If
                With d.range
                    'Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                    Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                    'Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                    Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                    Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                    Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                End With
            End If

            'HKF: 8 Dec 2008
            'If Not c.AllowableUnits Is Nothing Then
            If Not String.IsNullOrEmpty(c.AllowableUnits) AndAlso c.AllowableUnits <> "PYMWDTHMS" Then
                d.pattern = c.AllowableUnits
            End If

            objNode = mAomFactory.MakePrimitiveObject(an_attribute, d)

            ' Validate Interval PostConditions
        End Sub

        Private Sub BuildQuantity(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal q As Constraint_Quantity)
            Dim cQuantity As New XMLParser.C_DV_QUANTITY

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'cQuantity.rm_type_name = "QUANTITY" 
            cQuantity.rm_type_name = "DV_QUANTITY"
            cQuantity.node_id = ""
            cQuantity.occurrences = MakeOccurrences(New RmCardinality(1, 1))

            mAomFactory.add_object(value_attribute, cQuantity)

            ' set the property constraint - it should be present

            If Not q.IsNull Then

                Dim cp As New XMLParser.CODE_PHRASE

                Debug.Assert(q.IsCoded)

                'cp.code_string = q.OpenEhrCode 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                cp.code_string = q.OpenEhrCode.ToString()
                cp.terminology_id = New XMLParser.TERMINOLOGY_ID 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                cp.terminology_id.value = "openehr"

                cQuantity.property = cp

                If q.has_units Then
                    Dim unit_constraint As Constraint_QuantityUnit
                    Dim cUnit As XMLParser.C_QUANTITY_ITEM

                    cQuantity.list = Array.CreateInstance(GetType(XMLParser.C_QUANTITY_ITEM), q.Units.Count)

                    For i As Integer = 1 To q.Units.Count
                        unit_constraint = q.Units(i)
                        Dim a_real As XMLParser.IntervalOfReal = Nothing
                        cUnit = New XMLParser.C_QUANTITY_ITEM

                        cUnit.units = unit_constraint.Unit

                        'Magnitude
                        If unit_constraint.HasMaximum Or unit_constraint.HasMinimum Then
                            a_real = New XMLParser.IntervalOfReal
                            'a_real.has_maximum = unit_constraint.HasMaximum
                            'a_real.has_minimum = unit_constraint.HasMinimum

                            If unit_constraint.HasMaximum Then
                                a_real.upper = unit_constraint.MaximumRealValue
                                a_real.upperSpecified = True
                                a_real.upper_included = unit_constraint.IncludeMaximum
                                a_real.upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            Else
                                a_real.upper_unbounded = True
                            End If

                            If unit_constraint.HasMinimum Then
                                a_real.lower = unit_constraint.MinimumRealValue
                                a_real.lowerSpecified = True
                                a_real.lower_included = unit_constraint.IncludeMinimum
                                a_real.lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            Else
                                a_real.lower_unbounded = True
                            End If

                            'If unit_constraint.HasMaximum And unit_constraint.HasMinimum Then
                            '    a_real.minimum = unit_constraint.MinimumValue
                            '    a_real.minimumSpecified = True
                            '    a_real.maximum = unit_constraint.MaximumValue
                            '    a_real.maximumSpecified = True
                            '    If unit_constraint.IncludeMinimum = False Then
                            '        a_real.includes_minimum = unit_constraint.IncludeMinimum
                            '    End If
                            '    If unit_constraint.IncludeMaximum = False Then
                            '        a_real.includes_maximum = unit_constraint.IncludeMaximum
                            '    End If
                            'ElseIf unit_constraint.HasMaximum Then
                            '    a_real.maximum = unit_constraint.MaximumValue
                            '    a_real.maximumSpecified = True
                            '    a_real.includes_maximum = unit_constraint.IncludeMaximum
                            'ElseIf unit_constraint.HasMinimum Then
                            '    a_real.minimum = unit_constraint.MinimumValue
                            '    a_real.minimumSpecified = True
                            '    a_real.includes_minimum = unit_constraint.IncludeMinimum
                            'End If

                            ' Validate Interval PostConditions
                            With a_real
                                Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                                Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                                Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                                Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                                Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                                Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                            End With
                        End If

                        If Not a_real Is Nothing Then
                            cUnit.magnitude = a_real
                        End If

                        'Precision
                        If unit_constraint.Precision > -1 Then
                            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            'cUnit.precision = unit_constraint.Precision
                            cUnit.precision = New XMLParser.IntervalOfInteger
                            cUnit.precision.lower = unit_constraint.Precision
                            cUnit.precision.upper = unit_constraint.Precision
                            cUnit.precision.lower_included = True
                            cUnit.precision.upper_included = True
                            cUnit.precision.lowerSpecified = True
                            cUnit.precision.upperSpecified = True
                            cUnit.precision.lower_includedSpecified = True
                            cUnit.precision.upper_includedSpecified = True
                        End If

                        'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        If unit_constraint.HasAssumedValue Then
                            'cUnit.assumed_value = unit_constraint.AssumedValue (as CSng)
                            'cUnit.assumed_valueSpecified = True

                            If cQuantity.assumed_value Is Nothing Then
                                cQuantity.assumed_value = New XMLParser.DV_QUANTITY
                                cQuantity.assumed_value.units = unit_constraint.Unit '1 units string
                                cQuantity.assumed_value.magnitude = unit_constraint.AssumedValue '1 magnitude double
                                cQuantity.assumed_value.precision = unit_constraint.Precision
                                '0..1 precision int 'Optional, don't set it

                            Else 'C_DV_QUANTITY has only ONE assumed value.  More than one unit exists with an assumed value
                                Throw New ArgumentException("Quanitity constraint has more than one assumed value! (XML_Archetype BuildQuantity)")
                            End If
                        End If

                        'vb collection is base 1, cQuantity.list is base 0
                        cQuantity.list(i - 1) = cUnit
                    Next
                End If

                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'Else
                '    cQuantity.any_allowed = True
            End If

        End Sub

        Private Sub BuildBoolean(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal b As Constraint_Boolean)

            'Dim an_object As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(b.Type))
            Dim an_object As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(b.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            'Dim an_attribute As XMLParser.C_SINGLE_ATTRIBUTE = mAomFactory.MakeSingleAttribute(an_object, "value")
            Dim an_attribute As XMLParser.C_SINGLE_ATTRIBUTE = mAomFactory.MakeSingleAttribute(an_object, "value", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            Dim c_value As XMLParser.C_PRIMITIVE_OBJECT
            Dim c_bool As New XMLParser.C_BOOLEAN

            If b.TrueFalseAllowed Then
                c_bool.false_valid = True
                c_bool.true_valid = True
                c_value = mAomFactory.MakePrimitiveObject(an_attribute, c_bool)
            ElseIf b.TrueAllowed Then
                c_bool.false_valid = False
                c_bool.true_valid = True
                c_value = mAomFactory.MakePrimitiveObject(an_attribute, c_bool)
            ElseIf b.FalseAllowed Then
                c_bool.false_valid = True
                c_bool.true_valid = False
                c_value = mAomFactory.MakePrimitiveObject(an_attribute, c_bool)
            End If

            If b.hasAssumedValue Then
                c_bool.assumed_value = b.AssumedValue
                c_bool.assumed_valueSpecified = True
            End If

        End Sub

        Private Sub BuildOrdinal(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal o As Constraint_Ordinal)
            Dim c_value As XMLParser.C_DV_ORDINAL
            Dim o_v As OrdinalValue

            c_value = New XMLParser.C_DV_ORDINAL()
            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'c_value.rm_type_name = "ORDINAL"
            'c_value.list = Array.CreateInstance(GetType(XMLParser.ORDINAL), o.OrdinalValues.Count) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            c_value.rm_type_name = "DV_ORDINAL"
            c_value.list = Array.CreateInstance(GetType(XMLParser.DV_ORDINAL), o.OrdinalValues.Count)
            c_value.node_id = ""
            c_value.occurrences = MakeOccurrences(New RmCardinality(1, 1))

            If o.OrdinalValues.Count > 0 Then
                Dim i As Integer = 0
                For Each o_v In o.OrdinalValues
                    'SRH: Added as empty rows still give a count of 1
                    If o_v.InternalCode <> Nothing Then
                        'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        'Dim xmlO As New XMLParser.ORDINAL
                        'xmlO.value = o_v.Ordinal.ToString()
                        'xmlO.symbol = New XMLParser.CODE_PHRASE
                        'xmlO.symbol.code_string = o_v.InternalCode
                        'xmlO.symbol.terminology_id = "local"

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
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'c_value.assumed_value = CStr(o.AssumedValue)
                c_value.assumed_value = New XMLParser.DV_ORDINAL
                c_value.assumed_value.symbol = New XMLParser.DV_CODED_TEXT
                c_value.assumed_value.symbol.defining_code = New XMLParser.CODE_PHRASE
                c_value.assumed_value.symbol.defining_code.code_string = o.AssumedValue_CodeString
                c_value.assumed_value.symbol.defining_code.terminology_id = New XMLParser.TERMINOLOGY_ID
                c_value.assumed_value.symbol.defining_code.terminology_id.value = o.AssumedValue_TerminologyId
                c_value.assumed_value.symbol.value = ""
                c_value.assumed_value.value = CStr(o.AssumedValue)

            End If

            mAomFactory.add_object(value_attribute, c_value)
        End Sub

        Private Sub BuildText(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal t As Constraint_Text)

            Select Case t.TypeOfTextConstraint
                Case TextConstrainType.Terminology
                    If t.ConstraintCode <> "" Then
                        BuildCodedText(value_attribute, t.ConstraintCode)
                    End If
                Case TextConstrainType.Internal
                    BuildCodedText(value_attribute, t.AllowableValues, CStr(t.AssumedValue), t.AssumedValue_TerminologyId)
                Case TextConstrainType.Text
                    'HKF: reinstated BuildPlainText to be able to read old archetypes
                    'mAomFactory.MakeComplexObject(value_attribute, "DV_TEXT", "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    BuildPlainText(value_attribute, t.AllowableValues.Codes)
            End Select
        End Sub

        Protected Function GetPathOfNode(ByVal NodeId As String) As String
            Dim an_arraylist As System.Collections.ArrayList
            Dim s As String

            an_arraylist = mArchetypeParser.PhysicalPaths()

            For Each s In an_arraylist
                If s.EndsWith(NodeId & "]") Then
                    Return s
                End If
            Next
            Debug.Assert(False, "Should be a path for every node")
            Return ""
        End Function

        Private Sub BuildInterval(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Interval)

            Dim objNode As XMLParser.C_COMPLEX_OBJECT

            'objNode = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type))
            objNode = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'Upper of type T
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            'an_attribute = mAomFactory.MakeSingleAttribute(objNode, "upper")
            an_attribute = mAomFactory.MakeSingleAttribute(objNode, "upper", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            BuildElementConstraint(objNode, an_attribute, c.UpperLimit)

            'Lower of type T
            'an_attribute = mAomFactory.MakeSingleAttribute(objNode, "lower")
            an_attribute = mAomFactory.MakeSingleAttribute(objNode, "lower", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            BuildElementConstraint(objNode, an_attribute, c.LowerLimit)
        End Sub

        Private Sub BuildMultiMedia(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_MultiMedia)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT
            Dim code_rel_node As XMLParser.C_ATTRIBUTE
            Dim ca_Term As XMLParser.C_CODE_PHRASE

            'objNode = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type))
            objNode = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'code_rel_node = mAomFactory.MakeSingleAttribute(objNode, "media_type")
            code_rel_node = mAomFactory.MakeSingleAttribute(objNode, "media_type", value_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            ca_Term = New XMLParser.C_CODE_PHRASE
            ca_Term.rm_type_name = "CODE_PHRASE"
            ca_Term.node_id = ""
            ca_Term.occurrences = MakeOccurrences(New RmCardinality(1, 1))

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            ca_Term.terminology_id = New XMLParser.TERMINOLOGY_ID
            ca_Term.terminology_id.value = c.AllowableValues.TerminologyID
            If c.AllowableValues.TerminologyID = "" Then
                ca_Term.terminology_id.value = "local"
            End If
            'ca_Term.terminology = c.AllowableValues.TerminologyID

            If c.AllowableValues.Codes.Count > 0 Then
                ca_Term.code_list = Array.CreateInstance(GetType(String), c.AllowableValues.Codes.Count)
                c.AllowableValues.Codes.CopyTo(ca_Term.code_list, 0)
            End If

            mAomFactory.add_object(code_rel_node, ca_Term)

        End Sub

        Private Sub BuildURI(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_URI)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT

            If c.EhrUriOnly Then
                objNode = mAomFactory.MakeComplexObject(value_attribute, "DV_EHR_URI", "", MakeOccurrences(New RmCardinality(1, 1))) 'SRH: 1AUG2007 - support DV_EHR_URI
            Else
                objNode = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            End If

            If c.RegularExpression <> Nothing Then
                'Add a constraint to C_STRING
                Dim attribute As XMLParser.C_ATTRIBUTE
                attribute = mAomFactory.MakeSingleAttribute(objNode, "value", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.RegularExpression
                mAomFactory.MakePrimitiveObject(attribute, cSt)
            End If

        End Sub

        Private Sub BuildIdentifier(ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint_Identifier)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT

            objNode = mAomFactory.MakeComplexObject(value_attribute, ReferenceModel.RM_DataTypeName(c.Type), "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            If c.IssuerRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim attribute As XMLParser.C_ATTRIBUTE
                attribute = mAomFactory.MakeSingleAttribute(objNode, "issuer", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.IssuerRegex
                mAomFactory.MakePrimitiveObject(attribute, cSt)
            End If
            If c.TypeRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim attribute As XMLParser.C_ATTRIBUTE
                attribute = mAomFactory.MakeSingleAttribute(objNode, "type", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.TypeRegex
                mAomFactory.MakePrimitiveObject(attribute, cSt)
            End If
            If c.IDRegex <> Nothing Then
                'Add a constraint to C_STRING
                Dim attribute As XMLParser.C_ATTRIBUTE
                attribute = mAomFactory.MakeSingleAttribute(objNode, "id", MakeOccurrences(New RmCardinality(1, 1)))
                Dim cSt As New XMLParser.C_STRING
                cSt.pattern = c.IDRegex
                mAomFactory.MakePrimitiveObject(attribute, cSt)
            End If

        End Sub

        Private Sub BuildElementConstraint(ByVal parent As XMLParser.C_COMPLEX_OBJECT, ByVal value_attribute As XMLParser.C_ATTRIBUTE, ByVal c As Constraint)
            Try
                ' cannot have a value with no constraint on datatype
                Debug.Assert(c.Type <> ConstraintType.Any)

                Select Case c.Type
                    Case ConstraintType.Quantity
                        BuildQuantity(value_attribute, c)

                    Case ConstraintType.Boolean
                        BuildBoolean(value_attribute, c)

                    Case ConstraintType.Text
                        BuildText(value_attribute, c)

                    Case ConstraintType.Ordinal
                        BuildOrdinal(value_attribute, c)

                    Case ConstraintType.Any
                        'parent.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1                    

                    Case ConstraintType.Proportion
                        BuildProportion(value_attribute, c)

                    Case ConstraintType.Count
                        BuildCount(value_attribute, c)

                    Case ConstraintType.DateTime
                        BuildDateTime(value_attribute, c)

                    Case ConstraintType.Slot
                        Dim slot As New RmSlot()

                        slot.SlotConstraint = c
                        slot.Occurrences.IsUnbounded = True

                        BuildSlotFromAttribute(value_attribute, slot)

                    Case ConstraintType.Multiple
                        For Each a_constraint As Constraint In CType(c, Constraint_Choice).Constraints
                            BuildElementConstraint(parent, value_attribute, a_constraint)
                        Next

                    Case ConstraintType.Interval_Count, ConstraintType.Interval_Quantity, ConstraintType.Interval_DateTime
                        BuildInterval(value_attribute, c)

                    Case ConstraintType.MultiMedia
                        BuildMultiMedia(value_attribute, c)

                    Case ConstraintType.URI
                        BuildURI(value_attribute, c)

                    Case ConstraintType.Duration
                        BuildDuration(value_attribute, c)

                        'Case ConstraintType.Currency
                        '    BuildCurrency(value_attribute, c)

                    Case ConstraintType.Identifier
                        BuildIdentifier(value_attribute, c)

                    Case Else
                        Debug.Assert(False, String.Format("{0} constraint type is not handled", c.ToString()))

                End Select

            Catch ex As Exception
                Debug.Assert(False)
            End Try
        End Sub

        Private Sub BuildElementOrReference(ByVal Element As RmElement, ByRef RelNode As XMLParser.C_ATTRIBUTE)
            Dim value_attribute As XMLParser.C_ATTRIBUTE

            Try
                If Element.Type = StructureType.Reference Then
                    Dim ref As ReferenceToResolve

                    ref.Element = Element
                    ref.Attribute = RelNode

                    ReferencesToResolve.Add(ref)

                Else
                    Dim element_xmlObj As XMLParser.C_COMPLEX_OBJECT

                    element_xmlObj = mAomFactory.MakeComplexObject(RelNode, _
                        ReferenceModel.RM_StructureName(StructureType.Element), _
                        Element.NodeId, _
                        MakeOccurrences(Element.Occurrences))
                    'JAR

                    If Element.HasNameConstraint Then
                        Dim an_attribute As XMLParser.C_ATTRIBUTE

                        'an_attribute = mAomFactory.MakeSingleAttribute(element_xmlObj, "name")
                        an_attribute = mAomFactory.MakeSingleAttribute(element_xmlObj, "name", Element.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        BuildText(an_attribute, Element.NameConstraint)
                    End If

                    If Element.Constraint.Type = ConstraintType.Any Then
                        If element_xmlObj.attributes Is Nothing OrElse element_xmlObj.attributes.Length = 0 Then
                            'element_xmlObj.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        End If
                    Else
                        'value_attribute = mAomFactory.MakeSingleAttribute(element_xmlObj, "value")
                        value_attribute = mAomFactory.MakeSingleAttribute(element_xmlObj, "value", Element.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        BuildElementConstraint(element_xmlObj, value_attribute, Element.Constraint)
                    End If

                    'Check for constraint on Flavours of Null
                    If Element.HasNullFlavourConstraint() Then
                        Dim null_flavour_attribute As XMLParser.C_ATTRIBUTE
                        null_flavour_attribute = mAomFactory.MakeSingleAttribute(element_xmlObj, "null_flavour", New RmExistence(0, 1).XmlExistence)
                        BuildCodedText(null_flavour_attribute, Element.ConstrainedNullFlavours)
                    End If
                End If

            Catch ex As Exception
                Debug.Assert(False)
            End Try
        End Sub

        Private Sub BuildStructure(ByVal rmStruct As RmStructureCompound, ByRef objNode As XMLParser.C_COMPLEX_OBJECT)
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim rm As RmStructure

            Try
                ' preconditions
                Debug.Assert(rmStruct.NodeId <> "") ' anonymous

                ' now make sure there are some contents to the structure
                ' and if not set it to anyallowed
                If rmStruct.Children.Count > 0 Then
                    Select Case rmStruct.Type '.TypeName
                        Case StructureType.Single ' "SINGLE"
                            rm = rmStruct.Children.items(0)
                            'an_attribute = mAomFactory.MakeSingleAttribute(objNode, "item")
                            an_attribute = mAomFactory.MakeSingleAttribute(objNode, "item", rmStruct.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            If rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                BuildElementOrReference(rm, an_attribute)
                            ElseIf rm.Type = StructureType.Slot Then
                                BuildSlotFromAttribute(an_attribute, rm)
                            Else
                                Debug.Assert(False, "Type not handled")
                            End If
                        Case StructureType.List ' "LIST"
                            an_attribute = mAomFactory.MakeMultipleAttribute( _
                                objNode, _
                                "items", _
                                MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered), CType(rmStruct, RmStructureCompound).Children.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            'MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered)) ', _                        
                            'CType(rmStruct, RmStructureCompound).Children.Count)

                            For Each rm In rmStruct.Children.items
                                If rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                    BuildElementOrReference(rm, an_attribute)
                                ElseIf rm.Type = StructureType.Slot Then
                                    BuildSlotFromAttribute(an_attribute, rm)
                                Else
                                    Debug.Assert(False, "Type not handled")
                                End If
                            Next
                        Case StructureType.Tree ' "TREE"
                            an_attribute = mAomFactory.MakeMultipleAttribute( _
                                objNode, _
                                "items", _
                                MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered), CType(rmStruct, RmStructureCompound).Children.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            'MakeCardinality(CType(rmStruct, RmStructureCompound).Children.Cardinality, CType(rmStruct, RmStructureCompound).Children.Cardinality.Ordered)) ', _
                            'CType(rmStruct, RmStructureCompound).Children.Count)

                            For Each rm In rmStruct.Children.items
                                If rm.Type = StructureType.Cluster Then
                                    BuildCluster(rm, an_attribute)
                                ElseIf rm.Type = StructureType.Element Or rm.Type = StructureType.Reference Then
                                    BuildElementOrReference(rm, an_attribute)
                                ElseIf rm.Type = StructureType.Slot Then
                                    BuildSlotFromAttribute(an_attribute, rm)
                                Else
                                    Debug.Assert(False, "Type not handled")
                                End If
                            Next
                        Case StructureType.Table ' "TABLE"
                            Dim table As RmTable
                            Dim b As New XMLParser.C_BOOLEAN

                            b.assumed_valueSpecified = False

                            table = CType(rmStruct, RmTable)
                            ' set is rotated
                            'an_attribute = mAomFactory.MakeSingleAttribute(objNode, "rotated")
                            an_attribute = mAomFactory.MakeSingleAttribute(objNode, "rotated", rmStruct.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            If table.isRotated Then
                                b.true_valid = True
                                b.false_valid = False
                            Else
                                b.false_valid = True
                                b.true_valid = False
                            End If

                            mAomFactory.MakePrimitiveObject(an_attribute, b)

                            ' set number of row if not one
                            If table.NumberKeyColumns > 0 Then
                                Dim rh As New XMLParser.C_INTEGER
                                rh.range = New XMLParser.IntervalOfInteger

                                'an_attribute = mAomFactory.MakeSingleAttribute(objNode, "number_key_columns")
                                an_attribute = mAomFactory.MakeSingleAttribute(objNode, "number_key_columns", rmStruct.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                                rh.range.lower = table.NumberKeyColumns
                                rh.range.lower_included = True
                                rh.range.lower_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                rh.range.lowerSpecified = True

                                rh.range.upper_included = True
                                rh.range.upper_includedSpecified = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                rh.range.upper = table.NumberKeyColumns
                                rh.range.upperSpecified = True
                                'rh.list_openSpecified = False

                                'rh.range.includes_maximum = True
                                'rh.range.includes_minimum = True
                                'rh.range.maximum = table.NumberKeyColumns
                                'rh.range.minimum = table.NumberKeyColumns
                                'rh.list_openSpecified = False

                                ' Validate Interval PostConditions
                                With rh.range
                                    Debug.Assert(.lowerSpecified = Not .lower_unbounded, "lower specified must not equal lower unbounded")
                                    Debug.Assert(Not (.lower_included And .lower_unbounded), "lower included must not be true when unbounded")
                                    Debug.Assert(.upperSpecified = Not .upper_unbounded, "upper specified must not equal upper unbounded")
                                    Debug.Assert(Not (.upper_included And .upper_unbounded), "upper included must not be true when unbounded")
                                    Debug.Assert(.lower_includedSpecified Or .lower_unbounded, "lower included specified must not equal lower unbounded")
                                    Debug.Assert(.upper_includedSpecified Or .upper_unbounded, "upper included specified must not equal upper unbounded")
                                End With

                                mAomFactory.MakePrimitiveObject(an_attribute, rh)
                            End If


                            an_attribute = mAomFactory.MakeMultipleAttribute( _
                                objNode, _
                                "rows", _
                                MakeCardinality(New RmCardinality(rmStruct.Occurrences), True), rmStruct.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                            'MakeCardinality(New RmCardinality(rmStruct.Occurrences), True)) ', _
                            'CType(rmStruct.Children.items(0), RmCluster).Children.Count)

                            BuildCluster(rmStruct.Children.items(0), an_attribute)

                    End Select
                Else
                    'objNode.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                End If

                If ReferencesToResolve.Count > 0 Then
                    Dim ref_xmlRefNode As XMLParser.ARCHETYPE_INTERNAL_REF
                    Dim path As String

                    For Each ref As ReferenceToResolve In ReferencesToResolve

                        path = GetPathOfNode(ref.Element.NodeId)
                        If Not path Is Nothing Then
                            ref_xmlRefNode = mAomFactory.MakeArchetypeRef(ref.Attribute, "ELEMENT", path)
                            ref_xmlRefNode.occurrences = MakeOccurrences(ref.Element.Occurrences)
                        Else
                            'reference element no longer exists so build it as an element
                            Dim new_element As RmElement = ref.Element.Copy()

                            BuildElementOrReference(new_element, ref.Attribute)
                        End If
                    Next
                    ReferencesToResolve.Clear()
                End If

            Catch ex As Exception
                Debug.Assert(False)
            End Try
        End Sub

        Private Sub BuildSubjectOfData(ByVal subject As RelatedParty, ByVal root_node As XMLParser.C_COMPLEX_OBJECT)
            Dim objnode As XMLParser.C_COMPLEX_OBJECT
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim a_relationship As XMLParser.C_ATTRIBUTE

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'an_attribute = mAomFactory.MakeSingleAttribute(root_node, "subject")
            'objnode = mAomFactory.MakeComplexObject(an_attribute, "PARTY_RELATED")
            an_attribute = mAomFactory.MakeSingleAttribute(root_node, "subject", New RmExistence(1).XmlExistence)
            objnode = mAomFactory.MakeComplexObject(an_attribute, "PARTY_RELATED", "", MakeOccurrences(New RmCardinality(1, 1)))

            'a_relationship = mAomFactory.MakeSingleAttribute(objnode, "relationship")
            a_relationship = mAomFactory.MakeSingleAttribute(objnode, "relationship", New RmExistence(1).XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            BuildCodedText(a_relationship, subject.Relationship)
        End Sub

        Private Sub BuildSection(ByVal rmChildren As Children, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            an_attribute = mAomFactory.MakeMultipleAttribute( _
                xmlObj, _
                "items", _
                MakeCardinality(rmChildren.Cardinality, rmChildren.Cardinality.Ordered), rmChildren.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'MakeCardinality(rmChildren.Cardinality, rmChildren.Cardinality.Ordered)) ', _
            'rmChildren.Count)

            For Each a_structure As RmStructure In rmChildren

                If a_structure.Type = StructureType.SECTION Then
                    Dim new_section As XMLParser.C_COMPLEX_OBJECT

                    new_section = mAomFactory.MakeComplexObject( _
                    an_attribute, _
                    "SECTION", _
                    a_structure.NodeId, _
                    MakeOccurrences(a_structure.Occurrences))

                    If a_structure.HasNameConstraint Then
                        'an_attribute = mAomFactory.MakeSingleAttribute(new_section, "name")
                        an_attribute = mAomFactory.MakeSingleAttribute(new_section, "name", rmChildren.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        BuildText(an_attribute, a_structure.NameConstraint)
                    End If

                    If CType(a_structure, RmSection).Children.Count > 0 Then
                        BuildSection(CType(a_structure, RmSection).Children, new_section)
                    Else
                        'new_section.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    End If
                ElseIf a_structure.Type = StructureType.Slot Then
                    BuildSlotFromAttribute(an_attribute, a_structure)
                Else
                    Debug.Assert(False)
                End If
            Next
        End Sub

        Private Sub BuildComposition(ByVal Rm As RmComposition, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            ' set the category
            'an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "category")
            an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "category", New RmExistence(1).XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            Dim t As New Constraint_Text
            t.TypeOfTextConstraint = TextConstrainType.Terminology ' coded_text
            t.AllowableValues.TerminologyID = "openehr"

            If Rm.IsPersistent Then
                t.AllowableValues.Codes.Add("431") ' persistent
            Else
                t.AllowableValues.Codes.Add("433") ' event
            End If

            BuildCodedText(an_attribute, t.AllowableValues)

            'Changed SRH 29 Apr 2008 - added handling of participations

            Dim eventContext As XMLParser.C_COMPLEX_OBJECT = Nothing

            If Rm.HasParticipations Then
                an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "context", New RmExistence(1).XmlExistence)
                eventContext = mAomFactory.MakeComplexObject(an_attribute, "EVENT_CONTEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
                BuildParticipations(eventContext, Rm.Participations)
            End If


            ' Deal with the content and context
            If Rm.Data.Count > 0 Then

                For Each a_structure As RmStructure In Rm.Data
                    Select Case a_structure.Type
                        Case StructureType.List, StructureType.Single, StructureType.Table, StructureType.Tree

                            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            Dim new_structure As XMLParser.C_COMPLEX_OBJECT
                            'an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "context")
                            'new_structure = mAomFactory.MakeComplexObject(an_attribute, "EVENT_CONTEXT", ""))
                            'an_attribute = mAomFactory.MakeSingleAttribute(new_structure, "other_context")
                            'new_structure = mAomFactory.MakeComplexObject(an_attribute, ReferenceModel.RM_StructureName(a_structure.Type), a_structure.NodeId)

                            'Changed SRH 29 Apr 2008 - added handling of participations
                            If eventContext Is Nothing Then
                                an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "context", New RmExistence(1).XmlExistence)
                                eventContext = mAomFactory.MakeComplexObject(an_attribute, "EVENT_CONTEXT", "", MakeOccurrences(New RmCardinality(1, 1)))
                            End If
                            an_attribute = mAomFactory.MakeSingleAttribute(eventContext, "other_context", New RmExistence(1).XmlExistence)
                            new_structure = mAomFactory.MakeComplexObject(an_attribute, ReferenceModel.RM_StructureName(a_structure.Type), a_structure.NodeId, MakeOccurrences(New RmCardinality(1, 1)))
                            BuildStructure(a_structure, new_structure)

                        Case StructureType.SECTION

                            If CType(a_structure, RmSection).Children.Count > 0 Then
                                'an_attribute = mAomFactory.MakeSingleAttribute(xmlObj, "content")                                
                                an_attribute = mAomFactory.MakeMultipleAttribute(xmlObj, "content", MakeCardinality(Rm.Data.Cardinality), a_structure.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                                For Each slot As RmSlot In CType(a_structure, RmSection).Children
                                    BuildSlotFromAttribute(an_attribute, slot)
                                Next

                            End If

                        Case Else
                            Debug.Assert(False)
                    End Select
                Next
            Else
                'xmlObj.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            End If
        End Sub

        Private Sub BuildRootSection(ByVal Rm As RmSection, ByVal xmlObj As XMLParser.C_COMPLEX_OBJECT)
            ' Build a section, runtimename is already done
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            ' xmlObj.SetObjectId(Rm.NodeId))

            If Rm.Children.Count > 0 Then
                an_attribute = mAomFactory.MakeMultipleAttribute( _
                    xmlObj, _
                    "items", _
                    MakeCardinality(Rm.Children.Cardinality, Rm.Children.Cardinality.Ordered), Rm.Children.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'MakeCardinality(Rm.Children.Cardinality, Rm.Children.Cardinality.Ordered)) ', _
                'Rm.Children.Count)

                For Each a_structure As RmStructure In Rm.Children
                    If a_structure.Type = StructureType.SECTION Then
                        Dim new_section As XMLParser.C_COMPLEX_OBJECT

                        new_section = mAomFactory.MakeComplexObject( _
                            "SECTION", _
                            a_structure.NodeId, _
                            MakeOccurrences(a_structure.Occurrences))

                        If a_structure.HasNameConstraint Then
                            Dim another_attribute As XMLParser.C_ATTRIBUTE
                            'another_attribute = mAomFactory.MakeSingleAttribute(new_section, "name")
                            another_attribute = mAomFactory.MakeSingleAttribute(new_section, "name", a_structure.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            BuildText(another_attribute, a_structure.NameConstraint)
                        End If

                        If CType(a_structure, RmSection).Children.Count > 0 Then
                            BuildSection(CType(a_structure, RmSection).Children, new_section)
                        Else
                            'new_section.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        End If
                        mAomFactory.add_object(an_attribute, new_section)
                    ElseIf a_structure.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(an_attribute, a_structure)
                    Else
                        Debug.Assert(False)
                    End If
                Next
            Else
                'xmlObj.any_allowed = True 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            End If
        End Sub

        Private Sub BuildStructure(ByVal rm As RmStructureCompound, _
                ByVal an_adlArchetype As XMLParser.C_COMPLEX_OBJECT, _
                ByVal attribute_name As String)
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, attribute_name)
            an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, attribute_name, rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            If CType(rm.Children.items(0), RmStructure).Type = StructureType.Slot Then
                BuildSlotFromAttribute(an_attribute, rm.Children.items(0))
            Else
                Dim objNode As XMLParser.C_COMPLEX_OBJECT

                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'objNode = mAomFactory.MakeComplexObject( _
                '    an_attribute, _
                '    ReferenceModel.RM_StructureName(rm.Children.items(0).Type), _
                '    rm.Children.items(0).NodeId)

                objNode = mAomFactory.MakeComplexObject( _
                    an_attribute, _
                    ReferenceModel.RM_StructureName(rm.Children.items(0).Type), _
                    rm.Children.items(0).NodeId, MakeOccurrences(New RmCardinality(1, 1)))

                BuildStructure(rm.Children.items(0), objNode)
            End If
        End Sub

        Private Sub BuildProtocol(ByVal rm As RmStructure, ByVal an_adlArchetype As XMLParser.C_COMPLEX_OBJECT)
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim rmStructComp As RmStructureCompound

            If rm.Type = StructureType.Slot Then
                'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "protocol")
                an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "protocol", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                BuildSlotFromAttribute(an_attribute, rm)
            Else
                rmStructComp = CType(rm, RmStructureCompound)
                If rmStructComp.Children.Count > 0 Then
                    'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "protocol")
                    an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "protocol", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    ' only 1 protocol allowed
                    Dim objNode As XMLParser.C_COMPLEX_OBJECT

                    'objNode = mAomFactory.MakeComplexObject( _
                    '    an_attribute, _
                    '    ReferenceModel.RM_StructureName(rmStructComp.Children.items(0).Type), _
                    '    rmStructComp.Children.items(0).NodeId)


                    'Changed SRH - allow a slot for protocol
                    Dim protocolRm As RmStructure

                    protocolRm = rmStructComp.Children.items(0)
                    If protocolRm.Type = StructureType.Slot Then
                        BuildSlotFromAttribute(an_attribute, CType(protocolRm, RmSlot))

                    Else
                        objNode = mAomFactory.MakeComplexObject( _
                            an_attribute, _
                            ReferenceModel.RM_StructureName(rmStructComp.Children.items(0).Type), _
                            rmStructComp.Children.items(0).NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                        BuildStructure(CType(protocolRm, RmStructureCompound), objNode)
                    End If
                End If
            End If

        End Sub

        Private Sub BuildWorkFlowStep(ByVal rm As RmPathwayStep, ByVal an_attribute As XMLParser.C_ATTRIBUTE)
            Dim a_state, a_step As XMLParser.C_ATTRIBUTE
            Dim objNode As XMLParser.C_COMPLEX_OBJECT
            Dim code_phrase As New CodePhrase

            'objNode = mAomFactory.MakeComplexObject(an_attribute, "ISM_TRANSITION")
            objNode = mAomFactory.MakeComplexObject(an_attribute, "ISM_TRANSITION", "", MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            'a_state = mAomFactory.MakeSingleAttribute(objNode, "current_state")
            a_state = mAomFactory.MakeSingleAttribute(objNode, "current_state", an_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            code_phrase.TerminologyID = "openehr"
            code_phrase.Codes.Add((CInt(rm.StateType)).ToString)
            If rm.HasAlternativeState Then
                code_phrase.Codes.Add(CInt(rm.AlternativeState).ToString)
            End If
            BuildCodedText(a_state, code_phrase)

            'a_step = mAomFactory.MakeSingleAttribute(objNode, "careflow_step")
            a_step = mAomFactory.MakeSingleAttribute(objNode, "careflow_step", an_attribute.existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            code_phrase = New CodePhrase
            code_phrase.Codes.Add(rm.NodeId)  ' local is default terminology, node_id of rm is same as term code of name
            'code_phrase.TerminologyID = rm.NodeId
            BuildCodedText(a_step, code_phrase)

        End Sub

        Private Sub BuildPathway(ByVal rm As RmStructureCompound, ByVal arch_def As XMLParser.C_COMPLEX_OBJECT)
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            If rm.Children.Count > 0 Then
                'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "ism_transition")
                an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "ism_transition", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                For Each pathway_step As RmPathwayStep In rm.Children
                    BuildWorkFlowStep(pathway_step, an_attribute)
                Next
            End If
        End Sub

        Private Sub BuildActivity(ByVal rm As RmActivity, ByVal an_attribute As XMLParser.C_ATTRIBUTE)
            Dim objNode As XMLParser.C_COMPLEX_OBJECT = mAomFactory.MakeComplexObject(an_attribute, "ACTIVITY", rm.NodeId, MakeOccurrences(rm.Occurrences))

            Dim escapedString As String = rm.ArchetypeId

            If escapedString <> "" Then
                Dim i As Integer = escapedString.IndexOf("\")

                'Must have at least one escaped . or it is not valid unless it is the end
                If i < 0 Or i = escapedString.Length - 1 Then
                    escapedString = escapedString.Replace(".", "\.")
                End If

                escapedString = ReferenceModel.ReferenceModelName & "-ACTION\." + escapedString
                an_attribute = mAomFactory.MakeSingleAttribute(objNode, "action_archetype_id", rm.Children.Existence.XmlExistence)
                Dim c_s As New XMLParser.C_STRING
                c_s.pattern = "/" + escapedString + "/"
                mAomFactory.MakePrimitiveObject(an_attribute, c_s)
            End If

            For Each rm_struct As RmStructure In rm.Children
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'an_attribute = mAomFactory.MakeMultipleAttribute(objNode, "description", MakeCardinality(rm.Children.Cardinality)) 
                an_attribute = mAomFactory.MakeSingleAttribute(objNode, "description", rm.Children.Existence.XmlExistence)

                Select Case rm_struct.Type
                    Case StructureType.List, StructureType.Single, StructureType.Tree, StructureType.Table
                        Dim EIF_struct As XMLParser.C_COMPLEX_OBJECT
                        'EIF_struct = mAomFactory.MakeComplexObject(an_attribute, _
                        '    ReferenceModel.RM_StructureName(rm_struct.Type), _
                        '    rm_struct.NodeId)
                        EIF_struct = mAomFactory.MakeComplexObject(an_attribute, _
                            ReferenceModel.RM_StructureName(rm_struct.Type), _
                            rm_struct.NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                        BuildStructure(CType(rm_struct, RmStructureCompound), EIF_struct)

                    Case StructureType.Slot
                        ' this allows a structure to be archetyped at this point
                        Debug.Assert(CType(rm_struct, RmStructure).Type = StructureType.Slot)
                        BuildSlotFromAttribute(an_attribute, rm_struct)
                End Select
            Next
        End Sub

        Private Sub BuildInstruction(ByVal data As RmChildren)
            For Each rm As RmStructureCompound In data
                Select Case rm.Type
                    Case StructureType.Activities

                        'ToDo: Set cardinality on this attribute
                        Dim an_attribute As XMLParser.C_ATTRIBUTE

                        an_attribute = mAomFactory.MakeMultipleAttribute(mXmlArchetype.definition, _
                        "activities", MakeCardinality(New RmCardinality(0)), _
                        New RmExistence(1).XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1                        
                        'rm.Children.Count)

                        'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                        'only one activity allowed at present - EDT-44 not any more! 
                        'Debug.Assert(rm.Children.Count < 2)

                        For Each activity As RmActivity In rm.Children
                            BuildActivity(activity, an_attribute)
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
            Dim an_attribute As XMLParser.C_ATTRIBUTE
            Dim objNode As XMLParser.C_COMPLEX_OBJECT

            If rm.Children.items.Length > 0 Then
                'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "description")
                an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "description", rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                action_spec = rm.Children.items(0)

                Select Case action_spec.Type
                    Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                        'objNode = mAomFactory.MakeComplexObject(an_attribute, _
                        '    ReferenceModel.RM_StructureName(action_spec.Type), _
                        '    rm.Children.items(0).NodeId)

                        objNode = mAomFactory.MakeComplexObject(an_attribute, _
                                                    ReferenceModel.RM_StructureName(action_spec.Type), _
                                                    rm.Children.items(0).NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                        BuildStructure(action_spec, objNode)

                    Case StructureType.Slot
                        ' allows action to be specified in another archetype
                        Dim slot As RmSlot = CType(action_spec, RmSlot)

                        BuildSlotFromAttribute(an_attribute, slot)
                End Select
            End If
        End Sub

        Public Overridable Sub MakeParseTree()
            Try
                If Not mSynchronised Then
                    Dim rm As RmStructureCompound
                    Dim an_attribute As XMLParser.C_ATTRIBUTE

                    'reset the ADL definition to make it again
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

                        If cDefinition.hasNameConstraint Then
                            'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "name")
                            an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "name", New RmExistence(1).XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                            BuildText(an_attribute, cDefinition.NameConstraint)
                        End If

                        Debug.Assert(ReferenceModel.IsValidArchetypeDefinition(cDefinition.Type))

                        Select Case cDefinition.Type

                            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                'If mXmlArchetype.definition.any_allowed AndAlso CType(cDefinition, ArchetypeDefinition).Data.Count > 0 Then
                                Dim Definition As New C_COMPLEX_OBJECT_PROXY(mXmlArchetype.definition)

                                If Definition.Any_Allowed AndAlso CType(cDefinition, ArchetypeDefinition).Data.Count > 0 Then
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
                                'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "data")
                                an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "data", New RmExistence(1).XmlExistence)

                                Try
                                    Dim rm_struct As RmStructureCompound = CType(CType(cDefinition, ArchetypeDefinition).Data.items(0), RmStructureCompound).Children.items(0)
                                    Dim objNode As XMLParser.C_COMPLEX_OBJECT
                                    'objNode = mAomFactory.MakeComplexObject(an_attribute, ReferenceModel.RM_StructureName(rm_struct.Type), rm_struct.NodeId)
                                    objNode = mAomFactory.MakeComplexObject(an_attribute, ReferenceModel.RM_StructureName(rm_struct.Type), rm_struct.NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                                    BuildStructure(rm_struct, objNode)
                                Catch
                                    'ToDo - process error
                                    Debug.Assert(False, "Error building structure")
                                End Try

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
                                            Dim a_rm As RmStructure
                                            a_rm = rm.Children.items(0)

                                            If a_rm.Type = StructureType.History Then
                                                'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "state")
                                                an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "state", a_rm.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                                                ' can have EventSeries for each state
                                                BuildHistory(a_rm, an_attribute)
                                            Else
                                                rm_state = rm
                                            End If
                                    End Select
                                Next

                                'Add the data
                                If Not rm_data Is Nothing Then
                                    'an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "data")
                                    an_attribute = mAomFactory.MakeSingleAttribute(mXmlArchetype.definition, "data", rm_data.Existence.XmlExistence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                                    For Each a_rm As RmStructureCompound In rm_data.Children.items
                                        Select Case a_rm.Type '.TypeName
                                            Case StructureType.History
                                                If Not rm_state Is Nothing Then
                                                    BuildHistory(a_rm, an_attribute, rm_state)
                                                Else
                                                    BuildHistory(a_rm, an_attribute)
                                                End If
                                            Case Else
                                                Debug.Assert(False) '?OBSOLETE
                                                Dim objNode As XMLParser.C_COMPLEX_OBJECT
                                                'objNode = mAomFactory.MakeComplexObject(an_attribute, EiffelKernel.Create.STRING_8.make_from_cil(ReferenceModel.RM_StructureName(a_rm.Type)), a_rm.NodeId)
                                                objNode = mAomFactory.MakeComplexObject(an_attribute, EiffelKernel.Create.STRING_8.make_from_cil(ReferenceModel.RM_StructureName(a_rm.Type)), a_rm.NodeId, MakeOccurrences(New RmCardinality(1, 1))) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
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
                                            Debug.Assert(CType(rm.Children.items(0), RmStructure).Type = StructureType.Slot)
                                            BuildStructure(rm, mXmlArchetype.definition)
                                        Case StructureType.Protocol
                                            BuildProtocol(rm, mXmlArchetype.definition)
                                    End Select
                                Next
                        End Select
                    End If


                    If Me.HasLinkConstraints Then
                        BuildLinks(Me.Definition.RootLinks, mXmlArchetype.definition)
                    End If

                    mSynchronised = True
                End If
            Catch ex As Exception
                Debug.Assert(False)
            End Try
        End Sub

        Sub BuildLinks(ByVal cLinks As System.Collections.Generic.List(Of RmLink), ByVal cObject As XMLParser.C_COMPLEX_OBJECT)
            Dim linksAttribute As XMLParser.C_ATTRIBUTE = _
                mAomFactory.MakeMultipleAttribute(cObject, "links", MakeCardinality(New RmCardinality(0)), New RmExistence(0).XmlExistence)

            Dim anAttribute As XMLParser.C_ATTRIBUTE

            For Each l As RmLink In cLinks
                If l.HasConstraint Then
                    Dim linkObject As XMLParser.C_COMPLEX_OBJECT = _
                        mAomFactory.MakeComplexObject(linksAttribute, "LINK")
                    If l.Meaning.TypeOfTextConstraint <> TextConstrainType.Text Then
                        anAttribute = mAomFactory.MakeSingleAttribute(linkObject, "meaning", New RmExistence().XmlExistence)
                        BuildText(anAttribute, l.Meaning)
                    End If
                    If l.LinkType.TypeOfTextConstraint <> TextConstrainType.Text Then
                        anAttribute = mAomFactory.MakeSingleAttribute(linkObject, "type", New RmExistence().XmlExistence)
                        BuildText(anAttribute, l.LinkType)
                    End If
                    If l.Target.RegularExpression <> String.Empty Then
                        anAttribute = mAomFactory.MakeSingleAttribute(linkObject, "target".ToLowerInvariant, New RmExistence().XmlExistence)
                        BuildURI(anAttribute, l.Target)
                    End If
                End If
            Next
        End Sub


        Sub BuildEntryAttributes(ByVal anEntry As RmEntry, ByVal archetypeDefinition As XMLParser.C_COMPLEX_OBJECT)

            If anEntry.SubjectOfData.Relationship.Codes.Count > 0 Then
                BuildSubjectOfData(anEntry.SubjectOfData, archetypeDefinition)
            End If
            If CType(cDefinition, RmEntry).ProviderIsMandatory Then
                Dim objnode As XMLParser.C_COMPLEX_OBJECT
                Dim an_attribute As XMLParser.C_ATTRIBUTE
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'an_attribute = mAomFactory.MakeSingleAttribute(root_node, "subject")
                'objnode = mAomFactory.MakeComplexObject(an_attribute, "PARTY_RELATED")
                an_attribute = mAomFactory.MakeSingleAttribute(archetypeDefinition, "provider", New RmExistence(1).XmlExistence)
                objnode = mAomFactory.MakeComplexObject(an_attribute, "PARTY_PROXY", "", MakeOccurrences(New RmCardinality(1, 1)))
            End If

            BuildParticipations(Me.mXmlArchetype.definition, CType(cDefinition, RmEntry).OtherParticipations)

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
        Protected Sub BuildParticipation(ByVal attribute As XmlParser.C_ATTRIBUTE, ByVal participation As RmParticipation)
            Dim cObject As XmlParser.C_COMPLEX_OBJECT
            cObject = mAomFactory.MakeComplexObject(attribute, "PARTICIPATION", "", MakeOccurrences(participation.Occurrences))
            If participation.MandatoryDateTime Then
                Dim timeAttrib As XMLParser.C_ATTRIBUTE = mAomFactory.MakeSingleAttribute(cObject, "time", New RmExistence(1).XmlExistence)
            End If

            If participation.ModeSet.Codes.Count > 0 Then
                BuildCodedText(mAomFactory.MakeSingleAttribute(cObject, "mode", New RmExistence(1).XmlExistence), participation.ModeSet)
            End If

            If participation.FunctionConstraint.TypeOfTextConstraint <> TextConstrainType.Text Then
                Dim constraintAttribute As XMLParser.C_ATTRIBUTE
                constraintAttribute = mAomFactory.MakeSingleAttribute(cObject, "function", New RmExistence(1).XmlExistence)

                If participation.FunctionConstraint.TypeOfTextConstraint = TextConstrainType.Internal Then

                    BuildCodedText(constraintAttribute, participation.FunctionConstraint.AllowableValues)
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
                mArchetypeParser.NewArchetype(an_ArchetypeID.ToString, sPrimaryLanguageCode, OceanArchetypeEditor.DefaultLanguageCodeSet)
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

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'mArchetypeID = New ArchetypeID(mXmlArchetype.archetype_id)
            If Not mXmlArchetype.archetype_id Is Nothing Then 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                mArchetypeID = New ArchetypeID(mXmlArchetype.archetype_id.value)
            End If

            ' get the parent ID
            If Not mXmlArchetype.parent_archetype_id Is Nothing Then 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                sParentArchetypeID = mXmlArchetype.parent_archetype_id.value
            End If
            'this is the one
            mDescription = New XML_Description(mXmlArchetype.description, a_parser.Archetype.original_language.code_string)

            'HKF: 8 Dec 2008
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

                'mArchetypeID = New ArchetypeID(mXmlArchetype.archetype_id)
                If Not mXmlArchetype.archetype_id Is Nothing Then 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    mArchetypeID = New ArchetypeID(mXmlArchetype.archetype_id.value)
                End If

                ' get the parent ID
                If Not mXmlArchetype.parent_archetype_id Is Nothing Then 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    sParentArchetypeID = mXmlArchetype.parent_archetype_id.value
                End If

                ReferenceModel.SetArchetypedClass(mArchetypeID.ReferenceModelEntity)

                'description and translation details
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

                'SRH: 24.9.2007 - Check for root links
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
