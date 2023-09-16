using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Me_Api.Infra.Data.Migrations
{
    public partial class Alter_C_ExpiresAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expires",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Expires",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
