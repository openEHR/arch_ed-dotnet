'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class MultipleViewControl : Inherits ElementViewControl

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)
    End Sub

    Public Sub New(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim tabctrl As New TabControl

        tabctrl.Height = 80
        tabctrl.Width = 620
        tabctrl.Location = aLocation

        'Dim rel_pos As New Point(5, 5)
        'rel_pos.X = 5
        'rel_pos.Y = 5

        Dim maxWidth As Integer
        For Each c As Constraint In CType(aConstraint, Constraint_Choice).Constraints
            Dim tp As New TabPage
            'Select Case c.Type
            '    Case ConstraintType.Quantity
            '        Dim qvc As New QuantityViewControl(c)
            '        If qvc.Width > max_width Then
            '            max_width = qvc.Width
            '        End If
            '        tp.Controls.Add(qvc)
            '    Case ConstraintType.Text
            '        Dim tvc As New TextViewControl(c)
            '        If tvc.Width > max_width Then
            '            max_width = tvc.Width
            '        End If
            '        tp.Controls.Add(tvc)
            '    Case Else
            '        tp.Controls.Add(DataTypeToControl(c, rel_pos))
            'End Select
            Dim viewControl As ElementViewControl = ArchetypeView.ConstraintView(c, mFileManager)
            'If viewControl.Width > max_width Then
            '    max_width = viewControl.Width
            'End If
            maxWidth = Math.Max(viewControl.Width, maxWidth)

            tp.Controls.Add(viewControl)

            tp.Text = c.ConstraintTypeString
            tabctrl.TabPages.Add(tp)
        Next
        tabctrl.Width = maxWidth

        Me.Controls.Add(tabctrl)

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
'The Original Code is MultipleViewControl.vb.
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
