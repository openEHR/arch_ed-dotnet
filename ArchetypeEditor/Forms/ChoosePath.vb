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

Public Class frmChoosePath
    Inherits System.Windows.Forms.Form

    Private languageCode As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                Me.Text = Filemanager.GetOpenEhrTerm(104, "Choose")
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
    Friend WithEvents tvPaths As System.Windows.Forms.TreeView
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents tabCtrlPaths As System.Windows.Forms.TabControl
    Friend WithEvents tabNodes As System.Windows.Forms.TabPage
    Friend WithEvents tabPaths As System.Windows.Forms.TabPage
    Friend WithEvents listNodes As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tvPaths = New System.Windows.Forms.TreeView
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.tabCtrlPaths = New System.Windows.Forms.TabControl
        Me.tabNodes = New System.Windows.Forms.TabPage
        Me.listNodes = New System.Windows.Forms.ListBox
        Me.tabPaths = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tabCtrlPaths.SuspendLayout()
        Me.tabNodes.SuspendLayout()
        Me.tabPaths.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvPaths
        '
        Me.tvPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvPaths.HideSelection = False
        Me.tvPaths.ImageIndex = -1
        Me.tvPaths.Location = New System.Drawing.Point(0, 0)
        Me.tvPaths.Name = "tvPaths"
        Me.tvPaths.SelectedImageIndex = -1
        Me.tvPaths.Size = New System.Drawing.Size(692, 411)
        Me.tvPaths.TabIndex = 0
        '
        'butOK
        '
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(568, 16)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(106, 27)
        Me.butOK.TabIndex = 1
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(448, 16)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(105, 27)
        Me.butCancel.TabIndex = 2
        Me.butCancel.Text = "Cancel"
        '
        'tabCtrlPaths
        '
        Me.tabCtrlPaths.Controls.Add(Me.tabNodes)
        Me.tabCtrlPaths.Controls.Add(Me.tabPaths)
        Me.tabCtrlPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabCtrlPaths.Location = New System.Drawing.Point(0, 0)
        Me.tabCtrlPaths.Name = "tabCtrlPaths"
        Me.tabCtrlPaths.SelectedIndex = 0
        Me.tabCtrlPaths.Size = New System.Drawing.Size(700, 440)
        Me.tabCtrlPaths.TabIndex = 3
        '
        'tabNodes
        '
        Me.tabNodes.Controls.Add(Me.listNodes)
        Me.tabNodes.Location = New System.Drawing.Point(4, 25)
        Me.tabNodes.Name = "tabNodes"
        Me.tabNodes.Size = New System.Drawing.Size(692, 411)
        Me.tabNodes.TabIndex = 0
        Me.tabNodes.Text = "Nodes"
        '
        'listNodes
        '
        Me.listNodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listNodes.ItemHeight = 16
        Me.listNodes.Location = New System.Drawing.Point(0, 0)
        Me.listNodes.Name = "listNodes"
        Me.listNodes.Size = New System.Drawing.Size(692, 404)
        Me.listNodes.TabIndex = 0
        '
        'tabPaths
        '
        Me.tabPaths.Controls.Add(Me.tvPaths)
        Me.tabPaths.Location = New System.Drawing.Point(4, 25)
        Me.tabPaths.Name = "tabPaths"
        Me.tabPaths.Size = New System.Drawing.Size(692, 411)
        Me.tabPaths.TabIndex = 1
        Me.tabPaths.Text = "Paths"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.butCancel)
        Me.Panel1.Controls.Add(Me.butOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 440)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(700, 56)
        Me.Panel1.TabIndex = 4
        '
        'frmChoosePath
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(700, 496)
        Me.Controls.Add(Me.tabCtrlPaths)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmChoosePath"
        Me.Text = "ChoosePath"
        Me.tabCtrlPaths.ResumeLayout(False)
        Me.tabNodes.ResumeLayout(False)
        Me.tabPaths.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ReadOnly Property CodeOfNode() As String
        Get
            If Me.tabCtrlPaths.SelectedIndex = 0 Then
                If listNodes.SelectedIndex > -1 Then
                    Return listNodes.SelectedValue
                Else
                    Return ""
                End If
            Else
                If tvPaths.SelectedNode Is Nothing Then
                    Return ""
                Else
                    Return tvPaths.SelectedNode.Tag
                End If

            End If

        End Get
    End Property

    Public Sub Initialise(ByVal fm As FileManagerLocal)


        languageCode = fm.OntologyManager.LanguageCode

        'Nodes
        Dim dv As New DataView(fm.OntologyManager.TermDefinitionTable)
        dv.RowFilter = "id = '" & languageCode & "'"
        Me.listNodes.DataSource = dv
        Me.listNodes.DisplayMember = "Text"
        Me.listNodes.ValueMember = "Code"

        'Paths is populated before call
    End Sub

    Private Function GetTextFromPath(ByVal path As String, ByVal insideSqBrackets As Boolean) As String

        Dim splitNode As String() = path.Split("[]".ToCharArray)

        If splitNode.Length = 3 Then
            If insideSqBrackets Then
                Return splitNode(1)
            Else
                Return splitNode(0)
            End If
        Else
            Return path
        End If

    End Function

    Private Sub tvPaths_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvPaths.AfterSelect
        Me.AcceptButton = butOK
    End Sub

    Private Sub tvPaths_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvPaths.DoubleClick, listNodes.DoubleClick
        butOK_Click(sender, e)
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub listNodes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listNodes.SelectedIndexChanged
        Me.AcceptButton = butOK
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
'The Original Code is ChoosePath.vb.
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
