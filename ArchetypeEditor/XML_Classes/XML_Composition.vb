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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.XML_Composition.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Explicit On 

Namespace ArchetypeEditor.XML_Classes

    Class XML_COMPOSITION
        Inherits RmComposition

        Sub New(ByVal definition As XMLParser.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal)
            MyBase.New()

            ' set the root node id - usually the same as the concept
            mNodeID = definition.node_id

            For Each attribute As XMLParser.C_ATTRIBUTE In definition.attributes
                Select Case attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "category"
                        If attribute.children.Length > 0 Then
                            Dim t As Constraint_Text = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(attribute.children(0))

                            If t.AllowableValues.HasCode("431") Then
                                'isPersistent defaults to false (openehr::433) for event
                                mIsPersistent = True
                            End If
                        End If
                    Case "context"
                        If attribute.children.Length > 0 Then
                            Dim complexObj As XMLParser.C_COMPLEX_OBJECT = attribute.children(0)

                            For Each a As XMLParser.C_ATTRIBUTE In complexObj.attributes
                                Select Case a.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                                    Case "participations"
                                        Participations = New RmStructureCompound(a, StructureType.OtherParticipations, fileManager)
                                    Case "other_context"
                                        Dim child As XMLParser.C_COMPLEX_OBJECT = TryCast(a.children(0), XMLParser.C_COMPLEX_OBJECT)

                                        If Not child Is Nothing Then
                                            Select Case ReferenceModel.StructureTypeFromString(child.rm_type_name)
                                                Case StructureType.Single, StructureType.List, StructureType.Tree
                                                    mChildren.Add(New RmStructureCompound(child, fileManager))
                                                Case StructureType.Table
                                                    mChildren.Add(New RmTable(child, fileManager))
                                            End Select
                                        Else
                                            mChildren.Add(New RmSlot(CType(a.children(0), XMLParser.ARCHETYPE_SLOT)))
                                        End If
                                End Select
                            Next
                        End If
                    Case "content"
                        ' a set of slots constraining what sections can be added
                        Dim section As RmSection = New RmSection("root")

                        For i As Integer = 0 To attribute.children.Length - 1
                            section.Children.Add(New RmSlot(CType(attribute.children(i), XMLParser.ARCHETYPE_SLOT)))
                        Next

                        mChildren.Add(section)
                End Select
            Next
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
'The Original Code is XML_Composition.vb.
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
