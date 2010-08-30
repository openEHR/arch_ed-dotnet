Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports XMLParser

Public Class ADL_ArchetypeDetails
    Inherits ArchetypeDetails

    Private mADL_Description As AdlParser.ResourceDescription

    Public Overrides Sub AddOrReplace(ByVal a_language As String, ByVal a_detail As ArchetypeDescriptionItem)
        Dim eiffelLanguageString As EiffelKernel.String_8 = Eiffel.String(a_language)

        If HasDetailInLanguage(a_language) Then
            mADL_Description.Details.Remove(eiffelLanguageString)
        End If

        Dim EIF_detail As AdlParser.ResourceDescriptionItem = AdlParser.Create.ResourceDescriptionItem.MakeFromLanguage(eiffelLanguageString, Eiffel.String(a_detail.Purpose))

        For Each s As String In a_detail.KeyWords
            EIF_detail.AddKeyword(Eiffel.String(s))
        Next

        EIF_detail.SetMisuse(Eiffel.String(a_detail.MisUse))
        EIF_detail.SetUse(Eiffel.String(a_detail.Use))
        EIF_detail.SetPurpose(Eiffel.String(a_detail.Purpose))

        If Not a_detail.Copyright Is Nothing Then
            EIF_detail.SetCopyright(Eiffel.String(a_detail.Copyright))
        End If

        mADL_Description.AddDetail(EIF_detail)
    End Sub

    Public Overrides Function DetailInLanguage(ByVal a_language As String) As ArchetypeDescriptionItem
        Dim result As New ArchetypeDescriptionItem(a_language)

        If HasDetailInLanguage(a_language) Then
            Dim EIF_detail As AdlParser.ResourceDescriptionItem = mADL_Description.DetailForLanguage(Eiffel.String(a_language))

            If Not EIF_detail Is Nothing Then
                If Not EIF_detail.Misuse Is Nothing Then
                    result.MisUse = EIF_detail.Misuse.ToCil
                Else
                    result.MisUse = ""
                End If

                If Not EIF_detail.Use Is Nothing Then
                    result.Use = EIF_detail.Use.ToCil
                Else
                    result.Use = ""
                End If

                If Not EIF_detail.Purpose Is Nothing Then
                    result.Purpose = EIF_detail.Purpose.ToCil
                Else
                    result.Purpose = ""
                End If

                If Not EIF_detail.Keywords Is Nothing Then
                    For i As Integer = 1 To EIF_detail.Keywords.Count
                        result.KeyWords.Add(EIF_detail.Keywords.ITh(i).ToCil)
                    Next
                End If

                If Not EIF_detail.Copyright Is Nothing Then
                    result.Copyright = EIF_detail.Copyright.ToCil
                End If
            End If
        End If

        Return result
    End Function

    Public Overrides Function HasDetailInLanguage(ByVal a_language As String) As Boolean
        Return mADL_Description.Details.Has(Eiffel.String(a_language))
    End Function

    Sub New(ByVal a_description As AdlParser.ResourceDescription)
        mADL_Description = a_description
    End Sub

End Class
