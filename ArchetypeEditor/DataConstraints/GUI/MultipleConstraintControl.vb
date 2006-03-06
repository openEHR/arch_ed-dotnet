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

Public Class MultipleConstraintControl : Inherits ConstraintControl 'AnyConstraintControl
    Friend WithEvents PanelMultipleControl As System.Windows.Forms.Panel
    Friend WithEvents TabConstraints As System.Windows.Forms.TabControl
    Friend WithEvents AddConstraintMenu As ConstraintContextMenu
    Private mIsState As Boolean

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
    'Friend WithEvents PanelBoolean As System.Windows.Forms.Panel
    Friend WithEvents butRemoveUnit As System.Windows.Forms.Button
    Friend WithEvents butAddConstraint As System.Windows.Forms.Button
    Friend WithEvents ContextMenuDataType As System.Windows.Forms.ContextMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MultipleConstraintControl))
        Me.TabConstraints = New System.Windows.Forms.TabControl
        Me.PanelMultipleControl = New System.Windows.Forms.Panel
        Me.butRemoveUnit = New System.Windows.Forms.Button
        Me.butAddConstraint = New System.Windows.Forms.Button
        Me.ContextMenuDataType = New System.Windows.Forms.ContextMenu
        Me.PanelMultipleControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabConstraints
        '
        Me.TabConstraints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabConstraints.Location = New System.Drawing.Point(32, 0)
        Me.TabConstraints.Name = "TabConstraints"
        Me.TabConstraints.SelectedIndex = 0
        Me.TabConstraints.Size = New System.Drawing.Size(360, 232)
        Me.TabConstraints.TabIndex = 0
        '
        'PanelMultipleControl
        '
        Me.PanelMultipleControl.Controls.Add(Me.butRemoveUnit)
        Me.PanelMultipleControl.Controls.Add(Me.butAddConstraint)
        Me.PanelMultipleControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelMultipleControl.Location = New System.Drawing.Point(0, 0)
        Me.PanelMultipleControl.Name = "PanelMultipleControl"
        Me.PanelMultipleControl.Size = New System.Drawing.Size(32, 232)
        Me.PanelMultipleControl.TabIndex = 1
        '
        'butRemoveUnit
        '
        Me.butRemoveUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveUnit.Image = CType(resources.GetObject("butRemoveUnit.Image"), System.Drawing.Image)
        Me.butRemoveUnit.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveUnit.Location = New System.Drawing.Point(4, 40)
        Me.butRemoveUnit.Name = "butRemoveUnit"
        Me.butRemoveUnit.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveUnit.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.butRemoveUnit, "Remove selected constraint")
        '
        'butAddConstraint
        '
        Me.butAddConstraint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddConstraint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butAddConstraint.Image = CType(resources.GetObject("butAddConstraint.Image"), System.Drawing.Image)
        Me.butAddConstraint.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddConstraint.Location = New System.Drawing.Point(4, 8)
        Me.butAddConstraint.Name = "butAddConstraint"
        Me.butAddConstraint.Size = New System.Drawing.Size(24, 25)
        Me.butAddConstraint.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.butAddConstraint, "Add new constraint")
        '
        'MultipleConstraintControl
        '
        Me.Controls.Add(Me.TabConstraints)
        Me.Controls.Add(Me.PanelMultipleControl)
        Me.Name = "MultipleConstraintControl"
        Me.Size = New System.Drawing.Size(392, 232)
        Me.PanelMultipleControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region


    Private Shadows ReadOnly Property Constraint() As Constraint_Choice
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Choice)

            Return CType(MyBase.Constraint, Constraint_Choice)
        End Get
    End Property

    Private Sub AddConstraintControl(ByVal c As Constraint)
        Dim tp As TabPage
        Dim cc As ConstraintControl

        tp = New TabPage(c.ConstraintTypeString)

        If c.Type <> ConstraintType.Any And c.Type <> ConstraintType.URI Then
            cc = ConstraintControl.CreateConstraintControl( _
                    c.Type, mFileManager)
            tp.Controls.Add(cc)
            cc.ShowConstraint(mIsState, c)
            cc.Dock = DockStyle.Fill
        End If
        Me.TabConstraints.TabPages.Add(tp)
    End Sub
    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)
        Dim i As Integer
        Dim cc As ConstraintControl

        mIsState = IsState
        ' set constraint values on control

        For i = 0 To Me.Constraint.Constraints.Count - 1
            AddConstraintControl(Me.Constraint.Constraints.Item(i))
        Next

    End Sub
    Private Sub AddConstraint(ByVal a_constraint As Constraint)
        Me.Constraint.Constraints.Add(a_constraint)
        AddConstraintControl(a_constraint)
        mFileManager.FileEdited = True
    End Sub
    Private Sub butAddConstraint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddConstraint.Click
        If AddConstraintMenu Is Nothing Then
            AddConstraintMenu = New ConstraintContextMenu(New ConstraintContextMenu.ProcessMenuClick(AddressOf AddConstraint))
        Else
            AddConstraintMenu.Reset()
        End If
        AddConstraintMenu.HideMenuItem(ConstraintType.Multiple)
        AddConstraintMenu.Show(butAddConstraint, New System.Drawing.Point(5, 5))
    End Sub

    Private Sub butRemoveUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveUnit.Click
        If MessageBox.Show(AE_Constants.Instance.Remove & Me.TabConstraints.SelectedTab.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
            Dim i As Integer
            i = Me.TabConstraints.SelectedIndex
            Me.Constraint.Constraints.RemoveAt(i)
            Me.TabConstraints.TabPages.Remove(Me.TabConstraints.SelectedTab)
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
'The Original Code is MultipleConstraintControl.vb.
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
