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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.XML_Description.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Explicit On 

Namespace ArchetypeEditor.XML_Classes
    Public Class XML_Description
        Inherits ArchetypeDescription

        Private mXML_Description As XMLParser.RESOURCE_DESCRIPTION

        Public Overrides Property Details() As ArchetypeDetails
            Get
                Return New XML_ArchetypeDetails(mXML_Description)
            End Get
            Set(ByVal value As ArchetypeDetails)
                mArchetypeDetails = value
            End Set
        End Property


        Function XML_Description() As XMLParser.RESOURCE_DESCRIPTION
            ' set the variables

            ' Add the original author details

            Dim authorDetails As New ArrayList

            Dim di As New XMLParser.dictionaryItem

            di.key = "name"
            If (Not Me.OriginalAuthor Is Nothing) Then
                di.value = Me.mOriginalAuthor.Replace("""", "'")
            End If
            authorDetails.Add(di)

            If Me.mOriginalAuthorEmail <> "" Then
                di = New XMLParser.dictionaryItem
                di.key = "email"
                di.value = Me.mOriginalAuthorEmail.Replace("""", "'")
                authorDetails.Add(di)
            End If

            If Me.mOriginalAuthorDate <> "" Then
                di = New XMLParser.dictionaryItem
                di.key = "date"
                di.value = Me.mOriginalAuthorDate.Replace("""", "'")
                authorDetails.Add(di)
            End If

            If Me.mOriginalAuthorOrganisation <> "" Then
                di = New XMLParser.dictionaryItem
                di.key = "organisation"
                di.value = Me.mOriginalAuthorOrganisation.Replace("""", "'")
                authorDetails.Add(di)
            End If

            mXML_Description.original_author = authorDetails.ToArray(GetType(XMLParser.dictionaryItem))

            mXML_Description.lifecycle_state = Me.LifeCycleStateAsString.Replace("""", "'")

            If Not mArchetypePackageURI Is Nothing Then
                mXML_Description.resource_package_uri = mArchetypePackageURI.Replace("""", "'")
            End If

            ' clear the other contributors and add them again

            Dim arrayLength As Integer
            If mXML_Description.other_contributors Is Nothing Then
                arrayLength = 0
            Else
                arrayLength = mXML_Description.other_contributors.Length
            End If

            If arrayLength <> mOtherContributors.Count Then
                If mOtherContributors.Count = 0 Then
                    mXML_Description.other_contributors = Nothing
                Else
                    mXML_Description.other_contributors = Array.CreateInstance(GetType(String), mOtherContributors.Count)
                End If
            End If
            For i As Integer = 0 To mOtherContributors.Count - 1
                mXML_Description.other_contributors(i) = mOtherContributors(i)
            Next

            Return mXML_Description
        End Function

        Sub New(ByVal an_xml_archetype_description As XMLParser.RESOURCE_DESCRIPTION, ByVal a_language_code As String)
            mXML_Description = an_xml_archetype_description

            mADL_Version = "2.0" ' this is actually the archetype model rather than ADL
            If Not mXML_Description.resource_package_uri Is Nothing Then
                mArchetypePackageURI = mXML_Description.resource_package_uri
            End If

            If Not mXML_Description.original_author Is Nothing Then
                For Each di As XMLParser.dictionaryItem In mXML_Description.original_author
                    Select Case di.key.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "name"
                            mOriginalAuthor = di.value
                        Case "email"
                            mOriginalAuthorEmail = di.value
                        Case "date"
                            mOriginalAuthorDate = di.value
                        Case "organisation"
                            mOriginalAuthorOrganisation = di.value
                    End Select
                Next
            End If
            If Not mXML_Description.other_contributors Is Nothing Then
                For Each s As String In mXML_Description.other_contributors
                    mOtherContributors.Add(s)
                Next
            End If

            MyBase.LifeCycleStateAsString = mXML_Description.lifecycle_state

            If (mXML_Description.details Is Nothing) OrElse mXML_Description.details.Length = 0 Then
                Me.mArchetypeDetails.AddOrReplace( _
                a_language_code, _
                New ArchetypeDescriptionItem(a_language_code))
            End If
        End Sub

        Sub New()
            mXML_Description = New XMLParser.RESOURCE_DESCRIPTION()
            mOriginalAuthor = OceanArchetypeEditor.Instance.Options.UserName
        End Sub

        Sub New(ByVal adl_description As ADL_Classes.ADL_Description)
            mXML_Description = New XMLParser.RESOURCE_DESCRIPTION()
            Me.LifeCycleState = adl_description.LifeCycleState
            Me.CopyRight = adl_description.CopyRight
            Me.ArchetypePackageURI = adl_description.ArchetypePackageURI
            Me.OriginalAuthor = adl_description.OriginalAuthor
            Me.OriginalAuthorDate = adl_description.OriginalAuthorDate
            Me.OriginalAuthorEmail = adl_description.OriginalAuthorEmail
            Me.OriginalAuthorOrganisation = adl_description.OriginalAuthorOrganisation
            Me.OtherContributors = adl_description.OtherContributors
            Me.OtherDetails = adl_description.OtherDetails
            'Dim XmlArchetypeDetails As New XML_ArchetypeDetails(mXML_Description)
            'If adl_description.Details.Count > 0 Then
            '    For Each adl_detail In adl_description.Details

            '    Next

            'End If
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
'The Original Code is XML_Description.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
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
