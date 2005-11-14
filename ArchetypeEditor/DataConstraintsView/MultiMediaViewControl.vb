'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Option Strict On

Public Class MultiMediaViewControl : Inherits ElementViewControl ' ViewControl

    Private mToolTip As ToolTip
    Private WithEvents mComboBox As ComboBox

    Public Sub New(ByVal anElement As ArchetypeElement) 'ByVal aConstraint As Constraint_Text)
        MyBase.New(anElement)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint)
        MyBase.New(aConstraint)

    End Sub

    'Protected Overrides Sub InitialiseComponent(ByVal anElement As ArchetypeElement, _
    '        ByVal aLocation As System.Drawing.Point)
    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim MultiMediaConstraint As Constraint_MultiMedia = CType(aConstraint, Constraint_MultiMedia)

        mComboBox = New ComboBox
        Dim lth As Integer = 150

        mComboBox.Location = aLocation 'pos
        mComboBox.Height = 25
        mComboBox.Width = 150

        If MultiMediaConstraint.AllowableValues.Codes.Count > 0 Then

            For Each s As String In MultiMediaConstraint.AllowableValues.Codes
                If s.Length > lth Then
                    lth = s.Length
                End If
                Dim term As New RmTerm(s)
                term.Text = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(Integer.Parse(s), "?")
                mComboBox.Items.Add(term)
            Next
            If lth > 250 Then
                lth = 250
            End If
            mComboBox.Width = lth

        End If

        Me.Controls.Add(mComboBox)

    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mComboBox.SelectedIndexChanged

        If mComboBox.SelectedValue Is Nothing Then
            Me.Tag = New CodePhrase(CType(mComboBox.Items(0), RmTerm)).Phrase
            Value = mComboBox.Text
        Else
            Me.Tag = New CodePhrase(CType(mComboBox.SelectedValue, RmTerm)).Phrase
            Value = CType(mComboBox.SelectedValue, RmTerm).Text
        End If
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