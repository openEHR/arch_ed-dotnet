Option Explicit On 

Public Class Options
    Private mRepositoryPath As String
    Private mUserName As String
    Private mUserEmail As String
    Private mDefaultRM As Integer
    Private mHelpPath As String
    Private mColors() As Color = {Color.Yellow, Color.LightGreen, Color.LightSkyBlue, Color.Tomato, Color.Red, Color.Silver, Color.LightGray}

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
    Sub ShowOptionsForm()
        Dim frm As New ApplicationOptionsForm

        frm.txtUsername.Text = mUserName
        frm.txtEmail.Text = mUserEmail
        frm.txtRepositoryPath.Text = mRepositoryPath
        frm.txtHelpFile.Text = mHelpPath
        For i As Integer = 0 To ReferenceModel.Instance.ValidReferenceModelNames.Length - 1
            frm.comboReferenceModel.Items.Add(ReferenceModel.Instance.ValidReferenceModelNames(i))
        Next
        frm.comboReferenceModel.SelectedIndex = mDefaultRM
        frm.Panel_0.BackColor = mColors(0)
        frm.Panel_1.BackColor = mColors(1)
        frm.Panel_2.BackColor = mColors(2)
        frm.Panel_3.BackColor = mColors(3)
        frm.Panel_4.BackColor = mColors(4)
        frm.Panel_5.BackColor = mColors(5)
        frm.Panel_6.BackColor = mColors(6)

        If frm.ShowDialog() = DialogResult.OK Then
            mUserName = frm.txtUsername.Text
            mUserEmail = frm.txtEmail.Text
            mRepositoryPath = frm.txtRepositoryPath.Text
            mHelpPath = frm.txtHelpFile.Text
            mDefaultRM = frm.comboReferenceModel.SelectedIndex
            mColors(0) = frm.Panel_0.BackColor
            mColors(1) = frm.Panel_1.BackColor
            mColors(2) = frm.Panel_2.BackColor
            mColors(3) = frm.Panel_3.BackColor
            mColors(4) = frm.Panel_4.BackColor
            mColors(5) = frm.Panel_5.BackColor
            mColors(6) = frm.Panel_6.BackColor

            WriteConfiguration()
        End If

        frm.Close()

    End Sub

    Public Function LoadConfiguration() As Boolean
        Dim StrmRead As IO.StreamReader
        Dim x, z, code, description As String
        Dim y() As String
        Dim i As Integer
        Dim rw As DataRow

        If System.IO.File.Exists(Application.StartupPath & "\ArchetypeEditor.cfg") Then
            Try
                StrmRead = New IO.StreamReader("ArchetypeEditor.cfg")

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
        Dim x, z, code, description As String
        Dim y() As String
        Dim i As Integer
        Dim rw As DataRow

        Try
            StrmWrite = New IO.StreamWriter(Application.StartupPath & "\ArchetypeEditor.cfg", False)
            Try
                StrmWrite.WriteLine("UserName=" & mUserName)
                StrmWrite.WriteLine("UserEmail=" & mUserEmail)
                StrmWrite.WriteLine("RepositoryPath=" & mRepositoryPath)
                StrmWrite.WriteLine("HelpPath=" & mHelpPath)
                StrmWrite.WriteLine("DefaultReferenceModel=" & mDefaultRM.ToString)
                Dim s As String

                For i = 0 To mColors.Length - 1
                    s &= mColors(i).ToArgb.ToString
                    If i < mColors.Length - 1 Then
                        s &= ","
                    End If
                Next
                StrmWrite.WriteLine("StateMachineColours=" & s)
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
        Return mColors(a_StateMachineType - 1)
    End Function

    Sub New()
        mRepositoryPath = "\SampleArchetypes"
        mUserName = ""
        mUserEmail = ""
        mHelpPath = Application.StartupPath & "\Help\ArchetypeEditor.chm"
        LoadConfiguration()
    End Sub
End Class
