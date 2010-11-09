Public Class Link

    Private mOccurrences As New OccurrencesPanel(Filemanager.Master)
    Private mLink As New RmLink
    Private mLinkType As New TextConstraintControl(Filemanager.Master)
    Private mLinkMeaning As New TextConstraintControl(Filemanager.Master)
    Private mIsLoading As Boolean = True

    Public Property LinkConstraint() As RmLink
        Get
            mLink.Meaning = mLinkMeaning.TextConstraint
            'Occurrences set automatically
            mLink.LinkType = mLinkType.TextConstraint
            mLink.Target.RegularExpression = Me.txtTarget.Text
            Return mLink
        End Get
        Set(ByVal value As RmLink)
            mIsLoading = True
            mLink = value
            mLinkMeaning.TextConstraint = mLink.Meaning
            mOccurrences.Cardinality = mLink.Occurrences
            mLinkType.TextConstraint = mLink.LinkType
            Me.txtTarget.Text = mLink.Target.RegularExpression
            mIsLoading = False
        End Set
    End Property

    Private Sub Link_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PanelTop.Controls.Add(mOccurrences)
        mOccurrences.Dock = DockStyle.Left
        gbTarget.Dock = DockStyle.Fill

        If Not mLinkType.HasConstraint Then
            'if it has not been set from the archetype
            mLinkType.TextConstraint = New Constraint_Text
        End If

        mLinkType.BringToFront()
        gbType.Controls.Add(mLinkType)
        mLinkType.Dock = DockStyle.Fill

        If Not mLinkMeaning.HasConstraint Then
            'if it has not been set from the archetype
            mLinkMeaning.TextConstraint = New Constraint_Text
        End If

        mLinkMeaning.BringToFront()
        gbMeaning.Controls.Add(mLinkMeaning)
        mLinkMeaning.Dock = DockStyle.Fill

        If Main.Instance.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        mIsLoading = False
    End Sub

    Public Property Selected() As Boolean
        Get
            Return Me.butSelector.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.butSelector.Checked = False
        End Set
    End Property

    Private Sub Link_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        Me.butSelector.Checked = True
    End Sub

    Public Sub TranslateGUI()
        gbType.Text = Filemanager.GetOpenEhrTerm(443, "Type")
        gbTarget.Text = Filemanager.GetOpenEhrTerm(661, "Target")
        Me.gbMeaning.Text = Filemanager.GetOpenEhrTerm(662, "Meaning")
        mOccurrences.TranslateGUI()
    End Sub

    Public Sub Translate()
        mLinkMeaning.TextConstraint = mLink.Meaning
        mLinkType.TextConstraint = mLink.LinkType
    End Sub

    Public Function HasConstraint() As Boolean
        Return mLinkType.TextConstraint.TypeOfTextConstraint <> TextConstrainType.Text OrElse _
            mLinkMeaning.TextConstraint.TypeOfTextConstraint <> TextConstrainType.Text OrElse _
            txtTarget.Text <> String.Empty
    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mOccurrences.Cardinality = mLink.Occurrences

    End Sub

End Class
