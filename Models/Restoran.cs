namespace Glorpa.Models
{
    public class Restoran
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

        public List<Jelo>? Jela { get; set; }
    }
}