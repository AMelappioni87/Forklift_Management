Imports QuestPDF.Fluent
Imports QuestPDF.Infrastructure
Imports Maintenance.Shared.Models

Namespace Maintenance.Server.Services
    Public Class InterventoPdfGenerator
        Public Shared Function Generate(intervento As Intervento) As Byte()
            Dim document = Document.Create(Sub(container)
                container.Page(Sub(page)
                    page.Margin(20)
                    page.Header().Text($"Foglio intervento #{intervento.Id}").FontSize(20).Bold()
                    page.Content().Column(Sub(column)
                        column.Item().Text($"Cliente: {intervento.Ticket.Cliente.Nome}")
                        If intervento.Ticket.Carrello IsNot Nothing Then
                            column.Item().Text($"Carrello: {intervento.Ticket.Carrello.NumeroSerie}")
                        End If
                        column.Item().Text($"Ticket: {intervento.Ticket.Titolo}")

                        column.Item().Text("Controlli eseguiti:").Bold()
                        column.Item().Table(Sub(t)
                            t.ColumnsDefinition(Sub(c)
                                c.RelativeColumn()
                                c.ConstantColumn(60)
                            End Sub)
                            t.Header(Sub(h)
                                h.Cell().Element(AddressOf StyleHeader).Text("Check")
                                h.Cell().Element(AddressOf StyleHeader).Text("Esito")
                            End Sub)
                            For Each c In intervento.InterventoChecks
                                t.Cell().Text(c.CheckItem.Descrizione)
                                t.Cell().Text(If(c.Esito, "OK", "NO"))
                            Next
                        End Sub)

                        column.Item().Text("Ricambi utilizzati:").Bold()
                        column.Item().Table(Sub(t)
                            t.ColumnsDefinition(Sub(c)
                                c.RelativeColumn()
                                c.ConstantColumn(60)
                            End Sub)
                            t.Header(Sub(h)
                                h.Cell().Element(AddressOf StyleHeader).Text("Ricambio")
                                h.Cell().Element(AddressOf StyleHeader).Text("Qta")
                            End Sub)
                            For Each r In intervento.Ricambi
                                t.Cell().Text(r.Descrizione)
                                t.Cell().Text(r.Quantita.ToString())
                            Next
                        End Sub)

                        column.Item().Text("Manodopera:").Bold()
                        column.Item().Table(Sub(t)
                            t.ColumnsDefinition(Sub(c)
                                c.RelativeColumn()
                                c.ConstantColumn(60)
                            End Sub)
                            t.Header(Sub(h)
                                h.Cell().Element(AddressOf StyleHeader).Text("Descrizione")
                                h.Cell().Element(AddressOf StyleHeader).Text("Ore")
                            End Sub)
                            For Each m In intervento.Manodopera
                                t.Cell().Text(m.Descrizione)
                                t.Cell().Text(m.Ore.ToString())
                            Next
                        End Sub)

                        column.Item().Text($"Note: {intervento.Note}")
                    End Sub)
                    page.Footer().Column(Sub(col)
                        col.Item().Text($"Data prossimo controllo: {intervento.Ticket?.DataChiusura:d}")
                        If intervento.FirmaTecnico IsNot Nothing Then
                            col.Item().Image(intervento.FirmaTecnico).Height(50)
                        End If
                        If intervento.FirmaCliente IsNot Nothing Then
                            col.Item().Image(intervento.FirmaCliente).Height(50)
                        End If
                    End Sub)
                End Sub)
            End Sub)
            Return document.GeneratePdf()
        End Function

        Private Shared Function StyleHeader(elem As IContainer) As IContainer
            Return elem.DefaultTextStyle(Function(x) x.SemiBold())
        End Function
    End Class
End Namespace
