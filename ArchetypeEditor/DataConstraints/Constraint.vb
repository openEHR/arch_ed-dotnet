'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
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

Public Enum ConstraintType
    Nil = 0
    Any = 1
    [Boolean] = 2
    Quantity = 3
    QuantityUnit = 4
    Count = 5
    Text = 6
    DateTime = 7
    Ordinal = 8
    Multiple = 9
    Proportion = 10
    Slot = 11
    Interval_Count = 12
    Interval_Quantity = 13
    Interval_DateTime = 14
    MultiMedia = 15
    URI = 16
    Duration = 17
    Real = 18
    Identifier = 19
    Currency = 20
    Parsable = 21
End Enum


Public Class Constraint

    Public ReadOnly Property ConstraintTypeString() As String
        Get
            Return Me.Type.ToString
        End Get
    End Property
    Public Overridable ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Any
        End Get
    End Property

    'Returns a constraint which is ANY
    Public Overridable Function copy() As Constraint
        Return New Constraint
    End Function
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
'The Original Code is Constraint.vb.
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