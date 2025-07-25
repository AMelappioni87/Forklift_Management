Namespace Maintenance.Shared.Models
    Public Class AllegatoTicket
        Public Property Id As Integer
        Public Property NomeFile As String
        Public Property Percorso As String
        'contenuto del file per memorizzazione in DB
        Public Property Contenuto As Byte()

        Public Property TicketId As Integer
        Public Property Ticket As Ticket
    End Class
End Namespace

