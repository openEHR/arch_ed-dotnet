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

    Public Overrides Function HasLinks() As Boolean
        Return False
    End Function

    Overrides Function Copy() As RmStructure
        Dim rm As New RmSlot(mType)
        rm.mSlotConstraint = mSlotConstraint
        rm.Occurrences = cOccurrences
        rm.NodeId = sNodeId
        Return rm
    End Function

    Sub New()
        MyBase.New("", StructureType.Slot)
        cOccurrences.IsUnbounded = True
        mSlotConstraint = New Constraint_Slot
    End Sub

    Sub New(ByVal a_type As StructureType)
        MyBase.New("", StructureType.Slot)
        cOccurrences.IsUnbounded = True
        mSlotConstraint = New Constraint_Slot
        mSlotConstraint.RM_ClassType = a_type
    End Sub

#Region "ADL and XML oriented features"

    Sub New(ByVal archetypeSlot As AdlParser.ArchetypeSlot)
        MyBase.New(archetypeSlot)

        mSlotConstraint = New Constraint_Slot

        Select Case archetypeSlot.RmTypeName.ToCil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
            Case "SECTION"
                mSlotConstraint.RM_ClassType = StructureType.SECTION
            Case "ENTRY"
                mSlotConstraint.RM_ClassType = StructureType.ENTRY
            Case "ADMIN_ENTRY"
                mSlotConstraint.RM_ClassType = StructureType.ADMIN_ENTRY
            Case "OBSERVATION"
                mSlotConstraint.RM_ClassType = StructureType.OBSERVATION
            Case "EVALUATION"
                mSlotConstraint.RM_ClassType = StructureType.EVALUATION
            Case "ACTION"
                mSlotConstraint.RM_ClassType = StructureType.ACTION
            Case "INSTRUCTION"
                mSlotConstraint.RM_ClassType = StructureType.INSTRUCTION
            Case "ITEM_SINGLE"
                mSlotConstraint.RM_ClassType = StructureType.Single
            Case "ITEM_LIST"
                mSlotConstraint.RM_ClassType = StructureType.List
            Case "ITEM_TREE"
                mSlotConstraint.RM_ClassType = StructureType.Tree
            Case "ITEM_TABLE"
                mSlotConstraint.RM_ClassType = StructureType.Table
            Case "ITEM"
                mSlotConstraint.RM_ClassType = StructureType.Item
            Case "CLUSTER"
                mSlotConstraint.RM_ClassType = StructureType.Cluster
            Case "ELEMENT"
                mSlotConstraint.RM_ClassType = StructureType.Element
        End Select

        If archetypeSlot.HasIncludes Then
            For i As Integer = 1 To archetypeSlot.Includes.Count
                Dim assert As AdlParser.Assertion = CType(archetypeSlot.Includes.ITh(i), AdlParser.Assertion)
                Dim pattern As String = ArchetypeEditor.ADL_Classes.ADL_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    mSlotConstraint.IncludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(mSlotConstraint.RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + mSlotConstraint.RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Include.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Include.Add(s)
                    Next
                End If
            Next
        End If

        If archetypeSlot.HasExcludes Then
            For i As Integer = 1 To archetypeSlot.Excludes.Count
                Dim assert As AdlParser.Assertion = CType(archetypeSlot.Excludes.ITh(i), AdlParser.Assertion)
                Dim pattern As String = ArchetypeEditor.ADL_Classes.ADL_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    mSlotConstraint.ExcludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(mSlotConstraint.RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + mSlotConstraint.RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Exclude.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Exclude.Add(s)
                    Next
                End If
            Next
        End If
    End Sub

    Sub New(ByVal archetypeSlot As XMLParser.ARCHETYPE_SLOT)
        MyBase.New(archetypeSlot)

        mSlotConstraint = New Constraint_Slot

        Select Case archetypeSlot.rm_type_name.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
            Case "SECTION"
                mSlotConstraint.RM_ClassType = StructureType.SECTION
            Case "ENTRY"
                mSlotConstraint.RM_ClassType = StructureType.ENTRY
            Case "ADMIN_ENTRY"
                mSlotConstraint.RM_ClassType = StructureType.ADMIN_ENTRY
            Case "OBSERVATION"
                mSlotConstraint.RM_ClassType = StructureType.OBSERVATION
            Case "EVALUATION"
                mSlotConstraint.RM_ClassType = StructureType.EVALUATION
            Case "ACTION"
                mSlotConstraint.RM_ClassType = StructureType.ACTION
            Case "INSTRUCTION"
                mSlotConstraint.RM_ClassType = StructureType.INSTRUCTION
            Case "ITEM_SINGLE"
                mSlotConstraint.RM_ClassType = StructureType.Single
            Case "ITEM_LIST"
                mSlotConstraint.RM_ClassType = StructureType.List
            Case "ITEM_TREE"
                mSlotConstraint.RM_ClassType = StructureType.Tree
            Case "ITEM_TABLE"
                mSlotConstraint.RM_ClassType = StructureType.Table
            Case "ITEM"
                mSlotConstraint.RM_ClassType = StructureType.Item
            Case "CLUSTER"
                mSlotConstraint.RM_ClassType = StructureType.Cluster
            Case "ELEMENT"
                mSlotConstraint.RM_ClassType = StructureType.Element
        End Select

        If archetypeSlot.includes IsNot Nothing AndAlso archetypeSlot.includes.Length > 0 Then
            For i As Integer = 0 To archetypeSlot.includes.Length - 1
                Dim assert As XMLParser.ASSERTION = CType(archetypeSlot.includes(i), XMLParser.ASSERTION)
                Dim pattern As String = ArchetypeEditor.XML_Classes.XML_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    mSlotConstraint.IncludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(mSlotConstraint.RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + mSlotConstraint.RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Include.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Include.Add(s)
                    Next
                End If
            Next
        End If

        If archetypeSlot.excludes IsNot Nothing AndAlso archetypeSlot.excludes.Length > 0 Then
            For i As Integer = 0 To archetypeSlot.excludes.Length - 1
                Dim assert As XMLParser.ASSERTION = CType(archetypeSlot.excludes(i), XMLParser.ASSERTION)
                Dim pattern As String = ArchetypeEditor.XML_Classes.XML_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    mSlotConstraint.ExcludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(mSlotConstraint.RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + mSlotConstraint.RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Exclude.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        mSlotConstraint.Exclude.Add(s)
                    Next
                End If
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
