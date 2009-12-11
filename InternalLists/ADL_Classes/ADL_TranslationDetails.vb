Imports EiffelKernel = EiffelSoftware.Library.Base.kernel

Public Class ADL_TranslationDetails
    Inherits TranslationDetails

    Private mADL_Translation As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS

    Public Property ADL_Translation() As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS
        Get
            SetAuthorValue("name", AuthorName)
            SetAuthorValue("email", AuthorEmail)
            SetAuthorValue("organisation", AuthorOrganisation)

            If Accreditation <> "" Then
                mADL_Translation.set_accreditation(EiffelKernel.Create.STRING_8.make_from_cil(Accreditation))
            End If

            Return mADL_Translation
        End Get
        Set(ByVal value As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS)
            mADL_Translation = value
            Language = value.language.code_string.to_cil

            If Not value.accreditation Is Nothing Then
                Accreditation = value.accreditation.to_cil
            End If

            'get author details
            Dim s As EiffelKernel.STRING_8
            s = EiffelKernel.Create.STRING_8.make_from_cil("name")

            If value.author.has(s) Then
                AuthorName = CType(value.author.item(s), EiffelKernel.STRING_8).to_cil
            End If

            s = EiffelKernel.Create.STRING_8.make_from_cil("email")

            If value.author.has(s) Then
                AuthorEmail = CType(value.author.item(s), EiffelKernel.STRING_8).to_cil
            End If

            s = EiffelKernel.Create.STRING_8.make_from_cil("organisation")

            If value.author.has(s) Then
                AuthorOrganisation = CType(value.author.item(s), EiffelKernel.STRING_8).to_cil
            End If
        End Set
    End Property

    Private Sub SetAuthorValue(ByVal a_key As String, ByVal a_value As String)
        If a_key <> "" Then
            Dim adlKey As EiffelKernel.STRING_8
            Dim adlValue As EiffelKernel.STRING_8

            adlKey = EiffelKernel.Create.STRING_8.make_from_cil(a_key)
            adlValue = EiffelKernel.Create.STRING_8.make_from_cil(a_value)

            If mADL_Translation.author.has(adlKey) Then
                mADL_Translation.author.replace(adlValue, adlKey)
            ElseIf a_value <> "" Then
                mADL_Translation.author.put(adlValue, adlKey)
            End If
        End If
    End Sub

    Sub New(ByVal a_language As String)
        mADL_Translation = openehr.openehr.rm.common.resource.Create.TRANSLATION_DETAILS.make_from_language(EiffelKernel.Create.STRING_8.make_from_cil(a_language))
        Language = a_language
    End Sub

    Sub New(ByVal a_translation As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS)
        ADL_Translation = a_translation
    End Sub

    Sub New(ByVal a_translation As TranslationDetails)
        mADL_Translation = openehr.openehr.rm.common.resource.Create.TRANSLATION_DETAILS.make_from_language(EiffelKernel.Create.STRING_8.make_from_cil(a_translation.Language))
        Language = a_translation.Language
        Accreditation = a_translation.Accreditation
        AuthorName = a_translation.AuthorName
        AuthorEmail = a_translation.AuthorEmail
        AuthorOrganisation = a_translation.AuthorOrganisation
    End Sub

End Class
