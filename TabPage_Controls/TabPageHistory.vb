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

Public Class TabpageHistory
    Inherits System.Windows.Forms.UserControl

    Private current_item As EventListViewItem
    Private sNodeID As String
    Private MathFunctionTable As DataTable
    Private mIsLoading = False
    Private mFileManager As FileManagerLocal

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
    Friend WithEvents gbEventDetails As System.Windows.Forms.GroupBox
    Friend WithEvents txtEventDescription As System.Windows.Forms.TextBox
    Friend WithEvents butAddEvent As System.Windows.Forms.Button
    Friend WithEvents gbDuration As System.Windows.Forms.GroupBox
    Friend WithEvents comboIntervalViewPoint As System.Windows.Forms.ComboBox
    Friend WithEvents numericDuration As System.Windows.Forms.NumericUpDown
    Friend WithEvents comboDurationUnits As System.Windows.Forms.ComboBox
    Friend WithEvents RadioInterval As System.Windows.Forms.RadioButton
    Friend WithEvents radioPointInTime As System.Windows.Forms.RadioButton
    Friend WithEvents gbOffset As System.Windows.Forms.GroupBox
    Friend WithEvents NumericOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents comboOffsetUnits As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkIsPeriodic As System.Windows.Forms.CheckBox
    Friend WithEvents numPeriod As System.Windows.Forms.NumericUpDown
    Friend WithEvents comboTimeUnits As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radioFixed As System.Windows.Forms.RadioButton
    Friend WithEvents radioOpen As System.Windows.Forms.RadioButton
    Friend WithEvents cbFixedOffset As System.Windows.Forms.CheckBox
    Friend WithEvents cbFixedInterval As System.Windows.Forms.CheckBox
    Friend WithEvents butRemoveElement As System.Windows.Forms.Button

    Friend WithEvents lblNumMax As System.Windows.Forms.Label
    Friend WithEvents lblNumMin As System.Windows.Forms.Label
    Friend WithEvents numMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbUnbounded As System.Windows.Forms.CheckBox
    Friend WithEvents butListUp As System.Windows.Forms.Button
    Friend WithEvents butListDown As System.Windows.Forms.Button
    Friend WithEvents txtRuntimeConstraint As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ListEvents As System.Windows.Forms.ListView
    Friend WithEvents ImageListEvents As System.Windows.Forms.ImageList
    Friend WithEvents TheEvents As System.Windows.Forms.ColumnHeader
    Friend WithEvents buSetRuntimeConstraint As System.Windows.Forms.Button
    Friend WithEvents HelpProviderEventSeries As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabpageHistory))
        Me.gbEventDetails = New System.Windows.Forms.GroupBox
        Me.buSetRuntimeConstraint = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtRuntimeConstraint = New System.Windows.Forms.TextBox
        Me.txtEventDescription = New System.Windows.Forms.TextBox
        Me.gbDuration = New System.Windows.Forms.GroupBox
        Me.cbFixedInterval = New System.Windows.Forms.CheckBox
        Me.comboIntervalViewPoint = New System.Windows.Forms.ComboBox
        Me.numericDuration = New System.Windows.Forms.NumericUpDown
        Me.comboDurationUnits = New System.Windows.Forms.ComboBox
        Me.RadioInterval = New System.Windows.Forms.RadioButton
        Me.radioPointInTime = New System.Windows.Forms.RadioButton
        Me.gbOffset = New System.Windows.Forms.GroupBox
        Me.cbFixedOffset = New System.Windows.Forms.CheckBox
        Me.NumericOffset = New System.Windows.Forms.NumericUpDown
        Me.comboOffsetUnits = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblNumMax = New System.Windows.Forms.Label
        Me.lblNumMin = New System.Windows.Forms.Label
        Me.numMax = New System.Windows.Forms.NumericUpDown
        Me.cbUnbounded = New System.Windows.Forms.CheckBox
        Me.numMin = New System.Windows.Forms.NumericUpDown
        Me.butRemoveElement = New System.Windows.Forms.Button
        Me.butAddEvent = New System.Windows.Forms.Button
        Me.chkIsPeriodic = New System.Windows.Forms.CheckBox
        Me.numPeriod = New System.Windows.Forms.NumericUpDown
        Me.comboTimeUnits = New System.Windows.Forms.ComboBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.butListUp = New System.Windows.Forms.Button
        Me.butListDown = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.radioOpen = New System.Windows.Forms.RadioButton
        Me.radioFixed = New System.Windows.Forms.RadioButton
        Me.ListEvents = New System.Windows.Forms.ListView
        Me.TheEvents = New System.Windows.Forms.ColumnHeader
        Me.ImageListEvents = New System.Windows.Forms.ImageList(Me.components)
        Me.HelpProviderEventSeries = New System.Windows.Forms.HelpProvider
        Me.gbEventDetails.SuspendLayout()
        Me.gbDuration.SuspendLayout()
        CType(Me.numericDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbOffset.SuspendLayout()
        CType(Me.NumericOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbEventDetails
        '
        Me.gbEventDetails.Controls.Add(Me.buSetRuntimeConstraint)
        Me.gbEventDetails.Controls.Add(Me.Label11)
        Me.gbEventDetails.Controls.Add(Me.txtRuntimeConstraint)
        Me.gbEventDetails.Controls.Add(Me.txtEventDescription)
        Me.gbEventDetails.Controls.Add(Me.gbDuration)
        Me.gbEventDetails.Controls.Add(Me.RadioInterval)
        Me.gbEventDetails.Controls.Add(Me.radioPointInTime)
        Me.gbEventDetails.Controls.Add(Me.gbOffset)
        Me.gbEventDetails.Controls.Add(Me.Label14)
        Me.gbEventDetails.Controls.Add(Me.lblNumMax)
        Me.gbEventDetails.Controls.Add(Me.lblNumMin)
        Me.gbEventDetails.Controls.Add(Me.numMax)
        Me.gbEventDetails.Controls.Add(Me.cbUnbounded)
        Me.gbEventDetails.Controls.Add(Me.numMin)
        Me.gbEventDetails.Location = New System.Drawing.Point(361, 8)
        Me.gbEventDetails.Name = "gbEventDetails"
        Me.gbEventDetails.Size = New System.Drawing.Size(376, 316)
        Me.gbEventDetails.TabIndex = 34
        Me.gbEventDetails.TabStop = False
        Me.gbEventDetails.Text = "Event details"
        '
        'buSetRuntimeConstraint
        '
        Me.buSetRuntimeConstraint.Location = New System.Drawing.Point(328, 128)
        Me.buSetRuntimeConstraint.Name = "buSetRuntimeConstraint"
        Me.buSetRuntimeConstraint.Size = New System.Drawing.Size(32, 20)
        Me.buSetRuntimeConstraint.TabIndex = 43
        Me.buSetRuntimeConstraint.Text = "..."
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(4, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(116, 32)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Runtime name constraint:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRuntimeConstraint
        '
        Me.txtRuntimeConstraint.Location = New System.Drawing.Point(128, 128)
        Me.txtRuntimeConstraint.Name = "txtRuntimeConstraint"
        Me.txtRuntimeConstraint.ReadOnly = True
        Me.txtRuntimeConstraint.Size = New System.Drawing.Size(190, 22)
        Me.txtRuntimeConstraint.TabIndex = 5
        Me.txtRuntimeConstraint.Text = ""
        '
        'txtEventDescription
        '
        Me.txtEventDescription.Location = New System.Drawing.Point(16, 66)
        Me.txtEventDescription.Multiline = True
        Me.txtEventDescription.Name = "txtEventDescription"
        Me.txtEventDescription.Size = New System.Drawing.Size(349, 46)
        Me.txtEventDescription.TabIndex = 4
        Me.txtEventDescription.Text = ""
        '
        'gbDuration
        '
        Me.gbDuration.Controls.Add(Me.cbFixedInterval)
        Me.gbDuration.Controls.Add(Me.comboIntervalViewPoint)
        Me.gbDuration.Controls.Add(Me.numericDuration)
        Me.gbDuration.Controls.Add(Me.comboDurationUnits)
        Me.gbDuration.Location = New System.Drawing.Point(178, 200)
        Me.gbDuration.Name = "gbDuration"
        Me.gbDuration.Size = New System.Drawing.Size(190, 104)
        Me.gbDuration.TabIndex = 25
        Me.gbDuration.TabStop = False
        Me.gbDuration.Text = "Duration"
        Me.gbDuration.Visible = False
        '
        'cbFixedInterval
        '
        Me.cbFixedInterval.Location = New System.Drawing.Point(10, 16)
        Me.cbFixedInterval.Name = "cbFixedInterval"
        Me.cbFixedInterval.Size = New System.Drawing.Size(120, 24)
        Me.cbFixedInterval.TabIndex = 11
        Me.cbFixedInterval.Text = "Fixed Interval"
        '
        'comboIntervalViewPoint
        '
        Me.comboIntervalViewPoint.Location = New System.Drawing.Point(10, 43)
        Me.comboIntervalViewPoint.Name = "comboIntervalViewPoint"
        Me.comboIntervalViewPoint.Size = New System.Drawing.Size(174, 24)
        Me.comboIntervalViewPoint.TabIndex = 12
        '
        'numericDuration
        '
        Me.numericDuration.Location = New System.Drawing.Point(10, 69)
        Me.numericDuration.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numericDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericDuration.Name = "numericDuration"
        Me.numericDuration.Size = New System.Drawing.Size(48, 22)
        Me.numericDuration.TabIndex = 13
        Me.numericDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numericDuration.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericDuration.Visible = False
        '
        'comboDurationUnits
        '
        Me.comboDurationUnits.Items.AddRange(New Object() {"Millisec", "sec", "min", "hr", "day", "month", "year"})
        Me.comboDurationUnits.Location = New System.Drawing.Point(66, 69)
        Me.comboDurationUnits.Name = "comboDurationUnits"
        Me.comboDurationUnits.Size = New System.Drawing.Size(56, 24)
        Me.comboDurationUnits.TabIndex = 14
        Me.comboDurationUnits.Text = "min"
        Me.comboDurationUnits.Visible = False
        '
        'RadioInterval
        '
        Me.RadioInterval.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioInterval.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioInterval.Location = New System.Drawing.Point(180, 168)
        Me.RadioInterval.Name = "RadioInterval"
        Me.RadioInterval.Size = New System.Drawing.Size(128, 24)
        Me.RadioInterval.TabIndex = 7
        Me.RadioInterval.Text = "Interval"
        Me.RadioInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'radioPointInTime
        '
        Me.radioPointInTime.Appearance = System.Windows.Forms.Appearance.Button
        Me.radioPointInTime.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.radioPointInTime.Location = New System.Drawing.Point(26, 168)
        Me.radioPointInTime.Name = "radioPointInTime"
        Me.radioPointInTime.Size = New System.Drawing.Size(136, 24)
        Me.radioPointInTime.TabIndex = 6
        Me.radioPointInTime.Text = "Point in time"
        Me.radioPointInTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbOffset
        '
        Me.gbOffset.Controls.Add(Me.cbFixedOffset)
        Me.gbOffset.Controls.Add(Me.NumericOffset)
        Me.gbOffset.Controls.Add(Me.comboOffsetUnits)
        Me.gbOffset.Location = New System.Drawing.Point(26, 200)
        Me.gbOffset.Name = "gbOffset"
        Me.gbOffset.Size = New System.Drawing.Size(136, 72)
        Me.gbOffset.TabIndex = 22
        Me.gbOffset.TabStop = False
        Me.gbOffset.Text = "Offset"
        Me.gbOffset.Visible = False
        '
        'cbFixedOffset
        '
        Me.cbFixedOffset.Location = New System.Drawing.Point(16, 16)
        Me.cbFixedOffset.Name = "cbFixedOffset"
        Me.cbFixedOffset.Size = New System.Drawing.Size(112, 24)
        Me.cbFixedOffset.TabIndex = 8
        Me.cbFixedOffset.Text = "Fixed offset"
        '
        'NumericOffset
        '
        Me.NumericOffset.Location = New System.Drawing.Point(16, 47)
        Me.NumericOffset.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericOffset.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
        Me.NumericOffset.Name = "NumericOffset"
        Me.NumericOffset.Size = New System.Drawing.Size(48, 22)
        Me.NumericOffset.TabIndex = 9
        Me.NumericOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericOffset.Visible = False
        '
        'comboOffsetUnits
        '
        Me.comboOffsetUnits.Items.AddRange(New Object() {"millisec", "sec", "min", "hr", "day"})
        Me.comboOffsetUnits.Location = New System.Drawing.Point(72, 47)
        Me.comboOffsetUnits.Name = "comboOffsetUnits"
        Me.comboOffsetUnits.Size = New System.Drawing.Size(56, 24)
        Me.comboOffsetUnits.TabIndex = 10
        Me.comboOffsetUnits.Text = "min"
        Me.comboOffsetUnits.Visible = False
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(18, 47)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 24)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Description:"
        '
        'lblNumMax
        '
        Me.lblNumMax.Location = New System.Drawing.Point(176, 24)
        Me.lblNumMax.Name = "lblNumMax"
        Me.lblNumMax.Size = New System.Drawing.Size(40, 16)
        Me.lblNumMax.TabIndex = 38
        Me.lblNumMax.Text = "Max:"
        Me.lblNumMax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumMin
        '
        Me.lblNumMin.Location = New System.Drawing.Point(8, 24)
        Me.lblNumMin.Name = "lblNumMin"
        Me.lblNumMin.Size = New System.Drawing.Size(120, 16)
        Me.lblNumMin.TabIndex = 36
        Me.lblNumMin.Text = "Occurrences - Min:"
        Me.lblNumMin.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'numMax
        '
        Me.numMax.Location = New System.Drawing.Point(224, 22)
        Me.numMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMax.Name = "numMax"
        Me.numMax.Size = New System.Drawing.Size(48, 22)
        Me.numMax.TabIndex = 2
        Me.numMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cbUnbounded
        '
        Me.cbUnbounded.Location = New System.Drawing.Point(272, 24)
        Me.cbUnbounded.Name = "cbUnbounded"
        Me.cbUnbounded.Size = New System.Drawing.Size(112, 16)
        Me.cbUnbounded.TabIndex = 3
        Me.cbUnbounded.Text = "Unbounded"
        '
        'numMin
        '
        Me.numMin.Location = New System.Drawing.Point(136, 22)
        Me.numMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMin.Name = "numMin"
        Me.numMin.Size = New System.Drawing.Size(40, 22)
        Me.numMin.TabIndex = 1
        '
        'butRemoveElement
        '
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(114, 35)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveElement.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.butRemoveElement, "Remove selected item")
        '
        'butAddEvent
        '
        Me.butAddEvent.Image = CType(resources.GetObject("butAddEvent.Image"), System.Drawing.Image)
        Me.butAddEvent.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddEvent.Location = New System.Drawing.Point(114, 9)
        Me.butAddEvent.Name = "butAddEvent"
        Me.butAddEvent.Size = New System.Drawing.Size(24, 24)
        Me.butAddEvent.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.butAddEvent, "Add as new event")
        '
        'chkIsPeriodic
        '
        Me.chkIsPeriodic.Location = New System.Drawing.Point(12, 87)
        Me.chkIsPeriodic.Name = "chkIsPeriodic"
        Me.chkIsPeriodic.Size = New System.Drawing.Size(100, 64)
        Me.chkIsPeriodic.TabIndex = 18
        Me.chkIsPeriodic.Text = "Events at regular time period"
        '
        'numPeriod
        '
        Me.numPeriod.Location = New System.Drawing.Point(7, 153)
        Me.numPeriod.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numPeriod.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPeriod.Name = "numPeriod"
        Me.numPeriod.Size = New System.Drawing.Size(48, 22)
        Me.numPeriod.TabIndex = 19
        Me.numPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numPeriod.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPeriod.Visible = False
        '
        'comboTimeUnits
        '
        Me.comboTimeUnits.Items.AddRange(New Object() {"millisec", "sec", "min", "hr"})
        Me.comboTimeUnits.Location = New System.Drawing.Point(55, 153)
        Me.comboTimeUnits.Name = "comboTimeUnits"
        Me.comboTimeUnits.Size = New System.Drawing.Size(56, 24)
        Me.comboTimeUnits.TabIndex = 20
        Me.comboTimeUnits.Text = "min"
        Me.comboTimeUnits.Visible = False
        '
        'butListUp
        '
        Me.butListUp.Image = CType(resources.GetObject("butListUp.Image"), System.Drawing.Image)
        Me.butListUp.Location = New System.Drawing.Point(114, 61)
        Me.butListUp.Name = "butListUp"
        Me.butListUp.Size = New System.Drawing.Size(24, 24)
        Me.butListUp.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.butListUp, "Move selected item up")
        '
        'butListDown
        '
        Me.butListDown.Image = CType(resources.GetObject("butListDown.Image"), System.Drawing.Image)
        Me.butListDown.Location = New System.Drawing.Point(114, 87)
        Me.butListDown.Name = "butListDown"
        Me.butListDown.Size = New System.Drawing.Size(24, 24)
        Me.butListDown.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.butListDown, "Move selected item down")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radioOpen)
        Me.GroupBox1.Controls.Add(Me.radioFixed)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(96, 80)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Event list:"
        '
        'radioOpen
        '
        Me.radioOpen.Checked = True
        Me.radioOpen.Location = New System.Drawing.Point(8, 16)
        Me.radioOpen.Name = "radioOpen"
        Me.radioOpen.Size = New System.Drawing.Size(80, 24)
        Me.radioOpen.TabIndex = 16
        Me.radioOpen.TabStop = True
        Me.radioOpen.Text = "open"
        '
        'radioFixed
        '
        Me.radioFixed.Location = New System.Drawing.Point(8, 43)
        Me.radioFixed.Name = "radioFixed"
        Me.radioFixed.Size = New System.Drawing.Size(80, 24)
        Me.radioFixed.TabIndex = 17
        Me.radioFixed.Text = "Fixed"
        '
        'ListEvents
        '
        Me.ListEvents.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListEvents.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TheEvents})
        Me.ListEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListEvents.HideSelection = False
        Me.ListEvents.LabelEdit = True
        Me.ListEvents.LabelWrap = False
        Me.ListEvents.Location = New System.Drawing.Point(152, 8)
        Me.ListEvents.MultiSelect = False
        Me.ListEvents.Name = "ListEvents"
        Me.ListEvents.Size = New System.Drawing.Size(200, 320)
        Me.ListEvents.SmallImageList = Me.ImageListEvents
        Me.ListEvents.TabIndex = 0
        Me.ListEvents.View = System.Windows.Forms.View.Details
        '
        'TheEvents
        '
        Me.TheEvents.Text = "Events"
        Me.TheEvents.Width = 350
        '
        'ImageListEvents
        '
        Me.ImageListEvents.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageListEvents.ImageStream = CType(resources.GetObject("ImageListEvents.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListEvents.TransparentColor = System.Drawing.Color.Transparent
        '
        'TabpageHistory
        '
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.ListEvents)
        Me.Controls.Add(Me.butListUp)
        Me.Controls.Add(Me.butListDown)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gbEventDetails)
        Me.Controls.Add(Me.chkIsPeriodic)
        Me.Controls.Add(Me.numPeriod)
        Me.Controls.Add(Me.comboTimeUnits)
        Me.Controls.Add(Me.butAddEvent)
        Me.Controls.Add(Me.butRemoveElement)
        Me.HelpProviderEventSeries.SetHelpKeyword(Me, "HowTo/edit_EventSeries.htm")
        Me.HelpProviderEventSeries.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "TabpageHistory"
        Me.HelpProviderEventSeries.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(760, 336)
        Me.gbEventDetails.ResumeLayout(False)
        Me.gbDuration.ResumeLayout(False)
        CType(Me.numericDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbOffset.ResumeLayout(False)
        CType(Me.NumericOffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
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

    Friend Function BuildInterface(ByVal aContainer As Control, ByVal pos As Point, ByVal mandatory_only As Boolean)
        Dim spacer As Integer = 15
        Dim leftmargin As Integer = pos.X

        Dim combo As New ComboBox
        For Each elvi As EventListViewItem In Me.ListEvents.Items
            If ((elvi.IsMandatory) Or (Not mandatory_only)) Then
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

    End Function

    Friend Function ToRichText(ByRef Text As IO.StringWriter, ByVal level As Integer)
        Dim elvi As EventListViewItem

        Text.WriteLine(Space(3 * level) & "\cf1 EventSeries\cf0  = \{\par")
        level = level + 1
        If Me.chkIsPeriodic.Checked Then
            Text.WriteLine(Space(3 * level) & "Periodic offset = " & Me.numPeriod.Value.ToString & " " & Me.comboTimeUnits.Text & "\par")
        End If

        For Each elvi In Me.ListEvents.Items
            Dim s As String
            Text.WriteLine(Space(3 * level) & "\b " & elvi.Text & " (" & elvi.Occurrences.ToString & ") \b0\par")
            Text.WriteLine(Space(3 * level) & "\i   - " & elvi.Description & "\i0\par")

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
                        Text.WriteLine(Space(3 * level) & Filemanager.GetOpenEhrTerm(266, "Event math function") + _
                        " = " & Filemanager.GetOpenEhrTerm(Integer.Parse(elvi.AggregateMathFunction), "Fixed interval") & "\par")
                    Catch
                    End Try
            End Select

            Text.WriteLine("\par")
        Next

        level = level - 1
        Text.WriteLine(Space(3 * level) & "\} -- end EventSeries\par")
    End Function

    Friend Function ToHTML(ByRef Text As IO.StreamWriter, Optional ByVal BackGroundColour As String = "")
        Dim elvi As EventListViewItem

        If Me.chkIsPeriodic.Checked Then
            Text.WriteLine("<p>Periodic offset = " & Me.numPeriod.Value.ToString & " " & Me.comboTimeUnits.Text & "</p>")
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


        For Each elvi In Me.ListEvents.Items

            Text.WriteLine("<tr>")
            Text.WriteLine("<td><b>" & elvi.Text & "</b></td>")
            Text.WriteLine("<td><b>" & elvi.Description & "</b></td>")
            Text.WriteLine("<td>")

            Text.WriteLine(elvi.RM_Class.Type.ToString)

            Select Case elvi.RM_Class.Type
                Case StructureType.Event

                Case StructureType.PointEvent
                    If elvi.hasFixedOffset Then
                        Text.WriteLine("<br>" + Filemanager.GetOpenEhrTerm(179, "Offset") + _
                        " = " & elvi.Offset.ToString & " " & elvi.OffsetUnits & "<br>")
                    End If

                Case StructureType.IntervalEvent
                    If elvi.hasFixedWidth Then
                        Text.WriteLine("<br>" + Filemanager.GetOpenEhrTerm(143, "Fixed interval") + _
                        " = " & elvi.Width.ToString & " " & elvi.WidthUnits & "<br>")
                    End If
                    Try
                        Text.WriteLine("<br>" + Filemanager.GetOpenEhrTerm(266, "Event math function") + _
                        " = " + Filemanager.GetOpenEhrTerm(Integer.Parse(elvi.AggregateMathFunction), "Fixed interval"))
                    Catch
                    End Try
            End Select
            Text.WriteLine("</tr>")
        Next

        Text.WriteLine(Environment.NewLine & "</table>")
        Text.WriteLine(Environment.NewLine & "<hr>")


    End Function

    Friend Function ProcessEventSeries(ByVal rm As RmHistory)
        Dim ev As RmEvent
        Dim HistEvent As EventListViewItem

        sNodeID = rm.NodeId

        If rm.isPeriodic Then

            ' periodic
            Me.chkIsPeriodic.Checked = True
            Me.numPeriod.Value = rm.Period
            Me.comboTimeUnits.Text = rm.PeriodUnits

        Else
            ' not periodic
            Me.chkIsPeriodic.Checked = False
        End If

        If rm.Children.Fixed Then
            ' fixed EventSeries
            Me.radioFixed.Checked = True
        Else
            Me.radioOpen.Checked = True
        End If

        For Each ev In rm.Children
            HistEvent = New EventListViewItem(ev, mFileManager)
            Me.ListEvents.Items.Add(HistEvent)
        Next

        Translate()

        'CHANGE Sam Heard - 2004-05-15
        'Changed the order - set current_item after process event
        'and call process event on the first item in listEvents

        If Me.ListEvents.Items.Count > 0 Then
            Me.ListEvents.Items.Item(0).Selected = True
            Me.butRemoveElement.Visible = True
            ProcessEvent(Me.ListEvents.Items.Item(0))
            current_item = Me.ListEvents.Items.Item(0)
            current_item.Selected = True
        End If

    End Function

    Public ReadOnly Property ComponentType() As String
        Get
            Return "EventSeries"
        End Get
    End Property

    Public Sub Translate()

        Dim HistEvent As EventListViewItem
        Dim elvi As EventListViewItem

        current_item = Nothing

        For Each HistEvent In Me.ListEvents.Items
            HistEvent.Translate()
        Next

        If Me.ListEvents.SelectedItems.Count > 0 Then
            elvi = Me.ListEvents.SelectedItems(0)
            Me.txtEventDescription.Text = elvi.Description
            If elvi.hasNameConstraint Then
                Me.txtRuntimeConstraint.Text = elvi.NameConstraint.ToString
            End If
            current_item = elvi
        End If

    End Sub

    Public Sub TranslateGUI()
        Debug.Assert(False, "Not done yet")
    End Sub

    Friend Function SaveAsEventSeries() As RmHistory
        Dim ev As EventListViewItem
        Dim Hist As RmHistory

        Hist = New RmHistory(Me.NodeId)

        If Me.chkIsPeriodic.Checked Then
            Hist.Period = Me.numPeriod.Value
            Hist.PeriodUnits = Me.comboTimeUnits.Text
        End If

        If Me.ListEvents.Items.Count = 0 Then
            ' there must be at least one event
            Debug.Assert(False)
            butAddEvent_Click(New Object, New System.EventArgs)
        End If

        For Each ev In Me.ListEvents.Items
            Hist.Children.Add(ev.RM_Class)
        Next

        SetEventSeriesCardinality(Hist)

        Return Hist
    End Function

    Private Sub SetEventSeriesCardinality(ByVal a_EventSeries As RmHistory)

        If Me.radioFixed.Checked Then
            Dim i As Integer
            For Each evnt As RmEvent In a_EventSeries.Children
                If evnt.Occurrences.IsUnbounded Then
                    a_EventSeries.Children.Cardinality.IsUnbounded = True
                    Return
                End If
                i += evnt.Occurrences.MaxCount
            Next
            a_EventSeries.Children.Cardinality.MaxCount = i
            Return
        End If
        a_EventSeries.Children.Cardinality.IsUnbounded = True
    End Sub

    Friend Function Reset()
        ' empty and reset all controls
        Me.ListEvents.Items.Clear()
        Me.txtEventDescription.Text = ""
        Me.NumericOffset.Value = 0
        Me.chkIsPeriodic.Checked = False
        Me.numericDuration.Value = 0
    End Function

    Friend Function AddBaseLineEvent()
        Dim elvi As EventListViewItem

        elvi = New EventListViewItem(Filemanager.GetOpenEhrTerm(276, "Baseline event"), mFileManager)
        Me.txtEventDescription.Text = "*"
        elvi.Width = 1
        elvi.WidthUnits = "min"
        Me.radioPointInTime.Checked = False
        Me.cbFixedInterval.Checked = False
        Me.cbFixedOffset.Checked = False
        Me.numMax.Visible = False
        Me.numMin.Value = 0
        Me.numericDuration.Value = 1
        Me.cbUnbounded.Checked = True
        elvi.Occurrences.IsUnbounded = True
        Me.ListEvents.Items.Add(elvi)
        elvi.Selected = True
        current_item = elvi

    End Function

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
        Public Property isPointInTime() As Boolean
            Get
                Return element.isPointInTime
            End Get
            Set(ByVal Value As Boolean)
                element.isPointInTime = Value
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
                Return element.Width
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
        Public Property AggregateMathFunction() As String
            Get
                Return element.AggregateMathFunction
            End Get
            Set(ByVal Value As String)
                element.AggregateMathFunction = Value
            End Set
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

        Public Sub SetEventType(ByVal new_event_type As StructureType)
            element.SetType(new_event_type)
            SetImageIndex()
        End Sub

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
                    Me.ImageIndex = 2 + offset  '?
                Case StructureType.PointEvent
                    Me.ImageIndex = 0 + offset      'o
                Case StructureType.IntervalEvent
                    Me.ImageIndex = 1 + offset  'H
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

            Dim s As String
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

            Dim s As String

            MyBase.Text = elvi.Text
            sDescription = elvi.Description
            ' need to copy here as may be a copy process
            element = elvi.RM_Class.Copy
            SetImageIndex()

        End Sub

    End Class

#End Region

    Private Sub chkIsPeriodic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsPeriodic.CheckedChanged
        Dim check As Boolean

        check = Me.chkIsPeriodic.Checked
        Me.numPeriod.Visible = check
        Me.comboTimeUnits.Visible = check
        If Me.radioPointInTime.Checked Then
            Me.gbOffset.Visible = Not check
        End If

        If current_item Is Nothing Then Exit Sub

        mFileManager.FileEdited = True

    End Sub

    Private Sub radioPointInTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioPointInTime.CheckedChanged
        If Not Me.chkIsPeriodic.Checked Then
            Me.gbOffset.Visible = Me.radioPointInTime.Checked
        End If

        If current_item Is Nothing Then Exit Sub

        current_item.isPointInTime = True
        mFileManager.FileEdited = True

    End Sub

    Private Sub butAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddEvent.Click
        Dim elvi As EventListViewItem
        ' ensure there is no selected event
        If Not current_item Is Nothing Then
            current_item.Selected = False
        End If

        current_item = Nothing

        ' create a new generic event and set the screen accordingly
        elvi = New EventListViewItem(Filemanager.GetOpenEhrTerm(276, "Any event"), mFileManager)
        Me.txtEventDescription.Text = "*"
        elvi.Width = 1
        elvi.WidthUnits = "min"
        elvi.isPointInTime = True
        elvi.SetEventType(StructureType.Event)
        Me.radioPointInTime.Checked = False
        Me.cbFixedInterval.Checked = False
        Me.cbFixedOffset.Checked = False
        Me.numMax.Visible = False
        Me.numMin.Value = 0
        Me.numericDuration.Value = 1
        Me.cbUnbounded.Checked = True
        elvi.Occurrences.IsUnbounded = True
        Me.comboIntervalViewPoint.SelectedIndex = 1  ' delta
        Me.ListEvents.Items.Add(elvi)
        elvi.Selected = True
        current_item = elvi
        elvi.Selected = True
        butRemoveElement.Visible = True
        mFileManager.FileEdited = True
        elvi.BeginEdit()

    End Sub

    Private Sub ProcessEvent(ByVal elvi As EventListViewItem)
        Me.txtEventDescription.Text = elvi.Description
        Me.NumericOffset.Value = elvi.Offset

        If elvi.OffsetUnits <> "" Then
            Me.comboOffsetUnits.Text = elvi.OffsetUnits
        End If

        If elvi.Occurrences.IsUnbounded Then
            Me.cbUnbounded.Checked = True
        Else
            Me.cbUnbounded.Checked = False
            Me.numMax.Value = elvi.Occurrences.MaxCount
        End If
        Me.numMin.Value = elvi.Occurrences.MinCount


        If elvi.Width <> 0 Then
            Me.numericDuration.Value = elvi.Width
        End If

        If elvi.WidthUnits <> "" Then
            Me.comboDurationUnits.Text = elvi.WidthUnits
        End If

        If elvi.AggregateMathFunction <> "" Then
            Dim i As UInt64

            ' has to deal with the change of archetypes from a string
            ' to a code phrase and the openEHR code as an integer
            Try
                i = UInt64.Parse(elvi.AggregateMathFunction)  ' new form
                Me.comboIntervalViewPoint.SelectedValue = i
            Catch
                'text that is not a code
                If elvi.AggregateMathFunction = "Change" Then
                    Me.comboIntervalViewPoint.SelectedIndex = _
                    Me.comboIntervalViewPoint.FindStringExact("delta")
                    elvi.AggregateMathFunction = "147" ' as delta may not lead to a change
                Else
                    Me.comboIntervalViewPoint.SelectedIndex = _
                    Me.comboIntervalViewPoint.FindStringExact(elvi.AggregateMathFunction)
                End If
            End Try
        End If

        Select Case elvi.RM_Class.Type
            Case StructureType.Event
                Me.radioPointInTime.Checked = False
                Me.RadioInterval.Checked = False
            Case StructureType.PointEvent
                Me.radioPointInTime.Checked = True
                If elvi.hasFixedOffset Then
                    Me.cbFixedOffset.Checked = True
                Else
                    Me.cbFixedOffset.Checked = False
                End If
            Case StructureType.IntervalEvent
                Me.RadioInterval.Checked = True
                If elvi.hasFixedWidth Then
                    Me.cbFixedInterval.Checked = True
                Else
                    Me.cbFixedInterval.Checked = False
                End If
        End Select

        If elvi.hasNameConstraint Then
            Me.txtRuntimeConstraint.Text = elvi.NameConstraint.ToString
        End If
    End Sub

    Private Sub listEvents_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListEvents.SelectedIndexChanged
        Dim elvi As EventListViewItem

        If Me.ListEvents.SelectedItems.Count = 1 Then
            ' must be at least one selected - this is called twice when change selection
            ' first set to 0 then selects one
            If Not current_item Is Me.ListEvents.SelectedItems(0) Then
                If Not current_item Is Nothing Then
                    elvi = current_item
                    elvi.Selected = False   ' sets the imageindex to unselected (and the listitem to selected if it is not)
                    current_item = Nothing  ' stops processes when writing information
                End If
                elvi = Me.ListEvents.SelectedItems(0)
                ProcessEvent(elvi)
                current_item = elvi
                ' set the image to selected, don't use selected to change as it will call this again!
                current_item.Selected = True
            End If
        End If

    End Sub

    Private Sub txtEventDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEventDescription.TextChanged
        If current_item Is Nothing Then Return
        current_item.Description = Me.txtEventDescription.Text
        mFileManager.FileEdited = True
    End Sub

    Private Sub RadioInterval_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioInterval.CheckedChanged
        Me.gbDuration.Visible = RadioInterval.Checked

        If current_item Is Nothing Then Return

        current_item.isPointInTime = Not RadioInterval.Checked

        If RadioInterval.Checked Then
            current_item.AggregateMathFunction = Convert.ToString(Me.comboIntervalViewPoint.SelectedValue)
            cbFixedInterval_CheckedChanged(sender, e)
        End If
        mFileManager.FileEdited = True
    End Sub

    Private Sub NumericOffset_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericOffset.ValueChanged, NumericOffset.TextChanged
        If current_item Is Nothing Then Return
        current_item.Offset = Me.NumericOffset.Value
        mFileManager.FileEdited = True
    End Sub

    Private Sub comboOffsetUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboOffsetUnits.SelectedIndexChanged
        If current_item Is Nothing Then Return
        current_item.OffsetUnits = Me.comboOffsetUnits.Text
        mFileManager.FileEdited = True
    End Sub

    Private Sub numericDuration_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numericDuration.ValueChanged, numericDuration.TextChanged
        If current_item Is Nothing Then Return
        current_item.Width = Me.numericDuration.Value
        mFileManager.FileEdited = True
    End Sub

    Private Sub comboDurationUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboDurationUnits.SelectedIndexChanged
        If current_item Is Nothing Then Return
        current_item.WidthUnits = Me.comboDurationUnits.Text
        mFileManager.FileEdited = True
    End Sub

    Private Sub comboIntervalViewPoint_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboIntervalViewPoint.SelectedIndexChanged

        If (current_item Is Nothing) Or mIsLoading Then Return

        current_item.AggregateMathFunction = Convert.ToString(Me.comboIntervalViewPoint.SelectedValue)
        mFileManager.FileEdited = True

    End Sub

    Private Sub radioOpen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioOpen.CheckedChanged
        If current_item Is Nothing Then Return

        mFileManager.FileEdited = True
    End Sub

    Private Sub radioFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioFixed.CheckedChanged
        If current_item Is Nothing Then Return
        mFileManager.FileEdited = True
    End Sub

    Private Sub cbFixedOffset_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFixedOffset.CheckedChanged

        Me.NumericOffset.Visible = cbFixedOffset.Checked
        Me.comboOffsetUnits.Visible = cbFixedOffset.Checked

        If current_item Is Nothing Then Return

        If Me.cbFixedOffset.Checked Then
            current_item.hasFixedOffset = True
            ' may accept default so set them
            current_item.Offset = Me.NumericOffset.Value
            current_item.OffsetUnits = Me.comboOffsetUnits.Text
        Else
            current_item.hasFixedOffset = False
        End If
        mFileManager.FileEdited = True


    End Sub

    Private Sub cbFixedInterval_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFixedInterval.CheckedChanged
        Me.numericDuration.Visible = Me.cbFixedInterval.Checked
        Me.comboDurationUnits.Visible = Me.cbFixedInterval.Checked

        If current_item Is Nothing Then Return

        If cbFixedInterval.Checked Then
            current_item.hasFixedWidth = True
            ' may accept default so load these
            current_item.Width = Me.numericDuration.Value
            current_item.WidthUnits = Me.comboDurationUnits.Text
        Else
            current_item.hasFixedWidth = False
        End If
        mFileManager.FileEdited = True
    End Sub

    Private Sub butRemoveElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveElement.Click

        If Me.ListEvents.SelectedIndices.Count = 0 Or Me.ListEvents.Items.Count = 1 Then
            'must be one selected and more than one in the list - have to have one event!
            Beep()
        Else
            Dim elvi As EventListViewItem

            elvi = ListEvents.SelectedItems(0)

            If MessageBox.Show(AE_Constants.Instance.Remove & elvi.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                Dim nodeid As String

                ' leave an item selected if there is one
                If elvi.Index > 0 Then
                    Me.ListEvents.Items(elvi.Index - 1).Selected = True
                ElseIf Me.ListEvents.Items.Count > 1 Then
                    Me.ListEvents.Items(elvi.Index + 1).Selected = True
                End If
                elvi.Remove()
                mFileManager.FileEdited = True
            End If

        End If
    End Sub

    Private Sub numMin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMin.ValueChanged

        If Not current_item Is Nothing Then

            If numMin.Value > numMax.Value Then
                numMax.Value = numMin.Value
            End If
            current_item.Occurrences.MinCount = Me.numMin.Value
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub numMax_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMax.ValueChanged

        If Not current_item Is Nothing Then
            current_item.Occurrences.MaxCount = Me.numMax.Value
            mFileManager.FileEdited = True
            If numMax.Value < numMin.Value Then
                numMin.Value = numMax.Value
            End If
        End If

    End Sub

    Private Sub cbUnbounded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUnbounded.CheckedChanged

        If Me.cbUnbounded.Checked Then
            Me.numMax.Visible = False
            Me.lblNumMax.Visible = False

            If current_item Is Nothing Then Return

            current_item.Occurrences.IsUnbounded = True
        Else
            Me.numMax.Visible = True
            Me.lblNumMax.Visible = True

            If current_item Is Nothing Then Return

            current_item.Occurrences.MaxCount = Me.numMax.Value
            current_item.Occurrences.IsUnbounded = False
        End If
        mFileManager.FileEdited = True

    End Sub

    Private Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click

        If Not Me.ListEvents.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i As Integer
            i = Me.ListEvents.SelectedIndices(0)
            lvI = Me.ListEvents.SelectedItems(0)

            If i > 0 Then
                Me.ListEvents.Items.Remove(lvI)
                Me.ListEvents.Items.Insert((i - 1), lvI)
                mFileManager.FileEdited = True
                Me.ListEvents.Items.Item(i - 1).Selected = True
            End If
        End If

    End Sub

    Private Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click

        If Not Me.ListEvents.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i, c As Integer

            c = Me.ListEvents.Items.Count
            i = Me.ListEvents.SelectedIndices(0)
            lvI = Me.ListEvents.SelectedItems(0)

            If i < (c - 1) Then
                Me.ListEvents.Items.Remove(lvI)
                Me.ListEvents.Items.Insert((i + 1), lvI)
                lvI.Selected = True
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub ListEvents_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles ListEvents.AfterLabelEdit

        If Not e.Label Is Nothing Then
            Dim lvItem As EventListViewItem

            lvItem = Me.ListEvents.Items(e.Item)

            If e.Label = "" Then
                e.CancelEdit = True
                Return
            End If

            lvItem.Text = e.Label
        End If
    End Sub

    Private Sub buSetRuntimeConstraint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buSetRuntimeConstraint.Click
        Dim frm As New ConstraintForm
        Dim has_constraint As Boolean
        Dim t As Constraint_Text

        has_constraint = current_item.hasNameConstraint
        If has_constraint Then
            t = current_item.NameConstraint.copy
        End If

        frm.ShowConstraint(False, current_item.NameConstraint, mFileManager)
        Select Case frm.ShowDialog
            Case DialogResult.OK
                'no action
                mFileManager.FileEdited = True
            Case DialogResult.Cancel
                ' put it back to null if it was before
                If Not has_constraint Then
                    current_item.hasNameConstraint = False
                Else
                    current_item.NameConstraint = t
                End If
            Case DialogResult.Ignore
                current_item.hasNameConstraint = False
                mFileManager.FileEdited = True
        End Select

        If current_item.hasNameConstraint Then
            Me.txtRuntimeConstraint.Text = current_item.NameConstraint.ToString
        Else
            Me.txtRuntimeConstraint.Text = ""
        End If
    End Sub

    Private Sub TabPageEventSeries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.HelpProviderEventSeries.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath

        mIsLoading = True

        MathFunctionTable = New DataTable("MathFunction")
        Dim newcol As New DataColumn("Code", System.Type.GetType("System.UInt64"))
        MathFunctionTable.Columns.Add(newcol)
        newcol = New DataColumn("Text", System.Type.GetType("System.String"))
        MathFunctionTable.Columns.Add(newcol)

        Dim math_functions As DataRow()
        Dim new_row As DataRow

        math_functions = mFileManager.OntologyManager.CodeForGroupID(14, mFileManager.OntologyManager.LanguageCode) 'event math function

        For Each rw As DataRow In math_functions
            new_row = MathFunctionTable.NewRow
            new_row("Code") = rw.Item(1)
            new_row("Text") = rw.Item(2)
            MathFunctionTable.Rows.Add(new_row)
        Next

        MathFunctionTable.DefaultView.Sort = "Text"

        Me.comboIntervalViewPoint.DataSource = MathFunctionTable
        Me.comboIntervalViewPoint.DisplayMember = "Text"
        Me.comboIntervalViewPoint.ValueMember = "Code"

        mIsLoading = False


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
