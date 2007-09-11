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
    Friend WithEvents buttonClose As System.Windows.Forms.Button
    Public WithEvents timerSplash As System.Windows.Forms.Timer

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
        Me.SuspendLayout()
        '
        'buttonClose
        '
        Me.buttonClose.Location = New System.Drawing.Point(44, 194)
        Me.buttonClose.Name = "buttonClose"
        Me.buttonClose.Size = New System.Drawing.Size(75, 23)
        Me.buttonClose.TabIndex = 0
        Me.buttonClose.Text = "Close"
        Me.buttonClose.UseVisualStyleBackColor = True
        '
        'timerSplash
        '
        Me.timerSplash.Interval = 2000
        '
        'Splash
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(566, 254)
        Me.ControlBox = False
        Me.Controls.Add(Me.buttonClose)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Splash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Archetype Editor    Release 1 candidate (1243)"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private showingAsSplash As Boolean = False

    Public ReadOnly Property AssemblyDescription() As String
        Get
            ' Get all Description attributes on this assembly
            Dim attributes As Object() = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(System.Reflection.AssemblyDescriptionAttribute), False)
            ' If there aren't any Description attributes, return an empty string
            If attributes.Length = 0 Then
                Return ""
            End If
            ' If there is a Description attribute, return its value
            Return DirectCast(attributes(0), System.Reflection.AssemblyDescriptionAttribute).Description
        End Get
    End Property

    Public Overloads Sub Show(ByVal showAsSplash As Boolean)
        Me.Text = String.Format("{0}, v{1}.{2}", "Archetype Editor", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor)

        If showAsSplash Then
            Me.buttonClose.Visible = False
            Me.Text = String.Format("{0}, v{1}.{2}", "Archetype Editor", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor)
            'Me.labelDescription.Visible = False
            Me.timerSplash.Enabled = True
        Else
            Me.buttonClose.Visible = True
            'Me.labelDescription.Visible = True
            'Me.labelDescription.Text = AssemblyDescription
            'Me.labelDescription.BackColor = Color.FromArgb(80, 255, 255, 255)
            Me.Text = String.Format("{0}, v{1}", "Archetype Editor", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString)
        End If

        showingAsSplash = showAsSplash
        Me.Show()
    End Sub

    Private Sub timerSplash_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerSplash.Tick
        timerSplash.Enabled = False
        timerSplash.Interval = 50

        If Me.Opacity = 0 Then
            Me.Hide()
        Else
            Me.Opacity -= 0.05
            timerSplash.Enabled = True
        End If
    End Sub


    Private Sub buttonClose_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
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

