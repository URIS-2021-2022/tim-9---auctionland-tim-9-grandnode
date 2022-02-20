using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KomisijaService.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NazivKomisije",
                table: "Komisija",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Komisija",
                keyColumn: "KomisijaId",
                keyValue: new Guid("0648b913-c49e-4939-95ae-10185e475ef7"),
                column: "NazivKomisije",
                value: "Druga komisija");

            migrationBuilder.UpdateData(
                table: "Komisija",
                keyColumn: "KomisijaId",
                keyValue: new Guid("bf1c58fd-ba25-4bd9-837a-37c06ad29ea5"),
                column: "NazivKomisije",
                value: "PTreca komisija");

            migrationBuilder.UpdateData(
                table: "Komisija",
                keyColumn: "KomisijaId",
                keyValue: new Guid("c1b8a40c-0e1c-44a6-87d2-1593ab638e94"),
                column: "NazivKomisije",
                value: "Prva komisija");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivKomisije",
                table: "Komisija");
        }
    }
}
