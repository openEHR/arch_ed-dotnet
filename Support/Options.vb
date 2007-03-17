Option Explicit On 

Public Class Options
    Private mRepositoryPath As String
    Private mUserName As String
    Private mUserEmail As String
    Private mUserOrganisation As String
    Private mDefaultRM As Integer
    Private mOccurrencesView As String
    Private mHelpPath As String
    Private mDefaultParser As String
    Private mShowTermsInHtml As Boolean
    Private mShowCommentsInHtml As Boolean
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

    Sub ShowOptionsForm(Optional ByVal tabIndex As Integer = 0)
        Dim frm As New ApplicationOptionsForm

        If frm.TabConfiguration.TabPages.Count > tabIndex Then
            frm.TabConfiguration.SelectedIndex = tabIndex
        End If

        frm.txtUsername.Text = mUserName
        frm.txtEmail.Text = mUserEmail
        frm.txtOrganisation.Text = mUserOrganisation
        frm.txtRepositoryPath.Text = mRepositoryPath
        frm.txtHelpFile.Text = mHelpPath
        frm.comboOccurrences.Text = mOccurrencesView
        For i As Integer = 0 To ReferenceModel.ValidReferenceModelNames.Length - 1
            frm.comboReferenceModel.Items.Add(ReferenceModel.ValidReferenceModelNames(i))
        Next
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
            mRepositoryPath = frm.txtRepositoryPath.Text
            mHelpPath = frm.txtHelpFile.Text
            mDefaultRM = frm.comboReferenceModel.SelectedIndex
            mOccurrencesView = frm.comboOccurrences.Text
            mShowTermsInHtml = frm.chkShowTerminologyInHTML.Checked
            mShowCommentsInHtml = frm.chkShowCommentsInHTML.Checked
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
        Dim StrmRead As IO.StreamReader
        Dim x As String
        Dim y() As String
        Dim i As Integer
        Dim path As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\") + 1)

        If System.IO.File.Exists(path & "\ArchetypeEditor.cfg") Then
            Try
                StrmRead = New IO.StreamReader(path & "\ArchetypeEditor.cfg")

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
        Dim StrmWrite As IO.StreamWriter
        Dim i As Integer
        Dim path As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\") + 1)

        Try
            StrmWrite = New IO.StreamWriter(path & "\ArchetypeEditor.cfg", False)
            Try
                StrmWrite.WriteLine("UserName=" & mUserName)
                StrmWrite.WriteLine("UserEmail=" & mUserEmail)
                StrmWrite.WriteLine("Organisation=" & mUserOrganisation)
                StrmWrite.WriteLine("RepositoryPath=" & mRepositoryPath)
                StrmWrite.WriteLine("HelpPath=" & mHelpPath)
                StrmWrite.WriteLine("DefaultReferenceModel=" & mDefaultRM.ToString)
                StrmWrite.WriteLine("ShowTermsInHtml=" & mShowTermsInHtml.ToString)
                StrmWrite.WriteLine("ShowCommentsInHtml=" & mShowCommentsInHtml.ToString)
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
        If Not System.IO.File.Exists(mHelpPath) Then
            result = False
            message &= (": Help file does not exist @ " & mHelpPath)
        End If

        If Not System.IO.Directory.Exists(mRepositoryPath) Then
            result = False
            message &= (": Repository does not exist @ " & mRepositoryPath)
        End If

        If Not result Then
            MessageBox.Show(message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Return result
    End Function

    Sub New()
        Dim path As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\") + 1)
        mRepositoryPath = path + "SampleArchetypes"
        mUserName = ""
        mUserEmail = ""
        mHelpPath = path & "Help\ArchetypeEditor.chm"
        mOccurrencesView = "numeric"
        mDefaultParser = "adl"
        mShowTermsInHtml = False
        mShowCommentsInHtml = False
        LoadConfiguration()
        If Not ValidateConfiguration() Then
            Me.ShowOptionsForm(1)
        End If
    End Sub
End Class
