Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Client.Commands
Imports Maintenance.Shared.DTOs
Imports LiveCharts
Imports LiveCharts.Wpf

Namespace Maintenance.Client.ViewModels
    Public Class ReportViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _report As ReportData

        Public Property TicketsApertiSeries As SeriesCollection
        Public Property TicketsChiusiSeries As SeriesCollection
        Public Property InterventiClienteSeries As SeriesCollection
        Public Property TempoMedioSeries As SeriesCollection
        Public Property CostiClienteSeries As SeriesCollection

        Public ReadOnly Property ExportExcelCommand As RelayCommand
        Public ReadOnly Property ExportPdfCommand As RelayCommand

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()

            ExportExcelCommand = New RelayCommand(Sub() ExportExcel())
            ExportPdfCommand = New RelayCommand(Sub() ExportPdf())

            LoadData()
        End Sub

        Public Property Report As ReportData
            Get
                Return _report
            End Get
            Set(value As ReportData)
                If _report IsNot value Then
                    _report = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub LoadData()
            Try
                Report = _service.GetReportData()

                TicketsApertiSeries = New SeriesCollection From {
                    New ColumnSeries With {
                        .Title = "Aperti",
                        .Values = New ChartValues(Of Integer)(Report.TicketApertiPerMese.Values)
                    }
                }

                TicketsChiusiSeries = New SeriesCollection From {
                    New ColumnSeries With {
                        .Title = "Chiusi",
                        .Values = New ChartValues(Of Integer)(Report.TicketChiusiPerMese.Values)
                    }
                }

                InterventiClienteSeries = New SeriesCollection From {
                    New ColumnSeries With {
                        .Title = "Interventi",
                        .Values = New ChartValues(Of Integer)(Report.InterventiPerCliente.Values)
                    }
                }

                TempoMedioSeries = New SeriesCollection From {
                    New ColumnSeries With {
                        .Title = "Giorni",
                        .Values = New ChartValues(Of Double)(Report.TempoMedioRisoluzione.Values)
                    }
                }

                CostiClienteSeries = New SeriesCollection From {
                    New ColumnSeries With {
                        .Title = "Euro",
                        .Values = New ChartValues(Of Double)(Report.CostiTotaliPerCliente.Values.Select(Function(c) CDbl(c)))
                    }
                }

                OnPropertyChanged(NameOf(TicketsApertiSeries))
                OnPropertyChanged(NameOf(TicketsChiusiSeries))
                OnPropertyChanged(NameOf(InterventiClienteSeries))
                OnPropertyChanged(NameOf(TempoMedioSeries))
                OnPropertyChanged(NameOf(CostiClienteSeries))
            Catch ex As Exception
                ' Gestione errori semplificata
            End Try
        End Sub

        Private Sub ExportExcel()
            ' Implementazione semplificata di esportazione CSV
            Try
                Dim path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "report.csv")
                Using sw As New System.IO.StreamWriter(path)
                    sw.WriteLine("Categoria,Valore")
                    For Each kvp In Report.CostiTotaliPerCliente
                        sw.WriteLine($"{kvp.Key},{kvp.Value}")
                    Next
                End Using
            Catch ex As Exception
            End Try
        End Sub

        Private Sub ExportPdf()
            ' Metodo segnaposto per esportazione PDF
        End Sub
    End Class
End Namespace
