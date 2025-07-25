Imports System.Windows
Imports System.Windows.Controls
Imports MaterialDesignThemes.Wpf
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class OperatoriView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As OperatoriViewModel
            Get
                Return TryCast(DataContext, OperatoriViewModel)
            End Get
        End Property

        Private Sub OnAdd(sender As Object, e As RoutedEventArgs)
            Dim nuovo As New Operatore()
            ShowDialog(nuovo, Sub()
                                   Vm.AddOperatore(nuovo)
                               End Sub)
        End Sub

        Private Sub OnEdit(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedOperatore Is Nothing Then Return
            Dim clone As New Operatore() With {.Id = Vm.SelectedOperatore.Id, .Nome = Vm.SelectedOperatore.Nome}
            clone.OperatoreSkills = Vm.SelectedOperatore.OperatoreSkills?.Select(Function(os) New OperatoreSkill With {.OperatoreId = os.OperatoreId, .SkillId = os.SkillId}).ToList()
            ShowDialog(clone, Sub()
                                   Vm.SelectedOperatore.Nome = clone.Nome
                                   Vm.SelectedOperatore.OperatoreSkills = clone.OperatoreSkills
                                   Vm.UpdateOperatore(Vm.SelectedOperatore)
                               End Sub)
        End Sub

        Private Sub OnDelete(sender As Object, e As RoutedEventArgs)
            Vm.DeleteSelected()
        End Sub

        Private Sub ShowDialog(model As Operatore, onSave As Action)
            Dim dialog As New OperatoreDetailDialog(model, Vm.Skills)
            AddHandler dialog.Saved, Sub()
                                          onSave()
                                          DialogHost.CloseDialogCommand.Execute(Nothing, Host)
                                      End Sub
            DialogHost.Show(dialog, "Host")
        End Sub
    End Class
End Namespace
