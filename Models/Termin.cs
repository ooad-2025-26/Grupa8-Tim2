namespace Glorpa.Models
{
    public class Termin
    {
        public int Id { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }

        public int RasporedId { get; set; }
        public Raspored? Raspored { get; set; }
    }
}