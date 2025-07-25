Namespace Maintenance.Shared.Models
    Public Class Operatore
        Public Property Id As Integer
        Public Property Nome As String

        Public Property OperatoreSkills As ICollection(Of OperatoreSkill)
        Public Property Interventi As ICollection(Of Intervento)
        Public Property Utenti As ICollection(Of Utente)
    End Class
End Namespace

