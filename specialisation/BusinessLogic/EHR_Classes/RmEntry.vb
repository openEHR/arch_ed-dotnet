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


Public Class RmEntry
    Inherits ArchetypeDefinitionAbstract
    Protected rSubjectOfData As New RelatedParty

    Public Overridable Property SubjectOfData() As RelatedParty
        Get
            Return rSubjectOfData
        End Get
        Set(ByVal Value As RelatedParty)
            rSubjectOfData = Value
        End Set
    End Property

    Protected mProviderMandatory As Boolean = False

    Public Property ProviderIsMandatory() As Boolean
        Get
            Return mProviderMandatory
        End Get
        Set(ByVal value As Boolean)
            mProviderMandatory = value
        End Set
    End Property

    Protected mOtherParticipations As Collections.Generic.List(Of Participation)

    Public Property OtherParticipations() As Collections.Generic.List(Of Participation)
        Get
            If mOtherParticipations Is Nothing Then
                mOtherParticipations = New Collections.Generic.List(Of Participation)
            End If
            Return mOtherParticipations
        End Get
        Set(ByVal value As Collections.Generic.List(Of Participation))
            mOtherParticipations = value
        End Set
    End Property

    Sub New()
        mType = StructureType.ENTRY
        mChildren = New Children(mType)
        rSubjectOfData = New RelatedParty
        rSubjectOfData.Relationship.TerminologyID = "openehr"
    End Sub

    Sub New(ByVal SubType As String)
        Select Case SubType.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "observation"
                mType = StructureType.OBSERVATION
            Case "evaluation"
                mType = StructureType.EVALUATION
            Case "instruction"
                mType = StructureType.INSTRUCTION
            Case "action"
                mType = StructureType.ACTION
            Case "entry"
                mType = StructureType.ENTRY
            Case "admin_entry"
                mType = StructureType.ADMIN_ENTRY
            Case Else
                'raise error
                Beep()
                Debug.Assert(False)
        End Select
        mChildren = New Children(mType)
        rSubjectOfData = New RelatedParty
        rSubjectOfData.Relationship.TerminologyID = "openehr"
    End Sub

    Sub New(ByVal aType As StructureType)
        mType = aType
        mChildren = New Children(aType)
        rSubjectOfData = New RelatedParty
        rSubjectOfData.Relationship.TerminologyID = "openehr"
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
'The Original Code is RmEntry.vb.
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
