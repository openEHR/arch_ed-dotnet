'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class TextViewControl : Inherits ElementViewControl ' ViewControl

    Private mToolTip As ToolTip
    Private WithEvents mComboBox As ComboBox
    Private WithEvents mTextBox As TextBox

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)
    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, ByVal aLocation As System.Drawing.Point)
        Dim textConstraint As Constraint_Text = CType(aConstraint, Constraint_Text)

        Select Case textConstraint.TypeOfTextConstraint
            Case TextConstraintType.Text
                If textConstraint.AllowableValues.Codes.Count > 0 Then
                    mComboBox = New ComboBox
                    Dim lth As Integer = 150
                    mComboBox.Location = aLocation
                    mComboBox.Height = 25
                    mComboBox.Width = 150

                    For Each s As String In textConstraint.AllowableValues.Codes
                        If s.Length > lth Then
                            lth = s.Length
                        End If

                        mComboBox.Items.Add(s)
                    Next

                    If lth > 250 Then
                        lth = 250
                    End If

                    mComboBox.Width = lth

                    If textConstraint.HasAssumedValue Then
                        mComboBox.Text = CStr(textConstraint.AssumedValue)
                    End If

                    Controls.Add(mComboBox)
                Else
                    mTextBox = New TextBox
                    mTextBox.Location = aLocation
                    mTextBox.Height = 25
                    mTextBox.Width = 200
                    mTextBox.Text = Filemanager.GetOpenEhrTerm(441, "Free text")
                    Controls.Add(mTextBox)
                End If

            Case TextConstraintType.Internal
                If textConstraint.AllowableValues.Codes.Count > 0 Then
                    mComboBox = New ComboBox
                    Dim lth As Integer = 150
                    Dim term As RmTerm

                    mComboBox.Location = aLocation
                    mComboBox.Height = 25
                    mComboBox.Width = 150

                    For Each s As String In textConstraint.AllowableValues.Codes
                        term = Filemanager.Master.OntologyManager.GetTerm(s)

                        If Not term.Text Is Nothing Then
                            If term.Text.Length > lth Then
                                lth = term.Text.Length
                            End If

                            mComboBox.Items.Add(term)
                        End If
                    Next

                    If lth > 250 Then
                        lth = 250
                    End If

                    mComboBox.Width = lth

                    If textConstraint.HasAssumedValue Then
                        term = Filemanager.Master.OntologyManager.GetTerm(CStr(textConstraint.AssumedValue))
                        mComboBox.Text = term.Text
                    End If

                    Controls.Add(mComboBox)

                Else
                    mTextBox = New TextBox
                    mTextBox.Location = aLocation
                    mTextBox.Height = 30
                    mTextBox.Width = 300
                    mTextBox.Text = "No values set"
                    Controls.Add(mTextBox)
                End If

            Case TextConstraintType.Terminology
                Dim lbl As New Label
                lbl.Location = aLocation
                lbl.Height = 40
                lbl.Width = 250
                lbl.BorderStyle = BorderStyle.Fixed3D

                Dim term As RmTerm
                term = mFileManager.OntologyManager.GetTerm(textConstraint.ConstraintCode)
                lbl.Text = term.Text
                Controls.Add(lbl)

                Dim but As New Button
                but.Text = "..."
                aLocation.X += 260
                but.BackColor = System.Drawing.Color.LightGray

                mToolTip = New ToolTip
                mToolTip.SetToolTip(but, term.Description)

                but.Width = 30
                but.Height = 20
                but.Location = aLocation
                Controls.Add(but)

            Case Else
                Debug.Assert(False)
        End Select
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mComboBox.SelectedIndexChanged
        Try
            If TypeOf mComboBox.Items(0) Is RmTerm Then
                If mComboBox.SelectedValue Is Nothing Then
                    Tag = New CodePhrase(CType(mComboBox.Items(0), RmTerm)).Phrase
                    Value = mComboBox.Text
                Else
                    Tag = New CodePhrase(CType(mComboBox.SelectedValue, RmTerm)).Phrase
                    Value = CType(mComboBox.SelectedValue, RmTerm).Text
                End If
            Else  ' text string
                Tag = mComboBox.Text
                Value = mComboBox.Text
            End If
        Catch ex As Exception
            Debug.Assert(False, ex.Message)
        End Try
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mTextBox.TextChanged
        Tag = mTextBox.Text
        Value = mTextBox.Text
    End Sub

    Private mValue As String

    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            mValue = CStr(Value)
            MyBase.OnValueChanged()
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
'The Original Code is TextViewControl.vb.
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
