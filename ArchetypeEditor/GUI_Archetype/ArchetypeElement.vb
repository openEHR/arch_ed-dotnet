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

Public Class ArchetypeElement : Inherits ArchetypeNodeAbstract

    Private Property Element() As RmElement
        Get
            Return CType(Me.Item, RmElement)
        End Get
        Set(ByVal Value As RmElement)
            Me.Item = Value
        End Set
    End Property
    Public Overrides ReadOnly Property IsReference() As Boolean
        Get
            Return Me.Element.isReference
        End Get
    End Property
    Public Overrides ReadOnly Property HasReferences() As Boolean

        Get
            Return Me.Element.hasReferences
        End Get
    End Property
    Public Shadows ReadOnly Property RM_Class() As RmElement
        Get
            Debug.Assert(TypeOf MyBase.RM_Class Is RmElement)
            Return CType(MyBase.RM_Class, RmElement)
        End Get
    End Property
    Public Property Constraint() As Constraint
        Get
            Return Me.Element.Constraint
        End Get
        Set(ByVal Value As Constraint)
            Me.Element.Constraint = Value
        End Set
    End Property
    Public ReadOnly Property DataType() As String

        Get
            Return Me.Element.DataType
        End Get
    End Property

    Public Overrides Function Copy() As ArchetypeNode
        Return New ArchetypeElement(CType(Me.Element.Copy, RmElement), mFileManager)
    End Function

    Private Function TextConstraintToRichText(ByVal TextConstraint As Constraint_Text) As String
        Dim s As String
        Dim punctuation As Char() = {CType(".", Char), CType(",", Char)}
        Dim a_Term As RmTerm

        s = TextConstraint.TypeOfTextConstraint.ToString & ";"
        Select Case TextConstraint.TypeOfTextConstraint
            Case TextConstrainType.Text
                Dim a_string As String
                If TextConstraint.AllowableValues.Codes.Count > 0 Then
                    For Each a_string In TextConstraint.AllowableValues.Codes 'JAR: 13APR07, EDT-32 Support unicode
                        's = s & " '" & a_string & "',"                        
                        s = s & " '" & RichTextBoxUnicode.CreateRichTextBoxTag(a_string, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_TEXT) & "'," 'JAR: 13APR07, EDT-32 Support unicode

                    Next
                    s.TrimEnd(punctuation)
                End If
            Case TextConstrainType.Internal
                If TextConstraint.AllowableValues.Codes.Count > 1 Then
                    Dim a_string As String
                    For Each a_string In TextConstraint.AllowableValues.Codes
                        'a_Term = mFileManager.OntologyManager.GetTerm(a_string)
                        's = s & " '" & a_Term.Text & "',"                        
                        s = s & " '" & RichTextBoxUnicode.CreateRichTextBoxTag(a_string, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_TEXT) & "'," 'JAR: 13APR07, EDT-32 Support unicode

                    Next
                    s = s.TrimEnd(punctuation)
                End If
            Case TextConstrainType.Terminology
                a_Term = mFileManager.OntologyManager.GetTerm(TextConstraint.ConstraintCode)
                s = s & " " & a_Term.Text
        End Select

        Return s.Trim

    End Function

    Private Function QuantityConstraintToRichText(ByVal q As Constraint_Quantity, ByVal level As Integer) As String
        Dim Text As String
        Dim u As Constraint_QuantityUnit

        If q.IsCoded Then
            Text = (Space(3 * level) & "  Constraint: Physical property = " & _
            Filemanager.GetOpenEhrTerm(q.OpenEhrCode, q.PhysicalPropertyAsString) & ";" & "\par")
        Else
            Text = (Space(3 * level) & "  Constraint: Physical property = " & _
                q.PhysicalPropertyAsString & ";" & "\par")
        End If

        For Each u In q.Units
            Text &= Environment.NewLine & (Space(4 * level) & QuantityUnitConstraintToRichText(u) & "\par")
        Next
        Return Text
    End Function

    Private Function QuantityConstraintToHTML(ByVal q As Constraint_Quantity) As String
        Dim result As String

        If q.IsCoded Then
            result = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & _
                Filemanager.GetOpenEhrTerm(q.OpenEhrCode, q.PhysicalPropertyAsString) & "<br>"
        Else
            result = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & _
                q.PhysicalPropertyAsString & "<br>"
        End If

        For Each u As Constraint_QuantityUnit In q.Units
            result &= Environment.NewLine & QuantityUnitConstraintToRichText(u) & "<br>"
        Next
        Return result
    End Function

    Private Function DateTimeIntervalConstraintToHTML(ByVal dtInterval As Constraint_Interval_DateTime) As String
        Dim result, s As String
        Dim constraintDt As Constraint_DateTime
        result = ""
        constraintDt = CType(dtInterval.LowerLimit, Constraint_DateTime)
        s = Filemanager.GetOpenEhrTerm(constraintDt.TypeofDateTimeConstraint, "not known")


        result &= Environment.NewLine & AE_Constants.Instance.Lower & ": " & s & "<br>"

        constraintDt = CType(dtInterval.UpperLimit, Constraint_DateTime)
        s = Filemanager.GetOpenEhrTerm(constraintDt.TypeofDateTimeConstraint, "not known")

        result &= Environment.NewLine & AE_Constants.Instance.Upper & ": " & s & "<br>"
        Return result
    End Function


    Private Function QuantityIntervalConstraintToHTML(ByVal q As Constraint_Interval_Quantity) As String
        Dim result As String

        result = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & _
                Filemanager.GetOpenEhrTerm(q.QuantityPropertyCode, CType(q.LowerLimit, Constraint_Quantity).PhysicalPropertyAsString) & "<br>"

        Dim u As Constraint_QuantityUnit
        result &= Environment.NewLine & AE_Constants.Instance.Lower & ": <br>"
        For Each u In CType(q.LowerLimit, Constraint_Quantity).Units
            result &= QuantityUnitConstraintToRichText(u) & "<br>"
        Next
        result &= Environment.NewLine & AE_Constants.Instance.Upper & ": <br>"
        For Each u In CType(q.UpperLimit, Constraint_Quantity).Units
            result &= QuantityUnitConstraintToRichText(u) & "<br>"
        Next

        Return result
    End Function

    Private Function QuantityUnitConstraintToRichText(ByVal u As Constraint_QuantityUnit) As String
        Dim s As String = ""

        If u.Unit <> "" Then
            s = "  Units = " & u.ToString & ";"
        End If
        If u.HasMinimum Then
            If u.IncludeMinimum Then
                s &= " >="
            Else
                s &= " >"
            End If
            s &= u.MinimumValue.ToString & ";"
        End If
        If u.HasMaximum Then
            If u.IncludeMaximum Then
                s &= " <="
            Else
                s &= " <"
            End If
            s &= u.MaximumValue.ToString & ";"
        End If

        Return s

    End Function

    Private Function CountConstraintToRichText(ByVal CountConstraint As Constraint_Count) As String
        Dim s As String
        s = ""

        If CountConstraint.HasMinimum Then
            If Not CountConstraint.IncludeMinimum Then
                s &= " >"
            End If
            s &= CountConstraint.MinimumValue.ToString & ".."
        End If
        If CountConstraint.HasMaximum Then
            If Not CountConstraint.IncludeMaximum Then
                s &= "<"
            Else
                If Not CountConstraint.HasMinimum Then
                    s &= "<="
                End If
            End If
            s &= CountConstraint.MaximumValue.ToString
        Else
            s &= "*"
        End If
        Return s.Trim

    End Function

    Private Function DurationConstraintToRichText(ByVal durationConstraint As Constraint_Duration) As String
        Dim s As String
        s = mFileManager.OntologyManager.GetOpenEHRTerm(117, "Units") & ": "
        If durationConstraint.AllowableUnits = "" Then
            Return s & "*"
        Else
            Dim time As Boolean = False
            Dim iso As String = ""
            For Each c As Char In durationConstraint.AllowableUnits

                If c <> "P"c Then
                    If c = "T"c Then
                        time = True
                    Else
                        Select Case c
                            Case "Y"c
                                iso = "a"
                            Case "M"c
                                If time Then
                                    iso = "min"
                                Else
                                    iso = "mo"
                                End If
                            Case "W"c
                                iso = "wk"
                            Case Else
                                iso = c.ToString().ToLowerInvariant()
                        End Select

                        If iso <> "" Then
                            s &= OceanArchetypeEditor.ISO_TimeUnits.GetLanguageForISO(iso) & ", "
                        End If
                    End If
                End If
            Next
            Return s.Trim(", ".ToCharArray)
        End If
    End Function


    Private Function OrdinalConstraintToRichText(ByVal OrdinalConstraint As Constraint_Ordinal) As String
        Dim s As String
        Dim ov As OrdinalValue
        'Dim a_Term As RmTerm

        s = "{"
        For Each ov In OrdinalConstraint.OrdinalValues
            'a_Term = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            's = s & ov.Ordinal.ToString & ": \i " & a_Term.Text & "\i0 ; "
            s = s & ov.Ordinal.ToString & ": \i " & RichTextBoxUnicode.CreateRichTextBoxTag(ov.InternalCode, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_TEXT) & "\i0 ; " 'JAR: 13APR07, EDT-32 Support unicode
        Next
        s = s.Trim & "}"
        Return s

    End Function

    Private Function OrdinalConstraintToHTML(ByVal OrdinalConstraint As Constraint_Ordinal) As String
        Dim s As String
        Dim ov As OrdinalValue
        Dim a_Term As RmTerm

        s = ""
        For Each ov In OrdinalConstraint.OrdinalValues
            a_Term = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            s &= ov.Ordinal.ToString & ": <i> " & a_Term.Text & "<i><br> "
        Next
        s = s.Trim
        Return s

    End Function

    Private Function ProportionConstraintToHTML(ByVal RatioConstraint As Constraint_Proportion) As String
        Dim s As String

        s = CountConstraintToRichText(CType(RatioConstraint.Numerator, Constraint_Count))
        If RatioConstraint.IsPercent Then
            s &= " %"
        Else
            s &= " : " & CountConstraintToRichText(CType(RatioConstraint.Denominator, Constraint_Count))
        End If

        Return s

    End Function

    Private Function IntervalCountToHTML(ByVal intervalCountConstraint As Constraint_Interval_Count) As String
        Dim s As String
        s = AE_Constants.Instance.Upper & ": "
        s += CountConstraintToRichText(CType(intervalCountConstraint.UpperLimit, Constraint_Count))
        s += ", " & AE_Constants.Instance.Lower & ": "
        s += CountConstraintToRichText(CType(intervalCountConstraint.LowerLimit, Constraint_Count))
        Return s

    End Function

    Private Function SlotToHTML(ByVal a_slot_constraint As Constraint_Slot) As String
        Dim result As String = ""

        If a_slot_constraint.hasSlots Then
            If a_slot_constraint.IncludeAll Then
                result &= Environment.NewLine & mFileManager.OntologyManager.GetOpenEHRTerm(628, "Include all") & "<br>"
            ElseIf a_slot_constraint.Include.Count > 0 Then
                result &= Environment.NewLine & mFileManager.OntologyManager.GetOpenEHRTerm(625, "Include") & ": <br><ul>"
                For Each slot As String In a_slot_constraint.Include
                    result &= Environment.NewLine & "<li>" & slot & "</li>"
                Next
                result &= Environment.NewLine & "</ul><br>"
            End If
            If a_slot_constraint.ExcludeAll Then
                result &= Environment.NewLine & mFileManager.OntologyManager.GetOpenEHRTerm(629, "Exclude all") & "<br>"
            ElseIf a_slot_constraint.Exclude.Count > 0 Then
                result &= Environment.NewLine & mFileManager.OntologyManager.GetOpenEHRTerm(626, "Exclude") & ": <br><ul>"
                For Each slot As String In a_slot_constraint.Exclude
                    result &= Environment.NewLine & "<li>" & slot & "</li>"
                Next
                result &= Environment.NewLine & "</ul><br>"
            End If
        Else
            result &= Environment.NewLine & mFileManager.OntologyManager.GetOpenEHRTerm(628, "Include all") & "<br>"
        End If
        result &= "<br>"
        Return result
    End Function

    Private Function IntervalQuantityToHTML(ByVal intervalQuantityConstraint As Constraint_Interval_Quantity) As String
        Dim s As String

        s = QuantityIntervalConstraintToHTML(intervalQuantityConstraint)
        Return s

    End Function

    Private Function IntervalDateTimeToHTML(ByVal intervalDateTimeConstraint As Constraint_Interval_DateTime) As String
        Dim s As String

        s = DateTimeIntervalConstraintToHTML(intervalDateTimeConstraint)
        Return s

    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String

        ' write the cardinality of the element
        'Dim s1 As String = mText & " (" & mItem.Occurrences.ToString & ")"        
        Dim s1 As String = RichTextBoxUnicode.CreateRichTextBoxTag(NodeId, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_TEXT) & " (" & mItem.Occurrences.ToString & ")"  ''JAR: 13APR07, EDT-32 Support unicode

        ' add bars if table and wrapping text
        Dim result As String = (Space(3 * level) & "\b " & s1 & "\b0\par")

        'write the description of the element
        'Dim s As String = " " & mDescription        
        Dim s As String = RichTextBoxUnicode.CreateRichTextBoxTag(NodeId, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_DESC) 'JAR: 13APR07, EDT-32 Support unicode

        result = result & Environment.NewLine & (Space(3 * level) & s & "\par")

        result &= Environment.NewLine & (Space(3 * level) & "  DataType = " _
                & Me.Element.Constraint.ConstraintTypeString & "\par")

        result &= ConstraintToRichText(Me.Element.Constraint, level)
        result &= Environment.NewLine & ("\par")

        Return result

    End Function

    Private Function DateTimeConstraintToRichText(ByVal a_date_time_constraint As Constraint_DateTime) As String
        Dim result As String = ""
        result &= Filemanager.GetOpenEhrTerm(a_date_time_constraint.TypeofDateTimeConstraint, "not known")
        Return result
    End Function

    Private Function ConstraintToRichText(ByVal a_constraint As Constraint, ByVal level As Integer) As String
        Dim result As String = ""
        Select Case a_constraint.Type

            Case ConstraintType.Quantity

                result &= Environment.NewLine _
                        & QuantityConstraintToRichText(CType(a_constraint, Constraint_Quantity), level)

            Case ConstraintType.Count

                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & CountConstraintToRichText(CType(a_constraint, Constraint_Count)) & "\par")

            Case ConstraintType.Text

                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & TextConstraintToRichText(CType(a_constraint, Constraint_Text)) & "\par")

            Case ConstraintType.Boolean
                Dim b As Constraint_Boolean
                Dim s As String = ""

                b = CType(a_constraint, Constraint_Boolean)
                If b.TrueFalseAllowed Then
                    s = "*"
                ElseIf b.TrueAllowed Then
                    s = Boolean.TrueString
                Else
                    s = Boolean.FalseString
                End If

                If b.hasAssumedValue Then
                    s &= "; " & mFileManager.OntologyManager.GetOpenEHRTerm(158, "Assumed value") & " = " & b.AssumedValue.ToString
                End If
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & s & "\par")

            Case ConstraintType.DateTime
                Dim dt As Constraint_DateTime
                dt = CType(a_constraint, Constraint_DateTime)
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & DateTimeConstraintToRichText(dt) & "\par")

            Case ConstraintType.Ordinal
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & OrdinalConstraintToRichText(CType(a_constraint, Constraint_Ordinal)) & "\par")

            Case ConstraintType.Duration
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & DurationConstraintToRichText(CType(a_constraint, Constraint_Duration)) & "\par")

            Case ConstraintType.Any
                ' add nothing

            Case ConstraintType.Multiple
                For Each c As Constraint In CType(a_constraint, Constraint_Choice).Constraints
                    result &= ConstraintToRichText(c, level + 1)
                Next

            Case ConstraintType.MultiMedia
                'add nothing

            Case ConstraintType.URI
                'add nothing

            Case ConstraintType.Interval_Count
                Dim cic As Constraint_Interval_Count = CType(a_constraint, Constraint_Interval_Count)
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Upper & ": "
                result &= CountConstraintToRichText(CType(cic.UpperLimit, Constraint_Count))
                result &= ", " & AE_Constants.Instance.Lower & ": "
                result &= CountConstraintToRichText(CType(cic.LowerLimit, Constraint_Count))
                result &= "\par"

            Case ConstraintType.Interval_DateTime
                Dim cidt As Constraint_Interval_DateTime = CType(a_constraint, Constraint_Interval_DateTime)
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Upper & ": "
                result &= DateTimeConstraintToRichText(CType(cidt.UpperLimit, Constraint_DateTime))
                result &= ", " & AE_Constants.Instance.Lower & ": "
                result &= DateTimeConstraintToRichText(CType(cidt.LowerLimit, Constraint_DateTime))
                result &= "\par"

            Case ConstraintType.Interval_Quantity
                Dim ciq As Constraint_Interval_Count = CType(a_constraint, Constraint_Interval_Quantity)
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Upper & ": \par"
                result &= Environment.NewLine & QuantityConstraintToRichText(CType(ciq.UpperLimit, Constraint_Quantity), level + 1) & "\par"
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Lower & ": \par"
                result &= Environment.NewLine & QuantityConstraintToRichText(CType(ciq.LowerLimit, Constraint_Quantity), level + 1) & "\par"


            Case ConstraintType.Proportion
                Dim cp As Constraint_Proportion = CType(a_constraint, Constraint_Proportion)
                result &= Environment.NewLine & Space(3 * level)
                result &= CountConstraintToRichText(CType(cp.Numerator, Constraint_Count))
                If cp.IsPercent Then
                    result &= "%"
                ElseIf cp.IsUnitary Then
                    result &= " (" & mFileManager.OntologyManager.GetOpenEHRTerm(644, "Unitary") & ")"
                Else
                    result &= ": "
                    result &= CountConstraintToRichText(CType(cp.Denominator, Constraint_Count))
                End If

                If cp.IsIntegral Then
                    result &= " (" & mFileManager.OntologyManager.GetOpenEHRTerm(643, "Integral") & ")"
                End If

                result &= "\par"

            Case ConstraintType.Slot
                Dim cSlot As Constraint_Slot = CType(a_constraint, Constraint_Slot)
                If cSlot.hasSlots Then
                    If cSlot.IncludeAll Then
                        result &= Environment.NewLine & Space(3 * level) & mFileManager.OntologyManager.GetOpenEHRTerm(628, "Include all") & "\par"
                    ElseIf cSlot.Include.Count > 0 Then
                        result &= Environment.NewLine & Space(3 * level) & mFileManager.OntologyManager.GetOpenEHRTerm(625, "Include") & ": \par"
                        For Each slot As String In cSlot.Include
                            result &= Environment.NewLine & Space(3 * (level + 1)) & slot & "\par"
                        Next
                    End If
                    If cSlot.ExcludeAll Then
                        result &= Environment.NewLine & Space(3 * level) & mFileManager.OntologyManager.GetOpenEHRTerm(629, "Exclude all") & "\par"
                    ElseIf cSlot.Exclude.Count > 0 Then
                        result &= Environment.NewLine & Space(3 * level) & mFileManager.OntologyManager.GetOpenEHRTerm(626, "Exclude") & ": \par"
                        For Each slot As String In cSlot.Exclude
                            result &= Environment.NewLine & Space(3 * (level + 1)) & slot & "\par"
                        Next
                    End If
                Else
                    result &= Environment.NewLine & Space(3 * level) & mFileManager.OntologyManager.GetOpenEHRTerm(628, "Include all") & "\par"
                End If
                result &= "\par"

            Case Else
                Debug.Assert(False, a_constraint.Type.ToString & " not handled")

        End Select
        Return result
    End Function

    Private Structure HTML_Details
        Dim ImageSource As String
        Dim HTML As String
        Dim TerminologyCode As String
    End Structure

    Private Function GetHtmlDetails(ByVal c As Constraint) As HTML_Details

        Dim html_dt As HTML_Details = New HTML_Details()

        html_dt.HTML = ""
        html_dt.ImageSource = ""
        html_dt.TerminologyCode = ""

        Select Case c.Type

            Case ConstraintType.Multiple
                Dim first As Boolean = True

                For Each cc As Constraint In CType(c, Constraint_Choice).Constraints
                    If first Then
                        html_dt.HTML &= GetHtmlDetails(cc).HTML
                        first = False
                    Else
                        html_dt.HTML &= "<hr>" + GetHtmlDetails(cc).HTML
                    End If
                Next
                html_dt.ImageSource = "Images/choice.gif"

            Case ConstraintType.Any
                html_dt.HTML = Environment.NewLine & "&nbsp;"
                html_dt.ImageSource = "Images/any.gif"

            Case ConstraintType.Quantity

                html_dt.HTML = Environment.NewLine & QuantityConstraintToHTML(CType(c, Constraint_Quantity))
                html_dt.ImageSource = "Images/quantity.gif"

            Case ConstraintType.Count

                html_dt.HTML = Environment.NewLine & CountConstraintToRichText(CType(c, Constraint_Count))
                html_dt.ImageSource = "Images/count.gif"

            Case ConstraintType.Text

                html_dt.HTML = Environment.NewLine & TextConstraintToRichText(CType(c, Constraint_Text))
                html_dt.ImageSource = "Images/text.gif"


            Case ConstraintType.Boolean
                Dim b As Constraint_Boolean

                b = CType(c, Constraint_Boolean)

                html_dt.HTML = Environment.NewLine

                If b.TrueFalseAllowed Then
                    html_dt.HTML &= Boolean.TrueString & ", " & Boolean.FalseString
                ElseIf b.TrueAllowed Then
                    html_dt.HTML &= Boolean.TrueString
                Else
                    html_dt.HTML &= Boolean.FalseString
                End If

                If b.hasAssumedValue Then
                    html_dt.HTML &= Filemanager.GetOpenEhrTerm(158, "Assumed value:") & " " & b.AssumedValue.ToString
                End If

                html_dt.ImageSource = "Images/truefalse.gif"

            Case ConstraintType.DateTime
                Dim dt As Constraint_DateTime
                dt = CType(c, Constraint_DateTime)
                html_dt.HTML = Environment.NewLine & Filemanager.GetOpenEhrTerm(dt.TypeofDateTimeConstraint, "not known")

                html_dt.ImageSource = "Images/datetime.gif"

            Case ConstraintType.Ordinal
                html_dt.HTML = Environment.NewLine & OrdinalConstraintToHTML(CType(c, Constraint_Ordinal))

                html_dt.ImageSource = "Images/ordinal.gif"

            Case ConstraintType.URI
                html_dt.HTML = Environment.NewLine & "&nbsp;"
                html_dt.ImageSource = "Images/uri.gif"

            Case ConstraintType.Proportion
                html_dt.HTML = Environment.NewLine & ProportionConstraintToHTML(CType(c, Constraint_Proportion))
                html_dt.ImageSource = "Images/ratio.gif"

            Case ConstraintType.Duration
                html_dt.HTML = Environment.NewLine & DurationConstraintToRichText(CType(c, Constraint_Duration))
                html_dt.ImageSource = "Images/duration.gif"

            Case ConstraintType.Interval_Count
                html_dt.HTML = Environment.NewLine & IntervalCountToHTML(CType(c, Constraint_Interval_Count))
                html_dt.ImageSource = "Images/interval.gif"

            Case ConstraintType.Interval_Quantity
                html_dt.HTML = Environment.NewLine & IntervalQuantityToHTML(CType(c, Constraint_Interval_Quantity))
                html_dt.ImageSource = "Images/interval.gif"

            Case ConstraintType.Interval_DateTime
                html_dt.HTML = Environment.NewLine & IntervalDateTimeToHTML(CType(c, Constraint_Interval_DateTime))
                html_dt.ImageSource = "Images/interval.gif"

            Case ConstraintType.MultiMedia
                html_dt.HTML = ""
                html_dt.ImageSource = "Images/multimedia.gif"

            Case ConstraintType.Slot

                html_dt.HTML = Environment.NewLine & SlotToHTML(CType(c, Constraint_Slot))
                html_dt.ImageSource = "Images/slot.gif"

            Case Else
                Debug.WriteLine(c.Type.ToString)
                Debug.Assert(False, "Not handled")
        End Select

        Return html_dt


    End Function


    Public Overrides Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String

        ' write the cardinality of the element
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("<tr>")
        Dim class_names As String = ""
        Dim html_dt As HTML_Details = GetHtmlDetails(Me.Element.Constraint)
        Dim terminologyCode As String

        If OceanArchetypeEditor.Instance.Options.ShowTermsInHtml Then
            For Each terminologyRow As DataRow In mFileManager.OntologyManager.TerminologiesTable.Rows
                terminologyCode = CStr(terminologyRow.Item(0))
                If mFileManager.OntologyManager.Ontology.HasTermBinding(terminologyCode, Me.NodeId) Then
                    html_dt.TerminologyCode = "<br>" & terminologyCode & ": " & mFileManager.OntologyManager.Ontology.TermBinding(terminologyCode, Me.NodeId)
                End If
            Next
        End If

        If Me.Element.Constraint.Type = ConstraintType.Multiple Then
            Dim first As Boolean = True

            For Each c As Constraint In CType(Me.Element.Constraint, Constraint_Choice).Constraints
                If first Then
                    first = False
                    class_names &= c.Type.ToString
                Else
                    class_names &= "<hr>" & c.Type.ToString
                End If
            Next

        Else
            class_names = Me.Element.Constraint.ConstraintTypeString
        End If

        result.AppendFormat("{0}<td><table><tr><td width=""{1}""></td><td><img border=""0"" src=""{2}"" width=""32"" height=""32"" align=""middle""><b>{3}</b></td></table></td>", Environment.NewLine, (level * 20).ToString, html_dt.ImageSource, mText)
        result.AppendFormat("{0}<td>{1}{2}</td>", Environment.NewLine, mDescription, html_dt.TerminologyCode)
        result.AppendFormat("{0}<td><b><i>{1}</i></b><br>", Environment.NewLine, class_names)
        result.AppendFormat("{0}{1}</td>", Environment.NewLine, mItem.Occurrences.ToString)
        result.AppendFormat("{0}<td>{1}", Environment.NewLine, html_dt.HTML)
        result.AppendFormat("{0}</td>", Environment.NewLine)
        If showComments Then
            Dim s As String = Me.Comment
            If s = "" Then
                s = "&nbsp;"
            End If
            result.AppendFormat("{0}<td>{1}</td>", Environment.NewLine, s)
        End If

        Return result.ToString

    End Function


    Overrides Function ToString() As String
        Return mText
    End Function

    Public Sub New(ByVal aText As String, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(aText)
        mFileManager = a_file_manager
        Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(aText)
        Me.Element = New RmElement(aTerm.Code)
    End Sub

    Public Sub New(ByVal aElement As RmElement, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(aElement, a_file_manager)
    End Sub

    Public Sub New(ByVal aElementNode As ArchetypeElement)
        MyBase.New(aElementNode)
    End Sub

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
'The Original Code is ArchetypeElement.vb.
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
