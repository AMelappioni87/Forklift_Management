Namespace Maintenance.Client.Services
    Public Class NavigationService
        Inherits ViewModels.BaseViewModel

    Private _currentViewModel As BaseViewModel

    Public Property CurrentViewModel As BaseViewModel
        Get
            Return _currentViewModel
        End Get
        Private Set(value As BaseViewModel)
            If _currentViewModel IsNot value Then
                _currentViewModel = value
                OnPropertyChanged()
            End If
        End Set
    End Property

        Public Sub Navigate(viewModel As ViewModels.BaseViewModel)
            CurrentViewModel = viewModel
        End Sub
    End Class
End Namespace
