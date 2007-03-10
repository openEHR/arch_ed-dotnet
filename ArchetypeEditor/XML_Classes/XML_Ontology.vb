'
'
'	component:   "openEHR Archetype Project"
'	description: "Specialisation of the Ontology class to work with ADL"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/XML_Classes/SCCS/s.XML_Ontology.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Explicit On 
Namespace ArchetypeEditor.XML_Classes
    Friend Class XML_Ontology
        Inherits Ontology
        Private archetypeParser As XMLParser.XmlArchetypeParser
        Private sLanguageCode As String

        Public Overrides ReadOnly Property PrimaryLanguageCode() As String
            Get
                Return archetypeParser.Archetype.original_language.code_string
            End Get
        End Property
        Public Overrides ReadOnly Property LanguageCode() As String
            Get
                Return sLanguageCode
            End Get
        End Property
        Public Overrides ReadOnly Property NumberOfSpecialisations() As Integer
            Get
                Return archetypeParser.Archetype.ontology.specialisation_depth
            End Get
        End Property

        Public Overrides Function LanguageAvailable(ByVal code As String) As Boolean
            Return archetypeParser.Ontology.LanguageAvailable(code)
        End Function

        Public Overrides Function TerminologyAvailable(ByVal code As String) As Boolean
            Return archetypeParser.Ontology.TerminologyAvailable(code)
        End Function

        Public Overrides Function TermForCode(ByVal Code As String, ByVal Language As String) As RmTerm

            If Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("at") Then
                If archetypeParser.Ontology.HasTermCode(Code) Then
                    Return New XML_Classes.XML_Term(archetypeParser.Ontology.TermDefinition(Language, Code))
                End If
            ElseIf Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("ac") Then
                If archetypeParser.Ontology.HasTermCode(Code) Then
                    Return New XML_Classes.XML_Term(archetypeParser.Ontology.ConstraintDefinition(Language, Code))
                End If
            Else
                Debug.Assert(False, "Code type is not available")
            End If

            Return Nothing

        End Function

        Public Overrides Function IsMultiLanguage() As Boolean
            Return archetypeParser.Ontology.AvailableLanguages.Count > 1
        End Function

        Public Overrides Sub Reset()
            ' no action required
            ' archetypeParser.ontology.clear_terminology()
        End Sub

        Public Overrides Sub AddLanguage(ByVal code As String)
            archetypeParser.Ontology.AddLanguage(code)
        End Sub

        Public Overrides Sub AddTerminology(ByVal code As String)
            Try
                If Not archetypeParser.Ontology.TerminologyAvailable(code) Then
                    archetypeParser.Ontology.AddTerminology(code)
                End If
            Catch e As Exception
                Debug.Assert(False)
                MessageBox.Show(AE_Constants.Instance.Error_saving & " " & AE_Constants.Instance.Terminology, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Function HasTermBinding(ByVal a_terminology_id As String, ByVal a_path As String) As Boolean
            Return archetypeParser.Ontology.HasTermBinding(a_terminology_id, a_path)
        End Function

        Public Overrides Function HasConstraintBinding(ByVal a_terminology_id As String, ByVal a_path As String) As Boolean
            Return archetypeParser.Ontology.HasConstraintBinding(a_terminology_id, a_path)
        End Function

        Public Overrides Function TermBinding(ByVal a_terminology_id As String, ByVal a_path As String) As String
            Return archetypeParser.Ontology.TermBinding(a_terminology_id, a_path)
        End Function

        Public Overrides Function ConstraintBinding(ByVal a_terminology_id As String, ByVal a_path As String) As String
            Return archetypeParser.Ontology.ConstraintBinding(a_terminology_id, a_path)
        End Function

        Public Overrides Sub AddorReplaceTermBinding(ByVal sTerminology As String, ByVal sPath As String, ByVal sCode As String, ByVal sRelease As String)
            ' Added ascerts required for this piece of code to run successfully
            ' and try statement
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sPath <> "", "Path or nodeID are not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            ' release is not utilised at this point
            Try
                archetypeParser.Ontology.AddOrReplaceTermBinding(sCode, sPath, sTerminology)
            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveTermBinding(ByVal a_terminology_id As String, ByVal archetype_path As String)
            ' Added - Sam Heard 2005.05.15
            Debug.Assert(archetype_path <> "", "Code is not set")
            Debug.Assert(a_terminology_id <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                If archetypeParser.Ontology.HasTermBinding(a_terminology_id, archetype_path) Then
                    archetypeParser.Ontology.RemoveTermBinding(archetype_path, a_terminology_id)
                End If

            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub AddorReplaceConstraintBinding(ByVal a_terminology_id As String, ByVal ac_code As String, ByVal a_query As String, ByVal sRelease As String)
            ' CHANGE - Sam Heard 2006.05.07
            ' Added ascerts required for this piece of code to run successfully
            ' and try statement
            Debug.Assert(ac_code <> "", "Code is not set")
            Debug.Assert(a_query <> "", "Query is not set")
            Debug.Assert(a_terminology_id <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                archetypeParser.Ontology.AddOrReplaceConstraintBinding(a_query, ac_code, a_terminology_id)
            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveConstraintBinding(ByVal a_terminology_id As String, ByVal a_query As String)
            ' Added - Sam Heard 2005.05.15
            Debug.Assert(a_query <> "", "Code is not set")
            Debug.Assert(a_terminology_id <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                If archetypeParser.Ontology.HasConstraintBinding(a_terminology_id, a_query) Then
                    archetypeParser.Ontology.RemoveConstraintBinding(a_query, a_terminology_id)
                End If

            Catch e As System.Exception
                MessageBox.Show(e.Message, "XML Parser error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub SetLanguage(ByVal code As String)
            sLanguageCode = code
            archetypeParser.Ontology.SetLanguage(code)
        End Sub

        Public Overrides Function SpecialiseTerm(ByVal Text As String, ByVal Description As String, ByVal Id As String) As RmTerm
            ' increase the number of specialisations
            Dim an_Term As XML_Classes.XML_Term

            an_Term = New XML_Classes.XML_Term(NextSpecialisedId(Id))
            an_Term.Text = Text
            an_Term.Description = Description
            Me.AddTerm(an_Term)
            Return an_Term
        End Function

        Public Overrides Sub SetPrimaryLanguage(ByVal LanguageCode As String)
            ' sets the primary language of this archetype
            ' if this language is not in the available languages it adds it

            If LanguageCode = "" Then
                Return
            Else
                archetypeParser.Ontology.SetPrimaryLanguage(LanguageCode)
            End If
        End Sub

        Public Overrides Function NextTermId() As String
            Try
                Return archetypeParser.Ontology.NextTermId()
            Catch e As Exception
                Debug.Assert(False, e.Message)
                Return ""
            End Try
        End Function

        Public Overrides Function NextConstraintId() As String
            Return archetypeParser.Ontology.NextConstraintId()
        End Function

        Private Function NextSpecialisedId(ByVal ParentCode As String) As String
            Return archetypeParser.Ontology.NextSpecialisedTermId(ParentCode)
        End Function

        Public Overrides Sub AddTerm(ByVal a_Term As RmTerm)
            Dim an_xml_Term As XML_Classes.XML_Term

            an_xml_Term = New XML_Classes.XML_Term(a_Term)

            Try
                If Not archetypeParser.Ontology.HasTermCode(an_xml_Term.Code) Then
                    archetypeParser.Ontology.AddTermOrConstraintDefinition(sLanguageCode, an_xml_Term.XML_Term, False)
                Else
                    Debug.Assert(False)
                End If

            Catch e As Exception
                Debug.Assert(False, e.ToString)
            End Try
        End Sub

        Public Overrides Sub ReplaceTerm(ByVal a_Term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
            Dim language_code As String

            If Not a_Term.isConstraint Then
                If a_Term.Language <> "" Then
                    language_code = a_Term.Language
                Else
                    language_code = archetypeParser.Ontology.LanguageCode
                End If

                Try
                    If archetypeParser.Ontology.HasTermCode(a_Term.Code) Then
                        Dim term As XML_Term = New XML_Term(a_Term)
                        archetypeParser.Ontology.ReplaceTermDefinition(language_code, term.XML_Term, ReplaceTranslations)
                    Else
                        Debug.Assert(False, "Term code is not available: " & a_Term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Term is a constraint and should not be passed")
            End If
        End Sub

        Public Overrides Function HasTermCode(ByVal a_term_code As String) As Boolean
            If RmTerm.isValidTermCode(a_term_code) Then
                Return archetypeParser.Ontology.HasTermCode(a_term_code)
            End If
        End Function

        Public Overrides Sub AddConstraint(ByVal a_term As RmTerm)
            If a_term.isConstraint Then
                Try
                    If Not archetypeParser.Ontology.HasTermCode(a_term.Code) Then
                        archetypeParser.Ontology.AddTermOrConstraintDefinition( _
                            archetypeParser.Ontology.LanguageCode, _
                            New XML_Term(a_term).XML_Term, False)
                    Else
                        Debug.Assert(False, "Constraint code not available: " & a_term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Code is not a constraint code: " & a_term.Code)
            End If
        End Sub

        Public Sub AddTermDefinitionsFromTable(ByVal a_termdef_table As System.Data.DataTable)

            For Each data_row As System.Data.DataRow In a_termdef_table.Rows
                Dim aTerm As New XML_Term(CStr(data_row(1)), CStr(data_row(2)), _
                    CStr(data_row(3)), CStr(data_row(4)))
                archetypeParser.Ontology.AddTermOrConstraintDefinition(CStr(data_row(0)), aTerm.XML_Term, True)
            Next
        End Sub


        Public Sub AddTermBindingsFromTable(ByVal a_termBinding_table As System.Data.DataTable)
            Dim data_row As System.Data.DataRow
            For Each data_row In a_termBinding_table.Rows
                '                         terminology id        code                path
                archetypeParser.Ontology.AddOrReplaceTermBinding(CStr(data_row(2)), CStr(data_row(1)), CStr(data_row(0)))
            Next
        End Sub

        Public Sub AddConstraintBindingsFromTable(ByVal a_constraintBinding_table As System.Data.DataTable)
            Dim data_row As System.Data.DataRow
            For Each data_row In a_constraintBinding_table.Rows
                archetypeParser.Ontology.AddOrReplaceConstraintBinding(CStr(data_row(2)), CStr(data_row(1)), CStr(data_row(0)))
            Next
        End Sub


        Public Sub AddConstraintDefinitionsFromTable(ByVal a_termdef_table As System.Data.DataTable)
            Dim data_row As System.Data.DataRow
            For Each data_row In a_termdef_table.Rows
                Dim aTerm As XML_Term = New XML_Term(CStr(data_row(1)), CStr(data_row(2)), CStr(data_row(3)))
                archetypeParser.Ontology.AddTermOrConstraintDefinition(CStr(data_row(0)), aTerm.XML_Term, True)
            Next
        End Sub


        Public Overrides Sub ReplaceConstraint(ByVal a_term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)

            If a_Term.isConstraint Then

                Try
                    Dim xmlTerm As New XML_Term(a_term)
                    If archetypeParser.Ontology.HasTermCode(a_term.Code) Then
                        archetypeParser.Ontology.ReplaceTermDefinition( _
                            archetypeParser.Ontology.LanguageCode, xmlTerm.XML_Term, ReplaceTranslations)
                    Else
                        Debug.Assert(False, "Constraint code not available: " & a_term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Code is not a constraint code: " & a_Term.Code)
            End If
        End Sub

        Public Sub populate_languages(ByRef TheOntologyManager As OntologyManager)

            If archetypeParser.Ontology.AvailableLanguages.Count > 0 Then
                'A new ontology always adds the current language - but this may not be available in the archetype
                ' so clear ..
                TheOntologyManager.LanguagesTable.Clear()

                For Each language As String In archetypeParser.Ontology.AvailableLanguages
                    TheOntologyManager.AddLanguage(language)
                Next

            End If

        End Sub

        Private Sub populate_terminologies(ByRef TheOntologyManager As OntologyManager)
            ' populate the terminology table in TermLookUp
            If archetypeParser.Ontology.AvailableTerminologies.Count > 0 Then
                For Each terminologyId As String In archetypeParser.Ontology.AvailableTerminologies
                    Dim termDescription As String = TheOntologyManager.GetTerminologyDescription(terminologyId)
                    TheOntologyManager.AddTerminology(terminologyId, termDescription)
                Next
            End If
        End Sub

        Private Sub populate_term_definitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
            ' populate the TermDefinitions table in TermLookUp

            If archetypeParser.Archetype.ontology.term_defintions.Length > 0 Then
                Dim d_row As DataRow
                Dim a_term As XML_Classes.XML_Term

                For Each td As XMLParser.language_set In archetypeParser.Archetype.ontology.term_defintions
                    If LanguageCode = "" Or td.language = LanguageCode Then
                        ' set the term for all languages
                        For Each termDef As XMLParser.ARCHETYPE_TERM In td.terms
                            a_term = New XML_Classes.XML_Term(termDef)
                            d_row = TheOntologyManager.TermDefinitionTable.NewRow
                            d_row(0) = td.language
                            d_row(1) = a_term.Code
                            d_row(2) = a_term.Text
                            d_row(3) = a_term.Description
                            d_row(4) = a_term.Comment
                            ' add it to the GUI ontology
                            TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                        Next
                    End If
                Next
            End If
        End Sub

        Private Sub populate_term_bindings(ByRef TheOntologyManager As OntologyManager)
            ' populate the TermBindings table in TermLookUp

            If (Not archetypeParser.Archetype.ontology.term_bindings Is Nothing) AndAlso archetypeParser.Archetype.ontology.term_bindings.Length > 0 Then
                Dim d_row, selected_row As DataRow
                Dim terminology As String

                For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                    terminology = CStr(selected_row(0))
                    If archetypeParser.Ontology.TerminologyAvailable(terminology) Then
                        Dim ts As XMLParser.terminology_set = archetypeParser.Ontology.GetBindings(terminology, archetypeParser.Archetype.ontology.term_bindings)
                        'Can have terminologies which do not have bindings
                        If Not ts Is Nothing Then
                            Dim tab As DataTable = TheOntologyManager.TermBindingsTable
                            For Each bind As XMLParser.binding In ts.bindings
                                d_row = tab.NewRow
                                d_row(0) = selected_row(0)
                                d_row(1) = bind.code
                                d_row(2) = bind.value
                                tab.Rows.Add(d_row)
                            Next
                        End If
                    End If
                Next
            End If
        End Sub

        Private Sub populate_constraint_definitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
            ' populate the ConstraintDefinitions table in TermLookUp


            If (Not archetypeParser.Archetype.ontology.constraint_defintions Is Nothing) AndAlso archetypeParser.Archetype.ontology.constraint_defintions.Length > 0 Then
                Dim d_row As DataRow
                Dim a_term As XML_Classes.XML_Term

                For Each td As XMLParser.language_set In archetypeParser.Archetype.ontology.constraint_defintions
                    If LanguageCode = "" Or td.language = LanguageCode Then
                        ' set the term for all languages
                        If Not td.terms Is Nothing Then
                            For Each termDef As XMLParser.ARCHETYPE_TERM In td.terms
                                a_term = New XML_Classes.XML_Term(termDef)
                                d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                                d_row(0) = td.language
                                d_row(1) = a_term.Code
                                d_row(2) = a_term.Text
                                d_row(3) = a_term.Description
                                ' add it to the GUI ontology
                                TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                            Next
                        Else
                            Debug.Assert(False, "Constraint definitions exist but no terms")
                        End If
                    End If
                Next
            End If
        End Sub

        Private Function OntologyConstraintCodes() As ArrayList
            Dim result As New ArrayList
            If Not archetypeParser.Archetype.ontology.constraint_defintions Is Nothing Then
                Dim ls As XMLParser.language_set = archetypeParser.Archetype.ontology.constraint_defintions(0)
                For Each t As XMLParser.ARCHETYPE_TERM In ls.terms
                    result.Add(t.code)
                Next
            End If
            result.Sort()
            Return result
        End Function

        Private Function OntologyTermCodes() As ArrayList
            Dim result As New ArrayList
            If Not archetypeParser.Archetype.ontology.term_defintions Is Nothing Then
                Dim ls As XMLParser.language_set = archetypeParser.Archetype.ontology.term_defintions(0)
                For Each t As XMLParser.ARCHETYPE_TERM In ls.terms
                    result.Add(t.code)
                Next
            End If
            result.Sort()
            Return result
        End Function

        Private Sub populate_constraint_bindings(ByRef TheOntologyManager As OntologyManager)
            ' populate the ConstraintBindings table in TermLookUp

            If (Not archetypeParser.Archetype.ontology.constraint_bindings Is Nothing) AndAlso archetypeParser.Archetype.ontology.constraint_bindings.Length > 0 Then
                Dim d_row, selected_row As DataRow
                Dim terminology As String

                For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                    terminology = CStr(selected_row(0))
                    If archetypeParser.Ontology.TerminologyAvailable(terminology) Then
                        Dim ts As XMLParser.terminology_set = archetypeParser.Ontology.GetBindings(terminology, archetypeParser.Archetype.ontology.constraint_bindings)
                        'can have no bindings so
                        If Not ts Is Nothing AndAlso (Not ts.bindings Is Nothing) AndAlso (ts.bindings.Length > 0) Then
                            For Each bind As XMLParser.binding In ts.bindings
                                d_row = TheOntologyManager.ConstraintBindingsTable.NewRow
                                d_row(0) = selected_row(0)
                                d_row(1) = bind.code  'AC code
                                d_row(2) = bind.value
                                TheOntologyManager.ConstraintBindingsTable.Rows.Add(d_row)
                            Next
                        End If
                    End If
                Next
            End If
        End Sub

        Public Overrides Sub PopulateTermsInLanguage(ByRef TheOntologyManager As OntologyManager, ByVal LanguageCode As String)
            populate_term_definitions(TheOntologyManager, LanguageCode)
            populate_constraint_definitions(TheOntologyManager, LanguageCode)
        End Sub

        Public Overrides Sub PopulateAllTerms(ByRef TheOntologyManager As OntologyManager)
            If archetypeParser.ArchetypeAvailable Then
                populate_languages(TheOntologyManager)
                populate_terminologies(TheOntologyManager)
                populate_term_definitions(TheOntologyManager)
                populate_term_bindings(TheOntologyManager)
                populate_constraint_definitions(TheOntologyManager)
                populate_constraint_bindings(TheOntologyManager)
            End If

        End Sub

        Sub New(ByRef an_xml_parser As XMLParser.XmlArchetypeParser, Optional ByVal Replace As Boolean = False)
            archetypeParser = an_xml_parser
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
