'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Public Class QuantityViewControl : Inherits ElementViewControl
    Private WithEvents mNumeric As NumericUpDown

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)

    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

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

        If quantityConstraint.HasUnits Then
            Debug.Assert(TypeOf quantityConstraint.Units.Item(1) Is Constraint_Count)
            SetMaxMin(mNumeric, CType(quantityConstraint.Units.Item(1), Constraint_Count))
        End If

        mNumeric.Height = 25
        mNumeric.Width = 75

        If quantityConstraint.IsNull OrElse quantityConstraint.PhysicalPropertyAsString = "?" Then
            mNumeric.Location = New Point(aLocation.X, aLocation.Y)
            Controls.Add(mNumeric)
        Else
            mNumeric.Location = New Point(aLocation.X + 5, aLocation.Y + 5)
            Controls.Add(mNumeric)

            If quantityConstraint.HasUnits Then
                If quantityConstraint.Units.Count = 1 Then
                    ' shows one unit as a label
                    Dim lbl As New Label
                    lbl.Location = New Point(aLocation.X + 90, aLocation.Y + 5)

                    Debug.Assert(TypeOf quantityConstraint.Units(1) Is Constraint_QuantityUnit)
                    lbl.Text = CType(quantityConstraint.Units(1), Constraint_QuantityUnit).Unit
                    lbl.AutoSize = True
                    Controls.Add(lbl)
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

                    Controls.Add(combo)
                End If
            Else
                ' shows all allowable units for that property
                Dim combo As New ComboBox
                combo.Location = New Point(aLocation.X + 90, aLocation.Y + 5)
                combo.Height = 25
                combo.Width = 150

                Try
                    Dim d_row As DataRow
                    Dim d_rows As DataRow()

                    If quantityConstraint.IsCoded Then
                        d_rows = Main.Instance.PhysicalPropertiesTable.Select _
                            ("openEHR = " & quantityConstraint.OpenEhrCode.ToString())
                    Else
                        'OBSOLETE
                        If Main.Instance.DefaultLanguageCode = "en" Then
                            d_rows = Main.Instance.PhysicalPropertiesTable.Select _
                                ("Text = '" & quantityConstraint.PhysicalPropertyAsString & "'")
                        Else
                            d_rows = Main.Instance.PhysicalPropertiesTable.Select _
                               ("Translated = '" & quantityConstraint.PhysicalPropertyAsString & "'")
                        End If
                    End If

                    If Not d_rows Is Nothing AndAlso d_rows.Length > 0 Then
                        Dim id As String = d_rows(0)(0)

                        For Each d_row In Main.Instance.UnitsTable.Select("property_id = " & id)
                            combo.Items.Add(CStr(d_row(1)))
                        Next
                    End If
                    Controls.Add(combo)
                Catch
                    Debug.Assert(False, "Error selecting quantity property")
                    combo.Items.Add("#Error#")
                    Controls.Add(combo)
                End Try
            End If
        End If
    End Sub

    Private Sub SetMaxMin(ByVal aControl As NumericUpDown, ByVal u As Constraint_QuantityUnit)
        If u.HasMaximum AndAlso u.MaximumRealValue < 1000000 Then
            aControl.Maximum = CDec(u.MaximumRealValue)

            If u.MaximumRealValue <= 10 Then
                If u.Kind = ConstraintKind.QuantityUnit Then
                    aControl.Increment = CDec(0.1)
                End If
            End If

            If u.MaximumRealValue <= 1 Then
                If u.Kind = ConstraintKind.QuantityUnit Then
                    aControl.Increment = CDec(0.01)
                End If
            End If
        Else
            aControl.Maximum = 1000000
        End If

        If u.HasMinimum AndAlso u.MinimumRealValue > -1000000 Then
            aControl.Minimum = CDec(u.MinimumRealValue)
        Else
            aControl.Minimum = -1000000
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

    Private Sub Numeric_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mNumeric.ValueChanged
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
