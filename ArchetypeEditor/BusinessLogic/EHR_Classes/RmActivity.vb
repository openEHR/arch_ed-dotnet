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

#Region "ADL Handling"
    Sub New(ByVal EIF_Structure As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(EIF_Structure, a_filemanager)

        Me.Occurrences.SetFromString(EIF_Structure.occurrences.as_occurrences_string.to_cil)

        ' process the archetype id constraint
        If EIF_Structure.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("action_archetype_id")) Then
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            an_attribute = EIF_Structure.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("action_archetype_id"))
            Dim obj As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
            obj = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
            If obj.any_allowed Then
                Me.ArchetypeId = "."
            Else
                Me.ArchetypeId = CType(obj.item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING).strings.first.to_cil
            End If
        End If

        'process any activity descriptions

        If EIF_Structure.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("description")) Then
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            an_attribute = EIF_Structure.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("description"))
            ' could be a C_COMPLEX_OBJECT
            Dim obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT
            obj = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)
            If Not obj.any_allowed Then
                Select Case obj.generating_type.to_cil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                    Case "archetype_slot"
                        Me.Children.Add(New RmSlot(obj))
                    Case "c_complex_object"
                        Me.Children.Add(New RmStructureCompound(obj, a_filemanager))
                End Select
            End If
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