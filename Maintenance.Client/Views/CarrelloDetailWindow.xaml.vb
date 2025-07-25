Imports System.Windows
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class CarrelloDetailWindow
        Inherits Window

        Public Sub New(carrello As Carrello)
            InitializeComponent()
            DataContext = New CarrelloDetailViewModel(carrello)
        End Sub
    End Class
End Namespace
