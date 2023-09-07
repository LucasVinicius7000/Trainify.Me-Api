using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class AddFK_Organizacao_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Organizacoes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizacoes_UserId",
                table: "Organizacoes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizacoes_AspNetUsers_UserId",
                table: "Organizacoes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizacoes_AspNetUsers_UserId",
                table: "Organizacoes");

            migrationBuilder.DropIndex(
                name: "IX_Organizacoes_UserId",
                table: "Organizacoes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Organizacoes");
        }
    }
}
