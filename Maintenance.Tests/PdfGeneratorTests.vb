Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Maintenance.Server.Services
Imports Maintenance.Shared.Models

<TestClass>
Public Class PdfGeneratorTests
    <TestMethod>
    Public Sub Generate_Pdf_Contains_Data()
        Dim ticket As New Ticket With {.Titolo = "Tit", .DataRichiesta = DateTime.Now, .Cliente = New Cliente With {.Nome = "ACME"}}
        Dim intervento As New Intervento With {.Ticket = ticket, .DataInizio = DateTime.Now}
        intervento.InterventoChecks = New List(Of InterventoCheck) From {
            New InterventoCheck With {.CheckItem = New CheckItem With {.Descrizione = "Check"}, .Esito = "OK"}}
        Dim bytes = InterventoPdfGenerator.Generate(intervento)
        Assert.IsNotNull(bytes)
        Assert.IsTrue(bytes.Length > 100)
    End Sub
End Class
