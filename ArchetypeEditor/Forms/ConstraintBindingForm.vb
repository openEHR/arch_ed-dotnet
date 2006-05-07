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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.comboTerminology = New System.Windows.Forms.ComboBox
        Me.txtQuery = New System.Windows.Forms.TextBox
        Me.txtRelease = New System.Windows.Forms.TextBox
        Me.lblTerminology = New System.Windows.Forms.Label
        Me.lblQueryName = New System.Windows.Forms.Label
        Me.lblRelease = New System.Windows.Forms.Label
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'comboTerminology
        '
        Me.comboTerminology.Location = New System.Drawing.Point(48, 33)
        Me.comboTerminology.Name = "comboTerminology"
        Me.comboTerminology.Size = New System.Drawing.Size(392, 24)
        Me.comboTerminology.TabIndex = 0
        Me.comboTerminology.Text = "Choose..."
        '
        'txtQuery
        '
        Me.txtQuery.Location = New System.Drawing.Point(48, 96)
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Size = New System.Drawing.Size(392, 22)
        Me.txtQuery.TabIndex = 1
        Me.txtQuery.Text = ""
        '
        'txtRelease
        '
        Me.txtRelease.Location = New System.Drawing.Point(48, 152)
        Me.txtRelease.Name = "txtRelease"
        Me.txtRelease.Size = New System.Drawing.Size(344, 22)
        Me.txtRelease.TabIndex = 2
        Me.txtRelease.Text = ""
        '
        'lblTerminology
        '
        Me.lblTerminology.Location = New System.Drawing.Point(32, 8)
        Me.lblTerminology.Name = "lblTerminology"
        Me.lblTerminology.Size = New System.Drawing.Size(352, 24)
        Me.lblTerminology.TabIndex = 3
        Me.lblTerminology.Text = "Terminology"
        '
        'lblQueryName
        '
        Me.lblQueryName.Location = New System.Drawing.Point(32, 74)
        Me.lblQueryName.Name = "lblQueryName"
        Me.lblQueryName.Size = New System.Drawing.Size(352, 24)
        Me.lblQueryName.TabIndex = 4
        Me.lblQueryName.Text = "Query name"
        '
        'lblRelease
        '
        Me.lblRelease.Location = New System.Drawing.Point(32, 128)
        Me.lblRelease.Name = "lblRelease"
        Me.lblRelease.Size = New System.Drawing.Size(352, 24)
        Me.lblRelease.TabIndex = 5
        Me.lblRelease.Text = "Release"
        '
        'butOK
        '
        Me.butOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.butOK.Location = New System.Drawing.Point(368, 184)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(80, 32)
        Me.butOK.TabIndex = 6
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(256, 184)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(96, 32)
        Me.butCancel.TabIndex = 7
        Me.butCancel.Text = "Cancel"
        '
        'ConstraintBindingForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(464, 224)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.txtQuery)
        Me.Controls.Add(Me.lblQueryName)
        Me.Controls.Add(Me.lblTerminology)
        Me.Controls.Add(Me.txtRelease)
        Me.Controls.Add(Me.comboTerminology)
        Me.Controls.Add(Me.lblRelease)
        Me.Name = "ConstraintBindingForm"
        Me.Text = "Add binding"
        Me.ResumeLayout(False)

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

        Dim Terminologies As DataRow() _
                = Filemanager.Master.OntologyManager.GetTerminologyIdentifiers

        mDataTable.DefaultView.Sort = "Text"

        For i As Integer = 0 To Terminologies.Length - 1
            Dim newRow As DataRow = mDataTable.NewRow()
            newRow("Code") = Terminologies(i).Item(0)
            newRow("Text") = Terminologies(i).Item(1)
            mDataTable.Rows.Add(newRow)
        Next

        Me.comboTerminology.DataSource = mDataTable
        Me.comboTerminology.DisplayMember = "Text"
        Me.comboTerminology.ValueMember = "Code"
    End Sub
End Class
