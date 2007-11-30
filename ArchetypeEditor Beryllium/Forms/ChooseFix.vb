'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Jarrad Rigano"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/Forms/ChooseFix.vb $"
'	revision:    "$LastChangedRevision: 108 $"
'	last_change: "$LastChangedDate: 2007-04-30 07:58:54 +0930 (Mon, 30 Apr 2007) $"
'
'   Form to resolve clash between archetype id and filename

Public Class ChooseFix
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal mOntologyManager As OntologyManager, ByVal ArchetypeId As String, ByVal FileName As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Initialise(mOntologyManager, ArchetypeId, FileName)

        If Not Me.DesignMode Then
            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                Me.butCancel.Text = AE_Constants.Instance.Cancel
                Me.butOK.Text = AE_Constants.Instance.OK
            End If
        End If

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
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents cboFix As System.Windows.Forms.ComboBox
    Friend WithEvents lblArchetypeId As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents lblArchetypeIdValue As System.Windows.Forms.Label
    Friend WithEvents lblFileNameValue As System.Windows.Forms.Label
    Friend WithEvents lblFix As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChooseFix))
        Me.lblInfo = New System.Windows.Forms.Label
        Me.butOK = New System.Windows.Forms.Button
        Me.butCancel = New System.Windows.Forms.Button
        Me.cboFix = New System.Windows.Forms.ComboBox
        Me.lblFix = New System.Windows.Forms.Label
        Me.lblArchetypeId = New System.Windows.Forms.Label
        Me.lblFileName = New System.Windows.Forms.Label
        Me.lblArchetypeIdValue = New System.Windows.Forms.Label
        Me.lblFileNameValue = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.Location = New System.Drawing.Point(12, 9)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(332, 24)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "The Archetype Id and the Archetype file name must be the same!"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(350, 9)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(79, 24)
        Me.butOK.TabIndex = 2
        Me.butOK.Text = "OK"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(350, 40)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(79, 24)
        Me.butCancel.TabIndex = 3
        Me.butCancel.Text = "Cancel"
        '
        'cboFix
        '
        Me.cboFix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFix.FormattingEnabled = True
        Me.cboFix.Location = New System.Drawing.Point(15, 110)
        Me.cboFix.Name = "cboFix"
        Me.cboFix.Size = New System.Drawing.Size(414, 21)
        Me.cboFix.TabIndex = 6
        '
        'lblFix
        '
        Me.lblFix.Location = New System.Drawing.Point(12, 80)
        Me.lblFix.Name = "lblFix"
        Me.lblFix.Size = New System.Drawing.Size(322, 27)
        Me.lblFix.TabIndex = 7
        Me.lblFix.Text = "Select an option to resolve this issue:"
        Me.lblFix.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblArchetypeId
        '
        Me.lblArchetypeId.AutoSize = True
        Me.lblArchetypeId.Location = New System.Drawing.Point(12, 45)
        Me.lblArchetypeId.Name = "lblArchetypeId"
        Me.lblArchetypeId.Size = New System.Drawing.Size(70, 13)
        Me.lblArchetypeId.TabIndex = 8
        Me.lblArchetypeId.Text = "Archetype Id:"
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(12, 67)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(103, 13)
        Me.lblFileName.TabIndex = 9
        Me.lblFileName.Text = "Archetype file name:"
        '
        'lblArchetypeIdValue
        '
        Me.lblArchetypeIdValue.AutoSize = True
        Me.lblArchetypeIdValue.Location = New System.Drawing.Point(121, 45)
        Me.lblArchetypeIdValue.Name = "lblArchetypeIdValue"
        Me.lblArchetypeIdValue.Size = New System.Drawing.Size(33, 13)
        Me.lblArchetypeIdValue.TabIndex = 10
        Me.lblArchetypeIdValue.Text = "value"
        '
        'lblFileNameValue
        '
        Me.lblFileNameValue.AutoSize = True
        Me.lblFileNameValue.Location = New System.Drawing.Point(121, 67)
        Me.lblFileNameValue.Name = "lblFileNameValue"
        Me.lblFileNameValue.Size = New System.Drawing.Size(33, 13)
        Me.lblFileNameValue.TabIndex = 11
        Me.lblFileNameValue.Text = "value"
        '
        'ChooseFix
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(439, 142)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblFileNameValue)
        Me.Controls.Add(Me.lblArchetypeIdValue)
        Me.Controls.Add(Me.lblFileName)
        Me.Controls.Add(Me.lblArchetypeId)
        Me.Controls.Add(Me.lblFix)
        Me.Controls.Add(Me.cboFix)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.lblInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChooseFix"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ocean Archetype Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public Enum FixOption
        UseId = 0
        UseFileName = 1
        Ignore = 2
    End Enum

    Public selection As FixOption = FixOption.Ignore

    Private Sub Initialise(ByVal mOntologyManager As OntologyManager, ByVal ArchetypeId As String, ByVal FileName As String)
        'lblInfo.Text = "The " & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & " does not match the " & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & "!"
        lblInfo.Text = "The " & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & " and the " & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & " must be the same!"
        lblArchetypeId.Text = Space(8) & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & ": " & ArchetypeId
        lblFileName.Text = Space(8) & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & ": " & FileName
        lblArchetypeIdValue.Text = ArchetypeId
        lblFileNameValue.Text = FileName

        'must be the same
        'The Archetype Id and the file name must be the same

        lblArchetypeIdValue.Visible = False
        lblFileNameValue.Visible = False

        cboFix.Items.Clear()
        'cboFix.Items.Add("Use " & ArchetypeId) '0 UseId
        'cboFix.Items.Add("Use " & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & Space(1) & ArchetypeId) '0 UseId
        cboFix.Items.Add("Use " & ArchetypeId & "  (" & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & ")") '0 UseId
        'cboFix.Items.Add("Use (" & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & ") " & ArchetypeId) '0 UseId
        'cboFix.Items.Add("Use " & FileName)    '1 UseFileName
        'cboFix.Items.Add("Use " & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & Space(1) & FileName)    '1 UseFileName
        cboFix.Items.Add("Use " & FileName & "  (" & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & ")")    '1 UseFileName
        'cboFix.Items.Add("Use (" & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & ") " & FileName)    '1 UseFileName
        cboFix.Items.Add("Ignore (for now)")   '2 Ignore
        cboFix.SelectedIndex = 2
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        selection = cboFix.SelectedIndex

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Hide()
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
'The Original Code is ChooseFix.vb.
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
