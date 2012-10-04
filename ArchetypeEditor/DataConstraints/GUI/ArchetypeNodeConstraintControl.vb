'
'
'	component:   "openEHR Archetype Project"
'	description: "Control that represents the constraints applied to all nodes in an archetype - occurrences, description and runtime name"
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

Option Strict On

Public Class ArchetypeNodeConstraintControl
    Inherits System.Windows.Forms.UserControl

    Private mConstraintControl As ConstraintControl
    Private WithEvents mAnnotationsTable As DataTable
    Private mFileManager As FileManagerLocal
    Private mDataView As DataView
    Private WithEvents termLookUp As TerminologyLookup.TermLookupController
    Friend WithEvents tabConstraint As System.Windows.Forms.TabControl
    Friend WithEvents tpConstraint As System.Windows.Forms.TabPage
    Friend WithEvents tpConstraintDetails As System.Windows.Forms.TabPage
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents gbComments As System.Windows.Forms.GroupBox
    Friend WithEvents dgValueSets As System.Windows.Forms.DataGridView
    Friend WithEvents gbValueSets As System.Windows.Forms.GroupBox
    Friend WithEvents gbNullFlavours As System.Windows.Forms.GroupBox
    Friend WithEvents chkListNull As System.Windows.Forms.CheckedListBox
    Friend WithEvents PanelName As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gbAnnotations As System.Windows.Forms.GroupBox
    Friend WithEvents dgAnnotations As System.Windows.Forms.DataGridView
    Friend WithEvents AnnotationKeyColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AnnotationvalueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gbTerminology As System.Windows.Forms.GroupBox
    Friend WithEvents dgNodeBindings As System.Windows.Forms.DataGridView
    Friend WithEvents TerminologyColumn As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents CodeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PathColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents specialiseButton As System.Windows.Forms.Button
    Friend WithEvents mOccurrences As OccurrencesPanel

    Public Event Specialise(ByVal sender As Object, ByVal e As EventArgs)

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mAnnotationsTable = New DataTable
        Dim keyColumn As New DataColumn("Key", System.Type.GetType("System.String"))
        mAnnotationsTable.Columns.Add(keyColumn)
        Dim valueColumn As New DataColumn("Value", System.Type.GetType("System.String"))
        mAnnotationsTable.Columns.Add(valueColumn)

        dgAnnotations.DataSource = mAnnotationsTable

        mIsLoading = True

        mFileManager = a_file_manager
        mDataView = New DataView(mFileManager.OntologyManager.TermBindingsTable)

        mOccurrences = New OccurrencesPanel(mFileManager)

        Select Case Main.Instance.Options.OccurrencesView
            Case "lexical"
                mOccurrences.Mode = OccurrencesMode.Lexical
            Case "numeric"
                mOccurrences.Mode = OccurrencesMode.Numeric
        End Select

        PanelGenericConstraint.Controls.Add(mOccurrences)
        mOccurrences.Dock = DockStyle.Fill
        TranslateGUI()
        mIsLoading = False

        HelpProviderCommonConstraint.HelpNamespace = Main.Instance.Options.HelpLocationPath

        If Not Main.Instance.TerminologyLookup Is Nothing Then
            termLookUp = Main.Instance.TerminologyLookup.NewTermLookupController
            gbTerminology.Controls.Add(termLookUp.Control)
            termLookUp.Control.Dock = System.Windows.Forms.DockStyle.Bottom
            termLookUp.Control.Size = New System.Drawing.Size(420, 55)
            termLookUp.Control.Visible = False
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
    Protected components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PanelGenericConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelDataConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelAddressable As System.Windows.Forms.Panel
    Friend WithEvents txtRuntimeName As System.Windows.Forms.TextBox
    Friend WithEvents txtTermDescription As System.Windows.Forms.TextBox
    Friend WithEvents PanelLower As System.Windows.Forms.Panel
    Friend WithEvents butSetRuntimeName As System.Windows.Forms.Button
    Friend WithEvents HelpProviderCommonConstraint As System.Windows.Forms.HelpProvider
    Friend WithEvents lblRunTimeName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents labelAny As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ArchetypeNodeConstraintControl))
        Me.PanelGenericConstraint = New System.Windows.Forms.Panel
        Me.PanelDataConstraint = New System.Windows.Forms.Panel
        Me.labelAny = New System.Windows.Forms.Label
        Me.PanelAddressable = New System.Windows.Forms.Panel
        Me.PanelName = New System.Windows.Forms.Panel
        Me.butSetRuntimeName = New System.Windows.Forms.Button
        Me.lblRunTimeName = New System.Windows.Forms.Label
        Me.txtRuntimeName = New System.Windows.Forms.TextBox
        Me.txtTermDescription = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.Label
        Me.PanelLower = New System.Windows.Forms.Panel
        Me.HelpProviderCommonConstraint = New System.Windows.Forms.HelpProvider
        Me.tabConstraint = New System.Windows.Forms.TabControl
        Me.tpConstraint = New System.Windows.Forms.TabPage
        Me.tpConstraintDetails = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gbAnnotations = New System.Windows.Forms.GroupBox
        Me.dgAnnotations = New System.Windows.Forms.DataGridView
        Me.AnnotationKeyColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AnnotationvalueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gbTerminology = New System.Windows.Forms.GroupBox
        Me.dgNodeBindings = New System.Windows.Forms.DataGridView
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.gbComments = New System.Windows.Forms.GroupBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.gbNullFlavours = New System.Windows.Forms.GroupBox
        Me.chkListNull = New System.Windows.Forms.CheckedListBox
        Me.gbValueSets = New System.Windows.Forms.GroupBox
        Me.dgValueSets = New System.Windows.Forms.DataGridView
        Me.TerminologyColumn = New System.Windows.Forms.DataGridViewButtonColumn
        Me.CodeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PathColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.specialiseButton = New System.Windows.Forms.Button
        Me.PanelDataConstraint.SuspendLayout()
        Me.PanelAddressable.SuspendLayout()
        Me.PanelName.SuspendLayout()
        Me.PanelLower.SuspendLayout()
        Me.tabConstraint.SuspendLayout()
        Me.tpConstraint.SuspendLayout()
        Me.tpConstraintDetails.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gbAnnotations.SuspendLayout()
        CType(Me.dgAnnotations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTerminology.SuspendLayout()
        CType(Me.dgNodeBindings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbComments.SuspendLayout()
        Me.gbNullFlavours.SuspendLayout()
        Me.gbValueSets.SuspendLayout()
        CType(Me.dgValueSets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelGenericConstraint
        '
        Me.PanelGenericConstraint.BackColor = System.Drawing.Color.Transparent
        Me.PanelGenericConstraint.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelGenericConstraint.Location = New System.Drawing.Point(3, 3)
        Me.PanelGenericConstraint.Name = "PanelGenericConstraint"
        Me.PanelGenericConstraint.Size = New System.Drawing.Size(420, 51)
        Me.PanelGenericConstraint.TabIndex = 0
        '
        'PanelDataConstraint
        '
        Me.PanelDataConstraint.BackColor = System.Drawing.Color.Transparent
        Me.PanelDataConstraint.Controls.Add(Me.labelAny)
        Me.PanelDataConstraint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDataConstraint.Location = New System.Drawing.Point(0, 96)
        Me.PanelDataConstraint.Name = "PanelDataConstraint"
        Me.PanelDataConstraint.Size = New System.Drawing.Size(420, 311)
        Me.PanelDataConstraint.TabIndex = 32
        '
        'labelAny
        '
        Me.labelAny.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelAny.Location = New System.Drawing.Point(16, 8)
        Me.labelAny.Name = "labelAny"
        Me.labelAny.Size = New System.Drawing.Size(136, 24)
        Me.labelAny.TabIndex = 0
        Me.labelAny.Text = "Any"
        '
        'PanelAddressable
        '
        Me.PanelAddressable.BackColor = System.Drawing.Color.Transparent
        Me.PanelAddressable.Controls.Add(Me.PanelName)
        Me.PanelAddressable.Controls.Add(Me.txtTermDescription)
        Me.PanelAddressable.Controls.Add(Me.lblDescription)
        Me.PanelAddressable.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAddressable.Location = New System.Drawing.Point(0, 0)
        Me.PanelAddressable.Name = "PanelAddressable"
        Me.PanelAddressable.Size = New System.Drawing.Size(420, 96)
        Me.PanelAddressable.TabIndex = 31
        '
        'PanelName
        '
        Me.PanelName.Controls.Add(Me.butSetRuntimeName)
        Me.PanelName.Controls.Add(Me.lblRunTimeName)
        Me.PanelName.Controls.Add(Me.txtRuntimeName)
        Me.PanelName.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelName.Location = New System.Drawing.Point(0, 62)
        Me.PanelName.Name = "PanelName"
        Me.PanelName.Size = New System.Drawing.Size(420, 34)
        Me.PanelName.TabIndex = 27
        '
        'butSetRuntimeName
        '
        Me.butSetRuntimeName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderCommonConstraint.SetHelpKeyword(Me.butSetRuntimeName, "HowTo/Editing/Set_runtime_name.html")
        Me.HelpProviderCommonConstraint.SetHelpNavigator(Me.butSetRuntimeName, System.Windows.Forms.HelpNavigator.Topic)
        Me.butSetRuntimeName.Location = New System.Drawing.Point(381, 7)
        Me.butSetRuntimeName.Name = "butSetRuntimeName"
        Me.HelpProviderCommonConstraint.SetShowHelp(Me.butSetRuntimeName, True)
        Me.butSetRuntimeName.Size = New System.Drawing.Size(26, 20)
        Me.butSetRuntimeName.TabIndex = 8
        Me.butSetRuntimeName.Text = "..."
        '
        'lblRunTimeName
        '
        Me.lblRunTimeName.Location = New System.Drawing.Point(10, 2)
        Me.lblRunTimeName.Name = "lblRunTimeName"
        Me.lblRunTimeName.Size = New System.Drawing.Size(120, 32)
        Me.lblRunTimeName.TabIndex = 6
        Me.lblRunTimeName.Text = "Runtime name constraint:"
        Me.lblRunTimeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRuntimeName
        '
        Me.txtRuntimeName.Location = New System.Drawing.Point(136, 8)
        Me.txtRuntimeName.Name = "txtRuntimeName"
        Me.txtRuntimeName.ReadOnly = True
        Me.txtRuntimeName.Size = New System.Drawing.Size(206, 20)
        Me.txtRuntimeName.TabIndex = 7
        '
        'txtTermDescription
        '
        Me.txtTermDescription.Location = New System.Drawing.Point(136, 3)
        Me.txtTermDescription.Multiline = True
        Me.txtTermDescription.Name = "txtTermDescription"
        Me.txtTermDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermDescription.Size = New System.Drawing.Size(224, 53)
        Me.txtTermDescription.TabIndex = 5
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(19, 8)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(112, 16)
        Me.lblDescription.TabIndex = 26
        Me.lblDescription.Text = "Description:"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelLower
        '
        Me.PanelLower.BackColor = System.Drawing.Color.Transparent
        Me.PanelLower.Controls.Add(Me.PanelDataConstraint)
        Me.PanelLower.Controls.Add(Me.PanelAddressable)
        Me.PanelLower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLower.Location = New System.Drawing.Point(3, 54)
        Me.PanelLower.Name = "PanelLower"
        Me.PanelLower.Size = New System.Drawing.Size(420, 407)
        Me.PanelLower.TabIndex = 33
        '
        'tabConstraint
        '
        Me.tabConstraint.Controls.Add(Me.tpConstraint)
        Me.tabConstraint.Controls.Add(Me.tpConstraintDetails)
        Me.tabConstraint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabConstraint.Location = New System.Drawing.Point(0, 0)
        Me.tabConstraint.Name = "tabConstraint"
        Me.tabConstraint.SelectedIndex = 0
        Me.tabConstraint.Size = New System.Drawing.Size(434, 490)
        Me.tabConstraint.TabIndex = 34
        '
        'tpConstraint
        '
        Me.tpConstraint.BackColor = System.Drawing.Color.Transparent
        Me.tpConstraint.Controls.Add(Me.PanelLower)
        Me.tpConstraint.Controls.Add(Me.PanelGenericConstraint)
        Me.tpConstraint.Location = New System.Drawing.Point(4, 22)
        Me.tpConstraint.Margin = New System.Windows.Forms.Padding(0)
        Me.tpConstraint.Name = "tpConstraint"
        Me.tpConstraint.Padding = New System.Windows.Forms.Padding(3)
        Me.tpConstraint.Size = New System.Drawing.Size(426, 464)
        Me.tpConstraint.TabIndex = 0
        Me.tpConstraint.Text = "Constraint"
        '
        'tpConstraintDetails
        '
        Me.tpConstraintDetails.BackColor = System.Drawing.Color.Transparent
        Me.tpConstraintDetails.Controls.Add(Me.SplitContainer1)
        Me.tpConstraintDetails.Controls.Add(Me.Splitter2)
        Me.tpConstraintDetails.Controls.Add(Me.gbComments)
        Me.tpConstraintDetails.Controls.Add(Me.gbNullFlavours)
        Me.tpConstraintDetails.Controls.Add(Me.gbValueSets)
        Me.tpConstraintDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpConstraintDetails.Name = "tpConstraintDetails"
        Me.tpConstraintDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpConstraintDetails.Size = New System.Drawing.Size(426, 464)
        Me.tpConstraintDetails.TabIndex = 1
        Me.tpConstraintDetails.Text = "Details"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 71)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbAnnotations)
        Me.SplitContainer1.Panel1MinSize = 60
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbTerminology)
        Me.SplitContainer1.Panel2MinSize = 60
        Me.SplitContainer1.Size = New System.Drawing.Size(420, 172)
        Me.SplitContainer1.SplitterDistance = 103
        Me.SplitContainer1.TabIndex = 1
        '
        'gbAnnotations
        '
        Me.gbAnnotations.Controls.Add(Me.dgAnnotations)
        Me.gbAnnotations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbAnnotations.Location = New System.Drawing.Point(0, 0)
        Me.gbAnnotations.Name = "gbAnnotations"
        Me.gbAnnotations.Size = New System.Drawing.Size(420, 103)
        Me.gbAnnotations.TabIndex = 2
        Me.gbAnnotations.TabStop = False
        Me.gbAnnotations.Text = "Annotations"
        Me.gbAnnotations.Visible = False
        '
        'dgAnnotations
        '
        Me.dgAnnotations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgAnnotations.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AnnotationKeyColumn, Me.AnnotationvalueColumn})
        Me.dgAnnotations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgAnnotations.Location = New System.Drawing.Point(3, 16)
        Me.dgAnnotations.Name = "dgAnnotations"
        Me.dgAnnotations.Size = New System.Drawing.Size(414, 84)
        Me.dgAnnotations.TabIndex = 0
        '
        'key
        '
        Me.AnnotationKeyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AnnotationKeyColumn.DataPropertyName = "Key"
        Me.AnnotationKeyColumn.FillWeight = 25.0!
        Me.AnnotationKeyColumn.HeaderText = "Key"
        Me.AnnotationKeyColumn.Name = "key"
        '
        'valueColumn
        '
        Me.AnnotationvalueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AnnotationvalueColumn.DataPropertyName = "Value"
        Me.AnnotationvalueColumn.FillWeight = 75.0!
        Me.AnnotationvalueColumn.HeaderText = "Value"
        Me.AnnotationvalueColumn.Name = "valueColumn"
        '
        'gbTerminology
        '
        Me.gbTerminology.Controls.Add(Me.dgNodeBindings)
        Me.gbTerminology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTerminology.Location = New System.Drawing.Point(0, 0)
        Me.gbTerminology.Name = "gbTerminology"
        Me.gbTerminology.Size = New System.Drawing.Size(420, 65)
        Me.gbTerminology.TabIndex = 3
        Me.gbTerminology.TabStop = False
        Me.gbTerminology.Text = "Node meaning in terminologies"
        '
        'dgNodeBindings
        '
        Me.dgNodeBindings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgNodeBindings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TerminologyColumn, Me.CodeColumn, Me.PathColumn})
        Me.dgNodeBindings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgNodeBindings.Location = New System.Drawing.Point(3, 16)
        Me.dgNodeBindings.Name = "dgNodeBindings"
        Me.dgNodeBindings.RowTemplate.Height = 24
        Me.dgNodeBindings.Size = New System.Drawing.Size(414, 46)
        Me.dgNodeBindings.TabIndex = 2
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(3, 68)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(420, 3)
        Me.Splitter2.TabIndex = 11
        Me.Splitter2.TabStop = False
        '
        'gbComments
        '
        Me.gbComments.Controls.Add(Me.txtComments)
        Me.gbComments.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbComments.Location = New System.Drawing.Point(3, 3)
        Me.gbComments.Name = "gbComments"
        Me.gbComments.Size = New System.Drawing.Size(420, 65)
        Me.gbComments.TabIndex = 0
        Me.gbComments.TabStop = False
        Me.gbComments.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtComments.Location = New System.Drawing.Point(3, 16)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(414, 46)
        Me.txtComments.TabIndex = 0
        '
        'gbNullFlavours
        '
        Me.gbNullFlavours.Controls.Add(Me.chkListNull)
        Me.gbNullFlavours.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbNullFlavours.Location = New System.Drawing.Point(3, 298)
        Me.gbNullFlavours.Name = "gbNullFlavours"
        Me.gbNullFlavours.Size = New System.Drawing.Size(420, 110)
        Me.gbNullFlavours.TabIndex = 4
        Me.gbNullFlavours.TabStop = False
        Me.gbNullFlavours.Text = "Reasons why null"
        '
        'chkListNull
        '
        Me.chkListNull.CheckOnClick = True
        Me.chkListNull.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkListNull.FormattingEnabled = True
        Me.chkListNull.Location = New System.Drawing.Point(3, 16)
        Me.chkListNull.Name = "chkListNull"
        Me.chkListNull.Size = New System.Drawing.Size(414, 79)
        Me.chkListNull.TabIndex = 0
        '
        'gbValueSets
        '
        Me.gbValueSets.Controls.Add(Me.dgValueSets)
        Me.gbValueSets.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbValueSets.Location = New System.Drawing.Point(3, 408)
        Me.gbValueSets.Name = "gbValueSets"
        Me.gbValueSets.Size = New System.Drawing.Size(420, 53)
        Me.gbValueSets.TabIndex = 5
        Me.gbValueSets.TabStop = False
        Me.gbValueSets.Text = "Value sets in external terminologies"
        Me.gbValueSets.Visible = False
        '
        'dgValueSets
        '
        Me.dgValueSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgValueSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgValueSets.Location = New System.Drawing.Point(3, 16)
        Me.dgValueSets.Name = "dgValueSets"
        Me.dgValueSets.RowTemplate.Height = 24
        Me.dgValueSets.Size = New System.Drawing.Size(414, 34)
        Me.dgValueSets.TabIndex = 3
        '
        'TerminologyColumn
        '
        Me.TerminologyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.TerminologyColumn.FillWeight = 70.0!
        Me.TerminologyColumn.HeaderText = "Terminology"
        Me.TerminologyColumn.MinimumWidth = 150
        Me.TerminologyColumn.Name = "TerminologyColumn"
        Me.TerminologyColumn.ReadOnly = True
        Me.TerminologyColumn.Text = ""
        Me.TerminologyColumn.Width = 150
        '
        'CodeColumn
        '
        Me.CodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CodeColumn.FillWeight = 30.0!
        Me.CodeColumn.HeaderText = "Code"
        Me.CodeColumn.MinimumWidth = 20
        Me.CodeColumn.Name = "CodeColumn"
        Me.CodeColumn.Width = 57
        '
        'PathColumn
        '
        Me.PathColumn.HeaderText = "Path"
        Me.PathColumn.Name = "PathColumn"
        Me.PathColumn.Visible = False
        '
        'specialiseButton
        '
        Me.specialiseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.specialiseButton.Image = CType(resources.GetObject("specialiseButton.Image"), System.Drawing.Image)
        Me.specialiseButton.ImageAlign = ContentAlignment.MiddleLeft
        Me.specialiseButton.Location = New System.Drawing.Point(284, 0)
        Me.specialiseButton.Name = "specialiseButton"
        Me.specialiseButton.Size = New System.Drawing.Size(140, 20)
        Me.specialiseButton.TabIndex = 0
        Me.specialiseButton.Text = "Specialise..."
        Me.specialiseButton.UseVisualStyleBackColor = True
        '
        'ArchetypeNodeConstraintControl
        '
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.tabConstraint)
        Me.Controls.Add(Me.specialiseButton)
        Me.HelpProviderCommonConstraint.SetHelpKeyword(Me, "HowTo/Editing/set_common_constraints.htm")
        Me.HelpProviderCommonConstraint.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "ArchetypeNodeConstraintControl"
        Me.HelpProviderCommonConstraint.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(434, 490)
        Me.PanelDataConstraint.ResumeLayout(False)
        Me.PanelAddressable.ResumeLayout(False)
        Me.PanelAddressable.PerformLayout()
        Me.PanelName.ResumeLayout(False)
        Me.PanelName.PerformLayout()
        Me.PanelLower.ResumeLayout(False)
        Me.tabConstraint.ResumeLayout(False)
        Me.tpConstraint.ResumeLayout(False)
        Me.tpConstraintDetails.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.gbAnnotations.ResumeLayout(False)
        CType(Me.dgAnnotations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTerminology.ResumeLayout(False)
        CType(Me.dgNodeBindings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbComments.ResumeLayout(False)
        Me.gbComments.PerformLayout()
        Me.gbNullFlavours.ResumeLayout(False)
        Me.gbValueSets.ResumeLayout(False)
        CType(Me.dgValueSets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub


#End Region

    Private mArchetypeNode As ArchetypeNode

    Private mIsLoading As Boolean = True

    Protected ReadOnly Property IsLoading() As Boolean
        Get
            Return mIsLoading
        End Get
    End Property

    Public WriteOnly Property LocalFileManager() As FileManagerLocal
        Set(ByVal Value As FileManagerLocal)
            mFileManager = Value
            mOccurrences.LocalFileManager = Value
        End Set
    End Property

    Public Sub TranslateGUI()
        specialiseButton.Text = AE_Constants.Instance.Specialise
        lblDescription.Text = Filemanager.GetOpenEhrTerm(113, lblDescription.Text)
        lblRunTimeName.Text = Filemanager.GetOpenEhrTerm(114, lblRunTimeName.Text)
        tpConstraint.Text = Filemanager.GetOpenEhrTerm(87, tpConstraint.Text)
        tpConstraintDetails.Text = Filemanager.GetOpenEhrTerm(581, tpConstraintDetails.Text)
        TerminologyColumn.HeaderText = Filemanager.GetOpenEhrTerm(47, TerminologyColumn.HeaderText)
        CodeColumn.HeaderText = Filemanager.GetOpenEhrTerm(90, CodeColumn.HeaderText)
        AnnotationKeyColumn.HeaderText = Filemanager.GetOpenEhrTerm(696, AnnotationKeyColumn.HeaderText)
        AnnotationvalueColumn.HeaderText = Filemanager.GetOpenEhrTerm(697, AnnotationvalueColumn.HeaderText)
        gbAnnotations.Text = Filemanager.GetOpenEhrTerm(698, gbAnnotations.Text)
    End Sub

    Public Sub ShowConstraint(ByVal isSingle As Boolean, ByVal isState As Boolean, ByVal isMandatory As Boolean, ByVal node As ArchetypeNode, ByVal fileManager As FileManagerLocal)
        mFileManager = fileManager
        mIsLoading = True
        SuspendLayout()

        Dim isSameSpecialisationDepth As Boolean = mFileManager.OntologyManager.NumberOfSpecialisations = node.RM_Class.SpecialisationDepth Or node.IsAnonymous
        specialiseButton.Visible = Not isSameSpecialisationDepth
        specialiseButton.BringToFront()

        For Each c As Control In tpConstraint.Controls
            c.Enabled = isSameSpecialisationDepth
        Next

        For Each c As Control In tpConstraintDetails.Controls
            c.Enabled = isSameSpecialisationDepth
        Next

        ' hide the label if there is no constraint (for ANY or Cluster) - see below
        labelAny.Visible = False

        If Not termLookUp Is Nothing Then
            termLookUp.TermName = ""
        End If

        If Not mConstraintControl Is Nothing Then
            PanelDataConstraint.Controls.Remove(mConstraintControl)
            mConstraintControl = Nothing
        End If

        'Hide Occurrences and show null flavours if an Element archetype; and the text and description are not editable here.
        Dim isElement As Boolean = mFileManager.Archetype.RmEntity = StructureType.Element
        PanelGenericConstraint.Visible = Not isElement
        gbNullFlavours.Visible = isElement
        txtTermDescription.Enabled = isSameSpecialisationDepth And Not isElement
        txtComments.Enabled = isSameSpecialisationDepth And Not isElement

        mOccurrences.SetUnitary = False
        mOccurrences.SetSingle = isSingle

        Select Case node.RM_Class.Type

            Case StructureType.Tree, StructureType.List, StructureType.Table, StructureType.Single
                If isMandatory Then
                    mOccurrences.SetMandatory = True
                End If

                mOccurrences.SetUnitary = True

            Case StructureType.Element, StructureType.Reference
                Dim archetypeElem As ArchetypeElement = CType(node, ArchetypeElement)
                SetUpNullFlavours(archetypeElem)

                Select Case archetypeElem.Constraint.Kind
                    Case ConstraintKind.Any
                        labelAny.Text = AE_Constants.Instance.Any
                        labelAny.Visible = True
                    Case Else
                        If archetypeElem.Constraint.Kind = ConstraintKind.Text AndAlso CType(archetypeElem.Constraint, Constraint_Text).TypeOfTextConstraint = TextConstraintType.Terminology Then
                            gbValueSets.Visible = True
                        Else
                            gbValueSets.Visible = False
                        End If

                        mConstraintControl = ConstraintControl.CreateConstraintControl(archetypeElem.Constraint.Kind, mFileManager)
                        PanelDataConstraint.Controls.Add(mConstraintControl)
                        mConstraintControl.Dock = DockStyle.Fill
                        mConstraintControl.ShowElement(isState, archetypeElem)
                End Select

            Case StructureType.Slot
                mConstraintControl = ConstraintControl.CreateConstraintControl(ConstraintKind.Slot, mFileManager)
                PanelDataConstraint.Controls.Add(mConstraintControl)
                mConstraintControl.Dock = DockStyle.Fill
                Dim constraint As Constraint_Slot = TryCast(node.RM_Class, RmSlot).SlotConstraint
                mConstraintControl.ShowConstraint(isState, constraint)

                Select Case constraint.RM_ClassType
                    Case StructureType.Tree, StructureType.Table, StructureType.List, StructureType.Single, StructureType.Structure
                        If isMandatory Then
                            mOccurrences.SetMandatory = True
                        End If

                        mOccurrences.SetUnitary = True
                End Select

            Case StructureType.Cluster
                labelAny.Visible = False
                mConstraintControl = New ClusterControl(fileManager)
                CType(mConstraintControl, ClusterControl).Item = CType(node, ArchetypeComposite)
                PanelDataConstraint.Controls.Add(mConstraintControl)
                CType(mConstraintControl, ClusterControl).Header = 50
                mConstraintControl.Dock = DockStyle.Fill

        End Select

        mArchetypeNode = node

        If isState Then
            tpConstraint.BackColor = System.Drawing.Color.LightSteelBlue
            tpConstraintDetails.BackColor = System.Drawing.Color.LightSteelBlue
        Else
            tpConstraint.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(234, Byte), Integer))
            tpConstraintDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(234, Byte), Integer))
        End If

        SetControlValues(isState)

        If node.IsAnonymous Then
            If tabConstraint.TabPages.Contains(tpConstraintDetails) Then
                tabConstraint.TabPages.Remove(tpConstraintDetails)      'Hide details tab
            End If

            PanelName.Hide()
            dgNodeBindings.Hide()
        Else
            If Not tabConstraint.TabPages.Contains(tpConstraintDetails) Then
                tabConstraint.TabPages.Add(tpConstraintDetails)         'Show details tab
            End If

            PanelName.Show()
            dgNodeBindings.Show()
            Dim nodeID As String = node.RM_Class.NodeId
            mDataView.Table.Columns(1).DefaultValue = nodeID
            mDataView.RowFilter = "Path = '" & nodeID & "'"

            If mDataView.Count = 0 And Not termLookUp Is Nothing Then
                termLookUp.Control.Hide()
            End If
        End If

        If Main.Instance.IsDefaultLanguageRightToLeft Then
            Main.Reflect(mConstraintControl)
        End If

        ResumeLayout(False)
        mIsLoading = False
    End Sub

    Private Sub SetUpNullFlavours(ByVal archetypeElmnt As ArchetypeElement)
        Dim cp As CodePhrase = archetypeElmnt.RM_Class.ConstrainedNullFlavours
        Dim term As Term

        If chkListNull.Items.Count = 0 Then
            For Each row As DataRow In TerminologyServer.Instance.CodesForGrouperID(15)
                term = New Term(CStr(row(1)))
                term.Text = CStr(row(2))
                chkListNull.Items.Add(term, SetNullFlavorChecked(cp, term.Code))
            Next
        Else
            For i As Integer = 0 To chkListNull.Items.Count - 1
                term = CType(chkListNull.Items(i), Term)
                chkListNull.SetItemChecked(i, SetNullFlavorChecked(cp, term.Code))
            Next
        End If

        gbNullFlavours.Visible = True
    End Sub

    Private Function SetNullFlavorChecked(ByVal nullFlavors As CodePhrase, ByVal code As String) As Boolean
        Return nullFlavors.Codes.Count = 0 Or nullFlavors.Codes.Contains(code)
    End Function

    Protected Overridable Sub SetControlValues(ByVal IsState As Boolean)
        mOccurrences.Cardinality = mArchetypeNode.Occurrences

        'Disable all but occurrences for References
        Dim isSameSpecialisationDepth As Boolean = mFileManager.OntologyManager.NumberOfSpecialisations = mArchetypeNode.RM_Class.SpecialisationDepth Or mArchetypeNode.IsAnonymous
        Dim isntReference As Boolean = mArchetypeNode.RM_Class.Type <> StructureType.Reference
        PanelDataConstraint.Enabled = isSameSpecialisationDepth And isntReference
        PanelAddressable.Enabled = isSameSpecialisationDepth And isntReference

        If mArchetypeNode.IsAnonymous Then
            PanelAddressable.Visible = False
            gbTerminology.Visible = False
            gbAnnotations.Visible = False
        Else
            PanelAddressable.Visible = True
            gbTerminology.Visible = True
            gbAnnotations.Visible = True

            txtTermDescription.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).Description
            txtComments.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).Comment
            mAnnotationsTable.Clear()

            If CType(mArchetypeNode, ArchetypeNodeAbstract).Annotations.Count > 0 Then
                For Each key As String In CType(mArchetypeNode, ArchetypeNodeAbstract).Annotations.Keys
                    Dim row As DataRow = mAnnotationsTable.NewRow
                    row(0) = key
                    row(1) = CType(mArchetypeNode, ArchetypeNodeAbstract).Annotations.Item(key)
                    mAnnotationsTable.Rows.Add(row)
                Next
            End If

            txtRuntimeName.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).RuntimeNameText
        End If
    End Sub

    Private Sub txtTermDescription_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTermDescription.KeyPress
        ' work around as acceptsreturn = false does not deal with stop Enter unless there is a AcceptButton
        If e.KeyChar = Chr(13) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTermDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermDescription.TextChanged
        If Not mIsLoading And Not mArchetypeNode Is Nothing Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Description = txtTermDescription.Text
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub txtComments_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComments.TextChanged
        If Not mIsLoading And Not mArchetypeNode Is Nothing Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Comment = txtComments.Text
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butSetRuntimeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSetRuntimeName.Click
        If Not mArchetypeNode Is Nothing Then
            Dim t As Constraint_Text = Nothing
            Dim hasConstraint As Boolean = mArchetypeNode.RM_Class.HasNameConstraint

            If hasConstraint Then
                t = CType(mArchetypeNode.RM_Class.NameConstraint.Copy, Constraint_Text)
            End If

            Dim frm As New ConstraintForm
            frm.ShowConstraint(False, mArchetypeNode.RM_Class.NameConstraint, mFileManager)

            Select Case frm.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK
                    'no action
                    mFileManager.FileEdited = True
                Case Windows.Forms.DialogResult.Cancel
                    ' put it back to null if it was before
                    If Not hasConstraint Then
                        mArchetypeNode.RM_Class.HasNameConstraint = False
                    Else
                        mArchetypeNode.RM_Class.NameConstraint = t
                    End If
                Case Windows.Forms.DialogResult.Ignore
                    mArchetypeNode.RM_Class.HasNameConstraint = False
                    mFileManager.FileEdited = True
            End Select

            If mArchetypeNode.RM_Class.HasNameConstraint Then
                txtRuntimeName.Text = mArchetypeNode.RM_Class.NameConstraint.ToString
            Else
                txtRuntimeName.Text = ""
            End If
        End If
    End Sub

    Private Sub mAnnotations_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles mAnnotationsTable.ColumnChanging
        If e.Column.Ordinal = 0 Then
            Dim newKey As String = TryCast(e.ProposedValue, String)

            If newKey Is Nothing OrElse System.Text.RegularExpressions.Regex.IsMatch(newKey, "^[a-zA-Z][a-zA-Z0-9_]*$") Then
                e.Row.ClearErrors()
            Else
                e.Row.SetColumnError(0, AnnotationKeyColumn.HeaderText + " must start with a letter, optionally followed by letters or numbers.")
            End If
        End If

        If Not mIsLoading And Not e.Row.RowState = DataRowState.Detached Then
            If e.Column.Ordinal = 0 Then
                Dim oldKey As String = TryCast(e.Row(0), String)

                If Not String.IsNullOrEmpty(oldKey) Then
                    mFileManager.OntologyManager.DeleteOtherAnnotation(oldKey, mArchetypeNode.RM_Class.NodeId)
                End If
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub mAnnotations_RowChanged(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs) Handles mAnnotationsTable.RowChanging, mAnnotationsTable.RowDeleting
        If Not (mIsLoading Or mFileManager.FileLoading) Then
            If Not e.Row.HasErrors Then
                Dim key As String = TryCast(e.Row(0), String)
                Dim value As String = TryCast(e.Row(1), String)

                If Not String.IsNullOrEmpty(key) And Not String.IsNullOrEmpty(value) Then
                    Select Case e.Action
                        Case DataRowAction.Add, DataRowAction.Commit, DataRowAction.Change
                            mFileManager.OntologyManager.SetOtherAnnotation(key, value, mArchetypeNode.RM_Class.NodeId)
                        Case DataRowAction.Delete
                            mFileManager.OntologyManager.DeleteOtherAnnotation(key, mArchetypeNode.RM_Class.NodeId)
                    End Select

                    mFileManager.FileEdited = True
                End If
            End If
        End If
    End Sub

    Private Sub ArchetypeNodeConstraintControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TerminologyColumn.DataPropertyName = "Terminology"
        CodeColumn.DataPropertyName = "Code"
        PathColumn.DataPropertyName = "Path"
        dgNodeBindings.DataSource = mDataView
    End Sub

    Private Sub termLookUp_TermChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles termLookUp.TermChanged
        If Not dgNodeBindings.CurrentRow Is Nothing And Not termLookUp Is Nothing Then
            dgNodeBindings.CurrentRow.Cells(2).Value = termLookUp.ConceptId
            dgNodeBindings.CurrentRow.Cells(2).ToolTipText = termLookUp.TermName
        End If
    End Sub

    Private Sub PreferredTermsLoaded(ByVal dataset As Data.DataSet)
        Cursor = Cursors.Default

        If dataset.Tables(0).Rows.Count > 0 And Not termLookUp Is Nothing Then
            termLookUp.TermName = CStr(dataset.Tables(0).Rows(0).Item(2))
        End If
    End Sub

    Private Sub ShowTerminologyLookupException(ByVal ex As Exception)
        Cursor = Cursors.Default

        If Not Main.Instance.TerminologyLookup Is Nothing Then
            MessageBox.Show(ex.Message, Main.Instance.TerminologyLookup.Name + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub UpdateTermLookup(ByVal row As DataGridViewRow)
        If Not row Is Nothing And Not termLookUp Is Nothing Then
            termLookUp.TerminologyName = TryCast(row.Cells(0).Value, String)
            termLookUp.QueryName = row.Cells(0).ToolTipText
            termLookUp.Language = TryCast(row.Cells(0).Tag, String)
            termLookUp.TermName = row.Cells(2).ToolTipText
            termLookUp.Control.Visible = Not String.IsNullOrEmpty(termLookUp.TerminologyName) And Not String.IsNullOrEmpty(termLookUp.QueryName) And Not String.IsNullOrEmpty(termLookUp.Language)

            If termLookUp.Control.Visible And String.IsNullOrEmpty(termLookUp.TermName) Then
                Dim conceptId As String = TryCast(row.Cells(2).Value, String)

                If Not String.IsNullOrEmpty(conceptId) Then
                    Cursor = Cursors.WaitCursor
                    Main.Instance.TerminologyLookup.LoadPreferredTerms(AddressOf PreferredTermsLoaded, AddressOf ShowTerminologyLookupException, termLookUp.TerminologyName, termLookUp.Language, New String() {conceptId})
                End If
            End If
        End If
    End Sub

    Private Sub dgNodeBindings_CurrentCellChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgNodeBindings.CurrentCellChanged
        If Not mFileManager Is Nothing AndAlso Not mFileManager.FileLoading Then
            UpdateTermLookup(dgNodeBindings.CurrentRow)
        End If
    End Sub

    Private Sub dgNodeBindings_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgNodeBindings.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex = 0 And Main.Instance.Options.AllowTerminologyLookUp And Not Main.Instance.TerminologyLookup Is Nothing Then
            Dim form As New TerminologyLookup.TerminologySelectionForm(Main.Instance.TerminologyLookup)
            Main.Instance.TerminologyLookup.Url = Main.Instance.Options.TerminologyUrlString

            Dim row As DataGridViewRow = dgNodeBindings.Rows(e.RowIndex)
            form.TerminologyId = TryCast(row.Cells(0).Value, String)
            form.SubsetId = row.Cells(0).ToolTipText
            form.SubsetLanguage = TryCast(row.Cells(0).Tag, String)
            form.ReferenceId = TryCast(row.Cells(2).Value, String)
            form.ShowDialog(ParentForm)

            If form.DialogResult = DialogResult.OK Then
                mFileManager.OntologyManager.AddTerminology(form.TerminologyId)

                row.Cells(0).Value = form.TerminologyId
                row.Cells(0).ToolTipText = form.SubsetId
                row.Cells(0).Tag = form.SubsetLanguage

                If Not String.IsNullOrEmpty(form.ReferenceId) Then
                    row.Cells(2).Value = form.ReferenceId
                    row.Cells(2).ToolTipText = form.TermText
                End If

                mDataView.AddNew().Delete()     ' Kludge to force the creation of a new empty row in the grid.
                dgNodeBindings.CurrentCell = row.Cells(2)
            End If

            UpdateTermLookup(row)
        End If
    End Sub

    Private Sub dgNodeBindings_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgNodeBindings.CellValueChanged
        If Not dgNodeBindings Is Nothing Then
            Dim cell As DataGridViewCell = dgNodeBindings.CurrentCell

            If Not cell Is Nothing AndAlso cell.ColumnIndex = e.ColumnIndex AndAlso cell.RowIndex = e.RowIndex Then
                If e.ColumnIndex = 2 Then
                    'Check there are no illegal characters in the code string
                    Dim s As String = TryCast(cell.Value, String)

                    If Not s Is Nothing Then
                        Dim ss As String = System.Text.RegularExpressions.Regex.Replace(s, "[\*\(\)\]\[\~\`\!\@\#\$\%\^\&\+\=\""\{\}\|\;\:\?/\<\>\s]", "")

                        If s <> ss Then
                            cell.Value = ss
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub chkListNull_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListNull.ItemCheck
        If Not IsLoading Then
            Dim cp As CodePhrase = CType(mArchetypeNode, ArchetypeElement).RM_Class.ConstrainedNullFlavours
            Dim t As Term = CType(chkListNull.Items(e.Index), Term)

            If e.NewValue = CheckState.Checked Then
                If chkListNull.CheckedItems.Count = chkListNull.Items.Count Then
                    cp.Codes.Clear()
                Else
                    cp.Codes.Add(t.Code)
                End If
            Else
                If cp.Codes.Count > 0 Then
                    cp.Codes.Remove(t.Code)
                Else
                    'Add the checked codes
                    For Each tt As Term In chkListNull.Items
                        If Not tt Is t Then
                            cp.Codes.Add(tt.Code)
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub specialiseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles specialiseButton.Click
        RaiseEvent Specialise(sender, e)
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
'The Original Code is ArchetypeNodeConstraintControl.vb.
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

