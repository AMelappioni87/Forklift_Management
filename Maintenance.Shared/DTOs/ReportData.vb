Namespace Maintenance.Shared.DTOs
    Public Class ReportData
        Public Property TicketApertiPerMese As Dictionary(Of String, Integer)
        Public Property TicketChiusiPerMese As Dictionary(Of String, Integer)
        Public Property InterventiPerCliente As Dictionary(Of String, Integer)
        Public Property TempoMedioRisoluzione As Dictionary(Of String, Double)
        Public Property CostiTotaliPerCliente As Dictionary(Of String, Decimal)
    End Class
End Namespace
