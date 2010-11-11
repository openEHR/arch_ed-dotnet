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
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
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

    Public Shadows Property Text() As String
        Get
            Return mArchetypeNode.Text
        End Get
        Set(ByVal Value As String)
            mArchetypeNode.Text = Value
            If TypeOf (mArchetypeNode) Is ArchetypeSlot Then
                MyBase.Text = mArchetypeNode.Text
            Else
                MyBase.Text = Value
            End If
        End Set
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

    Public Function Copy(ByVal a_file_manager As FileManagerLocal) As ArchetypeTreeNode
        Select Case mArchetypeNode.RM_Class.Type
            Case StructureType.SECTION
                Return New ArchetypeTreeNode(CType(mArchetypeNode, RmSection), a_file_manager)
            Case StructureType.Cluster
                Return New ArchetypeTreeNode(CType(mArchetypeNode, ArchetypeComposite))
            Case StructureType.Element
                'Return New ArchetypeTreeNode(CType(mArchetypeNode, ArchetypeElement), a_file_manager)
                Return New ArchetypeTreeNode(mArchetypeNode)
            Case StructureType.Reference
                Return New ArchetypeTreeNode(mArchetypeNode)
            Case StructureType.Slot
                If (TypeOf mArchetypeNode Is ArchetypeSlot) Then
                    Return (New ArchetypeTreeNode(mArchetypeNode))
                Else
                    Return Nothing
                End If
            Case Else
                Debug.Assert(False, "Type not handled")
                Return Nothing
        End Select
    End Function

    Public Sub Translate()
        mArchetypeNode.Translate()
        MyBase.Text = mArchetypeNode.Text
    End Sub

    Public Sub Specialise()
        If Not mArchetypeNode.IsAnonymous Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Specialise()
            MyBase.Text = mArchetypeNode.Text
        Else
            Debug.Assert(False)
        End If
    End Sub

    Private Sub SetImageIndex()
        Select Case mArchetypeNode.RM_Class.Type
            Case StructureType.Cluster
                MyBase.ImageIndex = 76
                MyBase.SelectedImageIndex = 77
            Case StructureType.SECTION
                MyBase.ImageIndex = 1
                MyBase.SelectedImageIndex = 3
        End Select
    End Sub

    Public Sub New(ByVal aArchetypeNode As ArchetypeNode)
        MyBase.New(aArchetypeNode.Text)
        mArchetypeNode = aArchetypeNode.Copy

        If mArchetypeNode.RM_Class.Type = StructureType.Cluster AndAlso CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount < 1 Then
            CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount = 1
        End If
        SetImageIndex()
    End Sub

    Sub New(ByVal aText As String, ByVal a_type As StructureType, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(aText)
        Select Case a_type
            Case StructureType.Element
                mArchetypeNode = New ArchetypeElement(aText, a_file_manager)
            Case StructureType.Cluster
                mArchetypeNode = New ArchetypeComposite(aText, a_type, a_file_manager)
                If CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount < 1 Then
                    CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount = 1
                End If
                SetImageIndex()
            Case StructureType.SECTION
                mArchetypeNode = New ArchetypeComposite(aText, a_type, a_file_manager)
                SetImageIndex()
            Case Else
                Debug.Assert(False, String.Format("Type {0} is not handled", a_type.ToString.ToUpper))
                'Case StructureType.Slot
                '    mArchetypeNode = New ArchetypeSlot(aText, a_file_manager)
                '    'mArchetypeNode = New ArchetypeNodeAnonymous(a_type)
        End Select

        Me.Item.Occurrences.MaxCount = 1

    End Sub

    Sub New(ByVal aCluster As RmCluster, ByVal a_file_manager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeComposite(aCluster, a_file_manager)
        If CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount < 1 Then
            CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount = 1
        End If
        MyBase.Text = mArchetypeNode.Text
        SetImageIndex()
    End Sub

    Sub New(ByVal aCluster As ArchetypeComposite)
        MyBase.New()
        mArchetypeNode = aCluster.Copy
        MyBase.Text = mArchetypeNode.Text
        If CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount < 1 Then
            CType(mArchetypeNode, ArchetypeComposite).Cardinality.MinCount = 1
        End If
        SetImageIndex()
    End Sub

    Sub New(ByVal aSection As RmSection, ByVal a_file_manager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeComposite(aSection, a_file_manager)
        MyBase.Text = mArchetypeNode.Text
        'Me.Item.Occurrences = New Count(1, 1)
        SetImageIndex()
    End Sub

    Sub New(ByVal aSection As RmStructureCompound, ByVal a_file_manager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeComposite(aSection, a_file_manager)
        MyBase.Text = mArchetypeNode.Text
        'Me.Item.Occurrences = aSection.Occurrences
        SetImageIndex()
    End Sub

    Sub New(ByVal el As RmElement, ByVal a_file_manager As FileManagerLocal)
        MyBase.New()
        mArchetypeNode = New ArchetypeElement(el, a_file_manager)
        MyBase.Text = mArchetypeNode.Text
        'Me.Item.Occurrences = el.Occurrences
    End Sub

    Sub New(ByVal a_slot As RmSlot, ByVal a_file_manager As FileManagerLocal)
        'SRH: Changed 18 Aug 2008
        MyBase.New()
        If a_slot.NodeId <> String.Empty Then
            mArchetypeNode = New ArchetypeSlot(a_slot, a_file_manager)
            MyBase.Text = mArchetypeNode.Text
        Else
            MyBase.Text = a_file_manager.OntologyManager.GetOpenEHRTerm(CInt(a_slot.SlotConstraint.RM_ClassType), a_slot.SlotConstraint.RM_ClassType.ToString)
            mArchetypeNode = New ArchetypeNodeAnonymous(a_slot)
        End If

        'MyBase.New(a_file_manager.OntologyManager.GetOpenEHRTerm(CInt(a_slot.SlotConstraint.RM_ClassType), a_slot.SlotConstraint.RM_ClassType.ToString))
        'mArchetypeNode = New ArchetypeNodeAnonymous(a_slot)
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

