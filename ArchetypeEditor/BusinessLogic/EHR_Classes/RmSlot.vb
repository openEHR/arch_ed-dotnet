'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Option Strict On

Public Class RmSlot
    Inherits RmStructure
    Private mSlotConstraint As Constraint_Slot

    Property SlotConstraint() As Constraint_Slot
        Get
            Return mSlotConstraint
        End Get
        Set(ByVal Value As Constraint_Slot)
            mSlotConstraint = Value
        End Set
    End Property

    Overrides ReadOnly Property Type() As StructureType
        Get
            Return StructureType.Slot
        End Get
    End Property

    Overrides Function Copy() As RmStructure
        Dim rm As New RmSlot(Me.mType)
        rm.mSlotConstraint = Me.mSlotConstraint
        rm.Occurrences = Me.cOccurrences
        Return rm
    End Function

    Sub New()
        MyBase.New("", StructureType.Slot)
        mSlotConstraint = New Constraint_Slot
    End Sub

    Sub New(ByVal a_type As StructureType)
        MyBase.new("", StructureType.Slot)
        mSlotConstraint = New Constraint_Slot
        mSlotConstraint.RM_ClassType = a_type
    End Sub

#Region "ADL oriented features"

#Region "Processing ADL - incoming"

    Sub New(ByVal an_archetype_slot As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)
        MyBase.New(an_archetype_slot)

        mSlotConstraint = New Constraint_Slot

        Select Case an_archetype_slot.rm_type_name.to_cil
            Case "SECTION"
                mSlotConstraint.RM_ClassType = StructureType.SECTION
            Case "ENTRY"
                mSlotConstraint.RM_ClassType = StructureType.ENTRY
            Case "OBSERVATION"
                mSlotConstraint.RM_ClassType = StructureType.OBSERVATION
            Case "EVALUATION"
                mSlotConstraint.RM_ClassType = StructureType.EVALUATION
            Case "INSTRUCTION"
                mSlotConstraint.RM_ClassType = StructureType.INSTRUCTION
            Case "Simple", "Single"
                mSlotConstraint.RM_ClassType = StructureType.Single
            Case "List"
                mSlotConstraint.RM_ClassType = StructureType.List
            Case "Tree"
                mSlotConstraint.RM_ClassType = StructureType.Tree
            Case "Table"
                mSlotConstraint.RM_ClassType = StructureType.Table
            Case "Cluster"
                mSlotConstraint.RM_ClassType = StructureType.Cluster
        End Select

        If an_archetype_slot.has_includes Then
            Dim s As String
            For i As Integer = 1 To an_archetype_slot.includes.count
                Dim assert As openehr.openehr.am.archetype.assertion.ASSERTION
                assert = CType(an_archetype_slot.includes.i_th(i), openehr.openehr.am.archetype.assertion.ASSERTION)
                s = ADL_Tools.Instance.GetDomainConceptFromAssertion(assert)
                If s = "/.*/" Then
                    mSlotConstraint.IncludeAll = True
                Else
                    mSlotConstraint.Include.Add(s.Trim("/".ToCharArray))
                End If

            Next
        End If

        If an_archetype_slot.has_excludes Then
            Dim s As String
            For i As Integer = 1 To an_archetype_slot.excludes.count
                Dim assert As openehr.openehr.am.archetype.assertion.ASSERTION
                assert = CType(an_archetype_slot.excludes.i_th(i), openehr.openehr.am.archetype.assertion.ASSERTION)
                s = ADL_Tools.Instance.GetDomainConceptFromAssertion(assert)
                If s = "/.*/" Then
                    mSlotConstraint.ExcludeAll = True
                Else
                    mSlotConstraint.Exclude.Add(s.Trim(("/").ToCharArray))
                End If
            Next
        End If
    End Sub

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
'The Original Code is RmSlot.vb.
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
