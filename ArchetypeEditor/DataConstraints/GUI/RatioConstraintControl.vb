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

Public Class RatioConstraintControl : Inherits ConstraintControl
    Friend WithEvents PanelMultipleControl As System.Windows.Forms.Panel
    Friend WithEvents TabConstraints As System.Windows.Forms.TabControl
    Private mIsState As Boolean
    Private mDenominator As TabPage
    Private mNumeratorControl As CountConstraintControl
    Friend WithEvents chkListType As System.Windows.Forms.CheckedListBox
    Friend WithEvents cbAllowAll As System.Windows.Forms.CheckBox
    Private mDenominatorControl As CountConstraintControl

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal aFileManager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = aFileManager

        If Main.Instance.DefaultLanguageCode <> "en" Then
            lblRatio.Text = Filemanager.GetOpenEhrTerm(507, lblRatio.Text)
            cbIsIntegral.Text = Filemanager.GetOpenEhrTerm(630, cbIsIntegral.Text)
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents PanelBoolean As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuDataType As System.Windows.Forms.ContextMenu
    Friend WithEvents cbIsIntegral As System.Windows.Forms.CheckBox
    Friend WithEvents lblRatio As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabConstraints = New System.Windows.Forms.TabControl
        Me.PanelMultipleControl = New System.Windows.Forms.Panel
        Me.cbAllowAll = New System.Windows.Forms.CheckBox
        Me.chkListType = New System.Windows.Forms.CheckedListBox
        Me.lblRatio = New System.Windows.Forms.Label
        Me.cbIsIntegral = New System.Windows.Forms.CheckBox
        Me.ContextMenuDataType = New System.Windows.Forms.ContextMenu
        Me.PanelMultipleControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabConstraints
        '
        Me.TabConstraints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabConstraints.Location = New System.Drawing.Point(0, 142)
        Me.TabConstraints.Name = "TabConstraints"
        Me.TabConstraints.SelectedIndex = 0
        Me.TabConstraints.Size = New System.Drawing.Size(392, 66)
        Me.TabConstraints.TabIndex = 3
        '
        'PanelMultipleControl
        '
        Me.PanelMultipleControl.Controls.Add(Me.cbAllowAll)
        Me.PanelMultipleControl.Controls.Add(Me.chkListType)
        Me.PanelMultipleControl.Controls.Add(Me.lblRatio)
        Me.PanelMultipleControl.Controls.Add(Me.cbIsIntegral)
        Me.PanelMultipleControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMultipleControl.Location = New System.Drawing.Point(0, 0)
        Me.PanelMultipleControl.Name = "PanelMultipleControl"
        Me.PanelMultipleControl.Size = New System.Drawing.Size(392, 142)
        Me.PanelMultipleControl.TabIndex = 1
        '
        'cbAllowAll
        '
        Me.cbAllowAll.AutoSize = True
        Me.cbAllowAll.Location = New System.Drawing.Point(208, 3)
        Me.cbAllowAll.Name = "cbAllowAll"
        Me.cbAllowAll.Size = New System.Drawing.Size(80, 21)
        Me.cbAllowAll.TabIndex = 1
        Me.cbAllowAll.Text = "Allow all"
        Me.cbAllowAll.UseVisualStyleBackColor = True
        '
        'chkListType
        '
        Me.chkListType.CheckOnClick = True
        Me.chkListType.FormattingEnabled = True
        Me.chkListType.Items.AddRange(New Object() {"Ratio", "Unitary", "Percent", "Fraction", "Integer and fraction"})
        Me.chkListType.Location = New System.Drawing.Point(204, 27)
        Me.chkListType.Name = "chkListType"
        Me.chkListType.Size = New System.Drawing.Size(167, 106)
        Me.chkListType.TabIndex = 2
        '
        'lblRatio
        '
        Me.lblRatio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRatio.Location = New System.Drawing.Point(12, 3)
        Me.lblRatio.Name = "lblRatio"
        Me.lblRatio.Size = New System.Drawing.Size(94, 24)
        Me.lblRatio.TabIndex = 1
        Me.lblRatio.Text = "Proportion"
        '
        'cbIsIntegral
        '
        Me.cbIsIntegral.Location = New System.Drawing.Point(38, 55)
        Me.cbIsIntegral.Name = "cbIsIntegral"
        Me.cbIsIntegral.Size = New System.Drawing.Size(139, 24)
        Me.cbIsIntegral.TabIndex = 0
        Me.cbIsIntegral.Text = "Integral"
        Me.cbIsIntegral.ThreeState = True
        '
        'RatioConstraintControl
        '
        Me.Controls.Add(Me.TabConstraints)
        Me.Controls.Add(Me.PanelMultipleControl)
        Me.Name = "RatioConstraintControl"
        Me.Size = New System.Drawing.Size(392, 208)
        Me.PanelMultipleControl.ResumeLayout(False)
        Me.PanelMultipleControl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Protected ReadOnly Property Constraint() As Constraint_Proportion
        Get
            Return CType(mConstraint, Constraint_Proportion)
        End Get
    End Property

    Private Function NewCountContraintControl(ByVal c As Constraint, ByVal name As String) As CountConstraintControl
        Dim result As CountConstraintControl = CType(ConstraintControl.CreateConstraintControl(c.Kind, mFileManager), CountConstraintControl)
        Dim tp As New TabPage(name)
        tp.Controls.Add(result)
        result.ShowConstraint(mIsState, c)
        result.Dock = DockStyle.Fill
        TabConstraints.TabPages.Add(tp)
        Return result
    End Function

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        mIsState = isState
        mNumeratorControl = NewCountContraintControl(Constraint.Numerator, Filemanager.GetOpenEhrTerm(450, "Numerator"))
        mDenominatorControl = NewCountContraintControl(Constraint.Denominator, Filemanager.GetOpenEhrTerm(449, "Denominator"))
        mNumeratorControl.IsIntegral = Constraint.IsIntegral
        mDenominatorControl.IsIntegral = Constraint.IsIntegral

        If Constraint.IsIntegralSet Then
            cbIsIntegral.Checked = Constraint.IsIntegral
        Else
            cbIsIntegral.CheckState = CheckState.Indeterminate
        End If

        ' Hide check decimal places in proportion as precision is not available
        mNumeratorControl.chkDecimalPlaces.Hide()
        mDenominatorControl.chkDecimalPlaces.Hide()

        If Constraint.AllowsAllTypes Then
            cbAllowAll.Checked = True
        Else
            cbAllowAll.Checked = False

            For i As Integer = 0 To 4
                chkListType.SetItemChecked(i, Constraint.IsTypeAllowed(i))
            Next
        End If

        SetControlTabs(-1, CheckState.Indeterminate)
    End Sub

    Private Sub cbIsIntegral_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbIsIntegral.CheckStateChanged
        If Not IsLoading Then
            Constraint.IsIntegral = cbIsIntegral.Checked

            If cbIsIntegral.CheckState = CheckState.Indeterminate Then
                Constraint.IsIntegralSet = False
            Else
                Constraint.IsIntegralSet = True
            End If

            ' set the decimals to zero or two on the numvals
            mNumeratorControl.IsIntegral = Constraint.IsIntegral
            mDenominatorControl.IsIntegral = Constraint.IsIntegral
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbAllowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAllowAll.CheckedChanged
        chkListType.Visible = Not cbAllowAll.Checked

        If Not IsLoading Then
            If cbAllowAll.Checked Then
                Constraint.AllowAllTypes()
            End If

            'Set all the choices to true
            IsLoading = True

            For i As Integer = 0 To 4
                chkListType.SetItemChecked(i, True)
            Next

            IsLoading = False

            If TabConstraints.TabPages.Count = 1 Then
                TabConstraints.TabPages.Insert(1, mDenominator)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    ' Return -1 if there is more than one item and the index of the item if there is only one
    Private Function IsSingleOption(ByVal itemIndex As Integer) As Integer
        Dim result As Integer = -1

        If itemIndex > -1 Then
            If chkListType.CheckedIndices.Count = 2 Then
                For Each i As Integer In chkListType.CheckedIndices
                    If i <> itemIndex Then
                        result = i
                    End If
                Next
            End If
        ElseIf chkListType.CheckedIndices.Count = 1 Then
            result = chkListType.CheckedIndices.Item(0)
        End If

        Return result
    End Function

    Private Function UnitaryOrPercent(ByVal itemIndex As Integer) As Boolean
        Dim result As Boolean = False

        If (itemIndex = -1 And chkListType.CheckedIndices.Count = 2) Or chkListType.CheckedIndices.Count = 3 Then
            result = True

            For Each index As Integer In chkListType.CheckedIndices
                If index <> itemIndex Then
                    If index < 1 Or index > 2 Then
                        result = False
                    End If
                End If
            Next
        End If

        Return result
    End Function

    Private Sub SetControlTabs(ByVal itemIndex As Integer, ByVal checkedStatus As CheckState)
        Dim i As Integer

        If checkedStatus = CheckState.Unchecked Then
            i = IsSingleOption(itemIndex)
        Else
            i = IsSingleOption(-1)
        End If

        If i = 2 And checkedStatus <> CheckState.Checked Then
            SetAsPercent()
        ElseIf UnitaryOrPercent(itemIndex) Or _
            (i = 1 And checkedStatus <> CheckState.Checked) Or _
            (i = 1 And itemIndex = 2 And checkedStatus = CheckState.Checked) Or _
            (i = 2 And itemIndex = 1 And checkedStatus = CheckState.Checked) Then
            If TabConstraints.TabPages.Count = 2 Then
                mDenominator = TabConstraints.TabPages(1)
                TabConstraints.TabPages.RemoveAt(1)
            End If
        Else
            If TabConstraints.TabPages.Count = 1 Then
                TabConstraints.TabPages.Insert(1, mDenominator)
            End If
        End If
    End Sub

    Private Sub SetAsPercent()
        If TabConstraints.TabPages.Count > 1 Then
            mDenominator = TabConstraints.TabPages.Item(1)
            TabConstraints.TabPages.RemoveAt(1)
        End If

        Select Case Constraint.Denominator.Kind
            Case ConstraintKind.Real
                Constraint.Denominator.MinimumRealValue = 100
                Constraint.Denominator.MaximumRealValue = 100
        End Select

        If Not IsLoading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkListType_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListType.ItemCheck
        If Not IsLoading Then
            If chkListType.CheckedIndices.Count = 1 AndAlso e.NewValue = CheckState.Unchecked Then
                e.NewValue = CheckState.Checked
            Else
                If e.NewValue = CheckState.Checked Then
                    Constraint.AllowType(e.Index)
                Else
                    Constraint.DisallowType(e.Index)
                End If

                SetControlTabs(e.Index, e.NewValue)
                mFileManager.FileEdited = True
            End If
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
'The Original Code is MultipleConstraintControl.vb.
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
