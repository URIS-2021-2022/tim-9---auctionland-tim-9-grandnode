using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KomisijaService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clanovi",
                columns: table => new
                {
                    ClanoviId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clanovi", x => x.ClanoviId);
                });

            migrationBuilder.CreateTable(
                name: "Komisija",
                columns: table => new
                {
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PredsednikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisija", x => x.KomisijaId);
                });

            migrationBuilder.CreateTable(
                name: "Predsednik",
                columns: table => new
                {
                    PredsednikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predsednik", x => x.PredsednikId);
                });

            migrationBuilder.InsertData(
                table: "Clanovi",
                columns: new[] { "ClanoviId", "KomisijaId" },
                values: new object[,]
                {
                    { new Guid("ea3d75d9-61aa-4cc5-9e2a-6f01190b9044"), new Guid("c1b8a40c-0e1c-44a6-87d2-1593ab638e94") },
                    { new Guid("c84a7948-81c5-44d2-86c1-c601fdab526b"), new Guid("0648b913-c49e-4939-95ae-10185e475ef7") },
                    { new Guid("06cfa3e0-6d39-46c6-b5bb-98e0a64a9637"), new Guid("bf1c58fd-ba25-4bd9-837a-37c06ad29ea5") }
                });

            migrationBuilder.InsertData(
                table: "Komisija",
                columns: new[] { "KomisijaId", "PredsednikId" },
                values: new object[,]
                {
                    { new Guid("c1b8a40c-0e1c-44a6-87d2-1593ab638e94"), new Guid("61ef85bf-765a-4a53-a50a-9d99255cdeaf") },
                    { new Guid("0648b913-c49e-4939-95ae-10185e475ef7"), new Guid("efcbf7d7-de6b-4468-a8f7-d3907d541262") },
                    { new Guid("bf1c58fd-ba25-4bd9-837a-37c06ad29ea5"), new Guid("ebfc69f7-8626-48c4-8c92-c06ca85cf7b1") }
                });

            migrationBuilder.InsertData(
                table: "Predsednik",
                column: "PredsednikId",
                values: new object[]
                {
                    new Guid("61ef85bf-765a-4a53-a50a-9d99255cdeaf"),
                    new Guid("efcbf7d7-de6b-4468-a8f7-d3907d541262"),
                    new Guid("ebfc69f7-8626-48c4-8c92-c06ca85cf7b1")
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clanovi");

            migrationBuilder.DropTable(
                name: "Komisija");

            migrationBuilder.DropTable(
                name: "Predsednik");
        }
    }
}
