using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class Create_T_CursosEmAndamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CursosEmAndamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    CursoBaseId = table.Column<int>(type: "int", nullable: false),
                    AulaAtualId = table.Column<int>(type: "int", nullable: false),
                    UserIdMatriculador = table.Column<int>(type: "int", nullable: false),
                    ProgressoAulaAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatriculadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IniciadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcluidoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosEmAndamento", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursosEmAndamento");
        }
    }
}
