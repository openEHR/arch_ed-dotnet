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

        If OceanArchetypeEditor.Instance.DefaultLanguageCode <> "en" Then
            Me.lblSlot.Text = Filemanager.GetOpenEhrTerm(312, Me.lblSlot.Text)
            Me.gbInclude.Text = Filemanager.GetOpenEhrTerm(625, Me.gbInclude.Text)
            Me.gbExclude.Text = Filemanager.GetOpenEhrTerm(626, Me.gbExclude.Text)
            Me.butBrowse.Text = Filemanager.GetOpenEhrTerm(627, Me.butBrowse.Text)
            Me.chkIncludeAll.Text = Filemanager.GetOpenEhrTerm(628, Me.chkIncludeAll.Text)
            Me.chkExcludeAll.Text = Filemanager.GetOpenEhrTerm(628, Me.chkExcludeAll.Text)
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
    Friend WithEvents butBrowse As System.Windows.Forms.Button
    Friend WithEvents listAvailbleArchetypes As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SlotConstraintControl))
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
        Me.butBrowse = New System.Windows.Forms.Button
        Me.listAvailbleArchetypes = New System.Windows.Forms.ListBox
        Me.lblClass = New System.Windows.Forms.Label
        Me.lblSlot = New System.Windows.Forms.Label
        Me.PanelExcludeStatements = New System.Windows.Forms.Panel
        Me.PanelExcludeMinusTick = New System.Windows.Forms.Panel
        Me.PanelIncludeStatements = New System.Windows.Forms.Panel
        Me.PanelIncludeMinusTick = New System.Windows.Forms.Panel
        Me.gbExclude = New System.Windows.Forms.GroupBox
        Me.gbInclude = New System.Windows.Forms.GroupBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
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
        Me.chkIncludeAll.Location = New System.Drawing.Point(48, 1)
        Me.chkIncludeAll.Name = "chkIncludeAll"
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
        Me.listInclude.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listInclude.ForeColor = System.Drawing.Color.DarkGreen
        Me.listInclude.ItemHeight = 16
        Me.listInclude.Location = New System.Drawing.Point(40, 0)
        Me.listInclude.Name = "listInclude"
        Me.listInclude.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.listInclude.Size = New System.Drawing.Size(368, 84)
        Me.listInclude.Sorted = True
        Me.listInclude.TabIndex = 27
        '
        'PanelExclude
        '
        Me.PanelExclude.Controls.Add(Me.butExclude)
        Me.PanelExclude.Controls.Add(Me.chkExcludeAll)
        Me.PanelExclude.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelExclude.Location = New System.Drawing.Point(3, 18)
        Me.PanelExclude.Name = "PanelExclude"
        Me.PanelExclude.Size = New System.Drawing.Size(418, 32)
        Me.PanelExclude.TabIndex = 39
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
        Me.chkExcludeAll.Location = New System.Drawing.Point(48, 0)
        Me.chkExcludeAll.Name = "chkExcludeAll"
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
        Me.listExclude.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listExclude.ForeColor = System.Drawing.Color.DarkGreen
        Me.listExclude.ItemHeight = 16
        Me.listExclude.Location = New System.Drawing.Point(40, 0)
        Me.listExclude.Name = "listExclude"
        Me.listExclude.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.listExclude.Size = New System.Drawing.Size(368, 84)
        Me.listExclude.Sorted = True
        Me.listExclude.TabIndex = 33
        '
        'PanelSlotTop
        '
        Me.PanelSlotTop.Controls.Add(Me.butBrowse)
        Me.PanelSlotTop.Controls.Add(Me.listAvailbleArchetypes)
        Me.PanelSlotTop.Controls.Add(Me.lblClass)
        Me.PanelSlotTop.Controls.Add(Me.lblSlot)
        Me.PanelSlotTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSlotTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelSlotTop.Name = "PanelSlotTop"
        Me.PanelSlotTop.Size = New System.Drawing.Size(424, 88)
        Me.PanelSlotTop.TabIndex = 1
        '
        'butBrowse
        '
        Me.butBrowse.Location = New System.Drawing.Point(357, 8)
        Me.butBrowse.Name = "butBrowse"
        Me.butBrowse.Size = New System.Drawing.Size(64, 40)
        Me.butBrowse.TabIndex = 3
        Me.butBrowse.Text = "Browse"
        '
        'listAvailbleArchetypes
        '
        Me.listAvailbleArchetypes.ItemHeight = 16
        Me.listAvailbleArchetypes.Location = New System.Drawing.Point(96, 0)
        Me.listAvailbleArchetypes.Name = "listAvailbleArchetypes"
        Me.listAvailbleArchetypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.listAvailbleArchetypes.Size = New System.Drawing.Size(256, 84)
        Me.listAvailbleArchetypes.Sorted = True
        Me.listAvailbleArchetypes.TabIndex = 2
        '
        'lblClass
        '
        Me.lblClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClass.Location = New System.Drawing.Point(8, 24)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(80, 56)
        Me.lblClass.TabIndex = 1
        Me.lblClass.Text = "Class name"
        Me.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSlot
        '
        Me.lblSlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlot.Location = New System.Drawing.Point(8, 1)
        Me.lblSlot.Name = "lblSlot"
        Me.lblSlot.Size = New System.Drawing.Size(32, 16)
        Me.lblSlot.TabIndex = 0
        Me.lblSlot.Text = "Slot"
        '
        'PanelExcludeStatements
        '
        Me.PanelExcludeStatements.Controls.Add(Me.listExclude)
        Me.PanelExcludeStatements.Controls.Add(Me.PanelExcludeMinusTick)
        Me.PanelExcludeStatements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelExcludeStatements.DockPadding.Bottom = 2
        Me.PanelExcludeStatements.DockPadding.Right = 10
        Me.PanelExcludeStatements.Location = New System.Drawing.Point(3, 50)
        Me.PanelExcludeStatements.Name = "PanelExcludeStatements"
        Me.PanelExcludeStatements.Size = New System.Drawing.Size(418, 99)
        Me.PanelExcludeStatements.TabIndex = 38
        '
        'PanelExcludeMinusTick
        '
        Me.PanelExcludeMinusTick.Controls.Add(Me.butExcludeRemove)
        Me.PanelExcludeMinusTick.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelExcludeMinusTick.Location = New System.Drawing.Point(0, 0)
        Me.PanelExcludeMinusTick.Name = "PanelExcludeMinusTick"
        Me.PanelExcludeMinusTick.Size = New System.Drawing.Size(40, 97)
        Me.PanelExcludeMinusTick.TabIndex = 34
        '
        'PanelIncludeStatements
        '
        Me.PanelIncludeStatements.Controls.Add(Me.listInclude)
        Me.PanelIncludeStatements.Controls.Add(Me.PanelIncludeMinusTick)
        Me.PanelIncludeStatements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelIncludeStatements.DockPadding.Right = 10
        Me.PanelIncludeStatements.Location = New System.Drawing.Point(3, 56)
        Me.PanelIncludeStatements.Name = "PanelIncludeStatements"
        Me.PanelIncludeStatements.Size = New System.Drawing.Size(418, 101)
        Me.PanelIncludeStatements.TabIndex = 40
        '
        'PanelIncludeMinusTick
        '
        Me.PanelIncludeMinusTick.Controls.Add(Me.butIncludeRemove)
        Me.PanelIncludeMinusTick.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelIncludeMinusTick.Location = New System.Drawing.Point(0, 0)
        Me.PanelIncludeMinusTick.Name = "PanelIncludeMinusTick"
        Me.PanelIncludeMinusTick.Size = New System.Drawing.Size(40, 101)
        Me.PanelIncludeMinusTick.TabIndex = 28
        '
        'gbExclude
        '
        Me.gbExclude.Controls.Add(Me.PanelExcludeStatements)
        Me.gbExclude.Controls.Add(Me.PanelExclude)
        Me.gbExclude.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbExclude.Location = New System.Drawing.Point(0, 256)
        Me.gbExclude.Name = "gbExclude"
        Me.gbExclude.Size = New System.Drawing.Size(424, 152)
        Me.gbExclude.TabIndex = 41
        Me.gbExclude.TabStop = False
        Me.gbExclude.Text = "Exclude"
        '
        'gbInclude
        '
        Me.gbInclude.Controls.Add(Me.PanelIncludeStatements)
        Me.gbInclude.Controls.Add(Me.PanelInlcude)
        Me.gbInclude.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbInclude.Location = New System.Drawing.Point(0, 88)
        Me.gbInclude.Name = "gbInclude"
        Me.gbInclude.Size = New System.Drawing.Size(424, 160)
        Me.gbInclude.TabIndex = 42
        Me.gbInclude.TabStop = False
        Me.gbInclude.Text = "Include"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 248)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(424, 8)
        Me.Splitter1.TabIndex = 43
        Me.Splitter1.TabStop = False
        '
        'SlotConstraintControl
        '
        Me.Controls.Add(Me.gbExclude)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.gbInclude)
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

    Private Shadows ReadOnly Property Constraint() As Constraint_Slot
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Slot)
            Return CType(MyBase.Constraint, Constraint_Slot)
        End Get
    End Property


    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)
        Dim s As String

        Me.lblClass.Text = Filemanager.GetOpenEhrTerm(CInt(Me.Constraint.RM_ClassType), Me.Constraint.RM_ClassType.ToString())

        Me.butShowAll()

        If Me.Constraint.IncludeAll Then
            Me.chkIncludeAll.Checked = True
        End If

        If Me.Constraint.ExcludeAll Then
            Me.chkExcludeAll.Checked = True
        End If

        If Me.Constraint.hasSlots Then
            For Each s In Me.Constraint.Include
                Me.listInclude.Items.Add(s)
            Next

            For Each s In Me.Constraint.Exclude
                Me.listExclude.Items.Add(s)
            Next

        End If

    End Sub


    Private Sub listSlot_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles listExclude.KeyDown, listInclude.KeyDown
        Dim list As Windows.Forms.ListBox
        Dim col As CollectionOfSlots

        If sender Is Me.listInclude Then
            list = Me.listInclude
            col = Me.Constraint.Include
        ElseIf sender Is Me.listExclude Then
            list = Me.listExclude
            col = Me.Constraint.Exclude
        End If

        If Not list.SelectedItem Is Nothing Then
            If e.KeyCode = Keys.Delete Then
                If MessageBox.Show(AE_Constants.Instance.Remove & CStr(list.SelectedItem), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
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

        If sender Is Me.butIncludeRemove Then
            list = Me.listInclude
            col = Me.Constraint.Include
        ElseIf sender Is Me.butExcludeRemove Then
            list = Me.listExclude
            col = Me.Constraint.Exclude
        End If

        Dim do_remove As Boolean = False

        Select Case list.SelectedItems.Count
            Case 0
                ' do nothing
            Case 1
                do_remove = (MessageBox.Show(AE_Constants.Instance.Remove & " " & CStr(list.SelectedItems(0)), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK)
            Case 2
                do_remove = (MessageBox.Show(AE_Constants.Instance.Remove & " " & list.SelectedItems.Count.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK)

        End Select

        If do_remove Then
            Dim s(list.SelectedItems.Count - 1) As String
            list.SelectedItems.CopyTo(s, 0)
            For i As Integer = 0 To s.Length - 1
                col.Remove(s(i))
                list.Items.Remove(s(i))
            Next
            mFileManager.FileEdited = True
        End If

    End Sub

    Private Sub butSlotAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExclude.Click, butInclude.Click
        Dim list As Windows.Forms.ListBox
        Dim col As CollectionOfSlots

        If sender Is Me.butInclude Then
            list = Me.listInclude
            col = Me.Constraint.Include
        ElseIf sender Is Me.butExclude Then
            list = Me.listExclude
            col = Me.Constraint.Exclude
        End If

        If Me.listAvailbleArchetypes.SelectedItems.Count > 0 Then
            For Each s As String In Me.listAvailbleArchetypes.SelectedItems
                If Not col.Contains(s) Then
                    col.Add(s)
                    list.Items.Add(s)
                End If
            Next
            Me.listAvailbleArchetypes.ClearSelected()
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkIncludeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIncludeAll.CheckedChanged
        Me.PanelIncludeStatements.Visible = Not chkIncludeAll.Checked
        Me.chkExcludeAll.Enabled = Not chkIncludeAll.Checked ' can't have include all and exclude all
        If Not Me.IsLoading Then
            Me.Constraint.IncludeAll = chkIncludeAll.Checked
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub chkExcludeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcludeAll.CheckedChanged

        Me.PanelExcludeStatements.Visible = Not chkExcludeAll.Checked
        Me.chkIncludeAll.Enabled = Not chkExcludeAll.Checked ' can't have include all and exclude all
        If Not Me.IsLoading Then
            Me.Constraint.ExcludeAll = chkExcludeAll.Checked
            mFileManager.FileEdited = True
        End If

    End Sub

    Private Sub butBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butBrowse.Click
        Dim fd As New OpenFileDialog
        Dim s As String

        s = ReferenceModel.Instance.ReferenceModelName & "-" & ReferenceModel.Instance.RM_StructureName(Me.Constraint.RM_ClassType)
        fd.Filter = s & "|" & s & ".*.adl"
        fd.InitialDirectory = OceanArchetypeEditor.Instance.Options.RepositoryPath

        If fd.ShowDialog(Me) = DialogResult.OK Then
            Dim ss As String

            ss = fd.FileName.Substring(fd.FileName.LastIndexOf("\") + s.Length + 2)
            Me.listAvailbleArchetypes.Items.Insert(0, (ss.Substring(0, ss.LastIndexOf("."))))
            Me.listAvailbleArchetypes.SelectedIndex = 0
        End If

    End Sub

    Private Sub RetrieveFiles(ByVal a_directory As System.IO.DirectoryInfo)
        Dim f As System.IO.FileInfo
        Dim s As String
        Dim d As System.IO.DirectoryInfo

        s = ReferenceModel.Instance.ReferenceModelName & "-" & Me.lblClass.Text
        For Each f In a_directory.GetFiles(s & ".*.adl")
            Me.listAvailbleArchetypes.Items.Insert(0, f.Name.Substring(s.Length + 1, f.Name.LastIndexOf(".") - (s.Length + 1)))
        Next
        For Each d In a_directory.GetDirectories
            RetrieveFiles(d)
        Next

    End Sub
    Private Sub butShowAll()
        Dim d As System.IO.DirectoryInfo
        Me.Cursor = Cursors.WaitCursor

        d = New System.IO.DirectoryInfo(OceanArchetypeEditor.Instance.Options.RepositoryPath)
        If d.Exists Then
            RetrieveFiles(d)
            Me.Cursor = Cursors.Default
        Else
            Me.Cursor = Cursors.Default
            MessageBox.Show(AE_Constants.Instance.Error_loading & " " & OceanArchetypeEditor.Instance.Options.RepositoryPath, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    Private Sub PanelStatements_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelExcludeStatements.VisibleChanged, PanelIncludeStatements.VisibleChanged
        If PanelExcludeStatements.Visible And PanelIncludeStatements.Visible Then
            Me.gbInclude.Height = CInt((Me.Height - Me.PanelSlotTop.Height - 16) / 2)
        Else
            If Not PanelIncludeStatements.Visible Then
                Me.gbInclude.Height = Me.PanelInlcude.Height + 10
            Else
                '     Exclude is not visible
                Me.gbInclude.Height = Me.Height - Me.PanelSlotTop.Height - PanelExclude.Height - 30
            End If
        End If
    End Sub

    Private Sub SlotConstraintControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If PanelIncludeStatements.Visible Then
            If PanelExcludeStatements.Visible Then
                Me.gbInclude.Height = CInt((Me.Height - Me.PanelSlotTop.Height - 16) / 2)
            Else
                Me.gbInclude.Height = Me.Height - Me.PanelSlotTop.Height - PanelExclude.Height - 30
            End If
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
