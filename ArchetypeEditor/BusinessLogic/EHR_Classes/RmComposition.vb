'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'


Public Class RmComposition : Inherits ArchetypeDefinitionAbstract

    Protected mIsPersistent As Boolean = False

    Public Property IsPersistent() As Boolean
        Get
            Return mIsPersistent
        End Get
        Set(ByVal Value As Boolean)
            mIsPersistent = Value
        End Set
    End Property

    Protected mParticipations As RmStructureCompound

    Public Property Participations() As RmStructureCompound
        Get
            Return mParticipations
        End Get
        Set(ByVal value As RmStructureCompound)
            mParticipations = value

            If Not mParticipations Is Nothing Then
                mParticipations.NodeId = "participations" ' the attribute name
            End If
        End Set
    End Property

    Public ReadOnly Property HasParticipations() As Boolean
        Get
            Return Not IsPersistent AndAlso Not mParticipations Is Nothing AndAlso mParticipations.Children.Count > 0
        End Get
    End Property

    Public ReadOnly Property CategoryCodePhrase() As CodePhrase
        Get
            Dim result As New CodePhrase("openehr")

            If IsPersistent Then
                result.Codes.Add("431") ' persistent
            Else
                result.Codes.Add("433") ' event
            End If

            Return result
        End Get
    End Property

    Sub New()
        mType = StructureType.COMPOSITION
        mChildren = New Children(mType)
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
'The Original Code is RmComposition.vb.
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
