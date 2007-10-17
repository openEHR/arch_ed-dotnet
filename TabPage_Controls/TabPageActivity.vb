Public Class TabPageActivity
    Private mIsloading As Boolean
    Private mActionSpecification As TabPageStructure
    Friend WithEvents mOccurrences As OccurrencesPanel
    Private mActivity As RmActivity
    Private mFileManager As FileManagerLocal
    Private mTabPageInstruction As TabPageInstruction 'JAR: 30MAY07, EDT-44 Multiple activities per instruction

    Public Property Activity() As RmActivity
        Get
            mActivity.Children.Clear()

            mActivity.Occurrences = mOccurrences.Cardinality

            mActivity.ArchetypeId = Me.txtAction.Text

            If Not mActionSpecification Is Nothing Then
                Dim action_specification As RmStructure
                action_specification = mActionSpecification.SaveAsStructure()
                If Not action_specification Is Nothing Then
                    mActivity.Children.Add(mActionSpecification.SaveAsStructure)
                End If
            End If
            Return mActivity
        End Get
        Set(ByVal value As RmActivity)
            mActivity = value
            Me.lblNodeId.Text = mActivity.NodeId

            If mOccurrences Is Nothing Then
                mOccurrences = New OccurrencesPanel(mFileManager)
                Select Case OceanArchetypeEditor.Instance.Options.OccurrencesView
                    Case "lexical"
                        mOccurrences.Mode = OccurrencesMode.Lexical
                    Case "numeric"
                        mOccurrences.Mode = OccurrencesMode.Numeric
                End Select
            End If

            Me.mOccurrences.Cardinality = mActivity.Occurrences
            Me.txtAction.Text = mActivity.ArchetypeId

            For Each rm As RmStructure In mActivity.Children

                If Me.Controls.Contains(mActionSpecification) Then
                    Me.txtAction.Controls.Remove(mActionSpecification)
                End If
                mActionSpecification = New TabPageStructure

                Select Case rm.Type
                    Case StructureType.List, StructureType.Table, StructureType.Tree, StructureType.Single
                        mActionSpecification.ProcessStructure(CType(rm, RmStructureCompound))
                    Case StructureType.Slot
                        mActionSpecification.ProcessStructure(CType(rm, RmSlot))
                    Case Else
                        Debug.Assert(False, "Not handled yet")
                End Select

                Me.Controls.Add(mActionSpecification)
                mActionSpecification.BringToFront()
                mActionSpecification.Dock = DockStyle.Fill
            Next

        End Set
    End Property

    Private Sub TabPageActivity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mIsloading = True
        If mOccurrences Is Nothing Then
            mOccurrences = New OccurrencesPanel(mFileManager)
        End If
        Me.PanelAction.Controls.Add(mOccurrences)
        If Me.RightToLeft = Windows.Forms.RightToLeft.Yes Then
            OceanArchetypeEditor.Reflect(mOccurrences)
            mOccurrences.Dock = DockStyle.Left
        Else
            mOccurrences.Dock = DockStyle.Right
        End If

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        Me.HelpProviderActivity.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath
        'JAR: 30MAY07, EDT-44 Term already created in TabPageInstruction.  Below causes ontology to be thrown out!
        'If mFileManager.IsNew Then
        '    'need to add an RmActivity to the mActivities set
        '    Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm("New activity")
        '    mActivity = New RmActivity(a_term.Code)
        'End If
        mIsloading = False

    End Sub

    Sub TranslateGUI()
        Me.lblAction.Text = Filemanager.GetOpenEhrTerm(556, "Action")
    End Sub

    Public Sub Translate()
        mActionSpecification.Translate()

        If Not mActivity Is Nothing Then
            If mFileManager.OntologyManager.Ontology.LanguageAvailable(mFileManager.OntologyManager.LanguageCode) Then
                CType(Me.Parent, Crownwood.Magic.Controls.TabPage).Title = mFileManager.OntologyManager.GetText(mActivity.NodeId)
            End If
        End If
    End Sub

    Public Sub BuildInterface(ByVal aContainer As Control, ByRef pos As Point, ByVal mandatory_only As Boolean)
        If Not mActionSpecification Is Nothing Then
            mActionSpecification.BuildInterface(aContainer, pos, mandatory_only)
        End If
    End Sub

    Private Sub butGetAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butGetAction.Click
        Dim fd As New OpenFileDialog
        Dim s As String

        s = ReferenceModel.ReferenceModelName & "-ACTION"
        fd.Filter = s & "|" & s & ".*.adl"
        fd.InitialDirectory = OceanArchetypeEditor.Instance.Options.RepositoryPath & "\entry\action"

        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim ss As String

            ss = fd.FileName.Substring(fd.FileName.LastIndexOf("\") + s.Length + 2)
            'JAR: 07MAY2007, EDT-28 Display filename should match that when loaded (i.e. ArchetypeId)
            'Me.txtAction.Text = ss.Substring(0, ss.LastIndexOf(".")).Replace(".", "\.")
            Me.txtAction.Text = ss.Substring(0, ss.LastIndexOf("."))
        End If
    End Sub

    Private Sub menuItemRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        Dim a_term As RmTerm = mFileManager.OntologyManager.GetTerm(mActivity.NodeId)

        Dim s() As String = OceanArchetypeEditor.Instance.GetInput(a_term, Me.ParentForm)

        If s(0) <> "" Then
            CType(Me.Parent, Crownwood.Magic.Controls.TabPage).Title = a_term.Text
            mFileManager.OntologyManager.SetText(a_term)
            mFileManager.FileEdited = True
        End If
    End Sub

    'JAR: 30MAY07, EDT-44 Multiple activities per instruction
    Private Sub menuItemRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click        
        If Not mTabPageInstruction Is Nothing Then
            mTabPageInstruction.RemoveActivity()
        End If
    End Sub

    Private Sub txtAction_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAction.TextChanged
        If Not mIsloading Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub butOpenArchetype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpenArchetype.Click
        Try
            Dim start_info As New ProcessStartInfo
            Dim action_name As String
            Dim regx As System.Text.RegularExpressions.Regex
            Dim matchingFileNames As New ArrayList

            start_info.FileName = Application.ExecutablePath
            start_info.WorkingDirectory = Application.StartupPath


            ' get the name of the action
            action_name = Me.txtAction.Text
            regx = New System.Text.RegularExpressions.Regex(action_name)

            Dim dirinfo As System.IO.DirectoryInfo
            dirinfo = New System.IO.DirectoryInfo(OceanArchetypeEditor.Instance.Options.RepositoryPath & _
                "\entry\action\")

            For Each f As System.IO.FileInfo In dirinfo.GetFiles("*.adl")
                If regx.Match(f.Name).Success Then
                    matchingFileNames.Add(f.Name)
                End If
            Next

            Select Case matchingFileNames.Count
                Case 0
                    MessageBox.Show(AE_Constants.Instance.Could_not_find & " " & action_name, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Case 1
                    action_name = CStr(matchingFileNames(0))
                Case Else
                    'more than one
                    Dim frm As New Choose
                    frm.Set_Single()
                    frm.ListChoose.Items.AddRange(matchingFileNames.ToArray)
                    If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                        action_name = CStr(frm.ListChoose.SelectedItem)
                    Else
                        Return
                    End If
            End Select

            action_name = OceanArchetypeEditor.Instance.Options.RepositoryPath & _
                "\entry\action\" & action_name
            
            If IO.File.Exists(action_name) Then
                start_info.Arguments = Chr(34) & action_name & Chr(34) 'JAR: 07MAY2007, EDT-28 File name is split on space characters
                Process.Start(start_info)
            Else
                MessageBox.Show(AE_Constants.Instance.Could_not_find & " '" & action_name & "'", _
                    AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch
            MessageBox.Show(AE_Constants.Instance.Error_loading & " Archetype Editor", AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub toRichText(ByRef text As IO.StringWriter, ByVal level As Integer)
        text.WriteLine("\par Action archetype: \par")
        text.WriteLine("      " & Me.txtAction.Text & "\par")

        If Not mActionSpecification Is Nothing Then
            text.WriteLine("\par Action specification: \par")
            text.WriteLine("\par")
            mActionSpecification.toRichText(text, level + 1)
        End If
    End Sub

    Public Sub Reset()
        Me.txtAction.Text = ""
        mActionSpecification = New TabPageStructure
        Me.lblNodeId.Text = ""
    End Sub

    'JAR: 30MAY07, EDT-44 Multiple activities per instruction
    Public Sub ShowPopUp()
        Me.ContextMenuStrip1.Show()
    End Sub

    Public Sub ShowPopUp(ByVal location As Drawing.Point)
        Me.ContextMenuStrip1.Show(location)
    End Sub

    'JAR: 30MAY07, EDT-44 Multiple activities per instruction
    'Public Sub New()
    Public Sub New(ByVal ParentTabPageInstruction As TabPageInstruction)
        mTabPageInstruction = ParentTabPageInstruction

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mFileManager = Filemanager.Master
        If mActionSpecification Is Nothing Then
            mActionSpecification = New TabPageStructure()
        End If
        Me.Controls.Add(mActionSpecification)
        mActionSpecification.BringToFront()
        mActionSpecification.Dock = DockStyle.Fill
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        RenameToolStripMenuItem.Text = AE_Constants.Instance.Rename & " - " & CType(Me.Parent, Crownwood.Magic.Controls.TabPage).Title
        RemoveToolStripMenuItem.Text = AE_Constants.Instance.Remove & " - " & CType(Me.Parent, Crownwood.Magic.Controls.TabPage).Title 'JAR: 30MAY07, EDT-44 Multiple activities per instruction
    End Sub
End Class
