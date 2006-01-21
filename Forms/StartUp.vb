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
'	file:        "$Source: source/vb.net/archetype_editor/Forms/SCCS/s.StartUp.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class frmStartUp

    Inherits System.Windows.Forms.Form
    Private sChooseText As New String("Choose")

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents gbNew As System.Windows.Forms.GroupBox
    Friend WithEvents comboModel As System.Windows.Forms.ComboBox
    Friend WithEvents comboComponent As System.Windows.Forms.ComboBox
    Friend WithEvents txtConcept As System.Windows.Forms.TextBox
    Friend WithEvents gbExistingArchetype As System.Windows.Forms.GroupBox
    Friend WithEvents butOpen As System.Windows.Forms.Button
    Friend WithEvents lblShortConcept As System.Windows.Forms.Label
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblModel As System.Windows.Forms.Label
    Friend WithEvents HelpProviderStartUp As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmStartUp))
        Me.gbNew = New System.Windows.Forms.GroupBox
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.txtConcept = New System.Windows.Forms.TextBox
        Me.comboComponent = New System.Windows.Forms.ComboBox
        Me.comboModel = New System.Windows.Forms.ComboBox
        Me.lblShortConcept = New System.Windows.Forms.Label
        Me.lblModel = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.gbExistingArchetype = New System.Windows.Forms.GroupBox
        Me.butOpen = New System.Windows.Forms.Button
        Me.HelpProviderStartUp = New System.Windows.Forms.HelpProvider
        Me.gbNew.SuspendLayout()
        Me.gbExistingArchetype.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbNew
        '
        Me.gbNew.Controls.Add(Me.butOK)
        Me.gbNew.Controls.Add(Me.butCancel)
        Me.gbNew.Controls.Add(Me.txtConcept)
        Me.gbNew.Controls.Add(Me.comboComponent)
        Me.gbNew.Controls.Add(Me.comboModel)
        Me.gbNew.Controls.Add(Me.lblShortConcept)
        Me.gbNew.Controls.Add(Me.lblModel)
        Me.gbNew.Controls.Add(Me.Label2)
        Me.gbNew.Location = New System.Drawing.Point(19, 18)
        Me.gbNew.Name = "gbNew"
        Me.gbNew.Size = New System.Drawing.Size(547, 185)
        Me.gbNew.TabIndex = 0
        Me.gbNew.TabStop = False
        Me.gbNew.Text = "New Archetype"
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(278, 138)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(96, 37)
        Me.butOK.TabIndex = 5
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(173, 138)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(96, 37)
        Me.butCancel.TabIndex = 6
        Me.butCancel.Text = "Cancel"
        '
        'txtConcept
        '
        Me.txtConcept.Location = New System.Drawing.Point(8, 103)
        Me.txtConcept.Name = "txtConcept"
        Me.txtConcept.Size = New System.Drawing.Size(530, 22)
        Me.txtConcept.TabIndex = 4
        Me.txtConcept.Text = ""
        '
        'comboComponent
        '
        Me.comboComponent.Enabled = False
        Me.comboComponent.Location = New System.Drawing.Point(259, 48)
        Me.comboComponent.Name = "comboComponent"
        Me.comboComponent.Size = New System.Drawing.Size(269, 24)
        Me.comboComponent.TabIndex = 3
        Me.comboComponent.Text = "Choose..."
        '
        'comboModel
        '
        Me.comboModel.Location = New System.Drawing.Point(38, 48)
        Me.comboModel.Name = "comboModel"
        Me.comboModel.Size = New System.Drawing.Size(192, 24)
        Me.comboModel.TabIndex = 2
        Me.comboModel.Text = "Choose..."
        '
        'lblShortConcept
        '
        Me.lblShortConcept.Location = New System.Drawing.Point(10, 83)
        Me.lblShortConcept.Name = "lblShortConcept"
        Me.lblShortConcept.Size = New System.Drawing.Size(288, 28)
        Me.lblShortConcept.TabIndex = 9
        Me.lblShortConcept.Text = "Short concept label:"
        '
        'lblModel
        '
        Me.lblModel.Location = New System.Drawing.Point(38, 28)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(144, 27)
        Me.lblModel.TabIndex = 10
        Me.lblModel.Text = "Model"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(259, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(211, 27)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Component"
        '
        'gbExistingArchetype
        '
        Me.gbExistingArchetype.Controls.Add(Me.butOpen)
        Me.gbExistingArchetype.Location = New System.Drawing.Point(19, 217)
        Me.gbExistingArchetype.Name = "gbExistingArchetype"
        Me.gbExistingArchetype.Size = New System.Drawing.Size(547, 80)
        Me.gbExistingArchetype.TabIndex = 1
        Me.gbExistingArchetype.TabStop = False
        Me.gbExistingArchetype.Text = "Open existing archetype"
        '
        'butOpen
        '
        Me.butOpen.BackColor = System.Drawing.SystemColors.Control
        Me.butOpen.Image = CType(resources.GetObject("butOpen.Image"), System.Drawing.Image)
        Me.butOpen.Location = New System.Drawing.Point(235, 20)
        Me.butOpen.Name = "butOpen"
        Me.butOpen.Size = New System.Drawing.Size(77, 46)
        Me.butOpen.TabIndex = 7
        '
        'frmStartUp
        '
        Me.AcceptButton = Me.butOpen
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(585, 311)
        Me.Controls.Add(Me.gbExistingArchetype)
        Me.Controls.Add(Me.gbNew)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProviderStartUp.SetHelpKeyword(Me, "Screens/start_up_screen.htm")
        Me.HelpProviderStartUp.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStartUp"
        Me.HelpProviderStartUp.SetShowHelp(Me, True)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Archetype Editor"
        Me.gbNew.ResumeLayout(False)
        Me.gbExistingArchetype.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property ChooseAsText()
        Set(ByVal Value)
            sChooseText = Value
            Me.comboModel.Text = sChooseText & "..."
            Me.comboComponent.Text = sChooseText & "..."
        End Set
    End Property
    Public Property Archetype_ID() As ArchetypeID
        Get
            Return New ArchetypeID(Me.comboModel.Text & "-" & Me.comboComponent.Text & "." & Me.txtConcept.Text & ".v1draft")
        End Get
        Set(ByVal Value As ArchetypeID)
            Debug.Assert(False) ' untested
            'Authority-modelname-componentname.conceptlabel
            txtConcept.Text = Value.Concept

            Me.comboModel.SelectedItem = Value.Reference_Model.ToString
                Try
                Me.comboComponent.SelectedItem = Value.ReferenceModelEntity.ToString
                Catch ex As Exception
                    Debug.Assert(False)
                End Try
        End Set
    End Property


    Private Sub comboModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboModel.SelectedIndexChanged

        ' set the reference model, which sets valid classes to archetype
        ReferenceModel.Instance.ModelType = comboModel.SelectedIndex
        Me.comboComponent.Items.Clear()

        Me.comboComponent.SelectedIndex = -1
        For Each valid_archetype As StructureType In ReferenceModel.Instance.ValidArchetypeDefinitions
            Me.comboComponent.Items.Add(valid_archetype.ToString)
        Next
        Me.comboComponent.Enabled = True
        Me.AcceptButton = Me.butOK

    End Sub

    Private Sub butOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpen.Click
        Me.DialogResult = DialogResult.Yes
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        'Only allow OK if there is enough done
        If Me.comboModel.SelectedIndex = -1 Then
            Beep()
            Me.comboModel.Focus()
            Return
        ElseIf Me.comboComponent.SelectedIndex = -1 Then
            Beep()
            Me.comboComponent.Focus()
            Return
        ElseIf Me.txtConcept.Text = "" Then
            Beep()
            Me.txtConcept.Focus()
            Return
        End If

        ' need to check for illegal characters
        Me.txtConcept.Text = Me.txtConcept.Text.Replace(" ", "_")
        Me.txtConcept.Text = Me.txtConcept.Text.Replace("-", "_")

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub comboComponent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboComponent.SelectedIndexChanged
        If Me.comboComponent.SelectedIndex = -1 Then Return
        ReferenceModel.Instance.ArchetypedClass = ReferenceModel.Instance.ValidArchetypeDefinitions(comboComponent.SelectedIndex)
        Me.AcceptButton = butOK
    End Sub

    Private Sub frmStartUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 0 To ReferenceModel.Instance.ValidReferenceModelNames.Length - 1
            Me.comboModel.Items.Add(ReferenceModel.Instance.ValidReferenceModelNames(i))
        Next
        Me.comboModel.SelectedIndex = OceanArchetypeEditor.Instance.Options.DefaultReferenceModel
        Me.AcceptButton = Me.butOpen
        Me.HelpProviderStartUp.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
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
'The Original Code is StartUp.vb.
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
