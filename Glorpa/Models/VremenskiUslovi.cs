namespace Glorpa.Models
{
    public class VremenskiUslovi
    {
        public int Id { get; set; }

        public string Tip { get; set; }

        public string Opis { get; set; }

        public bool JeLiUslov { get; set; }

        // vise dostava moze imati iste vremenske uslove
        public ICollection<Dostava> Dostave { get; set; }
    }
}