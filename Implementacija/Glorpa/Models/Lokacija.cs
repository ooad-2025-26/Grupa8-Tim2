using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Lokacija
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Vrijeme { get; set; }

        // FK prema Dostava
        [ForeignKey("Dostava")]
        public int DostavaId { get; set; }

        public Dostava Dostava { get; set; }
    }
}