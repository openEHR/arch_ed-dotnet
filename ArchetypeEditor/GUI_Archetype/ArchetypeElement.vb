'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
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
            Return CType(Item, RmElement)
        End Get
        Set(ByVal Value As RmElement)
            Item = Value
        End Set
    End Property

    Public Overrides ReadOnly Property IsReference() As Boolean
        Get
            Return Element.IsReference
        End Get
    End Property

    Public Overrides ReadOnly Property HasReferences() As Boolean
        Get
            Return Element.HasReferences
        End Get
    End Property

    Public Shadows ReadOnly Property RM_Class() As RmElement
        Get
            Debug.Assert(TypeOf MyBase.RM_Class Is RmElement)
            Return CType(MyBase.RM_Class, RmElement)
        End Get
    End Property

    Public Overrides Property Constraint() As Constraint
        Get
            Return Element.Constraint
        End Get
        Set(ByVal value As Constraint)
            Element.Constraint = value
        End Set
    End Property

    Public ReadOnly Property DataType() As String
        Get
            Return Element.DataType
        End Get
    End Property

    Public Overrides ReadOnly Property CanChangeDataType() As Boolean
        Get
            Return Not IsReference And (CanRemove Or (Not CanSpecialise And Constraint.Kind = ConstraintKind.Any))
        End Get
    End Property

    Public Overrides Function Copy() As ArchetypeNode
        Return New ArchetypeElement(CType(Element.Copy, RmElement), mFileManager)
    End Function

    Private Function TextConstraintToRichText(ByVal TextConstraint As Constraint_Text) As String
        Dim punctuation As Char() = {CType(".", Char), CType(",", Char)}
        Dim term As RmTerm
        Dim s As String = TextConstraint.TypeOfTextConstraint.ToString & ";"

        Select Case TextConstraint.TypeOfTextConstraint
            Case TextConstraintType.Text
                Dim a_string As String

                If TextConstraint.AllowableValues.Codes.Count > 0 Then
                    For Each a_string In TextConstraint.AllowableValues.Codes
                        s = s & " '" & a_string & "',"
                    Next

                    s.TrimEnd(punctuation)
                End If
            Case TextConstraintType.Internal
                If TextConstraint.AllowableValues.Codes.Count > 1 Then
                    Dim a_string As String

                    For Each a_string In TextConstraint.AllowableValues.Codes
                        s = s & " '" & RichTextBoxUnicode.EscapedRtfString(mFileManager.OntologyManager.GetText(a_string)) & "',"
                    Next

                    s = s.TrimEnd(punctuation)
                End If
            Case TextConstraintType.Terminology
                term = mFileManager.OntologyManager.GetTerm(TextConstraint.ConstraintCode)
                s = s & " " & term.Text
        End Select

        Return s.Trim
    End Function

    Private Function QuantityConstraintToRichText(ByVal q As Constraint_Quantity, ByVal level As Integer) As String
        Dim result As String
        Dim u As Constraint_QuantityUnit

        If q.IsCoded Then
            result = (Space(3 * level) & "  Constraint: Physical property = " & Filemanager.GetOpenEhrTerm(q.OpenEhrCode, q.PhysicalPropertyAsString) & ";" & "\par")
        Else
            result = (Space(3 * level) & "  Constraint: Physical property = " & q.PhysicalPropertyAsString & ";" & "\par")
        End If

        For Each u In q.Units
            result &= Environment.NewLine & (Space(4 * level) & QuantityUnitConstraintToRichText(u) & "\par")
        Next

        Return result
    End Function

    Private Function QuantityConstraintToHTML(ByVal q As Constraint_Quantity) As String
        Dim result As String

        If q.IsCoded Then
            result = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & Filemanager.GetOpenEhrTerm(q.OpenEhrCode, q.PhysicalPropertyAsString) & "<br>"
        Else
            result = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & q.PhysicalPropertyAsString & "<br>"
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
        Dim result As String = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & Filemanager.GetOpenEhrTerm(q.QuantityPropertyCode, CType(q.LowerLimit, Constraint_Quantity).PhysicalPropertyAsString) & "<br>"

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

            s &= u.MinimumRealValue.ToString & ";"
        End If

        If u.HasMaximum Then
            If u.IncludeMaximum Then
                s &= " <="
            Else
                s &= " <"
            End If

            s &= u.MaximumRealValue.ToString & ";"
        End If

        Return s

    End Function

    Private Function CountConstraintToRichText(ByVal CountConstraint As Constraint_Count) As String
        Dim s As String = ""

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
                            s &= Main.ISO_TimeUnits.GetLanguageForISO(iso) & ", "
                        End If
                    End If
                End If
            Next

            Return s.Trim(", ".ToCharArray)
        End If
    End Function

    Private Function OrdinalConstraintToRichText(ByVal OrdinalConstraint As Constraint_Ordinal) As String
        Dim s As String = "{"

        For Each ov As OrdinalValue In OrdinalConstraint.OrdinalValues
            s = s & ov.Ordinal.ToString & ": \i " & RichTextBoxUnicode.EscapedRtfString(mFileManager.OntologyManager.GetText(ov.InternalCode)) & "\i0 ; "
        Next

        Return s.Trim & "}"
    End Function

    Private Function OrdinalConstraintToHTML(ByVal OrdinalConstraint As Constraint_Ordinal) As String
        Dim s As String = ""

        For Each ov As OrdinalValue In OrdinalConstraint.OrdinalValues
            Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            s &= ov.Ordinal.ToString & ": <i> " & term.Text & "<i><br> "
        Next

        Return s.Trim
    End Function

    Private Function ProportionConstraintToHTML(ByVal RatioConstraint As Constraint_Proportion) As String
        Dim s As String = CountConstraintToRichText(CType(RatioConstraint.Numerator, Constraint_Count))

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
        Return QuantityIntervalConstraintToHTML(intervalQuantityConstraint)
    End Function

    Private Function IntervalDateTimeToHTML(ByVal intervalDateTimeConstraint As Constraint_Interval_DateTime) As String
        Return DateTimeIntervalConstraintToHTML(intervalDateTimeConstraint)
    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        ' write the cardinality of the element
        Dim s1 As String = RichTextBoxUnicode.EscapedRtfString(mFileManager.OntologyManager.GetText(NodeId)) & " (" & mItem.Occurrences.ToString & ")"

        ' add bars if table and wrapping text
        Dim result As String = (Space(3 * level) & "\b " & s1 & "\b0\par")

        'write the description of the element
        Dim s As String = RichTextBoxUnicode.EscapedRtfString(mFileManager.OntologyManager.GetText(NodeId))
        result = result & Environment.NewLine & (Space(3 * level) & s & "\par")

        If Not Element.Constraint Is Nothing Then
            result &= Environment.NewLine & (Space(3 * level) & "  DataType = " & Element.Constraint.ConstraintKindString & "\par")
            result &= ConstraintToRichText(Element.Constraint, level)
            result &= Environment.NewLine & ("\par")
        End If

        Return result
    End Function

    Private Function DateTimeConstraintToRichText(ByVal a_date_time_constraint As Constraint_DateTime) As String
        Return Filemanager.GetOpenEhrTerm(a_date_time_constraint.TypeofDateTimeConstraint, "not known")
    End Function

    Private Function ConstraintToRichText(ByVal a_constraint As Constraint, ByVal level As Integer) As String
        Dim result As String = ""

        Select Case a_constraint.Kind

            Case ConstraintKind.Quantity
                result &= Environment.NewLine & QuantityConstraintToRichText(CType(a_constraint, Constraint_Quantity), level)

            Case ConstraintKind.Count
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & CountConstraintToRichText(CType(a_constraint, Constraint_Count)) & "\par")

            Case ConstraintKind.Text
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & TextConstraintToRichText(CType(a_constraint, Constraint_Text)) & "\par")

            Case ConstraintKind.Boolean
                Dim b As Constraint_Boolean = CType(a_constraint, Constraint_Boolean)
                Dim s As String = ""

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

            Case ConstraintKind.DateTime
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & DateTimeConstraintToRichText(CType(a_constraint, Constraint_DateTime)) & "\par")

            Case ConstraintKind.Ordinal
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & OrdinalConstraintToRichText(CType(a_constraint, Constraint_Ordinal)) & "\par")

            Case ConstraintKind.Duration
                result &= Environment.NewLine & (Space(3 * level) & "  Constraint: " & DurationConstraintToRichText(CType(a_constraint, Constraint_Duration)) & "\par")

            Case ConstraintKind.Any
                ' add nothing

            Case ConstraintKind.Multiple
                For Each c As Constraint In CType(a_constraint, Constraint_Choice).Constraints
                    result &= ConstraintToRichText(c, level + 1)
                Next

            Case ConstraintKind.MultiMedia
                'add nothing

            Case ConstraintKind.URI
                'add nothing

            Case ConstraintKind.Parsable
                'add nothing

            Case ConstraintKind.Identifier
                'add nothing

            Case ConstraintKind.Currency
                'add nothing

            Case ConstraintKind.Interval_Count
                Dim cic As Constraint_Interval_Count = CType(a_constraint, Constraint_Interval_Count)
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Upper & ": "
                result &= CountConstraintToRichText(CType(cic.UpperLimit, Constraint_Count))
                result &= ", " & AE_Constants.Instance.Lower & ": "
                result &= CountConstraintToRichText(CType(cic.LowerLimit, Constraint_Count))
                result &= "\par"

            Case ConstraintKind.Interval_DateTime
                Dim cidt As Constraint_Interval_DateTime = CType(a_constraint, Constraint_Interval_DateTime)
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Upper & ": "
                result &= DateTimeConstraintToRichText(CType(cidt.UpperLimit, Constraint_DateTime))
                result &= ", " & AE_Constants.Instance.Lower & ": "
                result &= DateTimeConstraintToRichText(CType(cidt.LowerLimit, Constraint_DateTime))
                result &= "\par"

            Case ConstraintKind.Interval_Quantity
                Dim ciq As Constraint_Interval_Count = CType(a_constraint, Constraint_Interval_Quantity)
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Upper & ": \par"
                result &= Environment.NewLine & QuantityConstraintToRichText(CType(ciq.UpperLimit, Constraint_Quantity), level + 1) & "\par"
                result &= Environment.NewLine & Space(3 * level) & AE_Constants.Instance.Lower & ": \par"
                result &= Environment.NewLine & QuantityConstraintToRichText(CType(ciq.LowerLimit, Constraint_Quantity), level + 1) & "\par"

            Case ConstraintKind.Proportion
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

            Case ConstraintKind.Slot
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
                Debug.Assert(False, a_constraint.Kind.ToString & " not handled")

        End Select
        Return result
    End Function

    Private Structure HTML_Details
        Dim ImageSource As String
        Dim HTML As String
        Dim TerminologyCode As String
    End Structure

    Private Function HtmlDetails(ByVal c As Constraint) As HTML_Details
        Dim result As HTML_Details = New HTML_Details()
        result.HTML = ""
        result.ImageSource = ""
        result.TerminologyCode = ""

        If Not c Is Nothing Then
            Select Case c.Kind
                Case ConstraintKind.Multiple
                    Dim prefix As String = ""

                    For Each cc As Constraint In CType(c, Constraint_Choice).Constraints
                        result.HTML &= prefix & HtmlDetails(cc).HTML
                        prefix = "<hr>"
                    Next

                    result.ImageSource = "Images/choice.gif"

                Case ConstraintKind.Any
                    result.HTML = Environment.NewLine & "&nbsp;"
                    result.ImageSource = "Images/any.gif"

                Case ConstraintKind.Quantity
                    result.HTML = Environment.NewLine & QuantityConstraintToHTML(CType(c, Constraint_Quantity))
                    result.ImageSource = "Images/quantity.gif"

                Case ConstraintKind.Count
                    result.HTML = Environment.NewLine & CountConstraintToRichText(CType(c, Constraint_Count))
                    result.ImageSource = "Images/count.gif"

                Case ConstraintKind.Text
                    result.HTML = Environment.NewLine & TextConstraintToRichText(CType(c, Constraint_Text))
                    result.ImageSource = "Images/text.gif"

                Case ConstraintKind.Boolean
                    Dim b As Constraint_Boolean = CType(c, Constraint_Boolean)
                    result.HTML = Environment.NewLine

                    If b.TrueFalseAllowed Then
                        result.HTML &= Boolean.TrueString & ", " & Boolean.FalseString
                    ElseIf b.TrueAllowed Then
                        result.HTML &= Boolean.TrueString
                    Else
                        result.HTML &= Boolean.FalseString
                    End If

                    If b.hasAssumedValue Then
                        result.HTML &= Filemanager.GetOpenEhrTerm(158, "Assumed value:") & " " & b.AssumedValue.ToString
                    End If

                    result.ImageSource = "Images/truefalse.gif"

                Case ConstraintKind.DateTime
                    Dim dt As Constraint_DateTime = CType(c, Constraint_DateTime)
                    result.HTML = Environment.NewLine & Filemanager.GetOpenEhrTerm(dt.TypeofDateTimeConstraint, "not known")
                    result.ImageSource = "Images/datetime.gif"

                Case ConstraintKind.Ordinal
                    result.HTML = Environment.NewLine & OrdinalConstraintToHTML(CType(c, Constraint_Ordinal))
                    result.ImageSource = "Images/ordinal.gif"

                Case ConstraintKind.URI
                    result.HTML = Environment.NewLine & "&nbsp;"
                    result.ImageSource = "Images/uri.gif"

                Case ConstraintKind.Proportion
                    result.HTML = Environment.NewLine & ProportionConstraintToHTML(CType(c, Constraint_Proportion))
                    result.ImageSource = "Images/ratio.gif"

                Case ConstraintKind.Duration
                    result.HTML = Environment.NewLine & DurationConstraintToRichText(CType(c, Constraint_Duration))
                    result.ImageSource = "Images/duration.gif"

                Case ConstraintKind.Interval_Count
                    result.HTML = Environment.NewLine & IntervalCountToHTML(CType(c, Constraint_Interval_Count))
                    result.ImageSource = "Images/interval.gif"

                Case ConstraintKind.Interval_Quantity
                    result.HTML = Environment.NewLine & IntervalQuantityToHTML(CType(c, Constraint_Interval_Quantity))
                    result.ImageSource = "Images/interval.gif"

                Case ConstraintKind.Interval_DateTime
                    result.HTML = Environment.NewLine & IntervalDateTimeToHTML(CType(c, Constraint_Interval_DateTime))
                    result.ImageSource = "Images/interval.gif"

                Case ConstraintKind.MultiMedia
                    result.HTML = ""
                    result.ImageSource = "Images/multimedia.gif"

                Case ConstraintKind.Slot
                    result.HTML = Environment.NewLine & SlotToHTML(CType(c, Constraint_Slot))
                    result.ImageSource = "Images/slot.gif"

                Case Else
                    Debug.WriteLine(c.Kind.ToString)
                    Debug.Assert(False, "Not handled")
            End Select
        End If

        Return result
    End Function

    Public Overrides Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String

        ' write the cardinality of the element
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("<tr>")
        Dim class_names As String = ""
        Dim html_dt As HTML_Details = HtmlDetails(Element.Constraint)
        Dim terminologyCode As String

        If Main.Instance.Options.ShowTermsInHtml Then
            For Each terminologyRow As DataRow In mFileManager.OntologyManager.TerminologiesTable.Rows
                terminologyCode = CStr(terminologyRow.Item(0))

                If mFileManager.OntologyManager.Ontology.HasTermBinding(terminologyCode, NodeId) Then
                    html_dt.TerminologyCode = "<br>" & terminologyCode & ": " & mFileManager.OntologyManager.Ontology.TermBinding(terminologyCode, NodeId)
                End If
            Next
        End If

        If Not Element.Constraint Is Nothing Then
            If Element.Constraint.Kind = ConstraintKind.Multiple Then
                Dim prefix As String = ""

                For Each c As Constraint In CType(Element.Constraint, Constraint_Choice).Constraints
                    class_names &= prefix & c.Kind.ToString
                    prefix = "<hr>"
                Next
            Else
                class_names = Element.Constraint.ConstraintKindString
            End If
        End If

        result.AppendFormat("{0}<td><table><tr><td width=""{1}""></td><td><img border=""0"" src=""{2}"" width=""32"" height=""32"" align=""middle""><b>{3}</b></td></table></td>", Environment.NewLine, (level * 20).ToString, html_dt.ImageSource, mText)
        result.AppendFormat("{0}<td>{1}{2}</td>", Environment.NewLine, mDescription, html_dt.TerminologyCode)
        result.AppendFormat("{0}<td><b><i>{1}</i></b><br>", Environment.NewLine, class_names)
        result.AppendFormat("{0}{1}</td>", Environment.NewLine, mItem.Occurrences.ToString)
        result.AppendFormat("{0}<td>{1}", Environment.NewLine, html_dt.HTML)
        result.AppendFormat("{0}</td>", Environment.NewLine)

        If showComments Then
            Dim s As String = Comment

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
        Element = New RmElement(aTerm.Code)
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
