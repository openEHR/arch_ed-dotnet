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
        Me.lblDateTime = New System.Windows.Forms.Label
        Me.cbDateTime = New System.Windows.Forms.CheckBox
        Me.gbConstraints.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelLeft
        '
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelLeft.Location = New System.Drawing.Point(25, 0)
        Me.panelLeft.Margin = New System.Windows.Forms.Padding(2)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Size = New System.Drawing.Size(350, 211)
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
        Me.butSelector.Margin = New System.Windows.Forms.Padding(2)
        Me.butSelector.Name = "butSelector"
        Me.butSelector.Size = New System.Drawing.Size(25, 211)
        Me.butSelector.TabIndex = 1
        Me.butSelector.TabStop = True
        Me.butSelector.Text = ">"
        Me.butSelector.UseVisualStyleBackColor = True
        '
        'gbFunction
        '
        Me.gbFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFunction.Location = New System.Drawing.Point(375, 0)
        Me.gbFunction.Margin = New System.Windows.Forms.Padding(2)
        Me.gbFunction.Name = "gbFunction"
        Me.gbFunction.Padding = New System.Windows.Forms.Padding(2)
        Me.gbFunction.Size = New System.Drawing.Size(389, 211)
        Me.gbFunction.TabIndex = 2
        Me.gbFunction.TabStop = False
        Me.gbFunction.Text = "Participant function"
        '
        'gbConstraints
        '
        Me.gbConstraints.Controls.Add(Me.lblDateTime)
        Me.gbConstraints.Controls.Add(Me.cbDateTime)
        Me.gbConstraints.Dock = System.Windows.Forms.DockStyle.Right
        Me.gbConstraints.Location = New System.Drawing.Point(764, 0)
        Me.gbConstraints.Margin = New System.Windows.Forms.Padding(2)
        Me.gbConstraints.Name = "gbConstraints"
        Me.gbConstraints.Padding = New System.Windows.Forms.Padding(2)
        Me.gbConstraints.Size = New System.Drawing.Size(103, 211)
        Me.gbConstraints.TabIndex = 3
        Me.gbConstraints.TabStop = False
        Me.gbConstraints.Text = "Constraints"
        '
        'lblDateTime
        '
        Me.lblDateTime.AutoSize = True
        Me.lblDateTime.Location = New System.Drawing.Point(5, 20)
        Me.lblDateTime.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(73, 13)
        Me.lblDateTime.TabIndex = 1
        Me.lblDateTime.Text = "Date and time"
        '
        'cbDateTime
        '
        Me.cbDateTime.Location = New System.Drawing.Point(7, 41)
        Me.cbDateTime.Margin = New System.Windows.Forms.Padding(2)
        Me.cbDateTime.Name = "cbDateTime"
        Me.cbDateTime.Size = New System.Drawing.Size(84, 40)
        Me.cbDateTime.TabIndex = 0
        Me.cbDateTime.Text = "Mandatory"
        Me.cbDateTime.UseVisualStyleBackColor = True
        '
        'Participation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.gbFunction)
        Me.Controls.Add(Me.gbConstraints)
        Me.Controls.Add(Me.panelLeft)
        Me.Controls.Add(Me.butSelector)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Participation"
        Me.Size = New System.Drawing.Size(867, 211)
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
