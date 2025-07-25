Namespace Maintenance.Shared.Models
    Public Class Pianificazione
        Public Property Id As Integer
        Public Property Data As DateTime
        Public Property Descrizione As String

        Public Property CarrelloId As Integer
        Public Property Carrello As Carrello
    End Class
End Namespace

