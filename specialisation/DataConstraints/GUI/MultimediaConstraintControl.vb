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

Public Class MultiMediaConstraintControl : Inherits ConstraintControl

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
            Me.lblMultiMedia.Text = Filemanager.GetOpenEhrTerm(386, Me.lblMultiMedia.Text)
        End If

        Dim d_row As DataRow() = mFileManager.OntologyManager.CodeForGroupID(19, "en") ' must be in English
        Dim s() As String
        Dim n As TreeNode

        For i As Integer = 0 To TvMultiMedia.GetNodeCount(False) - 1
            TvMultiMedia.Nodes(i).ImageIndex = i
        Next

        For Each r As DataRow In d_row

            s = CType(r.Item(2), String).Split("/".Chars(0))

            Select Case s(0)
                Case "audio"
                    n = New TreeNode(s(1))
                    n.Tag = r.Item(1).ToString
                    n.ImageIndex = 0
                    TvMultiMedia.Nodes.Item(0).Nodes.Add(n) ' Audio

                Case "image"
                    n = New TreeNode(s(1))
                    n.Tag = r.Item(1).ToString
                    n.ImageIndex = 1
                    TvMultiMedia.Nodes.Item(1).Nodes.Add(n) ' image

                Case "text"
                    n = New TreeNode(s(1))
                    n.Tag = r.Item(1).ToString
                    n.ImageIndex = 2
                    TvMultiMedia.Nodes.Item(2).Nodes.Add(n) ' text

                Case "video"
                    n = New TreeNode(s(1))
                    n.Tag = r.Item(1).ToString
                    n.ImageIndex = 3
                    TvMultiMedia.Nodes.Item(3).Nodes.Add(n) ' video

                Case "application"
                    n = New TreeNode(s(1))
                    n.Tag = r.Item(1).ToString
                    n.ImageIndex = 4
                    TvMultiMedia.Nodes.Item(4).Nodes.Add(n) ' application

            End Select

        Next

    End Sub



    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private components As System.ComponentModel.IContainer
    Friend WithEvents ImageListMIME As System.Windows.Forms.ImageList
    Friend WithEvents TvMultiMedia As System.Windows.Forms.TreeView
    Friend WithEvents lblMultiMedia As System.Windows.Forms.Label
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MultiMediaConstraintControl))
        Me.TvMultiMedia = New System.Windows.Forms.TreeView
        Me.ImageListMIME = New System.Windows.Forms.ImageList(Me.components)
        Me.lblMultiMedia = New System.Windows.Forms.Label
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.PanelTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'TvMultiMedia
        '
        Me.TvMultiMedia.CheckBoxes = True
        Me.TvMultiMedia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TvMultiMedia.HideSelection = False
        Me.TvMultiMedia.HotTracking = True
        Me.TvMultiMedia.ImageList = Me.ImageListMIME
        Me.TvMultiMedia.Location = New System.Drawing.Point(0, 32)
        Me.TvMultiMedia.Name = "TvMultiMedia"
        Me.TvMultiMedia.Nodes.AddRange(New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Audio"), New System.Windows.Forms.TreeNode("Image"), New System.Windows.Forms.TreeNode("Text"), New System.Windows.Forms.TreeNode("Video"), New System.Windows.Forms.TreeNode("Application")})
        Me.TvMultiMedia.SelectedImageIndex = 5
        Me.TvMultiMedia.Size = New System.Drawing.Size(304, 136)
        Me.TvMultiMedia.TabIndex = 37
        '
        'ImageListMIME
        '
        Me.ImageListMIME.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageListMIME.ImageStream = CType(resources.GetObject("ImageListMIME.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListMIME.TransparentColor = System.Drawing.Color.Transparent
        '
        'lblMultiMedia
        '
        Me.lblMultiMedia.BackColor = System.Drawing.Color.Transparent
        Me.lblMultiMedia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultiMedia.Location = New System.Drawing.Point(8, 16)
        Me.lblMultiMedia.Name = "lblMultiMedia"
        Me.lblMultiMedia.Size = New System.Drawing.Size(96, 24)
        Me.lblMultiMedia.TabIndex = 36
        Me.lblMultiMedia.Text = "Multi-Media"
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.lblMultiMedia)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(304, 32)
        Me.PanelTop.TabIndex = 38
        '
        'MultiMediaConstraintControl
        '
        Me.Controls.Add(Me.TvMultiMedia)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "MultiMediaConstraintControl"
        Me.Size = New System.Drawing.Size(304, 168)
        Me.PanelTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Shadows ReadOnly Property Constraint() As Constraint_MultiMedia
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_MultiMedia)

            Return CType(MyBase.Constraint, Constraint_MultiMedia)
        End Get
    End Property


    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control

        Dim cp As CodePhrase = Me.Constraint.AllowableValues

        Debug.Assert(cp.TerminologyID = "openEHR")

        For Each n As TreeNode In TvMultiMedia.Nodes
            For Each nn As TreeNode In n.Nodes
                If cp.HasCode(nn.Tag.ToString) Then
                    nn.Checked = True
                    n.Checked = True
                    nn.EnsureVisible()
                End If
            Next
        Next


    End Sub

    Private Sub TvMultiMedia_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvMultiMedia.AfterCheck
        'Dim ae As iArchetypeElementNode

        If MyBase.IsLoading Then Return

        Dim cp As CodePhrase = Me.Constraint.AllowableValues

        If e.Node.Parent Is Nothing Then
            ' check or uncheck all children
            e.Node.Expand()
            For Each n As TreeNode In e.Node.Nodes
                If n.Checked <> e.Node.Checked Then
                    n.Checked = e.Node.Checked
                End If
            Next
        Else
            If e.Node.Checked Then
                Debug.Assert(Not cp.HasCode(e.Node.Tag.ToString))
                cp.Codes.Add(e.Node.Tag.ToString)
                ' check the parent - but stop processing of this
                MyBase.IsLoading = True
                e.Node.Parent.Checked = True
                MyBase.IsLoading = False
            Else
                Debug.Assert(cp.HasCode(e.Node.Tag.ToString))
                cp.Codes.Remove(e.Node.Tag.ToString)
            End If
        End If


        mFileManager.FileEdited = True

    End Sub

    Private Function FindNode(ByVal NodeCol As TreeNodeCollection, ByVal sText As String, _
            Optional ByVal Tag As Boolean = False) As TreeNode

        'Dim n As TreeNode
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
            n = FindNode(n.Nodes, sText, Tag)
            If Not n Is Nothing Then
                Return n
            End If
        Next
        Return Nothing

    End Function

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

