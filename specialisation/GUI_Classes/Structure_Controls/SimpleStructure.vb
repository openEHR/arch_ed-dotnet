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

Option Explicit On 

Public Class SimpleStructure
    Inherits EntryStructure
    Private mElement As ArchetypeNode
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

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New("Single", a_file_manager) ' structure type simple

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        mFileManager.FileEdited = True

    End Sub

    Sub New(ByVal rm As RmStructureCompound, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(rm, a_file_manager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        mIsLoading = True
        Dim element As RmStructure = rm.Children.FirstElementOrElementSlot

        If Not element Is Nothing Then
            If element.Type = StructureType.Element Then
                mElement = New ArchetypeElement(element, mFileManager)
            Else 'Slot
                mElement = New ArchetypeSlot(element, mFileManager)
            End If

            txtSimple.Text = mElement.Text
            txtSimple.Enabled = True
            PictureBoxSimple.Image = ilSmall.Images(ImageIndexForItem(mElement, False))
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
    Friend WithEvents MenuSpecialise As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBoxSimple = New System.Windows.Forms.PictureBox
        Me.txtSimple = New System.Windows.Forms.TextBox
        Me.ContextMenuSimple = New System.Windows.Forms.ContextMenu
        Me.MenuSpecialise = New System.Windows.Forms.MenuItem
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
        Me.ContextMenuSimple.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuSpecialise})
        '
        'MenuSpecialise
        '
        Me.MenuSpecialise.Index = 0
        Me.MenuSpecialise.Text = "Specialise"
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
            Return mElement
        End Get
    End Property

    Public Overrides Property Archetype() As RmStructureCompound
        Get
            Dim rm As New RmStructureCompound(mNodeId, StructureType.Single)
            rm.Occurrences = New RmCardinality(1, 1)

            If Not mElement Is Nothing Then
                rm.Children.Add(mElement.RM_Class)
            End If

            Return rm
        End Get
        Set(ByVal Value As RmStructureCompound)
            mIsLoading = True
            mNodeId = Value.NodeId
            Dim element As RmStructure = Value.Children.FirstElementOrElementSlot

            If Not element Is Nothing Then
                If element.Type = StructureType.Element Then
                    mElement = New ArchetypeElement(element, mFileManager)
                Else
                    mElement = New ArchetypeSlot(element, mFileManager)
                End If

                txtSimple.Text = mElement.Text
                txtSimple.Enabled = True
                PictureBoxSimple.Image = ilSmall.Images(ImageIndexForItem(mElement, False))
            Else
                mElement = Nothing
                ButAddElement.Show()
                txtSimple.Text = AE_Constants.Instance.DragDropHere
                txtSimple.Enabled = False
                PictureBoxSimple.Image = Nothing
            End If

            mIsLoading = False
            mFileManager.FileEdited = True
            SetCurrentItem(mElement)
        End Set
    End Property

    Public Overrides ReadOnly Property Elements() As ArchetypeElement()
        Get
            If mElement Is Nothing Then
                Return Nothing
            Else
                Dim a_e(0) As ArchetypeElement
                a_e(0) = mElement
                Return a_e
            End If
        End Get
    End Property

    Public Overrides Function HasData() As Boolean
        Return Not mElement Is Nothing
    End Function

    Public Overrides Sub Reset()
        txtSimple.Text = ""
        ButAddElement.Show()
        txtSimple.Enabled = False
        PictureBoxSimple.Image = Nothing
    End Sub

    Public Overrides Sub Translate()
        mIsLoading = True

        If mElement Is Nothing Then
            txtSimple.Text = ""
        Else
            mElement.Translate()
            txtSimple.Text = mElement.Text
        End If

        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
        mIsLoading = False
    End Sub

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs)
        If TypeOf mElement Is ArchetypeElement Then
            If MessageBox.Show(AE_Constants.Instance.Specialise & " '" & txtSimple.Text & "'?", _
                AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                CType(mElement, ArchetypeElement).Specialise()
                txtSimple.Text = mElement.Text
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Assert(False, "Cannot add a reference to a simple")
    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        mConstraintMenu.Show(ButAddElement, New System.Drawing.Point(5, 5))
    End Sub

    Public Overrides Sub SetInitial()
        If Not mElement Is Nothing Then
            SetCurrentItem(mElement)
        End If
    End Sub

    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        mIsLoading = True

        If a_constraint.Type = ConstraintType.Slot Then
            Dim newSlot As New RmSlot(CType(a_constraint, Constraint_Slot).RM_ClassType)
            mElement = New ArchetypeSlot(newSlot, mFileManager)
        Else
            mElement = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New Element"), mFileManager)
            CType(mElement, ArchetypeElement).Constraint = a_constraint
        End If

        mElement.Occurrences.MaxCount = 1
        txtSimple.Text = mElement.Text
        txtSimple.Enabled = True
        PictureBoxSimple.Image = ilSmall.Images(ImageIndexForItem(mElement, False))
        txtSimple.Focus()
        txtSimple.SelectAll()
        mFileManager.FileEdited = True
        SetCurrentItem(mElement)
        ButAddElement.Hide()
        mIsLoading = False
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs)
        If MessageBox.Show(AE_Constants.Instance.Remove & mElement.Text, _
            AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, _
            MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            mIsLoading = True
            mElement = Nothing
            txtSimple.Text = ""
            ButAddElement.Show()
            txtSimple.Enabled = False
            PictureBoxSimple.Image = Nothing
            mIsLoading = False
            mFileManager.FileEdited = True
        End If
    End Sub

    ReadOnly Property Element() As ArchetypeElement
        Get
            Return mElement
        End Get
    End Property

    Public Overrides Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        If mElement Is Nothing Then
            Return ""
        Else
            Return mElement.ToRichText(indentlevel)
        End If
    End Function

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim showComments As Boolean = OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml

        result.AppendFormat("<p><i>Structure</i>: {0}", Filemanager.GetOpenEhrTerm(105, "SINGLE"))
        result.AppendFormat("{0}<table border=""1"" cellpadding=""2"" width=""100%"">", Environment.NewLine)
        result.AppendFormat(HtmlHeader(BackGroundColour, showComments))

        If Not mElement Is Nothing Then
            result.AppendFormat("{0}{1}", Environment.NewLine, mElement.ToHTML(0, showComments))
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
        Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)
        PictureBoxSimple.Image = ilSmall.Images(ImageIndexForConstraintType(element.Constraint.Type, False, False))
    End Sub

    Private Sub ContextMenuSimple_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuSimple.Popup
        Debug.Assert(ContextMenuSimple.MenuItems.Count = 2)
        ' show specialisation if appropriate

        If mCurrentItem Is Nothing OrElse OceanArchetypeEditor.Instance.CountInString(mCurrentItem.RM_Class.NodeId, ".") >= mFileManager.OntologyManager.NumberOfSpecialisations Then
            MenuSpecialise.Visible = False
        Else
            MenuSpecialise.Text = AE_Constants.Instance.Specialise
            MenuSpecialise.Visible = True
        End If

        If mFileManager.OntologyManager.Ontology.NumberOfSpecialisations = 0 Then
            ContextMenuSimple.MenuItems.Add(menuChangeStructure)
        End If
    End Sub

    Private Sub txtSimple_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSimple.MouseEnter
        SetToolTipSpecialisation(txtSimple, mElement)
    End Sub

    Private Sub txtSimple_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSimple.TextChanged
        If Not mIsLoading Then
            Debug.Assert(Not mCurrentItem.IsAnonymous)

            Dim newText As String = txtSimple.Text

            If newText <> mElement.Text Then
                Dim i As Integer = OceanArchetypeEditor.Instance.CountInString(CType(mCurrentItem, ArchetypeNodeAbstract).NodeId, ".")

                If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
                    txtSimple.Text = mElement.Text
                    SpecialiseCurrentItem(sender, e)
                End If

                i = OceanArchetypeEditor.Instance.CountInString(CType(mCurrentItem, ArchetypeNodeAbstract).NodeId, ".")

                If i >= mFileManager.OntologyManager.NumberOfSpecialisations Then
                    txtSimple.Text = newText
                    mElement.Text = newText
                End If
            End If
        End If
    End Sub

    Private Sub txtSimple_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' ask again if overriding specialisation rules
        OKtoEditSpecialisation = False
    End Sub

    Private Sub SimpleStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = txtSimple

        If Not mElement Is Nothing Then
            SetCurrentItem(mElement)
        Else
            mIsLoading = True
            txtSimple.Text = ""
            ButAddElement.Visible = True
            txtSimple.Enabled = False
            mIsLoading = False
        End If

        ' add the change structure menu from EntryStructure
        ContextMenuSimple.MenuItems.Add(menuChangeStructure)
    End Sub


#Region "Drag and Drop"

    Private Sub txtSimple_DragDrop(ByVal sender As System.Object, _
    ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSimple.DragDrop

        If Not mNewConstraint Is Nothing Then
            AddNewElement(mNewConstraint)
            mNewConstraint = Nothing
        Else
            Me.txtSimple.Enabled = False
            Debug.Assert(False, "No item dragged")
            Return
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
