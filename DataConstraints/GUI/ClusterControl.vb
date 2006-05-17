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

Option Strict On

Public Class ClusterControl : Inherits ConstraintControl

    Private mHeader As Integer
    Private mOccurrences As OccurrencesPanel

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal aFileManager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = aFileManager
        mOccurrences = New OccurrencesPanel(mFileManager)
        mOccurrences.IsContainer = True
        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.LabelTop.Text = Filemanager.GetOpenEhrTerm(313, Me.LabelTop.Text)
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
    Friend WithEvents ClusterPanelTop As System.Windows.Forms.Panel
    Friend WithEvents LabelTop As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ClusterPanelTop = New System.Windows.Forms.Panel
        Me.LabelTop = New System.Windows.Forms.Label
        Me.ClusterPanelTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'ClusterPanelTop
        '
        Me.ClusterPanelTop.Controls.Add(Me.LabelTop)
        Me.ClusterPanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.ClusterPanelTop.Location = New System.Drawing.Point(0, 0)
        Me.ClusterPanelTop.Name = "ClusterPanelTop"
        Me.ClusterPanelTop.Size = New System.Drawing.Size(376, 32)
        Me.ClusterPanelTop.TabIndex = 0
        '
        'LabelTop
        '
        Me.LabelTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTop.Location = New System.Drawing.Point(16, 8)
        Me.LabelTop.Name = "LabelTop"
        Me.LabelTop.Size = New System.Drawing.Size(144, 16)
        Me.LabelTop.TabIndex = 0
        Me.LabelTop.Text = "Cluster"
        '
        'ClusterControl
        '
        Me.Controls.Add(Me.ClusterPanelTop)
        Me.Name = "ClusterControl"
        Me.Size = New System.Drawing.Size(376, 96)
        Me.ClusterPanelTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim mItem As ArchetypeComposite

    Public Property Item() As ArchetypeComposite
        Get
            Return mItem
        End Get
        Set(ByVal Value As ArchetypeComposite)
            mItem = Value
            IsLoading = True
            SetValues()
            IsLoading = False
        End Set
    End Property

    Public Property Header() As Integer
        Get
            Return mHeader
        End Get
        Set(ByVal Value As Integer)
            Dim adjust As Integer

            If Value > 0 Then
                If mHeader = 0 Then
                    adjust = Value
                Else
                    adjust = Value - mHeader
                End If
                mHeader = Value
                For Each ctrl As Control In Me.Controls
                    ctrl.Location = New Drawing.Point(ctrl.Location.X, ctrl.Location.Y + adjust)
                Next
            End If

        End Set
    End Property

    Private Sub SetValues()
        ' set the cardinality

        mOccurrences.Cardinality = mItem.Cardinality
    End Sub


    Private Sub ClusterControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(mOccurrences)
        mOccurrences.BringToFront()
        mOccurrences.Dock = DockStyle.Top
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
'The Original Code is ConstraintControl.vb.
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
