using Glorpa.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Glorpa.Models
{
    public class Korisnik : IdentityUser
    {
        // NE STAVLJAJ svoj Id
        // IdentityUser već ima:
        // public string Id { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public TipKorisnika Uloga { get; set; }

        // 1 -> 1
        public Dug? Dug { get; set; }

        public ICollection<Narudzba>? Narudzbe { get; set; }

        public ICollection<ZahtjevPodrske>? ZahtjeviPodrske { get; set; }

        public ICollection<Raspored>? Rasporedi { get; set; }
    }
}