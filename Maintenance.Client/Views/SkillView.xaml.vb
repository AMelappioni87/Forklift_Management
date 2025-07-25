Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports MaterialDesignThemes.Wpf
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class SkillView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As SkillViewModel
            Get
                Return TryCast(DataContext, SkillViewModel)
            End Get
        End Property

        Private Sub OnAdd(sender As Object, e As RoutedEventArgs)
            Dim nuovo As New Skill()
            ShowDialog(nuovo, Sub()
                                   Vm.AddSkill(nuovo)
                               End Sub)
        End Sub

        Private Sub OnEdit(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedSkill Is Nothing Then Return
            Dim clone As New Skill() With {.Id = Vm.SelectedSkill.Id, .Nome = Vm.SelectedSkill.Nome}
            ShowDialog(clone, Sub()
                                   Vm.SelectedSkill.Nome = clone.Nome
                                   Vm.UpdateSkill(Vm.SelectedSkill)
                               End Sub)
        End Sub

        Private Sub OnDelete(sender As Object, e As RoutedEventArgs)
            Vm.DeleteSelected()
        End Sub

        Private Sub ShowDialog(model As Skill, onSave As Action)
            Dim panel As New StackPanel()
            panel.Children.Add(New TextBlock() With {.Text = "Nome"})
            Dim nameBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            nameBox.SetBinding(TextBox.TextProperty, New Binding("Nome") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(nameBox)
            Dim btn As New Button() With {.Content = "Salva", .Margin = New Thickness(0,8,0,0)}
            AddHandler btn.Click, Sub()
                                       onSave()
                                       DialogHost.CloseDialogCommand.Execute(Nothing, Host)
                                   End Sub
            panel.Children.Add(btn)
            DialogHost.Show(panel, "Host")
        End Sub
    End Class
End Namespace
