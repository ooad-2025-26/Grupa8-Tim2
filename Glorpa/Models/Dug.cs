namespace Glorpa.Models
{
    public class Dug
    {
        public int Id { get; set; }

        public double Iznos { get; set; }

        public DateTime Datum { get; set; }

        public bool Placeno { get; set; }

        // FK
        public int KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
    }
}