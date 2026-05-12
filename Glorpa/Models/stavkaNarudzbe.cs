namespace Glorpa.Models
{
    public class StavkaNarudzbe
    {
        public int Id { get; set; }

        public int Kolicina { get; set; }

        public double Cijena { get; set; }

        // FK prema narudzbi
        public int NarudzbaId { get; set; }

        public Narudzba Narudzba { get; set; }

        // FK prema jelu
        public int JeloId { get; set; }

        public Jelo Jelo { get; set; }
    }
}