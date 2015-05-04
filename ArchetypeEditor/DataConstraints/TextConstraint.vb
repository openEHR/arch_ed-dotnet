'
'	component:   "openEHR Archetype Project"
'	description: "Constraint for text values, text or coded text"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'

Option Strict On

Public Enum TextConstraintType
    Text
    Internal
    Terminology
End Enum

Public Class Constraint_Text
    Inherits Constraint_with_value

    Private mConstraintCode As String
    Private boolFixed As Boolean
    Private mTextConstraintType As TextConstraintType
    Private cpTerms As New CodePhrase
    Private mAssumed_value As String
    Private mTerminologyId As String

    Public Overrides Function Copy() As Constraint
        Dim result As New Constraint_Text
        result.mConstraintCode = mConstraintCode
        result.boolFixed = boolFixed
        result.mTextConstraintType = mTextConstraintType
        result.cpTerms.Phrase = cpTerms.Phrase
        result.mAssumed_value = mAssumed_value
        result.mTerminologyId = mTerminologyId
        result.HasAssumedValue = HasAssumedValue
        Return result
    End Function

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Text
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
                HasAssumedValue = True
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

    Public Property TypeOfTextConstraint() As TextConstraintType
        Get
            Return mTextConstraintType
        End Get
        Set(ByVal Value As TextConstraintType)
            ' HKF: 29 Sep 2008 - EDT-335, resolve AdlParser ERROR - Serialisation failed Found leaf term code 271 not defined in ontology 
            ' Ensure that Null Flavor code phrase terminology ID  of 'openehr' is not overwritten with local when TextConstraint set                
            'If Value = TextConstrainType.Internal Then
            If Value = TextConstraintType.Internal AndAlso AllowableValues.TerminologyID <> "openehr" Then
                AllowableValues.TerminologyID = "local"
            End If

            mTextConstraintType = Value
        End Set
    End Property

    Public Property ConstraintCode() As String
        Get
            Return mConstraintCode
        End Get
        Set(ByVal Value As String)
            mConstraintCode = Value
        End Set
    End Property

    Overrides Function ToString() As String
        Select Case mTextConstraintType
            Case TextConstraintType.Internal
                Return AE_Constants.Instance.InternalCodes
            Case TextConstraintType.Text
                Return AE_Constants.Instance.Text
            Case TextConstraintType.Terminology
                Return AE_Constants.Instance.Terminology
            Case Else
                Return MyBase.ToString()
        End Select
    End Function

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