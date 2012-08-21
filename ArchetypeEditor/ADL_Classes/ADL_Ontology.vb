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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Ontology.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel
Imports EiffelStructures = EiffelSoftware.Library.Base.structures
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes
    Friend Class ADL_Ontology
        Inherits Ontology
        Private EIF_adlInterface As openehr.adl_parser.interface.ADL_INTERFACE

        Public Overrides ReadOnly Property PrimaryLanguageCode() As String
            Get
                Dim result As String = ""

                If EIF_adlInterface.archetype_available Then
                    result = EIF_adlInterface.ontology.primary_language.to_cil
                End If

                Return result
            End Get
        End Property

        Public Overrides ReadOnly Property LanguageCode() As String
            Get
                Return EIF_adlInterface.current_language.to_cil
            End Get
        End Property

        Public Overrides ReadOnly Property NumberOfSpecialisations() As Integer
            Get
                Dim result As Integer = 0

                If EIF_adlInterface.archetype_available Then
                    result = EIF_adlInterface.adl_engine.archetype.specialisation_depth()
                End If

                Return result
            End Get
        End Property

        Public Overrides Function LanguageAvailable(ByVal code As String) As Boolean
            Return EIF_adlInterface.archetype_available AndAlso EIF_adlInterface.ontology.has_language(Eiffel.String(code))
        End Function

        Public Overrides Function TerminologyAvailable(ByVal code As String) As Boolean
            Return EIF_adlInterface.archetype_available AndAlso EIF_adlInterface.ontology.has_terminology(Eiffel.String(code))
        End Function

        Public Overrides Function TermForCode(ByVal Code As String, ByVal Language As String) As RmTerm
            Dim result As RmTerm = Nothing

            If EIF_adlInterface.archetype_available Then
                If Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("at") Then
                    If EIF_adlInterface.ontology.has_term_code(Eiffel.String(Code)) Then
                        result = New ADL_Term(EIF_adlInterface.ontology.term_definition(Eiffel.String(Language), Eiffel.String(Code)))
                    End If
                ElseIf Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("ac") Then
                    If EIF_adlInterface.ontology.has_constraint_code(Eiffel.String(Code)) Then
                        result = New ADL_Term(EIF_adlInterface.ontology.constraint_definition(Eiffel.String(Language), Eiffel.String(Code)))
                    End If
                End If
            End If

            Return result
        End Function

        Public Overrides Function IsMultiLanguage() As Boolean
            Return EIF_adlInterface.archetype_available AndAlso EIF_adlInterface.ontology.languages_available.count > 1
        End Function

        Public Overrides Sub Reset()
            ' no action required
            ' EIF_adlInterface.ontology.clear_terminology()
        End Sub

        Public Overrides Sub AddLanguage(ByVal code As String)
            If EIF_adlInterface.archetype_available Then
                EIF_adlInterface.ontology.add_language(Eiffel.String(code))
            End If
        End Sub

        Public Overrides Function HasTermBinding(ByVal a_terminology_id As String, ByVal a_path As String) As Boolean
            Return EIF_adlInterface.archetype_available AndAlso EIF_adlInterface.ontology.has_term_binding(Eiffel.String(a_terminology_id), Eiffel.String(a_path))
        End Function

        Public Overrides Function HasConstraintBinding(ByVal a_terminology_id As String, ByVal a_path As String) As Boolean
            Return EIF_adlInterface.archetype_available AndAlso EIF_adlInterface.ontology.has_constraint_binding(Eiffel.String(a_terminology_id), Eiffel.String(a_path))
        End Function

        Public Overrides Function TermBinding(ByVal a_terminology_id As String, ByVal a_path As String) As String
            Dim result As String = ""
            Dim codePhrase As openehr.openehr.rm.data_types.text.CODE_PHRASE

            If EIF_adlInterface.archetype_available Then
                codePhrase = EIF_adlInterface.ontology.term_binding(Eiffel.String(a_terminology_id), Eiffel.String(a_path))

                If Not codePhrase Is Nothing Then
                    result = codePhrase.code_string.to_cil
                End If
            End If

            Return result
        End Function

        Public Overrides Function ConstraintBinding(ByVal terminologyid As String, ByVal path As String) As String
            Dim result As String = ""
            Dim codePhrase As openehr.openehr.rm.data_types.text.CODE_PHRASE

            If EIF_adlInterface.archetype_available Then
                codePhrase = EIF_adlInterface.ontology.constraint_binding(Eiffel.String(terminologyid), Eiffel.String(path))

                If Not codePhrase Is Nothing Then
                    result = codePhrase.code_string.to_cil
                End If
            End If

            Return result
        End Function

        Public Overrides Sub AddorReplaceTermBinding(ByVal sTerminology As String, ByVal sPath As String, ByVal sCode As String, ByVal sRelease As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sPath <> "", "Path or nodeID are not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            Try
                If EIF_adlInterface.archetype_available Then
                    Dim str As EiffelKernel.STRING_8 = Eiffel.String(sPath)

                    If sRelease <> "" Then
                        sTerminology = String.Format("{0}({1})", sTerminology, sRelease)
                    End If

                    Dim cp As openehr.openehr.rm.data_types.text.CODE_PHRASE = openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string(Eiffel.String(sTerminology & "::" & sCode))

                    If EIF_adlInterface.ontology.has_term_binding(cp.terminology_id.value, str) Then
                        EIF_adlInterface.ontology.replace_term_binding(cp, str)
                    Else
                        EIF_adlInterface.ontology.add_term_binding(cp, str)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveTermBinding(ByVal sTerminology As String, ByVal sCode As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            Try
                If EIF_adlInterface.archetype_available Then
                    Dim str As EiffelKernel.STRING_8 = Eiffel.String(sCode)
                    Dim terminology As EiffelKernel.STRING_8 = Eiffel.String(sTerminology)

                    If EIF_adlInterface.ontology.has_term_binding(terminology, str) Then
                        EIF_adlInterface.ontology.remove_term_binding(str, terminology)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub AddorReplaceConstraintBinding(ByVal sTerminology As String, ByVal sCode As String, ByVal sQuery As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sQuery <> "", "Query is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            Try
                If EIF_adlInterface.archetype_available Then
                    Dim terminology As EiffelKernel.STRING_8 = Eiffel.String(sTerminology)
                    Dim cd As EiffelKernel.STRING_8 = Eiffel.String(sCode)
                    Dim uri As openehr.common_libs.basic.URI = openehr.common_libs.basic.Create.URI.make_from_string(Eiffel.String(sQuery))

                    If EIF_adlInterface.ontology.has_constraint_binding(terminology, cd) Then
                        EIF_adlInterface.ontology.replace_constraint_binding(uri, terminology, cd)
                    Else
                        EIF_adlInterface.ontology.add_constraint_binding(uri, terminology, cd)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveConstraintBinding(ByVal sTerminology As String, ByVal sCode As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            Try
                If EIF_adlInterface.archetype_available Then
                    Dim str As EiffelKernel.STRING_8 = Eiffel.String(sCode)
                    Dim terminology As EiffelKernel.STRING_8 = Eiffel.String(sTerminology)

                    If EIF_adlInterface.ontology.has_constraint_binding(terminology, str) Then
                        EIF_adlInterface.ontology.remove_constraint_binding(str, terminology)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub SetLanguage(ByVal code As String)
            EIF_adlInterface.set_current_language(Eiffel.String(code))
        End Sub

        Public Overrides Function SpecialiseTerm(ByVal Text As String, ByVal Description As String, ByVal Id As String) As RmTerm
            ' increase the number of specialisations
            Dim result As New ADL_Term(NextSpecialisedId(Id))
            result.Text = Text
            result.Description = Description
            AddTerm(result)
            Return result
        End Function

        Public Overrides Sub SetPrimaryLanguage(ByVal language As String)
            ' sets the primary language of this archetype
            ' if this language is added to the available languages it adds it

            If language <> "" And EIF_adlInterface.archetype_available Then
                EIF_adlInterface.ontology.set_primary_language(Eiffel.String(language))
            End If
        End Sub

        Public Overrides Function NextTermId() As String
            Dim result As String = ""

            Try
                If EIF_adlInterface.archetype_available Then
                    result = EIF_adlInterface.ontology.new_non_specialised_term_code.to_cil
                End If
            Catch e As Exception
                Debug.Assert(False, e.Message)
            End Try

            Return result
        End Function

        Public Overrides Function NextConstraintId() As String
            Dim result As String = ""

            If EIF_adlInterface.archetype_available Then
                result = EIF_adlInterface.ontology.new_constraint_code.to_cil
            End If

            Return result
        End Function

        Private Function NextSpecialisedId(ByVal ParentCode As String) As EiffelKernel.STRING_8
            Dim result As EiffelKernel.STRING_8

            If EIF_adlInterface.archetype_available Then
                result = EIF_adlInterface.ontology.new_specialised_term_code(Eiffel.String(ParentCode))
            Else
                result = EiffelKernel.Create.STRING_8.make_empty
            End If

            Return result
        End Function

        Public Overrides Sub AddTerm(ByVal term As RmTerm)
            If EIF_adlInterface.archetype_available Then
                Dim adlTerm As New ADL_Term(term)

                If Not term.IsConstraint Then
                    Try
                        If Not EIF_adlInterface.ontology.has_term_code(adlTerm.EIF_Term.code) Then
                            EIF_adlInterface.ontology.add_term_definition(EIF_adlInterface.current_language, adlTerm.EIF_Term)
                        Else
                            Debug.Assert(False)
                        End If
                    Catch e As Exception
                        Debug.Assert(False, e.ToString)
                    End Try
                Else
                    Try
                        If Not EIF_adlInterface.ontology.has_constraint_code(adlTerm.EIF_Term.code) Then
                            EIF_adlInterface.ontology.add_constraint_definition(EIF_adlInterface.current_language, adlTerm.EIF_Term)
                        Else
                            Debug.Assert(False)
                        End If
                    Catch e As Exception
                        Debug.Assert(False, e.ToString)
                    End Try
                End If
            End If
        End Sub

        Public Overrides Sub ReplaceTerm(ByVal term As RmTerm, Optional ByVal replaceTranslations As Boolean = False)
            If EIF_adlInterface.archetype_available Then
                If Not term.IsConstraint Then
                    Dim adlTerm As ADL_Term = New ADL_Term(term)
                    Dim language As EiffelKernel.STRING_8

                    If term.Language <> "" Then
                        language = Eiffel.String(term.Language)
                    Else
                        language = EIF_adlInterface.current_language
                    End If

                    Try
                        If EIF_adlInterface.ontology.has_term_code(adlTerm.EIF_Term.code) Then
                            EIF_adlInterface.ontology.replace_term_definition(language, adlTerm.EIF_Term, replaceTranslations)
                        Else
                            Debug.Assert(False, "Term code is not available: " & adlTerm.Code)
                        End If
                    Catch e As Exception
                        Debug.Assert(False, e.Message)
                    End Try
                Else
                    Debug.Assert(False, "Term is a constraint and should not be passed")
                End If
            End If
        End Sub

        Public Overrides Function HasTermCode(ByVal termCode As String) As Boolean
            Dim result As Boolean = False

            If RmTerm.IsValidTermCode(termCode) And EIF_adlInterface.archetype_available Then
                result = EIF_adlInterface.ontology.has_term_code(Eiffel.String(termCode)) Or EIF_adlInterface.ontology.has_constraint_code(Eiffel.String(termCode))
            End If

            Return result
        End Function

        Public Overrides Sub AddConstraint(ByVal term As RmTerm)
            If EIF_adlInterface.archetype_available Then
                If term.IsConstraint Then
                    Dim adlTerm As New ADL_Term(term)

                    Try
                        If Not EIF_adlInterface.ontology.has_constraint_code(adlTerm.EIF_Term.code) Then
                            EIF_adlInterface.ontology.add_constraint_definition(EIF_adlInterface.current_language, adlTerm.EIF_Term)
                        Else
                            Debug.Assert(False, "Constraint code not available: " & adlTerm.Code)
                        End If
                    Catch e As Exception
                        Debug.Assert(False, e.Message)
                    End Try
                Else
                    Debug.Assert(False, "Code is not a constraint code: " & term.Code)
                End If
            End If
        End Sub

        Public Overrides Sub ReplaceConstraint(ByVal term As RmTerm, Optional ByVal replaceTranslations As Boolean = False)
            If EIF_adlInterface.archetype_available Then
                If term.IsConstraint Then
                    Dim adlTerm As New ADL_Term(term)

                    Try
                        If EIF_adlInterface.ontology.has_constraint_code(adlTerm.EIF_Term.code) Then
                            EIF_adlInterface.ontology.replace_constraint_definition(EIF_adlInterface.current_language, adlTerm.EIF_Term, replaceTranslations)
                        Else
                            Debug.Assert(False, "Constraint code not available: " & adlTerm.Code)
                        End If
                    Catch e As Exception
                        Debug.Assert(False, e.Message)
                    End Try
                Else
                    Debug.Assert(False, "Code is not a constraint code: " & term.Code)
                End If
            End If
        End Sub

        Private Sub PopulateLanguages(ByRef ontologyManager As OntologyManager)
            If EIF_adlInterface.archetype_available And Not EIF_adlInterface.ontology.languages_available.empty() Then
                'A new ontology always adds the current language - but this may not be available in the archetype, so clear ..
                ontologyManager.LanguagesTable.Clear()
                Dim i As Integer

                For i = EIF_adlInterface.ontology.languages_available.lower() To EIF_adlInterface.ontology.languages_available.upper()
                    ontologyManager.AddLanguage(CType(EIF_adlInterface.ontology.languages_available.i_th(i), EiffelKernel.STRING_8).to_cil())
                Next
            End If
        End Sub

        Private Sub PopulateTerminologies(ByRef ontologyManager As OntologyManager)
            ' populate the terminology table in TermLookUp
            If EIF_adlInterface.archetype_available Then
                Dim i As Integer

                For i = EIF_adlInterface.ontology.terminologies_available.lower() To EIF_adlInterface.ontology.terminologies_available.upper()
                    ontologyManager.AddTerminology(CType(EIF_adlInterface.ontology.terminologies_available.i_th(i), EiffelKernel.STRING_8).to_cil())
                Next
            End If
        End Sub

        Private Sub PopulateTermDefinitions(ByRef ontologyManager As OntologyManager, ByVal language As String)
            ' populate the TermDefinitions table in TermLookUp
            If EIF_adlInterface.archetype_available And Not EIF_adlInterface.ontology.term_definitions.empty() Then
                Dim linklist As EiffelStructures.traversing.LINEAR_REFERENCE = EIF_adlInterface.ontology.term_codes
                linklist.start()

                Do While Not linklist.off
                    Dim term As ADL_Term
                    Dim row As DataRow
                    Dim s As EiffelKernel.STRING_8 = CType(linklist.item, EiffelKernel.STRING_8)

                    If language = "" Then
                        ' set the term for all languages
                        For Each terminologyRow As DataRow In ontologyManager.LanguagesTable.Rows
                            ' take each term from the ADL ontology
                            Dim adlTerm As openehr.openehr.am.archetype.ontology.ARCHETYPE_TERM = EIF_adlInterface.ontology.term_definition(Eiffel.String(terminologyRow(0)), s)

                            If adlTerm Is Nothing Then
                                Debug.Assert(False, "Term not in this language")
                                term = New ADL_Term(s.to_cil)
                                term.Text = "#Error#"
                                term.Description = "#Error#"
                                term.Comment = ""
                            Else
                                term = New ADL_Term(adlTerm)
                            End If

                            ' and if it is not an internal ID for machine processing
                            row = ontologyManager.TermDefinitionTable.NewRow
                            row(0) = terminologyRow(0)
                            row(1) = term.Code
                            row(2) = term.Text
                            row(3) = term.Description
                            row(4) = term.Comment
                            row(5) = term
                            ' add it to the GUI ontology
                            ontologyManager.TermDefinitionTable.Rows.Add(row)
                        Next
                    Else
                        'Add the new language to the GUI ontology
                        term = New ADL_Term(EIF_adlInterface.ontology.term_definition(Eiffel.String(language), s))
                        row = ontologyManager.TermDefinitionTable.NewRow
                        row(0) = language
                        row(1) = term.Code
                        row(2) = term.Text
                        row(3) = term.Description
                        row(4) = term.Comment
                        row(5) = term
                        ontologyManager.TermDefinitionTable.Rows.Add(row)
                    End If

                    linklist.forth()
                Loop
            End If
        End Sub

        Private Sub PopulateTermBindings(ByRef ontologyManager As OntologyManager)
            ' populate the TermBindings table in TermLookUp

            If EIF_adlInterface.archetype_available And Not EIF_adlInterface.ontology.term_bindings.empty() Then
                Dim d_row, selected_row As DataRow
                Dim terminology, code As EiffelKernel.STRING_8
                Dim cp As openehr.openehr.rm.data_types.text.CODE_PHRASE
                Dim Bindings As EiffelStructures.table.HASH_TABLE_REFERENCE_REFERENCE

                For Each selected_row In ontologyManager.TerminologiesTable.Rows
                    terminology = Eiffel.String(selected_row(0))

                    If EIF_adlInterface.ontology.has_term_bindings(terminology) Then
                        Bindings = EIF_adlInterface.ontology.term_bindings_for_terminology(terminology)
                        Bindings.start()

                        Do While Not Bindings.off
                            cp = Bindings.item_for_iteration
                            code = Bindings.key_for_iteration
                            d_row = ontologyManager.TermBindingsTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = code.to_cil
                            d_row(2) = cp.code_string

                            If Not cp.terminology_id.version_id Is Nothing Then
                                d_row(3) = cp.terminology_id.version_id.to_cil    ' release
                            End If

                            ontologyManager.TermBindingsTable.Rows.Add(d_row)
                            Bindings.forth()
                        Loop
                    End If
                Next
            End If
        End Sub

        Private Sub PopulateConstraintDefinitions(ByRef ontologyManager As OntologyManager, ByVal language As String)
            ' populate the ConstraintDefinitions table in TermLookUp
            If EIF_adlInterface.archetype_available And Not EIF_adlInterface.ontology.constraint_definitions.empty() Then
                Dim linklist As EiffelStructures.traversing.LINEAR_REFERENCE = EIF_adlInterface.ontology.constraint_codes
                linklist.start()

                Do While Not linklist.off
                    Dim term As ADL_Term
                    Dim row As DataRow
                    Dim s As EiffelKernel.STRING_8 = CType(linklist.item, EiffelKernel.STRING_8)

                    If language = "" Then
                        ' set the constraints for all languages
                        For Each terminologyRow As DataRow In ontologyManager.LanguagesTable.Rows
                            ' take each constraint from the ADL ontology
                            term = New ADL_Term(EIF_adlInterface.ontology.constraint_definition(Eiffel.String(terminologyRow(0)), s))
                            row = ontologyManager.ConstraintDefinitionTable.NewRow
                            row(0) = terminologyRow(0)
                            row(1) = term.Code
                            row(2) = term.Text
                            row(3) = term.Description
                            'Add it to the GUI
                            ontologyManager.ConstraintDefinitionTable.Rows.Add(row)
                        Next
                    Else
                        'Add the new language to the GUI ontology
                        term = New ADL_Term(EIF_adlInterface.ontology.constraint_definition(Eiffel.String(language), s))
                        row = ontologyManager.ConstraintDefinitionTable.NewRow
                        row(0) = language
                        row(1) = term.Code
                        row(2) = term.Text
                        row(3) = term.Description
                        ontologyManager.ConstraintDefinitionTable.Rows.Add(row)
                    End If

                    linklist.forth()
                Loop
            End If
        End Sub

        Private Sub PopulateConstraintBindings(ByRef ontologyManager As OntologyManager)
            ' populate the ConstraintBindings table in TermLookUp
            If EIF_adlInterface.archetype_available And Not EIF_adlInterface.ontology.constraint_bindings.empty() Then
                Dim acCodes As EiffelStructures.traversing.LINEAR_REFERENCE = EIF_adlInterface.ontology.constraint_codes
                acCodes.start()

                Do While Not acCodes.off
                    Dim acCode As EiffelKernel.STRING_8 = CType(acCodes.item, EiffelKernel.STRING_8)

                    For Each terminologyRow As DataRow In ontologyManager.TerminologiesTable.Rows
                        Dim terminologyId As String = CStr(terminologyRow(0))
                        Dim t As EiffelKernel.STRING_8 = Eiffel.String(terminologyId)

                        If EIF_adlInterface.ontology.has_constraint_binding(t, acCode) Then
                            Dim uri As String = EIF_adlInterface.ontology.constraint_binding(t, acCode).as_string.to_cil
                            ontologyManager.AddConstraintBinding(terminologyId, acCode.to_cil, uri)
                        End If
                    Next

                    acCodes.forth()
                Loop
            End If
        End Sub

        Public Overrides Sub PopulateTermsInLanguage(ByRef ontologyManager As OntologyManager, ByVal language As String)
            PopulateTermDefinitions(ontologyManager, language)
            PopulateConstraintDefinitions(ontologyManager, language)
        End Sub

        Public Overrides Sub PopulateAllTerms(ByRef ontologyManager As OntologyManager)
            If EIF_adlInterface.archetype_available Then
                PopulateLanguages(ontologyManager)
                PopulateTerminologies(ontologyManager)
                PopulateTermDefinitions(ontologyManager, "")
                PopulateTermBindings(ontologyManager)
                PopulateConstraintDefinitions(ontologyManager, "")
                PopulateConstraintBindings(ontologyManager)
            End If
        End Sub

        Sub New(ByRef an_adl_interface As openehr.adl_parser.interface.ADL_INTERFACE, Optional ByVal Replace As Boolean = False)
            EIF_adlInterface = an_adl_interface
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
'The Original Code is ADL_Ontology.vb.
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
