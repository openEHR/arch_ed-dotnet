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

Public Class ListStructure
    Inherits EntryStructure

    Private mDragItem As ArchetypeListViewItem

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal rm As RmStructureCompound, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(rm, a_file_manager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'Not able to set imagelists that are inherited
        Me.lvList.SmallImageList = Me.ilSmall


        Dim lvitem As ArchetypeListViewItem

        For Each element As RmElement In rm.Children
            ' have to create links to the new archetype here to maintain updates
            lvitem = New ArchetypeListViewItem(element, mFileManager)
            lvitem.ImageIndex = ImageIndexForConstraintType(lvitem.Item.Constraint.Type, element.isReference)
            ''If lvItem.Item.Constraint.ConstraintType = "Ordinal" Then
            'If lvItem.Item.Constraint.Type = ConstraintType.Ordinal Then
            '    SetOrdinals(lvItem)
            'End If
            Me.lvList.Items.Add(lvitem)
            If lvitem.Index = 0 Then
                lvitem.Selected = True
                lvitem.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.isReference, True)
            End If
        Next
    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New("List", a_file_manager)  ' structure type list
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        'Not able to set imagelists that are inherited
        Me.lvList.SmallImageList = Me.ilSmall
        mFileManager.FileEdited = True

    End Sub

    Public Sub New()
        MyBase.New()  ' structure type list
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        'Not able to set imagelists that are inherited
        Me.lvList.SmallImageList = Me.ilSmall
        If Not Me.DesignMode Then
            Debug.Assert(False)
        End If

    End Sub

    ''Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lvList As System.Windows.Forms.ListView
    Friend WithEvents ElementName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuList As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddReference As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveItemAndReference As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lvList = New System.Windows.Forms.ListView
        Me.ElementName = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuList = New System.Windows.Forms.ContextMenu
        Me.MenuRemove = New System.Windows.Forms.MenuItem
        Me.MenuRemoveItemAndReference = New System.Windows.Forms.MenuItem
        Me.SpecialiseMenuItem = New System.Windows.Forms.MenuItem
        Me.MenuAddReference = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'lvList
        '
        Me.lvList.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.lvList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ElementName})
        Me.lvList.ContextMenu = Me.ContextMenuList
        Me.lvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvList.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvList.FullRowSelect = True
        Me.lvList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvList.HideSelection = False
        Me.lvList.LabelEdit = True
        Me.lvList.Location = New System.Drawing.Point(40, 32)
        Me.lvList.MultiSelect = False
        Me.lvList.Name = "lvList"
        Me.lvList.Size = New System.Drawing.Size(464, 328)
        Me.lvList.TabIndex = 38
        Me.lvList.View = System.Windows.Forms.View.Details
        '
        'ElementName
        '
        Me.ElementName.Text = "Element"
        Me.ElementName.Width = 350
        '
        'ContextMenuList
        '
        Me.ContextMenuList.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemove, Me.SpecialiseMenuItem, Me.MenuAddReference})
        '
        'MenuRemove
        '
        Me.MenuRemove.Index = 0
        Me.MenuRemove.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemoveItemAndReference})
        Me.MenuRemove.Text = "Remove"
        '
        'MenuRemoveItemAndReference
        '
        Me.MenuRemoveItemAndReference.Index = 0
        Me.MenuRemoveItemAndReference.Text = "?"
        '
        'SpecialiseMenuItem
        '
        Me.SpecialiseMenuItem.Index = 1
        Me.SpecialiseMenuItem.Text = "Specialise"
        '
        'MenuAddReference
        '
        Me.MenuAddReference.Index = 2
        Me.MenuAddReference.Text = "Add Reference"
        '
        'ListStructure
        '
        Me.Controls.Add(Me.lvList)
        Me.Name = "ListStructure"
        Me.Controls.SetChildIndex(Me.lvList, 0)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub ListStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = lvList

        If Not Me.DesignMode Then
            'Set the menu texts
            Me.MenuRemove.Text = AE_Constants.Instance.Remove
            Me.SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
            Me.MenuAddReference.Text = AE_Constants.Instance.Add_Reference
        End If
    End Sub

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Return lvList.Items
        End Get
    End Property

    Public Overrides ReadOnly Property Elements() As ArchetypeElement()
        Get
            Dim i As Integer
            i = lvList.Items.Count
            If i > 0 Then
                Dim a_e(i) As ArchetypeElement
                For i = 0 To lvList.Items.Count - 1
                    a_e(i) = CType(lvList.Items(i), ArchetypeListViewItem).Item
                Next
                Return a_e
            Else
                Return Nothing
            End If
        End Get
    End Property


    Public Overrides Property Archetype() As RmStructureCompound
        Get
            Dim lvItem As ArchetypeListViewItem

            Dim RM_S As New RmStructureCompound(mNodeId, StructureType.List)

            RM_S.Children.Cardinality = mCardinalityControl.Item.Cardinality
            RM_S.Children.Cardinality.Ordered = mCardinalityControl.Item.IsOrdered

            For Each lvItem In Me.lvList.Items
                RM_S.Children.Add(lvItem.Item.RM_Class)
            Next
            Return RM_S
        End Get
        Set(ByVal Value As RmStructureCompound)
            ' handles conversion from other structures
            Debug.Assert(False, "ToDo")
            Me.lvList.Items.Clear()
            mNodeId = Value.NodeId
            Select Case Value.Type '.TypeName
                Case StructureType.Tree ' "TREE"
                    ProcessTreeToList(Value)
                Case StructureType.Single ' "SINGLE"
                    Dim item As New ArchetypeListViewItem(Value.Children.FirstElementNode, mFileManager)
                    lvList.Items.Add(item)
                Case StructureType.Table ' "TABLE"
                    Dim element As RmElement
                    For Each element In Value.Children
                        Dim item As New ArchetypeListViewItem(element, mFileManager)
                        lvList.Items.Add(item)
                    Next
            End Select
        End Set
    End Property


    Private Sub ProcessTreeToList(ByVal rm As RmStructureCompound)
        Dim a_rm_structure As RmStructure

        For Each a_rm_structure In rm.Children
            'If a_rm_structure.TypeName = "Cluster" Then
            If a_rm_structure.Type = StructureType.Cluster Then
                ProcessTreeToList(CType(a_rm_structure, RmStructureCompound))
            Else
                Dim item As New ArchetypeListViewItem(CType(a_rm_structure, RmElement), mFileManager)
                lvList.Items.Add(item)
            End If
        Next

    End Sub

    Public Overrides Sub Reset()
        Me.lvList.Items.Clear()
    End Sub

    Public Overrides Sub Translate()
        Dim lvitem As ArchetypeListViewItem
        For Each lvitem In Me.lvList.Items
            lvitem.Translate()
        Next
        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click
        If lvList.SelectedItems.Count > 0 Then
            Dim lvitem As ArchetypeListViewItem = CType(lvList.SelectedItems.Item(0), ArchetypeListViewItem)
            'Fixme - need to add code to check if can be specialised

            If MessageBox.Show(AE_Constants.Instance.Specialise & lvitem.Text, _
                AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Question) = DialogResult.OK Then

                If lvitem.Item.Occurrences.IsUnbounded Or lvitem.Item.Occurrences.MaxCount > 1 Then
                    Dim i As Integer

                    i = lvitem.Index
                    lvitem = lvitem.Copy
                    lvitem.Specialise()
                    Me.lvList.Items.Insert(i + 1, lvitem)
                Else
                    lvitem.Specialise()
                End If
                'force refresh
                MyBase.SetCurrentItem(lvitem.Item)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs) Handles MenuAddReference.Click
        Dim an_element As ArchetypeElement
        Dim element As RmElement
        Dim ref As RmReference

        ' create a new reference element pointing to this element
        If Me.lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem

            lvItem = CType(Me.lvList.SelectedItems(0), ArchetypeListViewItem)
            ref = New RmReference(lvItem.Item.RM_Class)
            ' record the presence of the reference so a delete can be safe
            CType(lvItem.Item.RM_Class, RmElement).hasReferences = True
            lvItem = New ArchetypeListViewItem(ref, mFileManager)
            ' insert in the list
            lvItem.ImageIndex = Me.ImageIndexForConstraintType(lvItem.Item.Constraint.Type, True)
            Me.lvList.Items.Insert(lvList.SelectedIndices(0) + 1, lvItem)
            mFileManager.FileEdited = True
        End If

    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        mConstraintMenu.Show(ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        Dim lvItem As New ArchetypeListViewItem(mFileManager.OntologyManager.GetOpenEHRTerm(109, "New Element"), mFileManager)
        lvItem.Item.Constraint = a_constraint
        Me.lvList.Items.Add(lvItem)
        lvItem.ImageIndex = Me.ImageIndexForConstraintType(a_constraint.Type, False, True)
        mFileManager.FileEdited = True
        lvItem.BeginEdit()
        If Not lvItem.Selected Then
            ' needed for first element in the list
            lvItem.Selected = True
        End If
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles MenuRemoveItemAndReference.Click
        If Me.lvList.SelectedIndices.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem
            Dim message As String

            lvItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

            If lvItem.Item.HasReferences Then
                message = AE_Constants.Instance.Remove & Me.lvList.SelectedItems(0).Text & " " & AE_Constants.Instance.All_References
            Else
                message = AE_Constants.Instance.Remove & Me.lvList.SelectedItems(0).Text
            End If

            If MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                Dim nodeid As String

                ' leave an item selected if there is one
                If lvItem.Index > 0 Then
                    Me.lvList.Items(lvItem.Index - 1).Selected = True
                ElseIf Me.lvList.Items.Count > 1 Then
                    Me.lvList.Items(lvItem.Index + 1).Selected = True
                End If

                ' if this is a reference then remove all items with nodeid
                If lvItem.Item.HasReferences Then
                    nodeid = lvItem.Item.NodeId
                    For Each lvItem In lvList.Items
                        If lvItem.Item.NodeId = nodeid Then
                            lvItem.Remove()
                        End If
                    Next
                Else
                    lvItem.Remove()
                End If
                If lvList.SelectedItems.Count = 0 Then
                    lvItem = Nothing
                    MyBase.SetCurrentItem(Nothing)
                End If
                mFileManager.FileEdited = True
            End If
        Else
            Beep()
        End If
    End Sub

    Public Overrides Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        Dim lvItem As ArchetypeListViewItem
        Dim text, s As String

        text = text & new_line & (Space(3 * indentlevel) & "\cf1 Structure\cf0  = \cf2 LIST\cf0\par")
        s = ""
        If mCardinalityControl.Item.IsOrdered Then
            s = "ordered"
        End If

        s = s.Trim

        text = text & new_line & (Space(3 * indentlevel) & "\cf2 Items\cf0  " & s & "\par")
        For Each lvItem In Me.lvList.Items
            text = text & new_line & lvItem.Item.ToRichText(indentlevel + 1)
        Next

        Return text
    End Function

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim lvItem As ArchetypeListViewItem
        Dim text, s As String

        s = ""
        If mCardinalityControl.Item.IsOrdered Then
            s &= ", ordered"
        End If

        s = s.Trim

        text = "<p>Structure = LIST" & s & "</p>"

        text &= Environment.NewLine & "<table border=""1"" cellpadding=""2"" width=""100%"">"

        If BackGroundColour = "" Then
            text &= Environment.NewLine & "<tr>"
        Else
            text &= Environment.NewLine & "<tr  bgcolor=""" & BackGroundColour & """>"
        End If
        text &= Environment.NewLine & "<td width=""20%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(54, "Concept") & "</h4></td>"
        text &= Environment.NewLine & "<td width = ""40%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(113, "Description") & "</h4></td>"
        text &= Environment.NewLine & "<td width = ""20%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(87, "Constraints") & "</h4></td>"
        text &= Environment.NewLine & "<td width=""20%""><h4>" & Filemanager.Instance.OntologyManager.GetOpenEHRTerm(438, "Values") & "</h4></td>"
        text &= Environment.NewLine & "</tr>"

        For Each lvItem In Me.lvList.Items
            text &= Environment.NewLine & lvItem.Item.ToHTML(0)
            text &= Environment.NewLine & "</tr>"
        Next
        text &= Environment.NewLine & "</table>"


        Return text
    End Function

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not Me.lvList.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i As Integer
            i = Me.lvList.SelectedIndices(0)
            lvI = Me.lvList.SelectedItems(0)

            If i > 0 Then
                Me.lvList.Items.Remove(lvI)
                Me.lvList.Items.Insert((i - 1), lvI)
                mFileManager.FileEdited = True
                Me.lvList.Items.Item(i - 1).Selected = True
            End If
        End If
    End Sub

    Protected Overrides Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not Me.lvList.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i, c As Integer

            c = Me.lvList.Items.Count
            i = Me.lvList.SelectedIndices(0)
            lvI = Me.lvList.SelectedItems(0)

            If i < (c - 1) Then
                Me.lvList.Items.Remove(lvI)
                Me.lvList.Items.Insert((i + 1), lvI)
                lvI.Selected = True
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub RefreshIcons()
        Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)

        If element.HasReferences Then
            For Each lvItem As ArchetypeListViewItem In lvList.Items
                If lvItem.Item.NodeId = element.NodeId Then
                    lvItem.ImageIndex = Me.ImageIndexForConstraintType(lvItem.Item.Constraint.Type, lvItem.Item.IsReference, lvItem.Selected)
                End If
            Next
        Else
            If lvList.SelectedItems.Count = 0 Then
                If lvList.Items.Count = 0 Then
                    Return
                End If
                lvList.Items(0).Selected = True
            End If
            CType(lvList.SelectedItems(0), ArchetypeListViewItem).ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.IsReference, True)
        End If

    End Sub

    Private Sub ContextMenuList_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuList.Popup

        Me.MenuRemove.Visible = False
        Me.SpecialiseMenuItem.Visible = False
        Me.MenuAddReference.Visible = False

        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem
            Dim i As Integer

            lvItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)
            Me.MenuRemove.Visible = True
            Me.MenuRemoveItemAndReference.Text = lvItem.Text
            'may be a reference and can't add a reference
            'If (Filemanager.Instance.OntologyManager.NumberOfSpecialisations = 0) AndAlso (Not lvItem.Item.IsReference) Then
            If Not lvItem.Item.IsReference Then
                Me.MenuAddReference.Visible = True
            End If
            ' show specialisation if appropriate
            i = ArchetypeEditor.Instance.CountInString(lvItem.Item.NodeId, ".")
            If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                Me.SpecialiseMenuItem.Visible = True
            End If
        End If

    End Sub

    Private Sub lvList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvList.SelectedIndexChanged
        Dim element As ArchetypeElement
        Dim lvItem As ArchetypeListViewItem
        Dim i As Integer
        Dim s As String

        If Me.lvList.Items.Count = 0 Then
            Me.butChangeDataType.Visible = False
            Return
        End If
        'make sure there is a selected item
        If Me.lvList.SelectedItems.Count = 1 Then
            Me.MenuRemoveItemAndReference.Text = Me.lvList.SelectedItems(0).Text
            'Unselect the previous item
            For Each lvItem In Me.lvList.Items
                lvItem.ImageIndex = Me.ImageIndexForConstraintType(lvItem.Item.Constraint.Type, lvItem.Item.IsReference)
            Next
            lvItem = CType(Me.lvList.SelectedItems(0), ArchetypeListViewItem)
            'Force the change to selected image
            element = lvItem.Item
            lvItem.ImageIndex = Me.ImageIndexForConstraintType(element.Constraint.Type, element.IsReference, True)
            SetCurrentItem(element)
            If lvItem.Item.HasReferences Then
                MenuRemoveItemAndReference.Text = MenuRemoveItemAndReference.Text & " [+]"
            End If
        Else
            Me.MenuRemove.Visible = False
        End If

    End Sub

    Private Sub lvList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvList.MouseMove
        Dim lvItem As ArchetypeListViewItem
        lvItem = CType(Me.lvList.GetItemAt(e.X, e.Y), ArchetypeListViewItem)
        If Not lvItem Is Nothing Then
            SetToolTipSpecialisation(Me.lvList, CType(Me.lvList.GetItemAt(e.X, e.Y), ArchetypeListViewItem).Item)
        End If
    End Sub


    Private Sub lvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvList.KeyDown
        If e.KeyCode = Keys.Delete Then
            Me.RemoveItemAndReferences(sender, e)
        End If
    End Sub

    Private Sub lvList_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvList.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            Dim lvItem As ArchetypeListViewItem

            lvItem = CType(Me.lvList.Items(e.Item), ArchetypeListViewItem)

            If e.Label = "" Then
                e.CancelEdit = True
                Return
            End If

            lvItem.Text = e.Label
            Me.MenuRemoveItemAndReference.Text = e.Label
            If lvItem.Item.HasReferences Then
                MenuRemoveItemAndReference.Text = MenuRemoveItemAndReference.Text & " [+]"
            End If
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub lvList_BeforeLabelEdit(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvList.BeforeLabelEdit
        Dim i As Integer
        If Not mCurrentItem Is Nothing Then
            If (Not mCurrentItem.IsAnonymous) And lvList.SelectedItems.Count = 1 Then
                i = ArchetypeEditor.Instance.CountInString(CType(mCurrentItem, ArchetypeNodeAbstract).NodeId, ".")
                If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                    If MessageBox.Show(AE_Constants.Instance.RequiresSpecialisationToEdit, _
                        AE_Constants.Instance.MessageBoxCaption, _
                        MessageBoxButtons.YesNo, _
                        MessageBoxIcon.Warning, _
                        MessageBoxDefaultButton.Button2) = DialogResult.No Then
                        e.CancelEdit = True
                    End If
                End If
            End If
        End If
    End Sub

#Region "Drag and Drop"

    Private Sub lvList_DragDrop(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.DragEventArgs) Handles lvList.DragDrop
        Dim position As Point
        Dim DropListItem As ArchetypeListViewItem
        Dim list_item_dragged As ArchetypeListViewItem
        Dim i As Integer

        If Not mDragItem Is Nothing Then
            list_item_dragged = mDragItem
        ElseIf Not mDragArchetypeNode Is Nothing Then
            list_item_dragged = New ArchetypeListViewItem(CType(mDragArchetypeNode, ArchetypeElement))
        Else
            Debug.Assert(False, "No item dragged")
            mDragArchetypeNode = Nothing
            mDragItem = Nothing
            Return
        End If

        position.X = e.X
        position.Y = e.Y
        position = Me.lvList.PointToClient(position)
        DropListItem = CType(Me.lvList.GetItemAt(position.X, position.Y), ArchetypeListViewItem)

        If e.Effect = DragDropEffects.Move Then
            lvList.Items.Remove(list_item_dragged)
        End If

        If DropListItem Is Nothing Then
            i = lvList.Items.Count - 1
        Else
            i = DropListItem.Index
        End If

        lvList.Items.Insert(i + 1, list_item_dragged)

        list_item_dragged.Selected = True
        If e.Effect = DragDropEffects.Copy Then
            ' have to do this last or not visible
            list_item_dragged.BeginEdit()
        End If
        mFileManager.FileEdited = True

        mDragArchetypeNode = Nothing
        mDragItem = Nothing
    End Sub

    Private Sub lvList_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvList.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub lvList_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvList.ItemDrag
        mDragItem = CType(e.Item, ArchetypeListViewItem)
        Me.lvList.AllowDrop = True
        Me.lvList.DoDragDrop(e, DragDropEffects.Move)
    End Sub

#End Region

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
'The Original Code is ListStructure.vb.
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
