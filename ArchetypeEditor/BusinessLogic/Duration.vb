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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/ADL_Classes/Duration.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2007-01-09 19:45:11 +0930 (Tue, 09 Jan 2007) $"
'
'

Option Explicit On
Option Strict On

Friend Class Duration
    Private sISODuration As String
    Private sUnits As String
    'Private iValue As Integer
    Private iValue As Decimal
    Private yr As Integer
    Private mo As Integer
    Private w As Integer
    Private d As Integer
    Private h As Integer
    Private m As Integer
    Private s As Long
    Private fractionalSecond As Double

    Property ISO_duration() As String
        Get
            Return sISODuration
        End Get
        Set(ByVal Value As String)
            sISODuration = Value.Trim("| ".ToCharArray())
            ProcessIso()
        End Set
    End Property
    'Property GUI_duration() As Integer
    Property GUI_duration() As Decimal
        Get
            Return iValue
        End Get
        'Set(ByVal Value As Integer)
        Set(ByVal Value As Decimal)
            Debug.Assert(sUnits <> "", "Value set before units") 'ToDo: Stop this dependency
            iValue = Value
            SetIsoDuration()
        End Set
    End Property
    Property ISO_Units() As String
        Get
            Return sUnits
        End Get
        Set(ByVal Value As String)
            If OceanArchetypeEditor.ISO_TimeUnits.IsValidIsoUnit(Value) Then
                If sUnits <> Value Then
                    sUnits = Value
                    SetIsoDuration()
                End If
            Else
                Debug.Assert(False, Value & " is not a valid ISO Unit")
                Throw New Exception(Value & " is not a valid ISO Unit")
            End If
        End Set
    End Property

    Private Function toDuration(ByVal units As String) As String
        Select Case units
            Case "min", "mo"
                Return "M"
            Case "a"
                Return "Y"
            Case "wk"
                Return "W"
            Case Else 'D, H, S
                Return units.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
        End Select
    End Function

    Private Sub SetIsoDuration()
        If sUnits = "millisec" Then
            sISODuration = "PT" & (iValue / 1000).ToString & "S"
        Else
            If (sUnits.ToLowerInvariant = "d") Or (sUnits.ToLowerInvariant = "mo") Or (sUnits.ToLowerInvariant = "wk") Or (sUnits.ToLowerInvariant = "a") Then
                sISODuration = "P" & iValue.ToString & toDuration(sUnits)
            Else
                sISODuration = "PT" & iValue.ToString & toDuration(sUnits)
            End If
        End If
    End Sub

    Private Sub ProcessIso()
        Dim str As String
        Dim y() As String
        Dim ymwd As String

        ' drop the leading P and convert to lower
        str = sISODuration.Substring(1).ToLowerInvariant

        'SH: EDT-518 - support wk, mth, yr
        'y = str.Split("d".ToCharArray())
        y = str.Split(("t".ToCharArray))
        If y.Length > 1 Then
            'ymwd = Val(y(0)).ToString
            ymwd = y(0)
            str = y(1)
        Else
            ymwd = str
            str = ""
        End If

        'Process ymwd
        If Not String.IsNullOrEmpty(ymwd) Then
            'Yrs
            y = ymwd.Split("y".ToCharArray)
            If y.Length > 1 Then
                yr = CInt(y(0))
                ymwd = y(1)
            End If

            'Process months
            y = ymwd.Split("m".ToCharArray)
            If y.Length > 1 Then
                mo = CInt(y(0))
                sUnits = "mo"
                ymwd = y(1)
            End If

            'Process weeks
            y = ymwd.Split("w".ToCharArray)
            If y.Length > 1 Then
                w = CInt(y(0))
                ymwd = y(1)
            End If

            'Process days
            y = ymwd.Split("d".ToCharArray)
            If y.Length > 1 Then
                d = CInt(y(0))
                ymwd = y(1)
            End If
        End If

        'Process time
        If Not String.IsNullOrEmpty(str) Then

            y = str.Split("h"c)
            If y.Length > 1 Then
                h = CInt(Val(y(0)))
                str = y(1)
            End If
            y = str.Split("m"c)
            If y.Length > 1 Then
                m = CInt(Val(y(0)))
                str = y(1)
            End If
            y = str.Split("s"c)
            If y.Length > 1 Then
                fractionalSecond = Val(y(0))
                s = CLng(Val(y(0)))
            End If
        End If

        If sUnits <> "mo" Then
            If sISODuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("y") Then
                sUnits = "a"
            ElseIf sISODuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("w") Then
                sUnits = "wk"
            ElseIf sISODuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("d") Then
                sUnits = "d"
            ElseIf sISODuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("h") Then
                sUnits = "h"
            ElseIf sISODuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("m") Then
                sUnits = "min"
            ElseIf sISODuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("s") Then
                'If InStr(s.ToString, ".") > 0 Then
                If InStr(fractionalSecond.ToString, ".") > 0 Then
                    ' this means there is a decimal point and the period must have been in millisecs
                    'If InStr(iValue.ToString, ".") Then
                    '    iValue = s * 1000
                    'iValue = CInt((fractionalSecond - s) * 1000)
                    sUnits = "millisec"
                    'End If
                Else
                    sUnits = "s"
                End If
            End If

            If sUnits = "a" Then
                iValue = yr
            ElseIf sUnits = "wk" Then
                iValue = (yr * 52) + w
            ElseIf sUnits = "d" Then
                iValue = (yr * 52) + (w * 7) + d
            ElseIf sUnits = "h" Then
                iValue = (d * 24) + h

            ElseIf sUnits = "min" Then
                iValue = (((d * 24) + h) * 60) + m

            ElseIf sUnits = "s" Then
                iValue = (((((d * 24) + h) * 60) + m) * 60) + s
            ElseIf sUnits = "millisec" Then
                iValue = CDec(((((((d * 24) + h) * 60) + m) * 60) + fractionalSecond) * 1000)
            End If
        Else
            iValue = mo
        End If
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
'The Original Code is Duration.vb.
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
