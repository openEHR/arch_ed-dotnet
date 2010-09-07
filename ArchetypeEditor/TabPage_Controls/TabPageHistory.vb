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
Option Explicit On
Option Strict On

Public Class TabpageHistory
    Inherits System.Windows.Forms.UserControl

    Private current_item As EventListViewItem
    Private sNodeID As String
    Private MathFunctionTable As DataTable
    Private mIsLoading As Boolean = False
    Private mFileManager As FileManagerLocal
    Friend WithEvents panelLeft As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents RightPanel As System.Windows.Forms.Panel
    Friend WithEvents EitherRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ContextMenuEvents As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SpecialiseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents listViewMathsFunctions As System.Windows.Forms.ListView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    WithEvents mOccurrences As OccurrencesPanel

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not DesignMode Then
            mFileManager = Filemanager.Master
            mOccurrences = New OccurrencesPanel(mFileManager)
            gbEventDetails.Controls.Add(mOccurrences)
            mOccurrences.Dock = DockStyle.Top
            mOccurrences.TabIndex = 0
            'gbDuration.Location = gbOffset.Location
            'gbDuration.Height = gbOffset.Height

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
    Friend WithEvents gbEventDetails As System.Windows.Forms.GroupBox
    Friend WithEvents txtEventDescription As System.Windows.Forms.TextBox
    Friend WithEvents butAddEvent As System.Windows.Forms.Button
    Friend WithEvents gbDuration As System.Windows.Forms.GroupBox
    Friend WithEvents numericDuration As System.Windows.Forms.NumericUpDown
    Friend WithEvents comboDurationUnits As System.Windows.Forms.ComboBox
    Friend WithEvents RadioInterval As System.Windows.Forms.RadioButton
    Friend WithEvents radioPointInTime As System.Windows.Forms.RadioButton
    Friend WithEvents gbOffset As System.Windows.Forms.GroupBox
    Friend WithEvents NumericOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents comboOffsetUnits As System.Windows.Forms.ComboBox

    Friend WithEvents chkIsPeriodic As System.Windows.Forms.CheckBox
    Friend WithEvents numPeriod As System.Windows.Forms.NumericUpDown
    Friend WithEvents comboTimeUnits As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    Friend WithEvents radioFixed As System.Windows.Forms.RadioButton
    Friend WithEvents radioOpen As System.Windows.Forms.RadioButton
    Friend WithEvents cbFixedOffset As System.Windows.Forms.CheckBox
    Friend WithEvents cbFixedInterval As System.Windows.Forms.CheckBox
    Friend WithEvents butRemoveElement As System.Windows.Forms.Button
    Friend WithEvents butListUp As System.Windows.Forms.Button
    Friend WithEvents butListDown As System.Windows.Forms.Button
    Friend WithEvents txtRuntimeConstraint As System.Windows.Forms.TextBox

    Friend WithEvents ListEvents As System.Windows.Forms.ListView
    Friend WithEvents ImageListEvents As System.Windows.Forms.ImageList
    Friend WithEvents TheEvents As System.Windows.Forms.ColumnHeader
    Friend WithEvents buSetRuntimeConstraint As System.Windows.Forms.Button
    Friend WithEvents HelpProviderEventSeries As System.Windows.Forms.HelpProvider
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblRuntimeName As System.Windows.Forms.Label
    Friend WithEvents gbEventList As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabpageHistory))
        Me.gbEventDetails = New System.Windows.Forms.GroupBox
        Me.gbDuration = New System.Windows.Forms.GroupBox
        Me.listViewMathsFunctions = New System.Windows.Forms.ListView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbFixedInterval = New System.Windows.Forms.CheckBox
        Me.comboDurationUnits = New System.Windows.Forms.ComboBox
        Me.numericDuration = New System.Windows.Forms.NumericUpDown
        Me.gbOffset = New System.Windows.Forms.GroupBox
        Me.cbFixedOffset = New System.Windows.Forms.CheckBox
        Me.NumericOffset = New System.Windows.Forms.NumericUpDown
        Me.comboOffsetUnits = New System.Windows.Forms.ComboBox
        Me.EitherRadioButton = New System.Windows.Forms.RadioButton
        Me.buSetRuntimeConstraint = New System.Windows.Forms.Button
        Me.txtRuntimeConstraint = New System.Windows.Forms.TextBox
        Me.txtEventDescription = New System.Windows.Forms.TextBox
        Me.RadioInterval = New System.Windows.Forms.RadioButton
        Me.radioPointInTime = New System.Windows.Forms.RadioButton
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblRuntimeName = New System.Windows.Forms.Label
        Me.butRemoveElement = New System.Windows.Forms.Button
        Me.butAddEvent = New System.Windows.Forms.Button
        Me.chkIsPeriodic = New System.Windows.Forms.CheckBox
        Me.numPeriod = New System.Windows.Forms.NumericUpDown
        Me.comboTimeUnits = New System.Windows.Forms.ComboBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.butListUp = New System.Windows.Forms.Button
        Me.butListDown = New System.Windows.Forms.Button
        Me.gbEventList = New System.Windows.Forms.GroupBox
        Me.radioOpen = New System.Windows.Forms.RadioButton
        Me.radioFixed = New System.Windows.Forms.RadioButton
        Me.ListEvents = New System.Windows.Forms.ListView
        Me.TheEvents = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuEvents = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SpecialiseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImageListEvents = New System.Windows.Forms.ImageList(Me.components)
        Me.HelpProviderEventSeries = New System.Windows.Forms.HelpProvider
        Me.panelLeft = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.RightPanel = New System.Windows.Forms.Panel
        Me.gbEventDetails.SuspendLayout()
        Me.gbDuration.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.numericDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbOffset.SuspendLayout()
        CType(Me.NumericOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEventList.SuspendLayout()
        Me.ContextMenuEvents.SuspendLayout()
        Me.panelLeft.SuspendLayout()
        Me.RightPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbEventDetails
        '
        Me.gbEventDetails.Controls.Add(Me.gbDuration)
        Me.gbEventDetails.Controls.Add(Me.gbOffset)
        Me.gbEventDetails.Controls.Add(Me.EitherRadioButton)
        Me.gbEventDetails.Controls.Add(Me.buSetRuntimeConstraint)
        Me.gbEventDetails.Controls.Add(Me.txtRuntimeConstraint)
        Me.gbEventDetails.Controls.Add(Me.txtEventDescription)
        Me.gbEventDetails.Controls.Add(Me.RadioInterval)
        Me.gbEventDetails.Controls.Add(Me.radioPointInTime)
        Me.gbEventDetails.Controls.Add(Me.lblDescription)
        Me.gbEventDetails.Controls.Add(Me.lblRuntimeName)
        Me.gbEventDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbEventDetails.Location = New System.Drawing.Point(0, 0)
        Me.gbEventDetails.Name = "gbEventDetails"
        Me.gbEventDetails.Size = New System.Drawing.Size(380, 494)
        Me.gbEventDetails.TabIndex = 34
        Me.gbEventDetails.TabStop = False
        Me.gbEventDetails.Text = "Event details"
        '
        'gbDuration
        '
        Me.gbDuration.Controls.Add(Me.listViewMathsFunctions)
        Me.gbDuration.Controls.Add(Me.Panel1)
        Me.gbDuration.Location = New System.Drawing.Point(149, 246)
        Me.gbDuration.Name = "gbDuration"
        Me.gbDuration.Size = New System.Drawing.Size(218, 248)
        Me.gbDuration.TabIndex = 25
        Me.gbDuration.TabStop = False
        Me.gbDuration.Text = "Duration"
        Me.gbDuration.Visible = False
        '
        'listViewMathsFunctions
        '
        Me.listViewMathsFunctions.CheckBoxes = True
        Me.listViewMathsFunctions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewMathsFunctions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.listViewMathsFunctions.Location = New System.Drawing.Point(3, 81)
        Me.listViewMathsFunctions.Name = "listViewMathsFunctions"
        Me.listViewMathsFunctions.ShowGroups = False
        Me.listViewMathsFunctions.Size = New System.Drawing.Size(212, 164)
        Me.listViewMathsFunctions.TabIndex = 4
        Me.listViewMathsFunctions.UseCompatibleStateImageBehavior = False
        Me.listViewMathsFunctions.View = System.Windows.Forms.View.List
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbFixedInterval)
        Me.Panel1.Controls.Add(Me.comboDurationUnits)
        Me.Panel1.Controls.Add(Me.numericDuration)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(212, 65)
        Me.Panel1.TabIndex = 5
        '
        'cbFixedInterval
        '
        Me.cbFixedInterval.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbFixedInterval.Location = New System.Drawing.Point(8, 12)
        Me.cbFixedInterval.Name = "cbFixedInterval"
        Me.cbFixedInterval.Size = New System.Drawing.Size(198, 24)
        Me.cbFixedInterval.TabIndex = 1
        Me.cbFixedInterval.Text = "Fixed Interval"
        '
        'comboDurationUnits
        '
        Me.comboDurationUnits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboDurationUnits.Location = New System.Drawing.Point(59, 38)
        Me.comboDurationUnits.Name = "comboDurationUnits"
        Me.comboDurationUnits.Size = New System.Drawing.Size(150, 21)
        Me.comboDurationUnits.TabIndex = 3
        Me.comboDurationUnits.Visible = False
        '
        'numericDuration
        '
        Me.numericDuration.Location = New System.Drawing.Point(8, 38)
        Me.numericDuration.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.numericDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericDuration.Name = "numericDuration"
        Me.numericDuration.Size = New System.Drawing.Size(45, 20)
        Me.numericDuration.TabIndex = 2
        Me.numericDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numericDuration.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericDuration.Visible = False
        '
        'gbOffset
        '
        Me.gbOffset.Controls.Add(Me.cbFixedOffset)
        Me.gbOffset.Controls.Add(Me.NumericOffset)
        Me.gbOffset.Controls.Add(Me.comboOffsetUnits)
        Me.gbOffset.Location = New System.Drawing.Point(162, 244)
        Me.gbOffset.Name = "gbOffset"
        Me.gbOffset.Size = New System.Drawing.Size(172, 76)
        Me.gbOffset.TabIndex = 22
        Me.gbOffset.TabStop = False
        Me.gbOffset.Text = "Offset"
        Me.gbOffset.Visible = False
        '
        'cbFixedOffset
        '
        Me.cbFixedOffset.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbFixedOffset.Location = New System.Drawing.Point(11, 16)
        Me.cbFixedOffset.Name = "cbFixedOffset"
        Me.cbFixedOffset.Size = New System.Drawing.Size(155, 24)
        Me.cbFixedOffset.TabIndex = 8
        Me.cbFixedOffset.Text = "Fixed Offset"
        '
        'NumericOffset
        '
        Me.NumericOffset.Location = New System.Drawing.Point(10, 43)
        Me.NumericOffset.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.NumericOffset.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.NumericOffset.Name = "NumericOffset"
        Me.NumericOffset.Size = New System.Drawing.Size(46, 20)
        Me.NumericOffset.TabIndex = 9
        Me.NumericOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericOffset.Visible = False
        '
        'comboOffsetUnits
        '
        Me.comboOffsetUnits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboOffsetUnits.Location = New System.Drawing.Point(61, 43)
        Me.comboOffsetUnits.Name = "comboOffsetUnits"
        Me.comboOffsetUnits.Size = New System.Drawing.Size(105, 21)
        Me.comboOffsetUnits.TabIndex = 10
        Me.comboOffsetUnits.Visible = False
        '
        'EitherRadioButton
        '
        Me.EitherRadioButton.Location = New System.Drawing.Point(16, 307)
        Me.EitherRadioButton.Name = "EitherRadioButton"
        Me.EitherRadioButton.Size = New System.Drawing.Size(156, 24)
        Me.EitherRadioButton.TabIndex = 8
        Me.EitherRadioButton.TabStop = True
        Me.EitherRadioButton.Text = "Any Event"
        '
        'buSetRuntimeConstraint
        '
        Me.buSetRuntimeConstraint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buSetRuntimeConstraint.Location = New System.Drawing.Point(335, 209)
        Me.buSetRuntimeConstraint.Name = "buSetRuntimeConstraint"
        Me.buSetRuntimeConstraint.Size = New System.Drawing.Size(32, 22)
        Me.buSetRuntimeConstraint.TabIndex = 5
        Me.buSetRuntimeConstraint.Text = "..."
        '
        'txtRuntimeConstraint
        '
        Me.txtRuntimeConstraint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRuntimeConstraint.Location = New System.Drawing.Point(16, 210)
        Me.txtRuntimeConstraint.Name = "txtRuntimeConstraint"
        Me.txtRuntimeConstraint.ReadOnly = True
        Me.txtRuntimeConstraint.Size = New System.Drawing.Size(315, 20)
        Me.txtRuntimeConstraint.TabIndex = 4
        '
        'txtEventDescription
        '
        Me.txtEventDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEventDescription.Location = New System.Drawing.Point(15, 95)
        Me.txtEventDescription.Multiline = True
        Me.txtEventDescription.Name = "txtEventDescription"
        Me.txtEventDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEventDescription.Size = New System.Drawing.Size(350, 95)
        Me.txtEventDescription.TabIndex = 2
        '
        'RadioInterval
        '
        Me.RadioInterval.Location = New System.Drawing.Point(16, 277)
        Me.RadioInterval.Name = "RadioInterval"
        Me.RadioInterval.Size = New System.Drawing.Size(156, 24)
        Me.RadioInterval.TabIndex = 7
        Me.RadioInterval.TabStop = True
        Me.RadioInterval.Text = "Interval"
        '
        'radioPointInTime
        '
        Me.radioPointInTime.Location = New System.Drawing.Point(16, 247)
        Me.radioPointInTime.Name = "radioPointInTime"
        Me.radioPointInTime.Size = New System.Drawing.Size(156, 24)
        Me.radioPointInTime.TabIndex = 6
        Me.radioPointInTime.TabStop = True
        Me.radioPointInTime.Text = "Point in time"
        '
        'lblDescription
        '
        Me.lblDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescription.Location = New System.Drawing.Point(15, 73)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(350, 24)
        Me.lblDescription.TabIndex = 1
        Me.lblDescription.Text = "Description:"
        '
        'lblRuntimeName
        '
        Me.lblRuntimeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRuntimeName.Location = New System.Drawing.Point(16, 193)
        Me.lblRuntimeName.Name = "lblRuntimeName"
        Me.lblRuntimeName.Size = New System.Drawing.Size(350, 24)
        Me.lblRuntimeName.TabIndex = 3
        Me.lblRuntimeName.Text = "Runtime name constraint:"
        '
        'butRemoveElement
        '
        Me.butRemoveElement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(167, 40)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveElement.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.butRemoveElement, "Remove selected item")
        '
        'butAddEvent
        '
        Me.butAddEvent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butAddEvent.Image = CType(resources.GetObject("butAddEvent.Image"), System.Drawing.Image)
        Me.butAddEvent.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddEvent.Location = New System.Drawing.Point(167, 14)
        Me.butAddEvent.Name = "butAddEvent"
        Me.butAddEvent.Size = New System.Drawing.Size(24, 24)
        Me.butAddEvent.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.butAddEvent, "Add as new event")
        '
        'chkIsPeriodic
        '
        Me.chkIsPeriodic.Location = New System.Drawing.Point(13, 135)
        Me.chkIsPeriodic.Name = "chkIsPeriodic"
        Me.chkIsPeriodic.Size = New System.Drawing.Size(178, 53)
        Me.chkIsPeriodic.TabIndex = 18
        Me.chkIsPeriodic.Text = "Events at regular time period"
        '
        'numPeriod
        '
        Me.numPeriod.Location = New System.Drawing.Point(13, 194)
        Me.numPeriod.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.numPeriod.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPeriod.Name = "numPeriod"
        Me.numPeriod.Size = New System.Drawing.Size(52, 20)
        Me.numPeriod.TabIndex = 19
        Me.numPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numPeriod.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPeriod.Visible = False
        '
        'comboTimeUnits
        '
        Me.comboTimeUnits.Location = New System.Drawing.Point(69, 194)
        Me.comboTimeUnits.Name = "comboTimeUnits"
        Me.comboTimeUnits.Size = New System.Drawing.Size(122, 21)
        Me.comboTimeUnits.TabIndex = 20
        Me.comboTimeUnits.Visible = False
        '
        'butListUp
        '
        Me.butListUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butListUp.Image = CType(resources.GetObject("butListUp.Image"), System.Drawing.Image)
        Me.butListUp.Location = New System.Drawing.Point(167, 66)
        Me.butListUp.Name = "butListUp"
        Me.butListUp.Size = New System.Drawing.Size(24, 24)
        Me.butListUp.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.butListUp, "Move selected item up")
        '
        'butListDown
        '
        Me.butListDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butListDown.Image = CType(resources.GetObject("butListDown.Image"), System.Drawing.Image)
        Me.butListDown.Location = New System.Drawing.Point(167, 92)
        Me.butListDown.Name = "butListDown"
        Me.butListDown.Size = New System.Drawing.Size(24, 24)
        Me.butListDown.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.butListDown, "Move selected item down")
        '
        'gbEventList
        '
        Me.gbEventList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbEventList.Controls.Add(Me.radioOpen)
        Me.gbEventList.Controls.Add(Me.radioFixed)
        Me.gbEventList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbEventList.Location = New System.Drawing.Point(13, 5)
        Me.gbEventList.Name = "gbEventList"
        Me.gbEventList.Size = New System.Drawing.Size(144, 111)
        Me.gbEventList.TabIndex = 0
        Me.gbEventList.TabStop = False
        Me.gbEventList.Text = "Event list"
        '
        'radioOpen
        '
        Me.radioOpen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radioOpen.Checked = True
        Me.radioOpen.Location = New System.Drawing.Point(8, 16)
        Me.radioOpen.Name = "radioOpen"
        Me.radioOpen.Size = New System.Drawing.Size(128, 32)
        Me.radioOpen.TabIndex = 16
        Me.radioOpen.TabStop = True
        Me.radioOpen.Text = "Open"
        '
        'radioFixed
        '
        Me.radioFixed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radioFixed.Location = New System.Drawing.Point(8, 56)
        Me.radioFixed.Name = "radioFixed"
        Me.radioFixed.Size = New System.Drawing.Size(128, 40)
        Me.radioFixed.TabIndex = 17
        Me.radioFixed.Text = "Fixed"
        '
        'ListEvents
        '
        Me.ListEvents.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListEvents.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TheEvents})
        Me.ListEvents.ContextMenuStrip = Me.ContextMenuEvents
        Me.ListEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListEvents.HideSelection = False
        Me.ListEvents.LabelEdit = True
        Me.ListEvents.LabelWrap = False
        Me.ListEvents.Location = New System.Drawing.Point(203, 0)
        Me.ListEvents.MultiSelect = False
        Me.ListEvents.Name = "ListEvents"
        Me.ListEvents.Size = New System.Drawing.Size(238, 494)
        Me.ListEvents.SmallImageList = Me.ImageListEvents
        Me.ListEvents.TabIndex = 1
        Me.ListEvents.UseCompatibleStateImageBehavior = False
        Me.ListEvents.View = System.Windows.Forms.View.Details
        '
        'TheEvents
        '
        Me.TheEvents.Text = "Events"
        Me.TheEvents.Width = 200
        '
        'ContextMenuEvents
        '
        Me.ContextMenuEvents.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpecialiseToolStripMenuItem})
        Me.ContextMenuEvents.Name = "ContextMenuEvents"
        Me.ContextMenuEvents.Size = New System.Drawing.Size(121, 26)
        '
        'SpecialiseToolStripMenuItem
        '
        Me.SpecialiseToolStripMenuItem.Name = "SpecialiseToolStripMenuItem"
        Me.SpecialiseToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.SpecialiseToolStripMenuItem.Text = "Specialise"
        '
        'ImageListEvents
        '
        Me.ImageListEvents.ImageStream = CType(resources.GetObject("ImageListEvents.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListEvents.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListEvents.Images.SetKeyName(0, "")
        Me.ImageListEvents.Images.SetKeyName(1, "")
        Me.ImageListEvents.Images.SetKeyName(2, "")
        Me.ImageListEvents.Images.SetKeyName(3, "")
        Me.ImageListEvents.Images.SetKeyName(4, "")
        Me.ImageListEvents.Images.SetKeyName(5, "")
        '
        'panelLeft
        '
        Me.panelLeft.AutoScroll = True
        Me.panelLeft.Controls.Add(Me.chkIsPeriodic)
        Me.panelLeft.Controls.Add(Me.comboTimeUnits)
        Me.panelLeft.Controls.Add(Me.butListUp)
        Me.panelLeft.Controls.Add(Me.butListDown)
        Me.panelLeft.Controls.Add(Me.numPeriod)
        Me.panelLeft.Controls.Add(Me.gbEventList)
        Me.panelLeft.Controls.Add(Me.butAddEvent)
        Me.panelLeft.Controls.Add(Me.butRemoveElement)
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelLeft.Location = New System.Drawing.Point(0, 0)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Size = New System.Drawing.Size(200, 494)
        Me.panelLeft.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(200, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 494)
        Me.Splitter1.TabIndex = 37
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(441, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 494)
        Me.Splitter2.TabIndex = 38
        Me.Splitter2.TabStop = False
        '
        'RightPanel
        '
        Me.RightPanel.AutoScroll = True
        Me.RightPanel.Controls.Add(Me.gbEventDetails)
        Me.RightPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.RightPanel.Location = New System.Drawing.Point(444, 0)
        Me.RightPanel.Name = "RightPanel"
        Me.RightPanel.Size = New System.Drawing.Size(380, 494)
        Me.RightPanel.TabIndex = 39
        '
        'TabpageHistory
        '
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.ListEvents)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.panelLeft)
        Me.Controls.Add(Me.RightPanel)
        Me.HelpProviderEventSeries.SetHelpKeyword(Me, "HowTo/edit_EventSeries.htm")
        Me.HelpProviderEventSeries.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "TabpageHistory"
        Me.HelpProviderEventSeries.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(824, 494)
        Me.gbEventDetails.ResumeLayout(False)
        Me.gbEventDetails.PerformLayout()
        Me.gbDuration.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.numericDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbOffset.ResumeLayout(False)
        CType(Me.NumericOffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEventList.ResumeLayout(False)
        Me.ContextMenuEvents.ResumeLayout(False)
        Me.panelLeft.ResumeLayout(False)
        Me.RightPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Methods and Properties"

    Public Property NodeId() As String
        Get
            Return sNodeID
        End Get
        Set(ByVal Value As String)
            sNodeID = Value
        End Set
    End Property

    Friend Sub BuildInterface(ByVal aContainer As Control, ByVal pos As Point, ByVal mandatory_only As Boolean)
        Dim spacer As Integer = 15
        Dim leftmargin As Integer = pos.X
        Dim combo As New ComboBox

        For Each elvi As EventListViewItem In ListEvents.Items
            If elvi.IsMandatory Or Not mandatory_only Then
                combo.Items.Add(elvi.Text)
            End If
        Next

        combo.Height = 25
        combo.Width = 150
        combo.Location = pos

        If combo.Items.Count > 0 Then
            combo.SelectedIndex = 0
        End If

        aContainer.Height = pos.Y + combo.Height + 10
        aContainer.Controls.Add(combo)
    End Sub

    Friend Sub ToRichText(ByRef Text As IO.StringWriter, ByVal level As Integer)
        Dim elvi As EventListViewItem

        Text.WriteLine(Space(3 * level) & "\cf1 EventSeries\cf0  = \{\par")
        level = level + 1

        If chkIsPeriodic.Checked Then
            Text.WriteLine(Space(3 * level) & "Periodic offset = " & numPeriod.Value.ToString & " " & comboTimeUnits.Text & "\par")
        End If

        For Each elvi In ListEvents.Items
            Dim s As String = RichTextBoxUnicode.CreateRichTextBoxTag(elvi.RM_Class.NodeId, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_TEXT) 'SRH: 23 Aug 2009 [EDT-575] Support unicode

            Text.WriteLine(Space(3 * level) & "\b " & s & " (" & elvi.Occurrences.ToString & ") \b0\par")

            s = RichTextBoxUnicode.CreateRichTextBoxTag(elvi.RM_Class.NodeId, RichTextBoxUnicode.RichTextDataType.ONTOLOGY_DESC) 'SRH: 23 Aug 2009 [EDT-575] Support unicode

            Text.WriteLine(Space(3 * level) & "\i   - " & s & "\i0\par")

            Text.WriteLine(Space(3 * level) & elvi.RM_Class.Type.ToString & "\par")

            Select Case elvi.RM_Class.Type
                Case StructureType.Event

                Case StructureType.PointEvent
                    If elvi.hasFixedOffset Then
                        Text.WriteLine(Space(3 * level) & Filemanager.GetOpenEhrTerm(179, "Offset") + _
                        " = " & elvi.Offset.ToString & " " & elvi.OffsetUnits & "\par")
                    End If

                Case StructureType.IntervalEvent
                    If elvi.hasFixedWidth Then
                        Text.WriteLine(Space(3 * level) & Filemanager.GetOpenEhrTerm(143, "Fixed interval") + _
                        " = " & elvi.Width.ToString & " " & elvi.WidthUnits & "\par")
                    End If
                    Try
                        If Not elvi.AggregateMathFunction Is Nothing AndAlso elvi.AggregateMathFunction.Codes.Count > 0 Then
                            Text.Write(Space(3 * level) & Filemanager.GetOpenEhrTerm(266, "Event math function"))
                            Text.Write(" = ")
                            Dim separator As String = ""
                            For Each code As String In elvi.AggregateMathFunction.Codes
                                Dim i As Integer
                                If Integer.TryParse(code, i) Then
                                    Text.Write(separator & Filemanager.GetOpenEhrTerm(i, "Fixed interval"))
                                    If separator = "" Then
                                        separator = ", "
                                    End If
                                End If
                            Next
                            Text.WriteLine("\par")
                        End If
                    Catch
                    End Try
            End Select

            Text.WriteLine("\par")
        Next

        level = level - 1
        Text.WriteLine(Space(3 * level) & "\} -- end EventSeries\par")
    End Sub

    Friend Sub ToHTML(ByRef Text As IO.StreamWriter, Optional ByVal BackGroundColour As String = "")
        Dim elvi As EventListViewItem

        If chkIsPeriodic.Checked Then
            Text.WriteLine("<p>Periodic offset = " & numPeriod.Value.ToString & " " & comboTimeUnits.Text & "</p>")
        End If

        Text.WriteLine(Environment.NewLine & "<table border=""1"" cellpadding=""2"" width=""100%"">")

        If BackGroundColour = "" Then
            Text.WriteLine(Environment.NewLine & "<tr>")
        Else
            Text.WriteLine(Environment.NewLine & "<tr  bgcolor=""" & BackGroundColour & """>")
        End If

        Text.WriteLine("<td width=""20%""><h4>" & Filemanager.GetOpenEhrTerm(133, "Events") & "</h4></td>")
        Text.WriteLine("<td width = ""40%""><h4>" & Filemanager.GetOpenEhrTerm(113, "Description") & "</h4></td>")
        Text.WriteLine("<td width = ""20%""><h4>" & Filemanager.GetOpenEhrTerm(87, "Constraints") & "</h4></td>")
        Text.WriteLine("</tr>")


        For Each elvi In ListEvents.Items
            Text.WriteLine("<tr>")
            Text.WriteLine("<td><b>" & elvi.Text & "</b></td>")
            Text.WriteLine("<td><b>" & elvi.Description & "</b></td>")
            Text.WriteLine("<td>")
            Text.WriteLine(elvi.RM_Class.Type.ToString)

            Select Case elvi.RM_Class.Type
                Case StructureType.Event

                Case StructureType.PointEvent
                    If elvi.hasFixedOffset Then
                        Text.WriteLine("<br>" + Filemanager.GetOpenEhrTerm(179, "Offset") + " = " & elvi.Offset.ToString & " " & elvi.OffsetUnits & "<br>")
                    End If

                Case StructureType.IntervalEvent
                    If elvi.hasFixedWidth Then
                        Text.WriteLine("<br>" + Filemanager.GetOpenEhrTerm(143, "Fixed interval") + " = " & elvi.Width.ToString & " " & elvi.WidthUnits & "<br>")
                    End If

                    If Not elvi.AggregateMathFunction Is Nothing Then
                        Text.Write("<br>" + Filemanager.GetOpenEhrTerm(266, "Event math function"))
                        Dim separator As String = ""

                        For Each code As String In elvi.AggregateMathFunction.Codes
                            Dim i As Integer

                            If Integer.TryParse(code, i) Then
                                Text.Write(separator & Filemanager.GetOpenEhrTerm(i, "Fixed interval"))

                                If separator = "" Then
                                    separator = ", "
                                End If
                            End If
                        Next
                    End If

                    Text.WriteLine("")
            End Select

            Text.WriteLine("</tr>")
        Next

        Text.WriteLine(Environment.NewLine & "</table>")
        Text.WriteLine(Environment.NewLine & "<hr>")
    End Sub

    Friend Sub ProcessEventSeries(ByVal rm As RmHistory)
        Dim ev As RmEvent
        Dim HistEvent As EventListViewItem
        sNodeID = rm.NodeId

        comboTimeUnits.Items.Clear()
        comboTimeUnits.Items.AddRange(OceanArchetypeEditor.ISO_TimeUnits.IsoTimeUnits())
        comboTimeUnits.SelectedIndex = 3        ' minutes

        comboOffsetUnits.Items.Clear()
        comboOffsetUnits.Items.AddRange(OceanArchetypeEditor.ISO_TimeUnits.IsoTimeUnits())
        comboOffsetUnits.SelectedIndex = 3      ' minutes

        comboDurationUnits.Items.Clear()
        comboDurationUnits.Items.AddRange(OceanArchetypeEditor.ISO_TimeUnits.IsoTimeUnits())
        comboDurationUnits.SelectedIndex = 3    ' minutes

        If rm.isPeriodic Then
            chkIsPeriodic.Checked = True
            numPeriod.Value = rm.Period
            comboTimeUnits.Text = OceanArchetypeEditor.ISO_TimeUnits.GetLanguageForISO(rm.PeriodUnits)
        Else
            chkIsPeriodic.Checked = False
        End If

        If rm.Children.Fixed Then
            radioFixed.Checked = True
        Else
            radioOpen.Checked = True
        End If

        For Each ev In rm.Children
            HistEvent = New EventListViewItem(ev, mFileManager)
            ListEvents.Items.Add(HistEvent)
        Next

        Translate()

        If ListEvents.Items.Count > 0 Then
            ListEvents.Items.Item(0).Selected = True
            butRemoveElement.Visible = True
            ProcessEvent(CType(ListEvents.Items.Item(0), EventListViewItem))
            current_item = CType(ListEvents.Items.Item(0), EventListViewItem)
            current_item.Selected = True
        End If
    End Sub

    Public ReadOnly Property ComponentType() As String
        Get
            Return "EventSeries"
        End Get
    End Property

    Public Sub Translate()
        Dim HistEvent As EventListViewItem
        Dim elvi As EventListViewItem
        current_item = Nothing

        For Each HistEvent In ListEvents.Items
            HistEvent.Translate()
        Next

        If ListEvents.SelectedItems.Count > 0 Then
            elvi = CType(ListEvents.SelectedItems(0), EventListViewItem)
            txtEventDescription.Text = elvi.Description

            If elvi.hasNameConstraint Then
                txtRuntimeConstraint.Text = elvi.NameConstraint.ToString
            End If

            current_item = elvi
        End If
    End Sub

    Public Sub TranslateGUI()
        cbFixedInterval.Text = Filemanager.GetOpenEhrTerm(143, cbFixedInterval.Text)
        cbFixedOffset.Text = Filemanager.GetOpenEhrTerm(180, cbFixedOffset.Text)
        lblDescription.Text = Filemanager.GetOpenEhrTerm(113, lblDescription.Text)
        lblRuntimeName.Text = Filemanager.GetOpenEhrTerm(114, lblRuntimeName.Text)
        gbEventDetails.Text = Filemanager.GetOpenEhrTerm(138, gbEventDetails.Text)
        gbOffset.Text = Filemanager.GetOpenEhrTerm(179, gbOffset.Text)
        gbDuration.Text = Filemanager.GetOpenEhrTerm(142, gbDuration.Text)
        radioPointInTime.Text = Filemanager.GetOpenEhrTerm(140, radioPointInTime.Text)
        RadioInterval.Text = Filemanager.GetOpenEhrTerm(141, RadioInterval.Text)
        EitherRadioButton.Text = Filemanager.GetOpenEhrTerm(276, RadioInterval.Text)
        chkIsPeriodic.Text = Filemanager.GetOpenEhrTerm(137, chkIsPeriodic.Text)
        gbEventList.Text = Filemanager.GetOpenEhrTerm(134, gbEventList.Text)
        radioOpen.Text = Filemanager.GetOpenEhrTerm(135, radioOpen.Text)
        radioFixed.Text = Filemanager.GetOpenEhrTerm(136, radioFixed.Text)
        SpecialiseToolStripMenuItem.Text = AE_Constants.Instance.Specialise
    End Sub

    Friend Function SaveAsEventSeries() As RmHistory
        Dim ev As EventListViewItem
        Dim Hist As RmHistory

        Hist = New RmHistory(NodeId)

        If chkIsPeriodic.Checked AndAlso Not comboTimeUnits.SelectedItem Is Nothing Then
            Hist.Period = CInt(numPeriod.Value)
            Hist.PeriodUnits = CType(comboTimeUnits.SelectedItem, TimeUnits.TimeUnit).ISOunit
            Hist.isPeriodic = True
        End If

        If ListEvents.Items.Count = 0 Then
            ' there must be at least one event
            Debug.Assert(False)
            butAddEvent_Click(New Object, New System.EventArgs)
        End If

        For Each ev In ListEvents.Items
            Hist.Children.Add(CType(ev.RM_Class, RmEvent))
        Next

        SetEventSeriesCardinality(Hist)
        Return Hist
    End Function

    Private Sub SetEventSeriesCardinality(ByVal a_EventSeries As RmHistory)
        If radioFixed.Checked Then
            Dim i As Integer

            For Each evnt As RmEvent In a_EventSeries.Children
                If evnt.Occurrences.IsUnbounded Then
                    a_EventSeries.Children.Cardinality.IsUnbounded = True
                    Return
                    ' FIXME: Spaghetti code!
                End If

                i += evnt.Occurrences.MaxCount
            Next

            a_EventSeries.Children.Cardinality.MaxCount = i
        Else
            a_EventSeries.Children.Cardinality.IsUnbounded = True
        End If
    End Sub

    Friend Sub Reset()
        ' empty and reset all controls
        ListEvents.Items.Clear()
        txtEventDescription.Text = ""
        NumericOffset.Value = 0
        chkIsPeriodic.Checked = False
        numericDuration.Value = 0
    End Sub

    Friend Sub AddBaseLineEvent()
        Dim elvi As New EventListViewItem(Filemanager.GetOpenEhrTerm(276, "Baseline event"), mFileManager)
        txtEventDescription.Text = "*"
        elvi.Width = 1
        elvi.WidthUnits = "min"
        radioPointInTime.Checked = False
        cbFixedInterval.Checked = False
        cbFixedOffset.Checked = False
        numericDuration.Value = 1
        mOccurrences.Cardinality = elvi.Occurrences
        ListEvents.Items.Add(elvi)
        elvi.Selected = True
        current_item = elvi
    End Sub

#End Region

#Region "Internal classes"

    Private Class EventListViewItem
        Inherits ListViewItem

        Private element As RmEvent
        Private sDescription As String
        Private boolNew As Boolean = False
        Private mFileManager As FileManagerLocal

        Public Shadows Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal Value As String)
                mFileManager.OntologyManager.SetText(Value, element.NodeId)
                MyBase.Text = Value
            End Set
        End Property
        Public Property hasNameConstraint() As Boolean
            Get
                Return element.HasNameConstraint
            End Get
            Set(ByVal Value As Boolean)
                element.HasNameConstraint = Value
            End Set
        End Property
        Public Property NameConstraint() As Constraint_Text
            Get
                Return element.NameConstraint
            End Get
            Set(ByVal Value As Constraint_Text)
                element.NameConstraint = Value
            End Set
        End Property
        Public Shadows Property Selected() As Boolean
            Get
                Return MyBase.Selected()
            End Get
            Set(ByVal Value As Boolean)
                If MyBase.Selected <> Value Then
                    MyBase.Selected = Value
                End If
                SetImageIndex()
            End Set
        End Property
        Public Property Description() As String
            Get
                Return sDescription
            End Get
            Set(ByVal Value As String)
                sDescription = Value
                mFileManager.OntologyManager.SetDescription(Value, element.NodeId)
            End Set
        End Property
        Public Property EventType() As RmEvent.ObservationEventType
            Get
                Return element.EventType
            End Get
            Set(ByVal Value As RmEvent.ObservationEventType)
                element.EventType = Value
                SetImageIndex()
            End Set
        End Property
        Public Property Offset() As Integer
            Get
                Return element.Offset
            End Get
            Set(ByVal Value As Integer)
                element.Offset = Value
            End Set
        End Property
        Public Property OffsetUnits() As String
            Get
                Return element.OffsetUnits
            End Get
            Set(ByVal Value As String)
                element.OffsetUnits = Value
            End Set
        End Property
        Public Property Width() As Integer
            Get
                Return CInt(element.Width)
            End Get
            Set(ByVal Value As Integer)
                element.Width = Value
            End Set
        End Property
        Public Property WidthUnits() As String
            Get
                Return element.WidthUnits
            End Get
            Set(ByVal Value As String)
                element.WidthUnits = Value
            End Set
        End Property

        Public Property AggregateMathFunction() As CodePhrase
            Get
                Return element.AggregateMathFunction
            End Get
            Set(ByVal Value As CodePhrase)
                element.AggregateMathFunction = Value
            End Set
        End Property

        Public ReadOnly Property HasMathsFunction() As Boolean
            Get
                Return element.EventType = RmEvent.ObservationEventType.Interval AndAlso Not element.AggregateMathFunction Is Nothing AndAlso element.AggregateMathFunction.Codes.Count > 0
            End Get
        End Property
        Public Property hasFixedOffset() As Boolean
            Get
                Return element.hasFixedOffset
            End Get
            Set(ByVal Value As Boolean)
                element.hasFixedOffset = Value
            End Set
        End Property
        Public Property hasFixedWidth() As Boolean
            Get
                Return element.hasFixedDuration
            End Get
            Set(ByVal Value As Boolean)
                element.hasFixedDuration = Value
            End Set
        End Property
        Public ReadOnly Property RM_Class() As RmStructure
            Get
                Return element
            End Get
        End Property
        Public Property Occurrences() As RmCardinality
            Get
                Return element.Occurrences
            End Get
            Set(ByVal Value As RmCardinality)
                element.Occurrences = Value
            End Set
        End Property
        Public ReadOnly Property Id() As String
            Get
                Id = element.NodeId
            End Get
        End Property
        Public ReadOnly Property TypeName() As String
            Get
                ' has to be an element as it is in a list
                Return element.TypeName
            End Get
        End Property
        Public ReadOnly Property IsMandatory() As Boolean
            Get
                Return (element.Occurrences.MinCount > 0)
            End Get
        End Property

        Public Sub Translate()
            Dim a_Term As RmTerm
            a_Term = mFileManager.OntologyManager.GetTerm(element.NodeId)
            MyBase.Text = a_Term.Text
            sDescription = a_Term.Description
        End Sub

        Public Function Copy() As EventListViewItem
            Dim newLvItem As EventListViewItem
            newLvItem = New EventListViewItem(Me, mFileManager)
            Return newLvItem
        End Function

        Public Sub Specialise()
            Dim a_Term As RmTerm

            MyBase.Text = "! - " & MyBase.Text
            a_Term = mFileManager.OntologyManager.SpecialiseTerm(MyBase.Text, sDescription, element.NodeId)
            element.NodeId = a_Term.Code
            If element.HasNameConstraint Then
                element.NameConstraint.ConstraintCode = mFileManager.OntologyManager.SpecialiseTerm(element.NameConstraint.ConstraintCode).Code
            End If
        End Sub

        Private Sub SetImageIndex()
            Dim offset As Integer

            If MyBase.Selected Then
                offset = 3
            End If

            Select Case element.Type
                Case StructureType.Event
                    ImageIndex = 2 + offset  '?
                Case StructureType.PointEvent
                    ImageIndex = 0 + offset      'o
                Case StructureType.IntervalEvent
                    ImageIndex = 1 + offset  'H
            End Select
        End Sub

        Sub New(ByVal Text As String, ByVal a_filemanager As FileManagerLocal)
            MyBase.New()
            mFileManager = a_filemanager
            MyBase.Text = Text

            Dim a_Term As RmTerm
            a_Term = mFileManager.OntologyManager.AddTerm(Text)
            element = New RmEvent(a_Term.Code)
            sDescription = "*"
        End Sub

        Sub New(ByVal an_event As RmEvent, ByVal a_filemanager As FileManagerLocal)
            MyBase.New()
            mFileManager = a_filemanager

            Dim a_Term As RmTerm

            element = an_event
            a_Term = mFileManager.OntologyManager.GetTerm(an_event.NodeId)
            MyBase.Text = a_Term.Text
            sDescription = a_Term.Description
            SetImageIndex()

        End Sub

        Sub New(ByVal elvi As EventListViewItem, ByVal a_filemanager As FileManagerLocal)
            MyBase.New()

            mFileManager = a_filemanager

            MyBase.Text = elvi.Text
            sDescription = elvi.Description
            ' need to copy here as may be a copy process
            element = CType(elvi.RM_Class.Copy, RmEvent)
            SetImageIndex()
        End Sub

    End Class

#End Region

    Private Sub chkIsPeriodic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsPeriodic.CheckedChanged
        numPeriod.Visible = chkIsPeriodic.Checked
        comboTimeUnits.Visible = chkIsPeriodic.Checked
        gbOffset.Visible = radioPointInTime.Checked And Not chkIsPeriodic.Checked

        If Not current_item Is Nothing Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub RadioEventType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioPointInTime.CheckedChanged, EitherRadioButton.CheckedChanged, RadioInterval.CheckedChanged
        gbDuration.Visible = RadioInterval.Checked
        gbOffset.Visible = radioPointInTime.Checked And Not chkIsPeriodic.Checked

        If Not current_item Is Nothing And Not mIsLoading Then
            If radioPointInTime.Checked Then
                current_item.EventType = RmEvent.ObservationEventType.PointInTime
            ElseIf RadioInterval.Checked Then
                current_item.EventType = RmEvent.ObservationEventType.Interval
                current_item.AggregateMathFunction = GetAggregateFunctions()
                cbFixedInterval_CheckedChanged(sender, e)
            Else
                current_item.EventType = RmEvent.ObservationEventType.Event
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Function GetAggregateFunctions() As CodePhrase
        Dim result As New CodePhrase("openehr")

        For Each l As ListViewItem In listViewMathsFunctions.CheckedItems
            result.Codes.Add(CStr(l.Tag))
        Next

        Return result
    End Function

    Private Sub butAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddEvent.Click
        Dim elvi As EventListViewItem
        ' ensure there is no selected event
        If Not current_item Is Nothing Then
            current_item.Selected = False
        End If

        current_item = Nothing

        ' create a new generic event and set the screen accordingly
        elvi = New EventListViewItem(Filemanager.GetOpenEhrTerm(276, "Any event"), mFileManager)
        txtEventDescription.Text = "*"
        elvi.Width = 1
        elvi.WidthUnits = "min"
        radioPointInTime.Checked = False
        cbFixedInterval.Checked = False
        cbFixedOffset.Checked = False
        mOccurrences.Cardinality = elvi.Occurrences
        numericDuration.Value = 1

        ListEvents.Items.Add(elvi)
        elvi.Selected = True
        current_item = elvi
        elvi.Selected = True
        butRemoveElement.Show()
        mFileManager.FileEdited = True
        elvi.BeginEdit()
    End Sub

    Private Sub ProcessEvent(ByVal elvi As EventListViewItem)
        mIsLoading = True

        txtEventDescription.Text = elvi.Description
        NumericOffset.Value = elvi.Offset

        If elvi.OffsetUnits <> "" Then
            comboOffsetUnits.Text = OceanArchetypeEditor.ISO_TimeUnits.GetLanguageForISO(elvi.OffsetUnits)
        End If

        mOccurrences.Cardinality = elvi.Occurrences

        If elvi.Width <> 0 Then
            numericDuration.Value = elvi.Width
        End If

        If elvi.WidthUnits <> "" Then
            comboDurationUnits.Text = OceanArchetypeEditor.ISO_TimeUnits.GetLanguageForISO(elvi.WidthUnits)
        End If

        Dim math_functions As DataRow() = mFileManager.OntologyManager.CodeForGroupID(14, OceanArchetypeEditor.DefaultLanguageCode) 'event math function
        listViewMathsFunctions.Clear()

        For Each rw As DataRow In math_functions
            Dim l As New ListViewItem
            Dim code As String = CStr(rw.Item(1))
            l.Tag = code
            l.Text = CStr(rw.Item(2))

            ' Deal with the change of archetypes from a string to a code phrase and the openEHR code as an integer.
            l.Checked = elvi.HasMathsFunction AndAlso elvi.AggregateMathFunction.Codes.Contains(code)
            listViewMathsFunctions.Items.Add(l)
        Next

        Select Case elvi.EventType
            Case RmEvent.ObservationEventType.Event
                EitherRadioButton.Checked = True
            Case RmEvent.ObservationEventType.PointInTime
                radioPointInTime.Checked = True
                cbFixedOffset.Checked = elvi.hasFixedOffset
            Case RmEvent.ObservationEventType.Interval
                RadioInterval.Checked = True
                cbFixedInterval.Checked = elvi.hasFixedWidth
        End Select

        If elvi.hasNameConstraint Then
            txtRuntimeConstraint.Text = elvi.NameConstraint.ToString
        End If

        mIsLoading = False
    End Sub

    Private Sub listEvents_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListEvents.SelectedIndexChanged
        Dim elvi As EventListViewItem

        If ListEvents.SelectedItems.Count = 1 Then
            ' must be at least one selected - this is called twice when change selection
            ' first set to 0 then selects one
            If Not current_item Is ListEvents.SelectedItems(0) Then
                If Not current_item Is Nothing Then
                    elvi = current_item
                    elvi.Selected = False   ' sets the imageindex to unselected (and the listitem to selected if it is not)
                    current_item = Nothing  ' stops processes when writing information
                End If

                elvi = CType(ListEvents.SelectedItems(0), EventListViewItem)
                ProcessEvent(elvi)
                current_item = elvi
                ' set the image to selected, don't use selected to change as it will call this again!
                current_item.Selected = True
            End If

            SpecialiseToolStripMenuItem.Visible = False

            If Not current_item Is Nothing Then
                Dim i As Integer = OceanArchetypeEditor.Instance.CountInString(current_item.RM_Class.NodeId, ".")
                Dim numberSpecialisations As Integer = mFileManager.OntologyManager.NumberOfSpecialisations

                If i < numberSpecialisations Then
                    SpecialiseToolStripMenuItem.Visible = True
                End If
            End If
        End If

    End Sub

    Private Sub txtEventDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEventDescription.TextChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            current_item.Description = txtEventDescription.Text
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub NumericOffset_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericOffset.ValueChanged, NumericOffset.TextChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            current_item.Offset = CInt(NumericOffset.Value)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub comboOffsetUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboOffsetUnits.SelectedIndexChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            current_item.OffsetUnits = CType(comboOffsetUnits.SelectedItem, TimeUnits.TimeUnit).ISOunit
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub numericDuration_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numericDuration.ValueChanged, numericDuration.TextChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            current_item.Width = CInt(numericDuration.Value)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub comboDurationUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboDurationUnits.SelectedIndexChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            current_item.WidthUnits = CType(comboDurationUnits.SelectedItem, TimeUnits.TimeUnit).ISOunit
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub listViewMathsFunctions_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles listViewMathsFunctions.ItemChecked
        If Not current_item Is Nothing And Not mIsLoading And listViewMathsFunctions.Focused Then
            current_item.AggregateMathFunction = GetAggregateFunctions()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub radioOpen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioOpen.CheckedChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub radioFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioFixed.CheckedChanged
        If Not current_item Is Nothing And Not mIsLoading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbFixedOffset_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFixedOffset.CheckedChanged
        NumericOffset.Visible = cbFixedOffset.Checked
        comboOffsetUnits.Visible = cbFixedOffset.Checked

        If Not current_item Is Nothing Then
            If cbFixedOffset.Checked Then
                current_item.hasFixedOffset = True
                ' may accept default so set them
                current_item.Offset = CInt(NumericOffset.Value)
                current_item.OffsetUnits = CType(comboOffsetUnits.SelectedItem, TimeUnits.TimeUnit).ISOunit
            Else
                current_item.hasFixedOffset = False
            End If

            If Not mIsLoading Then
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub cbFixedInterval_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFixedInterval.CheckedChanged
        numericDuration.Visible = cbFixedInterval.Checked
        comboDurationUnits.Visible = cbFixedInterval.Checked

        If Not current_item Is Nothing Then
            If cbFixedInterval.Checked Then
                current_item.hasFixedWidth = True
                ' may accept default so load these
                current_item.Width = CInt(numericDuration.Value)
                current_item.WidthUnits = CType(comboDurationUnits.SelectedItem, TimeUnits.TimeUnit).ISOunit
            Else
                current_item.hasFixedWidth = False
            End If

            If Not mIsLoading Then
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub butRemoveElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveElement.Click
        If ListEvents.SelectedIndices.Count = 0 Or ListEvents.Items.Count = 1 Then
            'must be one selected and more than one in the list - have to have one event!
            Beep()
        Else
            Dim elvi As EventListViewItem = CType(ListEvents.SelectedItems(0), EventListViewItem)

            If MessageBox.Show(AE_Constants.Instance.Remove & elvi.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                ' leave an item selected if there is one
                If elvi.Index > 0 Then
                    ListEvents.Items(elvi.Index - 1).Selected = True
                ElseIf ListEvents.Items.Count > 1 Then
                    ListEvents.Items(elvi.Index + 1).Selected = True
                End If

                elvi.Remove()
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not ListEvents.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i As Integer
            i = ListEvents.SelectedIndices(0)
            lvI = ListEvents.SelectedItems(0)

            If i > 0 Then
                ListEvents.Items.Remove(lvI)
                ListEvents.Items.Insert((i - 1), lvI)
                mFileManager.FileEdited = True
                ListEvents.Items.Item(i - 1).Selected = True
            End If
        End If
    End Sub

    Private Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not ListEvents.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i, c As Integer

            c = ListEvents.Items.Count
            i = ListEvents.SelectedIndices(0)
            lvI = ListEvents.SelectedItems(0)

            If i < c - 1 Then
                ListEvents.Items.Remove(lvI)
                ListEvents.Items.Insert((i + 1), lvI)
                lvI.Selected = True
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub ListEvents_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles ListEvents.AfterLabelEdit
        If Not e.Label Is Nothing Then
            Dim lvItem As EventListViewItem = CType(ListEvents.Items(e.Item), EventListViewItem)

            If e.Label = "" Then
                e.CancelEdit = True
            Else
                lvItem.Text = e.Label
            End If
        End If
    End Sub

    Private Sub buSetRuntimeConstraint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buSetRuntimeConstraint.Click
        Dim frm As New ConstraintForm
        Dim has_constraint As Boolean
        Dim t As Constraint_Text = Nothing
        has_constraint = current_item.hasNameConstraint

        If has_constraint Then
            t = CType(current_item.NameConstraint.Copy, Constraint_Text)
        End If

        frm.ShowConstraint(False, current_item.NameConstraint, mFileManager)

        Select Case frm.ShowDialog
            Case Windows.Forms.DialogResult.OK
                'no action
                mFileManager.FileEdited = True
            Case Windows.Forms.DialogResult.Cancel
                ' put it back to null if it was before
                If Not has_constraint Then
                    current_item.hasNameConstraint = False
                Else
                    current_item.NameConstraint = t
                End If
            Case Windows.Forms.DialogResult.Ignore
                current_item.hasNameConstraint = False
                mFileManager.FileEdited = True
        End Select

        If current_item.hasNameConstraint Then
            txtRuntimeConstraint.Text = current_item.NameConstraint.ToString
        Else
            txtRuntimeConstraint.Text = ""
        End If
    End Sub

    Private Sub TabPageEventSeries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HelpProviderEventSeries.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath

        mIsLoading = True

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        mIsLoading = False
    End Sub

    Private Sub comboTimeUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboTimeUnits.SelectedIndexChanged
        'Initialising
        If Not mFileManager Is Nothing And Not mIsLoading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub TabpageHistory_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        OceanArchetypeEditor.Reflect(Me)
    End Sub

    Private Sub ListEvents_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListEvents.KeyDown
        If Not current_item Is Nothing AndAlso e.KeyCode = Keys.Delete Then
            If MessageBox.Show(AE_Constants.Instance.Remove + " " + current_item.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                current_item.Remove()
            End If
        End If
    End Sub

    Private Sub SpecialiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpecialiseToolStripMenuItem.Click
        If Not current_item Is Nothing Then
            If MessageBox.Show(AE_Constants.Instance.Specialise & " '" & current_item.Text & "'?", _
                AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

                If Not (current_item.Occurrences.IsUnbounded Or current_item.Occurrences.MaxCount > 1) Then

                    current_item.Specialise()
                Else
                    Dim i As Integer = current_item.Index

                    current_item = current_item.Copy()
                    current_item.Specialise()

                    Me.ListEvents.Items.Insert(i + 1, current_item)

                    current_item.Selected = True
                    current_item.BeginEdit()
                    mFileManager.FileEdited = True
                End If
            End If
        End If
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
'The Original Code is TabpageHistory.vb.
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
