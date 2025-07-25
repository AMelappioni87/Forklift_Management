Imports System.Windows
Imports Maintenance.Client.ViewModels

Namespace Maintenance.Client.Views
    Partial Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            DataContext = New MainViewModel()
        End Sub
    End Class
End Namespace
