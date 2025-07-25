Namespace Maintenance.Shared.Models
    Public Class InterventoRicambi
        Public Property Id As Integer
        Public Property Codice As String
        Public Property Descrizione As String
        Public Property Quantita As Integer
        Public Property PrezzoUnitario As Decimal

        Public Property InterventoId As Integer
        Public Property Intervento As Intervento
    End Class
End Namespace

