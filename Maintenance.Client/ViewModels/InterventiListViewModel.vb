Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class InterventiListViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _interventi As ObservableCollection(Of Intervento)
        Private _selected As Intervento

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadData()
        End Sub

        Private Sub LoadData()
            Try
                Dim list = _service.GetInterventi()
                Dim filtered = list.Where(Function(i) i.Ticket IsNot Nothing AndAlso i.Ticket.Stato = TicketStatus.InLavorazione)
                Interventi = New ObservableCollection(Of Intervento)(filtered)
            Catch ex As Exception
            End Try
        End Sub

        Public Property Interventi As ObservableCollection(Of Intervento)
            Get
                Return _interventi
            End Get
            Private Set(value As ObservableCollection(Of Intervento))
                If _interventi IsNot value Then
                    _interventi = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property SelectedIntervento As Intervento
            Get
                Return _selected
            End Get
            Set(value As Intervento)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property
    End Class
End Namespace
