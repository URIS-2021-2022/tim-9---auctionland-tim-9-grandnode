using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZalbaService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Radnja",
                columns: table => new
                {
                    RadnjaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivRadnje = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnja", x => x.RadnjaId);
                });

            migrationBuilder.CreateTable(
                name: "StatusZalbe",
                columns: table => new
                {
                    StatusZalbeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivStatusa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusZalbe", x => x.StatusZalbeId);
                });

            migrationBuilder.CreateTable(
                name: "TipZalbe",
                columns: table => new
                {
                    TipZalbeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTipa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipZalbe", x => x.TipZalbeId);
                });

            migrationBuilder.CreateTable(
                name: "Zalba",
                columns: table => new
                {
                    ZalbaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipZalbe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumZalbe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PodnosilacZalbe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razlog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obrazlozenje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumResenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojResenja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusZalbe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojOdluke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Radnja = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalba", x => x.ZalbaId);
                });

            migrationBuilder.InsertData(
                table: "Radnja",
                columns: new[] { "RadnjaId", "NazivRadnje" },
                values: new object[,]
                {
                    { new Guid("b0f02aee-2bf3-43e2-bdd4-eb9e80f4bcc9"), "JN ide u drugi krug sa novim uslovima" },
                    { new Guid("62462998-fe3f-49f2-861d-9f226264beba"), "JN ide u drugi krug sa starim uslovima" },
                    { new Guid("3eeede02-9e9e-46d2-8034-d21125e45b43"), "JN ne ide u drugi krug" }
                });

            migrationBuilder.InsertData(
                table: "StatusZalbe",
                columns: new[] { "StatusZalbeId", "NazivStatusa" },
                values: new object[,]
                {
                    { new Guid("212b6e83-ab50-49ec-bd95-92cd5e8f8a25"), "Usvojena" },
                    { new Guid("0d377d88-9e91-43c0-adfd-ecc8bd406809"), "Odbijena" },
                    { new Guid("b1c8cea8-c996-4344-a211-6a7e7e257e46"), "Otvorena" }
                });

            migrationBuilder.InsertData(
                table: "TipZalbe",
                columns: new[] { "TipZalbeId", "NazivTipa" },
                values: new object[,]
                {
                    { new Guid("cd155ba7-f573-4f24-b412-e41994ef8073"), "Žalba na tok javnog nadmetanja" },
                    { new Guid("1fbd2475-b35e-4e47-af39-f784c8d49497"), "Žalba na Odluku o davanju u zakup" },
                    { new Guid("c925bbc9-bbc1-4917-9aea-d5b253abc8a5"), "Žalba na Odluku o davanju na korišćenje" }
                });

            migrationBuilder.InsertData(
                table: "Zalba",
                columns: new[] { "ZalbaId", "BrojOdluke", "BrojResenja", "DatumResenja", "DatumZalbe", "Obrazlozenje", "PodnosilacZalbe", "Radnja", "Razlog", "StatusZalbe", "TipZalbe" },
                values: new object[] { new Guid("007ed3b2-abb5-4bb8-90d5-f193907079ad"), "1221", "1035", new DateTime(2021, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), "Neispravnost prilikom dodeljivanja parcele", "Marko Markovic", new Guid("3eeede02-9e9e-46d2-8034-d21125e45b43"), "Krsenje pravilnika za javno nadmetanje", new Guid("212b6e83-ab50-49ec-bd95-92cd5e8f8a25"), new Guid("cd155ba7-f573-4f24-b412-e41994ef8073") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Radnja");

            migrationBuilder.DropTable(
                name: "StatusZalbe");

            migrationBuilder.DropTable(
                name: "TipZalbe");

            migrationBuilder.DropTable(
                name: "Zalba");
        }
    }
}
