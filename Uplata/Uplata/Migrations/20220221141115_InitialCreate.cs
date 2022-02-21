using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KursneListe",
                columns: table => new
                {
                    KursnaListaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valuta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vrednost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursneListe", x => x.KursnaListaID);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PozivNaBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iznos = table.Column<float>(type: "real", nullable: false),
                    SvrhaUplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KursnaListaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataID);
                    table.ForeignKey(
                        name: "FK_Uplate_KursneListe_KursnaListaID",
                        column: x => x.KursnaListaID,
                        principalTable: "KursneListe",
                        principalColumn: "KursnaListaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KursneListe",
                columns: new[] { "KursnaListaID", "Datum", "Valuta", "Vrednost" },
                values: new object[] { new Guid("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"), new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "RSD", 1234f });

            migrationBuilder.InsertData(
                table: "KursneListe",
                columns: new[] { "KursnaListaID", "Datum", "Valuta", "Vrednost" },
                values: new object[] { new Guid("f9d0d94c-a332-4437-a8d1-e2b64349e0ad"), new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "RSD", 4321f });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "KupacID", "KursnaListaID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("556228f6-4afd-4a7a-9767-371b608abaab"), "236541", new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4321f, new Guid("13d6ced2-ab84-4132-bf67-e96037f4813d"), new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"), new Guid("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"), "147852", "Uplata javnog nadmetanja" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "KupacID", "KursnaListaID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("3c08638f-86c4-4744-a6fa-e0863adccdc6"), "236541", new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4321f, new Guid("13d6ced2-ab84-4132-bf67-e96037f4813d"), new Guid("d371e7b7-9b08-4831-a300-0df4c200a3d8"), new Guid("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"), "147852", "Uplata javnog nadmetanja" });

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KursnaListaID",
                table: "Uplate",
                column: "KursnaListaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "KursneListe");
        }
    }
}
