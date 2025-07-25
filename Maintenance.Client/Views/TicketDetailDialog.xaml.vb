Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Win32
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class TicketDetailDialog
        Inherits UserControl

        Public Event Saved()

        Public Property Ticket As Ticket
        Public Property Clienti As List(Of Cliente)
        Public Property Carrelli As List(Of Carrello)
        Public Property TipoOptions As List(Of String)
        Public Property PrioritaOptions As List(Of String)
        Public Property Allegati As ObservableCollection(Of AllegatoTicket)
        Public Property SelectedCliente As Cliente
        Public Property SelectedCarrello As Carrello
        Public Property SelectedAllegato As AllegatoTicket

        Public Sub New(model As Ticket, cls As IEnumerable(Of Cliente), cars As IEnumerable(Of Carrello))
            InitializeComponent()
            Ticket = model
            Clienti = cls.ToList()
            Carrelli = cars.ToList()
            TipoOptions = New List(Of String)({"Guasto", "Manutenzione"})
            PrioritaOptions = New List(Of String)({"Bassa", "Media", "Alta"})
            If Ticket.Allegati IsNot Nothing Then
                Allegati = New ObservableCollection(Of AllegatoTicket)(Ticket.Allegati)
            Else
                Allegati = New ObservableCollection(Of AllegatoTicket)()
            End If
            DataContext = Me
            If Ticket.Cliente IsNot Nothing Then SelectedCliente = Ticket.Cliente
            If Ticket.Carrello IsNot Nothing Then SelectedCarrello = Ticket.Carrello
        End Sub

        Private Sub OnAddAttachment(sender As Object, e As RoutedEventArgs)
            Dim dlg As New OpenFileDialog() With {.Filter = "Files|*.png;*.jpg;*.jpeg;*.pdf", .Multiselect = True}
            If dlg.ShowDialog() Then
                For Each f In dlg.FileNames
                    Dim a As New AllegatoTicket With {
                        .NomeFile = System.IO.Path.GetFileName(f),
                        .Contenuto = File.ReadAllBytes(f)
                    }
                    Allegati.Add(a)
                Next
            End If
        End Sub

        Private Sub OnRemoveAttachment(sender As Object, e As RoutedEventArgs)
            Dim btn = TryCast(sender, Button)
            Dim item = TryCast(btn?.CommandParameter, AllegatoTicket)
            If item IsNot Nothing Then
                Allegati.Remove(item)
            End If
        End Sub

        Private Sub OnSave(sender As Object, e As RoutedEventArgs)
            Ticket.Cliente = SelectedCliente
            If SelectedCliente IsNot Nothing Then Ticket.ClienteId = SelectedCliente.Id
            Ticket.Carrello = SelectedCarrello
            If SelectedCarrello IsNot Nothing Then Ticket.CarrelloId = SelectedCarrello.Id
            Ticket.Allegati = Allegati.ToList()
            RaiseEvent Saved()
        End Sub
    End Class
End Namespace
