using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieCounter.Migrations
{
    public partial class V23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dani_Osobe_OsobaID",
                table: "Dani");

            migrationBuilder.AlterColumn<int>(
                name: "OsobaID",
                table: "Dani",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dani_Osobe_OsobaID",
                table: "Dani",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dani_Osobe_OsobaID",
                table: "Dani");

            migrationBuilder.AlterColumn<int>(
                name: "OsobaID",
                table: "Dani",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Dani_Osobe_OsobaID",
                table: "Dani",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
