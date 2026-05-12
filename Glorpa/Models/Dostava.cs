using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Dostava
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime VrijemePreuzimanja { get; set; }

        public DateTime VrijemeDostave { get; set; }

        // FK prema narudzbi
        public int NarudzbaId { get; set; }

        public Narudzba Narudzba { get; set; }

        public ICollection<Lokacija> Lokacije { get; set; }

        
        public Zarada Zarada { get; set; }

        
        public int VremenskiUsloviId { get; set; }

        public VremenskiUslovi VremenskiUslovi { get; set; }
    }
}