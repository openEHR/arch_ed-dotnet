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

Public Class Choose
    Inherits System.Windows.Forms.Form
    Friend DTab_2 As DataTable
    Friend DTab_1 As DataTable

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            If Main.Instance.DefaultLanguageCode <> "en" Then
                Text = Filemanager.GetOpenEhrTerm(104, "Choose")
                LblForm.Text = Filemanager.GetOpenEhrTerm(104, "Choose")
                butCancel.Text = AE_Constants.Instance.Cancel
                butOK.Text = AE_Constants.Instance.OK
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
    Friend WithEvents LblForm As System.Windows.Forms.Label
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents ListChoose As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Choose))
        Me.ListChoose = New System.Windows.Forms.ListBox
        Me.LblForm = New System.Windows.Forms.Label
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'ListChoose
        '
        Me.ListChoose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListChoose.HorizontalScrollbar = True
        Me.ListChoose.Location = New System.Drawing.Point(13, 32)
        Me.ListChoose.Name = "ListChoose"
        Me.ListChoose.Size = New System.Drawing.Size(274, 199)
        Me.ListChoose.TabIndex = 1
        '
        'LblForm
        '
        Me.LblForm.AutoSize = True
        Me.LblForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForm.Location = New System.Drawing.Point(12, 8)
        Me.LblForm.Name = "LblForm"
        Me.LblForm.Size = New System.Drawing.Size(55, 16)
        Me.LblForm.TabIndex = 0
        Me.LblForm.Text = "Choose"
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(452, 237)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(84, 28)
        Me.butCancel.TabIndex = 4
        Me.butCancel.Text = "Cancel"
        '
        'butOK
        '
        Me.butOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butOK.Location = New System.Drawing.Point(364, 237)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(83, 28)
        Me.butOK.TabIndex = 3
        Me.butOK.Text = "OK"
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.HorizontalScrollbar = True
        Me.ListBox2.Location = New System.Drawing.Point(296, 32)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(240, 199)
        Me.ListBox2.TabIndex = 2
        '
        'Choose
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(549, 276)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.LblForm)
        Me.Controls.Add(Me.ListChoose)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Choose"
        Me.ShowInTaskbar = False
        Me.Text = "Choose"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Sub Set_Single()
        MinimumSize = New Size(Width - ClientSize.Width + ListBox2.Left, Height)
        MaximumSize = New Size(MinimumSize.Width, Screen.PrimaryScreen.Bounds.Height)
        ListBox2.Visible = False
    End Sub

    Public Sub Set_Double()
        MinimumSize = New Size(Width - ClientSize.Width + ListBox2.Left + ListBox2.Width + ListChoose.Left, Height)
        MaximumSize = New Size(MinimumSize.Width, Screen.PrimaryScreen.Bounds.Height)
        ListBox2.Visible = True
    End Sub

    Public Sub PrepareDataTable_for_List(ByVal n As Integer)
        If n = 1 Then
            If DTab_1 Is Nothing Then
                DTab_1 = New DataTable("DataTable")
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.DataType = System.Type.GetType("System.Int32")
                idColumn.ColumnName = "Id"
                DTab_1.Columns.Add(idColumn)
                Dim CodeColumn As DataColumn = New DataColumn()
                CodeColumn.DataType = System.Type.GetType("System.String")
                CodeColumn.ColumnName = "Code"
                DTab_1.Columns.Add(CodeColumn)
                Dim TextColumn As DataColumn = New DataColumn()
                TextColumn.DataType = System.Type.GetType("System.String")
                TextColumn.ColumnName = "Text"
                DTab_1.Columns.Add(TextColumn)
            Else
                DTab_1.Rows.Clear()
            End If
        ElseIf n = 2 Then
            If DTab_2 Is Nothing Then
                DTab_2 = New DataTable("DataTable")
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.DataType = System.Type.GetType("System.Int32")
                idColumn.ColumnName = "Id"
                DTab_2.Columns.Add(idColumn)
                Dim CodeColumn As DataColumn = New DataColumn()
                CodeColumn.DataType = System.Type.GetType("System.String")
                CodeColumn.ColumnName = "Code"
                DTab_2.Columns.Add(CodeColumn)
                Dim TextColumn As DataColumn = New DataColumn()
                TextColumn.DataType = System.Type.GetType("System.String")
                TextColumn.ColumnName = "Text"
                DTab_2.Columns.Add(TextColumn)
            Else
                DTab_2.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click, ListChoose.DoubleClick
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
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
'The Original Code is Choose.vb.
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
