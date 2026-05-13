namespace Glorpa.Models
{
    public class Raspored
    {
        public int Id { get; set; }

        public DateTime Sedmica { get; set; }

        // FK prema korisniku
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }

       
        public ICollection<Termin> Termini { get; set; }
    }
}