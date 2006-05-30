'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on an archetype slot"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'

Option Strict On

Public Class Constraint_Slot
    Inherits Constraint

    Friend colInclude As New CollectionOfSlots
    Friend colExclude As New CollectionOfSlots
    Dim mType As StructureType
    Dim mExcludeAll As Boolean
    Dim mIncludeAll As Boolean
    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Slot
        End Get
    End Property
    Public ReadOnly Property hasSlots() As Boolean
        Get
            Return (colInclude.Count > 0 Or colExclude.Count > 0 Or mIncludeAll Or mExcludeAll)
        End Get
    End Property
    Property RM_ClassType() As StructureType
        Get
            Return mType
        End Get
        Set(ByVal Value As StructureType)
            mType = Value
        End Set
    End Property
    Public Property IncludeAll() As Boolean
        Get
            Return mIncludeAll
        End Get
        Set(ByVal Value As Boolean)
            mIncludeAll = Value
        End Set
    End Property
    Public Property ExcludeAll() As Boolean
        Get
            Return mExcludeAll
        End Get
        Set(ByVal Value As Boolean)
            mExcludeAll = Value
        End Set
    End Property
    Public Property Include() As CollectionOfSlots
        Get
            Return colInclude
        End Get
        Set(ByVal Value As CollectionOfSlots)
            colInclude = Value
        End Set
    End Property
    Public Property Exclude() As CollectionOfSlots
        Get
            Return colExclude
        End Get
        Set(ByVal Value As CollectionOfSlots)
            colExclude = Value
        End Set
    End Property

    Public Overrides Function Copy() As Constraint
        Dim slot As Constraint_Slot = New Constraint_Slot
        slot.colInclude = Me.colInclude.Copy
        slot.colExclude = Me.colExclude.Copy
        Return slot
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
'The Original Code is SlotConstraint.vb.
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