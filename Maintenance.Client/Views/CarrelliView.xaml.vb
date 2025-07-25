Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports MaterialDesignThemes.Wpf
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class CarrelliView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As CarrelliViewModel
            Get
                Return TryCast(DataContext, CarrelliViewModel)
            End Get
        End Property

        Private Sub OnAdd(sender As Object, e As RoutedEventArgs)
            Dim nuovo As New Carrello()
            ShowDialog(nuovo, Sub()
                                   Vm.AddCarrello(nuovo)
                               End Sub)
        End Sub

        Private Sub OnEdit(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedCarrello Is Nothing Then Return
            Dim clone As New Carrello() With {
                .Id = Vm.SelectedCarrello.Id,
                .NumeroSerie = Vm.SelectedCarrello.NumeroSerie,
                .Costruttore = Vm.SelectedCarrello.Costruttore,
                .Modello = Vm.SelectedCarrello.Modello,
                .DataProssimaManutenzione = Vm.SelectedCarrello.DataProssimaManutenzione,
                .Stato = Vm.SelectedCarrello.Stato,
                .ClienteId = Vm.SelectedCarrello.ClienteId
            }
            ShowDialog(clone, Sub()
                                   Vm.SelectedCarrello.NumeroSerie = clone.NumeroSerie
                                   Vm.SelectedCarrello.Costruttore = clone.Costruttore
                                   Vm.SelectedCarrello.Modello = clone.Modello
                                   Vm.SelectedCarrello.DataProssimaManutenzione = clone.DataProssimaManutenzione
                                   Vm.SelectedCarrello.Stato = clone.Stato
                                   Vm.SelectedCarrello.ClienteId = clone.ClienteId
                                   Vm.UpdateCarrello(Vm.SelectedCarrello)
                               End Sub)
        End Sub

        Private Sub OnDelete(sender As Object, e As RoutedEventArgs)
            Vm.DeleteSelected()
        End Sub

        Private Sub OnDetail(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedCarrello Is Nothing Then Return
            Dim win As New CarrelloDetailWindow(Vm.SelectedCarrello)
            win.ShowDialog()
        End Sub

        Private Sub ShowDialog(model As Carrello, onSave As Action)
            Dim panel As New StackPanel()
            panel.Children.Add(New TextBlock() With {.Text = "Costruttore"})
            Dim costBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            costBox.SetBinding(TextBox.TextProperty, New Binding("Costruttore") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(costBox)
            panel.Children.Add(New TextBlock() With {.Text = "Modello"})
            Dim modBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            modBox.SetBinding(TextBox.TextProperty, New Binding("Modello") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(modBox)
            panel.Children.Add(New TextBlock() With {.Text = "Matricola"})
            Dim matBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            matBox.SetBinding(TextBox.TextProperty, New Binding("NumeroSerie") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(matBox)
            panel.Children.Add(New TextBlock() With {.Text = "Cliente"})
            Dim combo As New ComboBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8), .ItemsSource = Vm.Clienti, .DisplayMemberPath = "Nome"}
            combo.SetBinding(ComboBox.SelectedItemProperty, New Binding("Cliente") With {.Source = model})
            panel.Children.Add(combo)
            panel.Children.Add(New TextBlock() With {.Text = "Prossima manutenzione"})
            Dim dateBox As New DatePicker() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            dateBox.SetBinding(DatePicker.SelectedDateProperty, New Binding("DataProssimaManutenzione") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(dateBox)
            panel.Children.Add(New TextBlock() With {.Text = "Stato"})
            Dim statoBox As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            statoBox.SetBinding(TextBox.TextProperty, New Binding("Stato") With {.Source = model})
            panel.Children.Add(statoBox)
            Dim btn As New Button() With {.Content = "Salva", .Margin = New Thickness(0,8,0,0)}
            AddHandler btn.Click, Sub()
                                       If combo.SelectedItem IsNot Nothing Then
                                           model.ClienteId = CType(combo.SelectedItem, Cliente).Id
                                       End If
                                       onSave()
                                       DialogHost.CloseDialogCommand.Execute(Nothing, Host)
                                   End Sub
            panel.Children.Add(btn)
            DialogHost.Show(panel, "Host")
        End Sub
    End Class
End Namespace
