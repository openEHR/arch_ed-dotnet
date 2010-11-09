Public Class Participation

    Private mRestrictedModeSet As New RestrictedSet
    Private mOccurrences As New OccurrencesPanel(Filemanager.Master)
    Private mParticipation As New RmParticipation
    Private mParticipationFunction As New TextConstraintControl(Filemanager.Master)
    Private mIsLoading As Boolean = True

    Public Property ParticipationConstraint() As RmParticipation
        Get
            mParticipation.ModeSet = mRestrictedModeSet.AsCodePhrase
            mParticipation.FunctionConstraint = mParticipationFunction.TextConstraint
            Return mParticipation
        End Get
        Set(ByVal value As RmParticipation)
            mIsLoading = True
            mParticipation = value
            mParticipationFunction.TextConstraint = mParticipation.FunctionConstraint
            mOccurrences.Cardinality = mParticipation.Occurrences
            mRestrictedModeSet.LocalFileManager = Filemanager.Master
            mRestrictedModeSet.AsCodePhrase = mParticipation.ModeSet
            cbDateTime.Checked = mParticipation.MandatoryDateTime
            mIsLoading = False
        End Set
    End Property

    Private Sub Participation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mRestrictedModeSet.LocalFileManager = Filemanager.Master
        mRestrictedModeSet.TermSetToRestrict = RestrictedSet.TermSet.ParticipationMode
        panelLeft.Controls.Add(mOccurrences)
        mOccurrences.Dock = DockStyle.Top
        panelLeft.Controls.Add(mRestrictedModeSet)
        mRestrictedModeSet.BringToFront()
        mRestrictedModeSet.Dock = DockStyle.Fill

        If Not mParticipationFunction.HasConstraint Then
            'if it has not been set from the archetype
            mParticipationFunction.TextConstraint = New Constraint_Text
        End If

        gbFunction.Controls.Add(mParticipationFunction)
        mParticipationFunction.Dock = DockStyle.Fill

        If Main.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        mIsLoading = False
    End Sub

    Public Property Selected() As Boolean
        Get
            Return butSelector.Checked
        End Get
        Set(ByVal value As Boolean)
            butSelector.Checked = False
        End Set
    End Property

    Private Sub Participation_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        butSelector.Checked = True
    End Sub

    Public Sub TranslateGUI()
        gbFunction.Text = Filemanager.GetOpenEhrTerm(658, "Participant function")
        gbConstraints.Text = Filemanager.GetOpenEhrTerm(87, "Constraints")
        lblDateTime.Text = Filemanager.GetOpenEhrTerm(12, "Date and time")
        cbDateTime.Text = Filemanager.GetOpenEhrTerm(446, "mandatory")
        mRestrictedModeSet.TranslateGUI()
        mOccurrences.TranslateGUI()
    End Sub

    Public Sub Translate()
        mRestrictedModeSet.Translate()
        mParticipationFunction.TextConstraint = mParticipation.FunctionConstraint
    End Sub

    Private Sub cbDateTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDateTime.CheckedChanged
        If Not mIsLoading Then
            mParticipation.MandatoryDateTime = cbDateTime.Checked
            Filemanager.Master.FileEdited = True
        End If
    End Sub

    Public Function HasConstraint() As Boolean
        Return mRestrictedModeSet.HasRestriction() OrElse cbDateTime.Checked OrElse mParticipationFunction.TextConstraint.TypeOfTextConstraint <> TextConstrainType.Text
    End Function

End Class
