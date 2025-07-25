Namespace Maintenance.Shared.Models
    Public Class CheckItem
        Public Property Id As Integer
        Public Property Descrizione As String

        Public Property InterventoChecks As ICollection(Of InterventoCheck)
    End Class
End Namespace

