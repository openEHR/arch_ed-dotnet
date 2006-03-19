'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class BooleanViewControl : Inherits ElementViewControl

    Private WithEvents mListBox As ListBox

    Private mLoading As Boolean

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)
    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim booleanConstraint As Constraint_Boolean = CType(aConstraint, Constraint_Boolean)
        mLoading = True
        mListBox = New ListBox

        mListBox.Items.Add(System.Boolean.TrueString)
        mListBox.Items.Add(System.Boolean.FalseString)
        mListBox.Location = aLocation

        If booleanConstraint.hasAssumedValue Then
            If booleanConstraint.AssumedValue = True Then
                mListBox.SelectedIndex = 0
            Else
                mListBox.SelectedIndex = 1
            End If
        End If

        mListBox.Height = 50
        mListBox.Width = 75

        Me.Controls.Add(mListBox)

        mLoading = False

    End Sub

    Private mValue As Boolean
    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            mValue = CBool(Value)
            Tag = mValue
            MyBase.OnValueChanged()
        End Set
    End Property

    Private Sub mListBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles mListBox.SelectedIndexChanged

        If mLoading Then Exit Sub

        If mListBox.SelectedValue Is Nothing Then
            Value = CStr(mListBox.Text)
        Else
            Value = CStr(mListBox.SelectedValue)
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
'The Original Code is BooleanViewControl.vb.
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