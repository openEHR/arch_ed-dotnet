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

        If Main.Instance.DefaultLanguageCode <> "en" Then
            Translate()
        End If
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
        'chkDays
        '
        Me.chkDays.Location = New System.Drawing.Point(16, 80)
        Me.chkDays.Name = "chkDays"
        Me.chkDays.Size = New System.Drawing.Size(176, 24)
        Me.chkDays.TabIndex = 41
        Me.chkDays.Tag = "d"
        Me.chkDays.Text = "Days"
        '
        'chkHours
        '
        Me.chkHours.Location = New System.Drawing.Point(224, 8)
        Me.chkHours.Name = "chkHours"
        Me.chkHours.Size = New System.Drawing.Size(152, 24)
        Me.chkHours.TabIndex = 42
        Me.chkHours.Tag = "h"
        Me.chkHours.Text = "Hours"
        '
        'chkMinutes
        '
        Me.chkMinutes.Location = New System.Drawing.Point(224, 32)
        Me.chkMinutes.Name = "chkMinutes"
        Me.chkMinutes.Size = New System.Drawing.Size(152, 24)
        Me.chkMinutes.TabIndex = 43
        Me.chkMinutes.Tag = "min"
        Me.chkMinutes.Text = "Minutes"
        '
        'chkSeconds
        '
        Me.chkSeconds.Location = New System.Drawing.Point(224, 56)
        Me.chkSeconds.Name = "chkSeconds"
        Me.chkSeconds.Size = New System.Drawing.Size(152, 24)
        Me.chkSeconds.TabIndex = 44
        Me.chkSeconds.Tag = "s"
        Me.chkSeconds.Text = "Seconds"
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

    Protected ReadOnly Property Constraint() As Constraint_Duration
        Get
            Return CType(mConstraint, Constraint_Duration)
        End Get
    End Property

    Dim WithEvents mCountControl As CountConstraintControl

    Private Sub SetUnitDisplay(ByVal Sender As Object, ByVal DisplayUnits As Boolean) Handles mCountControl.ChangeDisplay
        comboTimeUnits.Visible = DisplayUnits
    End Sub

    Public Sub Translate()
        LabelDuration.Text = Filemanager.GetOpenEhrTerm(142, LabelDuration.Text)
        chkYears.Text = Main.ISO_TimeUnits.GetLanguageForISO("a")
        chkMonths.Text = Main.ISO_TimeUnits.GetLanguageForISO("mo")
        chkWeeks.Text = Main.ISO_TimeUnits.GetLanguageForISO("wk")
        chkDays.Text = Main.ISO_TimeUnits.GetLanguageForISO("d")
        chkHours.Text = Main.ISO_TimeUnits.GetLanguageForISO("h")
        chkMinutes.Text = Main.ISO_TimeUnits.GetLanguageForISO("m")
        chkSeconds.Text = Main.ISO_TimeUnits.GetLanguageForISO("s")
        chkMilliseconds.Text = Main.ISO_TimeUnits.GetLanguageForISO("millisec")
    End Sub

    Protected Overrides Sub SetControlValues(ByVal IsState As Boolean)
        Dim s As String = Constraint.AllowableUnits

        mCountControl = New CountConstraintControl(mFileManager)
        Controls.Add(mCountControl)
        mCountControl.Top = panelDetail.Top + panelDetail.Height + 10
        mCountControl.ShowConstraint(IsState, Constraint)
        mCountControl.LabelQuantity.Text = AE_Constants.Instance.Unit

        If s = "PYMWDTHMS" Then
            chkAll.Checked = True
        Else
            chkAll.Checked = False
            Dim y As String() = s.Split("T".ToCharArray)

            If y.Length = 2 AndAlso y(1) <> "" Then
                ' there are day and time units
                If y(1).IndexOf("H") > -1 Then
                    chkHours.Checked = True
                End If

                If y(1).IndexOf("M") > -1 Then
                    chkMinutes.Checked = True
                End If

                If y(1).IndexOf("S") > -1 Then
                    chkSeconds.Checked = True
                End If
            End If

            If y(0).IndexOf("Y") > -1 Then
                chkYears.Checked = True
            End If

            If y(0).IndexOf("M") > -1 Then
                chkMonths.Checked = True
            End If

            If y(0).IndexOf("W") > -1 Then
                chkWeeks.Checked = True
            End If

            If y(0).IndexOf("D") > -1 Then
                chkDays.Checked = True
            End If
        End If

        If Constraint.MinMaxValueUnits <> "" Then
            Dim time_unit As String = ""

            Select Case Constraint.MinMaxValueUnits
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

            comboTimeUnits.SelectedIndex = comboTimeUnits.FindStringExact(time_unit)
        End If

        ResetTimeUnits()
        LabelDuration.Text = AE_Constants.Instance.Duration
    End Sub

    Private Function GetAllowableUnits() As String
        Dim result As String = "P"
        Dim time_separated As Boolean

        If chkYears.Checked Then
            result &= "Y"
        End If

        If chkMonths.Checked Then
            result &= "M"
        End If

        If chkWeeks.Checked Then
            result &= "W"
        End If

        If chkDays.Checked Then
            result &= "D"
        End If

        If chkHours.Checked Then
            result &= "TH"
            time_separated = True
        End If

        If chkMinutes.Checked Then
            If Not time_separated Then
                result &= "T"
                time_separated = True
            End If

            result &= "M"
        End If

        If chkSeconds.Checked Then
            If Not time_separated Then
                result &= "T"
                time_separated = True
            End If

            result &= "S"
        End If

        Return result
    End Function

    Private Sub ResetTimeUnits()
        Dim s As String = comboTimeUnits.Text
        comboTimeUnits.Items.Clear()

        If chkAll.Checked Then
            comboTimeUnits.Items.Add(chkYears.Text)
            comboTimeUnits.Items.Add(chkMonths.Text)
            comboTimeUnits.Items.Add(chkWeeks.Text)
            comboTimeUnits.Items.Add(chkDays.Text)
            comboTimeUnits.Items.Add(chkHours.Text)
            comboTimeUnits.Items.Add(chkMinutes.Text)
            comboTimeUnits.Items.Add(chkSeconds.Text)
        Else
            If chkYears.Checked Then
                comboTimeUnits.Items.Add(chkYears.Text)
            End If

            If chkMonths.Checked Then
                comboTimeUnits.Items.Add(chkMonths.Text)
            End If

            If chkWeeks.Checked Then
                comboTimeUnits.Items.Add(chkWeeks.Text)
            End If

            If chkDays.Checked Then
                comboTimeUnits.Items.Add(chkDays.Text)
            End If

            If chkHours.Checked Then
                comboTimeUnits.Items.Add(chkHours.Text)
            End If

            If chkMinutes.Checked Then
                comboTimeUnits.Items.Add(chkMinutes.Text)
            End If

            If chkSeconds.Checked Then
                comboTimeUnits.Items.Add(chkSeconds.Text)
            End If
        End If

        ' reset the units to the previous value
        Dim i As Integer = comboTimeUnits.FindStringExact(s)

        If i > -1 Then
            comboTimeUnits.SelectedIndex = i
        Else
            comboTimeUnits.SelectedIndex = comboTimeUnits.Items.Count - 1
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If chkAll.Checked Then
            panelDetail.Visible = False
            mCountControl.Top = panelTop.Top + panelTop.Height + 10
            comboTimeUnits.Top = panelTop.Top + panelTop.Height + 10
        Else
            panelDetail.Visible = True
            mCountControl.Top = panelDetail.Top + panelDetail.Height + 10
            comboTimeUnits.Top = panelDetail.Top + panelDetail.Height + 10
        End If

        ResetTimeUnits()

        If Not IsLoading() Then
            If chkAll.Checked Then
                Constraint.AllowableUnits = "PYMWDTHMS"
            Else
                Constraint.AllowableUnits = GetAllowableUnits()
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles chkYears.CheckedChanged, chkDays.CheckedChanged, _
        chkHours.CheckedChanged, chkMinutes.CheckedChanged, chkMonths.CheckedChanged, _
        chkSeconds.CheckedChanged, chkWeeks.CheckedChanged

        Dim s As String = comboTimeUnits.Text
        ResetTimeUnits()

        If Not IsLoading() Then
            Constraint.AllowableUnits = GetAllowableUnits()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub comboTimeUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboTimeUnits.SelectedIndexChanged
        If Not IsLoading Or Constraint.MinMaxValueUnits = "" Then
            If comboTimeUnits.SelectedIndex > -1 Then
                Dim s As String

                Select Case comboTimeUnits.Text
                    Case chkYears.Text
                        s = "Y"
                    Case chkMonths.Text
                        s = "M"
                    Case chkWeeks.Text
                        s = "W"
                    Case chkDays.Text
                        s = "D"
                    Case chkHours.Text
                        s = "TH"
                    Case chkMinutes.Text
                        s = "TM"
                    Case chkSeconds.Text
                        s = "TS"
                    Case Else
                        Debug.Assert(False, "Not handled")
                        Return
                End Select

                Constraint.MinMaxValueUnits = s

                If Not IsLoading Then
                    mFileManager.FileEdited = True
                End If
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

