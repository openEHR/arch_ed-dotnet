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

Public Class ArchetypeCompoundTreeNode : Inherits ArchetypeTreeNode
    Public Shadows ReadOnly Property Item() As ArchetypeComposite
        Get
            Debug.Assert(TypeOf MyBase.Item Is ArchetypeComposite)

            Return CType(MyBase.Item, ArchetypeComposite)
        End Get
    End Property

    Public ReadOnly Property IsFixed() As Boolean
        Get
            'Return Not cChildren.isUnbounded
            Return Me.Item.IsFixed()
        End Get
    End Property

    Public Property IsOrdered() As Boolean
        Get
            'Return boolOrdered
            Return Me.Item.IsOrdered
        End Get
        Set(ByVal Value As Boolean)
            'boolOrdered = Value
            Me.Item.IsOrdered = Value
        End Set
    End Property


    Public Overrides Function Copy() As ArchetypeTreeNode
        Return New ArchetypeCompoundTreeNode(Me.Item)
    End Function

    Sub New(ByVal aText As String, Optional ByVal IsSection As Boolean = False)
        MyBase.New(New ArchetypeComposite(aText))
        Me.Item.Occurrences.MaxCount = 1
        If IsSection Then
            MyBase.ImageIndex = 1
            MyBase.SelectedImageIndex = 3
        Else
            MyBase.ImageIndex = 32
            MyBase.SelectedImageIndex = 33
        End If
    End Sub

    Sub New(ByVal aCluster As RM_Cluster)
        MyBase.New(New ArchetypeComposite(aCluster))
        Me.Item.Occurrences = aCluster.Occurrences
        MyBase.ImageIndex = 32
        MyBase.SelectedImageIndex = 33
    End Sub

    Sub New(ByVal aSection As SECTION)
        MyBase.New(New ArchetypeComposite(aSection))
        Me.Item.Occurrences = New Count(1, 1)
        MyBase.ImageIndex = 1
        MyBase.SelectedImageIndex = 3
    End Sub

    Sub New(ByVal aSection As RM_Structure_Compound)
        MyBase.New(New ArchetypeComposite(aSection))
        Me.Item.Occurrences = aSection.Occurrences
        MyBase.ImageIndex = 1
        MyBase.SelectedImageIndex = 3
    End Sub

    Sub New(ByVal aNode As ArchetypeNode)
        MyBase.New(New ArchetypeComposite(aNode))
        Me.Item.Occurrences = aNode.Occurrences
        MyBase.ImageIndex = 32
        MyBase.SelectedImageIndex = 33
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
'The Original Code is ArchetypeCompoundTreeNode.vb.
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