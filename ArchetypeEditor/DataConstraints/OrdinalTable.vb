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
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class OrdinalTable : Inherits DataTable
    Implements IEnumerable
    Implements IEnumerator

    Public Sub New()
        MyBase.New("OrdinalTable")
        InitialiseTable()
    End Sub

    Private Sub InitialiseTable()
        Dim OrdColumn As DataColumn = New DataColumn
        OrdColumn.DataType = GetType(Integer)
        OrdColumn.ColumnName = "Ordinal"
        OrdColumn.AllowDBNull = False
        Columns.Add(OrdColumn)

        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = GetType(String)
        TextColumn.ColumnName = "OrdinalText"
        TextColumn.AllowDBNull = True
        Columns.Add(TextColumn)

        Dim IdColumn As DataColumn = New DataColumn
        IdColumn.DataType = GetType(String)
        IdColumn.ColumnName = "Code"
        Columns.Add(IdColumn)

        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = GetType(String)
        DescriptionColumn.ColumnName = "OrdinalDescription"
        DescriptionColumn.AllowDBNull = True
        Columns.Add(DescriptionColumn)

        Dim keys(0) As DataColumn
        keys(0) = OrdColumn
        PrimaryKey = keys

        DefaultView.Sort = "Ordinal"
    End Sub

    Public Overloads Sub Copy(ByVal sourceTable As OrdinalTable)
        Rows.Clear()

        For Each sourceOrdinal As OrdinalValue In sourceTable
            Dim o As OrdinalValue = NewOrdinal()
            o.Copy(sourceOrdinal)
            Add(o)
        Next
    End Sub

    Public Function NewOrdinal() As OrdinalValue
        Return New OrdinalValue(NewRow)
    End Function

    Public Sub Add(ByVal anOrdinal As OrdinalValue)
        Rows.Add(anOrdinal.DataRow)
    End Sub

    Public ReadOnly Property Count() As Integer
        Get
            Return DefaultView.Count
        End Get
    End Property

    Public Sub Commit()
        If Not mRowEnumerator Is Nothing Then
            Dim rowView As DataRowView = CType(mRowEnumerator.Current, DataRowView)

            If Not rowView Is Nothing Then
                If rowView.IsEdit Then
                    rowView.EndEdit()
                ElseIf rowView.IsNew Then
                    rowView.CancelEdit()
                End If
            End If
        End If
    End Sub

    Private Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        mRowEnumerator = DefaultView.GetEnumerator
        Return Me
    End Function

    Private mRowEnumerator As IEnumerator

    Private ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        Get
            Debug.Assert(Not mRowEnumerator Is Nothing)
            Debug.Assert(TypeOf mRowEnumerator.Current Is DataRowView)

            Dim rowView As DataRowView = CType(mRowEnumerator.Current, DataRowView)
            Return New OrdinalValue(rowView.Row)
        End Get
    End Property

    Private Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
        Debug.Assert(Not mRowEnumerator Is Nothing)

        Return mRowEnumerator.MoveNext
    End Function

    Private Sub ResetEnumerator() Implements System.Collections.IEnumerator.Reset
        Debug.Assert(Not mRowEnumerator Is Nothing)

        mRowEnumerator.Reset()
    End Sub

End Class

Public Class OrdinalValue
    Private mDataRow As DataRow

    Public ReadOnly Property DataRow() As DataRow
        Get
            Return mDataRow
        End Get
    End Property

    Public Property Ordinal() As Integer
        Get
            If mDataRow.IsNull(0) Then
                Return 0
            Else
                Return CInt(mDataRow(0))
            End If
        End Get
        Set(ByVal Value As Integer)
            mDataRow(0) = Value
        End Set
    End Property

    Public Property InternalCode() As String
        Get
            If mDataRow.IsNull(2) Then
                Return ""
            Else
                Return CStr(mDataRow(2))
            End If
        End Get
        Set(ByVal Value As String)
            mDataRow(2) = Value
        End Set
    End Property

    Public Property Text() As String
        Get
            If mDataRow.IsNull(1) Then
                Return ""
            Else
                Return CStr(mDataRow(1))
            End If
        End Get
        Set(ByVal Value As String)
            mDataRow(1) = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            If mDataRow.IsNull(3) Then
                Return ""
            Else
                Return CStr(mDataRow(3))
            End If
        End Get
        Set(ByVal Value As String)
            mDataRow(3) = Value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Dim result As String = ""

        If mDataRow.RowState <> DataRowState.Detached Then
            result = Ordinal.ToString.PadRight(2) & " - " & Text
        End If

        Return result
    End Function

    Public Sub New(ByVal newRow As DataRow)
        mDataRow = newRow
    End Sub

    Public Sub Copy(ByVal source As OrdinalValue)
        mDataRow(0) = source.Ordinal
        mDataRow(1) = source.Text
        mDataRow(2) = source.InternalCode
        mDataRow(3) = source.Description
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
'The Original Code is OrdinalTable.vb.
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
