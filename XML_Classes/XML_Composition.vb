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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.XML_Composition.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Explicit On 

Namespace ArchetypeEditor.XML_Classes

    Class XML_COMPOSITION
        Inherits RmComposition

        Sub New(ByVal Definition As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
            MyBase.New()

            Try
                Dim an_attribute As XMLParser.C_ATTRIBUTE

                ' set the root node id - usually the same as the concept
                mNodeID = Definition.node_id

                For Each an_attribute In Definition.attributes
                    Select Case an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "category"
                            Dim t As Constraint_Text
                            t = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(an_attribute.children(0))
                            If t.AllowableValues.HasCode("431") Then
                                'isPersistent defaults to false (openehr::433) for event
                                mIsPersistent = True
                            End If
                        Case "context"
                            Dim complexObj As XMLParser.C_COMPLEX_OBJECT
                            complexObj = an_attribute.children(0)
                            For Each context_attribute As XMLParser.C_ATTRIBUTE In complexObj.attributes
                                Select Case context_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                                    Case "participations"
                                        Me.Participations = New RmStructureCompound(an_attribute, StructureType.OtherParticipations, a_filemanager)
                                    Case "other_context"
                                        mChildren.Add(New RmStructureCompound(CType(context_attribute.children(0), XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                                End Select
                            Next
                        Case "content"
                            ' a set of slots constraining what sections can be added
                            Dim section As RmSection = New RmSection("root")

                            For i As Integer = 0 To an_attribute.children.Length - 1
                                section.Children.Add(New RmSlot(CType(an_attribute.children(i), XMLParser.ARCHETYPE_SLOT)))
                            Next
                            mChildren.Add(section)
                    End Select
                Next
            Catch ex As Exception
                Debug.Assert(True)
            End Try
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
