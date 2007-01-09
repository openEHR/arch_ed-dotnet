Public Class ArchetypeDetails
    Inherits Collections.DictionaryBase

    Public Overridable Sub AddOrReplace(ByVal a_language As String, ByVal a_detail As ArchetypeDescriptionItem)
        If Me.HasDetailInLanguage(a_language) Then
            Me.InnerHashtable.Remove(a_language)
        End If
        Me.InnerHashtable.Add(a_language, a_detail)
    End Sub

    Public Overridable Function DetailInLanguage(ByVal a_language As String) As ArchetypeDescriptionItem
        If Me.InnerHashtable.ContainsKey(a_language) Then
            Return Me.InnerHashtable.Item(a_language)
        Else
            Return Nothing
        End If
    End Function

    Public Overridable Function HasDetailInLanguage(ByVal a_language As String) As Boolean
        If Me.InnerHashtable.ContainsKey(a_language) Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
