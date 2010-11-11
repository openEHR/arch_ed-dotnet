'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on integers"
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

Public Class Constraint_Count
    Inherits Constraint_with_value

    Protected mMinVal As Double = 0
    Protected mMaxVal As Double = 0
    Protected mAssumedValue As Double
    Protected mHasMaxVal As Boolean
    Protected mHasMinVal As Boolean
    Protected mIncludeMax As Boolean = True
    Protected mIncludeMin As Boolean = True
    Protected mHasList As Boolean
    Protected mList As String

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Count
        End Get
    End Property

    Public Overrides Property AssumedValue() As Object
        Get
            If HasAssumedValue Then
                Return CLng(mAssumedValue)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            Try
                mAssumedValue = CLng(Value)
            Catch ex As Exception
                Debug.Assert(False, Value.ToString & "is not valid value for this type")
            End Try
        End Set
    End Property

    Public Property HasMinimum() As Boolean
        Get
            Return mHasMinVal
        End Get
        Set(ByVal Value As Boolean)
            mHasMinVal = Value
        End Set
    End Property

    Public Property HasMaximum() As Boolean
        Get
            Return mHasMaxVal
        End Get
        Set(ByVal Value As Boolean)
            mHasMaxVal = Value
        End Set
    End Property
    Public Property HasList() As Boolean
        Get
            Return mHasList
        End Get
        Set(ByVal Value As Boolean)
            mHasList = Value
        End Set
    End Property

    Public Property MinimumValue() As Long
        Get
            Return CLng(mMinVal)
        End Get
        Set(ByVal Value As Long)
            mMinVal = CSng(Value)
        End Set
    End Property

    Public Property MaximumValue() As Long
        Get
            Return CLng(mMaxVal)
        End Get
        Set(ByVal Value As Long)
            mMaxVal = CSng(Value)
        End Set
    End Property

    Public Property IncludeMaximum() As Boolean
        Get
            Return mIncludeMax
        End Get
        Set(ByVal Value As Boolean)
            mIncludeMax = Value
        End Set
    End Property

    Public Property IncludeMinimum() As Boolean
        Get
            Return mIncludeMin
        End Get
        Set(ByVal Value As Boolean)
            mIncludeMin = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the comma separated value list.
    ''' </summary>
    ''' <value>The value list as a comma separated string</value>
    Public Property ValueList() As String
        Get
            Return mList
        End Get
        Set(ByVal Value As String)
            mList = Value
        End Set
    End Property

    Public Overrides Function Copy() As Constraint
        Dim c As New Constraint_Count
        c.mHasMaxVal = mHasMaxVal
        c.mHasMinVal = mHasMinVal
        c.mMaxVal = mMaxVal
        c.mMinVal = mMinVal
        c.mAssumedValue = mAssumedValue
        c.HasAssumedValue = HasAssumedValue
        Return c
    End Function

    Public Sub SetFromReal(ByVal r As Constraint_Real)
        HasAssumedValue = r.HasAssumedValue

        If r.HasAssumedValue Then
            AssumedValue = r.AssumedValue
        End If

        HasMaximum = r.HasMaximum

        If r.HasMaximum Then
            MaximumValue = r.MaximumValue
        End If

        HasMinimum = r.HasMinimum

        If r.HasMinimum Then
            MinimumValue = r.MinimumValue
        End If

        IncludeMaximum = r.IncludeMaximum
        IncludeMinimum = r.IncludeMinimum
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
'The Original Code is CountConstraint.vb.
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