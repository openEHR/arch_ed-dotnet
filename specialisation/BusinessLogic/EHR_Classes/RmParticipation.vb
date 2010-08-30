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

    Sub New(ByVal a_participation As AdlParser.CComplexObject)
        MyBase.New(a_participation)

        For i As Integer = 1 To a_participation.Attributes.Count
            Dim attribute As AdlParser.CAttribute = a_participation.Attributes.ITh(i)

            If attribute.HasChildren Then
                Select Case attribute.RmAttributeName.ToCil.ToLowerInvariant
                    Case "mode"
                        Dim modeConstraint As Constraint_Text = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(attribute.Children.First)

                        If Not modeConstraint Is Nothing AndAlso modeConstraint.TypeOfTextConstraint = TextConstrainType.Internal Then
                            mModeSet = modeConstraint.AllowableValues
                        End If
                    Case "function"
                        mTextConstraint = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(attribute.Children.First)
                    Case "time"
                        mMandateDateTime = True
                End Select
            End If
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

End Class
