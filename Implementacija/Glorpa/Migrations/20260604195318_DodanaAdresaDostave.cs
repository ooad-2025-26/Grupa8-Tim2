using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glorpa.Migrations
{
    /// <inheritdoc />
    public partial class DodanaAdresaDostave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdresaDostave",
                table: "Narudzba",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresaDostave",
                table: "Narudzba");
        }
    }
}
