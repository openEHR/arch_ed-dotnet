'
'
'	component:   "openEHR Archetype Project"
'	description: "Builds all XML ArchetypeDetails"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.XML_ArchetypeDetails.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'
Option Explicit On

Namespace ArchetypeEditor.XML_Classes

    Public Class XML_ArchetypeDetails
        Inherits ArchetypeDetails

        Private mXML_Description As XMLParser.RESOURCE_DESCRIPTION
        Private mDescriptionItems As ArrayList

        Private ReadOnly Property DescriptionItemsAsArrayList() As ArrayList
            Get
                If mDescriptionItems Is Nothing Then
                    mDescriptionItems = New ArrayList()

                    If Not mXML_Description.details Is Nothing Then
                        mDescriptionItems.AddRange(mXML_Description.details)
                    End If
                End If

                Return mDescriptionItems
            End Get
        End Property

        Public Overrides Sub AddOrReplace(ByVal language As String, ByVal item As ArchetypeDescriptionItem)
            If HasDetailInLanguage(language) Then
                RemoveDescriptionItem(language)
            End If

            Dim detail As New XMLParser.RESOURCE_DESCRIPTION_ITEM
            detail.language = New XMLParser.CODE_PHRASE
            detail.language.code_string = language
            detail.language.terminology_id = New XMLParser.TERMINOLOGY_ID
            detail.language.terminology_id.value = OceanArchetypeEditor.DefaultLanguageCodeSet
            detail.copyright = item.Copyright

            If item.KeyWords.Count > 0 Then
                detail.keywords = Array.CreateInstance(GetType(String), item.KeyWords.Count)

                For i As Integer = 0 To item.KeyWords.Count - 1
                    detail.keywords(i) = item.KeyWords(i)
                Next
            End If

            detail.misuse = item.MisUse
            detail.use = item.Use()
            detail.purpose = item.Purpose
            AddDetail(detail)
        End Sub

        Public Sub AddDetail(ByVal item As XMLParser.RESOURCE_DESCRIPTION_ITEM)
            DescriptionItemsAsArrayList.Add(item)
            mXML_Description.details = DescriptionItemsAsArrayList.ToArray(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM))
        End Sub

        Public Sub RemoveDescriptionItem(ByVal language_code As String)
            Debug.Assert(HasDetailInLanguage(language_code))
            DescriptionItemsAsArrayList.RemoveAt(IndexOfDetailInLanguage(language_code))
            mXML_Description.details = DescriptionItemsAsArrayList.ToArray(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM))
        End Sub

        Public Overrides Function DetailInLanguage(ByVal language As String) As ArchetypeDescriptionItem
            Dim result As New ArchetypeDescriptionItem(language)
            Dim i As Integer = IndexOfDetailInLanguage(language)

            If i >= 0 Then
                Dim detail As XMLParser.RESOURCE_DESCRIPTION_ITEM = mXML_Description.details(i)

                If Not detail.misuse Is Nothing Then
                    result.MisUse = detail.misuse
                Else
                    result.MisUse = ""
                End If

                If Not detail.use Is Nothing Then
                    result.Use = detail.use
                Else
                    result.Use = ""
                End If

                If Not detail.purpose Is Nothing Then
                    result.Purpose = detail.purpose
                Else
                    result.Purpose = ""
                End If

                If Not detail.keywords Is Nothing Then
                    result.KeyWords.AddRange(detail.keywords)
                End If

                If Not detail.copyright Is Nothing Then
                    result.Copyright = detail.copyright
                End If
            End If

            Return result
        End Function

        Public Overrides Function HasDetailInLanguage(ByVal language As String) As Boolean
            Dim result As Boolean = False

            For Each rdi As XMLParser.RESOURCE_DESCRIPTION_ITEM In DescriptionItemsAsArrayList
                If rdi.language.code_string = language Then
                    result = True
                End If
            Next

            Return result
        End Function

        Private Function IndexOfDetailInLanguage(ByVal language As String) As Integer
            Dim result As Integer = -1

            For i As Integer = 0 To mXML_Description.details.Length - 1
                Dim rdi As XMLParser.RESOURCE_DESCRIPTION_ITEM = mXML_Description.details(i)

                If rdi.language.code_string = language Then
                    result = i
                End If
            Next

            Return result
        End Function

        Sub New(ByVal description As XMLParser.RESOURCE_DESCRIPTION)
            mXML_Description = description
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