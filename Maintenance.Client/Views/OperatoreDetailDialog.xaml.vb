Imports System.Windows
Imports System.Windows.Controls
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class OperatoreDetailDialog
        Inherits UserControl

        Public Event Saved()

        Public Property Operatore As Operatore
        Public Property Skills As List(Of Skill)

        Public Sub New(model As Operatore, available As IEnumerable(Of Skill))
            InitializeComponent()
            Operatore = model
            Skills = available.ToList()
            DataContext = Me
            If Operatore.OperatoreSkills IsNot Nothing Then
                For Each os In Operatore.OperatoreSkills
                    Dim skill = Skills.FirstOrDefault(Function(s) s.Id = os.SkillId)
                    If skill IsNot Nothing Then
                        SkillsList.SelectedItems.Add(skill)
                    End If
                Next
            End If
        End Sub

        Private Sub OnSave(sender As Object, e As RoutedEventArgs)
            Operatore.OperatoreSkills = New List(Of OperatoreSkill)()
            For Each s As Skill In SkillsList.SelectedItems
                Operatore.OperatoreSkills.Add(New OperatoreSkill With {.OperatoreId = Operatore.Id, .SkillId = s.Id})
            Next
            RaiseEvent Saved()
        End Sub
    End Class
End Namespace
