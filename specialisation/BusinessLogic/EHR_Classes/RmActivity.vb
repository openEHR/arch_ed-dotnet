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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/BusinessLogic/EHR_Classes/RmCluster.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2005-11-15 11:02:20 +0930 (Tue, 15 Nov 2005) $"
'
'
Option Strict On
Option Explicit On
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports XMLParser

Public Class RmActivity
    Inherits RmStructureCompound

    Private mArchetypeIdConstraint As String

    Public Property ArchetypeId() As String
        Get
            Return mArchetypeIdConstraint
        End Get
        Set(ByVal Value As String)
            If Value.StartsWith("/") Then
                'Trim leading and trailing expressions
                Value = Value.Substring(1, Value.Length() - 2)
            End If
            mArchetypeIdConstraint = Value
        End Set
    End Property

    Sub New(ByVal NodeId As String)
        MyBase.New(NodeId, StructureType.Activity)
    End Sub

    Sub New(ByVal EIF_Structure As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(EIF_Structure, a_filemanager)

        Occurrences.SetFromString(EIF_Structure.Occurrences.AsString.ToCil)

        ' process the archetype id constraint
        If EIF_Structure.HasAttribute(Eiffel.String("action_archetype_id")) Then
            Dim attribute As AdlParser.CAttribute = EIF_Structure.CAttributeAtPath(Eiffel.String("action_archetype_id"))

            If attribute.HasChildren Then
                Dim obj As AdlParser.CPrimitiveObject = CType(attribute.Children.First, AdlParser.CPrimitiveObject)

                If obj.AnyAllowed Then
                    ArchetypeId = "."
                Else
                    Dim item As AdlParser.CString = CType(obj.Item, AdlParser.CString)
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-ACTION\."

                    If item.Regexp IsNot Nothing Then
                        ArchetypeId = item.Regexp.ToCil.Replace(classPrefix, "").Replace("\.", ".")
                    ElseIf item.Strings IsNot Nothing Then
                        ArchetypeId = CType(item.Strings.ITh(1), EiffelKernel.String_8).ToCil.Replace(classPrefix, "").Replace("\.", ".")
                    Else
                        ArchetypeId = "."
                    End If
                End If
            End If
        End If

        'process any activity descriptions
        If EIF_Structure.HasAttribute(Eiffel.String("description")) Then
            Dim attribute As AdlParser.CAttribute = EIF_Structure.CAttributeAtPath(Eiffel.String("description"))

            If attribute.HasChildren Then
                ' could be a C_COMPLEX_OBJECT or an ARCHETYPE_SLOT
                Dim obj As AdlParser.CObject = CType(attribute.Children.First, AdlParser.CObject)

                Select Case obj.GeneratingType.Out.ToCil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                    Case "archetype_slot"
                        Children.Add(New RmSlot(CType(obj, AdlParser.ArchetypeSlot)))
                    Case "c_complex_object"
                        Children.Add(New RmStructureCompound(CType(obj, AdlParser.CComplexObject), a_filemanager))
                End Select
            End If
        End If
    End Sub

    Sub New(ByVal xml_Structure As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(xml_Structure, a_filemanager)

        Occurrences = ArchetypeEditor.XML_Classes.XML_Tools.SetOccurrences(xml_Structure.occurrences)

        ' process the archetype id constraint
        If Not xml_Structure.attributes Is Nothing Then
            For Each an_attribute As XMLParser.C_ATTRIBUTE In xml_Structure.attributes
                Select Case an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "action_archetype_id"
                        Dim obj As XMLParser.C_PRIMITIVE_OBJECT = CType(an_attribute.children(0), XMLParser.C_PRIMITIVE_OBJECT)
                        Dim primitiveObject As New C_PRIMITIVE_OBJECT_PROXY(obj)

                        If primitiveObject.Any_Allowed Then
                            ArchetypeId = "."
                        Else
                            Dim id As XMLParser.C_STRING = CType(obj.item, XMLParser.C_STRING)
                            Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-ACTION\."

                            If id.list IsNot Nothing Then
                                ArchetypeId = id.list(0).Trim("/".ToCharArray()).Replace(classPrefix, "").Replace("\.", ".")
                            ElseIf id.pattern IsNot Nothing Then
                                ArchetypeId = id.pattern.Trim("/".ToCharArray()).Replace(classPrefix, "").Replace("\.", ".")
                            Else
                                ArchetypeId = "."
                            End If
                        End If
                    Case "description"
                        Dim obj As XMLParser.C_OBJECT
                        obj = CType(an_attribute.children(0), XMLParser.C_OBJECT)

                        Select Case obj.GetType.ToString.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                            Case "xmlparser.archetype_slot"
                                Children.Add(New RmSlot(CType(obj, XMLParser.ARCHETYPE_SLOT)))
                            Case "xmlparser.c_complex_object"
                                Dim cObject As New C_COMPLEX_OBJECT_PROXY(CType(obj, XMLParser.C_COMPLEX_OBJECT))

                                If Not cObject.Any_Allowed Then
                                    Children.Add(New RmStructureCompound(CType(obj, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                                End If
                        End Select
                End Select
            Next
        End If
    End Sub

End Class
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
'The Original Code is RmActivity.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2006
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
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