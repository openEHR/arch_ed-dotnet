'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Composition.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 

Namespace ArchetypeEditor.ADL_Classes
    Public Class ADL_Description
        Inherits ArchetypeDescription

        Private mADL_Description As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION

        Public Overrides ReadOnly Property Details() As ArchetypeDetails
            Get
                Return New ADL_ArchetypeDetails(mADL_Description)
            End Get
        End Property


        Function ADL_Description() As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION
            ' set the variables

            ' Add the original author details
            mADL_Description.original_author.clear_all()
            If Me.OriginalAuthor <> "" Then
                mADL_Description.add_original_author_item( _
                     openehr.base.kernel.Create.STRING.make_from_cil("name"), _
                    openehr.base.kernel.Create.STRING.make_from_cil(Me.mOriginalAuthor))
            End If
            If Me.OriginalAuthorEmail <> "" Then
                mADL_Description.add_original_author_item( _
                     openehr.base.kernel.Create.STRING.make_from_cil("email"), _
                    openehr.base.kernel.Create.STRING.make_from_cil(Me.mOriginalAuthorEmail))
            End If
            If Me.OriginalAuthorOrganisation <> "" Then
                mADL_Description.add_original_author_item( _
                     openehr.base.kernel.Create.STRING.make_from_cil("organisation"), _
                    openehr.base.kernel.Create.STRING.make_from_cil(Me.mOriginalAuthorOrganisation))
            End If
            If Me.OriginalAuthorDate <> "" Then
                mADL_Description.add_original_author_item( _
                     openehr.base.kernel.Create.STRING.make_from_cil("date"), _
                    openehr.base.kernel.Create.STRING.make_from_cil(Me.mOriginalAuthorDate))
            End If

            mADL_Description.set_lifecycle_state( _
                openehr.base.kernel.Create.STRING.make_from_cil(Me.LifeCycleStateAsString))

            If Not mArchetypePackageURI Is Nothing Then
                mADL_Description.set_archetype_package_uri( _
                    openehr.base.kernel.Create.STRING.make_from_cil(mArchetypePackageURI))
            End If

            Return mADL_Description
        End Function

        Sub New(ByVal an_adl_archetype_description As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION)
            mADL_Description = an_adl_archetype_description

            'mADL_Version = mADL_Description.adl_version.to_cil ' set to 1.2 by default
            If Not mADL_Description.archetype_package_uri Is Nothing Then
                mArchetypePackageURI = mADL_Description.archetype_package_uri.as_string.to_cil
            End If

            If mADL_Description.original_author.has(openehr.base.kernel.Create.STRING.make_from_cil("name")) Then
                mOriginalAuthor = mADL_Description.original_author.item(openehr.base.kernel.Create.STRING.make_from_cil("name")).to_cil()
            End If

            If mADL_Description.original_author.has(openehr.base.kernel.Create.STRING.make_from_cil("email")) Then
                mOriginalAuthorEmail = mADL_Description.original_author.item(openehr.base.kernel.Create.STRING.make_from_cil("email")).to_cil
            End If

            If mADL_Description.original_author.has(openehr.base.kernel.Create.STRING.make_from_cil("organisation")) Then
                mOriginalAuthorOrganisation = mADL_Description.original_author.item(openehr.base.kernel.Create.STRING.make_from_cil("organisation")).to_cil
            End If

            If mADL_Description.original_author.has(openehr.base.kernel.Create.STRING.make_from_cil("date")) Then
                mOriginalAuthorDate = mADL_Description.original_author.item(openehr.base.kernel.Create.STRING.make_from_cil("date")).to_cil
            End If

            MyBase.LifeCycleStateAsString = mADL_Description.lifecycle_state.to_cil
            If mADL_Description.details.count > 0 Then
                Dim mADL_Detail As openehr.openehr.am.archetype.description.ARCHETYPE_DESCRIPTION_ITEM
                For i As Integer = 1 To mADL_Description.details.count

                    mADL_Detail = mADL_Description.details.item(mADL_Description.details.key_at(i))


                Next
            Else
                Me.mArchetypeDetails.AddOrReplace( _
                Filemanager.Master.OntologyManager.LanguageCode, _
                New ArchetypeDescriptionItem(Filemanager.Master.OntologyManager.LanguageCode))
            End If
        End Sub

        Sub New()
            mADL_Description = openehr.openehr.am.archetype.description.Create.ARCHETYPE_DESCRIPTION.make_author(openehr.base.kernel.Create.STRING.make_from_cil(OceanArchetypeEditor.Instance.Options.UserName))
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
'The Original Code is ADL_Composition.vb.
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
