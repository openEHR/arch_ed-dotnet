Public Class ChooseType

    Private Sub listType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listType.SelectedIndexChanged
        Me.DialogResult = listType.SelectedIndex
        Me.Close()
    End Sub
End Class