using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glorpa.Migrations
{
    /// <inheritdoc />
    public partial class FinalModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "Narudzbe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "KupacId",
                table: "Narudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Narudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Tezina",
                table: "Narudzbe",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    NivoPristupa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SredstvoDostave = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Dug = table.Column<double>(type: "float", nullable: true),
                    UkupnaZarada = table.Column<double>(type: "float", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restorani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restorani", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VremenskiUslovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipVremena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VremenskiUslovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dostave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VrijemePreuzimanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemeIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NarudzbaId = table.Column<int>(type: "int", nullable: false),
                    DostavljacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dostave_Korisnik_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dostave_Narudzbe_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzbe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dugovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Placeno = table.Column<bool>(type: "bit", nullable: false),
                    DostavljacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dugovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dugovi_Korisnik_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poruka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    Procitana = table.Column<bool>(type: "bit", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rasporedi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sedmica = table.Column<int>(type: "int", nullable: false),
                    DostavljacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rasporedi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rasporedi_Korisnik_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjeviPodrske",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    Prioritet = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DostavljacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjeviPodrske", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtjeviPodrske_Korisnik_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zarade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sati = table.Column<double>(type: "float", nullable: false),
                    DostavljacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zarade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zarade_Korisnik_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jela",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Tezina = table.Column<double>(type: "float", nullable: false),
                    RestoranId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jela_Restorani_RestoranId",
                        column: x => x.RestoranId,
                        principalTable: "Restorani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DostavaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lokacije_Dostave_DostavaId",
                        column: x => x.DostavaId,
                        principalTable: "Dostave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pocetak = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kraj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RasporedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termini_Rasporedi_RasporedId",
                        column: x => x.RasporedId,
                        principalTable: "Rasporedi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeNarudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    NarudzbaId = table.Column<int>(type: "int", nullable: false),
                    JeloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeNarudzbe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Jela_JeloId",
                        column: x => x.JeloId,
                        principalTable: "Jela",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Narudzbe_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzbe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_KupacId",
                table: "Narudzbe",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostave_DostavljacId",
                table: "Dostave",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostave_NarudzbaId",
                table: "Dostave",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dugovi_DostavljacId",
                table: "Dugovi",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_Jela_RestoranId",
                table: "Jela",
                column: "RestoranId");

            migrationBuilder.CreateIndex(
                name: "IX_Lokacije_DostavaId",
                table: "Lokacije",
                column: "DostavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_KorisnikId",
                table: "Notifikacije",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Rasporedi_DostavljacId",
                table: "Rasporedi",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_JeloId",
                table: "StavkeNarudzbe",
                column: "JeloId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_NarudzbaId",
                table: "StavkeNarudzbe",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_RasporedId",
                table: "Termini",
                column: "RasporedId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjeviPodrske_DostavljacId",
                table: "ZahtjeviPodrske",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_Zarade_DostavljacId",
                table: "Zarade",
                column: "DostavljacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzbe_Korisnik_KupacId",
                table: "Narudzbe",
                column: "KupacId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzbe_Korisnik_KupacId",
                table: "Narudzbe");

            migrationBuilder.DropTable(
                name: "Dugovi");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropTable(
                name: "StavkeNarudzbe");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "VremenskiUslovi");

            migrationBuilder.DropTable(
                name: "ZahtjeviPodrske");

            migrationBuilder.DropTable(
                name: "Zarade");

            migrationBuilder.DropTable(
                name: "Dostave");

            migrationBuilder.DropTable(
                name: "Jela");

            migrationBuilder.DropTable(
                name: "Rasporedi");

            migrationBuilder.DropTable(
                name: "Restorani");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropIndex(
                name: "IX_Narudzbe_KupacId",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "Datum",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "KupacId",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "Tezina",
                table: "Narudzbe");
        }
    }
}
