'
'	component:   "openEHR Archetype Project"
'	description: "Constriant on duration (time interval)"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'

Option Strict On

Public Class Constraint_Duration
    Inherits Constraint_with_value

    Private mValue As TimeSpan
    Public Overrides Property AssumedValue() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            mValue = CType(Value, TimeSpan)
        End Set
    End Property

    Private mAllowableUnits As ArrayList
    '99 = all, 0 = year, 1 = month, 2 = week, 3 = day, 4 = minute, 5 = second, 6 = millisecond
    Public Property AllowableUnits() As ArrayList
        Get
            Return mAllowableUnits
        End Get
        Set(ByVal Value As ArrayList)
            mAllowableUnits = Value
        End Set
    End Property


    Private mHasMaxValue As Boolean

    Public Property HasMaximumValue() As Boolean
        Get
            Return mHasMaxValue
        End Get
        Set(ByVal Value As Boolean)
            mHasMaxValue = Value
        End Set
    End Property

    Private mMaxVal As TimeSpan

    Public Property MaximumValue() As TimeSpan
        Get
            Return mMaxVal
        End Get
        Set(ByVal Value As TimeSpan)
            If (Value.Ticks <= mValue.MaxValue.Ticks) Then
                mMaxVal = Value
                mHasMaxValue = True
            End If
        End Set
    End Property


    Private mHasMinValue As Boolean

    Public Property HasMinimumValue() As Boolean
        Get
            Return mHasMinValue
        End Get
        Set(ByVal Value As Boolean)
            mHasMinValue = Value
        End Set
    End Property

    Private mMinVal As TimeSpan

    Public Property MinimumValue() As TimeSpan
        Get
            Return mMinVal
        End Get
        Set(ByVal Value As TimeSpan)
            If (Value.Ticks >= mValue.MinValue.Ticks) Then
                mMinVal = Value
                mHasMinValue = True
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