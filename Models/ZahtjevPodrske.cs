using Glorpa.Enums;

namespace Glorpa.Models
{
    public class ZahtjevPodrske
    {
        public int Id { get; set; }
        public TipZahtjeva Tip { get; set; }
        public Prioritet Prioritet { get; set; }
        public string Opis { get; set; }
        public StatusZahtjeva Status { get; set; }
        public DateTime Datum { get; set; }

        public int DostavljacId { get; set; }
        public Dostavljac? Dostavljac { get; set; }
    }
}