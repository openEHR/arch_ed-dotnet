'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Option Strict On

'Need to use inheritance here to allow use of the interface on RmStructure
' and RmStructureCompound

Public Interface ArcheTypeDefintionBasic
    ReadOnly Property Type() As StructureType
    Property RootNodeId() As String
    Property NameConstraint() As Constraint_Text
    Property hasNameConstraint() As Boolean
End Interface

Public Interface ArchetypeDefinition
    Inherits ArcheTypeDefintionBasic

    Property Data() As Children
    Function GetChildByNodeId(ByVal aNodeId As String) As RmStructure
End Interface

Public MustInherit Class ArchetypeDefinitionAbstract
    Implements ArchetypeDefinition

    Protected mChildren As Children
    Protected mNodeID As String
    Protected mRuntimeConstraint As Constraint_Text
    Protected mType As StructureType

    Public ReadOnly Property Type() As StructureType Implements ArchetypeDefinition.Type
        Get
            Return mType
        End Get
    End Property

    Public Property Data() As Children Implements ArchetypeDefinition.Data
        Get
            Return mChildren
        End Get
        Set(ByVal Value As Children)
            mChildren = Value
        End Set
    End Property
    Public Property hasNameConstraint() As Boolean Implements ArcheTypeDefintionBasic.hasNameConstraint
        Get
            Return Not mRuntimeConstraint Is Nothing
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                If mRuntimeConstraint Is Nothing Then
                    mRuntimeConstraint = New Constraint_Text
                End If
            Else
                mRuntimeConstraint = Nothing
            End If
        End Set
    End Property
    Public Property NameConstraint() As Constraint_Text Implements ArchetypeDefinition.NameConstraint
        Get
            If mRuntimeConstraint Is Nothing Then
                mRuntimeConstraint = New Constraint_Text
            End If
            Return mRuntimeConstraint
        End Get
        Set(ByVal Value As Constraint_Text)
            mRuntimeConstraint = Value
        End Set
    End Property
    Public Property RootNodeId() As String Implements ArchetypeDefinition.RootNodeId
        Get
            Return mNodeID
        End Get
        Set(ByVal Value As String)
            mNodeID = Value
        End Set
    End Property

    Public Function GetChildByNodeId(ByVal aNodeId As String) As RmStructure Implements ArchetypeDefinition.GetChildByNodeId

        If mNodeID = aNodeId Then
            Debug.Assert(False)
            Return Nothing
        Else
            Return mChildren.GetChildByNodeId(aNodeId)
        End If

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
'The Original Code is ArchetypeDefinition.vb.
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