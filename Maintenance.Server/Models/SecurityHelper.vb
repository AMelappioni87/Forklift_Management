Imports System.Security.Cryptography
Imports System.Text

Namespace Maintenance.Server.Models
    Public Module SecurityHelper
        Public Function ComputeHash(password As String) As String
            Using sha = SHA256.Create()
                Dim bytes = Encoding.UTF8.GetBytes(password)
                Dim hash = sha.ComputeHash(bytes)
                Return Convert.ToBase64String(hash)
            End Using
        End Function
    End Module
End Namespace
