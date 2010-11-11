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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Description.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 
Imports AM = XMLParser.OpenEhr.V1.Its.Xml.AM
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes
    Public Class ADL_Description
        Inherits ArchetypeDescription

        Private mADL_Description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION

        Public Overrides Property Details() As ArchetypeDetails
            Get
                Return New ADL_ArchetypeDetails(mADL_Description)
            End Get
            Set(ByVal value As ArchetypeDetails)
                mArchetypeDetails = value
            End Set
        End Property

        Function ADL_Description() As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION
            mADL_Description.original_author.clear_all()

            If OriginalAuthor IsNot Nothing Then
                mADL_Description.add_original_author_item(Eiffel.String("name"), Eiffel.String(mOriginalAuthor))
            End If

            If OriginalAuthorEmail <> "" Then
                mADL_Description.add_original_author_item(Eiffel.String("email"), Eiffel.String(mOriginalAuthorEmail))
            End If

            If OriginalAuthorOrganisation <> "" Then
                mADL_Description.add_original_author_item(Eiffel.String("organisation"), Eiffel.String(mOriginalAuthorOrganisation))
            End If

            If OriginalAuthorDate <> "" Then
                mADL_Description.add_original_author_item(Eiffel.String("date"), Eiffel.String(mOriginalAuthorDate))
            End If

            mADL_Description.set_lifecycle_state(Eiffel.String(LifeCycleStateAsString))

            If Not mArchetypePackageURI Is Nothing Then
                mADL_Description.set_resource_package_uri(Eiffel.String(mArchetypePackageURI))
            End If

            If Not mADL_Description.other_details Is Nothing Then
                mADL_Description.other_details.clear_all()
            End If

            If mReferences <> "" Then
                mADL_Description.add_other_detail(Eiffel.String("references"), Eiffel.String(mReferences))
            End If

            If ArchetypeDigest <> "" Then
                mADL_Description.add_other_detail(Eiffel.String(AM.ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID), Eiffel.String(ArchetypeDigest))
            End If

            mADL_Description.clear_other_contributors()

            For Each s As String In mOtherContributors
                mADL_Description.add_other_contributor(Eiffel.String(s))
            Next

            Return mADL_Description
        End Function

        Sub New(ByVal an_adl_archetype_description As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION)
            mADL_Description = an_adl_archetype_description

            If Not mADL_Description.resource_package_uri Is Nothing Then
                mArchetypePackageURI = mADL_Description.resource_package_uri.as_string.to_cil
            End If

            If mADL_Description.original_author.has(Eiffel.String("name")) Then
                mOriginalAuthor = mADL_Description.original_author.item(Eiffel.String("name")).to_cil()
            End If

            If mADL_Description.original_author.has(Eiffel.String("email")) Then
                mOriginalAuthorEmail = mADL_Description.original_author.item(Eiffel.String("email")).to_cil
            End If

            If mADL_Description.original_author.has(Eiffel.String("organisation")) Then
                mOriginalAuthorOrganisation = mADL_Description.original_author.item(Eiffel.String("organisation")).to_cil
            End If

            If mADL_Description.original_author.has(Eiffel.String("date")) Then
                mOriginalAuthorDate = mADL_Description.original_author.item(Eiffel.String("date")).to_cil
            End If

            If Not mADL_Description.other_contributors Is Nothing Then
                For i As Integer = 1 To mADL_Description.other_contributors.count
                    mOtherContributors.Add(mADL_Description.other_contributors.i_th(i).to_cil)
                Next
            End If

            If Not mADL_Description.other_details Is Nothing Then
                If mADL_Description.other_details.has(Eiffel.String("references")) Then
                    mReferences = mADL_Description.other_details.item(Eiffel.String("references")).to_cil()
                End If
            End If

            MyBase.LifeCycleStateAsString = mADL_Description.lifecycle_state.to_cil

            If mADL_Description.details.count = 0 Then
                mArchetypeDetails.AddOrReplace(Filemanager.Master.OntologyManager.LanguageCode, New ArchetypeDescriptionItem(Filemanager.Master.OntologyManager.LanguageCode))
            End If
        End Sub

        Sub New(ByVal an_archetype_description As ArchetypeDescription, ByVal a_language As String)
            mADL_Description = openehr.openehr.rm.common.resource.Create.RESOURCE_DESCRIPTION.make(Eiffel.String(Main.Instance.Options.UserName), Eiffel.String(a_language))

            If Not an_archetype_description.ArchetypePackageURI Is Nothing Then
                mArchetypePackageURI = an_archetype_description.ArchetypePackageURI
            End If

            mOriginalAuthor = an_archetype_description.OriginalAuthor
            mOriginalAuthorEmail = an_archetype_description.OriginalAuthorEmail
            mOriginalAuthorOrganisation = an_archetype_description.OriginalAuthorOrganisation
            mOriginalAuthorDate = an_archetype_description.OriginalAuthorDate
            mOtherContributors = an_archetype_description.OtherContributors
            mReferences = an_archetype_description.References

            MyBase.LifeCycleState = an_archetype_description.LifeCycleState
        End Sub

        Sub New(ByVal original_language As String)
            mADL_Description = openehr.openehr.rm.common.resource.Create.RESOURCE_DESCRIPTION.make(Eiffel.String(Main.Instance.Options.UserName), Eiffel.String(original_language))
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
'The Original Code is ADL_Description.vb.
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
