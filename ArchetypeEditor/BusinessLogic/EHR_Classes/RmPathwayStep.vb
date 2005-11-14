'
'	component:   "openEHR Archetype Project"
'	description: "This Reference model structure models the idea of a clinical state based on a reference model state"
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
Public Class RmPathwayStep
    Inherits RmStructure

    Private mStateType As StateMachineType = StateMachineType.Not_Set
    Private mAlternativeState As StateMachineType
    Private mAbortAllowed As Boolean
    Private mSuspendAllowed As Boolean


    Property StateType() As StateMachineType
        Get
            Return mStateType
        End Get
        Set(ByVal Value As StateMachineType)
            mStateType = Value
        End Set
    End Property
    ReadOnly Property HasAlternativeState() As Boolean
        Get
            Return mAlternativeState <> StateMachineType.Not_Set
        End Get
    End Property
    Property AlternativeState() As StateMachineType
        Get
            Return mAlternativeState
        End Get
        Set(ByVal Value As StateMachineType)
            mAlternativeState = Value
        End Set
    End Property
    Property AbortAllowed() As Boolean
        Get
            Return mAbortAllowed
        End Get
        Set(ByVal Value As Boolean)
            Debug.Assert(mStateType = StateMachineType.Active Or mStateType = StateMachineType.Initial)
            mAbortAllowed = Value
        End Set
    End Property
    Property SuspendAllowed() As Boolean
        Get
            Return mSuspendAllowed
        End Get
        Set(ByVal Value As Boolean)
            Debug.Assert(mStateType = StateMachineType.Active Or mStateType = StateMachineType.Initial)
            mSuspendAllowed = Value
        End Set
    End Property


    Sub New(ByVal nodeID As String, ByVal a_machine_state_type As StateMachineType)
        MyBase.New(nodeID, StructureType.WorkFlowStep)
        mStateType = a_machine_state_type
    End Sub

    Sub New(ByVal EIF_PathwayStep As openehr.am.C_COMPLEX_OBJECT)
        MyBase.New(EIF_PathwayStep.node_id.to_cil, StructureType.WorkFlowStep)
        ProcessPathwayStep(EIF_PathwayStep)
    End Sub

#Region "ADL Orientated Classes"

    Sub ProcessPathwayStep(ByVal EIF_Step As openehr.am.C_COMPLEX_OBJECT)
        Dim an_attribute As openehr.am.C_ATTRIBUTE
        Dim constraint As openehr.am.C_PRIMITIVE_OBJECT
        Dim cString As openehr.am.OE_C_STRING

        an_attribute = EIF_Step.attributes.first

        If an_attribute.children.count > 0 Then
            Dim y() As String

            constraint = an_attribute.children.first
            cString = constraint.item
            y = CType(cString.strings.i_th(1), openehr.Base_Net.STRING).to_cil.Split(",")

            Debug.Assert(y.Length < 3, "Have not dealt with multiple state possibilities > 2")

            Me.mStateType = Int(y(0))

            If y.Length > 1 Then
                Me.AlternativeState = Int(y(1))
                ' sets the HasAlternative State to True
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
'The Original Code is RmMachineSlot.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
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
