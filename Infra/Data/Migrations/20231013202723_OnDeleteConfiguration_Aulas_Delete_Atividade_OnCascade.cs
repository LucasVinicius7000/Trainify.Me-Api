using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class OnDeleteConfiguration_Aulas_Delete_Atividade_OnCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id");
        }
    }
}
