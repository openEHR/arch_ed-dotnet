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

Public Class ConstraintForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not DesignMode Then
            If Main.Instance.DefaultLanguageCode <> "en" Then
                DeleteButton.Text = Filemanager.GetOpenEhrTerm(631, "Delete this Constraint")
                CancelCloseButton.Text = AE_Constants.Instance.Cancel
                OkButton.Text = AE_Constants.Instance.OK
            End If
        End If
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents CancelCloseButton As System.Windows.Forms.Button
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents HelpProviderConstraintForm As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConstraintForm))
        Me.DeleteButton = New System.Windows.Forms.Button
        Me.CancelCloseButton = New System.Windows.Forms.Button
        Me.OkButton = New System.Windows.Forms.Button
        Me.HelpProviderConstraintForm = New System.Windows.Forms.HelpProvider
        Me.TopPanel = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'DeleteButton
        '
        Me.DeleteButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeleteButton.Location = New System.Drawing.Point(12, 416)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(265, 24)
        Me.DeleteButton.TabIndex = 1
        Me.DeleteButton.Text = "Delete this Constraint"
        '
        'CancelCloseButton
        '
        Me.CancelCloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelCloseButton.Location = New System.Drawing.Point(392, 416)
        Me.CancelCloseButton.Name = "CancelCloseButton"
        Me.CancelCloseButton.Size = New System.Drawing.Size(88, 24)
        Me.CancelCloseButton.TabIndex = 3
        Me.CancelCloseButton.Text = "Cancel"
        '
        'OkButton
        '
        Me.OkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OkButton.Location = New System.Drawing.Point(292, 416)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(88, 24)
        Me.OkButton.TabIndex = 2
        Me.OkButton.Text = "OK"
        '
        'TopPanel
        '
        Me.TopPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(492, 410)
        Me.TopPanel.TabIndex = 0
        '
        'ConstraintForm
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.CancelCloseButton
        Me.ClientSize = New System.Drawing.Size(492, 442)
        Me.ControlBox = False
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.CancelCloseButton)
        Me.HelpProviderConstraintForm.SetHelpKeyword(Me, "HowTo/Editing/Set_runtime_name.html")
        Me.HelpProviderConstraintForm.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(500, 450)
        Me.Name = "ConstraintForm"
        Me.HelpProviderConstraintForm.SetShowHelp(Me, True)
        Me.Text = "ConstraintForm"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mIsLoading As Boolean = True
    Private mConstraintControl As ConstraintControl

    Protected ReadOnly Property IsLoading() As Boolean
        Get
            Return mIsLoading
        End Get
    End Property

    Public Sub ShowConstraint(ByVal isState As Boolean, ByVal constraint As Constraint, ByVal filemanager As FileManagerLocal)
        mIsLoading = True
        Text = AE_Constants.Instance.MessageBoxCaption
        OkButton.Text = AE_Constants.Instance.OK
        SuspendLayout()

        If Not mConstraintControl Is Nothing Then
            TopPanel.Controls.Remove(mConstraintControl)
            mConstraintControl = Nothing
        End If

        If constraint.Kind <> ConstraintKind.Any Then
            mConstraintControl = ConstraintControl.CreateConstraintControl(constraint.Kind, filemanager)
            TopPanel.Controls.Add(mConstraintControl)
            mConstraintControl.Dock = DockStyle.Fill
            mConstraintControl.ShowConstraint(isState, constraint)
        End If

        ResumeLayout(False)
        mIsLoading = False
    End Sub

    Private Sub butDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        If MessageBox.Show(DeleteButton.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            DialogResult = DialogResult.Ignore
            Close()
        End If
    End Sub

    Private Sub ConstraintForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HelpProviderConstraintForm.HelpNamespace = Main.Instance.Options.HelpLocationPath
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
'The Original Code is ConstraintForm.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
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
