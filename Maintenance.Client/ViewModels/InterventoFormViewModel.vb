Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Commands
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class InterventoFormViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService

        Public Property Intervento As Intervento
        Public Property Checks As ObservableCollection(Of InterventoCheck)
        Public Property Ricambi As ObservableCollection(Of InterventoRicambi)
        Public Property Manodopera As ObservableCollection(Of InterventoManodopera)
        Public ReadOnly Property EsitoOptions As List(Of String) = New List(Of String) From {"OK", "Da sostituire", "Da monitorare"}

        Public ReadOnly Property SaveCommand As RelayCommand
        Public ReadOnly Property ConcludeCommand As RelayCommand

        Public Sub New(model As Intervento)
            Intervento = model
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()

            Checks = New ObservableCollection(Of InterventoCheck)()
            Ricambi = New ObservableCollection(Of InterventoRicambi)()
            Manodopera = New ObservableCollection(Of InterventoManodopera)()

            SaveCommand = New RelayCommand(Sub() SaveDraft(), Function(o) True)
            ConcludeCommand = New RelayCommand(Sub() Complete(), Function(o) True)
        End Sub

        Private Sub SaveDraft()
            If Intervento.Id = 0 Then
                _service.CreateIntervento(Intervento)
            Else
                _service.UpdateIntervento(Intervento)
            End If
        End Sub

        Public Sub Complete(Optional firmaTecnico As String = Nothing, Optional firmaCliente As String = Nothing)
            SaveDraft()
            If Not String.IsNullOrEmpty(firmaTecnico) OrElse Not String.IsNullOrEmpty(firmaCliente) Then
                _service.SalvaFirmeIntervento(Intervento.Id, firmaTecnico, firmaCliente)
            End If
            _service.GeneraFoglioIntervento(Intervento.Id)
        End Sub
    End Class
End Namespace
