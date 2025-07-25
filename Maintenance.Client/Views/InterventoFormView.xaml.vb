Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Ink
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.Views
    Public Partial Class InterventoFormView
        Inherits UserControl

        Public Sub New(model As Intervento)
            InitializeComponent()
            DataContext = New InterventoFormViewModel(model)
            If model.DataInizio = DateTime.MinValue Then
                model.DataInizio = Date.Now
                model.DataFine = Date.Now
            End If
        End Sub

        Private Function CanvasToBase64(canvas As InkCanvas) As String
            Dim bounds = New Rect(canvas.RenderSize)
            Dim rtb As New RenderTargetBitmap(CInt(bounds.Width), CInt(bounds.Height), 96, 96, PixelFormats.Default)
            rtb.Render(canvas)
            Dim encoder As New PngBitmapEncoder()
            encoder.Frames.Add(BitmapFrame.Create(rtb))
            Using ms As New MemoryStream()
                encoder.Save(ms)
                Return Convert.ToBase64String(ms.ToArray())
            End Using
        End Function

        Private Sub OnConclude(sender As Object, e As RoutedEventArgs)
            Dim vm = TryCast(DataContext, InterventoFormViewModel)
            Dim tech = CanvasToBase64(TecnicoCanvas)
            Dim cli = CanvasToBase64(ClienteCanvas)
            vm.Complete(tech, cli)
            Dim win = Window.GetWindow(Me)
            If win IsNot Nothing Then win.Close()
        End Sub

        Private Sub OnSave(sender As Object, e As RoutedEventArgs)
            Dim vm = TryCast(DataContext, InterventoFormViewModel)
            vm.SaveCommand.Execute(Nothing)
        End Sub
    End Class
End Namespace
