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

    ' HKF: 1606
    Private WithEvents mTermBindingCriteriaTable As DataTable

    Private mTerminologiesTable As DataTable
    Private mSubjectOfDataTable As DataTable
    Private mLanguageText As String
    Private mLanguageCode As String
    Private mPrimaryLanguageText As String
    Private mLastLanguageText As String
    Private mLastTerminologyText As String
    'Private sDefaultLanguageCode As String
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

    Public ReadOnly Property hasTermBinding(ByVal a_terminology As String, ByVal a_code As String, ByVal a_path As String) As Boolean
        Get
            Dim key(2) As Object
            key(0) = a_terminology
            key(1) = a_path
            key(2) = a_code
            Return Not Me.TermBindingsTable.Rows.Find(key) Is Nothing
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

    Function HasTerminology(ByVal a_terminology_id As String) As Boolean
        Return Not mTerminologiesTable.Rows.Find(a_terminology_id) Is Nothing
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
        'If Me.TermDefinitionTable.Rows.Count > 0 Then
        '    Stop
        'End If

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
        Me.mReplaceTranslations = 0

    End Sub

    Public Sub Reset(Optional ByVal LanguageCode As String = "") ' resets for beginning a new archetype
        Me.mReplaceTranslations = 0
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

    Public Function LanguageIsAvailable(ByVal code As String) As Boolean
        Return Not mLanguagesTable.Rows.Find(code) Is Nothing
    End Function

    Public Function GetTerm(ByVal Code As String) As RmTerm
        Dim Keys(1) As Object

        If Code Is Nothing Then
            ' return empty term
            Dim aterm As New RmTerm("")
            Return aterm
        Else
            If Not mLastTerm Is Nothing Then
                If mLastTerm.Code = Code Then
                    If mLastTerm.Language = mLanguageCode Then
                        Return mLastTerm
                    End If
                End If
            End If

            Dim aterm As RmTerm
            Dim d_row As DataRow

            Keys(0) = mLanguageCode
            Keys(1) = Code
            aterm = New RmTerm(Code)

            If aterm.isConstraint Then
                d_row = mConstraintDefinitionsTable.Rows.Find(Keys)
            Else
                d_row = mTermDefinitionsTable.Rows.Find(Keys)
            End If
            If Not d_row Is Nothing Then
                aterm.Language = mLanguageCode
                aterm.Text = CStr(d_row(2))
                aterm.Description = CStr(d_row(3))
                If Not aterm.isConstraint Then
                    'SRH 8 Nov 2007 - Added check for null
                    If Not TypeOf (d_row(4)) Is System.DBNull Then
                        aterm.Comment = CStr(d_row(4))
                    End If
                End If
            End If
            mLastTerm = aterm  ' remember last one for efficiency
            Return aterm
            End If
    End Function

    Public Function SpecialiseTerm(ByVal Text As String, ByVal Description As String, ByVal Id As String) As RmTerm
        Dim aterm As RmTerm

        aterm = mOntology.SpecialiseTerm(Text, Description, Id)
        UpdateTerm(aterm)
        Return aterm

    End Function

    Public Function SpecialiseTerm(ByVal a_term As RmTerm) As RmTerm
        Dim new_term As RmTerm

        new_term = mOntology.SpecialiseTerm(a_term.Text & "**", a_term.Description, a_term.Code)
        UpdateTerm(new_term)
        Return new_term
    End Function

    Public Function SpecialiseTerm(ByVal a_term_code As String) As RmTerm
        Dim new_term As RmTerm = Nothing
        Dim a_term As RmTerm

        a_term = mOntology.TermForCode(a_term_code, mLanguageCode)
        If Not a_term Is Nothing Then
            new_term = mOntology.SpecialiseTerm(a_term.Text & "**", a_term.Description, a_term.Code)
            UpdateTerm(new_term)
        End If
        Return new_term

    End Function

    Public Function NextTermId() As String
        Return mOntology.NextTermId
    End Function

    Public Function AddTerm(ByVal Text As String, Optional ByVal Description As String = "*") As RmTerm

        ' ensure there are no " in the string
        Text = Text.Replace("""", "'")
        Description = Description.Replace("""", "'")

        mLastTerm = New RmTerm(mOntology.NextTermId)
        mLastTerm.Text = Text
        mLastTerm.Description = Description
        mOntology.AddTerm(mLastTerm)
        UpdateTerm(mLastTerm)
        Return mLastTerm
    End Function

    Public Sub UpdateTerm(ByVal aterm As RmTerm)
        Dim d_row, l_row As DataRow

        ' add it to the GUI if it is not internal
        Debug.Assert(aterm.Code <> "")
        Debug.Assert(aterm.Text <> "")
        'CHANGE Sam Heard 2004-06-17
        'Can have empty description
        'Debug.Assert(aterm.Description <> "")
        If aterm.Code = "" Then
            Debug.Assert(False)
            Return
        ElseIf aterm.Text = "" Then
            Debug.Assert(False)
            aterm.Text = "?"
        ElseIf aterm.Description = "" Then
            aterm.Description = "*"
        End If

        If aterm.isConstraint Then
            For Each l_row In mLanguagesTable.Rows

                d_row = mConstraintDefinitionsTable.NewRow
                d_row(0) = l_row(0)
                d_row(1) = aterm.Code
                d_row(2) = aterm.Text
                d_row(3) = aterm.Description
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
                d_row(1) = aterm.Code
                d_row(2) = aterm.Text
                d_row(3) = aterm.Description
                Try
                    mTermDefinitionsTable.Rows.Add(d_row)
                Catch e As Exception
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

        End If

    End Sub

    Private Sub UpdateLanguage(ByVal LanguageCode As String)
        mOntology.PopulateTermsInLanguage(Me, LanguageCode)
    End Sub

    Public Function AddConstraint(ByVal Text As String, Optional ByVal Description As String = "*") As RmTerm
        Dim d_row As DataRow


        ' ensure there are no " in the string
        Text = Text.Replace("""", "'")
        Description = Description.Replace("""", "'")

        mLastTerm = New RmTerm(mOntology.NextConstraintID)
        mLastTerm.Text = Text
        mLastTerm.Description = Description
        mOntology.AddConstraint(mLastTerm)
        d_row = mConstraintDefinitionsTable.NewRow
        d_row(0) = mLanguageCode
        d_row(1) = mLastTerm.Code
        d_row(2) = Text
        d_row(3) = Description
        mConstraintDefinitionsTable.Rows.Add(d_row)
        Return mLastTerm
    End Function

    Public Overloads Sub SetText(ByVal Value As String, ByVal code As String)


        ' ensure there are no " in the string
        Value = Value.Replace("""", "'")

        mLastTerm = Me.GetTerm(code)
        mLastTerm.Text = Value
        SetText(mLastTerm)
    End Sub

    Private Function ReplaceTranslations() As Boolean
        If (mLanguageCode = Me.PrimaryLanguageCode) Then
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

    Public Overloads Sub SetText(ByVal aTerm As RmTerm)
        If aTerm.isConstraint Then
            Update(aTerm, mConstraintDefinitionsTable, ReplaceTranslations())
        Else
            Update(aTerm, mTermDefinitionsTable, ReplaceTranslations())
        End If
        mLastTerm = aTerm
    End Sub

    Public Function GetText(ByVal ConceptCode As String) As String 'JAR: 13APR07, EDT-32 Support unicode
        Dim mGetTerm As RmTerm
        mGetTerm = GetTerm(ConceptCode)
        If Not mGetTerm Is Nothing Then
            Return mGetTerm.Text
        End If
        Return ""
    End Function

    Public Overloads Sub SetDescription(ByVal Value As String, ByVal code As String)

        ' ensure there are no " in the string
        Value = Value.Replace("""", "'")

        mLastTerm = Me.GetTerm(code)
        mLastTerm.Description = Value
        SetText(mLastTerm)

    End Sub

    Public Function GetDescription(ByVal ConceptCode As String) As String 'JAR: 13APR07, EDT-32 Support unicode
        Dim mGetTerm As RmTerm
        mGetTerm = GetTerm(ConceptCode)
        If Not mGetTerm Is Nothing Then
            Return mGetTerm.Description
        End If
        Return ""
    End Function

    Public Overloads Sub SetComment(ByVal Value As String, ByVal code As String)

        ' ensure there are no " in the string
        Value = Value.Replace("""", "'")

        mLastTerm = Me.GetTerm(code)
        mLastTerm.Comment = Value
        SetText(mLastTerm)

    End Sub

    Private Sub Update(ByVal aTerm As RmTerm, ByVal aTable As DataTable, Optional ByVal ReplaceTranslations As Boolean = False)
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
                    If Not aTerm.isConstraint Then
                        If CStr(d_row(4)) <> aTerm.Comment Then
                            d_row(4) = aTerm.Comment
                        End If
                    End If
                    d_row.EndEdit()
                Else
                    mDoUpdateOntology = False 'as ontology changes are handled there
                    d_row.BeginEdit()
                    d_row(2) = "*" & aTerm.Text & "(" & mLanguageCode & ")"
                    d_row(3) = "*" & aTerm.Description & "(" & mLanguageCode & ")"
                    If Not aTerm.isConstraint Then
                        If Not (CStr(d_row(4)) = "" And aTerm.Comment = "") Then
                            d_row(4) = "*" & aTerm.Comment & "(" & mLanguageCode & ")"
                        End If
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
                If CStr(d_row(2)) <> aTerm.Text Then
                    d_row(2) = aTerm.Text
                End If
                If CStr(d_row(3)) <> aTerm.Description Then
                    d_row(3) = aTerm.Description
                End If
                If CStr(d_row(4)) <> aTerm.Comment Then
                    d_row(4) = aTerm.Comment
                End If
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
                'update the new terms generated by the 
                'ontology
                UpdateLanguage(a_LanguageCode)
            End If

            mFileManager.FileEdited = True

        End If
    End Sub

    Public Sub AddTerminology(ByVal TerminologyCode As String, Optional ByVal TerminologyText As String = "")
        Dim key As Object

        If TerminologyText = "" Then
            ' get the full name of the language from openEHR terminology
            mLastTerminologyText = TerminologyServer.Instance.CodeSetItemDescription("Terminology", TerminologyCode)
        Else
            mLastTerminologyText = TerminologyText
        End If
        key = TerminologyCode
        If TerminologiesTable.Rows.Find(key) Is Nothing Then
            Dim new_row As DataRow
            new_row = mTerminologiesTable.NewRow
            new_row(0) = TerminologyCode
            new_row(1) = mLastTerminologyText
            mTerminologiesTable.Rows.Add(new_row)
        End If
        ' ensure it is in the ontology as well
        If mDoUpdateOntology Then
            mOntology.AddTerminology(TerminologyCode)
        End If

        mFileManager.FileEdited = True

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

    ' HKF: 1606
    Private Function MakeTermBindingCriteriaTable() As DataTable
        Dim BindingCriteriaTable As New DataTable("TermBindingCriteria")

        ' Add six column objects to the table.
        Dim terminologyColumn As DataColumn = New DataColumn
        terminologyColumn.DataType = GetType(String) 'System.Type.GetType("System.String")
        terminologyColumn.ColumnName = "Terminology"
        BindingCriteriaTable.Columns.Add(terminologyColumn)

        Dim PathColumn As DataColumn = New DataColumn
        PathColumn.DataType = GetType(String) 'System.Type.GetType("System.String")
        PathColumn.ColumnName = "Path"
        BindingCriteriaTable.Columns.Add(PathColumn)

        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = GetType(String) 'System.Type.GetType("System.String")
        CodeColumn.ColumnName = "Code"
        BindingCriteriaTable.Columns.Add(CodeColumn)

        Dim ReleaseColumn As DataColumn = New DataColumn
        ReleaseColumn.DataType = GetType(String) 'System.Type.GetType("System.String")
        ReleaseColumn.ColumnName = "Release"
        ReleaseColumn.DefaultValue = ""
        BindingCriteriaTable.Columns.Add(ReleaseColumn)

        Dim CriteriaColumn As New DataColumn
        CriteriaColumn.DataType = GetType(String) 'System.Type.GetType("System.String")
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
        Dim DefinitionsTable As DataTable
        ' Create a new DataTable titled 'TermDefinitions' or 'ConstraintDefinitions'
        DefinitionsTable = New DataTable(nm)
        ' Add Four column objects to the table.
        ' The id is the language of the term defintion
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.String")
        idColumn.ColumnName = "id"
        DefinitionsTable.Columns.Add(idColumn)
        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = System.Type.GetType("System.String")
        CodeColumn.ColumnName = "Code"
        DefinitionsTable.Columns.Add(CodeColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        DefinitionsTable.Columns.Add(TextColumn)
        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = System.Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        DescriptionColumn.DefaultValue = "*"
        DefinitionsTable.Columns.Add(DescriptionColumn)
        Dim CommentColumn As DataColumn = New DataColumn
        CommentColumn.DataType = System.Type.GetType("System.String")
        CommentColumn.ColumnName = "Comment"
        CommentColumn.DefaultValue = ""
        DefinitionsTable.Columns.Add(CommentColumn)
        ' Return the new DataTable.
        Dim keys(1) As DataColumn
        keys(0) = idColumn
        keys(1) = CodeColumn
        DefinitionsTable.PrimaryKey = keys

        Return DefinitionsTable

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

        ' HKF: 1606
        ' TermBindingCriteria
        mTermBindingCriteriaTable = MakeTermBindingCriteriaTable()
        mLanguageDS.Tables.Add(mTermBindingCriteriaTable)
        Dim termBindingColumns As DataColumn() = {mTermBindingsTable.Columns(0), _
                mTermBindingsTable.Columns(1), mTermBindingsTable.Columns(2)}
        Dim termBindingCriteriaColumns As DataColumn() = {mTermBindingCriteriaTable.Columns(0), _
                mTermBindingCriteriaTable.Columns(1), mTermBindingCriteriaTable.Columns(2)}
        new_relation = New DataRelation("TermBindingTermBindingCriteria", _
                termBindingColumns, termBindingCriteriaColumns)

        mLanguageDS.Relations.Add(new_relation)

        mConstraintBindingsTable = MakeConstraintBindingTable()
        mLanguageDS.Tables.Add(mConstraintBindingsTable)

        new_relation = New DataRelation("TerminologiesConstraintBindings", mConstraintDefinitionsTable.Columns(1), mConstraintBindingsTable.Columns(1), False)
        mLanguageDS.Relations.Add(new_relation)

        mSubjectOfDataTable = MakeSubjectOfDataTable()
        Me.mLanguageDS.Tables.Add(mSubjectOfDataTable)
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
                aterm.Language = CStr(e.Row(0))
                aterm.Text = CStr(e.Row(2))
                aterm.Description = CStr(e.Row(3))
                If aterm.isConstraint Then
                    mOntology.ReplaceConstraint(aterm)
                Else
                    aterm.Comment = CStr(e.Row(4))
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
                    sTerminology = CStr(e.Row(0))
                Else
                    Return
                End If

                Dim sPath As String

                If Not e.Row(1) Is Nothing Then     'Path
                    sPath = CStr(e.Row(1))
                Else
                    Return
                End If

                Dim sCode As String
                If Not e.Row(2) Is Nothing Then     'Code
                    sCode = CStr(e.Row(2))
                Else
                    Return
                End If

                Dim sRelease As String = ""
                If Not e.Row(3) Is Nothing Then     'Release
                    sRelease = CStr(e.Row(3))
                End If

                If e.Action = DataRowAction.Add Or e.Action = DataRowAction.Change Then
                    If (sTerminology <> "" And sPath <> "" And sCode <> "") Then
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
                    sTerminology = CStr(e.Row(0))
                Else
                    Return
                End If

                Dim sCode As String = ""
                If Not e.Row(1) Is Nothing Then     'Code
                    sCode = CStr(e.Row(1))
                Else
                    Return
                End If

                Dim sQuery As String

                If Not e.Row(2) Is Nothing Then     'Query
                    sQuery = CStr(e.Row(2))
                Else
                    Return
                End If


                Dim sRelease As String = ""
                If Not e.Row(3) Is Nothing Then     'Release
                    sRelease = CStr(e.Row(3))
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

