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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/GUI_Classes/Specialised_VB_classes/ArchetypeTreeNode.vb $"
'	revision:    "$LastChangedRevision: 01 $"
'	last_change: "$LastChangedDate: 2007-04-13 02:32:19 +1030 (Fri, 13 Apr 2007) $"
'
'JAR: 13APR07, EDT-32 RichTextBox displays Unicode as question marks.  Fudge forces it to display using the SelectedText property.    
Option Explicit On

Public Class RichTextBoxUnicode

    Public Enum RichTextDataType
        ONTOLOGY_TEXT = 1
        ONTOLOGY_DESC = 2
        ARCHETYPE_PURPOSE = 3
        ARCHETYPE_USE = 4
        ARCHETYPE_MISUSE = 5
    End Enum

    'Returns a Tag to be placed in the RTF file
    Public Shared Function CreateRichTextBoxTag(ByVal Code As String, ByVal TagType As RichTextDataType) As String
        Return Chr(253) & Code & Chr(254) & TagType & Chr(253)        
    End Function

    'Processes a Rich Edit control to replace Tags with values
    Public Shared Sub ProcessRichEditControl(ByVal RichEditControl As System.Windows.Forms.RichTextBox, ByVal FileManager As FileManagerLocal, ByVal TabPage As TabPageDescription)
        Dim Pos1 As Integer
        Dim Pos2 As Integer
        Dim tag As String
        Dim conceptCode As String
        Dim dataType As RichTextDataType
        Dim stringArray() As String
        Dim replaceText As String = ""
        Dim VM As Char 'Use value mark as it cannot be easily entered by a user
        Dim AM As Char 'Use attribute mark as it cannot be easily enetered by a user

        VM = Chr(253)
        AM = Chr(254)

        Try
            RichEditControl.Visible = False 'switch off visibility.  Otherwise displays progression of replace

            Pos1 = RichEditControl.Find(VM)
            Do While Pos1 > 0
                Pos2 = RichEditControl.Find(VM, Pos1 + 1, RichTextBoxFinds.MatchCase)
                tag = Mid(RichEditControl.Text, Pos1 + 1, Pos2 - Pos1 + 1)
                stringArray = Split(tag, AM)
                conceptCode = Replace(stringArray(0), VM, "")
                dataType = Replace(stringArray(1), VM, "")

                Select Case dataType
                    Case RichTextDataType.ONTOLOGY_DESC
                        replaceText = FileManager.OntologyManager.GetDescription(conceptCode)
                    Case RichTextDataType.ONTOLOGY_TEXT
                        replaceText = FileManager.OntologyManager.GetText(conceptCode)
                    Case RichTextDataType.ARCHETYPE_PURPOSE
                        replaceText = TabPage.txtPurpose.Text
                    Case RichTextDataType.ARCHETYPE_USE
                        replaceText = TabPage.txtUse.Text
                    Case RichTextDataType.ARCHETYPE_MISUSE
                        replaceText = TabPage.txtMisuse.Text
                End Select

                'RichEditControl.SelectedText does not replace text when string is empty (Replace needs to occur to remove VM/AM)
                If replaceText = "" Then replaceText = " "

                RichEditControl.Select(Pos1, Pos2 - Pos1 + 1)
                'Debug.Print("Replace text " & RichEditControl.SelectedText & " with " & replaceText & "From " & Pos1 & " to " & Pos2 - Pos1 + 1)
                RichEditControl.SelectedText = replaceText

                'prepare for next call 
                Pos1 = RichEditControl.Find(VM, Pos1 + 2, RichTextBoxFinds.MatchCase) 'Use Pos1 to start search.  .Text is dynamic and replace may shrink charpos below Pos2
            Loop
            RichEditControl.SelectionStart = 0
            RichEditControl.Visible = True
        Catch
            MessageBox.Show("Error processing Rich Text Box", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            RichEditControl.Visible = True
        End Try
    End Sub
End Class
