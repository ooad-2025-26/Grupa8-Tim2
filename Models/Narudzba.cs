using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Narudzba
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public StatusNarudzbe Status { get; set; }
        public double UkupnaCijena { get; set; }
        public double Tezina { get; set; }

        public int KupacId { get; set; }
        public Kupac? Kupac { get; set; }

        public List<StavkaNarudzbe>? Stavke { get; set; }
    }
}
