Public Class ADL_TranslationDetails
    Inherits TranslationDetails

    Private mADL_Translation As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS

    Public Property ADL_Translation() As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS
        Get
            SetAuthorValue("name", Me.AuthorName)
            SetAuthorValue("email", Me.AuthorEmail)
            SetAuthorValue("organisation", Me.AuthorOrganisation)
            'If Me.Accreditation <> "" Then
            '    mADL_Translation.set_accreditation(openehr.base.kernel.Create.STRING.make_from_cil(Me.Accreditation))
            'End If
            Return mADL_Translation
        End Get
        Set(ByVal value As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS)
            mADL_Translation = value
            Me.Language = value.language.code_string.to_cil
            If Not value.accreditation Is Nothing Then
                Me.Accreditation = value.accreditation.to_cil
            End If
            'get author details
            Dim s As openehr.base.kernel.STRING
            s = openehr.base.kernel.Create.STRING.make_from_cil("name")
            If value.author.has(s) Then
                Me.AuthorName = CType(value.author.item(s), openehr.base.kernel.STRING).to_cil
            End If
            s = openehr.base.kernel.Create.STRING.make_from_cil("email")
            If value.author.has(s) Then
                Me.AuthorEmail = CType(value.author.item(s), openehr.base.kernel.STRING).to_cil
            End If
            s = openehr.base.kernel.Create.STRING.make_from_cil("organisation")
            If value.author.has(s) Then
                Me.AuthorOrganisation = CType(value.author.item(s), openehr.base.kernel.STRING).to_cil
            End If
        End Set
    End Property

    Private Sub SetAuthorValue(ByVal a_key As String, ByVal a_value As String)
        If a_key <> "" Then
            Dim adlKey As openehr.base.kernel.STRING
            Dim adlValue As openehr.base.kernel.STRING

            adlKey = openehr.base.kernel.Create.STRING.make_from_cil(a_key)
            adlValue = openehr.base.kernel.Create.STRING.make_from_cil(a_value)

            If mADL_Translation.author.has(adlKey) Then
                mADL_Translation.author.replace(adlValue, adlKey)
            Else
                ' only add the value if it is not empty
                If a_value <> "" Then
                    mADL_Translation.author.put(adlValue, adlKey)
                End If
            End If
        End If
    End Sub

    Sub New(ByVal a_language As String)
        mADL_Translation = openehr.openehr.rm.common.resource.Create.TRANSLATION_DETAILS.make_from_language(openehr.base.kernel.Create.STRING.make_from_cil(a_language))
        Me.Language = a_language
    End Sub

    Sub New(ByVal a_translation As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS)
        ADL_Translation = a_translation
    End Sub

    Sub New(ByVal a_translation As TranslationDetails)
        mADL_Translation = openehr.openehr.rm.common.resource.Create.TRANSLATION_DETAILS.make_from_language(openehr.base.kernel.Create.STRING.make_from_cil(a_translation.Language))
        Me.Language = a_translation.Language
        Me.Accreditation = a_translation.Accreditation
        Me.AuthorName = a_translation.AuthorName
        Me.AuthorEmail = a_translation.AuthorEmail
        Me.AuthorOrganisation = a_translation.AuthorOrganisation
    End Sub

End Class
