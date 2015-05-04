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

Option Strict On

Public MustInherit Class ArchetypeNode
    Public MustOverride Property Text() As String
    Public MustOverride Property Constraint() As Constraint
    Public MustOverride Function ToRichText(ByVal level As Integer) As String
    Public MustOverride Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String
    Public MustOverride Function Copy() As ArchetypeNode

    Protected mItem As RmStructure

    Public Overridable ReadOnly Property RM_Class() As RmStructure
        Get
            Return mItem
        End Get
    End Property

    Protected Overridable Property Item() As RmStructure
        Get
            Return mItem
        End Get
        Set(ByVal value As RmStructure)
            mItem = value
        End Set
    End Property

    Public Overridable Property Occurrences() As RmCardinality
        Get
            Return mItem.Occurrences
        End Get
        Set(ByVal value As RmCardinality)
            mItem.Occurrences = value
        End Set
    End Property

    Public Overridable ReadOnly Property IsMandatory() As Boolean
        Get
            Return Occurrences.MinCount > 0
        End Get
    End Property

    Public Overridable ReadOnly Property IsAnonymous() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property IsReference() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property HasReferences() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property CanRemove() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property CanSpecialise() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property CanChangeDataType() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable Function ImageIndex(ByVal isSelected As Boolean) As Integer
        Dim result As Integer = ConstraintKind.Any

        If Not Constraint Is Nothing Then
            result = Constraint.ImageIndexForConstraintKind(IsReference, isSelected)
        End If

        Return result
    End Function

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
'The Original Code is ArchetypeNode.vb.
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
