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
    Private mEmbeddedSlot As RmSlot

    Private mIsState As Boolean
    Private mValidStructureClasses As StructureType()
    Private WithEvents mArchetypeControl As EntryStructure
    Private WithEvents mFileManager As FileManagerLocal
    Public Event StructureChanged(ByVal sender As Object, ByVal a_structure As StructureType)

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal aEditor As Designer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        If Not Me.DesignMode Then
            mFileManager = Filemanager.Instance
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
    Friend WithEvents PanelStructure As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
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
    Friend WithEvents PanelDetails As ArchetypeNodeConstraintControl
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents HelpProviderTabPageStructure As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabPageStructure))
        Me.PanelStructure = New System.Windows.Forms.Panel
        Me.panelDisplay = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelDetails = New ArchetypeNodeConstraintControl
        Me.Splitter2 = New System.Windows.Forms.Splitter
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.comboStructure = New System.Windows.Forms.ComboBox
        Me.ttElement = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTipSpecialisation = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProviderTabPageStructure = New System.Windows.Forms.HelpProvider
        Me.PanelStructure.SuspendLayout()
        Me.panelEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelStructure
        '
        Me.PanelStructure.Controls.Add(Me.panelDisplay)
        Me.PanelStructure.Controls.Add(Me.Splitter1)
        Me.PanelStructure.Controls.Add(Me.PanelDetails)
        Me.PanelStructure.Controls.Add(Me.Splitter2)
        Me.PanelStructure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelStructure.Location = New System.Drawing.Point(0, 40)
        Me.PanelStructure.Name = "PanelStructure"
        Me.PanelStructure.Size = New System.Drawing.Size(848, 368)
        Me.PanelStructure.TabIndex = 8
        Me.PanelStructure.Visible = False
        '
        'panelDisplay
        '
        Me.panelDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelDisplay.DockPadding.All = 2
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me.panelDisplay, System.Windows.Forms.HelpNavigator.Index)
        Me.panelDisplay.Location = New System.Drawing.Point(0, 0)
        Me.panelDisplay.Name = "panelDisplay"
        Me.HelpProviderTabPageStructure.SetShowHelp(Me.panelDisplay, True)
        Me.panelDisplay.Size = New System.Drawing.Size(429, 368)
        Me.panelDisplay.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(429, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(8, 368)
        Me.Splitter1.TabIndex = 34
        Me.Splitter1.TabStop = False
        '
        'PanelDetails
        '
        Me.PanelDetails.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelDetails.Location = New System.Drawing.Point(437, 0)
        Me.PanelDetails.Name = "PanelDetails"
        Me.PanelDetails.Size = New System.Drawing.Size(408, 368)
        Me.PanelDetails.TabIndex = 33
        Me.PanelDetails.Visible = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(845, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 368)
        Me.Splitter2.TabIndex = 23
        Me.Splitter2.TabStop = False
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
        Me.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.ilSmall.ImageSize = New System.Drawing.Size(20, 20)
        Me.ilSmall.ImageStream = CType(resources.GetObject("ilSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilSmall.TransparentColor = System.Drawing.Color.Transparent
        '
        'panelEntry
        '
        Me.panelEntry.BackColor = System.Drawing.Color.Transparent
        Me.panelEntry.Controls.Add(Me.Label2)
        Me.panelEntry.Controls.Add(Me.comboStructure)
        Me.panelEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelEntry.Location = New System.Drawing.Point(0, 0)
        Me.panelEntry.Name = "panelEntry"
        Me.panelEntry.Size = New System.Drawing.Size(848, 40)
        Me.panelEntry.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(0, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Structure:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'comboStructure
        '
        Me.HelpProviderTabPageStructure.SetHelpKeyword(Me.comboStructure, "HowTo/Edit data/set_structure.htm")
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me.comboStructure, System.Windows.Forms.HelpNavigator.Topic)
        Me.comboStructure.Location = New System.Drawing.Point(80, 8)
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
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.PanelStructure)
        Me.Controls.Add(Me.panelEntry)
        Me.HelpProviderTabPageStructure.SetHelpKeyword(Me, "Screens/data_screen.htm")
        Me.HelpProviderTabPageStructure.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.HelpProviderTabPageStructure.SetHelpString(Me, "")
        Me.Name = "TabPageStructure"
        Me.HelpProviderTabPageStructure.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(848, 408)
        Me.PanelStructure.ResumeLayout(False)
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
            Me.panelDisplay.Controls.Clear()
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
                        Return Filemanager.Instance.OntologyManager.GetOpenEHRTerm(105, "Single")
                    Case StructureType.List
                        Return Filemanager.Instance.OntologyManager.GetOpenEHRTerm(106, "List")
                    Case StructureType.Tree
                        Return Filemanager.Instance.OntologyManager.GetOpenEHRTerm(107, "Tree")
                    Case StructureType.Table
                        Return Filemanager.Instance.OntologyManager.GetOpenEHRTerm(108, "Structure")
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
        mValidStructureClasses = ReferenceModel.Instance.ValidStructureTypes
        For Each ValidStructure As StructureType In mValidStructureClasses
            Me.comboStructure.Items.Add(ValidStructure.ToString)
        Next
        Me.HelpProviderTabPageStructure.HelpNamespace = ArchetypeEditor.Instance.Options.HelpLocationPath
    End Sub

    Sub ShowDetailPanel(ByVal CurrentItem As ArchetypeNode, ByVal e As EventArgs) Handles mArchetypeControl.CurrentItemChanged
        If CurrentItem Is Nothing Then
            Me.PanelDetails.Visible = False
        Else
            PanelDetails.ShowConstraint(mArchetypeControl.StructureType, _
                     IsState, CurrentItem, mFileManager)
            If Not Me.PanelDetails.Visible Then
                Me.PanelDetails.Visible = True
            End If
        End If
    End Sub

    Public Sub Translate()

        If Not mArchetypeControl Is Nothing Then
            mArchetypeControl.Translate()
        End If

    End Sub

    Public Function SaveAsStructure() As RmStructure
        ' save as RmStructureCompound or RmSlot

        If mIsEmbedded Then
            'Fixme - save embedded archetype

            If mFileManager.FileEdited AndAlso MessageBox.Show(AE_Constants.Instance.Save_changes & ": " & mFileManager.Archetype.Archetype_ID.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                mFileManager.Archetype.Definition = mArchetypeControl.Archetype
                mFileManager.WriteArchetype()
            End If
            Return mEmbeddedSlot
        Else
            If mArchetypeControl Is Nothing Then
                Return Nothing
            Else
                Return mArchetypeControl.Archetype
            End If
        End If
    End Function

    Public Function toRichText(ByRef text As IO.StringWriter, ByVal level As Integer) As String

        If Not mArchetypeControl Is Nothing Then
            text.WriteLine(mArchetypeControl.ToRichText(level, Chr(13) & Chr(10)))
            text.WriteLine("\pard\f0\fs20\par")
        End If

    End Function

    Public Function toHTML(ByRef text As IO.StreamWriter, Optional ByVal BackGroundColour As String = "") As String
        If Not mArchetypeControl Is Nothing Then
            text.WriteLine(mArchetypeControl.ToHTML(BackGroundColour))
            text.WriteLine("<hr>")
        End If
    End Function

    Public Sub Reset()
        mArchetypeControl.Reset()
    End Sub

    Public Sub ProcessStructure(ByVal a_compound_structure As RmStructureCompound)

        If Not a_compound_structure.Children Is Nothing Then ' Not sure that it should be there

            Me.panelEntry.Visible = False
            Me.PanelStructure.Visible = True

            Select Case a_compound_structure.Type '.TypeName
                Case StructureType.Single ' "Single", "Simple"

                    Me.ArchetypeDisplay = New SimpleStructure(a_compound_structure, mFileManager)

                Case StructureType.List ' "list", "List", "LIST"

                    ' this also shows the panels and sets lvList to visible
                    Me.ArchetypeDisplay = New ListStructure(a_compound_structure, mFileManager)

                Case StructureType.Tree ' "tree", "Tree", "TREE"

                    Me.ArchetypeDisplay = New TreeStructure(a_compound_structure, mFileManager)

                Case StructureType.Table ' "table", "Table", "TABLE"

                    Me.ArchetypeDisplay = New TableStructure(CType(a_compound_structure, RmTable), mFileManager)

                Case Else
                    Debug.Assert(False)
            End Select
        Else
            Me.panelEntry.Visible = True
            Me.PanelStructure.Visible = False
        End If
    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point)
        Dim spacer As Integer = 1

        'leftmargin = pos.X
        If aContainer.Name <> "tpInterface" Then
            aContainer.Size = New Size
        End If
        If Not mArchetypeControl Is Nothing Then
            ArchetypeView.Instance.BuildInterface(mArchetypeControl.InterfaceBuilder, aContainer, pos, spacer)
        End If
    End Sub

    Private Sub comboStructure_selectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboStructure.SelectedIndexChanged
        ' make the context menu for the design panel relevant

        Dim entry_structure As EntryStructure ' User control to provide the list or whatever
        Dim chosen_structure As StructureType

        Debug.Assert(Not mValidStructureClasses Is Nothing)  ' This should be set to populate the list

        If Me.comboStructure.SelectedIndex = -1 Then Return ' nothing selected

        chosen_structure = mValidStructureClasses(Me.comboStructure.SelectedIndex)
        ReferenceModel.Instance.StructureClass = chosen_structure

        Select Case chosen_structure
            Case StructureType.Single
                entry_structure = New SimpleStructure(mFileManager) ' inherits from EntryStructure
            Case StructureType.List
                entry_structure = New ListStructure(mFileManager) ' inherits from EntryStructure
            Case StructureType.Tree
                entry_structure = New TreeStructure(mFileManager) ' inherits from EntryStructure
            Case StructureType.Table
                entry_structure = New TableStructure(mFileManager) ' inherits from EntryStructure
        End Select

        If mArchetypeControl Is Nothing Then
            Me.ArchetypeDisplay = entry_structure
        Else
            'Changing structures
            entry_structure.Archetype = mArchetypeControl.Archetype
            Me.ArchetypeDisplay = entry_structure
        End If
        Me.PanelStructure.Visible = True
        Me.panelEntry.Visible = False

        mFileManager.FileEdited = True
        RaiseEvent StructureChanged(Me, chosen_structure)

    End Sub

#End Region

#Region "Features related to embedded archetype handling"

    Private ReadOnly Property ArchetypeAvailable() As Boolean
        Get
            Return mFileManager.ArchetypeAvailable
        End Get
    End Property

    Public Sub ProcessStructure(ByVal a_slot As RmSlot)
        mFileManager = New FileManagerLocal
        mIsEmbedded = True
        mEmbeddedSlot = a_slot
        OpenArchetype(a_slot)
    End Sub

    Private Overloads Sub OpenArchetype(ByVal an_archetype_ID As ArchetypeID)
        mFileManager.OpenArchetype(an_archetype_ID.ToString)
    End Sub

    Private Overloads Sub OpenArchetype(ByVal a_slot As RmSlot)
        Dim archetype_name As String

        archetype_name = ReferenceModel.Instance.ReferenceModelName & "-" & a_slot.SlotConstraint.RM_ClassType.ToString & "." & a_slot.SlotConstraint.Include.Item(0) & ".adl"
        OpenArchetype(ArchetypeEditor.Instance.Options.RepositoryPath & "\Structure\" & archetype_name)

    End Sub

    Private Overloads Sub openArchetype(ByVal an_archetype_name As String)
        mFileManager.OpenArchetype(an_archetype_name)
      
        If mFileManager.ArchetypeAvailable Then
            Dim lbl As New Label
            lbl.Location = New System.Drawing.Point(3, 20)
            lbl.Width = 400
            lbl.Height = 24
            Me.ProcessStructure(CType(mFileManager.Archetype.Definition, RmStructureCompound))
            lbl.Text = mFileManager.Archetype.Archetype_ID.ToString
            mArchetypeControl.PanelStructureHeader.Controls.Add(lbl)
            mArchetypeControl.PanelStructureHeader.Height = 40
        Else
            MessageBox.Show(AE_Constants.Instance.Error_loading & ": " & an_archetype_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'FixMe - offer to find file
        End If
    End Sub

    Private Sub SaveArchetype()
        mFileManager.WriteArchetype()
    End Sub

    Private Sub FileEditedChanged(ByVal sender As Object, ByVal e As FileManagerEventArgs) Handles mFileManager.IsFileDirtyChanged
        If mIsEmbedded Then
            Filemanager.Instance.FileEdited = True
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