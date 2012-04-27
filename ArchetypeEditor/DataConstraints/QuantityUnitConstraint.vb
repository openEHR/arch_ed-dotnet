'
'	component:   "openEHR Archetype Project"
'	description: "Constraints on Quantity Units"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'

Option Strict On


Public Class Constraint_QuantityUnit : Inherits Constraint_Real

    ' allows a unit to have individual magnitude constraints
    Private mUnit As String
    Private mIsTime As Boolean

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_QuantityUnit(mIsTime)
        result.mHasMaxVal = mHasMaxVal
        result.mHasMinVal = mHasMinVal
        result.mMaxVal = mMaxVal
        result.mMinVal = mMinVal
        result.mAssumedValue = mAssumedValue
        result.HasAssumedValue = HasAssumedValue
        result.mUnit = mUnit
        result.mIsTime = mIsTime
        result.mPrecision = mPrecision
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.QuantityUnit
        End Get
    End Property

    Property Unit() As String
        Get
            Return mUnit
        End Get
        Set(ByVal Value As String)
            If mIsTime Then
                mUnit = Main.ISO_TimeUnits.GetOptimalIsoUnit(Value)
            Else
                mUnit = Value
            End If
        End Set
    End Property

    Property IsTime() As Boolean
        Get
            Return mIsTime
        End Get
        Set(ByVal Value As Boolean)
            mIsTime = Value
        End Set
    End Property

    ReadOnly Property isCompoundUnit() As Boolean
        Get
            Return mUnit.StartsWith("{")
        End Get
    End Property

    Overrides Function ToString() As String
        If mIsTime Then
            If Main.ISO_TimeUnits.IsValidIsoUnit(mUnit) Then
                Return Main.ISO_TimeUnits.GetLanguageForISO(mUnit)
            Else
                Return mUnit
            End If
        Else
            Return mUnit
        End If
    End Function

    Sub New(ByVal is_time_unit As Boolean)
        'Special handling for time units as need to be language dependent display
        mIsTime = is_time_unit
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
'The Original Code is QuantityUnitConstraint.vb.
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