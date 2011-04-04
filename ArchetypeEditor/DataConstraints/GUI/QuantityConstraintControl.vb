
'
'	component:   "openEHR Archetype Project"
'	description: "Displays and allows creation and editing of a quantity constraint"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
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
        If Not Me.DesignMode Then
            Debug.Assert(False)
        End If

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = a_file_manager
        Me.QuantityUnitConstraint.LocalFileManager = mFileManager

        If Main.Instance.DefaultLanguageCode <> "en" Then
            LabelQuantity.Text = Filemanager.GetOpenEhrTerm(115, LabelQuantity.Text)
            lblListProperty.Text = Filemanager.GetOpenEhrTerm(116, lblListProperty.Text)
            lblListUnits.Text = Filemanager.GetOpenEhrTerm(117, lblListUnits.Text)
            QuantityUnitConstraint.LabelQuantity.Text = Filemanager.GetOpenEhrTerm(601, "Unit values")
            QuantityUnitConstraint.cbMinValue.Text = Filemanager.GetOpenEhrTerm(131, QuantityUnitConstraint.cbMinValue.Text)
            QuantityUnitConstraint.cbMaxValue.Text = Filemanager.GetOpenEhrTerm(132, QuantityUnitConstraint.cbMaxValue.Text)
            QuantityUnitConstraint.lblAssumedValue.Text = Filemanager.GetOpenEhrTerm(158, QuantityUnitConstraint.lblAssumedValue.Text)
        Else
            QuantityUnitConstraint.LabelQuantity.Text = "Unit values"
        End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QuantityConstraintControl))
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
        Me.listUnits.Location = New System.Drawing.Point(77, 100)
        Me.listUnits.Name = "listUnits"
        Me.listUnits.Size = New System.Drawing.Size(192, 56)
        Me.listUnits.TabIndex = 6
        '
        'lblListProperty
        '
        Me.lblListProperty.BackColor = System.Drawing.Color.Transparent
        Me.lblListProperty.Location = New System.Drawing.Point(24, 29)
        Me.lblListProperty.Name = "lblListProperty"
        Me.lblListProperty.Size = New System.Drawing.Size(224, 16)
        Me.lblListProperty.TabIndex = 1
        Me.lblListProperty.Text = "Property:"
        '
        'lblListUnits
        '
        Me.lblListUnits.BackColor = System.Drawing.Color.Transparent
        Me.lblListUnits.Location = New System.Drawing.Point(28, 78)
        Me.lblListUnits.Name = "lblListUnits"
        Me.lblListUnits.Size = New System.Drawing.Size(136, 16)
        Me.lblListUnits.TabIndex = 3
        Me.lblListUnits.Text = "Units:"
        '
        'comboPhysicalProperty
        '
        Me.comboPhysicalProperty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.comboPhysicalProperty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.comboPhysicalProperty.DisplayMember = "Text"
        Me.comboPhysicalProperty.Location = New System.Drawing.Point(41, 51)
        Me.comboPhysicalProperty.Name = "comboPhysicalProperty"
        Me.comboPhysicalProperty.Size = New System.Drawing.Size(289, 21)
        Me.comboPhysicalProperty.TabIndex = 2
        Me.comboPhysicalProperty.ValueMember = "id"
        '
        'butAddUnit
        '
        Me.butAddUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddUnit.Image = CType(resources.GetObject("butAddUnit.Image"), System.Drawing.Image)
        Me.butAddUnit.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddUnit.Location = New System.Drawing.Point(45, 100)
        Me.butAddUnit.Name = "butAddUnit"
        Me.butAddUnit.Size = New System.Drawing.Size(24, 25)
        Me.butAddUnit.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.butAddUnit, "Add new item")
        '
        'butRemoveUnit
        '
        Me.butRemoveUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveUnit.Image = CType(resources.GetObject("butRemoveUnit.Image"), System.Drawing.Image)
        Me.butRemoveUnit.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveUnit.Location = New System.Drawing.Point(45, 128)
        Me.butRemoveUnit.Name = "butRemoveUnit"
        Me.butRemoveUnit.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveUnit.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.butRemoveUnit, "Remove highlighted item")
        '
        'LabelQuantity
        '
        Me.LabelQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelQuantity.Location = New System.Drawing.Point(16, 8)
        Me.LabelQuantity.Name = "LabelQuantity"
        Me.LabelQuantity.Size = New System.Drawing.Size(88, 16)
        Me.LabelQuantity.TabIndex = 0
        Me.LabelQuantity.Text = "Quantity"
        '
        'QuantityUnitConstraint
        '
        Me.QuantityUnitConstraint.Location = New System.Drawing.Point(0, 171)
        Me.QuantityUnitConstraint.Name = "QuantityUnitConstraint"
        Me.QuantityUnitConstraint.Size = New System.Drawing.Size(384, 120)
        Me.QuantityUnitConstraint.TabIndex = 7
        Me.QuantityUnitConstraint.Visible = False
        '
        'QuantityConstraintControl
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.QuantityUnitConstraint)
        Me.Controls.Add(Me.LabelQuantity)
        Me.Controls.Add(Me.butRemoveUnit)
        Me.Controls.Add(Me.butAddUnit)
        Me.Controls.Add(Me.listUnits)
        Me.Controls.Add(Me.comboPhysicalProperty)
        Me.Controls.Add(Me.lblListProperty)
        Me.Controls.Add(Me.lblListUnits)
        Me.Name = "QuantityConstraintControl"
        Me.Size = New System.Drawing.Size(419, 297)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private mIsState As Boolean
    Private mIsTime As Boolean

    Private Shadows ReadOnly Property Constraint() As Constraint_Quantity
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Quantity)
            Debug.Assert(MyBase.Constraint.Kind = ConstraintKind.Quantity)
            Return CType(MyBase.Constraint, Constraint_Quantity)
        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control
        Me.LabelQuantity.Text = AE_Constants.Instance.Quantity
        mIsState = IsState
        RemoveHandler Me.comboPhysicalProperty.SelectedIndexChanged, AddressOf Me.comboPhysicalProperty_SelectedIndexChanged

        If Me.comboPhysicalProperty.DataSource Is Nothing Then
            Me.comboPhysicalProperty.DataSource = New DataView(Main.Instance.PhysicalPropertiesTable)
            CType(Me.comboPhysicalProperty.DataSource, DataView).Sort = Me.comboPhysicalProperty.DisplayMember
        End If

        ' Locate the property - it can be expressed as:
        ' an english string (old format), or
        ' a code_phrase 'terminology::code'

        If Not Constraint.IsNull Then
            If Constraint.IsCoded Then
                Debug.Assert(Constraint.PhysicalPropertyAsString.StartsWith("openehr"))

                Try
                    Dim i As Integer = Main.Instance.GetIdForPropertyOpenEhrCode(Constraint.OpenEhrCode)

                    If i > -1 Then
                        comboPhysicalProperty.SelectedValue = Main.Instance.GetIdForPropertyOpenEhrCode(Constraint.OpenEhrCode)
                    Else
                        comboPhysicalProperty.SelectedValue = 64
                    End If
                Catch
                    'ToDo: Raise error
                    Debug.Assert(False)
                End Try
            Else   ' Obsolete text string as needs to be language independent
                'And need to update the constraint to the coded version
                Dim d_row() As DataRow

                If Main.Instance.DefaultLanguageCode = "en" Then
                    d_row = CType(comboPhysicalProperty.DataSource, DataTable).Select("Text = '" & Constraint.PhysicalPropertyAsString & "'")
                Else
                    d_row = CType(comboPhysicalProperty.DataSource, DataTable).Select("Translated = '" & Constraint.PhysicalPropertyAsString & "'")
                End If

                If d_row.Length > 0 Then
                    Constraint.OpenEhrCode = CInt(d_row(0).Item(2))
                    mFileManager.FileEdited = True

                    If d_row.Length > 0 Then
                        comboPhysicalProperty.SelectedValue = d_row(0).Item(0)
                    End If
                Else
                    comboPhysicalProperty.SelectedValue = 64 ' Not set
                End If
            End If
        Else
            'Set the property value to Not Set
            comboPhysicalProperty.SelectedValue = 64 ' Not set
        End If

        'Check if the Property is Time - requires special language handling
        If CInt(comboPhysicalProperty.SelectedValue) = 2 Then
            mIsTime = True
        Else
            mIsTime = False
        End If

        listUnits.Items.Clear()

        For Each unit As Constraint_QuantityUnit In Me.Constraint.Units
            listUnits.Items.Add(unit)
        Next

        If listUnits.Items.Count > 0 Then
            listUnits.SelectedIndex = 0
        End If

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
        If (Not Me.comboPhysicalProperty.SelectedValue Is Nothing) Then
            If Convert.ToInt16(Me.comboPhysicalProperty.SelectedValue) = 64 Then
                'Property is not set so show all the units
                Dim Frm As New Choose
                Dim data_view As New DataView(Main.Instance.UnitsTable)
                data_view.Sort = "Text"
                Frm.ListChoose.DataSource = data_view
                Frm.ListChoose.DisplayMember = "Text"
                Frm.ListChoose.ValueMember = "property_id"

                Frm.Set_Single()
                Frm.ListChoose.SelectionMode = SelectionMode.One
                Frm.ShowDialog(Me)
                If Frm.ListChoose.SelectedIndex < 0 Then
                    Return
                End If

                'Set the property to the property Id and then set the constraint property
                Me.comboPhysicalProperty.SelectedValue = Frm.ListChoose.SelectedValue
                Me.Constraint.OpenEhrCode = CInt(CType(Me.comboPhysicalProperty.SelectedItem, DataRowView).Item("openEHR"))

                'Add the unit
                Dim quantityUnit As New Constraint_QuantityUnit(mIsTime)
                quantityUnit.Unit = CStr(CType(Frm.ListChoose.SelectedItem, DataRowView).Item(1))
                Constraint.Units.Add(quantityUnit, quantityUnit.Unit)
                listUnits.Items.Add(quantityUnit)
                listUnits.SelectedItem = quantityUnit

                mFileManager.FileEdited = True

            Else
                Dim c_menu As New ContextMenu
                Dim where_clause As String = ""

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

                For Each d_row As DataRow In Main.Instance.UnitsTable.Select("property_id = " & _
                        CStr(Me.comboPhysicalProperty.SelectedValue) & where_clause)

                    Dim s As String

                    s = CStr(d_row(1))

                    'Omit the "yr" unit as is a duplicate of "a"
                    If Not (mIsTime And s = "yr") Then
                        'Show language abbreviations for time
                        If mIsTime Then
                            s = Main.ISO_TimeUnits.GetLanguageForISO(s)
                        End If

                        Dim MI As MenuItem = New MenuItem()

                        If s.StartsWith("{") Then
                            'If these are numeric then substitute the terms in the appropriate language
                            Dim y() As String = (s.Trim("{}".ToCharArray())).Split("/"c)
                            Dim label As New System.Text.StringBuilder("{")
                            For Each code As String In y
                                Dim ii As Integer
                                If label.ToString() <> "{" Then
                                    label.Append("/")
                                End If
                                If Integer.TryParse(code, ii) Then
                                    label.Append(Filemanager.GetOpenEhrTerm(ii, "#Error#"))
                                Else
                                    label.Append(y)
                                End If
                            Next
                            label.Append("}")
                            MI.Text = label.ToString()
                            MI.Tag = s
                        Else
                            MI.Text = s
                        End If

                        AddHandler MI.Click, AddressOf AddUnit

                        c_menu.MenuItems.Add(MI)
                    End If

                Next
                c_menu.Show(butAddUnit, New System.Drawing.Point(5, 5))
            End If
        End If

    End Sub

    Private Sub butRemoveUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveUnit.Click

        If listUnits.Items.Count > 0 Then
            If listUnits.SelectedIndex > -1 Then

                Dim s As String = listUnits.SelectedItem.ToString
                If MessageBox.Show(AE_Constants.Instance.Remove & "'" & s & "'", _
                        AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, _
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
                        = Windows.Forms.DialogResult.OK Then

                    If Constraint.IsTime Then
                        Constraint.Units.Remove(Main.ISO_TimeUnits.GetISOForLanguage(s))
                    Else
                        Constraint.Units.Remove(s)
                    End If


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
        'Handling added specifically in code to allow loading

        If Me.comboPhysicalProperty.Focused Then
            If (Me.comboPhysicalProperty.SelectedIndex > -1) _
                    AndAlso (MyBase.Constraint.Kind = ConstraintKind.Quantity) Then

                'Check to see if it is TIME as Time units are handled with
                'language specific abbreviations

                If CInt(Me.comboPhysicalProperty.SelectedValue) = 2 Then
                    mIsTime = True
                Else
                    mIsTime = False
                End If

                MyBase.IsLoading = True
                'OBSOLETE
                'Me.Constraint.Physical_property = CStr(Me.comboPhysicalProperty.Text)
                ' get the openEHR term, which allows translation
                Me.Constraint.OpenEhrCode = CInt(CType(Me.comboPhysicalProperty.SelectedItem, DataRowView).Item("openEHR"))

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
        If Not MyBase.IsLoading Then
            Try
                Dim quantityUnit As New Constraint_QuantityUnit(mIsTime)

                Debug.Assert(TypeOf sender Is MenuItem)
                quantityUnit.Unit = CType(sender, MenuItem).Text

                If quantityUnit.isCompoundUnit Then
                    Try
                        quantityUnit.Unit = ChooseCompoundUnits(quantityUnit.Unit, CType(sender, MenuItem).Tag.ToString())
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End Try
                End If

                'SRH: 16 Mar 2009 - EDT-256 - allow no unit
                'If quantityUnit.Unit <> "" Then
                Constraint.Units.Add(quantityUnit, quantityUnit.Unit)
                listUnits.Items.Add(quantityUnit)
                listUnits.SelectedItem = quantityUnit
                mFileManager.FileEdited = True
                'End If
            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
                MessageBox.Show(AE_Constants.Instance.Duplicate_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End If
    End Sub

    Private Function ChooseCompoundUnits(ByVal Units As String, ByVal codes As String) As String
        Dim result As String = ""
        Dim Frm As New Choose
        Dim y, U_2, U_1 As String()
        Dim Phys_Prop As String
        Dim selected_rows As DataRow()
        Dim d_row As DataRow
        Dim curlybrackets As Char() = {"{"c, "}"c}
        Dim isTextCompoundUnits As Boolean

        'SRH Jan 2009 added language independent compound units
        If Not String.IsNullOrEmpty(codes) Then
            y = (codes.Trim("{}".ToCharArray())).Split("/"c)
            'Get the power if there is one
            U_1 = y(0).Split("^"c)
            Phys_Prop = U_1(0)
            selected_rows = Main.Instance.PhysicalPropertiesTable.Select(String.Format("openEHR = {0}", Phys_Prop))

        Else
            'Obsolete
            isTextCompoundUnits = True
            y = (Units.Trim("{}".ToCharArray())).Split("/"c)
            'Get the power if there is one
            U_1 = y(0).Split("^"c)
            Phys_Prop = U_1(0)
            selected_rows = Main.Instance.PhysicalPropertiesTable.Select(String.Format("Text = '{0}'", Phys_Prop))

        End If

        If selected_rows.Length <> 1 Then
            Throw New Exception(String.Format("Error loading property values for {0}", Phys_Prop))
        Else
            Phys_Prop = CStr(selected_rows(0).Item(0))
        End If

        'Now get the units
        selected_rows = Main.Instance.UnitsTable.Select(String.Format("property_id = {0}", Phys_Prop))


        For Each d_row In selected_rows
            ' cannot have not set at this level
            Frm.ListChoose.Items.Add(d_row("Text"))
        Next

        Select Case y.Length
            Case 1
                Frm.Set_Single()
                Frm.ListChoose.SelectionMode = SelectionMode.One

                Frm.ShowDialog(Me)

                If Frm.DialogResult = DialogResult.OK And Frm.ListChoose.SelectedIndex >= 0 Then
                    result = CStr(Frm.ListChoose.SelectedItem)

                    If result = "(none)" Then
                        result = ""
                    End If

                    If U_1.Length = 2 Then
                        result = result & U_1(1)
                    End If
                End If

            Case 2
                Frm.Set_Double()
                Frm.ListBox2.SelectionMode = SelectionMode.One
                Frm.ListChoose.SelectionMode = SelectionMode.One
                Frm.LblForm.Text = Filemanager.GetOpenEhrTerm(668, "Select one unit from the left and one from the right:")

                ' Second property
                'Get the power if there is one
                U_2 = y(1).Split("^"c)
                Phys_Prop = U_2(0)

                If isTextCompoundUnits Then
                    'Obsolete
                    selected_rows = Main.Instance.PhysicalPropertiesTable.Select(String.Format("Text = '{0}'", Phys_Prop))
                Else
                    selected_rows = Main.Instance.PhysicalPropertiesTable.Select(String.Format("openEHR = '{0}'", Phys_Prop))
                End If

                If selected_rows.Length <> 1 Then
                    Throw (New Exception(String.Format("Error loading property values for {0}", Phys_Prop)))
                Else
                    Phys_Prop = CStr(selected_rows(0).Item(0))
                End If

                selected_rows = Main.Instance.UnitsTable.Select("property_id = " & Phys_Prop)

                If selected_rows.Length = 0 Then
                    Throw New Exception(String.Format("Error loading property values for {0}", Phys_Prop))
                Else
                    For Each d_row In selected_rows
                        ' cannot have no denominator or not set
                        If CStr(d_row("Text")) <> "?" Then
                            Frm.ListBox2.Items.Add(d_row("Text"))
                        End If
                    Next

                    Frm.ShowDialog(Me)

                    If Frm.DialogResult = DialogResult.OK Then
                        If Frm.ListChoose.SelectedIndex >= 0 And Frm.ListBox2.SelectedIndex >= 0 Then
                            result = CStr(Frm.ListChoose.SelectedItem)

                            If result = "(none)" Then
                                result = ""
                            End If

                            If U_1.Length = 2 Then
                                result = result & U_1(1)
                            End If

                            result = result & "/" & CStr(Frm.ListBox2.SelectedItem)

                            If U_2.Length = 2 Then
                                result = result & U_2(1)
                            End If
                        ElseIf Frm.ListChoose.SelectedIndex < 0 And Frm.ListBox2.SelectedIndex < 0 Then
                            result = Units
                        End If
                    End If
                End If

        End Select

        Return result
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

