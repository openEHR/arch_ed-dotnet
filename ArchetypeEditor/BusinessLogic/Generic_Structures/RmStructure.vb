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

Public Enum StructureType
    Not_Set = 0
    [Structure] = 85
    [Single] = 105
    List = 106
    Tree = 107
    Table = 108
    Columns = 164
    Item = 673
    Cluster = 313
    Element = 567

    [Event] = 433
    PointEvent = 566
    IntervalEvent = 565

    Reference = 564
    Slot = 312
    CarePathwayStep = 563

    ENTRY = 559
    CARE_ENTRY = 674
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

    Participation = 654
    Link = 659
    OtherParticipations = 660

    ISM_TRANSITION = 1005
End Enum

Public Class RmStructure
    Implements ArcheTypeDefinitionBasic

    '  maps to C_OBJECT in ADL
    Protected sNodeId As String
    Protected cOccurrences As New RmCardinality
    Protected cExistence As New RmExistence
    Protected mNameConstraint As Constraint_Text
    Protected mStructureType As StructureType
    Protected mLinks As New System.Collections.Generic.List(Of RmLink)

    Public Overridable ReadOnly Property Type() As StructureType Implements ArcheTypeDefinitionBasic.Type
        Get
            Return mStructureType
        End Get
    End Property

    Public Property NameConstraint() As Constraint_Text Implements ArcheTypeDefinitionBasic.NameConstraint
        Get
            If mNameConstraint Is Nothing Then
                mNameConstraint = New Constraint_Text
            End If

            Return mNameConstraint
        End Get
        Set(ByVal Value As Constraint_Text)
            mNameConstraint = Value
        End Set
    End Property

    Public Property Links() As System.Collections.Generic.List(Of RmLink) Implements ArcheTypeDefinitionBasic.RootLinks
        Get
            Return mLinks
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of RmLink))
            mLinks = value
        End Set
    End Property

    Public Property HasNameConstraint() As Boolean Implements ArcheTypeDefinitionBasic.HasNameConstraint
        Get
            Return Not mNameConstraint Is Nothing
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                If mNameConstraint Is Nothing Then
                    mNameConstraint = New Constraint_Text
                End If
            Else
                mNameConstraint = Nothing
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

    Public Property Existence() As RmExistence
        Get
            Return cExistence
        End Get
        Set(ByVal Value As RmExistence)
            cExistence = Value
        End Set
    End Property

    Public ReadOnly Property SpecialisationDepth() As Integer
        Get
            Dim result As Integer = 0

            If Not NodeId Is Nothing Then
                result = NodeId.Split("."c).Length - 1
            End If

            Return result
        End Get
    End Property

    Public Overridable Function CanRemove(ByVal archetypeSpecialisationDepth As Integer) As Boolean
        Dim depth As Integer = SpecialisationDepth()
        Return archetypeSpecialisationDepth = depth And (depth = 0 Or NodeId.StartsWith("at0.") Or NodeId.IndexOf(".0.") > -1)
    End Function

    Public Overridable Function CanSpecialise(ByVal archetypeSpecialisationDepth As Integer) As Boolean
        Dim depth As Integer = SpecialisationDepth()
        Return archetypeSpecialisationDepth > 0 And (depth < archetypeSpecialisationDepth Or Occurrences.IsMultiple)
    End Function

    Public Overridable Function HasLinks() As Boolean
        For Each l As RmLink In mLinks
            If l.HasConstraint Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Overridable Function Copy() As RmStructure
        Dim result As New RmStructure(sNodeId, mStructureType)
        result.cOccurrences = cOccurrences.Copy

        If Not mNameConstraint Is Nothing Then
            result.mNameConstraint = mNameConstraint.Copy
        End If

        Return result
    End Function

    Sub New(ByRef Archetype As Archetype)
        ' For building new parse tree - and a root structure
        If Archetype.Definition.Type <> StructureType.Element Then
            CType(Archetype.Definition, ArchetypeDefinition).Data.Add(Me)
        Else
            Debug.Assert(False, "??")
        End If
    End Sub

    Sub New(ByVal struct As RmStructure)
        mStructureType = struct.mStructureType
        sNodeId = struct.sNodeId
        cOccurrences = struct.cOccurrences.Copy()

        If struct.HasNameConstraint Then
            mNameConstraint = struct.NameConstraint
        End If
    End Sub

    Sub New(ByVal NodeId As String, ByVal a_structure_type As StructureType)
        sNodeId = NodeId
        mStructureType = a_structure_type
    End Sub

    Sub New(ByVal EIF_Structure As openehr.openehr.am.archetype.constraint_model.C_OBJECT)
        If EIF_Structure.is_addressable Then
            sNodeId = EIF_Structure.node_id.to_cil
        End If

        cOccurrences = ArchetypeEditor.ADL_Classes.ADL_Tools.SetOccurrences(EIF_Structure.occurrences)
        mStructureType = ReferenceModel.StructureTypeFromString(EIF_Structure.rm_type_name.to_cil)

        If EIF_Structure.generator.to_cil = "C_COMPLEX_OBJECT" Then
            Dim s As String

            ' need to cope with runtime_label
            If CType(EIF_Structure, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).has_attribute(Eiffel.String("name")) Then
                s = "name"
            ElseIf CType(EIF_Structure, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).has_attribute(Eiffel.String("runtime_label")) Then
                'can be removed in the future
                s = "runtime_label"
            Else
                Return
            End If

            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(EIF_Structure, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).c_attribute_at_path(Eiffel.String(s))

            If attribute.has_children Then
                mNameConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
            End If
        End If
    End Sub

    Sub New(ByVal XML_Structure As XMLParser.C_OBJECT)
        If Not String.IsNullOrEmpty(XML_Structure.node_id) Then
            sNodeId = XML_Structure.node_id
        End If

        cOccurrences = ArchetypeEditor.XML_Classes.XML_Tools.SetOccurrences(XML_Structure.occurrences)
        mStructureType = ReferenceModel.StructureTypeFromString(XML_Structure.rm_type_name)

        If XML_Structure.GetType.ToString = "XMLParser.C_COMPLEX_OBJECT" Then
            If Not CType(XML_Structure, XMLParser.C_COMPLEX_OBJECT).attributes Is Nothing Then
                For Each an_attribute As XMLParser.C_ATTRIBUTE In CType(XML_Structure, XMLParser.C_COMPLEX_OBJECT).attributes
                    If an_attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture) = "name" Then
                        mNameConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                    End If
                Next
            End If
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
