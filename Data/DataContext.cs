using Microsoft.EntityFrameworkCore;
using Glorpa.Models;

namespace Glorpa.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Kupac> Kupci { get; set; }
        public DbSet<Dostavljac> Dostavljaci { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Restoran> Restorani { get; set; }
        public DbSet<Jelo> Jela { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }
        public DbSet<Dostava> Dostave { get; set; }
        public DbSet<Zarada> Zarade { get; set; }
        public DbSet<Dug> Dugovi { get; set; }
        public DbSet<Raspored> Rasporedi { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<ZahtjevPodrske> ZahtjeviPodrske { get; set; }
        public DbSet<Notifikacija> Notifikacije { get; set; }
        public DbSet<VremenskiUslovi> VremenskiUslovi { get; set; }
        public DbSet<Lokacija> Lokacije { get; set; }
    }
}