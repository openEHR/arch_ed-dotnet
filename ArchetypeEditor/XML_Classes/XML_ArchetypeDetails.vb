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
'Option Strict On
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

        Public Overrides Sub AddOrReplace(ByVal a_language As String, ByVal a_detail As ArchetypeDescriptionItem)
            If Me.HasDetailInLanguage(a_language) Then
                Me.RemoveDescriptionItem(a_language)
            End If
            Dim XML_detail As New XMLParser.RESOURCE_DESCRIPTION_ITEM

            XML_detail.language = New XMLParser.CODE_PHRASE
            XML_detail.language.code_string = a_language

            'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            'XML_detail.language.terminology_id = OceanArchetypeEditor.DefaultLanguageCodeSet
            XML_detail.language.terminology_id = New XMLParser.TERMINOLOGY_ID
            XML_detail.language.terminology_id.value = OceanArchetypeEditor.DefaultLanguageCodeSet

            XML_detail.copyright = a_detail.Copyright
            If a_detail.KeyWords.Count > 0 Then
                XML_detail.keywords = Array.CreateInstance(GetType(String), a_detail.KeyWords.Count)
                For i As Integer = 0 To a_detail.KeyWords.Count - 1
                    XML_detail.keywords(i) = a_detail.KeyWords(i)
                Next
            End If

            XML_detail.misuse = a_detail.MisUse
            XML_detail.use = a_detail.Use()
            XML_detail.purpose = a_detail.Purpose
            Me.AddDetail(XML_detail)
        End Sub

        Public Sub AddDetail(ByVal a_detail As XMLParser.RESOURCE_DESCRIPTION_ITEM)
            Me.DescriptionItemsAsArrayList.Add(a_detail)
            mXML_Description.details = Me.DescriptionItemsAsArrayList.ToArray(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM))
        End Sub

        Public Sub RemoveDescriptionItem(ByVal a_language_code As String)
            Debug.Assert(Me.HasDetailInLanguage(a_language_code))
            Me.DescriptionItemsAsArrayList.RemoveAt(Me.IndexOfDetailInLanguage(a_language_code))
            mXML_Description.details = Me.DescriptionItemsAsArrayList.ToArray(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM))
        End Sub

        Public Overrides Function DetailInLanguage(ByVal a_language As String) As ArchetypeDescriptionItem
            Dim archDescriptDetail As New ArchetypeDescriptionItem(a_language)

            If HasDetailInLanguage(a_language) Then
                For Each XML_detail As XMLParser.RESOURCE_DESCRIPTION_ITEM In mXML_Description.details
                    If XML_detail.language.code_string = a_language Then
                        If Not XML_detail.misuse Is Nothing Then
                            archDescriptDetail.MisUse = XML_detail.misuse
                        Else
                            archDescriptDetail.MisUse = ""
                        End If

                        If Not XML_detail.use Is Nothing Then
                            archDescriptDetail.Use = XML_detail.use
                        Else
                            archDescriptDetail.Use = ""
                        End If

                        If Not XML_detail.purpose Is Nothing Then
                            archDescriptDetail.Purpose = XML_detail.purpose
                        Else
                            archDescriptDetail.Purpose = ""
                        End If

                        If Not XML_detail.keywords Is Nothing Then
                            archDescriptDetail.KeyWords.AddRange(XML_detail.keywords)
                        End If
                        Exit For
                    End If
                Next
            End If
            Return archDescriptDetail
        End Function

        Public Overrides Function HasDetailInLanguage(ByVal a_language As String) As Boolean
            For Each rdi As XMLParser.RESOURCE_DESCRIPTION_ITEM In Me.DescriptionItemsAsArrayList
                If rdi.language.code_string = a_language Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Function IndexOfDetailInLanguage(ByVal a_language As String) As Integer
            For i As Integer = 0 To mXML_Description.details.Length - 1
                Dim rdi As XMLParser.RESOURCE_DESCRIPTION_ITEM = mXML_Description.details(i)
                If rdi.language.code_string = a_language Then
                    Return i
                End If
            Next
            Return -1
        End Function

        Sub New(ByVal a_description As XMLParser.RESOURCE_DESCRIPTION)
            'mADL_Details = a_description.details
            mXML_Description = a_description
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