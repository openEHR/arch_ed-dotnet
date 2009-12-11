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

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.radioText.Text = Filemanager.GetOpenEhrTerm(444, Me.radioText.Text)
            Me.radioInternal.Text = Filemanager.GetOpenEhrTerm(150, Me.radioInternal.Text)
            Me.radioTerminology.Text = Filemanager.GetOpenEhrTerm(47, Me.radioTerminology.Text)
            Me.butDefaultItem.Text = Filemanager.GetOpenEhrTerm(153, Me.butDefaultItem.Text)
            Me.ToolTip1.SetToolTip(Me.butAddItem, Filemanager.GetOpenEhrTerm(602, "Add existing term"))
            Me.ToolTip1.SetToolTip(Me.ButNewItem, Filemanager.GetOpenEhrTerm(603, "Add new term"))
            Me.ToolTip1.SetToolTip(Me.butRemoveItem, Filemanager.GetOpenEhrTerm(152, "Remove term"))
            Me.lblConstraint.Text = Filemanager.GetOpenEhrTerm(87, Me.lblConstraint.Text)
            Me.lblDescription.Text = Filemanager.GetOpenEhrTerm(113, Me.lblDescription.Text)

        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents gbAllowableValues As System.Windows.Forms.GroupBox
    Friend WithEvents ButNewItem As System.Windows.Forms.Button
    Friend WithEvents butDefaultItem As System.Windows.Forms.Button
    Friend WithEvents butRemoveItem As System.Windows.Forms.Button
    Friend WithEvents txtAssumedValue As System.Windows.Forms.TextBox
    Friend WithEvents butAddItem As System.Windows.Forms.Button
    Friend WithEvents listAllowableValues As System.Windows.Forms.ListBox
    Friend WithEvents radioInternal As System.Windows.Forms.RadioButton
    Friend WithEvents radioText As System.Windows.Forms.RadioButton
    Friend WithEvents radioTerminology As System.Windows.Forms.RadioButton
    Friend WithEvents txtTermConstraintDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtTermConstraintText As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuListAllowableValues As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPasteAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCancelCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemAddExisting As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuClearText As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuClearText As System.Windows.Forms.MenuItem
    'Private components As System.ComponentModel.IContainer
    'Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuItemEdit As System.Windows.Forms.MenuItem
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents btnPasteList As System.Windows.Forms.Button
    Friend WithEvents btnCopyList As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MenuItemRemove As System.Windows.Forms.MenuItem
    Friend WithEvents lblConstraint As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TextConstraintControl))
        Me.gbAllowableValues = New System.Windows.Forms.GroupBox
        Me.btnPasteList = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnCopyList = New System.Windows.Forms.Button
        Me.btnMoveDown = New System.Windows.Forms.Button
        Me.btnMoveUp = New System.Windows.Forms.Button
        Me.ButNewItem = New System.Windows.Forms.Button
        Me.butDefaultItem = New System.Windows.Forms.Button
        Me.butRemoveItem = New System.Windows.Forms.Button
        Me.txtAssumedValue = New System.Windows.Forms.TextBox
        Me.ContextMenuClearText = New System.Windows.Forms.ContextMenu
        Me.MenuClearText = New System.Windows.Forms.MenuItem
        Me.butAddItem = New System.Windows.Forms.Button
        Me.listAllowableValues = New System.Windows.Forms.ListBox
        Me.ContextMenuListAllowableValues = New System.Windows.Forms.ContextMenu
        Me.MenuItemEdit = New System.Windows.Forms.MenuItem
        Me.MenuItemRemove = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyAll = New System.Windows.Forms.MenuItem
        Me.MenuItemPasteAll = New System.Windows.Forms.MenuItem
        Me.MenuItemCancelCopy = New System.Windows.Forms.MenuItem
        Me.MenuItemAddExisting = New System.Windows.Forms.MenuItem
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblConstraint = New System.Windows.Forms.Label
        Me.radioInternal = New System.Windows.Forms.RadioButton
        Me.radioText = New System.Windows.Forms.RadioButton
        Me.radioTerminology = New System.Windows.Forms.RadioButton
        Me.txtTermConstraintDescription = New System.Windows.Forms.TextBox
        Me.txtTermConstraintText = New System.Windows.Forms.TextBox
        Me.gbAllowableValues.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbAllowableValues
        '
        Me.gbAllowableValues.BackColor = System.Drawing.Color.Transparent
        Me.gbAllowableValues.Controls.Add(Me.btnPasteList)
        Me.gbAllowableValues.Controls.Add(Me.btnCopyList)
        Me.gbAllowableValues.Controls.Add(Me.btnMoveDown)
        Me.gbAllowableValues.Controls.Add(Me.btnMoveUp)
        Me.gbAllowableValues.Controls.Add(Me.ButNewItem)
        Me.gbAllowableValues.Controls.Add(Me.butDefaultItem)
        Me.gbAllowableValues.Controls.Add(Me.butRemoveItem)
        Me.gbAllowableValues.Controls.Add(Me.txtAssumedValue)
        Me.gbAllowableValues.Controls.Add(Me.butAddItem)
        Me.gbAllowableValues.Controls.Add(Me.listAllowableValues)
        Me.gbAllowableValues.Location = New System.Drawing.Point(16, 40)
        Me.gbAllowableValues.Name = "gbAllowableValues"
        Me.gbAllowableValues.Size = New System.Drawing.Size(372, 362)
        Me.gbAllowableValues.TabIndex = 36
        Me.gbAllowableValues.TabStop = False
        '
        'btnPasteList
        '
        Me.btnPasteList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPasteList.ImageKey = "paste16.bmp"
        Me.btnPasteList.ImageList = Me.ImageList1
        Me.btnPasteList.Location = New System.Drawing.Point(15, 224)
        Me.btnPasteList.Name = "btnPasteList"
        Me.btnPasteList.Size = New System.Drawing.Size(24, 24)
        Me.btnPasteList.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.btnPasteList, "Paste an external list from Clipboard")
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
        'btnCopyList
        '
        Me.btnCopyList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopyList.ImageKey = "copy16.bmp"
        Me.btnCopyList.ImageList = Me.ImageList1
        Me.btnCopyList.Location = New System.Drawing.Point(15, 194)
        Me.btnCopyList.Name = "btnCopyList"
        Me.btnCopyList.Size = New System.Drawing.Size(24, 24)
        Me.btnCopyList.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnCopyList, "Copy the list to the clipboard")
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveDown.ImageKey = "arrowdown_blue16.bmp"
        Me.btnMoveDown.ImageList = Me.ImageList1
        Me.btnMoveDown.Location = New System.Drawing.Point(15, 154)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(24, 24)
        Me.btnMoveDown.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnMoveDown, "Move the highlighted item down the list." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveUp.ImageIndex = 1
        Me.btnMoveUp.ImageList = Me.ImageList1
        Me.btnMoveUp.Location = New System.Drawing.Point(15, 124)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(24, 24)
        Me.btnMoveUp.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnMoveUp, "Move the highlighted item up the list")
        '
        'ButNewItem
        '
        Me.ButNewItem.Image = CType(resources.GetObject("ButNewItem.Image"), System.Drawing.Image)
        Me.ButNewItem.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButNewItem.Location = New System.Drawing.Point(15, 24)
        Me.ButNewItem.Name = "ButNewItem"
        Me.ButNewItem.Size = New System.Drawing.Size(24, 24)
        Me.ButNewItem.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.ButNewItem, "Add a new term")
        '
        'butDefaultItem
        '
        Me.butDefaultItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.butDefaultItem.Location = New System.Drawing.Point(48, 333)
        Me.butDefaultItem.Name = "butDefaultItem"
        Me.butDefaultItem.Size = New System.Drawing.Size(122, 24)
        Me.butDefaultItem.TabIndex = 4
        Me.butDefaultItem.Text = "Set assumed value"
        Me.butDefaultItem.Visible = False
        '
        'butRemoveItem
        '
        Me.butRemoveItem.Image = CType(resources.GetObject("butRemoveItem.Image"), System.Drawing.Image)
        Me.butRemoveItem.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveItem.Location = New System.Drawing.Point(15, 56)
        Me.butRemoveItem.Name = "butRemoveItem"
        Me.butRemoveItem.Size = New System.Drawing.Size(24, 24)
        Me.butRemoveItem.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.butRemoveItem, "Remove term")
        '
        'txtAssumedValue
        '
        Me.txtAssumedValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAssumedValue.ContextMenu = Me.ContextMenuClearText
        Me.txtAssumedValue.Location = New System.Drawing.Point(176, 335)
        Me.txtAssumedValue.Name = "txtAssumedValue"
        Me.txtAssumedValue.ReadOnly = True
        Me.txtAssumedValue.Size = New System.Drawing.Size(188, 20)
        Me.txtAssumedValue.TabIndex = 5
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
        'butAddItem
        '
        Me.butAddItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddItem.Location = New System.Drawing.Point(15, 88)
        Me.butAddItem.Name = "butAddItem"
        Me.butAddItem.Size = New System.Drawing.Size(24, 24)
        Me.butAddItem.TabIndex = 2
        Me.butAddItem.Text = "..."
        Me.ToolTip1.SetToolTip(Me.butAddItem, "Add a term that is already defined")
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
        Me.listAllowableValues.Size = New System.Drawing.Size(318, 303)
        Me.listAllowableValues.TabIndex = 3
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
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(16, 104)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(88, 24)
        Me.lblDescription.TabIndex = 38
        Me.lblDescription.Text = "Description"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConstraint
        '
        Me.lblConstraint.Location = New System.Drawing.Point(16, 48)
        Me.lblConstraint.Name = "lblConstraint"
        Me.lblConstraint.Size = New System.Drawing.Size(88, 24)
        Me.lblConstraint.TabIndex = 37
        Me.lblConstraint.Text = "Constraint"
        Me.lblConstraint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'radioInternal
        '
        Me.radioInternal.Location = New System.Drawing.Point(148, 6)
        Me.radioInternal.Name = "radioInternal"
        Me.radioInternal.Size = New System.Drawing.Size(120, 32)
        Me.radioInternal.TabIndex = 32
        Me.radioInternal.Text = "Internal codes"
        '
        'radioText
        '
        Me.radioText.Location = New System.Drawing.Point(16, 6)
        Me.radioText.Name = "radioText"
        Me.radioText.Size = New System.Drawing.Size(103, 32)
        Me.radioText.TabIndex = 31
        Me.radioText.Text = "Free text or coded"
        '
        'radioTerminology
        '
        Me.radioTerminology.Location = New System.Drawing.Point(275, 6)
        Me.radioTerminology.Name = "radioTerminology"
        Me.radioTerminology.Size = New System.Drawing.Size(112, 32)
        Me.radioTerminology.TabIndex = 33
        Me.radioTerminology.Text = "Terminology"
        '
        'txtTermConstraintDescription
        '
        Me.txtTermConstraintDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTermConstraintDescription.Location = New System.Drawing.Point(19, 128)
        Me.txtTermConstraintDescription.Multiline = True
        Me.txtTermConstraintDescription.Name = "txtTermConstraintDescription"
        Me.txtTermConstraintDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermConstraintDescription.Size = New System.Drawing.Size(369, 265)
        Me.txtTermConstraintDescription.TabIndex = 35
        Me.txtTermConstraintDescription.Visible = False
        '
        'txtTermConstraintText
        '
        Me.txtTermConstraintText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTermConstraintText.Location = New System.Drawing.Point(19, 72)
        Me.txtTermConstraintText.Name = "txtTermConstraintText"
        Me.txtTermConstraintText.Size = New System.Drawing.Size(369, 20)
        Me.txtTermConstraintText.TabIndex = 34
        Me.txtTermConstraintText.Visible = False
        '
        'TextConstraintControl
        '
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.gbAllowableValues)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblConstraint)
        Me.Controls.Add(Me.radioInternal)
        Me.Controls.Add(Me.radioText)
        Me.Controls.Add(Me.radioTerminology)
        Me.Controls.Add(Me.txtTermConstraintDescription)
        Me.Controls.Add(Me.txtTermConstraintText)
        Me.Name = "TextConstraintControl"
        Me.Size = New System.Drawing.Size(407, 414)
        Me.gbAllowableValues.ResumeLayout(False)
        Me.gbAllowableValues.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mAllowedValuesDataView As DataView
    Private mTempConstraint As Constraint_Text
    Private mConstraintTerm As RmTerm
    'IMCN: 20 October 2009
    ' Supports correct ordering of internal list + reordering
    Private CodeTermList As New ArrayList()
    'IMCN: 20 October 2009
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
    '// Set constraint values on control
    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        'Changed SRH: 16th Sep - need to make this visible regardless of type to ensure
        ' it is displayed if type of constraint is changed (moved from SetInternalCodedValues)
        ' If IsState Then
        Me.butDefaultItem.Visible = True
        Me.txtAssumedValue.Visible = True
        'End If

        Select Case Me.Constraint.TypeOfTextConstraint
            Case TextConstrainType.Text
                radioText.Checked = True
                'Added for backward compatibility with some archetypes with textural constraints
                If Me.Constraint.AllowableValues.Codes.Count > 0 Then
                    Dim s As String
                    Me.listAllowableValues.DataSource = Nothing
                    Me.listAllowableValues.Items.Clear()

                    For Each s In Me.Constraint.AllowableValues.Codes
                        Me.listAllowableValues.Items.Add(s)
                    Next
                    Me.txtAssumedValue.Text = CStr(Me.Constraint.AssumedValue)
                End If

            Case TextConstrainType.Internal
                Me.radioInternal.Checked = True
                SetInternalCodedValues()

            Case TextConstrainType.Terminology
                Me.radioTerminology.Checked = True
                'SRH 2 Nov 2007: Changed to set the internal code
                'Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm( _
                '        Me.Constraint.ConstraintCode)
                mConstraintTerm = mFileManager.OntologyManager.GetTerm(Me.Constraint.ConstraintCode)

                Me.txtTermConstraintText.Text = mConstraintTerm.Text
                Me.txtTermConstraintDescription.Text = mConstraintTerm.Description

        End Select
        'Added IMCN: 20 October 2009
        RefreshButtons()


    End Sub

    Private Sub SetInternalCodedValues()

        If Me.Constraint.AllowableValues.Codes.Count > 0 AndAlso mFileManager.OntologyManager.Ontology.HasTermCode(TextConstraint.AllowableValues.Codes.Item(0)) Then
            RefreshlistAllowableValues(0)
            ' now look up the default
            If Me.Constraint.HasAssumedValue Then
                Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(Me.Constraint.AssumedValue))
                Me.txtAssumedValue.Text = aTerm.Text
            Else
                Me.txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
            End If

        Else
            ' to hide values as they are not overriden if the filter is set
            Me.listAllowableValues.DataSource = Nothing
            Me.listAllowableValues.Items.Clear()
            Me.txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
            'May not be present so clear them if they are
            TextConstraint.AllowableValues.Codes.Clear()
        End If

    End Sub

    'Added IMCN: 19 October 2009
    ' Establish correct cADL internal list order
    Private Sub RefreshlistAllowableValues(ByVal newindex As Integer)
        Dim atCode As String
        Dim rowindex As Integer

        'Initialise Dataview
        SetAllowableValuesFilter(Me.Constraint)
        mAllowedValuesDataView.Sort = "Code"

        'Refresh CodeTermList
        CodeTermList.Clear()
        For Each atCode In Me.Constraint.AllowableValues.Codes
            rowindex = mAllowedValuesDataView.Find(atCode)
            CodeTermList.Add(New CodeTerm(atCode, CStr(mAllowedValuesDataView.Item(rowindex)("Text"))))
        Next

        'Initialise Terms Listbox
        Me.listAllowableValues.DataSource = Nothing
        Me.listAllowableValues.Items.Clear()
        Me.listAllowableValues.DisplayMember = "Text"
        Me.listAllowableValues.ValueMember = "atCode"
        Me.listAllowableValues.DataSource = CodeTermList
        Me.listAllowableValues.Refresh()

        'Reset Selected index
        If newindex >= Me.listAllowableValues.Items.Count Then
            newindex = Me.listAllowableValues.Items.Count - 1
        End If
        Me.listAllowableValues.SelectedIndex = newindex

    End Sub
    ' Adds one or more atCodes from existing list of atCodes
    Private Sub butAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddItem.Click

        If MyBase.IsLoading Then Return

        'SRH: 30 Jun 2008 - added check to avoid values already added
        Dim currentValues(Me.Constraint.AllowableValues.Codes.Count) As String
        Me.Constraint.AllowableValues.Codes.CopyTo(currentValues, 0)

        If Me.radioText.Checked Then

        ElseIf Me.radioInternal.Checked Then

            Dim s() As String = OceanArchetypeEditor.Instance.ChooseInternal(mFileManager, currentValues)

            If s Is Nothing Then Return

            For i As Integer = 0 To s.Length - 1
                Me.Constraint.AllowableValues.Codes.Add(s(i))
            Next

            '//Added IMCN 22 Ocotober 2009
            RefreshlistAllowableValues(Me.Constraint.AllowableValues.Codes.Count - 1)

        End If

        '//Added IMCN 22 Ocotober 2009
        RefreshButtons()
        mFileManager.FileEdited = True

    End Sub

    Private Sub SetAllowableValuesFilter(ByVal TextConstraint As Constraint_Text)
        Dim i As Integer
        Dim cd As String
        Dim code As String
        cd = "Code = "

        For i = 0 To TextConstraint.AllowableValues.Codes.Count - 1
            ' Codes could be out of date if a save has been carried out
            code = TextConstraint.AllowableValues.Codes.Item(i)
            cd = cd & "'" & code & "'"
            If i < (TextConstraint.AllowableValues.Codes.Count - 1) Then
                cd = cd & " OR Code = "
            End If
        Next

        mAllowedValuesDataView.RowFilter = String.Format("({0}) AND (id = '{1}')", _
                cd, mFileManager.OntologyManager.LanguageCode)
    End Sub

    Private Sub butDefaultItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDefaultItem.Click

        If Me.listAllowableValues.SelectedIndex > -1 Then
            Dim s As String = ""
            ' the code is the default value, show the text
            '// Changed IMCN: 20 October 2009
            Me.txtAssumedValue.Text = Me.listAllowableValues.Text
            s = CStr(Me.listAllowableValues.SelectedValue)
            Me.txtAssumedValue.Tag = s
            ' automatically sets hasAssumedValue
            Me.Constraint.AssumedValue = s

            '// Added IMCN: 22 October 2009
            RefreshButtons()
            mFileManager.FileEdited = True
        End If
    End Sub

    'Adds a completely new item
    Private Sub butNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButNewItem.Click

        If Me.radioInternal.Checked Then
            AddNewInternalCode()
            'SRH: Dec 16 2008 - reintroduced for backward compatibility with textural constraints in archetypes
        ElseIf Me.radioText.Checked Then
            Dim s As String
            s = OceanArchetypeEditor.Instance.GetInput("Enter the new item:", Me.ParentForm)
            If s <> "" Then
                Me.listAllowableValues.DataSource = Nothing
                Me.listAllowableValues.Items.Add(s)
                Me.Constraint.AllowableValues.Codes.Add(s)
                '// Added IMCN: 22 October 2009
                RefreshButtons()
                mFileManager.FileEdited = True
            End If
        End If

    End Sub

    '// IMCN 22 October 2009
    '// Refactored
    Private Sub AddNewInternalCode()

        Dim s(1) As String
        s = OceanArchetypeEditor.Instance.GetInput(Filemanager.GetOpenEhrTerm(603, "Add new term"), _
                                                   AE_Constants.Instance.Description, Me.ParentForm)

        If s(0) <> "" Then
            Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(s(0), s(1))
            Dim term_id As String = aTerm.Code

            '// Removed IMCN 22 October 2009 
            '//  No longer required as listbox is not bound directly to Dataview  
            'If Me.listAllowableValues.DataSource Is Nothing Then
            '    mAllowedValuesDataView.RowFilter = String.Format("(Code = '{0}') AND (id = '{1}')", _
            '            aTerm.Code, mFileManager.OntologyManager.LanguageCode)
            '    Me.listAllowableValues.DataSource = mAllowedValuesDataView
            '    Me.listAllowableValues.DisplayMember = "Text"
            '    Me.listAllowableValues.ValueMember = "Code"
            'Else
            '    ' add this id to the term filter
            '    Dim f As String = mAllowedValuesDataView.RowFilter
            '    Dim i As Integer = InStr(f, ") AND (") - 1
            '    Dim str As String = " OR Code = '" & term_id & "'"
            '    mAllowedValuesDataView.RowFilter = f.Insert(i, str)
            'End If

            ' add the code to the constraint
            Me.Constraint.AllowableValues.Codes.Add(term_id)
            SetAllowableValuesFilter(Me.Constraint)
            RefreshlistAllowableValues(Me.Constraint.AllowableValues.Codes.Count - 1)

            '// Added IMCN: 22 October 2009
            RefreshButtons()
            mFileManager.FileEdited = True

        End If
    End Sub

    Private Sub butRemoveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveItem.Click
        Dim Index As Integer = Me.listAllowableValues.SelectedIndex

        '//IMCN 22 October 2009
        '// Refactored 
        If Index > -1 Then
            Try
                Dim defaultText As String = Me.txtAssumedValue.Text

                If Me.radioText.Checked Then
                    RemoveTextTerm(Index, defaultText)
                ElseIf Me.radioInternal.Checked Then
                    RemoveCodedTerm(defaultText)
                End If

            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
            End Try

            RefreshButtons()
        End If
    End Sub

    '//IMCN 22 October 2009
    '// Remove a text Term from the allowable term list 
    Private Sub RemoveTextTerm(ByVal Index As Integer, ByVal defaultText As String)

        If Me.listAllowableValues.Items.Count > 0 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & _
                               CStr(Me.listAllowableValues.Items(Index)), _
                               AE_Constants.Instance.MessageBoxCaption, _
                               MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                If CStr(Me.listAllowableValues.Items(Index)) = defaultText Then
                    Me.txtAssumedValue.Text = "(none)"
                    Me.Constraint.HasAssumedValue = False
                End If

                Me.Constraint.AllowableValues.Codes.Remove(CStr(Me.listAllowableValues.Items(Index)))
                Me.listAllowableValues.Items.RemoveAt(Index)

                mFileManager.FileEdited = True
                RefreshButtons()

            End If
        End If
    End Sub

    '//IMCN 22 October 2009
    '// Remove a coded Term from the allowable term list
    Private Sub RemoveCodedTerm(ByVal defaultText As String)

        If mAllowedValuesDataView.Count > 0 Then

            If MessageBox.Show(AE_Constants.Instance.Remove & _
                               CStr(Me.listAllowableValues.Text), _
                               AE_Constants.Instance.MessageBoxCaption, _
                               MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then

                ' have to delete this from all languages
                If CStr(Me.listAllowableValues.Text) = defaultText Then
                    Me.Constraint.HasAssumedValue = False
                    Me.txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))

                End If

                Dim code As String = CStr(Me.listAllowableValues.SelectedValue) '("Code")
                Me.Constraint.AllowableValues.Codes.Remove(code)

                'Cannot remove terms as are reused in the archetype - if this term is redundant it will be removed on save
                'ParentFrm.mFileManager.OntologyManager.RemoveTerm(Code)

                If Me.Constraint.AllowableValues.Codes.Count = 0 Then
                    Me.listAllowableValues.DataSource = Nothing
                    Me.listAllowableValues.Items.Clear()

                Else
                    '// IMCN : 20 October 2009
                    Me.RefreshlistAllowableValues(Me.listAllowableValues.SelectedIndex)

                    '// IMCN : 20 October 2009
                    '// No longer required as listbox items are no longer bound directly to the Dataview

                    'Dim f As String = mAllowedValuesDataView.RowFilter
                    'Dim i As Integer = InStr(f, "Code = '" & code & "' OR ")
                    'Dim Lengthof As Integer = ("Code = '" & code & "' OR ").Length - 1
                    'If i = 0 Then
                    '    'might be the last code
                    '    i = InStr(f, " OR Code = '" & code & "'")
                    '    Lengthof = (" OR Code = '" & code & "'").Length - 1
                    'End If
                    'If i = 0 Then
                    '    'might be the only code
                    '    i = InStr(f, "Code = '" & code & "'")
                    '    Lengthof = ("Code = '" & code & "'").Length - 1
                    'End If
                    'If i <> 0 Then
                    '    f = f.Substring(0, i - 1) & f.Substring(i + Lengthof)
                    'End If
                    'mAllowedValuesDataView.RowFilter = f
                End If

            End If
            mFileManager.FileEdited = True
            RefreshButtons()

        End If
    End Sub

    Private Sub radioInternal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioInternal.CheckedChanged
        If Me.radioInternal.Checked Then
            gbAllowableValues.Visible = True
            txtTermConstraintText.Visible = False
            txtTermConstraintDescription.Visible = False
            lblConstraint.Visible = False
            lblDescription.Visible = False
        End If

        If Not MyBase.IsLoading Then
            If radioInternal.Checked Then
                ButNewItem.Focus()
                Constraint.TypeOfTextConstraint = TextConstrainType.Internal
                SetInternalCodedValues()
                mFileManager.FileEdited = True
                'IMCN: Added 20 October 2009
                RefreshButtons()
            End If
        End If
    End Sub


    Private Sub radioTerminology_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioTerminology.CheckedChanged

        If radioTerminology.Checked Then
            gbAllowableValues.Visible = False
            txtTermConstraintText.Visible = True
            txtTermConstraintDescription.Visible = True
            lblConstraint.Visible = True
            lblDescription.Visible = True
        End If

        If Not MyBase.IsLoading Then
            If radioTerminology.Checked Then
                Constraint.TypeOfTextConstraint = TextConstrainType.Terminology
                MyBase.IsLoading = True  ' avoids replacing the text

                If Constraint.ConstraintCode = "" OrElse Not mFileManager.OntologyManager.Ontology.HasTermCode(Constraint.ConstraintCode) Then
                    mConstraintTerm = mFileManager.OntologyManager.AddConstraint(Filemanager.GetOpenEhrTerm(139, "New constraint"))
                    Constraint.ConstraintCode = mConstraintTerm.Code
                    txtTermConstraintText.Text = mConstraintTerm.Text
                    txtTermConstraintDescription.Text = mConstraintTerm.Description
                Else
                    mConstraintTerm = mFileManager.OntologyManager.GetTerm(Constraint.ConstraintCode)
                    txtTermConstraintText.Text = mConstraintTerm.Text
                    txtTermConstraintDescription.Text = mConstraintTerm.Description
                End If

                txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))
                Constraint.HasAssumedValue = False
                MyBase.IsLoading = False

                txtTermConstraintText.Focus()
                mFileManager.FileEdited = True
                'IMCN: Added 20 October 2009
                RefreshButtons()
            End If
        End If
    End Sub

    Private Sub radioText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioText.CheckedChanged
        If radioText.Checked Then
            'SRH: Dec 16 2008 - backward compatibility for existing textural constraints
            If (Constraint.TypeOfTextConstraint = TextConstrainType.Text) And (Constraint.AllowableValues.Codes.Count > 0) Then
                gbAllowableValues.Visible = True
            Else
                gbAllowableValues.Visible = False
            End If

            txtTermConstraintText.Visible = False
            txtTermConstraintDescription.Visible = False
            lblConstraint.Visible = False
            lblDescription.Visible = False
        End If

        If Not MyBase.IsLoading Then
            If radioText.Checked Then
                Constraint.TypeOfTextConstraint = TextConstrainType.Text
                mFileManager.FileEdited = True
                'IMCN: Added 20 October 2009
                RefreshButtons()
            End If
        End If
    End Sub

    Private Sub txtTermConstraintDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermConstraintDescription.TextChanged
        If Not MyBase.IsLoading Then
            mFileManager.OntologyManager.SetDescription(txtTermConstraintDescription.Text, _
                    Me.Constraint.ConstraintCode)
            'Remember if changes constraint type
            Me.mConstraintTerm.Description = txtTermConstraintDescription.Text

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub txtTermConstraintText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermConstraintText.TextChanged
        If Not MyBase.IsLoading Then
            mFileManager.OntologyManager.SetText(txtTermConstraintText.Text, _
                    Me.Constraint.ConstraintCode)
            'Remember if changes constraint type
            Me.mConstraintTerm.Text = txtTermConstraintText.Text
        End If
    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClearText.Click

        If MyBase.IsLoading Then Return

        Me.Constraint.HasAssumedValue = False
        Me.txtAssumedValue.Text = String.Format("({0})", Filemanager.GetOpenEhrTerm(34, "none"))

        mFileManager.FileEdited = True

    End Sub


    Private Sub MenuItemAddExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAddExisting.Click
        Me.butAddItem_Click(sender, e)
    End Sub

    Private Sub listAllowableValues_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.DoubleClick, MenuItemEdit.Click
        If Me.listAllowableValues.SelectedIndex > -1 Then
            Dim s(1) As String

            ' get the term that is selected
            '       Dim t As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(CType(Me.listAllowableValues.SelectedItem, DataRowView).Item(1)))
            '// Changed IMCN: 20 October 2009
            Dim t As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(Me.listAllowableValues.SelectedValue))
            If Not t Is Nothing Then
                s = OceanArchetypeEditor.Instance.GetInput(t, Me.ParentForm)

                If s(0) <> "" Then
                    mFileManager.OntologyManager.SetText(t)
                    mFileManager.OntologyManager.SetDescription(t.Description, t.Code)
                    '// Added IMCN: 20 October 2009
                    RefreshlistAllowableValues(Me.listAllowableValues.SelectedIndex)
                    RefreshButtons()
                    mFileManager.FileEdited = True
                Else
                    Return
                End If
            End If
        End If

    End Sub

    Private Sub ContextMenuListAllowableValues_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuListAllowableValues.Popup
        If Me.listAllowableValues.SelectedIndex > -1 Then
            '// ChangedIMCN: 20 October 2009)
            MenuItemEdit.Text = String.Format("{0} {1}", Filemanager.GetOpenEhrTerm(592, "Edit "), Me.listAllowableValues.Text)
            MenuItemEdit.Visible = True
            MenuItemRemove.Text = String.Format("{0} {1}", Filemanager.GetOpenEhrTerm(152, "Remove "), Me.listAllowableValues.Text)
            MenuItemRemove.Visible = True
        Else
            MenuItemEdit.Visible = False
            MenuItemRemove.Visible = False
        End If
    End Sub
    'IMCN: 19 October 2009
    Private Sub butMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        If Me.listAllowableValues.SelectedIndex > -1 Then
            Dim atCode As String
            Dim currIndex, newindex As Integer

            atCode = CStr(Me.listAllowableValues.SelectedValue)
            currIndex = Me.listAllowableValues.SelectedIndex
            If currIndex > 0 Then
                newindex = currIndex - 1
                Me.Constraint.AllowableValues.Codes.RemoveAt(currIndex)
                Me.Constraint.AllowableValues.Codes.Insert(newindex, atCode)
                RefreshlistAllowableValues(newindex)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If

        End If
    End Sub
    'IMCN: 19 October 2009
    Private Sub butMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        If Me.listAllowableValues.SelectedIndex > -1 Then
            Dim atCode As String
            Dim currIndex, newindex As Integer
            atCode = CStr(Me.listAllowableValues.SelectedValue)
            currIndex = Me.listAllowableValues.SelectedIndex
            If currIndex < Me.Constraint.AllowableValues.Codes.Count - 1 Then
                newindex = currIndex + 1
                Me.Constraint.AllowableValues.Codes.RemoveAt(currIndex)
                Me.Constraint.AllowableValues.Codes.Insert(newindex, atCode)
                Me.RefreshlistAllowableValues(newindex)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub
    'IMCN: 19 October 2009
    ' If internal code list is not empty allow export to tab-separated list via clipboard
    Private Sub butnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyList.Click

        If Me.radioInternal.Checked And (Me.Constraint.AllowableValues.Codes.Count > 0) Then
            Dim atCode As String
            Dim aTerm As RmTerm
            Dim ClipText As String = ""

            For Each atCode In Me.Constraint.AllowableValues.Codes()
                aTerm = mFileManager.OntologyManager.GetTerm(atCode)
                ClipText = ClipText + String.Format("{1}{0}{2}{3}", vbTab, aTerm.Text, aTerm.Description, vbLf)
            Next
            Clipboard.SetText(ClipText)
            MessageBox.Show("The list of internal codes has been copied to Clipboard,", _
                            AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            RefreshButtons()
        End If

    End Sub

    '// IMCN: 19 October 2009
    '// If internal code list is empty allow import of tab-separated list via clipboard
    '// Format expected is 2 columns with a TAB separator and LF line terminator
    '// e.g. at000001<TAB>New code<LF>

    Private Sub btnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPasteList.Click

        If Me.radioInternal.Checked And (Me.Constraint.AllowableValues.Codes.Count = 0) Then
            If (Me.Constraint.AllowableValues.Codes.Count <> 0) Then
                MessageBox.Show("A Clipboard paste can only be made to an empty list", AE_Constants.Instance.MessageBoxCaption, _
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim row As String
                Dim Term As RmTerm
                Dim rows As String() = Nothing

                rows = Clipboard.GetText().Split(CChar(vbLf))
                ' Parse clipboard text - tab separator
                For Each row In rows
                    'Skip empty rows
                    If row.Length <> 0 Then
                        Try
                            Dim columns As String()
                            columns = row.Split(CChar(vbTab))
                            Term = mFileManager.OntologyManager.AddTerm(columns(0), columns(1))
                            Me.Constraint.AllowableValues.Codes.Add(Term.Code)
                        Catch ex As Exception
                            MessageBox.Show("An error occured when importing the Clipboard" & vbCrLf & _
                            "The import expects a tab-separated format e.g. at0001<TAB>New code<LF>", _
                            AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Next

                RefreshlistAllowableValues(Me.Constraint.AllowableValues.Codes.Count - 1)
                mFileManager.FileEdited = True
                RefreshButtons()
            End If
        End If
    End Sub
    '// IMCN: 20 October 2009
    '// Refreshes button Enabled states
    Private Sub RefreshButtons()

        Dim EmptyList As Boolean = (Me.Constraint.AllowableValues.Codes.Count = 0)
        Dim EndOfList As Boolean = (Me.listAllowableValues.SelectedIndex = Me.Constraint.AllowableValues.Codes.Count - 1)
        Dim StartOfList As Boolean = (Me.listAllowableValues.SelectedIndex = 0)

        Me.btnMoveUp.Enabled = Not EmptyList And Not StartOfList
        Me.btnMoveDown.Enabled = Not EmptyList And Not EndOfList
        Me.butRemoveItem.Enabled = Not EmptyList
        Me.btnCopyList.Enabled = Not EmptyList
        Me.btnPasteList.Enabled = EmptyList
    End Sub

    Private Sub listAllowableValues_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.SelectedIndexChanged
        '// IMCN: 20 October 2009
        RefreshButtons()
    End Sub

    '// IMCN: 20 October 2009
    Private Sub MenuItemRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRemove.Click
        Me.butRemoveItem_Click(sender, e)
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

