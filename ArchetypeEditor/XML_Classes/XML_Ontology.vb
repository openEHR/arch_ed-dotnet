'
'
'	component:   "openEHR Archetype Project"
'	description: "Specialisation of the Ontology class to work with ADL"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'
Option Strict On
Option Explicit On 

Namespace ArchetypeEditor.XML_Classes
    Friend Class XML_Ontology
        Inherits Ontology
        Private archetypeParser As XMLParser.XmlArchetypeParser

        Public Overrides ReadOnly Property PrimaryLanguageCode() As String
            Get
                Return archetypeParser.Archetype.original_language.code_string
            End Get
        End Property
        Public Overrides ReadOnly Property LanguageCode() As String
            Get
                Return archetypeParser.Ontology.LanguageCode
            End Get
        End Property

        Public Overrides ReadOnly Property NumberOfSpecialisations() As Integer
            Get
                Return archetypeParser.Ontology.NumberOfSpecialisations
            End Get
        End Property

        Public Overrides Function LanguageAvailable(ByVal code As String) As Boolean
            Return archetypeParser.Ontology.LanguageAvailable(code)
        End Function

        Public Overrides Function TerminologyAvailable(ByVal code As String) As Boolean
            Return archetypeParser.Ontology.TerminologyAvailable(code)
        End Function

        Public Overrides Function TermForCode(ByVal code As String, ByVal language As String) As RmTerm
            If code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("at") Then
                If archetypeParser.Ontology.HasTermCode(code) Then
                    Return New XML_Classes.XML_Term(archetypeParser.Ontology.TermDefinition(language, code))
                End If
            ElseIf code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("ac") Then
                If archetypeParser.Ontology.HasTermCode(code) Then
                    Return New XML_Classes.XML_Term(archetypeParser.Ontology.ConstraintDefinition(language, code))
                End If
            Else
                Debug.Assert(False, "Code type is not available")
            End If

            Return Nothing
        End Function

        Public Overrides Function IsMultiLanguage() As Boolean
            Return archetypeParser.Ontology.IsMultiLanguage
        End Function

        Public Overrides Sub Reset()
            ' no action required
        End Sub

        Public Overrides Sub AddLanguage(ByVal code As String)
            archetypeParser.Ontology.AddLanguage(code)
        End Sub

        Public Overrides Function HasTermBinding(ByVal terminologyId As String, ByVal path As String) As Boolean
            Return archetypeParser.Ontology.HasTermBinding(terminologyId, path)
        End Function

        Public Overrides Function HasConstraintBinding(ByVal terminologyId As String, ByVal path As String) As Boolean
            Return archetypeParser.Ontology.HasConstraintBinding(terminologyId, path)
        End Function

        Public Overrides Function TermBinding(ByVal terminologyId As String, ByVal path As String) As String
            Return archetypeParser.Ontology.TermBinding(terminologyId, path)
        End Function

        Public Overrides Function ConstraintBinding(ByVal terminologyId As String, ByVal path As String) As String
            Return archetypeParser.Ontology.ConstraintBinding(terminologyId, path)
        End Function

        Public Overrides Sub AddorReplaceTermBinding(ByVal terminologyId As String, ByVal archetypePath As String, ByVal sCode As String, ByVal sRelease As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(archetypePath <> "", "Path or nodeID are not set")
            Debug.Assert(terminologyId <> "", "TerminologyID is not set")

            Try
                Dim terminology_idValue As String = terminologyId

                If Not String.IsNullOrEmpty(sRelease) Then
                    terminology_idValue += "(" + sRelease + ")"
                End If

                archetypeParser.Ontology.AddOrReplaceTermBinding(sCode, archetypePath, terminologyId, terminology_idValue)
            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveTermBinding(ByVal terminologyId As String, ByVal archetypePath As String)
            Debug.Assert(archetypePath <> "", "Code is not set")
            Debug.Assert(terminologyId <> "", "TerminologyID is not set")

            Try
                If archetypeParser.Ontology.HasTermBinding(terminologyId, archetypePath) Then
                    archetypeParser.Ontology.RemoveTermBinding(archetypePath, terminologyId)
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub AddorReplaceConstraintBinding(ByVal terminologyId As String, ByVal acCode As String, ByVal query As String)
            Debug.Assert(acCode <> "", "Code is not set")
            Debug.Assert(query <> "", "Query is not set")
            Debug.Assert(terminologyId <> "", "TerminologyID is not set")

            Try
                archetypeParser.Ontology.AddOrReplaceConstraintBinding(query, acCode, terminologyId)
            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveConstraintBinding(ByVal terminologyid As String, ByVal query As String)
            Debug.Assert(query <> "", "Code is not set")
            Debug.Assert(terminologyid <> "", "TerminologyID is not set")

            Try
                If archetypeParser.Ontology.HasConstraintBinding(terminologyid, query) Then
                    archetypeParser.Ontology.RemoveConstraintBinding(query, terminologyid)
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML Parser error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub SetLanguage(ByVal language As String)
            archetypeParser.Ontology.SetLanguage(language)
        End Sub

        Public Overrides Function SpecialiseTerm(ByVal text As String, ByVal description As String, ByVal id As String) As RmTerm
            ' increase the number of specialisations
            Dim result As New XML_Term(NextSpecialisedId(id))
            result.Text = text
            result.Description = description
            AddTerm(result)
            Return result
        End Function

        Public Overrides Sub SetPrimaryLanguage(ByVal language As String)
            ' sets the primary language of this archetype
            ' if this language is not in the available languages it adds it

            If language <> "" Then
                archetypeParser.Ontology.SetPrimaryLanguage(language)
            End If
        End Sub

        Public Overrides Function NextTermId() As String
            Dim result As String = ""

            Try
                result = archetypeParser.Ontology.NextTermId()
            Catch e As Exception
                Debug.Assert(False, e.Message)
            End Try

            Return result
        End Function

        Public Overrides Function NextConstraintId() As String
            Return archetypeParser.Ontology.NextConstraintId()
        End Function

        Private Function NextSpecialisedId(ByVal parentCode As String) As String
            Return archetypeParser.Ontology.NextSpecialisedTermId(parentCode)
        End Function

        Public Overrides Sub AddTerm(ByVal term As RmTerm)
            Try
                If Not archetypeParser.Ontology.HasTermCode(term.Code) Then
                    archetypeParser.Ontology.AddTermOrConstraintDefinition(LanguageCode, New XML_Term(term).XML_Term, False)
                Else
                    Debug.Assert(False)
                End If
            Catch e As Exception
                Debug.Assert(False, e.ToString)
            End Try
        End Sub

        Public Overrides Sub ReplaceTerm(ByVal term As RmTerm, ByVal replaceTranslations As Boolean)
            If archetypeParser.Ontology.HasTermCode(term.Code) Then
                Dim language As String = term.Language

                If language = "" Then
                    language = LanguageCode
                End If

                archetypeParser.Ontology.ReplaceTermDefinition(language, New XML_Term(term).XML_Term, replaceTranslations)
            Else
                Debug.Assert(False, "Term or constraint code is not available: " & term.Code)
            End If
        End Sub

        Public Overrides Function HasTermCode(ByVal termCode As String) As Boolean
            If RmTerm.IsValidTermCode(termCode) Then
                Return archetypeParser.Ontology.HasTermCode(termCode)
            End If
        End Function

        Public Overrides Sub AddConstraint(ByVal term As RmTerm)
            If term.IsConstraint Then
                Try
                    If Not archetypeParser.Ontology.HasTermCode(term.Code) Then
                        archetypeParser.Ontology.AddTermOrConstraintDefinition(LanguageCode, New XML_Term(term).XML_Term, False)
                    Else
                        Debug.Assert(False, "Constraint code not available: " & term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Code is not a constraint code: " & term.Code)
            End If
        End Sub

        Public Sub AddTermDefinitionsFromTable(ByVal table As DataTable)
            For Each row As DataRow In table.Rows
                Dim term As New XML_Term(CType(row(5), RmTerm))
                archetypeParser.Ontology.AddTermOrConstraintDefinition(CStr(row(0)), term.XML_Term, True)
            Next
        End Sub

        Public Sub AddTermBindingsFromTable(ByVal termBindingTable As System.Data.DataTable)
            For Each row As DataRow In termBindingTable.Rows
                '                                                terminology id        code                path
                'archetypeParser.Ontology.AddOrReplaceTermBinding(CStr(data_row(2)), CStr(data_row(1)), CStr(data_row(0)))
                Dim terminology_idValue As String = CStr(row(0))

                If Not row(3) Is Nothing AndAlso CStr(row(3)) <> "" Then
                    terminology_idValue += "(" + CStr(row(3)) + ")"
                End If

                archetypeParser.Ontology.AddOrReplaceTermBinding( _
                    CStr(row(2)), CStr(row(1)), CStr(row(0)), terminology_idValue)
                '   code_string,       archetype path,    terminology key,   terminology_id/value    
            Next
        End Sub

        Public Sub AddConstraintBindingsFromTable(ByVal constraintBindingTable As DataTable)
            For Each row As DataRow In constraintBindingTable.Rows
                archetypeParser.Ontology.AddOrReplaceConstraintBinding(CStr(row(4)), CStr(row(1)), CStr(row(0)))
            Next
        End Sub

        Private Sub PopulateLanguages(ByRef ontologyManager As OntologyManager)
            If archetypeParser.Ontology.AvailableLanguages.Count > 0 Then
                'A new ontology always adds the current language - but this may not be available in the archetype, so clear ..
                ontologyManager.LanguagesTable.Clear()

                For Each language As String In archetypeParser.Ontology.AvailableLanguages
                    ontologyManager.AddLanguage(language)
                Next
            End If
        End Sub

        Private Sub PopulateTerminologies(ByRef ontologyManager As OntologyManager)
            ' populate the terminology table in TermLookUp
            For Each terminologyId As String In archetypeParser.Ontology.AvailableTerminologies
                ontologyManager.AddTerminology(terminologyId)
            Next
        End Sub

        Private Sub PopulateTermDefinitions(ByVal table As DataTable, ByVal definitions As XMLParser.CodeDefinitionSet(), ByVal language As String)
            ' populate the TermDefinitions table in TermLookUp
            If Not definitions Is Nothing Then
                For Each definition As XMLParser.CodeDefinitionSet In definitions
                    If language = "" Or definition.language = language Then
                        ' set the term for all languages
                        If Not definition Is Nothing AndAlso Not definition.items Is Nothing Then
                            For Each termDef As XMLParser.ARCHETYPE_TERM In definition.items
                                Dim term As XML_Classes.XML_Term = New XML_Classes.XML_Term(termDef)
                                Dim row As DataRow = table.NewRow
                                row(0) = definition.language
                                row(1) = term.Code
                                row(2) = term.Text
                                row(3) = term.Description
                                row(4) = term.Comment
                                row(5) = term
                                ' add it to the GUI ontology
                                table.Rows.Add(row)
                            Next
                        End If
                    End If
                Next
            End If
        End Sub

        Private Sub PopulateTermBindings(ByRef ontologyManager As OntologyManager)
            ' populate the TermBindings table in TermLookUp
            Dim bindings As XMLParser.TermBindingSet() = archetypeParser.Archetype.ontology.term_bindings

            If Not bindings Is Nothing AndAlso bindings.Length > 0 Then
                For Each terminologyRow As DataRow In ontologyManager.TerminologiesTable.Rows
                    Dim terminology As String = CStr(terminologyRow(0))

                    If archetypeParser.Ontology.TerminologyAvailable(terminology) Then
                        Dim bindingSet As XMLParser.TermBindingSet = archetypeParser.Ontology.GetBindings(terminology, bindings)

                        If Not bindingSet Is Nothing AndAlso Not bindingSet.items Is Nothing Then
                            Dim table As DataTable = ontologyManager.TermBindingsTable

                            For Each bind As XMLParser.TERM_BINDING_ITEM In bindingSet.items
                                Dim row As DataRow = table.NewRow
                                row(0) = terminologyRow(0)
                                row(1) = bind.code

                                If Not bind.value Is Nothing Then
                                    If Not bind.value.terminology_id Is Nothing Then
                                        Dim strings() As String = bind.value.terminology_id.value.Split("("c)

                                        If strings.Length > 1 Then
                                            Dim release As String = strings(1).TrimEnd(")"c)
                                            row(3) = release
                                        End If
                                    End If

                                    row(2) = bind.value.code_string
                                End If

                                table.Rows.Add(row)
                            Next
                        End If
                    End If
                Next
            End If
        End Sub

        Private Sub PopulateConstraintBindings(ByRef ontologyManager As OntologyManager)
            ' populate the ConstraintBindings table in TermLookUp
            Dim bindings As XMLParser.ConstraintBindingSet() = archetypeParser.Archetype.ontology.constraint_bindings

            If Not bindings Is Nothing AndAlso bindings.Length > 0 Then
                For Each terminologyRow As DataRow In ontologyManager.TerminologiesTable.Rows
                    Dim terminology As String = CStr(terminologyRow(0))

                    If archetypeParser.Ontology.TerminologyAvailable(terminology) Then
                        Dim bindingSet As XMLParser.ConstraintBindingSet = archetypeParser.Ontology.GetBindings(terminology, bindings)

                        If Not bindingSet Is Nothing AndAlso Not bindingSet.items Is Nothing Then
                            For Each bind As XMLParser.CONSTRAINT_BINDING_ITEM In bindingSet.items
                                ontologyManager.AddConstraintBinding(terminology, bind.code, bind.value)
                            Next
                        End If
                    End If
                Next
            End If
        End Sub

        Public Overrides Sub PopulateTermsInLanguage(ByRef ontologyManager As OntologyManager, ByVal language As String)
            PopulateTermDefinitions(ontologyManager.TermDefinitionTable, archetypeParser.Archetype.ontology.term_definitions, language)
            PopulateTermDefinitions(ontologyManager.ConstraintDefinitionTable, archetypeParser.Archetype.ontology.constraint_definitions, language)
        End Sub

        Public Overrides Sub PopulateAllTerms(ByRef ontologyManager As OntologyManager)
            If archetypeParser.ArchetypeAvailable Then
                PopulateLanguages(ontologyManager)
                PopulateTerminologies(ontologyManager)
                PopulateTermsInLanguage(ontologyManager, "")
                PopulateTermBindings(ontologyManager)
                PopulateConstraintBindings(ontologyManager)
            End If
        End Sub

        Sub New(ByRef xmlParser As XMLParser.XmlArchetypeParser)
            archetypeParser = xmlParser
        End Sub

    End Class
End Namespace
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
'The Original Code is XML_Ontology.vb.
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
