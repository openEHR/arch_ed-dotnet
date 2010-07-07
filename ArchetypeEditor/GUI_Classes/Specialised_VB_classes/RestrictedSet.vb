Public Class RestrictedSet

    Public Enum TermSet
        'Numeric is the group id in openEHR terminology
        None = 0
        SubjectOfData = 1
        ParticipationMode = 9
        Setting = 10
    End Enum

    Private mFileManager As FileManagerLocal

    Private Sub radioRestrictedSet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioRestrictedSet.CheckedChanged
        If Not mFileManager Is Nothing Then
            If radioRestrictedSet.Checked Then
                listRestrictionSet.Show()
                butAddToRestrictedSet.Show()
                butRemoveFromRestrictedSet.Show()
            Else
                butAddToRestrictedSet.Hide()
                listRestrictionSet.Hide()
                butRemoveFromRestrictedSet.Hide()
            End If

            If mFileManager.FileLoading Then
                Return
            Else
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Public WriteOnly Property Title() As String
        Set(ByVal value As String)
            gbRestrictedData.Text = value
        End Set
    End Property

    Public WriteOnly Property LocalFileManager() As FileManagerLocal
        Set(ByVal value As FileManagerLocal)
            mFileManager = value
        End Set
    End Property

    Private Sub butAddToRestrictedSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddToRestrictedSet.Click
        If ChooseFromTermSet(mTermSet) Then
            mFileManager.FileEdited = True
        End If
    End Sub

    Public Property HasRestriction() As Boolean
        Get
            Return radioRestrictedSet.Checked AndAlso listRestrictionSet.Items.Count > 0
        End Get
        Set(ByVal value As Boolean)
            If radioRestrictedSet.Checked <> value Then
                radioRestrictedSet.Checked = value
            End If
        End Set
    End Property

    Private mTermSet As TermSet

    Friend Property TermSetToRestrict() As TermSet
        Get
            Return mTermSet
        End Get
        Set(ByVal value As TermSet)
            mTermSet = value

            Select Case value
                Case TermSet.None
                    Hide()
                Case RestrictedSet.TermSet.SubjectOfData
                    Title = Filemanager.GetOpenEhrTerm(32, "Subject of data")
                    Show()
                Case RestrictedSet.TermSet.Setting
                    Title = Filemanager.GetOpenEhrTerm(226, "Setting")
                    Show()
                Case TermSet.ParticipationMode
                    Title = Filemanager.GetOpenEhrTerm(192, "Participation mode")
                    Show()
                Case Else
                    Hide()
                    Debug.Assert(False, "Need to handle this sort of termset")
            End Select
        End Set
    End Property

    Public Property AsCodePhrase() As CodePhrase
        Get
            Dim cp As New CodePhrase
            cp.TerminologyID = "openehr"

            For Each term As RmTerm In listRestrictionSet.Items
                cp.Codes.Add(term.Code)
            Next

            Return cp
        End Get
        Set(ByVal value As CodePhrase)
            For Each s As String In value.Codes
                Dim rt As New RmTerm(s)
                rt.Text = Filemanager.GetOpenEhrTerm(CInt(s), "?")
                listRestrictionSet.Items.Add(rt)
            Next

            If Not radioRestrictedSet.Checked Then
                radioRestrictedSet.Checked = True
            End If
        End Set
    End Property

    'Friend Function ChooseRestrictionSet() As Boolean

    '    Select Case mFileManager.Archetype.RmEntity
    '        Case StructureType.OBSERVATION, StructureType.EVALUATION, StructureType.ENTRY, StructureType.INSTRUCTION
    '            Return ChooseSubjectOfData(1) 'Subject of care

    '        Case StructureType.COMPOSITION
    '            Return ChooseSubjectOfData(10) ' Setting

    '    End Select

    'End Function

    Friend Function ChooseFromTermSet(ByVal aTermSet As TermSet) As Boolean
        Dim frm As New Choose
        Dim i As Integer
        Dim Subjects As DataRow()
        Dim new_row As DataRow

        ' add the language codes - FIXME - from a file in future
        frm.Set_Single()
        frm.PrepareDataTable_for_List(1)
        Subjects = mFileManager.OntologyManager.CodeForGroupID(aTermSet, mFileManager.OntologyManager.LanguageCode) 'subject of data
        frm.DTab_1.DefaultView.Sort = "Text"

        For i = 0 To Subjects.Length - 1
            new_row = frm.DTab_1.NewRow()
            new_row("Code") = Subjects(i).Item(1).ToString
            new_row("Text") = Subjects(i).Item(2)
            frm.DTab_1.Rows.Add(new_row)
        Next

        frm.ListChoose.DataSource = frm.DTab_1
        frm.ListChoose.DisplayMember = "Text"
        frm.ListChoose.ValueMember = "Code"

        frm.ListChoose.SelectionMode = SelectionMode.MultiExtended

        If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            ' check it is not a language added previously
            Dim drv As DataRowView

            For Each drv In frm.ListChoose.SelectedItems
                Dim a_term As New RmTerm(drv.Item("Code"))
                a_term.Text = drv.Item("Text")
                listRestrictionSet.Items.Add(a_term)
            Next
        Else
            Return False
        End If

        Return True
    End Function

    Private Sub butRemoveFromRestrictedSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRemoveFromRestrictedSet.Click
        Dim i As Integer

        i = listRestrictionSet.SelectedIndex()

        If i > -1 Then
            If MessageBox.Show(AE_Constants.Instance.Remove & listRestrictionSet.SelectedItem.text, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                listRestrictionSet.Items.RemoveAt(i)
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Public Function CSV() As String
        Dim result As New System.Text.StringBuilder

        For Each aTerm As RmTerm In listRestrictionSet.Items
            result.AppendFormat("'{0}', ", aTerm.Text)
        Next

        Return result.ToString.TrimEnd(", ".ToCharArray())
    End Function

    Public Sub Reset()
        listRestrictionSet.Items.Clear()
        radioUnrestrictedSubject.Checked = True
        Hide()
    End Sub

    Public Sub Translate()
        Dim aTerm As RmTerm

        If listRestrictionSet.Items.Count > 0 Then
            Dim i As Integer

            ' this approach is required to change the text in the list box
            ' as just reassigning the text does not make it available
            For i = 0 To listRestrictionSet.Items.Count - 1
                aTerm = listRestrictionSet.Items(i)
                aTerm.Text = Filemanager.GetOpenEhrTerm(Val(aTerm.Code), "*" & aTerm.Text)
                listRestrictionSet.Items(i) = aTerm
            Next
        End If
    End Sub

    Public Sub TranslateGUI()
        radioUnrestrictedSubject.Text = Filemanager.GetOpenEhrTerm(56, radioUnrestrictedSubject.Text)
        radioRestrictedSet.Text = Filemanager.GetOpenEhrTerm(599, radioRestrictedSet.Text)
    End Sub

End Class
