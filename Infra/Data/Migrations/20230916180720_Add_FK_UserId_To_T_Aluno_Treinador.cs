using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class Add_FK_UserId_To_T_Aluno_Treinador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizacoes_Users_UserId",
                table: "Organizacoes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Treinadores",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Alunos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Treinadores_UserId",
                table: "Treinadores",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_UserId",
                table: "Alunos",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Users_UserId",
                table: "Alunos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizacoes_Users_UserId",
                table: "Organizacoes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treinadores_Users_UserId",
                table: "Treinadores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Users_UserId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizacoes_Users_UserId",
                table: "Organizacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinadores_Users_UserId",
                table: "Treinadores");

            migrationBuilder.DropIndex(
                name: "IX_Treinadores_UserId",
                table: "Treinadores");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_UserId",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Treinadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizacoes_Users_UserId",
                table: "Organizacoes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
