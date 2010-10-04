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
    Private mXsltScriptPath As String
    Private mUseXsltForHtml As Boolean
    Private mShowTermsInHtml As Boolean
    Private mShowCommentsInHtml As Boolean
    Private mShowLinksButton As Boolean
    Private mDefaultParser As String
    Private mTimerMinutes As Integer = 10
    Private mAllowWebSearch As Boolean
    Private defaultArchetypeRepositoryUrl As String = "http://openehr.org/knowledge/services/ArchetypeFinderBean?wsdl"
    Private mArchetypeRepositoryUrl As New Uri(defaultArchetypeRepositoryUrl)
    Private mAllowTerminologyLookUp As Boolean
    Private defaultTerminologyUrl As String = "http://ots.oceaninformatics.com/OTS/OTSService.asmx"
    Private mTerminologyUrl As New Uri(defaultTerminologyUrl)
    Private mColors() As Color = {Color.Yellow, Color.LightGreen, Color.LightSkyBlue, Color.Tomato, Color.Red, Color.Silver, Color.LightGray, Color.Orange}

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

    Property UserEmail() As String
        Get
            Return mUserEmail
        End Get
        Set(ByVal Value As String)
            mUserEmail = Value
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

    Property RepositoryUrl() As Uri
        Get
            Return mArchetypeRepositoryUrl
        End Get
        Set(ByVal value As Uri)
            mArchetypeRepositoryUrl = value
        End Set
    End Property

    Property TerminologyUrl() As Uri
        Get
            Return mTerminologyUrl
        End Get
        Set(ByVal value As Uri)
            mTerminologyUrl = value
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

    Property HelpLocationPath() As String
        Get
            Return mHelpPath
        End Get
        Set(ByVal Value As String)
            mHelpPath = Value
        End Set
    End Property

    Property XsltScriptPath() As String
        Get
            Return mXsltScriptPath
        End Get
        Set(ByVal Value As String)
            mXsltScriptPath = Value
        End Set
    End Property

    Property UseXsltForHtml() As Boolean
        Get
            Return mUseXsltForHtml
        End Get
        Set(ByVal value As Boolean)
            mUseXsltForHtml = value
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

    Property ShowLinksButton() As Boolean
        Get
            Return mShowLinksButton
        End Get
        Set(ByVal value As Boolean)
            mShowLinksButton = value
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

    Property AutosaveInterval() As Integer
        Get
            Return mTimerMinutes
        End Get
        Set(ByVal value As Integer)
            mTimerMinutes = value
        End Set
    End Property

    Sub ShowOptionsForm()
        Dim frm As New ApplicationOptionsForm
        frm.TabConfiguration.SelectedIndex = 1

        frm.txtUsername.Text = mUserName
        frm.txtEmail.Text = mUserEmail
        frm.txtOrganisation.Text = mUserOrganisation
        frm.RepositoryPathTextBox.Text = mRepositoryPath
        frm.RepositoryAutoSaveCheckBox.Checked = mRepositoryAutoSave
        frm.XmlRepositoryPathTextBox.Text = mXmlRepositoryPath
        frm.XmlRepositoryAutoSaveCheckBox.Checked = mXmlRepositoryAutoSave
        frm.txtTerminologyURL.Text = mTerminologyUrl.ToString
        frm.txtHelpFile.Text = mHelpPath
        frm.comboOccurrences.Text = mOccurrencesView
        frm.chkWebSearch.Checked = mAllowWebSearch
        frm.chkTerminology.Checked = mAllowTerminologyLookUp

        For i As Integer = 0 To ReferenceModel.ValidReferenceModelNames.Length - 1
            frm.comboReferenceModel.Items.Add(ReferenceModel.ValidReferenceModelNames(i))
        Next

        frm.numAutoSave.Value = CDec(mTimerMinutes)
        frm.txtURL.Text = mArchetypeRepositoryUrl.ToString

        If mDefaultParser = "xml" Then
            frm.chkParserXML.Checked = True
        Else
            frm.chkParserADL.Checked = True
        End If

        frm.XsltScriptPathTextBox.Text = mXsltScriptPath
        frm.XsltScriptPathCheckBox.Checked = mUseXsltForHtml
        frm.chkShowTerminologyInHTML.Checked = mShowTermsInHtml
        frm.chkShowCommentsInHTML.Checked = mShowCommentsInHtml
        frm.ShowLinksButtonCheckBox.Checked = mShowLinksButton

        frm.comboReferenceModel.SelectedIndex = mDefaultRM
        frm.Panel_0.BackColor = mColors(0)
        frm.Panel_1.BackColor = mColors(1)
        frm.Panel_2.BackColor = mColors(2)
        frm.Panel_3.BackColor = mColors(3)
        frm.Panel_4.BackColor = mColors(4)
        frm.Panel_5.BackColor = mColors(5)
        frm.Panel_6.BackColor = mColors(6)
        frm.Panel_7.BackColor = mColors(7)

        AddHandler frm.RestoreDefaultTerminologyServiceUrlButton.Click, AddressOf RestoreDefaultTerminologyServiceUrlButton_Click
        AddHandler frm.RestoreDefaultSharedRepositoryUrlButton.Click, AddressOf RestoreDefaultSharedRepositoryUrlButton_Click

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            mUserName = frm.txtUsername.Text
            mUserEmail = frm.txtEmail.Text
            mUserOrganisation = frm.txtOrganisation.Text
            mRepositoryPath = frm.RepositoryPathTextBox.Text
            mRepositoryAutoSave = frm.RepositoryAutoSaveCheckBox.Checked
            mXmlRepositoryPath = frm.XmlRepositoryPathTextBox.Text
            mXmlRepositoryAutoSave = frm.XmlRepositoryAutoSaveCheckBox.Checked
            mHelpPath = frm.txtHelpFile.Text

            mXsltScriptPath = frm.XsltScriptPathTextBox.Text
            mUseXsltForHtml = frm.XsltScriptPathCheckBox.Checked
            mShowTermsInHtml = frm.chkShowTerminologyInHTML.Checked
            mShowCommentsInHtml = frm.chkShowCommentsInHTML.Checked
            mShowLinksButton = frm.ShowLinksButtonCheckBox.Checked

            mDefaultRM = frm.comboReferenceModel.SelectedIndex
            mOccurrencesView = frm.comboOccurrences.Text

            If Uri.IsWellFormedUriString(frm.txtTerminologyURL.Text, UriKind.Absolute) Then
                mTerminologyUrl = New Uri(frm.txtTerminologyURL.Text)
            Else
                MessageBox.Show("Invalid URL for Terminology Service: " & frm.txtTerminologyURL.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If Uri.IsWellFormedUriString(frm.txtURL.Text, UriKind.Absolute) Then
                mArchetypeRepositoryUrl = New Uri(frm.txtURL.Text)
            Else
                MessageBox.Show("Invalid URL for Shared Repository: " & frm.txtURL.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            ' HKF: 22 Dec 2008
            'filename = Path.Combine(Application.StartupPath, "ArchetypeEditor.cfg")
            filename = Path.Combine(Options.AssemblyPath, "ArchetypeEditor.cfg")
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
                                Case "XsltScriptPath"
                                    mXsltScriptPath = y(1).Trim
                                Case "UseXsltForHtml"
                                    mUseXsltForHtml = Boolean.Parse(y(1).Trim)
                                Case "ShowTermsInHtml"
                                    mShowTermsInHtml = Boolean.Parse(y(1).Trim)
                                Case "ShowCommentsInHtml"
                                    mShowCommentsInHtml = Boolean.Parse(y(1).Trim)
                                Case "ShowLinksButton"
                                    mShowLinksButton = Boolean.Parse(y(1).Trim)
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
                                Case "AllowSearchForArchetypesFromWeb"
                                    mAllowWebSearch = Boolean.Parse(y(1).Trim)
                                Case "SharedRepositoryUrl"
                                    Dim obsoleteUrl As String = "archetypes.com.au/archetypefinder/services/ArchetypeFinderBean?wsdl"
                                    Dim url As String = y(1).Trim

                                    If url.EndsWith(obsoleteUrl) Then
                                        url = defaultArchetypeRepositoryUrl
                                    End If

                                    mArchetypeRepositoryUrl = New Uri(url)
                                Case "AllowTerminologyLookUp"
                                    mAllowTerminologyLookUp = Boolean.Parse(y(1).Trim)
                                Case "AutosaveInterval"
                                    mTimerMinutes = Integer.Parse(y(1).Trim)
                                Case "TerminologyUrl"
                                    Dim uriString As String = y(1).Trim

                                    If Uri.IsWellFormedUriString(uriString, UriKind.Absolute) Then
                                        mTerminologyUrl = New Uri(uriString)
                                    End If
                            End Select
                        Else
                            MessageBox.Show("Error reading '" & y(0) & "'", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Catch e As Exception
                        MessageBox.Show("Error reading Configuration File '" & filename & "' - please view options and save to restore: " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not StrmRead Is Nothing Then
                            StrmRead.Close()
                        End If

                        Return False    ' FIXME: Spaghetti code and command-query separation
                    End Try
                End While
            Catch e As Exception
                MessageBox.Show("Error reading Configuration File 'ArchetypeEditor.cfg' - please view options and save to restore: " & e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False    ' FIXME: Spaghetti code and command-query separation
            End Try

            StrmRead.Close()
        Else
            Return False    ' FIXME: Spaghetti code and command-query separation
        End If

        Return True ' FIXME: Spaghetti code and command-query separation
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
                StrmWrite.WriteLine("SharedRepositoryUrl=" & mArchetypeRepositoryUrl.ToString)
                StrmWrite.WriteLine("TerminologyUrl=" & mTerminologyUrl.ToString)
                StrmWrite.WriteLine("HelpPath=" & mHelpPath)
                StrmWrite.WriteLine("XsltScriptPath=" & mXsltScriptPath)
                StrmWrite.WriteLine("UseXsltForHtml=" & mUseXsltForHtml.ToString)
                StrmWrite.WriteLine("ShowTermsInHtml=" & mShowTermsInHtml.ToString)
                StrmWrite.WriteLine("ShowCommentsInHtml=" & mShowCommentsInHtml.ToString)
                StrmWrite.WriteLine("ShowLinksButton=" & mShowLinksButton.ToString)
                StrmWrite.WriteLine("DefaultReferenceModel=" & mDefaultRM.ToString)
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
        ' HKF: 22 Dec 2008
        'Dim result As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName)
        Dim result As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Options.ProductName)

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

    Public Sub ValidateConfiguration()
        Dim hasErrors As Boolean = False
        Dim message As String = "Errors:"

        If Not File.Exists(mHelpPath) Then
            hasErrors = True
            message &= ": Help file does not exist @ " & mHelpPath
        End If

        If Not Directory.Exists(mRepositoryPath) Then
            hasErrors = True
            message &= ": Repository does not exist @ " & mRepositoryPath
        End If

        If Not Directory.Exists(mXmlRepositoryPath) Then
            hasErrors = True
            message &= ": XML Repository does not exist @ " & mXmlRepositoryPath
        End If

        If hasErrors Then
            MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ShowOptionsForm()
        End If
    End Sub

    Private Sub RestoreDefaultTerminologyServiceUrlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(CType(sender, Control).FindForm(), ApplicationOptionsForm).txtTerminologyURL.Text = defaultTerminologyUrl
    End Sub

    Private Sub RestoreDefaultSharedRepositoryUrlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(CType(sender, Control).FindForm(), ApplicationOptionsForm).txtURL.Text = defaultArchetypeRepositoryUrl
    End Sub

    Sub New()
        ' HKF: 22 Dec 2008
        'mRepositoryPath = Path.Combine(Application.StartupPath, "..\Archetypes")
        mRepositoryPath = Path.Combine(Options.AssemblyPath, "..\Archetypes")
        mXmlRepositoryPath = mRepositoryPath
        mUserName = ""
        mUserEmail = ""
        ' HKF: 22 Dec 2008
        'mHelpPath = Path.Combine(Application.StartupPath, "Help\ArchetypeEditor.chm")
        'mXsltScriptPath = Path.Combine(Application.StartupPath, "HTML\adlxml-to-html.xsl")
        mHelpPath = Path.Combine(Options.AssemblyPath, "Help\ArchetypeEditor.chm")
        mXsltScriptPath = Path.Combine(Options.AssemblyPath, "HTML\adlxml-to-html.xsl")
        mUseXsltForHtml = False
        mShowTermsInHtml = False
        mShowCommentsInHtml = False
        mShowLinksButton = False
        mOccurrencesView = "numeric"
        mDefaultParser = "adl"
        mAllowWebSearch = False
        mAllowTerminologyLookUp = False
        LoadConfiguration()
    End Sub

    Public Shared ReadOnly Property AssemblyPath() As String
        Get
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim basePath As String = System.IO.Path.GetDirectoryName(assembly.CodeBase)

            If basePath.StartsWith("file:\") Then
                basePath = basePath.Substring(6)
            End If

            Return basePath
        End Get
    End Property

    Shared ReadOnly Property ProductName() As String
        Get
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim productAttributes() As System.Reflection.AssemblyProductAttribute
            productAttributes = assembly.GetCustomAttributes(GetType(System.Reflection.AssemblyProductAttribute), False)

            If productAttributes Is Nothing Or productAttributes.Length < 1 Then
                Throw New ApplicationException("Assembly must contain AssemblyProductAttribute")
            End If

            Return productAttributes(0).Product
        End Get
    End Property

End Class
