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
        Me.gbRestrictedData = New System.Windows.Forms.GroupBox()
        Me.listRestrictionSet = New System.Windows.Forms.ListBox()
        Me.butRemoveFromRestrictedSet = New System.Windows.Forms.Button()
        Me.radioRestrictedSet = New System.Windows.Forms.RadioButton()
        Me.radioUnrestrictedSubject = New System.Windows.Forms.RadioButton()
        Me.butAddToRestrictedSet = New System.Windows.Forms.Button()
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
        Me.gbRestrictedData.Margin = New System.Windows.Forms.Padding(2)
        Me.gbRestrictedData.Name = "gbRestrictedData"
        Me.gbRestrictedData.Padding = New System.Windows.Forms.Padding(2)
        Me.gbRestrictedData.Size = New System.Drawing.Size(212, 113)
        Me.gbRestrictedData.TabIndex = 17
        Me.gbRestrictedData.TabStop = False
        Me.gbRestrictedData.Text = "Restrict set"
        '
        'listRestrictionSet
        '
        Me.listRestrictionSet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listRestrictionSet.HorizontalScrollbar = True
        Me.listRestrictionSet.Location = New System.Drawing.Point(34, 37)
        Me.listRestrictionSet.Margin = New System.Windows.Forms.Padding(2)
        Me.listRestrictionSet.Name = "listRestrictionSet"
        Me.listRestrictionSet.Size = New System.Drawing.Size(171, 69)
        Me.listRestrictionSet.TabIndex = 16
        Me.listRestrictionSet.Visible = False
        '
        'butRemoveFromRestrictedSet
        '
        Me.butRemoveFromRestrictedSet.Image = CType(resources.GetObject("butRemoveFromRestrictedSet.Image"), System.Drawing.Image)
        Me.butRemoveFromRestrictedSet.Location = New System.Drawing.Point(6, 67)
        Me.butRemoveFromRestrictedSet.Margin = New System.Windows.Forms.Padding(2)
        Me.butRemoveFromRestrictedSet.Name = "butRemoveFromRestrictedSet"
        Me.butRemoveFromRestrictedSet.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveFromRestrictedSet.TabIndex = 15
        Me.butRemoveFromRestrictedSet.Visible = False
        '
        'radioRestrictedSet
        '
        Me.radioRestrictedSet.Location = New System.Drawing.Point(101, 15)
        Me.radioRestrictedSet.Margin = New System.Windows.Forms.Padding(2)
        Me.radioRestrictedSet.Name = "radioRestrictedSet"
        Me.radioRestrictedSet.Size = New System.Drawing.Size(74, 23)
        Me.radioRestrictedSet.TabIndex = 1
        Me.radioRestrictedSet.Text = "Restricted"
        '
        'radioUnrestrictedSubject
        '
        Me.radioUnrestrictedSubject.Checked = True
        Me.radioUnrestrictedSubject.Location = New System.Drawing.Point(10, 15)
        Me.radioUnrestrictedSubject.Margin = New System.Windows.Forms.Padding(2)
        Me.radioUnrestrictedSubject.Name = "radioUnrestrictedSubject"
        Me.radioUnrestrictedSubject.Size = New System.Drawing.Size(87, 23)
        Me.radioUnrestrictedSubject.TabIndex = 0
        Me.radioUnrestrictedSubject.TabStop = True
        Me.radioUnrestrictedSubject.Text = "Unrestricted"
        '
        'butAddToRestrictedSet
        '
        Me.butAddToRestrictedSet.Image = CType(resources.GetObject("butAddToRestrictedSet.Image"), System.Drawing.Image)
        Me.butAddToRestrictedSet.Location = New System.Drawing.Point(6, 41)
        Me.butAddToRestrictedSet.Margin = New System.Windows.Forms.Padding(2)
        Me.butAddToRestrictedSet.Name = "butAddToRestrictedSet"
        Me.butAddToRestrictedSet.Size = New System.Drawing.Size(24, 24)
        Me.butAddToRestrictedSet.TabIndex = 14
        Me.butAddToRestrictedSet.Visible = False
        '
        'RestrictedSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbRestrictedData)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "RestrictedSet"
        Me.Size = New System.Drawing.Size(212, 113)
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
