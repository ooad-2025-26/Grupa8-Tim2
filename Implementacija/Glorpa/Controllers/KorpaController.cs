using Glorpa.Data;
using Glorpa.Enums;
using Glorpa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Glorpa.Controllers
{
    public class KorpaController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public KorpaController(
            DataContext context,
            UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            var stavke =
                await _context.KorpaStavke
                    .Include(x => x.Jelo)
                    .Where(x => x.KorisnikId == korisnik.Id)
                    .ToListAsync();

            return View(stavke);
        }



        [HttpPost]
        public async Task<IActionResult> PromijeniKolicinu(
    int id,
    string akcija)
        {
            var stavka =
                await _context.KorpaStavke
                    .FindAsync(id);

            if (stavka == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (akcija == "plus"
                && stavka.Kolicina < 100)
            {
                stavka.Kolicina++;
            }

            if (akcija == "minus"
                && stavka.Kolicina > 1)
            {
                stavka.Kolicina--;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Ukloni(int id)
        {
            var stavka =
                await _context.KorpaStavke.FindAsync(id);

            if (stavka != null)
            {
                _context.KorpaStavke.Remove(stavka);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(TipPlacanja nacinPlacanja)
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(korisnik.Adresa))
            {
                TempData["Greska"] =
                    "Niste unijeli adresu dostave.";

                return RedirectToAction(nameof(Index));
            }
            var korpa =
                await _context.KorpaStavke
                    .Include(x => x.Jelo)
                    .Where(x => x.KorisnikId == korisnik.Id)
                    .ToListAsync();

            if (!korpa.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var narudzba = new Narudzba
            {
                Datum = DateTime.Now,
                Status = "Kreirana",
                KorisnikId = korisnik.Id,
                NacinPlacanja = nacinPlacanja,

                UkupnaCijena =
            korpa.Sum(x =>
                x.Jelo.Cijena * x.Kolicina) + 5,

                UkupnaTezinaKg =
            korpa.Sum(x =>
                x.Jelo.TezinaKg * x.Kolicina),

                AdresaDostave = korisnik.Adresa
            };

            _context.Narudzbe.Add(narudzba);

            await _context.SaveChangesAsync();

            foreach (var stavka in korpa)
            {
                _context.StavkeNarudzbe.Add(
                    new StavkaNarudzbe
                    {
                        NarudzbaId = narudzba.Id,
                        JeloId = stavka.JeloId,
                        Kolicina = stavka.Kolicina,
                        Cijena = stavka.Jelo.Cijena
                    });
            }

            _context.KorpaStavke.RemoveRange(korpa);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "MojeNarudzbe",
                "Narudzba");
        }
    }
}