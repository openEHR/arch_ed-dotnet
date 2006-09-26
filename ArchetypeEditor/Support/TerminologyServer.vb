'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class TerminologyServer

    Private xmlDoc As String
    Private xmlSchema As String
    Private DVPhrases As DataView
    Private initialised As Boolean
    Private Terminology As New DataSet("openEHRTerminology")

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
            If InStr(",terminology,Terminology,Terminologies,terminologies,TERMINOLOGY,TERMINOLOGIES,language,Language,LANGUAGE,languages,Languages,LANGUAGES,", "," & CodeSetName.Trim & ",") > 0 Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property HasGroupName(ByVal GroupName As String, ByVal language As String) As Boolean
        Get
            Dim selected_rows(), d_row As DataRow

            selected_rows = Terminology.Tables("Concept").Select("Rubric = '" & GroupName & "'")
            If selected_rows.Length > 0 Then
                Dim key(0) As Object
                For Each d_row In selected_rows
                    key(0) = d_row(0)
                    If Not Terminology.Tables("Grouper").Rows.Find(key) Is Nothing Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Get
    End Property


    Public Function CodeSetAsStringArray(ByVal CodeSetName As String) As String()
        'languages, countries, Media types, terminologies, compression algorithms, integrity check algorithms

        Select Case CodeSetName.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "concepts", "terminologies"
                Dim i, n_rows As Integer

                n_rows = Terminology.Tables.Item("TerminologyIdentifiers").Rows.Count - 1
                Dim CodeSetArray(n_rows) As String

                For i = 0 To n_rows
                    CodeSetArray(i) = Terminology.Tables("TerminologyIdentifiers").Rows(i).Item(0)
                Next
                Return CodeSetArray

            Case "language", "languages"
                Dim CodeSetArray(Terminology.Tables.Item("Languages").Rows.Count - 1) As String

                Terminology.Tables("Languages").Rows.CopyTo(CodeSetArray, 0)
                Return CodeSetArray

        End Select
    End Function

    Public Function CodeSetAsDataRow(ByVal CodeSetName As String) As DataRow()
        'languages, countries, Media types, terminologies, compression algorithms, integrity check algorithms

        Select Case CodeSetName.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "concepts", "terminologies"
                Dim selected_rows As DataRow()

                selected_rows = Terminology.Tables("TerminologyIdentifiers").Select()
                Return selected_rows

            Case "languages", "language"
                Dim selected_rows As DataRow()

                selected_rows = Terminology.Tables("Language").Select()
                Return selected_rows
        End Select
    End Function

    Public Function CodeSetItemDescription(ByVal CodeSetName As String, ByVal Code As String) As String
        Select Case CodeSetName.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "concept", "terminologies"
                Dim key(0) As Object
                key(0) = Code
                Try
                    Return Terminology.Tables("TerminologyIdentifiers").Rows.Find(key).Item(1)
                Catch
                    Return ""
                End Try

            Case "language", "languages"
                Dim selected_rows As DataRow()
                selected_rows = Terminology.Tables("Language").Select("Code = '" & Code & "'")
                If selected_rows.Length > 0 Then
                    Return selected_rows(0).Item("Description")
                Else
                    Return ""
                End If
        End Select

    End Function

    Public Function RubricForCode(ByVal Code As Integer, ByVal language As String) As String
        Dim keys(1) As Object
        Dim selected_row As DataRow

        Debug.Assert(Not language Is Nothing)
        Debug.Assert(Not language = "?")

        keys(0) = language
        keys(1) = Code
        selected_row = Terminology.Tables("Concept").Rows.Find(keys)
        If selected_row Is Nothing Then
            Dim i As Integer
            ' see if there is a standard version of the language and return that value if there is
            i = language.IndexOf("-")
            If i > -1 Then
                keys(0) = language.Substring(0, i)
                selected_row = Terminology.Tables("Concept").Rows.Find(keys)
                If Not selected_row Is Nothing Then
                    Return selected_row(2)
                End If
            End If
        Else
            Return selected_row(2)
        End If

        'otherwise return the English version
        If language <> "en" Then
            Return RubricForCode(Code, "en")
        Else
            Return "?"
        End If

    End Function

    Public Function CodesForGroupName(ByVal GrouperName As String, Optional ByVal language As String = "") As DataRow()
        Dim selected_rows, selected_rows1 As DataRow()
        Dim f As String
        Dim i As Integer

        selected_rows1 = Terminology.Tables("Grouper").Select("Label = '" & GrouperName & "'")

        If selected_rows1.Length = 0 Then Return Nothing

        If language = "" Then
            'language = Default_language
            language = OceanArchetypeEditor.DefaultLanguageCode
        End If

        selected_rows = Terminology.Tables("GrouperTerm").Select("GrouperID = " & selected_rows1(0).Item(1))

        Return GetConcepts(selected_rows, language)

    End Function

    Public Function CodesForGrouperID(ByVal GroupID As Integer, Optional ByVal language As String = "") As DataRow()
        Dim selected_concepts As DataRow()

        ' 1 = subject of care
        ' 2 = date tume constraints

        selected_concepts = Terminology.Tables("GroupedConcept").Select("GrouperID = " & GroupID.ToString)

        If selected_concepts.Length = 0 Then Return Nothing

        If language = "" Then
            'language = Default_language
            language = OceanArchetypeEditor.DefaultLanguageCode
        End If

        Return GetConcepts(selected_concepts, language)

    End Function

    Private Function GetConcepts(ByVal data_rows As DataRow(), ByVal language As String) As DataRow()
        Dim selected_terms As DataRow()
        Dim filterString, f, g As String
        Dim i As Integer

        f = "Language = '" & language & "' AND (ConceptID = "


        For i = 0 To data_rows.Length - 1
            g &= data_rows(i).Item(1).ToString
            If i < data_rows.Length - 1 Then
                g = g & " OR ConceptID = "
            End If
        Next
        g &= ")"

        filterString = f & g

        selected_terms = Terminology.Tables("Concept").Select(filterString)

        If selected_terms.Length > 0 AndAlso (selected_terms.Length < data_rows.Length) Then
            'differential translation or not completely translated
            Dim ii As Integer
            Dim standard_language As String

            ii = language.IndexOf("-")

            If ii > -1 Then
                Dim highestconcept As Integer
                Dim code, last_code As String
                Dim subsequent_standard, subsequent_language As Boolean
                Dim d_row, last_row As DataRow

                ' use standard language as master
                standard_language = language.Substring(0, ii)

                f = "(Language = '" & standard_language & "' OR " & "Language = '" & language & "') AND (ConceptID = "

                filterString = f & g

                selected_terms = Terminology.Tables("Concept").Select(filterString, "ConceptID ASC, Language DESC")

                f = "Language = '" & language & "' AND (ConceptID = "
                g = "Language = '" & standard_language & "' AND (ConceptID = "

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

                filterString = "(" & f & ")) OR (" & g & "))"
                selected_terms = Terminology.Tables("Concept").Select(filterString)

            End If

        ElseIf language <> "" And selected_terms.Length = 0 Then
            ' no terms in this language
            TranslateGroup(filterString, language)
            selected_terms = Terminology.Tables("Concept").Select(filterString)
        End If

        Return selected_terms

    End Function
    Private Function TranslateGroup(ByVal FilterString As String, ByVal languageID As String)
        Dim selected_rows As DataRow()
        Dim dr, nr As DataRow

        Dim i As Integer

        i = InStr(FilterString, "AND")
        FilterString = "Language = 'en' " & Mid(FilterString, i)
        selected_rows = Terminology.Tables("Concept").Select(FilterString)
        For Each dr In selected_rows
            nr = Terminology.Tables("Concept").NewRow
            nr(0) = languageID
            nr(1) = dr(1)
            nr(2) = "*" & dr(2) & "(en)"
            Terminology.Tables("Concept").Rows.Add(nr)
        Next

    End Function

    Public Function InitialiseTerminology(Optional ByVal Document As String = "", Optional ByVal Schema As String = "") As Boolean

        'FIXME - Add validation later
        'Dim xmlR As Xml.XmlTextReader
        'Dim xmlValid As Xml.XmlValidatingReader

        Dim KeyFields(1) As DataColumn

        xmlDoc = Document
        xmlSchema = Schema


        If xmlSchema = "" Then
            xmlSchema = "..\terminology.xsd"
        End If

        If xmlDoc = "" Then
            xmlDoc = "..\terminology.xml"
        End If

        Try
            Terminology.ReadXmlSchema(xmlSchema)
            Terminology.ReadXml(xmlDoc)
        Catch e As Exception
            MessageBox.Show("Loading terminology:" + e.Message)
        End Try

        KeyFields(0) = Terminology.Tables("Concept").Columns(0)
        KeyFields(1) = Terminology.Tables("Concept").Columns(1)
        Terminology.Tables("Concept").PrimaryKey = KeyFields

        ReDim KeyFields(0)
        KeyFields(0) = Terminology.Tables("Territory").Columns(0)
        Terminology.Tables("Territory").PrimaryKey = KeyFields

        Return True

    End Function

    'Sub New(ByVal a_default_language As String, ByVal ApplicationStartUpPath As String)
    Sub New()
        Dim primarykeyfields(0) As DataColumn

        '' runs if called externally
        'Default_language = a_default_language


        If Not initialised Then
            InitialiseTerminology(Application.StartupPath & "\terminology\terminology.xml", Application.StartupPath & "\terminology\terminology.xsd")
            initialised = True
        End If
        ' set the VSB column as a primary field for searching
        primarykeyfields(0) = Terminology.Tables("TerminologyIdentifiers").Columns(0)
        Terminology.Tables("TerminologyIdentifiers").PrimaryKey = primarykeyfields
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
