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

Public Class DateTimeConstraintControl : Inherits ConstraintControl

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

        ' Add the tags to the date time to allow language independent processing
        TvDateTime.Nodes.Item(0).Tag = 11 ' Allow all
        TvDateTime.Nodes.Item(1).Tag = 12 ' Date and time
        TvDateTime.Nodes.Item(1).Nodes.Item(0).Tag = 13 ' Date and partial time
        TvDateTime.Nodes.Item(2).Tag = 14 ' date only
        TvDateTime.Nodes.Item(2).Nodes.Item(0).Tag = 15 ' full date
        TvDateTime.Nodes.Item(2).Nodes.Item(1).Tag = 16 ' partial date
        TvDateTime.Nodes.Item(2).Nodes.Item(1).Nodes.Item(0).Tag = 17 ' partial date with month
        TvDateTime.Nodes.Item(3).Tag = 18 ' time only
        TvDateTime.Nodes.Item(3).Nodes.Item(0).Tag = 19 ' full time
        TvDateTime.Nodes.Item(3).Nodes.Item(1).Tag = 20 ' partial time
        TvDateTime.Nodes.Item(3).Nodes.Item(1).Nodes.Item(0).Tag = 21 ' partial time with minutes

        If Main.Instance.DefaultLanguageCode <> "en" Then
            TvDateTime.Nodes.Item(0).Text = Filemanager.GetOpenEhrTerm(11, "Allow all")
            TvDateTime.Nodes.Item(1).Text = Filemanager.GetOpenEhrTerm(12, "Date and time")
            TvDateTime.Nodes.Item(1).Nodes.Item(0).Text = Filemanager.GetOpenEhrTerm(13, "Date and partial time")
            TvDateTime.Nodes.Item(2).Text = Filemanager.GetOpenEhrTerm(14, "Date only")
            TvDateTime.Nodes.Item(2).Nodes.Item(0).Text = Filemanager.GetOpenEhrTerm(15, "Full date")
            TvDateTime.Nodes.Item(2).Nodes.Item(1).Text = Filemanager.GetOpenEhrTerm(16, "Partial date")
            TvDateTime.Nodes.Item(2).Nodes.Item(1).Nodes.Item(0).Text = Filemanager.GetOpenEhrTerm(17, "Partial date with month")
            TvDateTime.Nodes.Item(3).Text = Filemanager.GetOpenEhrTerm(18, "Time only")
            TvDateTime.Nodes.Item(3).Nodes.Item(0).Text = Filemanager.GetOpenEhrTerm(19, "Full time")
            TvDateTime.Nodes.Item(3).Nodes.Item(1).Text = Filemanager.GetOpenEhrTerm(20, "Partial time")
            TvDateTime.Nodes.Item(3).Nodes.Item(1).Nodes.Item(0).Text = Filemanager.GetOpenEhrTerm(21, "Partial time with minutes")
            LabelDateTime.Text = Filemanager.GetOpenEhrTerm(161, Me.LabelDateTime.Text)
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TvDateTime As System.Windows.Forms.TreeView
    Friend WithEvents LabelDateTime As System.Windows.Forms.Label
    Friend WithEvents ImageListDateTime As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(DateTimeConstraintControl))
        Me.TvDateTime = New System.Windows.Forms.TreeView
        Me.LabelDateTime = New System.Windows.Forms.Label
        Me.ImageListDateTime = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'TvDateTime
        '
        Me.TvDateTime.HideSelection = False
        Me.TvDateTime.HotTracking = True
        Me.TvDateTime.ImageList = Me.ImageListDateTime
        Me.TvDateTime.Location = New System.Drawing.Point(56, 40)
        Me.TvDateTime.Name = "TvDateTime"
        Me.TvDateTime.Nodes.AddRange(New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Allow all"), New System.Windows.Forms.TreeNode("Date and time", New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Date and partial time")}), New System.Windows.Forms.TreeNode("Date only", New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Full date"), New System.Windows.Forms.TreeNode("Partial date", New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Partial date with month")})}), New System.Windows.Forms.TreeNode("Time only", New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Full time hh:mm:ss"), New System.Windows.Forms.TreeNode("Partial time", New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Partial time with minutes")})})})
        Me.TvDateTime.SelectedImageIndex = 1
        Me.TvDateTime.Size = New System.Drawing.Size(240, 120)
        Me.TvDateTime.TabIndex = 37
        '
        'LabelDateTime
        '
        Me.LabelDateTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelDateTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDateTime.Location = New System.Drawing.Point(8, 8)
        Me.LabelDateTime.Name = "LabelDateTime"
        Me.LabelDateTime.Size = New System.Drawing.Size(96, 24)
        Me.LabelDateTime.TabIndex = 36
        Me.LabelDateTime.Text = "Date && Time"
        '
        'ImageListDateTime
        '
        Me.ImageListDateTime.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageListDateTime.ImageStream = CType(resources.GetObject("ImageListDateTime.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListDateTime.TransparentColor = System.Drawing.Color.Transparent
        '
        'DateTimeConstraintControl
        '
        Me.Controls.Add(Me.TvDateTime)
        Me.Controls.Add(Me.LabelDateTime)
        Me.Name = "DateTimeConstraintControl"
        Me.Size = New System.Drawing.Size(304, 168)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected ReadOnly Property Constraint() As Constraint_DateTime
        Get
            Return CType(mConstraint, Constraint_DateTime)
        End Get
    End Property

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        TvDateTime.SelectedNode = FindNode(TvDateTime.Nodes, CStr(Constraint.TypeofDateTimeConstraint), True)
        TvDateTime.SelectedNode.EnsureVisible()
    End Sub

    Private Sub TvDateTime_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvDateTime.AfterSelect
        If Not IsLoading Then
            Constraint.TypeofDateTimeConstraint = CInt(TvDateTime.SelectedNode.Tag)
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
'The Original Code is DateTimeConstraintControl.vb.
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
