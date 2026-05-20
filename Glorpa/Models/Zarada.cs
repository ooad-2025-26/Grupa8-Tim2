using Glorpa.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Zarada
    {
        [Key]
        public int Id { get; set; }

        public double Iznos { get; set; }

        public DateTime Datum { get; set; }

        public TipZarade TipZarade { get; set; }

        public double Sat { get; set; }

        // FK prema Dostava
        [ForeignKey("Dostava")]
        public int DostavaId { get; set; }

        public Dostava Dostava { get; set; }
    }
}