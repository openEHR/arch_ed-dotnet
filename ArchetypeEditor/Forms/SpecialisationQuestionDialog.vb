Imports System.Windows.Forms

Public Class SpecialisationQuestionDialog

    Protected Enum SpecialisationOption
        Yes = 0
        YesAndClone = 1
        No = 2
    End Enum

    Protected selection As SpecialisationOption

    Public Sub ShowForArchetypeNode(ByVal itemText As String, ByVal struct As RmStructure, ByVal archetypeSpecialisationDepth As Integer)
        Text = AE_Constants.Instance.MessageBoxCaption
        
        YesButton.Text = AE_Constants.Instance.SpecialiseYes
        YesButton.Enabled = struct.SpecialisationDepth < archetypeSpecialisationDepth
        YesAndCloneByCopyButton.Text = AE_Constants.Instance.SpecialiseAndClone
        YesAndCloneByCopyButton.Enabled = struct.Occurrences.IsMultiple
        NoButton.Text = AE_Constants.Instance.SpecialiseNo
        QuestionLabel.Text = AE_Constants.Instance.SpecialisationQuestion(itemText)
        
        ShowDialog()
        
       
    End Sub

    Public ReadOnly Property IsSpecialisationRequested() As Boolean
        Get
            Return selection <> SpecialisationOption.No
        End Get
    End Property

  
    
     Public ReadOnly Property IsCloningRequested() As Boolean
        Get
            Return selection = SpecialisationOption.YesAndClone 
        End Get
    End Property

    Private Sub YesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YesButton.Click
        Selection = SpecialisationOption.Yes
        Close()
    End Sub

    
    Private Sub NoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoButton.Click
        Selection = SpecialisationOption.No
        Close()
    End Sub
    
   
   
     Sub YesandCloneByCopyButtonClick(sender As Object, e As EventArgs)
    	 Selection = SpecialisationOption.YesAndClone
        Close()
    End Sub	
   
End Class
