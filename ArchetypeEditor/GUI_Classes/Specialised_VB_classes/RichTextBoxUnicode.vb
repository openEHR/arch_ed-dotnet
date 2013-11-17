'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Jarrad Rigano"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/GUI_Classes/Specialised_VB_classes/ArchetypeTreeNode.vb $"
'	revision:    "$LastChangedRevision: 01 $"
'	last_change: "$LastChangedDate: 2007-04-13 02:32:19 +1030 (Fri, 13 Apr 2007) $"
'

Option Explicit On

Public Class RichTextBoxUnicode

    Public Shared Function EscapedRtfString(ByVal s As String) As String
        Dim result As New System.Text.StringBuilder()

        If Not s Is Nothing Then
            For Each c As Char In s
                If c = "\"c Or c = "{"c Or c = "}"c Then
                    result.Append("\" + c)
                Else
                    Dim i As UInt32 = Convert.ToUInt32(c)

                    If &H20 <= i And i < &H7F Then
                        result.Append(c)
                    Else
                        result.Append("\u" + i.ToString + "?")
                    End If
                End If
            Next
        End If

        Return result.ToString()
    End Function

End Class
