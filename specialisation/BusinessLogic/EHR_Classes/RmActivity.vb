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
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel

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

#Region "ADL and XML Handling"
    Sub New(ByVal EIF_Structure As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(EIF_Structure, a_filemanager)

        Me.Occurrences.SetFromString(EIF_Structure.occurrences.as_occurrences_string.to_cil)

        ' process the archetype id constraint
        If EIF_Structure.has_attribute(EiffelKernel.Create.STRING_8.make_from_cil("action_archetype_id")) Then
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            an_attribute = EIF_Structure.c_attribute_at_path(EiffelKernel.Create.STRING_8.make_from_cil("action_archetype_id"))
            Dim obj As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
            obj = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
            If obj.any_allowed Then
                Me.ArchetypeId = "."
            Else
                Dim item As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING = CType(obj.item, openehr.openehr.am.archetype.constraint_model.primitive.C_STRING)
                If Not item.regexp Is Nothing Then
                    Me.ArchetypeId = item.regexp.to_cil.Replace("\.", ".")
                ElseIf Not item.strings Is Nothing Then
                    Me.ArchetypeId = CType(item.strings.i_th(1), EiffelKernel.STRING_8).to_cil.Replace("\.", ".")
                Else
                    Me.ArchetypeId = "."
                End If

            End If
        End If

        'process any activity descriptions

        If EIF_Structure.has_attribute(EiffelKernel.Create.STRING_8.make_from_cil("description")) Then
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            an_attribute = EIF_Structure.c_attribute_at_path(EiffelKernel.Create.STRING_8.make_from_cil("description"))
            ' could be a C_COMPLEX_OBJECT or an ARCHETYPE_SLOT
            Dim obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT
            obj = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)
            Select Case obj.generating_type.to_cil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                Case "archetype_slot"
                    Dim slot As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT
                    slot = CType(obj, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)
                    Me.Children.Add(New RmSlot(CType(obj, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)))
                Case "c_complex_object"
                    Dim complexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
                    complexObj = CType(obj, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                    Me.Children.Add(New RmStructureCompound(CType(obj, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager))
            End Select
        End If


    End Sub

    Sub New(ByVal xml_Structure As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(xml_Structure, a_filemanager)

        Me.Occurrences = ArchetypeEditor.XML_Classes.XML_Tools.SetOccurrences(xml_Structure.occurrences)

        ' process the archetype id constraint
        If Not xml_Structure.attributes Is Nothing Then
            For Each an_attribute As XMLParser.C_ATTRIBUTE In xml_Structure.attributes
                Select Case an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "action_archetype_id"
                        Dim obj As XMLParser.C_PRIMITIVE_OBJECT
                        obj = CType(an_attribute.children(0), XMLParser.C_PRIMITIVE_OBJECT)

                        'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                        'If obj.any_allowed Then
                        Dim primitiveObject As New C_PRIMITIVE_OBJECT_PROXY(obj)
                        If primitiveObject.Any_Allowed Then
                            Me.ArchetypeId = "."
                        Else
                            Dim id As XMLParser.C_STRING = CType(obj.item, XMLParser.C_STRING)
                            If id.list Is Nothing Then
                                If id.pattern Is Nothing Then
                                    Me.ArchetypeId = "."
                                Else
                                    Me.ArchetypeId = id.pattern.Trim("/".ToCharArray()).Replace("\.", ".")
                                End If
                            Else
                                Debug.Assert(False, "Should use pattern here")
                                Me.ArchetypeId = id.list(0).Trim("/".ToCharArray()).Replace("\.", ".")
                            End If
                        End If
                    Case "description"
                        Dim obj As XMLParser.C_OBJECT
                        obj = CType(an_attribute.children(0), XMLParser.C_OBJECT)
                        Select Case obj.GetType.ToString.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                            Case "xmlparser.archetype_slot"
                                Me.Children.Add(New RmSlot(CType(obj, XMLParser.ARCHETYPE_SLOT)))
                            Case "xmlparser.c_complex_object"
                                Dim cObject As New C_COMPLEX_OBJECT_PROXY(CType(obj, XMLParser.C_COMPLEX_OBJECT))
                                'SRH: 22.10.2007 Moved to inside this case statement as cannot caste from slot to complex object
                                'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                                'If Not obj.any_allowed Then

                                If Not cObject.Any_Allowed Then
                                    Me.Children.Add(New RmStructureCompound(CType(obj, XMLParser.C_COMPLEX_OBJECT), a_filemanager))
                                End If
                        End Select
                End Select
            Next
        End If
    End Sub

#End Region

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