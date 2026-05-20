using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class Jelo
    {
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public double Cijena { get; set; }

        public string TipJela { get; set; }

        
        [ForeignKey("Restoran")]
        public int RestoranId { get; set; }

        public Restoran Restoran { get; set; }

        public ICollection<StavkaNarudzbe> StavkeNarudzbe { get; set; }
    }
}
