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
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes
    Class ADL_Interface
        Implements Parser, IDisposable
        Private marchetypeParser As AdlParser.ArchetypeParser
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

        Public ReadOnly Property ArchetypeParser() As AdlParser.ArchetypeParser
            Get
                Return marchetypeParser
            End Get
        End Property

        Public ReadOnly Property AvailableFormats() As ArrayList Implements Parser.AvailableFormats
            Get
                Dim result As New ArrayList

                For i As Integer = 1 To ArchetypeParser.ArchetypeSerialiserFormats.Count
                    result.Add(ArchetypeParser.ArchetypeSerialiserFormats.ITh(i).ToString)
                Next

                Return result
            End Get
        End Property

        Public ReadOnly Property TypeName() As String Implements Parser.TypeName
            Get
                Return "adl"
            End Get
        End Property

        Public ReadOnly Property Status() As String Implements Parser.Status
            Get
                Return ArchetypeParser.AppRoot.Billboard.Content.ToCil
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
            ArchetypeParser.AppRoot.Billboard.Clear()
        End Sub

        Public Sub OpenFile(ByVal fileName As String, ByVal fileManager As FileManagerLocal) Implements Parser.OpenFile
            Dim current_culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            Dim replace_culture As Boolean

            mOpenFileError = True  ' default unless all goes well
            mFileName = fileName

            ' This code is essential to ensure that the parser reads regardless of the local culture
            If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator <> "." Then
                replace_culture = True
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture()
            End If

            ArchetypeParser.ArchDir.AddAdhocItem(Eiffel.String(fileName))
            Dim archetype As AdlParser.ArchRepArchetype = ArchetypeParser.ArchDir.SelectedArchetype

            If Not archetype Is Nothing AndAlso Not ArchetypeParser.AppRoot.Billboard.HasErrors Then
                archetype.Compile()

                If archetype.IsValid Then
                    fileManager.OntologyManager.Ontology = NewOntology()
                    adlArchetype = New ADL_Archetype(ArchetypeParser, fileManager)
                    mOpenFileError = False
                End If
            End If

            If replace_culture Then
                System.Threading.Thread.CurrentThread.CurrentCulture = current_culture
            End If
        End Sub

        Public Sub CreateNewArchetype(ByVal archetypeID As ArchetypeID, ByVal primaryLanguage As String) Implements Parser.CreateNewArchetype
            adlArchetype = New ADL_Archetype(ArchetypeParser, archetypeID, primaryLanguage)
        End Sub

        Public Function NewOntology() As Ontology Implements Parser.NewOntology
            Return New ADL_Ontology(ArchetypeParser)
        End Function

        Public Sub AddTermDefinitionsFromTable(ByVal table As DataTable, ByVal primaryLanguage As String)
            Dim arch As AdlParser.DifferentialArchetype = ArchetypeParser.DifferentialArchetype

            If Not arch Is Nothing Then
                Dim term As ADL_Term

                'First pass do primary language only
                For Each dRow As DataRow In table.Rows
                    Dim language As String = CType(dRow(0), String)

                    If primaryLanguage = language Then
                        term = New ADL_Term(CType(dRow(5), RmTerm))
                        arch.Ontology.AddTermDefinition(Eiffel.String(language), term.EIF_Term)
                    End If
                Next

                'Then subsequent languages
                For Each dRow As DataRow In table.Rows
                    Dim language As String = CType(dRow(0), String)

                    If primaryLanguage <> language Then
                        term = New ADL_Term(CType(dRow(5), RmTerm))
                        arch.Ontology.ReplaceTermDefinition(Eiffel.String(language), term.EIF_Term, False)
                    End If
                Next
            End If
        End Sub

        Public Sub AddConstraintDefinitionsFromTable(ByVal table As DataTable, ByVal primaryLanguage As String)
            Dim arch As AdlParser.DifferentialArchetype = ArchetypeParser.DifferentialArchetype

            If Not arch Is Nothing Then
                Dim term As ADL_Term

                'First pass do primary language only
                For Each dRow As DataRow In table.Rows
                    Dim language As String = CType(dRow(0), String)

                    If primaryLanguage = language Then
                        term = New ADL_Term(CStr(dRow(1)), CStr(dRow(2)), CStr(dRow(3)))
                        arch.Ontology.AddConstraintDefinition(Eiffel.String(language), term.EIF_Term)
                    End If
                Next

                'Then subsequent languages
                For Each dRow As DataRow In table.Rows
                    Dim language As String = CType(dRow(0), String)

                    If primaryLanguage <> language Then
                        term = New ADL_Term(CStr(dRow(1)), CStr(dRow(2)), CStr(dRow(3)))
                        arch.Ontology.ReplaceConstraintDefinition(Eiffel.String(language), term.EIF_Term, False)
                    End If
                Next
            End If
        End Sub

        Public Sub AddTermBindingsFromTable(ByVal table As DataTable)
            Dim arch As AdlParser.DifferentialArchetype = ArchetypeParser.DifferentialArchetype

            If Not arch Is Nothing Then
                Dim path As EiffelKernel.String_8
                Dim codePhrase As AdlParser.CodePhrase

                For Each dRow As DataRow In table.Rows
                    path = Eiffel.String(CType(dRow(1), String))
                    Dim terminologyId As String = CType(dRow(0), String)

                    If Not dRow.IsNull(3) Then
                        Dim version As String = CType(dRow(3), String)

                        If version <> String.Empty Then
                            terminologyId &= "(" & version & ")"
                        End If
                    End If

                    codePhrase = AdlParser.Create.CodePhrase.MakeFromString(Eiffel.String(terminologyId & "::" & CType(dRow(2), String)))
                    arch.Ontology.AddTermBinding(codePhrase, path)
                Next
            End If
        End Sub

        Public Sub AddConstraintBindingsFromTable(ByVal table As DataTable)
            Dim arch As AdlParser.DifferentialArchetype = ArchetypeParser.DifferentialArchetype

            If Not arch Is Nothing Then
                Dim terminology As EiffelKernel.String_8
                Dim constraintCode As EiffelKernel.String_8
                Dim path As AdlParser.Uri

                For Each dRow As DataRow In table.Rows
                    terminology = Eiffel.String(CType(dRow(0), String))
                    constraintCode = Eiffel.String(CType(dRow(1), String))
                    path = AdlParser.Create.Uri.MakeFromString(Eiffel.String(CType(dRow(2), String)))
                    arch.Ontology.AddConstraintBinding(path, terminology, constraintCode)
                Next
            End If
        End Sub

        Public Function GetCanonicalArchetype() As XMLParser.ARCHETYPE Implements Parser.CanonicalArchetype
            adlArchetype.MakeParseTree()
            Return adlArchetype.CanonicalArchetype()
        End Function

        Public Sub WriteFile(ByVal fileName As String, ByVal format As String, ByVal parserSynchronised As Boolean) Implements Parser.WriteFile
            'Change from intermediate format to ADL then make it again
            Dim errorMessage As String = Nothing

            Try
                If Not parserSynchronised Then
                    adlArchetype.MakeParseTree()
                End If

                Dim arch As AdlParser.ArchRepArchetype = ArchetypeParser.SelectedArchetype

                If arch Is Nothing Then
                    errorMessage = "Archetype is not available"
                ElseIf Not ArchetypeParser.HasArchetypeSerialiserFormat(Eiffel.String(format)) Then
                    errorMessage = "Archetype format '" & format & "' is not available"
                Else
                    adlArchetype.RemoveUnusedCodes()
                    adlArchetype.SetArchetypeDigest()
                    arch.SaveFlatAs(Eiffel.String(fileName), Eiffel.String(format))

                    If Not arch.SaveSucceeded Then
                        errorMessage = arch.Status.ToCil
                        arch.Reset()
                    End If
                End If
            Catch ex As Exception
                errorMessage = ex.Message
            End Try

            mWriteFileError = Not errorMessage Is Nothing

            If Not errorMessage Is Nothing Then
                MessageBox.Show(AE_Constants.Instance.Error_saving & " " & errorMessage, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        ' This method disposes the base object's resources.
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then
                    ' Insert code to free unmanaged resources.
                End If
                adlArchetype = Nothing
                marchetypeParser = Nothing
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
            marchetypeParser = AdlParser.Create.ArchetypeParser.DefaultCreate
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
