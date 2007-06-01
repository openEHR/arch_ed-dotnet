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

Public Class TabPageInstruction
    Inherits System.Windows.Forms.UserControl

    Private mIsloading As Boolean
    Private mActionSpecification As TabPageStructure
    Private tpNewActivity As New Crownwood.Magic.Controls.TabPage
    Friend WithEvents mOccurrences As OccurrencesPanel
    Friend WithEvents butRemoveActivity As System.Windows.Forms.Button
    Friend WithEvents butAddActivity As System.Windows.Forms.Button
    Friend WithEvents toolTipAction As System.Windows.Forms.ToolTip
    Private mFileManager As FileManagerLocal
    Public Event ProtocolCheckChanged(ByVal sender As Object, ByVal state As Boolean)

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal aEditor As Designer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        If Not Me.DesignMode Then
            mFileManager = Filemanager.Master
            Dim activity As New TabPageActivity(Me)
            Dim tpActivity As New Crownwood.Magic.Controls.TabPage
            tpActivity.Controls.Add(activity)
            activity.Dock = DockStyle.Fill
            Me.TabControlInstruction.TabPages.Add(tpActivity)
        End If
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
    Friend WithEvents PanelBaseTop As System.Windows.Forms.Panel
    Friend WithEvents HelpProviderInstruction As System.Windows.Forms.HelpProvider
    Friend WithEvents cbProtocol As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TabPageInstruction))
        Me.TabControlInstruction = New Crownwood.Magic.Controls.TabControl
        Me.butRemoveActivity = New System.Windows.Forms.Button
        Me.butAddActivity = New System.Windows.Forms.Button
        Me.PanelBaseTop = New System.Windows.Forms.Panel
        Me.cbProtocol = New System.Windows.Forms.CheckBox
        Me.HelpProviderInstruction = New System.Windows.Forms.HelpProvider
        Me.toolTipAction = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControlInstruction.SuspendLayout()
        Me.PanelBaseTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlInstruction
        '
        Me.TabControlInstruction.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabControlInstruction.BoldSelectedPage = True
        Me.TabControlInstruction.Controls.Add(Me.butRemoveActivity)
        Me.TabControlInstruction.Controls.Add(Me.butAddActivity)
        Me.TabControlInstruction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderInstruction.SetHelpKeyword(Me.TabControlInstruction, "Screens/pathway_screen.html")
        Me.HelpProviderInstruction.SetHelpNavigator(Me.TabControlInstruction, System.Windows.Forms.HelpNavigator.Topic)
        Me.TabControlInstruction.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabControlInstruction.Location = New System.Drawing.Point(0, 24)
        Me.TabControlInstruction.Name = "TabControlInstruction"
        Me.TabControlInstruction.PositionTop = True
        Me.HelpProviderInstruction.SetShowHelp(Me.TabControlInstruction, True)
        Me.TabControlInstruction.Size = New System.Drawing.Size(848, 400)
        Me.TabControlInstruction.TabIndex = 0
        Me.TabControlInstruction.TextInactiveColor = System.Drawing.Color.Black
        Me.TabControlInstruction.CausesValidation = True        
        '
        'butRemoveActivity
        '
        Me.butRemoveActivity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butRemoveActivity.Image = CType(resources.GetObject("butRemoveActivity.Image"), System.Drawing.Image)
        Me.butRemoveActivity.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butRemoveActivity.Location = New System.Drawing.Point(818, 0)
        Me.butRemoveActivity.Name = "butRemoveActivity"
        Me.butRemoveActivity.Size = New System.Drawing.Size(27, 25)
        Me.butRemoveActivity.TabIndex = 17
        Me.toolTipAction.SetToolTip(Me.butRemoveActivity, "Remove activity")
        Me.butRemoveActivity.Visible = False
        '
        'butAddActivity
        '
        Me.butAddActivity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butAddActivity.Image = CType(resources.GetObject("butAddActivity.Image"), System.Drawing.Image)
        Me.butAddActivity.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butAddActivity.Location = New System.Drawing.Point(789, 0)
        Me.butAddActivity.Name = "butAddActivity"
        Me.butAddActivity.Size = New System.Drawing.Size(27, 25)
        Me.butAddActivity.TabIndex = 16
        Me.toolTipAction.SetToolTip(Me.butAddActivity, "Add new activity")
        Me.butAddActivity.Visible = False 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
        '
        'PanelBaseTop
        '
        Me.PanelBaseTop.Controls.Add(Me.cbProtocol)
        Me.PanelBaseTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelBaseTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelBaseTop.Name = "PanelBaseTop"
        Me.PanelBaseTop.Size = New System.Drawing.Size(848, 24)
        Me.PanelBaseTop.TabIndex = 1
        '
        'cbProtocol
        '
        Me.cbProtocol.Location = New System.Drawing.Point(72, 0)
        Me.cbProtocol.Name = "cbProtocol"
        Me.cbProtocol.Size = New System.Drawing.Size(136, 24)
        Me.cbProtocol.TabIndex = 1
        Me.cbProtocol.Text = "Protocol"
        '
        'TabPageInstruction
        '
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.TabControlInstruction)
        Me.Controls.Add(Me.PanelBaseTop)
        Me.Name = "TabPageInstruction"
        Me.Size = New System.Drawing.Size(848, 424)
        Me.TabControlInstruction.ResumeLayout(False)
        Me.PanelBaseTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Function HasProtocol() As Boolean
        'JAR: 30MAY07, EDT-44 Multiple activities per instruction
        'For Each tp As Crownwood.Magic.Controls.TabPage In Me.TabControlInstruction.TabPages
        '    If tp.Name = "tpProtocol" Then
        '        Return True
        '    End If
        'Next
        'Return False
        Return Not TabControlInstruction.TabPages.Item(AE_Constants.Instance.Protocol) Is Nothing        
    End Function

    Private Sub TabPageInstruction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mIsloading = True

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        Me.HelpProviderInstruction.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
        If mFileManager.IsNew Then

            'JAR: 30MAY07, EDT-44 Multiple activities per instruction
            'need to add an RmActivity to the mActivities set
            'Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm(Filemanager.GetOpenEhrTerm(653, "New Activity"))
            'Dim anActivity As New RmActivity(a_term.Code)
            'Debug.Assert(Me.TabControlInstruction.TabPages.Count = 1)
            'Dim tpActivity As Crownwood.Magic.Controls.TabPage = CType(Me.TabControlInstruction.TabPages(0), Crownwood.Magic.Controls.TabPage)
            'tpActivity.Title = a_term.Text
            'CType(tpActivity.Controls(0), TabPageActivity).Activity = anActivity

            Me.TabControlInstruction.TabPages.Clear()
            AddActivityTab()

            Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm("Current Activity", "Current Activity")
            mFileManager.OntologyManager.SetText(a_term)
            Dim rmActivityItem As New RmActivity(a_term.Code)
            AddActivityTab(a_term.Text, rmActivityItem)
            Me.TabControlInstruction.SelectedTab = Me.TabControlInstruction.TabPages(0) 'set to first tab
        End If

        mIsloading = False
    End Sub

    Public ReadOnly Property ComponentType() As StructureType
        Get
            Return StructureType.INSTRUCTION
        End Get
    End Property

    Public Sub toRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        For Each tp As Crownwood.Magic.Controls.TabPage In Me.TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                Dim uc As Control = tp.Controls(0)
                If TypeOf (uc) Is TabPageActivity Then
                    text.WriteLine("\par Activities: \par")
                    CType(uc, TabPageActivity).toRichText(text, level + 1)
                ElseIf TypeOf (uc) Is TabPageStructure Then 'Protocol
                    text.WriteLine("\par Protocol: \par")
                    CType(uc, TabPageStructure).toRichText(text, level + 1)
                End If
            End If
        Next
    End Sub

    Public Sub Reset()
        Me.TabControlInstruction.TabPages.Clear()
        mFileManager = Filemanager.Master
        Dim activity As New TabPageActivity(Me)
        Dim tpActivity As New Crownwood.Magic.Controls.TabPage
        tpActivity.Controls.Add(activity)
        activity.Dock = DockStyle.Fill
        Me.TabControlInstruction.TabPages.Add(tpActivity)
        Me.butRemoveActivity.Visible = False
    End Sub

    Public Sub ProcessInstruction(ByVal instruction_attributes As Children)

        'ADL example
        'activities matches {
        '	ACTIVITY[at0001] matches {
        '		action_archetype_id matches {"openEHR-EHR-ACTION\.medication\.v1"}
        '		description matches {
        '			ITEM_TREE[at0101] matches {	-- tree

        ' At present there is only one allowed activity
        ' but there may be more in the future

        mIsloading = True

        'JAR: 30MAY07, EDT-44 Multiple activities per instruction
        Me.TabControlInstruction.TabPages.Clear()
        AddActivityTab()

        For Each rm_structure As RmStructureCompound In instruction_attributes

            Select Case rm_structure.Type
                Case StructureType.Activities

                    If rm_structure.Children.Count > 0 Then

                        'Me.TabControlInstruction.TabPages.Clear()

                        For Each rmActivityItem As RmActivity In rm_structure.Children
                            'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                            AddActivityTab(mFileManager.OntologyManager.GetText(rmActivityItem.NodeId), rmActivityItem)

                            'Me.TabControlInstruction.TabPages.Clear()

                            'For Each activity As RmActivity In rm_structure.Children
                            '    Dim anActivity As New TabPageActivity()
                            '    Dim tpActivity As New Crownwood.Magic.Controls.TabPage

                            '    tpActivity.Title = mFileManager.OntologyManager.GetText(activity.NodeId)

                            '    anActivity.Activity = activity
                            '    tpActivity.Controls.Add(anActivity)
                            '    anActivity.Dock = DockStyle.Fill
                            '    Me.TabControlInstruction.TabPages.Add(tpActivity)
                            'Next
                        Next
                    End If
                Case StructureType.Protocol
                    'do nothing
                Case Else
                    Debug.Assert(False, rm_structure.Type.ToString & " - type not handled for attribute 'activities'")
            End Select
        Next

        'JAR: 30MAY07, EDT-44 Multiple activities per instruction
        Me.TabControlInstruction.SelectedTab = Me.TabControlInstruction.TabPages(0) 'set to first tab

        'If Me.TabControlInstruction.TabPages.Count > 1 Then
        '    Me.butRemoveActivity.Visible = True
        'End If

        mIsloading = False

    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point, ByVal mandatory_only As Boolean)
        Dim spacer As Integer = 1

        'leftmargin = pos.X
        If aContainer.Name <> "tpInterface" Then
            aContainer.Size = New Size
        End If

        For Each tp As Crownwood.Magic.Controls.TabPage In Me.TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                Dim uc As Control = tp.Controls(0)
                If TypeOf (uc) Is TabPageActivity Then
                    CType(uc, TabPageActivity).BuildInterface(aContainer, pos, mandatory_only)
                ElseIf TypeOf (uc) Is TabPageStructure Then 'Protocol
                    CType(uc, TabPageStructure).BuildInterface(aContainer, pos, mandatory_only)
                End If
            End If
        Next

    End Sub

    Public Function SaveAsInstruction() As RmStructureCompound
        Dim rm As New RmStructureCompound("Instruction", StructureType.INSTRUCTION)

        'Add the activities
        Dim activities As New RmStructureCompound("activities", StructureType.Activities)

        'for each activity - there is only one at present! EDT-44 Not any more!

        For Each tp As Crownwood.Magic.Controls.TabPage In Me.TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                Dim uc As Control = tp.Controls(0)
                If TypeOf (uc) Is TabPageActivity Then
                    activities.Children.Add(CType(uc, TabPageActivity).Activity)
                ElseIf TypeOf (uc) Is TabPageStructure Then 'Protocol
                    rm.Children.Add(CType(uc, TabPageStructure).SaveAsStructure)
                End If
            End If
        Next
        rm.Children.Add(activities)
        Return rm
    End Function

    Public Sub Translate()
        For Each tp As Crownwood.Magic.Controls.TabPage In Me.TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                Dim uc As Control = tp.Controls(0)
                If TypeOf (uc) Is TabPageActivity Then
                    CType(uc, TabPageActivity).Translate()
                ElseIf TypeOf (uc) Is TabPageStructure Then
                    CType(uc, TabPageStructure).Translate()
                End If
            End If
        Next
    End Sub

    Public Sub TranslateGUI()
        Me.cbProtocol.Text = Filemanager.GetOpenEhrTerm(78, "Protocol")
        Me.toolTipAction.SetToolTip(Me.butAddActivity, Filemanager.GetOpenEhrTerm(653, "New activity"))
        Me.toolTipAction.SetToolTip(Me.butRemoveActivity, Filemanager.GetOpenEhrTerm(586, "Remove activity"))
    End Sub

    Private Sub cbProtocol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProtocol.CheckedChanged
        If Not mFileManager.FileLoading Then
            RaiseEvent ProtocolCheckChanged(Me.TabControlInstruction, cbProtocol.Checked)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub TabPageInstruction_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        OceanArchetypeEditor.Reflect(Me)
    End Sub

    'JAR: 30MAY07, EDT-44 Multiple activities per instruction
    Private Sub butRemoveActivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRemoveActivity.Click
        RemoveActivity()
    End Sub

    Private Sub butAddActivity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddActivity.Click
        'JAR: 30MAY07, EDT-44 Multiple activities per instruction
        NewActivity()

        'Dim activity As New TabPageActivity()
        'Dim tpActivity As New Crownwood.Magic.Controls.TabPage
        ''need to add an RmActivity to the mActivities set


        'Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm(Filemanager.GetOpenEhrTerm(653, "New activity"))

        'Dim s As String() = OceanArchetypeEditor.Instance.GetInput(a_term, Me.ParentForm)

        'If s(0) <> "" Then
        '    mFileManager.OntologyManager.SetText(a_term)
        '    Dim anActivity As New RmActivity(a_term.Code)
        '    tpActivity.Title = a_term.Text
        '    activity.Activity = anActivity

        '    tpActivity.Controls.Add(activity)
        '    activity.Dock = DockStyle.Fill
        '    Me.TabControlInstruction.TabPages.Add(tpActivity)
        '    Me.butRemoveActivity.Visible = True
        'End If
    End Sub

    Private Sub NewActivity() 'Prompts for activity description then adds
        Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm(Filemanager.GetOpenEhrTerm(653, "New activity"))        
        Dim s As String() = OceanArchetypeEditor.Instance.GetInput(a_term, Me.ParentForm)
        mFileManager.OntologyManager.SetText(a_term)

        If s(0) <> "" Then
            Dim rmActivityItem As New RmActivity(a_term.Code)
            AddActivityTab(a_term.Text, rmActivityItem)
        End If
    End Sub

    Private Sub AddActivityTab(ByVal TabCaption As String, ByVal anActivity As RmActivity)
        Dim activity As New TabPageActivity(Me)
        Dim tpActivity As New Crownwood.Magic.Controls.TabPage

        activity.Activity = anActivity
        activity.Dock = DockStyle.Fill
        tpActivity.Controls.Add(activity)
        tpActivity.Title = TabCaption

        'insert tab before "New Activity" tab
        Me.TabControlInstruction.TabPages.Insert(TabControlInstruction.TabPages.IndexOf(tpNewActivity), tpActivity)
    End Sub

    Private Sub AddActivityTab() 'Add "New Activity" blank tab
        tpNewActivity = New Crownwood.Magic.Controls.TabPage
        tpNewActivity.Title = " + " '"New Activity"
        Me.TabControlInstruction.TabPages.Add(tpNewActivity)
    End Sub

    Public Sub RemoveActivity()
        If Not TabControlInstruction.SelectedTab Is Nothing Then
            If Not TabControlInstruction.SelectedTab Is tpNewActivity Then
                'prompt for remove
                If MessageBox.Show(AE_Constants.Instance.Remove & " " & Chr(34) & TabControlInstruction.SelectedTab.Title & Chr(34), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = _
                    Windows.Forms.DialogResult.OK Then
                    Me.TabControlInstruction.TabPages.Remove(Me.TabControlInstruction.SelectedTab)  'remove tab
                    Me.TabControlInstruction.SelectedTab = Me.TabControlInstruction.TabPages(0)     'set to first tab
                    mFileManager.FileEdited = True
                End If
            End If
        End If
    End Sub

    Private Sub TabControlInstruction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlInstruction.Click

        If Not TabControlInstruction.SelectedTab Is Nothing Then
            If TabControlInstruction.SelectedTab Is tpNewActivity Then
                TabControlInstruction.ResumeLayout(False)
                NewActivity()
                'set current tab to the tab just added
                TabControlInstruction.SelectedIndex = TabControlInstruction.TabPages.IndexOf(tpNewActivity) - 1
                TabControlInstruction.ResumeLayout(True)
            End If
        End If
    End Sub

    Private Sub TabControlInstruction_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControlInstruction.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Not TabControlInstruction.SelectedTab Is Nothing AndAlso Not TabControlInstruction.SelectedTab Is tpNewActivity Then 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
                Dim uc As Control = TabControlInstruction.SelectedTab.Controls(0)
                If TypeOf (uc) Is TabPageActivity Then
                    'do not allow removal of first tab
                    CType(uc, TabPageActivity).RemoveToolStripMenuItem.Visible = TabControlInstruction.SelectedIndex <> 0
                    CType(uc, TabPageActivity).ShowPopUp(TabControlInstruction.PointToScreen(e.Location))
                End If
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

