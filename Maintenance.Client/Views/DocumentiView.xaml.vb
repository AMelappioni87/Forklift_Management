Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Win32
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class DocumentiView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As DocumentiViewModel
            Get
                Return TryCast(DataContext, DocumentiViewModel)
            End Get
        End Property

        Private Sub OnUpload(sender As Object, e As RoutedEventArgs)
            Dim dlg As New OpenFileDialog() With {.Multiselect = True}
            If dlg.ShowDialog() Then
                For Each f In dlg.FileNames
                    Dim doc As New Documento With {
                        .NomeFile = Path.GetFileName(f),
                        .Contenuto = File.ReadAllBytes(f),
                        .DataCaricamento = Date.Now,
                        .Tipo = Vm.SelectedTipo
                    }
                    Vm.AddDocumento(doc)
                Next
            End If
        End Sub

        Private Sub OnDownload(sender As Object, e As RoutedEventArgs)
            If Vm.SelectedDocumento Is Nothing Then Return
            Dim doc = Vm.GetDocumento(Vm.SelectedDocumento.Id)
            Dim dlg As New SaveFileDialog() With {.FileName = doc.NomeFile}
            If dlg.ShowDialog() Then
                File.WriteAllBytes(dlg.FileName, doc.Contenuto)
            End If
        End Sub

        Private Sub OnDelete(sender As Object, e As RoutedEventArgs)
            Vm.DeleteSelected()
        End Sub
    End Class
End Namespace

