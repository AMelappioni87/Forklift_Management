Imports System.ServiceModel
Imports Microsoft.Extensions.Configuration
Imports Microsoft.EntityFrameworkCore
Imports Maintenance.Server.Data
Imports Maintenance.Shared.Models

Namespace Maintenance.Server.Services
    <ServiceContract>
    Public Interface IMaintenanceService
        'Clienti
        <OperationContract> Function CreateCliente(cliente As Cliente) As Cliente
        <OperationContract> Function GetCliente(id As Integer) As Cliente
        <OperationContract> Function GetClienti() As List(Of Cliente)
        <OperationContract> Function UpdateCliente(cliente As Cliente) As Cliente
        <OperationContract> Function DeleteCliente(id As Integer) As Boolean

        'Carrelli
        <OperationContract> Function CreateCarrello(carrello As Carrello) As Carrello
        <OperationContract> Function GetCarrello(id As Integer) As Carrello
        <OperationContract> Function GetCarrelli() As List(Of Carrello)
        <OperationContract> Function UpdateCarrello(carrello As Carrello) As Carrello
        <OperationContract> Function DeleteCarrello(id As Integer) As Boolean

        'AccessoriCarrello
        <OperationContract> Function CreateAccessorioCarrello(accessorio As AccessorioCarrello) As AccessorioCarrello
        <OperationContract> Function GetAccessorioCarrello(id As Integer) As AccessorioCarrello
        <OperationContract> Function GetAccessoriCarrello() As List(Of AccessorioCarrello)
        <OperationContract> Function UpdateAccessorioCarrello(accessorio As AccessorioCarrello) As AccessorioCarrello
        <OperationContract> Function DeleteAccessorioCarrello(id As Integer) As Boolean

        'Contratti
        <OperationContract> Function CreateContratto(contratto As Contratto) As Contratto
        <OperationContract> Function GetContratto(id As Integer) As Contratto
        <OperationContract> Function GetContratti() As List(Of Contratto)
        <OperationContract> Function UpdateContratto(contratto As Contratto) As Contratto
        <OperationContract> Function DeleteContratto(id As Integer) As Boolean

        'Operatori
        <OperationContract> Function CreateOperatore(operatore As Operatore) As Operatore
        <OperationContract> Function GetOperatore(id As Integer) As Operatore
        <OperationContract> Function GetOperatori() As List(Of Operatore)
        <OperationContract> Function UpdateOperatore(operatore As Operatore) As Operatore
        <OperationContract> Function DeleteOperatore(id As Integer) As Boolean

        'Skill
        <OperationContract> Function CreateSkill(skill As Skill) As Skill
        <OperationContract> Function GetSkill(id As Integer) As Skill
        <OperationContract> Function GetSkills() As List(Of Skill)
        <OperationContract> Function UpdateSkill(skill As Skill) As Skill
        <OperationContract> Function DeleteSkill(id As Integer) As Boolean

        'Ticket
        <OperationContract> Function CreateTicket(ticket As Ticket) As Ticket
        <OperationContract> Function GetTicket(id As Integer) As Ticket
        <OperationContract> Function GetTickets() As List(Of Ticket)
        <OperationContract> Function UpdateTicket(ticket As Ticket) As Ticket
        <OperationContract> Function DeleteTicket(id As Integer) As Boolean

        'AllegatiTicket
        <OperationContract> Function CreateAllegatoTicket(allegato As AllegatoTicket) As AllegatoTicket
        <OperationContract> Function GetAllegatoTicket(id As Integer) As AllegatoTicket
        <OperationContract> Function GetAllegatiTicket() As List(Of AllegatoTicket)
        <OperationContract> Function UpdateAllegatoTicket(allegato As AllegatoTicket) As AllegatoTicket
        <OperationContract> Function DeleteAllegatoTicket(id As Integer) As Boolean

        'Interventi
        <OperationContract> Function CreateIntervento(intervento As Intervento) As Intervento
        <OperationContract> Function GetIntervento(id As Integer) As Intervento
        <OperationContract> Function GetInterventi() As List(Of Intervento)
        <OperationContract> Function UpdateIntervento(intervento As Intervento) As Intervento
        <OperationContract> Function DeleteIntervento(id As Integer) As Boolean

        'CheckItem
        <OperationContract> Function CreateCheckItem(item As CheckItem) As CheckItem
        <OperationContract> Function GetCheckItem(id As Integer) As CheckItem
        <OperationContract> Function GetCheckItems() As List(Of CheckItem)
        <OperationContract> Function UpdateCheckItem(item As CheckItem) As CheckItem
        <OperationContract> Function DeleteCheckItem(id As Integer) As Boolean

        'Pianificazioni
        <OperationContract> Function CreatePianificazione(pianificazione As Pianificazione) As Pianificazione
        <OperationContract> Function GetPianificazione(id As Integer) As Pianificazione
        <OperationContract> Function GetPianificazioni() As List(Of Pianificazione)
        <OperationContract> Function UpdatePianificazione(pianificazione As Pianificazione) As Pianificazione
        <OperationContract> Function DeletePianificazione(id As Integer) As Boolean

        'Documenti
        <OperationContract> Function CreateDocumento(documento As Documento) As Documento
        <OperationContract> Function GetDocumento(id As Integer) As Documento
        <OperationContract> Function GetDocumenti() As List(Of Documento)
        <OperationContract> Function UpdateDocumento(documento As Documento) As Documento
        <OperationContract> Function DeleteDocumento(id As Integer) As Boolean

        'Utenti
        <OperationContract> Function CreateUtente(utente As Utente) As Utente
        <OperationContract> Function GetUtente(id As Integer) As Utente
        <OperationContract> Function GetUtenti() As List(Of Utente)
        <OperationContract> Function UpdateUtente(utente As Utente) As Utente
        <OperationContract> Function DeleteUtente(id As Integer) As Boolean

        'Autenticazione
        <OperationContract> Function AuthenticateUser(username As String, password As String) As List(Of String)
    End Interface

    Public Class MaintenanceService
        Implements IMaintenanceService

        Private ReadOnly _connectionString As String

        Public Sub New()
            Dim config = New ConfigurationBuilder() _
                .SetBasePath(AppContext.BaseDirectory) _
                .AddJsonFile("appsettings.json") _
                .Build()
            _connectionString = config.GetConnectionString("DefaultConnection")
        End Sub

        Private Function CreateContext() As MaintenanceDbContext
            Dim options = New DbContextOptionsBuilder(Of MaintenanceDbContext)() _
                .UseSqlServer(_connectionString).Options
            Return New MaintenanceDbContext(options)
        End Function

        'Clienti
        Public Function CreateCliente(cliente As Cliente) As Cliente Implements IMaintenanceService.CreateCliente
            Try
                Using ctx = CreateContext()
                    ctx.Clienti.Add(cliente)
                    ctx.SaveChanges()
                    Return cliente
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetCliente(id As Integer) As Cliente Implements IMaintenanceService.GetCliente
            Try
                Using ctx = CreateContext()
                    Return ctx.Clienti.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetClienti() As List(Of Cliente) Implements IMaintenanceService.GetClienti
            Try
                Using ctx = CreateContext()
                    Return ctx.Clienti.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateCliente(cliente As Cliente) As Cliente Implements IMaintenanceService.UpdateCliente
            Try
                Using ctx = CreateContext()
                    ctx.Clienti.Update(cliente)
                    ctx.SaveChanges()
                    Return cliente
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteCliente(id As Integer) As Boolean Implements IMaintenanceService.DeleteCliente
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Clienti.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Clienti.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Carrelli
        Public Function CreateCarrello(carrello As Carrello) As Carrello Implements IMaintenanceService.CreateCarrello
            Try
                Using ctx = CreateContext()
                    ctx.Carrelli.Add(carrello)
                    ctx.SaveChanges()
                    Return carrello
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetCarrello(id As Integer) As Carrello Implements IMaintenanceService.GetCarrello
            Try
                Using ctx = CreateContext()
                    Return ctx.Carrelli.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetCarrelli() As List(Of Carrello) Implements IMaintenanceService.GetCarrelli
            Try
                Using ctx = CreateContext()
                    Return ctx.Carrelli.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateCarrello(carrello As Carrello) As Carrello Implements IMaintenanceService.UpdateCarrello
            Try
                Using ctx = CreateContext()
                    ctx.Carrelli.Update(carrello)
                    ctx.SaveChanges()
                    Return carrello
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteCarrello(id As Integer) As Boolean Implements IMaintenanceService.DeleteCarrello
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Carrelli.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Carrelli.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'AccessoriCarrello
        Public Function CreateAccessorioCarrello(accessorio As AccessorioCarrello) As AccessorioCarrello Implements IMaintenanceService.CreateAccessorioCarrello
            Try
                Using ctx = CreateContext()
                    ctx.AccessoriCarrello.Add(accessorio)
                    ctx.SaveChanges()
                    Return accessorio
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetAccessorioCarrello(id As Integer) As AccessorioCarrello Implements IMaintenanceService.GetAccessorioCarrello
            Try
                Using ctx = CreateContext()
                    Return ctx.AccessoriCarrello.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetAccessoriCarrello() As List(Of AccessorioCarrello) Implements IMaintenanceService.GetAccessoriCarrello
            Try
                Using ctx = CreateContext()
                    Return ctx.AccessoriCarrello.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateAccessorioCarrello(accessorio As AccessorioCarrello) As AccessorioCarrello Implements IMaintenanceService.UpdateAccessorioCarrello
            Try
                Using ctx = CreateContext()
                    ctx.AccessoriCarrello.Update(accessorio)
                    ctx.SaveChanges()
                    Return accessorio
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteAccessorioCarrello(id As Integer) As Boolean Implements IMaintenanceService.DeleteAccessorioCarrello
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.AccessoriCarrello.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.AccessoriCarrello.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Contratti
        Public Function CreateContratto(contratto As Contratto) As Contratto Implements IMaintenanceService.CreateContratto
            Try
                Using ctx = CreateContext()
                    ctx.Contratti.Add(contratto)
                    ctx.SaveChanges()
                    Return contratto
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetContratto(id As Integer) As Contratto Implements IMaintenanceService.GetContratto
            Try
                Using ctx = CreateContext()
                    Return ctx.Contratti.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetContratti() As List(Of Contratto) Implements IMaintenanceService.GetContratti
            Try
                Using ctx = CreateContext()
                    Return ctx.Contratti.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateContratto(contratto As Contratto) As Contratto Implements IMaintenanceService.UpdateContratto
            Try
                Using ctx = CreateContext()
                    ctx.Contratti.Update(contratto)
                    ctx.SaveChanges()
                    Return contratto
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteContratto(id As Integer) As Boolean Implements IMaintenanceService.DeleteContratto
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Contratti.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Contratti.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Operatori
        Public Function CreateOperatore(operatore As Operatore) As Operatore Implements IMaintenanceService.CreateOperatore
            Try
                Using ctx = CreateContext()
                    ctx.Operatori.Add(operatore)
                    ctx.SaveChanges()
                    Return operatore
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetOperatore(id As Integer) As Operatore Implements IMaintenanceService.GetOperatore
            Try
                Using ctx = CreateContext()
                    Return ctx.Operatori.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetOperatori() As List(Of Operatore) Implements IMaintenanceService.GetOperatori
            Try
                Using ctx = CreateContext()
                    Return ctx.Operatori.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateOperatore(operatore As Operatore) As Operatore Implements IMaintenanceService.UpdateOperatore
            Try
                Using ctx = CreateContext()
                    ctx.Operatori.Update(operatore)
                    ctx.SaveChanges()
                    Return operatore
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteOperatore(id As Integer) As Boolean Implements IMaintenanceService.DeleteOperatore
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Operatori.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Operatori.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Skill
        Public Function CreateSkill(skill As Skill) As Skill Implements IMaintenanceService.CreateSkill
            Try
                Using ctx = CreateContext()
                    ctx.Skills.Add(skill)
                    ctx.SaveChanges()
                    Return skill
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetSkill(id As Integer) As Skill Implements IMaintenanceService.GetSkill
            Try
                Using ctx = CreateContext()
                    Return ctx.Skills.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetSkills() As List(Of Skill) Implements IMaintenanceService.GetSkills
            Try
                Using ctx = CreateContext()
                    Return ctx.Skills.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateSkill(skill As Skill) As Skill Implements IMaintenanceService.UpdateSkill
            Try
                Using ctx = CreateContext()
                    ctx.Skills.Update(skill)
                    ctx.SaveChanges()
                    Return skill
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteSkill(id As Integer) As Boolean Implements IMaintenanceService.DeleteSkill
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Skills.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Skills.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Ticket
        Public Function CreateTicket(ticket As Ticket) As Ticket Implements IMaintenanceService.CreateTicket
            Try
                Using ctx = CreateContext()
                    ctx.Tickets.Add(ticket)
                    ctx.SaveChanges()
                    Return ticket
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetTicket(id As Integer) As Ticket Implements IMaintenanceService.GetTicket
            Try
                Using ctx = CreateContext()
                    Return ctx.Tickets.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetTickets() As List(Of Ticket) Implements IMaintenanceService.GetTickets
            Try
                Using ctx = CreateContext()
                    Return ctx.Tickets.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateTicket(ticket As Ticket) As Ticket Implements IMaintenanceService.UpdateTicket
            Try
                Using ctx = CreateContext()
                    ctx.Tickets.Update(ticket)
                    ctx.SaveChanges()
                    Return ticket
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteTicket(id As Integer) As Boolean Implements IMaintenanceService.DeleteTicket
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Tickets.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Tickets.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'AllegatiTicket
        Public Function CreateAllegatoTicket(allegato As AllegatoTicket) As AllegatoTicket Implements IMaintenanceService.CreateAllegatoTicket
            Try
                Using ctx = CreateContext()
                    ctx.AllegatiTicket.Add(allegato)
                    ctx.SaveChanges()
                    Return allegato
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetAllegatoTicket(id As Integer) As AllegatoTicket Implements IMaintenanceService.GetAllegatoTicket
            Try
                Using ctx = CreateContext()
                    Return ctx.AllegatiTicket.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetAllegatiTicket() As List(Of AllegatoTicket) Implements IMaintenanceService.GetAllegatiTicket
            Try
                Using ctx = CreateContext()
                    Return ctx.AllegatiTicket.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateAllegatoTicket(allegato As AllegatoTicket) As AllegatoTicket Implements IMaintenanceService.UpdateAllegatoTicket
            Try
                Using ctx = CreateContext()
                    ctx.AllegatiTicket.Update(allegato)
                    ctx.SaveChanges()
                    Return allegato
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteAllegatoTicket(id As Integer) As Boolean Implements IMaintenanceService.DeleteAllegatoTicket
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.AllegatiTicket.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.AllegatiTicket.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Interventi
        Public Function CreateIntervento(intervento As Intervento) As Intervento Implements IMaintenanceService.CreateIntervento
            Try
                Using ctx = CreateContext()
                    ctx.Interventi.Add(intervento)
                    ctx.SaveChanges()
                    Return intervento
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetIntervento(id As Integer) As Intervento Implements IMaintenanceService.GetIntervento
            Try
                Using ctx = CreateContext()
                    Return ctx.Interventi.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetInterventi() As List(Of Intervento) Implements IMaintenanceService.GetInterventi
            Try
                Using ctx = CreateContext()
                    Return ctx.Interventi.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateIntervento(intervento As Intervento) As Intervento Implements IMaintenanceService.UpdateIntervento
            Try
                Using ctx = CreateContext()
                    ctx.Interventi.Update(intervento)
                    ctx.SaveChanges()
                    Return intervento
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteIntervento(id As Integer) As Boolean Implements IMaintenanceService.DeleteIntervento
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Interventi.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Interventi.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'CheckItem
        Public Function CreateCheckItem(item As CheckItem) As CheckItem Implements IMaintenanceService.CreateCheckItem
            Try
                Using ctx = CreateContext()
                    ctx.CheckItems.Add(item)
                    ctx.SaveChanges()
                    Return item
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetCheckItem(id As Integer) As CheckItem Implements IMaintenanceService.GetCheckItem
            Try
                Using ctx = CreateContext()
                    Return ctx.CheckItems.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetCheckItems() As List(Of CheckItem) Implements IMaintenanceService.GetCheckItems
            Try
                Using ctx = CreateContext()
                    Return ctx.CheckItems.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateCheckItem(item As CheckItem) As CheckItem Implements IMaintenanceService.UpdateCheckItem
            Try
                Using ctx = CreateContext()
                    ctx.CheckItems.Update(item)
                    ctx.SaveChanges()
                    Return item
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteCheckItem(id As Integer) As Boolean Implements IMaintenanceService.DeleteCheckItem
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.CheckItems.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.CheckItems.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Pianificazioni
        Public Function CreatePianificazione(pianificazione As Pianificazione) As Pianificazione Implements IMaintenanceService.CreatePianificazione
            Try
                Using ctx = CreateContext()
                    ctx.Pianificazioni.Add(pianificazione)
                    ctx.SaveChanges()
                    Return pianificazione
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetPianificazione(id As Integer) As Pianificazione Implements IMaintenanceService.GetPianificazione
            Try
                Using ctx = CreateContext()
                    Return ctx.Pianificazioni.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetPianificazioni() As List(Of Pianificazione) Implements IMaintenanceService.GetPianificazioni
            Try
                Using ctx = CreateContext()
                    Return ctx.Pianificazioni.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdatePianificazione(pianificazione As Pianificazione) As Pianificazione Implements IMaintenanceService.UpdatePianificazione
            Try
                Using ctx = CreateContext()
                    ctx.Pianificazioni.Update(pianificazione)
                    ctx.SaveChanges()
                    Return pianificazione
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeletePianificazione(id As Integer) As Boolean Implements IMaintenanceService.DeletePianificazione
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Pianificazioni.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Pianificazioni.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Documenti
        Public Function CreateDocumento(documento As Documento) As Documento Implements IMaintenanceService.CreateDocumento
            Try
                Using ctx = CreateContext()
                    ctx.Documenti.Add(documento)
                    ctx.SaveChanges()
                    Return documento
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetDocumento(id As Integer) As Documento Implements IMaintenanceService.GetDocumento
            Try
                Using ctx = CreateContext()
                    Return ctx.Documenti.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetDocumenti() As List(Of Documento) Implements IMaintenanceService.GetDocumenti
            Try
                Using ctx = CreateContext()
                    Return ctx.Documenti.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateDocumento(documento As Documento) As Documento Implements IMaintenanceService.UpdateDocumento
            Try
                Using ctx = CreateContext()
                    ctx.Documenti.Update(documento)
                    ctx.SaveChanges()
                    Return documento
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteDocumento(id As Integer) As Boolean Implements IMaintenanceService.DeleteDocumento
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Documenti.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Documenti.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        'Utenti
        Public Function CreateUtente(utente As Utente) As Utente Implements IMaintenanceService.CreateUtente
            Try
                Using ctx = CreateContext()
                    ctx.Utenti.Add(utente)
                    ctx.SaveChanges()
                    Return utente
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetUtente(id As Integer) As Utente Implements IMaintenanceService.GetUtente
            Try
                Using ctx = CreateContext()
                    Return ctx.Utenti.Find(id)
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function GetUtenti() As List(Of Utente) Implements IMaintenanceService.GetUtenti
            Try
                Using ctx = CreateContext()
                    Return ctx.Utenti.ToList()
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function UpdateUtente(utente As Utente) As Utente Implements IMaintenanceService.UpdateUtente
            Try
                Using ctx = CreateContext()
                    ctx.Utenti.Update(utente)
                    ctx.SaveChanges()
                    Return utente
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function DeleteUtente(id As Integer) As Boolean Implements IMaintenanceService.DeleteUtente
            Try
                Using ctx = CreateContext()
                    Dim entity = ctx.Utenti.Find(id)
                    If entity Is Nothing Then Return False
                    ctx.Utenti.Remove(entity)
                    ctx.SaveChanges()
                    Return True
                End Using
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function

        Public Function AuthenticateUser(username As String, password As String) As List(Of String) Implements IMaintenanceService.AuthenticateUser
            Try
                Using ctx = CreateContext()
                    Dim user = ctx.Utenti.SingleOrDefault(Function(u) u.Username = username AndAlso u.PasswordHash = password)
                    If user Is Nothing Then
                        Throw New FaultException(Of String)("Credenziali non valide")
                    End If
                    'Valori di ruolo demo
                    Return New List(Of String) From {"User"}
                End Using
            Catch ex As FaultException
                Throw
            Catch ex As Exception
                Throw New FaultException(Of String)(ex.Message)
            End Try
        End Function
    End Class
End Namespace
