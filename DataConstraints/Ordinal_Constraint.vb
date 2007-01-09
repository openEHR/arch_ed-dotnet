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

Public Class Constraint_Ordinal : Inherits Constraint_with_value

    Public Event AssumedValueChanged As EventHandler

    Private mFixed As Boolean
    Private WithEvents mOrdinalTable As OrdinalTable
    Private mAssumedValue As Integer
    Private mIsLoadingComplete As Boolean
    Private mLanguage As String
    Private mFileManager As FileManagerLocal

    Public Overrides Function Copy() As Constraint
        Dim ord As New Constraint_Ordinal(mFileManager)

        ord.mFixed = mFixed
        ord.OrdinalValues.Copy(mOrdinalTable)
        ord.HasAssumedValue = Me.HasAssumedValue
        ord.AssumedValue = mAssumedValue

        ord.EndLoading()

        Return ord

    End Function

    Public Sub ClearOrdinalValues()
        mOrdinalTable.Rows.Clear()
    End Sub

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Ordinal
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

            OnAssumedValueChanged()
        End Set
    End Property

    Protected Sub OnAssumedValueChanged()
        RaiseEvent AssumedValueChanged(Me, New EventArgs)
    End Sub

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
            Dim i, ord As Integer

            For i = 0 To mOrdinalTable.Count - 1

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

    Property Language() As String
        Get
            Return mLanguage
        End Get
        Set(ByVal Value As String)
            mLanguage = Value
        End Set
    End Property

    Sub New(ByVal a_local_filemanager As FileManagerLocal)
        mFileManager = a_local_filemanager
        mOrdinalTable = New OrdinalTable
    End Sub

    Sub New(ByVal IsLoadingComplete As Boolean, ByVal a_local_filemanager As FileManagerLocal)
        Me.New(a_local_filemanager)
        mIsLoadingComplete = IsLoadingComplete
    End Sub

    Private Sub OrdinalTable_RowDeleting(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs) _
            Handles mOrdinalTable.RowDeleting

        If Not mIsLoadingComplete Then Exit Sub

        If e.Action = DataRowAction.Delete Then
            If Me.HasAssumedValue Then

                Dim ordinalValue As New OrdinalValue(e.Row)
                'Debug.Assert(TypeOf e.Row(1) Is Long)
                Debug.Assert(TypeOf Me.AssumedValue Is Integer)

                'If CLng(e.Row(1)) = CLng(Me.Constraint.AssumedValue) Then
                If ordinalValue.Ordinal = CInt(Me.AssumedValue) Then

                    Me.HasAssumedValue = False
                    'Me.txtAssumedOrdinal.Text = "(none)"

                    'TODO: update assumedOrdinal textbox
                    Debug.Assert(False, "TODO: update assumedOrdinal textbox")
                    OnAssumedValueChanged()
                End If

            End If

            mFileManager.FileEdited = True
        End If

    End Sub


    Private Sub OrdinalTable_ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) _
            Handles mOrdinalTable.ColumnChanging

        If Not mIsLoadingComplete Then Return

        'If e.Column.Ordinal = 2 Then
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
                    Dim a_Term As RmTerm = mFileManager.OntologyManager.AddTerm( _
                            CStr(e.ProposedValue))
                    ordinal.InternalCode = a_Term.Code
                    e.Row.Item(1) = a_term.Text
                    mIsLoadingComplete = True
                Else
                    'update ontology with ordinal text value when ordinal is edited
                    mFileManager.OntologyManager.SetText(CStr(e.ProposedValue), _
                            ordinal.InternalCode)
                End If

                'Added Sam Heard 2004-07-05
            Case "Ordinal"
                If TypeOf e.Row.Item(2) Is System.DBNull Then
                    mIsLoadingComplete = False
                    Dim ordinal As New OrdinalValue(e.Row)
                    Dim a_Term As RmTerm = mFileManager.OntologyManager.AddTerm( _
                                              "new ordinal")
                    ordinal.InternalCode = a_term.Code
                    e.Row.Item(1) = a_term.Text
                    mIsLoadingComplete = True
                End If

            Case "OrdinalDescription"
                If (TypeOf e.Row.Item(0) Is System.DBNull) And (TypeOf e.Row.Item(2) Is System.DBNull) Then
                    e.ProposedValue = System.DBNull.Value
                Else
                    'update ontology with ordinal text value when ordinal is edited
                    mFileManager.OntologyManager.SetDescription(CStr(e.ProposedValue), _
                            CStr(e.Row.Item(2)))
                End If


        End Select

        mFileManager.FileEdited = True
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
