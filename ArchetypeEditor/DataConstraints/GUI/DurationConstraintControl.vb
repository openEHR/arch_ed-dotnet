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

Public Class DurationConstraintControl : Inherits ConstraintControl

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = a_file_manager

        ' Add time units for the appropriate language



    End Sub



    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents LabelDateTime As System.Windows.Forms.Label
    Friend WithEvents ImageListDateTime As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkYears As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonths As System.Windows.Forms.CheckBox
    Friend WithEvents chkWeeks As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkHours As System.Windows.Forms.CheckBox
    Friend WithEvents chkMinutes As System.Windows.Forms.CheckBox
    Friend WithEvents chkDays As System.Windows.Forms.CheckBox
    Friend WithEvents chkMilliseconds As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(DurationConstraintControl))
        Me.ImageListDateTime = New System.Windows.Forms.ImageList(Me.components)
        Me.LabelDateTime = New System.Windows.Forms.Label
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.chkYears = New System.Windows.Forms.CheckBox
        Me.chkMonths = New System.Windows.Forms.CheckBox
        Me.chkWeeks = New System.Windows.Forms.CheckBox
        Me.chkHours = New System.Windows.Forms.CheckBox
        Me.chkMinutes = New System.Windows.Forms.CheckBox
        Me.CheckBox6 = New System.Windows.Forms.CheckBox
        Me.chkDays = New System.Windows.Forms.CheckBox
        Me.chkMilliseconds = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'ImageListDateTime
        '
        Me.ImageListDateTime.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageListDateTime.ImageStream = CType(resources.GetObject("ImageListDateTime.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListDateTime.TransparentColor = System.Drawing.Color.Transparent
        '
        'LabelDateTime
        '
        Me.LabelDateTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelDateTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDateTime.Location = New System.Drawing.Point(8, 8)
        Me.LabelDateTime.Name = "LabelDateTime"
        Me.LabelDateTime.Size = New System.Drawing.Size(96, 24)
        Me.LabelDateTime.TabIndex = 36
        Me.LabelDateTime.Text = "Duration"
        '
        'chkAll
        '
        Me.chkAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.Location = New System.Drawing.Point(120, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.TabIndex = 37
        Me.chkAll.Text = "Allow all"
        '
        'chkYears
        '
        Me.chkYears.Location = New System.Drawing.Point(24, 56)
        Me.chkYears.Name = "chkYears"
        Me.chkYears.TabIndex = 38
        Me.chkYears.Text = "Years"
        '
        'chkMonths
        '
        Me.chkMonths.Location = New System.Drawing.Point(24, 80)
        Me.chkMonths.Name = "chkMonths"
        Me.chkMonths.TabIndex = 39
        Me.chkMonths.Text = "Months"
        '
        'chkWeeks
        '
        Me.chkWeeks.Location = New System.Drawing.Point(24, 104)
        Me.chkWeeks.Name = "chkWeeks"
        Me.chkWeeks.TabIndex = 40
        Me.chkWeeks.Text = "Weeks"
        '
        'chkHours
        '
        Me.chkHours.Location = New System.Drawing.Point(152, 56)
        Me.chkHours.Name = "chkHours"
        Me.chkHours.TabIndex = 41
        Me.chkHours.Text = "Hours"
        '
        'chkMinutes
        '
        Me.chkMinutes.Location = New System.Drawing.Point(152, 80)
        Me.chkMinutes.Name = "chkMinutes"
        Me.chkMinutes.TabIndex = 42
        Me.chkMinutes.Text = "Minutes"
        '
        'CheckBox6
        '
        Me.CheckBox6.Location = New System.Drawing.Point(152, 104)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.TabIndex = 43
        Me.CheckBox6.Text = "Seconds"
        '
        'chkDays
        '
        Me.chkDays.Location = New System.Drawing.Point(24, 128)
        Me.chkDays.Name = "chkDays"
        Me.chkDays.TabIndex = 44
        Me.chkDays.Text = "Days"
        '
        'chkMilliseconds
        '
        Me.chkMilliseconds.Location = New System.Drawing.Point(152, 128)
        Me.chkMilliseconds.Name = "chkMilliseconds"
        Me.chkMilliseconds.TabIndex = 45
        Me.chkMilliseconds.Text = "Milliseconds"
        '
        'DurationConstraintControl
        '
        Me.Controls.Add(Me.chkMilliseconds)
        Me.Controls.Add(Me.chkDays)
        Me.Controls.Add(Me.CheckBox6)
        Me.Controls.Add(Me.chkMinutes)
        Me.Controls.Add(Me.chkHours)
        Me.Controls.Add(Me.chkWeeks)
        Me.Controls.Add(Me.chkMonths)
        Me.Controls.Add(Me.chkYears)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.LabelDateTime)
        Me.Name = "DurationConstraintControl"
        Me.Size = New System.Drawing.Size(304, 168)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Shadows ReadOnly Property Constraint() As Constraint_Duration
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Duration)

            Return CType(MyBase.Constraint, Constraint_Duration)
        End Get
    End Property


    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        'MyBase.SetControlValues(IsState) 

        ' set constraint values on control

        
        'Me.TvDateTime.SelectedNode = FindNode(Me.TvDateTime.Nodes, _
        '            CStr(Me.Constraint.TypeofDateTimeConstraint), True)

        'Me.TvDateTime.SelectedNode.EnsureVisible()

    End Sub

    Private Sub TvDateTime_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        'Dim ae As iArchetypeElementNode

        ' HKF: 1620
        If MyBase.IsLoading Then Return
        'If MyBase.ArchetypeNode Is Nothing Then Return

        'Try
        '    ae = CType(currentitem, iArchetypeElementNode)
        'Catch
        '    Return
        'End Try

        ' CType(MyBase.Constraint, Constraint_DateTime).TypeofDateTimeConstraint _
        '        = CInt(Me.TvDateTime.SelectedNode.Tag)

        mFileManager.FileEdited = True

    End Sub

    Private Sub DurationConstraintControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
'The Original Code is DateTimeConstraintControl.vb.
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