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
        lvList.SmallImageList = ilSmall

        Dim lvitem As ArchetypeListViewItem

        For Each item As RmStructure In rm.Children
            ' have to create links to the new archetype here to maintain updates
            Select Case item.Type
                Case StructureType.Element, StructureType.Reference
                    Dim element As RmElement = CType(item, RmElement)
                    lvitem = New ArchetypeListViewItem(element, mFileManager)
                    lvList.Items.Add(lvitem)
                    lvitem.RefreshIcons()
                Case StructureType.Slot
                    Dim slot As RmSlot = CType(item, RmSlot)

                    If slot.SlotConstraint.RM_ClassType = Global.ArchetypeEditor.StructureType.Element Then
                        lvitem = New ArchetypeListViewItem(slot, mFileManager)
                        lvList.Items.Add(lvitem)
                        lvitem.RefreshIcons()
                    End If
                Case Else
                    Debug.Assert(False, "Type not handled")
            End Select
        Next
    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New("List", a_file_manager)  ' structure type list
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        'Not able to set imagelists that are inherited
        lvList.SmallImageList = Me.ilSmall
    End Sub

    Public Sub New()
        MyBase.New()  ' structure type list
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        'Not able to set imagelists that are inherited
        lvList.SmallImageList = ilSmall

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
    Friend WithEvents RemoveMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents NameSlotMenuItem As MenuItem
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents AddReferenceMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RemoveItemAndReferencesMenuItem As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lvList = New System.Windows.Forms.ListView
        Me.ElementName = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuList = New System.Windows.Forms.ContextMenu
        Me.RemoveMenuItem = New System.Windows.Forms.MenuItem
        Me.RemoveItemAndReferencesMenuItem = New System.Windows.Forms.MenuItem
        Me.SpecialiseMenuItem = New System.Windows.Forms.MenuItem
        Me.AddReferenceMenuItem = New System.Windows.Forms.MenuItem
        Me.NameSlotMenuItem = New System.Windows.Forms.MenuItem
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
        Me.lvList.Location = New System.Drawing.Point(40, 27)
        Me.lvList.MultiSelect = False
        Me.lvList.Name = "lvList"
        Me.lvList.Size = New System.Drawing.Size(344, 379)
        Me.lvList.TabIndex = 38
        Me.lvList.UseCompatibleStateImageBehavior = False
        Me.lvList.View = System.Windows.Forms.View.Details
        '
        'ElementName
        '
        Me.ElementName.Text = "Element"
        Me.ElementName.Width = 350
        '
        'ContextMenuList
        '
        Me.ContextMenuList.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.RemoveMenuItem, Me.SpecialiseMenuItem, Me.AddReferenceMenuItem, Me.NameSlotMenuItem})
        '
        'RemoveMenuItem
        '
        Me.RemoveMenuItem.Index = 0
        Me.RemoveMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.RemoveItemAndReferencesMenuItem})
        Me.RemoveMenuItem.Text = "Remove"
        '
        'RemoveItemAndReferencesMenuItem
        '
        Me.RemoveItemAndReferencesMenuItem.Index = 0
        Me.RemoveItemAndReferencesMenuItem.Text = "?"
        '
        'SpecialiseMenuItem
        '
        Me.SpecialiseMenuItem.Index = 1
        Me.SpecialiseMenuItem.Text = "Specialise"
        '
        'AddReferenceMenuItem
        '
        Me.AddReferenceMenuItem.Index = 2
        Me.AddReferenceMenuItem.Text = "Add Reference"
        '
        'NameSlotMenuItem
        '
        Me.NameSlotMenuItem.Index = 3
        Me.NameSlotMenuItem.Text = "Name this Slot"
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

        If Not DesignMode Then
            'Set the menu texts
            If Main.Instance.DefaultLanguageCode <> "en" Then
                RemoveMenuItem.Text = AE_Constants.Instance.Remove
                SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
                AddReferenceMenuItem.Text = AE_Constants.Instance.AddReference
                NameSlotMenuItem.Text = AE_Constants.Instance.NameThisSlot
            End If

            ' add the change structure menu from EntryStructure
            If Not ContextMenuList.MenuItems.Contains(menuChangeStructure) Then
                ContextMenuList.MenuItems.Add(menuChangeStructure)
            End If
        End If
    End Sub

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Return lvList.Items
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructure
        Get
            Dim result As New RmStructureCompound(mNodeId, StructureType.List)
            result.Children.Cardinality = mCardinalityControl.Cardinality

            For Each lvItem As ArchetypeListViewItem In lvList.Items
                result.Children.Add(lvItem.Item.RM_Class)
            Next

            Return result
        End Get
        Set(ByVal value As RmStructure)
            Dim compound As RmStructureCompound = CType(value, RmStructureCompound)
            Dim struct As RmStructure
            lvList.Items.Clear()
            mNodeId = value.NodeId

            Select Case value.Type
                Case StructureType.Tree
                    ProcessTreeToList(compound)
                Case StructureType.Single
                    struct = compound.Children.FirstElementOrElementSlot
                    AddRmStructureToList(struct)
                Case StructureType.Table
                    If compound.Children.Items(0).Type = StructureType.Cluster Then
                        Dim clust As RmCluster = CType(compound.Children.Items(0), RmCluster)

                        For Each struct In clust.Children
                            AddRmStructureToList(struct)
                        Next
                    Else
                        Debug.Assert(False, "Not expected type")
                    End If
            End Select

            If lvList.Items.Count > 0 Then
                lvList.Items(0).Selected = True
            End If
        End Set
    End Property

    Private Sub AddRmStructureToList(ByVal struct As RmStructure)
        If Not struct Is Nothing Then
            Dim lvItem As ArchetypeListViewItem = Nothing

            Select Case struct.Type
                Case StructureType.Element, StructureType.Reference
                    lvItem = New ArchetypeListViewItem(CType(struct, RmElement), mFileManager)
                Case StructureType.Slot
                    If CType(struct, RmSlot).SlotConstraint.RM_ClassType = Global.ArchetypeEditor.StructureType.Element Then
                        lvItem = New ArchetypeListViewItem(CType(struct, RmSlot), mFileManager)
                    End If
                Case Else
                    Debug.Assert(False, "Type not handled")
            End Select

            If Not lvItem Is Nothing Then
                lvList.Items.Add(lvItem)
                lvItem.RefreshIcons()
            End If
        End If
    End Sub

    Private Sub ProcessTreeToList(ByVal rm As RmStructureCompound)
        For Each struct As RmStructure In rm.Children
            If struct.Type = StructureType.Cluster Then
                ProcessTreeToList(CType(struct, RmStructureCompound))
            Else
                AddRmStructureToList(struct)
            End If
        Next
    End Sub

    Public Overrides Sub Reset()
        lvList.Items.Clear()
    End Sub

    Public Overrides Sub Translate()
        Dim lvitem As ArchetypeListViewItem

        For Each lvitem In lvList.Items
            lvitem.Translate()
        Next

        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Protected Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click
        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems.Item(0), ArchetypeListViewItem)
            Dim node As ArchetypeNodeAbstract = CType(lvItem.Item, ArchetypeNodeAbstract)

            Dim dlg As New SpecialisationQuestionDialog()
            dlg.ShowForArchetypeNode(lvItem.Item.Text, node.RM_Class, SpecialisationDepth)

            If dlg.IsSpecialisationRequested Then
                If dlg.IsCloningRequested Then
                    Dim i As Integer = lvItem.Index
                    lvItem = lvItem.SpecialisedClone
                    lvList.Items.Insert(i + 1, lvItem)
                Else
                    lvItem.Specialise()
                End If

                lvList.SelectedItems.Clear()
                lvItem.Selected = True
                SetCurrentItem(lvItem.Item)
                RenameSelectedItem()
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs) Handles AddReferenceMenuItem.Click
        Dim ref As RmReference

        ' create a new reference element pointing to this element
        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

            If Not lvItem.Item.IsAnonymous Then
                ref = New RmReference(CType(lvItem.Item, ArchetypeElement).RM_Class)
                lvItem = New ArchetypeListViewItem(ref, mFileManager)
                ' insert in the list
                lvList.Items.Insert(lvList.SelectedIndices(0) + 1, lvItem)
                lvItem.RefreshIcons()
                mFileManager.FileEdited = True
            Else
                Debug.Assert(False)
            End If
        End If
    End Sub

    Protected Overrides Sub NameSlot(ByVal sender As Object, ByVal e As System.EventArgs) Handles NameSlotMenuItem.Click
        RenameSelectedItem()
    End Sub

    Protected Sub RenameSelectedItem()
        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

            If lvItem.Item.IsAnonymous Then
                Dim newSlot As New ArchetypeSlot(CType(lvItem.Item, ArchetypeNodeAnonymous), mFileManager)
                Dim i As Integer = lvItem.Index
                lvList.Items.RemoveAt(i)
                lvItem = New ArchetypeListViewItem(newSlot)
                lvList.Items.Insert(i, lvItem)
                lvItem.RefreshIcons()

                If Not lvItem.Selected Then
                    ' needed for first element in the list
                    lvItem.Selected = True
                End If

                mFileManager.FileEdited = True
            End If

            If lvList.LabelEdit Then
                lvItem.BeginEdit()
            End If
        End If
    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        mConstraintMenu.Show(ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Public Overrides Sub SetInitial()
        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        End If
    End Sub

    Protected Overrides Sub AddNewElement(ByVal aConstraint As Constraint)
        Dim lvItem As ArchetypeListViewItem = Nothing
        Dim editLabel As Boolean = False

        If aConstraint.Kind = ConstraintKind.Slot Then
            Select Case MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                Case DialogResult.Yes
                    Dim archetype_slot As New ArchetypeSlot(mFileManager.OntologyManager.GetOpenEHRTerm(CInt(StructureType.Element), StructureType.Element.ToString), StructureType.Element, mFileManager)
                    lvItem = New ArchetypeListViewItem(archetype_slot)
                    editLabel = True
                Case DialogResult.No
                    Dim newSlot As New RmSlot(StructureType.Element)
                    lvItem = New ArchetypeListViewItem(newSlot, mFileManager)
            End Select
        Else
            lvItem = New ArchetypeListViewItem(Filemanager.GetOpenEhrTerm(109, "New Element"), mFileManager)
            CType(lvItem.Item, ArchetypeElement).Constraint = aConstraint
            editLabel = True
        End If

        lvList.Items.Add(lvItem)
        lvItem.Selected = True
        lvItem.RefreshIcons()
        mFileManager.FileEdited = True

        If editLabel Then
            lvItem.BeginEdit()
        End If
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs) Handles RemoveItemAndReferencesMenuItem.Click
        If Me.lvList.SelectedIndices.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem
            Dim message As String

            lvItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

            If lvItem.Item.HasReferences Then
                message = AE_Constants.Instance.Remove & Me.lvList.SelectedItems(0).Text & " " & AE_Constants.Instance.AllReferences
            Else
                message = AE_Constants.Instance.Remove & Me.lvList.SelectedItems(0).Text
            End If

            If MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                Dim nodeid As String

                ' leave an item selected if there is one
                If lvItem.Index > 0 Then
                    lvList.Items(lvItem.Index - 1).Selected = True
                ElseIf lvList.Items.Count > 1 Then
                    lvList.Items(lvItem.Index + 1).Selected = True
                Else
                    SetCurrentItem(Nothing)
                End If

                ' if this is a reference then remove all items with nodeid
                If lvItem.Item.HasReferences Then
                    nodeid = CType(lvItem.Item, ArchetypeElement).NodeId

                    For Each lvItem In lvList.Items
                        If Not lvItem.Item.IsAnonymous AndAlso CType(lvItem.Item, ArchetypeElement).NodeId = nodeid Then
                            lvItem.Remove()
                        End If
                    Next
                Else
                    lvItem.Remove()
                End If
                If lvList.SelectedItems.Count = 0 Then
                    lvItem = Nothing
                    SetCurrentItem(Nothing)
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

        text = new_line & (Space(3 * indentlevel) & "\cf1 Structure\cf0  = \cf2 LIST\cf0\par")
        s = ""
        If mCardinalityControl.Cardinality.Ordered Then
            s = "ordered"
        End If

        s = s.Trim

        text = text & new_line & (Space(3 * indentlevel) & "\cf2 Items\cf0  " & s & "\par")
        For Each lvItem In Me.lvList.Items
            text &= new_line & lvItem.Item.ToRichText(indentlevel + 1)
        Next

        Return text
    End Function

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim lvItem As ArchetypeListViewItem
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim showComments As Boolean = Main.Instance.Options.ShowCommentsInHtml
        Dim s As String = ""

        If mCardinalityControl.Cardinality.Ordered Then
            s &= ", ordered"
        End If

        s = s.Trim
        result.AppendFormat("<p><i>Structure</i>: {0} {1}</p>", Filemanager.GetOpenEhrTerm(108, "LIST"), s)
        result.Append(Environment.NewLine)
        result.Append("<table border=""1"" cellpadding=""2"" width=""100%"">")
        result.AppendFormat(Me.HtmlHeader(BackGroundColour, showComments))

        For Each lvItem In Me.lvList.Items
            result.AppendFormat("{0}{1}", Environment.NewLine, lvItem.Item.ToHTML(0, showComments))
            result.AppendFormat("{0}</tr>", Environment.NewLine)
        Next

        result.Append("</table>")

        Return result.ToString()
    End Function

    Public Overrides Function HasData() As Boolean
        Return lvList.Items.Count > 0
    End Function

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListUp.Click
        If Not lvList.SelectedIndices.Count = 0 Then
            Dim i As Integer = lvList.SelectedIndices(0)
            Dim lvItem As ListViewItem = lvList.SelectedItems(0)

            If i > 0 Then
                lvList.Items.Remove(lvItem)
                lvList.Items.Insert(i - 1, lvItem)
                mFileManager.FileEdited = True
                lvItem.Selected = True
            End If
        End If
    End Sub

    Protected Overrides Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not lvList.SelectedIndices.Count = 0 Then
            Dim i As Integer = lvList.SelectedIndices(0)
            Dim lvItem As ListViewItem = lvList.SelectedItems(0)

            If i < lvList.Items.Count - 1 Then
                lvList.Items.Remove(lvItem)
                lvList.Items.Insert(i + 1, lvItem)
                lvItem.Selected = True
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub RefreshIcons()
        For Each lvItem As ArchetypeListViewItem In lvList.Items
            lvItem.RefreshIcons()
        Next
    End Sub

    Private Sub ContextMenuList_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuList.Popup
        RemoveMenuItem.Visible = False
        SpecialiseMenuItem.Visible = False
        AddReferenceMenuItem.Visible = False
        NameSlotMenuItem.Visible = False

        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)
            RemoveItemAndReferencesMenuItem.Text = lvItem.Text

            Dim item As ArchetypeNode = lvItem.Item
            Dim element As ArchetypeElement = TryCast(item, ArchetypeElement)
            AddReferenceMenuItem.Visible = Not (element Is Nothing OrElse element.IsReference)

            If item.IsAnonymous Then
                NameSlotMenuItem.Visible = True
                RemoveMenuItem.Visible = True
            Else
                Dim nodeId As String = CType(item, ArchetypeNodeAbstract).NodeId
                Dim i As Integer = item.RM_Class.SpecialisationDepth

                RemoveMenuItem.Visible = i = SpecialisationDepth And (i = 0 Or nodeId.StartsWith("at0.") Or nodeId.IndexOf(".0.") > -1)
                SpecialiseMenuItem.Visible = SpecialisationDepth > 0 And (i < SpecialisationDepth Or item.Occurrences.IsMultiple)
            End If
        End If
    End Sub

    Private Sub lvList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvList.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If lvList.SelectedItems.Count > 0 AndAlso lvList.SelectedItems(0).Bounds.Contains(e.Location) Then
                Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

                If Not lvItem Is Nothing AndAlso lvItem.Item.IsAnonymous Then
                    If MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        RenameSelectedItem()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub lvList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvList.SelectedIndexChanged
        If lvList.Items.Count = 0 Then
            butChangeDataType.Hide()
        ElseIf lvList.SelectedItems.Count = 1 Then
            RemoveItemAndReferencesMenuItem.Text = lvList.SelectedItems(0).Text
            RefreshIcons()

            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)
            SetCurrentItem(lvItem.Item)

            If lvItem.Item.HasReferences Then
                RemoveItemAndReferencesMenuItem.Text = String.Format("{0} [+]", RemoveItemAndReferencesMenuItem.Text)
            End If

            lvList.LabelEdit = Not lvItem.Item.IsReference
        Else
            RemoveMenuItem.Visible = False
        End If
    End Sub

    Private Sub lvList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvList.MouseMove
        Dim lvItem As ArchetypeListViewItem = CType(lvList.GetItemAt(e.X, e.Y), ArchetypeListViewItem)

        If Not lvItem Is Nothing Then
            SetToolTipSpecialisation(lvList, CType(lvList.GetItemAt(e.X, e.Y), ArchetypeListViewItem).Item)
        End If
    End Sub

    Private Sub lvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvList.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                RenameSelectedItem()
            Case Keys.Delete
                If lvList.SelectedItems.Count > 0 Then
                    Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)
                    Dim item As ArchetypeNode = lvItem.Item

                    If item.IsAnonymous Then
                        RemoveItemAndReferences(sender, e)
                    Else
                        Dim nodeId As String = CType(item, ArchetypeElement).NodeId
                        Dim i As Integer = item.RM_Class.SpecialisationDepth

                        If i = SpecialisationDepth And (i = 0 Or nodeId.StartsWith("at0.") Or nodeId.IndexOf(".0.") > -1) Then
                            RemoveItemAndReferences(sender, e)
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub lvList_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvList.AfterLabelEdit
        ' add the update of the Term and description

        If Not e.Label Is Nothing Then
            If e.Label = "" Or e.Label = " " Or e.Label = "  " Then
                e.CancelEdit = True
            Else
                Dim lvItem As ArchetypeListViewItem

                lvItem = CType(Me.lvList.Items(e.Item), ArchetypeListViewItem)
                lvItem.Text = e.Label

                RemoveItemAndReferencesMenuItem.Text = e.Label

                If lvItem.Item.HasReferences Then
                    Translate()
                    RemoveItemAndReferencesMenuItem.Text = String.Format("{0} [+]", RemoveItemAndReferencesMenuItem.Text)
                End If

                'Slots set the text to include the class
                If lvItem.Text <> e.Label Then
                    e.CancelEdit = True
                End If

                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub lvList_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvList.BeforeLabelEdit
        If Not mCurrentItem Is Nothing Then
            If Not mCurrentItem.IsAnonymous And lvList.SelectedItems.Count = 1 Then
                If mCurrentItem.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                    e.CancelEdit = True
                    SpecialiseCurrentItem(sender, e)
                End If
            Else
                e.CancelEdit = True
            End If
        End If
    End Sub

#Region "Drag and Drop"

    Private Sub lvList_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvList.ItemDrag
        mDragItem = CType(e.Item, ArchetypeListViewItem)
        lvList.AllowDrop = True
        lvList.DoDragDrop(e, DragDropEffects.Move)
    End Sub

    Private Sub lvList_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvList.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub lvList_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvList.DragDrop
        Dim position As Point
        Dim DropListItem As ArchetypeListViewItem
        Dim list_item_dragged As ArchetypeListViewItem = Nothing
        Dim editLabel As Boolean = False
        Dim i As Integer

        If Not mDragItem Is Nothing Then
            list_item_dragged = mDragItem
        ElseIf Not mNewConstraint Is Nothing Then
            If TypeOf mNewConstraint Is Constraint_Slot Then
                Select Case MessageBox.Show(AE_Constants.Instance.NameThisSlotQuestion, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    Case DialogResult.Yes
                        Dim archetype_slot As New ArchetypeSlot(mFileManager.OntologyManager.GetOpenEHRTerm(CInt(StructureType.Element), StructureType.Element.ToString), StructureType.Element, mFileManager)
                        list_item_dragged = New ArchetypeListViewItem(archetype_slot)
                        editLabel = True
                    Case DialogResult.No
                        Dim newSlot As New RmSlot(StructureType.Element)
                        list_item_dragged = New ArchetypeListViewItem(newSlot, mFileManager)
                End Select
            Else
                Dim archetype_element As ArchetypeElement
                archetype_element = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New element"), mFileManager)
                archetype_element.Constraint = mNewConstraint
                list_item_dragged = New ArchetypeListViewItem(archetype_element)
                editLabel = True
            End If
        Else
            Debug.Assert(False, "No item dragged")
            mNewConstraint = Nothing
            mNewCluster = False
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

        If editLabel And e.Effect = DragDropEffects.Copy Then
            ' have to do this last or not visible
            list_item_dragged.BeginEdit()
        End If

        mFileManager.FileEdited = True

        mNewConstraint = Nothing
        mNewCluster = False
        mDragItem = Nothing
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

