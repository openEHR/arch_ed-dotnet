<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChooseType
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
        Me.listType = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'listType
        '
        Me.listType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listType.FormattingEnabled = True
        Me.listType.ItemHeight = 18
        Me.listType.Location = New System.Drawing.Point(0, 0)
        Me.listType.Name = "listType"
        Me.listType.Size = New System.Drawing.Size(204, 40)
        Me.listType.TabIndex = 0
        '
        'ChooseType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(204, 40)
        Me.Controls.Add(Me.listType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChooseType"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Choose"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents listType As System.Windows.Forms.ListBox
End Class
