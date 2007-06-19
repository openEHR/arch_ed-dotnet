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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebSearchForm))
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.rdbtn_any = New System.Windows.Forms.RadioButton
        Me.rdbtn_id = New System.Windows.Forms.RadioButton
        Me.rdbtn_con = New System.Windows.Forms.RadioButton
        Me.rdbtn_des = New System.Windows.Forms.RadioButton
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblNum = New System.Windows.Forms.Label
        Me.btnReset = New System.Windows.Forms.Button
        Me.img_globe = New System.Windows.Forms.PictureBox
        Me.lbl_found = New System.Windows.Forms.Label
        CType(Me.img_globe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblSearch.Location = New System.Drawing.Point(30, 28)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(84, 17)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search for"
        '
        'txtTerm
        '
        Me.txtTerm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerm.Location = New System.Drawing.Point(203, 28)
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(550, 21)
        Me.txtTerm.TabIndex = 1
        '
        'rdbtn_any
        '
        Me.rdbtn_any.AutoSize = True
        Me.rdbtn_any.Checked = True
        Me.rdbtn_any.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_any.Location = New System.Drawing.Point(203, 65)
        Me.rdbtn_any.Name = "rdbtn_any"
        Me.rdbtn_any.Size = New System.Drawing.Size(76, 19)
        Me.rdbtn_any.TabIndex = 2
        Me.rdbtn_any.TabStop = True
        Me.rdbtn_any.Text = "Any Term"
        Me.rdbtn_any.UseVisualStyleBackColor = True
        '
        'rdbtn_id
        '
        Me.rdbtn_id.AutoSize = True
        Me.rdbtn_id.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_id.Location = New System.Drawing.Point(202, 96)
        Me.rdbtn_id.Name = "rdbtn_id"
        Me.rdbtn_id.Size = New System.Drawing.Size(91, 19)
        Me.rdbtn_id.TabIndex = 3
        Me.rdbtn_id.Text = "Archetype Id"
        Me.rdbtn_id.UseVisualStyleBackColor = True
        '
        'rdbtn_con
        '
        Me.rdbtn_con.AutoSize = True
        Me.rdbtn_con.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_con.Location = New System.Drawing.Point(494, 96)
        Me.rdbtn_con.Name = "rdbtn_con"
        Me.rdbtn_con.Size = New System.Drawing.Size(70, 19)
        Me.rdbtn_con.TabIndex = 4
        Me.rdbtn_con.Text = "Concept"
        Me.rdbtn_con.UseVisualStyleBackColor = True
        '
        'rdbtn_des
        '
        Me.rdbtn_des.AutoSize = True
        Me.rdbtn_des.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_des.Location = New System.Drawing.Point(353, 96)
        Me.rdbtn_des.Name = "rdbtn_des"
        Me.rdbtn_des.Size = New System.Drawing.Size(87, 19)
        Me.rdbtn_des.TabIndex = 5
        Me.rdbtn_des.Text = "Description"
        Me.rdbtn_des.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(203, 137)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 26)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblNum
        '
        Me.lblNum.AutoSize = True
        Me.lblNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNum.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblNum.Location = New System.Drawing.Point(30, 192)
        Me.lblNum.Margin = New System.Windows.Forms.Padding(3)
        Me.lblNum.Name = "lblNum"
        Me.lblNum.Size = New System.Drawing.Size(35, 17)
        Me.lblNum.TabIndex = 7
        Me.lblNum.Text = "999"
        Me.lblNum.Visible = False
        '
        'btnReset
        '
        Me.btnReset.AutoSize = True
        Me.btnReset.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(353, 137)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(100, 26)
        Me.btnReset.TabIndex = 9
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'img_globe
        '
        Me.img_globe.BackColor = System.Drawing.Color.LightSteelBlue
        Me.img_globe.Image = CType(resources.GetObject("img_globe.Image"), System.Drawing.Image)
        Me.img_globe.InitialImage = Nothing
        Me.img_globe.Location = New System.Drawing.Point(33, 99)
        Me.img_globe.Name = "img_globe"
        Me.img_globe.Size = New System.Drawing.Size(64, 64)
        Me.img_globe.TabIndex = 10
        Me.img_globe.TabStop = False
        '
        'lbl_found
        '
        Me.lbl_found.AutoSize = True
        Me.lbl_found.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_found.ForeColor = System.Drawing.Color.DarkBlue
        Me.lbl_found.Location = New System.Drawing.Point(63, 192)
        Me.lbl_found.Margin = New System.Windows.Forms.Padding(3)
        Me.lbl_found.Name = "lbl_found"
        Me.lbl_found.Size = New System.Drawing.Size(147, 17)
        Me.lbl_found.TabIndex = 11
        Me.lbl_found.Text = "Archetype(s) found"
        Me.lbl_found.Visible = False
        '
        'WebSearchForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(784, 191)
        Me.Controls.Add(Me.lbl_found)
        Me.Controls.Add(Me.img_globe)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.lblNum)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.rdbtn_des)
        Me.Controls.Add(Me.rdbtn_con)
        Me.Controls.Add(Me.rdbtn_id)
        Me.Controls.Add(Me.rdbtn_any)
        Me.Controls.Add(Me.txtTerm)
        Me.Controls.Add(Me.lblSearch)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(150, 100)
        Me.Name = "WebSearchForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Open from Web"
        CType(Me.img_globe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents rdbtn_any As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_id As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_con As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_des As System.Windows.Forms.RadioButton
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblNum As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents img_globe As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_found As System.Windows.Forms.Label
End Class
