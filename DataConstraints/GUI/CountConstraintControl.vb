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
        Me.NumericAssumed.Location = New System.Drawing.Point(268, 82)
        Me.NumericAssumed.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericAssumed.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.NumericAssumed.Name = "NumericAssumed"
        Me.NumericAssumed.Size = New System.Drawing.Size(88, 22)
        Me.NumericAssumed.TabIndex = 10
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
        Me.numMinValue.Size = New System.Drawing.Size(88, 22)
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
        Me.comboIncludeMin.Size = New System.Drawing.Size(48, 24)
        Me.comboIncludeMin.TabIndex = 4
        Me.comboIncludeMin.Text = ">="
        Me.comboIncludeMin.Visible = False
        '
        'comboIncludeMax
        '
        Me.comboIncludeMax.Items.AddRange(New Object() {"<=", "<"})
        Me.comboIncludeMax.Location = New System.Drawing.Point(212, 56)
        Me.comboIncludeMax.Name = "comboIncludeMax"
        Me.comboIncludeMax.Size = New System.Drawing.Size(48, 24)
        Me.comboIncludeMax.TabIndex = 7
        Me.comboIncludeMax.Text = "<="
        Me.comboIncludeMax.Visible = False
        '
        'lblAssumedValue
        '
        Me.lblAssumedValue.Location = New System.Drawing.Point(68, 83)
        Me.lblAssumedValue.Name = "lblAssumedValue"
        Me.lblAssumedValue.Size = New System.Drawing.Size(184, 24)
        Me.lblAssumedValue.TabIndex = 9
        Me.lblAssumedValue.Text = "Assumed value:"
        Me.lblAssumedValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAssumedValue.Visible = False
        '
        'numPrecision
        '
        Me.numPrecision.Location = New System.Drawing.Point(317, 4)
        Me.numPrecision.Name = "numPrecision"
        Me.numPrecision.Size = New System.Drawing.Size(39, 22)
        Me.numPrecision.TabIndex = 2
        Me.numPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numPrecision.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'chkDecimalPlaces
        '
        Me.chkDecimalPlaces.AutoSize = True
        Me.chkDecimalPlaces.Location = New System.Drawing.Point(134, 4)
        Me.chkDecimalPlaces.Name = "chkDecimalPlaces"
        Me.chkDecimalPlaces.Size = New System.Drawing.Size(156, 21)
        Me.chkDecimalPlaces.TabIndex = 1
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

        IsIntegral = Not (TypeOf Constraint Is Constraint_Real)
        SetMaxAndMin()
        LabelQuantity.Text = AE_Constants.Instance.Count
    End Sub

    Protected Sub SetStateValues()
        lblAssumedValue.Show()
        NumericAssumed.Show()

        If Constraint.HasAssumedValue Then
            NumericAssumed.Value = CType(Constraint.AssumedValue, Decimal)
        End If
    End Sub

    Private Sub SetMaxAndMin()
        If Constraint.HasMaximum Then
            cbMaxValue.Checked = True
            numMaxValue.Value = CDec(Constraint.MaximumValue)

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

            If TypeOf Constraint Is Constraint_Real Then
                numMinValue.Value = CDec(Constraint.MinimumValue)
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

                If CType(Constraint, Constraint_Real).Precision > -1 Then
                    chkDecimalPlaces.Checked = True
                    numPrecision.Value = CType(Constraint, Constraint_Real).Precision
                    numPrecision.Show()
                Else
                    chkDecimalPlaces.Checked = False
                    numPrecision.Hide()
                End If
            End If
        End Set
    End Property

    Protected Overridable Sub MinValueCheckedChanged()
        If cbMinValue.Checked Then
            Constraint.HasMinimum = True
            MinValueChanged()
            Constraint.IncludeMinimum = comboIncludeMin.SelectedIndex <> 1
        Else
            Constraint.HasMinimum = False
        End If
    End Sub

    Protected Sub cbMinValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMinValue.CheckedChanged
        numMinValue.Visible = cbMinValue.Checked
        comboIncludeMin.Visible = cbMinValue.Checked

        'Allows duration control to display the units combobox
        RaiseEvent ChangeDisplay(sender, cbMinValue.Checked Or cbMaxValue.Checked)

        If Not MyBase.IsLoading Then
            MinValueCheckedChanged()
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Sub MaxValueCheckedChanged()
        If cbMaxValue.Checked Then
            Constraint.HasMaximum = True
            MaxValueChanged()
            Constraint.IncludeMaximum = comboIncludeMax.SelectedIndex <> 1
        Else
            Constraint.HasMaximum = False
        End If
    End Sub

    Protected Sub cbMaxValue_CheckChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaxValue.CheckedChanged
        numMaxValue.Visible = cbMaxValue.Checked
        comboIncludeMax.Visible = cbMaxValue.Checked

        'Allows duration control to display the units combobox
        RaiseEvent ChangeDisplay(sender, cbMinValue.Checked Or cbMaxValue.Checked)

        If Not MyBase.IsLoading Then
            MaxValueCheckedChanged()
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overridable Sub MaxValueChanged()
        Dim maximum As Decimal
        Decimal.TryParse(numMaxValue.Text, maximum)
        Constraint.MaximumValue = Convert.ToInt32(maximum, System.Globalization.NumberFormatInfo.InvariantInfo)
    End Sub

    Private Sub numMaxValue_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles numMaxValue.Validating
        If Not MyBase.IsLoading Then
            If numMaxValue.Text = "" Then 'empty string, revert to previous value
                e.Cancel = True
                numMaxValue.Text = CStr(numMaxValue.Value) 'display previous value (previous value remains in .value but display is blank)
            End If
        End If
    End Sub

    Protected Sub numMaxValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMaxValue.TextChanged
        If Not MyBase.IsLoading Then
            Dim minimum, maximum As Decimal
            Decimal.TryParse(numMinValue.Text, minimum)
            Decimal.TryParse(numMaxValue.Text, maximum)

            If NumericAssumed.Visible Then
                NumericAssumed.Maximum = maximum

                If comboIncludeMax.SelectedIndex = 1 Then
                    ' don't include maximum
                    NumericAssumed.Maximum -= NumericAssumed.Increment
                End If
            End If

            If minimum > maximum Then
                numMinValue.Text = CStr(maximum)
            End If

            MaxValueChanged() 'Required here as change value and press Save toolbar button otherwise does not save new value!
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overridable Sub MinValueChanged()
        Dim minimum As Decimal
        Decimal.TryParse(numMaxValue.Text, minimum)
        Constraint.MinimumValue = Convert.ToInt32(minimum, System.Globalization.NumberFormatInfo.InvariantInfo)
    End Sub

    Private Sub numMinValue_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles numMinValue.Validating
        If Not MyBase.IsLoading Then
            If numMinValue.Text = "" Then 'empty string, revert to previous value
                e.Cancel = True
                numMinValue.Text = CStr(numMinValue.Value) 'display previous value (previous value remains in .value but display is blank)
            End If
        End If
    End Sub

    Protected Sub numMinValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMinValue.TextChanged
        If Not MyBase.IsLoading Then
            Dim minimum, maximum As Decimal
            Decimal.TryParse(numMinValue.Text, minimum)
            Decimal.TryParse(numMaxValue.Text, maximum)

            If NumericAssumed.Visible Then
                NumericAssumed.Minimum = minimum

                If comboIncludeMin.SelectedIndex = 1 Then
                    ' don't include minimum
                    NumericAssumed.Minimum += NumericAssumed.Increment
                End If
            End If

            If minimum > maximum Then
                numMaxValue.Text = CStr(minimum)
            End If

            MinValueChanged() 'Required here as change value and press Save toolbar button otherwise does not save new value!
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not MyBase.IsLoading Then
            Debug.Assert(False)
            mFileManager.FileEdited = True
        End If
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

            CType(ActiveControl, NumericUpDown).DecimalPlaces = ctrl.Index

            For i = 0 To ctrl.Parent.MenuItems.Count - 1
                ctrl.Parent.MenuItems(i).Checked = False
            Next

            ctrl.Checked = True
        End If
    End Sub

    Private Sub comboIncludeMin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIncludeMin.SelectedIndexChanged
        If Not MyBase.IsLoading Then
            If comboIncludeMin.SelectedIndex = 1 Then
                Constraint.IncludeMinimum = False

                If NumericAssumed.Visible Then ' is state control
                    NumericAssumed.Minimum = Constraint.MinimumValue + NumericAssumed.Increment
                End If
            Else
                Constraint.IncludeMinimum = True
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub comboIncludeMax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIncludeMax.SelectedIndexChanged
        If Not MyBase.IsLoading Then
            If comboIncludeMax.SelectedIndex = 1 Then
                Constraint.IncludeMaximum = False

                If NumericAssumed.Visible Then
                    NumericAssumed.Maximum = Constraint.MaximumValue - NumericAssumed.Increment
                End If
            Else
                Constraint.IncludeMaximum = True
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub NumericAssumed_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NumericAssumed.Validating
        If Not MyBase.IsLoading Then
            If NumericAssumed.Text = "" Then
                e.Cancel = True
                NumericAssumed.Text = CStr(NumericAssumed.Value) 'display prevous value (previous value remains in .value but display is blank)
            End If
        End If
    End Sub

    Private Sub NumericAssumed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericAssumed.ValueChanged, NumericAssumed.TextChanged
        If Not MyBase.IsLoading Then
            If NumericAssumed.DecimalPlaces = 0 Then
                Constraint.AssumedValue = CDec(NumericAssumed.Text)
            Else
                Constraint.AssumedValue = Convert.ToSingle(CDec(NumericAssumed.Text), System.Globalization.NumberFormatInfo.InvariantInfo)
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
            numMinValue.Increment = CDec(0.001)
        Else
            numMaxValue.DecimalPlaces = i
            numMinValue.DecimalPlaces = i
            Dim d As Decimal
            d = CDec(Math.Pow(10, -i)) ' set the increment to the power of the precision
            numMinValue.Increment = d
            numMaxValue.Increment = d
        End If

        If Not IsLoading Then
            Dim decimalPlaces As Integer = CInt(numPrecision.Value)

            If decimalPlaces > -1 AndAlso CType(Constraint, Constraint_Real).Precision > decimalPlaces Then
                numMaxValue.Value = CDec(Math.Round(CDbl(numMaxValue.Value), CInt(numPrecision.Value)))
                MaxValueChanged()
                numMinValue.Value = CDec(Math.Round(CDbl(numMinValue.Value), CInt(numPrecision.Value)))
                MinValueChanged()
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
                numMinValue.Increment = CDec(0.001)
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

