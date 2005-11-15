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

Option Explicit On 

Class RmEvent
    Inherits RmStructure

    Dim iOffset As Integer
    Dim sOffset As String
    Dim sMath As String
    Dim iWidth As Long = 1
    Dim sWidth As String
    Dim boolSignNeg, boolFixedDuration, boolFixedOffset As Boolean
    Dim boolPointInTime As Boolean = True
    Dim sData As String

    Public Shadows ReadOnly Property TypeName() As String
        Get
            Return "Event"
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
    Public Property isPointInTime() As Boolean
        Get
            Return boolPointInTime
        End Get
        Set(ByVal Value As Boolean)
            boolPointInTime = Value
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
    Property isSignNegative() As Boolean
        Get
            Return boolSignNeg
        End Get
        Set(ByVal Value As Boolean)
            boolSignNeg = Value
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
        Dim ae As New RmEvent(Me.NodeId)
        ae.cOccurrences = Me.cOccurrences
        ae.sNodeId = Me.sNodeId
        ae.mRunTimeConstraint = Me.mRunTimeConstraint
        ae.iOffset = Me.iOffset
        ae.sOffset = Me.sOffset
        ae.iWidth = Me.iWidth
        ae.sWidth = Me.sWidth
        ae.boolPointInTime = Me.boolPointInTime
        ae.boolSignNeg = Me.boolSignNeg
        ae.boolFixedDuration = Me.boolFixedDuration
        ae.boolFixedOffset = ae.boolFixedOffset
        Return ae
    End Function

    Sub New(ByVal NodeId As String)
        MyBase.New(NodeId, StructureType.Event)
    End Sub

#Region "ADL Oriented Features"

    Sub New(ByVal An_Event As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(An_Event.node_id.to_cil, StructureType.Event)
        ProcessEvent(An_Event)
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

#Region "ADL PRocessing - incoming"

    Private Sub ProcessEvent(ByVal ObjNode As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        Dim an_attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE
        Dim i As Integer

        cOccurrences = ADL_Tools.Instance.SetOccurrences(ObjNode.occurrences)

        For i = 1 To ObjNode.Attributes.Count

            an_attribute = ObjNode.Attributes.i_th(i)

            Select Case an_attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InstalledUICulture)
                Case "name", "runtime_label" ' runtime_label is redundant
                    mRuntimeConstraint = RmElement.ProcessText(CType(an_attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT))
                Case "offset"
                    Dim d As New Duration
                    Dim offset As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    offset = an_attribute.children.first
                    d.ISO_duration = offset.item.as_string.to_cil
                    Me.Offset = d.GUI_duration
                    Me.OffsetUnits = d.GUI_Units

                Case "width"
                    Dim d As New Duration
                    Dim width As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    boolPointInTime = False
                    width = an_attribute.children.first
                    d.ISO_duration = width.item.as_string.to_cil
                    Me.Width = d.GUI_duration
                    Me.WidthUnits = d.GUI_Units

                Case "aggregate_math_function"
                    Dim MathFunc As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    boolPointInTime = False
                    MathFunc = an_attribute.children.first
                    Me.sMath = MathFunc.item.as_string.to_cil.Trim("""")

                Case "display_as_positive"
                    Dim DisplayPos As openehr.openehr.am.archetype.constraint_model.C_PRIMITIVE_OBJECT

                    DisplayPos = an_attribute.children.first
                    boolSignNeg = (DisplayPos.item.as_string.to_cil = "True")

                Case "data"
                    ' return the data for processing
                    mEIF_Data = an_attribute

            End Select

        Next


        If Not Me.ADL_Data Is Nothing Then
            Dim cadlStruct As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT

            Select Case Me.ADL_Data.Children.First.Generating_Type.to_cil
                Case "ARCHETYPE_INTERNAL_REF"
                    ' Place holder for different structures at different events 
                    ' not available as yet
                Case "C_COMPLEX_OBJECT"
                    cadlStruct = CType(Me.ADL_Data.Children.First, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
                    Dim structure_type As StructureType

                    structure_type = ReferenceModel.Instance.StructureTypeFromString(cadlStruct.Rm_Type_Name.to_cil)

                    Debug.Assert(structure_type <> StructureType.Not_Set)

                    If structure_type = StructureType.Table Then
                        ADL_Tools.Instance.LastProcessedStructure = New RmTable(cadlStruct)
                    Else
                        ADL_Tools.Instance.LastProcessedStructure = New RmStructureCompound(cadlStruct)
                    End If
            End Select
        End If

    End Sub

#End Region
#Region "ADL Build - outgoing"

#End Region
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
