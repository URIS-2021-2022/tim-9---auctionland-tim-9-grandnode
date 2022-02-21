using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UgovorService.Migrations
{
    public partial class Key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgovorEnt_TipGarancijeEnt_TipGarancijeEntTipID",
                table: "UgovorEnt");

            migrationBuilder.DropIndex(
                name: "IX_UgovorEnt_TipGarancijeEntTipID",
                table: "UgovorEnt");

            migrationBuilder.DropColumn(
                name: "TipGarancijeEntTipID",
                table: "UgovorEnt");

            migrationBuilder.CreateIndex(
                name: "IX_UgovorEnt_TipID",
                table: "UgovorEnt",
                column: "TipID");

            migrationBuilder.AddForeignKey(
                name: "FK_UgovorEnt_TipGarancijeEnt_TipID",
                table: "UgovorEnt",
                column: "TipID",
                principalTable: "TipGarancijeEnt",
                principalColumn: "TipID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgovorEnt_TipGarancijeEnt_TipID",
                table: "UgovorEnt");

            migrationBuilder.DropIndex(
                name: "IX_UgovorEnt_TipID",
                table: "UgovorEnt");

            migrationBuilder.AddColumn<Guid>(
                name: "TipGarancijeEntTipID",
                table: "UgovorEnt",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UgovorEnt_TipGarancijeEntTipID",
                table: "UgovorEnt",
                column: "TipGarancijeEntTipID");

            migrationBuilder.AddForeignKey(
                name: "FK_UgovorEnt_TipGarancijeEnt_TipGarancijeEntTipID",
                table: "UgovorEnt",
                column: "TipGarancijeEntTipID",
                principalTable: "TipGarancijeEnt",
                principalColumn: "TipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
