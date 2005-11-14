'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source$"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Option Strict On

Public Class ArchetypeListViewItem : Inherits ListViewItem

    Private mArchetypeElement As ArchetypeElement

    Public ReadOnly Property Item() As ArchetypeElement
        Get
            Debug.Assert(Not mArchetypeElement Is Nothing)
            Return mArchetypeElement
        End Get
    End Property
    Public Shadows Property Text() As String
        Get
            Return mArchetypeElement.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Value
            mArchetypeElement.Text = Value
        End Set
    End Property

    'Private ReadOnly Property RuntimeNameText() As String
    '    Get
    '        Return mArchetypeElement.RuntimeNameText
    '    End Get
    'End Property

    'Private ReadOnly Property IsReference() As Boolean
    '    Get
    '        Return mArchetypeElement.IsReference
    '    End Get
    'End Property

    'Private ReadOnly Property HasReferences() As Boolean
    '    Get
    '        Return mArchetypeElement.HasReferences
    '    End Get
    'End Property

    'Private Property Description() As String
    '    Get
    '        Return mArchetypeElement.Description
    '    End Get
    '    Set(ByVal Value As String)
    '        mArchetypeElement.Description = Value
    '    End Set
    'End Property

    'Private ReadOnly Property RM_Class() As RmStructure
    '    Get
    '        Return mArchetypeElement.RM_Class
    '    End Get
    'End Property

    'Private Property Constraint() As Constraint
    '    Get
    '        Return mArchetypeElement.Constraint
    '    End Get
    '    Set(ByVal Value As Constraint)
    '        mArchetypeElement.Constraint = Value
    '    End Set
    'End Property

    'Private Property Occurrences() As RmCardinality
    '    Get
    '        Return mArchetypeElement.Occurrences
    '    End Get
    '    Set(ByVal Value As RmCardinality)
    '        mArchetypeElement.Occurrences = Value
    '    End Set
    'End Property

    'Private ReadOnly Property NodeId() As String
    '    Get
    '        Return mArchetypeElement.NodeId
    '    End Get
    'End Property

    'Private ReadOnly Property TypeName() As String
    '    Get
    '        '' has to be an element as it is in a list
    '        'Return "mElement"
    '        Return mArchetypeElement.RM_Class.Type.ToString
    '    End Get
    'End Property

    'Private ReadOnly Property DataType() As String
    '    Get
    '        Return mArchetypeElement.DataType
    '    End Get
    'End Property

    Public Sub Translate()
        mArchetypeElement.Translate()
        MyBase.Text = mArchetypeElement.Text
    End Sub

    Public Function Copy() As ArchetypeListViewItem
        Return New ArchetypeListViewItem(Me.Item)
    End Function

    Public Sub Specialise()
        mArchetypeElement.Specialise()
        MyBase.Text = mArchetypeElement.Text
    End Sub


    Sub New(ByVal aText As String, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(aText)
        mArchetypeElement = New ArchetypeElement(aText, a_file_manager)
    End Sub

    Sub New(ByVal el As RmElement, ByVal a_file_manager As FileManagerLocal)
        MyBase.New()
        'Must call translate to get the text
        Dim aTerm As RmTerm = a_file_manager.OntologyManager.GetTerm(el.NodeId)
        MyBase.Text = aTerm.Text

        mArchetypeElement = New ArchetypeElement(el, a_file_manager)

    End Sub

    Sub New(ByVal el As ArchetypeElement)
        MyBase.New(el.Text)

        mArchetypeElement = el

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
'The Original Code is ArchetypeListViewItem.vb.
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