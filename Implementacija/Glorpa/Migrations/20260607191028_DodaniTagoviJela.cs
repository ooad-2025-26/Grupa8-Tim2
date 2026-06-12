using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glorpa.Migrations
{
    /// <inheritdoc />
    public partial class DodaniTagoviJela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tagovi",
                table: "Jelo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tagovi",
                table: "Jelo");
        }
    }
}
