'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TextConstraintControl))
        Me.gbAllowableValues = New System.Windows.Forms.GroupBox
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
        Me.MenuItemCopyAll = New System.Windows.Forms.MenuItem
        Me.MenuItemPasteAll = New System.Windows.Forms.MenuItem
        Me.MenuItemCancelCopy = New System.Windows.Forms.MenuItem
        Me.MenuItemAddExisting = New System.Windows.Forms.MenuItem
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
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
        Me.gbAllowableValues.Controls.Add(Me.ButNewItem)
        Me.gbAllowableValues.Controls.Add(Me.butDefaultItem)
        Me.gbAllowableValues.Controls.Add(Me.butRemoveItem)
        Me.gbAllowableValues.Controls.Add(Me.txtAssumedValue)
        Me.gbAllowableValues.Controls.Add(Me.butAddItem)
        Me.gbAllowableValues.Controls.Add(Me.listAllowableValues)
        Me.gbAllowableValues.Location = New System.Drawing.Point(8, 40)
        Me.gbAllowableValues.Name = "gbAllowableValues"
        Me.gbAllowableValues.Size = New System.Drawing.Size(360, 192)
        Me.gbAllowableValues.TabIndex = 36
        Me.gbAllowableValues.TabStop = False
        '
        'ButNewItem
        '
        Me.ButNewItem.Image = CType(resources.GetObject("ButNewItem.Image"), System.Drawing.Image)
        Me.ButNewItem.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButNewItem.Location = New System.Drawing.Point(15, 24)
        Me.ButNewItem.Name = "ButNewItem"
        Me.ButNewItem.Size = New System.Drawing.Size(24, 24)
        Me.ButNewItem.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.ButNewItem, "Add a new term")
        '
        'butDefaultItem
        '
        Me.butDefaultItem.Location = New System.Drawing.Point(11, 165)
        Me.butDefaultItem.Name = "butDefaultItem"
        Me.butDefaultItem.Size = New System.Drawing.Size(135, 24)
        Me.butDefaultItem.TabIndex = 8
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
        Me.butRemoveItem.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.butRemoveItem, "Remove term")
        '
        'txtAssumedValue
        '
        Me.txtAssumedValue.ContextMenu = Me.ContextMenuClearText
        Me.txtAssumedValue.Location = New System.Drawing.Point(159, 165)
        Me.txtAssumedValue.Name = "txtAssumedValue"
        Me.txtAssumedValue.ReadOnly = True
        Me.txtAssumedValue.Size = New System.Drawing.Size(177, 22)
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
        Me.butAddItem.TabIndex = 4
        Me.butAddItem.Text = "..."
        Me.ToolTip1.SetToolTip(Me.butAddItem, "Add a term that is already defined")
        '
        'listAllowableValues
        '
        Me.listAllowableValues.ContextMenu = Me.ContextMenuListAllowableValues
        Me.listAllowableValues.DisplayMember = "Text"
        Me.listAllowableValues.ItemHeight = 16
        Me.listAllowableValues.Location = New System.Drawing.Point(46, 20)
        Me.listAllowableValues.Name = "listAllowableValues"
        Me.listAllowableValues.Size = New System.Drawing.Size(306, 132)
        Me.listAllowableValues.TabIndex = 3
        Me.listAllowableValues.ValueMember = "Code"
        '
        'ContextMenuListAllowableValues
        '
        Me.ContextMenuListAllowableValues.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemEdit, Me.MenuItemCopyAll, Me.MenuItemPasteAll, Me.MenuItemCancelCopy, Me.MenuItemAddExisting})
        '
        'MenuItemEdit
        '
        Me.MenuItemEdit.Index = 0
        Me.MenuItemEdit.Text = "Edit"
        Me.MenuItemEdit.Visible = False
        '
        'MenuItemCopyAll
        '
        Me.MenuItemCopyAll.Index = 1
        Me.MenuItemCopyAll.Text = "Copy all"
        '
        'MenuItemPasteAll
        '
        Me.MenuItemPasteAll.Enabled = False
        Me.MenuItemPasteAll.Index = 2
        Me.MenuItemPasteAll.Text = "Paste all"
        '
        'MenuItemCancelCopy
        '
        Me.MenuItemCancelCopy.Index = 3
        Me.MenuItemCancelCopy.Text = "Cancel copy"
        Me.MenuItemCancelCopy.Visible = False
        '
        'MenuItemAddExisting
        '
        Me.MenuItemAddExisting.Index = 4
        Me.MenuItemAddExisting.Text = "Add existing code(s)"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 24)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Description:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 24)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Constraint:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'radioInternal
        '
        Me.radioInternal.Location = New System.Drawing.Point(120, 6)
        Me.radioInternal.Name = "radioInternal"
        Me.radioInternal.Size = New System.Drawing.Size(120, 32)
        Me.radioInternal.TabIndex = 32
        Me.radioInternal.Text = "Internal codes"
        '
        'radioText
        '
        Me.radioText.Location = New System.Drawing.Point(9, 6)
        Me.radioText.Name = "radioText"
        Me.radioText.Size = New System.Drawing.Size(103, 32)
        Me.radioText.TabIndex = 31
        Me.radioText.Text = "Free text or coded"
        '
        'radioTerminology
        '
        Me.radioTerminology.Location = New System.Drawing.Point(248, 6)
        Me.radioTerminology.Name = "radioTerminology"
        Me.radioTerminology.Size = New System.Drawing.Size(128, 32)
        Me.radioTerminology.TabIndex = 33
        Me.radioTerminology.Text = "Terminology"
        '
        'txtTermConstraintDescription
        '
        Me.txtTermConstraintDescription.Location = New System.Drawing.Point(56, 128)
        Me.txtTermConstraintDescription.Multiline = True
        Me.txtTermConstraintDescription.Name = "txtTermConstraintDescription"
        Me.txtTermConstraintDescription.Size = New System.Drawing.Size(288, 56)
        Me.txtTermConstraintDescription.TabIndex = 35
        Me.txtTermConstraintDescription.Text = ""
        Me.txtTermConstraintDescription.Visible = False
        '
        'txtTermConstraintText
        '
        Me.txtTermConstraintText.Location = New System.Drawing.Point(56, 72)
        Me.txtTermConstraintText.Name = "txtTermConstraintText"
        Me.txtTermConstraintText.Size = New System.Drawing.Size(288, 22)
        Me.txtTermConstraintText.TabIndex = 34
        Me.txtTermConstraintText.Text = ""
        Me.txtTermConstraintText.Visible = False
        '
        'TextConstraintControl
        '
        Me.Controls.Add(Me.gbAllowableValues)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.radioInternal)
        Me.Controls.Add(Me.radioText)
        Me.Controls.Add(Me.radioTerminology)
        Me.Controls.Add(Me.txtTermConstraintDescription)
        Me.Controls.Add(Me.txtTermConstraintText)
        Me.Name = "TextConstraintControl"
        Me.Size = New System.Drawing.Size(376, 240)
        Me.gbAllowableValues.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mAllowedValuesDataView As DataView
    Private mTempConstraint As Constraint_Text

    Private Shadows ReadOnly Property Constraint() As Constraint_Text
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Text)

            Return CType(MyBase.Constraint, Constraint_Text)

        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        ' set constraint values on control

        If IsState Then
            Me.butDefaultItem.Visible = True
            Me.txtAssumedValue.Visible = True
        End If

        Select Case Me.Constraint.TypeOfTextConstraint
            Case TextConstrainType.Text
                Dim s As String
                Me.listAllowableValues.DataSource = Nothing
                Me.listAllowableValues.Items.Clear()
                Me.radioText.Checked = True

                For Each s In Me.Constraint.AllowableValues.Codes
                    Me.listAllowableValues.Items.Add(s)
                Next
                Me.txtAssumedValue.Text = CStr(Me.Constraint.AssumedValue)

                ' clear the Terminology fields
                Me.txtTermConstraintText.Text = ""
                Me.txtTermConstraintDescription.Text = ""

            Case TextConstrainType.Internal
                Me.radioInternal.Checked = True

                If Me.Constraint.AllowableValues.Codes.Count > 0 Then
                    SetAllowableValuesFilter(Me.Constraint)

                    Me.listAllowableValues.DataSource = mAllowedValuesDataView
                    Me.listAllowableValues.DisplayMember = "Text"
                    Me.listAllowableValues.ValueMember = "Code"

                    ' now look up the default
                    If Me.Constraint.HasAssumedValue Then
                        Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(CStr(Me.Constraint.AssumedValue))
                        Me.txtAssumedValue.Text = aTerm.Text

                    Else
                        Me.txtAssumedValue.Text = "(none)"
                    End If

                Else
                    ' to hide values as they are not override if the filter is set
                    Me.listAllowableValues.DataSource = Nothing
                    Me.listAllowableValues.Items.Clear()
                    Me.txtAssumedValue.Text = "(none)"

                End If

                ' clear the Terminology fields
                Me.txtTermConstraintText.Text = ""
                Me.txtTermConstraintDescription.Text = ""

            Case TextConstrainType.Terminology
                Me.radioTerminology.Checked = True
                Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm( _
                        Me.Constraint.ConstraintCode)
                Me.txtTermConstraintText.Text = aTerm.Text
                Me.txtTermConstraintDescription.Text = aTerm.Description

        End Select

    End Sub

    Private Sub butAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddItem.Click


        If MyBase.IsLoading Then Return

        Dim s() As String = ArchetypeEditor.Instance.ChooseInternal(mFileManager)

        If s Is Nothing Then Return

        For i As Integer = 0 To s.Length - 1
            Me.Constraint.AllowableValues.Codes.Add(s(i))
        Next

        SetAllowableValuesFilter(Me.Constraint)
        Me.listAllowableValues.DataSource = mAllowedValuesDataView
        Me.listAllowableValues.DisplayMember = "Text"
        Me.listAllowableValues.ValueMember = "Code"

        mFileManager.FileEdited = True

    End Sub

    Private Sub SetAllowableValuesFilter(ByVal TextConstraint As Constraint_Text)
        Dim i As Integer
        Dim cd As String
        cd = "Code = "
        For i = 0 To TextConstraint.AllowableValues.Codes.Count - 1
            cd = cd & "'" & TextConstraint.AllowableValues.Codes.Item(i) & "'"
            If i < (TextConstraint.AllowableValues.Codes.Count - 1) Then
                cd = cd & " OR Code = "
            End If
        Next

        mAllowedValuesDataView.RowFilter = String.Format("({0}) AND (id = '{1}')", _
                cd, mFileManager.OntologyManager.LanguageCode)
    End Sub

    Private Sub butDefaultItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDefaultItem.Click

        If Me.listAllowableValues.SelectedIndex > -1 Then
            Dim s As String
            If Me.radioInternal.Checked Then
                ' the code is the default value, show the text
                Me.txtAssumedValue.Text = CType(CType(Me.listAllowableValues.SelectedItem, DataRowView).Item("Text"), String)
                s = CStr(Me.listAllowableValues.SelectedValue)
                Me.txtAssumedValue.Tag = s

            ElseIf Me.radioText.Checked Then
                ' the text is default and the value
                s = Me.listAllowableValues.Text
                Me.txtAssumedValue.Text = s

            End If

            Me.Constraint.AssumedValue = s

            ' automatically sets hasAssumedValue
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButNewItem.Click

        If Me.radioInternal.Checked Then
            Dim s(1) As String
            s = ArchetypeEditor.Instance.GetInput("Enter the new item:", "Description")

            If s(0) <> "" Then
                Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(s(0), s(1))
                Dim term_id As String = aTerm.Code

                If Me.listAllowableValues.DataSource Is Nothing Then
                    mAllowedValuesDataView.RowFilter = String.Format("(Code = '{0}') AND (id = '{1}')", _
                            aTerm.Code, mFileManager.OntologyManager.LanguageCode)

                    Me.listAllowableValues.DataSource = mAllowedValuesDataView
                    Me.listAllowableValues.DisplayMember = "Text"
                    Me.listAllowableValues.ValueMember = "Code"
                Else
                    ' add this id to the term filter
                    Dim f As String = mAllowedValuesDataView.RowFilter
                    Dim i As Integer = InStr(f, ") AND (") - 1
                    Dim str As String = " OR Code = '" & term_id & "'"
                    mAllowedValuesDataView.RowFilter = f.Insert(i, str)
                End If

                ' add the code to the constraint
                Me.Constraint.AllowableValues.Codes.Add(term_id)

            Else
                Return

            End If

        ElseIf Me.radioText.Checked Then
            Dim s As String
            s = ArchetypeEditor.Instance.GetInput("Enter the new item:")

            If s <> "" Then
                Me.listAllowableValues.Items.Add(s)
                't = CType(Aen.Constraint, Constraint_Text)
                Me.Constraint.AllowableValues.Codes.Add(s)

            Else
                Return

            End If

        End If

        mFileManager.FileEdited = True

    End Sub

    Private Sub butRemoveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveItem.Click
        Dim i As Integer = Me.listAllowableValues.SelectedIndex

        If i > -1 Then
            RemoveAllowableValue(i)
        End If
    End Sub

    Private Sub RemoveAllowableValue(ByVal Index As Integer)
        
        Try
            Dim defaultText As String = Me.txtAssumedValue.Text

            If Me.radioText.Checked Then
                If Me.listAllowableValues.Items.Count > 0 Then

                    If MessageBox.Show(AE_Constants.Instance.Remove & _
                            CStr(Me.listAllowableValues.Items(Index)), _
                            AE_Constants.Instance.MessageBoxCaption, _
                            MessageBoxButtons.OKCancel) = DialogResult.OK Then

                        If CStr(Me.listAllowableValues.Items(Index)) = defaultText Then
                            Me.txtAssumedValue.Text = "(none)"
                            Me.Constraint.HasAssumedValue = False
                        End If

                        Me.Constraint.AllowableValues.Codes.Remove( _
                                CStr(Me.listAllowableValues.Items(Index)))
                        Me.listAllowableValues.Items.RemoveAt(Index)

                    End If

                End If

            ElseIf Me.radioInternal.Checked Then

                If mAllowedValuesDataView.Count > 0 Then

                    If MessageBox.Show(AE_Constants.Instance.Remove & _
                            CStr(Me.listAllowableValues.Text), _
                            AE_Constants.Instance.MessageBoxCaption, _
                            MessageBoxButtons.OKCancel) = DialogResult.OK Then

                        ' have to delete this from all languages
                        If CStr(Me.listAllowableValues.Text) = defaultText Then
                            Me.Constraint.HasAssumedValue = False
                            Me.txtAssumedValue.Text = "(none)"

                        End If

                        Dim code As String = CStr(Me.listAllowableValues.SelectedValue) '("Code")
                        Me.Constraint.AllowableValues.Codes.Remove(code)

                        'FIXME - cannot remove terms as are reused in the archetype
                        'ParentFrm.mFileManager.OntologyManager.RemoveTerm(Code)
                        If Me.Constraint.AllowableValues.Codes.Count = 0 Then
                            Me.listAllowableValues.DataSource = Nothing
                            Me.listAllowableValues.Items.Clear()

                        Else
                            Dim f As String = mAllowedValuesDataView.RowFilter
                            Dim i As Integer = InStr(f, "Code = '" & code & "' OR ")
                            Dim Lengthof As Integer = ("Code = '" & code & "' OR ").Length - 1
                            If i = 0 Then
                                'might be the last code
                                i = InStr(f, " OR Code = '" & code & "'")
                                Lengthof = (" OR Code = '" & code & "'").Length - 1
                            End If
                            If i = 0 Then
                                'might be the only code
                                i = InStr(f, "Code = '" & code & "'")
                                Lengthof = ("Code = '" & code & "'").Length - 1
                            End If
                            If i <> 0 Then
                                f = f.Substring(0, i - 1) & f.Substring(i + Lengthof)
                            End If
                            mAllowedValuesDataView.RowFilter = f
                        End If

                    End If

                End If

                mFileManager.FileEdited = True
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

    End Sub

    Private Sub radioInternal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioInternal.CheckedChanged
        If Me.radioInternal.Checked Then
            Me.gbAllowableValues.Visible = True
            Me.txtTermConstraintText.Visible = False
            Me.txtTermConstraintDescription.Visible = False
            Me.butAddItem.Visible = True
        Else
            Me.butAddItem.Visible = False
        End If

        If MyBase.IsLoading Then Return

        If Me.radioInternal.Checked Then

            If Me.Constraint.TypeOfTextConstraint = TextConstrainType.Text Then

                If Me.listAllowableValues.Items.Count > 0 Then

                    'data will be lost so check it is OK
                    If MessageBox.Show( _
                               AE_Constants.Instance.Convert_constraint_loose_data, _
                               AE_Constants.Instance.MessageBoxCaption, _
                               MessageBoxButtons.OKCancel, _
                               MessageBoxIcon.Warning, _
                               MessageBoxDefaultButton.Button2) = DialogResult.OK Then


                        ' could offer transform but risky as many variables!
                        Me.listAllowableValues.DataSource = Nothing
                        Me.listAllowableValues.Items.Clear()
                        Me.txtAssumedValue.Text = "(none)"
                        Me.Constraint.HasAssumedValue = False

                        Me.Constraint.AllowableValues.Codes.Clear()

                        Me.ButNewItem.Focus()
                    Else
                        ' put things back how they were

                        MyBase.IsLoading = True

                        Select Case Me.Constraint.TypeOfTextConstraint
                            Case TextConstrainType.Text
                                Me.radioText.Checked = True

                            Case TextConstrainType.Terminology
                                Me.radioTerminology.Checked = True

                            Case Else
                                Debug.Assert(False)
                        End Select

                        MyBase.IsLoading = False 'tempLoading

                        Exit Sub

                    End If
                Else

                    Me.Constraint.HasAssumedValue = False

                    Me.Constraint.AllowableValues.Codes.Clear()

                    Me.ButNewItem.Focus()

                End If

                Me.Constraint.TypeOfTextConstraint = TextConstrainType.Internal

                mFileManager.FileEdited = True
            End If
        End If

    End Sub

    Private Sub radioTerminology_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioTerminology.CheckedChanged

        If Me.radioTerminology.Checked Then
            Me.gbAllowableValues.Visible = False
            Me.txtTermConstraintText.Visible = True
            Me.txtTermConstraintDescription.Visible = True
        End If

        If MyBase.IsLoading Then Return

        If Me.radioTerminology.Checked Then

            If MessageBox.Show(AE_Constants.Instance.Convert_constraint_loose_data, _
                    AE_Constants.Instance.MessageBoxCaption, _
                    MessageBoxButtons.OKCancel, _
                    MessageBoxIcon.Warning, _
                    MessageBoxDefaultButton.Button2) = DialogResult.OK Then

                If Me.Constraint.ConstraintCode = "" Then
                    Dim term As RmTerm = mFileManager.OntologyManager.AddConstraint("New constraint")
                    Me.Constraint.ConstraintCode = Term.Code
                    MyBase.IsLoading = True  ' avoids replacing the text
                    Me.txtTermConstraintText.Text = "New constraint"
                    MyBase.IsLoading = False

                Else
                    Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(Me.Constraint.ConstraintCode)
                    Me.txtTermConstraintText.Text = term.Text
                    Me.txtTermConstraintDescription.Text = term.Description

                End If

                ' could offer transform but risky as many variables!
                Me.listAllowableValues.DataSource = Nothing
                Me.listAllowableValues.Items.Clear()
                Me.txtAssumedValue.Text = "(none)"
                Me.Constraint.HasAssumedValue = False

                Me.Constraint.AllowableValues.Codes.Clear()

                Me.txtTermConstraintText.Focus()
            Else
                ' put things back how they were
                MyBase.IsLoading = True
                Select Case Me.Constraint.TypeOfTextConstraint
                    Case TextConstrainType.Text
                        Me.radioText.Checked = True
                    Case TextConstrainType.Internal
                        Me.radioInternal.Checked = True
                End Select
                MyBase.IsLoading = True

                Exit Sub

            End If

            Me.Constraint.TypeOfTextConstraint = TextConstrainType.Terminology

            mFileManager.FileEdited = True

        End If

    End Sub

    Private Sub radioText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioText.CheckedChanged

        If Me.radioText.Checked Then
            Me.gbAllowableValues.Visible = True
            Me.txtTermConstraintText.Visible = False
            Me.txtTermConstraintDescription.Visible = False
        End If

        If MyBase.IsLoading Then Return

        If Me.radioText.Checked Then
            If Me.Constraint.TypeOfTextConstraint = TextConstrainType.Internal _
                    AndAlso Me.listAllowableValues.Items.Count > 1 Then

                If MessageBox.Show(AE_Constants.Instance.Convert_internal_text, _
                        AE_Constants.Instance.MessageBoxCaption, _
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, _
                        MessageBoxDefaultButton.Button2) = DialogResult.OK Then

                    Dim cp As New CodePhrase
                    Dim aTerm As RmTerm

                    For Each ss As String In Me.Constraint.AllowableValues.Codes
                        aTerm = mFileManager.OntologyManager.GetTerm(ss)
                        cp.Codes.Add(aTerm.Text)
                    Next

                    If Me.Constraint.HasAssumedValue Then
                        aTerm = mFileManager.OntologyManager.GetTerm(CStr(Me.Constraint.AssumedValue))
                    End If

                    Me.Constraint.AllowableValues = cp

                    If Me.Constraint.HasAssumedValue Then
                        Me.Constraint.AssumedValue = aTerm.Text
                    End If

                    Me.Constraint.ConstraintCode = ""

                    Me.ButNewItem.Focus()

                Else
                    ' put things back how they were
                    MyBase.IsLoading = True
                    Me.radioInternal.Checked = True
                    MyBase.IsLoading = False

                    Exit Sub
                End If

            End If

            Me.Constraint.TypeOfTextConstraint = TextConstrainType.Text

            Me.listAllowableValues.DataSource = Nothing

            mFileManager.FileEdited = True

        End If
    End Sub

    Private Sub txtTermConstraintDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermConstraintDescription.TextChanged
        If Not MyBase.IsLoading Then
            mFileManager.OntologyManager.SetDescription(txtTermConstraintDescription.Text, _
                    Me.Constraint.ConstraintCode)

            mFileManager.FileEdited = True

        End If
    End Sub

    Private Sub txtTermConstraintText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermConstraintText.TextChanged
        If Not MyBase.IsLoading Then
            mFileManager.OntologyManager.SetText(txtTermConstraintText.Text, _
                    Me.Constraint.ConstraintCode)
        End If
    End Sub

    Private Sub MenuClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClearText.Click

        If MyBase.IsLoading Then Return

        Me.Constraint.HasAssumedValue = False
        Me.txtAssumedValue.Text = "(none)"

        mFileManager.FileEdited = True

    End Sub

    Private Sub MenuItemCopyAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyAll.Click

        Me.MenuItemPasteAll.Enabled = True
        Me.MenuItemCancelCopy.Visible = True
        Me.MenuItemCopyAll.Enabled = False

        mTempConstraint = Me.Constraint

    End Sub

    Private Sub MenuItemAddExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAddExisting.Click
        Me.butAddItem_Click(sender, e)
    End Sub

    Private Sub MenuItemPasteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPasteAll.Click
        If mTempConstraint.Type = ConstraintType.Text Then

            If Me.Constraint.TypeOfTextConstraint = mTempConstraint.TypeOfTextConstraint Then
                MyBase.Constraint = mTempConstraint.copy

            ElseIf Me.Constraint.TypeOfTextConstraint = TextConstrainType.Internal _
                    AndAlso mTempConstraint.TypeOfTextConstraint = TextConstrainType.Text Then

                If MessageBox.Show(AE_Constants.Instance.Convert_string_text, _
                        AE_Constants.Instance.MessageBoxCaption, _
                        MessageBoxButtons.OKCancel, _
                        MessageBoxIcon.Question) = DialogResult.OK Then

                    For i As Integer = 0 To mTempConstraint.AllowableValues.Codes.Count - 1
                        Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm( _
                                mTempConstraint.AllowableValues.Codes.Item(i))
                        Me.Constraint.AllowableValues.Codes.Add(aTerm.Code)

                        If mTempConstraint.HasAssumedValue Then
                            If CStr(mTempConstraint.AssumedValue) _
                                    = mTempConstraint.AllowableValues.Codes.Item(i) Then
                                Me.Constraint.AssumedValue = aTerm.Code

                                Me.txtAssumedValue.Text = mTempConstraint.AllowableValues.Codes.Item(i)
                            End If
                        End If
                    Next

                Else
                    Return ' cancel

                End If

            ElseIf Me.Constraint.TypeOfTextConstraint = TextConstrainType.Text _
                    AndAlso mTempConstraint.TypeOfTextConstraint = TextConstrainType.Internal Then

                If MessageBox.Show(AE_Constants.Instance.Convert_text_string, _
                        AE_Constants.Instance.MessageBoxCaption, _
                        MessageBoxButtons.OKCancel, _
                        MessageBoxIcon.Question) = DialogResult.OK Then

                    For i As Integer = 0 To mTempConstraint.AllowableValues.Codes.Count - 1
                        Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm( _
                                mTempConstraint.AllowableValues.Codes.Item(i))

                        Me.Constraint.AllowableValues.Codes.Add(aTerm.Code)

                        If mTempConstraint.HasAssumedValue Then
                            If CStr(mTempConstraint.AssumedValue) = mTempConstraint.AllowableValues.Codes.Item(i) Then
                                Me.Constraint.AssumedValue = aTerm.Code
                            End If

                        End If

                    Next

                Else
                    Return ' cancel

                End If

            End If

        End If

        mFileManager.FileEdited = True

    End Sub

    Private Sub MenuItemCancelCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCancelCopy.Click
        Me.MenuItemPasteAll.Enabled = False
        Me.MenuItemCancelCopy.Visible = False
        Me.MenuItemCopyAll.Enabled = True
        mTempConstraint = Nothing
    End Sub

    Private Sub listAllowableValues_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listAllowableValues.DoubleClick, MenuItemEdit.Click
        If Me.listAllowableValues.SelectedIndex > -1 Then
            Dim s(1) As String

            ' get the term that is selected
            Dim t As RmTerm = mFilemanager.OntologyManager.GetTerm(Me.Constraint.AllowableValues.Codes.Item(Me.listAllowableValues.SelectedIndex))

            If Not t Is Nothing Then
                s = ArchetypeEditor.Instance.GetInput(t)

                If s(0) <> "" Then
                    mFilemanager.OntologyManager.SetText(t)
                    mFilemanager.OntologyManager.SetDescription(t.Description, t.Code)
                Else
                    Return
                End If
            End If
        End If

    End Sub

    Private Sub ContextMenuListAllowableValues_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuListAllowableValues.Popup
        If Me.listAllowableValues.SelectedIndex > -1 Then
            MenuItemEdit.Text = "Edit " & CType(CType(Me.listAllowableValues.SelectedItem, DataRowView).Item("Text"), String)
            MenuItemEdit.Visible = True
        Else
            MenuItemEdit.Visible = False
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