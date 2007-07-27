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

Public Class CountConstraintControl : Inherits ConstraintControl

    Public Event ChangeDisplay(ByVal sender As Object, ByVal HasMinOrMax As Boolean)

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

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.cbMinValue.Text = Filemanager.GetOpenEhrTerm(131, Me.cbMinValue.Text)
            Me.cbMaxValue.Text = Filemanager.GetOpenEhrTerm(132, Me.cbMaxValue.Text)
            Me.lblAssumedValue.Text = Filemanager.GetOpenEhrTerm(158, Me.lblAssumedValue.Text)
            Me.chkDecimalPlaces.Text = Filemanager.GetOpenEhrTerm(649, Me.chkDecimalPlaces.Text)
        End If

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents PanelQuantity As System.Windows.Forms.Panel
    Friend WithEvents LabelQuantity As System.Windows.Forms.Label
    Friend WithEvents cbMinValue As System.Windows.Forms.CheckBox
    Friend WithEvents cbMaxValue As System.Windows.Forms.CheckBox
    Friend WithEvents NumericAssumed As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMaxValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMinValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents ContextMaxMin As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents Decimal_0 As System.Windows.Forms.MenuItem
    Friend WithEvents Decimal_1 As System.Windows.Forms.MenuItem
    Friend WithEvents Decimal_2 As System.Windows.Forms.MenuItem
    Friend WithEvents Decimal_3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents Increment_1 As System.Windows.Forms.MenuItem
    Friend WithEvents Increment_10 As System.Windows.Forms.MenuItem
    Friend WithEvents Increment_100 As System.Windows.Forms.MenuItem
    Friend WithEvents Increment_1000 As System.Windows.Forms.MenuItem
    Friend WithEvents comboIncludeMin As System.Windows.Forms.ComboBox
    Friend WithEvents comboIncludeMax As System.Windows.Forms.ComboBox
    Friend WithEvents numPrecision As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkDecimalPlaces As System.Windows.Forms.CheckBox
    Friend WithEvents lblAssumedValue As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelQuantity = New System.Windows.Forms.Label
        Me.cbMinValue = New System.Windows.Forms.CheckBox
        Me.cbMaxValue = New System.Windows.Forms.CheckBox
        Me.NumericAssumed = New System.Windows.Forms.NumericUpDown
        Me.numMaxValue = New System.Windows.Forms.NumericUpDown
        Me.numMinValue = New System.Windows.Forms.NumericUpDown
        Me.ContextMaxMin = New System.Windows.Forms.ContextMenu
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.Decimal_0 = New System.Windows.Forms.MenuItem
        Me.Decimal_1 = New System.Windows.Forms.MenuItem
        Me.Decimal_2 = New System.Windows.Forms.MenuItem
        Me.Decimal_3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.Increment_1 = New System.Windows.Forms.MenuItem
        Me.Increment_10 = New System.Windows.Forms.MenuItem
        Me.Increment_100 = New System.Windows.Forms.MenuItem
        Me.Increment_1000 = New System.Windows.Forms.MenuItem
        Me.comboIncludeMin = New System.Windows.Forms.ComboBox
        Me.comboIncludeMax = New System.Windows.Forms.ComboBox
        Me.lblAssumedValue = New System.Windows.Forms.Label
        Me.numPrecision = New System.Windows.Forms.NumericUpDown
        Me.chkDecimalPlaces = New System.Windows.Forms.CheckBox
        CType(Me.NumericAssumed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMaxValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numPrecision, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelQuantity
        '
        Me.LabelQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelQuantity.Location = New System.Drawing.Point(8, 0)
        Me.LabelQuantity.Name = "LabelQuantity"
        Me.LabelQuantity.Size = New System.Drawing.Size(88, 16)
        Me.LabelQuantity.TabIndex = 12
        Me.LabelQuantity.Text = "Count"
        '
        'cbMinValue
        '
        Me.cbMinValue.Location = New System.Drawing.Point(20, 29)
        Me.cbMinValue.Name = "cbMinValue"
        Me.cbMinValue.Size = New System.Drawing.Size(184, 24)
        Me.cbMinValue.TabIndex = 0
        Me.cbMinValue.Text = "Set min. value"
        '
        'cbMaxValue
        '
        Me.cbMaxValue.Location = New System.Drawing.Point(20, 57)
        Me.cbMaxValue.Name = "cbMaxValue"
        Me.cbMaxValue.Size = New System.Drawing.Size(184, 24)
        Me.cbMaxValue.TabIndex = 3
        Me.cbMaxValue.Text = "Set max. value"
        '
        'NumericAssumed
        '
        Me.NumericAssumed.Location = New System.Drawing.Point(268, 82)
        Me.NumericAssumed.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericAssumed.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.NumericAssumed.Name = "NumericAssumed"
        Me.NumericAssumed.Size = New System.Drawing.Size(88, 22)
        Me.NumericAssumed.TabIndex = 7
        Me.NumericAssumed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericAssumed.ThousandsSeparator = True
        Me.NumericAssumed.Visible = False
        '
        'numMaxValue
        '
        Me.numMaxValue.Location = New System.Drawing.Point(268, 56)
        Me.numMaxValue.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.numMaxValue.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.numMaxValue.Name = "numMaxValue"
        Me.numMaxValue.Size = New System.Drawing.Size(88, 22)
        Me.numMaxValue.TabIndex = 5
        Me.numMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numMaxValue.ThousandsSeparator = True
        Me.numMaxValue.Visible = False
        '
        'numMinValue
        '
        Me.numMinValue.Location = New System.Drawing.Point(268, 30)
        Me.numMinValue.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.numMinValue.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.numMinValue.Name = "numMinValue"
        Me.numMinValue.Size = New System.Drawing.Size(88, 22) 'JAR: 22MAY2007, EDT-20 Matched control width to numMaxValue
        Me.numMinValue.TabIndex = 2
        Me.numMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numMinValue.ThousandsSeparator = True
        Me.numMinValue.Visible = False
        '
        'ContextMaxMin
        '
        Me.ContextMaxMin.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3, Me.MenuItem4})
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.Decimal_0, Me.Decimal_1, Me.Decimal_2, Me.Decimal_3})
        Me.MenuItem3.Text = "Decimal places..."
        '
        'Decimal_0
        '
        Me.Decimal_0.Checked = True
        Me.Decimal_0.Index = 0
        Me.Decimal_0.Text = "# (0)"
        '
        'Decimal_1
        '
        Me.Decimal_1.Index = 1
        Me.Decimal_1.Text = "#.# (1)"
        '
        'Decimal_2
        '
        Me.Decimal_2.Index = 2
        Me.Decimal_2.Text = "#.## (2)"
        '
        'Decimal_3
        '
        Me.Decimal_3.Index = 3
        Me.Decimal_3.Text = "#.### (3)"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.Increment_1, Me.Increment_10, Me.Increment_100, Me.Increment_1000})
        Me.MenuItem4.Text = "Increment"
        '
        'Increment_1
        '
        Me.Increment_1.Checked = True
        Me.Increment_1.Index = 0
        Me.Increment_1.Text = "1"
        '
        'Increment_10
        '
        Me.Increment_10.Index = 1
        Me.Increment_10.Text = "10"
        '
        'Increment_100
        '
        Me.Increment_100.Index = 2
        Me.Increment_100.Text = "100"
        '
        'Increment_1000
        '
        Me.Increment_1000.Index = 3
        Me.Increment_1000.Text = "1000"
        '
        'comboIncludeMin
        '
        Me.comboIncludeMin.Items.AddRange(New Object() {">=", ">"})
        Me.comboIncludeMin.Location = New System.Drawing.Point(212, 29)
        Me.comboIncludeMin.Name = "comboIncludeMin"
        Me.comboIncludeMin.Size = New System.Drawing.Size(48, 24)
        Me.comboIncludeMin.TabIndex = 1
        Me.comboIncludeMin.Text = ">="
        Me.comboIncludeMin.Visible = False
        '
        'comboIncludeMax
        '
        Me.comboIncludeMax.Items.AddRange(New Object() {"<=", "<"})
        Me.comboIncludeMax.Location = New System.Drawing.Point(212, 56)
        Me.comboIncludeMax.Name = "comboIncludeMax"
        Me.comboIncludeMax.Size = New System.Drawing.Size(48, 24)
        Me.comboIncludeMax.TabIndex = 4
        Me.comboIncludeMax.Text = "<="
        Me.comboIncludeMax.Visible = False
        '
        'lblAssumedValue
        '
        Me.lblAssumedValue.Location = New System.Drawing.Point(68, 83)
        Me.lblAssumedValue.Name = "lblAssumedValue"
        Me.lblAssumedValue.Size = New System.Drawing.Size(184, 24)
        Me.lblAssumedValue.TabIndex = 13
        Me.lblAssumedValue.Text = "Assumed value:"
        Me.lblAssumedValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAssumedValue.Visible = False
        '
        'numPrecision
        '
        Me.numPrecision.Location = New System.Drawing.Point(317, 4)
        Me.numPrecision.Name = "numPrecision"
        Me.numPrecision.Size = New System.Drawing.Size(39, 22)
        Me.numPrecision.TabIndex = 14
        Me.numPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numPrecision.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'chkDecimalPlaces
        '
        Me.chkDecimalPlaces.AutoSize = True
        Me.chkDecimalPlaces.Location = New System.Drawing.Point(134, 4)
        Me.chkDecimalPlaces.Name = "chkDecimalPlaces"
        Me.chkDecimalPlaces.Size = New System.Drawing.Size(156, 21)
        Me.chkDecimalPlaces.TabIndex = 15
        Me.chkDecimalPlaces.Text = "Limit decimal places"
        Me.chkDecimalPlaces.UseVisualStyleBackColor = True
        '
        'CountConstraintControl
        '
        Me.Controls.Add(Me.chkDecimalPlaces)
        Me.Controls.Add(Me.numPrecision)
        Me.Controls.Add(Me.lblAssumedValue)
        Me.Controls.Add(Me.comboIncludeMax)
        Me.Controls.Add(Me.comboIncludeMin)
        Me.Controls.Add(Me.LabelQuantity)
        Me.Controls.Add(Me.cbMinValue)
        Me.Controls.Add(Me.cbMaxValue)
        Me.Controls.Add(Me.NumericAssumed)
        Me.Controls.Add(Me.numMaxValue)
        Me.Controls.Add(Me.numMinValue)
        Me.Name = "CountConstraintControl"
        Me.Size = New System.Drawing.Size(363, 112)
        CType(Me.NumericAssumed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMaxValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numPrecision, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Protected Shadows ReadOnly Property Constraint() As Constraint_Count
        Get
            Return CType(MyBase.Constraint, Constraint_Count)
        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control

        If IsState Then
            SetStateValues()
        End If

        If TypeOf Me.Constraint Is Constraint_Real Then
            Me.IsIntegral = False
        Else
            Me.IsIntegral = True
        End If

        SetMaxAndMin()

        Me.LabelQuantity.Text = AE_Constants.Instance.Count

    End Sub

    Protected Sub SetStateValues()
        Me.lblAssumedValue.Visible = True
        Me.NumericAssumed.Visible = True
        If Me.Constraint.HasAssumedValue Then
            Me.NumericAssumed.Value = CType(Me.Constraint.AssumedValue, Decimal)
        End If
    End Sub

    Private Sub SetMaxAndMin()
        If Constraint.HasMaximum Then
            Me.cbMaxValue.Checked = True
            Me.numMaxValue.Value = CDec(Constraint.MaximumValue)
            If Constraint.IncludeMaximum Then
                Me.comboIncludeMax.SelectedIndex = 0
            Else
                Me.comboIncludeMax.SelectedIndex = 1
            End If
        Else
            Me.cbMaxValue.Checked = False
        End If

        If Constraint.HasMinimum Then
            Me.cbMinValue.Checked = True
            If TypeOf Me.Constraint Is Constraint_Real Then
                Me.numMinValue.Value = CDec(Constraint.MinimumValue)
            Else
                Me.numMinValue.Value = Constraint.MinimumValue
            End If
            If Constraint.IncludeMinimum Then
                Me.comboIncludeMin.SelectedIndex = 0
            Else
                Me.comboIncludeMin.SelectedIndex = 1
            End If
        Else
            Me.cbMinValue.Checked = False
        End If
    End Sub

    Public WriteOnly Property IsIntegral() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                Me.numPrecision.Visible = False
                Me.chkDecimalPlaces.Visible = False
                Me.numMaxValue.DecimalPlaces = 0
                Me.numMinValue.DecimalPlaces = 0
                Me.NumericAssumed.DecimalPlaces = 0
                Me.numMinValue.Increment = 1D
            Else
                Me.chkDecimalPlaces.Visible = True
                If CType(Me.Constraint, Constraint_Real).Precision > -1 Then
                    chkDecimalPlaces.Checked = True
                    Me.numPrecision.Value = CType(Me.Constraint, Constraint_Real).Precision
                    Me.numPrecision.Visible = True
                Else
                    chkDecimalPlaces.Checked = False
                    Me.numPrecision.Visible = False
                    'chkDecimalPlaces.Checked = True
                    'Me.numPrecision.Visible = True
                End If
            End If
        End Set
    End Property

    Protected Overridable Sub MinValueCheckedChanged()
        If Me.cbMinValue.Checked Then
            Constraint.HasMinimum = True
            MinValueChanged()
            If Me.comboIncludeMin.SelectedIndex = 1 Then
                Constraint.IncludeMinimum = False
            Else
                Constraint.IncludeMinimum = True
            End If

        Else
            Constraint.HasMinimum = False
        End If

    End Sub

    Protected Sub cbMinValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMinValue.CheckedChanged

        Me.numMinValue.Visible = Me.cbMinValue.Checked
        Me.comboIncludeMin.Visible = Me.cbMinValue.Checked

        'Allows duration control to display the units combobox
        RaiseEvent ChangeDisplay(sender, (cbMinValue.Checked Or cbMaxValue.Checked))

        If MyBase.IsLoading Then Return

        MinValueCheckedChanged()


        mFileManager.FileEdited = True

    End Sub

    Protected Sub MaxValueCheckedChanged()

        If Me.cbMaxValue.Checked Then
            Constraint.HasMaximum = True
            MaxValueChanged()
            If Me.comboIncludeMax.SelectedIndex = 1 Then
                Constraint.IncludeMaximum = False
            Else
                Constraint.IncludeMaximum = True
            End If
        Else
            Constraint.HasMaximum = False

        End If
    End Sub

    Protected Sub cbMaxValue_CheckChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaxValue.CheckedChanged

        Me.numMaxValue.Visible = Me.cbMaxValue.Checked
        Me.comboIncludeMax.Visible = Me.cbMaxValue.Checked

        'Allows duration control to display the units combobox
        RaiseEvent ChangeDisplay(sender, (cbMinValue.Checked Or cbMinValue.Checked))

        If MyBase.IsLoading Then Return

        MaxValueCheckedChanged()

        mFileManager.FileEdited = True

    End Sub

    Protected Overridable Sub MaxValueChanged()
        'JAR: 22MAY2007, EDT-20 Do NOT refer directly to .Value property as it triggers numeric reformat 
        'of the display which incorrectly sets the character position to 1.  Use .Text instead!
        'Constraint.MaximumValue = Convert.ToInt32(Me.numMaxValue.Value)
        Constraint.MaximumValue = Convert.ToInt32(CDec(Me.numMaxValue.Text))
    End Sub

    'JAR: 22MAY2007, EDT-20 Validate empty string
    Private Sub numMaxValue_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles numMaxValue.Validating
        If MyBase.IsLoading Then Return

        If numMaxValue.Text = "" Then 'empty string, revert to previous value
            e.Cancel = True
            numMaxValue.Text = CStr(numMaxValue.Value) 'display prevous value (previous value remains in .value but display is blank)
            Return
        End If
    End Sub

    'JAR: 22MAY2007, EDT-20 Restructured validation
    Protected Sub numMaxValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMaxValue.TextChanged

        If MyBase.IsLoading Then Return

        Dim maximum As Decimal
        If numMaxValue.Text <> "" Then maximum = CDec(numMaxValue.Text)

        If Me.NumericAssumed.Visible Then
            'Me.NumericAssumed.Maximum = numMaxValue.Value
            Me.NumericAssumed.Maximum = maximum
            If Me.comboIncludeMax.SelectedIndex = 1 Then
                ' don't include maximum
                Me.NumericAssumed.Maximum -= Me.NumericAssumed.Increment
            End If
        End If

        'If Me.numMaxValue.Value < Me.numMinValue.Value Then
        If maximum < CDec(numMinValue.Text) Then
            'Me.numMinValue.Value = Me.numMaxValue.Value
            numMinValue.Text = CStr(maximum)
        End If

        If MyBase.IsLoading Then Return

        MaxValueChanged() 'Required here as change value and press Save toolbar button otherwise does not save new value!
        mFileManager.FileEdited = True
    End Sub

    Protected Overridable Sub MinValueChanged()
        'JAR: 22MAY2007, EDT-20 Do NOT refer directly to .Value property as it triggers numeric reformat 
        'of the display which incorrectly sets the character position to 1.  Use .Text instead!
        'Constraint.MinimumValue = Convert.ToInt32(Me.numMinValue.Value)
        Constraint.MinimumValue = Convert.ToInt32(CDec(Me.numMinValue.Text))
    End Sub

    'JAR: 22MAY2007, EDT-20 Validate empty string
    Private Sub numMinValue_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles numMinValue.Validating
        If MyBase.IsLoading Then Return

        If numMinValue.Text = "" Then 'empty string
            e.Cancel = True
            numMinValue.Text = CStr(numMinValue.Value) 'display prevous value (previous value remains in .value but display is blank)
            Return
        End If
    End Sub

    'JAR: 22MAY2007, EDT-20 Do NOT refer directly to .Value property within _ValueChanged as it triggers numeric reformat 
    'of the display which incorrectly sets the character position to 1.  Use .Text instead!
    Protected Sub numMinValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMinValue.TextChanged

        If MyBase.IsLoading Then Return

        Dim minimum As Decimal
        If numMinValue.Text <> "" Then minimum = CDec(numMinValue.Text)

        If Me.NumericAssumed.Visible Then
            'Me.NumericAssumed.Minimum = numMinValue.Value
            Me.NumericAssumed.Minimum = minimum
            If Me.comboIncludeMin.SelectedIndex = 1 Then
                ' don't include minimum
                Me.NumericAssumed.Minimum += Me.NumericAssumed.Increment
            End If
        End If

        'If Me.numMinValue.Value > Me.numMaxValue.Value Then
        If minimum > CDec(numMaxValue.Text) Then
            'Me.numMaxValue.Value = Me.numMinValue.Value
            numMaxValue.Text = CStr(minimum)
        End If

        If MyBase.IsLoading Then Return

        MinValueChanged() 'Required here as change value and press Save toolbar button otherwise does not save new value!
        mFileManager.FileEdited = True
    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If MyBase.IsLoading Then Return

        Debug.Assert(False)

        mFileManager.FileEdited = True

    End Sub

    Private Sub Decimal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles Decimal_0.Click, Decimal_1.Click, Decimal_2.Click, Decimal_3.Click
        If TypeOf ActiveControl Is System.Windows.Forms.NumericUpDown Then
            Dim i As Integer
            Dim ctrl As System.Windows.Forms.MenuItem

            Try
                ctrl = CType(sender, System.Windows.Forms.MenuItem)
            Catch
                MessageBox.Show(Err.Description, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try


            CType(Me.ActiveControl, NumericUpDown).DecimalPlaces = ctrl.Index

            For i = 0 To ctrl.Parent.MenuItems.Count - 1
                ctrl.Parent.MenuItems(i).Checked = False
            Next

            ctrl.Checked = True

        End If
    End Sub

    Private Sub comboIncludeMin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIncludeMin.SelectedIndexChanged
        If MyBase.IsLoading Then Return

        If Me.comboIncludeMin.SelectedIndex = 1 Then
            Me.Constraint.IncludeMinimum = False
            If Me.NumericAssumed.Visible Then ' is state control
                Me.NumericAssumed.Minimum = Me.Constraint.MinimumValue + Me.NumericAssumed.Increment
            End If
        Else
            Me.Constraint.IncludeMinimum = True
        End If
        mFileManager.FileEdited = True
    End Sub

    Private Sub comboIncludeMax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIncludeMax.SelectedIndexChanged
        If MyBase.IsLoading Then Return

        If Me.comboIncludeMax.SelectedIndex = 1 Then
            Me.Constraint.IncludeMaximum = False
            If NumericAssumed.Visible Then
                Me.NumericAssumed.Maximum = Me.Constraint.MaximumValue - Me.NumericAssumed.Increment
            End If
        Else
            Me.Constraint.IncludeMaximum = True
        End If
        mFileManager.FileEdited = True
    End Sub

    'JAR: 22MAY2007, EDT-20 Validation for empty string
    Private Sub NumericAssumed_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NumericAssumed.Validating
        If MyBase.IsLoading Then Return
        If NumericAssumed.Text = "" Then 'empty string
            e.Cancel = True
            NumericAssumed.Text = CStr(NumericAssumed.Value) 'display prevous value (previous value remains in .value but display is blank)
            Return
        End If
    End Sub

    'JAR: 22MAY2007, EDT-20 Do NOT refer directly to .Value property within _ValueChanged as it triggers numeric reformat 
    'of the display which incorrectly sets the character position to 1.  Use .Text instead!
    Private Sub NumericAssumed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericAssumed.ValueChanged, NumericAssumed.TextChanged
        If MyBase.IsLoading Then Return

        If Me.NumericAssumed.DecimalPlaces = 0 Then
            'Me.Constraint.AssumedValue = Me.NumericAssumed.Value
            Constraint.AssumedValue = CDec(NumericAssumed.Text)
        Else
            'Me.Constraint.AssumedValue = Convert.ToSingle(Me.NumericAssumed.Value, System.Globalization.NumberFormatInfo.InvariantInfo)
            Constraint.AssumedValue = Convert.ToSingle(CDec(NumericAssumed.Text), System.Globalization.NumberFormatInfo.InvariantInfo)
        End If
        Me.Constraint.HasAssumedValue = True
        mFileManager.FileEdited = True
    End Sub

    Private Sub numPrecision_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numPrecision.ValueChanged
        Dim i As Integer = CInt(numPrecision.Value)
        If i > 2 Then
            Me.numMaxValue.DecimalPlaces = 3
            Me.numMinValue.DecimalPlaces = 3
            Me.numMinValue.Increment = CDec(0.001)
        Else
            Me.numMaxValue.DecimalPlaces = i
            Me.numMinValue.DecimalPlaces = i
            Dim d As Decimal
            d = CDec(Math.Pow(10, -i)) ' set the increment to the power of the precision
            Me.numMinValue.Increment = d
            Me.numMaxValue.Increment = d
        End If

        If Not Me.IsLoading Then
            Dim decimalPlaces As Integer = CInt(Me.numPrecision.Value)
            If decimalPlaces > -1 AndAlso CType(Me.Constraint, Constraint_Real).Precision > decimalPlaces Then
                Me.numMaxValue.Value = CDec(Math.Round(CDbl(Me.numMaxValue.Value), CInt(Me.numPrecision.Value)))
                MaxValueChanged()
                Me.numMinValue.Value = CDec(Math.Round(CDbl(Me.numMinValue.Value), CInt(Me.numPrecision.Value)))
                MinValueChanged()
            End If
            CType(Me.Constraint, Constraint_Real).Precision = decimalPlaces
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkDecimalPlaces_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDecimalPlaces.CheckedChanged
        Me.numPrecision.Visible = chkDecimalPlaces.Checked
        If Not Me.IsLoading Then
            mFileManager.FileEdited = True
            If chkDecimalPlaces.Checked Then
                numPrecision_ValueChanged(sender, e)
            Else
                CType(Me.Constraint, Constraint_Real).Precision = -1
                Me.numMaxValue.DecimalPlaces = 3
                Me.numMinValue.DecimalPlaces = 3
                Me.numMinValue.Increment = CDec(0.001)
            End If
            mFileManager.FileEdited = True
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
'The Original Code is CountConstraintControl.vb.
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

