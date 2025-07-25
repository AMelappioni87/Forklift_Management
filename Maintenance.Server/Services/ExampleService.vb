Imports System.ServiceModel

Namespace Maintenance.Server.Services
    <ServiceContract>
    Public Interface IExampleService
        <OperationContract>
        Function GetData(id As Integer) As String
    End Interface

    Public Class ExampleService
        Implements IExampleService

        Public Function GetData(id As Integer) As String Implements IExampleService.GetData
            Return $"You entered: {id}"
        End Function
    End Class
End Namespace
