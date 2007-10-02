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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabPageParticipation))
        Me.chkProvider = New System.Windows.Forms.CheckBox
        Me.panelProvider = New System.Windows.Forms.Panel
        Me.gbParticipations = New System.Windows.Forms.GroupBox
        Me.flowPanelParticipations = New System.Windows.Forms.FlowLayoutPanel
        Me.panelButtons = New System.Windows.Forms.Panel
        Me.butAddEvent = New System.Windows.Forms.Button
        Me.butRemoveElement = New System.Windows.Forms.Button
        Me.panelOccurrences = New System.Windows.Forms.Panel
        Me.panelProvider.SuspendLayout()
        Me.gbParticipations.SuspendLayout()
        Me.panelButtons.SuspendLayout()
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
        'gbParticipations
        '
        Me.gbParticipations.Controls.Add(Me.flowPanelParticipations)
        Me.gbParticipations.Controls.Add(Me.panelOccurrences)
        Me.gbParticipations.Controls.Add(Me.panelButtons)
        Me.gbParticipations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbParticipations.Location = New System.Drawing.Point(0, 28)
        Me.gbParticipations.Name = "gbParticipations"
        Me.gbParticipations.Size = New System.Drawing.Size(602, 307)
        Me.gbParticipations.TabIndex = 3
        Me.gbParticipations.TabStop = False
        Me.gbParticipations.Text = "Other Participations"
        '
        'flowPanelParticipations
        '
        Me.flowPanelParticipations.AutoScroll = True
        Me.flowPanelParticipations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowPanelParticipations.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flowPanelParticipations.Location = New System.Drawing.Point(33, 73)
        Me.flowPanelParticipations.Name = "flowPanelParticipations"
        Me.flowPanelParticipations.Size = New System.Drawing.Size(566, 231)
        Me.flowPanelParticipations.TabIndex = 0
        Me.flowPanelParticipations.WrapContents = False
        '
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.butAddEvent)
        Me.panelButtons.Controls.Add(Me.butRemoveElement)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelButtons.Location = New System.Drawing.Point(3, 18)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(30, 286)
        Me.panelButtons.TabIndex = 1
        '
        'butAddEvent
        '
        Me.butAddEvent.Image = CType(resources.GetObject("butAddEvent.Image"), System.Drawing.Image)
        Me.butAddEvent.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddEvent.Location = New System.Drawing.Point(3, 5)
        Me.butAddEvent.Name = "butAddEvent"
        Me.butAddEvent.Size = New System.Drawing.Size(24, 24)
        Me.butAddEvent.TabIndex = 25
        '
        'butRemoveElement
        '
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(3, 31)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveElement.TabIndex = 26
        '
        'panelOccurrences
        '
        Me.panelOccurrences.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelOccurrences.Location = New System.Drawing.Point(33, 18)
        Me.panelOccurrences.Name = "panelOccurrences"
        Me.panelOccurrences.Size = New System.Drawing.Size(566, 55)
        Me.panelOccurrences.TabIndex = 2
        Me.panelOccurrences.Visible = False
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
        Me.gbParticipations.ResumeLayout(False)
        Me.panelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkProvider As System.Windows.Forms.CheckBox
    Friend WithEvents panelProvider As System.Windows.Forms.Panel
    Friend WithEvents gbParticipations As System.Windows.Forms.GroupBox
    Friend WithEvents flowPanelParticipations As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents panelButtons As System.Windows.Forms.Panel
    Friend WithEvents butAddEvent As System.Windows.Forms.Button
    Friend WithEvents butRemoveElement As System.Windows.Forms.Button
    Friend WithEvents panelOccurrences As System.Windows.Forms.Panel

End Class
