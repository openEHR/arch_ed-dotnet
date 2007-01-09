Public Class TimeUnits

    Public Class TimeUnit
        Public LanguageString As String
        Public ISOunit As String
        Overrides Function ToString() As String
            Return LanguageString
        End Function
    End Class

    Shared mTimeUnitArray As New ArrayList
    Private mIsInitialised As Boolean = False

    Public Function IsoTimeUnits() As TimeUnit()
        If Not mIsInitialised Then
            Initialise()
        End If
        Return mTimeUnitArray.ToArray(GetType(TimeUnit))
    End Function

    Private Enum mTimeUnits
        microsec = 612
        millisec = 613
        s = 614
        min = 615
        h = 616
        d = 617
        wk = 620
        mo = 618
        a = 619
        yr = 619
    End Enum

    Public Function IsValidIsoUnit(ByVal a_unit As String) As Boolean
        Return Array.IndexOf(System.Enum.GetNames(GetType(mTimeUnits)), a_unit) > -1
    End Function

    Public Function GetValidIsoUnit(ByVal a_unit As String) As String
        Select Case a_unit.ToLowerInvariant
            Case "yr", "year", "y"
                Return "a"
            Case "mth", "month"
                Return "mo"
            Case "w", "week"
                Return "wk"
            Case "day"
                Return "d"
            Case "mn"
                Return "min"
            Case "m"
                Return "min"
            Case "sec"
                Return "s"
            Case Else
                Return ""
        End Select
    End Function

    Public Function GetLanguageForISO(ByVal an_iso_unit As String) As String
        If Not mIsInitialised Then
            Initialise()
        End If

        'yr is an alternative iso unit
        If an_iso_unit = "yr" Then
            an_iso_unit = "a"
        End If

        For Each tu As TimeUnit In mTimeUnitArray
            If tu.ISOunit = an_iso_unit Then
                Return tu.LanguageString
            End If
        Next

        Debug.Assert(False, "ISO unit not found: " & an_iso_unit)
        Return ""
    End Function

    Public Function GetOptimalIsoUnit(ByVal a_unit As String) As String
        If Not mIsInitialised Then
            Initialise()
        End If

        If Me.IsValidIsoUnit(a_unit) Then
            If a_unit = "yr" Then
                Return "a"
            Else
                Return a_unit
            End If
        Else
            Dim s As String = Me.GetValidIsoUnit(a_unit)
            If s <> "" Then
                Return s
            Else
                s = Me.GetISOForLanguage(a_unit)
                If s <> "" Then
                    Return s
                End If
            End If
        End If
        Return a_unit  'unable to standardise this
    End Function
    Public Function GetISOForLanguage(ByVal a_language_unit As String) As String
        If Not mIsInitialised Then
            Initialise()
        End If

        For Each tu As TimeUnit In mTimeUnitArray
            If tu.LanguageString = a_language_unit Then
                Return tu.ISOunit
            End If
        Next
        Debug.Assert(False, "Language unit not found: " & a_language_unit)
        Return ""
    End Function

    Private Sub Initialise()
        For Each name As String In System.Enum.GetNames(GetType(mTimeUnits))
            If name <> "yr" Then
                Dim tu As New TimeUnit
                tu.ISOunit = name
                tu.LanguageString = Filemanager.GetOpenEhrTerm(System.Enum.Parse(GetType(mTimeUnits), name), name)
                mTimeUnitArray.Add(tu)
            End If
        Next
        mIsInitialised = True
    End Sub

End Class
