using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parcela.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KatastarskaOpstina",
                columns: table => new
                {
                    KatastarskaOpstinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivKatastarskeOpstine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatastarskaOpstina", x => x.KatastarskaOpstinaID);
                });

            migrationBuilder.CreateTable(
                name: "Klasa",
                columns: table => new
                {
                    KlasaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivKlase = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasa", x => x.KlasaID);
                });

            migrationBuilder.CreateTable(
                name: "Kultura",
                columns: table => new
                {
                    KulturaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivKulture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpiaKulture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kultura", x => x.KulturaID);
                });

            migrationBuilder.CreateTable(
                name: "OblikSvojine",
                columns: table => new
                {
                    OblikSvojineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivOblikaSvojine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpisOblikaSvojine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OblikSvojine", x => x.OblikSvojineID);
                });

            migrationBuilder.CreateTable(
                name: "Obradivost",
                columns: table => new
                {
                    ObradivostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivObradivosti = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obradivost", x => x.ObradivostID);
                });

            migrationBuilder.CreateTable(
                name: "Odvodnjavanje",
                columns: table => new
                {
                    OdvodnjavanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivOdvodnjavanja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odvodnjavanje", x => x.OdvodnjavanjeID);
                });

            migrationBuilder.CreateTable(
                name: "Parcela",
                columns: table => new
                {
                    ParcelaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KorisnikParceleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Povrsina = table.Column<int>(type: "int", nullable: false),
                    BrojParcele = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KatastarskaOpstinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojListaNepokretnosti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KulturaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlasaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObradivostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZasticenaZonaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OblikSvojineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OdvodnjavanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObradivostStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KulturaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KlasaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZasticenaZonaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdvodnjavanjeStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcela", x => x.ParcelaID);
                });

            migrationBuilder.CreateTable(
                name: "ZasticenaZona",
                columns: table => new
                {
                    ZasticenaZonaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivZasticeneZone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZasticenaZona", x => x.ZasticenaZonaID);
                });

            migrationBuilder.CreateTable(
                name: "DeoParcele",
                columns: table => new
                {
                    DeoParceleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdealniDeoParcele = table.Column<int>(type: "int", nullable: false),
                    StvarniDeoParcele = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeoParcele", x => x.DeoParceleID);
                    table.ForeignKey(
                        name: "FK_DeoParcele_Parcela_ParcelaID",
                        column: x => x.ParcelaID,
                        principalTable: "Parcela",
                        principalColumn: "ParcelaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstina",
                columns: new[] { "KatastarskaOpstinaID", "NazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("7d03a946-40a3-41e8-8feb-dbe9381d2170"), "Čantavir" },
                    { new Guid("5a20c276-5b98-4e24-92b4-0996cb51b293"), "Bački Vinogradi" },
                    { new Guid("24742b99-32c6-4999-b0a7-757a178f9ee7"), "Bikovo" },
                    { new Guid("55c5e423-e70c-452f-a89f-42c412163b76"), "Đuđin" },
                    { new Guid("714f831b-b86b-443d-abf5-f248694a8b2e"), "Žednik" },
                    { new Guid("835bd51d-80e3-4c28-9e34-3951bac861c7"), "Tavankut" },
                    { new Guid("ddc12930-390b-4e16-8c91-b9259d53bf16"), "Bajmok" },
                    { new Guid("a077a57d-6e63-41c8-a944-c93eb938b8f7"), "Donji Grad" },
                    { new Guid("26831746-3efb-48a5-8cd9-9810db3a75f4"), "Stari Grad" },
                    { new Guid("f0152fa7-8abc-43e9-956d-1eff5a4ace62"), "Novi Grad" },
                    { new Guid("870742be-2358-45f2-bb1b-1d4efa6bf9d2"), "Palić" }
                });

            migrationBuilder.InsertData(
                table: "Klasa",
                columns: new[] { "KlasaID", "NazivKlase" },
                values: new object[,]
                {
                    { new Guid("a95ea9b3-4eef-495e-9bcd-3ede006d7609"), "VIII" },
                    { new Guid("ae9b7c8e-3f80-43b6-bc97-14f50bee4fb7"), "VII" },
                    { new Guid("480a44e0-dc28-4d2b-8013-96326d0f9982"), "VI" },
                    { new Guid("2c063dc7-fc37-4071-a1be-60f574184909"), "V" },
                    { new Guid("5c3ad689-4409-4e4d-8e93-fd452776c770"), "I" },
                    { new Guid("7cfbb7c4-6a83-4a69-bc73-5cacca0c01fd"), "III" },
                    { new Guid("5af4b60e-1aaa-46c8-a4a0-550e602e633a"), "II" },
                    { new Guid("4a0f4309-15ed-428b-b1a6-67245d316739"), "IV" }
                });

            migrationBuilder.InsertData(
                table: "Kultura",
                columns: new[] { "KulturaID", "NazivKulture", "OpiaKulture" },
                values: new object[,]
                {
                    { new Guid("101395f3-0ab9-4952-a691-530f14fa443d"), "Pašnjaci", null },
                    { new Guid("21b610b2-50fd-478e-b649-8d67694618c6"), "Trstici-močvare", null },
                    { new Guid("fc963149-30b2-4038-b5f0-bdaff5e332c2"), "Šume", null },
                    { new Guid("a644ca6e-89c2-4b16-84d8-6f5b76909ad2"), "Livade", null },
                    { new Guid("c52c698e-ab99-4b69-bd7f-80addbd72e10"), "Voćnjaci", null },
                    { new Guid("429b3bd9-c9e4-42e5-a94d-dcf10276756d"), "Vrtovi", null },
                    { new Guid("562d2838-345b-46e0-977e-b1350199e46b"), "Njive", null },
                    { new Guid("d54ae671-92eb-4892-8579-e91ef4a53f3e"), "Vinogradi", null }
                });

            migrationBuilder.InsertData(
                table: "OblikSvojine",
                columns: new[] { "OblikSvojineID", "NazivOblikaSvojine", "OpisOblikaSvojine" },
                values: new object[,]
                {
                    { new Guid("718462dc-15e4-4dce-81cf-e150ce5846bd"), "Drugi oblici", null },
                    { new Guid("84226e05-ec99-4c6c-827f-f0ccf63a0990"), "Privatna svojina", null },
                    { new Guid("36e7d2b4-188a-4560-8325-c8b591afcce4"), "Državna svojina RS", null },
                    { new Guid("2994163c-e08a-4647-996b-86eda5e0be7a"), "Državna svojina", null },
                    { new Guid("4938990b-fee0-464f-8fa7-835a0d1e71e1"), "Društvena svojina", null },
                    { new Guid("38f76c13-8b55-45f6-bf1a-7adfa9007aba"), "Zadružna svojina", null },
                    { new Guid("cf1e7bd8-2a93-4a08-9768-57e172643630"), "Mešovita svojina", null }
                });

            migrationBuilder.InsertData(
                table: "Obradivost",
                columns: new[] { "ObradivostID", "NazivObradivosti" },
                values: new object[,]
                {
                    { new Guid("9e1a4745-8838-4130-8662-4337d153a5fd"), "Obradivo" },
                    { new Guid("8a8d331a-3553-4de1-a8aa-c4a70e5b7765"), "Ostalo" }
                });

            migrationBuilder.InsertData(
                table: "Odvodnjavanje",
                columns: new[] { "OdvodnjavanjeID", "NazivOdvodnjavanja" },
                values: new object[,]
                {
                    { new Guid("814f8e70-0edf-4cf5-8729-5091f7590b68"), "Površinsko odvodnjavanje" },
                    { new Guid("0f18b42e-3451-426d-8720-cdd50110989b"), "Podzemno odvodnjavanje" }
                });

            migrationBuilder.InsertData(
                table: "Parcela",
                columns: new[] { "ParcelaID", "BrojListaNepokretnosti", "BrojParcele", "KatastarskaOpstinaID", "KlasaID", "KlasaStvarnoStanje", "KorisnikParceleID", "KulturaID", "KulturaStvarnoStanje", "OblikSvojineID", "ObradivostID", "ObradivostStvarnoStanje", "OdvodnjavanjeID", "OdvodnjavanjeStvarnoStanje", "Povrsina", "ZasticenaZonaID", "ZasticenaZonaStvarnoStanje" },
                values: new object[] { new Guid("ae463837-b971-4354-9f40-92cc819edf25"), "LN101", "PC-2601", new Guid("870742be-2358-45f2-bb1b-1d4efa6bf9d2"), new Guid("5c3ad689-4409-4e4d-8e93-fd452776c770"), "", new Guid("efb3be0f-7082-4998-858d-51340d2abbab"), new Guid("a644ca6e-89c2-4b16-84d8-6f5b76909ad2"), "", new Guid("84226e05-ec99-4c6c-827f-f0ccf63a0990"), new Guid("9e1a4745-8838-4130-8662-4337d153a5fd"), "", new Guid("0f18b42e-3451-426d-8720-cdd50110989b"), "", 100, new Guid("308a9704-2235-4310-9092-9cde4a40164b"), "" });

            migrationBuilder.InsertData(
                table: "ZasticenaZona",
                columns: new[] { "ZasticenaZonaID", "NazivZasticeneZone" },
                values: new object[,]
                {
                    { new Guid("7801b5f4-63ef-4990-b039-efa5afc22d1d"), "2" },
                    { new Guid("308a9704-2235-4310-9092-9cde4a40164b"), "3" },
                    { new Guid("70c4f5fe-694f-4879-84ca-de5744bbd967"), "1" }
                });

            migrationBuilder.InsertData(
                table: "ZasticenaZona",
                columns: new[] { "ZasticenaZonaID", "NazivZasticeneZone" },
                values: new object[] { new Guid("25fb6ccc-6713-44e3-8234-44d680fc1b5f"), "4" });

            migrationBuilder.InsertData(
                table: "DeoParcele",
                columns: new[] { "DeoParceleID", "IdealniDeoParcele", "ParcelaID", "StvarniDeoParcele" },
                values: new object[] { new Guid("bdafb306-0e28-4dd3-ad97-14532d86552c"), 100, new Guid("ae463837-b971-4354-9f40-92cc819edf25"), 100 });

            migrationBuilder.CreateIndex(
                name: "IX_DeoParcele_ParcelaID",
                table: "DeoParcele",
                column: "ParcelaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeoParcele");

            migrationBuilder.DropTable(
                name: "KatastarskaOpstina");

            migrationBuilder.DropTable(
                name: "Klasa");

            migrationBuilder.DropTable(
                name: "Kultura");

            migrationBuilder.DropTable(
                name: "OblikSvojine");

            migrationBuilder.DropTable(
                name: "Obradivost");

            migrationBuilder.DropTable(
                name: "Odvodnjavanje");

            migrationBuilder.DropTable(
                name: "ZasticenaZona");

            migrationBuilder.DropTable(
                name: "Parcela");
        }
    }
}
