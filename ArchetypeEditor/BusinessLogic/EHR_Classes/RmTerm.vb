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

Public Class RmTerm
    Protected sCode As String
    Protected sText As String 'New String("")
    Protected sDescription As String 'New String("")
    Protected sLanguageCode As String 'New String("")

    ReadOnly Property Code() As String
        Get
            Return sCode
        End Get
    End Property

    Property Language() As String
        Get
            Return sLanguageCode
        End Get
        Set(ByVal Value As String)
            sLanguageCode = Value
        End Set
    End Property

    Property Text() As String
        Get
            Return sText
        End Get
        Set(ByVal Value As String)
            sText = Value
        End Set
    End Property

    Property Description() As String
        Get
            Return sDescription
        End Get
        Set(ByVal Value As String)
            sDescription = Value
        End Set
    End Property

    ReadOnly Property isConstraint() As Boolean
        Get
            Dim s As String
            ' cannot use toupper or lower safely with internationalisation

            If isValidTermCode(Me.Code) Then
                s = Me.Code.Substring(0, 2).ToLower(System.Globalization.CultureInfo.InvariantCulture)
                If s = "at" Then
                    Return False
                ElseIf s = "ac" Then
                    Return True
                End If
            Else
                Debug.Assert(False)
            End If

        End Get
    End Property

    Friend Shared Function isValidTermCode(ByVal a_term_code) As Boolean
        Dim rx As New System.Text.RegularExpressions.Regex("a[ct](0\.[0-9]{1,4}|[0-9]{4})(\.[0-9]{1,3})*")
        Return rx.Match(a_term_code).Success()
    End Function

    Overrides Function ToString() As String
        Return sText
    End Function

    Sub New(ByVal Code As String)
        sCode = Code
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
'The Original Code is RmTerm.vb.
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
