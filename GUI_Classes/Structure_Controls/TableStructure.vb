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

Public Class TableStructure
    Inherits EntryStructure

    Private mArchetypeTable As DataTable
    Private Delegate Function delegateGetIconIndexForRow(ByVal row As Integer) As Integer
    Private TableArchetypeStyle As DataGridTableStyle
    Private RowHeadings As DataGridTextBoxColumn
    Private IconColumn As DataGridIconOnlyColumn
    Private mMenuItemAddRow As MenuItem
    Private mIsRotated As Boolean = True
    Private mRow As RmCluster
    Private mIsLoading As Boolean
    Friend WithEvents MenuRemoveColumnOrRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveColumn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Public Shadows Event CurrentItemChanged(ByVal an_archetype_node As ArchetypeNode)
    Private mKeyColumns As New Collection

    'ToDo: Add specialisation to tables

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal rm As RmTable, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(rm, a_file_manager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mIsLoading = True
        SetArchetypeTable()

        mRow = CType(rm.Children.items(0), RmCluster)

        Dim d_row As DataRow

        If mRow.Children.Count > 0 Then
            Dim rmStr As RmStructure
            Dim archNode As ArchetypeNode = Nothing

            Me.butChangeDataType.Visible = True

            If rm.isRotated Then
                For i As Integer = 0 To mRow.Children.Count - 1
                    'set column heading as always rotated - at the moment
                    rmStr = mRow.Children.items(i)
                    If rmStr.Type = StructureType.Element Then
                        archNode = New ArchetypeElement(CType(rmStr, RmElement), mFileManager)
                    ElseIf rmStr.Type = StructureType.Slot Then
                        archNode = New ArchetypeNodeAnonymous(CType(rmStr, RmSlot))
                    Else
                        Debug.Assert(False, "Type not handled")
                        Throw New Exception("Table row of invalid type")
                    End If

                    If i < rm.NumberKeyColumns AndAlso TypeOf archNode Is ArchetypeElement Then
                        AddColumnElement(CType(archNode, ArchetypeElement))
                    Else
                        d_row = mArchetypeTable.NewRow
                        d_row.Item(0) = Me.ImageIndexForItem(archNode)
                        d_row.Item(1) = archNode.Text
                        d_row.Item(2) = archNode
                        mArchetypeTable.Rows.Add(d_row)
                    End If
                Next
                If mArchetypeTable.Rows.Count > 0 Then
                    SetCurrentItem(CType(mArchetypeTable.Rows(0).Item(2), ArchetypeNode))
                End If
            Else
                Debug.Assert(False, "Unrotated tables are not handled at present")
            End If
        End If

        If mArchetypeTable.Rows.Count = 0 Then
            'FIXME raise error
            Return
        End If

        mIsLoading = False

    End Sub

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        If Not Me.DesignMode Then
            Debug.Assert(False)
        End If
    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New("Table", a_file_manager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        mFileManager.FileEdited = True
    End Sub
    'UserControl overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    ''Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dgGrid As System.Windows.Forms.DataGrid
    Friend WithEvents ContextMenuGrid As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuRenameColumn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRename As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgGrid = New System.Windows.Forms.DataGrid
        Me.ContextMenuGrid = New System.Windows.Forms.ContextMenu
        Me.MenuRename = New System.Windows.Forms.MenuItem
        Me.MenuRenameColumn = New System.Windows.Forms.MenuItem
        Me.MenuRemoveColumnOrRow = New System.Windows.Forms.MenuItem
        Me.MenuRemoveColumn = New System.Windows.Forms.MenuItem
        Me.MenuRemoveRow = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        CType(Me.dgGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgGrid
        '
        Me.dgGrid.CaptionVisible = False
        Me.dgGrid.ContextMenu = Me.ContextMenuGrid
        Me.dgGrid.DataMember = ""
        Me.dgGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgGrid.Location = New System.Drawing.Point(40, 24)
        Me.dgGrid.Name = "dgGrid"
        Me.dgGrid.Size = New System.Drawing.Size(344, 382)
        Me.dgGrid.TabIndex = 38
        '
        'ContextMenuGrid
        '
        Me.ContextMenuGrid.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRename, Me.MenuRemoveColumnOrRow})
        '
        'MenuRename
        '
        Me.MenuRename.Index = 0
        Me.MenuRename.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRenameColumn})
        Me.MenuRename.Text = "Rename"
        '
        'MenuRenameColumn
        '
        Me.MenuRenameColumn.Index = 0
        Me.MenuRenameColumn.Text = "Column"
        '
        'MenuRemoveColumnOrRow
        '
        Me.MenuRemoveColumnOrRow.Index = 1
        Me.MenuRemoveColumnOrRow.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemoveColumn, Me.MenuRemoveRow})
        Me.MenuRemoveColumnOrRow.Text = "Remove"
        '
        'MenuRemoveColumn
        '
        Me.MenuRemoveColumn.Index = 0
        Me.MenuRemoveColumn.Text = "Column"
        '
        'MenuRemoveRow
        '
        Me.MenuRemoveRow.Index = 1
        Me.MenuRemoveRow.Text = "Row"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.Text = "Remove"
        '
        'TableStructure
        '
        Me.Controls.Add(Me.dgGrid)
        Me.Name = "TableStructure"
        Me.Controls.SetChildIndex(Me.dgGrid, 0)
        CType(Me.dgGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub AddColumnElement(ByVal an_element As ArchetypeElement)
        mKeyColumns.Add(an_element)
        For Each t As String In CType(an_element.Constraint, Constraint_Text).AllowableValues.Codes
            AddColumn(mFileManager.OntologyManager.GetTerm(t))
        Next
    End Sub

    'Public Overrides ReadOnly Property Elements() As ArchetypeElement()
    '    Get
    '        Dim i As Integer
    '        i = mKeyColumns.Count
    '        If i > 0 Then
    '            Dim a_e(i) As ArchetypeElement
    '            For i = 1 To mKeyColumns.Count
    '                a_e(i) = CType(mKeyColumns.Item(i), ArchetypeElement)
    '            Next
    '            Return a_e
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    'End Property

    Private Sub TableStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = dgGrid
        If mArchetypeTable Is Nothing Then
            ' no archetype driving constructor
            SetArchetypeTable()
        Else
            If mArchetypeTable.Rows.Count > 0 Then
                dgGrid_CurrentCellChanged(sender, e)
            End If
        End If
        ' add the change structure menu from EntryStructure
        Me.ContextMenuGrid.MenuItems.Add(menuChangeStructure)
    End Sub

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Dim Obj As New ArrayList
            Obj.Add(mArchetypeTable)
            Obj.Add(mKeyColumns)
            Obj.Add(mIsRotated)
            Return Obj
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructureCompound
        Get
            Dim i, n_rows As Integer
            Dim RM_T As RmTable
            Dim element As RmElement

            'sets the cardinality of the children
            RM_T = New RmTable(mNodeId)
            RM_T.Children.Cardinality = Me.mCardinalityControl.Cardinality

            RM_T.isRotated = True
            RM_T.NumberKeyColumns = mKeyColumns.Count

            If mRow Is Nothing Then
                mRow = New RmCluster(mFileManager.OntologyManager.AddTerm("row", "@ internal @").Code)
            Else
                mRow.Children.Clear()
            End If

            n_rows = mArchetypeTable.Rows.Count

            mRow.Children.Cardinality.MinCount = n_rows + mKeyColumns.Count
            mRow.Children.Cardinality.MaxCount = n_rows + mKeyColumns.Count

            For Each row_heading As ArchetypeElement In mKeyColumns
                element = row_heading.RM_Class
                mRow.Children.Add(element)
            Next
            ' then the rows
            For i = 0 To n_rows - 1
                Dim rowStructure As RmStructure
                Dim archetype_node As ArchetypeNode

                archetype_node = CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode)
                rowStructure = archetype_node.RM_Class
                mRow.Children.Add(rowStructure)
            Next

            RM_T.Children.Add(mRow)
            Return RM_T
        End Get
        Set(ByVal Value As RmStructureCompound)
            Dim element As ArchetypeNode
            Dim aStructure As RmStructure
            Dim new_row As DataRow

            MyBase.SetCardinality(Value)

            mControl = dgGrid
            If mArchetypeTable Is Nothing Then
                ' no archetype driving constructor
                SetArchetypeTable()
            End If

            ' handles conversion from other structures
            Me.mArchetypeTable.Rows.Clear()
            mNodeId = Value.NodeId

            mIsLoading = True

            Select Case Value.Type '.TypeName
                Case StructureType.Tree ' "TREE"
                    ProcessNodesToTable(Value.Children)

                Case StructureType.Single ' "SINGLE"
                    aStructure = Value.Children.FirstElementOrElementSlot
                    If Not aStructure Is Nothing Then
                        If aStructure.Type = StructureType.Element Then
                            element = New ArchetypeElement(CType(aStructure, RmElement), mFileManager)
                        Else ' a slot
                            element = New ArchetypeNodeAnonymous(CType(aStructure, RmSlot))
                        End If
                        new_row = mArchetypeTable.NewRow
                        new_row(1) = element.Text
                        new_row(2) = element
                        new_row(0) = Me.ImageIndexForItem(element)
                        mArchetypeTable.Rows.Add(new_row)
                    End If

                Case StructureType.List ' "list"
                    For Each aStructure In Value.Children
                        If aStructure.Type = StructureType.Element Then
                            element = New ArchetypeElement(CType(aStructure, RmElement), mFileManager)
                        Else ' a slot
                            element = New ArchetypeNodeAnonymous(CType(aStructure, RmSlot))
                        End If
                        new_row = mArchetypeTable.NewRow
                        new_row(1) = element.Text
                        new_row(2) = element
                        new_row(0) = Me.ImageIndexForItem(element)
                        mArchetypeTable.Rows.Add(new_row)
                    Next
            End Select

            mIsLoading = False

        End Set
    End Property

    'JAR: 29MAY07, EDT-21 To detect whether items have been added to table
    Public Overrides Function HasData() As Boolean
        If Not mArchetypeTable Is Nothing Then
            Return (mArchetypeTable.Rows.Count > 0)
        Else
            Return False
        End If
    End Function

    Sub ProcessNodesToTable(ByVal ch As Children)
        Dim an_rm_structure As RmStructure

        For Each an_rm_structure In ch
            'If an_rm_structure.TypeName = "Cluster" Then
            If an_rm_structure.Type = StructureType.Cluster Then
                ProcessNodesToTable(CType(an_rm_structure, RmStructureCompound).Children)
            Else
                Dim element As ArchetypeElement
                Dim new_row As DataRow

                element = New ArchetypeElement(CType(an_rm_structure, RmElement), mFileManager)
                new_row = mArchetypeTable.NewRow

                new_row(1) = element.Text
                new_row(2) = element
                new_row(0) = Me.ImageIndexForConstraintType(element.Constraint.Type, element.IsReference)
                mArchetypeTable.Rows.Add(new_row)
            End If
        Next
    End Sub

    Public Overrides Sub Reset()
        Me.mArchetypeTable.Rows.Clear()
    End Sub

    Public Overrides Sub Translate()
        Dim i As Integer

        mIsLoading = True
        If mIsRotated Then
            For i = 0 To mArchetypeTable.Rows.Count - 1
                Dim an_element As ArchetypeNode
                an_element = CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode)
                an_element.Translate()
                mArchetypeTable.Rows(i).Item(1) = an_element.Text
            Next

            i = 2 ' columns start at 2 if rotated
            For Each row_heading As ArchetypeElement In mKeyColumns
                row_heading.Translate()
                TableArchetypeStyle.GridColumnStyles(i).HeaderText = row_heading.Text
                TableArchetypeStyle.GridColumnStyles(i).NullText = "(" & row_heading.Text & ")"
                i += 1
            Next
        Else
            Debug.Assert(False, "TODO")
        End If

        mIsLoading = False
        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs)
        If Me.dgGrid.CurrentRowIndex > -1 AndAlso TypeOf mArchetypeTable.Rows(Me.dgGrid.CurrentCell.RowNumber).Item(2) Is ArchetypeElement Then
            Dim mElement As ArchetypeElement = CType(mArchetypeTable.Rows(Me.dgGrid.CurrentCell.RowNumber).Item(2), ArchetypeElement)
            If mElement.IsReference Then
                MessageBox.Show(AE_Constants.Instance.Cannot_specialise_reference, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            If mElement.Occurrences.IsUnbounded Or mElement.Occurrences.MaxCount > 1 Then
                Dim new_element As ArchetypeElement
                Dim new_row As DataRow
                Dim a_cell As DataGridCell

                new_element = CType(CType(mElement, ArchetypeElement).Copy, ArchetypeElement)
                new_element.Specialise()

                new_row = mArchetypeTable.NewRow
                new_row(1) = new_element.Text
                mArchetypeTable.Rows.InsertAt(new_row, Me.dgGrid.CurrentRowIndex + 1)
                new_row(2) = new_element
                new_row(0) = Me.ImageIndexForConstraintType(new_element.Constraint.Type, CType(new_element.RM_Class, RmElement).isReference)
                ' go to the new entry
                a_cell.RowNumber = Me.dgGrid.CurrentRowIndex + 1
                a_cell.ColumnNumber = 1
                Me.dgGrid.Focus()
                Me.dgGrid.CurrentCell = a_cell

            Else
                mElement.Specialise()
                mArchetypeTable.Rows(Me.dgGrid.CurrentRowIndex).Item(1) = mCurrentItem.Text
            End If
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Assert(False, "Cannot add a reference to a table at present ? required")
    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        Dim cm As New ContextMenu
        Dim a_mi As MenuItem
        If mIsRotated Then
            a_mi = New MenuItem(Filemanager.GetOpenEhrTerm(324, "New Row"))
        Else
            a_mi = New MenuItem(Filemanager.GetOpenEhrTerm(323, "New Column"))
        End If
        mConstraintMenu = New ConstraintContextMenu(AddressOf AddNewElement, mFileManager)
        cm.MenuItems.Add(a_mi)
        a_mi.MergeMenu(mConstraintMenu)

        If mIsRotated Then
            mMenuItemAddRow = New MenuItem(Filemanager.GetOpenEhrTerm(323, "New Column"))
        Else
            mMenuItemAddRow = New MenuItem(Filemanager.GetOpenEhrTerm(323, "New Row"))
        End If
        AddHandler mMenuItemAddRow.Click, AddressOf AddKeyColumn
        cm.MenuItems.Add(mMenuItemAddRow)
        cm.Show(Me.ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Public Overrides Sub SetInitial()
        If Me.dgGrid.VisibleRowCount > 0 Then
            SetCurrentItem(CType(mArchetypeTable.Rows(0).Item(2), ArchetypeNode))
        End If
    End Sub


    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        Dim el As ArchetypeElement
        Dim new_row As DataRow
        Dim a_cell As DataGridCell

        mIsLoading = True
        new_row = mArchetypeTable.NewRow
        new_row(1) = Filemanager.GetOpenEhrTerm(109, "New Element")
        '      Try
        mArchetypeTable.Rows.Add(new_row)
        '     Catch
        '        MessageBox.Show(AE_Constants.Instance.Duplicate_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '       Return
        '      End Try

        el = New ArchetypeElement(CType(new_row(1), String), mFileManager)
        el.Occurrences.MaxCount = 1
        el.Constraint = a_constraint
        new_row(2) = el
        new_row(0) = Me.ImageIndexForConstraintType(a_constraint.Type)
        ' go to the new entry
        a_cell.RowNumber = mArchetypeTable.Rows.Count - 1
        a_cell.ColumnNumber = 1
        Me.dgGrid.Focus()
        SetCurrentItem(el)
        mFileManager.FileEdited = True
        Me.dgGrid.CurrentCell = a_cell
        mIsLoading = False
    End Sub

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub butListdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Sub AddColumn(ByVal a_term As RmTerm)
        Dim col As DataColumn
        Dim cs As New DataGridTextBoxColumn
        Dim c As New DataGridCell

        ' add the column to display
        col = New DataColumn(a_term.Code)
        col.Caption = a_term.Text
        col.DataType = System.Type.GetType("System.String")
        Try
            mArchetypeTable.Columns.Add(col)
        Catch
            MessageBox.Show(AE_Constants.Instance.Duplicate_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        cs.HeaderText = a_term.Text
        cs.NullText = "(" & a_term.Text & ")"
        cs.Width = 75
        cs.ReadOnly = True
        cs.MappingName = a_term.Code
        TableArchetypeStyle.GridColumnStyles.Add(cs)
        If Not mIsLoading Then
            c.RowNumber = 0
            c.ColumnNumber = Me.dgGrid.TableStyles(0).GridColumnStyles.IndexOf(cs)
            Me.dgGrid.CurrentCell = c
        End If
    End Sub

    Sub AddKeyColumn(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String()
        Dim a_term As RmTerm
        Dim element As ArchetypeElement

        ' adds columns if rotated
        If mIsRotated Then
            s = OceanArchetypeEditor.Instance.GetInput(AE_Constants.Instance.Text, AE_Constants.Instance.Description, Me.ParentForm)
            If s(0) <> "" Then
                a_term = mFileManager.OntologyManager.AddTerm(s(0), s(1))
                'If mArchetypeTable.Rows.Count > 0 Then
                If mKeyColumns.Count > 0 Then
                    element = CType(mKeyColumns.Item(mKeyColumns.Count), ArchetypeElement)
                Else
                    element = New ArchetypeElement("row_head", mFileManager)
                    element.Constraint = New Constraint_Text
                    mKeyColumns.Add(element)
                End If
                CType(element.Constraint, Constraint_Text).AllowableValues.Codes.Add(a_term.Code)
                AddColumn(a_term)
                mFileManager.FileEdited = True
            End If
        Else
            Debug.Assert(False, "TODO")
        End If
    End Sub

    Private Sub RemoveColumn(ByVal sender As Object, ByVal e As EventArgs) Handles MenuRemoveColumn.Click
        Dim columnIndex As Integer
        Dim columnLabel As String

        columnIndex = Me.dgGrid.CurrentCell.ColumnNumber
        columnLabel = Me.TableArchetypeStyle.GridColumnStyles(columnIndex).HeaderText

        If columnIndex < 2 Then
            'Nothing to delete
            MessageBox.Show(AE_Constants.Instance.Cannot_delete & ": " & AE_Constants.Instance.SelectItem, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        Else
            If MessageBox.Show(AE_Constants.Instance.Remove & "'" & columnLabel & "'", AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                Me.TableArchetypeStyle.GridColumnStyles.RemoveAt(columnIndex)
                mArchetypeTable.Columns.RemoveAt(columnIndex * 2)
            End If
        End If
    End Sub


    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles MenuRemoveRow.Click
        Dim rowIndex As Integer
        Dim rowLabel As String

        rowIndex = Me.dgGrid.CurrentRowIndex

        If rowIndex = -1 Then
            'Nothing to delete
            MessageBox.Show(AE_Constants.Instance.Cannot_delete & ": " & AE_Constants.Instance.SelectItem, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        'If Not row_selected Then
        '    ' and the column label
        '    ii = Me.dgGrid.CurrentCell.ColumnNumber
        '    label = Me.TableArchetypeStyle.GridColumnStyles(ii).HeaderText
        '    If ii < 2 Then
        '        'not a valid column to remove
        '        MessageBox.Show(AE_Constants.Instance.Cannot_delete & "'" & label & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Return
        '    End If

        'End If

        If rowIndex > -1 Then
            ' a row is selected
            rowLabel = CStr(Me.dgGrid.Item(rowIndex, 1))
            If MessageBox.Show(AE_Constants.Instance.Remove & "'" & rowLabel & "'", AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                mArchetypeTable.Rows.RemoveAt(dgGrid.CurrentRowIndex)
            End If
            'Dim selected_rows As DataRow()
            'If row_selected Then
            '    selected_rows = mArchetypeTable.Select("Text = '" & Label & "'")
            '    If selected_rows.Length = 1 Then
            '        If i > 0 Then
            '            Me.dgGrid.CurrentRowIndex = i - 1
            '        Else
            '            Me.dgGrid.CurrentRowIndex = i + 1
            '        End If
            '        'RemoveTerms(selected_rows(0).Item(2))
            '        mArchetypeTable.Rows.Remove(selected_rows(0))
            '    Else
            '        'FIXME - may need to say no row selected
            '        Debug.Assert(False)
            '        Beep()
            '    End If
            'Else
            '    ' column selected
            '    Me.TableArchetypeStyle.GridColumnStyles.RemoveAt(ii)
            '    mArchetypeTable.Columns.RemoveAt(ii * 2)
            '    ' moves the archetype object column to index ii * 2
            '    'RemoveTerms(Me.ArchetypeTable.Rows(0).Item(ii * 2))
            '    mArchetypeTable.Columns.RemoveAt(ii * 2)
            'End If

        End If
    End Sub

    Public Overrides Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        Dim i, col_width As Integer
        Dim tab_pad As Integer = 109
        Dim Col_count As Integer
        Dim Text, tab_str, col_str, tab_end_str, s As String
        Col_count = CInt(((mArchetypeTable.Columns.Count - 1) / 2))

        col_str = ""
        Text = new_line & (Space(3 * indentlevel) & "\cf1 Structure\cf0  = \cf2 TABLE\cf0\par")
        Text = Text & new_line & ("\par")

        col_width = CInt((8414 - (Col_count * tab_pad)) / Col_count)
        tab_str = "\trowd\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs "
        For i = 1 To Col_count
            col_str = col_str & "\cellx" & ((col_width * i) + tab_pad).ToString & "\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs "
        Next
        tab_end_str = "\cellx8414\pard"
        s = "Rows "
        If mCardinalityControl.Cardinality.Ordered Then
            s = "ordered "
        End If

        s = s.Trim

        Text = Text & new_line & (tab_str & col_str & tab_end_str)

        'First cell is rows and if ordered or fixed
        s = "\intbl\f0\fs20\b " '& s & "\cell"

        'Then add the column headings
        'JAR: 29MAY07, EDT-21 Exception thrown when mKeyColumns.count is 0 
        If mKeyColumns.Count > 0 Then
            For Each t As String In CType(CType(mKeyColumns(1), ArchetypeElement).Constraint, Constraint_Text).AllowableValues.Codes
                s = s & " " & mFileManager.OntologyManager.GetTerm(t).Text & "\cell "
            Next
        Else
            s = s & " " & Me.RowHeadings.HeaderText & "\cell "
        End If
        s = s & "\b0\row"
        Text = Text & new_line & s

        For i = 0 To mArchetypeTable.Rows.Count - 1
            Dim ArchS As ArchetypeNode 'ArchetypeSimple
            Dim constraintString As String

            s = "\intbl "
            ArchS = CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode)

            If ArchS.RM_Class.Type = StructureType.Element Then
                constraintString = CType(ArchS, ArchetypeElement).Constraint.ConstraintTypeString
            Else
                constraintString = mFileManager.OntologyManager.GetOpenEHRTerm(312, "Slot")
            End If

            s = s & ArchS.Text & " (" & constraintString & ")\cell "
            constraintString = ArchS.ToRichText(0)
            If constraintString.Length > 62 Then
                constraintString = constraintString.Substring(1, 62)
            End If

            Text = Text & new_line & constraintString & "\cell\cell\row"
        Next

        Return Text
    End Function

    Protected Overrides Sub RefreshIcons()
        Dim d_row As DataRow

        If mCurrentItem.HasReferences Then
            Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)

            For Each d_row In mArchetypeTable.Rows
                Dim node As ArchetypeNode = CType(d_row.Item(2), ArchetypeNode)
                If Not node.IsAnonymous AndAlso CType(node, ArchetypeElement).NodeId = element.NodeId Then
                    d_row.BeginEdit()
                    d_row(0) = Me.ImageIndexForConstraintType(element.Constraint.Type, element.IsReference, False)
                    d_row.EndEdit()
                End If
            Next
        Else
            d_row = mArchetypeTable.Rows(dgGrid.CurrentRowIndex)
            d_row.BeginEdit()
            d_row(0) = Me.ImageIndexForItem(CType(d_row.Item(2), ArchetypeNode))
            d_row.EndEdit()
        End If
        dgGrid.Refresh()
    End Sub

    Private Function GetImageIndexForRow(ByVal row As Integer) As Integer
        Dim i As Integer
        i = CInt(mArchetypeTable.Rows(row).Item(0))
        If dgGrid.CurrentRowIndex = row Then
            i += Me.SelectedImageOffset
        End If
        Return i

    End Function

    Private Function MakeArchetypeTable() As DataTable
        ' Create a new DataTable titled 'Archetypes'
        Dim ArchTab As DataTable = New DataTable("Archetypes")

        Dim IconColumn As DataColumn = New DataColumn
        IconColumn.DataType = System.Type.GetType("System.Int32")
        IconColumn.ColumnName = "Icon"
        ArchTab.Columns.Add(IconColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        ArchTab.Columns.Add(TextColumn)
        Dim ElementColumn As DataColumn = New DataColumn
        ElementColumn.DataType = System.Type.GetType("System.Object")
        ElementColumn.ColumnName = "Element"
        ArchTab.Columns.Add(ElementColumn)
        ' Create an array for DataColumn objects.
        ' Return the new DataTable.
        Return ArchTab
    End Function


    Private Sub SetArchetypeTable()

        mArchetypeTable = MakeArchetypeTable()

        'TableArchetypeStyle

        TableArchetypeStyle = New DataGridTableStyle(False)
        RowHeadings = New DataGridTextBoxColumn
        IconColumn = New DataGridIconOnlyColumn(Me.ilSmall, New delegateGetIconIndexForRow(AddressOf GetImageIndexForRow))

        Me.TableArchetypeStyle.AllowSorting = False
        Me.TableArchetypeStyle.DataGrid = Me.dgGrid
        Me.TableArchetypeStyle.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.IconColumn, Me.RowHeadings})
        Me.TableArchetypeStyle.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableArchetypeStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.TableArchetypeStyle.MappingName = "Archetypes"
        Me.TableArchetypeStyle.PreferredColumnWidth = 70
        Me.TableArchetypeStyle.RowHeadersVisible = False

        'Icon column
        '
        Me.IconColumn.HeaderText = ""
        Me.IconColumn.ReadOnly = True
        Me.IconColumn.MappingName = "Icon"
        Me.IconColumn.Width = Me.ilSmall.Images(0).Size.Width

        'RowHeadings
        '
        Me.RowHeadings.Format = ""
        Me.RowHeadings.FormatInfo = Nothing
        Me.RowHeadings.HeaderText = "Rows"
        Me.RowHeadings.MappingName = "Text"
        Me.RowHeadings.Width = 175

        mArchetypeTable.DefaultView.AllowNew = False
        AddHandler mArchetypeTable.ColumnChanging, New DataColumnChangeEventHandler(AddressOf ArchetypeTable_ColumnChanging)
        Me.dgGrid.DataSource = mArchetypeTable
        Me.dgGrid.TableStyles.Add(Me.TableArchetypeStyle)

    End Sub

    Private Sub MenuRenameColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuRenameColumn.Click
        Dim label As String
        Dim i As Integer

        i = Me.dgGrid.CurrentCell.ColumnNumber
        label = Me.TableArchetypeStyle.GridColumnStyles(i).HeaderText

        If i < 1 Then
            MessageBox.Show(AE_Constants.Instance.Cannot_rename & "'" & label & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim s() As String

        s = OceanArchetypeEditor.Instance.GetInput(AE_Constants.Instance.New_name & "'" & label & "'", AE_Constants.Instance.Description, Me.ParentForm)

        If s(0) <> "" Then
            ' HKF: 1613
            'Dim ArchCol As ArchetypeColumn
            Dim newTerm As RmTerm = New RmTerm(mArchetypeTable.Columns(i + 1).ColumnName)
            newTerm.Text = s(0)
            newTerm.Description = s(1)
            mFileManager.OntologyManager.SetText(newTerm)
            'Dim ArchCol As ArchetypeComposite
            'ArchCol = CType(mArchetypeTable.Rows(0).Item(i * 2), ArchetypeComposite)
            'ArchCol.Text = s(0)
            'ArchCol.Description = s(1)
            Me.TableArchetypeStyle.GridColumnStyles(i).HeaderText = s(0)
            Me.TableArchetypeStyle.GridColumnStyles(i).NullText = "(" & s(0) & ")"
            Me.dgGrid.Refresh()
        End If

    End Sub

    Private Sub ContextMenuGrid_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuGrid.Popup
        ContextMenuGrid.MenuItems.Clear()

        Dim hasRemoveMenu As Boolean = False

        Me.MenuRemoveColumn.Visible = False
        Me.MenuRemoveRow.Visible = False

        If (Me.dgGrid.CurrentCell.ColumnNumber > 1) Then
            Me.MenuRename.Text = Filemanager.GetOpenEhrTerm(325, "Rename")
            Me.MenuRenameColumn.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(164, "Column"), Me.TableArchetypeStyle.GridColumnStyles(Me.dgGrid.CurrentCell.ColumnNumber).HeaderText)
            ContextMenuGrid.MenuItems.Add(Me.MenuRename)
            ContextMenuGrid.MenuItems.Add(Me.MenuRemoveColumnOrRow)
            hasRemoveMenu = True
            MenuRemoveColumn.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(164, "Column"), MenuRenameColumn.Text)
            Me.MenuRemoveColumn.Visible = True
            ContextMenuGrid.MenuItems.Add(Me.MenuRemoveColumnOrRow)
        End If
        If Me.dgGrid.CurrentRowIndex > -1 Then
            If Not hasRemoveMenu Then
                ContextMenuGrid.MenuItems.Add(Me.MenuRemoveColumnOrRow)
            End If
            Me.MenuRemoveRow.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(163, "Row"), CStr(Me.dgGrid.Item(Me.dgGrid.CurrentRowIndex, 1)))
            Me.MenuRemoveRow.Visible = True
        End If


    End Sub

    Private Sub dgGrid_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgGrid.MouseMove
        Dim myHitInfo As DataGrid.HitTestInfo = dgGrid.HitTest(e.X, e.Y)
        Dim i As Integer

        i = myHitInfo.Row - 1
        If i > -1 Then
            SetToolTipSpecialisation(Me.dgGrid, CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode))
        End If
    End Sub

    Private Sub dgGrid_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgGrid.CurrentCellChanged
        ' arises when moves to new cell
        Dim i As Integer
        If mIsRotated Then
            Dim element As ArchetypeNode

            i = Me.dgGrid.CurrentCell.RowNumber
            element = CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode)
            SetCurrentItem(element)
        Else
            Debug.Assert(False, "TODO")
        End If
    End Sub


    Private Sub ArchetypeTable_ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)

        If Not mIsLoading Then
            Dim i As Integer
            Dim archetype_node As ArchetypeNode

            archetype_node = CType(e.Row.Item(2), ArchetypeNode)
            If archetype_node.RM_Class.Type = StructureType.Element Then
                Dim element As ArchetypeElement = CType(archetype_node, ArchetypeElement)
                i = OceanArchetypeEditor.Instance.CountInString(element.NodeId, ".")
                If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                    If MessageBox.Show(AE_Constants.Instance.RequiresSpecialisationToEdit, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                        e.ProposedValue = element.Text
                    End If
                End If
            End If
            archetype_node = CType(e.Row.Item(2), ArchetypeNode)
            archetype_node.Text = CStr(e.ProposedValue)
        End If
    End Sub

    Private Sub dgGrid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgGrid.MouseDown
        Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
        hti = Me.dgGrid.HitTest(e.X, e.Y)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Select Case hti.Type
                Case System.Windows.Forms.DataGrid.HitTestType.None

                Case System.Windows.Forms.DataGrid.HitTestType.Cell
                    Dim c As DataGridCell
                    c.RowNumber = hti.Row
                    c.ColumnNumber = hti.Column
                    Me.dgGrid.CurrentCell = c
                Case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader
                    Dim c As DataGridCell
                    c.RowNumber = (0)
                    c.ColumnNumber = hti.Column
                    Me.dgGrid.CurrentCell = c
                Case System.Windows.Forms.DataGrid.HitTestType.RowHeader
                    Dim c As DataGridCell
                    c.RowNumber = hti.Row
                    c.ColumnNumber = 0
                    Me.dgGrid.CurrentCell = c
            End Select
        End If
    End Sub

    Private Sub dgGrid_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dgGrid.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub dgGrid_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dgGrid.DragDrop
        Dim table_archetype As ArchetypeNode

        If Not mNewConstraint Is Nothing Then
            Dim new_row As DataRow
            Dim a_cell As DataGridCell

            If TypeOf mNewConstraint Is Constraint_Slot Then
                Dim newSlot As New RmSlot(StructureType.Element)
                table_archetype = New ArchetypeNodeAnonymous(newSlot)
            Else

                table_archetype = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New element"), mFileManager)
                CType(table_archetype, ArchetypeElement).Constraint = mNewConstraint
            End If
            mCurrentItem = Nothing
            mIsLoading = True
            new_row = mArchetypeTable.NewRow
            'Change Sam Heard 2004-06-11
            'Change to use constraint.type
            new_row(0) = Me.ImageIndexForItem(table_archetype)
            new_row(1) = table_archetype.Text
            new_row(2) = table_archetype
            mArchetypeTable.Rows.Add(new_row)

            ' go to the new entry
            a_cell.RowNumber = mArchetypeTable.Rows.Count - 1
            a_cell.ColumnNumber = 1
            Me.dgGrid.Focus()
            Me.dgGrid.CurrentCell = a_cell
            mFileManager.FileEdited = True
            mIsLoading = False
            mNewConstraint = Nothing
        End If

    End Sub


    Private Class DataGridIconOnlyColumn
        Inherits DataGridTextBoxColumn
        Private WithEvents _icons As ImageList
        Private _getIconIndex As delegateGetIconIndexForRow

        Public Sub New(ByVal Icons As ImageList, ByVal getIconIndex As delegateGetIconIndexForRow)
            MyBase.New()
            _icons = Icons
            _getIconIndex = getIconIndex

        End Sub

        Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal source As CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)

            Try
                'erase background
                g.FillRectangle(backBrush, bounds)
                If Me.DataGridTableStyle.DataGrid.CurrentCell.RowNumber = rowNum Then
                    ' paint the correct image for the datatype
                    g.DrawImage(Me._icons.Images(_getIconIndex(rowNum)), bounds)
                Else
                    g.DrawImage(Me._icons.Images(_getIconIndex(rowNum)), bounds)
                End If
            Catch ex As System.Exception
                ' empty catch 
            End Try

        End Sub


        Protected Overloads Overrides Sub Edit(ByVal source As CurrencyManager, ByVal rowNum As Integer, ByVal bounds As Rectangle, ByVal readOnly1 As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)

            'do not allow the unbound cell to become active
            If (Me.MappingName Is "Icon") Then
                Return
            End If
            MyBase.Edit(source, rowNum, bounds, readOnly1, instantText, cellIsVisible)

        End Sub
    End Class

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
'The Original Code is TableStructure.vb.
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
