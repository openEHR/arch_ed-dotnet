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

Public Class ConstraintContextMenu
    Inherits ContextMenu

    Public Delegate Sub Click(ByVal newConstraint As Constraint)
    Private onClick As Click
    Private mFileManager As FileManagerLocal

    Private _
        HeaderMenuItem,
        SpacerMenuItem,
        TextMenuItem,
        QuantityMenuItem,
        CountMenuItem,
        DateTimeMenuItem,
        OrdinalMenuItem,
        BooleanMenuItem,
        AnyMenuItem,
        MultipleMenuItem,
        SlotMenuItem,
        ProportionMenuItem,
        IntervalMenuItem,
        IntervalOfQuantityMenuItem,
        IntervalOfCountMenuItem,
        IntervalOfDateTimeMenuItem,
        IntervalOfDurationMenuItem,
        DurationMenuItem,
        MultiMediaMenuItem,
        UriMenuItem,
        IdentifierMenuItem,
        ParsableMenuItem,
        CurrencyMenuItem As MenuItem

    Public Sub ShowHeader(ByVal headerText As String)
        HeaderMenuItem.Text = headerText
        HeaderMenuItem.Visible = True
        SpacerMenuItem.Visible = True
    End Sub

    Public Sub ShowMenuItem(ByVal kind As ConstraintKind, ByVal isVisible As Boolean)
        Select Case kind
            Case ConstraintKind.Any
                AnyMenuItem.Visible = isVisible
            Case ConstraintKind.Boolean
                BooleanMenuItem.Visible = isVisible
            Case ConstraintKind.Count
                CountMenuItem.Visible = isVisible
            Case ConstraintKind.DateTime
                DateTimeMenuItem.Visible = isVisible
            Case ConstraintKind.Multiple
                MultipleMenuItem.Visible = isVisible
            Case ConstraintKind.Ordinal
                OrdinalMenuItem.Visible = isVisible
            Case ConstraintKind.Quantity
                QuantityMenuItem.Visible = isVisible
            Case ConstraintKind.Duration
                DurationMenuItem.Visible = isVisible
            Case ConstraintKind.Proportion
                ProportionMenuItem.Visible = isVisible
            Case ConstraintKind.Slot
                SlotMenuItem.Visible = isVisible
            Case ConstraintKind.Text
                TextMenuItem.Visible = isVisible
            Case ConstraintKind.Interval_Count
                IntervalOfCountMenuItem.Visible = isVisible
            Case ConstraintKind.Interval_Quantity
                IntervalOfQuantityMenuItem.Visible = isVisible
            Case ConstraintKind.Interval_DateTime
                IntervalOfDateTimeMenuItem.Visible = isVisible
            Case ConstraintKind.Interval_Duration
                IntervalOfDurationMenuItem.Visible = isVisible
            Case ConstraintKind.MultiMedia
                MultiMediaMenuItem.Visible = isVisible
            Case ConstraintKind.URI
                UriMenuItem.Visible = isVisible
            Case ConstraintKind.Currency
                CurrencyMenuItem.Visible = isVisible
            Case ConstraintKind.Identifier
                IdentifierMenuItem.Visible = isVisible
            Case ConstraintKind.Parsable
                ParsableMenuItem.Visible = isVisible
        End Select

        IntervalMenuItem.Visible = IntervalOfCountMenuItem.Visible Or IntervalOfQuantityMenuItem.Visible Or IntervalOfDateTimeMenuItem.Visible Or IntervalOfDurationMenuItem.Visible
    End Sub

    Protected Sub DoClick(ByVal sender As Object, ByVal e As EventArgs)
        If sender Is TextMenuItem Then
            onClick(New Constraint_Text)
        ElseIf sender Is QuantityMenuItem Then
            onClick(New Constraint_Quantity)
        ElseIf sender Is CountMenuItem Then
            onClick(New Constraint_Count)
        ElseIf sender Is DateTimeMenuItem Then
            onClick(New Constraint_DateTime)
        ElseIf sender Is OrdinalMenuItem Then
            onClick(New Constraint_Ordinal(True, mFileManager))
        ElseIf sender Is BooleanMenuItem Then
            onClick(New Constraint_Boolean)
        ElseIf sender Is AnyMenuItem Then
            onClick(New Constraint)
        ElseIf sender Is DurationMenuItem Then
            onClick(New Constraint_Duration)
        ElseIf sender Is MultipleMenuItem Then
            onClick(New Constraint_Choice)
        ElseIf sender Is SlotMenuItem Then
            onClick(New Constraint_Slot)
        ElseIf sender Is ProportionMenuItem Then
            onClick(New Constraint_Proportion)
            'Cannot be Quantity Unit alone
            'ElseIf Sender Is mMI_QuantityUnit Then
            '    click(New Constraint_QuantityUnit)
        ElseIf sender Is IntervalOfCountMenuItem Then
            onClick(New Constraint_Interval_Count)
        ElseIf sender Is IntervalOfQuantityMenuItem Then
            onClick(New Constraint_Interval_Quantity)
        ElseIf sender Is IntervalOfDateTimeMenuItem Then
            onClick(New Constraint_Interval_DateTime)
        ElseIf sender Is IntervalOfDurationMenuItem Then
            onClick(New Constraint_Interval_Duration)
        ElseIf sender Is MultiMediaMenuItem Then
            onClick(New Constraint_MultiMedia)
        ElseIf sender Is UriMenuItem Then
            onClick(New Constraint_URI)
        ElseIf sender Is IdentifierMenuItem Then
            onClick(New Constraint_Identifier)
        ElseIf sender Is CurrencyMenuItem Then
            onClick(New Constraint_Currency)
        ElseIf sender Is ParsableMenuItem Then
            onClick(New Constraint_Parsable)
        Else
            Debug.Assert(False, "Menu item is not loaded")
        End If
    End Sub

    Protected Function NewMenuItem(ByVal parent As Menu, ByVal text As String) As MenuItem
        Dim result As New MenuItem(text)
        parent.MenuItems.Add(result)
        AddHandler result.Click, AddressOf DoClick
        Return result
    End Function

    Sub New(ByVal eventHandler As Click, ByVal fileManager As FileManagerLocal)
        onClick = eventHandler
        mFileManager = fileManager

        ' add invisible head item
        HeaderMenuItem = New MenuItem
        MenuItems.Add(HeaderMenuItem)
        HeaderMenuItem.Visible = False
        ' and invisible spacer
        SpacerMenuItem = New MenuItem
        SpacerMenuItem.Text = "----------"
        SpacerMenuItem.Enabled = False
        MenuItems.Add(SpacerMenuItem)
        SpacerMenuItem.Visible = False

        TextMenuItem = NewMenuItem(Me, AE_Constants.Instance.Text)
        QuantityMenuItem = NewMenuItem(Me, AE_Constants.Instance.Quantity)
        CountMenuItem = NewMenuItem(Me, AE_Constants.Instance.Count)
        DateTimeMenuItem = NewMenuItem(Me, AE_Constants.Instance.DateTime)
        DurationMenuItem = NewMenuItem(Me, AE_Constants.Instance.Duration)
        OrdinalMenuItem = NewMenuItem(Me, AE_Constants.Instance.Ordinal)
        BooleanMenuItem = NewMenuItem(Me, AE_Constants.Instance.Boolean_)
        IntervalMenuItem = New MenuItem(AE_Constants.Instance.Interval)
        MenuItems.Add(IntervalMenuItem)
        IntervalOfCountMenuItem = NewMenuItem(IntervalMenuItem, AE_Constants.Instance.IntervalCount)
        IntervalOfQuantityMenuItem = NewMenuItem(IntervalMenuItem, AE_Constants.Instance.IntervalQuantity)
        IntervalOfDateTimeMenuItem = NewMenuItem(IntervalMenuItem, AE_Constants.Instance.IntervalDateTime)
        IntervalOfDurationMenuItem = NewMenuItem(IntervalMenuItem, AE_Constants.Instance.IntervalDuration)
        AnyMenuItem = NewMenuItem(Me, AE_Constants.Instance.Any)
        MultipleMenuItem = NewMenuItem(Me, AE_Constants.Instance.Multiple)
        MultiMediaMenuItem = NewMenuItem(Me, AE_Constants.Instance.MultiMedia)
        UriMenuItem = NewMenuItem(Me, AE_Constants.Instance.URI)
        ProportionMenuItem = NewMenuItem(Me, AE_Constants.Instance.Proportion)
        IdentifierMenuItem = NewMenuItem(Me, AE_Constants.Instance.Identifier)
        'TODO CurrencyMenuItem = NewMenuItem(Me, AE_Constants.Instance.Currency)
        SlotMenuItem = NewMenuItem(Me, AE_Constants.Instance.Slot)
        ParsableMenuItem = NewMenuItem(Me, AE_Constants.Instance.Parsable)
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
