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
'

Option Explicit On
Option Strict On

Friend Class Duration
    Private mIsoDuration As String
    Private mIsoUnits As String
    Private mDuration As Decimal

    Public Property ISO_duration() As String
        Get
            Return mIsoDuration
        End Get
        Set(ByVal Value As String)
            mIsoDuration = Value.Trim("| ".ToCharArray())
            ProcessIso()
        End Set
    End Property

    Public ReadOnly Property IsoUnits() As String
        Get
            Return mIsoUnits
        End Get
    End Property

    Public ReadOnly Property Duration() As Decimal
        Get
            Return mDuration
        End Get
    End Property

    Public Sub SetIsoDuration(ByVal value As Decimal, ByVal units As String)
        mDuration = value
        mIsoUnits = Main.ISO_TimeUnits.GetIsoUnitForDuration(units)

        If Not Main.ISO_TimeUnits.IsValidIsoUnit(mIsoUnits) Then
            mIsoUnits = ""
            mIsoDuration = ""
        Else
            If mIsoUnits = "d" Or mIsoUnits = "mo" Or mIsoUnits = "wk" Or mIsoUnits = "a" Then
                mIsoDuration = "P"
            Else
                mIsoDuration = "PT"
            End If

            If mIsoUnits = "millisec" Then
                mIsoDuration += (mDuration / 1000).ToString + "S"
            Else
                mIsoDuration += mDuration.ToString

                Select Case mIsoUnits
                    Case "min", "mo"
                        mIsoDuration += "M"
                    Case "a"
                        mIsoDuration += "Y"
                    Case "wk"
                        mIsoDuration += "W"
                    Case Else 'D, H, S
                        mIsoDuration += mIsoUnits.ToUpperInvariant
                End Select
            End If
        End If
    End Sub

    Private Sub ProcessIso()
        Dim ymwd As String

        ' drop the leading P and convert to lower
        Dim str As String = mIsoDuration.Substring(1).ToLowerInvariant
        Dim y() As String = str.Split(("t".ToCharArray))

        If y.Length > 1 Then
            ymwd = y(0)
            str = y(1)
        Else
            ymwd = str
            str = ""
        End If

        Dim yr As Integer
        Dim mo As Integer
        Dim w As Integer
        Dim d As Integer

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
                mIsoUnits = "mo"
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

        Dim h As Integer
        Dim m As Integer
        Dim s As Long
        Dim fractionalSecond As Double

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
                s = CLng(Val(y(0)))
                fractionalSecond = Val(y(0)) - s
            End If
        End If

        If mIsoUnits <> "mo" Then
            If mIsoDuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("y") Then
                mIsoUnits = "a"
            ElseIf mIsoDuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("w") Then
                mIsoUnits = "wk"
            ElseIf mIsoDuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("d") Then
                mIsoUnits = "d"
            ElseIf mIsoDuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("h") Then
                mIsoUnits = "h"
            ElseIf mIsoDuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("m") Then
                mIsoUnits = "min"
            ElseIf mIsoDuration.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith("s") Then
                If InStr(fractionalSecond.ToString, ".") > 0 Then
                    ' this means there is a decimal point and the period must have been in millisecs
                    mIsoUnits = "millisec"
                Else
                    mIsoUnits = "s"
                End If
            End If

            If mIsoUnits = "a" Then
                mDuration = yr
            ElseIf mIsoUnits = "wk" Then
                mDuration = (yr * 52) + w
            ElseIf mIsoUnits = "d" Then
                mDuration = (yr * 52) + (w * 7) + d
            ElseIf mIsoUnits = "h" Then
                mDuration = (d * 24) + h
            ElseIf mIsoUnits = "min" Then
                mDuration = (((d * 24) + h) * 60) + m
            ElseIf mIsoUnits = "s" Then
                mDuration = (((((d * 24) + h) * 60) + m) * 60) + s
            ElseIf mIsoUnits = "millisec" Then
                mDuration = CDec((((((((d * 24) + h) * 60) + m) * 60) + s + fractionalSecond) * 1000))
            End If
        Else
            mDuration = mo
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
