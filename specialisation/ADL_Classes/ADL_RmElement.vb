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
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/BusinessLogic/EHR_Classes/RmElement.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Strict On
Imports EiffelKernel = EiffelSoftware.Library.Base.Kernel
Imports EiffelList = EiffelSoftware.Library.Base.Structures.List
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes

    Public Class ADL_RmElement
        Inherits RmElement

        Sub New(ByVal EIF_Element As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
            MyBase.New(EIF_Element)
            ProcessElement(EIF_Element, a_filemanager)
        End Sub

        Private Sub ProcessElement(ByVal ComplexObj As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal)
            Try
                Dim v As EiffelKernel.String_8 = Eiffel.String("value")
                cConstraint = New Constraint

                If Not ComplexObj.AnyAllowed Then
                    Dim attribute As AdlParser.CAttribute
                    Dim i As Integer

                    If ComplexObj.HasAttribute(v) Then
                        attribute = ComplexObj.CAttributeAtPath(v)

                        If attribute.Children.Count > 1 Then
                            ' multiple constraints - not dealt with yet in the GUI
                            Dim m_c As New Constraint_Choice

                            For i = 1 To attribute.Children.Count
                                m_c.Constraints.Add(ProcessValue(CType(attribute.Children.ITh(i), AdlParser.CObject), a_filemanager))
                            Next

                            cConstraint = m_c
                        ElseIf attribute.HasChildren Then
                            cConstraint = ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager)
                        End If
                    End If

                    Dim hasNullFlavour As Boolean = False
                    v = Eiffel.String("null_flavor") ' Obsolete
                    hasNullFlavour = ComplexObj.HasAttribute(v)

                    If Not hasNullFlavour Then
                        v = Eiffel.String("null_flavour") ' redundant
                        hasNullFlavour = ComplexObj.HasAttribute(v)
                    End If

                    If hasNullFlavour Then
                        attribute = ComplexObj.CAttributeAtPath(v)

                        If attribute.Children.Count = 1 Then
                            Dim c As Constraint_Text = ProcessText(CType(attribute.Children.First, AdlParser.CComplexObject))

                            If Not c Is Nothing AndAlso c.AllowableValues.Codes.Count > 0 Then
                                ConstrainedNullFlavours = c.AllowableValues
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ComplexObj.NodeId.ToCil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function ProcessInterval(ByVal ObjNode As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal) As Constraint_Interval
            Dim attribute As AdlParser.CAttribute

            Debug.Assert(Not ObjNode.AnyAllowed)

            Select Case ObjNode.RmTypeName.ToCil.ToLowerInvariant()
                Case "interval_count"
                    Dim cic As New Constraint_Interval_Count
                    Dim countLimits As Constraint_Count

                    attribute = CType(ObjNode.Attributes.First, AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        countLimits = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_Count)

                        If countLimits.HasMaximum Then
                            CType(cic.UpperLimit, Constraint_Count).HasMaximum = True
                            CType(cic.UpperLimit, Constraint_Count).MaximumValue = countLimits.MaximumValue
                        End If

                        If countLimits.HasMinimum Then
                            CType(cic.LowerLimit, Constraint_Count).HasMinimum = True
                            CType(cic.LowerLimit, Constraint_Count).MinimumValue = countLimits.MinimumValue
                        End If
                    End If

                    Return cic
                Case "dv_interval<dv_count>", "dv_interval<count>", "interval<count>"
                    Dim cic As New Constraint_Interval_Count

                    Try
                        ' Get the upper value
                        If ObjNode.HasAttribute(Eiffel.String("upper")) Then
                            attribute = ObjNode.CAttributeAtPath(Eiffel.String("upper"))

                            If attribute.HasChildren Then
                                cic.UpperLimit = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_Count)
                            End If
                        End If

                        ' Get the lower value
                        If ObjNode.HasAttribute(Eiffel.String("lower")) Then
                            attribute = ObjNode.CAttributeAtPath(Eiffel.String("lower"))

                            If attribute.HasChildren Then
                                cic.LowerLimit = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_Count)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.NodeId.ToCil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Return cic
                Case "interval_quantity"
                    Dim ciq As New Constraint_Interval_Quantity
                    Dim quantLimits As New Constraint_Quantity

                    attribute = CType(ObjNode.Attributes.First, AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        quantLimits = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_Quantity)
                    End If

                    If quantLimits.has_units Then
                        ciq.AddUnit(CType(quantLimits.Units(0), Constraint_QuantityUnit))
                        CType(CType(ciq.UpperLimit, Constraint_Quantity).Units(0), Constraint_QuantityUnit).HasMinimum = False
                        CType(CType(ciq.LowerLimit, Constraint_Quantity).Units(0), Constraint_QuantityUnit).HasMaximum = False
                    End If

                    ciq.QuantityPropertyCode = quantLimits.OpenEhrCode
                    Return ciq
                Case "dv_interval<dv_quantity>", "dv_interval<quantity>", "interval<quantity>"
                    Dim ciq As New Constraint_Interval_Quantity

                    Try
                        ' Get the upper value
                        If ObjNode.HasAttribute(Eiffel.String("upper")) Then
                            attribute = ObjNode.CAttributeAtPath(Eiffel.String("upper"))

                            If attribute.HasChildren Then
                                ciq.UpperLimit = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_Quantity)
                            End If
                        End If

                        ' Get the lower value
                        If ObjNode.HasAttribute(Eiffel.String("lower")) Then
                            attribute = ObjNode.CAttributeAtPath(Eiffel.String("lower"))

                            If attribute.HasChildren Then
                                ciq.LowerLimit = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_Quantity)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.NodeId.ToCil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Return ciq
                Case "dv_interval<dv_date_time>", "dv_interval<date_time>", "interval<date_time>", "dv_interval<dv_date>", "dv_interval<dv_time>"
                    Dim cidt As New Constraint_Interval_DateTime

                    Try
                        ' Get the upper value
                        If ObjNode.HasAttribute(Eiffel.String("upper")) Then
                            attribute = ObjNode.CAttributeAtPath(Eiffel.String("upper"))

                            If attribute.HasChildren Then
                                cidt.UpperLimit = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_DateTime)
                            End If
                        End If

                        ' Get the lower value
                        If ObjNode.HasAttribute(Eiffel.String("lower")) Then
                            attribute = ObjNode.CAttributeAtPath(Eiffel.String("lower"))

                            If attribute.HasChildren Then
                                cidt.LowerLimit = CType(ProcessValue(CType(attribute.Children.First, AdlParser.CObject), a_filemanager), Constraint_DateTime)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.NodeId.ToCil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Return cidt
                Case Else
                    Debug.Assert(False, String.Format("Attribute not handled: {0}", ObjNode.RmTypeName.ToCil))
                    Return Nothing
            End Select
        End Function

        Private Function ProcessDuration(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_Duration
            If ObjNode.AnyAllowed Then
                Return New Constraint_Duration
            Else
                Dim attribute As AdlParser.CAttribute
                Dim durationConstraint As New Constraint_Duration

                If ObjNode.Attributes.Count > 0 Then
                    attribute = CType(ObjNode.Attributes.First, AdlParser.CAttribute)
                    Dim constraint As AdlParser.CPrimitiveObject

                    If attribute.HasChildren Then
                        constraint = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                        durationConstraint = ProcessDuration(constraint, durationConstraint)
                    End If

                    Return durationConstraint
                End If
            End If

            Debug.Assert(False, "Error processing Duration")
            Return New Constraint_Duration
        End Function

        Private Function ProcessDuration(ByVal ObjNode As AdlParser.CPrimitiveObject, ByVal duration As Constraint_Duration) As Constraint_Duration
            If ObjNode.AnyAllowed Then
                Return duration
            End If

            Dim cadlC As AdlParser.CDuration
            cadlC = CType(ObjNode.Item, AdlParser.CDuration)

            If Not cadlC.pattern Is Nothing Then
                duration.AllowableUnits = cadlC.pattern.ToCil
            End If

            If Not cadlC.Range Is Nothing Then
                If cadlC.Range.UpperUnbounded Then
                    duration.HasMaximum = False
                Else
                    Dim upperDuration As AdlParser.Iso8601Duration
                    upperDuration = CType(cadlC.Range.Upper, AdlParser.Iso8601Duration)

                    Dim units As String = upperDuration.Value.ToCil
                    units = units.ToUpperInvariant.Substring(units.Length - 1)

                    Select Case units
                        Case "S"
                            duration.MaximumValue = upperDuration.Seconds
                            units = "TS"
                        Case "M"
                            If upperDuration.Value.ToCil.ToLowerInvariant.Contains("t") Then
                                'Minutes
                                duration.MaximumValue = upperDuration.Minutes
                                units = "TM"
                            Else
                                'Months
                                duration.MaximumValue = upperDuration.Months
                            End If
                        Case "H"
                            duration.MaximumValue = upperDuration.Hours
                            units = "TH"
                        Case "D"
                            duration.MaximumValue = upperDuration.Days
                        Case "Y"
                            duration.MaximumValue = upperDuration.Years
                        Case "W"
                            duration.MaximumValue = upperDuration.Weeks
                    End Select

                    duration.MinMaxValueUnits = units
                    duration.HasMaximum = True
                    duration.IncludeMaximum = cadlC.Range.UpperIncluded
                End If

                If cadlC.Range.LowerUnbounded Then
                    duration.HasMinimum = False
                Else
                    Dim lowerDuration As AdlParser.Iso8601Duration
                    lowerDuration = CType(cadlC.Range.Lower, AdlParser.Iso8601Duration)

                    Dim units As String = lowerDuration.Value.ToCil
                    units = units.ToUpperInvariant.Substring(units.Length - 1)

                    Select Case units.ToUpperInvariant
                        Case "S"
                            duration.MinimumValue = lowerDuration.Seconds
                            units = "TS"
                        Case "M"
                            If lowerDuration.Value.ToCil.ToLowerInvariant.Contains("t") Then
                                'Minutes
                                duration.MinimumValue = lowerDuration.Minutes
                                units = "TM"
                            Else
                                'Months
                                duration.MinimumValue = lowerDuration.Months
                            End If
                        Case "H"
                            duration.MinimumValue = lowerDuration.Hours
                            units = "TH"
                        Case "D"
                            duration.MinimumValue = lowerDuration.Days
                        Case "Y"
                            duration.MinimumValue = lowerDuration.Years
                        Case "W"
                            duration.MinimumValue = lowerDuration.Weeks
                    End Select
                    If duration.MinMaxValueUnits = String.Empty Then
                        duration.MinMaxValueUnits = units
                    End If
                    duration.HasMinimum = True
                    duration.IncludeMinimum = cadlC.Range.LowerIncluded
                End If
            End If

            Return duration
        End Function

        Private Function ProcessMultiMedia(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_MultiMedia
            Dim result As New Constraint_MultiMedia

            If Not ObjNode.AnyAllowed Then
                Try
                    Dim mediaType As AdlParser.CAttribute = CType(ObjNode.Attributes.First, AdlParser.CAttribute)

                    If mediaType.HasChildren Then
                        Dim Obj As AdlParser.CObject = CType(mediaType.Children.First, AdlParser.CObject)
                        result.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessCodes(CType(Obj, AdlParser.CCodePhrase))
                    End If
                Catch ex As Exception
                    Debug.Assert(False)
                    MessageBox.Show(AE_Constants.Instance.Error_loading & " Multimedia constraint:" & ObjNode.NodeId.ToCil & _
                        " - " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Return result
        End Function

        Private Function ProcessValue(ByVal ObjNode As AdlParser.CObject, ByVal a_filemanager As FileManagerLocal) As Constraint

            Select Case ObjNode.RmTypeName.ToCil.ToLowerInvariant()
                Case "dv_quantity", "quantity"
                    Return ProcessQuantity(CType(ObjNode, AdlParser.CDvQuantity))
                Case "dv_coded_text", "dv_text", "coded_text", "text"
                    Return ProcessText(CType(ObjNode, AdlParser.CComplexObject))
                Case "dv_boolean", "boolean"
                    If TypeOf (ObjNode) Is AdlParser.CComplexObject Then
                        Return ProcessBoolean(CType(ObjNode, AdlParser.CComplexObject))
                    Else
                        'obsolete
                        Return ProcessBoolean(CType(ObjNode, AdlParser.CPrimitiveObject))
                    End If
                Case "dv_ordinal", "ordinal"
                    If TypeOf (ObjNode) Is AdlParser.CDvOrdinal Then
                        Return ProcessOrdinal(CType(ObjNode, AdlParser.CDvOrdinal), a_filemanager)
                    Else
                        'redundant
                        Return ProcessOrdinal(CType(ObjNode, AdlParser.CComplexObject), a_filemanager)
                    End If
                Case "dv_date_time", "dv_date", "dv_time", "datetime", "date_time", "date", "time", "_c_date"
                    If TypeOf (ObjNode) Is AdlParser.CComplexObject Then
                        Return ProcessDateTime(CType(ObjNode, AdlParser.CComplexObject))
                    Else
                        'obsolete
                        Return ProcessDateTime(CType(ObjNode, AdlParser.CPrimitiveObject))
                    End If
                Case "quantity_ratio" ' OBSOLETE
                    Return ProcessRatio(CType(ObjNode, AdlParser.CComplexObject))
                Case "dv_proportion", "proportion"
                    Return ProcessProportion(CType(ObjNode, AdlParser.CComplexObject))
                Case "dv_count", "count"
                    Return ProcessCount(CType(ObjNode, AdlParser.CComplexObject))
                Case "interval_count", "interval_quantity" 'OBSOLETE
                    Return ProcessInterval(CType(ObjNode, AdlParser.CComplexObject), a_filemanager)
                Case "dv_interval<dv_count>", "dv_interval<dv_quantity>", "dv_interval<dv_date_time>", "dv_interval<count>", "dv_interval<quantity>", "dv_interval<date_time>", "interval<count>", "interval<quantity>", "interval<date_time>", "dv_interval<dv_date>", "dv_interval<dv_time>"
                    Return ProcessInterval(CType(ObjNode, AdlParser.CComplexObject), a_filemanager)
                Case "dv_multimedia", "multimedia", "multi_media"
                    Return ProcessMultiMedia(CType(ObjNode, AdlParser.CComplexObject))
                Case "dv_uri", "uri", "dv_ehr_uri"
                    Return ProcessUri(CType(ObjNode, AdlParser.CComplexObject))
                Case "dv_identifier"
                    Return ProcessIdentifier(CType(ObjNode, AdlParser.CComplexObject))
                    'Case "dv_currency"
                    'Return ProcessCurrency(CType(ObjNode, AdlParser.CComplexObject))
                Case "dv_duration", "duration"
                    If TypeOf ObjNode Is AdlParser.CComplexObject Then
                        Return ProcessDuration(CType(ObjNode, AdlParser.CComplexObject))
                    Else
                        'obsolete
                        Dim constraintDuration As New Constraint_Duration
                        constraintDuration = ProcessDuration(CType(ObjNode, AdlParser.CPrimitiveObject), constraintDuration)
                        Return constraintDuration
                    End If
                Case "dv_parsable"
                    Return ProcessParsable(CType(ObjNode, AdlParser.CComplexObject))
                Case Else
                    Debug.Assert(False, String.Format("Attribute not handled: {0}", ObjNode.RmTypeName.ToCil))
                    Return New Constraint
            End Select
        End Function

        Shared Function ProcessParsable(ByVal dvParse As AdlParser.CComplexObject) As Constraint
            Dim result As New Constraint_Parsable
            Dim attribute As AdlParser.CAttribute

            Try
                For i As Integer = 1 To dvParse.Attributes.Count
                    attribute = CType(dvParse.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Dim cadlOS As AdlParser.CPrimitiveObject = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                        Dim cadlC As AdlParser.CString = CType(cadlOS.Item, AdlParser.CString)

                        Select Case attribute.RmAttributeName.ToCil.ToLowerInvariant()
                            Case "value"
                                'OBSOLETE

                            Case "formalism"
                                Try
                                    For ii As Integer = 1 To cadlC.Strings.Count
                                        result.AllowableFormalisms.Add(CType(cadlC.Strings.ITh(ii), EiffelKernel.String_8).ToCil)
                                    Next
                                Catch ex As Exception
                                    Debug.Assert(False)
                                    MessageBox.Show(AE_Constants.Instance.Error_loading & " Multimedia constraint:" & dvParse.NodeId.ToCil & _
                                        " - " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End Try
                        End Select
                    End If
                Next
            Catch e As Exception
                MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Return result
        End Function

        Shared Function ProcessUri(ByVal dvUri As AdlParser.CComplexObject) As Constraint
            Dim result As New Constraint_URI
            Dim attribute As AdlParser.CAttribute

            If dvUri.RmTypeName.ToCil.ToLowerInvariant = "dv_ehr_uri" Then
                result.EhrUriOnly = True
            End If

            If Not dvUri.AnyAllowed Then
                Try
                    attribute = CType(dvUri.Attributes.First, AdlParser.CAttribute)
                    Debug.Assert(attribute.RmAttributeName.ToCil.ToLowerInvariant = "value")

                    If attribute.HasChildren Then
                        Dim cadlOS As AdlParser.CPrimitiveObject = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                        Dim cadlC As AdlParser.CString = CType(cadlOS.Item, AdlParser.CString)
                        result.RegularExpression = cadlC.Regexp.ToCil
                    End If
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Return result
        End Function

        Shared Function ProcessIdentifier(ByVal dvIdentifier As AdlParser.CComplexObject) As Constraint
            Dim result As New Constraint_Identifier
            Dim attribute As AdlParser.CAttribute = Nothing

            If Not dvIdentifier.AnyAllowed Then
                Try
                    For i As Integer = 1 To dvIdentifier.Attributes.Count
                        attribute = CType(dvIdentifier.Attributes.ITh(i), AdlParser.CAttribute)

                        If Not attribute Is Nothing AndAlso attribute.HasChildren Then
                            Dim cadlOS As AdlParser.CPrimitiveObject = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                            Dim cadlC As AdlParser.CString = CType(cadlOS.Item, AdlParser.CString)

                            Select Case attribute.RmAttributeName.ToCil.ToLowerInvariant()
                                Case "issuer"
                                    result.IssuerRegex = cadlC.Regexp.ToCil
                                Case "type"
                                    result.TypeRegex = cadlC.Regexp.ToCil
                                Case "id"
                                    result.IDRegex = cadlC.Regexp.ToCil
                            End Select
                        End If
                    Next
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Return result
        End Function

        Private Function ProcessCount(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_Count
            Dim result As New Constraint_Count
            Dim i As Integer

            If Not ObjNode.AnyAllowed Then
                For i = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Select Case attribute.RmAttributeName.ToCil.ToLowerInvariant
                            Case "value", "magnitude"
                                Dim cadlOS As AdlParser.CPrimitiveObject = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                                Dim cadlC As AdlParser.CInteger = CType(cadlOS.Item, AdlParser.CInteger)

                                If Not cadlC.Range.LowerUnbounded Then
                                    result.HasMinimum = True
                                    result.MinimumValue = cadlC.Range.Lower
                                    result.IncludeMinimum = cadlC.Range.LowerIncluded
                                Else
                                    result.HasMinimum = False
                                End If

                                If Not cadlC.Range.UpperUnbounded Then
                                    result.HasMaximum = True
                                    result.MaximumValue = cadlC.Range.Upper
                                    result.IncludeMaximum = cadlC.Range.UpperIncluded
                                Else
                                    result.HasMaximum = False
                                End If

                                If cadlC.HasAssumedValue Then
                                    result.HasAssumedValue = True
                                    result.AssumedValue = CType(cadlC.AssumedValue, EiffelKernel.Integer_32).Item
                                End If
                            Case Else
                                Debug.Assert(False)
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessReal(ByVal ObjNode As AdlParser.CPrimitiveObject) As Constraint_Real
            Dim ct As New Constraint_Real
            Dim cadlC As AdlParser.CReal = CType(ObjNode.Item, AdlParser.CReal)

            If Not cadlC.Range.LowerUnbounded Then
                ct.HasMinimum = True
                ct.MinimumRealValue = cadlC.Range.Lower
                ct.IncludeMinimum = cadlC.Range.LowerIncluded
            Else
                ct.HasMinimum = False
            End If
            If Not cadlC.Range.UpperUnbounded Then
                ct.HasMaximum = True
                ct.MaximumRealValue = cadlC.Range.Upper
                ct.IncludeMaximum = cadlC.Range.UpperIncluded
            Else
                ct.HasMaximum = False
            End If
            If cadlC.HasAssumedValue Then
                ct.HasAssumedValue = True
                ct.AssumedValue = cadlC.AssumedValue
            End If

            Return ct
        End Function

        'OBSOLETE
        Private Function ProcessRatio(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_Proportion
            Dim result As New Constraint_Proportion

            If Not ObjNode.AnyAllowed Then
                For i As Integer = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Select Case attribute.RmAttributeName.ToCil
                            Case "numerator"
                                result.Numerator = New Constraint_Real(ProcessCount(CType(attribute.Children.First, AdlParser.CComplexObject)))
                            Case "denominator"
                                result.Denominator = New Constraint_Real(ProcessCount(CType(attribute.Children.First, AdlParser.CComplexObject)))
                            Case Else
                                Debug.Assert(False)
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessProportion(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_Proportion
            Dim result As New Constraint_Proportion

            If Not ObjNode.AnyAllowed Then
                For i As Integer = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Select Case attribute.RmAttributeName.ToCil
                            Case "numerator"
                                result.Numerator = ProcessReal(CType(attribute.Children.First, AdlParser.CPrimitiveObject))
                            Case "denominator"
                                result.Denominator = ProcessReal(CType(attribute.Children.First, AdlParser.CPrimitiveObject))
                            Case "is_integral"
                                Dim bool As Constraint_Boolean = ProcessBoolean(CType(attribute.Children.First, AdlParser.CPrimitiveObject))
                                result.IsIntegral = bool.TrueAllowed
                                result.IsIntegralSet = True
                            Case "type"
                                Dim cadlOS As AdlParser.CPrimitiveObject = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                                Dim cadlC As AdlParser.CInteger = CType(cadlOS.Item, AdlParser.CInteger)
                                result.SetAllTypesDisallowed()

                                If Not cadlC.List Is Nothing Then
                                    For ii As Integer = 1 To cadlC.List.Count
                                        result.AllowType(cadlC.List.ITh(ii))
                                    Next
                                ElseIf Not cadlC.Range Is Nothing Then
                                    'is an interval as only one allowed
                                    result.AllowType(cadlC.Range.Upper)
                                Else
                                    result.SetAllTypesAllowed()
                                End If
                            Case Else
                                Debug.Assert(False)
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessDateTime(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_DateTime
            Dim result As New Constraint_DateTime()

            If ObjNode.AnyAllowed Then
                Select Case ObjNode.RmTypeName.ToCil.ToUpperInvariant
                    Case "DV_DATE_TIME"
                        result.TypeofDateTimeConstraint = 11
                    Case "DV_DATE"
                        result.TypeofDateTimeConstraint = 14
                    Case "DV_TIME"
                        result.TypeofDateTimeConstraint = 18
                End Select
            Else
                For i As Integer = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Select Case attribute.RmAttributeName.ToCil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                            Case "value"
                                result = ProcessDateTime(CType(attribute.Children.First, AdlParser.CPrimitiveObject))
                                Exit For
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessDateTime(ByVal ObjNode As AdlParser.CPrimitiveObject) As Constraint_DateTime
            Dim result As New Constraint_DateTime
            Dim s As String

            Select Case ObjNode.RmTypeName.ToCil.ToUpperInvariant
                Case "DATE_TIME"
                    If ObjNode.AnyAllowed Then
                        s = "yyyy-??-??t??:??:??"
                    Else
                        s = CType(ObjNode.Item, AdlParser.CDateTime).Pattern.ToCil
                    End If
                Case "DATE"
                    If ObjNode.AnyAllowed Then
                        s = "yyyy-??-??"
                    Else
                        s = CType(ObjNode.Item, AdlParser.CDate).Pattern.ToCil
                    End If
                Case "TIME"
                    If ObjNode.AnyAllowed Then
                        s = "thh:??:??"
                    Else
                        s = CType(ObjNode.Item, AdlParser.CTime).Pattern.ToCil
                    End If
                Case Else
                    Debug.Assert(False)
                    s = ""
            End Select

            Select Case s.ToLowerInvariant()
                Case "yyyy-??-?? ??:??:??", "yyyy-??-??t??:??:??"
                    ' Allow all
                    result.TypeofDateTimeConstraint = 11
                Case "yyyy-mm-dd hh:mm:ss", "yyyy-mm-ddthh:mm:ss"
                    result.TypeofDateTimeConstraint = 12
                Case "yyyy-mm-dd hh:??:??", "yyyy-mm-ddthh:??:??"
                    'Partial Date time
                    result.TypeofDateTimeConstraint = 13
                Case "yyyy-??-??"
                    'Date only
                    result.TypeofDateTimeConstraint = 14
                Case "yyyy-mm-dd"
                    'Full date
                    result.TypeofDateTimeConstraint = 15
                Case "yyyy-??-xx"
                    'Partial date
                    result.TypeofDateTimeConstraint = 16
                Case "yyyy-mm-??"
                    'Partial date with month
                    result.TypeofDateTimeConstraint = 17
                Case "hh:??:??", "thh:??:??"
                    'TimeOnly
                    result.TypeofDateTimeConstraint = 18
                Case "hh:mm:ss", "thh:mm:ss"
                    'Full time
                    result.TypeofDateTimeConstraint = 19
                Case "hh:??:xx", "thh:??:xx"
                    'Partial time
                    result.TypeofDateTimeConstraint = 20
                Case "hh:mm:??", "thh:mm:??"
                    'Partial time with minutes
                    result.TypeofDateTimeConstraint = 21
            End Select
            'End If

            Return result
        End Function

        Private Function ProcessOrdinal(ByVal an_ordinal_constraint As AdlParser.CDvOrdinal, ByVal a_filemanager As FileManagerLocal) As Constraint_Ordinal
            Dim ord As New Constraint_Ordinal(a_filemanager)
            Dim Ordinals As EiffelList.LinkedListReference
            Dim c_phrase As New CodePhrase
            Dim openehr_ordinal As AdlParser.Ordinal

            '' first value may have a "?" instead of a code as holder for empty ordinal
            If Not an_ordinal_constraint.AnyAllowed Then
                Ordinals = an_ordinal_constraint.Items()

                Ordinals.Start()

                Do While Not Ordinals.Off
                    openehr_ordinal = CType(Ordinals.Active.Item, AdlParser.Ordinal)

                    c_phrase.Phrase = openehr_ordinal.Symbol.AsString.ToCil

                    If c_phrase.TerminologyID.ToLowerInvariant = "local" Then
                        If Array.IndexOf(ord.InternalCodes, c_phrase.FirstCode) = -1 Then
                            Dim newOrdinal As OrdinalValue = ord.OrdinalValues.NewOrdinal
                            newOrdinal.InternalCode = c_phrase.FirstCode
                            newOrdinal.Ordinal = openehr_ordinal.Value
                            ord.OrdinalValues.Add(newOrdinal)
                        End If
                    Else
                        Beep()
                        Debug.Assert(False)
                    End If

                    Ordinals.Forth()
                Loop

                If an_ordinal_constraint.HasAssumedValue Then
                    ord.HasAssumedValue = True
                    Dim ordinal As AdlParser.Ordinal = CType(an_ordinal_constraint.AssumedValue, AdlParser.Ordinal)
                    ord.AssumedValue = ordinal.Value

                    If Not ordinal.Symbol Is Nothing Then
                        If Not ordinal.Symbol.TerminologyId Is Nothing Then ord.AssumedValue_TerminologyId = ordinal.Symbol.TerminologyId.Value.ToCil
                        If Not ordinal.Symbol.CodeString Is Nothing Then ord.AssumedValue_CodeString = ordinal.Symbol.CodeString.ToCil
                    End If
                End If
            End If

            Return ord

        End Function

        'OBSOLETE
        Private Function ProcessOrdinal(ByVal ObjNode As AdlParser.CComplexObject, ByVal a_filemanager As FileManagerLocal) As Constraint_Ordinal
            Dim i As Integer

            If Not ObjNode.AnyAllowed Then
                For i = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Select Case attribute.RmAttributeName.ToCil.ToLowerInvariant()
                            Case "value"
                                Return ProcessOrdinal(CType(attribute.Children.First, AdlParser.CDvOrdinal), a_filemanager)
                            Case Else
                                Debug.Assert(False, "attribute not handled")
                        End Select
                    End If
                Next
            End If

            Return New Constraint_Ordinal(True, a_filemanager)
        End Function

        Private Function ProcessBoolean(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_Boolean
            If ObjNode.AnyAllowed Then
                Return New Constraint_Boolean
            Else
                For i As Integer = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    If attribute.HasChildren Then
                        Select Case attribute.RmAttributeName.ToCil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                            Case "value"
                                Return ProcessBoolean(CType(attribute.Children.First, AdlParser.CPrimitiveObject))
                        End Select
                    End If
                Next
            End If

            'Shouldn't get to here
            Debug.Assert(False, "Error processing boolean")
            Return New Constraint_Boolean
        End Function

        Private Function ProcessBoolean(ByVal ObjSimple As AdlParser.CPrimitiveObject) As Constraint_Boolean
            Dim b As New Constraint_Boolean
            Dim i As Integer
            Dim bool As AdlParser.CBoolean = CType(ObjSimple.Item, AdlParser.CBoolean)

            If bool.TrueValid Then
                i = 1
            End If
            If bool.FalseValid Then
                i += 2
            End If
            Select Case i
                Case 1
                    b.TrueAllowed = True
                Case 2
                    b.FalseAllowed = False
                Case 3
                    b.TrueFalseAllowed = False
            End Select

            If bool.HasAssumedValue() Then
                b.hasAssumedValue = True
                b.AssumedValue = CType(bool.AssumedValue, EiffelKernel.BooleanRef).Item
            End If

            Return b
        End Function

        Shared Function ProcessText(ByVal ObjNode As AdlParser.CComplexObject) As Constraint_Text
            Dim result As New Constraint_Text
            Dim i As Integer

            If ObjNode.AnyAllowed Then
                result.TypeOfTextConstraint = TextConstrainType.Text
            Else
                For i = 1 To ObjNode.Attributes.Count
                    Dim attribute As AdlParser.CAttribute = CType(ObjNode.Attributes.ITh(i), AdlParser.CAttribute)

                    Select Case attribute.RmAttributeName.ToCil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "defining_code", "code" 'code is redundant
                            'coded - could be a constraint or the actual codes so..
                            If attribute.HasChildren Then
                                Dim Obj As AdlParser.CObject = CType(attribute.Children.First, AdlParser.CObject)

                                Select Case Obj.GeneratingType.Out.ToCil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
                                    Case "CONSTRAINTREF"
                                        result.TypeOfTextConstraint = TextConstrainType.Terminology
                                        result.ConstraintCode = CType(Obj, AdlParser.ConstraintRef).Target.ToCil
                                    Case "CCODEPHRASE"
                                        result.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessCodes(CType(Obj, AdlParser.CCodePhrase))

                                        'get the code for the assumed value if it exists
                                        If CType(Obj, AdlParser.CCodePhrase).HasAssumedValue Then
                                            result.HasAssumedValue = True
                                            result.AssumedValue = CType(CType(Obj, AdlParser.CCodePhrase).AssumedValue, AdlParser.CodePhrase).CodeString.ToCil

                                            Dim assumedValue As AdlParser.CodePhrase = CType(CType(Obj, AdlParser.CCodePhrase).AssumedValue, AdlParser.CodePhrase)
                                            result.AssumedValue = assumedValue.CodeString().ToCil

                                            If Not assumedValue.TerminologyId Is Nothing Then
                                                result.AssumedValue_TerminologyId = assumedValue.TerminologyId.Value.ToCil
                                            End If
                                        End If

                                        If result.AllowableValues.TerminologyID = "local" Or result.AllowableValues.TerminologyID = "openehr" Then
                                            result.TypeOfTextConstraint = TextConstrainType.Internal
                                        End If
                                End Select
                            End If

                        Case "value"
                            Dim constraint As AdlParser.CPrimitiveObject
                            Dim cString As AdlParser.CString
                            Dim ii As Integer

                            result.TypeOfTextConstraint = TextConstrainType.Text

                            If attribute.HasChildren Then
                                constraint = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                                cString = CType(constraint.Item, AdlParser.CString)

                                For ii = 1 To cString.Strings.Count
                                    result.AllowableValues.Codes.Add(CType(cString.Strings.ITh(ii), EiffelKernel.String_8).ToCil)
                                Next
                            End If

                            'redundant by June 2006
                        Case "assumed_value"
                            Dim constraint As AdlParser.CPrimitiveObject
                            Dim cString As AdlParser.CString

                            If attribute.HasChildren Then
                                constraint = CType(attribute.Children.First, AdlParser.CPrimitiveObject)
                                cString = CType(constraint.Item, AdlParser.CString)
                                result.AssumedValue = CType(cString.Strings.First, EiffelKernel.String_8).ToCil
                            End If

                        Case "mapping"
                            Debug.Assert(False, "Mapping constraint is not available")
                            ''fix me - need to deal with mappings
                        Case Else
                            Debug.Assert(False, String.Format("Attribute not handled: {0}", attribute.RmAttributeName.ToCil))
                            ''fix me - need to deal with mappings
                    End Select
                Next
            End If

            Return result
        End Function

        Private Function ProcessQuantity(ByVal ObjNode As AdlParser.CDvQuantity) As Constraint_Quantity
            Dim q As New Constraint_Quantity
            Dim u As Constraint_QuantityUnit
            Dim i As Integer

            If ObjNode.AnyAllowed Then
                Return q
            End If

            If Not ObjNode.property Is Nothing Then
                Dim s As String = ObjNode.Property.CodeString.ToCil

                If IsNumeric(s) Then
                    q.OpenEhrCode = Integer.Parse(s)
                Else
                    'OBSOLETE - to cope with physical properties
                    q.PhysicalPropertyAsString = s
                End If

                If (Not ObjNode.list Is Nothing) AndAlso ObjNode.list.count > 0 Then
                    Dim cqi As AdlParser.CQuantityItem

                    For i = 1 To ObjNode.list.count
                        'Do not set attribute to true here as there
                        'is no special handling of time units until Unit is set
                        u = New Constraint_QuantityUnit(q.IsTime)
                        cqi = CType(ObjNode.list.ITh(i), AdlParser.CQuantityItem)

                        u.Unit = cqi.Units.ToCil

                        If Not cqi.AnyMagnitudeAllowed Then
                            u.HasMaximum = Not cqi.Magnitude.UpperUnbounded
                            If u.HasMaximum Then
                                u.MaximumRealValue = CSng(cqi.Magnitude.Upper)
                                u.IncludeMaximum = cqi.Magnitude.UpperIncluded
                            End If
                            u.HasMinimum = Not cqi.Magnitude.LowerUnbounded
                            If u.HasMinimum Then
                                u.MinimumRealValue = CSng(cqi.Magnitude.Lower)
                                u.IncludeMinimum = cqi.Magnitude.LowerIncluded
                            End If
                        End If

                        If Not cqi.AnyPrecisionAllowed Then
                            'Only deal in maximum precision
                            u.Precision = cqi.Precision.Upper
                        End If

                        ' need to add with key for retrieval
                        q.Units.Add(u, u.Unit)
                    Next
                End If

                If ObjNode.HasAssumedValue Then
                    Dim units As String = CType(ObjNode.AssumedValue, AdlParser.Quantity).Units.ToCil
                    Dim assumedUnits As Constraint_QuantityUnit

                    If q.IsCoded AndAlso q.OpenEhrCode = 128 Then  'time
                        assumedUnits = CType(q.Units.Item(OceanArchetypeEditor.ISO_TimeUnits.GetOptimalIsoUnit(units)), Constraint_QuantityUnit)
                    Else
                        assumedUnits = CType(q.Units.Item(units), Constraint_QuantityUnit)
                    End If

                    assumedUnits.AssumedValue = CType(ObjNode.AssumedValue, AdlParser.Quantity).Magnitude
                    assumedUnits.HasAssumedValue = True
                End If
            End If

            Return q
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
'The Original Code is RmElement.vb.
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
