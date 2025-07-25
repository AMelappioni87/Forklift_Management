Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.DTOs
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class DashboardViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _ticketCounts As Dictionary(Of String, Integer)
        Private _interventiNext7Days As Integer
        Private _appointments As List(Of Pianificazione)

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadData()
        End Sub

        Private Sub LoadData()
            Try
                Dim data = _service.GetDashboardData()
                TicketCounts = data.TicketCounts
                InterventiNext7Days = data.InterventiProssimi7Giorni
                Appointments = data.UpcomingAppointments
            Catch ex As Exception
                ' In produzione gestire log e notifiche
            End Try
        End Sub

        Public Property TicketCounts As Dictionary(Of String, Integer)
            Get
                Return _ticketCounts
            End Get
            Set(value As Dictionary(Of String, Integer))
                If _ticketCounts IsNot value Then
                    _ticketCounts = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property InterventiNext7Days As Integer
            Get
                Return _interventiNext7Days
            End Get
            Set(value As Integer)
                If _interventiNext7Days <> value Then
                    _interventiNext7Days = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Appointments As List(Of Pianificazione)
            Get
                Return _appointments
            End Get
            Set(value As List(Of Pianificazione))
                If _appointments IsNot value Then
                    _appointments = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Sub Refresh()
            LoadData()
        End Sub
    End Class
End Namespace
