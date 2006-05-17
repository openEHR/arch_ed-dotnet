'
'	component:   "openEHR Archetype Project"
'	description: "This Reference model structure models the idea of a clinical state based on a reference model state"
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
        MyBase.New(nodeID, StructureType.CarePathwayStep)
        mStateType = a_machine_state_type
    End Sub

    Sub New(ByVal a_node_id As String, ByVal EIF_PathwayStep As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(a_node_id, StructureType.CarePathwayStep)
        ProcessPathwayStep(EIF_PathwayStep)
    End Sub

#Region "ADL Orientated Classes"

    Sub ProcessPathwayStep(ByVal EIF_Step As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
        Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING

        For i As Integer = 1 To EIF_Step.attributes.count

            Dim coded_text As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT
            Dim t As Constraint_Text

            an_attribute = EIF_Step.attributes.i_th(i)

            coded_text = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)

            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "current_state"
                    t = RmElement.ProcessText(coded_text)
                    If t.AllowableValues.Codes.Count > 0 Then
                        mStateType = Integer.Parse(t.AllowableValues.Codes(0))
                    End If
                    If t.AllowableValues.Codes.Count > 1 Then
                        mAlternativeState = Integer.Parse(t.AllowableValues.Codes(1))
                    End If
                Case "careflow_step"
                    'No action now as atcode is set for RmPathwayStep and reproduced here
                Case Else
                    Debug.Assert(False, EIF_Step.rm_type_name.to_cil & " not handled")
            End Select
        Next

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
