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
'	file:        "$Source: source/vb.net/archetype_editor/BusinessLogic/Generic_Structures/SCCS/s.RmStructureCompound.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'
Option Explicit On
Option Strict On
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports XMLParser

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
        Return rm
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
        'colChildren.Existence = archetype_composite.existence 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
    End Sub

    Sub New(ByVal NodeId As String, ByVal a_structure As StructureType)
        MyBase.New(NodeId, a_structure)
        colChildren = New Children(mType)
    End Sub

#Region "ADL oriented features"

    Sub New(ByVal EIF_Structure As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(EIF_Structure)
        colChildren = New Children(mType)

        'mType is set by the RmStructure class

        Select Case mType
            Case StructureType.Single
                ProcessSimple(EIF_Structure, a_filemanager)
            Case StructureType.List
                ProcessList(EIF_Structure, a_filemanager)
                ArchetypeEditor.ADL_Classes.ADL_Tools.HighestLevelChildren = Me.Children
                ArchetypeEditor.ADL_Classes.ADL_Tools.PopulateReferences(Me)
            Case StructureType.Tree
                ProcessTree(EIF_Structure, a_filemanager)
                ArchetypeEditor.ADL_Classes.ADL_Tools.HighestLevelChildren = Me.Children
                ArchetypeEditor.ADL_Classes.ADL_Tools.PopulateReferences(Me)
            Case StructureType.Cluster, StructureType.History, StructureType.SECTION, StructureType.Table, StructureType.Activity
                Return  'code is dealt with in the specialised classes
            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Sub New(ByVal EIF_Attribute As AdlParser.CAttribute, ByVal a_structure_type As StructureType, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(a_structure_type.ToString, a_structure_type) 'State, Data, Protocol, ism_transition
        Debug.Assert(a_structure_type = StructureType.Data Or _
            a_structure_type = StructureType.State Or _
            a_structure_type = StructureType.Protocol Or _
            a_structure_type = StructureType.ISM_TRANSITION Or _
            a_structure_type = StructureType.ActivityDescription Or _
            a_structure_type = StructureType.Activities Or _
            a_structure_type = StructureType.OtherParticipations)

        colChildren = New Children(mType)
        ProcessData(EIF_Attribute, a_filemanager)
    End Sub

    Private Sub ProcessList(ByVal ObjNode As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
        Dim i As Integer

        For i = 1 To ObjNode.attributes.count
            Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

            Select Case attribute.RmAttributeName.ToCil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "name", "runtime_label" 'runtime_label is obsolete
                    If attribute.HasChildren Then
                        mRunTimeConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(attribute.Children.First, AdlParser.CComplexObject))
                    End If
                Case "items"
                    Dim ii As Integer
                    'Set whether the list is ordered or not
                    colChildren.Cardinality.SetFromOpenEHRCardinality(attribute.Cardinality)
                    colChildren.Existence.SetFromOpenEHRExistence(attribute.Existence)

                    For ii = 1 To attribute.Children.Count
                        Dim a_ComplexObject As AdlParser.CComplexObject

                        Select Case CType(attribute.Children.ITh(ii), AdlParser.CObject).GeneratingType.Out.ToCil
                            Case "C_COMPLEX_OBJECT"
                                a_ComplexObject = CType(attribute.Children.ITh(ii), AdlParser.CComplexObject)
                                colChildren.Add(New ArchetypeEditor.ADL_Classes.ADL_RmElement(a_ComplexObject, a_filemanager))
                            Case "ARCHETYPE_SLOT"
                                colChildren.Add(New RmSlot(CType(attribute.Children.ITh(ii), AdlParser.ArchetypeSlot)))
                            Case "ARCHETYPE_INTERNAL_REF"
                                colChildren.Add(ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessReference(CType(attribute.Children.ITh(ii), AdlParser.ArchetypeInternalRef)))
                        End Select
                    Next
                Case Else
                    Debug.Assert(False, attribute.RmAttributeName.ToCil & " not handled")
            End Select
        Next
    End Sub

    Private Sub ProcessSimple(ByVal ObjNode As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
        Dim i As Integer

        For i = 1 To ObjNode.attributes.count
            Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

            Select Case attribute.RmAttributeName.ToCil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "name", "runtime_label" ' runtime_label is obsolete
                    If attribute.HasChildren Then
                        mRunTimeConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(attribute.Children.First, AdlParser.CComplexObject))
                    End If
                Case "item"
                    colChildren.Existence.SetFromOpenEHRExistence(attribute.Existence)

                    If attribute.HasChildren Then
                        Select Case CType(attribute.Children.First, AdlParser.CObject).GeneratingType.Out.ToCil.ToUpperInvariant()
                            Case "C_COMPLEX_OBJECT"
                                colChildren.Add(New ArchetypeEditor.ADL_Classes.ADL_RmElement(CType(attribute.Children.First, AdlParser.CComplexObject), a_filemanager))
                            Case "ARCHETYPE_SLOT"
                                colChildren.Add(New RmSlot(CType(attribute.Children.First, AdlParser.ArchetypeSlot)))
                        End Select
                    End If
            End Select
        Next
    End Sub

    Protected Sub ProcessTree(ByVal ObjNode As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)

        If ObjNode.HasAttribute(Eiffel.String("items")) Then
            Dim an_attribute As AdlParser.CAttribute
            Dim i As Integer

            an_attribute = ObjNode.CAttributeAtPath(Eiffel.String("items"))

            ArchetypeEditor.ADL_Classes.ADL_Tools.SetCardinality(an_attribute.cardinality, colChildren)
            ArchetypeEditor.ADL_Classes.ADL_Tools.SetExistence(an_attribute.existence, colChildren) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

            For i = 1 To an_attribute.children.count
                Dim a_ComplexObject As AdlParser.CComplexObject
                Select Case CType(an_attribute.Children.ITh(i), AdlParser.CObject).GeneratingType.Out.ToCil.ToUpperInvariant()
                    Case "C_COMPLEX_OBJECT"
                        a_ComplexObject = CType(an_attribute.Children.ITh(i), AdlParser.CComplexObject)
                        Dim structure_type As StructureType

                        structure_type = ReferenceModel.StructureTypeFromString(a_ComplexObject.RmTypeName.ToCil)

                        Select Case structure_type
                            Case StructureType.Cluster
                                colChildren.Add(New RmCluster(a_ComplexObject, a_filemanager))
                            Case StructureType.Element
                                colChildren.Add(New ArchetypeEditor.ADL_Classes.ADL_RmElement(a_ComplexObject, a_filemanager))
                        End Select
                    Case "ARCHETYPE_SLOT"
                        colChildren.Add(New RmSlot(CType(an_attribute.Children.ITh(i), AdlParser.ArchetypeSlot)))
                    Case "ARCHETYPE_INTERNAL_REF"
                        colChildren.Add(ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessReference(CType(an_attribute.Children.ITh(i), AdlParser.ArchetypeInternalRef)))
                End Select
            Next
        End If

    End Sub

    Private Sub ProcessData(ByVal data_rel_node As AdlParser.CAttribute, ByVal a_filemanager As FileManagerLocal)
        Dim i As Integer
        Dim structure_type As StructureType

        For i = 1 To data_rel_node.children.count
            Dim ObjNode As AdlParser.CObject = CType(data_rel_node.Children.ITh(i), AdlParser.CObject)
            structure_type = ReferenceModel.StructureTypeFromString(ObjNode.RmTypeName.ToCil)

            Select Case ObjNode.GeneratingType.Out.ToCil
                ' may be a slot or a complex type
                Case "C_COMPLEX_OBJECT"
                    Select Case structure_type
                        Case StructureType.History
                            colChildren.Add(New RmHistory(CType(ObjNode, AdlParser.CComplexObject), a_filemanager))
                        Case StructureType.Single, StructureType.List, StructureType.Tree
                            ' a structure
                            colChildren.Add(New RmStructureCompound(CType(ObjNode, AdlParser.CComplexObject), a_filemanager))
                        Case StructureType.Table
                            colChildren.Add(New RmTable(CType(ObjNode, AdlParser.CComplexObject), a_filemanager))
                        Case StructureType.ISM_TRANSITION, StructureType.CarePathwayStep
                            'need to get the node_id from the workflow step to get the text displayed
                            'make sure there is a valid node_id for the careflow_step
                            Dim eif_string As EiffelKernel.String_8
                            eif_string = Eiffel.String("careflow_step")

                            If CType(ObjNode, AdlParser.CComplexObject).HasAttribute(eif_string) Then
                                Dim attribute As AdlParser.CAttribute = CType(ObjNode, AdlParser.CComplexObject).CAttributeAtPath(eif_string)

                                If Not attribute Is Nothing AndAlso attribute.HasChildren Then
                                    Dim node_id As String
                                    Dim codedText As AdlParser.CComplexObject = CType(attribute.Children.First, AdlParser.CComplexObject)
                                    Dim t As Constraint_Text = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(codedText)
                                    node_id = t.AllowableValues.FirstCode

                                    If RmTerm.IsValidTermCode(node_id) Then
                                        colChildren.Add(New RmPathwayStep(node_id, CType(ObjNode, AdlParser.CComplexObject)))
                                    End If
                                End If
                            End If
                        Case StructureType.Activity
                            colChildren.Add(New RmActivity(CType(ObjNode, AdlParser.CComplexObject), a_filemanager))
                        Case StructureType.Participation
                            colChildren.Add(New RmParticipation(CType(ObjNode, AdlParser.CComplexObject)))
                        Case Else
                            Debug.Assert(False)
                    End Select
                Case "ARCHETYPE_SLOT"
                    colChildren.Add(New RmSlot(CType(ObjNode, AdlParser.ArchetypeSlot)))
            End Select
        Next
    End Sub

#End Region

#Region "Processing XML"

    Sub New(ByVal XML_Structure As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(XML_Structure)
        colChildren = New Children(mType)

        'mType is set by the RmStructure class

        Select Case mType
            Case StructureType.Single
                ProcessSimple(XML_Structure, a_filemanager)
            Case StructureType.List
                ProcessList(XML_Structure, a_filemanager)
                ArchetypeEditor.XML_Classes.XML_Tools.HighestLevelChildren = Me.Children
                ArchetypeEditor.XML_Classes.XML_Tools.PopulateReferences(Me)
            Case StructureType.Tree
                ProcessTree(XML_Structure, a_filemanager)
                ArchetypeEditor.XML_Classes.XML_Tools.HighestLevelChildren = Me.Children
                ArchetypeEditor.XML_Classes.XML_Tools.PopulateReferences(Me)
            Case StructureType.Cluster, StructureType.History, StructureType.SECTION, StructureType.Table, StructureType.Activity
                Return  'code is dealt with in the specialised classes
            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Sub New(ByVal XML_Attribute As XMLParser.C_ATTRIBUTE, ByVal a_structure_type As StructureType, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(a_structure_type.ToString, a_structure_type) 'State, Data, Protocol, ism_transition
        Debug.Assert(a_structure_type = StructureType.Data Or _
            a_structure_type = StructureType.State Or _
            a_structure_type = StructureType.Protocol Or _
            a_structure_type = StructureType.ISM_TRANSITION Or _
            a_structure_type = StructureType.ActivityDescription Or _
            a_structure_type = StructureType.Activities Or _
            a_structure_type = StructureType.OtherParticipations)
        colChildren = New Children(mType)
        ProcessData(XML_Attribute, a_filemanager)
    End Sub

    Private Sub ProcessList(ByVal ObjNode As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        Dim an_attribute As XMLParser.C_ATTRIBUTE

        For Each an_attribute In ObjNode.attributes
            Select Case an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "name", "runtime_label" 'runtime_label is obsolete
                    mRunTimeConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                Case "items"
                    'Set whether the list is ordered or not
                    colChildren.Cardinality.SetFromXmlCardinality(CType(an_attribute, XMLParser.C_MULTIPLE_ATTRIBUTE).cardinality)
                    colChildren.Existence.SetFromXmlExistence(CType(an_attribute, XMLParser.C_MULTIPLE_ATTRIBUTE).existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

                    If Not an_attribute.children Is Nothing Then
                        For Each co As XMLParser.C_OBJECT In an_attribute.children
                            Select Case co.GetType.ToString.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                                Case "xmlparser.c_complex_object"
                                    colChildren.Add(New ArchetypeEditor.XML_Classes.XML_RmElement(CType(co, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                                Case "xmlparser.archetype_slot"
                                    colChildren.Add(New RmSlot(CType(co, XMLParser.ARCHETYPE_SLOT)))
                                Case "xmlparser.archetype_internal_ref"
                                    colChildren.Add(ArchetypeEditor.XML_Classes.XML_Tools.ProcessReference(CType(co, XMLParser.ARCHETYPE_INTERNAL_REF)))
                            End Select
                        Next
                    End If
                Case Else
                    Debug.Assert(False, an_attribute.rm_attribute_name & " not handled")
            End Select
        Next
    End Sub

    Private Sub ProcessSimple(ByVal ObjNode As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        Dim an_attribute As XMLParser.C_ATTRIBUTE

        For Each an_attribute In ObjNode.attributes
            Select Case an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "name", "runtime_label" ' runtime_label is obsolete
                    mRunTimeConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                Case "item"
                    Dim co As XMLParser.C_OBJECT = an_attribute.children(0)
                    colChildren.Existence.SetFromXmlExistence(CType(an_attribute, XMLParser.C_SINGLE_ATTRIBUTE).existence) 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    Select Case co.GetType.ToString.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "xmlparser.c_complex_object"
                            colChildren.Add(New ArchetypeEditor.XML_Classes.XML_RmElement(CType(co, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                        Case "xmlparser.archetype_slot"
                            colChildren.Add(New RmSlot(CType(co, XMLParser.ARCHETYPE_SLOT)))
                    End Select
            End Select
        Next
    End Sub

    Protected Sub ProcessTree(ByVal ObjNode As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
        Dim complexObject As New C_COMPLEX_OBJECT_PROXY(ObjNode)
        'If Not ObjNode.AnyAllowed AndAlso Not ObjNode.attributes Is Nothing Then
        If Not complexObject.Any_Allowed AndAlso Not ObjNode.attributes Is Nothing Then
            For Each an_attribute As XMLParser.C_ATTRIBUTE In ObjNode.attributes

                If an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture) = "items" Then
                    'for items - that is content only
                    ArchetypeEditor.XML_Classes.XML_Tools.SetCardinality(CType(an_attribute, XMLParser.C_MULTIPLE_ATTRIBUTE).cardinality, colChildren)

                    For Each cObject As XMLParser.C_OBJECT In CType(an_attribute, XMLParser.C_MULTIPLE_ATTRIBUTE).children
                        Select Case cObject.GetType.ToString.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                            Case "xmlparser.c_complex_object"
                                Dim a_ComplexObject As XMLParser.C_COMPLEX_OBJECT

                                a_ComplexObject = CType(cObject, XMLParser.C_COMPLEX_OBJECT)
                                Dim structure_type As StructureType

                                structure_type = ReferenceModel.StructureTypeFromString(a_ComplexObject.rm_type_name)

                                Select Case structure_type
                                    Case StructureType.Cluster
                                        colChildren.Add(New RmCluster(a_ComplexObject, a_filemanager))
                                    Case StructureType.Element
                                        colChildren.Add(New ArchetypeEditor.XML_Classes.XML_RmElement(a_ComplexObject, a_filemanager))
                                End Select
                            Case "xmlparser.archetype_slot"
                                colChildren.Add(New RmSlot(CType(cObject, XMLParser.ARCHETYPE_SLOT)))
                            Case "xmlparser.archetype_internal_ref"
                                colChildren.Add(ArchetypeEditor.XML_Classes.XML_Tools.ProcessReference(CType(cObject, XMLParser.ARCHETYPE_INTERNAL_REF)))
                        End Select
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub ProcessData(ByVal data_rel_node As XMLParser.C_ATTRIBUTE, ByVal a_filemanager As FileManagerLocal)
        Dim ObjNode As XMLParser.C_OBJECT
        Dim structure_type As StructureType
        Try
            For Each ObjNode In data_rel_node.children

                structure_type = ReferenceModel.StructureTypeFromString(ObjNode.rm_type_name)

                Select Case ObjNode.GetType.ToString.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    ' may be a slot or a complex type
                    Case "xmlparser.c_complex_object"
                        Select Case structure_type
                            Case StructureType.History
                                colChildren.Add(New RmHistory(CType(ObjNode, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                            Case StructureType.Single, StructureType.List, StructureType.Tree
                                ' a structure
                                colChildren.Add(New RmStructureCompound(CType(ObjNode, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                            Case StructureType.Table
                                colChildren.Add(New RmTable(CType(ObjNode, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                            Case StructureType.ISM_TRANSITION, StructureType.CarePathwayStep
                                'need to get the node_id from the workflow step to get the text displayed
                                For Each attribute As XMLParser.C_ATTRIBUTE In CType(ObjNode, XMLParser.C_COMPLEX_OBJECT).attributes
                                    If attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture) = "careflow_step" Then
                                        Dim node_id As String
                                        Dim codedText As XMLParser.C_COMPLEX_OBJECT = CType(attribute.children(0), XMLParser.C_COMPLEX_OBJECT)
                                        Dim t As Constraint_Text = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(codedText)
                                        node_id = t.AllowableValues.FirstCode

                                        If RmTerm.IsValidTermCode(node_id) Then
                                            colChildren.Add(New RmPathwayStep(node_id, CType(ObjNode, XMLParser.C_COMPLEX_OBJECT)))
                                        End If
                                    End If
                                Next

                            Case StructureType.Activity
                                colChildren.Add(New RmActivity(CType(ObjNode, XMLParser.C_COMPLEX_OBJECT), a_filemanager))

                            Case StructureType.Participation
                                colChildren.Add(New RmParticipation(CType(ObjNode, XMLParser.C_COMPLEX_OBJECT))) ', a_filemanager))

                            Case Else
                                Debug.Assert(False)
                        End Select
                    Case "xmlparser.archetype_slot"
                        colChildren.Add(New RmSlot(CType(ObjNode, XMLParser.ARCHETYPE_SLOT)))
                End Select

            Next
        Catch ex As Exception
            Debug.Assert(True)
        End Try
    End Sub

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
