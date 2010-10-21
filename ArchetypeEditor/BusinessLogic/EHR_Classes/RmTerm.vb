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
    Inherits Term
    Protected sDescription As String
    Protected sComment As String
    Protected sAnnotations As System.Collections.SortedList
    
    Property Description() As String
        Get
            Return sDescription
        End Get
        Set(ByVal Value As String)
            sDescription = Value
        End Set
    End Property

    Property Comment() As String
        Get
            Return sComment
        End Get
        Set(ByVal value As String)
            sComment = value
        End Set
    End Property

    ReadOnly Property OtherAnnotations() As System.Collections.SortedList
        Get
            If sAnnotations Is Nothing Then
                sAnnotations = New System.Collections.SortedList
            End If
            Return sAnnotations
        End Get
    End Property

    ReadOnly Property IsConstraint() As Boolean
        Get
            Dim result As Boolean = False

            If IsValidTermCode(Code) Then
                Dim s As String = Code.Substring(0, 2).ToLower(System.Globalization.CultureInfo.InvariantCulture)
                result = s = "ac"
            End If

            Return result
        End Get
    End Property

    Friend Shared Function IsValidTermCode(ByVal termCode As String) As Boolean
        Dim rx As New System.Text.RegularExpressions.Regex("a[ct](0\.[0-9]{1,4}|[0-9]{4})(\.[0-9]{1,3})*")
        Return Not termCode Is Nothing AndAlso rx.IsMatch(termCode)
    End Function

    Sub New(ByVal Code As String)
        MyBase.New(Code)
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
