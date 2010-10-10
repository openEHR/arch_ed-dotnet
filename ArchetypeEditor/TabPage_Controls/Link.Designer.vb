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
        Me.splitLinkMeaning = New System.Windows.Forms.SplitContainer
        Me.gbTarget.SuspendLayout()
        Me.PanelTop.SuspendLayout()
        Me.splitLinkMeaning.Panel1.SuspendLayout()
        Me.splitLinkMeaning.Panel2.SuspendLayout()
        Me.splitLinkMeaning.SuspendLayout()
        Me.SuspendLayout()
        '
        'butSelector
        '
        Me.butSelector.Appearance = System.Windows.Forms.Appearance.Button
        Me.butSelector.AutoSize = True
        Me.butSelector.Dock = System.Windows.Forms.DockStyle.Left
        Me.butSelector.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.butSelector.FlatAppearance.BorderSize = 0
        Me.butSelector.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark
        Me.butSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.butSelector.Location = New System.Drawing.Point(0, 0)
        Me.butSelector.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butSelector.Name = "butSelector"
        Me.butSelector.Size = New System.Drawing.Size(23, 380)
        Me.butSelector.TabIndex = 1
        Me.butSelector.TabStop = True
        Me.butSelector.Text = ">"
        Me.butSelector.UseVisualStyleBackColor = True
        '
        'gbType
        '
        Me.gbType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbType.Location = New System.Drawing.Point(0, 0)
        Me.gbType.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.gbType.Name = "gbType"
        Me.gbType.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.gbType.Size = New System.Drawing.Size(287, 343)
        Me.gbType.TabIndex = 2
        Me.gbType.TabStop = False
        Me.gbType.Text = "Link Type"
        '
        'gbTarget
        '
        Me.gbTarget.Controls.Add(Me.txtTarget)
        Me.gbTarget.Controls.Add(Me.TextBox1)
        Me.gbTarget.Dock = System.Windows.Forms.DockStyle.Right
        Me.gbTarget.Location = New System.Drawing.Point(282, 0)
        Me.gbTarget.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.gbTarget.Name = "gbTarget"
        Me.gbTarget.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.gbTarget.Size = New System.Drawing.Size(289, 37)
        Me.gbTarget.TabIndex = 3
        Me.gbTarget.TabStop = False
        Me.gbTarget.Text = "Target"
        '
        'txtTarget
        '
        Me.txtTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTarget.Location = New System.Drawing.Point(16, 15)
        Me.txtTarget.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(270, 20)
        Me.txtTarget.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(4, 123)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(129, 41)
        Me.TextBox1.TabIndex = 0
        '
        'gbMeaning
        '
        Me.gbMeaning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMeaning.Location = New System.Drawing.Point(0, 0)
        Me.gbMeaning.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.gbMeaning.Name = "gbMeaning"
        Me.gbMeaning.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.gbMeaning.Size = New System.Drawing.Size(281, 343)
        Me.gbMeaning.TabIndex = 4
        Me.gbMeaning.TabStop = False
        Me.gbMeaning.Text = "Meaning"
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.gbTarget)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(23, 0)
        Me.PanelTop.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(571, 37)
        Me.PanelTop.TabIndex = 5
        '
        'splitLinkMeaning
        '
        Me.splitLinkMeaning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitLinkMeaning.Location = New System.Drawing.Point(23, 37)
        Me.splitLinkMeaning.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.splitLinkMeaning.Name = "splitLinkMeaning"
        '
        'splitLinkMeaning.Panel1
        '
        Me.splitLinkMeaning.Panel1.Controls.Add(Me.gbMeaning)
        '
        'splitLinkMeaning.Panel2
        '
        Me.splitLinkMeaning.Panel2.Controls.Add(Me.gbType)
        Me.splitLinkMeaning.Size = New System.Drawing.Size(571, 343)
        Me.splitLinkMeaning.SplitterDistance = 281
        Me.splitLinkMeaning.SplitterWidth = 3
        Me.splitLinkMeaning.TabIndex = 6
        '
        'Link
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.splitLinkMeaning)
        Me.Controls.Add(Me.PanelTop)
        Me.Controls.Add(Me.butSelector)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Link"
        Me.Size = New System.Drawing.Size(594, 380)
        Me.gbTarget.ResumeLayout(False)
        Me.gbTarget.PerformLayout()
        Me.PanelTop.ResumeLayout(False)
        Me.splitLinkMeaning.Panel1.ResumeLayout(False)
        Me.splitLinkMeaning.Panel2.ResumeLayout(False)
        Me.splitLinkMeaning.ResumeLayout(False)
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
    Friend WithEvents splitLinkMeaning As System.Windows.Forms.SplitContainer

End Class
