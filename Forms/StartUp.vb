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
'	file:        "$Source: source/vb.net/archetype_editor/Forms/SCCS/s.StartUp.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
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
    Friend WithEvents gbFormat As System.Windows.Forms.GroupBox
    Friend WithEvents rbXML As System.Windows.Forms.RadioButton
    Friend WithEvents rbADL As System.Windows.Forms.RadioButton
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
        Me.gbFormat = New System.Windows.Forms.GroupBox
        Me.rbXML = New System.Windows.Forms.RadioButton
        Me.rbADL = New System.Windows.Forms.RadioButton
        Me.butOpen = New System.Windows.Forms.Button
        Me.HelpProviderStartUp = New System.Windows.Forms.HelpProvider
        Me.gbArchetypeFromWeb = New System.Windows.Forms.GroupBox
        Me.butOpenFromWeb = New System.Windows.Forms.Button
        Me.gbNew.SuspendLayout()
        Me.gbExistingArchetype.SuspendLayout()
        Me.gbFormat.SuspendLayout()
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
        Me.txtConcept.Location = New System.Drawing.Point(13, 103)
        Me.txtConcept.Name = "txtConcept"
        Me.txtConcept.Size = New System.Drawing.Size(515, 22)
        Me.txtConcept.TabIndex = 4
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
        'lblComponent
        '
        Me.lblComponent.Location = New System.Drawing.Point(259, 28)
        Me.lblComponent.Name = "lblComponent"
        Me.lblComponent.Size = New System.Drawing.Size(211, 27)
        Me.lblComponent.TabIndex = 11
        Me.lblComponent.Text = "Component"
        '
        'gbExistingArchetype
        '
        Me.gbExistingArchetype.Controls.Add(Me.gbFormat)
        Me.gbExistingArchetype.Controls.Add(Me.butOpen)
        Me.gbExistingArchetype.Location = New System.Drawing.Point(19, 217)
        Me.gbExistingArchetype.Name = "gbExistingArchetype"
        Me.gbExistingArchetype.Size = New System.Drawing.Size(547, 80)
        Me.gbExistingArchetype.TabIndex = 1
        Me.gbExistingArchetype.TabStop = False
        Me.gbExistingArchetype.Text = "Open existing archetype"
        '
        'gbFormat
        '
        Me.gbFormat.Controls.Add(Me.rbXML)
        Me.gbFormat.Controls.Add(Me.rbADL)
        Me.gbFormat.Enabled = False
        Me.gbFormat.Location = New System.Drawing.Point(373, 14)
        Me.gbFormat.Name = "gbFormat"
        Me.gbFormat.Size = New System.Drawing.Size(155, 60)
        Me.gbFormat.TabIndex = 9
        Me.gbFormat.TabStop = False
        Me.gbFormat.Text = "Format"
        '
        'rbXML
        '
        Me.rbXML.AutoSize = True
        Me.rbXML.Location = New System.Drawing.Point(86, 20)
        Me.rbXML.Name = "rbXML"
        Me.rbXML.Size = New System.Drawing.Size(57, 21)
        Me.rbXML.TabIndex = 1
        Me.rbXML.TabStop = True
        Me.rbXML.Text = "XML"
        Me.rbXML.UseVisualStyleBackColor = True
        '
        'rbADL
        '
        Me.rbADL.AutoSize = True
        Me.rbADL.Location = New System.Drawing.Point(14, 20)
        Me.rbADL.Name = "rbADL"
        Me.rbADL.Size = New System.Drawing.Size(56, 21)
        Me.rbADL.TabIndex = 0
        Me.rbADL.TabStop = True
        Me.rbADL.Text = "ADL"
        Me.rbADL.UseVisualStyleBackColor = True
        '
        'butOpen
        '
        Me.butOpen.BackColor = System.Drawing.SystemColors.Control
        Me.butOpen.Image = CType(resources.GetObject("butOpen.Image"), System.Drawing.Image)
        Me.butOpen.Location = New System.Drawing.Point(229, 21)
        Me.butOpen.Name = "butOpen"
        Me.butOpen.Size = New System.Drawing.Size(91, 46)
        Me.butOpen.TabIndex = 7
        Me.butOpen.UseVisualStyleBackColor = False
        '
        'gbArchetypeFromWeb
        '
        Me.gbArchetypeFromWeb.Controls.Add(Me.butOpenFromWeb)
        Me.gbArchetypeFromWeb.Location = New System.Drawing.Point(19, 317)
        Me.gbArchetypeFromWeb.Name = "gbArchetypeFromWeb"
        Me.gbArchetypeFromWeb.Size = New System.Drawing.Size(547, 80)
        Me.gbArchetypeFromWeb.TabIndex = 3
        Me.gbArchetypeFromWeb.TabStop = False
        Me.gbArchetypeFromWeb.Text = "Open archetype from Web"
        '
        'butOpenFromWeb
        '
        Me.butOpenFromWeb.BackColor = System.Drawing.SystemColors.Control
        Me.butOpenFromWeb.Image = CType(resources.GetObject("butOpenFromWeb.Image"), System.Drawing.Image)
        Me.butOpenFromWeb.Location = New System.Drawing.Point(235, 20)
        Me.butOpenFromWeb.Name = "butOpenFromWeb"
        Me.butOpenFromWeb.Size = New System.Drawing.Size(77, 46)
        Me.butOpenFromWeb.TabIndex = 7
        Me.butOpenFromWeb.UseVisualStyleBackColor = False
        '
        'frmStartUp
        '
        Me.AcceptButton = Me.butOpen
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(572, 404)
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
        Me.gbFormat.ResumeLayout(False)
        Me.gbFormat.PerformLayout()
        Me.gbArchetypeFromWeb.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property Archetype_ID() As ArchetypeID
        Get
            Try
                Return New ArchetypeID(Me.comboModel.Text & "-" & ReferenceModel.RM_StructureName(ReferenceModel.ArchetypedClass) & "." & Me.txtConcept.Text & ".v1draft")
            Catch
                Return Nothing
                Debug.Assert(False)
            End Try
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
        ReferenceModel.SetModelType(comboModel.SelectedIndex)
        Me.comboComponent.Items.Clear()

        Me.comboComponent.SelectedIndex = -1
        For Each valid_archetype As StructureType In ReferenceModel.ValidArchetypeDefinitions
            Me.comboComponent.Items.Add(valid_archetype.ToString)
        Next
        Me.comboComponent.Enabled = True
        Me.AcceptButton = Me.butOK

    End Sub

    Private Sub butOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpen.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
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

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub comboComponent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboComponent.SelectedIndexChanged
        If Me.comboComponent.SelectedIndex = -1 Then Return
        ReferenceModel.SetArchetypedClass(ReferenceModel.ValidArchetypeDefinitions(comboComponent.SelectedIndex))
        Me.AcceptButton = butOK
    End Sub

    Private Sub frmStartUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 0 To ReferenceModel.ValidReferenceModelNames.Length - 1
            Me.comboModel.Items.Add(ReferenceModel.ValidReferenceModelNames(i))
        Next
        If (OceanArchetypeEditor.Instance.Options.AllowWebSearch <> True) Then
            Me.gbArchetypeFromWeb.Visible = False
            Me.Height = Me.Height - ((Me.gbArchetypeFromWeb.Height) + 10)
        End If
        Me.comboModel.SelectedIndex = OceanArchetypeEditor.Instance.Options.DefaultReferenceModel
        Me.AcceptButton = Me.butOpen
        Me.HelpProviderStartUp.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
    End Sub

    Private Sub frmStartUp_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.RightToLeftChanged
        OceanArchetypeEditor.Reflect(Me)
    End Sub

    Private Sub rbADL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbADL.CheckedChanged
        If rbADL.Focused Then
            OceanArchetypeEditor.Instance.Options.DefaultParser = "adl"
        End If
    End Sub

    Private Sub rbXML_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbXML.CheckedChanged
        If rbXML.Focused Then
            OceanArchetypeEditor.Instance.Options.DefaultParser = "xml"
        End If
    End Sub

    Private Sub butOpenFromWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpenFromWeb.Click
        Me.DialogResult = Windows.Forms.DialogResult.Retry 'JAR: 18APR07, EDT-35 Clean up compile time warnings
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
