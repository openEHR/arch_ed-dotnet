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
            Return mTermDefinitionTable
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

            Dim keys(1) As Object
            keys(0) = mLanguageCode
            keys(1) = code
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
                    result.Text = CStr(row(2))
                    result.Description = CStr(row(3))
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

        If term.IsConstraint Then
            For Each row As DataRow In mLanguagesTable.Rows
                Dim newRow As DataRow = mConstraintDefinitionTable.NewRow
                newRow(0) = row(0)
                newRow(1) = term.Code
                newRow(2) = term.Text
                newRow(3) = term.Description

                Try
                    mConstraintDefinitionTable.Rows.Add(newRow)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        Else
            For Each row As DataRow In mLanguagesTable.Rows
                Dim newRow As DataRow = mTermDefinitionTable.NewRow
                newRow(0) = row(0)
                newRow(1) = term.Code
                newRow(2) = term.Text
                newRow(3) = term.Description
                newRow(5) = term

                Try
                    mTermDefinitionTable.Rows.Add(newRow)
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
        Dim row As DataRow = mConstraintDefinitionTable.NewRow
        row(0) = mLanguageCode
        row(1) = mLastTerm.Code
        row(2) = Text
        row(3) = Description
        mConstraintDefinitionTable.Rows.Add(row)
        Return mLastTerm
    End Function

    Private Function ReplaceTranslations(ByVal term As RmTerm) As Boolean
        Dim result As Boolean = False

        If mLanguageCode = PrimaryLanguageCode And mOntology.IsMultiLanguage Then
            If mReplaceTranslations = 0 Then
                mReplaceTranslations = 1
                Dim message As String = AE_Constants.Instance.ReplaceTranslations + Environment.NewLine + term.Code + " = """ + term.Text + """"

                If MessageBox.Show(Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    mReplaceTranslations = -1
                End If
            End If

            result = mReplaceTranslations = -1
        End If

        Return result
    End Function

    Public Sub SetRmTermText(ByVal term As RmTerm)
        If term.IsConstraint Then
            Update(term, mConstraintDefinitionTable)
        Else
            Update(term, mTermDefinitionTable)
        End If

        mLastTerm = term
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

    Public Sub SetOtherAnnotation(ByVal key As String, ByVal value As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.OtherAnnotations.Item(key) = value
        SetRmTermText(mLastTerm)
    End Sub

    Public Sub DeleteOtherAnnotation(ByVal Key As String, ByVal code As String)
        mLastTerm = GetTerm(code)
        mLastTerm.OtherAnnotations.Remove(Key)
        SetRmTermText(mLastTerm)
    End Sub

    Private Sub Update(ByVal term As RmTerm, ByVal table As DataTable)
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

                    If Not term.IsConstraint Then
                        If TryCast(row(4), String) <> term.Comment Then
                            row(4) = term.Comment
                        End If

                        row(5) = term
                    End If

                    row.EndEdit()
                Else
                    mDoUpdateOntology = False 'as ontology changes are handled there
                    row.BeginEdit()
                    row(2) = "*" & term.Text & "(" & mLanguageCode & ")"
                    row(3) = "*" & term.Description & "(" & mLanguageCode & ")"

                    If Not term.IsConstraint Then
                        If Not IsDBNull(row(4)) Or term.Comment <> "" Then
                            row(4) = "*" & term.Comment & "(" & mLanguageCode & ")"
                        End If

                        row(5) = term
                    End If

                    row.EndEdit()
                    mDoUpdateOntology = priorSetting
                End If
            Next
        Else
            'Need to update ontology here
            Dim keys(1) As Object
            keys(0) = mLanguageCode
            keys(1) = term.Code
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

        mFileManager.FileEdited = True
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
                UpdateLanguage(languageCode)
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

    Private Function MakeConstraintBindingsTable() As DataTable
        ' Note - there are no versions of the Terminology at present
        ' as if the knowledge model shifted fundamentally then I believe
        ' that the archetype would have to be released with a new version
        ' There may be a need to keep track of which release it is up to date with

        'Also - we may need to know for all terminology - when it was added - ie what revision

        Dim result As DataTable = New DataTable("ConstraintBindings")

        'Short name or ID of terminology
        Dim termColumn As DataColumn = New DataColumn
        termColumn.DataType = System.Type.GetType("System.String")
        termColumn.ColumnName = "Terminology"
        result.Columns.Add(termColumn)

        'AcCode acNNNN
        Dim iDColumn As DataColumn = New DataColumn
        iDColumn.DataType = System.Type.GetType("System.String")
        iDColumn.ColumnName = "ID"
        result.Columns.Add(iDColumn)

        'Release
        Dim releaseColumn As DataColumn = New DataColumn
        releaseColumn.DataType = System.Type.GetType("System.String")
        releaseColumn.ColumnName = "Release"
        releaseColumn.DefaultValue = ""
        result.Columns.Add(releaseColumn)

        'Subset
        Dim subsetColumn As DataColumn = New DataColumn
        subsetColumn.DataType = System.Type.GetType("System.String")
        subsetColumn.ColumnName = "Subset"
        subsetColumn.DefaultValue = ""
        result.Columns.Add(subsetColumn)

        'URI
        Dim uriColumn As DataColumn = New DataColumn
        uriColumn.DataType = System.Type.GetType("System.String")
        uriColumn.ColumnName = "CodePhrase"
        uriColumn.DefaultValue = ""
        result.Columns.Add(uriColumn)

        ' Return the new DataTable.
        Dim keys(1) As DataColumn
        keys(0) = termColumn
        keys(1) = iDColumn
        result.PrimaryKey = keys

        Return result
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

        mTermDefinitionTable = MakeDefinitionsTable("TermDefinitions")
        mLanguageDS.Tables.Add(mTermDefinitionTable)

        mConstraintDefinitionTable = MakeDefinitionsTable("ConstraintDefinitions")
        mLanguageDS.Tables.Add(mConstraintDefinitionTable)

        ' add relations
        new_relation = New DataRelation("LanguageTerms", mLanguagesTable.Columns(0), mTermDefinitionTable.Columns(0))
        mLanguageDS.Relations.Add(new_relation)

        new_relation = New DataRelation("LanguageConstraints", mLanguagesTable.Columns(0), mConstraintDefinitionTable.Columns(0))
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

        mConstraintBindingsTable = MakeConstraintBindingsTable()
        mLanguageDS.Tables.Add(mConstraintBindingsTable)

        new_relation = New DataRelation("TerminologiesConstraintBindings", mConstraintDefinitionTable.Columns(1), mConstraintBindingsTable.Columns(1), False)
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

                If term.IsConstraint Then
                    mOntology.ReplaceConstraint(term)
                Else
                    If Not IsDBNull(e.Row(4)) Then
                        term.Comment = TryCast(e.Row(4), String)
                    End If

                    mOntology.ReplaceTerm(term, ReplaceTranslations(term))
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

    Sub New(ByVal a_file_manager As FileManagerLocal)
        mLanguageDS = New DataSet("LanguageDataSet")
        InitialiseTables()
        AE_Constants.Create(Main.Instance.DefaultLanguageCode)
        Main.Instance.Options.ValidateConfiguration()
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

