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
    	Me.YesButton = New System.Windows.Forms.Button()
    	Me.NoButton = New System.Windows.Forms.Button()
    	Me.QuestionLabel = New System.Windows.Forms.Label()
    	Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    	Me.YesandCloneByCopyButton = New System.Windows.Forms.Button()
    	CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
    	Me.SuspendLayout
    	'
    	'YesButton
    	'
    	Me.YesButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
    	    	    	Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    	Me.YesButton.DialogResult = System.Windows.Forms.DialogResult.Yes
    	Me.YesButton.Location = New System.Drawing.Point(62, 61)
    	Me.YesButton.Name = "YesButton"
    	Me.YesButton.Size = New System.Drawing.Size(168, 37)
    	Me.YesButton.TabIndex = 1
    	Me.YesButton.Text = "Yes"
    	'
    	'NoButton
    	'
    	Me.NoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    	Me.NoButton.DialogResult = System.Windows.Forms.DialogResult.No
    	Me.NoButton.Location = New System.Drawing.Point(236, 61)
    	Me.NoButton.Name = "NoButton"
    	Me.NoButton.Size = New System.Drawing.Size(179, 37)
    	Me.NoButton.TabIndex = 3
    	Me.NoButton.Text = "No"
    	'
    	'QuestionLabel
    	'
    	Me.QuestionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
    	    	    	Or System.Windows.Forms.AnchorStyles.Left)  _
    	    	    	Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    	Me.QuestionLabel.Location = New System.Drawing.Point(62, 20)
    	Me.QuestionLabel.Name = "QuestionLabel"
    	Me.QuestionLabel.Size = New System.Drawing.Size(353, 21)
    	Me.QuestionLabel.TabIndex = 0
    	Me.QuestionLabel.Text = "Specialise?"
    	'
    	'PictureBox1
    	'
    	Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
    	Me.PictureBox1.Location = New System.Drawing.Point(15, 9)
    	Me.PictureBox1.Name = "PictureBox1"
    	Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
    	Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    	Me.PictureBox1.TabIndex = 4
    	Me.PictureBox1.TabStop = false
    	'
    	'YesandCloneByCopyButton
    	'
    	Me.YesandCloneByCopyButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
    	    	    	Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    	Me.YesandCloneByCopyButton.DialogResult = System.Windows.Forms.DialogResult.Yes
    	Me.YesandCloneByCopyButton.Location = New System.Drawing.Point(62, 104)
    	Me.YesandCloneByCopyButton.Name = "YesandCloneByCopyButton"
    	Me.YesandCloneByCopyButton.Size = New System.Drawing.Size(353, 33)
    	Me.YesandCloneByCopyButton.TabIndex = 5
    	Me.YesandCloneByCopyButton.Text = "Yes and Clone using copies"
    	AddHandler Me.YesandCloneByCopyButton.Click, AddressOf Me.YesandCloneByCopyButtonClick
    	'
    	'SpecialisationQuestionDialog
    	'
    	Me.AcceptButton = Me.YesButton
    	Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
    	Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    	Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    	Me.CancelButton = Me.NoButton
    	Me.ClientSize = New System.Drawing.Size(455, 183)
    	Me.Controls.Add(Me.YesandCloneByCopyButton)
    	Me.Controls.Add(Me.PictureBox1)
    	Me.Controls.Add(Me.YesButton)
    	Me.Controls.Add(Me.NoButton)
    	Me.Controls.Add(Me.QuestionLabel)
    	Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    	Me.MaximizeBox = false
    	Me.MinimizeBox = false
    	Me.Name = "SpecialisationQuestionDialog"
    	Me.ShowInTaskbar = false
    	Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    	Me.Text = "Specialise"
    	CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
    	Me.ResumeLayout(false)
    End Sub
    Friend WithEvents YesandCloneByCopyButton As System.Windows.Forms.Button
    Friend WithEvents YesButton As System.Windows.Forms.Button
    Friend WithEvents NoButton As System.Windows.Forms.Button
    Friend WithEvents QuestionLabel As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

    
    
    
   
End Class
