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

        'Autenticazione
        <OperationContract>
        Function AuthenticateUser(username As String, password As String) As List(Of String)
    End Interface
End Namespace
