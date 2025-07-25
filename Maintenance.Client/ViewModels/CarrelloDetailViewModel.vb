Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class CarrelloDetailViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _carrello As Carrello
        Private _interventi As ObservableCollection(Of Intervento)
        Private _documenti As ObservableCollection(Of Documento)

        Public Sub New(carrello As Carrello)
            _carrello = carrello
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadDetails()
        End Sub

        Public Property Carrello As Carrello
            Get
                Return _carrello
            End Get
            Set(value As Carrello)
                If _carrello IsNot value Then
                    _carrello = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Interventi As ObservableCollection(Of Intervento)
            Get
                Return _interventi
            End Get
            Private Set(value As ObservableCollection(Of Intervento))
                If _interventi IsNot value Then
                    _interventi = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Documenti As ObservableCollection(Of Documento)
            Get
                Return _documenti
            End Get
            Private Set(value As ObservableCollection(Of Documento))
                If _documenti IsNot value Then
                    _documenti = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub LoadDetails()
            Try
                Dim allInterventi = _service.GetInterventi()
                Interventi = New ObservableCollection(Of Intervento)(allInterventi.Where(Function(i) i.Ticket IsNot Nothing AndAlso i.Ticket.CarrelloId = Carrello.Id))
                Dim allDocs = _service.GetDocumenti()
                Documenti = New ObservableCollection(Of Documento)(allDocs.Where(Function(d) d.CarrelloId = Carrello.Id))
            Catch ex As Exception
            End Try
        End Sub
    End Class
End Namespace
