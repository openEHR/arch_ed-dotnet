Public Class ApplicationOptionsForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RepositoryPathLabel As System.Windows.Forms.Label
    Friend WithEvents RepositoryBrowseButton As System.Windows.Forms.Button
    Friend WithEvents RepositoryPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReferenceModelComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ReferenceModelLabel As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents StateMachineGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label_0 As System.Windows.Forms.Label
    Friend WithEvents Panel_0 As System.Windows.Forms.Panel
    Friend WithEvents Panel_2 As System.Windows.Forms.Panel
    Friend WithEvents Label_2 As System.Windows.Forms.Label
    Friend WithEvents Panel_3 As System.Windows.Forms.Panel
    Friend WithEvents Label_3 As System.Windows.Forms.Label
    Friend WithEvents Panel_1 As System.Windows.Forms.Panel
    Friend WithEvents Label_1 As System.Windows.Forms.Label
    Friend WithEvents Panel_4 As System.Windows.Forms.Panel
    Friend WithEvents Label_4 As System.Windows.Forms.Label
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents Panel_5 As System.Windows.Forms.Panel
    Friend WithEvents Label_5 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel_6 As System.Windows.Forms.Panel
    Friend WithEvents Label_6 As System.Windows.Forms.Label
    Friend WithEvents TabConfiguration As System.Windows.Forms.TabControl
    Friend WithEvents UserDetailsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents FileLocationsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents AppearanceTabPage As System.Windows.Forms.TabPage
    Friend WithEvents DefaultsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OccurrencesLabel As System.Windows.Forms.Label
    Friend WithEvents OccurrencesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Panel_7 As System.Windows.Forms.Panel
    Friend WithEvents ParserGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ParserXmlRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ParserAdlRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents SharedRepositoryUrlCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SharedRepositoryUrlLabel As System.Windows.Forms.Label
    Friend WithEvents SharedRepositoryUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TerminologyServiceUrlLabel As System.Windows.Forms.Label
    Friend WithEvents TerminologyServiceUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TerminologyServiceUrlCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AutoSaveLabel As System.Windows.Forms.Label
    Friend WithEvents numAutoSave As System.Windows.Forms.NumericUpDown
    Friend WithEvents XmlRepositoryPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents XmlRepositoryPathLabel As System.Windows.Forms.Label
    Friend WithEvents XmlRepositoryBrowseButton As System.Windows.Forms.Button
    Friend WithEvents XmlRepositoryAutoSaveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RepositoryAutoSaveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents HtmlTabPage As System.Windows.Forms.TabPage
    Friend WithEvents XsltScriptPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents XsltScriptPathLabel As System.Windows.Forms.Label
    Friend WithEvents XsltScriptPathButton As System.Windows.Forms.Button
    Friend WithEvents ShowCommentsInHTMLCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShowTerminologyInHTMLCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents XsltScriptPathExplanatoryLabel As System.Windows.Forms.Label
    Friend WithEvents XsltScriptPathCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShowLinksButtonCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RestoreDefaultSharedRepositoryUrlButton As System.Windows.Forms.Button
    Friend WithEvents RestoreDefaultTerminologyServiceUrlButton As System.Windows.Forms.Button
    Friend WithEvents txtOrganisation As System.Windows.Forms.TextBox
    Friend WithEvents OrganisationLabel As System.Windows.Forms.Label
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents EmailLabel As System.Windows.Forms.Label
    Friend WithEvents Label_7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApplicationOptionsForm))
        Me.RepositoryPathLabel = New System.Windows.Forms.Label
        Me.RepositoryPathTextBox = New System.Windows.Forms.TextBox
        Me.CloseButton = New System.Windows.Forms.Button
        Me.OkButton = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SharedRepositoryUrlTextBox = New System.Windows.Forms.TextBox
        Me.TerminologyServiceUrlTextBox = New System.Windows.Forms.TextBox
        Me.XmlRepositoryPathTextBox = New System.Windows.Forms.TextBox
        Me.XsltScriptPathTextBox = New System.Windows.Forms.TextBox
        Me.RepositoryBrowseButton = New System.Windows.Forms.Button
        Me.ReferenceModelComboBox = New System.Windows.Forms.ComboBox
        Me.ReferenceModelLabel = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.StateMachineGroupBox = New System.Windows.Forms.GroupBox
        Me.Panel_7 = New System.Windows.Forms.Panel
        Me.Label_7 = New System.Windows.Forms.Label
        Me.Panel_6 = New System.Windows.Forms.Panel
        Me.Label_6 = New System.Windows.Forms.Label
        Me.Panel_4 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label_4 = New System.Windows.Forms.Label
        Me.Panel_1 = New System.Windows.Forms.Panel
        Me.Label_1 = New System.Windows.Forms.Label
        Me.Panel_3 = New System.Windows.Forms.Panel
        Me.Label_3 = New System.Windows.Forms.Label
        Me.Panel_2 = New System.Windows.Forms.Panel
        Me.Label_2 = New System.Windows.Forms.Label
        Me.Panel_0 = New System.Windows.Forms.Panel
        Me.Label_0 = New System.Windows.Forms.Label
        Me.Panel_5 = New System.Windows.Forms.Panel
        Me.Label_5 = New System.Windows.Forms.Label
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.TabConfiguration = New System.Windows.Forms.TabControl
        Me.UserDetailsTabPage = New System.Windows.Forms.TabPage
        Me.txtOrganisation = New System.Windows.Forms.TextBox
        Me.OrganisationLabel = New System.Windows.Forms.Label
        Me.NameLabel = New System.Windows.Forms.Label
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.EmailLabel = New System.Windows.Forms.Label
        Me.FileLocationsTabPage = New System.Windows.Forms.TabPage
        Me.RestoreDefaultTerminologyServiceUrlButton = New System.Windows.Forms.Button
        Me.RestoreDefaultSharedRepositoryUrlButton = New System.Windows.Forms.Button
        Me.XmlRepositoryPathLabel = New System.Windows.Forms.Label
        Me.XmlRepositoryBrowseButton = New System.Windows.Forms.Button
        Me.TerminologyServiceUrlLabel = New System.Windows.Forms.Label
        Me.TerminologyServiceUrlCheckBox = New System.Windows.Forms.CheckBox
        Me.SharedRepositoryUrlLabel = New System.Windows.Forms.Label
        Me.SharedRepositoryUrlCheckBox = New System.Windows.Forms.CheckBox
        Me.RepositoryAutoSaveCheckBox = New System.Windows.Forms.CheckBox
        Me.XmlRepositoryAutoSaveCheckBox = New System.Windows.Forms.CheckBox
        Me.HtmlTabPage = New System.Windows.Forms.TabPage
        Me.XsltScriptPathCheckBox = New System.Windows.Forms.CheckBox
        Me.XsltScriptPathExplanatoryLabel = New System.Windows.Forms.Label
        Me.XsltScriptPathLabel = New System.Windows.Forms.Label
        Me.XsltScriptPathButton = New System.Windows.Forms.Button
        Me.ShowCommentsInHTMLCheckBox = New System.Windows.Forms.CheckBox
        Me.ShowTerminologyInHTMLCheckBox = New System.Windows.Forms.CheckBox
        Me.AppearanceTabPage = New System.Windows.Forms.TabPage
        Me.ShowLinksButtonCheckBox = New System.Windows.Forms.CheckBox
        Me.OccurrencesLabel = New System.Windows.Forms.Label
        Me.OccurrencesComboBox = New System.Windows.Forms.ComboBox
        Me.DefaultsTabPage = New System.Windows.Forms.TabPage
        Me.AutoSaveLabel = New System.Windows.Forms.Label
        Me.numAutoSave = New System.Windows.Forms.NumericUpDown
        Me.ParserGroupBox = New System.Windows.Forms.GroupBox
        Me.ParserXmlRadioButton = New System.Windows.Forms.RadioButton
        Me.ParserAdlRadioButton = New System.Windows.Forms.RadioButton
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.StateMachineGroupBox.SuspendLayout()
        Me.Panel_4.SuspendLayout()
        Me.TabConfiguration.SuspendLayout()
        Me.UserDetailsTabPage.SuspendLayout()
        Me.FileLocationsTabPage.SuspendLayout()
        Me.HtmlTabPage.SuspendLayout()
        Me.AppearanceTabPage.SuspendLayout()
        Me.DefaultsTabPage.SuspendLayout()
        CType(Me.numAutoSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ParserGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'RepositoryPathLabel
        '
        Me.RepositoryPathLabel.AutoSize = True
        Me.RepositoryPathLabel.Location = New System.Drawing.Point(19, 19)
        Me.RepositoryPathLabel.Name = "RepositoryPathLabel"
        Me.RepositoryPathLabel.Size = New System.Drawing.Size(130, 13)
        Me.RepositoryPathLabel.TabIndex = 0
        Me.RepositoryPathLabel.Text = "Archetype repository path:"
        Me.RepositoryPathLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'RepositoryPathTextBox
        '
        Me.RepositoryPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RepositoryPathTextBox.Location = New System.Drawing.Point(19, 38)
        Me.RepositoryPathTextBox.Name = "RepositoryPathTextBox"
        Me.RepositoryPathTextBox.Size = New System.Drawing.Size(490, 20)
        Me.RepositoryPathTextBox.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.RepositoryPathTextBox, "Leave blank for last directory used")
        '
        'CloseButton
        '
        Me.CloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CloseButton.Location = New System.Drawing.Point(539, 304)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(80, 24)
        Me.CloseButton.TabIndex = 3
        Me.CloseButton.Text = "Cancel"
        '
        'OkButton
        '
        Me.OkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OkButton.Location = New System.Drawing.Point(449, 304)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(80, 24)
        Me.OkButton.TabIndex = 2
        Me.OkButton.Text = "OK"
        '
        'SharedRepositoryUrlTextBox
        '
        Me.SharedRepositoryUrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SharedRepositoryUrlTextBox.Location = New System.Drawing.Point(19, 168)
        Me.SharedRepositoryUrlTextBox.Name = "SharedRepositoryUrlTextBox"
        Me.SharedRepositoryUrlTextBox.Size = New System.Drawing.Size(490, 20)
        Me.SharedRepositoryUrlTextBox.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.SharedRepositoryUrlTextBox, "Http address of repository")
        '
        'TerminologyServiceUrlTextBox
        '
        Me.TerminologyServiceUrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerminologyServiceUrlTextBox.Location = New System.Drawing.Point(19, 222)
        Me.TerminologyServiceUrlTextBox.Name = "TerminologyServiceUrlTextBox"
        Me.TerminologyServiceUrlTextBox.Size = New System.Drawing.Size(490, 20)
        Me.TerminologyServiceUrlTextBox.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.TerminologyServiceUrlTextBox, "Http address of repository")
        '
        'XmlRepositoryPathTextBox
        '
        Me.XmlRepositoryPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XmlRepositoryPathTextBox.Location = New System.Drawing.Point(19, 88)
        Me.XmlRepositoryPathTextBox.Name = "XmlRepositoryPathTextBox"
        Me.XmlRepositoryPathTextBox.Size = New System.Drawing.Size(490, 20)
        Me.XmlRepositoryPathTextBox.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.XmlRepositoryPathTextBox, "Leave blank for last directory used")
        '
        'XsltScriptPathTextBox
        '
        Me.XsltScriptPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XsltScriptPathTextBox.Location = New System.Drawing.Point(17, 28)
        Me.XsltScriptPathTextBox.Name = "XsltScriptPathTextBox"
        Me.XsltScriptPathTextBox.Size = New System.Drawing.Size(490, 20)
        Me.XsltScriptPathTextBox.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.XsltScriptPathTextBox, "Leave blank for last directory used")
        '
        'RepositoryBrowseButton
        '
        Me.RepositoryBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RepositoryBrowseButton.Image = CType(resources.GetObject("RepositoryBrowseButton.Image"), System.Drawing.Image)
        Me.RepositoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RepositoryBrowseButton.Location = New System.Drawing.Point(519, 31)
        Me.RepositoryBrowseButton.Name = "RepositoryBrowseButton"
        Me.RepositoryBrowseButton.Size = New System.Drawing.Size(96, 32)
        Me.RepositoryBrowseButton.TabIndex = 3
        Me.RepositoryBrowseButton.Text = "Browse..."
        Me.RepositoryBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ReferenceModelComboBox
        '
        Me.ReferenceModelComboBox.Location = New System.Drawing.Point(16, 56)
        Me.ReferenceModelComboBox.Name = "ReferenceModelComboBox"
        Me.ReferenceModelComboBox.Size = New System.Drawing.Size(208, 21)
        Me.ReferenceModelComboBox.TabIndex = 2
        '
        'ReferenceModelLabel
        '
        Me.ReferenceModelLabel.Location = New System.Drawing.Point(16, 21)
        Me.ReferenceModelLabel.Name = "ReferenceModelLabel"
        Me.ReferenceModelLabel.Size = New System.Drawing.Size(200, 32)
        Me.ReferenceModelLabel.TabIndex = 1
        Me.ReferenceModelLabel.Text = "Default reference model:"
        Me.ReferenceModelLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'StateMachineGroupBox
        '
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_7)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_7)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_6)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_6)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_4)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_4)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_1)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_1)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_3)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_3)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_2)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_2)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_0)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_0)
        Me.StateMachineGroupBox.Controls.Add(Me.Panel_5)
        Me.StateMachineGroupBox.Controls.Add(Me.Label_5)
        Me.StateMachineGroupBox.Location = New System.Drawing.Point(16, 8)
        Me.StateMachineGroupBox.Name = "StateMachineGroupBox"
        Me.StateMachineGroupBox.Size = New System.Drawing.Size(464, 117)
        Me.StateMachineGroupBox.TabIndex = 1
        Me.StateMachineGroupBox.TabStop = False
        Me.StateMachineGroupBox.Text = "State machine colours"
        '
        'Panel_7
        '
        Me.Panel_7.BackColor = System.Drawing.Color.Orange
        Me.Panel_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_7.Location = New System.Drawing.Point(424, 97)
        Me.Panel_7.Name = "Panel_7"
        Me.Panel_7.Size = New System.Drawing.Size(32, 16)
        Me.Panel_7.TabIndex = 16
        Me.Panel_7.TabStop = True
        '
        'Label_7
        '
        Me.Label_7.Location = New System.Drawing.Point(240, 97)
        Me.Label_7.Name = "Label_7"
        Me.Label_7.Size = New System.Drawing.Size(176, 16)
        Me.Label_7.TabIndex = 15
        Me.Label_7.Text = "Label7"
        Me.Label_7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_6
        '
        Me.Panel_6.BackColor = System.Drawing.Color.LightGray
        Me.Panel_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_6.Location = New System.Drawing.Point(424, 69)
        Me.Panel_6.Name = "Panel_6"
        Me.Panel_6.Size = New System.Drawing.Size(32, 16)
        Me.Panel_6.TabIndex = 14
        Me.Panel_6.TabStop = True
        '
        'Label_6
        '
        Me.Label_6.Location = New System.Drawing.Point(240, 69)
        Me.Label_6.Name = "Label_6"
        Me.Label_6.Size = New System.Drawing.Size(176, 16)
        Me.Label_6.TabIndex = 13
        Me.Label_6.Text = "Label6"
        Me.Label_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_4
        '
        Me.Panel_4.BackColor = System.Drawing.Color.Red
        Me.Panel_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_4.Controls.Add(Me.Panel2)
        Me.Panel_4.Location = New System.Drawing.Point(424, 17)
        Me.Panel_4.Name = "Panel_4"
        Me.Panel_4.Size = New System.Drawing.Size(32, 16)
        Me.Panel_4.TabIndex = 10
        Me.Panel_4.TabStop = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Location = New System.Drawing.Point(94, -2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(32, 24)
        Me.Panel2.TabIndex = 11
        '
        'Label_4
        '
        Me.Label_4.Location = New System.Drawing.Point(240, 16)
        Me.Label_4.Name = "Label_4"
        Me.Label_4.Size = New System.Drawing.Size(176, 15)
        Me.Label_4.TabIndex = 9
        Me.Label_4.Text = "Label4"
        Me.Label_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_1
        '
        Me.Panel_1.BackColor = System.Drawing.Color.Lime
        Me.Panel_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_1.Location = New System.Drawing.Point(200, 42)
        Me.Panel_1.Name = "Panel_1"
        Me.Panel_1.Size = New System.Drawing.Size(32, 15)
        Me.Panel_1.TabIndex = 4
        Me.Panel_1.TabStop = True
        '
        'Label_1
        '
        Me.Label_1.Location = New System.Drawing.Point(8, 42)
        Me.Label_1.Name = "Label_1"
        Me.Label_1.Size = New System.Drawing.Size(176, 15)
        Me.Label_1.TabIndex = 3
        Me.Label_1.Text = "Label1"
        Me.Label_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_3
        '
        Me.Panel_3.BackColor = System.Drawing.Color.Tomato
        Me.Panel_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_3.Location = New System.Drawing.Point(200, 98)
        Me.Panel_3.Name = "Panel_3"
        Me.Panel_3.Size = New System.Drawing.Size(32, 16)
        Me.Panel_3.TabIndex = 8
        Me.Panel_3.TabStop = True
        '
        'Label_3
        '
        Me.Label_3.Location = New System.Drawing.Point(8, 97)
        Me.Label_3.Name = "Label_3"
        Me.Label_3.Size = New System.Drawing.Size(176, 16)
        Me.Label_3.TabIndex = 7
        Me.Label_3.Text = "Label3"
        Me.Label_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_2
        '
        Me.Panel_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_2.Location = New System.Drawing.Point(200, 70)
        Me.Panel_2.Name = "Panel_2"
        Me.Panel_2.Size = New System.Drawing.Size(32, 16)
        Me.Panel_2.TabIndex = 6
        Me.Panel_2.TabStop = True
        '
        'Label_2
        '
        Me.Label_2.Location = New System.Drawing.Point(8, 69)
        Me.Label_2.Name = "Label_2"
        Me.Label_2.Size = New System.Drawing.Size(176, 16)
        Me.Label_2.TabIndex = 5
        Me.Label_2.Text = "Label2"
        Me.Label_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_0
        '
        Me.Panel_0.BackColor = System.Drawing.Color.Yellow
        Me.Panel_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_0.Location = New System.Drawing.Point(200, 16)
        Me.Panel_0.Name = "Panel_0"
        Me.Panel_0.Size = New System.Drawing.Size(32, 16)
        Me.Panel_0.TabIndex = 2
        Me.Panel_0.TabStop = True
        '
        'Label_0
        '
        Me.Label_0.Location = New System.Drawing.Point(8, 16)
        Me.Label_0.Name = "Label_0"
        Me.Label_0.Size = New System.Drawing.Size(176, 15)
        Me.Label_0.TabIndex = 1
        Me.Label_0.Text = "Label0"
        Me.Label_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_5
        '
        Me.Panel_5.BackColor = System.Drawing.Color.Silver
        Me.Panel_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_5.Location = New System.Drawing.Point(424, 42)
        Me.Panel_5.Name = "Panel_5"
        Me.Panel_5.Size = New System.Drawing.Size(32, 16)
        Me.Panel_5.TabIndex = 12
        Me.Panel_5.TabStop = True
        '
        'Label_5
        '
        Me.Label_5.Location = New System.Drawing.Point(240, 42)
        Me.Label_5.Name = "Label_5"
        Me.Label_5.Size = New System.Drawing.Size(176, 15)
        Me.Label_5.TabIndex = 11
        Me.Label_5.Text = "Label5"
        Me.Label_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabConfiguration
        '
        Me.TabConfiguration.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabConfiguration.Controls.Add(Me.UserDetailsTabPage)
        Me.TabConfiguration.Controls.Add(Me.FileLocationsTabPage)
        Me.TabConfiguration.Controls.Add(Me.HtmlTabPage)
        Me.TabConfiguration.Controls.Add(Me.AppearanceTabPage)
        Me.TabConfiguration.Controls.Add(Me.DefaultsTabPage)
        Me.TabConfiguration.Location = New System.Drawing.Point(0, 0)
        Me.TabConfiguration.Name = "TabConfiguration"
        Me.TabConfiguration.SelectedIndex = 0
        Me.TabConfiguration.Size = New System.Drawing.Size(643, 295)
        Me.TabConfiguration.TabIndex = 1
        '
        'UserDetailsTabPage
        '
        Me.UserDetailsTabPage.Controls.Add(Me.txtOrganisation)
        Me.UserDetailsTabPage.Controls.Add(Me.OrganisationLabel)
        Me.UserDetailsTabPage.Controls.Add(Me.NameLabel)
        Me.UserDetailsTabPage.Controls.Add(Me.txtUsername)
        Me.UserDetailsTabPage.Controls.Add(Me.txtEmail)
        Me.UserDetailsTabPage.Controls.Add(Me.EmailLabel)
        Me.UserDetailsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.UserDetailsTabPage.Name = "UserDetailsTabPage"
        Me.UserDetailsTabPage.Size = New System.Drawing.Size(635, 269)
        Me.UserDetailsTabPage.TabIndex = 0
        Me.UserDetailsTabPage.Text = "User Details"
        Me.UserDetailsTabPage.UseVisualStyleBackColor = True
        '
        'txtOrganisation
        '
        Me.txtOrganisation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOrganisation.Location = New System.Drawing.Point(139, 85)
        Me.txtOrganisation.Name = "txtOrganisation"
        Me.txtOrganisation.Size = New System.Drawing.Size(469, 20)
        Me.txtOrganisation.TabIndex = 12
        '
        'OrganisationLabel
        '
        Me.OrganisationLabel.Location = New System.Drawing.Point(30, 81)
        Me.OrganisationLabel.Name = "OrganisationLabel"
        Me.OrganisationLabel.Size = New System.Drawing.Size(103, 24)
        Me.OrganisationLabel.TabIndex = 11
        Me.OrganisationLabel.Text = "Organisation:"
        Me.OrganisationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NameLabel
        '
        Me.NameLabel.Location = New System.Drawing.Point(43, 19)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(88, 24)
        Me.NameLabel.TabIndex = 7
        Me.NameLabel.Text = "Name:"
        Me.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUsername.Location = New System.Drawing.Point(139, 19)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(469, 20)
        Me.txtUsername.TabIndex = 8
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmail.Location = New System.Drawing.Point(139, 51)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(469, 20)
        Me.txtEmail.TabIndex = 10
        '
        'EmailLabel
        '
        Me.EmailLabel.Location = New System.Drawing.Point(27, 49)
        Me.EmailLabel.Name = "EmailLabel"
        Me.EmailLabel.Size = New System.Drawing.Size(104, 24)
        Me.EmailLabel.TabIndex = 9
        Me.EmailLabel.Text = "Email:"
        Me.EmailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FileLocationsTabPage
        '
        Me.FileLocationsTabPage.Controls.Add(Me.RestoreDefaultTerminologyServiceUrlButton)
        Me.FileLocationsTabPage.Controls.Add(Me.RestoreDefaultSharedRepositoryUrlButton)
        Me.FileLocationsTabPage.Controls.Add(Me.RepositoryPathTextBox)
        Me.FileLocationsTabPage.Controls.Add(Me.XmlRepositoryPathTextBox)
        Me.FileLocationsTabPage.Controls.Add(Me.XmlRepositoryPathLabel)
        Me.FileLocationsTabPage.Controls.Add(Me.XmlRepositoryBrowseButton)
        Me.FileLocationsTabPage.Controls.Add(Me.TerminologyServiceUrlLabel)
        Me.FileLocationsTabPage.Controls.Add(Me.TerminologyServiceUrlTextBox)
        Me.FileLocationsTabPage.Controls.Add(Me.TerminologyServiceUrlCheckBox)
        Me.FileLocationsTabPage.Controls.Add(Me.SharedRepositoryUrlLabel)
        Me.FileLocationsTabPage.Controls.Add(Me.SharedRepositoryUrlTextBox)
        Me.FileLocationsTabPage.Controls.Add(Me.SharedRepositoryUrlCheckBox)
        Me.FileLocationsTabPage.Controls.Add(Me.RepositoryPathLabel)
        Me.FileLocationsTabPage.Controls.Add(Me.RepositoryBrowseButton)
        Me.FileLocationsTabPage.Controls.Add(Me.RepositoryAutoSaveCheckBox)
        Me.FileLocationsTabPage.Controls.Add(Me.XmlRepositoryAutoSaveCheckBox)
        Me.FileLocationsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.FileLocationsTabPage.Name = "FileLocationsTabPage"
        Me.FileLocationsTabPage.Size = New System.Drawing.Size(635, 269)
        Me.FileLocationsTabPage.TabIndex = 1
        Me.FileLocationsTabPage.Text = "File Locations"
        Me.FileLocationsTabPage.UseVisualStyleBackColor = True
        '
        'RestoreDefaultTerminologyServiceUrlButton
        '
        Me.RestoreDefaultTerminologyServiceUrlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RestoreDefaultTerminologyServiceUrlButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RestoreDefaultTerminologyServiceUrlButton.Location = New System.Drawing.Point(519, 215)
        Me.RestoreDefaultTerminologyServiceUrlButton.Name = "RestoreDefaultTerminologyServiceUrlButton"
        Me.RestoreDefaultTerminologyServiceUrlButton.Size = New System.Drawing.Size(96, 32)
        Me.RestoreDefaultTerminologyServiceUrlButton.TabIndex = 15
        Me.RestoreDefaultTerminologyServiceUrlButton.Text = "Restore Default"
        '
        'RestoreDefaultSharedRepositoryUrlButton
        '
        Me.RestoreDefaultSharedRepositoryUrlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RestoreDefaultSharedRepositoryUrlButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RestoreDefaultSharedRepositoryUrlButton.Location = New System.Drawing.Point(519, 161)
        Me.RestoreDefaultSharedRepositoryUrlButton.Name = "RestoreDefaultSharedRepositoryUrlButton"
        Me.RestoreDefaultSharedRepositoryUrlButton.Size = New System.Drawing.Size(96, 32)
        Me.RestoreDefaultSharedRepositoryUrlButton.TabIndex = 11
        Me.RestoreDefaultSharedRepositoryUrlButton.Text = "Restore Default"
        '
        'XmlRepositoryPathLabel
        '
        Me.XmlRepositoryPathLabel.AutoSize = True
        Me.XmlRepositoryPathLabel.Location = New System.Drawing.Point(19, 69)
        Me.XmlRepositoryPathLabel.Name = "XmlRepositoryPathLabel"
        Me.XmlRepositoryPathLabel.Size = New System.Drawing.Size(155, 13)
        Me.XmlRepositoryPathLabel.TabIndex = 4
        Me.XmlRepositoryPathLabel.Text = "Archetype XML repository path:"
        Me.XmlRepositoryPathLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'XmlRepositoryBrowseButton
        '
        Me.XmlRepositoryBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XmlRepositoryBrowseButton.Image = CType(resources.GetObject("XmlRepositoryBrowseButton.Image"), System.Drawing.Image)
        Me.XmlRepositoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XmlRepositoryBrowseButton.Location = New System.Drawing.Point(519, 81)
        Me.XmlRepositoryBrowseButton.Name = "XmlRepositoryBrowseButton"
        Me.XmlRepositoryBrowseButton.Size = New System.Drawing.Size(96, 32)
        Me.XmlRepositoryBrowseButton.TabIndex = 7
        Me.XmlRepositoryBrowseButton.Text = "Browse..."
        Me.XmlRepositoryBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TerminologyServiceUrlLabel
        '
        Me.TerminologyServiceUrlLabel.Location = New System.Drawing.Point(19, 194)
        Me.TerminologyServiceUrlLabel.Name = "TerminologyServiceUrlLabel"
        Me.TerminologyServiceUrlLabel.Size = New System.Drawing.Size(218, 24)
        Me.TerminologyServiceUrlLabel.TabIndex = 12
        Me.TerminologyServiceUrlLabel.Text = "URL for terminology service:"
        Me.TerminologyServiceUrlLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TerminologyServiceUrlCheckBox
        '
        Me.TerminologyServiceUrlCheckBox.AutoSize = True
        Me.TerminologyServiceUrlCheckBox.Location = New System.Drawing.Point(242, 203)
        Me.TerminologyServiceUrlCheckBox.Name = "TerminologyServiceUrlCheckBox"
        Me.TerminologyServiceUrlCheckBox.Size = New System.Drawing.Size(150, 17)
        Me.TerminologyServiceUrlCheckBox.TabIndex = 13
        Me.TerminologyServiceUrlCheckBox.Text = "Enable terminology lookup"
        Me.TerminologyServiceUrlCheckBox.UseVisualStyleBackColor = True
        '
        'SharedRepositoryUrlLabel
        '
        Me.SharedRepositoryUrlLabel.Location = New System.Drawing.Point(19, 141)
        Me.SharedRepositoryUrlLabel.Name = "SharedRepositoryUrlLabel"
        Me.SharedRepositoryUrlLabel.Size = New System.Drawing.Size(217, 24)
        Me.SharedRepositoryUrlLabel.TabIndex = 8
        Me.SharedRepositoryUrlLabel.Text = "URL for shared repository:"
        Me.SharedRepositoryUrlLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'SharedRepositoryUrlCheckBox
        '
        Me.SharedRepositoryUrlCheckBox.AutoSize = True
        Me.SharedRepositoryUrlCheckBox.Location = New System.Drawing.Point(241, 149)
        Me.SharedRepositoryUrlCheckBox.Name = "SharedRepositoryUrlCheckBox"
        Me.SharedRepositoryUrlCheckBox.Size = New System.Drawing.Size(132, 17)
        Me.SharedRepositoryUrlCheckBox.TabIndex = 9
        Me.SharedRepositoryUrlCheckBox.Text = "Enable internet search"
        Me.SharedRepositoryUrlCheckBox.UseVisualStyleBackColor = True
        '
        'RepositoryAutoSaveCheckBox
        '
        Me.RepositoryAutoSaveCheckBox.AutoSize = True
        Me.RepositoryAutoSaveCheckBox.Location = New System.Drawing.Point(241, 19)
        Me.RepositoryAutoSaveCheckBox.Name = "RepositoryAutoSaveCheckBox"
        Me.RepositoryAutoSaveCheckBox.Size = New System.Drawing.Size(197, 17)
        Me.RepositoryAutoSaveCheckBox.TabIndex = 1
        Me.RepositoryAutoSaveCheckBox.Text = "Always auto-save when saving XML"
        Me.RepositoryAutoSaveCheckBox.UseVisualStyleBackColor = True
        '
        'XmlRepositoryAutoSaveCheckBox
        '
        Me.XmlRepositoryAutoSaveCheckBox.AutoSize = True
        Me.XmlRepositoryAutoSaveCheckBox.Location = New System.Drawing.Point(241, 69)
        Me.XmlRepositoryAutoSaveCheckBox.Name = "XmlRepositoryAutoSaveCheckBox"
        Me.XmlRepositoryAutoSaveCheckBox.Size = New System.Drawing.Size(196, 17)
        Me.XmlRepositoryAutoSaveCheckBox.TabIndex = 5
        Me.XmlRepositoryAutoSaveCheckBox.Text = "Always auto-save when saving ADL"
        Me.XmlRepositoryAutoSaveCheckBox.UseVisualStyleBackColor = True
        '
        'HtmlTabPage
        '
        Me.HtmlTabPage.Controls.Add(Me.XsltScriptPathCheckBox)
        Me.HtmlTabPage.Controls.Add(Me.XsltScriptPathExplanatoryLabel)
        Me.HtmlTabPage.Controls.Add(Me.XsltScriptPathTextBox)
        Me.HtmlTabPage.Controls.Add(Me.XsltScriptPathLabel)
        Me.HtmlTabPage.Controls.Add(Me.XsltScriptPathButton)
        Me.HtmlTabPage.Controls.Add(Me.ShowCommentsInHTMLCheckBox)
        Me.HtmlTabPage.Controls.Add(Me.ShowTerminologyInHTMLCheckBox)
        Me.HtmlTabPage.Location = New System.Drawing.Point(4, 22)
        Me.HtmlTabPage.Name = "HtmlTabPage"
        Me.HtmlTabPage.Size = New System.Drawing.Size(635, 269)
        Me.HtmlTabPage.TabIndex = 4
        Me.HtmlTabPage.Text = "HTML"
        Me.HtmlTabPage.UseVisualStyleBackColor = True
        '
        'XsltScriptPathCheckBox
        '
        Me.XsltScriptPathCheckBox.AutoSize = True
        Me.XsltScriptPathCheckBox.Location = New System.Drawing.Point(239, 9)
        Me.XsltScriptPathCheckBox.Name = "XsltScriptPathCheckBox"
        Me.XsltScriptPathCheckBox.Size = New System.Drawing.Size(165, 17)
        Me.XsltScriptPathCheckBox.TabIndex = 1
        Me.XsltScriptPathCheckBox.Text = "Use XSLT to generate HTML"
        Me.XsltScriptPathCheckBox.UseVisualStyleBackColor = True
        '
        'XsltScriptPathExplanatoryLabel
        '
        Me.XsltScriptPathExplanatoryLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XsltScriptPathExplanatoryLabel.Location = New System.Drawing.Point(14, 56)
        Me.XsltScriptPathExplanatoryLabel.Name = "XsltScriptPathExplanatoryLabel"
        Me.XsltScriptPathExplanatoryLabel.Size = New System.Drawing.Size(596, 40)
        Me.XsltScriptPathExplanatoryLabel.TabIndex = 4
        Me.XsltScriptPathExplanatoryLabel.Text = "If no XSLT script is supplied then a built-in HTML generator will be used."
        '
        'XsltScriptPathLabel
        '
        Me.XsltScriptPathLabel.AutoSize = True
        Me.XsltScriptPathLabel.Location = New System.Drawing.Point(17, 9)
        Me.XsltScriptPathLabel.Name = "XsltScriptPathLabel"
        Me.XsltScriptPathLabel.Size = New System.Drawing.Size(89, 13)
        Me.XsltScriptPathLabel.TabIndex = 0
        Me.XsltScriptPathLabel.Text = "XSLT script path:"
        Me.XsltScriptPathLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'XsltScriptPathButton
        '
        Me.XsltScriptPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XsltScriptPathButton.Image = CType(resources.GetObject("XsltScriptPathButton.Image"), System.Drawing.Image)
        Me.XsltScriptPathButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XsltScriptPathButton.Location = New System.Drawing.Point(517, 17)
        Me.XsltScriptPathButton.Name = "XsltScriptPathButton"
        Me.XsltScriptPathButton.Size = New System.Drawing.Size(96, 32)
        Me.XsltScriptPathButton.TabIndex = 3
        Me.XsltScriptPathButton.Text = "Browse..."
        Me.XsltScriptPathButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ShowCommentsInHTMLCheckBox
        '
        Me.ShowCommentsInHTMLCheckBox.AutoSize = True
        Me.ShowCommentsInHTMLCheckBox.Location = New System.Drawing.Point(17, 135)
        Me.ShowCommentsInHTMLCheckBox.Name = "ShowCommentsInHTMLCheckBox"
        Me.ShowCommentsInHTMLCheckBox.Size = New System.Drawing.Size(148, 17)
        Me.ShowCommentsInHTMLCheckBox.TabIndex = 6
        Me.ShowCommentsInHTMLCheckBox.Text = "Show comments in HTML"
        Me.ShowCommentsInHTMLCheckBox.UseVisualStyleBackColor = True
        '
        'ShowTerminologyInHTMLCheckBox
        '
        Me.ShowTerminologyInHTMLCheckBox.AutoSize = True
        Me.ShowTerminologyInHTMLCheckBox.Location = New System.Drawing.Point(17, 112)
        Me.ShowTerminologyInHTMLCheckBox.Name = "ShowTerminologyInHTMLCheckBox"
        Me.ShowTerminologyInHTMLCheckBox.Size = New System.Drawing.Size(153, 17)
        Me.ShowTerminologyInHTMLCheckBox.TabIndex = 5
        Me.ShowTerminologyInHTMLCheckBox.Text = "Show terminology in HTML"
        Me.ShowTerminologyInHTMLCheckBox.UseVisualStyleBackColor = True
        '
        'AppearanceTabPage
        '
        Me.AppearanceTabPage.Controls.Add(Me.ShowLinksButtonCheckBox)
        Me.AppearanceTabPage.Controls.Add(Me.OccurrencesLabel)
        Me.AppearanceTabPage.Controls.Add(Me.OccurrencesComboBox)
        Me.AppearanceTabPage.Controls.Add(Me.StateMachineGroupBox)
        Me.AppearanceTabPage.Location = New System.Drawing.Point(4, 22)
        Me.AppearanceTabPage.Name = "AppearanceTabPage"
        Me.AppearanceTabPage.Size = New System.Drawing.Size(635, 269)
        Me.AppearanceTabPage.TabIndex = 2
        Me.AppearanceTabPage.Text = "Appearance"
        Me.AppearanceTabPage.UseVisualStyleBackColor = True
        '
        'ShowLinksButtonCheckBox
        '
        Me.ShowLinksButtonCheckBox.AutoSize = True
        Me.ShowLinksButtonCheckBox.Location = New System.Drawing.Point(273, 142)
        Me.ShowLinksButtonCheckBox.Name = "ShowLinksButtonCheckBox"
        Me.ShowLinksButtonCheckBox.Size = New System.Drawing.Size(138, 17)
        Me.ShowLinksButtonCheckBox.TabIndex = 4
        Me.ShowLinksButtonCheckBox.Text = "Show the Links button?"
        Me.ShowLinksButtonCheckBox.UseVisualStyleBackColor = True
        '
        'OccurrencesLabel
        '
        Me.OccurrencesLabel.Location = New System.Drawing.Point(20, 142)
        Me.OccurrencesLabel.Name = "OccurrencesLabel"
        Me.OccurrencesLabel.Size = New System.Drawing.Size(93, 14)
        Me.OccurrencesLabel.TabIndex = 2
        Me.OccurrencesLabel.Text = "Occurrences:"
        Me.OccurrencesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OccurrencesComboBox
        '
        Me.OccurrencesComboBox.Items.AddRange(New Object() {"numeric", "lexical"})
        Me.OccurrencesComboBox.Location = New System.Drawing.Point(120, 139)
        Me.OccurrencesComboBox.Name = "OccurrencesComboBox"
        Me.OccurrencesComboBox.Size = New System.Drawing.Size(113, 21)
        Me.OccurrencesComboBox.TabIndex = 3
        Me.OccurrencesComboBox.Text = "numeric"
        '
        'DefaultsTabPage
        '
        Me.DefaultsTabPage.Controls.Add(Me.AutoSaveLabel)
        Me.DefaultsTabPage.Controls.Add(Me.numAutoSave)
        Me.DefaultsTabPage.Controls.Add(Me.ParserGroupBox)
        Me.DefaultsTabPage.Controls.Add(Me.ReferenceModelComboBox)
        Me.DefaultsTabPage.Controls.Add(Me.ReferenceModelLabel)
        Me.DefaultsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DefaultsTabPage.Name = "DefaultsTabPage"
        Me.DefaultsTabPage.Size = New System.Drawing.Size(635, 269)
        Me.DefaultsTabPage.TabIndex = 3
        Me.DefaultsTabPage.Text = "Defaults"
        Me.DefaultsTabPage.UseVisualStyleBackColor = True
        '
        'AutoSaveLabel
        '
        Me.AutoSaveLabel.AutoSize = True
        Me.AutoSaveLabel.Location = New System.Drawing.Point(16, 99)
        Me.AutoSaveLabel.Name = "AutoSaveLabel"
        Me.AutoSaveLabel.Size = New System.Drawing.Size(137, 13)
        Me.AutoSaveLabel.TabIndex = 4
        Me.AutoSaveLabel.Text = "Autosave interval (minutes):"
        '
        'numAutoSave
        '
        Me.numAutoSave.Location = New System.Drawing.Point(16, 116)
        Me.numAutoSave.Name = "numAutoSave"
        Me.numAutoSave.Size = New System.Drawing.Size(63, 20)
        Me.numAutoSave.TabIndex = 5
        Me.numAutoSave.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'ParserGroupBox
        '
        Me.ParserGroupBox.Controls.Add(Me.ParserXmlRadioButton)
        Me.ParserGroupBox.Controls.Add(Me.ParserAdlRadioButton)
        Me.ParserGroupBox.Location = New System.Drawing.Point(293, 36)
        Me.ParserGroupBox.Name = "ParserGroupBox"
        Me.ParserGroupBox.Size = New System.Drawing.Size(155, 67)
        Me.ParserGroupBox.TabIndex = 3
        Me.ParserGroupBox.TabStop = False
        Me.ParserGroupBox.Text = "Parser"
        '
        'ParserXmlRadioButton
        '
        Me.ParserXmlRadioButton.AutoSize = True
        Me.ParserXmlRadioButton.Location = New System.Drawing.Point(27, 42)
        Me.ParserXmlRadioButton.Name = "ParserXmlRadioButton"
        Me.ParserXmlRadioButton.Size = New System.Drawing.Size(47, 17)
        Me.ParserXmlRadioButton.TabIndex = 1
        Me.ParserXmlRadioButton.Text = "XML"
        Me.ParserXmlRadioButton.UseVisualStyleBackColor = True
        '
        'ParserAdlRadioButton
        '
        Me.ParserAdlRadioButton.AutoSize = True
        Me.ParserAdlRadioButton.Location = New System.Drawing.Point(27, 19)
        Me.ParserAdlRadioButton.Name = "ParserAdlRadioButton"
        Me.ParserAdlRadioButton.Size = New System.Drawing.Size(46, 17)
        Me.ParserAdlRadioButton.TabIndex = 0
        Me.ParserAdlRadioButton.Text = "ADL"
        Me.ParserAdlRadioButton.UseVisualStyleBackColor = True
        '
        'ApplicationOptionsForm
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.CloseButton
        Me.ClientSize = New System.Drawing.Size(643, 334)
        Me.Controls.Add(Me.TabConfiguration)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(600, 360)
        Me.Name = "ApplicationOptionsForm"
        Me.ShowInTaskbar = False
        Me.Text = "Options"
        Me.StateMachineGroupBox.ResumeLayout(False)
        Me.Panel_4.ResumeLayout(False)
        Me.TabConfiguration.ResumeLayout(False)
        Me.UserDetailsTabPage.ResumeLayout(False)
        Me.UserDetailsTabPage.PerformLayout()
        Me.FileLocationsTabPage.ResumeLayout(False)
        Me.FileLocationsTabPage.PerformLayout()
        Me.HtmlTabPage.ResumeLayout(False)
        Me.HtmlTabPage.PerformLayout()
        Me.AppearanceTabPage.ResumeLayout(False)
        Me.AppearanceTabPage.PerformLayout()
        Me.DefaultsTabPage.ResumeLayout(False)
        Me.DefaultsTabPage.PerformLayout()
        CType(Me.numAutoSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ParserGroupBox.ResumeLayout(False)
        Me.ParserGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ApplicationOptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TranslateGUI()

        Dim ms As StateMachineType() = ReferenceModel.ValidStateMachineTypes
        Debug.Assert(ms.Length = 8)
        Label_0.Text = ms(0).ToString
        Label_1.Text = ms(1).ToString
        Label_2.Text = ms(2).ToString
        Label_3.Text = ms(3).ToString
        Label_4.Text = ms(4).ToString
        Label_5.Text = ms(5).ToString
        Label_6.Text = ms(6).ToString
        Label_7.Text = ms(7).ToString
    End Sub

    Private Sub TranslateGUI()
        UserDetailsTabPage.Text = Filemanager.GetOpenEhrTerm(722, UserDetailsTabPage.Text)
        FileLocationsTabPage.Text = Filemanager.GetOpenEhrTerm(723, FileLocationsTabPage.Text)
        HtmlTabPage.Text = Filemanager.GetOpenEhrTerm(724, HtmlTabPage.Text)
        AppearanceTabPage.Text = Filemanager.GetOpenEhrTerm(725, AppearanceTabPage.Text)
        DefaultsTabPage.Text = Filemanager.GetOpenEhrTerm(726, DefaultsTabPage.Text)

        OkButton.Text = Filemanager.GetOpenEhrTerm(165, OkButton.Text)
        CloseButton.Text = Filemanager.GetOpenEhrTerm(166, CloseButton.Text)

        NameLabel.Text = Filemanager.GetOpenEhrTerm(727, NameLabel.Text)
        EmailLabel.Text = Filemanager.GetOpenEhrTerm(728, EmailLabel.Text)
        OrganisationLabel.Text = Filemanager.GetOpenEhrTerm(729, OrganisationLabel.Text)

        RepositoryPathLabel.Text = Filemanager.GetOpenEhrTerm(731, RepositoryPathLabel.Text)
        RepositoryAutoSaveCheckBox.Text = Filemanager.GetOpenEhrTerm(732, RepositoryAutoSaveCheckBox.Text)
        RepositoryBrowseButton.Text = Filemanager.GetOpenEhrTerm(627, RepositoryBrowseButton.Text)
        XmlRepositoryPathLabel.Text = Filemanager.GetOpenEhrTerm(733, XmlRepositoryPathLabel.Text)
        XmlRepositoryAutoSaveCheckBox.Text = Filemanager.GetOpenEhrTerm(734, XmlRepositoryAutoSaveCheckBox.Text)
        XmlRepositoryBrowseButton.Text = Filemanager.GetOpenEhrTerm(627, XmlRepositoryBrowseButton.Text)
        SharedRepositoryUrlLabel.Text = Filemanager.GetOpenEhrTerm(735, SharedRepositoryUrlLabel.Text)
        SharedRepositoryUrlCheckBox.Text = Filemanager.GetOpenEhrTerm(736, SharedRepositoryUrlCheckBox.Text)
        RestoreDefaultSharedRepositoryUrlButton.Text = Filemanager.GetOpenEhrTerm(730, RestoreDefaultSharedRepositoryUrlButton.Text)
        TerminologyServiceUrlLabel.Text = Filemanager.GetOpenEhrTerm(737, TerminologyServiceUrlLabel.Text)
        TerminologyServiceUrlCheckBox.Text = Filemanager.GetOpenEhrTerm(738, TerminologyServiceUrlCheckBox.Text)
        RestoreDefaultTerminologyServiceUrlButton.Text = Filemanager.GetOpenEhrTerm(730, RestoreDefaultTerminologyServiceUrlButton.Text)

        XsltScriptPathLabel.Text = Filemanager.GetOpenEhrTerm(739, XsltScriptPathLabel.Text)
        XsltScriptPathCheckBox.Text = Filemanager.GetOpenEhrTerm(740, XsltScriptPathCheckBox.Text)
        XsltScriptPathButton.Text = Filemanager.GetOpenEhrTerm(627, XsltScriptPathButton.Text)
        XsltScriptPathExplanatoryLabel.Text = Filemanager.GetOpenEhrTerm(741, XsltScriptPathExplanatoryLabel.Text)
        ShowTerminologyInHTMLCheckBox.Text = Filemanager.GetOpenEhrTerm(742, ShowTerminologyInHTMLCheckBox.Text)
        ShowCommentsInHTMLCheckBox.Text = Filemanager.GetOpenEhrTerm(743, ShowCommentsInHTMLCheckBox.Text)

        StateMachineGroupBox.Text = Filemanager.GetOpenEhrTerm(744, StateMachineGroupBox.Text)
        OccurrencesLabel.Text = Filemanager.GetOpenEhrTerm(745, OccurrencesLabel.Text)
        OccurrencesComboBox.Items.Clear()
        OccurrencesComboBox.Items.Add(Filemanager.GetOpenEhrTerm(746, "numeric"))
        OccurrencesComboBox.Items.Add(Filemanager.GetOpenEhrTerm(747, "lexical"))
        ShowLinksButtonCheckBox.Text = Filemanager.GetOpenEhrTerm(748, ShowLinksButtonCheckBox.Text)

        ReferenceModelComboBox.Text = Filemanager.GetOpenEhrTerm(749, ReferenceModelComboBox.Text)
        ParserGroupBox.Text = Filemanager.GetOpenEhrTerm(750, ParserGroupBox.Text)
        ParserAdlRadioButton.Text = Filemanager.GetOpenEhrTerm(751, ParserAdlRadioButton.Text)
        ParserXmlRadioButton.Text = Filemanager.GetOpenEhrTerm(752, ParserXmlRadioButton.Text)
        AutoSaveLabel.Text = Filemanager.GetOpenEhrTerm(753, AutoSaveLabel.Text)
    End Sub

    Private Sub RepositoryBrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepositoryBrowseButton.Click
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop
        FolderBrowserDialog1.ShowNewFolderButton = True

        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            RepositoryPathTextBox.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub XmlRepositoryBrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XmlRepositoryBrowseButton.Click
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop
        FolderBrowserDialog1.ShowNewFolderButton = True

        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            XmlRepositoryPathTextBox.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub XsltScriptPathButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XsltScriptPathButton.Click
        If XsltScriptPathTextBox.Text = "" Then
            OpenFileDialog1.InitialDirectory = IO.Path.Combine(Application.StartupPath, "HTML")
        Else
            OpenFileDialog1.InitialDirectory = IO.Path.GetDirectoryName(XsltScriptPathTextBox.Text)
        End If

        OpenFileDialog1.Filter = "XSLT|*.xsl;*.xslt"

        If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            XsltScriptPathTextBox.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ColourPanel_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Panel_0.DoubleClick, Panel_1.DoubleClick, Panel_2.DoubleClick, Panel_3.DoubleClick, Panel_4.DoubleClick, Panel_5.DoubleClick, Panel_6.DoubleClick, Panel_7.DoubleClick
        Dim p As Panel = CType(sender, Panel)
        p.BorderStyle = BorderStyle.FixedSingle
        ColorDialog1.FullOpen = True
        ColorDialog1.AnyColor = True
        ColorDialog1.Color = p.BackColor

        If ColorDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            p.BackColor = ColorDialog1.Color
        End If

        p.BorderStyle = BorderStyle.Fixed3D
    End Sub

End Class

