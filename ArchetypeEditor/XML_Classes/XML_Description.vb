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
'
'option Strict On 
Option Explicit On 
Imports AM = XMLParser.OpenEhr.V1.Its.Xml.AM

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
            Dim authorDetails As New ArrayList
            Dim otherDetails As New ArrayList
            Dim di As New XMLParser.StringDictionaryItem
            di.id = "name"

            If Not OriginalAuthor Is Nothing Then
                di.Value = mOriginalAuthor
            End If

            authorDetails.Add(di)

            If mOriginalAuthorEmail <> "" Then
                di = New XMLParser.StringDictionaryItem
                di.id = "email"
                di.Value = mOriginalAuthorEmail
                authorDetails.Add(di)   
            End If

            If mOriginalAuthorDate <> "" Then
                di = New XMLParser.StringDictionaryItem
                di.id = "date"
                di.Value = mOriginalAuthorDate
                authorDetails.Add(di)
            End If

            If mOriginalAuthorOrganisation <> "" Then
                di = New XMLParser.StringDictionaryItem
                di.id = "organisation"
                di.Value = mOriginalAuthorOrganisation
                authorDetails.Add(di)
            End If

            mXML_Description.original_author = authorDetails.ToArray(GetType(XMLParser.StringDictionaryItem))

       		Dim ctr As Integer

            For ctr = 0 To mOtherDetails.OtherDetails.GetLength(0) - 1
                di = New XMLParser.StringDictionaryItem
                di.id = mOtherDetails.OtherDetails(ctr, 0)
                di.Value = mOtherDetails.OtherDetails(ctr, 1)
                otherDetails.Add(di)
            Next

            UpdateOtherDetails(ReferencesKey, mReferences, otherdetails)
            UpdateOtherDetails(CurrentContactKey, mCurrentContact, otherDetails)
            UpdateOtherDetails(OriginalPublisherKey, mOriginalPublisher, otherdetails)
            UpdateOtherDetails(OriginalNamespaceKey, mOriginalNamespace, otherdetails)
            UpdateOtherDetails(CustodianorganisationKey, mCustodianOrganisation, otherdetails)
            UpdateOtherDetails(CustodianNamespaceKey, mCustodianNamespace, otherDetails)
            UpdateOtherDetails(LicenceKey, mLicence, otherDetails)
            UpdateOtherDetails(ReviewDateKey, mReviewDate, otherDetails)
            UpdateOtherDetails(RevisionKey, mRevision, otherdetails)
 			UpdateOtherDetails(BuildIdKey, mBuildId, otherdetails)
 		

            If Not ArchetypeDigest Is Nothing Then
                UpdateOtherDetails(AM.ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID, ArchetypeDigest, otherdetails)
            End If

            mXML_Description.other_details = otherDetails.ToArray(GetType(XMLParser.StringDictionaryItem))
            mXML_Description.lifecycle_state = LifeCycleStateAsString

            If Not String.IsNullOrEmpty(mArchetypePackageURI) Then
                mXML_Description.resource_package_uri = mArchetypePackageURI
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

        Private Sub UpdateOtherDetails(ByVal currentKey As String, currentvalue As String, otherdetails As System.Collections.ArrayList)
            Dim di As XMLParser.StringDictionaryItem
            Dim currdi As XMLParser.StringDictionaryItem

            If currentvalue <> "" Then
                di = New XMLParser.StringDictionaryItem
                di.id = currentKey
                di.Value = CurrentValue

                Dim index As Integer = -1

                For i As Integer = 0 To otherdetails.Count - 1
                    currdi = TryCast(otherdetails.Item(i), XMLParser.StringDictionaryItem)

                    If currdi.id = di.id Then
                        index = i
                    End If
                Next

                If index = -1 Then
                    otherdetails.Add(di)
                Else
                    otherdetails.Item(index) = di
                End If
            End If
        End Sub

        Sub New(ByVal description As XMLParser.RESOURCE_DESCRIPTION, ByVal language As String)
            mXML_Description = description
            mADL_Version = "2.0" ' this is actually the archetype model rather than ADL
            mArchetypePackageURI = description.resource_package_uri

            If Not description.original_author Is Nothing Then
                For Each di As XMLParser.StringDictionaryItem In description.original_author
                    Select Case di.id.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "name"
                            mOriginalAuthor = di.Value
                        Case "email"
                            mOriginalAuthorEmail = di.Value
                        Case "date"
                            mOriginalAuthorDate = di.Value
                        Case "organisation"
                            mOriginalAuthorOrganisation = di.Value
                    End Select
                Next
            End If

            If Not description.other_contributors Is Nothing Then
                For Each s As String In description.other_contributors
                    mOtherContributors.Add(s)
                Next
            End If

            If Not description.other_details Is Nothing Then
                For Each di As XMLParser.StringDictionaryItem In description.other_details
                    mOtherDetails.AddOtherDetail(di.id, di.Value)

                    Select Case di.id.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case ReferencesKey
                            mReferences = di.Value
                        Case CurrentContactKey
                            mCurrentContact = di.Value
                        Case OriginalPublisherKey
                            mOriginalPublisher = di.Value
                        Case OriginalNamespaceKey
                            mOriginalNamespace = di.Value
                        Case CustodianorganisationKey
                            mCustodianOrganisation = di.Value
                        Case CustodianNamespaceKey
                            mCustodianNamespace = di.Value
                        Case LicenceKey
                            mLicence = di.Value
                        Case ReviewDate
                            mReviewDate = di.Value
                        Case RevisionKey
                        	mRevision = di.Value
                        Case BuildIdKey
                        	mBuildId = di.Value
                    End Select
                Next
            End If

            MyBase.LifeCycleStateAsString = description.lifecycle_state

            If description.details Is Nothing OrElse description.details.Length = 0 Then
                mArchetypeDetails.AddOrReplace(language, New ArchetypeDescriptionItem(language))
            End If
        End Sub

        Sub New()
            mXML_Description = New XMLParser.RESOURCE_DESCRIPTION()
            mOriginalAuthor = Main.Instance.Options.UserName
        End Sub

        Sub New(ByVal description As ADL_Classes.ADL_Description)
            mXML_Description = New XMLParser.RESOURCE_DESCRIPTION()
            ArchetypePackageURI = description.ArchetypePackageURI
            OriginalAuthor = description.OriginalAuthor
            OriginalAuthorDate = description.OriginalAuthorDate
            OriginalAuthorEmail = description.OriginalAuthorEmail
            OriginalAuthorOrganisation = description.OriginalAuthorOrganisation
            OtherContributors = description.OtherContributors
            References = description.References
            mOtherDetails = description.OtherDetails
            CurrentContact = description.CurrentContact
            LifeCycleState = description.LifeCycleState
            OriginalPublisher = description.OriginalPublisher
            OriginalNamespace = description.OriginalNamespace
            CustodianOrganisation = description.CustodianOrganisation
            CustodianNamespace = description.CustodianNamespace
            Licence = description.Licence
            Revision = description.Revision
            ReviewDate = description.ReviewDate
            BuildId = description.BuildId
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
