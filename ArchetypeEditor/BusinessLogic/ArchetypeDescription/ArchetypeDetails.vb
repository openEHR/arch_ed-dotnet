Public Class ArchetypeDetails
    Protected mDetails As New Hashtable

    Public Overridable Sub AddOrReplace(ByVal language As String, ByVal detail As ArchetypeDescriptionItem)
        If HasDetailInLanguage(language) Then
            mDetails.Remove(language)
        End If

        mDetails.Add(language, detail)
    End Sub

    Public Overridable Function DetailInLanguage(ByVal language As String) As ArchetypeDescriptionItem
        Dim result As New ArchetypeDescriptionItem(language)

        If mDetails.ContainsKey(language) Then
            result = mDetails.Item(language)
        End If

        Return result
    End Function

    Public Overridable Function HasDetailInLanguage(ByVal language As String) As Boolean
        Return mDetails.ContainsKey(language)
    End Function

    Public Overridable ReadOnly Property Count() As Integer
        Get
            Return mDetails.Count
        End Get
    End Property

End Class
