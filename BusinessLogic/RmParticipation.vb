Public Class RmParticipation
    Private mOccurrences As New RmCardinality(0)

    Public Property Occurrences() As RmCardinality
        Get
            Return mOccurrences
        End Get
        Set(ByVal value As RmCardinality)
            mOccurrences = value
        End Set
    End Property

    Private mModeSet As New CodePhrase

    Public Property ModeSet() As CodePhrase
        Get
            Return mModeSet
        End Get
        Set(ByVal value As CodePhrase)
            mModeSet = value
        End Set
    End Property



End Class
