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

Public Class MultipleConstraintControl : Inherits ConstraintControl
    Friend WithEvents PanelMultipleControl As System.Windows.Forms.Panel
    Friend WithEvents TabConstraints As System.Windows.Forms.TabControl
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
    Friend WithEvents RemoveButton As System.Windows.Forms.Button
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents ContextMenuDataType As System.Windows.Forms.ContextMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MultipleConstraintControl))
        Me.TabConstraints = New System.Windows.Forms.TabControl
        Me.PanelMultipleControl = New System.Windows.Forms.Panel
        Me.RemoveButton = New System.Windows.Forms.Button
        Me.AddButton = New System.Windows.Forms.Button
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
        Me.TabConstraints.TabIndex = 1
        '
        'PanelMultipleControl
        '
        Me.PanelMultipleControl.Controls.Add(Me.RemoveButton)
        Me.PanelMultipleControl.Controls.Add(Me.AddButton)
        Me.PanelMultipleControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelMultipleControl.Location = New System.Drawing.Point(0, 0)
        Me.PanelMultipleControl.Name = "PanelMultipleControl"
        Me.PanelMultipleControl.Size = New System.Drawing.Size(32, 232)
        Me.PanelMultipleControl.TabIndex = 0
        '
        'RemoveButton
        '
        Me.RemoveButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RemoveButton.Image = CType(resources.GetObject("RemoveButton.Image"), System.Drawing.Image)
        Me.RemoveButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.RemoveButton.Location = New System.Drawing.Point(4, 40)
        Me.RemoveButton.Name = "RemoveButton"
        Me.RemoveButton.Size = New System.Drawing.Size(24, 25)
        Me.RemoveButton.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.RemoveButton, "Remove selected constraint")
        '
        'AddButton
        '
        Me.AddButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AddButton.Image = CType(resources.GetObject("AddButton.Image"), System.Drawing.Image)
        Me.AddButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.AddButton.Location = New System.Drawing.Point(4, 8)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(24, 25)
        Me.AddButton.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.AddButton, "Add new constraint")
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
        ' Limit the choices to one of each type except for DV_TEXT (but ensure one is coded, one is free).
        If c.Kind = ConstraintKind.Text And nTextConstraints > 1 Then
            'ToDo - accurate error text
            MessageBox.Show(String.Format("{0}: {1}", AE_Constants.Instance.Duplicate_name, CType(c, Constraint_Text).TypeOfTextConstraint.ToString()), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim tp As TabPage = New TabPage(c.ConstraintKindString)
            TabConstraints.TabPages.Add(tp)
            TabConstraints.SelectedTab = tp

            If c.Kind <> ConstraintKind.Any Then
                Dim cc As ConstraintControl = ConstraintControl.CreateConstraintControl(c.Kind, mFileManager)
                tp.Controls.Add(cc)

                If c.Kind = ConstraintKind.Text Then
                    nTextConstraints = nTextConstraints + 1
                End If

                cc.ShowConstraint(mIsState, c)
                cc.Dock = DockStyle.Fill
                RestrictTextControls()
            End If
        End If
    End Sub

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        mIsState = isState

        For i As Integer = 0 To Constraint.Constraints.Count - 1
            AddConstraintControl(Constraint.Constraints.Item(i))
        Next
    End Sub

    Private nTextConstraints As Integer

    Private Sub AddConstraint(ByVal newConstraint As Constraint)
        Dim slot As Constraint_Slot = TryCast(newConstraint, Constraint_Slot)

        If Not slot Is Nothing Then
            slot.RM_ClassType = StructureType.Element
        End If

        Constraint.Constraints.Add(newConstraint)
        AddConstraintControl(newConstraint)
        mFileManager.FileEdited = True
    End Sub

    Protected Sub RestrictTextControls()
        Dim isFreeText As Boolean = False

        For Each tp As TabPage In TabConstraints.TabPages
            If tp.Controls.Count > 0 Then
                Dim cc As TextConstraintControl = TryCast(tp.Controls(0), TextConstraintControl)

                If Not cc Is Nothing Then
                    If isFreeText And cc.radioText.Checked Then
                        cc.radioTerminology.Checked = True
                    End If

                    isFreeText = cc.radioText.Checked
                    cc.radioText.Enabled = isFreeText Or nTextConstraints = 1
                    cc.radioInternal.Enabled = Not isFreeText Or nTextConstraints = 1
                    cc.radioTerminology.Enabled = Not isFreeText Or nTextConstraints = 1
                End If
            End If
        Next
    End Sub

    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        Dim menu As New ConstraintContextMenu(AddressOf AddConstraint, mFileManager)
        menu.ShowMenuItem(ConstraintKind.Multiple, False)
        menu.ShowMenuItem(ConstraintKind.Text, nTextConstraints < 2)

        For Each c As Constraint In Constraint.Constraints
            If c.Kind <> ConstraintKind.Text Then
                menu.ShowMenuItem(c.Kind, False)
            End If
        Next

        menu.Show(AddButton, New Point(5, 5))
    End Sub

    Private Sub RemoveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveButton.Click
        If MessageBox.Show(AE_Constants.Instance.Remove & TabConstraints.SelectedTab.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            Dim i As Integer = TabConstraints.SelectedIndex
            TabConstraints.TabPages.Remove(TabConstraints.SelectedTab)

            If Constraint.Constraints.Item(i).Kind = ConstraintKind.Text Then
                nTextConstraints = nTextConstraints - 1
                RestrictTextControls()
            End If

            Constraint.Constraints.RemoveAt(i)
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
