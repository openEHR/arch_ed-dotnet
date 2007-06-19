'
'Written by Jana Graenz
'13.02.2007
'
' This Class, presenting a small user control, is a helper class for the WebSearchForm-class.
' It presents one archetype (of the resut set) including a table for a short description and 
' a button to open the archetype. The description will come from the ArchetypeFinder-Service 
' which means service-requests have to be sent to the ArchtypeFinder.
' The type of the description can be decided here, depedning on the parameters will be requested. See below.
' 
'
Option Explicit On

Public Class myArchetypeFromWeb
    Public myWebSearchForm As WebSearchForm

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    Public Sub setTerms(ByVal id As String)

        Dim ArchetypeService As ArchetypeFinderWebServiceURL.ArchetypeFinderBeanService = New ArchetypeFinderWebServiceURL.ArchetypeFinderBeanService()
        Dim ArchetypeDescTermsReturn As Array = Nothing 'JAR: 18APR07, EDT-35 Clean up compile time warnings
       
        Dim ArchetypeDescAvailable As Boolean = False
        Dim ArchetypeURL As String
        Dim language As String

        language = OceanArchetypeEditor.DefaultLanguageCode

        Try
            'The ADL-URL is required for locating and opening the archetype
            ArchetypeURL = ArchetypeService.getArchetypeADLURL(id)
            If Not ArchetypeURL.StartsWith("http:") Then
                btn_open.Hide()
            Else
                btn_open.Text = Filemanager.GetOpenEhrTerm(61, "Open")
                btn_open.Name = ArchetypeURL
            End If
        Catch ex As System.Web.Services.Protocols.SoapException
            btn_open.Hide()
        End Try


        ' DescriptionTerms-Array : this is a collection of all elements 
        ' we want to have in our resultset as description for an archetype
        ' they will be displayed in a table, similar to the Archetype Finders resultset!
        ' developer of the AE can define here maually what types of description is useful and should be shown
        ' It is necesaary to use extactly the property-names of the Ontology, as all is retrieved from the Ontology!
        Dim DescriptionTerms(2) As String

        DescriptionTerms(0) = "hasEHRClass"
        DescriptionTerms(1) = "archetypeDescription"
        DescriptionTerms(2) = "archetypePurpose"


        Try
            'The description helps the user to "identify" the archetype by seeing more information than just the ID.
            ArchetypeDescTermsReturn = ArchetypeService.getDescriptionForArchetype(id, language, DescriptionTerms)
            ArchetypeDescAvailable = True

        Catch ex As System.Web.Services.Protocols.SoapException
            ArchetypeDescAvailable = False
        End Try

        If ArchetypeDescAvailable Then
            Dim aTerm As String
            Dim y As Int32
            Dim row As Int32 = 0
            For y = 0 To ArchetypeDescTermsReturn.Length - 1
                aTerm = ArchetypeDescTermsReturn.GetValue(y).ToString

                Dim splittedTerms() As String
                Dim key As String
                Dim value As String

                splittedTerms = aTerm.Split(":")

                'new Labels for key and value in one line
                key = splittedTerms(0)
                value = splittedTerms(1)

                Dim keylabel As New System.Windows.Forms.Label
                keylabel.Name = key
                keylabel.Text = key & ":"
                keylabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                keylabel.TextAlign = ContentAlignment.MiddleLeft
                keylabel.ForeColor = Color.DarkBlue
                keylabel.Width = 120


                Dim valuelabel As New System.Windows.Forms.Label
                valuelabel.Name = value.ToString
                valuelabel.Text = value
                valuelabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                valuelabel.TextAlign = ContentAlignment.MiddleLeft
                valuelabel.ForeColor = Color.DarkBlue
                valuelabel.Width = 580

                valuelabel.AutoSize = True


                descriptionTable.Controls.Add(keylabel, 0, row)
                descriptionTable.Controls.Add(valuelabel, 1, row)

                row = row + 1

            Next y

            'set the Concept and the ID
            Dim term(0) As String
            Dim TermReturn As Array

            term(0) = "archetypeConcept"
            TermReturn = ArchetypeService.getDescriptionForArchetype(id, language, term)
            For y = 0 To TermReturn.Length - 1
                aTerm = TermReturn.GetValue(y).ToString
                Dim splittedTerms() As String
                Dim key As String
                Dim value As String
                splittedTerms = aTerm.Split(":")
                key = splittedTerms(0)
                value = splittedTerms(1)

                lbl_thisConcept.Text = value
            Next y


            term(0) = "archetypeID"
            TermReturn = ArchetypeService.getDescriptionForArchetype(id, language, term)
            For y = 0 To TermReturn.Length - 1
                aTerm = TermReturn.GetValue(y).ToString
                Dim splittedTerms() As String
                Dim key As String
                Dim value As String
                splittedTerms = aTerm.Split(":")
                key = splittedTerms(0)
                value = splittedTerms(1)
                lbl_thisID.Text = "(" & value & ")"
                lbl_thisID.SetBounds(lbl_thisConcept.Location.X + lbl_thisConcept.Width + 20, lbl_thisID.Location.Y, lbl_thisID.Width, lbl_thisID.Height)
                lbl_thisID.Refresh()

            Next y


            Me.Height = descriptionTable.Location.Y + descriptionTable.Height + 10
        ElseIf (ArchetypeDescAvailable = False) Then

            lbl_thisConcept.Text = id
            descriptionTable.Visible = False
            lbl_thisID.Visible = False
            lbl_thisID.SendToBack()
            Me.Height = 40


        End If

        Me.Controls.Add(descriptionTable)
        Me.Margin = New Padding(0, 0, 0, 5)
            Me.Refresh()


    End Sub


    Private Sub btn_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_open.Click
        Dim myID As Button = sender
        myWebSearchForm.setArchetypeIdToBeOpened(myID.Name)
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
