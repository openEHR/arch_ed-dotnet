'
'
'	component:   "openEHR Archetype Project"
'	description: "Specialisation of the Ontology class to work with ADL"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/ADL_Classes/SCCS/s.ADL_Ontology.vb $"
'	revision:    "$Revision$"
'	last_change: "$Date$"
'
'

Option Explicit On 

Friend Class ADL_Ontology
    Inherits Ontology
    Private EIF_adlInterface As openehr.adl_parser.interface.ADL_INTERFACE
    Private sLanguageCode As String

    Public Overrides ReadOnly Property PrimaryLanguageCode() As String
        Get
            Return EIF_adlInterface.ontology.primary_language.to_cil
        End Get
    End Property
    Public Overrides ReadOnly Property LanguageCode() As String
        Get
            Return EIF_adlInterface.language.to_cil
        End Get
    End Property
    Public Overrides ReadOnly Property NumberOfSpecialisations() As Integer
        Get
            Return EIF_adlInterface.adl_engine.archetype.specialisation_depth
        End Get
    End Property

    Public Overrides Function LanguageAvailable(ByVal code As String) As Boolean
        If EIF_adlInterface.ontology.has_language(openehr.base.kernel.Create.STRING.make_from_cil(code)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function TerminologyAvailable(ByVal code As String) As Boolean
        If EIF_adlInterface.ontology.has_terminology(openehr.base.kernel.Create.STRING.make_from_cil(code)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function TermForCode(ByVal Code As String, ByVal Language As String) As RmTerm
        Dim a_term As ADL_Term

        If Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("at") Then
            If EIF_adlInterface.ontology.has_term_code(openehr.base.kernel.Create.STRING.make_from_cil(Code)) Then
                Return New ADL_Term(EIF_adlInterface.ontology.term_definition(openehr.base.kernel.Create.STRING.make_from_cil(Language), openehr.base.kernel.Create.STRING.make_from_cil(Code)))
            End If
        ElseIf Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("ac") Then
            If EIF_adlInterface.ontology.has_constraint_code(openehr.base.kernel.Create.STRING.make_from_cil(Code)) Then
                Return New ADL_Term(EIF_adlInterface.ontology.constraint_definition(openehr.base.kernel.Create.STRING.make_from_cil(Language), openehr.base.kernel.Create.STRING.make_from_cil(Code)))
            End If
        Else
            Debug.Assert(False, "Code type is not available")
        End If

        Return Nothing

    End Function

    Public Overrides Function IsMultiLanguage() As Boolean
        Return EIF_adlInterface.ontology.languages_available.count > 1
    End Function

    Public Overrides Sub Reset()
        EIF_adlInterface.ontology.clear_terminology()
    End Sub

    Public Overrides Sub AddLanguage(ByVal code As String)
        EIF_adlInterface.ontology.add_language_available(openehr.base.kernel.Create.STRING.make_from_cil(code))
    End Sub

    Public Overrides Sub AddTerminology(ByVal code As String)
        Dim s As openehr.base.kernel.STRING

        s = openehr.base.kernel.Create.STRING.make_from_cil(code)

        Try
            If Not EIF_adlInterface.ontology.has_terminology(s) Then
                EIF_adlInterface.ontology.add_binding_terminology(s)
            End If
        Catch e As Exception
            Debug.Assert(False)
            MessageBox.Show(AE_Constants.Instance.Error_saving & " " & AE_Constants.Instance.Terminology, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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
            Dim str As openehr.base.kernel.STRING

            str = openehr.base.kernel.Create.STRING.make_from_cil(sPath)
            cp = openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string(openehr.base.kernel.Create.STRING.make_from_cil(sTerminology & "::" & sCode))
            If EIF_adlInterface.ontology.has_term_binding(cp.terminology_id.as_string, str) Then
                EIF_adlInterface.ontology.replace_term_binding(cp, str)
            Else
                EIF_adlInterface.ontology.add_term_binding(cp, str)
            End If

        Catch e As System.Exception
            MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Overrides Sub SetLanguage(ByVal code As String)
        sLanguageCode = code
        EIF_adlInterface.set_language(openehr.base.kernel.Create.STRING.make_from_cil(code))
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
            EIF_adlInterface.ontology.set_primary_language(openehr.base.kernel.Create.STRING.make_from_cil(LanguageCode))
        End If
    End Sub

    Public Overrides Function NextTermId() As String
        Try
            Return EIF_adlInterface.ontology.new_non_specialised_term_code.to_cil
        Catch e As Exception
            Debug.Assert(False, e.Message)
        End Try
    End Function

    Public Overrides Function NextConstraintId() As String
        Return EIF_adlInterface.ontology.new_constraint_code.to_cil
    End Function

    Private Function NextSpecialisedId(ByVal ParentCode As String) As openehr.base.kernel.STRING
        Return EIF_adlInterface.ontology.new_specialised_term_code(openehr.base.kernel.Create.STRING.make_from_cil(ParentCode))
    End Function

    Public Overrides Sub AddTerm(ByVal a_Term As RmTerm)
        Dim an_adl_Term As ADL_Term

        an_adl_Term = New ADL_Term(a_Term)

        If Not a_Term.isConstraint Then

            Try
                If Not EIF_adlInterface.ontology.has_term_code(an_adl_Term.EIF_Term.code) Then
                    EIF_adlInterface.ontology.add_term_definition(EIF_adlInterface.language, an_adl_Term.EIF_Term)
                Else
                    Debug.Assert(False)
                End If

            Catch e As Exception
                Debug.Assert(False, e.ToString)
            End Try

        Else

            Try
                If Not EIF_adlInterface.ontology.has_constraint_code(an_adl_Term.EIF_Term.code) Then
                    EIF_adlInterface.ontology.add_constraint_definition(EIF_adlInterface.language, an_adl_Term.EIF_Term)
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
        Dim language_code As openehr.base.kernel.STRING

        If Not a_Term.isConstraint Then
            an_adl_Term = New ADL_Term(a_Term)
            If a_Term.Language <> "" Then
                language_code = openehr.base.kernel.Create.STRING.make_from_cil(a_Term.Language)
            Else
                language_code = EIF_adlInterface.language
            End If

            Try
                If EIF_adlInterface.ontology.has_term_code(an_adl_Term.EIF_Term.code) Then
                    EIF_adlInterface.ontology.replace_term_definition(language_code, an_adl_Term.EIF_Term, ReplaceTranslations)
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

    Public Overrides Sub AddConstraint(ByVal a_term As RmTerm)
        Dim an_adl_Term As ADL_Term

        If a_term.isConstraint Then
            an_adl_Term = New ADL_Term(a_term)
            Try
                If Not EIF_adlInterface.ontology.has_constraint_code(an_adl_Term.EIF_Term.code) Then
                    EIF_adlInterface.ontology.add_constraint_definition(EIF_adlInterface.language, an_adl_Term.EIF_Term)
                Else
                    Debug.Assert(False, "Constraint code not available: " & an_adl_Term.Code)
                End If
            Catch e As Exception
                Debug.Assert(False, e.Message)
            End Try
        Else
            Debug.Assert(False, "Code is not a constraint code: " & an_adl_Term.Code)
        End If
    End Sub

    Public Overrides Sub ReplaceConstraint(ByVal a_term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
        Dim an_adl_Term As ADL_Term

        If a_term.isConstraint Then
            an_adl_Term = New ADL_Term(a_term)

            Try
                If EIF_adlInterface.ontology.has_constraint_code(an_adl_Term.EIF_Term.code) Then
                    EIF_adlInterface.ontology.replace_constraint_definition(EIF_adlInterface.language, an_adl_Term.EIF_Term, ReplaceTranslations)
                Else
                    Debug.Assert(False, "Constraint code not available: " & an_adl_Term.Code)
                End If
            Catch e As Exception
                Debug.Assert(False, e.Message)
            End Try
        Else
            Debug.Assert(False, "Code is not a constraint code: " & an_adl_Term.Code)
        End If
    End Sub

    Public Sub populate_languages(ByRef TheOntologyManager As OntologyManager)

        If EIF_adlInterface.ontology.languages_available.empty() = False Then
            Dim i As Integer
            Dim s As String

            'A new ontology always adds the current language - but this may not be available in the archetype
            ' so clear ..
            TheOntologyManager.LanguagesTable.Clear()

            For i = EIF_adlInterface.ontology.languages_available.lower() To EIF_adlInterface.ontology.languages_available.upper()
                s = CType(EIF_adlInterface.ontology.languages_available.i_th(i), openehr.base.kernel.STRING).to_cil()
                TheOntologyManager.AddLanguage(s)
            Next

        End If

    End Sub

    Private Sub populate_terminologies(ByRef TheOntologyManager As OntologyManager)
        ' populate the terminology table in TermLookUp
        If EIF_adlInterface.ontology.terminologies_available.empty() = False Then
            Dim i As Integer
            Dim d_row As DataRow
            Dim s, t As String

            For i = EIF_adlInterface.ontology.terminologies_available.lower() To EIF_adlInterface.ontology.terminologies_available.upper()

                s = CType(EIF_adlInterface.ontology.terminologies_available.i_th(i), openehr.base.kernel.STRING).to_cil()
                t = TheOntologyManager.GetTerminologyDescription(s)
                TheOntologyManager.AddTerminology(s, t)
            Next
        End If
    End Sub

    Private Sub populate_term_definitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
        ' populate the TermDefinitions table in TermLookUp

        If EIF_adlInterface.ontology.term_definitions.empty() = False Then
            Dim i As Integer
            Dim d_row, selected_row As DataRow
            Dim s As openehr.base.kernel.STRING
            Dim a_term As ADL_Term
            Dim linklist As openehr.base.structures.list.LINKED_LIST_ANY

            linklist = EIF_adlInterface.ontology.term_codes

            linklist.start()
            Do While Not linklist.off
                s = CType(linklist.active.item, openehr.base.kernel.STRING)
                If LanguageCode = "" Then
                    ' set the term for all languages
                    For Each selected_row In TheOntologyManager.LanguagesTable.Rows
                        ' take each term from the ADL ontology
                        a_term = New ADL_Term(EIF_adlInterface.ontology.term_definition(openehr.base.kernel.Create.STRING.make_from_cil(selected_row(0)), s))
                        ' and if it is not an internal ID for machine processing
                        If InStr(a_term.Description, "@ internal @") = 0 Then
                            d_row = TheOntologyManager.TermDefinitionTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = a_term.Code
                            d_row(2) = a_term.Text
                            d_row(3) = a_term.Description
                            ' add it to the GUI ontology
                            TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                        End If
                    Next
                Else
                    ' just do it for the new language
                    a_term = New ADL_Term(EIF_adlInterface.ontology.term_definition(openehr.base.kernel.Create.STRING.make_from_cil(LanguageCode), s))
                    ' and if it is not an internal ID for machine processing
                    If InStr(a_term.Description, "@ internal @") = 0 Then
                        d_row = TheOntologyManager.TermDefinitionTable.NewRow
                        d_row(0) = LanguageCode
                        d_row(1) = a_term.Code
                        d_row(2) = a_term.Text
                        d_row(3) = a_term.Description
                        ' add it to the GUI ontology
                        TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                    End If
                End If
                linklist.forth()
            Loop
        End If
    End Sub

    Private Sub populate_term_bindings(ByRef TheOntologyManager As OntologyManager)
        ' populate the TermBindings table in TermLookUp

        If EIF_adlInterface.ontology.term_bindings.empty() = False Then
            Dim i As Integer
            Dim d_row, selected_row As DataRow
            Dim terminology, code As openehr.base.kernel.STRING
            Dim cp As openehr.openehr.rm.data_types.text.CODE_PHRASE
            Dim Bindings As openehr.Base.structures.table.HASH_TABLE_ANY_ANY


            For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                terminology = openehr.base.kernel.Create.STRING.make_from_cil(selected_row(0))
                If EIF_adlInterface.ontology.has_term_bindings(terminology) Then
                    Bindings = EIF_adlInterface.ontology.term_bindings_for_terminology(terminology)
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

        If EIF_adlInterface.ontology.constraint_definitions.empty() = False Then
            Dim i As Integer
            Dim d_row, selected_row As DataRow
            Dim s As openehr.base.kernel.STRING
            Dim a_term As ADL_Term
            Dim linklist As openehr.base.structures.list.LINKED_LIST_ANY

            linklist = EIF_adlInterface.ontology.constraint_codes

            linklist.start()
            Do While Not linklist.off
                s = CType(linklist.active.item, openehr.base.kernel.STRING)
                If LanguageCode = "" Then
                    For Each selected_row In TheOntologyManager.LanguagesTable.Rows
                        a_term = New ADL_Term(EIF_adlInterface.ontology.constraint_definition(openehr.base.kernel.Create.STRING.make_from_cil(selected_row(0)), s))
                        d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                        d_row(0) = selected_row(0)
                        d_row(1) = a_term.Code
                        d_row(2) = a_term.Text
                        d_row(3) = a_term.Description
                        TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                    Next
                Else
                    a_term = New ADL_Term(EIF_adlInterface.ontology.constraint_definition(openehr.base.kernel.Create.STRING.make_from_cil(LanguageCode), s))
                    d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                    d_row(0) = LanguageCode
                    d_row(1) = a_term.Code
                    d_row(2) = a_term.Text
                    d_row(3) = a_term.Description
                    TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                End If
                linklist.forth()
            Loop
        End If
    End Sub

    Private Sub populate_constraint_bindings(ByRef TheOntologyManager As OntologyManager)
        ' populate the ConstraintBindings table in TermLookUp

        If EIF_adlInterface.ontology.constraint_bindings.empty() = False Then
            Dim i As Integer
            Dim d_row, selected_row As DataRow
            Dim s As openehr.base.kernel.STRING
            Dim a_term As openehr.base.kernel.STRING
            Dim linklist As openehr.base.structures.list.LINKED_LIST_ANY

            linklist = EIF_adlInterface.ontology.constraint_codes

            linklist.start()
            Do While Not linklist.off
                s = CType(linklist.active.item, openehr.base.kernel.STRING)
                For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                    Try
                        If EIF_adlInterface.ontology.has_constraint_bindings(openehr.base.kernel.Create.STRING.make_from_cil(selected_row(0))) Then
                            If EIF_adlInterface.ontology.has_constraint_binding(openehr.base.kernel.Create.STRING.make_from_cil(selected_row(0)), s) Then
                                a_term = EIF_adlInterface.ontology.constraint_binding(openehr.base.kernel.Create.STRING.make_from_cil(selected_row(0)), s)
                                d_row = TheOntologyManager.ConstraintBindingsTable.NewRow
                                d_row(0) = selected_row(0)
                                d_row(1) = s.to_cil
                                d_row(2) = a_term.to_cil
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
        If EIF_adlInterface.archetype_available Then
            populate_languages(TheOntologyManager)
            populate_terminologies(TheOntologyManager)
            populate_term_definitions(TheOntologyManager)
            populate_term_bindings(TheOntologyManager)
            populate_constraint_definitions(TheOntologyManager)
            populate_constraint_bindings(TheOntologyManager)
        End If

    End Sub

    Sub New(ByRef an_adl_interface As openehr.adl_parser.interface.ADL_INTERFACE, Optional ByVal Replace As Boolean = False)
        EIF_adlInterface = an_adl_interface
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
