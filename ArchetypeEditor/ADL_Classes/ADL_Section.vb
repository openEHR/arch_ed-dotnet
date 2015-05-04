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
    Class ADL_SECTION
        Inherits RmSection

        Private Function GetRunTimeConstraintID(ByVal an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE) As String
            Dim result As String = ""
            Dim CodedText As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim constraint_object As openehr.openehr.am.archetype.constraint_model.C_OBJECT

            If an_attribute.has_children Then
                CodedText = an_attribute.children.first
                an_attribute = CodedText.attributes.first

                If an_attribute.has_children Then
                    constraint_object = an_attribute.children.first

                    If constraint_object.generator.to_cil = "CONSTRAINT_REF" Then
                        result = CType(constraint_object, openehr.openehr.am.archetype.constraint_model.CONSTRAINT_REF).as_string.to_cil
                    ElseIf constraint_object.generator.to_cil = "C_CODE_PHRASE" Then
                        result = CType(constraint_object, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE).as_string.to_cil()
                    End If
                End If
            End If

            ' strip off the square brackets
            result = result.Substring(1, result.Length - 2)

            If result.StartsWith("local::") Then
                ' when the runtime name is constrained by an 'at' code to be the same as the design name ie NodeId
                result = result.Substring(7)
            End If

            Return result
        End Function

        Private Sub ProcessSection(ByVal section As Object, ByVal obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT, ByVal fileManager As FileManagerLocal)
            'c_complex object means it is a section, otherwise a slot
            'a_rm_section is passed as object so that definition can be passed at the first level
            Dim i, j As Integer

            Select Case obj.generator.to_cil
                Case "C_COMPLEX_OBJECT"
                    Dim o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = CType(obj, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                    Dim subSection As New RmSection(o, fileManager)

                    For i = 1 To o.attributes.count
                        Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = o.attributes.i_th(i)

                        If a.has_children Then
                            Select Case a.rm_attribute_name.to_cil
                                Case "name", "Name", "NAME", "runtime_label"
                                    subSection.NameConstraint = ADL_RmElement.ProcessText(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                                Case "items", "Items", "ITEMS"
                                    For j = 1 To a.children.count
                                        ProcessSection(subSection, a.children.i_th(j), fileManager)
                                    Next
                            End Select
                        End If
                    Next

                    section.Children.Add(subSection)

                Case "ARCHETYPE_SLOT"
                    section.children.add(New RmSlot(CType(obj, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))

                Case Else
                    Debug.Assert(False, "Type is not catered for")
            End Select
        End Sub

        Sub New(ByRef obj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal)
            MyBase.New(obj, fileManager)

            If obj.has_attribute(Eiffel.String("items")) Then
                Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = obj.c_attribute_at_path(Eiffel.String("items"))
                ArchetypeEditor.ADL_Classes.ADL_Tools.SetCardinality(attribute.cardinality, Children)

                For i As Integer = 1 To attribute.children.count
                    ProcessSection(Me, attribute.children.i_th(i), fileManager)
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
