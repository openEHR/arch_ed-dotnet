Imports EiffelKernel = EiffelSoftware.Library.Base.kernel
Imports XMLParser

Public Class ADL_ArchetypeDetails
    Inherits ArchetypeDetails

    Private mAdlDescription As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION

    Public Overrides Sub AddOrReplace(ByVal language As String, ByVal detail As ArchetypeDescriptionItem)
        Dim eiffelLanguageString As EiffelKernel.string.STRING_8 = Eiffel.String(language)

        If HasDetailInLanguage(language) Then
            mAdlDescription.remove_detail(eiffelLanguageString)
        End If

        Dim d As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM
        d = openehr.openehr.rm.common.resource.Create.RESOURCE_DESCRIPTION_ITEM.make_from_language(eiffelLanguageString, Eiffel.String(detail.Purpose))

        For Each s As String In detail.KeyWords
            d.add_keyword(Eiffel.String(s))
        Next

        d.set_misuse(Eiffel.String(detail.MisUse))
        d.set_use(Eiffel.String(detail.Use))
        d.set_purpose(Eiffel.String(detail.Purpose))

        If Not detail.Copyright Is Nothing Then
            d.set_copyright(Eiffel.String(detail.Copyright))
        End If

        mAdlDescription.add_detail(d)
    End Sub

    Public Overrides Function DetailInLanguage(ByVal language As String) As ArchetypeDescriptionItem
        Dim result As New ArchetypeDescriptionItem(language)

        If HasDetailInLanguage(language) Then
            Dim d As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM = mAdlDescription.detail_for_language(Eiffel.String(language))

            If Not d Is Nothing Then
                If Not d.misuse Is Nothing Then
                    result.MisUse = d.misuse.to_cil
                Else
                    result.MisUse = ""
                End If

                If Not d.use Is Nothing Then
                    result.Use = d.use.to_cil
                Else
                    result.Use = ""
                End If

                If Not d.purpose Is Nothing Then
                    result.Purpose = d.purpose.to_cil
                Else
                    result.Purpose = ""
                End If

                If Not d.keywords Is Nothing Then
                    For i As Integer = 1 To d.keywords.count
                        result.KeyWords.Add(d.keywords.i_th(i).to_cil)
                    Next
                End If

                If Not d.copyright Is Nothing Then
                    result.Copyright = d.copyright.to_cil
                End If
            End If
        End If

        Return result
    End Function

    Public Overrides Function HasDetailInLanguage(ByVal language As String) As Boolean
        Return mAdlDescription.details.has(Eiffel.String(language))
    End Function

    Public Overrides ReadOnly Property Count() As Integer
        Get
            Return mAdlDescription.details.count
        End Get
    End Property

    Sub New(ByVal description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION)
        mAdlDescription = description
    End Sub

End Class
