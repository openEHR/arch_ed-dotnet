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

Public Class TreeStructure
    Inherits EntryStructure


    Friend MenuItemExpandAll As MenuItem
    Friend MenuItemCollapseAll As MenuItem
    Private TableArchetypeStyle As DataGridTableStyle
    Private mIsCluster As Boolean = False
    Friend mAddClusterMenuItem As MenuItem
    Private mDragTreeNode As ArchetypeTreeNode

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal rm As RmStructureCompound, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(rm, a_file_manager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If TypeOf (rm) Is RmCluster Then
            mIsCluster = True
        End If

        'Not able to set inherited imagelists through GUI
        Me.tvTree.ImageList = Me.ilSmall

        Dim a_rm_structure As RmStructure

        For Each a_rm_structure In rm.Children
            ' have to create links to the new archetype here to maintain updates

            Select Case a_rm_structure.Type '.TypeName
                Case StructureType.Cluster ' "Cluster"
                    Dim tvNode As ArchetypeTreeNode = New ArchetypeTreeNode(CType(a_rm_structure, RmCluster), mFileManager)
                    ProcessCluster(CType(a_rm_structure, RmCluster), tvNode)
                    tvTree.Nodes.Add(tvNode)

                Case StructureType.Element, StructureType.Reference
                    Dim element As RmElement = CType(a_rm_structure, RmElement)
                    Dim tvNode As ArchetypeTreeNode = New ArchetypeTreeNode(element, mFileManager)
                    tvNode.ImageIndex = ImageIndexForConstraintType(element.Constraint.Type, element.isReference, False)
                    tvNode.SelectedImageIndex = ImageIndexForConstraintType(element.Constraint.Type, element.isReference, True)
                    tvTree.Nodes.Add(tvNode)

                Case StructureType.Slot
                    Dim tvNode As ArchetypeTreeNode = New ArchetypeTreeNode(CType(a_rm_structure, RmSlot), mFileManager)
                    tvNode.ImageIndex = ImageIndexForItem(tvNode.Item, False)
                    tvNode.SelectedImageIndex = ImageIndexForItem(tvNode.Item, True)
                    tvTree.Nodes.Add(tvNode)

                Case Else
                    Debug.Assert(False)
            End Select
        Next
    End Sub

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If Not Me.DesignMode Then
            Debug.Assert(False)
        End If

        'Not able to set inherited imagelists through GUI
        Me.tvTree.ImageList = Me.ilSmall

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New("Tree", a_file_manager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'Not able to set inherited imagelists through GUI
        Me.tvTree.ImageList = Me.ilSmall
        mFileManager.FileEdited = True

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
    'Friend WithEvents ContextMenuTree As System.Windows.Forms.ContextMenu
    Friend WithEvents TreeContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuNameSlot As MenuItem
    Friend WithEvents MenuExpandAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuCollapseAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuSpecialise As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddReference As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveItemAndReferences As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tvTree = New System.Windows.Forms.TreeView
        Me.TreeContextMenu = New System.Windows.Forms.ContextMenu
        Me.MenuRemove = New System.Windows.Forms.MenuItem
        Me.MenuNameSlot = New System.Windows.Forms.MenuItem
        Me.MenuRemoveItemAndReferences = New System.Windows.Forms.MenuItem
        Me.MenuExpandAll = New System.Windows.Forms.MenuItem
        Me.MenuCollapseAll = New System.Windows.Forms.MenuItem
        Me.MenuSpecialise = New System.Windows.Forms.MenuItem
        Me.MenuAddReference = New System.Windows.Forms.MenuItem
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
        Me.TreeContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemove, Me.MenuExpandAll, Me.MenuCollapseAll, Me.MenuSpecialise, Me.MenuAddReference, Me.MenuNameSlot})
        '
        'MenuRemove
        '
        Me.MenuRemove.Index = 0
        Me.MenuRemove.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemoveItemAndReferences})
        Me.MenuRemove.Text = "Remove"
        '
        'MenuRemoveItemAndReferences
        '
        Me.MenuRemoveItemAndReferences.Index = 0
        Me.MenuRemoveItemAndReferences.Text = "?"
        '
        'MenuExpandAll
        '
        Me.MenuExpandAll.Index = 1
        Me.MenuExpandAll.Text = "Expand all"
        '
        'MenuCollapseAll
        '
        Me.MenuCollapseAll.Index = 2
        Me.MenuCollapseAll.Text = "Collapse all"
        '
        'MenuSpecialise
        '
        Me.MenuSpecialise.Index = 3
        Me.MenuSpecialise.Text = "Specialise"
        '
        'MenuAddReference
        '
        Me.MenuAddReference.Index = 4
        Me.MenuAddReference.Text = "Add reference"
        '
        'MenuNameSlot
        '
        Me.MenuNameSlot.Index = 5
        Me.MenuNameSlot.Text = "Name this slot"
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

    Public Overrides ReadOnly Property Elements() As ArchetypeElement()
        Get
            Dim i As Integer
            i = tvTree.GetNodeCount(False)
            If i > 0 Then
                Dim an_arraylist As New ArrayList

                AddToElementList(tvTree.Nodes, an_arraylist)

                Dim a_e(an_arraylist.Count - 1) As ArchetypeElement

                For i = 0 To an_arraylist.Count - 1
                    a_e(i) = CType(an_arraylist.Item(i), ArchetypeElement)
                Next
                Return a_e
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Property IsCluster() As Boolean
        Get
            Return mIsCluster
        End Get
        Set(ByVal value As Boolean)
            mIsCluster = value
        End Set
    End Property

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
            MenuRemove.Text = AE_Constants.Instance.Remove
            MenuExpandAll.Text = AE_Constants.Instance.Expand_All
            MenuCollapseAll.Text = AE_Constants.Instance.Collapse_All
            MenuSpecialise.Text = AE_Constants.Instance.Specialise
            MenuAddReference.Text = AE_Constants.Instance.Add_Reference
            MenuNameSlot.Text = AE_Constants.Instance.NameThisSlot

            If tvTree.GetNodeCount(False) > 0 Then
                tvTree.SelectedNode = tvTree.Nodes(0)
            End If
            ' add the change structure menu from EntryStructure
            If Not IsCluster AndAlso Not TreeContextMenu.MenuItems.Contains(menuChangeStructure) Then
                TreeContextMenu.MenuItems.Add(menuChangeStructure)
            End If
        End If
    End Sub

    Private Sub ProcessChildrenRM_Structures(ByVal colNodes As TreeNodeCollection, _
        ByVal rm As RmStructureCompound)

        For Each tvNode As ArchetypeTreeNode In colNodes
            Select Case tvNode.Item.RM_Class.Type
                Case StructureType.Cluster
                    Dim clusterNode As New RmCluster(CType(tvNode.Item, ArchetypeComposite))
                    'HERE IT IS
                    ProcessChildrenRM_Structures(tvNode.Nodes, clusterNode)
                    rm.Children.Add(clusterNode)

                Case StructureType.Element, StructureType.Reference
                    Dim ElementNode As RmElement = CType(tvNode.Item.RM_Class, RmElement)
                    rm.Children.Add(ElementNode)

                Case StructureType.Slot
                    Dim slotNode As RmSlot = CType(tvNode.Item.RM_Class, RmSlot)
                    rm.Children.Add(slotNode)

                Case Else
                    Debug.Assert(False)
            End Select
        Next
    End Sub

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Return Me.tvTree.Nodes
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructureCompound
        Get
            Dim tvNode As ArchetypeTreeNode
            Dim RM_S As RmStructureCompound

            ' sets the cardinality of the children
            If mIsCluster Then
                RM_S = New RmCluster(mNodeId)
            Else
                RM_S = New RmStructureCompound(mNodeId, StructureType.Tree)
            End If

            RM_S.Children.Cardinality = Me.mCardinalityControl.Cardinality

            For Each tvNode In Me.tvTree.Nodes

                Select Case tvNode.Item.RM_Class.Type
                    Case StructureType.Cluster
                        Dim a_cluster As New RmCluster(CType(tvNode.Item, ArchetypeComposite))

                        ProcessChildrenRM_Structures(tvNode.Nodes, a_cluster)
                        RM_S.Children.Add(a_cluster)

                    Case StructureType.Element, StructureType.Slot, StructureType.Reference
                        RM_S.Children.Add(tvNode.Item.RM_Class)

                    Case Else
                        Debug.Assert(False)
                End Select
            Next
            Return RM_S
        End Get
        Set(ByVal Value As RmStructureCompound)
            ' handles conversion from other structures
            tvTree.Nodes.Clear()
            mNodeId = Value.NodeId
            MyBase.SetCardinality(Value)

            Select Case Value.Type
                Case StructureType.List
                    For Each rm As RmStructure In Value.Children
                        AddTreeNode(rm)
                    Next

                Case StructureType.Single
                    Dim rm As RmStructure = Value.Children.FirstElementOrElementSlot

                    If Not rm Is Nothing Then
                        AddTreeNode(rm)
                    End If

                Case StructureType.Table
                    If Value.Children.items(0).Type = StructureType.Cluster Then
                        Dim clust As RmCluster = CType(Value.Children.items(0), RmCluster)

                        For Each rm As RmStructure In clust.Children
                            AddTreeNode(rm)
                        Next
                    Else
                        Debug.Assert(False, "Not expected type")
                    End If
            End Select
        End Set
    End Property

    Protected Sub AddTreeNode(ByVal rm As RmStructure)
        Dim node As ArchetypeTreeNode

        Select Case rm.Type
            Case StructureType.Element, StructureType.Reference
                node = New ArchetypeTreeNode(CType(rm, RmElement), mFileManager)
            Case StructureType.Slot
                node = New ArchetypeTreeNode(CType(rm, RmSlot), mFileManager)
            Case Else
                node = Nothing
                Debug.Assert(False, "Type not handled")
        End Select

        If Not node Is Nothing Then
            node.ImageIndex = ImageIndexForItem(node.Item, False)
            node.SelectedImageIndex = ImageIndexForItem(node.Item, True)
            tvTree.Nodes.Add(node)
        End If
    End Sub

    Public Overrides Sub reset()
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

    Friend Sub ProcessCluster(ByRef a_Cluster As RmCluster, ByRef ParentTreeNode As ArchetypeTreeNode)
        For Each rm As RmStructure In a_Cluster.Children
            ' have to create links to the new archetype here to maintain updates
            Select Case rm.Type '.TypeName
                Case StructureType.Cluster ' "Cluster"
                    Dim tvNode As New ArchetypeTreeNode(CType(rm, RmCluster), mFileManager)
                    ProcessCluster(CType(rm, RmCluster), CType(tvNode, ArchetypeTreeNode))
                    ParentTreeNode.Nodes.Add(tvNode)

                Case StructureType.Element, StructureType.Reference ' "Element", "Reference"
                    Dim tvNode As New ArchetypeTreeNode(CType(rm, RmElement), mFileManager)
                    Dim archetype_element As ArchetypeElement = CType(tvNode.Item, ArchetypeElement)
                    tvNode.ImageIndex = ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference, False)
                    tvNode.SelectedImageIndex = ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference, True)
                    ParentTreeNode.Nodes.Add(tvNode)

                Case StructureType.Slot
                    Dim tvNode As New ArchetypeTreeNode(CType(rm, RmSlot), mFileManager)
                    tvNode.ImageIndex = ImageIndexForItem(tvNode.Item, False)
                    tvNode.SelectedImageIndex = ImageIndexForItem(tvNode.Item, True)
                    ParentTreeNode.Nodes.Add(tvNode)

                Case Else
                    Debug.Assert(False, "Type not handled")
            End Select
        Next
    End Sub

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSpecialise.Click

        If Not tvTree.SelectedNode Is Nothing Then
            If MessageBox.Show(AE_Constants.Instance.Specialise & " '" & Me.tvTree.SelectedNode.Text & "'", _
                AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

                Dim tvNode As ArchetypeTreeNode = CType(Me.tvTree.SelectedNode, ArchetypeTreeNode)

                If TypeOf tvNode.Item Is ArchetypeSlot Then
                    tvNode.Specialise()
                ElseIf tvNode.Item.Occurrences.IsUnbounded Or tvNode.Item.Occurrences.MaxCount > 1 Then
                    Dim i As Integer

                    i = CType(tvNode, ArchetypeTreeNode).Index
                    tvNode = tvNode.Copy(mFileManager)
                    tvNode.Specialise()
                    If tvNode.Item.RM_Class.Type = StructureType.Element Then
                        Dim archetype_element As ArchetypeElement = CType(tvNode.Item, ArchetypeElement)
                        'Cannot specialise a reference
                        tvNode.ImageIndex = ImageIndexForConstraintType(archetype_element.Constraint.Type, False, False)
                        tvNode.SelectedImageIndex = ImageIndexForConstraintType(CType(tvNode.Item, ArchetypeElement).Constraint.Type, False, True)
                    End If

                    If tvTree.SelectedNode.Parent Is Nothing Then
                        Me.tvTree.Nodes.Insert(i + 1, tvNode)
                    Else
                        Me.tvTree.SelectedNode.Parent.Nodes.Insert(i + 1, tvNode)
                    End If
                Else
                    tvNode.Specialise()
                End If
                'SetCurrentItem(tvNode.Item)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs) Handles MenuAddReference.Click
        Dim tvNode As ArchetypeTreeNode
        Dim ref As RmReference

        If Not tvTree.SelectedNode Is Nothing Then
            tvNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            ' create a new reference element pointing to this element
            ref = New RmReference(CType(tvNode.Item.RM_Class, RmElement))
            ' record the presence of the reference so a delete can be safe
            CType(tvNode.Item.RM_Class, RmElement).hasReferences = True
            tvNode = New ArchetypeTreeNode(ref, mFileManager)
            tvNode.ImageIndex = ImageIndexForConstraintType(CType(tvNode.Item, ArchetypeElement).Constraint.Type, True, False)
            tvNode.SelectedImageIndex = ImageIndexForConstraintType(CType(tvNode.Item, ArchetypeElement).Constraint.Type, True, True)

            If Me.tvTree.SelectedNode.Parent Is Nothing Then
                Me.tvTree.Nodes.Insert(Me.tvTree.SelectedNode.Index + 1, tvNode)
            Else
                Me.tvTree.SelectedNode.Parent.Nodes.Insert(Me.tvTree.SelectedNode.Index + 1, tvNode)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub NameSlot(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuNameSlot.Click
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

                tvNode.ImageIndex = ImageIndexForConstraintType(ConstraintType.Slot, False, False)
                tvNode.SelectedImageIndex = ImageIndexForConstraintType(ConstraintType.Slot, False, True)

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

    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        Dim tvNode As ArchetypeTreeNode
        Dim editLabel As Boolean = True

        If a_constraint.Type <> ConstraintType.Slot Then
            tvNode = New ArchetypeTreeNode(Filemanager.GetOpenEhrTerm(109, "New Element"), StructureType.Element, mFileManager)
            ' set the image indexes
            CType(tvNode.Item, ArchetypeElement).Constraint = a_constraint
        ElseIf MessageBox.Show(AE_Constants.Instance.NameThisSlot, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim archetype_slot As New ArchetypeSlot(mFileManager.OntologyManager.GetOpenEHRTerm(CInt(StructureType.Element), StructureType.Element.ToString), StructureType.Element, mFileManager)
            tvNode = New ArchetypeTreeNode(archetype_slot)
        Else
            Dim newSlot As New RmSlot(StructureType.Element)
            tvNode = New ArchetypeTreeNode(newSlot, mFileManager)
            editLabel = False
        End If

        tvNode.ImageIndex = ImageIndexForConstraintType(a_constraint.Type, False, False)
        tvNode.SelectedImageIndex = ImageIndexForConstraintType(a_constraint.Type, False, True)

        If tvTree.SelectedNode Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            Dim a_node As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

            If a_node.Item.RM_Class.Type = StructureType.Cluster Then
                ' cluster selected so add at deeper level
                tvTree.SelectedNode.Nodes.Add(tvNode)
            Else
                ' element selected so add at the same level
                If tvTree.SelectedNode.Parent Is Nothing Then
                    tvTree.Nodes.Add(tvNode)
                Else
                    tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
                End If
            End If
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvTree.SelectedNode = tvNode

        If editLabel Then
            tvNode.BeginEdit()
        End If
    End Sub

    Sub AddNewCluster(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String = Filemanager.GetOpenEhrTerm(322, "New cluster")
        Dim tvNode As New ArchetypeTreeNode(s, StructureType.Cluster, mFileManager)
        Dim selNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

        If selNode Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        ElseIf selNode.RM_Class.Type = StructureType.Cluster Then
            selNode.Nodes.Add(tvNode)
        ElseIf selNode.Parent Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
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

        If tvTree.SelectedNode Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            Dim a_node As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

            If a_node.Item.RM_Class.Type = StructureType.Cluster Then
                ' cluster selected so add at deeper level
                tvTree.SelectedNode.Nodes.Add(tvNode)
            Else
                ' element selected so add at the same level
                If tvTree.SelectedNode.Parent Is Nothing Then
                    tvTree.Nodes.Add(tvNode)
                Else
                    tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
                End If
            End If
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvTree.SelectedNode = tvNode

        If editLabel Then
            tvNode.BeginEdit()
        End If
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles MenuRemoveItemAndReferences.Click
        Dim tvNode As ArchetypeTreeNode
        Dim has_references As Boolean
        Dim message As String

        If Not tvTree.SelectedNode Is Nothing Then
            tvNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            message = AE_Constants.Instance.Remove & Me.tvTree.SelectedNode.Text

            If tvNode.Item.RM_Class.Type = StructureType.Element Then
                If CType(tvNode.Item.RM_Class, RmElement).hasReferences Then
                    has_references = True
                    message = AE_Constants.Instance.Remove & Me.tvTree.SelectedNode.Text & " " & AE_Constants.Instance.All_References
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
                If has_references Then
                    RemoveTreeNodeAndReferences(Me.tvTree.Nodes, CType(tvNode.Item, ArchetypeElement).NodeId)
                Else
                    tvNode.Remove()
                End If

                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not Me.tvTree.SelectedNode Is Nothing Then
            Dim tvN As TreeNode
            Dim tvN_C As TreeNodeCollection
            Dim i As Integer
            Dim has_parent As Boolean

            tvN = Me.tvTree.SelectedNode
            If tvN.Parent Is Nothing Then
                tvN_C = Me.tvTree.Nodes
            Else
                tvN_C = tvN.Parent.Nodes
                has_parent = True
            End If

            i = tvN.Index

            If i > 0 Then
                tvN.Remove()
                tvN_C.Insert((i - 1), tvN)
                mFileManager.FileEdited = True
                Me.tvTree.SelectedNode = tvN
            ElseIf i = 0 AndAlso has_parent Then
                If tvN.Parent.Parent Is Nothing Then
                    tvN_C = Me.tvTree.Nodes
                Else
                    tvN_C = tvN.Parent.Parent.Nodes
                End If
                i = tvN.Parent.Index
                tvN.Remove()
                tvN_C.Insert((i), tvN)
                mFileManager.FileEdited = True
                Me.tvTree.SelectedNode = tvN
            End If
        End If
    End Sub

    Protected Overrides Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not Me.tvTree.SelectedNode Is Nothing Then
            Dim tvN As TreeNode
            Dim tvN_C As TreeNodeCollection
            Dim i As Integer

            tvN = Me.tvTree.SelectedNode
            If tvN.Parent Is Nothing Then
                tvN_C = Me.tvTree.Nodes
            Else
                tvN_C = tvN.Parent.Nodes
            End If

            i = tvN.Index

            If Not tvN.NextNode Is Nothing Then
                tvN.Remove()
                tvN_C.Insert((i + 1), tvN)
                mFileManager.FileEdited = True
                Me.tvTree.SelectedNode = tvN
            End If
        End If
    End Sub

    Protected Overrides Sub RefreshIcons()
        Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)

        If element.HasReferences Then
            RefreshReferenceNodeIcons(tvTree.Nodes, element)
        End If

        tvTree.SelectedNode.ImageIndex = ImageIndexForConstraintType(element.Constraint.Type, element.IsReference, False)
        tvTree.SelectedNode.SelectedImageIndex = ImageIndexForConstraintType(element.Constraint.Type, element.IsReference, True)
    End Sub

    Private Sub RefreshReferenceNodeIcons(ByVal a_node_collection As TreeNodeCollection, ByVal reference As ArchetypeElement)
        For Each tvNode As ArchetypeTreeNode In a_node_collection
            If tvNode.Item.RM_Class.Type = StructureType.Cluster Then
                RefreshReferenceNodeIcons(tvNode.Nodes, reference)
            ElseIf Not tvNode.Item.IsAnonymous AndAlso CType(tvNode.Item, ArchetypeNodeAbstract).NodeId = reference.NodeId Then
                Dim element As ArchetypeElement = CType(tvNode.Item, ArchetypeElement)

                If element.IsReference Then
                    tvNode.ImageIndex = ImageIndexForConstraintType(element.Constraint.Type, True, False)
                    tvNode.SelectedImageIndex = ImageIndexForConstraintType(element.Constraint.Type, True, True)
                End If
            End If
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
        Dim showComments As Boolean = OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml

        If IsCluster Then
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

    Public Overrides Function HasData() As Boolean
        Return Me.tvTree.Nodes.Count > 0
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
        Me.tvTree.ExpandAll()
    End Sub

    Private Sub TreeCollapseAll(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuCollapseAll.Click
        Me.tvTree.CollapseAll()
    End Sub

    Private Sub ContextMenuTree_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TreeContextMenu.Popup
        MenuRemove.Visible = False
        MenuSpecialise.Visible = False
        MenuAddReference.Visible = False
        MenuNameSlot.Visible = False

        If Not tvTree.SelectedNode Is Nothing Then
            Dim i As Integer
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            MenuRemoveItemAndReferences.Text = tvNode.Text

            If tvNode.Item.RM_Class.Type = StructureType.Element Then
                If Not CType(tvNode.Item.RM_Class, RmElement).isReference Then
                    MenuAddReference.Visible = True
                End If
            End If

            ' show specialisation if appropriate
            If Not tvNode.Item.IsAnonymous Then
                i = OceanArchetypeEditor.Instance.CountInString(CType(tvNode.Item, ArchetypeNodeAbstract).NodeId, ".")
                Dim numberSpecialisations As Integer = mFileManager.OntologyManager.NumberOfSpecialisations

                If i < numberSpecialisations Then
                    MenuSpecialise.Visible = True
                Else
                    If numberSpecialisations = 0 Or ((CType(tvNode.Item, ArchetypeNodeAbstract).NodeId.StartsWith("at0.") Or (CType(tvNode.Item, ArchetypeNodeAbstract).NodeId.IndexOf(".0.") > -1))) Then
                        MenuRemove.Visible = True
                    End If
                End If
            Else
                MenuNameSlot.Visible = True
                MenuRemove.Visible = True
            End If
        End If
    End Sub

    Private Sub tvTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvTree.AfterSelect
        Dim aArcheTypeNode As ArchetypeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode).Item
        SetCurrentItem(aArcheTypeNode)

        If aArcheTypeNode.RM_Class.Type = StructureType.Element Then
            Dim element As ArchetypeElement = CType(aArcheTypeNode, ArchetypeElement)

            If element.HasReferences Then
                MenuRemoveItemAndReferences.Text = String.Format("{0} [+]", MenuRemoveItemAndReferences.Text)
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
                SetToolTipSpecialisation(Me.tvTree, tvNode.Item)
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
                MenuRemoveItemAndReferences.Text = e.Label

                If tvNode.Item.RM_Class.Type = StructureType.Element Then
                    If CType(tvNode.Item, ArchetypeElement).HasReferences Then
                        MenuRemoveItemAndReferences.Text = String.Format("{0} [+]", MenuRemoveItemAndReferences.Text)
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
        Else
            Dim i As Integer = OceanArchetypeEditor.Instance.CountInString(CType(tvNode.Item, ArchetypeNodeAbstract).NodeId, ".")

            If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                If MessageBox.Show(AE_Constants.Instance.RequiresSpecialisationToEdit, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    e.CancelEdit = True
                End If
            End If
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
                Dim i As Integer
                Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

                If Not tvNode.Item.IsAnonymous Then
                    Dim numberSpecialisations As Integer = mFileManager.OntologyManager.NumberOfSpecialisations

                    i = OceanArchetypeEditor.Instance.CountInString(CType(tvNode.Item, ArchetypeNodeAbstract).NodeId, ".")

                    If (numberSpecialisations = 0) Or (i = numberSpecialisations And _
                        (((CType(tvNode.Item, ArchetypeNodeAbstract).NodeId.StartsWith("at0.") Or (CType(tvNode.Item, ArchetypeNodeAbstract).NodeId.IndexOf(".0.") > -1))))) Then
                        RemoveItemAndReferences(sender, e)
                    End If
                Else
                    RemoveItemAndReferences(sender, e)
                End If
        End Select
    End Sub

#Region "Drag and drop"

    Private Sub tvTree_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseDown
        Dim tn As TreeNode = tvTree.GetNodeAt(e.X, e.Y)

        If Not tn Is Nothing Then
            If e.Button = MouseButtons.Left Then
                tvTree.Cursor = Cursors.Hand
            End If
        ElseIf e.Button = MouseButtons.Right Then
            tvTree.SelectedNode = tn
        End If
    End Sub

    Private Sub tvTree_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseUp
        tvTree.Cursor = System.Windows.Forms.Cursors.Default

        If e.Button = MouseButtons.Left Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.GetNodeAt(e.X, e.Y), ArchetypeTreeNode)

            If Not tvNode Is Nothing Then
                If tvNode.IsSelected And tvNode.Item.IsAnonymous Then
                    If MessageBox.Show(AE_Constants.Instance.NameThisSlot, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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

            If MessageBox.Show(AE_Constants.Instance.NameThisSlot, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim archetype_slot As New ArchetypeSlot(slotClass.ToString, slotClass, mFileManager)
                result = New ArchetypeTreeNode(archetype_slot)
            Else
                Dim newSlot As New RmSlot(slotClass)
                result = New ArchetypeTreeNode(newSlot, mFileManager)
                allowEdit = False
            End If

            result.ImageIndex = ImageIndexForConstraintType(ConstraintType.Slot, False, False)
            result.SelectedImageIndex = ImageIndexForConstraintType(ConstraintType.Slot, False, True)
        End If

        Return result
    End Function

    Private Sub tvTree_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragDrop
        Dim position As Point
        Dim dropNode As ArchetypeTreeNode
        Dim dropParent As TreeNodeCollection
        Dim dropIndex As Integer
        Dim dragParent As TreeNodeCollection
        Dim allowEdit As Boolean = True
        Dim nodeDragged As ArchetypeTreeNode = mDragTreeNode

        If nodeDragged Is Nothing Then
            If Not mNewConstraint Is Nothing Then
                If TypeOf mNewConstraint Is Constraint_Slot Then
                    nodeDragged = GetSlotNode(New Point(e.X, e.Y), allowEdit)
                Else
                    Dim archetype_element As New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New element"), mFileManager)
                    archetype_element.Constraint = mNewConstraint
                    nodeDragged = New ArchetypeTreeNode(archetype_element)
                    nodeDragged.ImageIndex = ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference, False)
                    nodeDragged.SelectedImageIndex = ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference, True)
                End If
            ElseIf mNewCluster Then
                Dim new_cluster As New ArchetypeComposite(Filemanager.GetOpenEhrTerm(322, "New cluster"), StructureType.Cluster, mFileManager)
                nodeDragged = New ArchetypeTreeNode(new_cluster)
            End If
        End If

        If Not nodeDragged Is Nothing Then
            position.X = e.X
            position.Y = e.Y
            position = tvTree.PointToClient(position)
            dropNode = CType(tvTree.GetNodeAt(position), ArchetypeTreeNode)

            If Not dropNode Is Nothing Then
                ' if the drop node exists then set the parent
                If dropNode.Item.RM_Class.Type = StructureType.Cluster Then
                    ' drop as child of cluster
                    dropParent = dropNode.Nodes

                    If dropNode.IsExpanded Then
                        ' insert at the beginning
                        dropIndex = 0
                    Else
                        'insert at end
                        dropIndex = dropNode.GetNodeCount(False)
                    End If
                Else
                    dropIndex = dropNode.Index + 1

                    If dropNode.Parent Is Nothing Then
                        dropParent = tvTree.Nodes
                    Else
                        dropParent = dropNode.Parent.Nodes
                    End If
                End If
            Else
                'otherwise add it to the tvTree
                dropParent = Me.tvTree.Nodes
                dropIndex = Me.tvTree.GetNodeCount(False) ' add at the end
            End If

            If e.Effect = DragDropEffects.Move Then
                If nodeDragged.Parent Is Nothing Then
                    dragParent = tvTree.Nodes
                Else
                    dragParent = nodeDragged.Parent.Nodes
                End If

                If Not nodeDragged Is dropNode Then
                    dragParent.Remove(nodeDragged)
                Else
                    nodeDragged = Nothing
                    Beep()
                End If
            End If

            'Add the node
            If Not nodeDragged Is Nothing Then
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

