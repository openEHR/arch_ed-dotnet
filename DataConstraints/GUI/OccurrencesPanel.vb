
Public Enum OccurrencesMode
    Numeric
    Lexical
End Enum

Public Class OccurrencesPanel
    Inherits System.Windows.Forms.UserControl
    Private mMode As OccurrencesMode
    Private mCardinality As New RmCardinality
    Private mIsLoading As Boolean = False
    Private mIsSingle As Boolean = False


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents panelNumeric As System.Windows.Forms.Panel
    Friend WithEvents lblNumMax As System.Windows.Forms.Label
    Friend WithEvents lblNumMin As System.Windows.Forms.Label
    Friend WithEvents gbOccurrences As System.Windows.Forms.GroupBox
    Friend WithEvents comboOptional As System.Windows.Forms.ComboBox
    Friend WithEvents lblDash As System.Windows.Forms.Label
    Friend WithEvents panelLexical As System.Windows.Forms.Panel
    Friend WithEvents cbUnbounded As System.Windows.Forms.CheckBox
    Friend WithEvents comboRepeat As System.Windows.Forms.ComboBox
    Friend WithEvents numMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMax As System.Windows.Forms.NumericUpDown
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.panelNumeric = New System.Windows.Forms.Panel
        Me.lblNumMax = New System.Windows.Forms.Label
        Me.lblNumMin = New System.Windows.Forms.Label
        Me.cbUnbounded = New System.Windows.Forms.CheckBox
        Me.gbOccurrences = New System.Windows.Forms.GroupBox
        Me.panelLexical = New System.Windows.Forms.Panel
        Me.comboRepeat = New System.Windows.Forms.ComboBox
        Me.comboOptional = New System.Windows.Forms.ComboBox
        Me.lblDash = New System.Windows.Forms.Label
        Me.numMin = New System.Windows.Forms.NumericUpDown
        Me.numMax = New System.Windows.Forms.NumericUpDown
        Me.panelNumeric.SuspendLayout()
        Me.panelLexical.SuspendLayout()
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelNumeric
        '
        Me.panelNumeric.Controls.Add(Me.lblNumMax)
        Me.panelNumeric.Controls.Add(Me.lblNumMin)
        Me.panelNumeric.Controls.Add(Me.cbUnbounded)
        Me.panelNumeric.Location = New System.Drawing.Point(8, 0)
        Me.panelNumeric.Name = "panelNumeric"
        Me.panelNumeric.Size = New System.Drawing.Size(376, 32)
        Me.panelNumeric.TabIndex = 0
        '
        'lblNumMax
        '
        Me.lblNumMax.Location = New System.Drawing.Point(128, 3)
        Me.lblNumMax.Name = "lblNumMax"
        Me.lblNumMax.Size = New System.Drawing.Size(48, 16)
        Me.lblNumMax.TabIndex = 7
        Me.lblNumMax.Text = "Max:"
        Me.lblNumMax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumMin
        '
        Me.lblNumMin.BackColor = System.Drawing.Color.Transparent
        Me.lblNumMin.Location = New System.Drawing.Point(16, 3)
        Me.lblNumMin.Name = "lblNumMin"
        Me.lblNumMin.Size = New System.Drawing.Size(48, 16)
        Me.lblNumMin.TabIndex = 5
        Me.lblNumMin.Text = "Min:"
        Me.lblNumMin.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbUnbounded
        '
        Me.cbUnbounded.Location = New System.Drawing.Point(248, 0)
        Me.cbUnbounded.Name = "cbUnbounded"
        Me.cbUnbounded.Size = New System.Drawing.Size(120, 24)
        Me.cbUnbounded.TabIndex = 10
        Me.cbUnbounded.Text = "Unbounded"
        '
        'gbOccurrences
        '
        Me.gbOccurrences.Location = New System.Drawing.Point(8, 40)
        Me.gbOccurrences.Name = "gbOccurrences"
        Me.gbOccurrences.Size = New System.Drawing.Size(376, 48)
        Me.gbOccurrences.TabIndex = 1
        Me.gbOccurrences.TabStop = False
        Me.gbOccurrences.Text = "Occurrences"
        '
        'panelLexical
        '
        Me.panelLexical.Controls.Add(Me.comboRepeat)
        Me.panelLexical.Controls.Add(Me.comboOptional)
        Me.panelLexical.Controls.Add(Me.lblDash)
        Me.panelLexical.Location = New System.Drawing.Point(8, 104)
        Me.panelLexical.Name = "panelLexical"
        Me.panelLexical.Size = New System.Drawing.Size(376, 32)
        Me.panelLexical.TabIndex = 2
        '
        'comboRepeat
        '
        Me.comboRepeat.Items.AddRange(New Object() {"not repeating", "repeating - no limit", "repeating - limited"})
        Me.comboRepeat.Location = New System.Drawing.Point(115, 0)
        Me.comboRepeat.Name = "comboRepeat"
        Me.comboRepeat.Size = New System.Drawing.Size(149, 24)
        Me.comboRepeat.TabIndex = 2
        '
        'comboOptional
        '
        Me.comboOptional.Items.AddRange(New Object() {"optional", "mandatory"})
        Me.comboOptional.Location = New System.Drawing.Point(5, 0)
        Me.comboOptional.Name = "comboOptional"
        Me.comboOptional.Size = New System.Drawing.Size(104, 24)
        Me.comboOptional.TabIndex = 0
        '
        'lblDash
        '
        Me.lblDash.Location = New System.Drawing.Point(302, 4)
        Me.lblDash.Name = "lblDash"
        Me.lblDash.Size = New System.Drawing.Size(32, 16)
        Me.lblDash.TabIndex = 13
        Me.lblDash.Text = "-"
        Me.lblDash.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'numMin
        '
        Me.numMin.Location = New System.Drawing.Point(232, 160)
        Me.numMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMin.Name = "numMin"
        Me.numMin.Size = New System.Drawing.Size(40, 22)
        Me.numMin.TabIndex = 13
        '
        'numMax
        '
        Me.numMax.Location = New System.Drawing.Point(280, 160)
        Me.numMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMax.Name = "numMax"
        Me.numMax.Size = New System.Drawing.Size(40, 22)
        Me.numMax.TabIndex = 14
        Me.numMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'OccurrencesPanel
        '
        Me.Controls.Add(Me.numMax)
        Me.Controls.Add(Me.numMin)
        Me.Controls.Add(Me.panelLexical)
        Me.Controls.Add(Me.gbOccurrences)
        Me.Controls.Add(Me.panelNumeric)
        Me.Name = "OccurrencesPanel"
        Me.Size = New System.Drawing.Size(448, 208)
        Me.panelNumeric.ResumeLayout(False)
        Me.panelLexical.ResumeLayout(False)
        CType(Me.numMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Public Property Mode() As OccurrencesMode
        Get
            Return mMode
        End Get
        Set(ByVal Value As OccurrencesMode)
            mMode = Value
            Me.gbOccurrences.Controls.Clear()
            If mMode = OccurrencesMode.Lexical Then
                Me.numMin.Location = New Drawing.Point(268, 0)
                Me.panelLexical.Controls.Add(Me.numMin)
                Me.numMin.BringToFront()
                Me.numMax.Location = New Drawing.Point(324, 0)
                Me.panelLexical.Controls.Add(Me.numMax)
                Me.numMax.BringToFront()
                Me.panelLexical.Dock = DockStyle.Fill
                Me.gbOccurrences.Controls.Add(Me.panelLexical)
            Else
                Me.numMin.Location = New Drawing.Point(80, 0)
                Me.panelNumeric.Controls.Add(Me.numMin)
                Me.numMax.Location = New Drawing.Point(185, 0)
                Me.panelNumeric.Controls.Add(Me.numMax)
                Me.panelNumeric.Dock = DockStyle.Fill
                Me.gbOccurrences.Controls.Add(Me.panelNumeric)
            End If
        End Set
    End Property
    Public WriteOnly Property SetSingle() As Boolean
        Set(ByVal Value As Boolean)
            mIsSingle = Value
            If Value Then
                Me.numMin.Value = 1
                Me.numMax.Value = 1
                Me.cbUnbounded.Checked = False
                Me.Enabled = False
            Else
                Me.Enabled = True
            End If
        End Set
    End Property
    Public Property Cardinality() As RmCardinality
        Get
            Return mCardinality
        End Get
        Set(ByVal Value As RmCardinality)
            mCardinality = Value
            If Not mIsSingle Then
                UpdateControl()
            Else
                mCardinality.MaxCount = 1
                mCardinality.MinCount = 1
                mCardinality.IsUnbounded = False
            End If
        End Set
    End Property
    Public Property Title() As String
        Get
            Return gbOccurrences.Text
        End Get
        Set(ByVal Value As String)
            gbOccurrences.Text = Value
        End Set
    End Property

    Private Sub OccurrencesPanel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mIsLoading = True
        Me.Size = gbOccurrences.Size
        gbOccurrences.Dock = DockStyle.Fill
        If OceanArchetypeEditor.Instance.Options.OccurrencesView = "numeric" Then
            Me.Mode = OccurrencesMode.Numeric
        Else
            Me.Mode = OccurrencesMode.Lexical
            If Me.comboRepeat.SelectedIndex = -1 Then
                Me.comboRepeat.SelectedIndex = 0
            End If
            If Me.comboOptional.SelectedIndex = -1 Then
                Me.comboOptional.SelectedIndex = 0
            End If
        End If
        mIsLoading = False
    End Sub

    Private Sub UpdateControl()
        mIsLoading = True

        Me.numMin.Value = mCardinality.MinCount
        If mMode = OccurrencesMode.Lexical Then
            If mCardinality.MinCount = 0 Then
                Me.comboOptional.SelectedIndex = 0
            Else
                Me.comboOptional.SelectedIndex = 1
            End If
        End If

        If mCardinality.IsUnbounded Then
            If mMode = OccurrencesMode.Numeric Then
                Me.cbUnbounded.Checked = True
            Else
                Me.comboRepeat.SelectedIndex = 1
            End If
        Else
            Me.numMax.Value = mCardinality.MaxCount
            If mMode = OccurrencesMode.Lexical Then
                If mCardinality.MaxCount = 1 Then
                    Me.comboRepeat.SelectedIndex = 0 ' no repeat
                Else
                    Me.comboRepeat.SelectedIndex = 2 ' limited
                End If
            End If
        End If
        mIsLoading = False

    End Sub

    Private Sub comboRepeat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboRepeat.SelectedIndexChanged
        Select Case comboRepeat.SelectedIndex
            Case -1
                Debug.Assert(False)
            Case 0 ' no repeat
                If Not mIsLoading Then
                    Me.numMax.Value = 1
                End If
            Case 1 'unlimited repeat
                If Not mIsLoading Then
                    mCardinality.IsUnbounded = True
                End If
            Case 2 'limited repeat
                'no action
        End Select
        If Not mIsLoading Then
            Filemanager.Instance.FileEdited = True
        End If
        SetGUI()
    End Sub

    Sub SetGUI()
        Select Case comboRepeat.SelectedIndex
            Case 0 ' no repeat
                If comboOptional.SelectedIndex = 0 Then
                    lblDash.Text = "0..1"
                    numMin.Visible = False
                    numMax.Visible = False
                ElseIf comboOptional.SelectedIndex = 1 Then
                    lblDash.Text = "1..1"
                    numMin.Visible = False
                    numMax.Visible = False
                End If
            Case 1 ' repeating no limit
                If comboOptional.SelectedIndex = 0 Then
                    lblDash.Text = "0..*"
                    numMin.Visible = False
                    numMax.Visible = False
                ElseIf comboOptional.SelectedIndex = 1 Then
                    lblDash.Text = "..*"
                    numMin.Visible = True
                    numMax.Visible = False
                End If

            Case 2 ' repeating, limited
                If comboOptional.SelectedIndex = 0 Then
                    lblDash.Text = "0.."
                    numMin.Visible = False
                    numMax.Visible = True
                ElseIf comboOptional.SelectedIndex = 1 Then
                    lblDash.Text = ".."
                    numMin.Visible = True
                    numMax.Visible = True
                End If
                If numMax.Value < 2 Then
                    numMax.Value = 2
                End If
        End Select
    End Sub

    Private Sub comboOptional_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboOptional.SelectedIndexChanged
        If comboOptional.SelectedIndex = 0 Then
            'optional
            If Not mIsLoading Then
                Me.numMin.Value = 0
            End If
        Else
            'mandatory
            If Not mIsLoading Then
                Me.numMin.Value = 1
            End If
        End If
        If Not mIsLoading Then
            Filemanager.Instance.FileEdited = True
        End If
        SetGUI()
    End Sub

    Private Sub numMin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMin.TextChanged ', numMax.ValueChanged

        If Not mIsLoading Then
            Filemanager.Instance.FileEdited = True
            If mMode = OccurrencesMode.Lexical Then
                mIsLoading = True
                If numMin.Value = 0 Then
                    comboOptional.SelectedIndex = 0
                Else
                    comboOptional.SelectedIndex = 1
                End If
                mIsLoading = False
            End If
            mCardinality.MinCount = Me.numMin.Value
            If Me.numMax.Value <= Me.numMin.Value Then
                Me.numMax.Value = Me.numMin.Value
            End If
        End If
    End Sub

    Private Sub numMax_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMax.TextChanged ', numMax.ValueChanged

        If Not mIsLoading Then
            Filemanager.Instance.FileEdited = True
            If mMode = OccurrencesMode.Lexical Then
                mIsLoading = True
                If numMax.Value = 1 Then
                    Me.comboRepeat.SelectedIndex = 0
                Else
                    Me.comboRepeat.SelectedIndex = 2
                End If
                mIsLoading = False
            End If
            mCardinality.MaxCount = Me.numMax.Value
            If Me.numMax.Value < Me.numMin.Value Then
                Me.numMin.Value = Me.numMax.Value
            End If
        End If
    End Sub

    Private Sub cbUnbounded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUnbounded.CheckedChanged
        If Not mIsLoading Then
            mCardinality.IsUnbounded = cbUnbounded.Checked
        End If
        If Not Me.cbUnbounded.Checked Then
            Me.numMax.Visible = True
            If Not mIsLoading Then
                mCardinality.MaxCount = Me.numMax.Value
            End If
        Else
            Me.numMax.Visible = False
        End If
        If Not mIsLoading Then
            Filemanager.Instance.FileEdited = True
        End If
    End Sub

    Sub Translate()
        Dim i As Integer
        mIsLoading = True
        Me.gbOccurrences.Text = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(110, "Occurrences")
        Me.lblNumMin.Text = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(588, "Min:")
        Me.lblNumMax.Text = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(111, "Max:")
        i = Me.comboOptional.SelectedIndex
        Me.comboOptional.Items.Clear()
        Me.comboOptional.Items.Add(Filemanager.Instance.OntologyManager.GetOpenEHRTerm(448, "optional"))
        Me.comboOptional.Items.Add(Filemanager.Instance.OntologyManager.GetOpenEHRTerm(446, "mandatory"))
        Me.comboOptional.SelectedIndex = i
        i = Me.comboRepeat.SelectedIndex
        Me.comboRepeat.Items.Clear()
        Me.comboRepeat.Items.Add(Filemanager.Instance.OntologyManager.GetOpenEHRTerm(589, "not repeating"))
        Me.comboRepeat.Items.Add(Filemanager.Instance.OntologyManager.GetOpenEHRTerm(590, "repeating, no limit"))
        Me.comboRepeat.Items.Add(Filemanager.Instance.OntologyManager.GetOpenEHRTerm(591, "repeating, limited"))
        Me.comboRepeat.SelectedIndex = i
        Me.cbUnbounded.Text = Filemanager.Instance.OntologyManager.GetOpenEHRTerm(112, "Unbounded")
        mIsLoading = False

    End Sub

End Class
