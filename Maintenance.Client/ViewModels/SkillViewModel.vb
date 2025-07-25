Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports Maintenance.Client.Services
Imports Maintenance.Shared.Models

Namespace Maintenance.Client.ViewModels
    Public Class SkillViewModel
        Inherits BaseViewModel

        Private ReadOnly _service As IMaintenanceService
        Private _skills As ObservableCollection(Of Skill)
        Private _selected As Skill

        Public Sub New()
            Dim binding As New NetTcpBinding()
            Dim endpoint As New EndpointAddress("net.tcp://localhost:9000/MaintenanceService")
            Dim factory As New ChannelFactory(Of IMaintenanceService)(binding, endpoint)
            _service = factory.CreateChannel()
            LoadSkills()
        End Sub

        Private Sub LoadSkills()
            Try
                Dim list = _service.GetSkills()
                Skills = New ObservableCollection(Of Skill)(list)
            Catch ex As Exception
            End Try
        End Sub

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

        Public Property SelectedSkill As Skill
            Get
                Return _selected
            End Get
            Set(value As Skill)
                If _selected IsNot value Then
                    _selected = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Sub AddSkill(skill As Skill)
            Dim created = _service.CreateSkill(skill)
            Skills.Add(created)
        End Sub

        Public Sub UpdateSkill(skill As Skill)
            Dim updated = _service.UpdateSkill(skill)
            Dim idx = Skills.IndexOf(skill)
            If idx >= 0 Then
                Skills(idx) = updated
            End If
        End Sub

        Public Sub DeleteSelected()
            If SelectedSkill Is Nothing Then Return
            If _service.DeleteSkill(SelectedSkill.Id) Then
                Skills.Remove(SelectedSkill)
            End If
        End Sub
    End Class
End Namespace
