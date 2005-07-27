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

Public Class RatioConstraintControl : Inherits ConstraintControl 'AnyConstraintControl
    Friend WithEvents PanelMultipleControl As System.Windows.Forms.Panel
    Friend WithEvents TabConstraints As System.Windows.Forms.TabControl
    Private mIsState As Boolean
    Private mDenominator As TabPage

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = a_file_manager

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents PanelBoolean As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuDataType As System.Windows.Forms.ContextMenu
    Friend WithEvents cbAsPercent As System.Windows.Forms.CheckBox
    Friend WithEvents lblRatio As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabConstraints = New System.Windows.Forms.TabControl
        Me.PanelMultipleControl = New System.Windows.Forms.Panel
        Me.ContextMenuDataType = New System.Windows.Forms.ContextMenu
        Me.cbAsPercent = New System.Windows.Forms.CheckBox
        Me.lblRatio = New System.Windows.Forms.Label
        Me.PanelMultipleControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabConstraints
        '
        Me.TabConstraints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabConstraints.Location = New System.Drawing.Point(0, 32)
        Me.TabConstraints.Name = "TabConstraints"
        Me.TabConstraints.SelectedIndex = 0
        Me.TabConstraints.Size = New System.Drawing.Size(392, 176)
        Me.TabConstraints.TabIndex = 0
        '
        'PanelMultipleControl
        '
        Me.PanelMultipleControl.Controls.Add(Me.lblRatio)
        Me.PanelMultipleControl.Controls.Add(Me.cbAsPercent)
        Me.PanelMultipleControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMultipleControl.Location = New System.Drawing.Point(0, 0)
        Me.PanelMultipleControl.Name = "PanelMultipleControl"
        Me.PanelMultipleControl.Size = New System.Drawing.Size(392, 32)
        Me.PanelMultipleControl.TabIndex = 1
        '
        'cbAsPercent
        '
        Me.cbAsPercent.Location = New System.Drawing.Point(208, 8)
        Me.cbAsPercent.Name = "cbAsPercent"
        Me.cbAsPercent.Size = New System.Drawing.Size(176, 16)
        Me.cbAsPercent.TabIndex = 0
        Me.cbAsPercent.Text = "As percent (%)"
        '
        'lblRatio
        '
        Me.lblRatio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRatio.Location = New System.Drawing.Point(16, 8)
        Me.lblRatio.Name = "lblRatio"
        Me.lblRatio.Size = New System.Drawing.Size(72, 16)
        Me.lblRatio.TabIndex = 1
        Me.lblRatio.Text = "Ratio"
        '
        'RatioConstraintControl
        '
        Me.Controls.Add(Me.TabConstraints)
        Me.Controls.Add(Me.PanelMultipleControl)
        Me.Name = "RatioConstraintControl"
        Me.Size = New System.Drawing.Size(392, 208)
        Me.PanelMultipleControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region



    Private Shadows ReadOnly Property Constraint() As Constraint_Ratio
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Ratio)

            Return CType(MyBase.Constraint, Constraint_Ratio)
        End Get
    End Property

    Private Sub SetFactor(ByVal c As Constraint, ByVal name As String)
        Dim tp As TabPage
        Dim cc As ConstraintControl

        tp = New TabPage(name)

        cc = ConstraintControl.CreateConstraintControl( _
        c.Type, mFileManager)
        tp.Controls.Add(cc)
        cc.ShowConstraint(mIsState, c)
        cc.Dock = DockStyle.Fill
        Me.TabConstraints.TabPages.Add(tp)
    End Sub
    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)
        mIsState = IsState
        ' set constraint values on control

        SetFactor(Me.Constraint.Numerator, Filemanager.Instance.OntologyManager.GetOpenEHRTerm(450, "Numerator"))
        SetFactor(Me.Constraint.Denominator, Filemanager.Instance.OntologyManager.GetOpenEHRTerm(449, "Denominator"))

        If Me.Constraint.Denominator.Type = ConstraintType.Count Then
            cbAsPercent.Visible = True
            If CType(Me.Constraint.Denominator, Constraint_Count).MaximumValue = 100 AndAlso CType(Me.Constraint.Denominator, Constraint_Count).MinimumValue = 100 Then
                cbAsPercent.Checked = True
            End If
        Else
            cbAsPercent.Visible = False
        End If
    End Sub

    Private Sub cbAsPercent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAsPercent.CheckedChanged

        If cbAsPercent.Checked Then

            mDenominator = Me.TabConstraints.TabPages.Item(1)
            Me.TabConstraints.TabPages.RemoveAt(1)

            Select Case Me.Constraint.Denominator.Type
                Case ConstraintType.Count
                    CType(Me.Constraint.Denominator, Constraint_Count).MinimumValue = 100
                    CType(Me.Constraint.Denominator, Constraint_Count).MaximumValue = 100
                    'Case ConstraintType.Ordinal
                    '    Debug.Assert(False, "Not handled")
                    'Case ConstraintType.Quantity
                    '    Debug.Assert(False, "Not handled")
                    'Case ConstraintType.DateTime
                    '    Debug.Assert(False, "Not handled")
                    'Case ConstraintType.Duration
                    '    Debug.Assert(False, "Not handled")
            End Select

        Else
            Me.TabConstraints.TabPages.Add(mDenominator)
        End If

        If MyBase.IsLoading Then Return

        mFileManager.FileEdited = True
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
'The Original Code is MultipleConstraintControl.vb.
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