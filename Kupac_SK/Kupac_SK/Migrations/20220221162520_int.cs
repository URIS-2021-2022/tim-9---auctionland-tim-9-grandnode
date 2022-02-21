using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kupac_SK.Migrations
{
    public partial class @int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fizLica",
                columns: table => new
                {
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FizPravno = table.Column<bool>(type: "bit", nullable: false),
                    OstvarenaPovrsina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zabrana = table.Column<bool>(type: "bit", nullable: false),
                    PocetakZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzinaZabrane = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrestanakZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvlascenoLiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrioritetID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrTel1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrTel2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresaID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UplataID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fizLica", x => x.KupacID);
                });

            migrationBuilder.CreateTable(
                name: "kontaktOsoba",
                columns: table => new
                {
                    KontaktOsobaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funkcija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kontaktOsoba", x => x.KontaktOsobaID);
                });

            migrationBuilder.CreateTable(
                name: "pravnaLica",
                columns: table => new
                {
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatBr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktOsoba = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FizPravno = table.Column<bool>(type: "bit", nullable: false),
                    OstvarenaPovrsina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zabrana = table.Column<bool>(type: "bit", nullable: false),
                    PocetakZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzinaZabrane = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrestanakZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvlascenoLiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrioritetID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrTel1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrTel2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresaID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UplataID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pravnaLica", x => x.KupacID);
                });

            migrationBuilder.CreateTable(
                name: "prioriteti",
                columns: table => new
                {
                    PrioritetID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpisPrioriteta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prioriteti", x => x.PrioritetID);
                });

            migrationBuilder.InsertData(
                table: "fizLica",
                columns: new[] { "KupacID", "AdresaID", "BrTel1", "BrTel2", "BrojRacuna", "DuzinaZabrane", "Email", "FizPravno", "Ime", "JMBG", "OstvarenaPovrsina", "OvlascenoLiceID", "PocetakZabrane", "PrestanakZabrane", "Prezime", "PrioritetID", "UplataID", "Zabrana" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "bulevar 13", "064111558", "225447", "170000000082", "0", "imejl@gmail.com", true, "Sara", "160999979894", "15000", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Kijanovic", new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), "yyyyyyyyyyyyyyy", false });

            migrationBuilder.InsertData(
                table: "kontaktOsoba",
                columns: new[] { "KontaktOsobaID", "Funkcija", "Ime", "Prezime", "Telefon" },
                values: new object[,]
                {
                    { new Guid("c658a3cf-df57-4818-8a38-00b42bccc8a1"), "Zastupnik1", "Sara", "Kijanovic", " 12345687" },
                    { new Guid("b60955b8-fb83-4947-a72a-ec7050cb3454"), "Zastupnik2", "Teodora", "Kijanovic", " 18915517" }
                });

            migrationBuilder.InsertData(
                table: "pravnaLica",
                columns: new[] { "KupacID", "AdresaID", "BrTel1", "BrTel2", "BrojRacuna", "DuzinaZabrane", "Email", "Faks", "FizPravno", "KontaktOsoba", "MatBr", "Naziv", "OstvarenaPovrsina", "OvlascenoLiceID", "PocetakZabrane", "PrestanakZabrane", "PrioritetID", "UplataID", "Zabrana" },
                values: new object[] { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "bulevar 13", "064111558", "225447", "170000000082", "0", "imejl@gmail.com", "741258", false, new Guid("b60955b8-fb83-4947-a72a-ec7050cb3454"), "12345678", "doo x", "15000", new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "xxxx", false });

            migrationBuilder.InsertData(
                table: "prioriteti",
                columns: new[] { "PrioritetID", "OpisPrioriteta" },
                values: new object[,]
                {
                    { new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), "drugi testni prioritet" },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "prvi testni prioritet" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fizLica");

            migrationBuilder.DropTable(
                name: "kontaktOsoba");

            migrationBuilder.DropTable(
                name: "pravnaLica");

            migrationBuilder.DropTable(
                name: "prioriteti");
        }
    }
}
