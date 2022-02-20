using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavnoNadmetanje.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusiNadmetanja",
                columns: table => new
                {
                    StatusNadmetanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivStatusaNadmetanja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusiNadmetanja", x => x.StatusNadmetanjaID);
                });

            migrationBuilder.CreateTable(
                name: "TipoviJavnihNadmetanja",
                columns: table => new
                {
                    TipJavnogNadmetanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTipaJavnogNadmetanja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviJavnihNadmetanja", x => x.TipJavnogNadmetanjaID);
                });

            migrationBuilder.CreateTable(
                name: "JavnaNadmetanja",
                columns: table => new
                {
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremePocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeKraja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PocetnaCenaPoHektaru = table.Column<int>(type: "int", nullable: false),
                    Izuzeto = table.Column<bool>(type: "bit", nullable: false),
                    TipJavnogNadmetanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IzlicitiranaCena = table.Column<int>(type: "int", nullable: false),
                    PeriodZakupa = table.Column<int>(type: "int", nullable: false),
                    BrojUcesnika = table.Column<int>(type: "int", nullable: false),
                    VisinaDopuneDepozita = table.Column<int>(type: "int", nullable: false),
                    Krug = table.Column<int>(type: "int", nullable: false),
                    StatusNadmetanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OvlascenoLiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavnaNadmetanja", x => x.JavnoNadmetanjeID);
                    table.ForeignKey(
                        name: "FK_JavnaNadmetanja_StatusiNadmetanja_StatusNadmetanjaID",
                        column: x => x.StatusNadmetanjaID,
                        principalTable: "StatusiNadmetanja",
                        principalColumn: "StatusNadmetanjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JavnaNadmetanja_TipoviJavnihNadmetanja_TipJavnogNadmetanjaID",
                        column: x => x.TipJavnogNadmetanjaID,
                        principalTable: "TipoviJavnihNadmetanja",
                        principalColumn: "TipJavnogNadmetanjaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licitacije",
                columns: table => new
                {
                    LicitacijaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ogranicenja = table.Column<int>(type: "int", nullable: false),
                    KorakCene = table.Column<int>(type: "int", nullable: false),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RokPrijava = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacije", x => x.LicitacijaID);
                    table.ForeignKey(
                        name: "FK_Licitacije_JavnaNadmetanja_JavnoNadmetanjeID",
                        column: x => x.JavnoNadmetanjeID,
                        principalTable: "JavnaNadmetanja",
                        principalColumn: "JavnoNadmetanjeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusiNadmetanja",
                columns: new[] { "StatusNadmetanjaID", "NazivStatusaNadmetanja" },
                values: new object[,]
                {
                    { new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"), "Prvi krug" },
                    { new Guid("b1ad846b-f76f-4485-8c89-08e2dfebd112"), "Drugi krug sa starim uslovima" },
                    { new Guid("d85b4a71-27e4-4626-9a3e-0412430e03d6"), "Drugi krug sa novim uslovima" }
                });

            migrationBuilder.InsertData(
                table: "TipoviJavnihNadmetanja",
                columns: new[] { "TipJavnogNadmetanjaID", "NazivTipaJavnogNadmetanja" },
                values: new object[,]
                {
                    { new Guid("4246a611-7b2f-429d-a9ba-0e539c81b82f"), "Javna licitacija" },
                    { new Guid("99b6d6ec-4358-4898-936b-31b31d236324"), "Otvaranje zatvorenih ponuda" }
                });

            migrationBuilder.InsertData(
                table: "JavnaNadmetanja",
                columns: new[] { "JavnoNadmetanjeID", "AdresaID", "BrojUcesnika", "Datum", "IzlicitiranaCena", "Izuzeto", "Krug", "KupacID", "OvlascenoLiceID", "PeriodZakupa", "PocetnaCenaPoHektaru", "StatusNadmetanjaID", "TipJavnogNadmetanjaID", "VisinaDopuneDepozita", "VremeKraja", "VremePocetka" },
                values: new object[] { new Guid("208a48a5-371c-4f9d-ac23-18bb176ff8f3"), new Guid("a06f99d2-0ba7-40ff-a241-304a03dfe4be"), 10, new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7500, false, 1, new Guid("8b3b7775-4293-4b41-9ccc-19f9cf694d68"), new Guid("5cfa282f-8324-4a8b-8c23-8d43502ca01e"), 12, 5000, new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"), new Guid("4246a611-7b2f-429d-a9ba-0e539c81b82f"), 500, new DateTime(2022, 2, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 17, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "JavnaNadmetanja",
                columns: new[] { "JavnoNadmetanjeID", "AdresaID", "BrojUcesnika", "Datum", "IzlicitiranaCena", "Izuzeto", "Krug", "KupacID", "OvlascenoLiceID", "PeriodZakupa", "PocetnaCenaPoHektaru", "StatusNadmetanjaID", "TipJavnogNadmetanjaID", "VisinaDopuneDepozita", "VremeKraja", "VremePocetka" },
                values: new object[] { new Guid("13d6ced2-ab84-4132-bf67-e96037f4813d"), new Guid("a06f99d2-0ba7-40ff-a241-304a03dfe4be"), 10, new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000, false, 1, new Guid("8b3b7775-4293-4b41-9ccc-19f9cf694d68"), new Guid("5cfa282f-8324-4a8b-8c23-8d43502ca01e"), 12, 4000, new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"), new Guid("4246a611-7b2f-429d-a9ba-0e539c81b82f"), 400, new DateTime(2022, 2, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 18, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Licitacije",
                columns: new[] { "LicitacijaID", "Broj", "Datum", "Godina", "JavnoNadmetanjeID", "KorakCene", "Ogranicenja", "RokPrijava" },
                values: new object[] { new Guid("a215e4cb-a427-40cf-88b2-8488d140a939"), 1, new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2022, new Guid("208a48a5-371c-4f9d-ac23-18bb176ff8f3"), 100, 1, new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Licitacije",
                columns: new[] { "LicitacijaID", "Broj", "Datum", "Godina", "JavnoNadmetanjeID", "KorakCene", "Ogranicenja", "RokPrijava" },
                values: new object[] { new Guid("1de13266-85e8-4120-8b1f-daacc32c5811"), 2, new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2022, new Guid("208a48a5-371c-4f9d-ac23-18bb176ff8f3"), 200, 1, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_JavnaNadmetanja_StatusNadmetanjaID",
                table: "JavnaNadmetanja",
                column: "StatusNadmetanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_JavnaNadmetanja_TipJavnogNadmetanjaID",
                table: "JavnaNadmetanja",
                column: "TipJavnogNadmetanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Licitacije_JavnoNadmetanjeID",
                table: "Licitacije",
                column: "JavnoNadmetanjeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitacije");

            migrationBuilder.DropTable(
                name: "JavnaNadmetanja");

            migrationBuilder.DropTable(
                name: "StatusiNadmetanja");

            migrationBuilder.DropTable(
                name: "TipoviJavnihNadmetanja");
        }
    }
}
