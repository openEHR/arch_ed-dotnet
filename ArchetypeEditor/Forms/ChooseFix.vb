'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Jarrad Rigano"
'	support:     https://openehr.atlassian.net/browse/AEPR
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
    Friend WithEvents IgnoreButton As System.Windows.Forms.Button
    Friend WithEvents ArchetypeIdButton As System.Windows.Forms.Button
    Friend WithEvents FileNameButton As System.Windows.Forms.Button
    Friend WithEvents ArchetypeIdLabel As System.Windows.Forms.Label
    Friend WithEvents FileNameLabel As System.Windows.Forms.Label
    Friend WithEvents lblFix As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChooseFix))
        Me.lblInfo = New System.Windows.Forms.Label
        Me.lblFix = New System.Windows.Forms.Label
        Me.IgnoreButton = New System.Windows.Forms.Button
        Me.ArchetypeIdButton = New System.Windows.Forms.Button
        Me.FileNameButton = New System.Windows.Forms.Button
        Me.ArchetypeIdLabel = New System.Windows.Forms.Label
        Me.FileNameLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.Location = New System.Drawing.Point(12, 5)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(453, 24)
        Me.lblInfo.TabIndex = 0
        Me.lblInfo.Text = "The Archetype Id and the Archetype file name must be the same!"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblFix
        '
        Me.lblFix.Location = New System.Drawing.Point(12, 29)
        Me.lblFix.Name = "lblFix"
        Me.lblFix.Size = New System.Drawing.Size(453, 22)
        Me.lblFix.TabIndex = 1
        Me.lblFix.Text = "Select an option to resolve this issue:"
        Me.lblFix.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'IgnoreButton
        '
        Me.IgnoreButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.IgnoreButton.Location = New System.Drawing.Point(15, 63)
        Me.IgnoreButton.Name = "IgnoreButton"
        Me.IgnoreButton.Size = New System.Drawing.Size(172, 23)
        Me.IgnoreButton.TabIndex = 2
        Me.IgnoreButton.Text = "Ignore (for now)"
        Me.IgnoreButton.UseVisualStyleBackColor = True
        '
        'ArchetypeIdButton
        '
        Me.ArchetypeIdButton.Location = New System.Drawing.Point(15, 92)
        Me.ArchetypeIdButton.Name = "ArchetypeIdButton"
        Me.ArchetypeIdButton.Size = New System.Drawing.Size(172, 23)
        Me.ArchetypeIdButton.TabIndex = 3
        Me.ArchetypeIdButton.Text = "Use Archetype Id"
        Me.ArchetypeIdButton.UseVisualStyleBackColor = True
        '
        'FileNameButton
        '
        Me.FileNameButton.Location = New System.Drawing.Point(15, 121)
        Me.FileNameButton.Name = "FileNameButton"
        Me.FileNameButton.Size = New System.Drawing.Size(172, 23)
        Me.FileNameButton.TabIndex = 5
        Me.FileNameButton.Text = "Use Archetype file name"
        Me.FileNameButton.UseVisualStyleBackColor = True
        '
        'ArchetypeIdLabel
        '
        Me.ArchetypeIdLabel.AutoSize = True
        Me.ArchetypeIdLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ArchetypeIdLabel.Location = New System.Drawing.Point(193, 97)
        Me.ArchetypeIdLabel.Name = "ArchetypeIdLabel"
        Me.ArchetypeIdLabel.Size = New System.Drawing.Size(13, 13)
        Me.ArchetypeIdLabel.TabIndex = 4
        Me.ArchetypeIdLabel.Text = "?"
        '
        'FileNameLabel
        '
        Me.FileNameLabel.AutoSize = True
        Me.FileNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileNameLabel.Location = New System.Drawing.Point(193, 126)
        Me.FileNameLabel.Name = "FileNameLabel"
        Me.FileNameLabel.Size = New System.Drawing.Size(13, 13)
        Me.FileNameLabel.TabIndex = 6
        Me.FileNameLabel.Text = "?"
        '
        'ChooseFix
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.IgnoreButton
        Me.ClientSize = New System.Drawing.Size(690, 163)
        Me.ControlBox = False
        Me.Controls.Add(Me.FileNameLabel)
        Me.Controls.Add(Me.ArchetypeIdLabel)
        Me.Controls.Add(Me.FileNameButton)
        Me.Controls.Add(Me.ArchetypeIdButton)
        Me.Controls.Add(Me.IgnoreButton)
        Me.Controls.Add(Me.lblFix)
        Me.Controls.Add(Me.lblInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChooseFix"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mismatched Id and File Name"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public Enum FixOption
        UseId = 0
        UseFileName = 1
        Ignore = 2
    End Enum

    Public Selection As FixOption

    Private Sub Initialise(ByVal mOntologyManager As OntologyManager, ByVal ArchetypeId As String, ByVal FileName As String)
        Selection = FixOption.Ignore
        lblInfo.Text = "The " & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & " and the " & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & " must be the same!"
        ArchetypeIdButton.Text = "Use " & mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & ":"
        ArchetypeIdLabel.Text = ArchetypeId
        FileNameButton.Text = "Use " & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & ":"
        FileNameLabel.Text = FileName
    End Sub

    Private Sub ArchetypeIdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArchetypeIdButton.Click
        Selection = FixOption.UseId
        Close()
    End Sub

    Private Sub FileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileNameButton.Click
        Selection = FixOption.UseFileName
        Close()
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
