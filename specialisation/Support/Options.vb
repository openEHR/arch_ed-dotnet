Option Explicit On

Imports System.IO

Public Class Options
    Private mRepositoryPath As String
    Private mRepositoryAutoSave As Boolean
    Private mXmlRepositoryPath As String
    Private mXmlRepositoryAutoSave As Boolean
    Private mUserName As String
    Private mUserEmail As String
    Private mUserOrganisation As String
    Private mDefaultRM As Integer
    Private mOccurrencesView As String
    Private mHelpPath As String
    Private mDefaultParser As String
    Private mShowTermsInHtml As Boolean
    Private mShowCommentsInHtml As Boolean
    Private mTimerMinutes As Integer = 10
    Private mAllowWebSearch As Boolean
    Private mArchetypeRepositoryURL As New Uri("http://archetypes.com.au/archetypefinder/services/ArchetypeFinderBean?wsdl")
    Private mAllowTerminologyLookUp As Boolean
    Private mTerminologyURL As New Uri("http://ots.oceaninformatics.biz/OTS/OTSService.asmx")
    Private mColors() As Color = {Color.Yellow, Color.LightGreen, Color.LightSkyBlue, Color.Tomato, Color.Red, Color.Silver, Color.LightGray, Color.Orange}

    Property HelpLocationPath() As String
        Get
            Return mHelpPath
        End Get
        Set(ByVal Value As String)
            mHelpPath = Value
        End Set
    End Property

    Property RepositoryPath() As String
        Get
            Return mRepositoryPath
        End Get
        Set(ByVal Value As String)
            mRepositoryPath = Value
        End Set
    End Property

    Property RepositoryAutoSave() As Boolean
        Get
            Return mRepositoryAutoSave
        End Get
        Set(ByVal value As Boolean)
            mRepositoryAutoSave = value
        End Set
    End Property

    Property XmlRepositoryPath() As String
        Get
            Return mXmlRepositoryPath
        End Get
        Set(ByVal Value As String)
            mXmlRepositoryPath = Value
        End Set
    End Property

    Property XmlRepositoryAutoSave() As Boolean
        Get
            Return mXmlRepositoryAutoSave
        End Get
        Set(ByVal value As Boolean)
            mXmlRepositoryAutoSave = value
        End Set
    End Property

    Property RepositoryURL() As Uri
        Get
            Return mArchetypeRepositoryURL
        End Get
        Set(ByVal value As Uri)
            mArchetypeRepositoryURL = value
        End Set
    End Property

    Property TerminologyURL() As Uri
        Get
            Return mTerminologyURL
        End Get
        Set(ByVal value As Uri)
            mTerminologyURL = value
        End Set
    End Property

    Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal Value As String)
            mUserName = Value
        End Set
    End Property

    Property UserOrganisation() As String
        Get
            Return mUserOrganisation
        End Get
        Set(ByVal Value As String)
            mUserOrganisation = Value
        End Set
    End Property

    Property OccurrencesView() As String
        Get
            Return mOccurrencesView
        End Get
        Set(ByVal Value As String)
            mOccurrencesView = Value
        End Set
    End Property

    Property UserEmail() As String
        Get
            Return mUserEmail
        End Get
        Set(ByVal Value As String)
            mUserEmail = Value
        End Set
    End Property

    Property DefaultReferenceModel() As Integer
        Get
            Return mDefaultRM
        End Get
        Set(ByVal Value As Integer)
            mDefaultRM = Value
        End Set
    End Property

    Property DefaultParser() As String
        Get
            Return mDefaultParser
        End Get
        Set(ByVal Value As String)
            mDefaultParser = Value
        End Set
    End Property

    Property AllowWebSearch() As Boolean
        Get
            Return mAllowWebSearch
        End Get
        Set(ByVal Value As Boolean)
            mAllowWebSearch = Value
        End Set
    End Property

    Property AllowTerminologyLookUp() As Boolean
        Get
            Return mAllowTerminologyLookUp
        End Get
        Set(ByVal Value As Boolean)
            mAllowTerminologyLookUp = Value
        End Set
    End Property

    Property ShowTermsInHtml() As Boolean
        Get
            Return mShowTermsInHtml
        End Get
        Set(ByVal value As Boolean)
            mShowTermsInHtml = value
        End Set
    End Property

    Property ShowCommentsInHtml() As Boolean
        Get
            Return mShowCommentsInHtml
        End Get
        Set(ByVal value As Boolean)
            mShowCommentsInHtml = value
        End Set
    End Property

    Property AutosaveInterval() As Integer
        Get
            Return mTimerMinutes
        End Get
        Set(ByVal value As Integer)
            mTimerMinutes = value
        End Set
    End Property

    Sub ShowOptionsForm(Optional ByVal tabIndex As Integer = 0)
        Dim frm As New ApplicationOptionsForm

        If frm.TabConfiguration.TabPages.Count > tabIndex Then
            frm.TabConfiguration.SelectedIndex = tabIndex
        End If

        frm.txtUsername.Text = mUserName
        frm.txtEmail.Text = mUserEmail
        frm.txtOrganisation.Text = mUserOrganisation
        frm.RepositoryPathTextBox.Text = mRepositoryPath
        frm.RepositoryAutoSaveCheckBox.Checked = mRepositoryAutoSave
        frm.XmlRepositoryPathTextBox.Text = mXmlRepositoryPath
        frm.XmlRepositoryAutoSaveCheckBox.Checked = mXmlRepositoryAutoSave
        frm.txtTerminologyURL.Text = OTSControls.Term.OtsWebService.Url
        frm.txtHelpFile.Text = mHelpPath
        frm.comboOccurrences.Text = mOccurrencesView
        frm.chkWebSearch.Checked = mAllowWebSearch
        frm.chkTerminology.Checked = mAllowTerminologyLookUp

        For i As Integer = 0 To ReferenceModel.ValidReferenceModelNames.Length - 1
            frm.comboReferenceModel.Items.Add(ReferenceModel.ValidReferenceModelNames(i))
        Next

        frm.numAutoSave.Value = CDec(mTimerMinutes)
        frm.txtURL.Text = mArchetypeRepositoryURL.ToString
        frm.txtTerminologyURL.Text = mTerminologyURL.ToString

        If mDefaultParser = "xml" Then
            frm.chkParserXML.Checked = True
        Else
            frm.chkParserADL.Checked = True
        End If

        frm.comboReferenceModel.SelectedIndex = mDefaultRM
        frm.chkShowTerminologyInHTML.Checked = mShowTermsInHtml
        frm.chkShowCommentsInHTML.Checked = mShowCommentsInHtml
        frm.Panel_0.BackColor = mColors(0)
        frm.Panel_1.BackColor = mColors(1)
        frm.Panel_2.BackColor = mColors(2)
        frm.Panel_3.BackColor = mColors(3)
        frm.Panel_4.BackColor = mColors(4)
        frm.Panel_5.BackColor = mColors(5)
        frm.Panel_6.BackColor = mColors(6)
        frm.Panel_7.BackColor = mColors(7)

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            mUserName = frm.txtUsername.Text
            mUserEmail = frm.txtEmail.Text
            mUserOrganisation = frm.txtOrganisation.Text
            mRepositoryPath = frm.RepositoryPathTextBox.Text
            mRepositoryAutoSave = frm.RepositoryAutoSaveCheckBox.Checked
            mXmlRepositoryPath = frm.XmlRepositoryPathTextBox.Text
            mXmlRepositoryAutoSave = frm.XmlRepositoryAutoSaveCheckBox.Checked
            mHelpPath = frm.txtHelpFile.Text
            mDefaultRM = frm.comboReferenceModel.SelectedIndex
            mOccurrencesView = frm.comboOccurrences.Text
            mShowTermsInHtml = frm.chkShowTerminologyInHTML.Checked
            mShowCommentsInHtml = frm.chkShowCommentsInHTML.Checked
            mArchetypeRepositoryURL = New Uri(frm.txtURL.Text)
            If Uri.IsWellFormedUriString(frm.txtTerminologyURL.Text, UriKind.Absolute) Then
                mTerminologyURL = New Uri(frm.txtTerminologyURL.Text)
                OTSControls.Term.OtsWebService.Url = frm.txtTerminologyURL.Text
            End If
            mTimerMinutes = CInt(frm.numAutoSave.Value)
            mAllowWebSearch = frm.chkWebSearch.Checked
            mAllowTerminologyLookUp = frm.chkTerminology.Checked
            If frm.chkParserADL.Checked Then
                mDefaultParser = "adl"
            Else
                mDefaultParser = "xml"
            End If
            mColors(0) = frm.Panel_0.BackColor
            mColors(1) = frm.Panel_1.BackColor
            mColors(2) = frm.Panel_2.BackColor
            mColors(3) = frm.Panel_3.BackColor
            mColors(4) = frm.Panel_4.BackColor
            mColors(5) = frm.Panel_5.BackColor
            mColors(6) = frm.Panel_6.BackColor
            mColors(7) = frm.Panel_7.BackColor

            WriteConfiguration()
        End If

        frm.Close()
    End Sub

    Public Function LoadConfiguration() As Boolean
        Dim StrmRead As StreamReader
        Dim x As String
        Dim y() As String
        Dim i As Integer
        Dim filename As String = Path.Combine(ApplicationDataDirectory, "ArchetypeEditor.cfg")

        If Not File.Exists(filename) Then
            filename = Path.Combine(Application.StartupPath, "ArchetypeEditor.cfg")
        End If

        If File.Exists(filename) Then
            Try
                StrmRead = New StreamReader(filename)

                While StrmRead.Peek > 0

                    x = StrmRead.ReadLine
                    Try
                        y = x.Split("=")

                        If y.Length = 2 Then
                            Select Case y(0)
                                Case "UserName"
                                    mUserName = y(1)
                                Case "UserEmail"
                                    mUserEmail = y(1)
                                Case "Organisation"
                                    mUserOrganisation = y(1)
                                Case "RepositoryPath"
                                    mRepositoryPath = y(1).Trim
                                Case "RepositoryAutoSave"
                                    mRepositoryAutoSave = Boolean.Parse(y(1).Trim)
                                Case "XmlRepositoryPath"
                                    mXmlRepositoryPath = y(1).Trim
                                Case "XmlRepositoryAutoSave"
                                    mXmlRepositoryAutoSave = Boolean.Parse(y(1).Trim)
                                Case "HelpPath"
                                    mHelpPath = y(1).Trim
                                Case "DefaultReferenceModel"
                                    mDefaultRM = Val(y(1).Trim)
                                Case "StateMachineColours"
                                    y = y(1).Split(",")
                                    For i = 0 To 4
                                        mColors(i) = System.Drawing.Color.FromArgb(CInt(Val(y(i))))
                                    Next
                                Case "OccurrencesView"
                                    mOccurrencesView = y(1).Trim
                                Case "DefaultParser"
                                    mDefaultParser = y(1).Trim.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                                Case "ShowTermsInHtml"
                                    mShowTermsInHtml = Boolean.Parse(y(1).Trim)
                                Case "ShowCommentsInHtml"
                                    mShowCommentsInHtml = Boolean.Parse(y(1).Trim)
                                Case "AllowSearchForArchetypesFromWeb"
                                    mAllowWebSearch = Boolean.Parse(y(1).Trim)
                                Case "SharedRepositoryUrl"
                                    mArchetypeRepositoryURL = New Uri(y(1).Trim)
                                Case "AllowTerminologyLookUp"
                                    mAllowTerminologyLookUp = Boolean.Parse(y(1).Trim)
                                Case "AutosaveInterval"
                                    mTimerMinutes = Integer.Parse(y(1).Trim)
                                Case "TerminologyUrl"
                                    Dim uriString As String = y(1).Trim
                                    If Uri.IsWellFormedUriString(uriString, UriKind.Absolute) Then
                                        mTerminologyURL = New Uri(uriString)
                                        OTSControls.Term.OtsWebService.Url = mTerminologyURL.ToString
                                    End If
                            End Select
                        Else
                            MessageBox.Show("Error reading '" & y(0) & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Catch e As Exception
                        MessageBox.Show("Error reading Configuration File 'ArchetypeEditor.cfg' - please view options and save to restore: " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not StrmRead Is Nothing Then
                            StrmRead.Close()
                        End If
                        Return False
                    End Try
                End While
            Catch e As Exception
                MessageBox.Show("Error reading Configuration File 'ArchetypeEditor.cfg' - please view options and save to restore: " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
            StrmRead.Close()
        Else
            Return False
        End If
        Return True
    End Function

    Sub WriteConfiguration()
        Dim StrmWrite As StreamWriter
        Dim i As Integer

        Try
            StrmWrite = New StreamWriter(Path.Combine(ApplicationDataDirectory, "ArchetypeEditor.cfg"), False)

            Try
                StrmWrite.WriteLine("UserName=" & mUserName)
                StrmWrite.WriteLine("UserEmail=" & mUserEmail)
                StrmWrite.WriteLine("Organisation=" & mUserOrganisation)
                StrmWrite.WriteLine("RepositoryPath=" & mRepositoryPath)
                StrmWrite.WriteLine("RepositoryAutoSave=" & mRepositoryAutoSave)
                StrmWrite.WriteLine("XmlRepositoryPath=" & mXmlRepositoryPath)
                StrmWrite.WriteLine("XmlRepositoryAutoSave=" & mXmlRepositoryAutoSave)
                StrmWrite.WriteLine("SharedRepositoryUrl=" & mArchetypeRepositoryURL.ToString)
                StrmWrite.WriteLine("TerminologyUrl=" & mTerminologyURL.ToString)
                StrmWrite.WriteLine("HelpPath=" & mHelpPath)
                StrmWrite.WriteLine("DefaultReferenceModel=" & mDefaultRM.ToString)
                StrmWrite.WriteLine("ShowTermsInHtml=" & mShowTermsInHtml.ToString)
                StrmWrite.WriteLine("ShowCommentsInHtml=" & mShowCommentsInHtml.ToString)
                StrmWrite.WriteLine("AllowSearchForArchetypesFromWeb=" & mAllowWebSearch.ToString)
                StrmWrite.WriteLine("AllowTerminologyLookUp=" & mAllowTerminologyLookUp.ToString)
                StrmWrite.WriteLine("AutosaveInterval=" & mTimerMinutes.ToString())

                Dim s As String = ""

                For i = 0 To mColors.Length - 1
                    s &= mColors(i).ToArgb.ToString
                    If i < mColors.Length - 1 Then
                        s &= ","
                    End If
                Next

                StrmWrite.WriteLine("StateMachineColours=" & s)
                StrmWrite.WriteLine("OccurrencesView=" & mOccurrencesView)
                StrmWrite.WriteLine("DefaultParser=" & mDefaultParser)
            Catch e As Exception
                MessageBox.Show("Error reading Configuration File 'ArchetypeEditor.cfg' - please view options and save to restore: " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch e As Exception
            MessageBox.Show("Error reading Configuration File 'ArchetypeEditor.cfg' - please view options and save to restore: " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        StrmWrite.Close()
    End Sub

    Function ApplicationDataDirectory() As String
        Dim result As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName)

        If Not Directory.Exists(result) Then
            Directory.CreateDirectory(result)
        End If

        Return result
    End Function

    Function StateMachineColour(ByVal a_StateMachineType As StateMachineType) As Color
        Debug.Assert(a_StateMachineType <> StateMachineType.Not_Set)
        Select Case a_StateMachineType
            Case StateMachineType.Initial
                Return mColors(0)
            Case StateMachineType.Active
                Return mColors(1)
            Case StateMachineType.Completed
                Return mColors(2)
            Case StateMachineType.ActiveSuspended
                Return mColors(3)
            Case StateMachineType.InitialSuspended
                Return mColors(4)
            Case StateMachineType.ActiveAborted
                Return mColors(5)
            Case StateMachineType.InitialAborted
                Return mColors(6)
            Case StateMachineType.Scheduled
                Return mColors(7)
        End Select

    End Function

    Function ValidateConfiguration() As Boolean
        Dim result As Boolean = True
        Dim message As String = "Errors:"

        If Not File.Exists(mHelpPath) Then
            result = False
            message &= (": Help file does not exist @ " & mHelpPath)
        End If

        If Not Directory.Exists(mRepositoryPath) Then
            result = False
            message &= (": Repository does not exist @ " & mRepositoryPath)
        End If

        If Not Directory.Exists(mXmlRepositoryPath) Then
            result = False
            message &= (": XML Repository does not exist @ " & mXmlRepositoryPath)
        End If

        If Not result Then
            MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Return result
    End Function

    Sub New()
        Dim path As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\") + 1)
        mRepositoryPath = path + "SampleArchetypes"
        mXmlRepositoryPath = mRepositoryPath
        mUserName = ""
        mUserEmail = ""
        mHelpPath = path & "Help\ArchetypeEditor.chm"
        mOccurrencesView = "numeric"
        mDefaultParser = "adl"
        mShowTermsInHtml = False
        mShowCommentsInHtml = False
        mAllowWebSearch = False
        mAllowTerminologyLookUp = False
        LoadConfiguration()

        If Not ValidateConfiguration() Then
            Me.ShowOptionsForm(1)
        End If
    End Sub
End Class
