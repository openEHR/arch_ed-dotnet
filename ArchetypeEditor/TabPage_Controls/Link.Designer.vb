<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Link
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.butSelector = New System.Windows.Forms.RadioButton
        Me.gbType = New System.Windows.Forms.GroupBox
        Me.gbTarget = New System.Windows.Forms.GroupBox
        Me.txtTarget = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.gbMeaning = New System.Windows.Forms.GroupBox
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.gbTarget.SuspendLayout()
        Me.PanelTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'butSelector
        '
        Me.butSelector.Appearance = System.Windows.Forms.Appearance.Button
        Me.butSelector.AutoSize = True
        Me.butSelector.Dock = System.Windows.Forms.DockStyle.Left
        Me.butSelector.FlatAppearance.CheckedBackColor = System.Drawing.Color.Blue
        Me.butSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.butSelector.Location = New System.Drawing.Point(0, 0)
        Me.butSelector.Name = "butSelector"
        Me.butSelector.Size = New System.Drawing.Size(28, 311)
        Me.butSelector.TabIndex = 1
        Me.butSelector.TabStop = True
        Me.butSelector.Text = ">"
        Me.butSelector.UseVisualStyleBackColor = True
        '
        'gbType
        '
        Me.gbType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbType.Location = New System.Drawing.Point(391, 46)
        Me.gbType.Name = "gbType"
        Me.gbType.Size = New System.Drawing.Size(401, 265)
        Me.gbType.TabIndex = 2
        Me.gbType.TabStop = False
        Me.gbType.Text = "Link Type"
        '
        'gbTarget
        '
        Me.gbTarget.Controls.Add(Me.txtTarget)
        Me.gbTarget.Controls.Add(Me.TextBox1)
        Me.gbTarget.Dock = System.Windows.Forms.DockStyle.Right
        Me.gbTarget.Location = New System.Drawing.Point(363, 0)
        Me.gbTarget.Name = "gbTarget"
        Me.gbTarget.Size = New System.Drawing.Size(401, 46)
        Me.gbTarget.TabIndex = 3
        Me.gbTarget.TabStop = False
        Me.gbTarget.Text = "Target"
        '
        'txtTarget
        '
        Me.txtTarget.Location = New System.Drawing.Point(22, 20)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(374, 22)
        Me.txtTarget.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 151)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(171, 49)
        Me.TextBox1.TabIndex = 0
        '
        'gbMeaning
        '
        Me.gbMeaning.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbMeaning.Location = New System.Drawing.Point(28, 46)
        Me.gbMeaning.Name = "gbMeaning"
        Me.gbMeaning.Size = New System.Drawing.Size(363, 265)
        Me.gbMeaning.TabIndex = 4
        Me.gbMeaning.TabStop = False
        Me.gbMeaning.Text = "Meaning"
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.gbTarget)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(28, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(764, 46)
        Me.PanelTop.TabIndex = 5
        '
        'Link
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.gbType)
        Me.Controls.Add(Me.gbMeaning)
        Me.Controls.Add(Me.PanelTop)
        Me.Controls.Add(Me.butSelector)
        Me.Name = "Link"
        Me.Size = New System.Drawing.Size(792, 311)
        Me.gbTarget.ResumeLayout(False)
        Me.gbTarget.PerformLayout()
        Me.PanelTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butSelector As System.Windows.Forms.RadioButton
    Friend WithEvents gbType As System.Windows.Forms.GroupBox
    Friend WithEvents gbTarget As System.Windows.Forms.GroupBox
    Friend WithEvents gbMeaning As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTarget As System.Windows.Forms.TextBox
    Friend WithEvents PanelTop As System.Windows.Forms.Panel

End Class
