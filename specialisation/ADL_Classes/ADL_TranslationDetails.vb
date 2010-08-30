Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports XMLParser

Public Class ADL_TranslationDetails
    Inherits TranslationDetails

    Private mADL_Translation As AdlParser.TranslationDetails

    Public Property ADL_Translation() As AdlParser.TranslationDetails
        Get
            SetAuthorValue("name", AuthorName)
            SetAuthorValue("email", AuthorEmail)
            SetAuthorValue("organisation", AuthorOrganisation)

            If Accreditation <> "" Then
                mADL_Translation.SetAccreditation(Eiffel.String(Accreditation))
            End If

            Return mADL_Translation
        End Get
        Set(ByVal value As AdlParser.TranslationDetails)
            mADL_Translation = value
            Language = value.Language.CodeString.ToCil

            If Not value.Accreditation Is Nothing Then
                Accreditation = value.Accreditation.ToCil
            End If

            'get author details
            Dim s As EiffelKernel.String_8
            s = Eiffel.String("name")

            If value.Author.Has(s) Then
                AuthorName = CType(value.Author.At(s), EiffelKernel.String_8).ToCil
            End If

            s = Eiffel.String("email")

            If value.Author.Has(s) Then
                AuthorEmail = CType(value.Author.At(s), EiffelKernel.String_8).ToCil
            End If

            s = Eiffel.String("organisation")

            If value.Author.Has(s) Then
                AuthorOrganisation = CType(value.Author.At(s), EiffelKernel.String_8).ToCil
            End If
        End Set
    End Property

    Private Sub SetAuthorValue(ByVal a_key As String, ByVal a_value As String)
        If a_key <> "" Then
            Dim adlKey As EiffelKernel.String_8
            Dim adlValue As EiffelKernel.String_8

            adlKey = Eiffel.String(a_key)
            adlValue = Eiffel.String(a_value)

            If mADL_Translation.Author.Has(adlKey) Then
                mADL_Translation.Author.Replace(adlValue, adlKey)
            ElseIf a_value <> "" Then
                mADL_Translation.Author.Put(adlValue, adlKey)
            End If
        End If
    End Sub

    Sub New(ByVal a_language As String)
        mADL_Translation = AdlParser.Create.TranslationDetails.MakeFromLanguage(Eiffel.String(a_language))
        Language = a_language
    End Sub

    Sub New(ByVal a_translation As AdlParser.TranslationDetails)
        ADL_Translation = a_translation
    End Sub

    Sub New(ByVal a_translation As TranslationDetails)
        mADL_Translation = AdlParser.Create.TranslationDetails.MakeFromLanguage(Eiffel.String(a_translation.Language))
        Language = a_translation.Language
        Accreditation = a_translation.Accreditation
        AuthorName = a_translation.AuthorName
        AuthorEmail = a_translation.AuthorEmail
        AuthorOrganisation = a_translation.AuthorOrganisation
    End Sub

End Class
