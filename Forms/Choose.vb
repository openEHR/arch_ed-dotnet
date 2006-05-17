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
            If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
                Me.Text = Filemanager.GetOpenEhrTerm(104, "Choose")
                Me.LblForm.Text = Filemanager.GetOpenEhrTerm(104, "Choose")
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
    Friend WithEvents LblForm As System.Windows.Forms.Label
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents ListChoose As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Choose))
        Me.ListChoose = New System.Windows.Forms.ListBox
        Me.LblForm = New System.Windows.Forms.Label
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'ListChoose
        '
        Me.ListChoose.ItemHeight = 16
        Me.ListChoose.Location = New System.Drawing.Point(16, 46)
        Me.ListChoose.Name = "ListChoose"
        Me.ListChoose.Size = New System.Drawing.Size(328, 180)
        Me.ListChoose.TabIndex = 0
        '
        'LblForm
        '
        Me.LblForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForm.Location = New System.Drawing.Point(19, 9)
        Me.LblForm.Name = "LblForm"
        Me.LblForm.Size = New System.Drawing.Size(298, 28)
        Me.LblForm.TabIndex = 1
        Me.LblForm.Text = "Choose"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(499, 258)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(67, 37)
        Me.butCancel.TabIndex = 2
        Me.butCancel.Text = "Cancel"
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(576, 258)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(67, 37)
        Me.butOK.TabIndex = 3
        Me.butOK.Text = "OK"
        '
        'ListBox2
        '
        Me.ListBox2.ItemHeight = 16
        Me.ListBox2.Location = New System.Drawing.Point(355, 46)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(288, 180)
        Me.ListBox2.TabIndex = 4
        '
        'Choose
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(652, 306)
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

    End Sub

#End Region

    Public Sub Set_Single()
        Dim p As New Drawing.Point()
        Me.Width = 380
        p.X = 224
        p.Y = Me.butOK.Location.Y
        Me.butOK.Location = p
        p.X = 149
        p.Y = Me.butCancel.Location.Y
        Me.butCancel.Location = p
        Me.ListBox2.Visible = False
    End Sub

    Public Sub Set_Double()
        Dim p As New Drawing.Point()
        Me.Width = 552
        p.X = 480
        p.Y = Me.butOK.Location.Y
        Me.butOK.Location = p
        p.X = 416
        p.Y = Me.butCancel.Location.Y
        Me.butCancel.Location = p
        Me.ListBox2.Visible = True
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
        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.DialogResult = DialogResult.Cancel
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
