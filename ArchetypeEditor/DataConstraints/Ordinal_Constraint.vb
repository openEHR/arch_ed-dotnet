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

Public Class Constraint_Ordinal : Inherits Constraint_with_value

    Private mFixed As Boolean
    Private WithEvents mOrdinalTable As OrdinalTable
    Private mAssumedValue As Integer
    Private mTerminologyId As String = "local"
    Private mCodeString As String
    Private mIsLoadingComplete As Boolean
    Private mLanguage As String
    Private mFileManager As FileManagerLocal

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Ordinal(mFileManager)
        result.mFixed = mFixed
        result.OrdinalValues.Copy(mOrdinalTable)

        'SRH - 4th March 2009 - EDT-523 - error with assumed value being set to zero
        'ord.HasAssumedValue = HasAssumedValue
        If HasAssumedValue Then
            result.AssumedValue = mAssumedValue
        End If

        result.AssumedValue_TerminologyId = mTerminologyId
        result.AssumedValue_CodeString = mCodeString
        result.EndLoading()
        Return result
    End Function

    Public Sub ClearOrdinalValues()
        mOrdinalTable.Rows.Clear()
    End Sub

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Ordinal
        End Get
    End Property

    Public Overrides Property AssumedValue() As Object
        Get
            If HasAssumedValue Then
                Return mAssumedValue
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            Debug.Assert(TypeOf Value Is Integer)

            mAssumedValue = CInt(Value)
            HasAssumedValue = True
            Dim row As DataRow = mOrdinalTable.Rows.Find(Value)

            If Not row Is Nothing Then
                AssumedValue_CodeString = CStr(row.Item(2))
            End If
        End Set
    End Property

    Public Property AssumedValue_TerminologyId() As String
        Get
            If HasAssumedValue Then
                Return mTerminologyId
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            mTerminologyId = value
        End Set
    End Property

    Public Property AssumedValue_CodeString() As String
        Get
            If HasAssumedValue Then
                Return mCodeString
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            mCodeString = value
        End Set
    End Property

    Public ReadOnly Property OrdinalValues() As OrdinalTable
        Get
            Return mOrdinalTable
        End Get
    End Property

    Public Property Fixed() As Boolean
        Get
            Fixed = mFixed
        End Get
        Set(ByVal Value As Boolean)
            mFixed = Value
        End Set
    End Property

    Public ReadOnly Property IsInitialised() As Boolean
        Get
            Return mIsLoadingComplete
        End Get
    End Property

    Public ReadOnly Property NextFreeOrdinalValue() As Integer
        Get
            Dim ord As Integer

            For i As Integer = 0 To mOrdinalTable.Count - 1
                If i = 0 Then
                    ord = CType(mOrdinalTable.Rows(i).Item(0), Integer)
                Else
                    ord += 1

                    If mOrdinalTable.Rows.Find(ord) Is Nothing Then
                        Return ord
                    End If
                End If
            Next

            Return ord + 1
        End Get
    End Property

    ReadOnly Property InternalCodes() As String()
        Get
            If mOrdinalTable IsNot Nothing AndAlso mOrdinalTable.Rows.Count > 0 Then
                Dim upperBound As Integer = mOrdinalTable.Rows.Count - 1
                Dim result(upperBound) As String

                For i As Integer = 0 To upperBound
                    result(i) = CStr(mOrdinalTable.Rows(i).Item(2))
                Next

                Return result
            End If

            Return CType(Array.CreateInstance(GetType(String), 0), String())
        End Get
    End Property

    Property Language() As String
        Get
            Return mLanguage
        End Get
        Set(ByVal Value As String)
            mLanguage = Value
        End Set
    End Property

    Sub New(ByVal fileManager As FileManagerLocal)
        mFileManager = fileManager
        mOrdinalTable = New OrdinalTable
    End Sub

    Sub New(ByVal isLoadingComplete As Boolean, ByVal fileManager As FileManagerLocal)
        Me.New(fileManager)
        mIsLoadingComplete = isLoadingComplete
    End Sub

    Private Sub OrdinalTable_RowDeleting(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs) Handles mOrdinalTable.RowDeleting
        If mIsLoadingComplete And e.Action = DataRowAction.Delete Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub OrdinalTable_ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles mOrdinalTable.ColumnChanging
        If mIsLoadingComplete Then
            Select Case e.Column.ColumnName
                Case "OrdinalText"
                    Dim ordinal As New OrdinalValue(e.Row)

                    If TypeOf e.Row.Item(0) Is System.DBNull Then
                        mIsLoadingComplete = False
                        e.Row.Item(0) = mOrdinalTable.Rows.Count
                        mIsLoadingComplete = True
                    End If

                    If TypeOf e.Row.Item(2) Is System.DBNull Then
                        'add ordinal to ontology when new ordinal is added
                        mIsLoadingComplete = False
                        Dim term As RmTerm = mFileManager.OntologyManager.AddTerm(CStr(e.ProposedValue))
                        ordinal.InternalCode = term.Code
                        e.Row.Item(1) = term.Text
                        mIsLoadingComplete = True
                    Else
                        'update ontology with ordinal text value when ordinal is edited
                        mFileManager.OntologyManager.SetText(CStr(e.ProposedValue), ordinal.InternalCode)
                    End If

                Case "Ordinal"
                    If TypeOf e.Row.Item(2) Is System.DBNull Then
                        mIsLoadingComplete = False
                        Dim ordinal As New OrdinalValue(e.Row)
                        Dim term As RmTerm = mFileManager.OntologyManager.AddTerm("new ordinal")
                        ordinal.InternalCode = term.Code
                        e.Row.Item(1) = term.Text
                        mIsLoadingComplete = True
                    End If

                Case "OrdinalDescription"
                    If TypeOf e.Row.Item(0) Is System.DBNull And TypeOf e.Row.Item(2) Is System.DBNull Then
                        e.ProposedValue = System.DBNull.Value
                    Else
                        'update ontology with ordinal text value when ordinal is edited
                        mFileManager.OntologyManager.SetDescription(CStr(e.ProposedValue), CStr(e.Row.Item(2)))
                    End If
            End Select

            mFileManager.FileEdited = True
        End If
    End Sub

    Public Sub EndLoading()
        mIsLoadingComplete = True
    End Sub

    Public Sub BeginLoading()
        mIsLoadingComplete = False
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
'The Original Code is Ordinal_Constraint.vb.
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
