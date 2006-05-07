'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on Quantity values"
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

Public Class Constraint_Quantity
    Inherits Constraint

    Private mUnits As New Collection
    Private mTerminologyId As String = "openehr"
    Private mOpenEhrCode As String  'carries uncoded value
    Private mSeparator As String = "::"
    Private mIsCoded As Boolean

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Quantity
        End Get
    End Property
    Public ReadOnly Property has_units() As Boolean
        Get
            Return mUnits.Count > 0
        End Get
    End Property
    Public Property PhysicalPropertyAsString() As String
        Get
            If mIsCoded Then
                Return mTerminologyId & mSeparator & mOpenEhrCode
            Else
                Return mOpenEhrCode
            End If
        End Get
        Set(ByVal Value As String)
            Dim i As Integer = Value.IndexOf(mSeparator)
            If i > -1 Then
                mIsCoded = True
                mTerminologyId = Value.Substring(0, i)
                mOpenEhrCode = Value.Substring(i + mSeparator.Length)
            Else
                mIsCoded = False
                mOpenEhrCode = Value
            End If
        End Set
    End Property
    Public ReadOnly Property IsNull() As Boolean
        Get
            Return mOpenEhrCode = ""
        End Get
    End Property
    Public Property OpenEhrCode() As Integer
        Get
            If mIsCoded Then
                Try
                    Return Integer.Parse(mOpenEhrCode)
                Catch
                    Return 0
                End Try
            End If
        End Get
        Set(ByVal Value As Integer)
            mOpenEhrCode = Value.ToString()
            If mOpenEhrCode <> "" Then
                mIsCoded = True
            End If
        End Set
    End Property
    Public ReadOnly Property IsTime() As Boolean
        Get
            Return mOpenEhrCode = "128"
        End Get
    End Property
    Public ReadOnly Property IsCoded() As Boolean
        Get
            Return mIsCoded
        End Get
    End Property
    Public Property Units() As Collection
        Get
            Return mUnits
        End Get
        Set(ByVal Value As Collection)
            mUnits = Value
        End Set
    End Property

    Public Overrides Function Copy() As Constraint
        Dim q As New Constraint_Quantity

        q.mUnits = Me.mUnits
        q.mOpenEhrCode = mOpenEhrCode
        Return q
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
'The Original Code is QuantityConstraint.vb.
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