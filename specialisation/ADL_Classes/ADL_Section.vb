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
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes
    Class ADL_SECTION
        Inherits RmSection

        Private Function GetRunTimeConstraintID(ByVal an_attribute As AdlParser.CAttribute) As String
            Dim result As String = ""
            Dim CodedText As AdlParser.CComplexObject
            Dim constraint_object As AdlParser.CObject

            If an_attribute.HasChildren Then
                CodedText = an_attribute.Children.First
                an_attribute = CodedText.Attributes.First

                If an_attribute.HasChildren Then
                    constraint_object = an_attribute.Children.First

                    If constraint_object.GeneratingType.Out.ToCil = "CONSTRAINT_REF" Then
                        result = CType(constraint_object, AdlParser.ConstraintRef).AsString.ToCil
                    ElseIf constraint_object.GeneratingType.Out.ToCil = "C_CODE_PHRASE" Then
                        result = CType(constraint_object, AdlParser.CCodePhrase).AsString.ToCil()
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

        Private Sub ProcessSection(ByVal a_rm_section As Object, ByVal an_object As AdlParser.CObject)
            'ccomplex object means it is a section, otherwise a slot
            'a_rm_section is passed as object so that definition can be passed at the first level
            Dim an_attribute As AdlParser.CAttribute
            Dim i, j As Integer

            Select Case an_object.GeneratingType.Out.ToCil
                Case "C_COMPLEX_OBJECT"
                    Dim a_complex_object As AdlParser.CComplexObject
                    Dim a_section As RmSection

                    a_section = New RmSection(an_object.NodeId.ToCil)
                    a_complex_object = an_object

                    For i = 1 To a_complex_object.Attributes.Count
                        an_attribute = a_complex_object.Attributes.ITh(i)

                        If an_attribute.HasChildren Then
                            Select Case an_attribute.RmAttributeName.ToCil
                                Case "name", "Name", "NAME", "runtime_label"
                                    a_section.NameConstraint = ADL_RmElement.ProcessText(CType(an_attribute.Children.First, AdlParser.CComplexObject))
                                Case "items", "Items", "ITEMS"
                                    For j = 1 To an_attribute.Children.Count
                                        ProcessSection(a_section, an_attribute.Children.ITh(j))
                                    Next
                            End Select
                        End If
                    Next

                    a_rm_section.Children.Add(a_section)

                Case "ARCHETYPE_SLOT"
                    a_rm_section.children.add(New RmSlot(CType(an_object, AdlParser.ArchetypeSlot)))

                Case Else
                    Debug.Assert(False, "Type is not catered for")
            End Select
        End Sub

        Sub New(ByRef Definition As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
            MyBase.New(Definition, a_filemanager)

            If Definition.HasAttribute(Eiffel.String("items")) Then
                Dim an_attribute As AdlParser.CAttribute
                Dim i As Integer

                an_attribute = Definition.CAttributeAtPath(Eiffel.String("items"))
                ArchetypeEditor.ADL_Classes.ADL_Tools.SetCardinality(an_attribute.Cardinality, Children)

                For i = 1 To an_attribute.Children.Count
                    ProcessSection(Me, an_attribute.Children.ITh(i))
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
