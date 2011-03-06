Public Class ConstraintBindingForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TerminologyComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReleaseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TerminologyLabel As System.Windows.Forms.Label
    Friend WithEvents SubsetLabel As System.Windows.Forms.Label
    Friend WithEvents ReleaseLabel As System.Windows.Forms.Label
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents SubsetButton As System.Windows.Forms.Button
    Friend WithEvents TerminologyErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents CancelCloseButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConstraintBindingForm))
        Me.TerminologyComboBox = New System.Windows.Forms.ComboBox
        Me.SubsetTextBox = New System.Windows.Forms.TextBox
        Me.ReleaseTextBox = New System.Windows.Forms.TextBox
        Me.TerminologyLabel = New System.Windows.Forms.Label
        Me.SubsetLabel = New System.Windows.Forms.Label
        Me.ReleaseLabel = New System.Windows.Forms.Label
        Me.OkButton = New System.Windows.Forms.Button
        Me.CancelCloseButton = New System.Windows.Forms.Button
        Me.SubsetButton = New System.Windows.Forms.Button
        Me.TerminologyErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.TerminologyErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TerminologyComboBox
        '
        Me.TerminologyComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerminologyComboBox.Location = New System.Drawing.Point(127, 5)
        Me.TerminologyComboBox.Name = "TerminologyComboBox"
        Me.TerminologyComboBox.Size = New System.Drawing.Size(353, 21)
        Me.TerminologyComboBox.TabIndex = 1
        '
        'SubsetTextBox
        '
        Me.SubsetTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SubsetTextBox.Location = New System.Drawing.Point(127, 32)
        Me.SubsetTextBox.Name = "SubsetTextBox"
        Me.SubsetTextBox.Size = New System.Drawing.Size(314, 20)
        Me.SubsetTextBox.TabIndex = 3
        '
        'ReleaseTextBox
        '
        Me.ReleaseTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReleaseTextBox.Location = New System.Drawing.Point(127, 61)
        Me.ReleaseTextBox.Name = "ReleaseTextBox"
        Me.ReleaseTextBox.Size = New System.Drawing.Size(353, 20)
        Me.ReleaseTextBox.TabIndex = 6
        '
        'TerminologyLabel
        '
        Me.TerminologyLabel.Location = New System.Drawing.Point(12, 5)
        Me.TerminologyLabel.Name = "TerminologyLabel"
        Me.TerminologyLabel.Size = New System.Drawing.Size(98, 21)
        Me.TerminologyLabel.TabIndex = 0
        Me.TerminologyLabel.Text = "Terminology"
        '
        'SubsetLabel
        '
        Me.SubsetLabel.Location = New System.Drawing.Point(12, 32)
        Me.SubsetLabel.Name = "SubsetLabel"
        Me.SubsetLabel.Size = New System.Drawing.Size(98, 21)
        Me.SubsetLabel.TabIndex = 2
        Me.SubsetLabel.Text = "Subset"
        '
        'ReleaseLabel
        '
        Me.ReleaseLabel.Location = New System.Drawing.Point(12, 64)
        Me.ReleaseLabel.Name = "ReleaseLabel"
        Me.ReleaseLabel.Size = New System.Drawing.Size(98, 21)
        Me.ReleaseLabel.TabIndex = 5
        Me.ReleaseLabel.Text = "Release"
        '
        'OkButton
        '
        Me.OkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OkButton.Location = New System.Drawing.Point(300, 100)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(86, 28)
        Me.OkButton.TabIndex = 7
        Me.OkButton.Text = "OK"
        '
        'CancelCloseButton
        '
        Me.CancelCloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelCloseButton.Location = New System.Drawing.Point(394, 100)
        Me.CancelCloseButton.Name = "CancelCloseButton"
        Me.CancelCloseButton.Size = New System.Drawing.Size(86, 28)
        Me.CancelCloseButton.TabIndex = 8
        Me.CancelCloseButton.Text = "Cancel"
        '
        'SubsetButton
        '
        Me.SubsetButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SubsetButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubsetButton.Location = New System.Drawing.Point(447, 32)
        Me.SubsetButton.Name = "SubsetButton"
        Me.SubsetButton.Size = New System.Drawing.Size(33, 23)
        Me.SubsetButton.TabIndex = 4
        Me.SubsetButton.Text = "..."
        Me.SubsetButton.UseVisualStyleBackColor = True
        '
        'TerminologyErrorProvider
        '
        Me.TerminologyErrorProvider.ContainerControl = Me
        '
        'ConstraintBindingForm
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.CancelCloseButton
        Me.ClientSize = New System.Drawing.Size(492, 141)
        Me.Controls.Add(Me.SubsetButton)
        Me.Controls.Add(Me.CancelCloseButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.SubsetTextBox)
        Me.Controls.Add(Me.SubsetLabel)
        Me.Controls.Add(Me.TerminologyLabel)
        Me.Controls.Add(Me.ReleaseTextBox)
        Me.Controls.Add(Me.TerminologyComboBox)
        Me.Controls.Add(Me.ReleaseLabel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(800, 168)
        Me.MinimumSize = New System.Drawing.Size(500, 168)
        Me.Name = "ConstraintBindingForm"
        Me.Text = "Add Binding"
        CType(Me.TerminologyErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub ConstraintBinding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim table As New DataTable("DataTable")

        Dim codeColumn As DataColumn = New DataColumn
        codeColumn.DataType = System.Type.GetType("System.String")
        codeColumn.ColumnName = "Code"
        table.Columns.Add(codeColumn)

        Dim textColumn As DataColumn = New DataColumn
        textColumn.DataType = System.Type.GetType("System.String")
        textColumn.ColumnName = "Text"
        table.Columns.Add(textColumn)

        Dim terminologies As DataRow() = Filemanager.Master.OntologyManager.GetTerminologyIdentifiers
        table.DefaultView.Sort = "Text"

        For i As Integer = 0 To terminologies.Length - 1
            Dim row As DataRow = table.NewRow()
            row("Code") = terminologies(i).Item(0)
            row("Text") = terminologies(i).Item(1)
            table.Rows.Add(row)
        Next

        TerminologyComboBox.DataSource = table
        TerminologyComboBox.DisplayMember = "Text"
        TerminologyComboBox.ValueMember = "Code"
        TerminologyComboBox.SelectedValue = DBNull.Value
        SubsetButton.Visible = Main.Instance.Options.AllowTerminologyLookUp And Not Main.Instance.TerminologyLookup Is Nothing
    End Sub

    Private Sub SubsetButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubsetButton.Click
        Dim form As New TerminologyLookup.TerminologySelectionForm(Main.Instance.TerminologyLookup)
        Main.Instance.TerminologyLookup.Url = Main.Instance.Options.TerminologyUrlString

        form.TerminologyId = TerminologyComboBox.Text
        form.SubsetId = ""
        form.ShowDialog(ParentForm)

        If form.DialogResult = DialogResult.OK Then
            TerminologyComboBox.Text = form.TerminologyId
            SubsetTextBox.Text = form.SubsetId
        End If
    End Sub

    Private Sub TerminologyComboBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TerminologyComboBox.Validating
        Dim message As String = ""

        If Not CancelCloseButton.Focused Then
            If Not String.IsNullOrEmpty(TerminologyId) And Not System.Text.RegularExpressions.Regex.IsMatch(TerminologyId, "^[a-zA-Z][a-zA-Z0-9_\-/+]+$") Then
                e.Cancel = True
                message = "Terminology name must start with a letter, followed by one or more letters, digits, underscores, minuses, slashes or pluses"
            End If
        End If

        TerminologyErrorProvider.SetError(TerminologyComboBox, message)
        TerminologyErrorProvider.SetIconAlignment(TerminologyComboBox, ErrorIconAlignment.MiddleLeft)
    End Sub

    Public Sub AddConstraintBinding(ByVal ontologyManager As OntologyManager, ByVal acCode As String)
        Dim id As String = TerminologyId()
        Dim uri As String = ontologyManager.ConstraintBindingUri(id, ReleaseTextBox.Text, SubsetTextBox.Text)
        ontologyManager.AddTerminology(id)
        ontologyManager.AddConstraintBinding(id, acCode, uri)
    End Sub

    Public Function TerminologyId() As String
        Dim result As String = TryCast(TerminologyComboBox.SelectedValue, String)

        If String.IsNullOrEmpty(result) Then
            result = TerminologyComboBox.Text
        End If

        Return result
    End Function

End Class
