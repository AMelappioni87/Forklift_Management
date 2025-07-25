Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class DocumentiViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService

        Private _documenti As ObservableCollection(Of Documento)
        Private _filtered As ObservableCollection(Of Documento)
        Private _selectedTipo As String
        Private _selectedAssoc As String
        Private _selected As Documento

        Public ReadOnly Property Tipi As List(Of String) = New List(Of String) From {"", "Manuale", "Foto", "Altro"}
        Public ReadOnly Property Associazioni As List(Of String) = New List(Of String) From {"", "Carrello", "Ticket", "Intervento"}

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()

            LoadData()
        End Sub

        Private Sub LoadData()
            Try
                Dim list = _service.GetDocumenti()
                Documenti = New ObservableCollection(Of Documento)(list)
                ApplyFilter()
            Catch ex As Exception
            End Try
        End Sub

        Public Property Documenti As ObservableCollection(Of Documento)
            Get
                Return _documenti
            End Get
            Set(value As ObservableCollection(Of Documento))
                If _documenti IsNot value Then
                    _documenti = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property FilteredDocumenti As ObservableCollection(Of Documento)
            Get
                Return _filtered
            End Get
            Private Set(value As ObservableCollection(Of Documento))
                If _filtered IsNot value Then
                    _filtered = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Property SelectedTipo As String
            Get
                Return _selectedTipo
            End Get
            Set(value As String)
                If _selectedTipo <> value Then
                    _selectedTipo = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedAssociazione As String
            Get
                Return _selectedAssoc
            End Get
            Set(value As String)
                If _selectedAssoc <> value Then
                    _selectedAssoc = value
                    OnPropertyChanged()
                    ApplyFilter()
                End If
            End Set
        End Property

        Public Property SelectedDocumento As Documento
            Get
                Return _selected
            End Get
            Set(value As Documento)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private Sub ApplyFilter()
            If Documenti Is Nothing Then
                FilteredDocumenti = New ObservableCollection(Of Documento)()
                Return
            End If

            Dim query = Documenti.AsEnumerable()

            If Not String.IsNullOrWhiteSpace(SelectedTipo) Then
                query = query.Where(Function(d) d.Tipo = SelectedTipo)
            End If

            If Not String.IsNullOrWhiteSpace(SelectedAssociazione) Then
                Select Case SelectedAssociazione
                    Case "Carrello"
                        query = query.Where(Function(d) d.CarrelloId.HasValue)
                    Case "Ticket"
                        query = query.Where(Function(d) d.TicketId.HasValue)
                    Case "Intervento"
                        query = query.Where(Function(d) d.InterventoId.HasValue)
                End Select
            End If

            FilteredDocumenti = New ObservableCollection(Of Documento)(query)
        End Sub

        Public Sub AddDocumento(doc As Documento)
            Dim created = _service.CreateDocumento(doc)
            Documenti.Add(created)
            ApplyFilter()
        End Sub

        Public Function GetDocumento(id As Integer) As Documento
            Return _service.GetDocumento(id)
        End Function

        Public Sub DeleteSelected()
            If SelectedDocumento Is Nothing Then Return
            If _service.DeleteDocumento(SelectedDocumento.Id) Then
                Documenti.Remove(SelectedDocumento)
                ApplyFilter()
            End If
        End Sub
    End Class
End Namespace

