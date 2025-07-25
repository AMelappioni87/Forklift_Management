Imports System.ServiceModel

Namespace Maintenance.Client.Services
    <ServiceContract>
    Public Interface IMaintenanceService
        'Clienti
        <OperationContract>
        Function CreateCliente(cliente As Maintenance.Shared.Models.Cliente) As Maintenance.Shared.Models.Cliente
        <OperationContract>
        Function GetClienti() As List(Of Maintenance.Shared.Models.Cliente)
        <OperationContract>
        Function UpdateCliente(cliente As Maintenance.Shared.Models.Cliente) As Maintenance.Shared.Models.Cliente
        <OperationContract>
        Function DeleteCliente(id As Integer) As Boolean

        'Carrelli
        <OperationContract>
        Function CreateCarrello(carrello As Maintenance.Shared.Models.Carrello) As Maintenance.Shared.Models.Carrello
        <OperationContract>
        Function GetCarrelli() As List(Of Maintenance.Shared.Models.Carrello)
        <OperationContract>
        Function UpdateCarrello(carrello As Maintenance.Shared.Models.Carrello) As Maintenance.Shared.Models.Carrello
        <OperationContract>
        Function DeleteCarrello(id As Integer) As Boolean

        'Interventi e documenti per i dettagli carrello
        <OperationContract>
        Function GetInterventi() As List(Of Maintenance.Shared.Models.Intervento)
        <OperationContract>
        Function GetDocumenti() As List(Of Maintenance.Shared.Models.Documento)

        'Ticket
        <OperationContract>
        Function GetTickets() As List(Of Maintenance.Shared.Models.Ticket)
        <OperationContract>
        Function CreateTicket(ticket As Maintenance.Shared.Models.Ticket) As Maintenance.Shared.Models.Ticket
        <OperationContract>
        Function UpdateTicket(ticket As Maintenance.Shared.Models.Ticket) As Maintenance.Shared.Models.Ticket
        <OperationContract>
        Function DeleteTicket(id As Integer) As Boolean

        'Allegati ticket
        <OperationContract>
        Function CreateAllegatoTicket(allegato As Maintenance.Shared.Models.AllegatoTicket) As Maintenance.Shared.Models.AllegatoTicket
        <OperationContract>
        Function DeleteAllegatoTicket(id As Integer) As Boolean

        'Operatori
        <OperationContract>
        Function CreateOperatore(operatore As Maintenance.Shared.Models.Operatore) As Maintenance.Shared.Models.Operatore
        <OperationContract>
        Function GetOperatori() As List(Of Maintenance.Shared.Models.Operatore)
        <OperationContract>
        Function UpdateOperatore(operatore As Maintenance.Shared.Models.Operatore) As Maintenance.Shared.Models.Operatore
        <OperationContract>
        Function DeleteOperatore(id As Integer) As Boolean

        'Skill
        <OperationContract>
        Function GetSkills() As List(Of Maintenance.Shared.Models.Skill)
        <OperationContract>
        Function CreateSkill(skill As Maintenance.Shared.Models.Skill) As Maintenance.Shared.Models.Skill
        <OperationContract>
        Function UpdateSkill(skill As Maintenance.Shared.Models.Skill) As Maintenance.Shared.Models.Skill
        <OperationContract>
        Function DeleteSkill(id As Integer) As Boolean

        'Autenticazione
        <OperationContract>
        Function AuthenticateUser(username As String, password As String) As List(Of String)
    End Interface
End Namespace
