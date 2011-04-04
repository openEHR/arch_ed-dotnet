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

Public Enum ConstraintKind
    Cluster = 0
    Any = 1
    Quantity = 2
    Text = 3
    [Boolean] = 4
    Ordinal = 5
    Count = 6
    DateTime = 7
    Multiple = 8
    MultiMedia = 9
    URI = 10
    Proportion = 11
    Duration = 12
    Interval_Quantity = 13
    Interval_Count = 14
    Interval_DateTime = 15
    Slot = 16
    Identifier = 17
    Currency = 18
    Parsable = 19
    QuantityUnit = 20
    Real = 21
End Enum

Public Class Constraint

    Public ReadOnly Property ConstraintKindString() As String
        Get
            Return Kind.ToString
        End Get
    End Property

    Public Overridable ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Any
        End Get
    End Property

    Public Overridable Function Copy() As Constraint
        Return New Constraint
    End Function

    Public Const SelectedImageOffset As Integer = 40

    Public Function ImageIndexForConstraintKind(ByVal isReference As Boolean, ByVal isSelected As Boolean) As Integer
        Dim result As Integer = CType(Kind, Integer)

        If isReference Then result += 20

        If isSelected Then result += SelectedImageOffset

        Return result
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