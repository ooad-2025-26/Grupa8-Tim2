using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glorpa.Migrations
{
    /// <inheritdoc />
    public partial class DodanaAdresaKorisniku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Korisnik");
        }
    }
}
