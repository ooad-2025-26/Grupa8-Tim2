using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glorpa.Migrations
{
    /// <inheritdoc />
    public partial class DodanDostavljacUDostavu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DostavljacId",
                table: "Dostava",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dostava_DostavljacId",
                table: "Dostava",
                column: "DostavljacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dostava_Korisnik_DostavljacId",
                table: "Dostava",
                column: "DostavljacId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dostava_Korisnik_DostavljacId",
                table: "Dostava");

            migrationBuilder.DropIndex(
                name: "IX_Dostava_DostavljacId",
                table: "Dostava");

            migrationBuilder.DropColumn(
                name: "DostavljacId",
                table: "Dostava");
        }
    }
}
