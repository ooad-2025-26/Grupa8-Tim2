using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class ZahtjevPodrske
    {
        [Key]
        public int Id { get; set; }

        public string TipZahtjeva { get; set; }

        public string Prioritet { get; set; }

        public string Opis { get; set; }

        public string Status { get; set; }

        public DateTime Datum { get; set; }

        // FK prema Korisnik
        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
    }
}