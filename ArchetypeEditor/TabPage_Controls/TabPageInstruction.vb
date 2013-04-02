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
'

Option Strict On

Public Class TabPageInstruction
    Inherits System.Windows.Forms.UserControl

    Private mActionSpecification As TabPageStructure
    Private tpNewActivity As New Crownwood.Magic.Controls.TabPage
    Friend WithEvents mOccurrences As OccurrencesPanel
    Friend WithEvents butRemoveActivity As System.Windows.Forms.Button
    Friend WithEvents butAddActivity As System.Windows.Forms.Button
    Friend WithEvents toolTipAction As System.Windows.Forms.ToolTip
    Friend WithEvents cbParticipation As System.Windows.Forms.CheckBox
    Private mFileManager As FileManagerLocal
    Public Event ProtocolCheckChanged(ByVal sender As Object, ByVal state As Boolean)
    Public Event ParticipationCheckChanged(ByVal sender As Object, ByVal state As Boolean)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        If Not DesignMode Then
            mFileManager = Filemanager.Master
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
        Me.cbParticipation = New System.Windows.Forms.CheckBox
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
        Me.butAddActivity.Visible = False
        '
        'PanelBaseTop
        '
        Me.PanelBaseTop.Controls.Add(Me.cbParticipation)
        Me.PanelBaseTop.Controls.Add(Me.cbProtocol)
        Me.PanelBaseTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelBaseTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelBaseTop.Name = "PanelBaseTop"
        Me.PanelBaseTop.Size = New System.Drawing.Size(848, 24)
        Me.PanelBaseTop.TabIndex = 1
        '
        'cbParticipation
        '
        Me.cbParticipation.Location = New System.Drawing.Point(245, 0)
        Me.cbParticipation.Name = "cbParticipation"
        Me.cbParticipation.Size = New System.Drawing.Size(136, 24)
        Me.cbParticipation.TabIndex = 2
        Me.cbParticipation.Text = "Participation"
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
        Return Not TabControlInstruction.TabPages.Item(AE_Constants.Instance.Protocol) Is Nothing
    End Function

    Public Property HasParticipation() As Boolean
        Get
            Return Not TabControlInstruction.TabPages.Item(AE_Constants.Instance.Participation) Is Nothing
        End Get
        Set(ByVal value As Boolean)
            cbParticipation.Checked = value
        End Set
    End Property

    Private Sub TabPageInstruction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Main.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        HelpProviderInstruction.HelpNamespace = Main.Instance.Options.HelpLocationPath
    End Sub

    Public ReadOnly Property ComponentType() As StructureType
        Get
            Return StructureType.INSTRUCTION
        End Get
    End Property

    Public Sub ToRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        For Each tp As Crownwood.Magic.Controls.TabPage In TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then
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

    Public Sub ProcessInstruction(ByVal attributes As Children)
        'ADL example
        'activities cardinality matches {0..*; unordered} matches {
        '	ACTIVITY[at0001] occurrences matches {0..1} matches {	-- Act 1
        '		description matches {
        '			ITEM_TREE[at0002] matches {*}
        '		}
        '	}
        '	ACTIVITY[at0003] occurrences matches {0..1} matches {	-- Act 2
        '		description matches {
        '			ITEM_TREE[at0004] matches {*}
        '		}
        '	}
        '}

        ClearActivityTabs()

        If attributes Is Nothing Then
            Dim text As String = Filemanager.GetOpenEhrTerm(711, "Current Activity")
            Dim term As RmTerm = mFileManager.OntologyManager.AddTerm(text, text)
            mFileManager.OntologyManager.SetRmTermText(term)
            AddActivityTab(term.Text, New RmActivity(term.Code))
        Else
            For Each rm As RmStructureCompound In attributes
                Select Case rm.Type
                    Case StructureType.Activities
                        For Each activity As RmActivity In rm.Children
                            AddActivityTab(mFileManager.OntologyManager.GetText(activity.NodeId), activity)
                        Next
                    Case StructureType.Protocol
                        'do nothing
                    Case Else
                        Debug.Assert(False, rm.Type.ToString & " - type not handled for attribute 'activities'")
                End Select
            Next
        End If

        TabControlInstruction.SelectedTab = TabControlInstruction.TabPages(0) 'set to first tab
    End Sub

    Public Sub BuildInterface(ByVal container As Control, ByRef pos As Point, ByVal mandatoryOnly As Boolean)
        Dim spacer As Integer = 1

        If container.Name <> "tpInterface" Then
            container.Size = New Size
        End If

        For Each tp As Crownwood.Magic.Controls.TabPage In TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then
                Dim uc As Control = tp.Controls(0)

                If TypeOf uc Is TabPageActivity Then
                    CType(uc, TabPageActivity).BuildInterface(container, pos, mandatoryOnly)
                ElseIf TypeOf uc Is TabPageStructure Then 'Protocol
                    CType(uc, TabPageStructure).BuildInterface(container, pos, mandatoryOnly)
                End If
            End If
        Next
    End Sub

    Public Function SaveAsInstruction() As RmStructureCompound
        Dim result As New RmStructureCompound("Instruction", StructureType.INSTRUCTION)
        Dim activities As New RmStructureCompound("activities", StructureType.Activities)

        For Each tp As Crownwood.Magic.Controls.TabPage In TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then
                Dim uc As TabPageActivity = TryCast(tp.Controls(0), TabPageActivity)

                If Not uc Is Nothing Then
                    activities.Children.Add(uc.Activity)
                End If
            End If
        Next

        result.Children.Add(activities)
        Return result
    End Function

    Public Sub Translate()
        For Each tp As Crownwood.Magic.Controls.TabPage In TabControlInstruction.TabPages
            If Not tp Is tpNewActivity Then
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
        cbProtocol.Text = Filemanager.GetOpenEhrTerm(78, cbProtocol.Text)
        cbParticipation.Text = Filemanager.GetOpenEhrTerm(654, cbParticipation.Text)
        toolTipAction.SetToolTip(butAddActivity, Filemanager.GetOpenEhrTerm(653, "New activity"))
        toolTipAction.SetToolTip(butRemoveActivity, Filemanager.GetOpenEhrTerm(586, "Remove activity"))
    End Sub

    Private Sub cbProtocol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProtocol.CheckedChanged
        If Not mFileManager.FileLoading Then
            RaiseEvent ProtocolCheckChanged(TabControlInstruction, cbProtocol.Checked)
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub cbParticipation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbParticipation.CheckedChanged
        RaiseEvent ParticipationCheckChanged(TabControlInstruction, cbParticipation.Checked)

        If Not mFileManager.FileLoading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub TabPageInstruction_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        Main.Reflect(Me)
    End Sub

    Private Sub butRemoveActivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRemoveActivity.Click
        RemoveActivity()
    End Sub

    Private Sub butAddActivity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddActivity.Click
        AddNewActivity()
    End Sub

    Private Sub AddNewActivity()
        'Prompt for activity description and then add it.
        Dim term As RmTerm = mFileManager.OntologyManager.AddTerm(Filemanager.GetOpenEhrTerm(653, "New activity"))
        Dim s As String() = Main.Instance.GetInput(term, ParentForm)
        mFileManager.OntologyManager.SetRmTermText(term)

        If s(0) <> "" Then
            AddActivityTab(term.Text, New RmActivity(term.Code))
        End If
    End Sub

    Protected Sub ClearActivityTabs()
        tpNewActivity = New Crownwood.Magic.Controls.TabPage
        tpNewActivity.Title = " + "

        TabControlInstruction.TabPages.Clear()
        TabControlInstruction.TabPages.Add(tpNewActivity)
    End Sub

    Private Sub AddActivityTab(ByVal title As String, ByVal activity As RmActivity)
        Dim activityPage As New TabPageActivity(Me)
        activityPage.Activity = activity
        activityPage.Dock = DockStyle.Fill

        Dim tab As New Crownwood.Magic.Controls.TabPage
        tab.Controls.Add(activityPage)
        tab.Title = title

        'insert tab before "New Activity" tab
        TabControlInstruction.TabPages.Insert(TabControlInstruction.TabPages.IndexOf(tpNewActivity), tab)
    End Sub

    Public Sub RemoveActivity()
        If Not TabControlInstruction.SelectedTab Is Nothing Then
            If Not TabControlInstruction.SelectedTab Is tpNewActivity Then
                'prompt for remove
                If MessageBox.Show(AE_Constants.Instance.Remove & " " & Chr(34) & TabControlInstruction.SelectedTab.Title & Chr(34), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = _
                    Windows.Forms.DialogResult.OK Then
                    TabControlInstruction.TabPages.Remove(TabControlInstruction.SelectedTab)  'remove tab
                    TabControlInstruction.SelectedTab = TabControlInstruction.TabPages(0)     'set to first tab
                    mFileManager.FileEdited = True
                End If
            End If
        End If
    End Sub

    Private Sub TabControlInstruction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlInstruction.Click
        If Not TabControlInstruction.SelectedTab Is Nothing Then
            If TabControlInstruction.SelectedTab Is tpNewActivity Then
                TabControlInstruction.ResumeLayout(False)
                AddNewActivity()
                'set current tab to the tab just added
                TabControlInstruction.SelectedIndex = TabControlInstruction.TabPages.IndexOf(tpNewActivity) - 1
                TabControlInstruction.ResumeLayout(True)
            End If
        End If
    End Sub

    Private Sub TabControlInstruction_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControlInstruction.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Not TabControlInstruction.SelectedTab Is Nothing AndAlso Not TabControlInstruction.SelectedTab Is tpNewActivity Then
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

