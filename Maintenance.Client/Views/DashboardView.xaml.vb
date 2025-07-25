Imports System.Windows.Controls
Imports System.Windows
Imports Maintenance.Client.ViewModels

Namespace Maintenance.Client.Views
    Public Partial Class DashboardView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnOpenCalendar(sender As Object, e As RoutedEventArgs)
            Dim win As New Window With {
                .Title = "Calendario",
                .Content = New CalendarioView() With {.DataContext = New CalendarioViewModel()},
                .SizeToContent = SizeToContent.WidthAndHeight,
                .WindowStartupLocation = WindowStartupLocation.CenterOwner
            }
            win.ShowDialog()
        End Sub
    End Class
End Namespace
