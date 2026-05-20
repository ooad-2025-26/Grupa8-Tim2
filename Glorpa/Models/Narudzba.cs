using Glorpa.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public string Status { get; set; }

        public double UkupnaCijena { get; set; }

        public TipPlacanja NacinPlacanja { get; set; }

        // FK prema Korisnik
        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }

        public ICollection<StavkaNarudzbe> StavkeNarudzbe { get; set; }

        public Dostava Dostava { get; set; }
    }
}