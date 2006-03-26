'
'
'	component:   "openEHR Archetype Project"
'	description: "Control that represents the constraints applied to all nodes in an archetype - occurrences, description and runtime name"
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

Public Class ArchetypeNodeConstraintControl
    Inherits System.Windows.Forms.UserControl

    '    Private AnyConstraints As AnyConstraintControl
    Private mConstraintControl As ConstraintControl
    Private mFileManager As FileManagerLocal
    Friend WithEvents mOccurrences As OccurrencesPanel

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then

            mIsLoading = True

            mFileManager = Filemanager.Master

            mOccurrences = New OccurrencesPanel(mFileManager)
            Select Case OceanArchetypeEditor.Instance.Options.OccurrencesView
                Case "lexical"
                    mOccurrences.Mode = OccurrencesMode.Lexical
                Case "numeric"
                    mOccurrences.Mode = OccurrencesMode.Numeric
            End Select

            Me.PanelGenericConstraint.Controls.Add(mOccurrences)

            mOccurrences.Dock = DockStyle.Fill

            If OceanArchetypeEditor.Instance.DefaultLanguageCode <> "en" Then
                TranslateGUI()
            End If

            mIsLoading = False
        End If

        Me.HelpProviderCommonConstraint.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath

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
    Protected components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PanelGenericConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelDataConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelNonAnonymous As System.Windows.Forms.Panel
    Friend WithEvents txtRuntimeName As System.Windows.Forms.TextBox
    Friend WithEvents txtTermDescription As System.Windows.Forms.TextBox
    Friend WithEvents PanelLower As System.Windows.Forms.Panel
    Friend WithEvents butSetRuntimeName As System.Windows.Forms.Button
    Friend WithEvents HelpProviderCommonConstraint As System.Windows.Forms.HelpProvider
    Friend WithEvents labelAnyCluster As System.Windows.Forms.Label
    Friend WithEvents lblRunTimeName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PanelGenericConstraint = New System.Windows.Forms.Panel
        Me.PanelDataConstraint = New System.Windows.Forms.Panel
        Me.labelAnyCluster = New System.Windows.Forms.Label
        Me.PanelNonAnonymous = New System.Windows.Forms.Panel
        Me.butSetRuntimeName = New System.Windows.Forms.Button
        Me.txtRuntimeName = New System.Windows.Forms.TextBox
        Me.lblRunTimeName = New System.Windows.Forms.Label
        Me.txtTermDescription = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.Label
        Me.PanelLower = New System.Windows.Forms.Panel
        Me.HelpProviderCommonConstraint = New System.Windows.Forms.HelpProvider
        Me.PanelDataConstraint.SuspendLayout()
        Me.PanelNonAnonymous.SuspendLayout()
        Me.PanelLower.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelGenericConstraint
        '
        Me.PanelGenericConstraint.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelGenericConstraint.Location = New System.Drawing.Point(0, 0)
        Me.PanelGenericConstraint.Name = "PanelGenericConstraint"
        Me.PanelGenericConstraint.Size = New System.Drawing.Size(376, 48)
        Me.PanelGenericConstraint.TabIndex = 0
        '
        'PanelDataConstraint
        '
        Me.PanelDataConstraint.Controls.Add(Me.labelAnyCluster)
        Me.PanelDataConstraint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDataConstraint.Location = New System.Drawing.Point(0, 96)
        Me.PanelDataConstraint.Name = "PanelDataConstraint"
        Me.PanelDataConstraint.Size = New System.Drawing.Size(376, 104)
        Me.PanelDataConstraint.TabIndex = 31
        '
        'labelAnyCluster
        '
        Me.labelAnyCluster.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelAnyCluster.Location = New System.Drawing.Point(16, 8)
        Me.labelAnyCluster.Name = "labelAnyCluster"
        Me.labelAnyCluster.Size = New System.Drawing.Size(136, 40)
        Me.labelAnyCluster.TabIndex = 0
        Me.labelAnyCluster.Text = "Cluster"
        '
        'PanelNonAnonymous
        '
        Me.PanelNonAnonymous.Controls.Add(Me.butSetRuntimeName)
        Me.PanelNonAnonymous.Controls.Add(Me.txtRuntimeName)
        Me.PanelNonAnonymous.Controls.Add(Me.lblRunTimeName)
        Me.PanelNonAnonymous.Controls.Add(Me.txtTermDescription)
        Me.PanelNonAnonymous.Controls.Add(Me.lblDescription)
        Me.PanelNonAnonymous.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelNonAnonymous.Location = New System.Drawing.Point(0, 0)
        Me.PanelNonAnonymous.Name = "PanelNonAnonymous"
        Me.PanelNonAnonymous.Size = New System.Drawing.Size(376, 96)
        Me.PanelNonAnonymous.TabIndex = 32
        '
        'butSetRuntimeName
        '
        Me.HelpProviderCommonConstraint.SetHelpKeyword(Me.butSetRuntimeName, "HowTo/Edit data/Set_runtime_name.html")
        Me.HelpProviderCommonConstraint.SetHelpNavigator(Me.butSetRuntimeName, System.Windows.Forms.HelpNavigator.Topic)
        Me.butSetRuntimeName.Location = New System.Drawing.Point(330, 70)
        Me.butSetRuntimeName.Name = "butSetRuntimeName"
        Me.HelpProviderCommonConstraint.SetShowHelp(Me.butSetRuntimeName, True)
        Me.butSetRuntimeName.Size = New System.Drawing.Size(26, 20)
        Me.butSetRuntimeName.TabIndex = 8
        Me.butSetRuntimeName.Text = "..."
        '
        'txtRuntimeName
        '
        Me.txtRuntimeName.Location = New System.Drawing.Point(136, 62)
        Me.txtRuntimeName.Name = "txtRuntimeName"
        Me.txtRuntimeName.ReadOnly = True
        Me.txtRuntimeName.Size = New System.Drawing.Size(192, 22)
        Me.txtRuntimeName.TabIndex = 7
        Me.txtRuntimeName.Text = ""
        '
        'lblRunTimeName
        '
        Me.lblRunTimeName.Location = New System.Drawing.Point(8, 62)
        Me.lblRunTimeName.Name = "lblRunTimeName"
        Me.lblRunTimeName.Size = New System.Drawing.Size(120, 32)
        Me.lblRunTimeName.TabIndex = 6
        Me.lblRunTimeName.Text = "Runtime name constraint:"
        Me.lblRunTimeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTermDescription
        '
        Me.txtTermDescription.Location = New System.Drawing.Point(136, 3)
        Me.txtTermDescription.Multiline = True
        Me.txtTermDescription.Name = "txtTermDescription"
        Me.txtTermDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermDescription.Size = New System.Drawing.Size(224, 53)
        Me.txtTermDescription.TabIndex = 5
        Me.txtTermDescription.Text = ""
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(19, 8)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(112, 16)
        Me.lblDescription.TabIndex = 26
        Me.lblDescription.Text = "Description:"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelLower
        '
        Me.PanelLower.Controls.Add(Me.PanelDataConstraint)
        Me.PanelLower.Controls.Add(Me.PanelNonAnonymous)
        Me.PanelLower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLower.Location = New System.Drawing.Point(0, 48)
        Me.PanelLower.Name = "PanelLower"
        Me.PanelLower.Size = New System.Drawing.Size(376, 200)
        Me.PanelLower.TabIndex = 33
        '
        'ArchetypeNodeConstraintControl
        '
        Me.Controls.Add(Me.PanelLower)
        Me.Controls.Add(Me.PanelGenericConstraint)
        Me.HelpProviderCommonConstraint.SetHelpKeyword(Me, "HowTo/Edit data/set_common_constraints.htm")
        Me.HelpProviderCommonConstraint.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "ArchetypeNodeConstraintControl"
        Me.HelpProviderCommonConstraint.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(376, 248)
        Me.PanelDataConstraint.ResumeLayout(False)
        Me.PanelNonAnonymous.ResumeLayout(False)
        Me.PanelLower.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mArchetypeNode As ArchetypeNode

    Private mIsLoading As Boolean = True
    Protected ReadOnly Property IsLoading() As Boolean
        Get
            Return mIsLoading
        End Get
    End Property

    Public Sub TranslateGUI()
        Me.lblDescription.Text = Filemanager.GetOpenEhrTerm(113, Me.lblDescription.Text)
        Me.lblRunTimeName.Text = Filemanager.GetOpenEhrTerm(114, Me.lblRunTimeName.Text)
        Me.labelAnyCluster.Text = Filemanager.GetOpenEhrTerm(313, Me.labelAnyCluster.Text)
    End Sub

    Public Sub ShowConstraint(ByVal aStructureType As StructureType, _
            ByVal IsState As Boolean, ByVal aArchetypeNode As ArchetypeNode, ByVal a_file_manager As FileManagerLocal)

        mFileManager = a_file_manager
        mIsLoading = True
        Me.SuspendLayout()

        Try
            ' hide the label if there is no constraint (for ANY or Cluster) - see below
            Me.labelAnyCluster.Visible = False

            If Not mConstraintControl Is Nothing Then
                Me.PanelDataConstraint.Controls.Remove(mConstraintControl)
                mConstraintControl = Nothing
            End If

            Select Case aArchetypeNode.RM_Class.Type
                Case StructureType.Element, StructureType.Reference

                    Dim archetypeElem As ArchetypeElement = CType(aArchetypeNode, ArchetypeElement)

                    Select Case archetypeElem.Constraint.Type
                        Case ConstraintType.Any
                            Me.labelAnyCluster.Text = AE_Constants.Instance.Any
                            Me.labelAnyCluster.Visible = True

                        Case ConstraintType.URI
                            Me.labelAnyCluster.Text = AE_Constants.Instance.URI
                            Me.labelAnyCluster.Visible = True

                        Case Else
                            mConstraintControl = ConstraintControl.CreateConstraintControl( _
                                                           archetypeElem.Constraint.Type, mFileManager)


                            Me.PanelDataConstraint.Controls.Add(mConstraintControl)

                            ' Ensures the ZOrder leads to no overlap
                            mConstraintControl.Dock = DockStyle.Fill

                            mConstraintControl.ShowConstraint(IsState, archetypeElem)
                    End Select

                Case StructureType.Slot

                    mConstraintControl = ConstraintControl.CreateConstraintControl( _
                               ConstraintType.Slot, mFileManager)

                    Me.PanelDataConstraint.Controls.Add(mConstraintControl)

                    ' Ensures the ZOrder leads to no overlap
                    mConstraintControl.Dock = DockStyle.Fill

                    ' HKF: 1620
                    mConstraintControl.ShowConstraint(IsState, CType(CType(aArchetypeNode, ArchetypeNodeAnonymous).RM_Class, RmSlot).SlotConstraint)

                Case StructureType.Cluster
                    ' Me.labelAnyCluster.Text = AE_Constants.Instance.Cluster
                    Me.labelAnyCluster.Visible = True
                    mConstraintControl = New ClusterControl(a_file_manager)
                    CType(mConstraintControl, ClusterControl).Item = CType(aArchetypeNode, ArchetypeComposite)
                    Me.PanelDataConstraint.Controls.Add(mConstraintControl)

                    CType(mConstraintControl, ClusterControl).Header = 50
                    mConstraintControl.Dock = DockStyle.Fill

            End Select

            mArchetypeNode = aArchetypeNode

            If aStructureType = StructureType.Single Then
                mOccurrences.SetSingle = True
            Else
                mOccurrences.SetSingle = False
            End If

            SetControlValues(IsState)

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Me.ResumeLayout(False)
        mIsLoading = False
    End Sub

    Protected Overridable Sub SetControlValues(ByVal IsState As Boolean) '(ByVal aArchetypeNode As ArchetypeNode)

        ' ToDo: set constraint values on control

        ' set the cardinality

        mOccurrences.Cardinality = mArchetypeNode.Occurrences

        If mArchetypeNode.IsAnonymous Then
            Me.PanelNonAnonymous.Visible = False

        Else
            Me.PanelNonAnonymous.Visible = True

            ' set the description of the term
            Me.txtTermDescription.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).Description

            ' set the runtime name text
            Me.txtRuntimeName.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).RuntimeNameText

            If mArchetypeNode.RM_Class.Type = StructureType.Reference Then
                Me.Enabled = False
            Else
                Me.Enabled = True
            End If
        End If

    End Sub


    Private Sub txtTermDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermDescription.TextChanged

        If Not mIsLoading Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Description = Me.txtTermDescription.Text

            mFileManager.FileEdited = True

        End If

    End Sub


    Private Sub butSetRuntimeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSetRuntimeName.Click
        Dim frm As New ConstraintForm
        Dim has_constraint As Boolean
        Dim t As Constraint_Text

        has_constraint = mArchetypeNode.RM_Class.HasNameConstraint
        If has_constraint Then
            t = CType(mArchetypeNode.RM_Class.NameConstraint.copy, Constraint_Text)
        End If

        frm.ShowConstraint(False, mArchetypeNode.RM_Class.NameConstraint, mFileManager)
        Select Case frm.ShowDialog
            Case DialogResult.OK
                'no action
                mFileManager.FileEdited = True
            Case DialogResult.Cancel
                ' put it back to null if it was before
                If Not has_constraint Then
                    mArchetypeNode.RM_Class.HasNameConstraint = False
                Else
                    mArchetypeNode.RM_Class.NameConstraint = t
                End If
            Case DialogResult.Ignore
                mArchetypeNode.RM_Class.HasNameConstraint = False
                mFileManager.FileEdited = True
        End Select

        If mArchetypeNode.RM_Class.HasNameConstraint Then
            Me.txtRuntimeName.Text = mArchetypeNode.RM_Class.NameConstraint.ToString
        Else
            Me.txtRuntimeName.Text = ""
        End If
    End Sub

    Private Sub ArchetypeNodeConstraintControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub txtTermDescription_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTermDescription.KeyPress
        ' work around as acceptsreturn = false does not deal with stop Enter unless there is a AcceptButton
        If e.KeyChar = Chr(13) Then
            e.Handled = True
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
'The Original Code is ArchetypeNodeConstraintControl.vb.
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
