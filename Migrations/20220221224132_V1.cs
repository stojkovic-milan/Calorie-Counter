using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieCounter.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hrana",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kalorije = table.Column<int>(type: "int", nullable: false),
                    UgljeniHidrati = table.Column<int>(type: "int", nullable: false),
                    Proteini = table.Column<int>(type: "int", nullable: false),
                    Masti = table.Column<int>(type: "int", nullable: false),
                    VelPorcije = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hrana", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Osobe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CiljKg = table.Column<int>(type: "int", nullable: false),
                    CiljKcal = table.Column<int>(type: "int", nullable: false),
                    Visina = table.Column<int>(type: "int", nullable: false),
                    PocetakKg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dani",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kalorije = table.Column<int>(type: "int", nullable: false),
                    UgljeniHidrati = table.Column<int>(type: "int", nullable: false),
                    Proteini = table.Column<int>(type: "int", nullable: false),
                    Masti = table.Column<int>(type: "int", nullable: false),
                    Kilaza = table.Column<int>(type: "int", nullable: false),
                    OsobaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dani", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dani_Osobe_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osobe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obroci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kalorije = table.Column<int>(type: "int", nullable: false),
                    Masti = table.Column<int>(type: "int", nullable: false),
                    UgljeniHidrati = table.Column<int>(type: "int", nullable: false),
                    Proteini = table.Column<int>(type: "int", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DanID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obroci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obroci_Dani_DanID",
                        column: x => x.DanID,
                        principalTable: "Dani",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Porcije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Velicina = table.Column<int>(type: "int", nullable: false),
                    HranaID = table.Column<int>(type: "int", nullable: false),
                    ObrokID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porcije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Porcije_Hrana_HranaID",
                        column: x => x.HranaID,
                        principalTable: "Hrana",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Porcije_Obroci_ObrokID",
                        column: x => x.ObrokID,
                        principalTable: "Obroci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dani_OsobaID",
                table: "Dani",
                column: "OsobaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obroci_DanID",
                table: "Obroci",
                column: "DanID");

            migrationBuilder.CreateIndex(
                name: "IX_Porcije_HranaID",
                table: "Porcije",
                column: "HranaID");

            migrationBuilder.CreateIndex(
                name: "IX_Porcije_ObrokID",
                table: "Porcije",
                column: "ObrokID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Porcije");

            migrationBuilder.DropTable(
                name: "Hrana");

            migrationBuilder.DropTable(
                name: "Obroci");

            migrationBuilder.DropTable(
                name: "Dani");

            migrationBuilder.DropTable(
                name: "Osobe");
        }
    }
}
