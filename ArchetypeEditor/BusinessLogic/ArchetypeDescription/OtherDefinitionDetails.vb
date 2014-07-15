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
            Dim result(mOtherDetails.Count - 1, 1) As String
            Dim i As Integer = 0

            For Each key As Object In mOtherDetails.Keys
                result(i, 0) = CType(key, String)
                result(i, 1) = mOtherDetails.Item(key)
                i += 1
            Next

            Return result
        End Get
    End Property
    
    ReadOnly Property HashDetails As Hashtable
    	Get
    		Return mOtherDetails
    	End Get
    End Property

End Class
