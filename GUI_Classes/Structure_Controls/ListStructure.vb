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

        For Each item As RmStructure In rm.Children
            ' have to create links to the new archetype here to maintain updates
            Select Case item.Type
                Case StructureType.Element, StructureType.Reference
                    Dim element As RmElement = CType(item, RmElement)
                    lvitem = New ArchetypeListViewItem(element, mFileManager)
                    'Sets selected if first in list
                    lvitem.ImageIndex = ImageIndexForConstraintType(element.Constraint.Type, element.isReference, lvList.Items.Count = 0)
                    Me.lvList.Items.Add(lvitem)
                Case StructureType.Slot
                    Dim slot As RmSlot = CType(item, RmSlot)
                    If slot.SlotConstraint.RM_ClassType = Global.ArchetypeEditor.StructureType.Element Then
                        lvitem = New ArchetypeListViewItem(slot, mFileManager)
                        'Sets selected if first in list
                        lvitem.ImageIndex = ImageIndexForConstraintType(ConstraintType.Slot, False, lvList.Items.Count = 0)
                        Me.lvList.Items.Add(lvitem)
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
    Friend WithEvents MenuNameSlot As MenuItem
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents MenuAddReference As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRemoveItemAndReference As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lvList = New System.Windows.Forms.ListView
        Me.ElementName = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuList = New System.Windows.Forms.ContextMenu
        Me.MenuRemove = New System.Windows.Forms.MenuItem
        Me.MenuNameSlot = New System.Windows.Forms.MenuItem
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
        Me.lvList.Location = New System.Drawing.Point(40, 27)
        Me.lvList.MultiSelect = False
        Me.lvList.Name = "lvList"
        Me.lvList.Size = New System.Drawing.Size(344, 333)
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
        Me.ContextMenuList.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuRemove, Me.SpecialiseMenuItem, Me.MenuAddReference, Me.MenuNameSlot})
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
        'MenuNameSlot
        '
        Me.MenuNameSlot.Index = 3
        Me.MenuNameSlot.Text = "Name this slot"
        '

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
            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                MenuRemove.Text = AE_Constants.Instance.Remove
                SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
                MenuAddReference.Text = AE_Constants.Instance.Add_Reference
                MenuNameSlot.Text = AE_Constants.Instance.NameThisSlot
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

    'Public Overrides ReadOnly Property Elements() As ArchetypeElement()
    '    Get
    '        Dim i As Integer
    '        i = lvList.Items.Count
    '        If i > 0 Then
    '            Dim a_e(i) As ArchetypeElement
    '            For i = 0 To lvList.Items.Count - 1

    '                a_e(i) = CType(lvList.Items(i), ArchetypeListViewItem).Item
    '            Next
    '            Return a_e
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    'End Property

    Public Overrides Property Archetype() As RmStructureCompound
        Get
            Dim lvItem As ArchetypeListViewItem

            Dim RM_S As New RmStructureCompound(mNodeId, StructureType.List)

            RM_S.Children.Cardinality = mCardinalityControl.Cardinality

            For Each lvItem In Me.lvList.Items
                RM_S.Children.Add(lvItem.Item.RM_Class)
            Next
            Return RM_S
        End Get
        Set(ByVal Value As RmStructureCompound)
            Dim aStructure As RmStructure
            ' handles conversion from other structures
            Me.lvList.Items.Clear()
            mNodeId = Value.NodeId
            Select Case Value.Type '.TypeName
                Case StructureType.Tree ' "TREE"
                    ProcessTreeToList(Value)
                Case StructureType.Single ' "SINGLE"
                    aStructure = Value.Children.FirstElementOrElementSlot
                    AddRmStructureToList(aStructure)
                Case StructureType.Table ' "TABLE"
                    If Value.Children.items(0).Type = StructureType.Cluster Then
                        Dim clust As RmCluster

                        clust = CType(Value.Children.items(0), RmCluster)

                        For Each aStructure In clust.Children
                            AddRmStructureToList(aStructure)
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

    Private Sub AddRmStructureToList(ByVal a_structure As RmStructure)
        If Not a_structure Is Nothing Then
            Select Case a_structure.Type
                Case StructureType.Element, StructureType.Reference
                    lvList.Items.Add(New ArchetypeListViewItem(CType(a_structure, RmElement), mFileManager))
                Case StructureType.Slot
                    If CType(a_structure, RmSlot).SlotConstraint.RM_ClassType = Global.ArchetypeEditor.StructureType.Element Then
                        lvList.Items.Add(New ArchetypeListViewItem(CType(a_structure, RmSlot), mFileManager))
                    End If
                Case Else
                    Debug.Assert(False, "Type not handled")
            End Select
        End If
    End Sub

    Private Sub ProcessTreeToList(ByVal rm As RmStructureCompound)
        For Each aStructure As RmStructure In rm.Children
            'If a_rm_structure.TypeName = "Cluster" Then
            If aStructure.Type = StructureType.Cluster Then
                ProcessTreeToList(CType(aStructure, RmStructureCompound))
            Else
                AddRmStructureToList(aStructure)
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

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click
        If lvList.SelectedItems.Count > 0 Then
            Dim lvitem As ArchetypeListViewItem = CType(lvList.SelectedItems.Item(0), ArchetypeListViewItem)
            'Fixme - need to add code to check if can be specialised

            If MessageBox.Show(String.Format("{0} '{1}'", AE_Constants.Instance.Specialise, lvitem.Text), _
                AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

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
                SetCurrentItem(lvitem.Item)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs) Handles MenuAddReference.Click
        Dim ref As RmReference

        ' create a new reference element pointing to this element
        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

            If Not lvItem.Item.IsAnonymous Then
                ref = New RmReference(CType(lvItem.Item, ArchetypeElement).RM_Class)
                ' record the presence of the reference so a delete can be safe
                CType(lvItem.Item.RM_Class, RmElement).hasReferences = True
                lvItem = New ArchetypeListViewItem(ref, mFileManager)
                ' insert in the list
                lvItem.ImageIndex = ImageIndexForConstraintType(CType(lvItem.Item, ArchetypeElement).Constraint.Type, True, False)
                lvList.Items.Insert(lvList.SelectedIndices(0) + 1, lvItem)
                mFileManager.FileEdited = True
            Else
                Debug.Assert(False)
            End If
        End If
    End Sub

    Protected Overrides Sub NameSlot(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuNameSlot.Click
        If lvList.SelectedItems.Count > 0 Then
            ReplaceAnonymousSlot()
            lvList.SelectedItems(0).BeginEdit()
        End If
    End Sub

    Protected Sub ReplaceAnonymousSlot()
        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

            If lvItem.Item.IsAnonymous Then
                Dim newSlot As New ArchetypeSlot(CType(lvItem.Item, ArchetypeNodeAnonymous), mFileManager)
                Dim i As Integer = lvItem.Index
                lvList.Items.RemoveAt(i)
                lvItem = New ArchetypeListViewItem(newSlot)
                lvItem.ImageIndex = ImageIndexForConstraintType(ConstraintType.Slot, False, True)

                lvList.Items.Insert(i, lvItem)

                If Not lvItem.Selected Then
                    ' needed for first element in the list
                    lvItem.Selected = True
                End If

                mFileManager.FileEdited = True
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

    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        Dim lvItem As ArchetypeListViewItem = Nothing
        Dim editLabel As Boolean = False

        If a_constraint.Type = ConstraintType.Slot Then
            Select Case MessageBox.Show(AE_Constants.Instance.NameThisSlot, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            CType(lvItem.Item, ArchetypeElement).Constraint = a_constraint
            editLabel = True
        End If

        lvItem.ImageIndex = Me.ImageIndexForConstraintType(a_constraint.Type, False, True)
        lvList.Items.Add(lvItem)
        mFileManager.FileEdited = True

        If Not lvItem.Selected Then
            ' needed for first element in the list
            lvItem.Selected = True
        End If

        If editLabel Then
            lvItem.BeginEdit()
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
        Dim showComments As Boolean = OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml
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
            Dim lvI As ListViewItem
            Dim i As Integer
            i = lvList.SelectedIndices(0)
            lvI = lvList.SelectedItems(0)

            If i > 0 Then
                lvList.Items.Remove(lvI)
                lvList.Items.Insert((i - 1), lvI)
                mFileManager.FileEdited = True
                lvList.Items.Item(i - 1).Selected = True
            End If
        End If
    End Sub

    Protected Overrides Sub butListDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butListDown.Click
        If Not lvList.SelectedIndices.Count = 0 Then
            Dim lvI As ListViewItem
            Dim i, c As Integer

            c = lvList.Items.Count
            i = lvList.SelectedIndices(0)
            lvI = lvList.SelectedItems(0)

            If i < (c - 1) Then
                lvList.Items.Remove(lvI)
                lvList.Items.Insert((i + 1), lvI)
                lvI.Selected = True
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub RefreshIcons()
        If mCurrentItem.HasReferences Then
            Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)

            For Each lvItem As ArchetypeListViewItem In lvList.Items
                If Not lvItem.Item.IsAnonymous AndAlso CType(lvItem.Item, ArchetypeElement).NodeId = element.NodeId Then
                    lvItem.ImageIndex = ImageIndexForItem(lvItem.Item, False)
                End If
            Next
        Else
            If lvList.SelectedItems.Count = 0 Then
                If lvList.Items.Count = 0 Then
                    Return
                End If
                lvList.Items(0).Selected = True
            End If

            CType(lvList.SelectedItems(0), ArchetypeListViewItem).ImageIndex = Me.ImageIndexForItem(mCurrentItem, True)
        End If
    End Sub

    Private Sub ContextMenuList_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuList.Popup
        MenuRemove.Visible = False
        SpecialiseMenuItem.Visible = False
        MenuAddReference.Visible = False
        MenuNameSlot.Visible = False

        If lvList.SelectedItems.Count > 0 Then
            Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)
            MenuRemoveItemAndReference.Text = lvItem.Text

            'If it is an element and not a slot
            If Not lvItem.Item.IsAnonymous Then
                'may be a reference and can't add a reference
                If TypeOf lvItem.Item Is ArchetypeElement Then
                    Dim element As ArchetypeElement = CType(lvItem.Item, ArchetypeElement)

                    If Not element.IsReference Then
                        MenuAddReference.Visible = True
                    End If
                End If

                ' show specialisation if appropriate
                Dim nodeId As String = CType(lvItem.Item, ArchetypeNodeAbstract).NodeId
                Dim i As Integer = OceanArchetypeEditor.Instance.CountInString(nodeId, ".")
                Dim numberSpecialisations As Integer = mFileManager.OntologyManager.NumberOfSpecialisations

                If i < numberSpecialisations Then
                    SpecialiseMenuItem.Visible = True
                Else
                    If numberSpecialisations = 0 Or ((nodeId.StartsWith("at0.") Or (nodeId.IndexOf(".0.") > -1))) Then
                        MenuRemove.Visible = True
                    End If
                End If
            Else
                MenuNameSlot.Visible = True
                MenuRemove.Visible = True
            End If
        End If
    End Sub

    Private Sub lvList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvList.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If lvList.SelectedItems.Count > 0 AndAlso lvList.SelectedItems(0).Bounds.Contains(e.Location) Then
                Dim lvItem As ArchetypeListViewItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

                If Not lvItem Is Nothing AndAlso lvItem.Item.IsAnonymous Then
                    If MessageBox.Show(AE_Constants.Instance.NameThisSlot, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ReplaceAnonymousSlot()
                        lvList.SelectedItems(0).BeginEdit()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub lvList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvList.SelectedIndexChanged
        Dim lvItem As ArchetypeListViewItem

        If lvList.Items.Count = 0 Then
            butChangeDataType.Hide()
        ElseIf lvList.SelectedItems.Count = 1 Then
            MenuRemoveItemAndReference.Text = lvList.SelectedItems(0).Text

            'Unselect the previous item
            For Each lvItem In lvList.Items
                lvItem.ImageIndex = ImageIndexForItem(lvItem.Item, lvItem.Selected)
            Next

            lvItem = CType(Me.lvList.SelectedItems(0), ArchetypeListViewItem)
            'Force the change to selected image
            'lvItem.ImageIndex = Me.ImageIndexForItem(lvItem.Item, True)
            SetCurrentItem(lvItem.Item)

            If lvItem.Item.HasReferences Then
                MenuRemoveItemAndReference.Text = String.Format("{0} [+]", MenuRemoveItemAndReference.Text)
            End If

            lvList.LabelEdit = Not lvItem.Item.IsReference
        Else
            MenuRemove.Visible = False
        End If
    End Sub

    Private Sub lvList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvList.MouseMove
        Dim lvItem As ArchetypeListViewItem
        lvItem = CType(lvList.GetItemAt(e.X, e.Y), ArchetypeListViewItem)

        If Not lvItem Is Nothing Then
            SetToolTipSpecialisation(lvList, CType(lvList.GetItemAt(e.X, e.Y), ArchetypeListViewItem).Item)
        End If
    End Sub

    Private Sub lvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvList.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If lvList.SelectedItems.Count > 0 Then
                    ReplaceAnonymousSlot()
                    lvList.SelectedItems(0).BeginEdit()
                End If
            Case Keys.Delete
                Dim lvItem As ArchetypeListViewItem

                If lvList.SelectedItems.Count > 0 Then
                    lvItem = CType(lvList.SelectedItems(0), ArchetypeListViewItem)

                    If lvItem.Item.RM_Class.Type = StructureType.Element Then
                        Dim id As String = CType(lvItem.Item, ArchetypeElement).NodeId
                        Dim i As Integer = OceanArchetypeEditor.Instance.CountInString(id, ".")
                        Dim numSpecs As Integer = mFileManager.OntologyManager.NumberOfSpecialisations

                        If numSpecs = 0 Or (i = numSpecs And ((id.StartsWith("at0.") Or (id.IndexOf(".0.") > -1)))) Then
                            RemoveItemAndReferences(sender, e)
                        End If
                    Else
                        RemoveItemAndReferences(sender, e)
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

                MenuRemoveItemAndReference.Text = e.Label

                If lvItem.Item.HasReferences Then
                    Translate()
                    MenuRemoveItemAndReference.Text = String.Format("{0} [+]", MenuRemoveItemAndReference.Text)
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
        Dim i As Integer

        If Not mCurrentItem Is Nothing Then
            If Not mCurrentItem.IsAnonymous And lvList.SelectedItems.Count = 1 Then
                i = OceanArchetypeEditor.Instance.CountInString(CType(mCurrentItem, ArchetypeNodeAbstract).NodeId, ".")

                If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                    If MessageBox.Show(AE_Constants.Instance.RequiresSpecialisationToEdit, _
                        AE_Constants.Instance.MessageBoxCaption, _
                        MessageBoxButtons.YesNo, _
                        MessageBoxIcon.Warning, _
                        MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                        e.CancelEdit = True
                    End If
                End If
            Else
                e.CancelEdit = True
            End If
        End If
    End Sub

#Region "Drag and Drop"

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
                Select Case MessageBox.Show(AE_Constants.Instance.NameThisSlot, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

