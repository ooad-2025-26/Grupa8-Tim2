namespace Glorpa.Models
{
    public class Lokacija
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Vrijeme { get; set; }

        public int DostavaId { get; set; }
        public Dostava? Dostava { get; set; }
    }
}