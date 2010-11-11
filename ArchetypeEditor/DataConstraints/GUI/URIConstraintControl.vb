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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraints/GUI/ClusterControl.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2007-01-09 19:45:11 +0930 (Tue, 09 Jan 2007) $"
'
'

Option Strict On

Public Class UriConstraintControl : Inherits ConstraintControl


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal aFileManager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = aFileManager

        If Main.Instance.DefaultLanguageCode <> "en" Then
            Translate()
        End If
    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents ClusterPanelTop As System.Windows.Forms.Panel
    Friend WithEvents chkURI As System.Windows.Forms.CheckBox
    Friend WithEvents txtRegEx As System.Windows.Forms.TextBox
    Friend WithEvents lblRegex As System.Windows.Forms.Label
    Friend WithEvents LabelTop As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ClusterPanelTop = New System.Windows.Forms.Panel
        Me.LabelTop = New System.Windows.Forms.Label
        Me.chkURI = New System.Windows.Forms.CheckBox
        Me.txtRegEx = New System.Windows.Forms.TextBox
        Me.lblRegex = New System.Windows.Forms.Label
        Me.ClusterPanelTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'ClusterPanelTop
        '
        Me.ClusterPanelTop.Controls.Add(Me.LabelTop)
        Me.ClusterPanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.ClusterPanelTop.Location = New System.Drawing.Point(0, 0)
        Me.ClusterPanelTop.Name = "ClusterPanelTop"
        Me.ClusterPanelTop.Size = New System.Drawing.Size(376, 32)
        Me.ClusterPanelTop.TabIndex = 0
        '
        'LabelTop
        '
        Me.LabelTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTop.Location = New System.Drawing.Point(16, 8)
        Me.LabelTop.Name = "LabelTop"
        Me.LabelTop.Size = New System.Drawing.Size(144, 16)
        Me.LabelTop.TabIndex = 0
        Me.LabelTop.Text = "URI"
        '
        'chkURI
        '
        Me.chkURI.AutoSize = True
        Me.chkURI.Location = New System.Drawing.Point(34, 38)
        Me.chkURI.Name = "chkURI"
        Me.chkURI.Size = New System.Drawing.Size(158, 21)
        Me.chkURI.TabIndex = 1
        Me.chkURI.Text = "Link within EHR only"
        Me.chkURI.UseVisualStyleBackColor = True
        '
        'txtRegEx
        '
        Me.txtRegEx.Location = New System.Drawing.Point(34, 82)
        Me.txtRegEx.Name = "txtRegEx"
        Me.txtRegEx.Size = New System.Drawing.Size(303, 22)
        Me.txtRegEx.TabIndex = 2
        '
        'lblRegex
        '
        Me.lblRegex.AutoSize = True
        Me.lblRegex.Location = New System.Drawing.Point(31, 62)
        Me.lblRegex.Name = "lblRegex"
        Me.lblRegex.Size = New System.Drawing.Size(119, 17)
        Me.lblRegex.TabIndex = 3
        Me.lblRegex.Text = "Constraint on URI"
        '
        'UriConstraintControl
        '
        Me.Controls.Add(Me.txtRegEx)
        Me.Controls.Add(Me.lblRegex)
        Me.Controls.Add(Me.chkURI)
        Me.Controls.Add(Me.ClusterPanelTop)
        Me.Name = "UriConstraintControl"
        Me.Size = New System.Drawing.Size(376, 115)
        Me.ClusterPanelTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Shadows ReadOnly Property Constraint() As Constraint_URI
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_URI)
            Return CType(MyBase.Constraint, Constraint_URI)
        End Get
    End Property

    Public Sub Translate()
        Me.LabelTop.Text = Filemanager.GetOpenEhrTerm(656, Me.LabelTop.Text)
        Me.lblRegex.Text = Filemanager.GetOpenEhrTerm(657, Me.lblRegex.Text)
    End Sub

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control
        MyBase.IsLoading = True

        If Constraint.EhrUriOnly Then
            Me.chkURI.Checked = True
        Else
            Me.chkURI.Checked = False
        End If

        If Constraint.RegularExpression <> Nothing Then
            Me.txtRegEx.Text = Constraint.RegularExpression
        End If

    End Sub

    Private Sub chkURI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkURI.CheckedChanged
        If MyBase.IsLoading Then Return
        Constraint.EhrUriOnly = Me.chkURI.Checked
        mFileManager.FileEdited = True
    End Sub

    Private Sub txtRegEx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRegEx.TextChanged
        If MyBase.IsLoading Then Return
        Constraint.RegularExpression = txtRegEx.Text
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
'The Original Code is ConstraintControl.vb.
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
