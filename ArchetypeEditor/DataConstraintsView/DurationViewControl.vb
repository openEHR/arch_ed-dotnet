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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraintsView/QuantityViewControl.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Public Class DurationViewControl : Inherits ElementViewControl
    Private WithEvents mNumeric As NumericUpDown

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)

    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim durationConstraint As Constraint_Duration = CType(aConstraint, Constraint_Duration)

        'Dim ctrl As New NumericUpDown
        mNumeric = New NumericUpDown
        mNumeric.DecimalPlaces = 0

        'Change Sam Heard 2004-06-20
        'Crash if no units so added check

        If durationConstraint.MinMaxValueUnits <> "" Then
            SetMaxMin(mNumeric, CType(durationConstraint, Constraint_Count))
        End If

        mNumeric.Height = 25
        mNumeric.Width = 75

        mNumeric.Location = New Point(aLocation.X + 5, aLocation.Y + 5)
        Me.Controls.Add(mNumeric)

        ' shows all allowable units as per constraint
        Dim combo As New ComboBox
        combo.Location = New Point(aLocation.X + 90, aLocation.Y + 5)
        combo.Height = 25
        combo.Width = 150

        Dim time As Boolean = False

        For Each u As Char In durationConstraint.AllowableUnits
            Dim s As String = u.ToString.ToLower(System.Globalization.CultureInfo.InvariantCulture)


            If s = "t" Then
                time = True
            ElseIf s <> "p" Then
                If s = "m" Then
                    If time Then
                        s = "min"
                    Else
                        s = "mo"
                    End If
                ElseIf s = "y" Then
                    s = "a"
                ElseIf s = "w" Then
                    s = "wk"
                End If
                combo.Items.Add(Main.ISO_TimeUnits.GetLanguageForISO(s))
            End If
        Next

        Me.Controls.Add(combo)

    End Sub

    Private Sub SetMaxMin(ByVal aControl As NumericUpDown, ByVal u As Constraint_Count)
        If u.HasMaximum Then
            aControl.Maximum = u.MaximumValue
        Else
            aControl.Maximum = 1000000
        End If
        If u.HasMinimum Then
            aControl.Minimum = u.MinimumValue
        End If
    End Sub

    Private mValue As Decimal
    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            mValue = CDec(Value)
            MyBase.OnValueChanged()
        End Set
    End Property

    Private Sub Numeric_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles mNumeric.ValueChanged

        Tag = mNumeric.Value
        Value = mNumeric.Value
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
'The Original Code is DurationViewControl.vb.
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
