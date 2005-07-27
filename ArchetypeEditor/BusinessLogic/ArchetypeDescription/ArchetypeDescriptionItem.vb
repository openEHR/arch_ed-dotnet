Public Class ArchetypeDescriptionItem
    Protected mLanguage As String
    Protected mPurpose As String
    Protected mUse As String
    Protected mMisUse As String
    Protected mCopyright As String
    Protected mOriginalResourceURI As String
    Protected mOtherDetails As New OtherDefinitionDetails

    Property Language() As String
        Get
            Return mLanguage
        End Get
        Set(ByVal Value As String)
            mLanguage = Value
        End Set
    End Property
    Property Purpose() As String
        Get
            Return mPurpose
        End Get
        Set(ByVal Value As String)
            mPurpose = Value
        End Set
    End Property
    Property Use() As String
        Get
            Return mUse
        End Get
        Set(ByVal Value As String)
            mUse = Value
        End Set
    End Property
    Property MisUse() As String
        Get
            Return mMisUse
        End Get
        Set(ByVal Value As String)
            mMisUse = Value
        End Set
    End Property
    Property Copyright() As String
        Get
            Return mCopyright
        End Get
        Set(ByVal Value As String)
            mCopyright = Value
        End Set
    End Property
    Property OriginalResourceURI() As String
        Get
            Return mOriginalResourceURI
        End Get
        Set(ByVal Value As String)
            mOriginalResourceURI = Value
        End Set
    End Property
    ReadOnly Property OtherDetails() As OtherDefinitionDetails
        Get
            Return mOtherDetails
        End Get
    End Property

End Class
