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

Public Enum StructureType
    Not_Set = 0
    [Single] = 1
    List = 2
    Tree = 3
    Table = 4
    Columns = 31
    Cluster = 50
    Element = 60

    [Event] = 70
    PointEvent = 71
    IntervalEvent = 72

    Reference = 75
    Slot = 80
    WorkFlowStep = 85

    ENTRY = 100
    EVALUATION = 101
    OBSERVATION = 102
    INSTRUCTION = 103
    ADMIN_ENTRY = 104

    SECTION = 200

    COMPOSITION = 300

    Data = 1000
    State = 1001
    Protocol = 1002
    History = 1003
    Action = 1004
    InstructionActExection = 1005
End Enum

Public Class RmStructure
    Implements ArcheTypeDefintionBasic

    '  maps to C_OBJECT in ADL
    Protected sNodeId As String
    Protected cOccurrences As New RmCardinality
    Protected mRunTimeConstraint As Constraint_Text
    Protected mType As StructureType

    Public Overridable ReadOnly Property Type() As StructureType Implements ArcheTypeDefintionBasic.Type
        Get
            Return mType
        End Get
    End Property
    Public Property NameConstraint() As Constraint_Text Implements ArcheTypeDefintionBasic.NameConstraint
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
    Public Property HasNameConstraint() As Boolean Implements ArcheTypeDefintionBasic.hasNameConstraint
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
    Public Property NodeId() As String Implements ArcheTypeDefintionBasic.RootNodeId
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
        Archetype.Definition.Data.Add(Me)
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

#Region "ADL oriented features"

    Sub New(ByVal EIF_Structure As openehr.openehr.am.archetype.constraint_model.C_OBJECT)
        sNodeId = EIF_Structure.node_id.to_cil
        cOccurrences = ArchetypeEditor.ADL_Classes.ADL_Tools.Instance.SetOccurrences(EIF_Structure.occurrences)
        mType = ReferenceModel.Instance.StructureTypeFromString(EIF_Structure.rm_type_name.to_cil)

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
            mRunTimeConstraint = RmElement.ProcessText(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
        End If
    End Sub

#Region "Processing ADL - incoming"

#End Region

#Region "Building ADL - outgoing"

#End Region

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
