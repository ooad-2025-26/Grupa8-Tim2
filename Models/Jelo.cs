namespace Glorpa.Models
{
    public class Jelo
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public double Tezina { get; set; }

        public int RestoranId { get; set; }
        public Restoran? Restoran { get; set; }
    }
}
