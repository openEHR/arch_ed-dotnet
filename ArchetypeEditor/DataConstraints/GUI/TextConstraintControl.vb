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

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            radioText.Text = Filemanager.GetOpenEhrTerm(444, radioText.Text)
            radioInternal.Text = Filemanager.GetOpenEhrTerm(150, radioInternal.Text)
            radioTerminology.Text = Filemanager.GetOpenEhrTerm(47, radioTerminology.Text)
            DefaultItemButton.Text = Filemanager.GetOpenEhrTerm(153, DefaultItemButton.Text)
            ToolTip1.SetToolTip(AddItemButton, Filemanager.GetOpenEhrTerm(602, "Add existing term"))
            ToolTip1.SetToolTip(NewItemButton, Filemanager.GetOpenEhrTerm(603, "Add new term"))
            ToolTip1.SetToolTip(RemoveItemButton, Filemanager.GetOpenEhrTerm(152, "Remove term"))
            TermConstraintLabel.Text = Filemanager.GetOpenEhrTerm(87, TermConstraintLabel.Text)
            TermConstraintDescriptionLabel.Text = Filemanager.GetOpenEhrTerm(113, TermConstraintDescriptionLabel.Text)
            ConstraintBindingsGrid.TranslateGui()
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents gbAllowableValues As System.Windows.Forms.GroupBox
    Friend WithEvents NewItemButton As System.Windows.Forms.Button
    Friend WithEvents DefaultItemButton As System.Windows.Forms.Button
    Friend WithEvents RemoveItemButton As System.Windows.Forms.Button
    Friend WithEvents txtAssumedValue As System.Windows.Forms.TextBox
    Friend WithEvents AddItemButton As System.Windows.Forms.Button
    Friend WithEvents listAllowableValues As System.Windows.Forms.ListBox
    Friend WithEvents radioInternal As System.Windows.Forms.RadioButton
    Friend WithEvents radioText As System.Windows.Forms.RadioButton
    Friend WithEvents radioTerminology As System.Windows.Forms.RadioButton
    Friend WithEvents TermConstraintDescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TermConstraintTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConstraintBindingsGrid As ConstraintBindingsControl
    Friend WithEvents ContextMenuListAllowableValues As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPasteAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCancelCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemAddExisting As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuClearText As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuClearText As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemEdit As System.Windows.Forms.MenuItem
    Friend WithEvents TermConstraintDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents PasteListButton As System.Windows.Forms.Button
    Friend WithEvents CopyListButton As System.Windows.Forms.Button
    Friend WithEvents MoveDownButton As System.Windows.Forms.Button
    Friend WithEvents MoveUpButton As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MenuItemRemove As System.Windows.Forms.MenuItem
    Friend WithEvents TermConstraintLabel As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TextConstraintControl))
        Me.gbAllowableValues = New System.Windows.Forms.GroupBox
        Me.PasteListButton = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.CopyListButton = New System.Windows.Forms.Button
        Me.MoveDownButton = New System.Windows.Forms.Button
        Me.MoveUpButton = New System.Windows.Forms.Button
        Me.NewItemButton = New System.Windows.Forms.Button
        Me.DefaultItemButton = New System.Windows.Forms.Button
        Me.RemoveItemButton = New System.Windows.Forms.Button
        Me.txtAssumedValue = New System.Windows.Forms.TextBox
        Me.ContextMenuClearText = New System.Windows.Forms.ContextMenu
        Me.MenuClearText = New System.Windows.Forms.MenuItem
        Me.AddItemButton = New System.Windows.Forms.Button
        Me.listAllowableValues = New System.Windows.Forms.ListBox
        Me.ContextMenuListAllowableValues = New System.Windows.Forms.ContextMenu
        Me.MenuItemEdit = New System.Windows.Forms.MenuItem
        Me.MenuItemRemove = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyAll = New System.Windows.Forms.MenuItem
        Me.MenuItemPasteAll = New System.Windows.Forms.MenuItem
        Me.MenuItemCancelCopy = New System.Windows.Forms.MenuItem
        Me.MenuItemAddExisting = New System.Windows.Forms.MenuItem
        Me.TermConstraintDescriptionLabel = New System.Windows.Forms.Label
        Me.TermConstraintLabel = New System.Windows.Forms.Label
        Me.radioInternal = New System.Windows.Forms.RadioButton
        Me.radioText = New System.Windows.Forms.RadioButton
        Me.radioTerminology = New System.Windows.Forms.RadioButton
        Me.TermConstraintDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.TermConstraintTextBox = New System.Windows.Forms.TextBox
        Me.ConstraintBindingsGrid = New ConstraintBindingsControl
        Me.gbAllowableValues.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbAllowableValues
        '
        Me.gbAllowableValues.BackColor = System.Drawing.Color.Transparent
        Me.gbAllowableValues.Controls.Add(Me.PasteListButton)
        Me.gbAllowableValues.Controls.Add(Me.CopyListButton)
        Me.gbAllowableValues.Controls.Add(Me.MoveDownButton)
        Me.gbAllowableValues.Controls.Add(Me.MoveUpButton)
        Me.gbAllowableValues.Controls.Add(Me.NewItemButton)
        Me.gbAllowableValues.Controls.Add(Me.DefaultItemButton)
        Me.gbAllowableValues.Controls.Add(Me.RemoveItemButton)
        Me.gbAllowableValues.Controls.Add(Me.txtAssumedValue)
        Me.gbAllowableValues.Controls.Add(Me.AddItemButton)
        Me.gbAllowableValues.Controls.Add(Me.listAllowableValues)
        Me.gbAllowableValues.Location = New System.Drawing.Point(16, 48)
        Me.gbAllowableValues.Name = "gbAllowableValues"
        Me.gbAllowableValues.Size = New System.Drawing.Size(372, 310)
        Me.gbAllowableValues.TabIndex = 50
        Me.gbAllowableValues.TabStop = False
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
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia
        Me.ImageList1.Images.SetKeyName(0, "arrowdown_blue16.bmp")
        Me.ImageList1.Images.SetKeyName(1, "arrowup_blue16.bmp")
        Me.ImageList1.Images.SetKeyName(2, "copy16.bmp")
        Me.ImageList1.Images.SetKeyName(3, "paste16.bmp")
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
        'NewItemButton
        '
        Me.NewItemButton.Image = CType(resources.GetObject("NewItemButton.Image"), System.Drawing.Image)
        Me.NewItemButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.NewItemButton.Location = New System.Drawing.Point(15, 24)
        Me.NewItemButton.Name = "NewItemButton"
        Me.NewItemButton.Size = New System.Drawing.Size(24, 24)
        Me.NewItemButton.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.NewItemButton, "Add a new term")
        '
        'DefaultItemButton
        '
        Me.DefaultItemButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DefaultItemButton.Location = New System.Drawing.Point(46, 279)
        Me.DefaultItemButton.Name = "DefaultItemButton"
        Me.DefaultItemButton.Size = New System.Drawing.Size(122, 24)
        Me.DefaultItemButton.TabIndex = 8
        Me.DefaultItemButton.Text = "Set assumed value"
        Me.DefaultItemButton.Visible = False
        '
        'RemoveItemButton
        '
        Me.RemoveItemButton.Image = CType(resources.GetObject("RemoveItemButton.Image"), System.Drawing.Image)
        Me.RemoveItemButton.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.RemoveItemButton.Location = New System.Drawing.Point(15, 56)
        Me.RemoveItemButton.Name = "RemoveItemButton"
        Me.RemoveItemButton.Size = New System.Drawing.Size(24, 24)
        Me.RemoveItemButton.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.RemoveItemButton, "Remove term")
        '
        'txtAssumedValue
        '
        Me.txtAssumedValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAssumedValue.ContextMenu = Me.ContextMenuClearText
        Me.txtAssumedValue.Location = New System.Drawing.Point(176, 283)
        Me.txtAssumedValue.Name = "txtAssumedValue"
        Me.txtAssumedValue.ReadOnly = True
        Me.txtAssumedValue.Size = New System.Drawing.Size(188, 20)
        Me.txtAssumedValue.TabIndex = 9
        Me.txtAssumedValue.Text = "(none)"
        Me.txtAssumedValue.Visible = False
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
        'AddItemButton
        '
        Me.AddItemButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddItemButton.Location = New System.Drawing.Point(15, 88)
        Me.AddItemButton.Name = "AddItemButton"
        Me.AddItemButton.Size = New System.Drawing.Size(24, 24)
        Me.AddItemButton.TabIndex = 2
        Me.AddItemButton.Text = "..."
        Me.ToolTip1.SetToolTip(Me.AddItemButton, "Add a term that is already defined")
        '
        'listAllowableValues
        '
        Me.listAllowableValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listAllowableValues.ContextMenu = Me.ContextMenuListAllowableValues
        Me.listAllowableValues.DisplayMember = "Text"
        Me.listAllowableValues.Location = New System.Drawing.Point(46, 20)
        Me.listAllowableValues.MultiColumn = True
        Me.listAllowableValues.Name = "listAllowableValues"
        Me.listAllowableValues.Size = New System.Drawing.Size(318, 251)
        Me.listAllowableValues.TabIndex = 7
        '
        'ContextMenuListAllowableValues
        '
        Me.ContextMenuListAllowableValues.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemEdit, Me.MenuItemRemove, Me.MenuItemCopyAll, Me.MenuItemPasteAll, Me.MenuItemCancelCopy, Me.MenuItemAddExisting})
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
        Me.gbAllowableValues.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mAllowedValuesDataView As DataView
    Private mTempConstraint As Constraint_Text
    Private mConstraintTerm As RmTerm
    Private CodeTermList As New ArrayList()

    ' Supports correct ordering of internal list + reordering
    Private Class CodeTerm
        Private m_atCode As String
        Private m_Text As String

        Sub New(ByVal p_atCode As String, ByVal p_Text As String)
            MyBase.New()
            m_atCode = p_atCode
            m_Text = p_Text
        End Sub

        Public Property atCode() As String
            Get
                Return m_atCode
            End Get
            Set(ByVal value As String)
                m_atCode = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return m_Text
            End Get
            Set(ByVal value As String)
                m_Text = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Private Shadows ReadOnly Property Constraint() As Constraint_Text
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Text)
            Return CType(MyBase.Constraint, Constraint_Text)
        End Get
    End Property

    Friend Property TextConstraint() As Constraint_Text
        Get
            Return Constraint
        End Get
        Set(ByVal value As Constraint_Text)
            MyBase.Constraint = value
            SetControlValues(False)
            IsLoading = False
        End Set
    End Property

    ' Set constraint values on control
    Protected Overrides Sub SetControlValues(ByVal isState As Boolean)
        If isState Then
            DefaultItemButton.Visible = True
            txtAssumedValue.Visible = True
        End If

        SetControlValuesForTypeOfTextConstraint()
    End Sub

    Protected Sub SetControlValuesForTypeOfTextConstraint()
        Select Case Constraint.TypeOfTextConstraint
            Case TextConstrainType.Text
                radioText.Checked = True

                'Added for backward compatibility with some archetypes with textual constraints
                If Constraint.AllowableValues.Codes.Count > 0 Then
                    Dim s As String
                    listAllowableValues.DataSource = Nothing
                    listAllowableValues.Items.Clear()

                    For Each s In Constraint.AllowableValues.Codes
                        listAllowableValues.Items.Add(s)
                    Next

                    txtAssumedValue.Text = CStr(Constraint.AssumedValue)
                End If

            Case TextConstrainType.Internal
                radioInternal.Checked = True
                SetInternalCodedValues()

            Case TextConstrainType.Terminology
                radioTerminology.Checked = True
                mConstraintTerm = mFileManager.OntologyManager.GetTerm(Constraint.ConstraintCode)
                TermConstraintTextBox.Text = mConstraintTerm.Text
                TermConstraintDescriptionTextBox.Text = mConstraintTerm.Description
                ConstraintBindingsGrid.SelectConstraintCode(mConstraintTerm.Code)
        End Select

        RefreshButtons()
    End Sub

    Private Sub SetInternalCodedValues()
        If Constraint.AllowableValues.Codes.Count > 0 AndAlso mFileManager.OntologyManager.Ontology.HasTermCode(TextConstraint.AllowableValues.Codes.Item(0)) Then
            RefreshlistAllowableValues(0)

            ' now look up the default
            If Constraint.HasAssumedValue Then
                Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(Constraint.AssumedValue))
                txtAssumedValue.Text = aTerm.Text
            Else
                txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
            End If
        Else
            ' to hide values as they are not overriden if the filter is set
            listAllowableValues.DataSource = Nothing
            listAllowableValues.Items.Clear()
            txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
            'May not be present so clear them if they are
            TextConstraint.AllowableValues.Codes.Clear()
        End If
    End Sub

    ' Establish correct cADL internal list order
    Private Sub RefreshlistAllowableValues(ByVal newindex As Integer)
        Dim atCode As String

        'Initialise Dataview
        SetAllowableValuesFilter(Constraint)
        mAllowedValuesDataView.Sort = "Code"

        'Refresh CodeTermList
        CodeTermList.Clear()

        For Each atCode In Constraint.AllowableValues.Codes
            Dim rowindex As Integer = mAllowedValuesDataView.Find(atCode)
            CodeTermList.Add(New CodeTerm(atCode, CStr(mAllowedValuesDataView.Item(rowindex)("Text"))))
        Next

        'Initialise Terms Listbox
        listAllowableValues.DataSource = Nothing
        listAllowableValues.Items.Clear()
        listAllowableValues.DisplayMember = "Text"
        listAllowableValues.ValueMember = "atCode"
        listAllowableValues.DataSource = CodeTermList
        listAllowableValues.Refresh()

        'Reset Selected index
        If newindex >= listAllowableValues.Items.Count Then
            newindex = listAllowableValues.Items.Count - 1
        End If

        listAllowableValues.SelectedIndex = newindex
    End Sub

    ' Adds one or more atCodes from existing list of atCodes
    Private Sub butAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItemButton.Click
        If Not MyBase.IsLoading Then
            Dim currentValues(Constraint.AllowableValues.Codes.Count) As String
            Constraint.AllowableValues.Codes.CopyTo(currentValues, 0)

            If radioInternal.Checked Then
                Dim s() As String = OceanArchetypeEditor.Instance.ChooseInternal(mFileManager, currentValues)

                If Not s Is Nothing Then
                    For i As Integer = 0 To s.Length - 1
                        Constraint.AllowableValues.Codes.Add(s(i))
                    Next

                    RefreshlistAllowableValues(Constraint.AllowableValues.Codes.Count - 1)
                End If
            End If

            RefreshButtons()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub SetAllowableValuesFilter(ByVal TextConstraint As Constraint_Text)
        Dim i As Integer
        Dim filter As String = ""
        Dim prefix As String = "Code = "

        For i = 0 To TextConstraint.AllowableValues.Codes.Count - 1
            ' Codes could be out of date if a save has been carried out
            Dim code As String = TextConstraint.AllowableValues.Codes.Item(i)
            filter = filter + prefix + "'" + code + "'"
            prefix = " OR Code = "
        Next

        If filter <> "" Then
            filter = "(" + filter + ") AND "
        End If

        mAllowedValuesDataView.RowFilter = filter + "id = '" + mFileManager.OntologyManager.LanguageCode + "'"
    End Sub

    Private Sub butDefaultItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultItemButton.Click
        If listAllowableValues.SelectedIndex > -1 Then
            ' the code is the default value, show the text
            txtAssumedValue.Text = listAllowableValues.Text
            Dim s As String = CStr(listAllowableValues.SelectedValue)
            txtAssumedValue.Tag = s
            ' automatically sets hasAssumedValue
            Constraint.AssumedValue = s

            RefreshButtons()
            mFileManager.FileEdited = True
        End If
    End Sub

    'Adds a completely new item
    Private Sub butNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewItemButton.Click
        If radioInternal.Checked Then
            AddNewInternalCode()
        ElseIf radioText.Checked Then
            Dim s As String = OceanArchetypeEditor.Instance.GetInput("Enter the new item:", ParentForm)

            If s <> "" Then
                listAllowableValues.DataSource = Nothing
                listAllowableValues.Items.Add(s)
                Constraint.AllowableValues.Codes.Add(s)
                RefreshButtons()
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub AddNewInternalCode()
        Dim s(1) As String
        s = OceanArchetypeEditor.Instance.GetInput(Filemanager.GetOpenEhrTerm(603, "Add new term"), AE_Constants.Instance.Description, ParentForm)

        If s(0) <> "" Then
            Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(s(0), s(1))
            Dim term_id As String = aTerm.Code

            ' add the code to the constraint
            Constraint.AllowableValues.Codes.Add(term_id)
            SetAllowableValuesFilter(Constraint)
            RefreshlistAllowableValues(Constraint.AllowableValues.Codes.Count - 1)
            RefreshButtons()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butRemoveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveItemButton.Click
        Dim Index As Integer = listAllowableValues.SelectedIndex

        If Index > -1 Then
            Try
                Dim defaultText As String = txtAssumedValue.Text

                If radioText.Checked Then
                    RemoveTextTerm(Index, defaultText)
                ElseIf radioInternal.Checked Then
                    RemoveCodedTerm(defaultText)
                End If
            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
            End Try

            RefreshButtons()
        End If
    End Sub

    ' Remove a text Term from the allowable term list 
    Private Sub RemoveTextTerm(ByVal Index As Integer, ByVal defaultText As String)
        If listAllowableValues.Items.Count > 0 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & CStr(listAllowableValues.Items(Index)), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                If CStr(listAllowableValues.Items(Index)) = defaultText Then
                    txtAssumedValue.Text = "(none)"
                    Constraint.HasAssumedValue = False
                End If

                Constraint.AllowableValues.Codes.Remove(CStr(listAllowableValues.Items(Index)))
                listAllowableValues.Items.RemoveAt(Index)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    ' Remove a coded Term from the allowable term list
    Private Sub RemoveCodedTerm(ByVal defaultText As String)
        If mAllowedValuesDataView.Count > 0 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & CStr(listAllowableValues.Text), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                ' have to delete this from all languages
                If CStr(listAllowableValues.Text) = defaultText Then
                    Constraint.HasAssumedValue = False
                    txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
                End If

                Dim code As String = CStr(listAllowableValues.SelectedValue) '("Code")
                Constraint.AllowableValues.Codes.Remove(code)

                If Constraint.AllowableValues.Codes.Count = 0 Then
                    listAllowableValues.DataSource = Nothing
                    listAllowableValues.Items.Clear()
                Else
                    RefreshlistAllowableValues(listAllowableValues.SelectedIndex)
                End If
            End If

            mFileManager.FileEdited = True
            RefreshButtons()
        End If
    End Sub

    Private Sub radioText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioText.CheckedChanged
        If radioText.Checked Then
            gbAllowableValues.Visible = Constraint.TypeOfTextConstraint = TextConstrainType.Text And Constraint.AllowableValues.Codes.Count > 0
            TermConstraintLabel.Visible = False
            TermConstraintTextBox.Visible = False
            TermConstraintDescriptionLabel.Visible = False
            TermConstraintDescriptionTextBox.Visible = False
            ConstraintBindingsGrid.Visible = False
        End If

        If Not MyBase.IsLoading Then
            If radioText.Checked Then
                Constraint.TypeOfTextConstraint = TextConstrainType.Text
                mFileManager.FileEdited = True
                SetControlValuesForTypeOfTextConstraint()
            End If
        End If
    End Sub

    Private Sub radioInternal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioInternal.CheckedChanged
        If radioInternal.Checked Then
            gbAllowableValues.Visible = True
            TermConstraintLabel.Visible = False
            TermConstraintTextBox.Visible = False
            TermConstraintDescriptionLabel.Visible = False
            TermConstraintDescriptionTextBox.Visible = False
            ConstraintBindingsGrid.Visible = False
        End If

        If Not MyBase.IsLoading Then
            If radioInternal.Checked Then
                NewItemButton.Focus()
                Constraint.TypeOfTextConstraint = TextConstrainType.Internal
                SetInternalCodedValues()
                mFileManager.FileEdited = True
                SetControlValuesForTypeOfTextConstraint()
            End If
        End If
    End Sub

    Private Sub radioTerminology_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioTerminology.CheckedChanged
        If radioTerminology.Checked Then
            gbAllowableValues.Visible = False
            TermConstraintLabel.Visible = True
            TermConstraintTextBox.Visible = True
            TermConstraintDescriptionLabel.Visible = True
            TermConstraintDescriptionTextBox.Visible = True
            ConstraintBindingsGrid.Visible = True
        End If

        If Not MyBase.IsLoading Then
            If radioTerminology.Checked Then
                Constraint.TypeOfTextConstraint = TextConstrainType.Terminology
                MyBase.IsLoading = True  ' avoids replacing the text

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

                txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
                Constraint.HasAssumedValue = False
                MyBase.IsLoading = False

                TermConstraintTextBox.Focus()
                mFileManager.FileEdited = True
                SetControlValuesForTypeOfTextConstraint()
            End If
        End If
    End Sub

    Private Sub TermConstraintTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TermConstraintTextBox.TextChanged
        If Not MyBase.IsLoading Then
            mFileManager.OntologyManager.SetText(TermConstraintTextBox.Text, Constraint.ConstraintCode)
            'Remember if changes constraint type
            mConstraintTerm.Text = TermConstraintTextBox.Text
        End If
    End Sub

    Private Sub TermConstraintDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TermConstraintDescriptionTextBox.TextChanged
        If Not MyBase.IsLoading Then
            mFileManager.OntologyManager.SetDescription(TermConstraintDescriptionTextBox.Text, Constraint.ConstraintCode)
            'Remember if changes constraint type
            mConstraintTerm.Description = TermConstraintDescriptionTextBox.Text
        End If
    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClearText.Click
        If Not MyBase.IsLoading Then
            Constraint.HasAssumedValue = False
            txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub MenuItemAddExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAddExisting.Click
        butAddItem_Click(sender, e)
    End Sub

    Private Sub listAllowableValues_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.DoubleClick, MenuItemEdit.Click
        If listAllowableValues.SelectedIndex > -1 Then
            Dim s(1) As String
            Dim t As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(listAllowableValues.SelectedValue))

            If Not t Is Nothing Then
                s = OceanArchetypeEditor.Instance.GetInput(t, ParentForm)

                If s(0) <> "" Then
                    mFileManager.OntologyManager.SetRmTermText(t)
                    mFileManager.OntologyManager.SetDescription(t.Description, t.Code)
                    RefreshlistAllowableValues(listAllowableValues.SelectedIndex)
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
            Dim currIndex, newindex As Integer
            Dim atCode As String = CStr(listAllowableValues.SelectedValue)
            currIndex = listAllowableValues.SelectedIndex

            If currIndex > 0 Then
                newindex = currIndex - 1
                Constraint.AllowableValues.Codes.RemoveAt(currIndex)
                Constraint.AllowableValues.Codes.Insert(newindex, atCode)
                RefreshlistAllowableValues(newindex)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    Private Sub butMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveDownButton.Click
        If listAllowableValues.SelectedIndex > -1 Then
            Dim currIndex, newindex As Integer
            Dim atCode As String = CStr(listAllowableValues.SelectedValue)
            currIndex = listAllowableValues.SelectedIndex

            If currIndex < Constraint.AllowableValues.Codes.Count - 1 Then
                newindex = currIndex + 1
                Constraint.AllowableValues.Codes.RemoveAt(currIndex)
                Constraint.AllowableValues.Codes.Insert(newindex, atCode)
                RefreshlistAllowableValues(newindex)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    ' If internal code list is not empty allow export to tab-separated list via clipboard
    Private Sub butnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyListButton.Click
        If radioInternal.Checked And (Constraint.AllowableValues.Codes.Count > 0) Then
            Dim atCode As String
            Dim aTerm As RmTerm
            Dim clipText As String = ""

            For Each atCode In Constraint.AllowableValues.Codes()
                aTerm = mFileManager.OntologyManager.GetTerm(atCode)
                clipText = clipText + aTerm.Text + vbTab + aTerm.Description + vbLf
            Next

            Clipboard.SetText(clipText)
            MessageBox.Show("The list of internal codes has been copied to Clipboard,", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            RefreshButtons()
        End If
    End Sub

    ' If internal code list is empty allow import of tab-separated list via clipboard
    ' Format expected is 2 columns with a TAB separator and LF line terminator
    ' e.g. at000001<TAB>New code<LF>
    Private Sub btnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteListButton.Click
        If radioInternal.Checked And Constraint.AllowableValues.Codes.Count = 0 Then
            If Constraint.AllowableValues.Codes.Count <> 0 Then
                MessageBox.Show("A Clipboard paste can only be made to an empty list", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim row As String
                Dim term As RmTerm
                Dim rows As String() = Clipboard.GetText().Split(CChar(vbLf))

                ' Parse clipboard text - tab separator
                For Each row In rows
                    'Skip empty rows
                    If row.Length <> 0 Then
                        Dim columns As String() = row.Split(CChar(vbTab))

                        If columns.Length <> 2 Then
                            MessageBox.Show("Importing from the clipboard requires lines of tab-separated code and text, e.g., at0001<TAB>New code<LF>.", _
                            AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            term = mFileManager.OntologyManager.AddTerm(columns(0), columns(1))
                            Constraint.AllowableValues.Codes.Add(term.Code)
                        End If
                    End If
                Next

                RefreshlistAllowableValues(Constraint.AllowableValues.Codes.Count - 1)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub

    ' Refreshes button Enabled states
    Private Sub RefreshButtons()
        Dim EmptyList As Boolean = Constraint.AllowableValues.Codes.Count = 0
        Dim EndOfList As Boolean = listAllowableValues.SelectedIndex = Constraint.AllowableValues.Codes.Count - 1
        Dim StartOfList As Boolean = listAllowableValues.SelectedIndex = 0

        MoveUpButton.Enabled = Not EmptyList And Not StartOfList
        MoveDownButton.Enabled = Not EmptyList And Not EndOfList
        RemoveItemButton.Enabled = Not EmptyList
        CopyListButton.Enabled = Not EmptyList
        PasteListButton.Enabled = EmptyList
    End Sub

    Private Sub listAllowableValues_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.SelectedIndexChanged
        RefreshButtons()
    End Sub

    Private Sub MenuItemRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRemove.Click
        butRemoveItem_Click(sender, e)
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

