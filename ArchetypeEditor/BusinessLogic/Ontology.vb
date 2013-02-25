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

Public MustInherit Class Ontology
    Public MustOverride ReadOnly Property PrimaryLanguageCode() As String
    Public MustOverride ReadOnly Property LanguageCode() As String
    Public MustOverride ReadOnly Property NumberOfSpecialisations() As Integer
    Public MustOverride Sub Reset()
    Public MustOverride Function HasTermBinding(ByVal terminologyId As String, ByVal path As String) As Boolean
    Public MustOverride Function TermBinding(ByVal terminologyId As String, ByVal path As String) As String
    Public MustOverride Function HasConstraintBinding(ByVal erminologyId As String, ByVal path As String) As Boolean
    Public MustOverride Function ConstraintBinding(ByVal terminologyId As String, ByVal path As String) As String
    Public MustOverride Function LanguageAvailable(ByVal code As String) As Boolean
    Public MustOverride Function IsMultiLanguage() As Boolean
    Public MustOverride Function TerminologyAvailable(ByVal code As String) As Boolean
    Public MustOverride Sub SetLanguage(ByVal code As String)
    Public MustOverride Sub SetPrimaryLanguage(ByVal languageCode As String)
    Public MustOverride Function SpecialiseTerm(ByVal text As String, ByVal description As String, ByVal id As String) As RmTerm
    Public MustOverride Function NextTermId() As String
    Public MustOverride Function NextConstraintID() As String
    Public MustOverride Sub AddTerm(ByVal term As RmTerm)
    Public MustOverride Function HasTermCode(ByVal termCode As String) As Boolean
    Public MustOverride Sub ReplaceTerm(ByVal term As RmTerm, ByVal ReplaceTranslations As Boolean)
    Public MustOverride Sub AddConstraint(ByVal term As RmTerm)
    Public MustOverride Sub AddLanguage(ByVal languageCode As String)
    Public MustOverride Sub RemoveTermBinding(ByVal terminology As String, ByVal code As String)
    Public MustOverride Sub RemoveConstraintBinding(ByVal terminology As String, ByVal code As String)
    Public MustOverride Sub AddorReplaceTermBinding(ByVal terminology As String, ByVal path As String, ByVal code As String, ByVal release As String)
    Public MustOverride Sub AddorReplaceConstraintBinding(ByVal terminology As String, ByVal code As String, ByVal query As String)
    Public MustOverride Function TermForCode(ByVal code As String, ByVal languageCode As String) As RmTerm
    Public MustOverride Sub PopulateAllTerms(ByRef ontologyManager As OntologyManager)
    Public MustOverride Sub PopulateTermsInLanguage(ByRef ontologyManager As OntologyManager, ByVal languageCode As String)
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
'The Original Code is Ontology.vb.
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
