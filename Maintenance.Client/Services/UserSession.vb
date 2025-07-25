Namespace Maintenance.Client.Services
    Public Class UserSession
        Private Shared _instance As UserSession

        Public Property Username As String
        Public Property Role As String

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Instance As UserSession
            Get
                If _instance Is Nothing Then
                    _instance = New UserSession()
                End If
                Return _instance
            End Get
        End Property
    End Class
End Namespace
