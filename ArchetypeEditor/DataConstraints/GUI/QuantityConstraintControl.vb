
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
        If Not DesignMode Then
            Debug.Assert(False)
        End If

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = a_file_manager
        QuantityUnitConstraint.LocalFileManager = mFileManager

        If Main.Instance.DefaultLanguageCode <> "en" Then
            LabelQuantity.Text = Filemanager.GetOpenEhrTerm(115, LabelQuantity.Text)
            lblListProperty.Text = Filemanager.GetOpenEhrTerm(116, lblListProperty.Text)
            lblListUnits.Text = Filemanager.GetOpenEhrTerm(117, lblListUnits.Text)
            QuantityUnitConstraint.cbMinValue.Text = Filemanager.GetOpenEhrTerm(131, QuantityUnitConstraint.cbMinValue.Text)
            QuantityUnitConstraint.cbMaxValue.Text = Filemanager.GetOpenEhrTerm(132, QuantityUnitConstraint.cbMaxValue.Text)
            QuantityUnitConstraint.lblAssumedValue.Text = Filemanager.GetOpenEhrTerm(158, QuantityUnitConstraint.lblAssumedValue.Text)
            AssumedValueLabel.Text = Filemanager.GetOpenEhrTerm(158, AssumedValueLabel.Text)
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
    Friend WithEvents AssumedValuePanel As System.Windows.Forms.Panel
    Friend WithEvents AssumedValueComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AssumedValueLabel As System.Windows.Forms.Label
    Friend WithEvents AssumedValueNumericUpDown As System.Windows.Forms.NumericUpDown
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
        Me.AssumedValuePanel = New System.Windows.Forms.Panel
        Me.AssumedValueComboBox = New System.Windows.Forms.ComboBox
        Me.AssumedValueLabel = New System.Windows.Forms.Label
        Me.AssumedValueNumericUpDown = New System.Windows.Forms.NumericUpDown
        Me.AssumedValuePanel.SuspendLayout()
        CType(Me.AssumedValueNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'listUnits
        '
        Me.listUnits.Location = New System.Drawing.Point(134, 56)
        Me.listUnits.Name = "listUnits"
        Me.listUnits.Size = New System.Drawing.Size(231, 82)
        Me.listUnits.TabIndex = 6
        '
        'lblListProperty
        '
        Me.lblListProperty.BackColor = System.Drawing.Color.Transparent
        Me.lblListProperty.Location = New System.Drawing.Point(8, 32)
        Me.lblListProperty.Name = "lblListProperty"
        Me.lblListProperty.Size = New System.Drawing.Size(87, 16)
        Me.lblListProperty.TabIndex = 1
        Me.lblListProperty.Text = "Property:"
        Me.lblListProperty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblListUnits
        '
        Me.lblListUnits.BackColor = System.Drawing.Color.Transparent
        Me.lblListUnits.Location = New System.Drawing.Point(8, 56)
        Me.lblListUnits.Name = "lblListUnits"
        Me.lblListUnits.Size = New System.Drawing.Size(87, 16)
        Me.lblListUnits.TabIndex = 3
        Me.lblListUnits.Text = "Units:"
        Me.lblListUnits.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'comboPhysicalProperty
        '
        Me.comboPhysicalProperty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.comboPhysicalProperty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.comboPhysicalProperty.DisplayMember = "Text"
        Me.comboPhysicalProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboPhysicalProperty.Location = New System.Drawing.Point(101, 29)
        Me.comboPhysicalProperty.Name = "comboPhysicalProperty"
        Me.comboPhysicalProperty.Size = New System.Drawing.Size(264, 21)
        Me.comboPhysicalProperty.TabIndex = 2
        Me.comboPhysicalProperty.ValueMember = "id"
        '
        'butAddUnit
        '
        Me.butAddUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddUnit.Image = CType(resources.GetObject("butAddUnit.Image"), System.Drawing.Image)
        Me.butAddUnit.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddUnit.Location = New System.Drawing.Point(102, 56)
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
        Me.butRemoveUnit.Location = New System.Drawing.Point(102, 84)
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
        Me.QuantityUnitConstraint.Location = New System.Drawing.Point(-2, 141)
        Me.QuantityUnitConstraint.Name = "QuantityUnitConstraint"
        Me.QuantityUnitConstraint.Size = New System.Drawing.Size(365, 81)
        Me.QuantityUnitConstraint.TabIndex = 7
        Me.QuantityUnitConstraint.Visible = False
        '
        'AssumedValuePanel
        '
        Me.AssumedValuePanel.Controls.Add(Me.AssumedValueComboBox)
        Me.AssumedValuePanel.Controls.Add(Me.AssumedValueLabel)
        Me.AssumedValuePanel.Controls.Add(Me.AssumedValueNumericUpDown)
        Me.AssumedValuePanel.Location = New System.Drawing.Point(4, 224)
        Me.AssumedValuePanel.Name = "AssumedValuePanel"
        Me.AssumedValuePanel.Size = New System.Drawing.Size(366, 27)
        Me.AssumedValuePanel.TabIndex = 14
        Me.AssumedValuePanel.Visible = False
        '
        'AssumedValueComboBox
        '
        Me.AssumedValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AssumedValueComboBox.FormattingEnabled = True
        Me.AssumedValueComboBox.Location = New System.Drawing.Point(245, 4)
        Me.AssumedValueComboBox.Name = "AssumedValueComboBox"
        Me.AssumedValueComboBox.Size = New System.Drawing.Size(116, 21)
        Me.AssumedValueComboBox.TabIndex = 16
        '
        'AssumedValueLabel
        '
        Me.AssumedValueLabel.Location = New System.Drawing.Point(8, 1)
        Me.AssumedValueLabel.Name = "AssumedValueLabel"
        Me.AssumedValueLabel.Size = New System.Drawing.Size(122, 24)
        Me.AssumedValueLabel.TabIndex = 14
        Me.AssumedValueLabel.Text = "Assumed value:"
        Me.AssumedValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AssumedValueNumericUpDown
        '
        Me.AssumedValueNumericUpDown.Location = New System.Drawing.Point(138, 5)
        Me.AssumedValueNumericUpDown.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.AssumedValueNumericUpDown.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.AssumedValueNumericUpDown.Name = "AssumedValueNumericUpDown"
        Me.AssumedValueNumericUpDown.Size = New System.Drawing.Size(100, 20)
        Me.AssumedValueNumericUpDown.TabIndex = 15
        Me.AssumedValueNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AssumedValueNumericUpDown.ThousandsSeparator = True
        Me.AssumedValueNumericUpDown.Value = New Decimal(New Integer() {3, 0, 0, 0})
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
        Me.Controls.Add(Me.AssumedValuePanel)
        Me.Name = "QuantityConstraintControl"
        Me.Size = New System.Drawing.Size(373, 255)
        Me.AssumedValuePanel.ResumeLayout(False)
        CType(Me.AssumedValueNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
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

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        LabelQuantity.Text = AE_Constants.Instance.Quantity
        mIsState = isState
        RemoveHandler comboPhysicalProperty.SelectedIndexChanged, AddressOf comboPhysicalProperty_SelectedIndexChanged

        If comboPhysicalProperty.DataSource Is Nothing Then
            Dim view As New DataView(Main.Instance.PhysicalPropertiesTable)
            comboPhysicalProperty.DataSource = view
            view.Sort = comboPhysicalProperty.DisplayMember
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
                Dim filter As String

                If Main.Instance.DefaultLanguageCode = "en" Then
                    filter = "Text = '" & Constraint.PhysicalPropertyAsString & "'"
                Else
                    filter = "Translated = '" & Constraint.PhysicalPropertyAsString & "'"
                End If

                Dim row() As DataRow = CType(comboPhysicalProperty.DataSource, DataView).Table.Select(filter)

                If row.Length > 0 Then
                    Constraint.OpenEhrCode = CInt(row(0).Item(2))
                    mFileManager.FileEdited = True

                    If row.Length > 0 Then
                        comboPhysicalProperty.SelectedValue = row(0).Item(0)
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
        mIsTime = CInt(comboPhysicalProperty.SelectedValue) = 2

        listUnits.Items.Clear()
        AssumedValueComboBox.Items.Clear()
        AssumedValueNumericUpDown.Minimum = Decimal.MinValue
        AssumedValueNumericUpDown.Maximum = Decimal.MaxValue
        AssumedValueNumericUpDown.Increment = 1
        AssumedValueNumericUpDown.DecimalPlaces = 3
        AssumedValueNumericUpDown.Increment = CDec(0.001)
        AssumedValueNumericUpDown.Value = 0

        For Each unit As Constraint_QuantityUnit In Constraint.Units
            listUnits.Items.Add(unit)
            listUnits.SelectedIndex = 0
            AssumedValueComboBox.Items.Add(unit)

            If unit.HasAssumedValue Then
                AssumedValueComboBox.SelectedItem = unit
                AssumedValueNumericUpDown.Value = CType(unit.AssumedValue, Decimal)
                SetLimitsOnAssumedValueNumericUpDown(unit)
            End If
        Next

        AddHandler comboPhysicalProperty.SelectedIndexChanged, AddressOf comboPhysicalProperty_SelectedIndexChanged
    End Sub

    Private Sub listUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listUnits.SelectedIndexChanged
        ' no change to archetype so currentitem needs to be null
        If listUnits.SelectedIndex >= 0 Then
            Dim wasLoading As Boolean = IsLoading
            IsLoading = True
            QuantityUnitConstraint.Show()
            QuantityUnitConstraint.ShowConstraint(mIsState, CType(listUnits.SelectedItem, Constraint_QuantityUnit))
            AssumedValuePanel.Show()
            IsLoading = wasLoading
        Else
            QuantityUnitConstraint.Reset()
            QuantityUnitConstraint.Hide()
            AssumedValuePanel.Hide()
        End If
    End Sub

    Private Sub butAddUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddUnit.Click
        If Not comboPhysicalProperty.SelectedValue Is Nothing Then
            If Convert.ToInt16(comboPhysicalProperty.SelectedValue) = 64 Then
                'Property is not set so show all the units
                Dim form As New Choose
                Dim view As New DataView(Main.Instance.UnitsTable)
                view.Sort = "Text"
                form.ListChoose.DataSource = view
                form.ListChoose.DisplayMember = "Text"
                form.ListChoose.ValueMember = "property_id"
                form.Set_Single()
                form.ListChoose.SelectionMode = SelectionMode.One
                form.ShowDialog(Me)

                If form.ListChoose.SelectedIndex >= 0 Then
                    'Set the property to the property Id and then set the constraint property
                    comboPhysicalProperty.SelectedValue = form.ListChoose.SelectedValue
                    Constraint.OpenEhrCode = CInt(CType(comboPhysicalProperty.SelectedItem, DataRowView).Item("openEHR"))

                    'Add the unit
                    Dim quantityUnit As New Constraint_QuantityUnit(mIsTime)
                    quantityUnit.Unit = CStr(CType(form.ListChoose.SelectedItem, DataRowView).Item(1))
                    Constraint.Units.Add(quantityUnit, quantityUnit.Unit)
                    listUnits.Items.Add(quantityUnit)
                    listUnits.SelectedItem = quantityUnit
                    AssumedValueComboBox.Items.Add(quantityUnit)

                    mFileManager.FileEdited = True
                End If
            Else
                Dim menu As New ContextMenu
                Dim whereClause As String = ""

                'Exclude units already added
                For Each c_unit As Constraint_QuantityUnit In listUnits.Items
                    If whereClause = "" Then
                        whereClause = " AND NOT (Text = '" & c_unit.Unit & "'"
                    Else
                        whereClause &= " OR Text = '" & c_unit.Unit & "'"
                    End If
                Next

                If whereClause <> "" Then
                    whereClause &= ")"
                End If

                For Each row As DataRow In Main.Instance.UnitsTable.Select("property_id = " & CStr(comboPhysicalProperty.SelectedValue) & whereClause)
                    Dim s As String = CStr(row(1))

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
                        menu.MenuItems.Add(MI)
                    End If
                Next

                menu.Show(butAddUnit, New System.Drawing.Point(5, 5))
            End If
        End If
    End Sub

    Private Sub butRemoveUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveUnit.Click
        If listUnits.SelectedIndex >= 0 Then
            Dim s As String = listUnits.SelectedItem.ToString
            Dim text As String = AE_Constants.Instance.Remove & "'" & s & "'"

            If MessageBox.Show(text, AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                If Constraint.IsTime Then
                    Constraint.Units.Remove(Main.ISO_TimeUnits.GetISOForLanguage(s))
                Else
                    Constraint.Units.Remove(s)
                End If

                Dim i As Integer = Math.Max(0, listUnits.SelectedIndex - 1)
                AssumedValueComboBox.Items.Remove(listUnits.SelectedItem)
                listUnits.Items.Remove(listUnits.SelectedItem)

                If i < listUnits.Items.Count Then
                    listUnits.SelectedIndex = i
                End If

                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub comboPhysicalProperty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If comboPhysicalProperty.Focused Then
            If comboPhysicalProperty.SelectedIndex >= 0 AndAlso MyBase.Constraint.Kind = ConstraintKind.Quantity Then
                Dim wasLoading As Boolean = IsLoading
                IsLoading = True

                'Check to see if it is TIME as Time units are handled with language specific abbreviations
                mIsTime = CInt(comboPhysicalProperty.SelectedValue) = 2

                ' get the openEHR term, which allows translation
                Constraint.OpenEhrCode = CInt(CType(comboPhysicalProperty.SelectedItem, DataRowView).Item("openEHR"))

                'clear the units
                For Each u As Constraint_QuantityUnit In Constraint.Units
                    Constraint.Units.Remove(u.Unit)
                Next

                'Reset
                listUnits.SelectedIndex = -1
                listUnits.Items.Clear()
                AssumedValueComboBox.Items.Clear()
                mFileManager.FileEdited = True
                IsLoading = wasLoading
            End If
        End If
    End Sub

    Private Sub AssumedValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        AssumedValueNumericUpDown.ValueChanged, _
        AssumedValueComboBox.SelectedIndexChanged, _
        QuantityUnitConstraint.Leave

        If Not IsLoading Then
            Dim assumedUnit As Constraint_QuantityUnit = TryCast(AssumedValueComboBox.SelectedItem, Constraint_QuantityUnit)

            For Each unit As Constraint_QuantityUnit In Constraint.Units
                unit.HasAssumedValue = unit Is assumedUnit

                If unit.HasAssumedValue Then
                    SetLimitsOnAssumedValueNumericUpDown(unit)

                    Dim assumedValue As Decimal = 0
                    Decimal.TryParse(AssumedValueNumericUpDown.Text, assumedValue)

                    If AssumedValueNumericUpDown.DecimalPlaces = 0 Then
                        unit.AssumedValue = assumedValue
                    Else
                        unit.AssumedValue = Convert.ToSingle(assumedValue, System.Globalization.NumberFormatInfo.InvariantInfo)
                    End If
                End If
            Next

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub SetLimitsOnAssumedValueNumericUpDown(ByVal unit As Constraint_QuantityUnit)
        Dim precision As Integer = unit.Precision

        If precision < 0 Then
            precision = 3
        End If

        Dim increment As Decimal = CDec(Math.Pow(10, -precision)) ' set the increment to the power of the precision
        Dim minimum As Decimal = unit.MinimumValue
        Dim maximum As Decimal = unit.MaximumValue

        If Not unit.IncludeMinimum Then
            minimum += increment  ' don't include minimum
        End If

        If Not unit.IncludeMaximum Then
            maximum -= increment  ' don't include maximum
        End If

        AssumedValueNumericUpDown.DecimalPlaces = precision
        AssumedValueNumericUpDown.Increment = increment
        AssumedValueNumericUpDown.Minimum = minimum
        AssumedValueNumericUpDown.Maximum = maximum
    End Sub

    Private Sub AddUnit(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsLoading Then
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

                Constraint.Units.Add(quantityUnit, quantityUnit.Unit)
                listUnits.Items.Add(quantityUnit)
                listUnits.SelectedItem = quantityUnit
                AssumedValueComboBox.Items.Add(quantityUnit)
                mFileManager.FileEdited = True
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

