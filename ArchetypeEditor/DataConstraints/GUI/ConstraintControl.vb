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
        If Not DesignMode AndAlso AE_Constants.HasInstance Then
            SetHelpDetails() ' Has to be here so it is not called in design mode
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

    Private Sub SetHelpTopic(ByVal a_constraint_type As ConstraintKind)
        HelpProviderConstraint.SetHelpNavigator(Me, HelpNavigator.Topic)

        Select Case a_constraint_type
            Case ConstraintKind.Boolean
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_boolean.htm")
            Case ConstraintKind.Count
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_count.htm")
            Case ConstraintKind.DateTime
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_date_tihtml")
            Case ConstraintKind.Interval_Count
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_interval.html")
            Case ConstraintKind.Interval_Quantity
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_interval.html")
            Case ConstraintKind.Multiple
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_interval.html")
            Case ConstraintKind.Ordinal
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_multiple.html")
            Case ConstraintKind.Quantity
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_quantity.htm")
            Case ConstraintKind.Proportion
                HelpProviderConstraint.SetHelpKeyword(Me, "")
            Case ConstraintKind.Slot
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_slot.html")
            Case ConstraintKind.Text
                HelpProviderConstraint.SetHelpKeyword(Me, "HowTo/Editing/Data constraints/set_constraints_text.htm")
        End Select
    End Sub

    Public Sub ShowElement(ByVal isState As Boolean, ByVal aArchetypeElement As ArchetypeElement)
        mIsLoading = True
        mConstraint = aArchetypeElement.Constraint
        SetHelpTopic(mConstraint.Kind)
        SetControlValuesFromElement(isState, aArchetypeElement)
        mIsLoading = False
    End Sub

    Public Sub ShowConstraint(ByVal isState As Boolean, ByVal aConstraint As Constraint)
        mIsLoading = True
        mConstraint = aConstraint
        SetHelpTopic(mConstraint.Kind)

        If aConstraint.Kind = ConstraintKind.Ordinal Then
            SetControlValuesFromConstraint(isState, aConstraint)
        Else
            SetControlValues(isState)
        End If

        mIsLoading = False
    End Sub

    Protected Overridable Sub SetControlValuesFromElement(ByVal isState As Boolean, ByVal aArchetypeElement As ArchetypeElement)
        SetControlValues(isState)
    End Sub

    Protected Overridable Sub SetControlValuesFromConstraint(ByVal isState As Boolean, ByVal c As Constraint)
        SetControlValues(isState)
    End Sub

    Protected Overridable Sub SetControlValues(ByVal isState As Boolean)
        'Noop
    End Sub

    Public Function HasConstraint() As Boolean
        Return Not mConstraint Is Nothing
    End Function

    Public Shared Function CreateConstraintControl(ByVal aConstraintType As ConstraintKind, ByVal a_file_manager As FileManagerLocal) As ConstraintControl

        Select Case aConstraintType
            'Case ConstraintType.Any

            Case ConstraintKind.Boolean
                Return New BooleanConstraintControl(a_file_manager)

            Case ConstraintKind.Quantity
                Return New QuantityConstraintControl(a_file_manager)

            Case ConstraintKind.Count, ConstraintKind.Real
                Return New CountConstraintControl(a_file_manager)

            Case ConstraintKind.Text
                Return New TextConstraintControl(a_file_manager)

            Case ConstraintKind.DateTime
                Return New DateTimeConstraintControl(a_file_manager)

            Case ConstraintKind.Duration
                Return New DurationConstraintControl(a_file_manager)

            Case ConstraintKind.Ordinal
                Return New OrdinalConstraintControl(a_file_manager)

            Case ConstraintKind.Multiple
                Return New MultipleConstraintControl(a_file_manager)

            Case ConstraintKind.Proportion
                Return New RatioConstraintControl(a_file_manager)

            Case ConstraintKind.Slot
                Return New SlotConstraintControl(a_file_manager)

            Case ConstraintKind.Interval_Count, ConstraintKind.Interval_Quantity, ConstraintKind.Interval_DateTime
                Return New IntervalConstraintControl(a_file_manager)

            Case ConstraintKind.MultiMedia
                Return New MultiMediaConstraintControl(a_file_manager)

            Case ConstraintKind.URI
                Return New UriConstraintControl(a_file_manager)

            Case ConstraintKind.Identifier
                Return New IdentifierConstraintControl(a_file_manager)

            Case ConstraintKind.Currency
                Return New CountConstraintControl(a_file_manager)

            Case ConstraintKind.Parsable
                Return New ParsableConstraintControl(a_file_manager)

            Case Else
                Throw New ArgumentException( _
                        String.Format("'{0}' does not have a corresponding constraint control implemented", _
                        aConstraintType.ToString), "ConstraintType")
        End Select
    End Function

    Private Sub SetHelpDetails()
        Me.HelpProviderConstraint.HelpNamespace = Main.Instance.Options.HelpLocationPath
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

