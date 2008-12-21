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

Option Explicit On 

Class RmEvent
    Inherits RmStructure

    Enum ObservationEventType
        [Event]
        PointInTime
        Interval
    End Enum


    Dim iOffset As Integer
    Dim sOffset As String
    Dim sMath As String
    Dim iWidth As Long = 1
    Dim sWidth As String
    Dim boolSignNeg, boolFixedDuration, boolFixedOffset As Boolean
    Dim sData As String
    Dim mEventType As ObservationEventType
    Dim mMathFunction As Constraint_Text

    Public Shadows ReadOnly Property TypeName() As String
        Get
            Return mType.ToString
        End Get
    End Property

    Public Property Width() As Long
        Get
            Return iWidth
        End Get
        Set(ByVal Value As Long)
            iWidth = Value
            boolFixedDuration = True
        End Set
    End Property
    Public Property WidthUnits() As String
        Get
            Return sWidth
        End Get
        Set(ByVal Value As String)
            sWidth = Value
        End Set
    End Property
    Public Property EventType() As ObservationEventType
        Get
            Return mEventType
        End Get
        Set(ByVal Value As ObservationEventType)
            mEventType = Value
            Select Case Value
                Case ObservationEventType.Event
                    mType = StructureType.Event
                Case ObservationEventType.Interval
                    mType = StructureType.IntervalEvent
                Case ObservationEventType.PointInTime
                    mType = StructureType.PointEvent
            End Select
        End Set
    End Property
    Public Property Offset() As Integer
        Get
            Return iOffset
        End Get
        Set(ByVal Value As Integer)
            iOffset = Value
            boolFixedOffset = True
        End Set
    End Property
    Public Property OffsetUnits() As String
        Get
            Return sOffset
        End Get
        Set(ByVal Value As String)
            sOffset = Value
        End Set
    End Property
    Property AggregateMathFunction() As String
        Get
            Return sMath
        End Get
        Set(ByVal Value As String)
            sMath = Value
        End Set
    End Property
    Property hasFixedDuration() As Boolean
        Get
            Return boolFixedDuration
        End Get
        Set(ByVal Value As Boolean)
            boolFixedDuration = Value
        End Set
    End Property
    Property hasFixedOffset() As Boolean
        Get
            Return boolFixedOffset
        End Get
        Set(ByVal Value As Boolean)
            boolFixedOffset = Value
        End Set
    End Property
    Property data() As String
        Get
            Return sData
        End Get
        Set(ByVal Value As String)
            sData = Value
        End Set
    End Property

    Public Overrides Function Copy() As RmStructure
        Dim result As New RmEvent(NodeId)
        result.cOccurrences = cOccurrences
        result.mType = mType
        result.sNodeId = sNodeId
        result.mRunTimeConstraint = mRunTimeConstraint
        result.iOffset = iOffset
        result.sOffset = sOffset
        result.iWidth = iWidth
        result.sWidth = sWidth
        result.mEventType = EventType
        result.boolSignNeg = boolSignNeg
        result.boolFixedDuration = boolFixedDuration
        result.boolFixedOffset = boolFixedOffset
        Return result
    End Function

    Sub New(ByVal NodeId As String)
        MyBase.New(NodeId, StructureType.Event)
    End Sub

#Region "ADL Processing"

    Sub New(ByVal An_Event As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(An_Event.node_id.to_cil, StructureType.Event)
        ProcessEvent(An_Event, a_filemanager)
    End Sub

    Private mEIF_Data As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

    Property ADL_Data() As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Get
            Return mEIF_Data
        End Get
        Set(ByVal Value As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Debug.Assert(False)
        End Set
    End Property

    Private mEIF_State As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

    Property ADL_State() As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Get
            Return mEIF_State
        End Get
        Set(ByVal Value As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE)
            Debug.Assert(False)
        End Set
    End Property

    Private Sub ProcessEvent(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim i As Integer

        Select Case ObjNode.rm_type_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "event"
                EventType = ObservationEventType.Event
            Case "point_event"
                EventType = ObservationEventType.PointInTime
            Case "interval_event"
                EventType = ObservationEventType.Interval
            Case Else
                Debug.Assert(False)
        End Select

        cOccurrences = ArchetypeEditor.ADL_Classes.ADL_Tools.SetOccurrences(ObjNode.occurrences)

        For i = 1 To ObjNode.attributes.count

            an_attribute = ObjNode.attributes.i_th(i)

            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                Case "name", "runtime_label" ' runtime_label is OBSOLETE
                    mRunTimeConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "offset"
                    Try
                        Dim d As Duration = _
                            ArchetypeEditor.ADL_Classes.ADL_Tools.GetDuration(an_attribute)
                        Me.Offset = d.GUI_duration
                        Me.OffsetUnits = d.ISO_Units
                    Catch e As Exception
                        MessageBox.Show(String.Format("Event[{1}]/offset attribute - {0}", Me.NodeId, e.Message))
                    End Try

                Case "width"
                    Try
                        Dim d As Duration = _
                            ArchetypeEditor.ADL_Classes.ADL_Tools.GetDuration(an_attribute)
                        Me.Width = d.GUI_duration
                        Me.WidthUnits = d.ISO_Units
                    Catch e As Exception
                        MessageBox.Show(String.Format("Event[{1}]/width attribute - {0}", Me.NodeId, e.Message))
                    End Try

                Case "aggregate_math_function" ' OBSOLETE
                    Dim MathFunc As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    Debug.Assert(mType = StructureType.IntervalEvent)

                    MathFunc = an_attribute.children.first
                    Me.sMath = MathFunc.item.as_string.to_cil.Trim("""")

                Case "math_function"

                    Debug.Assert(mType = StructureType.IntervalEvent)

                    Dim textConstraint As Constraint_Text = _
                    ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                    Me.sMath = textConstraint.AllowableValues.FirstCode ' only one allowed

                Case "display_as_positive"  ' OBSOLETE
                    Dim DisplayPos As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    Debug.Assert(False, "archetype contains obsolete data - will be written correctly")

                    DisplayPos = an_attribute.children.first
                    boolSignNeg = (DisplayPos.item.as_string.to_cil = "True")
                    If boolSignNeg Then
                        sMath = "521" ' a change that is displayed in absolute terms
                    End If

                Case "data", "item" 'item is OBSOLETE
                    ' return the data for processing
                    mEIF_Data = an_attribute

                Case "state"
                    mEIF_State = an_attribute

            End Select

        Next


        If Not Me.ADL_Data Is Nothing Then
            Dim cadlStruct As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

            Dim generating_type As String = Me.ADL_Data.children.first.Generating_Type.to_cil
            Select Case generating_type
                Case "ARCHETYPE_INTERNAL_REF"
                    ' Place holder for different structures at different events 
                    ' not available as yet
                Case "C_COMPLEX_OBJECT"
                    cadlStruct = CType(Me.ADL_Data.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                    Dim structure_type As StructureType

                    structure_type = ReferenceModel.StructureTypeFromString(cadlStruct.rm_type_name.to_cil)

                    Debug.Assert(structure_type <> StructureType.Not_Set)

                    If structure_type = StructureType.Table Then
                        ArchetypeEditor.ADL_Classes.ADL_Tools.LastProcessedStructure = New RmTable(cadlStruct, a_filemanager)
                    Else
                        ArchetypeEditor.ADL_Classes.ADL_Tools.LastProcessedStructure = New RmStructureCompound(cadlStruct, a_filemanager)
                    End If
            End Select
        End If


        If Not Me.ADL_State Is Nothing Then
            Dim generating_type As String = Me.ADL_State.children.first.Generating_Type.to_cil

            Select Case generating_type
                Case "ARCHETYPE_INTERNAL_REF"
                    ' Place holder for different structures at different events 
                    ' not available as yet
                Case "C_COMPLEX_OBJECT"
                    ArchetypeEditor.ADL_Classes.ADL_Tools.StateStructure = New RmStructureCompound(Me.ADL_State, StructureType.State, a_filemanager)
                Case "ARCHETYPE_SLOT"
                    ArchetypeEditor.ADL_Classes.ADL_Tools.StateStructure = New RmStructureCompound(Me.ADL_State, StructureType.State, a_filemanager)
            End Select
        End If

    End Sub

#End Region

#Region "XML Processing"

    Sub New(ByVal An_Event As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(An_Event.node_id, StructureType.Event)
        ProcessEvent(An_Event, a_filemanager)
    End Sub

    Private mXML_Data As XMLParser.C_ATTRIBUTE
    Property XML_Data() As XMLParser.C_ATTRIBUTE
        Get
            Return mXML_Data
        End Get
        Set(ByVal Value As XMLParser.C_ATTRIBUTE)
            Debug.Assert(False)
        End Set
    End Property

    Private mXML_State As XMLParser.C_ATTRIBUTE
    Property XML_State() As XMLParser.C_ATTRIBUTE
        Get
            Return mXML_State
        End Get
        Set(ByVal Value As XMLParser.C_ATTRIBUTE)
            Debug.Assert(False)
        End Set
    End Property

    Private Sub ProcessEvent(ByVal ObjNode As XMLParser.C_COMPLEX_OBJECT, ByVal a_filemanager As FileManagerLocal)
        Dim an_attribute As XMLParser.C_ATTRIBUTE

        Select Case ObjNode.rm_type_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
            Case "event"
                EventType = ObservationEventType.Event
            Case "point_event"
                EventType = ObservationEventType.PointInTime
            Case "interval_event"
                EventType = ObservationEventType.Interval
        End Select

        cOccurrences = ArchetypeEditor.XML_Classes.XML_Tools.SetOccurrences(ObjNode.occurrences)

        For Each an_attribute In ObjNode.attributes

            Select Case an_attribute.rm_attribute_name.ToLowerInvariant()
                Case "name"
                    mRunTimeConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                Case "offset"
                    Try
                        Dim d As Duration = _
                            ArchetypeEditor.XML_Classes.XML_Tools.GetDuration(an_attribute)
                        Me.Offset = d.GUI_duration
                        Me.OffsetUnits = d.ISO_Units
                    Catch e As Exception
                        MessageBox.Show(String.Format("Error: Event[{0}]/offset attribute - {1}", Me.NodeId, e.Message))
                    End Try

                Case "width"
                    Debug.Assert(mType = StructureType.IntervalEvent)

                    Try
                        Dim d As Duration = _
                            ArchetypeEditor.XML_Classes.XML_Tools.GetDuration(an_attribute)
                        Me.Width = d.GUI_duration
                        Me.WidthUnits = d.ISO_Units
                    Catch e As Exception
                        MessageBox.Show(String.Format("Error: Event[{1}]/width attribute - {0}", Me.NodeId, e.Message))
                    End Try

                Case "math_function"
                    Debug.Assert(mType = StructureType.IntervalEvent)

                    Dim textConstraint As Constraint_Text = _
                    ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(CType(an_attribute.children(0), XMLParser.C_COMPLEX_OBJECT))
                    Me.sMath = textConstraint.AllowableValues.FirstCode ' only one allowed

                Case "data", "item" 'item is OBSOLETE
                    ' return the data for processing
                    mXML_Data = an_attribute

                Case "state"
                    ' return the state for processing
                    mXML_State = an_attribute
            End Select

        Next


        If Not Me.XML_Data Is Nothing AndAlso Not Me.XML_Data.children Is Nothing AndAlso Me.XML_Data.children.Length > 0 Then
            Dim cadlStruct As XMLParser.C_COMPLEX_OBJECT

            Dim generating_type As String = Me.XML_Data.children(0).GetType.ToString
            Select Case generating_type
                Case "XMLParser.ARCHETYPE_INTERNAL_REF"
                    ' Place holder for different structures at different events 
                    ' not available as yet
                Case "XMLParser.C_COMPLEX_OBJECT"
                    cadlStruct = CType(Me.XML_Data.children(0), XMLParser.C_COMPLEX_OBJECT)
                    Dim structure_type As StructureType

                    structure_type = ReferenceModel.StructureTypeFromString(cadlStruct.rm_type_name)

                    Debug.Assert(structure_type <> StructureType.Not_Set)

                    If structure_type = StructureType.Table Then
                        ArchetypeEditor.XML_Classes.XML_Tools.LastProcessedStructure = New RmTable(cadlStruct, a_filemanager)
                    Else
                        ArchetypeEditor.XML_Classes.XML_Tools.LastProcessedStructure = New RmStructureCompound(cadlStruct, a_filemanager)
                    End If
            End Select
        End If

        If Not Me.XML_State Is Nothing AndAlso Not Me.XML_State.children Is Nothing AndAlso Me.XML_State.children.Length > 0 Then
            Dim generating_type As String = Me.XML_State.children(0).GetType.ToString

            Select Case generating_type
                Case "XMLParser.ARCHETYPE_INTERNAL_REF"
                    ' Place holder for different structures at different events 
                    ' not available as yet
                Case "XMLParser.C_COMPLEX_OBJECT"
                    ArchetypeEditor.XML_Classes.XML_Tools.StateStructure = New RmStructureCompound(Me.XML_State, StructureType.State, a_filemanager)
                Case "XMLParser.ARCHETYPE_SLOT"
                    ArchetypeEditor.XML_Classes.XML_Tools.StateStructure = New RmStructureCompound(Me.XML_State, StructureType.State, a_filemanager)
            End Select
        End If

    End Sub

#End Region

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
'The Original Code is RmEvent.vb.
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
