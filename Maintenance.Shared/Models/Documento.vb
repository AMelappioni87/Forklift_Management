Namespace Maintenance.Shared.Models
    Public Class Documento
        Public Property Id As Integer
        Public Property NomeFile As String
        Public Property Percorso As String
        Public Property Contenuto As Byte()

        Public Property ClienteId As Integer?
        Public Property Cliente As Cliente

        Public Property CarrelloId As Integer?
        Public Property Carrello As Carrello
    End Class
End Namespace

