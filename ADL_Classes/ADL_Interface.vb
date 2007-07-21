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
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel

Namespace ArchetypeEditor.ADL_Classes
    Class ADL_Interface
        Implements Parser, IDisposable
        Private EIF_adlInterface As openehr.adl_parser.interface.ADL_INTERFACE
        Private mFileName As String
        Private adlArchetype As ADL_Archetype
        Private mOpenFileError As Boolean
        Private mWriteFileError As Boolean
        Protected disposed As Boolean = False


        Public ReadOnly Property FileName() As String Implements Parser.FileName
            Get
                Return mFileName
            End Get
        End Property
        Public ReadOnly Property ADL_Parser() As openehr.adl_parser.interface.ADL_INTERFACE
            Get
                Return EIF_adlInterface
            End Get
        End Property
        Public ReadOnly Property AvailableFormats() As ArrayList Implements Parser.AvailableFormats
            Get
                Dim formats As New ArrayList
                Dim s As String

                For i As Integer = 1 To EIF_adlInterface.archetype_serialiser_formats.count
                    s = CType(EIF_adlInterface.archetype_serialiser_formats.i_th(i), EiffelKernel.STRING_8).to_cil
                    ' sml is not valid
                    formats.Add(s)
                Next

                Return formats
            End Get
        End Property
        Public ReadOnly Property TypeName() As String Implements Parser.TypeName
            Get
                Return "adl"
            End Get
        End Property
        Public ReadOnly Property Status() As String Implements Parser.Status
            Get
                Return EIF_adlInterface.status.to_cil
            End Get
        End Property
        Public ReadOnly Property ArchetypeAvailable() As Boolean Implements Parser.ArchetypeAvailable
            Get
                Return Not adlArchetype Is Nothing
            End Get
        End Property
        Public ReadOnly Property Archetype() As Archetype Implements Parser.Archetype
            Get
                Return adlArchetype
            End Get
        End Property
        Public ReadOnly Property OpenFileError() As Boolean Implements Parser.OpenFileError
            Get
                Return mOpenFileError
            End Get
        End Property
        Public ReadOnly Property WriteFileError() As Boolean Implements Parser.WriteFileError
            Get
                Return mWriteFileError
            End Get
        End Property

        Public Sub ResetAll() Implements Parser.ResetAll
            EIF_adlInterface.reset()
        End Sub

        Public Sub Serialise(ByVal a_format As String) Implements Parser.Serialise
            If Me.AvailableFormats.Contains(a_format) Then
                Try
                    adlArchetype.MakeParseTree()
                    EIF_adlInterface.serialise_archetype(EiffelKernel.Create.STRING_8.make_from_cil(a_format))
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                    MessageBox.Show(AE_Constants.Instance.Error_saving, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub


        Public Sub OpenFile(ByVal FileName As String, ByVal a_filemanager As FileManagerLocal) Implements Parser.OpenFile

            Dim current_culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            Dim replace_culture As Boolean

            mOpenFileError = True  ' default unless all goes wel
            mFileName = FileName

            ' ADDED 2004-11-18
            ' Sam Heard
            ' This code is essential to ensure that the parser reads regardless of the local culture

            If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator <> "." Then
                replace_culture = True
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture()
            End If

            EIF_adlInterface.open_adl_file(EiffelKernel.Create.STRING_8.make_from_cil(FileName))

            ' check that file openned successfully by checking status
            If EIF_adlInterface.archetype_source_loaded Then
                EIF_adlInterface.parse_archetype()
                If EIF_adlInterface.parse_succeeded Then
                    Dim the_ontology As ADL_Ontology
                    the_ontology = New ADL_Ontology(EIF_adlInterface)
                    a_filemanager.OntologyManager.Ontology = the_ontology
                    adlArchetype = New ADL_Archetype(EIF_adlInterface.adl_engine.archetype, EIF_adlInterface.adl_engine, a_filemanager)
                    If EIF_adlInterface.archetype_available Then
                        mOpenFileError = False
                    End If
                End If
            End If

            If replace_culture Then
                System.Threading.Thread.CurrentThread.CurrentCulture = current_culture
            End If
        End Sub

        Public Sub NewArchetype(ByVal adlArchetypeID As ArchetypeID, ByVal LanguageCode As String) Implements Parser.NewArchetype
            adlArchetype = New ADL_Archetype(EIF_adlInterface.adl_engine, adlArchetypeID, LanguageCode)
        End Sub

        Public Sub AddTermDefinitionsFromTable(ByVal a_table As DataTable, ByVal primary_language As String)
            Dim term As ADL_Term
            Dim EifLanguage As EiffelKernel.STRING_8

            'First pass do primary language only
            For Each dRow As DataRow In a_table.Rows
                Dim language As String = CType(dRow(0), String)
                If primary_language = language Then
                    EifLanguage = EiffelKernel.Create.STRING_8.make_from_cil(language)
                    term = New ADL_Term(CStr(dRow(1)), CStr(dRow(2)), CStr(dRow(3)), CStr(dRow(4)))
                    EIF_adlInterface.ontology.add_term_definition(EifLanguage, term.EIF_Term)
                End If
            Next

            'Then subsequent languages
            For Each dRow As DataRow In a_table.Rows
                Dim language As String = CType(dRow(0), String)
                If primary_language <> language Then
                    EifLanguage = EiffelKernel.Create.STRING_8.make_from_cil(language)
                    term = New ADL_Term(CType(dRow(1), String), CType(dRow(2), String), CType(dRow(3), String), CType(dRow(4), String))
                    EIF_adlInterface.ontology.replace_term_definition(EifLanguage, term.EIF_Term, False)
                End If
            Next
        End Sub

        Public Sub AddConstraintDefinitionsFromTable(ByVal a_table As DataTable, ByVal primary_language As String)
            Dim term As ADL_Term
            Dim language As EiffelKernel.STRING_8

            'First pass do primary language only
            For Each dRow As DataRow In a_table.Rows
                If primary_language = CType(dRow(0), String) Then
                    language = EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(0), String))
                    term = New ADL_Term(CType(dRow(1), String), CType(dRow(2), String), CType(dRow(3), String))
                    EIF_adlInterface.ontology.add_constraint_definition(language, term.EIF_Term)
                End If
            Next

            'Then subsequent languages
            For Each dRow As DataRow In a_table.Rows
                If primary_language <> CType(dRow(0), String) Then
                    language = EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(0), String))
                    term = New ADL_Term(CType(dRow(1), String), CType(dRow(2), String), CType(dRow(3), String))
                    EIF_adlInterface.ontology.replace_constraint_definition(language, term.EIF_Term, False)
                End If
            Next
        End Sub

        Public Sub AddTermBindingsFromTable(ByVal a_table As DataTable)
            Dim path As EiffelKernel.STRING_8
            Dim codePhrase As openehr.openehr.rm.data_types.text.CODE_PHRASE

            For Each dRow As DataRow In a_table.Rows
                path = EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(1), String))
                codePhrase = openehr.openehr.rm.data_types.text.Create.CODE_PHRASE.make_from_string( _
                    EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(0), String) & "::" & CType(dRow(2), String)))
                EIF_adlInterface.ontology.add_term_binding(codePhrase, path)
            Next
        End Sub

        Public Sub AddConstraintBindingsFromTable(ByVal a_table As DataTable)
            Dim terminology As EiffelKernel.STRING_8
            Dim constraintCode As EiffelKernel.STRING_8
            Dim path As openehr.common_libs.basic.URI

            For Each dRow As DataRow In a_table.Rows
                terminology = EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(0), String))
                constraintCode = EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(1), String))
                path = openehr.common_libs.basic.Create.URI.make_from_string(EiffelKernel.Create.STRING_8.make_from_cil(CType(dRow(2), String)))
                EIF_adlInterface.ontology.add_constraint_binding(path, terminology, constraintCode)
            Next
        End Sub


        Public Sub WriteFile(ByVal FileName As String, ByVal output_format As String, ByVal parserSynchronised As Boolean) Implements Parser.WriteFile
            'Change from intermediate format to ADL
            ' then make it again

            mWriteFileError = True ' default is that an error occurred
            Try
                If Not parserSynchronised Then
                    adlArchetype.MakeParseTree()
                End If
                If EIF_adlInterface.archetype_available Then
                    adlArchetype.RemoveUnusedCodes()
                    If EIF_adlInterface.has_archetype_serialiser_format(EiffelKernel.Create.STRING_8.make_from_cil(output_format)) Then
                        EIF_adlInterface.save_archetype(EiffelKernel.Create.STRING_8.make_from_cil(FileName), EiffelKernel.Create.STRING_8.make_from_cil(output_format))
                        If EIF_adlInterface.exception_encountered Then
                            MessageBox.Show(EIF_adlInterface.status.to_cil)
                            EIF_adlInterface.reset()
                        ElseIf Not EIF_adlInterface.save_succeeded Then
                            MessageBox.Show(EIF_adlInterface.status.to_cil)
                        Else
                            mWriteFileError = False
                        End If
                    Else
                        MessageBox.Show("Archetype format - " & output_format & " -  no longer available")
                    End If
                Else
                    MessageBox.Show("Archetype not available - error on making parse tree")
                End If
            Catch ex As Exception
                MessageBox.Show(AE_Constants.Instance.Error_saving & " " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Public Sub WriteAdlDirect(ByVal FileName As String)
            Try
                If EIF_adlInterface.archetype_available Then
                    adlArchetype.RemoveUnusedCodes()
                    If EIF_adlInterface.has_archetype_serialiser_format(EiffelKernel.Create.STRING_8.make_from_cil("adl")) Then
                        EIF_adlInterface.save_archetype(EiffelKernel.Create.STRING_8.make_from_cil(FileName), EiffelKernel.Create.STRING_8.make_from_cil("adl"))
                        If EIF_adlInterface.exception_encountered Then
                            MessageBox.Show(EIF_adlInterface.status.to_cil)
                            EIF_adlInterface.reset()
                        ElseIf Not EIF_adlInterface.save_succeeded Then
                            MessageBox.Show(EIF_adlInterface.status.to_cil)
                        Else
                            mWriteFileError = False
                        End If
                    Else
                        MessageBox.Show("Archetype format - ADL -  no longer available")
                    End If
                Else
                    MessageBox.Show("Archetype not available - error on making parse tree")
                End If
            Catch ex As Exception
                MessageBox.Show(AE_Constants.Instance.Error_saving & " " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' This method disposes the base object's resources.
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then
                    ' Insert code to free unmanaged resources.
                End If
                adlArchetype = Nothing
                EIF_adlInterface = Nothing
            End If
            Me.disposed = True
        End Sub

#Region " IDisposable Support "
        ' Do not change or add Overridable to these methods.
        ' Put cleanup code in Dispose(ByVal disposing As Boolean).
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        Protected Overrides Sub Finalize()
            Dispose(False)
            MyBase.Finalize()
        End Sub
#End Region


        Sub New()
            EIF_adlInterface = openehr.adl_parser.interface.Create.ADL_INTERFACE.make
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
'The Original Code is ADL_Interface.vb.
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
