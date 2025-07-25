Namespace Maintenance.Shared.Models
    Public Class Skill
        Public Property Id As Integer
        Public Property Nome As String

        Public Property OperatoreSkills As ICollection(Of OperatoreSkill)
    End Class
End Namespace

