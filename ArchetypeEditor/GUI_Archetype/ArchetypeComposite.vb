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

Public Class ArchetypeComposite
    Inherits ArchetypeNodeAbstract
    Private mIsFixed As Boolean
    Private mCardinality As New RmCardinality(0)

    Public Property Cardinality() As RmCardinality
        Get
            Return mCardinality
        End Get
        Set(ByVal Value As RmCardinality)
            mCardinality = Value
        End Set
    End Property

    Public Property IsOrdered() As Boolean
        Get
            Return mCardinality.Ordered
        End Get
        Set(ByVal Value As Boolean)
            mCardinality.Ordered = Value
        End Set
    End Property

    Public Overrides Function Copy() As ArchetypeNode
        Return New ArchetypeComposite(Me)
    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        Dim s, s1 As String
        Dim nl As String = Chr(10) & Chr(13)

        s = (Space(3 * level) & "\ul " & mText & "\ulnone  (" & mItem.Occurrences.ToString & ")\par") & nl

        s &= (Space(3 * level) & "\i    - " & mDescription & "\i0\par") & nl
        s1 = "\cf2 Items \cf0"
        If IsOrdered Then
            s1 &= " ordered"
        End If
        If mIsFixed Then
            s1 &= " fixed"
        End If
        s1 = s1.Trim
        s1 &= "\par"
        s &= (Space(3 * (level + 1)) & s1)
        Return s
    End Function

    Public Overrides ReadOnly Property IsReference() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property HasReferences() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function ToHTML(ByVal level As Integer) As String
        Dim s As String
        Dim a_text As String = "<tr>"

        Select Case Me.mItem.Type
            Case StructureType.Cluster
                a_text &= Environment.NewLine & "<td><table><tr><td width=""" & (level * 20).ToString & """></td><td><img border=""0"" src=""Images/compound.gif"" width=""32"" height=""32"" align=""middle""><b><i>" & mText & "</i></b></td></table></td>"
                s = Filemanager.GetOpenEhrTerm(313, "Cluster")

            Case StructureType.SECTION
                a_text &= Environment.NewLine & "<td><table><tr><td width=""" & (level * 20).ToString & """></td><td><img border=""0"" src=""Images/section.gif"" width=""32"" height=""32"" align=""middle""><b><i>" & mText & "</i></b></td></table></td>"
                s = Filemanager.GetOpenEhrTerm(314, "Section")
            Case Else
                Debug.Assert(False)
                Return ""
        End Select

        a_text &= Environment.NewLine & "<td>" & mDescription & "</td>"
        a_text &= Environment.NewLine & "<td><b><i>" & s & "</b></i><br>"
        a_text &= Environment.NewLine & mItem.Occurrences.ToString

        If mIsFixed Then
            a_text &= ", fixed"
        End If
        If IsOrdered Then
            a_text &= ", ordered"
        End If
        a_text &= "</td><td>&nbsp;</td>"

        Return a_text

    End Function

    Public Sub New(ByVal aText As String, ByVal a_type As StructureType, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(aText)
        mFileManager = a_file_manager
        Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(aText)
        Me.Item = New RmStructure(aTerm.Code, a_type)
        Me.Item.Occurrences.MaxCount = 1
    End Sub

    Public Sub New(ByVal a_type As StructureType, ByVal a_file_manager As FileManagerLocal)
        ' for anonymous use of an archetype with no term
        ' called by structures when creating the cluster control which has no id
        MyBase.New("")

        mFileManager = a_file_manager
        Dim aTerm As RmTerm = New RmTerm("")
        Me.Item = New RmStructure(aTerm.Code, a_type)
        Me.Item.Occurrences.MaxCount = 1
    End Sub

    Sub New(ByVal aCluster As RmCluster, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(New RmStructure(aCluster), a_file_manager)
        mCardinality = aCluster.Children.Cardinality
        mIsFixed = aCluster.Children.Fixed
    End Sub

    Sub New(ByVal a_structure As RmStructureCompound, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(New RmStructure(a_structure), a_file_manager)
        mCardinality = a_structure.Children.Cardinality
        mIsFixed = a_structure.Children.Fixed
    End Sub

    Sub New(ByVal aSection As RmSection, ByVal a_file_manager As FileManagerLocal)
        MyBase.New(New RmStructure(aSection), a_file_manager)
        mCardinality = aSection.Children.Cardinality
        mIsFixed = aSection.Children.Fixed
    End Sub

    Sub New(ByVal aNode As ArchetypeNodeAbstract)
        MyBase.New(aNode)
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
'The Original Code is ArchetypeComposite.vb.
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
