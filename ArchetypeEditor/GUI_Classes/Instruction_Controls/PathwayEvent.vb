Public Class PathwayEvent
    Inherits System.Windows.Forms.UserControl

    Private mSelectedPenWidth As Integer = 3
    Private mDefaultWidth As Integer = 80
    Private mSelected As Boolean

    Private mText As String
    Private mDescription As String
    Private mItem As RmPathwayStep
    Private mLastEditWasOk As Boolean
    Friend WithEvents toolTipPathway As System.Windows.Forms.ToolTip
    Friend WithEvents MoveLeftMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents MoveRightMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RemoveMenuItem As System.Windows.Forms.MenuItem
    Private mFileManager As FileManagerLocal

    Public Event SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Removed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Moved(ByVal sender As Object, ByVal e As EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not DesignMode Then
            Debug.Assert(False) ' should only be used in design view
        End If
    End Sub

    Sub New(ByVal defaultMachineStateType As StateMachineType, ByVal fileManager As FileManagerLocal)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mFileManager = fileManager
        Dim term As RmTerm = mFileManager.OntologyManager.AddTerm(defaultMachineStateType.ToString)
        mText = term.Text
        mItem = New RmPathwayStep(term.Code, defaultMachineStateType)
        BackColor = Main.Instance.Options.StateMachineColour(defaultMachineStateType)
    End Sub

    Sub New(ByVal rm As RmPathwayStep, ByVal fileManager As FileManagerLocal)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        mFileManager = fileManager
        mItem = rm
        Translate()

        If mItem.HasAlternativeState Then
            BackColor = Main.Instance.Options.StateMachineColour(rm.AlternativeState)
        Else
            BackColor = Main.Instance.Options.StateMachineColour(rm.StateType)
        End If

        TextRectangle(mText, CreateGraphics)
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
    Friend WithEvents EditMenuItem As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ContextMenuPathwayEvent = New System.Windows.Forms.ContextMenu
        Me.EditMenuItem = New System.Windows.Forms.MenuItem
        Me.MoveLeftMenuItem = New System.Windows.Forms.MenuItem
        Me.MoveRightMenuItem = New System.Windows.Forms.MenuItem
        Me.toolTipPathway = New System.Windows.Forms.ToolTip(Me.components)
        Me.RemoveMenuItem = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'ContextMenuPathwayEvent
        '
        Me.ContextMenuPathwayEvent.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.EditMenuItem, Me.RemoveMenuItem, Me.MoveLeftMenuItem, Me.MoveRightMenuItem})
        '
        'EditMenuItem
        '
        Me.EditMenuItem.Index = 0
        Me.EditMenuItem.Text = "Edit"
        '
        'MoveLeftMenuItem
        '
        Me.MoveLeftMenuItem.Index = 2
        Me.MoveLeftMenuItem.Text = "Move Left"
        '
        'MoveRightMenuItem
        '
        Me.MoveRightMenuItem.Index = 3
        Me.MoveRightMenuItem.Text = "Move Right"
        '
        'toolTipPathway
        '
        Me.toolTipPathway.AutomaticDelay = 100
        '
        'RemoveMenuItem
        '
        Me.RemoveMenuItem.Index = 1
        Me.RemoveMenuItem.Text = "Remove"
        '
        'PathwayEvent
        '
        Me.BackColor = System.Drawing.Color.LightGreen
        Me.ContextMenu = Me.ContextMenuPathwayEvent
        Me.Name = "PathwayEvent"
        Me.Size = New System.Drawing.Size(80, 244)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public ReadOnly Property Item() As RmPathwayStep
        Get
            Return mItem
        End Get
    End Property

    Property Selected() As Boolean
        Get
            Return mSelected
        End Get
        Set(ByVal Value As Boolean)
            If mSelected <> Value Then
                mSelected = Value
                TextRectangle(mText, CreateGraphics)

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
                BackColor = Main.Instance.Options.StateMachineColour(mItem.StateType)
            Else
                BackColor = Main.Instance.Options.StateMachineColour(Value)
            End If

            TextRectangle(mText, CreateGraphics)
        End Set
    End Property

    Property DefaultStateMachineType() As StateMachineType
        Get
            Return mItem.StateType
        End Get
        Set(ByVal Value As StateMachineType)
            mItem.StateType = Value
            BackColor = Main.Instance.Options.StateMachineColour(Value)
        End Set
    End Property

    ReadOnly Property LastEditWasOk() As Boolean
        Get
            Return mLastEditWasOk
        End Get
    End Property

    Public ReadOnly Property SpecialisationDepth() As Integer
        Get
            Return mFileManager.OntologyManager.NumberOfSpecialisations
        End Get
    End Property

    Public ReadOnly Property IsSameSpecialisationDepth() As Boolean
        Get
            Return SpecialisationDepth = Item.SpecialisationDepth()
        End Get
    End Property

    Public Sub Translate()
        Dim term As RmTerm = mFileManager.OntologyManager.GetTerm(mItem.NodeId)
        mText = term.Text
        mDescription = term.Description
        toolTipPathway.SetToolTip(Me, mText & "[" & term.Code & "]")
        TextRectangle(mText, CreateGraphics)
    End Sub

    Public Sub Specialise()
        Dim dlg As New SpecialisationQuestionDialog()
        dlg.ShowForArchetypeNode(mText, Item, SpecialisationDepth)

        If dlg.IsSpecialisationRequested Then
            mItem = Item.Copy
            Item.NodeId = mFileManager.OntologyManager.SpecialiseTerm(mText, mDescription, Item.NodeId).Code

            If Item.HasNameConstraint AndAlso Item.NameConstraint.TypeOfTextConstraint = TextConstraintType.Terminology Then
                Item.NameConstraint.ConstraintCode = mFileManager.OntologyManager.SpecialiseNameConstraint(Item.NameConstraint.ConstraintCode).Code
            End If

            Translate()
            mFileManager.FileEdited = True
            RaiseEvent SelectionChanged(Me, New EventArgs)
        End If
    End Sub

    Public Sub Edit()
        mLastEditWasOk = False

        If Not IsSameSpecialisationDepth Then
            Specialise()
        End If

        If IsSameSpecialisationDepth Then
            Dim term As RmTerm = New RmTerm(mItem.NodeId)
            term.Text = mText
            term.Description = mDescription

            Debug.Assert(mItem.StateType <> StateMachineType.Not_Set)

            Dim s() As String = Main.Instance.GetInput(term, ParentForm)
            mLastEditWasOk = s(0) <> ""

            If mLastEditWasOk Then
                mFileManager.OntologyManager.SetRmTermText(term)
                Translate()
                mFileManager.FileEdited = True
            End If
        End If
    End Sub

    Public Sub Remove()
        If Item.CanRemove(SpecialisationDepth) Then
            If MessageBox.Show(AE_Constants.Instance.Remove & mText, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                Dim p As Control = Parent
                p.Controls.Remove(Me)
                mItem = Nothing
                mFileManager.FileEdited = True
                RaiseEvent Removed(p, New EventArgs)
            End If
        End If
    End Sub

    Public Sub Moveby(ByVal delta As Integer)
        ' Re-order the pathway event
        Dim controls As ControlCollection = Parent.Controls
        Dim currindex As Integer = controls.GetChildIndex(Me)
        Dim newindex As Integer = currindex + delta

        If newindex <> currindex And newindex >= 0 And newindex < controls.Count Then
            controls.SetChildIndex(Me, newindex)

            For i As Integer = 0 To controls.Count - 1
                controls(i).TabIndex = i
            Next

            mFileManager.FileEdited = True
            RaiseEvent Removed(Parent, New EventArgs)
        End If
    End Sub

    Private Function TextRectangle(ByVal TextToRender As String, ByVal g As Graphics) As Drawing.Rectangle
        Dim fontFamily As New FontFamily("Arial")
        Dim colour As Drawing.Color
        Dim font As New Font(fontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        Dim rect As New Rectangle(4, 4, Width - 8, Height - 8)
        Dim stringFormat As New StringFormat
        Dim solidBrush As New SolidBrush(Color.FromArgb(255, 0, 0, 0))  'black

        If mItem Is Nothing Then
            colour = BackColor
        Else
            colour = Main.Instance.Options.StateMachineColour(mItem.StateType)
        End If

        ' Exclude the area outside the triangle from graphics commands
        g.IntersectClip(rect)

        rect = New Rectangle(5, 5, Width - 10, Height - 10)
        g.Clear(colour)

        ' Centre each line of text.
        stringFormat.Alignment = StringAlignment.Center

        ' Centre the block of text (top to bottom) in the rectangle.
        stringFormat.LineAlignment = StringAlignment.Center

        g.DrawString(TextToRender, font, solidBrush, RectangleF.op_Implicit(rect), stringFormat)
        Dim pen As New Pen(Color.Black)

        If mSelected Then
            pen.Width = 3
        End If

        g.DrawRectangle(pen, rect)
    End Function

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        TextRectangle(mText, e.Graphics)
    End Sub

    Private Sub PathwayEvent_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
        Me.Select()
        Selected = True
    End Sub

    Private Sub PathwayEvent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        TextRectangle(mText, CreateGraphics)
    End Sub

    Private Sub EditMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditMenuItem.Click, Me.DoubleClick
        Edit()
    End Sub

    Private Sub RemoveMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveMenuItem.Click
        Remove()
    End Sub

    Private Sub MoveLeftMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveLeftMenuItem.Click
        Moveby(-1)
    End Sub

    Private Sub MenuMoveRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveRightMenuItem.Click
        Moveby(1)
    End Sub

    Private Sub ContextMenuPathwayEvent_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuPathwayEvent.Popup
        RemoveMenuItem.Visible = Item.CanRemove(SpecialisationDepth)
        RemoveMenuItem.Text = AE_Constants.Instance.Remove
        EditMenuItem.Text = mFileManager.OntologyManager.GetOpenEHRTerm(592, EditMenuItem.Text)
    End Sub

End Class
