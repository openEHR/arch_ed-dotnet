Public Class AuditDetails
    Protected mCommitter As String
    Protected mCommitterOrganisation As String
    Protected mTimeCommitted As DateTime
    Protected mRevision As String = "1.0"
    Protected mReason As String
    Protected mChangeType As String

    Property Committer() As String
        Get
            Return mCommitter
        End Get
        Set(ByVal Value As String)
            mCommitter = Value
        End Set
    End Property
    Property CommitterOrganisation() As String
        Get
            Return mCommitterOrganisation
        End Get
        Set(ByVal Value As String)
            mCommitterOrganisation = Value
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
    Property Revision() As String
        Get
            Return mRevision
        End Get
        Set(ByVal Value As String)
            mRevision = Value
        End Set
    End Property
    Property Reason() As String
        Get
            Return mReason
        End Get
        Set(ByVal Value As String)
            mReason = Value
        End Set
    End Property
    Property ChangeType() As String
        Get
            Return mChangeType
        End Get
        Set(ByVal Value As String)
            mChangeType = Value
        End Set
    End Property
End Class
