using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dokument_AK.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DokumentEnt",
                columns: table => new
                {
                    DokumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusDokID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZavodniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDonosenjaDokumenta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentEnt", x => x.DokumentID);
                    
                });

            migrationBuilder.CreateTable(
                name: "EksterniDokumentEnt",
                columns: table => new
                {
                    DokumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Izmenjen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EksterniDokumentEnt", x => x.DokumentID);
                });

            migrationBuilder.CreateTable(
                name: "InterniDokumentEnt",
                columns: table => new
                {
                    DokumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Izmenjen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterniDokumentEnt", x => x.DokumentID);
                });

            migrationBuilder.CreateTable(
                name: "StatusDokumentaEnt",
                columns: table => new
                {
                    StatusDokID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usvojen = table.Column<bool>(type: "bit", nullable: false),
                    Odbijen = table.Column<bool>(type: "bit", nullable: false),
                    Otvoren = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusDokumentaEnt", x => x.StatusDokID);
                });

            migrationBuilder.InsertData(
                table: "DokumentEnt",
                columns: new[] { "DokumentID", "Datum", "DatumDonosenjaDokumenta", "StatusDokID", "ZavodniBroj" },
                values: new object[,]
                {
                    { new Guid("1794d8c7-6c5c-4725-9d92-d819bdc07773"), new DateTime(2021, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2f530032-429e-4be7-b202-d800876d393d"), "15548/RS7" },
                    { new Guid("cfe84b37-bb6d-498d-a546-5dee8758ed1a"), new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "17748/RS7" }
                });

            migrationBuilder.InsertData(
                table: "EksterniDokumentEnt",
                columns: new[] { "DokumentID", "Izmenjen" },
                values: new object[] { new Guid("cfe84b37-bb6d-498d-a546-5dee8758ed1a"), false });

            migrationBuilder.InsertData(
                table: "InterniDokumentEnt",
                columns: new[] { "DokumentID", "Izmenjen" },
                values: new object[] { new Guid("1794d8c7-6c5c-4725-9d92-d819bdc07773"), true });

            migrationBuilder.InsertData(
                table: "StatusDokumentaEnt",
                columns: new[] { "StatusDokID", "Odbijen", "Otvoren", "Usvojen" },
                values: new object[,]
                {
                    { new Guid("2f530032-429e-4be7-b202-d800876d393d"), false, false, true },
                    { new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), false, true, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DokumentEnt");

            migrationBuilder.DropTable(
                name: "EksterniDokumentEnt");

            migrationBuilder.DropTable(
                name: "InterniDokumentEnt");

            migrationBuilder.DropTable(
                name: "StatusDokumentaEnt");
        }
    }
}
