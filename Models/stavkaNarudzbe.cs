namespace Glorpa.Models
{
    public class StavkaNarudzbe
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }

        public int NarudzbaId { get; set; }
        public Narudzba? Narudzba { get; set; }

        public int JeloId { get; set; }
        public Jelo? Jelo { get; set; }
    }
}