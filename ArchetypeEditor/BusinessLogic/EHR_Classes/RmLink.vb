Public Class RmLink
    Inherits RmStructure

    Private mMeaning As New Constraint_Text

    Public Property Meaning() As Constraint_Text
        Get
            Return mMeaning
        End Get
        Set(ByVal value As Constraint_Text)
            mMeaning = value
        End Set
    End Property

    Private mLinkType As New Constraint_Text

    Public Property LinkType() As Constraint_Text
        Get
            Return mLinkType
        End Get
        Set(ByVal value As Constraint_Text)
            mLinkType = value
        End Set
    End Property

    Private mTarget As New Constraint_URI()

    Public Property Target() As Constraint_URI
        Get
            mTarget.EhrUriOnly = True
            Return mTarget
        End Get
        Set(ByVal value As Constraint_URI)
            mTarget = value
            mTarget.EhrUriOnly = True
        End Set
    End Property

    Overrides ReadOnly Property Type() As StructureType
        Get
            Return StructureType.Link
        End Get
    End Property

    Public Overrides Function HasLinks() As Boolean
        Return False
    End Function

    Overrides Function Copy() As RmStructure
        Dim rm As New RmLink()
        rm.LinkType = mLinkType.Copy()
        rm.Meaning = mMeaning.Copy()
        rm.Target = mTarget.Copy()
        rm.Occurrences = Occurrences.Copy()
        Return rm
    End Function

    Public Function HasConstraint() As Boolean
        Return mLinkType.TypeOfTextConstraint <> TextConstraintType.Text OrElse _
        mMeaning.TypeOfTextConstraint <> TextConstraintType.Text OrElse _
        mTarget.RegularExpression <> String.Empty
    End Function


    Sub New()
        MyBase.New("", StructureType.Link)
    End Sub


#Region "ADL and XML oriented features"

    Sub New(ByVal XML_Element As XMLParser.C_COMPLEX_OBJECT)
        MyBase.New(XML_Element)
    End Sub

    Sub New(ByVal EIF_Element As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(EIF_Element)
    End Sub

#End Region

End Class
