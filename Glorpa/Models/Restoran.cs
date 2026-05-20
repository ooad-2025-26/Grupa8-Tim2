using System.ComponentModel.DataAnnotations;

namespace Glorpa.Models
{
    public class Restoran
    {
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public ICollection<Jelo> Jela { get; set; }
    }
}