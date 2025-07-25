Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.EntityFrameworkCore
Imports Maintenance.Server.Services
Imports Maintenance.Server.Data
Imports Maintenance.Server.Models
Imports Maintenance.Shared.Models
Imports System.ServiceModel

Friend Class TestService
    Inherits MaintenanceService

    Private ReadOnly _options As DbContextOptions(Of MaintenanceDbContext)

    Public Sub New(options As DbContextOptions(Of MaintenanceDbContext))
        MyBase.New()
        _options = options
    End Sub

    Protected Overrides Function CreateContext() As MaintenanceDbContext
        Return New MaintenanceDbContext(_options)
    End Function
End Class

<TestClass>
Public Class AuthenticationTests
    Private Function CreateOptions() As DbContextOptions(Of MaintenanceDbContext)
        Return New DbContextOptionsBuilder(Of MaintenanceDbContext)() _
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options
    End Function

    <TestMethod>
    Public Sub Authenticate_User_Correct_Credentials()
        Dim options = CreateOptions()
        Using ctx As New MaintenanceDbContext(options)
            Dim hashed = SecurityHelper.ComputeHash("pwd")
            ctx.Utenti.Add(New Utente With {.Username = "user", .PasswordHash = hashed, .Ruolo = "admin"})
            ctx.SaveChanges()
        End Using

        Dim svc As New TestService(options)
        Dim perms = svc.AuthenticateUser("user", "pwd")
        CollectionAssert.Contains(perms, "GestioneClienti")
    End Sub

    <TestMethod>
    Public Sub Authenticate_User_Wrong_Password()
        Dim options = CreateOptions()
        Using ctx As New MaintenanceDbContext(options)
            Dim hashed = SecurityHelper.ComputeHash("pwd")
            ctx.Utenti.Add(New Utente With {.Username = "user", .PasswordHash = hashed, .Ruolo = "admin"})
            ctx.SaveChanges()
        End Using
        Dim svc As New TestService(options)
        Assert.ThrowsException(Of FaultException(Of String))(Function() svc.AuthenticateUser("user", "nopwd"))
    End Sub
End Class
