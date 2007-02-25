Public Class ADL_ArchetypeDetails
    Inherits ArchetypeDetails

    Private mADL_Description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION

    Public Overrides Sub AddOrReplace(ByVal a_language As String, ByVal a_detail As ArchetypeDescriptionItem)
        Dim eiffelLanguageString As openehr.base.kernel.STRING

        eiffelLanguageString = openehr.base.kernel.Create.STRING.make_from_cil(a_language)

        
        If Me.HasDetailInLanguage(a_language) Then
            mADL_Description.details.remove(eiffelLanguageString)
        End If

        Dim EIF_detail As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM

        EIF_detail = openehr.openehr.rm.common.resource.Create.RESOURCE_DESCRIPTION_ITEM.make_from_language(eiffelLanguageString, openehr.base.kernel.Create.STRING.make_from_cil(a_detail.Purpose))

        For Each s As String In a_detail.KeyWords
            EIF_detail.add_keyword(openehr.base.kernel.Create.STRING.make_from_cil(s))
        Next

        EIF_detail.set_misuse(openehr.base.kernel.Create.STRING.make_from_cil(a_detail.MisUse))
        EIF_detail.set_use(openehr.base.kernel.Create.STRING.make_from_cil(a_detail.Use))
        EIF_detail.set_purpose(openehr.base.kernel.Create.STRING.make_from_cil(a_detail.Purpose))
        mADL_Description.add_detail(EIF_detail)
    End Sub

    Public Overrides Function DetailInLanguage(ByVal a_language As String) As ArchetypeDescriptionItem
        Dim archDescriptDetail As New ArchetypeDescriptionItem(a_language)

        If HasDetailInLanguage(a_language) Then
            Dim EIF_detail As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM
            'EIF_detail = mADL_Details.item(openehr.base.kernel.Create.STRING.make_from_cil(a_language))
            EIF_detail = mADL_Description.detail_for_language(openehr.base.kernel.Create.STRING.make_from_cil(a_language))
            If Not EIF_detail Is Nothing Then
                If Not EIF_detail.misuse Is Nothing Then
                    archDescriptDetail.MisUse = EIF_detail.misuse.to_cil
                Else
                    archDescriptDetail.MisUse = ""
                End If
                If Not EIF_detail.use Is Nothing Then
                    archDescriptDetail.Use = EIF_detail.use.to_cil
                Else
                    archDescriptDetail.Use = ""
                End If
                If Not EIF_detail.purpose Is Nothing Then
                    archDescriptDetail.Purpose = EIF_detail.purpose.to_cil
                Else
                    archDescriptDetail.Purpose = ""
                End If

                If Not EIF_detail.keywords Is Nothing Then
                    For i As Integer = 1 To EIF_detail.keywords.count
                        archDescriptDetail.KeyWords.Add(EIF_detail.keywords.i_th(i).to_cil)
                    Next
                End If
            End If
        End If
        Return archDescriptDetail
    End Function

    Public Overrides Function HasDetailInLanguage(ByVal a_language As String) As Boolean
        '        If mADL_Details.has(openehr.base.kernel.Create.STRING.make_from_cil(a_language)) Then
        If mADL_Description.details.has(openehr.base.kernel.Create.STRING.make_from_cil(a_language)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub New(ByVal a_description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION)
        'mADL_Details = a_description.details
        mADL_Description = a_description
    End Sub

End Class
