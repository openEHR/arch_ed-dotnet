'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Option Strict On

Public Class ArchetypeView

    Private ToolTip1 As New ToolTip

    Private Shared mInstance As ArchetypeView

    Public Shared ReadOnly Property Instance() As ArchetypeView
        Get
            If mInstance Is Nothing Then
                mInstance = New ArchetypeView
            End If

            Return mInstance
        End Get
    End Property

    Public Sub BuildInterface(ByVal node As ArchetypeNode, _
            ByVal aContainer As Control, ByRef pos As Point, ByVal spacer As Integer, ByVal mandatoryOnly As Boolean, ByVal a_filemanager As FileManagerLocal)

        If Not mandatoryOnly Or node.IsMandatory Then
            Dim view As New ViewPanel(New ColumnLayout(Orientation.CENTER, Orientation.LEFT))
            view.Location = pos
            AddArchetypeNode(node, view, mandatoryOnly, a_filemanager)
            pos.Y = view.Top + view.Height + spacer
            aContainer.Controls.Add(view)
        End If
    End Sub

    Public Sub BuildInterface(ByVal Items As Windows.Forms.ListView.ListViewItemCollection, _
            ByVal aContainer As Control, ByRef pos As Point, ByVal spacer As Integer, ByVal mandatoryOnly As Boolean, ByVal a_filemanager As FileManagerLocal)

        Dim view As New ViewPanel(New ColumnLayout(Orientation.CENTER, Orientation.LEFT, spacer))
        view.Location = pos
        view.SuspendLayout()

        For Each lvitem As ArchetypeListViewItem In Items
            AddArchetypeNode(lvitem.Item, view, mandatoryOnly, a_filemanager)
        Next

        view.ResumeLayout()

        If view.Controls.Count > 0 Then
            aContainer.Controls.Add(view)
            aContainer.Width = Math.Max(pos.X + view.Width + 5, aContainer.Width)

            If aContainer.Name <> "tpInterface" Then
                aContainer.Height = Math.Max(view.Top + view.Height + 1, aContainer.Height)
            End If

            pos.Y = view.Top + view.Height + spacer
        End If
    End Sub

    Public Sub BuildInterface(ByVal nodes As TreeNodeCollection, _
        ByVal aContainer As Control, ByRef pos As Point, ByVal spacer As Integer, ByVal mandatoryOnly As Boolean, ByVal a_filemanager As FileManagerLocal)

        NodesToControls(nodes, aContainer, pos, spacer, mandatoryOnly, a_filemanager)
    End Sub

    Public Sub BuildInterface(ByVal TableDetails As ArrayList, _
            ByVal aContainer As Control, ByRef pos As Point, ByVal spacer As Integer, ByVal mandatoryOnly As Boolean, ByVal a_filemanager As FileManagerLocal)

        Dim archetypeTable As DataTable
        Dim rowHeadings As Collection
        Dim isRotated As Boolean

        Debug.Assert(TypeOf TableDetails(0) Is DataTable)
        Debug.Assert(TypeOf TableDetails(1) Is Collection)
        Debug.Assert(TypeOf TableDetails(2) Is Boolean)

        archetypeTable = CType(TableDetails(0), DataTable)
        rowHeadings = CType(TableDetails(1), Collection)
        isRotated = CType(TableDetails(2), Boolean)

        If isRotated And rowHeadings.Count > 0 Then
            For Each t As String In CType(CType(rowHeadings(1), ArchetypeElement).Constraint, Constraint_Text).AllowableValues.Codes
                Dim relPos As New Point(20, 20)

                Dim gb As New GroupBox
                gb.Text = a_filemanager.OntologyManager.GetTerm(t).Text
                gb.Location = pos

                Dim view As New ViewPanel(New ColumnLayout(Orientation.CENTER, Orientation.LEFT, spacer))
                view.Location = relPos
                view.SuspendLayout()

                For Each row As DataRow In archetypeTable.Rows
                    Dim node As ArchetypeNode = TryCast(row(2), ArchetypeNode)

                    If Not node Is Nothing Then
                        AddArchetypeNode(node, view, mandatoryOnly, a_filemanager)
                    End If
                Next

                view.ResumeLayout()
                gb.Controls.Add(view)

                gb.Width = relPos.X + view.Width + 5
                gb.Height = view.Top + view.Height + 1

                pos.X += gb.Width + 5
                aContainer.Controls.Add(gb)
            Next
        End If
    End Sub

    Private Sub NodesToControls(ByVal NodeCol As TreeNodeCollection, _
        ByVal aContainer As Control, ByRef pos As Point, ByVal spacer As Integer, ByVal mandatoryOnly As Boolean, ByVal a_filemanager As FileManagerLocal)

        'Displays the archetype nodes as GUI controls on the Interface TAB

        Dim view As New ViewPanel(New ColumnLayout(Orientation.CENTER, Orientation.LEFT, spacer))
        view.Location = pos
        view.SuspendLayout()

        For Each tvNode As ArchetypeTreeNode In NodeCol
            If tvNode.Item.IsMandatory Or Not mandatoryOnly Then
                If tvNode.Item.RM_Class.Type = StructureType.Cluster Then
                    Dim ctrl As New Panel
                    ctrl.Location = pos

                    Dim relPos As New Point(0, 20)
                    Dim lbl As New Label
                    lbl.Width = 150
                    lbl.Height = 20
                    lbl.Text = tvNode.Text
                    lbl.AutoSize = lbl.Text.Length > 20
                    lbl.Location = relPos
                    lbl.BorderStyle = BorderStyle.FixedSingle
                    ToolTip1.SetToolTip(lbl, tvNode.Item.Occurrences.ToString)

                    'bold if must exist
                    If tvNode.Item.Occurrences.MinCount > 0 Then
                        lbl.Font = New System.Drawing.Font(lbl.Font, FontStyle.Bold)
                    End If

                    ctrl.Controls.Add(lbl)
                    lbl.BackColor = System.Drawing.Color.CornflowerBlue

                    If Not tvNode.Item.IsAnonymous AndAlso CType(tvNode.Item, ArchetypeNodeAbstract).RuntimeNameText <> "" Then
                        Dim but As New Button
                        but.Text = "..."
                        relPos.X = lbl.Location.X + lbl.Size.Width + 10
                        but.BackColor = System.Drawing.Color.LightGray

                        ToolTip1.SetToolTip(but, CType(tvNode.Item, ArchetypeNodeAbstract).RuntimeNameText)

                        but.Width = 30
                        but.Height = 20
                        but.Location = relPos

                        ctrl.Controls.Add(but)
                        ctrl.Width = 300
                    End If

                    relPos.X = 20
                    relPos.Y = 40
                    NodesToControls(tvNode.Nodes, ctrl, relPos, spacer, mandatoryOnly, a_filemanager)
                    view.Controls.Add(ctrl)
                Else
                    AddArchetypeNode(tvNode.Item, view, mandatoryOnly, a_filemanager)
                End If
            End If
        Next

        view.ResumeLayout()

        If view.Controls.Count > 0 Then
            aContainer.Controls.Add(view)
            aContainer.Width = Math.Max(pos.X + view.Width + 5, aContainer.Width)

            If aContainer.Name <> "tpInterface" Then
                aContainer.Height = Math.Max(view.Top + view.Height + 1, aContainer.Height)
            End If

            pos.Y = view.Top + view.Height + spacer
        End If
    End Sub

    Public Sub AddArchetypeNode(ByVal node As ArchetypeNode, ByVal view As ViewPanel, ByVal mandatoryOnly As Boolean, ByVal a_filemanager As FileManagerLocal)
        If Not mandatoryOnly Or node.IsMandatory Then
            Dim element As ArchetypeElement = TryCast(node, ArchetypeElement)

            If Not element Is Nothing Then
                view.Controls.Add(ElementView(element, a_filemanager))
            Else
                Dim panel As New Panel
                panel.BorderStyle = BorderStyle.Fixed3D
                panel.Size = New Size(500, 25)
                Dim lbl As New Label
                lbl.Text = node.RM_Class.Type.ToString + ": " + node.Text
                lbl.AutoSize = True
                panel.Controls.Add(lbl)
                view.Controls.Add(panel)
            End If
        End If
    End Sub

    Public Function ElementView(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal) As ElementViewControl
        'any additions need to be processed in the function below
        Dim t As ConstraintKind

        If Not anElement Is Nothing AndAlso Not anElement.Constraint Is Nothing Then
            t = anElement.Constraint.Kind
        End If

        Select Case t
            Case ConstraintKind.Text
                Return New TextViewControl(anElement, a_filemanager)

            Case ConstraintKind.Quantity
                Return New QuantityViewControl(anElement, a_filemanager)

            Case ConstraintKind.Duration
                Return New DurationViewControl(anElement, a_filemanager)

            Case ConstraintKind.Boolean
                Return New BooleanViewControl(anElement, a_filemanager)

            Case ConstraintKind.Ordinal
                Return New OrdinalViewControl(anElement, a_filemanager)

            Case ConstraintKind.DateTime
                Return New DateTimeViewControl(anElement, a_filemanager)

            Case ConstraintKind.Count
                Return New CountViewControl(anElement, a_filemanager)

            Case ConstraintKind.Multiple
                Return New MultipleViewControl(anElement, a_filemanager)

            Case ConstraintKind.URI
                Return New URIViewControl(anElement, a_filemanager)

            Case ConstraintKind.Identifier
                Return New IdentifierViewControl(anElement, a_filemanager)

            Case ConstraintKind.Interval_Count, ConstraintKind.Interval_Quantity, ConstraintKind.Interval_DateTime
                Return New IntervalViewControl(anElement, a_filemanager)

            Case ConstraintKind.Proportion
                Return New RatioViewControl(anElement, a_filemanager)

            Case ConstraintKind.MultiMedia
                Return New MultiMediaViewControl(anElement, a_filemanager)

            Case ConstraintKind.Parsable
                Return New ParsableViewControl(anElement, a_filemanager)

            Case Else
                Return New DatatypeViewControl(anElement, a_filemanager)
        End Select
    End Function

    Public Shared Function ConstraintView(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal) As ElementViewControl
        'any additions need to be processed in the function above
        Dim t As ConstraintKind

        If Not aConstraint Is Nothing Then
            t = aConstraint.Kind
        End If

        Select Case t
            Case ConstraintKind.Text
                Return New TextViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Quantity
                Return New QuantityViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Duration
                Return New DurationViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Boolean
                Return New BooleanViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Ordinal
                Return New OrdinalViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.DateTime
                Return New DateTimeViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Count
                Return New CountViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Multiple
                Return New MultipleViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.URI
                Return New URIViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Identifier
                Return New IdentifierViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Interval_Count, ConstraintKind.Interval_Quantity, ConstraintKind.Interval_DateTime
                Return New IntervalViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Proportion
                Return New RatioViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.MultiMedia
                Return New MultiMediaViewControl(aConstraint, a_filemanager)

            Case ConstraintKind.Parsable
                Return New ParsableViewControl(aConstraint, a_filemanager)

            Case Else
                Return New DatatypeViewControl(aConstraint, a_filemanager)
        End Select
    End Function

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
'The Original Code is ArchetypeView.vb.
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
