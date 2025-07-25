Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.EntityFrameworkCore
Imports Maintenance.Server.Data
Imports Maintenance.Shared.Models

<TestClass>
Public Class DbContextCrudTests
    Private Function CreateContext() As MaintenanceDbContext
        Dim options = New DbContextOptionsBuilder(Of MaintenanceDbContext)()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options
        Return New MaintenanceDbContext(options)
    End Function

    <TestMethod>
    Public Sub Cliente_CRUD_Works()
        Using ctx = CreateContext()
            Dim c As New Cliente With {.Nome = "Mario", .Indirizzo = "Via Roma"}
            ctx.Clienti.Add(c)
            ctx.SaveChanges()
            Assert.IsTrue(c.Id > 0)

            Dim read = ctx.Clienti.Find(c.Id)
            Assert.IsNotNull(read)

            read.Nome = "Luigi"
            ctx.SaveChanges()
            Dim updated = ctx.Clienti.Find(c.Id)
            Assert.AreEqual("Luigi", updated.Nome)

            ctx.Clienti.Remove(updated)
            ctx.SaveChanges()
            Dim deleted = ctx.Clienti.Find(c.Id)
            Assert.IsNull(deleted)
        End Using
    End Sub

    <TestMethod>
    Public Sub Carrello_CRUD_Works()
        Using ctx = CreateContext()
            Dim cliente As New Cliente With {.Nome = "Societa", .Indirizzo = "Piazza"}
            ctx.Clienti.Add(cliente)
            ctx.SaveChanges()
            Dim car As New Carrello With {.NumeroSerie = "123", .ClienteId = cliente.Id}
            ctx.Carrelli.Add(car)
            ctx.SaveChanges()

            Dim read = ctx.Carrelli.Include(Function(x) x.Cliente).Single(Function(x) x.Id = car.Id)
            Assert.AreEqual("123", read.NumeroSerie)
            Assert.IsNotNull(read.Cliente)

            read.Stato = "Usato"
            ctx.SaveChanges()
            Dim updated = ctx.Carrelli.Find(car.Id)
            Assert.AreEqual("Usato", updated.Stato)

            ctx.Carrelli.Remove(updated)
            ctx.SaveChanges()
            Dim deleted = ctx.Carrelli.Find(car.Id)
            Assert.IsNull(deleted)
        End Using
    End Sub
End Class
