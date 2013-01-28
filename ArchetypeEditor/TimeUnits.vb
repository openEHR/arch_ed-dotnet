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

    Private Enum ValidIsoUnits
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

    Public Function IsValidIsoUnit(ByVal unit As String) As Boolean
        Return Array.IndexOf(System.Enum.GetNames(GetType(ValidIsoUnits)), unit) > -1
    End Function

    Public Function GetIsoUnitForDuration(ByVal duration As String) As String
        Dim result As String = ""

        If Not String.IsNullOrEmpty(duration) Then
            Select Case duration.ToLowerInvariant
                Case "y"
                    result = "a"
                Case "m"
                    result = "mo"
                Case "th"
                    result = "h"
                Case "tm"
                    result = "min"
                Case "w"
                    result = "wk"
                Case "ts"
                    result = "s"
                Case Else
                    result = duration.ToLowerInvariant
            End Select
        End If

        Return result
    End Function

    Public Function GetValidIsoUnit(ByVal unit As String) As String
        Select Case unit.ToLowerInvariant
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

    Public Function GetLanguageForISO(ByVal isoUnit As String) As String
        If Not mIsInitialised Then
            Initialise()
        End If

        'yr is an alternative iso unit
        If isoUnit = "yr" Then
            isoUnit = "a"
        End If

        For Each tu As TimeUnit In mTimeUnitArray
            If tu.ISOunit = isoUnit Then
                Return tu.LanguageString
            End If
        Next

        Debug.Assert(False, "ISO unit not found: " & isoUnit)
        Return ""
    End Function

    Public Function GetOptimalIsoUnit(ByVal unit As String) As String
        If Not mIsInitialised Then
            Initialise()
        End If

        If IsValidIsoUnit(unit) Then
            If unit = "yr" Then
                Return "a"
            Else
                Return unit
            End If
        Else
            Dim s As String = Me.GetValidIsoUnit(unit)

            If s <> "" Then
                Return s
            Else
                s = Me.GetISOForLanguage(unit)

                If s <> "" Then
                    Return s
                End If
            End If
        End If

        Return unit  'unable to standardise this
    End Function

    Public Function GetISOForLanguage(ByVal languageUnit As String) As String
        If Not mIsInitialised Then
            Initialise()
        End If

        For Each tu As TimeUnit In mTimeUnitArray
            If tu.LanguageString = languageUnit Then
                Return tu.ISOunit
            End If
        Next

        Debug.Assert(False, "Language unit not found: " & languageUnit)
        Return ""
    End Function

    Private Sub Initialise()
        For Each name As String In System.Enum.GetNames(GetType(ValidIsoUnits))
            If name <> "yr" Then
                Dim tu As New TimeUnit
                tu.ISOunit = name
                tu.LanguageString = Filemanager.GetOpenEhrTerm(System.Enum.Parse(GetType(ValidIsoUnits), name), name)
                mTimeUnitArray.Add(tu)
            End If
        Next

        mIsInitialised = True
    End Sub

End Class
