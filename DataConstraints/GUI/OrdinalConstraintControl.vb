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

Public Class OrdinalConstraintControl : Inherits ConstraintControl

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

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.LabelOrdinal.Text = Filemanager.GetOpenEhrTerm(156, Me.LabelOrdinal.Text)
            Me.butSetAssumedOrdinal.Text = Filemanager.GetOpenEhrTerm(153, Me.butSetAssumedOrdinal.Text)
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtAssumedOrdinal As System.Windows.Forms.TextBox
    Friend WithEvents butSetAssumedOrdinal As System.Windows.Forms.Button
    Friend WithEvents dgOrdinal As System.Windows.Forms.DataGrid
    Friend WithEvents OrdinalStyle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents GridOrdinalColumnOrdinal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GridOrdinalColumnText As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents LabelOrdinal As System.Windows.Forms.Label
    Friend WithEvents ContextMenuClearText As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuClearText As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuListAllowableValues As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPasteAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCancelCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemAddExisting As System.Windows.Forms.MenuItem
    Friend WithEvents GridOrdinalColumnDescription As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtAssumedOrdinal = New System.Windows.Forms.TextBox
        Me.ContextMenuClearText = New System.Windows.Forms.ContextMenu
        Me.MenuClearText = New System.Windows.Forms.MenuItem
        Me.butSetAssumedOrdinal = New System.Windows.Forms.Button
        Me.dgOrdinal = New System.Windows.Forms.DataGrid
        Me.ContextMenuListAllowableValues = New System.Windows.Forms.ContextMenu
        Me.MenuItemCopyAll = New System.Windows.Forms.MenuItem
        Me.MenuItemPasteAll = New System.Windows.Forms.MenuItem
        Me.MenuItemCancelCopy = New System.Windows.Forms.MenuItem
        Me.MenuItemAddExisting = New System.Windows.Forms.MenuItem
        Me.OrdinalStyle = New System.Windows.Forms.DataGridTableStyle
        Me.GridOrdinalColumnOrdinal = New System.Windows.Forms.DataGridTextBoxColumn
        Me.GridOrdinalColumnText = New System.Windows.Forms.DataGridTextBoxColumn
        Me.GridOrdinalColumnDescription = New System.Windows.Forms.DataGridTextBoxColumn
        Me.LabelOrdinal = New System.Windows.Forms.Label
        CType(Me.dgOrdinal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtAssumedOrdinal
        '
        Me.txtAssumedOrdinal.ContextMenu = Me.ContextMenuClearText
        Me.txtAssumedOrdinal.Location = New System.Drawing.Point(192, 224)
        Me.txtAssumedOrdinal.Name = "txtAssumedOrdinal"
        Me.txtAssumedOrdinal.ReadOnly = True
        Me.txtAssumedOrdinal.Size = New System.Drawing.Size(176, 20)
        Me.txtAssumedOrdinal.TabIndex = 40
        Me.txtAssumedOrdinal.TabStop = False
        Me.txtAssumedOrdinal.Text = "(none)"
        Me.txtAssumedOrdinal.Visible = False
        '
        'ContextMenuClearText
        '
        Me.ContextMenuClearText.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuClearText})
        '
        'MenuClearText
        '
        Me.MenuClearText.Index = 0
        Me.MenuClearText.Text = "Clear"
        '
        'butSetAssumedOrdinal
        '
        Me.butSetAssumedOrdinal.Location = New System.Drawing.Point(24, 224)
        Me.butSetAssumedOrdinal.Name = "butSetAssumedOrdinal"
        Me.butSetAssumedOrdinal.Size = New System.Drawing.Size(152, 24)
        Me.butSetAssumedOrdinal.TabIndex = 39
        Me.butSetAssumedOrdinal.Text = "Set assumed value"
        Me.butSetAssumedOrdinal.Visible = False
        '
        'dgOrdinal
        '
        Me.dgOrdinal.AllowSorting = False
        Me.dgOrdinal.CaptionBackColor = System.Drawing.Color.CornflowerBlue
        Me.dgOrdinal.CaptionVisible = False
        Me.dgOrdinal.ContextMenu = Me.ContextMenuListAllowableValues
        Me.dgOrdinal.DataMember = ""
        Me.dgOrdinal.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgOrdinal.Location = New System.Drawing.Point(24, 56)
        Me.dgOrdinal.Name = "dgOrdinal"
        Me.dgOrdinal.Size = New System.Drawing.Size(344, 152)
        Me.dgOrdinal.TabIndex = 3
        Me.dgOrdinal.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.OrdinalStyle})
        '
        'ContextMenuListAllowableValues
        '
        Me.ContextMenuListAllowableValues.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyAll, Me.MenuItemPasteAll, Me.MenuItemCancelCopy, Me.MenuItemAddExisting})
        '
        'MenuItemCopyAll
        '
        Me.MenuItemCopyAll.Index = 0
        Me.MenuItemCopyAll.Text = "Copy all"
        '
        'MenuItemPasteAll
        '
        Me.MenuItemPasteAll.Enabled = False
        Me.MenuItemPasteAll.Index = 1
        Me.MenuItemPasteAll.Text = "Paste all"
        '
        'MenuItemCancelCopy
        '
        Me.MenuItemCancelCopy.Index = 2
        Me.MenuItemCancelCopy.Text = "Cancel copy"
        Me.MenuItemCancelCopy.Visible = False
        '
        'MenuItemAddExisting
        '
        Me.MenuItemAddExisting.Index = 3
        Me.MenuItemAddExisting.Text = "Add existing code(s)"
        '
        'OrdinalStyle
        '
        Me.OrdinalStyle.AllowSorting = False
        Me.OrdinalStyle.DataGrid = Me.dgOrdinal
        Me.OrdinalStyle.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.GridOrdinalColumnOrdinal, Me.GridOrdinalColumnText, Me.GridOrdinalColumnDescription})
        Me.OrdinalStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.OrdinalStyle.MappingName = "OrdinalTable"
        '
        'GridOrdinalColumnOrdinal
        '
        Me.GridOrdinalColumnOrdinal.Format = ""
        Me.GridOrdinalColumnOrdinal.FormatInfo = Nothing
        Me.GridOrdinalColumnOrdinal.HeaderText = "Ordinal"
        Me.GridOrdinalColumnOrdinal.MappingName = "Ordinal"
        Me.GridOrdinalColumnOrdinal.Width = 32
        '
        'GridOrdinalColumnText
        '
        Me.GridOrdinalColumnText.Format = ""
        Me.GridOrdinalColumnText.FormatInfo = Nothing
        Me.GridOrdinalColumnText.HeaderText = "Text"
        Me.GridOrdinalColumnText.MappingName = "OrdinalText"
        Me.GridOrdinalColumnText.Width = 240
        '
        'GridOrdinalColumnDescription
        '
        Me.GridOrdinalColumnDescription.Format = ""
        Me.GridOrdinalColumnDescription.FormatInfo = Nothing
        Me.GridOrdinalColumnDescription.HeaderText = "Description"
        Me.GridOrdinalColumnDescription.MappingName = "OrdinalDescription"
        Me.GridOrdinalColumnDescription.Width = 300
        '
        'LabelOrdinal
        '
        Me.LabelOrdinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelOrdinal.Location = New System.Drawing.Point(8, 16)
        Me.LabelOrdinal.Name = "LabelOrdinal"
        Me.LabelOrdinal.Size = New System.Drawing.Size(96, 24)
        Me.LabelOrdinal.TabIndex = 37
        Me.LabelOrdinal.Text = "Ordinal"
        '
        'OrdinalConstraintControl
        '
        Me.Controls.Add(Me.txtAssumedOrdinal)
        Me.Controls.Add(Me.butSetAssumedOrdinal)
        Me.Controls.Add(Me.dgOrdinal)
        Me.Controls.Add(Me.LabelOrdinal)
        Me.Name = "OrdinalConstraintControl"
        Me.Size = New System.Drawing.Size(376, 256)
        CType(Me.dgOrdinal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mTempConstraint As Constraint_Ordinal
    Private WithEvents mOrdinalConstraint As Constraint_Ordinal

    Private Shadows ReadOnly Property Constraint() As Constraint_Ordinal
        Get
            Return mOrdinalConstraint
        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean, _
            ByVal aArchetypeElement As ArchetypeElement)

        Debug.Assert(TypeOf aArchetypeElement.Constraint Is Constraint_Ordinal)
        mOrdinalConstraint = CType(aArchetypeElement.Constraint, Constraint_Ordinal)

        ' set constraint values on control
        If IsState Then
            Me.butSetAssumedOrdinal.Visible = True
            Me.txtAssumedOrdinal.Visible = True
        End If

        InitialiseOrdinals()

        If (Not OceanArchetypeEditor.Instance.TempConstraint Is Nothing) AndAlso (OceanArchetypeEditor.Instance.TempConstraint.Type = ConstraintType.Ordinal) Then
            Me.MenuItemPasteAll.Enabled = True
            Me.MenuItemCancelCopy.Visible = True
            Me.MenuItemCopyAll.Enabled = False
        End If

    End Sub

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean, ByVal c As Constraint)

        Debug.Assert(TypeOf c Is Constraint_Ordinal)

        mOrdinalConstraint = CType(c, Constraint_Ordinal)

        ' set constraint values on control
        If IsState Then
            Me.butSetAssumedOrdinal.Visible = True
            Me.txtAssumedOrdinal.Visible = True
        End If

        InitialiseOrdinals()

    End Sub


    Private Sub butSetAssumedOrdinal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles butSetAssumedOrdinal.Click

        If Me.dgOrdinal.CurrentRowIndex > -1 Then
            Dim ordinal As Integer = CInt(Me.dgOrdinal.Item(Me.dgOrdinal.CurrentRowIndex, 0))
            Me.txtAssumedOrdinal.Text = CStr(Me.dgOrdinal.Item(Me.dgOrdinal.CurrentRowIndex, 1))
            Me.Constraint.AssumedValue = ordinal
            mFileManager.FileEdited = True
        End If

    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClearText.Click

        If MyBase.IsLoading Then Return

        Me.Constraint.HasAssumedValue = False
        Me.txtAssumedOrdinal.Text = "(none)"

        mFileManager.FileEdited = True

    End Sub

    Private Sub MenuItemCopyAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyAll.Click

        OceanArchetypeEditor.Instance.TempConstraint = Me.Constraint

    End Sub

    Private Sub MenuItemAddExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAddExisting.Click
        AddCodeToOrdinal()
    End Sub

    Private Sub AddCodeToOrdinal()
        If MyBase.IsLoading Then Return

        Dim s As String() = OceanArchetypeEditor.Instance.ChooseInternal(mFileManager)
        If s Is Nothing Then Return

        For i As Integer = 0 To s.Length - 1
            Dim newOrdinal As OrdinalValue = Me.Constraint.OrdinalValues.NewOrdinal

            Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(s(i))

            ' order is important as will create a new term unless the term code is added first
            newOrdinal.InternalCode = s(i)
            newOrdinal.Text = aTerm.Text
            newOrdinal.Ordinal = Me.Constraint.NextFreeOrdinalValue
            Me.Constraint.OrdinalValues.Add(newOrdinal)
        Next

        mFileManager.FileEdited = True

    End Sub

    Private Sub MenuItemPasteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPasteAll.Click

        Me.Constraint.ClearOrdinalValues()

        Debug.Assert(OceanArchetypeEditor.Instance.TempConstraint.Type = ConstraintType.Ordinal)

        '' default key(0) is set to ae.NodeId
        For Each ov As OrdinalValue In CType(OceanArchetypeEditor.Instance.TempConstraint, Constraint_Ordinal).OrdinalValues
            Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            Dim newOrdinal As OrdinalValue = Me.Constraint.OrdinalValues.NewOrdinal

            newOrdinal.Ordinal = ov.Ordinal
            newOrdinal.Text = aTerm.Text
            newOrdinal.InternalCode = ov.InternalCode

            Me.Constraint.OrdinalValues.Add(newOrdinal)

        Next

        MyBase.Constraint = Me.Constraint

        Me.MenuItemPasteAll.Enabled = False
        Me.MenuItemCancelCopy.Visible = False
        Me.MenuItemCopyAll.Enabled = True
        OceanArchetypeEditor.Instance.TempConstraint = Nothing

        mFileManager.FileEdited = True

    End Sub

    Private Sub InitialiseOrdinals()

        If (Not Me.Constraint.IsInitialised) Or (Me.Constraint.Language <> mFileManager.OntologyManager.LanguageCode) Then
            Me.Constraint.BeginLoading()

            'set the language as need to reinitialise if change language
            Me.Constraint.Language = mFileManager.OntologyManager.LanguageCode

            For Each ov As OrdinalValue In Me.Constraint.OrdinalValues
                Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(ov.InternalCode)

                ov.Text = aTerm.Text
                ov.Description = aTerm.Description
                If (Not Me.Constraint.AssumedValue Is Nothing) AndAlso (ov.Ordinal = CInt(Me.Constraint.AssumedValue)) Then
                    Me.txtAssumedOrdinal.Text = ov.Text
                End If
            Next
            Me.Constraint.EndLoading()
        End If

        Me.dgOrdinal.SetDataBinding(Me.Constraint.OrdinalValues.DefaultView, "")

    End Sub

    Private Sub MenuItemCancelCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCancelCopy.Click
        Me.MenuItemPasteAll.Enabled = False
        Me.MenuItemCancelCopy.Visible = False
        Me.MenuItemCopyAll.Enabled = True
        mTempConstraint = Nothing
    End Sub

    Private Sub mOrdinalConstraint_AssumedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mOrdinalConstraint.AssumedValueChanged
        If Not Me.Constraint.HasAssumedValue Then
            Me.txtAssumedOrdinal.Text = "(none)"
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
'The Original Code is OrdinalConstraintControl.vb.
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