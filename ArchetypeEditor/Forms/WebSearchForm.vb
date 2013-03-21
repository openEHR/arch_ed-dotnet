'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Jana Graenz, Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006,2007 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'
'Written by Jana Graenz, Sam Heard
'13.02.2007, 11.2007
'
' WebSearchForm: window form to enable the search for archetypes from the web. 
' Is openend when user clicks one of the menu items designed for this form. "Open archetype from Web"

Option Explicit On

Imports System.Web.Services.Description

Public Class WebSearchForm
    Inherits System.Windows.Forms.Form
    Private archetypeIdToBeOpened As String
    Private WithEvents ArchetypeService As org.openehr.ArchetypeFinderBean
    Private archetypeTable As New TableLayoutPanel
    Public chosen As Boolean = False

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtTerm.Text = "" Then
            Dim message As String = Filemanager.GetOpenEhrTerm(665, "Please enter your search parameter")
            MessageBox.Show(message)
            comboSearch.Focus()
        Else
            Dim ArchetypeIDs As String() = Nothing
            Dim aTerm(0) As String

            ' The referenced ArchetypeFinderService provides us an object to access all available services of the ArchetypeFinder
            Try
                Cursor = Cursors.WaitCursor
                listViewArchetypes.Clear()
                ArchetypeService = New org.openehr.ArchetypeFinderBean()

                Select Case comboSearch.SelectedIndex
                    Case 1 ' ArchetypeID
                        ArchetypeIDs = ArchetypeService.getArchetypeIdsFromPartialId(txtTerm.Text.Trim)
                    Case 2 ' Concept
                        aTerm(0) = "archetypeConcept=" & txtTerm.Text.Trim
                        ArchetypeIDs = ArchetypeService.getArchetypeIds(aTerm)
                    Case 3 ' Description
                        aTerm(0) = "archetypeDescription=" & txtTerm.Text.Trim
                        ArchetypeIDs = ArchetypeService.getArchetypeIds(aTerm)
                    Case Else
                        aTerm(0) = "archetypeTermsCollect=" & txtTerm.Text.Trim
                        ArchetypeIDs = ArchetypeService.getArchetypeIds(aTerm)
                End Select

                ' no archetypes found
                If ArchetypeIDs Is Nothing OrElse ArchetypeIDs(0) Is Nothing Then
                    lblNum.Text = "0"
                    lblNum.Visible = True
                    'archetypeTable.Controls.Clear()
                    'archetypeTable.Visible = False
                    'archetypeTable.Refresh()
                    'Me.Height = 250
                    'Me.Refresh()
                    '' archetypes were found and we set them as a resultset to the form
                Else
                    lblNum.Text = ArchetypeIDs.Length.ToString()
                    If Me.listViewArchetypes.Columns.Count = 0 Then
                        ' Create and initialize column headers for ListViewArchetypes.
                        Dim columnHeader0 As New ColumnHeader()
                        columnHeader0.Text = Filemanager.GetOpenEhrTerm(54, "Concept")
                        columnHeader0.Width = 250

                        Dim columnHeader1 As New ColumnHeader()
                        columnHeader1.Width = 100
                        columnHeader1.Text = Filemanager.GetOpenEhrTerm(53, "Version")

                        Dim columnHeader2 As New ColumnHeader()
                        columnHeader2.Text = "Id"
                        columnHeader2.Width = 400

                        ' Add the column headers to CustomersListView.
                        listViewArchetypes.Columns.AddRange(New ColumnHeader() {columnHeader0, columnHeader1, columnHeader2})
                    End If

                    listViewArchetypes.ShowGroups = True

                    Dim lvgAction As New ListViewGroup(Filemanager.GetOpenEhrTerm(556, "Action"))
                    Dim lvgEvaluation As New ListViewGroup(Filemanager.GetOpenEhrTerm(555, "Evaluation"))
                    Dim lvgInstruction As New ListViewGroup(Filemanager.GetOpenEhrTerm(557, "Instruction"))
                    Dim lvgObservation As New ListViewGroup(Filemanager.GetOpenEhrTerm(554, "Observation"))
                    Dim lvgAdmin As New ListViewGroup(Filemanager.GetOpenEhrTerm(560, "Administration entry"))
                    Dim lvgStructure As New ListViewGroup(Filemanager.GetOpenEhrTerm(85, "Structure"))
                    Dim lvgCluster As New ListViewGroup(Filemanager.GetOpenEhrTerm(313, "Cluster"))
                    Dim lvgElement As New ListViewGroup(Filemanager.GetOpenEhrTerm(567, "Element"))
                    Dim lvgSection As New ListViewGroup(Filemanager.GetOpenEhrTerm(314, "Section"))
                    Dim lvgComposition As New ListViewGroup(Filemanager.GetOpenEhrTerm(561, "Composition"))

                    listViewArchetypes.Groups.Add(lvgComposition)
                    listViewArchetypes.Groups.Add(lvgSection)
                    listViewArchetypes.Groups.Add(lvgObservation)
                    listViewArchetypes.Groups.Add(lvgEvaluation)
                    listViewArchetypes.Groups.Add(lvgInstruction)
                    listViewArchetypes.Groups.Add(lvgAction)
                    listViewArchetypes.Groups.Add(lvgAdmin)
                    listViewArchetypes.Groups.Add(lvgStructure)
                    listViewArchetypes.Groups.Add(lvgCluster)
                    listViewArchetypes.Groups.Add(lvgElement)

                    For Each id As String In ArchetypeIDs
                        Dim imageIndex As Integer
                        Dim lvg As ListViewGroup
                        Dim archId As New ArchetypeID(id)

                        Select Case archId.ReferenceModelEntity
                            Case StructureType.OBSERVATION
                                imageIndex = 4
                                lvg = lvgObservation
                            Case StructureType.EVALUATION
                                imageIndex = 2
                                lvg = lvgEvaluation
                            Case StructureType.ACTION
                                imageIndex = 1
                                lvg = lvgAction
                            Case StructureType.INSTRUCTION
                                imageIndex = 3
                                lvg = lvgInstruction
                            Case StructureType.Cluster
                                imageIndex = 5
                                lvg = lvgCluster
                            Case StructureType.SECTION
                                imageIndex = 7
                                lvg = lvgSection
                            Case StructureType.COMPOSITION
                                imageIndex = 6
                                lvg = lvgComposition
                            Case StructureType.Element
                                lvg = lvgElement
                            Case Else
                                imageIndex = 0
                                lvg = lvgStructure
                        End Select

                        'If Not listViewArchetypes.Groups.Contains(lvg) Then
                        '    listViewArchetypes.Groups.Add(lvg)
                        'End If

                        Dim s As String() = New String() {archId.Concept.Replace("_", " "), archId.VersionAsString(), id}
                        listViewArchetypes.Items.Add(New ListViewItem(s, imageIndex, lvg))
                    Next

                    listViewArchetypes.Focus()
                    listViewArchetypes.Items(0).Selected = True
                    AcceptButton = butOK
                End If
            Catch ex As System.Net.WebException
                Dim message As String = Filemanager.GetOpenEhrTerm(664, "Network error") + Environment.NewLine + ex.Message
                MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            Catch ex2 As System.Web.Services.Protocols.SoapException
                Dim message As String = Filemanager.GetOpenEhrTerm(664, "Network error") + Environment.NewLine + ex2.Message
                MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            Finally
                Cursor = Cursors.Default
            End Try
        End If
    End Sub

    ' the FinderService returned a result of archetype Ids, which we want to list now in a Table and show them to the user
    Private Sub setResults(ByVal ids As Array)

        'show the number of found archetypes above the list
        lblNum.Text = String.Format("{0} {1}", ids.Length, Filemanager.GetOpenEhrTerm(653, "archetype(s) found"))
        lblNum.Visible = True

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

        Controls.Add(archetypeTable)

        ' arrange the finished archetypeTable on the WindowsForm
        Height = archetypeTable.Height + 280

        ' of the Table is big and long (many archetypes found) we enale scrolling
        If (archetypeTable.Height > 650) Then
            archetypeTable.Height = 650
            archetypeTable.AutoSize = False
            archetypeTable.AutoScroll = True
            Height = archetypeTable.Height + 280
            CenterToScreen()
        End If

        archetypeTable.Padding = New Padding(0)
        archetypeTable.Refresh()
        archetypeTable.Visible = True
        CenterToScreen()
        Refresh()
    End Sub

    Public ReadOnly Property getArchetypeIdTobeOpened() As String
        Get
            Return archetypeIdToBeOpened.Trim()
        End Get
    End Property

    Private Sub txtTerm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTerm.TextChanged
        If Not AcceptButton Is btnSearch Then
            AcceptButton = btnSearch
        End If
    End Sub

    Private returnString As String

    Private AsyncOpCompleted As Boolean

    Private Sub ArchetypeWebService_GetADL(ByVal sender As Object, ByVal e As org.openehr.getArchetypeInADLCompletedEventArgs) Handles ArchetypeService.getArchetypeInADLCompleted
        If e.Error Is Nothing Then
            returnString = e.Result
            AsyncOpCompleted = True
        Else
            returnString = True
            Throw New Exception(e.Error.Message)
        End If
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        If listViewArchetypes.SelectedItems.Count > 0 Then
            archetypeIdToBeOpened = listViewArchetypes.SelectedItems(0).SubItems(2).Text
            Dim tempPath, downloadPath As String
            tempPath = System.IO.Path.GetTempPath

            Try
                ProgressBar1.Show()
                Cursor = Cursors.WaitCursor
                Application.DoEvents()

                ArchetypeService.getArchetypeInADLAsync(archetypeIdToBeOpened)

                Do While (Not AsyncOpCompleted)
                    System.Threading.Thread.Sleep(10)
                    Application.DoEvents()
                Loop

                'archetypeservice.

                downloadPath = System.IO.Path.Combine(tempPath, String.Format("{0}.adl", archetypeIdToBeOpened))
                'Try
                '    request = System.Net.WebRequest.Create(fileUrl)
                '    'CHANGED SRH - says use the default
                '    'request.Proxy = System.Net.WebProxy.GetDefaultProxy
                '    request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials ' to avoid eventually Proxy-Troubles
                '    response = CType(request.GetResponse(), Net.HttpWebResponse)
                'Catch ex As Exception
                '    Return False
                'End Try

                If returnString <> "" Then
                    Dim sw As New System.IO.StreamWriter(downloadPath)

                    'Dim dataStream As IO.Stream = response.GetResponseStream()
                    '' Open the stream using a StreamReader for easy access.
                    'Dim reader As New IO.StreamReader(dataStream)
                    '' Read the content.
                    'Dim responseFromServer As String = reader.ReadToEnd()
                    ' Display the content.
                    'sw.WriteLine(responseFromServer)
                    sw.Write(returnString)
                    '' Cleanup the streams and the response.
                    'reader.Close()
                    'dataStream.Close()
                    'response.Close()
                    sw.Close()

                    ' the web archetype has been written into a local temporary file!
                    archetypeIdToBeOpened = downloadPath
                    DialogResult = Windows.Forms.DialogResult.OK
                End If
            Catch ex As Exception
                Dim message As String = String.Format("{0} :{1}", Filemanager.GetOpenEhrTerm(664, "Network error"), ex.Message)
                MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                archetypeIdToBeOpened = ""
                DialogResult = Windows.Forms.DialogResult.Abort
            Finally
                ProgressBar1.Hide()
                Cursor = Cursors.Default
            End Try
        Else
            archetypeIdToBeOpened = ""
            Me.DialogResult = Windows.Forms.DialogResult.Abort
        End If

        Close()
    End Sub

    Private Sub listViewArchetypes_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listViewArchetypes.DoubleClick
        butOK_Click(sender, e)
    End Sub

    Private Sub WebSearchForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtTerm.Focus()
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
'Sam Heard, Ocean Informatics (www.oceaninformatics.com).
'Portions created by the Initial Developer are Copyright (C) 2007
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
