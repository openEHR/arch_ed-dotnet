'
'	component:   "openEHR Archetype Project"
'	description: "Container class for choice constraint"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Public Class ConstraintCollection
    Inherits CollectionBase

    Public Sub Add(ByVal Value As Constraint)
        List.Add(Value)
    End Sub

    Public Function Copy() As ConstraintCollection
        Dim result As New ConstraintCollection

        For i As Integer = 0 To List.Count - 1
            result.Add(CType(List.Item(i), Constraint))
        Next

        Return result
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal Value As Constraint)
        List.Insert(index, Value)
    End Sub

    Public Sub Move(ByVal value As Constraint, ByVal index As Integer)
        List.Remove(value)
        List.Insert(index, value)
    End Sub

    Public Property Item(ByVal key As Integer) As Constraint
        Get
            Item = CType(List.Item(key), Constraint)
        End Get
        Set(ByVal Value As Constraint)
            List.Item(key) = Value
        End Set
    End Property

    Public Sub Remove(ByVal c As Constraint)
        List.Remove(c)
    End Sub

    Sub New()
        MyBase.New()
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
'The Original Code is ConstraintCollection.vb.
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