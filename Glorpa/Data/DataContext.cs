using Glorpa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Glorpa.Data
{
    public class DataContext : IdentityDbContext<Korisnik>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici { get; set; }

        public DbSet<Restoran> Restorani { get; set; }

        public DbSet<Jelo> Jela { get; set; }

        public DbSet<Narudzba> Narudzbe { get; set; }

        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }

        public DbSet<Dostava> Dostave { get; set; }

        public DbSet<Lokacija> Lokacije { get; set; }

        public DbSet<Zarada> Zarade { get; set; }

        public DbSet<VremenskiUslovi> VremenskiUslovi { get; set; }

        public DbSet<Dug> Dugovi { get; set; }

        public DbSet<Raspored> Rasporedi { get; set; }

        public DbSet<Termin> Termini { get; set; }

        public DbSet<ZahtjevPodrske> ZahtjeviPodrske { get; set; }

    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");

            modelBuilder.Entity<Restoran>().ToTable("Restoran");

            modelBuilder.Entity<Jelo>().ToTable("Jelo");

            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");

            modelBuilder.Entity<StavkaNarudzbe>().ToTable("StavkaNarudzbe");

            modelBuilder.Entity<Dostava>().ToTable("Dostava");

            modelBuilder.Entity<Lokacija>().ToTable("Lokacija");

            modelBuilder.Entity<Zarada>().ToTable("Zarada");

            modelBuilder.Entity<VremenskiUslovi>().ToTable("VremenskiUslovi");

            modelBuilder.Entity<Dug>().ToTable("Dug");

            modelBuilder.Entity<Raspored>().ToTable("Raspored");

            modelBuilder.Entity<Termin>().ToTable("Termin");

            modelBuilder.Entity<ZahtjevPodrske>().ToTable("ZahtjevPodrske");

          

            // Restoran 1 -> vise Jela
            modelBuilder.Entity<Jelo>()
                .HasOne(j => j.Restoran)
                .WithMany(r => r.Jela)
                .HasForeignKey(j => j.RestoranId);

            // Korisnik 1 -> vise Narudzbi
            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Korisnik)
                .WithMany(k => k.Narudzbe)
                .HasForeignKey(n => n.KorisnikId);

            // Narudzba 1 -> vise Stavki
            modelBuilder.Entity<StavkaNarudzbe>()
                .HasOne(s => s.Narudzba)
                .WithMany(n => n.StavkeNarudzbe)
                .HasForeignKey(s => s.NarudzbaId);

            // Jelo 1 -> vise StavkiNarudzbe
            modelBuilder.Entity<StavkaNarudzbe>()
                .HasOne(s => s.Jelo)
                .WithMany(j => j.StavkeNarudzbe)
                .HasForeignKey(s => s.JeloId);

            // Narudzba 1 -> 1 Dostava
            modelBuilder.Entity<Dostava>()
                .HasOne(d => d.Narudzba)
                .WithOne(n => n.Dostava)
                .HasForeignKey<Dostava>(d => d.NarudzbaId);

            // Dostava 1 -> vise Lokacija
            modelBuilder.Entity<Lokacija>()
                .HasOne(l => l.Dostava)
                .WithMany(d => d.Lokacije)
                .HasForeignKey(l => l.DostavaId);

            // Dostava 1 -> 1 Zarada
            modelBuilder.Entity<Zarada>()
                .HasOne(z => z.Dostava)
                .WithOne(d => d.Zarada)
                .HasForeignKey<Zarada>(z => z.DostavaId);

            // VremenskiUslovi 1 -> vise Dostava
            modelBuilder.Entity<Dostava>()
                .HasOne(d => d.VremenskiUslovi)
                .WithMany(v => v.Dostave)
                .HasForeignKey(d => d.VremenskiUsloviId);

            // Korisnik 1 -> vise Rasporeda
            modelBuilder.Entity<Raspored>()
                .HasOne(r => r.Korisnik)
                .WithMany(k => k.Rasporedi)
                .HasForeignKey(r => r.KorisnikId);

            // Raspored 1 -> vise Termina
            modelBuilder.Entity<Termin>()
                .HasOne(t => t.Raspored)
                .WithMany(r => r.Termini)
                .HasForeignKey(t => t.RasporedId);

            // Korisnik 1 -> vise ZahtjevaPodrske
            modelBuilder.Entity<ZahtjevPodrske>()
                .HasOne(z => z.Korisnik)
                .WithMany(k => k.ZahtjeviPodrske)
                .HasForeignKey(z => z.KorisnikId);

            // Korisnik 1 -> 1 Dug
            modelBuilder.Entity<Korisnik>()
                .HasOne(k => k.Dug)
                .WithOne(d => d.Korisnik)
                .HasForeignKey<Dug>(d => d.KorisnikId);
        }
    }
}