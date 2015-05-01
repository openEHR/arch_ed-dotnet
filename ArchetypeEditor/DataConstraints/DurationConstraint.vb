'
'	component:   "openEHR Archetype Project"
'	description: "Constriant on duration (time interval)"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Public Class Constraint_Duration
    Inherits Constraint_Count

    Private mMinMaxValueUnits As String

    Private mAllowableUnits As String = "PYMWDTHMS"
    ' Y = year, M = month, W = week, D = day, T as separator then H = hour
    ' M = minute S = second

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Duration
        result.mMinVal = mMinVal
        result.mMaxVal = mMaxVal
        result.mAssumedValue = mAssumedValue
        result.mHasMinVal = mHasMinVal
        result.mHasMaxVal = mHasMaxVal
        result.mIncludeMin = mIncludeMin
        result.mIncludeMax = mIncludeMax
        result.HasAssumedValue = HasAssumedValue
        result.mAllowableUnits = mAllowableUnits
        result.mMinMaxValueUnits = MinMaxValueUnits
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Duration
        End Get
    End Property

    Public Property MinMaxValueUnits() As String
        Get
            Return mMinMaxValueUnits
        End Get
        Set(ByVal Value As String)
            mMinMaxValueUnits = Value
        End Set
    End Property

    Public Property AllowableUnits() As String
        Get
            Return mAllowableUnits
        End Get
        Set(ByVal Value As String)
            mAllowableUnits = Value
        End Set
    End Property

    Public Function LimitsAsPattern() As String
        Dim result As String = ""

        If HasMinimum Then
            If Not IncludeMinimum Then
                result &= ">"
            End If

            result &= "P" & mMinMaxValueUnits & mMinVal.ToString()
        End If

        If HasMaximum Then
            If HasMinimum Then
                result &= ".."
            End If

            If Not IncludeMaximum Then
                result &= "<"
            End If

            result &= "P" & mMinMaxValueUnits
        End If

        Return result
    End Function

    Public Sub SetMinimumValueAndUnits(ByVal durationString As String)
        SetValueAndUnits(durationString, 1)
    End Sub

    Public Sub SetMaximumValueAndUnits(ByVal durationString As String)
        SetValueAndUnits(durationString, 2)
    End Sub

    Public Sub SetAssumedValueAndUnits(ByVal durationString As String)
        SetValueAndUnits(durationString, 3)
    End Sub

    Protected Sub SetValueAndUnits(ByVal durationString As String, ByVal which As Integer)
        Dim units As String = ""
        Dim valueString As String = ""

        For Each c As Char In durationString.ToUpperInvariant
            Select Case c
                Case "P"c
                    ' Remove the leading P
                Case "Y"c, "W"c, "D"c, "H"c, "M"c, "S"c, "T"c
                    units += c
                Case Else
                    valueString += c
            End Select
        Next

        Dim value As Long = Convert.ToInt64(Val(valueString))

        If which = 1 Then
            MinimumValue = value
        ElseIf which = 2 Then
            MaximumValue = value
        Else
            AssumedValue = value
        End If

        If String.IsNullOrEmpty(MinMaxValueUnits) Then
            MinMaxValueUnits = units
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
'The Original Code is DurationConstraint.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
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
