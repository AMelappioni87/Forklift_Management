Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class CarrelliViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _carrelli As ObservableCollection(Of Carrello)
        Private _filtered As ObservableCollection(Of Carrello)
        Private _selectedCliente As Cliente
        Private _selectedStato As String
        Private _clienti As ObservableCollection(Of Cliente)
        Private _selected As Carrello

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadData()
        End Sub

        Private Sub LoadData()
            Try
                Dim cls = _service.GetClienti()
                Clienti = New ObservableCollection(Of Cliente)(cls)
                Dim list = _service.GetCarrelli()
                Carrelli = New ObservableCollection(Of Carrello)(list)
                ApplyFilter()
            Catch ex As Exception
                ' gestione errori semplificata
            End Try
        End Sub

        Public Property Carrelli As ObservableCollection(Of Carrello)
            Get
                Return _carrelli
            End Get
            Set(value As ObservableCollection(Of Carrello))
                If _carrelli IsNot value Then
                    _carrelli = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property FilteredCarrelli As ObservableCollection(Of Carrello)
            Get
                Return _filtered
            End Get
            Private Set(value As ObservableCollection(Of Carrello))
                If _filtered IsNot value Then
                    _filtered = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Clienti As ObservableCollection(Of Cliente)
            Get
                Return _clienti
            End Get
            Set(value As ObservableCollection(Of Cliente))
                If _clienti IsNot value Then
                    _clienti = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property SelectedCliente As Cliente
            Get
                Return _selectedCliente
            End Get
            Set(value As Cliente)
                If _selectedCliente IsNot value Then
                    _selectedCliente = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedStato As String
            Get
                Return _selectedStato
            End Get
            Set(value As String)
                If _selectedStato <> value Then
                    _selectedStato = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedCarrello As Carrello
            Get
                Return _selected
            End Get
            Set(value As Carrello)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub ApplyFilter()
            If Carrelli Is Nothing Then
                FilteredCarrelli = New ObservableCollection(Of Carrello)()
                Return
            End If
            Dim query = Carrelli.AsEnumerable()
            If SelectedCliente IsNot Nothing Then
                query = query.Where(Function(c) c.ClienteId = SelectedCliente.Id)
            End If
            If Not String.IsNullOrWhiteSpace(SelectedStato) Then
                query = query.Where(Function(c) c.Stato IsNot Nothing AndAlso c.Stato = SelectedStato)
            End If
            FilteredCarrelli = New ObservableCollection(Of Carrello)(query)
        End Sub

        Public Sub AddCarrello(carrello As Carrello)
            Dim created = _service.CreateCarrello(carrello)
            Carrelli.Add(created)
            ApplyFilter()
        End Sub

        Public Sub UpdateCarrello(carrello As Carrello)
            Dim updated = _service.UpdateCarrello(carrello)
            Dim idx = Carrelli.IndexOf(carrello)
            If idx >= 0 Then
                Carrelli(idx) = updated
            End If
            ApplyFilter()
        End Sub

        Public Sub DeleteSelected()
            If SelectedCarrello Is Nothing Then Return
            If _service.DeleteCarrello(SelectedCarrello.Id) Then
                Carrelli.Remove(SelectedCarrello)
                ApplyFilter()
            End If
        End Sub
    End Class
End Namespace
