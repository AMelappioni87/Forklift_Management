Imports System.ServiceModel
Imports System.Windows
Imports Maintenance.Client.Services

Namespace Maintenance.Client.Views
    Public Partial Class LoginWindow
        Inherits Window

        Private ReadOnly _factory As ChannelFactory(Of IMaintenanceService)

        Public Sub New()
            InitializeComponent()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            _factory = New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
        End Sub

        Private Sub OnLoginClick(sender As Object, e As RoutedEventArgs)
            Dim username = UsernameTextBox.Text
            Dim password = PasswordBox.Password
            Try
                Dim client = _factory.CreateChannel()
                Dim roles = client.AuthenticateUser(username, password)

                Dim session = UserSession.Instance
                session.Username = username
                session.Role = If(roles.Any(), roles(0), String.Empty)

                Dim main = New MainWindow()
                main.Show()
                Me.Close()
            Catch ex As FaultException(Of String)
                MessageBox.Show(ex.Detail, "Errore di autenticazione", MessageBoxButton.OK, MessageBoxImage.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End Sub
    End Class
End Namespace
