Option Explicit On

Public Class XML_TranslationDetails
    Inherits TranslationDetails

    Private mXmlTranslation As XMLParser.TRANSLATION_DETAILS

    Public Property XmlTranslation() As XMLParser.TRANSLATION_DETAILS
        Get
            SetAuthorValue("name", Me.AuthorName)
            SetAuthorValue("email", Me.AuthorEmail)
            SetAuthorValue("organisation", Me.AuthorOrganisation)
            ' HKF: 11 Dec 08
            If Not String.IsNullOrEmpty(Me.Accreditation) Then
                mXmlTranslation.accreditation = Me.Accreditation
            End If

            Return mXmlTranslation
        End Get
        Set(ByVal value As XMLParser.TRANSLATION_DETAILS)
            If value Is Nothing Then Throw New ArgumentException("translation details must not be null")
            If value.author Is Nothing Then Throw New ArgumentException("translation details author must not be null")
            If value.language Is Nothing Then Throw New ArgumentException("translation details language must not be null")

            mXmlTranslation = value
            Me.Language = value.language.code_string
            If value.accreditation IsNot Nothing Then
                Me.Accreditation = value.accreditation
            End If

            'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            'For Each di As XMLParser.dictionaryItem In value.author
            For Each di As XMLParser.StringDictionaryItem In value.author
                Select Case di.id.ToLowerInvariant()
                    Case "name"
                        Me.AuthorName = di.Value
                    Case "email"
                        Me.AuthorEmail = di.Value
                    Case "organisation"
                        Me.AuthorOrganisation = di.Value
                    Case Else
                        Debug.Assert(False, "Irregular attribute")
                End Select
            Next
        End Set
    End Property

    Private Sub SetAuthorValue(ByVal a_key As String, ByVal a_value As String)
        'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        'Dim di As XMLParser.dictionaryItem
        Dim di As XMLParser.StringDictionaryItem
        Dim i As Integer

        If a_key.ToLowerInvariant = "name" And a_value = "" Then
            a_value = Filemanager.GetOpenEhrTerm(253, "Unknown", Me.Language)
        End If

        If a_value <> "" Then
            If Not mXmlTranslation.author Is Nothing Then
                For Each di In mXmlTranslation.author
                    If di.id.ToLowerInvariant = a_key.ToLowerInvariant Then
                        di.Value = a_value
                        Return
                    End If
                Next
                i = mXmlTranslation.author.Length
                Array.Resize(mXmlTranslation.author, i + 1)
            Else
                i = 0
                mXmlTranslation.author = Array.CreateInstance(GetType(XMLParser.StringDictionaryItem), i + 1)
            End If

            di = New XMLParser.StringDictionaryItem
            di.id = a_key
            di.Value = a_value

            mXmlTranslation.author(i) = di
        End If
    End Sub

    Sub New(ByVal a_language As String)
        Dim xmlCodePhrase As New XMLParser.CODE_PHRASE
        xmlCodePhrase.code_string = a_language

        If xmlCodePhrase.terminology_id Is Nothing Then
            xmlCodePhrase.terminology_id = New XMLParser.TERMINOLOGY_ID
        End If

        xmlCodePhrase.terminology_id.value = Main.Instance.DefaultLanguageCodeSet
        mXmlTranslation = New XMLParser.TRANSLATION_DETAILS
        mXmlTranslation.language = xmlCodePhrase
        Language = a_language
        AuthorName = Main.Instance.Options.UserName
        AuthorOrganisation = Main.Instance.Options.UserOrganisation
        AuthorEmail = Main.Instance.Options.UserEmail
    End Sub

    Sub New(ByVal a_translation As TranslationDetails)
        Dim xmlCodePhrase As New XMLParser.CODE_PHRASE
        xmlCodePhrase.code_string = a_translation.Language

        If xmlCodePhrase.terminology_id Is Nothing Then
            xmlCodePhrase.terminology_id = New XMLParser.TERMINOLOGY_ID
        End If

        xmlCodePhrase.terminology_id.value = Main.Instance.DefaultLanguageCodeSet
        mXmlTranslation = New XMLParser.TRANSLATION_DETAILS
        mXmlTranslation.language = xmlCodePhrase
        Language = a_translation.Language
        AuthorName = a_translation.AuthorName
        AuthorOrganisation = a_translation.AuthorOrganisation
        AuthorEmail = a_translation.AuthorEmail
        Accreditation = a_translation.Accreditation
    End Sub

    Sub New(ByVal a_translation As XMLParser.TRANSLATION_DETAILS)
        XmlTranslation = a_translation
    End Sub

End Class
