using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;
using Glorpa.Enums;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Text.Json;
namespace Glorpa.Controllers
{
    public class NarudzbaController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public NarudzbaController(
         DataContext context,
         UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ProvjeriNotifikacije()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return Json(new
                {
                    ima = false
                });
            }

            var notifikacija =
                await _context.Notifikacije
                    .Where(x =>
                        x.KorisnikId == korisnik.Id
                        &&
                        !x.Procitana)
                    .OrderBy(x => x.Datum)
                    .FirstOrDefaultAsync();

            if (notifikacija == null)
            {
                return Json(new
                {
                    ima = false
                });
            }

            notifikacija.Procitana = true;

            await _context.SaveChangesAsync();

            return Json(new
            {
                ima = true,
                poruka = notifikacija.Poruka
            });
        }

        public async Task<IActionResult> PratiDostavu(int id)
        {
            var dostava =
                await _context.Dostave
                    .Include(d => d.Narudzba)
                    .FirstOrDefaultAsync(
                        d => d.NarudzbaId == id);

            if (dostava == null)
            {
                return NotFound();
            }

            return View(dostava);
        }
        public async Task<IActionResult> MojeNarudzbe()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var narudzbe =
                await _context.Narudzbe
                    .Where(n => n.KorisnikId == korisnik.Id)
                    .OrderByDescending(n => n.Datum)
                    .ToListAsync();

            return View(narudzbe);
        }
        // GET: Narudzba
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Narudzbe
                .Include(n => n.Korisnik)
                .Include(n => n.Dostava);

            return View(await dataContext.ToListAsync());
        }

        // GET: Narudzba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Korisnik)
                .Include(n => n.Dostava)
               .Include(n => n.StavkeNarudzbe)
    .ThenInclude(s => s.Jelo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Narudzba/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici, "Id", "Ime");

            return View();
        }
        // POST: Narudzba/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("Id,Datum,Status,UkupnaCijena,NacinPlacanja,KorisnikId")]
    Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narudzba);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["KorisnikId"] =
                new SelectList(
                    _context.Korisnici,
                    "Id",
                    "Ime",
                    narudzba.KorisnikId);

            return View(narudzba);
        }
        [HttpPost]
        public async Task<IActionResult> DodijeliNarudzbu(
     int narudzbaId,
     string dostavljacId)
        {
            var narudzba =
        await _context.Narudzbe

            .Include(n => n.StavkeNarudzbe)
                .ThenInclude(s => s.Jelo)
                    .ThenInclude(j => j.Restoran)

            .FirstOrDefaultAsync(n => n.Id == narudzbaId);

            if (narudzba == null)
            {
                return NotFound();
            }
            var restoran =
    narudzba.StavkeNarudzbe
        ?.FirstOrDefault()
        ?.Jelo
        ?.Restoran;

            string adresaRestorana =
                restoran?.Adresa ?? "";

            string adresaKupca =
                narudzba.AdresaDostave ?? "";

            double kilometara = 0;

            if (!string.IsNullOrWhiteSpace(adresaRestorana) &&
                !string.IsNullOrWhiteSpace(adresaKupca))
            {
                var r =
                    await Geocode(adresaRestorana);

                var k =
                    await Geocode(adresaKupca);

                if (!(r.lat == 0 && r.lon == 0) &&
                    !(k.lat == 0 && k.lon == 0))
                {
                    kilometara =
                        IzracunajKm(
                            r.lat,
                            r.lon,
                            k.lat,
                            k.lon);
                }
            }
            var vremenskiUslovId =
                await _context.VremenskiUslovi
                .OrderBy(x => Guid.NewGuid())
                .Select(x => x.Id)
                .FirstAsync();
            var dostava = new Dostava
            {
                NarudzbaId = narudzbaId,
                DostavljacId = dostavljacId,
                Status = "CEKA_PREUZIMANJE",
                VrijemePreuzimanja = DateTime.Now,
                VrijemeDostave = DateTime.Now,

                Kilometara = kilometara,

                VremenskiUsloviId = vremenskiUslovId
            };

            _context.Dostave.Add(dostava);

            narudzba.Status = "CEKA_PREUZIMANJE";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Narudzba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe.FindAsync(id);

            if (narudzba == null)
            {
                return NotFound();
            }

            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    narudzba.KorisnikId);

            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Datum,Status,UkupnaCijena,NacinPlacanja,KorisnikId")]
            Narudzba narudzba)
        {
            if (id != narudzba.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    narudzba.KorisnikId);

            return View(narudzba);
        }

        // GET: Narudzba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await _context.Narudzbe.FindAsync(id);

            if (narudzba != null)
            {
                _context.Narudzbe.Remove(narudzba);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Dodijeli(int id)
        {
            var narudzba = await _context.Narudzbe
                .Include(n => n.Korisnik)
                .FirstOrDefaultAsync(n => n.Id == id);

            bool teskaNarudzba =
                narudzba.UkupnaTezinaKg > 5;

            if (narudzba == null)
            {
                return NotFound();
            }
            var sada = DateTime.Now;

            string danas =
                sada.DayOfWeek switch
                {
                    DayOfWeek.Monday => "PONEDJELJAK",
                    DayOfWeek.Tuesday => "UTORAK",
                    DayOfWeek.Wednesday => "SRIJEDA",
                    DayOfWeek.Thursday => "CETVRTAK",
                    DayOfWeek.Friday => "PETAK",
                    DayOfWeek.Saturday => "SUBOTA",
                    _ => "NEDJELJA"
                };

            var sviDostavljaci = await _context.Korisnici
                .Where(k => k.Uloga == TipKorisnika.Dostavljac)
                .ToListAsync();

            var dostupniDostavljaci =
                new List<Korisnik>();

            foreach (var d in sviDostavljaci)
            {
                bool radiSada =
    await _context.Termini

        .Include(t => t.Raspored)

        .AnyAsync(t =>

            t.Raspored.KorisnikId == d.Id
            && t.Dan == danas
            && t.Pocetak.Hour <= sada.Hour
            && t.Kraj.Hour > sada.Hour
        );

                if (!radiSada)
                {
                    continue;
                }
                if (d.TrenutniDug >= 500)
                {
                    continue;
                }
                int brojAktivnihDostava =
                    await _context.Dostave
                        .CountAsync(x =>
                            x.DostavljacId == d.Id &&
                            x.Status != "DOSTAVLJENA");

                if (d.DostavnoSredstvo == "BICIKL")
                {
                    if (teskaNarudzba)
                    {
                        continue;
                    }

                    if (brojAktivnihDostava < 1)
                    {
                        dostupniDostavljaci.Add(d);
                    }
                }
                else if (d.DostavnoSredstvo == "AUTOMOBIL")
                {
                    if (brojAktivnihDostava < 3)
                    {
                        dostupniDostavljaci.Add(d);
                    }
                }
            }

            ViewBag.Dostavljaci = dostupniDostavljaci;

            return View(narudzba);
        }
        private async Task<(double lat, double lon)> Geocode(string adresa)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "GlorpaApp/1.0");

            string url =
                $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(adresa)}";

            string json =
                await client.GetStringAsync(url);

            var doc =
                JsonDocument.Parse(json);

            if (doc.RootElement.GetArrayLength() == 0)
            {
                return (0, 0);
            }

            var prvi =
                doc.RootElement[0];

            return (
                double.Parse(prvi.GetProperty("lat").GetString()),
                double.Parse(prvi.GetProperty("lon").GetString())
            );
        }

        private double IzracunajKm(
            double lat1,
            double lon1,
            double lat2,
            double lon2)
        {
            double R = 6371;

            double dLat =
                (lat2 - lat1) * Math.PI / 180;

            double dLon =
                (lon2 - lon1) * Math.PI / 180;

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                +
                Math.Cos(lat1 * Math.PI / 180)
                * Math.Cos(lat2 * Math.PI / 180)
                *
                Math.Sin(dLon / 2)
                * Math.Sin(dLon / 2);

            double c =
                2 * Math.Atan2(
                    Math.Sqrt(a),
                    Math.Sqrt(1 - a));

            return R * c;
        }
        private bool NarudzbaExists(int id)
        {
            return _context.Narudzbe.Any(e => e.Id == id);
        }
    }
}