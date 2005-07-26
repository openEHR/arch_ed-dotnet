Public Class OtherDefinitionDetails
    Private mOtherDetails As New Hashtable
    Sub AddOtherDetail(ByVal key As String, ByVal value As String)
        If mOtherDetails.ContainsKey(key) Then
            mOtherDetails.Item(key) = value
        Else
            mOtherDetails.Add(key, value)
        End If
    End Sub
    Sub RemoveOtherDetail(ByVal key As String)
        Debug.Assert(mOtherDetails.ContainsKey(key))
        If mOtherDetails.ContainsKey(key) Then
            mOtherDetails.Remove(key)
        End If
    End Sub
    Function hasOtherDetail(ByVal key As String) As Boolean
        Return mOtherDetails.ContainsKey(key)
    End Function
    ReadOnly Property OtherDetails() As String(,)
        Get
            Dim i As Integer = 0
            Dim s(mOtherDetails.Count - 1, 1) As String

            For Each key As Object In mOtherDetails.Keys
                s(i, 0) = CType(key, String)
                s(i, 1) = mOtherDetails.Item(key)
                i += 1
            Next
            Return s
        End Get
    End Property
End Class
