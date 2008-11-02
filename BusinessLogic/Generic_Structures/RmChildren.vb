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

Option Strict On

Public MustInherit Class RmChildren
    Inherits CollectionBase

    Public ReadOnly Property Fixed() As Boolean
        Get
            Return Not mCardinality.IsUnbounded
        End Get
    End Property

    Protected mCardinality As New RmCardinality(0)
    Protected mExistence As New RmExistence 'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1

    Public Property Cardinality() As RmCardinality
        Get
            Return mCardinality
        End Get
        Set(ByVal Value As RmCardinality)
            mCardinality = Value
        End Set
    End Property

    Public Property Existence() As RmExistence 'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        Get
            Return mExistence
        End Get
        Set(ByVal value As RmExistence)
            mExistence = value
        End Set
    End Property

    Public Sub Add(ByVal an_RM_Structure As RmStructure)
        Me.List.Add(an_RM_Structure)
    End Sub

    Public Function GetChildByNodeId(ByVal aNodeId As String) As RmStructure

        For Each child As RmStructure In Me.List
            If child.NodeId = aNodeId Then
                Return child
            End If

            If TypeOf child Is RmStructureCompound Then
                Dim compoundChild As RmStructureCompound = CType(child, RmStructureCompound)
                Dim deepChild As RmStructure = compoundChild.GetChildByNodeId(aNodeId)

                If Not deepChild Is Nothing Then
                    Return deepChild
                End If
            End If
        Next
        Return Nothing
    End Function

End Class

Public Class Children

    Inherits RmChildren

    Private boolOrdered As Boolean = True
    Private mParentStructureType As StructureType

    Public ReadOnly Property items() As RmStructure()
        Get
            Dim rm(MyBase.List.Count - 1) As RmStructure
            Dim i As Integer
            For i = 0 To MyBase.List.Count - 1
                rm(i) = CType(MyBase.List.Item(i), RmStructure)
            Next
            Return rm
        End Get
    End Property

    ReadOnly Property FirstElementOrElementSlot() As RmStructure
        Get
            Dim i As Integer

            For i = 0 To MyBase.List.Count - 1
                Select Case Me.items(i).Type
                    Case StructureType.Element

                        Return CType(Me.items(i), RmElement)

                    Case StructureType.Cluster

                        Dim rm As RmStructure
                        rm = CType(Me.items(i), RmCluster).Children.FirstElementOrElementSlot
                        If Not rm Is Nothing Then
                            Return rm
                        End If

                    Case StructureType.Slot
                        Dim aSlot As RmSlot = CType(Me.items(i), RmSlot)
                        If aSlot.Type = StructureType.Element Then
                            Return aSlot
                        End If
                End Select

            Next

            Return Nothing
        End Get
    End Property

    Public Function copy() As Children
        Dim child As New Children(mParentStructureType)
        Dim rm As RmStructure

        For Each rm In Me.items
            Dim rm1 As RmStructure
            rm1 = rm.Copy
            child.Add(rm)
        Next

        Return child
    End Function

    Public Shadows Sub Add(ByVal an_RM_Structure As RmStructure)
        If an_RM_Structure IsNot Nothing AndAlso ReferenceModel.IsValidChild(mParentStructureType, an_RM_Structure.Type) Then
            'Is valid child traps post condition of false as should not arise
            MyBase.List.Add(an_RM_Structure)
        End If
    End Sub

    Sub New(ByVal ParentStructureType As StructureType)
        mParentStructureType = ParentStructureType
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
'The Original Code is RmChildren.vb.
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

