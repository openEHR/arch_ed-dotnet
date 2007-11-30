<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TabPageActivity
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabPageActivity))
        Me.PanelAction = New System.Windows.Forms.Panel
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RenameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.butOpenArchetype = New System.Windows.Forms.Button
        Me.lblNodeId = New System.Windows.Forms.Label
        Me.lblAction = New System.Windows.Forms.Label
        Me.butGetAction = New System.Windows.Forms.Button
        Me.txtAction = New System.Windows.Forms.TextBox
        Me.HelpProviderActivity = New System.Windows.Forms.HelpProvider
        Me.openToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.selectToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PanelAction.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelAction
        '
        Me.PanelAction.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PanelAction.Controls.Add(Me.butOpenArchetype)
        Me.PanelAction.Controls.Add(Me.lblNodeId)
        Me.PanelAction.Controls.Add(Me.lblAction)
        Me.PanelAction.Controls.Add(Me.butGetAction)
        Me.PanelAction.Controls.Add(Me.txtAction)
        Me.PanelAction.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAction.Location = New System.Drawing.Point(0, 0)
        Me.PanelAction.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PanelAction.Name = "PanelAction"
        Me.PanelAction.Size = New System.Drawing.Size(467, 37)
        Me.PanelAction.TabIndex = 3
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RenameToolStripMenuItem, Me.RemoveToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 70)
        '
        'RenameToolStripMenuItem
        '
        Me.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        Me.RenameToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RenameToolStripMenuItem.Text = "Rename"
        '
        'butOpenArchetype
        '
        Me.butOpenArchetype.Image = CType(resources.GetObject("butOpenArchetype.Image"), System.Drawing.Image)
        Me.butOpenArchetype.Location = New System.Drawing.Point(252, 6)
        Me.butOpenArchetype.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butOpenArchetype.Name = "butOpenArchetype"
        Me.butOpenArchetype.Size = New System.Drawing.Size(18, 20)
        Me.butOpenArchetype.TabIndex = 5
        Me.openToolTip.SetToolTip(Me.butOpenArchetype, "View embedded archetype")
        '
        'lblNodeId
        '
        Me.lblNodeId.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblNodeId.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblNodeId.Location = New System.Drawing.Point(425, 0)
        Me.lblNodeId.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNodeId.Name = "lblNodeId"
        Me.lblNodeId.Size = New System.Drawing.Size(42, 37)
        Me.lblNodeId.TabIndex = 4
        '
        'lblAction
        '
        Me.lblAction.Location = New System.Drawing.Point(6, 9)
        Me.lblAction.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(48, 13)
        Me.lblAction.TabIndex = 3
        Me.lblAction.Text = "Action"
        '
        'butGetAction
        '
        Me.butGetAction.Location = New System.Drawing.Point(222, 6)
        Me.butGetAction.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.butGetAction.Name = "butGetAction"
        Me.butGetAction.Size = New System.Drawing.Size(24, 20)
        Me.butGetAction.TabIndex = 2
        Me.butGetAction.Text = "..."
        Me.selectToolTip.SetToolTip(Me.butGetAction, "Browse for embedded archetype")
        '
        'txtAction
        '
        Me.txtAction.Location = New System.Drawing.Point(54, 6)
        Me.txtAction.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtAction.Name = "txtAction"
        Me.txtAction.Size = New System.Drawing.Size(163, 20)
        Me.txtAction.TabIndex = 1
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'TabPageActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelAction)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "TabPageActivity"
        Me.Size = New System.Drawing.Size(467, 306)
        Me.PanelAction.ResumeLayout(False)
        Me.PanelAction.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelAction As System.Windows.Forms.Panel
    Friend WithEvents butOpenArchetype As System.Windows.Forms.Button
    Friend WithEvents lblNodeId As System.Windows.Forms.Label
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents butGetAction As System.Windows.Forms.Button
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents HelpProviderActivity As System.Windows.Forms.HelpProvider
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RenameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents openToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents selectToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
