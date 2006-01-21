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
    Friend WithEvents butBrowse As System.Windows.Forms.Button
    Friend WithEvents txtRepositoryPath As System.Windows.Forms.TextBox
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ApplicationOptionsForm))
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.lblArchetypePath = New System.Windows.Forms.Label
        Me.txtRepositoryPath = New System.Windows.Forms.TextBox
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtHelpFile = New System.Windows.Forms.TextBox
        Me.butBrowse = New System.Windows.Forms.Button
        Me.comboReferenceModel = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.gbUserDetails = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
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
        Me.Label4 = New System.Windows.Forms.Label
        Me.butHelpBrowse = New System.Windows.Forms.Button
        Me.tpAppearance = New System.Windows.Forms.TabPage
        Me.tpDefaults = New System.Windows.Forms.TabPage
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.gbUserDetails.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel_4.SuspendLayout()
        Me.TabConfiguration.SuspendLayout()
        Me.tpUser.SuspendLayout()
        Me.tpLocations.SuspendLayout()
        Me.tpAppearance.SuspendLayout()
        Me.tpDefaults.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(10, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(124, 28)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Email address"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(144, 65)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(274, 22)
        Me.txtEmail.TabIndex = 12
        Me.txtEmail.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(29, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 27)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(144, 28)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(202, 22)
        Me.txtUsername.TabIndex = 10
        Me.txtUsername.Text = ""
        '
        'lblArchetypePath
        '
        Me.lblArchetypePath.Location = New System.Drawing.Point(38, 18)
        Me.lblArchetypePath.Name = "lblArchetypePath"
        Me.lblArchetypePath.Size = New System.Drawing.Size(173, 28)
        Me.lblArchetypePath.TabIndex = 17
        Me.lblArchetypePath.Text = "Archetype repository path"
        Me.lblArchetypePath.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtRepositoryPath
        '
        Me.txtRepositoryPath.Location = New System.Drawing.Point(38, 55)
        Me.txtRepositoryPath.Name = "txtRepositoryPath"
        Me.txtRepositoryPath.Size = New System.Drawing.Size(442, 22)
        Me.txtRepositoryPath.TabIndex = 16
        Me.txtRepositoryPath.Text = ""
        Me.ToolTip1.SetToolTip(Me.txtRepositoryPath, "Leave blank for last directory used")
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(403, 240)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(96, 28)
        Me.butCancel.TabIndex = 18
        Me.butCancel.Text = "Cancel"
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(518, 240)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(96, 28)
        Me.butOK.TabIndex = 19
        Me.butOK.Text = "OK"
        '
        'txtHelpFile
        '
        Me.txtHelpFile.Location = New System.Drawing.Point(38, 120)
        Me.txtHelpFile.Name = "txtHelpFile"
        Me.txtHelpFile.Size = New System.Drawing.Size(442, 22)
        Me.txtHelpFile.TabIndex = 21
        Me.txtHelpFile.Text = ""
        Me.ToolTip1.SetToolTip(Me.txtHelpFile, "Leave blank for last directory used")
        '
        'butBrowse
        '
        Me.butBrowse.Image = CType(resources.GetObject("butBrowse.Image"), System.Drawing.Image)
        Me.butBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.butBrowse.Location = New System.Drawing.Point(499, 46)
        Me.butBrowse.Name = "butBrowse"
        Me.butBrowse.Size = New System.Drawing.Size(115, 37)
        Me.butBrowse.TabIndex = 20
        Me.butBrowse.Text = "Browse..."
        Me.butBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'comboReferenceModel
        '
        Me.comboReferenceModel.Location = New System.Drawing.Point(19, 65)
        Me.comboReferenceModel.Name = "comboReferenceModel"
        Me.comboReferenceModel.Size = New System.Drawing.Size(250, 22)
        Me.comboReferenceModel.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(19, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 37)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Default reference model"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'gbUserDetails
        '
        Me.gbUserDetails.Controls.Add(Me.Label2)
        Me.gbUserDetails.Controls.Add(Me.txtUsername)
        Me.gbUserDetails.Controls.Add(Me.txtEmail)
        Me.gbUserDetails.Controls.Add(Me.Label3)
        Me.gbUserDetails.Location = New System.Drawing.Point(19, 37)
        Me.gbUserDetails.Name = "gbUserDetails"
        Me.gbUserDetails.Size = New System.Drawing.Size(519, 101)
        Me.gbUserDetails.TabIndex = 23
        Me.gbUserDetails.TabStop = False
        Me.gbUserDetails.Text = "User details"
        '
        'GroupBox1
        '
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
        Me.GroupBox1.Location = New System.Drawing.Point(19, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(557, 166)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "State machine colours"
        '
        'Panel_6
        '
        Me.Panel_6.BackColor = System.Drawing.Color.LightGray
        Me.Panel_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_6.Location = New System.Drawing.Point(509, 92)
        Me.Panel_6.Name = "Panel_6"
        Me.Panel_6.Size = New System.Drawing.Size(38, 28)
        Me.Panel_6.TabIndex = 13
        '
        'Label_6
        '
        Me.Label_6.Location = New System.Drawing.Point(288, 92)
        Me.Label_6.Name = "Label_6"
        Me.Label_6.Size = New System.Drawing.Size(211, 28)
        Me.Label_6.TabIndex = 12
        Me.Label_6.Text = "Label6"
        Me.Label_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_4
        '
        Me.Panel_4.BackColor = System.Drawing.Color.Red
        Me.Panel_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_4.Controls.Add(Me.Panel2)
        Me.Panel_4.Location = New System.Drawing.Point(509, 18)
        Me.Panel_4.Name = "Panel_4"
        Me.Panel_4.Size = New System.Drawing.Size(38, 28)
        Me.Panel_4.TabIndex = 9
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Location = New System.Drawing.Point(113, -2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(38, 27)
        Me.Panel2.TabIndex = 11
        '
        'Label_4
        '
        Me.Label_4.Location = New System.Drawing.Point(288, 18)
        Me.Label_4.Name = "Label_4"
        Me.Label_4.Size = New System.Drawing.Size(211, 28)
        Me.Label_4.TabIndex = 8
        Me.Label_4.Text = "Label4"
        Me.Label_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_1
        '
        Me.Panel_1.BackColor = System.Drawing.Color.Lime
        Me.Panel_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_1.Location = New System.Drawing.Point(240, 55)
        Me.Panel_1.Name = "Panel_1"
        Me.Panel_1.Size = New System.Drawing.Size(38, 28)
        Me.Panel_1.TabIndex = 7
        '
        'Label_1
        '
        Me.Label_1.Location = New System.Drawing.Point(10, 55)
        Me.Label_1.Name = "Label_1"
        Me.Label_1.Size = New System.Drawing.Size(211, 28)
        Me.Label_1.TabIndex = 6
        Me.Label_1.Text = "Label1"
        Me.Label_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_3
        '
        Me.Panel_3.BackColor = System.Drawing.Color.Tomato
        Me.Panel_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_3.Location = New System.Drawing.Point(240, 129)
        Me.Panel_3.Name = "Panel_3"
        Me.Panel_3.Size = New System.Drawing.Size(38, 28)
        Me.Panel_3.TabIndex = 5
        '
        'Label_3
        '
        Me.Label_3.Location = New System.Drawing.Point(10, 129)
        Me.Label_3.Name = "Label_3"
        Me.Label_3.Size = New System.Drawing.Size(211, 28)
        Me.Label_3.TabIndex = 4
        Me.Label_3.Text = "Label3"
        Me.Label_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_2
        '
        Me.Panel_2.BackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(192, Byte), CType(192, Byte))
        Me.Panel_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_2.Location = New System.Drawing.Point(240, 92)
        Me.Panel_2.Name = "Panel_2"
        Me.Panel_2.Size = New System.Drawing.Size(38, 28)
        Me.Panel_2.TabIndex = 3
        '
        'Label_2
        '
        Me.Label_2.Location = New System.Drawing.Point(10, 92)
        Me.Label_2.Name = "Label_2"
        Me.Label_2.Size = New System.Drawing.Size(211, 28)
        Me.Label_2.TabIndex = 2
        Me.Label_2.Text = "Label2"
        Me.Label_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_0
        '
        Me.Panel_0.BackColor = System.Drawing.Color.Yellow
        Me.Panel_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_0.Location = New System.Drawing.Point(240, 18)
        Me.Panel_0.Name = "Panel_0"
        Me.Panel_0.Size = New System.Drawing.Size(38, 28)
        Me.Panel_0.TabIndex = 1
        '
        'Label_0
        '
        Me.Label_0.Location = New System.Drawing.Point(10, 18)
        Me.Label_0.Name = "Label_0"
        Me.Label_0.Size = New System.Drawing.Size(211, 28)
        Me.Label_0.TabIndex = 0
        Me.Label_0.Text = "Label0"
        Me.Label_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_5
        '
        Me.Panel_5.BackColor = System.Drawing.Color.Silver
        Me.Panel_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_5.Location = New System.Drawing.Point(509, 55)
        Me.Panel_5.Name = "Panel_5"
        Me.Panel_5.Size = New System.Drawing.Size(38, 28)
        Me.Panel_5.TabIndex = 11
        '
        'Label_5
        '
        Me.Label_5.Location = New System.Drawing.Point(288, 55)
        Me.Label_5.Name = "Label_5"
        Me.Label_5.Size = New System.Drawing.Size(211, 28)
        Me.Label_5.TabIndex = 10
        Me.Label_5.Text = "Label5"
        Me.Label_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabConfiguration
        '
        Me.TabConfiguration.Controls.Add(Me.tpUser)
        Me.TabConfiguration.Controls.Add(Me.tpLocations)
        Me.TabConfiguration.Controls.Add(Me.tpAppearance)
        Me.TabConfiguration.Controls.Add(Me.tpDefaults)
        Me.TabConfiguration.Dock = System.Windows.Forms.DockStyle.Top
        Me.TabConfiguration.Location = New System.Drawing.Point(0, 0)
        Me.TabConfiguration.Name = "TabConfiguration"
        Me.TabConfiguration.SelectedIndex = 0
        Me.TabConfiguration.Size = New System.Drawing.Size(643, 222)
        Me.TabConfiguration.TabIndex = 25
        '
        'tpUser
        '
        Me.tpUser.Controls.Add(Me.gbUserDetails)
        Me.tpUser.Location = New System.Drawing.Point(4, 25)
        Me.tpUser.Name = "tpUser"
        Me.tpUser.Size = New System.Drawing.Size(635, 193)
        Me.tpUser.TabIndex = 0
        Me.tpUser.Text = "User details"
        '
        'tpLocations
        '
        Me.tpLocations.Controls.Add(Me.Label4)
        Me.tpLocations.Controls.Add(Me.txtHelpFile)
        Me.tpLocations.Controls.Add(Me.butHelpBrowse)
        Me.tpLocations.Controls.Add(Me.lblArchetypePath)
        Me.tpLocations.Controls.Add(Me.txtRepositoryPath)
        Me.tpLocations.Controls.Add(Me.butBrowse)
        Me.tpLocations.Location = New System.Drawing.Point(4, 25)
        Me.tpLocations.Name = "tpLocations"
        Me.tpLocations.Size = New System.Drawing.Size(635, 193)
        Me.tpLocations.TabIndex = 1
        Me.tpLocations.Text = "File locations"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(38, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(173, 28)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Help file"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'butHelpBrowse
        '
        Me.butHelpBrowse.Image = CType(resources.GetObject("butHelpBrowse.Image"), System.Drawing.Image)
        Me.butHelpBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.butHelpBrowse.Location = New System.Drawing.Point(499, 111)
        Me.butHelpBrowse.Name = "butHelpBrowse"
        Me.butHelpBrowse.Size = New System.Drawing.Size(115, 37)
        Me.butHelpBrowse.TabIndex = 23
        Me.butHelpBrowse.Text = "Browse..."
        Me.butHelpBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tpAppearance
        '
        Me.tpAppearance.Controls.Add(Me.GroupBox1)
        Me.tpAppearance.Location = New System.Drawing.Point(4, 25)
        Me.tpAppearance.Name = "tpAppearance"
        Me.tpAppearance.Size = New System.Drawing.Size(635, 193)
        Me.tpAppearance.TabIndex = 2
        Me.tpAppearance.Text = "Appearance"
        '
        'tpDefaults
        '
        Me.tpDefaults.Controls.Add(Me.comboReferenceModel)
        Me.tpDefaults.Controls.Add(Me.Label1)
        Me.tpDefaults.Location = New System.Drawing.Point(4, 25)
        Me.tpDefaults.Name = "tpDefaults"
        Me.tpDefaults.Size = New System.Drawing.Size(635, 193)
        Me.tpDefaults.TabIndex = 3
        Me.tpDefaults.Text = "Defaults"
        '
        'ApplicationOptionsForm
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(643, 274)
        Me.Controls.Add(Me.TabConfiguration)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.butCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ApplicationOptionsForm"
        Me.ShowInTaskbar = False
        Me.Text = "ApplicationOptionsForm"
        Me.gbUserDetails.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel_4.ResumeLayout(False)
        Me.TabConfiguration.ResumeLayout(False)
        Me.tpUser.ResumeLayout(False)
        Me.tpLocations.ResumeLayout(False)
        Me.tpAppearance.ResumeLayout(False)
        Me.tpDefaults.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub butBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butBrowse.Click
        
        Me.FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            Me.txtRepositoryPath.Text = FolderBrowserDialog1.SelectedPath
        End If

    End Sub

    Private Sub ApplicationOptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ms As StateMachineType()

        ms = ReferenceModel.Instance.ValidStateMachineTypes
        Debug.Assert(ms.Length = 7)
        Me.Label_0.Text = ms(0).ToString
        Me.Label_1.Text = ms(1).ToString
        Me.Label_2.Text = ms(2).ToString
        Me.Label_3.Text = ms(3).ToString
        Me.Label_4.Text = ms(4).ToString
        Me.Label_4.Text = ms(5).ToString
        Me.Label_4.Text = ms(6).ToString
    End Sub

    Private Sub ColourPanel_doubleClisk(ByVal sender As System.Object, ByVal e As EventArgs) Handles Panel_4.DoubleClick, Panel_0.DoubleClick, Panel_2.DoubleClick, Panel_1.DoubleClick, Panel_3.DoubleClick
        Dim p As Panel

        p = CType(sender, Panel)
        p.BorderStyle = BorderStyle.FixedSingle
        Me.ColorDialog1.FullOpen = True
        Me.ColorDialog1.AnyColor = True
        Me.ColorDialog1.Color = p.BackColor
        If Me.ColorDialog1.ShowDialog = DialogResult.OK Then
            p.BackColor = ColorDialog1.Color
        End If
        p.BorderStyle = BorderStyle.Fixed3D

    End Sub

    Private Sub butHelpBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butHelpBrowse.Click
        Me.OpenFileDialog1.InitialDirectory = Application.StartupPath & "\Help"
        Me.OpenFileDialog1.Filter = "Windows help (chm)|*.chm|HTML|*.htm, *.html"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Me.txtHelpFile.Text = OpenFileDialog1.FileName
        End If

    End Sub
End Class

