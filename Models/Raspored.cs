namespace Glorpa.Models
{
    public class Raspored
    {
        public int Id { get; set; }
        public int Sedmica { get; set; }

        public int DostavljacId { get; set; }
        public Dostavljac? Dostavljac { get; set; }

        public List<Termin>? Termini { get; set; }
    }
}