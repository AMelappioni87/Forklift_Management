Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.EntityFrameworkCore
Imports Maintenance.Shared.Models
Imports Maintenance.Server.Data
Imports Maintenance.Server.Background

<TestClass>
Public Class PianificazioneWorkerTests
    Private Function CreateContext() As MaintenanceDbContext
        Dim options = New DbContextOptionsBuilder(Of MaintenanceDbContext)()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options
        Return New MaintenanceDbContext(options)
    End Function

    <TestMethod>
    Public Sub Scadenza_Crea_Ticket_Assegna_Operatore_Libero()
        Using ctx = CreateContext()
            Dim op1 As New Operatore With {.Nome = "A"}
            Dim op2 As New Operatore With {.Nome = "B"}
            ctx.Operatori.AddRange(op1, op2)

            Dim t1 As New Ticket With {.Titolo = "T1", .DataRichiesta = DateTime.Now, .Stato = TicketStatus.Aperto}
            Dim t2 As New Ticket With {.Titolo = "T2", .DataRichiesta = DateTime.Now, .Stato = TicketStatus.Aperto}
            ctx.Tickets.AddRange(t1, t2)
            ctx.SaveChanges()
            ctx.Interventi.AddRange(New Intervento With {.TicketId = t1.Id, .OperatoreId = op1.Id, .DataInizio = DateTime.Now},
                                    New Intervento With {.TicketId = t2.Id, .OperatoreId = op1.Id, .DataInizio = DateTime.Now})
            Dim t3 As New Ticket With {.Titolo = "T3", .DataRichiesta = DateTime.Now, .Stato = TicketStatus.Aperto}
            ctx.Tickets.Add(t3)
            ctx.SaveChanges()
            ctx.Interventi.Add(New Intervento With {.TicketId = t3.Id, .OperatoreId = op2.Id, .DataInizio = DateTime.Now})
            ctx.SaveChanges()

            Dim cliente As New Cliente With {.Nome = "C", .Indirizzo = "I"}
            ctx.Clienti.Add(cliente)
            ctx.SaveChanges()
            Dim car As New Carrello With {.NumeroSerie = "X", .ClienteId = cliente.Id}
            ctx.Carrelli.Add(car)
            ctx.Pianificazioni.Add(New Pianificazione With {.CarrelloId = car.Id, .Data = DateTime.Now.AddDays(-1), .Descrizione = "Prog"})
            ctx.SaveChanges()

            PianificazioneWorker.ProcessPianificazioni(ctx, False)

            Dim newTicket = ctx.Tickets.OrderByDescending(Function(x) x.Id).First()
            Dim intervento = ctx.Interventi.Where(Function(i) i.TicketId = newTicket.Id).FirstOrDefault()
            Assert.IsNotNull(intervento)
            Assert.AreEqual(op2.Id, intervento.OperatoreId)
        End Using
    End Sub
End Class
