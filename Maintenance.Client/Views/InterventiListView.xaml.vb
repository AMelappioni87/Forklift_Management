Imports System.Windows
Imports System.Windows.Controls
Imports Maintenance.Client.ViewModels
Imports MaterialDesignThemes.Wpf

Namespace Maintenance.Client.Views
    Public Partial Class InterventiListView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As InterventiListViewModel
            Get
                Return TryCast(DataContext, InterventiListViewModel)
            End Get
        End Property

        Private Sub OnOpen(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedIntervento Is Nothing Then Return
            Dim dlg As New Window With {
                .Title = "Intervento",
                .Content = New InterventoFormView(Vm.SelectedIntervento),
                .SizeToContent = SizeToContent.WidthAndHeight,
                .WindowStartupLocation = WindowStartupLocation.CenterOwner
            }
            dlg.ShowDialog()
        End Sub
    End Class
End Namespace
