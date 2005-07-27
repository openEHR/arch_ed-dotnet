Public Class ArchetypeTabPageStructure
    Inherits TabPageStructure

    Private mArchetype As Archetype

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        If Not Me.DesignMode Then
            mFileManager = New FileManagerLocal
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    ReadOnly Property ArchetypeAvailable() As Boolean
        Get
            Return mFileManager.ArchetypeAvailable
        End Get
    End Property

    Public Overloads Sub OpenArchetype(ByVal an_archetype_ID As ArchetypeID)
        mFileManager.OpenArchetype(an_archetype_ID.ToString)
    End Sub

    Public Overloads Sub OpenArchetype(ByVal a_slot As RmSlot)
        Dim archetype_name As String

        archetype_name = ReferenceModel.Instance.ReferenceModelName & "-" & a_slot.SlotConstraint.RM_ClassType.ToString & "." & a_slot.SlotConstraint.Include.Item(0) & ".adl"
        OpenArchetype(ArchetypeEditor.Instance.Options.RepositoryPath & "\Structure\" & archetype_name)

    End Sub

    Public Overloads Sub openArchetype(ByVal an_archetype_name As String)
        mFileManager.OpenArchetype(an_archetype_name)
        If mFileManager.ArchetypeAvailable Then
            Me.ProcessStructure(mFileManager.Archetype.Definition)
            Dim lbl As New Label
            lbl.Text = mFileManager.Archetype.Archetype_ID.ToString
            lbl.Location = New System.Drawing.Point(3, 20)
            lbl.Width = 400
            lbl.Height = 24
            mArchetypeControl.PanelStructureHeader.Height = 40
            mArchetypeControl.PanelStructureHeader.Controls.Add(lbl)

        End If
    End Sub

    Public Sub SaveArchetype()
        mFileManager.WriteArchetype()
    End Sub

End Class
