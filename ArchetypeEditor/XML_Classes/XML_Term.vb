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
        Private a_Xml_Term As XMLParser.ARCHETYPE_TERM

        Public ReadOnly Property XML_Term() As XMLParser.ARCHETYPE_TERM
            Get
                SetItem("text", sText)
                SetItem("description", sDescription)

                If Not (sComment Is Nothing OrElse sComment = "") Then
                    SetItem("comment", sComment)
                End If

                For Each k As String In OtherAnnotations.Keys
                    SetItem(k, CStr(sAnnotations.Item(k)))
                Next

                Return a_Xml_Term
            End Get
        End Property

        Private Function GetItem(ByVal key As String) As String
            For Each di As XMLParser.StringDictionaryItem In a_Xml_Term.items
                If di.id = key Then
                    Return di.Value
                End If
            Next

            Return ""
        End Function

        Private Sub SetItem(ByVal Item As String, ByVal Value As String)
            Dim i As Integer
            Dim new_items() As XMLParser.StringDictionaryItem

            If Not a_Xml_Term.items Is Nothing Then
                ' set the value of the item
                For Each di As XMLParser.StringDictionaryItem In a_Xml_Term.items
                    If di.id = Item Then
                        di.Value = Value
                        Return  'FIXME: REMOVE SPAGHETTI CODE! ... return if it is found, but without this goto!
                    End If
                Next

                new_items = a_Xml_Term.items
                i = new_items.Length
                Array.Resize(new_items, i + 1)
            Else
                new_items = CType(Array.CreateInstance(GetType(XMLParser.StringDictionaryItem), 1), XMLParser.StringDictionaryItem())
                i = 0
            End If

            Dim new_di As New XMLParser.StringDictionaryItem()
            new_di.id = Item
            new_di.value = Value
            new_items(i) = new_di
            a_Xml_Term.items = new_items
        End Sub

        Sub New(ByVal ID As String)
            MyBase.new(ID)
            a_Xml_Term = New XMLParser.ARCHETYPE_TERM()
            a_Xml_Term.code = ID
        End Sub

        Sub New(ByVal a_Term As RmTerm)
            MyBase.New(a_Term.Code)
            sText = a_Term.Text
            sDescription = a_Term.Description
            sComment = a_Term.Comment
            sAnnotations = a_Term.OtherAnnotations
            a_Xml_Term = New XMLParser.ARCHETYPE_TERM()
            a_Xml_Term.items = CType(Array.CreateInstance(GetType(XMLParser.StringDictionaryItem), 1), XMLParser.StringDictionaryItem())
            a_Xml_Term.items(0) = New XMLParser.StringDictionaryItem
            a_Xml_Term.items(0).id = "text"
            a_Xml_Term.code = a_Term.Code
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
            a_Xml_Term = New XMLParser.ARCHETYPE_TERM()
            a_Xml_Term.code = code
            SetItem("text", sText)
            SetItem("description", sDescription)
        End Sub

        Sub New(ByVal an_xmlTerm As XMLParser.ARCHETYPE_TERM)
            MyBase.New(an_xmlTerm.code)
            a_Xml_Term = an_xmlTerm

            For Each di As XMLParser.StringDictionaryItem In an_xmlTerm.items
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
