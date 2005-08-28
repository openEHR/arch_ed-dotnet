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

Option Explicit On 

Class ADL_ENTRY
    Inherits RmEntry

    'highest_level that need to be processed
    ' to discover reference pointers
    Private highest_level_children As Children

    Private Sub ProcessSubjectOfData(ByVal SubjectOfData As openehr.openehr.am.archetype.constraint_model.C_Attribute)
        Dim ComplexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_Attribute
        Dim i As Integer

        For i = 1 To SubjectOfData.Children.Count
            ComplexObj = SubjectOfData.Children.i_th(i)
            Select Case ComplexObj.rm_type_name.to_cil
                Case "RELATED_PARTY"
                    an_attribute = ComplexObj.attributes.first  ' get the 'relationship' an_attribute
                    ComplexObj = an_attribute.children.first  ' CODED_TEXT
                    an_attribute = ComplexObj.attributes.first  ' code
                    rSubjectOfData.Relationship = ADL_Tools.Instance.ProcessCodes(an_attribute.children.first)
            End Select
        Next
    End Sub


    Sub New(ByRef Definition As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(Definition.rm_type_name.to_cil) ' sets the type to OBSERVATION, EVALUATION etc
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_Attribute
        Dim i As Integer

        ' set the root node id - usually the same as the concept
        mNodeID = Definition.node_id.to_cil

        For i = 1 To Definition.Attributes.Count
            an_attribute = Definition.Attributes.i_th(i)
            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "subject"
                    ProcessSubjectOfData(an_attribute)
                Case "name", "runtime_label" 'run_time_label is obsolete
                    mRuntimeConstraint = RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "data"
                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.Data))
                    ' remembers the Processed data off events
                Case "state"
                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.State))
                Case "protocol"
                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.Protocol))
                Case "activity_definition"
                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.Action))
                Case ReferenceModel.Instance.RM_StructureName(StructureType.InstructionActExection).ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.InstructionActExection))
            End Select
        Next
    End Sub

End Class 'ADL_ENTRY

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
'The Original Code is ADL_Entry.vb.
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
