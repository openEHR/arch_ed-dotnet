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
Namespace ArchetypeEditor.ADL_Classes
    Class ADL_SECTION
        Inherits RmSection

        Private Function GetRunTimeConstraintID(ByVal an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE) As String

            Dim CodedText As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim constraint_object As openehr.openehr.am.archetype.constraint_model.C_OBJECT
            Dim s As String

            CodedText = an_attribute.children.first
            an_attribute = CodedText.attributes.first
            constraint_object = an_attribute.children.first

            If constraint_object.generating_type.to_cil = "CONSTRAINT_REF" Then
                s = CType(constraint_object, openehr.openehr.am.archetype.constraint_model.CONSTRAINT_REF).as_string.to_cil
            ElseIf constraint_object.generating_type.to_cil = "C_CODED_TERM" Then
                s = CType(constraint_object, openehr.openehr.am.openehr_profile.data_types.text.C_CODED_TERM).as_string.to_cil()
            End If
            ' strip off the square brackets
            s = s.Substring(1, s.Length - 2)
            If s.StartsWith("local::") Then
                ' when the runtime name is constrained by an 'at' code to be the same as the design name ie NodeId
                s = s.Substring(7)
            End If
            Return s

        End Function

        Private Function ProcessSection(ByVal a_rm_section As Object, ByVal an_object As openehr.openehr.am.archetype.constraint_model.C_OBJECT)
            'ccomplex object means it is a section, otherwise a slot
            'a_rm_section is passed as object so that definition can be passed at the first level
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim i, j As Integer

            Select Case an_object.generating_type.to_cil
                Case "C_COMPLEX_OBJECT"
                    Dim a_complex_object As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                    Dim a_section As RmSection

                    a_section = New RmSection(an_object.node_id.to_cil)
                    a_complex_object = an_object

                    For i = 1 To a_complex_object.attributes.count
                        an_attribute = a_complex_object.attributes.i_th(i)
                        Select Case an_attribute.rm_attribute_name.to_cil
                            Case "name", "Name", "NAME", "runtime_label"
                                a_section.NameConstraint = RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                            Case "items", "Items", "ITEMS"
                                For j = 1 To an_attribute.children.count
                                    ProcessSection(a_section, an_attribute.children.i_th(j))
                                Next
                        End Select
                    Next
                    a_rm_section.Children.Add(a_section)

                Case "ARCHETYPE_SLOT"
                    a_rm_section.children.add(New RmSlot(an_object))

                Case Else
                    Debug.Assert(False, "Type is not catered for")
                    Return Nothing
            End Select

        End Function


        Sub New(ByRef Definition As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
            MyBase.New(Definition)

            If Definition.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("items")) Then
                Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                Dim i As Integer

                an_attribute = Definition.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("items"))
                For i = 1 To an_attribute.children.count
                    ProcessSection(Me, an_attribute.children.i_th(i))
                Next
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
'The Original Code is ADL_Section.vb.
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
