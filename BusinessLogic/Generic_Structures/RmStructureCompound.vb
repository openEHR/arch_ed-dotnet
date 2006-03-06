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
'	file:        "$Source: source/vb.net/archetype_editor/BusinessLogic/Generic_Structures/SCCS/s.RmStructureCompound.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class RmStructureCompound
    Inherits RmStructure
    Implements ArchetypeDefinition  ' allows archetyping of entry structures

    Private colChildren As Children


    Public Overrides ReadOnly Property Type() As StructureType
        Get
            Return mType
        End Get
    End Property

    Public Property Children() As Children Implements ArchetypeDefinition.Data
        Get
            Return colChildren
        End Get
        Set(ByVal value As Children)
            colChildren = value
        End Set
    End Property

    Public Overrides Function Copy() As RmStructure
        Dim rm As New RmStructureCompound(Me.NodeId, mType)

        rm.cOccurrences = Me.cOccurrences.Copy
        rm.colChildren = Me.colChildren.copy
    End Function

    Public Overridable Function GetChildByNodeId(ByVal aNodeId As String) As RmStructure Implements ArchetypeDefinition.GetChildByNodeId
        Return Me.Children.GetChildByNodeId(aNodeId)
    End Function

    Sub New(ByVal rm As RmStructure)
        MyBase.New(rm)
        colChildren = New Children(mType)
    End Sub

    Sub New(ByVal archetype_composite As ArchetypeComposite)
        MyBase.New(archetype_composite.RM_Class)
        colChildren = New Children(mType)
        colChildren.Cardinality = archetype_composite.Cardinality
        colChildren.Cardinality.Ordered = archetype_composite.IsOrdered
    End Sub


    Sub New(ByVal NodeId As String, ByVal a_structure As StructureType)
        MyBase.New(NodeId, a_structure)
        colChildren = New Children(mType)
    End Sub


#Region "ADL oriented features"

    Sub New(ByVal EIF_Structure As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(EIF_Structure)
        colChildren = New Children(mType)

        'mType is set by the RmStructure class

        Select Case mType
            Case StructureType.Single
                ProcessSimple(EIF_Structure)
            Case StructureType.List
                ProcessList(EIF_Structure)
                ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.HighestLevelChildren = Me.Children
                ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.PopulateReferences(Me)
            Case StructureType.Tree
                ProcessTree(EIF_Structure)
                ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.HighestLevelChildren = Me.Children
                ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.PopulateReferences(Me)
            Case StructureType.Cluster, StructureType.History, StructureType.SECTION, StructureType.Table, StructureType.Activity
                Return  'code is dealt with in the specialised classes
            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Sub New(ByVal EIF_Attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal a_structure_type As StructureType)
        MyBase.New(a_structure_type.ToString, a_structure_type) 'State, Data, Protocol, ism_transition
        Debug.Assert(a_structure_type = StructureType.Data Or _
            a_structure_type = StructureType.State Or _
            a_structure_type = StructureType.Protocol Or _
            a_structure_type = StructureType.ism_transition Or _
            a_structure_type = StructureType.ActivityDescription Or _
            a_structure_type = StructureType.Activities)
        colChildren = New Children(mType)
        ProcessData(EIF_Attribute)
    End Sub

#Region "Processing ADL - incoming"

    Private Sub ProcessList(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim i As Integer

        For i = 1 To ObjNode.attributes.count
            an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "name", "runtime_label" 'runtime_label is obsolete
                    mRuntimeConstraint = RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "items"
                    Dim ii As Integer
                    'Set whether the list is ordered or not
                    colChildren.Cardinality.SetFromOpenEHRCardinality(an_attribute.cardinality)
                    For ii = 1 To an_attribute.children.count
                        Dim a_ComplexObject As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                        Select Case CType(an_attribute.children.i_th(ii), openehr.openehr.am.archetype.constraint_model.C_OBJECT).Generating_Type.to_cil
                            Case "C_COMPLEX_OBJECT"
                                a_ComplexObject = CType(an_attribute.children.i_th(ii), openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                                colChildren.Add(New RmElement(a_ComplexObject))
                            Case "ARCHETYPE_INTERNAL_REF"
                                colChildren.Add(ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.ProcessReference(CType(an_attribute.children.i_th(ii), openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF)))
                        End Select
                    Next
                    'Case "ordered", "Ordered", "ORDERED"
                    '    Dim cadlObjSimple As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                    '    Dim c_Boolean As openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN

                    '    cadlObjSimple = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                    '    If cadlObjSimple.Rm_Type_Name.to_cil = "BOOLEAN" Then
                    '        c_Boolean = CType(cadlObjSimple.Item, openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN)
                    '        If c_Boolean.true_valid Then
                    '            colChildren.Ordered = True
                    '        End If
                    '    End If
                    'Case "count", "Count", "COUNT"
                    '    Dim cadlObjSimple As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    '    cadlObjSimple = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                    '    If cadlObjSimple.Rm_Type_Name.to_cil = "INTEGER" Then
                    '        colChildren.Cardinality = ADL_Tools.Instance.SetOccurrences(CType(cadlobjsimple.Item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER).Interval)
                    '    End If
                Case ReferenceModel.Instance.RM_StructureName(StructureType.CarePathwayStep)
                    Dim ii As Integer
                    For ii = 1 To an_attribute.children.count
                        ObjNode = CType(an_attribute.children.i_th(ii), openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                        colChildren.Add(New RmPathwayStep(ObjNode))
                    Next
                    Return
            End Select
        Next
    End Sub

    Private Sub ProcessSimple(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim i As Integer

        For i = 1 To ObjNode.attributes.count
            an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "name", "runtime_label" ' runtime_label is obsolete
                    mRuntimeConstraint = RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "item"
                    colChildren.Add(New RmElement(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
            End Select

        Next

    End Sub

    Protected Sub ProcessTree(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)

        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("items")) Then
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim i As Integer

            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("items"))

            Dim s As String = an_attribute.cardinality.as_string.to_cil
            i = s.IndexOf(";")
            If i > -1 Then
                colChildren.Cardinality.SetFromString(an_attribute.cardinality.as_string.to_cil.Substring(0, i))
                Select Case s.Substring(i + 1).ToLower(System.Globalization.CultureInfo.InvariantCulture).Trim()
                    Case "ordered"
                        colChildren.Cardinality.Ordered = True
                    Case "unordered"
                        colChildren.Cardinality.Ordered = False
                    Case Else
                        Debug.Assert(False, "Not handled")
                End Select
            Else
                colChildren.Cardinality.SetFromString(an_attribute.cardinality.as_string.to_cil)
            End If

            For i = 1 To an_attribute.children.count
                Dim a_ComplexObject As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                Select Case CType(an_attribute.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_OBJECT).generating_type.to_cil
                    Case "C_COMPLEX_OBJECT"
                        a_ComplexObject = CType(an_attribute.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                        Dim structure_type As StructureType

                        structure_type = ReferenceModel.Instance.StructureTypeFromString(a_ComplexObject.rm_type_name.to_cil)

                        Select Case structure_type
                            Case StructureType.Cluster
                                colChildren.Add(New RmCluster(a_ComplexObject))
                            Case StructureType.Element
                                colChildren.Add(New RmElement(a_ComplexObject))
                        End Select
                    Case "ARCHETYPE_INTERNAL_REF"
                        colChildren.Add(ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.ProcessReference(CType(an_attribute.children.i_th(i), openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF)))
                End Select
            Next
        End If

    End Sub

    Private Sub ProcessData(ByVal data_rel_node As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim ObjNode As openehr.openehr.am.archetype.constraint_model.C_OBJECT
        Dim i As Integer
        Dim structure_type As StructureType


        For i = 1 To data_rel_node.children.count

            ObjNode = CType(data_rel_node.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_OBJECT)
            structure_type = ReferenceModel.Instance.StructureTypeFromString(ObjNode.rm_type_name.to_cil)

            Select Case ObjNode.generating_type.to_cil
                ' may be a slot or a complex type
            Case "C_COMPLEX_OBJECT"
                    Select Case structure_type
                        Case StructureType.History
                            colChildren.Add(New RmHistory(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case StructureType.Single, StructureType.List, StructureType.Tree
                            ' a structure
                            colChildren.Add(New RmStructureCompound(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case StructureType.Table
                            colChildren.Add(New RmTable(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case StructureType.CarePathwayStep
                            colChildren.Add(New RmPathwayStep(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case StructureType.Activity
                            colChildren.Add(New RmActivity(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case Else
                            Debug.Assert(False)
                    End Select
                Case "ARCHETYPE_SLOT"
                    colChildren.Add(New RmSlot(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))
            End Select

        Next

    End Sub

#End Region

#Region "Building ADL - outgoing"

#End Region

#End Region


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
'The Original Code is RmStructureCompound.vb.
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
