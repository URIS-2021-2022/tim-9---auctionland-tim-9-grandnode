using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace galic_korisnik.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    korisnikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipKorisnikaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.korisnikId);
                });

            migrationBuilder.CreateTable(
                name: "TipKorisnika",
                columns: table => new
                {
                    tipKorisnikaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    uloga = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipKorisnika", x => x.tipKorisnikaId);
                });

            migrationBuilder.InsertData(
                table: "Korisnik",
                columns: new[] { "korisnikId", "Salt", "ime", "korisnickoIme", "lozinka", "prezime", "tipKorisnikaId" },
                values: new object[] { new Guid("f7a20259-5aeb-3135-64ea-32cf7a96b98a"), null, "Petar", "PPetrovic", "123456", "Petrovic", new Guid("ce4a6a8a-b25d-d5d0-9364-3dee56521821") });

            migrationBuilder.InsertData(
                table: "Korisnik",
                columns: new[] { "korisnikId", "Salt", "ime", "korisnickoIme", "lozinka", "prezime", "tipKorisnikaId" },
                values: new object[] { new Guid("e8920f41-e035-da6d-27d1-ee8909f6271d"), null, "Marko", "MMarkovic", "123456", "Markovic", new Guid("22caf793-fbaa-a3f5-8266-7fc3dcc798dc") });

            migrationBuilder.InsertData(
                table: "TipKorisnika",
                columns: new[] { "tipKorisnikaId", "uloga" },
                values: new object[] { new Guid("9d8004cb-fad6-40a9-9d9e-978ff4f98481"), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "TipKorisnika");
        }
    }
}
