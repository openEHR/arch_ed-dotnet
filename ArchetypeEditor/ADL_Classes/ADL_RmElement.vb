'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     http://www.openehr.org/issues/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://svn.openehr.org/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/BusinessLogic/EHR_Classes/RmElement.vb $"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate: 2006-05-17 18:54:30 +0930 (Wed, 17 May 2006) $"
'
'

Option Strict On
Imports EiffelKernel = EiffelSoftware.Library.Base.kernel
Imports EiffelList = EiffelSoftware.Library.Base.structures.list
Imports XMLParser

Namespace ArchetypeEditor.ADL_Classes

    Public Class ADL_RmElement
        Inherits RmElement

        Sub New(ByVal EIF_Element As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal)
            MyBase.New(EIF_Element)
            ProcessElement(EIF_Element, fileManager)
        End Sub

        Private Sub ProcessElement(ByVal complexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal)
            Try
                Dim v As EiffelKernel.STRING_8 = Eiffel.String("value")
                cConstraint = New Constraint

                If Not complexObj.any_allowed Then
                    Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                    Dim i As Integer

                    If complexObj.has_attribute(v) Then
                        a = complexObj.c_attribute_at_path(v)

                        If a.children.count > 1 Then
                            Dim c As New Constraint_Choice

                            For i = 1 To a.children.count
                                c.Constraints.Add(ProcessValue(CType(a.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager))
                            Next

                            cConstraint = c
                        ElseIf a.has_children Then
                            cConstraint = ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager)
                        End If
                    End If

                    Dim hasNullFlavour As Boolean = False
                    v = Eiffel.String("null_flavor") ' Obsolete
                    hasNullFlavour = complexObj.has_attribute(v)

                    If Not hasNullFlavour Then
                        v = Eiffel.String("null_flavour") ' redundant
                        hasNullFlavour = complexObj.has_attribute(v)
                    End If

                    If hasNullFlavour Then
                        a = complexObj.c_attribute_at_path(v)

                        If a.children.count = 1 Then
                            Dim c As Constraint_Text = ProcessText(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))

                            If Not c Is Nothing AndAlso c.AllowableValues.Codes.Count > 0 Then
                                ConstrainedNullFlavours = c.AllowableValues
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & complexObj.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function ProcessInterval(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal) As Constraint_Interval
            Dim a As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            Debug.Assert(Not ObjNode.any_allowed)

            Select Case ObjNode.rm_type_name.to_cil.ToLowerInvariant()
                Case "interval_count"
                    Dim cic As New Constraint_Interval_Count
                    Dim countLimits As Constraint_Count

                    a = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If a.has_children Then
                        countLimits = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_Count)

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
                        If ObjNode.has_attribute(Eiffel.String("upper")) Then
                            a = ObjNode.c_attribute_at_path(Eiffel.String("upper"))

                            If a.has_children Then
                                cic.UpperLimit = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_Count)
                            End If
                        End If

                        ' Get the lower value
                        If ObjNode.has_attribute(Eiffel.String("lower")) Then
                            a = ObjNode.c_attribute_at_path(Eiffel.String("lower"))

                            If a.has_children Then
                                cic.LowerLimit = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_Count)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Return cic
                Case "interval_quantity"
                    Dim ciq As New Constraint_Interval_Quantity
                    Dim quantLimits As New Constraint_Quantity

                    a = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If a.has_children Then
                        quantLimits = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_Quantity)
                    End If

                    If quantLimits.HasUnits Then
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
                        If ObjNode.has_attribute(Eiffel.String("upper")) Then
                            a = ObjNode.c_attribute_at_path(Eiffel.String("upper"))

                            If a.has_children Then
                                ciq.UpperLimit = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_Quantity)
                            End If
                        End If

                        ' Get the lower value
                        If ObjNode.has_attribute(Eiffel.String("lower")) Then
                            a = ObjNode.c_attribute_at_path(Eiffel.String("lower"))

                            If a.has_children Then
                                ciq.LowerLimit = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_Quantity)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Return ciq
                Case "dv_interval<dv_date_time>", "dv_interval<date_time>", "interval<date_time>", "dv_interval<dv_date>", "dv_interval<dv_time>"
                    Dim cidt As New Constraint_Interval_DateTime

                    Try
                        ' Get the upper value
                        If ObjNode.has_attribute(Eiffel.String("upper")) Then
                            a = ObjNode.c_attribute_at_path(Eiffel.String("upper"))

                            If a.has_children Then
                                cidt.UpperLimit = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_DateTime)
                            End If
                        End If

                        ' Get the lower value
                        If ObjNode.has_attribute(Eiffel.String("lower")) Then
                            a = ObjNode.c_attribute_at_path(Eiffel.String("lower"))

                            If a.has_children Then
                                cidt.LowerLimit = CType(ProcessValue(CType(a.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), fileManager), Constraint_DateTime)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Return cidt
                Case Else
                    Debug.Assert(False, String.Format("Attribute not handled: {0}", ObjNode.rm_type_name.to_cil))
                    Return Nothing
            End Select
        End Function

        Private Function ProcessDuration(ByVal o As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Duration
            Dim result As New Constraint_Duration

            If Not o.any_allowed AndAlso o.attributes.count > 0 Then
                Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(o.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                If attribute.has_children Then
                    result = ProcessDurationValue(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                End If
            End If

            Return result
        End Function

        Private Function ProcessDurationValue(ByVal o As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Duration
            Dim result As New Constraint_Duration

            If Not o.any_allowed Then
                Dim duration As openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION = CType(o.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION)

                If duration.pattern IsNot Nothing Then
                    result.AllowableUnits = duration.pattern.to_cil
                End If

                If duration.interval IsNot Nothing Then
                    result.HasMinimum = Not duration.interval.lower_unbounded
                    result.HasMaximum = Not duration.interval.upper_unbounded

                    If result.HasMinimum Then
                        result.SetMinimumValueAndUnits(CType(duration.interval.lower, openehr.common_libs.date_time.ISO8601_DURATION).value.to_cil)
                        result.IncludeMinimum = duration.interval.lower_included
                    End If

                    If result.HasMaximum Then
                        result.SetMaximumValueAndUnits(CType(duration.interval.upper, openehr.common_libs.date_time.ISO8601_DURATION).value.to_cil)
                        result.IncludeMaximum = duration.interval.upper_included
                    End If
                End If

                result.HasAssumedValue = duration.assumed_value IsNot Nothing

                If result.HasAssumedValue Then
                    result.SetAssumedValueAndUnits(CType(duration.assumed_value, openehr.common_libs.date_time.ISO8601_DURATION).value.to_cil)
                End If
            End If

            Return result
        End Function

        Private Sub SetDurationValueAndUnits(ByVal d As openehr.common_libs.date_time.ISO8601_DURATION, ByVal duration As Constraint_Duration, ByVal which As Integer)
            Dim s As String = d.value.to_cil.ToUpperInvariant
            Dim units As String = s.Substring(s.Length - 1)
            Dim value As Integer = 0

            Select Case units
                Case "S"
                    value = d.seconds
                    units = "TS"
                Case "M"
                    If s.Contains("T") Then
                        value = d.minutes
                        units = "TM"
                    Else
                        value = d.months
                    End If
                Case "H"
                    value = d.hours
                    units = "TH"
                Case "D"
                    value = d.days
                Case "Y"
                    value = d.years
                Case "W"
                    value = d.weeks
            End Select

            If which = 0 Then
                duration.MinimumValue = value
            ElseIf which = 1 Then
                duration.MaximumValue = value
            Else
                duration.AssumedValue = value
            End If

            If duration.MinMaxValueUnits Is Nothing Then
                duration.MinMaxValueUnits = units
            End If
        End Sub

        Private Function ProcessMultiMedia(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_MultiMedia
            Dim result As New Constraint_MultiMedia

            If Not ObjNode.any_allowed Then
                Try
                    Dim mediaType As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If mediaType.has_children Then
                        Dim Obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT = CType(mediaType.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)
                        result.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessCodes(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE))
                    End If
                Catch ex As Exception
                    Debug.Assert(False)
                    MessageBox.Show(AE_Constants.Instance.ErrorLoading & " Multimedia constraint:" & ObjNode.node_id.to_cil & _
                        " - " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Return result
        End Function

        Private Function ProcessValue(ByVal node As openehr.openehr.am.archetype.constraint_model.C_OBJECT, ByVal fileManager As FileManagerLocal) As Constraint
            Dim result As Constraint
            Dim slot As openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT = TryCast(node, openehr.openehr.am.archetype.constraint_model.ARCHETYPE_SLOT)

            If Not slot Is Nothing Then
                result = New Constraint_Slot(slot)
            Else
                Select Case node.rm_type_name.to_cil.ToLowerInvariant()
                    Case "dv_quantity", "quantity"
                        result = ProcessQuantity(CType(node, openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_QUANTITY))
                    Case "dv_coded_text", "dv_text", "coded_text", "text"
                        result = ProcessText(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "dv_boolean", "boolean"
                        If TypeOf (node) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                            result = ProcessBoolean(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                        Else
                            'obsolete
                            result = ProcessBoolean(CType(node, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                        End If
                    Case "dv_ordinal", "ordinal"
                        If TypeOf (node) Is openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL Then
                            result = ProcessOrdinal(CType(node, openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL), fileManager)
                        Else
                            'redundant
                            result = ProcessOrdinal(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), fileManager)
                        End If
                    Case "dv_date_time", "dv_date", "dv_time", "datetime", "date_time", "date", "time", "_c_date"
                        If TypeOf (node) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                            result = ProcessDateTime(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                        Else
                            'obsolete
                            result = ProcessDateTime(CType(node, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                        End If
                    Case "quantity_ratio" ' OBSOLETE
                        result = ProcessRatio(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "dv_proportion", "proportion"
                        result = ProcessProportion(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "dv_count", "count"
                        result = ProcessCount(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "interval_count", "interval_quantity" 'OBSOLETE
                        result = ProcessInterval(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), fileManager)
                    Case "dv_interval<dv_count>", "dv_interval<count>", "interval<count>", _
                         "dv_interval<dv_quantity>", "dv_interval<quantity>", "interval<quantity>", _
                         "dv_interval<dv_date_time>", "dv_interval<date_time>", "interval<date_time>", _
                         "dv_interval<dv_date>", "dv_interval<dv_time>"
                        result = ProcessInterval(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), fileManager)
                    Case "dv_multimedia", "multimedia", "multi_media"
                        result = ProcessMultiMedia(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "dv_uri", "uri", "dv_ehr_uri"
                        result = ProcessUri(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "dv_identifier"
                        result = ProcessIdentifier(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                        'TODO Case "dv_currency"
                        '    result = ProcessCurrency(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case "dv_duration", "duration"
                        If TypeOf node Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                            result = ProcessDuration(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                        Else
                            'obsolete
                            result = ProcessDurationValue(CType(node, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                        End If
                    Case "dv_parsable"
                        result = ProcessParsable(CType(node, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Case Else
                        Debug.Assert(False, String.Format("Attribute not handled: {0}", node.rm_type_name.to_cil))
                        result = New Constraint
                End Select
            End If

            Return result
        End Function

        Shared Function ProcessParsable(ByVal dvParse As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint
            Dim result As New Constraint_Parsable
            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            Try
                For i As Integer = 1 To dvParse.attributes.count
                    attribute = CType(dvParse.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                        Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_STRING)

                        Select Case attribute.rm_attribute_name.to_cil.ToLowerInvariant()
                            Case "value"
                                'OBSOLETE

                            Case "formalism"
                                Try
                                    For ii As Integer = 1 To cadlC.strings.count
                                        result.AllowableFormalisms.Add(CType(cadlC.strings.i_th(ii), EiffelKernel.STRING_8).to_cil)
                                    Next
                                Catch ex As Exception
                                    Debug.Assert(False)
                                    MessageBox.Show(AE_Constants.Instance.ErrorLoading & " Multimedia constraint:" & dvParse.node_id.to_cil & _
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

        Shared Function ProcessUri(ByVal dvUri As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint
            Dim result As New Constraint_URI
            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            If dvUri.rm_type_name.to_cil.ToLowerInvariant = "dv_ehr_uri" Then
                result.EhrUriOnly = True
            End If

            If Not dvUri.any_allowed Then
                Try
                    attribute = CType(dvUri.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                    Debug.Assert(attribute.rm_attribute_name.to_cil.ToLowerInvariant = "value")

                    If attribute.has_children Then
                        Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                        Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_STRING)
                        result.RegularExpression = cadlC.regexp.to_cil
                    End If
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Return result
        End Function

        Shared Function ProcessIdentifier(ByVal dvIdentifier As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint
            Dim result As New Constraint_Identifier
            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = Nothing

            If Not dvIdentifier.any_allowed Then
                Try
                    For i As Integer = 1 To dvIdentifier.attributes.count
                        attribute = CType(dvIdentifier.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                        If Not attribute Is Nothing AndAlso attribute.has_children Then
                            Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                            Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_STRING)

                            Select Case attribute.rm_attribute_name.to_cil.ToLowerInvariant()
                                Case "issuer"
                                    result.IssuerRegex = cadlC.regexp.to_cil
                                Case "type"
                                    result.TypeRegex = cadlC.regexp.to_cil
                                Case "id"
                                    result.IDRegex = cadlC.regexp.to_cil
                            End Select
                        End If
                    Next
                Catch e As Exception
                    MessageBox.Show(e.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Return result
        End Function

        Private Function ProcessCount(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Count
            Dim result As New Constraint_Count
            Dim i As Integer

            If Not ObjNode.any_allowed Then
                For i = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Select Case attribute.rm_attribute_name.to_cil.ToLowerInvariant
                            Case "value", "magnitude"
                                Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                Dim magnitude As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)

                                If Not magnitude.interval.lower_unbounded Then
                                    result.HasMinimum = True
                                    result.MinimumValue = magnitude.interval.lower
                                    result.IncludeMinimum = magnitude.interval.lower_included
                                Else
                                    result.HasMinimum = False
                                End If

                                If Not magnitude.interval.upper_unbounded Then
                                    result.HasMaximum = True
                                    result.MaximumValue = magnitude.interval.upper
                                    result.IncludeMaximum = magnitude.interval.upper_included
                                Else
                                    result.HasMaximum = False
                                End If

                                If magnitude.has_assumed_value Then
                                    result.HasAssumedValue = True
                                    result.AssumedValue = CType(magnitude.assumed_value, EiffelKernel.INTEGER_32).item
                                End If
                            Case Else
                                Debug.Assert(False)
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessReal(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Real
            Dim result As New Constraint_Real
            Dim magnitude As openehr.openehr.am.archetype.constraint_model.primitive.C_REAL = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_REAL)

            If Not magnitude.interval.lower_unbounded Then
                result.HasMinimum = True
                result.MinimumRealValue = magnitude.interval.lower
                result.IncludeMinimum = magnitude.interval.lower_included
            Else
                result.HasMinimum = False
            End If

            If Not magnitude.interval.upper_unbounded Then
                result.HasMaximum = True
                result.MaximumRealValue = magnitude.interval.upper
                result.IncludeMaximum = magnitude.interval.upper_included
            Else
                result.HasMaximum = False
            End If

            If magnitude.has_assumed_value Then
                result.HasAssumedValue = True
                result.AssumedValue = CType(magnitude.assumed_value, EiffelKernel.dotnet.REAL_32_REF).item
            End If

            Return result
        End Function

        'OBSOLETE
        Private Function ProcessRatio(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Proportion
            Dim result As New Constraint_Proportion

            If Not ObjNode.any_allowed Then
                For i As Integer = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Select Case attribute.rm_attribute_name.to_cil
                            Case "numerator"
                                result.Numerator = New Constraint_Real(ProcessCount(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                            Case "denominator"
                                result.Denominator = New Constraint_Real(ProcessCount(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                            Case Else
                                Debug.Assert(False)
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessProportion(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Proportion
            Dim result As New Constraint_Proportion

            If Not ObjNode.any_allowed Then
                For i As Integer = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Select Case attribute.rm_attribute_name.to_cil
                            Case "numerator"
                                result.Numerator = ProcessReal(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                            Case "denominator"
                                result.Denominator = ProcessReal(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                            Case "is_integral"
                                Dim bool As Constraint_Boolean = ProcessBoolean(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                                result.IsIntegral = bool.TrueAllowed
                                result.IsIntegralSet = True
                            Case "type"
                                Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)
                                result.DisallowAllTypes()

                                If Not cadlC.list Is Nothing Then
                                    For ii As Integer = 1 To cadlC.list.count
                                        result.AllowType(cadlC.list.i_th(ii))
                                    Next
                                ElseIf Not cadlC.interval Is Nothing Then
                                    'is an interval as only one allowed
                                    result.AllowType(cadlC.interval.upper)
                                Else
                                    result.AllowAllTypes()
                                End If
                            Case Else
                                Debug.Assert(False)
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessDateTime(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_DateTime
            Dim result As New Constraint_DateTime()

            If ObjNode.any_allowed Then
                Select Case ObjNode.rm_type_name.to_cil.ToUpperInvariant
                    Case "DV_DATE_TIME"
                        result.TypeofDateTimeConstraint = 11
                    Case "DV_DATE"
                        result.TypeofDateTimeConstraint = 14
                    Case "DV_TIME"
                        result.TypeofDateTimeConstraint = 18
                End Select
            Else
                For i As Integer = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                            Case "value"
                                result = ProcessDateTime(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                                Exit For
                        End Select
                    End If
                Next
            End If

            Return result
        End Function

        Private Function ProcessDateTime(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_DateTime
            Dim result As New Constraint_DateTime
            Dim s As String

            'SRH: 13 jan 2009 - EDT-497 - Allow all added to each type
            'If Not ObjNode.any_allowed Then
            Select Case ObjNode.rm_type_name.to_cil.ToUpperInvariant
                Case "DATE_TIME"
                    If ObjNode.any_allowed Then
                        s = "yyyy-??-??t??:??:??"
                    Else
                        s = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DATE_TIME).pattern.to_cil
                    End If
                Case "DATE"
                    If ObjNode.any_allowed Then
                        s = "yyyy-??-??"
                    Else
                        s = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DATE).pattern.to_cil
                    End If
                Case "TIME"
                    If ObjNode.any_allowed Then
                        s = "thh:??:??"
                    Else
                        s = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_TIME).pattern.to_cil
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

        Private Function ProcessOrdinal(ByVal an_ordinal_constraint As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL, ByVal fileManager As FileManagerLocal) As Constraint_Ordinal
            Dim ord As New Constraint_Ordinal(fileManager)
            Dim Ordinals As EiffelList.LINKED_LIST_REFERENCE
            Dim c_phrase As New CodePhrase
            Dim openehr_ordinal As openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL

            '' first value may have a "?" instead of a code as holder for empty ordinal
            If Not an_ordinal_constraint.any_allowed Then
                Ordinals = an_ordinal_constraint.items()

                Ordinals.start()

                Do While Not Ordinals.off
                    openehr_ordinal = CType(Ordinals.active.item, openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL)

                    c_phrase.Phrase = openehr_ordinal.symbol.as_string.to_cil
                    'SRH: 31 May 2008 - check for redundancy in AT codes when adding ordinals (had a problem after manual cutting and pasting by some users
                    If c_phrase.TerminologyID.ToLowerInvariant = "local" Then
                        If Array.IndexOf(ord.InternalCodes, c_phrase.FirstCode) = -1 Then
                            Dim newOrdinal As OrdinalValue = ord.OrdinalValues.NewOrdinal
                            newOrdinal.InternalCode = c_phrase.FirstCode
                            newOrdinal.Ordinal = openehr_ordinal.value
                            ord.OrdinalValues.Add(newOrdinal)
                        End If
                    Else
                        Beep()
                        Debug.Assert(False)
                    End If

                    Ordinals.forth()
                Loop

                If an_ordinal_constraint.has_assumed_value Then
                    ord.HasAssumedValue = True
                    'JAR: 30APR2007, EDT-42 Support XML Schema 1.0.1
                    Dim ordinal As openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL
                    ordinal = CType(an_ordinal_constraint.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL)
                    'ord.AssumedValue = CType(an_ordinal_constraint.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL).value
                    ord.AssumedValue = ordinal.value
                    If Not ordinal.symbol Is Nothing Then
                        If Not ordinal.symbol.terminology_id Is Nothing Then ord.AssumedValue_TerminologyId = ordinal.symbol.terminology_id.value.to_cil()
                        If Not ordinal.symbol.code_string Is Nothing Then ord.AssumedValue_CodeString = ordinal.symbol.code_string.to_cil()
                    End If
                End If
            End If

            Return ord

        End Function

        'OBSOLETE
        Private Function ProcessOrdinal(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal fileManager As FileManagerLocal) As Constraint_Ordinal
            Dim i As Integer

            If Not ObjNode.any_allowed Then
                For i = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Select Case attribute.rm_attribute_name.to_cil.ToLowerInvariant()
                            Case "value"
                                Return ProcessOrdinal(CType(attribute.children.first, openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL), fileManager)
                            Case Else
                                Debug.Assert(False, "attribute not handled")
                        End Select
                    End If
                Next
            End If

            Return New Constraint_Ordinal(True, fileManager)
        End Function

        Private Function ProcessBoolean(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Boolean
            If ObjNode.any_allowed Then
                Return New Constraint_Boolean
            Else
                For i As Integer = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    If attribute.has_children Then
                        Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                            Case "value"
                                Return ProcessBoolean(CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                        End Select
                    End If
                Next
            End If

            'Shouldn't get to here
            Debug.Assert(False, "Error processing boolean")
            Return New Constraint_Boolean
        End Function

        Private Function ProcessBoolean(ByVal ObjSimple As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Boolean
            Dim result As New Constraint_Boolean
            Dim i As Integer = 0
            Dim bool As openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN = CType(ObjSimple.item, openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN)

            If bool.true_valid Then
                i = 1
            End If

            If bool.false_valid Then
                i += 2
            End If

            Select Case i
                Case 1
                    result.AllowTrueOnly()
                Case 2
                    result.AllowFalseOnly()
                Case 3
                    result.AllowTrueOrFalse()
            End Select

            If bool.has_assumed_value() Then
                result.HasAssumedValue = True
                result.AssumedValue = CType(bool.assumed_value, EiffelKernel.BOOLEAN_REF).item
            End If

            Return result
        End Function

        Shared Function ProcessText(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Text
            Dim result As New Constraint_Text
            Dim i As Integer

            If ObjNode.any_allowed Then
                result.TypeOfTextConstraint = TextConstraintType.Text
            Else
                For i = 1 To ObjNode.attributes.count
                    Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "defining_code", "code" 'code is redundant
                            'coded - could be a constraint or the actual codes so..
                            If attribute.has_children Then
                                Dim Obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)

                                Select Case Obj.generating_type.to_cil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
                                    Case "CONSTRAINT_REF"
                                        result.TypeOfTextConstraint = TextConstraintType.Terminology
                                        result.ConstraintCode = CType(Obj, openehr.openehr.am.archetype.constraint_model.CONSTRAINT_REF).target.to_cil
                                    Case "C_CODE_PHRASE"
                                        result.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessCodes(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE))

                                        'get the code for the assumed value if it exists
                                        If CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE).has_assumed_value Then
                                            result.HasAssumedValue = True
                                            result.AssumedValue = CType(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE).assumed_value, openehr.openehr.rm.data_types.text.CODE_PHRASE).code_string.to_cil

                                            Dim assumedValue As openehr.openehr.rm.data_types.text.CODE_PHRASE = CType(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE).assumed_value, openehr.openehr.rm.data_types.text.CODE_PHRASE)
                                            result.AssumedValue = assumedValue.code_string().to_cil()
                                        End If

                                        If result.AllowableValues.TerminologyID = "local" Or result.AllowableValues.TerminologyID = "openehr" Then
                                            result.TypeOfTextConstraint = TextConstraintType.Internal
                                        End If
                                End Select
                            End If

                        Case "value"
                            Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                            Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING
                            Dim ii As Integer

                            result.TypeOfTextConstraint = TextConstraintType.Text

                            If attribute.has_children Then
                                constraint = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                cString = CType(constraint.item, openehr.openehr.am.archetype.constraint_model.primitive.C_STRING)

                                For ii = 1 To cString.strings.count
                                    result.AllowableValues.Codes.Add(CType(cString.strings.i_th(ii), EiffelKernel.STRING_8).to_cil)
                                Next
                            End If

                            'redundant by June 2006
                        Case "assumed_value"
                            Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                            Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.C_STRING

                            If attribute.has_children Then
                                constraint = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                cString = CType(constraint.item, openehr.openehr.am.archetype.constraint_model.primitive.C_STRING)
                                result.AssumedValue = CType(cString.strings.first, EiffelKernel.STRING_8).to_cil
                            End If

                        Case "mapping"
                            Debug.Assert(False, "Mapping constraint is not available")
                            ''fix me - need to deal with mappings
                        Case Else
                            Debug.Assert(False, String.Format("Attribute not handled: {0}", attribute.rm_attribute_name.to_cil))
                            ''fix me - need to deal with mappings
                    End Select
                Next
            End If

            Return result
        End Function

        Private Function ProcessQuantity(ByVal node As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_QUANTITY) As Constraint_Quantity
            Dim result As New Constraint_Quantity
            Dim u As Constraint_QuantityUnit
            Dim i As Integer

            If Not node.any_allowed And Not node.property Is Nothing Then
                Dim s As String = node.property.code_string.to_cil

                If IsNumeric(s) Then
                    result.OpenEhrCode = Integer.Parse(s)
                Else
                    'OBSOLETE - to cope with physical properties
                    result.PhysicalPropertyAsString = s
                End If

                If Not node.list Is Nothing AndAlso node.list.count > 0 Then
                    Dim cqi As openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM

                    For i = 1 To node.list.count
                        'Do not set attribute to true here as there is no special handling of time units until Unit is set
                        u = New Constraint_QuantityUnit(result.IsTime)
                        cqi = CType(node.list.i_th(i), openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM)
                        u.Unit = cqi.units.to_cil

                        If Not cqi.any_magnitude_allowed Then
                            u.HasMaximum = Not cqi.magnitude.upper_unbounded

                            If u.HasMaximum Then
                                u.MaximumRealValue = CSng(cqi.magnitude.upper)
                                u.IncludeMaximum = cqi.magnitude.upper_included
                            End If

                            u.HasMinimum = Not cqi.magnitude.lower_unbounded

                            If u.HasMinimum Then
                                u.MinimumRealValue = CSng(cqi.magnitude.lower)
                                u.IncludeMinimum = cqi.magnitude.lower_included
                            End If
                        End If

                        If Not cqi.any_precision_allowed Then
                            'Only deal in maximum precision
                            u.Precision = cqi.precision.upper
                        End If

                        result.Units.Add(u, u.Unit)
                    Next
                End If

                If node.has_assumed_value Then
                    Dim units As String = CType(node.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.QUANTITY).units.to_cil
                    Dim assumedUnits As Constraint_QuantityUnit

                    If result.IsCoded AndAlso result.OpenEhrCode = 128 Then  'time
                        assumedUnits = CType(result.Units.Item(Main.ISO_TimeUnits.GetOptimalIsoUnit(units)), Constraint_QuantityUnit)
                    Else
                        assumedUnits = CType(result.Units.Item(units), Constraint_QuantityUnit)
                    End If

                    assumedUnits.AssumedValue = CType(node.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.QUANTITY).magnitude
                    assumedUnits.HasAssumedValue = True
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
