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

Public Class RmElement
    Inherits RmStructure
    Private colReferences As New System.Collections.Specialized.StringCollection
    Protected cConstraint As Constraint
    Protected boolIsReference As Boolean
    Protected boolHasReferences As Boolean

    Public ReadOnly Property isReference() As Boolean
        Get
            Return boolIsReference
        End Get
    End Property
    Public Overridable Property hasReferences() As Boolean
        Get
            Return boolHasReferences
        End Get
        Set(ByVal Value As Boolean)
            boolHasReferences = Value
        End Set
    End Property
    Public Overrides ReadOnly Property Type() As StructureType
        Get
            Return StructureType.Element
        End Get
    End Property

    Public Overridable ReadOnly Property DataType() As String
        Get
            Return cConstraint.Type.ToString
        End Get
    End Property
    Public Overridable Property Constraint() As Constraint
        Get
            Return cConstraint
        End Get
        Set(ByVal Value As Constraint)
            cConstraint = Value
        End Set
    End Property

    Public Overrides Function Copy() As RmStructure
        Dim ae As New RmElement(Me.NodeId)
        ' Also copies if it is a reference but no longer leaves it as a reference
        ' Used in specialisation of archetypes
        ae.cOccurrences = Me.Occurrences.Copy
        ae.cConstraint = Me.Constraint.copy
        ae.sNodeId = Me.NodeId
        If Not mRunTimeConstraint Is Nothing Then
            ae.mRunTimeConstraint = CType(Me.mRunTimeConstraint.copy, Constraint_Text)
        End If
        Return ae
    End Function

    Sub New(ByVal e As RmElement)
        MyBase.New(e)
        ' for reference
    End Sub
    Sub New(ByVal NodeId As String)
        MyBase.New(NodeId, StructureType.Element)
    End Sub

    Sub New(ByVal XML_Element As XMLParser.C_COMPLEX_OBJECT)
        MyBase.New(XML_Element)
    End Sub

    Sub New(ByVal EIF_Element As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(EIF_Element)
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
'The Original Code is RmElement.vb.
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
