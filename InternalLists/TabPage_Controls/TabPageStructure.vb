'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
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
    Private mEmbeddedAllowed As Boolean = True
    Private mEmbeddedLoaded As Boolean = False
    Private mIsCluster As Boolean = False
    Private mIsElement As Boolean = False
    Private mValidStructureClasses As StructureType()
    Private WithEvents mArchetypeControl As EntryStructure
    Private WithEvents mSplitter As Splitter
    Private mFileManager As FileManagerLocal
    Friend WithEvents PanelDetails As ArchetypeNodeConstraintControl

    'SRH: Jan 6th 2010 - EDT 585
    Private mIsMandatory As Boolean = False

    'SRH: Jan 11 2009 - EDT-486 - update structure
    Public Event UpdateStructure(ByVal sender As Object, ByVal newStructure As StructureType)
    Public Delegate Sub TabPageStructureUpdateStructure(ByVal sender As Object, ByVal newStructure As StructureType)

    

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal aEditor As Designer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        If Not Me.DesignMode Then
            Dim ds As DockStyle = DockStyle.Right

            mFileManager = Filemanager.Master
            mValidStructureClasses = ReferenceModel.ValidStructureTypes
            For Each ValidStructure As StructureType In mValidStructureClasses
                Me.comboStructure.Items.Add(Filemanager.GetOpenEhrTerm(CInt(ValidStructure), ValidStructure.ToString))
            Next

            PanelDetails = New ArchetypeNodeConstraintControl(mFileManager)
            Me.panelStructure.Controls.Add(PanelDetails)

            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                Me.comboStructure.Text = Filemanager.GetOpenEhrTerm(104, "Choose...")
                TranslateGUI()
                If OceanArchetypeEditor.IsDefaultLanguageRightToLeft Then
                    ds = DockStyle.Left
                    OceanArchetypeEditor.Reflect(PanelDetails)
                End If
            End If

            PanelDetails.Dock = ds

            mSplitter = New Splitter
            mSplitter.Dock = ds
            Me.panelStructure.Controls.Add(mSplitter)

            Me.panelDisplay.Dock = DockStyle.Fill

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
    Friend WithEvents panelStructure As System.Windows.Forms.Panel
    Friend WithEvents panelDisplay As System.Windows.Forms.Panel
    Friend WithEvents panelEntry As System.Windows.Forms.Panel
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
        Me.panelStructure = New System.Windows.Forms.Panel
        Me.panelDisplay = New System.Windows.Forms.Panel
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
        Me.panelEntry = New System.Windows.Forms.Panel
        Me.chkEmbedded = New System.Windows.Forms.CheckBox
        Me.lblStructure = New System.Windows.Forms.Label
        Me.comboStructure = New System.Windows.Forms.ComboBox
        Me.ttElement = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTipSpecialisation = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProviderTabPageStructure = New System.Windows.Forms.HelpProvider
        Me.panelStructure.SuspendLayout()
        Me.panelEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelStructure
        '
        Me.panelStructure.BackColor = System.Drawing.Color.Transparent
        Me.panelStructure.Controls.Add(Me.panelDisplay)
        Me.panelStructure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelStructure.Location = New System.Drawing.Point(0, 40)
        Me.panelStructure.Name = "panelStructure"
        Me.panelStructure.Size = New System.Drawing.Size(650, 502)
        Me.panelStructure.TabIndex = 8
        Me.panelStructure.Visible = False
        '
        'panelDisplay
        '
        Me.panelDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me.panelDisplay, System.Windows.Forms.HelpNavigator.Index)
        Me.panelDisplay.Location = New System.Drawing.Point(192, 84)
        Me.panelDisplay.Name = "panelDisplay"
        Me.panelDisplay.Padding = New System.Windows.Forms.Padding(2)
        Me.HelpProviderTabPageStructure.SetShowHelp(Me.panelDisplay, True)
        Me.panelDisplay.Size = New System.Drawing.Size(342, 350)
        Me.panelDisplay.TabIndex = 0
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
        'panelEntry
        '
        Me.panelEntry.BackColor = System.Drawing.Color.Transparent
        Me.panelEntry.Controls.Add(Me.chkEmbedded)
        Me.panelEntry.Controls.Add(Me.lblStructure)
        Me.panelEntry.Controls.Add(Me.comboStructure)
        Me.panelEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelEntry.Location = New System.Drawing.Point(0, 0)
        Me.panelEntry.Name = "panelEntry"
        Me.panelEntry.Size = New System.Drawing.Size(650, 40)
        Me.panelEntry.TabIndex = 9
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
        Me.lblStructure.TabIndex = 9
        Me.lblStructure.Text = "Structure"
        Me.lblStructure.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'comboStructure
        '
        Me.HelpProviderTabPageStructure.SetHelpKeyword(Me.comboStructure, "HowTo/Edit data/set_structure.htm")
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me.comboStructure, System.Windows.Forms.HelpNavigator.Topic)
        Me.comboStructure.Location = New System.Drawing.Point(97, 8)
        Me.comboStructure.Name = "comboStructure"
        Me.HelpProviderTabPageStructure.SetShowHelp(Me.comboStructure, True)
        Me.comboStructure.Size = New System.Drawing.Size(136, 21)
        Me.comboStructure.TabIndex = 7
        Me.comboStructure.Text = "Choose..."
        '
        'HelpProviderTabPageStructure
        '
        Me.HelpProviderTabPageStructure.HelpNamespace = ""
        '
        'TabPageStructure
        '
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(650, 542)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Controls.Add(Me.panelStructure)
        Me.Controls.Add(Me.panelEntry)
        Me.HelpProviderTabPageStructure.SetHelpKeyword(Me, "Screens/data_screen.htm")
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.HelpProviderTabPageStructure.SetHelpString(Me, "")
        Me.Name = "TabPageStructure"
        Me.HelpProviderTabPageStructure.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(650, 542)
        Me.panelStructure.ResumeLayout(False)
        Me.panelEntry.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Properties"

    Public Property ArchetypeDisplay() As EntryStructure
        Get
            Return mArchetypeControl
        End Get
        Set(ByVal Value As EntryStructure)
            mArchetypeControl = Value
            If Me.panelDisplay.Controls.Count > 0 Then
                Me.panelDisplay.Controls.Clear()
            End If
            Me.panelDisplay.Controls.Add(Value)
            Value.Dock = DockStyle.Fill
        End Set
    End Property
    Public ReadOnly Property ComponentType() As String
        Get
            Return "Structure"
        End Get
    End Property
    Public Property IsState() As Boolean
        Get
            Return mIsState
        End Get
        Set(ByVal Value As Boolean)
            mIsState = Value
            If Value Then
                Me.BackColor = System.Drawing.Color.LightSteelBlue
            Else
                Me.BackColor = System.Drawing.Color.LemonChiffon
            End If
        End Set
    End Property
    'SRH: Jan 6th 2010 - EDT 585
    Public Property IsMandatory() As Boolean
        Get
            Return mIsMandatory
        End Get
        Set(ByVal Value As Boolean)
            mIsMandatory = Value
        End Set
    End Property
    Public Property EmbeddedAllowed() As Boolean
        Get
            Return mEmbeddedAllowed
        End Get
        Set(ByVal Value As Boolean)
            mEmbeddedAllowed = Value
        End Set
    End Property
    Public Property ShowStructureCombo() As Boolean
        Get
            Return Me.panelEntry.Visible
        End Get
        Set(ByVal Value As Boolean)
            If Me.panelEntry.Visible <> Value Then
                Me.panelEntry.Visible = Value
            End If
        End Set
    End Property
    Public ReadOnly Property StructureType() As StructureType
        Get
            If mArchetypeControl Is Nothing Then
                Return Nothing
            Else
                Return mArchetypeControl.StructureType
            End If
        End Get
    End Property
    Public ReadOnly Property StructureTypeAsString() As String
        Get
            If mArchetypeControl Is Nothing Then
                Return ""
            Else
                Select Case mArchetypeControl.StructureType
                    Case StructureType.Single
                        Return Filemanager.GetOpenEhrTerm(105, "Single")
                    Case StructureType.List
                        Return Filemanager.GetOpenEhrTerm(106, "List")
                    Case StructureType.Tree
                        Return Filemanager.GetOpenEhrTerm(107, "Tree")
                    Case StructureType.Table
                        Return Filemanager.GetOpenEhrTerm(108, "Structure")
                    Case Else
                        Debug.Assert(False)
                        Return ""
                End Select
            End If
        End Get
    End Property
    Public ReadOnly Property Elements() As ArchetypeElement()
        Get
            If Not mArchetypeControl Is Nothing Then
                Return mArchetypeControl.Elements
            Else
                Return Nothing
            End If
        End Get
    End Property
#End Region

#Region "Functions"

    Private Sub TabPageStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkEmbedded.Visible = mEmbeddedAllowed
        HelpProviderTabPageStructure.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
    End Sub

    Sub ShowStructurePanel(ByVal Sender As Object, ByVal e As EventArgs) Handles mArchetypeControl.ChangeStructure
        panelEntry.Show()
        Me.comboStructure.Focus()
    End Sub

    Sub ShowDetailPanel(ByVal CurrentItem As ArchetypeNode, ByVal e As EventArgs) Handles mArchetypeControl.CurrentItemChanged
        If CurrentItem Is Nothing Then
            PanelDetails.Hide()
        Else
            Dim s As StructureType

            If mArchetypeControl Is Nothing Then  'with a slot only
                If TypeOf (CurrentItem) Is ArchetypeNodeAnonymous Then
                    s = CType(CurrentItem, ArchetypeNodeAnonymous).RM_Class.Type
                ElseIf TypeOf (CurrentItem) Is ArchetypeSlot Then
                    s = CType(CurrentItem, ArchetypeSlot).RM_Class.Type
                End If
            Else
                s = mArchetypeControl.StructureType
            End If

            PanelDetails.ShowConstraint(s, IsState, IsMandatory, CurrentItem, mFileManager)

                If Not PanelDetails.Visible Then
                    PanelDetails.Show()
                End If
            End If
    End Sub

    Public Sub Translate()
        If Not mArchetypeControl Is Nothing Then
            If mFileManager Is Filemanager.Master Then
                mArchetypeControl.Translate()
            Else
                'SRH: 17 Aug 2008 - added translation control for embedded archetype
                Dim lang As String = Filemanager.Master.OntologyManager.LanguageCode
                If mFileManager.OntologyManager.HasLanguage(lang) Then
                    If mFileManager.OntologyManager.LanguageCode <> lang Then
                        mFileManager.OntologyManager.LanguageCode = lang
                        mArchetypeControl.Translate()
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub TranslateGUI()
        mIsLoading = True
        comboStructure.Items.Clear()

        If Not mValidStructureClasses Is Nothing Then
            For Each ValidStructure As StructureType In mValidStructureClasses
                comboStructure.Items.Add(Filemanager.GetOpenEhrTerm(ValidStructure, ValidStructure.ToString))
            Next
        End If

        lblStructure.Text = Filemanager.GetOpenEhrTerm(85, lblStructure.Text)

        If chkEmbedded.Visible Then
            chkEmbedded.Text = Filemanager.GetOpenEhrTerm(605, chkEmbedded.Text)
        End If

        mIsLoading = False
    End Sub

    Public Function SaveAsStructure() As RmStructure
        ' save as RmStructureCompound or RmSlot

        If mIsEmbedded Then
            If mEmbeddedSlot Is Nothing Then
                Return Nothing
            Else
                Return mEmbeddedSlot.RM_Class
            End If
        Else
            If mArchetypeControl Is Nothing Then
                Return Nothing
            Else
                If mIsElement Then
                    Return CType(mArchetypeControl, ElementOnly).Archetype
                Else
                    Return mArchetypeControl.Archetype
                End If
            End If
        End If
    End Function

    Public Sub toRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        If Not mArchetypeControl Is Nothing Then
            text.WriteLine(mArchetypeControl.ToRichText(level, Chr(13) & Chr(10)))
            text.WriteLine("\pard\f0\fs20\par")
        End If
    End Sub

    Public Sub toHTML(ByRef text As IO.StreamWriter, Optional ByVal BackGroundColour As String = "")
        If Not mArchetypeControl Is Nothing Then
            text.WriteLine(mArchetypeControl.ToHTML(BackGroundColour))
            text.WriteLine("<hr>")
        End If
    End Sub

    Public Sub Reset()
        mArchetypeControl.Reset()
    End Sub

    Public Sub ProcessStructure(ByVal an_element As RmElement)
        mIsLoading = True
        mIsElement = True
        panelEntry.Hide()
        panelStructure.Show()
        ArchetypeDisplay = New ElementOnly(an_element, mFileManager)

        'Set the initial value
        ArchetypeDisplay.SetInitial()

        mIsLoading = False
    End Sub

    Public Sub ProcessStructure(ByVal a_compound_structure As RmStructureCompound)
        mIsLoading = True

        If Not a_compound_structure.Children Is Nothing Then ' Not sure that it should be there
            panelEntry.Hide()
            panelStructure.Show()

            Select Case a_compound_structure.Type '.TypeName
                Case StructureType.Single ' "Single", "Simple"
                    ArchetypeDisplay = New SimpleStructure(a_compound_structure, mFileManager)

                Case StructureType.List
                    ' this also shows the panels and sets lvList to visible
                    ArchetypeDisplay = New ListStructure(a_compound_structure, mFileManager)

                Case StructureType.Tree
                    ArchetypeDisplay = New TreeStructure(a_compound_structure, mFileManager)

                Case StructureType.Table
                    ArchetypeDisplay = New TableStructure(CType(a_compound_structure, RmTable), mFileManager)

                Case StructureType.Cluster
                    ArchetypeDisplay = New TreeStructure(a_compound_structure, mFileManager)

                Case Else
                    Debug.Assert(False)
            End Select

            'Set the initial value
            ArchetypeDisplay.SetInitial()
        Else
            panelEntry.Show()
            panelStructure.Hide()
        End If

        mIsLoading = False
    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point, ByVal mandatory_only As Boolean)
        Dim spacer As Integer = 1

        'leftmargin = pos.X
        If aContainer.Name <> "tpInterface" Then
            aContainer.Size = New Size
        End If

        If Not mArchetypeControl Is Nothing AndAlso mArchetypeControl.HasData Then
            ArchetypeView.Instance.BuildInterface(mArchetypeControl.InterfaceBuilder, aContainer, pos, spacer, mandatory_only, mFileManager)
        End If
    End Sub

    Public Sub SetAsElement(ByVal a_node_id As String)
        mIsElement = True
        panelEntry.Hide()

        If mIsEmbedded Then
            If mIsLoading Then
                If mEmbeddedSlot Is Nothing Then
                    mEmbeddedSlot = New ArchetypeNodeAnonymous(StructureType.Element)
                End If
            Else
                ' have to have a new slot if change the structure
                mEmbeddedSlot = New ArchetypeNodeAnonymous(StructureType.Element)
            End If

            panelStructure.Show()
            panelDisplay.Hide()
            ShowDetailPanel(mEmbeddedSlot, New EventArgs)
        Else
            ' ensure the structure component is visible
            If Not panelDisplay.Visible Then
                panelDisplay.Show()
            End If

            Dim newElementOnly As New ElementOnly(New RmElement(mFileManager.Archetype.ConceptCode), mFileManager)
            ArchetypeDisplay = newElementOnly
            PanelDetails.Hide()
        End If

        panelStructure.Show()
    End Sub

    Public Sub SetAsCluster(ByVal a_node_id As String)
        mIsCluster = True
        panelEntry.Hide()

        If mIsEmbedded Then
            If mIsLoading Then
                If mEmbeddedSlot Is Nothing Then
                    mEmbeddedSlot = New ArchetypeNodeAnonymous(StructureType.Cluster)
                End If
            Else
                ' have to have a new slot if change the structure
                mEmbeddedSlot = New ArchetypeNodeAnonymous(StructureType.Cluster)
            End If

            panelStructure.Show()
            panelDisplay.Hide()
            ShowDetailPanel(mEmbeddedSlot, New EventArgs)
        Else
            ' ensure the structure component is visible
            If Not panelDisplay.Visible Then
                panelDisplay.Show()
            End If

            Dim newTreeStructure As New TreeStructure(New RmCluster(mFileManager.Archetype.ConceptCode), mFileManager)
            ArchetypeDisplay = newTreeStructure
            PanelDetails.Hide()
        End If

        panelStructure.Show()
    End Sub

    Private Sub comboStructure_selectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboStructure.SelectedIndexChanged
        Dim entry_structure As EntryStructure ' User control to provide the list or whatever
        Dim chosen_structure As StructureType
        Dim switchOffPanelDetails As Boolean = False

        Debug.Assert(mIsCluster = False)

        Debug.Assert(Not mValidStructureClasses Is Nothing)  ' This should be set to populate the list

        If comboStructure.SelectedIndex = -1 Then Return ' nothing selected

        chosen_structure = mValidStructureClasses(comboStructure.SelectedIndex)
        ReferenceModel.SetStructureClass(chosen_structure)

        If mIsEmbedded Then
            If mIsLoading Then
                If mEmbeddedSlot Is Nothing Then
                    mEmbeddedSlot = New ArchetypeNodeAnonymous(chosen_structure)
                End If
            Else
                ' have to have a new slot if change the structure
                mEmbeddedSlot = New ArchetypeNodeAnonymous(chosen_structure)
                
            End If
            panelStructure.Show()
            panelDisplay.Hide()
            ShowDetailPanel(mEmbeddedSlot, New EventArgs)
        Else

            panelStructure.SuspendLayout()
            panelEntry.SuspendLayout()
            panelDisplay.SuspendLayout()

            ' ensure the structure component is visible
            panelDisplay.Show()

            Select Case chosen_structure
                Case StructureType.Single
                    entry_structure = New SimpleStructure(mFileManager) ' inherits from EntryStructure
                Case StructureType.List
                    entry_structure = New ListStructure(mFileManager) ' inherits from EntryStructure
                Case StructureType.Tree
                    entry_structure = New TreeStructure(mFileManager) ' inherits from EntryStructure
                Case StructureType.Table
                    entry_structure = New TableStructure(mFileManager) ' inherits from EntryStructure
                Case Else
                    Debug.Assert(False)
                    Return
            End Select

            If mArchetypeControl Is Nothing Then
                ArchetypeDisplay = entry_structure

                'new structure so hide details
                'JAR: 17APR07, EDT-22 Adding constraint details when no object exists raises exception
                'Me.PanelDetails.Visible = False 'This is switched back on when PanelStructure.Visible = True
                switchOffPanelDetails = True

            Else
                'Changing structures
                If entry_structure.StructureType <> mArchetypeControl.StructureType Then
                    entry_structure.Archetype = mArchetypeControl.Archetype
                    ArchetypeDisplay = entry_structure
                End If
            End If

            'pane visibility
            panelStructure.Show()
            panelEntry.Hide()
            PanelDetails.Visible = Not switchOffPanelDetails 'JAR: 17APR07, EDT-22 Adding constraint details when no object exists raises exception

            panelStructure.ResumeLayout(True)
            panelEntry.ResumeLayout(True)
            panelDisplay.ResumeLayout(True)
        End If

        If Not mIsLoading Then
            mFileManager.FileEdited = True
            'SRH: Jan 11 2009 - EDT-486 - update structure
            RaiseEvent UpdateStructure(Me, chosen_structure)
        End If

    End Sub

#End Region

#Region "Features related to embedded archetype handling"

    Private ReadOnly Property ArchetypeAvailable() As Boolean
        Get
            Return mFileManager.ArchetypeAvailable
        End Get
    End Property

    Public Sub ProcessStructure(ByVal slot As RmSlot)
        mIsLoading = True
        mIsEmbedded = True
        mEmbeddedSlot = New ArchetypeNodeAnonymous(slot)

        Dim sl As Constraint_Slot = slot.SlotConstraint
        mFileManager = New FileManagerLocal
        PanelDetails.LocalFileManager = mFileManager
        mFileManager.ObjectToSave = Me

        mFileManager.FileLoading = True

        If OpenArchetypeForSlot(slot, OceanArchetypeEditor.Instance.Options.RepositoryPath & "\structure") Then
            mFileManager.FileLoading = False
            Filemanager.AddEmbedded(mFileManager)

            'Hide context menu to change structure
            If Not mArchetypeControl Is Nothing Then
                mArchetypeControl.ShowChangeStructureMenu = False
                Translate()
            End If
        Else
            mFileManager = Filemanager.Master
            chkEmbedded.Checked = True

            Select Case slot.SlotConstraint.RM_ClassType
                Case StructureType.Single
                    comboStructure.SelectedIndex = 0
                Case StructureType.List
                    comboStructure.SelectedIndex = 1
                Case StructureType.Tree
                    comboStructure.SelectedIndex = 2
                Case StructureType.Table
                    comboStructure.SelectedIndex = 3
            End Select

            Translate()
        End If

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

    Private Sub OpenArchetype(ByVal an_archetype_name As String)
        mFileManager.OpenArchetype(an_archetype_name)

        If mFileManager.ArchetypeAvailable Then
            Dim lbl As New Label
            lbl.Location = New System.Drawing.Point(120, 2)
            lbl.Width = 320
            lbl.Height = 24
            ProcessStructure(CType(mFileManager.Archetype.Definition, RmStructureCompound))
            lbl.Text = mFileManager.Archetype.Archetype_ID.ToString
            mArchetypeControl.PanelStructureHeader.Controls.Add(lbl)
            lbl.BringToFront()
        Else
            MessageBox.Show(AE_Constants.Instance.Error_loading & ": " & an_archetype_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub PrepareToSave()
        mFileManager.Archetype.Definition = mArchetypeControl.Archetype
    End Sub

    Private Sub chkEmbedded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEmbedded.CheckedChanged
        mIsEmbedded = chkEmbedded.Checked
        If Not mIsLoading Then
            'changed the embedded status
            ' if it is not loading and is changed to false then
            ' need to remove the filemanager
            'ToDo: needs to be more comprehensive if more than one embedded
            If mIsEmbedded AndAlso comboStructure.SelectedIndex = -1 Then
                comboStructure.Focus()
                comboStructure.DroppedDown = True
                Return
            Else
                If Filemanager.HasEmbedded AndAlso (Not Filemanager.Master Is mFileManager) Then
                    Filemanager.RemoveEmbedded(mFileManager)
                    mFileManager = Filemanager.Master
                End If
            End If

            comboStructure_selectedIndexChanged(sender, e)
        End If
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