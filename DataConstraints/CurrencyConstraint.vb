'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on real number"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraints/RealConstraint.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2007-01-09 19:45:11 +0930 (Tue, 09 Jan 2007) $"
'

Option Strict On

Public Class Constraint_Currency
    Inherits Constraint_Real

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Currency
        End Get
    End Property

    Public Property MinimumMoneyValue() As Double
        Get
            Return mMinVal
        End Get
        Set(ByVal Value As Double)
            mMinVal = Value
        End Set
    End Property

    Public Property MaximumMoneyValue() As Double
        Get
            Return mMaxVal
        End Get
        Set(ByVal Value As Double)
            mMaxVal = Value
        End Set
    End Property

    Public Overrides Property AssumedValue() As Object
        Get
            If HasAssumedValue Then
                Return CSng(mAssumedValue)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            Try
                mAssumedValue = CSng(Value)
            Catch ex As Exception
                Debug.Assert(False, Value.ToString & "is not valid value for this type")
            End Try
        End Set
    End Property

    Public Overrides Function Copy() As Constraint
        Dim c As New Constraint_Currency
        c.mHasMaxVal = mHasMaxVal
        c.mHasMinVal = mHasMinVal
        c.mMaxVal = mMaxVal
        c.mMinVal = mMinVal
        c.mAssumedValue = mAssumedValue
        c.HasAssumedValue = HasAssumedValue
        c.mPrecision = Precision
        Return c
    End Function

    Sub New()
        'default to 2 decimal places
        mPrecision = 2
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
'The Original Code is RealConstraint.vb.
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