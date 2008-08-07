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

Public Class QuantityUnitConstraintControl : Inherits CountConstraintControl

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New(a_file_manager)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mFileManager = a_file_manager

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'QuantityUnitConstraintControl
        '
        Me.Name = "QuantityUnitConstraintControl"
        Me.Size = New System.Drawing.Size(384, 120)

    End Sub

#End Region


    Public WriteOnly Property LocalFileManager() As FileManagerLocal
        Set(ByVal Value As FileManagerLocal)
            mFileManager = Value
        End Set
    End Property

    Protected Shadows ReadOnly Property Constraint() As Constraint_QuantityUnit
        Get
            Debug.Assert(TypeOf MyBase.Constraint Is Constraint_Count)
            Return CType(MyBase.Constraint, Constraint_QuantityUnit)
        End Get
    End Property

    Public Sub Reset()
        Dim loading As Boolean = MyBase.IsLoading

        Try
            MyBase.IsLoading = True
            cbMaxValue.Checked = False
            numMaxValue.Value = 0
            cbMinValue.Checked = False
            numMinValue.Value = 0
            NumericAssumed.Value = 0
            comboIncludeMax.SelectedIndex = 0
            comboIncludeMin.SelectedIndex = 0
        Finally
            MyBase.IsLoading = loading
        End Try
    End Sub

    Protected Overrides Sub MaxValueChanged()
        Dim maximum As Decimal
        Decimal.TryParse(numMaxValue.Text, maximum)
        Constraint.MaximumRealValue = Convert.ToSingle(maximum, System.Globalization.NumberFormatInfo.InvariantInfo)
    End Sub

    Protected Overrides Sub MinValueChanged()
        Dim minimum As Decimal
        Decimal.TryParse(numMinValue.Text, minimum)
        Constraint.MinimumRealValue = Convert.ToSingle(minimum, System.Globalization.NumberFormatInfo.InvariantInfo)
    End Sub

    Protected Overloads Overrides Sub SetControlValues(ByVal IsState As Boolean)
        Reset()

        MyBase.SetControlValues(IsState)

        'SetMaxAndMin()

        'If IsState Then
        '    SetStateValues()
        'End If

    End Sub


    'Private Sub SetMaxAndMin()
    '    If Constraint.HasMaximum Then
    '        Me.cbMaxValue.Checked = True
    '        Me.numMaxValue.Value = CDec(Constraint.MaximumValue)
    '        If Constraint.IncludeMaximum Then
    '            Me.comboIncludeMax.SelectedIndex = 0
    '        Else
    '            Me.comboIncludeMax.SelectedIndex = 1
    '        End If
    '    Else
    '        Me.cbMaxValue.Checked = False
    '    End If

    '    If Constraint.HasMinimum Then
    '        Me.cbMinValue.Checked = True
    '        Me.numMinValue.Value = CDec(Constraint.MinimumValue)
    '        If Constraint.IncludeMinimum Then
    '            Me.comboIncludeMin.SelectedIndex = 0
    '        Else
    '            Me.comboIncludeMin.SelectedIndex = 1
    '        End If
    '    Else
    '        Me.cbMinValue.Checked = False
    '    End If
    'End Sub

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
'The Original Code is QuantityUnitConstraintControl.vb.
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
