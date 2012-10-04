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

Public Class TreeStructure
    Inherits EntryStructure


    Friend MenuItemExpandAll As MenuItem
    Friend MenuItemCollapseAll As MenuItem
    Private TableArchetypeStyle As DataGridTableStyle
    Private mIsCluster As Boolean
    Friend mAddClusterMenuItem As MenuItem
    Private mDragTreeNode As ArchetypeTreeNode

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Debug.Assert(DesignMode)

        'Not able to set inherited imagelists through GUI
        tvTree.ImageList = ilSmall
    End Sub

    Public Sub New(ByVal compound As RmStructureCompound, ByVal fileManager As FileManagerLocal)
        MyBase.New(compound, fileManager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mIsCluster = TypeOf (compound) Is RmCluster

        'Not able to set inherited imagelists through GUI
        tvTree.ImageList = ilSmall
        ProcessCompound(compound, tvTree.Nodes)
    End Sub

    Public Sub New(ByVal fileManager As FileManagerLocal)
        MyBase.New("Tree", fileManager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Not able to set inherited imagelists through GUI
        tvTree.ImageList = ilSmall
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
    Private Shadows components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents tvTree As System.Windows.Forms.TreeView
    Friend WithEvents TreeContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents RemoveMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents NameSlotMenuItem As MenuItem
    Friend WithEvents MenuExpandAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuCollapseAll As System.Windows.Forms.MenuItem
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents AddReferenceMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RemoveItemAndReferencesMenuItem As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tvTree = New System.Windows.Forms.TreeView
        Me.TreeContextMenu = New System.Windows.Forms.ContextMenu
        Me.RemoveMenuItem = New System.Windows.Forms.MenuItem
        Me.NameSlotMenuItem = New System.Windows.Forms.MenuItem
        Me.RemoveItemAndReferencesMenuItem = New System.Windows.Forms.MenuItem
        Me.MenuExpandAll = New System.Windows.Forms.MenuItem
        Me.MenuCollapseAll = New System.Windows.Forms.MenuItem
        Me.SpecialiseMenuItem = New System.Windows.Forms.MenuItem
        Me.AddReferenceMenuItem = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'tvTree
        '
        Me.tvTree.AllowDrop = True
        Me.tvTree.ContextMenu = Me.TreeContextMenu
        Me.tvTree.Cursor = System.Windows.Forms.Cursors.Default
        Me.tvTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvTree.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvTree.HideSelection = False
        Me.tvTree.HotTracking = True
        Me.tvTree.ImageIndex = -1
        Me.tvTree.LabelEdit = True
        Me.tvTree.Location = New System.Drawing.Point(40, 24)
        Me.tvTree.Name = "tvTree"
        Me.tvTree.SelectedImageIndex = -1
        Me.tvTree.Size = New System.Drawing.Size(416, 336)
        Me.tvTree.TabIndex = 38
        '
        'TreeContextMenu
        '
        Me.TreeContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.RemoveMenuItem, Me.MenuExpandAll, Me.MenuCollapseAll, Me.SpecialiseMenuItem, Me.AddReferenceMenuItem, Me.NameSlotMenuItem})
        '
        'MenuRemove
        '
        Me.RemoveMenuItem.Index = 0
        Me.RemoveMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.RemoveItemAndReferencesMenuItem})
        Me.RemoveMenuItem.Text = "Remove"
        '
        'MenuRemoveItemAndReferences
        '
        Me.RemoveItemAndReferencesMenuItem.Index = 0
        Me.RemoveItemAndReferencesMenuItem.Text = "?"
        '
        'MenuExpandAll
        '
        Me.MenuExpandAll.Index = 1
        Me.MenuExpandAll.Text = "Expand All"
        '
        'MenuCollapseAll
        '
        Me.MenuCollapseAll.Index = 2
        Me.MenuCollapseAll.Text = "Collapse All"
        '
        'SpecialiseMenuItem
        '
        Me.SpecialiseMenuItem.Index = 3
        Me.SpecialiseMenuItem.Text = "Specialise"
        '
        'AddReferenceMenuItem
        '
        Me.AddReferenceMenuItem.Index = 4
        Me.AddReferenceMenuItem.Text = "Add Reference"
        '
        'MenuNameSlot
        '
        Me.NameSlotMenuItem.Index = 5
        Me.NameSlotMenuItem.Text = "Name this Slot"
        '
        'TreeStructure
        '
        Me.Controls.Add(Me.tvTree)
        Me.Name = "TreeStructure"
        Me.Size = New System.Drawing.Size(456, 360)
        Me.Controls.SetChildIndex(Me.tvTree, 0)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub AddToElementList(ByVal a_node_collection As TreeNodeCollection, ByVal arch_elements As ArrayList)
        For Each n As ArchetypeTreeNode In a_node_collection
            If n.GetNodeCount(False) > 0 Then
                ' must be a cluster
                AddToElementList(n.Nodes, arch_elements)
            Else
                If n.Item.RM_Class.Type = StructureType.Element Then
                    arch_elements.Add(n.Item)
                End If
            End If
        Next
    End Sub

    Private Sub TreeStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = tvTree

        If Not DesignMode Then
            'set the text for the menus
            RemoveMenuItem.Text = AE_Constants.Instance.Remove
            MenuExpandAll.Text = AE_Constants.Instance.ExpandAll
            MenuCollapseAll.Text = AE_Constants.Instance.CollapseAll
            SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
            AddReferenceMenuItem.Text = AE_Constants.Instance.AddReference
            NameSlotMenuItem.Text = AE_Constants.Instance.NameThisSlot

            If tvTree.GetNodeCount(False) > 0 Then
                tvTree.SelectedNode = tvTree.Nodes(0)
            End If
        End If
    End Sub

    Private Sub ProcessChildrenRM_Structures(ByVal colNodes As TreeNodeCollection, ByVal rm As RmStructureCompound)
        For Each tvNode As ArchetypeTreeNode In colNodes
            Select Case tvNode.Item.RM_Class.Type
                Case StructureType.Cluster
                    Dim cluster As New RmCluster(CType(tvNode.Item, ArchetypeComposite))
                    ProcessChildrenRM_Structures(tvNode.Nodes, cluster)
                    rm.Children.Add(cluster)

                Case StructureType.Element, StructureType.Reference
                    Dim element As RmElement = CType(tvNode.Item.RM_Class, RmElement)
                    rm.Children.Add(element)

                Case StructureType.Slot
                    Dim slot As RmSlot = CType(tvNode.Item.RM_Class, RmSlot)
                    rm.Children.Add(slot)

                Case Else
                    Debug.Assert(False)
            End Select
        Next
    End Sub

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Return tvTree.Nodes
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructure
        Get
            Dim result As RmStructureCompound

            ' sets the cardinality of the children
            If mIsCluster Then
                result = New RmCluster(mNodeId)
            Else
                result = New RmStructureCompound(mNodeId, StructureType.Tree)
            End If

            result.Children.Cardinality = mCardinalityControl.Cardinality

            Dim tvNode As ArchetypeTreeNode

            For Each tvNode In tvTree.Nodes
                Select Case tvNode.Item.RM_Class.Type
                    Case StructureType.Cluster
                        Dim cluster As New RmCluster(CType(tvNode.Item, ArchetypeComposite))
                        ProcessChildrenRM_Structures(tvNode.Nodes, cluster)
                        result.Children.Add(cluster)

                    Case StructureType.Element, StructureType.Slot, StructureType.Reference
                        result.Children.Add(tvNode.Item.RM_Class)

                    Case Else
                        Debug.Assert(False)
                End Select
            Next

            Return result
        End Get
        Set(ByVal value As RmStructure)
            Dim compound As RmStructureCompound = CType(value, RmStructureCompound)

            ' handles conversion from other structures
            tvTree.Nodes.Clear()
            mNodeId = value.NodeId
            MyBase.SetCardinality(compound)

            Select Case value.Type
                Case StructureType.Tree, StructureType.List, StructureType.Single
                    ProcessCompound(compound, tvTree.Nodes)

                Case StructureType.Table
                    If compound.Children.Items(0).Type = StructureType.Cluster Then
                        Dim clust As RmCluster = CType(compound.Children.Items(0), RmCluster)

                        For Each rm As RmStructure In clust.Children
                            AddTreeNode(rm, tvTree.Nodes)
                        Next
                    Else
                        Debug.Assert(False, "Not expected type")
                    End If
            End Select
        End Set
    End Property

    Protected Sub ProcessCompound(ByRef compound As RmStructureCompound, ByRef parentNodes As TreeNodeCollection)
        For Each struct As RmStructure In compound.Children
            AddTreeNode(struct, parentNodes)
        Next
    End Sub

    Protected Sub AddTreeNode(ByVal struct As RmStructure, ByRef parentNodes As TreeNodeCollection)
        Dim tvNode As ArchetypeTreeNode

        Select Case struct.Type
            Case StructureType.Cluster
                tvNode = New ArchetypeTreeNode(CType(struct, RmCluster), mFileManager)
                ProcessCompound(CType(struct, RmCluster), tvNode.Nodes)
            Case StructureType.Element, StructureType.Reference
                tvNode = New ArchetypeTreeNode(CType(struct, RmElement), mFileManager)
            Case StructureType.Slot
                tvNode = New ArchetypeTreeNode(CType(struct, RmSlot), mFileManager)
            Case Else
                tvNode = Nothing
        End Select

        If Not tvNode Is Nothing Then
            tvNode.RefreshIcons()
            parentNodes.Add(tvNode)
        End If
    End Sub

    Public Overrides Sub Reset()
        tvTree.Nodes.Clear()
    End Sub

    Private Sub PopulateTreeNodes(ByRef tvnodes As TreeNodeCollection)
        For Each tn As ArchetypeTreeNode In tvnodes
            CType(tn, ArchetypeTreeNode).Translate()
            PopulateTreeNodes(tn.Nodes)
        Next
    End Sub

    Public Overrides Sub Translate()
        PopulateTreeNodes(tvTree.Nodes)
        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Public Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click
        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            Dim dlg As New SpecialisationQuestionDialog()
            dlg.ShowForArchetypeNode(tvNode.Item.Text, tvNode.RM_Class, SpecialisationDepth)

            If dlg.IsSpecialisationRequested Then
                If dlg.IsCloningRequested Then
                    Dim i As Integer = tvNode.Index
                    tvNode = tvNode.SpecialisedClone(mFileManager)

                    If tvTree.SelectedNode.Parent Is Nothing Then
                        tvTree.Nodes.Insert(i + 1, tvNode)
                    Else
                        tvTree.SelectedNode.Parent.Nodes.Insert(i + 1, tvNode)
                    End If
                Else
                    tvNode.Specialise()
                End If

                tvTree.SelectedNode = tvNode
                SetCurrentItem(tvNode.Item)
                tvNode.BeginEdit()
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs) Handles AddReferenceMenuItem.Click
        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            Dim ref As RmReference = New RmReference(CType(tvNode.Item.RM_Class, RmElement))
            tvNode = New ArchetypeTreeNode(ref, mFileManager)
            tvNode.RefreshIcons()

            If tvTree.SelectedNode.Parent Is Nothing Then
                tvTree.Nodes.Insert(tvTree.SelectedNode.Index + 1, tvNode)
            Else
                tvTree.SelectedNode.Parent.Nodes.Insert(tvTree.SelectedNode.Index + 1, tvNode)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub NameSlot(ByVal sender As Object, ByVal e As System.EventArgs) Handles NameSlotMenuItem.Click
        If Not tvTree.SelectedNode Is Nothing Then
            ReplaceAnonymousSlot()
            tvTree.SelectedNode.BeginEdit()
        End If
    End Sub

    Protected Sub ReplaceAnonymousSlot()
        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

            If tvNode.Item.IsAnonymous Then
                Dim newSlot As New ArchetypeSlot(CType(tvNode.Item, ArchetypeNodeAnonymous), mFileManager)
                Dim i As Integer = tvNode.Index
                Dim nc As TreeNodeCollection

                If tvNode.Parent Is Nothing Then
                    nc = tvTree.Nodes
                Else
                    nc = tvNode.Parent.Nodes
                End If

                tvNode.Remove()
                tvNode = New ArchetypeTreeNode(newSlot)
                tvNode.RefreshIcons()
                nc.Insert(i, tvNode)

                mFileManager.FileEdited = True
                tvNode.EnsureVisible()
                tvTree.SelectedNode = tvNode
            End If
        End If
    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        Dim cm As New ContextMenu
        Dim a_mi As MenuItem
        a_mi = New MenuItem(Filemanager.GetOpenEhrTerm(109, "New element"))
        cm.MenuItems.Add(a_mi)
        a_mi.MergeMenu(mConstraintMenu)

        mAddClusterMenuItem = New MenuItem(Filemanager.GetOpenEhrTerm(322, "New cluster"))
        AddHandler mAddClusterMenuItem.Click, AddressOf AddNewCluster
        cm.MenuItems.Add(mAddClusterMenuItem)

        Dim addSlotMenuItem As New MenuItem(Filemanager.GetOpenEhrTerm(312, "New slot"))
        AddHandler addSlotMenuItem.Click, AddressOf AddNewSlot
        cm.MenuItems.Add(addSlotMenuItem)

        cm.Show(Me.ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Public Overrides Sub SetInitial()
        If tvTree.Nodes.Count > 0 Then
            tvTree.SelectedNode = tvTree.Nodes(0)
        End If
    End Sub

    Protected Overrides Sub AddNewElement(ByVal aConstraint As Constraint)
        Dim tvNode As ArchetypeTreeNode
        Dim editLabel As Boolean = True

        If aConstraint.Kind <> ConstraintKind.Slot Then
            tvNode = New ArchetypeTreeNode(Filemanager.GetOpenEhrTerm(109, "New Element"), StructureType.Element, mFileManager)
            CType(tvNode.Item, ArchetypeElement).Constraint = aConstraint
        ElseIf MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            tvNode = New ArchetypeTreeNode(New ArchetypeSlot(mFileManager.OntologyManager.GetOpenEHRTerm(CInt(StructureType.Element), StructureType.Element.ToString), StructureType.Element, mFileManager))
        Else
            tvNode = New ArchetypeTreeNode(New RmSlot(StructureType.Element), mFileManager)
            editLabel = False
        End If

        tvNode.RefreshIcons()
        Dim selectedNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

        If selectedNode Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        ElseIf selectedNode.RM_Class.Type = StructureType.Cluster Then
            selectedNode.Nodes.Add(tvNode)
        ElseIf selectedNode.Parent Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            selectedNode.Parent.Nodes.Add(tvNode)
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvTree.SelectedNode = tvNode

        If editLabel Then
            tvNode.BeginEdit()
        End If
    End Sub

    Sub AddNewCluster(ByVal sender As Object, ByVal e As EventArgs)
        Dim tvNode As New ArchetypeTreeNode(Filemanager.GetOpenEhrTerm(322, "New cluster"), StructureType.Cluster, mFileManager)
        tvNode.RefreshIcons()
        Dim selectedNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

        If selectedNode Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        ElseIf selectedNode.RM_Class.Type = StructureType.Cluster Then
            selectedNode.Nodes.Add(tvNode)
        ElseIf selectedNode.Parent Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            selectedNode.Parent.Nodes.Add(tvNode)
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvTree.SelectedNode = tvNode
        SetCurrentItem(tvNode.Item)
        tvNode.BeginEdit()
    End Sub

    Sub AddNewSlot(ByVal sender As Object, ByVal e As EventArgs)
        Dim editLabel As Boolean = True
        Dim tvNode As ArchetypeTreeNode = GetSlotNode(PointToScreen(CType(sender, MenuItem).Parent.GetContextMenu.SourceControl.Location), editLabel)

        If Not tvNode Is Nothing Then
            tvNode.RefreshIcons()
            Dim selectedNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

            If selectedNode Is Nothing Then
                tvTree.Nodes.Add(tvNode)
            ElseIf selectedNode.RM_Class.Type = StructureType.Cluster Then
                selectedNode.Nodes.Add(tvNode)
            ElseIf selectedNode.Parent Is Nothing Then
                tvTree.Nodes.Add(tvNode)
            Else
                selectedNode.Parent.Nodes.Add(tvNode)
            End If

            mFileManager.FileEdited = True
            tvNode.EnsureVisible()
            tvTree.SelectedNode = tvNode

            If editLabel Then
                tvNode.BeginEdit()
            End If
        End If
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles RemoveItemAndReferencesMenuItem.Click
        Dim tvNode As ArchetypeTreeNode = TryCast(tvTree.SelectedNode, ArchetypeTreeNode)

        If Not tvNode Is Nothing AndAlso tvNode.Item.CanRemove Then
            Dim hasReferences As Boolean = False
            Dim message As String = AE_Constants.Instance.Remove & tvTree.SelectedNode.Text

            If tvNode.Item.RM_Class.Type = StructureType.Element Then
                If CType(tvNode.Item.RM_Class, RmElement).HasReferences Then
                    hasReferences = True
                    message = AE_Constants.Instance.Remove & tvTree.SelectedNode.Text & " " & AE_Constants.Instance.AllReferences
                End If
            End If

            If MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                ' leave an item selected if there is one
                If Not tvNode.PrevNode Is Nothing Then
                    tvTree.SelectedNode = tvNode.PrevNode
                ElseIf Not tvNode.NextNode Is Nothing Then
                    tvTree.SelectedNode = tvNode.NextNode
                Else
                    If tvNode.Parent Is Nothing Then
                        tvTree.SelectedNode = Nothing
                        SetCurrentItem(Nothing)
                    Else
                        tvTree.SelectedNode = tvNode.Parent
                    End If
                End If

                'if the current node has references then remove all nodes with the same id
                If hasReferences Then
                    RemoveTreeNodeAndReferences(tvTree.Nodes, CType(tvNode.Item, ArchetypeElement).NodeId)
                Else
                    tvNode.Remove()
                End If

                mFileManager.FileEdited = True
            End If
        Else
            Beep()
        End If
    End Sub

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        Dim tvNode As TreeNode = tvTree.SelectedNode

        If Not tvNode Is Nothing Then
            Dim nodes As TreeNodeCollection
            Dim parent As TreeNode = tvNode.Parent
            Dim i As Integer = tvNode.Index

            If i > 0 Then
                If parent Is Nothing Then
                    nodes = tvTree.Nodes
                Else
                    nodes = parent.Nodes
                End If

                tvNode.Remove()
                nodes.Insert(i - 1, tvNode)
                mFileManager.FileEdited = True
            ElseIf Not parent Is Nothing Then
                If parent.Parent Is Nothing Then
                    nodes = tvTree.Nodes
                Else
                    nodes = parent.Parent.Nodes
                End If

                i = parent.Index
                tvNode.Remove()
                nodes.Insert(i, tvNode)
                mFileManager.FileEdited = True
            End If

            tvTree.SelectedNode = tvNode
        End If
    End Sub

    Protected Overrides Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        Dim tvNode As TreeNode = tvTree.SelectedNode

        If Not tvNode Is Nothing Then
            Dim parent As TreeNode = tvNode.Parent

            If Not tvNode.NextNode Is Nothing Then
                Dim nodes As TreeNodeCollection

                If parent Is Nothing Then
                    nodes = tvTree.Nodes
                Else
                    nodes = parent.Nodes
                End If

                Dim i As Integer = tvNode.Index
                tvNode.Remove()
                nodes.Insert(i + 1, tvNode)
                mFileManager.FileEdited = True
            End If

            tvTree.SelectedNode = tvNode
        End If
    End Sub

    Protected Overrides Sub RefreshIcons()
        RefreshIconsRecursively(tvTree.Nodes)
    End Sub

    Private Sub RefreshIconsRecursively(ByVal nodes As TreeNodeCollection)
        For Each tvNode As ArchetypeTreeNode In nodes
            RefreshIconsRecursively(tvNode.Nodes)
            tvNode.RefreshIcons()
        Next
    End Sub

    Private Sub RemoveTreeNodeAndReferences(ByVal NodeCollection As TreeNodeCollection, ByVal id As String)
        Dim tvNode As ArchetypeTreeNode

        For Each tvNode In NodeCollection
            If Not tvNode.Item.IsAnonymous Then
                If CType(tvNode.Item, ArchetypeNodeAbstract).NodeId = id Then
                    tvNode.Remove()
                Else
                    If tvNode.GetNodeCount(False) > 0 Then
                        RemoveTreeNodeAndReferences(tvNode.Nodes, id)
                    End If
                End If
            End If
        Next
    End Sub

    Public Overrides Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        Dim text, s As String

        text = new_line & (Space(3 * indentlevel) & "\cf1 Structure\cf0  = \cf2 TREE\cf0\par")
        s = ""

        If mCardinalityControl.Cardinality.Ordered Then
            s = "ordered"
        End If

        s = s.Trim
        text = text & new_line & (Space(3 * indentlevel) & "\cf2 Items\cf0  " & s & "\par")
        text = text & new_line & TreeToRichText(tvTree.Nodes, indentlevel + 3, new_line)
        Return text
    End Function

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim showComments As Boolean = Main.Instance.Options.ShowCommentsInHtml

        If mIsCluster Then
            result.Append("<p>")
            result.Append(CStr(IIf(mCardinalityControl.Cardinality.Ordered, mFileManager.OntologyManager.GetOpenEHRTerm(162, "Ordered"), "")))
            result.Append("</p>")
        Else
            result.AppendFormat("<p><i>Structure</i>: {0}", Filemanager.GetOpenEhrTerm(107, "TREE"))
            result.Append(CStr(IIf(mCardinalityControl.Cardinality.Ordered, ", " & mFileManager.OntologyManager.GetOpenEHRTerm(162, "Ordered"), "")))
            result.Append("</p>")
        End If

        result.AppendFormat("{0}<table border=""1"" cellpadding=""2"" width=""100%"">", Environment.NewLine)

        result.Append(Environment.NewLine)

        result.AppendFormat(Me.HtmlHeader(BackGroundColour, showComments))

        result.AppendFormat("{0}{1}", Environment.NewLine, TreeToHTML(tvTree.Nodes, 0, showComments))
        result.AppendFormat("{0}</tr>", Environment.NewLine)
        result.AppendFormat("{0}</table>", Environment.NewLine)

        Return result.ToString
    End Function

    Public Overrides Function ItemCount() As Integer
        Return tvTree.GetNodeCount(True)
    End Function

    Private Function TreeToRichText(ByVal TreeNodes As TreeNodeCollection, ByVal level As Integer, ByVal new_line As String) As String
        Dim an As ArchetypeTreeNode
        Dim text As String = ""

        For Each an In TreeNodes
            text = text & new_line & an.Item.ToRichText(level)
            If an.GetNodeCount(False) > 0 Then
                text = text & new_line & TreeToRichText(an.Nodes, level + 3, new_line)
            End If
        Next

        Return text
    End Function

    Private Function TreeToHTML(ByVal TreeNodes As TreeNodeCollection, ByVal level As Integer, ByVal showComments As Boolean) As String
        Dim text As String = ""

        For Each an As ArchetypeTreeNode In TreeNodes
            text &= Environment.NewLine & an.Item.ToHTML(level, showComments)
            text &= Environment.NewLine & "</tr>"

            If an.GetNodeCount(False) > 0 Then
                text &= Environment.NewLine & TreeToHTML(an.Nodes, level + 1, showComments)
            End If
        Next

        Return text
    End Function

    Private Sub TreeExpandAll(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuExpandAll.Click
        tvTree.ExpandAll()
    End Sub

    Private Sub TreeCollapseAll(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuCollapseAll.Click
        tvTree.CollapseAll()
    End Sub

    Private Sub ContextMenuTree_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TreeContextMenu.Popup
        RemoveMenuItem.Visible = False
        SpecialiseMenuItem.Visible = False
        AddReferenceMenuItem.Visible = False
        NameSlotMenuItem.Visible = False

        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            RemoveItemAndReferencesMenuItem.Text = tvNode.Text

            Dim item As ArchetypeNode = tvNode.Item
            Dim element As ArchetypeElement = TryCast(item, ArchetypeElement)
            AddReferenceMenuItem.Visible = Not (element Is Nothing OrElse element.IsReference)
            NameSlotMenuItem.Visible = item.IsAnonymous
            RemoveMenuItem.Visible = item.CanRemove
            SpecialiseMenuItem.Visible = item.CanSpecialise
        End If
    End Sub

    Private Sub tvTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvTree.AfterSelect
        Dim aArcheTypeNode As ArchetypeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode).Item
        SetCurrentItem(aArcheTypeNode)

        If aArcheTypeNode.RM_Class.Type = StructureType.Element Then
            Dim element As ArchetypeElement = CType(aArcheTypeNode, ArchetypeElement)

            If element.HasReferences Then
                RemoveItemAndReferencesMenuItem.Text = String.Format("{0} [+]", RemoveItemAndReferencesMenuItem.Text)
            End If

            tvTree.LabelEdit = Not element.IsReference
        Else
            tvTree.LabelEdit = True
        End If
    End Sub

    Dim mHoverNode As ArchetypeTreeNode

    Private Sub tvTree_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseMove
        Dim tvNode As ArchetypeTreeNode = CType(tvTree.GetNodeAt(e.X, e.Y), ArchetypeTreeNode)

        If Not tvNode Is Nothing Then
            If Not mHoverNode Is tvNode Then
                mHoverNode = tvNode
                SetToolTipSpecialisation(tvTree, tvNode.Item)
            End If
        End If
    End Sub

    Private Sub tvTree_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvTree.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            If e.Label = "" Then
                e.CancelEdit = True
            Else
                Dim tvNode As ArchetypeTreeNode = CType(e.Node, ArchetypeTreeNode)
                tvNode.Text = e.Label
                tvNode.Item.Text = e.Label
                RemoveItemAndReferencesMenuItem.Text = e.Label

                If tvNode.Item.RM_Class.Type = StructureType.Element Then
                    If CType(tvNode.Item, ArchetypeElement).HasReferences Then
                        RemoveItemAndReferencesMenuItem.Text = String.Format("{0} [+]", RemoveItemAndReferencesMenuItem.Text)
                        Translate()
                    End If
                End If

                If tvNode.Text <> e.Label Then
                    e.CancelEdit = True ' need to show slots text if it is different
                End If
            End If
        End If
    End Sub

    Private Sub tvTree_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvTree.BeforeLabelEdit
        Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

        If tvNode.Item.IsAnonymous Then
            e.CancelEdit = True
        ElseIf tvNode.Item.RM_Class.SpecialisationDepth < SpecialisationDepth Then
            e.CancelEdit = True
            SpecialiseCurrentItem(sender, e)
        End If
    End Sub

    Private Sub tvTree_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvTree.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If Not tvTree.SelectedNode Is Nothing Then
                    ReplaceAnonymousSlot()
                    tvTree.SelectedNode.BeginEdit()
                End If
            Case Keys.Delete
                RemoveItemAndReferences(sender, e)
        End Select
    End Sub

#Region "Drag and drop"

    Private Sub tvTree_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseDown
        Dim tn As TreeNode = tvTree.GetNodeAt(e.X, e.Y)

        If Not tn Is Nothing Then
            If e.Button = MouseButtons.Left Then
                tvTree.Cursor = Cursors.Hand
            ElseIf e.Button = MouseButtons.Right Then
                tvTree.SelectedNode = tn
            End If
        End If
    End Sub

    Private Sub tvTree_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseUp
        tvTree.Cursor = System.Windows.Forms.Cursors.Default

        If e.Button = MouseButtons.Left Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.GetNodeAt(e.X, e.Y), ArchetypeTreeNode)

            If Not tvNode Is Nothing Then
                If tvNode.IsSelected And tvNode.Item.IsAnonymous Then
                    If MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ReplaceAnonymousSlot()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub tvTree_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvTree.ItemDrag
        mDragTreeNode = CType(e.Item, ArchetypeTreeNode)
        mDragTreeNode.Collapse()
        tvTree.AllowDrop = True
        tvTree.DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    'Not needed as collapse containers before dragging which prevents dragging them onto their children

    'Private Sub tvTree_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragOver
    '    Dim dropNode As TreeNode
    '    Dim position As Drawing.Point
    '    Dim parentNode As TreeNode

    '    'Prevent dropping a parent on a child
    '    If Not mDragTreeNode Is Nothing Then
    '        position.X = e.X
    '        position.Y = e.Y
    '        position = Me.tvTree.PointToClient(position)
    '        DropNode = CType(Me.tvTree.GetNodeAt(position), ArchetypeTreeNode)
    '        If Not dropNode Is Nothing Then
    '            parentNode = dropNode.Parent
    '            While Not parentNode Is Nothing
    '                If parentNode Is mDragTreeNode Then
    '                    e.Effect = DragDropEffects.None
    '                    Return
    '                End If
    '                parentNode = parentNode.Parent
    '            End While
    '        End If
    '    End If
    '    'Allow the allowed effect
    '    e.Effect = e.AllowedEffect

    'End Sub

    Private Function GetSlotNode(ByVal pt As Point, ByRef allowEdit As Boolean) As ArchetypeTreeNode
        Dim frmChooseType As New ChooseType
        Dim result As ArchetypeTreeNode = Nothing

        For Each t As Integer In ReferenceModel.validArchetypeSlots(Global.ArchetypeEditor.StructureType.Cluster)
            frmChooseType.listType.Items.Add(Filemanager.GetOpenEhrTerm(t, "Slot"))
        Next

        'frmChooseType.listType.Items.Add(Filemanager.GetOpenEhrTerm(567, "Element"))
        'frmChooseType.listType.Items.Add(Filemanager.GetOpenEhrTerm(313, "Cluster"))
        frmChooseType.Text = Filemanager.GetOpenEhrTerm(104, "Choose..")
        frmChooseType.StartPosition = FormStartPosition.Manual
        frmChooseType.Location = New Point(pt.X, pt.Y)
        frmChooseType.ShowDialog(ParentForm)

        If frmChooseType.DialogResult = DialogResult.OK Then
            Dim slotClass As StructureType

            Select Case frmChooseType.listType.SelectedIndex
                Case 0
                    slotClass = StructureType.Element
                Case 1
                    slotClass = Global.ArchetypeEditor.StructureType.Cluster
                Case 2
                    slotClass = Global.ArchetypeEditor.StructureType.Item
            End Select

            If MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim archetype_slot As New ArchetypeSlot(slotClass.ToString, slotClass, mFileManager)
                result = New ArchetypeTreeNode(archetype_slot)
            Else
                Dim newSlot As New RmSlot(slotClass)
                result = New ArchetypeTreeNode(newSlot, mFileManager)
                allowEdit = False
            End If
        End If

        Return result
    End Function

    Private Sub tvTree_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragDrop
        Dim allowEdit As Boolean = True
        Dim nodeDragged As ArchetypeTreeNode = mDragTreeNode

        If nodeDragged Is Nothing Then
            If Not mNewConstraint Is Nothing Then
                If TypeOf mNewConstraint Is Constraint_Slot Then
                    nodeDragged = GetSlotNode(New Point(e.X, e.Y), allowEdit)
                Else
                    Dim element As New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New element"), mFileManager)
                    element.Constraint = mNewConstraint
                    nodeDragged = New ArchetypeTreeNode(element)
                End If
            ElseIf mNewCluster Then
                Dim cluster As New ArchetypeComposite(Filemanager.GetOpenEhrTerm(322, "New cluster"), StructureType.Cluster, mFileManager)
                nodeDragged = New ArchetypeTreeNode(cluster)
            End If
        End If

        If Not nodeDragged Is Nothing Then
            nodeDragged.RefreshIcons()

            Dim position As Point
            position.X = e.X
            position.Y = e.Y
            position = tvTree.PointToClient(position)
            Dim dropNode As ArchetypeTreeNode = CType(tvTree.GetNodeAt(position), ArchetypeTreeNode)

            If nodeDragged Is dropNode Then
                Beep()
            Else
                Dim dropParent, dragParent As TreeNodeCollection
                Dim dropIndex As Integer

                If dropNode Is Nothing Then
                    dropParent = tvTree.Nodes
                    dropIndex = tvTree.GetNodeCount(False)
                ElseIf dropNode.Item.RM_Class.Type = StructureType.Cluster Then
                    dropParent = dropNode.Nodes

                    If dropNode.IsExpanded Then
                        dropIndex = 0
                    Else
                        dropIndex = dropNode.GetNodeCount(False)
                    End If
                Else
                    If dropNode.Parent Is Nothing Then
                        dropParent = tvTree.Nodes
                    Else
                        dropParent = dropNode.Parent.Nodes
                    End If

                    dropIndex = dropNode.Index + 1
                End If

                If e.Effect = DragDropEffects.Move Then
                    If nodeDragged.Parent Is Nothing Then
                        dragParent = tvTree.Nodes
                    Else
                        dragParent = nodeDragged.Parent.Nodes
                    End If

                    dragParent.Remove(nodeDragged)
                End If

                dropParent.Insert(dropIndex, nodeDragged)
                nodeDragged.EnsureVisible()
                tvTree.SelectedNode = nodeDragged

                If e.Effect = DragDropEffects.Copy And allowEdit Then
                    ' have to do this last or not visible
                    nodeDragged.BeginEdit()
                End If

                mFileManager.FileEdited = True
            End If
        End If

        mNewConstraint = Nothing
        mNewCluster = False
        mDragTreeNode = Nothing
        tvTree.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub tvTree_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

#End Region

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
'The Original Code is TreeStructure.vb.
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

