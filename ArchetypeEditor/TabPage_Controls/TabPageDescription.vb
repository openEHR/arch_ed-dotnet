
Option Strict On

Public Class TabPageDescription
    Inherits System.Windows.Forms.UserControl

    Private mArchetypeDescription As ArchetypeDescription
    Private mLifeCycleStatesTable As DataTable
    Private mCurrentLanguage As String


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
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtUse As System.Windows.Forms.TextBox
    Friend WithEvents txtMisuse As System.Windows.Forms.TextBox
    Friend WithEvents gbUse As System.Windows.Forms.GroupBox
    Friend WithEvents gbMisuse As System.Windows.Forms.GroupBox
    Friend WithEvents panelDescription As System.Windows.Forms.Panel
    Friend WithEvents listKeyword As System.Windows.Forms.ListBox
    Friend WithEvents lblKeyword As System.Windows.Forms.Label
    Friend WithEvents TabDescription As Crownwood.Magic.Controls.TabControl
    Friend WithEvents tpDescDetails As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpAuthor As Crownwood.Magic.Controls.TabPage
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents gbAuthor As System.Windows.Forms.GroupBox
    Friend WithEvents txtOriginalAuthor As System.Windows.Forms.TextBox
    Friend WithEvents txtOriginalEmail As System.Windows.Forms.TextBox
    Friend WithEvents comboLifeCycle As System.Windows.Forms.ComboBox
    Friend WithEvents gbPurpose As System.Windows.Forms.GroupBox
    Friend WithEvents txtPurpose As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents ButAddKeyWord As System.Windows.Forms.Button
    Friend WithEvents butRemoveKeyWord As System.Windows.Forms.Button
    Friend WithEvents lblOrganisation As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents txtOrganisation As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents c_menuPaste As System.Windows.Forms.MenuItem
    Friend WithEvents c_menuPasteAll As System.Windows.Forms.MenuItem
    Friend WithEvents c_menuPasteName As System.Windows.Forms.MenuItem
    Friend WithEvents c_menuPasteEmail As System.Windows.Forms.MenuItem
    Friend WithEvents c_menuPasteOrg As System.Windows.Forms.MenuItem
    Friend WithEvents c_menPasteDate As System.Windows.Forms.MenuItem
    Friend WithEvents lblContributors As System.Windows.Forms.Label
    Friend WithEvents listContributors As System.Windows.Forms.ListBox
    Friend WithEvents butRemoveContributor As System.Windows.Forms.Button
    Friend WithEvents butAddContributor As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabPageDescription))
        Me.lblStatus = New System.Windows.Forms.Label
        Me.txtUse = New System.Windows.Forms.TextBox
        Me.txtMisuse = New System.Windows.Forms.TextBox
        Me.gbUse = New System.Windows.Forms.GroupBox
        Me.gbMisuse = New System.Windows.Forms.GroupBox
        Me.gbPurpose = New System.Windows.Forms.GroupBox
        Me.txtPurpose = New System.Windows.Forms.TextBox
        Me.panelDescription = New System.Windows.Forms.Panel
        Me.ButAddKeyWord = New System.Windows.Forms.Button
        Me.butRemoveKeyWord = New System.Windows.Forms.Button
        Me.comboLifeCycle = New System.Windows.Forms.ComboBox
        Me.lblKeyword = New System.Windows.Forms.Label
        Me.listKeyword = New System.Windows.Forms.ListBox
        Me.TabDescription = New Crownwood.Magic.Controls.TabControl
        Me.tpAuthor = New Crownwood.Magic.Controls.TabPage
        Me.butAddContributor = New System.Windows.Forms.Button
        Me.butRemoveContributor = New System.Windows.Forms.Button
        Me.lblContributors = New System.Windows.Forms.Label
        Me.listContributors = New System.Windows.Forms.ListBox
        Me.gbAuthor = New System.Windows.Forms.GroupBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.c_menuPaste = New System.Windows.Forms.MenuItem
        Me.c_menuPasteAll = New System.Windows.Forms.MenuItem
        Me.c_menuPasteName = New System.Windows.Forms.MenuItem
        Me.c_menuPasteEmail = New System.Windows.Forms.MenuItem
        Me.c_menuPasteOrg = New System.Windows.Forms.MenuItem
        Me.c_menPasteDate = New System.Windows.Forms.MenuItem
        Me.lblDate = New System.Windows.Forms.Label
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.lblOrganisation = New System.Windows.Forms.Label
        Me.txtOrganisation = New System.Windows.Forms.TextBox
        Me.lblName = New System.Windows.Forms.Label
        Me.txtOriginalAuthor = New System.Windows.Forms.TextBox
        Me.lblEmail = New System.Windows.Forms.Label
        Me.txtOriginalEmail = New System.Windows.Forms.TextBox
        Me.tpDescDetails = New Crownwood.Magic.Controls.TabPage
        Me.Splitter3 = New System.Windows.Forms.Splitter
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.gbUse.SuspendLayout()
        Me.gbMisuse.SuspendLayout()
        Me.gbPurpose.SuspendLayout()
        Me.panelDescription.SuspendLayout()
        Me.tpAuthor.SuspendLayout()
        Me.gbAuthor.SuspendLayout()
        Me.tpDescDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(48, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(168, 24)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "Authorship lifecycle:"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUse
        '
        Me.txtUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUse.Location = New System.Drawing.Point(3, 20)
        Me.txtUse.Multiline = True
        Me.txtUse.Name = "txtUse"
        Me.txtUse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUse.Size = New System.Drawing.Size(694, 105)
        Me.txtUse.TabIndex = 4
        Me.txtUse.Text = ""
        '
        'txtMisuse
        '
        Me.txtMisuse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMisuse.Location = New System.Drawing.Point(3, 20)
        Me.txtMisuse.Multiline = True
        Me.txtMisuse.Name = "txtMisuse"
        Me.txtMisuse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMisuse.Size = New System.Drawing.Size(694, 89)
        Me.txtMisuse.TabIndex = 6
        Me.txtMisuse.Text = ""
        '
        'gbUse
        '
        Me.gbUse.Controls.Add(Me.txtUse)
        Me.gbUse.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbUse.Location = New System.Drawing.Point(0, 231)
        Me.gbUse.Name = "gbUse"
        Me.gbUse.Size = New System.Drawing.Size(700, 128)
        Me.gbUse.TabIndex = 9
        Me.gbUse.TabStop = False
        Me.gbUse.Text = "Use"
        '
        'gbMisuse
        '
        Me.gbMisuse.Controls.Add(Me.txtMisuse)
        Me.gbMisuse.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbMisuse.Location = New System.Drawing.Point(0, 362)
        Me.gbMisuse.Name = "gbMisuse"
        Me.gbMisuse.Size = New System.Drawing.Size(700, 112)
        Me.gbMisuse.TabIndex = 10
        Me.gbMisuse.TabStop = False
        Me.gbMisuse.Text = "Misuse"
        '
        'gbPurpose
        '
        Me.gbPurpose.Controls.Add(Me.txtPurpose)
        Me.gbPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbPurpose.Location = New System.Drawing.Point(0, 0)
        Me.gbPurpose.Name = "gbPurpose"
        Me.gbPurpose.Size = New System.Drawing.Size(465, 228)
        Me.gbPurpose.TabIndex = 13
        Me.gbPurpose.TabStop = False
        Me.gbPurpose.Text = "Purpose"
        '
        'txtPurpose
        '
        Me.txtPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPurpose.Location = New System.Drawing.Point(3, 20)
        Me.txtPurpose.Multiline = True
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPurpose.Size = New System.Drawing.Size(459, 205)
        Me.txtPurpose.TabIndex = 0
        Me.txtPurpose.Text = ""
        '
        'panelDescription
        '
        Me.panelDescription.Controls.Add(Me.ButAddKeyWord)
        Me.panelDescription.Controls.Add(Me.butRemoveKeyWord)
        Me.panelDescription.Controls.Add(Me.comboLifeCycle)
        Me.panelDescription.Controls.Add(Me.lblKeyword)
        Me.panelDescription.Controls.Add(Me.listKeyword)
        Me.panelDescription.Controls.Add(Me.lblStatus)
        Me.panelDescription.Dock = System.Windows.Forms.DockStyle.Right
        Me.panelDescription.Location = New System.Drawing.Point(468, 0)
        Me.panelDescription.Name = "panelDescription"
        Me.panelDescription.Size = New System.Drawing.Size(232, 228)
        Me.panelDescription.TabIndex = 14
        '
        'ButAddKeyWord
        '
        Me.ButAddKeyWord.BackColor = System.Drawing.Color.Transparent
        Me.ButAddKeyWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButAddKeyWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButAddKeyWord.Image = CType(resources.GetObject("ButAddKeyWord.Image"), System.Drawing.Image)
        Me.ButAddKeyWord.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButAddKeyWord.Location = New System.Drawing.Point(16, 72)
        Me.ButAddKeyWord.Name = "ButAddKeyWord"
        Me.ButAddKeyWord.Size = New System.Drawing.Size(24, 25)
        Me.ButAddKeyWord.TabIndex = 34
        '
        'butRemoveKeyWord
        '
        Me.butRemoveKeyWord.BackColor = System.Drawing.Color.Transparent
        Me.butRemoveKeyWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveKeyWord.Image = CType(resources.GetObject("butRemoveKeyWord.Image"), System.Drawing.Image)
        Me.butRemoveKeyWord.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveKeyWord.Location = New System.Drawing.Point(16, 104)
        Me.butRemoveKeyWord.Name = "butRemoveKeyWord"
        Me.butRemoveKeyWord.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveKeyWord.TabIndex = 35
        '
        'comboLifeCycle
        '
        Me.comboLifeCycle.Location = New System.Drawing.Point(48, 24)
        Me.comboLifeCycle.Name = "comboLifeCycle"
        Me.comboLifeCycle.Size = New System.Drawing.Size(168, 25)
        Me.comboLifeCycle.TabIndex = 4
        '
        'lblKeyword
        '
        Me.lblKeyword.Location = New System.Drawing.Point(49, 46)
        Me.lblKeyword.Name = "lblKeyword"
        Me.lblKeyword.Size = New System.Drawing.Size(112, 24)
        Me.lblKeyword.TabIndex = 3
        Me.lblKeyword.Text = "Keywords:"
        Me.lblKeyword.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'listKeyword
        '
        Me.listKeyword.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listKeyword.ItemHeight = 17
        Me.listKeyword.Location = New System.Drawing.Point(48, 72)
        Me.listKeyword.Name = "listKeyword"
        Me.listKeyword.Size = New System.Drawing.Size(168, 123)
        Me.listKeyword.TabIndex = 2
        '
        'TabDescription
        '
        Me.TabDescription.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDescription.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabDescription.Location = New System.Drawing.Point(2, 2)
        Me.TabDescription.Name = "TabDescription"
        Me.TabDescription.PositionTop = True
        Me.TabDescription.SelectedIndex = 1
        Me.TabDescription.SelectedTab = Me.tpAuthor
        Me.TabDescription.Size = New System.Drawing.Size(700, 500)
        Me.TabDescription.TabIndex = 15
        Me.TabDescription.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpDescDetails, Me.tpAuthor})
        '
        'tpAuthor
        '
        Me.tpAuthor.Controls.Add(Me.butAddContributor)
        Me.tpAuthor.Controls.Add(Me.butRemoveContributor)
        Me.tpAuthor.Controls.Add(Me.lblContributors)
        Me.tpAuthor.Controls.Add(Me.listContributors)
        Me.tpAuthor.Controls.Add(Me.gbAuthor)
        Me.tpAuthor.Location = New System.Drawing.Point(0, 0)
        Me.tpAuthor.Name = "tpAuthor"
        Me.tpAuthor.Size = New System.Drawing.Size(700, 474)
        Me.tpAuthor.TabIndex = 1
        Me.tpAuthor.Title = "Authorship"
        '
        'butAddContributor
        '
        Me.butAddContributor.BackColor = System.Drawing.Color.Transparent
        Me.butAddContributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddContributor.Image = CType(resources.GetObject("butAddContributor.Image"), System.Drawing.Image)
        Me.butAddContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddContributor.Location = New System.Drawing.Point(24, 189)
        Me.butAddContributor.Name = "butAddContributor"
        Me.butAddContributor.Size = New System.Drawing.Size(24, 25)
        Me.butAddContributor.TabIndex = 38
        '
        'butRemoveContributor
        '
        Me.butRemoveContributor.BackColor = System.Drawing.Color.Transparent
        Me.butRemoveContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveContributor.Image = CType(resources.GetObject("butRemoveContributor.Image"), System.Drawing.Image)
        Me.butRemoveContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveContributor.Location = New System.Drawing.Point(24, 221)
        Me.butRemoveContributor.Name = "butRemoveContributor"
        Me.butRemoveContributor.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveContributor.TabIndex = 39
        '
        'lblContributors
        '
        Me.lblContributors.Location = New System.Drawing.Point(56, 163)
        Me.lblContributors.Name = "lblContributors"
        Me.lblContributors.Size = New System.Drawing.Size(112, 24)
        Me.lblContributors.TabIndex = 37
        Me.lblContributors.Text = "Contributors"
        Me.lblContributors.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'listContributors
        '
        Me.listContributors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listContributors.ItemHeight = 17
        Me.listContributors.Location = New System.Drawing.Point(56, 189)
        Me.listContributors.Name = "listContributors"
        Me.listContributors.Size = New System.Drawing.Size(528, 123)
        Me.listContributors.TabIndex = 36
        '
        'gbAuthor
        '
        Me.gbAuthor.ContextMenu = Me.ContextMenu1
        Me.gbAuthor.Controls.Add(Me.lblDate)
        Me.gbAuthor.Controls.Add(Me.txtDate)
        Me.gbAuthor.Controls.Add(Me.lblOrganisation)
        Me.gbAuthor.Controls.Add(Me.txtOrganisation)
        Me.gbAuthor.Controls.Add(Me.lblName)
        Me.gbAuthor.Controls.Add(Me.txtOriginalAuthor)
        Me.gbAuthor.Controls.Add(Me.lblEmail)
        Me.gbAuthor.Controls.Add(Me.txtOriginalEmail)
        Me.gbAuthor.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbAuthor.Location = New System.Drawing.Point(0, 0)
        Me.gbAuthor.Name = "gbAuthor"
        Me.gbAuthor.Size = New System.Drawing.Size(700, 152)
        Me.gbAuthor.TabIndex = 4
        Me.gbAuthor.TabStop = False
        Me.gbAuthor.Text = "Original author"
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.c_menuPaste})
        '
        'c_menuPaste
        '
        Me.c_menuPaste.Index = 0
        Me.c_menuPaste.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.c_menuPasteAll, Me.c_menuPasteName, Me.c_menuPasteEmail, Me.c_menuPasteOrg, Me.c_menPasteDate})
        Me.c_menuPaste.Text = "Paste"
        '
        'c_menuPasteAll
        '
        Me.c_menuPasteAll.Index = 0
        Me.c_menuPasteAll.Text = "All"
        '
        'c_menuPasteName
        '
        Me.c_menuPasteName.Index = 1
        Me.c_menuPasteName.Text = "Name"
        '
        'c_menuPasteEmail
        '
        Me.c_menuPasteEmail.Index = 2
        Me.c_menuPasteEmail.Text = "Email"
        '
        'c_menuPasteOrg
        '
        Me.c_menuPasteOrg.Index = 3
        Me.c_menuPasteOrg.Text = "Organisation"
        '
        'c_menPasteDate
        '
        Me.c_menPasteDate.Index = 4
        Me.c_menPasteDate.Text = "Date"
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(16, 120)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(128, 24)
        Me.lblDate.TabIndex = 7
        Me.lblDate.Text = "Date:"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(152, 120)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(160, 24)
        Me.txtDate.TabIndex = 6
        Me.txtDate.Text = ""
        '
        'lblOrganisation
        '
        Me.lblOrganisation.Location = New System.Drawing.Point(16, 88)
        Me.lblOrganisation.Name = "lblOrganisation"
        Me.lblOrganisation.Size = New System.Drawing.Size(128, 24)
        Me.lblOrganisation.TabIndex = 5
        Me.lblOrganisation.Text = "Organisation:"
        Me.lblOrganisation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOrganisation
        '
        Me.txtOrganisation.Location = New System.Drawing.Point(152, 88)
        Me.txtOrganisation.Name = "txtOrganisation"
        Me.txtOrganisation.Size = New System.Drawing.Size(424, 24)
        Me.txtOrganisation.TabIndex = 4
        Me.txtOrganisation.Text = ""
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(16, 24)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(128, 24)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Name:"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalAuthor
        '
        Me.txtOriginalAuthor.Location = New System.Drawing.Point(152, 24)
        Me.txtOriginalAuthor.Name = "txtOriginalAuthor"
        Me.txtOriginalAuthor.Size = New System.Drawing.Size(424, 24)
        Me.txtOriginalAuthor.TabIndex = 0
        Me.txtOriginalAuthor.Text = ""
        '
        'lblEmail
        '
        Me.lblEmail.Location = New System.Drawing.Point(16, 56)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(128, 24)
        Me.lblEmail.TabIndex = 3
        Me.lblEmail.Text = "Email:"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalEmail
        '
        Me.txtOriginalEmail.Location = New System.Drawing.Point(152, 56)
        Me.txtOriginalEmail.Name = "txtOriginalEmail"
        Me.txtOriginalEmail.Size = New System.Drawing.Size(424, 24)
        Me.txtOriginalEmail.TabIndex = 1
        Me.txtOriginalEmail.Text = ""
        '
        'tpDescDetails
        '
        Me.tpDescDetails.Controls.Add(Me.gbPurpose)
        Me.tpDescDetails.Controls.Add(Me.Splitter3)
        Me.tpDescDetails.Controls.Add(Me.panelDescription)
        Me.tpDescDetails.Controls.Add(Me.Splitter2)
        Me.tpDescDetails.Controls.Add(Me.gbUse)
        Me.tpDescDetails.Controls.Add(Me.Splitter1)
        Me.tpDescDetails.Controls.Add(Me.gbMisuse)
        Me.tpDescDetails.Location = New System.Drawing.Point(0, 0)
        Me.tpDescDetails.Name = "tpDescDetails"
        Me.tpDescDetails.Selected = False
        Me.tpDescDetails.Size = New System.Drawing.Size(700, 474)
        Me.tpDescDetails.TabIndex = 0
        Me.tpDescDetails.Title = "Details"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter3.Location = New System.Drawing.Point(465, 0)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(3, 228)
        Me.Splitter3.TabIndex = 17
        Me.Splitter3.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(0, 228)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(700, 3)
        Me.Splitter2.TabIndex = 16
        Me.Splitter2.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 359)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(700, 3)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'TabPageDescription
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.TabDescription)
        Me.DockPadding.All = 2
        Me.Name = "TabPageDescription"
        Me.Size = New System.Drawing.Size(704, 504)
        Me.gbUse.ResumeLayout(False)
        Me.gbMisuse.ResumeLayout(False)
        Me.gbPurpose.ResumeLayout(False)
        Me.panelDescription.ResumeLayout(False)
        Me.tpAuthor.ResumeLayout(False)
        Me.gbAuthor.ResumeLayout(False)
        Me.tpDescDetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property Description() As ArchetypeDescription
        Get
            SaveDescription()
            Return mArchetypeDescription
        End Get
        Set(ByVal Value As ArchetypeDescription)
            ' add the lifecycle states to the combo box
            LoadAuthorStatesTableCombo()
            Me.comboLifeCycle.SelectedValue = CInt(Value.LifeCycleState)
            'Me.comboLifeCycle.SelectedIndex = Me.comboLifeCycle.FindStringExact(Value.LifeCycleStateAsString)
            Me.txtOriginalAuthor.Text = Value.OriginalAuthor
            Me.txtOriginalEmail.Text = Value.OriginalAuthorEmail
            Me.txtOrganisation.Text = Value.OriginalAuthorOrganisation
            Me.txtDate.Text = Value.OriginalAuthorDate
            For Each s As String In Value.OtherContributors
                Me.listContributors.Items.Add(s)
            Next

            mArchetypeDescription = Value
            mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
            SetDescriptionDetailValues()
        End Set
    End Property

    Public Sub SaveDescription()

        If mArchetypeDescription Is Nothing Then
            Select Case Filemanager.Master.ParserType
                Case "adl"
                    mArchetypeDescription = New ArchetypeEditor.ADL_Classes.ADL_Description
                Case Else
                    mArchetypeDescription = New ArchetypeDescription
            End Select
        End If
        If comboLifeCycle.SelectedIndex > -1 Then
            mArchetypeDescription.LifeCycleState = CType([Enum].ToObject(GetType(LifeCycleStates), Convert.ToInt32(Me.comboLifeCycle.SelectedValue)), LifeCycleStates)
        Else
            'set lifecycle to not set
            mArchetypeDescription.LifeCycleState = CType([Enum].ToObject(GetType(LifeCycleStates), 0), LifeCycleStates)
        End If

        mArchetypeDescription.OriginalAuthor = Me.txtOriginalAuthor.Text
        mArchetypeDescription.OriginalAuthorEmail = Me.txtOriginalEmail.Text
        mArchetypeDescription.OriginalAuthorOrganisation = Me.txtOrganisation.Text
        mArchetypeDescription.OriginalAuthorDate = Me.txtDate.Text

        ' get the contributors
        For Each s As String In Me.listContributors.Items
            mArchetypeDescription.OtherContributors.Add(s)
        Next

        If mCurrentLanguage Is Nothing Then
            mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        End If

        Dim archDescriptionItem As New ArchetypeDescriptionItem( _
            mCurrentLanguage)

        ' get the key words
        For Each s As String In Me.listKeyword.Items
            archDescriptionItem.KeyWords.Add(s)
        Next

        archDescriptionItem.Purpose = Me.txtPurpose.Text
        archDescriptionItem.Use = Me.txtUse.Text
        archDescriptionItem.MisUse = Me.txtMisuse.Text
        mArchetypeDescription.Details.AddOrReplace(archDescriptionItem.Language, archDescriptionItem)
    End Sub

    Public Sub SetDescriptionDetailValues()
        Me.listKeyword.Items.Clear()
        If mArchetypeDescription.Details.HasDetailInLanguage(mCurrentLanguage) Then
            Dim archDescriptionItem As ArchetypeDescriptionItem
            archDescriptionItem = mArchetypeDescription.Details.DetailInLanguage(mCurrentLanguage)
            For Each s As String In archDescriptionItem.KeyWords
                Me.listKeyword.Items.Add(s)
            Next
            Me.txtPurpose.Text = archDescriptionItem.Purpose
            Me.txtUse.Text = archDescriptionItem.Use
            Me.txtMisuse.Text = archDescriptionItem.MisUse
        Else
            Me.txtPurpose.Text = ""
            Me.txtUse.Text = ""
            Me.txtMisuse.Text = ""
        End If
    End Sub

    Public Sub Reset()
        Me.txtOriginalAuthor.Text = OceanArchetypeEditor.Instance.Options.UserName
        Me.txtOriginalEmail.Text = OceanArchetypeEditor.Instance.Options.UserEmail
        Me.txtPurpose.Text = ""
        Me.comboLifeCycle.SelectedIndex = -1
        Me.txtUse.Text = ""
        Me.txtMisuse.Text = ""
        Me.listKeyword.Items.Clear()
        Me.listContributors.Items.Clear()
    End Sub

    Public Sub Translate()
        SaveDescription() ' in the previous language = mCurrentLanguage
        mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        SetDescriptionDetailValues()
    End Sub

    Private Sub TranslateGUI()
        Me.lblKeyword.Text = Filemanager.GetOpenEhrTerm(578, Me.lblKeyword.Text) & ":"
        Me.lblStatus.Text = Filemanager.GetOpenEhrTerm(568, Me.lblStatus.Text) & ":"
        Me.lblName.Text = Filemanager.GetOpenEhrTerm(579, Me.lblName.Text) & ":"
        Me.lblEmail.Text = Filemanager.GetOpenEhrTerm(207, Me.lblEmail.Text) & ":"
        Me.lblDate.Text = Filemanager.GetOpenEhrTerm(593, Me.lblDate.Text) & ":"
        Me.lblOrganisation.Text = Filemanager.GetOpenEhrTerm(594, Me.lblOrganisation.Text) & ":"
        Me.tpAuthor.Title = Filemanager.GetOpenEhrTerm(580, Me.tpAuthor.Title)
        Me.tpDescDetails.Title = Filemanager.GetOpenEhrTerm(581, Me.tpDescDetails.Title)
        Me.gbPurpose.Text = Filemanager.GetOpenEhrTerm(585, Me.gbPurpose.Text)
        Me.gbUse.Text = Filemanager.GetOpenEhrTerm(582, Me.gbUse.Text)
        Me.gbMisuse.Text = Filemanager.GetOpenEhrTerm(583, Me.gbMisuse.Text)
        Me.gbAuthor.Text = Filemanager.GetOpenEhrTerm(584, Me.gbAuthor.Text)
    End Sub

    Private Function MakeLifeCycleTable() As DataTable
        ' Create a new DataTable titled 'Languages'
        Dim lifeCycleTable As DataTable = New DataTable("LifeCycles")
        ' Add two column objects to the table.
        'First the language ID = two letter ISO code
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "code"
        lifeCycleTable.Columns.Add(idColumn)
        'Second the language text from windows/ISO list
        Dim LifeCycleColumn As DataColumn = New DataColumn
        LifeCycleColumn.DataType = System.Type.GetType("System.String")
        LifeCycleColumn.ColumnName = "LifeCycle"
        lifeCycleTable.Columns.Add(LifeCycleColumn)
        ' Create an array for DataColumn objects.
        Dim keys(0) As DataColumn
        keys(0) = idColumn
        lifeCycleTable.PrimaryKey = keys
        ' Return the new DataTable.
        Return lifeCycleTable
    End Function

    Private Sub LoadAuthorStatesTableCombo()
        ' add the lifecycle states to the combo box
        Dim i As Integer = Me.comboLifeCycle.SelectedIndex

        If mLifeCycleStatesTable Is Nothing Then
            mLifeCycleStatesTable = MakeLifeCycleTable()
        Else
            mLifeCycleStatesTable.Rows.Clear()
        End If

        Dim d_r As DataRow()
        d_r = Filemanager.Master.OntologyManager.CodeForGroupID(23) ' LifeCycle states
        For Each data_row As DataRow In d_r
            Dim new_row As DataRow = mLifeCycleStatesTable.NewRow
            new_row(0) = data_row(1)
            new_row(1) = data_row(2)
            mLifeCycleStatesTable.Rows.Add(new_row)
        Next
        Me.comboLifeCycle.DataSource = mLifeCycleStatesTable
        Me.comboLifeCycle.DisplayMember = "LifeCycle"
        Me.comboLifeCycle.ValueMember = "code"

        Me.comboLifeCycle.SelectedIndex = i

    End Sub
    Private Sub TabPageDescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        Dim temp_isloading As Boolean = Filemanager.Master.FileLoading
        Filemanager.Master.FileLoading = True
        LoadAuthorStatesTableCombo()
        If OceanArchetypeEditor.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If
        Filemanager.Master.FileLoading = temp_isloading
    End Sub

    Private Sub TextUpdated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMisuse.TextChanged, txtOriginalAuthor.TextChanged, txtOriginalEmail.TextChanged, txtPurpose.TextChanged, comboLifeCycle.SelectedIndexChanged, txtUse.TextChanged

        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If

    End Sub

    Private Sub ButAddKeyWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddKeyWord.Click
        Dim ipb As New InputForm
        ipb.lblInput.Text = Filemanager.GetOpenEhrTerm(578, "Keyword")
        If ipb.ShowDialog = DialogResult.OK Then
            Me.listKeyword.Items.Add(ipb.txtInput.Text)
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub butRemoveKeyWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveKeyWord.Click
        If Me.listKeyword.SelectedIndex > -1 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " - " & CStr(Me.listKeyword.SelectedItem), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.listKeyword.Items.RemoveAt(Me.listKeyword.SelectedIndex)
                Filemanager.Master.FileEdited = True
            End If
        End If
    End Sub

    Private Sub c_menuPasteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c_menuPasteAll.Click, c_menPasteDate.Click, c_menuPasteEmail.Click, c_menuPasteName.Click, c_menuPasteOrg.Click
        If sender Is Me.c_menuPasteAll Or sender Is Me.c_menuPasteName Then
            Me.txtOriginalAuthor.Text = OceanArchetypeEditor.Instance.Options.UserName
        End If
        If sender Is Me.c_menuPasteAll Or sender Is Me.c_menuPasteEmail Then
            Me.txtOriginalEmail.Text = OceanArchetypeEditor.Instance.Options.UserEmail
        End If
        If sender Is Me.c_menuPasteAll Or sender Is Me.c_menuPasteOrg Then
            Me.txtOrganisation.Text = OceanArchetypeEditor.Instance.Options.UserOrganisation
        End If
        If sender Is Me.c_menuPasteAll Or sender Is Me.c_menPasteDate Then
            Me.txtDate.Text = System.DateTime.Now().ToShortDateString
        End If

    End Sub

    Private Sub butAddContributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddContributor.Click
        Dim ipb As New InputForm
        ipb.lblInput.Text = Filemanager.GetOpenEhrTerm(604, "Contributors")
        If ipb.ShowDialog = DialogResult.OK Then
            Me.listContributors.Items.Add(ipb.txtInput.Text)
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub butRemoveContributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveContributor.Click
        If Me.listContributors.SelectedIndex > -1 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " - " & CStr(Me.listContributors.SelectedItem), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.listContributors.Items.RemoveAt(Me.listContributors.SelectedIndex)
                Filemanager.Master.FileEdited = True
            End If
        End If
    End Sub
End Class
