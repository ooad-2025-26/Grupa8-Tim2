using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Zarada
    {
        public int Id { get; set; }

        public double Iznos { get; set; }

        public DateTime Datum { get; set; }
        public TipZarade TipZarade { get; set; }
        public double Sat { get; set; }

        // FK prema dostavi
        public int DostavaId { get; set; }

        public Dostava Dostava { get; set; }
    }

}
