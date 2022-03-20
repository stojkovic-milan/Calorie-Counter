using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieCounter.Migrations
{
    public partial class V21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porcije_Hrana_HranaID",
                table: "Porcije");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hrana",
                table: "Hrana");

            migrationBuilder.RenameTable(
                name: "Hrana",
                newName: "Osoba");

            migrationBuilder.AddColumn<int>(
                name: "FizAktivnost",
                table: "Osobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Godine",
                table: "Osobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Pol",
                table: "Osobe",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrenutnoKg",
                table: "Osobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Osoba",
                table: "Osoba",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Porcije_Osoba_HranaID",
                table: "Porcije",
                column: "HranaID",
                principalTable: "Osoba",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porcije_Osoba_HranaID",
                table: "Porcije");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Osoba",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "FizAktivnost",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "Godine",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "Pol",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "TrenutnoKg",
                table: "Osobe");

            migrationBuilder.RenameTable(
                name: "Osoba",
                newName: "Hrana");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hrana",
                table: "Hrana",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Porcije_Hrana_HranaID",
                table: "Porcije",
                column: "HranaID",
                principalTable: "Hrana",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
