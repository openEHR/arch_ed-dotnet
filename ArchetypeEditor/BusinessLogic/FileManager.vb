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
'	file:        "$Source: source/vb.net/archetype_editor/BusinessLogic/SCCS/s.FileManager.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'
'Option Strict On
Option Explicit On 
Imports XMLParser

Public Class FileManagerLocal
    Private mIsFileDirty As Boolean
    Private mIsFileLoading As Boolean
    Private mParserSynchronised As Boolean = True
    Private mHasOpenFileError, mHasWriteFileError As Boolean
    Private mArchetypeEngine As Parser
    Private mFileName, mPriorFileName As String
    Private mWorkingDirectory As String
    Private mIsNew As Boolean = False
    Private mObjectToSave As Object
    Private mOntologyManager As New OntologyManager(Me)

    Public Property OntologyManager() As OntologyManager
        Get
            Return mOntologyManager
        End Get
        Set(ByVal Value As OntologyManager)
            mOntologyManager = Value
        End Set
    End Property
    Public ReadOnly Property ArchetypeAvailable() As Boolean
        Get
            Return mArchetypeEngine.ArchetypeAvailable()
        End Get
    End Property
    Public Property IsNew() As Boolean
        Get
            Return mIsNew
        End Get
        Set(ByVal Value As Boolean)
            mIsNew = Value
            If Value Then
                mParserSynchronised = False
            End If
        End Set
    End Property
    Public WriteOnly Property ObjectToSave() As Object
        Set(ByVal Value As Object)
            'Containing object must have a "PrepareToSave" sub
            mObjectToSave = Value
        End Set
    End Property
    Public Property WorkingDirectory() As String
        Get
            Return mWorkingDirectory
        End Get
        Set(ByVal Value As String)
            mWorkingDirectory = Value
        End Set
    End Property
    Public ReadOnly Property ParserType() As String
        Get
            Return mArchetypeEngine.TypeName()
        End Get
    End Property
    Public Property FileName() As String
        Get
            Return mFileName
        End Get
        Set(ByVal Value As String)
            mFileName = Value
        End Set
    End Property
    Public ReadOnly Property OpenFileError() As Boolean
        Get
            Return mHasOpenFileError
        End Get
    End Property
    Public ReadOnly Property WriteFileError() As Boolean
        Get
            Return mHasWriteFileError
        End Get
    End Property
    Public Property FileEdited() As Boolean
        Get
            Return mIsFileDirty
        End Get
        Set(ByVal Value As Boolean)
            'As the File Save might have been set by an embedded
            'archetype so have to raise the event
            If Not mIsFileLoading Then
                mIsFileDirty = Value
                Filemanager.SetFileChangedToolBar(Value)
                mParserSynchronised = Not Value
            End If
        End Set
    End Property
    'EDT-584 - to allow out of date archetypes to be updated
    Public Sub FileUpdateRequired()
        'allows override of file edited when loading
        Dim temp As Boolean
        temp = mIsFileLoading
        mIsFileLoading = False
        FileEdited = True
        mIsFileLoading = temp
    End Sub


    Public Property ParserSynchronised() As Boolean
        Get
            Return mParserSynchronised
        End Get
        Set(ByVal Value As Boolean)
            mParserSynchronised = Value
        End Set
    End Property

    Public ReadOnly Property AvailableFormatFilter() As String
        Get
            ' returns a filedialog filter
            Dim format_filter As String = ""

            For i As Integer = 0 To mArchetypeEngine.AvailableFormats.Count - 1
                Dim s As String = CStr(mArchetypeEngine.AvailableFormats(i))
                format_filter &= s.ToUpper(System.Globalization.CultureInfo.InvariantCulture) & "|*." & s & "|"
            Next

            Select Case ParserType
                Case "adl"
                    format_filter &= "XML|*.xml"
                Case "xml"
                    format_filter &= "ADL|*.adl"
                Case Else
                    Debug.Assert(False, ParserType & " not handled")
            End Select

            Return format_filter
        End Get
    End Property

    Public ReadOnly Property AvailableFormats() As ArrayList
        Get
            Dim formats As ArrayList = mArchetypeEngine.AvailableFormats

            'Ensure ADL and XML available as provided by parsers
            If Not formats.Contains("xml") Then
                formats.Add("xml")
            End If

            If Not formats.Contains("adl") Then
                formats.Add("adl")
            End If

            Return formats
        End Get
    End Property

    Public Function IndexOfFormat(ByVal a_format As String) As Integer
        ' returns a filedialog filter
        Return mArchetypeEngine.AvailableFormats.IndexOf(a_format)
    End Function

    Public Property FileLoading() As Boolean
        Get
            Return mIsFileLoading
        End Get
        Set(ByVal Value As Boolean)
            If mIsFileLoading <> Value Then
                mIsFileLoading = Value
                ' now stop recursive updates through the interface to the ontology
                ' when the file is loading, otherwise allow them
                OntologyManager.DoUpdateOntology = Not Value
            End If
        End Set
    End Property

    Public ReadOnly Property Status() As String
        Get
            Return mArchetypeEngine.Status
        End Get
    End Property

    Public ReadOnly Property Archetype() As Archetype
        Get
            Return mArchetypeEngine.Archetype()
        End Get
    End Property

    Private InitialisedTerminologies As Collections.Generic.List(Of String)
    Private webError As Boolean

    Public Function OpenArchetype(ByVal aFileName As String) As Boolean
        Try
            mPriorFileName = FileName

            'Need to check file name for eMail extensions

            'Dim i As Integer = aFileName.LastIndexOf("."c)

            'If i = 0 Then
            '    Return False
            'Else
            '    Dim ext As String = aFileName.Substring(0, i).ToLowerInvariant
            '    If ext <> "adl" Or ext <> "xml" Then
            '        'could be an email temporary name

            '    End If
            'End If

            FileName = aFileName

            If aFileName.ToLowerInvariant().EndsWith(".adl") Then
                If mArchetypeEngine Is Nothing Or ParserType.ToLowerInvariant() = "xml" Then
                    mOntologyManager.Ontology = Nothing
                    mArchetypeEngine = Nothing
                    mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
                End If
            ElseIf aFileName.ToLowerInvariant().EndsWith(".xml") Then
                If mArchetypeEngine Is Nothing Or ParserType.ToLowerInvariant() = "adl" Then
                    mOntologyManager.Ontology = Nothing
                    mArchetypeEngine = Nothing
                    mArchetypeEngine = New ArchetypeEditor.XML_Classes.XML_Interface
                End If
            Else
                MessageBox.Show("File type: " & aFileName & " is not supported", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False    ' FIXME: spaghetti code!
            End If

            mHasOpenFileError = False
            mArchetypeEngine.OpenFile(aFileName, Me)

            ' if this archtype comes from the web, aFileName will be the URL.
            ' In this case we have to download the file temporarily on the users system so that it can be opened in the Editor.
            ' It will be downloaded in the temporary system folder and deleted immediatly after it has been opened in the Editor.
            ' User has to save the file manually if a local copy of it is wanted.
            ' This avoids data and file overflow.

            If aFileName.StartsWith(System.IO.Path.GetTempPath) Then
                'SRH 22 Aug 2009 - [EDT-570]
                System.IO.File.SetAttributes(aFileName, IO.FileAttributes.Normal)
                System.IO.File.Delete(aFileName)
                'From VB subassembly (to be avoided)
                'Kill(aFileName)
                'Set the file name to the current directory and not the temp folder
                FileName = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), System.IO.Path.GetFileName(aFileName))
            End If

            If mArchetypeEngine.OpenFileError Then
                mHasOpenFileError = True
                Return False    ' FIXME: spaghetti code!
            End If

            mOntologyManager.PopulateAllTerms() 'Note: call switches on FileEdited!
            mOntologyManager.SetBestLanguage()

            mPriorFileName = Nothing
            FileEdited = False
            CheckFileNameAgainstArchetypeId()

            Return True ' FIXME: spaghetti code!
        Catch e As Exception
            ' Need to indicate reason for exception
            FileName = mPriorFileName
            mPriorFileName = Nothing
        End Try
    End Function

    Private Sub CheckFileNameAgainstArchetypeId()
        Dim name As String = IO.Path.GetFileNameWithoutExtension(FileName)

        If String.Compare(name, Archetype.Archetype_ID.ToString, True) <> 0 Then
            If ArchetypeID.IsValidId(name) Then
                'validate the concept to update it with the correct case and remove illegal characters
                Dim Id1 As New ArchetypeID(Archetype.Archetype_ID.ToString)
                Id1.Concept = Id1.ValidConcept(Id1.Concept, "", False)

                Dim Id2 As New ArchetypeID(name)
                Id2.Concept = Id2.ValidConcept(Id2.Concept, "", False)

                Dim frm As New ChooseFix(mOntologyManager, Id1.ToString, Id2.ToString)
                frm.ShowDialog()

                If frm.Selection <> ChooseFix.FixOption.Ignore Then
                    'NOTE: The following updates can occur!
                    '    Update 1: Update if concept was changed in ValidConcept call
                    '    Update 2: Update according to the user selection

                    Dim use As String = IIf(frm.Selection = ChooseFix.FixOption.UseId, Id1.ToString, Id2.ToString)

                    'update filename if changed
                    If String.Compare(name, use, True) <> 0 Then 'case insensitive (windows o/s has issues updating file name case!)
                        mPriorFileName = FileName
                        FileName = IO.Path.Combine(IO.Path.GetDirectoryName(FileName), use & "." & ParserType)
                    End If

                    'update archetype id if changed
                    If Archetype.Archetype_ID.ToString <> use Then 'case sensitive
                        Archetype.Archetype_ID.SetFromString(use)
                        Archetype.UpdateArchetypeId() 'force details set above to be updated in the Eiffel parser                
                    End If

                    Dim priorFileLoading As Boolean = FileLoading
                    FileLoading = False
                    FileEdited = True
                    FileLoading = priorFileLoading
                End If
            End If
        End If
    End Sub

    Public Function FormatIsAvailable(ByVal a_format As String) As Boolean
        Return mArchetypeEngine.AvailableFormats.Contains(a_format)
    End Function

    Public Sub SerialiseArchetype(ByVal a_format As String)
        mArchetypeEngine.Serialise(a_format)
    End Sub

    Public Function CreateXMLParser() As XMLParser.XmlArchetypeParser
        'Create a new parser
        Dim xml_parser As New XMLParser.XmlArchetypeParser()

        xml_parser.NewArchetype(Archetype.Archetype_ID.ToString, mOntologyManager.PrimaryLanguageCode, Main.Instance.DefaultLanguageCodeSet)
        xml_parser.Archetype.concept = Archetype.ConceptCode
        xml_parser.Archetype.definition.node_id = xml_parser.Archetype.concept

        Dim xmlOntology As ArchetypeEditor.XML_Classes.XML_Ontology = New ArchetypeEditor.XML_Classes.XML_Ontology(xml_parser)

        'Set the root id which can be different than the concept ID
        Dim definition As ArcheTypeDefinitionBasic = Archetype.Definition

        If Not definition Is Nothing AndAlso Not definition.RootNodeId Is Nothing AndAlso xml_parser.Archetype.definition.node_id <> definition.RootNodeId Then
            xml_parser.Archetype.definition.node_id = definition.RootNodeId
        End If

        If mOntologyManager.NumberOfSpecialisations > 0 Then
            If xml_parser.Archetype.parent_archetype_id Is Nothing Then
                xml_parser.Archetype.parent_archetype_id = New XMLParser.ARCHETYPE_ID
            End If

            xml_parser.Archetype.parent_archetype_id.value = Archetype.ParentArchetype
        End If

        xml_parser.Archetype.adl_version = "1.4"

        'remove the concept code from ontology as will be set again
        xml_parser.Archetype.ontology.term_definitions = Nothing

        'term definitions
        xmlOntology.AddTermDefinitionsFromTable(mOntologyManager.TermDefinitionTable)

        'constraint definitions
        xmlOntology.AddConstraintDefinitionsFromTable(mOntologyManager.ConstraintDefinitionTable)

        'bindings
        xmlOntology.AddTermBindingsFromTable(mOntologyManager.TermBindingsTable)
        xmlOntology.AddConstraintBindingsFromTable(mOntologyManager.ConstraintBindingsTable)

        'languages - need translations and details for each language
        Dim ii As Integer = mOntologyManager.LanguagesTable.Rows.Count
        Dim translationsArray As XMLParser.TRANSLATION_DETAILS() = Array.CreateInstance(GetType(XMLParser.TRANSLATION_DETAILS), ii - 1)
        Dim details_array As XMLParser.RESOURCE_DESCRIPTION_ITEM() = Array.CreateInstance(GetType(XMLParser.RESOURCE_DESCRIPTION_ITEM), ii)

        Dim i As Integer = 0
        ii = 0

        For Each row As DataRow In mOntologyManager.LanguagesTable.Rows
            Dim language As String = CStr(row(0))
            Dim cp As New XMLParser.CODE_PHRASE
            cp.terminology_id = New XMLParser.TERMINOLOGY_ID
            cp.terminology_id.value = Main.Instance.DefaultLanguageCodeSet
            cp.code_string = language

            'Add the translations
            If language <> mOntologyManager.PrimaryLanguageCode And Archetype.TranslationDetails.ContainsKey(language) Then
                Dim translationDetail As TranslationDetails = Archetype.TranslationDetails.Item(language)
                Dim xmlTranslationDetail As New XML_TranslationDetails(translationDetail)
                translationsArray(i) = xmlTranslationDetail.XmlTranslation
                i += 1
            End If

            'Add the archetype details in each language
            Dim archDetail As ArchetypeDescriptionItem = Archetype.Description.Details.DetailInLanguage(language)
            Dim xml_detail As New XMLParser.RESOURCE_DESCRIPTION_ITEM
            xml_detail.language = cp

            If Not String.IsNullOrEmpty(archDetail.Copyright) Then
                xml_detail.copyright = archDetail.Copyright
            End If

            xml_detail.misuse = archDetail.MisUse

            If archDetail.OriginalResourceURI <> "" Then
                Dim new_items(0) As XMLParser.StringDictionaryItem
                new_items(0).Value = archDetail.OriginalResourceURI
                xml_detail.original_resource_uri = new_items
            End If

            xml_detail.purpose = archDetail.Purpose
            xml_detail.use = archDetail.Use

            If Not archDetail.KeyWords Is Nothing AndAlso archDetail.KeyWords.Count > 0 Then
                xml_detail.keywords = Array.CreateInstance(GetType(String), archDetail.KeyWords.Count)

                For j As Integer = 0 To archDetail.KeyWords.Count - 1
                    xml_detail.keywords(j) = archDetail.KeyWords.Item(j)
                Next
            End If

            details_array(ii) = xml_detail
            ii += 1
        Next

        'Definition
        Dim xmlArchetype As New ArchetypeEditor.XML_Classes.XML_Archetype(xml_parser)
        xmlArchetype.Definition = Archetype.Definition
        xmlArchetype.Description = Archetype.Description
        xmlArchetype.MakeParseTree()
        ParserSynchronised = True

        'description
        If TypeOf Archetype.Description Is ArchetypeEditor.XML_Classes.XML_Description Then
            xml_parser.Archetype.description = CType(Archetype.Description, ArchetypeEditor.XML_Classes.XML_Description).XML_Description
        Else
            xml_parser.Archetype.description = New ArchetypeEditor.XML_Classes.XML_Description(Archetype.Description).XML_Description
        End If

        xml_parser.Archetype.description.details = details_array

        'translations
        xml_parser.Archetype.translations = translationsArray
        Return xml_parser
    End Function

    Private Function CreateAdlParser() As ArchetypeEditor.ADL_Classes.ADL_Interface
        'Create a new parser
        Dim adlParser As New ArchetypeEditor.ADL_Classes.ADL_Interface()
        adlParser.NewArchetype(Archetype.Archetype_ID, mOntologyManager.PrimaryLanguageCode)
        adlParser.Archetype.ConceptCode = Archetype.ConceptCode

        If mOntologyManager.NumberOfSpecialisations > 0 Then
            adlParser.Archetype.ParentArchetype = Archetype.ParentArchetype
        End If

        adlParser.ADL_Parser.archetype.set_adl_version(Eiffel.String("1.4"))

        'HKF: 8 Dec 2008
        'description
        adlParser.Archetype.Description = New ArchetypeEditor.ADL_Classes.ADL_Description(Me.Archetype.Description, mOntologyManager.PrimaryLanguageCode)

        'populate the ontology

        'languages - need translations and details for each language
        'First deal with the original language
        For Each dRow As DataRow In mOntologyManager.LanguagesTable.Rows
            Dim language As String = CStr(dRow(0))
            If language <> mOntologyManager.PrimaryLanguageCode Then
                Dim adlTranslationDetails As ADL_TranslationDetails = New ADL_TranslationDetails(Me.Archetype.TranslationDetails.Item(language))
                adlParser.ADL_Parser.ontology.add_language(Eiffel.String(language))
                'HKF: 8 Dec 2008
                adlParser.Archetype.TranslationDetails.Add(language, adlTranslationDetails)
            End If

            ' HKF: 8 Dec 2008
            Dim archDetail As ArchetypeDescriptionItem = Me.Archetype.Description.Details.DetailInLanguage(language)
            adlParser.Archetype.Description.Details.AddOrReplace(language, archDetail)
        Next
        'term definitions
        adlParser.AddTermDefinitionsFromTable(mOntologyManager.TermDefinitionTable, mOntologyManager.PrimaryLanguageCode)

        'constraint definitions
        adlParser.AddConstraintDefinitionsFromTable(mOntologyManager.ConstraintDefinitionTable, mOntologyManager.PrimaryLanguageCode)

        'bindings
        adlParser.AddTermBindingsFromTable(mOntologyManager.TermBindingsTable)
        adlParser.AddConstraintBindingsFromTable(mOntologyManager.ConstraintBindingsTable)

        'Build the Definition
        Dim adl_archetype As ArchetypeEditor.ADL_Classes.ADL_Archetype = adlParser.Archetype
        adl_archetype.Definition = Me.Archetype.Definition
        adl_archetype.MakeParseTree()
        Me.ParserSynchronised = True

        Return adlParser
    End Function

    Public Function ExportSerialised(ByVal a_format As String) As String
        mObjectToSave.PrepareToSave()

        Select Case a_format.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "xml"
                Dim xml_parser As XMLParser.XmlArchetypeParser = CreateXMLParser()
                Return xml_parser.Serialise()
            Case "adl"
                Dim adl_parser As ArchetypeEditor.ADL_Classes.ADL_Interface = CreateAdlParser()
                Return adl_parser.Archetype.SerialisedArchetype(a_format)
            Case Else
                Debug.Assert(False, "Format not handled")
                Return "Format not available"
        End Select
    End Function

    Public Sub Export(ByVal format As String)
        'Use another parser to save file

        mObjectToSave.PrepareToSave()
        Dim ext As String = format.ToLower(Globalization.CultureInfo.InvariantCulture)
        Dim filter As String = format.ToUpper(Globalization.CultureInfo.InvariantCulture) & "|" & "*." & ext

        Select Case ext
            Case "xml"
                Dim parser As XMLParser.XmlArchetypeParser = CreateXMLParser()
                Dim s As String = FileNameChosenByUser(ext, filter)

                If s <> "" Then
                    parser.WriteFile(s)

                    If parser.Status <> "" Then
                        MessageBox.Show(parser.Status, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

            Case "adl"
                Dim parser As ArchetypeEditor.ADL_Classes.ADL_Interface = CreateAdlParser()
                Dim s As String = FileNameChosenByUser(ext, filter)

                If s <> "" Then
                    parser.WriteAdlDirect(s)
                End If

            Case Else
                MessageBox.Show(AE_Constants.Instance.Feature_not_available, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
    End Sub

    Public Sub ExportCanonicalArchetypeModel()
        mObjectToSave.PrepareToSave()
        Dim ext As String = "cam"
        'Dim filter As String = ext & "|" & "*." & ext
        Dim filter As String = String.Format("Canonical Archetype Model (*.{0})|*.{0}|All files (*.*)|*.*", ext)

        'Dim filename As String = FileNameChosenByUser(ext, filter)

        Dim dlg As New SaveFileDialog
        dlg.Filter = filter
        dlg.FileName = Archetype.Archetype_ID.ToString & "." & ext
        dlg.DefaultExt = ext
        'dlg.AddExtension = True
        dlg.Title = AE_Constants.Instance.MessageBoxCaption
        dlg.ValidateNames = True
        dlg.OverwritePrompt = True

        If dlg.ShowDialog() <> DialogResult.Cancel Then
            Dim canonicalArchetype As XMLParser.ARCHETYPE = mArchetypeEngine.GetCanonicalArchetype()

            Dim settings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
            settings.Encoding = System.Text.Encoding.UTF8
            settings.OmitXmlDeclaration = True
            settings.Indent = False

            Dim xmlWriter As System.Xml.XmlWriter
            xmlWriter = System.Xml.XmlWriter.Create(dlg.FileName, settings)
            Try
                XMLParser.OpenEhr.V1.Its.Xml.AM.AmSerializer.Serialize(xmlWriter, canonicalArchetype)
            Finally
                xmlWriter.Close()
            End Try
        End If
    End Sub

    Public Sub ParserReset(Optional ByVal an_archetype_ID As ArchetypeID = Nothing)
        If mArchetypeEngine Is Nothing Then
            mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
        Else
            If Not an_archetype_ID Is Nothing Then
                NewArchetype(an_archetype_ID, Main.Instance.Options.DefaultParser)
            Else
                mArchetypeEngine.ResetAll()
                ' need to reload the current archetype if there is one
                If Not mPriorFileName = Nothing Then
                    mArchetypeEngine.OpenFile(mPriorFileName, Me)
                    mPriorFileName = Nothing
                End If
            End If
        End If
    End Sub

    Public Sub AutoSave(ByVal n As Integer)
        mObjectToSave.PrepareToSave()
        AutoWrite("Recovery-" & Archetype.Archetype_ID.ToString)
    End Sub

    Public Function SaveArchetype() As Boolean
        Dim name As String

        If IsNew OrElse Not IO.File.Exists(FileName) Then
            name = FileNameChosenByUser(ParserType, AvailableFormatFilter)
        Else
            name = FileName
        End If

        If name <> "" Then
            mObjectToSave.PrepareToSave()
            FileName = name
            CheckFileNameAgainstArchetypeId()

            If Not ArchetypeID.IsValidId(IO.Path.GetFileNameWithoutExtension(FileName)) Then
                Dim newName As String = Archetype.Archetype_ID.ToString & "." & ParserType
                Dim text As String = String.Format(mOntologyManager.GetOpenEHRTerm(686, "Invalid archetype file name {0}. Save as {1}?"), IO.Path.GetFileName(FileName), newName)

                If MessageBox.Show(text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    FileName = IO.Path.Combine(IO.Path.GetDirectoryName(FileName), newName)
                    FileEdited = True
                End If
            End If

            If ArchetypeID.IsValidId(IO.Path.GetFileNameWithoutExtension(FileName)) Then
                SaveArchetypeAs(FileName)

                If Not FileEdited Then
                    Dim parser As Parser = mArchetypeEngine
                    Dim ontology As Ontology = mOntologyManager.Ontology
                    Dim ext As String = IO.Path.GetExtension(FileName.ToLowerInvariant())

                    If ext = ".adl" Then
                        If Main.Instance.Options.XmlRepositoryAutoSave Then
                            FileEdited = True
                            name = IO.Path.GetFullPath(IO.Path.ChangeExtension(FileName, ".xml"))

                            If name.StartsWith(Main.Instance.Options.RepositoryPath + IO.Path.DirectorySeparatorChar) Then
                                name = name.Replace(Main.Instance.Options.RepositoryPath, Main.Instance.Options.XmlRepositoryPath)
                                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(name))
                            End If

                            SaveArchetypeAs(name)
                        End If
                    ElseIf ext = ".xml" Then
                        If Main.Instance.Options.RepositoryAutoSave Then
                            FileEdited = True
                            name = IO.Path.GetFullPath(IO.Path.ChangeExtension(FileName, ".adl"))

                            If name.StartsWith(Main.Instance.Options.XmlRepositoryPath + IO.Path.DirectorySeparatorChar) Then
                                name = name.Replace(Main.Instance.Options.XmlRepositoryPath, Main.Instance.Options.RepositoryPath)
                                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(name))
                            End If

                            SaveArchetypeAs(name)
                        End If
                    End If

                    mArchetypeEngine = parser
                    mOntologyManager.ReplaceOntology(ontology)
                End If
            End If
        End If

        Return Not FileEdited
    End Function

    Private Sub SaveArchetypeAs(ByRef name As String)
        If name <> "" Then
            If IO.File.Exists(name) AndAlso (IO.File.GetAttributes(name) And IO.FileAttributes.ReadOnly) > 0 Then
                MessageBox.Show(name & ": " & Filemanager.GetOpenEhrTerm(439, "Read only"), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim ext As String = System.IO.Path.GetExtension(name.ToLowerInvariant())

                If ext <> "." & ParserType.ToLowerInvariant() Then
                    If ext = ".adl" Then
                        Dim parser As ArchetypeEditor.ADL_Classes.ADL_Interface = CreateAdlParser()
                        mArchetypeEngine = parser
                        mOntologyManager.ReplaceOntology(New ArchetypeEditor.ADL_Classes.ADL_Ontology(parser.ADL_Parser))
                    ElseIf ext = ".xml" Then
                        Dim parser As XMLParser.XmlArchetypeParser = CreateXMLParser()
                        mArchetypeEngine = New ArchetypeEditor.XML_Classes.XML_Interface(parser)
                        Dim ontology As New ArchetypeEditor.XML_Classes.XML_Ontology(parser)
                        ontology.SetLanguage(OntologyManager.LanguageCode)
                        mOntologyManager.ReplaceOntology(ontology)
                    End If
                End If

                Try
                    WriteArchetype(name)

                    If Not mHasWriteFileError Then
                        FileEdited = False
                    End If
                Catch ex As Exception
                    MessageBox.Show(AE_Constants.Instance.Error_saving & FileName & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Function FileNameChosenByUser(ByVal ext As String, ByVal filter As String) As String
        Dim result As String = ""
        Dim dlg As New SaveFileDialog
        dlg.Filter = filter
        dlg.FileName = Archetype.Archetype_ID.ToString & "." & ext
        dlg.DefaultExt = ext
        Dim i As Integer = IndexOfFormat(ext) + 1

        If i > 0 Then
            dlg.FilterIndex = i
        End If

        dlg.AddExtension = True
        dlg.Title = AE_Constants.Instance.MessageBoxCaption
        dlg.ValidateNames = True
        dlg.OverwritePrompt = False
        AddHandler dlg.FileOk, AddressOf ValidateFileNameChosenByUser

        If dlg.ShowDialog() <> DialogResult.Cancel Then
            result = dlg.FileName
        End If

        Return result
    End Function

    Private Sub ValidateFileNameChosenByUser(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim dlg As SaveFileDialog = CType(sender, SaveFileDialog)
        Dim ext As String = dlg.Filter.Split("|"c)((dlg.FilterIndex - 1) * 2).ToLower(System.Globalization.CultureInfo.InvariantCulture)
        dlg.FileName = IO.Path.ChangeExtension(dlg.FileName, ext)

        If IO.File.Exists(dlg.FileName) Then
            Dim question As String = String.Format(AE_Constants.Instance.ReplaceExistingFileQuestion, dlg.FileName)
            e.Cancel = MessageBox.Show(question, dlg.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No
        End If
    End Sub

    Private Sub AutoWrite(ByVal fileName As String)
        Dim appata As String = Main.Instance.Options.ApplicationDataDirectory
        mArchetypeEngine.WriteFile(IO.Path.Combine(appata, fileName & "." & ParserType), ParserType, ParserSynchronised)
    End Sub

    Protected Sub WriteArchetype(ByVal fileName As String)
        'Check that the file name is an available format
        Dim s As String = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLowerInvariant()

        If FormatIsAvailable(s) Then
            mHasWriteFileError = False
            mArchetypeEngine.WriteFile(fileName, s, ParserSynchronised)

            If mArchetypeEngine.WriteFileError Then
                mHasWriteFileError = True
            End If
        Else
            MessageBox.Show(AE_Constants.Instance.Incorrect_format & "File: '" & fileName & ", Format: '" & s & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    'Public Sub ConvertToADL(ByVal anOntologyManager As OntologyManager)
    '    Debug.Assert(Not anOntologyManager Is Nothing, "Ontology Manager is not set")

    '    Debug.Assert(Not mArchetypeEngine Is Nothing, "Archetype Engine is not set")
    '    Debug.Assert(TypeOf mArchetypeEngine Is TextParser, "Archetype Engine is not a Text Parser")
    '    Dim old_archetype_engine As TextParser = CType(mArchetypeEngine, TextParser)

    '    mArchetypeEngine = New ADL_Interface
    '    mArchetypeEngine.NewArchetype(old_archetype_engine.Archetype.ArchetypeID, _
    '            anOntologyManager.LanguageCode)
    '    Archetype.ConceptCode = old_archetype_engine.Archetype.ConceptCode
    '    Archetype.LifeCycle = old_archetype_engine.Archetype.LifeCycle

    '    If Not old_archetype_engine.Archetype.ParentArchetype Is Nothing Then
    '        Archetype.ParentArchetype _
    '                = old_archetype_engine.Archetype.ParentArchetype
    '    End If

    '    'anOntologyManager.ConvertToADL(New ADL_Ontology(CType(mArchetypeEngine, ADL_Interface).EIF_adlInterface, False))
    'End Sub

    Public Sub NewArchetype(ByVal archetypeID As ArchetypeID, ByVal FileType As String)
        If ParserType <> Main.Instance.Options.DefaultParser Then
            Select Case Main.Instance.Options.DefaultParser
                Case "adl"
                    mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
                Case "xml"
                    mArchetypeEngine = New ArchetypeEditor.XML_Classes.XML_Interface
            End Select
        End If

        mArchetypeEngine.NewArchetype(archetypeID, Main.Instance.DefaultLanguageCode)

        If ArchetypeAvailable Then
            'Apply a new ontology - this empties the GUI - use ReplaceOntology to preserve
            Select Case FileType.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "adl"
                    OntologyManager.Ontology = New ArchetypeEditor.ADL_Classes.ADL_Ontology(CType(mArchetypeEngine, ArchetypeEditor.ADL_Classes.ADL_Interface).ADL_Parser)
                Case "xml"
                    OntologyManager.Ontology = New ArchetypeEditor.XML_Classes.XML_Ontology(CType(mArchetypeEngine, ArchetypeEditor.XML_Classes.XML_Interface).Xml_Parser)
                    OntologyManager.Ontology.SetLanguage(Main.Instance.DefaultLanguageCode)
                Case Else
                    Debug.Assert(False, "Type is not handled: " & FileType)
            End Select

            ' a new archetype always has a concept code set to "at0000"
            Dim term As New RmTerm(Archetype.ConceptCode)
            term.Text = "unknown"
            term.Description = "unknown"
            OntologyManager.UpdateTerm(term)
        End If
    End Sub

    Sub New()
        Select Case Main.Instance.Options.DefaultParser
            Case "adl"
                mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
            Case "xml"
                mArchetypeEngine = New ArchetypeEditor.XML_Classes.XML_Interface
        End Select
    End Sub
End Class

Public Delegate Sub FileManagerEventHandler(ByVal e As FileManagerEventArgs)

Public Class FileManagerEventArgs
    Private mIsFileDirty As Boolean
    Public ReadOnly Property IsFileDirty() As Boolean
        Get
            Return mIsFileDirty
        End Get
    End Property

    Public Sub New(ByVal IsFileDirty As Boolean)
        mIsFileDirty = IsFileDirty
    End Sub
End Class

Class Filemanager
    'Inherits FileManagerLocal
    ' Allows Designer wide access to FileManager, while enabling local access if required

    ' FileManager Singleton
    'Private Shared mInstance As FileManagerLocal
    Private Shared mFileManagerCollection As New ArrayList
    Shared Event IsFileDirtyChanged As FileManagerEventHandler
    Private Shared mHasEmbedded As Boolean = False

    Shared Sub SetFileChangedToolBar(ByVal isChanged As Boolean)
        ' used by embedded archetypes to set the GUI to filechanged
        RaiseEvent IsFileDirtyChanged(New FileManagerEventArgs(isChanged))
    End Sub
    Private Sub OnIsFileDirtyChanged(ByVal IsFileDirty As Boolean)
        RaiseEvent IsFileDirtyChanged(New FileManagerEventArgs(IsFileDirty))
    End Sub

    Public Shared Property Master() As FileManagerLocal
        Get
            If mFileManagerCollection.Count > 0 Then
                Return CType(mFileManagerCollection(0), FileManagerLocal)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As FileManagerLocal)
            ' setting the master must require clearing embedded if there are any
            mFileManagerCollection.Insert(0, Value)
        End Set
    End Property
    Public Shared ReadOnly Property HasFileToSave() As Boolean
        Get
            For Each f As FileManagerLocal In mFileManagerCollection
                If f.FileEdited Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property
    Public Shared ReadOnly Property HasEmbedded() As Boolean
        Get
            Return mHasEmbedded
        End Get
    End Property

    Public Shared Sub AddEmbedded(ByVal f As FileManagerLocal)
        mFileManagerCollection.Add(f)
        mHasEmbedded = True
    End Sub

    Public Shared Sub ClearEmbedded()
        While mFileManagerCollection.Count > 1
            mFileManagerCollection.RemoveAt(1)
        End While
    End Sub

    Public Shared Sub RemoveEmbedded(ByVal f As FileManagerLocal)
        If mHasEmbedded Then
            mFileManagerCollection.Remove(f)
            If mFileManagerCollection.Count = 1 Then
                mHasEmbedded = False
            End If
        End If
    End Sub

    Public Shared Function GetOpenEhrTerm(ByVal code As Integer, ByVal default_text As String, Optional ByVal language As String = "") As String
        If language = "" Then
            Return Master.OntologyManager.GetOpenEHRTerm(code, default_text)
        Else
            Return Master.OntologyManager.GetOpenEHRTerm(code, default_text, language)
        End If
    End Function

    Public Shared Function SaveFiles(ByVal askToSave As Boolean) As Boolean
        For Each f As FileManagerLocal In mFileManagerCollection
            'SRH: Jan 2009 EDT-495,276 - force a regeneration as this means the archetype is always up to date when saved
            'If f.FileEdited Then 
            If mFileManagerCollection.Count > 1 Or askToSave Then
                Select Case MessageBox.Show(AE_Constants.Instance.Save_changes & " '" & f.Archetype.Archetype_ID.ToString & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case Windows.Forms.DialogResult.Cancel
                        Return False
                    Case Windows.Forms.DialogResult.No
                        Return True
                    Case Windows.Forms.DialogResult.Yes
                        If f.SaveArchetype() Then
                            f.IsNew = False
                        Else
                            Return False
                        End If
                End Select
            Else
                If f.SaveArchetype() Then
                    f.IsNew = False
                Else
                    Return False
                End If
            End If
            'End If
        Next
        Return True
    End Function

    Public Shared Sub AutoFileSave()
        Dim i As Integer
        For Each f As FileManagerLocal In mFileManagerCollection
            If f.FileEdited Then
                i = i + 1
                f.AutoSave(i)
            End If
        Next
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
'The Original Code is FileManager.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'   Jana Graenz
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

