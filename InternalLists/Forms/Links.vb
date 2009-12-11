Public Class Links

    Private Sub butAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddEvent.Click
        Dim l As New Link
        panelLinks.Controls.Add(l)
        l.BringToFront()
        l.Dock = DockStyle.Top
        panelLinks.ScrollControlIntoView(l)
    End Sub

    Private WithEvents mLink As Link

    Private Sub SelectedLinkChanged(ByVal sender As Object, ByVal e As EventArgs)
        For Each l As Link In panelLinks.Controls
            If Not l Is sender Then
                l.Selected = False
            Else
                mLink = sender
            End If
        Next
    End Sub

    Public Function HasLinkConstraints() As Boolean
        'Allows empty participations to be ignored
        For Each l As Link In panelLinks.Controls
            If l.HasConstraint Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Property Links() As System.Collections.Generic.List(Of RmLink)
        Get
            Dim result As New System.Collections.Generic.List(Of RmLink)

            For Each l As Link In panelLinks.Controls
                If l.HasConstraint Then
                    result.Add(l.LinkConstraint)
                End If
            Next

            Return result
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of RmLink))
            For Each l As RmLink In value
                Dim newLink As New Link
                newLink.LinkConstraint = l
                panelLinks.Controls.Add(newLink)
                newLink.Dock = DockStyle.Top
            Next
        End Set
    End Property

    Private Sub flowPanelLinks_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles panelLinks.ControlAdded
        AddHandler e.Control.Enter, AddressOf SelectedLinkChanged
    End Sub

    Private Sub butRemoveElement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRemoveElement.Click
        If Not mLink Is Nothing Then
            RemoveHandler mLink.Enter, AddressOf SelectedLinkChanged
            panelLinks.Controls.Remove(mLink)
        End If
    End Sub

End Class