Namespace Maintenance.Shared.Models
    Public Class Carrello
        Public Property Id As Integer
        Public Property NumeroSerie As String
        Public Property Costruttore As String
        Public Property Modello As String
        Public Property DataProssimaManutenzione As Date?
        Public Property Stato As String

        Public Property ClienteId As Integer
        Public Property Cliente As Cliente

        Public Property Accessori As ICollection(Of AccessorioCarrello)
        Public Property Contratti As ICollection(Of Contratto)
        Public Property Pianificazioni As ICollection(Of Pianificazione)
        Public Property Tickets As ICollection(Of Ticket)
        Public Property Documenti As ICollection(Of Documento)
    End Class
End Namespace

