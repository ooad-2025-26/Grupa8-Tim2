using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glorpa.Migrations
{
    /// <inheritdoc />
    public partial class Dostavapromjena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kilometara",
                table: "Zarada");

            migrationBuilder.AddColumn<double>(
                name: "Kilometara",
                table: "Dostava",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kilometara",
                table: "Dostava");

            migrationBuilder.AddColumn<double>(
                name: "Kilometara",
                table: "Zarada",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
