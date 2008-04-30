Public Class RmParticipation
    Inherits RmStructure

    Overrides ReadOnly Property Type() As StructureType
        Get
            Return StructureType.Participation
        End Get
    End Property

    Private mModeSet As New CodePhrase

    Public Property ModeSet() As CodePhrase
        Get
            Return mModeSet
        End Get
        Set(ByVal value As CodePhrase)
            mModeSet = value
        End Set
    End Property

    Private mTextConstraint As New Constraint_Text

    Public Property FunctionConstraint() As Constraint_Text
        Get
            Return mTextConstraint
        End Get
        Set(ByVal value As Constraint_Text)
            mTextConstraint = value
        End Set
    End Property

    Private mMandateDateTime As Boolean

    Public Property MandatoryDateTime() As Boolean
        Get
            Return mMandateDateTime
        End Get
        Set(ByVal value As Boolean)
            mMandateDateTime = value
        End Set
    End Property


    Overrides Function Copy() As RmStructure
        Dim rm As New RmParticipation()
        rm.FunctionConstraint = mTextConstraint.Copy()
        rm.MandatoryDateTime = MandatoryDateTime
        rm.ModeSet = ModeSet.Copy()
        rm.Occurrences = Occurrences.Copy()
        Return rm
    End Function

    Sub New()
        MyBase.New("", StructureType.Participation)
    End Sub

#Region "ADL and XML oriented features"

    Sub New(ByVal a_participation As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(a_participation)

        Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE

        For i As Integer = 1 To a_participation.attributes.count
            attribute = a_participation.attributes.i_th(i)
            Select Case attribute.rm_attribute_name.to_cil.ToLowerInvariant
                Case "mode"
                    Dim modeConstraint As Constraint_Text = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(attribute.children.first)
                    If Not modeConstraint Is Nothing AndAlso modeConstraint.TypeOfTextConstraint = TextConstrainType.Internal Then
                        mModeSet = modeConstraint.AllowableValues
                    End If
                Case "function"
                    mTextConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(attribute.children.first)
                Case "time"
                    mMandateDateTime = True
            End Select
        Next

    End Sub
    Sub New(ByVal a_participation As XMLParser.C_COMPLEX_OBJECT)
        MyBase.New(a_participation)

        For Each attribute As XMLParser.C_ATTRIBUTE In a_participation.attributes
            Select Case attribute.rm_attribute_name.ToLowerInvariant
                Case "mode"
                    Dim modeConstraint As Constraint_Text = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(attribute.children(0))
                    If Not modeConstraint Is Nothing AndAlso modeConstraint.TypeOfTextConstraint = TextConstrainType.Internal Then
                        mModeSet = modeConstraint.AllowableValues
                    End If
                Case "function"
                    mTextConstraint = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(attribute.children(0))
                Case "time"
                    mMandateDateTime = True
            End Select
        Next
    End Sub
#End Region



End Class
