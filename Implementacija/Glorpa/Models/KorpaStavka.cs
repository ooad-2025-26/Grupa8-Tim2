using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorpa.Models
{
    public class KorpaStavka
    {
        [Key]
        public int Id { get; set; }

        public string KorisnikId { get; set; }

        [ForeignKey("Jelo")]
        public int JeloId { get; set; }

        public Jelo Jelo { get; set; }

        public int Kolicina { get; set; }
    }
}