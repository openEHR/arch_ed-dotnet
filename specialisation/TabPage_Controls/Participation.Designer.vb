<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Participation
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
        Me.panelLeft = New System.Windows.Forms.Panel
        Me.butSelector = New System.Windows.Forms.RadioButton
        Me.gbFunction = New System.Windows.Forms.GroupBox
        Me.gbConstraints = New System.Windows.Forms.GroupBox
        Me.cbDateTime = New System.Windows.Forms.CheckBox
        Me.lblDateTime = New System.Windows.Forms.Label
        Me.gbConstraints.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelLeft
        '
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelLeft.Location = New System.Drawing.Point(28, 0)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Size = New System.Drawing.Size(343, 206)
        Me.panelLeft.TabIndex = 0
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
        Me.butSelector.Size = New System.Drawing.Size(28, 206)
        Me.butSelector.TabIndex = 1
        Me.butSelector.TabStop = True
        Me.butSelector.Text = ">"
        Me.butSelector.UseVisualStyleBackColor = True
        '
        'gbFunction
        '
        Me.gbFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFunction.Location = New System.Drawing.Point(371, 0)
        Me.gbFunction.Name = "gbFunction"
        Me.gbFunction.Size = New System.Drawing.Size(377, 206)
        Me.gbFunction.TabIndex = 2
        Me.gbFunction.TabStop = False
        Me.gbFunction.Text = "Participant function"
        '
        'gbConstraints
        '
        Me.gbConstraints.Controls.Add(Me.lblDateTime)
        Me.gbConstraints.Controls.Add(Me.cbDateTime)
        Me.gbConstraints.Dock = System.Windows.Forms.DockStyle.Right
        Me.gbConstraints.Location = New System.Drawing.Point(748, 0)
        Me.gbConstraints.Name = "gbConstraints"
        Me.gbConstraints.Size = New System.Drawing.Size(124, 206)
        Me.gbConstraints.TabIndex = 3
        Me.gbConstraints.TabStop = False
        Me.gbConstraints.Text = "Constraints"
        '
        'cbDateTime
        '
        Me.cbDateTime.Location = New System.Drawing.Point(9, 50)
        Me.cbDateTime.Name = "cbDateTime"
        Me.cbDateTime.Size = New System.Drawing.Size(112, 49)
        Me.cbDateTime.TabIndex = 0
        Me.cbDateTime.Text = "mandatory"
        Me.cbDateTime.UseVisualStyleBackColor = True
        '
        'lblDateTime
        '
        Me.lblDateTime.AutoSize = True
        Me.lblDateTime.Location = New System.Drawing.Point(7, 24)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(96, 17)
        Me.lblDateTime.TabIndex = 1
        Me.lblDateTime.Text = "Date and time"
        '
        'Participation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.gbFunction)
        Me.Controls.Add(Me.gbConstraints)
        Me.Controls.Add(Me.panelLeft)
        Me.Controls.Add(Me.butSelector)
        Me.Name = "Participation"
        Me.Size = New System.Drawing.Size(872, 206)
        Me.gbConstraints.ResumeLayout(False)
        Me.gbConstraints.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents panelLeft As System.Windows.Forms.Panel
    Friend WithEvents butSelector As System.Windows.Forms.RadioButton
    Friend WithEvents gbFunction As System.Windows.Forms.GroupBox
    Friend WithEvents gbConstraints As System.Windows.Forms.GroupBox
    Friend WithEvents cbDateTime As System.Windows.Forms.CheckBox
    Friend WithEvents lblDateTime As System.Windows.Forms.Label

End Class
