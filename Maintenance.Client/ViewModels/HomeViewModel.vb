Namespace Maintenance.Client.ViewModels
    Public Class HomeViewModel
        Inherits BaseViewModel

    Private _message As String = "Benvenuto nella schermata Home"

    Public Property Message As String
        Get
            Return _message
        End Get
        Set(value As String)
            If _message <> value Then
                _message = value
                OnPropertyChanged()
            End If
        End Set
    End Property
    End Class
End Namespace
