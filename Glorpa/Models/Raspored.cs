using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Raspored
    {
        [Key]
        public int Id { get; set; }

        public DateTime Sedmica { get; set; }

        // FK prema Korisnik
        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }

        public ICollection<Termin> Termini { get; set; }
    }
}