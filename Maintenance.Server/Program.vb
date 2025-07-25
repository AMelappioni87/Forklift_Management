Imports System.ServiceModel
Imports Maintenance.Server.Services
Imports Maintenance.Server.Background

Module Program
    Sub Main(args As String())
        Dim baseAddress As New Uri("net.tcp://localhost:9000/MaintenanceService")
        Dim worker As New PianificazioneWorker()
        worker.Start()

        Using host As New ServiceHost(GetType(MaintenanceService), baseAddress)
            host.AddServiceEndpoint(GetType(IMaintenanceService), New NetTcpBinding(), "")
            host.Open()
            Console.WriteLine($"Service running at {baseAddress}")
            Console.WriteLine("Press Enter to exit")
            Console.ReadLine()
            host.Close()
        End Using

        worker.Dispose()
    End Sub
End Module
