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
'	file:        "$Source: source/vb.net/archetype_editor/Forms/SCCS/s.Designer.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

'Option Strict On

Public Class Designer
    Inherits System.Windows.Forms.Form

    Private WithEvents mAutoSaveTimer As New Timer()
    Private mComponentsCollection As New Collection
    Private mTabPagesCollection As New Collections.Hashtable
    Private mBaseTabPagesCollection As New Collection
    Private mTabPageDataEventSeries As TabpageHistory
    Private mTabPageStateEventSeries As TabpageHistory
    Private WithEvents mTabPageDataStructure As TabPageStructure
    Private mTabPageDataStateStructure As TabPageStructure
    Private mTabPageProtocolStructure As TabPageStructure
    Private mTabPageParticipation As TabPageParticipation
    Private WithEvents mTabPageStateStructure As TabPageStructure
    Private WithEvents mTabPageInstruction As TabPageInstruction
    Private WithEvents mTabPageAction As TabPageAction
    Private mTabPageSection As TabPageSection
    Private mRestrictedSubject As RestrictedSet
    Private mTabPageComposition As TabPageComposition
    Private mDataViewTermBindings As DataView
    Private mDataViewConstraintBindings As DataView
    Private mDataViewTerminologies As DataView
    Private mFindString As String = ""
    Private mFindStringFrom As Integer = -1
    Private mFileManager As FileManagerLocal    
    Friend WithEvents mRichTextArchetype As ArchetypeEditor.Specialised_VB_Classes.RichTextBoxPrintable
    Friend WithEvents mTermBindingPanel As TermBindingPanel
    Friend WithEvents menuFileExport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuFileExportType As System.Windows.Forms.MenuItem
    Friend WithEvents butADL As System.Windows.Forms.ToolBarButton
    Friend WithEvents butXML As System.Windows.Forms.ToolBarButton
    Friend WithEvents butOWL As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblConstraintStatements As System.Windows.Forms.Label
    Friend WithEvents panelConstraintStatementTop As System.Windows.Forms.Panel
    Friend WithEvents DataGridConstraintStatements As System.Windows.Forms.DataGridView
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents terminology As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents release As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PanelDescription As System.Windows.Forms.Panel
    Friend WithEvents RichTextBoxDescription As System.Windows.Forms.RichTextBox
    Friend WithEvents tabComment As System.Windows.Forms.TabControl
    Friend WithEvents tpConceptDescription As System.Windows.Forms.TabPage
    Friend WithEvents tpConceptComment As System.Windows.Forms.TabPage
    Friend WithEvents txtConceptComment As System.Windows.Forms.TextBox
    Friend WithEvents MenuFileOpenFromWeb As System.Windows.Forms.MenuItem
    Friend WithEvents ToolBarOpenFromWeb As System.Windows.Forms.ToolBarButton
    Friend WithEvents cbParticipation As System.Windows.Forms.CheckBox
    Friend WithEvents tpParticipation As Crownwood.Magic.Controls.TabPage
    Friend WithEvents PanelConcept_1 As System.Windows.Forms.Panel
    Friend WithEvents gbSpecialisation As System.Windows.Forms.GroupBox
    Friend WithEvents tvSpecialisation As System.Windows.Forms.TreeView
    Friend WithEvents butLinks As System.Windows.Forms.Button
    Friend WithEvents mTabPageDescription As TabPageDescription

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents chkEventSeries As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridDefinitions As System.Windows.Forms.DataGrid
    Friend WithEvents butAdd As System.Windows.Forms.Button
    Friend WithEvents DefinitionTableStyle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DesignerColumnLabel As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DesignerColumnDefinition As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridConstraintDefinitions As System.Windows.Forms.DataGrid
    Friend WithEvents OpenFileDialogArchetype As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtConceptInFull As System.Windows.Forms.TextBox
    Friend WithEvents TxtConceptDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblPrimaryLanguage As System.Windows.Forms.Label
    Friend WithEvents MenuFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuFileSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuFile As System.Windows.Forms.MenuItem
    Friend WithEvents MenuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents PanelMain As System.Windows.Forms.Panel
    Friend WithEvents PanelHeader As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents MenuFileClose As System.Windows.Forms.MenuItem
    Friend WithEvents cbPersonState As System.Windows.Forms.CheckBox
    Friend WithEvents PanelRoot As System.Windows.Forms.Panel
    Friend WithEvents cbProtocol As System.Windows.Forms.CheckBox
    Friend WithEvents PanelConfigStructure As System.Windows.Forms.Panel
    Friend WithEvents cbStructurePersonState As System.Windows.Forms.CheckBox
    Friend WithEvents TabMain As Crownwood.Magic.Controls.TabControl
    Friend WithEvents TabTerminology As Crownwood.Magic.Controls.TabControl
    Friend WithEvents TabDesign As Crownwood.Magic.Controls.TabControl
    Friend WithEvents TabStructure As Crownwood.Magic.Controls.TabControl
    Friend WithEvents tpHeader As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpDesign As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpTerminology As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpData As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpConstraints As Crownwood.Magic.Controls.TabPage
    Friend WithEvents panelLanguages As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblArchetypeName As System.Windows.Forms.Label
    Friend WithEvents tpRootState As Crownwood.Magic.Controls.TabPage
    Friend WithEvents PanelState As System.Windows.Forms.Panel
    Friend WithEvents TabState As Crownwood.Magic.Controls.TabControl
    Friend WithEvents tpRootStateStructure As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpRootStateEventSeries As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpDataStructure As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpText As Crownwood.Magic.Controls.TabPage
    Friend WithEvents ToolBarOpen As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarMain As System.Windows.Forms.ToolBar
    Friend WithEvents ImageListToolbar As System.Windows.Forms.ImageList
    Friend WithEvents ToolBarSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarSeparator1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarNew As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuPublish As System.Windows.Forms.MenuItem
    Friend WithEvents MenuFileSpecialise As System.Windows.Forms.MenuItem
    Friend WithEvents PanelConstraintBinding As System.Windows.Forms.Panel
    Friend WithEvents tpSectionPage As Crownwood.Magic.Controls.TabPage
    Friend WithEvents MenuFileNew As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents PanelConstraintDefTop As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tpTerms As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpLanguages As Crownwood.Magic.Controls.TabPage
    Friend WithEvents ListLanguages As System.Windows.Forms.ListBox
    Friend WithEvents lblAvailableLanguages As System.Windows.Forms.Label
    Friend WithEvents butAddTerminology As System.Windows.Forms.Button
    Friend WithEvents DataGridTerminologies As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle3 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents DataGridTableStyle4 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MenuLanguageAvailable As System.Windows.Forms.MenuItem
    Friend WithEvents MenuLanguageAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuLanguage As System.Windows.Forms.MenuItem
    Friend WithEvents MenuLanguageChange As System.Windows.Forms.MenuItem
    Friend WithEvents tpInterface As Crownwood.Magic.Controls.TabPage
    Friend WithEvents lblLifecycle As System.Windows.Forms.Label
    Friend WithEvents MenuTerminologyAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuTerminologyAvailable As System.Windows.Forms.MenuItem
    Friend WithEvents lblConcept As System.Windows.Forms.Label
    Friend WithEvents lblArchetypeFileName As System.Windows.Forms.Label
    Friend WithEvents lblPrimaryLanguageText As System.Windows.Forms.Label
    Friend WithEvents MenuHelpLicence As System.Windows.Forms.MenuItem
    Friend WithEvents MenuHelpOcean As System.Windows.Forms.MenuItem
    Friend WithEvents MenuHelpStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuPublishPack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuPublishFinalise As System.Windows.Forms.MenuItem
    Friend WithEvents MenuTerminology As System.Windows.Forms.MenuItem
    Friend WithEvents MenuHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuHelpOceanEditor As System.Windows.Forms.MenuItem
    Friend WithEvents PanelConcept As System.Windows.Forms.Panel
    Friend WithEvents lblAvailableTerminologies As System.Windows.Forms.Label
    Friend WithEvents tpBindings As Crownwood.Magic.Controls.TabPage
    Friend WithEvents PanelTermDefinitions As System.Windows.Forms.Panel
    Friend WithEvents MenuFileSaveAs As System.Windows.Forms.MenuItem
    Friend WithEvents MenuHelpReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuViewConfig As System.Windows.Forms.MenuItem
    Friend WithEvents HelpProviderDesigner As System.Windows.Forms.HelpProvider
    Friend WithEvents panelDiplayTop As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuDisplay As System.Windows.Forms.ContextMenu
    Friend WithEvents menuDisplayPrint As System.Windows.Forms.MenuItem
    Friend WithEvents menuDisplaySaveAs As System.Windows.Forms.MenuItem
    Friend WithEvents menuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents menuEditArchID As System.Windows.Forms.MenuItem
    Friend WithEvents MenuDisplayFind As System.Windows.Forms.MenuItem
    Friend WithEvents MenuDisplayFindAgain As System.Windows.Forms.MenuItem
    Friend WithEvents tpDescription As Crownwood.Magic.Controls.TabPage
    Friend WithEvents DataGridTableStyle5 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ToolBarRTF As System.Windows.Forms.ToolBar
    Friend WithEvents butRTF As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbSep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbSep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents butHTML1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents butSaveFile As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents butPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents cbMandatory As System.Windows.Forms.CheckBox
    Friend WithEvents menuFileNewWindow As System.Windows.Forms.MenuItem
    Friend WithEvents butLookUpConstraint As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Designer))
        Me.TxtConceptDescription = New System.Windows.Forms.TextBox
        Me.lblConcept = New System.Windows.Forms.Label
        Me.txtConceptInFull = New System.Windows.Forms.TextBox
        Me.PanelConcept = New System.Windows.Forms.Panel
        Me.tabComment = New System.Windows.Forms.TabControl
        Me.tpConceptDescription = New System.Windows.Forms.TabPage
        Me.tpConceptComment = New System.Windows.Forms.TabPage
        Me.txtConceptComment = New System.Windows.Forms.TextBox
        Me.PanelConfigStructure = New System.Windows.Forms.Panel
        Me.cbStructurePersonState = New System.Windows.Forms.CheckBox
        Me.chkEventSeries = New System.Windows.Forms.CheckBox
        Me.PanelRoot = New System.Windows.Forms.Panel
        Me.cbParticipation = New System.Windows.Forms.CheckBox
        Me.cbPersonState = New System.Windows.Forms.CheckBox
        Me.cbProtocol = New System.Windows.Forms.CheckBox
        Me.DataGridConstraintDefinitions = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridDefinitions = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.butAdd = New System.Windows.Forms.Button
        Me.DefinitionTableStyle = New System.Windows.Forms.DataGridTableStyle
        Me.DesignerColumnLabel = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DesignerColumnDefinition = New System.Windows.Forms.DataGridTextBoxColumn
        Me.OpenFileDialogArchetype = New System.Windows.Forms.OpenFileDialog
        Me.lblArchetypeFileName = New System.Windows.Forms.Label
        Me.lblPrimaryLanguage = New System.Windows.Forms.Label
        Me.lblPrimaryLanguageText = New System.Windows.Forms.Label
        Me.MainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuFile = New System.Windows.Forms.MenuItem
        Me.MenuFileOpen = New System.Windows.Forms.MenuItem
        Me.MenuFileOpenFromWeb = New System.Windows.Forms.MenuItem
        Me.MenuFileNew = New System.Windows.Forms.MenuItem
        Me.menuFileNewWindow = New System.Windows.Forms.MenuItem
        Me.MenuFileSave = New System.Windows.Forms.MenuItem
        Me.MenuFileSaveAs = New System.Windows.Forms.MenuItem
        Me.menuFileExport = New System.Windows.Forms.MenuItem
        Me.MenuFileExportType = New System.Windows.Forms.MenuItem
        Me.MenuFileClose = New System.Windows.Forms.MenuItem
        Me.MenuFileSpecialise = New System.Windows.Forms.MenuItem
        Me.MenuFileExit = New System.Windows.Forms.MenuItem
        Me.menuEdit = New System.Windows.Forms.MenuItem
        Me.menuEditArchID = New System.Windows.Forms.MenuItem
        Me.MenuViewConfig = New System.Windows.Forms.MenuItem
        Me.MenuPublish = New System.Windows.Forms.MenuItem
        Me.MenuPublishPack = New System.Windows.Forms.MenuItem
        Me.MenuPublishFinalise = New System.Windows.Forms.MenuItem
        Me.MenuLanguage = New System.Windows.Forms.MenuItem
        Me.MenuLanguageAvailable = New System.Windows.Forms.MenuItem
        Me.MenuLanguageAdd = New System.Windows.Forms.MenuItem
        Me.MenuLanguageChange = New System.Windows.Forms.MenuItem
        Me.MenuTerminology = New System.Windows.Forms.MenuItem
        Me.MenuTerminologyAvailable = New System.Windows.Forms.MenuItem
        Me.MenuTerminologyAdd = New System.Windows.Forms.MenuItem
        Me.MenuHelp = New System.Windows.Forms.MenuItem
        Me.MenuHelpStart = New System.Windows.Forms.MenuItem
        Me.MenuHelpReport = New System.Windows.Forms.MenuItem
        Me.MenuHelpLicence = New System.Windows.Forms.MenuItem
        Me.MenuHelpOcean = New System.Windows.Forms.MenuItem
        Me.MenuHelpOceanEditor = New System.Windows.Forms.MenuItem
        Me.PanelMain = New System.Windows.Forms.Panel
        Me.TabMain = New Crownwood.Magic.Controls.TabControl
        Me.tpHeader = New Crownwood.Magic.Controls.TabPage
        Me.PanelDescription = New System.Windows.Forms.Panel
        Me.RichTextBoxDescription = New System.Windows.Forms.RichTextBox
        Me.PanelConcept_1 = New System.Windows.Forms.Panel
        Me.gbSpecialisation = New System.Windows.Forms.GroupBox
        Me.tvSpecialisation = New System.Windows.Forms.TreeView
        Me.tpDesign = New Crownwood.Magic.Controls.TabPage
        Me.TabDesign = New Crownwood.Magic.Controls.TabControl
        Me.tpData = New Crownwood.Magic.Controls.TabPage
        Me.TabStructure = New Crownwood.Magic.Controls.TabControl
        Me.tpDataStructure = New Crownwood.Magic.Controls.TabPage
        Me.tpRootState = New Crownwood.Magic.Controls.TabPage
        Me.TabState = New Crownwood.Magic.Controls.TabControl
        Me.tpRootStateStructure = New Crownwood.Magic.Controls.TabPage
        Me.tpRootStateEventSeries = New Crownwood.Magic.Controls.TabPage
        Me.PanelState = New System.Windows.Forms.Panel
        Me.tpParticipation = New Crownwood.Magic.Controls.TabPage
        Me.tpSectionPage = New Crownwood.Magic.Controls.TabPage
        Me.tpTerminology = New Crownwood.Magic.Controls.TabPage
        Me.TabTerminology = New Crownwood.Magic.Controls.TabControl
        Me.tpTerms = New Crownwood.Magic.Controls.TabPage
        Me.PanelTermDefinitions = New System.Windows.Forms.Panel
        Me.tpBindings = New Crownwood.Magic.Controls.TabPage
        Me.tpConstraints = New Crownwood.Magic.Controls.TabPage
        Me.DataGridConstraintStatements = New System.Windows.Forms.DataGridView
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.terminology = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.release = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.panelConstraintStatementTop = New System.Windows.Forms.Panel
        Me.lblConstraintStatements = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelConstraintDefTop = New System.Windows.Forms.Panel
        Me.PanelConstraintBinding = New System.Windows.Forms.Panel
        Me.butLookUpConstraint = New System.Windows.Forms.Button
        Me.tpLanguages = New Crownwood.Magic.Controls.TabPage
        Me.DataGridTerminologies = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle3 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTableStyle4 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTableStyle5 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.butAddTerminology = New System.Windows.Forms.Button
        Me.lblAvailableTerminologies = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.panelLanguages = New System.Windows.Forms.Panel
        Me.ListLanguages = New System.Windows.Forms.ListBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblAvailableLanguages = New System.Windows.Forms.Label
        Me.tpText = New Crownwood.Magic.Controls.TabPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.panelDiplayTop = New System.Windows.Forms.Panel
        Me.ToolBarRTF = New System.Windows.Forms.ToolBar
        Me.tbSep1 = New System.Windows.Forms.ToolBarButton
        Me.butRTF = New System.Windows.Forms.ToolBarButton
        Me.butADL = New System.Windows.Forms.ToolBarButton
        Me.butXML = New System.Windows.Forms.ToolBarButton
        Me.butOWL = New System.Windows.Forms.ToolBarButton
        Me.tbSep2 = New System.Windows.Forms.ToolBarButton
        Me.butHTML1 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton
        Me.butSaveFile = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton
        Me.butPrint = New System.Windows.Forms.ToolBarButton
        Me.ImageListToolbar = New System.Windows.Forms.ImageList(Me.components)
        Me.tpInterface = New Crownwood.Magic.Controls.TabPage
        Me.cbMandatory = New System.Windows.Forms.CheckBox
        Me.tpDescription = New Crownwood.Magic.Controls.TabPage
        Me.ContextMenuDisplay = New System.Windows.Forms.ContextMenu
        Me.menuDisplayPrint = New System.Windows.Forms.MenuItem
        Me.MenuDisplayFind = New System.Windows.Forms.MenuItem
        Me.MenuDisplayFindAgain = New System.Windows.Forms.MenuItem
        Me.menuDisplaySaveAs = New System.Windows.Forms.MenuItem
        Me.PanelHeader = New System.Windows.Forms.Panel
        Me.lblArchetypeName = New System.Windows.Forms.Label
        Me.lblLifecycle = New System.Windows.Forms.Label
        Me.ToolBarMain = New System.Windows.Forms.ToolBar
        Me.ToolBarNew = New System.Windows.Forms.ToolBarButton
        Me.ToolBarOpen = New System.Windows.Forms.ToolBarButton
        Me.ToolBarOpenFromWeb = New System.Windows.Forms.ToolBarButton
        Me.ToolBarSave = New System.Windows.Forms.ToolBarButton
        Me.ToolBarSeparator1 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarPrint = New System.Windows.Forms.ToolBarButton
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProviderDesigner = New System.Windows.Forms.HelpProvider
        Me.butLinks = New System.Windows.Forms.Button
        Me.PanelConcept.SuspendLayout()
        Me.tabComment.SuspendLayout()
        Me.tpConceptDescription.SuspendLayout()
        Me.tpConceptComment.SuspendLayout()
        Me.PanelConfigStructure.SuspendLayout()
        Me.PanelRoot.SuspendLayout()
        CType(Me.DataGridConstraintDefinitions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridDefinitions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelMain.SuspendLayout()
        Me.tpHeader.SuspendLayout()
        Me.PanelDescription.SuspendLayout()
        Me.PanelConcept_1.SuspendLayout()
        Me.gbSpecialisation.SuspendLayout()
        Me.tpDesign.SuspendLayout()
        Me.tpData.SuspendLayout()
        Me.tpRootState.SuspendLayout()
        Me.tpTerminology.SuspendLayout()
        Me.tpTerms.SuspendLayout()
        Me.tpConstraints.SuspendLayout()
        CType(Me.DataGridConstraintStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelConstraintStatementTop.SuspendLayout()
        Me.PanelConstraintBinding.SuspendLayout()
        Me.tpLanguages.SuspendLayout()
        CType(Me.DataGridTerminologies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.panelLanguages.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tpText.SuspendLayout()
        Me.panelDiplayTop.SuspendLayout()
        Me.tpInterface.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtConceptDescription
        '
        Me.TxtConceptDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtConceptDescription.Location = New System.Drawing.Point(3, 3)
        Me.TxtConceptDescription.Multiline = True
        Me.TxtConceptDescription.Name = "TxtConceptDescription"
        Me.TxtConceptDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtConceptDescription.Size = New System.Drawing.Size(514, 71)
        Me.TxtConceptDescription.TabIndex = 1
        Me.TxtConceptDescription.Tag = ""
        '
        'lblConcept
        '
        Me.lblConcept.Location = New System.Drawing.Point(13, 12)
        Me.lblConcept.Name = "lblConcept"
        Me.lblConcept.Size = New System.Drawing.Size(67, 21)
        Me.lblConcept.TabIndex = 8
        Me.lblConcept.Text = "Concept:"
        Me.lblConcept.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtConceptInFull
        '
        Me.txtConceptInFull.Location = New System.Drawing.Point(90, 12)
        Me.txtConceptInFull.Name = "txtConceptInFull"
        Me.txtConceptInFull.Size = New System.Drawing.Size(325, 24)
        Me.txtConceptInFull.TabIndex = 0
        Me.txtConceptInFull.Tag = ""
        '
        'PanelConcept
        '
        Me.PanelConcept.BackColor = System.Drawing.Color.LightYellow
        Me.PanelConcept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelConcept.Controls.Add(Me.butLinks)
        Me.PanelConcept.Controls.Add(Me.tabComment)
        Me.PanelConcept.Controls.Add(Me.lblConcept)
        Me.PanelConcept.Controls.Add(Me.txtConceptInFull)
        Me.PanelConcept.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelConcept.Location = New System.Drawing.Point(0, 0)
        Me.PanelConcept.Name = "PanelConcept"
        Me.PanelConcept.Size = New System.Drawing.Size(969, 111)
        Me.PanelConcept.TabIndex = 3
        '
        'tabComment
        '
        Me.tabComment.Controls.Add(Me.tpConceptDescription)
        Me.tabComment.Controls.Add(Me.tpConceptComment)
        Me.tabComment.Dock = System.Windows.Forms.DockStyle.Right
        Me.tabComment.HotTrack = True
        Me.tabComment.Location = New System.Drawing.Point(437, 0)
        Me.tabComment.Multiline = True
        Me.tabComment.Name = "tabComment"
        Me.tabComment.SelectedIndex = 0
        Me.tabComment.Size = New System.Drawing.Size(528, 107)
        Me.tabComment.TabIndex = 10
        '
        'tpConceptDescription
        '
        Me.tpConceptDescription.BackColor = System.Drawing.Color.LightYellow
        Me.tpConceptDescription.Controls.Add(Me.TxtConceptDescription)
        Me.tpConceptDescription.Location = New System.Drawing.Point(4, 26)
        Me.tpConceptDescription.Name = "tpConceptDescription"
        Me.tpConceptDescription.Padding = New System.Windows.Forms.Padding(3)
        Me.tpConceptDescription.Size = New System.Drawing.Size(520, 77)
        Me.tpConceptDescription.TabIndex = 0
        Me.tpConceptDescription.Text = "Description"
        Me.tpConceptDescription.UseVisualStyleBackColor = True
        '
        'tpConceptComment
        '
        Me.tpConceptComment.BackColor = System.Drawing.Color.LightYellow
        Me.tpConceptComment.Controls.Add(Me.txtConceptComment)
        Me.tpConceptComment.Location = New System.Drawing.Point(4, 26)
        Me.tpConceptComment.Name = "tpConceptComment"
        Me.tpConceptComment.Padding = New System.Windows.Forms.Padding(3)
        Me.tpConceptComment.Size = New System.Drawing.Size(520, 77)
        Me.tpConceptComment.TabIndex = 1
        Me.tpConceptComment.Text = "Comment"
        Me.tpConceptComment.UseVisualStyleBackColor = True
        '
        'txtConceptComment
        '
        Me.txtConceptComment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtConceptComment.Location = New System.Drawing.Point(3, 3)
        Me.txtConceptComment.Multiline = True
        Me.txtConceptComment.Name = "txtConceptComment"
        Me.txtConceptComment.Size = New System.Drawing.Size(514, 71)
        Me.txtConceptComment.TabIndex = 0
        '
        'PanelConfigStructure
        '
        Me.PanelConfigStructure.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.PanelConfigStructure.Controls.Add(Me.cbStructurePersonState)
        Me.PanelConfigStructure.Controls.Add(Me.chkEventSeries)
        Me.PanelConfigStructure.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelConfigStructure.Location = New System.Drawing.Point(0, 0)
        Me.PanelConfigStructure.Name = "PanelConfigStructure"
        Me.PanelConfigStructure.Padding = New System.Windows.Forms.Padding(1)
        Me.PanelConfigStructure.Size = New System.Drawing.Size(969, 28)
        Me.PanelConfigStructure.TabIndex = 9
        '
        'cbStructurePersonState
        '
        Me.cbStructurePersonState.Location = New System.Drawing.Point(288, 5)
        Me.cbStructurePersonState.Name = "cbStructurePersonState"
        Me.cbStructurePersonState.Size = New System.Drawing.Size(230, 23)
        Me.cbStructurePersonState.TabIndex = 31
        Me.cbStructurePersonState.Text = "Person State"
        Me.ToolTip1.SetToolTip(Me.cbStructurePersonState, "Information about the person that influences the interpretation")
        '
        'chkEventSeries
        '
        Me.chkEventSeries.Enabled = False
        Me.chkEventSeries.Location = New System.Drawing.Point(46, 0)
        Me.chkEventSeries.Name = "chkEventSeries"
        Me.chkEventSeries.Size = New System.Drawing.Size(236, 28)
        Me.chkEventSeries.TabIndex = 8
        Me.chkEventSeries.Text = "Data: Event Series"
        Me.ToolTip1.SetToolTip(Me.chkEventSeries, "Repeated measurements in same series")
        '
        'PanelRoot
        '
        Me.PanelRoot.BackColor = System.Drawing.Color.LightYellow
        Me.PanelRoot.Controls.Add(Me.cbParticipation)
        Me.PanelRoot.Controls.Add(Me.cbPersonState)
        Me.PanelRoot.Controls.Add(Me.cbProtocol)
        Me.PanelRoot.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelRoot.Location = New System.Drawing.Point(0, 0)
        Me.PanelRoot.Name = "PanelRoot"
        Me.PanelRoot.Size = New System.Drawing.Size(969, 37)
        Me.PanelRoot.TabIndex = 11
        '
        'cbParticipation
        '
        Me.cbParticipation.Location = New System.Drawing.Point(288, 6)
        Me.cbParticipation.Name = "cbParticipation"
        Me.cbParticipation.Size = New System.Drawing.Size(154, 28)
        Me.cbParticipation.TabIndex = 30
        Me.cbParticipation.Text = "Participation"
        Me.ToolTip1.SetToolTip(Me.cbParticipation, "About who participated in what has been recorded")
        '
        'cbPersonState
        '
        Me.cbPersonState.Enabled = False
        Me.cbPersonState.Location = New System.Drawing.Point(527, 10)
        Me.cbPersonState.Name = "cbPersonState"
        Me.cbPersonState.Size = New System.Drawing.Size(228, 19)
        Me.cbPersonState.TabIndex = 31
        Me.cbPersonState.Text = "Person State with EventSeries"
        Me.ToolTip1.SetToolTip(Me.cbPersonState, "Only for situations where 'state' information requires a EventSeries event")
        '
        'cbProtocol
        '
        Me.cbProtocol.Location = New System.Drawing.Point(72, 10)
        Me.cbProtocol.Name = "cbProtocol"
        Me.cbProtocol.Size = New System.Drawing.Size(154, 19)
        Me.cbProtocol.TabIndex = 29
        Me.cbProtocol.Text = "Protocol"
        Me.ToolTip1.SetToolTip(Me.cbProtocol, "About HOW the information was collected")
        '
        'DataGridConstraintDefinitions
        '
        Me.DataGridConstraintDefinitions.AllowNavigation = False
        Me.DataGridConstraintDefinitions.CaptionBackColor = System.Drawing.Color.CornflowerBlue
        Me.DataGridConstraintDefinitions.CaptionText = "Constraint definitions"
        Me.DataGridConstraintDefinitions.DataMember = ""
        Me.DataGridConstraintDefinitions.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGridConstraintDefinitions.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridConstraintDefinitions.Location = New System.Drawing.Point(0, 8)
        Me.DataGridConstraintDefinitions.Name = "DataGridConstraintDefinitions"
        Me.DataGridConstraintDefinitions.RowHeaderWidth = 20
        Me.DataGridConstraintDefinitions.Size = New System.Drawing.Size(969, 295)
        Me.DataGridConstraintDefinitions.TabIndex = 4
        Me.DataGridConstraintDefinitions.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.DataGridConstraintDefinitions
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Code"
        Me.DataGridTextBoxColumn6.ReadOnly = True
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Text"
        Me.DataGridTextBoxColumn7.Width = 385
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Description"
        Me.DataGridTextBoxColumn8.Width = 450
        '
        'DataGridDefinitions
        '
        Me.DataGridDefinitions.CaptionBackColor = System.Drawing.Color.CornflowerBlue
        Me.DataGridDefinitions.CaptionText = "Term definitions"
        Me.DataGridDefinitions.DataMember = ""
        Me.DataGridDefinitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridDefinitions.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridDefinitions.Location = New System.Drawing.Point(0, 18)
        Me.DataGridDefinitions.Name = "DataGridDefinitions"
        Me.DataGridDefinitions.RowHeaderWidth = 25
        Me.DataGridDefinitions.Size = New System.Drawing.Size(969, 551)
        Me.DataGridDefinitions.TabIndex = 1
        Me.DataGridDefinitions.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGridDefinitions
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.RowHeaderWidth = 25
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Code"
        Me.DataGridTextBoxColumn2.ReadOnly = True
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Text"
        Me.DataGridTextBoxColumn3.Width = 200
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Description"
        Me.DataGridTextBoxColumn4.Width = 450
        '
        'butAdd
        '
        Me.butAdd.Image = CType(resources.GetObject("butAdd.Image"), System.Drawing.Image)
        Me.butAdd.Location = New System.Drawing.Point(10, 76)
        Me.butAdd.Name = "butAdd"
        Me.butAdd.Size = New System.Drawing.Size(26, 26)
        Me.butAdd.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.butAdd, "Add a language")
        '
        'DefinitionTableStyle
        '
        Me.DefinitionTableStyle.DataGrid = Me.DataGridDefinitions
        Me.DefinitionTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DesignerColumnLabel
        '
        Me.DesignerColumnLabel.Format = ""
        Me.DesignerColumnLabel.FormatInfo = Nothing
        Me.DesignerColumnLabel.MappingName = "Label"
        Me.DesignerColumnLabel.Width = -1
        '
        'DesignerColumnDefinition
        '
        Me.DesignerColumnDefinition.Format = ""
        Me.DesignerColumnDefinition.FormatInfo = Nothing
        Me.DesignerColumnDefinition.MappingName = "Definition"
        Me.DesignerColumnDefinition.Width = -1
        '
        'OpenFileDialogArchetype
        '
        Me.OpenFileDialogArchetype.ReadOnlyChecked = True
        '
        'lblArchetypeFileName
        '
        Me.lblArchetypeFileName.Location = New System.Drawing.Point(8, 32)
        Me.lblArchetypeFileName.Name = "lblArchetypeFileName"
        Me.lblArchetypeFileName.Size = New System.Drawing.Size(240, 19)
        Me.lblArchetypeFileName.TabIndex = 6
        Me.lblArchetypeFileName.Text = "Archetype file name:"
        '
        'lblPrimaryLanguage
        '
        Me.lblPrimaryLanguage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrimaryLanguage.Location = New System.Drawing.Point(20, 33)
        Me.lblPrimaryLanguage.Name = "lblPrimaryLanguage"
        Me.lblPrimaryLanguage.Size = New System.Drawing.Size(317, 19)
        Me.lblPrimaryLanguage.TabIndex = 7
        Me.lblPrimaryLanguage.Text = "-"
        Me.lblPrimaryLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPrimaryLanguageText
        '
        Me.lblPrimaryLanguageText.Location = New System.Drawing.Point(10, 0)
        Me.lblPrimaryLanguageText.Name = "lblPrimaryLanguageText"
        Me.lblPrimaryLanguageText.Size = New System.Drawing.Size(163, 28)
        Me.lblPrimaryLanguageText.TabIndex = 8
        Me.lblPrimaryLanguageText.Text = "Primary Language:"
        Me.lblPrimaryLanguageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuFile, Me.menuEdit, Me.MenuPublish, Me.MenuLanguage, Me.MenuTerminology, Me.MenuHelp})
        '
        'MenuFile
        '
        Me.MenuFile.Index = 0
        Me.MenuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuFileOpen, Me.MenuFileOpenFromWeb, Me.MenuFileNew, Me.menuFileNewWindow, Me.MenuFileSave, Me.MenuFileSaveAs, Me.menuFileExport, Me.MenuFileClose, Me.MenuFileSpecialise, Me.MenuFileExit})
        Me.MenuFile.Shortcut = System.Windows.Forms.Shortcut.CtrlF
        Me.MenuFile.ShowShortcut = False
        Me.MenuFile.Text = "File"
        '
        'MenuFileOpen
        '
        Me.MenuFileOpen.Index = 0
        Me.MenuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.MenuFileOpen.Text = "Open "
        '
        'MenuFileOpenFromWeb
        '
        Me.MenuFileOpenFromWeb.Index = 1
        Me.MenuFileOpenFromWeb.Shortcut = System.Windows.Forms.Shortcut.CtrlW
        Me.MenuFileOpenFromWeb.Text = "Open from Web"
        '
        'MenuFileNew
        '
        Me.MenuFileNew.Index = 2
        Me.MenuFileNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.MenuFileNew.Text = "New"
        '
        'menuFileNewWindow
        '
        Me.menuFileNewWindow.Index = 3
        Me.menuFileNewWindow.Text = "New window"
        '
        'MenuFileSave
        '
        Me.MenuFileSave.Index = 4
        Me.MenuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.MenuFileSave.Text = "Save"
        Me.MenuFileSave.Visible = False
        '
        'MenuFileSaveAs
        '
        Me.MenuFileSaveAs.Index = 5
        Me.MenuFileSaveAs.Text = "Save As"
        '
        'menuFileExport
        '
        Me.menuFileExport.Index = 6
        Me.menuFileExport.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuFileExportType})
        Me.menuFileExport.Text = "Export"
        '
        'MenuFileExportType
        '
        Me.MenuFileExportType.Index = 0
        Me.MenuFileExportType.Text = "Type"
        '
        'MenuFileClose
        '
        Me.MenuFileClose.Index = 7
        Me.MenuFileClose.Text = "Close"
        '
        'MenuFileSpecialise
        '
        Me.MenuFileSpecialise.Index = 8
        Me.MenuFileSpecialise.Text = "Specialise"
        Me.MenuFileSpecialise.Visible = False
        '
        'MenuFileExit
        '
        Me.MenuFileExit.Index = 9
        Me.MenuFileExit.Text = "E&xit"
        '
        'menuEdit
        '
        Me.menuEdit.Index = 1
        Me.menuEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuEditArchID, Me.MenuViewConfig})
        Me.menuEdit.Text = "Edit"
        '
        'menuEditArchID
        '
        Me.menuEditArchID.Index = 0
        Me.menuEditArchID.Text = "Archetype ID"
        '
        'MenuViewConfig
        '
        Me.MenuViewConfig.Index = 1
        Me.MenuViewConfig.Text = "Preferences"
        '
        'MenuPublish
        '
        Me.MenuPublish.Index = 2
        Me.MenuPublish.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuPublishPack, Me.MenuPublishFinalise})
        Me.MenuPublish.Text = "Publish"
        '
        'MenuPublishPack
        '
        Me.MenuPublishPack.Enabled = False
        Me.MenuPublishPack.Index = 0
        Me.MenuPublishPack.Text = "Pack"
        '
        'MenuPublishFinalise
        '
        Me.MenuPublishFinalise.Enabled = False
        Me.MenuPublishFinalise.Index = 1
        Me.MenuPublishFinalise.Text = "Finalise"
        '
        'MenuLanguage
        '
        Me.MenuLanguage.Index = 3
        Me.MenuLanguage.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuLanguageAvailable, Me.MenuLanguageAdd, Me.MenuLanguageChange})
        Me.MenuLanguage.Text = "Language"
        '
        'MenuLanguageAvailable
        '
        Me.MenuLanguageAvailable.Index = 0
        Me.MenuLanguageAvailable.Text = "Available languages"
        '
        'MenuLanguageAdd
        '
        Me.MenuLanguageAdd.Index = 1
        Me.MenuLanguageAdd.Text = "Add language"
        '
        'MenuLanguageChange
        '
        Me.MenuLanguageChange.Index = 2
        Me.MenuLanguageChange.Text = "Change language"
        '
        'MenuTerminology
        '
        Me.MenuTerminology.Index = 4
        Me.MenuTerminology.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuTerminologyAvailable, Me.MenuTerminologyAdd})
        Me.MenuTerminology.Text = "&Terminology"
        '
        'MenuTerminologyAvailable
        '
        Me.MenuTerminologyAvailable.Index = 0
        Me.MenuTerminologyAvailable.Text = "Available terminologies"
        '
        'MenuTerminologyAdd
        '
        Me.MenuTerminologyAdd.Index = 1
        Me.MenuTerminologyAdd.Text = "Add terminology"
        '
        'MenuHelp
        '
        Me.MenuHelp.Index = 5
        Me.MenuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuHelpStart, Me.MenuHelpReport, Me.MenuHelpLicence, Me.MenuHelpOcean, Me.MenuHelpOceanEditor})
        Me.MenuHelp.Text = "&Help"
        '
        'MenuHelpStart
        '
        Me.MenuHelpStart.Index = 0
        Me.MenuHelpStart.Text = "Help Topics"
        '
        'MenuHelpReport
        '
        Me.MenuHelpReport.Index = 1
        Me.MenuHelpReport.Text = "Report issue"
        '
        'MenuHelpLicence
        '
        Me.MenuHelpLicence.Index = 2
        Me.MenuHelpLicence.Text = "Licence"
        '
        'MenuHelpOcean
        '
        Me.MenuHelpOcean.Index = 3
        Me.MenuHelpOcean.Text = "About Ocean Informatics"
        '
        'MenuHelpOceanEditor
        '
        Me.MenuHelpOceanEditor.Index = 4
        Me.MenuHelpOceanEditor.Text = "About the Ocean Editor"
        '
        'PanelMain
        '
        Me.PanelMain.Controls.Add(Me.TabMain)
        Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMain.Location = New System.Drawing.Point(0, 92)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(969, 621)
        Me.PanelMain.TabIndex = 9
        '
        'TabMain
        '
        Me.TabMain.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabMain.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabMain.Location = New System.Drawing.Point(0, 0)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.PositionTop = True
        Me.TabMain.SelectedIndex = 0
        Me.TabMain.SelectedTab = Me.tpHeader
        Me.HelpProviderDesigner.SetShowHelp(Me.TabMain, True)
        Me.TabMain.Size = New System.Drawing.Size(969, 621)
        Me.TabMain.TabIndex = 1
        Me.TabMain.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpHeader, Me.tpDesign, Me.tpSectionPage, Me.tpTerminology, Me.tpText, Me.tpInterface, Me.tpDescription})
        Me.TabMain.TextInactiveColor = System.Drawing.Color.Black
        '
        'tpHeader
        '
        Me.tpHeader.BackColor = System.Drawing.Color.LemonChiffon
        Me.tpHeader.Controls.Add(Me.PanelDescription)
        Me.tpHeader.Controls.Add(Me.PanelConcept_1)
        Me.tpHeader.Controls.Add(Me.PanelConcept)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpHeader, "Screens/header.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpHeader, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpHeader.Location = New System.Drawing.Point(0, 0)
        Me.tpHeader.Name = "tpHeader"
        Me.HelpProviderDesigner.SetShowHelp(Me.tpHeader, True)
        Me.tpHeader.Size = New System.Drawing.Size(969, 595)
        Me.tpHeader.TabIndex = 0
        Me.tpHeader.Title = "Header"
        '
        'PanelDescription
        '
        Me.PanelDescription.Controls.Add(Me.RichTextBoxDescription)
        Me.PanelDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDescription.Location = New System.Drawing.Point(0, 111)
        Me.PanelDescription.Name = "PanelDescription"
        Me.PanelDescription.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelDescription.Size = New System.Drawing.Size(969, 283)
        Me.PanelDescription.TabIndex = 4
        '
        'RichTextBoxDescription
        '
        Me.RichTextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxDescription.Location = New System.Drawing.Point(10, 10)
        Me.RichTextBoxDescription.Name = "RichTextBoxDescription"
        Me.RichTextBoxDescription.ReadOnly = True
        Me.RichTextBoxDescription.Size = New System.Drawing.Size(949, 263)
        Me.RichTextBoxDescription.TabIndex = 5
        Me.RichTextBoxDescription.Text = ""
        '
        'PanelConcept_1
        '
        Me.PanelConcept_1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.PanelConcept_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelConcept_1.Controls.Add(Me.gbSpecialisation)
        Me.PanelConcept_1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelConcept_1.Location = New System.Drawing.Point(0, 394)
        Me.PanelConcept_1.Name = "PanelConcept_1"
        Me.PanelConcept_1.Size = New System.Drawing.Size(969, 201)
        Me.PanelConcept_1.TabIndex = 5
        '
        'gbSpecialisation
        '
        Me.gbSpecialisation.Controls.Add(Me.tvSpecialisation)
        Me.gbSpecialisation.Location = New System.Drawing.Point(328, 2)
        Me.gbSpecialisation.Name = "gbSpecialisation"
        Me.gbSpecialisation.Size = New System.Drawing.Size(624, 190)
        Me.gbSpecialisation.TabIndex = 12
        Me.gbSpecialisation.TabStop = False
        Me.gbSpecialisation.Text = "Specialisation"
        Me.gbSpecialisation.Visible = False
        '
        'tvSpecialisation
        '
        Me.tvSpecialisation.Location = New System.Drawing.Point(16, 30)
        Me.tvSpecialisation.Name = "tvSpecialisation"
        Me.tvSpecialisation.Size = New System.Drawing.Size(595, 138)
        Me.tvSpecialisation.TabIndex = 0
        '
        'tpDesign
        '
        Me.tpDesign.Controls.Add(Me.TabDesign)
        Me.tpDesign.Controls.Add(Me.PanelRoot)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpDesign, "Screens/Entry Model screen.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpDesign, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpDesign.Location = New System.Drawing.Point(0, 0)
        Me.tpDesign.Name = "tpDesign"
        Me.tpDesign.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpDesign, True)
        Me.tpDesign.Size = New System.Drawing.Size(969, 595)
        Me.tpDesign.TabIndex = 1
        Me.tpDesign.Title = "Definition"
        '
        'TabDesign
        '
        Me.TabDesign.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabDesign.BoldSelectedPage = True
        Me.TabDesign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderDesigner.SetHelpKeyword(Me.TabDesign, "HowTo/edit_data.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.TabDesign, System.Windows.Forms.HelpNavigator.Topic)
        Me.TabDesign.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabDesign.Location = New System.Drawing.Point(0, 37)
        Me.TabDesign.Name = "TabDesign"
        Me.TabDesign.PositionTop = True
        Me.TabDesign.SelectedIndex = 0
        Me.TabDesign.SelectedTab = Me.tpData
        Me.HelpProviderDesigner.SetShowHelp(Me.TabDesign, True)
        Me.TabDesign.Size = New System.Drawing.Size(969, 558)
        Me.TabDesign.TabIndex = 12
        Me.TabDesign.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpData, Me.tpRootState, Me.tpParticipation})
        Me.TabDesign.TextInactiveColor = System.Drawing.Color.Black
        '
        'tpData
        '
        Me.tpData.Controls.Add(Me.TabStructure)
        Me.tpData.Controls.Add(Me.PanelConfigStructure)
        Me.tpData.Location = New System.Drawing.Point(0, 0)
        Me.tpData.Name = "tpData"
        Me.tpData.Size = New System.Drawing.Size(969, 532)
        Me.tpData.TabIndex = 0
        Me.tpData.Title = "Data"
        '
        'TabStructure
        '
        Me.TabStructure.BackColor = System.Drawing.Color.RoyalBlue
        Me.TabStructure.BoldSelectedPage = True
        Me.TabStructure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderDesigner.SetHelpKeyword(Me.TabStructure, "HowTo/edit_data.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.TabStructure, System.Windows.Forms.HelpNavigator.Topic)
        Me.TabStructure.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabStructure.Location = New System.Drawing.Point(0, 28)
        Me.TabStructure.Name = "TabStructure"
        Me.TabStructure.PositionTop = True
        Me.TabStructure.SelectedIndex = 0
        Me.TabStructure.SelectedTab = Me.tpDataStructure
        Me.HelpProviderDesigner.SetShowHelp(Me.TabStructure, True)
        Me.TabStructure.Size = New System.Drawing.Size(969, 504)
        Me.TabStructure.TabIndex = 10
        Me.TabStructure.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpDataStructure})
        Me.TabStructure.TextInactiveColor = System.Drawing.Color.Black
        '
        'tpDataStructure
        '
        Me.tpDataStructure.BackColor = System.Drawing.Color.CornflowerBlue
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpDataStructure, "HowTo/edit_data.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpDataStructure, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpDataStructure.Location = New System.Drawing.Point(0, 0)
        Me.tpDataStructure.Name = "tpDataStructure"
        Me.HelpProviderDesigner.SetShowHelp(Me.tpDataStructure, True)
        Me.tpDataStructure.Size = New System.Drawing.Size(969, 478)
        Me.tpDataStructure.TabIndex = 0
        Me.tpDataStructure.Title = "Structure"
        '
        'tpRootState
        '
        Me.tpRootState.Controls.Add(Me.TabState)
        Me.tpRootState.Controls.Add(Me.PanelState)
        Me.tpRootState.Location = New System.Drawing.Point(0, 0)
        Me.tpRootState.Name = "tpRootState"
        Me.tpRootState.Selected = False
        Me.tpRootState.Size = New System.Drawing.Size(969, 532)
        Me.tpRootState.TabIndex = 1
        Me.tpRootState.Title = "State"
        '
        'TabState
        '
        Me.TabState.BackColor = System.Drawing.Color.RoyalBlue
        Me.TabState.BoldSelectedPage = True
        Me.TabState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderDesigner.SetHelpKeyword(Me.TabState, "HowTo/edit_state.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.TabState, System.Windows.Forms.HelpNavigator.Topic)
        Me.TabState.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabState.Location = New System.Drawing.Point(0, 28)
        Me.TabState.Name = "TabState"
        Me.TabState.PositionTop = True
        Me.TabState.SelectedIndex = 0
        Me.TabState.SelectedTab = Me.tpRootStateStructure
        Me.HelpProviderDesigner.SetShowHelp(Me.TabState, True)
        Me.TabState.Size = New System.Drawing.Size(969, 504)
        Me.TabState.TabIndex = 1
        Me.TabState.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpRootStateStructure, Me.tpRootStateEventSeries})
        '
        'tpRootStateStructure
        '
        Me.tpRootStateStructure.BackColor = System.Drawing.Color.CornflowerBlue
        Me.tpRootStateStructure.Location = New System.Drawing.Point(0, 0)
        Me.tpRootStateStructure.Name = "tpRootStateStructure"
        Me.tpRootStateStructure.Size = New System.Drawing.Size(969, 478)
        Me.tpRootStateStructure.TabIndex = 0
        Me.tpRootStateStructure.Title = "Structure"
        '
        'tpRootStateEventSeries
        '
        Me.tpRootStateEventSeries.BackColor = System.Drawing.Color.CornflowerBlue
        Me.tpRootStateEventSeries.Location = New System.Drawing.Point(0, 0)
        Me.tpRootStateEventSeries.Name = "tpRootStateEventSeries"
        Me.tpRootStateEventSeries.Selected = False
        Me.tpRootStateEventSeries.Size = New System.Drawing.Size(969, 478)
        Me.tpRootStateEventSeries.TabIndex = 1
        Me.tpRootStateEventSeries.Title = "State Event Series"
        '
        'PanelState
        '
        Me.PanelState.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PanelState.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelState.Location = New System.Drawing.Point(0, 0)
        Me.PanelState.Name = "PanelState"
        Me.PanelState.Size = New System.Drawing.Size(969, 28)
        Me.PanelState.TabIndex = 0
        '
        'tpParticipation
        '
        Me.tpParticipation.Location = New System.Drawing.Point(0, 0)
        Me.tpParticipation.Name = "tpParticipation"
        Me.tpParticipation.Selected = False
        Me.tpParticipation.Size = New System.Drawing.Size(969, 532)
        Me.tpParticipation.TabIndex = 2
        Me.tpParticipation.Title = "Participation"
        '
        'tpSectionPage
        '
        Me.tpSectionPage.BackColor = System.Drawing.Color.LightYellow
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpSectionPage, "Screens/section_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpSectionPage, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpSectionPage.Location = New System.Drawing.Point(0, 0)
        Me.tpSectionPage.Name = "tpSectionPage"
        Me.tpSectionPage.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpSectionPage, True)
        Me.tpSectionPage.Size = New System.Drawing.Size(969, 595)
        Me.tpSectionPage.TabIndex = 4
        Me.tpSectionPage.Title = "Definition"
        '
        'tpTerminology
        '
        Me.tpTerminology.Controls.Add(Me.TabTerminology)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpTerminology, "Screens/terminology_screens.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpTerminology, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpTerminology.Location = New System.Drawing.Point(0, 0)
        Me.tpTerminology.Name = "tpTerminology"
        Me.tpTerminology.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpTerminology, True)
        Me.tpTerminology.Size = New System.Drawing.Size(969, 595)
        Me.tpTerminology.TabIndex = 2
        Me.tpTerminology.Title = "Terminology"
        '
        'TabTerminology
        '
        Me.TabTerminology.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabTerminology.BoldSelectedPage = True
        Me.TabTerminology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabTerminology.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabTerminology.Location = New System.Drawing.Point(0, 0)
        Me.TabTerminology.Name = "TabTerminology"
        Me.TabTerminology.PositionTop = True
        Me.TabTerminology.SelectedIndex = 0
        Me.TabTerminology.SelectedTab = Me.tpTerms
        Me.TabTerminology.Size = New System.Drawing.Size(969, 595)
        Me.TabTerminology.TabIndex = 0
        Me.TabTerminology.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpTerms, Me.tpBindings, Me.tpConstraints, Me.tpLanguages})
        '
        'tpTerms
        '
        Me.tpTerms.BackColor = System.Drawing.Color.LightYellow
        Me.tpTerms.Controls.Add(Me.DataGridDefinitions)
        Me.tpTerms.Controls.Add(Me.PanelTermDefinitions)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpTerms, "Screens/term_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpTerms, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpTerms.Location = New System.Drawing.Point(0, 0)
        Me.tpTerms.Name = "tpTerms"
        Me.HelpProviderDesigner.SetShowHelp(Me.tpTerms, True)
        Me.tpTerms.Size = New System.Drawing.Size(969, 569)
        Me.tpTerms.TabIndex = 2
        Me.tpTerms.Title = "Terms "
        Me.ToolTip1.SetToolTip(Me.tpTerms, "Internal term definitions")
        '
        'PanelTermDefinitions
        '
        Me.PanelTermDefinitions.BackColor = System.Drawing.Color.LightYellow
        Me.PanelTermDefinitions.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTermDefinitions.Location = New System.Drawing.Point(0, 0)
        Me.PanelTermDefinitions.Name = "PanelTermDefinitions"
        Me.PanelTermDefinitions.Size = New System.Drawing.Size(969, 18)
        Me.PanelTermDefinitions.TabIndex = 3
        '
        'tpBindings
        '
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpBindings, "Screens/term_binding_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpBindings, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpBindings.Location = New System.Drawing.Point(0, 0)
        Me.tpBindings.Name = "tpBindings"
        Me.tpBindings.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpBindings, True)
        Me.tpBindings.Size = New System.Drawing.Size(969, 569)
        Me.tpBindings.TabIndex = 3
        Me.tpBindings.Title = "Term Bindings"
        '
        'tpConstraints
        '
        Me.tpConstraints.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.tpConstraints.Controls.Add(Me.DataGridConstraintStatements)
        Me.tpConstraints.Controls.Add(Me.panelConstraintStatementTop)
        Me.tpConstraints.Controls.Add(Me.Splitter1)
        Me.tpConstraints.Controls.Add(Me.DataGridConstraintDefinitions)
        Me.tpConstraints.Controls.Add(Me.PanelConstraintDefTop)
        Me.tpConstraints.Controls.Add(Me.PanelConstraintBinding)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpConstraints, "Screens/constraints_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpConstraints, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpConstraints.Location = New System.Drawing.Point(0, 0)
        Me.tpConstraints.Name = "tpConstraints"
        Me.tpConstraints.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpConstraints, True)
        Me.tpConstraints.Size = New System.Drawing.Size(969, 569)
        Me.tpConstraints.TabIndex = 1
        Me.tpConstraints.Title = "Constraints"
        Me.ToolTip1.SetToolTip(Me.tpConstraints, "Constraint definitions")
        '
        'DataGridConstraintStatements
        '
        Me.DataGridConstraintStatements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridConstraintStatements.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.terminology, Me.Code, Me.release})
        Me.DataGridConstraintStatements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridConstraintStatements.Location = New System.Drawing.Point(0, 338)
        Me.DataGridConstraintStatements.Name = "DataGridConstraintStatements"
        Me.DataGridConstraintStatements.RowTemplate.Height = 24
        Me.DataGridConstraintStatements.Size = New System.Drawing.Size(969, 167)
        Me.DataGridConstraintStatements.TabIndex = 12
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "acCode"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'terminology
        '
        Me.terminology.DataPropertyName = "Terminology"
        Me.terminology.FillWeight = 60.0!
        Me.terminology.HeaderText = "Terminology"
        Me.terminology.MinimumWidth = 100
        Me.terminology.Name = "terminology"
        Me.terminology.Width = 400
        '
        'Code
        '
        Me.Code.DataPropertyName = "CodePhrase"
        Me.Code.FillWeight = 20.0!
        Me.Code.HeaderText = "Query ID"
        Me.Code.Name = "Code"
        Me.Code.Width = 350
        '
        'release
        '
        Me.release.DataPropertyName = "Release"
        Me.release.FillWeight = 10.0!
        Me.release.HeaderText = "Release"
        Me.release.Name = "release"
        Me.release.Width = 150
        '
        'panelConstraintStatementTop
        '
        Me.panelConstraintStatementTop.BackColor = System.Drawing.Color.CornflowerBlue
        Me.panelConstraintStatementTop.Controls.Add(Me.lblConstraintStatements)
        Me.panelConstraintStatementTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelConstraintStatementTop.Location = New System.Drawing.Point(0, 314)
        Me.panelConstraintStatementTop.Name = "panelConstraintStatementTop"
        Me.panelConstraintStatementTop.Size = New System.Drawing.Size(969, 24)
        Me.panelConstraintStatementTop.TabIndex = 14
        '
        'lblConstraintStatements
        '
        Me.lblConstraintStatements.AutoSize = True
        Me.lblConstraintStatements.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World)
        Me.lblConstraintStatements.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblConstraintStatements.Location = New System.Drawing.Point(8, 3)
        Me.lblConstraintStatements.Name = "lblConstraintStatements"
        Me.lblConstraintStatements.Size = New System.Drawing.Size(153, 18)
        Me.lblConstraintStatements.TabIndex = 13
        Me.lblConstraintStatements.Text = "Constraint bindings"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 303)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(969, 11)
        Me.Splitter1.TabIndex = 11
        Me.Splitter1.TabStop = False
        '
        'PanelConstraintDefTop
        '
        Me.PanelConstraintDefTop.BackColor = System.Drawing.Color.LightYellow
        Me.PanelConstraintDefTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelConstraintDefTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelConstraintDefTop.Name = "PanelConstraintDefTop"
        Me.PanelConstraintDefTop.Size = New System.Drawing.Size(969, 8)
        Me.PanelConstraintDefTop.TabIndex = 10
        '
        'PanelConstraintBinding
        '
        Me.PanelConstraintBinding.BackColor = System.Drawing.Color.LemonChiffon
        Me.PanelConstraintBinding.Controls.Add(Me.butLookUpConstraint)
        Me.PanelConstraintBinding.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelConstraintBinding.Location = New System.Drawing.Point(0, 505)
        Me.PanelConstraintBinding.Name = "PanelConstraintBinding"
        Me.PanelConstraintBinding.Size = New System.Drawing.Size(969, 64)
        Me.PanelConstraintBinding.TabIndex = 7
        '
        'butLookUpConstraint
        '
        Me.butLookUpConstraint.BackColor = System.Drawing.Color.LemonChiffon
        Me.butLookUpConstraint.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Me.butLookUpConstraint.Location = New System.Drawing.Point(264, 16)
        Me.butLookUpConstraint.Name = "butLookUpConstraint"
        Me.butLookUpConstraint.Size = New System.Drawing.Size(320, 28)
        Me.butLookUpConstraint.TabIndex = 2
        Me.butLookUpConstraint.Text = "Add constraint binding"
        Me.butLookUpConstraint.UseVisualStyleBackColor = False
        '
        'tpLanguages
        '
        Me.tpLanguages.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.tpLanguages.Controls.Add(Me.DataGridTerminologies)
        Me.tpLanguages.Controls.Add(Me.Panel2)
        Me.tpLanguages.Controls.Add(Me.Splitter2)
        Me.tpLanguages.Controls.Add(Me.panelLanguages)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpLanguages, "Screens/languages_terminologies_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpLanguages, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpLanguages.Location = New System.Drawing.Point(0, 0)
        Me.tpLanguages.Name = "tpLanguages"
        Me.tpLanguages.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpLanguages, True)
        Me.tpLanguages.Size = New System.Drawing.Size(969, 569)
        Me.tpLanguages.TabIndex = 0
        Me.tpLanguages.Title = "Languages && Terminologies"
        Me.ToolTip1.SetToolTip(Me.tpLanguages, "Available Languages and terminologies")
        '
        'DataGridTerminologies
        '
        Me.DataGridTerminologies.CaptionBackColor = System.Drawing.Color.CornflowerBlue
        Me.DataGridTerminologies.CaptionText = "Available terminologies"
        Me.DataGridTerminologies.DataMember = ""
        Me.DataGridTerminologies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridTerminologies.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTerminologies.Location = New System.Drawing.Point(384, 46)
        Me.DataGridTerminologies.Name = "DataGridTerminologies"
        Me.DataGridTerminologies.ReadOnly = True
        Me.DataGridTerminologies.Size = New System.Drawing.Size(585, 523)
        Me.DataGridTerminologies.TabIndex = 10
        Me.DataGridTerminologies.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle3, Me.DataGridTableStyle4, Me.DataGridTableStyle5})
        '
        'DataGridTableStyle3
        '
        Me.DataGridTableStyle3.DataGrid = Me.DataGridTerminologies
        Me.DataGridTableStyle3.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn5})
        Me.DataGridTableStyle3.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle3.MappingName = "Terminologies"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Code"
        Me.DataGridTextBoxColumn1.MappingName = "Terminology"
        Me.DataGridTextBoxColumn1.Width = 125
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Description"
        Me.DataGridTextBoxColumn5.MappingName = "Description"
        Me.DataGridTextBoxColumn5.Width = 325
        '
        'DataGridTableStyle4
        '
        Me.DataGridTableStyle4.DataGrid = Me.DataGridTerminologies
        Me.DataGridTableStyle4.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12})
        Me.DataGridTableStyle4.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle4.MappingName = "TermBindings"
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Path"
        Me.DataGridTextBoxColumn9.MappingName = "Path"
        Me.DataGridTextBoxColumn9.Width = 75
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Code"
        Me.DataGridTextBoxColumn11.MappingName = "Code"
        Me.DataGridTextBoxColumn11.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Release"
        Me.DataGridTextBoxColumn12.MappingName = "Release"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'DataGridTableStyle5
        '
        Me.DataGridTableStyle5.DataGrid = Me.DataGridTerminologies
        Me.DataGridTableStyle5.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn13})
        Me.DataGridTableStyle5.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle5.MappingName = "TermBindingCriteria"
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Criteria"
        Me.DataGridTextBoxColumn13.MappingName = "Criteria"
        Me.DataGridTextBoxColumn13.Width = 250
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightYellow
        Me.Panel2.Controls.Add(Me.butAddTerminology)
        Me.Panel2.Controls.Add(Me.lblAvailableTerminologies)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(384, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(585, 46)
        Me.Panel2.TabIndex = 13
        '
        'butAddTerminology
        '
        Me.butAddTerminology.Image = CType(resources.GetObject("butAddTerminology.Image"), System.Drawing.Image)
        Me.butAddTerminology.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddTerminology.Location = New System.Drawing.Point(10, 9)
        Me.butAddTerminology.Name = "butAddTerminology"
        Me.butAddTerminology.Size = New System.Drawing.Size(27, 31)
        Me.butAddTerminology.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.butAddTerminology, "Add a language")
        '
        'lblAvailableTerminologies
        '
        Me.lblAvailableTerminologies.Location = New System.Drawing.Point(48, 15)
        Me.lblAvailableTerminologies.Name = "lblAvailableTerminologies"
        Me.lblAvailableTerminologies.Size = New System.Drawing.Size(250, 25)
        Me.lblAvailableTerminologies.TabIndex = 11
        Me.lblAvailableTerminologies.Text = "Available terminologies:"
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(374, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(10, 569)
        Me.Splitter2.TabIndex = 12
        Me.Splitter2.TabStop = False
        '
        'panelLanguages
        '
        Me.panelLanguages.BackColor = System.Drawing.Color.LightYellow
        Me.panelLanguages.Controls.Add(Me.ListLanguages)
        Me.panelLanguages.Controls.Add(Me.Panel1)
        Me.panelLanguages.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelLanguages.Location = New System.Drawing.Point(0, 0)
        Me.panelLanguages.Name = "panelLanguages"
        Me.panelLanguages.Size = New System.Drawing.Size(374, 569)
        Me.panelLanguages.TabIndex = 2
        '
        'ListLanguages
        '
        Me.ListLanguages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListLanguages.ItemHeight = 17
        Me.ListLanguages.Location = New System.Drawing.Point(0, 111)
        Me.ListLanguages.Name = "ListLanguages"
        Me.ListLanguages.Size = New System.Drawing.Size(374, 446)
        Me.ListLanguages.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblAvailableLanguages)
        Me.Panel1.Controls.Add(Me.butAdd)
        Me.Panel1.Controls.Add(Me.lblPrimaryLanguageText)
        Me.Panel1.Controls.Add(Me.lblPrimaryLanguage)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(374, 111)
        Me.Panel1.TabIndex = 11
        '
        'lblAvailableLanguages
        '
        Me.lblAvailableLanguages.Location = New System.Drawing.Point(49, 80)
        Me.lblAvailableLanguages.Name = "lblAvailableLanguages"
        Me.lblAvailableLanguages.Size = New System.Drawing.Size(250, 27)
        Me.lblAvailableLanguages.TabIndex = 10
        Me.lblAvailableLanguages.Text = "Available languages:"
        '
        'tpText
        '
        Me.tpText.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpText.Controls.Add(Me.Panel3)
        Me.tpText.Controls.Add(Me.panelDiplayTop)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpText, "Screens/display_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpText, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpText.Location = New System.Drawing.Point(0, 0)
        Me.tpText.Name = "tpText"
        Me.tpText.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpText, True)
        Me.tpText.Size = New System.Drawing.Size(969, 595)
        Me.tpText.TabIndex = 3
        Me.tpText.Title = "Display"
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 40)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel3.Size = New System.Drawing.Size(969, 555)
        Me.Panel3.TabIndex = 4
        '
        'panelDiplayTop
        '
        Me.panelDiplayTop.Controls.Add(Me.ToolBarRTF)
        Me.panelDiplayTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelDiplayTop.Location = New System.Drawing.Point(0, 0)
        Me.panelDiplayTop.Name = "panelDiplayTop"
        Me.panelDiplayTop.Size = New System.Drawing.Size(969, 40)
        Me.panelDiplayTop.TabIndex = 3
        '
        'ToolBarRTF
        '
        Me.ToolBarRTF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolBarRTF.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBarRTF.AutoSize = False
        Me.ToolBarRTF.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbSep1, Me.butRTF, Me.butADL, Me.butXML, Me.butOWL, Me.tbSep2, Me.butHTML1, Me.ToolBarButton1, Me.butSaveFile, Me.ToolBarButton2, Me.butPrint})
        Me.ToolBarRTF.ButtonSize = New System.Drawing.Size(20, 30)
        Me.ToolBarRTF.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolBarRTF.DropDownArrows = True
        Me.ToolBarRTF.ImageList = Me.ImageListToolbar
        Me.ToolBarRTF.Location = New System.Drawing.Point(0, 0)
        Me.ToolBarRTF.Name = "ToolBarRTF"
        Me.ToolBarRTF.ShowToolTips = True
        Me.ToolBarRTF.Size = New System.Drawing.Size(969, 40)
        Me.ToolBarRTF.TabIndex = 4
        Me.ToolBarRTF.Wrappable = False
        '
        'tbSep1
        '
        Me.tbSep1.Name = "tbSep1"
        Me.tbSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'butRTF
        '
        Me.butRTF.ImageIndex = 4
        Me.butRTF.Name = "butRTF"
        Me.butRTF.Pushed = True
        Me.butRTF.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.butRTF.Tag = "rtf"
        Me.butRTF.Text = "RTF"
        '
        'butADL
        '
        Me.butADL.Name = "butADL"
        Me.butADL.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.butADL.Tag = "adl"
        Me.butADL.Text = "ADL"
        '
        'butXML
        '
        Me.butXML.Name = "butXML"
        Me.butXML.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.butXML.Tag = "xml"
        Me.butXML.Text = "XML"
        '
        'butOWL
        '
        Me.butOWL.Name = "butOWL"
        Me.butOWL.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.butOWL.Tag = "owl"
        Me.butOWL.Text = "OWL"
        '
        'tbSep2
        '
        Me.tbSep2.Name = "tbSep2"
        Me.tbSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'butHTML1
        '
        Me.butHTML1.ImageIndex = 5
        Me.butHTML1.Name = "butHTML1"
        Me.butHTML1.Tag = "html"
        Me.butHTML1.Text = "HTML"
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'butSaveFile
        '
        Me.butSaveFile.ImageIndex = 1
        Me.butSaveFile.Name = "butSaveFile"
        Me.butSaveFile.Tag = "save"
        Me.butSaveFile.Text = "Save"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.Name = "ToolBarButton2"
        Me.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'butPrint
        '
        Me.butPrint.ImageIndex = 2
        Me.butPrint.Name = "butPrint"
        Me.butPrint.Tag = "print"
        Me.butPrint.Text = "Print"
        '
        'ImageListToolbar
        '
        Me.ImageListToolbar.ImageStream = CType(resources.GetObject("ImageListToolbar.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListToolbar.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListToolbar.Images.SetKeyName(0, "")
        Me.ImageListToolbar.Images.SetKeyName(1, "")
        Me.ImageListToolbar.Images.SetKeyName(2, "")
        Me.ImageListToolbar.Images.SetKeyName(3, "")
        Me.ImageListToolbar.Images.SetKeyName(4, "")
        Me.ImageListToolbar.Images.SetKeyName(5, "")
        Me.ImageListToolbar.Images.SetKeyName(6, "searchweb.ico")
        '
        'tpInterface
        '
        Me.tpInterface.AutoScroll = True
        Me.tpInterface.Controls.Add(Me.cbMandatory)
        Me.HelpProviderDesigner.SetHelpKeyword(Me.tpInterface, "Screens/interface_screen.html")
        Me.HelpProviderDesigner.SetHelpNavigator(Me.tpInterface, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpInterface.Location = New System.Drawing.Point(0, 0)
        Me.tpInterface.Name = "tpInterface"
        Me.tpInterface.Selected = False
        Me.HelpProviderDesigner.SetShowHelp(Me.tpInterface, True)
        Me.tpInterface.Size = New System.Drawing.Size(969, 595)
        Me.tpInterface.TabIndex = 5
        Me.tpInterface.Title = "Interface"
        '
        'cbMandatory
        '
        Me.cbMandatory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbMandatory.Location = New System.Drawing.Point(792, 8)
        Me.cbMandatory.Name = "cbMandatory"
        Me.cbMandatory.Size = New System.Drawing.Size(152, 24)
        Me.cbMandatory.TabIndex = 0
        Me.cbMandatory.Text = "Mandatory"
        '
        'tpDescription
        '
        Me.tpDescription.Location = New System.Drawing.Point(0, 0)
        Me.tpDescription.Name = "tpDescription"
        Me.tpDescription.Selected = False
        Me.tpDescription.Size = New System.Drawing.Size(969, 595)
        Me.tpDescription.TabIndex = 6
        Me.tpDescription.Title = "Description"
        '
        'ContextMenuDisplay
        '
        Me.ContextMenuDisplay.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuDisplayPrint, Me.MenuDisplayFind, Me.MenuDisplayFindAgain, Me.menuDisplaySaveAs})
        '
        'menuDisplayPrint
        '
        Me.menuDisplayPrint.Index = 0
        Me.menuDisplayPrint.Text = "Print"
        '
        'MenuDisplayFind
        '
        Me.MenuDisplayFind.Index = 1
        Me.MenuDisplayFind.Text = "Find"
        '
        'MenuDisplayFindAgain
        '
        Me.MenuDisplayFindAgain.Index = 2
        Me.MenuDisplayFindAgain.Text = "Find again"
        '
        'menuDisplaySaveAs
        '
        Me.menuDisplaySaveAs.Index = 3
        Me.menuDisplaySaveAs.Text = "Save as"
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.Ivory
        Me.PanelHeader.Controls.Add(Me.lblArchetypeFileName)
        Me.PanelHeader.Controls.Add(Me.lblArchetypeName)
        Me.PanelHeader.Controls.Add(Me.lblLifecycle)
        Me.PanelHeader.Controls.Add(Me.ToolBarMain)
        Me.PanelHeader.Controls.Add(Me.PictureBox1)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(969, 92)
        Me.PanelHeader.TabIndex = 10
        '
        'lblArchetypeName
        '
        Me.lblArchetypeName.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblArchetypeName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArchetypeName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblArchetypeName.Location = New System.Drawing.Point(0, 57)
        Me.lblArchetypeName.Name = "lblArchetypeName"
        Me.lblArchetypeName.Size = New System.Drawing.Size(881, 35)
        Me.lblArchetypeName.TabIndex = 10
        Me.lblArchetypeName.Text = "Archetype Editor by Ocean Informatics"
        '
        'lblLifecycle
        '
        Me.lblLifecycle.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLifecycle.Location = New System.Drawing.Point(797, 37)
        Me.lblLifecycle.Name = "lblLifecycle"
        Me.lblLifecycle.Size = New System.Drawing.Size(57, 46)
        Me.lblLifecycle.TabIndex = 12
        Me.lblLifecycle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLifecycle.Visible = False
        '
        'ToolBarMain
        '
        Me.ToolBarMain.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarNew, Me.ToolBarOpen, Me.ToolBarOpenFromWeb, Me.ToolBarSave, Me.ToolBarSeparator1, Me.ToolBarPrint})
        Me.ToolBarMain.DropDownArrows = True
        Me.ToolBarMain.ImageList = Me.ImageListToolbar
        Me.ToolBarMain.Location = New System.Drawing.Point(0, 0)
        Me.ToolBarMain.Name = "ToolBarMain"
        Me.ToolBarMain.ShowToolTips = True
        Me.ToolBarMain.Size = New System.Drawing.Size(881, 28)
        Me.ToolBarMain.TabIndex = 11
        '
        'ToolBarNew
        '
        Me.ToolBarNew.ImageIndex = 3
        Me.ToolBarNew.Name = "ToolBarNew"
        Me.ToolBarNew.ToolTipText = "Create a new archetype"
        Me.ToolBarNew.Visible = False
        '
        'ToolBarOpen
        '
        Me.ToolBarOpen.ImageIndex = 0
        Me.ToolBarOpen.Name = "ToolBarOpen"
        Me.ToolBarOpen.ToolTipText = "Open archetype"
        '
        'ToolBarOpenFromWeb
        '
        Me.ToolBarOpenFromWeb.ImageIndex = 6
        Me.ToolBarOpenFromWeb.Name = "ToolBarOpenFromWeb"
        Me.ToolBarOpenFromWeb.Visible = False
        '
        'ToolBarSave
        '
        Me.ToolBarSave.ImageIndex = 1
        Me.ToolBarSave.Name = "ToolBarSave"
        Me.ToolBarSave.ToolTipText = "Save archetype"
        Me.ToolBarSave.Visible = False
        '
        'ToolBarSeparator1
        '
        Me.ToolBarSeparator1.Name = "ToolBarSeparator1"
        Me.ToolBarSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarPrint
        '
        Me.ToolBarPrint.ImageIndex = 2
        Me.ToolBarPrint.Name = "ToolBarPrint"
        Me.ToolBarPrint.ToolTipText = "Print archetype"
        Me.ToolBarPrint.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(881, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(88, 92)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'butLinks
        '
        Me.butLinks.Image = CType(resources.GetObject("butLinks.Image"), System.Drawing.Image)
        Me.butLinks.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.butLinks.Location = New System.Drawing.Point(318, 42)
        Me.butLinks.Name = "butLinks"
        Me.butLinks.Size = New System.Drawing.Size(97, 27)
        Me.butLinks.TabIndex = 2
        Me.butLinks.Text = "Links"
        Me.butLinks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.butLinks.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.butLinks.UseVisualStyleBackColor = True
        '
        'Designer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(969, 713)
        Me.Controls.Add(Me.PanelMain)
        Me.Controls.Add(Me.PanelHeader)
        Me.HelpProviderDesigner.SetHelpKeyword(Me, "HowTo/ocean_archetype_editor.htm")
        Me.HelpProviderDesigner.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu
        Me.Name = "Designer"
        Me.HelpProviderDesigner.SetShowHelp(Me, True)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Archetype Editor"
        Me.PanelConcept.ResumeLayout(False)
        Me.PanelConcept.PerformLayout()
        Me.tabComment.ResumeLayout(False)
        Me.tpConceptDescription.ResumeLayout(False)
        Me.tpConceptDescription.PerformLayout()
        Me.tpConceptComment.ResumeLayout(False)
        Me.tpConceptComment.PerformLayout()
        Me.PanelConfigStructure.ResumeLayout(False)
        Me.PanelRoot.ResumeLayout(False)
        CType(Me.DataGridConstraintDefinitions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridDefinitions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelMain.ResumeLayout(False)
        Me.tpHeader.ResumeLayout(False)
        Me.PanelDescription.ResumeLayout(False)
        Me.PanelConcept_1.ResumeLayout(False)
        Me.gbSpecialisation.ResumeLayout(False)
        Me.tpDesign.ResumeLayout(False)
        Me.tpData.ResumeLayout(False)
        Me.tpRootState.ResumeLayout(False)
        Me.tpTerminology.ResumeLayout(False)
        Me.tpTerms.ResumeLayout(False)
        Me.tpConstraints.ResumeLayout(False)
        CType(Me.DataGridConstraintStatements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelConstraintStatementTop.ResumeLayout(False)
        Me.panelConstraintStatementTop.PerformLayout()
        Me.PanelConstraintBinding.ResumeLayout(False)
        Me.tpLanguages.ResumeLayout(False)
        CType(Me.DataGridTerminologies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.panelLanguages.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.tpText.ResumeLayout(False)
        Me.panelDiplayTop.ResumeLayout(False)
        Me.tpInterface.ResumeLayout(False)
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Principle functions"

    Private Sub OpenArchetype(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFileOpen.Click

        If Not CheckOKtoClose() Then
            Return
        End If

        Me.OpenFileDialogArchetype.Filter = "ADL|*.adl|XML|*.xml|All files|*.*"

        Select Case mFileManager.ParserType
            Case "adl"
                Me.OpenFileDialogArchetype.FilterIndex = 1
            Case "xml"
                Me.OpenFileDialogArchetype.FilterIndex = 2
            Case Else
                Me.OpenFileDialogArchetype.FilterIndex = 3
        End Select

        If mFileManager.WorkingDirectory <> "" Then
            Me.OpenFileDialogArchetype.InitialDirectory = mFileManager.WorkingDirectory
        End If

        If OpenFileDialogArchetype.ShowDialog(Me) = System.Windows.Forms.DialogResult.Cancel Then
            Return
        End If

        OpenArchetype(Me.OpenFileDialogArchetype.FileName)

    End Sub

    ' added funtion (by Jana Graenz)
    ' loads the Windows form "WebSearchForm" for enabling a search for archetypes from the web 
    ' (accessing the web-based Archetype Finder via its provided Web Services)
    Private Sub OpenArchetypeFromWeb(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFileOpenFromWeb.Click
        If Not CheckOKtoClose() Then
            Return
        End If


        Dim frm As New WebSearchForm

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then

            frm.Text = Filemanager.GetOpenEhrTerm(658, frm.Text)
            frm.lblSearch.Text = Filemanager.GetOpenEhrTerm(651, frm.lblSearch.Text)
            frm.rdbtn_any.Text = Filemanager.GetOpenEhrTerm(657, frm.rdbtn_any.Text)
            frm.rdbtn_id.Text = Filemanager.GetOpenEhrTerm(632, frm.rdbtn_id.Text)
            frm.rdbtn_des.Text = Filemanager.GetOpenEhrTerm(113, frm.rdbtn_des.Text)
            frm.rdbtn_con.Text = Filemanager.GetOpenEhrTerm(54, frm.rdbtn_con.Text)
            frm.btnSearch.Text = Filemanager.GetOpenEhrTerm(655, frm.btnSearch.Text)
            frm.btnReset.Text = Filemanager.GetOpenEhrTerm(656, frm.btnReset.Text)
            frm.lblNum.Text = Filemanager.GetOpenEhrTerm(653, frm.lblNum.Text)

        End If

        frm.ShowDialog(Me)
        Dim myArchetypeURL As String
        ' The boolean variable "chosen" determines if the user decided to open one found archetype 
        ' (a result set is shown as a list with button a Open-Button for each archetype on the WebSearchForm)
        ' is chosen true, we find out which archetypes has to be opened and takes its URL for further processing
        If frm.chosen = True Then

            myArchetypeURL = frm.getArchetypeIdTobeOpened().Trim
            OpenArchetype(myArchetypeURL)

        End If
    End Sub

    Private Sub OpenArchetype(ByVal a_file_name As String)
        Dim i As Integer

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        ' stop auto updating of controls
        Dim previousEditedState As Boolean = mFileManager.FileEdited

        mFileManager.FileEdited = False
        mFileManager.FileLoading = True
        Dim watch As Stopwatch = Stopwatch.StartNew()
        If Not mFileManager.OpenArchetype(a_file_name) Then
            MessageBox.Show(mFileManager.Status, _
                AE_Constants.Instance.MessageBoxCaption, _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            mFileManager.ParserReset()
            mFileManager.FileLoading = False
            mFileManager.FileEdited = previousEditedState
            Exit Sub
        End If

        Debug.WriteLine(a_file_name + ": " + CStr(watch.Elapsed.TotalSeconds))

        'Show the correct display format toolbars
        For i = 2 To 5
            Dim tbb As ToolBarButton = Me.ToolBarRTF.Buttons(i)
            If Not mFileManager.AvailableFormats.Contains(tbb.Tag) Then
                tbb.Visible = False
            Else
                tbb.Visible = True
            End If
        Next

        'remove embedded filemanagers
        Filemanager.ClearEmbedded()

        ' stop the handler while we get all the languages
        RemoveHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged

        Me.ResetDefaults()

        '-----------------------------------------------
        ' Deal with the languages, terms and terminology bindings

        ' set the language that is chosen
        mFileManager.OntologyManager.LanguageCode = Me.ListLanguages.SelectedValue

        ' set the concept and description
        Dim a_term As RmTerm = mFileManager.OntologyManager.GetTerm( _
                mFileManager.Archetype.ConceptCode)
        If Not a_term Is Nothing Then
            Me.txtConceptInFull.Text = a_term.Text
            Me.TxtConceptDescription.Text = a_term.Description
            Me.txtConceptComment.Text = a_term.Comment
        End If

        If mFileManager.OntologyManager.NumberOfSpecialisations > 0 Then
            Dim tn As TreeNode
            Dim tnc As TreeNodeCollection
            Dim ct As CodeAndTerm()

            tnc = Me.tvSpecialisation.Nodes
            ct = OceanArchetypeEditor.Instance.GetSpecialisationChain(mFileManager.Archetype.ConceptCode, mFileManager)
            For i = 0 To ct.Length - 1
                tn = New TreeNode
                tn.Text = ct(i).Text
                tn.Tag = ct(i).Code
                tnc.Add(tn)
                tnc = tn.Nodes
            Next
            Me.tvSpecialisation.ExpandAll()
            Me.gbSpecialisation.Visible = True
        End If

        If mFileManager.Archetype.hasData Then
            'Dim sType As String

            'sType = mFileManager.Archetype.Definition.TypeName

            Select Case mFileManager.Archetype.RmEntity

                Case StructureType.ENTRY, StructureType.EVALUATION, StructureType.OBSERVATION, _
                    StructureType.INSTRUCTION, StructureType.ADMIN_ENTRY, StructureType.ACTION

                    'If mFileManager.Archetype.Definition.TypeName.StartsWith("ENTRY") Then
                    Dim rm As RmStructureCompound
                    ' allow restriction of subject of data
                    InitialiseRestrictedSet(RestrictedSet.TermSet.SubjectOfData)

                    If CType(mFileManager.Archetype.Definition, RmEntry).HasParticipationConstraint Then
                        Me.cbParticipation.Checked = True
                        mTabPageParticipation.chkProvider.Checked = CType(mFileManager.Archetype.Definition, RmEntry).ProviderIsMandatory
                        If CType(mFileManager.Archetype.Definition, RmEntry).HasOtherParticipations Then
                            mTabPageParticipation.OtherParticipations = CType(mFileManager.Archetype.Definition, RmEntry).OtherParticipations
                        End If
                    End If

                    ' deal with the various groups of information appropriate to the type
                    Select Case mFileManager.Archetype.RmEntity
                        Case StructureType.ENTRY ' "ENTRY"
                            For Each rm In CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.Data
                                        ProcessEventSeries(rm)
                                    Case StructureType.Protocol
                                        ProcessProtocol(rm, Me.TabDesign)
                                End Select
                            Next

                        Case StructureType.OBSERVATION ' "ENTRY.OBSERVATION"
                            For Each rm In CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.Data
                                        Dim rm_s As RmStructureCompound
                                        For Each rm_s In rm.Children
                                            'Select Case rm_s.TypeName
                                            Select Case rm_s.Type
                                                Case StructureType.History
                                                    ProcessEventSeries(rm_s)
                                                Case Else
                                                    SetUpDataStructure()
                                                    Me.ProcessDataStructure(rm_s)
                                            End Select
                                        Next

                                    Case StructureType.State
                                        Debug.Assert(rm.Children.Count > 0)

                                        Dim rm_1 As RmStructure
                                        rm_1 = rm.Children.items(0)

                                        If rm_1.Type = StructureType.History Then
                                            ProcessStateEventSeries(rm_1)
                                        Else
                                            ProcessState(rm_1)
                                        End If

                                    Case StructureType.Protocol
                                        Debug.Assert(rm.Children.Count > 0)
                                        ProcessProtocol(rm.Children.items(0), Me.TabDesign)
                                End Select
                            Next

                        Case StructureType.EVALUATION ' "ENTRY.EVALUATION"
                            For Each rm In CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data
                                Select Case rm.Type
                                    Case StructureType.Data
                                        SetUpDataStructure()
                                        If rm.Children.Count > 0 Then
                                            ProcessDataStructure(rm.Children.items(0))
                                        End If

                                        '  Case StructureType.State
                                        '     ProcessState(rm.Children.items(0))

                                    Case StructureType.Protocol
                                        ProcessProtocol(rm.Children.items(0), Me.TabDesign)
                                End Select
                            Next

                        Case StructureType.INSTRUCTION ' "ENTRY.INSTRUCTION"
                            SetUpInstruction()
                            mTabPageInstruction.ProcessInstruction(CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data)
                            For Each rmStruct As RmStructureCompound In CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data
                                If rmStruct.Type = StructureType.Protocol Then
                                    mTabPageInstruction.cbProtocol.Checked = True
                                    ProcessProtocol(rmStruct.Children.items(0), mTabPageInstruction.TabControlInstruction)
                                End If
                            Next

                        Case StructureType.ACTION
                            SetUpAction()
                            mTabPageAction.ProcessAction(CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data)
                            For Each rmStruct As RmStructureCompound In CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data
                                If rmStruct.Type = StructureType.Protocol Then
                                    mTabPageAction.cbProtocol.Checked = True
                                    ProcessProtocol(rmStruct.Children.items(0), mTabPageAction.TabControlAction)
                                End If
                            Next

                        Case StructureType.ADMIN_ENTRY
                            rm = CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.items(0)
                            SetUpDataStructure()
                            If rm.Children.Count > 0 Then
                                ProcessDataStructure(rm.Children.items(0))
                            End If

                    End Select

                    ' fill the subject of data if required
                    Dim cp As CodePhrase
                    cp = CType(mFileManager.Archetype.Definition, RmEntry).SubjectOfData.Relationship
                    If Not cp Is Nothing Then
                        If cp.Codes.Count > 0 Then
                            Me.mRestrictedSubject.AsCodePhrase = cp
                        End If
                    End If

                Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table

                    ProcessStructure(mFileManager.Archetype.Definition)

                Case StructureType.Cluster
                    ProcessCluster(mFileManager.Archetype.Definition)

                Case StructureType.Element
                    ProcessElement(mFileManager.Archetype.Definition)

                Case StructureType.SECTION
                    SetUpSection()
                    mTabPageSection.ProcessSection( _
                            mFileManager.Archetype.Definition)

                Case StructureType.COMPOSITION
                    InitialiseRestrictedSet(RestrictedSet.TermSet.Setting)

                    SetUpComposition()
                    mTabPageComposition.ProcessComposition( _
                        mFileManager.Archetype.Definition)
            End Select

        End If

        SetUpGUI(mFileManager.Archetype.RmEntity, False)

        AddHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged

        ' set the specific language if it is present e.g. en-US, en-AU
        If Me.ListLanguages.SelectedValue <> OceanArchetypeEditor.SpecificLanguageCode Then
            If mFileManager.OntologyManager.LanguageIsAvailable(OceanArchetypeEditor.SpecificLanguageCode) Then
                Me.ListLanguages.SelectedValue = OceanArchetypeEditor.SpecificLanguageCode
                Translate(OceanArchetypeEditor.SpecificLanguageCode)
            ElseIf mFileManager.OntologyManager.LanguageIsAvailable(OceanArchetypeEditor.DefaultLanguageCode) Then
                If Me.ListLanguages.SelectedValue <> OceanArchetypeEditor.DefaultLanguageCode Then
                    Me.ListLanguages.SelectedValue = OceanArchetypeEditor.DefaultLanguageCode
                    Translate(OceanArchetypeEditor.DefaultLanguageCode)
                End If
            Else
                Me.ListLanguages.SelectedValue = mFileManager.OntologyManager.PrimaryLanguageCode
                ChangeLanguage(Me.ListLanguages.SelectedValue)
            End If
        End If

        If Not mTermBindingPanel Is Nothing Then
            Me.mTermBindingPanel.PopulatePathTree()
        End If

        mFileManager.FileLoading = False

        Me.MenuFileSpecialise.Visible = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Sub InitialiseRestrictedSet(ByVal aRestriction As RestrictedSet.TermSet)
        If mRestrictedSubject Is Nothing Then
            mRestrictedSubject = New RestrictedSet()
            mRestrictedSubject.LocalFileManager = mFileManager
            Me.PanelConcept_1.Controls.Add(mRestrictedSubject)
            mRestrictedSubject.Dock = DockStyle.Left
        Else
            mRestrictedSubject.Reset()
        End If
        mRestrictedSubject.TermSetToRestrict = aRestriction
    End Sub

    Private Sub NewArchetype(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuFileNew.Click, MenuFileClose.Click

        If CheckOKtoClose() Then

            'reset the header

            If SetNewArchetypeName(sender Is MenuFileClose) = 2 Then  ' a new archetype
                'remove embedded filemanagers
                Filemanager.ClearEmbedded()

                Me.SuspendLayout()

                RemoveHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged
                ' stop auto updating of controls
                mFileManager.FileLoading = True
                Me.ResetDefaults()

                'reset the filename to null to force SaveAs
                mFileManager.FileName = ""

                SetUpGUI(ReferenceModel.ArchetypedClass, True)

                mFileManager.FileLoading = False
                mFileManager.FileEdited = True

                AddHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged

                Me.ResumeLayout()

                ' reset to event model
                ' clear all additions

                Me.ShowAsDraft = False

                Me.ToolBarNew.Visible = False
            End If
        End If

    End Sub

    Private Sub SaveArchetype(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFileSave.Click, MenuFileSaveAs.Click

        Dim s As String

        If sender Is MenuFileSaveAs Then  ' save as a different name or format
            'Remember the parser type
            Dim parserType As String = mFileManager.ParserType

            s = Filemanager.Master.FileName
            Filemanager.Master.FileName = ""

            If Not Filemanager.Master.SaveArchetype() Then
                Filemanager.Master.FileName = s
                Me.MenuFileSpecialise.Visible = True

            Else
                If mFileManager.ParserType <> parserType Then
                    'Saved in a different format so set the description to right format
                    Filemanager.Master.FileLoading = True
                    mTabPageDescription.Description = mFileManager.Archetype.Description
                    mTabPageDescription.TranslationDetails = mFileManager.Archetype.TranslationDetails
                    Filemanager.Master.FileLoading = False
                End If
                If Not Filemanager.HasFileToSave Then
                    'Hide save button on Toolbar
                    Filemanager.SetFileChangedToolBar(False)
                End If
            End If
        Else
            If Filemanager.SaveFiles(False) Then
                Me.MenuFileSpecialise.Visible = True
            End If
        End If
    End Sub

#End Region

#Region "Internal functions to initialise form variables and build tables"

    ' Initialisation and Data table initialisers
    Private Sub DesignerInitialiser()

        Me.TabMain.SelectedIndex = 0
        Me.TabMain.SelectedTab = Me.tpHeader
        Me.tpHeader.Selected = True
        Me.TabTerminology.SelectedIndex = 0
        Me.TabTerminology.SelectedTab = Me.tpTerms
        Me.tpTerms.Selected = True

        ' add the data page to the base collection to use as the root state incase
        mBaseTabPagesCollection.Add(Me.tpRootState, Me.tpRootState.Name)
        Me.TabDesign.TabPages.Remove(Me.tpRootState)
        mBaseTabPagesCollection.Add(Me.tpParticipation, Me.tpParticipation.Name)
        Me.TabDesign.TabPages.Remove(Me.tpParticipation)
        mBaseTabPagesCollection.Add(Me.tpSectionPage, Me.tpSectionPage.Name)
        Me.TabMain.TabPages.Remove(Me.tpSectionPage)
        mBaseTabPagesCollection.Add(Me.tpDesign, Me.tpDesign.Name)
        ' leave in place as default

        ' add the display printable rich text box
        mRichTextArchetype = New ArchetypeEditor.Specialised_VB_Classes.RichTextBoxPrintable
        Me.Panel3.Controls.Add(mRichTextArchetype)
        mRichTextArchetype.Dock = DockStyle.Fill

        ' add the term binding panel
        mTermBindingPanel = New TermBindingPanel
        Me.tpBindings.Controls.Add(mTermBindingPanel)
        mTermBindingPanel.Dock = DockStyle.Fill

        'Add description
        mTabPageDescription = New TabPageDescription
        Me.tpDescription.Controls.Add(mTabPageDescription)
        mTabPageDescription.Dock = DockStyle.Fill
        Me.mComponentsCollection.Add(mTabPageDescription)

    End Sub

    Private Sub AddLanguageToMenu(ByVal LanguageText As String)
        If LanguageText = "" Then
            'Fixme - raise error
            Return
        Else
            Dim MI As New MenuItem
            MI.Text = LanguageText
            If LanguageText = mFileManager.OntologyManager.LanguageText Then
                MI.Checked = True
            End If
            Me.MenuLanguageChange.MenuItems.Add(MI)
        End If

    End Sub

    Private Sub FileManager_IsFileDirtyChanged(ByVal e As FileManagerEventArgs)
        'Handles FileManager.IsFileDirtyChanged

        If e.IsFileDirty = True And Me.ShowMenuFileSave = False Then
            Me.ShowMenuFileSave = True
            Me.ShowToolbarSaveButton = True
            Me.ShowToolbarNewButton = True
            Me.ShowAsDraft = True

        ElseIf e.IsFileDirty = False And Me.ShowMenuFileSave = True Then
            Me.ShowMenuFileSave = False
            Me.ShowToolbarSaveButton = False
        End If
    End Sub
#End Region

#Region "Internal functions to populate the Designer with different terms and languages"

    Private Sub TranslateGUI(ByVal language As String)

        If OceanArchetypeEditor.IsLanguageRightToLeft(language) Then
            Me.RightToLeft = Windows.Forms.RightToLeft.Yes
        Else
            Me.RightToLeft = Windows.Forms.RightToLeft.Inherit
        End If

        'MenuItem labels
        Me.MenuFile.Text = Filemanager.GetOpenEhrTerm(43, Me.MenuFile.Text, language)
        Me.MenuFileClose.Text = Filemanager.GetOpenEhrTerm(184, Me.MenuFileClose.Text, language)
        Me.MenuFileNew.Text = Filemanager.GetOpenEhrTerm(151, Me.MenuFileNew.Text, language)
        Me.MenuFileExit.Text = Filemanager.GetOpenEhrTerm(63, Me.MenuFileExit.Text, language)
        Me.MenuFileOpen.Text = Filemanager.GetOpenEhrTerm(61, Me.MenuFileOpen.Text, language)
        Me.MenuFileOpenFromWeb.Text = Filemanager.GetOpenEhrTerm(651, Me.MenuFileOpenFromWeb.Text, language)
        Me.MenuFileSave.Text = Filemanager.GetOpenEhrTerm(183, Me.MenuFileSave.Text, language)
        Me.menuFileNewWindow.Text = Filemanager.GetOpenEhrTerm(595, Me.menuFileNewWindow.Text, language)
        Me.MenuFileSaveAs.Text = Filemanager.GetOpenEhrTerm(596, Me.MenuFileSaveAs.Text, language)
        Me.MenuFileSpecialise.Text = Filemanager.GetOpenEhrTerm(185, Me.MenuFileSpecialise.Text, language)
        Me.menuEdit.Text = Filemanager.GetOpenEhrTerm(592, Me.menuEdit.Text, language)
        Me.menuEditArchID.Text = Filemanager.GetOpenEhrTerm(632, Me.menuEditArchID.Text, language)
        Me.MenuViewConfig.Text = Filemanager.GetOpenEhrTerm(598, Me.MenuViewConfig.Text, language)
        Me.MenuHelpReport.Text = Filemanager.GetOpenEhrTerm(597, Me.MenuHelpReport.Text, language)
        Me.MenuHelp.Text = Filemanager.GetOpenEhrTerm(48, Me.MenuHelp.Text, language)
        Me.MenuHelpStart.Text = Filemanager.GetOpenEhrTerm(72, Me.MenuHelpStart.Text, language)
        Me.MenuHelpLicence.Text = Filemanager.GetOpenEhrTerm(73, Me.MenuHelpLicence.Text, language)
        Me.MenuTerminology.Text = Filemanager.GetOpenEhrTerm(47, Me.MenuTerminology.Text, language)
        Me.MenuHelpOcean.Text = Filemanager.GetOpenEhrTerm(74, Me.MenuHelpOcean.Text, language)
        Me.MenuHelpOceanEditor.Text = Filemanager.GetOpenEhrTerm(75, Me.MenuHelpOceanEditor.Text, language)
        Me.MenuLanguage.Text = Filemanager.GetOpenEhrTerm(46, Me.MenuLanguage.Text, language)
        Me.MenuLanguageAdd.Text = Filemanager.GetOpenEhrTerm(68, Me.MenuLanguageAdd.Text, language)
        Me.MenuLanguageChange.Text = Filemanager.GetOpenEhrTerm(69, Me.MenuLanguageChange.Text, language)
        Me.MenuLanguageAvailable.Text = Filemanager.GetOpenEhrTerm(67, Me.MenuLanguageAvailable.Text, language)
        Me.MenuPublish.Text = Filemanager.GetOpenEhrTerm(45, Me.MenuPublish.Text, language)
        Me.MenuPublishFinalise.Text = Filemanager.GetOpenEhrTerm(66, Me.MenuPublishFinalise.Text, language)
        Me.MenuPublishPack.Text = Filemanager.GetOpenEhrTerm(65, Me.MenuPublishPack.Text, language)
        Me.MenuTerminologyAdd.Text = Filemanager.GetOpenEhrTerm(71, Me.MenuTerminologyAdd.Text, language)
        Me.MenuTerminologyAvailable.Text = Filemanager.GetOpenEhrTerm(70, Me.MenuTerminologyAvailable.Text, language)

        'Front panel of designer
        Me.lblArchetypeFileName.Text = Filemanager.GetOpenEhrTerm(57, Me.lblArchetypeFileName.Text, language)
        Me.lblArchetypeName.Text = Filemanager.GetOpenEhrTerm(58, Me.lblArchetypeName.Text, language)
        Me.lblConcept.Text = Filemanager.GetOpenEhrTerm(54, Me.lblConcept.Text, language)
        Me.tpConceptDescription.Text = Filemanager.GetOpenEhrTerm(113, Me.tpConceptDescription.Text, language)
        Me.tpConceptComment.Text = Filemanager.GetOpenEhrTerm(652, Me.tpConceptComment.Text, language)
        Me.gbSpecialisation.Text = Filemanager.GetOpenEhrTerm(186, Me.gbSpecialisation.Text, language)
        Me.butLinks.Text = Filemanager.GetOpenEhrTerm(659, Me.butLinks.Text, language)

        'Entry tab on designer
        Me.cbProtocol.Text = Filemanager.GetOpenEhrTerm(78, Me.cbProtocol.Text, language)
        Me.cbParticipation.Text = Filemanager.GetOpenEhrTerm(654, Me.cbParticipation.Text, language)
        Me.cbPersonState.Text = Filemanager.GetOpenEhrTerm(79, Me.cbPersonState.Text, language)
        Me.chkEventSeries.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(80, Me.chkEventSeries.Text, language), Filemanager.GetOpenEhrTerm(81, Me.chkEventSeries.Text, language))
        Me.cbStructurePersonState.Text = Filemanager.GetOpenEhrTerm(82, Me.cbStructurePersonState.Text, language)
        Me.cbProtocol.Text = Filemanager.GetOpenEhrTerm(78, Me.cbProtocol.Text, language)

        'Display tab on Designer
        Me.butSaveFile.Text = Filemanager.GetOpenEhrTerm(103, Me.butSaveFile.Text, language)
        Me.butPrint.Text = Filemanager.GetOpenEhrTerm(520, Me.butPrint.Text, language)

        'Interface tab
        Me.cbMandatory.Text = Filemanager.GetOpenEhrTerm(446, Me.cbMandatory.Text, language)

        'Terminology tab on Designer
        Me.lblPrimaryLanguageText.Text = Filemanager.GetOpenEhrTerm(187, Me.lblPrimaryLanguageText.Text, language)
        Me.lblAvailableLanguages.Text = Filemanager.GetOpenEhrTerm(67, Me.lblAvailableLanguages.Text, language)
        Me.lblAvailableTerminologies.Text = Filemanager.GetOpenEhrTerm(70, Me.lblAvailableLanguages.Text, language)
        Me.DataGridTerminologies.CaptionText = Filemanager.GetOpenEhrTerm(70, Me.DataGridTerminologies.CaptionText, language)

        Me.DataGridDefinitions.CaptionText = Filemanager.GetOpenEhrTerm(89, Me.DataGridDefinitions.CaptionText, language)
        Me.DataGridConstraintDefinitions.CaptionText = Filemanager.GetOpenEhrTerm(623, Me.DataGridConstraintDefinitions.CaptionText, language)
        Me.lblConstraintStatements.Text = Filemanager.GetOpenEhrTerm(93, lblConstraintStatements.Text, language)
        Me.butLookUpConstraint.Text = Filemanager.GetOpenEhrTerm(99, butLookUpConstraint.Text, language)

        'ColumnStyleHeadings
        DataGridTextBoxColumn1.HeaderText = AE_Constants.Instance.Terminology
        DataGridTextBoxColumn2.HeaderText = Filemanager.GetOpenEhrTerm(90, DataGridTextBoxColumn2.HeaderText, language) ' Code
        DataGridTextBoxColumn3.HeaderText = Filemanager.GetOpenEhrTerm(91, DataGridTextBoxColumn3.HeaderText, language) ' Text
        DataGridTextBoxColumn4.HeaderText = Filemanager.GetOpenEhrTerm(113, DataGridTextBoxColumn4.HeaderText, language) 'Description
        DataGridTextBoxColumn5.HeaderText = DataGridTextBoxColumn4.HeaderText
        DataGridTextBoxColumn6.HeaderText = DataGridTextBoxColumn2.HeaderText
        DataGridTextBoxColumn7.HeaderText = DataGridTextBoxColumn3.HeaderText
        DataGridTextBoxColumn8.HeaderText = DataGridTextBoxColumn4.HeaderText
        DataGridTextBoxColumn9.HeaderText = Filemanager.GetOpenEhrTerm(96, DataGridTextBoxColumn9.HeaderText, language) 'Path
        DataGridTextBoxColumn11.HeaderText = DataGridTextBoxColumn2.HeaderText
        DataGridTextBoxColumn12.HeaderText = Filemanager.GetOpenEhrTerm(97, DataGridTextBoxColumn12.HeaderText, language) 'Release
        DataGridTextBoxColumn13.HeaderText = Filemanager.GetOpenEhrTerm(622, DataGridTextBoxColumn13.HeaderText, language) 'Criteria

        'Constraint data grid
        Me.DataGridConstraintStatements.Columns(0).HeaderText = AE_Constants.Instance.Terminology
        Me.DataGridConstraintStatements.Columns(2).HeaderText = Filemanager.GetOpenEhrTerm(624, Me.DataGridConstraintStatements.Columns(1).HeaderText, language) 'Query name
        Me.DataGridConstraintStatements.Columns(3).HeaderText = Filemanager.GetOpenEhrTerm(97, Me.DataGridConstraintStatements.Columns(2).HeaderText, language) 'Release

        'TabControl headings
        Me.tpHeader.Title = Filemanager.GetOpenEhrTerm(76, Me.tpHeader.Title, language)
        Me.tpDesign.Title = Filemanager.GetOpenEhrTerm(647, Me.tpDesign.Title, language)
        Me.tpParticipation.Title = Filemanager.GetOpenEhrTerm(654, Me.tpParticipation.Title, language)
        Me.tpSectionPage.Title = Filemanager.GetOpenEhrTerm(647, Me.tpSectionPage.Title, language)
        Me.tpTerminology.Title = Filemanager.GetOpenEhrTerm(47, Me.tpTerminology.Title, language)
        Me.tpText.Title = Filemanager.GetOpenEhrTerm(83, Me.tpText.Title, language)
        Me.tpInterface.Title = Filemanager.GetOpenEhrTerm(84, Me.tpInterface.Title, language)
        Me.tpDescription.Title = Filemanager.GetOpenEhrTerm(113, Me.tpDescription.Title, language)
        Me.tpTerms.Title = Filemanager.GetOpenEhrTerm(86, Me.tpTerms.Title, language)
        Me.tpBindings.Title = Filemanager.GetOpenEhrTerm(93, Me.tpTerms.Title, language)
        Me.tpConstraints.Title = Filemanager.GetOpenEhrTerm(87, Me.tpConstraints.Title, language)
        Me.tpLanguages.Title = Filemanager.GetOpenEhrTerm(88, Me.tpLanguages.Title, language)
        Me.tpData.Title = Filemanager.GetOpenEhrTerm(80, Me.tpData.Title, language)
        Me.tpRootState.Title = Filemanager.GetOpenEhrTerm(177, Me.tpRootState.Title, language)
        Me.tpRootStateEventSeries.Title = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(177, Me.chkEventSeries.Text, language), Filemanager.GetOpenEhrTerm(81, Me.chkEventSeries.Text, language))
        Me.tpRootStateStructure.Title = Filemanager.GetOpenEhrTerm(177, Me.tpRootStateStructure.Title, language)
        Me.tpRootStateStructure.Title = Filemanager.GetOpenEhrTerm(446, Me.cbMandatory.Text, language)


    End Sub

    Private Sub TranslateSpecialisationNodes(ByVal tnc As TreeNodeCollection)
        Dim tNode As TreeNode
        Dim a_Term As RmTerm

        For Each tNode In tnc
            TranslateSpecialisationNodes(tNode.Nodes)
            a_Term = mFileManager.OntologyManager.GetTerm(tNode.Tag)
            tNode.Text = a_Term.Text
        Next
    End Sub

    Private Sub Translate(Optional ByVal LanguageCode As String = "")

        Dim obj As Object
        Dim a_Term As RmTerm

        ' file loading so no updates
        mFileManager.FileLoading = True
        ' set the language
        If LanguageCode <> "" Then
            Dim MI As MenuItem

            mFileManager.OntologyManager.LanguageCode = LanguageCode
            For Each MI In Me.MenuLanguageChange.MenuItems
                If MI.Text = mFileManager.OntologyManager.LanguageText Then
                    MI.Checked = True
                Else
                    MI.Checked = False
                End If
            Next

        End If
        ' Concept and description fields on header and the form text

        a_Term = mFileManager.OntologyManager.GetTerm(mFileManager.Archetype.ConceptCode)
        Me.txtConceptInFull.Text = a_Term.Text
        Me.Text = AE_Constants.Instance.MessageBoxCaption & " [" & a_Term.Text & "]"
        Me.TxtConceptDescription.Text = a_Term.Description
        Me.txtConceptComment.Text = a_Term.Comment

        'and in the specialisation chain
        If Me.tvSpecialisation.GetNodeCount(False) > 0 Then
            TranslateSpecialisationNodes(Me.tvSpecialisation.Nodes)
        End If

        ' and the subject of care
        If Not mRestrictedSubject Is Nothing AndAlso mRestrictedSubject.HasRestriction Then
            mRestrictedSubject.Translate()
        End If

        mFileManager.FileLoading = False

        For Each obj In mComponentsCollection
            ' each component must have a populate terms method to enable translation
            obj.Translate()
        Next

        'Translate descriptions
        mTabPageDescription.Translate()
        mTermBindingPanel.PopulatePathTree()
        RichTextBoxDescription.Rtf = mTabPageDescription.AsRtfString()
        RichTextBoxUnicode.ProcessRichEditControl(RichTextBoxDescription, mFileManager, mTabPageDescription) 'JAR: 13APR07, EDT-32 Support unicode

        Select Case Me.TabMain.SelectedTab.Name
            Case "tpInterface"
                BuildInterface()
                Me.cbMandatory.BringToFront()
            Case "tpText"
                WriteRichText()
        End Select

    End Sub

    Private Sub BindTables()
        Dim CM As CurrencyManager

        'Me.mDataViewTermBindings = New DataView(mFileManager.OntologyManager.TermBindingsTable)
        'Me.mDataViewTermBindings.AllowNew = False

        Me.mDataViewConstraintBindings = New DataView(mFileManager.OntologyManager.ConstraintBindingsTable)
        Me.mDataViewConstraintBindings.RowFilter = "ID ='ZZZZ'" ' do not show any for the moment
        Me.mDataViewConstraintBindings.AllowNew = False


        ' ensure that the table is bound to the languages list
        Me.ListLanguages.DataSource = mFileManager.OntologyManager.LanguagesTable
        Me.ListLanguages.DisplayMember = "Language"
        Me.ListLanguages.ValueMember = "id"

        ' bind the Term definitions table

        Me.DataGridDefinitions.DataSource = mFileManager.OntologyManager.LanguagesTable
        Me.DataGridDefinitions.DataMember = "LanguageTerms"
        Me.DataGridDefinitions.TableStyles(0).MappingName = "TermDefinitions"
        Me.DataGridDefinitions.TableStyles(0).GridColumnStyles(0).MappingName = "Code"
        Me.DataGridDefinitions.TableStyles(0).GridColumnStyles(1).MappingName = "Text"
        Me.DataGridDefinitions.TableStyles(0).GridColumnStyles(2).MappingName = "Description"

        ' only way to control the dataview behind a related grid
        CM = Me.BindingContext(DataGridDefinitions.DataSource, DataGridDefinitions.DataMember)
        CType(CM.List, DataView).AllowNew = False
        CType(CM.List, DataView).AllowDelete = False

        ' bind the Constraint definitions table
        Me.DataGridConstraintDefinitions.DataSource = mFileManager.OntologyManager.LanguagesTable
        Me.DataGridConstraintDefinitions.DataMember = "LanguageConstraints"
        Me.DataGridConstraintDefinitions.TableStyles(0).MappingName = "ConstraintDefinitions"
        Me.DataGridConstraintDefinitions.TableStyles(0).GridColumnStyles(0).MappingName = "Code"
        Me.DataGridConstraintDefinitions.TableStyles(0).GridColumnStyles(1).MappingName = "Text"
        Me.DataGridConstraintDefinitions.TableStyles(0).GridColumnStyles(2).MappingName = "Description"

        ' only way to edit the dataview behind a related grid
        CM = Me.BindingContext(DataGridConstraintDefinitions.DataSource, DataGridConstraintDefinitions.DataMember)
        CType(CM.List, DataView).AllowNew = False
        CType(CM.List, DataView).AllowDelete = False

        'bind the terminology combobox
        Me.DataGridTerminologies.DataSource = mFileManager.OntologyManager.TerminologiesTable

        Me.terminology.DataSource = OceanArchetypeEditor.Instance.MakeTerminologyDataTable
        Me.terminology.DisplayMember = "Text"
        Me.terminology.ValueMember = "Code"


        Me.DataGridConstraintStatements.DataSource = Me.mDataViewConstraintBindings

    End Sub

    Public Sub WriteRichText()
        Dim text As New IO.StringWriter
        Dim commaspace As Char() = {" ", ","}

        text.WriteLine("{\rtf1\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 Tahoma;}{\f1\fnil\fcharset2 Symbol;}}")
        text.WriteLine("{\colortbl ;\red0\green0\blue255;\red0\green255\blue0;}")
        text.WriteLine("\viewkind4\uc1\pard\tx2840\tx5112\lang3081\f0\fs20")
        text.WriteLine("\cf1 Header\cf0\par")
        'text.WriteLine("   Concept: " & Me.txtConceptInFull.Text & "\par")        
        text.WriteLine("   Concept: " & RichTextBoxUnicode.CreateRichTextBoxTag(mFileManager.Archetype.ConceptCode, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_TEXT) & "\par") 'JAR: 13APR07, EDT-32 Support unicode

        text.WriteLine("\par")
        text.WriteLine("\cf1 Definition\cf0\par")

        If Not mFileManager.Archetype.Definition Is Nothing Then
            text.WriteLine("\cf2    " & mFileManager.Archetype.Definition.Type.ToString & "\cf0\par")
        End If

        Select Case mFileManager.Archetype.RmEntity
            Case StructureType.SECTION
                text.WriteLine("\par")
                ' add subject relationship constraint here
                text.WriteLine("\cf1   DATA\cf0  = \{\par")
                mTabPageSection.ToRichText(text, 2)
                text.WriteLine("\par")
                text.WriteLine("  \} -- end Data\par")
                text.WriteLine("\par")

            Case StructureType.COMPOSITION

            Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION, StructureType.ACTION, StructureType.ADMIN_ENTRY
                If Not mRestrictedSubject Is Nothing AndAlso mRestrictedSubject.HasRestriction Then
                    text.WriteLine("       Subject relationship restricted to " & mRestrictedSubject.CSV & "\par")
                End If
                text.WriteLine("\par")
                ' add subject relationship constraint here
                text.WriteLine("\cf1   DATA\cf0  = \{\par")


                If mFileManager.Archetype.RmEntity = StructureType.INSTRUCTION Then
                    If Not mTabPageInstruction Is Nothing Then
                        mTabPageInstruction.toRichText(text, 2)
                    End If
                ElseIf mFileManager.Archetype.RmEntity = StructureType.ACTION Then
                    If Not mTabPageAction Is Nothing Then
                        mTabPageAction.toRichText(text, 2)
                    End If
                Else
                    If Not mTabPageDataStructure Is Nothing Then
                        mTabPageDataStructure.toRichText(text, 2)
                    End If
                End If

                If Not mTabPageDataStateStructure Is Nothing Then
                    text.WriteLine("\cf1      STATE\cf0  = \{\par")
                    mTabPageDataStateStructure.toRichText(text, 3)
                    text.WriteLine("     \} -- end State\par")
                    text.WriteLine("\par")
                End If

                If Not mTabPageDataEventSeries Is Nothing Then
                    mTabPageDataEventSeries.ToRichText(text, 3)
                End If

                text.WriteLine("\par")
                text.WriteLine("  \} -- end Data\par")
                text.WriteLine("\par")

                If Not mTabPageProtocolStructure Is Nothing Then
                    text.WriteLine("\cf1   PROTOCOL\cf0  = \{\par")
                    mTabPageProtocolStructure.toRichText(text, 2)
                    text.WriteLine("     \} -- end Protocol\par")
                    text.WriteLine("\par")
                End If


                If Not mTabPageStateStructure Is Nothing Then
                    text.WriteLine("cf1   STATE\cf0  = \{\par")
                    mTabPageStateStructure.toRichText(text, 2)
                    If Not mTabPageStateEventSeries Is Nothing Then
                        mTabPageStateEventSeries.ToRichText(text, 2)
                    End If
                    text.WriteLine("     \} -- end State\par")
                    text.WriteLine("\par")
                End If

                text.WriteLine("\par")

            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                If Not mTabPageDataStructure Is Nothing Then
                    mTabPageDataStructure.toRichText(text, 2)
                End If

            Case StructureType.Cluster, StructureType.Element
                If Not mTabPageDataStructure Is Nothing Then
                    mTabPageDataStructure.toRichText(text, 2)
                End If
        End Select

        mRichTextArchetype.Rtf = text.ToString
        RichTextBoxUnicode.ProcessRichEditControl(mRichTextArchetype, mFileManager, mTabPageDescription)
    End Sub

    Public Sub WriteToHTML(ByVal filename As String)
        Dim text As IO.StreamWriter
        Dim commaspace As Char() = {" ", ","}

        text = IO.File.CreateText(Application.StartupPath & filename)

        text.WriteLine("<HTML>")
        text.WriteLine("<HEAD>")
        text.WriteLine("<meta http-equiv=""Content-Language"" content=""" & mFileManager.OntologyManager.LanguageCode & """>")
        text.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">")
        text.WriteLine("<title>" & Me.txtConceptInFull.Text & "</title>")
        text.WriteLine("</HEAD>")

        text.WriteLine("<BODY>")
        text.WriteLine("<table border=""0"" cellpadding=""2"" width=""100%"">")
        text.WriteLine("<tr>")
        text.WriteLine("<td width=""100%"" bgcolor=""#000080"">")
        text.WriteLine("<h1 align=""center""><font color=""#FFFFFF"">" & Me.txtConceptInFull.Text & "</font></h1>")
        text.WriteLine("</td>")
        text.WriteLine("</tr>")
        text.WriteLine("</table>")
        text.WriteLine("<p><i>Entity</i>: " & mFileManager.Archetype.Archetype_ID.ReferenceModelEntity.ToString & "</p>")
        text.WriteLine("<table border=""1"" cellpadding=""3"" width=""100%"">")

        Dim width As String = "50"
        If OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml() Then
            width = "33"
        End If
        text.WriteLine("<tr>")
        text.WriteLine(String.Format("<td width=""{0}%""><h4>Concept description:</h4></td>", width))
        text.WriteLine(String.Format("<td width=""{0}%""><h4>Identification:</h4></td>", width))
        If OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml() Then
            text.WriteLine(String.Format("<td width=""{0}%""><h4>Comments:</h4></td>", width))
        End If
        text.WriteLine("</tr>")
        text.WriteLine("<tr>")
        text.WriteLine(String.Format("<td width=""{0}%"">{1}</td>", width, TxtConceptDescription.Text))
        text.WriteLine(String.Format("<td width=""{0}%""><i>Id</i>: {1}<br><i>Reference model</i>: {2}</td>", _
            width, mFileManager.Archetype.Archetype_ID.ToString(), mFileManager.Archetype.Archetype_ID.Reference_Model.ToString))

        Dim commentString As String = "&nbsp;"

        If OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml() Then
            text.WriteLine(String.Format("<td width=""{0}%"">{1}</td>", width, CStr(IIf(Me.txtConceptComment.Text <> String.Empty, Me.txtConceptComment.Text, commentString))))
        End If
        text.WriteLine("</tr>")
        text.WriteLine("</table>")


        text.WriteLine("<table border=""1"" cellpadding=""3"" style=""background-color: rgb(229, 229, 229); width:100%;"" >")
        text.WriteLine("<tr>")
        text.WriteLine(String.Format("<td width=""33%""><h4>{0}</h4></td>", Filemanager.GetOpenEhrTerm(585, "Purpose")))
        text.WriteLine(String.Format("<td width=""33%""><h4>{0}</h4></td>", Filemanager.GetOpenEhrTerm(582, "Use")))
        text.WriteLine(String.Format("<td width=""33%""><h4>{0}</h4></td>", Filemanager.GetOpenEhrTerm(583, "Misuse")))
        text.WriteLine(String.Format("<td width=""33%""><h4>{0}</h4></td>", "References")) 'JAR: 24MAY2007, EDT-30 Add field for References
        text.WriteLine("</tr>")
        text.WriteLine("<tr>")

        text.WriteLine(String.Format("<td width=""33%"">{0}</td>", CStr(IIf(Me.mTabPageDescription.txtPurpose.Text <> String.Empty, Me.mTabPageDescription.txtPurpose.Text, commentString))))
        text.WriteLine(String.Format("<td width=""33%"">{0}</td>", CStr(IIf(Me.mTabPageDescription.txtUse.Text <> String.Empty, Me.mTabPageDescription.txtUse.Text, commentString))))
        text.WriteLine(String.Format("<td width=""33%"">{0}</td>", CStr(IIf(Me.mTabPageDescription.txtMisuse.Text <> String.Empty, Me.mTabPageDescription.txtMisuse.Text, commentString))))
        text.WriteLine(String.Format("<td width=""33%"">{0}</td>", CStr(IIf(Me.mTabPageDescription.txtReferences.Text <> String.Empty, Me.mTabPageDescription.txtReferences.Text, commentString)))) 'JAR: 24MAY2007, EDT-30 Add field for References
        text.WriteLine("</tr>")
        text.WriteLine("</table>")

        Select Case mFileManager.Archetype.RmEntity
            'Case StructureType.SECTION
            '    mTabPageSection.toHTML()

            'Case StructureType.COMPOSITION
            '    mTabPageComposition.toHTML()

            Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.INSTRUCTION

                text.WriteLine("<hr>")
                text.WriteLine("<table border=""0"" cellpadding=""2"" width=""100%"">")
                text.WriteLine("<tr>")
                text.WriteLine("<td width=""100%"" bgcolor=""#FFFF53"">")
                text.WriteLine("<h2>" & Filemanager.GetOpenEhrTerm(80, "Data") & "</h2>")
                text.WriteLine("</td>")
                text.WriteLine("</tr>")
                text.WriteLine("</table>")

                'If mFileManager.Archetype.RmEntity = StructureType.INSTRUCTION Then
                '    If Not mTabPageInstruction Is Nothing Then
                '        mTabPageInstruction.toRichText(text, 2)
                '    End If
                'Else

                If Not mTabPageDataStructure Is Nothing Then
                    mTabPageDataStructure.toHTML(text, "#FFFF53")
                End If

                'End If

                If Not mTabPageDataStateStructure Is Nothing Then
                    text.WriteLine("<table border=""0"" cellpadding=""2"" width=""100%"">")
                    text.WriteLine("<tr>")
                    text.WriteLine("<td width=""100%"" bgcolor=""#9DAAFF"">")
                    text.WriteLine("<h2>" & Filemanager.GetOpenEhrTerm(177, "State") & "</h2>")
                    text.WriteLine("</td>")
                    text.WriteLine("</tr>")
                    text.WriteLine("</table>")
                    mTabPageDataStateStructure.toHTML(text, "#9DAAFF")
                End If

                If Not mTabPageDataEventSeries Is Nothing Then
                    text.WriteLine("<table border=""0"" cellpadding=""2"" width=""100%"">")
                    text.WriteLine("<tr>")
                    text.WriteLine("<td width=""100%"" bgcolor=""#AAFE92"">")
                    text.WriteLine("<h2>" & Filemanager.GetOpenEhrTerm(81, "Event Series") & "</h2>")
                    text.WriteLine("</td>")
                    text.WriteLine("</tr>")
                    text.WriteLine("</table>")
                    mTabPageDataEventSeries.ToHTML(text, "#AAFE92")
                End If

                If Not mRestrictedSubject Is Nothing AndAlso mRestrictedSubject.HasRestriction Then
                    text.WriteLine("<br>Subject relationship restricted to " & mRestrictedSubject.CSV & "<br>")
                End If

                If Not mTabPageProtocolStructure Is Nothing Then
                    text.WriteLine("<table border=""0"" cellpadding=""2"" width=""100%"">")
                    text.WriteLine("<tr>")
                    text.WriteLine("<td width=""100%"" bgcolor=""#FF717B"">")
                    text.WriteLine("<h2>" & Filemanager.GetOpenEhrTerm(78, "Protocol") & "</h2>")
                    text.WriteLine("</td>")
                    text.WriteLine("</tr>")
                    text.WriteLine("</table>")
                    mTabPageProtocolStructure.toHTML(text, "#FF717B")
                End If


                'If Not mTabPageStateStructure Is Nothing Then
                '    text.WriteLine("cf1   STATE\cf0  = \{\par")
                '    mTabPageStateStructure.toRichText(text, 2)
                '    If Not mTabPageStateEventSeries Is Nothing Then
                '        mTabPageStateEventSeries.ToRichText(text, 2)
                '    End If
                '    text.WriteLine("     \} -- end State\par")
                '    text.WriteLine("\par")
                'End If

                'text.WriteLine("\par")

            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                If Not mTabPageDataStructure Is Nothing Then
                    mTabPageDataStructure.toHTML(text)
                End If

            Case StructureType.Cluster, StructureType.Element
                If Not mTabPageDataStructure Is Nothing Then
                    mTabPageDataStructure.toHTML(text)
                End If

        End Select

        text.WriteLine("</BODY>")

        text.WriteLine("</HTML>")

        text.Flush()
        text.Close()

    End Sub

#End Region

#Region "Choose form functions"

    Friend Function ChooseLanguage() As Boolean
        Dim frm As New Choose
        Dim lang As String
        Dim i As Integer
        Dim Languages As DataRow()
        Dim New_row As DataRow

        ' add the language codes - FIXME - from a file in future
        frm.Set_Single()
        frm.PrepareDataTable_for_List(1)
        Languages = mFileManager.OntologyManager.GetLanguageList

        ' the first language is "aaaa" and is set to show all in the openEHR maintenance
        For i = 0 To Languages.Length - 1
            New_row = frm.DTab_1.NewRow()
            New_row("Code") = Languages(i).Item(0)
            New_row("Text") = Languages(i).Item(1)
            frm.DTab_1.Rows.Add(New_row)
        Next

        frm.ListChoose.DataSource = frm.DTab_1
        frm.DTab_1.DefaultView.Sort = "Text"
        frm.ListChoose.DisplayMember = "Text"
        frm.ListChoose.ValueMember = "Code"

        frm.ShowDialog(Me)

        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            ' check it is not a language added previously

            lang = frm.ListChoose.SelectedValue

            If Not mFileManager.OntologyManager.LanguagesTable.Rows.Find(lang) Is Nothing Then
                Return False ' no need to add anything
            End If


            ' stop the handler while we get all the languages
            RemoveHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged

            ' check with user they want to add a languge
            If (MessageBox.Show(AE_Constants.Instance.NewLanguage, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = _
                Windows.Forms.DialogResult.OK) Then
                mFileManager.OntologyManager.AddLanguage(lang, frm.ListChoose.Text)
                Me.AddLanguageToMenu(frm.ListChoose.Text)
            Else
                MessageBox.Show(AE_Constants.Instance.Language_addition_cancelled, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                AddHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged
                Return False
            End If

            AddHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged
            Return True
        Else
            Return False
        End If

    End Function

    'Friend Function ChooseTerminology() As Boolean
    '    Dim frm As New Choose
    '    Dim term, Description As String
    '    Dim i As Integer
    '    Dim Terminologies As DataRow()
    '    Dim new_row As DataRow

    '    ' add the language codes - FIXME - from a file in future
    '    frm.Set_Single()
    '    frm.PrepareDataTable_for_List(1)
    '    Terminologies = mFileManager.OntologyManager.GetTerminologyIdentifiers
    '    frm.DTab_1.DefaultView.Sort = "Text"
    '    For i = 0 To Terminologies.Length - 1
    '        new_row = frm.DTab_1.NewRow()
    '        new_row("Code") = Terminologies(i).Item(0)
    '        new_row("Text") = Terminologies(i).Item(1)
    '        frm.DTab_1.Rows.Add(new_row)
    '    Next

    '    frm.ListChoose.DataSource = frm.DTab_1
    '    frm.ListChoose.DisplayMember = "Text"
    '    frm.ListChoose.ValueMember = "Code"

    '    If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        ' check it is not a language added previously
    '        term = frm.ListChoose.SelectedValue
    '        Description = frm.ListChoose.Text
    '        If Not mFileManager.OntologyManager.TerminologiesTable.Select("Terminology = '" & term & "'").Length = 0 Then
    '            Beep()
    '            Return False
    '        End If

    '        ' there is already a language in the archetype
    '        If (MessageBox.Show(AE_Constants.Instance.NewTerminology & Description, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK) Then
    '            ' add to the terminologies
    '            mFileManager.OntologyManager.AddTerminology(term, Description)
    '        End If
    '    Else
    '        Return False
    '    End If
    '    Return True
    'End Function




#End Region

#Region "Public Properties, Methods and Types"

    Public Property ShowToolbarSaveButton() As Boolean
        Get
            Return Me.ToolBarSave.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.ToolBarSave.Visible = Value
        End Set
    End Property

    Dim m_ArchetypeToOpen As String
    'Allows setting archetype to open on load if required
    Public Property ArchetypeToOpen() As String
        Get
            Return m_ArchetypeToOpen
        End Get
        Set(ByVal Value As String)
            m_ArchetypeToOpen = Value
        End Set
    End Property

    Public Property ShowToolbarNewButton() As Boolean
        Get
            Return Me.ToolBarNew.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.ToolBarNew.Visible = Value
        End Set
    End Property
    Public Property ShowMenuFileNew() As Boolean
        Get
            Return Me.MenuFileNew.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.MenuFileNew.Visible = Value
        End Set
    End Property
    Public Property ShowMenuFileSave() As Boolean
        Get
            Return Me.MenuFileSave.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.MenuFileSave.Visible = Value
        End Set
    End Property
    Public Property ShowAsDraft() As Boolean
        Get
            Return Me.lblLifecycle.Visible
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                If mFileManager.Archetype.LifeCycle Is Nothing Then
                    mFileManager.Archetype.LifeCycle = "draft"
                    Me.lblLifecycle.Text = "draft"
                    Me.lblLifecycle.Visible = True
                Else
                    'ToDo - need to do revision number
                    'Debug.Assert(False)
                End If
            End If

        End Set
    End Property

    Public Sub AutoSave()
        EmergencySave(Me, New EventArgs)
    End Sub

    Private Sub EmergencySave(ByVal sender As Object, ByVal e As EventArgs) Handles mAutoSaveTimer.Tick

        If OceanArchetypeEditor.Instance.Options.AutosaveInterval = 0 Then
            Me.mAutoSaveTimer.Enabled = False
        Else
            If Not Me.mAutoSaveTimer.Enabled Or Me.mAutoSaveTimer.Interval <> OceanArchetypeEditor.Instance.Options.AutosaveInterval * 60000 Then
                Me.mAutoSaveTimer.Enabled = True
                Me.mAutoSaveTimer.Interval = OceanArchetypeEditor.Instance.Options.AutosaveInterval * 60000
            End If
            Filemanager.AutoFileSave()
        End If
    End Sub

    Public Sub ResetDefaults()
        ' show the front page
        Me.TabMain.SelectedIndex = 0
        Me.TabMain.SelectedTab = Me.tpHeader

        ' set the front page to default state
        Me.lblArchetypeName.Text = ""
        If Not mRestrictedSubject Is Nothing Then
            mRestrictedSubject.Reset() 'clears list and makes invisible
        End If
        Me.gbSpecialisation.Visible = False
        Me.tvSpecialisation.Nodes.Clear()
        Me.txtConceptInFull.Text = ""
        Me.RichTextBoxDescription.Text = ""
        Me.TxtConceptDescription.Text = ""
        Me.txtConceptComment.Text = ""
        Me.tabComment.SelectedIndex = 0
        
        'set the other pages
        Me.cbStructurePersonState.Checked = False
        Me.cbStructurePersonState.Visible = False
        Me.cbPersonState.Checked = False
        Me.cbPersonState.Visible = False
        Me.chkEventSeries.Checked = False
        Me.chkEventSeries.Visible = False
        Me.cbProtocol.Checked = False
        Me.cbProtocol.Visible = False
        Me.cbStructurePersonState.Checked = False
        Me.cbStructurePersonState.Visible = False

        'set the display panel to nothing
        Me.mRichTextArchetype.Clear()

        'Set the participation to false
        Me.cbParticipation.Checked = False

        'Get rid of the languages from menu
        Me.MenuLanguageChange.MenuItems.Clear()

        ' Now set the interface elements

        While Me.TabDesign.TabPages.Count > 1
            ' get rid of the tab pages except Data
            Me.TabDesign.TabPages.RemoveAt(1)
        End While

        While Me.TabStructure.TabPages.Count > 1
            ' get rid of the tab pages except Structure
            Me.TabStructure.TabPages.RemoveAt(1)
        End While

        ' clear any controls on the tab pages to ensure restart

        Me.tpDataStructure.Controls.Clear()
        Me.tpRootStateStructure.Controls.Clear()
        Me.tpRootStateEventSeries.Controls.Clear()

        'clear any added pages and controls
        mTabPagesCollection = New Collections.Hashtable  'clear all tab pages
        mComponentsCollection = New Collection    ' clear all the active components

        ' kill all the components
        mTabPageDataEventSeries = Nothing
        mTabPageStateEventSeries = Nothing
        mTabPageDataStructure = Nothing
        mTabPageDataStateStructure = Nothing
        mTabPageProtocolStructure = Nothing
        mTabPageStateStructure = Nothing
        mTabPageSection = Nothing
        mTabPageDescription.Reset()
        mTermBindingPanel.Reset()
        If Not mTabPageParticipation Is Nothing Then
            mTabPageParticipation.Reset()
        End If


    End Sub

    Public Sub ShowTabPages(ByVal archetyped_class As StructureType, ByVal isNew As Boolean)

        'layout of the pages to match the archetyped class
        'Compositions, Sections and Instructions use the tpSectionPage which is generic - probably a bad name!

        Select Case archetyped_class

            Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.ADMIN_ENTRY
                ' enable restriction of subject of care
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.SubjectOfData)
                End If
                'need to check not changing from section
                If Me.TabMain.TabPages.Contains(Me.tpSectionPage) Then
                    Me.TabMain.TabPages.Remove(Me.tpSectionPage)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpDesign"))
                End If

                Select Case archetyped_class
                    Case StructureType.ADMIN_ENTRY, StructureType.ENTRY
                        'no protocol, state or EventSeries
                        Me.cbPersonState.Visible = False
                        Me.cbStructurePersonState.Visible = False
                        Me.cbProtocol.Visible = False
                    Case StructureType.EVALUATION
                        Me.cbPersonState.Visible = False
                        Me.cbStructurePersonState.Visible = False
                        Me.cbProtocol.Visible = True
                    Case StructureType.OBSERVATION
                        Me.cbPersonState.Visible = True
                        Me.cbStructurePersonState.Visible = True
                        Me.cbProtocol.Visible = True
                End Select

            Case StructureType.INSTRUCTION
                ' enable restriction of subject of care
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.SubjectOfData)
                End If

                'need to check not changing from section
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If

            Case StructureType.ACTION
                ' enable restriction of subject of care
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.SubjectOfData)
                End If

                'need to check not changing from section
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If

            Case StructureType.SECTION
                ' disable restriction of subject of care
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.None)
                End If

                'need to check not changing from Evaluation, Observation
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If

            Case StructureType.COMPOSITION
                ' Set to restriction of setting
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.Setting)
                End If


                'need to check not changing from Evaluation, Observation
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If

                'Me.tpSectionPage.Title = ReferenceModel.ArchetypedClass.ToString

            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                ' disable restriction of subject of care
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.None)
                End If

                'need to check not changing from section
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If

                'Me.tpSectionPage.Title = ReferenceModel.StructureClass.ToString

            Case StructureType.Cluster
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.None)
                End If
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If

            Case StructureType.Element
                If isNew Then
                    InitialiseRestrictedSet(RestrictedSet.TermSet.None)
                End If
                If Me.TabMain.TabPages.Contains(Me.tpDesign) Then
                    Me.TabMain.TabPages.Remove(Me.tpDesign)
                    Me.TabMain.TabPages.Insert(1, Me.mBaseTabPagesCollection("tpSectionPage"))
                End If
        End Select

    End Sub

    Private Function CheckOKtoClose() As Boolean
        If Filemanager.HasFileToSave Then
            Return Filemanager.SaveFiles(True)
        End If
        Return True
    End Function
#End Region

#Region "Menus, toolbar and related functions - apart from language (see region below)"

    Private Sub SetUpStructure()
        Me.mTabPageDataStructure = New TabPageStructure
        Me.tpSectionPage.Controls.Clear()
        Me.tpSectionPage.Controls.Add(mTabPageDataStructure)
        mTabPageDataStructure.Dock = DockStyle.Fill
        Me.mComponentsCollection.Add(mTabPageDataStructure)
    End Sub

    Private Sub SetUpSection()
        mTabPageSection = New TabPageSection
        Me.tpSectionPage.Controls.Clear()
        Me.tpSectionPage.Controls.Add(mTabPageSection)
        mTabPageSection.Dock = DockStyle.Fill
        Me.mComponentsCollection.Add(mTabPageSection)
        'Me.tpSectionPage.Title = AE_Constants.Instance.Section

        Me.HelpProviderDesigner.SetHelpNavigator(tpSectionPage, HelpNavigator.Topic)
        Me.HelpProviderDesigner.SetHelpKeyword(tpSectionPage, "HowTo/edit_section.htm")
    End Sub

    Private Sub SetUpComposition()
        mTabPageComposition = New TabPageComposition
        Me.tpSectionPage.Controls.Clear()
        Me.tpSectionPage.Controls.Add(mTabPageComposition)
        mTabPageComposition.Dock = DockStyle.Fill
        Me.mComponentsCollection.Add(mTabPageComposition)
        'Me.tpSectionPage.Title = mFileManager.Archetype.RmType.ToString

        Me.HelpProviderDesigner.SetHelpNavigator(tpSectionPage, HelpNavigator.Topic)
        'FIXME - need to add help about how to edit a composition
        Me.HelpProviderDesigner.SetHelpKeyword(tpSectionPage, "HowTo/edit_section.htm")
    End Sub


    Private Sub SetUpEventSeries(Optional ByVal NodeId As String = "")
        Dim tp As New Crownwood.Magic.Controls.TabPage
        Dim file_loading As Boolean

        mTabPageDataEventSeries = New TabpageHistory
        tp.Name = "tpDataEventSeries"
        tp.Title = Filemanager.GetOpenEhrTerm(133, "Events")
        mTabPagesCollection.Add(tp.Name, tp)
        mComponentsCollection.Add(mTabPageDataEventSeries)
        tp.Controls.Add(mTabPageDataEventSeries)
        mTabPageDataEventSeries.Dock = DockStyle.Fill
        Me.TabStructure.TabPages.Add(tp)

        file_loading = mFileManager.FileLoading
        mFileManager.FileLoading = True
        ' this creates a new EventSeries unless fileloading set to true
        ' which happens when creating a new archetype
        Me.chkEventSeries.Checked = True
        ' now put it back to how it was
        mFileManager.FileLoading = file_loading

        If NodeId = "" Then
            ' a new EventSeries so set Id and add a baseline event as default
            mTabPageDataEventSeries.NodeId = mFileManager.OntologyManager.AddTerm("Event Series", "@ internal @").Code
            mTabPageDataEventSeries.AddBaseLineEvent()
        Else
            mTabPageDataEventSeries.NodeId = NodeId
        End If

        Me.HelpProviderDesigner.SetHelpNavigator(tp, HelpNavigator.Topic)
        Me.HelpProviderDesigner.SetHelpKeyword(tp, "HowTo/edit_EventSeries.htm")
    End Sub

    Private Sub SetUpInstruction()
        ' reset the data structure tab page
        mTabPageInstruction = New TabPageInstruction

        'Me.tpSectionPage.Title = Filemanager.GetOpenEhrTerm(557, StructureType.INSTRUCTION.ToString)
        Me.tpSectionPage.Controls.Clear()
        Me.tpSectionPage.Controls.Add(mTabPageInstruction)
        mTabPageInstruction.Dock = DockStyle.Fill

        ' add it to the collection of components that require translation
        Me.mComponentsCollection.Add(mTabPageInstruction)

        Me.HelpProviderDesigner.SetHelpNavigator(tpSectionPage, HelpNavigator.Topic)
        Me.HelpProviderDesigner.SetHelpKeyword(tpSectionPage, "HowTo/edit_instruction.htm")

    End Sub

    Private Sub SetUpAction()
        ' reset the data structure tab page
        mTabPageAction = New TabPageAction

        'Me.tpSectionPage.Title = Filemanager.GetOpenEhrTerm(556, "Action")
        Me.tpSectionPage.Controls.Clear()
        Me.tpSectionPage.Controls.Add(mTabPageAction)
        mTabPageAction.Dock = DockStyle.Fill

        ' add it to the collection of components that require translation
        Me.mComponentsCollection.Add(mTabPageAction)

        Me.HelpProviderDesigner.SetHelpNavigator(tpSectionPage, HelpNavigator.Topic)
        Me.HelpProviderDesigner.SetHelpKeyword(tpSectionPage, "HowTo/edit_instruction.htm")

    End Sub

    Private Sub SetUpDataStructure()
        ' reset the data structure tab page
        mTabPageDataStructure = New TabPageStructure

        'Changed SRH: Sep 1st 2007
        mTabPageDataStructure.EmbeddedAllowed = False

        Me.tpDataStructure.Title = Filemanager.GetOpenEhrTerm(85, "Structure")
        Me.tpDataStructure.Controls.Add(mTabPageDataStructure)
        mTabPageDataStructure.Dock = DockStyle.Fill

        ' add it to the collection of components that require translation
        mComponentsCollection.Add(mTabPageDataStructure)

        Me.TabStructure.SelectedIndex = 0
        Me.tpDataStructure.Selected = True
    End Sub

    Private Sub SetUpGUI(ByVal archetyped_class As StructureType, ByVal isNew As Boolean)

        'NOTE: Showing tabpages in the editor requires generating
        'IDs for the structural components (e.g. EventSeries and List)
        'This feature cannot be run unless the Ontology is initialised
        If mFileManager.OntologyManager.Ontology Is Nothing Then
            Beep()
            Debug.Assert(False)
            Return
        End If

        Select Case archetyped_class
            Case StructureType.OBSERVATION
                'must have a EventSeries
                Me.cbPersonState.Visible = True
                If isNew Then
                    SetUpDataStructure()
                    SetUpEventSeries()
                End If

            Case StructureType.EVALUATION, StructureType.ADMIN_ENTRY
                If isNew Then
                    SetUpDataStructure()
                End If

            Case StructureType.INSTRUCTION
                If isNew Then
                    SetUpInstruction()
                End If

            Case StructureType.ACTION
                If isNew Then
                    SetUpAction()
                End If

            Case StructureType.SECTION
                If isNew Then
                    SetUpSection()
                End If

            Case StructureType.COMPOSITION
                If isNew Then
                    SetUpComposition()
                End If

            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                If isNew Then
                    SetUpStructure()
                End If

            Case StructureType.Cluster
                If isNew Then
                    SetUpStructure()
                    Me.mTabPageDataStructure.SetAsCluster(mFileManager.Archetype.ConceptCode)
                End If

            Case StructureType.Element
                If isNew Then
                    SetUpStructure()
                    Me.mTabPageDataStructure.SetAsElement(mFileManager.Archetype.ConceptCode)
                End If

            Case StructureType.ENTRY
                If isNew Then
                    SetUpDataStructure()
                End If

            Case Else
                Beep()
                Debug.Assert(False)
                Throw New Exception(String.Format("{0}: {1}", AE_Constants.Instance.Error_loading, mFileManager.Archetype.Archetype_ID.ToString))

        End Select

        ShowTabPages(archetyped_class, isNew)

        Me.lblLifecycle.Text = mFileManager.Archetype.LifeCycle
        Me.lblArchetypeName.Text = mFileManager.Archetype.Archetype_ID.ToString
        ' Set the form text
        Me.Text = AE_Constants.Instance.MessageBoxCaption & " [" & Me.txtConceptInFull.Text & "]"

        ' Set the GUI language elements
        Me.lblPrimaryLanguage.Text = mFileManager.OntologyManager.PrimaryLanguageText
        ' set the language menu
        Dim d_row As DataRow
        For Each d_row In mFileManager.OntologyManager.LanguagesTable.Rows
            AddLanguageToMenu(d_row(1))
        Next

        'Set the description and translation
        mTabPageDescription.Description = mFileManager.Archetype.Description
        mTabPageDescription.TranslationDetails = mFileManager.Archetype.TranslationDetails

        RichTextBoxDescription.Rtf = mTabPageDescription.AsRtfString()
        RichTextBoxUnicode.ProcessRichEditControl(RichTextBoxDescription, mFileManager, mTabPageDescription) 'JAR: 13APR07, EDT-32 Support unicode
    End Sub

    Private Function SetNewArchetypeName(Optional ByVal AllowOpen As Boolean = True) As Integer
        ' returns 0 if cancelled
        ' returns  1 if archetype is opened
        ' returns 2 if new archetype

        Dim frm As New frmStartUp
        Dim i As Integer

        If AllowOpen Then
            mFileManager.FileLoading = True
            ' clear the archetype editor
            Me.ResetDefaults()
            ' prevent being asked again to save if open new archetype
            mFileManager.FileEdited = False
            mFileManager.FileLoading = False
        End If

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            frm.comboModel.Text = Filemanager.GetOpenEhrTerm(104, "Choose...", OceanArchetypeEditor.DefaultLanguageCode)
            frm.comboComponent.Text = Filemanager.GetOpenEhrTerm(104, "Choose...", OceanArchetypeEditor.DefaultLanguageCode)
            frm.gbNew.Text = Filemanager.GetOpenEhrTerm(50, frm.gbNew.Text, OceanArchetypeEditor.DefaultLanguageCode)
            frm.lblComponent.Text = Filemanager.GetOpenEhrTerm(513, frm.lblComponent.Text, OceanArchetypeEditor.DefaultLanguageCode)
            frm.lblModel.Text = Filemanager.GetOpenEhrTerm(51, frm.lblModel.Text, OceanArchetypeEditor.DefaultLanguageCode)
            frm.lblShortConcept.Text = Filemanager.GetOpenEhrTerm(52, frm.lblShortConcept.Text, OceanArchetypeEditor.DefaultLanguageCode)
            frm.butOK.Text = Filemanager.GetOpenEhrTerm(165, frm.butOK.Text, OceanArchetypeEditor.DefaultLanguageCode)
            If Not AllowOpen Then
                frm.butCancel.Text = Filemanager.GetOpenEhrTerm(166, "Cancel")
            Else
                frm.gbExistingArchetype.Text = Filemanager.GetOpenEhrTerm(609, "Open existing archetypes")
                frm.gbArchetypeFromWeb.Text = Filemanager.GetOpenEhrTerm(650, "Open Archetype from Web")
                frm.butCancel.Text = Filemanager.GetOpenEhrTerm(63, "Exit")
                frm.gbFormat.Text = Filemanager.GetOpenEhrTerm(638, "Format")
            End If
            frm.RightToLeft = Me.RightToLeft
        End If

        If OceanArchetypeEditor.Instance.Options.DefaultParser.ToLower(System.Globalization.CultureInfo.InvariantCulture) = "adl" Then
            frm.rbADL.Checked = True
        Else
            frm.rbXML.Checked = True
        End If

        If Not AllowOpen Then
            frm.gbExistingArchetype.Visible = False
            frm.Height -= (frm.gbExistingArchetype.Height + 10)
            If (OceanArchetypeEditor.Instance.Options.AllowWebSearch = False) Then
                frm.gbArchetypeFromWeb.Visible = False
                Me.Height -= ((frm.gbArchetypeFromWeb.Height) + 10)
            End If

        End If

        i = frm.ShowDialog(Me)

        Select Case i

            Case 1
                'this creates an archetype and sets the ontology
                mFileManager.NewArchetype(frm.Archetype_ID, OceanArchetypeEditor.Instance.Options.DefaultParser)

                If mFileManager.Archetype.ArchetypeAvailable Then
                    mFileManager.Archetype.Version = 1
                    mFileManager.Archetype.LifeCycle = "draft"

                    ' Now set up the GUI - requires an ontology.
                    frm.Close()
                    mFileManager.IsNew = True  ' this is a new archetype
                    Return 2
                End If
            Case 2 'cancel or exit
                frm.Close()
                If AllowOpen Then
                    Me.Close() ' close the application
                Else
                    Return 0
                End If
            Case 4 'pressed the open-From-Web Button
                frm.Close()
                Me.OpenArchetypeFromWeb(Me, New System.EventArgs)
                If mFileManager.Archetype Is Nothing Then
                    'open archetype was cancelled so go back to new
                    Return SetNewArchetypeName()
                End If
                Return 1
            Case 6  'pressed the open button
                frm.Close()
                Me.OpenArchetype(Me, New System.EventArgs)
                If mFileManager.Archetype Is Nothing Then
                    'open archetype was cancelled so go back to new
                    Return SetNewArchetypeName()
                End If
                Return 1
        End Select
    End Function

    Private Sub MenuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFileExit.Click
        'If CheckOKtoClose() Then
        'Close checks if it is OK to close
        Me.Close()
        'Me.Dispose()
        'End If
    End Sub

    Private Sub MenuViewLanguageTerminology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuLanguageAvailable.Click, MenuTerminologyAvailable.Click
        Me.TabMain.SelectedIndex = 2
        Me.TabMain.SelectedTab = Me.tpTerminology
        Me.TabTerminology.SelectedIndex = 2
        Me.TabTerminology.SelectedTab = Me.tpLanguages
        Me.ListLanguages.Focus()
    End Sub

    Private Sub MenuViewArchetypes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As Form
        frm = New formCreateClinicalModel
        frm.ShowDialog(Me)
    End Sub

    Private Sub MenuHelpOceanEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuHelpOceanEditor.Click
        Dim Frm As New Splash
        Frm.ControlBox = True
        Frm.ShowDialog(Me)
        Frm.Dispose()
    End Sub

    Private Sub UpdateSpecialisationTree(ByVal Label As String, ByVal NodeId As String)
        Dim tnc As TreeNodeCollection
        Dim tn, new_node As TreeNode

        If Me.tvSpecialisation.GetNodeCount(False) > 0 Then
            Dim i As Integer

            tn = Me.tvSpecialisation.Nodes(0)
            For i = 2 To Me.tvSpecialisation.GetNodeCount(True)
                tn = tn.LastNode
            Next
            If tn.Tag = NodeId Then
                ' update the node as specialisation is already there
                tn.Text = Label
                Return
            End If
            tnc = tn.Nodes
        Else
            tnc = Me.tvSpecialisation.Nodes
        End If
        new_node = New TreeNode
        new_node.Text = Label
        new_node.Tag = NodeId
        tnc.Add(new_node)
        new_node.EnsureVisible()

    End Sub

    Private Sub MenuFileSpecialise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFileSpecialise.Click
        'Enable specialisation of the archetype
        If MessageBox.Show(AE_Constants.Instance.Specialise & " " & mFileManager.Archetype.Archetype_ID.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = _
            Windows.Forms.DialogResult.OK Then
            Dim s As String
            s = "-"
            While InStr(s, "-") > 0
                s = OceanArchetypeEditor.Instance.GetInput("Enter the new concept ('-' is not allowed)", Me)
            End While

            If s <> "" Then
                Dim a_Term As RmTerm

                ' replace spaces with underscore
                s = s.Replace(" "c, "_"c)

                'specialise concept
                mFileManager.FileLoading = True
                mFileManager.Archetype.Specialise(s, mFileManager.OntologyManager)

                ' show the new archetype ID in the GUI
                Me.lblArchetypeName.Text = mFileManager.Archetype.Archetype_ID.ToString
                a_Term = mFileManager.OntologyManager.GetTerm(mFileManager.Archetype.ConceptCode)
                Me.txtConceptInFull.Text = a_Term.Text
                Me.TxtConceptDescription.Text = a_Term.Description
                Me.txtConceptComment.Text = ""
                Me.gbSpecialisation.Visible = True
                UpdateSpecialisationTree(a_Term.Text, mFileManager.Archetype.ConceptCode)
                Me.MenuFileSpecialise.Visible = False
                mFileManager.FileLoading = False
                mFileManager.FileEdited = True
                mFileManager.IsNew = True
                mFileManager.FileName = ""    'new filename and needs to save as
            End If
        End If
    End Sub

    Private Sub ToolBarMain_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBarMain.ButtonClick
        Select Case ToolBarMain.Buttons.IndexOf(e.Button)

            Case 0 ' New
                NewArchetype(sender, e)
            Case 1 ' open
                OpenArchetype(sender, e)
            Case 2 ' Open from Web
                OpenArchetypeFromWeb(sender, e)
            Case 3 ' Save                                
                If Filemanager.SaveFiles(False) Then
                    Me.MenuFileSpecialise.Visible = True
                End If
            Case 4 ' separator

            Case 5 ' Print
                ' only available when displaying archetype

        End Select
    End Sub

    Private Sub MenuHelpOcean_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuHelpOcean.Click
        'FIXME
        Process.Start("http://www.oceaninformatics.biz")
    End Sub

    Private Sub MenuHelpReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuHelpReport.Click
        Process.Start("http://www.oceaninformatics.biz/archetype_editor/bug_reporting_archetype_editor.htm")
    End Sub

    Private Sub MenuHelpStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuHelpStart.Click
        Try
            Process.Start(Application.StartupPath & "\Help\ArchetypeEditor.chm")
        Catch
            MessageBox.Show(AE_Constants.Instance.Error_loading & " Help\ArchetypeEditor.chm", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuHelpLicence_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuHelpLicence.Click
        Dim s As String
        Try
            s = "file:///" & Application.StartupPath & "\Ocean Archetype Editor Licence Agreement.html"
            Process.Start(s)
        Catch
            MessageBox.Show(AE_Constants.Instance.Error_loading & " Ocean Archetype Editor Licence Agreement.html", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuViewConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuViewConfig.Click
        OceanArchetypeEditor.Instance.Options.ShowOptionsForm()
    End Sub

    Private Sub menuFileNewWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFileNewWindow.Click
        Try
            Dim start_info As New ProcessStartInfo
            start_info.FileName = Application.ExecutablePath
            start_info.WorkingDirectory = Application.StartupPath
            Process.Start(start_info)
        Catch
            MessageBox.Show(AE_Constants.Instance.Error_loading & " Archetype Editor", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Language related functions - Add, Change, List_selectedIndex, Menu Change"

    Private Sub AddLanguage(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAdd.Click, MenuLanguageAdd.Click
        If ChooseLanguage() Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub ChangeLanguage(ByVal LangCode As String)
        If mFileManager.OntologyManager.LanguageCode <> LangCode Then
            ' a new language is selected so populate the terms
            mFileManager.OntologyManager.LanguageCode = LangCode
            Translate(LangCode)
        End If
    End Sub

    Private Sub ListLanguages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListLanguages.SelectedIndexChanged

        ' need to load the definitions

        If Me.ListLanguages.Focused Then
            ' crownwood controls have a bug where selecting a tabpage triggers select index on this control
            ' the following if statement short circuits this
            ChangeLanguage(Me.ListLanguages.SelectedValue)
        End If

    End Sub

    Private Sub MenuLanguageChange_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuLanguageChange.Popup
        Dim MI As MenuItem
        For Each MI In MenuLanguageChange.MenuItems
            AddHandler MI.Click, AddressOf MenuChangeLanguage_Click
        Next
    End Sub

    Private Sub MenuChangeLanguage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        i = Me.ListLanguages.FindStringExact(sender.text)
        If i > -1 Then
            Me.ListLanguages.SelectedIndex = i
            ChangeLanguage(Me.ListLanguages.SelectedValue)
        End If
    End Sub

#End Region

#Region "Methods to build the GUI when an archetype is loaded"

    Private Sub ProcessStateEventSeries(ByVal a_history As RmHistory)

        Me.cbPersonState.Checked = True
        'cannot have state associated with structure
        Me.cbStructurePersonState.Enabled = False
        Me.TabDesign.TabPages.Add(Me.mBaseTabPagesCollection.Item("tpRootState"))
        Me.tpRootState.Visible = True

        mTabPageStateEventSeries = New TabpageHistory
        mTabPageStateEventSeries.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpRootStateEventSeries.Controls.Add(mTabPageStateEventSeries)
        mTabPageStateEventSeries.Dock = DockStyle.Fill
        mTabPageStateEventSeries.ProcessEventSeries(a_history)
        mComponentsCollection.Add(mTabPageStateEventSeries)

        mTabPageStateStructure = New TabPageStructure  'Me
        mTabPageStateStructure.IsState = True  ' sets some display characteristics of buttons
        mTabPageStateStructure.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpRootStateStructure.Controls.Add(mTabPageStateStructure)
        mTabPageStateStructure.Dock = DockStyle.Fill
        If Not a_history.Data Is Nothing Then
            Me.mTabPageStateStructure.ProcessStructure(a_history.Data)
        End If
        Me.tpRootStateStructure.Title = Filemanager.GetOpenEhrTerm(mTabPageStateStructure.StructureType, mTabPageStateStructure.StructureType.ToString())
        mComponentsCollection.Add(mTabPageStateStructure)

    End Sub

    Private Sub ProcessEventSeries(ByVal a_EventSeries As RmHistory)
        Dim tp As New Crownwood.Magic.Controls.TabPage

        SetUpEventSeries(a_EventSeries.NodeId)
        mTabPageDataEventSeries.ProcessEventSeries(a_EventSeries)
        SetUpDataStructure()
        If Not a_EventSeries.Data Is Nothing Then
            ProcessDataStructure(a_EventSeries.Data)
        End If

    End Sub

    Private Sub ProcessState(ByVal a_Structure As RmStructure)

        Dim tp As New Crownwood.Magic.Controls.TabPage
        mTabPageDataStateStructure = New TabPageStructure  'Me)
        mTabPageDataStateStructure.IsState = True ' allows assumed values to be set (buttons visible)
        Me.cbStructurePersonState.Checked = True

        If a_Structure.Type = StructureType.Slot Then
            mTabPageDataStateStructure.ProcessStructure(CType(a_Structure, RmSlot))
        Else
            mTabPageDataStateStructure.ProcessStructure(CType(a_Structure, RmStructureCompound))
        End If

        ' add it to the collection of components that require translation
        tp.Title = mTabPageDataStateStructure.StructureType
        mComponentsCollection.Add(mTabPageDataStateStructure)
        tp.Controls.Add(mTabPageDataStateStructure)
        mTabPageDataStateStructure.Dock = DockStyle.Fill
        tp.BackColor = System.Drawing.Color.RoyalBlue
        tp.Name = "tpStateStructure"
        tp.Title = AE_Constants.Instance.Person_state
        Me.TabStructure.TabPages.Add(tp)


        Me.HelpProviderDesigner.SetHelpNavigator(tp, HelpNavigator.Topic)
        Me.HelpProviderDesigner.SetHelpKeyword(tp, "HowTo/edit_state.html")

    End Sub

    Private Sub ProcessStructure(ByVal a_Structure As RmStructureCompound)
        SetUpStructure()
        mTabPageDataStructure.ProcessStructure(a_Structure)
    End Sub

    Private Sub ProcessCluster(ByVal a_Structure As RmCluster)
        SetUpStructure()
        mTabPageDataStructure.ProcessStructure(a_Structure)
    End Sub

    Private Sub ProcessElement(ByVal an_element As RmElement)
        SetUpStructure()
        mTabPageDataStructure.ProcessStructure(an_element)
    End Sub


    Private Sub ProcessPathwaySpecification(ByVal a_structure As RmStructureCompound)


    End Sub

    Private Sub ProcessDataStructure(ByVal a_Structure As RmStructure)
        'Changed - no slots for data in archetypes (allowed in state and protocol)
        'Added chkEmbedded = false
        'If a_Structure.Type = StructureType.Slot Then
        'mTabPageDataStructure.ProcessStructure(CType(a_Structure, RmSlot))
        'Else
        mTabPageDataStructure.ProcessStructure(CType(a_Structure, RmStructureCompound))
        'End If
        mTabPageDataStructure.EmbeddedAllowed = False
        Me.tpDataStructure.Title = mTabPageDataStructure.StructureTypeAsString
    End Sub

    Private Sub ProcessProtocol(ByVal rm As RmStructure, ByVal tbCtrl As Crownwood.Magic.Controls.TabControl)
        Dim tp As New Crownwood.Magic.Controls.TabPage
        mTabPageProtocolStructure = New TabPageStructure '(Me)
        mTabPageProtocolStructure.BackColor = System.Drawing.Color.PaleGoldenrod
        If rm.Type = StructureType.Slot Then
            mTabPageProtocolStructure.ProcessStructure(CType(rm, RmSlot))
        Else
            mTabPageProtocolStructure.ProcessStructure(CType(rm, RmStructureCompound))
        End If
        
        ' add it to the collection of components that require translation
        mComponentsCollection.Add(mTabPageProtocolStructure)
        tp.Controls.Add(mTabPageProtocolStructure)
        mTabPageProtocolStructure.Dock = DockStyle.Fill
        tp.BackColor = System.Drawing.Color.LightSteelBlue
        tp.Name = "tpProtocol"
        tp.Title = Filemanager.GetOpenEhrTerm(78, "Protocol")
        tbCtrl.TabPages.Add(tp)
        Me.cbProtocol.Checked = True

        Me.HelpProviderDesigner.SetHelpNavigator(tp, HelpNavigator.Topic)
        Me.HelpProviderDesigner.SetHelpKeyword(tp, "HowTo/edit_protocol.htm")
    End Sub
#End Region

#Region "Methods to save archetype as represented in the GUI"

    Public Sub PrepareToSave() 'Called internally and by FileManager
        Dim STATE_processed As Boolean
        Dim tp As Crownwood.Magic.Controls.TabPage

        ' Clear the definitions prior to rebuilding them        
        mFileManager.Archetype.ResetDefinitions()
        mFileManager.Archetype.Description = mTabPageDescription.Description
        mFileManager.Archetype.TranslationDetails = mTabPageDescription.TranslationDetails

        If Me.ShowAsDraft Then
            mFileManager.Archetype.LifeCycle = "Initial"
        End If

        ' For all ENTRY subtypes

        ' get the subject of data information
        Select Case mFileManager.Archetype.Definition.Type
            Case StructureType.ENTRY, StructureType.EVALUATION, _
                StructureType.OBSERVATION, StructureType.INSTRUCTION, _
                StructureType.ACTION, StructureType.ADMIN_ENTRY

                'Subject of data
                If Not mRestrictedSubject Is Nothing AndAlso mRestrictedSubject.HasRestriction Then
                    CType(mFileManager.Archetype.Definition, RmEntry).SubjectOfData.Relationship = mRestrictedSubject.AsCodePhrase
                End If

                'Participations
                If cbParticipation.Checked Then
                    'Participations may have been added
                    If mTabPageParticipation.chkProvider.Checked Then
                        CType(mFileManager.Archetype.Definition, RmEntry).ProviderIsMandatory = True
                    Else
                        CType(mFileManager.Archetype.Definition, RmEntry).ProviderIsMandatory = False
                    End If

                    'Check for other participations
                    If mTabPageParticipation.HasOtherParticipations Then
                        CType(mFileManager.Archetype.Definition, RmEntry).OtherParticipations = mTabPageParticipation.OtherParticipations
                    End If
                End If

                Select Case mFileManager.Archetype.Definition.Type
                    Case StructureType.INSTRUCTION
                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data = mTabPageInstruction.SaveAsInstruction.Children
                        If mTabPageInstruction.HasProtocol AndAlso Not mTabPageProtocolStructure Is Nothing AndAlso mTabPageProtocolStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                            Dim rm As New RmStructureCompound(StructureType.Protocol.ToString, StructureType.Protocol)
                            rm.Children.Add(mTabPageProtocolStructure.SaveAsStructure)
                            CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                        End If

                    Case StructureType.ACTION
                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data = mTabPageAction.SaveAsAction.Children
                        If mTabPageAction.HasProtocol AndAlso Not mTabPageProtocolStructure Is Nothing AndAlso mTabPageProtocolStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                            Dim rm As New RmStructureCompound(StructureType.Protocol.ToString, StructureType.Protocol)
                            rm.Children.Add(mTabPageProtocolStructure.SaveAsStructure)
                            CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                        End If

                    Case StructureType.OBSERVATION
                        For Each tp In Me.TabStructure.TabPages

                            ' Observation always has at least one event
                            Select Case tp.Name

                                Case "tpDataEventSeries"
                                    If Not mTabPageDataEventSeries Is Nothing Then
                                        Dim rm As RmStructureCompound
                                        Dim RmHistory As RmHistory

                                        rm = New RmStructureCompound(StructureType.Data.ToString, StructureType.Data)
                                        RmHistory = mTabPageDataEventSeries.SaveAsEventSeries()
                                        If Not mTabPageDataStructure Is Nothing AndAlso mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                            RmHistory.Data = mTabPageDataStructure.SaveAsStructure
                                        End If
                                        rm.Children.Add(RmHistory)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If

                                Case "tpStateStructure"
                                    If Not mTabPageDataStateStructure Is Nothing AndAlso mTabPageDataStateStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        Dim rmState As New RmStructureCompound(StructureType.State.ToString, StructureType.State)
                                        rmState.Children.Add(mTabPageDataStateStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rmState)
                                        STATE_processed = True
                                    End If
                            End Select
                        Next

                        ' there may be PROTOCOL and ROOT_State

                        For Each tp In Me.TabDesign.TabPages
                            ' DATA will have STRUCTURE - simple, list, tree, table
                            Select Case tp.Name
                                Case "tpData"
                                    'No action as dealt with above
                                Case "tpProtocol"
                                    If Not mTabPageProtocolStructure Is Nothing AndAlso mTabPageProtocolStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        Dim rm As New RmStructureCompound(StructureType.Protocol.ToString, StructureType.Protocol)
                                        rm.Children.Add(mTabPageProtocolStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If

                                Case "tpRootState"
                                    If Not mTabPageStateStructure Is Nothing Then
                                        ' check only one state expression
                                        If STATE_processed Then
                                            'FIXME - raise error
                                            Exit Select
                                        End If

                                        Dim Tab As Crownwood.Magic.Controls.TabControl
                                        Dim rm As New RmStructureCompound(StructureType.State.ToString, StructureType.State)

                                        Try
                                            Tab = CType(tp.Controls(0), Crownwood.Magic.Controls.TabControl)

                                            If Tab.TabPages.Count = 2 Then
                                                If Not mTabPageStateEventSeries Is Nothing Then
                                                    Dim stateHistory As RmHistory = mTabPageStateEventSeries.SaveAsEventSeries
                                                    If Not mTabPageStateStructure Is Nothing AndAlso mTabPageStateStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                                        stateHistory.Data = Me.mTabPageStateStructure.SaveAsStructure
                                                        rm.Children.Add(stateHistory)
                                                    End If
                                                End If
                                            End If
                                        Catch
                                            'FIXME raise error
                                            Beep()
                                            Debug.Assert(False)

                                        End Try
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If
                            End Select
                        Next

                    Case StructureType.EVALUATION ' "ENTRY.Evaluation

                        For Each tp In Me.TabStructure.TabPages

                            Select Case tp.Name

                                Case "tpDataStructure"
                                    If Not mTabPageDataStructure Is Nothing AndAlso mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        Dim rm As RmStructureCompound
                                        rm = New RmStructureCompound(StructureType.Data.ToString, StructureType.Data)
                                        rm.Children.Add(mTabPageDataStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If

                                Case "tpStateStructure"
                                    If Not mTabPageDataStateStructure Is Nothing AndAlso mTabPageDataStateStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        STATE_processed = True
                                        Dim rm As RmStructureCompound
                                        rm = New RmStructureCompound(StructureType.State.ToString, StructureType.State)
                                        rm.Children.Add(mTabPageDataStateStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If
                            End Select
                        Next

                        For Each tp In Me.TabDesign.TabPages
                            ' DATA will have STRUCTURE - simple, list, tree, table
                            Select Case tp.Name
                                Case "tpData"
                                    'No action as dealt with above
                                Case "tpProtocol"
                                    If Not mTabPageProtocolStructure Is Nothing AndAlso mTabPageProtocolStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        Dim rm As RmStructureCompound
                                        rm = New RmStructureCompound(StructureType.Protocol.ToString, StructureType.Protocol)
                                        rm.Children.Add(mTabPageProtocolStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If
                            End Select
                        Next


                    Case StructureType.ENTRY ' "ENTRY"

                        For Each tp In Me.TabStructure.TabPages

                            Select Case tp.Name
                                Case "tpDataEventSeries"
                                    If Me.chkEventSeries.Checked Then
                                        If Not mTabPageDataEventSeries Is Nothing Then
                                            Dim RmHistory As RmHistory
                                            RmHistory = mTabPageDataEventSeries.SaveAsEventSeries()
                                            If Not mTabPageDataStructure Is Nothing AndAlso mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                                RmHistory.Data = mTabPageDataStructure.SaveAsStructure
                                            End If
                                            CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(RmHistory)
                                        End If
                                    End If

                                Case "tpDataStructure"
                                    If Not Me.chkEventSeries.Checked Then
                                        If Not mTabPageDataStructure Is Nothing AndAlso mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                            Dim rm As RmStructureCompound
                                            rm = New RmStructureCompound(StructureType.Data.ToString, StructureType.Data)
                                            rm.Children.Add(mTabPageDataStructure.SaveAsStructure)
                                            CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                        End If
                                    End If

                                Case "tpStateStructure"
                                    If Not mTabPageDataStateStructure Is Nothing AndAlso mTabPageDataStateStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        STATE_processed = True
                                        Dim rm As RmStructureCompound
                                        rm = New RmStructureCompound(StructureType.State.ToString, StructureType.State)
                                        rm.Children.Add(mTabPageDataStateStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If
                            End Select
                        Next

                        ' there may be PROTOCOL and ROOT_State

                        For Each tp In Me.TabDesign.TabPages
                            ' DATA will have STRUCTURE - simple, list, tree, table
                            Select Case tp.Name
                                Case "tpData"
                                    'No action as dealt with above
                                Case "tpProtocol"
                                    If mTabPageProtocolStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        Dim rm As RmStructureCompound
                                        rm = New RmStructureCompound(StructureType.Protocol.ToString, StructureType.Protocol)
                                        rm.Children.Add(mTabPageProtocolStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If
                            End Select
                        Next


                    Case StructureType.ADMIN_ENTRY

                        For Each tp In Me.TabStructure.TabPages
                            Select Case tp.Name
                                Case "tpDataStructure"
                                    If Not mTabPageDataStructure Is Nothing AndAlso mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                                        Dim rm As RmStructureCompound
                                        rm = New RmStructureCompound(StructureType.Data.ToString, StructureType.Data)
                                        rm.Children.Add(mTabPageDataStructure.SaveAsStructure)
                                        CType(mFileManager.Archetype.Definition, ArchetypeDefinition).Data.Add(rm)
                                    End If
                            End Select
                        Next

                End Select

            Case StructureType.SECTION ' "SECTION"

                ' Added try to this call as it now throws an exception if
                ' it encounters any components that are not sections
                Try
                    mFileManager.Archetype.Definition = mTabPageSection.SaveAsSection()
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Case StructureType.Cluster ' "CLUSTER"

                ' Added try to this call as it now throws an exception if
                ' it encounters any components that are not clusters or elements
                Try
                    If mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception 
                        mFileManager.Archetype.Definition = Me.mTabPageDataStructure.SaveAsStructure()
                    End If
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Case StructureType.Element

                ' Added try to this call as it now throws an exception if
                ' it encounters any components that are not clusters or elements
                Try
                    If mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                        mFileManager.Archetype.Definition = Me.mTabPageDataStructure.SaveAsStructure()
                    End If
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Case StructureType.COMPOSITION

                ' Throws exception if encounters any 
                ' components that are not sections
                Try
                    mFileManager.Archetype.Definition = mTabPageComposition.SaveAsComposition()
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try


            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                If mTabPageDataStructure.HasData Then 'JAR: 31MAY07, EDT-21 Empty structure raises an exception
                    mFileManager.Archetype.Definition = mTabPageDataStructure.SaveAsStructure
                End If
        End Select
    End Sub

#End Region

#Region "Form functions"

    Private Sub Designer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' general initialisation

        AddHandler Filemanager.IsFileDirtyChanged, AddressOf FileManager_IsFileDirtyChanged
        mFileManager = New FileManagerLocal
        Filemanager.Master = mFileManager
        mFileManager.ObjectToSave = Me

        DesignerInitialiser() ' see Internal functions region

        ' the following lines are necessary to maintain links in the bindings
        ' for the databindings to stay current
        Me.tpHeader.BindingContext = Me.BindingContext
        Me.tpDesign.BindingContext = Me.BindingContext
        Me.tpTerminology.BindingContext = Me.BindingContext
        Me.tpData.BindingContext = Me.BindingContext
        Me.tpTerms.BindingContext = Me.BindingContext
        Me.tpLanguages.BindingContext = Me.BindingContext
        Me.tpConstraints.BindingContext = Me.BindingContext

        ' Set the help context
        Me.HelpProviderDesigner.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath

        If (OceanArchetypeEditor.Instance.Options.AllowWebSearch = False) Then
            Me.ToolBarOpenFromWeb.Visible = False
            Me.MenuFileOpenFromWeb.Visible = False
        End If

        'Initialise the bindings of tables for all the lookups
        BindTables()

        ' add the handler after all the languages have been added
        AddHandler ListLanguages.SelectedIndexChanged, AddressOf ListLanguages_SelectedIndexChanged

        ' Set Tooltips
        Me.ToolBarOpen.ToolTipText = Filemanager.GetOpenEhrTerm(609, "Open archetype")
        Me.ToolBarOpenFromWeb.ToolTipText = Filemanager.GetOpenEhrTerm(650, "Open Archetype from Web")
        Me.ToolBarNew.ToolTipText = Filemanager.GetOpenEhrTerm(151, "New")
        Me.ToolBarPrint.ToolTipText = Filemanager.GetOpenEhrTerm(520, "Print")
        Me.ToolBarSave.ToolTipText = Filemanager.GetOpenEhrTerm(183, "Save")

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            TranslateGUI(OceanArchetypeEditor.DefaultLanguageCode)
        End If

        If ArchetypeToOpen <> "" Then
            'command line variable has been set
            Dim archID As String = ArchetypeToOpen.Substring(ArchetypeToOpen.LastIndexOf("\") + 1)
            If archID.StartsWith("OceanRecovery-") Then
                archID = archID.Substring(14)
            End If

            If ArchetypeID.ValidId(archID) Then
                Dim archetypID As ArchetypeID = New ArchetypeID(archID)
                ReferenceModel.SetModelType(archetypID.Reference_Model)
            Else
                ReferenceModel.SetModelType(ReferenceModelType.openEHR_EHR)
            End If

            ''JAR: 12APR07, EDT23 Continue to display splash screen for another 1 second (accounts for overhead to load archetype)
            ''System.Threading.Thread.Sleep(1000)
            'frmSplash.Hide() 'Hide splash screen before open as open can display messagebox that will otherwise sit behind splash screen

            OpenArchetype(ArchetypeToOpen)

            Me.Show()

            If Not mFileManager.ArchetypeAvailable Then
                Me.Close()
            End If

        Else
            ''JAR: 12APR07, EDT23: Continue to display splash screen for another 2 seconds
            'System.Threading.Thread.Sleep(2000)
            'frmSplash.Hide()
            Me.Show()

            'load the start screen
            If SetNewArchetypeName() = 2 Then

                ' new archetype
                SetUpGUI(ReferenceModel.ArchetypedClass, True)
                mFileManager.FileLoading = False
                mFileManager.FileEdited = True

                'Case 1  -  archetype openned - no action
                'Case 0  - exit application called from in set new archetype name
            End If
        End If

            If OceanArchetypeEditor.Instance.Options.AutosaveInterval = 0 Then
                Me.mAutoSaveTimer.Enabled = False
            Else
                Me.mAutoSaveTimer.Enabled = True
                Me.mAutoSaveTimer.Interval = OceanArchetypeEditor.Instance.Options.AutosaveInterval * 60000
            End If

            ''Add the display format buttons based on the parser types
            'For Each format_type As String In mFileManager.AvailableFormats
            '    format_type = format_type.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
            '    If format_type <> "HTML" Then
            '        Dim tbb As New ToolBarButton(format_type)
            '        tbb.Tag = format_type
            '        tbb.Style = ToolBarButtonStyle.ToggleButton
            '        ToolBarRTF.Buttons.Insert(2, tbb)
            '    End If
            'Next
    End Sub

    Private Sub Designer_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not CheckOKtoClose() Then
            e.Cancel = True
        End If
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

#Region "Functions associated with GUI widgets"

    Private Sub chkEventSeries_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEventSeries.CheckedChanged

        ' only proceed if in interactive state
        If mFileManager.FileLoading Then Return

        If chkEventSeries.Checked Then
            If mTabPagesCollection.Contains("tpDataEventSeries") Then
                ' if one is already built
                Me.TabStructure.TabPages.Add(mTabPagesCollection.Item("tpDataEventSeries"))
            Else
                SetUpEventSeries()
            End If
            ' now set the selected tab page to this one
            Dim i As Integer
            For i = 0 To Me.TabStructure.TabPages.Count - 1
                If Me.TabStructure.TabPages(i).Name = "tpDataEventSeries" Then
                    Me.TabStructure.SelectedIndex = i
                End If
            Next
        Else
            Dim tp As Crownwood.Magic.Controls.TabPage
            For Each tp In Me.TabStructure.TabPages
                If tp.Name = "tpDataEventSeries" Then
                    If Not mTabPagesCollection.Contains("tpDataEventSeries") Then
                        mTabPagesCollection.Add(tp.Name, tp)
                    End If
                    Me.TabStructure.TabPages.Remove(tp)
                    Exit For
                End If
            Next
        End If

        If mFileManager.FileLoading Then Exit Sub

        mFileManager.FileEdited = True

    End Sub

    Private Sub txtConceptInFull_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConceptInFull.TextChanged
        If Not mFileManager.FileLoading Then
            mFileManager.OntologyManager.SetText(txtConceptInFull.Text, mFileManager.Archetype.ConceptCode)
            mFileManager.FileEdited = True
            Me.Text = AE_Constants.Instance.MessageBoxCaption & " [" & Me.txtConceptInFull.Text & "]"

            If Me.gbSpecialisation.Visible Then
                UpdateSpecialisationTree(Me.txtConceptInFull.Text, mFileManager.Archetype.ConceptCode)
            End If
        End If
    End Sub

    Private Sub TxtConceptDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtConceptDescription.TextChanged
        If Not mFileManager.FileLoading Then
            If Me.txtConceptInFull.Text = "" Then
                MessageBox.Show(AE_Constants.Instance.Set_concept, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.TxtConceptDescription.Clear()
                Me.txtConceptInFull.Focus()
            Else
                mFileManager.OntologyManager.SetDescription(TxtConceptDescription.Text, mFileManager.Archetype.ConceptCode)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub txtConceptComment_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConceptComment.TextChanged
        If Not mFileManager.FileLoading Then
            If Me.txtConceptInFull.Text = "" Then
                MessageBox.Show(AE_Constants.Instance.Set_concept, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtConceptComment.Clear()
                Me.txtConceptInFull.Focus()
            Else
                mFileManager.OntologyManager.SetComment(txtConceptComment.Text, mFileManager.Archetype.ConceptCode)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub ProtocolCheckChanged(ByVal tbCtrl As Object, ByVal state As Boolean) Handles mTabPageAction.ProtocolCheckChanged, mTabPageInstruction.ProtocolCheckChanged
        Dim tp As Crownwood.Magic.Controls.TabPage
        Dim CrownCtrl As Crownwood.Magic.Controls.TabControl = tbCtrl

        If state Then
            If Me.mTabPagesCollection.Contains("tpProtocol") Then
                If mFileManager.Archetype.RmEntity = StructureType.INSTRUCTION Then
                    CrownCtrl.TabPages.Insert(0, Me.mTabPagesCollection.Item("tpProtocol")) 'JAR: 30MAY07, EDT-44 Protocol to be the first tab
                Else
                    CrownCtrl.TabPages.Add(Me.mTabPagesCollection.Item("tpProtocol"))
                End If
            Else
                mTabPageProtocolStructure = New TabPageStructure '(Me)
                mTabPageProtocolStructure.BackColor = System.Drawing.Color.LightGoldenrodYellow
                tp = New Crownwood.Magic.Controls.TabPage
                tp.Name = "tpProtocol"
                tp.Title = AE_Constants.Instance.Protocol
                tp.Controls.Add(mTabPageProtocolStructure)
                mTabPageProtocolStructure.Dock = DockStyle.Fill
                mComponentsCollection.Add(mTabPageProtocolStructure)
                If Not mTabPagesCollection.Contains(tp.Name) Then
                    Me.mTabPagesCollection.Add(tp.Name, tp)
                End If
                If mFileManager.Archetype.RmEntity = StructureType.INSTRUCTION Then
                    CrownCtrl.TabPages.Insert(0, tp) 'JAR: 30MAY07, EDT-44 Protocol to be the first tab
                Else
                    CrownCtrl.TabPages.Add(tp)
                End If

                Me.HelpProviderDesigner.SetHelpNavigator(tp, HelpNavigator.Topic)
                Me.HelpProviderDesigner.SetHelpKeyword(tp, "HowTo/edit_protocol.htm")
            End If
            ' now set the selected tab page to this one
            Dim i As Integer
            For i = 0 To CrownCtrl.TabPages.Count - 1
                If CrownCtrl.TabPages(i).Name = "tpProtocol" Then
                    Me.TabDesign.SelectedIndex = i
                End If
            Next
        Else
            For Each tp In CrownCtrl.TabPages
                If tp.Name = "tpProtocol" Then
                    If Not Me.mTabPagesCollection.ContainsKey("tpProtocol") Then
                        Me.mTabPagesCollection.Add("tpProtocol", tp) ' save it incase reinstate
                    End If
                    CrownCtrl.TabPages.Remove(tp)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub cbProtocol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProtocol.CheckedChanged

        If mFileManager.FileLoading Then Exit Sub

        ProtocolCheckChanged(Me.TabDesign, Me.cbProtocol.Checked)

        mFileManager.FileEdited = True

    End Sub

    Private Sub cbStructurePersonState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStructurePersonState.CheckedChanged

        If cbStructurePersonState.Checked Then
            'cannot have rootstate - 'Person State With EventSeries'
            Me.cbPersonState.Enabled = False

            If mFileManager.FileLoading Then Exit Sub

            If mTabPagesCollection.Contains("tpStateStructure") Then
                ' no State page added
                Me.TabStructure.TabPages.Add(Me.mTabPagesCollection.Item("tpStateStructure"))
            Else
                ' no State page added
                Dim tp As New Crownwood.Magic.Controls.TabPage

                mTabPageDataStateStructure = New TabPageStructure '(Me)
                mTabPageDataStateStructure.IsState = True ' makes assumed value button/text box visible
                'Adding it to this collection allows it to be removed
                ' and then readded without losing the data
                mComponentsCollection.Add(mTabPageDataStateStructure)
                tp.Controls.Add(mTabPageDataStateStructure)
                mTabPageDataStateStructure.Dock = DockStyle.Fill
                tp.BackColor = System.Drawing.Color.RoyalBlue
                tp.Name = "tpStateStructure"
                ' add it to the collection incase it is needed again
                mTabPagesCollection.Add(tp.Name, tp)
                tp.Title = AE_Constants.Instance.Person_state
                Me.TabStructure.TabPages.Add(tp)
                Me.HelpProviderDesigner.SetHelpNavigator(tp, HelpNavigator.Topic)
                Me.HelpProviderDesigner.SetHelpKeyword(tp, "HowTo/edit_state.htm")
            End If
            ' now set the selected tab page to this one
            Dim i As Integer
            For i = 0 To Me.TabStructure.TabPages.Count - 1
                If Me.TabStructure.TabPages(i).Name = "tpStateStructure" Then
                    Me.TabStructure.SelectedIndex = i
                End If
            Next

        Else

            If mFileManager.FileLoading Then
                Me.cbPersonState.Enabled = True
            Else
                Dim tp As Crownwood.Magic.Controls.TabPage

                If MessageBox.Show(AE_Constants.Instance.Remove_state, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = _
                    Windows.Forms.DialogResult.OK Then
                    Me.cbPersonState.Enabled = True
                    For Each tp In Me.TabStructure.TabPages
                        If tp.Name = "tpStateStructure" Then
                            Me.TabStructure.TabPages.Remove(tp)
                            Exit For
                        End If
                    Next
                    mFileManager.FileEdited = True
                Else
                    'cancel the checkChanged
                    mFileManager.FileLoading = True
                    Me.cbStructurePersonState.Checked = True
                    mFileManager.FileLoading = False
                End If
            End If
        End If
    End Sub

    Private Sub cbPersonState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPersonState.CheckedChanged

        ' ensure in interactive state
        If mFileManager.FileLoading Then Return

        If cbPersonState.Checked Then
            'cannot have state associated with structure
            Me.cbStructurePersonState.Enabled = False

            If Me.mTabPagesCollection.Contains("tpRootState") Then
                Me.TabDesign.TabPages.Add(Me.mTabPagesCollection.Item("tpRootState"))
            Else
                Me.TabDesign.TabPages.Add(Me.mBaseTabPagesCollection.Item("tpRootState"))
            End If

            Me.tpRootState.Visible = True
            If Me.tpRootStateStructure.Controls.Count = 0 Then
                ' add a TabPageStructure component
                mTabPageStateStructure = New TabPageStructure '(Me)
                mTabPageStateStructure.IsState = True ' makes assumed value button/text box visible
                Me.tpRootStateStructure.Controls.Add(mTabPageStateStructure)
                mTabPageStateStructure.Dock = DockStyle.Fill

                mTabPageStateEventSeries = New TabpageHistory
                mTabPageStateEventSeries.BackColor = System.Drawing.Color.LightSteelBlue
                mTabPageStateEventSeries.NodeId = mFileManager.OntologyManager.AddTerm("State Event Series", "@ internal @").Code
                mTabPageStateEventSeries.AddBaseLineEvent()

                Me.tpRootStateEventSeries.Controls.Add(mTabPageStateEventSeries)
                mTabPageStateEventSeries.Dock = DockStyle.Fill
            End If
        Else
            Dim tp As Crownwood.Magic.Controls.TabPage
            If MessageBox.Show(AE_Constants.Instance.Remove_state, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = _
                Windows.Forms.DialogResult.OK Then

                Me.cbStructurePersonState.Enabled = True
                For Each tp In Me.TabDesign.TabPages
                    If tp.Name = "tpRootState" Then
                        If Not mTabPagesCollection.Contains("tpRootState") Then
                            mTabPagesCollection.Add(tp.Name, tp)
                        End If
                        Me.TabDesign.TabPages.Remove(tp)
                        Exit For
                    End If
                Next
            Else
                'cancel the checkChanged
                mFileManager.FileLoading = True
                Me.cbPersonState.Checked = True
                mFileManager.FileLoading = False
            End If
        End If

        mFileManager.FileEdited = True

    End Sub

    Private Sub TabMain_SelectionChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabMain.SelectionChanging
        ' catch any refreshes that are required
        If TabMain.SelectedTab Is Me.tpTerminology Then
            ' edits of terms in the table may have taken place and will need to be 
            ' reflected in the GUI (via Translate)

            If Not mFileManager.FileLoading Then
                ' if this is not due to a file load (e.g. opening a file)

                If Not mFileManager.OntologyManager.TermDefinitionTable.GetChanges Is Nothing Then
                    ' and there have been some changes to the table
                    mFileManager.OntologyManager.TermDefinitionTable.AcceptChanges()
                    mFileManager.FileEdited = True

                    Me.Translate()

                End If

            End If

        ElseIf TabMain.SelectedTab Is Me.tpInterface Then
            'JAR: 01JUN07, EDT-24 Interface tab does not release UID objects which causes crash
            'force garbage collection
            'tpInterface.Dispose() 'Note: Me.tpInterface.Controls.Clear() and tpInterface = Nothing have no affect on held objects!
            'GC.Collect()
            'GC.WaitForPendingFinalizers()

            'next time visit tab, tab is empty!
            'BuildTab()

        ElseIf TabMain.SelectedTab Is Me.tpDescription Then
            'Ensure the description is up to date
            RichTextBoxDescription.Rtf = mTabPageDescription.AsRtfString()
            RichTextBoxUnicode.ProcessRichEditControl(RichTextBoxDescription, mFileManager, mTabPageDescription) 'JAR: 13APR07, EDT-32 Support unicode
        End If
    End Sub

    'JAR: 01JUN07, EDT-24 Interface tab does not release UID objects which causes crash
    'Private Sub BuildTab() 'tpInterface is unusable after the dispose
    '    Me.tpInterface = New Crownwood.Magic.Controls.TabPage

    '    Me.tpInterface.SuspendLayout()

    '    Me.tpInterface.AutoScroll = True
    '    Me.tpInterface.Controls.Add(Me.cbMandatory)
    '    Me.HelpProviderDesigner.SetHelpKeyword(Me.tpInterface, "Screens/interface_screen.html")
    '    Me.HelpProviderDesigner.SetHelpNavigator(Me.tpInterface, System.Windows.Forms.HelpNavigator.Topic)
    '    Me.tpInterface.Location = New System.Drawing.Point(0, 0)
    '    Me.tpInterface.Name = "tpInterface"
    '    Me.tpInterface.Selected = False
    '    Me.HelpProviderDesigner.SetShowHelp(Me.tpInterface, True)
    '    Me.tpInterface.Size = New System.Drawing.Size(969, 595)
    '    Me.tpInterface.TabIndex = 5
    '    Me.tpInterface.Title = "Interface"
    '    Me.tpInterface.ResumeLayout(False)

    '    Me.tpInterface.Title = Filemanager.GetOpenEhrTerm(84, Me.tpInterface.Title) 'language?

    '    TabMain.SuspendLayout()
    '    TabMain.TabPages.Remove(TabMain.TabPages.Item("Interface"))
    '    TabMain.TabPages.Add(tpInterface)        
    '    TabMain.ResumeLayout()

    '    'Me.TabMain.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpHeader, Me.tpDesign, Me.tpSectionPage, Me.tpTerminology, Me.tpText, Me.tpInterface, Me.tpDescription})

    'End Sub

    Private Sub TabMain_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabMain.SelectionChanged

        If TabMain.SelectedTab Is tpText Then
            If Me.mRichTextArchetype.Text = "" Then
                For Each tbb As System.Windows.Forms.ToolBarButton In Me.ToolBarRTF.Buttons
                    tbb.Pushed = False
                Next
                Me.butRTF.Pushed = True
                WriteRichText()
            Else
                RefreshRichText()
            End If

        ElseIf Me.TabMain.SelectedTab Is tpInterface Then
            BuildInterface()
            Me.cbMandatory.BringToFront()

        ElseIf Me.TabMain.SelectedTab Is tpTerminology Then
            ' rebuild the ParseTree to ensure the
            ' paths are all available
            If mFileManager.FileEdited Then
                PrepareToSave()
                mTermBindingPanel.PopulatePathTree()
            End If
            'CHANGE - Sam Heard - 2004-05-21
            ' accept all changes in the termdefinitions table
            'so can test if any changes made
            mFileManager.OntologyManager.TermDefinitionTable.AcceptChanges()

        End If
    End Sub

    Private Sub butAddTerminology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddTerminology.Click, MenuTerminologyAdd.Click
        'If ChooseTerminology() Then
        If OceanArchetypeEditor.Instance.AddTerminology() Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub DataGridConstraintDefinitions_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridConstraintDefinitions.CurrentCellChanged
        ' set the mDataViewConstraintBindings to the appropriate term
        Dim ID As String

        If Me.DataGridConstraintDefinitions.CurrentRowIndex > -1 Then
            If Me.DataGridConstraintDefinitions.VisibleRowCount > 0 AndAlso _
            (Not TypeOf (Me.DataGridConstraintDefinitions.Item(Me.DataGridConstraintDefinitions.CurrentRowIndex, 0)) Is System.DBNull) Then
                Try
                    ID = Me.DataGridConstraintDefinitions.Item(Me.DataGridConstraintDefinitions.CurrentRowIndex, 0)
                    If Not ID Is Nothing Then
                        mDataViewConstraintBindings.RowFilter = "ID = '" & ID & "'"
                    End If
                Catch
                    ' if it falls off the grid to an empty row
                    Debug.Assert(False)
                End Try
            End If
        End If

    End Sub

    Private Sub butLookUpConstraint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLookUpConstraint.Click
        Dim s As String = Filemanager.GetOpenEhrTerm(99, "Add binding")

        Dim frm As New ConstraintBindingForm

        If Me.DataGridConstraintDefinitions.VisibleRowCount > 0 AndAlso Me.DataGridConstraintDefinitions.CurrentRowIndex > -1 Then
            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                frm.Text = s
                frm.lblQueryName.Text = Filemanager.GetOpenEhrTerm(624, frm.lblQueryName.Text)
                frm.lblRelease.Text = Filemanager.GetOpenEhrTerm(97, frm.lblRelease.Text)
                frm.lblTerminology.Text = AE_Constants.Instance.Terminology
                frm.butCancel.Text = AE_Constants.Instance.Cancel
                frm.butOK.Text = AE_Constants.Instance.OK
            End If

            While frm.ShowDialog(Me.ParentForm) = Windows.Forms.DialogResult.OK
                If (frm.comboTerminology.SelectedIndex > -1 And frm.txtQuery.Text <> "") Then

                    'Add the terminology
                    If Not mFileManager.OntologyManager.HasTerminology(frm.comboTerminology.SelectedValue) Then
                        mFileManager.OntologyManager.AddTerminology(frm.comboTerminology.SelectedValue, frm.comboTerminology.SelectedText)
                    End If

                    'Then the binding
                    Dim new_row As DataRow
                    Dim acCode As String = Me.DataGridConstraintDefinitions.Item _
                        (Me.DataGridConstraintDefinitions.CurrentRowIndex, 0)

                    If RmTerm.isValidTermCode(acCode) AndAlso frm.txtQuery.Text <> "" Then
                        new_row = Filemanager.Master.OntologyManager.ConstraintBindingsTable.NewRow
                        new_row(0) = frm.comboTerminology.SelectedValue
                        new_row(1) = acCode
                        new_row(2) = "http://openEHR.org/" & frm.txtQuery.Text
                        'new_row(3) = frm.txtRelease.Text
                        Dim keys(1) As Object
                        keys(0) = new_row(0)
                        keys(1) = new_row(1)

                        If Filemanager.Master.OntologyManager.ConstraintBindingsTable.Rows.Contains(keys) Then
                            'change the constraint
                            Dim row_to_change As DataRow = Filemanager.Master.OntologyManager.ConstraintBindingsTable.Rows.Find(keys)
                            row_to_change.BeginEdit()
                            row_to_change(2) = new_row(2)
                            row_to_change.EndEdit()
                        Else
                            Filemanager.Master.OntologyManager.ConstraintBindingsTable.Rows.Add(new_row)
                        End If

                        Filemanager.Master.FileEdited = True
                    End If
                    Return
                Else
                    MessageBox.Show(AE_Constants.Instance.Add_Reference, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End While
        Else
            MessageBox.Show(AE_Constants.Instance.Add_Reference, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Sub BuildInterface()
        ' build the data

        Me.tpInterface.Controls.Clear()

        ' ? use tabcontrol in the future for protocol and state
        Select Case mFileManager.Archetype.RmEntity
            Case StructureType.ENTRY, StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.ADMIN_ENTRY
                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Me.chkEventSeries.Checked Then
                    Dim gb As New GroupBox
                    Dim rel_pos As New Point

                    gb.Text = Filemanager.GetOpenEhrTerm(275, "History")

                    gb.FlatStyle = FlatStyle.Popup
                    pos.X = 10
                    gb.Location = pos
                    rel_pos.X = 20
                    rel_pos.Y = 20
                    If Not Me.mTabPageDataEventSeries Is Nothing Then
                        Me.mTabPageDataEventSeries.BuildInterface(gb, rel_pos, Me.cbMandatory.Checked)
                    End If
                    gb.Height = gb.Height + 10
                    Me.tpInterface.Controls.Add(gb)
                    pos.Y = pos.Y + gb.Height + 10
                End If

                If Not Me.mTabPageDataStructure Is Nothing Then
                    Me.mTabPageDataStructure.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If

                If Me.cbStructurePersonState.Checked Then
                    Dim gb As New GroupBox
                    Dim rel_pos As New Point

                    gb.Text = Filemanager.GetOpenEhrTerm(177, "State")
                    gb.FlatStyle = FlatStyle.Popup
                    pos.X = 10
                    gb.Location = pos
                    rel_pos.X = 20
                    rel_pos.Y = 20
                    Me.mTabPageDataStateStructure.BuildInterface(gb, rel_pos, Me.cbMandatory.Checked)
                    gb.Height = gb.Height + 10
                    If gb.Controls.Count > 0 Then
                        Me.tpInterface.Controls.Add(gb)
                    End If
                    pos.Y = pos.Y + gb.Height + 10
                End If

                If Me.cbPersonState.Checked Then
                    Dim gb, hist As GroupBox
                    Dim rel_pos As New Point
                    gb = New GroupBox
                    gb.Text = Filemanager.GetOpenEhrTerm(79, "Person State with History")
                    gb.FlatStyle = FlatStyle.Popup
                    ' add EventSeries
                    pos.X = 10
                    gb.Location = pos

                    ' now add the EventSeries element
                    hist = New GroupBox
                    Dim hist_pos As New Point
                    hist.Text = Filemanager.GetOpenEhrTerm(275, "History")
                    hist.FlatStyle = FlatStyle.Popup
                    hist_pos.X = 20
                    hist_pos.Y = 20
                    hist.Location = hist_pos
                    rel_pos.X = 20
                    rel_pos.Y = 20
                    If Not Me.mTabPageStateEventSeries Is Nothing Then
                        Me.mTabPageStateEventSeries.BuildInterface(hist, rel_pos, Me.cbMandatory.Checked)
                    End If
                    gb.Controls.Add(hist)
                    gb.Height = hist.Height + 10
                    pos.Y = pos.Y + hist.Height + 10
                    rel_pos.X = 20
                    rel_pos.Y = rel_pos.Y + hist.Height + 10
                    Me.mTabPageStateStructure.BuildInterface(gb, rel_pos, cbMandatory.Checked)
                    gb.Height = gb.Height + 10
                    If gb.Controls.Count > 0 Then
                        Me.tpInterface.Controls.Add(gb)
                    End If
                    pos.Y = pos.Y + gb.Height + 10
                End If

                If Me.cbProtocol.Checked Then
                    Dim gb As New GroupBox
                    Dim rel_pos As New Point

                    gb.Text = Filemanager.GetOpenEhrTerm(78, "Protocol")
                    gb.FlatStyle = FlatStyle.Popup
                    pos.X = 10
                    gb.Location = pos
                    rel_pos.X = 20
                    rel_pos.Y = 20
                    If Not Me.mTabPageProtocolStructure Is Nothing Then
                        Me.mTabPageProtocolStructure.BuildInterface(gb, rel_pos, Me.cbMandatory.Checked)
                    End If
                    gb.Height = gb.Height + 10
                    If gb.Controls.Count > 0 Then
                        Me.tpInterface.Controls.Add(gb)
                    End If
                    pos.Y = pos.Y + gb.Height + 10
                End If

            Case StructureType.INSTRUCTION

                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Not Me.mTabPageInstruction Is Nothing Then
                    Me.mTabPageInstruction.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If

            Case StructureType.ACTION

                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Not Me.mTabPageAction Is Nothing Then
                    Me.mTabPageAction.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If

            Case StructureType.Cluster
                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Not Me.mTabPageDataStructure Is Nothing Then
                    Me.mTabPageDataStructure.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If

            Case StructureType.Element
                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Not Me.mTabPageDataStructure Is Nothing Then
                    Me.mTabPageDataStructure.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If

            Case StructureType.SECTION

            Case StructureType.COMPOSITION
                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Not Me.mTabPageComposition Is Nothing Then
                    Me.mTabPageComposition.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If

            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                Dim pos As New Point
                pos.X = 10
                pos.Y = 10

                If Not Me.mTabPageDataStructure Is Nothing Then
                    Me.mTabPageDataStructure.BuildInterface(tpInterface, pos, Me.cbMandatory.Checked)
                End If
        End Select

        If Me.RightToLeft = Windows.Forms.RightToLeft.Yes Then
            OceanArchetypeEditor.Reflect(tpInterface)
        End If
        'Put back the mandatory text box
        Me.tpInterface.Controls.Add(Me.cbMandatory)

    End Sub

    Sub RefreshRichText()
        ' refreshes the output of the rich text box
        Dim s As String = ""

        For Each tbb As System.Windows.Forms.ToolBarButton In Me.ToolBarRTF.Buttons
            If tbb.Pushed Then
                s = CType(tbb.Tag, String).ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Exit For
            End If
        Next

        Select Case s
            Case "save", "print", "html", ""
                ' do nothing
            Case Else
                If s = "rtf" Then
                    WriteRichText()
                Else
                    Me.PrepareToSave()
                    If s = mFileManager.ParserType.ToLowerInvariant() Then
                        Me.mRichTextArchetype.Text = mFileManager.Archetype.SerialisedArchetype(s)
                    Else
                        Me.mRichTextArchetype.Text = mFileManager.ExportSerialised(s)
                    End If
                End If
        End Select

    End Sub

    Private Sub ToolBarRTF_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBarRTF.ButtonClick

        Static Dim format As String = "rtf" ' remember the format that has been set
        Dim saveFile As New SaveFileDialog

        Dim s As String = CStr(e.Button.Tag).ToLower(System.Globalization.CultureInfo.InvariantCulture)

        ' buttons are tagged with the file extension and the fixed buttons are tagged with
        ' standard english text.

        Select Case s
            Case "save"

                Dim ds As New Windows.Forms.RichTextBoxStreamType

                saveFile.Filter = format.ToUpper(System.Globalization.CultureInfo.InvariantCulture) & "|*." & format
                saveFile.FileName = mFileManager.Archetype.Archetype_ID.ToString & "." & format
                saveFile.OverwritePrompt = True
                saveFile.AddExtension = True
                saveFile.Title = AE_Constants.Instance.MessageBoxCaption
                saveFile.ValidateNames = True
                If saveFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Select Case format
                        Case "rtf"
                            ds = RichTextBoxStreamType.RichNoOleObjs
                        Case Else
                            ds = RichTextBoxStreamType.PlainText
                    End Select

                    Try
                        Me.mRichTextArchetype.SaveFile(saveFile.FileName, ds)
                    Catch
                        MessageBox.Show(AE_Constants.Instance.Error_saving & mFileManager.Archetype.Archetype_ID.ToString & ".rtf", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Case "print"
                Me.mRichTextArchetype.Print()

            Case "html"
                Try
                    WriteToHTML("\HTML\temp.html")
                    Process.Start("file://" & Application.StartupPath & "\HTML\temp.html")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Case Else
                ' remember this for when the save button is pushed
                format = s

                ' toggle the buttons
                For Each bt As ToolBarButton In ToolBarRTF.Buttons
                    If Not bt Is e.Button Then
                        bt.Pushed = False
                    End If
                Next

                If s = "rtf" Then
                    WriteRichText()
                Else
                    If s = mFileManager.ParserType.ToLowerInvariant() Then
                        Me.PrepareToSave()
                        Me.mRichTextArchetype.Text = mFileManager.Archetype.SerialisedArchetype(s)
                    Else
                        Me.mRichTextArchetype.Text = mFileManager.ExportSerialised(s)
                    End If
                End If
        End Select
    End Sub


#End Region

    Private Sub menuDisplayPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDisplayPrint.Click
        Me.mRichTextArchetype.Print()
    End Sub

    Private Sub menuEditArchID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuEditArchID.Click
        Dim arch_id As String = mFileManager.Archetype.Archetype_ID.Concept
        Dim i As Integer = arch_id.LastIndexOf("-")
        Dim new_concept As String

        new_concept = OceanArchetypeEditor.Instance.GetInput(Filemanager.GetOpenEhrTerm(54, "Concept"), Me)

        'JAR: 22MAY07, EDT-41 Validate archetype ID  
        'If new_concept = "" Then
        '    Return
        'End If

        'If i > -1 Then
        '    new_concept = arch_id.Substring(0, i + 1) + new_concept.Replace("-", "_")
        'Else
        '    new_concept = new_concept.Replace(" ", "_")
        'End If

        'mFileManager.Archetype.Archetype_ID.Concept = new_concept.ToLowerInvariant

        If mFileManager.Archetype.Archetype_ID.ValidConcept(new_concept, arch_id.ToString) Then 'Note: new_concept can be updated in ValidConcept!
            mFileManager.Archetype.Archetype_ID.Concept = new_concept
        Else
            Return
        End If

        ' force save as to new file
        mFileManager.IsNew = True
        mFileManager.FileEdited = True
        Me.lblArchetypeName.Text = mFileManager.Archetype.Archetype_ID.ToString
    End Sub

    Private Sub MenuDisplayFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDisplayFind.Click, MenuDisplayFindAgain.Click
        If sender Is MenuDisplayFind Then
            mFindString = OceanArchetypeEditor.Instance.GetInput(AE_Constants.Instance.Text, Me)
            If mFindString <> "" Then
                mFindStringFrom = Me.mRichTextArchetype.Find(mFindString)
                If mFindStringFrom > -1 Then
                    Me.mRichTextArchetype.Select(mFindStringFrom, mFindString.Length)
                End If
            End If
        Else
            mFindStringFrom = Me.mRichTextArchetype.Find(mFindString, mFindStringFrom + mFindString.Length, RichTextBoxFinds.None)
            If mFindStringFrom > -1 Then
                Me.mRichTextArchetype.Select(mFindStringFrom, mFindString.Length)
            Else
                Beep()
                Me.mRichTextArchetype.Select(0, 0)
            End If
        End If
    End Sub

    Private Sub ContextMenuDisplay_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuDisplay.Popup
        If mFindStringFrom <> -1 Then
            Me.MenuDisplayFindAgain.Visible = True
        Else
            Me.MenuDisplayFindAgain.Visible = False
        End If
    End Sub

    Private Sub cbMandatory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMandatory.CheckedChanged
        BuildInterface()
    End Sub

    Private Sub MenuFileExportCEN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MessageBox.Show(AE_Constants.Instance.Feature_not_available, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Designer_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        OceanArchetypeEditor.Reflect(Me)
    End Sub

    Private Sub menuFileExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFileExport.Select
        If Filemanager.Master.ParserType = "adl" Then
            MenuFileExportType.Text = "XML"
        Else
            'MenuFileExportType.Text = AE_Constants.Instance.Feature_not_available
            MenuFileExportType.Text = "ADL"
        End If
    End Sub

    Private Sub MenuFileExportType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFileExportType.Click
        Me.Cursor = Cursors.WaitCursor
        If MenuFileExportType.Text = "XML" Then
            Filemanager.Master.Export("XML")
        ElseIf MenuFileExportType.Text = "ADL" Then
            Filemanager.Master.Export("ADL")
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub RichTextBoxDescription_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBoxDescription.DoubleClick
        Me.TabMain.SelectedTab = Me.tpDescription
    End Sub

    Private Sub RichTextBoxDescription_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RichTextBoxDescription.KeyPress
        If (e.KeyChar > "0"c And e.KeyChar < "z"c) Then
            Me.TabMain.SelectedTab = Me.tpDescription
        End If
    End Sub

    Private Sub cbParticipation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbParticipation.CheckedChanged
        If cbParticipation.Checked Then
            If mTabPageParticipation Is Nothing Then
                mTabPageParticipation = New TabPageParticipation()
                tpParticipation.Controls.Add(mTabPageParticipation)
                mTabPageParticipation.Dock = DockStyle.Fill
            End If
            Me.TabDesign.TabPages.Add(Me.mBaseTabPagesCollection.Item("tpParticipation"))
        Else
            Me.TabDesign.TabPages.Remove(Me.tpParticipation)
        End If

    End Sub

    Private Sub butLinks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLinks.Click
        Dim frm As New Links
        frm.ShowDialog(Me)

    End Sub
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
'The Original Code is Designer.vb.
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

