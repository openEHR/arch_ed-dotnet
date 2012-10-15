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

Public Class TableStructure
    Inherits EntryStructure

    Private mArchetypeTable As DataTable
    Private Delegate Function delegateGetIconIndexForRow(ByVal row As Integer) As Integer
    Private TableArchetypeStyle As DataGridTableStyle
    Private RowHeadings As DataGridTextBoxColumn
    Private IconColumn As DataGridIconOnlyColumn
    Private mIsRotated As Boolean = True
    Private mRow As RmCluster
    Private mIsLoading As Boolean
    Friend WithEvents MenuRemoveColumnOrRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveColumn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveRow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Public Shadows Event CurrentItemChanged(ByVal an_archetype_node As ArchetypeNode)
    Private mKeyColumns As New Collection

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Debug.Assert(DesignMode)
    End Sub

    Public Sub New(ByVal rm As RmTable, ByVal filemanager As FileManagerLocal)
        MyBase.New(rm, filemanager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mIsLoading = True
        SetArchetypeTable()

        If rm.Children.Count > 0 Then
            mRow = CType(rm.Children.Items(0), RmCluster)

            If mRow.Children.Count > 0 Then
                Dim rmStr As RmStructure
                Dim archNode As ArchetypeNode = Nothing

                butChangeDataType.Show()

                If rm.isRotated Then
                    For i As Integer = 0 To mRow.Children.Count - 1
                        'set column heading as always rotated - at the moment
                        rmStr = mRow.Children.Items(i)

                        If rmStr.Type = StructureType.Element Then
                            archNode = New ArchetypeElement(CType(rmStr, RmElement), mFileManager)
                        ElseIf rmStr.Type = StructureType.Slot Then
                            If String.IsNullOrEmpty(rmStr.NodeId) Then
                                archNode = New ArchetypeNodeAnonymous(CType(rmStr, RmSlot))
                            Else
                                archNode = New ArchetypeSlot(CType(rmStr, RmSlot), mFileManager)
                            End If
                        Else
                            Debug.Assert(False, "Type not handled")
                            Throw New Exception("Table row of invalid type")
                        End If

                        If i < rm.NumberKeyColumns AndAlso TypeOf archNode Is ArchetypeElement Then
                            AddColumnElement(CType(archNode, ArchetypeElement))
                        Else
                            Dim row As DataRow = mArchetypeTable.NewRow
                            row.Item(0) = archNode.ImageIndex(False)
                            row.Item(1) = archNode.Text
                            row.Item(2) = archNode
                            mArchetypeTable.Rows.Add(row)
                        End If
                    Next

                    If mArchetypeTable.Rows.Count > 0 Then
                        SetCurrentItem(CType(mArchetypeTable.Rows(0).Item(2), ArchetypeNode))
                    End If
                Else
                    Debug.Assert(False, "Unrotated tables are not handled at present")
                End If
            End If
        End If

        mIsLoading = False
    End Sub

    Public Sub New(ByVal filemanager As FileManagerLocal)
        MyBase.New("Table", filemanager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Friend WithEvents MenuNameSlot As MenuItem

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgGrid = New System.Windows.Forms.DataGrid
        Me.ContextMenuGrid = New System.Windows.Forms.ContextMenu
        Me.MenuRename = New System.Windows.Forms.MenuItem
        Me.MenuNameSlot = New System.Windows.Forms.MenuItem
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
        Me.ContextMenuGrid.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRename, Me.MenuRemoveColumnOrRow, Me.MenuNameSlot})
        '
        'MenuRename
        '
        Me.MenuRename.Index = 0
        Me.MenuRename.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRenameColumn})
        Me.MenuRename.Text = "Rename"
        '
        'MenuNameSlot
        '
        Me.MenuNameSlot.Index = 2
        Me.MenuNameSlot.Text = "Name this Slot"
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

        If Main.Instance.DefaultLanguageCode <> "en" Then
            MenuNameSlot.Text = AE_Constants.Instance.NameThisSlot
        End If
    End Sub

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Dim result As New ArrayList
            result.Add(mArchetypeTable)
            result.Add(mKeyColumns)
            result.Add(mIsRotated)
            Return result
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructure
        Get
            Dim result As New RmTable(mNodeId)
            result.Children.Cardinality = mCardinalityControl.Cardinality
            result.isRotated = True
            result.NumberKeyColumns = mKeyColumns.Count

            If mRow Is Nothing Then
                mRow = New RmCluster(mFileManager.OntologyManager.AddTerm("row", "@ internal @").Code)
            Else
                mRow.Children.Clear()
            End If

            Dim n_rows As Integer = mArchetypeTable.Rows.Count
            mRow.Children.Cardinality.MinCount = n_rows + mKeyColumns.Count
            mRow.Children.Cardinality.MaxCount = n_rows + mKeyColumns.Count

            For Each row_heading As ArchetypeElement In mKeyColumns
                mRow.Children.Add(row_heading.RM_Class)
            Next

            For i As Integer = 0 To n_rows - 1
                Dim archetype_node As ArchetypeNode = CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode)
                mRow.Children.Add(archetype_node.RM_Class)
            Next

            result.Children.Add(mRow)
            Return result
        End Get
        Set(ByVal value As RmStructure)
            Dim compound As RmStructureCompound = CType(value, RmStructureCompound)
            MyBase.SetCardinality(compound)
            mControl = dgGrid

            If mArchetypeTable Is Nothing Then
                ' no archetype driving constructor
                SetArchetypeTable()
            End If

            ' handles conversion from other structures
            mArchetypeTable.Rows.Clear()
            mNodeId = value.NodeId

            mIsLoading = True

            Select Case value.Type
                Case StructureType.Tree
                    AddNodesToTable(compound.Children)

                Case StructureType.Single
                    Dim rm As RmStructure = compound.Children.FirstElementOrElementSlot

                    If Not rm Is Nothing Then
                        AddTableRow(rm)
                    End If

                Case StructureType.List
                    For Each rm As RmStructure In compound.Children
                        AddTableRow(rm)
                    Next
            End Select

            mIsLoading = False
        End Set
    End Property

    Public Overrides Function ItemCount() As Integer
        Dim result As Integer = 0

        If Not mArchetypeTable Is Nothing Then
            result = mArchetypeTable.Rows.Count
        End If

        Return result
    End Function

    Sub AddNodesToTable(ByVal ch As Children)
        For Each rm As RmStructure In ch
            If rm.Type = StructureType.Cluster Then
                AddNodesToTable(CType(rm, RmStructureCompound).Children)
            ElseIf rm.Type = StructureType.Slot Then
                'Have to lose cluster slots
                If CType(rm, RmSlot).SlotConstraint.RM_ClassType = Global.ArchetypeEditor.StructureType.Element Then
                    AddTableRow(rm)
                End If
            Else
                AddTableRow(rm)
            End If
        Next
    End Sub

    Protected Sub AddTableRow(ByVal rm As RmStructure)
        Dim node As ArchetypeNode

        Select Case rm.Type
            Case StructureType.Element, StructureType.Reference
                node = New ArchetypeElement(CType(rm, RmElement), mFileManager)
            Case StructureType.Slot
                node = New ArchetypeSlot(CType(rm, RmSlot), mFileManager)
            Case Else
                node = Nothing
                Debug.Assert(False, "Type not handled")
        End Select

        If Not node Is Nothing Then
            Dim row As DataRow = mArchetypeTable.NewRow
            row(1) = node.Text
            row(2) = node
            row(0) = node.ImageIndex(False)
            mArchetypeTable.Rows.Add(row)
        End If
    End Sub

    Public Overrides Sub Reset()
        mArchetypeTable.Rows.Clear()
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

    Public Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs)
        If dgGrid.CurrentRowIndex > -1 Then
            Dim node As ArchetypeNodeAbstract = TryCast(mArchetypeTable.Rows(dgGrid.CurrentCell.RowNumber).Item(2), ArchetypeNodeAbstract)

            If Not node Is Nothing Then
                If node.IsReference Then
                    MessageBox.Show(AE_Constants.Instance.CannotSpecialisereference, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Dim dlg As New SpecialisationQuestionDialog()
                    dlg.ShowForArchetypeNode(node.Text, node.RM_Class, SpecialisationDepth)

                    If dlg.IsSpecialisationRequested Then
                        If dlg.IsCloningRequested Then
                            node = CType(node.Copy, ArchetypeNodeAbstract)
                            node.Specialise()

                            Dim row As DataRow = mArchetypeTable.NewRow
                            row(1) = node.Text
                            mArchetypeTable.Rows.InsertAt(row, dgGrid.CurrentRowIndex + 1)
                            row(2) = node
                            row(0) = node.ImageIndex(False)

                            ' go to the new entry
                            Dim cell As DataGridCell
                            cell.RowNumber = dgGrid.CurrentRowIndex + 1
                            cell.ColumnNumber = 1
                            dgGrid.Focus()
                            dgGrid.CurrentCell = cell
                        Else
                            node.Specialise()
                            mArchetypeTable.Rows(dgGrid.CurrentRowIndex).Item(1) = mCurrentItem.Text
                        End If

                        SetCurrentItem(node)
                        mFileManager.FileEdited = True
                    End If
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub ReplaceCurrentItem(ByVal node As ArchetypeNode)
        Dim i As Integer = dgGrid.CurrentCell.RowNumber

        If i > -1 Then
            mArchetypeTable.Rows(i).Item(2) = node
            mIsLoading = True
            mArchetypeTable.Rows(i).Item(1) = node.Text
            mIsLoading = False
            mCurrentItem = node
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Assert(False, "Cannot add a reference to a table at present ? required")
    End Sub

    Protected Overrides Sub ShowAddElementMenu(ByVal menu As ConstraintContextMenu)
        Dim m As New ContextMenu
        Dim rowItem As MenuItem = New MenuItem(Filemanager.GetOpenEhrTerm(324, "New Row"))
        Dim columnItem As MenuItem = New MenuItem(Filemanager.GetOpenEhrTerm(323, "New Column"))

        If mIsRotated Then
            m.MenuItems.Add(rowItem)
            rowItem.MergeMenu(menu)
            AddHandler columnItem.Click, AddressOf AddKeyColumn
            m.MenuItems.Add(columnItem)
        Else
            m.MenuItems.Add(columnItem)
            columnItem.MergeMenu(menu)
            AddHandler rowItem.Click, AddressOf AddKeyColumn
            m.MenuItems.Add(rowItem)
        End If

        m.Show(ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Public Overrides Sub SetInitial()
        If dgGrid.VisibleRowCount > 0 Then
            SetCurrentItem(CType(mArchetypeTable.Rows(0).Item(2), ArchetypeNode))
        End If
    End Sub

    Protected Overrides Sub AddNewElement(ByVal constraint As Constraint)
        mIsLoading = True
        Dim node As ArchetypeNode

        If constraint.Kind = ConstraintKind.Slot Then
            node = NewSlotNode()
        Else
            node = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New Element"), mFileManager)
            node.Occurrences.MaxCount = 1
            node.Constraint = constraint
        End If

        Dim row As DataRow = mArchetypeTable.NewRow
        row(0) = node.ImageIndex(False)
        row(1) = node.Text
        row(2) = node
        mArchetypeTable.Rows.Add(row)

        ' go to the new entry
        Dim cell As DataGridCell
        cell.RowNumber = mArchetypeTable.Rows.Count - 1
        cell.ColumnNumber = 1
        dgGrid.Focus()
        dgGrid.CurrentCell = cell
        SetCurrentItem(node)
        mFileManager.FileEdited = True
        mIsLoading = False
    End Sub

    Protected Function NewSlotNode() As ArchetypeNode
        Dim result As ArchetypeNode = Nothing

        If MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            result = New ArchetypeSlot(mFileManager.OntologyManager.GetOpenEHRTerm(CInt(StructureType.Element), StructureType.Element.ToString), StructureType.Element, mFileManager)
        Else
            result = New ArchetypeNodeAnonymous(StructureType.Element)
        End If

        Return result
    End Function

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
            s = Main.Instance.GetInput(AE_Constants.Instance.Text, AE_Constants.Instance.Description, Me.ParentForm)
            If s(0) <> "" Then
                a_term = mFileManager.OntologyManager.AddTerm(s(0), s(1))
                'If mArchetypeTable.Rows.Count > 0 Then
                If mKeyColumns.Count > 0 Then
                    element = CType(mKeyColumns.Item(mKeyColumns.Count), ArchetypeElement)
                Else
                    element = New ArchetypeElement("row_head", mFileManager)
                    'Added SRH: Need to set the constraint to internal (as no longer default)
                    Dim c As Constraint_Text = New Constraint_Text
                    c.TypeOfTextConstraint = TextConstraintType.Internal
                    element.Constraint = c
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
        Dim columnIndex As Integer = dgGrid.CurrentCell.ColumnNumber
        Dim columnLabel As String = TableArchetypeStyle.GridColumnStyles(columnIndex).HeaderText

        If columnIndex < 2 Then
            'Nothing to delete
            MessageBox.Show(AE_Constants.Instance.Cannot_delete & ": " & AE_Constants.Instance.SelectItem, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If MessageBox.Show(AE_Constants.Instance.Remove & "'" & columnLabel & "'", AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                TableArchetypeStyle.GridColumnStyles.RemoveAt(columnIndex)
                mArchetypeTable.Columns.RemoveAt(columnIndex * 2)
            End If
        End If
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles MenuRemoveRow.Click
        Dim rowIndex As Integer = dgGrid.CurrentRowIndex

        If rowIndex < 0 Then
            MessageBox.Show(AE_Constants.Instance.Cannot_delete & ": " & AE_Constants.Instance.SelectItem, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim rowLabel As String

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

            ' a row is selected
            rowLabel = CStr(dgGrid.Item(rowIndex, 1))

            If MessageBox.Show(AE_Constants.Instance.Remove & "'" & rowLabel & "'", AE_Constants.Instance.Remove, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                mArchetypeTable.Rows.RemoveAt(dgGrid.CurrentRowIndex)

                If dgGrid.CurrentRowIndex < 0 Then
                    SetCurrentItem(Nothing)
                End If
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

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim showComments As Boolean = Main.Instance.Options.ShowCommentsInHtml

        result.AppendFormat("<p><i>Structure</i>: {0}", Filemanager.GetOpenEhrTerm(108, "TABLE"))
        result.Append(CStr(IIf(mCardinalityControl.Cardinality.Ordered, ", " & mFileManager.OntologyManager.GetOpenEHRTerm(162, "Ordered"), "")))
        result.Append("</p>")
        result.AppendFormat("<h2>{0}<h2>", Filemanager.GetOpenEhrTerm(164, "Columns"))
        result.AppendFormat("{0}<table border=""1"" cellpadding=""2"" width=""100%"">", Environment.NewLine)
        'Column heads
        If mKeyColumns.Count > 0 Then
            For Each row_heading As ArchetypeElement In mKeyColumns
                result.AppendLine("<tr>")
                For Each cde As String In CType(row_heading.RM_Class.Constraint, Constraint_Text).AllowableValues.Codes
                    result.AppendFormat("<td><h3>{0}</h3></td>", mFileManager.OntologyManager.GetTerm(cde).Text)
                Next
                result.AppendLine("</tr>")
            Next
        End If
        result.AppendLine("</table>")
        result.AppendFormat("<h2>{0}<h2>", Filemanager.GetOpenEhrTerm(163, "Rows"))
        result.AppendFormat("{0}<table border=""1"" cellpadding=""2"" width=""100%"">", Environment.NewLine)
        result.Append(Me.HtmlHeader(BackGroundColour, showComments))

        result.AppendFormat("{0}{1}", Environment.NewLine, TableToHTML(Me.mArchetypeTable, showComments))
        If result.ToString.EndsWith("<tr>") Then
            result.AppendLine("</tr>")
        End If
        result.AppendLine("</table>")

        Return result.ToString
    End Function

    Private Function TableToHTML(ByVal table As DataTable, ByVal showComments As Boolean) As String
        Dim text As System.Text.StringBuilder = New System.Text.StringBuilder

        For Each dRow As DataRow In table.Rows
            For i As Integer = 1 To mKeyColumns.Count
                text.AppendFormat("<tr>{0}</tr>", CType(dRow.Item(2), ArchetypeNode).ToHTML(0, showComments))
            Next
        Next

        Return text.ToString()
    End Function

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
                constraintString = CType(ArchS, ArchetypeElement).Constraint.ConstraintKindString
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
        Dim row As DataRow

        If mCurrentItem.HasReferences Then
            Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)

            For Each row In mArchetypeTable.Rows
                Dim node As ArchetypeNode = CType(row.Item(2), ArchetypeNode)

                If Not node.IsAnonymous AndAlso CType(node, ArchetypeElement).NodeId = element.NodeId Then
                    row.BeginEdit()
                    row(0) = element.ImageIndex(False)
                    row.EndEdit()
                End If
            Next
        Else
            row = mArchetypeTable.Rows(dgGrid.CurrentRowIndex)
            row.BeginEdit()
            row(0) = CType(row.Item(2), ArchetypeNode).ImageIndex(False)
            row.EndEdit()
        End If

        dgGrid.Refresh()
    End Sub

    Private Function GetImageIndexForRow(ByVal row As Integer) As Integer
        Dim result As Integer = CInt(mArchetypeTable.Rows(row).Item(0))

        If dgGrid.CurrentRowIndex = row Then
            result += Constraint.SelectedImageOffset
        End If

        Return result
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
        Dim i As Integer = dgGrid.CurrentCell.ColumnNumber
        Dim label As String = TableArchetypeStyle.GridColumnStyles(i).HeaderText

        If i < 1 Then
            MessageBox.Show(AE_Constants.Instance.Cannot_rename & "'" & label & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim s() As String = Main.Instance.GetInput(AE_Constants.Instance.New_name & "'" & label & "'", AE_Constants.Instance.Description, ParentForm)

            If s(0) <> "" Then
                Dim newTerm As RmTerm = New RmTerm(mArchetypeTable.Columns(i + 1).ColumnName)
                newTerm.Text = s(0)
                newTerm.Description = s(1)
                mFileManager.OntologyManager.SetRmTermText(newTerm)
                TableArchetypeStyle.GridColumnStyles(i).HeaderText = s(0)
                TableArchetypeStyle.GridColumnStyles(i).NullText = "(" & s(0) & ")"
                dgGrid.Refresh()
            End If
        End If
    End Sub

    Private Sub ContextMenuGrid_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuGrid.Popup
        ContextMenuGrid.MenuItems.Clear()

        Dim hasRemoveMenu As Boolean = False
        MenuRemoveColumn.Visible = False
        MenuRemoveRow.Visible = False

        If dgGrid.CurrentCell.ColumnNumber > 1 Then
            MenuRename.Text = Filemanager.GetOpenEhrTerm(325, "Rename")
            MenuRenameColumn.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(164, "Column"), TableArchetypeStyle.GridColumnStyles(dgGrid.CurrentCell.ColumnNumber).HeaderText)
            ContextMenuGrid.MenuItems.Add(MenuRename)
            ContextMenuGrid.MenuItems.Add(MenuRemoveColumnOrRow)
            hasRemoveMenu = True
            MenuRemoveColumn.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(164, "Column"), MenuRenameColumn.Text)
            MenuRemoveColumn.Visible = True
            ContextMenuGrid.MenuItems.Add(MenuRemoveColumnOrRow)
        End If

        If dgGrid.CurrentRowIndex > -1 Then
            If Not hasRemoveMenu Then
                ContextMenuGrid.MenuItems.Add(MenuRemoveColumnOrRow)
            End If

            MenuRemoveRow.Text = String.Format("{0}: {1}", Filemanager.GetOpenEhrTerm(163, "Row"), CStr(dgGrid.Item(dgGrid.CurrentRowIndex, 1)))
            MenuRemoveRow.Visible = True
        End If
    End Sub

    Private Sub dgGrid_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgGrid.MouseMove
        Dim myHitInfo As DataGrid.HitTestInfo = dgGrid.HitTest(e.X, e.Y)
        Dim i As Integer = myHitInfo.Row - 1

        If i > -1 Then
            SetToolTipSpecialisation(dgGrid, CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode))
        End If
    End Sub

    Private Sub dgGrid_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgGrid.CurrentCellChanged
        ' arises when moves to new cell
        If mIsRotated Then
            Dim i As Integer = dgGrid.CurrentCell.RowNumber
            Dim element As ArchetypeNode = CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNode)
            MenuNameSlot.Visible = TypeOf element Is ArchetypeNodeAnonymous AndAlso element.RM_Class.Type = Global.ArchetypeEditor.StructureType.Slot
            SetCurrentItem(element)
        Else
            Debug.Assert(False, "TODO")
        End If
    End Sub

    Protected Overrides Sub NameSlot(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuNameSlot.Click
        Dim i As Integer = dgGrid.CurrentCell.RowNumber

        If i > -1 Then
            ReplaceCurrentItem(New ArchetypeSlot(CType(mArchetypeTable.Rows(i).Item(2), ArchetypeNodeAnonymous), mFileManager))
        End If
    End Sub

    Private Sub ArchetypeTable_ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
        If Not mIsLoading Then
            Dim node As ArchetypeNode = CType(e.Row.Item(2), ArchetypeNode)

            If node.RM_Class.Type = StructureType.Slot AndAlso TypeOf (node) Is ArchetypeNodeAnonymous Then
                e.ProposedValue = node.Text
            Else
                If node.RM_Class.Type = StructureType.Element Then
                    Dim element As ArchetypeElement = CType(node, ArchetypeElement)

                    If element.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                        SpecialiseCurrentItem(sender, e)

                        If element.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                            e.ProposedValue = element.Text
                        End If
                    End If
                End If

                Dim proposedText As String = TryCast(e.ProposedValue, String)

                If Not proposedText Is Nothing Then
                    node.Text = proposedText

                    'Slot may reset text to include class
                    If node.Text <> proposedText Then
                        e.ProposedValue = node.Text
                    End If
                End If
            End If
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
        Dim node As ArchetypeNode = Nothing

        If Not mNewConstraint Is Nothing Then
            mCurrentItem = Nothing
            AddNewElement(mNewConstraint)
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
            If Not MappingName Is "Icon" Then
                MyBase.Edit(source, rowNum, bounds, readOnly1, instantText, cellIsVisible)
            End If
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
