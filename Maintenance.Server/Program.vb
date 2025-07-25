Imports System.ServiceModel
Imports Maintenance.Server.Services

Module Program
    Sub Main(args As String())
        Using host As New ServiceHost(GetType(ExampleService))
            host.Open()
            Console.WriteLine("Service is running... Press Enter to exit")
            Console.ReadLine()
            host.Close()
        End Using
    End Sub
End Module
