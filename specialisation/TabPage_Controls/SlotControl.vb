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

Public Class SlotControl
    Inherits System.Windows.Forms.UserControl

    Private mRMStructure As RM_Structure
    Private mIsLoading As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        mIsLoading = True
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mIsLoading = False

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
    Friend WithEvents TabSlots As Crownwood.Magic.Controls.TabControl
    Friend WithEvents tpSectionSlots As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpEntrySlots As Crownwood.Magic.Controls.TabPage
    Friend WithEvents butExcludeEntryRemove As System.Windows.Forms.Button
    Friend WithEvents butIncludeEntryRemove As System.Windows.Forms.Button
    Friend WithEvents butExcludeEntrySave As System.Windows.Forms.Button
    Friend WithEvents butIncludeEntrySave As System.Windows.Forms.Button
    Friend WithEvents butExcludeEntry As System.Windows.Forms.Button
    Friend WithEvents butIncludeEntry As System.Windows.Forms.Button
    Friend WithEvents lblEntryExclude As System.Windows.Forms.Label
    Friend WithEvents LblInclude As System.Windows.Forms.Label
    Friend WithEvents txtExcludeEntry As System.Windows.Forms.TextBox
    Friend WithEvents txtIncludeEntry As System.Windows.Forms.TextBox
    Friend WithEvents listIncludeEntry As System.Windows.Forms.ListBox
    Friend WithEvents listExcludeEntry As System.Windows.Forms.ListBox
    Friend WithEvents butExcludeSectionRemove As System.Windows.Forms.Button
    Friend WithEvents butIncludeSectionRemove As System.Windows.Forms.Button
    Friend WithEvents butExcludeSectionSave As System.Windows.Forms.Button
    Friend WithEvents butIncludeSectionSave As System.Windows.Forms.Button
    Friend WithEvents butExcludeSection As System.Windows.Forms.Button
    Friend WithEvents butIncludeSection As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblIncludeSection As System.Windows.Forms.Label
    Friend WithEvents txtExcludeSection As System.Windows.Forms.TextBox
    Friend WithEvents listIncludeSection As System.Windows.Forms.ListBox
    Friend WithEvents listExcludeSection As System.Windows.Forms.ListBox
    Friend WithEvents txtIncludeSection As System.Windows.Forms.TextBox
    Friend WithEvents lblNumMax As System.Windows.Forms.Label
    Friend WithEvents lblNumMin As System.Windows.Forms.Label
    Friend WithEvents numMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbUnbounded As System.Windows.Forms.CheckBox
    Friend WithEvents panelSectionTop As System.Windows.Forms.Panel
    Friend WithEvents PanelSection As System.Windows.Forms.Panel
    Friend WithEvents txtRuntimeName As System.Windows.Forms.TextBox
    Friend WithEvents txtTermDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SlotControl))
        Me.panelSectionTop = New System.Windows.Forms.Panel
        Me.lblNumMax = New System.Windows.Forms.Label
        Me.lblNumMin = New System.Windows.Forms.Label
        Me.numMin = New System.Windows.Forms.NumericUpDown
        Me.numMax = New System.Windows.Forms.NumericUpDown
        Me.cbUnbounded = New System.Windows.Forms.CheckBox
        Me.TabSlots = New Crownwood.Magic.Controls.TabControl
        Me.tpEntrySlots = New Crownwood.Magic.Controls.TabPage
        Me.butExcludeEntryRemove = New System.Windows.Forms.Button
        Me.butIncludeEntryRemove = New System.Windows.Forms.Button
        Me.butExcludeEntrySave = New System.Windows.Forms.Button
        Me.butIncludeEntrySave = New System.Windows.Forms.Button
        Me.butExcludeEntry = New System.Windows.Forms.Button
        Me.butIncludeEntry = New System.Windows.Forms.Button
        Me.lblEntryExclude = New System.Windows.Forms.Label
        Me.LblInclude = New System.Windows.Forms.Label
        Me.txtExcludeEntry = New System.Windows.Forms.TextBox
        Me.txtIncludeEntry = New System.Windows.Forms.TextBox
        Me.listIncludeEntry = New System.Windows.Forms.ListBox
        Me.listExcludeEntry = New System.Windows.Forms.ListBox
        Me.tpSectionSlots = New Crownwood.Magic.Controls.TabPage
        Me.butExcludeSectionRemove = New System.Windows.Forms.Button
        Me.butIncludeSectionRemove = New System.Windows.Forms.Button
        Me.butExcludeSectionSave = New System.Windows.Forms.Button
        Me.butIncludeSectionSave = New System.Windows.Forms.Button
        Me.butExcludeSection = New System.Windows.Forms.Button
        Me.butIncludeSection = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblIncludeSection = New System.Windows.Forms.Label
        Me.txtExcludeSection = New System.Windows.Forms.TextBox
        Me.listIncludeSection = New System.Windows.Forms.ListBox
        Me.listExcludeSection = New System.Windows.Forms.ListBox
        Me.txtIncludeSection = New System.Windows.Forms.TextBox
        Me.PanelSection = New System.Windows.Forms.Panel
        Me.txtRuntimeName = New System.Windows.Forms.TextBox
        Me.txtTermDescription = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.panelSectionTop.SuspendLayout()
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpEntrySlots.SuspendLayout()
        Me.tpSectionSlots.SuspendLayout()
        Me.PanelSection.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelSectionTop
        '
        Me.panelSectionTop.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.panelSectionTop.Controls.Add(Me.lblNumMax)
        Me.panelSectionTop.Controls.Add(Me.lblNumMin)
        Me.panelSectionTop.Controls.Add(Me.numMin)
        Me.panelSectionTop.Controls.Add(Me.numMax)
        Me.panelSectionTop.Controls.Add(Me.cbUnbounded)
        Me.panelSectionTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelSectionTop.Location = New System.Drawing.Point(0, 0)
        Me.panelSectionTop.Name = "panelSectionTop"
        Me.panelSectionTop.Size = New System.Drawing.Size(424, 32)
        Me.panelSectionTop.TabIndex = 2
        '
        'lblNumMax
        '
        Me.lblNumMax.Location = New System.Drawing.Point(201, 8)
        Me.lblNumMax.Name = "lblNumMax"
        Me.lblNumMax.Size = New System.Drawing.Size(32, 16)
        Me.lblNumMax.TabIndex = 25
        Me.lblNumMax.Text = "Max:"
        Me.lblNumMax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumMin
        '
        Me.lblNumMin.BackColor = System.Drawing.Color.Transparent
        Me.lblNumMin.Location = New System.Drawing.Point(23, 8)
        Me.lblNumMin.Name = "lblNumMin"
        Me.lblNumMin.Size = New System.Drawing.Size(120, 16)
        Me.lblNumMin.TabIndex = 23
        Me.lblNumMin.Text = "Occurrences - Min:"
        Me.lblNumMin.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'numMin
        '
        Me.numMin.Location = New System.Drawing.Point(153, 6)
        Me.numMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMin.Name = "numMin"
        Me.numMin.Size = New System.Drawing.Size(40, 20)
        Me.numMin.TabIndex = 24
        '
        'numMax
        '
        Me.numMax.Location = New System.Drawing.Point(241, 6)
        Me.numMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMax.Name = "numMax"
        Me.numMax.Size = New System.Drawing.Size(48, 20)
        Me.numMax.TabIndex = 26
        Me.numMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cbUnbounded
        '
        Me.cbUnbounded.Location = New System.Drawing.Point(297, 8)
        Me.cbUnbounded.Name = "cbUnbounded"
        Me.cbUnbounded.Size = New System.Drawing.Size(112, 16)
        Me.cbUnbounded.TabIndex = 27
        Me.cbUnbounded.Text = "Unbounded"
        '
        'TabSlots
        '
        Me.TabSlots.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabSlots.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabSlots.Location = New System.Drawing.Point(0, 232)
        Me.TabSlots.Name = "TabSlots"
        Me.TabSlots.PositionTop = True
        Me.TabSlots.SelectedIndex = 0
        Me.TabSlots.SelectedTab = Me.tpEntrySlots
        Me.TabSlots.Size = New System.Drawing.Size(424, 184)
        Me.TabSlots.TabIndex = 3
        Me.TabSlots.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpEntrySlots, Me.tpSectionSlots})
        '
        'tpEntrySlots
        '
        Me.tpEntrySlots.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.tpEntrySlots.Controls.Add(Me.butExcludeEntryRemove)
        Me.tpEntrySlots.Controls.Add(Me.butIncludeEntryRemove)
        Me.tpEntrySlots.Controls.Add(Me.butExcludeEntrySave)
        Me.tpEntrySlots.Controls.Add(Me.butIncludeEntrySave)
        Me.tpEntrySlots.Controls.Add(Me.butExcludeEntry)
        Me.tpEntrySlots.Controls.Add(Me.butIncludeEntry)
        Me.tpEntrySlots.Controls.Add(Me.lblEntryExclude)
        Me.tpEntrySlots.Controls.Add(Me.LblInclude)
        Me.tpEntrySlots.Controls.Add(Me.txtExcludeEntry)
        Me.tpEntrySlots.Controls.Add(Me.txtIncludeEntry)
        Me.tpEntrySlots.Controls.Add(Me.listIncludeEntry)
        Me.tpEntrySlots.Controls.Add(Me.listExcludeEntry)
        Me.tpEntrySlots.Location = New System.Drawing.Point(0, 0)
        Me.tpEntrySlots.Name = "tpEntrySlots"
        Me.tpEntrySlots.Size = New System.Drawing.Size(424, 159)
        Me.tpEntrySlots.TabIndex = 1
        Me.tpEntrySlots.Title = "Entry Slot"
        '
        'butExcludeEntryRemove
        '
        Me.butExcludeEntryRemove.Image = CType(resources.GetObject("butExcludeEntryRemove.Image"), System.Drawing.Image)
        Me.butExcludeEntryRemove.Location = New System.Drawing.Point(25, 192)
        Me.butExcludeEntryRemove.Name = "butExcludeEntryRemove"
        Me.butExcludeEntryRemove.Size = New System.Drawing.Size(24, 24)
        Me.butExcludeEntryRemove.TabIndex = 27
        '
        'butIncludeEntryRemove
        '
        Me.butIncludeEntryRemove.Image = CType(resources.GetObject("butIncludeEntryRemove.Image"), System.Drawing.Image)
        Me.butIncludeEntryRemove.Location = New System.Drawing.Point(25, 56)
        Me.butIncludeEntryRemove.Name = "butIncludeEntryRemove"
        Me.butIncludeEntryRemove.Size = New System.Drawing.Size(24, 24)
        Me.butIncludeEntryRemove.TabIndex = 26
        '
        'butExcludeEntrySave
        '
        Me.butExcludeEntrySave.Image = CType(resources.GetObject("butExcludeEntrySave.Image"), System.Drawing.Image)
        Me.butExcludeEntrySave.Location = New System.Drawing.Point(25, 224)
        Me.butExcludeEntrySave.Name = "butExcludeEntrySave"
        Me.butExcludeEntrySave.Size = New System.Drawing.Size(24, 24)
        Me.butExcludeEntrySave.TabIndex = 25
        Me.butExcludeEntrySave.Visible = False
        '
        'butIncludeEntrySave
        '
        Me.butIncludeEntrySave.Image = CType(resources.GetObject("butIncludeEntrySave.Image"), System.Drawing.Image)
        Me.butIncludeEntrySave.Location = New System.Drawing.Point(25, 88)
        Me.butIncludeEntrySave.Name = "butIncludeEntrySave"
        Me.butIncludeEntrySave.Size = New System.Drawing.Size(24, 24)
        Me.butIncludeEntrySave.TabIndex = 24
        Me.butIncludeEntrySave.Visible = False
        '
        'butExcludeEntry
        '
        Me.butExcludeEntry.Image = CType(resources.GetObject("butExcludeEntry.Image"), System.Drawing.Image)
        Me.butExcludeEntry.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butExcludeEntry.Location = New System.Drawing.Point(25, 160)
        Me.butExcludeEntry.Name = "butExcludeEntry"
        Me.butExcludeEntry.Size = New System.Drawing.Size(24, 24)
        Me.butExcludeEntry.TabIndex = 23
        '
        'butIncludeEntry
        '
        Me.butIncludeEntry.Image = CType(resources.GetObject("butIncludeEntry.Image"), System.Drawing.Image)
        Me.butIncludeEntry.Location = New System.Drawing.Point(25, 24)
        Me.butIncludeEntry.Name = "butIncludeEntry"
        Me.butIncludeEntry.Size = New System.Drawing.Size(24, 24)
        Me.butIncludeEntry.TabIndex = 22
        '
        'lblEntryExclude
        '
        Me.lblEntryExclude.Location = New System.Drawing.Point(61, 140)
        Me.lblEntryExclude.Name = "lblEntryExclude"
        Me.lblEntryExclude.Size = New System.Drawing.Size(176, 16)
        Me.lblEntryExclude.TabIndex = 21
        Me.lblEntryExclude.Text = "Exclude entries:"
        '
        'LblInclude
        '
        Me.LblInclude.Location = New System.Drawing.Point(59, 5)
        Me.LblInclude.Name = "LblInclude"
        Me.LblInclude.Size = New System.Drawing.Size(176, 16)
        Me.LblInclude.TabIndex = 20
        Me.LblInclude.Text = "Include entries:"
        '
        'txtExcludeEntry
        '
        Me.txtExcludeEntry.ForeColor = System.Drawing.Color.DarkRed
        Me.txtExcludeEntry.Location = New System.Drawing.Point(59, 160)
        Me.txtExcludeEntry.Name = "txtExcludeEntry"
        Me.txtExcludeEntry.Size = New System.Drawing.Size(336, 23)
        Me.txtExcludeEntry.TabIndex = 19
        Me.txtExcludeEntry.Text = ""
        '
        'txtIncludeEntry
        '
        Me.txtIncludeEntry.ForeColor = System.Drawing.Color.DarkGreen
        Me.txtIncludeEntry.Location = New System.Drawing.Point(59, 24)
        Me.txtIncludeEntry.Name = "txtIncludeEntry"
        Me.txtIncludeEntry.Size = New System.Drawing.Size(336, 23)
        Me.txtIncludeEntry.TabIndex = 18
        Me.txtIncludeEntry.Text = ""
        '
        'listIncludeEntry
        '
        Me.listIncludeEntry.ForeColor = System.Drawing.Color.DarkGreen
        Me.listIncludeEntry.ItemHeight = 16
        Me.listIncludeEntry.Location = New System.Drawing.Point(59, 48)
        Me.listIncludeEntry.Name = "listIncludeEntry"
        Me.listIncludeEntry.Size = New System.Drawing.Size(336, 84)
        Me.listIncludeEntry.TabIndex = 17
        '
        'listExcludeEntry
        '
        Me.listExcludeEntry.ForeColor = System.Drawing.Color.DarkRed
        Me.listExcludeEntry.ItemHeight = 16
        Me.listExcludeEntry.Location = New System.Drawing.Point(59, 184)
        Me.listExcludeEntry.Name = "listExcludeEntry"
        Me.listExcludeEntry.Size = New System.Drawing.Size(336, 84)
        Me.listExcludeEntry.TabIndex = 16
        '
        'tpSectionSlots
        '
        Me.tpSectionSlots.BackColor = System.Drawing.Color.LemonChiffon
        Me.tpSectionSlots.Controls.Add(Me.butExcludeSectionRemove)
        Me.tpSectionSlots.Controls.Add(Me.butIncludeSectionRemove)
        Me.tpSectionSlots.Controls.Add(Me.butExcludeSectionSave)
        Me.tpSectionSlots.Controls.Add(Me.butIncludeSectionSave)
        Me.tpSectionSlots.Controls.Add(Me.butExcludeSection)
        Me.tpSectionSlots.Controls.Add(Me.butIncludeSection)
        Me.tpSectionSlots.Controls.Add(Me.Label1)
        Me.tpSectionSlots.Controls.Add(Me.lblIncludeSection)
        Me.tpSectionSlots.Controls.Add(Me.txtExcludeSection)
        Me.tpSectionSlots.Controls.Add(Me.listIncludeSection)
        Me.tpSectionSlots.Controls.Add(Me.listExcludeSection)
        Me.tpSectionSlots.Controls.Add(Me.txtIncludeSection)
        Me.tpSectionSlots.Location = New System.Drawing.Point(0, 0)
        Me.tpSectionSlots.Name = "tpSectionSlots"
        Me.tpSectionSlots.Selected = False
        Me.tpSectionSlots.Size = New System.Drawing.Size(424, 159)
        Me.tpSectionSlots.TabIndex = 0
        Me.tpSectionSlots.Title = "Section slot"
        '
        'butExcludeSectionRemove
        '
        Me.butExcludeSectionRemove.Image = CType(resources.GetObject("butExcludeSectionRemove.Image"), System.Drawing.Image)
        Me.butExcludeSectionRemove.Location = New System.Drawing.Point(25, 198)
        Me.butExcludeSectionRemove.Name = "butExcludeSectionRemove"
        Me.butExcludeSectionRemove.Size = New System.Drawing.Size(24, 24)
        Me.butExcludeSectionRemove.TabIndex = 28
        '
        'butIncludeSectionRemove
        '
        Me.butIncludeSectionRemove.Image = CType(resources.GetObject("butIncludeSectionRemove.Image"), System.Drawing.Image)
        Me.butIncludeSectionRemove.Location = New System.Drawing.Point(25, 54)
        Me.butIncludeSectionRemove.Name = "butIncludeSectionRemove"
        Me.butIncludeSectionRemove.Size = New System.Drawing.Size(24, 24)
        Me.butIncludeSectionRemove.TabIndex = 27
        '
        'butExcludeSectionSave
        '
        Me.butExcludeSectionSave.Image = CType(resources.GetObject("butExcludeSectionSave.Image"), System.Drawing.Image)
        Me.butExcludeSectionSave.Location = New System.Drawing.Point(25, 230)
        Me.butExcludeSectionSave.Name = "butExcludeSectionSave"
        Me.butExcludeSectionSave.Size = New System.Drawing.Size(24, 24)
        Me.butExcludeSectionSave.TabIndex = 26
        Me.butExcludeSectionSave.Visible = False
        '
        'butIncludeSectionSave
        '
        Me.butIncludeSectionSave.Image = CType(resources.GetObject("butIncludeSectionSave.Image"), System.Drawing.Image)
        Me.butIncludeSectionSave.Location = New System.Drawing.Point(25, 86)
        Me.butIncludeSectionSave.Name = "butIncludeSectionSave"
        Me.butIncludeSectionSave.Size = New System.Drawing.Size(24, 24)
        Me.butIncludeSectionSave.TabIndex = 25
        Me.butIncludeSectionSave.Visible = False
        '
        'butExcludeSection
        '
        Me.butExcludeSection.Image = CType(resources.GetObject("butExcludeSection.Image"), System.Drawing.Image)
        Me.butExcludeSection.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butExcludeSection.Location = New System.Drawing.Point(25, 166)
        Me.butExcludeSection.Name = "butExcludeSection"
        Me.butExcludeSection.Size = New System.Drawing.Size(22, 24)
        Me.butExcludeSection.TabIndex = 24
        '
        'butIncludeSection
        '
        Me.butIncludeSection.Image = CType(resources.GetObject("butIncludeSection.Image"), System.Drawing.Image)
        Me.butIncludeSection.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butIncludeSection.Location = New System.Drawing.Point(25, 24)
        Me.butIncludeSection.Name = "butIncludeSection"
        Me.butIncludeSection.Size = New System.Drawing.Size(23, 24)
        Me.butIncludeSection.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(54, 142)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 16)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Exclude sections:"
        '
        'lblIncludeSection
        '
        Me.lblIncludeSection.Location = New System.Drawing.Point(54, 6)
        Me.lblIncludeSection.Name = "lblIncludeSection"
        Me.lblIncludeSection.Size = New System.Drawing.Size(176, 16)
        Me.lblIncludeSection.TabIndex = 21
        Me.lblIncludeSection.Text = "Include sections:"
        '
        'txtExcludeSection
        '
        Me.txtExcludeSection.ForeColor = System.Drawing.Color.DarkRed
        Me.txtExcludeSection.Location = New System.Drawing.Point(54, 164)
        Me.txtExcludeSection.Name = "txtExcludeSection"
        Me.txtExcludeSection.Size = New System.Drawing.Size(336, 23)
        Me.txtExcludeSection.TabIndex = 20
        Me.txtExcludeSection.Text = ""
        '
        'listIncludeSection
        '
        Me.listIncludeSection.ForeColor = System.Drawing.Color.DarkGreen
        Me.listIncludeSection.ItemHeight = 16
        Me.listIncludeSection.Location = New System.Drawing.Point(54, 52)
        Me.listIncludeSection.Name = "listIncludeSection"
        Me.listIncludeSection.Size = New System.Drawing.Size(336, 84)
        Me.listIncludeSection.TabIndex = 19
        '
        'listExcludeSection
        '
        Me.listExcludeSection.ForeColor = System.Drawing.Color.DarkRed
        Me.listExcludeSection.ItemHeight = 16
        Me.listExcludeSection.Location = New System.Drawing.Point(54, 188)
        Me.listExcludeSection.Name = "listExcludeSection"
        Me.listExcludeSection.Size = New System.Drawing.Size(336, 84)
        Me.listExcludeSection.TabIndex = 18
        '
        'txtIncludeSection
        '
        Me.txtIncludeSection.ForeColor = System.Drawing.Color.DarkGreen
        Me.txtIncludeSection.Location = New System.Drawing.Point(54, 24)
        Me.txtIncludeSection.Name = "txtIncludeSection"
        Me.txtIncludeSection.Size = New System.Drawing.Size(336, 23)
        Me.txtIncludeSection.TabIndex = 17
        Me.txtIncludeSection.Text = ""
        '
        'PanelSection
        '
        Me.PanelSection.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.PanelSection.Controls.Add(Me.txtRuntimeName)
        Me.PanelSection.Controls.Add(Me.txtTermDescription)
        Me.PanelSection.Controls.Add(Me.Label11)
        Me.PanelSection.Controls.Add(Me.Label12)
        Me.PanelSection.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSection.Location = New System.Drawing.Point(0, 32)
        Me.PanelSection.Name = "PanelSection"
        Me.PanelSection.Size = New System.Drawing.Size(424, 80)
        Me.PanelSection.TabIndex = 4
        '
        'txtRuntimeName
        '
        Me.txtRuntimeName.Location = New System.Drawing.Point(128, 48)
        Me.txtRuntimeName.Name = "txtRuntimeName"
        Me.txtRuntimeName.Size = New System.Drawing.Size(223, 20)
        Me.txtRuntimeName.TabIndex = 33
        Me.txtRuntimeName.Text = ""
        '
        'txtTermDescription
        '
        Me.txtTermDescription.Location = New System.Drawing.Point(128, 8)
        Me.txtTermDescription.Multiline = True
        Me.txtTermDescription.Name = "txtTermDescription"
        Me.txtTermDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermDescription.Size = New System.Drawing.Size(224, 38)
        Me.txtTermDescription.TabIndex = 31
        Me.txtTermDescription.Text = ""
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(16, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 26)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Runtime name constraint:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(24, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 16)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "Description:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SlotControl
        '
        Me.Controls.Add(Me.TabSlots)
        Me.Controls.Add(Me.PanelSection)
        Me.Controls.Add(Me.panelSectionTop)
        Me.Name = "SlotControl"
        Me.Size = New System.Drawing.Size(424, 416)
        Me.panelSectionTop.ResumeLayout(False)
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpEntrySlots.ResumeLayout(False)
        Me.tpSectionSlots.ResumeLayout(False)
        Me.PanelSection.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Sub ShowConstraints(ByVal a_node As TreeNode)
        Dim occ As Count

        Me.Visible = True
        Me.reset()
        mIsLoading = True

        If TypeOf a_node Is ArchetypeCompoundTreeNode Then
            Dim n As ArchetypeCompoundTreeNode = a_node
            mRMStructure = n.RM_Class
            Me.PanelSection.Visible = True
            Me.TabSlots.Visible = False
            Me.txtRuntimeName.Text = n.RuntimeNameText
            Me.txtTermDescription.Text = n.Description
            occ = n.Occurrences

        ElseIf TypeOf a_node Is ArchetypeSlotTreeNode Then
            Dim n As ArchetypeSlotTreeNode = a_node
            mRMStructure = n.Slot
            Me.PanelSection.Visible = False
            Me.TabSlots.Visible = True
            occ = n.Slot.Occurrences

        Else
            Debug.Assert(False)
            Me.Visible = False
            Return
        End If


        'set the occurrences
        If occ.IsUnbounded Then
            Me.cbUnbounded.Checked = True
        Else
            Me.cbUnbounded.Checked = False
        End If

        If occ.MaxCount > 0 Then
            Me.numMax.Value = occ.MaxCount
        End If

        Me.numMin.Value = occ.MinCount
        mIsLoading = False
    End Sub

    Private Sub txtRuntimeName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRuntimeName.TextChanged
        If Not mIsLoading Then
            Dim a_Term As Term
            Debug.Assert(mRMStructure.Type = StructureType.RunTime)
            Dim rm As RM_Structure_RunTime = CType(mRMStructure, RM_Structure_RunTime)

            If rm.RuntimeName <> "" Then
                a_Term = OntologyManager.Instance.AddConstraint(Me.txtRuntimeName.Text)
                CType(mRMStructure, RM_Structure_RunTime).RuntimeName = a_Term.Text
            Else
                OntologyManager.Instance.SetText(txtRuntimeName.Text, rm.RuntimeName)
            End If
            FileManager.Instance.FileEdited = True
        End If
    End Sub

    Private Sub numMin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMin.ValueChanged
        If Not mIsLoading Then
            If numMin.Value > numMax.Value Then
                numMax.Value = numMin.Value
            End If
            mRMStructure.Occurrences.MinCount = Me.numMin.Value
            FileManager.Instance.FileEdited = True
        End If
    End Sub

    Private Sub numMax_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMax.ValueChanged

        If Not mIsLoading Then
            mRMStructure.Occurrences.MaxCount = Me.numMax.Value
            FileManager.Instance.FileEdited = True
            If numMax.Value < numMin.Value Then
                numMin.Value = numMax.Value
            End If
        End If

    End Sub

    Private Sub cbUnbounded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUnbounded.CheckedChanged
        If Me.cbUnbounded.Checked Then
            Me.numMax.Visible = False
            Me.lblNumMax.Visible = False

            If mIsLoading Then Return

            mRMStructure.Occurrences.IsUnbounded = True
        Else
            Me.numMax.Visible = True
            Me.lblNumMax.Visible = True

            If mIsLoading Then Return

            mRMStructure.Occurrences.MaxCount = Me.numMax.Value
            mRMStructure.Occurrences.IsUnbounded = False
        End If
        FileManager.Instance.FileEdited = True

    End Sub

    Private Sub txtTermDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Debug.Assert(mRMStructure.Type = StructureType.RunTime)
        If mIsLoading Then
            Try
                OntologyManager.Instance.SetText(Me.txtTermDescription.Text, mRMStructure.NodeId)
                FileManager.Instance.FileEdited = True
            Catch
                ' fixme throw error
            End Try
        End If

    End Sub

    Sub reset()
        ' reset the tab page
        Me.txtExcludeEntry.Text = ""
        Me.txtExcludeSection.Text = ""
        Me.txtIncludeEntry.Text = ""
        Me.txtIncludeSection.Text = ""
        Me.butExcludeEntrySave.Visible = False
        Me.butExcludeSectionSave.Visible = False
        Me.butIncludeEntrySave.Visible = False
        Me.butIncludeSectionSave.Visible = False
        Me.listExcludeEntry.Items.Clear()
        Me.listExcludeSection.Items.Clear()
        Me.listIncludeEntry.Items.Clear()
        Me.listIncludeSection.Items.Clear()
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
'The Original Code is SlotControl.vb.
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
