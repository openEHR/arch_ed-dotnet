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
'	file:        "$Source: source/vb.net/archetype_editor/BusinessLogic/SCCS/s.FileManager.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'
'Option Strict On
Option Explicit On 
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel

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
    Private mTermBindingLookUpTables As Collections.Generic.SortedDictionary(Of String, DataTable)

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
                format_filter &= s & "|*." & s & "|"
            Next
            Select Case ParserType
                Case "adl"
                    format_filter &= "xml|*.xml"
                Case "xml"
                    format_filter &= "adl|*.adl"
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

    Public Function HasTermBindings(ByVal language As String, ByVal terminologyId As String) As Boolean

        If webError Then Return False

        If Not mTermBindingLookUpTables Is Nothing Then
            If mTermBindingLookUpTables.ContainsKey(language & terminologyId) Then
                Return True
            End If
        Else
            mTermBindingLookUpTables = New Collections.Generic.SortedDictionary(Of String, DataTable)
        End If
        Try
            If Not mOntologyManager.TermBindingsTable Is Nothing AndAlso mOntologyManager.TermBindingsTable.Rows.Count > 0 Then
                Dim selected_rows As DataRow() = mOntologyManager.TermBindingsTable.Select("Terminology = 'SNOMED-CT'")
                If Not selected_rows Is Nothing AndAlso selected_rows.Length > 0 Then
                    Dim conceptIds(selected_rows.Length) As String
                    For i As Integer = 0 To selected_rows.Length - 1
                        conceptIds(i) = selected_rows(i).Item("Code")
                    Next
                    Dim termTable As DataTable = OTSControls.Term.OtsWebService.GetTerminologyPreferredTerms(OTSControls.OTSServer.TerminologyName.Snomed, language, conceptIds).Tables(0)
                    mTermBindingLookUpTables.Add(language & terminologyId, termTable)
                End If
            End If
        Catch
            webError = True
        End Try

        Return False
    End Function

    Public Function BindingText(ByVal language As String, ByVal TerminologyId As String, ByVal terminologyCode As String) As String
        Dim dTable As DataTable = mTermBindingLookUpTables.Item(language & TerminologyId)
        If Not dTable Is Nothing Then
            Dim dRow As DataRow = dTable.Rows.Find(terminologyCode).Item(1)
            If Not dRow Is Nothing Then
                Return dRow(1)
            End If
        End If
        Return String.Empty
    End Function

    Public Function OpenArchetype(ByVal aFileName As String) As Boolean
        Try
            mPriorFileName = Me.FileName

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


            Me.FileName = aFileName

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
                Debug.Assert(False)
                Return False
            End If

            mHasOpenFileError = False

            ' next section: written by Jana Graenz, necessary for opening a "web archetype" (where path is a URL)
            ' if this archtype comes from the web, aFileName will be the URL.
            ' In this case we have to download the file temporarily on the users system so that it can be opened in the Editor.
            ' it will be downloaded in the temporary system folder and deleted immediatly after it has been opened in the Editor.
            ' This avoids data and file overflow.

            
            'end of addition

            mArchetypeEngine.OpenFile(aFileName, Me)

            ' next section: written by Jana Graenz 2007-02-28

            If aFileName.StartsWith(System.IO.Path.GetTempPath) Then
                'delete the temporarily downloaded adl-file after opening it.
                'user has to safe the file locally if he/she wants to keep it
                Kill(aFileName)
            End If

            'end of addition

            If mArchetypeEngine.OpenFileError Then
                mHasOpenFileError = True
                Return False
            End If

            'JAR: 23MAY2007, EDT-16 Validate Archetype Id against file name
            'Else
            '' ensure the filename and archetype ID are in tune
            'Dim i As Integer = aFileName.LastIndexOf("\")
            'Dim shortFileName As String = aFileName.Substring(i + 1)

            'If Not shortFileName.StartsWith(mArchetypeEngine.Archetype.Archetype_ID.ToString & ".") Then
            '    If MessageBox.Show(mOntologyManager.GetOpenEHRTerm(57, "Archetype file name") & _
            '       ": " & shortFileName & "; " & Environment.NewLine & _
            '       mOntologyManager.GetOpenEHRTerm(632, "Archetype Id") & _
            '       ": " & mArchetypeEngine.Archetype.Archetype_ID.ToString & "." & Environment.NewLine & _
            '       mOntologyManager.GetOpenEHRTerm(147, "Change") & _
            '       " " & mOntologyManager.GetOpenEHRTerm(57, "Archetype file name"), _
            '       AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '        Me.FileName = aFileName.Substring(i + 1) + mArchetypeEngine.Archetype.Archetype_ID.ToString & "." & ParserType
            '    End If
            'End If

            mOntologyManager.PopulateAllTerms() 'Note: call switches on FileEdited!
            mPriorFileName = Nothing
            FileEdited = False

            'ensure the filename and archetype ID match (ignore case!)        
            Dim shortFileName As String = aFileName.Substring((aFileName.LastIndexOf("\")) + 1)

            If Not shortFileName.StartsWith(mArchetypeEngine.Archetype.Archetype_ID.ToString & ".", StringComparison.InvariantCultureIgnoreCase) Then
                If ArchetypeID.IsValidId(shortFileName.Substring(0, shortFileName.LastIndexOf("."))) Then
                    If Not CheckFileName(shortFileName) Then 'returns false if an update occurred
                        FileLoading = False
                        FileEdited = True
                        FileLoading = True
                    End If
                Else
                    FileName = mArchetypeEngine.Archetype.Archetype_ID.ToString & "." & ParserType
                    FileLoading = False
                    FileEdited = True
                    FileLoading = True
                End If
            End If

            Return True
        Catch e As Exception
            Me.FileName = mPriorFileName
            mPriorFileName = Nothing
        End Try
    End Function

    'JAR: 23MAY2007, EDT-16 Validate Archetype Id against file name
    Private Function CheckFileName(ByVal FileName As String) As Boolean 'returns false if an update occurred

        Dim updateOccurred As Boolean = False
        Dim shortFileName As String = Left(FileName, FileName.LastIndexOf("."))

        'validate the concept to update it with the correct case and remove illegal characters
        Dim Id1 As New ArchetypeID(Archetype.Archetype_ID.ToString)
        Id1.Concept = Id1.ValidConcept(Id1.Concept, "", False)

        Dim Id2 As New ArchetypeID(shortFileName)
        Id2.Concept = Id2.ValidConcept(Id2.Concept, "", False)

        Dim frm As New ChooseFix(mOntologyManager, Id1.ToString, Id2.ToString)
        If frm.ShowDialog <> Windows.Forms.DialogResult.Cancel And frm.selection <> ChooseFix.FixOption.Ignore Then 'selection made

            'NOTE: The following updates can occur!
            '    Update 1: Update if concept was changed in ValidConcept call
            '    Update 2: Update according to the user selection

            updateOccurred = True

            Dim Use As String = IIf(frm.selection = ChooseFix.FixOption.UseId, Id1.ToString, Id2.ToString)

            'update filename if changed
            If String.Compare(shortFileName, Use, True) > 0 Then 'case insensitive (windows o/s has issues updating file name case!)
                mPriorFileName = Me.FileName
                Me.FileName = Replace(Me.FileName, FileName, Use & "." & ParserType)
                updateOccurred = True
            End If

            'update archetype id if changed
            If Archetype.Archetype_ID.ToString <> Use Then 'case sensitive
                Archetype.Archetype_ID.SetFromString(Use)
                Archetype.UpdateArchetypeId() 'force details set above to be updated in the Eiffel parser                
            End If
        End If

        frm.Close()
        Return Not updateOccurred
    End Function

    Public Function FormatIsAvailable(ByVal a_format As String) As Boolean

        If mArchetypeEngine.AvailableFormats.Contains(a_format) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub SerialiseArchetype(ByVal a_format As String)
        mArchetypeEngine.Serialise(a_format)
    End Sub

    Public Function CreateXMLParser() As XMLParser.XmlArchetypeParser
        'Create a new parser
        Dim xml_parser As New XMLParser.XmlArchetypeParser()

        xml_parser.NewArchetype( _
                   mArchetypeEngine.Archetype.Archetype_ID.ToString, _
                   mOntologyManager.PrimaryLanguageCode, OceanArchetypeEditor.DefaultLanguageCodeSet)
        xml_parser.Archetype.concept = mArchetypeEngine.Archetype.ConceptCode
        xml_parser.Archetype.definition.node_id = xml_parser.Archetype.concept 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

        Dim xmlOntology As ArchetypeEditor.XML_Classes.XML_Ontology = New ArchetypeEditor.XML_Classes.XML_Ontology(xml_parser)

        'Set the root id which can be different than the concept ID
        Dim definition As ArcheTypeDefinitionBasic = mArchetypeEngine.Archetype.Definition

        If Not definition Is Nothing AndAlso Not definition.RootNodeId Is Nothing AndAlso xml_parser.Archetype.definition.node_id <> definition.RootNodeId Then
            xml_parser.Archetype.definition.node_id = definition.RootNodeId
        End If

        If mOntologyManager.NumberOfSpecialisations > 0 Then
            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'xml_parser.Archetype.parent_archetype_id = mArchetypeEngine.Archetype.ParentArchetype 
            If xml_parser.Archetype.parent_archetype_id Is Nothing Then
                xml_parser.Archetype.parent_archetype_id = New XMLParser.ARCHETYPE_ID
            End If

            xml_parser.Archetype.parent_archetype_id.value = mArchetypeEngine.Archetype.ParentArchetype
        End If

        xml_parser.Archetype.adl_version = "1.4"

        'remove the concept code from ontology as will be set again
        xml_parser.Archetype.ontology.term_definitions = Nothing 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1

        'populate the ontology
        'xml_parser.Archetype.ontology.specialisation_depth = mOntologyManager.NumberOfSpecialisations.ToString 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1        

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
            cp.terminology_id.value = OceanArchetypeEditor.DefaultLanguageCodeSet
            'cp.terminology_id = OceanArchetypeEditor.DefaultLanguageCodeSet
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

            If archDetail.Copyright <> "" Then
                xml_detail.copyright = archDetail.Copyright
            End If

            xml_detail.misuse = archDetail.MisUse

            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'xml_detail.original_resource_uri = archDetail.OriginalResourceURI
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
        xmlArchetype.Description = Archetype.Description 'EDT33: View archetype as XML raises exception https://projects.oceanehr.com/jira/browse/EDT-33
        xmlArchetype.MakeParseTree()
        ParserSynchronised = True

        'description
        Dim xml_description As New ArchetypeEditor.XML_Classes.XML_Description(Archetype.Description)
        xml_parser.Archetype.description = xml_description.XML_Description
        xml_parser.Archetype.description.details = details_array

        'translations
        xml_parser.Archetype.translations = translationsArray
        Return xml_parser
    End Function

    Private Function CreateAdlParser() As ArchetypeEditor.ADL_Classes.ADL_Interface
        'Create a new parser
        Dim adlParser As New ArchetypeEditor.ADL_Classes.ADL_Interface()
        adlParser.NewArchetype( _
            mArchetypeEngine.Archetype.Archetype_ID, _
            mOntologyManager.PrimaryLanguageCode)
        adlParser.Archetype.ConceptCode = mArchetypeEngine.Archetype.ConceptCode
        'adlParser.Archetype.Definition.RootNodeId = mArchetypeEngine.Archetype.Definition.RootNodeId 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1        

        If mOntologyManager.NumberOfSpecialisations > 0 Then
            adlParser.Archetype.ParentArchetype = mArchetypeEngine.Archetype.ParentArchetype
        End If

        adlParser.ADL_Parser.archetype.set_adl_version(EiffelKernel.Create.STRING_8.make_from_cil("1.4"))

        'populate the ontology

        'languages - need translations and details for each language
        Dim translationsArray As New ArrayList
        Dim detailsArray As New ArrayList

        'First deal with the original language
        For Each dRow As DataRow In mOntologyManager.LanguagesTable.Rows
            Dim language As String = CStr(dRow(0))
            If language <> mOntologyManager.PrimaryLanguageCode Then
                Dim adlTranslationDetails As ADL_TranslationDetails = New ADL_TranslationDetails(Me.Archetype.TranslationDetails.Item(language))
                translationsArray.Add(adlTranslationDetails.ADL_Translation)
                adlParser.ADL_Parser.ontology.add_language(EiffelKernel.Create.STRING_8.make_from_cil(language))
            End If

            Dim cp As openehr.openehr.rm.data_types.text.Impl.CODE_PHRASE
            cp = openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string( _
                EiffelKernel.Create.STRING_8.make_from_cil(OceanArchetypeEditor.DefaultLanguageCodeSet & "::" & language))
            Dim archDetail As ArchetypeDescriptionItem = Me.Archetype.Description.Details.DetailInLanguage(language)

            Dim adl_detail As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM
            adl_detail = openehr.openehr.rm.common.resource.Create.RESOURCE_DESCRIPTION_ITEM.make_from_language( _
                EiffelKernel.Create.STRING_8.make_from_cil(language), _
                EiffelKernel.Create.STRING_8.make_from_cil((archDetail.Purpose)))
            If archDetail.Copyright <> "" Then
                adl_detail.set_copyright(EiffelKernel.Create.STRING_8.make_from_cil(archDetail.Copyright))
            End If
            adl_detail.set_misuse(EiffelKernel.Create.STRING_8.make_from_cil(archDetail.MisUse))
            'ToDo: adl_detail.add_original_resource_uri()
            adl_detail.set_purpose(EiffelKernel.Create.STRING_8.make_from_cil(archDetail.Purpose))
            adl_detail.set_use(EiffelKernel.Create.STRING_8.make_from_cil(archDetail.Use))
            If (Not archDetail.KeyWords Is Nothing) AndAlso archDetail.KeyWords.Count > 0 Then
                For j As Integer = 0 To archDetail.KeyWords.Count - 1
                    adl_detail.add_keyword(EiffelKernel.Create.STRING_8.make_from_cil(archDetail.KeyWords.Item(j)))
                Next
            End If
            detailsArray.Add(adl_detail)

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

        'description
        Dim adl_description As New ArchetypeEditor.ADL_Classes.ADL_Description(Me.Archetype.Description, mOntologyManager.PrimaryLanguageCode)
        adlParser.ADL_Parser.archetype.set_description(adl_description.ADL_Description)
        For Each an_adl_detail As openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION_ITEM In detailsArray
            adlParser.ADL_Parser.archetype.description.add_detail(an_adl_detail)
        Next

        'translations
        For Each an_adl_translation As openehr.openehr.rm.common.resource.TRANSLATION_DETAILS In translationsArray
            adlParser.ADL_Parser.archetype.add_translation(an_adl_translation, an_adl_translation.language.code_string)
        Next

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

    Public Sub Export(ByVal a_format As String)
        'Use another parser to save file

        mObjectToSave.PrepareToSave()

        Select Case a_format.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "xml"
                Dim xml_parser As XMLParser.XmlArchetypeParser = CreateXMLParser()
                Dim s As String = ChooseFileName(a_format.ToLower(System.Globalization.CultureInfo.InvariantCulture))

                If s <> "" Then
                    xml_parser.WriteFile(s)

                    If xml_parser.Status <> "" Then
                        MessageBox.Show(xml_parser.Status, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

                '-----------ADL--------------------------------------------

            Case "adl"
                Dim adl_parser As ArchetypeEditor.ADL_Classes.ADL_Interface = CreateAdlParser()
                Dim s As String = ChooseFileName(a_format.ToLower(System.Globalization.CultureInfo.InvariantCulture))

                If s <> "" Then
                    adl_parser.WriteAdlDirect(s)
                End If

            Case Else
                MessageBox.Show(AE_Constants.Instance.Feature_not_available, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select

    End Sub

    Public Sub ParserReset(Optional ByVal an_archetype_ID As ArchetypeID = Nothing)
        If mArchetypeEngine Is Nothing Then
            mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
        Else
            If Not an_archetype_ID Is Nothing Then
                NewArchetype(an_archetype_ID, OceanArchetypeEditor.Instance.Options.DefaultParser)
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
        AutoWrite("OceanRecovery-" & Me.Archetype.Archetype_ID.ToString)
    End Sub

    Public Function SaveArchetype() As Boolean
        Dim result As Boolean = False
        Dim name As String

        If IsNew OrElse Not IO.File.Exists(FileName) Then
            name = ChooseFileName()
        Else
            name = FileName
        End If

        If name <> "" Then
            mObjectToSave.PrepareToSave()
            Dim ext As String = System.IO.Path.GetExtension(name.ToLowerInvariant())
            result = SaveArchetypeAs(name)

            If result Then
                Dim parser As Parser = mArchetypeEngine
                Dim ontology As Ontology = mOntologyManager.Ontology

                If ext = ".adl" Then
                    If OceanArchetypeEditor.Instance.Options.XmlRepositoryAutoSave Then
                        Dim xml As String = System.IO.Path.GetFullPath(System.IO.Path.ChangeExtension(name, ".xml"))

                        If xml.StartsWith(OceanArchetypeEditor.Instance.Options.RepositoryPath + System.IO.Path.DirectorySeparatorChar) Then
                            xml = xml.Replace(OceanArchetypeEditor.Instance.Options.RepositoryPath, OceanArchetypeEditor.Instance.Options.XmlRepositoryPath)
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(xml))
                        End If

                        result = SaveArchetypeAs(xml)
                    End If
                ElseIf ext = ".xml" Then
                    If OceanArchetypeEditor.Instance.Options.RepositoryAutoSave Then
                        Dim adl As String = System.IO.Path.GetFullPath(System.IO.Path.ChangeExtension(name, ".adl"))

                        If adl.StartsWith(OceanArchetypeEditor.Instance.Options.XmlRepositoryPath + System.IO.Path.DirectorySeparatorChar) Then
                            adl = adl.Replace(OceanArchetypeEditor.Instance.Options.XmlRepositoryPath, OceanArchetypeEditor.Instance.Options.RepositoryPath)
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(adl))
                        End If

                        result = SaveArchetypeAs(adl)
                    End If
                End If

                mArchetypeEngine = parser
                mOntologyManager.ReplaceOntology(ontology)
                FileName = name
            End If
        End If

        Return result
    End Function

    Private Function SaveArchetypeAs(ByRef name As String) As Boolean
        Dim result As Boolean = False

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
                        mOntologyManager.ReplaceOntology(New ArchetypeEditor.XML_Classes.XML_Ontology(parser, True))
                    Else
                        Debug.Assert(False, "File type is not catered for: " & name)
                        name = ""
                    End If
                End If

                If name <> "" Then
                    Try
                        FileName = name
                        WriteArchetype()

                        If Not mHasWriteFileError Then
                            FileEdited = False
                            result = True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Error_saving & FileName & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        End If

        Return result
    End Function

    Private Function ChooseFileName(ByVal a_file_type As String) As String
        Dim saveFile As New SaveFileDialog

        saveFile.Filter = a_file_type.ToUpper(System.Globalization.CultureInfo.InvariantCulture) & "|" & a_file_type
        saveFile.FileName = Me.Archetype.Archetype_ID.ToString
        saveFile.OverwritePrompt = True
        saveFile.DefaultExt = a_file_type
        saveFile.AddExtension = True
        saveFile.Title = AE_Constants.Instance.MessageBoxCaption
        saveFile.ValidateNames = True
        If saveFile.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Return ""
        Else
            'Check the file extension is added
            Dim s As String
            s = saveFile.FileName.Substring(saveFile.FileName.LastIndexOf(".") + 1)
            If s = a_file_type Then
                Return saveFile.FileName
            Else
                Return saveFile.FileName & "." & a_file_type
            End If
        End If

    End Function

    Private Function ChooseFileName() As String
        Dim saveFile As New SaveFileDialog

        saveFile.Filter = Me.AvailableFormatFilter
        saveFile.FileName = Me.Archetype.Archetype_ID.ToString
        saveFile.OverwritePrompt = True
        saveFile.DefaultExt = Me.ParserType
        Dim i As Integer = Me.IndexOfFormat(Me.ParserType) + 1

        If i > 0 Then
            saveFile.FilterIndex = i
        End If

        saveFile.AddExtension = True
        saveFile.Title = AE_Constants.Instance.MessageBoxCaption
        saveFile.ValidateNames = True

        If saveFile.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Return ""
        Else
            'Check the file extension is added
            Dim s, ext As String

            ext = saveFile.Filter.Split("|".ToCharArray())((saveFile.FilterIndex - 1) * 2)
            s = saveFile.FileName.Substring(saveFile.FileName.LastIndexOf(".") + 1)

            If s = ext Then
                Return saveFile.FileName
            Else
                Return saveFile.FileName & "." & ext
            End If
        End If
    End Function

    Private Sub AutoWrite(ByVal fileName As String)
        Dim appata As String = OceanArchetypeEditor.Instance.Options.ApplicationDataDirectory
        mArchetypeEngine.WriteFile(IO.Path.Combine(appata, fileName & "." & ParserType), ParserType, ParserSynchronised)
    End Sub

    Public Sub WriteArchetype()
        'Check that the file name is an available format
        Dim s As String = mFileName.Substring(mFileName.LastIndexOf(".") + 1).ToLowerInvariant()

        If FormatIsAvailable(s) Then
            mHasWriteFileError = False
            mArchetypeEngine.WriteFile(mFileName, s, Me.ParserSynchronised)

            If mArchetypeEngine.WriteFileError Then
                mHasWriteFileError = True
            End If
        Else
            MessageBox.Show(AE_Constants.Instance.Incorrect_format & "File: '" & mFileName & ", Format: '" & s & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
    '    mArchetypeEngine.Archetype.ConceptCode = old_archetype_engine.Archetype.ConceptCode
    '    mArchetypeEngine.Archetype.LifeCycle = old_archetype_engine.Archetype.LifeCycle

    '    If Not old_archetype_engine.Archetype.ParentArchetype Is Nothing Then
    '        mArchetypeEngine.Archetype.ParentArchetype _
    '                = old_archetype_engine.Archetype.ParentArchetype
    '    End If

    '    'anOntologyManager.ConvertToADL(New ADL_Ontology(CType(mArchetypeEngine, ADL_Interface).EIF_adlInterface, False))
    'End Sub

    Public Sub NewArchetype(ByVal an_ArchetypeID As ArchetypeID, ByVal FileType As String)
        If ParserType <> OceanArchetypeEditor.Instance.Options.DefaultParser Then
            Select Case OceanArchetypeEditor.Instance.Options.DefaultParser
                Case "adl"
                    mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
                Case "xml"
                    mArchetypeEngine = New ArchetypeEditor.XML_Classes.XML_Interface
            End Select
        End If

        Select Case FileType.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "adl"
                Dim a_ontology As ArchetypeEditor.ADL_Classes.ADL_Ontology
                Dim a_term As RmTerm
                mArchetypeEngine.NewArchetype(an_ArchetypeID, OceanArchetypeEditor.DefaultLanguageCode)


                If mArchetypeEngine.ArchetypeAvailable Then

                    a_ontology = New ArchetypeEditor.ADL_Classes.ADL_Ontology(CType(mArchetypeEngine, ArchetypeEditor.ADL_Classes.ADL_Interface).ADL_Parser)

                    'Apply a new ontology - this empties the GUI - use ReplaceOntology to preserve
                    OntologyManager.Ontology = a_ontology
                    ' a new archetype always has a concept code set to "at0000"
                    a_term = New RmTerm(mArchetypeEngine.Archetype.ConceptCode)
                    a_term.Text = "?"
                    OntologyManager.UpdateTerm(a_term)
                End If
            Case "xml"
                Dim a_ontology As ArchetypeEditor.XML_Classes.XML_Ontology
                Dim a_term As RmTerm
                mArchetypeEngine.NewArchetype(an_ArchetypeID, OceanArchetypeEditor.DefaultLanguageCode)

                If mArchetypeEngine.ArchetypeAvailable Then

                    a_ontology = New ArchetypeEditor.XML_Classes.XML_Ontology(CType(mArchetypeEngine, ArchetypeEditor.XML_Classes.XML_Interface).Xml_Parser)
                    a_ontology.SetLanguage(OceanArchetypeEditor.DefaultLanguageCode)

                    'Apply a new ontology - this empties the GUI - use ReplaceOntology to preserve
                    OntologyManager.Ontology = a_ontology
                    ' a new archetype always has a concept code set to "at0000"
                    a_term = New RmTerm(mArchetypeEngine.Archetype.ConceptCode)
                    a_term.Text = "?"
                    OntologyManager.UpdateTerm(a_term)
                End If
            Case Else
                Debug.Assert(False, "Type is not handled: " & FileType)
        End Select

    End Sub

    Sub New()
        Select Case OceanArchetypeEditor.Instance.Options.DefaultParser
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
            If f.FileEdited Then
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
            End If
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

