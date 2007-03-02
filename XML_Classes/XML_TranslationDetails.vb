Public Class XML_TranslationDetails
    Inherits TranslationDetails

    Private mXmlTranslation As XMLParser.TRANSLATION_DETAILS

    Public Property XmlTranslation() As XMLParser.TRANSLATION_DETAILS
        Get
            SetAuthorValue("name", Me.AuthorName)
            SetAuthorValue("email", Me.AuthorEmail)
            SetAuthorValue("organisation", Me.AuthorOrganisation)
            mXmlTranslation.accreditation = Me.Accreditation
            Return mXmlTranslation
        End Get
        Set(ByVal value As XMLParser.TRANSLATION_DETAILS)
            mXmlTranslation = value
            Me.Language = value.language.code_string
            Me.Accreditation = value.accreditation

            For Each di As XMLParser.dictionaryItem In value.author
                Select Case di.key.ToLowerInvariant()
                    Case "name"
                        Me.AuthorName = di.value
                    Case "email"
                        Me.AuthorEmail = di.value
                    Case "organisation"
                        Me.AuthorOrganisation = di.value
                    Case Else
                        Debug.Assert(False, "Irregular attribute")
                End Select
            Next
        End Set
    End Property

    Private Sub SetAuthorValue(ByVal a_key As String, ByVal a_value As String)
        Dim di As XMLParser.dictionaryItem
        Dim i As Integer

        If a_key.ToLowerInvariant = "name" And a_value = "" Then
            a_value = Filemanager.GetOpenEhrTerm(253, "Unknown", Me.Language)
        End If

        If a_value <> "" Then
            If Not mXmlTranslation.author Is Nothing Then
                For Each di In mXmlTranslation.author
                    If di.key.ToLowerInvariant = a_key.ToLowerInvariant Then
                        di.value = a_value
                        Return
                    End If
                Next
                i = mXmlTranslation.author.Length
                Array.Resize(mXmlTranslation.author, i + 1)
            Else
                i = 0
                mXmlTranslation.author = Array.CreateInstance(GetType(XMLParser.dictionaryItem), i + 1)
            End If

            di = New XMLParser.dictionaryItem
            di.key = a_key
            di.value = a_value

            mXmlTranslation.author(i) = di
        End If
    End Sub

    Sub New(ByVal a_language As String)
        Dim xmlCodePhrase As New XMLParser.CODE_PHRASE

        xmlCodePhrase.code_string = a_language
        xmlCodePhrase.terminology_id = OceanArchetypeEditor.DefaultLanguageCodeSet

        mXmlTranslation = New XMLParser.TRANSLATION_DETAILS
        mXmlTranslation.language = xmlCodePhrase

        Me.Language = a_language

        Me.AuthorName = OceanArchetypeEditor.Instance.Options.UserName
        Me.AuthorOrganisation = OceanArchetypeEditor.Instance.Options.UserOrganisation
        Me.AuthorEmail = OceanArchetypeEditor.Instance.Options.UserEmail

    End Sub

    Sub New(ByVal a_translation As TranslationDetails)
        Dim xmlCodePhrase As New XMLParser.CODE_PHRASE

        xmlCodePhrase.code_string = a_translation.Language
        xmlCodePhrase.terminology_id = OceanArchetypeEditor.DefaultLanguageCodeSet

        mXmlTranslation = New XMLParser.TRANSLATION_DETAILS
        mXmlTranslation.language = xmlCodePhrase

        Me.Language = a_translation.Language
        Me.AuthorName = a_translation.AuthorName
        Me.AuthorOrganisation = a_translation.AuthorOrganisation
        Me.AuthorEmail = a_translation.AuthorEmail
        Me.Accreditation = a_translation.Accreditation

    End Sub

    Sub New(ByVal a_translation As XMLParser.TRANSLATION_DETAILS)
        Me.XmlTranslation = a_translation
    End Sub

End Class
