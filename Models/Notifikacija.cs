using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Notifikacija
    {
        public int Id { get; set; }
        public string Poruka { get; set; }
        public DateTime Datum { get; set; }
        public TipNotifikacije Tip { get; set; }
        public bool Procitana { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik? Korisnik { get; set; }
    }
}