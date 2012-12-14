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
'	file:        "$Source: source/vb.net/archetype_editor/DataConstraints/SCCS/s.SlotConstraintControl.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class SlotConstraintControl : Inherits ConstraintControl

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
        lblParser.Text = String.Format("({0})", mFileManager.ParserType)

        If Main.Instance.DefaultLanguageCode <> "en" Then
            lblSlot.Text = Filemanager.GetOpenEhrTerm(312, lblSlot.Text)
            gbInclude.Text = Filemanager.GetOpenEhrTerm(625, gbInclude.Text)
            gbExclude.Text = Filemanager.GetOpenEhrTerm(626, gbExclude.Text)
            chkIncludeAll.Text = Filemanager.GetOpenEhrTerm(628, chkIncludeAll.Text)
            chkExcludeAll.Text = Filemanager.GetOpenEhrTerm(629, chkExcludeAll.Text)
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private components As System.ComponentModel.IContainer
    Friend WithEvents PanelSlotTop As System.Windows.Forms.Panel
    Friend WithEvents lblSlot As System.Windows.Forms.Label
    Friend WithEvents lblClass As System.Windows.Forms.Label
    Friend WithEvents butIncludeRemove As System.Windows.Forms.Button
    Friend WithEvents butInclude As System.Windows.Forms.Button
    Friend WithEvents listInclude As System.Windows.Forms.ListBox
    Friend WithEvents chkIncludeAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkExcludeAll As System.Windows.Forms.CheckBox
    Friend WithEvents butExcludeRemove As System.Windows.Forms.Button
    Friend WithEvents butExclude As System.Windows.Forms.Button
    Friend WithEvents listExclude As System.Windows.Forms.ListBox
    Friend WithEvents PanelExclude As System.Windows.Forms.Panel
    Friend WithEvents PanelInlcude As System.Windows.Forms.Panel
    Friend WithEvents PanelExcludeStatements As System.Windows.Forms.Panel
    Friend WithEvents PanelExcludeMinusTick As System.Windows.Forms.Panel
    Friend WithEvents PanelIncludeStatements As System.Windows.Forms.Panel
    Friend WithEvents PanelIncludeMinusTick As System.Windows.Forms.Panel
    Friend WithEvents gbExclude As System.Windows.Forms.GroupBox
    Friend WithEvents gbInclude As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents butBrowse As System.Windows.Forms.Button
    Friend WithEvents lblParser As System.Windows.Forms.Label
    Friend WithEvents AvailableArchetypesListBox As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SlotConstraintControl))
        Me.PanelInlcude = New System.Windows.Forms.Panel
        Me.butInclude = New System.Windows.Forms.Button
        Me.chkIncludeAll = New System.Windows.Forms.CheckBox
        Me.butIncludeRemove = New System.Windows.Forms.Button
        Me.listInclude = New System.Windows.Forms.ListBox
        Me.PanelExclude = New System.Windows.Forms.Panel
        Me.butExclude = New System.Windows.Forms.Button
        Me.chkExcludeAll = New System.Windows.Forms.CheckBox
        Me.butExcludeRemove = New System.Windows.Forms.Button
        Me.listExclude = New System.Windows.Forms.ListBox
        Me.PanelSlotTop = New System.Windows.Forms.Panel
        Me.lblParser = New System.Windows.Forms.Label
        Me.butBrowse = New System.Windows.Forms.Button
        Me.AvailableArchetypesListBox = New System.Windows.Forms.ListBox
        Me.lblClass = New System.Windows.Forms.Label
        Me.lblSlot = New System.Windows.Forms.Label
        Me.PanelExcludeStatements = New System.Windows.Forms.Panel
        Me.PanelExcludeMinusTick = New System.Windows.Forms.Panel
        Me.PanelIncludeStatements = New System.Windows.Forms.Panel
        Me.PanelIncludeMinusTick = New System.Windows.Forms.Panel
        Me.gbExclude = New System.Windows.Forms.GroupBox
        Me.gbInclude = New System.Windows.Forms.GroupBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.PanelInlcude.SuspendLayout()
        Me.PanelExclude.SuspendLayout()
        Me.PanelSlotTop.SuspendLayout()
        Me.PanelExcludeStatements.SuspendLayout()
        Me.PanelExcludeMinusTick.SuspendLayout()
        Me.PanelIncludeStatements.SuspendLayout()
        Me.PanelIncludeMinusTick.SuspendLayout()
        Me.gbExclude.SuspendLayout()
        Me.gbInclude.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelInlcude
        '
        Me.PanelInlcude.Controls.Add(Me.butInclude)
        Me.PanelInlcude.Controls.Add(Me.chkIncludeAll)
        Me.PanelInlcude.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelInlcude.Location = New System.Drawing.Point(3, 16)
        Me.PanelInlcude.Name = "PanelInlcude"
        Me.PanelInlcude.Size = New System.Drawing.Size(418, 40)
        Me.PanelInlcude.TabIndex = 33
        '
        'butInclude
        '
        Me.butInclude.Image = CType(resources.GetObject("butInclude.Image"), System.Drawing.Image)
        Me.butInclude.Location = New System.Drawing.Point(8, 2)
        Me.butInclude.Name = "butInclude"
        Me.butInclude.Size = New System.Drawing.Size(24, 24)
        Me.butInclude.TabIndex = 29
        '
        'chkIncludeAll
        '
        Me.chkIncludeAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkIncludeAll.Location = New System.Drawing.Point(40, 10)
        Me.chkIncludeAll.Name = "chkIncludeAll"
        Me.chkIncludeAll.Size = New System.Drawing.Size(368, 24)
        Me.chkIncludeAll.TabIndex = 32
        Me.chkIncludeAll.Text = "Include All"
        '
        'butIncludeRemove
        '
        Me.butIncludeRemove.Image = CType(resources.GetObject("butIncludeRemove.Image"), System.Drawing.Image)
        Me.butIncludeRemove.Location = New System.Drawing.Point(8, 8)
        Me.butIncludeRemove.Name = "butIncludeRemove"
        Me.butIncludeRemove.Size = New System.Drawing.Size(24, 24)
        Me.butIncludeRemove.TabIndex = 31
        '
        'listInclude
        '
        Me.listInclude.AllowDrop = True
        Me.listInclude.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listInclude.ForeColor = System.Drawing.Color.DarkGreen
        Me.listInclude.Location = New System.Drawing.Point(40, 0)
        Me.listInclude.Name = "listInclude"
        Me.listInclude.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.listInclude.Size = New System.Drawing.Size(368, 95)
        Me.listInclude.Sorted = True
        Me.listInclude.TabIndex = 38
        '
        'PanelExclude
        '
        Me.PanelExclude.Controls.Add(Me.butExclude)
        Me.PanelExclude.Controls.Add(Me.chkExcludeAll)
        Me.PanelExclude.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelExclude.Location = New System.Drawing.Point(3, 16)
        Me.PanelExclude.Name = "PanelExclude"
        Me.PanelExclude.Size = New System.Drawing.Size(418, 32)
        Me.PanelExclude.TabIndex = 33
        '
        'butExclude
        '
        Me.butExclude.Image = CType(resources.GetObject("butExclude.Image"), System.Drawing.Image)
        Me.butExclude.Location = New System.Drawing.Point(8, 3)
        Me.butExclude.Name = "butExclude"
        Me.butExclude.Size = New System.Drawing.Size(24, 24)
        Me.butExclude.TabIndex = 35
        '
        'chkExcludeAll
        '
        Me.chkExcludeAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkExcludeAll.Location = New System.Drawing.Point(40, 4)
        Me.chkExcludeAll.Name = "chkExcludeAll"
        Me.chkExcludeAll.Size = New System.Drawing.Size(368, 24)
        Me.chkExcludeAll.TabIndex = 38
        Me.chkExcludeAll.Text = "Exclude All"
        '
        'butExcludeRemove
        '
        Me.butExcludeRemove.Image = CType(resources.GetObject("butExcludeRemove.Image"), System.Drawing.Image)
        Me.butExcludeRemove.Location = New System.Drawing.Point(8, 8)
        Me.butExcludeRemove.Name = "butExcludeRemove"
        Me.butExcludeRemove.Size = New System.Drawing.Size(24, 24)
        Me.butExcludeRemove.TabIndex = 37
        '
        'listExclude
        '
        Me.listExclude.AllowDrop = True
        Me.listExclude.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listExclude.ForeColor = System.Drawing.Color.DarkGreen
        Me.listExclude.Location = New System.Drawing.Point(40, 0)
        Me.listExclude.Name = "listExclude"
        Me.listExclude.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.listExclude.Size = New System.Drawing.Size(368, 82)
        Me.listExclude.Sorted = True
        Me.listExclude.TabIndex = 38
        '
        'PanelSlotTop
        '
        Me.PanelSlotTop.Controls.Add(Me.lblParser)
        Me.PanelSlotTop.Controls.Add(Me.butBrowse)
        Me.PanelSlotTop.Controls.Add(Me.AvailableArchetypesListBox)
        Me.PanelSlotTop.Controls.Add(Me.lblClass)
        Me.PanelSlotTop.Controls.Add(Me.lblSlot)
        Me.PanelSlotTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSlotTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelSlotTop.MinimumSize = New System.Drawing.Size(100, 50)
        Me.PanelSlotTop.Name = "PanelSlotTop"
        Me.PanelSlotTop.Size = New System.Drawing.Size(424, 88)
        Me.PanelSlotTop.TabIndex = 1
        '
        'lblParser
        '
        Me.lblParser.Location = New System.Drawing.Point(390, 49)
        Me.lblParser.Name = "lblParser"
        Me.lblParser.Size = New System.Drawing.Size(38, 19)
        Me.lblParser.TabIndex = 4
        Me.lblParser.Text = "(ADL)"
        '
        'butBrowse
        '
        Me.butBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butBrowse.Location = New System.Drawing.Point(397, 8)
        Me.butBrowse.Name = "butBrowse"
        Me.butBrowse.Size = New System.Drawing.Size(24, 29)
        Me.butBrowse.TabIndex = 3
        Me.butBrowse.Text = "..."
        '
        'AvailableArchetypesListBox
        '
        Me.AvailableArchetypesListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AvailableArchetypesListBox.Location = New System.Drawing.Point(106, 1)
        Me.AvailableArchetypesListBox.Name = "AvailableArchetypesListBox"
        Me.AvailableArchetypesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.AvailableArchetypesListBox.Size = New System.Drawing.Size(283, 82)
        Me.AvailableArchetypesListBox.Sorted = True
        Me.AvailableArchetypesListBox.TabIndex = 2
        '
        'lblClass
        '
        Me.lblClass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClass.Location = New System.Drawing.Point(8, 24)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(94, 56)
        Me.lblClass.TabIndex = 1
        Me.lblClass.Text = "Class name"
        Me.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSlot
        '
        Me.lblSlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlot.Location = New System.Drawing.Point(8, 1)
        Me.lblSlot.Name = "lblSlot"
        Me.lblSlot.Size = New System.Drawing.Size(112, 16)
        Me.lblSlot.TabIndex = 0
        Me.lblSlot.Text = "Slot"
        '
        'PanelExcludeStatements
        '
        Me.PanelExcludeStatements.Controls.Add(Me.listExclude)
        Me.PanelExcludeStatements.Controls.Add(Me.PanelExcludeMinusTick)
        Me.PanelExcludeStatements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelExcludeStatements.Location = New System.Drawing.Point(3, 48)
        Me.PanelExcludeStatements.Name = "PanelExcludeStatements"
        Me.PanelExcludeStatements.Padding = New System.Windows.Forms.Padding(0, 0, 10, 2)
        Me.PanelExcludeStatements.Size = New System.Drawing.Size(418, 93)
        Me.PanelExcludeStatements.TabIndex = 34
        '
        'PanelExcludeMinusTick
        '
        Me.PanelExcludeMinusTick.Controls.Add(Me.butExcludeRemove)
        Me.PanelExcludeMinusTick.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelExcludeMinusTick.Location = New System.Drawing.Point(0, 0)
        Me.PanelExcludeMinusTick.Name = "PanelExcludeMinusTick"
        Me.PanelExcludeMinusTick.Size = New System.Drawing.Size(40, 91)
        Me.PanelExcludeMinusTick.TabIndex = 33
        '
        'PanelIncludeStatements
        '
        Me.PanelIncludeStatements.Controls.Add(Me.listInclude)
        Me.PanelIncludeStatements.Controls.Add(Me.PanelIncludeMinusTick)
        Me.PanelIncludeStatements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelIncludeStatements.Location = New System.Drawing.Point(3, 56)
        Me.PanelIncludeStatements.Name = "PanelIncludeStatements"
        Me.PanelIncludeStatements.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.PanelIncludeStatements.Size = New System.Drawing.Size(418, 101)
        Me.PanelIncludeStatements.TabIndex = 34
        '
        'PanelIncludeMinusTick
        '
        Me.PanelIncludeMinusTick.Controls.Add(Me.butIncludeRemove)
        Me.PanelIncludeMinusTick.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelIncludeMinusTick.Location = New System.Drawing.Point(0, 0)
        Me.PanelIncludeMinusTick.Name = "PanelIncludeMinusTick"
        Me.PanelIncludeMinusTick.Size = New System.Drawing.Size(40, 101)
        Me.PanelIncludeMinusTick.TabIndex = 33
        '
        'gbExclude
        '
        Me.gbExclude.Controls.Add(Me.PanelExcludeStatements)
        Me.gbExclude.Controls.Add(Me.PanelExclude)
        Me.gbExclude.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbExclude.Location = New System.Drawing.Point(0, 264)
        Me.gbExclude.Name = "gbExclude"
        Me.gbExclude.Size = New System.Drawing.Size(424, 144)
        Me.gbExclude.TabIndex = 5
        Me.gbExclude.TabStop = False
        Me.gbExclude.Text = "Exclude"
        '
        'gbInclude
        '
        Me.gbInclude.Controls.Add(Me.PanelIncludeStatements)
        Me.gbInclude.Controls.Add(Me.PanelInlcude)
        Me.gbInclude.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbInclude.Location = New System.Drawing.Point(0, 96)
        Me.gbInclude.Name = "gbInclude"
        Me.gbInclude.Size = New System.Drawing.Size(424, 160)
        Me.gbInclude.TabIndex = 3
        Me.gbInclude.TabStop = False
        Me.gbInclude.Text = "Include"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 88)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(424, 8)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 256)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(424, 8)
        Me.Splitter2.TabIndex = 4
        Me.Splitter2.TabStop = False
        '
        'SlotConstraintControl
        '
        Me.Controls.Add(Me.gbExclude)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.gbInclude)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.PanelSlotTop)
        Me.Name = "SlotConstraintControl"
        Me.Size = New System.Drawing.Size(424, 408)
        Me.PanelInlcude.ResumeLayout(False)
        Me.PanelExclude.ResumeLayout(False)
        Me.PanelSlotTop.ResumeLayout(False)
        Me.PanelExcludeStatements.ResumeLayout(False)
        Me.PanelExcludeMinusTick.ResumeLayout(False)
        Me.PanelIncludeStatements.ResumeLayout(False)
        Me.PanelIncludeMinusTick.ResumeLayout(False)
        Me.gbExclude.ResumeLayout(False)
        Me.gbInclude.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected ReadOnly Property Constraint() As Constraint_Slot
        Get
            Return CType(mConstraint, Constraint_Slot)
        End Get
    End Property

    Protected Overrides Sub SetControlValues(ByVal IsState As Boolean)
        Dim s As String
        lblClass.Text = Filemanager.GetOpenEhrTerm(CInt(Constraint.RM_ClassType), Constraint.RM_ClassType.ToString())
        butShowAll()

        If Constraint.IncludeAll Then
            chkIncludeAll.Checked = True
        End If

        If Constraint.ExcludeAll Then
            chkExcludeAll.Checked = True
        End If

        If Constraint.hasSlots Then
            For Each s In Constraint.Include
                listInclude.Items.Add(s)
            Next

            For Each s In Constraint.Exclude
                listExclude.Items.Add(s)
            Next
        End If
    End Sub

    Private Sub listSlot_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles listExclude.KeyDown, listInclude.KeyDown
        Dim list As Windows.Forms.ListBox
        Dim col As CollectionOfSlots

        If sender Is listInclude Then
            list = listInclude
            col = Constraint.Include
        ElseIf sender Is listExclude Then
            list = listExclude
            col = Constraint.Exclude
        Else
            Debug.Assert(False, "Control not included")
            Return
        End If

        If Not list.SelectedItem Is Nothing Then
            If e.KeyCode = Keys.Delete Then
                If MessageBox.Show(AE_Constants.Instance.Remove & CStr(list.SelectedItem), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                    col.Remove(CStr(list.SelectedItem))
                    list.Items.RemoveAt(list.SelectedIndex)
                    mFileManager.FileEdited = True
                End If
            End If
        End If
    End Sub

    Private Sub butSlotRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExcludeRemove.Click, butIncludeRemove.Click
        Dim list As Windows.Forms.ListBox
        Dim col As CollectionOfSlots

        If sender Is butIncludeRemove Then
            list = listInclude
            col = Constraint.Include
        ElseIf sender Is butExcludeRemove Then
            list = listExclude
            col = Constraint.Exclude
        Else
            Debug.Assert(False, "Control not included")
            Return
        End If

        Dim doRemove As Boolean = False

        Select Case list.SelectedItems.Count
            Case 0
                ' do nothing
            Case 1
                doRemove = (MessageBox.Show(AE_Constants.Instance.Remove & " " & CStr(list.SelectedItems(0)), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK)
            Case Is > 1
                doRemove = (MessageBox.Show(AE_Constants.Instance.Remove & " " & list.SelectedItems.Count.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK)

        End Select

        If doRemove Then
            Dim s(list.SelectedItems.Count - 1) As String
            list.SelectedItems.CopyTo(s, 0)

            For i As Integer = 0 To s.Length - 1
                col.Remove(s(i))
                list.Items.Remove(s(i))
            Next

            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butInclude.Click
        If chkIncludeAll.Checked Then
            chkIncludeAll.Checked = False
        End If

        AddSlot(AvailableArchetypesListBox.SelectedItems, listInclude, listExclude, Constraint.Include, Constraint.Exclude)
    End Sub

    Private Sub butExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExclude.Click
        If chkExcludeAll.Checked Then
            chkExcludeAll.Checked = False
        End If

        AddSlot(AvailableArchetypesListBox.SelectedItems, listExclude, listInclude, Constraint.Exclude, Constraint.Include)
    End Sub

    Private Sub AddSlot(ByVal names As ICollection, ByVal list As Windows.Forms.ListBox, ByVal list2 As Windows.Forms.ListBox, ByVal col As CollectionOfSlots, ByVal col2 As CollectionOfSlots)
        If names.Count > 0 Then
            Dim specialisation As String

            Dim label As New System.Text.StringBuilder()
            For Each s As String In names
                label.AppendLine(s)
            Next
            label.AppendLine()
            label.AppendLine(AE_Constants.Instance.SpecialisationsToo)

            If MessageBox.Show(label.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                specialisation = "(-[a-zA-Z0-9_]+)*"
            Else
                specialisation = ""
            End If

            For Each s As String In names
                'SRH Aug 20 2008 - now have full IDs so this doesn't work
                's = s.Replace(".", specialisation & "\.")
                Dim i As Integer = s.LastIndexOf(".")
                s = String.Format("{0}{1}{2}", s.Substring(0, i), specialisation, s.Substring(i))
                s = s.Replace(".", "\.")

                If Not col.Contains(s) Then
                    col.Add(s)
                    list.Items.Add(s)

                    If list2.Items.Contains(s) Then
                        list2.Items.Remove(s)
                        col2.Remove(s)
                    End If
                End If
            Next

            AvailableArchetypesListBox.ClearSelected()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkIncludeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIncludeAll.CheckedChanged
        PanelIncludeStatements.Visible = Not chkIncludeAll.Checked
        chkExcludeAll.Enabled = Not chkIncludeAll.Checked ' can't have include all and exclude all

        If Not IsLoading Then
            Constraint.IncludeAll = chkIncludeAll.Checked
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkExcludeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcludeAll.CheckedChanged
        PanelExcludeStatements.Visible = Not chkExcludeAll.Checked
        chkIncludeAll.Enabled = Not chkExcludeAll.Checked ' can't have include all and exclude all

        If Not IsLoading Then
            Constraint.ExcludeAll = chkExcludeAll.Checked
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butBrowse.Click
        Dim description As String = ReferenceModel.RM_StructureName(Constraint.RM_ClassType)
        Dim patterns As String = ""

        If ReferenceModel.IsAbstract(Constraint.RM_ClassType) Then
            Dim separator As String = ""
            description &= ": "

            For Each t As StructureType In ReferenceModel.Specialisations(Constraint.RM_ClassType)
                description &= separator & ReferenceModel.RM_StructureName(t)
                patterns &= separator & ReferenceModel.ReferenceModelName & "-" & ReferenceModel.RM_StructureName(t) & ".*." & Filemanager.Master.ParserType
                separator = ";"
            Next
        Else
            patterns = ReferenceModel.ReferenceModelName & "-" & description & ".*." & Filemanager.Master.ParserType
        End If

        Dim fd As New OpenFileDialog
        fd.Filter = description & "|" & patterns
        fd.InitialDirectory = Main.Instance.Options.RepositoryPath

        If fd.ShowDialog(Me) = DialogResult.OK Then
            Dim name As String = IO.Path.GetFileNameWithoutExtension(fd.FileName)

            If Not ReferenceModel.IsAbstract(Constraint.RM_ClassType) Then
                name = name.Substring(name.IndexOf(".") + 1)
            End If

            If Not AvailableArchetypesListBox.Items.Contains(name) Then
                AvailableArchetypesListBox.Items.Add(name)
                AvailableArchetypesListBox.SelectedIndex = AvailableArchetypesListBox.FindStringExact(name)
            End If
        End If
    End Sub

    Private Sub AddFilestoListBox(ByVal directory As System.IO.DirectoryInfo, ByVal pattern As String, ByVal clipFileName As Boolean)
        For Each f As IO.FileInfo In directory.GetFiles(pattern, IO.SearchOption.AllDirectories)
            Dim fileName As String = IO.Path.ChangeExtension(f.Name, Nothing)

            If clipFileName Then
                fileName = fileName.Substring(fileName.IndexOf(".") + 1)
            End If

            'SRH: 19 Jan 2009 - EDT-503 - Show archetypes whether ADL or XML
            If Not AvailableArchetypesListBox.Items.Contains(fileName) Then
                AvailableArchetypesListBox.Items.Insert(0, fileName)
            End If
        Next
    End Sub
    'SRH: 19 Jan 2009 - EDT-503 - Show archetypes whether ADL or XML

    'Private Sub RetrieveFiles(ByVal directory As System.IO.DirectoryInfo, ByVal fileExtension As String)
    Private Sub RetrieveFiles(ByVal directory As System.IO.DirectoryInfo, ByVal fileExtensions As ArrayList)
        Dim s As String

        If ReferenceModel.IsAbstract(Constraint.RM_ClassType) Then
            For Each t As StructureType In ReferenceModel.Specialisations(Constraint.RM_ClassType)
                'SRH: 19 Jan 2009 - EDT-503 - Show archetypes whether ADL or XML
                For Each fileType As String In fileExtensions
                    s = String.Format("{0}-{1}.*.{2}", ReferenceModel.ReferenceModelName, ReferenceModel.RM_StructureName(t), fileType)
                    AddFilestoListBox(directory, s, False)
                Next
            Next
        Else
            'SRH: 19 Jan 2009 - EDT-503 - Show archetypes whether ADL or XML
            For Each fileType As String In fileExtensions
                s = String.Format("{0}-{1}.*.{2}", ReferenceModel.ReferenceModelName, ReferenceModel.RM_StructureName(Constraint.RM_ClassType), fileType)
                AddFilestoListBox(directory, s, True)
            Next
        End If
    End Sub

    Private Sub butShowAll()
        Dim errorMessage As String = Nothing

        Cursor = Cursors.WaitCursor
        Dim d As New System.IO.DirectoryInfo(Main.Instance.Options.RepositoryPath)

        If d.Exists Then
            Try
                'SRH: 19 Jan 2009 - EDT-503 - Show archetypes whether ADL or XML

                'RetrieveFiles(d, Filemanager.Master.ParserType)
                RetrieveFiles(d, Filemanager.Master.AvailableFormats)
            Catch ex As Exception
                errorMessage = AE_Constants.Instance.ErrorLoading & " '" & Main.Instance.Options.RepositoryPath & "':" & Environment.NewLine & ex.Message
            End Try
        Else
            errorMessage = AE_Constants.Instance.ErrorLoading & " '" & Main.Instance.Options.RepositoryPath & "'."
        End If

        Cursor = Cursors.Default

        If errorMessage IsNot Nothing Then
            MessageBox.Show(errorMessage, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub PanelStatements_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelExcludeStatements.VisibleChanged, PanelIncludeStatements.VisibleChanged
        If PanelExcludeStatements.Visible And PanelIncludeStatements.Visible Then
            gbInclude.Height = CInt((Height - PanelSlotTop.Height - 16) / 2)
        Else
            If Not PanelIncludeStatements.Visible Then
                gbInclude.Height = PanelInlcude.Height + 10
            Else
                '     Exclude is not visible
                gbInclude.Height = Height - PanelSlotTop.Height - PanelExclude.Height - 30
            End If
        End If
    End Sub

    Private Sub SlotConstraintControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If PanelIncludeStatements.Visible Then
            If PanelExcludeStatements.Visible Then
                gbInclude.Height = CInt((Height - PanelSlotTop.Height - 16) / 2)
            Else
                gbInclude.Height = Height - PanelSlotTop.Height - PanelExclude.Height - 30
            End If
        End If
    End Sub

#Region "Drag and drop"

    Private Sub listAvailbleArchetypes_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AvailableArchetypesListBox.MouseDown
        Dim indexOfItem As Integer = AvailableArchetypesListBox.IndexFromPoint(e.X, e.Y)
        If (indexOfItem >= 0 AndAlso indexOfItem < AvailableArchetypesListBox.Items.Count) Then
            AvailableArchetypesListBox.DoDragDrop(AvailableArchetypesListBox.Items(indexOfItem), DragDropEffects.Copy)
        End If
    End Sub

    Private Sub listInclude_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles listInclude.DragEnter
        If e.Data.GetDataPresent(DataFormats.StringFormat) AndAlso e.AllowedEffect = DragDropEffects.Copy Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub listExclude_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles listExclude.DragEnter
        If e.Data.GetDataPresent(DataFormats.StringFormat) AndAlso e.AllowedEffect = DragDropEffects.Copy Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub listInclude_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles listInclude.DragDrop
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            AddSlot(New String() {CStr(e.Data.GetData(DataFormats.StringFormat))}, listInclude, listExclude, Constraint.Include, Constraint.Exclude)
        End If
    End Sub

    Private Sub listExclude_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles listExclude.DragDrop
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            AddSlot(New String() {CStr(e.Data.GetData(DataFormats.StringFormat))}, listExclude, listInclude, Constraint.Exclude, Constraint.Include)
        End If
    End Sub

    Private Sub listAvailbleArchetypes_DragLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AvailableArchetypesListBox.DragLeave
        AvailableArchetypesListBox.SelectedIndices.Remove(AvailableArchetypesListBox.SelectedIndex)
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
'The Original Code is SlotConstraintControl.vb.
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
