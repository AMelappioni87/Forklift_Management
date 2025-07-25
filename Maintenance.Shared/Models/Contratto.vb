Namespace Maintenance.Shared.Models
    Public Class Contratto
        Public Property Id As Integer

        Public Property ClienteId As Integer
        Public Property Cliente As Cliente

        Public Property CarrelloId As Integer
        Public Property Carrello As Carrello

        Public Property DataInizio As DateTime
        Public Property DataFine As DateTime?
    End Class
End Namespace

