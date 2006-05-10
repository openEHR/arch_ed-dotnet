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

Public Class IntervalConstraintControl : Inherits ConstraintControl

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()



    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = a_file_manager

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            Me.LabelInterval.Text = Filemanager.GetOpenEhrTerm(141, Me.LabelInterval.Text)
        End If
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ImageListDateTime As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LabelInterval As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(IntervalConstraintControl))
        Me.ImageListDateTime = New System.Windows.Forms.ImageList(Me.components)
        Me.LabelInterval = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageListDateTime
        '
        Me.ImageListDateTime.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageListDateTime.ImageStream = CType(resources.GetObject("ImageListDateTime.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListDateTime.TransparentColor = System.Drawing.Color.Transparent
        '
        'LabelInterval
        '
        Me.LabelInterval.BackColor = System.Drawing.Color.Transparent
        Me.LabelInterval.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelInterval.Location = New System.Drawing.Point(8, 6)
        Me.LabelInterval.Name = "LabelInterval"
        Me.LabelInterval.Size = New System.Drawing.Size(248, 24)
        Me.LabelInterval.TabIndex = 36
        Me.LabelInterval.Text = "Interval"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LabelInterval)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(304, 32)
        Me.Panel1.TabIndex = 37
        '
        'IntervalConstraintControl
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "IntervalConstraintControl"
        Me.Size = New System.Drawing.Size(304, 168)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private mConstraintControl As ConstraintControl

    Private Shadows ReadOnly Property Constraint() As Constraint_Interval
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Interval)

            Return CType(MyBase.Constraint, Constraint_Interval)
        End Get
    End Property

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)

        Select Case MyBase.Constraint.Type
            Case ConstraintType.Interval_Count
                Me.LabelInterval.Text = AE_Constants.Instance.IntervalCount
                mConstraintControl = New CountConstraintControl(mFileManager)
                'Fixme - need constants
                CType(mConstraintControl, CountConstraintControl).cbMinValue.Text = _
                    AE_Constants.Instance.SetAbsoluteMin
                CType(mConstraintControl, CountConstraintControl).cbMaxValue.Text = _
                    AE_Constants.Instance.SetAbsoluteMax
            Case ConstraintType.Interval_Quantity
                Me.LabelInterval.Text = AE_Constants.Instance.IntervalQuantity
                mConstraintControl = New QuantityConstraintControl(mFileManager)
                CType(mConstraintControl, QuantityConstraintControl).QuantityUnitConstraint.cbMinValue.Text = _
                    AE_Constants.Instance.SetAbsoluteMin
                CType(mConstraintControl, QuantityConstraintControl).QuantityUnitConstraint.cbMaxValue.Text = _
                    AE_Constants.Instance.SetAbsoluteMax
            Case ConstraintType.Interval_DateTime
                Me.LabelInterval.Text = AE_Constants.Instance.IntervalDateTime
                mConstraintControl = New DateTimeConstraintControl(mFileManager)
        End Select

        Me.Controls.Add(mConstraintControl)

        ' Ensures the ZOrder leads to no overlap
        mConstraintControl.Dock = DockStyle.Fill

        ' HKF: 1620
        mConstraintControl.ShowConstraint(IsState, CType(MyBase.Constraint, Constraint_Interval).AbsoluteLimits)

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
'The Original Code is DateTimeConstraintControl.vb.
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
