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
    Private WithEvents DetailsPanel As ArchetypeNodeConstraintControl
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

        If Not DesignMode Then
            mFileManager = Filemanager.Master
            InitialiseCardinality(StructureType.SECTION)
            DetailsPanel = New ArchetypeNodeConstraintControl(mFileManager)
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
    Friend WithEvents tvTree As System.Windows.Forms.TreeView
    Friend WithEvents ilSection As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuTree As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuSectionAdd As System.Windows.Forms.MenuItem
    Friend WithEvents NameSlotMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RemoveMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
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
        Me.tvTree = New System.Windows.Forms.TreeView
        Me.ContextMenuTree = New System.Windows.Forms.ContextMenu
        Me.MenuSectionAdd = New System.Windows.Forms.MenuItem
        Me.NameSlotMenuItem = New System.Windows.Forms.MenuItem
        Me.RemoveMenuItem = New System.Windows.Forms.MenuItem
        Me.SpecialiseMenuItem = New System.Windows.Forms.MenuItem
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
        'tvTree
        '
        Me.tvTree.AllowDrop = True
        Me.tvTree.ContextMenu = Me.ContextMenuTree
        Me.tvTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvTree.ImageIndex = 0
        Me.tvTree.ImageList = Me.ilSection
        Me.tvTree.LabelEdit = True
        Me.tvTree.Location = New System.Drawing.Point(32, 69)
        Me.tvTree.Name = "tvTree"
        Me.tvTree.SelectedImageIndex = 0
        Me.tvTree.Size = New System.Drawing.Size(328, 347)
        Me.tvTree.TabIndex = 4
        '
        'ContextMenuTree
        '
        Me.ContextMenuTree.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuSectionAdd, Me.RemoveMenuItem, Me.SpecialiseMenuItem, Me.NameSlotMenuItem})
        '
        'MenuSectionAdd
        '
        Me.MenuSectionAdd.Index = 0
        Me.MenuSectionAdd.Text = "New"
        '
        'RemoveMenuItem
        '
        Me.RemoveMenuItem.Index = 1
        Me.RemoveMenuItem.Text = "Remove"
        Me.RemoveMenuItem.Visible = False
        '
        'SpecialiseMenuItem
        '
        Me.SpecialiseMenuItem.Index = 2
        Me.SpecialiseMenuItem.Text = "Specialise"
        '
        'NameSlotMenuItem
        '
        Me.NameSlotMenuItem.Index = 3
        Me.NameSlotMenuItem.Text = "Name this slot"
        Me.NameSlotMenuItem.Visible = False
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
        Me.Controls.Add(Me.tvTree)
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

        TreeToRichText(tvTree.Nodes, text, level + 1)
        text.WriteLine("\pard\f0\fs20\par")
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
            ProcessChildrenRM_Structures(tvTree.Nodes, SectNode)
        Else
            SectNode = New RmSection(mFileManager.Archetype.ConceptCode)

            If tvTree.GetNodeCount(False) > 0 Then
                ProcessChildrenRM_Structures(tvTree.Nodes, SectNode)
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

    Public Sub ProcessSection(ByVal section As RmSection)
        SetCardinality(section)
        tvTree.Nodes.Clear()

        If mRootOfComposition Then
            For Each slot As RmSlot In section.Children
                Dim n As New ArchetypeTreeNode(CType(slot, RmSlot), mFileManager)
                tvTree.Nodes.Add(n)
            Next
        Else
            ProcessSectionRecursively(section, tvTree.Nodes)
        End If
    End Sub

    Private Sub ProcessSectionRecursively(ByVal section As RmSection, ByVal tvNodes As TreeNodeCollection)
        For Each node As Object In section.Children
            If TypeOf node Is RmSection Then
                Dim n As New ArchetypeTreeNode(CType(node, RmSection), mFileManager)
                tvNodes.Add(n)
                ProcessSectionRecursively(node, n.Nodes)
            ElseIf TypeOf node Is RmSlot Then
                Dim n As New ArchetypeTreeNode(CType(node, RmSlot), mFileManager)
                tvNodes.Add(n)
            End If
        Next
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
        TranslateSectionNodes(tvTree.Nodes)
        tvTree_AfterSelect(Me, Nothing)
    End Sub

    Public Sub TranslateGUI()
        mCardinalityControl.TranslateGUI()
    End Sub

    Protected Sub SetCardinality(ByVal rm As RmStructureCompound)
        If mCardinalityControl Is Nothing Then
            InitialiseCardinality(rm.Type)
        End If

        mCardinalityControl.Cardinality = rm.Children.Cardinality
    End Sub

    Protected Sub InitialiseCardinality(ByVal type As StructureType)
        mCardinalityControl = New OccurrencesPanel(mFileManager)
        mCardinalityControl.LocalFileManager = mFileManager

        If type = StructureType.Single Then
            mCardinalityControl.SetSingle = True
        Else
            mCardinalityControl.IsContainer = True
            mCardinalityControl.Location = New Point(0, 0)
            PanelTop.Controls.Add(mCardinalityControl)

            If type = StructureType.Cluster Or type = StructureType.SECTION Then
                mCardinalityControl.SetMandatory = True
                mCardinalityControl.IsContainer = True
            End If
        End If
    End Sub

    Protected ReadOnly Property SpecialisationDepth() As Integer
        Get
            Return mFileManager.OntologyManager.NumberOfSpecialisations
        End Get
    End Property

    Private Sub tvTree_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvTree.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = e.Node

            If e.Label = "" Or e.Label = " " Or e.Label = "  " Then
                e.CancelEdit = True
            Else
                tvNode.Text = e.Label
                tvNode.Item.Text = e.Label

                'Slots may set text to include class
                If tvNode.Text <> e.Label Then
                    e.CancelEdit = True
                End If
            End If
        End If
    End Sub

    Private Sub tvTree_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvTree.BeforeLabelEdit
        ' add the update of the Term and description

        Dim tvNode As ArchetypeTreeNode = e.Node

        If tvNode.Item.IsAnonymous Then
            e.CancelEdit = True
        End If
    End Sub

    Private Sub butRemoveSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveSection.Click, RemoveMenuItem.Click
        Dim n As TreeNode = tvTree.SelectedNode

        If Not n Is Nothing Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " " & n.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                tvTree.Nodes.Remove(n)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Public Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click, DetailsPanel.Specialise
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

                tvNode.ImageIndex = 0
                tvNode.SelectedImageIndex = 2
                tvTree.SelectedNode = tvNode
                DetailsPanel.ShowConstraint(False, False, False, tvNode.Item, mFileManager)
                tvNode.BeginEdit()
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub NameSlot_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NameSlotMenuItem.Click
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
                tvNode.ImageIndex = 0
                tvNode.SelectedImageIndex = 2
                nc.Insert(i, tvNode)

                mFileManager.FileEdited = True
                tvNode.EnsureVisible()
                tvTree.SelectedNode = tvNode
            End If
        End If
    End Sub

    Private Sub tvTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvTree.AfterSelect
        RemoveMenuItem.Visible = False
        SpecialiseMenuItem.Visible = False
        NameSlotMenuItem.Visible = False
        DetailsPanel.Visible = Not tvTree.SelectedNode Is Nothing

        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNode As ArchetypeTreeNode = CType(tvTree.SelectedNode, ArchetypeTreeNode)
            Dim item As ArchetypeNode = tvNode.Item

            SuspendLayout()
            DetailsPanel.ShowConstraint(False, False, False, item, mFileManager)
            ResumeLayout()

            If item.IsAnonymous Then
                NameSlotMenuItem.Visible = True
                RemoveMenuItem.Visible = True
            Else
                Dim nodeId As String = CType(item, ArchetypeNodeAbstract).NodeId
                Dim i As Integer = item.RM_Class.SpecialisationDepth

                RemoveMenuItem.Visible = i = SpecialisationDepth And (i = 0 Or nodeId.StartsWith("at0.") Or nodeId.IndexOf(".0.") > -1)
                SpecialiseMenuItem.Visible = SpecialisationDepth > 0 And (i < SpecialisationDepth Or item.Occurrences.IsMultiple)
                NameSlotMenuItem.Visible = item.IsAnonymous
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
        tvTree.ExpandAll()
        DetailsPanel.Visible = False
        PanelSectionConstraint.Controls.Add(DetailsPanel)
        DetailsPanel.Dock = DockStyle.Fill

        If tvTree.GetNodeCount(False) > 0 Then
            tvTree.SelectedNode = tvTree.Nodes(0)
        End If

        HelpProviderSection.HelpNamespace = Main.Instance.Options.HelpLocationPath

        If Not DesignMode Then
            'set the text for the menus
            RemoveMenuItem.Text = AE_Constants.Instance.Remove
            SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
            NameSlotMenuItem.Text = AE_Constants.Instance.NameThisSlot
        End If
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
            tvTree.Nodes.Add(tvNode)
        ElseIf tvTree.SelectedNode Is Nothing Or mNoHitNode Then
            tvTree.Nodes.Add(tvNode)
        ElseIf CType(tvTree.SelectedNode, ArchetypeTreeNode).Item.RM_Class.Type = StructureType.SECTION Then
            tvTree.SelectedNode.Nodes.Add(tvNode)
        ElseIf tvTree.SelectedNode.Parent Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvNode.BeginEdit()
        tvTree.SelectedNode = tvNode
    End Sub

    Sub AddSection(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String

        Debug.Assert(mRootOfComposition = False)

        'Fixme
        s = Filemanager.GetOpenEhrTerm(314, "Section")
        Dim tvNode As New ArchetypeTreeNode(s, StructureType.SECTION, mFileManager)

        If tvTree.SelectedNode Is Nothing Or mNoHitNode Then
            tvTree.Nodes.Add(tvNode)
        ElseIf CType(sender, MenuItem).Index = 1 Then  ' add as subsection
            tvTree.SelectedNode.Nodes.Add(tvNode)
        ElseIf tvTree.SelectedNode.Parent Is Nothing Then
            tvTree.Nodes.Add(tvNode)
        Else
            tvTree.SelectedNode.Parent.Nodes.Add(tvNode)
        End If

        mFileManager.FileEdited = True
        tvNode.EnsureVisible()
        tvTree.SelectedNode = tvNode
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
            'If (Not tvTree.SelectedNode Is Nothing) AndAlso _
            '    CType(tvTree.SelectedNode, ArchetypeTreeNode).Item.RM_Class.Type = _
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

    Private Sub tvTree_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvTree.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If Not tvTree.SelectedNode Is Nothing Then
                    tvTree.SelectedNode.BeginEdit()
                End If
            Case Keys.Delete
                butRemoveSection_Click(sender, New EventArgs)
        End Select
    End Sub

#End Region

#Region "Moving and dragging slots and sections"

    Private Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNodeCollection As TreeNodeCollection
            Dim tvParentNodeCollection As TreeNodeCollection = Nothing
            Dim i As Integer
            Dim tvN As TreeNode = tvTree.SelectedNode

            If tvN.Parent Is Nothing Then
                tvNodeCollection = tvTree.Nodes
            Else
                tvNodeCollection = tvN.Parent.Nodes

                If tvN.Parent.Parent Is Nothing Then
                    tvParentNodeCollection = tvTree.Nodes
                Else
                    tvParentNodeCollection = tvN.Parent.Parent.Nodes
                End If
            End If

            i = tvN.Index

            If i > 0 Then
                tvN.Remove()
                tvNodeCollection.Insert(i - 1, tvN)
                mFileManager.FileEdited = True
                tvTree.SelectedNode = tvN
            ElseIf i = 0 AndAlso Not tvParentNodeCollection Is Nothing Then
                Dim ii As Integer = tvN.Parent.Index
                tvN.Remove()
                tvParentNodeCollection.Insert(ii, tvN)
                mFileManager.FileEdited = True
                tvTree.SelectedNode = tvN
            End If
        End If
    End Sub

    Private Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not tvTree.SelectedNode Is Nothing Then
            Dim tvNodeCollection As TreeNodeCollection
            Dim tvParentNodeCollection As TreeNodeCollection = Nothing
            Dim i As Integer
            Dim tvN As TreeNode = tvTree.SelectedNode

            If tvN.Parent Is Nothing Then
                tvNodeCollection = tvTree.Nodes
            Else
                tvNodeCollection = tvN.Parent.Nodes

                If tvN.Parent.Parent Is Nothing Then
                    tvParentNodeCollection = tvTree.Nodes
                Else
                    tvParentNodeCollection = tvN.Parent.Parent.Nodes
                End If
            End If

            i = tvN.Index

            If Not tvN.NextNode Is Nothing Then
                tvN.Remove()
                tvNodeCollection.Insert(i + 1, tvN)
                mFileManager.FileEdited = True
                tvTree.SelectedNode = tvN
            ElseIf i = tvNodeCollection.Count - 1 AndAlso Not tvParentNodeCollection Is Nothing Then
                Dim ii As Integer = tvN.Parent.Index
                tvN.Remove()
                tvParentNodeCollection.Insert(ii + 1, tvN)
                mFileManager.FileEdited = True
                tvTree.SelectedNode = tvN
            End If
        End If
    End Sub

    Private Sub tvTree_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseDown
        mNoHitNode = False

        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            tvTree.Cursor = System.Windows.Forms.Cursors.Hand
        ElseIf e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim tn As TreeNode = tvTree.GetNodeAt(e.X, e.Y)

            If Not tn Is Nothing Then
                tvTree.SelectedNode = tn
            Else
                mNoHitNode = True
            End If
        End If
    End Sub

    Private Sub tvTree_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvTree.MouseUp
        tvTree.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub tvTree_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvTree.ItemDrag
        mNodeDragged = e.Item
        tvTree.DoDragDrop(e, DragDropEffects.Move)
    End Sub

    Private Sub tvTree_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub tvTree_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragOver
        'Prevent dropping a parent on a child

        e.Effect = e.AllowedEffect

        If Not mNodeDragged Is Nothing Then
            Dim position As Drawing.Point
            position.X = e.X
            position.Y = e.Y
            position = tvTree.PointToClient(position)
            Dim node As TreeNode = tvTree.GetNodeAt(position)

            While Not node Is Nothing
                node = node.Parent

                If node Is mNodeDragged Then
                    e.Effect = DragDropEffects.None
                    node = Nothing
                End If
            End While
        End If
    End Sub

    Private Sub tvTree_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvTree.DragDrop
        If Not mNodeDragged Is Nothing Then
            Dim position As Point
            position.X = e.X
            position.Y = e.Y
            position = tvTree.PointToClient(position)
            Dim dropNode As ArchetypeTreeNode = tvTree.GetNodeAt(position)

            If dropNode Is mNodeDragged Then
                Beep()
            Else
                Dim dropParent, dragParent As TreeNodeCollection
                Dim dropIndex As Integer

                If dropNode Is Nothing Then
                    dropParent = tvTree.Nodes
                    dropIndex = tvTree.GetNodeCount(False)
                ElseIf dropNode.Item.RM_Class.Type = StructureType.SECTION Then
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

                If mNodeDragged.Parent Is Nothing Then
                    dragParent = tvTree.Nodes
                Else
                    dragParent = mNodeDragged.Parent.Nodes
                End If

                dragParent.Remove(mNodeDragged)
                dropParent.Insert(dropIndex, mNodeDragged)
                mNodeDragged.EnsureVisible()
                tvTree.SelectedNode = mNodeDragged
                mFileManager.FileEdited = True
            End If
        End If

        tvTree.Cursor = System.Windows.Forms.Cursors.Default
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
