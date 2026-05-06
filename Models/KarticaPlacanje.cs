using Glorpa.Interfaces;

namespace Glorpa.Models
{
    public class KarticaPlacanje : IPlacanje
    {
        public int Id { get; set; }
        public string BrojKartice { get; set; }
        public string RokVazenja { get; set; }
        public string CVV { get; set; }

        public void IzvrsiPlacanje(double iznos)
        {
        }
    }
}