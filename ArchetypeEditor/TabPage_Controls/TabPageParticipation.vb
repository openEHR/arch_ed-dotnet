Public Class TabPageParticipation

    Private Sub chkProvider_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProvider.CheckedChanged
        If Not Filemanager.Master.FileLoading Then
            Filemanager.Master.FileEdited = True
        End If
    End Sub
End Class
