Namespace Maintenance.Shared.Models
    Public Class Ticket
        Public Property Id As Integer
        Public Property Titolo As String
        Public Property Descrizione As String
        Public Property DataApertura As DateTime
        Public Property DataChiusura As DateTime?

        Public Property ClienteId As Integer
        Public Property Cliente As Cliente

        Public Property CarrelloId As Integer?
        Public Property Carrello As Carrello

        Public Property Allegati As ICollection(Of AllegatoTicket)
        Public Property Interventi As ICollection(Of Intervento)
    End Class
End Namespace

