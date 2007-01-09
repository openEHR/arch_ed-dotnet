'
'	component:   "openEHR Archetype Project"
'	description: "Constraint for text values, text or coded text"
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

Option Strict On

Public Enum TextConstrainType
    Internal
    Text
    Terminology
End Enum

Public Class Constraint_Text
    Inherits Constraint_with_value

    Private mConstraintCode As String
    Private boolFixed As Boolean
    Private mTextConstraintType As TextConstrainType
    Private cpTerms As New CodePhrase
    Private mAssumed_value As String

    Public Overrides Function Copy() As Constraint
        Dim t As New Constraint_Text

        t.boolFixed = Me.boolFixed
        t.cpTerms.Phrase = Me.cpTerms.Phrase
        t.mConstraintCode = Me.mConstraintCode
        t.mTextConstraintType = Me.mTextConstraintType
        t.boolFixed = Me.boolFixed
        t.HasAssumedValue = Me.HasAssumedValue
        t.mAssumed_value = Me.mAssumed_value
        Return t
    End Function

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Text
        End Get
    End Property
    Public Overrides Property AssumedValue() As Object
        Get
            If HasAssumedValue Then

                If Not cpTerms.Codes.Contains(mAssumed_value) Then
                    mAssumed_value = Nothing
                    HasAssumedValue = False
                End If
                Return mAssumed_value
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            Debug.Assert(TypeOf Value Is String)

            If cpTerms.Codes.Contains(CStr(Value)) Then
                mAssumed_value = CStr(Value)
            End If
        End Set
    End Property

    Public Property AllowableValues() As CodePhrase
        Get
            Return cpTerms
        End Get
        Set(ByVal Value As CodePhrase)
            cpTerms = Value
        End Set
    End Property
    Public Property Fixed() As Boolean
        Get
            Fixed = boolFixed
        End Get
        Set(ByVal Value As Boolean)
            boolFixed = Value
        End Set
    End Property
    Public Property TypeOfTextConstraint() As TextConstrainType
        Get
            Return mTextConstraintType
        End Get
        Set(ByVal Value As TextConstrainType)
            Select Case Value
                Case TextConstrainType.Internal
                    Me.AllowableValues.TerminologyID = "local"
                Case Else
                    Me.AllowableValues.TerminologyID = ""
            End Select
            mTextConstraintType = Value
        End Set
    End Property
    Public Property ConstraintCode() As String
        Get
            If mTextConstraintType = TextConstrainType.Terminology Then
                Return mConstraintCode
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mTextConstraintType = TextConstrainType.Terminology
            mConstraintCode = Value
        End Set
    End Property
    Overrides Function ToString() As String
        Select Case mTextConstraintType
            Case TextConstrainType.Internal
                Return AE_Constants.Instance.InternalCodes
            Case TextConstrainType.Text
                Return AE_Constants.Instance.Text
            Case TextConstrainType.Terminology
                Return AE_Constants.Instance.Terminology
            Case Else
                Return MyBase.ToString()
        End Select
    End Function

    Sub New()
        'default
        mTextConstraintType = TextConstrainType.Internal
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
'The Original Code is TextConstraint.vb.
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