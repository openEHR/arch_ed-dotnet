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


    End Sub



    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private components As System.ComponentModel.IContainer
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkYears As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonths As System.Windows.Forms.CheckBox
    Friend WithEvents chkWeeks As System.Windows.Forms.CheckBox
    Friend WithEvents chkHours As System.Windows.Forms.CheckBox
    Friend WithEvents chkMinutes As System.Windows.Forms.CheckBox
    Friend WithEvents chkDays As System.Windows.Forms.CheckBox
    Friend WithEvents chkMilliseconds As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeconds As System.Windows.Forms.CheckBox
    Friend WithEvents panelTop As System.Windows.Forms.Panel
    Friend WithEvents panelDetail As System.Windows.Forms.Panel
    Friend WithEvents LabelDuration As System.Windows.Forms.Label
    Friend WithEvents comboTimeUnits As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelDuration = New System.Windows.Forms.Label
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.chkYears = New System.Windows.Forms.CheckBox
        Me.chkMonths = New System.Windows.Forms.CheckBox
        Me.chkWeeks = New System.Windows.Forms.CheckBox
        Me.chkHours = New System.Windows.Forms.CheckBox
        Me.chkMinutes = New System.Windows.Forms.CheckBox
        Me.chkSeconds = New System.Windows.Forms.CheckBox
        Me.chkDays = New System.Windows.Forms.CheckBox
        Me.chkMilliseconds = New System.Windows.Forms.CheckBox
        Me.panelTop = New System.Windows.Forms.Panel
        Me.panelDetail = New System.Windows.Forms.Panel
        Me.comboTimeUnits = New System.Windows.Forms.ComboBox
        Me.panelTop.SuspendLayout()
        Me.panelDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelDuration
        '
        Me.LabelDuration.BackColor = System.Drawing.Color.Transparent
        Me.LabelDuration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDuration.Location = New System.Drawing.Point(8, 8)
        Me.LabelDuration.Name = "LabelDuration"
        Me.LabelDuration.Size = New System.Drawing.Size(96, 24)
        Me.LabelDuration.TabIndex = 36
        Me.LabelDuration.Text = "Duration"
        '
        'chkAll
        '
        Me.chkAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.Location = New System.Drawing.Point(224, 8)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(144, 32)
        Me.chkAll.TabIndex = 37
        Me.chkAll.Text = "Allow all time units"
        '
        'chkYears
        '
        Me.chkYears.Location = New System.Drawing.Point(16, 8)
        Me.chkYears.Name = "chkYears"
        Me.chkYears.Size = New System.Drawing.Size(176, 24)
        Me.chkYears.TabIndex = 38
        Me.chkYears.Tag = "a"
        Me.chkYears.Text = "Years"
        '
        'chkMonths
        '
        Me.chkMonths.Location = New System.Drawing.Point(16, 32)
        Me.chkMonths.Name = "chkMonths"
        Me.chkMonths.Size = New System.Drawing.Size(176, 24)
        Me.chkMonths.TabIndex = 39
        Me.chkMonths.Tag = "mo"
        Me.chkMonths.Text = "Months"
        '
        'chkWeeks
        '
        Me.chkWeeks.Location = New System.Drawing.Point(16, 56)
        Me.chkWeeks.Name = "chkWeeks"
        Me.chkWeeks.Size = New System.Drawing.Size(176, 24)
        Me.chkWeeks.TabIndex = 40
        Me.chkWeeks.Tag = "wk"
        Me.chkWeeks.Text = "Weeks"
        '
        'chkHours
        '
        Me.chkHours.Location = New System.Drawing.Point(224, 8)
        Me.chkHours.Name = "chkHours"
        Me.chkHours.Size = New System.Drawing.Size(152, 24)
        Me.chkHours.TabIndex = 41
        Me.chkHours.Tag = "h"
        Me.chkHours.Text = "Hours"
        '
        'chkMinutes
        '
        Me.chkMinutes.Location = New System.Drawing.Point(224, 32)
        Me.chkMinutes.Name = "chkMinutes"
        Me.chkMinutes.Size = New System.Drawing.Size(152, 24)
        Me.chkMinutes.TabIndex = 42
        Me.chkMinutes.Tag = "min"
        Me.chkMinutes.Text = "Minutes"
        '
        'chkSeconds
        '
        Me.chkSeconds.Location = New System.Drawing.Point(224, 56)
        Me.chkSeconds.Name = "chkSeconds"
        Me.chkSeconds.Size = New System.Drawing.Size(152, 24)
        Me.chkSeconds.TabIndex = 43
        Me.chkSeconds.Tag = "s"
        Me.chkSeconds.Text = "Seconds"
        '
        'chkDays
        '
        Me.chkDays.Location = New System.Drawing.Point(16, 80)
        Me.chkDays.Name = "chkDays"
        Me.chkDays.Size = New System.Drawing.Size(176, 24)
        Me.chkDays.TabIndex = 44
        Me.chkDays.Tag = "d"
        Me.chkDays.Text = "Days"
        '
        'chkMilliseconds
        '
        Me.chkMilliseconds.Location = New System.Drawing.Point(224, 80)
        Me.chkMilliseconds.Name = "chkMilliseconds"
        Me.chkMilliseconds.Size = New System.Drawing.Size(152, 24)
        Me.chkMilliseconds.TabIndex = 45
        Me.chkMilliseconds.Tag = "millisec"
        Me.chkMilliseconds.Text = "Milliseconds"
        Me.chkMilliseconds.Visible = False
        '
        'panelTop
        '
        Me.panelTop.Controls.Add(Me.chkAll)
        Me.panelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTop.Location = New System.Drawing.Point(0, 0)
        Me.panelTop.Name = "panelTop"
        Me.panelTop.Size = New System.Drawing.Size(384, 48)
        Me.panelTop.TabIndex = 46
        '
        'panelDetail
        '
        Me.panelDetail.Controls.Add(Me.chkMilliseconds)
        Me.panelDetail.Controls.Add(Me.chkDays)
        Me.panelDetail.Controls.Add(Me.chkSeconds)
        Me.panelDetail.Controls.Add(Me.chkMinutes)
        Me.panelDetail.Controls.Add(Me.chkHours)
        Me.panelDetail.Controls.Add(Me.chkWeeks)
        Me.panelDetail.Controls.Add(Me.chkMonths)
        Me.panelDetail.Controls.Add(Me.chkYears)
        Me.panelDetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelDetail.Location = New System.Drawing.Point(0, 48)
        Me.panelDetail.Name = "panelDetail"
        Me.panelDetail.Size = New System.Drawing.Size(384, 104)
        Me.panelDetail.TabIndex = 47
        '
        'comboTimeUnits
        '
        Me.comboTimeUnits.Items.AddRange(New Object() {"M; months", "Y; Years"})
        Me.comboTimeUnits.Location = New System.Drawing.Point(256, 160)
        Me.comboTimeUnits.Name = "comboTimeUnits"
        Me.comboTimeUnits.Size = New System.Drawing.Size(88, 24)
        Me.comboTimeUnits.TabIndex = 56
        Me.comboTimeUnits.Visible = False
        '
        'DurationConstraintControl
        '
        Me.Controls.Add(Me.comboTimeUnits)
        Me.Controls.Add(Me.panelDetail)
        Me.Controls.Add(Me.LabelDuration)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "DurationConstraintControl"
        Me.Size = New System.Drawing.Size(384, 264)
        Me.panelTop.ResumeLayout(False)
        Me.panelDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Shadows ReadOnly Property Constraint() As Constraint_Duration
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Duration)

            Return CType(MyBase.Constraint, Constraint_Duration)
        End Get
    End Property

    Dim WithEvents mCountControl As CountConstraintControl

    Private Sub SetUnitDisplay(ByVal Sender As Object, ByVal DisplayUnits As Boolean) Handles mCountControl.ChangeDisplay
        Me.comboTimeUnits.Visible = DisplayUnits
    End Sub

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        Dim s As String = Me.Constraint.AllowableUnits

        mCountControl = New CountConstraintControl(mFileManager)
        Me.Controls.Add(mCountControl)
        mCountControl.Top = Me.panelDetail.Top + Me.panelDetail.Height + 10
        mCountControl.ShowConstraint(IsState, Me.Constraint)
        mCountControl.LabelQuantity.Text = AE_Constants.Instance.Unit

        If s = "*" Then
            Me.chkAll.Checked = True
        Else
            Me.chkAll.Checked = False

            Dim y As String() = s.Split("T".ToCharArray)
            If y.Length = 2 AndAlso y(1) <> "" Then
                ' there are day and time units
                If y(1).IndexOf("H") > -1 Then
                    Me.chkHours.Checked = True
                End If
                If y(1).IndexOf("M") > -1 Then
                    Me.chkMinutes.Checked = True
                End If
                If y(1).IndexOf("S") > -1 Then
                    Me.chkSeconds.Checked = True
                End If
            End If
            If y(0).IndexOf("Y") > -1 Then
                Me.chkYears.Checked = True
            End If
            If y(0).IndexOf("M") > -1 Then
                Me.chkMonths.Checked = True
            End If
            If y(0).IndexOf("W") > -1 Then
                Me.chkWeeks.Checked = True
            End If
            If y(0).IndexOf("D") > -1 Then
                Me.chkDays.Checked = True
            End If
        End If

        If Me.Constraint.ValueUnits <> "" Then
            Dim time_unit As String = ""
            Select Case Me.Constraint.ValueUnits
                Case "Y"
                    time_unit = chkYears.Text
                Case "M"
                    time_unit = chkMonths.Text
                Case "W"
                    time_unit = chkWeeks.Text
                Case "D"
                    time_unit = chkDays.Text
                Case "TH"
                    time_unit = chkHours.Text
                Case "TM"
                    time_unit = chkMinutes.Text
                Case "TS"
                    time_unit = chkSeconds.Text
                Case Else
                    Debug.Assert(False, "Not handled")
            End Select
            Me.comboTimeUnits.SelectedIndex = Me.comboTimeUnits.FindStringExact(time_unit)
        End If

        ResetTimeUnits()
        Me.LabelDuration.Text = AE_Constants.Instance.Duration

    End Sub

    Private Function GetAllowableUnits() As String
        Dim result As String = ""
        Dim time_separated As Boolean


        If Me.chkYears.Checked Then
            result &= "Y"
        End If
        If Me.chkMonths.Checked Then
            result &= "M"
        End If
        If Me.chkWeeks.Checked Then
            result &= "W"
        End If
        If Me.chkDays.Checked Then
            result &= "D"
        End If
        If Me.chkHours.Checked Then
            result &= "TH"
            time_separated = True
        End If
        If Me.chkMinutes.Checked Then
            If Not time_separated Then
                result &= "T"
                time_separated = True
            End If
            result &= "M"
        End If
        If Me.chkSeconds.Checked Then
            If Not time_separated Then
                result &= "T"
                time_separated = True
            End If
            result &= "S"
        End If
        Return result

    End Function

    Private Sub ResetTimeUnits()

        Dim s As String = Me.comboTimeUnits.Text

        Me.comboTimeUnits.Items.Clear()
        If chkAll.Checked Then
            Me.comboTimeUnits.Items.Add(chkYears.Text)
            Me.comboTimeUnits.Items.Add(chkMonths.Text)
            Me.comboTimeUnits.Items.Add(chkWeeks.Text)
            Me.comboTimeUnits.Items.Add(chkDays.Text)
            Me.comboTimeUnits.Items.Add(chkHours.Text)
            Me.comboTimeUnits.Items.Add(chkMinutes.Text)
            Me.comboTimeUnits.Items.Add(chkSeconds.Text)
        Else
            If chkYears.Checked Then
                Me.comboTimeUnits.Items.Add(chkYears.Text)
            End If
            If chkMonths.Checked Then
                Me.comboTimeUnits.Items.Add(chkMonths.Text)
            End If
            If chkWeeks.Checked Then
                Me.comboTimeUnits.Items.Add(chkWeeks.Text)
            End If
            If chkDays.Checked Then
                Me.comboTimeUnits.Items.Add(chkDays.Text)
            End If
            If chkHours.Checked Then
                Me.comboTimeUnits.Items.Add(chkHours.Text)
            End If
            If chkMinutes.Checked Then
                Me.comboTimeUnits.Items.Add(chkMinutes.Text)
            End If
            If chkSeconds.Checked Then
                Me.comboTimeUnits.Items.Add(chkSeconds.Text)
            End If
        End If

        ' reset the units to the previous value
        Dim i As Integer = Me.comboTimeUnits.FindStringExact(s)
        If i > -1 Then
            Me.comboTimeUnits.SelectedIndex = i
        Else
            Me.comboTimeUnits.SelectedIndex = Me.comboTimeUnits.Items.Count - 1
        End If

    End Sub
    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If chkAll.Checked Then
            Me.panelDetail.Visible = False
            mCountControl.Top = Me.panelTop.Top + Me.panelTop.Height + 10
            Me.comboTimeUnits.Top = Me.panelTop.Top + Me.panelTop.Height + 10
        Else
            Me.panelDetail.Visible = True
            mCountControl.Top = Me.panelDetail.Top + Me.panelDetail.Height + 10
            Me.comboTimeUnits.Top = Me.panelDetail.Top + Me.panelDetail.Height + 10
        End If

        ResetTimeUnits()

        If Not MyBase.IsLoading() Then
            If chkAll.Checked Then
                Me.Constraint.AllowableUnits = "*"
            Else
                Me.Constraint.AllowableUnits = GetAllowableUnits()
            End If
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles chkYears.CheckedChanged, chkDays.CheckedChanged, _
        chkHours.CheckedChanged, chkMinutes.CheckedChanged, chkMonths.CheckedChanged, _
        chkSeconds.CheckedChanged, chkWeeks.CheckedChanged

        Dim s As String = Me.comboTimeUnits.Text

        ResetTimeUnits()


        If Not MyBase.IsLoading() Then
            Me.Constraint.AllowableUnits = GetAllowableUnits()
            mFileManager.FileEdited = True
        End If
    End Sub


    Private Sub comboTimeUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboTimeUnits.SelectedIndexChanged
        If Not MyBase.IsLoading Then
            If comboTimeUnits.SelectedIndex > -1 Then
                Dim s As String

                Select Case comboTimeUnits.Text
                    Case Me.chkYears.Text
                        s = "Y"
                    Case Me.chkMonths.Text
                        s = "M"
                    Case Me.chkWeeks.Text
                        s = "W"
                    Case Me.chkDays.Text
                        s = "D"
                    Case Me.chkHours.Text
                        s = "TH"
                    Case Me.chkMinutes.Text
                        s = "TM"
                    Case Me.chkSeconds.Text
                        s = "TS"
                    Case Else
                        Debug.Assert(False, "Not handled")
                        Return
                End Select
                Me.Constraint.ValueUnits = s
                mFileManager.FileEdited = True
            End If
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
