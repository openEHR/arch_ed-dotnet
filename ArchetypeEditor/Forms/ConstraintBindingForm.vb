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
    Friend WithEvents comboTerminology As System.Windows.Forms.ComboBox
    Friend WithEvents txtQuery As System.Windows.Forms.TextBox
    Friend WithEvents txtRelease As System.Windows.Forms.TextBox
    Friend WithEvents lblTerminology As System.Windows.Forms.Label
    Friend WithEvents lblQueryName As System.Windows.Forms.Label
    Friend WithEvents lblRelease As System.Windows.Forms.Label
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents lblQuery As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConstraintBindingForm))
        Me.comboTerminology = New System.Windows.Forms.ComboBox
        Me.txtQuery = New System.Windows.Forms.TextBox
        Me.txtRelease = New System.Windows.Forms.TextBox
        Me.lblTerminology = New System.Windows.Forms.Label
        Me.lblQueryName = New System.Windows.Forms.Label
        Me.lblRelease = New System.Windows.Forms.Label
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.lblQuery = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'comboTerminology
        '
        Me.comboTerminology.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboTerminology.Location = New System.Drawing.Point(15, 29)
        Me.comboTerminology.Name = "comboTerminology"
        Me.comboTerminology.Size = New System.Drawing.Size(437, 21)
        Me.comboTerminology.TabIndex = 0
        Me.comboTerminology.Text = "Choose..."
        '
        'txtQuery
        '
        Me.txtQuery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQuery.Location = New System.Drawing.Point(147, 83)
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Size = New System.Drawing.Size(305, 20)
        Me.txtQuery.TabIndex = 1
        '
        'txtRelease
        '
        Me.txtRelease.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRelease.Location = New System.Drawing.Point(15, 133)
        Me.txtRelease.Name = "txtRelease"
        Me.txtRelease.Size = New System.Drawing.Size(239, 20)
        Me.txtRelease.TabIndex = 2
        Me.txtRelease.Visible = False
        '
        'lblTerminology
        '
        Me.lblTerminology.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTerminology.Location = New System.Drawing.Point(12, 5)
        Me.lblTerminology.Name = "lblTerminology"
        Me.lblTerminology.Size = New System.Drawing.Size(440, 21)
        Me.lblTerminology.TabIndex = 3
        Me.lblTerminology.Text = "Terminology:"
        '
        'lblQueryName
        '
        Me.lblQueryName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblQueryName.Location = New System.Drawing.Point(12, 65)
        Me.lblQueryName.Name = "lblQueryName"
        Me.lblQueryName.Size = New System.Drawing.Size(440, 21)
        Me.lblQueryName.TabIndex = 4
        Me.lblQueryName.Text = "Query name:"
        '
        'lblRelease
        '
        Me.lblRelease.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRelease.Location = New System.Drawing.Point(12, 109)
        Me.lblRelease.Name = "lblRelease"
        Me.lblRelease.Size = New System.Drawing.Size(242, 21)
        Me.lblRelease.TabIndex = 5
        Me.lblRelease.Text = "Release"
        Me.lblRelease.Visible = False
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(272, 127)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(86, 28)
        Me.butOK.TabIndex = 6
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(366, 127)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(86, 28)
        Me.butCancel.TabIndex = 7
        Me.butCancel.Text = "Cancel"
        '
        'lblQuery
        '
        Me.lblQuery.Location = New System.Drawing.Point(12, 86)
        Me.lblQuery.Name = "lblQuery"
        Me.lblQuery.Size = New System.Drawing.Size(133, 21)
        Me.lblQuery.TabIndex = 8
        Me.lblQuery.Text = "terminology:"
        Me.lblQuery.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ConstraintBindingForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(464, 168)
        Me.Controls.Add(Me.lblQuery)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.txtQuery)
        Me.Controls.Add(Me.lblQueryName)
        Me.Controls.Add(Me.lblTerminology)
        Me.Controls.Add(Me.txtRelease)
        Me.Controls.Add(Me.comboTerminology)
        Me.Controls.Add(Me.lblRelease)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(472, 194)
        Me.Name = "ConstraintBindingForm"
        Me.Text = "Add Binding"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim mDataTable As DataTable

    Private Sub ConstraintBinding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mDataTable = New DataTable("DataTable")
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.Int32")
        idColumn.ColumnName = "Id"
        mDataTable.Columns.Add(idColumn)
        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = System.Type.GetType("System.String")
        CodeColumn.ColumnName = "Code"
        mDataTable.Columns.Add(CodeColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        mDataTable.Columns.Add(TextColumn)

        Dim Terminologies As DataRow() = Filemanager.Master.OntologyManager.GetTerminologyIdentifiers
        mDataTable.DefaultView.Sort = "Text"

        For i As Integer = 0 To Terminologies.Length - 1
            Dim newRow As DataRow = mDataTable.NewRow()
            newRow("Code") = Terminologies(i).Item(0)
            newRow("Text") = Terminologies(i).Item(1)
            mDataTable.Rows.Add(newRow)
        Next

        comboTerminology.DataSource = mDataTable
        comboTerminology.DisplayMember = "Text"
        comboTerminology.ValueMember = "Code"
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        If comboTerminology.SelectedIndex < 0 Or txtQuery.Text = "" Then
            MessageBox.Show(AE_Constants.Instance.Add_Reference, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Close()
        End If
    End Sub

    Public Sub AddConstraintBinding(ByVal ontologyManager As OntologyManager, ByVal acCode As String)
        ontologyManager.AddConstraintBinding(acCode, comboTerminology.SelectedValue, comboTerminology.SelectedText, txtRelease.Text, txtQuery.Text)
    End Sub

End Class
