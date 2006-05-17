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

Public Class IntervalViewControl : Inherits ElementViewControl

    Private WithEvents mLower As NumericUpDown
    Private WithEvents mUpper As NumericUpDown

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        mLower = New NumericUpDown
        mUpper = New NumericUpDown

        If aConstraint.Type = ConstraintType.Interval_Quantity Then
            Dim c As Constraint_Interval_Quantity = CType(aConstraint, Constraint_Interval_Quantity)

            'ToDo: ensure this works OK

            If CType(c.AbsoluteLimits, Constraint_Quantity).has_units Then
                SetMaxMin(mLower, CType(CType(c.AbsoluteLimits, Constraint_Quantity).Units(0), Constraint_QuantityUnit))
            End If
        ElseIf aConstraint.Type = ConstraintType.Interval_Count Then

            Dim c As Constraint_Interval = CType(aConstraint, Constraint_Interval)

            SetMaxMin(mLower, CType(c.AbsoluteLimits, Constraint_Count))
            mLower.Height = 25
            mLower.Width = 75
            mLower.Location = aLocation

            Me.Controls.Add(mLower)

            Dim lbl As New Label
            lbl.Text = "-"
            lbl.Width = 15
            lbl.Location = New Drawing.Point(aLocation.X + 85, aLocation.Y)
            Me.Controls.Add(lbl)

            SetMaxMin(mUpper, CType(c.AbsoluteLimits, Constraint_Count))
            mUpper.Height = 25
            mUpper.Width = 75
            mUpper.Location = New Drawing.Point(aLocation.X + 105, aLocation.Y)
            Me.Controls.Add(mUpper)
        End If
    End Sub

    Private Sub SetMaxMin(ByVal aControl As NumericUpDown, ByVal c As Constraint_Count)
        If c.HasMaximum Then
            aControl.Maximum = c.MaximumValue
            'CHANGE Sam Heard 2004-05-24
            'Added set increment
            If c.MaximumValue <= 10 Then
                If c.Type = ConstraintType.QuantityUnit Then
                    aControl.Increment = CDec(0.1)
                End If
            End If
            If c.MaximumValue <= 1 Then
                If c.Type = ConstraintType.QuantityUnit Then
                    aControl.Increment = CDec(0.01)
                End If
            End If
        Else
            aControl.Maximum = 1000000
        End If
        If c.HasMinimum Then
            aControl.Minimum = c.MinimumValue
        End If
    End Sub

    Private mValue As String
    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            mValue = CStr(Value)
            Tag = mValue
            MyBase.OnValueChanged()
        End Set
    End Property

    Private Sub Numeric_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles mLower.ValueChanged, mUpper.ValueChanged

        Value = CStr(mLower.Value & " - " & mUpper.Value)
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
'The Original Code is CountViewControl.vb.
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
