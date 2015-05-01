'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Option Strict On

Public Class OntologyManager
    Private mLanguageDS As DataSet
    Private mLanguagesTable As DataTable
    Private mFileManager As FileManagerLocal

    Private WithEvents mTermDefinitionTable As DataTable
    Private WithEvents mConstraintDefinitionTable As DataTable
    Private WithEvents mConstraintBindingsTable As DataTable
    Private WithEvents mTermBindingsTable As DataTable
    Private WithEvents mTermBindingCriteriaTable As DataTable

    Private mTerminologiesTable As DataTable
    Private mSubjectOfDataTable As DataTable
    Private mLanguageText As String
    Private mLanguageCode As String
    Private mPrimaryLanguageText As String
    Private mLastLanguageText As String
    Private mOntology As Ontology
    Private mLastTerm As RmTerm
    Private mDoUpdateOntology As Boolean = False
    Private mReplaceTranslations As Integer = 0  ' 0 = ?, -1 = yes, 1 = no

    Public Property PrimaryLanguageCode() As String
        Get
            Return mOntology.PrimaryLanguageCode
        End Get
        Set(ByVal value As String)
            mOntology.SetPrimaryLanguage(value)
            mPrimaryLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", value)

            If mPrimaryLanguageText Is Nothing Then
                mPrimaryLanguageText = value
            End If
        End Set
    End Property

    Public ReadOnly Property PrimaryLanguageText() As String
        Get
            If mPrimaryLanguageText Is Nothing Then
                mPrimaryLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", mOntology.PrimaryLanguageCode)

                If mPrimaryLanguageText Is Nothing Then
                    mPrimaryLanguageText = PrimaryLanguageCode
                End If
            End If

            Return mPrimaryLanguageText
        End Get
    End Property

    Public Property LanguageCode() As String
        Get
            Return mLanguageCode
        End Get
        Set(ByVal value As String)
            mLanguageCode = value
            mOntology.SetLanguage(value)
            mLanguageText = TerminologyServer.Instance.CodeSetItemDescription("Language", value)

            If mLanguageText Is Nothing Then
                mLanguageText = value
            End If
        End Set
    End Property

    Public ReadOnly Property LanguageText() As String
        Get
            Return mLanguageText
        End Get
    End Property

    Public ReadOnly Property NumberOfSpecialisations() As Integer
        Get
            Dim result As Integer = 0

            If Not mOntology Is Nothing Then
                result = mOntology.NumberOfSpecialisations
            End If

            Return result
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
            Return mTermDefinitionTable
        End Get
    End Property

    Public ReadOnly Property HasTermBinding(ByVal terminology As String, ByVal code As String, ByVal path As String) As Boolean
        Get
            Dim key(2) As Object
            key(0) = terminology
            key(1) = path
            key(2) = code
            Return Not TermBindingsTable.Rows.Find(key) Is Nothing
        End Get
    End Property

    Public ReadOnly Property ConstraintDefinitionTable() As DataTable
        Get
            Return mConstraintDefinitionTable
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
            InitialiseOntologyManager(Main.Instance.DefaultLanguageCode)
        End Set
    End Property

    Sub ReplaceOntology(ByVal anOntology As Ontology)
        mOntology = anOntology
    End Sub

    Private Sub InitialiseOntologyManager(ByVal languageCode As String)
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

        Dim languageText As String = TerminologyServer.Instance.CodeSetItemDescription("Language", languageCode)

        If languageText Is Nothing Then
            languageText = languageCode
        End If

        mPrimaryLanguageText = Nothing
        mLanguageCode = languageCode
        mLanguageText = languageText
        mLastLanguageText = languageText
        mLastTerm = Nothing
        mReplaceTranslations = 0

        Dim row As DataRow = mLanguagesTable.NewRow
        row(0) = languageCode
        row(1) = languageText
        mLanguagesTable.Rows.Add(row)
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
            Dim row As DataRow
            Dim keys As Object() = {mLanguageCode, code}
            result = New RmTerm(code)

            If result.IsConstraint Then
                row = mConstraintDefinitionTable.Rows.Find(keys)
            Else
                row = mTermDefinitionTable.Rows.Find(keys)
            End If

            If Not row Is Nothing Then
                If TypeOf row(5) Is RmTerm Then
                    result = CType(row(5), RmTerm)
                Else
                    result.Language = mLanguageCode
                    result.Text = TryCast(row(2), String)
                    result.Description = TryCast(row(3), String)
                    result.Comment = TryCast(row(4), String)
                End If
            End If

            mLastTerm = result  ' remember last one for efficiency
        End If

        Return result
    End Function

    Public Function SpecialiseTerm(ByVal text As String, ByVal description As String, ByVal id As String) As RmTerm
        Dim split As String() = id.Split("."c)
        Dim specialisationDepth As Integer = split.Length - 1

        If specialisationDepth = NumberOfSpecialisations Then
            id = split(0)

            For i As Integer = 1 To specialisationDepth - 1
                id = id + "." + split(i)
            Next
        End If

        Dim result As RmTerm = mOntology.SpecialiseTerm(text, description, id)
        UpdateTerm(result)
        Return result
    End Function

    Public Function SpecialiseNameConstraint(ByVal a_term_code As String) As RmTerm
        Dim result As RmTerm = Nothing
        Dim term As RmTerm = mOntology.TermForCode(a_term_code, mLanguageCode)

        If Not term Is Nothing Then
            result = SpecialiseTerm(term.Text & "**", term.Description, term.Code)
        End If

        Return result
    End Function

    Public Sub UpdateTerm(ByVal term As RmTerm)
        ' add it to the GUI if it is not internal
        Debug.Assert(term.Code <> "")

        If term.Text = "" Then
            term.Text = "unknown"
        End If

        If term.Description = "" Then
            term.Description = "*"
        End If

        Dim table As DataTable

        If term.IsConstraint Then
            table = mConstraintDefinitionTable
        Else
            table = mTermDefinitionTable
        End If

        For Each languageRow As DataRow In mLanguagesTable.Rows
            Dim newRow As DataRow = table.NewRow
            newRow(0) = languageRow(0)
            newRow(1) = term.Code
            newRow(2) = term.Text
            newRow(3) = term.Description
            newRow(4) = term.Comment
            newRow(5) = term

            Try
                table.Rows.Add(newRow)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Sub

    Public Function AddTerm(ByVal text As String, Optional ByVal description As String = "*") As RmTerm
        mLastTerm = New RmTerm(mOntology.NextTermId)
        mLastTerm.Text = text
        mLastTerm.Description = description
        mOntology.AddTerm(mLastTerm)
        UpdateTerm(mLastTerm)
        Return mLastTerm
    End Function

    Public Function AddConstraint(ByVal text As String, Optional ByVal description As String = "*") As RmTerm
        mLastTerm = New RmTerm(mOntology.NextConstraintID)
        mLastTerm.Text = text
        mLastTerm.Description = description
        mOntology.AddConstraint(mLastTerm)
        UpdateTerm(mLastTerm)
        Return mLastTerm
    End Function

    Private Function ReplaceTranslations(ByVal term As RmTerm) As Boolean
        Dim result As Boolean = False

        If mLanguageCode = PrimaryLanguageCode And mOntology.IsMultiLanguage Then
            If mReplaceTranslations = 0 And Not mFileManager.FileLoading Then
                mReplaceTranslations = 1
                Dim message As String = AE_Constants.Instance.ReplaceTranslations + Environment.NewLine + term.Code + " = """ + term.Text + """"

                If MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    mReplaceTranslations = -1
                End If
            End If

            result = mReplaceTranslations = -1
        End If

        Return result
    End Function

    Public Sub SetRmTermText(ByVal term As RmTerm)
        Dim table As DataTable

        If term.IsConstraint Then
            table = mConstraintDefinitionTable
        Else
            table = mTermDefinitionTable
        End If

        Dim row As DataRow

        If ReplaceTranslations(term) Then
            Dim priorSetting As Boolean = mDoUpdateOntology

            For Each row In table.Select("Code ='" & term.Code & "'")
                If TryCast(row(0), String) = mLanguageCode Then
                    row.BeginEdit()

                    If TryCast(row(2), String) <> term.Text Then
                        row(2) = term.Text
                    End If

                    If TryCast(row(3), String) <> term.Description Then
                        row(3) = term.Description
                    End If

                    If TryCast(row(4), String) <> term.Comment Then
                        row(4) = term.Comment
                    End If

                    row(5) = term
                    row.EndEdit()
                Else
                    mDoUpdateOntology = False 'as ontology changes are handled there
                    row.BeginEdit()
                    row(2) = "*" & term.Text & "(" & mLanguageCode & ")"
                    row(3) = "*" & term.Description & "(" & mLanguageCode & ")"

                    If Not IsDBNull(row(4)) Or term.Comment <> "" Then
                        row(4) = "*" & term.Comment & "(" & mLanguageCode & ")"
                    End If

                    row(5) = term
                    row.EndEdit()
                    mDoUpdateOntology = priorSetting
                End If
            Next
        Else
            'Need to update ontology here
            Dim keys As Object() = {mLanguageCode, term.Code}
            row = table.Rows.Find(keys)

            If Not row Is Nothing Then
                row.BeginEdit()

                If TryCast(row(2), String) <> term.Text Then
                    row(2) = term.Text
                End If

                If TryCast(row(3), String) <> term.Description Then
                    row(3) = term.Description
                End If

                If TryCast(row(4), String) <> term.Comment Then
                    row(4) = term.Comment
                End If

                row(5) = term
                row.EndEdit()
            End If
        End If

        mLastTerm = term
        mFileManager.FileEdited = True
    End Sub

    Public Sub SetText(ByVal value As String, ByVal code As String)
        mLastTerm = GetTerm(code)

        If mLastTerm.Text <> value Then
            mLastTerm.Text = value
            SetRmTermText(mLastTerm)
        End If
    End Sub

    Public Function GetText(ByVal code As String) As String
        Dim result As String = ""
        Dim term As RmTerm = GetTerm(code)

        If Not term Is Nothing Then
            result = term.Text
        End If

        Return result
    End Function

    Public Sub SetDescription(ByVal value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.Description = value
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

    Public Sub SetComment(ByVal value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.Comment = value
        SetRmTermText(mLastTerm)
    End Sub

    Public Sub SetOtherAnnotation(ByVal key As String, ByVal value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.OtherAnnotations.Item(key) = value
        SetRmTermText(mLastTerm)
    End Sub

    Public Sub DeleteOtherAnnotation(ByVal key As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.OtherAnnotations.Remove(key)
        SetRmTermText(mLastTerm)
    End Sub

    Public Sub AddLanguage(ByVal languageCode As String, Optional ByVal languageText As String = "")
        If languageText = "" Then
            ' get the full name of the language from openEHR terminology
            languageText = TerminologyServer.Instance.CodeSetItemDescription("Language", languageCode)
        End If

        If languageText Is Nothing Then
            languageText = languageCode
        End If

        mLastLanguageText = languageText

        If Not HasLanguage(languageCode) Then
            Dim row As DataRow = LanguagesTable.NewRow
            row(0) = languageCode
            row("Language") = languageText
            LanguagesTable.Rows.Add(row)

            If Not mOntology.LanguageAvailable(languageCode) Then
                mOntology.AddLanguage(languageCode)
                'update the new terms generated by the ontology
                mOntology.PopulateTermsInLanguage(Me, languageCode)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Public Sub AddTerminology(ByVal terminologyId As String)
        If TerminologiesTable.Rows.Find(terminologyId) Is Nothing Then
            Dim terminologyText As String = TerminologyServer.Instance.CodeSetItemDescription("Terminologies", terminologyId)

            If terminologyText = "" Then
                terminologyText = terminologyId
            End If

            Dim row As DataRow = TerminologiesTable.NewRow
            row(0) = terminologyId
            row(1) = terminologyText
            TerminologiesTable.Rows.Add(row)
        End If

        mFileManager.FileEdited = True
    End Sub

    Public Sub AddConstraintBinding(ByVal terminologyId As String, ByVal acCode As String, ByVal uri As String)
        If RmTerm.IsValidTermCode(acCode) And Not String.IsNullOrEmpty(terminologyId) Then
            AddTerminology(terminologyId)

            Dim row As DataRow = ConstraintBindingsTable.NewRow
            PopulateConstraintBindingRow(row, terminologyId, acCode, uri)

            Dim keys(1) As Object
            keys(0) = row(0)
            keys(1) = row(1)

            If ConstraintBindingsTable.Rows.Contains(keys) Then
                'change the constraint
                row = ConstraintBindingsTable.Rows.Find(keys)
                row.BeginEdit()
                PopulateConstraintBindingRow(row, terminologyId, acCode, uri)
                row.EndEdit()
            Else
                ConstraintBindingsTable.Rows.Add(row)
            End If

            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Public Function ConstraintBindingUri(ByVal terminologyId As String, ByVal release As String, ByVal subset As String) As String
        Dim result As String = ""

        If Not String.IsNullOrEmpty(terminologyId) Then
            result = System.Uri.EscapeDataString(terminologyId)

            If Not String.IsNullOrEmpty(release) Then
                result = result + "/" + System.Uri.EscapeDataString(release)
            End If

            If Not String.IsNullOrEmpty(subset) Then
                result = result + "?subset=" + System.Uri.EscapeDataString(subset)
            End If

            result = "terminology:" + result
        End If

        Return result
    End Function

    Public Sub PopulateConstraintBindingRow(ByVal row As DataRow, ByVal terminologyId As String, ByVal acCode As String, ByVal uri As String)
        If Not row Is Nothing And Not String.IsNullOrEmpty(terminologyId) And RmTerm.IsValidTermCode(acCode) Then
            Dim release As String = ""
            Dim subset As String = ""

            If Not uri Is Nothing AndAlso uri.StartsWith("terminology:") Then
                Dim s As String = uri.Substring(uri.IndexOf(":") + 1)
                Dim i As Integer = s.IndexOf("?")

                If i >= 0 Then
                    For Each parameter As String In s.Substring(i + 1).Split("&"c)
                        If parameter.StartsWith("subset=") Then
                            subset = System.Uri.UnescapeDataString(parameter.Substring(parameter.IndexOf("=") + 1))
                        End If
                    Next

                    s = s.Remove(i)
                End If

                i = s.IndexOf("/")

                If i >= 0 Then
                    release = System.Uri.UnescapeDataString(s.Substring(i + 1))
                End If
            End If

            row(0) = terminologyId
            row(1) = acCode
            row(2) = release
            row(3) = subset
            row(4) = uri
        End If
    End Sub

    Public Function GetOpenEHRTerm(ByVal code As Integer, ByVal DefaultTerm As String, Optional ByVal Language As String = "?") As String
        Dim result As String = Nothing

        ' returns the string in the language
        If Language = "?" Then
            Language = Main.Instance.SpecificLanguageCode
        End If

        Try
            result = TerminologyServer.Instance.RubricForCode(code, Language)
        Catch except As Exception
            MessageBox.Show("Error in terminology server: " & except.Message, "Ocean Archetype Parser", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If result Is Nothing Then
            result = DefaultTerm
        End If

        Return result
    End Function

    Public Function CodeForGroupID(ByVal GroupID As Integer, Optional ByVal language As String = "") As DataRow()
        Return TerminologyServer.Instance.CodesForGrouperID(GroupID, language)
    End Function

    Public Sub SetBestLanguage()
        ' set the specific language if it is present e.g. en-US, en-AU
        Dim bestLanguage As String

        If HasLanguage(Main.Instance.SpecificLanguageCode) Then
            bestLanguage = Main.Instance.SpecificLanguageCode
        ElseIf HasLanguage(Main.Instance.DefaultLanguageCode) Then
            bestLanguage = Main.Instance.DefaultLanguageCode
        ElseIf HasLanguage(Filemanager.Master.OntologyManager.LanguageCode) Then
            bestLanguage = Filemanager.Master.OntologyManager.LanguageCode
        Else
            bestLanguage = PrimaryLanguageCode
        End If

        If mFileManager.OntologyManager.LanguageCode <> bestLanguage Then
            LanguageCode = bestLanguage
        End If
    End Sub

    Private Function MakeSubjectOfDataTable() As DataTable
        ' Create a new DataTable titled 'Languages'
        Dim result As DataTable = New DataTable("SubjectOfData")

        Dim languageColumn As New DataColumn
        languageColumn.DataType = System.Type.GetType("System.String")
        languageColumn.ColumnName = "Language"
        result.Columns.Add(languageColumn)

        Dim idColumn As New DataColumn
        idColumn.DataType = System.Type.GetType("System.Int32")
        idColumn.ColumnName = "TermId"
        idColumn.AutoIncrement = False
        result.Columns.Add(idColumn)

        Dim phraseColumn As New DataColumn
        phraseColumn.DataType = System.Type.GetType("System.String")
        phraseColumn.ColumnName = "Phrase"
        result.Columns.Add(phraseColumn)

        result.PrimaryKey = New DataColumn() {languageColumn, idColumn}
        Return result
    End Function

    Private Function MakeLanguagesTable() As DataTable
        ' Create a new DataTable titled 'Languages'
        Dim result As DataTable = New DataTable("Languages")

        'First the language ID = two letter ISO code
        Dim idColumn As New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "id"
        result.Columns.Add(idColumn)

        'Second the language text from windows/ISO list
        Dim languageColumn As New DataColumn
        languageColumn.DataType = System.Type.GetType("System.String")
        languageColumn.ColumnName = "Language"
        result.Columns.Add(languageColumn)

        result.PrimaryKey = New DataColumn() {idColumn}
        Return result
    End Function

    Private Function MakeTerminologiesTable() As DataTable
        ' Create a new DataTable titled 'Terminology'
        Dim result As DataTable = New DataTable("Terminologies")

        Dim terminologyColumn As New DataColumn
        terminologyColumn.DataType = System.Type.GetType("System.String")
        terminologyColumn.ColumnName = "Terminology"
        result.Columns.Add(terminologyColumn)

        Dim descriptionColumn As New DataColumn
        descriptionColumn.DataType = System.Type.GetType("System.String")
        descriptionColumn.ColumnName = "Description"
        result.Columns.Add(descriptionColumn)

        result.PrimaryKey = New DataColumn() {terminologyColumn}
        result.DefaultView.AllowNew = False
        Return result
    End Function

    Private Function MakeConstraintBindingsTable() As DataTable
        ' Note - there are no versions of the Terminology at present
        ' as if the knowledge model shifted fundamentally then I believe
        ' that the archetype would have to be released with a new version
        ' There may be a need to keep track of which release it is up to date with

        'Also - we may need to know for all terminology - when it was added - ie what revision

        Dim result As New DataTable("ConstraintBindings")

        'Short name or ID of terminology
        Dim terminologyColumn As New DataColumn
        terminologyColumn.DataType = System.Type.GetType("System.String")
        terminologyColumn.ColumnName = "Terminology"
        result.Columns.Add(terminologyColumn)

        'AcCode acNNNN
        Dim idColumn As New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "ID"
        result.Columns.Add(idColumn)

        Dim releaseColumn As New DataColumn
        releaseColumn.DataType = System.Type.GetType("System.String")
        releaseColumn.ColumnName = "Release"
        releaseColumn.DefaultValue = ""
        result.Columns.Add(releaseColumn)

        Dim subsetColumn As New DataColumn
        subsetColumn.DataType = System.Type.GetType("System.String")
        subsetColumn.ColumnName = "Subset"
        subsetColumn.DefaultValue = ""
        result.Columns.Add(subsetColumn)

        Dim uriColumn As New DataColumn
        uriColumn.DataType = System.Type.GetType("System.String")
        uriColumn.ColumnName = "CodePhrase"
        uriColumn.DefaultValue = ""
        result.Columns.Add(uriColumn)

        result.PrimaryKey = New DataColumn() {terminologyColumn, idColumn}
        Return result
    End Function

    Private Function MakeTermBindingsTable() As DataTable
        ' Note - there are no versions of the Terminology at present
        ' as if the knowledge model shifted fundamentally then I believe
        ' that the archetype would have to be released with a new version
        ' There may be a need to keep track of which release it is up to date with

        'Also - we may need to know for all terminology - when it was added - ie what revision

        Dim result As New DataTable("TermBindings")

        Dim terminologyColumn As New DataColumn  ' the code in the terminology
        terminologyColumn.DataType = System.Type.GetType("System.String")
        terminologyColumn.ColumnName = "Terminology"
        result.Columns.Add(terminologyColumn)

        Dim pathColumn As New DataColumn
        pathColumn.DataType = System.Type.GetType("System.String")
        pathColumn.ColumnName = "Path"  ' the path - could be a node or the full path of a node +/- criteria
        result.Columns.Add(pathColumn)

        Dim codeColumn As New DataColumn
        codeColumn.DataType = System.Type.GetType("System.String")
        codeColumn.ColumnName = "Code"
        codeColumn.DefaultValue = ""
        result.Columns.Add(codeColumn)

        Dim releaseColumn As New DataColumn
        releaseColumn.DataType = System.Type.GetType("System.String")
        releaseColumn.ColumnName = "Release"
        releaseColumn.DefaultValue = ""
        result.Columns.Add(releaseColumn)

        result.PrimaryKey = New DataColumn() {terminologyColumn, pathColumn, codeColumn}
        Return result
    End Function

    Private Function MakeTermBindingCriteriaTable() As DataTable
        Dim result As New DataTable("TermBindingCriteria")

        Dim terminologyColumn As New DataColumn
        terminologyColumn.DataType = GetType(String)
        terminologyColumn.ColumnName = "Terminology"
        result.Columns.Add(terminologyColumn)

        Dim pathColumn As New DataColumn
        pathColumn.DataType = GetType(String)
        pathColumn.ColumnName = "Path"
        result.Columns.Add(pathColumn)

        Dim codeColumn As New DataColumn
        codeColumn.DataType = GetType(String)
        codeColumn.ColumnName = "Code"
        result.Columns.Add(codeColumn)

        Dim releaseColumn As New DataColumn
        releaseColumn.DataType = GetType(String)
        releaseColumn.ColumnName = "Release"
        releaseColumn.DefaultValue = ""
        result.Columns.Add(releaseColumn)

        Dim criteriaColumn As New DataColumn
        criteriaColumn.DataType = GetType(String)
        criteriaColumn.ColumnName = "Criteria"
        criteriaColumn.DefaultValue = ""
        result.Columns.Add(criteriaColumn)

        result.PrimaryKey = New DataColumn() {terminologyColumn, pathColumn, codeColumn, criteriaColumn}
        Return result
    End Function

    Private Function MakeDefinitionsTable(ByVal nm As String) As DataTable
        ' Create a new DataTable titled 'TermDefinitions' or 'ConstraintDefinitions'
        Dim result As New DataTable(nm)

        ' The id is the language of the term definition
        Dim idColumn As New DataColumn
        idColumn.DataType = GetType(String)
        idColumn.ColumnName = "id"
        result.Columns.Add(idColumn)

        Dim codeColumn As New DataColumn
        codeColumn.DataType = GetType(String)
        codeColumn.ColumnName = "Code"
        result.Columns.Add(codeColumn)

        Dim textColumn As New DataColumn
        textColumn.DataType = GetType(String)
        textColumn.ColumnName = "Text"
        result.Columns.Add(textColumn)

        Dim descriptionColumn As New DataColumn
        descriptionColumn.DataType = GetType(String)
        descriptionColumn.ColumnName = "Description"
        descriptionColumn.DefaultValue = "*"
        result.Columns.Add(descriptionColumn)

        Dim commentColumn As New DataColumn
        commentColumn.DataType = GetType(String)
        commentColumn.ColumnName = "Comment"
        commentColumn.DefaultValue = ""
        result.Columns.Add(commentColumn)

        Dim termAsObjectColumn As New DataColumn
        termAsObjectColumn.DataType = GetType(RmTerm)
        termAsObjectColumn.ColumnName = "TermAsObject"
        termAsObjectColumn.DefaultValue = Nothing
        result.Columns.Add(termAsObjectColumn)

        ' Support temporary correct ordering of internal codelists to match order in Definition
        Dim listOrderColumn As New DataColumn
        listOrderColumn.DataType = GetType(Integer)
        listOrderColumn.ColumnName = "ListOrder"
        result.Columns.Add(listOrderColumn)

        result.PrimaryKey = New DataColumn() {idColumn, codeColumn}
        Return result
    End Function

    Private Sub InitialiseTables()
        mLanguagesTable = MakeLanguagesTable()
        mTermDefinitionTable = MakeDefinitionsTable("TermDefinitions")
        mConstraintDefinitionTable = MakeDefinitionsTable("ConstraintDefinitions")
        mTerminologiesTable = MakeTerminologiesTable()
        mTermBindingsTable = MakeTermBindingsTable()
        mTermBindingCriteriaTable = MakeTermBindingCriteriaTable()
        mConstraintBindingsTable = MakeConstraintBindingsTable()
        mSubjectOfDataTable = MakeSubjectOfDataTable()

        mLanguageDS.Tables.Add(mLanguagesTable)
        mLanguageDS.Tables.Add(mTermDefinitionTable)
        mLanguageDS.Tables.Add(mConstraintDefinitionTable)
        mLanguageDS.Tables.Add(mTerminologiesTable)
        mLanguageDS.Tables.Add(mTermBindingsTable)
        mLanguageDS.Tables.Add(mTermBindingCriteriaTable)
        mLanguageDS.Tables.Add(mConstraintBindingsTable)
        mLanguageDS.Tables.Add(mSubjectOfDataTable)

        Dim termBindingColumns As DataColumn() = {mTermBindingsTable.Columns(0), mTermBindingsTable.Columns(1), mTermBindingsTable.Columns(2)}
        Dim termBindingCriteriaColumns As DataColumn() = {mTermBindingCriteriaTable.Columns(0), mTermBindingCriteriaTable.Columns(1), mTermBindingCriteriaTable.Columns(2)}

        mLanguageDS.Relations.Add(New DataRelation("LanguageTerms", mLanguagesTable.Columns(0), mTermDefinitionTable.Columns(0)))
        mLanguageDS.Relations.Add(New DataRelation("LanguageConstraints", mLanguagesTable.Columns(0), mConstraintDefinitionTable.Columns(0)))
        mLanguageDS.Relations.Add(New DataRelation("TerminologiesBindings", mTerminologiesTable.Columns(0), mTermBindingsTable.Columns(0)))
        mLanguageDS.Relations.Add(New DataRelation("TermBindingTermBindingCriteria", termBindingColumns, termBindingCriteriaColumns))
        mLanguageDS.Relations.Add(New DataRelation("TerminologiesConstraintBindings", mConstraintDefinitionTable.Columns(1), mConstraintBindingsTable.Columns(1), False))
        mLanguageDS.Relations.Add(New DataRelation("LanguageSubjectOfData", mLanguagesTable.Columns(0), mSubjectOfDataTable.Columns(0)))
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

    Public Sub DefinitionsTable_RowChanged(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles mTermDefinitionTable.RowChanged, mConstraintDefinitionTable.RowChanged
        If mDoUpdateOntology Then
            If e.Action = DataRowAction.Change Then
                Dim term As RmTerm = TryCast(e.Row(5), RmTerm)

                If term Is Nothing Then
                    term = New RmTerm(CStr(e.Row(1)))
                End If

                term.Language = TryCast(e.Row(0), String)
                term.Text = TryCast(e.Row(2), String)
                term.Description = TryCast(e.Row(3), String)
                term.Comment = TryCast(e.Row(4), String)

                mOntology.ReplaceTerm(term, ReplaceTranslations(term))
                mFileManager.FileEdited = True
                'DO NOT DELETE TERMS - these may be used elsewhere so are removed at end of session
                'ElseIf e.Action = DataRowAction.Delete Then
                '    mOntology.DeleteTerm(aterm)
            End If
        End If
    End Sub

    Public Sub TermBindingsTable_RowChanged(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles mTermBindingsTable.RowChanged, mTermBindingsTable.RowDeleting
        If mDoUpdateOntology Then
            Dim terminologyId As String = TryCast(e.Row(0), String)
            Dim path As String = TryCast(e.Row(1), String)
            Dim code As String = TryCast(e.Row(2), String)
            Dim release As String = TryCast(e.Row(3), String)

            If e.Action = DataRowAction.Add Or e.Action = DataRowAction.Change Then
                code = System.Text.RegularExpressions.Regex.Replace(code, "[\*\(\)\]\[\~\`\!\@\#\$\%\^\&\+\=\""\{\}\|\;\:\?/\<\>\s]", "")

                If Not String.IsNullOrEmpty(terminologyId) And Not String.IsNullOrEmpty(path) And Not String.IsNullOrEmpty(code) Then
                    mOntology.AddorReplaceTermBinding(terminologyId, path, code, release)
                End If
            ElseIf e.Action = DataRowAction.Delete Then
                mOntology.RemoveTermBinding(terminologyId, path)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Public Sub ConstraintBindingsTable_RowChanged(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles mConstraintBindingsTable.RowChanged, mConstraintBindingsTable.RowDeleting
        If mDoUpdateOntology Then
            Dim terminologyId As String = TryCast(e.Row(0), String)
            Dim acCode As String = TryCast(e.Row(1), String)
            Dim release As String = TryCast(e.Row(2), String)
            Dim subset As String = TryCast(e.Row(3), String)

            If e.Action = DataRowAction.Add Or e.Action = DataRowAction.Change Then
                Dim uri As String = ConstraintBindingUri(terminologyId, release, subset)

                If Not String.IsNullOrEmpty(terminologyId) And Not String.IsNullOrEmpty(acCode) And Not String.IsNullOrEmpty(uri) Then
                    If Not uri.Equals(e.Row(4)) Then
                        e.Row(4) = uri
                    End If

                    mOntology.AddorReplaceConstraintBinding(terminologyId, acCode, uri)
                End If
            ElseIf e.Action = DataRowAction.Delete Then
                mOntology.RemoveConstraintBinding(terminologyId, acCode)
            End If

            mFileManager.FileEdited = True
        End If
    End Sub

    Sub New(ByVal fileManager As FileManagerLocal)
        mLanguageDS = New DataSet("LanguageDataSet")
        InitialiseTables()
        mFileManager = fileManager
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

