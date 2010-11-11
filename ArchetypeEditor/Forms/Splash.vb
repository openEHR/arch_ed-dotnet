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
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class Splash
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        VersionLabel.Text = "Version " + Application.ProductVersion
        CopyrightLabel.Text = Options.Copyright
        LogoPictureBox.Image = Main.Instance.Splash
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
    Friend WithEvents buttonClose As System.Windows.Forms.Button
    Public WithEvents timerSplash As System.Windows.Forms.Timer
    Friend WithEvents VersionLabel As System.Windows.Forms.Label
    Friend WithEvents CopyrightLabel As System.Windows.Forms.Label
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Splash))
        Me.buttonClose = New System.Windows.Forms.Button
        Me.timerSplash = New System.Windows.Forms.Timer(Me.components)
        Me.VersionLabel = New System.Windows.Forms.Label
        Me.CopyrightLabel = New System.Windows.Forms.Label
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'buttonClose
        '
        Me.buttonClose.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.PaleGoldenrod
        Me.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonClose.Location = New System.Drawing.Point(25, 187)
        Me.buttonClose.Name = "buttonClose"
        Me.buttonClose.Size = New System.Drawing.Size(108, 26)
        Me.buttonClose.TabIndex = 0
        Me.buttonClose.Text = "Close"
        Me.buttonClose.UseVisualStyleBackColor = False
        '
        'timerSplash
        '
        Me.timerSplash.Interval = 2000
        '
        'VersionLabel
        '
        Me.VersionLabel.BackColor = System.Drawing.Color.Transparent
        Me.VersionLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VersionLabel.Location = New System.Drawing.Point(149, 152)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(242, 14)
        Me.VersionLabel.TabIndex = 1
        Me.VersionLabel.Text = "Version"
        Me.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CopyrightLabel
        '
        Me.CopyrightLabel.BackColor = System.Drawing.Color.Transparent
        Me.CopyrightLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CopyrightLabel.Location = New System.Drawing.Point(148, 169)
        Me.CopyrightLabel.Name = "CopyrightLabel"
        Me.CopyrightLabel.Size = New System.Drawing.Size(242, 14)
        Me.CopyrightLabel.TabIndex = 2
        Me.CopyrightLabel.Text = "Copyright © "
        Me.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Location = New System.Drawing.Point(6, 6)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(144, 181)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LogoPictureBox.TabIndex = 3
        Me.LogoPictureBox.TabStop = False
        '
        'Splash
        '
        Me.AcceptButton = Me.buttonClose
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CancelButton = Me.buttonClose
        Me.ClientSize = New System.Drawing.Size(578, 262)
        Me.Controls.Add(Me.buttonClose)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.Controls.Add(Me.CopyrightLabel)
        Me.Controls.Add(Me.VersionLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Splash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About Archetype Editor"
        Me.TopMost = True
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub ShowAsSplash()
        buttonClose.Hide()
        timerSplash.Enabled = True
        FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Show()
    End Sub

    Private Sub timerSplash_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerSplash.Tick
        timerSplash.Enabled = False
        timerSplash.Interval = 50

        If Opacity <= 0.05 Then
            Hide()
        Else
            Opacity *= 3 / 4
            timerSplash.Enabled = True
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
'The Original Code is Splash.vb.
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

