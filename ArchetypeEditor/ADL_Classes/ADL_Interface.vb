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
Namespace ArchetypeEditor.ADL_Classes
    Class ADL_Interface
        Implements Parser
        Private EIF_adlInterface As openehr.adl_parser.interface.ADL_INTERFACE
        Private mFileName As String
        Private an_Archetype As ADL_Archetype
        Private mOpenFileError As Boolean
        Private mWriteFileError As Boolean

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

                For i As Integer = 1 To EIF_adlInterface.archetype_serialiser_formats.count
                    formats.Add(CType(EIF_adlInterface.archetype_serialiser_formats.i_th(i), openehr.base.kernel.STRING).to_cil)
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
                Return Not an_Archetype Is Nothing
            End Get
        End Property
        Public ReadOnly Property Archetype() As Archetype Implements Parser.Archetype
            Get
                Return an_Archetype
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
                    an_Archetype.MakeParseTree()
                    EIF_adlInterface.serialise_archetype(openehr.base.kernel.Create.STRING.make_from_cil(a_format))
                Catch e As Exception
                    Debug.Assert(False, e.Message)
                    MessageBox.Show(AE_Constants.Instance.Error_saving, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub


        Public Sub OpenFile(ByVal FileName As String, ByVal a_filemanager As FileManagerLocal) Implements Parser.OpenFile

            Dim current_culture As System.Globalization.CultureInfo
            Dim replace_culture As Boolean

            mOpenFileError = True  ' default unless all goes wel
            mFileName = FileName

            ' ADDED 2004-11-18
            ' Sam Heard
            ' This code is essential to ensure that the parser reads regardless of the local culture

            If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator <> "." Then
                current_culture = System.Globalization.CultureInfo.CurrentCulture
                replace_culture = True
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture()
            End If

            EIF_adlInterface.open_adl_file(openehr.base.kernel.Create.STRING.make_from_cil(FileName))

            ' check that file openned successfully by checking status
            If EIF_adlInterface.archetype_source_loaded Then
                EIF_adlInterface.parse_archetype()
                If EIF_adlInterface.parse_succeeded Then
                    Dim the_ontology As ADL_Ontology
                    the_ontology = New ADL_Ontology(EIF_adlInterface)
                    a_filemanager.OntologyManager.Ontology = the_ontology
                    an_Archetype = New ADL_Archetype(EIF_adlInterface.adl_engine.archetype, EIF_adlInterface.adl_engine, a_filemanager)
                    If EIF_adlInterface.archetype_available Then
                        mOpenFileError = False
                    End If
                End If
            End If

            If replace_culture Then
                System.Threading.Thread.CurrentThread.CurrentCulture = current_culture
            End If
        End Sub

        Public Sub NewArchetype(ByVal an_ArchetypeID As ArchetypeID, ByVal LanguageCode As String) Implements Parser.NewArchetype
            an_Archetype = New ADL_Archetype(EIF_adlInterface.adl_engine, an_ArchetypeID, LanguageCode)
        End Sub

        Public Sub WriteFile(ByVal FileName As String, Optional ByVal output_format As String = "adl") Implements Parser.WriteFile
            'Change from intermediate format to ADL
            ' then make it again

            mWriteFileError = True ' default is that an error occurred
            Try
                an_Archetype.MakeParseTree()
                If EIF_adlInterface.archetype_available Then
                    'an_Archetype.RemoveUnusedCodes()
                    If EIF_adlInterface.has_archetype_serialiser_format(openehr.base.kernel.Create.STRING.make_from_cil(output_format)) Then
                        EIF_adlInterface.save_archetype(openehr.base.kernel.Create.STRING.make_from_cil(FileName), openehr.base.kernel.Create.STRING.make_from_cil(output_format))
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
