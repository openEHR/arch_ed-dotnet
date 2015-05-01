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
Public Class ParsingTools

    ' The limit of the processing for references
    Private Shared mHighestLevelChildren As Children

    Public Shared Property HighestLevelChildren() As Children
        Get
            Return mHighestLevelChildren
        End Get
        Set(ByVal Value As Children)
            mHighestLevelChildren = Value
        End Set
    End Property

    ' Singleton
    Private Shared mRmDateStructure As RmStructureCompound

    Public Shared Property LastProcessedStructure() As RmStructureCompound
        ' may process a structure within a EventSeries - and need to save it
        ' as data...so remember it here
        Get
            Return mRmDateStructure
        End Get
        Set(ByVal value As RmStructureCompound)
            mRmDateStructure = value
        End Set
    End Property

    ' Singleton
    Private Shared mRmStateStructure As RmStructureCompound

    'Changed SRH: 1 September 2007 - to allow slots for state
    Public Shared Property StateStructure() As RmStructureCompound
        ' may process a state structure within a EventSeries - and need to save it
        ' as state...so remember it here
        Get
            Return mRmStateStructure
        End Get
        Set(ByVal value As RmStructureCompound)
            mRmStateStructure = value
        End Set
    End Property


    Private Shared Function getElementForReference(ByVal nodeid As String, ByVal the_Children As Children) As RmElement
        Dim rm As RmStructure

        For Each rm In the_Children
            If rm.NodeId = nodeid Then
                'can be multiple references and may have same node id
                If rm.Type = StructureType.Element Then
                    Return rm
                End If
            End If

            If TypeOf rm Is RmCluster Then
                Dim element As RmElement
                element = getElementForReference(nodeid, CType(rm, RmStructureCompound).Children)
                If Not element Is Nothing Then
                    Return element
                End If
            End If
        Next
        Return Nothing

    End Function

    Public Shared Sub PopulateReferences(ByVal rm As RmStructureCompound)
        Dim element As RmElement
        Dim a_structure As RmStructure

        For Each a_structure In rm.Children
            Select Case a_structure.Type
                Case StructureType.Reference
                    'If a_structure.TypeName = "Reference" Then

                    element = getElementForReference(a_structure.NodeId, mHighestLevelChildren)
                    If Not element Is Nothing Then
                        element.HasReferences = True
                        CType(a_structure, RmReference).SetElement(element)
                    Else
                        a_structure = Nothing
                    End If

                Case StructureType.Cluster

                    If CType(a_structure, RmCluster).Children.Count > 0 Then
                        PopulateReferences(a_structure)
                    End If

            End Select
        Next

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
'The Original Code is ParsingTools.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
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
