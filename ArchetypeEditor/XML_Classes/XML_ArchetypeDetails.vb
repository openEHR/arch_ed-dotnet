'
'
'	component:   "openEHR Archetype Project"
'	description: "Builds all XML ArchetypeDetails"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'
Option Explicit On

Namespace ArchetypeEditor.XML_Classes

    Public Class XML_ArchetypeDetails
        Inherits ArchetypeDetails

        Private mXmlDescription As XMLParser.RESOURCE_DESCRIPTION
        Private mDescriptionItems As ArrayList

        Private ReadOnly Property DescriptionItemsAsArrayList() As ArrayList
            Get
                If mDescriptionItems Is Nothing Then
                    mDescriptionItems = New ArrayList()

                    If Not mXmlDescription.details Is Nothing Then
                        mDescriptionItems.AddRange(mXmlDescription.details)
                    End If
                End If

                Return mDescriptionItems
            End Get
        End Property

        Public Overrides Sub AddOrReplace(ByVal language As String, ByVal detail As ArchetypeDescriptionItem)
            If HasDetailInLanguage(language) Then
                RemoveDescriptionItem(language)
            End If

            Dim d As New XMLParser.RESOURCE_DESCRIPTION_ITEM
            d.language = New XMLParser.CODE_PHRASE
            d.language.code_string = language
            d.language.terminology_id = New XMLParser.TERMINOLOGY_ID
            d.language.terminology_id.value = Main.Instance.DefaultLanguageCodeSet
            d.copyright = detail.Copyright

            If detail.KeyWords.Count > 0 Then
                d.keywords = Array.CreateInstance(GetType(String), detail.KeyWords.Count)

                For i As Integer = 0 To detail.KeyWords.Count - 1
                    d.keywords(i) = detail.KeyWords(i)
                Next
            End If

            d.misuse = detail.MisUse
            d.use = detail.Use()
            d.purpose = detail.Purpose
            AddDetail(d)
        End Sub

        Public Sub AddDetail(ByVal d As XMLParser.RESOURCE_DESCRIPTION_ITEM)
            DescriptionItemsAsArrayList.Add(d)
            mXmlDescription.details = DescriptionItemsAsArrayList.ToArray(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM))
        End Sub

        Public Sub RemoveDescriptionItem(ByVal language As String)
            Debug.Assert(HasDetailInLanguage(language))
            DescriptionItemsAsArrayList.RemoveAt(IndexOfDetailInLanguage(language))
            mXmlDescription.details = DescriptionItemsAsArrayList.ToArray(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM))
        End Sub

        Public Overrides Function DetailInLanguage(ByVal language As String) As ArchetypeDescriptionItem
            Dim result As New ArchetypeDescriptionItem(language)
            Dim i As Integer = IndexOfDetailInLanguage(language)

            If i >= 0 Then
                Dim d As XMLParser.RESOURCE_DESCRIPTION_ITEM = mXmlDescription.details(i)

                If Not d.misuse Is Nothing Then
                    result.MisUse = d.misuse
                Else
                    result.MisUse = ""
                End If

                If Not d.use Is Nothing Then
                    result.Use = d.use
                Else
                    result.Use = ""
                End If

                If Not d.purpose Is Nothing Then
                    result.Purpose = d.purpose
                Else
                    result.Purpose = ""
                End If

                If Not d.keywords Is Nothing Then
                    result.KeyWords.AddRange(d.keywords)
                End If

                If Not d.copyright Is Nothing Then
                    result.Copyright = d.copyright
                End If
            End If

            Return result
        End Function

        Public Overrides Function HasDetailInLanguage(ByVal language As String) As Boolean
            Dim result As Boolean = False

            For Each d As XMLParser.RESOURCE_DESCRIPTION_ITEM In DescriptionItemsAsArrayList
                If d.language.code_string = language Then
                    result = True
                End If
            Next

            Return result
        End Function

        Private Function IndexOfDetailInLanguage(ByVal language As String) As Integer
            Dim result As Integer = -1

            If Not mXmlDescription.details Is Nothing Then
                For i As Integer = 0 To mXmlDescription.details.Length - 1
                    Dim d As XMLParser.RESOURCE_DESCRIPTION_ITEM = mXmlDescription.details(i)

                    If d.language.code_string = language Then
                        result = i
                    End If
                Next
            End If

            Return result
        End Function

        Public Overrides ReadOnly Property Count() As Integer
            Get
                Return mXmlDescription.details.Length
            End Get
        End Property

        Sub New(ByVal description As XMLParser.RESOURCE_DESCRIPTION)
            mXmlDescription = description
        End Sub

    End Class

End Namespace

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
'The Original Code is XML_ArchetypeDetails.vb.
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