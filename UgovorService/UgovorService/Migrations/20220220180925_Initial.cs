using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UgovorService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipGarancijeEnt",
                columns: table => new
                {
                    TipID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipGarancijeEnt", x => x.TipID);
                });

            migrationBuilder.CreateTable(
                name: "UgovorEnt",
                columns: table => new
                {
                    UgovorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipGarancijeEntTipID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DokumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZavodniBr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumZavo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rok = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumPot = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UgovorEnt", x => x.UgovorID);
                    table.ForeignKey(
                        name: "FK_UgovorEnt_TipGarancijeEnt_TipGarancijeEntTipID",
                        column: x => x.TipGarancijeEntTipID,
                        principalTable: "TipGarancijeEnt",
                        principalColumn: "TipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TipGarancijeEnt",
                columns: new[] { "TipID", "Tip" },
                values: new object[,]
                {
                    { new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"), "Mesecna" },
                    { new Guid("5a36d34e-1620-40c6-ab8c-b96b0be49332"), "Kvartalna" }
                });

            migrationBuilder.InsertData(
                table: "UgovorEnt",
                columns: new[] { "UgovorID", "DatumPot", "DatumZavo", "DokumentID", "JavnoNadmetanjeID", "KupacID", "Mesto", "Rok", "TipGarancijeEntTipID", "TipID", "ZavodniBr" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(2020, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"), new Guid("bf33a825-ad63-4e04-a812-74ffbebdadbb"), new Guid("7091a32b-7d21-43a7-9b41-a0419ac8edcc"), "Novi Sad", new DateTime(2023, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"), "471/RS" },
                    { new Guid("6f4967b3-beb3-4acf-95aa-488b16f8fc9a"), new DateTime(2019, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"), new Guid("5a36d34e-1620-40c6-ab8c-b96b0be49333"), new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"), "Beograd", new DateTime(2022, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("5a36d34e-1620-40c6-ab8c-b96b0be49332"), "471/RS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UgovorEnt_TipGarancijeEntTipID",
                table: "UgovorEnt",
                column: "TipGarancijeEntTipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UgovorEnt");

            migrationBuilder.DropTable(
                name: "TipGarancijeEnt");
        }
    }
}
