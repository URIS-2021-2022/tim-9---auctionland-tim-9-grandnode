using Microsoft.EntityFrameworkCore.Migrations;


namespace Dokument_AK.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DokumentEnt_StatusDokID",
                table: "DokumentEnt",
                column: "StatusDokID");

            migrationBuilder.AddForeignKey(
                name: "FK_DokumentEnt_StatusDokumentaEnt_StatusDokID",
                table: "DokumentEnt",
                column: "StatusDokID",
                principalTable: "StatusDokumentaEnt",
                principalColumn: "StatusDokID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DokumentEnt_StatusDokumentaEnt_StatusDokID",
                table: "DokumentEnt");

            migrationBuilder.DropIndex(
                name: "IX_DokumentEnt_StatusDokID",
                table: "DokumentEnt");
        }
    }
}
