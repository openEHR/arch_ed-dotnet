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
        Me.PanelLeft.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(27, 533)
        Me.PanelLeft.TabIndex = 0
        '
        'butAddEvent
        '
        Me.butAddEvent.Image = CType(resources.GetObject("butAddEvent.Image"), System.Drawing.Image)
        Me.butAddEvent.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddEvent.Location = New System.Drawing.Point(4, 5)
        Me.butAddEvent.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butAddEvent.Name = "butAddEvent"
        Me.butAddEvent.Size = New System.Drawing.Size(18, 20)
        Me.butAddEvent.TabIndex = 27
        '
        'butRemoveElement
        '
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(4, 26)
        Me.butRemoveElement.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(18, 20)
        Me.butRemoveElement.TabIndex = 28
        '
        'PanelBottom
        '
        Me.PanelBottom.Controls.Add(Me.butCancel)
        Me.PanelBottom.Controls.Add(Me.butOK)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(27, 506)
        Me.PanelBottom.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(605, 27)
        Me.PanelBottom.TabIndex = 2
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(545, 2)
        Me.butCancel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(56, 24)
        Me.butCancel.TabIndex = 1
        Me.butCancel.Text = "Cancel"
        Me.butCancel.UseVisualStyleBackColor = True
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(486, 2)
        Me.butOK.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(56, 24)
        Me.butOK.TabIndex = 0
        Me.butOK.Text = "OK"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'panelLinks
        '
        Me.panelLinks.AutoScroll = True
        Me.panelLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelLinks.Location = New System.Drawing.Point(27, 0)
        Me.panelLinks.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.panelLinks.Name = "panelLinks"
        Me.panelLinks.Size = New System.Drawing.Size(605, 506)
        Me.panelLinks.TabIndex = 3
        '
        'Links
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(632, 533)
        Me.Controls.Add(Me.panelLinks)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelLeft)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
