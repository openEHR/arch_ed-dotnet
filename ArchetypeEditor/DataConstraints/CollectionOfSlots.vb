'
'	component:   "openEHR Archetype Project"
'	description: "Collection of Slot constraints"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Public Class CollectionOfSlots
    Inherits CollectionBase

    Public Function Contains(ByVal value As String) As Boolean
        Return MyBase.InnerList.Contains(value)
    End Function

    Public Sub Add(ByVal Value As String)
        Me.List.Add(Value)
    End Sub

    Public Function IndexOf(ByVal value As String) As Integer
        Return MyBase.InnerList.IndexOf(value)
    End Function

    Public Function Copy() As CollectionOfSlots
        Dim co As New CollectionOfSlots

        For i As Integer = 0 To Me.List.Count - 1
            co.Add(CStr(Me.List.Item(i)))
        Next
        Return co
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal Value As String)
        Me.List.Insert(index, Value)
    End Sub

    Public Sub Move(ByVal value As String, ByVal index As Integer)
        Me.List.Remove(value)
        Me.List.Insert(index, value)
    End Sub
    Public ReadOnly Property Items() As String()
        Get
            Dim s(Me.List.Count - 1) As String
            For i As Integer = 0 To Me.List.Count - 1
                s(i) = CStr(Me.List.Item(i))
            Next
            Return s
        End Get
    End Property
    Public Property Item(ByVal key As Integer) As String
        Get
            Return CStr(Me.List.Item(key))
        End Get
        Set(ByVal Value As String)
            Me.List.Item(key) = Value
        End Set
    End Property

    Public Sub Remove(ByVal a_slot_constraint As String)
        If Me.List.Contains(a_slot_constraint) Then
            Me.List.Remove(a_slot_constraint)
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
'The Original Code is CollectionOfSlots.vb.
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