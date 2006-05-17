'
'
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

Public Enum Orientation
    LEFT
    CENTER
    RIGHT
    TOP
    BOTTOM
End Enum

Public Class ColumnLayout : Inherits LayoutManager
    'Inherits BaseLayout

    Private Shared mDefaultGap As Integer = 2

    Private mGap As Integer
    Private mVerticalOrientation As Orientation
    Private mHorizontalOrientation As Orientation

    Public Sub New()
        Me.New(Orientation.CENTER, Orientation.CENTER, mDefaultGap)
    End Sub

    Public Sub New(ByVal gap As Integer)
        Me.New(Orientation.CENTER, Orientation.CENTER, gap)
    End Sub

    Public Sub New(ByVal verticalOrientation As Orientation, ByVal horizontalOrientation As Orientation)
        Me.New(verticalOrientation, horizontalOrientation, mDefaultGap)
    End Sub

    Public Sub New(ByVal verticalOrientation As Orientation, ByVal horizontalOrientation As Orientation, ByVal gap As Integer)
        Debug.Assert(gap >= 0)
        Debug.Assert(horizontalOrientation = Orientation.LEFT Or _
                    horizontalOrientation = Orientation.CENTER Or _
                    horizontalOrientation = Orientation.RIGHT)
        Debug.Assert(verticalOrientation = Orientation.TOP Or _
                    verticalOrientation = Orientation.CENTER Or _
                    verticalOrientation = Orientation.BOTTOM)

        mGap = gap
        mVerticalOrientation = verticalOrientation
        mHorizontalOrientation = horizontalOrientation
    End Sub

    Public Overrides Sub LayoutContainer(ByVal target As ViewPanel)
        Dim insets As insets = target.Insets
        Dim top As Integer = insets.Top
        Dim tps As Size = target.LayoutSize

        target.Size = tps

        Dim targetSize As Size = target.Size

        If mVerticalOrientation = Orientation.CENTER Then
            top = top + CInt((targetSize.Height / 2) - (tps.Height / 2))
        ElseIf mVerticalOrientation = Orientation.BOTTOM Then
            top = top + targetSize.Height - tps.Height + insets.Top
        End If

        Dim i As Integer
        'For i = 0 To target.Controls.Count - 1
        'comp = target.ContainerControl.Controls(i)
        For Each comp As Control In target.Controls
            Dim left As Integer = insets.Left

            If comp.Visible Then
                'If TypeOf comp Is IDynaControl Then
                '    ps = CType(comp, IDynaControl).PreferredSize
                'Else
                '    ps = comp.Size
                'End If

                If mHorizontalOrientation = Orientation.CENTER Then
                    left = CInt((targetSize.Width / 2) - (comp.Width / 2))
                ElseIf mHorizontalOrientation = Orientation.RIGHT Then
                    left = targetSize.Width - comp.Width - insets.Right
                End If

                'comp.Bounds = New Rectangle(left, top, ps.Width, ps.Height)
                comp.Location = New System.Drawing.Point(left, top)
                'comp.Size = New System.Drawing.Size(comp.Width, comp.Height)

                top = top + comp.Height + mGap

            End If
        Next
    End Sub

    'Public Overrides Function MinimumLayoutSize(ByVal target As IDynaContainer) As System.Drawing.Size
    '    Dim insets As insets = target.Insets
    '    Dim nComponents As Integer = target.ContainerControl.Controls.Count
    '    Dim comp As Control
    '    Dim d As Size
    '    Dim s As Size = New Size

    '    Dim i As Integer
    '    For i = 0 To nComponents - 1
    '        comp = target.ContainerControl.Controls(i)
    '        If comp.Visible Then
    '            If TypeOf comp Is IDynaControl Then
    '                d = CType(comp, IDynaControl).MinimumSize
    '            Else
    '                d = comp.Size
    '            End If
    '            If i > 0 Then
    '                s.Height = s.Height + mGap
    '            End If
    '            s.Height = s.Height + d.Height
    '            s.Width = Math.Max(d.Width, s.Width)
    '        End If
    '    Next
    '    s.Width = s.Width + insets.Left + insets.Right
    '    s.Height = s.Height + insets.Top + insets.Bottom
    '    Return s
    'End Function

    'Public Overrides Function PreferredLayoutSize(ByVal target As IDynaContainer) As System.Drawing.Size
    '    Dim insets As insets = target.Insets
    '    Dim nComponents As Integer = target.ContainerControl.Controls.Count
    '    Dim comp As Control
    '    Dim d As Size
    '    Dim s As Size = New Size

    '    Dim i As Integer
    '    For i = 0 To nComponents - 1
    '        comp = target.ContainerControl.Controls(i)
    '        If comp.Visible Then
    '            If TypeOf comp Is IDynaControl Then
    '                d = CType(comp, IDynaControl).PreferredSize
    '            Else
    '                d = comp.Size
    '            End If
    '            If i > 0 Then
    '                s.Height = s.Height + mGap
    '            End If
    '            s.Height = s.Height + d.Height
    '            s.Width = Math.Max(d.Width, s.Width)
    '        End If
    '    Next
    '    s.Width = s.Width + insets.Left + insets.Right
    '    s.Height = s.Height + insets.Top + insets.Bottom
    '    Return s
    'End Function

    Public Overrides Function LayoutSize(ByVal aTarget As ViewPanel) As System.Drawing.Size

        Dim s As Size = New Size
        For i As Integer = 0 To aTarget.Controls.Count - 1
            Dim comp As Control = aTarget.Controls(i)
            If comp.Visible Then
                'If TypeOf comp Is IDynaControl Then
                '    d = CType(comp, IDynaControl).PreferredSize
                'Else
                Dim d As Size = comp.Size
                'End If
                If i > 0 Then
                    s.Height = s.Height + mGap
                End If
                s.Height = s.Height + d.Height
                s.Width = Math.Max(d.Width, s.Width)
            End If
        Next

        Dim insets As insets = aTarget.Insets
        s.Width = s.Width + insets.Left + insets.Right
        s.Height = s.Height + insets.Top + insets.Bottom

        Return s
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
'The Original Code is ColumnLayout.vb.
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
