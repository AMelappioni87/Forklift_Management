Imports Microsoft.EntityFrameworkCore
Imports Maintenance.Shared.Models

Namespace Maintenance.Server.Data
    Public Class MaintenanceDbContext
        Inherits DbContext

        Public Sub New(options As DbContextOptions(Of MaintenanceDbContext))
            MyBase.New(options)
        End Sub

        Public Property Clienti As DbSet(Of Cliente)
        Public Property Carrelli As DbSet(Of Carrello)
        Public Property AccessoriCarrello As DbSet(Of AccessorioCarrello)
        Public Property Contratti As DbSet(Of Contratto)
        Public Property Operatori As DbSet(Of Operatore)
        Public Property Skills As DbSet(Of Skill)
        Public Property OperatoreSkills As DbSet(Of OperatoreSkill)
        Public Property Tickets As DbSet(Of Ticket)
        Public Property AllegatiTicket As DbSet(Of AllegatoTicket)
        Public Property Interventi As DbSet(Of Intervento)
        Public Property CheckItems As DbSet(Of CheckItem)
        Public Property InterventoChecks As DbSet(Of InterventoCheck)
        Public Property InterventoRicambi As DbSet(Of InterventoRicambi)
        Public Property InterventoManodopera As DbSet(Of InterventoManodopera)
        Public Property Pianificazioni As DbSet(Of Pianificazione)
        Public Property Documenti As DbSet(Of Documento)
        Public Property LogOperazioni As DbSet(Of LogOperazione)
        Public Property Utenti As DbSet(Of Utente)

        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            ' Cliente
            modelBuilder.Entity(Of Cliente)().HasKey(Function(c) c.Id)
            modelBuilder.Entity(Of Cliente)().Property(Function(c) c.Nome).IsRequired()

            ' Carrello
            modelBuilder.Entity(Of Carrello)().HasKey(Function(c) c.Id)
            modelBuilder.Entity(Of Carrello)().Property(Function(c) c.NumeroSerie).IsRequired()
            modelBuilder.Entity(Of Carrello)().Property(Function(c) c.Costruttore).IsRequired(False)
            modelBuilder.Entity(Of Carrello)().Property(Function(c) c.Modello).IsRequired(False)
            modelBuilder.Entity(Of Carrello)().Property(Function(c) c.DataProssimaManutenzione).IsRequired(False)
            modelBuilder.Entity(Of Carrello)().Property(Function(c) c.Stato).IsRequired(False)
            modelBuilder.Entity(Of Carrello)() _
                .HasOne(Function(c) c.Cliente) _
                .WithMany(Function(c) c.Carrelli) _
                .HasForeignKey(Function(c) c.ClienteId)

            ' AccessorioCarrello
            modelBuilder.Entity(Of AccessorioCarrello)().HasKey(Function(a) a.Id)
            modelBuilder.Entity(Of AccessorioCarrello)() _
                .HasOne(Function(a) a.Carrello) _
                .WithMany(Function(c) c.Accessori) _
                .HasForeignKey(Function(a) a.CarrelloId)

            ' Contratto
            modelBuilder.Entity(Of Contratto)().HasKey(Function(c) c.Id)
            modelBuilder.Entity(Of Contratto)() _
                .HasOne(Function(c) c.Cliente) _
                .WithMany(Function(cl) cl.Contratti) _
                .HasForeignKey(Function(c) c.ClienteId)
            modelBuilder.Entity(Of Contratto)() _
                .HasOne(Function(c) c.Carrello) _
                .WithMany(Function(car) car.Contratti) _
                .HasForeignKey(Function(c) c.CarrelloId)

            ' Operatore
            modelBuilder.Entity(Of Operatore)().HasKey(Function(o) o.Id)
            modelBuilder.Entity(Of Operatore)().Property(Function(o) o.Nome).IsRequired()

            ' Skill
            modelBuilder.Entity(Of Skill)().HasKey(Function(s) s.Id)
            modelBuilder.Entity(Of Skill)().Property(Function(s) s.Nome).IsRequired()

            ' OperatoreSkill - many to many
            modelBuilder.Entity(Of OperatoreSkill)().HasKey(Function(os) New With {os.OperatoreId, os.SkillId})
            modelBuilder.Entity(Of OperatoreSkill)() _
                .HasOne(Function(os) os.Operatore) _
                .WithMany(Function(o) o.OperatoreSkills) _
                .HasForeignKey(Function(os) os.OperatoreId)
            modelBuilder.Entity(Of OperatoreSkill)() _
                .HasOne(Function(os) os.Skill) _
                .WithMany(Function(s) s.OperatoreSkills) _
                .HasForeignKey(Function(os) os.SkillId)

            ' Ticket
            modelBuilder.Entity(Of Ticket)().HasKey(Function(t) t.Id)
            modelBuilder.Entity(Of Ticket)().Property(Function(t) t.Titolo).IsRequired()
            modelBuilder.Entity(Of Ticket)().Property(Function(t) t.Numero).IsRequired(False)
            modelBuilder.Entity(Of Ticket)().Property(Function(t) t.Tipo).IsRequired(False)
            modelBuilder.Entity(Of Ticket)().Property(Function(t) t.Priorita).IsRequired(False)
            modelBuilder.Entity(Of Ticket)().Property(Function(t) t.DataRichiesta).IsRequired()
            modelBuilder.Entity(Of Ticket)() _
                .HasOne(Function(t) t.Cliente) _
                .WithMany(Function(c) c.Tickets) _
                .HasForeignKey(Function(t) t.ClienteId)
            modelBuilder.Entity(Of Ticket)() _
                .HasOne(Function(t) t.Carrello) _
                .WithMany(Function(c) c.Tickets) _
                .HasForeignKey(Function(t) t.CarrelloId) _
                .IsRequired(False)
            modelBuilder.Entity(Of Ticket)() _
                .HasOne(Function(t) t.TecnicoAssegnato) _
                .WithMany() _
                .HasForeignKey(Function(t) t.TecnicoId) _
                .IsRequired(False)

            ' AllegatoTicket
            modelBuilder.Entity(Of AllegatoTicket)().HasKey(Function(a) a.Id)
            modelBuilder.Entity(Of AllegatoTicket)() _
                .HasOne(Function(a) a.Ticket) _
                .WithMany(Function(t) t.Allegati) _
                .HasForeignKey(Function(a) a.TicketId)

            ' Intervento
            modelBuilder.Entity(Of Intervento)().HasKey(Function(i) i.Id)
            modelBuilder.Entity(Of Intervento)() _
                .HasOne(Function(i) i.Ticket) _
                .WithMany(Function(t) t.Interventi) _
                .HasForeignKey(Function(i) i.TicketId)
            modelBuilder.Entity(Of Intervento)() _
                .HasOne(Function(i) i.Operatore) _
                .WithMany(Function(o) o.Interventi) _
                .HasForeignKey(Function(i) i.OperatoreId)

            ' CheckItem
            modelBuilder.Entity(Of CheckItem)().HasKey(Function(c) c.Id)

            ' InterventoCheck - many to many
            modelBuilder.Entity(Of InterventoCheck)().HasKey(Function(ic) New With {ic.InterventoId, ic.CheckItemId})
            modelBuilder.Entity(Of InterventoCheck)() _
                .HasOne(Function(ic) ic.Intervento) _
                .WithMany(Function(i) i.InterventoChecks) _
                .HasForeignKey(Function(ic) ic.InterventoId)
            modelBuilder.Entity(Of InterventoCheck)() _
                .HasOne(Function(ic) ic.CheckItem) _
                .WithMany(Function(ci) ci.InterventoChecks) _
                .HasForeignKey(Function(ic) ic.CheckItemId)

            ' InterventoRicambi
            modelBuilder.Entity(Of InterventoRicambi)().HasKey(Function(r) r.Id)
            modelBuilder.Entity(Of InterventoRicambi)() _
                .HasOne(Function(r) r.Intervento) _
                .WithMany(Function(i) i.Ricambi) _
                .HasForeignKey(Function(r) r.InterventoId)

            ' InterventoManodopera
            modelBuilder.Entity(Of InterventoManodopera)().HasKey(Function(m) m.Id)
            modelBuilder.Entity(Of InterventoManodopera)() _
                .HasOne(Function(m) m.Intervento) _
                .WithMany(Function(i) i.Manodopera) _
                .HasForeignKey(Function(m) m.InterventoId)

            ' Pianificazione
            modelBuilder.Entity(Of Pianificazione)().HasKey(Function(p) p.Id)
            modelBuilder.Entity(Of Pianificazione)() _
                .HasOne(Function(p) p.Carrello) _
                .WithMany(Function(c) c.Pianificazioni) _
                .HasForeignKey(Function(p) p.CarrelloId)

            ' Documento
            modelBuilder.Entity(Of Documento)().HasKey(Function(d) d.Id)
            modelBuilder.Entity(Of Documento)() _
                .HasOne(Function(d) d.Cliente) _
                .WithMany(Function(c) c.Documenti) _
                .HasForeignKey(Function(d) d.ClienteId) _
                .IsRequired(False)
            modelBuilder.Entity(Of Documento)() _
                .HasOne(Function(d) d.Carrello) _
                .WithMany(Function(c) c.Documenti) _
                .HasForeignKey(Function(d) d.CarrelloId) _
                .IsRequired(False)

            ' Utente
            modelBuilder.Entity(Of Utente)().HasKey(Function(u) u.Id)
            modelBuilder.Entity(Of Utente)().Property(Function(u) u.Username).IsRequired()
            modelBuilder.Entity(Of Utente)() _
                .HasOne(Function(u) u.Operatore) _
                .WithMany(Function(o) o.Utenti) _
                .HasForeignKey(Function(u) u.OperatoreId) _
                .IsRequired(False)

            ' LogOperazione
            modelBuilder.Entity(Of LogOperazione)().HasKey(Function(l) l.Id)
            modelBuilder.Entity(Of LogOperazione)() _
                .HasOne(Function(l) l.Utente) _
                .WithMany(Function(u) u.LogOperazioni) _
                .HasForeignKey(Function(l) l.UtenteId)
        End Sub
    End Class
End Namespace

