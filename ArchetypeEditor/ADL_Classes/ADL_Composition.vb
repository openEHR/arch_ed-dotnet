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

Option Explicit On 
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes

    Class ADL_Composition
        Inherits RmComposition

        Sub New(ByRef definition As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal)
            MyBase.New()

            ' set the root node id - usually the same as the concept
            mNodeID = definition.node_id.to_cil

            For i As Integer = 1 To definition.attributes.count
                Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = definition.attributes.i_th(i)

                Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "category"
                        IsPersistent = attribute.has_children AndAlso ADL_RmElement.ProcessText(attribute.children.first).AllowableValues.HasCode("431")
                    Case "context"
                        If attribute.has_children Then
                            Dim complexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = attribute.children.first

                            For j As Integer = 1 To complexObj.attributes.count
                                Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = complexObj.attributes.i_th(j)

                                Select Case a.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                                    Case "participations"
                                        Participations = New RmStructureCompound(a, StructureType.OtherParticipations, fileManager)
                                    Case "other_context"
                                        If a.has_children Then
                                            Dim child As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = TryCast(a.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)

                                            If Not child Is Nothing Then
                                                Select Case ReferenceModel.StructureTypeFromString(child.rm_type_name.to_cil)
                                                    Case StructureType.Single, StructureType.List, StructureType.Tree
                                                        mChildren.Add(New RmStructureCompound(child, fileManager))
                                                    Case StructureType.Table
                                                        mChildren.Add(New RmTable(child, fileManager))
                                                End Select
                                            Else
                                                mChildren.Add(New RmSlot(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))
                                            End If
                                        End If
                                End Select
                            Next
                        End If
                    Case "content"
                        ' a set of slots constraining what sections can be added
                        Dim section As RmSection = New RmSection("root")

                        For j As Integer = 1 To attribute.children.count
                            Try
                                Dim obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT = attribute.children.i_th(j)

                                If obj.generator.to_cil = "ARCHETYPE_SLOT" Then
                                    section.Children.Add(New RmSlot(CType(attribute.children.i_th(j), openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))
                                Else
                                    MessageBox.Show("Editor does not support compositions with fixed sections. Create slots and separate suitable section archetype", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Catch ex As Exception
                                MessageBox.Show(String.Format("{0}- ({1}): {2}", AE_Constants.Instance.ErrorLoading, fileManager.Archetype.Archetype_ID.ToString(), ex.Message), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
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
'The Original Code is ADL_Composition.vb.
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
