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
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel

Namespace ArchetypeEditor.ADL_Classes
    Public Class ADL_Tools
        Inherits ParsingTools


        Public Shared Function ProcessReference(ByVal objRef As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_INTERNAL_REF) As RmReference
            Dim rm As RmReference

            rm = New RmReference


            ' get the path - this also sets the nodeid of the leaf in ref
            ' populating the references is done at the end in case references appear before their targets
            rm.Path = objRef.target_path.to_cil
            rm.Occurrences = SetOccurrences(objRef.occurrences)

            Return rm

        End Function

        Public Shared Function SetOccurrences(ByVal cadlOccurrences As openehr.common_libs.basic.INTERVAL_INTEGER_32) As RmCardinality
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

        Public Shared Sub SetCardinality(ByVal cadlCardinality As openehr.openehr.am.archetype.constraint_model.CARDINALITY, ByVal colChildren As Children)
            If cadlCardinality Is Nothing Then Throw New ArgumentNullException("cadlCardinality")

            colChildren.Cardinality = SetOccurrences(cadlCardinality.interval)
            colChildren.Cardinality.Ordered = cadlCardinality.is_ordered
        End Sub

        'JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        Public Shared Sub SetExistence(ByVal cadlExistence As openehr.common_libs.basic.INTERVAL_INTEGER_32, ByVal colChildren As Children)
            Dim existence As New RmExistence

            If cadlExistence.upper_unbounded Then
                existence.IsUnbounded = True
            Else
                existence.MaxCount = cadlExistence.upper
            End If
            If Not cadlExistence.lower_unbounded Then
                existence.MinCount = cadlExistence.lower
            End If
            colChildren.Existence = existence
        End Sub

        Public Shared Function ProcessCodes(ByVal Constraint As openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE) As CodePhrase
            Dim cp As New CodePhrase

            For i As Integer = 1 To Constraint.code_count
                'SRH: 31 May 2008 - Check for repeats
                Dim s As String = CType(Constraint.code_list.i_th(i), EiffelKernel.STRING_8).to_cil
                If Not cp.Codes.Contains(s) Then
                    cp.Codes.Add(s)
                End If
            Next

            cp.TerminologyID = Constraint.terminology_id.value.to_cil
            Return cp
        End Function

        Public Shared Function GetConstraintFromAssertion(ByVal assert As openehr.openehr.am.archetype.assertion.ASSERTION) As String
            Select Case assert.expression.generating_type.to_cil
                Case "EXPR_BINARY_OPERATOR"
                    Dim expr As openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR = assert.expression

                    If expr.left_operand.as_string.to_cil = "archetype_id/value" Then
                        Return CType(expr.right_operand, openehr.openehr.am.archetype.assertion.EXPR_LEAF).out.to_cil.Trim("/".ToCharArray())
                    ElseIf expr.left_operand.as_string.to_cil = "concept" Then 'Obsolete
                        Return CType(expr.right_operand, openehr.openehr.am.archetype.assertion.EXPR_LEAF).out.to_cil.Trim("/".ToCharArray())
                    ElseIf expr.left_operand.as_string.to_cil = "domain_concept" Then 'Obsolete
                        Return CType(expr.right_operand, openehr.openehr.am.archetype.assertion.EXPR_LEAF).out.to_cil.Trim("/".ToCharArray())
                    End If
                Case Else
                    Debug.Assert(False)
            End Select

            Return "????"
        End Function

        Friend Shared Function GetDuration(ByVal an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE) As Duration
            Dim result As New Duration
            Dim durationObject As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT = Nothing

            If an_attribute.has_children Then
                Try
                    If TypeOf (an_attribute.children.first) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                        Dim durationAttribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                        durationAttribute = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT).attributes.i_th(1)
                        durationObject = durationAttribute.children.first
                    Else
                        'obsolete: C_PRIMITIVE_OBJECT
                        durationObject = an_attribute.children.first
                    End If
                Catch
                    Debug.Assert(False, "Error casting to width")
                    Throw New Exception("Parsing error: Duration")
                End Try
            End If

            If Not durationObject Is Nothing Then
                Dim durationConstraint As openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION = CType(durationObject.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION)

                If Not durationConstraint.interval Is Nothing Then
                    'ToDo: deal with genuine range as now max = min only
                    result.ISO_duration = CType(durationConstraint.interval.upper, openehr.common_libs.date_time.Impl.ISO8601_DURATION).as_string.to_cil
                ElseIf Not durationConstraint.pattern Is Nothing Then 'obsolete (error in previous archetypes)
                    result.ISO_duration = durationConstraint.pattern.to_cil
                End If
            End If

            Return result
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
