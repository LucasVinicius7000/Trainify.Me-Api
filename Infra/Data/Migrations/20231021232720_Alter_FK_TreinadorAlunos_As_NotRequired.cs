using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class Alter_FK_TreinadorAlunos_As_NotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Treinadores_TreinadorId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Treinadores_TreinadorId",
                table: "Alunos",
                column: "TreinadorId",
                principalTable: "Treinadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Treinadores_TreinadorId",
                table: "Alunos");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Treinadores_TreinadorId",
                table: "Alunos",
                column: "TreinadorId",
                principalTable: "Treinadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
