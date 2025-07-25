Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class OperatoriViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _operatori As ObservableCollection(Of Operatore)
        Private _filtered As ObservableCollection(Of Operatore)
        Private _searchText As String
        Private _selected As Operatore
        Private _skills As ObservableCollection(Of Skill)

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadData()
        End Sub

        Private Sub LoadData()
            Try
                Dim list = _service.GetOperatori()
                Operatori = New ObservableCollection(Of Operatore)(list)
                Skills = New ObservableCollection(Of Skill)(_service.GetSkills())
                ApplyFilter()
            Catch ex As Exception
                ' gestione errori semplificata
            End Try
        End Sub

        Public Property Operatori As ObservableCollection(Of Operatore)
            Get
                Return _operatori
            End Get
            Set(value As ObservableCollection(Of Operatore))
                If _operatori IsNot value Then
                    _operatori = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property FilteredOperatori As ObservableCollection(Of Operatore)
            Get
                Return _filtered
            End Get
            Private Set(value As ObservableCollection(Of Operatore))
                If _filtered IsNot value Then
                    _filtered = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property SearchText As String
            Get
                Return _searchText
            End Get
            Set(value As String)
                If _searchText <> value Then
                    _searchText = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedOperatore As Operatore
            Get
                Return _selected
            End Get
            Set(value As Operatore)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property Skills As ObservableCollection(Of Skill)
            Get
                Return _skills
            End Get
            Private Set(value As ObservableCollection(Of Skill))
                If _skills IsNot value Then
                    _skills = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub ApplyFilter()
            If Operatori Is Nothing Then
                FilteredOperatori = New ObservableCollection(Of Operatore)()
                Return
            End If
            Dim query = Operatori.AsEnumerable()
            If Not String.IsNullOrWhiteSpace(SearchText) Then
                query = query.Where(Function(o) o.Nome IsNot Nothing AndAlso o.Nome.ToLower().Contains(SearchText.ToLower()))
            End If
            FilteredOperatori = New ObservableCollection(Of Operatore)(query)
        End Sub

        Public Sub AddOperatore(op As Operatore)
            Dim created = _service.CreateOperatore(op)
            Operatori.Add(created)
            ApplyFilter()
        End Sub

        Public Sub UpdateOperatore(op As Operatore)
            Dim updated = _service.UpdateOperatore(op)
            Dim idx = Operatori.IndexOf(op)
            If idx >= 0 Then
                Operatori(idx) = updated
            End If
            ApplyFilter()
        End Sub

        Public Sub DeleteSelected()
            If SelectedOperatore Is Nothing Then Return
            If _service.DeleteOperatore(SelectedOperatore.Id) Then
                Operatori.Remove(SelectedOperatore)
                ApplyFilter()
            End If
        End Sub
    End Class
End Namespace
