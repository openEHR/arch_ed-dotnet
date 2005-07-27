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

Public Class ClusterControl : Inherits ConstraintControl

    Dim mHeader As Integer

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal aFileManager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = aFileManager

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents numMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbUnbounded As System.Windows.Forms.CheckBox
    Friend WithEvents lblNumMax As System.Windows.Forms.Label
    Friend WithEvents lblNumMin As System.Windows.Forms.Label
    Friend WithEvents numMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbOrdered As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.numMax = New System.Windows.Forms.NumericUpDown
        Me.cbUnbounded = New System.Windows.Forms.CheckBox
        Me.lblNumMax = New System.Windows.Forms.Label
        Me.lblNumMin = New System.Windows.Forms.Label
        Me.numMin = New System.Windows.Forms.NumericUpDown
        Me.cbOrdered = New System.Windows.Forms.CheckBox
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'numMax
        '
        Me.numMax.Location = New System.Drawing.Point(208, 48)
        Me.numMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMax.Name = "numMax"
        Me.numMax.Size = New System.Drawing.Size(48, 20)
        Me.numMax.TabIndex = 8
        Me.numMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cbUnbounded
        '
        Me.cbUnbounded.Location = New System.Drawing.Point(264, 48)
        Me.cbUnbounded.Name = "cbUnbounded"
        Me.cbUnbounded.Size = New System.Drawing.Size(96, 16)
        Me.cbUnbounded.TabIndex = 9
        Me.cbUnbounded.Text = "Unbounded"
        '
        'lblNumMax
        '
        Me.lblNumMax.Location = New System.Drawing.Point(168, 48)
        Me.lblNumMax.Name = "lblNumMax"
        Me.lblNumMax.Size = New System.Drawing.Size(32, 16)
        Me.lblNumMax.TabIndex = 7
        Me.lblNumMax.Text = "Max:"
        Me.lblNumMax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumMin
        '
        Me.lblNumMin.BackColor = System.Drawing.Color.Transparent
        Me.lblNumMin.Location = New System.Drawing.Point(8, 48)
        Me.lblNumMin.Name = "lblNumMin"
        Me.lblNumMin.Size = New System.Drawing.Size(104, 16)
        Me.lblNumMin.TabIndex = 5
        Me.lblNumMin.Text = "Cardinality -Min:"
        Me.lblNumMin.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'numMin
        '
        Me.numMin.Location = New System.Drawing.Point(120, 48)
        Me.numMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMin.Name = "numMin"
        Me.numMin.Size = New System.Drawing.Size(40, 20)
        Me.numMin.TabIndex = 6
        Me.numMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cbOrdered
        '
        Me.cbOrdered.Location = New System.Drawing.Point(120, 0)
        Me.cbOrdered.Name = "cbOrdered"
        Me.cbOrdered.Size = New System.Drawing.Size(112, 24)
        Me.cbOrdered.TabIndex = 33
        Me.cbOrdered.Text = "Ordered"
        '
        'ClusterControl
        '
        Me.Controls.Add(Me.cbOrdered)
        Me.Controls.Add(Me.numMax)
        Me.Controls.Add(Me.cbUnbounded)
        Me.Controls.Add(Me.lblNumMax)
        Me.Controls.Add(Me.lblNumMin)
        Me.Controls.Add(Me.numMin)
        Me.Name = "ClusterControl"
        Me.Size = New System.Drawing.Size(376, 80)
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim mItem As ArchetypeComposite

    Public Property Item() As ArchetypeComposite
        Get
            Return mItem
        End Get
        Set(ByVal Value As ArchetypeComposite)
            mItem = Value
            IsLoading = True
            SetValues()
            IsLoading = False
        End Set
    End Property

    Public Property Header() As Integer
        Get
            Return mHeader
        End Get
        Set(ByVal Value As Integer)
            Dim adjust As Integer

            If Value > 0 Then
                If mHeader = 0 Then
                    adjust = Value
                Else
                    adjust = Value - mHeader
                End If
                mHeader = Value
                For Each ctrl As Control In Me.Controls
                    ctrl.Location = New Drawing.Point(ctrl.Location.X, ctrl.Location.Y + adjust)
                Next
            End If

        End Set
    End Property

    Private Sub SetValues()
        ' set the cardinality
        If mItem.Cardinality.IsUnbounded _
            OrElse mItem.Cardinality.MaxCount < 1 Then

            Me.cbUnbounded.Checked = True
        Else
            Me.cbUnbounded.Checked = False
            Me.numMax.Value = mItem.Cardinality.MaxCount
        End If

        Me.numMin.Value = mItem.Cardinality.MinCount

        Me.cbOrdered.Checked = mItem.IsOrdered
    End Sub

    Private Sub numMin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMin.ValueChanged, numMin.TextChanged
        If Not IsLoading Then
            Try
                If numMin.Value > numMax.Value Then
                    numMax.Value = numMin.Value
                End If
                mItem.Cardinality.MinCount = CInt(Me.numMin.Value)

                mFileManager.FileEdited = True

            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
            End Try
        End If
    End Sub

    Private Sub numMax_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMax.ValueChanged, numMax.TextChanged

        'If Not mArchetypeNode Is Nothing Then
        If Not MyBase.IsLoading Then
            Try
                mItem.Cardinality.MaxCount = CInt(Me.numMax.Value)

                mFileManager.FileEdited = True

                If numMax.Value < numMin.Value Then
                    numMin.Value = numMax.Value
                End If

            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
            End Try
        End If

    End Sub

    Private Sub cbUnbounded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUnbounded.CheckedChanged
        Try
            If Me.cbUnbounded.Checked Then
                Me.numMax.Visible = False
                Me.lblNumMax.Visible = False
                mItem.Cardinality.IsUnbounded = True
            Else
                Me.numMax.Visible = True
                Me.lblNumMax.Visible = True

                If Me.numMax.Value < Me.numMin.Value Then
                    Me.numMax.Value = Me.numMin.Value
                End If
                mItem.Cardinality.MaxCount = CInt(Me.numMax.Value)
                mItem.Cardinality.IsUnbounded = False
            End If

            If Not MyBase.IsLoading Then mFileManager.FileEdited = True

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try
    End Sub

    Private Sub cbOrdered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdered.CheckedChanged
        If Not MyBase.IsLoading Then
            mItem.IsOrdered = cbOrdered.Checked
            mFileManager.FileEdited = True
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
'The Original Code is ConstraintControl.vb.
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