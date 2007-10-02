<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RestrictedSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RestrictedSet))
        Me.gbRestrictedData = New System.Windows.Forms.GroupBox
        Me.listRestrictionSet = New System.Windows.Forms.ListBox
        Me.butRemoveFromRestrictedSet = New System.Windows.Forms.Button
        Me.radioRestrictedSet = New System.Windows.Forms.RadioButton
        Me.radioUnrestrictedSubject = New System.Windows.Forms.RadioButton
        Me.butAddToRestrictedSet = New System.Windows.Forms.Button
        Me.gbRestrictedData.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbRestrictedData
        '
        Me.gbRestrictedData.Controls.Add(Me.listRestrictionSet)
        Me.gbRestrictedData.Controls.Add(Me.butRemoveFromRestrictedSet)
        Me.gbRestrictedData.Controls.Add(Me.radioRestrictedSet)
        Me.gbRestrictedData.Controls.Add(Me.radioUnrestrictedSubject)
        Me.gbRestrictedData.Controls.Add(Me.butAddToRestrictedSet)
        Me.gbRestrictedData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRestrictedData.Location = New System.Drawing.Point(0, 0)
        Me.gbRestrictedData.Name = "gbRestrictedData"
        Me.gbRestrictedData.Size = New System.Drawing.Size(283, 139)
        Me.gbRestrictedData.TabIndex = 17
        Me.gbRestrictedData.TabStop = False
        Me.gbRestrictedData.Text = "Restrict set"
        '
        'listRestrictionSet
        '
        Me.listRestrictionSet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listRestrictionSet.ItemHeight = 16
        Me.listRestrictionSet.Location = New System.Drawing.Point(40, 46)
        Me.listRestrictionSet.Name = "listRestrictionSet"
        Me.listRestrictionSet.Size = New System.Drawing.Size(232, 84)
        Me.listRestrictionSet.TabIndex = 13
        Me.listRestrictionSet.Visible = False
        '
        'butRemoveFromRestrictedSet
        '
        Me.butRemoveFromRestrictedSet.Image = CType(resources.GetObject("butRemoveFromRestrictedSet.Image"), System.Drawing.Image)
        Me.butRemoveFromRestrictedSet.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveFromRestrictedSet.Location = New System.Drawing.Point(8, 82)
        Me.butRemoveFromRestrictedSet.Name = "butRemoveFromRestrictedSet"
        Me.butRemoveFromRestrictedSet.Size = New System.Drawing.Size(27, 25)
        Me.butRemoveFromRestrictedSet.TabIndex = 15
        Me.butRemoveFromRestrictedSet.Visible = False
        '
        'radioRestrictedSet
        '
        Me.radioRestrictedSet.Location = New System.Drawing.Point(160, 18)
        Me.radioRestrictedSet.Name = "radioRestrictedSet"
        Me.radioRestrictedSet.Size = New System.Drawing.Size(136, 28)
        Me.radioRestrictedSet.TabIndex = 1
        Me.radioRestrictedSet.Text = "Restricted"
        '
        'radioUnrestrictedSubject
        '
        Me.radioUnrestrictedSubject.Checked = True
        Me.radioUnrestrictedSubject.Location = New System.Drawing.Point(14, 18)
        Me.radioUnrestrictedSubject.Name = "radioUnrestrictedSubject"
        Me.radioUnrestrictedSubject.Size = New System.Drawing.Size(138, 28)
        Me.radioUnrestrictedSubject.TabIndex = 0
        Me.radioUnrestrictedSubject.TabStop = True
        Me.radioUnrestrictedSubject.Text = "Unrestricted"
        '
        'butAddToRestrictedSet
        '
        Me.butAddToRestrictedSet.Image = CType(resources.GetObject("butAddToRestrictedSet.Image"), System.Drawing.Image)
        Me.butAddToRestrictedSet.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddToRestrictedSet.Location = New System.Drawing.Point(8, 51)
        Me.butAddToRestrictedSet.Name = "butAddToRestrictedSet"
        Me.butAddToRestrictedSet.Size = New System.Drawing.Size(27, 25)
        Me.butAddToRestrictedSet.TabIndex = 14
        Me.butAddToRestrictedSet.Visible = False
        '
        'RestrictedSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbRestrictedData)
        Me.Name = "RestrictedSet"
        Me.Size = New System.Drawing.Size(283, 139)
        Me.gbRestrictedData.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbRestrictedData As System.Windows.Forms.GroupBox
    Friend WithEvents listRestrictionSet As System.Windows.Forms.ListBox
    Friend WithEvents butRemoveFromRestrictedSet As System.Windows.Forms.Button
    Friend WithEvents radioRestrictedSet As System.Windows.Forms.RadioButton
    Friend WithEvents radioUnrestrictedSubject As System.Windows.Forms.RadioButton
    Friend WithEvents butAddToRestrictedSet As System.Windows.Forms.Button

End Class
