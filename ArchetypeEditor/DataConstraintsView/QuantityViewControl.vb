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

Public Class QuantityViewControl : Inherits ElementViewControl
    Private WithEvents mNumeric As NumericUpDown

    Public Sub New(ByVal anElement As ArchetypeElement)
        MyBase.New(anElement)

    End Sub

    Public Sub New(ByVal aConstraint As Constraint)
        MyBase.New(aConstraint)

    End Sub

    'Protected Overrides Sub InitialiseComponent(ByVal anElement As ArchetypeElement, _
    '        ByVal aLocation As Point)
    '    Dim aConstraint As Constraint_Quantity = CType(anElement.Constraint, Constraint_Quantity)
    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim quantityConstraint As Constraint_Quantity = CType(aConstraint, Constraint_Quantity)

        'Dim ctrl As New NumericUpDown
        mNumeric = New NumericUpDown
        mNumeric.DecimalPlaces = 2

        'Change Sam Heard 2004-06-20
        'Crash if no units so added check

        If quantityConstraint.has_units Then
            Debug.Assert(TypeOf quantityConstraint.Units.Item(1) Is Constraint_Count)
            SetMaxMin(mNumeric, CType(quantityConstraint.Units.Item(1), Constraint_Count))
        End If

        mNumeric.Height = 25
        mNumeric.Width = 75
        If quantityConstraint.Physical_property = "" _
                OrElse quantityConstraint.Physical_property = "?" Then

            mNumeric.Location = New Point(aLocation.X, aLocation.Y)

            Me.Controls.Add(mNumeric)

        Else
            mNumeric.Location = New Point(aLocation.X + 5, aLocation.Y + 5)
            Me.Controls.Add(mNumeric)

            If quantityConstraint.has_units Then
                If quantityConstraint.Units.Count = 1 Then
                    ' shows one unit as a label
                    Dim lbl As New Label
                    lbl.Location = New Point(aLocation.X + 90, aLocation.Y + 5)

                    Debug.Assert(TypeOf quantityConstraint.Units(1) Is Constraint_QuantityUnit)
                    lbl.Text = CType(quantityConstraint.Units(1), Constraint_QuantityUnit).Unit

                    Me.Controls.Add(lbl)

                Else
                    ' shows all allowable units as per constraint
                    Dim combo As New ComboBox
                    combo.Location = New Point(aLocation.X + 90, aLocation.Y + 5)
                    Dim u As Constraint_QuantityUnit
                    combo.Height = 25
                    combo.Width = 150
                    For Each u In quantityConstraint.Units
                        combo.Items.Add(u)
                    Next

                    Me.Controls.Add(combo)
                End If
            Else
                ' shows all allowable units for that property
                Dim combo As New ComboBox
                combo.Location = New Point(aLocation.X + 90, aLocation.Y + 5)
                Dim u As Constraint_QuantityUnit
                combo.Height = 25
                combo.Width = 150

                Dim d_row As DataRow
                d_row = OceanArchetypeEditor.Instance.PhysicalPropertiesTable.Select("Text = '" & quantityConstraint.Physical_property & "'")(0)

                Dim id As String = d_row(0)

                For Each d_row In OceanArchetypeEditor.Instance.UnitsTable.Select("property_id = " & id)

                    combo.Items.Add(CStr(d_row(1)))
                Next
                Me.Controls.Add(combo)
            End If
        End If
    End Sub

    Private Sub SetMaxMin(ByVal aControl As NumericUpDown, ByVal u As Constraint_QuantityUnit)
        If u.HasMaximum Then
            aControl.Maximum = u.MaximumValue
            'CHANGE Sam Heard 2004-05-24
            'Added set increment
            If u.MaximumValue <= 10 Then
                'If u.ConstraintType = "QuantityUnit" Then
                If u.Type = ConstraintType.QuantityUnit Then
                    aControl.Increment = CDec(0.1)
                End If
            End If
            If u.MaximumValue <= 1 Then
                'If u.ConstraintType = "QuantityUnit" Then
                If u.Type = ConstraintType.QuantityUnit Then
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
'The Original Code is QuantityViewControl.vb.
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
