'	component:   "openEHR Archetype Project"
'	description: "Constraint for Boolean values"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Public Class Constraint_Boolean
    Inherits Constraint

    Private boolAssumed As Boolean
    Private boolHasAssumed As Boolean
    Private iBoolVal As Integer ' 0 = TrueOrFalse (default), 1 = True, 2 = False

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Boolean
        result.boolAssumed = boolAssumed
        result.boolHasAssumed = boolHasAssumed
        result.iBoolVal = iBoolVal
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Boolean
        End Get
    End Property

    Public Property AssumedValue() As Boolean
        Get
            If boolHasAssumed Then
                Return boolAssumed
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Boolean)
            If Value And iBoolVal < 2 Then
                boolAssumed = Value
                boolHasAssumed = True
                Return
            Else
                If iBoolVal = 0 Or iBoolVal = 2 Then
                    boolAssumed = Value
                    boolHasAssumed = True
                    Return
                End If
            End If
            Debug.Assert(False)
        End Set
    End Property

    Public ReadOnly Property TrueAllowed() As Boolean
        Get
            Return iBoolVal = 1
        End Get
    End Property

    Public ReadOnly Property FalseAllowed() As Boolean
        Get
            Return iBoolVal = 2
        End Get
    End Property

    Public ReadOnly Property TrueFalseAllowed() As Boolean
        Get
            Return iBoolVal = 0
        End Get
    End Property

    Public Sub AllowTrueOnly()
        iBoolVal = 1

        ' cannot have an assumed value of false
        boolHasAssumed = boolHasAssumed And boolAssumed
    End Sub

    Public Sub AllowFalseOnly()
        iBoolVal = 2

        ' cannot have an assumed value of true
        boolHasAssumed = boolHasAssumed And Not boolAssumed
    End Sub

    Public Sub AllowTrueOrFalse()
        iBoolVal = 0
    End Sub

    Public Property HasAssumedValue() As Boolean
        Get
            Return boolHasAssumed
        End Get
        Set(ByVal Value As Boolean)
            boolHasAssumed = Value
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
'The Original Code is BooleanConstraint.vb.
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