'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class RmElement
    Inherits RmStructure
    Private colReferences As New System.Collections.Specialized.StringCollection
    Protected cConstraint As Constraint
    Protected boolIsReference As Boolean
    Protected boolHasReferences As Boolean

    Public ReadOnly Property isReference() As Boolean
        Get
            Return boolIsReference
        End Get
    End Property
    Public Overridable Property hasReferences() As Boolean
        Get
            Return boolHasReferences
        End Get
        Set(ByVal Value As Boolean)
            boolHasReferences = Value
        End Set
    End Property
    Public Overrides ReadOnly Property Type() As StructureType
        Get
            Return StructureType.Element
        End Get
    End Property

    Public Overridable ReadOnly Property DataType() As String
        Get
            Return cConstraint.Type.ToString
        End Get
    End Property
    Public Overridable Property Constraint() As Constraint
        Get
            Return cConstraint
        End Get
        Set(ByVal Value As Constraint)
            cConstraint = Value
        End Set
    End Property

    Public Overrides Function Copy() As RmStructure
        Dim ae As New RmElement(Me.NodeId)
        ' Also copies if it is a reference but no longer leaves it as a reference
        ' Used in specialisation of archetypes
        ae.cOccurrences = Me.Occurrences.Copy
        ae.cConstraint = Me.Constraint.copy
        ae.sNodeId = Me.NodeId
        If Not mRunTimeConstraint Is Nothing Then
            ae.mRunTimeConstraint = CType(Me.mRunTimeConstraint.copy, Constraint_Text)
        End If
        Return ae
    End Function

    Sub New(ByVal e As RmElement)
        MyBase.New(e)
        ' for reference
    End Sub
    Sub New(ByVal NodeId As String)
        MyBase.New(NodeId, StructureType.Element)
    End Sub

#Region "ADL oriented features"

    Sub New(ByVal EIF_Element As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(EIF_Element)
        ProcessElement(EIF_Element, a_filemanager)
    End Sub

#Region "Processing ADL - incoming"

    Private Sub ProcessElement(ByVal ComplexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)

        Try

            If ComplexObj.any_allowed Or (Not ComplexObj.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("value"))) Then
                'This is an unknown and is available for specialisation
                Dim c As New Constraint
                Me.cConstraint = c
                Return
            End If

            ' Get the value
            If ComplexObj.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("value")) Then
                Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                Dim i As Integer

                an_attribute = ComplexObj.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("value"))

                If an_attribute.children.count > 1 Then
                    ' multiple constraints - not dealt with yet in the GUI
                    Dim m_c As New Constraint_Choice
                    For i = 1 To an_attribute.children.count
                        m_c.Constraints.Add(ProcessValue(CType(an_attribute.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager))
                    Next
                    cConstraint = m_c
                Else
                    cConstraint = ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ComplexObj.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' set to any
            cConstraint = New Constraint
        End Try
    End Sub

    Private Function ProcessInterval(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal) As Constraint_Interval

        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim i As Integer

        Debug.Assert((Not ObjNode.any_allowed) And (ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("absolute_limits"))))
        Debug.Assert(ObjNode.attributes.count = 1)

        an_attribute = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

        Select Case ObjNode.rm_type_name.to_cil
            Case "interval_count", "Interval_Count", "INTERVAL_COUNT"
                Dim cic As New Constraint_Interval_Count
                cic.AbsoluteLimits = ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager)
                Return cic
            Case "interval_quantity", "Interval_Quantity", "INTERVAL_QUANTITY"
                Dim ciq As New Constraint_Interval_Quantity
                ciq.AbsoluteLimits = ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager)
                Return ciq
        End Select

    End Function

    Private Function ProcessMultiMedia(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_MultiMedia
        Dim mm As New Constraint_MultiMedia
        Dim media_type As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

        If ObjNode.any_allowed Then
            Return mm
        End If

        Try
            media_type = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Dim cp As CodePhrase
            Dim Obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT

            Obj = CType(media_type.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)

            mm.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.ProcessCodes(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODED_TERM))

        Catch ex As Exception
            Debug.Assert(False)
            MessageBox.Show(AE_Constants.Instance.Error_loading & " Multimedia constraint:" & ObjNode.node_id.to_cil & _
                " - " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return mm

    End Function

    Private Function ProcessValue(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_OBJECT, ByVal a_filemanager As FileManagerLocal) As Constraint

        Select Case ObjNode.rm_type_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "quantity", "real_quantity" 'real_quantity is obsolete
                Return ProcessQuantity(CType(ObjNode, openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY))
            Case "coded_text", "text"
                Return ProcessText(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
            Case "boolean"
                If TypeOf (ObjNode) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                    Return ProcessBoolean(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Else
                    'obsolete
                    Return ProcessBoolean(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                End If
            Case "ordinal"
                Return ProcessOrdinal(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager)
            Case "datetime", "date_time", "date", "time", "_c_date"
                If TypeOf (ObjNode) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                    Return ProcessDateTime(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Else
                    'obsolete
                    Return ProcessDateTime(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                End If
            Case "quantity_ratio"
                    Return ProcessRatio(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
            Case "countable", "count"
                    Return ProcessCount(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
            Case "interval_count", "interval_quantity"
                    Return ProcessInterval(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager)
            Case "multimedia", "multi_media"
                    Return ProcessMultiMedia(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
            Case "uri"
                    Return New Constraint_URI
            Case "duration"
                    Return ProcessDuration(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
            Case Else
                    Debug.Assert(False)
                    Return New Constraint
        End Select
    End Function

    Private Function ProcessCount(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Count
        Dim ct As New Constraint_Count
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim i As Integer

        If ObjNode.any_allowed Then
            Return ct
        End If

        For i = 1 To ObjNode.attributes.count
            an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "value", "magnitude"
                    Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                    Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER

                    cadlOS = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                    cadlC = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)

                    If Not cadlC.interval.lower_unbounded Then
                        ct.MinimumValue = cadlC.interval.lower
                        ct.IncludeMinimum = cadlC.interval.lower_included
                    End If
                    If Not cadlC.interval.upper_unbounded Then
                        ct.MaximumValue = cadlC.interval.upper
                        ct.IncludeMaximum = cadlC.interval.upper_included
                    Else
                        ct.HasMaximum = False
                    End If
                    If cadlC.has_assumed_value Then
                        ct.AssumedValue = CType(cadlC.assumed_value, openehr.base.kernel.INTEGER_REF).item
                    End If
                Case Else
                    Debug.Assert(False)
            End Select
        Next
        Return ct
    End Function

    Private Function ProcessDuration(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Duration
        Dim duration As New Constraint_Duration
       
        If ObjNode.any_allowed Then
            Return duration
        End If

        Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION
        cadlC = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION)
        duration.AllowableUnits = cadlC.pattern.to_cil
        Return duration
    End Function

    Private Function ProcessRatio(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Ratio
        Dim rat As New Constraint_Ratio
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

        If ObjNode.any_allowed Then
            Return rat
        Else
            For i As Integer = 1 To ObjNode.attributes.count
                an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                Select Case an_attribute.rm_attribute_name.to_cil
                    Case "numerator"
                        rat.Numerator = ProcessCount(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "denominator"
                        rat.Denominator = ProcessCount(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case Else
                        Debug.Assert(False)
                End Select
            Next
        End If

        Return rat

    End Function

    Private Function ProcessDateTime(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_DateTime

        If ObjNode.any_allowed Then
            Return New Constraint_DateTime
        Else
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            For i As Integer = 1 To ObjNode.attributes.count

                an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "value"
                        Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                        If an_attribute.children.count > 0 Then
                            constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                            Return ProcessDateTime(constraint)
                        End If
                End Select
            Next
        End If

        'Shouldn't get to here
        Debug.Assert(False, "Error processing boolean")
        Return New Constraint_DateTime

    End Function

    Private Function ProcessDateTime(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_DateTime
        Dim dt As New Constraint_DateTime
        Dim s As String

        If ObjNode.any_allowed Then
            Return dt
        End If

        Select Case ObjNode.rm_type_name.to_cil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
            Case "DATE_TIME"
                Dim cadlDT As openehr.openehr.am.archetype.constraint_model.primitive.C_DATE_TIME
                cadlDT = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DATE_TIME)
                s = cadlDT.pattern.to_cil

            Case "DATE"
                Dim cadlD As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_DATE
                cadlD = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_DATE)
                s = cadlD.pattern.to_cil

            Case "TIME"
                Dim cadlT As openehr.openehr.am.archetype.constraint_model.primitive.C_TIME
                cadlT = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_TIME)
                s = cadlT.pattern.to_cil

        End Select


        Select Case s.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "yyyy-??-?? ??:??:??", "yyyy-??-??t??:??:??"
                ' Allow all
                dt.TypeofDateTimeConstraint = 11
            Case "yyyy-mm-dd hh:mm:ss", "yyyy-mm-ddthh:mm:ss"
                dt.TypeofDateTimeConstraint = 12
            Case "yyy-mm-dd hh:??:??", "yyy-mm-ddthh:??:??"
                'Partial Date time

                dt.TypeofDateTimeConstraint = 13
            Case "yyyy-??-??"
                'Date only
                dt.TypeofDateTimeConstraint = 14
            Case "yyyy-mm-dd"
                'Full date
                dt.TypeofDateTimeConstraint = 15
            Case "yyyy-??-xx"
                'Partial date
                dt.TypeofDateTimeConstraint = 16
            Case "yyyy-mm-??"
                'Partial date with month
                dt.TypeofDateTimeConstraint = 17
            Case "hh:??:??", "thh:??:??"
                'TimeOnly
                dt.TypeofDateTimeConstraint = 18
            Case "hh:mm:ss", "thh:mm:ss"
                'Full time
                dt.TypeofDateTimeConstraint = 19
            Case "hh:??:xx", "thh:??:xx"
                'Partial time
                dt.TypeofDateTimeConstraint = 20
            Case "hh:mm:??", "thh:mm:??"
                'Partial time with minutes
                dt.TypeofDateTimeConstraint = 21
        End Select

        Return dt

    End Function

    Private Function ProcessOrdinal(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal) As Constraint_Ordinal
        Dim i As Integer
        Dim c_phrase As New CodePhrase
        Dim openehr_ordinal As openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL
        Dim c_value As openehr.openehr.am.openehr_profile.data_types.quantity.C_ORDINAL
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim Ordinals As openehr.base.structures.list.LINKED_LIST_ANY

        If ObjNode.any_allowed Then
            Return New Constraint_Ordinal(True, a_filemanager)
        End If

        Dim ord As New Constraint_Ordinal(a_filemanager)

        For i = 1 To ObjNode.attributes.count

            an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "value"
                    c_value = CType(an_attribute.children.first, openehr.openehr.am.openehr_profile.data_types.quantity.C_ORDINAL)

                    '' first value may have a "?" instead of a code as holder for empty ordinal
                    Ordinals = c_value.items()

                    Ordinals.start()
                    Do While Not Ordinals.off
                        openehr_ordinal = CType(Ordinals.active.item, openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL)

                        Dim newOrdinal As OrdinalValue = ord.OrdinalValues.NewOrdinal

                        newOrdinal.Ordinal = openehr_ordinal.value
                        c_phrase.Phrase = openehr_ordinal.symbol.as_string.to_cil

                        If c_phrase.TerminologyID = "local" Then
                            newOrdinal.InternalCode = c_phrase.FirstCode
                            ord.OrdinalValues.Add(newOrdinal)
                        Else
                            Beep()
                            Debug.Assert(False)
                        End If

                        Ordinals.forth()
                    Loop

                Case "assumed_value"
                    Dim c_assumed_value As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                    Dim c_integer As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER

                    c_assumed_value = CType(an_attribute.children.first(), openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                    c_integer = CType(c_assumed_value.item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)
                    ord.AssumedValue = c_integer.interval.lower

            End Select

        Next

        Return ord
    End Function

    Private Function ProcessBoolean(ByVal ObjSimple As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Boolean
        Dim b As New Constraint_Boolean
        Dim i As Integer

        Dim bool As openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN = _
            CType(ObjSimple.item, openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN)

        If bool.true_valid Then
            i = 1
        End If
        If bool.false_valid Then
            i += 2
        End If
        Select Case i
            Case 1
                b.TrueAllowed = True
            Case 2
                b.FalseAllowed = False
            Case 3
                b.TrueFalseAllowed = False
        End Select

        If bool.has_assumed_value() Then
            b.AssumedValue = CType(bool.assumed_value, openehr.base.kernel.BOOLEAN_REF).item
        End If


        Return b

    End Function

    Private Function ProcessBoolean(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Boolean

        If ObjNode.any_allowed Then
            Return New Constraint_Boolean
        Else
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            For i As Integer = 1 To ObjNode.attributes.count

                an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "value"
                        Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                        If an_attribute.children.count > 0 Then
                            constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                            Return ProcessBoolean(constraint)
                        End If
                End Select
            Next
        End If

        'Shouldn't get to here
        Debug.Assert(False, "Error processing boolean")
        Return New Constraint_Boolean

    End Function

    Shared Function ProcessText(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Text
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim t As New Constraint_Text
        Dim i As Integer

        If ObjNode.any_allowed Then
            t.TypeOfTextConstraint = TextConstrainType.Text
            Return t
        End If

        For i = 1 To ObjNode.attributes.count

            an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "code"
                    'coded - could be a constraint or the actual codes so..
                    Dim cp As CodePhrase
                    Dim Obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT

                    Obj = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)

                    Select Case Obj.generating_type.to_cil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
                        Case "CONSTRAINT_REF"
                            t.TypeOfTextConstraint = TextConstrainType.Terminology
                            t.ConstraintCode = CType(Obj, openehr.openehr.am.archetype.constraint_model.CONSTRAINT_REF).target.to_cil
                        Case "C_CODED_TERM"
                            t.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.ProcessCodes(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODED_TERM))

                            If CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODED_TERM).has_assumed_value Then
                                t.AssumedValue = CType(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODED_TERM).assumed_value, openehr.base.kernel.STRING).to_cil
                            End If

                            If t.AllowableValues.TerminologyID = "local" Then
                                t.TypeOfTextConstraint = TextConstrainType.Internal
                            End If
                    End Select


                Case "value"
                    Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                    Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING
                    Dim s As String
                    Dim EIF_String As openehr.base.kernel.STRING
                    Dim ii As Integer

                    t.TypeOfTextConstraint = TextConstrainType.Text

                    If an_attribute.children.count > 0 Then
                        constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                        cString = CType(constraint.Item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING)
                        For ii = 1 To cString.Strings.Count
                            EIF_String = CType(cString.Strings.i_th(ii), openehr.base.kernel.STRING)
                            t.AllowableValues.Codes.Add(EIF_String.to_cil)
                        Next
                    End If

                    'redundant by June 2006
                Case "assumed_value"
                    Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                    Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING
                    Dim s As String
                    Dim EIF_String As openehr.base.kernel.STRING
                    Dim ii As Integer

                    If an_attribute.children.count > 0 Then
                        constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                        cString = CType(constraint.Item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING)
                        EIF_String = CType(cString.Strings.First, openehr.base.kernel.STRING)
                        t.AssumedValue = EIF_String.to_cil
                    End If

                Case "mapping"
                    Debug.Assert(False)
                    ''fix me - need to deal with mappings
            End Select
        Next
        Return t

    End Function

    Private Function ProcessQuantity(ByVal ObjNode As openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY) As Constraint_Quantity
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim q As New Constraint_Quantity
        Dim u As Constraint_QuantityUnit
        Dim i As Integer

        If ObjNode.any_allowed Then
            Return q
        End If

        If Not ObjNode.property Is Nothing Then

            Dim s As String = ObjNode.property.code_string.to_cil

            If IsNumeric(s) Then
                q.OpenEhrCode = Integer.Parse(s)
            Else
                'OBSOLETE - to cope with physical properties
                q.PhysicalPropertyAsString = s
            End If

            If ObjNode.list.count > 0 Then
                Dim cqi As openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM

                For i = 1 To ObjNode.list.count
                    'Do not set attribute to true here as there
                    'is no special handling of time units until Unit is set
                    u = New Constraint_QuantityUnit(q.IsTime)
                    cqi = CType(ObjNode.list.i_th(i), openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM)

                    u.Unit = cqi.units.to_cil

                    If Not cqi.any_magnitude_allowed Then
                        u.HasMaximum = Not cqi.magnitude.upper_unbounded
                        If u.HasMaximum Then
                            u.MaximumValue = CSng(cqi.magnitude.upper)
                            u.IncludeMaximum = cqi.magnitude.upper_included
                        End If
                        u.HasMinimum = Not cqi.magnitude.lower_unbounded
                        If u.HasMinimum Then
                            u.MinimumValue = CSng(cqi.magnitude.lower)
                            u.IncludeMinimum = cqi.magnitude.lower_included
                        End If
                    End If
                    ' need to add with key for retrieval
                    q.Units.Add(u, u.Unit)
                Next

            End If

            If ObjNode.has_assumed_value Then
                Dim assumed_units As String = CType(ObjNode.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.QUANTITY).units.to_cil
                Dim assumed_value As Single = CType(ObjNode.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.QUANTITY).magnitude
                CType(q.Units.Item(assumed_units), Constraint_QuantityUnit).AssumedValue = assumed_value
            End If

        End If

        Return q

    End Function

#End Region

#Region "Building ADL - outgoing"

#End Region

#End Region

End Class

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
'The Original Code is RmElement.vb.
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
