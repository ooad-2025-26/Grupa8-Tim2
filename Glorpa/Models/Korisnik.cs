using Glorpa.Enums;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace Glorpa.Models
{
    public class Korisnik : IdentityUser
    {
        public string Id { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

       
        public TipKorisnika Uloga { get; set; }
        public Dug Dug { get; set; }

        // 1 korisnik -> vise narudzbi
        public ICollection<Narudzba> Narudzbe { get; set; }

        // 1 korisnik -> vise zahtjeva podrske
        public ICollection<ZahtjevPodrske> ZahtjeviPodrske { get; set; }

        // 1 korisnik -> vise rasporeda
        public ICollection<Raspored> Rasporedi { get; set; }
    }    }