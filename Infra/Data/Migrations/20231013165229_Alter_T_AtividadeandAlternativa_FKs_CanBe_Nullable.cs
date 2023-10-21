using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class Alter_T_AtividadeandAlternativa_FKs_CanBe_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_AlternativaCorretaId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_AulaId",
                table: "Atividades");

            migrationBuilder.AlterColumn<int>(
                name: "AulaId",
                table: "Atividades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AlternativaCorretaId",
                table: "Atividades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AlternativaCorretaId",
                table: "Atividades",
                column: "AlternativaCorretaId",
                unique: true,
                filter: "[AlternativaCorretaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AulaId",
                table: "Atividades",
                column: "AulaId",
                unique: true,
                filter: "[AulaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_AlternativaCorretaId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_AulaId",
                table: "Atividades");

            migrationBuilder.AlterColumn<int>(
                name: "AulaId",
                table: "Atividades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlternativaCorretaId",
                table: "Atividades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AlternativaCorretaId",
                table: "Atividades",
                column: "AlternativaCorretaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AulaId",
                table: "Atividades",
                column: "AulaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
