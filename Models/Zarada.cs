namespace Glorpa.Models
{
    public class Zarada
    {
        public int Id { get; set; }
        public double Iznos { get; set; }
        public DateTime Datum { get; set; }
        public double Sati { get; set; }

        public int DostavljacId { get; set; }
        public Dostavljac? Dostavljac { get; set; }
    }
}
