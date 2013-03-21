'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Public Class formCreateClinicalModel
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabComponents As System.Windows.Forms.TabControl
    Friend WithEvents TabPgFolder As System.Windows.Forms.TabPage
    Friend WithEvents TabPgTx As System.Windows.Forms.TabPage
    Friend WithEvents TabPgOrg As System.Windows.Forms.TabPage
    Friend WithEvents TabPgEntry As System.Windows.Forms.TabPage
    Friend WithEvents TabCtrlEntries As System.Windows.Forms.TabControl
    Friend WithEvents TabPgObs As System.Windows.Forms.TabPage
    Friend WithEvents TabPgInstruct As System.Windows.Forms.TabPage
    Friend WithEvents TabPgEval As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents TreeFolder As System.Windows.Forms.TreeView
    Friend WithEvents TreeTransaction As System.Windows.Forms.TreeView
    Friend WithEvents TreeOrganiser As System.Windows.Forms.TreeView
    Friend WithEvents TreeObservation As System.Windows.Forms.TreeView
    Friend WithEvents TreeInstruction As System.Windows.Forms.TreeView
    Friend WithEvents TreeEvaluation As System.Windows.Forms.TreeView
    Friend WithEvents butGo As System.Windows.Forms.Button
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxComponents As System.Windows.Forms.PictureBox
    Friend WithEvents OleDbConnection1 As System.Data.OleDb.OleDbConnection
    Friend WithEvents OleDbClinicalModels As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents OleDbSelectCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents TabPgAction As System.Windows.Forms.TabPage
    Friend WithEvents TreeAction As System.Windows.Forms.TreeView
    Friend WithEvents ListReturnValues As System.Windows.Forms.ListView
    Friend WithEvents LblContains As System.Windows.Forms.Label
    Friend WithEvents butOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formCreateClinicalModel))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TabComponents = New System.Windows.Forms.TabControl
        Me.TabPgFolder = New System.Windows.Forms.TabPage
        Me.TreeFolder = New System.Windows.Forms.TreeView
        Me.TabPgTx = New System.Windows.Forms.TabPage
        Me.TreeTransaction = New System.Windows.Forms.TreeView
        Me.TabPgEntry = New System.Windows.Forms.TabPage
        Me.TabCtrlEntries = New System.Windows.Forms.TabControl
        Me.TabPgObs = New System.Windows.Forms.TabPage
        Me.TreeObservation = New System.Windows.Forms.TreeView
        Me.TabPgInstruct = New System.Windows.Forms.TabPage
        Me.TreeInstruction = New System.Windows.Forms.TreeView
        Me.TabPgEval = New System.Windows.Forms.TabPage
        Me.TreeEvaluation = New System.Windows.Forms.TreeView
        Me.TabPgAction = New System.Windows.Forms.TabPage
        Me.TreeAction = New System.Windows.Forms.TreeView
        Me.TabPgOrg = New System.Windows.Forms.TabPage
        Me.TreeOrganiser = New System.Windows.Forms.TreeView
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.ComboFilter = New System.Windows.Forms.ComboBox
        Me.butGo = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBoxComponents = New System.Windows.Forms.PictureBox
        Me.OleDbConnection1 = New System.Data.OleDb.OleDbConnection
        Me.OleDbClinicalModels = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.ListReturnValues = New System.Windows.Forms.ListView
        Me.LblContains = New System.Windows.Forms.Label
        Me.butOK = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.TabComponents.SuspendLayout()
        Me.TabPgFolder.SuspendLayout()
        Me.TabPgTx.SuspendLayout()
        Me.TabPgEntry.SuspendLayout()
        Me.TabCtrlEntries.SuspendLayout()
        Me.TabPgObs.SuspendLayout()
        Me.TabPgInstruct.SuspendLayout()
        Me.TabPgEval.SuspendLayout()
        Me.TabPgAction.SuspendLayout()
        Me.TabPgOrg.SuspendLayout()
        CType(Me.PictureBoxComponents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.TabComponents)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TxtSearch)
        Me.Panel1.Controls.Add(Me.ComboFilter)
        Me.Panel1.Controls.Add(Me.butGo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(288, 646)
        Me.Panel1.TabIndex = 5
        '
        'TabComponents
        '
        Me.TabComponents.Controls.Add(Me.TabPgFolder)
        Me.TabComponents.Controls.Add(Me.TabPgTx)
        Me.TabComponents.Controls.Add(Me.TabPgEntry)
        Me.TabComponents.Controls.Add(Me.TabPgOrg)
        Me.TabComponents.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabComponents.Location = New System.Drawing.Point(0, 74)
        Me.TabComponents.Name = "TabComponents"
        Me.TabComponents.SelectedIndex = 0
        Me.TabComponents.Size = New System.Drawing.Size(284, 568)
        Me.TabComponents.TabIndex = 6
        '
        'TabPgFolder
        '
        Me.TabPgFolder.Controls.Add(Me.TreeFolder)
        Me.TabPgFolder.Location = New System.Drawing.Point(4, 22)
        Me.TabPgFolder.Name = "TabPgFolder"
        Me.TabPgFolder.Size = New System.Drawing.Size(276, 542)
        Me.TabPgFolder.TabIndex = 0
        Me.TabPgFolder.Text = "Folders"
        '
        'TreeFolder
        '
        Me.TreeFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeFolder.FullRowSelect = True
        Me.TreeFolder.HideSelection = False
        Me.TreeFolder.Location = New System.Drawing.Point(0, 0)
        Me.TreeFolder.Name = "TreeFolder"
        Me.TreeFolder.Size = New System.Drawing.Size(276, 542)
        Me.TreeFolder.TabIndex = 0
        '
        'TabPgTx
        '
        Me.TabPgTx.Controls.Add(Me.TreeTransaction)
        Me.TabPgTx.Location = New System.Drawing.Point(4, 22)
        Me.TabPgTx.Name = "TabPgTx"
        Me.TabPgTx.Size = New System.Drawing.Size(276, 542)
        Me.TabPgTx.TabIndex = 1
        Me.TabPgTx.Text = "Composition"
        Me.TabPgTx.Visible = False
        '
        'TreeTransaction
        '
        Me.TreeTransaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeTransaction.Location = New System.Drawing.Point(0, 0)
        Me.TreeTransaction.Name = "TreeTransaction"
        Me.TreeTransaction.Size = New System.Drawing.Size(276, 542)
        Me.TreeTransaction.TabIndex = 0
        '
        'TabPgEntry
        '
        Me.TabPgEntry.Controls.Add(Me.TabCtrlEntries)
        Me.TabPgEntry.Location = New System.Drawing.Point(4, 22)
        Me.TabPgEntry.Name = "TabPgEntry"
        Me.TabPgEntry.Size = New System.Drawing.Size(276, 542)
        Me.TabPgEntry.TabIndex = 3
        Me.TabPgEntry.Text = "Entry"
        Me.TabPgEntry.Visible = False
        '
        'TabCtrlEntries
        '
        Me.TabCtrlEntries.Controls.Add(Me.TabPgObs)
        Me.TabCtrlEntries.Controls.Add(Me.TabPgInstruct)
        Me.TabCtrlEntries.Controls.Add(Me.TabPgEval)
        Me.TabCtrlEntries.Controls.Add(Me.TabPgAction)
        Me.TabCtrlEntries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabCtrlEntries.Location = New System.Drawing.Point(0, 0)
        Me.TabCtrlEntries.Name = "TabCtrlEntries"
        Me.TabCtrlEntries.SelectedIndex = 0
        Me.TabCtrlEntries.Size = New System.Drawing.Size(276, 542)
        Me.TabCtrlEntries.TabIndex = 0
        '
        'TabPgObs
        '
        Me.TabPgObs.Controls.Add(Me.TreeObservation)
        Me.TabPgObs.Location = New System.Drawing.Point(4, 22)
        Me.TabPgObs.Name = "TabPgObs"
        Me.TabPgObs.Size = New System.Drawing.Size(268, 516)
        Me.TabPgObs.TabIndex = 2
        Me.TabPgObs.Text = "Observation"
        '
        'TreeObservation
        '
        Me.TreeObservation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeObservation.Location = New System.Drawing.Point(0, 0)
        Me.TreeObservation.Name = "TreeObservation"
        Me.TreeObservation.Size = New System.Drawing.Size(268, 516)
        Me.TreeObservation.Sorted = True
        Me.TreeObservation.TabIndex = 0
        '
        'TabPgInstruct
        '
        Me.TabPgInstruct.Controls.Add(Me.TreeInstruction)
        Me.TabPgInstruct.Location = New System.Drawing.Point(4, 22)
        Me.TabPgInstruct.Name = "TabPgInstruct"
        Me.TabPgInstruct.Size = New System.Drawing.Size(268, 516)
        Me.TabPgInstruct.TabIndex = 1
        Me.TabPgInstruct.Text = "Instruction"
        Me.TabPgInstruct.Visible = False
        '
        'TreeInstruction
        '
        Me.TreeInstruction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeInstruction.Location = New System.Drawing.Point(0, 0)
        Me.TreeInstruction.Name = "TreeInstruction"
        Me.TreeInstruction.Size = New System.Drawing.Size(268, 516)
        Me.TreeInstruction.Sorted = True
        Me.TreeInstruction.TabIndex = 0
        '
        'TabPgEval
        '
        Me.TabPgEval.Controls.Add(Me.TreeEvaluation)
        Me.TabPgEval.Location = New System.Drawing.Point(4, 22)
        Me.TabPgEval.Name = "TabPgEval"
        Me.TabPgEval.Size = New System.Drawing.Size(268, 516)
        Me.TabPgEval.TabIndex = 0
        Me.TabPgEval.Text = "Evaluation"
        Me.TabPgEval.Visible = False
        '
        'TreeEvaluation
        '
        Me.TreeEvaluation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeEvaluation.Location = New System.Drawing.Point(0, 0)
        Me.TreeEvaluation.Name = "TreeEvaluation"
        Me.TreeEvaluation.Size = New System.Drawing.Size(268, 516)
        Me.TreeEvaluation.Sorted = True
        Me.TreeEvaluation.TabIndex = 0
        '
        'TabPgAction
        '
        Me.TabPgAction.Controls.Add(Me.TreeAction)
        Me.TabPgAction.Location = New System.Drawing.Point(4, 22)
        Me.TabPgAction.Name = "TabPgAction"
        Me.TabPgAction.Size = New System.Drawing.Size(268, 516)
        Me.TabPgAction.TabIndex = 3
        Me.TabPgAction.Text = "Activity"
        '
        'TreeAction
        '
        Me.TreeAction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeAction.Location = New System.Drawing.Point(0, 0)
        Me.TreeAction.Name = "TreeAction"
        Me.TreeAction.Size = New System.Drawing.Size(268, 516)
        Me.TreeAction.Sorted = True
        Me.TreeAction.TabIndex = 0
        '
        'TabPgOrg
        '
        Me.TabPgOrg.Controls.Add(Me.TreeOrganiser)
        Me.TabPgOrg.Location = New System.Drawing.Point(4, 22)
        Me.TabPgOrg.Name = "TabPgOrg"
        Me.TabPgOrg.Size = New System.Drawing.Size(276, 542)
        Me.TabPgOrg.TabIndex = 2
        Me.TabPgOrg.Text = "Section"
        Me.TabPgOrg.Visible = False
        '
        'TreeOrganiser
        '
        Me.TreeOrganiser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeOrganiser.Location = New System.Drawing.Point(0, 0)
        Me.TreeOrganiser.Name = "TreeOrganiser"
        Me.TreeOrganiser.Size = New System.Drawing.Size(276, 542)
        Me.TreeOrganiser.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Search"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TxtSearch
        '
        Me.TxtSearch.Location = New System.Drawing.Point(64, 9)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(160, 20)
        Me.TxtSearch.TabIndex = 4
        '
        'ComboFilter
        '
        Me.ComboFilter.Location = New System.Drawing.Point(63, 42)
        Me.ComboFilter.Name = "ComboFilter"
        Me.ComboFilter.Size = New System.Drawing.Size(201, 21)
        Me.ComboFilter.TabIndex = 7
        Me.ComboFilter.Text = "<none>"
        '
        'butGo
        '
        Me.butGo.Location = New System.Drawing.Point(227, 9)
        Me.butGo.Name = "butGo"
        Me.butGo.Size = New System.Drawing.Size(32, 24)
        Me.butGo.TabIndex = 9
        Me.butGo.Text = "Go"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(14, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 18)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Filter"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PictureBoxComponents
        '
        Me.PictureBoxComponents.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxComponents.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PictureBoxComponents.Location = New System.Drawing.Point(288, 190)
        Me.PictureBoxComponents.Name = "PictureBoxComponents"
        Me.PictureBoxComponents.Size = New System.Drawing.Size(416, 456)
        Me.PictureBoxComponents.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxComponents.TabIndex = 6
        Me.PictureBoxComponents.TabStop = False
        Me.PictureBoxComponents.Visible = False
        '
        'OleDbConnection1
        '
        Me.OleDbConnection1.ConnectionString = resources.GetString("OleDbConnection1.ConnectionString")
        '
        'OleDbClinicalModels
        '
        Me.OleDbClinicalModels.SelectCommand = Me.OleDbSelectCommand1
        Me.OleDbClinicalModels.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "qryArchetypeListWIthParents", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Archetype", "Archetype"), New System.Data.Common.DataColumnMapping("Class", "Class"), New System.Data.Common.DataColumnMapping("Concept", "Concept"), New System.Data.Common.DataColumnMapping("Parent", "Parent")})})
        '
        'OleDbSelectCommand1
        '
        Me.OleDbSelectCommand1.CommandText = "SELECT Archetype, Class, Concept, Parent FROM qryArchetypeListWIthParents"
        Me.OleDbSelectCommand1.Connection = Me.OleDbConnection1
        '
        'ListReturnValues
        '
        Me.ListReturnValues.Location = New System.Drawing.Point(328, 32)
        Me.ListReturnValues.Name = "ListReturnValues"
        Me.ListReturnValues.Size = New System.Drawing.Size(352, 144)
        Me.ListReturnValues.TabIndex = 8
        Me.ListReturnValues.UseCompatibleStateImageBehavior = False
        Me.ListReturnValues.View = System.Windows.Forms.View.List
        '
        'LblContains
        '
        Me.LblContains.Location = New System.Drawing.Point(330, 15)
        Me.LblContains.Name = "LblContains"
        Me.LblContains.Size = New System.Drawing.Size(136, 16)
        Me.LblContains.TabIndex = 9
        Me.LblContains.Text = "Legal compositions:"
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(632, 600)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(56, 24)
        Me.butOK.TabIndex = 10
        Me.butOK.Text = "OK"
        '
        'formCreateClinicalModel
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(704, 646)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.LblContains)
        Me.Controls.Add(Me.ListReturnValues)
        Me.Controls.Add(Me.PictureBoxComponents)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "formCreateClinicalModel"
        Me.Text = "Choose and archetype"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabComponents.ResumeLayout(False)
        Me.TabPgFolder.ResumeLayout(False)
        Me.TabPgTx.ResumeLayout(False)
        Me.TabPgEntry.ResumeLayout(False)
        Me.TabCtrlEntries.ResumeLayout(False)
        Me.TabPgObs.ResumeLayout(False)
        Me.TabPgInstruct.ResumeLayout(False)
        Me.TabPgEval.ResumeLayout(False)
        Me.TabPgAction.ResumeLayout(False)
        Me.TabPgOrg.ResumeLayout(False)
        CType(Me.PictureBoxComponents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub formCreateClinicalModel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim nd, nd1 As TreeNode
        Dim str, str1, str2, str3 As String
        Dim i As Integer
        Dim dr As OleDb.OleDbDataReader
        Dim tr_v As TreeView

        str2 = Application.StartupPath
        str1 = "Provider=Microsoft.Jet.OLEDB.4.0;Password="""";User ID=Admin;Data Source="
        str3 = ";Mode=Share Deny None;Extended Properties="""";Jet OLEDB:System database="""";Jet OLEDB:Registry Path="""";Jet OLEDB:Database Password="""";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="""";Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False"
        str = str1 & str2 & "\Data\openEHR_Clinical_Models.mdb" & str3
        OleDbConnection1.ConnectionString = str
        Me.OleDbConnection1.Open()

        dr = Me.OleDbClinicalModels.SelectCommand.ExecuteReader

        While dr.Read

            str = Trim(dr.GetString(2)) 'concept
            i = InStr(str, " (")  ' strip off the class name in ()
            'might be no space
            If i = 0 Then
                i = InStr(str, " (")
            End If
            ' as long as it was found strip the Class name as not required
            If i > 2 Then
                str = Mid(str, 1, i - 1)
            End If

            Select Case Trim(dr.GetString(1))
                ' is case sensitive
                Case "openEHR Folder"
                    tr_v = Me.TreeFolder

                Case "openEHR Transaction", "Specialised Transaction"
                    tr_v = Me.TreeTransaction

                Case "openEHR Organiser"
                    tr_v = Me.TreeOrganiser

                Case "openEHR Evaluation"
                    tr_v = Me.TreeEvaluation

                Case "openEHR Observation"
                    tr_v = Me.TreeObservation

                Case "openEHR Instruction"
                    tr_v = Me.TreeInstruction

                Case "openEHR Observation-Action"
                    tr_v = Me.TreeAction

                Case Else
                    tr_v = Nothing

            End Select

            If Not IsNothing(tr_v) Then
                ' it is a recognised class so create a new node for it
                nd = New TreeNode(str)
                nd.Tag = Trim(dr.GetString(0))

                ' is it a specialisation - there will be an entry in field(3)
                If dr.Item(3).ToString <> "" Then
                    ' find the node of this archetype
                    nd1 = GetNode(Trim(dr.GetString(3)), "tag", tr_v)
                    If Not IsNothing(nd1) Then
                        nd1.Nodes.Add(nd)
                    Else
                        tr_v.Nodes.Add(nd)
                    End If
                Else
                    tr_v.Nodes.Add(nd)
                End If

            End If

        End While

        dr.Close()

        'populate the filters - default is <none>

        Dim Sqlcmd As OleDb.OleDbCommand

        Me.ComboFilter.Items.Add("<none>")

        Sqlcmd = OleDbConnection1.CreateCommand

        Sqlcmd.CommandText = "SELECT Slot_or_facet_value as Category FROM (qryCategories)"

        dr = Sqlcmd.ExecuteReader

        While dr.Read
            Me.ComboFilter.Items.Add(Trim(dr.GetString(0)))
        End While

        dr.Close()

        'populate the trees


    End Sub

    Private Function GetNode(ByVal Txt As String, ByVal Tp As String, ByVal Tr As TreeView) As TreeNode
        Dim nc As Collection
        Dim nd As TreeNode

        For Each nd In Tr.Nodes
            nc = CallRecursive(nd, Tp, Txt)
            If Not IsNothing(nc) Then
                If nc.Count > 0 Then
                    Return nc.Item(1)
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Sub butGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butGo.Click
        ' Search in the active treeview
        Dim tb As TabPage
        Dim tr As TreeView
        Dim nd As New TreeNode()
        Dim nc As New Collection()
        Dim n_nodes As Integer
        Dim searchstring As String

        searchstring = Me.TxtSearch.Text

        tb = Me.TabComponents.SelectedTab()
        If tb.Name = "TabPgEntry" Then
            tb = Me.TabCtrlEntries.SelectedTab()
        End If
        Select Case tb.Name
            Case "TabPgFolder"
                tr = Me.TreeFolder
            Case "TabPgTx"
                tr = Me.TreeTransaction
            Case "TabPgOrg"
                tr = Me.TreeOrganiser
            Case "TabPgObs"
                tr = Me.TreeObservation
            Case "TabPgInstruct"
                tr = Me.TreeInstruction
            Case "TabPgEval"
                tr = Me.TreeEvaluation
            Case "TabPgAction"
                tr = Me.TreeAction
            Case Else
                Debug.Assert(False, "Control not handled")
                Return
        End Select

        tr.CollapseAll()

        nc = Get_matching_nodes(tr, "Text", searchstring)

        n_nodes = nc.Count

        If n_nodes > 1 Then
            Dim frm As New Choose()

            For Each nd In nc
                ' make sure that all the nodes are visible
                frm.ListChoose.Items.Add(nd.Text)
            Next
            frm.ShowDialog(Me)
            If frm.ListChoose.SelectedIndex >= 0 Then
                For Each nd In nc
                    If nd.Text = frm.ListChoose.Items(frm.ListChoose.SelectedIndex) Then
                        nd.EnsureVisible()
                        tr.SelectedNode = nd
                        tr.Select()
                    End If
                Next
                tr.Select()
            End If

        Else
            ' there is only one so there is no need to choose which one..
            If n_nodes = 1 Then
                nd = nc(1)
                nd.EnsureVisible()
                tr.SelectedNode = nd
                tr.Select()
            End If
        End If


    End Sub

    Private Function Get_matching_nodes(ByVal TrView As TreeView, ByVal node_property As String, ByVal str_to_look_for As String) As Collection
        Dim nc As Collection
        Dim node_collection As New Collection()
        Dim nd As TreeNode
        Dim i As Integer

        For Each nd In TrView.Nodes
            nc = CallRecursive(nd, node_property, str_to_look_for)
            If nc.Count > 0 Then
                For i = 1 To nc.Count
                    node_collection.Add(nc(i))
                    nc.Remove(i)
                Next
            End If
        Next
        Return node_collection

    End Function


    Private Function CallRecursive(ByVal nd As TreeNode, ByVal node_property As String, ByVal str_to_look_for As String) As Collection

        Dim n, n1 As TreeNode
        Dim nc As Collection = New Collection()
        Dim nc1 As Collection = New Collection()

        Dim str As String

        Select Case UCase(node_property)
            Case "TAG"
                str = nd.Tag
            Case "TEXT"
                str = nd.Text
            Case Else
                Debug.Assert(False)
                Return Nothing
        End Select

        If InStr(str, str_to_look_for, CompareMethod.Text) Then
            ' add to the collection of nodes returned
            nc.Add(nd)
        End If

        For Each n In nd.Nodes
            nc1 = CallRecursive(n, node_property, str_to_look_for)
            For Each n1 In nc1
                ' add it to the master collection
                nc.Add(n1)
            Next
        Next
        Return nc

    End Function

    Private Sub TxtSearch_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSearch.Enter
        Me.AcceptButton = Me.butGo
    End Sub

    Private Sub TxtSearch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSearch.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub TreeFolder_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeFolder.AfterSelect
        GetContents(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub TreeObservation_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeObservation.AfterSelect
        GetReturnValues(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub TabComponents_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabComponents.SelectedIndexChanged
        Me.PictureBoxComponents.Visible = False
        Me.ListReturnValues.Clear()
        Select Case TabComponents.SelectedTab.Name
            Case "tabPgFolder"
                Me.LblContains.Text = "Legal transactions:"

            Case "TabPgTx"
                Me.LblContains.Text = "Legal organisers:"

            Case "TabPgOrg"
                Me.LblContains.Text = "Legal entries:"

            Case "TabPgEntry"
                Me.LblContains.Text = "Return paths:"

        End Select
    End Sub

    Private Sub GetContents(ByVal str As String)
        Dim dr As OleDb.OleDbDataReader
        Dim Sqlcmd As OleDb.OleDbCommand

        Sqlcmd = OleDbConnection1.CreateCommand

        Sqlcmd.CommandText = "SELECT Frame, Name FROM (qryCategoryContainment) WHERE archetype = '" & str & "'"

        dr = Sqlcmd.ExecuteReader

        Me.ListReturnValues.Clear()

        While dr.Read
            Me.ListReturnValues.Items.Add(Trim(dr.GetString(1)))
        End While

        dr.Close()

    End Sub

    Private Sub GetReturnValues(ByVal str As String)
        Dim dr As OleDb.OleDbDataReader
        Dim Sqlcmd As OleDb.OleDbCommand

        Sqlcmd = OleDbConnection1.CreateCommand

        Sqlcmd.CommandText = "SELECT ReurnValue FROM(qryReturnPaths) WHERE qryReturnPaths.Archetype = '" & str & "'"

        dr = Sqlcmd.ExecuteReader

        Me.ListReturnValues.Clear()

        While dr.Read
            Me.ListReturnValues.Items.Add(Trim(dr.GetString(0)))
        End While

        dr.Close()

    End Sub

    Private Sub TreeInstruction_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeInstruction.AfterSelect
        GetReturnValues(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub TreeEvaluation_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeEvaluation.AfterSelect
        GetReturnValues(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub TreeAction_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeAction.AfterSelect
        GetReturnValues(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub TreeTransaction_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeTransaction.AfterSelect
        GetContents(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub TreeOrganiser_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeOrganiser.AfterSelect
        GetContents(CType(e.Node.Tag, String).ToString)
    End Sub

    Private Sub butOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOK.Click
        Me.Close()
    End Sub

End Class


'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is ArchetypeLibrary.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'
