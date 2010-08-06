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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblArchetypePath As System.Windows.Forms.Label
    Friend WithEvents RepositoryBrowseButton As System.Windows.Forms.Button
    Friend WithEvents RepositoryPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents comboReferenceModel As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents gbUserDetails As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
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
    Friend WithEvents tpUser As System.Windows.Forms.TabPage
    Friend WithEvents tpLocations As System.Windows.Forms.TabPage
    Friend WithEvents tpAppearance As System.Windows.Forms.TabPage
    Friend WithEvents tpDefaults As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents butHelpBrowse As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtHelpFile As System.Windows.Forms.TextBox
    Friend WithEvents lblOccurrences As System.Windows.Forms.Label
    Friend WithEvents comboOccurrences As System.Windows.Forms.ComboBox
    Friend WithEvents txtOrganisation As System.Windows.Forms.TextBox
    Friend WithEvents lblOrganisation As System.Windows.Forms.Label
    Friend WithEvents Panel_7 As System.Windows.Forms.Panel
    Friend WithEvents grpParser As System.Windows.Forms.GroupBox
    Friend WithEvents chkParserXML As System.Windows.Forms.CheckBox
    Friend WithEvents chkParserADL As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowTerminologyInHTML As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowCommentsInHTML As System.Windows.Forms.CheckBox
    Friend WithEvents chkWebSearch As System.Windows.Forms.CheckBox
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents lblTerminology As System.Windows.Forms.Label
    Friend WithEvents txtTerminologyURL As System.Windows.Forms.TextBox
    Friend WithEvents chkTerminology As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents numAutoSave As System.Windows.Forms.NumericUpDown
    Friend WithEvents XmlRepositoryPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents XmlRepositoryBrowseButton As System.Windows.Forms.Button
    Friend WithEvents XmlRepositoryAutoSaveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RepositoryAutoSaveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label_7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApplicationOptionsForm))
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.lblArchetypePath = New System.Windows.Forms.Label
        Me.RepositoryPathTextBox = New System.Windows.Forms.TextBox
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtHelpFile = New System.Windows.Forms.TextBox
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.txtTerminologyURL = New System.Windows.Forms.TextBox
        Me.XmlRepositoryPathTextBox = New System.Windows.Forms.TextBox
        Me.RepositoryBrowseButton = New System.Windows.Forms.Button
        Me.comboReferenceModel = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.gbUserDetails = New System.Windows.Forms.GroupBox
        Me.txtOrganisation = New System.Windows.Forms.TextBox
        Me.lblOrganisation = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
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
        Me.tpUser = New System.Windows.Forms.TabPage
        Me.tpLocations = New System.Windows.Forms.TabPage
        Me.XmlRepositoryAutoSaveCheckBox = New System.Windows.Forms.CheckBox
        Me.RepositoryAutoSaveCheckBox = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.XmlRepositoryBrowseButton = New System.Windows.Forms.Button
        Me.lblTerminology = New System.Windows.Forms.Label
        Me.chkTerminology = New System.Windows.Forms.CheckBox
        Me.lblURL = New System.Windows.Forms.Label
        Me.chkWebSearch = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.butHelpBrowse = New System.Windows.Forms.Button
        Me.tpAppearance = New System.Windows.Forms.TabPage
        Me.chkShowCommentsInHTML = New System.Windows.Forms.CheckBox
        Me.chkShowTerminologyInHTML = New System.Windows.Forms.CheckBox
        Me.lblOccurrences = New System.Windows.Forms.Label
        Me.comboOccurrences = New System.Windows.Forms.ComboBox
        Me.tpDefaults = New System.Windows.Forms.TabPage
        Me.Label5 = New System.Windows.Forms.Label
        Me.numAutoSave = New System.Windows.Forms.NumericUpDown
        Me.grpParser = New System.Windows.Forms.GroupBox
        Me.chkParserXML = New System.Windows.Forms.CheckBox
        Me.chkParserADL = New System.Windows.Forms.CheckBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.gbUserDetails.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel_4.SuspendLayout()
        Me.TabConfiguration.SuspendLayout()
        Me.tpUser.SuspendLayout()
        Me.tpLocations.SuspendLayout()
        Me.tpAppearance.SuspendLayout()
        Me.tpDefaults.SuspendLayout()
        CType(Me.numAutoSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpParser.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 24)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Email"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmail.Location = New System.Drawing.Point(120, 56)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(469, 20)
        Me.txtEmail.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUsername.Location = New System.Drawing.Point(120, 24)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(412, 20)
        Me.txtUsername.TabIndex = 2
        '
        'lblArchetypePath
        '
        Me.lblArchetypePath.AutoSize = True
        Me.lblArchetypePath.Location = New System.Drawing.Point(17, 11)
        Me.lblArchetypePath.Name = "lblArchetypePath"
        Me.lblArchetypePath.Size = New System.Drawing.Size(127, 13)
        Me.lblArchetypePath.TabIndex = 0
        Me.lblArchetypePath.Text = "Archetype repository path"
        Me.lblArchetypePath.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'RepositoryPathTextBox
        '
        Me.RepositoryPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RepositoryPathTextBox.Location = New System.Drawing.Point(17, 28)
        Me.RepositoryPathTextBox.Name = "RepositoryPathTextBox"
        Me.RepositoryPathTextBox.Size = New System.Drawing.Size(490, 20)
        Me.RepositoryPathTextBox.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.RepositoryPathTextBox, "Leave blank for last directory used")
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(539, 304)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(80, 24)
        Me.butCancel.TabIndex = 3
        Me.butCancel.Text = "Cancel"
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(449, 304)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(80, 24)
        Me.butOK.TabIndex = 2
        Me.butOK.Text = "OK"
        '
        'txtHelpFile
        '
        Me.txtHelpFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHelpFile.Location = New System.Drawing.Point(17, 128)
        Me.txtHelpFile.Name = "txtHelpFile"
        Me.txtHelpFile.Size = New System.Drawing.Size(490, 20)
        Me.txtHelpFile.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtHelpFile, "Leave blank for last directory used")
        '
        'txtURL
        '
        Me.txtURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtURL.Location = New System.Drawing.Point(17, 228)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(594, 20)
        Me.txtURL.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtURL, "Http address of repository")
        '
        'txtTerminologyURL
        '
        Me.txtTerminologyURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTerminologyURL.Location = New System.Drawing.Point(17, 178)
        Me.txtTerminologyURL.Name = "txtTerminologyURL"
        Me.txtTerminologyURL.Size = New System.Drawing.Size(595, 20)
        Me.txtTerminologyURL.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtTerminologyURL, "Http address of repository")
        '
        'XmlRepositoryPathTextBox
        '
        Me.XmlRepositoryPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XmlRepositoryPathTextBox.Location = New System.Drawing.Point(17, 78)
        Me.XmlRepositoryPathTextBox.Name = "XmlRepositoryPathTextBox"
        Me.XmlRepositoryPathTextBox.Size = New System.Drawing.Size(490, 20)
        Me.XmlRepositoryPathTextBox.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.XmlRepositoryPathTextBox, "Leave blank for last directory used")
        '
        'RepositoryBrowseButton
        '
        Me.RepositoryBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RepositoryBrowseButton.Image = CType(resources.GetObject("RepositoryBrowseButton.Image"), System.Drawing.Image)
        Me.RepositoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RepositoryBrowseButton.Location = New System.Drawing.Point(517, 17)
        Me.RepositoryBrowseButton.Name = "RepositoryBrowseButton"
        Me.RepositoryBrowseButton.Size = New System.Drawing.Size(96, 32)
        Me.RepositoryBrowseButton.TabIndex = 3
        Me.RepositoryBrowseButton.Text = "Browse..."
        Me.RepositoryBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'comboReferenceModel
        '
        Me.comboReferenceModel.Location = New System.Drawing.Point(16, 56)
        Me.comboReferenceModel.Name = "comboReferenceModel"
        Me.comboReferenceModel.Size = New System.Drawing.Size(208, 21)
        Me.comboReferenceModel.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 32)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Default reference model"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'gbUserDetails
        '
        Me.gbUserDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbUserDetails.Controls.Add(Me.txtOrganisation)
        Me.gbUserDetails.Controls.Add(Me.lblOrganisation)
        Me.gbUserDetails.Controls.Add(Me.Label2)
        Me.gbUserDetails.Controls.Add(Me.txtUsername)
        Me.gbUserDetails.Controls.Add(Me.txtEmail)
        Me.gbUserDetails.Controls.Add(Me.Label3)
        Me.gbUserDetails.Location = New System.Drawing.Point(16, 7)
        Me.gbUserDetails.Name = "gbUserDetails"
        Me.gbUserDetails.Size = New System.Drawing.Size(602, 125)
        Me.gbUserDetails.TabIndex = 23
        Me.gbUserDetails.TabStop = False
        Me.gbUserDetails.Text = "User details"
        '
        'txtOrganisation
        '
        Me.txtOrganisation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOrganisation.Location = New System.Drawing.Point(120, 90)
        Me.txtOrganisation.Name = "txtOrganisation"
        Me.txtOrganisation.Size = New System.Drawing.Size(469, 20)
        Me.txtOrganisation.TabIndex = 6
        '
        'lblOrganisation
        '
        Me.lblOrganisation.Location = New System.Drawing.Point(7, 86)
        Me.lblOrganisation.Name = "lblOrganisation"
        Me.lblOrganisation.Size = New System.Drawing.Size(103, 24)
        Me.lblOrganisation.TabIndex = 5
        Me.lblOrganisation.Text = "Organisation"
        Me.lblOrganisation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel_7)
        Me.GroupBox1.Controls.Add(Me.Label_7)
        Me.GroupBox1.Controls.Add(Me.Panel_6)
        Me.GroupBox1.Controls.Add(Me.Label_6)
        Me.GroupBox1.Controls.Add(Me.Panel_4)
        Me.GroupBox1.Controls.Add(Me.Label_4)
        Me.GroupBox1.Controls.Add(Me.Panel_1)
        Me.GroupBox1.Controls.Add(Me.Label_1)
        Me.GroupBox1.Controls.Add(Me.Panel_3)
        Me.GroupBox1.Controls.Add(Me.Label_3)
        Me.GroupBox1.Controls.Add(Me.Panel_2)
        Me.GroupBox1.Controls.Add(Me.Label_2)
        Me.GroupBox1.Controls.Add(Me.Panel_0)
        Me.GroupBox1.Controls.Add(Me.Label_0)
        Me.GroupBox1.Controls.Add(Me.Panel_5)
        Me.GroupBox1.Controls.Add(Me.Label_5)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(464, 117)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "State machine colours"
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
        Me.TabConfiguration.Controls.Add(Me.tpUser)
        Me.TabConfiguration.Controls.Add(Me.tpLocations)
        Me.TabConfiguration.Controls.Add(Me.tpAppearance)
        Me.TabConfiguration.Controls.Add(Me.tpDefaults)
        Me.TabConfiguration.Location = New System.Drawing.Point(0, 0)
        Me.TabConfiguration.Name = "TabConfiguration"
        Me.TabConfiguration.SelectedIndex = 0
        Me.TabConfiguration.Size = New System.Drawing.Size(643, 295)
        Me.TabConfiguration.TabIndex = 1
        '
        'tpUser
        '
        Me.tpUser.Controls.Add(Me.gbUserDetails)
        Me.tpUser.Location = New System.Drawing.Point(4, 22)
        Me.tpUser.Name = "tpUser"
        Me.tpUser.Size = New System.Drawing.Size(635, 269)
        Me.tpUser.TabIndex = 0
        Me.tpUser.Text = "User details"
        '
        'tpLocations
        '
        Me.tpLocations.Controls.Add(Me.RepositoryPathTextBox)
        Me.tpLocations.Controls.Add(Me.XmlRepositoryPathTextBox)
        Me.tpLocations.Controls.Add(Me.Label6)
        Me.tpLocations.Controls.Add(Me.XmlRepositoryBrowseButton)
        Me.tpLocations.Controls.Add(Me.txtHelpFile)
        Me.tpLocations.Controls.Add(Me.lblTerminology)
        Me.tpLocations.Controls.Add(Me.txtTerminologyURL)
        Me.tpLocations.Controls.Add(Me.chkTerminology)
        Me.tpLocations.Controls.Add(Me.lblURL)
        Me.tpLocations.Controls.Add(Me.txtURL)
        Me.tpLocations.Controls.Add(Me.chkWebSearch)
        Me.tpLocations.Controls.Add(Me.Label4)
        Me.tpLocations.Controls.Add(Me.butHelpBrowse)
        Me.tpLocations.Controls.Add(Me.lblArchetypePath)
        Me.tpLocations.Controls.Add(Me.RepositoryBrowseButton)
        Me.tpLocations.Controls.Add(Me.RepositoryAutoSaveCheckBox)
        Me.tpLocations.Controls.Add(Me.XmlRepositoryAutoSaveCheckBox)
        Me.tpLocations.Location = New System.Drawing.Point(4, 22)
        Me.tpLocations.Name = "tpLocations"
        Me.tpLocations.Size = New System.Drawing.Size(635, 269)
        Me.tpLocations.TabIndex = 1
        Me.tpLocations.Text = "File locations"
        '
        'XmlRepositoryAutoSaveCheckBox
        '
        Me.XmlRepositoryAutoSaveCheckBox.AutoSize = True
        Me.XmlRepositoryAutoSaveCheckBox.Location = New System.Drawing.Point(239, 59)
        Me.XmlRepositoryAutoSaveCheckBox.Name = "XmlRepositoryAutoSaveCheckBox"
        Me.XmlRepositoryAutoSaveCheckBox.Size = New System.Drawing.Size(196, 17)
        Me.XmlRepositoryAutoSaveCheckBox.TabIndex = 5
        Me.XmlRepositoryAutoSaveCheckBox.Text = "Always auto-save when saving ADL"
        Me.XmlRepositoryAutoSaveCheckBox.UseVisualStyleBackColor = True
        '
        'RepositoryAutoSaveCheckBox
        '
        Me.RepositoryAutoSaveCheckBox.AutoSize = True
        Me.RepositoryAutoSaveCheckBox.Location = New System.Drawing.Point(239, 9)
        Me.RepositoryAutoSaveCheckBox.Name = "RepositoryAutoSaveCheckBox"
        Me.RepositoryAutoSaveCheckBox.Size = New System.Drawing.Size(197, 17)
        Me.RepositoryAutoSaveCheckBox.TabIndex = 1
        Me.RepositoryAutoSaveCheckBox.Text = "Always auto-save when saving XML"
        Me.RepositoryAutoSaveCheckBox.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Archetype XML repository path"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'XmlRepositoryBrowseButton
        '
        Me.XmlRepositoryBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XmlRepositoryBrowseButton.Image = CType(resources.GetObject("XmlRepositoryBrowseButton.Image"), System.Drawing.Image)
        Me.XmlRepositoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XmlRepositoryBrowseButton.Location = New System.Drawing.Point(517, 67)
        Me.XmlRepositoryBrowseButton.Name = "XmlRepositoryBrowseButton"
        Me.XmlRepositoryBrowseButton.Size = New System.Drawing.Size(96, 32)
        Me.XmlRepositoryBrowseButton.TabIndex = 7
        Me.XmlRepositoryBrowseButton.Text = "Browse..."
        Me.XmlRepositoryBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTerminology
        '
        Me.lblTerminology.Location = New System.Drawing.Point(17, 152)
        Me.lblTerminology.Name = "lblTerminology"
        Me.lblTerminology.Size = New System.Drawing.Size(218, 24)
        Me.lblTerminology.TabIndex = 11
        Me.lblTerminology.Text = "URL for Terminology Service"
        Me.lblTerminology.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'chkTerminology
        '
        Me.chkTerminology.AutoSize = True
        Me.chkTerminology.Location = New System.Drawing.Point(240, 159)
        Me.chkTerminology.Name = "chkTerminology"
        Me.chkTerminology.Size = New System.Drawing.Size(160, 17)
        Me.chkTerminology.TabIndex = 12
        Me.chkTerminology.Text = "Enable Terminology LookUp"
        Me.chkTerminology.UseVisualStyleBackColor = True
        '
        'lblURL
        '
        Me.lblURL.Location = New System.Drawing.Point(17, 203)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(217, 24)
        Me.lblURL.TabIndex = 14
        Me.lblURL.Text = "URL for shared repository"
        Me.lblURL.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'chkWebSearch
        '
        Me.chkWebSearch.AutoSize = True
        Me.chkWebSearch.Location = New System.Drawing.Point(239, 209)
        Me.chkWebSearch.Name = "chkWebSearch"
        Me.chkWebSearch.Size = New System.Drawing.Size(135, 17)
        Me.chkWebSearch.TabIndex = 15
        Me.chkWebSearch.Text = "Enable Internet Search"
        Me.chkWebSearch.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Help file"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'butHelpBrowse
        '
        Me.butHelpBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butHelpBrowse.Image = CType(resources.GetObject("butHelpBrowse.Image"), System.Drawing.Image)
        Me.butHelpBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.butHelpBrowse.Location = New System.Drawing.Point(519, 118)
        Me.butHelpBrowse.Name = "butHelpBrowse"
        Me.butHelpBrowse.Size = New System.Drawing.Size(96, 32)
        Me.butHelpBrowse.TabIndex = 10
        Me.butHelpBrowse.Text = "Browse..."
        Me.butHelpBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tpAppearance
        '
        Me.tpAppearance.Controls.Add(Me.chkShowCommentsInHTML)
        Me.tpAppearance.Controls.Add(Me.chkShowTerminologyInHTML)
        Me.tpAppearance.Controls.Add(Me.lblOccurrences)
        Me.tpAppearance.Controls.Add(Me.comboOccurrences)
        Me.tpAppearance.Controls.Add(Me.GroupBox1)
        Me.tpAppearance.Location = New System.Drawing.Point(4, 22)
        Me.tpAppearance.Name = "tpAppearance"
        Me.tpAppearance.Size = New System.Drawing.Size(635, 269)
        Me.tpAppearance.TabIndex = 2
        Me.tpAppearance.Text = "Appearance"
        '
        'chkShowCommentsInHTML
        '
        Me.chkShowCommentsInHTML.AutoSize = True
        Me.chkShowCommentsInHTML.Location = New System.Drawing.Point(274, 161)
        Me.chkShowCommentsInHTML.Name = "chkShowCommentsInHTML"
        Me.chkShowCommentsInHTML.Size = New System.Drawing.Size(148, 17)
        Me.chkShowCommentsInHTML.TabIndex = 5
        Me.chkShowCommentsInHTML.Text = "Show comments in HTML"
        Me.chkShowCommentsInHTML.UseVisualStyleBackColor = True
        '
        'chkShowTerminologyInHTML
        '
        Me.chkShowTerminologyInHTML.AutoSize = True
        Me.chkShowTerminologyInHTML.Location = New System.Drawing.Point(274, 141)
        Me.chkShowTerminologyInHTML.Name = "chkShowTerminologyInHTML"
        Me.chkShowTerminologyInHTML.Size = New System.Drawing.Size(153, 17)
        Me.chkShowTerminologyInHTML.TabIndex = 4
        Me.chkShowTerminologyInHTML.Text = "Show terminology in HTML"
        Me.chkShowTerminologyInHTML.UseVisualStyleBackColor = True
        '
        'lblOccurrences
        '
        Me.lblOccurrences.Location = New System.Drawing.Point(20, 142)
        Me.lblOccurrences.Name = "lblOccurrences"
        Me.lblOccurrences.Size = New System.Drawing.Size(93, 14)
        Me.lblOccurrences.TabIndex = 2
        Me.lblOccurrences.Text = "Occurrences"
        '
        'comboOccurrences
        '
        Me.comboOccurrences.Items.AddRange(New Object() {"numeric", "lexical"})
        Me.comboOccurrences.Location = New System.Drawing.Point(120, 139)
        Me.comboOccurrences.Name = "comboOccurrences"
        Me.comboOccurrences.Size = New System.Drawing.Size(113, 21)
        Me.comboOccurrences.TabIndex = 3
        Me.comboOccurrences.Text = "numeric"
        '
        'tpDefaults
        '
        Me.tpDefaults.Controls.Add(Me.Label5)
        Me.tpDefaults.Controls.Add(Me.numAutoSave)
        Me.tpDefaults.Controls.Add(Me.grpParser)
        Me.tpDefaults.Controls.Add(Me.comboReferenceModel)
        Me.tpDefaults.Controls.Add(Me.Label1)
        Me.tpDefaults.Location = New System.Drawing.Point(4, 22)
        Me.tpDefaults.Name = "tpDefaults"
        Me.tpDefaults.Size = New System.Drawing.Size(635, 269)
        Me.tpDefaults.TabIndex = 3
        Me.tpDefaults.Text = "Defaults"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Autosave interval (minutes)"
        '
        'numAutoSave
        '
        Me.numAutoSave.Location = New System.Drawing.Point(16, 116)
        Me.numAutoSave.Name = "numAutoSave"
        Me.numAutoSave.Size = New System.Drawing.Size(63, 20)
        Me.numAutoSave.TabIndex = 5
        Me.numAutoSave.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'grpParser
        '
        Me.grpParser.Controls.Add(Me.chkParserXML)
        Me.grpParser.Controls.Add(Me.chkParserADL)
        Me.grpParser.Location = New System.Drawing.Point(293, 36)
        Me.grpParser.Name = "grpParser"
        Me.grpParser.Size = New System.Drawing.Size(155, 67)
        Me.grpParser.TabIndex = 3
        Me.grpParser.TabStop = False
        Me.grpParser.Text = "Parser"
        '
        'chkParserXML
        '
        Me.chkParserXML.AutoSize = True
        Me.chkParserXML.Location = New System.Drawing.Point(27, 38)
        Me.chkParserXML.Name = "chkParserXML"
        Me.chkParserXML.Size = New System.Drawing.Size(48, 17)
        Me.chkParserXML.TabIndex = 1
        Me.chkParserXML.Text = "XML"
        Me.chkParserXML.UseVisualStyleBackColor = True
        '
        'chkParserADL
        '
        Me.chkParserADL.AutoSize = True
        Me.chkParserADL.Location = New System.Drawing.Point(27, 18)
        Me.chkParserADL.Name = "chkParserADL"
        Me.chkParserADL.Size = New System.Drawing.Size(47, 17)
        Me.chkParserADL.TabIndex = 0
        Me.chkParserADL.Text = "ADL"
        Me.chkParserADL.UseVisualStyleBackColor = True
        '
        'ApplicationOptionsForm
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(643, 334)
        Me.Controls.Add(Me.TabConfiguration)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.butCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(600, 360)
        Me.Name = "ApplicationOptionsForm"
        Me.ShowInTaskbar = False
        Me.Text = "Options"
        Me.gbUserDetails.ResumeLayout(False)
        Me.gbUserDetails.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel_4.ResumeLayout(False)
        Me.TabConfiguration.ResumeLayout(False)
        Me.tpUser.ResumeLayout(False)
        Me.tpLocations.ResumeLayout(False)
        Me.tpLocations.PerformLayout()
        Me.tpAppearance.ResumeLayout(False)
        Me.tpAppearance.PerformLayout()
        Me.tpDefaults.ResumeLayout(False)
        Me.tpDefaults.PerformLayout()
        CType(Me.numAutoSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpParser.ResumeLayout(False)
        Me.grpParser.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub RepositoryBrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepositoryBrowseButton.Click
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.ShowNewFolderButton = True

        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            RepositoryPathTextBox.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub XmlRepositoryBrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XmlRepositoryBrowseButton.Click
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.ShowNewFolderButton = True

        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            XmlRepositoryPathTextBox.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ApplicationOptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ms As StateMachineType()

        ms = ReferenceModel.ValidStateMachineTypes
        Debug.Assert(ms.Length = 8)
        Me.Label_0.Text = ms(0).ToString
        Me.Label_1.Text = ms(1).ToString
        Me.Label_2.Text = ms(2).ToString
        Me.Label_3.Text = ms(3).ToString
        Me.Label_4.Text = ms(4).ToString
        Me.Label_5.Text = ms(5).ToString
        Me.Label_6.Text = ms(6).ToString
        Me.Label_7.Text = ms(7).ToString
    End Sub

    Private Sub ColourPanel_doubleClisk(ByVal sender As System.Object, ByVal e As EventArgs) Handles Panel_4.DoubleClick, Panel_0.DoubleClick, Panel_2.DoubleClick, Panel_1.DoubleClick, Panel_3.DoubleClick
        Dim p As Panel

        p = CType(sender, Panel)
        p.BorderStyle = BorderStyle.FixedSingle
        Me.ColorDialog1.FullOpen = True
        Me.ColorDialog1.AnyColor = True
        Me.ColorDialog1.Color = p.BackColor
        If Me.ColorDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            p.BackColor = ColorDialog1.Color
        End If
        p.BorderStyle = BorderStyle.Fixed3D

    End Sub

    Private Sub butHelpBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butHelpBrowse.Click
        Me.OpenFileDialog1.InitialDirectory = Application.StartupPath & "\Help"
        Me.OpenFileDialog1.Filter = "Windows help (chm)|*.chm|HTML|*.htm, *.html"
        If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Me.txtHelpFile.Text = OpenFileDialog1.FileName
        End If

    End Sub

    Private Sub chkParserADL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkParserADL.CheckedChanged
        chkParserXML.Checked = Not chkParserADL.Checked
    End Sub

    Private Sub chkParserXML_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkParserXML.CheckedChanged
        chkParserADL.Checked = Not chkParserXML.Checked
    End Sub

End Class
