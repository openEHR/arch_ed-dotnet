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

Option Explicit On 
Namespace ArchetypeEditor.ADL_Classes

    Class ADL_ENTRY
        Inherits RmEntry

        'highest_level that need to be processed
        ' to discover reference pointers
        Private highest_level_children As Children

        Private Sub ProcessSubjectOfData(ByVal SubjectOfData As AdlParser.CAttribute)
            Dim ComplexObj As AdlParser.CComplexObject
            Dim attribute As AdlParser.CAttribute
            Dim i As Integer

            For i = 1 To SubjectOfData.Children.Count
                ComplexObj = SubjectOfData.Children.ITh(i)

                Select Case ComplexObj.RmTypeName.ToCil.ToLowerInvariant()
                    Case "party_related", "related_party" ' related party is obsolete
                        attribute = ComplexObj.Attributes.First  ' get the 'relationship' an_attribute

                        If attribute.HasChildren Then
                            ComplexObj = attribute.Children.First  ' CODED_TEXT
                            attribute = ComplexObj.Attributes.First  ' defining_code
                            rSubjectOfData.Relationship = ADL_Tools.ProcessCodes(attribute.Children.First)
                        End If
                End Select
            Next
        End Sub

        Sub New(ByRef Definition As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
            MyBase.New(Definition.RmTypeName.ToCil) ' sets the type to OBSERVATION, EVALUATION etc
            Dim attribute As AdlParser.CAttribute
            Dim i As Integer

            ' set the root node id - usually the same as the concept
            mNodeID = Definition.NodeId.ToCil

            For i = 1 To Definition.Attributes.Count
                attribute = Definition.Attributes.ITh(i)

                Select Case attribute.RmAttributeName.ToCil.ToLowerInvariant
                    Case "subject"
                        ProcessSubjectOfData(attribute)
                    Case "name", "runtime_label" 'run_time_label is obsolete
                        mRuntimeConstraint = ADL_RmElement.ProcessText(CType(attribute.Children.First, AdlParser.CComplexObject))
                    Case "data"
                        mChildren.Add(New RmStructureCompound(attribute, StructureType.Data, a_filemanager))
                        ' remembers the Processed data off events
                    Case "state"
                        mChildren.Add(New RmStructureCompound(attribute, StructureType.State, a_filemanager))
                    Case "protocol"
                        mChildren.Add(New RmStructureCompound(attribute, StructureType.Protocol, a_filemanager))
                    Case "description"
                        mChildren.Add(New RmStructureCompound(attribute, StructureType.ActivityDescription, a_filemanager))
                    Case "ism_transition", "pathway_specification" 'pathway_spec is obsolete 
                        mChildren.Add(New RmStructureCompound(attribute, StructureType.ISM_TRANSITION, a_filemanager))
                    Case "activities"
                        mChildren.Add(New RmStructureCompound(attribute, StructureType.Activities, a_filemanager))
                    Case "provider"
                        Me.ProviderIsMandatory = True
                    Case "other_participations"
                        Me.OtherParticipations = New RmStructureCompound(attribute, StructureType.OtherParticipations, a_filemanager)
                    Case "links"
                        ' No action as handled for all archetypes
                    Case Else
                        Debug.Assert(False, String.Format("{0} not handled", attribute.RmAttributeName.ToCil))
                End Select
            Next
            If Not ArchetypeEditor.ADL_Classes.ADL_Tools.StateStructure Is Nothing Then
                mChildren.Add(ArchetypeEditor.ADL_Classes.ADL_Tools.StateStructure)
                ArchetypeEditor.ADL_Classes.ADL_Tools.StateStructure = Nothing
            End If
        End Sub

    End Class
End Namespace
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
