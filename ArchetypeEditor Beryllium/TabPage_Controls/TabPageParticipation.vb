Public Class TabPageParticipation

    Private mCardinality As OccurrencesPanel

    Private Sub chkProvider_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProvider.CheckedChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Private WithEvents mParticipation As Participation
    Private Sub SelectedParticipationChanged(ByVal sender As Object, ByVal e As EventArgs)
        For Each p As Participation In flowPanelParticipations.Controls
            If Not p Is sender Then
                p.Selected = False
            Else
                mParticipation = sender
            End If
        Next
    End Sub

    Public Function HasOtherParticipations() As Boolean
        'Allows empty participations to be ignored
        For Each p As Participation In flowPanelParticipations.Controls
            If p.HasConstraint Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub Reset()
        flowPanelParticipations.Controls.Clear()
        mParticipation = Nothing
        chkProvider.Checked = False
        Me.panelOccurrences.Visible = False
        Me.panelOccurrences.Controls.Clear()
    End Sub

    Public Property OtherParticipations() As RmStructureCompound
        Get
            If HasOtherParticipations() Then
                Dim otherPart As New RmStructureCompound("other_participations", StructureType.OtherParticipations)
                For Each p As Participation In flowPanelParticipations.Controls
                    otherPart.Children.Add(p.ParticipationConstraint)
                Next
                Return otherPart
            End If
            Return Nothing
        End Get
        Set(ByVal value As RmStructureCompound)
            For Each p As RmParticipation In value.Children
                Dim new_participation As New Participation
                new_participation.ParticipationConstraint = p
                flowPanelParticipations.Controls.Add(new_participation)
            Next
            If flowPanelParticipations.Controls.Count > 1 Then
                DisplayCardinality()
                If Not mCardinality Is Nothing Then
                    mCardinality.Cardinality = value.Children.Cardinality
                End If
            End If
        End Set
    End Property

    Private Sub butAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddEvent.Click
        DisplayCardinality()
        Me.flowPanelParticipations.Controls.Add(New Participation)
    End Sub

    Private Sub DisplayCardinality()
        If (flowPanelParticipations.Controls.Count = 1) Then
            Me.panelOccurrences.Visible = True
            If mCardinality Is Nothing Then
                mCardinality = New OccurrencesPanel(Filemanager.Master)
                mCardinality.Title = Filemanager.GetOpenEhrTerm(437, "Cardinality")
                panelOccurrences.Controls.Add(mCardinality)
                mCardinality.Dock = DockStyle.Fill
            End If
        End If
    End Sub

    Private Sub butRemoveElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveElement.Click
        If Not mParticipation Is Nothing Then
            If (flowPanelParticipations.Controls.Count = 2) Then
                Me.panelOccurrences.Visible = False
                panelOccurrences.Controls.Clear()
                mCardinality = Nothing
            End If
            RemoveHandler mParticipation.Enter, AddressOf SelectedParticipationChanged
            flowPanelParticipations.Controls.Remove(mParticipation)
        End If
    End Sub

    Private Sub flowPanelParticipations_ControlAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles flowPanelParticipations.ControlAdded
        AddHandler e.Control.Enter, AddressOf SelectedParticipationChanged
    End Sub
End Class
