'
'	component:   "openEHR Archetype Project"
'	description: "Constraints on ratios"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Enum ProportionType
    Ratio = 0
    Unitary = 1
    Percent = 2
    Fraction = 3
    IntegerFraction = 4
End Enum

Public Class Constraint_Proportion
    Inherits Constraint_with_value

    Private mNumerator As New Constraint_Real ' can be real if required
    Private mDenominator As New Constraint_Real ' can be real if required
    Private mIsIntegral As Boolean = False ' means that the numberator and denominators as reals by default
    Private mAllowedTypes As Boolean() = {True, True, True, True, True}
    Private mIsIntegralSet As Boolean = False

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Proportion
        result.mNumerator = CType(mNumerator.Copy, Constraint_Real)
        result.mDenominator = CType(mDenominator.Copy, Constraint_Real)
        result.mIsIntegral = mIsIntegral
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Proportion
        End Get
    End Property

    Public Overrides Property AssumedValue() As Object
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Object)
        End Set
    End Property

    Public Property IsIntegralSet() As Boolean
        Get
            Return mIsIntegralSet
        End Get
        Set(ByVal value As Boolean)
            mIsIntegralSet = value
        End Set
    End Property

    Public Property IsIntegral() As Boolean
        Get
            Return mIsIntegral
        End Get
        Set(ByVal Value As Boolean)
            mIsIntegral = Value
        End Set
    End Property

    Public Property Numerator() As Constraint_Real
        Get
            Return mNumerator
        End Get
        Set(ByVal Value As Constraint_Real)
            If ValidValue(Value) Then
                mNumerator = Value
            End If
        End Set
    End Property

    Public Property Denominator() As Constraint_Real
        Get
            Return mDenominator
        End Get
        Set(ByVal Value As Constraint_Real)
            If ValidValue(Value) Then
                mDenominator = Value
            End If
        End Set
    End Property

    Private Function ValidValue(ByVal value As Constraint_Count) As Boolean
        If IsIntegral Then
            If (Convert.ToInt32(value.MaximumValue) - value.MaximumValue <> 0) Or (Convert.ToInt32(value.MinimumValue) - value.MinimumValue <> 0) Then
                Return False
            End If
        End If

        Return True
    End Function

    Public ReadOnly Property HasDenominator() As Boolean
        Get
            Return mAllowedTypes(0) Or mAllowedTypes(3) Or mAllowedTypes(4)
        End Get
    End Property

    Public ReadOnly Property IsPercent() As Boolean
        Get
            Return Not mAllowedTypes(0) And Not mAllowedTypes(1) And mAllowedTypes(2) And Not mAllowedTypes(3) And Not mAllowedTypes(4)
        End Get
    End Property

    Public ReadOnly Property IsUnitary() As Boolean
        Get
            Return Not mAllowedTypes(0) And mAllowedTypes(1) And Not mAllowedTypes(2) And Not mAllowedTypes(3) And Not mAllowedTypes(4)
        End Get
    End Property

    Public Function IsTypeAllowed(ByVal proportionType As Integer) As Boolean
        Return proportionType >= mAllowedTypes.GetLowerBound(0) AndAlso proportionType <= mAllowedTypes.GetUpperBound(0) AndAlso mAllowedTypes(proportionType)
    End Function

    Public ReadOnly Property AllowsAllTypes() As Boolean
        Get
            Return mAllowedTypes(0) And mAllowedTypes(1) And mAllowedTypes(2) And mAllowedTypes(3) And mAllowedTypes(4)
        End Get
    End Property

    Public Sub AllowAllTypes()
        For i As Integer = 0 To 4
            mAllowedTypes(i) = True
        Next
    End Sub

    Public Sub DisallowAllTypes()
        For i As Integer = 0 To 4
            mAllowedTypes(i) = False
        Next
    End Sub

    Public Sub AllowType(ByVal proportionType As Integer)
        If proportionType >= mAllowedTypes.GetLowerBound(0) And proportionType <= mAllowedTypes.GetUpperBound(0) Then
            mAllowedTypes(proportionType) = True
        Else
            Debug.Assert(False)
        End If
    End Sub

    Public Sub DisallowType(ByVal proportionType As Integer)
        If proportionType >= mAllowedTypes.GetLowerBound(0) And proportionType <= mAllowedTypes.GetUpperBound(0) Then
            mAllowedTypes(proportionType) = False
        Else
            Debug.Assert(False)
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