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
    Private mActionSpecification As TabPageStructure
    Friend WithEvents mOccurrences As OccurrencesPanel
    Private mActivity As RmActivity
    Private mFileManager As FileManagerLocal

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal aEditor As Designer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        'Try
        InitializeComponent()

        If Not Me.DesignMode Then
            mFileManager = Filemanager.Master
            If mActionSpecification Is Nothing Then
                mActionSpecification = New TabPageStructure
            End If
            Me.tpActivity.Controls.Add(mActionSpecification)
            mActionSpecification.BringToFront()
            mActionSpecification.Dock = DockStyle.Fill
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
    Friend WithEvents butGetAction As System.Windows.Forms.Button
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents PanelAction As System.Windows.Forms.Panel
    Friend WithEvents lblNodeId As System.Windows.Forms.Label
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents tpActivity As Crownwood.Magic.Controls.TabPage
    Friend WithEvents menuItemRename As System.Windows.Forms.MenuItem
    Friend WithEvents butOpenArchetype As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabPageInstruction))
        Me.TabControlInstruction = New Crownwood.Magic.Controls.TabControl
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.menuItemRename = New System.Windows.Forms.MenuItem
        Me.tpActivity = New Crownwood.Magic.Controls.TabPage
        Me.PanelAction = New System.Windows.Forms.Panel
        Me.butOpenArchetype = New System.Windows.Forms.Button
        Me.lblNodeId = New System.Windows.Forms.Label
        Me.lblAction = New System.Windows.Forms.Label
        Me.butGetAction = New System.Windows.Forms.Button
        Me.txtAction = New System.Windows.Forms.TextBox
        Me.PanelBaseTop = New System.Windows.Forms.Panel
        Me.HelpProviderInstruction = New System.Windows.Forms.HelpProvider
        Me.tpActivity.SuspendLayout()
        Me.PanelAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlInstruction
        '
        Me.TabControlInstruction.BackColor = System.Drawing.Color.CornflowerBlue
        Me.TabControlInstruction.BoldSelectedPage = True
        Me.TabControlInstruction.ContextMenu = Me.ContextMenu1
        Me.TabControlInstruction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderInstruction.SetHelpKeyword(Me.TabControlInstruction, "Screens/pathway_screen.html")
        Me.HelpProviderInstruction.SetHelpNavigator(Me.TabControlInstruction, System.Windows.Forms.HelpNavigator.Topic)
        Me.TabControlInstruction.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways
        Me.TabControlInstruction.Location = New System.Drawing.Point(0, 24)
        Me.TabControlInstruction.Name = "TabControlInstruction"
        Me.TabControlInstruction.PositionTop = True
        Me.TabControlInstruction.SelectedIndex = 0
        Me.TabControlInstruction.SelectedTab = Me.tpActivity
        Me.HelpProviderInstruction.SetShowHelp(Me.TabControlInstruction, True)
        Me.TabControlInstruction.Size = New System.Drawing.Size(848, 400)
        Me.TabControlInstruction.TabIndex = 0
        Me.TabControlInstruction.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpActivity})
        Me.TabControlInstruction.TextInactiveColor = System.Drawing.Color.Black
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItemRename})
        '
        'menuItemRename
        '
        Me.menuItemRename.Index = 0
        Me.menuItemRename.Text = "Rename"
        '
        'tpActivity
        '
        Me.tpActivity.Controls.Add(Me.PanelAction)
        Me.HelpProviderInstruction.SetHelpKeyword(Me.tpActivity, "Screens/action_screen.html")
        Me.HelpProviderInstruction.SetHelpNavigator(Me.tpActivity, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpActivity.Location = New System.Drawing.Point(0, 0)
        Me.tpActivity.Name = "tpActivity"
        Me.HelpProviderInstruction.SetShowHelp(Me.tpActivity, True)
        Me.tpActivity.Size = New System.Drawing.Size(848, 374)
        Me.tpActivity.TabIndex = 2
        Me.tpActivity.Title = "Activity"
        '
        'PanelAction
        '
        Me.PanelAction.Controls.Add(Me.butOpenArchetype)
        Me.PanelAction.Controls.Add(Me.lblNodeId)
        Me.PanelAction.Controls.Add(Me.lblAction)
        Me.PanelAction.Controls.Add(Me.butGetAction)
        Me.PanelAction.Controls.Add(Me.txtAction)
        Me.PanelAction.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAction.Location = New System.Drawing.Point(0, 0)
        Me.PanelAction.Name = "PanelAction"
        Me.PanelAction.Size = New System.Drawing.Size(848, 48)
        Me.PanelAction.TabIndex = 2
        '
        'butOpenArchetype
        '
        Me.butOpenArchetype.Image = CType(resources.GetObject("butOpenArchetype.Image"), System.Drawing.Image)
        Me.butOpenArchetype.Location = New System.Drawing.Point(336, 8)
        Me.butOpenArchetype.Name = "butOpenArchetype"
        Me.butOpenArchetype.Size = New System.Drawing.Size(24, 24)
        Me.butOpenArchetype.TabIndex = 5
        '
        'lblNodeId
        '
        Me.lblNodeId.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblNodeId.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblNodeId.Location = New System.Drawing.Point(792, 0)
        Me.lblNodeId.Name = "lblNodeId"
        Me.lblNodeId.Size = New System.Drawing.Size(56, 48)
        Me.lblNodeId.TabIndex = 4
        '
        'lblAction
        '
        Me.lblAction.Location = New System.Drawing.Point(8, 11)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(64, 16)
        Me.lblAction.TabIndex = 3
        Me.lblAction.Text = "Action"
        '
        'butGetAction
        '
        Me.butGetAction.Location = New System.Drawing.Point(296, 8)
        Me.butGetAction.Name = "butGetAction"
        Me.butGetAction.Size = New System.Drawing.Size(32, 24)
        Me.butGetAction.TabIndex = 2
        Me.butGetAction.Text = "..."
        '
        'txtAction
        '
        Me.txtAction.Location = New System.Drawing.Point(72, 8)
        Me.txtAction.Name = "txtAction"
        Me.txtAction.Size = New System.Drawing.Size(216, 24)
        Me.txtAction.TabIndex = 1
        Me.txtAction.Text = ""
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
        Me.tpActivity.ResumeLayout(False)
        Me.PanelAction.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TabPageInstruction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mIsloading = True
        If mOccurrences Is Nothing Then
            mOccurrences = New OccurrencesPanel(mFileManager)
        End If
        Me.PanelAction.Controls.Add(mOccurrences)
        mOccurrences.Dock = DockStyle.Right
        Me.HelpProviderInstruction.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
        If mFileManager.IsNew Then
            'need to add an RmActivity to the mActivities set
            Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm("new activity")
            Dim activity As New RmActivity(a_term.Code)
            mActivity = activity
        End If
        mIsloading = False

    End Sub

    Public ReadOnly Property ComponentType() As StructureType
        Get
            Return StructureType.INSTRUCTION
        End Get
    End Property

    Public Function toRichText(ByRef text As IO.StringWriter, ByVal level As Integer) As String
        text.WriteLine("\par Action archetype: \par")
        text.WriteLine("      " & Me.txtAction.Text & "\par")

        If Not mActionSpecification Is Nothing Then
            text.WriteLine("\par Action specification: \par")
            text.WriteLine("\par")
            mActionSpecification.toRichText(text, level + 1)
        End If
    End Function

    Public Sub Reset()
        Me.txtAction.Text = ""
        mActionSpecification = New TabPageStructure
        Me.lblNodeId.Text = ""
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

        For Each rm_structure As RmStructureCompound In instruction_attributes

            Select Case rm_structure.Type
                Case StructureType.Activities

                    Debug.Assert(rm_structure.Children.Count < 2)

                    For Each activity As RmActivity In rm_structure.Children

                        mActivity = activity

                        Me.lblNodeId.Text = activity.NodeId
                        If mOccurrences Is Nothing Then
                            mOccurrences = New OccurrencesPanel(mFileManager)
                            Select Case OceanArchetypeEditor.Instance.Options.OccurrencesView
                                Case "lexical"
                                    mOccurrences.Mode = OccurrencesMode.Lexical
                                Case "numeric"
                                    mOccurrences.Mode = OccurrencesMode.Numeric
                            End Select
                        End If

                        Me.mOccurrences.Cardinality = activity.Occurrences
                        Me.txtAction.Text = activity.ArchetypeId

                        For Each rm As RmStructure In activity.Children

                            If Me.tpActivity.Controls.Contains(mActionSpecification) Then
                                Me.txtAction.Controls.Remove(mActionSpecification)
                            End If
                            mActionSpecification = New TabPageStructure

                            Select Case rm.Type
                                Case StructureType.List, StructureType.Table, StructureType.Tree, StructureType.Single
                                    mActionSpecification.ProcessStructure(CType(rm, RmStructureCompound))
                                Case StructureType.Slot
                                    mActionSpecification.ProcessStructure(CType(rm, RmSlot))
                                Case Else
                                    Debug.Assert(False, "Not handled yet")
                            End Select

                            Me.tpActivity.Controls.Add(mActionSpecification)
                            mActionSpecification.BringToFront()
                            mActionSpecification.Dock = DockStyle.Fill

                        Next
                    Next
                Case Else
                    Debug.Assert(False, rm_structure.Type.ToString & " - type not handled for attribute 'activities'")
            End Select
        Next

        mIsloading = False

    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point, ByVal mandatory_only As Boolean)
        Dim spacer As Integer = 1

        'leftmargin = pos.X
        If aContainer.Name <> "tpInterface" Then
            aContainer.Size = New Size
        End If

        If Not mActionSpecification Is Nothing Then
            mActionSpecification.BuildInterface(aContainer, pos, mandatory_only)
        End If

    End Sub

    Public Function SaveAsInstruction() As RmStructureCompound
        Dim rm As New RmStructureCompound("Instruction", StructureType.INSTRUCTION)

        'Add the activities
        Dim activities As New RmStructureCompound("activities", StructureType.Activities)

        'for each each activity - there is only one at present!

        mActivity.Children.Clear()

        mActivity.Occurrences = mOccurrences.Cardinality

        mActivity.ArchetypeId = Me.txtAction.Text

        If Not mActionSpecification Is Nothing Then
            mActivity.Children.Add(mActionSpecification.SaveAsStructure)
        Else
            'add reference to action archetype
            'ToDo:
        End If

        activities.Children.Add(mActivity)

        rm.Children.Add(activities)

        Return rm
    End Function

    Public Sub Translate()
        mActionSpecification.Translate()
        If mFileManager.OntologyManager.Ontology.LanguageAvailable(mFileManager.OntologyManager.LanguageCode) Then
            Me.tpActivity.Title = mFileManager.OntologyManager.GetTerm(mActivity.NodeId).Text
        End If
    End Sub

    Public Sub TranslateGUI()
        Me.tpActivity.Title = Filemanager.GetOpenEhrTerm(586, "Activity")
        Me.lblAction.Text = Filemanager.GetOpenEhrTerm(556, "Action")
    End Sub

    Private Sub butGetAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butGetAction.Click
        Dim fd As New OpenFileDialog
        Dim s As String

        s = ReferenceModel.Instance.ReferenceModelName & "-ACTION"
        fd.Filter = s & "|" & s & ".*.adl"
        fd.InitialDirectory = OceanArchetypeEditor.Instance.Options.RepositoryPath & "\Action"

        If fd.ShowDialog = DialogResult.OK Then
            Dim ss As String

            ss = fd.FileName.Substring(fd.FileName.LastIndexOf("\") + s.Length + 2)
            Me.txtAction.Text = ss.Substring(0, ss.LastIndexOf("."))
        End If
    End Sub

    Private Sub ContextMenu1_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenu1.Popup
        If Me.TabControlInstruction.SelectedTab Is Me.tpActivity Then
            menuItemRename.Text = AE_Constants.Instance.Rename & " - " & tpActivity.Title
            menuItemRename.Visible = True
        Else
            menuItemRename.Visible = False
        End If

    End Sub

    Private Sub menuItemRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuItemRename.Click
        Dim f As New InputForm

        'ToDo: needs to change to allow more than one activity
        If f.ShowDialog = DialogResult.OK Then
            If f.txtInput.Text <> "" Then
                Me.tpActivity.Title = f.txtInput.Text
                mFileManager.OntologyManager.SetText(Me.tpActivity.Title, mActivity.NodeId)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Private Sub txtAction_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAction.TextChanged
        If Not mIsloading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butOpenArchetype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpenArchetype.Click
        Try
            Dim start_info As New ProcessStartInfo
            start_info.FileName = Application.ExecutablePath
            start_info.WorkingDirectory = Application.StartupPath
            start_info.Arguments = OceanArchetypeEditor.Instance.Options.RepositoryPath & "\entry\action\" & "openEHR-EHR-ACTION." & Me.txtAction.Text & ".adl"
            Process.Start(start_info)
        Catch
            MessageBox.Show(AE_Constants.Instance.Error_loading & " Archetype Editor", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

