Namespace Maintenance.Shared.DTOs
    Public Class DashboardData
        Public Property TicketCounts As Dictionary(Of String, Integer)
        Public Property InterventiProssimi7Giorni As Integer
        Public Property UpcomingAppointments As List(Of Maintenance.Shared.Models.Pianificazione)
    End Class
End Namespace
