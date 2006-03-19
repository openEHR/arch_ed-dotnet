'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$Source: source/vb.net/archetype_editor/BusinessLogic/SCCS/s.FileManager.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 

Public Class FileManagerLocal
    Private mIsFileDirty As Boolean
    Private mIsFileLoading As Boolean
    Private mHasOpenFileError, mHasWriteFileError As Boolean
    Private mArchetypeEngine As Parser
    Private mFileName, mPriorFileName As String
    Private mWorkingDirectory As String
    Private mIsNew As Boolean = False
    Private mObjectToSave As Object
    Private mOntologyManager As New OntologyManager

    Public Event ArchetypeLoaded As EventHandler

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
            If Value <> mIsFileDirty Then
                mIsFileDirty = Value
                Filemanager.SetFileChangedToolBar(Value)
            End If
        End Set
    End Property
    Public ReadOnly Property AvailableFormatFilter() As String
        Get
            ' returns a filedialog filter
            Dim format_filter As String

            For i As Integer = 0 To mArchetypeEngine.AvailableFormats.Count - 1
                Dim s As String = CStr(mArchetypeEngine.AvailableFormats(i))
                format_filter &= s & "|*." & s
                If i < mArchetypeEngine.AvailableFormats.Count - 1 Then
                    format_filter &= "|"
                End If
            Next
            Return format_filter
        End Get
    End Property
    Public ReadOnly Property AvailableFormats() As ArrayList
        Get
            Return mArchetypeEngine.AvailableFormats
        End Get
    End Property

    Public Function IndexOfFormat(ByVal a_format As String) As Integer
        ' returns a filedialog filter
        Return mArchetypeEngine.AvailableFormats.IndexOf(a_format)
    End Function

    Public Property FileLoading() As Boolean
        'Public ReadOnly Property FileLoading() As Boolean
        Get
            Return mIsFileLoading
        End Get

        'End Property

        'Private WriteOnly Property IsFileLoading() As Boolean
        Set(ByVal Value As Boolean)
            mIsFileLoading = Value

            If Value Then
                ' stop recursive updates through the interface to the ontology
                OntologyManager.DoUpdateOntology = False
            Else
                ' allow them
                OntologyManager.DoUpdateOntology = True
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

    Public Function OpenArchetype(ByVal aFileName As String) As Boolean
        'Try
        mPriorFileName = Me.FileName

        Me.FileName = aFileName

        If aFileName.EndsWith(".adl") Then
            If mArchetypeEngine Is Nothing Then
                mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
            End If
        Else
            Debug.Assert(False)
            MessageBox.Show("File type: " & aFileName & " is not supported", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        mHasOpenFileError = False
        mArchetypeEngine.OpenFile(aFileName, Me)

        If mArchetypeEngine.OpenFileError Then
            mHasOpenFileError = True

            Return False

        Else
            mPriorFileName = Nothing
            mOntologyManager.PopulateAllTerms()

            FileEdited = False

            RaiseEvent ArchetypeLoaded(Me, New EventArgs)

            Return True
        End If
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

    Public Sub ParserReset(Optional ByVal an_archetype_ID As ArchetypeID = Nothing)
        If mArchetypeEngine Is Nothing Then
            mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
        Else
            If Not an_archetype_ID Is Nothing Then
                NewArchetype(an_archetype_ID)
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

    Public Function SaveArchetype() As Boolean
        Dim s As String

        If Me.IsNew Then   ' never saved before
            s = ChooseFileName()
        Else
            If IO.File.Exists(Me.FileName) Then  ' check the file exists so it is saving over the top
                s = FileName
            Else
                s = ChooseFileName()
            End If
        End If

        If s <> "" Then

            ' The file might be in a repository and readonly
            If IO.File.Exists(s) Then
                ' if it isn't a new file
                Dim fa As IO.FileAttributes = IO.File.GetAttributes(s)
                'check it isn't readonly
                If (fa And IO.FileAttributes.ReadOnly) > 0 Then
                    MessageBox.Show(s & ": " & Filemanager.GetOpenEhrTerm(439, "Read only"), AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If

            'Now write the arcehtype using the parser
            Me.FileName = s

            Try
                mObjectToSave.PrepareToSave()
                WriteArchetype()
                Return True
            Catch ex As Exception
                MessageBox.Show(AE_Constants.Instance.Error_saving & Me.FileName & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        Else
            Return False  ' did not set a filename
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
            saveFile.FilterIndex = i  ' adl is the third type
        End If
        saveFile.AddExtension = True
        saveFile.Title = AE_Constants.Instance.MessageBoxCaption
        saveFile.ValidateNames = True
        If saveFile.ShowDialog() = DialogResult.Cancel Then
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


    Public Sub WriteArchetype()

        Dim i As Integer

        'Check that the file name is an available format

        Dim s As String = mFileName.Substring(mFileName.LastIndexOf(".") + 1)

        If FormatIsAvailable(s) Then
            mHasWriteFileError = False
            mArchetypeEngine.WriteFile(mFileName, s)
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

    Public Sub NewArchetype(ByVal an_ArchetypeID As ArchetypeID, Optional ByVal FileType As String = "adl")
        Select Case FileType
            Case "adl", "ADL", "Adl"
                Dim a_ontology As ArchetypeEditor.ADL_Classes.ADL_Ontology
                Dim a_term As RmTerm
                mArchetypeEngine.NewArchetype(an_ArchetypeID, OceanArchetypeEditor.Instance.DefaultLanguageCode)

                If mArchetypeEngine.ArchetypeAvailable Then

                    a_ontology = New ArchetypeEditor.ADL_Classes.ADL_Ontology(CType(mArchetypeEngine, ArchetypeEditor.ADL_Classes.ADL_Interface).ADL_Parser)

                    'Apply a new ontology - this empties the GUI - use ReplaceOntology to preserve
                    OntologyManager.Ontology = a_ontology
                    ' a new archetype always has a concept code set to "at0000"
                    a_term = New RmTerm(mArchetypeEngine.Archetype.ConceptCode)
                    a_term.Text = "?"
                    OntologyManager.UpdateTerm(a_term)
                End If
            Case Else
                Debug.Assert(False)
                'Case "archetype", "ARCHETYPE", "Archetype"
                '    mArchetypeEngine = New Text_Parsing.TextParser(OntologyManager.Instance, OceanArchetypeEditor.Instance.DefaultLanguageCode)
        End Select

    End Sub

    Sub New()
        mArchetypeEngine = New ArchetypeEditor.ADL_Classes.ADL_Interface
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

    Shared Sub SetFileChangedToolBar(ByVal isChanged As Boolean)
        ' used by embedded archetypes to set the GUI to filechanged
        RaiseEvent IsFileDirtyChanged(New FileManagerEventArgs(isChanged))
    End Sub
    Private Sub OnIsFileDirtyChanged(ByVal IsFileDirty As Boolean)
        RaiseEvent IsFileDirtyChanged(New FileManagerEventArgs(IsFileDirty))
    End Sub

    'Public Shared ReadOnly Property Instance() As FileManagerLocal
    '    Get
    '        If mInstance Is Nothing Then
    '            mInstance = New FileManagerLocal
    '            'Throw New InvalidOperationException("FileManager instance not created")
    '        End If

    '        Return mInstance
    '    End Get
    'End Property

    Public Shared Sub AddEmbedded(ByVal f As FileManagerLocal)
        mFileManagerCollection.Add(f)
    End Sub
    Public Shared Property Master() As FileManagerLocal
        Get
            If mFileManagerCollection.Count > 0 Then
                Return mFileManagerCollection(0)
            End If
        End Get
        Set(ByVal Value As FileManagerLocal)
            mFileManagerCollection.Insert(0, Value)
        End Set
    End Property
    Public Shared ReadOnly Property HasFileToSave() As Boolean
        Get
            'If mInstance.FileEdited Then
            '    Return True
            'Else
            For Each f As FileManagerLocal In mFileManagerCollection
                If f.FileEdited Then
                    Return True
                End If
            Next
            'End If
            Return False
        End Get
    End Property

    Public Shared Function GetOpenEhrTerm(ByVal code As Integer, ByVal default_text As String, Optional ByVal language As String = "") As String
        If language = "" Then
            Return Master.OntologyManager.GetOpenEHRTerm(code, default_text)
        Else
            Return Master.OntologyManager.GetOpenEHRTerm(code, default_text, language)
        End If
    End Function

    Public Shared Function SaveFiles(ByVal askToSave As Boolean) As Boolean
        'If mInstance.FileEdited Then
        '    Select Case MessageBox.Show(AE_Constants.Instance.Save_changes & mFileManager.Archetype.Archetype_ID.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        '        Case DialogResult.Cancel
        '            Return False
        '        Case DialogResult.Yes
        '            mInstance.SaveArchetype()
        '            mInstance.FileEdited = False
        '    End Select
        'End If
        For Each f As FileManagerLocal In mFileManagerCollection
            If f.FileEdited Then
                If mFileManagerCollection.Count > 1 Or askToSave Then
                    Select Case MessageBox.Show(AE_Constants.Instance.Save_changes & f.Archetype.Archetype_ID.ToString, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case DialogResult.Cancel
                            Return False
                        Case DialogResult.No
                            Return True
                        Case DialogResult.Yes
                            If f.SaveArchetype() Then
                                f.IsNew = False
                                f.FileEdited = False
                            Else
                                Return False
                            End If
                    End Select
                Else
                    If f.SaveArchetype() Then
                        f.IsNew = False
                        f.FileEdited = False
                    Else
                        Return False
                    End If
                End If
            End If
        Next
        Return True
    End Function




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
