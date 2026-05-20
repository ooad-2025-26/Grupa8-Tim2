using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Glorpa.Enums;

namespace Glorpa.Models
{
    public class Dostava
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime VrijemePreuzimanja { get; set; }

        public DateTime VrijemeDostave { get; set; }

        [ForeignKey("Narudzba")]
        public int NarudzbaId { get; set; }

        public Narudzba Narudzba { get; set; }

        public ICollection<Lokacija> Lokacije { get; set; }

        public Zarada Zarada { get; set; }


        [ForeignKey("VremenskiUslovi")]
        public int VremenskiUsloviId { get; set; }

        public VremenskiUslovi VremenskiUslovi { get; set; }
    }
}