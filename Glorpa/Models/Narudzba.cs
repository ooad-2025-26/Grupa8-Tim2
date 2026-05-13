using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Narudzba
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public string Status { get; set; }

        public double UkupnaCijena { get; set; }

        public TipPlacanja NacinPlacanja { get; set; }


        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }

        public ICollection<StavkaNarudzbe> StavkeNarudzbe { get; set; }

    
        public Dostava Dostava { get; set; }
    }
}
