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
Namespace ArchetypeEditor.ADL_Classes
    Public Class ADL_Tools

        ' Singleton
        Private mRmStructureCompound As RmStructureCompound

        Private Shared mInstance As ADL_Tools
        Public Shared ReadOnly Property Instance() As ADL_Tools
            Get
                If mInstance Is Nothing Then
                    mInstance = New ADL_Tools
                End If

                Return mInstance
            End Get
        End Property

        ' The limit of the processing for references
        Private mHighestLevelChildren As Children

        Public Property HighestLevelChildren() As Children
            Get
                Return mHighestLevelChildren
            End Get
            Set(ByVal Value As Children)
                mHighestLevelChildren = Value
            End Set
        End Property
        Public Property LastProcessedStructure() As RmStructureCompound
            ' may process a structure within a EventSeries - and need to save it
            ' as data...so remember it here
            Get
                Return mRmStructureCompound
            End Get
            Set(ByVal value As RmStructureCompound)
                mRmStructureCompound = value
            End Set
        End Property

        Private Function getElementForReference(ByVal nodeid As String, ByVal the_Children As Children) As RmElement
            Dim rm As RmStructure

            For Each rm In the_Children
                If rm.NodeId = nodeid Then
                    'can be multiple references and may have same node id
                    If rm.Type = StructureType.Element Then
                        Return rm
                    End If
                End If

                If TypeOf rm Is RmCluster Then
                    Dim element As RmElement
                    element = getElementForReference(nodeid, CType(rm, RmStructureCompound).Children)
                    If Not element Is Nothing Then
                        Return element
                    End If
                End If
            Next
            Return Nothing

        End Function

        Public Function ProcessReference(ByVal objRef As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF) As RmReference
            Dim rm As RmReference
            Dim nodeid As String

            rm = New RmReference

            ' get the path - this also sets the nodeid of the leaf in ref
            ' populating the references is done at the end in case references appear before their targets
            rm.Path = objRef.target_path.to_cil
            rm.Occurrences = ADL_Tools.Instance.SetOccurrences(objRef.occurrences)

            Return rm

        End Function

        Public Sub PopulateReferences(ByVal rm As RmStructureCompound)
            Dim element As RmElement
            Dim a_structure As RmStructure

            For Each a_structure In rm.Children
                Select Case a_structure.Type
                    Case StructureType.Reference
                        'If a_structure.TypeName = "Reference" Then

                        element = getElementForReference(a_structure.NodeId, mHighestLevelChildren)
                        If Not element Is Nothing Then
                            element.hasReferences = True
                            CType(a_structure, RmReference).SetElement(element)
                        Else
                            a_structure = Nothing
                        End If

                    Case StructureType.Cluster

                        If CType(a_structure, RmCluster).Children.Count > 0 Then
                            PopulateReferences(a_structure)
                        End If

                End Select
            Next

        End Sub

        Public Function SetOccurrences(ByVal cadlOccurrences As openehr.common_libs.basic.OE_INTERVAL_INT32) As RmCardinality
            Dim c As New RmCardinality

            If cadlOccurrences.upper_unbounded Then
                c.IsUnbounded = True
            Else
                c.MaxCount = cadlOccurrences.upper
            End If
            If Not cadlOccurrences.lower_unbounded Then
                c.MinCount = cadlOccurrences.lower
            End If
            Return c
        End Function

        Public Function ProcessCodes(ByVal Constraint As openehr.openehr.am.openehr_profile.data_types.text.C_CODED_TERM) As CodePhrase
            Dim s As String
            Dim cp As New CodePhrase

            For i As Integer = 1 To Constraint.code_count
                cp.Codes.Add(CType(Constraint.code_list.i_th(i), openehr.base.kernel.STRING).to_cil)
            Next

            cp.TerminologyID = Constraint.terminology_id.as_string.to_cil
            Return cp
        End Function

        Function GetDomainConceptFromAssertion(ByVal assert As openehr.openehr.am.archetype.assertion.ASSERTION) As String
            Select Case assert.expression.generating_type.to_cil
                Case "EXPR_BINARY_OPERATOR"
                    Dim expr As openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR = assert.expression
                    Debug.Assert(expr.left_operand.as_string.to_cil = "domain_concept")
                    Return CType(expr.right_operand, openehr.openehr.am.archetype.assertion.EXPR_LEAF).out.to_cil
                Case Else
                    Debug.Assert(False)
                    Return "????"
            End Select
        End Function


    End Class
End Namespace
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
'The Original Code is ADL_Tools.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
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
