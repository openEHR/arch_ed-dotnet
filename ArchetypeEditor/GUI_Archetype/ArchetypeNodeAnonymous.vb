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

Option Strict On

Public Class ArchetypeNodeAnonymous
    Implements ArchetypeNode

    Private mRMStructure As RmStructure
    Private mText As String

    Public Property Text() As String Implements ArchetypeNode.Text
        Get
            Return mText
        End Get
        Set(ByVal Value As String)
            'noop - required as overriden in specialised classes
            Debug.Assert(False)
        End Set
    End Property
    Public ReadOnly Property isAnonymous() As Boolean Implements ArchetypeNode.IsAnonymous
        Get
            Return True
        End Get
    End Property
    Public Property Occurrences() As RmCardinality Implements ArchetypeNode.Occurrences
        Get
            Return mRMStructure.Occurrences
        End Get
        Set(ByVal Value As RmCardinality)
            mRMStructure.Occurrences = Value
        End Set
    End Property
    Public ReadOnly Property IsMandatory() As Boolean Implements ArchetypeNode.IsMandatory
        Get
            Return (mRMStructure.Occurrences.MinCount > 0)
        End Get
    End Property
    Public ReadOnly Property RM_Class() As RmStructure Implements ArchetypeNode.RM_Class
        Get
            Return mRMStructure
        End Get
    End Property

    Public Sub Translate() Implements ArchetypeNode.Translate
        Select Case mRMStructure.Type
            Case StructureType.Slot
                Select Case CType(mRMStructure, RmSlot).SlotConstraint.RM_ClassType
                    Case StructureType.ENTRY
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(559, "ENTRY")
                    Case StructureType.OBSERVATION
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(554, "OBSERVATION")
                    Case StructureType.EVALUATION
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(555, "EVALUATION")
                    Case StructureType.INSTRUCTION
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(557, "INSTRUCTION")
                    Case StructureType.ACTION
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(556, "ACTION")
                    Case StructureType.ADMIN_ENTRY
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(560, "Administration ENTRY")
                    Case StructureType.SECTION
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(314, "SECTION")
                    Case StructureType.Cluster
                        mText = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(313, "Cluster")
                End Select
            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Function ToRichText(ByVal level As Integer) As String Implements ArchetypeNode.ToRichText
        Dim s, statement As String
        Dim nl As String = Chr(10) & Chr(13)

        If mRMStructure.Type = StructureType.Slot Then
            Dim slot_constraint As Constraint_Slot

            Try
                slot_constraint = CType(mRMStructure, RmSlot).SlotConstraint
            Catch ex As Exception
                Return ""
            End Try

            s &= Space(3 * level) & slot_constraint.RM_ClassType.ToString & ":\par"
            If slot_constraint.Include.Count > 0 Then
                If slot_constraint.IncludeAll Then
                    s &= nl & Space(3 * (level + 1)) & "  Include ALL\par"
                Else
                    s &= nl & Space(3 * (level + 1)) & "  Include:\par"
                    For Each statement In slot_constraint.Include
                        s &= nl & Space(3 * (level + 2)) & statement & "\par"
                    Next
                End If
            End If
            If slot_constraint.Exclude.Count > 0 Then
                If slot_constraint.ExcludeAll Then
                    s &= nl & Space(3 * (level + 1)) & "  Exclude ALL\par"
                Else
                    s &= nl & Space(3 * (level + 1)) & "  Exclude:\par"
                    For Each statement In slot_constraint.Exclude
                        s &= nl & Space(3 * (level + 2)) & statement & "\par"
                    Next
                End If
            End If
            Return s
        End If
    End Function

    Function ToHTML(ByVal level As Integer) As String Implements ArchetypeNode.ToHTML
        Dim s As String
        Dim nl As String = Chr(10) & Chr(13)

        If mRMStructure.Type = StructureType.Slot Then
            Dim slot_constraint As Constraint_Slot

            Try
                slot_constraint = CType(mRMStructure, RmSlot).SlotConstraint
            Catch ex As Exception
                Return ""
            End Try

            s = "<table><tr><td width=""" & (level * 20).ToString & """></td><td><table>"

            s &= Environment.NewLine & "<tr>"
            s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(312, "Slot") & "</td>"
            If slot_constraint.RM_ClassType = StructureType.SECTION Then
                s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(172, "Include sections") & "</td>"
                s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(173, "Exclude sections") & "</td>"
            ElseIf slot_constraint.RM_ClassType = StructureType.ENTRY Then
                s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(175, "Include entries") & "</td>"
                s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(176, "Exclude entries") & "</td>"
            End If
            s &= Environment.NewLine & "</tr>"
            s &= Environment.NewLine & "<tr>"
            s &= Environment.NewLine & "<td>" & slot_constraint.RM_ClassType.ToString & "</td>"
            If slot_constraint.Include.Count > 0 Then
                If slot_constraint.IncludeAll Then
                    s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(11, "Allow all") & "</td>"
                Else
                    s &= Environment.NewLine & "<td>"
                    For Each statement As String In slot_constraint.Include
                        s &= Environment.NewLine & statement & "<br>"
                    Next
                    s &= Environment.NewLine & "</td>"
                End If
            End If
            If slot_constraint.Exclude.Count > 0 Then
                If slot_constraint.ExcludeAll Then
                    s &= Environment.NewLine & "<td>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(101, "All") & "</td>"
                Else
                    s &= Environment.NewLine & "<td>"
                    For Each statement As String In slot_constraint.Exclude
                        s &= Environment.NewLine & statement & "<br>"
                    Next
                    s &= Environment.NewLine & "</td>"
                End If
            End If
            s &= "</table></td></table>"
            Return s
        End If
    End Function


    Public Function Copy() As ArchetypeNode Implements ArchetypeNode.Copy
        Dim an_anon As New ArchetypeNodeAnonymous

        an_anon.mRMStructure = Me.mRMStructure.Copy
        an_anon.mText = Me.mText
        Return an_anon

    End Function

    Sub New()
        mRMStructure = New RmSlot
        Translate()
    End Sub

    Sub New(ByVal a_type As StructureType)
        ' set the text value to the class type
        mRMStructure = New RmSlot(a_type)
        Translate()
    End Sub

    Sub New(ByVal a_slot As RmSlot)
        mRMStructure = a_slot
        Translate()
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
'The Original Code is ArchetypeNodeAnonymous.vb.
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