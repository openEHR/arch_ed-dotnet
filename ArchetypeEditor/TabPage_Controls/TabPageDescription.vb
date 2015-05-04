
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
    Private WithEvents mOtherDetailsTable As DataTable

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.gbPurpose = New System.Windows.Forms.GroupBox()
        Me.gbMisuse = New System.Windows.Forms.GroupBox()
        Me.txtMisuse = New System.Windows.Forms.TextBox()
        Me.gbUse = New System.Windows.Forms.GroupBox()
        Me.txtUse = New System.Windows.Forms.TextBox()
        Me.txtPurpose = New System.Windows.Forms.TextBox()
        Me.panelDescription = New System.Windows.Forms.Panel()
        Me.CopyrightTextBox = New System.Windows.Forms.TextBox()
        Me.CopyrightLabel = New System.Windows.Forms.Label()
        Me.txtRevision = New System.Windows.Forms.TextBox()
        Me.lblRevision = New System.Windows.Forms.Label()
        Me.txtLicense = New System.Windows.Forms.TextBox()
        Me.lblLicence = New System.Windows.Forms.Label()
        Me.ButAddKeyWord = New System.Windows.Forms.Button()
        Me.butRemoveKeyWord = New System.Windows.Forms.Button()
        Me.comboLifeCycle = New System.Windows.Forms.ComboBox()
        Me.lblKeyword = New System.Windows.Forms.Label()
        Me.listKeyword = New System.Windows.Forms.ListBox()
        Me.TabDescription = New Crownwood.Magic.Controls.TabControl()
        Me.tpReferences = New Crownwood.Magic.Controls.TabPage()
        Me.gbTechnicalIdentifiers = New System.Windows.Forms.GroupBox()
        Me.lblCanonicalHash = New System.Windows.Forms.Label()
        Me.txtCanonicalHash = New System.Windows.Forms.TextBox()
        Me.txtBuildUid = New System.Windows.Forms.TextBox()
        Me.lbBuildUid = New System.Windows.Forms.Label()
        Me.gbOtherDetails = New System.Windows.Forms.GroupBox()
        Me.dgOtherDetails = New System.Windows.Forms.DataGridView()
        Me.OtherDetailsKeyColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OtherDetailsValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtReferences = New System.Windows.Forms.TextBox()
        Me.tpDescDetails = New Crownwood.Magic.Controls.TabPage()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.tpAuthor = New Crownwood.Magic.Controls.TabPage()
        Me.gbCurrentResponsibility = New System.Windows.Forms.GroupBox()
        Me.txtReviewDate = New System.Windows.Forms.TextBox()
        Me.lblReviewDate = New System.Windows.Forms.Label()
        Me.btnCustodianDefaults = New System.Windows.Forms.Button()
        Me.txtCustodianNamespace = New System.Windows.Forms.TextBox()
        Me.lblCustodianNamespace = New System.Windows.Forms.Label()
        Me.txtCustodianOrganisation = New System.Windows.Forms.TextBox()
        Me.lblCustodianOrganisation = New System.Windows.Forms.Label()
        Me.CurrentContactButton = New System.Windows.Forms.Button()
        Me.lblCurrentContact = New System.Windows.Forms.Label()
        Me.txtCurrentContact = New System.Windows.Forms.TextBox()
        Me.gbContributors = New System.Windows.Forms.GroupBox()
        Me.butEditContributor = New System.Windows.Forms.Button()
        Me.listContributors = New System.Windows.Forms.ListBox()
        Me.butAddContributor = New System.Windows.Forms.Button()
        Me.butRemoveContributor = New System.Windows.Forms.Button()
        Me.gbAuthor = New System.Windows.Forms.GroupBox()
        Me.btnPublisherDefaults = New System.Windows.Forms.Button()
        Me.txtOriginalNamespace = New System.Windows.Forms.TextBox()
        Me.lblOriginalNamespace = New System.Windows.Forms.Label()
        Me.txtOriginalPublisher = New System.Windows.Forms.TextBox()
        Me.lblOriginalPublisher = New System.Windows.Forms.Label()
        Me.UseAllAuthorDefaultsButton = New System.Windows.Forms.Button()
        Me.TodayButton = New System.Windows.Forms.Button()
        Me.UseYourOrganisationButton = New System.Windows.Forms.Button()
        Me.UseYourEmailButton = New System.Windows.Forms.Button()
        Me.UseYourNameButton = New System.Windows.Forms.Button()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.lblOrganisation = New System.Windows.Forms.Label()
        Me.txtOrganisation = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtOriginalAuthor = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtOriginalEmail = New System.Windows.Forms.TextBox()
        Me.tpTranslation = New Crownwood.Magic.Controls.TabPage()
        Me.gbTranslator = New System.Windows.Forms.GroupBox()
        Me.lblAccreditation = New System.Windows.Forms.Label()
        Me.txtTranslationAccreditation = New System.Windows.Forms.TextBox()
        Me.lblTranslatorOrganisation = New System.Windows.Forms.Label()
        Me.txtTranslatorOrganisation = New System.Windows.Forms.TextBox()
        Me.lblTranslatorName = New System.Windows.Forms.Label()
        Me.txtTranslatorName = New System.Windows.Forms.TextBox()
        Me.lblTranslatorEmail = New System.Windows.Forms.Label()
        Me.txtTranslatorEmail = New System.Windows.Forms.TextBox()
        Me.gbPurpose.SuspendLayout()
        Me.gbMisuse.SuspendLayout()
        Me.gbUse.SuspendLayout()
        Me.panelDescription.SuspendLayout()
        Me.tpReferences.SuspendLayout()
        Me.gbTechnicalIdentifiers.SuspendLayout()
        Me.gbOtherDetails.SuspendLayout()
        CType(Me.dgOtherDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        Me.tpDescDetails.SuspendLayout()
        Me.tpAuthor.SuspendLayout()
        Me.gbCurrentResponsibility.SuspendLayout()
        Me.gbContributors.SuspendLayout()
        Me.gbAuthor.SuspendLayout()
        Me.tpTranslation.SuspendLayout()
        Me.gbTranslator.SuspendLayout()
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
        'gbPurpose
        '
        Me.gbPurpose.Controls.Add(Me.gbMisuse)
        Me.gbPurpose.Controls.Add(Me.gbUse)
        Me.gbPurpose.Controls.Add(Me.txtPurpose)
        Me.gbPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbPurpose.Location = New System.Drawing.Point(0, 0)
        Me.gbPurpose.Name = "gbPurpose"
        Me.gbPurpose.Size = New System.Drawing.Size(620, 600)
        Me.gbPurpose.TabIndex = 0
        Me.gbPurpose.TabStop = False
        Me.gbPurpose.Text = "Purpose"
        '
        'gbMisuse
        '
        Me.gbMisuse.Controls.Add(Me.txtMisuse)
        Me.gbMisuse.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbMisuse.Location = New System.Drawing.Point(3, 461)
        Me.gbMisuse.Name = "gbMisuse"
        Me.gbMisuse.Size = New System.Drawing.Size(614, 136)
        Me.gbMisuse.TabIndex = 11
        Me.gbMisuse.TabStop = False
        Me.gbMisuse.Text = "Misuse"
        '
        'txtMisuse
        '
        Me.txtMisuse.AcceptsReturn = True
        Me.txtMisuse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMisuse.Location = New System.Drawing.Point(3, 19)
        Me.txtMisuse.Multiline = True
        Me.txtMisuse.Name = "txtMisuse"
        Me.txtMisuse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMisuse.Size = New System.Drawing.Size(608, 114)
        Me.txtMisuse.TabIndex = 6
        '
        'gbUse
        '
        Me.gbUse.Controls.Add(Me.txtUse)
        Me.gbUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbUse.Location = New System.Drawing.Point(3, 132)
        Me.gbUse.Name = "gbUse"
        Me.gbUse.Size = New System.Drawing.Size(614, 465)
        Me.gbUse.TabIndex = 10
        Me.gbUse.TabStop = False
        Me.gbUse.Text = "Use"
        '
        'txtUse
        '
        Me.txtUse.AcceptsReturn = True
        Me.txtUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUse.Location = New System.Drawing.Point(3, 19)
        Me.txtUse.Multiline = True
        Me.txtUse.Name = "txtUse"
        Me.txtUse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUse.Size = New System.Drawing.Size(608, 443)
        Me.txtUse.TabIndex = 4
        '
        'txtPurpose
        '
        Me.txtPurpose.AcceptsReturn = True
        Me.txtPurpose.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtPurpose.Location = New System.Drawing.Point(3, 19)
        Me.txtPurpose.Multiline = True
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPurpose.Size = New System.Drawing.Size(614, 113)
        Me.txtPurpose.TabIndex = 0
        '
        'panelDescription
        '
        Me.panelDescription.Controls.Add(Me.CopyrightTextBox)
        Me.panelDescription.Controls.Add(Me.CopyrightLabel)
        Me.panelDescription.Controls.Add(Me.txtRevision)
        Me.panelDescription.Controls.Add(Me.lblRevision)
        Me.panelDescription.Controls.Add(Me.txtLicense)
        Me.panelDescription.Controls.Add(Me.lblLicence)
        Me.panelDescription.Controls.Add(Me.ButAddKeyWord)
        Me.panelDescription.Controls.Add(Me.butRemoveKeyWord)
        Me.panelDescription.Controls.Add(Me.comboLifeCycle)
        Me.panelDescription.Controls.Add(Me.lblKeyword)
        Me.panelDescription.Controls.Add(Me.listKeyword)
        Me.panelDescription.Controls.Add(Me.lblStatus)
        Me.panelDescription.Dock = System.Windows.Forms.DockStyle.Right
        Me.panelDescription.Location = New System.Drawing.Point(623, 0)
        Me.panelDescription.Name = "panelDescription"
        Me.panelDescription.Size = New System.Drawing.Size(327, 600)
        Me.panelDescription.TabIndex = 1
        '
        'CopyrightTextBox
        '
        Me.CopyrightTextBox.Location = New System.Drawing.Point(17, 125)
        Me.CopyrightTextBox.Name = "CopyrightTextBox"
        Me.CopyrightTextBox.Size = New System.Drawing.Size(281, 23)
        Me.CopyrightTextBox.TabIndex = 49
        '
        'CopyrightLabel
        '
        Me.CopyrightLabel.Location = New System.Drawing.Point(14, 99)
        Me.CopyrightLabel.Name = "CopyrightLabel"
        Me.CopyrightLabel.Size = New System.Drawing.Size(74, 23)
        Me.CopyrightLabel.TabIndex = 48
        Me.CopyrightLabel.Text = "Copyright:"
        Me.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRevision
        '
        Me.txtRevision.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtRevision.Location = New System.Drawing.Point(16, 73)
        Me.txtRevision.Name = "txtRevision"
        Me.txtRevision.ReadOnly = True
        Me.txtRevision.Size = New System.Drawing.Size(281, 23)
        Me.txtRevision.TabIndex = 40
        '
        'lblRevision
        '
        Me.lblRevision.Location = New System.Drawing.Point(16, 55)
        Me.lblRevision.Name = "lblRevision"
        Me.lblRevision.Size = New System.Drawing.Size(200, 17)
        Me.lblRevision.TabIndex = 39
        Me.lblRevision.Text = "Revision"
        Me.lblRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLicense
        '
        Me.txtLicense.Location = New System.Drawing.Point(17, 178)
        Me.txtLicense.Multiline = True
        Me.txtLicense.Name = "txtLicense"
        Me.txtLicense.Size = New System.Drawing.Size(281, 60)
        Me.txtLicense.TabIndex = 38
        '
        'lblLicence
        '
        Me.lblLicence.Location = New System.Drawing.Point(16, 145)
        Me.lblLicence.Name = "lblLicence"
        Me.lblLicence.Size = New System.Drawing.Size(127, 32)
        Me.lblLicence.TabIndex = 37
        Me.lblLicence.Text = "Licence:"
        Me.lblLicence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ButAddKeyWord
        '
        Me.ButAddKeyWord.BackColor = System.Drawing.Color.Transparent
        Me.ButAddKeyWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButAddKeyWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButAddKeyWord.Image = CType(resources.GetObject("ButAddKeyWord.Image"), System.Drawing.Image)
        Me.ButAddKeyWord.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButAddKeyWord.Location = New System.Drawing.Point(299, 269)
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
        Me.butRemoveKeyWord.Location = New System.Drawing.Point(300, 300)
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
        Me.comboLifeCycle.Size = New System.Drawing.Size(284, 23)
        Me.comboLifeCycle.TabIndex = 1
        '
        'lblKeyword
        '
        Me.lblKeyword.Location = New System.Drawing.Point(16, 242)
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
        Me.listKeyword.Location = New System.Drawing.Point(17, 269)
        Me.listKeyword.Name = "listKeyword"
        Me.listKeyword.Size = New System.Drawing.Size(276, 304)
        Me.listKeyword.TabIndex = 36
        '
        'TabDescription
        '
        Me.TabDescription.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TabDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDescription.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabDescription.Location = New System.Drawing.Point(2, 2)
        Me.TabDescription.Name = "TabDescription"
        Me.TabDescription.PositionTop = True
        Me.TabDescription.SelectedIndex = 3
        Me.TabDescription.SelectedTab = Me.tpReferences
        Me.TabDescription.Size = New System.Drawing.Size(950, 631)
        Me.TabDescription.TabIndex = 15
        Me.TabDescription.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpDescDetails, Me.tpAuthor, Me.tpTranslation, Me.tpReferences})
        '
        'tpReferences
        '
        Me.tpReferences.Controls.Add(Me.gbTechnicalIdentifiers)
        Me.tpReferences.Controls.Add(Me.gbOtherDetails)
        Me.tpReferences.Controls.Add(Me.groupBox2)
        Me.tpReferences.Location = New System.Drawing.Point(0, 0)
        Me.tpReferences.Name = "tpReferences"
        Me.tpReferences.Size = New System.Drawing.Size(950, 606)
        Me.tpReferences.TabIndex = 3
        Me.tpReferences.Title = "References / Other Details"
        '
        'gbTechnicalIdentifiers
        '
        Me.gbTechnicalIdentifiers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTechnicalIdentifiers.AutoSize = True
        Me.gbTechnicalIdentifiers.Controls.Add(Me.lblCanonicalHash)
        Me.gbTechnicalIdentifiers.Controls.Add(Me.txtCanonicalHash)
        Me.gbTechnicalIdentifiers.Controls.Add(Me.txtBuildUid)
        Me.gbTechnicalIdentifiers.Controls.Add(Me.lbBuildUid)
        Me.gbTechnicalIdentifiers.Location = New System.Drawing.Point(465, 324)
        Me.gbTechnicalIdentifiers.Name = "gbTechnicalIdentifiers"
        Me.gbTechnicalIdentifiers.Size = New System.Drawing.Size(482, 279)
        Me.gbTechnicalIdentifiers.TabIndex = 58
        Me.gbTechnicalIdentifiers.TabStop = False
        Me.gbTechnicalIdentifiers.Text = "Technical identifiers (read-only)"
        '
        'lblCanonicalHash
        '
        Me.lblCanonicalHash.Location = New System.Drawing.Point(40, 82)
        Me.lblCanonicalHash.Name = "lblCanonicalHash"
        Me.lblCanonicalHash.Size = New System.Drawing.Size(121, 23)
        Me.lblCanonicalHash.TabIndex = 69
        Me.lblCanonicalHash.Text = "Canonical MD5 hash"
        Me.lblCanonicalHash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCanonicalHash
        '
        Me.txtCanonicalHash.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtCanonicalHash.Location = New System.Drawing.Point(177, 83)
        Me.txtCanonicalHash.Name = "txtCanonicalHash"
        Me.txtCanonicalHash.ReadOnly = True
        Me.txtCanonicalHash.Size = New System.Drawing.Size(267, 23)
        Me.txtCanonicalHash.TabIndex = 68
        Me.txtCanonicalHash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBuildUid
        '
        Me.txtBuildUid.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtBuildUid.Location = New System.Drawing.Point(177, 37)
        Me.txtBuildUid.Name = "txtBuildUid"
        Me.txtBuildUid.ReadOnly = True
        Me.txtBuildUid.Size = New System.Drawing.Size(267, 23)
        Me.txtBuildUid.TabIndex = 67
        Me.txtBuildUid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbBuildUid
        '
        Me.lbBuildUid.Location = New System.Drawing.Point(93, 39)
        Me.lbBuildUid.Name = "lbBuildUid"
        Me.lbBuildUid.Size = New System.Drawing.Size(68, 16)
        Me.lbBuildUid.TabIndex = 66
        Me.lbBuildUid.Text = "Build Uid"
        Me.lbBuildUid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbOtherDetails
        '
        Me.gbOtherDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbOtherDetails.Controls.Add(Me.dgOtherDetails)
        Me.gbOtherDetails.Location = New System.Drawing.Point(3, 324)
        Me.gbOtherDetails.Name = "gbOtherDetails"
        Me.gbOtherDetails.Size = New System.Drawing.Size(459, 279)
        Me.gbOtherDetails.TabIndex = 43
        Me.gbOtherDetails.TabStop = False
        Me.gbOtherDetails.Text = "Other details (Read-only)"
        '
        'dgOtherDetails
        '
        Me.dgOtherDetails.AllowUserToAddRows = False
        Me.dgOtherDetails.AllowUserToDeleteRows = False
        Me.dgOtherDetails.AllowUserToResizeColumns = False
        Me.dgOtherDetails.AllowUserToResizeRows = False
        Me.dgOtherDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOtherDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OtherDetailsKeyColumn, Me.OtherDetailsValueColumn})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgOtherDetails.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgOtherDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOtherDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.dgOtherDetails.Location = New System.Drawing.Point(3, 19)
        Me.dgOtherDetails.Name = "dgOtherDetails"
        Me.dgOtherDetails.ReadOnly = True
        Me.dgOtherDetails.Size = New System.Drawing.Size(453, 257)
        Me.dgOtherDetails.TabIndex = 0
        '
        'OtherDetailsKeyColumn
        '
        Me.OtherDetailsKeyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.OtherDetailsKeyColumn.DataPropertyName = "Key"
        Me.OtherDetailsKeyColumn.FillWeight = 25.0!
        Me.OtherDetailsKeyColumn.HeaderText = "Key"
        Me.OtherDetailsKeyColumn.Name = "OtherDetailsKeyColumn"
        Me.OtherDetailsKeyColumn.ReadOnly = True
        '
        'OtherDetailsValueColumn
        '
        Me.OtherDetailsValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.OtherDetailsValueColumn.DataPropertyName = "Value"
        Me.OtherDetailsValueColumn.FillWeight = 75.0!
        Me.OtherDetailsValueColumn.HeaderText = "Value"
        Me.OtherDetailsValueColumn.Name = "OtherDetailsValueColumn"
        Me.OtherDetailsValueColumn.ReadOnly = True
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.txtReferences)
        Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupBox2.Location = New System.Drawing.Point(0, 0)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(950, 321)
        Me.groupBox2.TabIndex = 42
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "References"
        '
        'txtReferences
        '
        Me.txtReferences.AcceptsReturn = True
        Me.txtReferences.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReferences.Location = New System.Drawing.Point(3, 19)
        Me.txtReferences.Multiline = True
        Me.txtReferences.Name = "txtReferences"
        Me.txtReferences.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReferences.Size = New System.Drawing.Size(944, 299)
        Me.txtReferences.TabIndex = 1
        '
        'tpDescDetails
        '
        Me.tpDescDetails.Controls.Add(Me.gbPurpose)
        Me.tpDescDetails.Controls.Add(Me.Splitter3)
        Me.tpDescDetails.Controls.Add(Me.panelDescription)
        Me.tpDescDetails.Controls.Add(Me.Splitter2)
        Me.tpDescDetails.Controls.Add(Me.Splitter1)
        Me.tpDescDetails.Location = New System.Drawing.Point(0, 0)
        Me.tpDescDetails.Name = "tpDescDetails"
        Me.tpDescDetails.Selected = False
        Me.tpDescDetails.Size = New System.Drawing.Size(950, 606)
        Me.tpDescDetails.TabIndex = 0
        Me.tpDescDetails.Title = "Details"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter3.Location = New System.Drawing.Point(620, 0)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(3, 600)
        Me.Splitter3.TabIndex = 17
        Me.Splitter3.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(0, 600)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(950, 3)
        Me.Splitter2.TabIndex = 16
        Me.Splitter2.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 603)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(950, 3)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'tpAuthor
        '
        Me.tpAuthor.Controls.Add(Me.gbCurrentResponsibility)
        Me.tpAuthor.Controls.Add(Me.gbContributors)
        Me.tpAuthor.Controls.Add(Me.gbAuthor)
        Me.tpAuthor.Location = New System.Drawing.Point(0, 0)
        Me.tpAuthor.Name = "tpAuthor"
        Me.tpAuthor.Selected = False
        Me.tpAuthor.Size = New System.Drawing.Size(950, 606)
        Me.tpAuthor.TabIndex = 1
        Me.tpAuthor.Title = "Authorship"
        '
        'gbCurrentResponsibility
        '
        Me.gbCurrentResponsibility.Controls.Add(Me.txtReviewDate)
        Me.gbCurrentResponsibility.Controls.Add(Me.lblReviewDate)
        Me.gbCurrentResponsibility.Controls.Add(Me.btnCustodianDefaults)
        Me.gbCurrentResponsibility.Controls.Add(Me.txtCustodianNamespace)
        Me.gbCurrentResponsibility.Controls.Add(Me.lblCustodianNamespace)
        Me.gbCurrentResponsibility.Controls.Add(Me.txtCustodianOrganisation)
        Me.gbCurrentResponsibility.Controls.Add(Me.lblCustodianOrganisation)
        Me.gbCurrentResponsibility.Controls.Add(Me.CurrentContactButton)
        Me.gbCurrentResponsibility.Controls.Add(Me.lblCurrentContact)
        Me.gbCurrentResponsibility.Controls.Add(Me.txtCurrentContact)
        Me.gbCurrentResponsibility.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbCurrentResponsibility.Location = New System.Drawing.Point(0, 256)
        Me.gbCurrentResponsibility.Name = "gbCurrentResponsibility"
        Me.gbCurrentResponsibility.Size = New System.Drawing.Size(950, 167)
        Me.gbCurrentResponsibility.TabIndex = 44
        Me.gbCurrentResponsibility.TabStop = False
        Me.gbCurrentResponsibility.Text = "Currently responsible"
        '
        'txtReviewDate
        '
        Me.txtReviewDate.Location = New System.Drawing.Point(591, 76)
        Me.txtReviewDate.Name = "txtReviewDate"
        Me.txtReviewDate.Size = New System.Drawing.Size(188, 23)
        Me.txtReviewDate.TabIndex = 52
        '
        'lblReviewDate
        '
        Me.lblReviewDate.Location = New System.Drawing.Point(543, 61)
        Me.lblReviewDate.Name = "lblReviewDate"
        Me.lblReviewDate.Size = New System.Drawing.Size(60, 51)
        Me.lblReviewDate.TabIndex = 51
        Me.lblReviewDate.Text = "Review Date:"
        Me.lblReviewDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCustodianDefaults
        '
        Me.btnCustodianDefaults.Location = New System.Drawing.Point(542, 30)
        Me.btnCustodianDefaults.Name = "btnCustodianDefaults"
        Me.btnCustodianDefaults.Size = New System.Drawing.Size(237, 23)
        Me.btnCustodianDefaults.TabIndex = 50
        Me.btnCustodianDefaults.Text = "Use your Publisher defaults"
        Me.btnCustodianDefaults.UseVisualStyleBackColor = True
        '
        'txtCustodianNamespace
        '
        Me.txtCustodianNamespace.Location = New System.Drawing.Point(112, 75)
        Me.txtCustodianNamespace.Name = "txtCustodianNamespace"
        Me.txtCustodianNamespace.Size = New System.Drawing.Size(410, 23)
        Me.txtCustodianNamespace.TabIndex = 49
        '
        'lblCustodianNamespace
        '
        Me.lblCustodianNamespace.Location = New System.Drawing.Point(25, 70)
        Me.lblCustodianNamespace.Name = "lblCustodianNamespace"
        Me.lblCustodianNamespace.Size = New System.Drawing.Size(77, 32)
        Me.lblCustodianNamespace.TabIndex = 48
        Me.lblCustodianNamespace.Text = "Custodian Namespace"
        Me.lblCustodianNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustodianOrganisation
        '
        Me.txtCustodianOrganisation.Location = New System.Drawing.Point(112, 31)
        Me.txtCustodianOrganisation.Name = "txtCustodianOrganisation"
        Me.txtCustodianOrganisation.Size = New System.Drawing.Size(410, 23)
        Me.txtCustodianOrganisation.TabIndex = 47
        '
        'lblCustodianOrganisation
        '
        Me.lblCustodianOrganisation.Location = New System.Drawing.Point(25, 27)
        Me.lblCustodianOrganisation.Name = "lblCustodianOrganisation"
        Me.lblCustodianOrganisation.Size = New System.Drawing.Size(77, 32)
        Me.lblCustodianOrganisation.TabIndex = 46
        Me.lblCustodianOrganisation.Text = "Custodian Organisation:"
        Me.lblCustodianOrganisation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CurrentContactButton
        '
        Me.CurrentContactButton.Location = New System.Drawing.Point(542, 122)
        Me.CurrentContactButton.Name = "CurrentContactButton"
        Me.CurrentContactButton.Size = New System.Drawing.Size(237, 23)
        Me.CurrentContactButton.TabIndex = 9
        Me.CurrentContactButton.Text = "Use your Author details"
        Me.CurrentContactButton.UseVisualStyleBackColor = True
        '
        'lblCurrentContact
        '
        Me.lblCurrentContact.Location = New System.Drawing.Point(7, 104)
        Me.lblCurrentContact.Name = "lblCurrentContact"
        Me.lblCurrentContact.Size = New System.Drawing.Size(95, 59)
        Me.lblCurrentContact.TabIndex = 0
        Me.lblCurrentContact.Text = "Contact"
        Me.lblCurrentContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrentContact
        '
        Me.txtCurrentContact.Location = New System.Drawing.Point(112, 122)
        Me.txtCurrentContact.Name = "txtCurrentContact"
        Me.txtCurrentContact.Size = New System.Drawing.Size(410, 23)
        Me.txtCurrentContact.TabIndex = 1
        '
        'gbContributors
        '
        Me.gbContributors.Controls.Add(Me.butEditContributor)
        Me.gbContributors.Controls.Add(Me.listContributors)
        Me.gbContributors.Controls.Add(Me.butAddContributor)
        Me.gbContributors.Controls.Add(Me.butRemoveContributor)
        Me.gbContributors.Location = New System.Drawing.Point(0, 426)
        Me.gbContributors.Name = "gbContributors"
        Me.gbContributors.Size = New System.Drawing.Size(950, 177)
        Me.gbContributors.TabIndex = 40
        Me.gbContributors.TabStop = False
        Me.gbContributors.Text = "Contributors"
        '
        'butEditContributor
        '
        Me.butEditContributor.BackColor = System.Drawing.Color.Transparent
        Me.butEditContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butEditContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butEditContributor.Location = New System.Drawing.Point(79, 84)
        Me.butEditContributor.Name = "butEditContributor"
        Me.butEditContributor.Size = New System.Drawing.Size(24, 25)
        Me.butEditContributor.TabIndex = 40
        Me.butEditContributor.Text = "..."
        Me.butEditContributor.UseVisualStyleBackColor = False
        '
        'listContributors
        '
        Me.listContributors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.listContributors.ItemHeight = 15
        Me.listContributors.Location = New System.Drawing.Point(109, 22)
        Me.listContributors.Name = "listContributors"
        Me.listContributors.Size = New System.Drawing.Size(684, 124)
        Me.listContributors.TabIndex = 36
        '
        'butAddContributor
        '
        Me.butAddContributor.BackColor = System.Drawing.Color.Transparent
        Me.butAddContributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddContributor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddContributor.Image = CType(resources.GetObject("butAddContributor.Image"), System.Drawing.Image)
        Me.butAddContributor.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddContributor.Location = New System.Drawing.Point(79, 22)
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
        Me.butRemoveContributor.Location = New System.Drawing.Point(79, 53)
        Me.butRemoveContributor.Name = "butRemoveContributor"
        Me.butRemoveContributor.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveContributor.TabIndex = 39
        Me.butRemoveContributor.UseVisualStyleBackColor = False
        '
        'gbAuthor
        '
        Me.gbAuthor.Controls.Add(Me.btnPublisherDefaults)
        Me.gbAuthor.Controls.Add(Me.txtOriginalNamespace)
        Me.gbAuthor.Controls.Add(Me.lblOriginalNamespace)
        Me.gbAuthor.Controls.Add(Me.txtOriginalPublisher)
        Me.gbAuthor.Controls.Add(Me.lblOriginalPublisher)
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
        Me.gbAuthor.Size = New System.Drawing.Size(950, 256)
        Me.gbAuthor.TabIndex = 4
        Me.gbAuthor.TabStop = False
        Me.gbAuthor.Text = "Original author and publisher"
        '
        'btnPublisherDefaults
        '
        Me.btnPublisherDefaults.Location = New System.Drawing.Point(551, 173)
        Me.btnPublisherDefaults.Name = "btnPublisherDefaults"
        Me.btnPublisherDefaults.Size = New System.Drawing.Size(229, 23)
        Me.btnPublisherDefaults.TabIndex = 45
        Me.btnPublisherDefaults.Text = "Use your Publisher defaults"
        Me.btnPublisherDefaults.UseVisualStyleBackColor = True
        '
        'txtOriginalNamespace
        '
        Me.txtOriginalNamespace.Location = New System.Drawing.Point(111, 213)
        Me.txtOriginalNamespace.Name = "txtOriginalNamespace"
        Me.txtOriginalNamespace.Size = New System.Drawing.Size(424, 23)
        Me.txtOriginalNamespace.TabIndex = 44
        '
        'lblOriginalNamespace
        '
        Me.lblOriginalNamespace.Location = New System.Drawing.Point(1, 207)
        Me.lblOriginalNamespace.Name = "lblOriginalNamespace"
        Me.lblOriginalNamespace.Size = New System.Drawing.Size(102, 32)
        Me.lblOriginalNamespace.TabIndex = 43
        Me.lblOriginalNamespace.Text = "Original Namespace:"
        Me.lblOriginalNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalPublisher
        '
        Me.txtOriginalPublisher.Location = New System.Drawing.Point(111, 173)
        Me.txtOriginalPublisher.Name = "txtOriginalPublisher"
        Me.txtOriginalPublisher.Size = New System.Drawing.Size(424, 23)
        Me.txtOriginalPublisher.TabIndex = 40
        '
        'lblOriginalPublisher
        '
        Me.lblOriginalPublisher.Location = New System.Drawing.Point(1, 167)
        Me.lblOriginalPublisher.Name = "lblOriginalPublisher"
        Me.lblOriginalPublisher.Size = New System.Drawing.Size(102, 32)
        Me.lblOriginalPublisher.TabIndex = 39
        Me.lblOriginalPublisher.Text = "Original Publisher:"
        Me.lblOriginalPublisher.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UseAllAuthorDefaultsButton
        '
        Me.UseAllAuthorDefaultsButton.Location = New System.Drawing.Point(551, 127)
        Me.UseAllAuthorDefaultsButton.Name = "UseAllAuthorDefaultsButton"
        Me.UseAllAuthorDefaultsButton.Size = New System.Drawing.Size(229, 23)
        Me.UseAllAuthorDefaultsButton.TabIndex = 12
        Me.UseAllAuthorDefaultsButton.Text = "Use all Author defaults"
        Me.UseAllAuthorDefaultsButton.UseVisualStyleBackColor = True
        '
        'TodayButton
        '
        Me.TodayButton.Location = New System.Drawing.Point(277, 126)
        Me.TodayButton.Name = "TodayButton"
        Me.TodayButton.Size = New System.Drawing.Size(158, 23)
        Me.TodayButton.TabIndex = 11
        Me.TodayButton.Text = "Today"
        Me.TodayButton.UseVisualStyleBackColor = True
        '
        'UseYourOrganisationButton
        '
        Me.UseYourOrganisationButton.Location = New System.Drawing.Point(551, 93)
        Me.UseYourOrganisationButton.Name = "UseYourOrganisationButton"
        Me.UseYourOrganisationButton.Size = New System.Drawing.Size(229, 23)
        Me.UseYourOrganisationButton.TabIndex = 8
        Me.UseYourOrganisationButton.Text = "Use your Organisation"
        Me.UseYourOrganisationButton.UseVisualStyleBackColor = True
        '
        'UseYourEmailButton
        '
        Me.UseYourEmailButton.Location = New System.Drawing.Point(551, 61)
        Me.UseYourEmailButton.Name = "UseYourEmailButton"
        Me.UseYourEmailButton.Size = New System.Drawing.Size(229, 23)
        Me.UseYourEmailButton.TabIndex = 5
        Me.UseYourEmailButton.Text = "Use your Email"
        Me.UseYourEmailButton.UseVisualStyleBackColor = True
        '
        'UseYourNameButton
        '
        Me.UseYourNameButton.Location = New System.Drawing.Point(551, 32)
        Me.UseYourNameButton.Name = "UseYourNameButton"
        Me.UseYourNameButton.Size = New System.Drawing.Size(229, 23)
        Me.UseYourNameButton.TabIndex = 2
        Me.UseYourNameButton.Text = "Use your Name"
        Me.UseYourNameButton.UseVisualStyleBackColor = True
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(8, 126)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(95, 24)
        Me.lblDate.TabIndex = 9
        Me.lblDate.Text = "Date:"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(111, 126)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(160, 23)
        Me.txtDate.TabIndex = 10
        '
        'lblOrganisation
        '
        Me.lblOrganisation.Location = New System.Drawing.Point(8, 94)
        Me.lblOrganisation.Name = "lblOrganisation"
        Me.lblOrganisation.Size = New System.Drawing.Size(95, 24)
        Me.lblOrganisation.TabIndex = 6
        Me.lblOrganisation.Text = "Organisation:"
        Me.lblOrganisation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOrganisation
        '
        Me.txtOrganisation.Location = New System.Drawing.Point(111, 94)
        Me.txtOrganisation.Name = "txtOrganisation"
        Me.txtOrganisation.Size = New System.Drawing.Size(424, 23)
        Me.txtOrganisation.TabIndex = 7
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(8, 30)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(95, 24)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name:"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalAuthor
        '
        Me.txtOriginalAuthor.Location = New System.Drawing.Point(111, 30)
        Me.txtOriginalAuthor.Name = "txtOriginalAuthor"
        Me.txtOriginalAuthor.Size = New System.Drawing.Size(424, 23)
        Me.txtOriginalAuthor.TabIndex = 1
        '
        'lblEmail
        '
        Me.lblEmail.Location = New System.Drawing.Point(8, 62)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(95, 24)
        Me.lblEmail.TabIndex = 3
        Me.lblEmail.Text = "Email:"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOriginalEmail
        '
        Me.txtOriginalEmail.Location = New System.Drawing.Point(111, 62)
        Me.txtOriginalEmail.Name = "txtOriginalEmail"
        Me.txtOriginalEmail.Size = New System.Drawing.Size(424, 23)
        Me.txtOriginalEmail.TabIndex = 4
        '
        'tpTranslation
        '
        Me.tpTranslation.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpTranslation.Controls.Add(Me.gbTranslator)
        Me.tpTranslation.Location = New System.Drawing.Point(0, 0)
        Me.tpTranslation.Name = "tpTranslation"
        Me.tpTranslation.Selected = False
        Me.tpTranslation.Size = New System.Drawing.Size(950, 606)
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
        Me.gbTranslator.Size = New System.Drawing.Size(950, 157)
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
        'TabPageDescription
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.TabDescription)
        Me.Name = "TabPageDescription"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(954, 635)
        Me.gbPurpose.ResumeLayout(False)
        Me.gbPurpose.PerformLayout()
        Me.gbMisuse.ResumeLayout(False)
        Me.gbMisuse.PerformLayout()
        Me.gbUse.ResumeLayout(False)
        Me.gbUse.PerformLayout()
        Me.panelDescription.ResumeLayout(False)
        Me.panelDescription.PerformLayout()
        Me.tpReferences.ResumeLayout(False)
        Me.tpReferences.PerformLayout()
        Me.gbTechnicalIdentifiers.ResumeLayout(False)
        Me.gbTechnicalIdentifiers.PerformLayout()
        Me.gbOtherDetails.ResumeLayout(False)
        CType(Me.dgOtherDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.tpDescDetails.ResumeLayout(False)
        Me.tpAuthor.ResumeLayout(False)
        Me.gbCurrentResponsibility.ResumeLayout(False)
        Me.gbCurrentResponsibility.PerformLayout()
        Me.gbContributors.ResumeLayout(False)
        Me.gbAuthor.ResumeLayout(False)
        Me.gbAuthor.PerformLayout()
        Me.tpTranslation.ResumeLayout(False)
        Me.gbTranslator.ResumeLayout(False)
        Me.gbTranslator.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend txtCanonicalHash As System.Windows.Forms.TextBox
    Public lblCanonicalHash As System.Windows.Forms.Label
    Private gbTechnicalIdentifiers As System.Windows.Forms.GroupBox
    Public lbBuildUid As System.Windows.Forms.Label
    Friend txtBuildUid As System.Windows.Forms.TextBox
    Friend AnnotationValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend AnnotationKeyColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend OtherDetailsValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend OtherDetailsKeyColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend dgOtherDetails As System.Windows.Forms.DataGridView
    Friend gbOtherDetails As System.Windows.Forms.GroupBox
    Friend lblReviewDate As System.Windows.Forms.Label
    Friend txtReviewDate As System.Windows.Forms.TextBox
    Friend lblCustodianOrganisation As System.Windows.Forms.Label
    Friend txtCustodianOrganisation As System.Windows.Forms.TextBox
    Friend btnCustodianDefaults As System.Windows.Forms.Button
    Friend groupBox2 As System.Windows.Forms.GroupBox
    Friend lblOriginalPublisher As System.Windows.Forms.Label
    Friend txtOriginalPublisher As System.Windows.Forms.TextBox
    Friend lblOriginalNamespace As System.Windows.Forms.Label
    Friend txtOriginalNamespace As System.Windows.Forms.TextBox
    Friend btnPublisherDefaults As System.Windows.Forms.Button
    Public lblRevision As System.Windows.Forms.Label
    Friend txtRevision As System.Windows.Forms.TextBox
    Friend lblCustodianNamespace As System.Windows.Forms.Label
    Friend txtCustodianNamespace As System.Windows.Forms.TextBox
    Friend lblLicence As System.Windows.Forms.Label
    Friend txtLicense As System.Windows.Forms.TextBox

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
            txtCustodianNamespace.Text = value.CustodianNamespace
            txtCustodianOrganisation.Text = value.CustodianOrganisation
            txtOriginalPublisher.Text = value.OriginalPublisher
            txtOriginalNamespace.Text = value.OriginalNamespace
            txtLicense.Text = value.Licence
            txtReviewDate.Text = value.ReviewDate
            txtRevision.Text = value.Revision

            txtBuildUid.Text = value.BuildUid
            txtCanonicalHash.Text = value.ArchetypeDigest

            txtReferences.Text = NewlinesNormalised(Value.References)
            mArchetypeDescription = Value
            mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
            SetDescriptionDetailValues(False)

            MakeOtherDetailsTable()
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
        mArchetypeDescription.OriginalPublisher = txtOriginalPublisher.Text
        mArchetypeDescription.OriginalNamespace = txtOriginalNamespace.Text
        mArchetypeDescription.CustodianOrganisation = txtCustodianOrganisation.Text
        mArchetypeDescription.CustodianNamespace = txtCustodianNamespace.Text
        mArchetypeDescription.Licence = txtLicense.Text
        mArchetypeDescription.Revision = txtRevision.Text
        mArchetypeDescription.ReviewDate = txtReviewDate.Text




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
        Dim result As New System.Text.StringBuilder()

        result.AppendLine("{\rtf1\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 Tahoma;}{\f1\fnil\fcharset2 Symbol;}}")
        result.AppendLine("{\colortbl ;\red0\green0\blue255;\red0\green255\blue0;}")
        result.AppendLine("\viewkind4\uc1\pard\tx2840\tx5112\lang3081\f0\fs20")

        result.AppendLine("\b")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(Filemanager.GetOpenEhrTerm(585, "Purpose")))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(txtPurpose.Text))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(Filemanager.GetOpenEhrTerm(582, "Use")))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(txtUse.Text))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(Filemanager.GetOpenEhrTerm(583, "Misuse")))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(txtMisuse.Text))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(Filemanager.GetOpenEhrTerm(690, "Copyright")))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(CopyrightTextBox.Text))
        result.AppendLine("\par")
        result.AppendLine("\par")

        result.AppendLine("\b")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(Filemanager.GetOpenEhrTerm(691, "References")))
        result.Append(":\b0")
        result.AppendLine("\par")
        result.AppendLine(RichTextBoxUnicode.EscapedRtfString(txtReferences.Text))
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
        txtLicense.Text = ""
        txtReferences.Text = ""
        txtRevision.Text = ""
        txtOriginalPublisher.Text = ""
        txtOriginalNamespace.Text = ""
        txtCustodianOrganisation.Text = ""
        txtCustodianNamespace.Text = ""
        txtReviewDate.Text = ""
        txtCurrentContact.Text = ""

        txtBuildUid.Text = ""
        txtCanonicalHash.Text = ""

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
        Dim wasLoading As Boolean = Filemanager.Master.FileLoading
        Filemanager.Master.FileLoading = True

        If Not wasLoading Then
            SaveDescription() ' in the previous language = mCurrentLanguage
            SaveTranslation()
        End If

        ' Now mark the texts as needing translation in case this is a new language
        mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        SetDescriptionDetailValues(True)
        SetTranslationValues()
        Filemanager.Master.FileLoading = wasLoading
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
        tpReferences.Title = Filemanager.GetOpenEhrTerm(768, tpReferences.Title)
        gbPurpose.Text = Filemanager.GetOpenEhrTerm(585, gbPurpose.Text)
        gbUse.Text = Filemanager.GetOpenEhrTerm(582, gbUse.Text)
        gbMisuse.Text = Filemanager.GetOpenEhrTerm(583, gbMisuse.Text)
        CopyrightLabel.Text = Filemanager.GetOpenEhrTerm(690, CopyrightLabel.Text)
        gbAuthor.Text = Filemanager.GetOpenEhrTerm(584, gbAuthor.Text)
        gbTranslator.Text = Filemanager.GetOpenEhrTerm(757, gbTranslator.Text)
        gbContributors.Text = Filemanager.GetOpenEhrTerm(604, gbContributors.Text)
        gbCurrentResponsibility.Text = Filemanager.GetOpenEhrTerm(705, gbCurrentResponsibility.Text)
        lblCurrentContact.Text = Filemanager.GetOpenEhrTerm(704, lblCurrentContact.Text)
        UseYourNameButton.Text = Filemanager.GetOpenEhrTerm(716, UseYourNameButton.Text)
        UseYourEmailButton.Text = Filemanager.GetOpenEhrTerm(717, UseYourEmailButton.Text)
        UseYourOrganisationButton.Text = Filemanager.GetOpenEhrTerm(718, UseYourOrganisationButton.Text)
        CurrentContactButton.Text = Filemanager.GetOpenEhrTerm(719, CurrentContactButton.Text)
        UseAllAuthorDefaultsButton.Text = Filemanager.GetOpenEhrTerm(720, UseAllAuthorDefaultsButton.Text)
        TodayButton.Text = Filemanager.GetOpenEhrTerm(721, TodayButton.Text)
        lblLicence.Text = Filemanager.GetOpenEhrTerm(761, lblLicence.Text)
        lblRevision.Text = Filemanager.GetOpenEhrTerm(762, lblRevision.Text)
        lblOriginalPublisher.Text = Filemanager.GetOpenEhrTerm(763, lblOriginalPublisher.Text)
        lblOriginalNamespace.Text = Filemanager.GetOpenEhrTerm(764, lblOriginalNamespace.Text)
        lblCustodianOrganisation.Text = Filemanager.GetOpenEhrTerm(765, lblCustodianOrganisation.Text)
        lblCustodianNamespace.Text = Filemanager.GetOpenEhrTerm(766, lblCustodianNamespace.Text)
        lblReviewDate.Text = Filemanager.GetOpenEhrTerm(767, lblReviewDate.Text)
        gbOtherDetails.Text = Filemanager.GetOpenEhrTerm(769, gbOtherDetails.Text)

        lbBuildUid.Text = Filemanager.GetOpenEhrTerm(772, lbBuildUid.Text)
        lblCanonicalHash.Text = Filemanager.GetOpenEhrTerm(774, lblCanonicalHash.Text)
        gbTechnicalIdentifiers.Text = Filemanager.GetOpenEhrTerm(775, gbTechnicalIdentifiers.Text)

    End Sub

    Private Function MakeLifeCycleTable() As DataTable
        ' Create a new DataTable titled 'Languages'
        Dim result As DataTable = New DataTable("LifeCycles")

        'First the language ID = two letter ISO code
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "code"
        result.Columns.Add(idColumn)

        'Second the language text from windows/ISO list
        Dim lifeCycleColumn As DataColumn = New DataColumn
        lifeCycleColumn.DataType = System.Type.GetType("System.String")
        lifeCycleColumn.ColumnName = "LifeCycle"
        result.Columns.Add(lifeCycleColumn)

        result.PrimaryKey = New DataColumn() {idColumn}
        Return result
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

        For Each r As DataRow In Filemanager.Master.OntologyManager.CodeForGroupID(23) ' LifeCycle states
            Dim row As DataRow = mLifeCycleStatesTable.NewRow
            row(0) = r(1)
            row(1) = r(2)
            mLifeCycleStatesTable.Rows.Add(row)
        Next

        mLifeCycleStatesTable.AcceptChanges()
        comboLifeCycle.DataSource = mLifeCycleStatesTable
        comboLifeCycle.DisplayMember = "LifeCycle"
        comboLifeCycle.ValueMember = "code"
        comboLifeCycle.SelectedIndex = i
    End Sub

    Private Function MakeOtherDetailsTable() As DataTable
        mOtherDetailsTable = New DataTable
        mOtherDetailsTable.Columns.Add(New DataColumn("Key", GetType(String)))
        mOtherDetailsTable.Columns.Add(New DataColumn("Value", GetType(String)))

        OtherDetailsKeyColumn.DataPropertyName = "Key"
        OtherDetailsValueColumn.DataPropertyName = "Value"
        dgOtherDetails.DataSource = mOtherDetailsTable

        mOtherDetailsTable.Clear()
        mArchetypeDescription.FillTable(mOtherDetailsTable)
        Return mOtherDetailsTable
    End Function

    Private Sub TabPageDescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mCurrentLanguage = Filemanager.Master.OntologyManager.LanguageCode
        Dim wasLoading As Boolean = Filemanager.Master.FileLoading
        Filemanager.Master.FileLoading = True

        If Main.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        Filemanager.Master.FileLoading = wasLoading
    End Sub

    Private Sub TextUpdated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrganisation.TextChanged, _
            txtOrganisation.TextChanged, txtTranslationAccreditation.TextChanged, _
            txtTranslatorEmail.TextChanged, txtTranslatorOrganisation.TextChanged, _
            txtDate.TextChanged, txtOriginalAuthor.TextChanged, txtOriginalEmail.TextChanged, txtPurpose.TextChanged, _
            comboLifeCycle.SelectedIndexChanged, txtUse.TextChanged, txtMisuse.TextChanged, CopyrightTextBox.TextChanged, _
            txtReferences.TextChanged, txtCurrentContact.TextChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private Sub ButAddKeyWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddKeyWord.Click
        Dim form As New InputForm
        form.lblInput.Text = Filemanager.GetOpenEhrTerm(578, "Keyword")

        If form.ShowDialog(ParentForm) = Windows.Forms.DialogResult.OK Then
            listKeyword.Items.Add(form.txtInput.Text)
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
        Dim form As New InputForm
        form.lblInput.Text = Filemanager.GetOpenEhrTerm(604, "Contributors")

        If form.ShowDialog = Windows.Forms.DialogResult.OK Then
            listContributors.Items.Add(form.txtInput.Text)
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
        txtCurrentContact.Text = Main.Instance.Options.UserName & ", " & Main.Instance.Options.UserOrganisation & "<" & Main.Instance.Options.UserEmail & ">"
    End Sub


End Class




