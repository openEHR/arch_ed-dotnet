
Option Strict On

Public Class TabPageDescription
    Inherits System.Windows.Forms.UserControl

    Private mArchetypeDescription As ArchetypeDescription
    Private mLifeCycleStatesTable As DataTable
    Friend WithEvents tpTranslation As Crownwood.Magic.Controls.TabPage
    Friend WithEvents gbTranslator As System.Windows.Forms.GroupBox
    Friend WithEvents lblAccreditation As System.Windows.Forms.Label
    Friend WithEvents txtTranslationAccreditation As System.Windows.Forms.TextBox
    Friend WithEvents lblTranslatorOrganisation As System.Windows.Forms.Label
    Friend WithEvents txtTranslatorOrganisation As System.Windows.Forms.TextBox
    Friend WithEvents lblTranslatorName As System.Windows.Forms.Label
    Friend WithEvents txtTranslatorName As System.Windows.Forms.TextBox
    Friend WithEvents lblTranslatorEmail As System.Windows.Forms.Label
    Friend WithEvents txtTranslatorEmail As System.Windows.Forms.TextBox
    Private mCurrentLanguage As String
    Private mTranslationAltered As Boolean
    Friend WithEvents tpReferences As Crownwood.Magic.Controls.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReferences As System.Windows.Forms.TextBox
    Friend WithEvents UseAllAuthorDefaultsButton As System.Windows.Forms.Button
    Friend WithEvents TodayButton As System.Windows.Forms.Button
    Friend WithEvents UseYourOrganisationButton As System.Windows.Forms.Button
    Friend WithEvents UseYourEmailButton As System.Windows.Forms.Button
    Friend WithEvents UseYourNameButton As System.Windows.Forms.Button
    Friend WithEvents butEditContributor As System.Windows.Forms.Button
    Friend WithEvents CopyrightTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CopyrightLabel As System.Windows.Forms.Label
    Friend WithEvents gbCurrentResponsibility As System.Windows.Forms.GroupBox
    Friend WithEvents CurrentContactButton As System.Windows.Forms.Button
    Friend WithEvents lblCurrentContact As System.Windows.Forms.Label
    Friend WithEvents txtCurrentContact As System.Windows.Forms.TextBox

    Private mIsLoading As Boolean = False


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
    Friend WithEvents listContributors As System.Windows.Forms.ListBox
    Friend WithEvents butRemoveContributor As System.Windows.Forms.Button
    Friend WithEvents butAddContributor As System.Windows.Forms.Button
    Friend WithEvents gbContributors As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabPageDescription))
        Me.lblStatus = New System.Windows.Forms.Label
        Me.txtUse = New System.Windows.Forms.TextBox
        Me.txtMisuse = New System.Windows.Forms.TextBox
        Me.gbUse = New System.Windows.Forms.GroupBox
        Me.gbMisuse = New System.Windows.Forms.GroupBox
        Me.gbPurpose = New System.Windows.Forms.GroupBox
        Me.txtPurpose = New System.Windows.Forms.TextBox
        Me.panelDescription = New System.Windows.Forms.Panel
        Me.CopyrightTextBox = New System.Windows.Forms.TextBox
        Me.CopyrightLabel = New System.Windows.Forms.Label
        Me.ButAddKeyWord = New System.Windows.Forms.Button
        Me.butRemoveKeyWord = New System.Windows.Forms.Button
        Me.comboLifeCycle = New System.Windows.Forms.ComboBox
        Me.lblKeyword = New System.Windows.Forms.Label
        Me.listKeyword = New System.Windows.Forms.ListBox
        Me.TabDescription = New Crownwood.Magic.Controls.TabControl
        Me.tpAuthor = New Crownwood.Magic.Controls.TabPage
        Me.gbCurrentResponsibility = New System.Windows.Forms.GroupBox
        Me.CurrentContactButton = New System.Windows.Forms.Button
        Me.lblCurrentContact = New System.Windows.Forms.Label
        Me.txtCurrentContact = New System.Windows.Forms.TextBox
        Me.gbContributors = New System.Windows.Forms.GroupBox
        Me.butEditContributor = New System.Windows.Forms.Button
        Me.listContributors = New System.Windows.Forms.ListBox
        Me.butAddContributor = New System.Windows.Forms.Button
        Me.butRemoveContributor = New System.Windows.Forms.Button
        Me.gbAuthor = New System.Windows.Forms.GroupBox
        Me.UseAllAuthorDefaultsButton = New System.Windows.Forms.Button
        Me.TodayButton = New System.Windows.Forms.Button
        Me.UseYourOrganisationButton = New System.Windows.Forms.Button
        Me.UseYourEmailButton = New System.Windows.Forms.Button
        Me.UseYourNameButton = New System.Windows.Forms.Button
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
        Me.tpTranslation = New Crownwood.Magic.Controls.TabPage
        Me.gbTranslator = New System.Windows.Forms.GroupBox
        Me.lblAccreditation = New System.Windows.Forms.Label
        Me.txtTranslationAccreditation = New System.Windows.Forms.TextBox
        Me.lblTranslatorOrganisation = New System.Windows.Forms.Label
        Me.txtTranslatorOrganisation = New System.Windows.Forms.TextBox
        Me.lblTranslatorName = New System.Windows.Forms.Label
        Me.txtTranslatorName = New System.Windows.Forms.TextBox
        Me.lblTranslatorEmail = New System.Windows.Forms.Label
        Me.txtTranslatorEmail = New System.Windows.Forms.TextBox
        Me.tpReferences = New Crownwood.Magic.Controls.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtReferences = New System.Windows.Forms.TextBox
        Me.gbUse.SuspendLayout()
        Me.gbMisuse.SuspendLayout()
        Me.gbPurpose.SuspendLayout()
        Me.panelDescription.SuspendLayout()
        Me.tpAuthor.SuspendLayout()
        Me.gbCurrentResponsibility.SuspendLayout()
        Me.gbContributors.SuspendLayout()
        Me.gbAuthor.SuspendLayout()
        Me.tpDescDetails.SuspendLayout()
        Me.tpTranslation.SuspendLayout()
        Me.gbTranslator.SuspendLayout()
        Me.tpReferences.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(16, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(200, 32)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Authorship lifecycle:"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUse
        '
        Me.txtUse.AcceptsReturn = True
        Me.txtUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUse.Location = New System.Drawing.Point(3, 19)
        Me.txtUse.Multiline = True
        Me.txtUse.Name = "txtUse"
        Me.txtUse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUse.Size = New System.Drawing.Size(790, 106)
        Me.txtUse.TabIndex = 4
        '
        'txtMisuse
        '
        Me.txtMisuse.AcceptsReturn = True
        Me.txtMisuse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMisuse.Location = New System.Drawing.Point(3, 19)
        Me.txtMisuse.Multiline = True
        Me.txtMisuse.Name = "txtMisuse"
        Me.txtMisuse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMisuse.Size = New System.Drawing.Size(790, 90)
        Me.txtMisuse.TabIndex = 6
        '
        'gbUse
        '
        Me.gbUse.Controls.Add(Me.txtUse)
        Me.gbUse.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbUse.Location = New System.Drawing.Point(0, 232)
        Me.gbUse.Name = "gbUse"
        Me.gbUse.Size = New System.Drawing.Size(796, 128)
        Me.gbUse.TabIndex = 9
        Me.gbUse.TabStop = False
        Me.gbUse.Text = "Use"
        '
        'gbMisuse
        '
        Me.gbMisuse.Controls.Add(Me.txtMisuse)
        Me.gbMisuse.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbMisuse.Location = New System.Drawing.Point(0, 363)
        Me.gbMisuse.Name = "gbMisuse"
        Me.gbMisuse.Size = New System.Drawing.Size(796, 112)
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
        Me.gbPurpose.Size = New System.Drawing.Size(561, 229)
        Me.gbPurpose.TabIndex = 0
        Me.gbPurpose.TabStop = False
        Me.gbPurpose.Text = "Purpose"
        '
        'txtPurpose
        '
        Me.txtPurpose.AcceptsReturn = True
        Me.txtPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPurpose.Location = New System.Drawing.Point(3, 19)
        Me.txtPurpose.Multiline = True
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPurpose.Size = New System.Drawing.Size(555, 207)
        Me.txtPurpose.TabIndex = 0
        '
        'panelDescription
        '
        Me.panelDescription.Controls.Add(Me.CopyrightTextBox)
        Me.panelDescription.Controls.Add(Me.CopyrightLabel)
        Me.panelDescription.Controls.Add(Me.ButAddKeyWord)
        Me.panelDescription.Controls.Add(Me.butRemoveKeyWord)
        Me.panelDescription.Controls.Add(Me.comboLifeCycle)
        Me.panelDescription.Controls.Add(Me.lblKeyword)
        Me.panelDescription.Controls.Add(Me.listKeyword)
        Me.panelDescription.Controls.Add(Me.lblStatus)
        Me.panelDescription.Dock = System.Windows.Forms.DockStyle.Right
        Me.panelDescription.Location = New System.Drawing.Point(564, 0)
        Me.panelDescription.Name = "panelDescription"
        Me.panelDescription.Size = New System.Drawing.Size(232, 229)
        Me.panelDescription.TabIndex = 1
        '
        'CopyrightTextBox
        '
        Me.CopyrightTextBox.Location = New System.Drawing.Point(16, 82)
        Me.CopyrightTextBox.Name = "CopyrightTextBox"
        Me.CopyrightTextBox.Size = New System.Drawing.Size(200, 23)
        Me.CopyrightTextBox.TabIndex = 3
        '
        'CopyrightLabel
        '
        Me.CopyrightLabel.Location = New System.Drawing.Point(16, 50)
        Me.CopyrightLabel.Name = "CopyrightLabel"
        Me.CopyrightLabel.Size = New System.Drawing.Size(200, 32)
        Me.CopyrightLabel.TabIndex = 2
        Me.CopyrightLabel.Text = "Copyright:"
        Me.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ButAddKeyWord
        '
        Me.ButAddKeyWord.BackColor = System.Drawing.Color.Transparent
        Me.ButAddKeyWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButAddKeyWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButAddKeyWord.Image = CType(resources.GetObject("ButAddKeyWord.Image"), System.Drawing.Image)
        Me.ButAddKeyWord.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButAddKeyWord.Location = New System.Drawing.Point(16, 130)
        Me.ButAddKeyWord.Name = "ButAddKeyWord"
        Me.ButAddKeyWord.Size = New System.Drawing.Size(24, 25)
        Me.ButAddKeyWord.TabIndex = 34
        Me.ButAddKeyWord.UseVisualStyleBackColor = False
        '
        'butRemoveKeyWord
        '
        Me.butRemoveKeyWord.BackColor = System.Drawing.Color.Transparent
        Me.butRemoveKeyWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveKeyWord.Image = CType(resources.GetObject("butRemoveKeyWord.Image"), System.Drawing.Image)
        Me.butRemoveKeyWord.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveKeyWord.Location = New System.Drawing.Point(16, 162)
        Me.butRemoveKeyWord.Name = "butRemoveKeyWord"
        Me.butRemoveKeyWord.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveKeyWord.TabIndex = 35
        Me.butRemoveKeyWord.UseVisualStyleBackColor = False
        '
        'comboLifeCycle
        '
        Me.comboLifeCycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboLifeCycle.Location = New System.Drawing.Point(16, 26)
        Me.comboLifeCycle.Name = "comboLifeCycle"
        Me.comboLifeCycle.Size = New System.Drawing.Size(200, 23)
        Me.comboLifeCycle.TabIndex = 1
        '
        'lblKeyword
        '
        Me.lblKeyword.Location = New System.Drawing.Point(49, 106)
        Me.lblKeyword.Name = "lblKeyword"
        Me.lblKeyword.Size = New System.Drawing.Size(112, 24)
        Me.lblKeyword.TabIndex = 4
        Me.lblKeyword.Text = "Keywords:"
        Me.lblKeyword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'listKeyword
        '
        Me.listKeyword.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listKeyword.ItemHeight = 15
        Me.listKeyword.Location = New System.Drawing.Point(48, 130)
        Me.listKeyword.Name = "listKeyword"
        Me.listKeyword.Size = New System.Drawing.Size(168, 79)
        Me.listKeyword.TabIndex = 36
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
        Me.TabDescription.Size = New System.Drawing.Size(796, 500)
        Me.TabDescription.TabIndex = 15
        Me.TabDescription.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpDescDetails, Me.tpAuthor, Me.tpTranslation, Me.tpReferences})
        '
        'tpAuthor
        '
        Me.tpAuthor.Controls.Add(Me.gbCurrentResponsibility)
        Me.tpAuthor.Controls.Add(Me.gbContributors)
        Me.tpAuthor.Controls.Add(Me.gbAuthor)
        Me.tpAuthor.Location = New System.Drawing.Point(0, 0)
        Me.tpAuthor.Name = "tpAuthor"
        Me.tpAuthor.Size = New System.Drawing.Size(796, 475)
        Me.tpAuthor.TabIndex = 1
        Me.tpAuthor.Title = "Authorship"
        '
        'gbCurrentResponsibility
        '
        Me.gbCurrentResponsibility.Controls.Add(Me.CurrentContactButton)
        Me.gbCurrentResponsibility.Controls.Add(Me.lblCurrentContact)
        Me.gbCurrentResponsibility.Controls.Add(Me.txtCurrentContact)
        Me.gbCurrentResponsibility.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbCurrentResponsibility.Location = New System.Drawing.Point(0, 328)
        Me.gbCurrentResponsibility.Name = "gbCurrentResponsibility"
        Me.gbCurrentResponsibility.Size = New System.Drawing.Size(796, 81)
        Me.gbCurrentResponsibility.TabIndex = 44
        Me.gbCurrentResponsibility.TabStop = False
        Me.gbCurrentResponsibility.Text = "Currently responsible"
        '
        'CurrentContactButton
        '
        Me.CurrentContactButton.Location = New System.Drawing.Point(547, 35)
        Me.CurrentContactButton.Name = "CurrentContactButton"
        Me.CurrentContactButton.Size = New System.Drawing.Size(229, 23)
        Me.CurrentContactButton.TabIndex = 9
        Me.CurrentContactButton.Text = "Use your Author details"
        Me.CurrentContactButton.UseVisualStyleBackColor = True
        '
        'lblCurrentContact
        '
        Me.lblCurrentContact.Location = New System.Drawing.Point(20, 33)
        Me.lblCurrentContact.Name = "lblCurrentContact"
        Me.lblCurrentContact.Size = New System.Drawing.Size(89, 24)
        Me.lblCurrentContact.TabIndex = 0
        Me.lblCurrentContact.Text = "Contact:"
        Me.lblCurrentContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrentContact
        '
        Me.txtCurrentContact.Location = New System.Drawing.Point(115, 35)
        Me.txtCurrentContact.Name = "txtCurrentContact"
        Me.txtCurrentContact.Size = New System.Drawing.Size(426, 23)
        Me.txtCurrentContact.TabIndex = 1
        '
        'gbContributors
        '
        Me.gbContributors.Controls.Add(Me.butEditContributor)
        Me.gbContributors.Controls.Add(Me.listContributors)
        Me.gbContributors.Controls.Add(Me.butAddContributor)
        Me.gbContributors.Controls.Add(Me.butRemoveContributor)
        Me.gbContributors.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbContributors.Location = New System.Drawing.Point(0, 152)
        Me.gbContributors.Name = "gbContributors"
        Me.gbContributors.Size = New System.Drawing.Size(796, 176)
        Me.gbContributors.TabIndex = 40
        Me.gbContributors.TabStop = False
        Me.gbContributors.Text = "Contributors"
        '
        'butEditContributor
        '
        Me.butEditContributor.BackColor = System.Drawing.Color.Transparent
        Me.butEditContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butEditContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butEditContributor.Location = New System.Drawing.Point(32, 88)
        Me.butEditContributor.Name = "butEditContributor"
        Me.butEditContributor.Size = New System.Drawing.Size(24, 25)
        Me.butEditContributor.TabIndex = 40
        Me.butEditContributor.Text = "..."
        Me.butEditContributor.UseVisualStyleBackColor = False
        '
        'listContributors
        '
        Me.listContributors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listContributors.ItemHeight = 15
        Me.listContributors.Location = New System.Drawing.Point(64, 24)
        Me.listContributors.Name = "listContributors"
        Me.listContributors.Size = New System.Drawing.Size(712, 124)
        Me.listContributors.TabIndex = 36
        '
        'butAddContributor
        '
        Me.butAddContributor.BackColor = System.Drawing.Color.Transparent
        Me.butAddContributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddContributor.Image = CType(resources.GetObject("butAddContributor.Image"), System.Drawing.Image)
        Me.butAddContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddContributor.Location = New System.Drawing.Point(32, 24)
        Me.butAddContributor.Name = "butAddContributor"
        Me.butAddContributor.Size = New System.Drawing.Size(24, 25)
        Me.butAddContributor.TabIndex = 38
        Me.butAddContributor.UseVisualStyleBackColor = False
        '
        'butRemoveContributor
        '
        Me.butRemoveContributor.BackColor = System.Drawing.Color.Transparent
        Me.butRemoveContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveContributor.Image = CType(resources.GetObject("butRemoveContributor.Image"), System.Drawing.Image)
        Me.butRemoveContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveContributor.Location = New System.Drawing.Point(32, 56)
        Me.butRemoveContributor.Name = "butRemoveContributor"
        Me.butRemoveContributor.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveContributor.TabIndex = 39
        Me.butRemoveContributor.UseVisualStyleBackColor = False
        '
        'gbAuthor
        '
        Me.gbAuthor.Controls.Add(Me.UseAllAuthorDefaultsButton)
        Me.gbAuthor.Controls.Add(Me.TodayButton)
        Me.gbAuthor.Controls.Add(Me.UseYourOrganisationButton)
        Me.gbAuthor.Controls.Add(Me.UseYourEmailButton)
        Me.gbAuthor.Controls.Add(Me.UseYourNameButton)
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
        Me.gbAuthor.Size = New System.Drawing.Size(796, 152)
        Me.gbAuthor.TabIndex = 4
        Me.gbAuthor.TabStop = False
        Me.gbAuthor.Text = "Original author"
        '
        'UseAllAuthorDefaultsButton
        '
        Me.UseAllAuthorDefaultsButton.Location = New System.Drawing.Point(547, 117)
        Me.UseAllAuthorDefaultsButton.Name = "UseAllAuthorDefaultsButton"
        Me.UseAllAuthorDefaultsButton.Size = New System.Drawing.Size(229, 23)
        Me.UseAllAuthorDefaultsButton.TabIndex = 12
        Me.UseAllAuthorDefaultsButton.Text = "Use all Author defaults"
        Me.UseAllAuthorDefaultsButton.UseVisualStyleBackColor = True
        '
        'TodayButton
        '
        Me.TodayButton.Location = New System.Drawing.Point(283, 116)
        Me.TodayButton.Name = "TodayButton"
        Me.TodayButton.Size = New System.Drawing.Size(158, 23)
        Me.TodayButton.TabIndex = 11
        Me.TodayButton.Text = "Today"
        Me.TodayButton.UseVisualStyleBackColor = True
        '
        'UseYourOrganisationButton
        '
        Me.UseYourOrganisationButton.Location = New System.Drawing.Point(547, 84)
        Me.UseYourOrganisationButton.Name = "UseYourOrganisationButton"
        Me.UseYourOrganisationButton.Size = New System.Drawing.Size(229, 23)
        Me.UseYourOrganisationButton.TabIndex = 8
        Me.UseYourOrganisationButton.Text = "Use your Organisation"
        Me.UseYourOrganisationButton.UseVisualStyleBackColor = True
        '
        'UseYourEmailButton
        '
        Me.UseYourEmailButton.Location = New System.Drawing.Point(547, 52)
        Me.UseYourEmailButton.Name = "UseYourEmailButton"
        Me.UseYourEmailButton.Size = New System.Drawing.Size(229, 23)
        Me.UseYourEmailButton.TabIndex = 5
        Me.UseYourEmailButton.Text = "Use your Email"
        Me.UseYourEmailButton.UseVisualStyleBackColor = True
        '
        'UseYourNameButton
        '
        Me.UseYourNameButton.Location = New System.Drawing.Point(547, 20)
        Me.UseYourNameButton.Name = "UseYourNameButton"
        Me.UseYourNameButton.Size = New System.Drawing.Size(229, 23)
        Me.UseYourNameButton.TabIndex = 2
        Me.UseYourNameButton.Text = "Use your Name"
        Me.UseYourNameButton.UseVisualStyleBackColor = True
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(14, 116)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(95, 24)
        Me.lblDate.TabIndex = 9
        Me.lblDate.Text = "Date:"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(117, 116)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(160, 23)
        Me.txtDate.TabIndex = 10
        '
        'lblOrganisation
        '
        Me.lblOrganisation.Location = New System.Drawing.Point(14, 84)
        Me.lblOrganisation.Name = "lblOrganisation"
        Me.lblOrganisation.Size = New System.Drawing.Size(95, 24)
        Me.lblOrganisation.TabIndex = 6
        Me.lblOrganisation.Text = "Organisation:"
        Me.lblOrganisation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOrganisation
        '
        Me.txtOrganisation.Location = New System.Drawing.Point(117, 84)
        Me.txtOrganisation.Name = "txtOrganisation"
        Me.txtOrganisation.Size = New System.Drawing.Size(424, 23)
        Me.txtOrganisation.TabIndex = 7
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(14, 20)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(95, 24)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name:"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalAuthor
        '
        Me.txtOriginalAuthor.Location = New System.Drawing.Point(117, 20)
        Me.txtOriginalAuthor.Name = "txtOriginalAuthor"
        Me.txtOriginalAuthor.Size = New System.Drawing.Size(424, 23)
        Me.txtOriginalAuthor.TabIndex = 1
        '
        'lblEmail
        '
        Me.lblEmail.Location = New System.Drawing.Point(14, 52)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(95, 24)
        Me.lblEmail.TabIndex = 3
        Me.lblEmail.Text = "Email:"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalEmail
        '
        Me.txtOriginalEmail.Location = New System.Drawing.Point(117, 52)
        Me.txtOriginalEmail.Name = "txtOriginalEmail"
        Me.txtOriginalEmail.Size = New System.Drawing.Size(424, 23)
        Me.txtOriginalEmail.TabIndex = 4
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
        Me.tpDescDetails.Size = New System.Drawing.Size(796, 475)
        Me.tpDescDetails.TabIndex = 0
        Me.tpDescDetails.Title = "Details"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter3.Location = New System.Drawing.Point(561, 0)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(3, 229)
        Me.Splitter3.TabIndex = 17
        Me.Splitter3.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(0, 229)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(796, 3)
        Me.Splitter2.TabIndex = 16
        Me.Splitter2.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 360)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(796, 3)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'tpTranslation
        '
        Me.tpTranslation.BackColor = System.Drawing.Color.LightBlue
        Me.tpTranslation.Controls.Add(Me.gbTranslator)
        Me.tpTranslation.Location = New System.Drawing.Point(0, 0)
        Me.tpTranslation.Name = "tpTranslation"
        Me.tpTranslation.Selected = False
        Me.tpTranslation.Size = New System.Drawing.Size(796, 475)
        Me.tpTranslation.TabIndex = 2
        Me.tpTranslation.Title = "Translation"
        '
        'gbTranslator
        '
        Me.gbTranslator.Controls.Add(Me.lblAccreditation)
        Me.gbTranslator.Controls.Add(Me.txtTranslationAccreditation)
        Me.gbTranslator.Controls.Add(Me.lblTranslatorOrganisation)
        Me.gbTranslator.Controls.Add(Me.txtTranslatorOrganisation)
        Me.gbTranslator.Controls.Add(Me.lblTranslatorName)
        Me.gbTranslator.Controls.Add(Me.txtTranslatorName)
        Me.gbTranslator.Controls.Add(Me.lblTranslatorEmail)
        Me.gbTranslator.Controls.Add(Me.txtTranslatorEmail)
        Me.gbTranslator.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbTranslator.Location = New System.Drawing.Point(0, 0)
        Me.gbTranslator.Name = "gbTranslator"
        Me.gbTranslator.Size = New System.Drawing.Size(796, 157)
        Me.gbTranslator.TabIndex = 5
        Me.gbTranslator.TabStop = False
        Me.gbTranslator.Text = "Translator"
        '
        'lblAccreditation
        '
        Me.lblAccreditation.Location = New System.Drawing.Point(16, 120)
        Me.lblAccreditation.Name = "lblAccreditation"
        Me.lblAccreditation.Size = New System.Drawing.Size(128, 24)
        Me.lblAccreditation.TabIndex = 7
        Me.lblAccreditation.Text = "Accreditation"
        Me.lblAccreditation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranslationAccreditation
        '
        Me.txtTranslationAccreditation.Location = New System.Drawing.Point(152, 120)
        Me.txtTranslationAccreditation.Name = "txtTranslationAccreditation"
        Me.txtTranslationAccreditation.Size = New System.Drawing.Size(160, 23)
        Me.txtTranslationAccreditation.TabIndex = 6
        '
        'lblTranslatorOrganisation
        '
        Me.lblTranslatorOrganisation.Location = New System.Drawing.Point(16, 88)
        Me.lblTranslatorOrganisation.Name = "lblTranslatorOrganisation"
        Me.lblTranslatorOrganisation.Size = New System.Drawing.Size(128, 24)
        Me.lblTranslatorOrganisation.TabIndex = 5
        Me.lblTranslatorOrganisation.Text = "Organisation"
        Me.lblTranslatorOrganisation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranslatorOrganisation
        '
        Me.txtTranslatorOrganisation.Location = New System.Drawing.Point(152, 88)
        Me.txtTranslatorOrganisation.Name = "txtTranslatorOrganisation"
        Me.txtTranslatorOrganisation.Size = New System.Drawing.Size(424, 23)
        Me.txtTranslatorOrganisation.TabIndex = 4
        '
        'lblTranslatorName
        '
        Me.lblTranslatorName.Location = New System.Drawing.Point(16, 24)
        Me.lblTranslatorName.Name = "lblTranslatorName"
        Me.lblTranslatorName.Size = New System.Drawing.Size(128, 24)
        Me.lblTranslatorName.TabIndex = 2
        Me.lblTranslatorName.Text = "Name"
        Me.lblTranslatorName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranslatorName
        '
        Me.txtTranslatorName.Location = New System.Drawing.Point(152, 24)
        Me.txtTranslatorName.Name = "txtTranslatorName"
        Me.txtTranslatorName.Size = New System.Drawing.Size(424, 23)
        Me.txtTranslatorName.TabIndex = 0
        '
        'lblTranslatorEmail
        '
        Me.lblTranslatorEmail.Location = New System.Drawing.Point(16, 56)
        Me.lblTranslatorEmail.Name = "lblTranslatorEmail"
        Me.lblTranslatorEmail.Size = New System.Drawing.Size(128, 24)
        Me.lblTranslatorEmail.TabIndex = 3
        Me.lblTranslatorEmail.Text = "Email"
        Me.lblTranslatorEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTranslatorEmail
        '
        Me.txtTranslatorEmail.Location = New System.Drawing.Point(152, 56)
        Me.txtTranslatorEmail.Name = "txtTranslatorEmail"
        Me.txtTranslatorEmail.Size = New System.Drawing.Size(424, 23)
        Me.txtTranslatorEmail.TabIndex = 1
        '
        'tpReferences
        '
        Me.tpReferences.Controls.Add(Me.GroupBox1)
        Me.tpReferences.Location = New System.Drawing.Point(0, 0)
        Me.tpReferences.Name = "tpReferences"
        Me.tpReferences.Selected = False
        Me.tpReferences.Size = New System.Drawing.Size(796, 475)
        Me.tpReferences.TabIndex = 3
        Me.tpReferences.Title = "References"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtReferences)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(796, 475)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        '
        'txtReferences
        '
        Me.txtReferences.AcceptsReturn = True
        Me.txtReferences.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReferences.Location = New System.Drawing.Point(3, 19)
        Me.txtReferences.Multiline = True
        Me.txtReferences.Name = "txtReferences"
        Me.txtReferences.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReferences.Size = New System.Drawing.Size(790, 453)
        Me.txtReferences.TabIndex = 0
        '
        'TabPageDescription
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.TabDescription)
        Me.Name = "TabPageDescription"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(800, 504)
        Me.gbUse.ResumeLayout(False)
        Me.gbUse.PerformLayout()
        Me.gbMisuse.ResumeLayout(False)
        Me.gbMisuse.PerformLayout()
        Me.gbPurpose.ResumeLayout(False)
        Me.gbPurpose.PerformLayout()
        Me.panelDescription.ResumeLayout(False)
        Me.panelDescription.PerformLayout()
        Me.tpAuthor.ResumeLayout(False)
        Me.gbCurrentResponsibility.ResumeLayout(False)
        Me.gbCurrentResponsibility.PerformLayout()
        Me.gbContributors.ResumeLayout(False)
        Me.gbAuthor.ResumeLayout(False)
        Me.gbAuthor.PerformLayout()
        Me.tpDescDetails.ResumeLayout(False)
        Me.tpTranslation.ResumeLayout(False)
        Me.gbTranslator.ResumeLayout(False)
        Me.gbTranslator.PerformLayout()
        Me.tpReferences.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
            comboLifeCycle.SelectedValue = CInt(Value.LifeCycleState)
            txtOriginalAuthor.Text = Value.OriginalAuthor
            txtOriginalEmail.Text = Value.OriginalAuthorEmail
            txtOrganisation.Text = Value.OriginalAuthorOrganisation
            txtDate.Text = Value.OriginalAuthorDate
            listContributors.Items.Clear()

            For Each s As String In Value.OtherContributors
                listContributors.Items.Add(s)
            Next

            txtCurrentContact.Text = Value.CurrentContact
            txtReferences.Text = NewlinesNormalised(Value.References)
            mArchetypeDescription = Value
            mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
            SetDescriptionDetailValues(False)
        End Set
    End Property

    Dim mTranslationDetails As Generic.SortedList(Of String, TranslationDetails) = New Generic.SortedList(Of String, TranslationDetails)

    Public Property TranslationDetails() As Generic.SortedList(Of String, TranslationDetails)
        Get
            SaveTranslation()
            Return mTranslationDetails
        End Get
        Set(ByVal value As Generic.SortedList(Of String, TranslationDetails))
            mTranslationDetails = value
            SetTranslationValues()
        End Set
    End Property

    Private Sub SaveTranslation()
        If mTranslationAltered Then
            Dim t As TranslationDetails = Nothing

            If mTranslationDetails.Keys.Contains(mCurrentLanguage) Then
                t = mTranslationDetails.Item(mCurrentLanguage)
            Else
                Select Case Filemanager.Master.ParserType
                    Case "adl"
                        t = New ADL_TranslationDetails(mCurrentLanguage)
                    Case "xml"
                        t = New XML_TranslationDetails(mCurrentLanguage)
                End Select
            End If

            t.Accreditation = txtTranslationAccreditation.Text
            t.AuthorName = txtTranslatorName.Text
            t.AuthorOrganisation = txtTranslatorOrganisation.Text
            t.AuthorEmail = txtTranslatorEmail.Text

            If mTranslationDetails.Keys.Contains(t.Language) Then
                mTranslationDetails.Item(t.Language) = t
            Else
                mTranslationDetails.Add(t.Language, t)
            End If
        End If

        mTranslationAltered = False
    End Sub

    Private Sub SaveDescription()
        If mArchetypeDescription Is Nothing Then
            Select Case Filemanager.Master.ParserType
                Case "adl"
                    mArchetypeDescription = New ArchetypeEditor.ADL_Classes.ADL_Description(Filemanager.Master.OntologyManager.PrimaryLanguageCode)
                Case "xml"
                    mArchetypeDescription = New ArchetypeEditor.XML_Classes.XML_Description
            End Select
        End If

        If comboLifeCycle.SelectedIndex > -1 Then
            mArchetypeDescription.LifeCycleState = CType([Enum].ToObject(GetType(LifeCycleStates), Convert.ToInt32(comboLifeCycle.SelectedValue)), LifeCycleStates)
        Else
            'set lifecycle to not set
            mArchetypeDescription.LifeCycleState = CType([Enum].ToObject(GetType(LifeCycleStates), 0), LifeCycleStates)
        End If

        mArchetypeDescription.OriginalAuthor = txtOriginalAuthor.Text
        mArchetypeDescription.OriginalAuthorEmail = txtOriginalEmail.Text
        mArchetypeDescription.OriginalAuthorOrganisation = txtOrganisation.Text
        mArchetypeDescription.OriginalAuthorDate = txtDate.Text
        mArchetypeDescription.References = txtReferences.Text
        mArchetypeDescription.CurrentContact = txtCurrentContact.Text
        mArchetypeDescription.OtherContributors.Clear()

        For Each s As String In listContributors.Items
            mArchetypeDescription.OtherContributors.Add(s)
        Next

        If mCurrentLanguage Is Nothing Then
            mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        End If

        Dim archDescriptionItem As New ArchetypeDescriptionItem(mCurrentLanguage)
        Dim existingArchetypeDetails As ArchetypeDescriptionItem = mArchetypeDescription.Details.DetailInLanguage(mCurrentLanguage)
        archDescriptionItem.Copyright = existingArchetypeDetails.Copyright

        For Each s As String In listKeyword.Items
            archDescriptionItem.KeyWords.Add(s)
        Next

        archDescriptionItem.Purpose = txtPurpose.Text
        archDescriptionItem.Use = txtUse.Text
        archDescriptionItem.MisUse = txtMisuse.Text
        archDescriptionItem.Copyright = CopyrightTextBox.Text
        mArchetypeDescription.Details.AddOrReplace(archDescriptionItem.Language, archDescriptionItem)
    End Sub

    Public Function AsRtfString() As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder()

        result.AppendLine("{\rtf1\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 Tahoma;}{\f1\fnil\fcharset2 Symbol;}}")
        result.AppendLine("{\colortbl ;\red0\green0\blue255;\red0\green255\blue0;}")
        result.AppendLine("\viewkind4\uc1\pard\tx2840\tx5112\lang3081\f0\fs20")

        result.AppendLine("\b")
        result.AppendLine(Filemanager.GetOpenEhrTerm(585, "Purpose"))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.CreateRichTextBoxTag("", RichTextBoxUnicode.RichTextDataType.ArchetypePurpose))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(Filemanager.GetOpenEhrTerm(582, "Use"))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.CreateRichTextBoxTag("", RichTextBoxUnicode.RichTextDataType.ArchetypeUse))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(Filemanager.GetOpenEhrTerm(583, "Misuse"))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.CreateRichTextBoxTag("", RichTextBoxUnicode.RichTextDataType.ArchetypeMisuse))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(Filemanager.GetOpenEhrTerm(690, "Copyright"))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.CreateRichTextBoxTag("", RichTextBoxUnicode.RichTextDataType.ArchetypeCopyright))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(Filemanager.GetOpenEhrTerm(691, "References"))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.CreateRichTextBoxTag("", RichTextBoxUnicode.RichTextDataType.ArchetypeReferences))
        result.AppendLine("\par")
        result.AppendLine("\par")

        Return result.ToString()
    End Function

    Protected Overridable Function NewlinesNormalised(ByVal s As String) As String
        NewlinesNormalised = ""

        If s IsNot Nothing Then
            NewlinesNormalised = System.Text.RegularExpressions.Regex.Replace(s, "(\r\n|\n)", Environment.NewLine)
        End If
    End Function

    Private Sub SetDescriptionDetailValues(ByVal translate As Boolean)
        Dim archDescriptionItem As ArchetypeDescriptionItem
        listKeyword.Items.Clear()

        If mArchetypeDescription.Details.HasDetailInLanguage(mCurrentLanguage) Then
            archDescriptionItem = mArchetypeDescription.Details.DetailInLanguage(mCurrentLanguage)

            For Each s As String In archDescriptionItem.KeyWords
                listKeyword.Items.Add(s)
            Next

            txtPurpose.Text = NewlinesNormalised(archDescriptionItem.Purpose)
            txtUse.Text = NewlinesNormalised(archDescriptionItem.Use)
            txtMisuse.Text = NewlinesNormalised(archDescriptionItem.MisUse)
            CopyrightTextBox.Text = archDescriptionItem.Copyright
        ElseIf translate Then
            If mArchetypeDescription.Details.HasDetailInLanguage(Filemanager.Master.OntologyManager.PrimaryLanguageCode) Then
                archDescriptionItem = mArchetypeDescription.Details.DetailInLanguage(Filemanager.Master.OntologyManager.PrimaryLanguageCode)

                For Each s As String In archDescriptionItem.KeyWords
                    listKeyword.Items.Add(String.Format("*{0}({1})", s, Filemanager.Master.OntologyManager.PrimaryLanguageCode))
                Next

                txtPurpose.Text = String.Format("*{0}({1})", NewlinesNormalised(archDescriptionItem.Purpose), Filemanager.Master.OntologyManager.PrimaryLanguageCode)
                txtUse.Text = String.Format("*{0}({1})", NewlinesNormalised(archDescriptionItem.Use), Filemanager.Master.OntologyManager.PrimaryLanguageCode)
                txtMisuse.Text = String.Format("*{0}({1})", NewlinesNormalised(archDescriptionItem.MisUse), Filemanager.Master.OntologyManager.PrimaryLanguageCode)
                CopyrightTextBox.Text = String.Format("*{0}({1})", archDescriptionItem.Copyright, Filemanager.Master.OntologyManager.PrimaryLanguageCode)
            Else
                If txtPurpose.Text <> "" Then
                    txtPurpose.Text = String.Format("*{0}({1})", txtPurpose.Text, mCurrentLanguage)
                End If

                If txtUse.Text <> "" Then
                    txtUse.Text = String.Format("*{0}({1})", txtUse.Text, mCurrentLanguage)
                End If

                If txtMisuse.Text <> "" Then
                    txtMisuse.Text = String.Format("*{0}({1})", txtMisuse.Text, mCurrentLanguage)
                End If

                If CopyrightTextBox.Text <> "" Then
                    CopyrightTextBox.Text = String.Format("*{0}({1})", CopyrightTextBox.Text, mCurrentLanguage)
                End If
            End If
        End If
    End Sub

    Private Sub SetTranslationValues()
        mIsLoading = True

        If mCurrentLanguage = Filemanager.Master.OntologyManager.PrimaryLanguageCode Then
            txtTranslationAccreditation.Text = ""
            txtTranslatorEmail.Text = ""
            txtTranslatorName.Text = ""
            txtTranslatorOrganisation.Text = ""
            gbTranslator.Visible = False
        Else
            gbTranslator.Visible = True
            Dim t As TranslationDetails = Nothing

            If mTranslationDetails.Keys.Contains(mCurrentLanguage) Then
                t = mTranslationDetails.Item(mCurrentLanguage)
            Else
                Select Case Filemanager.Master.ParserType
                    Case "adl"
                        t = New ADL_TranslationDetails(mCurrentLanguage)
                    Case "xml"
                        t = New XML_TranslationDetails(mCurrentLanguage)
                End Select

                mTranslationAltered = True
                Filemanager.Master.FileEdited = True
            End If

            txtTranslatorName.Text = t.AuthorName
            txtTranslationAccreditation.Text = t.Accreditation
            txtTranslatorEmail.Text = t.AuthorEmail
            txtTranslatorOrganisation.Text = t.AuthorOrganisation
        End If

        mIsLoading = False
    End Sub

    Public Sub Reset()
        txtOriginalAuthor.Text = Main.Instance.Options.UserName
        txtOriginalEmail.Text = Main.Instance.Options.UserEmail
        txtPurpose.Text = ""
        comboLifeCycle.SelectedIndex = -1
        txtUse.Text = ""
        txtMisuse.Text = ""
        CopyrightTextBox.Text = ""
        txtReferences.Text = ""
        listKeyword.Items.Clear()
        listContributors.Items.Clear()
        txtTranslationAccreditation.Text = ""
        txtTranslatorEmail.Text = ""
        txtTranslatorName.Text = ""
        txtTranslatorOrganisation.Text = ""
        mTranslationAltered = False
        gbTranslator.Visible = False
    End Sub

    Public Sub Translate()
        Dim fileState As Boolean = Filemanager.Master.FileLoading
        Filemanager.Master.FileLoading = True
        SaveDescription() ' in the previous language = mCurrentLanguage
        SaveTranslation()
        ' Now mark the texts as needing translation in case this is a new language
        mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        SetDescriptionDetailValues(True)
        SetTranslationValues()
        Filemanager.Master.FileLoading = fileState
    End Sub

    Private Sub TranslateGUI()
        lblKeyword.Text = Filemanager.GetOpenEhrTerm(578, lblKeyword.Text)
        lblStatus.Text = Filemanager.GetOpenEhrTerm(568, lblStatus.Text)
        lblName.Text = Filemanager.GetOpenEhrTerm(579, lblName.Text)
        lblTranslatorName.Text = lblName.Text
        lblEmail.Text = Filemanager.GetOpenEhrTerm(207, lblEmail.Text)
        lblTranslatorEmail.Text = lblEmail.Text
        lblDate.Text = Filemanager.GetOpenEhrTerm(593, lblDate.Text)
        lblAccreditation.Text = Filemanager.GetOpenEhrTerm(651, lblAccreditation.Text)
        lblOrganisation.Text = Filemanager.GetOpenEhrTerm(594, lblOrganisation.Text)
        lblTranslatorOrganisation.Text = lblOrganisation.Text
        tpAuthor.Title = Filemanager.GetOpenEhrTerm(580, tpAuthor.Title)
        tpDescDetails.Title = Filemanager.GetOpenEhrTerm(581, tpDescDetails.Title)
        tpTranslation.Title = Filemanager.GetOpenEhrTerm(650, tpTranslation.Title)
        tpReferences.Title = Filemanager.GetOpenEhrTerm(691, tpReferences.Title)
        gbPurpose.Text = Filemanager.GetOpenEhrTerm(585, gbPurpose.Text)
        gbUse.Text = Filemanager.GetOpenEhrTerm(582, gbUse.Text)
        gbMisuse.Text = Filemanager.GetOpenEhrTerm(583, gbMisuse.Text)
        CopyrightLabel.Text = Filemanager.GetOpenEhrTerm(690, CopyrightLabel.Text)
        gbAuthor.Text = Filemanager.GetOpenEhrTerm(584, gbAuthor.Text)
        gbContributors.Text = Filemanager.GetOpenEhrTerm(604, gbContributors.Text)
        gbCurrentResponsibility.Text = Filemanager.GetOpenEhrTerm(705, gbCurrentResponsibility.Text)
        lblCurrentContact.Text = Filemanager.GetOpenEhrTerm(704, lblCurrentContact.Text)
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
        Dim i As Integer = comboLifeCycle.SelectedIndex

        'If the table is only cleared then the second time the
        ' table is cleared and reloaded an index error occurs
        ' which does not appear to be in the data.
        'For this reason the table is rebuilt
        '
        'Uncomment the following lines to debug

        'If mLifeCycleStatesTable Is Nothing Then
        mLifeCycleStatesTable = MakeLifeCycleTable()
        'Else
        '    mLifeCycleStatesTable.Rows.Clear()
        'End If

        'Debug.Assert(mLifeCycleStatesTable.Rows.Count = 0, "Did not clear lifecycletable")

        Dim row As DataRow() = Filemanager.Master.OntologyManager.CodeForGroupID(23) ' LifeCycle states

        For Each data_row As DataRow In row
            Dim new_row As DataRow = mLifeCycleStatesTable.NewRow
            new_row(0) = data_row(1)
            new_row(1) = data_row(2)
            mLifeCycleStatesTable.Rows.Add(new_row)
        Next

        mLifeCycleStatesTable.AcceptChanges()
        comboLifeCycle.DataSource = mLifeCycleStatesTable
        comboLifeCycle.DisplayMember = "LifeCycle"
        comboLifeCycle.ValueMember = "code"
        comboLifeCycle.SelectedIndex = i
    End Sub

    Private Sub TabPageDescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        Dim temp_isloading As Boolean = Filemanager.Master.FileLoading
        Filemanager.Master.FileLoading = True

        If Main.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        Filemanager.Master.FileLoading = temp_isloading
    End Sub

    Private Sub TextUpdated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOriginalAuthor.TextChanged, txtOriginalEmail.TextChanged, txtPurpose.TextChanged, comboLifeCycle.SelectedIndexChanged, txtUse.TextChanged, txtMisuse.TextChanged, CopyrightTextBox.TextChanged, txtReferences.TextChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub ButAddKeyWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddKeyWord.Click
        Dim ipb As New InputForm
        ipb.lblInput.Text = Filemanager.GetOpenEhrTerm(578, "Keyword")

        If ipb.ShowDialog(ParentForm) = Windows.Forms.DialogResult.OK Then
            listKeyword.Items.Add(ipb.txtInput.Text)
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub butRemoveKeyWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveKeyWord.Click
        If listKeyword.SelectedIndex > -1 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " - " & CStr(listKeyword.SelectedItem), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                listKeyword.Items.RemoveAt(listKeyword.SelectedIndex)
                Filemanager.Master.FileEdited = True
            End If
        End If
    End Sub

    Private Sub UseAllAuthorDefaultsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseAllAuthorDefaultsButton.Click, UseYourNameButton.Click, UseYourEmailButton.Click, UseYourOrganisationButton.Click, TodayButton.Click
        If sender Is UseAllAuthorDefaultsButton Or sender Is UseYourNameButton Then
            txtOriginalAuthor.Text = Main.Instance.Options.UserName
        End If

        If sender Is UseAllAuthorDefaultsButton Or sender Is UseYourEmailButton Then
            txtOriginalEmail.Text = Main.Instance.Options.UserEmail
        End If

        If sender Is UseAllAuthorDefaultsButton Or sender Is UseYourOrganisationButton Then
            txtOrganisation.Text = Main.Instance.Options.UserOrganisation
        End If

        If sender Is UseAllAuthorDefaultsButton Or sender Is TodayButton Then
            txtDate.Text = System.DateTime.Now().ToString("yyyy-MM-dd")
        End If
    End Sub

    Private Sub butAddContributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddContributor.Click
        Dim ipb As New InputForm
        ipb.lblInput.Text = Filemanager.GetOpenEhrTerm(604, "Contributors")

        If ipb.ShowDialog = Windows.Forms.DialogResult.OK Then
            listContributors.Items.Add(ipb.txtInput.Text)
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub butRemoveContributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveContributor.Click
        If listContributors.SelectedIndex > -1 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & " - " & CStr(listContributors.SelectedItem), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                listContributors.Items.RemoveAt(listContributors.SelectedIndex)
                Filemanager.Master.FileEdited = True
            End If
        End If
    End Sub

    Private Sub txtDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDate.TextChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub txtOrganisation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrganisation.TextChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub txtCurrentContact_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCurrentContact.TextChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub TabPageDescription_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        Main.Reflect(Me)
    End Sub

    Private Sub txtTranslatorName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTranslatorName.TextChanged, txtTranslationAccreditation.TextChanged, txtTranslatorEmail.TextChanged, txtTranslatorOrganisation.TextChanged
        If Not mIsLoading Then
            mTranslationAltered = True
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub butEditContributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butEditContributor.Click
        If listContributors.SelectedIndex > -1 Then
            Dim ipb As New InputForm
            ipb.lblInput.Text = Filemanager.GetOpenEhrTerm(604, "Contributors")
            ipb.txtInput.Text = listContributors.Text

            If ipb.ShowDialog = Windows.Forms.DialogResult.OK Then
                listContributors.Items(listContributors.SelectedIndex) = ipb.txtInput.Text
                Filemanager.Master.FileEdited = True
            End If
        End If
    End Sub

    Private Sub listContributors_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listContributors.DoubleClick
        butEditContributor_Click(sender, e)
    End Sub

    Private Sub CurrentContactButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrentContactButton.Click
        txtCurrentContact.Text = Main.Instance.Options.UserName & ", " & Main.Instance.Options.UserOrganisation & ", " & Main.Instance.Options.UserEmail
    End Sub

End Class
