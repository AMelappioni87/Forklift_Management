Namespace Maintenance.Shared.Models
    Public Class Cliente
        Public Property Id As Integer
        Public Property Nome As String
        Public Property Indirizzo As String

        Public Property Carrelli As ICollection(Of Carrello)
        Public Property Contratti As ICollection(Of Contratto)
        Public Property Tickets As ICollection(Of Ticket)
        Public Property Documenti As ICollection(Of Documento)
    End Class
End Namespace

