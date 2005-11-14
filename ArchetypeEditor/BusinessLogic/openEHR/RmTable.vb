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

    Sub New(ByVal EIF_Table As openehr.am.C_COMPLEX_OBJECT)
        'FIXMENOW probably can call
        MyBase.New(EIF_Table.node_id.to_cil, StructureType.Table)
        ProcessTable(EIF_Table)
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

    Private Sub ProcessRows(ByVal RelNode As openehr.am.C_ATTRIBUTE)
        Dim rows As RmCluster
        'Dim rm_column As RmElement
        'Dim cadlColumn As openehr.am.C_COMPLEX_OBJECT
        'Dim i As Integer


        rows = New RmCluster(CType(RelNode.children.first, openehr.am.C_COMPLEX_OBJECT))

        'columns = New RmStructureCompound(StructureType.Columns, StructureType.Columns)

        'For i = 1 To RelNode.children.count
        '    cadlColumn = RelNode.children.i_th(i)
        '    rm_column = New RmElement(cadlColumn)
        '    columns.Children.Add(rm_column)
        'Next
        Me.Children.Add(rows)
    End Sub

    Private Sub ProcessTable(ByVal ObjNode As openehr.am.C_COMPLEX_OBJECT)
        Dim an_attribute As openehr.am.C_ATTRIBUTE
        Dim i As Integer

        For i = 1 To ObjNode.attributes.count
            an_attribute = ObjNode.attributes.i_th(i)
            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                Case "name", "runtime_label"
                    mRuntimeConstraint = RmElement.ProcessText(CType(an_attribute.children.first, openehr.am.C_COMPLEX_OBJECT))
                Case "rotated"
                    Dim b As openehr.am.C_BOOLEAN

                    b = CType(CType(an_attribute.children.first, _
                            openehr.am.C_PRIMITIVE_OBJECT).item, openehr.am.C_BOOLEAN)
                    If b.true_valid Then
                        mRotated = True
                    Else
                        mRotated = False
                    End If
                Case "number_key_columns"
                    Dim int As openehr.am.C_INTEGER

                    int = CType(CType(an_attribute.children.first, _
                            openehr.am.C_PRIMITIVE_OBJECT).item, openehr.am.C_INTEGER)
                    mNumberKeyColumns = int.interval.lower ' lower or higher will get the number

                Case "rows"
                    ProcessRows(an_attribute)
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