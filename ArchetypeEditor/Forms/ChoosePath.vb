'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Public Class frmChoosePath
    Inherits System.Windows.Forms.Form

    Private LanguageCode As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not DesignMode Then
            If Main.Instance.DefaultLanguageCode <> "en" Then
                Text = Filemanager.GetOpenEhrTerm(104, "Choose")
                butCancel.Text = AE_Constants.Instance.Cancel
                butOK.Text = AE_Constants.Instance.OK
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChoosePath))
        Me.tvPaths = New System.Windows.Forms.TreeView
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'tvPaths
        '
        Me.tvPaths.Location = New System.Drawing.Point(24, 24)
        Me.tvPaths.Name = "tvPaths"
        Me.tvPaths.ShowRootLines = False
        Me.tvPaths.Size = New System.Drawing.Size(536, 360)
        Me.tvPaths.TabIndex = 0
        '
        'butOK
        '
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(368, 400)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(88, 24)
        Me.butOK.TabIndex = 1
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(472, 400)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(88, 24)
        Me.butCancel.TabIndex = 2
        Me.butCancel.Text = "Cancel"
        '
        'frmChoosePath
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(584, 430)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.tvPaths)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChoosePath"
        Me.Text = "ChoosePath"
        Me.ResumeLayout(False)

    End Sub

#End Region

    ReadOnly Property Path() As String
        Get
            Return tvPaths.SelectedNode.Tag
        End Get
    End Property

    Public Sub Initialise(ByVal a_LanguageCode As String)
        Dim i, ii As Integer
        Dim LogPath, PhysPath, y, z As String()
        Dim s, clean_s As String
        Dim node As TreeNode = Nothing
        Dim Nodes As TreeNodeCollection
        Dim NodeFound, Enclosed As Boolean
        Dim c As Char

        LanguageCode = a_LanguageCode
        ' get the paths with the language labels
        LogPath = Filemanager.Master.Archetype.Paths(LanguageCode, True)
        ' get the paths with the NodeId labels
        PhysPath = Filemanager.Master.Archetype.Paths(LanguageCode, False)

        For i = 0 To LogPath.Length - 1
            clean_s = ""
            ' check there are no enclosed "/" if so replace with "|"
            For Each c In LogPath(i)
                If c = "[" Then
                    Enclosed = True
                ElseIf c = "]" Then
                    Enclosed = False
                ElseIf c = "/" Then
                    If Enclosed Then
                        c = "|"
                    End If
                End If
                clean_s = clean_s & c
            Next
            y = clean_s.Split("/")
            z = PhysPath(i).Split("/")
            Nodes = tvPaths.Nodes
            ' ignore the last segment as it is always ""
            For ii = 0 To y.Length - 2
                NodeFound = False
                If InStr(y(ii), "[") Then
                    'CHANGE - Sam Heard 2004-05-19
                    ' added else clause
                    ' FIXME need to pass the structure type across
                    ' in logical path
                    s = Mid(y(ii), y(ii).IndexOf("[") + 1, y(ii).LastIndexOf("]"))
                Else
                    s = "[structure]"
                End If
                For Each n As TreeNode In Nodes
                    If PhysPath(i).StartsWith(n.Tag) Then
                        NodeFound = True
                        node = n
                        Exit For
                    End If
                Next
                If Not NodeFound Then
                    node = New TreeNode(s)
                    Nodes.Add(node)
                    ' Changed Sam Heard
                    ' Tag added only if new
                    If node.Parent Is Nothing Then
                        node.Tag = z(ii) & "/"
                    Else
                        node.Tag = node.Parent.Tag & z(ii) & "/"
                    End If
                End If
                Nodes = node.Nodes
            Next
        Next

        tvPaths.ExpandAll()

    End Sub

    Private Sub tvPaths_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvPaths.AfterSelect
        Me.AcceptButton = butOK
    End Sub

    Private Sub tvPaths_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvPaths.DoubleClick
        butOK_Click(sender, e)
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        Me.Close()
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
