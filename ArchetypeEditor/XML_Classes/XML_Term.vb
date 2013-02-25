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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/XML_Classes/XML_Term.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'
Option Strict On
Option Explicit On

Namespace ArchetypeEditor.XML_Classes

    Class XML_Term
        Inherits RmTerm
        Private mXmlTerm As XMLParser.ARCHETYPE_TERM

        Public ReadOnly Property XML_Term() As XMLParser.ARCHETYPE_TERM
            Get
                SetItem("text", sText)
                SetItem("description", sDescription)
                SetItem("comment", sComment)

                For Each k As String In OtherAnnotations.Keys
                    SetItem(k, CStr(sAnnotations.Item(k)))
                Next

                Return mXmlTerm
            End Get
        End Property

        Private Function GetItem(ByVal key As String) As String
            For Each di As XMLParser.StringDictionaryItem In mXmlTerm.items
                If di.id = key Then
                    Return di.Value
                End If
            Next

            Return ""
        End Function

        Private Sub SetItem(ByVal id As String, ByVal Value As String)
            If Not String.IsNullOrEmpty(Value) Then
                Dim i As Integer
                Dim items() As XMLParser.StringDictionaryItem = mXmlTerm.items
                Dim item As XMLParser.StringDictionaryItem = Nothing

                If items Is Nothing Then
                    i = 0
                    items = New XMLParser.StringDictionaryItem(1) {}
                Else
                    For Each di As XMLParser.StringDictionaryItem In items
                        If di.id = id Then
                            di.Value = Value
                            item = di
                        End If
                    Next

                    If item Is Nothing Then
                        i = items.Length
                        Array.Resize(items, i + 1)
                    End If
                End If

                If item Is Nothing Then
                    item = New XMLParser.StringDictionaryItem()
                    item.id = id
                    item.Value = Value
                    items(i) = item
                    mXmlTerm.items = items
                End If
            End If
        End Sub

        Sub New(ByVal ID As String)
            MyBase.new(ID)
            mXmlTerm = New XMLParser.ARCHETYPE_TERM()
            mXmlTerm.code = ID
        End Sub

        Sub New(ByVal term As RmTerm)
            MyBase.New(term.Code)
            sText = term.Text
            sDescription = term.Description
            sComment = term.Comment
            sAnnotations = term.OtherAnnotations
            mXmlTerm = New XMLParser.ARCHETYPE_TERM()
            mXmlTerm.items = CType(Array.CreateInstance(GetType(XMLParser.StringDictionaryItem), 1), XMLParser.StringDictionaryItem())
            mXmlTerm.items(0) = New XMLParser.StringDictionaryItem
            mXmlTerm.items(0).id = "text"
            mXmlTerm.code = term.Code
            SetItem("text", sText)
            SetItem("description", sDescription)
            SetItem("comment", sComment)

            For Each k As String In OtherAnnotations.Keys
                SetItem(k, CStr(sAnnotations.Item(k)))
            Next
        End Sub

        Sub New(ByVal code As String, ByVal text As String, ByVal description As String)
            MyBase.New(code)
            sText = text
            sDescription = description
            mXmlTerm = New XMLParser.ARCHETYPE_TERM()
            mXmlTerm.code = code
            SetItem("text", sText)
            SetItem("description", sDescription)
        End Sub

        Sub New(ByVal term As XMLParser.ARCHETYPE_TERM)
            MyBase.New(term.code)
            mXmlTerm = term

            For Each di As XMLParser.StringDictionaryItem In term.items
                Select Case di.id.ToLowerInvariant()
                    Case "text"
                        sText = di.Value
                    Case "description"
                        sDescription = di.Value
                    Case "comment"
                        sComment = di.Value
                    Case Else
                        OtherAnnotations.Add(di.id, di.Value)
                End Select
            Next
        End Sub

    End Class
End Namespace
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
'The Original Code is XML_Term.vb.
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
