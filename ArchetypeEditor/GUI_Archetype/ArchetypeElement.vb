'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
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
    Public ReadOnly Property IsReference() As Boolean

        Get
            Return Me.Element.isReference
        End Get
    End Property
    Public ReadOnly Property HasReferences() As Boolean

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
        Dim d_row As DataRowView
        Dim s As String
        Dim punctuation As Char() = {CType(".", Char), CType(",", Char)}
        Dim a_Term As RmTerm

        s = TextConstraint.TypeOfTextConstraint.ToString & ";"
        Select Case TextConstraint.TypeOfTextConstraint
            Case TextConstrainType.Text
                Dim a_string As String
                If TextConstraint.AllowableValues.Codes.Count > 0 Then
                    For Each a_string In TextConstraint.AllowableValues.Codes
                        s = s & " '" & a_string & "',"
                    Next
                    s.TrimEnd(punctuation)
                End If
            Case TextConstrainType.Internal
                If TextConstraint.AllowableValues.Codes.Count > 1 Then
                    Dim a_string As String
                    For Each a_string In TextConstraint.AllowableValues.Codes
                        a_Term = mFileManager.OntologyManager.GetTerm(a_string)
                        s = s & " '" & a_Term.Text & "',"
                    Next
                    s = s.TrimEnd(punctuation)
                End If
            Case TextConstrainType.Terminology
                a_Term = mFileManager.OntologyManager.GetTerm(TextConstraint.ConstraintCode)
                s = s & " " & a_Term.Text
        End Select

        Return s.Trim

    End Function

    Private Function QuantityConstraintToRichText(ByVal q As Constraint_Quantity, ByVal level As Integer, ByVal new_line As String) As String
        Dim Text, s As String
        Dim u As Constraint_QuantityUnit

        Text = (Space(3 * level) & "  Constraint: Physical property = " & q.Physical_property & ";" & "\par")
        For Each u In q.Units
            Text &= new_line & (Space(4 * level) & QuantityUnitConstraintToRichText(u) & "\par")
        Next
        Return Text
    End Function

    Private Function QuantityConstraintToHTML(ByVal q As Constraint_Quantity) As String
        Dim a_text As String

        a_text = Filemanager.GetOpenEhrTerm(116, "Property") & " = " & q.Physical_property & "<br>"
        For Each u As Constraint_QuantityUnit In q.Units
            a_text &= Environment.NewLine & QuantityUnitConstraintToRichText(u) & "<br>"
        Next
        Return a_text
    End Function

    Private Function QuantityUnitConstraintToRichText(ByVal u As Constraint_QuantityUnit) As String
        Dim s As String

        If u.Unit <> "" Then
            s = "  Units = " & u.Unit & ";"
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

    Private Function OrdinalConstraintToRichText(ByVal OrdinalConstraint As Constraint_Ordinal) As String
        Dim s As String
        Dim ov As OrdinalValue
        Dim a_Term As RmTerm

        s = "{"
        For Each ov In OrdinalConstraint.OrdinalValues
            Dim str As String
            a_Term = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            s = s & ov.Ordinal.ToString & ": \i " & a_Term.Text & "\i0 ; "
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
            Dim str As String
            a_Term = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            s &= ov.Ordinal.ToString & ": <i> " & a_Term.Text & "<i><br> "
        Next
        s = s.Trim
        Return s

    End Function

    Private Function RatioConstraintToHTML(ByVal RatioConstraint As Constraint_Ratio) As String
        Dim s As String

        s = CountConstraintToRichText(CType(RatioConstraint.Numerator, Constraint_Count))
        If RatioConstraint.IsPercent Then
            s &= " %"
        Else
            s &= " : " & CountConstraintToRichText(CType(RatioConstraint.Denominator, Constraint_Count))
        End If

        Return s

    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        Dim new_line As String = Chr(10) & Chr(13)

        'Dim Text, x, s, s1 As String

        ' write the cardinality of the element
        Dim s1 As String = mText & " (" & mItem.Occurrences.ToString & ")"

        ' add bars if table and wrapping text
        Dim aText As String = (Space(3 * level) & "\b " & s1 & "\b0\par")

        'write the description of the element
        Dim s As String = " " & mDescription
        aText = aText & new_line & (Space(3 * level) & s & "\par")

        aText &= new_line & (Space(3 * level) & "  DataType = " _
                & Me.Element.Constraint.ConstraintTypeString & "\par")

        Select Case Me.Element.Constraint.Type
            Case ConstraintType.Quantity

                aText &= new_line _
                        & QuantityConstraintToRichText(CType(Me.Element.Constraint, Constraint_Quantity), level, new_line)

            Case ConstraintType.Count

                aText &= new_line & (Space(3 * level) & "  Constraint: " & CountConstraintToRichText(CType(Me.Element.Constraint, Constraint_Count)) & "\par")

            Case ConstraintType.Text

                aText &= new_line & (Space(3 * level) & "  Constraint: " & TextConstraintToRichText(CType(Me.Element.Constraint, Constraint_Text)) & "\par")

            Case ConstraintType.Boolean
                Dim b As Constraint_Boolean

                b = CType(Me.Element.Constraint, Constraint_Boolean)
                If b.hasAssumedValue Then
                    s = "Default = " & b.AssumedValue.ToString
                    aText &= new_line & (Space(3 * level) & "  Constraint: " & s & "\par")
                End If

            Case ConstraintType.DateTime
                Dim dt As Constraint_DateTime
                dt = CType(Me.Element.Constraint, Constraint_DateTime)
                s = Filemanager.GetOpenEhrTerm(dt.TypeofDateTimeConstraint, "not known")
                aText &= new_line & (Space(3 * level) & "  Constraint: " & s & "\par")

            Case ConstraintType.Ordinal
                aText &= new_line & (Space(3 * level) & "  Constraint: " & OrdinalConstraintToRichText(CType(Me.Element.Constraint, Constraint_Ordinal)) & "\par")

        End Select

        aText &= new_line & ("\par")
        Return aText

    End Function

    Private Structure HTML_Details
        Dim ImageSource As String
        Dim HTML As String
    End Structure

    Private Function GetHtmlDetails(ByVal c As Constraint) As HTML_Details
        Dim html_dt As HTML_Details

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

                If b.TrueAllowed Then
                    html_dt.HTML &= Boolean.TrueString
                    If b.FalseAllowed Then
                        html_dt.HTML &= ", " & Boolean.FalseString
                    End If
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

            Case ConstraintType.Ratio
                html_dt.HTML = Environment.NewLine & RatioConstraintToHTML(CType(c, Constraint_Ratio))
                html_dt.ImageSource = "Images/ratio.gif"
            Case Else
                Debug.WriteLine(c.Type.ToString)
                Debug.Assert(False, "Not handled")
        End Select

        Return html_dt


    End Function



    Public Overrides Function ToHTML(ByVal level As Integer) As String

        ' write the cardinality of the element
        Dim a_text As String = "<tr>"
        Dim class_names As String
        Dim html_dt As HTML_Details = GetHtmlDetails(Me.Element.Constraint)

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

        a_text &= Environment.NewLine & "<td><table><tr><td width=""" & (level * 20).ToString & """></td><td><img border=""0"" src=""" & html_dt.ImageSource & """ width=""32"" height=""32"" align=""middle""><b>" & mText & "</b></td></table></td>"
        a_text &= Environment.NewLine & "<td>" & mDescription & "</td>"
        a_text &= Environment.NewLine & "<td><b><i>" & class_names & "</i></b><br>"
        a_text &= Environment.NewLine & mItem.Occurrences.ToString & "</td>"
        a_text &= Environment.NewLine & "<td>" & html_dt.HTML
        a_text &= Environment.NewLine & "</td>"

        Return a_text

    End Function


    Overrides Function ToString() As String
        Return mText
    End Function

    Public Sub New(ByVal aText As String, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(aText)
        mFileManager = a_file_manager
        Dim aTerm As RmTerm = mFilemanager.OntologyManager.AddTerm(aText)
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
