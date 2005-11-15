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


Public Class TabPageSection
    Inherits System.Windows.Forms.UserControl

    Private mNodeDragged As ArchetypeTreeNode
    Private MenuItemSpecialise As MenuItem
    Private mConstraintDisplay As ArchetypeNodeConstraintControl
    Private mRootOfComposition As Boolean = False


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tvSection As System.Windows.Forms.TreeView
    Friend WithEvents ilSection As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuTree As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuSectionAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuSectionRemove As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents butListUp As System.Windows.Forms.Button
    Friend WithEvents butListDown As System.Windows.Forms.Button
    Friend WithEvents cbFixed As System.Windows.Forms.CheckBox
    Friend WithEvents cbOrdered As System.Windows.Forms.CheckBox
    Friend WithEvents ButAddSubsection As System.Windows.Forms.Button
    Friend WithEvents butRemoveSection As System.Windows.Forms.Button
    Friend WithEvents PanelSectionConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents HelpProviderSection As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabPageSection))
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.cbFixed = New System.Windows.Forms.CheckBox
        Me.cbOrdered = New System.Windows.Forms.CheckBox
        Me.ButAddSubsection = New System.Windows.Forms.Button
        Me.butRemoveSection = New System.Windows.Forms.Button
        Me.butListUp = New System.Windows.Forms.Button
        Me.butListDown = New System.Windows.Forms.Button
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.tvSection = New System.Windows.Forms.TreeView
        Me.ContextMenuTree = New System.Windows.Forms.ContextMenu
        Me.MenuSectionAdd = New System.Windows.Forms.MenuItem
        Me.MenuSectionRemove = New System.Windows.Forms.MenuItem
        Me.ilSection = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelLeft = New System.Windows.Forms.Panel
        Me.PanelSectionConstraint = New System.Windows.Forms.Panel
        Me.HelpProviderSection = New System.Windows.Forms.HelpProvider
        Me.PanelTop.SuspendLayout()
        Me.PanelLeft.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.cbFixed)
        Me.PanelTop.Controls.Add(Me.cbOrdered)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(800, 40)
        Me.PanelTop.TabIndex = 0
        '
        'cbFixed
        '
        Me.cbFixed.Location = New System.Drawing.Point(248, 8)
        Me.cbFixed.Name = "cbFixed"
        Me.cbFixed.Size = New System.Drawing.Size(88, 24)
        Me.cbFixed.TabIndex = 37
        Me.cbFixed.Text = "Fixed"
        '
        'cbOrdered
        '
        Me.cbOrdered.Location = New System.Drawing.Point(138, 8)
        Me.cbOrdered.Name = "cbOrdered"
        Me.cbOrdered.Size = New System.Drawing.Size(102, 24)
        Me.cbOrdered.TabIndex = 36
        Me.cbOrdered.Text = "Ordered"
        '
        'ButAddSubsection
        '
        Me.ButAddSubsection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButAddSubsection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButAddSubsection.Image = CType(resources.GetObject("ButAddSubsection.Image"), System.Drawing.Image)
        Me.ButAddSubsection.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButAddSubsection.Location = New System.Drawing.Point(6, 8)
        Me.ButAddSubsection.Name = "ButAddSubsection"
        Me.ButAddSubsection.Size = New System.Drawing.Size(24, 24)
        Me.ButAddSubsection.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.ButAddSubsection, "Add a section or a slot")
        '
        'butRemoveSection
        '
        Me.butRemoveSection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveSection.Image = CType(resources.GetObject("butRemoveSection.Image"), System.Drawing.Image)
        Me.butRemoveSection.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveSection.Location = New System.Drawing.Point(6, 36)
        Me.butRemoveSection.Name = "butRemoveSection"
        Me.butRemoveSection.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveSection.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.butRemoveSection, "Remove section or slot")
        '
        'butListUp
        '
        Me.butListUp.Image = CType(resources.GetObject("butListUp.Image"), System.Drawing.Image)
        Me.butListUp.Location = New System.Drawing.Point(6, 64)
        Me.butListUp.Name = "butListUp"
        Me.butListUp.Size = New System.Drawing.Size(24, 24)
        Me.butListUp.TabIndex = 32
        '
        'butListDown
        '
        Me.butListDown.Image = CType(resources.GetObject("butListDown.Image"), System.Drawing.Image)
        Me.butListDown.Location = New System.Drawing.Point(6, 92)
        Me.butListDown.Name = "butListDown"
        Me.butListDown.Size = New System.Drawing.Size(24, 24)
        Me.butListDown.TabIndex = 33
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(360, 40)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(8, 376)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'tvSection
        '
        Me.tvSection.AllowDrop = True
        Me.tvSection.ContextMenu = Me.ContextMenuTree
        Me.tvSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvSection.ImageList = Me.ilSection
        Me.tvSection.LabelEdit = True
        Me.tvSection.Location = New System.Drawing.Point(32, 40)
        Me.tvSection.Name = "tvSection"
        Me.tvSection.Size = New System.Drawing.Size(328, 376)
        Me.tvSection.TabIndex = 3
        '
        'ContextMenuTree
        '
        Me.ContextMenuTree.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuSectionAdd, Me.MenuSectionRemove})
        '
        'MenuSectionAdd
        '
        Me.MenuSectionAdd.Index = 0
        Me.MenuSectionAdd.Text = "New"
        '
        'MenuSectionRemove
        '
        Me.MenuSectionRemove.Index = 1
        Me.MenuSectionRemove.Text = "Remove"
        Me.MenuSectionRemove.Visible = False
        '
        'ilSection
        '
        Me.ilSection.ImageSize = New System.Drawing.Size(20, 20)
        Me.ilSection.ImageStream = CType(resources.GetObject("ilSection.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilSection.TransparentColor = System.Drawing.Color.Transparent
        '
        'PanelLeft
        '
        Me.PanelLeft.Controls.Add(Me.butListUp)
        Me.PanelLeft.Controls.Add(Me.butListDown)
        Me.PanelLeft.Controls.Add(Me.ButAddSubsection)
        Me.PanelLeft.Controls.Add(Me.butRemoveSection)
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelLeft.Location = New System.Drawing.Point(0, 40)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(32, 376)
        Me.PanelLeft.TabIndex = 4
        '
        'PanelSectionConstraint
        '
        Me.PanelSectionConstraint.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSectionConstraint.Location = New System.Drawing.Point(368, 40)
        Me.PanelSectionConstraint.Name = "PanelSectionConstraint"
        Me.PanelSectionConstraint.Size = New System.Drawing.Size(432, 376)
        Me.PanelSectionConstraint.TabIndex = 5
        '
        'TabPageSection
        '
        Me.BackColor = System.Drawing.Color.LightYellow
        Me.Controls.Add(Me.tvSection)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.PanelSectionConstraint)
        Me.Controls.Add(Me.PanelLeft)
        Me.Controls.Add(Me.PanelTop)
        Me.HelpProviderSection.SetHelpKeyword(Me, "Screens/section_screen.html")
        Me.HelpProviderSection.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "TabPageSection"
        Me.HelpProviderSection.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(800, 416)
        Me.PanelTop.ResumeLayout(False)
        Me.PanelLeft.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Property IsRootOfComposition() As Boolean
        Get
            Return mRootOfComposition
        End Get
        Set(ByVal Value As Boolean)
            mRootOfComposition = Value
        End Set
    End Property

#Region "Functions"


    Private Function TreeToRichText(ByVal TreeNodes As TreeNodeCollection, ByRef text As IO.StringWriter, ByVal level As Integer)
        Dim n As ArchetypeTreeNode
        Dim x, s, s1 As String

        For Each n In TreeNodes
            text.WriteLine(n.Item.ToRichText(level))
            If n.GetNodeCount(False) > 0 Then
                TreeToRichText(n.Nodes, text, level + 3)
            End If
        Next
    End Function

    Public Function ToRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        Dim s, x As String

        s = ""
        If Me.cbOrdered.Checked Then
            s = "ordered"
        End If
        If Me.cbFixed.Checked Then
            s = s & " fixed"
        End If

        s = s.Trim

        If s <> "" Then
            text.WriteLine(Space(3 * level) & s & "\par")
        End If
        TreeToRichText(Me.tvSection.Nodes, text, level + 1)
        text.WriteLine("\pard\f0\fs20\par")

    End Function


    Private Sub ProcessSection(ByVal A_sect As RmSection, ByVal tvNodes As TreeNodeCollection)
        Dim rm As Object

        ' can be RmSlot or RmStructureCompound
        For Each rm In A_sect.Children

            If TypeOf rm Is RmSection Then
                Dim n As ArchetypeTreeNode
                n = New ArchetypeTreeNode(CType(rm, RmSection))
                tvNodes.Add(n)
                ProcessSection(rm, n.Nodes)
            ElseIf TypeOf rm Is RmSlot Then
                Dim n As New ArchetypeTreeNode(CType(rm, RmSlot))
                tvNodes.Add(n)
            End If
        Next

    End Sub

    Private Function ProcessChildrenRM_Structures(ByVal colNodes As TreeNodeCollection, ByRef a_section As RmSection)

        Dim tvNode As TreeNode
        Dim SectionNode As RmSection

        For Each tvNode In colNodes
            Dim rm As RmStructure
            rm = CType(tvNode, ArchetypeTreeNode).Item.RM_Class
            If rm.Type = StructureType.SECTION Then
                SectionNode = New RmSection(rm)
                ProcessChildrenRM_Structures(tvNode.Nodes, SectionNode)
                a_section.Children.Add(SectionNode)
            ElseIf rm.Type = StructureType.Slot Then
                a_section.Children.Add(rm)
            Else
                Debug.Assert(False, "Type is not catered for")
            End If
        Next

    End Function

    Public Function SaveAsSection() As RmSection
        Dim tvNode As ArchetypeTreeNode
        Dim SectNode As RmSection

        'Try
        If mRootOfComposition Then
            SectNode = New RmSection("root")
            ProcessChildrenRM_Structures(Me.tvSection.Nodes, SectNode)

        Else
            If Me.tvSection.GetNodeCount(False) > 0 Then
                tvNode = Me.tvSection.Nodes(0)

                SectNode = New RmSection(tvNode.RM_Class)
                ProcessChildrenRM_Structures(tvNode.Nodes, SectNode)
            Else
                Return Nothing
            End If
        End If

        SectNode.Children.Cardinality.Ordered = Me.cbOrdered.Checked
        If Me.cbFixed.Checked Then
            Dim i As Integer
            For Each rm As RmStructure In SectNode.Children
                If rm.Occurrences.IsUnbounded Then
                    Return SectNode
                End If
                i += rm.Occurrences.MaxCount
            Next
            SectNode.Children.Cardinality.MaxCount = i
        End If
        Return SectNode

        'Catch e As Exception
        '   MessageBox.Show(AE_Constants.Instance.Error_saving & " " & AE_Constants.Instance.Section & ": " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Function

    Public Function ProcessSection(ByVal a_section As RmSection)
        Dim rm_node As Object

        Me.tvSection.Nodes.Clear()

        If mRootOfComposition Then

            For Each slot As RmSlot In a_section.Children
                Dim n As New ArchetypeTreeNode(CType(slot, RmSlot))

                tvSection.Nodes.Add(n)
            Next
        Else
            Dim a_section_tree_node As New ArchetypeTreeNode(a_section, Filemanager.Instance)

            
            Me.tvSection.Nodes.Add(a_section_tree_node)

            For Each rm_node In a_section.Children

                If TypeOf rm_node Is RmSection Then
                    Dim n As New ArchetypeTreeNode(CType(rm_node, RmStructureCompound), Filemanager.Instance)
                    a_section_tree_node.Nodes.Add(n)
                    ProcessSection(rm_node, n.Nodes)
                ElseIf TypeOf rm_node Is RmSlot Then
                    Dim n As New ArchetypeTreeNode(CType(rm_node, RmSlot))
                    a_section_tree_node.Nodes.Add(n)
                Else
                    Debug.Assert(False, "Type not catered for")
                End If
            Next

        End If
        
    End Function

    Private Function TranslateSectionNodes(ByRef tvnodes As TreeNodeCollection)
        For Each n As TreeNode In tvnodes
            CType(n, ArchetypeTreeNode).Translate()
            If n.GetNodeCount(False) > 0 Then
                TranslateSectionNodes(n.Nodes)
            End If
        Next
    End Function

    Public Sub Translate()
        TranslateSectionNodes(Me.tvSection.Nodes)

    End Sub


#End Region

    Private Sub tvSection_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvSection.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            Dim tvNode As ArchetypeTreeNode

            tvNode = e.Node
            If tvNode.Text = "" Then
                e.CancelEdit = True
                Return
            End If

            tvNode.Text = e.Label
        End If
    End Sub

    Private Sub tvSection_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvSection.BeforeLabelEdit
        ' add the update of the Term and description

        Dim tvNode As ArchetypeTreeNode

        tvNode = e.Node
        If tvNode.RM_Class.Type = StructureType.Slot Then
            Beep()
            e.CancelEdit = True
        End If
    End Sub

    Private Sub butRemoveSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveSection.Click, MenuSectionRemove.Click
        Dim n As TreeNode = Me.tvSection.SelectedNode
        If Not n Is Nothing Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " " & n.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                Me.tvSection.Nodes.Remove(n)
            End If
        End If
    End Sub


    Private Sub tvSection_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSection.AfterSelect
        ' pass the active selection as an iArchetypeItemNode
        If tvSection.SelectedNode Is Nothing Then
            MenuSectionRemove.Visible = False
            mConstraintDisplay.Visible = False
        Else
            MenuSectionRemove.Visible = True
            Dim a_node As ArchetypeTreeNode = tvSection.SelectedNode
            Try
                Me.SuspendLayout()
                mConstraintDisplay.ShowConstraint(a_node.RM_Class.Type, _
                     False, a_node.Item, Filemanager.Instance)
                Me.ResumeLayout()
            Catch
                Debug.Assert(False, "Type is not catered for")
            End Try

            If Not mConstraintDisplay.Visible Then
                mConstraintDisplay.Visible = True
            End If
        End If

    End Sub


    Private Sub cbOrdered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdered.Click
        Filemanager.Instance.FileEdited = True
    End Sub

    Private Sub cbFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFixed.Click
        Filemanager.Instance.FileEdited = True
    End Sub

    Private Sub TabPageSection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.tvSection.ExpandAll()
        mConstraintDisplay = New ArchetypeNodeConstraintControl
        mConstraintDisplay.Visible = False
        Me.PanelSectionConstraint.Controls.Add(mConstraintDisplay)
        mConstraintDisplay.Dock = DockStyle.Fill
        If Me.tvSection.GetNodeCount(False) > 0 Then
            Me.tvSection.SelectedNode = Me.tvSection.Nodes(0)
        End If
        Me.HelpProviderSection.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
    End Sub

#Region "Buttons and menus - adding and removing slots and sections"

    Sub AddSlot(ByVal sender As Object, ByVal e As EventArgs)
        Dim struct_type As StructureType
        Dim tvNode As ArchetypeTreeNode

        If mRootOfComposition Then
            struct_type = ReferenceModel.Instance.validArchetypeSlots(StructureType.COMPOSITION).GetValue(CType(sender, MenuItem).Index)

            Dim archNode As New ArchetypeNodeAnonymous(struct_type)

            tvNode = New ArchetypeTreeNode(archNode)
            tvNode.SelectedImageIndex = 2
            Me.tvSection.Nodes.Add(tvNode)

        Else
            struct_type = ReferenceModel.Instance.validArchetypeSlots(StructureType.SECTION).GetValue(CType(sender, MenuItem).Index)

            Dim archNode As New ArchetypeNodeAnonymous(struct_type)

            tvNode = New ArchetypeTreeNode(archNode)
            tvNode.SelectedImageIndex = 2

            If CType(tvSection.SelectedNode, ArchetypeTreeNode).Item.RM_Class.Type = StructureType.SECTION Then
                Me.tvSection.SelectedNode.Nodes.Add(tvNode)
            Else
                Debug.Assert(Not tvSection.SelectedNode Is Nothing)
                Me.tvSection.SelectedNode.Parent.Nodes.Add(tvNode)
            End If
        End If
        Filemanager.Instance.FileEdited = True
        tvNode.EnsureVisible()
        Me.tvSection.SelectedNode = tvNode

        
    End Sub

    Sub AddSection(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String

        Debug.Assert(mRootOfComposition = False)

        'Fixme
        s = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(2000, "New Section")
        Dim tvNode As New ArchetypeTreeNode(s, StructureType.SECTION, Filemanager.Instance)

        If Me.tvSection.SelectedNode Is Nothing Then
            Me.tvSection.Nodes.Add(tvNode)
        Else
            If CType(sender, MenuItem).Index = 1 Then  ' add as subsection
                Me.tvSection.SelectedNode.Nodes.Add(tvNode)
            Else
                If Me.tvSection.SelectedNode.Parent Is Nothing Then
                    Me.tvSection.Nodes.Add(tvNode)
                Else
                    Me.tvSection.SelectedNode.Parent.Nodes.Add(tvNode)
                End If
            End If
        End If
        Filemanager.Instance.FileEdited = True
        tvNode.EnsureVisible()
        Me.tvSection.SelectedNode = tvNode
        tvNode.BeginEdit()
    End Sub

    Private Sub ButAddSubsection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddSubsection.Click
        CreateContextMenu.Show(ButAddSubsection, ButAddSubsection.Location)
    End Sub

    Private Function CreateContextMenu() As ContextMenu
        Dim cm As New ContextMenu
        Dim mi As MenuItem


        If mRootOfComposition Then
            mi = New MenuItem("Slot")
            cm.MenuItems.Add(mi)

            For Each strtype As StructureType In ReferenceModel.Instance.validArchetypeSlots(StructureType.COMPOSITION)
                mi = New MenuItem(strtype.ToString)

                AddHandler mi.Click, AddressOf AddSlot
                cm.MenuItems(cm.MenuItems.Count - 1).MenuItems.Add(mi)
            Next
            Return cm
        Else

            If tvSection.SelectedNode Is Nothing Then
                mi = New MenuItem("SECTION")
                AddHandler mi.Click, AddressOf AddSection
                cm.MenuItems.Add(mi)
            Else
                If CType(tvSection.SelectedNode, ArchetypeTreeNode).Item.RM_Class.Type = StructureType.SECTION Then
                    mi = New MenuItem("SECTION")
                    AddHandler mi.Click, AddressOf AddSection
                    cm.MenuItems.Add(mi)
                    mi = New MenuItem("Sub-SECTION")
                    AddHandler mi.Click, AddressOf AddSection
                    cm.MenuItems.Add(mi)
                End If
            End If

            If Me.tvSection.GetNodeCount(False) > 0 Then
                mi = New MenuItem("Slot")
                cm.MenuItems.Add(mi)

                For Each strtype As StructureType In ReferenceModel.Instance.validArchetypeSlots(StructureType.SECTION)
                    mi = New MenuItem(strtype.ToString)

                    AddHandler mi.Click, AddressOf AddSlot
                    cm.MenuItems(cm.MenuItems.Count - 1).MenuItems.Add(mi)
                Next
            End If
            Return cm
        End If
        
    End Function

    Private Sub ContextMenuTree_Popup(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContextMenuTree.Popup
        ContextMenuTree.MenuItems(0).MenuItems.Clear()
        ContextMenuTree.MenuItems(0).MergeMenu(CreateContextMenu)
    End Sub

#End Region

#Region "Moving and dragging slots and sections"

    Private Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not Me.tvSection.SelectedNode Is Nothing Then
            Dim tvN As TreeNode
            Dim tvN_C As TreeNodeCollection
            Dim i As Integer

            tvN = Me.tvSection.SelectedNode
            If tvN.Parent Is Nothing Then
                tvN_C = Me.tvSection.Nodes
            Else
                tvN_C = tvN.Parent.Nodes
            End If

            i = tvN.Index

            If i > 0 Then
                tvN.Remove()
                tvN_C.Insert((i - 1), tvN)
                Filemanager.Instance.FileEdited = True
                Me.tvSection.SelectedNode = tvN
            End If
        End If

    End Sub

    Private Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not Me.tvSection.SelectedNode Is Nothing Then
            Dim tvN As TreeNode
            Dim tvN_C As TreeNodeCollection
            Dim i As Integer

            tvN = Me.tvSection.SelectedNode
            If tvN.Parent Is Nothing Then
                tvN_C = Me.tvSection.Nodes
            Else
                tvN_C = tvN.Parent.Nodes
            End If

            i = tvN.Index

            If Not tvN.NextNode Is Nothing Then
                tvN.Remove()
                tvN_C.Insert((i + 1), tvN)
                Filemanager.Instance.FileEdited = True
                Me.tvSection.SelectedNode = tvN
            End If
        End If
    End Sub

    Private Sub tvSection_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvSection.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.tvSection.Cursor = System.Windows.Forms.Cursors.Hand
        ElseIf e.Button = MouseButtons.Right Then
            Dim tn As TreeNode
            tn = Me.tvSection.GetNodeAt(e.X, e.Y)
            If Not tn Is Nothing Then
                Me.tvSection.SelectedNode = tn
            End If
        End If
    End Sub

    Private Sub tvSection_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvSection.MouseUp
        Me.tvSection.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub tvSection_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvSection.ItemDrag
        mNodeDragged = e.Item
        Me.tvSection.DoDragDrop(e, DragDropEffects.Move)
    End Sub

    Private Sub tvSection_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvSection.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub tvSection_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvSection.DragDrop
        Dim position As Point
        Dim DropNode As ArchetypeTreeNode
        Dim DropParent As TreeNodeCollection
        Dim DragParent As TreeNodeCollection


        If Not mNodeDragged Is Nothing Then

            position.X = e.X
            position.Y = e.Y
            position = Me.tvSection.PointToClient(position)
            DropNode = Me.tvSection.GetNodeAt(position)

            If Not DropNode Is Nothing Then
                If DropNode.Parent Is Nothing Then
                    ' force drop on the section that is parent
                    DropParent = Me.tvSection.Nodes(0).Nodes
                Else
                    DropParent = DropNode.Parent.Nodes  ' if it is a slot, need to drop it on the drop parent
                End If

                If mNodeDragged.Parent Is Nothing Then
                    ' can't drag root
                    Beep()
                    Return
                Else
                    DragParent = mNodeDragged.Parent.Nodes
                End If

                DragParent.Remove(mNodeDragged)

                ' indent if start at same level
                If (DropParent Is DragParent) And (DropNode.RM_Class.Type = StructureType.SECTION) Then
                    DropNode.Nodes.Add(mNodeDragged)
                Else
                    ' different level or not section
                    If mNodeDragged.RM_Class.Type = StructureType.Slot And DropNode.RM_Class.Type = StructureType.SECTION Then
                        DropNode.Nodes.Add(mNodeDragged)
                    Else
                        DropParent.Insert(DropNode.Index + 1, mNodeDragged)
                    End If

                End If
                Filemanager.Instance.FileEdited = True
                mNodeDragged.EnsureVisible()
                Me.tvSection.SelectedNode = mNodeDragged
            End If

        End If

        Me.tvSection.Cursor = System.Windows.Forms.Cursors.Default

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
'The Original Code is TabPageSection.vb.
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
