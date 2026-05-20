using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Termin
    {
        [Key]
        public int Id { get; set; }

        public DateTime Pocetak { get; set; }

        public DateTime Kraj { get; set; }

        public double Trajanje { get; set; }

        // FK prema Raspored
        [ForeignKey("Raspored")]
        public int RasporedId { get; set; }

        public Raspored Raspored { get; set; }
    }
}