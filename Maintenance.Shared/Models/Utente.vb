Namespace Maintenance.Shared.Models
    Public Class Utente
        Public Property Id As Integer
        Public Property Username As String
        Public Property PasswordHash As String
        'admin, tecnico, cliente
        Public Property Ruolo As String

        Public Property OperatoreId As Integer?
        Public Property Operatore As Operatore

        Public Property LogOperazioni As ICollection(Of LogOperazione)
    End Class
End Namespace

