Imports Microsoft.EntityFrameworkCore.Migrations

Namespace Maintenance.Server.Data.Migrations
    Public Partial Class Initial
        Inherits Migration

        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateTable(
                name:="Clienti",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="int", nullable:=False).
                        Annotation("SqlServer:Identity", "1, 1"),
                    .Nome = table.Column(Of String)(type:="nvarchar(max)", nullable:=False),
                    .Indirizzo = table.Column(Of String)(type:="nvarchar(max)", nullable:=True)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Clienti", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateTable(
                name:="Carrelli",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="int", nullable:=False).
                        Annotation("SqlServer:Identity", "1, 1"),
                    .NumeroSerie = table.Column(Of String)(type:="nvarchar(max)", nullable:=False),
                    .ClienteId = table.Column(Of Integer)(type:="int", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Carrelli", Function(x) x.Id)
                    table.ForeignKey(
                        name:="FK_Carrelli_Clienti_ClienteId",
                        column:=Function(x) x.ClienteId,
                        principalTable:="Clienti",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)

            migrationBuilder.CreateTable(
                name:="Operatori",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="int", nullable:=False).
                        Annotation("SqlServer:Identity", "1, 1"),
                    .Nome = table.Column(Of String)(type:="nvarchar(max)", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Operatori", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateTable(
                name:="Skills",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="int", nullable:=False).
                        Annotation("SqlServer:Identity", "1, 1"),
                    .Nome = table.Column(Of String)(type:="nvarchar(max)", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Skills", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateTable(
                name:="OperatoreSkills",
                columns:=Function(table) New With {
                    .OperatoreId = table.Column(Of Integer)(type:="int", nullable:=False),
                    .SkillId = table.Column(Of Integer)(type:="int", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_OperatoreSkills", Function(x) New With {x.OperatoreId, x.SkillId})
                    table.ForeignKey(
                        name:="FK_OperatoreSkills_Operatori_OperatoreId",
                        column:=Function(x) x.OperatoreId,
                        principalTable:="Operatori",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                    table.ForeignKey(
                        name:="FK_OperatoreSkills_Skills_SkillId",
                        column:=Function(x) x.SkillId,
                        principalTable:="Skills",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(name:="OperatoreSkills")
            migrationBuilder.DropTable(name:="Skills")
            migrationBuilder.DropTable(name:="Operatori")
            migrationBuilder.DropTable(name:="Carrelli")
            migrationBuilder.DropTable(name:="Clienti")
        End Sub
    End Class
End Namespace

