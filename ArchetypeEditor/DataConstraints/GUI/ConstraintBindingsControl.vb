Public Class ConstraintBindingsControl

    Private view As DataView
    Private ontologyManager As OntologyManager
    Private acCode As String

    Public Sub TranslateGui()
        TerminologyColumn.HeaderText = AE_Constants.Instance.Terminology
        ReleaseColumn.HeaderText = Filemanager.GetOpenEhrTerm(97, ReleaseColumn.HeaderText)
        SubsetColumn.HeaderText = Filemanager.GetOpenEhrTerm(624, SubsetColumn.HeaderText)
        NewButtonToolTip.SetToolTip(NewButton, Filemanager.GetOpenEhrTerm(99, "Add constraint binding"))
    End Sub

    Public Sub BindTables(ByVal ontologyManager As OntologyManager)
        Me.ontologyManager = ontologyManager
        view = New DataView(ontologyManager.ConstraintBindingsTable)
        view.AllowNew = False
        Grid.DataSource = view
        Grid.Columns(4).Visible = Grid.Width > TerminologyColumn.Width + ReleaseColumn.Width + 400
    End Sub

    Public Sub SelectConstraintCode(ByVal value As String)
        acCode = value

        If Not view Is Nothing Then
            If String.IsNullOrEmpty(acCode) Then
                view.RowFilter = "ID = NULL"
            Else
                view.RowFilter = "ID = '" & acCode & "'"
            End If
        End If
    End Sub

    Private Sub Grid_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles Grid.DataBindingComplete
        For Each row As DataGridViewRow In Grid.Rows
            row.Cells(0).ToolTipText = TryCast(row.Cells(4).Value, String)
        Next
    End Sub

    Private Sub Grid_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex = 0 And Main.Instance.Options.AllowTerminologyLookUp And Not Main.Instance.TerminologyLookup Is Nothing Then
            Dim form As New TerminologyLookup.TerminologySelectionForm(Main.Instance.TerminologyLookup)
            Main.Instance.TerminologyLookup.Url = Main.Instance.Options.TerminologyUrlString

            Dim row As DataGridViewRow = Grid.Rows(e.RowIndex)
            form.TerminologyId = TryCast(row.Cells(0).Value, String)
            form.SubsetId = ""
            form.ShowDialog(ParentForm)

            If form.DialogResult = DialogResult.OK Then
                row.Cells(0).Value = form.TerminologyId
                row.Cells(3).Value = form.SubsetId
            End If

            Grid.CurrentCell = row.Cells(2)
        End If
    End Sub

    Private Sub NewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton.Click
        If Not String.IsNullOrEmpty(acCode) Then
            Dim frm As New ConstraintBindingForm
            frm.Text = Filemanager.GetOpenEhrTerm(99, frm.Text)

            If Main.Instance.DefaultLanguageCode <> "en" Then
                frm.SubsetLabel.Text = Filemanager.GetOpenEhrTerm(624, frm.SubsetLabel.Text)
                frm.ReleaseLabel.Text = Filemanager.GetOpenEhrTerm(97, frm.ReleaseLabel.Text)
                frm.TerminologyLabel.Text = AE_Constants.Instance.Terminology
                frm.CancelCloseButton.Text = AE_Constants.Instance.Cancel
                frm.OkButton.Text = AE_Constants.Instance.OK
            End If

            If frm.ShowDialog(ParentForm) = Windows.Forms.DialogResult.OK Then
                frm.AddConstraintBinding(ontologyManager, acCode)
                SelectConstraintCode(acCode)
            End If
        End If
    End Sub

    Private Sub RemoveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveButton.Click
        If Not Grid.CurrentRow Is Nothing AndAlso Grid.CurrentRow.Index >= 0 Then
            view.Delete(Grid.CurrentRow.Index)
        End If
    End Sub

End Class
