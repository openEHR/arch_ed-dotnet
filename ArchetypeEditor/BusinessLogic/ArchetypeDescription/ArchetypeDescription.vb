Imports AM = XMLParser.OpenEhr.V1.Its.Xml.AM
Imports XMLParser

Public Enum LifeCycleStates
    NotSet = 118
    '' AEPR-32 IMCN 5 May 2015 removed old author lifecycle states
    ''  Initial = 524
    ''AuthorDraft = 569
    ''OrganisationDraft = 571
    ''CommitteeDraft = 570
    ''Submitted = 572
    ''Candidate = 573
    ''ApprovedCandidate = 574
    published = 575
    rejected = 576
    '   obsolete = 577
    deprecated = 770
    in_development = 771
End Enum

Public MustInherit Class ArchetypeDescription
    Protected mOriginalAuthor As String
    Protected mOriginalAuthorEmail As String
    Protected mOriginalAuthorOrganisation As String
    Protected mOriginalAuthorDate As String
    Protected mLifeCycleState As LifeCycleStates
    Protected mADL_Version As String = "1.4"
    Protected mArchetypePackageURI As String
    Protected mOtherDetails As New OtherDefinitionDetails
    Protected mArchetypeDetails As New ArchetypeDetails
    Protected mOtherContributors As New Collections.Specialized.StringCollection
    Protected mReferences As String
    Protected mCurrentContact As String
    Protected mLicence As String
    Protected mOriginalPublisher As String
    Protected mOriginalNamespace As String
    Protected mCustodianOrganisation As String
    Protected mCustodianNamespace As String
    Protected mRevision As String
    Protected mReviewDate As String
    Protected mBuildUid As String
    Private mArchetypeDigest As String

    Public Const CurrentContactKey As String = "current_contact"
    Public Const OriginalPublisherKey As String = "original_publisher"
    Public Const OriginalNamespaceKey As String = "original_namespace"
    Public Const CustodianorganisationKey As String = "custodian_organisation"
    Public Const CustodianNamespaceKey As String = "custodian_namespace"
    Public Const ReviewDateKey As String = "review_date"
    Public Const LicenceKey As String = "licence"
    Public Const RevisionKey As String = "revision"
    Public Const ReferencesKey As String = "references"
    Public Const BuildUidKey As String = "build_uid"
 

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

    Property CurrentContact() As String
        Get
            Return mCurrentContact
        End Get
        Set(ByVal Value As String)
            mCurrentContact = Value
        End Set
    End Property
    
    Public Property Licence() As String
		Get
			Return mLicence
		End Get
		Set
			mLicence = value
		End Set
	End Property

	Public Property OriginalPublisher() As String
		Get
			Return mOriginalPublisher
		End Get
		Set
			mOriginalPublisher = value
		End Set
	End Property

	Public Property OriginalNamespace() As String
		Get
			Return mOriginalNamespace
		End Get
		Set
			mOriginalNamespace = value
		End Set
	End Property

	Public Property CustodianOrganisation() As String
		Get
			Return mCustodianOrganisation
		End Get
		Set
			mCustodianOrganisation = value
		End Set
	End Property

	Public Property CustodianNamespace() As String
		Get
			Return mCustodianNamespace
		End Get
		Set
			mCustodianNamespace = value
		End Set
	End Property

	Public Property Revision As String
		Get
			Return mRevision
		End Get
		Set
			mRevision = value
		End Set
	End Property

	Public Property ReviewDate As String
		Get
			Return mReviewDate
		End Get
		Set
			mReviewDate = value
		End Set
	End Property
	
	
	
	Public Property BuildUid As String
		Get
			Return mBuildUid
		End Get
		Set
			mBuildUid = value
		End Set
	End Property
	
    Property OtherDetails As OtherDefinitionDetails
        Get
            Return mOtherDetails
        End Get
         Set(ByVal value As OtherDefinitionDetails)
            mOtherDetails = value
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

    ReadOnly Property ADL_Version() As String
        Get
            Return mADL_Version
        End Get
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
    
    Sub FillTable(table As DataTable)
        For Each entry As DictionaryEntry In mOtherDetails.HashDetails
            Dim detailkey As String = entry.Key
            Dim detailItem As String = entry.Value

            If (detailkey <> CurrentContactKey) And _
                (detailkey <> OriginalPublisherKey) And _
                (detailkey <> OriginalNamespaceKey) And _
                (detailkey <> CustodianorganisationKey) And _
                (detailkey <> CustodianNamespaceKey) And _
                (detailkey <> LicenceKey) And _
                (detailkey <> ReferencesKey) And _
                (detailkey <> ReviewDateKey) And _
                (detailkey <> BuildUidKey) And _
                (detailkey <> RevisionKey) And _
                (detailkey <> AM.ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID) Then
                Dim row As DataRow = table.NewRow
                row(0) = detailkey
                row(1) = detailItem
                table.Rows.Add(row)
            End If
        Next
    End Sub

End Class
