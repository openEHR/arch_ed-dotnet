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

Public Class ConstraintContextMenu
    Inherits ContextMenu

    Public Delegate Sub ProcessMenuClick(ByVal a_constraint As Constraint)
    Private _ProcessMenuClick As ProcessMenuClick
    Private mFileManager As FileManagerLocal
    Private mMI_Header, mMI_Spacer, mMI_Text, _
        mMI_Quantity, mMI_Count, mMI_DateTime, _
        mMI_Ordinal, mMI_Boolean, mMI_any, mMI_Multiple, _
        mMI_Slot, mMI_Ratio, mMI_QuantityUnit, mMI_Interval, _
        mMI_Interval_Quantity, mMI_Interval_Count, mMI_Interval_DateTime, _
        mMI_Duration, mMI_MultiMedia, mMI_URI, mMI_Identifier, mMI_Currency, _
        mMI_Parsable As MenuItem

    Sub ShowHeader(ByVal header_text As String)
        mMI_Header.Text = header_text
        mMI_Header.Visible = True
        mMI_Spacer.Visible = True
    End Sub

    Sub ShowMenuItem(ByVal a_constraint_type As ConstraintType)
        Select Case a_constraint_type
            Case ConstraintType.Any
                mMI_any.Visible = True
            Case ConstraintType.Boolean
                mMI_Boolean.Visible = True
            Case ConstraintType.Count
                mMI_Count.Visible = True
            Case ConstraintType.DateTime
                mMI_DateTime.Visible = True
            Case ConstraintType.Multiple
                mMI_Multiple.Visible = True
            Case ConstraintType.Ordinal
                mMI_Ordinal.Visible = True
            Case ConstraintType.Quantity
                mMI_Quantity.Visible = True
            Case ConstraintType.QuantityUnit
                mMI_QuantityUnit.Visible = True
            Case ConstraintType.Duration
                mMI_Duration.Visible = True
            Case ConstraintType.Proportion
                mMI_Ratio.Visible = True
            Case ConstraintType.Slot
                mMI_Slot.Visible = True
            Case ConstraintType.Text
                mMI_Text.Visible = True
            Case ConstraintType.Interval_Count
                mMI_Interval_Count.Visible = True
                mMI_Interval.Visible = True
            Case ConstraintType.Interval_Quantity
                mMI_Interval_Quantity.Visible = True
                mMI_Interval.Visible = True
            Case ConstraintType.Interval_DateTime
                mMI_Interval_DateTime.Visible = True
                mMI_Interval.Visible = True
            Case ConstraintType.MultiMedia
                mMI_MultiMedia.Visible = True
            Case ConstraintType.URI
                mMI_URI.Visible = True
            Case ConstraintType.Currency
                mMI_Currency.Visible = True
            Case ConstraintType.Identifier
                mMI_Identifier.Visible = True
            Case ConstraintType.Parsable
                mMI_Parsable.Visible = True
        End Select
    End Sub

    Sub HideMenuItem(ByVal a_constraint_type As ConstraintType)
        Select Case a_constraint_type
            Case ConstraintType.Any
                mMI_any.Visible = False
            Case ConstraintType.Boolean
                mMI_Boolean.Visible = False
            Case ConstraintType.Count
                mMI_Count.Visible = False
            Case ConstraintType.DateTime
                mMI_DateTime.Visible = False
            Case ConstraintType.Multiple
                mMI_Multiple.Visible = False
            Case ConstraintType.Ordinal
                mMI_Ordinal.Visible = False
            Case ConstraintType.Quantity
                mMI_Quantity.Visible = False
            Case ConstraintType.QuantityUnit
                mMI_QuantityUnit.Visible = False
            Case ConstraintType.Duration
                mMI_Duration.Visible = False
            Case ConstraintType.Proportion
                mMI_Ratio.Visible = False
            Case ConstraintType.Slot
                mMI_Slot.Visible = False
            Case ConstraintType.Text
                mMI_Text.Visible = False
            Case ConstraintType.Interval_Count
                mMI_Interval_Count.Visible = False
            Case ConstraintType.Interval_Quantity
                mMI_Interval_Quantity.Visible = False
            Case ConstraintType.Interval_DateTime
                mMI_Interval_DateTime.Visible = False
            Case ConstraintType.MultiMedia
                mMI_MultiMedia.Visible = False
            Case ConstraintType.URI
                mMI_URI.Visible = False
            Case ConstraintType.Currency
                mMI_Currency.Visible = False
            Case ConstraintType.Identifier
                mMI_Identifier.Visible = False
            Case ConstraintType.Parsable
                mMI_Parsable.Visible = False
        End Select

        If mMI_Interval_Count.Visible = False And mMI_Interval_Quantity.Visible = False And mMI_Interval_DateTime.Visible = False Then
            mMI_Interval.Visible = False
        Else
            mMI_Interval.Visible = True
        End If
    End Sub
    Sub Reset()
        mMI_Header.Text = ""
        mMI_Header.Visible = False
        mMI_Spacer.Visible = False
        mMI_Text.Visible = True
        mMI_Quantity.Visible = True
        mMI_Duration.Visible = True
        mMI_Count.Visible = True
        mMI_DateTime.Visible = True
        mMI_Ordinal.Visible = True
        mMI_Boolean.Visible = True
        mMI_any.Visible = True
        mMI_Multiple.Visible = True
        mMI_Interval.Visible = True
        mMI_Interval_Quantity.Visible = True
        mMI_Interval_Count.Visible = True
        mMI_Interval_DateTime.Visible = True
        mMI_MultiMedia.Visible = True
        mMI_URI.Visible = True
        mMI_Ratio.Visible = True
        mMI_Slot.Visible = True
        mMI_Currency.Visible = True
        mMI_Identifier.Visible = True
        mMI_Parsable.Visible = True

        '==========================
        'Rest are not usual datatypes
        mMI_QuantityUnit.Visible = False
    End Sub

    Sub HideAll()
        mMI_Text.Visible = False
        mMI_Quantity.Visible = False
        mMI_Duration.Visible = False
        mMI_Count.Visible = False
        mMI_DateTime.Visible = False
        mMI_Ordinal.Visible = False
        mMI_Boolean.Visible = False
        mMI_any.Visible = False
        mMI_Multiple.Visible = False
        mMI_Slot.Visible = False
        mMI_Ratio.Visible = False
        mMI_QuantityUnit.Visible = False
        mMI_Interval.Visible = False
        mMI_Interval_Quantity.Visible = False
        mMI_Interval_Count.Visible = False
        mMI_Interval_DateTime.Visible = False
        mMI_MultiMedia.Visible = False
        mMI_URI.Visible = False
        mMI_Currency.Visible = False
        mMI_Identifier.Visible = False
        mMI_Parsable.Visible = False

    End Sub

    Private Sub InternalProcessMenuItemClick(ByVal Sender As Object, ByVal e As EventArgs)
        If Sender Is mMI_Text Then
            _ProcessMenuClick(New Constraint_Text)
        ElseIf Sender Is mMI_Quantity Then
            _ProcessMenuClick(New Constraint_Quantity)
        ElseIf Sender Is mMI_Count Then
            _ProcessMenuClick(New Constraint_Count)
        ElseIf Sender Is mMI_DateTime Then
            _ProcessMenuClick(New Constraint_DateTime)
        ElseIf Sender Is mMI_Ordinal Then
            _ProcessMenuClick(New Constraint_Ordinal(True, mFileManager))
        ElseIf Sender Is mMI_Boolean Then
            _ProcessMenuClick(New Constraint_Boolean)
        ElseIf Sender Is mMI_any Then
            _ProcessMenuClick(New Constraint)
        ElseIf Sender Is mMI_Duration Then
            _ProcessMenuClick(New Constraint_Duration)
        ElseIf Sender Is mMI_Multiple Then
            _ProcessMenuClick(New Constraint_Choice)
        ElseIf Sender Is mMI_Slot Then
            _ProcessMenuClick(New Constraint_Slot)
        ElseIf Sender Is mMI_Ratio Then
            _ProcessMenuClick(New Constraint_Proportion)
            'Cannot be Quantity Unit alone
            'ElseIf Sender Is mMI_QuantityUnit Then
            '    _ProcessMenuClick(New Constraint_QuantityUnit)
        ElseIf Sender Is mMI_Interval_Count Then
            _ProcessMenuClick(New Constraint_Interval_Count)
        ElseIf Sender Is mMI_Interval_Quantity Then
            _ProcessMenuClick(New Constraint_Interval_Quantity)
        ElseIf Sender Is mMI_Interval_DateTime Then
            _ProcessMenuClick(New Constraint_Interval_DateTime)
        ElseIf Sender Is mMI_MultiMedia Then
            _ProcessMenuClick(New Constraint_MultiMedia)
        ElseIf Sender Is mMI_URI Then
            _ProcessMenuClick(New Constraint_URI)
        ElseIf Sender Is mMI_Identifier Then
            _ProcessMenuClick(New Constraint_Identifier)
        ElseIf Sender Is mMI_Currency Then
            _ProcessMenuClick(New Constraint_Currency)
        ElseIf Sender Is mMI_Parsable Then
            _ProcessMenuClick(New Constraint_Parsable)
        Else
            Debug.Assert(False, "Menu item is not loaded")
        End If
    End Sub

    Sub New(ByVal a_sub As ProcessMenuClick, ByVal a_filemanager As FileManagerLocal)
        mFileManager = a_filemanager

        _ProcessMenuClick = a_sub

        ' add invisible head item
        mMI_Header = New MenuItem
        Me.MenuItems.Add(mMI_Header)
        mMI_Header.Visible = False
        ' and invisible spacer
        mMI_Spacer = New MenuItem
        mMI_Spacer.Text = "----------"
        mMI_Spacer.Enabled = False
        Me.MenuItems.Add(mMI_Spacer)
        mMI_Spacer.Visible = False

        mMI_Text = New MenuItem(AE_Constants.Instance.Text)
        Me.MenuItems.Add(mMI_Text)
        AddHandler mMI_Text.Click, AddressOf InternalProcessMenuItemClick
        mMI_Quantity = New MenuItem(AE_Constants.Instance.Quantity)
        Me.MenuItems.Add(mMI_Quantity)
        AddHandler mMI_Quantity.Click, AddressOf InternalProcessMenuItemClick
        mMI_Count = New MenuItem(AE_Constants.Instance.Count)
        Me.MenuItems.Add(mMI_Count)
        AddHandler mMI_Count.Click, AddressOf InternalProcessMenuItemClick
        mMI_DateTime = New MenuItem(AE_Constants.Instance.DateTime)
        Me.MenuItems.Add(mMI_DateTime)
        AddHandler mMI_DateTime.Click, AddressOf InternalProcessMenuItemClick
        mMI_Duration = New MenuItem(AE_Constants.Instance.Duration)
        Me.MenuItems.Add(mMI_Duration)
        AddHandler mMI_Duration.Click, AddressOf InternalProcessMenuItemClick
        mMI_Ordinal = New MenuItem(AE_Constants.Instance.Ordinal)
        Me.MenuItems.Add(mMI_Ordinal)
        AddHandler mMI_Ordinal.Click, AddressOf InternalProcessMenuItemClick
        mMI_Boolean = New MenuItem(AE_Constants.Instance.Boolean_)
        Me.MenuItems.Add(mMI_Boolean)
        AddHandler mMI_Boolean.Click, AddressOf InternalProcessMenuItemClick
        mMI_Interval = New MenuItem(AE_Constants.Instance.Interval)
        Me.MenuItems.Add(mMI_Interval)
        mMI_Interval_Count = New MenuItem(AE_Constants.Instance.IntervalCount)
        mMI_Interval.MenuItems.Add(mMI_Interval_Count)
        AddHandler mMI_Interval_Count.Click, AddressOf InternalProcessMenuItemClick
        mMI_Interval_Quantity = New MenuItem(AE_Constants.Instance.IntervalQuantity)
        mMI_Interval.MenuItems.Add(mMI_Interval_Quantity)
        AddHandler mMI_Interval_Quantity.Click, AddressOf InternalProcessMenuItemClick
        mMI_Interval_DateTime = New MenuItem(AE_Constants.Instance.IntervalDateTime)
        mMI_Interval.MenuItems.Add(mMI_Interval_DateTime)
        AddHandler mMI_Interval_DateTime.Click, AddressOf InternalProcessMenuItemClick
        mMI_any = New MenuItem(AE_Constants.Instance.Any)
        Me.MenuItems.Add(mMI_any)
        AddHandler mMI_any.Click, AddressOf InternalProcessMenuItemClick
        mMI_Multiple = New MenuItem(AE_Constants.Instance.Multiple)
        Me.MenuItems.Add(mMI_Multiple)
        AddHandler mMI_Multiple.Click, AddressOf InternalProcessMenuItemClick
        mMI_MultiMedia = New MenuItem(AE_Constants.Instance.MultiMedia)
        Me.MenuItems.Add(mMI_MultiMedia)
        AddHandler mMI_MultiMedia.Click, AddressOf InternalProcessMenuItemClick
        mMI_URI = New MenuItem(AE_Constants.Instance.URI)
        Me.MenuItems.Add(mMI_URI)
        AddHandler mMI_URI.Click, AddressOf InternalProcessMenuItemClick
        mMI_Ratio = New MenuItem(AE_Constants.Instance.Proportion)
        Me.MenuItems.Add(mMI_Ratio)
        AddHandler mMI_Ratio.Click, AddressOf InternalProcessMenuItemClick
        mMI_Identifier = New MenuItem(AE_Constants.Instance.Identifier)
        Me.MenuItems.Add(mMI_Identifier)
        AddHandler mMI_Identifier.Click, AddressOf InternalProcessMenuItemClick
        mMI_Currency = New MenuItem(AE_Constants.Instance.Currency)
        'UNCOMMENT TO ADD CURRENCY
        'Me.MenuItems.Add(mMI_Currency)
        'AddHandler mMI_Currency.Click, AddressOf InternalProcessMenuItemClick

        mMI_Slot = New MenuItem(AE_Constants.Instance.Slot)
        Me.MenuItems.Add(mMI_Slot)
        AddHandler mMI_Slot.Click, AddressOf InternalProcessMenuItemClick
        'mMI_Slot.Visible = False
        mMI_QuantityUnit = New MenuItem(AE_Constants.Instance.Unit)
        Me.MenuItems.Add(mMI_QuantityUnit)
        AddHandler mMI_QuantityUnit.Click, AddressOf InternalProcessMenuItemClick
        mMI_QuantityUnit.Visible = False
        mMI_Parsable = New MenuItem(AE_Constants.Instance.Parsable)
        Me.MenuItems.Add(mMI_Parsable)
        AddHandler mMI_Parsable.Click, AddressOf InternalProcessMenuItemClick
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
'The Original Code is ConstraintContextMenu.vb.
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
