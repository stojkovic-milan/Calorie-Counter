using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieCounter.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VelPorcije",
                table: "Hrana");

            migrationBuilder.AddColumn<int>(
                name: "Kalorije",
                table: "Porcije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Masti",
                table: "Porcije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Proteini",
                table: "Porcije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UgljeniHidrati",
                table: "Porcije",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kalorije",
                table: "Porcije");

            migrationBuilder.DropColumn(
                name: "Masti",
                table: "Porcije");

            migrationBuilder.DropColumn(
                name: "Proteini",
                table: "Porcije");

            migrationBuilder.DropColumn(
                name: "UgljeniHidrati",
                table: "Porcije");

            migrationBuilder.AddColumn<int>(
                name: "VelPorcije",
                table: "Hrana",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
