<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class myArchetypeFromWeb
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
        Me.lbl_thisConcept = New System.Windows.Forms.Label
        Me.btn_open = New System.Windows.Forms.Button
        Me.descriptionTable = New System.Windows.Forms.TableLayoutPanel
        Me.lbl_thisID = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lbl_thisConcept
        '
        Me.lbl_thisConcept.AutoSize = True
        Me.lbl_thisConcept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_thisConcept.ForeColor = System.Drawing.Color.DarkBlue
        Me.lbl_thisConcept.Location = New System.Drawing.Point(8, 5)
        Me.lbl_thisConcept.Name = "lbl_thisConcept"
        Me.lbl_thisConcept.Padding = New System.Windows.Forms.Padding(2)
        Me.lbl_thisConcept.Size = New System.Drawing.Size(86, 19)
        Me.lbl_thisConcept.TabIndex = 0
        Me.lbl_thisConcept.Text = "thisConcept"
        '
        'btn_open
        '
        Me.btn_open.AutoSize = True
        Me.btn_open.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btn_open.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btn_open.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btn_open.Location = New System.Drawing.Point(637, 5)
        Me.btn_open.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_open.Name = "btn_open"
        Me.btn_open.Size = New System.Drawing.Size(66, 23)
        Me.btn_open.TabIndex = 8
        Me.btn_open.Text = "Open"
        Me.btn_open.UseVisualStyleBackColor = False
        '
        'descriptionTable
        '
        Me.descriptionTable.AutoSize = True
        Me.descriptionTable.BackColor = System.Drawing.Color.LemonChiffon
        Me.descriptionTable.ColumnCount = 2
        Me.descriptionTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.descriptionTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 580.0!))
        Me.descriptionTable.Location = New System.Drawing.Point(11, 31)
        Me.descriptionTable.Margin = New System.Windows.Forms.Padding(0)
        Me.descriptionTable.Name = "descriptionTable"
        Me.descriptionTable.RowCount = 1
        Me.descriptionTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.descriptionTable.Size = New System.Drawing.Size(700, 23)
        Me.descriptionTable.TabIndex = 9
        '
        'lbl_thisID
        '
        Me.lbl_thisID.AutoSize = True
        Me.lbl_thisID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_thisID.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lbl_thisID.Location = New System.Drawing.Point(270, 5)
        Me.lbl_thisID.Name = "lbl_thisID"
        Me.lbl_thisID.Padding = New System.Windows.Forms.Padding(2)
        Me.lbl_thisID.Size = New System.Drawing.Size(42, 19)
        Me.lbl_thisID.TabIndex = 7
        Me.lbl_thisID.Text = "thisID"
        '
        'myArchetypeFromWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.descriptionTable)
        Me.Controls.Add(Me.btn_open)
        Me.Controls.Add(Me.lbl_thisID)
        Me.Controls.Add(Me.lbl_thisConcept)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "myArchetypeFromWeb"
        Me.Size = New System.Drawing.Size(720, 79)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_thisConcept As System.Windows.Forms.Label
    Friend WithEvents btn_open As System.Windows.Forms.Button
    Friend WithEvents descriptionTable As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_thisID As System.Windows.Forms.Label

End Class
