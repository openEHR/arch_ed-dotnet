<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Recovery
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Recovery))
        Me.chkListRecovery = New System.Windows.Forms.CheckedListBox
        Me.panelBottom = New System.Windows.Forms.Panel
        Me.butOK = New System.Windows.Forms.Button
        Me.panelTop = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.panelBottom.SuspendLayout()
        Me.panelTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkListRecovery
        '
        Me.chkListRecovery.CheckOnClick = True
        Me.chkListRecovery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkListRecovery.FormattingEnabled = True
        Me.chkListRecovery.Location = New System.Drawing.Point(0, 45)
        Me.chkListRecovery.Name = "chkListRecovery"
        Me.chkListRecovery.Size = New System.Drawing.Size(407, 174)
        Me.chkListRecovery.TabIndex = 0
        '
        'panelBottom
        '
        Me.panelBottom.Controls.Add(Me.butOK)
        Me.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBottom.Location = New System.Drawing.Point(0, 220)
        Me.panelBottom.Name = "panelBottom"
        Me.panelBottom.Size = New System.Drawing.Size(407, 40)
        Me.panelBottom.TabIndex = 1
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(323, 5)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(72, 24)
        Me.butOK.TabIndex = 0
        Me.butOK.Text = "OK"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'panelTop
        '
        Me.panelTop.Controls.Add(Me.Label1)
        Me.panelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTop.Location = New System.Drawing.Point(0, 0)
        Me.panelTop.Name = "panelTop"
        Me.panelTop.Size = New System.Drawing.Size(407, 45)
        Me.panelTop.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(271, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please check the files you wish to recover"
        '
        'Recovery
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 260)
        Me.Controls.Add(Me.chkListRecovery)
        Me.Controls.Add(Me.panelTop)
        Me.Controls.Add(Me.panelBottom)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Recovery"
        Me.Text = "Recovery"
        Me.panelBottom.ResumeLayout(False)
        Me.panelTop.ResumeLayout(False)
        Me.panelTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkListRecovery As System.Windows.Forms.CheckedListBox
    Friend WithEvents panelBottom As System.Windows.Forms.Panel
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents panelTop As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
