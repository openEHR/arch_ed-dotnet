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
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Ontology.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel
Imports EiffelList = EiffelSoftware.Library.Base.structures.list
Imports EiffelTable = EiffelSoftware.Library.Base.structures.table

Namespace ArchetypeEditor.ADL_Classes
    Friend Class ADL_Ontology
        Inherits Ontology
        Private EiffelCompiler As openehr.adl_parser.interface.ARCHETYPE_COMPILER
        Private sLanguageCode As String

        Protected ReadOnly Property Ontology() As openehr.openehr.am.archetype.ontology.ARCHETYPE_ONTOLOGY
            Get
                Return EiffelCompiler.archetype.ontology
            End Get
        End Property

        Public Overrides ReadOnly Property PrimaryLanguageCode() As String
            Get
                Return Ontology.primary_language.to_cil
            End Get
        End Property

        Public Overrides ReadOnly Property LanguageCode() As String
            Get
                Return EiffelCompiler.current_language.to_cil
            End Get
        End Property

        Public Overrides ReadOnly Property NumberOfSpecialisations() As Integer
            Get
                Return EiffelCompiler.archetype.specialisation_depth
            End Get
        End Property

        Public Overrides Function LanguageAvailable(ByVal code As String) As Boolean
            Return Ontology.has_language(EiffelKernel.Create.STRING_8.make_from_cil(code))
        End Function

        Public Overrides Function TerminologyAvailable(ByVal code As String) As Boolean
            Return Ontology.has_terminology(EiffelKernel.Create.STRING_8.make_from_cil(code))
        End Function

        Public Overrides Function TermForCode(ByVal Code As String, ByVal Language As String) As RmTerm

            If Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("at") Then
                If Ontology.has_term_code(EiffelKernel.Create.STRING_8.make_from_cil(Code)) Then
                    Return New ADL_Term(Ontology.term_definition(EiffelKernel.Create.STRING_8.make_from_cil(Language), EiffelKernel.Create.STRING_8.make_from_cil(Code)))
                End If
            ElseIf Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("ac") Then
                If Ontology.has_constraint_code(EiffelKernel.Create.STRING_8.make_from_cil(Code)) Then
                    Return New ADL_Term(Ontology.constraint_definition(EiffelKernel.Create.STRING_8.make_from_cil(Language), EiffelKernel.Create.STRING_8.make_from_cil(Code)))
                End If
            Else
                Debug.Assert(False, "Code type is not available")
            End If

            Return Nothing

        End Function

        Public Overrides Function IsMultiLanguage() As Boolean
            Return Ontology.languages_available.count > 1
        End Function

        Public Overrides Sub Reset()
            ' no action required
            ' Ontology.clear_terminology()
        End Sub

        Public Overrides Sub AddLanguage(ByVal code As String)
            Ontology.add_language(EiffelKernel.Create.STRING_8.make_from_cil(code))
        End Sub

        Public Overrides Sub AddTerminology(ByVal code As String)
            Dim s As EiffelKernel.STRING_8

            s = EiffelKernel.Create.STRING_8.make_from_cil(code)

            Try
                If Not Ontology.has_terminology(s) Then
                    Ontology.add_binding_terminology(s)
                End If
            Catch e As Exception
                Debug.Assert(False)
                MessageBox.Show(AE_Constants.Instance.Error_saving & " " & AE_Constants.Instance.Terminology, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Function HasTermBinding(ByVal a_terminology_id As String, ByVal a_path As String) As Boolean
            Return Ontology.has_term_binding( _
                EiffelKernel.Create.STRING_8.make_from_cil(a_terminology_id), _
                EiffelKernel.Create.STRING_8.make_from_cil(a_path))
        End Function

        Public Overrides Function HasConstraintBinding(ByVal a_terminology_id As String, ByVal a_path As String) As Boolean
            Return Ontology.has_constraint_binding( _
                EiffelKernel.Create.STRING_8.make_from_cil(a_terminology_id), _
                EiffelKernel.Create.STRING_8.make_from_cil(a_path))
        End Function

        Public Overrides Function TermBinding(ByVal a_terminology_id As String, ByVal a_path As String) As String
            Dim codePhrase As openehr.openehr.rm.data_types.text.CODE_PHRASE

            codePhrase = Ontology.term_binding( _
                EiffelKernel.Create.STRING_8.make_from_cil(a_terminology_id), _
                EiffelKernel.Create.STRING_8.make_from_cil(a_path))

            If codePhrase Is Nothing Then
                Return ""
            Else
                Return codePhrase.code_string.to_cil
            End If
        End Function

        Public Overrides Function ConstraintBinding(ByVal a_terminology_id As String, ByVal a_path As String) As String
            Dim codePhrase As openehr.openehr.rm.data_types.text.CODE_PHRASE

            codePhrase = Ontology.constraint_binding( _
                EiffelKernel.Create.STRING_8.make_from_cil(a_terminology_id), _
                EiffelKernel.Create.STRING_8.make_from_cil(a_path))

            If codePhrase Is Nothing Then
                Return ""
            Else
                Return codePhrase.code_string.to_cil
            End If
        End Function


        Public Overrides Sub AddorReplaceTermBinding(ByVal sTerminology As String, ByVal sPath As String, ByVal sCode As String, ByVal sRelease As String)
            ' CHANGE - Sam Heard 2005.05.15
            ' Added ascerts required for this piece of code to run successfully
            ' and try statement
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sPath <> "", "Path or nodeID are not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                Dim cp As openehr.openehr.rm.data_types.text.CODE_PHRASE
                Dim str As EiffelKernel.STRING_8

                str = EiffelKernel.Create.STRING_8.make_from_cil(sPath)
                cp = openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string(EiffelKernel.Create.STRING_8.make_from_cil(sTerminology & "::" & sCode))

                If Ontology.has_term_binding(cp.terminology_id.value, str) Then
                    Ontology.replace_term_binding(cp, str)
                Else
                    Ontology.add_term_binding(cp, str)
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveTermBinding(ByVal sTerminology As String, ByVal sCode As String)
            ' Added - Sam Heard 2005.05.15
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                Dim str, terminology As EiffelKernel.STRING_8

                str = EiffelKernel.Create.STRING_8.make_from_cil(sCode)
                terminology = EiffelKernel.Create.STRING_8.make_from_cil(sTerminology)

                If Ontology.has_term_binding(terminology, str) Then
                    Ontology.remove_term_binding(str, terminology)
                End If

            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub AddorReplaceConstraintBinding(ByVal sTerminology As String, ByVal sCode As String, ByVal sQuery As String, ByVal sRelease As String)
            ' CHANGE - Sam Heard 2006.05.07
            ' Added ascerts required for this piece of code to run successfully
            ' and try statement
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sQuery <> "", "Query is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                Dim cd, qry As EiffelKernel.STRING_8

                qry = EiffelKernel.Create.STRING_8.make_from_cil(sQuery)
                cd = EiffelKernel.Create.STRING_8.make_from_cil(sCode)

                If Ontology.has_constraint_binding(EiffelKernel.Create.STRING_8.make_from_cil(sTerminology), cd) Then
                    Ontology.replace_constraint_binding( _
                        openehr.common_libs.basic.Create.URI.make_from_string(qry), _
                        EiffelKernel.Create.STRING_8.make_from_cil(sTerminology), _
                        cd)
                Else
                    Ontology.add_constraint_binding( _
                        openehr.common_libs.basic.Create.URI.make_from_string(qry), _
                        EiffelKernel.Create.STRING_8.make_from_cil(sTerminology), _
                        cd)
                End If

            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveConstraintBinding(ByVal sTerminology As String, ByVal sCode As String)
            ' Added - Sam Heard 2005.05.15
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")
            ' release is not utilised at this point
            Try
                Dim str, terminology As EiffelKernel.STRING_8

                str = EiffelKernel.Create.STRING_8.make_from_cil(sCode)
                terminology = EiffelKernel.Create.STRING_8.make_from_cil(sTerminology)
                If Ontology.has_constraint_binding(terminology, str) Then
                    Ontology.remove_constraint_binding(str, terminology)
                End If

            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub SetLanguage(ByVal code As String)
            sLanguageCode = code
            EiffelCompiler.set_current_language(EiffelKernel.Create.STRING_8.make_from_cil(code))
        End Sub

        Public Overrides Function SpecialiseTerm(ByVal Text As String, ByVal Description As String, ByVal Id As String) As RmTerm
            ' increase the number of specialisations
            Dim an_Term As ADL_Term

            an_Term = New ADL_Term(NextSpecialisedId(Id))
            an_Term.Text = Text
            an_Term.Description = Description
            Me.AddTerm(an_Term)
            Return an_Term
        End Function

        Public Overrides Sub SetPrimaryLanguage(ByVal LanguageCode As String)
            ' sets the primary language of this archetype
            ' if this language is added to the available languages it adds it

            If LanguageCode = "" Then
                Return
            Else
                Ontology.set_primary_language(EiffelKernel.Create.STRING_8.make_from_cil(LanguageCode))
            End If
        End Sub

        Public Overrides Function NextTermId() As String
            Try
                Return Ontology.new_non_specialised_term_code.to_cil
            Catch e As Exception
                Debug.Assert(False, e.Message)
                Return ""
            End Try
        End Function

        Public Overrides Function NextConstraintId() As String
            Return Ontology.new_constraint_code.to_cil
        End Function

        Private Function NextSpecialisedId(ByVal ParentCode As String) As EiffelKernel.STRING_8
            Return Ontology.new_specialised_term_code(EiffelKernel.Create.STRING_8.make_from_cil(ParentCode))
        End Function

        Public Overrides Sub AddTerm(ByVal a_Term As RmTerm)
            Dim an_adl_Term As ADL_Term

            an_adl_Term = New ADL_Term(a_Term)

            If Not a_Term.isConstraint Then
                Try
                    If Not Ontology.has_term_code(an_adl_Term.EIF_Term.code) Then
                        Ontology.add_term_definition(EiffelCompiler.current_language, an_adl_Term.EIF_Term)
                    Else
                        Debug.Assert(False)
                    End If

                Catch e As Exception
                    Debug.Assert(False, e.ToString)
                End Try
            Else
                Try
                    If Not Ontology.has_constraint_code(an_adl_Term.EIF_Term.code) Then
                        Ontology.add_constraint_definition(EiffelCompiler.current_language, an_adl_Term.EIF_Term)
                    Else
                        Debug.Assert(False)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.ToString)
                End Try
            End If
        End Sub

        Public Overrides Sub ReplaceTerm(ByVal a_Term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
            Dim an_adl_Term As ADL_Term
            Dim language_code As EiffelKernel.STRING_8

            If Not a_Term.isConstraint Then
                an_adl_Term = New ADL_Term(a_Term)
                If a_Term.Language <> "" Then
                    language_code = EiffelKernel.Create.STRING_8.make_from_cil(a_Term.Language)
                Else
                    language_code = EiffelCompiler.current_language
                End If

                Try
                    If Ontology.has_term_code(an_adl_Term.EIF_Term.code) Then
                        Ontology.replace_term_definition(language_code, an_adl_Term.EIF_Term, ReplaceTranslations)
                    Else
                        Debug.Assert(False, "Term code is not available: " & an_adl_Term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Term is a constraint and should not be passed")
            End If
        End Sub

        'Not used as not safe - better to remove unused terms when saving the archetype

        'Public Overrides Sub DeleteTerm(ByVal a_Term As RmTerm)
        '    Dim an_adl_Term As ADL_Term

        '    an_adl_Term = New ADL_Term(a_Term)
        '    Try
        '        If Ontology.has_term_code(an_adl_Term.EIF_Term.code) Then
        '            If an_adl_Term.isConstraint Then
        '                Ontology.remove_constraint_definition(an_adl_Term.EIF_Term)
        '            Else
        '                Ontology.remove_term_definition(an_adl_Term.EIF_Term)
        '            End If
        '        Else
        '            Debug.Assert(False, "Term code is not available: " & an_adl_Term.Code)
        '        End If
        '    Catch e As Exception
        '        Debug.Assert(False, e.Message)
        '    End Try
        'End Sub

        Public Overrides Function HasTermCode(ByVal a_term_code As String) As Boolean
            If RmTerm.isValidTermCode(a_term_code) Then
                If Ontology.has_term_code(EiffelKernel.Create.STRING_8.make_from_cil(a_term_code)) Then
                    Return True
                ElseIf Ontology.has_constraint_code(EiffelKernel.Create.STRING_8.make_from_cil(a_term_code)) Then
                    Return True
                End If
            End If

            Return False
        End Function

        Public Overrides Sub AddConstraint(ByVal a_term As RmTerm)
            Dim an_adl_Term As ADL_Term

            If a_term.isConstraint Then
                an_adl_Term = New ADL_Term(a_term)

                Try
                    If Not Ontology.has_constraint_code(an_adl_Term.EIF_Term.code) Then
                        Ontology.add_constraint_definition(EiffelCompiler.current_language, an_adl_Term.EIF_Term)
                    Else
                        Debug.Assert(False, "Constraint code not available: " & an_adl_Term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Code is not a constraint code: " & a_term.Code)
            End If
        End Sub

        Public Overrides Sub ReplaceConstraint(ByVal a_term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
            Dim an_adl_Term As ADL_Term

            If a_term.isConstraint Then
                an_adl_Term = New ADL_Term(a_term)

                Try
                    If Ontology.has_constraint_code(an_adl_Term.EIF_Term.code) Then
                        Ontology.replace_constraint_definition(EiffelCompiler.current_language, an_adl_Term.EIF_Term, ReplaceTranslations)
                    Else
                        Debug.Assert(False, "Constraint code not available: " & an_adl_Term.Code)
                    End If
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                End Try
            Else
                Debug.Assert(False, "Code is not a constraint code: " & a_term.Code)
            End If
        End Sub

        Public Sub populate_languages(ByRef TheOntologyManager As OntologyManager)

            If Not Ontology.languages_available.empty() Then
                Dim i As Integer
                Dim s As String

                'A new ontology always adds the current language - but this may not be available in the archetype
                ' so clear ..
                TheOntologyManager.LanguagesTable.Clear()

                For i = Ontology.languages_available.lower() To Ontology.languages_available.upper()
                    s = CType(Ontology.languages_available.i_th(i), EiffelKernel.STRING_8).to_cil()
                    TheOntologyManager.AddLanguage(s)
                Next

            End If

        End Sub

        Private Sub populate_terminologies(ByRef TheOntologyManager As OntologyManager)
            ' populate the terminology table in TermLookUp
            If Not Ontology.terminologies_available.empty() Then
                Dim i As Integer
                Dim s, t As String

                For i = Ontology.terminologies_available.lower() To Ontology.terminologies_available.upper()
                    s = CType(Ontology.terminologies_available.i_th(i), EiffelKernel.STRING_8).to_cil()
                    t = TheOntologyManager.GetTerminologyDescription(s)
                    TheOntologyManager.AddTerminology(s, t)
                Next
            End If
        End Sub

        Private Sub populate_term_definitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
            ' populate the TermDefinitions table in TermLookUp

            If Not Ontology.term_definitions.empty() Then
                Dim d_row, selected_row As DataRow
                Dim s As EiffelKernel.STRING_8
                Dim a_term As ADL_Term
                Dim linklist As EiffelList.LINKED_LIST_REFERENCE

                linklist = Ontology.term_codes
                linklist.start()

                Do While Not linklist.off
                    s = CType(linklist.active.item, EiffelKernel.STRING_8)
                    If LanguageCode = "" Then
                        ' set the term for all languages
                        For Each selected_row In TheOntologyManager.LanguagesTable.Rows
                            ' take each term from the ADL ontology
                            a_term = New ADL_Term(Ontology.term_definition(EiffelKernel.Create.STRING_8.make_from_cil(selected_row(0)), s))
                            ' and if it is not an internal ID for machine processing
                            d_row = TheOntologyManager.TermDefinitionTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = a_term.Code
                            d_row(2) = a_term.Text
                            d_row(3) = a_term.Description
                            d_row(4) = a_term.Comment
                            ' add it to the GUI ontology
                            TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                        Next
                    Else
                        ' just do it for the new language
                        a_term = New ADL_Term(Ontology.term_definition(EiffelKernel.Create.STRING_8.make_from_cil(LanguageCode), s))
                        ' and if it is not an internal ID for machine processing
                        d_row = TheOntologyManager.TermDefinitionTable.NewRow
                        d_row(0) = LanguageCode
                        d_row(1) = a_term.Code
                        d_row(2) = a_term.Text
                        d_row(3) = a_term.Description
                        d_row(4) = a_term.Comment
                        ' add it to the GUI ontology
                        TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                    End If
                    linklist.forth()
                Loop
            End If
        End Sub

        Private Sub populate_term_bindings(ByRef TheOntologyManager As OntologyManager)
            ' populate the TermBindings table in TermLookUp

            If Not Ontology.term_bindings.empty() Then
                Dim d_row, selected_row As DataRow
                Dim terminology, code As EiffelKernel.STRING_8
                Dim cp As openehr.openehr.rm.data_types.text.CODE_PHRASE
                Dim Bindings As EiffelTable.HASH_TABLE_REFERENCE_REFERENCE

                For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                    terminology = EiffelKernel.Create.STRING_8.make_from_cil(selected_row(0))
                    If Ontology.has_term_bindings(terminology) Then
                        Bindings = Ontology.term_bindings_for_terminology(terminology)
                        Bindings.start()
                        Do While Not Bindings.off
                            cp = Bindings.item_for_iteration
                            code = Bindings.key_for_iteration
                            d_row = TheOntologyManager.TermBindingsTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = code.to_cil
                            d_row(2) = cp.code_string
                            TheOntologyManager.TermBindingsTable.Rows.Add(d_row)
                            Bindings.forth()
                        Loop
                    End If
                Next
            End If
        End Sub

        Private Sub populate_constraint_definitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
            ' populate the ConstraintDefinitions table in TermLookUp

            If Not Ontology.constraint_definitions.empty() Then
                Dim d_row, selected_row As DataRow
                Dim s As EiffelKernel.STRING_8
                Dim a_term As ADL_Term
                Dim linklist As EiffelList.LINKED_LIST_REFERENCE

                linklist = Ontology.constraint_codes
                linklist.start()

                Do While Not linklist.off
                    s = CType(linklist.active.item, EiffelKernel.STRING_8)

                    If LanguageCode = "" Then
                        ' set the constraints for all languages
                        For Each selected_row In TheOntologyManager.LanguagesTable.Rows
                            ' take each constraint from the ADL ontology
                            a_term = New ADL_Term(Ontology.constraint_definition(EiffelKernel.Create.STRING_8.make_from_cil(selected_row(0)), s))
                            d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = a_term.Code
                            d_row(2) = a_term.Text
                            d_row(3) = a_term.Description
                            'Add it to the GUI
                            TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                        Next
                    Else
                        'Do it for the new language
                        a_term = New ADL_Term(Ontology.constraint_definition(EiffelKernel.Create.STRING_8.make_from_cil(LanguageCode), s))
                        d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                        d_row(0) = LanguageCode
                        d_row(1) = a_term.Code
                        d_row(2) = a_term.Text
                        d_row(3) = a_term.Description
                        'Add it for the ontology
                        TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                    End If

                    linklist.forth()
                Loop
            End If
        End Sub

        Private Sub populate_constraint_bindings(ByRef TheOntologyManager As OntologyManager)
            ' populate the ConstraintBindings table in TermLookUp

            If Not Ontology.constraint_bindings.empty() Then
                Dim d_row, selected_row As DataRow
                Dim s As EiffelKernel.STRING_8
                Dim a_query As EiffelKernel.STRING_8
                Dim linklist As EiffelList.LINKED_LIST_REFERENCE

                linklist = Ontology.constraint_codes
                linklist.start()

                Do While Not linklist.off
                    s = CType(linklist.active.item, EiffelKernel.STRING_8)

                    For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                        Try
                            If Ontology.has_constraint_bindings(EiffelKernel.Create.STRING_8.make_from_cil(selected_row(0))) Then
                                If Ontology.has_constraint_binding(EiffelKernel.Create.STRING_8.make_from_cil(selected_row(0)), s) Then
                                    a_query = Ontology.constraint_binding(EiffelKernel.Create.STRING_8.make_from_cil(selected_row(0)), s).as_string
                                    d_row = TheOntologyManager.ConstraintBindingsTable.NewRow
                                    d_row(0) = selected_row(0)
                                    d_row(1) = s.to_cil
                                    d_row(2) = a_query.to_cil
                                    TheOntologyManager.ConstraintBindingsTable.Rows.Add(d_row)
                                End If
                            End If
                        Catch ex As Exception
                            'FIXME - to catch if couldn't add d_row for debug - probably remove try in longterm
                            Debug.Assert(False, "Error adding constraint binding")
                        End Try
                    Next

                    linklist.forth()
                Loop
            End If
        End Sub

        Public Overrides Sub PopulateTermsInLanguage(ByRef TheOntologyManager As OntologyManager, ByVal LanguageCode As String)
            populate_term_definitions(TheOntologyManager, LanguageCode)
            populate_constraint_definitions(TheOntologyManager, LanguageCode)

        End Sub

        Public Overrides Sub PopulateAllTerms(ByRef TheOntologyManager As OntologyManager)
            If Not EiffelCompiler.archetype Is Nothing Then
                populate_languages(TheOntologyManager)
                populate_terminologies(TheOntologyManager)
                populate_term_definitions(TheOntologyManager)
                populate_term_bindings(TheOntologyManager)
                populate_constraint_definitions(TheOntologyManager)
                populate_constraint_bindings(TheOntologyManager)
            End If

        End Sub

        Sub New(ByRef compiler As openehr.adl_parser.interface.ARCHETYPE_COMPILER, Optional ByVal Replace As Boolean = False)
            EiffelCompiler = compiler
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
