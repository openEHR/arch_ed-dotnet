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

Public Class OntologyManager
    Private mLanguageDS As DataSet
    Private mLanguagesTable As DataTable
    Private mFileManager As FileManagerLocal

    Private WithEvents mTermDefinitionsTable As DataTable
    Private WithEvents mConstraintDefinitionsTable As DataTable
    Private WithEvents mConstraintBindingsTable As DataTable
    Private WithEvents mTermBindingsTable As DataTable
    Private WithEvents mTermBindingCriteriaTable As DataTable

    Private mTerminologiesTable As DataTable
    Private mSubjectOfDataTable As DataTable
    Private mLanguageText As String
    Private mLanguageCode As String
    Private mPrimaryLanguageText As String
    Private mLastLanguageText As String
    Private mLastTerminologyText As String
    Private mOntology As Ontology
    Private mLastTerm As RmTerm
    Private mDoUpdateOntology As Boolean = False
    Private mReplaceTranslations As Integer = 0  ' 0 = ?, -1 = yes, 1 = no

    Public Property PrimaryLanguageCode() As String
        Get
            Return mOntology.PrimaryLanguageCode
        End Get
        Set(ByVal Value As String)
            mOntology.SetPrimaryLanguage(Value)
            mPrimaryLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", Value)
        End Set
    End Property

    Public ReadOnly Property PrimaryLanguageText() As String
        Get
            If mPrimaryLanguageText Is Nothing Then
                mPrimaryLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", mOntology.PrimaryLanguageCode)
            End If
            Return mPrimaryLanguageText
        End Get
    End Property

    Public Property LanguageCode() As String
        Get
            Return mLanguageCode
        End Get
        Set(ByVal Value As String)
            mLanguageCode = Value
            mOntology.SetLanguage(Value)
            mLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", Value)
        End Set
    End Property

    Public ReadOnly Property LanguageText() As String
        Get
            Return mLanguageText
        End Get
    End Property

    Public ReadOnly Property NumberOfSpecialisations() As Integer
        Get
            Return mOntology.NumberOfSpecialisations
        End Get
    End Property

    Public ReadOnly Property LastAddedLanguageText() As String
        Get
            Return mLastLanguageText
        End Get
    End Property

    Public ReadOnly Property LanguagesTable() As DataTable
        Get
            Return mLanguagesTable
        End Get
    End Property

    Public ReadOnly Property TermDefinitionTable() As DataTable
        Get
            Return mTermDefinitionsTable
        End Get
    End Property

    Public ReadOnly Property HasTermBinding(ByVal a_terminology As String, ByVal a_code As String, ByVal a_path As String) As Boolean
        Get
            Dim key(2) As Object
            key(0) = a_terminology
            key(1) = a_path
            key(2) = a_code
            Return Not TermBindingsTable.Rows.Find(key) Is Nothing
        End Get
    End Property

    Public ReadOnly Property ConstraintDefinitionTable() As DataTable
        Get
            Return mConstraintDefinitionsTable
        End Get
    End Property

    Public ReadOnly Property TerminologiesTable() As DataTable
        Get
            Return mTerminologiesTable
        End Get
    End Property

    Public ReadOnly Property SubjectOfDataTable() As DataTable
        Get
            Return mSubjectOfDataTable
        End Get
    End Property

    Public ReadOnly Property ConstraintBindingsTable() As DataTable
        Get
            Return mConstraintBindingsTable
        End Get
    End Property

    Public ReadOnly Property TermBindingsTable() As DataTable
        Get
            Return mTermBindingsTable
        End Get
    End Property

    Public ReadOnly Property TermBindingCriteriaTable() As DataTable
        Get
            Return mTermBindingCriteriaTable
        End Get
    End Property

    Public Property DoUpdateOntology() As Boolean
        Get
            Return mDoUpdateOntology
        End Get
        Set(ByVal Value As Boolean)
            mDoUpdateOntology = Value
        End Set
    End Property

    Public Property Ontology() As Ontology
        Get
            Return mOntology
        End Get
        Set(ByVal Value As Ontology)
            mOntology = Value
            Me.InitialiseOntologyManager(OceanArchetypeEditor.DefaultLanguageCode)
        End Set
    End Property

    Function HasTerminology(ByVal terminologyId As String) As Boolean
        Return Not mTerminologiesTable.Rows.Find(terminologyId) Is Nothing
    End Function

    Sub ReplaceOntology(ByVal anOntology As Ontology)
        mOntology = anOntology
    End Sub

    Private Sub InitialiseOntologyManager(ByVal LanguageCode As String)
        Dim new_row As DataRow
        Dim LanguageText As String

        Try
            mLanguageDS.Clear()
        Catch
            If mLanguageDS.HasChanges Then
                ' must accept these before calling clear
                mLanguageDS.AcceptChanges()
            End If

            mLanguageDS.Clear()
        End Try

        Debug.Assert(TermDefinitionTable.Rows.Count = 0)

        LanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", LanguageCode)
        mPrimaryLanguageText = Nothing
        mLanguageCode = LanguageCode
        mLanguageText = LanguageText
        mLastLanguageText = LanguageText
        new_row = mLanguagesTable.NewRow
        new_row(0) = mLanguageCode
        new_row(1) = mLanguageText
        mLanguagesTable.Rows.Add(new_row)
        mLastTerm = Nothing
        mReplaceTranslations = 0
    End Sub

    Public Sub Reset(Optional ByVal LanguageCode As String = "") ' resets for beginning a new archetype
        mReplaceTranslations = 0
        mOntology.Reset()

        If LanguageCode = "" Then
            LanguageCode = OceanArchetypeEditor.DefaultLanguageCode
        End If

        mOntology.SetPrimaryLanguage(LanguageCode)
        InitialiseOntologyManager(LanguageCode)
    End Sub

    Public Sub PopulateAllTerms()
        mOntology.PopulateAllTerms(Me)
    End Sub

    Public Function HasLanguage(ByVal code As String) As Boolean
        Return Not mLanguagesTable.Rows.Find(code) Is Nothing
    End Function

    Public Function GetTerm(ByVal code As String) As RmTerm
        Dim result As RmTerm

        If code Is Nothing Then
            result = New RmTerm("")
        ElseIf Not mLastTerm Is Nothing AndAlso mLastTerm.Code = code AndAlso mLastTerm.Language = mLanguageCode Then
            result = mLastTerm
        Else
            Dim d_row As DataRow

            Dim Keys(1) As Object
            Keys(0) = mLanguageCode
            Keys(1) = code
            result = New RmTerm(code)

            If result.IsConstraint Then
                d_row = mConstraintDefinitionsTable.Rows.Find(Keys)

                If Not d_row Is Nothing Then
                    result.Language = mLanguageCode
                    result.Text = CStr(d_row(2))
                    result.Description = CStr(d_row(3))
                End If
            Else
                d_row = mTermDefinitionsTable.Rows.Find(Keys)

                If Not d_row Is Nothing AndAlso TypeOf d_row(5) Is RmTerm Then
                    result = CType(d_row(5), RmTerm)
                End If
            End If

            mLastTerm = result  ' remember last one for efficiency
        End If

        Return result
    End Function

    Public Function SpecialiseTerm(ByVal text As String, ByVal description As String, ByVal id As String) As RmTerm
        Dim result As RmTerm = mOntology.SpecialiseTerm(text, description, id)
        UpdateTerm(result)
        Return result
    End Function

    Public Function SpecialiseTerm(ByVal term As RmTerm) As RmTerm
        Dim result As RmTerm = mOntology.SpecialiseTerm(term.Text & "**", term.Description, term.Code)
        UpdateTerm(result)
        Return result
    End Function

    Public Function SpecialiseTerm(ByVal a_term_code As String) As RmTerm
        Dim result As RmTerm = Nothing
        Dim a_term As RmTerm

        a_term = mOntology.TermForCode(a_term_code, mLanguageCode)

        If Not a_term Is Nothing Then
            result = mOntology.SpecialiseTerm(a_term.Text & "**", a_term.Description, a_term.Code)
            UpdateTerm(result)
        End If

        Return result
    End Function

    Public Function NextTermId() As String
        Return mOntology.NextTermId
    End Function

    Public Function AddTerm(ByVal Text As String, Optional ByVal Description As String = "*") As RmTerm
        mLastTerm = New RmTerm(mOntology.NextTermId)
        mLastTerm.Text = Text
        mLastTerm.Description = Description
        mOntology.AddTerm(mLastTerm)
        UpdateTerm(mLastTerm)
        Return mLastTerm
    End Function

    Public Sub UpdateTerm(ByVal term As RmTerm)
        ' add it to the GUI if it is not internal
        Debug.Assert(term.Code <> "")
        Debug.Assert(term.Text <> "")

        Dim d_row, l_row As DataRow

        If term.Code = "" Then
            Debug.Assert(False)
            Return
        ElseIf term.Text = "" Then
            Debug.Assert(False)
            term.Text = "unknown"
            term.Description = "unknown"
        ElseIf term.Description = "" Then
            term.Description = "*"
        End If

        If term.isConstraint Then
            For Each l_row In mLanguagesTable.Rows
                d_row = mConstraintDefinitionsTable.NewRow
                d_row(0) = l_row(0)
                d_row(1) = term.Code
                d_row(2) = term.Text
                d_row(3) = term.Description

                Try
                    mConstraintDefinitionsTable.Rows.Add(d_row)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        Else
            For Each l_row In mLanguagesTable.Rows
                d_row = mTermDefinitionsTable.NewRow
                d_row(0) = l_row(0)
                d_row(1) = term.Code
                d_row(2) = term.Text
                d_row(3) = term.Description
                d_row(5) = term

                Try
                    mTermDefinitionsTable.Rows.Add(d_row)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub

    Private Sub UpdateLanguage(ByVal LanguageCode As String)
        mOntology.PopulateTermsInLanguage(Me, LanguageCode)
    End Sub

    Public Function AddConstraint(ByVal Text As String, Optional ByVal Description As String = "*") As RmTerm
        mLastTerm = New RmTerm(mOntology.NextConstraintID)
        mLastTerm.Text = Text
        mLastTerm.Description = Description
        mOntology.AddConstraint(mLastTerm)
        Dim d_row As DataRow = mConstraintDefinitionsTable.NewRow
        d_row(0) = mLanguageCode
        d_row(1) = mLastTerm.Code
        d_row(2) = Text
        d_row(3) = Description
        mConstraintDefinitionsTable.Rows.Add(d_row)
        Return mLastTerm
    End Function

    Private Function ReplaceTranslations() As Boolean
        If mLanguageCode = PrimaryLanguageCode Then
            If mOntology.IsMultiLanguage Then
                If mReplaceTranslations = 0 Then
                    If MessageBox.Show(AE_Constants.Instance.ReplaceTranslations, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        mReplaceTranslations = -1 'true
                        Return True
                    Else
                        mReplaceTranslations = 1 ' false
                    End If
                Else
                    If mReplaceTranslations = 1 Then
                        Return False
                    Else
                        ' -1
                        Return True
                    End If
                End If
            End If
        End If

        Return False
    End Function

    Public Sub SetRmTermText(ByVal term As RmTerm)
        If term.IsConstraint Then
            Update(term, mConstraintDefinitionsTable, ReplaceTranslations())
        Else
            Update(term, mTermDefinitionsTable, ReplaceTranslations())
        End If

        mLastTerm = term
    End Sub

    Public Sub SetText(ByVal Value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.Text = Value
        SetRmTermText(mLastTerm)
    End Sub

    Public Function GetText(ByVal code As String) As String
        Dim result As String = ""
        Dim term As RmTerm = GetTerm(code)

        If Not term Is Nothing Then
            result = term.Text
        End If

        Return result
    End Function

    Public Sub SetDescription(ByVal Value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.Description = Value
        SetRmTermText(mLastTerm)
    End Sub

    Public Function GetDescription(ByVal code As String) As String
        Dim result As String = ""
        Dim term As RmTerm = GetTerm(code)

        If Not term Is Nothing Then
            result = term.Description
        End If

        Return result
    End Function

    Public Sub SetComment(ByVal Value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.Comment = Value
        SetRmTermText(mLastTerm)
    End Sub

    Public Sub SetOtherAnnotation(ByVal Key As String, ByVal Value As String, ByVal code As String)
        mLastTerm = GetTerm(code)

        If mLastTerm.OtherAnnotations.ContainsKey(Key) Then
            mLastTerm.OtherAnnotations.Item(Key) = Value
        Else
            mLastTerm.OtherAnnotations.Add(Key, Value)
        End If

        SetRmTermText(mLastTerm)
    End Sub

    Public Sub DeleteOtherAnnotation(ByVal Key As String, ByVal code As String)
        mLastTerm = GetTerm(code)

        If mLastTerm.OtherAnnotations.ContainsKey(Key) Then
            mLastTerm.OtherAnnotations.Remove(Key)
        End If

        SetRmTermText(mLastTerm)
    End Sub

    Public Sub RenameAnnotationKey(ByVal oldKey As String, ByVal newKey As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        Debug.Assert(mLastTerm.OtherAnnotations.ContainsKey(oldKey), "Must have key")
        Dim value As String

        If mLastTerm.OtherAnnotations.ContainsKey(oldKey) Then
            value = CStr(mLastTerm.OtherAnnotations.Item(oldKey))
            mLastTerm.OtherAnnotations.Remove(oldKey)
            mLastTerm.OtherAnnotations.Add(newKey, value)
            SetRmTermText(mLastTerm)
        End If
    End Sub

    Private Sub Update(ByVal aTerm As RmTerm, ByVal aTable As DataTable, ByVal ReplaceTranslations As Boolean)
        Dim d_row As DataRow

        If ReplaceTranslations Then
            Dim priorSetting As Boolean = mDoUpdateOntology
            Dim selected_rows As DataRow()
            selected_rows = aTable.Select("Code ='" & aTerm.Code & "'")

            For Each d_row In selected_rows
                If CStr(d_row(0)) = mLanguageCode Then
                    d_row.BeginEdit()

                    If CStr(d_row(2)) <> aTerm.Text Then
                        d_row(2) = aTerm.Text
                    End If

                    If CStr(d_row(3)) <> aTerm.Description Then
                        d_row(3) = aTerm.Description
                    End If

                    If Not aTerm.IsConstraint Then
                        If Not (IsDBNull(d_row(4)) And String.IsNullOrEmpty(aTerm.Comment)) Then
                            If CStr(d_row(4)) <> aTerm.Comment Then
                                d_row(4) = aTerm.Comment
                            End If
                        End If

                        d_row(5) = aTerm
                    End If

                    d_row.EndEdit()
                Else
                    mDoUpdateOntology = False 'as ontology changes are handled there
                    d_row.BeginEdit()
                    d_row(2) = "*" & aTerm.Text & "(" & mLanguageCode & ")"
                    d_row(3) = "*" & aTerm.Description & "(" & mLanguageCode & ")"

                    If Not aTerm.IsConstraint Then
                        If Not (IsDBNull(d_row(4)) And String.IsNullOrEmpty(aTerm.Comment)) Then
                            d_row(4) = "*" & aTerm.Comment & "(" & mLanguageCode & ")"
                        End If

                        d_row(5) = aTerm
                    End If

                    d_row.EndEdit()
                    mDoUpdateOntology = priorSetting
                End If
            Next
        Else
            'Need to update ontology here
            Dim keys(1) As Object
            keys(0) = mLanguageCode
            keys(1) = aTerm.Code
            d_row = aTable.Rows.Find(keys)

            If Not d_row Is Nothing Then
                d_row.BeginEdit()

                If IsDBNull(d_row(2)) OrElse CStr(d_row(2)) <> aTerm.Text Then
                    d_row(2) = aTerm.Text
                End If

                If IsDBNull(d_row(3)) OrElse CStr(d_row(3)) <> aTerm.Description Then
                    d_row(3) = aTerm.Description
                End If

                If IsDBNull(d_row(4)) OrElse CStr(d_row(4)) <> aTerm.Comment Then
                    d_row(4) = aTerm.Comment
                End If

                d_row(5) = aTerm
                d_row.EndEdit()
            End If
        End If

        mFileManager.FileEdited = True
    End Sub

    Public Sub AddLanguage(ByVal a_LanguageCode As String, Optional ByVal LanguageText As String = "")
        Dim key As Object

        If LanguageText = "" Then
            ' get the full name of the language from openEHR terminology
            mLastLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", a_LanguageCode)
        Else
            mLastLanguageText = LanguageText
        End If

        If mLastLanguageText = "" Then
            Debug.Assert(False, "Language code is empty")
            Return
        End If

        key = a_LanguageCode
        If LanguagesTable.Rows.Find(key) Is Nothing Then
            Dim new_row As DataRow
            new_row = LanguagesTable.NewRow
            new_row(0) = a_LanguageCode
            new_row("Language") = mLastLanguageText
            LanguagesTable.Rows.Add(new_row)

            If Not mOntology.LanguageAvailable(a_LanguageCode) Then
                mOntology.AddLanguage(a_LanguageCode)
                'update the new terms generated by the ontology
                UpdateLanguage(a_LanguageCode)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Public Sub AddTerminology(ByVal terminologyId As String, ByVal terminologyText As String)
        If terminologyText = "" Then
            ' get the full name of the language from openEHR terminology
            mLastTerminologyText = TerminologyServer.Instance.CodeSetItemDescription("Terminology", terminologyId)
        Else
            mLastTerminologyText = terminologyText
        End If

        If TerminologiesTable.Rows.Find(terminologyId) Is Nothing Then
            Dim row As DataRow = TerminologiesTable.NewRow
            row(0) = terminologyId
            row(1) = mLastTerminologyText
            TerminologiesTable.Rows.Add(row)
        End If

        ' ensure it is in the ontology as well
        If mDoUpdateOntology Then
            mOntology.AddTerminology(terminologyId)
        End If

        mFileManager.FileEdited = True
    End Sub

    Public Sub AddConstraintBinding(ByVal acCode As String, ByVal terminologyId As String, ByVal terminologyText As String, ByVal release As String, ByVal subset As String)
        If Not HasTerminology(terminologyId) Then
            AddTerminology(terminologyId, terminologyText)
        End If

        If RmTerm.IsValidTermCode(acCode) Then
            Dim uri As String = "terminology:" + terminologyId

            If release <> "" Then
                uri = uri + "/" + release
            End If

            If subset <> "" Then
                uri = uri + "?subset=" + subset.Replace(" ", "+")
            End If

            Dim row As DataRow = Filemanager.Master.OntologyManager.ConstraintBindingsTable.NewRow
            row(0) = terminologyId
            row(1) = acCode
            row(2) = uri
            row(3) = release

            Dim keys(1) As Object
            keys(0) = row(0)
            keys(1) = row(1)

            If Filemanager.Master.OntologyManager.ConstraintBindingsTable.Rows.Contains(keys) Then
                'change the constraint
                row = Filemanager.Master.OntologyManager.ConstraintBindingsTable.Rows.Find(keys)
                row.BeginEdit()
                row(2) = uri
                row.EndEdit()
            Else
                Filemanager.Master.OntologyManager.ConstraintBindingsTable.Rows.Add(row)
            End If

            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Public Function GetOpenEHRTerm(ByVal code As Integer, ByVal DefaultTerm As String, Optional ByVal Language As String = "?") As String
        Dim s As String = Nothing

        ' returns the string in the language
        If Language = "?" Then
            Language = OceanArchetypeEditor.SpecificLanguageCode
        End If

        Try
            s = TerminologyServer.Instance.RubricForCode(code, Language)
        Catch except As Exception
            MessageBox.Show("Error in terminology server: " & except.Message, "Ocean Archetype Parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If s Is Nothing Then
            Return DefaultTerm
        Else
            Return s
        End If
    End Function

    Public Function CodeForGroupID(ByVal GroupID As Integer, Optional ByVal language As String = "") As DataRow()
        Return TerminologyServer.Instance.CodesForGrouperID(GroupID, language)
    End Function

    Private Function MakeSubjectOfDataTable() As DataTable
        ' Create a new DataTable titled 'Languages'
        Dim SoCTable As DataTable = New DataTable("SubjectOfData")
        ' Add three column objects to the table.

        Dim LanguageColumn As DataColumn = New DataColumn
        LanguageColumn.DataType = System.Type.GetType("System.String")
        LanguageColumn.ColumnName = "Language"
        SoCTable.Columns.Add(LanguageColumn)
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.Int32")
        idColumn.ColumnName = "TermId"
        idColumn.AutoIncrement = False
        SoCTable.Columns.Add(idColumn)
        Dim PhraseColumn As DataColumn = New DataColumn
        PhraseColumn.DataType = System.Type.GetType("System.String")
        PhraseColumn.ColumnName = "Phrase"
        SoCTable.Columns.Add(PhraseColumn)
        ' Create an array for DataColumn objects.
        Dim keys(1) As DataColumn
        keys(0) = LanguageColumn
        keys(1) = idColumn
        SoCTable.PrimaryKey = keys
        ' Return the new DataTable.
        Return SoCTable
    End Function

    Public Sub SetBestLanguage()
        ' set the specific language if it is present e.g. en-US, en-AU
        Dim bestLanguage As String

        If HasLanguage(OceanArchetypeEditor.SpecificLanguageCode) Then
            bestLanguage = OceanArchetypeEditor.SpecificLanguageCode
        ElseIf HasLanguage(OceanArchetypeEditor.DefaultLanguageCode) Then
            bestLanguage = OceanArchetypeEditor.DefaultLanguageCode
        ElseIf HasLanguage(Filemanager.Master.OntologyManager.LanguageCode) Then
            bestLanguage = Filemanager.Master.OntologyManager.LanguageCode
        Else
            bestLanguage = PrimaryLanguageCode
        End If

        If mFileManager.OntologyManager.LanguageCode <> bestLanguage Then
            LanguageCode = bestLanguage
        End If
    End Sub

    Private Function MakeLanguagesTable() As DataTable
        ' Create a new DataTable titled 'Languages'
        Dim LangTable As DataTable = New DataTable("Languages")
        ' Add two column objects to the table.
        'First the language ID = two letter ISO code
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "id"
        LangTable.Columns.Add(idColumn)
        'Second the language text from windows/ISO list
        Dim LanguageColumn As DataColumn = New DataColumn
        LanguageColumn.DataType = System.Type.GetType("System.String")
        LanguageColumn.ColumnName = "Language"
        LangTable.Columns.Add(LanguageColumn)
        ' Create an array for DataColumn objects.
        Dim keys(0) As DataColumn
        keys(0) = idColumn
        LangTable.PrimaryKey = keys
        ' Return the new DataTable.
        Return LangTable
    End Function

    Private Function MakeTerminologiesTable() As DataTable
        ' Create a new DataTable titled 'Terminology'
        Dim TermTable As DataTable = New DataTable("Terminologies")
        ' Add two column objects to the table.
        Dim TermColumn As DataColumn = New DataColumn
        TermColumn.DataType = System.Type.GetType("System.String")
        TermColumn.ColumnName = "Terminology"
        TermTable.Columns.Add(TermColumn)
        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = System.Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        TermTable.Columns.Add(DescriptionColumn)
        ' Create an array for DataColumn objects.
        Dim keys(0) As DataColumn
        keys(0) = TermColumn
        TermTable.PrimaryKey = keys
        ' Return the new DataTable.
        TermTable.DefaultView.AllowNew = False
        Return TermTable
    End Function

    Private Function MakeConstraintBindingTable() As DataTable
        ' Note - there are no versions of the Terminology at present
        ' as if the knowledge model shifted fundamentally then I believe
        ' that the archetype would have to be released with a new version
        ' There may be a need to keep track of which release it is up to date with

        'Also - we may need to know for all terminology - when it was added - ie what revision

        Dim BindingsTable As DataTable

        BindingsTable = New DataTable("ConstraintBindings")
        ' Add six column objects to the table.
        'Short name or ID of terminology
        Dim TermColumn As DataColumn = New DataColumn
        TermColumn.DataType = System.Type.GetType("System.String")
        TermColumn.ColumnName = "Terminology"
        BindingsTable.Columns.Add(TermColumn)
        'AcCode acNNNN
        Dim IDColumn As DataColumn = New DataColumn
        IDColumn.DataType = System.Type.GetType("System.String")
        IDColumn.ColumnName = "ID"
        BindingsTable.Columns.Add(IDColumn)
        'URL to the path
        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = System.Type.GetType("System.String")
        CodeColumn.ColumnName = "CodePhrase"
        CodeColumn.DefaultValue = ""
        BindingsTable.Columns.Add(CodeColumn)
        'Release information if relevant
        Dim ReleaseColumn As DataColumn = New DataColumn
        ReleaseColumn.DataType = System.Type.GetType("System.String")
        ReleaseColumn.ColumnName = "Release"
        ReleaseColumn.DefaultValue = ""
        BindingsTable.Columns.Add(ReleaseColumn)
        ' Return the new DataTable.
        Dim keys(1) As DataColumn
        keys(0) = TermColumn
        keys(1) = IDColumn
        BindingsTable.PrimaryKey = keys

        Return BindingsTable
    End Function

    Private Function MakeTermBindingTable() As DataTable
        ' Note - there are no versions of the Terminology at present
        ' as if the knowledge model shifted fundamentally then I believe
        ' that the archetype would have to be released with a new version
        ' There may be a need to keep track of which release it is up to date with

        'Also - we may need to know for all terminology - when it was added - ie what revision

        Dim BindingsTable As DataTable
        BindingsTable = New DataTable("TermBindings")
        ' Add six column objects to the table.
        'CODE
        Dim TermColumn As DataColumn = New DataColumn  ' the code in the terminology
        TermColumn.DataType = System.Type.GetType("System.String")
        TermColumn.ColumnName = "Terminology"
        BindingsTable.Columns.Add(TermColumn)

        'PATH
        Dim PathColumn As DataColumn = New DataColumn
        PathColumn.DataType = System.Type.GetType("System.String")
        PathColumn.ColumnName = "Path"  ' the path - could be a node or the full path of a node +/- criteria
        BindingsTable.Columns.Add(PathColumn)
        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = System.Type.GetType("System.String")
        CodeColumn.ColumnName = "Code"
        CodeColumn.DefaultValue = ""
        BindingsTable.Columns.Add(CodeColumn)
        'RELEASE
        Dim ReleaseColumn As DataColumn = New DataColumn
        ReleaseColumn.DataType = System.Type.GetType("System.String")
        ReleaseColumn.ColumnName = "Release"
        ReleaseColumn.DefaultValue = ""
        BindingsTable.Columns.Add(ReleaseColumn)
        ' Return the new DataTable.
        Dim keys(2) As DataColumn
        keys(0) = TermColumn
        keys(1) = PathColumn
        keys(2) = CodeColumn
        BindingsTable.PrimaryKey = keys

        Return BindingsTable
    End Function

    Private Function MakeTermBindingCriteriaTable() As DataTable
        Dim BindingCriteriaTable As New DataTable("TermBindingCriteria")

        ' Add six column objects to the table.
        Dim terminologyColumn As DataColumn = New DataColumn
        terminologyColumn.DataType = GetType(String)
        terminologyColumn.ColumnName = "Terminology"
        BindingCriteriaTable.Columns.Add(terminologyColumn)

        Dim PathColumn As DataColumn = New DataColumn
        PathColumn.DataType = GetType(String)
        PathColumn.ColumnName = "Path"
        BindingCriteriaTable.Columns.Add(PathColumn)

        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = GetType(String)
        CodeColumn.ColumnName = "Code"
        BindingCriteriaTable.Columns.Add(CodeColumn)

        Dim ReleaseColumn As DataColumn = New DataColumn
        ReleaseColumn.DataType = GetType(String)
        ReleaseColumn.ColumnName = "Release"
        ReleaseColumn.DefaultValue = ""
        BindingCriteriaTable.Columns.Add(ReleaseColumn)

        Dim CriteriaColumn As New DataColumn
        CriteriaColumn.DataType = GetType(String)
        CriteriaColumn.ColumnName = "Criteria"
        CriteriaColumn.DefaultValue = ""
        BindingCriteriaTable.Columns.Add(CriteriaColumn)

        ' Return the new DataTable.
        Dim keys(3) As DataColumn
        keys(0) = terminologyColumn
        keys(1) = PathColumn
        keys(2) = CodeColumn
        keys(3) = CriteriaColumn
        BindingCriteriaTable.PrimaryKey = keys

        Return BindingCriteriaTable
    End Function

    Private Function MakeDefinitionsTable(ByVal nm As String) As DataTable
        ' Create a new DataTable titled 'TermDefinitions' or 'ConstraintDefinitions'
        Dim result As DataTable
        result = New DataTable(nm)

        ' Add Four column objects to the table.
        ' The id is the language of the term defintion
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "id"
        result.Columns.Add(idColumn)

        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = System.Type.GetType("System.String")
        CodeColumn.ColumnName = "Code"
        result.Columns.Add(CodeColumn)

        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        result.Columns.Add(TextColumn)

        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = System.Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        DescriptionColumn.DefaultValue = "*"
        result.Columns.Add(DescriptionColumn)

        Dim CommentColumn As DataColumn = New DataColumn
        CommentColumn.DataType = System.Type.GetType("System.String")
        CommentColumn.ColumnName = "Comment"
        CommentColumn.DefaultValue = ""
        result.Columns.Add(CommentColumn)

        Dim TermAsObjectColumn As DataColumn = New DataColumn
        TermAsObjectColumn.DataType = System.Type.GetType("System.Object")
        TermAsObjectColumn.ColumnName = "TermAsObject"
        TermAsObjectColumn.DefaultValue = ""
        result.Columns.Add(TermAsObjectColumn)

        ' Support temporary correct ordering of internal codelists to match order in Definition
        Dim listOrderColumn As DataColumn = New DataColumn
        listOrderColumn.DataType = System.Type.GetType("System.Int32")
        listOrderColumn.ColumnName = "ListOrder"
        result.Columns.Add(listOrderColumn)

        ' Return the new DataTable.
        Dim keys(1) As DataColumn
        keys(0) = idColumn
        keys(1) = CodeColumn
        result.PrimaryKey = keys

        Return result
    End Function

    Private Sub InitialiseTables()
        Dim new_relation As DataRelation
        ' make the tables
        mLanguagesTable = MakeLanguagesTable()
        mLanguageDS.Tables.Add(mLanguagesTable)

        mTermDefinitionsTable = MakeDefinitionsTable("TermDefinitions")
        mLanguageDS.Tables.Add(mTermDefinitionsTable)

        mConstraintDefinitionsTable = MakeDefinitionsTable("ConstraintDefinitions")
        mLanguageDS.Tables.Add(mConstraintDefinitionsTable)

        ' add relations
        new_relation = New DataRelation("LanguageTerms", mLanguagesTable.Columns(0), mTermDefinitionsTable.Columns(0))
        mLanguageDS.Relations.Add(new_relation)

        new_relation = New DataRelation("LanguageConstraints", mLanguagesTable.Columns(0), mConstraintDefinitionsTable.Columns(0))
        mLanguageDS.Relations.Add(new_relation)

        mTerminologiesTable = MakeTerminologiesTable()
        mLanguageDS.Tables.Add(mTerminologiesTable)

        ' TermBinding
        mTermBindingsTable = MakeTermBindingTable()
        mLanguageDS.Tables.Add(mTermBindingsTable)

        new_relation = New DataRelation("TerminologiesBindings", mTerminologiesTable.Columns(0), mTermBindingsTable.Columns(0))
        mLanguageDS.Relations.Add(new_relation)

        ' TermBindingCriteria
        mTermBindingCriteriaTable = MakeTermBindingCriteriaTable()
        mLanguageDS.Tables.Add(mTermBindingCriteriaTable)
        Dim termBindingColumns As DataColumn() = {mTermBindingsTable.Columns(0), mTermBindingsTable.Columns(1), mTermBindingsTable.Columns(2)}
        Dim termBindingCriteriaColumns As DataColumn() = {mTermBindingCriteriaTable.Columns(0), mTermBindingCriteriaTable.Columns(1), mTermBindingCriteriaTable.Columns(2)}
        new_relation = New DataRelation("TermBindingTermBindingCriteria", termBindingColumns, termBindingCriteriaColumns)

        mLanguageDS.Relations.Add(new_relation)

        mConstraintBindingsTable = MakeConstraintBindingTable()
        mLanguageDS.Tables.Add(mConstraintBindingsTable)

        new_relation = New DataRelation("TerminologiesConstraintBindings", mConstraintDefinitionsTable.Columns(1), mConstraintBindingsTable.Columns(1), False)
        mLanguageDS.Relations.Add(new_relation)

        mSubjectOfDataTable = MakeSubjectOfDataTable()
        mLanguageDS.Tables.Add(mSubjectOfDataTable)

        ' add relations
        new_relation = New DataRelation("LanguageSubjectOfData", mLanguagesTable.Columns(0), mSubjectOfDataTable.Columns(0))
        mLanguageDS.Relations.Add(new_relation)
    End Sub

    Public Function GetTerminologyDescription(ByVal TerminologyCode As String) As String
        Return TerminologyServer.Instance.CodeSetItemDescription("Terminologies", TerminologyCode)
    End Function

    Public Function GetTerminologyIdentifiers() As DataRow()
        Return TerminologyServer.Instance.CodeSetAsDataRow("Terminologies")
    End Function

    Public Function GetLanguageList() As DataRow()
        Return TerminologyServer.Instance.CodeSetAsDataRow("Languages")
    End Function

    Public Sub DefinitionsTable_RowChanged(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles mTermDefinitionsTable.RowChanged, mConstraintDefinitionsTable.RowChanged
        If mDoUpdateOntology Then
            If e.Action = DataRowAction.Change Then
                Dim aterm As New RmTerm(CStr(e.Row(1)))

                If Not aterm.IsConstraint Then
                    If Not IsDBNull(e.Row(5)) Then
                        aterm = CType(e.Row(5), RmTerm)
                    End If
                End If

                aterm.Language = CStr(e.Row(0))
                aterm.Text = CStr(e.Row(2))
                aterm.Description = CStr(e.Row(3))

                If aterm.IsConstraint Then
                    mOntology.ReplaceConstraint(aterm)
                Else
                    If Not IsDBNull(e.Row(4)) Then
                        aterm.Comment = CStr(e.Row(4))
                    End If

                    mOntology.ReplaceTerm(aterm, ReplaceTranslations())
                End If

                mFileManager.FileEdited = True
                'DO NOT DELETE TERMS - these may be used elsewhere so are removed at end of session
                'ElseIf e.Action = DataRowAction.Delete Then
                '    mOntology.DeleteTerm(aterm)
            End If
        End If
    End Sub

    Public Sub TermBindingsTable_RowChanged(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles mTermBindingsTable.RowChanged, mTermBindingsTable.RowDeleting
        If mDoUpdateOntology Then
            Try
                Dim sTerminology As String

                If Not e.Row(0) Is Nothing Then     'Terminology
                    sTerminology = TryCast(e.Row(0), String)
                Else
                    Return
                End If

                Dim sPath As String

                If Not e.Row(1) Is Nothing Then     'Path
                    sPath = TryCast(e.Row(1), String)
                Else
                    Return
                End If

                Dim sCode As String
                If Not e.Row(2) Is Nothing Then     'Code
                    sCode = TryCast(e.Row(2), String)
                Else
                    Return
                End If

                Dim sRelease As String = ""

                If Not e.Row(3) Is Nothing Then     'Release
                    sRelease = TryCast(e.Row(3), String)
                End If

                If e.Action = DataRowAction.Add Or e.Action = DataRowAction.Change Then
                    sCode = System.Text.RegularExpressions.Regex.Replace(sCode, "[\*\(\)\]\[\~\`\!\@\#\$\%\^\&\+\=\""\{\}\|\;\:\?/\<\>\s]", "")

                    If Not String.IsNullOrEmpty(sTerminology) And Not String.IsNullOrEmpty(sPath) And Not String.IsNullOrEmpty(sCode) Then
                        mOntology.AddorReplaceTermBinding(sTerminology, sPath, sCode, sRelease)
                    End If
                ElseIf e.Action = DataRowAction.Delete Then
                    mOntology.RemoveTermBinding(sTerminology, sPath)
                End If

                mFileManager.FileEdited = True
            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
            End Try
        End If
    End Sub

    Public Sub ConstraintBindingsTable_RowChanged(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles mConstraintBindingsTable.RowChanged, mConstraintBindingsTable.RowDeleting
        If mDoUpdateOntology Then
            Try
                Dim sTerminology As String

                If Not e.Row(0) Is Nothing Then     'Terminology
                    sTerminology = TryCast(e.Row(0), String)
                Else
                    Return
                End If

                Dim sCode As String = ""

                If Not e.Row(1) Is Nothing Then     'Code
                    sCode = TryCast(e.Row(1), String)
                Else
                    Return
                End If

                Dim sQuery As String

                If Not e.Row(2) Is Nothing Then     'Query
                    sQuery = TryCast(e.Row(2), String)
                Else
                    Return
                End If

                Dim sRelease As String = ""

                If Not e.Row(3) Is Nothing Then     'Release
                    sRelease = TryCast(e.Row(3), String)
                End If

                If e.Action = DataRowAction.Add Or e.Action = DataRowAction.Change Then
                    mOntology.AddorReplaceConstraintBinding(sTerminology, sCode, sQuery, sRelease)
                ElseIf e.Action = DataRowAction.Delete Then
                    mOntology.RemoveConstraintBinding(sTerminology, sCode)
                End If

                mFileManager.FileEdited = True
            Catch ex As Exception
                Debug.Assert(False, ex.ToString)
            End Try
        End If
    End Sub

    Sub New(ByVal a_file_manager As FileManagerLocal)
        mLanguageDS = New DataSet("LanguageDataSet")
        InitialiseTables()
        AE_Constants.Create(OceanArchetypeEditor.DefaultLanguageCode)
        OceanArchetypeEditor.Instance.Options.ValidateConfiguration()
        mFileManager = a_file_manager
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
'The Original Code is OntologyManager.vb.
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

