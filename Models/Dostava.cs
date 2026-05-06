using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Dostava
    {
        public int Id { get; set; }
        public StatusDostave Status { get; set; }
        public DateTime VrijemePreuzimanja { get; set; }
        public DateTime VrijemeIsporuke { get; set; }

        public int NarudzbaId { get; set; }
        public Narudzba? Narudzba { get; set; }

        public int DostavljacId { get; set; }
        public Dostavljac? Dostavljac { get; set; }
    }
}