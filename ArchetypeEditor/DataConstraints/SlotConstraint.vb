'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on an archetype slot"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Public Class Constraint_Slot
    Inherits Constraint

    Friend colInclude As New CollectionOfSlots
    Friend colExclude As New CollectionOfSlots
    Dim mType As StructureType
    Dim mExcludeAll As Boolean
    Dim mIncludeAll As Boolean

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Slot()
        result.colInclude = colInclude.Copy
        result.colExclude = colExclude.Copy
        result.mType = mType
        result.mExcludeAll = mExcludeAll
        result.mIncludeAll = mIncludeAll
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Slot
        End Get
    End Property

    Public ReadOnly Property hasSlots() As Boolean
        Get
            Return colInclude.Count > 0 Or colExclude.Count > 0 Or mIncludeAll Or mExcludeAll
        End Get
    End Property

    Property RM_ClassType() As StructureType
        Get
            Return mType
        End Get
        Set(ByVal Value As StructureType)
            mType = Value
        End Set
    End Property

    Public Property IncludeAll() As Boolean
        Get
            Return mIncludeAll
        End Get
        Set(ByVal Value As Boolean)
            mIncludeAll = Value
        End Set
    End Property

    Public Property ExcludeAll() As Boolean
        Get
            Return mExcludeAll
        End Get
        Set(ByVal Value As Boolean)
            mExcludeAll = Value
        End Set
    End Property

    Public Property Include() As CollectionOfSlots
        Get
            Return colInclude
        End Get
        Set(ByVal Value As CollectionOfSlots)
            colInclude = Value
        End Set
    End Property

    Public Property Exclude() As CollectionOfSlots
        Get
            Return colExclude
        End Get
        Set(ByVal Value As CollectionOfSlots)
            colExclude = Value
        End Set
    End Property

    Sub SetRmTypeName(ByVal name As String)
        Select Case name.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
            Case "SECTION"
                RM_ClassType = StructureType.SECTION
            Case "ENTRY"
                RM_ClassType = StructureType.ENTRY
            Case "ADMIN_ENTRY"
                RM_ClassType = StructureType.ADMIN_ENTRY
            Case "OBSERVATION"
                RM_ClassType = StructureType.OBSERVATION
            Case "EVALUATION"
                RM_ClassType = StructureType.EVALUATION
            Case "ACTION"
                RM_ClassType = StructureType.ACTION
            Case "INSTRUCTION"
                RM_ClassType = StructureType.INSTRUCTION
            Case "ITEM_SINGLE"
                RM_ClassType = StructureType.Single
            Case "ITEM_LIST"
                RM_ClassType = StructureType.List
            Case "ITEM_TREE"
                RM_ClassType = StructureType.Tree
            Case "ITEM_TABLE"
                RM_ClassType = StructureType.Table
            Case "ITEM"
                RM_ClassType = StructureType.Item
            Case "CLUSTER"
                RM_ClassType = StructureType.Cluster
            Case "ELEMENT"
                RM_ClassType = StructureType.Element
        End Select
    End Sub

    Sub New()
    End Sub

    Sub New(ByVal slot As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)
        SetRmTypeName(slot.rm_type_name.to_cil)

        If slot.has_includes Then
            For i As Integer = 1 To slot.includes.count
                Dim assert As openehr.openehr.am.archetype.assertion.ASSERTION = CType(slot.includes.i_th(i), openehr.openehr.am.archetype.assertion.ASSERTION)
                Dim pattern As String = ArchetypeEditor.ADL_Classes.ADL_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    IncludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        Include.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        Include.Add(s)
                    Next
                End If
            Next
        End If

        If slot.has_excludes Then
            For i As Integer = 1 To slot.excludes.count
                Dim assert As openehr.openehr.am.archetype.assertion.ASSERTION = CType(slot.excludes.i_th(i), openehr.openehr.am.archetype.assertion.ASSERTION)
                Dim pattern As String = ArchetypeEditor.ADL_Classes.ADL_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    ExcludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        Exclude.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        Exclude.Add(s)
                    Next
                End If
            Next
        End If
    End Sub

    Sub New(ByVal slot As XMLParser.ARCHETYPE_SLOT)
        SetRmTypeName(slot.rm_type_name)

        If slot.includes IsNot Nothing AndAlso slot.includes.Length > 0 Then
            For i As Integer = 0 To slot.includes.Length - 1
                Dim assert As XMLParser.ASSERTION = CType(slot.includes(i), XMLParser.ASSERTION)
                Dim pattern As String = ArchetypeEditor.XML_Classes.XML_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    IncludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        Include.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        Include.Add(s)
                    Next
                End If
            Next
        End If

        If slot.excludes IsNot Nothing AndAlso slot.excludes.Length > 0 Then
            For i As Integer = 0 To slot.excludes.Length - 1
                Dim assert As XMLParser.ASSERTION = CType(slot.excludes(i), XMLParser.ASSERTION)
                Dim pattern As String = ArchetypeEditor.XML_Classes.XML_Tools.GetConstraintFromAssertion(assert)

                If pattern = ".*" Then
                    ExcludeAll = True
                ElseIf Not ReferenceModel.IsAbstract(RM_ClassType) Then
                    Dim classPrefix As String = ReferenceModel.ReferenceModelName + "-" + RM_ClassType.ToString.ToUpperInvariant

                    For Each s As String In pattern.Split("|"c)
                        Exclude.Add(s.Replace(classPrefix, "").TrimStart("\"c, "."c))
                    Next
                Else
                    For Each s As String In pattern.Split("|"c)
                        Exclude.Add(s)
                    Next
                End If
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
'The Original Code is SlotConstraint.vb.
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