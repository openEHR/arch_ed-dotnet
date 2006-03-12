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

Option Strict On

Public Class TreeStructure
    Inherits EntryStructure


    Friend MenuItemExpandAll As MenuItem
    Friend MenuItemCollapseAll As MenuItem
    Private TableArchetypeStyle As DataGridTableStyle
    Friend mAddClusterMenuItem As MenuItem
    Private mDragTreeNode As ArchetypeTreeNode

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal rm As RmStructureCompound, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(rm, a_file_manager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'Not able to set inherited imagelists through GUI
        Me.tvTree.ImageList = Me.ilSmall

        Dim a_rm_structure As RmStructure

        For Each a_rm_structure In rm.Children
            ' have to create links to the new archetype here to maintain updates

            Select Case a_rm_structure.Type '.TypeName
                Case StructureType.Cluster ' "Cluster"
                    Dim tvNode As ArchetypeTreeNode
                    tvNode = New ArchetypeTreeNode(CType(a_rm_structure, RmCluster), mFileManager)
                    ProcessCluster(CType(a_rm_structure, RmCluster), tvNode)
                    Me.tvTree.Nodes.Add(tvNode)

                Case StructureType.Element ' "Element"
                    Dim tvNode As ArchetypeTreeNode
                    Dim element As RmElement

                    element = CType(a_rm_structure, RmElement)
                    tvNode = New ArchetypeTreeNode(element, mFileManager)
                    tvNode.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference)
                    tvNode.SelectedImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference, True)

                    Me.tvTree.Nodes.Add(tvNode)

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
    Friend WithEvents MenuExpandAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuCollapseAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuSpecialise As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddReference As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveItemAndReferences As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tvTree = New System.Windows.Forms.TreeView
        Me.TreeContextMenu = New System.Windows.Forms.ContextMenu
        Me.MenuRemove = New System.Windows.Forms.MenuItem
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
        Me.TreeContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemove, Me.MenuExpandAll, Me.MenuCollapseAll, Me.MenuSpecialise, Me.MenuAddReference})
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
            Dim i, j As Integer
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

    Private Sub AddToElementList(ByVal a_node_collection As TreeNodeCollection, ByVal arch_elements As ArrayList)
        For Each n As ArchetypeTreeNode In a_node_collection
            If n.GetNodeCount(False) > 0 Then
                ' must be a cluster
                AddToElementList(n.Nodes, arch_elements)
            Else
                If n.Item.RM_Class.Type = StructureType.Element Then
                    arch_elements.Add(n.Item)
                Else
                    Debug.Assert(False)
                End If
            End If
        Next
    End Sub


    Private Sub TreeStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = tvTree

        If Not Me.DesignMode Then
            'set the text for the menus
            Me.MenuRemove.Text = AE_Constants.Instance.Remove
            Me.MenuExpandAll.Text = AE_Constants.Instance.Expand_All
            Me.MenuCollapseAll.Text = AE_Constants.Instance.Collapse_All
            Me.MenuSpecialise.Text = AE_Constants.Instance.Specialise
            Me.MenuAddReference.Text = AE_Constants.Instance.Add_Reference
            If Me.tvTree.GetNodeCount(False) > 0 Then
                Me.tvTree.SelectedNode = Me.tvTree.Nodes(0)
            End If
        End If

    End Sub

    Private Sub ProcessChildrenRM_Structures(ByVal colNodes As TreeNodeCollection, _
        ByVal rm As RmStructureCompound)

        For Each tvNode As ArchetypeTreeNode In colNodes

            Select Case tvNode.Item.RM_Class.Type
                Case StructureType.Cluster
                    Dim clusterNode As New RmCluster(CType(tvNode.Item.RM_Class, RmStructure))
                    ProcessChildrenRM_Structures(tvNode.Nodes, clusterNode)
                    rm.Children.Add(clusterNode)

                Case StructureType.Element, StructureType.Reference
                    Dim ElementNode As RmElement = CType(tvNode.Item.RM_Class, RmElement)
                    'If ElementNode.DataType = "Ordinal" Then
                    '    Debug.Assert(False, "TODO")
                    '    'GetOrdinals(tvNode)
                    'End If
                    rm.Children.Add(ElementNode)

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

            ' sets the cardinality of the children
            Dim RM_S As New RmStructureCompound(mNodeId, StructureType.Tree)

            RM_S.Children.Cardinality = Me.mCardinalityControl.Cardinality

            For Each tvNode In Me.tvTree.Nodes

                Select Case tvNode.Item.RM_Class.Type
                    Case StructureType.Cluster
                        'Dim a_cluster As New RmCluster(CType(tvNode.Item.RM_Class, RmStructure))
                        Dim a_cluster As New RmCluster(CType(tvNode.Item, ArchetypeComposite))

                        ProcessChildrenRM_Structures(tvNode.Nodes, a_cluster)
                        RM_S.Children.Add(a_cluster)

                    Case StructureType.Element
                        RM_S.Children.Add(tvNode.Item.RM_Class)

                    Case Else
                        Debug.Assert(False)
                End Select
            Next
            Return RM_S
        End Get
        Set(ByVal Value As RmStructureCompound)
            ' handles conversion from other structures
            Debug.Assert(False, "ToDo")
            Me.tvTree.Nodes.Clear()
            mNodeId = Value.NodeId
            MyBase.SetCardinality(Value)

            Select Case Value.Type '.TypeName
                Case StructureType.List ' "List"
                    Dim element As RmElement
                    For Each element In Value.Children
                        Dim node As New ArchetypeTreeNode(element, mFileManager)
                        node.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference)
                        node.SelectedImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference, True)
                        tvTree.Nodes.Add(node)
                    Next
                Case StructureType.Single ' "SINGLE"
                    Dim element As RmElement
                    element = Value.Children.FirstElementNode
                    Dim node As New ArchetypeTreeNode(element, mFileManager)
                    node.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference)
                    node.SelectedImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference, True)

                    tvTree.Nodes.Add(node)
                Case StructureType.Table ' "TABLE"
                    Dim element As RmElement
                    For Each element In Value.Children
                        Dim node As New ArchetypeTreeNode(element, mFileManager)
                        node.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference)
                        node.SelectedImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference, True)
                        tvTree.Nodes.Add(node)
                    Next
            End Select
        End Set
    End Property

    Public Overrides Sub reset()
        Me.tvTree.Nodes.Clear()
    End Sub

    Private Sub PopulateTreeNodes(ByRef tvnodes As TreeNodeCollection)
        Dim tn As ArchetypeTreeNode
        For Each tn In tvnodes
            CType(tn, ArchetypeTreeNode).Translate()
            PopulateTreeNodes(tn.Nodes)
        Next
    End Sub

    Public Overrides Sub Translate()
        PopulateTreeNodes(tvTree.Nodes)
        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Private Sub ProcessCluster(ByRef a_Cluster As RmCluster, ByRef ParentTreeNode As ArchetypeTreeNode)
        Dim rm As RmStructure

        For Each rm In a_Cluster.Children
            ' have to create links to the new archetype here to maintain updates
            Select Case rm.Type '.TypeName
                Case StructureType.Cluster ' "Cluster"
                    Dim tvNode As New ArchetypeTreeNode(CType(rm, RmCluster), mFileManager)
                    ProcessCluster(CType(rm, RmCluster), CType(tvNode, ArchetypeTreeNode))
                    ParentTreeNode.Nodes.Add(tvNode)

                Case StructureType.Element, StructureType.Reference ' "Element", "Reference"
                    Dim tvNode As New ArchetypeTreeNode(CType(rm, RmElement), mFileManager)
                    Dim archetype_element As ArchetypeElement = CType(tvNode.item, ArchetypeElement)
                    tvNode.ImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference)
                    tvNode.SelectedImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference, True)
                    ParentTreeNode.Nodes.Add(tvNode)

                Case Else
                    Debug.Assert(False)
            End Select

        Next
    End Sub

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSpecialise.Click

        If Not tvTree.SelectedNode Is Nothing Then
            If MessageBox.Show(AE_Constants.Instance.Specialise & " '" & Me.tvTree.SelectedNode.Text & "'", _
                AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Question) = DialogResult.OK Then

                Dim tvNode As ArchetypeTreeNode = CType(Me.tvTree.SelectedNode, ArchetypeTreeNode)

                If tvNode.Item.Occurrences.IsUnbounded Or tvNode.Item.Occurrences.MaxCount > 1 Then
                    Dim i As Integer

                    i = CType(tvNode, ArchetypeTreeNode).Index
                    tvNode = tvNode.Copy(mFileManager)
                    tvNode.Specialise()
                    If tvNode.Item.RM_Class.Type = StructureType.Element Then
                        Dim archetype_element As ArchetypeElement = CType(tvNode.Item, ArchetypeElement)
                        'Cannot specialise a reference
                        tvNode.ImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type)
                        tvNode.SelectedImageIndex = Me.ImageIndexForConstraintType(CType(tvNode.Item, ArchetypeElement).Constraint.Type, False, True)
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
            tvNode.ImageIndex = Me.ImageIndexForConstraintType(CType(tvNode.Item, ArchetypeElement).Constraint.Type, True)
            tvNode.SelectedImageIndex = Me.ImageIndexForConstraintType(CType(tvNode.Item, ArchetypeElement).Constraint.Type, True, True)
            If Me.tvTree.SelectedNode.Parent Is Nothing Then
                Me.tvTree.Nodes.Insert(Me.tvTree.SelectedNode.Index + 1, tvNode)
            Else
                Me.tvTree.SelectedNode.Parent.Nodes.Insert(Me.tvTree.SelectedNode.Index + 1, tvNode)
            End If
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        Dim cm As New ContextMenu
        Dim mi, a_mi As MenuItem
        a_mi = New MenuItem(mFileManager.OntologyManager.GetOpenEHRTerm(109, "New element"))
        cm.MenuItems.Add(a_mi)
        a_mi.MergeMenu(mConstraintMenu)

        mAddClusterMenuItem = New MenuItem(mFileManager.OntologyManager.GetOpenEHRTerm(322, "New cluster"))
        AddHandler mAddClusterMenuItem.Click, AddressOf AddNewCluster
        cm.MenuItems.Add(mAddClusterMenuItem)

        cm.Show(Me.ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Protected Overrides Sub addNewElement(ByVal a_constraint As Constraint)
        Dim tvNode As ArchetypeTreeNode
        Dim rw, rw1 As DataRow
        Dim a_node As ArchetypeTreeNode

        tvNode = New ArchetypeTreeNode(mFileManager.OntologyManager.GetOpenEHRTerm(109, "New Element"), StructureType.Element, mFileManager)
        ' set the image indexes
        tvNode.ImageIndex = Me.ImageIndexForConstraintType(a_constraint.Type)
        tvNode.SelectedImageIndex = Me.ImageIndexForConstraintType(a_constraint.Type, False, True)
        CType(tvNode.Item, ArchetypeElement).Constraint = a_constraint

        If Me.tvTree.SelectedNode Is Nothing Then
            Me.tvTree.Nodes.Add(tvNode)
        Else
            a_node = CType(Me.tvTree.SelectedNode, ArchetypeTreeNode)

            If a_node.Item.RM_Class.Type = StructureType.Cluster Then
                ' cluster selected so add at deeper level
                Me.tvTree.SelectedNode.Nodes.Add(tvNode)
            Else
                ' element selected so add at the same level
                If Me.tvTree.SelectedNode.Parent Is Nothing Then
                    Me.tvTree.Nodes.Add(tvNode)
                Else
                    Me.tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
                End If

            End If

        End If
        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        Me.tvTree.SelectedNode = tvNode
        tvNode.BeginEdit()

    End Sub

    Sub AddNewCluster(ByVal sender As Object, ByVal e As EventArgs)
        ' adds a new cluster
        Dim tvNode, selNode As ArchetypeTreeNode
        '        Dim a_node As ArchetypeTreeNode
        Dim s As String

        s = mFileManager.OntologyManager.GetOpenEHRTerm(322, "New cluster")
        tvNode = New ArchetypeTreeNode(s, StructureType.Cluster, mFileManager)
        selNode = CType(Me.tvTree.SelectedNode, ArchetypeTreeNode)

        If selNode Is Nothing Then
            Me.tvTree.Nodes.Add(tvNode)
        Else
            If selNode.RM_Class.Type = StructureType.Cluster Then
                selNode.Nodes.Add(tvNode)
            Else
                If selNode.Parent Is Nothing Then
                    Me.tvTree.Nodes.Add(tvNode)
                Else
                    Me.tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
                End If
            End If
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        Me.tvTree.SelectedNode = tvNode
        SetCurrentItem(tvNode.Item)
        tvNode.BeginEdit()
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles MenuRemoveItemAndReferences.Click
        Dim tvNode As ArchetypeTreeNode
        Dim has_references As Boolean
        Dim message As String

        If Not Me.tvTree.SelectedNode Is Nothing Then

            tvNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            message = AE_Constants.Instance.Remove & Me.tvTree.SelectedNode.Text
            If tvNode.Item.RM_Class.Type = StructureType.Element Then
                If CType(tvNode.Item.RM_Class, RmElement).hasReferences Then
                    has_references = True
                    message = AE_Constants.Instance.Remove & Me.tvTree.SelectedNode.Text & " " & AE_Constants.Instance.All_References
                End If
            End If

            If MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                ' leave an item selected if there is one
                If Not tvNode.PrevNode Is Nothing Then
                    Me.tvTree.SelectedNode = tvNode.PrevNode
                ElseIf Not tvNode.NextNode Is Nothing Then
                    Me.tvTree.SelectedNode = tvNode.NextNode
                Else
                    If tvNode.Parent Is Nothing Then
                        Me.tvTree.SelectedNode = Nothing
                        SetCurrentItem(Nothing)

                    Else
                        Me.tvTree.SelectedNode = tvNode.Parent
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
            RefreshTreeNodeIcons(tvTree.Nodes, element)
        Else
            tvTree.SelectedNode.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.IsReference)
            tvTree.SelectedNode.SelectedImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.IsReference, True)
        End If

    End Sub

    Private Sub RefreshTreeNodeIcons(ByVal a_node_collection As TreeNodeCollection, ByVal reference As ArchetypeElement)

        For Each tvNode As ArchetypeTreeNode In a_node_collection
            If tvNode.Item.RM_Class.Type = StructureType.Cluster Then
                RefreshTreeNodeIcons(tvNode.Nodes, reference)
            Else
                If (Not tvNode.Item.IsAnonymous) AndAlso _
                CType(tvNode.Item, ArchetypeNodeAbstract).NodeId = reference.NodeId Then
                    Dim archetype_element As ArchetypeElement = CType(tvNode.Item, ArchetypeElement)
                    Debug.Assert(archetype_element.IsReference, "Must be a reference here")
                    tvNode.ImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type, True, False)
                    tvNode.SelectedImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type, True, True)
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
        Dim an As ArchetypeTreeNode

        text = text & new_line & (Space(3 * indentlevel) & "\cf1 Structure\cf0  = \cf2 TREE\cf0\par")
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
        Dim lvItem As ArchetypeListViewItem
        Dim text, s As String

        s = ""
        If mCardinalityControl.Cardinality.Ordered Then
            s &= ", ordered"
        End If

        s = s.Trim

        text = "<p>Structure = TREE" & s & "</p>"

        text &= Environment.NewLine & "<table border=""1"" cellpadding=""2"" width=""100%"">"

        If BackGroundColour = "" Then
            text &= Environment.NewLine & "<tr>"
        Else
            text &= Environment.NewLine & "<tr  bgcolor=""" & BackGroundColour & """>"
        End If
        text &= Environment.NewLine & "<td width=""20%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(54, "Concept") & "</h4></td>"
        text &= Environment.NewLine & "<td width = ""40%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(113, "Description") & "</h4></td>"
        text &= Environment.NewLine & "<td width = ""20%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(87, "Constraints") & "</h4></td>"
        text &= Environment.NewLine & "<td width=""20%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(438, "Values") & "</h4></td>"
        text &= Environment.NewLine & "</tr>"

        text &= Environment.NewLine & TreeToHTML(tvTree.Nodes, 0)
        text &= Environment.NewLine & "</tr>"
        text &= Environment.NewLine & "</table>"


        Return text
    End Function

    Private Function TreeToRichText(ByVal TreeNodes As TreeNodeCollection, ByVal level As Integer, ByVal new_line As String) As String
        Dim an As ArchetypeTreeNode
        Dim text As String

        For Each an In TreeNodes
            text = text & new_line & an.Item.ToRichText(level)
            If an.GetNodeCount(False) > 0 Then
                text = text & new_line & TreeToRichText(an.Nodes, level + 3, new_line)
            End If
        Next
        Return text
    End Function

    Private Function TreeToHTML(ByVal TreeNodes As TreeNodeCollection, ByVal level As Integer) As String
        Dim text As String = ""

        For Each an As ArchetypeTreeNode In TreeNodes
            text &= Environment.NewLine & an.Item.ToHTML(level)
            text &= Environment.NewLine & "</tr>"
            If an.GetNodeCount(False) > 0 Then
                text &= Environment.NewLine & TreeToHTML(an.Nodes, level + 1)
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

        Me.MenuRemove.Visible = False
        Me.MenuSpecialise.Visible = False
        Me.MenuAddReference.Visible = False

        If Not tvTree.SelectedNode Is Nothing Then
            Dim i As Integer
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            Me.MenuRemove.Visible = True
            Me.MenuRemoveItemAndReferences.Text = tvNode.Text

            If tvNode.Item.RM_Class.Type = StructureType.Element Then
                'If Filemanager.Instance.OntologyManager.NumberOfSpecialisations = 0 Then
                If Not CType(tvNode.Item.RM_Class, RmElement).isReference Then
                    Me.MenuAddReference.Visible = True
                End If
                'End If
        End If

        ' show specialisation if appropriate
        If Not tvNode.Item.IsAnonymous Then
            i = OceanArchetypeEditor.Instance.CountInString(CType(tvNode.Item, ArchetypeNodeAbstract).NodeId, ".")
            If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                Me.MenuSpecialise.Visible = True
            End If
        End If
        End If

    End Sub

    Private Sub tvTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvTree.AfterSelect
        Dim aArcheTypeNode As ArchetypeNode

        aArcheTypeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode).Item
        SetCurrentItem(aArcheTypeNode)
        If aArcheTypeNode.RM_Class.Type = StructureType.Element Then
            If CType(aArcheTypeNode, ArchetypeElement).HasReferences Then
                Me.MenuRemoveItemAndReferences.Text = Me.MenuRemoveItemAndReferences.Text & " [+]"
            End If
        End If
    End Sub

    Private Sub tvTree_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseMove
        Dim a_node As ArchetypeTreeNode
        a_node = CType(Me.tvTree.GetNodeAt(e.X, e.Y), ArchetypeTreeNode)
        If Not a_node Is Nothing Then
            SetToolTipSpecialisation(Me.tvTree, a_node.Item)
        End If
    End Sub

    Private Sub tvTree_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvTree.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            Dim tvNode As ArchetypeTreeNode

            tvNode = CType(e.Node, ArchetypeTreeNode)
            If tvNode.Text = "" Then
                e.CancelEdit = True
                Return
            End If

            tvNode.Text = e.Label
            Me.MenuRemoveItemAndReferences.Text = e.Label
            If tvNode.Item.RM_Class.Type = StructureType.Element Then
                If CType(tvNode.Item, ArchetypeElement).HasReferences Then
                    MenuRemoveItemAndReferences.Text = MenuRemoveItemAndReferences.Text & " [+]"
                End If
            End If
        End If
    End Sub

    Private Sub tvTree_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvTree.BeforeLabelEdit
        Dim i As Integer
        Dim tvNode As ArchetypeTreeNode

        tvNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)

        If tvNode.Item.IsAnonymous Then
            'cannot edit
            MessageBox.Show(AE_Constants.Instance.Cannot_rename, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.CancelEdit = True
            Return
        End If

        If Not tvNode Is Nothing Then
            i = OceanArchetypeEditor.Instance.CountInString(CType(tvNode.Item, ArchetypeNodeAbstract).NodeId, ".")
            If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                If MessageBox.Show(AE_Constants.Instance.RequiresSpecialisationToEdit, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    e.CancelEdit = True
                End If
            End If
        End If

    End Sub

    Private Sub tvTree_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvTree.KeyDown
        If e.KeyCode = Keys.Delete Then
            Me.RemoveItemAndReferences(sender, e)
        End If
    End Sub

#Region "Drag and drop"

    Private Sub tvTree_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseDown
        Dim tn As TreeNode
        tn = Me.tvTree.GetNodeAt(e.X, e.Y)
        If Not tn Is Nothing Then
            If e.Button = MouseButtons.Left Then
                Me.tvTree.Cursor = System.Windows.Forms.Cursors.Hand
            ElseIf e.Button = MouseButtons.Right Then
                Me.tvTree.SelectedNode = tn
            End If
        End If
    End Sub


    Private Sub tvTree_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseUp
        Me.tvTree.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub tvTree_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvTree.ItemDrag
        mDragTreeNode = CType(e.Item, ArchetypeTreeNode)
        mDragTreeNode.Collapse()
        Me.tvTree.AllowDrop = True
        Me.tvTree.DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub tvTree_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragDrop
        Dim position As Point
        Dim DropNode As ArchetypeTreeNode
        Dim DropParent As TreeNodeCollection
        Dim DropIndex As Integer
        Dim DragParent As TreeNodeCollection
        Dim mNodeDragged As ArchetypeTreeNode

        If Not mDragTreeNode Is Nothing Then
            mNodeDragged = mDragTreeNode
        ElseIf Not mDragArchetypeNode Is Nothing Then
            If mDragArchetypeNode.RM_Class.Type = StructureType.Cluster Then
                mNodeDragged = New ArchetypeTreeNode(mDragArchetypeNode)
            ElseIf mDragArchetypeNode.RM_Class.Type = StructureType.Element Then
                mNodeDragged = New ArchetypeTreeNode(mDragArchetypeNode)
                Dim archetype_element As ArchetypeElement = CType(mNodeDragged.Item, ArchetypeElement)
                mNodeDragged.ImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference)
                mNodeDragged.SelectedImageIndex = Me.ImageIndexForConstraintType(archetype_element.Constraint.Type, archetype_element.IsReference, True)
            Else
                Debug.Assert(False, "No node type")
            End If
        Else
            Debug.Assert(False, "No drag item set")
            mDragArchetypeNode = Nothing
            mDragTreeNode = Nothing
            Me.tvTree.Cursor = System.Windows.Forms.Cursors.Default
            Return
        End If

        position.X = e.X
        position.Y = e.Y
        position = Me.tvTree.PointToClient(position)
        DropNode = CType(Me.tvTree.GetNodeAt(position), ArchetypeTreeNode)

        If Not DropNode Is Nothing Then
            ' if the drop node exists then set the parent

            If DropNode.Item.RM_Class.Type = StructureType.Cluster Then
                ' drop as child of cluster
                DropParent = DropNode.Nodes
                If DropNode.IsExpanded Then
                    ' insert at the beginning
                    DropIndex = 0
                Else
                    'insert at end
                    DropIndex = DropNode.GetNodeCount(False)
                End If
            Else
                DropIndex = DropNode.Index + 1
                If DropNode.Parent Is Nothing Then
                    DropParent = Me.tvTree.Nodes
                Else
                    DropParent = DropNode.Parent.Nodes
                End If
            End If
        Else
            'otherwise add it to the tvTree
            DropParent = Me.tvTree.Nodes
            DropIndex = Me.tvTree.GetNodeCount(False) ' add at the end
        End If

        If e.Effect = DragDropEffects.Move Then
            If mNodeDragged.Parent Is Nothing Then
                DragParent = Me.tvTree.Nodes
            Else
                DragParent = mNodeDragged.Parent.Nodes
            End If
            If Not mNodeDragged Is DropNode Then
                DragParent.Remove(mNodeDragged)
            Else
                Beep()
                mDragArchetypeNode = Nothing
                mDragTreeNode = Nothing
                Me.tvTree.Cursor = System.Windows.Forms.Cursors.Default
                Return
            End If
        End If

        'Add the node
        DropParent.Insert(DropIndex, mNodeDragged)

        mNodeDragged.EnsureVisible()
        Me.tvTree.SelectedNode = mNodeDragged
        If e.Effect = DragDropEffects.Copy Then
            ' have to do this last or not visible
            mNodeDragged.BeginEdit()
        End If
        mFileManager.FileEdited = True

        ' set the drag items to nothing
        mDragArchetypeNode = Nothing
        mDragTreeNode = Nothing
        Me.tvTree.Cursor = System.Windows.Forms.Cursors.Default
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

