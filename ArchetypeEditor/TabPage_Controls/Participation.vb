Public Class Participation

    Private mRestrictedModeSet As RestrictedSet
    Private mOccurrences As OccurrencesPanel
    Private mParticipation As RmParticipation

    Public Property Participation() As RmParticipation
        Get
            mParticipation.ModeSet = mRestrictedModeSet.AsCodePhrase
            Return mParticipation
        End Get
        Set(ByVal value As RmParticipation)
            mParticipation = value
            mOccurrences.Cardinality = mParticipation.Occurrences
            mRestrictedModeSet.AsCodePhrase = mParticipation.ModeSet
        End Set
    End Property
    Private Sub Participation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mRestrictedModeSet = New RestrictedSet
        mRestrictedModeSet.LocalFileManager = Filemanager.Master
        mRestrictedModeSet.TermSetToRestrict = RestrictedSet.TermSet.ParticipationMode
    End Sub


End Class
