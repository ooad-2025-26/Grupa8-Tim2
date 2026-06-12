using System.ComponentModel.DataAnnotations;

namespace Glorpa.Models
{
    public class VremenskiUslovi
    {
        [Key]
        public int Id { get; set; }

        public string Tip { get; set; }

        public string Opis { get; set; }

        public bool JeLiUslov { get; set; }

        public ICollection<Dostava>? Dostave { get; set; }

        public VremenskiUslovi()
        {
        }
    }
}