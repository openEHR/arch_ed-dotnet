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

Public Class ViewPanel : Inherits Panel

    Public Sub New(ByVal aLayoutManager As ColumnLayout)
        Me.New()
        mLayoutManager = aLayoutManager
    End Sub

    Public Sub New()
        MyBase.New()
        MyBase.Size = New Size(0, 0)

#If Not PaintBoundary Then
        mInsets = New Insets(0, 0, 0, 0)
#Else
        mInsets = New Insets(1, 1, 1, 1)
#End If
    End Sub

    Private mLayoutManager As ColumnLayout

    Public Property LayoutManager() As ColumnLayout
        Get
            Return mLayoutManager
        End Get
        Set(ByVal Value As ColumnLayout)
            mLayoutManager = Value
        End Set
    End Property

    Private mInsets As Insets

    Public ReadOnly Property Insets() As Insets
        Get
            Return mInsets
        End Get
    End Property

    Public Function LayoutSize() As System.Drawing.Size
        If mLayoutManager Is Nothing Then
            Return Size
        Else
            Return mLayoutManager.LayoutSize(Me)
        End If
    End Function

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

#If PaintBoundary Then
        e.Graphics.DrawRectangle(New Pen(Color.Red), 0, 0, Size.Width - 1, Size.Height - 1)
#End If
    End Sub

    Protected Overrides Sub OnLayout(ByVal e As System.Windows.Forms.LayoutEventArgs)
        MyBase.OnLayout(e)

        If Not mLayoutManager Is Nothing Then
            mLayoutManager.LayoutContainer(Me)
        End If
    End Sub

End Class

Public Structure Insets
    Public Top As Integer
    Public Left As Integer
    Public Right As Integer
    Public Bottom As Integer

    Public Sub New(ByVal left As Integer, ByVal right As Integer, ByVal top As Integer, ByVal bottom As Integer)
        Me.Top = top
        Me.Left = left
        Me.Right = right
        Me.Bottom = bottom
    End Sub
End Structure


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
'The Original Code is ViewPanel.vb.
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
