using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Dostavljac : Korisnik
    {
        public TipDostave SredstvoDostave { get; set; }
        public StatusDostavljaca Status { get; set; }
        public double Dug { get; set; }
        public double UkupnaZarada { get; set; }

        public List<Dostava>? Dostave { get; set; }
        public List<Raspored>? Rasporedi { get; set; }
        public List<ZahtjevPodrske>? Zahtjevi { get; set; }
    }
}