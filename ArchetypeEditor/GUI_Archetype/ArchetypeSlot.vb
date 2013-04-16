Option Strict On

Public Class ArchetypeSlot
    Inherits ArchetypeNodeAbstract

    Private Property Slot() As RmSlot
        Get
            Return CType(Item, RmSlot)
        End Get
        Set(ByVal Value As RmSlot)
            Item = Value
        End Set
    End Property

    Public Overrides Property Constraint() As Constraint
        Get
            Return Slot.SlotConstraint
        End Get
        Set(ByVal Value As Constraint)
            Slot.SlotConstraint = CType(Value, Constraint_Slot)
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Dim s As String = mFileManager.OntologyManager.GetOpenEHRTerm(Slot.SlotConstraint.RM_ClassType, Slot.SlotConstraint.RM_ClassType.ToString)
            Return String.Format("{0} [{1}]", MyBase.Text, s)
        End Get
        Set(ByVal value As String)
            Dim i As Integer = value.IndexOf("["c)

            If i > 0 Then
                If (i > 1) And value.Substring(i - 1, 1) = " " Then
                    i = i - 1
                End If

                MyBase.Text = value.Substring(0, i)
            Else
                MyBase.Text = value
            End If
        End Set
    End Property

    Public Overrides Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String
        Dim s As New System.Text.StringBuilder()
        Dim slot_constraint As Constraint_Slot

        Try
            slot_constraint = Slot.SlotConstraint
        Catch ex As Exception
            Return ""
        End Try

        s.Append("<tr><td><table><tr><td width=""")
        s.Append((level * 20).ToString)
        s.Append("""></td><td>")

        s.Append("<img border=""0"" src=""Images/slot.gif"" width=""32"" height=""32"" align=""middle"">")
        s.AppendLine("</td></tr></table></td>")

        's &= Environment.NewLine & "<tr>"
        s.AppendFormat("<td>{0}<br>{1}</td>", Filemanager.GetOpenEhrTerm(312, "Slot"), Me.Text)
        s.AppendLine()

        Dim include_label As String
        Dim exclude_label As String

        If slot_constraint.RM_ClassType = StructureType.SECTION Then
            include_label = Filemanager.GetOpenEhrTerm(172, "Include sections")
            exclude_label = Filemanager.GetOpenEhrTerm(173, "Exclude sections")
        ElseIf slot_constraint.RM_ClassType = StructureType.ENTRY Then
            include_label = Filemanager.GetOpenEhrTerm(175, "Include entries")
            exclude_label = Filemanager.GetOpenEhrTerm(176, "Exclude entries")
        Else
            include_label = Filemanager.GetOpenEhrTerm(625, "Include") & " : " & slot_constraint.RM_ClassType.ToString
            exclude_label = Filemanager.GetOpenEhrTerm(626, "Exclude") & " : " & slot_constraint.RM_ClassType.ToString
        End If

        include_label &= "<br>"
        exclude_label &= "<br>"

        s.AppendFormat("<td>{0}", include_label)
        If slot_constraint.Include.Count > 0 Then
            If slot_constraint.IncludeAll Then
                s.Append(Filemanager.GetOpenEhrTerm(11, "Allow all"))
            Else
                For Each statement As String In slot_constraint.Include
                    s.AppendFormat("{0}{1}<br>", Environment.NewLine, statement)
                Next

            End If
        End If
        s.AppendLine("</td>")

        s.AppendFormat("<td>{0}", exclude_label)
        If slot_constraint.Exclude.Count > 0 Then
            If slot_constraint.ExcludeAll Then
                s.Append(Filemanager.GetOpenEhrTerm(11, "Allow all"))
            Else
                For Each statement As String In slot_constraint.Exclude
                    s.AppendFormat("{0}{1}<br>", Environment.NewLine, statement)
                Next

            End If
        End If
        s.Append("</td>")
        If Main.Instance.Options.ShowCommentsInHtml Then
            s.Append("<td>&nbsp;</td>")
        End If
        s.Append("</tr>")
        Return s.ToString()

    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        Dim s, statement As String
        Dim slot_constraint As Constraint_Slot

        Try
            slot_constraint = Slot.SlotConstraint
        Catch ex As Exception
            Return ""
        End Try

        s = Space(3 * level) & slot_constraint.RM_ClassType.ToString & ":\par"
        If slot_constraint.IncludeAll Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Include ALL\par"
        ElseIf slot_constraint.Include.Count > 0 Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Include:\par"
            For Each statement In slot_constraint.Include
                s &= Environment.NewLine & Space(3 * (level + 2)) & statement & "\par"
            Next
        End If

        If slot_constraint.ExcludeAll Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Exclude ALL\par"
        ElseIf slot_constraint.Exclude.Count > 0 Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Exclude:\par"
            For Each statement In slot_constraint.Exclude
                s &= Environment.NewLine & Space(3 * (level + 2)) & statement & "\par"
            Next
        End If

        Return s
    End Function

    Public Overrides Function Copy() As ArchetypeNode
        Return New ArchetypeSlot(CType(Slot.Copy, RmSlot), mFileManager)
    End Function

    Sub New(ByVal slot As RmSlot, ByVal fileManager As FileManagerLocal)
        MyBase.New(slot, fileManager)
    End Sub

    Sub New(ByVal anonymousSlot As ArchetypeNodeAnonymous, ByVal fileManager As FileManagerLocal)
        MyBase.New(anonymousSlot.RM_Class, fileManager)
        Slot.NodeId = fileManager.OntologyManager.AddTerm(ReferenceModel.RM_StructureName(Slot.SlotConstraint.RM_ClassType)).Code
    End Sub

    Sub New(ByVal text As String, ByVal slotClass As StructureType, ByVal fileManager As FileManagerLocal)
        MyBase.New(New RmSlot(slotClass), fileManager)
        Slot.NodeId = fileManager.OntologyManager.AddTerm(text).Code
    End Sub

End Class
