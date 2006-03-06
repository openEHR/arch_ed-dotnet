'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'


Public MustInherit Class Archetype
    'Protected adlEngine As openehr.openehr.am.archetype.constraint_model.AdlEngine

    Protected cDefinition As ArchetypeDefinition
    Protected mDescription As New ArchetypeDescription
    Protected sParentArchetypeID As String
    Protected sConceptCode As New String("at0000")
    Protected sPrimaryLanguageCode As String
    Protected sReferenceModelOriginator As New String("?")
    Protected sLifeCycle As New String("draft")
    Protected iVersion As Integer = 1
    Protected WithEvents mArchetypeID As ArchetypeID

    MustOverride Property LifeCycle() As String
    MustOverride Property ConceptCode() As String
    MustOverride Property Archetype_ID() As ArchetypeID
    MustOverride ReadOnly Property ArchetypeAvailable() As Boolean
    MustOverride ReadOnly Property Paths(ByVal languageCode As String, Optional ByVal Logical As Boolean = False) As String()

    MustOverride ReadOnly Property SourceCode() As String
    MustOverride ReadOnly Property SerialisedArchetype(ByVal a_format As String) As String
    MustOverride Sub Specialise(ByVal NewConceptShortName As String, ByRef The_Ontology As OntologyManager)
    Public ReadOnly Property RmEntity() As StructureType
        Get
            Return mArchetypeID.ReferenceModelEntity
        End Get
    End Property
    Public Property Description() As ArchetypeDescription
        Get
            Return mDescription
        End Get
        Set(ByVal Value As ArchetypeDescription)
            mDescription = Value
        End Set
    End Property
    Public ReadOnly Property RmType() As ReferenceModelType
        Get
            Return mArchetypeID.Reference_Model
        End Get
        'Set(ByVal Value As ReferenceModelType)
        '    Debug.Assert(False)
        '    'will need a whole rule engine to do this
        'End Set
    End Property
    Public Overridable Property ParentArchetype() As String
        Get
            Return sParentArchetypeID
        End Get
        Set(ByVal Value As String)
            sParentArchetypeID = Value
        End Set
    End Property
    Public ReadOnly Property hasData() As Boolean
        Get
            Return Not cDefinition Is Nothing
        End Get
    End Property
    Public Property Version() As Integer
        Get
            Return iVersion
        End Get
        Set(ByVal Value As Integer)
            iVersion = Value
        End Set
    End Property
    Public Property Definition() As ArchetypeDefinition
        Get
            Return cDefinition
        End Get
        Set(ByVal value As ArchetypeDefinition)
            cDefinition = value
        End Set
    End Property
    Public Property Extendable() As Boolean
        Get
            'FIXME - can it be specialised?
        End Get
        Set(ByVal Value As Boolean)
            'FIXME - set the extensibility
        End Set
    End Property

    Protected Sub setDefinition()
        ' lets each specialised archetype type set the entity
        ' in the GUI archetype
        Select Case mArchetypeID.ReferenceModelEntity
            Case StructureType.COMPOSITION
                cDefinition = New RmComposition
            Case StructureType.SECTION
                cDefinition = New RmSection("?")
            Case StructureType.ENTRY, StructureType.OBSERVATION, _
                StructureType.INSTRUCTION, StructureType.EVALUATION, _
                StructureType.ACTION, StructureType.ADMIN_ENTRY
                cDefinition = New RmEntry(mArchetypeID.ReferenceModelEntity)
            Case StructureType.Single, StructureType.List, StructureType.Tree
                cDefinition = New RmStructureCompound("?", mArchetypeID.ReferenceModelEntity)
            Case StructureType.Table
                cDefinition = New RmTable("?")
        End Select
        ReferenceModel.Instance.ArchetypedClass = mArchetypeID.ReferenceModelEntity
    End Sub

    Public Sub ResetDefinitions()
        Debug.Assert(Not cDefinition Is Nothing)
        If Not cDefinition Is Nothing Then
            cDefinition.Data.Clear()
        End If
    End Sub

    Sub New(ByVal Primary_Language As String)
        sPrimaryLanguageCode = Primary_Language
    End Sub

    Sub New(ByVal Primary_Language As String, ByVal an_archetypeID As ArchetypeID)
        sPrimaryLanguageCode = Primary_Language
        mArchetypeID = an_archetypeID
        setDefinition()
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
'The Original Code is Archetype.vb.
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
