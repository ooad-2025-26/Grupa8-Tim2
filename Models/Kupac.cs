namespace Glorpa.Models
{
    public class Kupac : Korisnik
    {
        public string Adresa { get; set; }

        public List<Narudzba>? Narudzbe { get; set; }
    }
}
