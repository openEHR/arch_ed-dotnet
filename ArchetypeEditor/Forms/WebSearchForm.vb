Imports System.Web.Services.Description

'
'Written by Jana Graenz
'13.02.2007
'
' WebSearchForm: window form to enable the search for archetypes from the web. 
' Is openend when user clicks one of the menu items designed for this form. "Open archetype from Web"
' Includes basically a textfield to enter the search string and - so far - radiobuttons to precise the search. Plus buttons for Search and a Reset of the Form.
' The result set of found archetypes will be organized in a table that lays on this WebSearchForm-Window. 
' in this table (archetypeTable) each cell is representing one archetype. 
' The Form for this archetype is a userControl called "myArchetypeFromWeb", represented by its own class.

'Option Strict On
Public Class WebSearchForm
    Inherits System.Windows.Forms.Form
    Private archetypeIdToBeOpened As String
    Private ArchetypeService As ArchetypeFinderWebServiceURL.ArchetypeFinderBeanService
    Private archetypeTable As New TableLayoutPanel
    Public chosen As Boolean = False

    ' Function handles all actions that happen when the user clicks the button "Search" within this form
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' Is the textfield empty, a messageBox asks the user to provide a search parameter first
        If (txtTerm.Text = "") Then
            Dim message As String = Filemanager.GetOpenEhrTerm(652, "Please enter your search parameter")
            MessageBox.Show(message)
        Else
            Dim ArchetypeIDs As Array
            Dim aTerm(0) As String

            ' The referenced ArchetypeFinderService provides us an object to access all available services of the ArchetypeFinder
            Try

              
                ArchetypeService = New ArchetypeFinderWebServiceURL.ArchetypeFinderBeanService()
                'get the required Archetypes depending on the radiobutton that is checked

                If rdbtn_id.Checked Then
                    ArchetypeIDs = ArchetypeService.getArchetypeIdsFromPartialId(txtTerm.Text.Trim)
                ElseIf rdbtn_any.Checked Then
                    aTerm(0) = "archetypeTermsCollect=" & txtTerm.Text.Trim
                    ArchetypeIDs = ArchetypeService.getArchetypeIds(aTerm)
                ElseIf rdbtn_con.Checked Then
                    aTerm(0) = "archetypeConcept=" & txtTerm.Text.Trim
                    ArchetypeIDs = ArchetypeService.getArchetypeIds(aTerm)
                ElseIf rdbtn_des.Checked Then
                    aTerm(0) = "archetypeDescription=" & txtTerm.Text.Trim
                    ArchetypeIDs = ArchetypeService.getArchetypeIds(aTerm)
                End If

                ' no archetypes found
                If (ArchetypeIDs Is Nothing) Then
                    lblNum.Text = Filemanager.GetOpenEhrTerm(654, "No archetype(s) found")
                    lblNum.Visible = True
                    lbl_found.Visible = False
                    archetypeTable.Controls.Clear()
                    archetypeTable.Visible = False
                    archetypeTable.Refresh()
                    Me.Height = 250
                    Me.Refresh()
                    ' archetypes were found and we set them as a resultset to the form
                Else
                    Me.setResults(ArchetypeIDs)
                End If

                'throw exception if ArchetypeFinder Web Services can not be invoked
            Catch ex As System.Net.WebException
                Dim message As String = Filemanager.GetOpenEhrTerm(659, "No network connection available.")
                MessageBox.Show(message)
                Me.Close()
                'Catch ex2 As System.Web.Services.Protocols.SoapException
                '    Dim message2 As String = Filemanager.GetOpenEhrTerm(654, "Service not available")
                '    MessageBox.Show(message2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    Me.Close()
            End Try


        End If


    End Sub

    ' the FinderService returned a result of archetype Ids, which we want to list now in a Table and show them to the user
    Private Sub setResults(ByVal ids As Array)

        'show the number of found archetypes above the list
        lblNum.Text = ids.Length
        lblNum.Visible = True
        lbl_found.Text = Filemanager.GetOpenEhrTerm(653, "archetype(s) found")
        lbl_found.Visible = True

        ' create a Table "archetypeTable" to list the archetypes
        archetypeTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
        archetypeTable.BackColor = Color.LightSteelBlue
        archetypeTable.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowOnly
        archetypeTable.AutoSize = True
        archetypeTable.Controls.Clear()
        archetypeTable.GrowStyle = TableLayoutPanelGrowStyle.AddRows
        archetypeTable.SetBounds(lblNum.Location.X, 230, 740, 30)
        
        ' every found archetype will be shown with a short description. 
        ' every archetype is represented by a little user control (vb-class myArchetypeFromWeb)
        Dim x As Int32
        For x = 0 To ids.Length - 1
            ' ID of every archetype
            Dim oneArch As New myArchetypeFromWeb
            Dim myID As String = ids.GetValue(x).ToString

            oneArch.myWebSearchForm = Me  ' so that the archetype "knows" where he belongs to
            oneArch.setTerms(myID)  'we set the Terms to the archetype-user control (show id, description and button for opening etc)

            'add the Archetype to the next cell of the table
            archetypeTable.Controls.Add(oneArch, 0, x)

        Next x
        Me.Controls.Add(archetypeTable)

        ' arrange the finished archetypeTable on the WindowsForm
        Me.Height = archetypeTable.Height + 280

        ' of the Table is big and long (many archetypes found) we enale scrolling
        If (archetypeTable.Height > 650) Then
            archetypeTable.Height = 650
            archetypeTable.AutoSize = False
            archetypeTable.AutoScroll = True
            Me.Height = archetypeTable.Height + 280
            'Me.SetBounds(200, 10, Me.Width, Me.Height)
            Me.CenterToScreen()
        End If

        archetypeTable.Padding = New Padding(0)
        archetypeTable.Refresh()
        archetypeTable.Visible = True
        Me.CenterToScreen()

        Me.Refresh()
    End Sub

    ' two helper functions to identify which archetype should be opened (after the user clicked the provided opening button)

    Public Function setArchetypeIdToBeOpened(ByVal thisID As String)
        archetypeIdToBeOpened = thisID

        chosen = True
        Me.Close()
      
    End Function

    Public Function getArchetypeIdTobeOpened() As String
        Return archetypeIdToBeOpened
    End Function

    ' this function resets the, clears the text-field and sets the bounds to its original state
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtTerm.Text = ""
        lblNum.Visible = False
        lbl_found.Visible = False

        archetypeTable.Controls.Clear()
        archetypeTable.Visible = False

        archetypeTable.Refresh()
        Me.Height = 240
        Me.Refresh()
    End Sub

    ' pressing enter while the WebSearchForm is active starts the search
    Private Sub WebSearchForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnSearch
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
'The Original Code is Designer.vb.
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
