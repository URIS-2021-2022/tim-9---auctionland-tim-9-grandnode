using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KomisijaService.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PredsednikId",
                table: "Komisija",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "KomisijaId",
                table: "Clanovi",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Komisija_PredsednikId",
                table: "Komisija",
                column: "PredsednikId");

            migrationBuilder.CreateIndex(
                name: "IX_Clanovi_KomisijaId",
                table: "Clanovi",
                column: "KomisijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clanovi_Komisija_KomisijaId",
                table: "Clanovi",
                column: "KomisijaId",
                principalTable: "Komisija",
                principalColumn: "KomisijaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Komisija_Predsednik_PredsednikId",
                table: "Komisija",
                column: "PredsednikId",
                principalTable: "Predsednik",
                principalColumn: "PredsednikId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clanovi_Komisija_KomisijaId",
                table: "Clanovi");

            migrationBuilder.DropForeignKey(
                name: "FK_Komisija_Predsednik_PredsednikId",
                table: "Komisija");

            migrationBuilder.DropIndex(
                name: "IX_Komisija_PredsednikId",
                table: "Komisija");

            migrationBuilder.DropIndex(
                name: "IX_Clanovi_KomisijaId",
                table: "Clanovi");

            migrationBuilder.AlterColumn<Guid>(
                name: "PredsednikId",
                table: "Komisija",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "KomisijaId",
                table: "Clanovi",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
