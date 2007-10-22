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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/XML_Classes/XML_Entry.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Explicit On 
Namespace ArchetypeEditor.XML_Classes

    Class XML_ENTRY
        Inherits RmEntry

        'highest_level that need to be processed
        ' to discover reference pointers
        Private highest_level_children As Children

        Private Sub ProcessSubjectOfData(ByVal SubjectOfData As XMLParser.C_ATTRIBUTE)
            Dim ComplexObj As XMLParser.C_COMPLEX_OBJECT
            Dim an_attribute As XMLParser.C_ATTRIBUTE

            For Each dataSubject As XMLParser.C_COMPLEX_OBJECT In SubjectOfData.children
                Select Case dataSubject.rm_type_name.ToLowerInvariant()
                    Case "party_related", "related_party" ' related_party is obsolete 
                        If (Not dataSubject.attributes Is Nothing) AndAlso dataSubject.attributes.Length > 0 Then
                            an_attribute = dataSubject.attributes(0)  ' get the 'relationship' an_attribute
                            If (Not an_attribute.children Is Nothing) AndAlso an_attribute.children.Length > 0 Then
                                ComplexObj = an_attribute.children(0)  ' CODED_TEXT
                                an_attribute = ComplexObj.attributes(0)  ' defining code
                                rSubjectOfData.Relationship = ArchetypeEditor.XML_Classes.XML_Tools.ProcessCodes(an_attribute.children(0))
                            End If
                        End If
                    Case Else
                            Debug.Assert(False, "Type not handled: " & dataSubject.rm_type_name)
                End Select
            Next
        End Sub

        Sub New(ByVal Definition As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)

            MyBase.New(Definition.rm_type_name) ' sets the type to OBSERVATION, EVALUATION etc

            Try
                Dim an_attribute As XMLParser.C_ATTRIBUTE

                ' set the root node id - usually the same as the concept
                mNodeID = Definition.node_id
                If Not Definition.attributes Is Nothing Then
                    For Each an_attribute In Definition.attributes
                        If Not an_attribute.rm_attribute_name Is Nothing Then
                            Select Case an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                                Case "subject"
                                    ProcessSubjectOfData(an_attribute)
                                Case "name", "runtime_label" 'run_time_label is obsolete
                                    mRuntimeConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                                Case "data"
                                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.Data, a_filemanager))
                                    ' remembers the Processed data off events
                                Case "state"
                                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.State, a_filemanager))
                                Case "protocol"
                                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.Protocol, a_filemanager))
                                Case "description"
                                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.ActivityDescription, a_filemanager))
                                Case "ism_transition", "pathway_specification" 'pathway_spec is obsolete 
                                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.ISM_TRANSITION, a_filemanager))
                                Case "activities"
                                    mChildren.Add(New RmStructureCompound(an_attribute, StructureType.Activities, a_filemanager))
                                Case "links"
                                    'No action as handled for all archetypes
                                Case Else
                                    Debug.Assert(False, "Attribute '" & an_attribute.rm_attribute_name & "' is not handled")
                            End Select
                        Else
                            Debug.Assert(False, an_attribute.ToString() & " has no rm_attribute_name")
                        End If
                    Next
                    If Not ArchetypeEditor.XML_Classes.XML_Tools.StateStructure Is Nothing Then
                        mChildren.Add(ArchetypeEditor.XML_Classes.XML_Tools.StateStructure)
                        ArchetypeEditor.XML_Classes.XML_Tools.StateStructure = Nothing
                    End If
                End If
            Catch ex As Exception
                Debug.Assert(True)
            End Try
        End Sub

        Sub New(ByVal a_rm_type As StructureType, ByVal a_node_id As String)
            MyBase.New(a_rm_type)
            mNodeID = a_node_id
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
'The Original Code is XML_Entry.vb.
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
