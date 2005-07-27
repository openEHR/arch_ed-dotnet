'
'	component:   "openEHR Archetype Project"
'	description: "Constraints on ratios"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'

Option Strict On

Class Constraint_Ratio
    Inherits Constraint_with_value

    Private mNumerator As New Constraint_Count ' can be real if required
    Private mDenominator As New Constraint_Count ' can be real if required
    Private mIsIntegral As Boolean = True ' means that the numberator and denominators as counts not reals
    Private mIsPercent As Boolean  ' Denominator must be 100

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            'Return "Ratio"
            Return ConstraintType.Ratio
        End Get
    End Property
    Public Overrides Property AssumedValue() As Object
        Get
            'fixme
            If Me.HasAssumedValue Then
                'fixme
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            'fixme
            HasAssumedValue = True

        End Set
    End Property
    Public Property IsIntegral() As Boolean
        Get
            Return mIsIntegral
        End Get
        Set(ByVal Value As Boolean)
            If mIsIntegral <> Value Then
                If mIsIntegral Then
                    If mNumerator.Type = ConstraintType.Real Then
                        Dim num As New Constraint_Count
                        num.SetFromReal(CType(mNumerator, Constraint_Real))
                        mNumerator = num
                    End If
                    If mDenominator.Type = ConstraintType.Real Then
                        Dim num As New Constraint_Count
                        num.SetFromReal(CType(mDenominator, Constraint_Real))
                        mDenominator = num
                    End If
                Else
                    If mNumerator.Type = ConstraintType.Count Then
                        Dim num As New Constraint_Real
                        num.SetFromCount(mNumerator)
                        mNumerator = num
                    End If
                    If mDenominator.Type = ConstraintType.Count Then
                        Dim num As New Constraint_Real
                        num.SetFromCount(mDenominator)
                        mDenominator = num
                    End If
                End If
            End If
            mIsIntegral = Value
        End Set
    End Property
    Public Property Numerator() As Constraint_Count
        Get
            Return mNumerator
        End Get
        Set(ByVal Value As Constraint_Count)
            If ValidValue(Value) Then
                mNumerator = Value
            End If
        End Set
    End Property
    Public Property Denominator() As Constraint_Count
        Get
            Return mDenominator
        End Get
        Set(ByVal Value As Constraint_Count)
            If ValidValue(Value) Then
                mDenominator = Value
            End If
        End Set
    End Property

    Private Function ValidValue(ByVal value As Constraint_Count) As Boolean
        If IsPercent Then
            If value.MinimumValue <> 100 Or value.MaximumValue <> 100 Then
                Debug.Assert(False)
                'ToDo: throw error
                Return False
            End If
        ElseIf IsIntegral Then
            If value.Type <> ConstraintType.Count Then
                Debug.Assert(False)
                'ToDo: throw error
                Return False
            End If
        End If
        Return True
    End Function
    Public Overrides Function Copy() As Constraint
        Dim c As New Constraint_Ratio
        c.Numerator = CType(Me.Numerator.copy, Constraint_Count)
        c.Denominator = CType(Me.Denominator.copy, Constraint_Count)
        c.IsPercent = Me.IsPercent
        Return c
    End Function

    Public Property IsPercent() As Boolean
        Get
            Return mIsPercent
        End Get
        Set(ByVal Value As Boolean)
            mIsPercent = Value
            If mIsPercent Then
                mDenominator.MinimumValue = 100
                mDenominator.MaximumValue = 100
            End If
        End Set
    End Property

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
'The Original Code is RatioConstraint.vb.
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