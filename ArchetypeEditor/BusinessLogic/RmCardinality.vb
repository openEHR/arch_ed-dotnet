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
Option Explicit On

Public Class RmCardinality
    ' class for counts and cardinality
    Private mMaxCount As Integer
    Private mMinCount As Integer
    Private mUnbounded As Boolean
    Private mIncludeUpper As Boolean = True
    Private mIncludeLower As Boolean = True
    Private mOrdered As Boolean = False
    Private mIsDefault As Boolean = False

    Public Event Updated As CardinalityUpdatedEventHandler
    Public Delegate Sub CardinalityUpdatedEventHandler(ByVal sender As Object, ByVal e As EventArgs)

    Public Property MaxCount() As Integer
        Get
            Return mMaxCount
        End Get
        Set(ByVal Value As Integer)
            mUnbounded = False

            If Value <> mMaxCount Then
                If Value >= 1 Then
                    mMaxCount = Value

                    If Value < mMinCount Then
                        mMinCount = Value
                    End If
                End If

                'SRH: 11 Jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
                RaiseEvent Updated(Me, New EventArgs)
            End If

            mIsDefault = False
        End Set
    End Property

    Public ReadOnly Property IsDefault() As Boolean
        Get
            Return mIsDefault
        End Get
    End Property

    Public Property MinCount() As Integer
        Get
            Return mMinCount
        End Get
        Set(ByVal Value As Integer)
            If Value <> mMinCount Then
                If Value >= 0 Then
                    mMinCount = Value

                    If Value > mMaxCount Then
                        mMaxCount = Value
                    End If
                End If

                'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
                RaiseEvent Updated(Me, New EventArgs)
            End If

            mIsDefault = False
        End Set
    End Property

    Public Property IsUnbounded() As Boolean
        Get
            Return mUnbounded
        End Get
        Set(ByVal Value As Boolean)
            If mUnbounded <> Value Then
                mUnbounded = Value

                If Not mUnbounded Then
                    If mMinCount > mMaxCount Then
                        mMaxCount = mMinCount
                    End If
                End If

                mIsDefault = False

                'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
                RaiseEvent Updated(Me, New EventArgs)
            End If
        End Set
    End Property

    Public Property IncludeUpper() As Boolean
        Get
            If Not mUnbounded Then
                Return mIncludeUpper
            Else
                Return False
            End If
        End Get
        Set(ByVal Value As Boolean)
            If mIncludeUpper <> Value Then
                mIncludeUpper = Value
                mIsDefault = False

                'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
                RaiseEvent Updated(Me, New EventArgs)
            End If
        End Set
    End Property

    Public Property IncludeLower() As Boolean
        Get
            Return mIncludeLower
        End Get
        Set(ByVal Value As Boolean)
            If mIncludeLower <> Value Then
                mIncludeLower = Value
                mIsDefault = False

                'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
                RaiseEvent Updated(Me, New EventArgs)
            End If
        End Set
    End Property

    Public Property Ordered() As Boolean
        Get
            Return mOrdered
        End Get
        Set(ByVal Value As Boolean)
            mOrdered = Value
            mIsDefault = False
        End Set
    End Property

    Public ReadOnly Property IsMultiple() As Boolean
        Get
            Return IsUnbounded Or MaxCount > 1
        End Get
    End Property

    Public Function Copy() As RmCardinality
        Return New RmCardinality(Me)
    End Function

    Public Overrides Function ToString() As String
        Dim max As String = "*"

        If Not mUnbounded Then
            max = mMaxCount.ToString
        End If

        Return mMinCount.ToString & ".." & max
    End Function

    Public Sub SetFromOpenEHRCardinality(ByVal a_cardinality As openehr.openehr.am.archetype.constraint_model.CARDINALITY)
        If a_cardinality.interval.upper_unbounded Then
            mUnbounded = True
        Else
            mUnbounded = False
            mMaxCount = a_cardinality.interval.upper
        End If

        mMinCount = a_cardinality.interval.lower
        mOrdered = a_cardinality.is_ordered
        mIsDefault = False
    End Sub

    Public Sub SetFromXmlCardinality(ByVal a_cardinality As XMLParser.CARDINALITY)
        If a_cardinality.interval.upperSpecified = True Then
            mUnbounded = False
            mMaxCount = a_cardinality.interval.upper
        Else
            mUnbounded = True
        End If

        mMinCount = a_cardinality.interval.lower
        mOrdered = a_cardinality.is_ordered
        mIsDefault = False
    End Sub

    Public Sub SetFromString(ByVal a_string As String)
        'Format is n..* or n..n or n (= n..n)

        Dim i As Integer = a_string.IndexOf("..")

        If i > -1 Then
            If a_string.EndsWith("..*") Then
                mUnbounded = True
            Else
                mUnbounded = False
                mMaxCount = Integer.Parse(a_string.Substring(a_string.LastIndexOf(".") + 1))
            End If

            mMinCount = Integer.Parse(a_string.Substring(0, a_string.IndexOf(".")))
        Else
            mMinCount = Integer.Parse(a_string)
            mMaxCount = Integer.Parse(a_string)
        End If

        mIsDefault = False
    End Sub

    Sub New(ByVal lower As Integer, ByVal upper As Integer)
        mMinCount = lower
        mMaxCount = upper
        mUnbounded = False
    End Sub

    Sub New(ByVal lower As Integer)
        mMinCount = lower
        mMaxCount = 1
        mUnbounded = True
    End Sub

    Sub New()
        'default is 0..1
        mMaxCount = 1
        mMinCount = 0
        mUnbounded = False
        mIsDefault = True
    End Sub

    Sub New(ByVal count As RmCardinality)
        mUnbounded = count.IsUnbounded
        mMaxCount = count.MaxCount
        mMinCount = count.MinCount
        mIncludeLower = count.IncludeLower
        mIncludeUpper = count.IncludeUpper
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
'The Original Code is RmCardinality.vb.
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
