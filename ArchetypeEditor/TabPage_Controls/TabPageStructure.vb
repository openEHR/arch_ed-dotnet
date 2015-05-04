'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Option Explicit On

Public Class TabPageStructure
    Inherits System.Windows.Forms.UserControl

    Private mIsEmbedded As Boolean
    Private mEmbeddedSlot As ArchetypeNodeAnonymous
    Private mIsLoading As Boolean = False
    Private mIsState As Boolean
    Private mIsMandatory As Boolean = False
    Private mEmbeddedLoaded As Boolean = False
    Private mValidStructureClasses As StructureType()
    Private WithEvents mStructureControl As EntryStructure
    Private mFileManager As FileManagerLocal
    Friend WithEvents DetailsPanel As ArchetypeNodeConstraintControl
    Public Event UpdateStructure(ByVal sender As Object, ByVal newStructure As StructureType)
    Public Delegate Sub TabPageStructureUpdateStructure(ByVal sender As Object, ByVal newStructure As StructureType)

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        If Not DesignMode Then
            mFileManager = Filemanager.Master
            mValidStructureClasses = ReferenceModel.ValidStructureTypes
            DetailsPanel = New ArchetypeNodeConstraintControl(mFileManager)
            Controls.Add(DetailsPanel)
            TranslateGUI()

            Dim ds As DockStyle = DockStyle.Right

            If Main.Instance.IsDefaultLanguageRightToLeft Then
                ds = DockStyle.Left
                Main.Reflect(DetailsPanel)
            End If

            DetailsPanel.Dock = ds
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
    Friend WithEvents leftPanel As System.Windows.Forms.Panel
    Friend WithEvents displayPanel As System.Windows.Forms.Panel
    Friend WithEvents structurePanel As System.Windows.Forms.Panel
    Friend WithEvents comboStructure As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuListView As System.Windows.Forms.ContextMenu
    Friend WithEvents menuAdd_node As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddElement As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddElementText As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddElementQuantity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddItem As System.Windows.Forms.MenuItem
    Friend WithEvents menuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveElement As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuGrid As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemGridAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemGridAddColumn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemGridAddRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemGridRemove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemGridRemoveColumn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemGridRemoveRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemGridRename As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemRenameColumn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemRenameRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuGridAddText As System.Windows.Forms.MenuItem
    Friend WithEvents MenuGridAddQuantity As System.Windows.Forms.MenuItem
    Friend WithEvents ilSmall As System.Windows.Forms.ImageList
    Friend WithEvents MenuAddElementOrdinal As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddElementDateTime As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddElementBoolean As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddElementAny As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuSimple As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemSpecialiseSimple As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTipSpecialisation As System.Windows.Forms.ToolTip
    Friend WithEvents MenuGridAddCountable As System.Windows.Forms.MenuItem
    Friend WithEvents MenuGridAddBoolean As System.Windows.Forms.MenuItem
    Friend WithEvents MenuGridAddAny As System.Windows.Forms.MenuItem
    Friend WithEvents MenuGridAddOrdinal As System.Windows.Forms.MenuItem
    Friend WithEvents MenuGridAddDate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddElementCountable As System.Windows.Forms.MenuItem
    Friend WithEvents ttElement As System.Windows.Forms.ToolTip
    Friend WithEvents HelpProviderTabPageStructure As System.Windows.Forms.HelpProvider
    Friend WithEvents lblStructure As System.Windows.Forms.Label
    Friend WithEvents chkEmbedded As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabPageStructure))
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.displayPanel = New System.Windows.Forms.Panel
        Me.structurePanel = New System.Windows.Forms.Panel
        Me.chkEmbedded = New System.Windows.Forms.CheckBox
        Me.lblStructure = New System.Windows.Forms.Label
        Me.comboStructure = New System.Windows.Forms.ComboBox
        Me.ContextMenuGrid = New System.Windows.Forms.ContextMenu
        Me.MenuItemGridAdd = New System.Windows.Forms.MenuItem
        Me.MenuItemGridAddColumn = New System.Windows.Forms.MenuItem
        Me.MenuItemGridAddRow = New System.Windows.Forms.MenuItem
        Me.MenuGridAddText = New System.Windows.Forms.MenuItem
        Me.MenuGridAddQuantity = New System.Windows.Forms.MenuItem
        Me.MenuGridAddCountable = New System.Windows.Forms.MenuItem
        Me.MenuGridAddOrdinal = New System.Windows.Forms.MenuItem
        Me.MenuGridAddDate = New System.Windows.Forms.MenuItem
        Me.MenuGridAddBoolean = New System.Windows.Forms.MenuItem
        Me.MenuGridAddAny = New System.Windows.Forms.MenuItem
        Me.MenuItemGridRemove = New System.Windows.Forms.MenuItem
        Me.MenuItemGridRemoveColumn = New System.Windows.Forms.MenuItem
        Me.MenuItemGridRemoveRow = New System.Windows.Forms.MenuItem
        Me.MenuItemGridRename = New System.Windows.Forms.MenuItem
        Me.MenuItemRenameColumn = New System.Windows.Forms.MenuItem
        Me.MenuItemRenameRow = New System.Windows.Forms.MenuItem
        Me.ContextMenuSimple = New System.Windows.Forms.ContextMenu
        Me.MenuItemSpecialiseSimple = New System.Windows.Forms.MenuItem
        Me.ContextMenuListView = New System.Windows.Forms.ContextMenu
        Me.menuAdd_node = New System.Windows.Forms.MenuItem
        Me.MenuAddElement = New System.Windows.Forms.MenuItem
        Me.menuAddElementText = New System.Windows.Forms.MenuItem
        Me.menuAddElementQuantity = New System.Windows.Forms.MenuItem
        Me.MenuAddElementCountable = New System.Windows.Forms.MenuItem
        Me.MenuAddElementOrdinal = New System.Windows.Forms.MenuItem
        Me.MenuAddElementDateTime = New System.Windows.Forms.MenuItem
        Me.MenuAddElementBoolean = New System.Windows.Forms.MenuItem
        Me.MenuAddElementAny = New System.Windows.Forms.MenuItem
        Me.MenuAddItem = New System.Windows.Forms.MenuItem
        Me.menuRemove = New System.Windows.Forms.MenuItem
        Me.MenuRemoveElement = New System.Windows.Forms.MenuItem
        Me.ilSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.ttElement = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTipSpecialisation = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProviderTabPageStructure = New System.Windows.Forms.HelpProvider
        Me.leftPanel.SuspendLayout()
        Me.structurePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'leftPanel
        '
        Me.leftPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.leftPanel.BackColor = System.Drawing.Color.Transparent
        Me.leftPanel.Controls.Add(Me.displayPanel)
        Me.leftPanel.Controls.Add(Me.structurePanel)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.leftPanel.Location = New System.Drawing.Point(0, 0)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(650, 542)
        Me.leftPanel.TabIndex = 0
        '
        'displayPanel
        '
        Me.displayPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.displayPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me.displayPanel, System.Windows.Forms.HelpNavigator.Index)
        Me.displayPanel.Location = New System.Drawing.Point(0, 40)
        Me.displayPanel.Name = "displayPanel"
        Me.displayPanel.Padding = New System.Windows.Forms.Padding(2)
        Me.HelpProviderTabPageStructure.SetShowHelp(Me.displayPanel, True)
        Me.displayPanel.Size = New System.Drawing.Size(650, 502)
        Me.displayPanel.TabIndex = 1
        '
        'structurePanel
        '
        Me.structurePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.structurePanel.BackColor = System.Drawing.Color.Transparent
        Me.structurePanel.Controls.Add(Me.chkEmbedded)
        Me.structurePanel.Controls.Add(Me.lblStructure)
        Me.structurePanel.Controls.Add(Me.comboStructure)
        Me.structurePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.structurePanel.Location = New System.Drawing.Point(0, 0)
        Me.structurePanel.Name = "structurePanel"
        Me.structurePanel.Size = New System.Drawing.Size(650, 40)
        Me.structurePanel.TabIndex = 0
        '
        'chkEmbedded
        '
        Me.chkEmbedded.Location = New System.Drawing.Point(264, 5)
        Me.chkEmbedded.Name = "chkEmbedded"
        Me.chkEmbedded.Size = New System.Drawing.Size(200, 32)
        Me.chkEmbedded.TabIndex = 10
        Me.chkEmbedded.Text = "Embedded archetype"
        '
        'lblStructure
        '
        Me.lblStructure.BackColor = System.Drawing.Color.Transparent
        Me.lblStructure.Location = New System.Drawing.Point(9, 12)
        Me.lblStructure.Name = "lblStructure"
        Me.lblStructure.Size = New System.Drawing.Size(79, 16)
        Me.lblStructure.TabIndex = 1
        Me.lblStructure.Text = "Structure"
        Me.lblStructure.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'comboStructure
        '
        Me.HelpProviderTabPageStructure.SetHelpKeyword(Me.comboStructure, "HowTo/Editing/set_structure.htm")
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me.comboStructure, System.Windows.Forms.HelpNavigator.Topic)
        Me.comboStructure.Location = New System.Drawing.Point(97, 8)
        Me.comboStructure.Name = "comboStructure"
        Me.HelpProviderTabPageStructure.SetShowHelp(Me.comboStructure, True)
        Me.comboStructure.Size = New System.Drawing.Size(136, 21)
        Me.comboStructure.TabIndex = 7
        Me.comboStructure.DropDownStyle = ComboBoxStyle.DropDownList
        '
        'ContextMenuGrid
        '
        Me.ContextMenuGrid.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemGridAdd, Me.MenuItemGridRemove, Me.MenuItemGridRename})
        '
        'MenuItemGridAdd
        '
        Me.MenuItemGridAdd.Index = 0
        Me.MenuItemGridAdd.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemGridAddColumn, Me.MenuItemGridAddRow})
        Me.MenuItemGridAdd.Text = "Add"
        '
        'MenuItemGridAddColumn
        '
        Me.MenuItemGridAddColumn.Index = 0
        Me.MenuItemGridAddColumn.Text = "Column"
        '
        'MenuItemGridAddRow
        '
        Me.MenuItemGridAddRow.Index = 1
        Me.MenuItemGridAddRow.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuGridAddText, Me.MenuGridAddQuantity, Me.MenuGridAddCountable, Me.MenuGridAddOrdinal, Me.MenuGridAddDate, Me.MenuGridAddBoolean, Me.MenuGridAddAny})
        Me.MenuItemGridAddRow.Text = "Row"
        '
        'MenuGridAddText
        '
        Me.MenuGridAddText.Index = 0
        Me.MenuGridAddText.Text = "Text"
        '
        'MenuGridAddQuantity
        '
        Me.MenuGridAddQuantity.Index = 1
        Me.MenuGridAddQuantity.Text = "Quantity"
        '
        'MenuGridAddCountable
        '
        Me.MenuGridAddCountable.Index = 2
        Me.MenuGridAddCountable.Text = "Count"
        '
        'MenuGridAddOrdinal
        '
        Me.MenuGridAddOrdinal.Index = 3
        Me.MenuGridAddOrdinal.Text = "Ordinal"
        '
        'MenuGridAddDate
        '
        Me.MenuGridAddDate.Index = 4
        Me.MenuGridAddDate.Text = "DateTime"
        '
        'MenuGridAddBoolean
        '
        Me.MenuGridAddBoolean.Index = 5
        Me.MenuGridAddBoolean.Text = "Boolean"
        '
        'MenuGridAddAny
        '
        Me.MenuGridAddAny.Index = 6
        Me.MenuGridAddAny.Text = "Any"
        '
        'MenuItemGridRemove
        '
        Me.MenuItemGridRemove.Index = 1
        Me.MenuItemGridRemove.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemGridRemoveColumn, Me.MenuItemGridRemoveRow})
        Me.MenuItemGridRemove.Text = "Remove"
        '
        'MenuItemGridRemoveColumn
        '
        Me.MenuItemGridRemoveColumn.Index = 0
        Me.MenuItemGridRemoveColumn.Text = "Column"
        '
        'MenuItemGridRemoveRow
        '
        Me.MenuItemGridRemoveRow.Index = 1
        Me.MenuItemGridRemoveRow.Text = "Row"
        '
        'MenuItemGridRename
        '
        Me.MenuItemGridRename.Index = 2
        Me.MenuItemGridRename.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemRenameColumn, Me.MenuItemRenameRow})
        Me.MenuItemGridRename.Text = "Rename"
        '
        'MenuItemRenameColumn
        '
        Me.MenuItemRenameColumn.Index = 0
        Me.MenuItemRenameColumn.Text = "Column"
        '
        'MenuItemRenameRow
        '
        Me.MenuItemRenameRow.Index = 1
        Me.MenuItemRenameRow.Text = "Row"
        '
        'ContextMenuSimple
        '
        Me.ContextMenuSimple.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSpecialiseSimple})
        '
        'MenuItemSpecialiseSimple
        '
        Me.MenuItemSpecialiseSimple.Index = 0
        Me.MenuItemSpecialiseSimple.Text = "Specialise"
        '
        'ContextMenuListView
        '
        Me.ContextMenuListView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAdd_node, Me.menuRemove})
        '
        'menuAdd_node
        '
        Me.menuAdd_node.Index = 0
        Me.menuAdd_node.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuAddElement, Me.MenuAddItem})
        Me.menuAdd_node.Text = "New"
        '
        'MenuAddElement
        '
        Me.MenuAddElement.Index = 0
        Me.MenuAddElement.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAddElementText, Me.menuAddElementQuantity, Me.MenuAddElementCountable, Me.MenuAddElementOrdinal, Me.MenuAddElementDateTime, Me.MenuAddElementBoolean, Me.MenuAddElementAny})
        Me.MenuAddElement.Text = "Element"
        '
        'menuAddElementText
        '
        Me.menuAddElementText.Index = 0
        Me.menuAddElementText.Text = "Text"
        '
        'menuAddElementQuantity
        '
        Me.menuAddElementQuantity.Index = 1
        Me.menuAddElementQuantity.Text = "Quantity"
        '
        'MenuAddElementCountable
        '
        Me.MenuAddElementCountable.Index = 2
        Me.MenuAddElementCountable.Text = "Count"
        '
        'MenuAddElementOrdinal
        '
        Me.MenuAddElementOrdinal.Index = 3
        Me.MenuAddElementOrdinal.Text = "Ordinal"
        '
        'MenuAddElementDateTime
        '
        Me.MenuAddElementDateTime.Index = 4
        Me.MenuAddElementDateTime.Text = "DateTime"
        '
        'MenuAddElementBoolean
        '
        Me.MenuAddElementBoolean.Index = 5
        Me.MenuAddElementBoolean.Text = "Boolean"
        '
        'MenuAddElementAny
        '
        Me.MenuAddElementAny.Index = 6
        Me.MenuAddElementAny.Text = "Any"
        '
        'MenuAddItem
        '
        Me.MenuAddItem.Index = 1
        Me.MenuAddItem.Text = "Cluster"
        Me.MenuAddItem.Visible = False
        '
        'menuRemove
        '
        Me.menuRemove.Index = 1
        Me.menuRemove.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemoveElement})
        Me.menuRemove.Text = "Remove"
        '
        'MenuRemoveElement
        '
        Me.MenuRemoveElement.Index = 0
        Me.MenuRemoveElement.Text = "ElementName"
        '
        'ilSmall
        '
        Me.ilSmall.ImageStream = CType(resources.GetObject("ilSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.ilSmall.Images.SetKeyName(0, "")
        Me.ilSmall.Images.SetKeyName(1, "")
        Me.ilSmall.Images.SetKeyName(2, "")
        Me.ilSmall.Images.SetKeyName(3, "")
        Me.ilSmall.Images.SetKeyName(4, "")
        Me.ilSmall.Images.SetKeyName(5, "")
        Me.ilSmall.Images.SetKeyName(6, "")
        Me.ilSmall.Images.SetKeyName(7, "")
        Me.ilSmall.Images.SetKeyName(8, "")
        Me.ilSmall.Images.SetKeyName(9, "")
        Me.ilSmall.Images.SetKeyName(10, "")
        Me.ilSmall.Images.SetKeyName(11, "")
        Me.ilSmall.Images.SetKeyName(12, "")
        Me.ilSmall.Images.SetKeyName(13, "")
        Me.ilSmall.Images.SetKeyName(14, "")
        Me.ilSmall.Images.SetKeyName(15, "")
        Me.ilSmall.Images.SetKeyName(16, "")
        Me.ilSmall.Images.SetKeyName(17, "")
        Me.ilSmall.Images.SetKeyName(18, "")
        Me.ilSmall.Images.SetKeyName(19, "")
        Me.ilSmall.Images.SetKeyName(20, "")
        Me.ilSmall.Images.SetKeyName(21, "")
        Me.ilSmall.Images.SetKeyName(22, "")
        Me.ilSmall.Images.SetKeyName(23, "")
        Me.ilSmall.Images.SetKeyName(24, "")
        Me.ilSmall.Images.SetKeyName(25, "")
        Me.ilSmall.Images.SetKeyName(26, "")
        Me.ilSmall.Images.SetKeyName(27, "")
        Me.ilSmall.Images.SetKeyName(28, "")
        Me.ilSmall.Images.SetKeyName(29, "")
        Me.ilSmall.Images.SetKeyName(30, "")
        Me.ilSmall.Images.SetKeyName(31, "")
        Me.ilSmall.Images.SetKeyName(32, "")
        Me.ilSmall.Images.SetKeyName(33, "")
        '
        'HelpProviderTabPageStructure
        '
        Me.HelpProviderTabPageStructure.HelpNamespace = ""
        '
        'TabPageStructure
        '
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Controls.Add(Me.leftPanel)
        Me.HelpProviderTabPageStructure.SetHelpKeyword(Me, "Screens/data_screen.htm")
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.HelpProviderTabPageStructure.SetHelpString(Me, "")
        Me.Name = "TabPageStructure"
        Me.HelpProviderTabPageStructure.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(650, 542)
        Me.leftPanel.ResumeLayout(False)
        Me.structurePanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property IsState() As Boolean
        Get
            Return mIsState
        End Get
        Set(ByVal Value As Boolean)
            mIsState = Value

            If Value Then
                BackColor = System.Drawing.Color.LightSteelBlue
            Else
                BackColor = System.Drawing.Color.LemonChiffon
            End If
        End Set
    End Property

    Public Property IsMandatory() As Boolean
        Get
            Return mIsMandatory
        End Get
        Set(ByVal Value As Boolean)
            mIsMandatory = Value
        End Set
    End Property

    Public ReadOnly Property StructureType() As StructureType
        Get
            Dim result As StructureType = StructureType.Not_Set

            If mIsEmbedded Then
                If Not mEmbeddedSlot Is Nothing Then
                    Dim slot As RmSlot = TryCast(mEmbeddedSlot.RM_Class, RmSlot)

                    If Not slot Is Nothing Then
                        result = slot.SlotConstraint.RM_ClassType
                    End If
                End If
            ElseIf Not mStructureControl Is Nothing Then
                result = mStructureControl.StructureType
            End If

            Return result
        End Get
    End Property

    Public ReadOnly Property StructureTypeAsString() As String
        Get
            Return Filemanager.GetOpenEhrTerm(CInt(StructureType), StructureType.ToString)
        End Get
    End Property

    Public Function SaveAsStructure() As RmStructure
        ' Whether to save as RmStructureCompound or RmSlot
        Dim result As RmStructure = Nothing

        If mIsEmbedded Then
            If Not mEmbeddedSlot Is Nothing Then
                result = mEmbeddedSlot.RM_Class
            End If
        ElseIf Not mStructureControl Is Nothing Then
            result = mStructureControl.Archetype
        End If

        Return result
    End Function

    Public Sub DisallowEmbedded()
        chkEmbedded.Visible = False
    End Sub

    Public Sub SetButtonVisibility(ByVal node As ArchetypeNode)
        If Not mStructureControl Is Nothing Then
            mStructureControl.SetButtonVisibility(node)
        End If
    End Sub

    Protected Sub SetEntryStructure(ByVal value As EntryStructure)
        mStructureControl = value
        displayPanel.Controls.Clear()

        If Not value Is Nothing Then
            displayPanel.Controls.Add(value)
            value.Dock = DockStyle.Fill
            value.SetTextForNodeId(StructureTypeAsString)
        End If
    End Sub

    Private Sub TabPageStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HelpProviderTabPageStructure.HelpNamespace = Main.Instance.Options.HelpLocationPath
        structurePanel.Enabled = mFileManager.OntologyManager.NumberOfSpecialisations = 0

        If Not mIsEmbedded And comboStructure.Items.Count > 0 Then
            Dim wasLoading As Boolean = mIsLoading
            mIsLoading = True

            comboStructure.SelectedItem = StructureTypeAsString

            If comboStructure.SelectedIndex < 0 Then
                comboStructure.SelectedItem = Filemanager.GetOpenEhrTerm(CInt(StructureType.Tree), StructureType.Tree.ToString)

                If comboStructure.SelectedIndex < 0 Then
                    comboStructure.SelectedIndex = 0
                End If
            End If

            structurePanel.Visible = comboStructure.Items.Count > 1

            If structurePanel.Visible And structurePanel.Enabled Then
                comboStructure.Focus()
            End If

            mIsLoading = wasLoading
        End If
    End Sub

    Private Sub ShowDetailPanel(ByVal node As ArchetypeNode, ByVal e As EventArgs) Handles mStructureControl.CurrentItemChanged
        If node Is Nothing And mIsEmbedded Then
            node = mEmbeddedSlot
        End If

        If node Is Nothing Then
            DetailsPanel.Hide()
        Else
            DetailsPanel.ShowConstraint(StructureType = StructureType.Single, IsState, IsMandatory, node, mFileManager)
            DetailsPanel.Show()
        End If
    End Sub

    Private Sub Specialise(ByVal sender As Object, ByVal e As EventArgs) Handles DetailsPanel.Specialise
        If Not mStructureControl Is Nothing Then
            mStructureControl.SpecialiseCurrentItem(sender, e)
        End If
    End Sub

    Public Sub Translate()
        structurePanel.Enabled = mFileManager.OntologyManager.NumberOfSpecialisations = 0

        If mIsEmbedded Then
            ShowDetailPanel(mEmbeddedSlot, Nothing)
        ElseIf Not mStructureControl Is Nothing Then
            If mFileManager Is Filemanager.Master Then
                mStructureControl.Translate()
            Else
                Dim lang As String = Filemanager.Master.OntologyManager.LanguageCode

                If mFileManager.OntologyManager.HasLanguage(lang) Then
                    If mFileManager.OntologyManager.LanguageCode <> lang Then
                        mFileManager.OntologyManager.LanguageCode = lang
                        mStructureControl.Translate()
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub TranslateGUI()
        mIsLoading = True
        comboStructure.Items.Clear()

        If Not mValidStructureClasses Is Nothing Then
            For Each s As StructureType In mValidStructureClasses
                comboStructure.Items.Add(Filemanager.GetOpenEhrTerm(CInt(s), s.ToString))
            Next
        End If

        lblStructure.Text = Filemanager.GetOpenEhrTerm(85, lblStructure.Text)
        chkEmbedded.Text = Filemanager.GetOpenEhrTerm(605, chkEmbedded.Text)
        mIsLoading = False
    End Sub

    Public Sub toRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        If Not mStructureControl Is Nothing Then
            text.WriteLine(mStructureControl.ToRichText(level, Chr(13) & Chr(10)))
            text.WriteLine("\pard\f0\fs20\par")
        End If
    End Sub

    Public Sub toHTML(ByRef text As IO.StreamWriter, Optional ByVal BackGroundColour As String = "")
        If Not mStructureControl Is Nothing Then
            text.WriteLine(mStructureControl.ToHTML(BackGroundColour))
            text.WriteLine("<hr>")
        End If
    End Sub

    Public Sub ProcessElement(ByVal element As RmElement)
        mIsLoading = True
        structurePanel.Hide()
        displayPanel.Show()
        SetEntryStructure(New ElementOnly(element, mFileManager))
        mStructureControl.SetInitial()
        mIsLoading = False
    End Sub

    Public Sub ProcessStructure(ByVal struct As RmStructureCompound)
        mIsLoading = True

        If struct.Children Is Nothing Then
            structurePanel.Show()
            displayPanel.Hide()
        Else
            structurePanel.Hide()
            displayPanel.Show()

            Select Case struct.Type
                Case StructureType.Single
                    SetEntryStructure(New SimpleStructure(struct, mFileManager))

                Case StructureType.List
                    ' this also shows the panels and sets lvList to visible
                    SetEntryStructure(New ListStructure(struct, mFileManager))

                Case StructureType.Tree, StructureType.Cluster
                    SetEntryStructure(New TreeStructure(struct, mFileManager))

                Case StructureType.Table
                    SetEntryStructure(New TableStructure(CType(struct, RmTable), mFileManager))

                Case Else
                    Debug.Assert(False)
            End Select

            mStructureControl.SetInitial()
        End If

        mIsLoading = False
    End Sub

    Public Sub BuildInterface(ByVal container As Control, ByRef pos As Point, ByVal mandatoryOnly As Boolean)
        Dim spacer As Integer = 1

        If container.Name <> "tpInterface" Then
            container.Size = New Size
        End If

        If Not mStructureControl Is Nothing AndAlso mStructureControl.ItemCount > 0 Then
            ArchetypeView.Instance.BuildInterface(mStructureControl.InterfaceBuilder, container, pos, spacer, mandatoryOnly, mFileManager)
        End If
    End Sub

    Public Sub SetAsElement()
        structurePanel.Hide()
        displayPanel.Show()
        SetEntryStructure(New ElementOnly(New RmElement(mFileManager.Archetype.ConceptCode), mFileManager))
        DetailsPanel.Hide()
    End Sub

    Public Sub SetAsCluster()
        structurePanel.Hide()
        displayPanel.Show()
        SetEntryStructure(New TreeStructure(New RmCluster(mFileManager.Archetype.ConceptCode), mFileManager))
        DetailsPanel.Hide()
    End Sub

    Private Sub comboStructure_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles comboStructure.KeyDown
        If e.KeyCode = Keys.Escape And comboStructure.DroppedDown Then
            Enabled = False
            comboStructure.SelectedItem = StructureTypeAsString
            comboStructure.DroppedDown = False
            Enabled = True
            comboStructure.Focus()
        End If
    End Sub

    Private Sub comboStructure_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboStructure.SelectedIndexChanged, comboStructure.DropDownClosed
        Debug.Assert(Not mValidStructureClasses Is Nothing)

        If Enabled And comboStructure.SelectedIndex >= 0 And Not comboStructure.DroppedDown Then
            Dim chosenStructure As StructureType = mValidStructureClasses(comboStructure.SelectedIndex)

            If Not mIsLoading And Not mIsEmbedded And chosenStructure <> StructureType Then
                If Not mStructureControl Is Nothing AndAlso mStructureControl.ItemCount > 1 Then
                    If StructureType = StructureType.Tree Or chosenStructure = StructureType.Single Then
                        Dim question As String = String.Format(AE_Constants.Instance.ChangeStructureTypeWarning, Filemanager.GetOpenEhrTerm(CInt(chosenStructure), chosenStructure.ToString))

                        If MessageBox.Show(question, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                            comboStructure.SelectedItem = StructureTypeAsString
                        End If
                    End If
                End If
            End If

            If chosenStructure = mValidStructureClasses(comboStructure.SelectedIndex) Then
                ReferenceModel.SetStructureClass(chosenStructure)

                If mIsLoading Or mIsEmbedded <> chkEmbedded.Checked Or chosenStructure <> StructureType Then
                    mIsEmbedded = chkEmbedded.Checked

                    If mIsEmbedded Then
                        If Not mIsLoading Or mEmbeddedSlot Is Nothing Then
                            mEmbeddedSlot = New ArchetypeNodeAnonymous(chosenStructure)
                        End If

                        displayPanel.Hide()
                        ShowDetailPanel(mEmbeddedSlot, Nothing)
                    Else
                        If chosenStructure <> StructureType Then
                            Dim entryStructure As EntryStructure = Nothing ' User control to provide the list or whatever

                            Select Case chosenStructure
                                Case StructureType.Single
                                    entryStructure = New SimpleStructure(mFileManager)
                                Case StructureType.List
                                    entryStructure = New ListStructure(mFileManager)
                                Case StructureType.Tree
                                    entryStructure = New TreeStructure(mFileManager)
                                Case StructureType.Table
                                    entryStructure = New TableStructure(mFileManager)
                                Case Else
                                    Debug.Assert(False)
                            End Select

                            If Not entryStructure Is Nothing Then
                                If Not mStructureControl Is Nothing Then
                                    entryStructure.Archetype = mStructureControl.Archetype
                                End If

                                SetEntryStructure(entryStructure)
                            End If
                        End If

                        displayPanel.Show()
                        DetailsPanel.Visible = mStructureControl.ItemCount > 0
                    End If

                    If Not mIsLoading Then
                        mFileManager.FileEdited = True
                        RaiseEvent UpdateStructure(Me, chosenStructure)
                    End If
                End If
            End If
        End If
    End Sub

#Region "Features related to embedded archetype handling"

    Public Sub ProcessSlot(ByVal slot As RmSlot)
        mIsLoading = True
        mIsEmbedded = True
        mEmbeddedSlot = New ArchetypeNodeAnonymous(slot)
        comboStructure.SelectedIndex = -1

        mFileManager = New FileManagerLocal
        DetailsPanel.LocalFileManager = mFileManager
        mFileManager.ObjectToSave = Me
        mFileManager.FileLoading = True

        If OpenArchetypeForSlot(slot, Main.Instance.Options.RepositoryPath & "\structure") Then
            mFileManager.FileLoading = False
            Filemanager.AddEmbedded(mFileManager)
        Else
            mFileManager = Filemanager.Master
            chkEmbedded.Checked = True
            SetEntryStructure(Nothing)
            comboStructure.SelectedItem = StructureTypeAsString
        End If

        Translate()
        mIsLoading = False
    End Sub

    Private Function OpenArchetypeForSlot(ByVal slot As RmSlot, ByVal path As String) As Boolean
        Dim frm As New Choose
        frm.Set_Single()

        Dim dir As New IO.DirectoryInfo(path)

        If dir.Exists Then
            Dim sl As Constraint_Slot = slot.SlotConstraint
            Dim classPrefix As String = ReferenceModel.ReferenceModelName & "-" & ReferenceModel.RM_StructureName(sl.RM_ClassType)
            Dim pattern As String = ""
            Dim separator As String = ""

            For Each include As String In sl.Include
                pattern &= separator & include
                separator = "|"
            Next

            If pattern = "" And Not sl.ExcludeAll Then
                pattern = "[^.]+\.[^.]+"
            End If

            If Not pattern.StartsWith(classPrefix) Then
                pattern = classPrefix & "\.(" & pattern & ")"
            End If

            Dim inclusions As New System.Text.RegularExpressions.Regex("^" & pattern & "\.adl$")

            pattern = ""
            separator = ""

            For Each exclude As String In sl.Exclude
                pattern &= separator & exclude
                separator = "|"
            Next

            If Not pattern.StartsWith(classPrefix) Then
                pattern = classPrefix & "\.(" & pattern & ")"
            End If

            Dim exclusions As New System.Text.RegularExpressions.Regex("^" & pattern & "\.adl$")

            For Each f As IO.FileInfo In dir.GetFiles(classPrefix & ".*.adl")
                'SRH: this command on windows will return adls files as well (ext of length 3 are treated as wild!! ie = adl*)
                If inclusions.IsMatch(f.Name) And Not exclusions.IsMatch(f.Name) Then
                    frm.ListChoose.Items.Add(f.Name)
                End If
            Next
        End If

        Select Case frm.ListChoose.Items.Count
            Case 0
                Dim question As String = String.Format(AE_Constants.Instance.NoEmbeddedArchetypeMatches, path) & vbCrLf & vbCrLf & AE_Constants.Instance.Locate_file_yourself & "?"

                If MessageBox.Show(question, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim dlg As New FolderBrowserDialog

                    If dlg.ShowDialog(Me) = DialogResult.OK Then
                        OpenArchetypeForSlot = OpenArchetypeForSlot(slot, dlg.SelectedPath)
                    End If
                End If
            Case 1
                Dim question As String = Filemanager.GetOpenEhrTerm(606, "Load embedded archetype") & vbCrLf & frm.ListChoose.Items(0)

                If MessageBox.Show(question, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    OpenArchetype(IO.Path.Combine(path, frm.ListChoose.Items(0)))
                    OpenArchetypeForSlot = mFileManager.ArchetypeAvailable
                End If
            Case Else
                frm.LblForm.Text = Filemanager.GetOpenEhrTerm(605, "Embedded archetype")

                If frm.ShowDialog = Windows.Forms.DialogResult.OK And frm.ListChoose.SelectedItem IsNot Nothing Then
                    OpenArchetype(IO.Path.Combine(path, CStr(frm.ListChoose.SelectedItem)))
                    OpenArchetypeForSlot = mFileManager.ArchetypeAvailable
                End If
        End Select
    End Function

    Private Sub OpenArchetype(ByVal fileName As String)
        mFileManager.OpenArchetype(fileName)

        If Not mFileManager.HasOpenFileError Then
            Dim lbl As New Label
            lbl.Location = New System.Drawing.Point(120, 2)
            lbl.Width = 320
            lbl.Height = 24
            ProcessStructure(CType(mFileManager.Archetype.Definition, RmStructureCompound))
            lbl.Text = mFileManager.Archetype.Archetype_ID.ToString
            mStructureControl.PanelStructureHeader.Controls.Add(lbl)
            lbl.BringToFront()
        End If
    End Sub

    Public Sub PrepareToSave()
        mFileManager.Archetype.Definition = mStructureControl.Archetype
    End Sub

    Private Sub chkEmbedded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEmbedded.CheckedChanged
        If Not mIsLoading Then
            ' if it is not loading and is changed to false then need to remove the filemanager.
            'ToDo: needs to be more comprehensive if more than one embedded
            If Filemanager.HasEmbedded And Not Filemanager.Master Is mFileManager Then
                Filemanager.RemoveEmbedded(mFileManager)
                mFileManager = Filemanager.Master
            End If

            comboStructure_SelectedIndexChanged(sender, e)
        End If

        mIsEmbedded = chkEmbedded.Checked
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
'The Original Code is TabPageStructure.vb.
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
