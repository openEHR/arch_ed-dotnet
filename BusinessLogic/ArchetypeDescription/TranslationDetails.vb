Public Class TranslationDetails
    Protected mLanguage As String
    Protected mAuthorName As String = "?"
    Protected mAuthorEmail As String = ""
    Protected mAccreditation As String = ""
    Protected mTimeCommitted As String
    Protected mAuthorOrganisation As String = ""
    Protected mOtherDetails As New OtherDefinitionDetails

    Property Language() As String
        Get
            Return mLanguage
        End Get
        Set(ByVal Value As String)
            mLanguage = Value
        End Set
    End Property
    Property AuthorName() As String
        Get
            Return mAuthorName
        End Get
        Set(ByVal Value As String)
            mAuthorName = Value
        End Set
    End Property
    Property AuthorEmail() As String
        Get
            Return mAuthorEmail
        End Get
        Set(ByVal Value As String)
            mAuthorEmail = Value
        End Set
    End Property
    Property AuthorOrganisation() As String
        Get
            Return mAuthorOrganisation
        End Get
        Set(ByVal Value As String)
            mAuthorOrganisation = Value
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
