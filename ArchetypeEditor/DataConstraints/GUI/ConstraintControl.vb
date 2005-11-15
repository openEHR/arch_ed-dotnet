'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class ConstraintControl
    Inherits System.Windows.Forms.UserControl

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
    Protected WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents HelpProviderConstraint As System.Windows.Forms.HelpProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProviderConstraint = New System.Windows.Forms.HelpProvider
        '
        'ConstraintControl
        '
        Me.Name = "ConstraintControl"

    End Sub

#End Region

    Private mIsLoading As Boolean = True
    Protected mFileManager As FileManagerLocal

    Protected Property IsLoading() As Boolean
        Get
            Return mIsLoading
        End Get
        Set(ByVal Value As Boolean)
            mIsLoading = Value
        End Set
    End Property

    Private mConstraint As Constraint
    Protected Property Constraint() As Constraint
        Get
            Debug.Assert(Not mConstraint Is Nothing)

            Return mConstraint
        End Get
        Set(ByVal Value As Constraint)
            mConstraint = Value
        End Set
    End Property

    Private Sub SetHelpTopic(ByVal a_constraint_type As ConstraintType)
        ' set the help topic

        Me.HelpProviderConstraint.SetHelpNavigator(Me, HelpNavigator.Topic)

        Select Case a_constraint_type
            Case ConstraintType.Boolean
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_boolean.htm")
            Case ConstraintType.Count
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_count.htm")
            Case ConstraintType.DateTime
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_date_time.html")
            Case ConstraintType.Interval_Count
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_interval.html")
            Case ConstraintType.Interval_Quantity
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_interval.html")
            Case ConstraintType.Multiple
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_interval.html")
            Case ConstraintType.Ordinal
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_multiple.html")
            Case ConstraintType.Quantity
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_quantity.htm")
            Case ConstraintType.Ratio
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "")
            Case ConstraintType.Slot
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_slot.html")
            Case ConstraintType.Text
                Me.HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Edit data/Data constraints/set_constraints_text.htm")
        End Select

    End Sub

    Public Overloads Sub ShowConstraint(ByVal IsState As Boolean, _
            ByVal aArchetypeElement As ArchetypeElement)

        mIsLoading = True

        mConstraint = aArchetypeElement.Constraint

        SetHelpTopic(mConstraint.Type)

        SetControlValues(IsState, aArchetypeElement)

        mIsLoading = False
    End Sub

    Public Overloads Sub ShowConstraint(ByVal IsState As Boolean, _
            ByVal aConstraint As Constraint)

        mIsLoading = True

        mConstraint = aConstraint

        SetHelpTopic(mConstraint.Type)

        Try
            If aConstraint.Type = ConstraintType.Ordinal Then
                SetControlValues(IsState, aConstraint)
            Else
                SetControlValues(IsState)
            End If

        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        mIsLoading = False

    End Sub

    Protected Overridable Sub SetControlValues(ByVal IsState As Boolean, _
            ByVal aArchetypeElement As ArchetypeElement)

        SetControlValues(IsState)
    End Sub

    Protected Overridable Sub SetControlValues(ByVal IsState As Boolean, _
                ByVal c As Constraint)
        'Added Sam Heard 2004-06-07
        ' required for multiple constraints
        SetControlValues(IsState)
    End Sub

    Protected Overridable Sub SetControlValues(ByVal IsState As Boolean)
        'Noop
    End Sub

    Public Shared Function CreateConstraintControl(ByVal aConstraintType As ConstraintType, ByVal a_file_manager As FileManagerLocal) As ConstraintControl

        Select Case aConstraintType
            'Case ConstraintType.Any

        Case ConstraintType.Boolean
                Return New BooleanConstraintControl(a_file_manager)

            Case ConstraintType.Quantity
                Return New QuantityConstraintControl(a_file_manager)

            Case ConstraintType.Count
                Return New CountConstraintControl(a_file_manager)

            Case ConstraintType.Text
                Return New TextConstraintControl(a_file_manager)

            Case ConstraintType.DateTime
                Return New DateTimeConstraintControl(a_file_manager)

            Case ConstraintType.Duration
                Return New DurationConstraintControl(a_file_manager)

            Case ConstraintType.Ordinal
                Return New OrdinalConstraintControl(a_file_manager)

            Case ConstraintType.Multiple
                Return New MultipleConstraintControl(a_file_manager)

            Case ConstraintType.Ratio
                Return New RatioConstraintControl(a_file_manager)

            Case ConstraintType.Slot
                Return New SlotConstraintControl(a_file_manager)

            Case ConstraintType.Interval_Count, ConstraintType.Interval_Quantity, ConstraintType.Interval_DateTime
                Return New IntervalConstraintControl(a_file_manager)

            Case ConstraintType.MultiMedia
                Return New MultiMediaConstraintControl(a_file_manager)

            Case Else
                Throw New ArgumentException( _
                        String.Format("'{0}' does not have a corresponding constraint control implemented", _
                        aConstraintType.ToString), "ConstraintType")
        End Select
    End Function

    Private Sub ConstraintControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.HelpProviderConstraint.HelpNamespace = ArchetypeEditor.Instance.Options.HelpLocationPath
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
'The Original Code is ConstraintControl.vb.
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
