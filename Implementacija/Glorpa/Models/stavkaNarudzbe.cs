using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class StavkaNarudzbe
    {
        [Key]
        public int Id { get; set; }

        public int Kolicina { get; set; }

        public double Cijena { get; set; }

        // FK prema Narudzba
        [ForeignKey("Narudzba")]
        public int NarudzbaId { get; set; }

        public Narudzba Narudzba { get; set; }

        // FK prema Jelo
        [ForeignKey("Jelo")]
        public int JeloId { get; set; }

        public Jelo Jelo { get; set; }
    }
}