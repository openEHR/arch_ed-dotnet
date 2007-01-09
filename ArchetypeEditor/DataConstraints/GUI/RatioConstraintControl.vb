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

Public Class RatioConstraintControl : Inherits ConstraintControl 'AnyConstraintControl
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

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = a_file_manager
        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.lblRatio.Text = Filemanager.GetOpenEhrTerm(507, Me.lblRatio.Text)
            Me.cbIsIntegral.Text = Filemanager.GetOpenEhrTerm(630, Me.cbIsIntegral.Text)
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



    Private Shadows ReadOnly Property Constraint() As Constraint_Proportion
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Proportion)

            Return CType(MyBase.Constraint, Constraint_Proportion)
        End Get
    End Property

    Private Function SetFactor(ByVal c As Constraint, ByVal name As String) As CountConstraintControl
        Dim tp As TabPage
        Dim cc As CountConstraintControl

        tp = New TabPage(name)

        cc = CType(ConstraintControl.CreateConstraintControl( _
            c.Type, mFileManager), CountConstraintControl)
        tp.Controls.Add(cc)
        cc.ShowConstraint(mIsState, c)
        cc.Dock = DockStyle.Fill
        Me.TabConstraints.TabPages.Add(tp)
        Return cc
    End Function

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)
        mIsState = IsState
        ' set constraint values on control

        mNumeratorControl = SetFactor(Me.Constraint.Numerator, Filemanager.GetOpenEhrTerm(450, "Numerator"))
        mDenominatorControl = SetFactor(Me.Constraint.Denominator, Filemanager.GetOpenEhrTerm(449, "Denominator"))
        mNumeratorControl.IsIntegral = Me.Constraint.IsIntegral
        mDenominatorControl.IsIntegral = Me.Constraint.IsIntegral
        If Me.Constraint.IsIntegralSet Then
            cbIsIntegral.Checked = Me.Constraint.IsIntegral
        Else
            cbIsIntegral.CheckState = CheckState.Indeterminate
        End If

        If Me.Constraint.AllowAllTypes Then
            Me.cbAllowAll.Checked = True
        Else
            Me.cbAllowAll.Checked = False
            For i As Integer = 0 To 4
                Me.chkListType.SetItemChecked(i, Me.Constraint.IsTypeAllowed(i))
            Next
        End If


    End Sub

    Private WriteOnly Property SetAsPercent() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                mDenominator = Me.TabConstraints.TabPages.Item(1)
                Me.TabConstraints.TabPages.RemoveAt(1)

                Select Case Me.Constraint.Denominator.Type
                    Case ConstraintType.Real
                        CType(Me.Constraint.Denominator, Constraint_Real).MinimumValue = 100
                        CType(Me.Constraint.Denominator, Constraint_Real).MaximumValue = 100
                End Select
            Else
                Me.TabConstraints.TabPages.Add(mDenominator)
            End If

            If MyBase.IsLoading Then Return

            mFileManager.FileEdited = True
        End Set
    End Property

    Private Sub cbIsIntegral_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbIsIntegral.CheckStateChanged

        If MyBase.IsLoading Then Return

        Me.Constraint.IsIntegral = cbIsIntegral.Checked

        If cbIsIntegral.CheckState = CheckState.Indeterminate Then
            Me.Constraint.IsIntegralSet = False
        Else
            Me.Constraint.IsIntegralSet = True
        End If

        ' set the decimals to zero or two on the numvals
        mNumeratorControl.IsIntegral = Me.Constraint.IsIntegral
        mDenominatorControl.IsIntegral = Me.Constraint.IsIntegral

        mFileManager.FileEdited = True
    End Sub

    Private Sub cbAllowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAllowAll.CheckedChanged
        Me.chkListType.Visible = Not cbAllowAll.Checked

        If MyBase.IsLoading Then Return

        Me.Constraint.AllowAllTypes = cbAllowAll.Checked

        'Set all the choices to true
        MyBase.IsLoading = True
        For i As Integer = 0 To 4
            Me.chkListType.SetItemChecked(i, True)
        Next
        MyBase.IsLoading = False

        mFileManager.FileEdited = True

    End Sub

    
    Private Sub chkListType_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListType.ItemCheck

        If MyBase.IsLoading Then Return

        If chkListType.CheckedIndices.Count = 1 AndAlso e.NewValue = CheckState.Unchecked Then
            e.NewValue = CheckState.Checked
            Return
        End If

        If e.NewValue = CheckState.Checked Then
            Me.Constraint.AllowType(e.Index)
        Else
            Me.Constraint.DisAllowType(e.Index)
        End If

        mFileManager.FileEdited = True
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
