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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraints/GUI/ClusterControl.vb $"
'	revision:    "$LastChangedRevision: 323 $"
'	last_change: "$LastChangedDate: 2007-01-09 19:45:11 +0930 (Tue, 09 Jan 2007) $"
'
'

Option Strict On

Public Class IdentifierConstraintControl : Inherits ConstraintControl


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal aFileManager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = aFileManager
        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
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
    Friend WithEvents txtIssuer As System.Windows.Forms.TextBox
    Friend WithEvents lblIssuer As System.Windows.Forms.Label
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents lblAssigner As System.Windows.Forms.Label
    Friend WithEvents LabelTop As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ClusterPanelTop = New System.Windows.Forms.Panel
        Me.LabelTop = New System.Windows.Forms.Label
        Me.txtIssuer = New System.Windows.Forms.TextBox
        Me.lblIssuer = New System.Windows.Forms.Label
        Me.txtType = New System.Windows.Forms.TextBox
        Me.lblType = New System.Windows.Forms.Label
        Me.txtId = New System.Windows.Forms.TextBox
        Me.lblAssigner = New System.Windows.Forms.Label
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
        Me.LabelTop.Text = "Identifier"
        '
        'txtIssuer
        '
        Me.txtIssuer.Location = New System.Drawing.Point(34, 58)
        Me.txtIssuer.Name = "txtIssuer"
        Me.txtIssuer.Size = New System.Drawing.Size(303, 22)
        Me.txtIssuer.TabIndex = 2
        '
        'lblIssuer
        '
        Me.lblIssuer.AutoSize = True
        Me.lblIssuer.Location = New System.Drawing.Point(31, 38)
        Me.lblIssuer.Name = "lblIssuer"
        Me.lblIssuer.Size = New System.Drawing.Size(46, 17)
        Me.lblIssuer.TabIndex = 3
        Me.lblIssuer.Text = "Issuer"
        '
        'txtType
        '
        Me.txtType.Location = New System.Drawing.Point(38, 106)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(303, 22)
        Me.txtType.TabIndex = 4
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(35, 86)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(40, 17)
        Me.lblType.TabIndex = 5
        Me.lblType.Text = "Type"
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(38, 154)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(303, 22)
        Me.txtId.TabIndex = 6
        '
        'lblAssigner
        '
        Me.lblAssigner.AutoSize = True
        Me.lblAssigner.Location = New System.Drawing.Point(35, 134)
        Me.lblAssigner.Name = "lblAssigner"
        Me.lblAssigner.Size = New System.Drawing.Size(21, 17)
        Me.lblAssigner.TabIndex = 7
        Me.lblAssigner.Text = "ID"
        '
        'IdentifierConstraintControl
        '
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.lblAssigner)
        Me.Controls.Add(Me.txtType)
        Me.Controls.Add(Me.lblType)
        Me.Controls.Add(Me.txtIssuer)
        Me.Controls.Add(Me.lblIssuer)
        Me.Controls.Add(Me.ClusterPanelTop)
        Me.Name = "IdentifierConstraintControl"
        Me.Size = New System.Drawing.Size(376, 215)
        Me.ClusterPanelTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Shadows ReadOnly Property Constraint() As Constraint_Identifier
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Identifier)
            Return CType(MyBase.Constraint, Constraint_Identifier)
        End Get
    End Property

    Public Sub Translate()
        Me.LabelTop.Text = Filemanager.GetOpenEhrTerm(663, Me.LabelTop.Text)
        Me.lblIssuer.Text = Filemanager.GetOpenEhrTerm(671, Me.lblIssuer.Text)
        Me.lblType.Text = Filemanager.GetOpenEhrTerm(443, Me.lblType.Text)
        Me.lblAssigner.Text = Filemanager.GetOpenEhrTerm(670, Me.lblAssigner.Text)
    End Sub

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control
        MyBase.IsLoading = True


        If Constraint.IssuerRegex <> Nothing Then
            Me.txtIssuer.Text = Constraint.IssuerRegex
        End If

        If Constraint.TypeRegex <> Nothing Then
            Me.txtType.Text = Constraint.TypeRegex
        End If

        If Constraint.IDRegex <> Nothing Then
            Me.txtId.Text = Constraint.IDRegex
        End If


    End Sub

    Private Sub txtIssuer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIssuer.TextChanged
        If MyBase.IsLoading Then Return
        Constraint.IssuerRegex = txtIssuer.Text
        mFileManager.FileEdited = True
    End Sub

    Private Sub txtType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtType.TextChanged
        If MyBase.IsLoading Then Return
        Constraint.TypeRegex = txtType.Text
        mFileManager.FileEdited = True
    End Sub

    Private Sub txtId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtId.TextChanged
        If MyBase.IsLoading Then Return
        Constraint.IDRegex = txtId.Text
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
