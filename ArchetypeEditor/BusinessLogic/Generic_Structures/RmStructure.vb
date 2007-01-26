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

Public Enum StructureType
    Not_Set = 0
    [Single] = 105
    List = 106
    Tree = 107
    Table = 108
    Columns = 164
    Cluster = 313
    Element = 567

    [Event] = 433
    PointEvent = 566
    IntervalEvent = 565

    Reference = 564
    Slot = 312
    CarePathwayStep = 563

    ENTRY = 559
    EVALUATION = 555
    OBSERVATION = 554
    INSTRUCTION = 557
    ACTION = 556
    ADMIN_ENTRY = 560

    SECTION = 314

    COMPOSITION = 561

    Data = 80
    State = 177
    Protocol = 78
    History = 275
    Activity = 586
    Activities = 587
    ActivityDescription = 509
    ISM_TRANSITION = 1005
End Enum

Public Class RmStructure
    Implements ArcheTypeDefinitionBasic

    '  maps to C_OBJECT in ADL
    Protected sNodeId As String
    Protected cOccurrences As New RmCardinality
    Protected mRunTimeConstraint As Constraint_Text
    Protected mType As StructureType

    Public Overridable ReadOnly Property Type() As StructureType Implements ArcheTypeDefinitionBasic.Type
        Get
            Return mType
        End Get
    End Property
    Public Property NameConstraint() As Constraint_Text Implements ArcheTypeDefinitionBasic.NameConstraint
        Get
            If mRunTimeConstraint Is Nothing Then
                mRunTimeConstraint = New Constraint_Text
            End If
            Return mRunTimeConstraint
        End Get
        Set(ByVal Value As Constraint_Text)
            mRunTimeConstraint = Value
        End Set
    End Property
    Public Property HasNameConstraint() As Boolean Implements ArcheTypeDefinitionBasic.hasNameConstraint
        Get
            Return Not mRunTimeConstraint Is Nothing
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                If mRunTimeConstraint Is Nothing Then
                    mRunTimeConstraint = New Constraint_Text
                End If
            Else
                mRunTimeConstraint = Nothing
            End If
        End Set
    End Property
    Public Property NodeId() As String Implements ArcheTypeDefinitionBasic.RootNodeId
        Get
            Return sNodeId
        End Get
        Set(ByVal Value As String)
            sNodeId = Value
        End Set
    End Property
    Public Property Occurrences() As RmCardinality
        Get
            Return cOccurrences
        End Get
        Set(ByVal Value As RmCardinality)
            cOccurrences = Value
        End Set
    End Property

    Public Overridable Function Copy() As RmStructure
        Dim rm As New RmStructure(sNodeId, mType)
        rm.cOccurrences = Me.cOccurrences.Copy
        If Not Me.mRunTimeConstraint Is Nothing Then
            rm.mRunTimeConstraint = Me.mRunTimeConstraint.copy
        End If
        Return rm
    End Function

    Sub New(ByRef Archetype As Archetype)
        ' For building new parse tree - and a root structure
        If Archetype.Definition.Type <> StructureType.Element Then
            CType(Archetype.Definition, ArchetypeDefinition).Data.Add(Me)
        Else
            Debug.Assert(False, "??")
        End If
    End Sub

    Sub New(ByVal a_RmStructure As RmStructure)
        mType = a_RmStructure.mType
        sNodeId = a_RmStructure.sNodeId
        cOccurrences = a_RmStructure.cOccurrences
        If a_RmStructure.HasNameConstraint Then
            mRunTimeConstraint = a_RmStructure.NameConstraint
        End If
    End Sub

    Sub New(ByVal NodeId As String, ByVal a_structure_type As StructureType)
        sNodeId = NodeId
        mType = a_structure_type
    End Sub

#Region "ADL and XML oriented features"

    Sub New(ByVal EIF_Structure As openehr.openehr.am.archetype.constraint_model.C_OBJECT)
        sNodeId = EIF_Structure.node_id.to_cil
        cOccurrences = ArchetypeEditor.ADL_Classes.ADL_Tools.SetOccurrences(EIF_Structure.occurrences)
        mType = ReferenceModel.StructureTypeFromString(EIF_Structure.rm_type_name.to_cil)

        If EIF_Structure.generating_type.to_cil = "C_COMPLEX_OBJECT" Then
            Dim s As String
            ' need to cope with runtime_label
            If CType(EIF_Structure, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("name")) Then
                s = "name"
            ElseIf CType(EIF_Structure, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("runtime_label")) Then
                'can be removed in the future
                s = "runtime_label"
            Else
                Return
            End If
            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            attribute = CType(EIF_Structure, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil(s))
            mRunTimeConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
        End If
    End Sub

    Sub New(ByVal XML_Structure As XMLParser.C_OBJECT)
        sNodeId = XML_Structure.node_id
        cOccurrences = ArchetypeEditor.XML_Classes.XML_Tools.SetOccurrences(XML_Structure.occurrences)
        mType = ReferenceModel.StructureTypeFromString(XML_Structure.rm_type_name)

        If XML_Structure.GetType.ToString = "XMLParser.C_COMPLEX_OBJECT" Then
            If Not CType(XML_Structure, XMLParser.C_COMPLEX_OBJECT).attributes Is Nothing Then
                For Each an_attribute As XMLParser.C_ATTRIBUTE In CType(XML_Structure, XMLParser.C_COMPLEX_OBJECT).attributes
                    If an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture) = "name" Then
                        mRunTimeConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                    End If
                Next
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
'The Original Code is RmStructure.vb.
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
