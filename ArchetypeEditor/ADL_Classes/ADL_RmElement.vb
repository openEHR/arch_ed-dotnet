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
Namespace ArchetypeEditor.ADL_Classes

    Public Class ADL_RmElement
        Inherits RmElement

        Sub New(ByVal EIF_Element As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
            MyBase.New(EIF_Element)
            ProcessElement(EIF_Element, a_filemanager)
        End Sub

        Private Sub ProcessElement(ByVal ComplexObj As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)

            Try

                If ComplexObj.any_allowed Or (Not ComplexObj.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("value"))) Then
                    'This is an unknown and is available for specialisation
                    Dim c As New Constraint
                    Me.cConstraint = c
                    Return
                End If

                ' Get the value
                If ComplexObj.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("value")) Then
                    Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
                    Dim i As Integer

                    an_attribute = ComplexObj.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("value"))

                    If an_attribute.children.count > 1 Then
                        ' multiple constraints - not dealt with yet in the GUI
                        Dim m_c As New Constraint_Choice
                        For i = 1 To an_attribute.children.count
                            m_c.Constraints.Add(ProcessValue(CType(an_attribute.children.i_th(i), openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager))
                        Next
                        cConstraint = m_c
                    Else
                        cConstraint = ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager)
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ComplexObj.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' set to any
                cConstraint = New Constraint
            End Try
        End Sub

        Private Function ProcessInterval(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal) As Constraint_Interval

            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            Debug.Assert(Not ObjNode.any_allowed)

            Select Case ObjNode.rm_type_name.to_cil.ToLowerInvariant()
                Case "interval_count"
                    Dim cic As New Constraint_Interval_Count
                    Dim countLimits As Constraint_Count

                    an_attribute = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    countLimits = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_Count)
                    If countLimits.HasMaximum Then
                        CType(cic.UpperLimit, Constraint_Count).HasMaximum = True
                        CType(cic.UpperLimit, Constraint_Count).MaximumValue = countLimits.MaximumValue
                    End If
                    If countLimits.HasMinimum Then
                        CType(cic.LowerLimit, Constraint_Count).HasMinimum = True
                        CType(cic.LowerLimit, Constraint_Count).MinimumValue = countLimits.MinimumValue
                    End If
                    Return cic
                Case "dv_interval<dv_count>", "dv_interval<count>", "interval<count>"
                    Dim cic As New Constraint_Interval_Count
                    Try
                        ' Get the upper value
                        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("upper")) Then
                            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("upper"))
                            cic.UpperLimit = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_Count)
                        End If
                        ' Get the lower value
                        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("lower")) Then
                            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("lower"))
                            cic.LowerLimit = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_Count)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Return cic
                Case "interval_quantity"
                    Dim ciq As New Constraint_Interval_Quantity
                    Dim quantLimits As New Constraint_Quantity

                    an_attribute = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    quantLimits = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_Quantity)

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
                        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("upper")) Then
                            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("upper"))
                            ciq.UpperLimit = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_Quantity)
                        End If
                        ' Get the lower value
                        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("lower")) Then
                            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("lower"))
                            ciq.LowerLimit = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_Quantity)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(AE_Constants.Instance.Incorrect_format & " " & ObjNode.node_id.to_cil & ": " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Return ciq
                Case "dv_interval<dv_date_time>", "dv_interval<date_time>", "interval<date_time>"
                    Dim cidt As New Constraint_Interval_DateTime
                    Try
                        ' Get the upper value
                        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("upper")) Then
                            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("upper"))
                            cidt.UpperLimit = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_DateTime)
                        End If
                        ' Get the lower value
                        If ObjNode.has_attribute(openehr.base.kernel.Create.STRING.make_from_cil("lower")) Then
                            an_attribute = ObjNode.c_attribute_at_path(openehr.base.kernel.Create.STRING.make_from_cil("lower"))
                            cidt.LowerLimit = CType(ProcessValue(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT), a_filemanager), Constraint_DateTime)
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

        Private Function ProcessDuration(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Duration

            If ObjNode.any_allowed Then
                Return New Constraint_Duration
            Else
                Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

                For i As Integer = 1 To ObjNode.attributes.count

                    an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                    Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "value"
                            Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                            If an_attribute.children.count > 0 Then
                                constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                Return ProcessDuration(constraint)
                            End If
                    End Select
                Next
            End If

            'Shouldn't get to here
            Debug.Assert(False, "Error processing Duration")
            Return New Constraint_Duration

        End Function


        Private Function ProcessDuration(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Duration
            Dim duration As New Constraint_Duration

            If ObjNode.any_allowed Then
                Return duration
            End If

            Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION
            cadlC = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DURATION)
            duration.AllowableUnits = cadlC.pattern.to_cil
            Return duration
        End Function

        Private Function ProcessMultiMedia(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_MultiMedia
            Dim mm As New Constraint_MultiMedia
            Dim media_type As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            If ObjNode.any_allowed Then
                Return mm
            End If

            Try
                media_type = CType(ObjNode.attributes.first, openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                Dim Obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT

                Obj = CType(media_type.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)

                mm.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessCodes(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE))

            Catch ex As Exception
                Debug.Assert(False)
                MessageBox.Show(AE_Constants.Instance.Error_loading & " Multimedia constraint:" & ObjNode.node_id.to_cil & _
                    " - " & ex.Message, AE_Constants.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Return mm

        End Function

        Private Function ProcessValue(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_OBJECT, ByVal a_filemanager As FileManagerLocal) As Constraint

            Select Case ObjNode.rm_type_name.to_cil.ToLowerInvariant()
                Case "dv_quantity", "quantity"
                    Return ProcessQuantity(CType(ObjNode, openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_QUANTITY))
                Case "dv_coded_text", "dv_text", "coded_text", "text"
                    Return ProcessText(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "dv_boolean", "boolean"
                    If TypeOf (ObjNode) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                        Return ProcessBoolean(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Else
                        'obsolete
                        Return ProcessBoolean(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                    End If
                Case "dv_ordinal", "ordinal"
                    If TypeOf (ObjNode) Is openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL Then
                        Return ProcessOrdinal(CType(ObjNode, openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL), a_filemanager)
                    Else
                        'redundant
                        Return ProcessOrdinal(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager)
                    End If
                Case "dv_date_time", "dv_date", "dv_time", "datetime", "date_time", "date", "time", "_c_date"
                    If TypeOf (ObjNode) Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                        Return ProcessDateTime(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Else
                        'obsolete
                        Return ProcessDateTime(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                    End If
                Case "quantity_ratio" ' OBSOLETE
                    Return ProcessRatio(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "dv_proportion", "proportion"
                    Return ProcessProportion(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "dv_count", "count"
                    Return ProcessCount(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "interval_count", "interval_quantity" 'OBSOLETE
                    Return ProcessInterval(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager)
                Case "dv_interval<dv_count>", "dv_interval<dv_quantity>", "dv_interval<dv_date_time>", "dv_interval<count>", "dv_interval<quantity>", "dv_interval<date_time>", "interval<count>", "interval<quantity>", "interval<date_time>"
                    Return ProcessInterval(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT), a_filemanager)
                Case "dv_multimedia", "multimedia", "multi_media"
                    Return ProcessMultiMedia(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "dv_uri", "uri"
                    Return New Constraint_URI
                Case "dv_duration", "duration"
                    If TypeOf ObjNode Is openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT Then
                        Return ProcessDuration(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Else
                        'obsolete
                        Return ProcessDuration(CType(ObjNode, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                    End If
                Case Else
                    Debug.Assert(False, String.Format("Attribute not handled: {0}", ObjNode.rm_type_name.to_cil))
                    Return New Constraint
            End Select
        End Function

        Private Function ProcessCount(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Count
            Dim ct As New Constraint_Count
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim i As Integer

            If ObjNode.any_allowed Then
                Return ct
            End If

            For i = 1 To ObjNode.attributes.count
                an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                Select Case an_attribute.rm_attribute_name.to_cil.ToLowerInvariant
                    Case "value", "magnitude"
                        Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                        Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER

                        cadlOS = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                        cadlC = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)

                        If Not cadlC.interval.lower_unbounded Then
                            ct.HasMinimum = True
                            ct.MinimumValue = cadlC.interval.lower
                            ct.IncludeMinimum = cadlC.interval.lower_included
                        Else
                            ct.HasMinimum = False
                        End If
                        If Not cadlC.interval.upper_unbounded Then
                            ct.HasMaximum = True
                            ct.MaximumValue = cadlC.interval.upper
                            ct.IncludeMaximum = cadlC.interval.upper_included
                        Else
                            ct.HasMaximum = False
                        End If

                        If cadlC.has_assumed_value Then
                            ct.HasAssumedValue = True
                            ct.AssumedValue = CType(cadlC.assumed_value, openehr.base.kernel.INTEGER_REF).item
                        End If
                    Case Else
                        Debug.Assert(False)
                End Select
            Next
            Return ct
        End Function

        Private Function ProcessReal(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Real
            Dim ct As New Constraint_Real
           
            Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_REAL

            cadlC = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_REAL)

            If Not cadlC.interval.lower_unbounded Then
                ct.HasMinimum = True
                ct.MinimumValue = cadlC.interval.lower
                ct.IncludeMinimum = cadlC.interval.lower_included
            Else
                ct.HasMinimum = False
            End If
            If Not cadlC.interval.upper_unbounded Then
                ct.HasMaximum = True
                ct.MaximumValue = cadlC.interval.upper
                ct.IncludeMaximum = cadlC.interval.upper_included
            Else
                ct.HasMaximum = False
            End If
            If cadlC.has_assumed_value Then
                ct.HasAssumedValue = True
                ct.AssumedValue = cadlC.assumed_value
            End If

            Return ct

        End Function

        'OBSOLETE
        Private Function ProcessRatio(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Proportion
            Dim rat As New Constraint_Proportion
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            If ObjNode.any_allowed Then
                Return rat
            Else
                For i As Integer = 1 To ObjNode.attributes.count
                    an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                    Select Case an_attribute.rm_attribute_name.to_cil
                        Case "numerator"
                            rat.Numerator = New Constraint_Real(ProcessCount(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case "denominator"
                            rat.Denominator = New Constraint_Real(ProcessCount(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)))
                        Case Else
                            Debug.Assert(False)
                    End Select
                Next
            End If

            Return rat

        End Function

        Private Function ProcessProportion(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Proportion
            Dim proportion As New Constraint_Proportion
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

            If ObjNode.any_allowed Then
                Return proportion
            Else
                For i As Integer = 1 To ObjNode.attributes.count
                    an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                    Select Case an_attribute.rm_attribute_name.to_cil
                        Case "numerator"
                            proportion.Numerator = ProcessReal(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                        Case "denominator"
                            proportion.Denominator = ProcessReal(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                        Case "is_integral"
                            Dim bool As Constraint_Boolean = ProcessBoolean(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT))
                            If bool.TrueAllowed Then
                                proportion.IsIntegral = True
                            Else
                                proportion.IsIntegral = False
                            End If
                            proportion.IsIntegralSet = True
                        Case "type"
                            Dim cadlOS As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                            Dim cadlC As openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER

                            cadlOS = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                            cadlC = CType(cadlOS.item, openehr.openehr.am.archetype.constraint_model.primitive.C_INTEGER)
                            proportion.SetAllTypesDisallowed()
                            If Not cadlC.list Is Nothing Then
                                For ii As Integer = 1 To cadlC.list.count
                                    proportion.AllowType(cadlC.list.i_th(ii))
                                Next
                            ElseIf Not cadlC.interval Is Nothing Then
                                'is an interval as only one allowed
                                proportion.AllowType(cadlC.interval.upper)
                            Else
                                proportion.SetAllTypesAllowed()
                            End If


                        Case Else
                            Debug.Assert(False)
                    End Select
                Next
            End If

            Return proportion

        End Function


        Private Function ProcessDateTime(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_DateTime

            If ObjNode.any_allowed Then
                Return New Constraint_DateTime
            Else
                Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

                For i As Integer = 1 To ObjNode.attributes.count

                    an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                    Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "value"
                            Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                            If an_attribute.children.count > 0 Then
                                constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                Return ProcessDateTime(constraint)
                            End If
                    End Select
                Next
            End If

            'Shouldn't get to here
            Debug.Assert(False, "Error processing boolean")
            Return New Constraint_DateTime

        End Function

        Private Function ProcessDateTime(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_DateTime
            Dim dt As New Constraint_DateTime
            Dim s As String

            If ObjNode.any_allowed Then
                Return dt
            End If

            Select Case ObjNode.rm_type_name.to_cil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
                Case "DATE_TIME"
                    Dim cadlDT As openehr.openehr.am.archetype.constraint_model.primitive.C_DATE_TIME
                    cadlDT = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_DATE_TIME)
                    s = cadlDT.pattern.to_cil

                Case "DATE"
                    Dim cadlD As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_DATE
                    cadlD = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_DATE)
                    s = cadlD.pattern.to_cil

                Case "TIME"
                    Dim cadlT As openehr.openehr.am.archetype.constraint_model.primitive.C_TIME
                    cadlT = CType(ObjNode.item, openehr.openehr.am.archetype.constraint_model.primitive.C_TIME)
                    s = cadlT.pattern.to_cil

                Case Else
                    Debug.Assert(False)
                    Return Nothing
            End Select


            Select Case s.ToLowerInvariant()
                Case "yyyy-??-?? ??:??:??", "yyyy-??-??t??:??:??"
                    ' Allow all
                    dt.TypeofDateTimeConstraint = 11
                Case "yyyy-mm-dd hh:mm:ss", "yyyy-mm-ddthh:mm:ss"
                    dt.TypeofDateTimeConstraint = 12
                Case "yyyy-mm-dd hh:??:??", "yyyy-mm-ddthh:??:??"
                    'Partial Date time
                    dt.TypeofDateTimeConstraint = 13
                Case "yyyy-??-??"
                    'Date only
                    dt.TypeofDateTimeConstraint = 14
                Case "yyyy-mm-dd"
                    'Full date
                    dt.TypeofDateTimeConstraint = 15
                Case "yyyy-??-xx"
                    'Partial date
                    dt.TypeofDateTimeConstraint = 16
                Case "yyyy-mm-??"
                    'Partial date with month
                    dt.TypeofDateTimeConstraint = 17
                Case "hh:??:??", "thh:??:??"
                    'TimeOnly
                    dt.TypeofDateTimeConstraint = 18
                Case "hh:mm:ss", "thh:mm:ss"
                    'Full time
                    dt.TypeofDateTimeConstraint = 19
                Case "hh:??:xx", "thh:??:xx"
                    'Partial time
                    dt.TypeofDateTimeConstraint = 20
                Case "hh:mm:??", "thh:mm:??"
                    'Partial time with minutes
                    dt.TypeofDateTimeConstraint = 21
            End Select

            Return dt

        End Function

        Private Function ProcessOrdinal(ByVal an_ordinal_constraint As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL, ByVal a_filemanager As FileManagerLocal) As Constraint_Ordinal
            Dim ord As New Constraint_Ordinal(a_filemanager)
            Dim Ordinals As openehr.base.structures.list.LINKED_LIST_ANY
            Dim c_phrase As New CodePhrase
            Dim openehr_ordinal As openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL

            '' first value may have a "?" instead of a code as holder for empty ordinal
            If Not an_ordinal_constraint.any_allowed Then
                Ordinals = an_ordinal_constraint.items()

                Ordinals.start()

                Do While Not Ordinals.off
                    openehr_ordinal = CType(Ordinals.active.item, openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL)

                    Dim newOrdinal As OrdinalValue = ord.OrdinalValues.NewOrdinal

                    newOrdinal.Ordinal = openehr_ordinal.value
                    c_phrase.Phrase = openehr_ordinal.symbol.as_string.to_cil

                    If c_phrase.TerminologyID = "local" Then
                        newOrdinal.InternalCode = c_phrase.FirstCode
                        ord.OrdinalValues.Add(newOrdinal)
                    Else
                        Beep()
                        Debug.Assert(False)
                    End If

                    Ordinals.forth()
                Loop

                If an_ordinal_constraint.has_assumed_value Then
                    ord.HasAssumedValue = True
                    ord.AssumedValue = CType(an_ordinal_constraint.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.ORDINAL).value
                End If
            End If

            Return ord

        End Function

        'OBSOLETE
        Private Function ProcessOrdinal(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal) As Constraint_Ordinal
            Dim i As Integer
            Dim c_value As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
           
            If Not ObjNode.any_allowed Then
                For i = 1 To ObjNode.attributes.count

                    an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)

                    Select Case an_attribute.rm_attribute_name.to_cil.ToLowerInvariant()
                        Case "value"
                            c_value = CType(an_attribute.children.first, openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_ORDINAL)

                            Return ProcessOrdinal(c_value, a_filemanager)
                        Case Else
                            Debug.Assert(False, "attribute not handled")
                    End Select

                Next
            End If
            Return New Constraint_Ordinal(True, a_filemanager)

        End Function

        Private Function ProcessBoolean(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Boolean

            If ObjNode.any_allowed Then
                Return New Constraint_Boolean
            Else
                Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

                For i As Integer = 1 To ObjNode.attributes.count

                    an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                    Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                        Case "value"
                            Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                            If an_attribute.children.count > 0 Then
                                constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                                Return ProcessBoolean(constraint)
                            End If
                    End Select
                Next
            End If

            'Shouldn't get to here
            Debug.Assert(False, "Error processing boolean")
            Return New Constraint_Boolean

        End Function

        Private Function ProcessBoolean(ByVal ObjSimple As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT) As Constraint_Boolean
            Dim b As New Constraint_Boolean
            Dim i As Integer


            Dim bool As openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN = _
                CType(ObjSimple.item, openehr.openehr.am.archetype.constraint_model.primitive.C_BOOLEAN)

            If bool.true_valid Then
                i = 1
            End If
            If bool.false_valid Then
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

            If bool.has_assumed_value() Then
                b.hasAssumedValue = True
                b.AssumedValue = CType(bool.assumed_value, openehr.base.kernel.BOOLEAN_REF).item
            End If

            Return b

        End Function


        Shared Function ProcessText(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT) As Constraint_Text
            Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
            Dim t As New Constraint_Text
            Dim i As Integer

            If ObjNode.any_allowed Then
                t.TypeOfTextConstraint = TextConstrainType.Text
                Return t
            End If

            For i = 1 To ObjNode.attributes.count

                an_attribute = CType(ObjNode.attributes.i_th(i), openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
                Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "defining_code", "code" 'code is redundant
                        'coded - could be a constraint or the actual codes so..
                        Dim Obj As openehr.openehr.am.archetype.constraint_model.C_OBJECT

                        Obj = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_OBJECT)

                        Select Case Obj.generating_type.to_cil.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
                            Case "CONSTRAINT_REF"
                                t.TypeOfTextConstraint = TextConstrainType.Terminology
                                t.ConstraintCode = CType(Obj, openehr.openehr.am.archetype.constraint_model.CONSTRAINT_REF).target.to_cil
                            Case "C_CODE_PHRASE"
                                t.AllowableValues = ArchetypeEditor.ADL_Classes.ADL_Tools.ProcessCodes(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE))

                                'get the code for the assumed value if it exists
                                If CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE).has_assumed_value Then
                                    t.HasAssumedValue = True
                                    t.AssumedValue = CType(CType(Obj, openehr.openehr.am.openehr_profile.data_types.text.C_CODE_PHRASE).assumed_value, openehr.openehr.rm.data_types.text.CODE_PHRASE).code_string.to_cil
                                End If

                                If t.AllowableValues.TerminologyID = "local" Then
                                    t.TypeOfTextConstraint = TextConstrainType.Internal
                                End If
                        End Select


                    Case "value"
                        Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                        Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING
                        Dim EIF_String As openehr.base.kernel.STRING
                        Dim ii As Integer

                        t.TypeOfTextConstraint = TextConstrainType.Text

                        If an_attribute.children.count > 0 Then
                            constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                            cString = CType(constraint.item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING)
                            For ii = 1 To cString.strings.count
                                EIF_String = CType(cString.strings.i_th(ii), openehr.base.kernel.STRING)
                                t.AllowableValues.Codes.Add(EIF_String.to_cil)
                            Next
                        End If

                        'redundant by June 2006
                    Case "assumed_value"
                        Dim constraint As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT
                        Dim cString As openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING
                        Dim EIF_String As openehr.base.kernel.STRING

                        If an_attribute.children.count > 0 Then
                            constraint = CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT)
                            cString = CType(constraint.item, openehr.openehr.am.archetype.constraint_model.primitive.OE_C_STRING)
                            EIF_String = CType(cString.strings.first, openehr.base.kernel.STRING)
                            t.AssumedValue = EIF_String.to_cil
                        End If

                    Case "mapping"
                        Debug.Assert(False, "Mapping constraint is not available")
                        ''fix me - need to deal with mappings
                    Case Else
                        Debug.Assert(False, String.Format("Attribute not handled: {0}", an_attribute.rm_attribute_name.to_cil))
                        ''fix me - need to deal with mappings
                End Select
            Next
            Return t

        End Function

        Private Function ProcessQuantity(ByVal ObjNode As openehr.openehr.am.openehr_profile.data_types.quantity.C_DV_QUANTITY) As Constraint_Quantity
            Dim q As New Constraint_Quantity
            Dim u As Constraint_QuantityUnit
            Dim i As Integer

            If ObjNode.any_allowed Then
                Return q
            End If

            If Not ObjNode.property Is Nothing Then

                Dim s As String = ObjNode.property.code_string.to_cil

                If IsNumeric(s) Then
                    q.OpenEhrCode = Integer.Parse(s)
                Else
                    'OBSOLETE - to cope with physical properties
                    q.PhysicalPropertyAsString = s
                End If

                If (Not ObjNode.list Is Nothing) AndAlso ObjNode.list.count > 0 Then
                    Dim cqi As openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM

                    For i = 1 To ObjNode.list.count
                        'Do not set attribute to true here as there
                        'is no special handling of time units until Unit is set
                        u = New Constraint_QuantityUnit(q.IsTime)
                        cqi = CType(ObjNode.list.i_th(i), openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM)

                        u.Unit = cqi.units.to_cil

                        If Not cqi.any_magnitude_allowed Then
                            u.HasMaximum = Not cqi.magnitude.upper_unbounded
                            If u.HasMaximum Then
                                u.MaximumValue = CSng(cqi.magnitude.upper)
                                u.IncludeMaximum = cqi.magnitude.upper_included
                            End If
                            u.HasMinimum = Not cqi.magnitude.lower_unbounded
                            If u.HasMinimum Then
                                u.MinimumValue = CSng(cqi.magnitude.lower)
                                u.IncludeMinimum = cqi.magnitude.lower_included
                            End If
                            If Not cqi.any_precision_allowed Then
                                'Only deal in maximum precision
                                u.Precision = cqi.precision.upper
                            End If
                        End If
                        ' need to add with key for retrieval
                        q.Units.Add(u, u.Unit)
                    Next

                End If

                If ObjNode.has_assumed_value Then
                    Dim assumed_units As String = CType(ObjNode.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.QUANTITY).units.to_cil
                    Dim assumed_value As Single = CType(ObjNode.assumed_value, openehr.openehr.am.openehr_profile.data_types.quantity.QUANTITY).magnitude
                    CType(q.Units.Item(assumed_units), Constraint_QuantityUnit).AssumedValue = assumed_value
                    CType(q.Units.Item(assumed_units), Constraint_QuantityUnit).HasAssumedValue = True
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
