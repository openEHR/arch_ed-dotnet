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

    Sub New(ByVal an_element As RmElement, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(an_element, a_file_manager)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mLoading = True

        If Not an_element Is Nothing Then
            mElement = New ArchetypeElement(an_element, mFileManager)

            If Not mElement.Constraint Is Nothing Then
                PictureBoxSimple.Image = ilSmall.Images(mElement.Constraint.ImageIndexForConstraintKind(False, False))
            Else
                ButAddElement.Show()
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
    Friend WithEvents SpecialiseMenuItem As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBoxSimple = New System.Windows.Forms.PictureBox
        Me.ContextMenuSimple = New System.Windows.Forms.ContextMenu
        Me.SpecialiseMenuItem = New System.Windows.Forms.MenuItem
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
        Me.ContextMenuSimple.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.SpecialiseMenuItem})
        '
        'MenuSpecialise
        '
        Me.SpecialiseMenuItem.Index = 0
        Me.SpecialiseMenuItem.Text = "Specialise"
        '
        'lblElement
        '
        Me.lblElement.AutoSize = True
        Me.lblElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElement.Location = New System.Drawing.Point(110, 40)
        Me.lblElement.Name = "lblElement"
        Me.lblElement.Size = New System.Drawing.Size(59, 17)
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
        'ElementOnly
        '
        Me.ContextMenu = Me.ContextMenuSimple
        Me.Controls.Add(Me.lblElement)
        Me.Controls.Add(Me.PictureBoxSimple)
        Me.Name = "ElementOnly"
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

    Public Overrides Property Archetype() As RmStructure
        Get
            Return mElement.RM_Class
        End Get
        Set(ByVal value As RmStructure)
            mNodeId = value.NodeId
            mLoading = True
            mElement = New ArchetypeElement(value, mFileManager)
            PictureBoxSimple.Image = ilSmall.Images(mElement.Constraint.ImageIndexForConstraintKind(False, False))
            mLoading = False
            mFileManager.FileEdited = True
            SetCurrentItem(mElement)
        End Set
    End Property

    Public Overrides Sub Reset()
        PictureBoxSimple.Image = Nothing
    End Sub

    Public Overrides Sub Translate()
        mElement.Translate()
        'call base translate to raise event to refresh constraint display
        MyBase.Translate()
    End Sub

    Public Overrides Sub SpecialiseCurrentItem(ByVal sender As Object, ByVal e As EventArgs) Handles SpecialiseMenuItem.Click
        Dim dlg As New SpecialisationQuestionDialog()
        dlg.ShowForArchetypeNode(Element.Text, Element.RM_Class, SpecialisationDepth)

        If dlg.IsSpecialisationRequested Then
            Element.Specialise()
            SetCurrentItem(Element)
            mFileManager.FileEdited = True
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
            SetCurrentItem(mElement) ' does not raise an event during construction
        End If
    End Sub

    Protected Overrides Sub AddNewElement(ByVal a_constraint As Constraint)
        Dim temp As New RmElement(mFileManager.Archetype.ConceptCode)
        mElement = New ArchetypeElement(temp, mFileManager)
        mElement.Constraint = a_constraint
        mElement.Occurrences.MaxCount = 1
        PictureBoxSimple.Image = ilSmall.Images(mElement.Constraint.ImageIndexForConstraintKind(False, False))
        mFileManager.FileEdited = True
        SetCurrentItem(Element)
        ButAddElement.Hide()
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
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("<br>")
        Dim showComments As Boolean = Main.Instance.Options.ShowCommentsInHtml

        result.AppendFormat("{0}<table border=""1"" cellpadding=""2"" width=""100%"">", Environment.NewLine)
        result.AppendFormat(HtmlHeader(BackGroundColour, showComments))
        result.AppendFormat("{0}{1}", Environment.NewLine, mElement.ToHTML(0, showComments))
        result.AppendFormat("{0}</tr>", Environment.NewLine)
        result.AppendFormat("{0}</table>", Environment.NewLine)
        Return result.ToString
    End Function

    Public Overrides Function ItemCount() As Integer
        Return IIf(mElement Is Nothing, 0, 1)
    End Function

    Protected Overrides Sub butListUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub butListdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'nothing for this class
    End Sub

    Protected Overrides Sub RefreshIcons()
        Dim element As ArchetypeElement = CType(mCurrentItem, ArchetypeElement)
        PictureBoxSimple.Image = ilSmall.Images(element.Constraint.ImageIndexForConstraintKind(False, False))
    End Sub

    Private Sub ContextMenuSimple_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuSimple.Popup
        ' show specialisation if appropriate

        SpecialiseMenuItem.Visible = False

        If Not mCurrentItem Is Nothing Then
            If mCurrentItem.RM_Class.SpecialisationDepth < SpecialisationDepth Then
                SpecialiseMenuItem.Text = AE_Constants.Instance.Specialise
                SpecialiseMenuItem.Visible = True
            End If
        End If
    End Sub

    Private Sub SimpleStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set the variable in the base class
        mControl = Me.lblElement

        If Not mElement Is Nothing AndAlso Not mElement.Constraint Is Nothing Then
            SetCurrentItem(mElement)
        End If
    End Sub

#Region "Drag and Drop"

    Private Sub lblElement_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)
        If Not mNewConstraint Is Nothing Then
            mElement = New ArchetypeElement(Filemanager.GetOpenEhrTerm(109, "New element"), mFileManager)
            mElement.Constraint = mNewConstraint
            PictureBoxSimple.Image = ilSmall.Images(mElement.Constraint.ImageIndexForConstraintKind(False, False))
            SetCurrentItem(mElement)
            mFileManager.FileEdited = True
            mNewConstraint = Nothing
        Else
            Debug.Assert(False, "No item dragged")
        End If
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
