Public Class TranslationDetails
    Protected mLanguage As String
    Protected mAuthor As String
    Protected mAccreditation As String
    Protected mTimeCommitted As String
    Protected mOtherDetails As New OtherDefinitionDetails

    Property Language() As String
        Get
            Return mLanguage
        End Get
        Set(ByVal Value As String)
            mLanguage = Value
        End Set
    End Property
    Property Author() As String
        Get
            Return mAuthor
        End Get
        Set(ByVal Value As String)
            mAuthor = Value
        End Set
    End Property
    Property TimeCommitted() As DateTime
        Get
            Return mTimeCommitted
        End Get
        Set(ByVal Value As DateTime)
            mTimeCommitted = Value
        End Set
    End Property
    Property Accreditation() As String
        Get
            Return mAccreditation
        End Get
        Set(ByVal Value As String)
            mAccreditation = Value
        End Set
    End Property
    ReadOnly Property OtherDetails() As OtherDefinitionDetails
        Get
            Return mOtherDetails
        End Get
    End Property
End Class
