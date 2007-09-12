Public Class Links

    Private Sub butAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddEvent.Click
        Me.flowPanelLinks.Controls.Add(New Link)
    End Sub

    Private WithEvents mLink As Link
    Private Sub SelectedParticipationChanged(ByVal sender As Object, ByVal e As EventArgs)
        For Each l As Link In flowPanelLinks.Controls
            If Not l Is sender Then
                l.Selected = False
            Else
                mLink = sender
            End If
        Next
    End Sub

    Public Function HasLinkConstraints() As Boolean
        'Allows empty participations to be ignored
        For Each l As Link In flowPanelLinks.Controls
            If l.HasConstraint Then
                Return True
            End If
        Next
        Return False
    End Function

End Class