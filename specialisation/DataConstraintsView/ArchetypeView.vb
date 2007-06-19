'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
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

    Public Sub BuildInterface(ByVal an_element As ArchetypeElement, _
            ByVal aContainer As Control, ByRef aLocation As Point, _
            ByVal spacer As Integer, ByVal mandatory_only As Boolean, ByVal a_filemanager As FileManagerLocal)

        If (mandatory_only And (Not an_element.IsMandatory)) Then
            Return
        End If

        Dim view As New ViewPanel(New ColumnLayout(Orientation.CENTER, Orientation.LEFT))
        view.Location = New Point(aLocation.X, aLocation.Y)
        view.Controls.Add(ElementView(an_element, a_filemanager))
        aLocation.Y = view.Top + view.Height + spacer
        aContainer.Controls.Add(view)
    End Sub

    Public Sub BuildInterface(ByVal Items As Windows.forms.ListView.ListViewItemCollection, _
            ByVal aContainer As Control, ByRef aLocation As Point, _
            ByVal spacer As Integer, ByVal mandatory_only As Boolean, ByVal a_filemanager As FileManagerLocal)

        Dim view As New ViewPanel(New ColumnLayout(Orientation.CENTER, Orientation.LEFT, spacer))
        view.Location = New Point(aLocation.X, aLocation.Y)
        view.SuspendLayout()

        For Each lvitem As ArchetypeListViewItem In Items
            If ((lvitem.Item.IsMandatory) Or (Not mandatory_only)) Then
                Select Case lvitem.Item.RM_Class.Type
                    Case StructureType.Element, StructureType.Reference
                        view.Controls.Add(ElementView(CType(lvitem.Item, ArchetypeElement), a_filemanager))
                    Case StructureType.Slot
                        Dim panel As New Windows.Forms.Panel
                        Dim lbl As New Windows.Forms.Label
                        lbl.Text = lvitem.Text
                        panel.Controls.Add(lbl)
                        view.Controls.Add(panel)
                    Case Else
                        Debug.Assert(False, "Type not handled")
                End Select

            End If
        Next

        view.ResumeLayout() '(False)

        If view.Controls.Count > 0 Then
            aContainer.Controls.Add(view)

            aContainer.Width = Math.Max(aLocation.X + view.Width + 5, aContainer.Width)

            If aContainer.Name <> "tpInterface" Then
                aContainer.Height = Math.Max(view.Top + view.Height + 1, aContainer.Height)
            End If

            aLocation.Y = view.Top + view.Height + spacer
        End If
    End Sub

    Public Sub BuildInterface(ByVal Nodes As TreeNodeCollection, _
        ByVal aContainer As Control, ByRef Pos As Point, ByVal spacer As Integer, ByVal mandatory_only As Boolean, ByVal a_filemanager As FileManagerLocal)

        NodesToControls(Nodes, Pos, aContainer, spacer, mandatory_only, a_filemanager)
    End Sub

    Public Sub BuildInterface(ByVal TableDetails As ArrayList, _
            ByVal aContainer As Control, ByRef pos As Point, ByVal spacer As Integer, ByVal mandatory_only As Boolean, ByVal a_filemanager As FileManagerLocal)

        Dim archetypeTable As DataTable
        Dim rowHeadings As Collection
        Dim isRotated As Boolean

        Debug.Assert(TypeOf TableDetails(0) Is DataTable)
        Debug.Assert(TypeOf TableDetails(1) Is Collection)
        Debug.Assert(TypeOf TableDetails(2) Is Boolean)

        archetypeTable = CType(TableDetails(0), DataTable)
        rowHeadings = CType(TableDetails(1), Collection)
        isRotated = CType(TableDetails(2), Boolean)

        If isRotated Then


            For Each t As String In CType(CType(rowHeadings(1), ArchetypeElement).Constraint, Constraint_Text).AllowableValues.Codes
                Dim Rel_Pos As New Point(20, 20)

                Dim gb As New GroupBox
                gb.Text = a_filemanager.OntologyManager.GetTerm(t).Text
                gb.Location = pos

                Dim view As New ViewPanel( _
                        New ColumnLayout(Orientation.CENTER, Orientation.LEFT, spacer))
                view.Location = New Point(Rel_Pos.X, Rel_Pos.Y)
                view.SuspendLayout()

                For Each d_row As DataRow In archetypeTable.Rows
                    Debug.Assert(TypeOf d_row(2) Is ArchetypeElement)
                    Dim ae As ArchetypeElement = CType(d_row(2), ArchetypeElement)
                    If mandatory_only Then
                        If ae.Occurrences.MinCount > 0 Then
                            view.Controls.Add(ElementView(ae, a_filemanager))
                        End If
                    Else
                        view.Controls.Add(ElementView(ae, a_filemanager))
                    End If
                Next

                view.ResumeLayout()
                gb.Controls.Add(view)

                gb.Width = Rel_Pos.X + view.Width + 5
                gb.Height = view.Top + view.Height + 1

                pos.X = pos.X + gb.Width + 5
                aContainer.Controls.Add(gb)
            Next
        End If
    End Sub


    Private Function NodesToControls(ByVal NodeCol As TreeNodeCollection, _
        ByRef aLocation As Point, ByVal aContainer As Control, _
        ByVal spacer As Integer, ByVal mandatory_only As Boolean, ByVal a_filemanager As FileManagerLocal) As Integer

        'Displays the archetype nodes as GUI controls on the Interface TAB

        Dim view As New ViewPanel( _
        New ColumnLayout(Orientation.CENTER, Orientation.LEFT, spacer))
        view.Location = New Point(aLocation.X, aLocation.Y)
        view.SuspendLayout()

        For Each tvNode As ArchetypeTreeNode In NodeCol

            If ((tvNode.Item.IsMandatory) Or (Not mandatory_only)) Then

                Select Case tvNode.Item.RM_Class.Type
                    Case StructureType.Cluster
                        Dim ctrl As New Panel
                        ctrl.Location = aLocation

                        Dim lbl As New Label
                        lbl.Width = 150
                        lbl.Height = 20
                        lbl.Text = tvNode.Text
                        If lbl.Text.Length > 20 Then
                            lbl.AutoSize = True
                        End If

                        Dim rel_pos As New Point(0, 20)

                        lbl.Location = rel_pos
                        lbl.BorderStyle = BorderStyle.FixedSingle

                        ' cardinality as a tooltip
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
                            rel_pos.X = lbl.Location.X + lbl.Size.Width + 10
                            but.BackColor = System.Drawing.Color.LightGray

                            ToolTip1.SetToolTip(but, CType(tvNode.Item, ArchetypeNodeAbstract).RuntimeNameText)

                            but.Width = 30
                            but.Height = 20
                            but.Location = rel_pos

                            ctrl.Controls.Add(but)
                            ctrl.Width = 300
                        End If


                        rel_pos.X = 20
                        rel_pos.Y = 40

                        NodesToControls(tvNode.Nodes, rel_pos, ctrl, spacer, mandatory_only, a_filemanager)

                        view.Controls.Add(ctrl)

                    Case StructureType.Element, StructureType.Reference

                        view.Controls.Add(ElementView(CType(tvNode.Item, ArchetypeElement), a_filemanager))

                    Case StructureType.Slot
                        Dim newPanel As New Panel
                        newPanel.BorderStyle = BorderStyle.Fixed3D
                        newPanel.Size = New Size(150, 25)
                        Dim lbl As New Label
                        lbl.Text = Filemanager.GetOpenEhrTerm(312, "Slot") & ": " & tvNode.Text

                        newPanel.Controls.Add(lbl)

                        view.Controls.Add(newPanel)

                    Case Else
                        Beep()
                        Debug.Assert(False)


                End Select
            End If
        Next

        view.ResumeLayout() '(False)

        If view.Controls.Count > 0 Then
            aContainer.Controls.Add(view)

            aContainer.Width = Math.Max(aLocation.X + view.Width + 5, aContainer.Width)
            If aContainer.Name <> "tpInterface" Then
                aContainer.Height = Math.Max(view.Top + view.Height + 1, aContainer.Height)
            End If

            aLocation.Y = view.Top + view.Height + spacer
        End If

    End Function

    Public Shared Function ElementView(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal) As ElementViewControl

        'any additions need to be processed in the overloaded function below
        Select Case anElement.Constraint.Type
            Case ConstraintType.Text
                Return New TextViewControl(anElement, a_filemanager)

            Case ConstraintType.Quantity
                Return New QuantityViewControl(anElement, a_filemanager)

            Case ConstraintType.Duration
                Return New DurationViewControl(anElement, a_filemanager)

            Case ConstraintType.Boolean
                Return New BooleanViewControl(anElement, a_filemanager)

            Case ConstraintType.Ordinal
                Return New OrdinalViewControl(anElement, a_filemanager)

            Case ConstraintType.DateTime
                Return New DateTimeViewControl(anElement, a_filemanager)

            Case ConstraintType.Count
                Return New CountViewControl(anElement, a_filemanager)

            Case ConstraintType.Multiple
                Return New MultipleViewControl(anElement, a_filemanager)

            Case ConstraintType.URI
                Return New URIViewControl(anElement, a_filemanager)

            Case ConstraintType.Interval_Count, ConstraintType.Interval_Quantity, ConstraintType.Interval_DateTime
                Return New IntervalViewControl(anElement, a_filemanager)

            Case ConstraintType.Proportion
                Return New RatioViewControl(anElement, a_filemanager)

            Case ConstraintType.MultiMedia
                Return New MultiMediaViewControl(anElement, a_filemanager)

            Case Else
                Return New DatatypeViewControl(anElement, a_filemanager)
        End Select
    End Function

    Public Shared Function ElementView(ByVal aConstraint As Constraint, ByVal a_filemanager As FileManagerLocal) As ElementViewControl

        'any additions need to be processed in the overloaded function above

        Select Case aConstraint.Type
            Case ConstraintType.Text
                Return New TextViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Quantity
                Return New QuantityViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Duration
                Return New DurationViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Boolean
                Return New BooleanViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Ordinal
                Return New OrdinalViewControl(aConstraint, a_filemanager)

            Case ConstraintType.DateTime
                Return New DateTimeViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Count
                Return New CountViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Multiple
                Return New MultipleViewControl(aConstraint, a_filemanager)

            Case ConstraintType.URI
                Return New URIViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Interval_Count, ConstraintType.Interval_Quantity
                Return New IntervalViewControl(aConstraint, a_filemanager)

            Case ConstraintType.Proportion
                Return New RatioViewControl(aConstraint, a_filemanager)

            Case ConstraintType.MultiMedia
                Return New MultiMediaViewControl(aConstraint, a_filemanager)

            Case Else
                Return New DatatypeViewControl(aConstraint, a_filemanager)
        End Select
    End Function

    'JAR: 01JUN07, EDT-24 Interface tab does not release UID objects which causes crash
    Protected Sub Dispose(ByVal disposing As Boolean)
        mInstance = Nothing
        ToolTip1 = Nothing
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
