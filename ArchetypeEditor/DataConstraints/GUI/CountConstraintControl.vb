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

    Public Sub New(ByVal fileManager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = fileManager
        Translate()
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
    Friend WithEvents chkList As System.Windows.Forms.CheckBox
    Friend WithEvents txtList As System.Windows.Forms.TextBox
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
        Me.chkList = New System.Windows.Forms.CheckBox
        Me.txtList = New System.Windows.Forms.TextBox
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
        Me.LabelQuantity.TabIndex = 0
        Me.LabelQuantity.Text = "Count"
        '
        'cbMinValue
        '
        Me.cbMinValue.Location = New System.Drawing.Point(20, 29)
        Me.cbMinValue.Name = "cbMinValue"
        Me.cbMinValue.Size = New System.Drawing.Size(184, 24)
        Me.cbMinValue.TabIndex = 3
        Me.cbMinValue.Text = "Set min. value"
        '
        'cbMaxValue
        '
        Me.cbMaxValue.Location = New System.Drawing.Point(20, 57)
        Me.cbMaxValue.Name = "cbMaxValue"
        Me.cbMaxValue.Size = New System.Drawing.Size(184, 24)
        Me.cbMaxValue.TabIndex = 6
        Me.cbMaxValue.Text = "Set max. value"
        '
        'NumericAssumed
        '
        Me.NumericAssumed.Location = New System.Drawing.Point(268, 83)
        Me.NumericAssumed.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericAssumed.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.NumericAssumed.Name = "NumericAssumed"
        Me.NumericAssumed.Size = New System.Drawing.Size(100, 20)
        Me.NumericAssumed.TabIndex = 10
        Me.NumericAssumed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericAssumed.ThousandsSeparator = True
        '
        'numMaxValue
        '
        Me.numMaxValue.Location = New System.Drawing.Point(268, 56)
        Me.numMaxValue.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.numMaxValue.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.numMaxValue.Name = "numMaxValue"
        Me.numMaxValue.Size = New System.Drawing.Size(100, 20)
        Me.numMaxValue.TabIndex = 8
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
        Me.numMinValue.Size = New System.Drawing.Size(100, 20)
        Me.numMinValue.TabIndex = 5
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
        Me.comboIncludeMin.Size = New System.Drawing.Size(48, 21)
        Me.comboIncludeMin.TabIndex = 4
        Me.comboIncludeMin.Text = ">="
        Me.comboIncludeMin.Visible = False
        '
        'comboIncludeMax
        '
        Me.comboIncludeMax.Items.AddRange(New Object() {"<=", "<"})
        Me.comboIncludeMax.Location = New System.Drawing.Point(212, 56)
        Me.comboIncludeMax.Name = "comboIncludeMax"
        Me.comboIncludeMax.Size = New System.Drawing.Size(48, 21)
        Me.comboIncludeMax.TabIndex = 7
        Me.comboIncludeMax.Text = "<="
        Me.comboIncludeMax.Visible = False
        '
        'lblAssumedValue
        '
        Me.lblAssumedValue.Location = New System.Drawing.Point(78, 80)
        Me.lblAssumedValue.Name = "lblAssumedValue"
        Me.lblAssumedValue.Size = New System.Drawing.Size(184, 24)
        Me.lblAssumedValue.TabIndex = 9
        Me.lblAssumedValue.Text = "Assumed value:"
        Me.lblAssumedValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numPrecision
        '
        Me.numPrecision.Location = New System.Drawing.Point(317, 4)
        Me.numPrecision.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.numPrecision.Name = "numPrecision"
        Me.numPrecision.Size = New System.Drawing.Size(51, 20)
        Me.numPrecision.TabIndex = 2
        Me.numPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numPrecision.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'chkDecimalPlaces
        '
        Me.chkDecimalPlaces.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDecimalPlaces.AutoSize = True
        Me.chkDecimalPlaces.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDecimalPlaces.Location = New System.Drawing.Point(191, 5)
        Me.chkDecimalPlaces.Name = "chkDecimalPlaces"
        Me.chkDecimalPlaces.Size = New System.Drawing.Size(120, 17)
        Me.chkDecimalPlaces.TabIndex = 1
        Me.chkDecimalPlaces.Text = "Limit decimal places"
        Me.chkDecimalPlaces.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDecimalPlaces.UseVisualStyleBackColor = True
        '
        'chkList
        '
        Me.chkList.AutoSize = True
        Me.chkList.Location = New System.Drawing.Point(20, 111)
        Me.chkList.Name = "chkList"
        Me.chkList.Size = New System.Drawing.Size(96, 17)
        Me.chkList.TabIndex = 11
        Me.chkList.Text = "List (e.g. 1,4,6)"
        Me.chkList.UseVisualStyleBackColor = True
        Me.chkList.Visible = False
        '
        'txtList
        '
        Me.txtList.Location = New System.Drawing.Point(212, 109)
        Me.txtList.Name = "txtList"
        Me.txtList.Size = New System.Drawing.Size(156, 20)
        Me.txtList.TabIndex = 12
        Me.txtList.Visible = False
        '
        'CountConstraintControl
        '
        Me.Controls.Add(Me.txtList)
        Me.Controls.Add(Me.chkList)
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
        Me.Size = New System.Drawing.Size(375, 141)
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

    Public Sub Translate()
        cbMinValue.Text = Filemanager.GetOpenEhrTerm(131, cbMinValue.Text)
        cbMaxValue.Text = Filemanager.GetOpenEhrTerm(132, cbMaxValue.Text)
        lblAssumedValue.Text = Filemanager.GetOpenEhrTerm(158, lblAssumedValue.Text)
        chkDecimalPlaces.Text = Filemanager.GetOpenEhrTerm(649, chkDecimalPlaces.Text)
    End Sub

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        ' set constraint values on control
        NumericAssumed.Minimum = Decimal.MinValue
        NumericAssumed.Maximum = Decimal.MaxValue
        NumericAssumed.Value = 0

        IsIntegral = Not (TypeOf Constraint Is Constraint_Real Or TypeOf Constraint Is Constraint_Currency)
        SetMaxAndMin()

        If Constraint.HasAssumedValue Then
            NumericAssumed.Value = CType(Constraint.AssumedValue, Decimal)
        End If

        If TypeOf Constraint Is Constraint_Currency Then
            LabelQuantity.Text = AE_Constants.Instance.Currency
        Else
            LabelQuantity.Text = AE_Constants.Instance.Count
        End If

        LabelQuantity.Text = AE_Constants.Instance.Count
    End Sub

    Private Sub SetMaxAndMin()
        If Constraint.HasList Then
            chkList.Checked = True
            txtList.Text = Constraint.ValueList
        Else
            Dim realConstraint As Constraint_Real = TryCast(Constraint, Constraint_Real)

            If Constraint.HasMaximum Then
                cbMaxValue.Checked = True

                If Not realConstraint Is Nothing Then
                    Dim max As Single = realConstraint.MaximumRealValue

                    If max > numMaxValue.Maximum Then
                        numMaxValue.Value = numMaxValue.Maximum
                    ElseIf max < numMaxValue.Minimum Then
                        numMaxValue.Value = numMaxValue.Minimum
                    Else
                        numMaxValue.Value = CDec(max)
                    End If
                Else
                    numMaxValue.Value = Constraint.MaximumValue
                End If

                If Constraint.IncludeMaximum Then
                    comboIncludeMax.SelectedIndex = 0
                Else
                    comboIncludeMax.SelectedIndex = 1
                End If
            Else
                cbMaxValue.Checked = False
            End If

            If Constraint.HasMinimum Then
                cbMinValue.Checked = True

                If Not realConstraint Is Nothing Then
                    Dim min As Single = realConstraint.MinimumRealValue

                    If min > numMinValue.Maximum Then
                        numMinValue.Value = numMinValue.Maximum
                    ElseIf min < numMinValue.Minimum Then
                        numMinValue.Value = numMinValue.Minimum
                    Else
                        numMinValue.Value = CDec(min)
                    End If
                Else
                    numMinValue.Value = Constraint.MinimumValue
                End If

                If Constraint.IncludeMinimum Then
                    comboIncludeMin.SelectedIndex = 0
                Else
                    comboIncludeMin.SelectedIndex = 1
                End If
            Else
                cbMinValue.Checked = False
            End If

            CoordinateMinAndMaxValues()
        End If
    End Sub

    Protected Overridable Sub CoordinateMinAndMaxValues()
        Dim minimum As Decimal = numMinValue.Minimum
        Dim maximum As Decimal = numMinValue.Maximum

        If HasConstraint() Then
            Dim realConstraint As Constraint_Real = TryCast(Constraint, Constraint_Real)

            If realConstraint IsNot Nothing Then
                If cbMinValue.Checked Then
                    Decimal.TryParse(numMinValue.Text, minimum)
                End If

                If cbMaxValue.Checked Then
                    Decimal.TryParse(numMaxValue.Text, maximum)
                End If

                realConstraint.MinimumRealValue = minimum
                realConstraint.MaximumRealValue = maximum
            Else
                Dim iMinimum As Integer = Integer.MinValue
                Dim iMaximum As Integer = Integer.MaxValue

                If cbMinValue.Checked Then
                    Integer.TryParse(numMinValue.Text, Globalization.NumberStyles.Integer Or Globalization.NumberStyles.AllowThousands, Nothing, iMinimum)
                End If

                If cbMaxValue.Checked Then
                    Integer.TryParse(numMaxValue.Text, Globalization.NumberStyles.Integer Or Globalization.NumberStyles.AllowThousands, Nothing, iMaximum)
                End If

                minimum = iMinimum
                maximum = iMaximum
                Constraint.MinimumValue = iMinimum
                Constraint.MaximumValue = iMaximum
            End If
        End If

        NumericAssumed.Minimum = minimum
        NumericAssumed.Maximum = maximum

        If comboIncludeMin.SelectedIndex = 1 Then
            NumericAssumed.Minimum += NumericAssumed.Increment  ' don't include minimum
        End If

        If comboIncludeMax.SelectedIndex = 1 Then
            NumericAssumed.Maximum -= NumericAssumed.Increment  ' don't include maximum
        End If

        If minimum > maximum Then
            If numMinValue.Focused Then
                numMaxValue.Text = CStr(minimum)
            Else
                numMinValue.Text = CStr(maximum)
            End If
        End If
    End Sub

    Public WriteOnly Property IsIntegral() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                numPrecision.Hide()
                chkDecimalPlaces.Hide()
                numMaxValue.DecimalPlaces = 0
                numMinValue.DecimalPlaces = 0
                NumericAssumed.DecimalPlaces = 0
                numMinValue.Increment = 1D
            Else
                chkDecimalPlaces.Show()

                If TypeOf Constraint Is Constraint_Real Then
                    If CType(Constraint, Constraint_Real).Precision > -1 Then
                        chkDecimalPlaces.Checked = True
                        numPrecision.Value = CType(Constraint, Constraint_Real).Precision
                        numPrecision.Show()
                    Else
                        chkDecimalPlaces.Checked = False
                        numPrecision.Hide()
                    End If
                ElseIf TypeOf Constraint Is Constraint_Currency Then
                    numPrecision.Value = 2  ' FIXME: Not all currencies have a precision of 2!
                    numPrecision.Hide()
                End If
            End If
        End Set
    End Property

    Protected Sub cbMinValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMinValue.CheckedChanged
        If cbMinValue.Checked And numMaxValue.Visible And numMinValue.Value > numMaxValue.Value Then
            numMinValue.Value = numMaxValue.Value
        End If

        numMinValue.Visible = cbMinValue.Checked
        comboIncludeMin.Visible = cbMinValue.Checked

        'Allows duration control to display the units combobox
        RaiseEvent ChangeDisplay(sender, cbMinValue.Checked Or cbMaxValue.Checked)

        If Not IsLoading Then
            Constraint.IncludeMinimum = cbMinValue.Checked And comboIncludeMin.SelectedIndex <> 1
            Constraint.HasMinimum = cbMinValue.Checked
            CoordinateMinAndMaxValues()
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Sub cbMaxValue_CheckChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaxValue.CheckedChanged
        If cbMaxValue.Checked And numMinValue.Visible And numMaxValue.Value < numMinValue.Value Then
            numMaxValue.Value = numMinValue.Value
        End If

        numMaxValue.Visible = cbMaxValue.Checked
        comboIncludeMax.Visible = cbMaxValue.Checked

        'Allows duration control to display the units combobox
        RaiseEvent ChangeDisplay(sender, cbMinValue.Checked Or cbMaxValue.Checked)

        If Not IsLoading Then
            Constraint.IncludeMaximum = cbMaxValue.Checked And comboIncludeMax.SelectedIndex <> 1
            Constraint.HasMaximum = cbMaxValue.Checked
            CoordinateMinAndMaxValues()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub MinOrMaxValue_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles numMinValue.Validating, numMaxValue.Validating
        If Not IsLoading Then
            Dim c As NumericUpDown = CType(sender, NumericUpDown)

            If c.Text = "" Then 'empty string, revert to previous value
                e.Cancel = True
                c.Text = CStr(c.Value) 'display previous value (previous value remains in .value but display is blank)
            End If
        End If
    End Sub

    Protected Sub MinOrMaxValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMinValue.TextChanged, numMaxValue.TextChanged
        If Not IsLoading Then
            CoordinateMinAndMaxValues()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsLoading Then
            Debug.Assert(False)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub Decimal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Decimal_0.Click, Decimal_1.Click, Decimal_2.Click, Decimal_3.Click
        If TypeOf ActiveControl Is System.Windows.Forms.NumericUpDown Then
            Dim i As Integer
            Dim ctrl As System.Windows.Forms.MenuItem

            Try
                ctrl = CType(sender, System.Windows.Forms.MenuItem)
            Catch
                MessageBox.Show(Err.Description, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            CType(ActiveControl, NumericUpDown).DecimalPlaces = ctrl.Index

            For i = 0 To ctrl.Parent.MenuItems.Count - 1
                ctrl.Parent.MenuItems(i).Checked = False
            Next

            ctrl.Checked = True
        End If
    End Sub

    Private Sub comboIncludeMin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIncludeMin.SelectedIndexChanged
        If Not IsLoading Then
            If comboIncludeMin.SelectedIndex = 1 Then
                Constraint.IncludeMinimum = False
                NumericAssumed.Minimum = Constraint.MinimumValue + NumericAssumed.Increment
            Else
                Constraint.IncludeMinimum = True
            End If

            CoordinateMinAndMaxValues()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub comboIncludeMax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIncludeMax.SelectedIndexChanged
        If Not IsLoading Then
            If comboIncludeMax.SelectedIndex = 1 Then
                Constraint.IncludeMaximum = False
                NumericAssumed.Maximum = Constraint.MaximumValue - NumericAssumed.Increment
            Else
                Constraint.IncludeMaximum = True
            End If

            CoordinateMinAndMaxValues()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub NumericAssumed_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NumericAssumed.Validating
        If Not IsLoading Then
            If NumericAssumed.Text = "" Then
                e.Cancel = True
                NumericAssumed.Text = CStr(NumericAssumed.Value) 'display previous value (previous value remains in .value but display is blank)
            End If
        End If
    End Sub

    Private Sub NumericAssumed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericAssumed.ValueChanged, NumericAssumed.TextChanged
        If Not IsLoading Then
            Dim assumedValue As Decimal = 0
            Decimal.TryParse(NumericAssumed.Text, assumedValue)

            If NumericAssumed.DecimalPlaces = 0 Then
                Constraint.AssumedValue = assumedValue
            Else
                Constraint.AssumedValue = Convert.ToSingle(assumedValue, Globalization.NumberFormatInfo.InvariantInfo)
            End If

            Constraint.HasAssumedValue = True
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub numPrecision_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numPrecision.ValueChanged
        Dim i As Integer = CInt(numPrecision.Value)

        If i > 2 Then
            numMaxValue.DecimalPlaces = 3
            numMinValue.DecimalPlaces = 3
            NumericAssumed.DecimalPlaces = 3
            numMinValue.Increment = CDec(0.001)
        Else
            numMaxValue.DecimalPlaces = i
            numMinValue.DecimalPlaces = i
            NumericAssumed.DecimalPlaces = i
            Dim d As Decimal = CDec(Math.Pow(10, -i)) ' set the increment to the power of the precision
            numMinValue.Increment = d
            numMaxValue.Increment = d
        End If

        If Not IsLoading Then
            Dim decimalPlaces As Integer = CInt(numPrecision.Value)

            If decimalPlaces > -1 AndAlso CType(Constraint, Constraint_Real).Precision > decimalPlaces Then
                numMaxValue.Value = CDec(Math.Round(CDbl(numMaxValue.Value), CInt(numPrecision.Value)))
                numMinValue.Value = CDec(Math.Round(CDbl(numMinValue.Value), CInt(numPrecision.Value)))
                CoordinateMinAndMaxValues()
            End If

            CType(Constraint, Constraint_Real).Precision = decimalPlaces
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkDecimalPlaces_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDecimalPlaces.CheckedChanged
        numPrecision.Visible = chkDecimalPlaces.Checked

        If Not IsLoading Then
            mFileManager.FileEdited = True

            If chkDecimalPlaces.Checked Then
                numPrecision_ValueChanged(sender, e)
            Else
                CType(Constraint, Constraint_Real).Precision = -1
                numMaxValue.DecimalPlaces = 3
                numMinValue.DecimalPlaces = 3
                NumericAssumed.DecimalPlaces = 3
                numMinValue.Increment = CDec(0.001)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub


    Private Sub txtList_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtList.KeyPress
        If e.KeyChar = ","c Then
            e.Handled = (txtList.TextLength = 0 Or txtList.Text.EndsWith(","))
        ElseIf "1234567890".Contains(e.KeyChar.ToString()) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Protected Sub chkList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkList.CheckedChanged
        If Not IsLoading Then
            mFileManager.FileEdited = True

            If chkList.Checked Then
                Constraint.HasList = True
                Constraint.ValueList = txtList.Text
            Else
                Constraint.HasList = False
            End If
        End If

        If chkList.Checked Then
            txtList.Visible = True
            cbMinValue.Checked = False
            cbMinValue.Enabled = False
            cbMaxValue.Checked = False
            cbMaxValue.Enabled = False
        Else
            txtList.Visible = False
            cbMinValue.Enabled = True
            cbMaxValue.Enabled = True
        End If
    End Sub

    Private Sub txtList_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtList.TextChanged
        If Not IsLoading Then
            Constraint.ValueList = txtList.Text
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

