Public Class ArchetypeDescription
    Protected mOriginalAuthor As String
    Protected mOriginalAuthorOrganisation As String
    Protected mLifeCycleState As LifeCycle
    Protected mADL_Version As String = "1.2"
    Protected mArchetypePackageURI As String
    Protected mOtherDetails As New OtherDefinitionDetails

    Enum LifeCycle
        NotSet = 0
        Initial = 5
        AuthorDraft = 10
        CommitteeDraft = 20
        OrganisationDraft = 40
        Candidate = 60
        ApprovedCandidate = 80
        Published = 100
    End Enum

    Property OriginalAuthor() As String
        Get
            Return mOriginalAuthor
        End Get
        Set(ByVal Value As String)
            mOriginalAuthor = Value
        End Set
    End Property
    Property OriginalAuthorOrganisation() As String
        Get
            Return mOriginalAuthorOrganisation
        End Get
        Set(ByVal Value As String)
            mOriginalAuthorOrganisation = Value
        End Set
    End Property
    Property LifeCycleState() As LifeCycle
        Get
            Return mLifeCycleState
        End Get
        Set(ByVal Value As LifeCycle)
            mLifeCycleState = Value
        End Set
    End Property
    ReadOnly Property ADL_Version() As String
        Get
            Return mADL_Version
        End Get
    End Property
    Property LifeCycleStateAsString() As String
        Get
            mLifeCycleState.ToString()
        End Get
        Set(ByVal Value As String)
            'Returns the structure type that matches the string
            'Ignoring case - could be a problem in some languages
            Try
                mLifeCycleState = CType([Enum].Parse(GetType(LifeCycle), Value, True), LifeCycle)
            Catch
                'FIXME - raise error
            End Try
        End Set
    End Property
    Property ArchetypePackageURI() As String
        Get
            Return mArchetypePackageURI
        End Get
        Set(ByVal Value As String)
            mArchetypePackageURI = Value
        End Set
    End Property
    ReadOnly Property OtherDetails() As OtherDefinitionDetails
        Get
            Return mOtherDetails
        End Get
    End Property

End Class
