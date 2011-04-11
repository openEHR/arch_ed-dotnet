'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class ArchetypeNodeAnonymous
    Inherits ArchetypeNode

    Private Property Slot() As RmSlot
        Get
            Return CType(Item, RmSlot)
        End Get
        Set(ByVal Value As RmSlot)
            Item = Value
        End Set
    End Property

    Public Overrides Property Constraint() As Constraint
        Get
            Return Slot.SlotConstraint
        End Get
        Set(ByVal value As Constraint)
            Slot.SlotConstraint = CType(value, Constraint_Slot)
        End Set
    End Property

    Public ReadOnly Property SlotConstraint() As Constraint_Slot
        Get
            Return Slot.SlotConstraint
        End Get
    End Property

    Private mText As String

    Public Overrides Property Text() As String
        Get
            Return mText
        End Get
        Set(ByVal Value As String)
            'noop - required as overriden in specialised classes
            Debug.Assert(False)
        End Set
    End Property

    Public Overrides ReadOnly Property IsAnonymous() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Translate()
        Select Case Item.Type
            Case StructureType.Slot
                Select Case SlotConstraint.RM_ClassType
                    Case StructureType.ENTRY
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(559, "ENTRY")
                    Case StructureType.OBSERVATION
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(554, "OBSERVATION")
                    Case StructureType.EVALUATION
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(555, "EVALUATION")
                    Case StructureType.INSTRUCTION
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(557, "INSTRUCTION")
                    Case StructureType.ACTION
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(556, "ACTION")
                    Case StructureType.ADMIN_ENTRY
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(560, "Administration ENTRY")
                    Case StructureType.SECTION
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(314, "SECTION")
                    Case StructureType.Item
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(673, "Item")
                    Case StructureType.Cluster
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(313, "Cluster")
                    Case StructureType.Element
                        mText = Filemanager.Master.OntologyManager.GetOpenEHRTerm(567, "Element")
                End Select
            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        Dim s, statement As String

        If Item.Type = StructureType.Slot Then
            Dim slot_constraint As Constraint_Slot = SlotConstraint
            s = Space(3 * level) & slot_constraint.RM_ClassType.ToString & ":\par"

            If slot_constraint.IncludeAll Then
                s &= Environment.NewLine & Space(3 * (level + 1)) & "  Include ALL\par"
            ElseIf slot_constraint.Include.Count > 0 Then
                s &= Environment.NewLine & Space(3 * (level + 1)) & "  Include:\par"

                For Each statement In slot_constraint.Include
                    s &= Environment.NewLine & Space(3 * (level + 2)) & statement & "\par"
                Next
            End If

            If slot_constraint.ExcludeAll Then
                s &= Environment.NewLine & Space(3 * (level + 1)) & "  Exclude ALL\par"
            ElseIf slot_constraint.Exclude.Count > 0 Then
                s &= Environment.NewLine & Space(3 * (level + 1)) & "  Exclude:\par"

                For Each statement In slot_constraint.Exclude
                    s &= Environment.NewLine & Space(3 * (level + 2)) & statement & "\par"
                Next
            End If

            Return s
        End If

        Return ""
    End Function

    Public Overrides Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String
        Dim s As String

        If Item.Type = StructureType.Slot Then
            Dim slot_constraint As Constraint_Slot = SlotConstraint
            s = "<tr><td><table><tr><td width=""" & (level * 20).ToString & """></td><td>"
            s &= "<img border=""0"" src=""Images/slot.gif"" width=""32"" height=""32"" align=""middle"">"
            s &= "</td></tr></table></td>"

            's &= Environment.NewLine & "<tr>"
            s &= Environment.NewLine & "<td>" & Filemanager.GetOpenEhrTerm(312, "Slot") & "</td>"

            Dim include_label As String
            Dim exclude_label As String

            If slot_constraint.RM_ClassType = StructureType.SECTION Then
                include_label = Filemanager.GetOpenEhrTerm(172, "Include sections")
                exclude_label = Filemanager.GetOpenEhrTerm(173, "Exclude sections")
            ElseIf slot_constraint.RM_ClassType = StructureType.ENTRY Then
                include_label = Filemanager.GetOpenEhrTerm(175, "Include entries")
                exclude_label = Filemanager.GetOpenEhrTerm(176, "Exclude entries")
            Else
                include_label = Filemanager.GetOpenEhrTerm(625, "Include") & " : " & slot_constraint.RM_ClassType.ToString
                exclude_label = Filemanager.GetOpenEhrTerm(626, "Exclude") & " : " & slot_constraint.RM_ClassType.ToString
            End If

            include_label &= "<br>"
            exclude_label &= "<br>"

            s &= Environment.NewLine & "<td>" & include_label
            If slot_constraint.Include.Count > 0 Then
                If slot_constraint.IncludeAll Then
                    s &= Filemanager.GetOpenEhrTerm(11, "Allow all")
                Else
                    For Each statement As String In slot_constraint.Include
                        s &= Environment.NewLine & statement & "<br>"
                    Next

                End If
            End If
            s &= "</td>"

            s &= Environment.NewLine & "<td>" & exclude_label
            If slot_constraint.Exclude.Count > 0 Then
                If slot_constraint.ExcludeAll Then
                    s &= Filemanager.GetOpenEhrTerm(11, "Allow all")
                Else
                    For Each statement As String In slot_constraint.Exclude
                        s &= Environment.NewLine & statement & "<br>"
                    Next

                End If
            End If
            s &= "</td>"
            If Main.Instance.Options.ShowCommentsInHtml Then
                s &= "<td>&nbsp;</td>"
            End If
            s &= "</tr>"
            Return s
        End If

        Return ""
    End Function

    Public Overrides Function Copy() As ArchetypeNode
        Dim result As New ArchetypeNodeAnonymous
        result.Item = Item.Copy
        result.mText = mText
        Return result
    End Function

    Sub New()
        Item = New RmSlot
        Translate()
    End Sub

    Sub New(ByVal type As StructureType)
        ' set the text value to the class type
        Item = New RmSlot(type)

        'SRH: 6th Jan 2010 - EDT 585 - default for structures should be 0..1 (except for Data which should be 1..1)

        If type = StructureType.Tree Or type = StructureType.Table Or type = StructureType.List Or type = StructureType.Single Then
            Occurrences.MinCount = 0
            Occurrences.MaxCount = 1
        End If

        Translate()
    End Sub

    Sub New(ByVal slot As RmSlot)
        Item = slot
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
