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

Public Class TextConstraintControl : Inherits ConstraintControl

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
        mAllowedValuesDataView = New DataView(mFileManager.OntologyManager.TermDefinitionTable)
        ConstraintBindingsGrid.BindTables(mFileManager.OntologyManager)

        If Main.Instance.DefaultLanguageCode <> "en" Then
            radioText.Text = Filemanager.GetOpenEhrTerm(444, radioText.Text)
            radioInternal.Text = Filemanager.GetOpenEhrTerm(150, radioInternal.Text)
            radioTerminology.Text = Filemanager.GetOpenEhrTerm(47, radioTerminology.Text)
            AssumedValueCheckBox.Text = Filemanager.GetOpenEhrTerm(158, AssumedValueCheckBox.Text)
            ToolTip1.SetToolTip(AddExistingTermButton, Filemanager.GetOpenEhrTerm(602, "Add existing term"))
            ToolTip1.SetToolTip(NewTermButton, Filemanager.GetOpenEhrTerm(603, "Add new term"))
            ToolTip1.SetToolTip(RemoveTermButton, Filemanager.GetOpenEhrTerm(152, "Remove term"))
            TermConstraintLabel.Text = Filemanager.GetOpenEhrTerm(87, TermConstraintLabel.Text)
            TermConstraintDescriptionLabel.Text = Filemanager.GetOpenEhrTerm(113, TermConstraintDescriptionLabel.Text)
            ConstraintBindingsGrid.TranslateGui()
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents radioInternal As System.Windows.Forms.RadioButton
    Friend WithEvents radioText As System.Windows.Forms.RadioButton
    Friend WithEvents radioTerminology As System.Windows.Forms.RadioButton
    Friend WithEvents TermConstraintDescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TermConstraintTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuListAllowableValues As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPasteAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCancelCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemAddExisting As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemEdit As System.Windows.Forms.MenuItem
    Friend WithEvents TermConstraintDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MenuItemRemove As System.Windows.Forms.MenuItem
    Friend WithEvents menuCopyTerm As System.Windows.Forms.MenuItem
    Friend WithEvents listAllowableValues As System.Windows.Forms.ListBox
    Friend WithEvents AddExistingTermButton As System.Windows.Forms.Button
    Friend WithEvents RemoveTermButton As System.Windows.Forms.Button
    Friend WithEvents NewTermButton As System.Windows.Forms.Button
    Friend WithEvents MoveUpButton As System.Windows.Forms.Button
    Friend WithEvents MoveDownButton As System.Windows.Forms.Button
    Friend WithEvents CopyListButton As System.Windows.Forms.Button
    Friend WithEvents PasteListButton As System.Windows.Forms.Button
    Friend WithEvents gbAllowableValues As System.Windows.Forms.GroupBox
    Friend WithEvents ListViewSelected As System.Windows.Forms.ListView
    Friend WithEvents Code As System.Windows.Forms.ColumnHeader
    Friend WithEvents Description As System.Windows.Forms.ColumnHeader
    Friend WithEvents ConstraintBindingsGrid As ConstraintBindingsControl
    Friend WithEvents AssumedValueComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AssumedValueCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TermConstraintLabel As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TextConstraintControl))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuListAllowableValues = New System.Windows.Forms.ContextMenu
        Me.MenuItemEdit = New System.Windows.Forms.MenuItem
        Me.MenuItemRemove = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyAll = New System.Windows.Forms.MenuItem
        Me.MenuItemPasteAll = New System.Windows.Forms.MenuItem
        Me.MenuItemCancelCopy = New System.Windows.Forms.MenuItem
        Me.MenuItemAddExisting = New System.Windows.Forms.MenuItem
        Me.menuCopyTerm = New System.Windows.Forms.MenuItem
        Me.TermConstraintDescriptionLabel = New System.Windows.Forms.Label
        Me.TermConstraintLabel = New System.Windows.Forms.Label
        Me.radioInternal = New System.Windows.Forms.RadioButton
        Me.radioText = New System.Windows.Forms.RadioButton
        Me.radioTerminology = New System.Windows.Forms.RadioButton
        Me.TermConstraintDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.TermConstraintTextBox = New System.Windows.Forms.TextBox
        Me.listAllowableValues = New System.Windows.Forms.ListBox
        Me.AddExistingTermButton = New System.Windows.Forms.Button
        Me.RemoveTermButton = New System.Windows.Forms.Button
        Me.NewTermButton = New System.Windows.Forms.Button
        Me.MoveUpButton = New System.Windows.Forms.Button
        Me.MoveDownButton = New System.Windows.Forms.Button
        Me.CopyListButton = New System.Windows.Forms.Button
        Me.PasteListButton = New System.Windows.Forms.Button
        Me.gbAllowableValues = New System.Windows.Forms.GroupBox
        Me.AssumedValueComboBox = New System.Windows.Forms.ComboBox
        Me.AssumedValueCheckBox = New System.Windows.Forms.CheckBox
        Me.ListViewSelected = New System.Windows.Forms.ListView
        Me.Code = New System.Windows.Forms.ColumnHeader
        Me.Description = New System.Windows.Forms.ColumnHeader
        Me.ConstraintBindingsGrid = New ConstraintBindingsControl
        Me.gbAllowableValues.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia
        Me.ImageList1.Images.SetKeyName(0, "arrowdown_blue16.bmp")
        Me.ImageList1.Images.SetKeyName(1, "arrowup_blue16.bmp")
        Me.ImageList1.Images.SetKeyName(2, "copy16.bmp")
        Me.ImageList1.Images.SetKeyName(3, "paste16.bmp")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        '
        'ContextMenuListAllowableValues
        '
        Me.ContextMenuListAllowableValues.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemEdit, Me.MenuItemRemove, Me.MenuItemCopyAll, Me.MenuItemPasteAll, Me.MenuItemCancelCopy, Me.MenuItemAddExisting, Me.menuCopyTerm})
        '
        'MenuItemEdit
        '
        Me.MenuItemEdit.Index = 0
        Me.MenuItemEdit.Text = "Edit"
        Me.MenuItemEdit.Visible = False
        '
        'MenuItemRemove
        '
        Me.MenuItemRemove.Index = 1
        Me.MenuItemRemove.Text = "Remove "
        '
        'MenuItemCopyAll
        '
        Me.MenuItemCopyAll.Index = 2
        Me.MenuItemCopyAll.Text = "Copy all"
        Me.MenuItemCopyAll.Visible = False
        '
        'MenuItemPasteAll
        '
        Me.MenuItemPasteAll.Enabled = False
        Me.MenuItemPasteAll.Index = 3
        Me.MenuItemPasteAll.Text = "Paste all"
        '
        'MenuItemCancelCopy
        '
        Me.MenuItemCancelCopy.Index = 4
        Me.MenuItemCancelCopy.Text = "Cancel copy"
        Me.MenuItemCancelCopy.Visible = False
        '
        'MenuItemAddExisting
        '
        Me.MenuItemAddExisting.Index = 5
        Me.MenuItemAddExisting.Text = "Add existing code(s)"
        '
        'menuCopyTerm
        '
        Me.menuCopyTerm.Index = 6
        Me.menuCopyTerm.Text = "Copy Term to clipboard"
        '
        'TermConstraintDescriptionLabel
        '
        Me.TermConstraintDescriptionLabel.Location = New System.Drawing.Point(16, 110)
        Me.TermConstraintDescriptionLabel.Name = "TermConstraintDescriptionLabel"
        Me.TermConstraintDescriptionLabel.Size = New System.Drawing.Size(88, 24)
        Me.TermConstraintDescriptionLabel.TabIndex = 36
        Me.TermConstraintDescriptionLabel.Text = "Description"
        Me.TermConstraintDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TermConstraintLabel
        '
        Me.TermConstraintLabel.Location = New System.Drawing.Point(16, 78)
        Me.TermConstraintLabel.Name = "TermConstraintLabel"
        Me.TermConstraintLabel.Size = New System.Drawing.Size(88, 24)
        Me.TermConstraintLabel.TabIndex = 34
        Me.TermConstraintLabel.Text = "Constraint"
        Me.TermConstraintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'radioInternal
        '
        Me.radioInternal.Location = New System.Drawing.Point(148, 6)
        Me.radioInternal.Name = "radioInternal"
        Me.radioInternal.Size = New System.Drawing.Size(120, 56)
        Me.radioInternal.TabIndex = 32
        Me.radioInternal.Text = "Internal codes"
        '
        'radioText
        '
        Me.radioText.Location = New System.Drawing.Point(16, 6)
        Me.radioText.Name = "radioText"
        Me.radioText.Size = New System.Drawing.Size(103, 56)
        Me.radioText.TabIndex = 31
        Me.radioText.Text = "Free text or coded"
        '
        'radioTerminology
        '
        Me.radioTerminology.Location = New System.Drawing.Point(275, 18)
        Me.radioTerminology.Name = "radioTerminology"
        Me.radioTerminology.Size = New System.Drawing.Size(112, 32)
        Me.radioTerminology.TabIndex = 33
        Me.radioTerminology.Text = "Terminology"
        '
        'TermConstraintDescriptionTextBox
        '
        Me.TermConstraintDescriptionTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TermConstraintDescriptionTextBox.Location = New System.Drawing.Point(108, 110)
        Me.TermConstraintDescriptionTextBox.Multiline = True
        Me.TermConstraintDescriptionTextBox.Name = "TermConstraintDescriptionTextBox"
        Me.TermConstraintDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TermConstraintDescriptionTextBox.Size = New System.Drawing.Size(280, 110)
        Me.TermConstraintDescriptionTextBox.TabIndex = 37
        '
        'TermConstraintTextBox
        '
        Me.TermConstraintTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TermConstraintTextBox.Location = New System.Drawing.Point(108, 80)
        Me.TermConstraintTextBox.Name = "TermConstraintTextBox"
        Me.TermConstraintTextBox.Size = New System.Drawing.Size(280, 20)
        Me.TermConstraintTextBox.TabIndex = 35
        '
        'listAllowableValues
        '
        Me.listAllowableValues.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listAllowableValues.ContextMenu = Me.ContextMenuListAllowableValues
        Me.listAllowableValues.DisplayMember = "Text"
        Me.listAllowableValues.Location = New System.Drawing.Point(45, 19)
        Me.listAllowableValues.Name = "listAllowableValues"
        Me.listAllowableValues.Size = New System.Drawing.Size(318, 199)
        Me.listAllowableValues.TabIndex = 7
        '
        'AddExistingTermButton
        '
        Me.AddExistingTermButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddExistingTermButton.Location = New System.Drawing.Point(15, 88)
        Me.AddExistingTermButton.Name = "AddExistingTermButton"
        Me.AddExistingTermButton.Size = New System.Drawing.Size(24, 24)
        Me.AddExistingTermButton.TabIndex = 2
        Me.AddExistingTermButton.Text = "..."
        Me.ToolTip1.SetToolTip(Me.AddExistingTermButton, "Add a term that is already defined")
        '
        'RemoveTermButton
        '
        Me.RemoveTermButton.Image = CType(resources.GetObject("RemoveTermButton.Image"), System.Drawing.Image)
        Me.RemoveTermButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.RemoveTermButton.Location = New System.Drawing.Point(15, 56)
        Me.RemoveTermButton.Name = "RemoveTermButton"
        Me.RemoveTermButton.Size = New System.Drawing.Size(24, 24)
        Me.RemoveTermButton.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.RemoveTermButton, "Remove term")
        '
        'NewTermButton
        '
        Me.NewTermButton.Image = CType(resources.GetObject("NewTermButton.Image"), System.Drawing.Image)
        Me.NewTermButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.NewTermButton.Location = New System.Drawing.Point(15, 24)
        Me.NewTermButton.Name = "NewTermButton"
        Me.NewTermButton.Size = New System.Drawing.Size(24, 24)
        Me.NewTermButton.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.NewTermButton, "Add a new term")
        '
        'MoveUpButton
        '
        Me.MoveUpButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MoveUpButton.ImageIndex = 1
        Me.MoveUpButton.ImageList = Me.ImageList1
        Me.MoveUpButton.Location = New System.Drawing.Point(15, 124)
        Me.MoveUpButton.Name = "MoveUpButton"
        Me.MoveUpButton.Size = New System.Drawing.Size(24, 24)
        Me.MoveUpButton.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.MoveUpButton, "Move the highlighted item up the list")
        '
        'MoveDownButton
        '
        Me.MoveDownButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MoveDownButton.ImageKey = "arrowdown_blue16.bmp"
        Me.MoveDownButton.ImageList = Me.ImageList1
        Me.MoveDownButton.Location = New System.Drawing.Point(15, 154)
        Me.MoveDownButton.Name = "MoveDownButton"
        Me.MoveDownButton.Size = New System.Drawing.Size(24, 24)
        Me.MoveDownButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.MoveDownButton, "Move the highlighted item down the list." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'CopyListButton
        '
        Me.CopyListButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CopyListButton.ImageKey = "copy16.bmp"
        Me.CopyListButton.ImageList = Me.ImageList1
        Me.CopyListButton.Location = New System.Drawing.Point(15, 194)
        Me.CopyListButton.Name = "CopyListButton"
        Me.CopyListButton.Size = New System.Drawing.Size(24, 24)
        Me.CopyListButton.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.CopyListButton, "Copy the list to the clipboard")
        '
        'PasteListButton
        '
        Me.PasteListButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasteListButton.ImageKey = "paste16.bmp"
        Me.PasteListButton.ImageList = Me.ImageList1
        Me.PasteListButton.Location = New System.Drawing.Point(15, 224)
        Me.PasteListButton.Name = "PasteListButton"
        Me.PasteListButton.Size = New System.Drawing.Size(24, 24)
        Me.PasteListButton.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.PasteListButton, "Paste an external list from Clipboard")
        '
        'gbAllowableValues
        '
        Me.gbAllowableValues.BackColor = System.Drawing.Color.Transparent
        Me.gbAllowableValues.Controls.Add(Me.AssumedValueComboBox)
        Me.gbAllowableValues.Controls.Add(Me.AssumedValueCheckBox)
        Me.gbAllowableValues.Controls.Add(Me.ListViewSelected)
        Me.gbAllowableValues.Controls.Add(Me.PasteListButton)
        Me.gbAllowableValues.Controls.Add(Me.CopyListButton)
        Me.gbAllowableValues.Controls.Add(Me.MoveDownButton)
        Me.gbAllowableValues.Controls.Add(Me.MoveUpButton)
        Me.gbAllowableValues.Controls.Add(Me.NewTermButton)
        Me.gbAllowableValues.Controls.Add(Me.RemoveTermButton)
        Me.gbAllowableValues.Controls.Add(Me.AddExistingTermButton)
        Me.gbAllowableValues.Controls.Add(Me.listAllowableValues)
        Me.gbAllowableValues.Location = New System.Drawing.Point(16, 48)
        Me.gbAllowableValues.Name = "gbAllowableValues"
        Me.gbAllowableValues.Size = New System.Drawing.Size(372, 310)
        Me.gbAllowableValues.TabIndex = 50
        Me.gbAllowableValues.TabStop = False
        Me.gbAllowableValues.Visible = False
        '
        'AssumedValueComboBox
        '
        Me.AssumedValueComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AssumedValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AssumedValueComboBox.FormattingEnabled = True
        Me.AssumedValueComboBox.Location = New System.Drawing.Point(202, 281)
        Me.AssumedValueComboBox.Name = "AssumedValueComboBox"
        Me.AssumedValueComboBox.Size = New System.Drawing.Size(157, 21)
        Me.AssumedValueComboBox.TabIndex = 12
        Me.AssumedValueComboBox.Visible = False
        '
        'AssumedValueCheckBox
        '
        Me.AssumedValueCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AssumedValueCheckBox.Location = New System.Drawing.Point(15, 280)
        Me.AssumedValueCheckBox.Name = "AssumedValueCheckBox"
        Me.AssumedValueCheckBox.Size = New System.Drawing.Size(184, 24)
        Me.AssumedValueCheckBox.TabIndex = 11
        Me.AssumedValueCheckBox.Text = "Assumed value"
        '
        'ListViewSelected
        '
        Me.ListViewSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewSelected.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Code, Me.Description})
        Me.ListViewSelected.GridLines = True
        Me.ListViewSelected.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewSelected.LabelWrap = False
        Me.ListViewSelected.Location = New System.Drawing.Point(46, 224)
        Me.ListViewSelected.Name = "ListViewSelected"
        Me.ListViewSelected.ShowItemToolTips = True
        Me.ListViewSelected.Size = New System.Drawing.Size(317, 50)
        Me.ListViewSelected.TabIndex = 10
        Me.ListViewSelected.UseCompatibleStateImageBehavior = False
        Me.ListViewSelected.View = System.Windows.Forms.View.Details
        '
        'Code
        '
        Me.Code.Text = "Code"
        Me.Code.Width = 47
        '
        'Description
        '
        Me.Description.Text = "Description"
        Me.Description.Width = 263
        '
        'ConstraintBindingsGrid
        '
        Me.ConstraintBindingsGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConstraintBindingsGrid.Location = New System.Drawing.Point(8, 230)
        Me.ConstraintBindingsGrid.Name = "ConstraintBindingsGrid"
        Me.ConstraintBindingsGrid.Size = New System.Drawing.Size(390, 130)
        Me.ConstraintBindingsGrid.TabIndex = 38
        '
        'TextConstraintControl
        '
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.gbAllowableValues)
        Me.Controls.Add(Me.TermConstraintDescriptionLabel)
        Me.Controls.Add(Me.TermConstraintLabel)
        Me.Controls.Add(Me.radioInternal)
        Me.Controls.Add(Me.radioText)
        Me.Controls.Add(Me.radioTerminology)
        Me.Controls.Add(Me.TermConstraintDescriptionTextBox)
        Me.Controls.Add(Me.TermConstraintTextBox)
        Me.Controls.Add(Me.ConstraintBindingsGrid)
        Me.Name = "TextConstraintControl"
        Me.Size = New System.Drawing.Size(407, 364)
        Me.gbAllowableValues.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mAllowedValuesDataView As DataView
    Private mTempConstraint As Constraint_Text
    Private mConstraintTerm As RmTerm

    ' Supports correct ordering of internal list + reordering
    Private Class CodeTerm
        Private mCode As String
        Private mText As String

        Sub New(ByVal code As String, ByVal text As String)
            MyBase.New()
            mCode = code
            mText = text
        End Sub

        Public Property Code() As String
            Get
                Return mCode
            End Get
            Set(ByVal value As String)
                mCode = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return mText
            End Get
            Set(ByVal value As String)
                mText = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Public Property Constraint() As Constraint_Text
        Get
            Return CType(mConstraint, Constraint_Text)
        End Get
        Set(ByVal value As Constraint_Text)
            mConstraint = value
            SetControlValues(False)
            IsLoading = False
        End Set
    End Property

    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        SetControlValuesForTypeOfTextConstraint()
    End Sub

    Protected Sub SetControlValuesForTypeOfTextConstraint()
        Select Case Constraint.TypeOfTextConstraint
            Case TextConstraintType.Text
                radioText.Checked = True

            Case TextConstraintType.Internal
                radioInternal.Checked = True
                SetInternalCodedValues(0)

            Case TextConstraintType.Terminology
                radioTerminology.Checked = True
                mConstraintTerm = mFileManager.OntologyManager.GetTerm(Constraint.ConstraintCode)
                TermConstraintTextBox.Text = mConstraintTerm.Text
                TermConstraintDescriptionTextBox.Text = mConstraintTerm.Description
                ConstraintBindingsGrid.SelectConstraintCode(mConstraintTerm.Code)
        End Select

        RefreshButtons()
    End Sub

    Private Sub SetInternalCodedValues(ByVal index As Integer)
        SetAllowableValuesFilter()
        Dim codes As New ArrayList()

        For i As Integer = Constraint.AllowableValues.Codes.Count - 1 To 0 Step -1
            Dim code As String = Constraint.AllowableValues.Codes(i)
            Dim foundIndex As Integer = mAllowedValuesDataView.Find(code)

            If foundIndex >= 0 Then
                codes.Insert(0, New CodeTerm(code, CStr(mAllowedValuesDataView.Item(foundIndex)("Text"))))
            Else
                Constraint.AllowableValues.Codes.RemoveAt(i)
            End If
        Next

        listAllowableValues.DataSource = Nothing
        listAllowableValues.DisplayMember = "Text"
        listAllowableValues.ValueMember = "Code"
        listAllowableValues.DataSource = codes
        listAllowableValues.Refresh()
        listAllowableValues.SelectedIndex = Math.Min(index, listAllowableValues.Items.Count - 1)

        Dim assumedValue As Object = Constraint.AssumedValue
        AssumedValueCheckBox.Checked = Constraint.HasAssumedValue
        AssumedValueComboBox.DisplayMember = "Text"
        AssumedValueComboBox.ValueMember = "Code"
        AssumedValueComboBox.DataSource = codes
        AssumedValueComboBox.BindingContext = New BindingContext()  ' Allows AssumedValueComboBox.SelectedItem to be independent of listAllowableValues.SelectedItem

        If Constraint.AssumedValue IsNot Nothing Then
            AssumedValueComboBox.SelectedValue = assumedValue
        End If
    End Sub

    Private Sub SetAllowableValuesFilter()
        Dim filter As String = ""
        Dim prefix As String = "Code = "

        For Each code As String In Constraint.AllowableValues.Codes
            filter = filter + prefix + "'" + code + "'"
            prefix = " OR Code = "
        Next

        If filter <> "" Then
            filter = "(" + filter + ") AND "
        End If

        mAllowedValuesDataView.RowFilter = filter + "id = '" + mFileManager.OntologyManager.LanguageCode + "'"
        mAllowedValuesDataView.Sort = "Code"
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
            Constraint.AssumedValue = AssumedValueComboBox.SelectedValue
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub NewTermButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewTermButton.Click
        Dim s(1) As String
        s = Main.Instance.GetInput(Filemanager.GetOpenEhrTerm(603, "Add new term"), AE_Constants.Instance.Description, ParentForm)

        If s(0) <> "" Then
            Dim term As RmTerm = mFileManager.OntologyManager.AddTerm(s(0), s(1))
            Constraint.AllowableValues.Codes.Add(term.Code)
            SetInternalCodedValues(Constraint.AllowableValues.Codes.Count - 1)
            RefreshButtons()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub RemoveTermButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveTermButton.Click, MenuItemRemove.Click
        If mAllowedValuesDataView.Count > 0 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & listAllowableValues.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                If listAllowableValues.SelectedValue Is AssumedValueComboBox.SelectedValue Then
                    AssumedValueCheckBox.Checked = False
                End If

                Constraint.AllowableValues.Codes.Remove(CStr(listAllowableValues.SelectedValue))
                SetInternalCodedValues(listAllowableValues.SelectedIndex)
            End If

            mFileManager.FileEdited = True
            RefreshButtons()
        End If
    End Sub

    Private Sub AddExistingTermButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddExistingTermButton.Click, MenuItemAddExisting.Click
        If Not MyBase.IsLoading Then
            Dim currentValues(Constraint.AllowableValues.Codes.Count) As String
            Constraint.AllowableValues.Codes.CopyTo(currentValues, 0)

            If radioInternal.Checked Then
                Dim s() As String = Main.Instance.ChooseInternal(mFileManager, currentValues)

                If Not s Is Nothing Then
                    For i As Integer = 0 To s.Length - 1
                        Constraint.AllowableValues.Codes.Add(s(i))
                    Next

                    SetInternalCodedValues(Constraint.AllowableValues.Codes.Count - 1)
                End If
            End If

            RefreshButtons()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub TextTypeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioText.CheckedChanged, radioInternal.CheckedChanged, radioTerminology.CheckedChanged
        gbAllowableValues.Visible = radioInternal.Checked
        TermConstraintLabel.Visible = radioTerminology.Checked
        TermConstraintTextBox.Visible = radioTerminology.Checked
        TermConstraintDescriptionLabel.Visible = radioTerminology.Checked
        TermConstraintDescriptionTextBox.Visible = radioTerminology.Checked
        ConstraintBindingsGrid.Visible = radioTerminology.Checked

        If Not IsLoading Then
            If radioText.Checked Then
                Constraint.TypeOfTextConstraint = TextConstraintType.Text
                Constraint.HasAssumedValue = False
            ElseIf radioInternal.Checked Then
                Constraint.TypeOfTextConstraint = TextConstraintType.Internal
                NewTermButton.Focus()
                SetInternalCodedValues(0)
            Else
                Constraint.TypeOfTextConstraint = TextConstraintType.Terminology
                IsLoading = True  ' avoids replacing the text

                If Constraint.ConstraintCode = "" OrElse Not mFileManager.OntologyManager.Ontology.HasTermCode(Constraint.ConstraintCode) Then
                    mConstraintTerm = mFileManager.OntologyManager.AddConstraint(Filemanager.GetOpenEhrTerm(139, "New constraint"))
                    Constraint.ConstraintCode = mConstraintTerm.Code
                    TermConstraintTextBox.Text = mConstraintTerm.Text
                    TermConstraintDescriptionTextBox.Text = mConstraintTerm.Description
                Else
                    mConstraintTerm = mFileManager.OntologyManager.GetTerm(Constraint.ConstraintCode)
                    TermConstraintTextBox.Text = mConstraintTerm.Text
                    TermConstraintDescriptionTextBox.Text = mConstraintTerm.Description
                End If

                Constraint.HasAssumedValue = False
                IsLoading = False
                TermConstraintTextBox.Focus()
            End If

            mFileManager.FileEdited = True
            SetControlValuesForTypeOfTextConstraint()
        End If
    End Sub

    Private Sub TermConstraintTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TermConstraintTextBox.TextChanged
        If Not IsLoading Then
            mFileManager.OntologyManager.SetText(TermConstraintTextBox.Text, Constraint.ConstraintCode)
            'Remember if changes constraint type
            mConstraintTerm.Text = TermConstraintTextBox.Text
        End If
    End Sub

    Private Sub TermConstraintDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TermConstraintDescriptionTextBox.TextChanged
        If Not IsLoading Then
            mFileManager.OntologyManager.SetDescription(TermConstraintDescriptionTextBox.Text, Constraint.ConstraintCode)
            'Remember if changes constraint type
            mConstraintTerm.Description = TermConstraintDescriptionTextBox.Text
        End If
    End Sub

    Private Sub listAllowableValues_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemEdit.Click, listAllowableValues.DoubleClick, ListViewSelected.DoubleClick
        If listAllowableValues.SelectedIndex > -1 Then
            Dim s(1) As String
            Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(listAllowableValues.SelectedValue))

            If Not term Is Nothing Then
                s = Main.Instance.GetInput(term, ParentForm)

                If s(0) <> "" Then
                    mFileManager.OntologyManager.SetRmTermText(term)
                    mFileManager.OntologyManager.SetDescription(term.Description, term.Code)
                    SetInternalCodedValues(listAllowableValues.SelectedIndex)
                    RefreshButtons()
                    mFileManager.FileEdited = True
                End If
            End If
        End If
    End Sub

    Private Sub ContextMenuListAllowableValues_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuListAllowableValues.Popup
        If listAllowableValues.SelectedIndex > -1 Then
            MenuItemEdit.Text = String.Format("{0} {1}", Filemanager.GetOpenEhrTerm(592, "Edit "), listAllowableValues.Text)
            MenuItemEdit.Visible = True
            MenuItemRemove.Text = String.Format("{0} {1}", Filemanager.GetOpenEhrTerm(152, "Remove "), listAllowableValues.Text)
            MenuItemRemove.Visible = True
        Else
            MenuItemEdit.Visible = False
            MenuItemRemove.Visible = False
        End If
    End Sub

    Private Sub butMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveUpButton.Click
        If listAllowableValues.SelectedIndex > -1 Then
            Dim code As String = CStr(listAllowableValues.SelectedValue)
            Dim currIndex As Integer = listAllowableValues.SelectedIndex

            If currIndex > 0 Then
                Dim newindex As Integer = currIndex - 1
                Constraint.AllowableValues.Codes.RemoveAt(currIndex)
                Constraint.AllowableValues.Codes.Insert(newindex, code)
                SetInternalCodedValues(newindex)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    Private Sub butMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveDownButton.Click
        If listAllowableValues.SelectedIndex > -1 Then
            Dim code As String = CStr(listAllowableValues.SelectedValue)
            Dim currIndex As Integer = listAllowableValues.SelectedIndex

            If currIndex < Constraint.AllowableValues.Codes.Count - 1 Then
                Dim newindex As Integer = currIndex + 1
                Constraint.AllowableValues.Codes.RemoveAt(currIndex)
                Constraint.AllowableValues.Codes.Insert(newindex, code)
                SetInternalCodedValues(newindex)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    ' If internal code list is not empty allow export to tab-separated list via clipboard
    Private Sub butnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyListButton.Click
        If Constraint.AllowableValues.Codes.Count > 0 Then
            Dim clipText As String = ""

            For Each code As String In Constraint.AllowableValues.Codes()
                Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(code)
                clipText = clipText + term.Text + vbTab + term.Description + vbLf
            Next

            Clipboard.SetText(clipText)
            ToolTip1.Show("Copied to clipboard" + vbCrLf + clipText, listAllowableValues, 5000)
            RefreshButtons()
        End If
    End Sub

    ' If internal code list is empty allow import of tab-separated list via clipboard.
    ' Format expected is either 1 or 2 columns, with a TAB separator and LF line terminator.
    ' E.g. at000001<TAB>New code<LF>
    Private Sub btnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteListButton.Click
        If Constraint.AllowableValues.Codes.Count = 0 Then
            If Constraint.AllowableValues.Codes.Count <> 0 Then
                MessageBox.Show("A Clipboard paste can only be made to an empty list", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim row As String

                ' Parse clipboard text - tab separator
                For Each row In Clipboard.GetText().Split(CChar(vbLf))
                    'Skip empty rows
                    If row.Length <> 0 Then
                        Dim columns As String() = row.Trim().Split(CChar(vbTab))
                        Dim term As RmTerm

                        If columns.Length = 1 Then
                            term = mFileManager.OntologyManager.AddTerm(columns(0), "")
                            Constraint.AllowableValues.Codes.Add(term.Code)
                        ElseIf columns.Length = 2 Then
                            term = mFileManager.OntologyManager.AddTerm(columns(0), columns(1))
                            Constraint.AllowableValues.Codes.Add(term.Code)
                        Else
                            MessageBox.Show("Importing from the clipboard requires lines of tab-separated code and text, e.g., at0001<TAB>New code<LF>.", _
                            AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Next

                SetInternalCodedValues(Constraint.AllowableValues.Codes.Count - 1)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    Private Sub RefreshButtons()
        Dim isEmpty As Boolean = Constraint.AllowableValues.Codes.Count = 0
        Dim isAtEnd As Boolean = listAllowableValues.SelectedIndex = Constraint.AllowableValues.Codes.Count - 1
        Dim isAtStart As Boolean = listAllowableValues.SelectedIndex = 0

        MoveUpButton.Enabled = Not isEmpty And Not isAtStart
        MoveDownButton.Enabled = Not isEmpty And Not isAtEnd
        RemoveTermButton.Enabled = Not isEmpty
        CopyListButton.Enabled = Not isEmpty
        PasteListButton.Enabled = isEmpty
    End Sub

    Private Sub RefreshListItemDetail()
        Dim term As RmTerm = SelectedTerm()
        ListViewSelected.Items.Clear()

        If Not term Is Nothing Then
            Dim listviewContent(2) As String
            listviewContent(0) = term.Code
            listviewContent(1) = term.Description
            ListViewSelected.Items.Add(New ListViewItem(listviewContent))
        End If
    End Sub

    Private Sub listAllowableValues_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.SelectedIndexChanged
        RefreshButtons()
        RefreshListItemDetail()
    End Sub

    Private Sub listAllowableValues_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.MouseEnter
        ToolTip1.SetToolTip(listAllowableValues, listAllowableValues.Items.Count.ToString + " internal codes")
    End Sub

    Private Sub menuCopyTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCopyTerm.Click
        Dim term As RmTerm = SelectedTerm()

        If Not term Is Nothing Then
            Dim clipText As String = "local::" + term.Code + "::" + term.Text + vbLf
            Clipboard.SetText(clipText)
            ToolTip1.Show("Copied to clipboard" + vbCrLf + clipText, listAllowableValues, 5000)
        End If
    End Sub

    Private Function SelectedTerm() As RmTerm
        Dim result As RmTerm = Nothing
        Dim i As Integer = listAllowableValues.SelectedIndex

        If i >= 0 And i < Constraint.AllowableValues.Codes.Count Then
            result = mFileManager.OntologyManager.GetTerm(Constraint.AllowableValues.Codes(i))
        End If

        Return result
    End Function

    Private Sub TextConstraintControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListViewSelected.ContextMenu = ContextMenuListAllowableValues
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
'The Original Code is TextConstraintControl.vb.
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

