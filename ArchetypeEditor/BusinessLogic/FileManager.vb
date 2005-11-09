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

Option Strict On

Public Class FileManagerLocal
    Private mIsFileDirty As Boolean
    Private mIsFileLoading As Boolean
    Private mHasOpenFileError, mHasWriteFileError As Boolean
    Private mArchetypeEngine As Parser
    Private mFileName, mPriorFileName As String
    Private mWorkingDirectory As String
    Private mIsNew As Boolean = False
    Private mOntologyManager As New OntologyManager

    Public Event IsFileDirtyChanged As FileManagerEventHandler
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
                OnIsFileDirtyChanged(Value)
            End If
        End Set
    End Property
    Public ReadOnly Property AvailableFormatFilter() As String
        Get
            ' returns a filedialog filter
            Dim format_filter As String

            For i As Integer = 0 To mArchetypeEngine.AvailableFormats.Length - 1
                Dim s As String = mArchetypeEngine.AvailableFormats(i)

                format_filter &= s & "|*." & s
                If i < mArchetypeEngine.AvailableFormats.Length - 1 Then
                    format_filter &= "|"
                End If
            Next
            Return format_filter
        End Get
    End Property

    Public Function IndexOfFormat(ByVal a_format As String) As Integer
        ' returns a filedialog filter
        Dim format_filter As String

        For i As Integer = 0 To mArchetypeEngine.AvailableFormats.Length - 1
            If a_format = mArchetypeEngine.AvailableFormats(i) Then
                Return i + 1
            End If
        Next
        Return 0
    End Function

    Protected Sub OnIsFileDirtyChanged(ByVal IsFileDirty As Boolean)
        RaiseEvent IsFileDirtyChanged(Me, New FileManagerEventArgs(IsFileDirty))
    End Sub

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
                Filemanager.Instance.OntologyManager.DoUpdateOntology = False
            Else
                ' allow them
                Filemanager.Instance.OntologyManager.DoUpdateOntology = True
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
                mArchetypeEngine = New ADL_Interface
            End If
        Else
            Debug.Assert(False)
            MessageBox.Show("File type: " & aFileName & " is not supported", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        mHasOpenFileError = False
        mArchetypeEngine.OpenFile(aFileName, mOntologyManager)

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

        For i As Integer = 0 To mArchetypeEngine.AvailableFormats.Length - 1
            If a_format = mArchetypeEngine.AvailableFormats(i) Then
                Return True
            End If
        Next
        Return False

    End Function

    Public Sub ParserReset(Optional ByVal an_archetype_ID As ArchetypeID = Nothing)
        If mArchetypeEngine Is Nothing Then
            mArchetypeEngine = New ADL_Interface
        Else
            If Not an_archetype_ID Is Nothing Then
                NewArchetype(an_archetype_ID)
            Else
                mArchetypeEngine.ResetAll()
                ' need to reload the current archetype if there is one
                If Not mPriorFileName = Nothing Then
                    mArchetypeEngine.OpenFile(mPriorFileName, mOntologyManager)
                    mPriorFileName = Nothing
                End If
            End If
        End If
    End Sub

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
                Dim a_ontology As ADL_Ontology
                Dim a_term As RmTerm
                mArchetypeEngine.NewArchetype(an_ArchetypeID, ArchetypeEditor.Instance.DefaultLanguageCode)

                If mArchetypeEngine.ArchetypeAvailable Then

                    a_ontology = New ADL_Ontology(CType(mArchetypeEngine, ADL_Interface).ADL_Parser)

                    'Apply a new ontology - this empties the GUI - use ReplaceOntology to preserve
                    Filemanager.Instance.OntologyManager.Ontology = a_ontology
                    ' a new archetype always has a concept code set to "at0000"
                    a_term = New RmTerm(mArchetypeEngine.Archetype.ConceptCode)
                    a_term.Text = "?"
                    Filemanager.Instance.OntologyManager.UpdateTerm(a_term)
                End If
            Case Else
                Debug.Assert(False)
                'Case "archetype", "ARCHETYPE", "Archetype"
                '    mArchetypeEngine = New Text_Parsing.TextParser(OntologyManager.Instance, ArchetypeEditor.Instance.DefaultLanguageCode)
        End Select

    End Sub

    Sub New() 'ByVal ParentFrm As Archetype_editor.Designer)
        'default is to work with ADL
        'sDefaultLanguageCode = ParentFrm.DefaultLanguageCode
        'mEditor = ParentFrm
        mArchetypeEngine = New ADL_Interface
    End Sub
End Class

Public Delegate Sub FileManagerEventHandler(ByVal sender As Object, _
        ByVal e As FileManagerEventArgs)

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
    Inherits FileManagerLocal
    ' Allows Designer wide access to FileManager, while enabling local access if required

    ' FileManager Singleton
    Private Shared mInstance As FileManagerLocal


    Public Shared ReadOnly Property Instance() As FileManagerLocal
        Get
            If mInstance Is Nothing Then
                mInstance = New FileManagerLocal
                'Throw New InvalidOperationException("FileManager instance not created")
            End If

            Return mInstance
        End Get
    End Property

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