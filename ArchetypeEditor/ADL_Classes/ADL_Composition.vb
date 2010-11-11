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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Composition.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes

Class ADL_COMPOSITION
    Inherits RmComposition

        Sub New(ByRef Definition As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
            MyBase.New()

            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim i As Integer

            ' set the root node id - usually the same as the concept
            mNodeID = Definition.node_id.to_cil

            For i = 1 To Definition.attributes.count
                attribute = Definition.attributes.i_th(i)

                Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "category"
                        If attribute.has_children Then
                            Dim t As Constraint_Text = ADL_RmElement.ProcessText(attribute.children.first)

                            If t.AllowableValues.HasCode("431") Then
                                'isPersistent defaults to false (openehr::433) for event
                                mIsPersistent = True
                            End If
                        End If
                    Case "context"
                        If attribute.has_children Then
                            Dim complexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = attribute.children.first

                            If complexObj.has_attribute(Eiffel.String("other_context")) Then
                                attribute = complexObj.c_attribute_at_path(Eiffel.String("other_context"))

                                If attribute.has_children Then
                                    Dim child As Object = attribute.children.first

                                    If TypeOf child Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                                        mChildren.Add(New RmStructureCompound(CType(child, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager))
                                        ' remembers the Processed data off events
                                    Else
                                        mChildren.Add(New RmSlot(CType(child, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))
                                    End If
                                End If
                            End If

                            If complexObj.has_attribute(Eiffel.String("participations")) Then
                                attribute = complexObj.c_attribute_at_path(Eiffel.String("participations"))
                                Participations = New RmStructureCompound(attribute, StructureType.OtherParticipations, a_filemanager)
                            End If
                        End If
                    Case "content"
                        ' a set of slots constraining what sections can be added
                        Dim section As RmSection = New RmSection("root")

                        For j As Integer = 1 To attribute.children.count
                            Try
                                Dim obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT = attribute.children.i_th(j)

                                If obj.generating_type.to_cil = "ARCHETYPE_SLOT" Then
                                    section.Children.Add(New RmSlot(CType(attribute.children.i_th(j), openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))
                                Else
                                    MessageBox.Show("Editor does not support compositions with fixed sections. Create slots and separate suitable section archetype", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Catch ex As Exception
                                MessageBox.Show(String.Format("{0}- ({1}): {2}", AE_Constants.Instance.Error_loading, a_filemanager.Archetype.Archetype_ID.ToString(), ex.Message), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
