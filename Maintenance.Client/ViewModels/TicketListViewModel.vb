Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class TicketListViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _tickets As ObservableCollection(Of Ticket)
        Private _filtered As ObservableCollection(Of Ticket)
        Private _operatori As ObservableCollection(Of Operatore)
        Private _clienti As ObservableCollection(Of Cliente)
        Private _carrelli As ObservableCollection(Of Carrello)

        Private _selectedStato As TicketStatus?
        Private _selectedPriorita As String
        Private _selectedTecnico As Operatore
        Private _dataDa As Date?
        Private _dataA As Date?
        Private _selected As Ticket

        Public ReadOnly Property Statuses As Array = [Enum].GetValues(GetType(TicketStatus))
        Public ReadOnly Property PrioritaOptions As String() = {"Bassa", "Media", "Alta"}

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadData()
        End Sub

        Private Sub LoadData()
            Try
                Tickets = New ObservableCollection(Of Ticket)(_service.GetTickets())
                Operatori = New ObservableCollection(Of Operatore)(_service.GetOperatori())
                Clienti = New ObservableCollection(Of Cliente)(_service.GetClienti())
                Carrelli = New ObservableCollection(Of Carrello)(_service.GetCarrelli())
                ApplyFilter()
            Catch ex As Exception
            End Try
        End Sub

        Public Property Tickets As ObservableCollection(Of Ticket)
            Get
                Return _tickets
            End Get
            Set(value As ObservableCollection(Of Ticket))
                If _tickets IsNot value Then
                    _tickets = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property FilteredTickets As ObservableCollection(Of Ticket)
            Get
                Return _filtered
            End Get
            Private Set(value As ObservableCollection(Of Ticket))
                If _filtered IsNot value Then
                    _filtered = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Operatori As ObservableCollection(Of Operatore)
            Get
                Return _operatori
            End Get
            Private Set(value As ObservableCollection(Of Operatore))
                If _operatori IsNot value Then
                    _operatori = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Clienti As ObservableCollection(Of Cliente)
            Get
                Return _clienti
            End Get
            Private Set(value As ObservableCollection(Of Cliente))
                If _clienti IsNot value Then
                    _clienti = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Carrelli As ObservableCollection(Of Carrello)
            Get
                Return _carrelli
            End Get
            Private Set(value As ObservableCollection(Of Carrello))
                If _carrelli IsNot value Then
                    _carrelli = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property SelectedStato As TicketStatus?
            Get
                Return _selectedStato
            End Get
            Set(value As TicketStatus?)
                If _selectedStato <> value Then
                    _selectedStato = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedPriorita As String
            Get
                Return _selectedPriorita
            End Get
            Set(value As String)
                If _selectedPriorita <> value Then
                    _selectedPriorita = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedTecnico As Operatore
            Get
                Return _selectedTecnico
            End Get
            Set(value As Operatore)
                If _selectedTecnico IsNot value Then
                    _selectedTecnico = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property DataDa As Date?
            Get
                Return _dataDa
            End Get
            Set(value As Date?)
                If _dataDa <> value Then
                    _dataDa = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property DataA As Date?
            Get
                Return _dataA
            End Get
            Set(value As Date?)
                If _dataA <> value Then
                    _dataA = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedTicket As Ticket
            Get
                Return _selected
            End Get
            Set(value As Ticket)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub ApplyFilter()
            If Tickets Is Nothing Then
                FilteredTickets = New ObservableCollection(Of Ticket)()
                Return
            End If
            Dim query = Tickets.AsEnumerable()
            If SelectedStato.HasValue Then
                query = query.Where(Function(t) t.Stato = SelectedStato.Value)
            End If
            If Not String.IsNullOrWhiteSpace(SelectedPriorita) Then
                query = query.Where(Function(t) t.Priorita = SelectedPriorita)
            End If
            If SelectedTecnico IsNot Nothing Then
                query = query.Where(Function(t) t.TecnicoId = SelectedTecnico.Id)
            End If
            If DataDa.HasValue Then
                query = query.Where(Function(t) t.DataRichiesta >= DataDa.Value)
            End If
            If DataA.HasValue Then
                query = query.Where(Function(t) t.DataRichiesta <= DataA.Value)
            End If
            FilteredTickets = New ObservableCollection(Of Ticket)(query)
        End Sub

        Public Sub AddTicket(ticket As Ticket)
            Dim created = _service.CreateTicket(ticket)
            Tickets.Add(created)
            ApplyFilter()
        End Sub

        Public Sub UpdateTicket(ticket As Ticket)
            Dim updated = _service.UpdateTicket(ticket)
            Dim idx = Tickets.IndexOf(ticket)
            If idx >= 0 Then
                Tickets(idx) = updated
            End If
            ApplyFilter()
        End Sub

        Public Sub DeleteSelected()
            If SelectedTicket Is Nothing Then Return
            If _service.DeleteTicket(SelectedTicket.Id) Then
                Tickets.Remove(SelectedTicket)
                ApplyFilter()
            End If
        End Sub
    End Class
End Namespace
