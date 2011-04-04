'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/GUI_Classes/Structure_Controls/SCCS/s.EntryStructure.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 

Public Class EntryStructure
    Inherits System.Windows.Forms.UserControl

    Private mStructureType As StructureType   'implement as overrided property
    Protected OKtoEditSpecialisation As Boolean
    Protected mNodeId As String
    Protected mIsState As Boolean
    Protected mConstraintMenu As ConstraintContextMenu
    Protected mOrdinalTable As DataTable
    Protected mCurrentItem As ArchetypeNode
    Protected mFileManager As FileManagerLocal
    Protected mCardinalityControl As OccurrencesPanel
    Protected WithEvents menuChangeStructure As System.Windows.Forms.MenuItem

    'implement as overrided property
    Protected mControl As Control  ' the GUI control in the inherited class e.g. tree, text etc

    'Protected mDragArchetypeNode As ArchetypeNode
    Protected mNewConstraint As Constraint
    Friend WithEvents pbSlot As System.Windows.Forms.PictureBox
    Protected mNewCluster As Boolean = False
    Public Event CurrentItemChanged(ByVal sender As ArchetypeNode, ByVal e As EventArgs)
    Public Event ChangeStructure(ByVal sender As Object, ByVal e As EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal rm As RmElement, ByVal fileManager As FileManagerLocal)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mNodeId = rm.NodeId
        mFileManager = fileManager
        mStructureType = rm.Type
        SetHelpTopic(StructureType.Single)
        ShowIcons()
    End Sub

    Public Sub New(ByVal rm As RmStructureCompound, ByVal fileManager As FileManagerLocal)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mNodeId = rm.NodeId
        mFileManager = fileManager
        mStructureType = rm.Type

        Select Case rm.Type
            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                SetHelpTopic(mStructureType)
                menuChangeStructure = New System.Windows.Forms.MenuItem(AE_Constants.Instance.ChangeStructure)
            Case StructureType.Cluster
                SetHelpTopic(StructureType.Tree)
            Case Else
                Debug.Assert(False)
        End Select

        ShowIcons()
        SetCardinality(rm)
    End Sub

    Public Sub New(ByVal structureAsString As String, ByVal fileManager As FileManagerLocal)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mFileManager = fileManager

        ' also sets the node Id if it is not already set
        mStructureType = System.Enum.Parse(StructureType.GetType, structureAsString)

        Select Case mStructureType
            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                SetHelpTopic(mStructureType)
            Case Else
                Debug.Assert(False)
        End Select

        menuChangeStructure = New System.Windows.Forms.MenuItem(AE_Constants.Instance.ChangeStructure)
        ShowIcons()
        SetCardinality(mStructureType)
    End Sub

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
    Private components As System.ComponentModel.IContainer

    'Required by the Windows Form Designer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PanelIcons As System.Windows.Forms.Panel
    Friend WithEvents ButAddElement As System.Windows.Forms.Button
    Friend WithEvents butRemoveElement As System.Windows.Forms.Button
    Friend WithEvents butListUp As System.Windows.Forms.Button
    Friend WithEvents butListDown As System.Windows.Forms.Button
    Friend WithEvents pbText As System.Windows.Forms.PictureBox
    Friend WithEvents pbQuantity As System.Windows.Forms.PictureBox
    Friend WithEvents pbCount As System.Windows.Forms.PictureBox
    Friend WithEvents pbDateTime As System.Windows.Forms.PictureBox
    Friend WithEvents pbOrdinal As System.Windows.Forms.PictureBox
    Friend WithEvents pbBoolean As System.Windows.Forms.PictureBox
    Friend WithEvents pbAny As System.Windows.Forms.PictureBox
    Friend WithEvents pbCluster As System.Windows.Forms.PictureBox
    Friend WithEvents butChangeDataType As System.Windows.Forms.Button
    Friend WithEvents PanelStructureHeader As System.Windows.Forms.Panel
    Friend WithEvents ilSmall As System.Windows.Forms.ImageList
    Friend WithEvents ttElement As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTipSpecialisation As System.Windows.Forms.ToolTip
    Friend WithEvents helpEntryStructure As System.Windows.Forms.HelpProvider
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents lblAtcode As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntryStructure))
        Me.PanelIcons = New System.Windows.Forms.Panel
        Me.ButAddElement = New System.Windows.Forms.Button
        Me.butRemoveElement = New System.Windows.Forms.Button
        Me.butListUp = New System.Windows.Forms.Button
        Me.butListDown = New System.Windows.Forms.Button
        Me.pbText = New System.Windows.Forms.PictureBox
        Me.pbQuantity = New System.Windows.Forms.PictureBox
        Me.pbCount = New System.Windows.Forms.PictureBox
        Me.pbDateTime = New System.Windows.Forms.PictureBox
        Me.pbOrdinal = New System.Windows.Forms.PictureBox
        Me.pbBoolean = New System.Windows.Forms.PictureBox
        Me.pbAny = New System.Windows.Forms.PictureBox
        Me.pbCluster = New System.Windows.Forms.PictureBox
        Me.butChangeDataType = New System.Windows.Forms.Button
        Me.pbSlot = New System.Windows.Forms.PictureBox
        Me.PanelStructureHeader = New System.Windows.Forms.Panel
        Me.lblAtcode = New System.Windows.Forms.Label
        Me.ilSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.ttElement = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTipSpecialisation = New System.Windows.Forms.ToolTip(Me.components)
        Me.helpEntryStructure = New System.Windows.Forms.HelpProvider
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelIcons.SuspendLayout()
        CType(Me.pbText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbOrdinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBoolean, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAny, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCluster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSlot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelStructureHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelIcons
        '
        Me.PanelIcons.Controls.Add(Me.ButAddElement)
        Me.PanelIcons.Controls.Add(Me.butRemoveElement)
        Me.PanelIcons.Controls.Add(Me.butListUp)
        Me.PanelIcons.Controls.Add(Me.butListDown)
        Me.PanelIcons.Controls.Add(Me.pbText)
        Me.PanelIcons.Controls.Add(Me.pbQuantity)
        Me.PanelIcons.Controls.Add(Me.pbCount)
        Me.PanelIcons.Controls.Add(Me.pbDateTime)
        Me.PanelIcons.Controls.Add(Me.pbOrdinal)
        Me.PanelIcons.Controls.Add(Me.pbBoolean)
        Me.PanelIcons.Controls.Add(Me.pbAny)
        Me.PanelIcons.Controls.Add(Me.pbCluster)
        Me.PanelIcons.Controls.Add(Me.butChangeDataType)
        Me.PanelIcons.Controls.Add(Me.pbSlot)
        Me.PanelIcons.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelIcons.Location = New System.Drawing.Point(0, 27)
        Me.PanelIcons.Name = "PanelIcons"
        Me.PanelIcons.Size = New System.Drawing.Size(40, 379)
        Me.PanelIcons.TabIndex = 1
        '
        'ButAddElement
        '
        Me.ButAddElement.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ButAddElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButAddElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.helpEntryStructure.SetHelpNavigator(Me.ButAddElement, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButAddElement.Image = CType(resources.GetObject("ButAddElement.Image"), System.Drawing.Image)
        Me.ButAddElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButAddElement.Location = New System.Drawing.Point(8, 4)
        Me.ButAddElement.Name = "ButAddElement"
        Me.helpEntryStructure.SetShowHelp(Me.ButAddElement, True)
        Me.ButAddElement.Size = New System.Drawing.Size(24, 25)
        Me.ButAddElement.TabIndex = 0
        Me.ButAddElement.UseVisualStyleBackColor = False
        '
        'butRemoveElement
        '
        Me.butRemoveElement.BackColor = System.Drawing.Color.LightSteelBlue
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(8, 31)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveElement.TabIndex = 1
        Me.butRemoveElement.UseVisualStyleBackColor = False
        '
        'butListUp
        '
        Me.butListUp.BackColor = System.Drawing.Color.LightSteelBlue
        Me.butListUp.Image = CType(resources.GetObject("butListUp.Image"), System.Drawing.Image)
        Me.butListUp.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butListUp.Location = New System.Drawing.Point(8, 58)
        Me.butListUp.Name = "butListUp"
        Me.butListUp.Size = New System.Drawing.Size(24, 25)
        Me.butListUp.TabIndex = 2
        Me.butListUp.UseVisualStyleBackColor = False
        '
        'butListDown
        '
        Me.butListDown.BackColor = System.Drawing.Color.LightSteelBlue
        Me.butListDown.Image = CType(resources.GetObject("butListDown.Image"), System.Drawing.Image)
        Me.butListDown.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butListDown.Location = New System.Drawing.Point(8, 85)
        Me.butListDown.Name = "butListDown"
        Me.butListDown.Size = New System.Drawing.Size(24, 25)
        Me.butListDown.TabIndex = 3
        Me.butListDown.UseVisualStyleBackColor = False
        '
        'pbText
        '
        Me.pbText.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbText.Image = CType(resources.GetObject("pbText.Image"), System.Drawing.Image)
        Me.pbText.Location = New System.Drawing.Point(8, 112)
        Me.pbText.Name = "pbText"
        Me.pbText.Size = New System.Drawing.Size(24, 25)
        Me.pbText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbText.TabIndex = 32
        Me.pbText.TabStop = False
        '
        'pbQuantity
        '
        Me.pbQuantity.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbQuantity.Image = CType(resources.GetObject("pbQuantity.Image"), System.Drawing.Image)
        Me.pbQuantity.Location = New System.Drawing.Point(8, 139)
        Me.pbQuantity.Name = "pbQuantity"
        Me.pbQuantity.Size = New System.Drawing.Size(24, 25)
        Me.pbQuantity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbQuantity.TabIndex = 33
        Me.pbQuantity.TabStop = False
        '
        'pbCount
        '
        Me.pbCount.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbCount.Image = CType(resources.GetObject("pbCount.Image"), System.Drawing.Image)
        Me.pbCount.Location = New System.Drawing.Point(8, 166)
        Me.pbCount.Name = "pbCount"
        Me.pbCount.Size = New System.Drawing.Size(24, 25)
        Me.pbCount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbCount.TabIndex = 39
        Me.pbCount.TabStop = False
        '
        'pbDateTime
        '
        Me.pbDateTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbDateTime.Image = CType(resources.GetObject("pbDateTime.Image"), System.Drawing.Image)
        Me.pbDateTime.Location = New System.Drawing.Point(8, 193)
        Me.pbDateTime.Name = "pbDateTime"
        Me.pbDateTime.Size = New System.Drawing.Size(24, 25)
        Me.pbDateTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbDateTime.TabIndex = 34
        Me.pbDateTime.TabStop = False
        '
        'pbOrdinal
        '
        Me.pbOrdinal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbOrdinal.Image = CType(resources.GetObject("pbOrdinal.Image"), System.Drawing.Image)
        Me.pbOrdinal.Location = New System.Drawing.Point(8, 220)
        Me.pbOrdinal.Name = "pbOrdinal"
        Me.pbOrdinal.Size = New System.Drawing.Size(24, 25)
        Me.pbOrdinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbOrdinal.TabIndex = 35
        Me.pbOrdinal.TabStop = False
        '
        'pbBoolean
        '
        Me.pbBoolean.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbBoolean.Image = CType(resources.GetObject("pbBoolean.Image"), System.Drawing.Image)
        Me.pbBoolean.Location = New System.Drawing.Point(8, 247)
        Me.pbBoolean.Name = "pbBoolean"
        Me.pbBoolean.Size = New System.Drawing.Size(24, 25)
        Me.pbBoolean.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBoolean.TabIndex = 37
        Me.pbBoolean.TabStop = False
        '
        'pbAny
        '
        Me.pbAny.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbAny.Image = CType(resources.GetObject("pbAny.Image"), System.Drawing.Image)
        Me.pbAny.Location = New System.Drawing.Point(8, 274)
        Me.pbAny.Name = "pbAny"
        Me.pbAny.Size = New System.Drawing.Size(24, 25)
        Me.pbAny.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbAny.TabIndex = 36
        Me.pbAny.TabStop = False
        '
        'pbCluster
        '
        Me.pbCluster.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbCluster.Image = CType(resources.GetObject("pbCluster.Image"), System.Drawing.Image)
        Me.pbCluster.Location = New System.Drawing.Point(8, 301)
        Me.pbCluster.Name = "pbCluster"
        Me.pbCluster.Size = New System.Drawing.Size(24, 25)
        Me.pbCluster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbCluster.TabIndex = 38
        Me.pbCluster.TabStop = False
        '
        'butChangeDataType
        '
        Me.helpEntryStructure.SetHelpKeyword(Me.butChangeDataType, "HowTo/Editing/change_datatype.htm")
        Me.helpEntryStructure.SetHelpNavigator(Me.butChangeDataType, System.Windows.Forms.HelpNavigator.Topic)
        Me.butChangeDataType.Image = CType(resources.GetObject("butChangeDataType.Image"), System.Drawing.Image)
        Me.butChangeDataType.Location = New System.Drawing.Point(8, 354)
        Me.butChangeDataType.Name = "butChangeDataType"
        Me.helpEntryStructure.SetShowHelp(Me.butChangeDataType, True)
        Me.butChangeDataType.Size = New System.Drawing.Size(24, 25)
        Me.butChangeDataType.TabIndex = 4
        Me.butChangeDataType.Visible = False
        '
        'pbSlot
        '
        Me.pbSlot.Image = CType(resources.GetObject("pbSlot.Image"), System.Drawing.Image)
        Me.pbSlot.Location = New System.Drawing.Point(8, 328)
        Me.pbSlot.Name = "pbSlot"
        Me.pbSlot.Size = New System.Drawing.Size(24, 25)
        Me.pbSlot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbSlot.TabIndex = 40
        Me.pbSlot.TabStop = False
        '
        'PanelStructureHeader
        '
        Me.PanelStructureHeader.Controls.Add(Me.lblAtcode)
        Me.PanelStructureHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelStructureHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelStructureHeader.Name = "PanelStructureHeader"
        Me.PanelStructureHeader.Size = New System.Drawing.Size(384, 24)
        Me.PanelStructureHeader.TabIndex = 0
        '
        'lblAtcode
        '
        Me.lblAtcode.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblAtcode.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAtcode.Location = New System.Drawing.Point(312, 0)
        Me.lblAtcode.Name = "lblAtcode"
        Me.lblAtcode.Size = New System.Drawing.Size(72, 24)
        Me.lblAtcode.TabIndex = 0
        Me.lblAtcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.ilSmall.Images.SetKeyName(34, "")
        Me.ilSmall.Images.SetKeyName(35, "")
        Me.ilSmall.Images.SetKeyName(36, "")
        Me.ilSmall.Images.SetKeyName(37, "")
        Me.ilSmall.Images.SetKeyName(38, "")
        Me.ilSmall.Images.SetKeyName(39, "")
        Me.ilSmall.Images.SetKeyName(40, "")
        Me.ilSmall.Images.SetKeyName(41, "")
        Me.ilSmall.Images.SetKeyName(42, "")
        Me.ilSmall.Images.SetKeyName(43, "")
        Me.ilSmall.Images.SetKeyName(44, "")
        Me.ilSmall.Images.SetKeyName(45, "")
        Me.ilSmall.Images.SetKeyName(46, "")
        Me.ilSmall.Images.SetKeyName(47, "")
        Me.ilSmall.Images.SetKeyName(48, "")
        Me.ilSmall.Images.SetKeyName(49, "")
        Me.ilSmall.Images.SetKeyName(50, "")
        Me.ilSmall.Images.SetKeyName(51, "")
        Me.ilSmall.Images.SetKeyName(52, "")
        Me.ilSmall.Images.SetKeyName(53, "")
        Me.ilSmall.Images.SetKeyName(54, "")
        Me.ilSmall.Images.SetKeyName(55, "")
        Me.ilSmall.Images.SetKeyName(56, "")
        Me.ilSmall.Images.SetKeyName(57, "")
        Me.ilSmall.Images.SetKeyName(58, "")
        Me.ilSmall.Images.SetKeyName(59, "")
        Me.ilSmall.Images.SetKeyName(60, "")
        Me.ilSmall.Images.SetKeyName(61, "")
        Me.ilSmall.Images.SetKeyName(62, "")
        Me.ilSmall.Images.SetKeyName(63, "")
        Me.ilSmall.Images.SetKeyName(64, "")
        Me.ilSmall.Images.SetKeyName(65, "")
        Me.ilSmall.Images.SetKeyName(66, "")
        Me.ilSmall.Images.SetKeyName(67, "")
        Me.ilSmall.Images.SetKeyName(68, "")
        Me.ilSmall.Images.SetKeyName(69, "")
        Me.ilSmall.Images.SetKeyName(70, "")
        Me.ilSmall.Images.SetKeyName(71, "")
        Me.ilSmall.Images.SetKeyName(72, "")
        Me.ilSmall.Images.SetKeyName(73, "")
        Me.ilSmall.Images.SetKeyName(74, "")
        Me.ilSmall.Images.SetKeyName(75, "")
        Me.ilSmall.Images.SetKeyName(76, "")
        Me.ilSmall.Images.SetKeyName(77, "")
        Me.ilSmall.Images.SetKeyName(78, "")
        Me.ilSmall.Images.SetKeyName(79, "")
        '
        'ToolTipSpecialisation
        '
        Me.ToolTipSpecialisation.AutoPopDelay = 5000
        Me.ToolTipSpecialisation.InitialDelay = 500
        Me.ToolTipSpecialisation.ReshowDelay = 100
        '
        'helpEntryStructure
        '
        Me.helpEntryStructure.HelpNamespace = ""
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 24)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(384, 3)
        Me.Splitter1.TabIndex = 38
        Me.Splitter1.TabStop = False
        '
        'EntryStructure
        '
        Me.Controls.Add(Me.PanelIcons)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.PanelStructureHeader)
        Me.helpEntryStructure.SetHelpKeyword(Me, "Edit an archetype")
        Me.helpEntryStructure.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.TableOfContents)
        Me.Name = "EntryStructure"
        Me.helpEntryStructure.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(384, 406)
        Me.PanelIcons.ResumeLayout(False)
        CType(Me.pbText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbOrdinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBoolean, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAny, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCluster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSlot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelStructureHeader.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'implement as overrided property
    Public ReadOnly Property StructureType() As StructureType
        Get
            Return mStructureType
        End Get
    End Property

    Public Sub SetTextForNodeId(ByVal text As String)
        If mNodeId = "" Then
            mNodeId = mFileManager.OntologyManager.AddTerm(text, "@ internal @").Code
        ElseIf mNodeId <> mFileManager.Archetype.ConceptCode Then
            mFileManager.OntologyManager.SetText(text, mNodeId)
        End If
    End Sub

    Public Overridable ReadOnly Property InterfaceBuilder() As Object
        Get
            Throw New NotImplementedException("Subclass must override this property")
        End Get
    End Property

    Public Property IsChangeStructureMenuVisible() As Boolean
        Get
            Return Not menuChangeStructure Is Nothing AndAlso menuChangeStructure.Visible
        End Get
        Set(ByVal Value As Boolean)
            menuChangeStructure.Visible = Value
        End Set
    End Property

    Private Sub menuChangeStructure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuChangeStructure.Click
        NotifyChangeStructure(Me, e)
    End Sub

    Public Overridable Property Archetype() As RmStructure
        Get
            Throw New NotImplementedException("Subclass must override this property")
        End Get
        Set(ByVal Value As RmStructure)
            Throw New NotImplementedException("Subclass must override this property")
        End Set
    End Property

    Protected Sub SetCardinality(ByVal rm As RmStructureCompound)
        SetCardinality(rm.Type)
        mCardinalityControl.Cardinality = rm.Children.Cardinality
        AddHandler mCardinalityControl.Cardinality.Updated, AddressOf CardinalityUpdated
        CardinalityUpdated(mCardinalityControl, New EventArgs)
    End Sub

    Protected Sub SetCardinality(ByVal a_structure_type As StructureType)
        mCardinalityControl = New OccurrencesPanel(mFileManager)
        mCardinalityControl.LocalFileManager = mFileManager

        If a_structure_type = StructureType.Single Then
            mCardinalityControl.SetSingle = True
        Else
            mCardinalityControl.IsContainer = True
            mCardinalityControl.Location = New Drawing.Point(0, 0)
            PanelStructureHeader.Controls.Add(mCardinalityControl)

            If a_structure_type = Global.ArchetypeEditor.StructureType.Cluster Then
                mCardinalityControl.SetMandatory = True
                mCardinalityControl.IsContainer = True
            End If

            AddHandler mCardinalityControl.Cardinality.Updated, AddressOf CardinalityUpdated
        End If
    End Sub

    Private Sub CardinalityUpdated(ByVal sender As Object, ByVal e As EventArgs)
        If mCardinalityControl.Cardinality.ToString() = "0..*" Then
            PanelStructureHeader.Height = 25
        Else
            PanelStructureHeader.Height = 75
        End If
    End Sub

    Protected Function HtmlHeader(ByVal aBackGroundColour As String, ByVal showComments As Boolean) As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim displayWidth As String = "20"

        If aBackGroundColour = "" Then
            result.Append("<tr>")
        Else
            result.AppendFormat("<tr  bgcolor=""{0}"">", aBackGroundColour)
        End If

        If showComments Then
            displayWidth = "16"
        End If

        result.AppendFormat("{0}<td width=""{1}%""><h4>{2}</h4></td>", Environment.NewLine, displayWidth, Filemanager.GetOpenEhrTerm(54, "Concept"))
        result.AppendFormat("{0}<td width=""{1}%""><h4>{2}</h4></td>", Environment.NewLine, displayWidth, Filemanager.GetOpenEhrTerm(113, "Description"))
        result.AppendFormat("{0}<td width=""{1}%""><h4>{2}</h4></td>", Environment.NewLine, displayWidth, Filemanager.GetOpenEhrTerm(87, "Constraints"))
        result.AppendFormat("{0}<td width=""{1}%""><h4>{2}</h4></td>", Environment.NewLine, displayWidth, Filemanager.GetOpenEhrTerm(438, "Values"))

        If showComments Then
            result.AppendFormat("{0}<td width=""{1}%""><h4>{2}</h4></td>", Environment.NewLine, displayWidth, Filemanager.GetOpenEhrTerm(652, "Comments"))
        End If

        result.AppendFormat("{0}</tr>", Environment.NewLine)

        Return result.ToString
    End Function

    Protected Sub SetHelpTopic(ByVal a_structure_type As StructureType)
        helpEntryStructure.SetHelpNavigator(Me, HelpNavigator.Topic)

        Select Case a_structure_type
            Case StructureType.Single
                helpEntryStructure.SetHelpKeyword(Me, "Screens/structure_simple.htm")
            Case StructureType.List
                helpEntryStructure.SetHelpKeyword(Me, "Screens/structure_list.htm")
            Case StructureType.Tree
                helpEntryStructure.SetHelpKeyword(Me, "Screens/structure_tree.htm")
            Case StructureType.Table
                helpEntryStructure.SetHelpKeyword(Me, "Screens/structure_table.htm")
        End Select
    End Sub

    Public Overridable Sub Translate()
        RaiseEvent CurrentItemChanged(mCurrentItem, New EventArgs)
    End Sub

    Public Overridable Sub Reset()
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Public Overridable Sub SetInitial()
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub SetUpAddElementMenu()
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub AddNewElement(ByVal a_constraint As Constraint)
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub AddReference(ByVal sender As Object, ByVal e As EventArgs)
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub NameSlot(ByVal sender As Object, ByVal e As EventArgs)
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs)
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Public Overridable Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        Throw New NotImplementedException("Subclass must override this method")
    End Function

    Public Overridable Function ToHTML(ByVal BackGroundColour As String) As String
        Throw New NotImplementedException("Subclass must override this method")
    End Function

    Public Overridable Function HasData() As Boolean
        Throw New NotImplementedException("Subclass must override this method")
    End Function

    Protected Overridable Sub ButListUp_Click(ByVal sender As Object, ByVal e As EventArgs)
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub ButListDown_Click(ByVal sender As Object, ByVal e As EventArgs)
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Overridable Sub RefreshIcons()
        Throw New NotImplementedException("Subclass must override this method")
    End Sub

    Protected Sub SetCurrentItem(ByVal node As ArchetypeNode)
        ' if nothing this hides panelDetails
        mCurrentItem = node

        If Not node Is Nothing Then
            lblAtcode.Text = node.RM_Class.NodeId
        Else
            lblAtcode.Text = ""
        End If

        SetButtonVisibility(node)
        RaiseEvent CurrentItemChanged(node, New EventArgs)
    End Sub

    Public Sub SetButtonVisibility(ByVal node As ArchetypeNode)
        'Hide the icons if simple to stop drag and drop
        If mStructureType = StructureType.Single AndAlso pbText.Visible Then
            pbAny.Hide()
            pbBoolean.Hide()
            pbCount.Hide()
            pbDateTime.Hide()
            pbOrdinal.Hide()
            pbQuantity.Hide()
            pbText.Hide()
            LayoutIcons()
        End If

        If node Is Nothing OrElse node.RM_Class.Type <> StructureType.Element Then
            butRemoveElement.Enabled = False
            butChangeDataType.Hide()
        Else
            butChangeDataType.Enabled = Not CType(node, ArchetypeElement).IsReference
            butChangeDataType.Show()
            butRemoveElement.Enabled = True

            If SpecialisationDepth > 0 Then
                ' ensure that datatypes cannot be changed in specialisations not at this level
                'except for any

                If node.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                    butRemoveElement.Enabled = False
                    butChangeDataType.Hide()
                End If

                If CType(node, ArchetypeElement).Constraint.Kind = ConstraintKind.Any Then
                    butChangeDataType.Show()
                End If
            End If
        End If
    End Sub

    Protected Sub SetToolTipSpecialisation(ByVal ctrl As Control, ByVal item As ArchetypeNode)
        If item Is Nothing Then
            ToolTipSpecialisation.RemoveAll()
        ElseIf SpecialisationDepth > 0 Then
            If Not item.IsAnonymous Then
                Dim ct() As CodeAndTerm = Main.Instance.GetSpecialisationChain(CType(item, ArchetypeNodeAbstract).NodeId, mFileManager)

                If ct.Length = 1 Then
                    ToolTipSpecialisation.RemoveAll()
                Else
                    Dim s As String = "Specialised:" & Environment.NewLine
                    Dim i As Integer

                    For i = 0 To ct.Length - 1
                        s = s & Space((i * 2) + 2) & "- " & ct(i).Text

                        If i < ct.Length - 1 Then
                            s = s & Environment.NewLine
                        End If
                    Next

                    ToolTipSpecialisation.SetToolTip(ctrl, s)
                End If
            End If
        End If
    End Sub

    Protected ReadOnly Property SpecialisationDepth() As Integer
        Get
            Return mFileManager.OntologyManager.NumberOfSpecialisations
        End Get
    End Property

    Private Sub LayoutIcons()
        ' now space the buttons consistently
        Dim ctrl As Control
        Dim loc As New System.Drawing.Point(8, 4)

        For Each ctrl In PanelIcons.Controls
            If ctrl.Visible Then
                ctrl.Location = loc
                loc.Y += 27
            End If
        Next

        'tag the change datatype button on the end
        butChangeDataType.Location = loc
    End Sub

    Protected Sub ShowIcons()
        ' turn off any inappropriate buttons
        Select Case mStructureType
            Case StructureType.List
                pbCluster.Hide()
            Case StructureType.Table
                butListUp.Hide()
                butListDown.Hide()
                pbCluster.Hide()
            Case StructureType.Element, StructureType.Single
                butListUp.Hide()
                butListDown.Hide()
                pbCluster.Hide()
                ButAddElement.Hide()
                butRemoveElement.Hide()
                pbSlot.Hide()
                pbText.Hide()
                pbQuantity.Hide()
                pbDateTime.Hide()
                pbCount.Hide()
                pbOrdinal.Hide()
                pbBoolean.Hide()
                pbAny.Hide()
        End Select

        LayoutIcons()
    End Sub

    Private Sub ButAddElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddElement.Click
        mConstraintMenu = New ConstraintContextMenu(AddressOf AddNewElement, mFileManager)
        mConstraintMenu.ShowHeader(Filemanager.GetOpenEhrTerm(155, "Add"))
        SetUpAddElementMenu()
    End Sub

    Private Sub butRemoveElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveElement.Click
        RemoveItemAndReferences(sender, e)
    End Sub

    Private Sub ChangeConstraint(ByVal a_constraint As Constraint)
        Debug.Assert(mCurrentItem.RM_Class.Type = StructureType.Element)
        If a_constraint.Kind = ConstraintKind.Multiple Then
            'Add the current constraint to the multiple constraint before setting the current item to the multiple
            CType(a_constraint, Constraint_Choice).Constraints.Add(CType(mCurrentItem, ArchetypeElement).Constraint)
        ElseIf CType(mCurrentItem, ArchetypeElement).Constraint.Kind = ConstraintKind.Multiple Then
            'Or if the current item is multiple
            Dim m As Constraint_Choice
            m = CType(mCurrentItem, ArchetypeElement).Constraint
            For Each c As Constraint In m.Constraints
                'find the constraint that is of the same type as a_constraint if there is one
                If c.Kind = a_constraint.Kind Then
                    a_constraint = c
                End If
            Next
        End If
        'now set the current item to the new constraint
        CType(mCurrentItem, ArchetypeElement).Constraint = a_constraint
        mFileManager.FileEdited = True
        RefreshIcons()
        RaiseEvent CurrentItemChanged(mCurrentItem, New EventArgs)
    End Sub

    Private Sub butChangeDataType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butChangeDataType.Click
        Debug.Assert(Not mCurrentItem Is Nothing, "Button should not be available")
        Debug.Assert(mCurrentItem.RM_Class.Type = StructureType.Element, "Button should not be available")
        mConstraintMenu = New ConstraintContextMenu(New ConstraintContextMenu.ProcessMenuClick(AddressOf ChangeConstraint), mFileManager)
        ' hide the current constraint type
        mConstraintMenu.HideMenuItem(CType(mCurrentItem, ArchetypeElement).Constraint.Kind)
        mConstraintMenu.ShowHeader(Filemanager.GetOpenEhrTerm(60, "Change data type"))
        mConstraintMenu.Show(butChangeDataType, New System.Drawing.Point(5, 5))
    End Sub

    Private Sub cbOrdered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not mCurrentItem Is Nothing Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
        ' changes the colour of some buttons when the background colour changes
        If Me.BackColor.Equals(System.Drawing.Color.LightSteelBlue) Then
            ButAddElement.BackColor = System.Drawing.Color.CornflowerBlue
            butRemoveElement.BackColor = System.Drawing.Color.CornflowerBlue
            butListUp.BackColor = System.Drawing.Color.CornflowerBlue
            butListDown.BackColor = System.Drawing.Color.CornflowerBlue
        End If
    End Sub

    Private Sub EntryStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Me.DesignMode Then
            ' add the tooltips to the buttons on the left
            Me.ttElement.SetToolTip(Me.pbText, AE_Constants.Instance.Text)
            Me.ttElement.SetToolTip(Me.pbQuantity, AE_Constants.Instance.Quantity)
            Me.ttElement.SetToolTip(Me.pbAny, AE_Constants.Instance.Any)
            Me.ttElement.SetToolTip(Me.pbBoolean, AE_Constants.Instance.Boolean_)
            Me.ttElement.SetToolTip(Me.pbOrdinal, AE_Constants.Instance.Ordinal)
            Me.ttElement.SetToolTip(Me.pbCount, AE_Constants.Instance.Count)
            Me.ttElement.SetToolTip(Me.pbDateTime, AE_Constants.Instance.DateTime)
            Me.ttElement.SetToolTip(Me.pbCluster, AE_Constants.Instance.Cluster)
            Me.ttElement.SetToolTip(Me.butChangeDataType, AE_Constants.Instance.ChangeDataType)
            Me.ttElement.SetToolTip(Me.pbSlot, AE_Constants.Instance.Slot)

            Me.helpEntryStructure.HelpNamespace = Main.Instance.Options.HelpLocationPath
        End If
    End Sub

    Protected Sub NotifyChangeStructure(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ChangeStructure(sender, e)
    End Sub

    Private Sub pbGroup_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
                Handles pbAny.MouseUp, pbBoolean.MouseUp, pbCluster.MouseUp, pbCount.MouseUp, pbDateTime.MouseUp, _
                    pbOrdinal.MouseUp, pbText.MouseUp, pbQuantity.MouseUp, pbSlot.MouseUp

        'cancel drag and drop operation
        mNewConstraint = Nothing
        mNewCluster = False
    End Sub

    Private Sub pbGroup_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
                Handles pbAny.MouseDown, pbBoolean.MouseDown, pbCluster.MouseDown, pbCount.MouseDown, _
                        pbDateTime.MouseDown, pbOrdinal.MouseDown, pbQuantity.MouseDown, pbText.MouseDown, pbSlot.MouseDown

        mControl.AllowDrop = True

        If sender.name = "pbCluster" Then
            mNewCluster = True
            sender.DoDragDrop(mNewCluster, DragDropEffects.Copy)
        Else
            ' create mNewConstraint with the correct constraint
            Dim ctrlName As String = sender.name

            Select Case ctrlName
                Case "pbAny"
                    mNewConstraint = New Constraint
                Case "pbBoolean"
                    mNewConstraint = New Constraint_Boolean
                Case "pbCount"
                    mNewConstraint = New Constraint_Count
                Case "pbDateTime"
                    mNewConstraint = New Constraint_DateTime
                Case "pbOrdinal"
                    mNewConstraint = New Constraint_Ordinal(True, mFileManager)
                Case "pbQuantity"
                    mNewConstraint = New Constraint_Quantity
                Case "pbText"
                    mNewConstraint = New Constraint_Text
                Case "pbSlot"
                    mNewConstraint = New Constraint_Slot
            End Select
            sender.DoDragDrop(mNewConstraint, DragDropEffects.Copy)
        End If

        If mControl.Enabled = False Then
            mControl.Enabled = True
        End If
    End Sub

    Private Sub EntryStructure_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        Main.Reflect(Me)
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
'The Original Code is EntryStructure.vb.
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
