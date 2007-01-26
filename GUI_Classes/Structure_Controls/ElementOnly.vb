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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/GUI_Classes/Structure_Controls/SimpleStructure.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2007-01-09 19:45:11 +0930 (Tue, 09 Jan 2007) $"
'
'

Option Explicit On 

Public Class ElementOnly
    Inherits EntryStructure
    Private mElement As ArchetypeElement
    Private mLoading As Boolean
    Friend WithEvents lblElement As System.Windows.Forms.Label
    Friend WithEvents lblElementOnly As System.Windows.Forms.Label
    Private mOKtoEditSpecialisation As Boolean


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

    Sub New(ByVal an_element As RmElement, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(an_element, a_file_manager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mLoading = True

        If Not an_element Is Nothing Then
            mElement = New ArchetypeElement(an_element, mFileManager)
            If Not mElement.Constraint Is Nothing Then
                Me.PictureBoxSimple.Image = Me.ilSmall.Images(Me.ImageIndexForConstraintType(mElement.Constraint.Type))
                ' can't add any more elements to simple

                SetCurrentItem(mElement) ' does not raise an event during construction
            Else
                Me.ButAddElement.Visible = True
            End If
        End If
        mLoading = False
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
    Friend WithEvents ContextMenuSimple As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuSpecialise As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBoxSimple = New System.Windows.Forms.PictureBox
        Me.ContextMenuSimple = New System.Windows.Forms.ContextMenu
        Me.MenuSpecialise = New System.Windows.Forms.MenuItem
        Me.lblElement = New System.Windows.Forms.Label
        Me.lblElementOnly = New System.Windows.Forms.Label
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
        'ContextMenuSimple
        '
        Me.ContextMenuSimple.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuSpecialise})
        '
        'MenuSpecialise
        '
        Me.MenuSpecialise.Index = 0
        Me.MenuSpecialise.Text = "Specialise"
        '
        'lblElement
        '
        Me.lblElement.AutoSize = True
        Me.lblElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElement.Location = New System.Drawing.Point(110, 40)
        Me.lblElement.Name = "lblElement"
        Me.lblElement.Size = New System.Drawing.Size(70, 20)
        Me.lblElement.TabIndex = 40
        Me.lblElement.Text = "Element"
        '
        'lblElementOnly
        '
        Me.lblElementOnly.AutoSize = True
        Me.lblElementOnly.Location = New System.Drawing.Point(112, 40)
        Me.lblElementOnly.Name = "lblElementOnly"
        Me.lblElementOnly.Size = New System.Drawing.Size(59, 17)
        Me.lblElementOnly.TabIndex = 40
        Me.lblElementOnly.Text = "Element"
        '
        'SimpleStructure
        '
        Me.Controls.Add(Me.lblElement)
        Me.Controls.Add(Me.PictureBoxSimple)
        Me.Name = "SimpleStructure"
        Me.Controls.SetChildIndex(Me.PictureBoxSimple, 0)
        Me.Controls.SetChildIndex(Me.lblElement, 0)
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

    Public Shadows Property Archetype() As RmElement
        Get
            Return mElement.RM_Class
        End Get
        Set(ByVal Value As RmElement)
            mNodeId = Value.NodeId
            mLoading = True
            mElement = New ArchetypeElement(Value, mFileManager)
            Me.PictureBoxSimple.Image = Me.ilSmall.Images(Me.ImageIndexForConstraintType(mElement.Constraint.Type))
            mLoading = False
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

    Public Overrides Sub Reset()
        Me.PictureBoxSimple.Image = Nothing
    End Sub

    Public Overrides Sub Translate()
        mElement.Translate()
        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Protected Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs)
        If MessageBox.Show(String.Format("{0} {1}?", AE_Constants.Instance.Specialise, mCurrentItem.Text), AE_Constants.Instance.MessageBoxCaption, _
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            mElement.Specialise()
            Debug.Assert(False, "Needs to be tested")
            mFileManager.FileEdited = True
        End If
    End Sub

    Protected Overrides Sub AddReference(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Assert(False, "Cannot add a reference to a simple")
    End Sub

    Protected Overrides Sub SetUpAddElementMenu()
        mConstraintMenu.Show(ButAddElement, New System.Drawing.Point(5, 5))
    End Sub


    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        Dim temp As New RmElement(mFileManager.Archetype.ConceptCode)
        mElement = New ArchetypeElement(temp, mFileManager)
        mElement.Constraint = a_constraint
        mElement.Occurrences.MaxCount = 1
        Me.PictureBoxSimple.Image = Me.ilSmall.Images(Me.ImageIndexForConstraintType(mElement.Constraint.Type))
        mFileManager.FileEdited = True
        SetCurrentItem(Element)
        ButAddElement.Visible = False
    End Sub

    Protected Overrides Sub RemoveItemAndReferences(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Assert(False)
    End Sub

    ReadOnly Property Element() As ArchetypeElement
        Get
            Return mElement
        End Get
    End Property

    Public Overrides Function ToRichText(ByVal indentlevel As Integer, ByVal new_line As String) As String
        Return mElement.ToRichText(indentlevel)
    End Function

    Public Overrides Function ToHTML(ByVal BackGroundColour As String) As String
        Dim text As String = "<br>"

        text &= Environment.NewLine & "<table border=""1"" cellpadding=""2"" width=""100%"">"

        If BackGroundColour = "" Then
            text &= Environment.NewLine & "<tr>"
        Else
            text &= Environment.NewLine & "<tr  bgcolor=""" & BackGroundColour & """>"
        End If

        text &= Environment.NewLine & "<td width=""20%""><h4>" & Filemanager.GetOpenEhrTerm(54, "Concept") & "</h4></td>"
        text &= Environment.NewLine & "<td width = ""40%""><h4>" & Filemanager.GetOpenEhrTerm(113, "Description") & "</h4></td>"
        text &= Environment.NewLine & "<td width = ""20%""><h4>" & Filemanager.GetOpenEhrTerm(87, "Constraints") & "</h4></td>"
        text &= Environment.NewLine & "<td width=""20%""><h4>" & Filemanager.GetOpenEhrTerm(438, "Values") & "</h4></td>"
        text &= Environment.NewLine & "</tr>"

        text &= Environment.NewLine & mElement.ToHTML(0)
        text &= Environment.NewLine & "</tr>"

        text &= Environment.NewLine & "</table>"
        Return text
    End Function

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub butListdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub RefreshIcons()
        Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)
        Me.PictureBoxSimple.Image = Me.ilSmall.Images(Me.ImageIndexForConstraintType(element.Constraint.Type))
    End Sub

    Private Sub ContextMenuSimple_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuSimple.Popup
        Debug.Assert(ContextMenuSimple.MenuItems.Count = 2)
        ' show specialisation if appropriate

        Dim i As Integer = OceanArchetypeEditor.Instance.CountInString(mCurrentItem.RM_Class.NodeId, ".")

        If i < mFileManager.OntologyManager.NumberOfSpecialisations Then
            Me.MenuSpecialise.Text = AE_Constants.Instance.Specialise
            Me.MenuSpecialise.Visible = True
        Else
            MenuSpecialise.Visible = False
        End If

    End Sub


    Private Sub txtSimple_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Private Sub SimpleStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = Me.lblElement
        If (Not mElement Is Nothing) AndAlso (Not mElement.Constraint Is Nothing) Then
            SetCurrentItem(mElement)

        End If
    End Sub


#Region "Drag and Drop"

    Private Sub lblElement_DragDrop(ByVal sender As System.Object, _
    ByVal e As System.Windows.Forms.DragEventArgs)

        If Not mNewConstraint Is Nothing Then
            mElement = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New element"), mFileManager)
            mElement.Constraint = mNewConstraint
        Else
            Debug.Assert(False, "No item dragged")
            Return
        End If
        Me.PictureBoxSimple.Image = Me.ilSmall.Images(Me.ImageIndexForConstraintType(mElement.Constraint.Type))
        SetCurrentItem(mElement)

        mFileManager.FileEdited = True

        mNewConstraint = Nothing
    End Sub

    Private Sub txtSimple_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
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
