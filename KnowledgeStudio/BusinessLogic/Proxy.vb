Option Strict On
Option Explicit On

Public MustInherit Class C_OBJECT_PROXY
    Public MustOverride ReadOnly Property Any_Allowed() As Boolean

    Private mConstraintObject As XMLParser.C_OBJECT

    Public Sub New(ByVal ConstraintObject As XMLParser.C_OBJECT)
        If ConstraintObject Is Nothing Then
            Throw New ArgumentNullException("ConstraintObject")
        End If

        mConstraintObject = ConstraintObject
    End Sub

    Public ReadOnly Property ConstraintObject() As XMLParser.C_OBJECT
        Get
            Return mConstraintObject
        End Get
    End Property
End Class


Public Class C_COMPLEX_OBJECT_PROXY
    Inherits C_OBJECT_PROXY

    Private mComplexObject As XMLParser.C_COMPLEX_OBJECT

    Public Sub New(ByVal ComplexObject As XMLParser.C_COMPLEX_OBJECT)
        MyBase.New(ComplexObject)
        mComplexObject = ComplexObject
    End Sub

    Public Overrides ReadOnly Property Any_Allowed() As Boolean 'Result = attributes.isempty
        Get
            If mComplexObject.attributes Is Nothing Then
                Return True
            Else
                Return (mComplexObject.attributes.Length = 0)
            End If
        End Get
    End Property

    Public Shadows ReadOnly Property ConstraintObject() As XMLParser.C_COMPLEX_OBJECT
        Get
            Return CType(MyBase.ConstraintObject, XMLParser.C_COMPLEX_OBJECT)
        End Get
    End Property
End Class


Public Class C_PRIMITIVE_OBJECT_PROXY
    Inherits C_OBJECT_PROXY

    Private mPrimitiveObject As XMLParser.C_PRIMITIVE_OBJECT

    Public Sub New(ByVal PrimitiveObject As XMLParser.C_PRIMITIVE_OBJECT)
        MyBase.New(PrimitiveObject)
        mPrimitiveObject = PrimitiveObject
    End Sub

    Public Overrides ReadOnly Property Any_Allowed() As Boolean
        Get
            Return (mPrimitiveObject.item Is Nothing)           
        End Get
    End Property

    Public Shadows ReadOnly Property ConstraintObject() As XMLParser.C_PRIMITIVE_OBJECT
        Get
            Return CType(MyBase.ConstraintObject, XMLParser.C_PRIMITIVE_OBJECT)
        End Get
    End Property
End Class


Public Class C_DV_QUANTITY_PROXY

    Private mDvQuantity As XMLParser.C_DV_QUANTITY

    Public Sub New(ByVal DvQuantity As XMLParser.C_DV_QUANTITY)
        If DvQuantity Is Nothing Then
            Throw New ArgumentNullException("ConstraintObject")
        End If

        mDvQuantity = DvQuantity
    End Sub

    Public ReadOnly Property Any_Allowed() As Boolean
        Get
            'mDvQuantity.list
            Dim listLength As Integer = 0
            If Not mDvQuantity.list Is Nothing Then listLength = mDvQuantity.list.Length
            Return (listLength = 0 And mDvQuantity.property Is Nothing)
        End Get
    End Property

    Public ReadOnly Property ConstraintObject() As XMLParser.C_DV_QUANTITY
        Get
            Return mDvQuantity
        End Get
    End Property
End Class