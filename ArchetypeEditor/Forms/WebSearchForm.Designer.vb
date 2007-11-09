<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WebSearchForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebSearchForm))
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblNum = New System.Windows.Forms.Label
        Me.img_globe = New System.Windows.Forms.PictureBox
        Me.gbSearch = New System.Windows.Forms.GroupBox
        Me.comboSearch = New System.Windows.Forms.ComboBox
        Me.listViewArchetypes = New System.Windows.Forms.ListView
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.PanelBottom = New System.Windows.Forms.Panel
        Me.ImageListArchetypes = New System.Windows.Forms.ImageList(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        CType(Me.img_globe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSearch.SuspendLayout()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTerm
        '
        Me.txtTerm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerm.Location = New System.Drawing.Point(90, 21)
        Me.txtTerm.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(367, 24)
        Me.txtTerm.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(463, 17)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(42, 32)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblNum
        '
        Me.lblNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNum.AutoSize = True
        Me.lblNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNum.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblNum.Location = New System.Drawing.Point(502, 63)
        Me.lblNum.Margin = New System.Windows.Forms.Padding(4)
        Me.lblNum.Name = "lblNum"
        Me.lblNum.Size = New System.Drawing.Size(16, 18)
        Me.lblNum.TabIndex = 7
        Me.lblNum.Text = "0"
        Me.lblNum.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblNum.Visible = False
        '
        'img_globe
        '
        Me.img_globe.BackColor = System.Drawing.Color.LightSteelBlue
        Me.img_globe.Image = CType(resources.GetObject("img_globe.Image"), System.Drawing.Image)
        Me.img_globe.InitialImage = Nothing
        Me.img_globe.Location = New System.Drawing.Point(7, 21)
        Me.img_globe.Margin = New System.Windows.Forms.Padding(4)
        Me.img_globe.Name = "img_globe"
        Me.img_globe.Size = New System.Drawing.Size(79, 71)
        Me.img_globe.TabIndex = 10
        Me.img_globe.TabStop = False
        '
        'gbSearch
        '
        Me.gbSearch.Controls.Add(Me.comboSearch)
        Me.gbSearch.Controls.Add(Me.img_globe)
        Me.gbSearch.Controls.Add(Me.txtTerm)
        Me.gbSearch.Controls.Add(Me.btnSearch)
        Me.gbSearch.Controls.Add(Me.lblNum)
        Me.gbSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbSearch.Location = New System.Drawing.Point(3, 3)
        Me.gbSearch.Name = "gbSearch"
        Me.gbSearch.Size = New System.Drawing.Size(530, 90)
        Me.gbSearch.TabIndex = 12
        Me.gbSearch.TabStop = False
        Me.gbSearch.Text = "Search"
        '
        'comboSearch
        '
        Me.comboSearch.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.comboSearch.FormattingEnabled = True
        Me.comboSearch.Items.AddRange(New Object() {"All", "Archetype ID", "Concept", "Description"})
        Me.comboSearch.Location = New System.Drawing.Point(94, 53)
        Me.comboSearch.Name = "comboSearch"
        Me.comboSearch.Size = New System.Drawing.Size(174, 24)
        Me.comboSearch.TabIndex = 3
        Me.comboSearch.Text = "All"
        '
        'listViewArchetypes
        '
        Me.listViewArchetypes.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.listViewArchetypes.AutoArrange = False
        Me.listViewArchetypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewArchetypes.FullRowSelect = True
        Me.listViewArchetypes.HideSelection = False
        Me.listViewArchetypes.Location = New System.Drawing.Point(3, 93)
        Me.listViewArchetypes.MultiSelect = False
        Me.listViewArchetypes.Name = "listViewArchetypes"
        Me.listViewArchetypes.Size = New System.Drawing.Size(530, 256)
        Me.listViewArchetypes.SmallImageList = Me.ImageListArchetypes
        Me.listViewArchetypes.TabIndex = 4
        Me.listViewArchetypes.UseCompatibleStateImageBehavior = False
        Me.listViewArchetypes.View = System.Windows.Forms.View.List
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.Location = New System.Drawing.Point(353, 6)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(83, 28)
        Me.butOK.TabIndex = 5
        Me.butOK.Text = "OK"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(442, 6)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(83, 28)
        Me.butCancel.TabIndex = 6
        Me.butCancel.Text = "Cancel"
        Me.butCancel.UseVisualStyleBackColor = True
        '
        'PanelBottom
        '
        Me.PanelBottom.Controls.Add(Me.ProgressBar1)
        Me.PanelBottom.Controls.Add(Me.butOK)
        Me.PanelBottom.Controls.Add(Me.butCancel)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(3, 349)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(530, 40)
        Me.PanelBottom.TabIndex = 16
        '
        'ImageListArchetypes
        '
        Me.ImageListArchetypes.ImageStream = CType(resources.GetObject("ImageListArchetypes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListArchetypes.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListArchetypes.Images.SetKeyName(0, "structure.png")
        Me.ImageListArchetypes.Images.SetKeyName(1, "action.png")
        Me.ImageListArchetypes.Images.SetKeyName(2, "evaluation.png")
        Me.ImageListArchetypes.Images.SetKeyName(3, "instruction.png")
        Me.ImageListArchetypes.Images.SetKeyName(4, "observation.png")
        Me.ImageListArchetypes.Images.SetKeyName(5, "Cluster.png")
        Me.ImageListArchetypes.Images.SetKeyName(6, "composition.png")
        Me.ImageListArchetypes.Images.SetKeyName(7, "section.gif")
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.ProgressBar1.Location = New System.Drawing.Point(13, 12)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(265, 17)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 7
        Me.ProgressBar1.Visible = False
        '
        'WebSearchForm
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(536, 392)
        Me.ControlBox = False
        Me.Controls.Add(Me.listViewArchetypes)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.gbSearch)
        Me.Location = New System.Drawing.Point(150, 100)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "WebSearchForm"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Open from Web"
        CType(Me.img_globe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSearch.ResumeLayout(False)
        Me.gbSearch.PerformLayout()
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblNum As System.Windows.Forms.Label
    Friend WithEvents img_globe As System.Windows.Forms.PictureBox
    Friend WithEvents gbSearch As System.Windows.Forms.GroupBox
    Friend WithEvents listViewArchetypes As System.Windows.Forms.ListView
    Friend WithEvents comboSearch As System.Windows.Forms.ComboBox
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents ImageListArchetypes As System.Windows.Forms.ImageList
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
