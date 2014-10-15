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

Option Strict On

Public Class ArchetypeTreeNode : Inherits TreeNode

    Private mArchetypeNode As ArchetypeNode

    Public ReadOnly Property Item() As ArchetypeNode
        Get
            Debug.Assert(Not mArchetypeNode Is Nothing)

            Return mArchetypeNode
        End Get
    End Property

    Public ReadOnly Property TypeName() As String
        Get
            Return mArchetypeNode.RM_Class.Type.ToString
        End Get
    End Property

    Public ReadOnly Property RM_Class() As RmStructure
        Get
            Return mArchetypeNode.RM_Class
        End Get
    End Property

    Public Property Description() As String
        Get
            Debug.Assert(Not mArchetypeNode.IsAnonymous)
            Return CType(mArchetypeNode, ArchetypeNodeAbstract).Description
        End Get
        Set(ByVal Value As String)
            Debug.Assert(Not mArchetypeNode.IsAnonymous)
            CType(mArchetypeNode, ArchetypeNodeAbstract).Description = Value
        End Set
    End Property

    Public ReadOnly Property RuntimeNameText() As String
        Get
            Debug.Assert(Not mArchetypeNode.IsAnonymous)
            Return CType(mArchetypeNode, ArchetypeNodeAbstract).RuntimeNameText
        End Get
    End Property

    Public Property Occurrences() As RmCardinality
        Get
            Return mArchetypeNode.Occurrences
        End Get
        Set(ByVal Value As RmCardinality)
            mArchetypeNode.Occurrences = Value
        End Set
    End Property

    Public ReadOnly Property NodeId() As String
        Get
            Debug.Assert(Not mArchetypeNode.IsAnonymous)
            Return CType(mArchetypeNode, ArchetypeNodeAbstract).NodeId
        End Get
    End Property

    Public Sub Translate()
        Text = mArchetypeNode.Text
    End Sub

    Public Sub RefreshIcons()
        ImageIndex = Item.ImageIndex(False)
        SelectedImageIndex = Item.ImageIndex(True)
    End Sub

    Public Sub Specialise()
        If Not mArchetypeNode.IsAnonymous Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Specialise()
            Text = mArchetypeNode.Text
        End If

        RefreshIcons()
    End Sub

       
    Public Function SpecialisedClone(ByVal fileManager As FileManagerLocal) As ArchetypeTreeNode
        Dim result As ArchetypeTreeNode
        Dim composite As ArchetypeComposite = TryCast(mArchetypeNode, ArchetypeComposite)

        If composite Is Nothing Then
            result = New ArchetypeTreeNode(mArchetypeNode)
        Else
            result = New ArchetypeTreeNode(composite)

            For Each child As ArchetypeTreeNode In Nodes
                Dim clone As ArchetypeTreeNode
                
               	Dim el As RmElement = TryCast(child.Item.RM_Class, RmElement)
             
                If el Is Nothing OrElse Not child.Item.CanSpecialise() Then
                    clone = child.SpecialisedClone(fileManager)
                Else
                
                	' Corrected old clonig method which required Internal references
                	' 	 Dim ref As RmReference = New RmReference(el)
                	clone = New ArchetypeTreeNode(el, fileManager)
                    clone.RefreshIcons()
                End If

                result.Nodes.Add(clone)
            Next

            result.Expand()
        End If

        result.Specialise()
        Return result
    End Function

    Public Sub New(ByVal node As ArchetypeNode)
        MyBase.New(node.Text)
        mArchetypeNode = node.Copy
    End Sub

    Sub New(ByVal text As String, ByVal type As StructureType, ByVal fileManager As FileManagerLocal)
        MyBase.New(text)

        Select Case type
            Case StructureType.Element
                mArchetypeNode = New ArchetypeElement(text, fileManager)
            Case StructureType.Cluster
                mArchetypeNode = New ArchetypeComposite(text, type, fileManager)
            Case StructureType.SECTION
                mArchetypeNode = New ArchetypeComposite(text, type, fileManager)
                ImageIndex = 1
                SelectedImageIndex = 3
            Case Else
                Debug.Assert(False, String.Format("Type {0} is not handled", type.ToString.ToUpper))
        End Select

        Item.Occurrences.MaxCount = 1
    End Sub

    Sub New(ByVal cluster As RmCluster, ByVal fileManager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeComposite(cluster, fileManager)
        Text = mArchetypeNode.Text
    End Sub

    Sub New(ByVal cluster As ArchetypeComposite)
        MyBase.New()
        mArchetypeNode = cluster.Copy
        Text = mArchetypeNode.Text
    End Sub

    Sub New(ByVal section As RmSection, ByVal fileManager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeComposite(section, fileManager)
        Text = mArchetypeNode.Text
        ImageIndex = 1
        SelectedImageIndex = 3
    End Sub

    Sub New(ByVal el As RmElement, ByVal fileManager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeElement(el, fileManager)
        Text = mArchetypeNode.Text
    End Sub

    Sub New(ByVal slot As RmSlot, ByVal fileManager As FileManagerLocal)
        MyBase.New()

        If slot.NodeId <> "" Then
            mArchetypeNode = New ArchetypeSlot(slot, fileManager)
        Else
            mArchetypeNode = New ArchetypeNodeAnonymous(slot)
        End If

        Text = mArchetypeNode.Text
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
'The Original Code is ArchetypeTreeNode.vb.
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

