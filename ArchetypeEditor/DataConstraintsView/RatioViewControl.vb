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
'

Option Strict On

Public Class RatioViewControl : Inherits ElementViewControl

    Private WithEvents mNumerator As NumericUpDown
    Private WithEvents mDenominator As NumericUpDown
    Private mIsPercent As Boolean

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        mNumerator = New NumericUpDown
        mDenominator = New NumericUpDown

        Dim c As Constraint_Proportion = CType(aConstraint, Constraint_Proportion)

        SetMaxMin(mNumerator, c.Numerator)
        mNumerator.Height = 25
        mNumerator.Width = 75
        mNumerator.Location = aLocation

        Me.Controls.Add(mNumerator)

        Dim lbl As New Label
        lbl.Width = 15
        lbl.Location = New Drawing.Point(aLocation.X + 85, aLocation.Y)
        Me.Controls.Add(lbl)


        If c.Denominator.HasMaximum AndAlso c.Denominator.MaximumRealValue = 100 AndAlso c.Denominator.MinimumRealValue = 100 Then
            lbl.Text = "%"
            mIsPercent = True
        Else
            lbl.Text = ":"
            SetMaxMin(mDenominator, c.Denominator)
            mDenominator.Height = 25
            mDenominator.Width = 75
            mDenominator.Location = New Drawing.Point(aLocation.X + 105, aLocation.Y)
            Me.Controls.Add(mDenominator)

        End If
    End Sub

    Private Sub SetMaxMin(ByVal aControl As NumericUpDown, ByVal u As Constraint_Count)
        If u.HasMaximum Then
            aControl.Maximum = u.MaximumValue

            If u.MaximumValue <= 10 Then
                If u.Kind = ConstraintKind.QuantityUnit Then
                    aControl.Increment = CDec(0.1)
                End If
            End If

            If u.MaximumValue <= 1 Then
                If u.Kind = ConstraintKind.QuantityUnit Then
                    aControl.Increment = CDec(0.01)
                End If
            End If
        Else
            aControl.Maximum = 1000000
        End If

        If u.HasMinimum Then
            aControl.Minimum = u.MinimumValue
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
            Handles mNumerator.ValueChanged, mDenominator.ValueChanged

        If mIsPercent Then
            Value = CStr(mNumerator.Value) + " %"
        Else
            Value = CStr(mNumerator.Value & " : " & mDenominator.Value)
        End If
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
