
Public Enum LifeCycleStates
    NotSet = 118
    Initial = 524
    AuthorDraft = 569
    OrganisationDraft = 571
    CommitteeDraft = 570
    Submitted = 572
    Candidate = 573
    ApprovedCandidate = 574
    Published = 575
    Rejected = 576
    Obsolete = 577
End Enum

Public MustInherit Class ArchetypeDescription
    Protected mOriginalAuthor As String
    Protected mOriginalAuthorEmail As String
    Protected mOriginalAuthorOrganisation As String
    Protected mOriginalAuthorDate As String
    Protected mLifeCycleState As LifeCycleStates
    Protected mArchetypePackageURI As String
    Protected mOtherDetails As New OtherDefinitionDetails
    Protected mArchetypeDetails As New ArchetypeDetails
    Protected mCopyRight As String
    Protected mOtherContributors As New Collections.Specialized.StringCollection
    Protected mReferences As String
    Protected mArchetypeDigest As String

    Property OriginalAuthor() As String
        Get
            Return mOriginalAuthor
        End Get
        Set(ByVal Value As String)
            mOriginalAuthor = Value
        End Set
    End Property

    Property OriginalAuthorEmail() As String
        Get
            Return mOriginalAuthorEmail
        End Get
        Set(ByVal Value As String)
            mOriginalAuthorEmail = Value
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

    Property OriginalAuthorDate() As String
        Get
            Return mOriginalAuthorDate
        End Get
        Set(ByVal Value As String)
            mOriginalAuthorDate = Value
        End Set
    End Property

    Property OtherContributors() As Collections.Specialized.StringCollection
        Get
            Return mOtherContributors
        End Get
        Set(ByVal value As Collections.Specialized.StringCollection)
            mOtherContributors = value
        End Set
    End Property

    Property References() As String
        Get
            Return mReferences
        End Get
        Set(ByVal Value As String)
            mReferences = Value
        End Set
    End Property

    Property ArchetypeDigest() As String
        Get
            Return mArchetypeDigest
        End Get
        Set(ByVal Value As String)
            mArchetypeDigest = Value
        End Set
    End Property

    Property CopyRight() As String
        Get
            Return mCopyRight
        End Get
        Set(ByVal Value As String)
            mCopyRight = Value
        End Set
    End Property

    Public Overridable Property Details() As ArchetypeDetails
        Get
            Return mArchetypeDetails
        End Get
        Set(ByVal value As ArchetypeDetails)
            mArchetypeDetails = value
        End Set
    End Property

    Property LifeCycleState() As LifeCycleStates
        Get
            Return mLifeCycleState
        End Get
        Set(ByVal Value As LifeCycleStates)
            mLifeCycleState = Value
        End Set
    End Property

    Property LifeCycleStateAsString() As String
        Get
            Return mLifeCycleState.ToString()
        End Get
        Set(ByVal Value As String)
            'Returns the structure type that matches the string
            'Ignoring case - could be a problem in some languages
            Try
                mLifeCycleState = CType([Enum].Parse(GetType(LifeCycleStates), Value, True), LifeCycleStates)
            Catch
                mLifeCycleState = LifeCycleStates.NotSet
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
End Class
