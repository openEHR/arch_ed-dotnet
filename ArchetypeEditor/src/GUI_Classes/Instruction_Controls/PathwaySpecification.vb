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

Public Class PathwaySpecification
    Inherits System.Windows.Forms.UserControl


    Private mValidStateMachineClasses As StateMachineType()
    Private WithEvents mPathwayEvent As PathwayEvent
    Private mIsloading As Boolean
    Private mTabPages As New Collection
    Public Event StructureChanged(ByVal sender As Object, ByVal a_structure As StructureType)


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
    Friend WithEvents gbInitial As System.Windows.Forms.GroupBox
    Friend WithEvents PanelInitial As System.Windows.Forms.Panel
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
    Friend WithEvents pbInitialTopUp As System.Windows.Forms.PictureBox
    Friend WithEvents pbInitialTopDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbActiveBottomDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbInitialBottomDown As System.Windows.Forms.PictureBox
    Friend WithEvents pbInitialToActive As System.Windows.Forms.PictureBox
    Friend WithEvents pbActiveToCompleted As System.Windows.Forms.PictureBox
    Friend WithEvents PanelCompletedSpacer As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tpTransition As System.Windows.Forms.TabPage
    Friend WithEvents tpState As System.Windows.Forms.TabPage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(PathwaySpecification))
        Me.PanelRight = New System.Windows.Forms.Panel
        Me.gbCompleted = New System.Windows.Forms.GroupBox
        Me.PanelCompleted = New System.Windows.Forms.Panel
        Me.ContextMenuState = New System.Windows.Forms.ContextMenu
        Me.MenuAdd = New System.Windows.Forms.MenuItem
        Me.PanelRightBottom = New System.Windows.Forms.Panel
        Me.tabProperties = New System.Windows.Forms.TabControl
        Me.tpTransition = New System.Windows.Forms.TabPage
        Me.cbAborted = New System.Windows.Forms.CheckBox
        Me.cbSuspended = New System.Windows.Forms.CheckBox
        Me.tpState = New System.Windows.Forms.TabPage
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
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelLeft = New System.Windows.Forms.Panel
        Me.gbInitial = New System.Windows.Forms.GroupBox
        Me.PanelInitial = New System.Windows.Forms.Panel
        Me.PanelSpacer = New System.Windows.Forms.Panel
        Me.pbInitialToActive = New System.Windows.Forms.PictureBox
        Me.PanelCompletedSpacer = New System.Windows.Forms.Panel
        Me.pbActiveToCompleted = New System.Windows.Forms.PictureBox
        Me.panelBottomSpacer = New System.Windows.Forms.Panel
        Me.pbInitialBottomDown = New System.Windows.Forms.PictureBox
        Me.pbActiveBottomDown = New System.Windows.Forms.PictureBox
        Me.panelTopSpacer = New System.Windows.Forms.Panel
        Me.pbInitialTopDown = New System.Windows.Forms.PictureBox
        Me.pbInitialTopUp = New System.Windows.Forms.PictureBox
        Me.pbActiveTopDown = New System.Windows.Forms.PictureBox
        Me.pbActiveTopUp = New System.Windows.Forms.PictureBox
        Me.SplitterTop = New System.Windows.Forms.Splitter
        Me.SplitterBottom = New System.Windows.Forms.Splitter
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.PanelLeft.SuspendLayout()
        Me.gbInitial.SuspendLayout()
        Me.PanelSpacer.SuspendLayout()
        Me.PanelCompletedSpacer.SuspendLayout()
        Me.panelBottomSpacer.SuspendLayout()
        Me.panelTopSpacer.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelRight
        '
        Me.PanelRight.Controls.Add(Me.gbCompleted)
        Me.PanelRight.Controls.Add(Me.PanelRightBottom)
        Me.PanelRight.Controls.Add(Me.PanelRightTop)
        Me.PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelRight.Location = New System.Drawing.Point(768, 0)
        Me.PanelRight.Name = "PanelRight"
        Me.PanelRight.Size = New System.Drawing.Size(200, 616)
        Me.PanelRight.TabIndex = 3
        '
        'gbCompleted
        '
        Me.gbCompleted.BackColor = System.Drawing.Color.LightYellow
        Me.gbCompleted.Controls.Add(Me.PanelCompleted)
        Me.gbCompleted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCompleted.Location = New System.Drawing.Point(0, 152)
        Me.gbCompleted.Name = "gbCompleted"
        Me.gbCompleted.Size = New System.Drawing.Size(200, 316)
        Me.gbCompleted.TabIndex = 0
        Me.gbCompleted.TabStop = False
        Me.gbCompleted.Text = "Completed"
        '
        'PanelCompleted
        '
        Me.PanelCompleted.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelCompleted.ContextMenu = Me.ContextMenuState
        Me.PanelCompleted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelCompleted.Location = New System.Drawing.Point(3, 16)
        Me.PanelCompleted.Name = "PanelCompleted"
        Me.PanelCompleted.Size = New System.Drawing.Size(194, 297)
        Me.PanelCompleted.TabIndex = 0
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
        Me.PanelRightBottom.Size = New System.Drawing.Size(200, 148)
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
        Me.tabProperties.Size = New System.Drawing.Size(200, 148)
        Me.tabProperties.TabIndex = 0
        Me.tabProperties.Visible = False
        '
        'tpTransition
        '
        Me.tpTransition.Controls.Add(Me.cbAborted)
        Me.tpTransition.Controls.Add(Me.cbSuspended)
        Me.tpTransition.Controls.Add(Me.Label2)
        Me.tpTransition.Location = New System.Drawing.Point(4, 22)
        Me.tpTransition.Name = "tpTransition"
        Me.tpTransition.Size = New System.Drawing.Size(192, 122)
        Me.tpTransition.TabIndex = 0
        Me.tpTransition.Text = "Transition"
        '
        'cbAborted
        '
        Me.cbAborted.Location = New System.Drawing.Point(24, 56)
        Me.cbAborted.Name = "cbAborted"
        Me.cbAborted.Size = New System.Drawing.Size(192, 24)
        Me.cbAborted.TabIndex = 2
        Me.cbAborted.Text = "Aborted"
        Me.cbAborted.Visible = False
        '
        'cbSuspended
        '
        Me.cbSuspended.Location = New System.Drawing.Point(24, 24)
        Me.cbSuspended.Name = "cbSuspended"
        Me.cbSuspended.Size = New System.Drawing.Size(192, 32)
        Me.cbSuspended.TabIndex = 1
        Me.cbSuspended.Text = "Suspended"
        Me.cbSuspended.Visible = False
        '
        'tpState
        '
        Me.tpState.Controls.Add(Me.Label1)
        Me.tpState.Controls.Add(Me.cbAlternativeState)
        Me.tpState.Location = New System.Drawing.Point(4, 22)
        Me.tpState.Name = "tpState"
        Me.tpState.Size = New System.Drawing.Size(192, 122)
        Me.tpState.TabIndex = 1
        Me.tpState.Text = "State"
        Me.tpState.Visible = False
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
        Me.PanelRightTop.Size = New System.Drawing.Size(200, 152)
        Me.PanelRightTop.TabIndex = 1
        '
        'SplitterRight
        '
        Me.SplitterRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.SplitterRight.Location = New System.Drawing.Point(765, 0)
        Me.SplitterRight.Name = "SplitterRight"
        Me.SplitterRight.Size = New System.Drawing.Size(3, 616)
        Me.SplitterRight.TabIndex = 4
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
        Me.gbSuspended.Size = New System.Drawing.Size(765, 128)
        Me.gbSuspended.TabIndex = 5
        Me.gbSuspended.TabStop = False
        Me.gbSuspended.Text = "Suspended"
        '
        'gbSuspendedActive
        '
        Me.gbSuspendedActive.Controls.Add(Me.PanelSuspendActive)
        Me.gbSuspendedActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSuspendedActive.Location = New System.Drawing.Point(152, 16)
        Me.gbSuspendedActive.Name = "gbSuspendedActive"
        Me.gbSuspendedActive.Size = New System.Drawing.Size(610, 109)
        Me.gbSuspendedActive.TabIndex = 4
        Me.gbSuspendedActive.TabStop = False
        Me.gbSuspendedActive.Text = "Suspended active"
        '
        'PanelSuspendActive
        '
        Me.PanelSuspendActive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSuspendActive.ContextMenu = Me.ContextMenuState
        Me.PanelSuspendActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSuspendActive.Location = New System.Drawing.Point(3, 16)
        Me.PanelSuspendActive.Name = "PanelSuspendActive"
        Me.PanelSuspendActive.Size = New System.Drawing.Size(604, 90)
        Me.PanelSuspendActive.TabIndex = 1
        '
        'gbTop
        '
        Me.gbTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbTop.Location = New System.Drawing.Point(128, 16)
        Me.gbTop.Name = "gbTop"
        Me.gbTop.Size = New System.Drawing.Size(24, 109)
        Me.gbTop.TabIndex = 3
        Me.gbTop.TabStop = False
        '
        'gbSuspendedInitial
        '
        Me.gbSuspendedInitial.Controls.Add(Me.PanelSuspendInitial)
        Me.gbSuspendedInitial.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbSuspendedInitial.Location = New System.Drawing.Point(3, 16)
        Me.gbSuspendedInitial.Name = "gbSuspendedInitial"
        Me.gbSuspendedInitial.Size = New System.Drawing.Size(125, 109)
        Me.gbSuspendedInitial.TabIndex = 2
        Me.gbSuspendedInitial.TabStop = False
        Me.gbSuspendedInitial.Text = "Suspended initial"
        '
        'PanelSuspendInitial
        '
        Me.PanelSuspendInitial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSuspendInitial.ContextMenu = Me.ContextMenuState
        Me.PanelSuspendInitial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSuspendInitial.Location = New System.Drawing.Point(3, 16)
        Me.PanelSuspendInitial.Name = "PanelSuspendInitial"
        Me.PanelSuspendInitial.Size = New System.Drawing.Size(119, 90)
        Me.PanelSuspendInitial.TabIndex = 0
        '
        'gbAborted
        '
        Me.gbAborted.Controls.Add(Me.gbAbortedActive)
        Me.gbAborted.Controls.Add(Me.gbBottom)
        Me.gbAborted.Controls.Add(Me.gbAbortedInitial)
        Me.gbAborted.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbAborted.Location = New System.Drawing.Point(0, 488)
        Me.gbAborted.Name = "gbAborted"
        Me.gbAborted.Size = New System.Drawing.Size(765, 128)
        Me.gbAborted.TabIndex = 7
        Me.gbAborted.TabStop = False
        Me.gbAborted.Text = "Aborted"
        '
        'gbAbortedActive
        '
        Me.gbAbortedActive.Controls.Add(Me.PanelAbortActive)
        Me.gbAbortedActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbAbortedActive.Location = New System.Drawing.Point(157, 16)
        Me.gbAbortedActive.Name = "gbAbortedActive"
        Me.gbAbortedActive.Size = New System.Drawing.Size(605, 109)
        Me.gbAbortedActive.TabIndex = 4
        Me.gbAbortedActive.TabStop = False
        Me.gbAbortedActive.Text = "Aborted active"
        '
        'PanelAbortActive
        '
        Me.PanelAbortActive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelAbortActive.ContextMenu = Me.ContextMenuState
        Me.PanelAbortActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelAbortActive.Location = New System.Drawing.Point(3, 16)
        Me.PanelAbortActive.Name = "PanelAbortActive"
        Me.PanelAbortActive.Size = New System.Drawing.Size(599, 90)
        Me.PanelAbortActive.TabIndex = 1
        '
        'gbBottom
        '
        Me.gbBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbBottom.Location = New System.Drawing.Point(128, 16)
        Me.gbBottom.Name = "gbBottom"
        Me.gbBottom.Size = New System.Drawing.Size(29, 109)
        Me.gbBottom.TabIndex = 3
        Me.gbBottom.TabStop = False
        '
        'gbAbortedInitial
        '
        Me.gbAbortedInitial.Controls.Add(Me.PanelAbortInitial)
        Me.gbAbortedInitial.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbAbortedInitial.Location = New System.Drawing.Point(3, 16)
        Me.gbAbortedInitial.Name = "gbAbortedInitial"
        Me.gbAbortedInitial.Size = New System.Drawing.Size(125, 109)
        Me.gbAbortedInitial.TabIndex = 2
        Me.gbAbortedInitial.TabStop = False
        Me.gbAbortedInitial.Text = "Aborted initial"
        '
        'PanelAbortInitial
        '
        Me.PanelAbortInitial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelAbortInitial.ContextMenu = Me.ContextMenuState
        Me.PanelAbortInitial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelAbortInitial.Location = New System.Drawing.Point(3, 16)
        Me.PanelAbortInitial.Name = "PanelAbortInitial"
        Me.PanelAbortInitial.Size = New System.Drawing.Size(119, 90)
        Me.PanelAbortInitial.TabIndex = 0
        '
        'PanelMiddle
        '
        Me.PanelMiddle.Controls.Add(Me.gbActive)
        Me.PanelMiddle.Controls.Add(Me.Splitter1)
        Me.PanelMiddle.Controls.Add(Me.PanelLeft)
        Me.PanelMiddle.Controls.Add(Me.PanelCompletedSpacer)
        Me.PanelMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMiddle.Location = New System.Drawing.Point(0, 151)
        Me.PanelMiddle.Name = "PanelMiddle"
        Me.PanelMiddle.Size = New System.Drawing.Size(765, 314)
        Me.PanelMiddle.TabIndex = 8
        '
        'gbActive
        '
        Me.gbActive.BackColor = System.Drawing.Color.LightYellow
        Me.gbActive.Controls.Add(Me.PanelActive)
        Me.gbActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbActive.Location = New System.Drawing.Point(163, 0)
        Me.gbActive.Name = "gbActive"
        Me.gbActive.Size = New System.Drawing.Size(578, 314)
        Me.gbActive.TabIndex = 2
        Me.gbActive.TabStop = False
        Me.gbActive.Text = "Active"
        '
        'PanelActive
        '
        Me.PanelActive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelActive.ContextMenu = Me.ContextMenuState
        Me.PanelActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelActive.Location = New System.Drawing.Point(3, 16)
        Me.PanelActive.Name = "PanelActive"
        Me.PanelActive.Size = New System.Drawing.Size(572, 295)
        Me.PanelActive.TabIndex = 0
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
        Me.PanelLeft.Controls.Add(Me.gbInitial)
        Me.PanelLeft.Controls.Add(Me.PanelSpacer)
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelLeft.Location = New System.Drawing.Point(0, 0)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(160, 314)
        Me.PanelLeft.TabIndex = 6
        '
        'gbInitial
        '
        Me.gbInitial.BackColor = System.Drawing.Color.LightYellow
        Me.gbInitial.Controls.Add(Me.PanelInitial)
        Me.gbInitial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbInitial.Location = New System.Drawing.Point(0, 0)
        Me.gbInitial.Name = "gbInitial"
        Me.gbInitial.Size = New System.Drawing.Size(136, 314)
        Me.gbInitial.TabIndex = 0
        Me.gbInitial.TabStop = False
        Me.gbInitial.Text = "Initial"
        '
        'PanelInitial
        '
        Me.PanelInitial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelInitial.ContextMenu = Me.ContextMenuState
        Me.PanelInitial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelInitial.Location = New System.Drawing.Point(3, 16)
        Me.PanelInitial.Name = "PanelInitial"
        Me.PanelInitial.Size = New System.Drawing.Size(130, 295)
        Me.PanelInitial.TabIndex = 0
        '
        'PanelSpacer
        '
        Me.PanelSpacer.BackColor = System.Drawing.Color.LightYellow
        Me.PanelSpacer.Controls.Add(Me.pbInitialToActive)
        Me.PanelSpacer.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSpacer.Location = New System.Drawing.Point(136, 0)
        Me.PanelSpacer.Name = "PanelSpacer"
        Me.PanelSpacer.Size = New System.Drawing.Size(24, 314)
        Me.PanelSpacer.TabIndex = 3
        '
        'pbInitialToActive
        '
        Me.pbInitialToActive.Image = CType(resources.GetObject("pbInitialToActive.Image"), System.Drawing.Image)
        Me.pbInitialToActive.Location = New System.Drawing.Point(5, 144)
        Me.pbInitialToActive.Name = "pbInitialToActive"
        Me.pbInitialToActive.Size = New System.Drawing.Size(16, 16)
        Me.pbInitialToActive.TabIndex = 5
        Me.pbInitialToActive.TabStop = False
        '
        'PanelCompletedSpacer
        '
        Me.PanelCompletedSpacer.BackColor = System.Drawing.Color.LightYellow
        Me.PanelCompletedSpacer.Controls.Add(Me.pbActiveToCompleted)
        Me.PanelCompletedSpacer.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelCompletedSpacer.Location = New System.Drawing.Point(741, 0)
        Me.PanelCompletedSpacer.Name = "PanelCompletedSpacer"
        Me.PanelCompletedSpacer.Size = New System.Drawing.Size(24, 314)
        Me.PanelCompletedSpacer.TabIndex = 5
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
        Me.panelBottomSpacer.Controls.Add(Me.pbInitialBottomDown)
        Me.panelBottomSpacer.Controls.Add(Me.pbActiveBottomDown)
        Me.panelBottomSpacer.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBottomSpacer.Location = New System.Drawing.Point(0, 468)
        Me.panelBottomSpacer.Name = "panelBottomSpacer"
        Me.panelBottomSpacer.Size = New System.Drawing.Size(765, 20)
        Me.panelBottomSpacer.TabIndex = 9
        '
        'pbInitialBottomDown
        '
        Me.pbInitialBottomDown.Image = CType(resources.GetObject("pbInitialBottomDown.Image"), System.Drawing.Image)
        Me.pbInitialBottomDown.Location = New System.Drawing.Point(56, 2)
        Me.pbInitialBottomDown.Name = "pbInitialBottomDown"
        Me.pbInitialBottomDown.Size = New System.Drawing.Size(16, 16)
        Me.pbInitialBottomDown.TabIndex = 5
        Me.pbInitialBottomDown.TabStop = False
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
        Me.panelTopSpacer.Controls.Add(Me.pbInitialTopDown)
        Me.panelTopSpacer.Controls.Add(Me.pbInitialTopUp)
        Me.panelTopSpacer.Controls.Add(Me.pbActiveTopDown)
        Me.panelTopSpacer.Controls.Add(Me.pbActiveTopUp)
        Me.panelTopSpacer.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTopSpacer.Location = New System.Drawing.Point(0, 128)
        Me.panelTopSpacer.Name = "panelTopSpacer"
        Me.panelTopSpacer.Size = New System.Drawing.Size(765, 20)
        Me.panelTopSpacer.TabIndex = 10
        '
        'pbInitialTopDown
        '
        Me.pbInitialTopDown.Image = CType(resources.GetObject("pbInitialTopDown.Image"), System.Drawing.Image)
        Me.pbInitialTopDown.Location = New System.Drawing.Point(56, 2)
        Me.pbInitialTopDown.Name = "pbInitialTopDown"
        Me.pbInitialTopDown.Size = New System.Drawing.Size(16, 16)
        Me.pbInitialTopDown.TabIndex = 3
        Me.pbInitialTopDown.TabStop = False
        '
        'pbInitialTopUp
        '
        Me.pbInitialTopUp.Image = CType(resources.GetObject("pbInitialTopUp.Image"), System.Drawing.Image)
        Me.pbInitialTopUp.Location = New System.Drawing.Point(40, 2)
        Me.pbInitialTopUp.Name = "pbInitialTopUp"
        Me.pbInitialTopUp.Size = New System.Drawing.Size(16, 16)
        Me.pbInitialTopUp.TabIndex = 2
        Me.pbInitialTopUp.TabStop = False
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
        Me.SplitterTop.Size = New System.Drawing.Size(765, 3)
        Me.SplitterTop.TabIndex = 11
        Me.SplitterTop.TabStop = False
        '
        'SplitterBottom
        '
        Me.SplitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitterBottom.Location = New System.Drawing.Point(0, 465)
        Me.SplitterBottom.Name = "SplitterBottom"
        Me.SplitterBottom.Size = New System.Drawing.Size(765, 3)
        Me.SplitterBottom.TabIndex = 12
        Me.SplitterBottom.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Allow transition to:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 32)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Allow alternative state for this event."
        '
        'PathwaySpecification
        '
        Me.BackColor = System.Drawing.Color.LemonChiffon
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
        Me.PanelLeft.ResumeLayout(False)
        Me.gbInitial.ResumeLayout(False)
        Me.PanelSpacer.ResumeLayout(False)
        Me.PanelCompletedSpacer.ResumeLayout(False)
        Me.panelBottomSpacer.ResumeLayout(False)
        Me.panelTopSpacer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub PathwaySpecification_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mValidStateMachineClasses = ReferenceModel.Instance.ValidStateMachineTypes
        Me.PathwaySpecification_Resize(sender, e)
        'hold the tabpages in a collection so can remove them from the Tab as required
        mTabPages.Add(Me.tpTransition, tpTransition.Name)
        mTabPages.Add(Me.tpState, tpState.Name)

    End Sub

    Property PathwaySteps() As RmStructureCompound
        Get
            Return BuildPathwaySteps()
        End Get
        Set(ByVal Value As RmStructureCompound)
            Dim Ctrl As Panel

            Debug.Assert(Value.Type = StructureType.InstructionActExection)
            For Each ms As RmPathwayStep In Value.Children
                Dim pv As New PathwayEvent(ms)
                Select Case pv.DefaultStateMachineType
                    Case StateMachineType.ActiveAborted
                        Ctrl = Me.PanelAbortActive
                    Case StateMachineType.Active
                        Ctrl = Me.PanelActive
                    Case StateMachineType.ActiveSuspended
                        Ctrl = Me.PanelSuspendActive
                    Case StateMachineType.Initial
                        Ctrl = Me.PanelInitial
                    Case StateMachineType.InitialAborted
                        Ctrl = Me.PanelAbortInitial
                    Case StateMachineType.InitialSuspended
                        Ctrl = Me.PanelSuspendInitial
                    Case StateMachineType.Completed
                        Ctrl = Me.PanelCompleted
                End Select
                Ctrl.Controls.Add(pv)
                pv.ContextMenuPathwayEvent.MergeMenu(Me.ContextMenuState)
                AddHandler pv.SelectionChanged, AddressOf OnSelectionChanged
                AddHandler pv.Deleted, AddressOf OnDeleted
                LayOutControls(Ctrl)
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

        If i = 0 Then Return

        total_width = PathwayEvent.DefaultWidth * i

        If total_width > Ctrl.Width Then
            scalefactor = (Ctrl.Width - (spacer * i) - left_margin) / total_width
        Else
            scalefactor = 1
        End If
        For i = 0 To Ctrl.Controls.Count - 1
            path_event = CType(Ctrl.Controls(i), PathwayEvent)
            path_event.Width = (path_event.DefaultWidth * scalefactor)
            path_event.Height = Ctrl.Height - 6
            path_event.Top = 3
            path_event.Left = left_margin
            left_margin = path_event.Left + path_event.Width + spacer
        Next

    End Sub

    'Private Sub gbInitial_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles gbInitial.Resize
    '    Me.PanelAbortInitial.Width = gbInitial.Width
    '    Me.PanelSuspendInitial.Width = gbInitial.Width
    'End Sub

    'Private Sub gbSuspended_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles gbSuspended.Resize
    '    Me.PanelRightTop.Height = gbSuspended.Height
    'End Sub


    'Private Sub gbAborted_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles gbAborted.Resize
    '    Me.PanelRightBottom.Height = gbAborted.Height
    'End Sub

    Private Sub Panel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelActive.Resize, PanelAbortActive.Resize, PanelAbortInitial.Resize, PanelInitial.Resize, PanelSuspendInitial.Resize, PanelCompleted.Resize, PanelSuspendActive.Resize
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
        Dim rm As New RmStructureCompound("InstructionActExection", StructureType.InstructionActExection)

        GetPathwaySteps(rm.Children, Me.PanelInitial)
        GetPathwaySteps(rm.Children, Me.PanelSuspendInitial)
        GetPathwaySteps(rm.Children, Me.PanelAbortInitial)
        GetPathwaySteps(rm.Children, Me.PanelActive)
        GetPathwaySteps(rm.Children, Me.PanelSuspendActive)
        GetPathwaySteps(rm.Children, Me.PanelAbortActive)
        GetPathwaySteps(rm.Children, Me.PanelCompleted)

        Return rm
    End Function

    Private Sub OnDeleted(ByVal Sender As Object, ByVal e As EventArgs)
        LayOutControls(Sender)
    End Sub

    Private Sub OnSelectionChanged(ByVal Sender As Object, ByVal e As EventArgs)

        Dim selected_pv As PathwayEvent = CType(Sender, PathwayEvent)

        Me.tabProperties.Visible = True

        Debug.Assert(selected_pv.Selected)

        If Not selected_pv Is mPathwayEvent Then
            If Not mPathwayEvent Is Nothing Then
                mPathwayEvent.Selected = False
            End If
        End If

        mPathwayEvent = selected_pv

        Select Case mPathwayEvent.DefaultStateMachineType
            Case StateMachineType.ActiveAborted, StateMachineType.InitialAborted, StateMachineType.Completed
                If Me.tabProperties.Contains(tpTransition) Then
                    Me.tabProperties.TabPages.Remove(tpTransition)
                End If
                If Me.tabProperties.Contains(tpState) Then
                    Me.tabProperties.TabPages.Remove(tpState)
                End If

            Case StateMachineType.InitialSuspended
                If Not Me.tabProperties.Contains(tpTransition) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpTransition.Name), TabPage))
                End If
                If Not Me.tabProperties.Contains(tpState) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpState.Name), TabPage))
                End If
                cbAlternativeState.Text = StateMachineType.ActiveSuspended.ToString
                cbAlternativeState.Tag = StateMachineType.ActiveSuspended
                Me.cbAlternativeState.Visible = True
                Me.cbSuspended.Visible = False
                If Me.PanelAbortInitial.Controls.Count > 0 Then
                    Me.cbAborted.Visible = True
                Else
                    Me.cbAborted.Visible = False
                End If
            Case StateMachineType.ActiveSuspended
                If Not Me.tabProperties.Contains(tpTransition) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpTransition.Name), TabPage))
                End If
                If Not Me.tabProperties.Contains(tpState) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpState.Name), TabPage))
                End If
                cbAlternativeState.Text = StateMachineType.InitialSuspended.ToString
                cbAlternativeState.Tag = StateMachineType.InitialSuspended
                Me.cbAlternativeState.Visible = True
                Me.cbSuspended.Visible = False
                '? allow transitions to aborted
                If Me.PanelAbortActive.Controls.Count > 0 Then
                    Me.cbAborted.Visible = True
                Else
                    Me.cbAborted.Visible = False
                End If
            Case StateMachineType.Active
                If Not Me.tabProperties.Contains(tpTransition) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpTransition.Name), TabPage))
                End If
                If Not Me.tabProperties.Contains(tpState) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpState.Name), TabPage))
                End If
                cbAlternativeState.Text = StateMachineType.Initial.ToString
                cbAlternativeState.Tag = StateMachineType.Initial
                Me.cbAlternativeState.Visible = True
                If Me.PanelAbortActive.Controls.Count > 0 Then
                    Me.cbAborted.Visible = True
                Else
                    Me.cbAborted.Visible = False
                End If
                If Me.PanelSuspendActive.Controls.Count > 0 Then
                    Me.cbSuspended.Visible = True
                Else
                    Me.cbSuspended.Visible = False
                End If

            Case StateMachineType.Initial
                If Not Me.tabProperties.Contains(tpTransition) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpTransition.Name), TabPage))
                End If
                If Not Me.tabProperties.Contains(tpState) Then
                    Me.tabProperties.TabPages.Add(CType(mTabPages(tpState.Name), TabPage))
                End If
                cbAlternativeState.Text = StateMachineType.Active.ToString
                cbAlternativeState.Tag = StateMachineType.Active
                If Me.PanelAbortInitial.Controls.Count > 0 Then
                    Me.cbAborted.Visible = True
                Else
                    Me.cbAborted.Visible = False
                End If
                If Me.PanelSuspendInitial.Controls.Count > 0 Then
                    Me.cbSuspended.Visible = True
                Else
                    Me.cbSuspended.Visible = False
                End If
                Me.cbAlternativeState.Visible = True
        End Select

        mIsloading = True
        cbAlternativeState.Checked = Me.mPathwayEvent.Item.HasAlternativeState
        Me.cbSuspended.Checked = Me.mPathwayEvent.Item.SuspendAllowed
        Me.cbAborted.Checked = Me.mPathwayEvent.Item.AbortAllowed
        mIsloading = False
    End Sub


    Private Sub MenuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAdd.Click

        Dim pv As PathwayEvent
        Dim ctrl As Control

        ctrl = CType(sender, MenuItem).GetContextMenu.SourceControl

        If ctrl Is Nothing Then
            'may not pick up pathway event
            ctrl = mPathwayEvent
        End If

        If TypeOf ctrl Is PathwayEvent Then ' needed as this menu is added to the pathway event's context
            'menu - needed as the grid can fill up
            ' get the parent
            ctrl = ctrl.Parent
        End If

        If ctrl Is Me.PanelAbortActive Then
            pv = New PathwayEvent(StateMachineType.ActiveAborted)
        ElseIf ctrl Is Me.PanelAbortInitial Then
            pv = New PathwayEvent(StateMachineType.InitialAborted)
        ElseIf ctrl Is Me.PanelActive Then
            pv = New PathwayEvent(StateMachineType.Active)
        ElseIf ctrl Is Me.PanelInitial Then
            pv = New PathwayEvent(StateMachineType.Initial)
        ElseIf ctrl Is Me.PanelCompleted Then
            pv = New PathwayEvent(StateMachineType.Completed)
        ElseIf ctrl Is Me.PanelSuspendInitial Then
            pv = New PathwayEvent(StateMachineType.InitialSuspended)
        ElseIf ctrl Is Me.PanelSuspendActive Then
            pv = New PathwayEvent(StateMachineType.ActiveSuspended)
        Else
            Debug.Assert(False)
            Beep()
            Return
        End If
        ctrl.Controls.Add(pv)
        pv.ContextMenuPathwayEvent.MergeMenu(Me.ContextMenuState)
        AddHandler pv.SelectionChanged, AddressOf OnSelectionChanged
        AddHandler pv.Deleted, AddressOf OnDeleted
        pv.Selected = True
        LayOutControls(ctrl)
    End Sub

    Private Sub cbAlternativeState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAlternativeState.CheckedChanged
        If Not mIsloading Then
            If mPathwayEvent Is Nothing Then Return

            If cbAlternativeState.Checked Then
                Me.mPathwayEvent.AlternativeState = CType(Me.cbAlternativeState.Tag, StateMachineType)
            Else
                Me.mPathwayEvent.AlternativeState = StateMachineType.Not_Set
            End If
            Filemanager.Instance.FileEdited = True
        End If
    End Sub

    Private Sub cbSuspended_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSuspended.CheckedChanged
        If Not mIsloading Then
            If mPathwayEvent Is Nothing Then Return

            Me.mPathwayEvent.Item.SuspendAllowed = Me.cbSuspended.Checked
            Filemanager.Instance.FileEdited = True
        End If
    End Sub

    Private Sub cbAborted_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAborted.CheckedChanged
        If Not mIsloading Then
            If mPathwayEvent Is Nothing Then Return

            Me.mPathwayEvent.Item.AbortAllowed = Me.cbAborted.Checked
            Filemanager.Instance.FileEdited = True
        End If
    End Sub

    Private Sub Splitter_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitterBottom.SplitterMoved, Splitter1.SplitterMoved
        Me.PathwaySpecification_Resize(sender, e)
    End Sub

    Private Sub PathwaySpecification_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.pbActiveBottomDown.Left = Me.gbActive.Left + Int(Me.gbActive.Width / 2)
        Me.pbActiveTopUp.Left = Me.gbActive.Left + Int(Me.gbActive.Width / 2) - 8
        Me.pbActiveTopDown.Left = Me.gbActive.Left + Int(Me.gbActive.Width / 2) + 8
        Me.pbActiveToCompleted.Top = Me.gbActive.Top + Int(Me.gbActive.Height / 2)
        Me.pbInitialToActive.Top = Me.gbActive.Top + Int(Me.gbActive.Height / 2)
        Me.gbSuspendedInitial.Width = Me.gbInitial.Width
        Me.gbAbortedInitial.Width = Me.gbInitial.Width
        Me.PanelRightTop.Height = Me.gbSuspended.Height + Me.panelTopSpacer.Height
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