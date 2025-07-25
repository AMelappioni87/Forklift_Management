Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports MaterialDesignThemes.Wpf
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class TicketListView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As TicketListViewModel
            Get
                Return TryCast(DataContext, TicketListViewModel)
            End Get
        End Property

        Private Sub OnAdd(sender As Object, e As RoutedEventArgs)
            Dim nuovo As New Ticket() With {.DataRichiesta = Date.Now, .Stato = TicketStatus.Aperto}
            ShowDialog(nuovo, Sub() Vm.AddTicket(nuovo))
        End Sub

        Private Sub OnEdit(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedTicket Is Nothing Then Return
            Dim clone As New Ticket With {
                .Id = Vm.SelectedTicket.Id,
                .Numero = Vm.SelectedTicket.Numero,
                .Titolo = Vm.SelectedTicket.Titolo,
                .Descrizione = Vm.SelectedTicket.Descrizione,
                .Tipo = Vm.SelectedTicket.Tipo,
                .Priorita = Vm.SelectedTicket.Priorita,
                .DataRichiesta = Vm.SelectedTicket.DataRichiesta,
                .DataApertura = Vm.SelectedTicket.DataApertura,
                .DataChiusura = Vm.SelectedTicket.DataChiusura,
                .Stato = Vm.SelectedTicket.Stato,
                .ClienteId = Vm.SelectedTicket.ClienteId,
                .CarrelloId = Vm.SelectedTicket.CarrelloId,
                .TecnicoId = Vm.SelectedTicket.TecnicoId,
                .Allegati = If(Vm.SelectedTicket.Allegati?.ToList(), New List(Of AllegatoTicket))
            }
            ShowDialog(clone, Sub()
                                   Vm.SelectedTicket.Numero = clone.Numero
                                   Vm.SelectedTicket.Titolo = clone.Titolo
                                   Vm.SelectedTicket.Descrizione = clone.Descrizione
                                   Vm.SelectedTicket.Tipo = clone.Tipo
                                   Vm.SelectedTicket.Priorita = clone.Priorita
                                   Vm.SelectedTicket.DataRichiesta = clone.DataRichiesta
                                   Vm.SelectedTicket.Stato = clone.Stato
                                   Vm.SelectedTicket.ClienteId = clone.ClienteId
                                   Vm.SelectedTicket.CarrelloId = clone.CarrelloId
                                   Vm.SelectedTicket.TecnicoId = clone.TecnicoId
                                   Vm.SelectedTicket.Allegati = clone.Allegati
                                   Vm.UpdateTicket(Vm.SelectedTicket)
                               End Sub)
        End Sub

        Private Sub OnDelete(sender As Object, e As RoutedEventArgs)
            Vm.DeleteSelected()
        End Sub

        Private Sub ShowDialog(model As Ticket, onSave As Action)
            Dim dlg As New TicketDetailDialog(model, Vm.Clienti, Vm.Carrelli)
            AddHandler dlg.Saved, Sub()
                                       onSave()
                                       DialogHost.CloseDialogCommand.Execute(Nothing, Host)
                                   End Sub
            DialogHost.Show(dlg, "Host")
        End Sub
    End Class
End Namespace
