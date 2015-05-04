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
'	file:        "$URL: http://www.openehr.org/svn/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraints/GUI/MultimediaConstraintControl.vb $"
'	revision:    "$LastChangedRevision: 146 $"
'	last_change: "$LastChangedDate: 2007-09-11 02:48:04 +0200 (Tue, 11 Sep 2007) $"
'
'

Option Strict On

Public Class ParsableConstraintControl : Inherits ConstraintControl

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

        If Main.Instance.DefaultLanguageCode <> "en" Then
            lblParsable.Text = Filemanager.GetOpenEhrTerm(684, lblParsable.Text)
        End If

        Dim d_row As DataRow() = mFileManager.OntologyManager.CodeForGroupID(25, "en") ' must be in English

        For Each r As DataRow In d_row

            'Select Case s(0)
            '    Case "audio"
            '        n = New TreeNode(s(1))
            '        n.Tag = r.Item(1).ToString
            '        n.ImageIndex = 0
            '        TvParsable.Nodes.Item(0).Nodes.Add(n) ' Audio

            '    Case "image"
            '        n = New TreeNode(s(1))
            '        n.Tag = r.Item(1).ToString
            '        n.ImageIndex = 1
            '        TvParsable.Nodes.Item(1).Nodes.Add(n) ' image

            'Case "text"
            Dim key As String = CStr(r.Item(2))
            Dim labelText As String = Filemanager.GetOpenEhrTerm(CInt(r.Item(1)), CStr(r.Item(2)))
            TvParsable.Nodes.Add(key, labelText.Substring(labelText.IndexOf("/"c) + 1), 0)
            TvParsable.Nodes(TvParsable.Nodes.IndexOfKey(key)).Tag = key

            '    Case "video"
            'n = New TreeNode(s(1))
            'n.Tag = r.Item(1).ToString
            'n.ImageIndex = 3
            'TvParsable.Nodes.Item(3).Nodes.Add(n) ' video

            '    Case "application"
            'n = New TreeNode(s(1))
            'n.Tag = r.Item(1).ToString
            'n.ImageIndex = 4
            'TvParsable.Nodes.Item(4).Nodes.Add(n) ' application

            'End Select

        Next

    End Sub



    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private components As System.ComponentModel.IContainer
    Friend WithEvents ImageListMIME As System.Windows.Forms.ImageList
    Friend WithEvents TvParsable As System.Windows.Forms.TreeView
    Friend WithEvents lblParsable As System.Windows.Forms.Label
    Friend WithEvents Panelbottom As System.Windows.Forms.Panel
    Friend WithEvents txtNewFormalism As System.Windows.Forms.TextBox
    Friend WithEvents ButNewItem As System.Windows.Forms.Button
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParsableConstraintControl))
        Me.TvParsable = New System.Windows.Forms.TreeView
        Me.ImageListMIME = New System.Windows.Forms.ImageList(Me.components)
        Me.lblParsable = New System.Windows.Forms.Label
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.Panelbottom = New System.Windows.Forms.Panel
        Me.txtNewFormalism = New System.Windows.Forms.TextBox
        Me.ButNewItem = New System.Windows.Forms.Button
        Me.PanelTop.SuspendLayout()
        Me.Panelbottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TvParsable
        '
        Me.TvParsable.CheckBoxes = True
        Me.TvParsable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TvParsable.HideSelection = False
        Me.TvParsable.HotTracking = True
        Me.TvParsable.ImageIndex = 0
        Me.TvParsable.ImageList = Me.ImageListMIME
        Me.TvParsable.Location = New System.Drawing.Point(0, 32)
        Me.TvParsable.Name = "TvParsable"
        Me.TvParsable.SelectedImageIndex = 0
        Me.TvParsable.Size = New System.Drawing.Size(304, 105)
        Me.TvParsable.TabIndex = 37
        '
        'ImageListMIME
        '
        Me.ImageListMIME.ImageStream = CType(resources.GetObject("ImageListMIME.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListMIME.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListMIME.Images.SetKeyName(0, "")
        '
        'lblParsable
        '
        Me.lblParsable.BackColor = System.Drawing.Color.Transparent
        Me.lblParsable.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParsable.Location = New System.Drawing.Point(8, 13)
        Me.lblParsable.Name = "lblParsable"
        Me.lblParsable.Size = New System.Drawing.Size(96, 27)
        Me.lblParsable.TabIndex = 36
        Me.lblParsable.Text = "Parsable text"
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.lblParsable)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(304, 32)
        Me.PanelTop.TabIndex = 38
        '
        'Panelbottom
        '
        Me.Panelbottom.Controls.Add(Me.ButNewItem)
        Me.Panelbottom.Controls.Add(Me.txtNewFormalism)
        Me.Panelbottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panelbottom.Location = New System.Drawing.Point(0, 137)
        Me.Panelbottom.Name = "Panelbottom"
        Me.Panelbottom.Size = New System.Drawing.Size(304, 31)
        Me.Panelbottom.TabIndex = 39
        '
        'txtNewFormalism
        '
        Me.txtNewFormalism.Location = New System.Drawing.Point(41, 7)
        Me.txtNewFormalism.Name = "txtNewFormalism"
        Me.txtNewFormalism.Size = New System.Drawing.Size(260, 20)
        Me.txtNewFormalism.TabIndex = 0
        '
        'ButNewItem
        '
        Me.ButNewItem.Image = CType(resources.GetObject("ButNewItem.Image"), System.Drawing.Image)
        Me.ButNewItem.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButNewItem.Location = New System.Drawing.Point(11, 3)
        Me.ButNewItem.Name = "ButNewItem"
        Me.ButNewItem.Size = New System.Drawing.Size(24, 24)
        Me.ButNewItem.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ButNewItem, "Add a new term")
        '
        'ParsableConstraintControl
        '
        Me.Controls.Add(Me.TvParsable)
        Me.Controls.Add(Me.Panelbottom)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "ParsableConstraintControl"
        Me.Size = New System.Drawing.Size(304, 168)
        Me.PanelTop.ResumeLayout(False)
        Me.Panelbottom.ResumeLayout(False)
        Me.Panelbottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected ReadOnly Property Constraint() As Constraint_Parsable
        Get
            Return CType(mConstraint, Constraint_Parsable)
        End Get
    End Property

    Protected Overrides Sub SetControlValues(ByVal IsState As Boolean)
        ' set constraint values on control
        For Each s As String In Constraint.AllowableFormalisms
            Dim n As TreeNode

            If Not TvParsable.Nodes.ContainsKey(s) Then
                TvParsable.Nodes.Add(s, s)
                n = TvParsable.Nodes(TvParsable.Nodes.IndexOfKey(s))
                n.Tag = s
            Else
                n = TvParsable.Nodes(TvParsable.Nodes.IndexOfKey(s))
            End If

            n.Checked = True
        Next
    End Sub

    Private Sub TvParsable_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvParsable.AfterCheck
        If Not IsLoading Then
            Dim cp As Generic.List(Of String) = Constraint.AllowableFormalisms

            If e.Node.Checked Then
                Debug.Assert(Not cp.Contains(e.Node.Tag.ToString))
                cp.Add(e.Node.Tag.ToString)
            Else
                Debug.Assert(cp.Contains(e.Node.Tag.ToString))
                cp.Remove(e.Node.Tag.ToString)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Function FindNode(ByVal NodeCol As TreeNodeCollection, ByVal sText As String, Optional ByVal Tag As Boolean = False) As TreeNode
        For Each n As TreeNode In NodeCol
            If Tag Then
                If CStr(n.Tag) = sText Then
                    Return n
                End If
            Else
                If n.Text = sText Then
                    Return n
                End If
            End If

            If Not n Is Nothing Then
                Return n
            End If
        Next

        Return Nothing
    End Function

    Sub TranslateGUI()
        lblParsable.Text = Filemanager.GetOpenEhrTerm(684, "Parsable")
    End Sub

    Private Sub ButNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButNewItem.Click
        If txtNewFormalism.Text.Trim.Length > 2 Then
            Dim s As String = txtNewFormalism.Text.Trim
            TvParsable.Nodes.Add(s, s)
            TvParsable.Nodes(TvParsable.Nodes.IndexOfKey(s)).Tag = s
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
'The Original Code is MultimediaConstraintControl.vb.
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

