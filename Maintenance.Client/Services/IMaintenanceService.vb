Imports System.ServiceModel

Namespace Maintenance.Client.Services
    <ServiceContract>
    Public Interface IMaintenanceService
        <OperationContract>
        Function AuthenticateUser(username As String, password As String) As List(Of String)
    End Interface
End Namespace
