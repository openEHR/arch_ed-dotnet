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

Option Strict On

Public MustInherit Class ArchetypeNodeAbstract
    Inherits ArchetypeNode

    Protected mFileManager As FileManagerLocal

    Public Overrides Property Text() As String
        Get
            Return mFileManager.OntologyManager.GetTerm(NodeId).Text
        End Get
        Set(ByVal Value As String)
            mFileManager.OntologyManager.SetText(Value, NodeId)
            mFileManager.FileEdited = True
        End Set
    End Property

    Public Property Description() As String
        Get
            Return mFileManager.OntologyManager.GetTerm(NodeId).Description
        End Get
        Set(ByVal Value As String)
            mFileManager.OntologyManager.SetDescription(Value, NodeId)
            mFileManager.FileEdited = True
        End Set
    End Property

    Public Property Comment() As String
        Get
            Return mFileManager.OntologyManager.GetTerm(NodeId).Comment
        End Get
        Set(ByVal Value As String)
            mFileManager.OntologyManager.SetComment(Value, NodeId)
            mFileManager.FileEdited = True
        End Set
    End Property

    Public ReadOnly Property Annotations() As System.Collections.SortedList
        Get
            Return mFileManager.OntologyManager.GetTerm(NodeId).OtherAnnotations
        End Get
    End Property

    Public ReadOnly Property RuntimeNameText() As String
        Get
            Dim result As String = ""

            If Item.HasNameConstraint Then
                result = Item.NameConstraint.ToString
            End If

            Return result
        End Get
    End Property

    Public ReadOnly Property NameConstraint() As Constraint_Text
        Get
            Return mItem.NameConstraint
        End Get
    End Property

    Public ReadOnly Property NodeId() As String
        Get
            Return mItem.NodeId
        End Get
    End Property

    Public Overridable Sub Specialise()
        Item = Item.Copy
        mItem.NodeId = mFileManager.OntologyManager.SpecialiseTerm("! - " & Text, Description, NodeId).Code

        If mItem.HasNameConstraint AndAlso mItem.NameConstraint.TypeOfTextConstraint = TextConstraintType.Terminology Then
            mItem.NameConstraint.ConstraintCode = mFileManager.OntologyManager.SpecialiseNameConstraint(mItem.NameConstraint.ConstraintCode).Code
        End If

        mFileManager.FileEdited = True
    End Sub

    Public Overrides Function ToString() As String
        Return Text
    End Function

    Public Overrides ReadOnly Property CanRemove() As Boolean
        Get
            Return Item.CanRemove(mFileManager.OntologyManager.NumberOfSpecialisations)
        End Get
    End Property

    Public Overrides ReadOnly Property CanSpecialise() As Boolean
        Get
            Return Item.CanSpecialise(mFileManager.OntologyManager.NumberOfSpecialisations)
        End Get
    End Property

    Sub New(ByVal item As RmStructure, ByVal fileManager As FileManagerLocal)
        mItem = item
        mFileManager = fileManager
    End Sub

    Sub New(ByVal node As ArchetypeNodeAbstract)
        mItem = node.Item.Copy
        mFileManager = node.mFileManager
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
