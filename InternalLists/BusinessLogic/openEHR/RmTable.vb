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
Option Explicit On 

Public Class RmTable : Inherits RmStructureCompound
    Private mRotated As Boolean
    Private mNumberKeyColumns As Integer = 0

    Property isRotated() As Boolean
        Get
            Return mRotated
        End Get
        Set(ByVal Value As Boolean)
            mRotated = Value
        End Set
    End Property
    Property NumberKeyColumns() As Integer
        Get
            Return mNumberKeyColumns
        End Get
        Set(ByVal Value As Integer)
            mNumberKeyColumns = Value
        End Set
    End Property

    Sub New(ByVal EIF_Table As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        'FIXMENOW probably can call
        MyBase.New(EIF_Table.node_id.to_cil, StructureType.Table)
        ProcessTable(EIF_Table, a_filemanager)
    End Sub

    Sub New(ByVal XML_Table As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        'FIXMENOW probably can call
        MyBase.New(XML_Table.node_id, StructureType.Table)
        ProcessTable(XML_Table, a_filemanager)
    End Sub

    Sub New(ByVal rm As RmStructure)
        MyBase.New(rm)
    End Sub

    Sub New(ByVal archetype_composite As ArchetypeComposite)
        MyBase.New(archetype_composite)
    End Sub

    Sub New(ByVal NodeId As String)
        MyBase.New(NodeId, StructureType.Table)
    End Sub

    Private Sub ProcessRows(ByVal relNode As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE, ByVal a_filemanager As FileManagerLocal)
        If relNode.has_children Then
            Dim rows As RmCluster = New RmCluster(CType(relNode.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager)
            Children.Add(rows)
        End If
    End Sub

    Private Sub ProcessRows(ByVal RelNode As XMLParser.C_ATTRIBUTE, ByVal a_filemanager As FileManagerLocal)
        Dim rows As RmCluster = New RmCluster(CType(RelNode.children(0), XMLParser.C_COMPLEX_OBJECT), a_filemanager)
        Children.Add(rows)
    End Sub

    Private Sub ProcessTable(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        Dim i As Integer

        For i = 1 To ObjNode.attributes.count
            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

            If attribute.has_children Then
                Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                    Case "name", "runtime_label"
                        mRunTimeConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "rotated"
                        Dim b As openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN = CType(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT).item, openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN)
                        mRotated = b.true_valid
                    Case "number_key_columns"
                        Dim int As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER = CType(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT).item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)
                        mNumberKeyColumns = int.interval.lower ' lower or higher will get the number
                    Case "rows"
                        ProcessRows(attribute, a_filemanager)
                End Select
            End If
        Next
    End Sub

    Private Sub ProcessTable(ByVal ObjNode As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        Dim attribute As XMLParser.C_ATTRIBUTE

        For Each attribute In ObjNode.attributes
            Select Case attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                Case "name", "runtime_label"
                    mRunTimeConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                Case "rotated"
                    Dim b As XMLParser.C_BOOLEAN = CType(CType(attribute.children(0), XMLParser.C_PRIMITIVE_OBJECT).item, XMLParser.C_BOOLEAN)
                    mRotated = b.true_valid
                Case "number_key_columns"
                    Dim int As XMLParser.C_INTEGER = CType(CType(attribute.children(0), XMLParser.C_PRIMITIVE_OBJECT).item, XMLParser.C_INTEGER)
                    mNumberKeyColumns = int.range.lower ' lower or higher will get the number
                Case "rows"
                    ProcessRows(attribute, a_filemanager)
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
'The Original Code is RmTable.vb.
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
