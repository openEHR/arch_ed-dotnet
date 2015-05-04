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
    Private defaultArchetypeRepositoryUrl As String = "http://openehr.org/ckm/services/ArchetypeFinderBean?wsdl"
    Private mArchetypeRepositoryUrl As New Uri(defaultArchetypeRepositoryUrl)
    Private mAllowTerminologyLookUp As Boolean
    Private mDefaultTerminologyUrl As String
    Private mTerminologyUrl As Uri
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

    Property DefaultTerminologyUrl() As String
        Get
            Return mDefaultTerminologyUrl
        End Get
        Set(ByVal value As String)
            mDefaultTerminologyUrl = value
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

    ReadOnly Property TerminologyUrlString() As String
        Get
            Dim result As String = ""

            If AllowTerminologyLookUp And Not TerminologyUrl Is Nothing Then
                result = TerminologyUrl.ToString
            End If

            Return result
        End Get
    End Property

    Property AllowTerminologyLookUp() As Boolean
        Get
            Return mAllowTerminologyLookUp
        End Get
        Set(ByVal Value As Boolean)
            mAllowTerminologyLookUp = Value
        End Set
    End Property

    ReadOnly Property HelpLocationPath() As String
        Get
            Return mHelpPath
        End Get
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

    Public Sub ShowOptionsForm()
        Dim frm As New ApplicationOptionsForm
        frm.TabConfiguration.SelectedIndex = 1

        frm.txtUsername.Text = mUserName
        frm.txtEmail.Text = mUserEmail
        frm.txtOrganisation.Text = mUserOrganisation
        frm.RepositoryPathTextBox.Text = mRepositoryPath
        frm.RepositoryAutoSaveCheckBox.Checked = mRepositoryAutoSave
        frm.XmlRepositoryPathTextBox.Text = mXmlRepositoryPath
        frm.XmlRepositoryAutoSaveCheckBox.Checked = mXmlRepositoryAutoSave

        frm.TerminologyServiceUrlTextBox.Visible = Not TerminologyUrl Is Nothing
        frm.TerminologyServiceUrlLabel.Visible = Not TerminologyUrl Is Nothing
        frm.TerminologyServiceUrlCheckBox.Visible = Not TerminologyUrl Is Nothing
        frm.RestoreDefaultTerminologyServiceUrlButton.Visible = Not TerminologyUrl Is Nothing

        If Not TerminologyUrl Is Nothing Then
            frm.TerminologyServiceUrlTextBox.Text = mTerminologyUrl.ToString
        End If

        frm.OccurrencesComboBox.Text = mOccurrencesView
        frm.SharedRepositoryUrlCheckBox.Checked = mAllowWebSearch
        frm.TerminologyServiceUrlCheckBox.Checked = mAllowTerminologyLookUp

        For i As Integer = 0 To ReferenceModel.ValidReferenceModelNames.Length - 1
            frm.ReferenceModelComboBox.Items.Add(ReferenceModel.ValidReferenceModelNames(i))
        Next

        frm.numAutoSave.Value = CDec(mTimerMinutes)
        frm.SharedRepositoryUrlTextBox.Text = mArchetypeRepositoryUrl.ToString

        If mDefaultParser = "xml" Then
            frm.ParserXmlRadioButton.Checked = True
        Else
            frm.ParserAdlRadioButton.Checked = True
        End If

        frm.XsltScriptPathTextBox.Text = mXsltScriptPath
        frm.XsltScriptPathCheckBox.Checked = mUseXsltForHtml
        frm.ShowTerminologyInHTMLCheckBox.Checked = mShowTermsInHtml
        frm.ShowCommentsInHTMLCheckBox.Checked = mShowCommentsInHtml
        frm.ShowLinksButtonCheckBox.Checked = mShowLinksButton

        frm.ReferenceModelComboBox.SelectedIndex = mDefaultRM
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

            mXsltScriptPath = frm.XsltScriptPathTextBox.Text
            mUseXsltForHtml = frm.XsltScriptPathCheckBox.Checked
            mShowTermsInHtml = frm.ShowTerminologyInHTMLCheckBox.Checked
            mShowCommentsInHtml = frm.ShowCommentsInHTMLCheckBox.Checked
            mShowLinksButton = frm.ShowLinksButtonCheckBox.Checked

            mDefaultRM = frm.ReferenceModelComboBox.SelectedIndex
            mOccurrencesView = frm.OccurrencesComboBox.Text

            If Uri.IsWellFormedUriString(frm.TerminologyServiceUrlTextBox.Text, UriKind.Absolute) Then
                mTerminologyUrl = New Uri(frm.TerminologyServiceUrlTextBox.Text)
            ElseIf frm.TerminologyServiceUrlTextBox.Visible Then
                MessageBox.Show("Invalid URL for Terminology Service: " & frm.TerminologyServiceUrlTextBox.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If Uri.IsWellFormedUriString(frm.SharedRepositoryUrlTextBox.Text, UriKind.Absolute) Then
                mArchetypeRepositoryUrl = New Uri(frm.SharedRepositoryUrlTextBox.Text)
            Else
                MessageBox.Show("Invalid URL for Shared Repository: " & frm.SharedRepositoryUrlTextBox.Text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            mTimerMinutes = CInt(frm.numAutoSave.Value)
            mAllowWebSearch = frm.SharedRepositoryUrlCheckBox.Checked
            mAllowTerminologyLookUp = frm.TerminologyServiceUrlCheckBox.Checked

            If frm.ParserAdlRadioButton.Checked Then
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
            filename = Path.Combine(OldApplicationDataDirectory, "ArchetypeEditor.cfg")
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
                                    Dim obsoleteUrl2 As String = "openehr.org/knowledge/services/ArchetypeFinderBean?wsdl"
                                    Dim url As String = y(1).Trim

                                    If url.EndsWith(obsoleteUrl) Or url.EndsWith(obsoleteUrl2) Then
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

                If Not mTerminologyUrl Is Nothing Then
                    StrmWrite.WriteLine("TerminologyUrl=" & mTerminologyUrl.ToString)
                End If

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

    Public Function OldApplicationDataDirectory() As String
        Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AssemblyTitle)
    End Function

    Public Function ApplicationDataDirectory() As String
        Dim result As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim product As String = ProductName
        Dim i As Integer = product.LastIndexOf(" ")

        If i > 0 Then
            i = product.LastIndexOf(" ", i - 1)

            If i > 0 Then
                result = Path.Combine(result, product.Remove(i))
                product = product.Substring(i + 1)
            End If
        End If

        result = Path.Combine(result, product)

        If Not Directory.Exists(result) Then
            Directory.CreateDirectory(result)
        End If

        Return result
    End Function

    Function StateMachineColour(ByVal a_StateMachineType As StateMachineType) As Color
        Debug.Assert(a_StateMachineType <> StateMachineType.Not_Set)

        Select Case a_StateMachineType
            Case StateMachineType.Planned
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
            Case Else
                Return Color.PaleGoldenrod
        End Select
    End Function

    Public Sub ValidateConfiguration()
        Dim hasErrors As Boolean = False
        Dim message As String = ""

        If mRepositoryPath = "" And mXmlRepositoryPath = "" Then
            hasErrors = True
            message = "To begin working with " & ProductName & ", please configure your Archetype Repository paths."
        Else
            message = "Configuration error:"

            If Not Directory.Exists(mRepositoryPath) Then
                hasErrors = True
                message &= vbCrLf & "Archetype Repository does not exist at '" & mRepositoryPath & "'."
            End If

            If Not Directory.Exists(mXmlRepositoryPath) Then
                hasErrors = True
                message &= vbCrLf & "XML Archetype Repository does not exist at '" & mXmlRepositoryPath & "'."
            End If
        End If

        If hasErrors Then
            MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ShowOptionsForm()
        End If
    End Sub

    Private Sub RestoreDefaultTerminologyServiceUrlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(CType(sender, Control).FindForm(), ApplicationOptionsForm).TerminologyServiceUrlTextBox.Text = DefaultTerminologyUrl
    End Sub

    Private Sub RestoreDefaultSharedRepositoryUrlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(CType(sender, Control).FindForm(), ApplicationOptionsForm).SharedRepositoryUrlTextBox.Text = defaultArchetypeRepositoryUrl
    End Sub

    Sub New()
        mRepositoryPath = ""
        mXmlRepositoryPath = ""
        mUserName = ""
        mUserEmail = ""
        mHelpPath = Path.Combine(AssemblyPath, "Help\ArchetypeEditor.chm")
        mXsltScriptPath = Path.Combine(AssemblyPath, "HTML\adlxml-to-html.xsl")
        mUseXsltForHtml = False
        mShowTermsInHtml = False
        mShowCommentsInHtml = False
        mShowLinksButton = False
        mOccurrencesView = "numeric"
        mDefaultParser = "adl"
        mAllowWebSearch = False
        mAllowTerminologyLookUp = False
    End Sub

    Public ReadOnly Property AssemblyPath() As String
        Get
            Dim result As String
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly

            '// AEPR-35 IMCN 4 May 2015 
            '//    result = System.IO.Path.GetDirectoryName(assembly.CodeBase)                                         
            '//    If result.StartsWith("file:\") Then
            '//       result = result.Substring(6)
            ' //   End If

            result = System.IO.Path.GetDirectoryName(assembly.Location)

            Return result
        End Get
    End Property

    Public ReadOnly Property ProductName() As String
        Get
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
            Dim attributes() As System.Reflection.AssemblyProductAttribute
            attributes = assembly.GetCustomAttributes(GetType(System.Reflection.AssemblyProductAttribute), False)

            If attributes Is Nothing Or attributes.Length < 1 Then
                Throw New ApplicationException("Assembly must contain AssemblyProductAttribute")
            End If

            Return attributes(0).Product
        End Get
    End Property

    Public ReadOnly Property Copyright() As String
        Get
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
            Dim attributes() As System.Reflection.AssemblyCopyrightAttribute
            attributes = assembly.GetCustomAttributes(GetType(System.Reflection.AssemblyCopyrightAttribute), False)

            If attributes Is Nothing Or attributes.Length < 1 Then
                Throw New ApplicationException("Assembly must contain AssemblyCopyrightAttribute")
            End If

            Return attributes(0).Copyright
        End Get
    End Property

    Public ReadOnly Property AssemblyTitle() As String
        Get
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
            Dim attributes() As System.Reflection.AssemblyTitleAttribute
            attributes = assembly.GetCustomAttributes(GetType(System.Reflection.AssemblyTitleAttribute), False)

            If attributes Is Nothing Or attributes.Length < 1 Then
                Throw New ApplicationException("Assembly must contain AssemblyTitleAttribute")
            End If

            Return attributes(0).Title
        End Get
    End Property

End Class
