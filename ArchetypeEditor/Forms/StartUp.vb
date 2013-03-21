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
Option Explicit On

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

    Friend WithEvents lblModel As System.Windows.Forms.Label
    Friend WithEvents HelpProviderStartUp As System.Windows.Forms.HelpProvider
    Friend WithEvents gbArchetypeFromWeb As System.Windows.Forms.GroupBox
    Friend WithEvents butOpenFromWeb As System.Windows.Forms.Button
    Friend WithEvents lblComponent As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStartUp))
        Me.gbNew = New System.Windows.Forms.GroupBox
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.txtConcept = New System.Windows.Forms.TextBox
        Me.comboComponent = New System.Windows.Forms.ComboBox
        Me.comboModel = New System.Windows.Forms.ComboBox
        Me.lblShortConcept = New System.Windows.Forms.Label
        Me.lblModel = New System.Windows.Forms.Label
        Me.lblComponent = New System.Windows.Forms.Label
        Me.gbExistingArchetype = New System.Windows.Forms.GroupBox
        Me.butOpen = New System.Windows.Forms.Button
        Me.HelpProviderStartUp = New System.Windows.Forms.HelpProvider
        Me.gbArchetypeFromWeb = New System.Windows.Forms.GroupBox
        Me.butOpenFromWeb = New System.Windows.Forms.Button
        Me.gbNew.SuspendLayout()
        Me.gbExistingArchetype.SuspendLayout()
        Me.gbArchetypeFromWeb.SuspendLayout()
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
        Me.gbNew.Controls.Add(Me.lblComponent)
        Me.gbNew.Location = New System.Drawing.Point(16, 16)
        Me.gbNew.Name = "gbNew"
        Me.gbNew.Size = New System.Drawing.Size(456, 160)
        Me.gbNew.TabIndex = 0
        Me.gbNew.TabStop = False
        Me.gbNew.Text = "New Archetype"
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(144, 120)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(80, 32)
        Me.butOK.TabIndex = 5
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(232, 120)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(80, 32)
        Me.butCancel.TabIndex = 6
        Me.butCancel.Text = "Cancel"
        '
        'txtConcept
        '
        Me.txtConcept.Location = New System.Drawing.Point(11, 89)
        Me.txtConcept.Name = "txtConcept"
        Me.txtConcept.Size = New System.Drawing.Size(429, 20)
        Me.txtConcept.TabIndex = 4
        '
        'comboComponent
        '
        Me.comboComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboComponent.Enabled = False
        Me.comboComponent.Location = New System.Drawing.Point(216, 42)
        Me.comboComponent.MaxDropDownItems = 20
        Me.comboComponent.Name = "comboComponent"
        Me.comboComponent.Size = New System.Drawing.Size(224, 21)
        Me.comboComponent.TabIndex = 3
        '
        'comboModel
        '
        Me.comboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboModel.Location = New System.Drawing.Point(32, 42)
        Me.comboModel.MaxDropDownItems = 20
        Me.comboModel.Name = "comboModel"
        Me.comboModel.Size = New System.Drawing.Size(160, 21)
        Me.comboModel.TabIndex = 2
        '
        'lblShortConcept
        '
        Me.lblShortConcept.Location = New System.Drawing.Point(8, 72)
        Me.lblShortConcept.Name = "lblShortConcept"
        Me.lblShortConcept.Size = New System.Drawing.Size(240, 24)
        Me.lblShortConcept.TabIndex = 9
        Me.lblShortConcept.Text = "Short concept label:"
        '
        'lblModel
        '
        Me.lblModel.Location = New System.Drawing.Point(32, 24)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(120, 24)
        Me.lblModel.TabIndex = 10
        Me.lblModel.Text = "Model"
        '
        'lblComponent
        '
        Me.lblComponent.Location = New System.Drawing.Point(216, 24)
        Me.lblComponent.Name = "lblComponent"
        Me.lblComponent.Size = New System.Drawing.Size(176, 24)
        Me.lblComponent.TabIndex = 11
        Me.lblComponent.Text = "Component"
        '
        'gbExistingArchetype
        '
        Me.gbExistingArchetype.Controls.Add(Me.butOpen)
        Me.gbExistingArchetype.Location = New System.Drawing.Point(16, 188)
        Me.gbExistingArchetype.Name = "gbExistingArchetype"
        Me.gbExistingArchetype.Size = New System.Drawing.Size(456, 69)
        Me.gbExistingArchetype.TabIndex = 1
        Me.gbExistingArchetype.TabStop = False
        Me.gbExistingArchetype.Text = "Open existing archetype"
        '
        'butOpen
        '
        Me.butOpen.BackColor = System.Drawing.SystemColors.Control
        Me.butOpen.Image = CType(resources.GetObject("butOpen.Image"), System.Drawing.Image)
        Me.butOpen.Location = New System.Drawing.Point(191, 18)
        Me.butOpen.Name = "butOpen"
        Me.butOpen.Size = New System.Drawing.Size(76, 40)
        Me.butOpen.TabIndex = 7
        Me.butOpen.UseVisualStyleBackColor = False
        '
        'gbArchetypeFromWeb
        '
        Me.gbArchetypeFromWeb.Controls.Add(Me.butOpenFromWeb)
        Me.gbArchetypeFromWeb.Location = New System.Drawing.Point(16, 275)
        Me.gbArchetypeFromWeb.Name = "gbArchetypeFromWeb"
        Me.gbArchetypeFromWeb.Size = New System.Drawing.Size(456, 69)
        Me.gbArchetypeFromWeb.TabIndex = 3
        Me.gbArchetypeFromWeb.TabStop = False
        Me.gbArchetypeFromWeb.Text = "Open archetype from Web"
        '
        'butOpenFromWeb
        '
        Me.butOpenFromWeb.BackColor = System.Drawing.SystemColors.Control
        Me.butOpenFromWeb.Image = CType(resources.GetObject("butOpenFromWeb.Image"), System.Drawing.Image)
        Me.butOpenFromWeb.Location = New System.Drawing.Point(196, 17)
        Me.butOpenFromWeb.Name = "butOpenFromWeb"
        Me.butOpenFromWeb.Size = New System.Drawing.Size(64, 40)
        Me.butOpenFromWeb.TabIndex = 7
        Me.butOpenFromWeb.UseVisualStyleBackColor = False
        '
        'frmStartUp
        '
        Me.AcceptButton = Me.butOpen
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(490, 362)
        Me.Controls.Add(Me.gbArchetypeFromWeb)
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
        Me.gbNew.PerformLayout()
        Me.gbExistingArchetype.ResumeLayout(False)
        Me.gbArchetypeFromWeb.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property Archetype_ID() As ArchetypeID
        Get
            Try
                Return New ArchetypeID(Me.comboModel.Text & "-" & ReferenceModel.RM_StructureName(ReferenceModel.ArchetypedClass) & "." & txtConcept.Text & ".v1")
            Catch
                Return Nothing
                Debug.Assert(False)
            End Try
        End Get
        Set(ByVal Value As ArchetypeID)
            Debug.Assert(False) ' untested
            'Authority-modelname-componentname.conceptlabel
            txtConcept.Text = Value.Concept
            comboModel.SelectedItem = Value.Reference_Model.ToString

            Try
                comboComponent.SelectedItem = Value.ReferenceModelEntity.ToString
            Catch ex As Exception
                Debug.Assert(False)
            End Try
        End Set
    End Property

    Private Sub comboModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboModel.SelectedIndexChanged
        ' set the reference model, which sets valid classes to archetype
        ReferenceModel.SetModelType(comboModel.SelectedIndex)
        comboComponent.Items.Clear()
        comboComponent.SelectedIndex = -1

        For Each valid_archetype As StructureType In ReferenceModel.ValidArchetypeDefinitions
            comboComponent.Items.Add(valid_archetype.ToString)
        Next

        comboComponent.Enabled = True
        AcceptButton = butOK
    End Sub

    Private Sub butOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpen.Click
        DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        If comboModel.SelectedIndex = -1 Then
            Beep()
            comboModel.Focus()
        ElseIf comboComponent.SelectedIndex = -1 Then
            Beep()
            comboComponent.Focus()
        Else
            Dim s As String = Archetype_ID.ValidConcept(txtConcept.Text, "", True)

            If s = "" Then
                Beep()
                txtConcept.Focus()
            Else
                txtConcept.Text = s
                DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    Private Sub comboComponent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboComponent.SelectedIndexChanged
        If comboComponent.SelectedIndex >= 0 Then
            ReferenceModel.SetArchetypedClass(ReferenceModel.ValidArchetypeDefinitions(comboComponent.SelectedIndex))
            AcceptButton = butOK
        End If
    End Sub

    Private Sub frmStartUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each name As String In ReferenceModel.ValidReferenceModelNames
            comboModel.Items.Add(name)
        Next

        comboModel.SelectedIndex = Main.Instance.Options.DefaultReferenceModel
        AcceptButton = butOpen
        HelpProviderStartUp.HelpNamespace = Main.Instance.Options.HelpLocationPath
    End Sub

    Private Sub frmStartUp_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        Main.Reflect(Me)
    End Sub

    Private Sub butOpenFromWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpenFromWeb.Click
        DialogResult = Windows.Forms.DialogResult.Retry
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
