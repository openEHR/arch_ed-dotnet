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

Public Class SimpleStructure
    Inherits EntryStructure
    Private mIsLoading As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New() ' structure type simple

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        If Not Me.DesignMode Then
            Debug.Assert(False)
        End If
    End Sub

    Public Sub New(ByVal fileManager As FileManagerLocal)
        MyBase.New("Single", fileManager) ' structure type simple

        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Sub New(ByVal rm As RmStructureCompound, ByVal fileManager As FileManagerLocal)
        MyBase.New(rm, fileManager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        mIsLoading = True
        Dim element As RmStructure = rm.Children.FirstElementOrElementSlot

        If Not element Is Nothing Then
            If element.Type = StructureType.Element Then
                mCurrentItem = New ArchetypeElement(element, mFileManager)
            Else
                mCurrentItem = New ArchetypeSlot(element, mFileManager)
            End If

            txtSimple.Text = mCurrentItem.Text
            txtSimple.Enabled = True
            PictureBoxSimple.Image = ilSmall.Images(mCurrentItem.ImageIndex(False))
        Else
            ButAddElement.Show()
        End If

        mIsLoading = False
    End Sub


    'UserControl overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    ''Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PictureBoxSimple As System.Windows.Forms.PictureBox
    Friend WithEvents txtSimple As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuSimple As System.Windows.Forms.ContextMenu
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBoxSimple = New System.Windows.Forms.PictureBox
        Me.txtSimple = New System.Windows.Forms.TextBox
        Me.ContextMenuSimple = New System.Windows.Forms.ContextMenu
        Me.SpecialiseMenuItem = New System.Windows.Forms.MenuItem
        CType(Me.PictureBoxSimple, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxSimple
        '
        Me.PictureBoxSimple.Location = New System.Drawing.Point(64, 40)
        Me.PictureBoxSimple.Name = "PictureBoxSimple"
        Me.PictureBoxSimple.Size = New System.Drawing.Size(24, 29)
        Me.PictureBoxSimple.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxSimple.TabIndex = 39
        Me.PictureBoxSimple.TabStop = False
        '
        'txtSimple
        '
        Me.txtSimple.Enabled = False
        Me.txtSimple.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSimple.Location = New System.Drawing.Point(96, 40)
        Me.txtSimple.Name = "txtSimple"
        Me.txtSimple.Size = New System.Drawing.Size(280, 20)
        Me.txtSimple.TabIndex = 38
        '
        'ContextMenuSimple
        '
        Me.ContextMenuSimple.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.SpecialiseMenuItem})
        '
        'MenuSpecialise
        '
        Me.SpecialiseMenuItem.Index = 0
        Me.SpecialiseMenuItem.Text = "Specialise"
        '
        'SimpleStructure
        '
        Me.ContextMenu = Me.ContextMenuSimple
        Me.Controls.Add(Me.PictureBoxSimple)
        Me.Controls.Add(Me.txtSimple)
        Me.Name = "SimpleStructure"
        Me.Controls.SetChildIndex(Me.txtSimple, 0)
        Me.Controls.SetChildIndex(Me.PictureBoxSimple, 0)
        CType(Me.PictureBoxSimple, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Overrides ReadOnly Property InterfaceBuilder() As Object
        Get
            Return mCurrentItem
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructure
        Get
            Dim result As New RmStructureCompound(mNodeId, StructureType.Single)
            result.Occurrences = New RmCardinality(1, 1)

            If Not mCurrentItem Is Nothing Then
                result.Children.Add(mCurrentItem.RM_Class)
            End If

            Return result
        End Get
        Set(ByVal value As RmStructure)
            mIsLoading = True
            mNodeId = value.NodeId
            Dim element As RmStructure = CType(value, RmStructureCompound).Children.FirstElementOrElementSlot()

            If Not element Is Nothing Then
                If element.Type = StructureType.Element Then
                    mCurrentItem = New ArchetypeElement(element, mFileManager)
                Else
                    mCurrentItem = New ArchetypeSlot(element, mFileManager)
                End If

                txtSimple.Text = mCurrentItem.Text
                txtSimple.Enabled = True
                PictureBoxSimple.Image = ilSmall.Images(mCurrentItem.ImageIndex(False))
            Else
                mCurrentItem = Nothing
                ButAddElement.Show()
                txtSimple.Text = AE_Constants.Instance.DragDropHere
                txtSimple.Enabled = False
                PictureBoxSimple.Image = Nothing
            End If

            mIsLoading = False
            mFileManager.FileEdited = True
            SetCurrentItem(mCurrentItem)
        End Set
    End Property

    Public Overrides Function ItemCount() As Integer
        Return IIf(mCurrentItem Is Nothing, 0, 1)
    End Function

    Public Overrides Sub Reset()
        txtSimple.Text = ""
        ButAddElement.Show()
        txtSimple.Enabled = False
        PictureBoxSimple.Image = Nothing
    End Sub

    Public Overrides Sub Translate()
        mIsLoading = True

        If mCurrentItem Is Nothing Then
            txtSimple.Text = ""
        Else
            txtSimple.Text = mCurrentItem.Text
        End If

        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
        mIsLoading = False
    End Sub

    Public Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click
        Dim node As ArchetypeNodeAbstract = TryCast(mCurrentItem, ArchetypeNodeAbstract)

        If Not node Is Nothing Then
            Dim dlg As New SpecialisationQuestionDialog()
            dlg.ShowForArchetypeNode(node.Text, node.RM_Class, SpecialisationDepth)

            If dlg.IsSpecialisationRequested Then
                node.Specialise()
                txtSimple.Text = node.Text
                SetCurrentItem(node)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Assert(False, "Cannot add a reference to a simple")
    End Sub

    Public Overrides Sub SetInitial()
        If Not mCurrentItem Is Nothing Then
            SetCurrentItem(mCurrentItem)
        End If
    End Sub

    Protected Overrides Sub AddNewElement(ByVal constraint As Constraint)
        mIsLoading = True

        If constraint.Kind = ConstraintKind.Slot Then
            mCurrentItem = New ArchetypeSlot(Filemanager.GetOpenEhrTerm(CInt(StructureType.Element), StructureType.Element.ToString), StructureType.Element, mFileManager)
        Else
            mCurrentItem = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New Element"), mFileManager)
            mCurrentItem.Constraint = constraint
        End If

        mCurrentItem.Occurrences.MaxCount = 1
        txtSimple.Text = mCurrentItem.Text
        txtSimple.Enabled = True
        PictureBoxSimple.Image = ilSmall.Images(mCurrentItem.ImageIndex(False))
        txtSimple.Focus()
        txtSimple.SelectAll()
        mFileManager.FileEdited = True
        SetCurrentItem(mCurrentItem)
        ButAddElement.Hide()
        mIsLoading = False
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs)
        If MessageBox.Show(AE_Constants.Instance.Remove & mCurrentItem.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            mIsLoading = True
            mCurrentItem = Nothing
            txtSimple.Text = ""
            ButAddElement.Show()
            txtSimple.Enabled = False
            PictureBoxSimple.Image = Nothing
            mIsLoading = False
            mFileManager.FileEdited = True
        End If
    End Sub

    Public Overrides Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        If mCurrentItem Is Nothing Then
            Return ""
        Else
            Return mCurrentItem.ToRichText(indentlevel)
        End If
    End Function

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim showComments As Boolean = Main.Instance.Options.ShowCommentsInHtml

        result.AppendFormat("<p><i>Structure</i>: {0}", Filemanager.GetOpenEhrTerm(105, "SINGLE"))
        result.AppendFormat("{0}<table border=""1"" cellpadding=""2"" width=""100%"">", Environment.NewLine)
        result.AppendFormat(HtmlHeader(BackGroundColour, showComments))

        If Not mCurrentItem Is Nothing Then
            result.AppendFormat("{0}{1}", Environment.NewLine, mCurrentItem.ToHTML(0, showComments))
        End If

        result.AppendFormat("{0}</tr>", Environment.NewLine)
        result.AppendFormat("{0}</table>", Environment.NewLine)

        Return result.ToString
    End Function

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub butListdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub RefreshIcons()
        PictureBoxSimple.Image = ilSmall.Images(mCurrentItem.ImageIndex(False))
    End Sub

    Private Sub ContextMenuSimple_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuSimple.Popup
        SpecialiseMenuItem.Visible = False

        If Not mCurrentItem Is Nothing Then
            If mCurrentItem.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
                SpecialiseMenuItem.Visible = True
            End If
        End If
    End Sub

    Private Sub txtSimple_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSimple.MouseEnter
        SetToolTipSpecialisation(txtSimple, mCurrentItem)
    End Sub

    Private Sub txtSimple_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSimple.TextChanged
        If Not mIsLoading Then
            Debug.Assert(Not mCurrentItem.IsAnonymous)

            Dim newText As String = txtSimple.Text

            If newText <> mCurrentItem.Text Then
                If mCurrentItem.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                    txtSimple.Text = mCurrentItem.Text
                    SpecialiseCurrentItem(sender, e)
                End If

                If mCurrentItem.RM_Class.SpecialisationDepth = SpecialisationDepth Then
                    txtSimple.Text = newText
                    mCurrentItem.Text = newText
                End If
            End If
        End If
    End Sub

    Private Sub SimpleStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = txtSimple

        If Not mCurrentItem Is Nothing Then
            SetCurrentItem(mCurrentItem)
        Else
            mIsLoading = True
            txtSimple.Text = ""
            ButAddElement.Visible = True
            txtSimple.Enabled = False
            mIsLoading = False
        End If
    End Sub


#Region "Drag and Drop"

    Private Sub txtSimple_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSimple.DragDrop
        If Not mNewConstraint Is Nothing Then
            AddNewElement(mNewConstraint)
            mNewConstraint = Nothing
        Else
            txtSimple.Enabled = False
            Debug.Assert(False, "No item dragged")
        End If
    End Sub

    Private Sub txtSimple_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSimple.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

#End Region

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
'The Original Code is SimpleStructure.vb.
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
