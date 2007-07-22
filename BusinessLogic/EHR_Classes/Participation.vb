Public Class Participation
    Private mFunction As New RmTerm("")

    Public Property [Function]() As RmTerm
        Get
            Return mFunction
        End Get
        Set(ByVal value As RmTerm)
            mFunction = value
        End Set
    End Property

    Private mDateTimeMandatory As Boolean

    Public Property DateTimeMandatory() As Boolean
        Get
            Return mDateTimeMandatory
        End Get
        Set(ByVal value As Boolean)
            mDateTimeMandatory = value
        End Set
    End Property

    Private cpRestrictedRoles As CodePhrase

    Public Property RestrictedRoles() As CodePhrase
        Get
            If cpRestrictedRoles Is Nothing Then
                cpRestrictedRoles = New CodePhrase()
            End If
            Return cpRestrictedRoles
        End Get
        Set(ByVal value As CodePhrase)
            cpRestrictedRoles = value
        End Set
    End Property


    Public Function HasRestrictedRoles() As Boolean
        Return Not (cpRestrictedRoles Is Nothing OrElse cpRestrictedRoles.Codes.Count = 0)
    End Function


End Class
