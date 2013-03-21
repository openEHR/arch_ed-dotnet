'
'	component:   "openEHR Archetype Project"
'	description: "Constraints on intervals of datetime (one date to the next date), not to be confused with duration"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Class Constraint_Interval_DateTime
    Inherits Constraint_Interval

    Private mDateTimeUpper As New Constraint_DateTime
    Private mDateTimeLower As New Constraint_DateTime

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Interval_DateTime
        result.mDateTimeLower = CType(mDateTimeLower.Copy, Constraint_DateTime)
        result.mDateTimeUpper = CType(mDateTimeUpper.Copy, Constraint_DateTime)
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Interval_DateTime
        End Get
    End Property

    Overrides Property UpperLimit() As Constraint
        Get
            Return mDateTimeUpper
        End Get
        Set(ByVal Value As Constraint)
            Debug.Assert(TypeOf Value Is Constraint_DateTime)
            mDateTimeUpper = CType(Value, Constraint_DateTime)
        End Set
    End Property

    Overrides Property LowerLimit() As Constraint
        Get
            Return mDateTimeLower
        End Get
        Set(ByVal Value As Constraint)
            Debug.Assert(TypeOf Value Is Constraint_DateTime)
            mDateTimeLower = CType(Value, Constraint_DateTime)
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
'The Original Code is IntervalOfDateTimeConstraint.vb.
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