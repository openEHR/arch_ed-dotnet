<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpecialisationQuestionDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SpecialisationQuestionDialog))
        Me.YesButton = New System.Windows.Forms.Button
        Me.NoButton = New System.Windows.Forms.Button
        Me.QuestionLabel = New System.Windows.Forms.Label
        Me.YesAndCloneButton = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'YesButton
        '
        Me.YesButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.YesButton.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.YesButton.Location = New System.Drawing.Point(15, 64)
        Me.YesButton.Name = "YesButton"
        Me.YesButton.Size = New System.Drawing.Size(465, 23)
        Me.YesButton.TabIndex = 1
        Me.YesButton.Text = "Yes"
        '
        'NoButton
        '
        Me.NoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NoButton.DialogResult = System.Windows.Forms.DialogResult.No
        Me.NoButton.Location = New System.Drawing.Point(330, 93)
        Me.NoButton.Name = "NoButton"
        Me.NoButton.Size = New System.Drawing.Size(150, 23)
        Me.NoButton.TabIndex = 3
        Me.NoButton.Text = "No"
        '
        'QuestionLabel
        '
        Me.QuestionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QuestionLabel.Location = New System.Drawing.Point(53, 9)
        Me.QuestionLabel.Name = "QuestionLabel"
        Me.QuestionLabel.Size = New System.Drawing.Size(427, 52)
        Me.QuestionLabel.TabIndex = 0
        Me.QuestionLabel.Text = "Specialise?"
        '
        'YesAndCloneButton
        '
        Me.YesAndCloneButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.YesAndCloneButton.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.YesAndCloneButton.Location = New System.Drawing.Point(15, 93)
        Me.YesAndCloneButton.Name = "YesAndCloneButton"
        Me.YesAndCloneButton.Size = New System.Drawing.Size(304, 23)
        Me.YesAndCloneButton.TabIndex = 2
        Me.YesAndCloneButton.Text = "Yes and Clone"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(15, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'SpecialisationQuestionDialog
        '
        Me.AcceptButton = Me.YesButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.NoButton
        Me.ClientSize = New System.Drawing.Size(492, 128)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.YesAndCloneButton)
        Me.Controls.Add(Me.YesButton)
        Me.Controls.Add(Me.NoButton)
        Me.Controls.Add(Me.QuestionLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SpecialisationQuestionDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Specialise"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents YesButton As System.Windows.Forms.Button
    Friend WithEvents NoButton As System.Windows.Forms.Button
    Friend WithEvents QuestionLabel As System.Windows.Forms.Label
    Friend WithEvents YesAndCloneButton As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
