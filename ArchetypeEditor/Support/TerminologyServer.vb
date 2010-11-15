'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class TerminologyServer
    Private DVPhrases As DataView
    Private Terminology As New DataSet("openEHRTerminology")
    Private Languages, Territories, TerminologyIdentifiers, Concepts, Groupers, GroupedConcepts As DataTable

    Shared mInstance As TerminologyServer

    Public Shared ReadOnly Property Instance() As TerminologyServer
        Get
            If mInstance Is Nothing Then
                mInstance = New TerminologyServer
            End If

            Return mInstance
        End Get
    End Property

    Public ReadOnly Property HasCodeSet(ByVal CodeSetName As String) As Boolean
        Get
            ' string of names recognised as termsets - allows different casing to avoid problems
            ' of case changes in a multilingual environment
            Return InStr(",terminology,Terminology,Terminologies,terminologies,TERMINOLOGY,TERMINOLOGIES,language,Language,LANGUAGE,languages,Languages,LANGUAGES,", "," & CodeSetName.Trim & ",") > 0
        End Get
    End Property

    Public ReadOnly Property HasGroupName(ByVal GroupName As String, ByVal language As String) As Boolean
        Get
            Dim selected_rows(), d_row As DataRow
            selected_rows = Concepts.Select("Rubric = '" & GroupName & "'")

            If selected_rows.Length > 0 Then
                Dim key(0) As Object

                For Each d_row In selected_rows
                    key(0) = d_row(0)

                    If Not Groupers.Rows.Find(key) Is Nothing Then
                        Return True
                    End If
                Next
            End If

            Return False
        End Get
    End Property

    Public Function CodeSetAsStringArray(ByVal CodeSetName As String) As String()
        'languages, countries, Media types, terminologies, compression algorithms, integrity check algorithms

        Select Case CodeSetName
            Case "Concepts", "Concepts", "Terminologies", "terminologies", "CONCEPTS", "TERMINOLOGIES"
                Dim i, n_rows As Integer

                n_rows = TerminologyIdentifiers.Rows.Count - 1
                Dim CodeSetArray(n_rows) As String

                For i = 0 To n_rows
                    CodeSetArray(i) = TerminologyIdentifiers.Rows(i).Item(0)
                Next

                Return CodeSetArray

            Case "language", "Language", "LANGUAGE", "languages", "Languages", "LANGUAGES"
                Dim CodeSetArray(Languages.Rows.Count - 1) As String

                Languages.Rows.CopyTo(CodeSetArray, 0)
                Return CodeSetArray

            Case Else
                Debug.Assert(False)
                Return Nothing
        End Select
    End Function

    Public Function CodeSetAsDataRow(ByVal CodeSetName As String) As DataRow()
        'languages, countries, Media types, terminologies, compression algorithms, integrity check algorithms

        Select Case CodeSetName.ToLowerInvariant()
            Case "concepts", "terminologies"
                Return TerminologyIdentifiers.Select()

            Case "languages", "language"
                Return Languages.Select()

            Case Else
                Debug.Assert(False)
                Return Nothing
        End Select
    End Function

    Public Function CodeSetItemDescription(ByVal CodeSetName As String, ByVal Code As String) As String
        Dim result As String = Nothing

        Select Case CodeSetName.ToLowerInvariant()
            Case "concept", "terminologies"
                Dim key(0) As Object
                key(0) = Code
                Dim row As DataRow = TerminologyIdentifiers.Rows.Find(key)

                If Not row Is Nothing Then
                    result = row.Item(1)
                End If

            Case "language", "languages"
                Dim selected_rows As DataRow()
                selected_rows = Languages.Select("Code = '" & Code & "'")

                If selected_rows.Length > 0 Then
                    result = selected_rows(0).Item("Description")
                End If
        End Select

        Return result
    End Function

    Public Function RubricForCode(ByVal Code As Integer, ByVal language As String) As String
        Dim keys(1) As Object
        Dim selected_row As DataRow

        Debug.Assert(Not language Is Nothing)
        Debug.Assert(Not language = "?")

        keys(0) = language
        keys(1) = Code
        selected_row = Concepts.Rows.Find(keys)

        If selected_row Is Nothing Then
            Dim i As Integer
            ' see if there is a standard version of the language and return that value if there is
            i = language.IndexOf("-")

            If i > -1 Then
                keys(0) = language.Substring(0, i)
                selected_row = Concepts.Rows.Find(keys)

                If Not selected_row Is Nothing Then
                    Return selected_row(2)
                End If
            End If
        Else
            Return selected_row(2)
        End If

        'If nothing is found then return the english text
        If language <> "en" Then
            Return RubricForCode(Code, "en")
        Else
            Return "?"
        End If
    End Function

    Public Function CodesForGroupName(ByVal GrouperName As String, Optional ByVal language As String = "") As DataRow()
        Dim selected_rows, selected_rows1 As DataRow()
        
        selected_rows1 = Groupers.Select("Label = '" & GrouperName & "'")

        If selected_rows1.Length = 0 Then Return Nothing

        If language = "" Then
            language = Main.Instance.DefaultLanguageCode
        End If

        selected_rows = GroupedConcepts.Select("GrouperID = " & selected_rows1(0).Item(1))

        Return GetConcepts(selected_rows, language)
    End Function

    Public Function CodesForGrouperID(ByVal GroupID As Integer, Optional ByVal language As String = "") As DataRow()
        Dim selected_concepts As DataRow()

        ' 1 = subject of care
        ' 2 = date tume constraints

        selected_concepts = GroupedConcepts.Select("GrouperID = " & GroupID.ToString)

        If selected_concepts.Length = 0 Then Return Nothing

        If language = "" Then
            'language = Default_language
            language = Main.Instance.DefaultLanguageCode
        End If

        Return GetConcepts(selected_concepts, language)
    End Function

    Private Function GetConcepts(ByVal data_rows As DataRow(), ByVal language As String) As DataRow()
        Dim selected_terms As DataRow()
        Dim filterString, f, g As String
        Dim i As Integer

        g = ""
        f = "Language = '" & language & "' AND (ConceptID = "

        For i = 0 To data_rows.Length - 1
            g &= data_rows(i).Item(1).ToString
            If i < data_rows.Length - 1 Then
                g = g & " OR ConceptID = "
            End If
        Next
        g &= ")"

        filterString = f & g

        selected_terms = Concepts.Select(filterString)

        If selected_terms.Length > 0 AndAlso (selected_terms.Length < data_rows.Length) Then
            'differential translation or not completely translated
            Dim ii As Integer
            Dim standard_language As String

            ii = language.IndexOf("-")

            If ii > -1 Then
                Dim code, last_code As String
                Dim subsequent_standard, subsequent_language As Boolean

                ' use standard language as master
                standard_language = language.Substring(0, ii)

                f = "(Language = '" & standard_language & "' OR " & "Language = '" & language & "') AND (ConceptID = "

                filterString = f & g

                selected_terms = Concepts.Select(filterString, "ConceptID ASC, Language DESC")

                f = "Language = '" & language & "' AND (ConceptID = "
                g = "Language = '" & standard_language & "' AND (ConceptID = "
                last_code = ""

                For i = 0 To selected_terms.Length - 1
                    code = selected_terms(i).Item(1).ToString
                    Debug.WriteLine(selected_terms(i).Item(0) & ", " & selected_terms(i).Item(1).ToString & ", " & selected_terms(i).Item(2))

                    If code <> last_code Then
                        'needed
                        If selected_terms(i).Item(0) = language Then
                            If subsequent_language Then  ' first one so no or
                                f &= "OR ConceptID = "
                            Else
                                subsequent_language = True
                            End If

                            f &= selected_terms(i).Item(1).ToString
                        Else
                            'standard language
                            If subsequent_standard Then  ' first one so no or
                                g &= " OR ConceptID = "
                            Else
                                subsequent_standard = True
                            End If

                            g &= selected_terms(i).Item(1).ToString
                        End If
                    End If

                    last_code = code
                Next

                filterString = "((" & f & ") OR (" & g & "))"
                selected_terms = Concepts.Select(filterString)
            End If

        ElseIf language <> "" And selected_terms.Length = 0 Then
            ' no terms in this language
            TranslateGroup(filterString, language)
            selected_terms = Concepts.Select(filterString)
        End If

        Return selected_terms
    End Function

    Private Sub TranslateGroup(ByVal FilterString As String, ByVal languageID As String)
        Dim selected_rows As DataRow()
        Dim dr, nr As DataRow

        FilterString = "Language = 'en' " & Mid(FilterString, InStr(FilterString, "AND"))
        selected_rows = Concepts.Select(FilterString)

        For Each dr In selected_rows
            nr = Concepts.NewRow
            nr(0) = languageID
            nr(1) = dr(1)
            nr(2) = "*" & dr(2) & "(en)"
            Concepts.Rows.Add(nr)
        Next
    End Sub

    Private Sub AppendTerminologiesFromLookup()
        If Main.Instance.Options.AllowTerminologyLookUp And Not Main.Instance.TerminologyLookup Is Nothing Then
            Try
                If Not Main.Instance.Options.TerminologyUrl Is Nothing Then
                    Main.Instance.TerminologyLookup.Url = Main.Instance.Options.TerminologyUrl.ToString
                End If

                For Each t As TerminologyLookup.Terminology In Main.Instance.TerminologyLookup.Terminologies
                    Dim key(0) As Object
                    key(0) = t.TerminologyId

                    If TerminologyIdentifiers.Rows.Find(key) Is Nothing Then
                        TerminologyIdentifiers.Rows.Add(t.TerminologyId, t.TerminologyId, Main.Instance.TerminologyLookup.Name, DBNull.Value)
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show("Loading " + Main.Instance.TerminologyLookup.Name + " terminologies: " + ex.Message)
            End Try
        End If
    End Sub

    Public Sub InitialiseTerminology(ByVal Document As String, ByVal Schema As String)
        'FIXME - Add validation later
        'Dim xmlR As Xml.XmlTextReader
        'Dim xmlValid As Xml.XmlValidatingReader

        Try
            Terminology.ReadXmlSchema(Schema)
            Terminology.ReadXml(Document)
        Catch ex As Exception
            MessageBox.Show("Loading terminologies: " + ex.Message)
        End Try

        Languages = Terminology.Tables("Language")
        Territories = Terminology.Tables("Territory")
        TerminologyIdentifiers = Terminology.Tables("TerminologyIdentifiers")
        Concepts = Terminology.Tables("Concept")
        Groupers = Terminology.Tables("Grouper")
        GroupedConcepts = Terminology.Tables("GroupedConcept")

        Dim KeyFields(1) As DataColumn
        KeyFields(0) = Concepts.Columns(0)
        KeyFields(1) = Concepts.Columns(1)
        Concepts.PrimaryKey = KeyFields

        ReDim KeyFields(0)
        KeyFields(0) = Territories.Columns(0)
        Territories.PrimaryKey = KeyFields
    End Sub

    Sub New()
        InitialiseTerminology(Main.Instance.Options.AssemblyPath & "\terminology\terminology.xml", Main.Instance.Options.AssemblyPath & "\terminology\terminology.xsd")

        ' set the VSB column as a primary field for searching
        Dim primarykeyfields(0) As DataColumn
        primarykeyfields(0) = TerminologyIdentifiers.Columns(0)
        TerminologyIdentifiers.PrimaryKey = primarykeyfields
        AppendTerminologiesFromLookup()
    End Sub

End Class

'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is TerminologyServer.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'
