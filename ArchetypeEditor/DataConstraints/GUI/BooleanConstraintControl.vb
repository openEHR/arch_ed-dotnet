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

Public Class BooleanConstraintControl : Inherits ConstraintControl 'AnyConstraintControl

    Private mIsState As Boolean


#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If Not Me.DesignMode Then
            Debug.Assert(False, "Should not get to here")
        End If

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mFileManager = a_file_manager

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.LabelTrueFalse.Text = Filemanager.GetOpenEhrTerm(157, Me.LabelTrueFalse.Text)
            Me.LabelTrueFalseDefault.Text = Filemanager.GetOpenEhrTerm(600, Me.LabelTrueFalseDefault.Text)
            Me.gbAssummedValue.Text = Filemanager.GetOpenEhrTerm(158, Me.gbAssummedValue.Text)



        End If

    End Sub


    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents PanelBoolean As System.Windows.Forms.Panel
    Friend WithEvents LabelTrueFalseDefault As System.Windows.Forms.Label
    Friend WithEvents listBoolean As System.Windows.Forms.ListBox
    Friend WithEvents LabelTrueFalse As System.Windows.Forms.Label
    Friend WithEvents gbAssummedValue As System.Windows.Forms.GroupBox
    Friend WithEvents cbTrue As System.Windows.Forms.CheckBox
    Friend WithEvents cbFalse As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelTrueFalseDefault = New System.Windows.Forms.Label
        Me.listBoolean = New System.Windows.Forms.ListBox
        Me.LabelTrueFalse = New System.Windows.Forms.Label
        Me.gbAssummedValue = New System.Windows.Forms.GroupBox
        Me.cbTrue = New System.Windows.Forms.CheckBox
        Me.cbFalse = New System.Windows.Forms.CheckBox
        Me.gbAssummedValue.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelTrueFalseDefault
        '
        Me.LabelTrueFalseDefault.Location = New System.Drawing.Point(8, 40)
        Me.LabelTrueFalseDefault.Name = "LabelTrueFalseDefault"
        Me.LabelTrueFalseDefault.Size = New System.Drawing.Size(120, 36)
        Me.LabelTrueFalseDefault.TabIndex = 2
        Me.LabelTrueFalseDefault.Text = "Allowed values:"
        Me.LabelTrueFalseDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'listBoolean
        '
        Me.listBoolean.Location = New System.Drawing.Point(72, 24)
        Me.listBoolean.Name = "listBoolean"
        Me.listBoolean.Size = New System.Drawing.Size(72, 43)
        Me.listBoolean.TabIndex = 1
        '
        'LabelTrueFalse
        '
        Me.LabelTrueFalse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTrueFalse.Location = New System.Drawing.Point(16, 8)
        Me.LabelTrueFalse.Name = "LabelTrueFalse"
        Me.LabelTrueFalse.Size = New System.Drawing.Size(96, 24)
        Me.LabelTrueFalse.TabIndex = 0
        Me.LabelTrueFalse.Text = "True/False"
        '
        'gbAssummedValue
        '
        Me.gbAssummedValue.Controls.Add(Me.listBoolean)
        Me.gbAssummedValue.Location = New System.Drawing.Point(56, 88)
        Me.gbAssummedValue.Name = "gbAssummedValue"
        Me.gbAssummedValue.Size = New System.Drawing.Size(160, 80)
        Me.gbAssummedValue.TabIndex = 3
        Me.gbAssummedValue.TabStop = False
        Me.gbAssummedValue.Text = "Assumed value"
        '
        'cbTrue
        '
        Me.cbTrue.Checked = True
        Me.cbTrue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbTrue.Location = New System.Drawing.Point(152, 32)
        Me.cbTrue.Name = "cbTrue"
        Me.cbTrue.Size = New System.Drawing.Size(72, 24)
        Me.cbTrue.TabIndex = 4
        Me.cbTrue.Text = "True"
        '
        'cbFalse
        '
        Me.cbFalse.Checked = True
        Me.cbFalse.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbFalse.Location = New System.Drawing.Point(152, 56)
        Me.cbFalse.Name = "cbFalse"
        Me.cbFalse.Size = New System.Drawing.Size(72, 24)
        Me.cbFalse.TabIndex = 5
        Me.cbFalse.Text = "False"
        '
        'BooleanConstraintControl
        '
        Me.Controls.Add(Me.cbFalse)
        Me.Controls.Add(Me.cbTrue)
        Me.Controls.Add(Me.gbAssummedValue)
        Me.Controls.Add(Me.LabelTrueFalseDefault)
        Me.Controls.Add(Me.LabelTrueFalse)
        Me.Name = "BooleanConstraintControl"
        Me.Size = New System.Drawing.Size(232, 184)
        Me.gbAssummedValue.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Shadows ReadOnly Property Constraint() As Constraint_Boolean
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Boolean)

            Return CType(MyBase.Constraint, Constraint_Boolean)
        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        mIsState = IsState

        ' set constraint values on control

        Me.cbFalse.Text = System.Boolean.FalseString
        Me.cbTrue.Text = System.Boolean.TrueString

        If Me.Constraint.TrueFalseAllowed Then
            Me.cbTrue.Checked = True
            Me.cbFalse.Checked = True
        ElseIf Me.Constraint.TrueAllowed Then
            Me.cbTrue.Checked = True
            Me.cbFalse.Checked = False
        ElseIf Me.Constraint.FalseAllowed Then
            Me.cbFalse.Checked = True
            Me.cbTrue.Checked = False
        End If

        If IsState Then
            Me.gbAssummedValue.Visible = True
            If Me.Constraint.hasAssumedValue Then
                If Me.Constraint.AssumedValue Then
                    Me.listBoolean.SelectedIndex = Me.listBoolean.Items.IndexOf(System.Boolean.TrueString)
                Else
                    Me.listBoolean.SelectedIndex = Me.listBoolean.Items.IndexOf(System.Boolean.FalseString)
                End If
            End If
        Else
            Me.gbAssummedValue.Visible = False
        End If

    End Sub

    Private Sub listBoolean_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listBoolean.SelectedIndexChanged
        If Not MyBase.IsLoading AndAlso mIsState = True AndAlso Me.listBoolean.SelectedIndex <> -1 Then

            Me.Constraint.hasAssumedValue = True

            Select Case Me.listBoolean.SelectedItem.ToString
                Case System.Boolean.TrueString
                    ' true
                    Me.Constraint.AssumedValue = True

                Case System.Boolean.FalseString ' false
                    Me.Constraint.AssumedValue = False

                Case Else
                    Debug.Assert(False)
            End Select

            mFileManager.FileEdited = True

        End If
    End Sub

    Private Sub cbTrue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTrue.CheckedChanged
        If Not MyBase.IsLoading Then
            If Me.cbTrue.Checked Then
                If Me.Constraint.FalseAllowed Then
                    Me.Constraint.TrueFalseAllowed = True
                Else
                    Me.Constraint.TrueAllowed = True
                End If
            Else
                If Not Me.cbFalse.Checked Then
                    ' something has to be allowed
                    Me.cbFalse.Checked = True
                Else
                    Me.Constraint.FalseAllowed = True
                End If
            End If

            mFileManager.FileEdited = True
        End If

        If Me.listBoolean.Items.Contains(System.Boolean.TrueString) Then
            If Not Me.cbTrue.Checked Then
                Me.listBoolean.Items.Remove(System.Boolean.TrueString)
            End If
        Else
            If Me.cbTrue.Checked Then
                Me.listBoolean.Items.Add(System.Boolean.TrueString)
            End If
        End If

    End Sub

    Private Sub cbFalse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFalse.CheckedChanged
        If Not MyBase.IsLoading Then
            If Me.cbFalse.Checked Then
                If Me.Constraint.TrueAllowed Then
                    Me.Constraint.TrueFalseAllowed = True
                Else
                    Me.Constraint.FalseAllowed = True
                End If
            Else
                If Not Me.cbTrue.Checked Then
                    ' something has to be allowed
                    Me.cbTrue.Checked = True
                Else
                    Me.Constraint.TrueAllowed = True
                End If
            End If

            mFileManager.FileEdited = True
        End If

        If Me.listBoolean.Items.Contains(System.Boolean.FalseString) Then
            If Not Me.cbFalse.Checked Then
                Me.listBoolean.Items.Remove(System.Boolean.FalseString)
            End If
        Else
            If Me.cbFalse.Checked Then
                Me.listBoolean.Items.Add(System.Boolean.FalseString)
            End If
        End If
    End Sub

    Private Sub BooleanConstraintControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LabelTrueFalse.Text = AE_Constants.Instance.Boolean_
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
'The Original Code is BooleanConstraintControl.vb.
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
