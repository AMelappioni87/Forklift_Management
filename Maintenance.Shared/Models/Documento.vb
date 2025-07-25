Namespace Maintenance.Shared.Models
    Public Class Documento
        Public Property Id As Integer
        Public Property NomeFile As String
        Public Property Percorso As String
        Public Property Contenuto As Byte()

        Public Property Tipo As String
        Public Property DataCaricamento As Date?

        Public Property ClienteId As Integer?
        Public Property Cliente As Cliente

        Public Property CarrelloId As Integer?
        Public Property Carrello As Carrello

        Public Property TicketId As Integer?
        Public Property Ticket As Ticket

        Public Property InterventoId As Integer?
        Public Property Intervento As Intervento

        Public ReadOnly Property EntitaCollegata As String
            Get
                If CarrelloId.HasValue Then
                    Return $"Carrello {CarrelloId.Value}"
                End If
                If TicketId.HasValue Then
                    Return $"Ticket {TicketId.Value}"
                End If
                If InterventoId.HasValue Then
                    Return $"Intervento {InterventoId.Value}"
                End If
                Return String.Empty
            End Get
        End Property
    End Class
End Namespace

