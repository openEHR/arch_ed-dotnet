'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class ConstraintForm
    Inherits System.Windows.Forms.Form


    '    Private AnyConstraints As AnyConstraintControl
    Private mConstraintControl As ConstraintControl
    Private mFileManager As FileManagerLocal


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                Me.butDelete.Text = Filemanager.GetOpenEhrTerm(631, "Delete all")
                Me.butCancel.Text = AE_Constants.Instance.Cancel
                Me.butOK.Text = AE_Constants.Instance.OK
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
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butDelete As System.Windows.Forms.Button
    Friend WithEvents HelpProviderConstraintForm As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PanelBottom = New System.Windows.Forms.Panel
        Me.butDelete = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.HelpProviderConstraintForm = New System.Windows.Forms.HelpProvider
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelBottom
        '
        Me.PanelBottom.Controls.Add(Me.butDelete)
        Me.PanelBottom.Controls.Add(Me.butCancel)
        Me.PanelBottom.Controls.Add(Me.butOK)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 228)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(489, 37)
        Me.PanelBottom.TabIndex = 0
        '
        'butDelete
        '
        Me.butDelete.Location = New System.Drawing.Point(96, 5)
        Me.butDelete.Name = "butDelete"
        Me.butDelete.Size = New System.Drawing.Size(104, 27)
        Me.butDelete.TabIndex = 2
        Me.butDelete.Text = "Delete All"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(288, 5)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(96, 27)
        Me.butCancel.TabIndex = 1
        Me.butCancel.Text = "Cancel"
        '
        'butOK
        '
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(392, 5)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(88, 27)
        Me.butOK.TabIndex = 0
        Me.butOK.Text = "OK"
        '
        'ConstraintForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(489, 265)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelBottom)
        Me.HelpProviderConstraintForm.SetHelpKeyword(Me, "HowTo/Edit data/Set_runtime_name.html")
        Me.HelpProviderConstraintForm.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "ConstraintForm"
        Me.HelpProviderConstraintForm.SetShowHelp(Me, True)
        Me.Text = "ConstraintForm"
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mIsLoading As Boolean = True
    Protected ReadOnly Property IsLoading() As Boolean
        Get
            Return mIsLoading
        End Get
    End Property

    Public Sub ShowConstraint(ByVal IsState As Boolean, ByVal aConstraint As Constraint, ByVal a_file_manager As FileManagerLocal)

        mFileManager = a_file_manager

        mIsLoading = True

        Me.Text = AE_Constants.Instance.MessageBoxCaption
        Me.butOK.Text = AE_Constants.Instance.OK
        Me.SuspendLayout()

        ' ensure that it is not hidden by slot control
        '        Me.txtRuntimeName.Visible = True
        '       Me.txtTermDescription.Visible = True

        Try

            If Not mConstraintControl Is Nothing Then
                Me.Controls.Remove(mConstraintControl)
                mConstraintControl = Nothing
            End If

            If aConstraint.Type <> ConstraintType.Any Then
                mConstraintControl = ConstraintControl.CreateConstraintControl( _
                        aConstraint.Type, mFileManager)

                Me.Controls.Add(mConstraintControl)

                ' Ensures the ZOrder leads to no overlap
                mConstraintControl.Dock = DockStyle.Fill

                ' HKF: 1620
                mConstraintControl.ShowConstraint(IsState, aConstraint)
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Me.ResumeLayout(False)
        mIsLoading = False
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        Me.Close()
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.Close()
    End Sub

    Private Sub butDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDelete.Click
        If MessageBox.Show("Delete all", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.DialogResult = DialogResult.Ignore
            Me.Close()
        End If
    End Sub

    Private Sub ConstraintForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.HelpProviderConstraintForm.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
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
