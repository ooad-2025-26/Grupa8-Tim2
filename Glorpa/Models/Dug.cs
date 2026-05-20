using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Dug
    {
        [Key]
        public int Id { get; set; }

        public double Iznos { get; set; }

        public DateTime Datum { get; set; }

        public bool Placeno { get; set; }

        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
    }
}