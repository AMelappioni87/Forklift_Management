Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports Maintenance.Client.ViewModels
Imports Maintenance.Shared.Models
Imports MaterialDesignThemes.Wpf

Namespace Maintenance.Client.Views
    Public Partial Class CalendarioView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private ReadOnly Property Vm As CalendarioViewModel
            Get
                Return TryCast(DataContext, CalendarioViewModel)
            End Get
        End Property

        Private _dragEvent As CalendarEvent

        Private Sub OnPrev(sender As Object, e As RoutedEventArgs)
            Vm.PrevMonth()
        End Sub

        Private Sub OnNext(sender As Object, e As RoutedEventArgs)
            Vm.NextMonth()
        End Sub

        Private Sub OnEventClick(sender As Object, e As MouseButtonEventArgs)
            Dim brd = TryCast(sender, Border)
            Dim ev = TryCast(brd?.Tag, CalendarEvent)
            If ev Is Nothing Then Return
            ' In real app open detail windows
            MessageBox.Show(ev.Subject, "Dettaglio")
        End Sub

        Private Sub OnEventDrag(sender As Object, e As MouseEventArgs)
            If e.LeftButton = MouseButtonState.Pressed Then
                Dim brd = TryCast(sender, Border)
                Dim ev = TryCast(brd?.Tag, CalendarEvent)
                If ev IsNot Nothing Then
                    _dragEvent = ev
                    DragDrop.DoDragDrop(brd, ev, DragDropEffects.Move)
                End If
            End If
        End Sub

        Private Sub OnDayDrop(sender As Object, e As DragEventArgs)
            Dim day = TryCast(CType(sender, Border).Tag, CalendarDay)
            If day Is Nothing OrElse _dragEvent Is Nothing Then Return
            Vm.MoveEvent(_dragEvent, day.Date)
            _dragEvent = Nothing
        End Sub

        Private Sub OnAddPlan(sender As Object, e As RoutedEventArgs)
            Dim model As New Pianificazione With {.Data = Date.Now}
            Dim panel As New StackPanel()
            panel.Children.Add(New TextBlock() With {.Text = "Carrello"})
            Dim combo As New ComboBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8), .ItemsSource = Vm.Carrelli, .DisplayMemberPath = "NumeroSerie"}
            panel.Children.Add(combo)
            panel.Children.Add(New TextBlock() With {.Text = "Data"})
            Dim dateBox As New DatePicker() With {.Width = 200, .Margin = New Thickness(0,0,0,8), .SelectedDate = model.Data}
            panel.Children.Add(dateBox)
            panel.Children.Add(New TextBlock() With {.Text = "Descrizione"})
            Dim desc As New TextBox() With {.Width = 200, .Margin = New Thickness(0,0,0,8)}
            desc.SetBinding(TextBox.TextProperty, New Binding("Descrizione") With {.Source = model, .UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged})
            panel.Children.Add(desc)
            Dim btn As New Button() With {.Content = "Salva"}
            AddHandler btn.Click, Sub()
                                       model.Carrello = TryCast(combo.SelectedItem, Carrello)
                                       If model.Carrello IsNot Nothing Then model.CarrelloId = model.Carrello.Id
                                       model.Data = If(dateBox.SelectedDate, Date.Now)
                                       Vm.AddPianificazione(model)
                                       DialogHost.CloseDialogCommand.Execute(Nothing, Host)
                                   End Sub
            panel.Children.Add(btn)
            DialogHost.Show(panel, Host)
        End Sub
    End Class
End Namespace
