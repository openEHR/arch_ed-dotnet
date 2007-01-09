'
'
'	component:   "openEHR Archetype Project"
'	description: "This class models the structural and compositional rules that apply to the different reference models"                    
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

Public Enum ReferenceModelType
    Not_Set = -1
    openEHR_EHR = 0
    CEN_EHR = 1
    HL7_CDA = 2
    openEHR_Demographic = 3
    ' These must match the indexes of collection property of the Model ComboBox
End Enum

Public Enum StateMachineType
    Not_Set = 0
    Initial = 524
    Active = 245
    Completed = 532
    ActiveSuspended = 530
    InitialSuspended = 527
    ActiveAborted = 531
    InitialAborted = 528
    Scheduled = 529
End Enum

Public Class ReferenceModel
    Private Shared mRefModelType As ReferenceModelType
    Private Shared mArchetypedClass As StructureType
    Private Shared mStructureClass As StructureType
    Private Shared mReferenceModelNames As Collections.Hashtable
    Private Shared mReferenceModelDataTypes As Collections.Hashtable
    Private Shared mValidReferenceModelNames As String() = {"openEHR-EHR"} ', "CEN-EHR"} ', "HL7-CDA"} ',"openEHR-Demographic"}


    Public Shared Function ModelType() As ReferenceModelType
        Return mRefModelType
    End Function
    Public Shared Sub SetModelType(ByVal Value As ReferenceModelType)
        mRefModelType = Value
        LoadReferenceModelNames()
        LoadReferenceModelDataTypes()
    End Sub

    Public Shared Function ReferenceModelName() As String
        Return mValidReferenceModelNames(mRefModelType)
    End Function

    Public Shared Sub SetReferenceModelName(ByVal Value As String)
        Debug.Assert(IsValidReferenceModelName(Value))
        Dim i As Integer = Array.IndexOf(mValidReferenceModelNames, Value)
        mRefModelType = CType(i, ReferenceModelType)
        LoadReferenceModelNames()
    End Sub

    Public Shared Function ValidReferenceModelNames() As String()
        Return mValidReferenceModelNames
    End Function

    Public Shared Function ArchetypedClass() As StructureType
        Return mArchetypedClass
    End Function

    Public Shared Sub SetArchetypedClass(ByVal Value As StructureType)
        ' not a null value
        Debug.Assert(Not Value = StructureType.Not_Set)
        ' is a valid type for this reference model
        Debug.Assert(IsValidArchetypeDefinition(Value))
        mArchetypedClass = Value
        Select Case Value
            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                mStructureClass = Value
        End Select
    End Sub

    Public Shared Function StructureClass() As StructureType
        Return mStructureClass
    End Function

    Public Shared Sub SetStructureClass(ByVal Value As StructureType)
        Select Case Value
            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                mStructureClass = Value
            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Private Shared Sub LoadReferenceModelNames()
        mReferenceModelNames = New Hashtable

        Select Case mRefModelType
            Case ReferenceModelType.openEHR_EHR
                mReferenceModelNames.Add(105, "ITEM_SINGLE")
                mReferenceModelNames.Add(106, "ITEM_LIST")
                mReferenceModelNames.Add(107, "ITEM_TREE")
                mReferenceModelNames.Add(108, "ITEM_TABLE")
                mReferenceModelNames.Add(313, "CLUSTER")
                mReferenceModelNames.Add(567, "ELEMENT")
                mReferenceModelNames.Add(433, "EVENT")
                mReferenceModelNames.Add(566, "POINT_EVENT")
                mReferenceModelNames.Add(565, "INTERVAL_EVENT")
                mReferenceModelNames.Add(563, "WORKFLOW_STEP")
                mReferenceModelNames.Add(275, "EVENT_SERIES") 'Obsolete
                mReferenceModelNames.Add(1005, "pathway_specification") 'Obsolete
        End Select

    End Sub

    Public Shared Function RM_StructureName(ByVal s_type As StructureType) As String
        If mReferenceModelNames.ContainsKey(CType(s_type, Integer)) Then
            Return CStr(mReferenceModelNames.Item(CType(s_type, Integer)))
        Else
            Return s_type.ToString
        End If
    End Function

    Private Shared Sub LoadReferenceModelDataTypes()
        mReferenceModelDataTypes = New Hashtable

        Select Case mRefModelType
            Case ReferenceModelType.openEHR_EHR
                mReferenceModelDataTypes.Add(12, "INTERVAL<COUNT>")
                mReferenceModelDataTypes.Add(13, "INTERVAL<QUANTITY>")
                mReferenceModelDataTypes.Add(14, "INTERVAL<DATE_TIME>")
        End Select

    End Sub

    Public Shared Function RM_DataTypeName(ByVal d_type As ConstraintType) As String
        Dim result As String
        If mReferenceModelDataTypes.ContainsKey(CType(d_type, Integer)) Then
            result = CStr(mReferenceModelDataTypes.Item(CType(d_type, Integer)))
        Else
            result = d_type.ToString.ToUpperInvariant()
        End If

        Return String.Format("DV_{0}", result)

    End Function

    Public Shared Function IsValidReferenceModelName(ByVal RefModelName As String) As Boolean
        Return Array.IndexOf(mValidReferenceModelNames, RefModelName) > -1
    End Function

    Public Shared Function IsValidChild(ByVal Parent As StructureType, ByVal Child As StructureType) As Boolean
        Select Case mRefModelType
            Case ReferenceModelType.openEHR_EHR
                Select Case Parent
                    Case StructureType.COMPOSITION 'openEHR
                        Select Case Child
                            Case StructureType.SECTION, StructureType.List, StructureType.Table, StructureType.Single, StructureType.Tree
                                Return True
                        End Select
                    Case StructureType.SECTION 'openEHR
                        Select Case Child
                            Case StructureType.SECTION, StructureType.Slot
                                Return True
                        End Select
                    Case StructureType.OBSERVATION, StructureType.EVALUATION 'openEHR
                        ' set the current entry type
                        Select Case Child
                            Case StructureType.Data, StructureType.State, StructureType.Protocol
                                Return True
                        End Select
                    Case StructureType.ADMIN_ENTRY
                        ' only data for admin entry
                        Select Case Child
                            Case StructureType.Data
                                Return True
                        End Select
                    Case StructureType.INSTRUCTION
                        Select Case Child
                            Case StructureType.Activities, StructureType.Protocol
                                Return True
                        End Select
                    Case StructureType.ACTION
                        Select Case Child
                            Case StructureType.ISM_TRANSITION, StructureType.ActivityDescription, StructureType.Protocol
                                Return True
                        End Select
                    Case StructureType.Activity
                        Select Case Child
                            Case StructureType.List, StructureType.Table, StructureType.Tree, StructureType.Single, StructureType.Slot
                                Return True
                        End Select
                    Case StructureType.Data 'openEHR
                        Debug.Assert(mArchetypedClass <> 0)
                        Select Case mArchetypedClass
                            Case StructureType.EVALUATION, StructureType.ADMIN_ENTRY
                                Select Case Child
                                    Case StructureType.List, StructureType.Single, StructureType.Table, StructureType.Tree, StructureType.Slot
                                        mStructureClass = Child
                                        Return True
                                End Select
                            Case StructureType.OBSERVATION
                                Select Case Child
                                    Case StructureType.History
                                        Return True
                                End Select
                        End Select
                    Case StructureType.Activities
                        Select Case Child
                            Case StructureType.Activity
                                Return True
                        End Select
                    Case StructureType.Activity
                        Select Case Child
                            Case StructureType.ActivityDescription
                                Return True
                        End Select
                    Case StructureType.State, StructureType.Protocol
                        Select Case Child
                            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                                mStructureClass = Child
                                Return True
                            Case StructureType.History ' Allows for history based state information
                                Return True
                        End Select
                    Case StructureType.Single 'openEHR
                        mStructureClass = Parent
                        Select Case Child

                            Case StructureType.Element
                                Return True
                        End Select
                    Case StructureType.List 'openEHR
                        mStructureClass = Parent
                        Select Case Child
                            Case StructureType.Element, StructureType.Reference
                                Return True
                        End Select
                    Case StructureType.Tree, StructureType.Cluster 'openEHR
                        If Parent = StructureType.Tree Then
                            mStructureClass = Parent
                        End If
                        Select Case Child
                            Case StructureType.Element, StructureType.Reference, StructureType.Cluster
                                Return True
                        End Select
                    Case StructureType.Table 'openEHR
                        mStructureClass = Parent
                        Select Case Child
                            Case StructureType.Cluster
                                Return True
                        End Select
                    Case StructureType.Columns
                        Select Case Child
                            Case StructureType.Element
                                Return True
                        End Select
                    Case StructureType.ISM_TRANSITION
                        Select Case Child
                            Case StructureType.CarePathwayStep
                                Return True
                        End Select
                    Case StructureType.ActivityDescription
                        Select Case Child
                            Case StructureType.Slot, StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                                Return True
                        End Select
                End Select

            Case ReferenceModelType.CEN_EHR, ReferenceModelType.HL7_CDA
                Select Case Parent
                    Case StructureType.COMPOSITION 'CEN
                        Debug.Assert(False, "Not available")
                    Case StructureType.SECTION ' CEN
                        Select Case Child
                            Case StructureType.SECTION, StructureType.Slot
                                Return True
                        End Select
                    Case StructureType.ENTRY 'CEN
                        Select Case Child
                            Case StructureType.Tree, StructureType.Single, StructureType.List
                                Return True
                        End Select
                    Case StructureType.Tree, StructureType.Cluster 'CEN
                        If Parent = StructureType.Tree Then
                            mStructureClass = Parent
                        End If
                        Select Case Child
                            Case StructureType.Element, StructureType.Reference, StructureType.Cluster
                                Return True
                        End Select
                    Case StructureType.List, StructureType.Single
                        mStructureClass = Parent
                        Select Case Child
                            Case StructureType.Element, StructureType.Reference
                                Return True
                        End Select
                End Select
                'Case ReferenceModelType.HL7_CDA
            Case ReferenceModelType.openEHR_Demographic
                Debug.Assert(False, "Demographic model is not available as yet")
        End Select

        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " Does not conform to reference model", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Debug.Assert(False)
        Return False

    End Function

    Public Shared Function IsValidEntryType(ByVal a_structure_type As StructureType) As Boolean
        For Each st As StructureType In ValidEntryTypes()
            If st = a_structure_type Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function ValidEntryTypes() As StructureType()
        Select Case mRefModelType
            Case ReferenceModelType.openEHR_EHR
                Dim s(2) As StructureType
                s(0) = StructureType.OBSERVATION
                s(1) = StructureType.EVALUATION
                s(2) = StructureType.INSTRUCTION
                s(3) = StructureType.ADMIN_ENTRY
                Return s
            Case ReferenceModelType.CEN_EHR, ReferenceModelType.HL7_CDA
                Dim s(0) As StructureType
                s(0) = StructureType.ENTRY
                Return s
            Case ReferenceModelType.openEHR_Demographic
                Debug.Assert(False, "Not available")
                Return Nothing
            Case Else
                Debug.Assert(False, "Not available")
                Return Nothing
        End Select
    End Function


    Public Shared Function IsValidStrucutureType(ByVal a_structure_type As StructureType) As Boolean
        For Each st As StructureType In ValidStructureTypes()
            If st = a_structure_type Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function ValidStructureTypes() As StructureType()
        Select Case mArchetypedClass
            Case StructureType.Single, StructureType.List, StructureType.Tree, StructureType.Table
                Dim s(0) As StructureType
                s(0) = mArchetypedClass
                Return s
            Case StructureType.Cluster, StructureType.Columns, StructureType.Element, StructureType.Reference, StructureType.SECTION, StructureType.Slot
                'classes which cannot contain structure types        
                Dim s(0) As StructureType
                Return s ' empty
            Case Else
                Select Case mRefModelType
                    Case ReferenceModelType.openEHR_EHR
                        Dim s(3) As StructureType
                        s(0) = StructureType.Single
                        s(1) = StructureType.List
                        s(2) = StructureType.Tree
                        s(3) = StructureType.Table
                        Return s
                    Case ReferenceModelType.CEN_EHR, ReferenceModelType.HL7_CDA
                        Dim s(2) As StructureType
                        s(0) = StructureType.Single
                        s(1) = StructureType.List
                        s(2) = StructureType.Tree
                        Return s
                    Case ReferenceModelType.openEHR_Demographic
                        Debug.Assert(False, "Not available")
                        Dim s(0) As StructureType
                        Return s ' empty
                    Case Else
                        Dim s(0) As StructureType
                        Return s ' empty
                End Select
        End Select
    End Function

    Public Shared Function IsValidArchetypeDefinition(ByVal a_structure_type As StructureType) As Boolean
        For Each st As StructureType In ValidArchetypeDefinitions()
            If st = a_structure_type Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function validArchetypeSlots(ByVal a_structure_type As StructureType) As StructureType()
        Select Case a_structure_type
            Case StructureType.COMPOSITION
                Select Case mRefModelType
                    Case ReferenceModelType.openEHR_EHR, ReferenceModelType.HL7_CDA
                        Dim s(5) As StructureType
                        s(0) = StructureType.SECTION
                        s(1) = StructureType.ADMIN_ENTRY
                        s(2) = StructureType.OBSERVATION
                        s(3) = StructureType.EVALUATION
                        s(4) = StructureType.INSTRUCTION
                        s(5) = StructureType.ACTION
                        Return s
                    Case ReferenceModelType.CEN_EHR
                        Dim s(1) As StructureType
                        s(0) = StructureType.SECTION
                        s(1) = StructureType.ENTRY
                        Return s
                End Select
            Case StructureType.SECTION
                Select Case mRefModelType
                    Case ReferenceModelType.openEHR_EHR
                        Dim s(5) As StructureType
                        s(0) = StructureType.EVALUATION
                        s(1) = StructureType.INSTRUCTION
                        s(2) = StructureType.OBSERVATION
                        s(3) = StructureType.ACTION
                        s(4) = StructureType.SECTION
                        s(5) = StructureType.ADMIN_ENTRY
                        Return s
                    Case ReferenceModelType.CEN_EHR, ReferenceModelType.HL7_CDA
                        Dim s(1) As StructureType
                        s(0) = StructureType.ENTRY
                        s(1) = StructureType.SECTION
                        Return s
                    Case Else
                        Debug.Assert(False)
                End Select
            Case Else
                Select Case mStructureClass
                    Case StructureType.Single, StructureType.List
                        Dim s(0) As StructureType
                        s(0) = StructureType.Element
                        Return s
                    Case StructureType.Tree
                        Dim s(1) As StructureType
                        s(0) = StructureType.Element
                        s(1) = StructureType.Cluster
                        Return s
                    Case StructureType.Table
                        Debug.Assert(False)                      
                End Select
        End Select
        Return Nothing
    End Function
    Public Shared Function ValidArchetypeDefinitions() As StructureType()
        Select Case mRefModelType
            Case ReferenceModelType.openEHR_EHR
                Dim s(10) As StructureType
                s(0) = StructureType.OBSERVATION
                s(1) = StructureType.EVALUATION
                s(2) = StructureType.INSTRUCTION
                s(3) = StructureType.ACTION
                s(4) = StructureType.SECTION
                s(5) = StructureType.COMPOSITION
                s(6) = StructureType.Single
                s(7) = StructureType.List
                s(8) = StructureType.Tree
                s(9) = StructureType.Table
                s(10) = StructureType.ADMIN_ENTRY
                Return s
            Case ReferenceModelType.CEN_EHR, ReferenceModelType.HL7_CDA
                Dim s(2) As StructureType
                s(0) = StructureType.ENTRY
                s(1) = StructureType.SECTION
                s(2) = StructureType.COMPOSITION
                Return s
            Case ReferenceModelType.openEHR_Demographic
                Debug.Assert(False, "Not available")
                Dim s(0) As StructureType
                Return s
            Case Else
                Dim s(0) As StructureType
                Return s
        End Select
        Return Nothing
    End Function

    Public Shared Function ValidStateMachineTypes() As StateMachineType()
        Select Case mRefModelType
            Case ReferenceModelType.openEHR_EHR
                Dim s(7) As StateMachineType
                s(0) = StateMachineType.Initial
                s(1) = StateMachineType.Active
                s(2) = StateMachineType.Completed
                s(3) = StateMachineType.ActiveSuspended
                s(4) = StateMachineType.InitialSuspended
                s(5) = StateMachineType.ActiveAborted
                s(6) = StateMachineType.InitialAborted
                s(7) = StateMachineType.Scheduled
                Return s

            Case Else
                Debug.Assert(False, "No other reference model handled")
                Return Nothing
        End Select

    End Function

    Public Shared Function StructureTypeFromString(ByVal a_structure_name As String) As StructureType
        'Returns the structure type that matches the string
        'Ignoring case - could be a problem in some languages
        Try
            If mReferenceModelNames.ContainsValue(a_structure_name) Then
                For Each rfn As DictionaryEntry In mReferenceModelNames
                    If CStr(rfn.Value) = a_structure_name Then
                        Return CType(rfn.Key, StructureType)
                    End If
                Next
            Else
                Return CType([Enum].Parse(GetType(StructureType), a_structure_name, True), StructureType)
            End If

        Catch
            ' not a valid structure type - obsolete structure types are:
            Select Case a_structure_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "simple"
                    Return StructureType.Single
                Case Else
                    MessageBox.Show(AE_Constants.Instance.Error_loading & ": ITEM_STRUCTURE = " & a_structure_name & " -> ITEM_TREE", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return StructureType.Tree
            End Select
        End Try
    End Function

    Public Shared Sub Reset()
        mRefModelType = ReferenceModelType.Not_Set
        mArchetypedClass = StructureType.Not_Set
        mStructureClass = StructureType.Not_Set
    End Sub
End Class

'Class ReferenceModel
'    Inherits ReferenceModelLocal

'    ' ReferenceModel Singleton
'    Private Shared mInstance As ReferenceModel

'    Public Shared ReadOnly Property Instance() As ReferenceModel
'        Get
'            If mInstance Is Nothing Then
'                mInstance = New ReferenceModel
'            End If
'            Return mInstance
'        End Get
'    End Property

'End Class

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
'The Original Code is ReferenceModel.vb.
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

