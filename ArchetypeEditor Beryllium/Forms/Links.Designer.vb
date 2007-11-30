<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Links
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Links))
        Me.PanelLeft = New System.Windows.Forms.Panel
        Me.butAddEvent = New System.Windows.Forms.Button
        Me.butRemoveElement = New System.Windows.Forms.Button
        Me.PanelBottom = New System.Windows.Forms.Panel
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.panelLinks = New System.Windows.Forms.Panel
        Me.PanelLeft.SuspendLayout()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelLeft
        '
        Me.PanelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelLeft.Controls.Add(Me.butAddEvent)
        Me.PanelLeft.Controls.Add(Me.butRemoveElement)
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelLeft.Location = New System.Drawing.Point(0, 0)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(35, 656)
        Me.PanelLeft.TabIndex = 0
        '
        'butAddEvent
        '
        Me.butAddEvent.Image = CType(resources.GetObject("butAddEvent.Image"), System.Drawing.Image)
        Me.butAddEvent.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddEvent.Location = New System.Drawing.Point(5, 6)
        Me.butAddEvent.Name = "butAddEvent"
        Me.butAddEvent.Size = New System.Drawing.Size(24, 24)
        Me.butAddEvent.TabIndex = 27
        '
        'butRemoveElement
        '
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(5, 32)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveElement.TabIndex = 28
        '
        'PanelBottom
        '
        Me.PanelBottom.Controls.Add(Me.butCancel)
        Me.PanelBottom.Controls.Add(Me.butOK)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(35, 623)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(807, 33)
        Me.PanelBottom.TabIndex = 2
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(727, 2)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(75, 30)
        Me.butCancel.TabIndex = 1
        Me.butCancel.Text = "Cancel"
        Me.butCancel.UseVisualStyleBackColor = True
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(648, 2)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(75, 30)
        Me.butOK.TabIndex = 0
        Me.butOK.Text = "OK"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'panelLinks
        '
        Me.panelLinks.AutoScroll = True
        Me.panelLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelLinks.Location = New System.Drawing.Point(35, 0)
        Me.panelLinks.Name = "panelLinks"
        Me.panelLinks.Size = New System.Drawing.Size(807, 623)
        Me.panelLinks.TabIndex = 3
        '
        'Links
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(842, 656)
        Me.Controls.Add(Me.panelLinks)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelLeft)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Links"
        Me.Text = "Links"
        Me.PanelLeft.ResumeLayout(False)
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents butAddEvent As System.Windows.Forms.Button
    Friend WithEvents butRemoveElement As System.Windows.Forms.Button
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents panelLinks As System.Windows.Forms.Panel
End Class
