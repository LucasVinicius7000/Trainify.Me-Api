using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class Add_C_RefreshToken_Expires_T_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativa_Atividade_AtividadeId",
                table: "Alternativa");

            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Alternativa_AlternativaCorretaId",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Aulas_AulaId",
                table: "Atividade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atividade",
                table: "Atividade");

            migrationBuilder.RenameTable(
                name: "Atividade",
                newName: "Atividades");

            migrationBuilder.RenameIndex(
                name: "IX_Atividade_AulaId",
                table: "Atividades",
                newName: "IX_Atividades_AulaId");

            migrationBuilder.RenameIndex(
                name: "IX_Atividade_AlternativaCorretaId",
                table: "Atividades",
                newName: "IX_Atividades_AlternativaCorretaId");

            migrationBuilder.AddColumn<string>(
                name: "Expires",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atividades",
                table: "Atividades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativa_Atividades_AtividadeId",
                table: "Alternativa",
                column: "AtividadeId",
                principalTable: "Atividades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Alternativa_AlternativaCorretaId",
                table: "Atividades",
                column: "AlternativaCorretaId",
                principalTable: "Alternativa",
                principalColumn: "Id");

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
                name: "FK_Alternativa_Atividades_AtividadeId",
                table: "Alternativa");

            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Alternativa_AlternativaCorretaId",
                table: "Atividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Aulas_AulaId",
                table: "Atividades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atividades",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Expires",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Atividades",
                newName: "Atividade");

            migrationBuilder.RenameIndex(
                name: "IX_Atividades_AulaId",
                table: "Atividade",
                newName: "IX_Atividade_AulaId");

            migrationBuilder.RenameIndex(
                name: "IX_Atividades_AlternativaCorretaId",
                table: "Atividade",
                newName: "IX_Atividade_AlternativaCorretaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atividade",
                table: "Atividade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativa_Atividade_AtividadeId",
                table: "Alternativa",
                column: "AtividadeId",
                principalTable: "Atividade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Alternativa_AlternativaCorretaId",
                table: "Atividade",
                column: "AlternativaCorretaId",
                principalTable: "Alternativa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Aulas_AulaId",
                table: "Atividade",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
