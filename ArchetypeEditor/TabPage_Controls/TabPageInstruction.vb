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

Option Explicit On 

Public Class TabPageInstruction
    Inherits System.Windows.Forms.UserControl

    Private mIsloading As Boolean
    Private mPathwaySpecification As PathwaySpecification
    Private mActionSpecification As TabPageStructure
    Private mAllowDeltas As New CodePhrase
    Private mAllowedValuesDataView As New DataView(Filemanager.Instance.OntologyManager.TermDefinitionTable)

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal aEditor As Designer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        'Try
        InitializeComponent()
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabControlInstruction As Crownwood.Magic.Controls.TabControl
    Friend WithEvents tpPathway As Crownwood.Magic.Controls.TabPage
    Friend WithEvents PanelBaseTop As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuPathway As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuAdd As System.Windows.Forms.MenuItem
    Friend WithEvents tpConditions As Crownwood.Magic.Controls.TabPage
    Friend WithEvents tpAction As Crownwood.Magic.Controls.TabPage
    Friend WithEvents PanelTiming As System.Windows.Forms.Panel
    Friend WithEvents gbTiming As System.Windows.Forms.GroupBox
    Friend WithEvents gbConditions As System.Windows.Forms.GroupBox
    Friend WithEvents checkListTiming As System.Windows.Forms.CheckedListBox
    Friend WithEvents checkListConditions As System.Windows.Forms.CheckedListBox
    Friend WithEvents HelpProviderInstruction As System.Windows.Forms.HelpProvider
    Friend WithEvents tpDelta As Crownwood.Magic.Controls.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButAddElement As System.Windows.Forms.Button
    Friend WithEvents listAllowedDeltas As System.Windows.Forms.ListBox
    Friend WithEvents butRemoveElement As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabPageInstruction))
        Me.TabControlInstruction = New Crownwood.Magic.Controls.TabControl
        Me.tpDelta = New Crownwood.Magic.Controls.TabPage
        Me.butRemoveElement = New System.Windows.Forms.Button
        Me.ButAddElement = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.listAllowedDeltas = New System.Windows.Forms.ListBox
        Me.tpAction = New Crownwood.Magic.Controls.TabPage
        Me.tpPathway = New Crownwood.Magic.Controls.TabPage
        Me.tpConditions = New Crownwood.Magic.Controls.TabPage
        Me.PanelTiming = New System.Windows.Forms.Panel
        Me.gbConditions = New System.Windows.Forms.GroupBox
        Me.checkListConditions = New System.Windows.Forms.CheckedListBox
        Me.gbTiming = New System.Windows.Forms.GroupBox
        Me.checkListTiming = New System.Windows.Forms.CheckedListBox
        Me.ContextMenuPathway = New System.Windows.Forms.ContextMenu
        Me.MenuAdd = New System.Windows.Forms.MenuItem
        Me.PanelBaseTop = New System.Windows.Forms.Panel
        Me.HelpProviderInstruction = New System.Windows.Forms.HelpProvider
        Me.tpDelta.SuspendLayout()
        Me.tpConditions.SuspendLayout()
        Me.PanelTiming.SuspendLayout()
        Me.gbConditions.SuspendLayout()
        Me.gbTiming.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlInstruction
        '
        Me.TabControlInstruction.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabControlInstruction.BoldSelectedPage = True
        Me.TabControlInstruction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderInstruction.SetHelpKeyword(Me.TabControlInstruction, "Screens/pathway_screen.html")
        Me.HelpProviderInstruction.SetHelpNavigator(Me.TabControlInstruction, System.Windows.Forms.HelpNavigator.Topic)
        Me.TabControlInstruction.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabControlInstruction.Location = New System.Drawing.Point(0, 24)
        Me.TabControlInstruction.Name = "TabControlInstruction"
        Me.TabControlInstruction.PositionTop = True
        Me.TabControlInstruction.SelectedIndex = 0
        Me.TabControlInstruction.SelectedTab = Me.tpAction
        Me.HelpProviderInstruction.SetShowHelp(Me.TabControlInstruction, True)
        Me.TabControlInstruction.Size = New System.Drawing.Size(848, 400)
        Me.TabControlInstruction.TabIndex = 0
        Me.TabControlInstruction.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpAction, Me.tpPathway, Me.tpConditions, Me.tpDelta})
        Me.TabControlInstruction.TextInactiveColor = System.Drawing.Color.Black
        '
        'tpDelta
        '
        Me.tpDelta.Controls.Add(Me.butRemoveElement)
        Me.tpDelta.Controls.Add(Me.ButAddElement)
        Me.tpDelta.Controls.Add(Me.Label1)
        Me.tpDelta.Controls.Add(Me.listAllowedDeltas)
        Me.tpDelta.Location = New System.Drawing.Point(0, 0)
        Me.tpDelta.Name = "tpDelta"
        Me.tpDelta.Selected = False
        Me.tpDelta.Size = New System.Drawing.Size(848, 374)
        Me.tpDelta.TabIndex = 3
        Me.tpDelta.Title = "Allowable changes"
        '
        'butRemoveElement
        '
        Me.butRemoveElement.BackColor = System.Drawing.SystemColors.Control
        Me.butRemoveElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butRemoveElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butRemoveElement.Image = CType(resources.GetObject("butRemoveElement.Image"), System.Drawing.Image)
        Me.butRemoveElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveElement.Location = New System.Drawing.Point(200, 72)
        Me.butRemoveElement.Name = "butRemoveElement"
        Me.butRemoveElement.Size = New System.Drawing.Size(24, 25)
        Me.butRemoveElement.TabIndex = 34
        '
        'ButAddElement
        '
        Me.ButAddElement.BackColor = System.Drawing.SystemColors.Control
        Me.ButAddElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButAddElement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButAddElement.Image = CType(resources.GetObject("ButAddElement.Image"), System.Drawing.Image)
        Me.ButAddElement.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.ButAddElement.Location = New System.Drawing.Point(200, 40)
        Me.ButAddElement.Name = "ButAddElement"
        Me.ButAddElement.Size = New System.Drawing.Size(24, 25)
        Me.ButAddElement.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(112, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Allowable changes"
        '
        'listAllowedDeltas
        '
        Me.listAllowedDeltas.ItemHeight = 17
        Me.listAllowedDeltas.Location = New System.Drawing.Point(232, 40)
        Me.listAllowedDeltas.Name = "listAllowedDeltas"
        Me.listAllowedDeltas.Size = New System.Drawing.Size(264, 157)
        Me.listAllowedDeltas.TabIndex = 0
        '
        'tpAction
        '
        Me.HelpProviderInstruction.SetHelpKeyword(Me.tpAction, "Screens/action_screen.html")
        Me.HelpProviderInstruction.SetHelpNavigator(Me.tpAction, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpAction.Location = New System.Drawing.Point(0, 0)
        Me.tpAction.Name = "tpAction"
        Me.HelpProviderInstruction.SetShowHelp(Me.tpAction, True)
        Me.tpAction.Size = New System.Drawing.Size(848, 374)
        Me.tpAction.TabIndex = 2
        Me.tpAction.Title = "Action"
        '
        'tpPathway
        '
        Me.tpPathway.Location = New System.Drawing.Point(0, 0)
        Me.tpPathway.Name = "tpPathway"
        Me.tpPathway.Selected = False
        Me.tpPathway.Size = New System.Drawing.Size(848, 374)
        Me.tpPathway.TabIndex = 0
        Me.tpPathway.Title = "Pathway"
        '
        'tpConditions
        '
        Me.tpConditions.Controls.Add(Me.PanelTiming)
        Me.tpConditions.Location = New System.Drawing.Point(0, 0)
        Me.tpConditions.Name = "tpConditions"
        Me.tpConditions.Selected = False
        Me.tpConditions.Size = New System.Drawing.Size(848, 374)
        Me.tpConditions.TabIndex = 1
        Me.tpConditions.Title = "Conditions"
        '
        'PanelTiming
        '
        Me.PanelTiming.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.PanelTiming.Controls.Add(Me.gbConditions)
        Me.PanelTiming.Controls.Add(Me.gbTiming)
        Me.PanelTiming.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTiming.Location = New System.Drawing.Point(0, 0)
        Me.PanelTiming.Name = "PanelTiming"
        Me.PanelTiming.Size = New System.Drawing.Size(848, 374)
        Me.PanelTiming.TabIndex = 0
        '
        'gbConditions
        '
        Me.gbConditions.Controls.Add(Me.checkListConditions)
        Me.gbConditions.Location = New System.Drawing.Point(312, 24)
        Me.gbConditions.Name = "gbConditions"
        Me.gbConditions.Size = New System.Drawing.Size(264, 272)
        Me.gbConditions.TabIndex = 1
        Me.gbConditions.TabStop = False
        Me.gbConditions.Text = "Conditions"
        '
        'checkListConditions
        '
        Me.checkListConditions.Items.AddRange(New Object() {"Active conditions", "Complete conditions", "Suspend conditions", "Abort conditions", "Active triggers", "Compete triggers", "Suspend triggers", "Abort triggers", "Notify user triggers", "Notify system triggers"})
        Me.checkListConditions.Location = New System.Drawing.Point(16, 24)
        Me.checkListConditions.Name = "checkListConditions"
        Me.checkListConditions.Size = New System.Drawing.Size(240, 213)
        Me.checkListConditions.TabIndex = 0
        '
        'gbTiming
        '
        Me.gbTiming.Controls.Add(Me.checkListTiming)
        Me.gbTiming.Location = New System.Drawing.Point(24, 24)
        Me.gbTiming.Name = "gbTiming"
        Me.gbTiming.Size = New System.Drawing.Size(264, 272)
        Me.gbTiming.TabIndex = 0
        Me.gbTiming.TabStop = False
        Me.gbTiming.Text = "Timing"
        '
        'checkListTiming
        '
        Me.checkListTiming.Items.AddRange(New Object() {"Start date/time", "Finish date/time", "Action duration", "Earliest start date/time", "Earliest completed date/time", "Latest start date/time", "Latest completed date/time", "Allow instruction repeats", "Number of repeats"})
        Me.checkListTiming.Location = New System.Drawing.Point(16, 24)
        Me.checkListTiming.Name = "checkListTiming"
        Me.checkListTiming.Size = New System.Drawing.Size(240, 213)
        Me.checkListTiming.TabIndex = 0
        '
        'ContextMenuPathway
        '
        Me.ContextMenuPathway.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuAdd})
        '
        'MenuAdd
        '
        Me.MenuAdd.Index = 0
        Me.MenuAdd.Text = "Add"
        '
        'PanelBaseTop
        '
        Me.PanelBaseTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelBaseTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelBaseTop.Name = "PanelBaseTop"
        Me.PanelBaseTop.Size = New System.Drawing.Size(848, 24)
        Me.PanelBaseTop.TabIndex = 1
        '
        'TabPageInstruction
        '
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.TabControlInstruction)
        Me.Controls.Add(Me.PanelBaseTop)
        Me.Name = "TabPageInstruction"
        Me.Size = New System.Drawing.Size(848, 424)
        Me.tpDelta.ResumeLayout(False)
        Me.tpConditions.ResumeLayout(False)
        Me.PanelTiming.ResumeLayout(False)
        Me.gbConditions.ResumeLayout(False)
        Me.gbTiming.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TabPageInstruction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mIsLoading = True
        If mPathwaySpecification Is Nothing Then
            Me.tpPathway.Controls.Clear()
            mPathwaySpecification = New PathwaySpecification
            Me.tpPathway.Controls.Add(mPathwaySpecification)
            mPathwaySpecification.Dock = DockStyle.Fill
        End If
        If mActionSpecification Is Nothing Then
            Me.tpAction.Controls.Clear()
            mActionSpecification = New TabPageStructure
            Me.tpAction.Controls.Add(mActionSpecification)
            mActionSpecification.Dock = DockStyle.Fill
        End If

        Me.HelpProviderInstruction.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
        mIsLoading = False

    End Sub


    Public ReadOnly Property ComponentType() As StructureType
        Get
            Return StructureType.INSTRUCTION
        End Get
    End Property

    Public Function toRichText(ByRef text As IO.StringWriter, ByVal level As Integer) As String
        text.WriteLine("\par Action specification: \par")
        text.WriteLine("\par")
        mActionSpecification.toRichText(text, level + 1)
    End Function

    Public Sub Reset()
        Me.tpPathway.Controls.Clear()
        mPathwaySpecification = New PathwaySpecification
        Me.tpPathway.Controls.Add(mPathwaySpecification)
        mPathwaySpecification.Dock = DockStyle.Fill
    End Sub

    Public Sub ProcessInstruction(ByVal Struct As Children)

        For Each rm As RmStructureCompound In Struct
            Select Case rm.Type
                Case StructureType.InstructionActExection
                    Me.tpPathway.Controls.Clear()
                    mPathwaySpecification = New PathwaySpecification
                    Me.tpPathway.Controls.Add(mPathwaySpecification)
                    mPathwaySpecification.Dock = DockStyle.Fill
                    mPathwaySpecification.PathwaySteps = rm
                Case StructureType.Action
                    Dim an_action As RmStructure = rm.Children.items(0)
                    Me.tpAction.Controls.Clear()
                    mActionSpecification = New TabPageStructure
                    If an_action.Type = StructureType.Slot Then
                        mActionSpecification.ProcessStructure(CType(an_action, RmSlot))
                    Else
                        Debug.Assert(an_action.Type = StructureType.Single Or an_action.Type = StructureType.List Or an_action.Type = StructureType.Tree Or an_action.Type = StructureType.Table)
                        mActionSpecification.ProcessStructure(CType(an_action, RmStructureCompound))
                    End If
                    Me.tpAction.Controls.Add(mActionSpecification)
                    mActionSpecification.Dock = DockStyle.Fill
                Case Else
                    Debug.Assert(False, "Not handled yet")
            End Select
        Next
    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point)
        Dim spacer As Integer = 1

        'leftmargin = pos.X
        If aContainer.Name <> "tpInterface" Then
            aContainer.Size = New Size
        End If

        If Not mActionSpecification Is Nothing Then
            mActionSpecification.BuildInterface(aContainer, pos)
        End If

    End Sub

    Public Function SaveAsInstruction() As RmStructureCompound
        Dim rm As New RmStructureCompound("Instruction", StructureType.INSTRUCTION)
        If Not mPathwaySpecification Is Nothing Then
            rm.Children.Add(mPathwaySpecification.PathwaySteps)
        End If
        If Not mActionSpecification Is Nothing Then
            Dim action_spec As New RmStructureCompound("activity", StructureType.Action)
            action_spec.Children.Add(mActionSpecification.SaveAsStructure)
            rm.Children.Add(action_spec)
        End If

        Return rm
    End Function

    Private Sub ButAddElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddElement.Click
        'Dim ae() As ArchetypeElement

        'ae = OceanArchetypeEditor.Instance.ChooseInternal(Me.mActionSpecification.Elements, Me.listAllowedDeltas.Items)
        'If Not ae Is Nothing Then
        '    listAllowedDeltas.Items.AddRange(ae)
        'End If

    End Sub

    Private Sub butAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAddElement.Click

        If mIsloading Then Return

        Dim ae() As ArchetypeElement = OceanArchetypeEditor.Instance.ChooseInternal(Me.mActionSpecification.Elements, mAllowDeltas)

        If ae Is Nothing Then Return

        For i As Integer = 0 To ae.Length - 1
            mAllowDeltas.Codes.Add(ae(i).NodeId)
        Next

        SetAllowableValuesFilter(mAllowDeltas)
        Me.listAllowedDeltas.DataSource = mAllowedValuesDataView
        Me.listAllowedDeltas.DisplayMember = "Text"
        Me.listAllowedDeltas.ValueMember = "Code"

        Filemanager.Instance.FileEdited = True

    End Sub

    Private Sub SetAllowableValuesFilter(ByVal a_codephrase As CodePhrase)
        Dim i As Integer
        Dim cd As String
        cd = "Code = "
        For i = 0 To a_codephrase.Codes.Count - 1
            cd = cd & "'" & a_codephrase.Codes.Item(i) & "'"
            If i < (a_codephrase.Codes.Count - 1) Then
                cd = cd & " OR Code = "
            End If
        Next

        mAllowedValuesDataView.RowFilter = String.Format("({0}) AND (id = '{1}')", _
                cd, Filemanager.Instance.OntologyManager.LanguageCode)
    End Sub

    Private Sub butRemoveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveElement.Click
        Dim i As Integer = Me.listAllowedDeltas.SelectedIndex

        If i > -1 Then
            RemoveAllowableValue(i)
        End If

        Filemanager.Instance.FileEdited = True
    End Sub

    Private Sub RemoveAllowableValue(ByVal Index As Integer)

        Try

            If mAllowedValuesDataView.Count > 0 Then

                If MessageBox.Show(AE_Constants.Instance.Remove & _
                        CStr(Me.listAllowedDeltas.Text), _
                        AE_Constants.Instance.MessageBoxCaption, _
                        MessageBoxButtons.OKCancel) = DialogResult.OK Then

                    Dim code As String = CStr(Me.listAllowedDeltas.SelectedValue) '("Code")
                    mAllowDeltas.Codes.Remove(code)

                    If mAllowDeltas.Codes.Count = 0 Then
                        Me.listAllowedDeltas.DataSource = Nothing
                        Me.listAllowedDeltas.Items.Clear()

                    Else
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
                        SetAllowableValuesFilter(mAllowDeltas)
                    End If
                End If
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

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
'The Original Code is TabPageInstruction.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
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

