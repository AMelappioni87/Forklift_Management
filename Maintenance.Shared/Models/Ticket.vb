Namespace Maintenance.Shared.Models
    Public Class Ticket
        Public Property Id As Integer
        'numero progressivo del ticket
        Public Property Numero As String
        Public Property Titolo As String
        Public Property Descrizione As String
        Public Property Tipo As String
        Public Property Priorita As String
        Public Property DataRichiesta As DateTime
        Public Property DataApertura As DateTime
        Public Property DataChiusura As DateTime?

        Public Property Stato As TicketStatus

        Public Property TecnicoId As Integer?
        Public Property TecnicoAssegnato As Operatore

        Public Property ClienteId As Integer
        Public Property Cliente As Cliente

        Public Property CarrelloId As Integer?
        Public Property Carrello As Carrello

        Public Property Allegati As ICollection(Of AllegatoTicket)
        Public Property Interventi As ICollection(Of Intervento)
    End Class
End Namespace

