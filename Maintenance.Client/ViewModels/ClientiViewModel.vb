Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class ClientiViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _clienti As ObservableCollection(Of Cliente)
        Private _filtered As ObservableCollection(Of Cliente)
        Private _searchText As String
        Private _selected As Cliente

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadClienti()
        End Sub

        Private Sub LoadClienti()
            Try
                Dim list = _service.GetClienti()
                Clienti = New ObservableCollection(Of Cliente)(list)
                ApplyFilter()
            Catch ex As Exception
                ' gestione errori semplificata
            End Try
        End Sub

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

        Public Property FilteredClienti As ObservableCollection(Of Cliente)
            Get
                Return _filtered
            End Get
            Private Set(value As ObservableCollection(Of Cliente))
                If _filtered IsNot value Then
                    _filtered = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property SearchText As String
            Get
                Return _searchText
            End Get
            Set(value As String)
                If _searchText <> value Then
                    _searchText = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedCliente As Cliente
            Get
                Return _selected
            End Get
            Set(value As Cliente)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub ApplyFilter()
            If Clienti Is Nothing Then
                FilteredClienti = New ObservableCollection(Of Cliente)()
                Return
            End If
            Dim query = Clienti.AsEnumerable()
            If Not String.IsNullOrWhiteSpace(SearchText) Then
                query = query.Where(Function(c) c.Nome IsNot Nothing AndAlso c.Nome.ToLower().Contains(SearchText.ToLower()))
            End If
            FilteredClienti = New ObservableCollection(Of Cliente)(query)
        End Sub

        Public Sub AddCliente(cliente As Cliente)
            Dim created = _service.CreateCliente(cliente)
            Clienti.Add(created)
            ApplyFilter()
        End Sub

        Public Sub UpdateCliente(cliente As Cliente)
            Dim updated = _service.UpdateCliente(cliente)
            Dim idx = Clienti.IndexOf(cliente)
            If idx >= 0 Then
                Clienti(idx) = updated
            End If
            ApplyFilter()
        End Sub

        Public Sub DeleteSelected()
            If SelectedCliente Is Nothing Then Return
            If _service.DeleteCliente(SelectedCliente.Id) Then
                Clienti.Remove(SelectedCliente)
                ApplyFilter()
            End If
        End Sub
    End Class
End Namespace
