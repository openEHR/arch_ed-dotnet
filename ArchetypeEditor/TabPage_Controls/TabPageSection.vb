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


Public Class TabPageSection
    Inherits System.Windows.Forms.UserControl

    Private mNodeDragged As ArchetypeTreeNode
    Private MenuItemSpecialise As MenuItem
    Private mConstraintDisplay As ArchetypeNodeConstraintControl
    Private mRootOfComposition As Boolean = False
    Private mFileManager As FileManagerLocal
    Private mNoHitNode As Boolean = False
    Private mCardinalityControl As OccurrencesPanel


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            mFileManager = Filemanager.Master
            SetCardinality(StructureType.SECTION)
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
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tvSection As System.Windows.Forms.TreeView
    Friend WithEvents ilSection As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuTree As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuSectionAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuNameSlot As System.Windows.Forms.MenuItem
    Friend WithEvents MenuSectionRemove As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents butListUp As System.Windows.Forms.Button
    Friend WithEvents butListDown As System.Windows.Forms.Button
    Friend WithEvents ButAddSubsection As System.Windows.Forms.Button
    Friend WithEvents butRemoveSection As System.Windows.Forms.Button
    Friend WithEvents PanelSectionConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents HelpProviderSection As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabPageSection))
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.ButAddSubsection = New System.Windows.Forms.Button
        Me.butRemoveSection = New System.Windows.Forms.Button
        Me.butListUp = New System.Windows.Forms.Button
        Me.butListDown = New System.Windows.Forms.Button
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.tvSection = New System.Windows.Forms.TreeView
        Me.ContextMenuTree = New System.Windows.Forms.ContextMenu
        Me.MenuSectionAdd = New System.Windows.Forms.MenuItem
        Me.MenuNameSlot = New System.Windows.Forms.MenuItem
        Me.MenuSectionRemove = New System.Windows.Forms.MenuItem
        Me.ilSection = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelLeft = New System.Windows.Forms.Panel
        Me.PanelSectionConstraint = New System.Windows.Forms.Panel
        Me.HelpProviderSection = New System.Windows.Forms.HelpProvider
        Me.PanelLeft.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(800, 69)
        Me.PanelTop.TabIndex = 0
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
        Me.ButAddSubsection.TabIndex = 1
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
        Me.butRemoveSection.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.butRemoveSection, "Remove section or slot")
        '
        'butListUp
        '
        Me.butListUp.Image = CType(resources.GetObject("butListUp.Image"), System.Drawing.Image)
        Me.butListUp.Location = New System.Drawing.Point(6, 64)
        Me.butListUp.Name = "butListUp"
        Me.butListUp.Size = New System.Drawing.Size(24, 24)
        Me.butListUp.TabIndex = 3
        '
        'butListDown
        '
        Me.butListDown.Image = CType(resources.GetObject("butListDown.Image"), System.Drawing.Image)
        Me.butListDown.Location = New System.Drawing.Point(6, 92)
        Me.butListDown.Name = "butListDown"
        Me.butListDown.Size = New System.Drawing.Size(24, 24)
        Me.butListDown.TabIndex = 4
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(360, 69)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(8, 347)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'tvSection
        '
        Me.tvSection.AllowDrop = True
        Me.tvSection.ContextMenu = Me.ContextMenuTree
        Me.tvSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvSection.ImageIndex = 0
        Me.tvSection.ImageList = Me.ilSection
        Me.tvSection.LabelEdit = True
        Me.tvSection.Location = New System.Drawing.Point(32, 69)
        Me.tvSection.Name = "tvSection"
        Me.tvSection.SelectedImageIndex = 0
        Me.tvSection.Size = New System.Drawing.Size(328, 347)
        Me.tvSection.TabIndex = 4
        '
        'ContextMenuTree
        '
        Me.ContextMenuTree.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuSectionAdd, Me.MenuSectionRemove, Me.MenuNameSlot})
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
        'MenuNameSlot
        '
        Me.MenuNameSlot.Index = 2
        Me.MenuNameSlot.Text = "Name this slot"
        Me.MenuSectionRemove.Visible = False
        '
        'ilSection
        '
        Me.ilSection.ImageStream = CType(resources.GetObject("ilSection.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilSection.TransparentColor = System.Drawing.Color.Transparent
        Me.ilSection.Images.SetKeyName(0, "")
        Me.ilSection.Images.SetKeyName(1, "")
        Me.ilSection.Images.SetKeyName(2, "")
        Me.ilSection.Images.SetKeyName(3, "")
        '
        'PanelLeft
        '
        Me.PanelLeft.Controls.Add(Me.butListUp)
        Me.PanelLeft.Controls.Add(Me.butListDown)
        Me.PanelLeft.Controls.Add(Me.ButAddSubsection)
        Me.PanelLeft.Controls.Add(Me.butRemoveSection)
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelLeft.Location = New System.Drawing.Point(0, 69)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(32, 347)
        Me.PanelLeft.TabIndex = 3
        '
        'PanelSectionConstraint
        '
        Me.PanelSectionConstraint.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSectionConstraint.Location = New System.Drawing.Point(368, 69)
        Me.PanelSectionConstraint.Name = "PanelSectionConstraint"
        Me.PanelSectionConstraint.Size = New System.Drawing.Size(432, 347)
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

    Private Sub TreeToRichText(ByVal TreeNodes As TreeNodeCollection, ByRef text As IO.StringWriter, ByVal level As Integer)
        Dim n As ArchetypeTreeNode

        For Each n In TreeNodes
            text.WriteLine(n.Item.ToRichText(level))
            If n.GetNodeCount(False) > 0 Then
                TreeToRichText(n.Nodes, text, level + 3)
            End If
        Next
    End Sub

    Public Sub ToRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        Dim s As String = ""

        If mCardinalityControl.cbOrdered.Checked Then
            s = "ordered"
        End If

        s = s.Trim

        If s <> "" Then
            text.WriteLine(Space(3 * level) & s & "\par")
        End If

        TreeToRichText(tvSection.Nodes, text, level + 1)
        text.WriteLine("\pard\f0\fs20\par")
    End Sub

    Private Sub ProcessSection(ByVal A_sect As RmSection, ByVal tvNodes As TreeNodeCollection)
        Dim rm As Object

        ' can be RmSlot or RmStructureCompound
        For Each rm In A_sect.Children
            If TypeOf rm Is RmSection Then
                Dim n As ArchetypeTreeNode
                n = New ArchetypeTreeNode(CType(rm, RmSection), mFileManager)
                tvNodes.Add(n)
                ProcessSection(rm, n.Nodes)
            ElseIf TypeOf rm Is RmSlot Then
                Dim n As New ArchetypeTreeNode(CType(rm, RmSlot), mFileManager)
                tvNodes.Add(n)
            End If
        Next
    End Sub

    Private Sub ProcessChildrenRM_Structures(ByVal colNodes As TreeNodeCollection, ByRef a_section As RmSection)
        Dim tvNode As TreeNode
        Dim SectionNode As RmSection

        For Each tvNode In colNodes
            Dim rm As RmStructure = CType(tvNode, ArchetypeTreeNode).Item.RM_Class

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
    End Sub

    Public Function SaveAsSection() As RmSection
        Dim SectNode As RmSection

        If mRootOfComposition Then
            SectNode = New RmSection("root")
            ProcessChildrenRM_Structures(tvSection.Nodes, SectNode)
        Else
            SectNode = New RmSection(mFileManager.Archetype.ConceptCode)

            If tvSection.GetNodeCount(False) > 0 Then
                ProcessChildrenRM_Structures(tvSection.Nodes, SectNode)
            End If
        End If

        If SectNode.Children.Count > 0 Then
            SectNode.Children.Cardinality = mCardinalityControl.Cardinality
            'Changed SRH Jan 2007 - added cardinality control
            'If cbFixed.Checked Then
            '    Dim i As Integer
            '    For Each rm As RmStructure In SectNode.Children
            '        If rm.Occurrences.IsUnbounded Then
            '            Return SectNode
            '        End If
            '        i += rm.Occurrences.MaxCount
            '    Next
            '    SectNode.Children.Cardinality.MaxCount = i
            'End If
        End If

        Return SectNode
    End Function

    Public Sub ProcessSection(ByVal a_section As RmSection)
        Dim rm_node As Object
        SetCardinality(a_section)
        tvSection.Nodes.Clear()

        If mRootOfComposition Then
            For Each slot As RmSlot In a_section.Children
                Dim n As New ArchetypeTreeNode(CType(slot, RmSlot), mFileManager)
                tvSection.Nodes.Add(n)
            Next
        Else
            For Each rm_node In a_section.Children
                If TypeOf rm_node Is RmSection Then
                    Dim n As New ArchetypeTreeNode(CType(rm_node, RmStructureCompound), mFileManager)
                    tvSection.Nodes.Add(n)
                    ProcessSection(rm_node, n.Nodes)
                ElseIf TypeOf rm_node Is RmSlot Then
                    Dim n As New ArchetypeTreeNode(CType(rm_node, RmSlot), mFileManager)
                    tvSection.Nodes.Add(n)
                Else
                    Debug.Assert(False, "Type not catered for")
                End If
            Next
        End If
    End Sub

    Private Sub TranslateSectionNodes(ByRef tvnodes As TreeNodeCollection)
        For Each n As TreeNode In tvnodes
            CType(n, ArchetypeTreeNode).Translate()

            If n.GetNodeCount(False) > 0 Then
                TranslateSectionNodes(n.Nodes)
            End If
        Next
    End Sub

    Public Sub Translate()
        TranslateSectionNodes(tvSection.Nodes)
    End Sub

    Public Sub TranslateGUI()
        mCardinalityControl.TranslateGUI()
    End Sub

    Protected Sub SetCardinality(ByVal rm As RmStructureCompound)
        If mCardinalityControl Is Nothing Then
            SetCardinality(rm.Type)
        End If

        mCardinalityControl.Cardinality = rm.Children.Cardinality
    End Sub

    Protected Sub SetCardinality(ByVal a_structure_type As StructureType)
        mCardinalityControl = New OccurrencesPanel(mFileManager)
        mCardinalityControl.LocalFileManager = mFileManager

        If a_structure_type = StructureType.Single Then
            mCardinalityControl.SetSingle = True
        Else
            mCardinalityControl.IsContainer = True
            mCardinalityControl.Location = New Drawing.Point(0, 0)
            PanelTop.Controls.Add(mCardinalityControl)
        End If
    End Sub

    Private Sub tvSection_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvSection.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = e.Node

            If e.Label = "" Or e.Label = " " Or e.Label = "  " Then
                e.CancelEdit = True
            Else
                tvNode.Text = e.Label

                'Slots may set text to include class
                If tvNode.Text <> e.Label Then
                    e.CancelEdit = True
                End If
            End If
        End If
    End Sub

    Private Sub tvSection_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvSection.BeforeLabelEdit
        ' add the update of the Term and description

        Dim tvNode As ArchetypeTreeNode

        tvNode = e.Node
        If tvNode.RM_Class.Type = StructureType.Slot AndAlso TypeOf (tvNode.Item) Is ArchetypeNodeAnonymous Then
            e.CancelEdit = True
        End If
    End Sub

    Private Sub butRemoveSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveSection.Click, MenuSectionRemove.Click
        Dim n As TreeNode = tvSection.SelectedNode

        If Not n Is Nothing Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " " & n.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                tvSection.Nodes.Remove(n)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub NameSlot_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuNameSlot.Click
        Dim a_node As ArchetypeTreeNode = tvSection.SelectedNode
        Dim slot As ArchetypeNodeAnonymous = CType(a_node.Item, ArchetypeNodeAnonymous)
        Dim newSlot As New ArchetypeSlot(slot, mFileManager)
        Dim i As Integer = a_node.Index
        Dim nc As TreeNodeCollection

        If a_node.Parent Is Nothing Then
            nc = tvSection.Nodes
        Else
            nc = a_node.Parent.Nodes
        End If

        a_node.Remove()

        a_node = New ArchetypeTreeNode(newSlot)
        nc.Insert(i, a_node)

        a_node.ImageIndex = 0
        a_node.SelectedImageIndex = 2

        a_node.EnsureVisible()
        tvSection.SelectedNode = a_node

        mFileManager.FileEdited = True
        a_node.BeginEdit()
    End Sub

    Private Sub tvSection_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSection.AfterSelect
        ' pass the active selection as an iArchetypeItemNode
        MenuNameSlot.Visible = False

        If tvSection.SelectedNode Is Nothing Then
            MenuSectionRemove.Visible = False
            mConstraintDisplay.Visible = False
        Else
            MenuSectionRemove.Visible = True
            Dim a_node As ArchetypeTreeNode = tvSection.SelectedNode

            Try
                SuspendLayout()
                mConstraintDisplay.ShowConstraint(False, False, a_node.Item, mFileManager)
                ResumeLayout()
            Catch
                Debug.Assert(False, "Type is not catered for")
                Return
            End Try

            If a_node.Item.RM_Class.Type = StructureType.Slot AndAlso TypeOf a_node.Item Is ArchetypeNodeAnonymous Then
                MenuNameSlot.Visible = True
            End If

            If Not mConstraintDisplay.Visible Then
                mConstraintDisplay.Visible = True
            End If
        End If
    End Sub

    Private Sub cbOrdered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mFileManager.FileEdited = True
    End Sub

    Private Sub cbFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mFileManager.FileEdited = True
    End Sub

    Private Sub TabPageSection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tvSection.ExpandAll()
        mConstraintDisplay = New ArchetypeNodeConstraintControl(mFileManager)
        mConstraintDisplay.Visible = False
        PanelSectionConstraint.Controls.Add(mConstraintDisplay)
        mConstraintDisplay.Dock = DockStyle.Fill

        If tvSection.GetNodeCount(False) > 0 Then
            tvSection.SelectedNode = tvSection.Nodes(0)
        End If

        HelpProviderSection.HelpNamespace = Main.Instance.Options.HelpLocationPath
    End Sub

#Region "Buttons and menus - adding and removing slots and sections"

    Sub AddSlot(ByVal sender As Object, ByVal e As EventArgs)
        Dim tvNode As ArchetypeTreeNode = Nothing
        Dim editLabel As Boolean = False
        Dim struct_type As StructureType = CInt(CType(sender, MenuItem).Tag)

        Select Case MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            Case DialogResult.Yes
                tvNode = New ArchetypeTreeNode(New ArchetypeSlot(struct_type.ToString, struct_type, mFileManager))
                editLabel = True
            Case DialogResult.No
                tvNode = New ArchetypeTreeNode(New ArchetypeNodeAnonymous(struct_type))
            Case DialogResult.Cancel
                Return
        End Select

        tvNode.ImageIndex = 0
        tvNode.SelectedImageIndex = 2

        If mRootOfComposition Then
            tvSection.Nodes.Add(tvNode)
        ElseIf tvSection.SelectedNode Is Nothing Or mNoHitNode Then
            tvSection.Nodes.Add(tvNode)
        ElseIf CType(tvSection.SelectedNode, ArchetypeTreeNode).Item.RM_Class.Type = StructureType.SECTION Then
            tvSection.SelectedNode.Nodes.Add(tvNode)
        ElseIf tvSection.SelectedNode.Parent Is Nothing Then
            tvSection.Nodes.Add(tvNode)
        Else
            tvSection.SelectedNode.Parent.Nodes.Add(tvNode)
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvNode.BeginEdit()
        tvSection.SelectedNode = tvNode
    End Sub

    Sub AddSection(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String

        Debug.Assert(mRootOfComposition = False)

        'Fixme
        s = Filemanager.GetOpenEhrTerm(314, "Section")
        Dim tvNode As New ArchetypeTreeNode(s, StructureType.SECTION, mFileManager)

        If tvSection.SelectedNode Is Nothing Or mNoHitNode Then
            tvSection.Nodes.Add(tvNode)
        ElseIf CType(sender, MenuItem).Index = 1 Then  ' add as subsection
            tvSection.SelectedNode.Nodes.Add(tvNode)
        ElseIf tvSection.SelectedNode.Parent Is Nothing Then
            tvSection.Nodes.Add(tvNode)
        Else
            tvSection.SelectedNode.Parent.Nodes.Add(tvNode)
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvSection.SelectedNode = tvNode
        tvNode.BeginEdit()
    End Sub

    Private Sub ButAddSubsection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddSubsection.Click
        CreateContextMenu.Show(ButAddSubsection, ButAddSubsection.Location)
    End Sub

    Private Function CreateContextMenu() As ContextMenu
        Dim cm As New ContextMenu
        Dim mi As MenuItem

        If mRootOfComposition Then
            mi = New MenuItem(Filemanager.GetOpenEhrTerm(312, "Slot"))
            cm.MenuItems.Add(mi)

            For Each strtype As StructureType In ReferenceModel.validArchetypeSlots(StructureType.COMPOSITION)
                mi = New MenuItem(Filemanager.GetOpenEhrTerm(strtype, strtype.ToString))
                mi.Tag = CInt(strtype)
                AddHandler mi.Click, AddressOf AddSlot
                cm.MenuItems(cm.MenuItems.Count - 1).MenuItems.Add(mi)
            Next
        Else
            'Change SRH: Only subsections allowed
            'mi = New MenuItem(Filemanager.GetOpenEhrTerm(314, "Section"))
            'AddHandler mi.Click, AddressOf AddSection
            'cm.MenuItems.Add(mi)
            'If (Not tvSection.SelectedNode Is Nothing) AndAlso _
            '    CType(tvSection.SelectedNode, ArchetypeTreeNode).Item.RM_Class.Type = _
            '    StructureType.SECTION Then

            mi = New MenuItem(Filemanager.GetOpenEhrTerm(558, "Sub-Section"))
            AddHandler mi.Click, AddressOf AddSection
            cm.MenuItems.Add(mi)
            'End If
            mi = New MenuItem(Filemanager.GetOpenEhrTerm(312, "Slot"))
            cm.MenuItems.Add(mi)

            For Each strtype As StructureType In ReferenceModel.validArchetypeSlots(StructureType.SECTION)
                mi = New MenuItem(Filemanager.GetOpenEhrTerm(strtype, strtype.ToString))
                mi.Tag = CInt(strtype)
                AddHandler mi.Click, AddressOf AddSlot
                cm.MenuItems(cm.MenuItems.Count - 1).MenuItems.Add(mi)
            Next
        End If

        Return cm
    End Function

    Private Sub ContextMenuTree_Popup(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContextMenuTree.Popup
        ContextMenuTree.MenuItems(0).MenuItems.Clear()
        ContextMenuTree.MenuItems(0).MergeMenu(CreateContextMenu)
    End Sub

    Private Sub tvSection_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvSection.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If Not tvSection.SelectedNode Is Nothing Then
                    tvSection.SelectedNode.BeginEdit()
                End If
            Case Keys.Delete
                butRemoveSection_Click(sender, New EventArgs)
        End Select
    End Sub

#End Region

#Region "Moving and dragging slots and sections"

    Private Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not tvSection.SelectedNode Is Nothing Then
            Dim tvNodeCollection As TreeNodeCollection
            Dim tvParentNodeCollection As TreeNodeCollection = Nothing
            Dim i As Integer
            Dim tvN As TreeNode = tvSection.SelectedNode

            If tvN.Parent Is Nothing Then
                tvNodeCollection = tvSection.Nodes
            Else
                tvNodeCollection = tvN.Parent.Nodes

                If tvN.Parent.Parent Is Nothing Then
                    tvParentNodeCollection = tvSection.Nodes
                Else
                    tvParentNodeCollection = tvN.Parent.Parent.Nodes
                End If
            End If

            i = tvN.Index

            If i > 0 Then
                tvN.Remove()
                tvNodeCollection.Insert(i - 1, tvN)
                mFileManager.FileEdited = True
                tvSection.SelectedNode = tvN
            ElseIf i = 0 AndAlso Not tvParentNodeCollection Is Nothing Then
                Dim ii As Integer = tvN.Parent.Index
                tvN.Remove()
                tvParentNodeCollection.Insert(ii, tvN)
                mFileManager.FileEdited = True
                tvSection.SelectedNode = tvN
            End If
        End If
    End Sub

    Private Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not tvSection.SelectedNode Is Nothing Then
            Dim tvNodeCollection As TreeNodeCollection
            Dim tvParentNodeCollection As TreeNodeCollection = Nothing
            Dim i As Integer
            Dim tvN As TreeNode = tvSection.SelectedNode

            If tvN.Parent Is Nothing Then
                tvNodeCollection = tvSection.Nodes
            Else
                tvNodeCollection = tvN.Parent.Nodes

                If tvN.Parent.Parent Is Nothing Then
                    tvParentNodeCollection = tvSection.Nodes
                Else
                    tvParentNodeCollection = tvN.Parent.Parent.Nodes
                End If
            End If

            i = tvN.Index

            If Not tvN.NextNode Is Nothing Then
                tvN.Remove()
                tvNodeCollection.Insert(i + 1, tvN)
                mFileManager.FileEdited = True
                tvSection.SelectedNode = tvN
            ElseIf i = tvNodeCollection.Count - 1 AndAlso Not tvParentNodeCollection Is Nothing Then
                Dim ii As Integer = tvN.Parent.Index
                tvN.Remove()
                tvParentNodeCollection.Insert(ii + 1, tvN)
                mFileManager.FileEdited = True
                tvSection.SelectedNode = tvN
            End If
        End If
    End Sub

    Private Sub tvSection_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvSection.MouseDown
        mNoHitNode = False

        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            tvSection.Cursor = System.Windows.Forms.Cursors.Hand
        ElseIf e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim tn As TreeNode = tvSection.GetNodeAt(e.X, e.Y)

            If Not tn Is Nothing Then
                tvSection.SelectedNode = tn
            Else
                mNoHitNode = True
            End If
        End If
    End Sub

    Private Sub tvSection_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvSection.MouseUp
        tvSection.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub tvSection_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvSection.ItemDrag
        mNodeDragged = e.Item
        tvSection.DoDragDrop(e, DragDropEffects.Move)
    End Sub

    Private Sub tvSection_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvSection.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub tvSection_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvSection.DragOver
        'Prevent dropping a parent on a child

        e.Effect = e.AllowedEffect

        If Not mNodeDragged Is Nothing Then
            Dim position As Drawing.Point
            position.X = e.X
            position.Y = e.Y
            position = tvSection.PointToClient(position)
            Dim node As TreeNode = tvSection.GetNodeAt(position)

            While Not node Is Nothing
                node = node.Parent

                If node Is mNodeDragged Then
                    e.Effect = DragDropEffects.None
                    node = Nothing
                End If
            End While
        End If
    End Sub

    Private Sub tvSection_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvSection.DragDrop
        If Not mNodeDragged Is Nothing Then
            Dim position As Point
            position.X = e.X
            position.Y = e.Y
            position = tvSection.PointToClient(position)
            Dim dropNode As ArchetypeTreeNode = tvSection.GetNodeAt(position)

            If dropNode Is mNodeDragged Then
                Beep()
            Else
                Dim dropParent, dragParent As TreeNodeCollection
                Dim dropIndex As Integer

                If dropNode Is Nothing Then
                    dropParent = tvSection.Nodes
                    dropIndex = tvSection.GetNodeCount(False)
                ElseIf dropNode.Item.RM_Class.Type = StructureType.SECTION Then
                    dropParent = dropNode.Nodes

                    If dropNode.IsExpanded Then
                        dropIndex = 0
                    Else
                        dropIndex = dropNode.GetNodeCount(False)
                    End If
                Else
                    If dropNode.Parent Is Nothing Then
                        dropParent = tvSection.Nodes
                    Else
                        dropParent = dropNode.Parent.Nodes
                    End If

                    dropIndex = dropNode.Index + 1
                End If

                If mNodeDragged.Parent Is Nothing Then
                    dragParent = tvSection.Nodes
                Else
                    dragParent = mNodeDragged.Parent.Nodes
                End If

                dragParent.Remove(mNodeDragged)
                dropParent.Insert(dropIndex, mNodeDragged)
                mNodeDragged.EnsureVisible()
                tvSection.SelectedNode = mNodeDragged
                mFileManager.FileEdited = True
            End If
        End If

        tvSection.Cursor = System.Windows.Forms.Cursors.Default
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
