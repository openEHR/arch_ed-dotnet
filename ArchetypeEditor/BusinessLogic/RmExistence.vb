'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Jarrad Rigano"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/BusinessLogic/RmExistence.vb $"
'	revision:    "$LastChangedRevision: 63 $"
'	last_change: "$LastChangedDate: 2007-01-09 20:45:11 +1030 (Tue, 09 Jan 2007) $"
'
'
Option Strict On
Option Explicit On

Public Class RmExistence
    ' class for existence
    Private mMaxCount As Integer = 1
    Private mMinCount As Integer = 1
    Private mUnbounded As Boolean '= False
    Private mIncludeUpper As Boolean = True
    Private mIncludeLower As Boolean = True
    Private mIsDefault As Boolean = False

    'Private sCount As String

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
                    'setCount()
                End If
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
                    'setCount()
                End If
            End If
            mIsDefault = False
        End Set
    End Property

    Public Property IsUnbounded() As Boolean
        Get
            Return mUnbounded
        End Get
        Set(ByVal Value As Boolean)
            mUnbounded = Value
            If Not mUnbounded Then
                If mMinCount > mMaxCount Then
                    mMaxCount = mMinCount
                End If
            End If
            mIsDefault = False
            'setCount()
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
            mIncludeUpper = Value
            mIsDefault = False
        End Set
    End Property

    Public Property IncludeLower() As Boolean
        Get
            Return mIncludeLower
        End Get
        Set(ByVal Value As Boolean)
            mIncludeLower = Value
            mIsDefault = False
        End Set
    End Property

    Public Function Copy() As RmExistence
        Return New RmExistence(Me)
    End Function

    Public Overrides Function ToString() As String

        Dim max As String = "*"
        If Not mUnbounded Then
            '    max = "*"
            'Else
            max = mMaxCount.ToString
        End If

        Return mMinCount.ToString & ".." & max

    End Function

    Public Sub SetFromOpenEHRExistence(ByVal a_existence As openehr.common_libs.basic.INTERVAL_INTEGER_32)
        If a_existence Is Nothing Then
            Debug.Assert(True)
        End If

        If a_existence.upper_unbounded Then
            mUnbounded = True
        Else
            mUnbounded = False
            mMaxCount = a_existence.upper
        End If
        mMinCount = a_existence.lower

        mIsDefault = False

    End Sub

    Public ReadOnly Property XmlExistence() As XMLParser.IntervalOfInteger
        Get
            Dim an_interval As New XMLParser.IntervalOfInteger()

            an_interval.upper_included = True            

            If IsUnbounded Then
                an_interval.lower_included = IncludeLower
                an_interval.lower_includedSpecified = True 'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                an_interval.lower = MinCount
                an_interval.lowerSpecified = IncludeLower
            Else
                an_interval.lower_included = IncludeLower
                an_interval.lower_includedSpecified = True 'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                an_interval.lower = MinCount
                an_interval.lowerSpecified = IncludeLower

                an_interval.upper_included = IncludeUpper
                an_interval.upper_includedSpecified = True 'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                an_interval.upper = MaxCount
                an_interval.upperSpecified = IncludeUpper
            End If

            Return an_interval
        End Get
    End Property

    Public Sub SetFromXmlExistence(ByVal a_existence As XMLParser.IntervalOfInteger)

        'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        If a_existence Is Nothing Then
            a_existence = New XMLParser.IntervalOfInteger
        End If

        If a_existence.upperSpecified = True Then
            mUnbounded = False
            mMaxCount = a_existence.upper
        Else
            mUnbounded = True
        End If
        mMinCount = a_existence.lower

        mIsDefault = False
    End Sub

    Public Sub SetFromString(ByVal a_string As String)
        'Format is n..* or n..n
        ' or n (= n..n)

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
        Debug.Assert(lower <> Nothing And upper <> Nothing)
        mMinCount = lower
        mMaxCount = upper
        mUnbounded = False
    End Sub

    Sub New(ByVal lower As Integer)
        mMinCount = lower
        mMaxCount = 1
        '        mUnbounded = True
    End Sub

    Sub New()
        'default is 1..1
        mMaxCount = 1
        mMinCount = 1
        mUnbounded = False
        mIsDefault = True
    End Sub

    Sub New(ByVal aCount As RmExistence)
        mUnbounded = aCount.IsUnbounded
        mMaxCount = aCount.MaxCount
        mMinCount = aCount.MinCount
        mIncludeLower = aCount.IncludeLower
        mIncludeUpper = aCount.IncludeUpper
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
'The Original Code is RmExistence.vb.
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
