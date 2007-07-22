<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TabPageParticipation
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
        Me.chkProvider = New System.Windows.Forms.CheckBox
        Me.panelProvider = New System.Windows.Forms.Panel
        Me.dgOtherParticipations = New System.Windows.Forms.DataGridView
        Me.gbParticipations = New System.Windows.Forms.GroupBox
        Me.pFunction = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pTimeInterval = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.pMode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.panelProvider.SuspendLayout()
        CType(Me.dgOtherParticipations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbParticipations.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkProvider
        '
        Me.chkProvider.AutoSize = True
        Me.chkProvider.Location = New System.Drawing.Point(37, 5)
        Me.chkProvider.Name = "chkProvider"
        Me.chkProvider.Size = New System.Drawing.Size(289, 21)
        Me.chkProvider.TabIndex = 0
        Me.chkProvider.Text = "Mandatory to record Information Provider"
        Me.chkProvider.UseVisualStyleBackColor = True
        '
        'panelProvider
        '
        Me.panelProvider.Controls.Add(Me.chkProvider)
        Me.panelProvider.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelProvider.Location = New System.Drawing.Point(0, 0)
        Me.panelProvider.Name = "panelProvider"
        Me.panelProvider.Size = New System.Drawing.Size(602, 28)
        Me.panelProvider.TabIndex = 1
        '
        'dgOtherParticipations
        '
        Me.dgOtherParticipations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOtherParticipations.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.pFunction, Me.pTimeInterval, Me.pMode})
        Me.dgOtherParticipations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOtherParticipations.Location = New System.Drawing.Point(3, 18)
        Me.dgOtherParticipations.Name = "dgOtherParticipations"
        Me.dgOtherParticipations.RowTemplate.Height = 24
        Me.dgOtherParticipations.Size = New System.Drawing.Size(596, 286)
        Me.dgOtherParticipations.TabIndex = 2
        '
        'gbParticipations
        '
        Me.gbParticipations.Controls.Add(Me.dgOtherParticipations)
        Me.gbParticipations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbParticipations.Enabled = False
        Me.gbParticipations.Location = New System.Drawing.Point(0, 28)
        Me.gbParticipations.Name = "gbParticipations"
        Me.gbParticipations.Size = New System.Drawing.Size(602, 307)
        Me.gbParticipations.TabIndex = 3
        Me.gbParticipations.TabStop = False
        Me.gbParticipations.Text = "Other Participations"
        '
        'pFunction
        '
        Me.pFunction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.pFunction.FillWeight = 40.0!
        Me.pFunction.HeaderText = "Function"
        Me.pFunction.Name = "pFunction"
        Me.pFunction.Width = 87
        '
        'pTimeInterval
        '
        Me.pTimeInterval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.pTimeInterval.FillWeight = 20.0!
        Me.pTimeInterval.HeaderText = "Mandate TimeInterval"
        Me.pTimeInterval.Name = "pTimeInterval"
        Me.pTimeInterval.Width = 135
        '
        'pMode
        '
        Me.pMode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.pMode.FillWeight = 20.0!
        Me.pMode.HeaderText = "Mode"
        Me.pMode.Name = "pMode"
        Me.pMode.Width = 68
        '
        'TabPageParticipation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbParticipations)
        Me.Controls.Add(Me.panelProvider)
        Me.Name = "TabPageParticipation"
        Me.Size = New System.Drawing.Size(602, 335)
        Me.panelProvider.ResumeLayout(False)
        Me.panelProvider.PerformLayout()
        CType(Me.dgOtherParticipations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbParticipations.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkProvider As System.Windows.Forms.CheckBox
    Friend WithEvents panelProvider As System.Windows.Forms.Panel
    Friend WithEvents dgOtherParticipations As System.Windows.Forms.DataGridView
    Friend WithEvents gbParticipations As System.Windows.Forms.GroupBox
    Friend WithEvents pFunction As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pTimeInterval As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents pMode As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
