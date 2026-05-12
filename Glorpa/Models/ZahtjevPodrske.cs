using Glorpa.Enums;

namespace Glorpa.Models
{
    public class ZahtjevPodrske
    {
        public int Id { get; set; }

        public string TipZahtjeva { get; set; }

        public string Prioritet { get; set; }

        public string Opis { get; set; }

        public string Status { get; set; }

        public DateTime Datum { get; set; }

        // FK prema korisniku
        public int KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
    }
}