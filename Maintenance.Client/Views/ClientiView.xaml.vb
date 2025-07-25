Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports MaterialDesignThemes.Wpf
Imports Maintenance.Shared.Models
Imports Maintenance.Client.ViewModels

Namespace Maintenance.Client.Views
    Public Partial Class ClientiView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As ClientiViewModel
            Get
                Return TryCast(DataContext, ClientiViewModel)
            End Get
        End Property

        Private Sub OnAdd(sender As Object, e As RoutedEventArgs)
            Dim nuovo As New Cliente()
            ShowDialog(nuovo, Sub()
                                   Vm.AddCliente(nuovo)
                               End Sub)
        End Sub

        Private Sub OnEdit(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedCliente Is Nothing Then Return
            Dim clone = New Cliente() With {
                .Id = Vm.SelectedCliente.Id,
                .Nome = Vm.SelectedCliente.Nome,
                .Indirizzo = Vm.SelectedCliente.Indirizzo
            }
            ShowDialog(clone, Sub()
                                   Vm.SelectedCliente.Nome = clone.Nome
                                   Vm.SelectedCliente.Indirizzo = clone.Indirizzo
                                   Vm.UpdateCliente(Vm.SelectedCliente)
                               End Sub)
        End Sub

        Private Sub OnDelete(sender As Object, e As RoutedEventArgs)
            Vm.DeleteSelected()
        End Sub

        Private Sub ShowDialog(model As Cliente, onSave As Action)
            Dim panel As New StackPanel()
            Dim nameBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            nameBox.SetBinding(TextBox.TextProperty, New Binding("Nome") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            Dim addressBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            addressBox.SetBinding(TextBox.TextProperty, New Binding("Indirizzo") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(New TextBlock() With {.Text = "Nome"})
            panel.Children.Add(nameBox)
            panel.Children.Add(New TextBlock() With {.Text = "Indirizzo"})
            panel.Children.Add(addressBox)
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
