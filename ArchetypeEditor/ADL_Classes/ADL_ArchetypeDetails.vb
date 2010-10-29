Imports EiffelKernel = EiffelSoftware.Library.Base.kernel
Imports XMLParser

Public Class ADL_ArchetypeDetails
    Inherits ArchetypeDetails

    Private mADL_Description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION

    Public Overrides Sub AddOrReplace(ByVal a_language As String, ByVal a_detail As ArchetypeDescriptionItem)
        Dim eiffelLanguageString As EiffelKernel.STRING_8 = Eiffel.String(a_language)

        If HasDetailInLanguage(a_language) Then
            mADL_Description.details.remove(eiffelLanguageString)
        End If

        Dim detail As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM

        detail = openehr.openehr.rm.common.resource.Create.RESOURCE_DESCRIPTION_ITEM.make_from_language(eiffelLanguageString, Eiffel.String(a_detail.Purpose))

        For Each s As String In a_detail.KeyWords
            detail.add_keyword(Eiffel.String(s))
        Next

        detail.set_misuse(Eiffel.String(a_detail.MisUse))
        detail.set_use(Eiffel.String(a_detail.Use))
        detail.set_purpose(Eiffel.String(a_detail.Purpose))

        If Not a_detail.Copyright Is Nothing Then
            detail.set_copyright(Eiffel.String(a_detail.Copyright))
        End If

        mADL_Description.add_detail(detail)
    End Sub

    Public Overrides Function DetailInLanguage(ByVal a_language As String) As ArchetypeDescriptionItem
        Dim result As New ArchetypeDescriptionItem(a_language)

        If HasDetailInLanguage(a_language) Then
            Dim detail As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM = mADL_Description.detail_for_language(Eiffel.String(a_language))

            If Not detail Is Nothing Then
                If Not detail.misuse Is Nothing Then
                    result.MisUse = detail.misuse.to_cil
                Else
                    result.MisUse = ""
                End If

                If Not detail.use Is Nothing Then
                    result.Use = detail.use.to_cil
                Else
                    result.Use = ""
                End If

                If Not detail.purpose Is Nothing Then
                    result.Purpose = detail.purpose.to_cil
                Else
                    result.Purpose = ""
                End If

                If Not detail.keywords Is Nothing Then
                    For i As Integer = 1 To detail.keywords.count
                        result.KeyWords.Add(detail.keywords.i_th(i).to_cil)
                    Next
                End If

                If Not detail.copyright Is Nothing Then
                    result.Copyright = detail.copyright.to_cil
                End If
            End If
        End If

        Return result
    End Function

    Public Overrides Function HasDetailInLanguage(ByVal a_language As String) As Boolean
        Return mADL_Description.details.has(Eiffel.String(a_language))
    End Function

    Sub New(ByVal a_description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION)
        mADL_Description = a_description
    End Sub

End Class
