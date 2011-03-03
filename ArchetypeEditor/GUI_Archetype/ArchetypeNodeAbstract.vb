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

Option Strict On

Public MustInherit Class ArchetypeNodeAbstract
    Implements ArchetypeNode

    Protected mFileManager As FileManagerLocal

    Protected mText As String
    Protected mDescription As String
    Protected mComment As String
    Protected mAnnotations As New System.Collections.SortedList
    Protected mItem As RmStructure

    Public Overridable Property Text() As String Implements ArchetypeNode.Text
        Get
            Return mText
        End Get
        Set(ByVal Value As String)
            mText = Value
            mFileManager.OntologyManager.SetText(Value, mItem.NodeId)
            mFileManager.FileEdited = True
        End Set
    End Property
    Public Shadows ReadOnly Property RM_Class() As RmStructure Implements ArchetypeNode.RM_Class
        Get
            Return mItem
        End Get
    End Property
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal Value As String)
            mDescription = Value
            mFileManager.OntologyManager.SetDescription(Value, mItem.NodeId)
            mFileManager.FileEdited = True
        End Set
    End Property
    Public Property Comment() As String
        Get
            Return mComment
        End Get
        Set(ByVal Value As String)
            mComment = Value
            mFileManager.OntologyManager.SetComment(Value, mItem.NodeId)
            mFileManager.FileEdited = True
        End Set
    End Property
    Public ReadOnly Property Annotations() As System.Collections.SortedList
        Get
            Return mAnnotations
        End Get
    End Property

    Public ReadOnly Property RuntimeNameText() As String
        Get
            If Item.HasNameConstraint Then
                Return Item.NameConstraint.ToString
            Else
                Return ""
            End If
        End Get
    End Property
    Public ReadOnly Property NameConstraint() As Constraint_Text
        Get
            Return mItem.NameConstraint
        End Get
    End Property

    Public MustOverride ReadOnly Property IsReference() As Boolean Implements ArchetypeNode.IsReference
    Public MustOverride ReadOnly Property HasReferences() As Boolean Implements ArchetypeNode.HasReferences

    Public Property Occurrences() As RmCardinality Implements ArchetypeNode.Occurrences
        Get
            Return mItem.Occurrences
        End Get
        Set(ByVal Value As RmCardinality)
            mItem.Occurrences = Value
        End Set
    End Property
    Public ReadOnly Property IsMandatory() As Boolean Implements ArchetypeNode.IsMandatory
        Get
            Return (mItem.Occurrences.MinCount > 0)
        End Get
    End Property
    Public ReadOnly Property NodeId() As String
        Get
            Return mItem.NodeId
        End Get
    End Property
    Public ReadOnly Property isAnonymous() As Boolean Implements ArchetypeNode.IsAnonymous
        Get
            Return False
        End Get
    End Property

    Private Overloads Function CopyArchetypeNode() As ArchetypeNode
        Return Me.Copy
    End Function

    Public MustOverride Function Copy() As ArchetypeNode Implements ArchetypeNode.Copy

    Public MustOverride Function ToRichText(ByVal level As Integer) As String Implements ArchetypeNode.ToRichText

    Public MustOverride Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String Implements ArchetypeNode.ToHTML

    Public Sub Translate() Implements ArchetypeNode.Translate
        Dim aTerm As RmTerm

        aTerm = mFileManager.OntologyManager.GetTerm(mItem.NodeId)
        mText = aTerm.Text
        mDescription = aTerm.Description
        mComment = aTerm.Comment
    End Sub

    Public Sub Specialise()
        Dim aTerm As RmTerm

        mText = "! - " & mText
        aTerm = mFileManager.OntologyManager.SpecialiseTerm(mText, mDescription, mItem.NodeId)
        mItem.NodeId = aTerm.Code
        ' if there is a constraint on the runtime name this will
        ' have to be specialised as well
        If mItem.HasNameConstraint Then
            If mItem.NameConstraint.TypeOfTextConstraint = TextConstrainType.Terminology Then
                aTerm = mFileManager.OntologyManager.SpecialiseTerm(mItem.NameConstraint.ConstraintCode)
                mItem.NameConstraint.ConstraintCode = aTerm.Code
            End If
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return mText
    End Function

    Protected Property Item() As RmStructure
        Get
            Return mItem
        End Get
        Set(ByVal Value As RmStructure)
            mItem = Value
        End Set
    End Property

    Protected Sub New(ByVal aText As String)
        mText = aText
        mDescription = "*"
    End Sub

    Sub New(ByVal aItem As RmStructure, ByVal a_file_manager As FileManagerLocal)
        mItem = aItem
        mFileManager = a_file_manager

        Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(aItem.NodeId)
        mText = aTerm.Text
        mDescription = aTerm.Description
        mComment = aTerm.Comment
        mAnnotations = aTerm.OtherAnnotations
    End Sub

    Sub New(ByVal a_node As ArchetypeNodeAbstract)
        mFileManager = a_node.mFileManager
        mItem = a_node.mItem.Copy
        Dim aTerm As RmTerm = mFileManager.OntologyManager.GetTerm(a_node.NodeId)
        mText = aTerm.Text
        mDescription = aTerm.Description
        mComment = aTerm.Comment
        mAnnotations = aTerm.OtherAnnotations
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
'The Original Code is ArchetypeNodeAbstract.vb.
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
