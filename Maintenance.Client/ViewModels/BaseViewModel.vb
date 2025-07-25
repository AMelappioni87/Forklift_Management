Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace Maintenance.Client.ViewModels
    Public MustInherit Class BaseViewModel
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
