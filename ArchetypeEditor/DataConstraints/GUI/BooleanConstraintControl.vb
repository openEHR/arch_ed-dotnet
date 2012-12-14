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

Public Class BooleanConstraintControl : Inherits ConstraintControl 'AnyConstraintControl

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If Not Me.DesignMode Then
            Debug.Assert(False, "Should not get to here")
        End If

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = a_file_manager

        If Main.Instance.DefaultLanguageCode <> "en" Then
            LabelTrueFalse.Text = Filemanager.GetOpenEhrTerm(157, Me.LabelTrueFalse.Text)
            LabelTrueFalseDefault.Text = Filemanager.GetOpenEhrTerm(600, Me.LabelTrueFalseDefault.Text)
            AssumedValueCheckBox.Text = Filemanager.GetOpenEhrTerm(158, AssumedValueCheckBox.Text)
        End If
    End Sub


    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents PanelBoolean As System.Windows.Forms.Panel
    Friend WithEvents LabelTrueFalseDefault As System.Windows.Forms.Label
    Friend WithEvents AssumedValueListBox As System.Windows.Forms.ListBox
    Friend WithEvents LabelTrueFalse As System.Windows.Forms.Label
    Friend WithEvents gbAssummedValue As System.Windows.Forms.GroupBox
    Friend WithEvents cbTrue As System.Windows.Forms.CheckBox
    Friend WithEvents AssumedValueCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents cbFalse As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelTrueFalseDefault = New System.Windows.Forms.Label
        Me.AssumedValueListBox = New System.Windows.Forms.ListBox
        Me.LabelTrueFalse = New System.Windows.Forms.Label
        Me.cbTrue = New System.Windows.Forms.CheckBox
        Me.cbFalse = New System.Windows.Forms.CheckBox
        Me.AssumedValueCheckBox = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'LabelTrueFalseDefault
        '
        Me.LabelTrueFalseDefault.Location = New System.Drawing.Point(8, 40)
        Me.LabelTrueFalseDefault.Name = "LabelTrueFalseDefault"
        Me.LabelTrueFalseDefault.Size = New System.Drawing.Size(120, 36)
        Me.LabelTrueFalseDefault.TabIndex = 1
        Me.LabelTrueFalseDefault.Text = "Allowed values:"
        Me.LabelTrueFalseDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AssumedValueListBox
        '
        Me.AssumedValueListBox.Location = New System.Drawing.Point(238, 98)
        Me.AssumedValueListBox.Name = "AssumedValueListBox"
        Me.AssumedValueListBox.Size = New System.Drawing.Size(118, 43)
        Me.AssumedValueListBox.TabIndex = 5
        Me.AssumedValueListBox.Visible = False
        '
        'LabelTrueFalse
        '
        Me.LabelTrueFalse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTrueFalse.Location = New System.Drawing.Point(16, 8)
        Me.LabelTrueFalse.Name = "LabelTrueFalse"
        Me.LabelTrueFalse.Size = New System.Drawing.Size(96, 24)
        Me.LabelTrueFalse.TabIndex = 0
        Me.LabelTrueFalse.Text = "True/False"
        '
        'cbTrue
        '
        Me.cbTrue.Checked = True
        Me.cbTrue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbTrue.Location = New System.Drawing.Point(138, 32)
        Me.cbTrue.Name = "cbTrue"
        Me.cbTrue.Size = New System.Drawing.Size(72, 24)
        Me.cbTrue.TabIndex = 2
        Me.cbTrue.Text = "True"
        '
        'cbFalse
        '
        Me.cbFalse.Checked = True
        Me.cbFalse.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbFalse.Location = New System.Drawing.Point(138, 56)
        Me.cbFalse.Name = "cbFalse"
        Me.cbFalse.Size = New System.Drawing.Size(72, 24)
        Me.cbFalse.TabIndex = 3
        Me.cbFalse.Text = "False"
        '
        'AssumedValueCheckBox
        '
        Me.AssumedValueCheckBox.Location = New System.Drawing.Point(48, 98)
        Me.AssumedValueCheckBox.Name = "AssumedValueCheckBox"
        Me.AssumedValueCheckBox.Size = New System.Drawing.Size(184, 24)
        Me.AssumedValueCheckBox.TabIndex = 4
        Me.AssumedValueCheckBox.Text = "Assumed value"
        '
        'BooleanConstraintControl
        '
        Me.Controls.Add(Me.AssumedValueCheckBox)
        Me.Controls.Add(Me.cbFalse)
        Me.Controls.Add(Me.cbTrue)
        Me.Controls.Add(Me.LabelTrueFalseDefault)
        Me.Controls.Add(Me.LabelTrueFalse)
        Me.Controls.Add(Me.AssumedValueListBox)
        Me.Name = "BooleanConstraintControl"
        Me.Size = New System.Drawing.Size(375, 184)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected ReadOnly Property Constraint() As Constraint_Boolean
        Get
            Return CType(mConstraint, Constraint_Boolean)
        End Get
    End Property

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        cbFalse.Text = AE_Constants.Instance.False_
        cbTrue.Text = AE_Constants.Instance.True_

        If Constraint.TrueFalseAllowed Then
            cbTrue.Checked = True
            cbFalse.Checked = True
        ElseIf Constraint.TrueAllowed Then
            cbTrue.Checked = True
            cbFalse.Checked = False
        ElseIf Constraint.FalseAllowed Then
            cbFalse.Checked = True
            cbTrue.Checked = False
        End If

        AssumedValueCheckBox.Checked = Constraint.HasAssumedValue

        If Constraint.HasAssumedValue Then
            If AssumedValueListBox.Items.Contains(AE_Constants.Instance.True_) Then
                If Not cbTrue.Checked Then
                    AssumedValueListBox.Items.Remove(AE_Constants.Instance.True_)
                End If
            Else
                If cbTrue.Checked Then
                    AssumedValueListBox.Items.Add(AE_Constants.Instance.True_)
                End If
            End If

            If AssumedValueListBox.Items.Contains(AE_Constants.Instance.False_) Then
                If Not cbFalse.Checked Then
                    AssumedValueListBox.Items.Remove(AE_Constants.Instance.False_)
                End If
            Else
                If cbFalse.Checked Then
                    AssumedValueListBox.Items.Add(AE_Constants.Instance.False_)
                End If
            End If

            If AssumedValueListBox.Items.Count = 1 Then
                AssumedValueListBox.SelectedIndex = 0
            ElseIf Constraint.AssumedValue Then
                AssumedValueListBox.SelectedIndex = AssumedValueListBox.Items.IndexOf(AE_Constants.Instance.True_)
            Else
                AssumedValueListBox.SelectedIndex = AssumedValueListBox.Items.IndexOf(AE_Constants.Instance.False_)
            End If
        End If
    End Sub

    Private Sub AssumedValueCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssumedValueCheckBox.CheckedChanged
        AssumedValueListBox.Visible = AssumedValueCheckBox.Checked

        If Not IsLoading Then
            Constraint.HasAssumedValue = AssumedValueCheckBox.Checked
            SetControlValues(False)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub AssumedValueListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssumedValueListBox.SelectedIndexChanged
        If Not MyBase.IsLoading AndAlso AssumedValueListBox.SelectedIndex <> -1 Then
            Constraint.HasAssumedValue = True

            Select Case AssumedValueListBox.SelectedItem.ToString
                Case AE_Constants.Instance.True_
                    Constraint.AssumedValue = True
                Case AE_Constants.Instance.False_ ' false
                    Constraint.AssumedValue = False
                Case Else
                    Debug.Assert(False)
            End Select

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbTrue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTrue.CheckedChanged
        If Not MyBase.IsLoading Then
            If cbTrue.Checked Then
                If Constraint.FalseAllowed Then
                    Constraint.AllowTrueOrFalse()
                Else
                    Constraint.AllowTrueOnly()
                End If
            Else
                ' something has to be allowed
                cbFalse.Checked = True
                Constraint.AllowFalseOnly()
            End If

            SetControlValues(False)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbFalse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFalse.CheckedChanged
        If Not MyBase.IsLoading Then
            If cbFalse.Checked Then
                If Constraint.TrueAllowed Then
                    Constraint.AllowTrueOrFalse()
                Else
                    Constraint.AllowFalseOnly()
                End If
            Else
                ' something has to be allowed
                cbTrue.Checked = True
                Constraint.AllowTrueOnly()
            End If

            SetControlValues(False)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub BooleanConstraintControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LabelTrueFalse.Text = AE_Constants.Instance.Boolean_
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
'The Original Code is BooleanConstraintControl.vb.
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
