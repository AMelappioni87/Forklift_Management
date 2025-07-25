Namespace Maintenance.Shared.Models
    Public Class Intervento
        Public Property Id As Integer
        Public Property Data As DateTime
        Public Property Note As String

        Public Property TicketId As Integer
        Public Property Ticket As Ticket

        Public Property OperatoreId As Integer
        Public Property Operatore As Operatore

        Public Property InterventoChecks As ICollection(Of InterventoCheck)
        Public Property Ricambi As ICollection(Of InterventoRicambi)
        Public Property Manodopera As ICollection(Of InterventoManodopera)
    End Class
End Namespace

