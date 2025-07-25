Imports Maintenance.Client.Services

Namespace Maintenance.Client.ViewModels
    Public Class MainViewModel
        Inherits BaseViewModel

        Public ReadOnly Property Navigation As NavigationService

        Public Sub New()
            Navigation = New NavigationService()
            Navigation.Navigate(New HomeViewModel())
        End Sub
    End Class
End Namespace
