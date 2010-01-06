
Public Enum OccurrencesMode
    Numeric
    Lexical
End Enum

Public Class OccurrencesPanel
    Inherits System.Windows.Forms.UserControl
    Private mMode As OccurrencesMode
    Private WithEvents mCardinality As New RmCardinality
    Private mIsLoading As Boolean = False
    Private mIsSingle As Boolean = False
    Private mIncludeOrdered As Boolean = False
    Private mFileManager As FileManagerLocal


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal a_filemanager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = a_filemanager

        'SRH: 24 Aug 2009 [EDT-576] - set mode before setting values (was in load)
        If OceanArchetypeEditor.Instance.Options.OccurrencesView = "numeric" Then
            Me.Mode = OccurrencesMode.Numeric
        Else
            Me.Mode = OccurrencesMode.Lexical
        End If
        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.TranslateGUI()
        End If

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
    Friend WithEvents cbOrdered As System.Windows.Forms.CheckBox
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
        Me.cbOrdered = New System.Windows.Forms.CheckBox
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
        Me.panelNumeric.Location = New System.Drawing.Point(0, 128)
        Me.panelNumeric.Name = "panelNumeric"
        Me.panelNumeric.Size = New System.Drawing.Size(376, 32)
        Me.panelNumeric.TabIndex = 3
        '
        'lblNumMax
        '
        Me.lblNumMax.Location = New System.Drawing.Point(128, 3)
        Me.lblNumMax.Name = "lblNumMax"
        Me.lblNumMax.Size = New System.Drawing.Size(48, 16)
        Me.lblNumMax.TabIndex = 15
        Me.lblNumMax.Text = "Max:"
        Me.lblNumMax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumMin
        '
        Me.lblNumMin.BackColor = System.Drawing.Color.Transparent
        Me.lblNumMin.Location = New System.Drawing.Point(16, 3)
        Me.lblNumMin.Name = "lblNumMin"
        Me.lblNumMin.Size = New System.Drawing.Size(48, 16)
        Me.lblNumMin.TabIndex = 0
        Me.lblNumMin.Text = "Min:"
        Me.lblNumMin.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbUnbounded
        '
        Me.cbUnbounded.Location = New System.Drawing.Point(248, 0)
        Me.cbUnbounded.Name = "cbUnbounded"
        Me.cbUnbounded.Size = New System.Drawing.Size(120, 24)
        Me.cbUnbounded.TabIndex = 25
        Me.cbUnbounded.Text = "Unbounded"
        '
        'gbOccurrences
        '
        Me.gbOccurrences.Location = New System.Drawing.Point(0, 24)
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
        Me.panelLexical.Location = New System.Drawing.Point(0, 88)
        Me.panelLexical.Name = "panelLexical"
        Me.panelLexical.Size = New System.Drawing.Size(376, 32)
        Me.panelLexical.TabIndex = 2
        '
        'comboRepeat
        '
        Me.comboRepeat.Items.AddRange(New Object() {"not repeating", "repeating - no limit", "repeating - limited"})
        Me.comboRepeat.Location = New System.Drawing.Point(115, 0)
        Me.comboRepeat.Name = "comboRepeat"
        Me.comboRepeat.Size = New System.Drawing.Size(149, 21)
        Me.comboRepeat.TabIndex = 1
        '
        'comboOptional
        '
        Me.comboOptional.Items.AddRange(New Object() {"optional", "mandatory"})
        Me.comboOptional.Location = New System.Drawing.Point(5, 0)
        Me.comboOptional.Name = "comboOptional"
        Me.comboOptional.Size = New System.Drawing.Size(104, 21)
        Me.comboOptional.TabIndex = 0
        '
        'lblDash
        '
        Me.lblDash.Location = New System.Drawing.Point(302, 4)
        Me.lblDash.Name = "lblDash"
        Me.lblDash.Size = New System.Drawing.Size(32, 16)
        Me.lblDash.TabIndex = 15
        Me.lblDash.Text = "-"
        Me.lblDash.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'numMin
        '
        Me.numMin.Location = New System.Drawing.Point(232, 160)
        Me.numMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMin.Name = "numMin"
        Me.numMin.Size = New System.Drawing.Size(40, 20)
        Me.numMin.TabIndex = 10
        '
        'numMax
        '
        Me.numMax.Location = New System.Drawing.Point(280, 160)
        Me.numMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numMax.Name = "numMax"
        Me.numMax.Size = New System.Drawing.Size(40, 20)
        Me.numMax.TabIndex = 20
        Me.numMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cbOrdered
        '
        Me.cbOrdered.Location = New System.Drawing.Point(9, 0)
        Me.cbOrdered.Name = "cbOrdered"
        Me.cbOrdered.Size = New System.Drawing.Size(112, 24)
        Me.cbOrdered.TabIndex = 0
        Me.cbOrdered.Text = "Ordered"
        Me.cbOrdered.Visible = False
        '
        'OccurrencesPanel
        '
        Me.Controls.Add(Me.numMax)
        Me.Controls.Add(Me.numMin)
        Me.Controls.Add(Me.panelLexical)
        Me.Controls.Add(Me.gbOccurrences)
        Me.Controls.Add(Me.panelNumeric)
        Me.Controls.Add(Me.cbOrdered)
        Me.Name = "OccurrencesPanel"
        Me.Size = New System.Drawing.Size(376, 208)
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
            gbOccurrences.Controls.Clear()

            If mMode = OccurrencesMode.Lexical Then
                numMin.Location = New Drawing.Point(268, 0)
                panelLexical.Controls.Add(numMin)
                numMin.BringToFront()
                numMax.Location = New Drawing.Point(324, 0)
                panelLexical.Controls.Add(numMax)
                numMax.BringToFront()
                panelLexical.Dock = DockStyle.Fill
                gbOccurrences.Controls.Add(panelLexical)
            Else
                numMin.Location = New Drawing.Point(80, 0)
                panelNumeric.Controls.Add(numMin)
                numMax.Location = New Drawing.Point(185, 0)
                panelNumeric.Controls.Add(numMax)
                panelNumeric.Dock = DockStyle.Fill
                gbOccurrences.Controls.Add(panelNumeric)
            End If
        End Set
    End Property

    Public WriteOnly Property LocalFileManager() As FileManagerLocal
        Set(ByVal Value As FileManagerLocal)
            mFileManager = Value
        End Set
    End Property

    Private Sub CardinalityUpdatedExternally(ByVal sender As Object, ByVal e As EventArgs) Handles mCardinality.Updated

        If Not mIsLoading Then
            UpdateControl()
        End If

    End Sub

    Public Property IsContainer() As Boolean
        Get
            Return mIncludeOrdered
        End Get
        Set(ByVal Value As Boolean)
            mIncludeOrdered = Value
            cbOrdered.Visible = Value

            If Value Then
                ' it is a container then default max is unbounded
                If mCardinality.IsDefault Then
                    mCardinality.IsUnbounded = True
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property SetSingle() As Boolean
        Set(ByVal Value As Boolean)
            mIsLoading = True
            mIsSingle = Value
            mIncludeOrdered = False

            If Value Then
                numMin.Value = 1
                numMax.Value = 1
                cbUnbounded.Checked = False
                Enabled = False

                If mMode = OccurrencesMode.Lexical Then
                    comboOptional.SelectedIndex = 1
                End If
            Else
                Enabled = True
            End If

            mIsLoading = False
        End Set
    End Property

    Public WriteOnly Property SetMandatory() As Boolean
        Set(ByVal Value As Boolean)
            mIsLoading = True
            mIncludeOrdered = False

            If Value Then
                numMin.Value = 1
                numMin.Minimum = 1
                numMax.Minimum = 1
                
                If mMode = OccurrencesMode.Lexical Then
                    comboOptional.SelectedIndex = 1
                End If
            Else
                numMin.Minimum = 0
                numMax.Minimum = 0
            End If

            mIsLoading = False
        End Set
    End Property

    Public WriteOnly Property SetUnitary() As Boolean
        Set(ByVal Value As Boolean)
            mIsLoading = True
            mIncludeOrdered = False

            If Value Then
                numMax.Value = 1
                numMax.Maximum = 1
                numMin.Maximum = 1
                cbUnbounded.Checked = False
                cbUnbounded.Visible = False
                If mMode = OccurrencesMode.Lexical Then
                    comboOptional.SelectedIndex = 1
                End If
            Else
                numMax.Maximum = 1000
                numMin.Maximum = 1000
                cbUnbounded.Visible = True
            End If

            mIsLoading = False
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

        ' Set for cardinality if appropriate
        If mIncludeOrdered Then
            Height = gbOccurrences.Height + 40
            gbOccurrences.Text = AE_Constants.Instance.Cardinality
            cbOrdered.Show()
        Else
            Size = gbOccurrences.Size
            cbOrdered.Hide()
            gbOccurrences.Dock = DockStyle.Fill
        End If

        'If OceanArchetypeEditor.Instance.Options.OccurrencesView = "numeric" Then
        '   Mode = OccurrencesMode.Numeric
        'Else
        If Mode = OccurrencesMode.Lexical Then

            If comboRepeat.SelectedIndex = -1 Then
                If mCardinality.IsUnbounded Then
                    comboRepeat.SelectedIndex = 1
                Else
                    comboRepeat.SelectedIndex = 0
                End If
            End If

            If comboOptional.SelectedIndex = -1 Then
                comboOptional.SelectedIndex = 0
            End If
        End If

        mIsLoading = False
    End Sub

    Private Sub UpdateControl()
        mIsLoading = True

        'SRH: 23 Jun 2009 EDT-514
        If numMin.Minimum <= mCardinality.MinCount Then
            numMin.Value = mCardinality.MinCount
        Else
            mCardinality.MinCount = numMin.Minimum
        End If
        If mMode = OccurrencesMode.Lexical Then
            If mCardinality.MinCount = 0 Then
                comboOptional.SelectedIndex = 0 ' optional
            Else
                comboOptional.SelectedIndex = 1 ' mandatory
            End If

            If mCardinality.IsUnbounded Then
                comboRepeat.SelectedIndex = 1 ' unlimited
            ElseIf mCardinality.MaxCount = 1 Then
                comboRepeat.SelectedIndex = 0 ' no repeat
            Else
                comboRepeat.SelectedIndex = 2 ' limited
            End If
        Else
            cbUnbounded.Checked = mCardinality.IsUnbounded
        End If

        If Not mCardinality.IsUnbounded Then
            numMax.Value = mCardinality.MaxCount
        End If

        If mIncludeOrdered Then
            cbOrdered.Checked = mCardinality.Ordered
        End If

        mIsLoading = False
    End Sub

    Private Sub comboRepeat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboRepeat.SelectedIndexChanged
        If Not mIsLoading Then
            Select Case comboRepeat.SelectedIndex
                Case -1
                    Debug.Assert(False)
                Case 0 ' no repeat
                    numMax.Value = 1
                Case 1 'unlimited repeat
                    mCardinality.IsUnbounded = True
                Case 2 'limited repeat
                    'no action
            End Select

            mFileManager.FileEdited = True
        End If

        SetGUI()
    End Sub

    Sub SetGUI()
        Select Case comboRepeat.SelectedIndex
            Case 0 ' no repeat
                If comboOptional.SelectedIndex = 0 Then
                    lblDash.Text = "0..1"
                    numMin.Hide()
                    numMax.Hide()
                ElseIf comboOptional.SelectedIndex = 1 Then
                    lblDash.Text = "1..1"
                    numMin.Hide()
                    numMax.Hide()
                End If
            Case 1 ' repeating no limit
                If comboOptional.SelectedIndex = 0 Then
                    lblDash.Text = "0..*"
                    numMin.Hide()
                    numMax.Hide()
                ElseIf comboOptional.SelectedIndex = 1 Then
                    lblDash.Text = "..*"
                    numMin.Show()
                    numMax.Hide()
                End If
            Case 2 ' repeating, limited
                If comboOptional.SelectedIndex = 0 Then
                    lblDash.Text = "0.."
                    numMin.Hide()
                    numMax.Show()
                ElseIf comboOptional.SelectedIndex = 1 Then
                    lblDash.Text = ".."
                    numMin.Show()
                    numMax.Show()
                End If

                If numMax.Value < 2 Then
                    numMax.Value = 2
                End If
        End Select
    End Sub

    Private Sub comboOptional_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboOptional.SelectedIndexChanged
        If Not mIsLoading Then
            numMin.Value = comboOptional.SelectedIndex
            mFileManager.FileEdited = True
        End If

        SetGUI()
    End Sub

    Private Sub numMin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMin.TextChanged ', numMax.ValueChanged
        If Not mIsLoading Then
            mFileManager.FileEdited = True
            'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
            mIsLoading = True

            If mMode = OccurrencesMode.Lexical Then
                'mIsLoading = True

                If numMin.Value = 0 Then
                    comboOptional.SelectedIndex = 0
                Else
                    comboOptional.SelectedIndex = 1
                End If

                'mIsLoading = False
            End If

            mCardinality.MinCount = numMin.Value

            If numMax.Value < numMin.Value Then
                'Protect max val being changed if unbounded
                mIsLoading = cbUnbounded.Checked
                numMax.Value = numMin.Value
                'mIsLoading = False
            End If

            mIsLoading = False

        End If
    End Sub

    Private Sub numMax_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numMax.TextChanged ', numMax.ValueChanged
        If Not mIsLoading Then
            mFileManager.FileEdited = True

            'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
            mIsLoading = True

            If mMode = OccurrencesMode.Lexical Then
                'mIsLoading = True

                If numMax.Value = 1 Then
                    comboRepeat.SelectedIndex = 0
                Else
                    comboRepeat.SelectedIndex = 2
                End If

                'mIsLoading = False
            End If

            mCardinality.MaxCount = numMax.Value

            mIsLoading = False

            If numMin.Value > numMax.Value Then
                numMin.Value = numMax.Value
            End If

        End If
    End Sub

    Private Sub cbUnbounded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUnbounded.CheckedChanged
        numMax.Visible = Not cbUnbounded.Checked

        If Not mIsLoading Then
            'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
            mIsLoading = True

            If Not cbUnbounded.Checked Then
                mCardinality.MaxCount = numMax.Value
            End If

            mCardinality.IsUnbounded = cbUnbounded.Checked

            'SRH: 11 jan 2009 - EDT-502 - prevent recursive loops with auto set of minimum value based on events
            mIsLoading = False

            mFileManager.FileEdited = True
        End If
    End Sub

    Sub TranslateGUI()
        Dim i As Integer
        mIsLoading = True
        gbOccurrences.Text = Filemanager.GetOpenEhrTerm(110, "Occurrences")
        lblNumMin.Text = Filemanager.GetOpenEhrTerm(588, "Min:")
        lblNumMax.Text = Filemanager.GetOpenEhrTerm(111, "Max:")
        i = comboOptional.SelectedIndex
        comboOptional.Items.Clear()
        comboOptional.Items.Add(Filemanager.GetOpenEhrTerm(448, "optional"))
        comboOptional.Items.Add(Filemanager.GetOpenEhrTerm(446, "mandatory"))
        comboOptional.SelectedIndex = i
        i = comboRepeat.SelectedIndex
        comboRepeat.Items.Clear()
        comboRepeat.Items.Add(Filemanager.GetOpenEhrTerm(589, "not repeating"))
        comboRepeat.Items.Add(Filemanager.GetOpenEhrTerm(590, "repeating, no limit"))
        comboRepeat.Items.Add(Filemanager.GetOpenEhrTerm(591, "repeating, limited"))
        comboRepeat.SelectedIndex = i
        cbUnbounded.Text = Filemanager.GetOpenEhrTerm(112, "Unbounded")
        cbOrdered.Text = Filemanager.GetOpenEhrTerm(162, "Ordered")
        mIsLoading = False
    End Sub

    Private Sub cbOrdered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdered.CheckedChanged
        If Not mIsLoading Then
            mCardinality.Ordered = cbOrdered.Checked
            mFileManager.FileEdited = True
        End If
    End Sub

End Class
