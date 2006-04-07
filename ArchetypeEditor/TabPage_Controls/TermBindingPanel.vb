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

Option Strict On


Public Class TermBindingPanel
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            mFileManager = Filemanager.Master


            ' Term Binding data view
            mTermBindingView = New DataView(mFileManager.OntologyManager.TermBindingsTable)
            mTermBindingView.AllowNew = False

            mTermBindingCriteriaView = New DataView(mFileManager.OntologyManager.TermBindingCriteriaTable)
            mTermBindingCriteriaView.AllowNew = False
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
    Friend WithEvents PathsTreeView As System.Windows.Forms.TreeView
    Friend WithEvents AddBindingButton As System.Windows.Forms.Button
    Friend WithEvents BindingImageList As System.Windows.Forms.ImageList
    Friend WithEvents DeleteBindingButton As System.Windows.Forms.Button
    Friend WithEvents AddCriteriaButton As System.Windows.Forms.Button
    Friend WithEvents DeleteCriteriaButton As System.Windows.Forms.Button
    Friend WithEvents BindingCriteriaListBox As System.Windows.Forms.ListBox
    'Friend WithEvents BindingListBox As System.Windows.Forms.ListBox
    Friend WithEvents BindingList As System.Windows.Forms.ListView
    Friend WithEvents BindingOkButton As System.Windows.Forms.Button
    Friend WithEvents ReleaseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NodePathLabel As System.Windows.Forms.Label
    Friend WithEvents PathRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents NodeRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BindingToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents AddBindingGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents AddBindingCriteriaGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BindingGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CancelAddBindingButton As System.Windows.Forms.Button
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents AddTerminologyButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents OperatorComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CriteriaValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CriteriaTermLabel As System.Windows.Forms.Label
    Friend WithEvents BindingCriteriaCancelButton As System.Windows.Forms.Button
    Friend WithEvents CriteriaPathRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents CriteriaNodeRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents CriteriaOkButton As System.Windows.Forms.Button
    Friend WithEvents TerminologyComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents PanelBindings As System.Windows.Forms.Panel
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TermBindingPanel))
        Me.PathsTreeView = New System.Windows.Forms.TreeView
        Me.BindingImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.BindingGroupBox = New System.Windows.Forms.GroupBox
        Me.BindingList = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.DeleteBindingButton = New System.Windows.Forms.Button
        Me.AddBindingButton = New System.Windows.Forms.Button
        Me.DeleteCriteriaButton = New System.Windows.Forms.Button
        Me.BindingCriteriaListBox = New System.Windows.Forms.ListBox
        Me.AddCriteriaButton = New System.Windows.Forms.Button
        Me.AddBindingGroupBox = New System.Windows.Forms.GroupBox
        Me.CancelAddBindingButton = New System.Windows.Forms.Button
        Me.BindingOkButton = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ReleaseTextBox = New System.Windows.Forms.TextBox
        Me.CodeTextBox = New System.Windows.Forms.TextBox
        Me.NodePathLabel = New System.Windows.Forms.Label
        Me.PathRadioButton = New System.Windows.Forms.RadioButton
        Me.NodeRadioButton = New System.Windows.Forms.RadioButton
        Me.AddBindingCriteriaGroupBox = New System.Windows.Forms.GroupBox
        Me.CriteriaPathRadioButton = New System.Windows.Forms.RadioButton
        Me.CriteriaNodeRadioButton = New System.Windows.Forms.RadioButton
        Me.CriteriaTermLabel = New System.Windows.Forms.Label
        Me.OperatorComboBox = New System.Windows.Forms.ComboBox
        Me.BindingCriteriaCancelButton = New System.Windows.Forms.Button
        Me.CriteriaOkButton = New System.Windows.Forms.Button
        Me.CriteriaValueTextBox = New System.Windows.Forms.TextBox
        Me.BindingToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.TopPanel = New System.Windows.Forms.Panel
        Me.AddTerminologyButton = New System.Windows.Forms.Button
        Me.TerminologyComboBox = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.PanelBindings = New System.Windows.Forms.Panel
        Me.gbCriteria = New System.Windows.Forms.GroupBox
        Me.BindingGroupBox.SuspendLayout()
        Me.AddBindingGroupBox.SuspendLayout()
        Me.AddBindingCriteriaGroupBox.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        Me.PanelBindings.SuspendLayout()
        Me.gbCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'PathsTreeView
        '
        Me.PathsTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PathsTreeView.HideSelection = False
        Me.PathsTreeView.ImageList = Me.BindingImageList
        Me.PathsTreeView.Location = New System.Drawing.Point(4, 32)
        Me.PathsTreeView.Name = "PathsTreeView"
        Me.PathsTreeView.PathSeparator = "/"
        Me.PathsTreeView.ShowRootLines = False
        Me.PathsTreeView.Size = New System.Drawing.Size(880, 194)
        Me.PathsTreeView.TabIndex = 1
        '
        'BindingImageList
        '
        Me.BindingImageList.ImageSize = New System.Drawing.Size(20, 20)
        Me.BindingImageList.ImageStream = CType(resources.GetObject("BindingImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BindingImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'BindingGroupBox
        '
        Me.BindingGroupBox.Controls.Add(Me.BindingList)
        Me.BindingGroupBox.Controls.Add(Me.DeleteBindingButton)
        Me.BindingGroupBox.Controls.Add(Me.AddBindingButton)
        Me.BindingGroupBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.BindingGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.BindingGroupBox.Name = "BindingGroupBox"
        Me.BindingGroupBox.Size = New System.Drawing.Size(224, 96)
        Me.BindingGroupBox.TabIndex = 9
        Me.BindingGroupBox.TabStop = False
        Me.BindingGroupBox.Text = "Bindings"
        '
        'BindingList
        '
        Me.BindingList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.BindingList.FullRowSelect = True
        Me.BindingList.HideSelection = False
        Me.BindingList.Location = New System.Drawing.Point(56, 16)
        Me.BindingList.MultiSelect = False
        Me.BindingList.Name = "BindingList"
        Me.BindingList.Size = New System.Drawing.Size(160, 69)
        Me.BindingList.SmallImageList = Me.BindingImageList
        Me.BindingList.TabIndex = 8
        Me.BindingList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Code"
        Me.ColumnHeader1.Width = 156
        '
        'DeleteBindingButton
        '
        Me.DeleteBindingButton.Image = CType(resources.GetObject("DeleteBindingButton.Image"), System.Drawing.Image)
        Me.DeleteBindingButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.DeleteBindingButton.Location = New System.Drawing.Point(24, 49)
        Me.DeleteBindingButton.Name = "DeleteBindingButton"
        Me.DeleteBindingButton.Size = New System.Drawing.Size(22, 22)
        Me.DeleteBindingButton.TabIndex = 7
        Me.DeleteBindingButton.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AddBindingButton
        '
        Me.AddBindingButton.Image = CType(resources.GetObject("AddBindingButton.Image"), System.Drawing.Image)
        Me.AddBindingButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.AddBindingButton.Location = New System.Drawing.Point(24, 22)
        Me.AddBindingButton.Name = "AddBindingButton"
        Me.AddBindingButton.Size = New System.Drawing.Size(22, 22)
        Me.AddBindingButton.TabIndex = 6
        Me.AddBindingButton.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DeleteCriteriaButton
        '
        Me.DeleteCriteriaButton.Image = CType(resources.GetObject("DeleteCriteriaButton.Image"), System.Drawing.Image)
        Me.DeleteCriteriaButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.DeleteCriteriaButton.Location = New System.Drawing.Point(19, 51)
        Me.DeleteCriteriaButton.Name = "DeleteCriteriaButton"
        Me.DeleteCriteriaButton.Size = New System.Drawing.Size(24, 22)
        Me.DeleteCriteriaButton.TabIndex = 10
        '
        'BindingCriteriaListBox
        '
        Me.BindingCriteriaListBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BindingCriteriaListBox.ItemHeight = 16
        Me.BindingCriteriaListBox.Location = New System.Drawing.Point(48, 16)
        Me.BindingCriteriaListBox.Name = "BindingCriteriaListBox"
        Me.BindingCriteriaListBox.Size = New System.Drawing.Size(600, 68)
        Me.BindingCriteriaListBox.TabIndex = 11
        Me.BindingCriteriaListBox.TabStop = False
        '
        'AddCriteriaButton
        '
        Me.AddCriteriaButton.Image = CType(resources.GetObject("AddCriteriaButton.Image"), System.Drawing.Image)
        Me.AddCriteriaButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.AddCriteriaButton.Location = New System.Drawing.Point(20, 22)
        Me.AddCriteriaButton.Name = "AddCriteriaButton"
        Me.AddCriteriaButton.Size = New System.Drawing.Size(22, 22)
        Me.AddCriteriaButton.TabIndex = 9
        '
        'AddBindingGroupBox
        '
        Me.AddBindingGroupBox.Controls.Add(Me.CancelAddBindingButton)
        Me.AddBindingGroupBox.Controls.Add(Me.BindingOkButton)
        Me.AddBindingGroupBox.Controls.Add(Me.Label4)
        Me.AddBindingGroupBox.Controls.Add(Me.Label1)
        Me.AddBindingGroupBox.Controls.Add(Me.ReleaseTextBox)
        Me.AddBindingGroupBox.Controls.Add(Me.CodeTextBox)
        Me.AddBindingGroupBox.Controls.Add(Me.NodePathLabel)
        Me.AddBindingGroupBox.Controls.Add(Me.PathRadioButton)
        Me.AddBindingGroupBox.Controls.Add(Me.NodeRadioButton)
        Me.AddBindingGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AddBindingGroupBox.Location = New System.Drawing.Point(4, 226)
        Me.AddBindingGroupBox.Name = "AddBindingGroupBox"
        Me.AddBindingGroupBox.Size = New System.Drawing.Size(880, 90)
        Me.AddBindingGroupBox.TabIndex = 12
        Me.AddBindingGroupBox.TabStop = False
        Me.AddBindingGroupBox.Text = "Add Binding"
        Me.AddBindingGroupBox.Visible = False
        '
        'CancelAddBindingButton
        '
        Me.CancelAddBindingButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelAddBindingButton.Location = New System.Drawing.Point(728, 61)
        Me.CancelAddBindingButton.Name = "CancelAddBindingButton"
        Me.CancelAddBindingButton.Size = New System.Drawing.Size(64, 20)
        Me.CancelAddBindingButton.TabIndex = 27
        Me.CancelAddBindingButton.Text = "Cancel"
        '
        'BindingOkButton
        '
        Me.BindingOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BindingOkButton.Location = New System.Drawing.Point(808, 61)
        Me.BindingOkButton.Name = "BindingOkButton"
        Me.BindingOkButton.Size = New System.Drawing.Size(64, 20)
        Me.BindingOkButton.TabIndex = 26
        Me.BindingOkButton.Text = "OK"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(216, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Release"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Code"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ReleaseTextBox
        '
        Me.ReleaseTextBox.Location = New System.Drawing.Point(296, 63)
        Me.ReleaseTextBox.Name = "ReleaseTextBox"
        Me.ReleaseTextBox.TabIndex = 25
        Me.ReleaseTextBox.Text = ""
        '
        'CodeTextBox
        '
        Me.CodeTextBox.Location = New System.Drawing.Point(104, 63)
        Me.CodeTextBox.Name = "CodeTextBox"
        Me.CodeTextBox.TabIndex = 24
        Me.CodeTextBox.Text = ""
        '
        'NodePathLabel
        '
        Me.NodePathLabel.Location = New System.Drawing.Point(72, 19)
        Me.NodePathLabel.Name = "NodePathLabel"
        Me.NodePathLabel.Size = New System.Drawing.Size(712, 32)
        Me.NodePathLabel.TabIndex = 26
        '
        'PathRadioButton
        '
        Me.PathRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PathRadioButton.Checked = True
        Me.PathRadioButton.Location = New System.Drawing.Point(8, 40)
        Me.PathRadioButton.Name = "PathRadioButton"
        Me.PathRadioButton.Size = New System.Drawing.Size(56, 16)
        Me.PathRadioButton.TabIndex = 29
        Me.PathRadioButton.TabStop = True
        Me.PathRadioButton.Text = "Path"
        Me.PathRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NodeRadioButton
        '
        Me.NodeRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NodeRadioButton.Location = New System.Drawing.Point(8, 16)
        Me.NodeRadioButton.Name = "NodeRadioButton"
        Me.NodeRadioButton.Size = New System.Drawing.Size(56, 16)
        Me.NodeRadioButton.TabIndex = 28
        Me.NodeRadioButton.Text = "Node"
        Me.NodeRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AddBindingCriteriaGroupBox
        '
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.CriteriaPathRadioButton)
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.CriteriaNodeRadioButton)
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.CriteriaTermLabel)
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.OperatorComboBox)
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.BindingCriteriaCancelButton)
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.CriteriaOkButton)
        Me.AddBindingCriteriaGroupBox.Controls.Add(Me.CriteriaValueTextBox)
        Me.AddBindingCriteriaGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AddBindingCriteriaGroupBox.Location = New System.Drawing.Point(4, 316)
        Me.AddBindingCriteriaGroupBox.Name = "AddBindingCriteriaGroupBox"
        Me.AddBindingCriteriaGroupBox.Size = New System.Drawing.Size(880, 72)
        Me.AddBindingCriteriaGroupBox.TabIndex = 13
        Me.AddBindingCriteriaGroupBox.TabStop = False
        Me.AddBindingCriteriaGroupBox.Text = "Add Binding Criteria"
        Me.AddBindingCriteriaGroupBox.Visible = False
        '
        'CriteriaPathRadioButton
        '
        Me.CriteriaPathRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CriteriaPathRadioButton.Location = New System.Drawing.Point(8, 44)
        Me.CriteriaPathRadioButton.Name = "CriteriaPathRadioButton"
        Me.CriteriaPathRadioButton.Size = New System.Drawing.Size(56, 16)
        Me.CriteriaPathRadioButton.TabIndex = 39
        Me.CriteriaPathRadioButton.Text = "Path"
        Me.CriteriaPathRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CriteriaNodeRadioButton
        '
        Me.CriteriaNodeRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CriteriaNodeRadioButton.Checked = True
        Me.CriteriaNodeRadioButton.Location = New System.Drawing.Point(8, 20)
        Me.CriteriaNodeRadioButton.Name = "CriteriaNodeRadioButton"
        Me.CriteriaNodeRadioButton.Size = New System.Drawing.Size(56, 16)
        Me.CriteriaNodeRadioButton.TabIndex = 38
        Me.CriteriaNodeRadioButton.TabStop = True
        Me.CriteriaNodeRadioButton.Text = "Node"
        Me.CriteriaNodeRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CriteriaTermLabel
        '
        Me.CriteriaTermLabel.BackColor = System.Drawing.Color.White
        Me.CriteriaTermLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CriteriaTermLabel.Location = New System.Drawing.Point(72, 20)
        Me.CriteriaTermLabel.Name = "CriteriaTermLabel"
        Me.CriteriaTermLabel.Size = New System.Drawing.Size(400, 40)
        Me.CriteriaTermLabel.TabIndex = 40
        '
        'OperatorComboBox
        '
        Me.OperatorComboBox.Items.AddRange(New Object() {"=", "<", "<=", ">", ">=", "!="})
        Me.OperatorComboBox.Location = New System.Drawing.Point(488, 20)
        Me.OperatorComboBox.Name = "OperatorComboBox"
        Me.OperatorComboBox.Size = New System.Drawing.Size(64, 24)
        Me.OperatorComboBox.TabIndex = 34
        Me.OperatorComboBox.Text = "="
        '
        'BindingCriteriaCancelButton
        '
        Me.BindingCriteriaCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BindingCriteriaCancelButton.Location = New System.Drawing.Point(728, 44)
        Me.BindingCriteriaCancelButton.Name = "BindingCriteriaCancelButton"
        Me.BindingCriteriaCancelButton.Size = New System.Drawing.Size(64, 20)
        Me.BindingCriteriaCancelButton.TabIndex = 37
        Me.BindingCriteriaCancelButton.Text = "Cancel"
        '
        'CriteriaOkButton
        '
        Me.CriteriaOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CriteriaOkButton.Location = New System.Drawing.Point(808, 44)
        Me.CriteriaOkButton.Name = "CriteriaOkButton"
        Me.CriteriaOkButton.Size = New System.Drawing.Size(64, 20)
        Me.CriteriaOkButton.TabIndex = 36
        Me.CriteriaOkButton.Text = "OK"
        '
        'CriteriaValueTextBox
        '
        Me.CriteriaValueTextBox.Location = New System.Drawing.Point(568, 20)
        Me.CriteriaValueTextBox.Name = "CriteriaValueTextBox"
        Me.CriteriaValueTextBox.Size = New System.Drawing.Size(200, 22)
        Me.CriteriaValueTextBox.TabIndex = 35
        Me.CriteriaValueTextBox.Text = ""
        Me.CriteriaValueTextBox.Visible = False
        '
        'BindingToolTip
        '
        Me.BindingToolTip.AutoPopDelay = 5000
        Me.BindingToolTip.InitialDelay = 1000
        Me.BindingToolTip.ReshowDelay = 100
        Me.BindingToolTip.ShowAlways = True
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.AddTerminologyButton)
        Me.TopPanel.Controls.Add(Me.TerminologyComboBox)
        Me.TopPanel.Controls.Add(Me.Label2)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(4, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(880, 32)
        Me.TopPanel.TabIndex = 14
        '
        'AddTerminologyButton
        '
        Me.AddTerminologyButton.Image = CType(resources.GetObject("AddTerminologyButton.Image"), System.Drawing.Image)
        Me.AddTerminologyButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.AddTerminologyButton.Location = New System.Drawing.Point(584, 8)
        Me.AddTerminologyButton.Name = "AddTerminologyButton"
        Me.AddTerminologyButton.Size = New System.Drawing.Size(22, 22)
        Me.AddTerminologyButton.TabIndex = 13
        '
        'TerminologyComboBox
        '
        Me.TerminologyComboBox.DisplayMember = "Description"
        Me.TerminologyComboBox.Location = New System.Drawing.Point(256, 8)
        Me.TerminologyComboBox.Name = "TerminologyComboBox"
        Me.TerminologyComboBox.Size = New System.Drawing.Size(328, 24)
        Me.TerminologyComboBox.TabIndex = 12
        Me.TerminologyComboBox.ValueMember = "Terminology"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(248, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Binding terminology:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelBindings
        '
        Me.PanelBindings.Controls.Add(Me.gbCriteria)
        Me.PanelBindings.Controls.Add(Me.BindingGroupBox)
        Me.PanelBindings.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBindings.Location = New System.Drawing.Point(4, 388)
        Me.PanelBindings.Name = "PanelBindings"
        Me.PanelBindings.Size = New System.Drawing.Size(880, 96)
        Me.PanelBindings.TabIndex = 15
        '
        'gbCriteria
        '
        Me.gbCriteria.Controls.Add(Me.BindingCriteriaListBox)
        Me.gbCriteria.Controls.Add(Me.AddCriteriaButton)
        Me.gbCriteria.Controls.Add(Me.DeleteCriteriaButton)
        Me.gbCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCriteria.Location = New System.Drawing.Point(224, 0)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(656, 96)
        Me.gbCriteria.TabIndex = 0
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "Criteria"
        Me.gbCriteria.Visible = False
        '
        'TermBindingPanel
        '
        Me.BackColor = System.Drawing.Color.LightYellow
        Me.Controls.Add(Me.PathsTreeView)
        Me.Controls.Add(Me.AddBindingGroupBox)
        Me.Controls.Add(Me.AddBindingCriteriaGroupBox)
        Me.Controls.Add(Me.PanelBindings)
        Me.Controls.Add(Me.TopPanel)
        Me.DockPadding.Bottom = 4
        Me.DockPadding.Left = 4
        Me.DockPadding.Right = 4
        Me.Name = "TermBindingPanel"
        Me.Size = New System.Drawing.Size(888, 488)
        Me.BindingGroupBox.ResumeLayout(False)
        Me.AddBindingGroupBox.ResumeLayout(False)
        Me.AddBindingCriteriaGroupBox.ResumeLayout(False)
        Me.TopPanel.ResumeLayout(False)
        Me.PanelBindings.ResumeLayout(False)
        Me.gbCriteria.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents mTermBindingView As DataView
    Private WithEvents mTermBindingCriteriaView As DataView
    Private mFileManager As FileManagerLocal
    Private mCurrentTermNode As TermNode
    Private mCurrentBindingCriteria As BindingCriteria
    Private mCriteriaMode As Boolean
    Private WithEvents mCriteriaElementView As ElementViewControl
    Private mIsLoading As Boolean
    Private mCriteriaNewRow As DataRow

    Private Sub BindingTerminologyComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TerminologyComboBox.SelectedIndexChanged

        If Not Me.TerminologyComboBox.SelectedValue Is Nothing Then
            'PopulateNodeTree()
            ShowNodesImage(PathsTreeView.Nodes)
            SetTermBindingFilter()
        End If
    End Sub

    Private Function GetTextFromPath(ByVal path As String, ByVal insideSqBrackets As Boolean) As String

        Dim splitNode As String() = path.Split("[]".ToCharArray)

        If splitNode.Length = 3 Then
            If insideSqBrackets Then
                Return splitNode(1)
            Else
                Return splitNode(0)
            End If
        Else
            Return path
        End If

    End Function

    Private Sub PopulateNodeTree()
        Try

            PathsTreeView.SuspendLayout()

            Dim languageCode As String = mFileManager.OntologyManager.LanguageCode

            ' get the paths with the language labels
            Dim logicalPaths As String() = mFileManager.Archetype.Paths(languageCode, mFileManager.ParserSynchronised, True)

            ' get the paths with the NodeId labels
            Dim physicalPaths As String() = mFileManager.Archetype.Paths(languageCode, mFileManager.ParserSynchronised, False)

            For i As Integer = 0 To logicalPaths.Length - 1

                'ignore value paths and empty attributes
                If logicalPaths(i).EndsWith("]") Then
                    Dim clean_s As String = ""


                    ' check there are no enclosed "/" if so replace with "|"
                    For Each c As Char In logicalPaths(i)
                        Dim enclosed As Boolean
                        Select Case c
                            Case "["c
                                enclosed = True

                            Case "]"c
                                enclosed = False

                            Case "/"c
                                If enclosed Then
                                    c = "|"c
                                End If
                        End Select

                        clean_s = clean_s & c
                    Next    ' c

                    Dim y As String() = clean_s.Split("/"c)
                    Dim z As String() = physicalPaths(i).Split("/"c)
                    Dim nodes As TreeNodeCollection = PathsTreeView.Nodes

                    'Ignore the first string as always ""
                    Debug.Assert(y(0) = "", "String is not empty as expected")

                    For j As Integer = 1 To y.Length - 1
                        Dim nodeFound As Boolean = False
                        Dim nodeText As String

                        nodeText = GetTextFromPath(y(j), False)

                        Select Case nodeText.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                            Case "items", "item"
                                nodeText = GetTextFromPath(y(j), True)
                            Case "state", "protocol"
                                ' do nothing - use outer text
                            Case "data"
                                If y(j).IndexOf("history") > -1 Then
                                    nodeText = y(j)
                                End If
                            Case "events"
                                nodeText = y(j)
                        End Select

                        Dim nodeId As String = GetTextFromPath(z(j), True)

                        Dim node As TermNode
                        ' does this node exist elsewhere?
                        For Each node In nodes
                            If nodeId = node.NodeId Then
                                nodeFound = True
                                Exit For
                            End If
                        Next

                        If Not nodeFound Then
                            node = New TermNode(nodeId, nodeText, mFileManager)
                            If nodeText.StartsWith("data") Then
                                nodes.Insert(0, node)
                            ElseIf nodeText.StartsWith("events") Then
                                If y.Length > 3 Then
                                    ' the data node
                                    nodes.Insert(0, node)
                                Else
                                    nodes.Add(node)
                                End If
                            Else
                                nodes.Add(node)
                            End If

                            node.PhysicalPathPart = z(j)

                        End If

                        nodes = node.Nodes

                    Next    ' y index

                End If

            Next    ' LogPath index

            If mIsLoading Then
                PathsTreeView.ExpandAll()
            Else
                ShowNodesImage(PathsTreeView.Nodes)
            End If

            If PathsTreeView.GetNodeCount(False) > 0 Then
                PathsTreeView.SelectedNode = PathsTreeView.Nodes(0)
            End If
            PathsTreeView.ResumeLayout()

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try
    End Sub

    Private Sub ShowNodesImage(ByVal aNodes As TreeNodeCollection)

        Static imageTermBindingDataView As DataView
        If imageTermBindingDataView Is Nothing Then
            imageTermBindingDataView = New DataView(mFileManager.OntologyManager.TermBindingsTable)
        End If

        For Each node As TermNode In aNodes
            node.ImageIndex = 0
            node.SelectedImageIndex = 0

            If Not TerminologyComboBox.SelectedValue Is Nothing Then
                SetTermBindingFilter(imageTermBindingDataView, _
                        CStr(TerminologyComboBox.SelectedValue), _
                        node.NodeTerm, node.PhysicalPath)

                If imageTermBindingDataView.Count > 0 Then
                    node.ImageIndex = 1
                    node.SelectedImageIndex = 1 '2
                End If

            End If

            If Not node.Parent Is Nothing _
        AndAlso node.Parent.ImageIndex <= 0 Then

                node.Parent.ImageIndex = 4
                node.Parent.SelectedImageIndex = 4
            End If

            ShowNodesImage(node.Nodes)
        Next
    End Sub

    Private Sub TermBindingPanel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Me.DesignMode Then
            Try

                Me.TerminologyComboBox.DataSource = mFileManager.OntologyManager.TerminologiesTable

                If PathsTreeView.Nodes.Count = 0 Then
                    PopulateNodeTree()
                End If

                BindingToolTip.SetToolTip(Me.AddBindingButton, "Add term binding")
                BindingToolTip.SetToolTip(Me.DeleteBindingButton, "Remove selected term binding")

            Catch ex As Exception
                Debug.Assert(False, ex.ToString)

            End Try
        End If
    End Sub

    Public Sub PopulatePathTree()
        PathsTreeView.Nodes.Clear()
        mIsLoading = True
        PopulateNodeTree()
        mIsLoading = False
    End Sub

    'Private Sub mFileManager_ArchetypeLoaded(ByVal sender As Object, ByVal e As System.EventArgs) Handles mFileManager.ArchetypeLoaded
    '    PathsTreeView.Nodes.Clear()
    '    mIsLoading = True
    '    PopulateNodeTree()
    '    mIsLoading = False
    '    'PopulateNodeTree(mFileManager.Archetype.Definition.Data)

    'End Sub

    Private Sub NodeScopeCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles PathRadioButton.CheckedChanged, NodeRadioButton.CheckedChanged

        If Not PathsTreeView.SelectedNode Is Nothing Then
            Debug.Assert(Not PathsTreeView.SelectedNode Is Nothing)
            PopulateNewBindingPath()
        End If

        Me.CodeTextBox.Focus()

    End Sub

    Private Sub PathsTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles PathsTreeView.AfterSelect
        Debug.Assert(Not PathsTreeView.SelectedNode Is Nothing)
        'Debug.Assert(TypeOf PathsTreeView.SelectedNode Is TermNode)

        Try
            If Not CriteriaMode Then

                SetTermBindingFilter()

                If AddBindingGroupBox.Visible Then
                    PopulateNewBindingPath()
                End If

            Else
                PopulateCriteriaPath()

                ShowCriteriaElementView()
            End If

            Dim selectedNode As TermNode = CType(PathsTreeView.SelectedNode, TermNode)
            BindingToolTip.SetToolTip(PathsTreeView, selectedNode.PhysicalPath)

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)

        End Try
    End Sub

    Private Sub SetTermBindingFilter()
        Dim selectedNode As TermNode = CType(PathsTreeView.SelectedNode, TermNode)

        SetTermBindingFilter(mTermBindingView, CStr(TerminologyComboBox.SelectedValue), _
                selectedNode.NodeTerm, selectedNode.PhysicalPath)
    End Sub

    Private Sub SetTermBindingFilter(ByVal TermBindingView As DataView, _
            ByVal TerminologyId As String, ByVal NodeId As String, ByVal NodePath As String)

        TermBindingView.RowFilter = String.Format( _
                "Terminology = '{0}' AND (Path = '{1}' OR Path = '{2}')", _
                TerminologyId, NodeId, NodePath)
    End Sub

    Private Sub AddBindingButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddBindingButton.Click

        If Not TerminologyComboBox.SelectedValue Is Nothing Then
            AddBindingGroupBox.Visible = True
            Me.PanelBindings.Enabled = False

            CodeTextBox.Text = ""
            ReleaseTextBox.Text = ""

            PopulateNewBindingPath()
        Else
            MsgBox("Select a terminology")
        End If
    End Sub

    Private Sub PopulateNewBindingPath()
        Debug.Assert(Not PathsTreeView.SelectedNode Is Nothing)
        Debug.Assert(TypeOf PathsTreeView.SelectedNode Is TermNode)

        Try
            Dim selectedNode As TermNode = CType(PathsTreeView.SelectedNode, TermNode)

            If PathRadioButton.Checked Then
                NodePathLabel.Text = selectedNode.PhysicalPath

            Else
                Debug.Assert(NodeRadioButton.Checked, "No node scope option selected")

                NodePathLabel.Text = selectedNode.NodeTerm
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)

        End Try
    End Sub

    Private Sub AddPathToBindings(ByVal aPath As String, ByVal aCode As String, Optional ByVal aCriteria As String = "")

        mCriteriaNewRow = mFileManager.OntologyManager.TermBindingsTable.NewRow
        mCriteriaNewRow(0) = Me.TerminologyComboBox.SelectedValue
        mCriteriaNewRow(1) = aPath
        mCriteriaNewRow("code") = aCode

        If mFileManager.OntologyManager.Ontology.HasTermBinding(CStr(mCriteriaNewRow(0)), aPath) Then
            ' already has this path so add criteria
            If MessageBox.Show("Term has binding, add anyway?", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If
        End If

        mFileManager.OntologyManager.TermBindingsTable.Rows.Add(mCriteriaNewRow)

        mFileManager.FileEdited = True

        Debug.Assert(BindingList.Items.Count > 0, "BindingList not updated")

        If BindingList.Items.Count > 0 Then
            BindingList.Items(BindingList.Items.Count - 1).Selected = True
        End If

    End Sub

    Private Sub RemovePathFromBindings(ByVal aPath As String, ByVal aCode As String, Optional ByVal aCriteria As String = "")

        Dim SelectedRows As DataRow()

        SelectedRows = mFileManager.OntologyManager.TermBindingsTable.Select _
            ("[Terminology]='" & CStr(Me.TerminologyComboBox.SelectedValue) & "'" _
            & " AND [Path]='" & aPath & "'" _
            & " AND [Code]='" & aCode & "'")

        For Each d_row As DataRow In SelectedRows
            d_row.Delete()
        Next

        mFileManager.OntologyManager.TermBindingsTable.AcceptChanges()

        mFileManager.FileEdited = True

        If BindingList.Items.Count > 0 Then
            BindingList.Items(BindingList.Items.Count - 1).Selected = True
        End If
    End Sub

    Private Sub AddTerminologyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles AddTerminologyButton.Click

        Try
            If OceanArchetypeEditor.Instance.AddTerminology() Then
                mFileManager.FileEdited = True

                TerminologyComboBox.SelectedIndex = TerminologyComboBox.Items.Count - 1
                SetTermBindingFilter()
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)

        End Try
    End Sub

    Private Sub BindingList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles BindingList.SelectedIndexChanged

        Debug.Assert(BindingList.SelectedIndices.Count <= 1)
        If BindingList.SelectedIndices.Count = 1 Then
            Dim index As Integer = BindingList.SelectedIndices.Item(0)
            Me.gbCriteria.Visible = True
            If index >= 0 Then
                Dim bindingRowView As DataRowView = mTermBindingView.Item(index)

                ' populate binding criteria
                Dim path As String = CStr(bindingRowView.Item("Path"))
                Dim code As String = CStr(bindingRowView.Item("Code"))

                Me.SetBindingCriteriaFilter(path, code)

                ' set binding list tooltip
                BindingToolTip.SetToolTip(BindingList, path)
            Else
                Debug.Assert(False)
            End If
        Else
            If BindingList.SelectedIndices.Count = 0 Then
                Me.gbCriteria.Visible = False
            End If
            BindingToolTip.SetToolTip(BindingList, "")
        End If
    End Sub

    Private Sub BindingOkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles BindingOkButton.Click

        Debug.Assert(Me.NodePathLabel.Text <> "", "Node path not set")

        If Me.CodeTextBox.Text = "" Then
            MessageBox.Show("Binding code required", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.TerminologyComboBox.SelectedValue Is Nothing Then
            MessageBox.Show("Binding terminology required", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'add "//" if this is a node binding

        If Me.NodeRadioButton.Checked Then
            Me.NodePathLabel.Text = "//" & Me.NodePathLabel.Text
        End If

        If mFileManager.OntologyManager.hasTermBinding(CStr(TerminologyComboBox.SelectedValue), CodeTextBox.Text, NodePathLabel.Text) Then
            If MessageBox.Show(AE_Constants.Instance.Must_add_criteria, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = DialogResult.OK Then
                AddBindingGroupBox.Visible = False
                AddCriteriaButton_Click(sender, e)
                mCriteriaMode = True
            Else
                Me.PanelBindings.Enabled = True
                AddBindingGroupBox.Visible = False
            End If
        Else
            Try
                AddPathToBindings(NodePathLabel.Text, CodeTextBox.Text)

                ShowNodesImage(PathsTreeView.Nodes)

                Me.PanelBindings.Enabled = True
                AddBindingGroupBox.Visible = False

            Catch ex As Exception

                Debug.Assert(False, ex.ToString)
                MessageBox.Show(AE_Constants.Instance.Duplicate_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub mTermBindingView_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) _
            Handles mTermBindingView.ListChanged

        PopulateBindingList()
    End Sub

    Private Sub PopulateBindingList()
        BindingList.Items.Clear()

        For Each bindingRow As DataRowView In mTermBindingView

            Dim imageIndex As Integer
            If bindingRow.Item("Path").ToString.IndexOf("/") > 0 Then
                ' Path
                imageIndex = 6
            Else
                ' Node
                imageIndex = 5
            End If

            BindingList.Items.Add(CStr(bindingRow.Item("code")), imageIndex)

        Next

        If BindingList.Items.Count > 0 Then
            BindingList.Items(0).Selected = True

        Else
            BindingToolTip.SetToolTip(BindingList, "")
            ' hide the criteria as there are no nodes
            Me.gbCriteria.Visible = False
            SetBindingCriteriaFilter("", "")
        End If

    End Sub

    Private Sub SetBindingCriteriaFilter(ByVal Path As String, ByVal Code As String)
        '    Called during load before terminologyComboBox has a selected value so...

        If Not TerminologyComboBox.SelectedValue Is Nothing Then
            mTermBindingCriteriaView.RowFilter = String.Format( _
                "Terminology = '{0}' AND Path = '{1}' AND Code = '{2}'", _
                CStr(TerminologyComboBox.SelectedValue), Path, Code)
        End If
    End Sub

    Private Sub CancelAddBindingButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles CancelAddBindingButton.Click

        Me.PanelBindings.Enabled = True
        AddBindingGroupBox.Visible = False

    End Sub

    Private Sub DeleteBindingButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles DeleteBindingButton.Click

        Debug.Assert(BindingList.SelectedIndices.Count <= 1)
        If BindingList.SelectedIndices.Count = 1 Then

            Dim index As Integer = BindingList.SelectedIndices.Item(0)
            If index >= 0 Then
                Dim a_code As String = CStr(mTermBindingView.Item(index).Item("Code"))
                Dim a_path As String = CStr(mTermBindingView.Item(index).Item("Path"))
                If mFileManager.OntologyManager.hasTermBinding(CStr(TerminologyComboBox.SelectedValue), a_code, a_path) Then
                    Try
                        RemovePathFromBindings(a_path, a_code)
                    Catch ex As Exception
                        Debug.Assert(False, ex.ToString)
                        MessageBox.Show(AE_Constants.Instance.Error_saving, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If

            ShowNodesImage(PathsTreeView.Nodes)

        End If
    End Sub

    Private Sub AddCriteriaButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles AddCriteriaButton.Click

        If BindingList.SelectedIndices.Count > 0 Then
            'Preconditions
            Debug.Assert(Not PathsTreeView.SelectedNode Is Nothing)
            Debug.Assert(TypeOf PathsTreeView.SelectedNode Is TermNode)

            'Empty the criteria text box
            CriteriaValueTextBox.Text = ""

            'Make the right panel visible
            AddBindingCriteriaGroupBox.Visible = True
            Me.PanelBindings.Enabled = False

            mCurrentTermNode = CType(PathsTreeView.SelectedNode, TermNode)

            mCurrentBindingCriteria = New BindingCriteria(mFileManager)

            PopulateCriteriaPath()

            mCriteriaMode = True

            ShowCriteriaElementView()
        Else
            MsgBox("Select a term binding")
        End If
    End Sub

    Private ReadOnly Property CriteriaMode() As Boolean
        Get
            Return AddBindingCriteriaGroupBox.Visible
        End Get
    End Property

    Private Sub BindingCriteriaCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingCriteriaCancelButton.Click
        AddBindingCriteriaGroupBox.Visible = False
        Me.PanelBindings.Enabled = True
        mCriteriaMode = False
        PathsTreeView.SelectedNode = mCurrentTermNode
    End Sub

    Private Sub CriteriaOkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CriteriaOkButton.Click

        mCurrentBindingCriteria.Operator = CStr(OperatorComboBox.Text)
        mCurrentBindingCriteria.ValueOperand = CStr(CriteriaValueTextBox.Tag)
        If mCriteriaMode Then
            ' have to add the binding and the criteria
            AddBindingCriteria()
            AddPathToBindings(Me.CodeTextBox.Text & "{" & mCurrentBindingCriteria.ToPhysicalCriteria & "}", BindingList.SelectedItems(0).Text)
            ShowNodesImage(PathsTreeView.Nodes)
        End If
        BindingCriteriaCancelButton_Click(sender, e)

    End Sub

    Private Sub PopulateCriteriaPath()
        Try
            Dim selectedNode As TermNode = CType(PathsTreeView.SelectedNode, TermNode)

            If CriteriaPathRadioButton.Checked Then
                mCurrentBindingCriteria.NodeOperand = selectedNode.PhysicalPath
            Else
                Debug.Assert(CriteriaNodeRadioButton.Checked, "No criteria scope option selected")

                mCurrentBindingCriteria.NodeOperand = selectedNode.NodeTerm
            End If

            CriteriaTermLabel.Text = mCurrentBindingCriteria.NodeText

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try
    End Sub

    Private Sub ShowCriteriaElementView()
        Dim selectedNode As TermNode = CType(PathsTreeView.SelectedNode, TermNode)
        Dim termConstraint As Constraint = selectedNode.Constraint

        If Not mCriteriaElementView Is Nothing Then
            AddBindingCriteriaGroupBox.Controls.Remove(mCriteriaElementView)
        End If

        If Not termConstraint Is Nothing Then
            mCriteriaElementView = ArchetypeView.ElementView(termConstraint, mFileManager)
            mCriteriaElementView.Location = Me.CriteriaValueTextBox.Location 'New Point(520, 64)
            AddBindingCriteriaGroupBox.Controls.Add(mCriteriaElementView)
        End If
    End Sub

    Private Sub CriteriaNodeRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles CriteriaNodeRadioButton.CheckedChanged, CriteriaPathRadioButton.CheckedChanged

        If Not PathsTreeView.SelectedNode Is Nothing Then
            'Debug.Assert(Not PathsTreeView.SelectedNode Is Nothing)
            PopulateCriteriaPath()
        End If
    End Sub

    Private Sub AddBindingCriteria()
        Try
            Dim bindingRowView As DataRowView = mTermBindingView.Item(BindingList.SelectedIndices.Item(0))

            ' populate binding criteria
            Dim path As String = CStr(bindingRowView.Item("Path"))
            Dim terminology As String = CStr(bindingRowView.Item("Terminology"))
            Dim code As String = CStr(bindingRowView.Item("Code"))

            Dim newRow As DataRow = mFileManager.OntologyManager.TermBindingCriteriaTable.NewRow
            newRow(0) = terminology
            newRow(1) = path
            newRow("code") = code
            newRow("Criteria") = mCurrentBindingCriteria.ToPhysicalCriteria

            mFileManager.OntologyManager.TermBindingCriteriaTable.Rows.Add(newRow)

            mFileManager.FileEdited = True

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try
    End Sub

    Public Sub Translate()
        Debug.Assert(False, "Not done yet")
    End Sub

    Private Sub TranslateGUI()
        Debug.Assert(False, "Not done yet")
    End Sub

    Private Sub DeleteCriteriaButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteCriteriaButton.Click
        'Debug.Assert(BindingCriteriaListBox.SelectedIndex >= 0)
        If BindingCriteriaListBox.SelectedIndex >= 0 Then

            mTermBindingCriteriaView.Item(BindingCriteriaListBox.SelectedIndex).Delete()

        Else
            MsgBox("Select a binding criteria to remove")
        End If

    End Sub

    Private Sub mTermBindingCriteriaView_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles mTermBindingCriteriaView.ListChanged

        PopulateBindingCriteriaList()

    End Sub

    Private Sub PopulateBindingCriteriaList()
        Try
            BindingCriteriaListBox.Items.Clear()

            For Each criteriaRow As DataRowView In mTermBindingCriteriaView

                Dim criteriaExpression As String = CStr(criteriaRow.Item("criteria"))
                Dim criteria As New BindingCriteria(criteriaExpression, mFileManager)

                BindingCriteriaListBox.Items.Add(criteria)

            Next

            BindingToolTip.SetToolTip(BindingCriteriaListBox, "")

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try
    End Sub

    Private Sub CriteriaValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mCriteriaElementView.ValueChanged

        CriteriaValueTextBox.Text = CStr(mCriteriaElementView.Value)
        CriteriaValueTextBox.Tag = mCriteriaElementView.Tag
    End Sub

    Class TermNode : Inherits TreeNode
        Private mFileManager As FileManagerLocal

        Public Sub New(ByVal aNodeId As String, ByVal aText As String, ByVal a_filemanager As FileManagerLocal)

            MyBase.New(aText)
            mFileManager = a_filemanager
            Me.NodeId = aNodeId

            'If NodeTerm <> "structure" Then
            '    Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(NodeTerm)
            '    If term.Text <> "" Then
            '        MyBase.Text = term.Text
            '    End If
            'End If

        End Sub

        Private mNodeId As String
        Property NodeId() As String
            Get
                Return mNodeId
            End Get
            Set(ByVal Value As String)
                mNodeId = Value
            End Set
        End Property

        Property PhysicalPathPart() As String
            Get
                Return CStr(MyBase.Tag)
            End Get
            Set(ByVal Value As String)
                MyBase.Tag = Value
            End Set
        End Property

        ReadOnly Property NodeTerm() As String
            Get
                Return mNodeId
            End Get
        End Property

        Shadows ReadOnly Property Parent() As TermNode
            Get
                If Not MyBase.Parent Is Nothing Then
                    Debug.Assert(TypeOf MyBase.Parent Is TermNode, "TermNode expected")

                    Return CType(MyBase.Parent, TermNode)
                End If
            End Get
        End Property

        ReadOnly Property LogicalPath() As String
            Get
                Return MyBase.FullPath
            End Get
        End Property

        Public Function PhysicalPath() As String
            If Me.Parent Is Nothing Then
                Return "/" & Me.PhysicalPathPart
            Else
                Return Me.Parent.PhysicalPath & "/" & Me.PhysicalPathPart
            End If
        End Function

        'Public Sub Translate()
        '    Dim term As Term = mFileManager.OntologyManager.GetTerm(mNodeId)
        '    MyBase.Text = term.Text
        '    'sDescription = term.Description
        '    'term = mFileManager.OntologyManager.GetTerm(Item.RuntimeName)
        '    'sConstraintText = term.Text
        '    For Each node As TermNode In Me.Nodes
        '        node.Translate()
        '    Next
        'End Sub

        ReadOnly Property Constraint() As Constraint
            Get
                Dim rmClass As RmStructure
                rmClass = mFileManager.Archetype.Definition.GetChildByNodeId(NodeTerm)

                If TypeOf rmClass Is RmElement Then
                    Return CType(rmClass, RmElement).Constraint
                End If
            End Get
        End Property

        Public Function Copy() As TermNode
            Dim tn As New TermNode(Me.mNodeId, Me.Text, Me.mFileManager)
            tn.Tag = Me.Tag
            Return tn
        End Function

    End Class

    Public Class BindingCriteria
        Public NodeOperand As String
        Public Operator As String = "="
        Public ValueOperand As String
        Private mFilemanager As FileManagerLocal

        Public ReadOnly Property NodeText() As String
            Get
                If NodeOperand.IndexOf("/") > -1 Then
                    Dim path As String = NodeOperand.TrimEnd("/"c)
                    Dim pathParts() As String = path.Split("/"c)

                    path = ""
                    For Each part As String In pathParts
                        Dim i As Integer = part.IndexOf("[") + 1
                        If i > 0 Then
                            Dim j As Integer = part.IndexOf("]")
                            Debug.Assert(j > 0)
                            Dim partTerm As String = part.Substring(i, j - i)

                            Dim term As RmTerm = mFilemanager.OntologyManager.GetTerm(partTerm)
                            If term.Text <> "" Then
                                part = term.Text
                            Else
                                ' TODO: what are valid values/mappings here?
                                part = part.Substring(0, i - 1)
                            End If

                        Else
                            ' TODO: what are valid values/mappings here?
                            part = "structure"
                        End If

                        path &= "/" & part
                    Next

                    Return path

                Else
                    If mFilemanager.OntologyManager.Ontology.HasTermCode(NodeOperand) Then
                        Dim nodeTerm As RmTerm = mFilemanager.OntologyManager.GetTerm(NodeOperand)

                        Return nodeTerm.Text
                    Else
                        Return NodeOperand
                    End If
                End If
            End Get
        End Property

        Public BooleanOperator As String = "OR"

        Public Overrides Function ToString() As String
            Return ToLogicalCriteria()
        End Function

        Public Function ToPhysicalCriteria() As String
            ' Computerable criteria expression
            Dim criteria As String = String.Format("{0} {1} {2}", NodeOperand, _
                    Operator, ValueOperand)

            Return criteria
        End Function

        Public Function ToLogicalCriteria() As String
            ' Readable criteria expression
            Dim criteria As String = String.Format("{0} {1} {2}", NodeText, _
            Operator, ValueText)

            Return criteria

        End Function

        Public ReadOnly Property ValueText() As String
            Get
                Dim nodeId As String = NodeOperand
                If nodeId.IndexOf("/") > 0 Then
                    'Debug.Assert(False)

                    Dim path As String = nodeId.TrimEnd("/"c)
                    Dim pathParts() As String = path.Split("/"c)
                    nodeId = pathParts(pathParts.Length - 1)

                    Dim i As Integer = nodeId.IndexOf("[") + 1
                    Debug.Assert(i > 0)

                    Dim j As Integer = nodeId.IndexOf("]")
                    Debug.Assert(j > 0)

                    nodeId = nodeId.Substring(i, j - i)

                End If

                Dim rmClass As RmStructure
                rmClass = mFilemanager.Archetype.Definition.Data.GetChildByNodeId(nodeId)

                Debug.Assert(TypeOf rmClass Is RmElement)
                Dim valueConstraint As Constraint = CType(rmClass, RmElement).Constraint

                Select Case valueConstraint.Type
                    Case ConstraintType.Ordinal
                        Dim ordinalConstraint As Constraint_Ordinal = CType(valueConstraint, Constraint_Ordinal)
                        Dim rows() As DataRow = ordinalConstraint.OrdinalValues.Select("Ordinal = " & ValueOperand)
                        Debug.Assert(rows.Length = 1)
                        Dim ordinal As New OrdinalValue(rows(0))

                        Return ordinal.Text

                    Case Else
                        Return ValueOperand

                End Select
            End Get
        End Property

        Public Sub New(ByVal a_filemanager As FileManagerLocal)
            mFilemanager = a_filemanager
        End Sub

        Public Sub New(ByVal CriteriaExpression As String, ByVal a_filemanager As FileManagerLocal)
            mFilemanager = a_filemanager
            Dim criteriaParts() As String = CriteriaExpression.Split(" "c)
            Debug.Assert(criteriaParts.Length >= 3)

            NodeOperand = criteriaParts(0)
            Operator = criteriaParts(1)
            'ValueOperand = criteriaParts(2)
            ValueOperand = String.Join(" ", criteriaParts, 2, criteriaParts.Length - 2)
        End Sub
    End Class

    Private Sub AddBindingGroupBox_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddBindingGroupBox.VisibleChanged
        If Me.AddBindingGroupBox.Visible Then
            Me.CodeTextBox.Focus()
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
'The Original Code is TermBindingPanel.vb.
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

