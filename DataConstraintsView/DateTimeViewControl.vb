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

Public Class DateTimeViewControl : Inherits ElementViewControl

    'Private WithEvents mComboBox As ComboBox

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        'Dim p As New Panel
        'Dim rel_pos As New Point
        Dim max_width As Integer

        Dim dt As Constraint_DateTime = CType(aConstraint, Constraint_DateTime)
        Dim rel_pos As Point = aLocation
        Dim p As Control = Me

        Select Case dt.TypeofDateTimeConstraint
            Case 11 ' allow all

                p.Controls.AddRange(LabelNumValPair(rel_pos, "[dd]", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", System.DateTime.Today.Year, 1900))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[hh]", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 59, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[ss]", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 12 'Date and time
                p.Controls.AddRange(LabelNumValPair(rel_pos, "dd", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", System.DateTime.Today.Year, 1900))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "hh", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 59, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "ss", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 13 'Date and partial time
                p.Controls.AddRange(LabelNumValPair(rel_pos, "dd", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", 2003, 1900))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "hh", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 14 'Date only
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[dd]", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", System.DateTime.Today.Year, 1900))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 15 'Full date
                p.Controls.AddRange(LabelNumValPair(rel_pos, "dd", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", System.DateTime.Today.Year, 1900))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 16 'Partial date
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[dd]", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", System.DateTime.Today.Year, 1900))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 17 'Partial date with month
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[dd]", 31, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 12, 1))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "yyyy", System.DateTime.Today.Year, 1900))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 18 'Time only
                p.Controls.AddRange(LabelNumValPair(rel_pos, "hh", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 59, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[ss]", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 19 'Full time
                p.Controls.AddRange(LabelNumValPair(rel_pos, "hh", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 59, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "ss", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 20 'Partial time
                p.Controls.AddRange(LabelNumValPair(rel_pos, "hh", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[mm]", 59, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[ss]", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5

            Case 21 'Partial time with minutes
                p.Controls.AddRange(LabelNumValPair(rel_pos, "hh", 23, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "mm", 59, 0))
                rel_pos.X = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5
                p.Controls.AddRange(LabelNumValPair(rel_pos, "[ss]", 59, 0))
                max_width = rel_pos.X + p.Controls.Item(p.Controls.Count - 1).Width + 5


        End Select

        p.Width = max_width

        'Me.Controls.Add(p)

    End Sub

    Private Function LabelNumValPair(ByRef pos As Point, ByVal text As String, _
            Optional ByVal maxval As Integer = 1000000, _
            Optional ByVal minval As Integer = 0) As Control()

        Dim ctrl(1) As Control
        Dim lbl As New Label
        lbl.Location = pos
        lbl.AutoSize = True
        lbl.Height = 20
        lbl.Text = text
        ctrl(0) = lbl
        Dim num As New NumericUpDown
        num.Maximum = maxval
        num.Minimum = minval
        num.Height = 25
        If text.StartsWith("[") Then
            num.Width = (10 * (text.Length - 2)) + 20
        Else
            num.Width = (10 * text.Length) + 20
        End If
        num.Location = New Drawing.Point(pos.X, pos.Y + 26)
        ctrl(1) = num
        Return ctrl

    End Function

    ' Value = -1 => not selected
    Private mValue As Integer
    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            If (Value Is Nothing AndAlso mValue >= 0) _
                    OrElse (Not Value Is Nothing AndAlso mValue < 0) _
                    OrElse (Not Value Is Nothing AndAlso mValue <> CInt(Value)) Then

                If Not Value Is Nothing Then
                    mValue = CInt(Value)

                Else    ' not selected
                    mValue = -1
                End If
                Tag = mValue
                MyBase.OnValueChanged()
            End If
        End Set
    End Property

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
'The Original Code is DateTimeViewControl.vb.
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
