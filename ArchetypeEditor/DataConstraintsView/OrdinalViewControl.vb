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

Public Class OrdinalViewControl : Inherits ElementViewControl

    Private WithEvents mComboBox As ComboBox

    Public Sub New(ByVal an_Element As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(an_Element, a_filemanager)
        If Not CType(an_Element.Constraint, Constraint_Ordinal).IsInitialised Then
            CType(an_Element.Constraint, Constraint_Ordinal).BeginLoading()
            For Each ov As OrdinalValue In CType(an_Element.Constraint, Constraint_Ordinal).OrdinalValues
                Dim aTerm As RmTerm = Filemanager.Master.OntologyManager.GetTerm(ov.InternalCode)

                ov.Text = aTerm.Text
            Next
            CType(an_Element.Constraint, Constraint_Ordinal).EndLoading()
        End If
    End Sub

    Public Sub New(ByVal a_Constraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(a_Constraint, a_filemanager)
        If Not CType(a_Constraint, Constraint_Ordinal).IsInitialised Then
            CType(a_Constraint, Constraint_Ordinal).BeginLoading()
            For Each ov As OrdinalValue In CType(a_Constraint, Constraint_Ordinal).OrdinalValues
                Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
                ov.Text = aTerm.Text
            Next
            CType(a_Constraint, Constraint_Ordinal).EndLoading()
        End If

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim ord As Constraint_Ordinal = CType(aConstraint, Constraint_Ordinal)

        mComboBox = New ComboBox
        mComboBox.Height = 25
        mComboBox.Width = 150
        mComboBox.DropDownWidth = 250
        mComboBox.Location = aLocation

        mComboBox.DisplayMember = "OrdinalAndText"

        Debug.Assert(Not ord.OrdinalValues Is Nothing)

        For Each value As OrdinalValue In ord.OrdinalValues
            'mComboBox.Items.Add(value.Ordinal.ToString & " - " & value.Text)
            mComboBox.Items.Add(value)

            If ord.HasAssumedValue Then
                Debug.Assert(TypeOf ord.AssumedValue Is Integer)

                If value.Ordinal = CInt(ord.AssumedValue) Then
                    mComboBox.Text = value.Text
                End If

            End If
        Next

        'mLoading = False
        Me.Controls.Add(mComboBox)

    End Sub

    ' Value = -1 => not selected
    Private mValue As Integer
    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            If (Value Is Nothing AndAlso mValue >= 0) _
                    OrElse (Not Value Is Nothing AndAlso mValue < 0) _
                    OrElse (Not Value Is Nothing AndAlso mValue <> CInt(Value)) Then

                If Not Value Is Nothing Then
                    mValue = CInt(Value)

                Else    ' not selected
                    mValue = -1
                End If

                MyBase.OnValueChanged()
            End If
        End Set
    End Property

    'Private mText As String

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mComboBox.SelectedIndexChanged

        If mComboBox.SelectedItem Is Nothing Then
            Tag = Nothing
            Value = Nothing
        Else
            'Value = CInt(mComboBox.SelectedValue)
            Tag = CType(mComboBox.SelectedItem, OrdinalValue).Ordinal
            Value = CType(mComboBox.SelectedItem, OrdinalValue).Ordinal
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
'The Original Code is OrdinalViewControl.vb.
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
