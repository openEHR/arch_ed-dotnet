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
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class ArchetypeElementTreeNode : Inherits ArchetypeTreeNode

    Public Shadows ReadOnly Property Item() As ArchetypeElement
        Get
            Debug.Assert(TypeOf MyBase.Item Is ArchetypeElement)

            Return CType(MyBase.Item, ArchetypeElement)
        End Get
    End Property

    Private ReadOnly Property IsReference() As Boolean

        Get
            Return Me.Item.IsReference
        End Get
    End Property
    Private ReadOnly Property HasReferences() As Boolean

        Get
            Return Me.Item.HasReferences
        End Get
    End Property
    Private Property Constraint() As Constraint
        Get
            Return Me.Item.Constraint
        End Get
        Set(ByVal Value As Constraint)
            Me.Item.Constraint = Value
        End Set
    End Property

    Private ReadOnly Property DataType() As String
        Get
            Return Me.Item.DataType
        End Get

    End Property

    Public Overrides Function Copy() As ArchetypeTreeNode
        Return New ArchetypeElementTreeNode(Me.Item.Copy)
    End Function

    Sub New(ByVal aText As String)
        MyBase.New(New ArchetypeElement(aText))
        Me.Item.Occurrences.MaxCount = 1
    End Sub

    Sub New(ByVal el As RM_Element)
        MyBase.New(New ArchetypeElement(el))
    End Sub

    Sub New(ByVal el As ArchetypeNode)
        MyBase.New(New ArchetypeElement(CType(el.RM_Class, RM_Element)))
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
'The Original Code is ArchetypeElementTreeNode.vb.
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
