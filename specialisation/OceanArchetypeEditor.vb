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
'	file:        "$Source: source/vb.net/archetype_editor/SCCS/s.ArchetypeEditor.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class OceanArchetypeEditor

    ' ArchetypeEditor Singleton
    Private Shared mInstance As OceanArchetypeEditor
    Private Shared mMenu As Menu

    Public Shared ISO_TimeUnits As New TimeUnits

    Public Shared ReadOnly Property Instance() As OceanArchetypeEditor
        Get
            If mInstance Is Nothing Then
                mInstance = New OceanArchetypeEditor
            End If

            Return mInstance
        End Get
    End Property

    Private mOptions As Options
    ReadOnly Property Options() As Options
        Get
            If mOptions Is Nothing Then
                mOptions = New Options
            End If
            Return mOptions
        End Get
    End Property

    ReadOnly Property MainMenu() As Menu
        Get
            Return mMenu
        End Get
    End Property

    Private Shared mDefaultLanguageCodeSet As String = "ISO_639-1"

    Public Shared ReadOnly Property DefaultLanguageCodeSet() As String
        Get
            Return mDefaultLanguageCodeSet
        End Get
    End Property

    Private Shared mDefaultLanguageCode As String

    Public Shared ReadOnly Property DefaultLanguageCode() As String
        Get
            Debug.Assert(mDefaultLanguageCode <> "", "DefaultLanguageCode not set")
            Return mDefaultLanguageCode
        End Get
    End Property

    Private Shared mSpecificLanguageCode As String
    Public Shared ReadOnly Property SpecificLanguageCode() As String
        Get
            Debug.Assert(mSpecificLanguageCode <> "", "SpecificLanguageCode not set")
            Return mSpecificLanguageCode
        End Get
    End Property

    Private mConstraint As Constraint
    Public Property TempConstraint() As Constraint
        Get
            Return mConstraint
        End Get
        Set(ByVal Value As Constraint)
            mConstraint = Value
        End Set
    End Property

    Protected Sub New()

        mDataSet = New DataSet("DesignerDataSet")

    End Sub

    Private mDataSet As DataSet
    Public ReadOnly Property DataSet() As DataSet
        Get
            Return mDataSet
        End Get
    End Property


    Private Function MakeUnitsTable() As DataTable
        Dim tTable As DataTable

        ' Now only used as a backup if there are no PropertyUnits XML files

        ' Create a new DataTable titled 'TermDefinitions' or 'ConstraintDefinitions'
        tTable = New DataTable("Unit")
        ' Add three column objects to the table.
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.Int32")
        idColumn.ColumnName = "property_id"
        idColumn.AutoIncrement = False
        tTable.Columns.Add(idColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        tTable.Columns.Add(TextColumn)
        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = System.Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        tTable.Columns.Add(DescriptionColumn)
        ' Return the new DataTable.
        Dim keys(1) As DataColumn
        keys(0) = idColumn
        keys(1) = TextColumn
        tTable.PrimaryKey = keys

        MakeUnitsTable = tTable

    End Function

    Public Function MakeTerminologyDataTable() As DataTable
        Dim mDataTable As DataTable

        mDataTable = New DataTable("DataTable")
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.Int32")
        idColumn.ColumnName = "Id"
        mDataTable.Columns.Add(idColumn)
        Dim CodeColumn As DataColumn = New DataColumn
        CodeColumn.DataType = System.Type.GetType("System.String")
        CodeColumn.ColumnName = "Code"
        mDataTable.Columns.Add(CodeColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        mDataTable.Columns.Add(TextColumn)

        Dim Terminologies As DataRow() _
                = Filemanager.Master.OntologyManager.GetTerminologyIdentifiers

        mDataTable.DefaultView.Sort = "Text"

        For i As Integer = 0 To Terminologies.Length - 1
            Dim newRow As DataRow = mDataTable.NewRow()
            newRow("Code") = Terminologies(i).Item(0)
            newRow("Text") = Terminologies(i).Item(1)
            mDataTable.Rows.Add(newRow)
        Next

        Return mDataTable
    End Function


    Private Function MakePhysicalPropertiesTable() As DataTable
        Dim tTable As DataTable

        ' Now only used as a backup if there are no PropertyUnits XML files

        ' Create a new DataTable titled 'TermDefinitions' or 'ConstraintDefinitions'
        tTable = New DataTable("Property")
        ' Add three column objects to the table.
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = System.Type.GetType("System.Int32")
        idColumn.ColumnName = "id"
        idColumn.AutoIncrement = True
        tTable.Columns.Add(idColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = System.Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        tTable.Columns.Add(TextColumn)
        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = System.Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        tTable.Columns.Add(DescriptionColumn)
        ' Return the new DataTable.
        'Dim keys(1) As DataColumn
        Dim keys(0) As DataColumn
        keys(0) = idColumn
        'keys(1) = TextColumn
        tTable.PrimaryKey = keys

        Return tTable

    End Function

    Public Function AddTerminology() As Boolean
        ' add the language codes 
        Dim frm As New Choose
        frm.Set_Single()
        frm.PrepareDataTable_for_List(1)

        Dim Terminologies As DataRow() _
                = Filemanager.Master.OntologyManager.GetTerminologyIdentifiers
        frm.DTab_1.DefaultView.Sort = "Text"

        For i As Integer = 0 To Terminologies.Length - 1
            Dim newRow As DataRow = frm.DTab_1.NewRow()
            newRow("Code") = Terminologies(i).Item(0)
            newRow("Text") = Terminologies(i).Item(1)
            frm.DTab_1.Rows.Add(newRow)
        Next

        frm.ListChoose.DataSource = frm.DTab_1
        frm.ListChoose.DisplayMember = "Text"
        frm.ListChoose.ValueMember = "Code"

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' check it is not a terminology added previously
            Dim term As String = CStr(frm.ListChoose.SelectedValue)
            Dim description As String = frm.ListChoose.Text

            If Not Filemanager.Master.OntologyManager.TerminologiesTable.Select("Terminology = '" & term & "'").Length = 0 Then
                Beep()
                Debug.Assert(False)

                Return False
            End If

            ' there is already a language in the archetype
            If (MessageBox.Show(AE_Constants.Instance.NewTerminology & description, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK) Then
                ' add to the terminologies
                Filemanager.Master.OntologyManager.AddTerminology(term, description)
            End If

        Else
            Return False

        End If

        Return True
    End Function

    Public Function GetInput(ByVal label As String, ByVal parentForm As Form) As String
        Dim frm As New InputForm
        Dim s As String = ""

        frm.lblInput.Text = label
        frm.Text = AE_Constants.Instance.MessageBoxCaption

        If frm.ShowDialog(parentForm) = Windows.Forms.DialogResult.OK Then
            s = frm.txtInput.Text
        End If
        frm.Close()

        Return s
    End Function

    Public Function GetInput(ByVal label_1 As String, ByVal label_2 As String, ByVal parentForm As Form) As String()
        Dim frm As New InputForm
        Dim s(1) As String

        frm.lblInput.Text = label_1
        frm.LblInput2.Text = label_2
        frm.Text = AE_Constants.Instance.MessageBoxCaption

        If frm.ShowDialog(parentForm) = Windows.Forms.DialogResult.OK Then
            s(0) = frm.txtInput.Text
            s(1) = frm.txtInput2.Text
            If s(1) = "" Then
                'avoids null in xml read
                s(1) = "*"
            End If
        End If
        frm.Close()
        Return s
    End Function

    Public Function GetInput(ByVal a_term As RmTerm, ByVal parentForm As Form) As String()
        Dim frm As New InputForm
        Dim s(1) As String

        frm.lblInput.Text = AE_Constants.Instance.Text
        frm.LblInput2.Text = AE_Constants.Instance.Description
        frm.Text = AE_Constants.Instance.MessageBoxCaption
        ' add the current term text and description
        frm.txtInput.Text = a_term.Text
        frm.txtInput2.Text = a_term.Description

        If frm.ShowDialog(parentForm) = Windows.Forms.DialogResult.OK Then
            s(0) = frm.txtInput.Text
            If s(0) <> "" Then
                a_term.Text = s(0)
            End If
            s(1) = frm.txtInput2.Text
            If s(1) = "" Then
                'avoids null in xml read
                s(1) = "*"
            End If
            a_term.Description = s(1)
        End If
        frm.Close()
        Return s
    End Function

    Public Function ChooseInternal(ByVal a_file_manager As FileManagerLocal) As String()
        Try
            Dim Frm As New Choose
            Dim selected_rows As DataRow()
            Dim i As Integer

            Frm.Set_Single()

            Frm.PrepareDataTable_for_List(1)

            selected_rows = a_file_manager.OntologyManager.TermDefinitionTable.Select(String.Format("Id = '{0}'", _
                        a_file_manager.OntologyManager.LanguageCode))

            For i = 0 To selected_rows.Length - 1
                Dim New_row As DataRow
                New_row = Frm.DTab_1.NewRow
                New_row(1) = selected_rows(i).Item(1)
                New_row(2) = selected_rows(i).Item(2)
                Frm.DTab_1.Rows.Add(New_row)
            Next
            Frm.ListChoose.SelectionMode = SelectionMode.MultiExtended
            Frm.ListChoose.DataSource = Frm.DTab_1
            Frm.ListChoose.DisplayMember = "Text"
            Frm.ListChoose.ValueMember = "Code"

            If Frm.ShowDialog() = Windows.Forms.DialogResult.OK Then

                If Frm.ListChoose.SelectedIndices.Count > 0 Then
                    Dim s(Frm.ListChoose.SelectedItems.Count - 1) As String
                    For i = 0 To Frm.ListChoose.SelectedItems.Count - 1
                        'Change Sam Heard 2004-06-11
                        'Change from datarow to datarowview
                        Debug.Assert(TypeOf Frm.ListChoose.SelectedItems(i) Is DataRowView)
                        Dim selectedRow As DataRowView = CType(Frm.ListChoose.SelectedItems(i), DataRowView)
                        s(i) = CStr(selectedRow.Item("Code"))
                    Next

                    Return s
                End If

            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Return Nothing

    End Function

    Public Function ChooseInternal(ByVal an_array_of_elements As ArchetypeElement(), Optional ByVal AlreadyAdded As CodePhrase = Nothing) As ArchetypeElement()
        Try
            Dim Frm As New Choose
            Dim i As Integer

            Frm.Set_Single()


            For i = 0 To an_array_of_elements.Length - 1
                If Not AlreadyAdded.Codes.Contains(an_array_of_elements(i).NodeId) Then
                    Frm.ListChoose.Items.Add(an_array_of_elements(i))
                End If
            Next

            If Frm.ShowDialog() = Windows.Forms.DialogResult.OK Then

                If Frm.ListChoose.SelectedIndices.Count > 0 Then
                    Dim a_e(Frm.ListChoose.SelectedIndices.Count - 1) As ArchetypeElement
                    Frm.ListChoose.SelectedItems.CopyTo(a_e, 0)
                    Return a_e
                End If
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Return Nothing

    End Function


    Public Function CountInString(ByVal String1 As String, ByVal string2 As String) As Integer ' returns the number of occurences
        Dim counter As Integer
        Dim i As Integer = InStr(String1, string2)
        While i > 0
            counter += 1
            String1 = Mid(String1, i + 1)
            i = InStr(String1, string2)
        End While

        Return counter

    End Function

    Public Function GetSpecialisationChain(ByVal Id As String, ByVal a_filemanager As FileManagerLocal) As CodeAndTerm()
        Dim i, start, n As Integer
        n = CountInString(Id, ".")
        Dim ct(n) As CodeAndTerm
        Dim counter As Integer
        start = 1
        For i = 0 To n
            Dim a_ct As New CodeAndTerm
            start = InStr(start, Id, ".")
            If start > 0 Then
                a_ct.Code = Mid(Id, 1, start - 1)
            Else
                a_ct.Code = Id
            End If

            'catch the fragments that represent no term at that level of specialisation
            ' these will be AT0.# etc
            ' or AT0003.0.#
            If a_ct.Code = "AT0" Or a_ct.Code = "at0" Or a_ct.Code.EndsWith(".0") Then
                ReDim ct(ct.Length - 2)
            Else
                Dim a_Term As RmTerm

                a_Term = a_filemanager.OntologyManager.GetTerm(a_ct.Code)
                a_ct.Text = a_Term.Text
                ct(counter) = a_ct
                counter += 1
            End If
            start += 1
        Next
        Return ct
    End Function

    Public ReadOnly Property UnitsTable() As DataTable
        Get
            If Not mDataSet.Tables.Contains("Unit") Then
                MakeQuantityTables()
            End If

            Return mDataSet.Tables.Item("Unit")
        End Get
    End Property

    Public ReadOnly Property PhysicalPropertiesTable() As DataTable
        Get
            If Not mDataSet.Tables.Contains("Property") Then
                MakeQuantityTables()
            End If

            Return mDataSet.Tables.Item("Property")
        End Get
    End Property

    Private Sub MakeQuantityTables()

        'CHANGED Sam Heard 2004-09-05
        ' Added XML file for units and properties

        Try
            mDataSet.ReadXmlSchema(Application.StartupPath & "\PropertyUnits\PropertyUnits.xsd")
            mDataSet.ReadXml(Application.StartupPath & "\PropertyUnits\PropertyUnitData.xml")

            ' Set up the primary key
            Dim keys(0) As DataColumn
            keys(0) = mDataSet.Tables("Property").Columns(0) ' property id
            mDataSet.Tables("Property").PrimaryKey = keys
            mDataSet.Tables("Property").DefaultView.Sort = "Text"

            If mDefaultLanguageCode <> "en" Then
                'translate the properties
                ' 0 = Id
                ' 1 = text
                ' 2 = openEHR code
                ' 3 = translation
                ' 4 = language code
                For Each dr As DataRow In mDataSet.Tables("Property").Rows
                    dr.BeginEdit()
                    dr(3) = dr(1)
                    dr(1) = Filemanager.GetOpenEhrTerm(CInt(dr(2)), CStr(dr(1)), mDefaultLanguageCode)
                    dr(4) = mDefaultLanguageCode
                    dr.EndEdit()
                Next
            End If

            ' Set up the primary key
            ReDim keys(1)
            keys(0) = mDataSet.Tables("Unit").Columns(0) ' property id
            keys(1) = mDataSet.Tables("Unit").Columns(1)
            mDataSet.Tables("Unit").PrimaryKey = keys
            mDataSet.Tables("Unit").DefaultView.Sort = "Text"

            Dim new_relation As New DataRelation("PhysPropUnits", mDataSet.Tables("Property").Columns(0), _
                    mDataSet.Tables("Unit").Columns(0))
            mDataSet.Relations.Add(new_relation)

        Catch e As Exception
            ' emergency if data is not available as file
            Dim physicalProperties As DataTable = MakePhysicalPropertiesTable()
            Dim units As DataTable = MakeUnitsTable()
            mDataSet.Tables.Add(physicalProperties)
            mDataSet.Tables.Add(units)
            Dim new_relation As New DataRelation("PhysPropUnits", physicalProperties.Columns(0), _
                units.Columns(0))
            mDataSet.Relations.Add(new_relation)
            PopulatePhysPropUnitTables(units, physicalProperties)
        End Try

        '
    End Sub

    Public Function GetIdForPropertyOpenEhrCode(ByVal openEhrCode As Integer) As Integer
        Try
            Dim dr As DataRow() = mDataSet.Tables("Property").Select("openEHR = " & Convert.ToString(openEhrCode))
            If Not dr Is Nothing AndAlso dr.Length = 1 Then
                Return CInt(dr(0).Item(0))
            Else
                Return -1
            End If
        Catch e As Exception
            Return -1
        End Try
    End Function
    Private Sub PopulatePhysPropUnitTables(ByVal Units As DataTable, _
            ByVal PhysicalProperties As DataTable)

        Dim id As Integer

        ' Now only used as a backup if there are no PropertyUnits XML files

        id = AddPhysicalProperty(PhysicalProperties, "?")
        AddUnit(Units, id, "?")

        id = AddPhysicalProperty(PhysicalProperties, "concentration")
        AddUnit(Units, id, "%")
        AddUnit(Units, id, "{MASS/VOLUME}")
        AddUnit(Units, id, "{QUALIFIED REAL/VOLUME}")
        AddUnit(Units, id, "{VOLUME/VOLUME}")

        id = AddPhysicalProperty(PhysicalProperties, "energy")
        AddUnit(Units, id, "J")
        AddUnit(Units, id, "kCal")
        AddUnit(Units, id, "Cal")


        id = AddPhysicalProperty(PhysicalProperties, "length")
        AddUnit(Units, id, "nanom")
        AddUnit(Units, id, "mm")
        AddUnit(Units, id, "cm")
        AddUnit(Units, id, "m")
        AddUnit(Units, id, "Km")
        AddUnit(Units, id, "1000th Inch")
        AddUnit(Units, id, "Inch")
        AddUnit(Units, id, "ft")
        AddUnit(Units, id, "yd")
        AddUnit(Units, id, "Mile")

        id = AddPhysicalProperty(PhysicalProperties, "qualified real")
        AddUnit(Units, id, "")
        AddUnit(Units, id, "x 10^3")
        AddUnit(Units, id, "x 10^6")
        AddUnit(Units, id, "x 10^9")
        AddUnit(Units, id, "x 10^12")

        id = AddPhysicalProperty(PhysicalProperties, "mass")
        AddUnit(Units, id, "IU")
        AddUnit(Units, id, "mIU")
        AddUnit(Units, id, "mmol")
        AddUnit(Units, id, "pmol")
        AddUnit(Units, id, "nanogm")
        AddUnit(Units, id, "microgm")
        AddUnit(Units, id, "mgm")
        AddUnit(Units, id, "gm")
        AddUnit(Units, id, "Kg")
        AddUnit(Units, id, "Tonne")
        AddUnit(Units, id, "oz")
        AddUnit(Units, id, "lb")
        AddUnit(Units, id, "Stone")
        AddUnit(Units, id, "Hundred Wt")
        AddUnit(Units, id, "Ton")

        id = AddPhysicalProperty(PhysicalProperties, "pressure")
        AddUnit(Units, id, "mm[Hg]")
        AddUnit(Units, id, "cm[H20]")
        AddUnit(Units, id, "pascal")
        AddUnit(Units, id, "millibar")
        AddUnit(Units, id, "kPa")
        AddUnit(Units, id, "{MASS/LENGTH^2}")

        id = AddPhysicalProperty(PhysicalProperties, "rate")
        AddUnit(Units, id, "{QUALIFIED REAL/TIME}")
        AddUnit(Units, id, "{VOLUME/TIME}")
        AddUnit(Units, id, "{LENGTH/TIME}")

        id = AddPhysicalProperty(PhysicalProperties, "temperature")
        AddUnit(Units, id, "C")
        AddUnit(Units, id, "F")

        id = AddPhysicalProperty(PhysicalProperties, "time")
        AddUnit(Units, id, "microsec")
        AddUnit(Units, id, "millisec")
        AddUnit(Units, id, "sec")
        AddUnit(Units, id, "min")
        AddUnit(Units, id, "hr")
        AddUnit(Units, id, "day")
        AddUnit(Units, id, "mth")
        AddUnit(Units, id, "yr")

        id = AddPhysicalProperty(PhysicalProperties, "volume")
        AddUnit(Units, id, "mm^3")
        AddUnit(Units, id, "cc")
        AddUnit(Units, id, "dl")
        AddUnit(Units, id, "l")
        AddUnit(Units, id, "Kl")
        AddUnit(Units, id, "Fl oz")
        AddUnit(Units, id, "Pint")
        AddUnit(Units, id, "Imp gallon")
        AddUnit(Units, id, "Gallon")

        id = AddPhysicalProperty(PhysicalProperties, "work")
        AddUnit(Units, id, "Watt")
        AddUnit(Units, id, "{ENERGY/TIME}")

    End Sub

    Private Function AddPhysicalProperty(ByVal PhysicalProperties As DataTable, _
            ByVal Txt As String) As Integer

        Dim rw As DataRow = PhysicalProperties.NewRow()
        rw("Text") = Txt
        Dim i As Integer = CInt(rw(0))         ' get the id to return
        PhysicalProperties.Rows.Add(rw)

        Return i
    End Function

    Private Sub AddUnit(ByVal Units As DataTable, ByVal Id As Integer, _
            ByVal Txt As String, Optional ByVal Desc As String = "")

        Dim rw As DataRow = Units.NewRow()

        rw("Text") = Txt
        rw("Description") = Desc

        rw(0) = Id ' get the id to return
        Units.Rows.Add(rw)
    End Sub

    Shared Function IsDefaultLanguageRightToLeft() As Boolean
        Return IsLanguageRightToLeft(mDefaultLanguageCode)
    End Function

    Shared Function IsLanguageRightToLeft(ByVal a_language_code As String) As Boolean
        Select Case a_language_code
            Case "fa"
                Return True
        End Select
    End Function

    Shared Sub main(ByVal CmdArgs() As String)

#Const TEST_LANGUAGE_TRANSLATION = False

#If Not TEST_LANGUAGE_TRANSLATION Then

        'default language as two letter code e.g. "en"
        mDefaultLanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName

        ' specific as four letter e.g. "en-AU"
        mSpecificLanguageCode = System.Globalization.CultureInfo.CurrentCulture.Name

#Else
        'FOR TESTING LANGUAGE TRANSLATION
        mDefaultLanguageCode = "fa"
        mSpecificLanguageCode = "fa"

       'mDefaultLanguageCode = "pt-br"
       'mSpecificLanguageCode = "pt-br"

        'mDefaultLanguageCode = "da"
        'mSpecificLanguageCode = "da"

        'mDefaultLanguageCode = "nl"
        'mSpecificLanguageCode = "nl"

        'mDefaultLanguageCode = "de"
        'mSpecificLanguageCode = "de"
#End If
        Dim frm As New Designer

        If CmdArgs.Length > 0 Then
            frm.ArchetypeToOpen = CmdArgs(0)
        End If

        mMenu = frm.MainMenu

        If IsLanguageRightToLeft(mDefaultLanguageCode) Then
            frm.RightToLeft = RightToLeft.Yes
        End If

        Try            
            frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("This program has encountered an error and will shut down" & vbCrLf & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            frm.Close()
        End Try
    End Sub

    Shared Sub Reflect(ByVal a_control As Control)
        For Each Ctrl As Control In a_control.Controls

            If Ctrl.Dock = DockStyle.Left Then
                Ctrl.Dock = DockStyle.Right
            ElseIf Ctrl.Dock = DockStyle.Right Then
                Ctrl.Dock = DockStyle.Left
            ElseIf Ctrl.Dock = DockStyle.None Then
                Ctrl.Location = New Drawing.Point(Ctrl.Parent.Width - (Ctrl.Location.X + Ctrl.Width), Ctrl.Location.Y)

                If Not (TypeOf Ctrl Is Windows.Forms.TabPage) And _
                Not (TypeOf Ctrl Is Crownwood.Magic.Controls.TabPage) And _
                Not (TypeOf Ctrl Is Windows.Forms.TabControl) Then
                    If Ctrl.Anchor = 5 Then
                        Debug.WriteLine(Ctrl.Name)
                        Ctrl.Anchor = CType(9, Windows.Forms.AnchorStyles)
                    End If
                End If
            End If

            If Ctrl.Controls.Count > 0 Then
                Reflect(Ctrl)
            End If
        Next
    End Sub

End Class

Public Structure CodeAndTerm
    Private sCode As String
    Private sText As String
    Property Code() As String
        Get
            Return sCode
        End Get
        Set(ByVal Value As String)
            sCode = Value
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
End Structure



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
'The Original Code is ArchetypeEditor.vb.
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

