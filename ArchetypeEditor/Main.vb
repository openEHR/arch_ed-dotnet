'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Option Strict On

Public Class Main

    ' ArchetypeEditor Singleton
    Protected Shared mInstance As Main

    Public Shared ReadOnly Property Instance() As Main
        Get
            Return mInstance
        End Get
    End Property

    Public Shared ISO_TimeUnits As New TimeUnits

    Shared Sub Main(ByVal args() As String)
        mInstance = New Main
        Instance.Logo = My.Resources.OpenEHR
        Instance.Splash = My.Resources.OpenEHRSplash
        Instance.Run(args)
    End Sub

    Public Sub Run(ByVal args() As String)
        ' Enable XP visual style in order to show groups in the web lookup form.
        ' IMPORTANT! This must be the first call in the application; otherwise the icons do not display on the main toolbar!
        Application.EnableVisualStyles()
        Options.LoadConfiguration()

        Dim archetypePath As String = ""
        Dim exportDirectory As String = ""
        Dim exportFormats As New Collections.Generic.List(Of String)
        Dim invalidArgs As String = ""

        For Each arg As String In args
            If IO.Path.GetExtension(arg.ToLowerInvariant) = ".adl" Or IO.Path.GetExtension(arg.ToLowerInvariant) = ".xml" Then
                archetypePath = arg
            ElseIf arg.ToLowerInvariant = "-exportadl" Or arg.ToLowerInvariant = "-exportxml" Then
                If archetypePath = "" Then
                    invalidArgs = invalidArgs + " " + arg + " must come after [archetypefiles]"
                Else
                    exportFormats.Add(arg.ToLowerInvariant.Substring(7))
                End If
            ElseIf exportFormats.Count > 0 And exportDirectory = "" Then
                exportDirectory = IO.Path.GetFullPath(arg)
            ElseIf arg.Length >= 2 Then
                If arg.StartsWith("/l:") Then
                    arg = arg.Substring(3)
                End If

                mDefaultLanguageCode = arg.Substring(0, 2)
                mSpecificLanguageCode = arg
            Else
                invalidArgs = invalidArgs + " " + arg
            End If
        Next

        AE_Constants.Create(DefaultLanguageCode)

        If invalidArgs <> "" Then
            DisplayUsage(invalidArgs)
        ElseIf exportFormats.Count > 0 Then
            Export(archetypePath, exportDirectory, exportFormats)
        Else
            ShowSplash()
            Dim frm As New Designer
            frm.ArchetypeToOpen = archetypePath

            'Pick up any Autosave files that are lying around.
            Dim files As IO.FileInfo() = New IO.DirectoryInfo(Options.ApplicationDataDirectory).GetFiles("Recovery-*.*")

            If Not files Is Nothing AndAlso files.Length > 0 Then
                Dim recoverFrm As New Recovery
                recoverFrm.chkListRecovery.Items.AddRange(files)
                recoverFrm.ShowDialog()

                For Each f As IO.FileInfo In recoverFrm.chkListRecovery.Items
                    If recoverFrm.chkListRecovery.CheckedItems.Contains(f) Then
                        If String.IsNullOrEmpty(frm.ArchetypeToOpen) Then
                            frm.ArchetypeToOpen = f.FullName
                        Else
                            Dim info As New ProcessStartInfo
                            info.FileName = f.FullName
                            info.WorkingDirectory = Application.StartupPath
                            Process.Start(info)
                        End If
                    Else
                        f.Delete()
                    End If
                Next
            End If

            If IsLanguageRightToLeft(mDefaultLanguageCode) Then
                frm.RightToLeft = RightToLeft.Yes
            End If

            Try
                frm.ShowDialog()
            Catch ex As Exception
                MessageBox.Show("This program has encountered an error and will shut down - a recovery file will be available on restart" & vbCrLf & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                frm.Close()
            End Try
        End If
    End Sub

    Protected Sub DisplayUsage(ByVal invalidArgs As String)
        Dim message As String = "Invalid arguments:" + invalidArgs + Environment.NewLine + _
            "Usage: " + IO.Path.GetFileName(Application.ExecutablePath) + " [languagecode] [archetypefiles] [-exportadl] [-exportxml] [exportdirectory]" + Environment.NewLine + _
            "Examples:" + Environment.NewLine + _
            "* Opening an ADL file with German as current language: de cluster\openEHR-EHR-CLUSTER.xx.v1.adl" + Environment.NewLine + _
            "* Opening an XML file with US English as current language: en-us cluster\openEHR-EHR-CLUSTER.xx.v1.xml" + Environment.NewLine + _
            "* Export all ADL files in-place: C:\knowledge\*.adl -exportadl" + Environment.NewLine + _
            "* Export all ADL files as ADL and XML to a directory: C:\knowledge\*.adl -exportadl -exportxml C:\export" + Environment.NewLine

        MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Protected Sub Export(ByVal archetypePath As String, ByVal exportDirectory As String, ByVal formats As Collections.Generic.List(Of String))
        Dim frm As New Designer
        Dim searchPattern As String = IO.Path.GetFileName(archetypePath)
        Dim sourceDirectory As String = IO.Path.GetDirectoryName(archetypePath)

        If sourceDirectory = "" Then
            sourceDirectory = "."
        End If

        sourceDirectory = IO.Path.GetFullPath(sourceDirectory)

        If exportDirectory = "" Then
            exportDirectory = sourceDirectory
        End If

        For Each filename As String In IO.Directory.GetFiles(sourceDirectory, searchPattern, IO.SearchOption.AllDirectories)
            Dim exportFilename As String = IO.Path.Combine(exportDirectory, filename.Remove(0, sourceDirectory.Length + 1))
            IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(exportFilename))
            frm.OpenArchetype(filename)

            For Each format As String In formats
                Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(Filemanager.Master.SerialisedArchetype(format))
                Dim stream As New IO.FileStream(IO.Path.ChangeExtension(exportFilename, format), IO.FileMode.Create)

                Try
                    stream.Write(bytes, 0, bytes.Length)
                Finally
                    stream.Close()
                End Try
            Next
        Next
    End Sub

    Protected Sub ShowSplash()
        Dim f As New Splash
        f.ShowAsSplash()
        Application.DoEvents()
    End Sub

    Private mLogo As Bitmap

    Public Property Logo() As Bitmap
        Get
            Return mLogo
        End Get
        Set(ByVal value As Bitmap)
            mLogo = value
        End Set
    End Property

    Private mSplash As Bitmap

    Public Property Splash() As Bitmap
        Get
            Return mSplash
        End Get
        Set(ByVal value As Bitmap)
            mSplash = value
        End Set
    End Property

    Private mTerminologyLookup As TerminologyLookup.TerminologySelection

    Public Property TerminologyLookup() As TerminologyLookup.TerminologySelection
        Get
            Return mTerminologyLookup
        End Get
        Set(ByVal value As TerminologyLookup.TerminologySelection)
            mTerminologyLookup = value
        End Set
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

    Private mDefaultLanguageCodeSet As String = "ISO_639-1"

    Public ReadOnly Property DefaultLanguageCodeSet() As String
        Get
            Return mDefaultLanguageCodeSet
        End Get
    End Property

    Private mDefaultLanguageCode As String

    Public ReadOnly Property DefaultLanguageCode() As String
        Get
            Debug.Assert(mDefaultLanguageCode <> "", "DefaultLanguageCode not set")
            Return mDefaultLanguageCode
        End Get
    End Property

    Private mSpecificLanguageCode As String

    Public ReadOnly Property SpecificLanguageCode() As String
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

    Public Sub New()
        mDataSet = New DataSet("DesignerDataSet")

        'default language as two letter code e.g. "en"
        mDefaultLanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName

        ' specific as four letter e.g. "en-au"
        mSpecificLanguageCode = System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag.ToLowerInvariant()
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
        idColumn.DataType = Type.GetType("System.Int32")
        idColumn.ColumnName = "property_id"
        idColumn.AutoIncrement = False
        tTable.Columns.Add(idColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        tTable.Columns.Add(TextColumn)
        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        tTable.Columns.Add(DescriptionColumn)
        ' Return the new DataTable.
        Dim keys(1) As DataColumn
        keys(0) = idColumn
        keys(1) = TextColumn
        tTable.PrimaryKey = keys

        MakeUnitsTable = tTable
    End Function

    Private Function MakePhysicalPropertiesTable() As DataTable
        Dim result As DataTable

        ' Now only used as a backup if there are no PropertyUnits XML files

        ' Create a new DataTable titled 'TermDefinitions' or 'ConstraintDefinitions'
        result = New DataTable("Property")
        ' Add three column objects to the table.
        Dim idColumn As DataColumn = New DataColumn
        idColumn.DataType = Type.GetType("System.Int32")
        idColumn.ColumnName = "id"
        idColumn.AutoIncrement = True
        result.Columns.Add(idColumn)
        Dim TextColumn As DataColumn = New DataColumn
        TextColumn.DataType = Type.GetType("System.String")
        TextColumn.ColumnName = "Text"
        result.Columns.Add(TextColumn)
        Dim DescriptionColumn As DataColumn = New DataColumn
        DescriptionColumn.DataType = Type.GetType("System.String")
        DescriptionColumn.ColumnName = "Description"
        result.Columns.Add(DescriptionColumn)
        ' Return the new DataTable.
        'Dim keys(1) As DataColumn
        Dim keys(0) As DataColumn
        keys(0) = idColumn
        'keys(1) = TextColumn
        result.PrimaryKey = keys

        Return result
    End Function

    Public Function AddTerminology() As Boolean
        ' add the language codes 
        Dim result As Boolean = False
        Dim frm As New Choose
        frm.Set_Single()
        frm.PrepareDataTable_for_List(1)

        Dim terminologies As DataRow() = Filemanager.Master.OntologyManager.GetTerminologyIdentifiers
        frm.DTab_1.DefaultView.Sort = "Text"

        For i As Integer = 0 To terminologies.Length - 1
            Dim newRow As DataRow = frm.DTab_1.NewRow()
            newRow("Code") = terminologies(i).Item(0)
            newRow("Text") = terminologies(i).Item(1)
            frm.DTab_1.Rows.Add(newRow)
        Next

        frm.ListChoose.DataSource = frm.DTab_1
        frm.ListChoose.DisplayMember = "Text"
        frm.ListChoose.ValueMember = "Code"

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' check it is not a terminology added previously
            Dim term As String = CStr(frm.ListChoose.SelectedValue)
            Dim description As String = frm.ListChoose.Text

            If Filemanager.Master.OntologyManager.TerminologiesTable.Select("Terminology = '" & term & "'").Length > 0 Then
                Beep()
            Else
                result = True

                If MessageBox.Show(AE_Constants.Instance.NewTerminology & description, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    Filemanager.Master.OntologyManager.AddTerminology(term)
                End If
            End If
        End If

        Return result
    End Function

    Public Function GetInput(ByVal label As String, ByVal parentForm As Form, Optional ByVal defaultValue As String = "") As String
        Dim result As String = ""

        Dim frm As New InputForm
        frm.lblInput.Text = label

        If defaultValue <> "" Then
            frm.txtInput.Text = defaultValue
            frm.txtInput.SelectAll()
        End If

        frm.Text = AE_Constants.Instance.MessageBoxCaption

        If frm.ShowDialog(parentForm) = DialogResult.OK Then
            result = frm.txtInput.Text
        End If

        frm.Close()
        Return result
    End Function

    Public Function GetInput(ByVal label_1 As String, ByVal label_2 As String, ByVal parentForm As Form) As String()
        Dim result(1) As String
        Dim frm As New InputForm

        frm.lblInput.Text = label_1
        frm.LblInput2.Text = label_2
        frm.Text = AE_Constants.Instance.MessageBoxCaption

        If frm.ShowDialog(parentForm) = DialogResult.OK Then
            result(0) = frm.txtInput.Text
            result(1) = frm.txtInput2.Text

            If result(1) = "" Then
                'avoids null in xml read
                result(1) = "*"
            End If
        End If

        frm.Close()
        Return result
    End Function

    Public Function GetInput(ByVal a_term As RmTerm, ByVal parentForm As Form) As String()
        Dim result(1) As String

        Dim frm As New InputForm
        frm.lblInput.Text = AE_Constants.Instance.Text
        frm.LblInput2.Text = AE_Constants.Instance.Description
        frm.Text = AE_Constants.Instance.MessageBoxCaption
        ' add the current term text and description
        frm.txtInput.Text = a_term.Text
        frm.txtInput2.Text = a_term.Description

        If frm.ShowDialog(parentForm) = Windows.Forms.DialogResult.OK Then
            result(0) = frm.txtInput.Text

            If result(0) <> "" Then
                a_term.Text = result(0)
            End If

            result(1) = frm.txtInput2.Text

            If result(1) = "" Then
                'avoids null in xml read
                result(1) = "*"
            End If

            a_term.Description = result(1)
        End If

        frm.Close()
        Return result
    End Function

    Public Function ChooseInternal(ByVal fileManager As FileManagerLocal, ByVal excludedTerms As String()) As String()
        Try
            Dim dialog As New Choose
            dialog.Set_Single()
            dialog.PrepareDataTable_for_List(1)

            Dim selectedRows As DataRow() = fileManager.OntologyManager.TermDefinitionTable.Select(String.Format("Id = '{0}'", fileManager.OntologyManager.LanguageCode))

            For i As Integer = 0 To selectedRows.Length - 1
                'Ensure it is not an orphan term in the ontology
                Dim s As String = CStr(selectedRows(i).Item(1))

                If fileManager.OntologyManager.Ontology.HasTermCode(s) AndAlso Not Array.IndexOf(excludedTerms, s) > -1 Then
                    Dim row As DataRow
                    row = dialog.DTab_1.NewRow
                    row(1) = s
                    row(2) = selectedRows(i).Item(2)
                    dialog.DTab_1.Rows.Add(row)
                End If
            Next

            dialog.ListChoose.SelectionMode = SelectionMode.MultiExtended
            dialog.ListChoose.DataSource = dialog.DTab_1
            dialog.ListChoose.DisplayMember = "Text"
            dialog.ListChoose.ValueMember = "Code"

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.ListChoose.SelectedIndices.Count > 0 Then
                    Dim result(dialog.ListChoose.SelectedItems.Count - 1) As String

                    For i As Integer = 0 To dialog.ListChoose.SelectedItems.Count - 1
                        Dim selectedRow As DataRowView = CType(dialog.ListChoose.SelectedItems(i), DataRowView)
                        result(i) = CStr(selectedRow.Item("Code"))
                    Next

                    Return result
                End If
            End If
        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Return Nothing
    End Function

    Public Function ChooseInternal(ByVal an_array_of_elements As ArchetypeElement(), Optional ByVal AlreadyAdded As CodePhrase = Nothing) As ArchetypeElement()
        Try
            Dim dialog As New Choose
            dialog.Set_Single()

            For i As Integer = 0 To an_array_of_elements.Length - 1
                If Not AlreadyAdded.Codes.Contains(an_array_of_elements(i).NodeId) Then
                    dialog.ListChoose.Items.Add(an_array_of_elements(i))
                End If
            Next

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then

                If dialog.ListChoose.SelectedIndices.Count > 0 Then
                    Dim result(dialog.ListChoose.SelectedIndices.Count - 1) As ArchetypeElement
                    dialog.ListChoose.SelectedItems.CopyTo(result, 0)
                    Return result
                End If
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Return Nothing
    End Function

    Public Function GetSpecialisationChain(ByVal nodeId As String, ByVal fileManager As FileManagerLocal) As CodeAndTerm()
        Dim result(nodeId.Length) As CodeAndTerm
        Dim length As Integer = 0
        Dim start As Integer = 1

        While start > 0
            Dim ct As New CodeAndTerm
            start = InStr(start, nodeId, ".")

            If start > 0 Then
                ct.Code = Mid(nodeId, 1, start - 1)
                start += 1
            Else
                ct.Code = nodeId
            End If

            'catch the fragments that represent no term at that level of specialisation
            ' these will be AT0.# or AT0003.0.#, etc.
            If ct.Code <> "AT0" And ct.Code <> "at0" And Not ct.Code.EndsWith(".0") Then
                ct.Text = fileManager.OntologyManager.GetTerm(ct.Code).Text
                result(length) = ct
                length += 1
            End If
        End While

        ReDim Preserve result(length - 1)
        Return result
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
                    'dr(3) = dr(1)
                    dr(1) = Filemanager.GetOpenEhrTerm(CInt(dr(2)), CStr(dr(1)), mDefaultLanguageCode)
                    'dr(4) = mDefaultLanguageCode
                    dr.EndEdit()
                Next
            End If

            ' Set up the primary key
            ReDim keys(1)
            keys(0) = mDataSet.Tables("Unit").Columns(0) ' property id
            keys(1) = mDataSet.Tables("Unit").Columns(1)
            mDataSet.Tables("Unit").PrimaryKey = keys
            mDataSet.Tables("Unit").DefaultView.Sort = "Text"

            Dim new_relation As New DataRelation("PhysPropUnits", mDataSet.Tables("Property").Columns(0), mDataSet.Tables("Unit").Columns(0))
            mDataSet.Relations.Add(new_relation)
        Catch e As Exception
            ' emergency if data is not available as file
            Dim physicalProperties As DataTable = MakePhysicalPropertiesTable()
            Dim units As DataTable = MakeUnitsTable()
            mDataSet.Tables.Add(physicalProperties)
            mDataSet.Tables.Add(units)
            Dim new_relation As New DataRelation("PhysPropUnits", physicalProperties.Columns(0), units.Columns(0))
            mDataSet.Relations.Add(new_relation)
            PopulatePhysPropUnitTables(units, physicalProperties)
        End Try
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

    Private Sub PopulatePhysPropUnitTables(ByVal Units As DataTable, ByVal PhysicalProperties As DataTable)
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

    Private Function AddPhysicalProperty(ByVal PhysicalProperties As DataTable, ByVal Txt As String) As Integer
        Dim rw As DataRow = PhysicalProperties.NewRow()
        rw("Text") = Txt
        Dim i As Integer = CInt(rw(0))         ' get the id to return
        PhysicalProperties.Rows.Add(rw)
        Return i
    End Function

    Private Sub AddUnit(ByVal Units As DataTable, ByVal Id As Integer, ByVal Txt As String, Optional ByVal Desc As String = "")
        Dim rw As DataRow = Units.NewRow()
        rw("Text") = Txt
        rw("Description") = Desc
        rw(0) = Id ' get the id to return
        Units.Rows.Add(rw)
    End Sub

    Public Function IsDefaultLanguageRightToLeft() As Boolean
        Return IsLanguageRightToLeft(mDefaultLanguageCode)
    End Function

    Public Function IsLanguageRightToLeft(ByVal a_language_code As String) As Boolean
        Select Case a_language_code
            Case "fa"
                Return True
        End Select
    End Function

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

