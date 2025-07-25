Namespace Maintenance.Shared.Models
    Public Class InterventoCheck
        Public Property InterventoId As Integer
        Public Property Intervento As Intervento

        Public Property CheckItemId As Integer
        Public Property CheckItem As CheckItem

        ' Esito puo' assumere valori "OK", "Da sostituire" oppure "Da monitorare"
        Public Property Esito As String
        Public Property Nota As String
    End Class
End Namespace

