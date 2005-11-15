
'
'	component:   "openEHR Archetype Project"
'	description: "Displays and allows creation and editing of a quantity constraint"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/DataConstraints/SCCS/s.QuantityConstraintControl.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class QuantityConstraintControl : Inherits ConstraintControl

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = a_file_manager
        Me.QuantityUnitConstraint.LocalFileManager = mFileManager


    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents listUnits As System.Windows.Forms.ListBox
    Friend WithEvents lblListProperty As System.Windows.Forms.Label
    Friend WithEvents lblListUnits As System.Windows.Forms.Label
    Friend WithEvents comboPhysicalProperty As System.Windows.Forms.ComboBox
    Friend WithEvents butAddUnit As System.Windows.Forms.Button
    Friend WithEvents butRemoveUnit As System.Windows.Forms.Button
    Friend WithEvents LabelQuantity As System.Windows.Forms.Label
    Friend WithEvents QuantityUnitConstraint As QuantityUnitConstraintControl

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(QuantityConstraintControl))
        Me.listUnits = New System.Windows.Forms.ListBox
        Me.lblListProperty = New System.Windows.Forms.Label
        Me.lblListUnits = New System.Windows.Forms.Label
        Me.comboPhysicalProperty = New System.Windows.Forms.ComboBox
        Me.butAddUnit = New System.Windows.Forms.Button
        Me.butRemoveUnit = New System.Windows.Forms.Button
        Me.LabelQuantity = New System.Windows.Forms.Label
        Me.QuantityUnitConstraint = New QuantityUnitConstraintControl
        Me.SuspendLayout()
        '
        'listUnits
        '
        Me.listUnits.ItemHeight = 16
        Me.listUnits.Location = New System.Drawing.Point(88, 66)
        Me.listUnits.Name = "listUnits"
        Me.listUnits.Size = New System.Drawing.Size(192, 68)
        Me.listUnits.TabIndex = 15
        '
        'lblListProperty
        '
        Me.lblListProperty.BackColor = System.Drawing.Color.Transparent
        Me.lblListProperty.Location = New System.Drawing.Point(16, 34)
        Me.lblListProperty.Name = "lblListProperty"
        Me.lblListProperty.Size = New System.Drawing.Size(72, 16)
        Me.lblListProperty.TabIndex = 9
        Me.lblListProperty.Text = "Property:"
        Me.lblListProperty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblListUnits
        '
        Me.lblListUnits.BackColor = System.Drawing.Color.Transparent
        Me.lblListUnits.Location = New System.Drawing.Point(16, 66)
        Me.lblListUnits.Name = "lblListUnits"
        Me.lblListUnits.Size = New System.Drawing.Size(64, 16)
        Me.lblListUnits.TabIndex = 2
        Me.lblListUnits.Text = "Units:"
        Me.lblListUnits.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'comboPhysicalProperty
        '
        Me.comboPhysicalProperty.DisplayMember = "Text"
        Me.comboPhysicalProperty.Location = New System.Drawing.Point(88, 34)
        Me.comboPhysicalProperty.Name = "comboPhysicalProperty"
        Me.comboPhysicalProperty.Size = New System.Drawing.Size(272, 24)
        Me.comboPhysicalProperty.TabIndex = 0
        Me.comboPhysicalProperty.ValueMember = "id"
        '
        'butAddUnit
        '
        Me.butAddUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddUnit.Image = CType(resources.GetObject("butAddUnit.Image"), System.Drawing.Image)
        Me.butAddUnit.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddUnit.Location = New System.Drawing.Point(56, 82)
        Me.butAddUnit.Name = "butAddUnit"
        Me.butAddUnit.Size = New System.Drawing.Size(24, 25)
        Me.butAddUnit.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.butAddUnit, "Add new item")
        '
        'butRemoveUnit
        '
        Me.butRemoveUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveUnit.Image = CType(resources.GetObject("butRemoveUnit.Image"), System.Drawing.Image)
        Me.butRemoveUnit.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveUnit.Location = New System.Drawing.Point(56, 114)
        Me.butRemoveUnit.Name = "butRemoveUnit"
        Me.butRemoveUnit.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveUnit.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.butRemoveUnit, "Remove highlighted item")
        '
        'LabelQuantity
        '
        Me.LabelQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelQuantity.Location = New System.Drawing.Point(16, 8)
        Me.LabelQuantity.Name = "LabelQuantity"
        Me.LabelQuantity.Size = New System.Drawing.Size(88, 16)
        Me.LabelQuantity.TabIndex = 43
        Me.LabelQuantity.Text = "Quantity"
        '
        'QuantityUnitConstraint
        '
        Me.QuantityUnitConstraint.Location = New System.Drawing.Point(0, 152)
        Me.QuantityUnitConstraint.Name = "QuantityUnitConstraint"
        Me.QuantityUnitConstraint.Size = New System.Drawing.Size(384, 120)
        Me.QuantityUnitConstraint.TabIndex = 0
        Me.QuantityUnitConstraint.Visible = False
        '
        'QuantityConstraintControl
        '
        Me.Controls.Add(Me.QuantityUnitConstraint)
        Me.Controls.Add(Me.LabelQuantity)
        Me.Controls.Add(Me.butRemoveUnit)
        Me.Controls.Add(Me.butAddUnit)
        Me.Controls.Add(Me.listUnits)
        Me.Controls.Add(Me.comboPhysicalProperty)
        Me.Controls.Add(Me.lblListProperty)
        Me.Controls.Add(Me.lblListUnits)
        Me.Name = "QuantityConstraintControl"
        Me.Size = New System.Drawing.Size(368, 272)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private mIsState As Boolean

    Private Shadows ReadOnly Property Constraint() As Constraint_Quantity
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Quantity)
            Debug.Assert(MyBase.Constraint.Type = ConstraintType.Quantity)
            Return CType(MyBase.Constraint, Constraint_Quantity)
        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control
        Me.LabelQuantity.Text = AE_Constants.Instance.Quantity
        mIsState = IsState
        RemoveHandler Me.comboPhysicalProperty.SelectedIndexChanged, AddressOf Me.comboPhysicalProperty_SelectedIndexChanged

        If Me.comboPhysicalProperty.DataSource Is Nothing Then
            Me.comboPhysicalProperty.DataSource = ArchetypeEditor.Instance.PhysicalPropertiesTable
        End If

        listUnits.Items.Clear()

        For Each unit As Constraint_QuantityUnit In Me.Constraint.Units
            listUnits.Items.Add(unit)
        Next

        If listUnits.Items.Count > 0 Then
            listUnits.SelectedIndex = 0
        End If

        Dim i As Integer = Me.comboPhysicalProperty.FindStringExact( _
                Me.Constraint.Physical_property)
        Me.comboPhysicalProperty.SelectedIndex = i

        AddHandler Me.comboPhysicalProperty.SelectedIndexChanged, AddressOf Me.comboPhysicalProperty_SelectedIndexChanged

    End Sub

    Private Sub listUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles listUnits.SelectedIndexChanged

        ' no change to archetype so currentitem needs to be null
        If listUnits.SelectedIndex > -1 Then

            MyBase.IsLoading = True

            Dim quantityUnit As Constraint_QuantityUnit
            quantityUnit = CType(listUnits.SelectedItem, Constraint_QuantityUnit)

            ' HKF: 1620
            QuantityUnitConstraint.Visible = True
            QuantityUnitConstraint.ShowConstraint(mIsState, quantityUnit)

            MyBase.IsLoading = False
        Else
            QuantityUnitConstraint.Visible = False
        End If
    End Sub

    Private Sub butAddUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddUnit.Click

        Dim c_menu As New ContextMenu
        Dim where_clause As String

        'Exclude units already added
        For Each c_unit As Constraint_QuantityUnit In Me.listUnits.Items
            If where_clause = "" Then
                where_clause = " AND NOT (Text = '" & c_unit.Unit & "'"
            Else
                where_clause &= " OR Text = '" & c_unit.Unit & "'"
            End If
        Next

        If where_clause <> "" Then
            where_clause &= ")"
        End If

        For Each d_row As DataRow In ArchetypeEditor.Instance.UnitsTable.Select("property_id = " & _
                CStr(Me.comboPhysicalProperty.SelectedValue) & where_clause)

            Dim s As String

            s = CStr(d_row(1))

            Dim MI As MenuItem = New MenuItem(s)
            AddHandler MI.Click, AddressOf AddUnit

            c_menu.MenuItems.Add(MI)
        Next
        c_menu.Show(butAddUnit, New System.Drawing.Point(5, 5))

    End Sub

    Private Sub butRemoveUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveUnit.Click

        If listUnits.Items.Count > 0 Then
            If listUnits.SelectedIndex > -1 Then

                Dim s As String = listUnits.SelectedItem.ToString
                If MessageBox.Show(AE_Constants.Instance.Remove & "'" & s & "'", _
                        AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, _
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
                        = DialogResult.OK Then

                    Constraint.Units.Remove(s)

                    Dim i As Integer = listUnits.SelectedIndex
                    listUnits.Items.Remove(listUnits.SelectedItem)

                    If i - 1 > -1 Then
                        listUnits.SelectedIndex = i - 1
                    Else
                        QuantityUnitConstraint.Reset()
                    End If

                    mFileManager.FileEdited = True
                End If

            End If

        End If
    End Sub

    Private Sub comboPhysicalProperty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            'Handles comboPhysicalProperty.SelectedIndexChanged

        If Me.comboPhysicalProperty.Focused Then
            If (Me.comboPhysicalProperty.SelectedIndex > -1) _
                    AndAlso (MyBase.Constraint.Type = ConstraintType.Quantity) Then

                MyBase.IsLoading = True
                Me.Constraint.Physical_property = CStr(Me.comboPhysicalProperty.Text)

                'clear the units
                For Each u As Constraint_QuantityUnit In Me.Constraint.Units
                    Me.Constraint.Units.Remove(u.Unit)
                Next

                'Reset 
                listUnits.Items.Clear()

                QuantityUnitConstraint.Reset()

                mFileManager.FileEdited = True

                MyBase.IsLoading = False
            End If
        End If
    End Sub

    Private Sub AddUnit(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'If Not MyBase.ArchetypeElement Is Nothing Then
        If Not MyBase.IsLoading Then
            Try
                Dim quantityUnit As New Constraint_QuantityUnit

                Debug.Assert(TypeOf sender Is MenuItem)
                quantityUnit.Unit = CType(sender, MenuItem).Text

                If quantityUnit.isCompoundUnit Then
                    Me.AddCompoundUnits(quantityUnit)
                End If

                Constraint.Units.Add(quantityUnit, quantityUnit.Unit)
                listUnits.Items.Add(quantityUnit)
                listUnits.SelectedItem = quantityUnit

                mFileManager.FileEdited = True

            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
                MessageBox.Show(AE_Constants.Instance.Duplicate_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End If
    End Sub

    Private Sub AddCompoundUnits(ByVal a_unit_constraint As Constraint_QuantityUnit)
        Dim s As String

        s = ChooseCompoundUnits(a_unit_constraint.Unit)
        'Update the archetype
        If s <> "" Then
            a_unit_constraint.Unit = s
        End If
    End Sub

    Private Function ChooseCompoundUnits(ByVal Units As String) As String
        Dim Frm As New Choose
        Dim y, U_2, U_1 As String()
        Dim Phys_Prop, CompoundUnit As String
        Dim id As Integer
        Dim selected_rows As DataRow()
        Dim d_row, new_row As DataRow
        Dim curlybrackets As Char() = {"{"c, "}"c}

        y = Units.Split("/"c)

        y(0) = y(0).Trim(curlybrackets)
        'Get the power if there is one
        U_1 = y(0).Split("^"c)
        Phys_Prop = U_1(0)

        selected_rows = ArchetypeEditor.Instance.PhysicalPropertiesTable.Select("Text = '" & Phys_Prop & "'")

        If selected_rows.Length <> 1 Then
            'FIXME - report error
            Return ""
        End If

        id = CInt(selected_rows(0).Item(0))

        selected_rows = ArchetypeEditor.Instance.UnitsTable.Select("property_id = " & id.ToString)
        If selected_rows.Length = 0 Then
            'FIXME Error
            Return ""
        End If

        For Each d_row In selected_rows
            ' cannot have not set at this level
            Frm.ListChoose.Items.Add(d_row("Text"))
        Next


        Select Case y.Length
            Case 1
                Frm.Set_Single()
                Frm.ListChoose.SelectionMode = SelectionMode.One


                Frm.ShowDialog(Me)
                If Frm.ListChoose.SelectedIndex < 0 Then
                    ' cancel sets this
                    Return ""
                End If

                CompoundUnit = CStr(Frm.ListChoose.SelectedItem)
                If CompoundUnit = "(none)" Then
                    CompoundUnit = ""
                End If

                If U_1.Length = 2 Then
                    CompoundUnit = CompoundUnit & U_1(1)
                End If

            Case 2

                Frm.Set_Double()
                Frm.ListBox2.SelectionMode = SelectionMode.One
                Frm.ListChoose.SelectionMode = SelectionMode.One

                ' Second property

                y(1) = y(1).TrimEnd(curlybrackets)
                'Get the power if there is one
                U_2 = y(1).Split("^"c)
                Phys_Prop = U_2(0)

                selected_rows = ArchetypeEditor.Instance.PhysicalPropertiesTable.Select("Text = '" & Phys_Prop & "'")

                If selected_rows.Length <> 1 Then
                    'FIXME - report error
                    Return ""
                End If

                id = CInt(selected_rows(0).Item(0))

                selected_rows = ArchetypeEditor.Instance.UnitsTable.Select("property_id = " & id.ToString)
                If selected_rows.Length = 0 Then
                    'FIXME Error
                    Return ""
                End If

                For Each d_row In selected_rows
                    ' cannot have no denominator or not set
                    If (CStr(d_row("Text")) <> "?") Then
                        Frm.ListBox2.Items.Add(d_row("Text"))
                    End If
                Next

                Frm.ShowDialog(Me)

                If Frm.ListChoose.SelectedIndex < 0 Or Frm.ListBox2.SelectedIndex < 0 Then
                    ' cancel sets this
                    Return ""
                End If

                CompoundUnit = CStr(Frm.ListChoose.SelectedItem)
                If CompoundUnit = "(none)" Then
                    CompoundUnit = ""
                End If

                If U_1.Length = 2 Then
                    CompoundUnit = CompoundUnit & U_1(1)
                End If

                CompoundUnit = CompoundUnit & "/" & CStr(Frm.ListBox2.SelectedItem)

                If U_2.Length = 2 Then
                    CompoundUnit = CompoundUnit & U_2(1)
                End If

            Case Else
                Return ""
        End Select





        Return CompoundUnit


    End Function
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
'The Original Code is QuantityConstraintControl.vb.
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
