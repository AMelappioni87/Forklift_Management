Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class CalendarioViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _currentDate As Date
        Private _days As ObservableCollection(Of CalendarDay)
        Private _events As List(Of CalendarEvent)
        Private _carrelli As ObservableCollection(Of Carrello)

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            CurrentDate = New Date(Date.Now.Year, Date.Now.Month, 1)
            LoadData()
        End Sub

        Public Property CurrentDate As Date
            Get
                Return _currentDate
            End Get
            Set(value As Date)
                If _currentDate <> value Then
                    _currentDate = value
                    OnPropertyChanged()
                    BuildCalendar()
                End If
            End Set
        End Property

        Public Property Days As ObservableCollection(Of CalendarDay)
            Get
                Return _days
            End Get
            Private Set(value As ObservableCollection(Of CalendarDay))
                If _days IsNot value Then
                    _days = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Carrelli As ObservableCollection(Of Carrello)
            Get
                Return _carrelli
            End Get
            Private Set(value As ObservableCollection(Of Carrello))
                If _carrelli IsNot value Then
                    _carrelli = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Sub LoadData()
            Try
                Dim inter = _service.GetInterventi()
                Dim plan = _service.GetPianificazioni()
                Carrelli = New ObservableCollection(Of Carrello)(_service.GetCarrelli())
                _events = inter.Select(Function(i) New CalendarEvent With {
                    .Id = i.Id,
                    .Data = i.DataInizio,
                    .Subject = $"Intervento {i.Ticket?.Numero}",
                    .IsIntervento = True
                }).ToList()
                _events.AddRange(plan.Select(Function(p) New CalendarEvent With {
                    .Id = p.Id,
                    .Data = p.Data,
                    .Subject = $"Pianificazione {p.Descrizione}",
                    .IsIntervento = False
                }))
                BuildCalendar()
            Catch ex As Exception
                ' simplified error handling
            End Try
        End Sub

        Private Sub BuildCalendar()
            If _events Is Nothing Then
                Days = New ObservableCollection(Of CalendarDay)()
                Return
            End If
            Dim first = New Date(CurrentDate.Year, CurrentDate.Month, 1)
            Dim daysInMonth = Date.DaysInMonth(first.Year, first.Month)
            Dim list As New ObservableCollection(Of CalendarDay)()
            For i As Integer = 0 To daysInMonth - 1
                Dim d = first.AddDays(i)
                Dim evts = _events.Where(Function(ev) ev.Data.Date = d.Date).ToList()
                list.Add(New CalendarDay With {.Date = d, .Events = New ObservableCollection(Of CalendarEvent)(evts)})
            Next
            Days = list
        End Sub

        Public Sub MoveEvent(ev As CalendarEvent, newDate As Date)
            If ev Is Nothing Then Return
            ev.Data = newDate.Date.Add(ev.Data.TimeOfDay)
            If ev.IsIntervento Then
                Dim updated As New Intervento With {.Id = ev.Id, .DataInizio = ev.Data}
                _service.UpdateIntervento(updated)
            Else
                Dim p As New Pianificazione With {.Id = ev.Id, .Data = ev.Data}
                _service.UpdatePianificazione(p)
            End If
            BuildCalendar()
        End Sub

        Public Sub AddPianificazione(p As Pianificazione)
            Dim created = _service.CreatePianificazione(p)
            _events.Add(New CalendarEvent With {
                .Id = created.Id,
                .Data = created.Data,
                .Subject = $"Pianificazione {created.Descrizione}",
                .IsIntervento = False
            })
            BuildCalendar()
        End Sub

        Public Sub NextMonth()
            CurrentDate = CurrentDate.AddMonths(1)
        End Sub

        Public Sub PrevMonth()
            CurrentDate = CurrentDate.AddMonths(-1)
        End Sub
    End Class

    Public Class CalendarDay
        Public Property [Date] As Date
        Public Property Events As ObservableCollection(Of CalendarEvent)
    End Class

    Public Class CalendarEvent
        Public Property Id As Integer
        Public Property Data As DateTime
        Public Property Subject As String
        Public Property IsIntervento As Boolean
    End Class
End Namespace
