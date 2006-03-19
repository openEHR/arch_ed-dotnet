Public Class PathwayEvent
    Inherits System.Windows.Forms.UserControl

    Private mSelectedPenWidth As Integer = 3
    Private mDefaultWidth As Integer = 80
    Private mSelected As Boolean

    Private mText As String
    Private mDescription As String
    Private mItem As RmPathwayStep
    Private mFileManager As FileManagerLocal

    Public Event SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Deleted(ByVal sender As Object, ByVal e As EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            Debug.Assert(False) ' should only be used in design view
        End If
    End Sub

    Sub New(ByVal DefaultMachineStateType As StateMachineType, ByVal a_filemanager As FileManagerLocal)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mFileManager = a_filemanager
        Dim a_term As RmTerm = mFileManager.OntologyManager.AddTerm("New Pathway step")
        mText = a_term.Text
        mItem = New RmPathwayStep(a_term.Code, DefaultMachineStateType)
        Me.BackColor = OceanArchetypeEditor.Instance.Options.StateMachineColour(DefaultMachineStateType)
        MenuEdit_Click(Me, New EventArgs)
    End Sub

    Sub New(ByVal rm As RmPathwayStep, ByVal a_filemanager As FileManagerLocal)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mFileManager = a_filemanager
        mItem = rm
        Me.Translate()
        If mItem.HasAlternativeState Then
            Me.BackColor = OceanArchetypeEditor.Instance.Options.StateMachineColour(rm.AlternativeState)
        Else
            Me.BackColor = OceanArchetypeEditor.Instance.Options.StateMachineColour(rm.StateType)
        End If
        Me.TextRectangle(mText, Me.CreateGraphics)
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
    Friend WithEvents ContextMenuPathwayEvent As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ContextMenuPathwayEvent = New System.Windows.Forms.ContextMenu
        Me.MenuEdit = New System.Windows.Forms.MenuItem
        Me.MenuDelete = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        '
        'ContextMenuPathwayEvent
        '
        Me.ContextMenuPathwayEvent.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuEdit, Me.MenuDelete, Me.MenuItem1})
        '
        'MenuEdit
        '
        Me.MenuEdit.Index = 0
        Me.MenuEdit.Text = "Edit text"
        '
        'MenuDelete
        '
        Me.MenuDelete.Index = 1
        Me.MenuDelete.Text = "Delete"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "Properties"
        '
        'PathwayEvent
        '
        Me.BackColor = System.Drawing.Color.LightGreen
        Me.ContextMenu = Me.ContextMenuPathwayEvent
        Me.Name = "PathwayEvent"
        Me.Size = New System.Drawing.Size(80, 204)

    End Sub

#End Region

    Property Item() As RmPathwayStep
        Get
            Return mItem
        End Get
        Set(ByVal Value As RmPathwayStep)
            mItem = Value
        End Set
    End Property
    Property Selected() As Boolean
        Get
            Return mSelected
        End Get
        Set(ByVal Value As Boolean)
            If mSelected <> Value Then
                mSelected = Value
                TextRectangle(mText, Me.CreateGraphics)
                If Value Then
                    RaiseEvent SelectionChanged(Me, New EventArgs)
                End If
            End If
        End Set
    End Property
    Shared ReadOnly Property DefaultWidth() As Integer
        Get
            Return 80
        End Get
    End Property
    Property AlternativeState() As StateMachineType
        Get
            Return mItem.AlternativeState
        End Get
        Set(ByVal Value As StateMachineType)
            mItem.AlternativeState = Value
            If Value = StateMachineType.Not_Set Then
                Me.BackColor = OceanArchetypeEditor.Instance.Options.StateMachineColour(mItem.StateType)
            Else
                Me.BackColor = OceanArchetypeEditor.Instance.Options.StateMachineColour(Value)
            End If
            TextRectangle(mText, Me.CreateGraphics)
        End Set
    End Property
    Property DefaultStateMachineType() As StateMachineType
        Get
            Return mItem.StateType
        End Get
        Set(ByVal Value As StateMachineType)
            mItem.StateType = Value
            Me.BackColor = OceanArchetypeEditor.Instance.Options.StateMachineColour(Value)
        End Set
    End Property
    Property PathwayEventText() As String
        Get
            Return mText
        End Get
        Set(ByVal Value As String)
            mText = Value
            TextRectangle(mText, Me.CreateGraphics)
        End Set
    End Property
    Public Sub Translate()
        Dim a_term As RmTerm

        a_term = mFileManager.OntologyManager.GetTerm(mItem.NodeId)
        mText = a_term.Text
        mDescription = a_term.Description
    End Sub
    Private Function TextRectangle(ByVal TextToRender As String, ByVal g As Graphics) As Drawing.Rectangle
        Dim fontFamily As New fontFamily("Arial")
        Dim b As Brush
        Dim a_colour As Drawing.Color
        Dim font As New font( _
           fontFamily, _
           8, _
           FontStyle.Regular, _
           GraphicsUnit.Point)
        Dim rect As New Rectangle(4, 4, Me.Width - 8, Me.Height - 8)
        Dim stringFormat As New stringFormat
        Dim solidBrush As New solidBrush(Color.FromArgb(255, 0, 0, 0))  'black

        If mItem Is Nothing Then
            a_colour = Me.BackColor
        Else
            'If Not mItem.AlternativeState = StateMachineType.Not_Set Then
            'a_colour = OceanArchetypeEditor.Instance.Options.StateMachineColour(mItem.AlternativeState)
            'Else
                a_colour = OceanArchetypeEditor.Instance.Options.StateMachineColour(mItem.StateType)
            'End If
        End If

        ' exclude the area outside the triangle from graphics commands
        g.IntersectClip(rect)

        rect = New Rectangle(5, 5, Me.Width - 10, Me.Height - 10)
        g.Clear(a_colour)

        ' Center each line of text.
        stringFormat.Alignment = StringAlignment.Center

        ' Center the block of text (top to bottom) in the rectangle.
        stringFormat.LineAlignment = StringAlignment.Center

        g.DrawString(TextToRender, font, solidBrush, RectangleF.op_Implicit(rect), stringFormat)
        Dim pen As New pen(Color.Black)

        If mSelected Then
            pen.Width = 3
        End If

        g.DrawRectangle(pen, rect)


    End Function

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        TextRectangle(mText, e.Graphics)
    End Sub

    Private Sub MenuEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuEdit.Click
        Dim s() As String
        Dim a_term As RmTerm

        a_term = New RmTerm(Me.mItem.NodeId)
        a_term.Text = mText
        a_term.Description = mDescription

        Debug.Assert(mItem.StateType <> StateMachineType.Not_Set)

        s = OceanArchetypeEditor.Instance.GetInput(a_term)
        If s(0) = "" Then ' cancelled
            Return
        End If

        mText = a_term.Text
        mDescription = a_term.Description

        mFileManager.OntologyManager.SetText(a_term)
        TextRectangle(mText, Me.CreateGraphics)
        mFileManager.FileEdited = True

    End Sub

    Private Sub PathwayEvent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        TextRectangle(mText, Me.CreateGraphics)
    End Sub

    Private Sub PathwayEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Me.Select()
        If Not mSelected Then
            Me.Selected = True
        End If
    End Sub

    Private Sub MenuDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDelete.Click
        Dim p As Panel
        ' get the parent - needed to layout controls as basis for the event

        p = Me.Parent
        p.Controls.Remove(Me)
        Me.mItem = Nothing
        mFileManager.FileEdited = True
        RaiseEvent Deleted(p, New EventArgs)
    End Sub

End Class
