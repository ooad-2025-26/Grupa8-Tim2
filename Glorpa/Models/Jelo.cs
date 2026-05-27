namespace Glorpa.Models
{
    public class Jelo
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public double Cijena { get; set; }

        public string TipJela { get; set; }

        // FK
        public int RestoranId { get; set; }

        public Restoran Restoran { get; set; }

        public ICollection<StavkaNarudzbe> StavkeNarudzbe { get; set; }
    }

}
