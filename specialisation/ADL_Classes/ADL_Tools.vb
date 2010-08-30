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
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel

Namespace ArchetypeEditor.ADL_Classes
    Public Class ADL_Tools
        Inherits ParsingTools

        Public Shared Function ProcessReference(ByVal objRef As AdlParser.ArchetypeInternalRef) As RmReference
            Dim result As RmReference = New RmReference

            ' get the path - this also sets the nodeid of the leaf in ref
            ' populating the references is done at the end in case references appear before their targets
            result.Path = objRef.TargetPath.ToCil
            result.Occurrences = NewOccurrences(objRef.Occurrences)
            Return result
        End Function

        Public Shared Function NewOccurrences(ByVal cadlOccurrences As AdlParser.IntervalInteger_32) As RmCardinality
            Dim result As New RmCardinality

            If cadlOccurrences.UpperUnbounded Then
                result.IsUnbounded = True
            Else
                result.MaxCount = cadlOccurrences.Upper
            End If

            If Not cadlOccurrences.LowerUnbounded Then
                result.MinCount = cadlOccurrences.Lower
            End If

            Return result
        End Function

        Public Shared Sub SetCardinality(ByVal cadlCardinality As AdlParser.Cardinality, ByVal colChildren As Children)
            If cadlCardinality Is Nothing Then Throw New ArgumentNullException("cadlCardinality")

            colChildren.Cardinality = NewOccurrences(cadlCardinality.Interval)
            colChildren.Cardinality.Ordered = cadlCardinality.IsOrdered
        End Sub

        Public Shared Sub SetExistence(ByVal cadlExistence As AdlParser.IntervalInteger_32, ByVal colChildren As Children)
            Dim existence As New RmExistence

            If cadlExistence.UpperUnbounded Then
                existence.IsUnbounded = True
            Else
                existence.MaxCount = cadlExistence.Upper
            End If

            If Not cadlExistence.LowerUnbounded Then
                existence.MinCount = cadlExistence.Lower
            End If

            colChildren.Existence = existence
        End Sub

        Public Shared Function ProcessCodes(ByVal Constraint As AdlParser.CCodePhrase) As CodePhrase
            Dim result As New CodePhrase

            For i As Integer = 1 To Constraint.CodeCount
                Dim s As String = CType(Constraint.CodeList.ITh(i), EiffelKernel.String_8).ToCil

                If Not result.Codes.Contains(s) Then
                    result.Codes.Add(s)
                End If
            Next

            result.TerminologyID = Constraint.TerminologyId.Value.ToCil
            Return result
        End Function

        Public Shared Function GetConstraintFromAssertion(ByVal assert As AdlParser.Assertion) As String
            Select Case assert.Expression.GeneratingType.Out.ToCil
                Case "EXPR_BINARY_OPERATOR"
                    Dim expr As AdlParser.ExprBinaryOperator = assert.Expression

                    If expr.LeftOperand.AsString.ToCil = "archetype_id/value" Then
                        Return CType(expr.RightOperand, AdlParser.ExprLeaf).Out.ToCil.Trim("/".ToCharArray())
                    ElseIf expr.LeftOperand.AsString.ToCil = "concept" Then 'Obsolete
                        Return CType(expr.RightOperand, AdlParser.ExprLeaf).Out.ToCil.Trim("/".ToCharArray())
                    ElseIf expr.LeftOperand.AsString.ToCil = "domain_concept" Then 'Obsolete
                        Return CType(expr.RightOperand, AdlParser.ExprLeaf).Out.ToCil.Trim("/".ToCharArray())
                    End If
                Case Else
                    Debug.Assert(False)
            End Select

            Return "????"
        End Function

        Friend Shared Function GetDuration(ByVal an_attribute As AdlParser.CAttribute) As Duration
            Dim result As New Duration
            Dim durationObject As AdlParser.CPrimitiveObject = Nothing

            If an_attribute.HasChildren Then
                Try
                    If TypeOf (an_attribute.Children.First) Is AdlParser.CComplexObject Then
                        Dim durationAttribute As AdlParser.CAttribute
                        durationAttribute = CType(an_attribute.Children.First, AdlParser.CComplexObject).Attributes.ITh(1)
                        durationObject = durationAttribute.Children.First
                    Else
                        'obsolete: C_PRIMITIVE_OBJECT
                        durationObject = an_attribute.Children.First
                    End If
                Catch
                    Debug.Assert(False, "Error casting to width")
                    Throw New Exception("Parsing error: Duration")
                End Try
            End If

            If Not durationObject Is Nothing Then
                Dim durationConstraint As AdlParser.CDuration = CType(durationObject.Item, AdlParser.CDuration)

                If Not durationConstraint.Range Is Nothing Then
                    'ToDo: deal with genuine range as now max = min only
                    result.ISO_duration = CType(durationConstraint.Range.Upper, AdlParser.Iso8601Duration).AsString.ToCil
                ElseIf Not durationConstraint.Pattern Is Nothing Then 'obsolete (error in previous archetypes)
                    result.ISO_duration = durationConstraint.Pattern.ToCil
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
