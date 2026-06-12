using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Notifikacija
    {
        [Key]
        public int Id { get; set; }

        public string Poruka { get; set; }

        public bool Procitana { get; set; }

        public DateTime Datum { get; set; }

        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik? Korisnik { get; set; }
    }
}