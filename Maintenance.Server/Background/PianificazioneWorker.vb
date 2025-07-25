Imports System.Threading
Imports System.Net.Mail
Imports System.Net
Imports Microsoft.Extensions.Configuration
Imports Microsoft.EntityFrameworkCore
Imports Maintenance.Server.Data
Imports Maintenance.Shared.Models

Namespace Maintenance.Server.Background
    ''' <summary>
    ''' Servizio in background che controlla ogni giorno le pianificazioni
    ''' e crea i ticket di manutenzione programmata notificando via email.
    ''' </summary>
    Public Class PianificazioneWorker
        Implements IDisposable

        Private _timer As Timer
        Private ReadOnly _connectionString As String
        Private ReadOnly _smtpHost As String
        Private ReadOnly _smtpPort As Integer
        Private ReadOnly _smtpUser As String
        Private ReadOnly _smtpPass As String
        Private ReadOnly _smtpFrom As String

        Public Sub New()
            Dim config = New ConfigurationBuilder() _
                .SetBasePath(AppContext.BaseDirectory) _
                .AddJsonFile("appsettings.json") _
                .Build()

            _connectionString = config.GetConnectionString("DefaultConnection")
            _smtpHost = config("Smtp:Host")
            _smtpPort = Integer.Parse(config("Smtp:Port"))
            _smtpUser = config("Smtp:Username")
            _smtpPass = config("Smtp:Password")
            _smtpFrom = config("Smtp:From")
        End Sub

        ''' <summary>
        ''' Avvia il timer che esegue il controllo subito e poi ogni 24 ore.
        ''' </summary>
        Public Sub Start()
            _timer = New Timer(AddressOf Execute, Nothing, TimeSpan.Zero, TimeSpan.FromDays(1))
        End Sub

        Private Sub Execute(state As Object)
            Try
                Using ctx = CreateContext()
                    Dim now = DateTime.Now
                    ' Seleziona le pianificazioni scadute
                    Dim scadute = ctx.Pianificazioni _
                        .Include(Function(p) p.Carrello) _
                        .ThenInclude(Function(c) c.Cliente) _
                        .Where(Function(p) p.Data <= now) _
                        .ToList()

                    For Each p In scadute
                        Dim operatore = GetOperatoreDisponibile(ctx)
                        If operatore Is Nothing Then Continue For

                        ' Crea un nuovo ticket di manutenzione programmata
                        Dim ticket = New Ticket() With {
                            .Titolo = "manutenzione programmata",
                            .Descrizione = p.Descrizione,
                            .DataApertura = now,
                            .ClienteId = p.Carrello.ClienteId,
                            .CarrelloId = p.CarrelloId,
                            .Stato = TicketStatus.Aperto
                        }
                        ctx.Tickets.Add(ticket)
                        ctx.SaveChanges()

                        ' Associa un primo intervento al tecnico scelto
                        Dim intervento = New Intervento() With {
                            .Data = now,
                            .Note = "Intervento programmato",
                            .TicketId = ticket.Id,
                            .OperatoreId = operatore.Id
                        }
                        ctx.Interventi.Add(intervento)
                        ctx.SaveChanges()

                        ' Invia email di notifica
                        If Not String.IsNullOrEmpty(operatore.Email) Then
                            SendEmail(operatore.Email, "Nuovo ticket assegnato", $"Ti è stato assegnato il ticket {ticket.Id}.")
                        End If
                        If p.Carrello?.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(p.Carrello.Cliente.Email) Then
                            SendEmail(p.Carrello.Cliente.Email, "Apertura ticket manutenzione", $"E' stato aperto il ticket {ticket.Id}.")
                        End If
                    Next
                End Using
            Catch ex As Exception
                ' Gestire log in produzione
                Console.WriteLine($"Errore PianificazioneWorker: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Seleziona l'operatore con il minor numero di ticket aperti.
        ''' </summary>
        Private Function GetOperatoreDisponibile(ctx As MaintenanceDbContext) As Operatore
            Dim operatori = ctx.Operatori.Include(Function(o) o.OperatoreSkills).ToList()
            Dim migliore As Operatore = Nothing
            Dim minTicket As Integer = Integer.MaxValue

            For Each op In operatori
                Dim aperti = ctx.Interventi _
                    .Include(Function(i) i.Ticket) _
                    .Where(Function(i) i.OperatoreId = op.Id AndAlso i.Ticket.DataChiusura Is Nothing) _
                    .Select(Function(i) i.TicketId).Distinct().Count()

                If aperti < minTicket Then
                    minTicket = aperti
                    migliore = op
                End If
            Next

            Return migliore
        End Function

        Private Sub SendEmail(address As String, subject As String, body As String)
            Using client As New SmtpClient(_smtpHost, _smtpPort)
                client.Credentials = New NetworkCredential(_smtpUser, _smtpPass)
                client.EnableSsl = True
                Dim mail As New MailMessage(_smtpFrom, address, subject, body)
                client.Send(mail)
            End Using
        End Sub

        Private Function CreateContext() As MaintenanceDbContext
            Dim options = New DbContextOptionsBuilder(Of MaintenanceDbContext)() _
                .UseSqlServer(_connectionString).Options
            Return New MaintenanceDbContext(options)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            _timer?.Dispose()
        End Sub
    End Class
End Namespace
