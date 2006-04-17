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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/TabPage_Controls/TabPageInstruction.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-02-20 10:03:02 +0930 (Mon, 20 Feb 2006) $"
'
'

Option Explicit On 

Public Class TabPageAction
    Inherits System.Windows.Forms.UserControl

    Private mIsloading As Boolean
    Private mPathwaySpecification As PathwaySpecification
    Private mActionDescription As TabPageStructure
    Private mFileManager As FileManagerLocal

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal aEditor As Designer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        'Try
        InitializeComponent()

        If Not Me.DesignMode Then
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
    Friend WithEvents tpPathway As Crownwood.Magic.Controls.TabPage
    Friend WithEvents PanelBaseTop As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuPathway As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuAdd As System.Windows.Forms.MenuItem
    Friend WithEvents tpAction As Crownwood.Magic.Controls.TabPage
    Friend WithEvents HelpProviderInstruction As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControlInstruction = New Crownwood.Magic.Controls.TabControl
        Me.tpPathway = New Crownwood.Magic.Controls.TabPage
        Me.tpAction = New Crownwood.Magic.Controls.TabPage
        Me.ContextMenuPathway = New System.Windows.Forms.ContextMenu
        Me.MenuAdd = New System.Windows.Forms.MenuItem
        Me.PanelBaseTop = New System.Windows.Forms.Panel
        Me.HelpProviderInstruction = New System.Windows.Forms.HelpProvider
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
        Me.TabControlInstruction.SelectedIndex = 1
        Me.TabControlInstruction.SelectedTab = Me.tpPathway
        Me.HelpProviderInstruction.SetShowHelp(Me.TabControlInstruction, True)
        Me.TabControlInstruction.Size = New System.Drawing.Size(848, 400)
        Me.TabControlInstruction.TabIndex = 0
        Me.TabControlInstruction.TabPages.AddRange(New Crownwood.Magic.Controls.TabPage() {Me.tpAction, Me.tpPathway})
        Me.TabControlInstruction.TextInactiveColor = System.Drawing.Color.Black
        '
        'tpPathway
        '
        Me.tpPathway.Location = New System.Drawing.Point(0, 0)
        Me.tpPathway.Name = "tpPathway"
        Me.tpPathway.Size = New System.Drawing.Size(848, 374)
        Me.tpPathway.TabIndex = 0
        Me.tpPathway.Title = "Pathway"
        '
        'tpAction
        '
        Me.HelpProviderInstruction.SetHelpKeyword(Me.tpAction, "Screens/action_screen.html")
        Me.HelpProviderInstruction.SetHelpNavigator(Me.tpAction, System.Windows.Forms.HelpNavigator.Topic)
        Me.tpAction.Location = New System.Drawing.Point(0, 0)
        Me.tpAction.Name = "tpAction"
        Me.tpAction.Selected = False
        Me.HelpProviderInstruction.SetShowHelp(Me.tpAction, True)
        Me.tpAction.Size = New System.Drawing.Size(848, 374)
        Me.tpAction.TabIndex = 2
        Me.tpAction.Title = "Action description"
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
        'TabPageAction
        '
        Me.BackColor = System.Drawing.Color.LemonChiffon
        Me.Controls.Add(Me.TabControlInstruction)
        Me.Controls.Add(Me.PanelBaseTop)
        Me.Name = "TabPageAction"
        Me.Size = New System.Drawing.Size(848, 424)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TabPageAction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mIsloading = True
        If mPathwaySpecification Is Nothing Then
            Me.tpPathway.Controls.Clear()
            mPathwaySpecification = New PathwaySpecification
            Me.tpPathway.Controls.Add(mPathwaySpecification)
            mPathwaySpecification.Dock = DockStyle.Fill
        End If
        If mActionDescription Is Nothing Then
            Me.tpAction.Controls.Clear()
            mActionDescription = New TabPageStructure
            Me.tpAction.Controls.Add(mActionDescription)
            mActionDescription.Dock = DockStyle.Fill
        End If
        If OceanArchetypeEditor.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If
        Me.HelpProviderInstruction.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
        mIsloading = False

    End Sub


    Public ReadOnly Property ComponentType() As StructureType
        Get
            Return StructureType.ACTION
        End Get
    End Property

    Public Function toRichText(ByRef text As IO.StringWriter, ByVal level As Integer) As String
        text.WriteLine("\par Action description: \par")
        text.WriteLine("\par")
        mActionDescription.toRichText(text, level + 1)
    End Function

    Public Sub Reset()
        Me.tpPathway.Controls.Clear()
        mPathwaySpecification = New PathwaySpecification
        Me.tpPathway.Controls.Add(mPathwaySpecification)
        mPathwaySpecification.Dock = DockStyle.Fill
    End Sub

    Public Sub ProcessAction(ByVal Struct As Children)

        For Each rm As RmStructureCompound In Struct
            Select Case rm.Type
                Case StructureType.ism_transition
                    Me.tpPathway.Controls.Clear()
                    mPathwaySpecification = New PathwaySpecification
                    Me.tpPathway.Controls.Add(mPathwaySpecification)
                    mPathwaySpecification.Dock = DockStyle.Fill
                    mPathwaySpecification.PathwaySteps = rm
                Case StructureType.ActivityDescription
                    Dim an_action As RmStructure = rm.Children.items(0)
                    Me.tpAction.Controls.Clear()
                    mActionDescription = New TabPageStructure
                    If an_action.Type = StructureType.Slot Then
                        mActionDescription.ProcessStructure(CType(an_action, RmSlot))
                    Else
                        Debug.Assert(an_action.Type = StructureType.Single Or an_action.Type = StructureType.List Or an_action.Type = StructureType.Tree Or an_action.Type = StructureType.Table)
                        mActionDescription.ProcessStructure(CType(an_action, RmStructureCompound))
                    End If
                    Me.tpAction.Controls.Add(mActionDescription)
                    mActionDescription.Dock = DockStyle.Fill
                Case Else
                    Debug.Assert(False, "Not handled yet")
            End Select
        Next
    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point, ByVal mandatory_only As Boolean)
        Dim spacer As Integer = 1

        'leftmargin = pos.X
        If aContainer.Name <> "tpInterface" Then
            aContainer.Size = New Size
        End If

        If Not mActionDescription Is Nothing Then
            mActionDescription.BuildInterface(aContainer, pos, mandatory_only)
        End If

    End Sub

    Public Function SaveAsAction() As RmStructureCompound
        Dim rm As New RmStructureCompound("Action", StructureType.ACTION)
        If Not mPathwaySpecification Is Nothing Then
            rm.Children.Add(mPathwaySpecification.PathwaySteps)
        End If
        If Not mActionDescription Is Nothing Then
            Dim action_spec As New RmStructureCompound("activity_description", StructureType.ActivityDescription)
            Dim rm_struct As RmStructure = mActionDescription.SaveAsStructure
            If Not rm_struct Is Nothing Then
                action_spec.Children.Add(rm_struct)
            End If
            rm.Children.Add(action_spec)
        End If

        Return rm
    End Function

    Public Sub Translate()
        mPathwaySpecification.Translate()
        mActionDescription.Translate()
    End Sub

    Public Sub TranslateGUI()
        Me.tpAction.Title = Filemanager.GetOpenEhrTerm(509, "Activity description")
        Me.tpPathway.Title = Filemanager.GetOpenEhrTerm(510, "Pathway")
        mPathwaySpecification.TranslateGUI()
        mActionDescription.TranslateGUI()
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

