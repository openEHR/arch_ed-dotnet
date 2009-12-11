Public Class ChooseType

    Private Sub SelectAndClose()
        If listType.SelectedIndex >= 0 Then
            DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub listType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listType.Click
        SelectAndClose()
    End Sub

    Private Sub listType_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles listType.KeyUp
        If e.KeyCode = Keys.Enter Then
            SelectAndClose()
        ElseIf e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

End Class