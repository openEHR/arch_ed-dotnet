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

Public Class PathwaySpecification
    Inherits System.Windows.Forms.UserControl

    Private mValidStateMachineClasses As StateMachineType()
    Private WithEvents mPathwayEvent As PathwayEvent
    Private mIsloading As Boolean
    Private mFileManager As FileManagerLocal
    Public Event StructureChanged(ByVal sender As Object, ByVal a_structure As StructureType)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If Not Me.DesignMode Then
            mFileManager = Filemanager.Master
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
    Friend WithEvents PanelRight As System.Windows.Forms.Panel
    Friend WithEvents gbCompleted As System.Windows.Forms.GroupBox
    Friend WithEvents PanelCompleted As System.Windows.Forms.Panel
    Friend WithEvents PanelRightBottom As System.Windows.Forms.Panel
    Friend WithEvents tabProperties As System.Windows.Forms.TabControl
    Friend WithEvents cbAborted As System.Windows.Forms.CheckBox
    Friend WithEvents cbSuspended As System.Windows.Forms.CheckBox
    Friend WithEvents cbAlternativeState As System.Windows.Forms.CheckBox
    Friend WithEvents PanelRightTop As System.Windows.Forms.Panel
    Friend WithEvents SplitterRight As System.Windows.Forms.Splitter
    Friend WithEvents gbSuspended As System.Windows.Forms.GroupBox
    Friend WithEvents PanelSuspendActive As System.Windows.Forms.Panel
    Friend WithEvents PanelSuspendInitial As System.Windows.Forms.Panel
    Friend WithEvents gbAborted As System.Windows.Forms.GroupBox
    Friend WithEvents PanelAbortActive As System.Windows.Forms.Panel
    Friend WithEvents PanelAbortInitial As System.Windows.Forms.Panel
    Friend WithEvents PanelMiddle As System.Windows.Forms.Panel
    Friend WithEvents gbActive As System.Windows.Forms.GroupBox
    Friend WithEvents PanelActive As System.Windows.Forms.Panel
    Friend WithEvents gbPlanned As System.Windows.Forms.GroupBox
    Friend WithEvents PanelPlanned As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuState As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuAdd As System.Windows.Forms.MenuItem
    Friend WithEvents gbSuspendedInitial As System.Windows.Forms.GroupBox
    Friend WithEvents gbTop As System.Windows.Forms.GroupBox
    Friend WithEvents gbSuspendedActive As System.Windows.Forms.GroupBox
    Friend WithEvents gbAbortedInitial As System.Windows.Forms.GroupBox
    Friend WithEvents gbBottom As System.Windows.Forms.GroupBox
    Friend WithEvents gbAbortedActive As System.Windows.Forms.GroupBox
    Friend WithEvents panelBottomSpacer As System.Windows.Forms.Panel
    Friend WithEvents panelTopSpacer As System.Windows.Forms.Panel
    Friend WithEvents SplitterTop As System.Windows.Forms.Splitter
    Friend WithEvents SplitterBottom As System.Windows.Forms.Splitter
    Friend WithEvents PanelSpacer As System.Windows.Forms.Panel
    Friend WithEvents pbActiveTopUp As System.Windows.Forms.PictureBox
    Friend WithEvents pbActiveTopDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbPlannedTopUp As System.Windows.Forms.PictureBox
    Friend WithEvents pbPlannedTopDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbActiveBottomDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbPlannedBottomDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbPlannedToActive As System.Windows.Forms.PictureBox
    Friend WithEvents pbActiveToCompleted As System.Windows.Forms.PictureBox
    Friend WithEvents PanelCompletedSpacer As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents lblAllowTransition As System.Windows.Forms.Label
    Friend WithEvents lblAllowAltState As System.Windows.Forms.Label
    Friend WithEvents tpTransition As System.Windows.Forms.TabPage
    Friend WithEvents tpState As System.Windows.Forms.TabPage
    Friend WithEvents gbScheduled As System.Windows.Forms.GroupBox
    Friend WithEvents PanelScheduled As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PathwaySpecification))
        Me.PanelRight = New System.Windows.Forms.Panel
        Me.gbCompleted = New System.Windows.Forms.GroupBox
        Me.PanelCompleted = New System.Windows.Forms.Panel
        Me.ContextMenuState = New System.Windows.Forms.ContextMenu
        Me.MenuAdd = New System.Windows.Forms.MenuItem
        Me.PanelRightBottom = New System.Windows.Forms.Panel
        Me.tabProperties = New System.Windows.Forms.TabControl
        Me.tpTransition = New System.Windows.Forms.TabPage
        Me.lblAllowTransition = New System.Windows.Forms.Label
        Me.cbAborted = New System.Windows.Forms.CheckBox
        Me.cbSuspended = New System.Windows.Forms.CheckBox
        Me.tpState = New System.Windows.Forms.TabPage
        Me.lblAllowAltState = New System.Windows.Forms.Label
        Me.cbAlternativeState = New System.Windows.Forms.CheckBox
        Me.PanelRightTop = New System.Windows.Forms.Panel
        Me.SplitterRight = New System.Windows.Forms.Splitter
        Me.gbSuspended = New System.Windows.Forms.GroupBox
        Me.gbSuspendedActive = New System.Windows.Forms.GroupBox
        Me.PanelSuspendActive = New System.Windows.Forms.Panel
        Me.gbTop = New System.Windows.Forms.GroupBox
        Me.gbSuspendedInitial = New System.Windows.Forms.GroupBox
        Me.PanelSuspendInitial = New System.Windows.Forms.Panel
        Me.gbAborted = New System.Windows.Forms.GroupBox
        Me.gbAbortedActive = New System.Windows.Forms.GroupBox
        Me.PanelAbortActive = New System.Windows.Forms.Panel
        Me.gbBottom = New System.Windows.Forms.GroupBox
        Me.gbAbortedInitial = New System.Windows.Forms.GroupBox
        Me.PanelAbortInitial = New System.Windows.Forms.Panel
        Me.PanelMiddle = New System.Windows.Forms.Panel
        Me.gbActive = New System.Windows.Forms.GroupBox
        Me.PanelActive = New System.Windows.Forms.Panel
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.gbScheduled = New System.Windows.Forms.GroupBox
        Me.PanelScheduled = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelLeft = New System.Windows.Forms.Panel
        Me.gbPlanned = New System.Windows.Forms.GroupBox
        Me.PanelPlanned = New System.Windows.Forms.Panel
        Me.PanelSpacer = New System.Windows.Forms.Panel
        Me.pbPlannedToActive = New System.Windows.Forms.PictureBox
        Me.PanelCompletedSpacer = New System.Windows.Forms.Panel
        Me.pbActiveToCompleted = New System.Windows.Forms.PictureBox
        Me.panelBottomSpacer = New System.Windows.Forms.Panel
        Me.pbPlannedBottomDown = New System.Windows.Forms.PictureBox
        Me.pbActiveBottomDown = New System.Windows.Forms.PictureBox
        Me.panelTopSpacer = New System.Windows.Forms.Panel
        Me.pbPlannedTopDown = New System.Windows.Forms.PictureBox
        Me.pbPlannedTopUp = New System.Windows.Forms.PictureBox
        Me.pbActiveTopDown = New System.Windows.Forms.PictureBox
        Me.pbActiveTopUp = New System.Windows.Forms.PictureBox
        Me.SplitterTop = New System.Windows.Forms.Splitter
        Me.SplitterBottom = New System.Windows.Forms.Splitter
        Me.PanelRight.SuspendLayout()
        Me.gbCompleted.SuspendLayout()
        Me.PanelRightBottom.SuspendLayout()
        Me.tabProperties.SuspendLayout()
        Me.tpTransition.SuspendLayout()
        Me.tpState.SuspendLayout()
        Me.gbSuspended.SuspendLayout()
        Me.gbSuspendedActive.SuspendLayout()
        Me.gbSuspendedInitial.SuspendLayout()
        Me.gbAborted.SuspendLayout()
        Me.gbAbortedActive.SuspendLayout()
        Me.gbAbortedInitial.SuspendLayout()
        Me.PanelMiddle.SuspendLayout()
        Me.gbActive.SuspendLayout()
        Me.gbScheduled.SuspendLayout()
        Me.PanelLeft.SuspendLayout()
        Me.gbPlanned.SuspendLayout()
        Me.PanelSpacer.SuspendLayout()
        CType(Me.pbPlannedToActive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelCompletedSpacer.SuspendLayout()
        CType(Me.pbActiveToCompleted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelBottomSpacer.SuspendLayout()
        CType(Me.pbPlannedBottomDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbActiveBottomDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTopSpacer.SuspendLayout()
        CType(Me.pbPlannedTopDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPlannedTopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbActiveTopDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbActiveTopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelRight
        '
        Me.PanelRight.Controls.Add(Me.gbCompleted)
        Me.PanelRight.Controls.Add(Me.PanelRightBottom)
        Me.PanelRight.Controls.Add(Me.PanelRightTop)
        Me.PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelRight.Location = New System.Drawing.Point(808, 0)
        Me.PanelRight.Name = "PanelRight"
        Me.PanelRight.Size = New System.Drawing.Size(160, 616)
        Me.PanelRight.TabIndex = 8
        '
        'gbCompleted
        '
        Me.gbCompleted.BackColor = System.Drawing.Color.LightYellow
        Me.gbCompleted.Controls.Add(Me.PanelCompleted)
        Me.gbCompleted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCompleted.Location = New System.Drawing.Point(0, 152)
        Me.gbCompleted.Name = "gbCompleted"
        Me.gbCompleted.Size = New System.Drawing.Size(160, 316)
        Me.gbCompleted.TabIndex = 2
        Me.gbCompleted.TabStop = False
        Me.gbCompleted.Text = "Completed"
        '
        'PanelCompleted
        '
        Me.PanelCompleted.BackColor = System.Drawing.Color.Ivory
        Me.PanelCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelCompleted.ContextMenu = Me.ContextMenuState
        Me.PanelCompleted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelCompleted.Location = New System.Drawing.Point(3, 16)
        Me.PanelCompleted.Name = "PanelCompleted"
        Me.PanelCompleted.Size = New System.Drawing.Size(154, 297)
        Me.PanelCompleted.TabIndex = 0
        Me.PanelCompleted.TabStop = True
        '
        'ContextMenuState
        '
        Me.ContextMenuState.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuAdd})
        '
        'MenuAdd
        '
        Me.MenuAdd.Index = 0
        Me.MenuAdd.Text = "Add Machine State"
        '
        'PanelRightBottom
        '
        Me.PanelRightBottom.Controls.Add(Me.tabProperties)
        Me.PanelRightBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelRightBottom.Location = New System.Drawing.Point(0, 468)
        Me.PanelRightBottom.Name = "PanelRightBottom"
        Me.PanelRightBottom.Size = New System.Drawing.Size(160, 148)
        Me.PanelRightBottom.TabIndex = 2
        '
        'tabProperties
        '
        Me.tabProperties.Controls.Add(Me.tpTransition)
        Me.tabProperties.Controls.Add(Me.tpState)
        Me.tabProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProperties.Location = New System.Drawing.Point(0, 0)
        Me.tabProperties.Multiline = True
        Me.tabProperties.Name = "tabProperties"
        Me.tabProperties.SelectedIndex = 0
        Me.tabProperties.Size = New System.Drawing.Size(160, 148)
        Me.tabProperties.TabIndex = 0
        Me.tabProperties.Visible = False
        '
        'tpTransition
        '
        Me.tpTransition.Controls.Add(Me.lblAllowTransition)
        Me.tpTransition.Controls.Add(Me.cbAborted)
        Me.tpTransition.Controls.Add(Me.cbSuspended)
        Me.tpTransition.Location = New System.Drawing.Point(4, 22)
        Me.tpTransition.Name = "tpTransition"
        Me.tpTransition.Size = New System.Drawing.Size(152, 122)
        Me.tpTransition.TabIndex = 0
        Me.tpTransition.Text = "Transition"
        '
        'lblAllowTransition
        '
        Me.lblAllowTransition.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAllowTransition.Location = New System.Drawing.Point(0, 0)
        Me.lblAllowTransition.Name = "lblAllowTransition"
        Me.lblAllowTransition.Size = New System.Drawing.Size(152, 32)
        Me.lblAllowTransition.TabIndex = 3
        Me.lblAllowTransition.Text = "Allow transition to:"
        '
        'cbAborted
        '
        Me.cbAborted.Location = New System.Drawing.Point(24, 62)
        Me.cbAborted.Name = "cbAborted"
        Me.cbAborted.Size = New System.Drawing.Size(125, 24)
        Me.cbAborted.TabIndex = 2
        Me.cbAborted.Text = "Aborted"
        Me.cbAborted.Visible = False
        '
        'cbSuspended
        '
        Me.cbSuspended.Location = New System.Drawing.Point(24, 30)
        Me.cbSuspended.Name = "cbSuspended"
        Me.cbSuspended.Size = New System.Drawing.Size(125, 32)
        Me.cbSuspended.TabIndex = 1
        Me.cbSuspended.Text = "Suspended"
        Me.cbSuspended.Visible = False
        '
        'tpState
        '
        Me.tpState.Controls.Add(Me.lblAllowAltState)
        Me.tpState.Controls.Add(Me.cbAlternativeState)
        Me.tpState.Location = New System.Drawing.Point(4, 22)
        Me.tpState.Name = "tpState"
        Me.tpState.Size = New System.Drawing.Size(152, 122)
        Me.tpState.TabIndex = 1
        Me.tpState.Text = "State"
        Me.tpState.Visible = False
        '
        'lblAllowAltState
        '
        Me.lblAllowAltState.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAllowAltState.Location = New System.Drawing.Point(0, 0)
        Me.lblAllowAltState.Name = "lblAllowAltState"
        Me.lblAllowAltState.Size = New System.Drawing.Size(152, 32)
        Me.lblAllowAltState.TabIndex = 2
        Me.lblAllowAltState.Text = "Allow alternative state for this action."
        '
        'cbAlternativeState
        '
        Me.cbAlternativeState.Location = New System.Drawing.Point(24, 40)
        Me.cbAlternativeState.Name = "cbAlternativeState"
        Me.cbAlternativeState.Size = New System.Drawing.Size(192, 32)
        Me.cbAlternativeState.TabIndex = 1
        Me.cbAlternativeState.Text = "Alternative state"
        '
        'PanelRightTop
        '
        Me.PanelRightTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelRightTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelRightTop.Name = "PanelRightTop"
        Me.PanelRightTop.Size = New System.Drawing.Size(160, 152)
        Me.PanelRightTop.TabIndex = 0
        '
        'SplitterRight
        '
        Me.SplitterRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.SplitterRight.Location = New System.Drawing.Point(805, 0)
        Me.SplitterRight.Name = "SplitterRight"
        Me.SplitterRight.Size = New System.Drawing.Size(3, 616)
        Me.SplitterRight.TabIndex = 1
        Me.SplitterRight.TabStop = False
        '
        'gbSuspended
        '
        Me.gbSuspended.Controls.Add(Me.gbSuspendedActive)
        Me.gbSuspended.Controls.Add(Me.gbTop)
        Me.gbSuspended.Controls.Add(Me.gbSuspendedInitial)
        Me.gbSuspended.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbSuspended.Location = New System.Drawing.Point(0, 0)
        Me.gbSuspended.Name = "gbSuspended"
        Me.gbSuspended.Size = New System.Drawing.Size(805, 128)
        Me.gbSuspended.TabIndex = 0
        Me.gbSuspended.TabStop = False
        '
        'gbSuspendedActive
        '
        Me.gbSuspendedActive.Controls.Add(Me.PanelSuspendActive)
        Me.gbSuspendedActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSuspendedActive.Location = New System.Drawing.Point(152, 16)
        Me.gbSuspendedActive.Name = "gbSuspendedActive"
        Me.gbSuspendedActive.Size = New System.Drawing.Size(650, 109)
        Me.gbSuspendedActive.TabIndex = 2
        Me.gbSuspendedActive.TabStop = False
        Me.gbSuspendedActive.Text = "Suspended"
        '
        'PanelSuspendActive
        '
        Me.PanelSuspendActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelSuspendActive.ContextMenu = Me.ContextMenuState
        Me.PanelSuspendActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSuspendActive.Location = New System.Drawing.Point(3, 16)
        Me.PanelSuspendActive.Name = "PanelSuspendActive"
        Me.PanelSuspendActive.Size = New System.Drawing.Size(644, 90)
        Me.PanelSuspendActive.TabIndex = 0
        Me.PanelSuspendActive.TabStop = True
        '
        'gbTop
        '
        Me.gbTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbTop.Location = New System.Drawing.Point(128, 16)
        Me.gbTop.Name = "gbTop"
        Me.gbTop.Size = New System.Drawing.Size(24, 109)
        Me.gbTop.TabIndex = 1
        Me.gbTop.TabStop = False
        '
        'gbSuspendedInitial
        '
        Me.gbSuspendedInitial.Controls.Add(Me.PanelSuspendInitial)
        Me.gbSuspendedInitial.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbSuspendedInitial.Location = New System.Drawing.Point(3, 16)
        Me.gbSuspendedInitial.Name = "gbSuspendedInitial"
        Me.gbSuspendedInitial.Size = New System.Drawing.Size(125, 109)
        Me.gbSuspendedInitial.TabIndex = 0
        Me.gbSuspendedInitial.TabStop = False
        Me.gbSuspendedInitial.Text = "Postponed"
        '
        'PanelSuspendInitial
        '
        Me.PanelSuspendInitial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelSuspendInitial.ContextMenu = Me.ContextMenuState
        Me.PanelSuspendInitial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSuspendInitial.Location = New System.Drawing.Point(3, 16)
        Me.PanelSuspendInitial.Name = "PanelSuspendInitial"
        Me.PanelSuspendInitial.Size = New System.Drawing.Size(119, 90)
        Me.PanelSuspendInitial.TabIndex = 0
        Me.PanelSuspendInitial.TabStop = True
        '
        'gbAborted
        '
        Me.gbAborted.Controls.Add(Me.gbAbortedActive)
        Me.gbAborted.Controls.Add(Me.gbBottom)
        Me.gbAborted.Controls.Add(Me.gbAbortedInitial)
        Me.gbAborted.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbAborted.Location = New System.Drawing.Point(0, 488)
        Me.gbAborted.Name = "gbAborted"
        Me.gbAborted.Size = New System.Drawing.Size(805, 128)
        Me.gbAborted.TabIndex = 4
        Me.gbAborted.TabStop = False
        '
        'gbAbortedActive
        '
        Me.gbAbortedActive.Controls.Add(Me.PanelAbortActive)
        Me.gbAbortedActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbAbortedActive.Location = New System.Drawing.Point(157, 16)
        Me.gbAbortedActive.Name = "gbAbortedActive"
        Me.gbAbortedActive.Size = New System.Drawing.Size(645, 109)
        Me.gbAbortedActive.TabIndex = 2
        Me.gbAbortedActive.TabStop = False
        Me.gbAbortedActive.Text = "Aborted"
        '
        'PanelAbortActive
        '
        Me.PanelAbortActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelAbortActive.ContextMenu = Me.ContextMenuState
        Me.PanelAbortActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelAbortActive.Location = New System.Drawing.Point(3, 16)
        Me.PanelAbortActive.Name = "PanelAbortActive"
        Me.PanelAbortActive.Size = New System.Drawing.Size(639, 90)
        Me.PanelAbortActive.TabIndex = 0
        Me.PanelAbortActive.TabStop = True
        '
        'gbBottom
        '
        Me.gbBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbBottom.Location = New System.Drawing.Point(128, 16)
        Me.gbBottom.Name = "gbBottom"
        Me.gbBottom.Size = New System.Drawing.Size(29, 109)
        Me.gbBottom.TabIndex = 1
        Me.gbBottom.TabStop = False
        '
        'gbAbortedInitial
        '
        Me.gbAbortedInitial.Controls.Add(Me.PanelAbortInitial)
        Me.gbAbortedInitial.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbAbortedInitial.Location = New System.Drawing.Point(3, 16)
        Me.gbAbortedInitial.Name = "gbAbortedInitial"
        Me.gbAbortedInitial.Size = New System.Drawing.Size(125, 109)
        Me.gbAbortedInitial.TabIndex = 0
        Me.gbAbortedInitial.TabStop = False
        Me.gbAbortedInitial.Text = "Cancelled"
        '
        'PanelAbortInitial
        '
        Me.PanelAbortInitial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelAbortInitial.ContextMenu = Me.ContextMenuState
        Me.PanelAbortInitial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelAbortInitial.Location = New System.Drawing.Point(3, 16)
        Me.PanelAbortInitial.Name = "PanelAbortInitial"
        Me.PanelAbortInitial.Size = New System.Drawing.Size(119, 90)
        Me.PanelAbortInitial.TabIndex = 0
        Me.PanelAbortInitial.TabStop = True
        '
        'PanelMiddle
        '
        Me.PanelMiddle.Controls.Add(Me.gbActive)
        Me.PanelMiddle.Controls.Add(Me.Splitter2)
        Me.PanelMiddle.Controls.Add(Me.gbScheduled)
        Me.PanelMiddle.Controls.Add(Me.Splitter1)
        Me.PanelMiddle.Controls.Add(Me.PanelLeft)
        Me.PanelMiddle.Controls.Add(Me.PanelCompletedSpacer)
        Me.PanelMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMiddle.Location = New System.Drawing.Point(0, 151)
        Me.PanelMiddle.Name = "PanelMiddle"
        Me.PanelMiddle.Size = New System.Drawing.Size(805, 314)
        Me.PanelMiddle.TabIndex = 3
        '
        'gbActive
        '
        Me.gbActive.BackColor = System.Drawing.Color.LightYellow
        Me.gbActive.Controls.Add(Me.PanelActive)
        Me.gbActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbActive.Location = New System.Drawing.Point(283, 0)
        Me.gbActive.Name = "gbActive"
        Me.gbActive.Size = New System.Drawing.Size(498, 314)
        Me.gbActive.TabIndex = 2
        Me.gbActive.TabStop = False
        Me.gbActive.Text = "Active"
        '
        'PanelActive
        '
        Me.PanelActive.BackColor = System.Drawing.Color.Ivory
        Me.PanelActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelActive.ContextMenu = Me.ContextMenuState
        Me.PanelActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelActive.Location = New System.Drawing.Point(3, 16)
        Me.PanelActive.Name = "PanelActive"
        Me.PanelActive.Size = New System.Drawing.Size(492, 295)
        Me.PanelActive.TabIndex = 0
        Me.PanelActive.TabStop = True
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(275, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(8, 314)
        Me.Splitter2.TabIndex = 9
        Me.Splitter2.TabStop = False
        '
        'gbScheduled
        '
        Me.gbScheduled.Controls.Add(Me.PanelScheduled)
        Me.gbScheduled.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbScheduled.Location = New System.Drawing.Point(163, 0)
        Me.gbScheduled.Name = "gbScheduled"
        Me.gbScheduled.Size = New System.Drawing.Size(112, 314)
        Me.gbScheduled.TabIndex = 1
        Me.gbScheduled.TabStop = False
        Me.gbScheduled.Text = "Scheduled"
        '
        'PanelScheduled
        '
        Me.PanelScheduled.BackColor = System.Drawing.Color.Ivory
        Me.PanelScheduled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelScheduled.ContextMenu = Me.ContextMenuState
        Me.PanelScheduled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelScheduled.Location = New System.Drawing.Point(3, 16)
        Me.PanelScheduled.Name = "PanelScheduled"
        Me.PanelScheduled.Size = New System.Drawing.Size(106, 295)
        Me.PanelScheduled.TabIndex = 0
        Me.PanelScheduled.TabStop = True
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(160, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 314)
        Me.Splitter1.TabIndex = 7
        Me.Splitter1.TabStop = False
        '
        'PanelLeft
        '
        Me.PanelLeft.Controls.Add(Me.gbPlanned)
        Me.PanelLeft.Controls.Add(Me.PanelSpacer)
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelLeft.Location = New System.Drawing.Point(0, 0)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(160, 314)
        Me.PanelLeft.TabIndex = 0
        '
        'gbPlanned
        '
        Me.gbPlanned.BackColor = System.Drawing.Color.LightYellow
        Me.gbPlanned.Controls.Add(Me.PanelPlanned)
        Me.gbPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbPlanned.Location = New System.Drawing.Point(0, 0)
        Me.gbPlanned.Name = "gbPlanned"
        Me.gbPlanned.Size = New System.Drawing.Size(136, 314)
        Me.gbPlanned.TabIndex = 0
        Me.gbPlanned.TabStop = False
        Me.gbPlanned.Text = "Planned"
        '
        'PanelPlanned
        '
        Me.PanelPlanned.BackColor = System.Drawing.Color.Ivory
        Me.PanelPlanned.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelPlanned.ContextMenu = Me.ContextMenuState
        Me.PanelPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPlanned.Location = New System.Drawing.Point(3, 16)
        Me.PanelPlanned.Name = "PanelPlanned"
        Me.PanelPlanned.Size = New System.Drawing.Size(130, 295)
        Me.PanelPlanned.TabIndex = 0
        Me.PanelPlanned.TabStop = True
        '
        'PanelSpacer
        '
        Me.PanelSpacer.BackColor = System.Drawing.Color.LightYellow
        Me.PanelSpacer.Controls.Add(Me.pbPlannedToActive)
        Me.PanelSpacer.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSpacer.Location = New System.Drawing.Point(136, 0)
        Me.PanelSpacer.Name = "PanelSpacer"
        Me.PanelSpacer.Size = New System.Drawing.Size(24, 314)
        Me.PanelSpacer.TabIndex = 1
        '
        'pbPlannedToActive
        '
        Me.pbPlannedToActive.Image = CType(resources.GetObject("pbPlannedToActive.Image"), System.Drawing.Image)
        Me.pbPlannedToActive.Location = New System.Drawing.Point(5, 144)
        Me.pbPlannedToActive.Name = "pbPlannedToActive"
        Me.pbPlannedToActive.Size = New System.Drawing.Size(16, 16)
        Me.pbPlannedToActive.TabIndex = 5
        Me.pbPlannedToActive.TabStop = False
        '
        'PanelCompletedSpacer
        '
        Me.PanelCompletedSpacer.BackColor = System.Drawing.Color.LightYellow
        Me.PanelCompletedSpacer.Controls.Add(Me.pbActiveToCompleted)
        Me.PanelCompletedSpacer.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelCompletedSpacer.Location = New System.Drawing.Point(781, 0)
        Me.PanelCompletedSpacer.Name = "PanelCompletedSpacer"
        Me.PanelCompletedSpacer.Size = New System.Drawing.Size(24, 314)
        Me.PanelCompletedSpacer.TabIndex = 3
        '
        'pbActiveToCompleted
        '
        Me.pbActiveToCompleted.Image = CType(resources.GetObject("pbActiveToCompleted.Image"), System.Drawing.Image)
        Me.pbActiveToCompleted.Location = New System.Drawing.Point(4, 160)
        Me.pbActiveToCompleted.Name = "pbActiveToCompleted"
        Me.pbActiveToCompleted.Size = New System.Drawing.Size(16, 16)
        Me.pbActiveToCompleted.TabIndex = 6
        Me.pbActiveToCompleted.TabStop = False
        '
        'panelBottomSpacer
        '
        Me.panelBottomSpacer.Controls.Add(Me.pbPlannedBottomDown)
        Me.panelBottomSpacer.Controls.Add(Me.pbActiveBottomDown)
        Me.panelBottomSpacer.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBottomSpacer.Location = New System.Drawing.Point(0, 468)
        Me.panelBottomSpacer.Name = "panelBottomSpacer"
        Me.panelBottomSpacer.Size = New System.Drawing.Size(805, 20)
        Me.panelBottomSpacer.TabIndex = 9
        '
        'pbPlannedBottomDown
        '
        Me.pbPlannedBottomDown.Image = CType(resources.GetObject("pbPlannedBottomDown.Image"), System.Drawing.Image)
        Me.pbPlannedBottomDown.Location = New System.Drawing.Point(56, 2)
        Me.pbPlannedBottomDown.Name = "pbPlannedBottomDown"
        Me.pbPlannedBottomDown.Size = New System.Drawing.Size(16, 16)
        Me.pbPlannedBottomDown.TabIndex = 5
        Me.pbPlannedBottomDown.TabStop = False
        '
        'pbActiveBottomDown
        '
        Me.pbActiveBottomDown.Image = CType(resources.GetObject("pbActiveBottomDown.Image"), System.Drawing.Image)
        Me.pbActiveBottomDown.Location = New System.Drawing.Point(400, 2)
        Me.pbActiveBottomDown.Name = "pbActiveBottomDown"
        Me.pbActiveBottomDown.Size = New System.Drawing.Size(16, 16)
        Me.pbActiveBottomDown.TabIndex = 2
        Me.pbActiveBottomDown.TabStop = False
        '
        'panelTopSpacer
        '
        Me.panelTopSpacer.Controls.Add(Me.pbPlannedTopDown)
        Me.panelTopSpacer.Controls.Add(Me.pbPlannedTopUp)
        Me.panelTopSpacer.Controls.Add(Me.pbActiveTopDown)
        Me.panelTopSpacer.Controls.Add(Me.pbActiveTopUp)
        Me.panelTopSpacer.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTopSpacer.Location = New System.Drawing.Point(0, 128)
        Me.panelTopSpacer.Name = "panelTopSpacer"
        Me.panelTopSpacer.Size = New System.Drawing.Size(805, 20)
        Me.panelTopSpacer.TabIndex = 2
        '
        'pbPlannedTopDown
        '
        Me.pbPlannedTopDown.Image = CType(resources.GetObject("pbPlannedTopDown.Image"), System.Drawing.Image)
        Me.pbPlannedTopDown.Location = New System.Drawing.Point(56, 2)
        Me.pbPlannedTopDown.Name = "pbPlannedTopDown"
        Me.pbPlannedTopDown.Size = New System.Drawing.Size(16, 16)
        Me.pbPlannedTopDown.TabIndex = 3
        Me.pbPlannedTopDown.TabStop = False
        '
        'pbPlannedTopUp
        '
        Me.pbPlannedTopUp.Image = CType(resources.GetObject("pbPlannedTopUp.Image"), System.Drawing.Image)
        Me.pbPlannedTopUp.Location = New System.Drawing.Point(40, 2)
        Me.pbPlannedTopUp.Name = "pbPlannedTopUp"
        Me.pbPlannedTopUp.Size = New System.Drawing.Size(16, 16)
        Me.pbPlannedTopUp.TabIndex = 2
        Me.pbPlannedTopUp.TabStop = False
        '
        'pbActiveTopDown
        '
        Me.pbActiveTopDown.Image = CType(resources.GetObject("pbActiveTopDown.Image"), System.Drawing.Image)
        Me.pbActiveTopDown.Location = New System.Drawing.Point(384, 2)
        Me.pbActiveTopDown.Name = "pbActiveTopDown"
        Me.pbActiveTopDown.Size = New System.Drawing.Size(16, 16)
        Me.pbActiveTopDown.TabIndex = 1
        Me.pbActiveTopDown.TabStop = False
        '
        'pbActiveTopUp
        '
        Me.pbActiveTopUp.Image = CType(resources.GetObject("pbActiveTopUp.Image"), System.Drawing.Image)
        Me.pbActiveTopUp.Location = New System.Drawing.Point(368, 2)
        Me.pbActiveTopUp.Name = "pbActiveTopUp"
        Me.pbActiveTopUp.Size = New System.Drawing.Size(16, 16)
        Me.pbActiveTopUp.TabIndex = 0
        Me.pbActiveTopUp.TabStop = False
        '
        'SplitterTop
        '
        Me.SplitterTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitterTop.Location = New System.Drawing.Point(0, 148)
        Me.SplitterTop.Name = "SplitterTop"
        Me.SplitterTop.Size = New System.Drawing.Size(805, 3)
        Me.SplitterTop.TabIndex = 11
        Me.SplitterTop.TabStop = False
        '
        'SplitterBottom
        '
        Me.SplitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitterBottom.Location = New System.Drawing.Point(0, 465)
        Me.SplitterBottom.Name = "SplitterBottom"
        Me.SplitterBottom.Size = New System.Drawing.Size(805, 3)
        Me.SplitterBottom.TabIndex = 3
        Me.SplitterBottom.TabStop = False
        '
        'PathwaySpecification
        '
        Me.BackColor = System.Drawing.Color.LightYellow
        Me.Controls.Add(Me.PanelMiddle)
        Me.Controls.Add(Me.SplitterBottom)
        Me.Controls.Add(Me.SplitterTop)
        Me.Controls.Add(Me.panelTopSpacer)
        Me.Controls.Add(Me.panelBottomSpacer)
        Me.Controls.Add(Me.gbAborted)
        Me.Controls.Add(Me.gbSuspended)
        Me.Controls.Add(Me.SplitterRight)
        Me.Controls.Add(Me.PanelRight)
        Me.Name = "PathwaySpecification"
        Me.Size = New System.Drawing.Size(968, 616)
        Me.PanelRight.ResumeLayout(False)
        Me.gbCompleted.ResumeLayout(False)
        Me.PanelRightBottom.ResumeLayout(False)
        Me.tabProperties.ResumeLayout(False)
        Me.tpTransition.ResumeLayout(False)
        Me.tpState.ResumeLayout(False)
        Me.gbSuspended.ResumeLayout(False)
        Me.gbSuspendedActive.ResumeLayout(False)
        Me.gbSuspendedInitial.ResumeLayout(False)
        Me.gbAborted.ResumeLayout(False)
        Me.gbAbortedActive.ResumeLayout(False)
        Me.gbAbortedInitial.ResumeLayout(False)
        Me.PanelMiddle.ResumeLayout(False)
        Me.gbActive.ResumeLayout(False)
        Me.gbScheduled.ResumeLayout(False)
        Me.PanelLeft.ResumeLayout(False)
        Me.gbPlanned.ResumeLayout(False)
        Me.PanelSpacer.ResumeLayout(False)
        CType(Me.pbPlannedToActive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelCompletedSpacer.ResumeLayout(False)
        CType(Me.pbActiveToCompleted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelBottomSpacer.ResumeLayout(False)
        CType(Me.pbPlannedBottomDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbActiveBottomDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTopSpacer.ResumeLayout(False)
        CType(Me.pbPlannedTopDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPlannedTopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbActiveTopDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbActiveTopUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub PathwaySpecification_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mValidStateMachineClasses = ReferenceModel.ValidStateMachineTypes
        PathwaySpecification_Resize(sender, e)
    End Sub

    Property PathwaySteps() As RmStructureCompound
        Get
            Return BuildPathwaySteps()
        End Get
        Set(ByVal Value As RmStructureCompound)
            Debug.Assert(Value.Type = StructureType.ISM_TRANSITION)

            For Each ms As RmPathwayStep In Value.Children
                Dim pv As New PathwayEvent(ms, mFileManager)

                Select Case pv.DefaultStateMachineType
                    Case StateMachineType.ActiveAborted
                        AddPathwayEvent(PanelAbortActive, pv)
                    Case StateMachineType.Active
                        AddPathwayEvent(PanelActive, pv)
                    Case StateMachineType.ActiveSuspended
                        AddPathwayEvent(PanelSuspendActive, pv)
                    Case StateMachineType.Planned, StateMachineType.Initial
                        AddPathwayEvent(PanelPlanned, pv)
                    Case StateMachineType.InitialAborted
                        AddPathwayEvent(PanelAbortInitial, pv)
                    Case StateMachineType.InitialSuspended
                        AddPathwayEvent(PanelSuspendInitial, pv)
                    Case StateMachineType.Completed
                        AddPathwayEvent(PanelCompleted, pv)
                    Case StateMachineType.Scheduled
                        AddPathwayEvent(PanelScheduled, pv)
                    Case Else
                        Debug.Assert(False)
                End Select
            Next
        End Set
    End Property

#Region "Resizing and form layout"

    Private Sub LayOutControls(ByVal Ctrl As Control)
        Dim i, total_width As Integer
        Dim spacer As Integer = 5
        Dim left_margin As Integer = 5
        Dim scalefactor As Double = 1
        Dim path_event As PathwayEvent

        i = Ctrl.Controls.Count

        If i <> 0 Then
            total_width = PathwayEvent.DefaultWidth * i

            If total_width > Ctrl.Width Then
                scalefactor = (Ctrl.Width - (spacer * i) - left_margin) / total_width
            Else
                scalefactor = 1
            End If

            For i = 0 To Ctrl.Controls.Count - 1
                path_event = CType(Ctrl.Controls(i), PathwayEvent)
                path_event.Width = (PathwayEvent.DefaultWidth * scalefactor)
                path_event.Height = Ctrl.Height - 6
                path_event.Top = 3
                path_event.Left = left_margin
                left_margin = path_event.Left + path_event.Width + spacer
            Next
        End If
    End Sub

    Private Sub Panel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelActive.Resize, PanelAbortActive.Resize, PanelAbortInitial.Resize, PanelPlanned.Resize, PanelSuspendInitial.Resize, PanelCompleted.Resize, PanelSuspendActive.Resize, PanelScheduled.Resize
        LayOutControls(CType(sender, Control))
    End Sub

#End Region

    Private Sub GetPathwaySteps(ByVal Parent As Children, ByVal Ctrl As Control)
        For Each c As Control In Ctrl.Controls
            If TypeOf c Is PathwayEvent Then
                Parent.Add(CType(c, PathwayEvent).Item)
            End If
        Next
    End Sub

    Private Function BuildPathwaySteps() As RmStructureCompound
        Dim rm As New RmStructureCompound("ism_transition", StructureType.ISM_TRANSITION)

        GetPathwaySteps(rm.Children, PanelPlanned)
        GetPathwaySteps(rm.Children, PanelSuspendInitial)
        GetPathwaySteps(rm.Children, PanelAbortInitial)
        GetPathwaySteps(rm.Children, PanelScheduled)
        GetPathwaySteps(rm.Children, PanelActive)
        GetPathwaySteps(rm.Children, PanelSuspendActive)
        GetPathwaySteps(rm.Children, PanelAbortActive)
        GetPathwaySteps(rm.Children, PanelCompleted)

        Return rm
    End Function

    Private Sub OnDeleted(ByVal Sender As Object, ByVal e As EventArgs)
        LayOutControls(Sender)
    End Sub

    Private Sub OnMoved(ByVal Sender As Object, ByVal e As EventArgs)
        LayOutControls(Sender)
    End Sub

    Private Sub OnSelectionChanged(ByVal Sender As Object, ByVal e As EventArgs)

        Dim selected_pv As PathwayEvent = CType(Sender, PathwayEvent)

        Debug.Assert(selected_pv.Selected)

        If Not selected_pv Is mPathwayEvent Then
            If Not mPathwayEvent Is Nothing Then
                mPathwayEvent.Selected = False
            End If
        End If

        mPathwayEvent = selected_pv
        tabProperties.Show()
        Dim state As StateMachineType = StateMachineType.Not_Set

        Select Case mPathwayEvent.DefaultStateMachineType
            Case StateMachineType.ActiveAborted, StateMachineType.InitialAborted, StateMachineType.Completed, StateMachineType.Scheduled
                tabProperties.Hide()

            Case StateMachineType.InitialSuspended
                state = StateMachineType.ActiveSuspended
                cbSuspended.Hide()
                cbAborted.Visible = PanelAbortInitial.Controls.Count > 0

            Case StateMachineType.ActiveSuspended
                state = StateMachineType.InitialSuspended
                cbSuspended.Hide()
                cbAborted.Visible = PanelAbortActive.Controls.Count > 0

            Case StateMachineType.Active
                state = StateMachineType.Planned
                cbAborted.Visible = PanelAbortActive.Controls.Count > 0
                cbSuspended.Visible = PanelSuspendActive.Controls.Count > 0

            Case StateMachineType.Planned
                state = StateMachineType.Active
                cbAborted.Visible = PanelAbortInitial.Controls.Count > 0
                cbSuspended.Visible = PanelSuspendInitial.Controls.Count > 0
        End Select

        If state <> StateMachineType.Not_Set Then
            cbAlternativeState.Text = Filemanager.GetOpenEhrTerm(state, state.ToString)
            cbAlternativeState.Tag = state
            cbAlternativeState.Show()
        End If

        mIsloading = True
        cbAlternativeState.Checked = mPathwayEvent.Item.HasAlternativeState
        cbSuspended.Checked = mPathwayEvent.Item.SuspendAllowed
        cbAborted.Checked = mPathwayEvent.Item.AbortAllowed
        mIsloading = False
    End Sub

    Private Sub MenuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAdd.Click, PanelAbortActive.DoubleClick, PanelAbortInitial.DoubleClick, PanelActive.DoubleClick, PanelPlanned.DoubleClick, PanelCompleted.DoubleClick, PanelSuspendInitial.DoubleClick, PanelSuspendActive.DoubleClick, PanelScheduled.DoubleClick
        Dim ctrl As Control = Nothing

        If TypeOf sender Is Control Then
            ctrl = sender
        ElseIf TypeOf sender Is MenuItem Then
            ctrl = CType(sender, MenuItem).GetContextMenu.SourceControl
        End If

        If ctrl Is Nothing Then
            ctrl = mPathwayEvent
        End If

        If TypeOf ctrl Is PathwayEvent Then
            ctrl = ctrl.Parent
        End If

        If ctrl Is PanelAbortActive Then
            AddMachineState(ctrl, StateMachineType.ActiveAborted)
        ElseIf ctrl Is PanelAbortInitial Then
            AddMachineState(ctrl, StateMachineType.InitialAborted)
        ElseIf ctrl Is PanelActive Then
            AddMachineState(ctrl, StateMachineType.Active)
        ElseIf ctrl Is PanelPlanned Then
            AddMachineState(ctrl, StateMachineType.Planned)
        ElseIf ctrl Is PanelCompleted Then
            AddMachineState(ctrl, StateMachineType.Completed)
        ElseIf ctrl Is PanelSuspendInitial Then
            AddMachineState(ctrl, StateMachineType.InitialSuspended)
        ElseIf ctrl Is PanelSuspendActive Then
            AddMachineState(ctrl, StateMachineType.ActiveSuspended)
        ElseIf ctrl Is PanelScheduled Then
            AddMachineState(ctrl, StateMachineType.Scheduled)
        Else
            Debug.Assert(False)
            Beep()
        End If
    End Sub

    Private Sub AddMachineState(ByVal ctrl As Control, ByVal defaultMachineStateType As StateMachineType)
        Dim pv As PathwayEvent = New PathwayEvent(defaultMachineStateType, mFileManager)
        pv.Edit()

        If pv.LastEditWasOk Then
            AddPathwayEvent(ctrl, pv)
            pv.Focus()
        End If
    End Sub

    Private Sub AddPathwayEvent(ByVal ctrl As Control, ByVal pv As PathwayEvent)
        ctrl.Controls.Add(pv)
        pv.TabStop = True
        pv.ContextMenuPathwayEvent.MergeMenu(ContextMenuState)
        AddHandler pv.SelectionChanged, AddressOf OnSelectionChanged
        AddHandler pv.Deleted, AddressOf OnDeleted
        AddHandler pv.Moved, AddressOf OnMoved
        LayOutControls(ctrl)
    End Sub

    Private Sub cbAlternativeState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAlternativeState.CheckedChanged
        If Not mIsloading AndAlso Not mPathwayEvent Is Nothing Then
            If cbAlternativeState.Checked Then
                mPathwayEvent.AlternativeState = CType(cbAlternativeState.Tag, StateMachineType)
            Else
                mPathwayEvent.AlternativeState = StateMachineType.Not_Set
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbSuspended_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSuspended.CheckedChanged
        If Not mIsloading AndAlso Not mPathwayEvent Is Nothing Then
            mPathwayEvent.Item.SuspendAllowed = cbSuspended.Checked
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbAborted_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAborted.CheckedChanged
        If Not mIsloading AndAlso Not mPathwayEvent Is Nothing Then
            mPathwayEvent.Item.AbortAllowed = cbAborted.Checked
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub Splitter_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitterBottom.SplitterMoved, Splitter1.SplitterMoved, Splitter2.SplitterMoved
        PathwaySpecification_Resize(sender, e)
    End Sub

    Private Sub PathwaySpecification_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pbActiveBottomDown.Left = gbActive.Left + Int(gbActive.Width / 2)
        pbActiveTopUp.Left = gbActive.Left + Int(gbActive.Width / 2) - 8
        pbActiveTopDown.Left = gbActive.Left + Int(gbActive.Width / 2) + 8
        pbActiveToCompleted.Top = gbActive.Top + Int(gbActive.Height / 2)
        pbPlannedToActive.Top = gbActive.Top + Int(gbActive.Height / 2)
        gbSuspendedInitial.Width = gbPlanned.Width
        gbAbortedInitial.Width = gbPlanned.Width
        PanelRightTop.Height = gbSuspended.Height + panelTopSpacer.Height
    End Sub

    Private Sub TranslatePathwayEvents(ByVal Ctrl As Control)
        For Each c As Control In Ctrl.Controls
            If TypeOf c Is PathwayEvent Then
                CType(c, PathwayEvent).Translate()
            End If
        Next
    End Sub

    Public Sub TranslateGUI()
        gbPlanned.Text = Filemanager.GetOpenEhrTerm(526, "Planned")
        gbSuspendedInitial.Text = Filemanager.GetOpenEhrTerm(527, "Postponed")
        gbAbortedInitial.Text = Filemanager.GetOpenEhrTerm(528, "Cancelled")
        gbScheduled.Text = Filemanager.GetOpenEhrTerm(529, "Scheduled")
        gbActive.Text = Filemanager.GetOpenEhrTerm(245, "Active")
        gbSuspendedActive.Text = Filemanager.GetOpenEhrTerm(530, "Suspended")
        gbAbortedActive.Text = Filemanager.GetOpenEhrTerm(547, "Abort")
        gbCompleted.Text = Filemanager.GetOpenEhrTerm(532, "Completed")
        lblAllowTransition.Text = Filemanager.GetOpenEhrTerm(677, "Allow transition to")
        cbSuspended.Text = Filemanager.GetOpenEhrTerm(530, "Suspended")
        cbAborted.Text = Filemanager.GetOpenEhrTerm(547, "Abort")
        lblAllowAltState.Text = Filemanager.GetOpenEhrTerm(678, "Allow alternative state")
        cbAlternativeState.Text = Filemanager.GetOpenEhrTerm(679, "Alternative state")
        tpTransition.Text = Filemanager.GetOpenEhrTerm(676, "Transition")
        tpState.Text = Filemanager.GetOpenEhrTerm(177, "State")
    End Sub

    Public Sub Translate()
        TranslatePathwayEvents(PanelPlanned)
        TranslatePathwayEvents(PanelSuspendInitial)
        TranslatePathwayEvents(PanelAbortInitial)
        TranslatePathwayEvents(PanelScheduled)
        TranslatePathwayEvents(PanelActive)
        TranslatePathwayEvents(PanelSuspendActive)
        TranslatePathwayEvents(PanelAbortActive)
        TranslatePathwayEvents(PanelCompleted)
    End Sub

    Private Sub PathwaySpecification_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mPathwayEvent.KeyDown
        If mPathwayEvent.Selected Then
            If e.KeyCode = Keys.Delete Then
                If MessageBox.Show(AE_Constants.Instance.Remove & mPathwayEvent.PathwayEventText, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    mPathwayEvent.Remove()
                End If
            ElseIf e.KeyCode = Keys.Left And e.Modifiers = Keys.Control Then
                mPathwayEvent.Moveby(-1)
            ElseIf e.KeyCode = Keys.Right And e.Modifiers = Keys.Control Then
                mPathwayEvent.Moveby(1)
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
'The Original Code is PathwaySpecification.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
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
