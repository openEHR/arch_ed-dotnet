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

        If Main.Instance.DefaultLanguageCode <> "en" Then
            LabelOrdinal.Text = Filemanager.GetOpenEhrTerm(156, LabelOrdinal.Text)
            AssumedValueCheckBox.Text = Filemanager.GetOpenEhrTerm(158, AssumedValueCheckBox.Text)
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dgOrdinal As System.Windows.Forms.DataGrid
    Friend WithEvents OrdinalStyle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents GridOrdinalColumnOrdinal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GridOrdinalColumnText As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents LabelOrdinal As System.Windows.Forms.Label
    Friend WithEvents ContextMenuListAllowableValues As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPasteAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCancelCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemAddExisting As System.Windows.Forms.MenuItem
    Friend WithEvents AssumedValueCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AssumedValueComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents GridOrdinalColumnDescription As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
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
        Me.AssumedValueCheckBox = New System.Windows.Forms.CheckBox
        Me.AssumedValueComboBox = New System.Windows.Forms.ComboBox
        CType(Me.dgOrdinal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgOrdinal
        '
        Me.dgOrdinal.AllowSorting = False
        Me.dgOrdinal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOrdinal.CaptionBackColor = System.Drawing.Color.CornflowerBlue
        Me.dgOrdinal.CaptionVisible = False
        Me.dgOrdinal.ContextMenu = Me.ContextMenuListAllowableValues
        Me.dgOrdinal.DataMember = ""
        Me.dgOrdinal.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgOrdinal.Location = New System.Drawing.Point(11, 56)
        Me.dgOrdinal.Name = "dgOrdinal"
        Me.dgOrdinal.Size = New System.Drawing.Size(357, 152)
        Me.dgOrdinal.TabIndex = 2
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
        Me.LabelOrdinal.TabIndex = 1
        Me.LabelOrdinal.Text = "Ordinal"
        '
        'AssumedValueCheckBox
        '
        Me.AssumedValueCheckBox.Location = New System.Drawing.Point(15, 222)
        Me.AssumedValueCheckBox.Name = "AssumedValueCheckBox"
        Me.AssumedValueCheckBox.Size = New System.Drawing.Size(184, 24)
        Me.AssumedValueCheckBox.TabIndex = 3
        Me.AssumedValueCheckBox.Text = "Assumed value"
        '
        'AssumedValueComboBox
        '
        Me.AssumedValueComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AssumedValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AssumedValueComboBox.FormattingEnabled = True
        Me.AssumedValueComboBox.Location = New System.Drawing.Point(202, 223)
        Me.AssumedValueComboBox.Name = "AssumedValueComboBox"
        Me.AssumedValueComboBox.Size = New System.Drawing.Size(162, 21)
        Me.AssumedValueComboBox.TabIndex = 4
        Me.AssumedValueComboBox.Visible = False
        '
        'OrdinalConstraintControl
        '
        Me.Controls.Add(Me.AssumedValueComboBox)
        Me.Controls.Add(Me.AssumedValueCheckBox)
        Me.Controls.Add(Me.dgOrdinal)
        Me.Controls.Add(Me.LabelOrdinal)
        Me.Name = "OrdinalConstraintControl"
        Me.Size = New System.Drawing.Size(376, 256)
        CType(Me.dgOrdinal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected ReadOnly Property Constraint() As Constraint_Ordinal
        Get
            Return CType(mConstraint, Constraint_Ordinal)
        End Get
    End Property

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        AssumedValueCheckBox.Checked = Constraint.HasAssumedValue
        InitialiseOrdinals()
        dgOrdinal.SetDataBinding(Constraint.OrdinalValues.DefaultView, "")

        Dim view As DataView = New DataView(Constraint.OrdinalValues)
        view.Sort = "Ordinal"
        AddHandler view.ListChanged, AddressOf AssumedValueComboBox_SelectedIndexChanged
        AssumedValueComboBox.DataSource = view

        For Each row As DataRowView In view
            If Constraint.AssumedValue IsNot Nothing AndAlso New OrdinalValue(row.Row).Ordinal = CInt(Constraint.AssumedValue) Then
                AssumedValueComboBox.SelectedItem = row
            End If
        Next

        If Main.Instance.TempConstraint IsNot Nothing AndAlso Main.Instance.TempConstraint.Kind = ConstraintKind.Ordinal Then
            MenuItemPasteAll.Enabled = True
            MenuItemCancelCopy.Visible = True
            MenuItemCopyAll.Enabled = False
        End If
    End Sub

    Protected Sub InitialiseOrdinals()
        If Not Constraint.IsInitialised Or Constraint.Language <> mFileManager.OntologyManager.LanguageCode Then
            Constraint.BeginLoading()

            'set the language as need to reinitialise if change language
            Constraint.Language = mFileManager.OntologyManager.LanguageCode

            For Each ov As OrdinalValue In Constraint.OrdinalValues
                Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
                ov.Text = term.Text
                ov.Description = term.Description
            Next

            Constraint.EndLoading()
        End If
    End Sub

    Private Sub AssumedValueComboBox_Format(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ListControlConvertEventArgs) Handles AssumedValueComboBox.Format
        e.Value = New OrdinalValue(CType(e.ListItem, DataRowView).Row).ToString
    End Sub

    Private Sub AssumedValueCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssumedValueCheckBox.CheckedChanged
        AssumedValueComboBox.Visible = AssumedValueCheckBox.Checked

        If Not IsLoading Then
            Constraint.HasAssumedValue = AssumedValueCheckBox.Checked
            AssumedValueComboBox_SelectedIndexChanged(sender, e)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub AssumedValueComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssumedValueComboBox.SelectedIndexChanged
        If Not IsLoading And Constraint.HasAssumedValue Then
            Dim row As DataRowView = TryCast(AssumedValueComboBox.SelectedItem, DataRowView)

            If row IsNot Nothing AndAlso row.Row.RowState <> DataRowState.Detached Then
                Constraint.AssumedValue = New OrdinalValue(row.Row).Ordinal
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub MenuItemCopyAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyAll.Click
        Main.Instance.TempConstraint = Constraint
    End Sub

    Private Sub MenuItemAddExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAddExisting.Click
        If Not IsLoading Then
            Dim codes As String() = Main.Instance.ChooseInternal(mFileManager, Constraint.InternalCodes)

            If codes IsNot Nothing Then
                For Each code As String In codes
                    Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(code)
                    Dim newOrdinal As OrdinalValue = Constraint.OrdinalValues.NewOrdinal
                    newOrdinal.InternalCode = code ' Must be done first to avoid creating a new term
                    newOrdinal.Text = term.Text
                    newOrdinal.Ordinal = Constraint.NextFreeOrdinalValue
                    Constraint.OrdinalValues.Add(newOrdinal)
                Next

                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub MenuItemPasteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPasteAll.Click
        Constraint.ClearOrdinalValues()

        Debug.Assert(Main.Instance.TempConstraint.Kind = ConstraintKind.Ordinal)

        ' default key(0) is set to ae.NodeId
        For Each ov As OrdinalValue In CType(Main.Instance.TempConstraint, Constraint_Ordinal).OrdinalValues
            Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(ov.InternalCode)
            Dim newOrdinal As OrdinalValue = Constraint.OrdinalValues.NewOrdinal
            newOrdinal.Ordinal = ov.Ordinal
            newOrdinal.Text = term.Text
            newOrdinal.InternalCode = ov.InternalCode
            Constraint.OrdinalValues.Add(newOrdinal)
        Next

        MenuItemPasteAll.Enabled = False
        MenuItemCancelCopy.Visible = False
        MenuItemCopyAll.Enabled = True
        Main.Instance.TempConstraint = Nothing

        mFileManager.FileEdited = True
    End Sub

    Private Sub MenuItemCancelCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCancelCopy.Click
        MenuItemPasteAll.Enabled = False
        MenuItemCancelCopy.Visible = False
        MenuItemCopyAll.Enabled = True
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
