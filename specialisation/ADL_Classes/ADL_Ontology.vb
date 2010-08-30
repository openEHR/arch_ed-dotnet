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
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports EiffelList = EiffelSoftware.Library.Base.Structures.List
Imports EiffelTable = EiffelSoftware.Library.Base.Structures.Table
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes
    Friend Class ADL_Ontology
        Inherits Ontology

        Private archetypeParser As AdlParser.ArchetypeParser

        Public ReadOnly Property DifferentialArchetype() As AdlParser.DifferentialArchetype
            Get
                Return archetypeParser.DifferentialArchetype
            End Get
        End Property

        Public ReadOnly Property DifferentialOntology() As AdlParser.DifferentialArchetypeOntology
            Get
                Debug.Assert(Not DifferentialArchetype Is Nothing)
                Return CType(DifferentialArchetype.Ontology, AdlParser.DifferentialArchetypeOntology)
            End Get
        End Property

        Public Overrides ReadOnly Property PrimaryLanguageCode() As String
            Get
                Return DifferentialOntology.OriginalLanguage.ToCil
            End Get
        End Property

        Public Overrides ReadOnly Property LanguageCode() As String
            Get
                Return archetypeParser.AppRoot.CurrentLanguage.ToCil
            End Get
        End Property

        Public Overrides ReadOnly Property NumberOfSpecialisations() As Integer
            Get
                If DifferentialArchetype Is Nothing Then
                    NumberOfSpecialisations = 0
                Else
                    NumberOfSpecialisations = DifferentialArchetype.SpecialisationDepth
                End If
            End Get
        End Property

        Public Overrides Function LanguageAvailable(ByVal code As String) As Boolean
            Return Not DifferentialArchetype Is Nothing AndAlso DifferentialOntology.HasLanguage(Eiffel.String(code))
        End Function

        Public Overrides Function TerminologyAvailable(ByVal code As String) As Boolean
            Return Not DifferentialArchetype Is Nothing AndAlso DifferentialOntology.HasTerminology(Eiffel.String(code))
        End Function

        Public Overrides Function TermForCode(ByVal Code As String, ByVal Language As String) As RmTerm
            Dim result As RmTerm = Nothing

            If Not DifferentialArchetype Is Nothing Then
                If Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("at") Then
                    If DifferentialOntology.HasTermCode(Eiffel.String(Code)) Then
                        result = New ADL_Term(DifferentialOntology.TermDefinition(Eiffel.String(Language), Eiffel.String(Code)))
                    End If
                ElseIf Code.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("ac") Then
                    If DifferentialOntology.HasConstraintCode(Eiffel.String(Code)) Then
                        result = New ADL_Term(DifferentialOntology.ConstraintDefinition(Eiffel.String(Language), Eiffel.String(Code)))
                    End If
                End If
            End If

            Return result
        End Function

        Public Overrides Function IsMultiLanguage() As Boolean
            Return Not DifferentialArchetype Is Nothing AndAlso DifferentialOntology.LanguagesAvailable.Upper > 1
        End Function

        Public Overrides Sub Reset()
            ' no action required
        End Sub

        Public Overrides Sub AddLanguage(ByVal code As String)
            If Not DifferentialArchetype Is Nothing Then
                DifferentialOntology.AddLanguage(Eiffel.String(code))
            End If
        End Sub

        Public Overrides Sub AddTerminology(ByVal code As String)
            Dim s As EiffelKernel.String_8 = Eiffel.String(code)

            Try
                If Not DifferentialArchetype Is Nothing AndAlso Not DifferentialOntology.HasTerminology(s) Then
                    DifferentialOntology.AddBindingTerminology(s)
                End If
            Catch e As Exception
                Debug.Assert(False)
                MessageBox.Show(AE_Constants.Instance.Error_saving & " " & AE_Constants.Instance.Terminology, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Function HasTermBinding(ByVal terminologyId As String, ByVal path As String) As Boolean
            Return Not DifferentialArchetype Is Nothing AndAlso DifferentialOntology.HasTermBinding(Eiffel.String(terminologyId), Eiffel.String(path))
        End Function

        Public Overrides Function HasConstraintBinding(ByVal terminologyId As String, ByVal path As String) As Boolean
            Return Not DifferentialArchetype Is Nothing AndAlso DifferentialOntology.HasConstraintBinding(Eiffel.String(terminologyId), Eiffel.String(path))
        End Function

        Public Overrides Function TermBinding(ByVal terminologyId As String, ByVal path As String) As String
            Dim result As String = ""
            Dim codePhrase As AdlParser.CodePhrase

            If Not DifferentialArchetype Is Nothing Then
                codePhrase = DifferentialOntology.TermBinding(Eiffel.String(terminologyId), Eiffel.String(path))

                If Not codePhrase Is Nothing Then
                    result = codePhrase.CodeString.ToCil
                End If
            End If

            Return result
        End Function

        Public Overrides Function ConstraintBinding(ByVal terminologyId As String, ByVal path As String) As String
            Dim result As String = ""
            Dim codePhrase As AdlParser.CodePhrase

            If Not DifferentialArchetype Is Nothing Then
                codePhrase = DifferentialOntology.ConstraintBinding(Eiffel.String(terminologyId), Eiffel.String(path))

                If Not codePhrase Is Nothing Then
                    result = codePhrase.CodeString.ToCil
                End If
            End If

            Return result
        End Function

        Public Overrides Sub AddorReplaceTermBinding(ByVal sTerminology As String, ByVal sPath As String, ByVal sCode As String, ByVal sRelease As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sPath <> "", "Path or nodeID are not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            ' release is not utilised at this point
            Try
                If Not DifferentialArchetype Is Nothing Then
                    Dim str As EiffelKernel.String_8 = Eiffel.String(sPath)

                    If sRelease <> "" Then
                        sTerminology = String.Format("{0}({1})", sTerminology, sRelease)
                    End If

                    Dim cp As AdlParser.CodePhrase = AdlParser.Create.CodePhrase.MakeFromString(Eiffel.String(sTerminology & "::" & sCode))

                    If DifferentialOntology.HasTermBinding(cp.TerminologyId.Value, str) Then
                        DifferentialOntology.ReplaceTermBinding(cp, str)
                    Else
                        DifferentialOntology.AddTermBinding(cp, str)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveTermBinding(ByVal sTerminology As String, ByVal sCode As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            ' release is not utilised at this point
            Try
                If Not DifferentialArchetype Is Nothing Then
                    Dim str As EiffelKernel.String_8 = Eiffel.String(sCode)
                    Dim terminology As EiffelKernel.String_8 = Eiffel.String(sTerminology)

                    If DifferentialOntology.HasTermBinding(terminology, str) Then
                        DifferentialOntology.RemoveTermBinding(str, terminology)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub AddorReplaceConstraintBinding(ByVal sTerminology As String, ByVal sCode As String, ByVal sQuery As String, ByVal sRelease As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sQuery <> "", "Query is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            ' release is not utilised at this point
            Try
                If Not DifferentialArchetype Is Nothing Then
                    Dim cd As EiffelKernel.String_8 = Eiffel.String(sCode)
                    Dim qry As EiffelKernel.String_8 = Eiffel.String(sQuery)

                    If sRelease <> "" Then
                        sTerminology = String.Format("{0}({1})", sTerminology, sRelease)
                    End If

                    If DifferentialOntology.HasConstraintBinding(Eiffel.String(sTerminology), cd) Then
                        DifferentialOntology.ReplaceConstraintBinding(AdlParser.Create.Uri.MakeFromString(qry), Eiffel.String(sTerminology), cd)
                    Else
                        DifferentialOntology.AddConstraintBinding(AdlParser.Create.Uri.MakeFromString(qry), Eiffel.String(sTerminology), cd)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub RemoveConstraintBinding(ByVal sTerminology As String, ByVal sCode As String)
            Debug.Assert(sCode <> "", "Code is not set")
            Debug.Assert(sTerminology <> "", "TerminologyID is not set")

            ' release is not utilised at this point
            Try
                If Not DifferentialArchetype Is Nothing Then
                    Dim str As EiffelKernel.String_8 = Eiffel.String(sCode)
                    Dim terminology As EiffelKernel.String_8 = Eiffel.String(sTerminology)

                    If DifferentialOntology.HasConstraintBinding(terminology, str) Then
                        DifferentialOntology.RemoveConstraintBinding(str, terminology)
                    End If
                End If
            Catch e As System.Exception
                MessageBox.Show(e.Message, "ADL DLL error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overrides Sub SetLanguage(ByVal code As String)
            archetypeParser.AppRoot.SetCurrentLanguage(Eiffel.String(code))
        End Sub

        Public Overrides Function SpecialiseTerm(ByVal Text As String, ByVal Description As String, ByVal Id As String) As RmTerm
            ' increase the number of specialisations
            Dim result As New ADL_Term(NextSpecialisedId(Id))
            result.Text = Text
            result.Description = Description
            AddTerm(result)
            Return result
        End Function

        Public Overrides Sub SetPrimaryLanguage(ByVal LanguageCode As String)
            ' sets the primary language of this archetype
            ' if this language is added to the available languages it adds it

            If LanguageCode <> "" And Not DifferentialArchetype Is Nothing Then
                DifferentialOntology.SetOriginalLanguage(Eiffel.String(LanguageCode))
            End If
        End Sub

        Public Overrides Function NextTermId() As String
            Dim result As String = ""

            Try
                If Not DifferentialArchetype Is Nothing Then
                    result = DifferentialOntology.NewNonSpecialisedTermCode.ToCil
                End If
            Catch e As Exception
                Debug.Assert(False, e.Message)
                result = ""
            End Try

            Return result
        End Function

        Public Overrides Function NextConstraintId() As String
            Dim result As String = ""

            If Not DifferentialArchetype Is Nothing Then
                result = DifferentialOntology.NewNonSpecialisedConstraintCode.ToCil
            End If

            Return result
        End Function

        Private Function NextSpecialisedId(ByVal ParentCode As String) As EiffelKernel.String_8
            Dim result As EiffelKernel.String_8

            If Not DifferentialArchetype Is Nothing Then
                result = DifferentialOntology.NewSpecialisedTermCode(Eiffel.String(ParentCode))
            Else
                result = EiffelKernel.Create.String_8.MakeEmpty
            End If

            Return result
        End Function

        Public Overrides Sub AddTerm(ByVal term As RmTerm)
            If Not DifferentialArchetype Is Nothing Then
                Dim adlTerm As New ADL_Term(term)

                If Not term.IsConstraint Then
                    Try
                        If Not DifferentialOntology.HasTermCode(adlTerm.EIF_Term.Code) Then
                            DifferentialOntology.AddTermDefinition(archetypeParser.AppRoot.CurrentLanguage, adlTerm.EIF_Term)
                        Else
                            Debug.Assert(False)
                        End If

                    Catch e As Exception
                        Debug.Assert(False, e.ToString)
                    End Try
                Else
                    Try
                        If Not DifferentialOntology.HasConstraintCode(adlTerm.EIF_Term.Code) Then
                            DifferentialOntology.AddConstraintDefinition(archetypeParser.AppRoot.CurrentLanguage, adlTerm.EIF_Term)
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
            If Not DifferentialArchetype Is Nothing Then
                Dim adlTerm As ADL_Term
                Dim languageCode As EiffelKernel.String_8

                If Not term.IsConstraint Then
                    adlTerm = New ADL_Term(term)

                    If term.Language <> "" Then
                        languageCode = Eiffel.String(term.Language)
                    Else
                        languageCode = archetypeParser.AppRoot.CurrentLanguage
                    End If

                    Try
                        If DifferentialOntology.HasTermCode(adlTerm.EIF_Term.Code) Then
                            DifferentialOntology.ReplaceTermDefinition(languageCode, adlTerm.EIF_Term, replaceTranslations)
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

        'Not used as not safe - better to remove unused terms when saving the archetype

        'Public Overrides Sub DeleteTerm(ByVal term As RmTerm)
        '    Dim adlTerm As ADL_Term

        '    adlTerm = New ADL_Term(term)
        '    Try
        '        If DifferentialOntology.HasTermCode(adlTerm.EIF_Term.code) Then
        '            If adlTerm.isConstraint Then
        '                DifferentialOntology.RemoveConstraintDefinition(adlTerm.EIF_Term)
        '            Else
        '                DifferentialOntology.RemoveTermDefinition(adlTerm.EIF_Term)
        '            End If
        '        Else
        '            Debug.Assert(False, "Term code is not available: " & adlTerm.Code)
        '        End If
        '    Catch e As Exception
        '        Debug.Assert(False, e.Message)
        '    End Try
        'End Sub

        Public Overrides Function HasTermCode(ByVal termCode As String) As Boolean
            Dim result As Boolean = False

            If RmTerm.IsValidTermCode(termCode) And Not DifferentialArchetype Is Nothing Then
                result = DifferentialOntology.HasTermCode(Eiffel.String(termCode)) Or DifferentialOntology.HasConstraintCode(Eiffel.String(termCode))
            End If

            Return result
        End Function

        Public Overrides Sub AddConstraint(ByVal term As RmTerm)
            If Not DifferentialArchetype Is Nothing Then
                If term.IsConstraint Then
                    Dim adlTerm As New ADL_Term(term)

                    Try
                        If Not DifferentialOntology.HasConstraintCode(adlTerm.EIF_Term.Code) Then
                            DifferentialOntology.AddConstraintDefinition(archetypeParser.AppRoot.CurrentLanguage, adlTerm.EIF_Term)
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

        Public Overrides Sub ReplaceConstraint(ByVal term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
            If Not DifferentialArchetype Is Nothing Then
                If term.IsConstraint Then
                    Dim adlTerm As New ADL_Term(term)

                    Try
                        If DifferentialOntology.HasConstraintCode(adlTerm.EIF_Term.Code) Then
                            DifferentialOntology.ReplaceConstraintDefinition(archetypeParser.AppRoot.CurrentLanguage, adlTerm.EIF_Term, ReplaceTranslations)
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

        Public Sub PopulateLanguages(ByRef TheOntologyManager As OntologyManager)
            If Not DifferentialArchetype Is Nothing And Not DifferentialOntology.LanguagesAvailable.Empty Then
                Dim i As Integer

                'A new ontology always adds the current language - but this may not be available in the archetype, so clear ..
                TheOntologyManager.LanguagesTable.Clear()

                For i = DifferentialOntology.LanguagesAvailable.Lower To DifferentialOntology.LanguagesAvailable.Upper
                    TheOntologyManager.AddLanguage(CType(DifferentialOntology.LanguagesAvailable.ITh(i), EiffelKernel.String_8).ToCil, "")
                Next
            End If
        End Sub

        Private Sub PopulateTerminologies(ByRef TheOntologyManager As OntologyManager)
            ' populate the terminology table in TermLookUp
            If Not DifferentialArchetype Is Nothing And Not DifferentialOntology.TerminologiesAvailable.Empty Then
                Dim i As Integer
                Dim s, t As String

                For i = DifferentialOntology.TerminologiesAvailable.Lower To DifferentialOntology.TerminologiesAvailable.Upper
                    s = CType(DifferentialOntology.TerminologiesAvailable.ITh(i), EiffelKernel.String_8).ToCil
                    t = TheOntologyManager.GetTerminologyDescription(s)
                    TheOntologyManager.AddTerminology(s, t)
                Next
            End If
        End Sub

        Private Sub PopulateTermDefinitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
            ' populate the TermDefinitions table in TermLookUp

            If Not DifferentialArchetype Is Nothing And Not DifferentialOntology.TermDefinitions.Empty Then
                Dim d_row, selected_row As DataRow
                Dim s As EiffelKernel.String_8
                Dim term As ADL_Term
                Dim linklist As EiffelList.LinkedListReference = DifferentialOntology.TermCodes
                linklist.Start()

                Do While Not linklist.Off
                    s = CType(linklist.Active.Item, EiffelKernel.String_8)

                    If LanguageCode = "" Then
                        ' set the term for all languages
                        For Each selected_row In TheOntologyManager.LanguagesTable.Rows
                            ' take each term from the ADL ontology
                            Dim archetypeTerm As AdlParser.ArchetypeTerm = DifferentialOntology.TermDefinition(Eiffel.String(selected_row(0)), s)

                            If archetypeTerm Is Nothing Then
                                Debug.Assert(False, "Term not in this language")
                                term = New ADL_Term(s.ToCil)
                                term.Text = "#Error#"
                                term.Description = "#Error#"
                                term.Comment = ""
                            Else
                                term = New ADL_Term(archetypeTerm)
                            End If

                            ' and if it is not an internal ID for machine processing
                            d_row = TheOntologyManager.TermDefinitionTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = term.Code
                            d_row(2) = term.Text
                            d_row(3) = term.Description
                            d_row(4) = term.Comment
                            d_row(5) = term
                            ' add it to the GUI ontology
                            TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                        Next
                    Else
                        ' just do it for the new language
                        term = New ADL_Term(DifferentialOntology.TermDefinition(Eiffel.String(LanguageCode), s))
                        ' and if it is not an internal ID for machine processing
                        d_row = TheOntologyManager.TermDefinitionTable.NewRow
                        d_row(0) = LanguageCode
                        d_row(1) = term.Code
                        d_row(2) = term.Text
                        d_row(3) = term.Description
                        d_row(4) = term.Comment
                        ' add it to the GUI ontology
                        TheOntologyManager.TermDefinitionTable.Rows.Add(d_row)
                    End If

                    linklist.Forth()
                Loop
            End If
        End Sub

        Private Sub PopulateTermBindings(ByRef TheOntologyManager As OntologyManager)
            ' populate the TermBindings table in TermLookUp

            If Not DifferentialArchetype Is Nothing And Not DifferentialOntology.TermBindings.Empty Then
                Dim d_row, selected_row As DataRow
                Dim terminology, code As EiffelKernel.String_8
                Dim cp As AdlParser.CodePhrase
                Dim bindings As EiffelTable.HashTableReferenceReference

                For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                    terminology = Eiffel.String(selected_row(0))

                    If DifferentialOntology.HasTermBindings(terminology) Then
                        bindings = DifferentialOntology.TermBindingsForTerminology(terminology)
                        bindings.Start()

                        Do While Not bindings.Off
                            cp = bindings.ItemForIteration
                            code = bindings.KeyForIteration
                            d_row = TheOntologyManager.TermBindingsTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = code.ToCil
                            d_row(2) = cp.CodeString

                            If Not cp.TerminologyId.VersionId Is Nothing Then
                                d_row(3) = cp.TerminologyId.VersionId.ToCil    ' release
                            End If

                            TheOntologyManager.TermBindingsTable.Rows.Add(d_row)
                            bindings.Forth()
                        Loop
                    End If
                Next
            End If
        End Sub

        Private Sub PopulateConstraintDefinitions(ByRef TheOntologyManager As OntologyManager, Optional ByVal LanguageCode As String = "")
            ' populate the ConstraintDefinitions table in TermLookUp

            If Not DifferentialArchetype Is Nothing And Not DifferentialOntology.ConstraintDefinitions.Empty Then
                Dim d_row, selected_row As DataRow
                Dim s As EiffelKernel.String_8
                Dim term As ADL_Term
                Dim linklist As EiffelList.LinkedListReference = DifferentialOntology.ConstraintCodes
                linklist.Start()

                Do While Not linklist.Off
                    s = CType(linklist.Active.Item, EiffelKernel.String_8)

                    If LanguageCode = "" Then
                        ' set the constraints for all languages
                        For Each selected_row In TheOntologyManager.LanguagesTable.Rows
                            ' take each constraint from the ADL ontology
                            term = New ADL_Term(DifferentialOntology.ConstraintDefinition(Eiffel.String(selected_row(0)), s))
                            d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                            d_row(0) = selected_row(0)
                            d_row(1) = term.Code
                            d_row(2) = term.Text
                            d_row(3) = term.Description
                            'Add it to the GUI
                            TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                        Next
                    Else
                        'Do it for the new language
                        term = New ADL_Term(DifferentialOntology.ConstraintDefinition(Eiffel.String(LanguageCode), s))
                        d_row = TheOntologyManager.ConstraintDefinitionTable.NewRow
                        d_row(0) = LanguageCode
                        d_row(1) = term.Code
                        d_row(2) = term.Text
                        d_row(3) = term.Description
                        'Add it for the ontology
                        TheOntologyManager.ConstraintDefinitionTable.Rows.Add(d_row)
                    End If

                    linklist.Forth()
                Loop
            End If
        End Sub

        Private Sub PopulateConstraintBindings(ByRef TheOntologyManager As OntologyManager)
            ' populate the ConstraintBindings table in TermLookUp

            If Not DifferentialArchetype Is Nothing And Not DifferentialOntology.ConstraintBindings.Empty Then
                Dim d_row, selected_row As DataRow
                Dim s As EiffelKernel.String_8
                Dim a_query As EiffelKernel.String_8
                Dim linklist As EiffelList.LinkedListReference = DifferentialOntology.ConstraintCodes
                linklist.Start()

                Do While Not linklist.Off
                    s = CType(linklist.Active.Item, EiffelKernel.String_8)

                    For Each selected_row In TheOntologyManager.TerminologiesTable.Rows
                        Try
                            If DifferentialOntology.HasConstraintBindings(Eiffel.String(selected_row(0))) Then
                                If DifferentialOntology.HasConstraintBinding(Eiffel.String(selected_row(0)), s) Then
                                    a_query = DifferentialOntology.ConstraintBinding(Eiffel.String(selected_row(0)), s).AsString
                                    d_row = TheOntologyManager.ConstraintBindingsTable.NewRow
                                    d_row(0) = selected_row(0)
                                    d_row(1) = s.ToCil
                                    d_row(2) = a_query.ToCil
                                    TheOntologyManager.ConstraintBindingsTable.Rows.Add(d_row)
                                End If
                            End If
                        Catch ex As Exception
                            'FIXME - to catch if couldn't add d_row for debug - probably remove try in longterm
                            Debug.Assert(False, "Error adding constraint binding")
                        End Try
                    Next

                    linklist.Forth()
                Loop
            End If
        End Sub

        Public Overrides Sub PopulateTermsInLanguage(ByRef TheOntologyManager As OntologyManager, ByVal LanguageCode As String)
            PopulateTermDefinitions(TheOntologyManager, LanguageCode)
            PopulateConstraintDefinitions(TheOntologyManager, LanguageCode)
        End Sub

        Public Overrides Sub PopulateAllTerms(ByRef TheOntologyManager As OntologyManager)
            If Not DifferentialArchetype Is Nothing Then
                PopulateLanguages(TheOntologyManager)
                PopulateTerminologies(TheOntologyManager)
                PopulateTermDefinitions(TheOntologyManager)
                PopulateTermBindings(TheOntologyManager)
                PopulateConstraintDefinitions(TheOntologyManager)
                PopulateConstraintBindings(TheOntologyManager)
            End If
        End Sub

        Sub New(ByRef parser As AdlParser.ArchetypeParser)
            archetypeParser = parser
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
