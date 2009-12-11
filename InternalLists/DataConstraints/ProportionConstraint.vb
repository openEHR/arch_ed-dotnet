'
'	component:   "openEHR Archetype Project"
'	description: "Constraints on ratios"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraints/RatioConstraint.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2005-07-27 21:03:30 +0930 (Wed, 27 Jul 2005) $"
'

Option Strict On

Enum ProportionType
    Ratio = 0
    Unitary = 1
    Percent = 2
    Fraction = 3
    IntegerFraction = 4
End Enum

Class Constraint_Proportion
    Inherits Constraint_with_value

    Private mNumerator As New Constraint_Real ' can be real if required
    Private mDenominator As New Constraint_Real ' can be real if required
    Private mIsIntegral As Boolean = False ' means that the numberator and denominators as reals by default
    Private mAllowedTypes As Boolean() = {True, True, True, True, True}
    Private mIsIntegralSet As Boolean = False

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            'Return "Proportion"
            Return ConstraintType.Proportion
        End Get
    End Property
    Public Overrides Property AssumedValue() As Object
        Get
            'fixme
            If Me.HasAssumedValue Then
                'ToDo: fix this
                Debug.Assert(False)
                Return Nothing
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            'fixme
            HasAssumedValue = True
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
    Friend Sub SetAllTypesDisallowed()
        For i As Integer = 0 To 4
            mAllowedTypes(i) = False
        Next
    End Sub
    Friend Sub SetAllTypesAllowed()
        For i As Integer = 0 To 4
            mAllowedTypes(i) = True
        Next
    End Sub
    Private Function ValidValue(ByVal value As Constraint_Count) As Boolean
       If IsIntegral Then
            If (Convert.ToInt32(value.MaximumValue) - value.MaximumValue <> 0) Or (Convert.ToInt32(value.MinimumValue) - value.MinimumValue <> 0) Then
                Debug.Assert(False)
                'ToDo: throw error
                Return False
            End If
        End If
        Return True
    End Function
    Public Overrides Function Copy() As Constraint
        Dim c As New Constraint_Proportion
        c.Numerator = CType(Me.Numerator.Copy, Constraint_Real)
        c.Denominator = CType(Me.Denominator.Copy, Constraint_Real)
        c.mIsIntegral = mIsIntegral
        Return c
    End Function

    Public Property IsPercent() As Boolean
        Get
            Return (mAllowedTypes(0) = False And mAllowedTypes(1) = False And mAllowedTypes(2) = True And mAllowedTypes(3) = False And mAllowedTypes(4) = False)
        End Get
        Set(ByVal value As Boolean)
            If value Then
                mDenominator.MinimumRealValue = 100
                mDenominator.MaximumRealValue = 100
            Else
                mDenominator.HasMinimum = True
                mDenominator.MinimumRealValue = 0
                mDenominator.IncludeMinimum = False
                mDenominator.HasMaximum = False
            End If
        End Set
    End Property

    Public Property IsUnitary() As Boolean
        Get
            Return (mAllowedTypes(0) = False And mAllowedTypes(1) = True And mAllowedTypes(2) = False And mAllowedTypes(3) = False And mAllowedTypes(4) = False)
        End Get
        Set(ByVal value As Boolean)
            If value Then
                mDenominator.MinimumRealValue = 1
                mDenominator.MaximumRealValue = 1
            Else
                mDenominator.HasMinimum = True
                mDenominator.MinimumRealValue = 0
                mDenominator.IncludeMinimum = False
                mDenominator.HasMaximum = False
            End If
        End Set
    End Property

    'Returns true if any type is allowed
    Public Property AllowAllTypes() As Boolean
        Get
            Return (mAllowedTypes(0) And mAllowedTypes(1) And mAllowedTypes(2) And mAllowedTypes(3) And mAllowedTypes(4))
        End Get
        Set(ByVal value As Boolean)
            If value Then
                For i As Integer = 0 To 4
                    mAllowedTypes(i) = True
                Next
            End If
        End Set
    End Property

    Public Sub AllowType(ByVal ProportionType As Integer)
        If ProportionType >= mAllowedTypes.GetLowerBound(0) And ProportionType <= mAllowedTypes.GetUpperBound(0) Then
            mAllowedTypes(ProportionType) = True
        Else
            Debug.Assert(False)
        End If
    End Sub

    Public Function IsTypeAllowed(ByVal proportionType As Integer) As Boolean
        If proportionType >= mAllowedTypes.GetLowerBound(0) And proportionType <= mAllowedTypes.GetUpperBound(0) Then
            Return mAllowedTypes(proportionType)
        Else
            Debug.Assert(False)
            Return False
        End If
    End Function

    Public Sub DisAllowType(ByVal ProportionType As Integer)
        If ProportionType >= mAllowedTypes.GetLowerBound(0) And ProportionType <= mAllowedTypes.GetUpperBound(0) Then
            mAllowedTypes(ProportionType) = False
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