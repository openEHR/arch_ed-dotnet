<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConstraintBindingsControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConstraintBindingsControl))
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.NewButton = New System.Windows.Forms.Button
        Me.RemoveButton = New System.Windows.Forms.Button
        Me.NewButtonToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.TerminologyColumn = New System.Windows.Forms.DataGridViewButtonColumn
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ReleaseColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubsetColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CodePhrase = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TerminologyColumn, Me.ID, Me.ReleaseColumn, Me.SubsetColumn, Me.CodePhrase})
        Me.Grid.Location = New System.Drawing.Point(33, 0)
        Me.Grid.Name = "Grid"
        Me.Grid.RowHeadersWidth = 25
        Me.Grid.RowTemplate.Height = 24
        Me.Grid.Size = New System.Drawing.Size(651, 292)
        Me.Grid.TabIndex = 2
        '
        'NewButton
        '
        Me.NewButton.Image = CType(resources.GetObject("NewButton.Image"), System.Drawing.Image)
        Me.NewButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.NewButton.Location = New System.Drawing.Point(3, 3)
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(24, 24)
        Me.NewButton.TabIndex = 0
        Me.NewButtonToolTip.SetToolTip(Me.NewButton, "Add constraint binding")
        '
        'RemoveButton
        '
        Me.RemoveButton.Image = CType(resources.GetObject("RemoveButton.Image"), System.Drawing.Image)
        Me.RemoveButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.RemoveButton.Location = New System.Drawing.Point(3, 35)
        Me.RemoveButton.Name = "RemoveButton"
        Me.RemoveButton.Size = New System.Drawing.Size(24, 24)
        Me.RemoveButton.TabIndex = 1
        '
        'TerminologyColumn
        '
        Me.TerminologyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TerminologyColumn.DataPropertyName = "Terminology"
        Me.TerminologyColumn.FillWeight = 60.0!
        Me.TerminologyColumn.HeaderText = "Terminology"
        Me.TerminologyColumn.MinimumWidth = 100
        Me.TerminologyColumn.Name = "TerminologyColumn"
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "acCode"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'ReleaseColumn
        '
        Me.ReleaseColumn.DataPropertyName = "Release"
        Me.ReleaseColumn.FillWeight = 10.0!
        Me.ReleaseColumn.HeaderText = "Release"
        Me.ReleaseColumn.Name = "ReleaseColumn"
        '
        'SubsetColumn
        '
        Me.SubsetColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SubsetColumn.DataPropertyName = "Subset"
        Me.SubsetColumn.FillWeight = 20.0!
        Me.SubsetColumn.HeaderText = "Subset"
        Me.SubsetColumn.Name = "SubsetColumn"
        '
        'CodePhrase
        '
        Me.CodePhrase.DataPropertyName = "CodePhrase"
        Me.CodePhrase.HeaderText = "URI"
        Me.CodePhrase.Name = "CodePhrase"
        Me.CodePhrase.ReadOnly = True
        Me.CodePhrase.Width = 300
        '
        'ConstraintBindingsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.NewButton)
        Me.Controls.Add(Me.RemoveButton)
        Me.Controls.Add(Me.Grid)
        Me.Name = "ConstraintBindingsControl"
        Me.Size = New System.Drawing.Size(684, 292)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents RemoveButton As System.Windows.Forms.Button
    Friend WithEvents NewButtonToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents TerminologyColumn As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReleaseColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubsetColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodePhrase As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
