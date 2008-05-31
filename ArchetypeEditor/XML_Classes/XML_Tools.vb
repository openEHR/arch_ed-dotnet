'Option Strict On
Option Explicit On

Namespace ArchetypeEditor.XML_Classes
    Public Class XML_Tools
        Inherits ParsingTools


        Public Shared Function ProcessReference(ByVal objRef As XMLParser.ARCHETYPE_INTERNAL_REF) As RmReference
            Dim rm As RmReference

            rm = New RmReference

            ' get the path - this also sets the nodeid of the leaf in ref
            ' populating the references is done at the end in case references appear before their targets
            rm.Path = objRef.target_path
            rm.Occurrences = SetOccurrences(objRef.occurrences)

            Return rm

        End Function

        Public Shared Function SetOccurrences(ByVal cadlOccurrences As XMLParser.IntervalOfInteger) As RmCardinality
            Dim c As New RmCardinality

            If Not cadlOccurrences Is Nothing Then
                'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                'If cadlOccurrences.maximum <> "" Then
                '    c.MaxCount = Integer.Parse(cadlOccurrences.maximum)
                'Else
                '    c.IsUnbounded = True
                'End If
                'If cadlOccurrences.minimum <> "" Then
                '    c.MinCount = Integer.Parse(cadlOccurrences.minimum)
                'End If
                If cadlOccurrences.upper_included Then
                    c.MaxCount = cadlOccurrences.upper
                Else
                    c.IsUnbounded = True
                End If
                If cadlOccurrences.lower_included Then
                    c.MinCount = cadlOccurrences.lower
                End If
            End If
            Return c
        End Function

        Public Shared Sub SetCardinality(ByVal cadlCardinality As XMLParser.CARDINALITY, ByVal colChildren As Children)
            colChildren.Cardinality = SetOccurrences(cadlCardinality.interval)
            colChildren.Cardinality.Ordered = cadlCardinality.is_ordered
        End Sub

        Public Shared Function ProcessCodes(ByVal Constraint As XMLParser.C_CODE_PHRASE) As CodePhrase
            Dim cp As New CodePhrase

            If Not Constraint.code_list Is Nothing AndAlso Constraint.code_list.Length > 0 Then
                'SRH: 31 May 2008 - Check for repeats
                For Each s As String In Constraint.code_list
                    If Not cp.Codes.Contains(s) Then
                        cp.Codes.Add(s)
                    End If
                Next
            End If
            'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
            'cp.TerminologyID = Constraint.terminology
            If Not Constraint.terminology_id Is Nothing Then
                cp.TerminologyID = Constraint.terminology_id.value
            End If
            Return cp
        End Function

        Public Shared Function GetConstraintFromAssertion(ByVal assert As XMLParser.ASSERTION) As String
            Try

                Select Case assert.expression.GetType.ToString.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "xmlparser.expr_binary_operator"
                        Dim expr As XMLParser.EXPR_BINARY_OPERATOR = assert.expression

                        'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                        'Dim cstring As XMLParser.C_STRING = CType(expr.right_operand, XMLParser.EXPR_LEAF).item
                        'Return cstring.pattern.Trim("/".ToCharArray())

                        Dim expressionLeaf As XMLParser.EXPR_LEAF
                        expressionLeaf = CType(expr.right_operand, XMLParser.EXPR_LEAF)

                        If expressionLeaf.item.GetType().Name.ToLower = "string" Then
                            Return expressionLeaf.item.ToString.Trim("/".ToCharArray())
                        Else
                            Dim cstring As XMLParser.C_STRING = expressionLeaf.item
                            Return cstring.pattern.Trim("/".ToCharArray())
                        End If

                    Case Else
                        Debug.Assert(False)
                        Return "????"
                End Select

            Catch ex As Exception
                Debug.Assert(True)
                Return "????"
            End Try
        End Function

        Friend Shared Function GetDuration(ByVal an_attribute As XMLParser.C_ATTRIBUTE) As Duration
            Dim result As New Duration
            Dim durationObject As XMLParser.C_PRIMITIVE_OBJECT
            Try
                If TypeOf (an_attribute.children(0)) Is XMLParser.C_COMPLEX_OBJECT Then
                    Dim durationAttribute As XMLParser.C_ATTRIBUTE
                    durationAttribute = CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT).attributes(0)
                    durationObject = durationAttribute.children(0)
                Else
                    'obsolete: C_PRIMITIVE_OBJECT
                    durationObject = an_attribute.children(0)
                End If
            Catch
                Debug.Assert(False, "Error casting to duration")
                Throw New Exception("Parsing error: Duration")
            End Try

            If Not durationObject Is Nothing Then
                Dim durationConstraint As XMLParser.C_DURATION = _
                    CType(durationObject.item, XMLParser.C_DURATION)
                If Not durationConstraint.range Is Nothing Then
                    'ToDo: deal with genuine range as now max = min only
                    result.ISO_duration = durationConstraint.range.upper 'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                ElseIf Not durationConstraint.pattern Is Nothing Then 'obsolete (error in previous archetypes)
                    result.ISO_duration = durationConstraint.pattern
                End If
            End If
            Return result
        End Function

    End Class
End Namespace
