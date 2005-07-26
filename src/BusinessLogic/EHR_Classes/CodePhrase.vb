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
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class CodePhrase
    Private sSeparator As String = "::"
    Private sTerminology As String = "local" ' defaults to local terminology
    Private cCodes As New Collections.Specialized.StringCollection

    Property TerminologyID() As String
        Get
            Return sTerminology
        End Get
        Set(ByVal Value As String)
            sTerminology = Value
        End Set
    End Property
    Property CodesAsCommaSeparatedString() As String
        Get
            Dim s, ss As String
            s = ""
            For Each ss In cCodes
                s = s & "," & ss
            Next
            Return s
        End Get
        Set(ByVal Value As String)
            Dim y() As String
            Dim i As Integer

            y = Value.Split(CType(",", Char))
            cCodes.Clear()
            For i = 0 To y.Length - 1
                cCodes.Add(y(i))
            Next
        End Set
    End Property
    Property FirstCode() As String
        ' allows dealing with single codes easily
        Get
            If cCodes.Count > 0 Then
                Return cCodes.Item(0)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As String)
            If cCodes.Count = 0 Then
                cCodes.Add(Value)
            Else
                cCodes.RemoveAt(0)
                cCodes.Insert(0, Value)
            End If
        End Set
    End Property
    ReadOnly Property Codes() As Collections.Specialized.StringCollection
        Get
            Return cCodes
        End Get
    End Property

    Property Phrase() As String
        Get
            If cCodes.Count = 0 Then
                Return Nothing
            End If
            Dim s, ss As String
            For Each ss In cCodes
                s = s & ss & ","
            Next
            'remove the trailing ","
            s = s.Remove(s.Length - 1, 1)
            Return sTerminology & sSeparator & s
        End Get
        Set(ByVal Value As String)
            Dim i As Integer

            'CHANGE - Sam Heard 2004-05-21
            'Bug allows empty code to be added
            'added if y(i) <> "" then to prevent this

            If Not Value Is Nothing Then
                i = InStr(Value, sSeparator)
                If i = 0 Then
                    sTerminology = ""
                    cCodes.Clear()
                Else
                    Dim y() As String
                    sTerminology = Value.Substring(0, i - 1)
                    y = Value.Substring(i + 1).Split(CType(",", Char))
                    cCodes.Clear()
                    For i = 0 To y.Length - 1
                        If y(i) <> "" Then
                            cCodes.Add(y(i).Trim)
                        End If
                    Next
                End If
            End If
        End Set
    End Property

    Property Separator() As String
        Get
            Return sSeparator
        End Get
        Set(ByVal Value As String)
            sSeparator = Value
        End Set
    End Property

    Function hasCode(ByVal code As String) As Boolean
        If cCodes.Contains(code) Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub New()

    End Sub

    Sub New(ByVal a_term As RmTerm)
        cCodes.Add(a_term.Code)
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
'The Original Code is CodePhrase.vb.
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