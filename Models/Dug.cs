namespace Glorpa.Models
{
    public class Dug
    {
        public int Id { get; set; }
        public double Iznos { get; set; }
        public DateTime Datum { get; set; }
        public bool Placeno { get; set; }

        public int DostavljacId { get; set; }
        public Dostavljac? Dostavljac { get; set; }
    }
}