using Glorpa.Data;
using Glorpa.Enums;
using Glorpa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Glorpa.Controllers
{
    public class DostavaController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public DostavaController(
            DataContext context,
            UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    

        // GET: Dostava
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Dostave
                .Include(d => d.Narudzba)
                .Include(d => d.VremenskiUslovi);

            return View(await dataContext.ToListAsync());
        }

        // GET: Dostava/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostava = await _context.Dostave
                .Include(d => d.Narudzba)
                .Include(d => d.VremenskiUslovi)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dostava == null)
            {
                return NotFound();
            }

            return View(dostava);
        }

        // GET: Dostava/Create
        public IActionResult Create()
        {
            ViewData["NarudzbaId"] = new SelectList(_context.Narudzbe, "Id", "Id");

            ViewData["VremenskiUsloviId"] =
                new SelectList(_context.VremenskiUslovi, "Id", "Id");

            return View();
        }

        // POST: Dostava/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Status,VrijemePreuzimanja,VrijemeDostave,NarudzbaId,VremenskiUsloviId")]
    Dostava dostava)
        {
            Console.WriteLine("USAO POST CREATE");

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _context.Add(dostava);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["NarudzbaId"] =
                new SelectList(_context.Narudzbe, "Id", "Id", dostava.NarudzbaId);

            ViewData["VremenskiUsloviId"] =
                new SelectList(_context.VremenskiUslovi,
                    "Id",
                    "Id",
                    dostava.VremenskiUsloviId);

            return View(dostava);
        }

        // GET: Dostava/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostava = await _context.Dostave.FindAsync(id);

            if (dostava == null)
            {
                return NotFound();
            }

            ViewData["NarudzbaId"] =
                new SelectList(_context.Narudzbe,
                    "Id",
                    "Id",
                    dostava.NarudzbaId);

            ViewData["VremenskiUsloviId"] =
                new SelectList(_context.VremenskiUslovi,
                    "Id",
                    "Id",
                    dostava.VremenskiUsloviId);

            return View(dostava);
        }

        // POST: Dostava/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Status,VrijemePreuzimanja,VrijemeDostave,NarudzbaId,VremenskiUsloviId")]
            Dostava dostava)
        {
            if (id != dostava.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dostava);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DostavaExists(dostava.Id))
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

            ViewData["NarudzbaId"] =
                new SelectList(_context.Narudzbe,
                    "Id",
                    "Id",
                    dostava.NarudzbaId);

            ViewData["VremenskiUsloviId"] =
                new SelectList(_context.VremenskiUslovi,
                    "Id",
                    "Id",
                    dostava.VremenskiUsloviId);

            return View(dostava);
        }

        // GET: Dostava/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostava = await _context.Dostave
                .Include(d => d.Narudzba)
                .Include(d => d.VremenskiUslovi)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dostava == null)
            {
                return NotFound();
            }

            return View(dostava);
        }

        // POST: Dostava/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dostava = await _context.Dostave.FindAsync(id);

            if (dostava != null)
            {
                _context.Dostave.Remove(dostava);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MojeDostave()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var dostave =
                await _context.Dostave
                    .Include(d => d.Narudzba)
                        .ThenInclude(n => n.Korisnik)
                    .Include(d => d.VremenskiUslovi)
                    .Where(d => d.DostavljacId == korisnik.Id)
                    .OrderByDescending(d => d.Id)
                    .ToListAsync();
            var danas = DateTime.Today;

            ViewBag.Danas =
                dostave.Count(x =>
                    x.Status == "DOSTAVLJENA" &&
                    x.VrijemeDostave.Date == danas);

            ViewBag.Sedmica =
                dostave.Count(x =>
                    x.Status == "DOSTAVLJENA" &&
                    x.VrijemeDostave >= danas.AddDays(-7));

            ViewBag.Mjesec =
                dostave.Count(x =>
                    x.Status == "DOSTAVLJENA" &&
                    x.VrijemeDostave.Month == DateTime.Now.Month &&
                    x.VrijemeDostave.Year == DateTime.Now.Year);

            return View(dostave);
        }
        public async Task<IActionResult> StatusDostave()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var dostave =
        await _context.Dostave

            .Include(d => d.Narudzba)
                .ThenInclude(n => n.Korisnik)

            .Include(d => d.Narudzba)
                .ThenInclude(n => n.StavkeNarudzbe)
                    .ThenInclude(s => s.Jelo)
                        .ThenInclude(j => j.Restoran)

            .Where(d =>
                d.DostavljacId == korisnik.Id &&
                d.Status != "DOSTAVLJENA")

            .OrderBy(d => d.Id)

            .ToListAsync();

            return View(dostave);
        }
        [HttpPost]
        public async Task<IActionResult> SljedeciStatus(int id)
        {
            var dostava =
      await _context.Dostave
      .Include(d => d.Narudzba)
      .Include(d => d.VremenskiUslovi)
      .FirstOrDefaultAsync(d => d.Id == id);

            if (dostava == null)
            {
                return NotFound();
            }

            switch (dostava.Status)
            {
                case "Prihvacena":
                case "CEKA_PREUZIMANJE":

                    dostava.Status = "PREUZETA";

                    dostava.Narudzba.Status = "PREUZETA";

                    dostava.VrijemePreuzimanja =
                        DateTime.Now;
                    _context.Notifikacije.Add(
    new Notifikacija
    {
        KorisnikId =
            dostava.Narudzba.KorisnikId,

        Poruka =
            "🚚 Dostavljač je preuzeo vašu narudžbu.",

        Procitana = false,

        Datum = DateTime.Now
    });
                    break;

                case "PREUZETA":

                    dostava.Status = "NA_PUTU";

                    dostava.Narudzba.Status = "NA_PUTU";

                    dostava.VrijemePolaska =
                        DateTime.Now;
                    _context.Notifikacije.Add(
    new Notifikacija
    {
        KorisnikId =
            dostava.Narudzba.KorisnikId,

        Poruka =
            "🚚 Vaša narudžba je na putu.",

        Procitana = false,

        Datum = DateTime.Now
    });
                    break;

                case "NA_PUTU":

                    dostava.Status = "DOSTAVLJENA";
                    dostava.Narudzba.Status = "DOSTAVLJENA";
                    dostava.VrijemeDostave = DateTime.Now;

                    var dostavljac =
                        await _userManager.FindByIdAsync(
                            dostava.DostavljacId);
                    if (dostava.Narudzba.NacinPlacanja
    == TipPlacanja.Kes)
                    {
                        dostavljac.TrenutniDug +=
                            dostava.Narudzba.UkupnaCijena;
                    }

                    double iznos = 5;

                    if (dostavljac?.DostavnoSredstvo == "AUTOMOBIL")
                    {
                        iznos += dostava.Kilometara * 0.1;
                    }

                    double iznosZarade = 5;
                    if (dostava.VremenskiUslovi?.Tip == "Jaka kisa"
       || dostava.VremenskiUslovi?.Tip == "Snijeg"
       || dostava.VremenskiUslovi?.Tip == "Poledica")
                    {
                        iznosZarade += 0.50;
                    }
                    if (dostavljac?.DostavnoSredstvo == "AUTOMOBIL")
                    {
                        iznosZarade +=
                            dostava.Kilometara * 0.1;
                    }

                    var novaZarada = new Zarada
                    {
                        DostavaId = dostava.Id,

                       
                        Iznos = Math.Round(iznosZarade, 2),

                        Datum = DateTime.Now,

                        Sat = DateTime.Now.Hour
                    };

                    if (dostavljac?.DostavnoSredstvo == "AUTOMOBIL")
                    {
                        novaZarada.TipZarade =
                            TipZarade.ZAuto;
                    }
                    else
                    {
                        novaZarada.TipZarade =
                            TipZarade.ZBiciklo;
                    }
                    dostavljac.StanjeUKasi +=
    iznosZarade;
                    _context.Zarade.Add(novaZarada);
                    _context.Notifikacije.Add(
    new Notifikacija
    {
        KorisnikId =
            dostava.Narudzba.KorisnikId,

        Poruka =
            "✅ Vaša narudžba je uspješno dostavljena.",

        Procitana = false,

        Datum = DateTime.Now
    });
                    break;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(StatusDostave));
        }

        [HttpPost]
        public async Task<IActionResult> PromijeniDostavnoSredstvo()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return Json(new
                {
                    uspjeh = false
                });
            }

            if (korisnik.Uloga != Enums.TipKorisnika.Dostavljac)
            {
                return Json(new
                {
                    uspjeh = false
                });
            }

            var dostavljaci =
            await _context.Korisnici
                .Where(x =>
                    x.Uloga == Enums.TipKorisnika.Dostavljac &&
                    (
                        x.DostavnoSredstvo == "BICIKL" ||
                        x.DostavnoSredstvo == "AUTOMOBIL"
                    ))
                .ToListAsync();
            int brojAuto =
     dostavljaci.Count(x =>
         x.DostavnoSredstvo == "AUTOMOBIL");

            int brojBicikl =
                dostavljaci.Count(x =>
                    x.DostavnoSredstvo == "BICIKL");

            int ukupno =
                brojAuto + brojBicikl;
            
                if (korisnik.DostavnoSredstvo == "BICIKL")
            {
                if (ukupno > 15)
            {

                double noviProcenat =
                    ((double)(brojAuto + 1)
                    / ukupno) * 100;

                if (noviProcenat > 40)
                {
                    return Json(new
                    {
                        uspjeh = false,
                        poruka =
                        "Maksimalno 40% dostavljača može koristiti automobil."
                    });
                }
                }

                korisnik.DostavnoSredstvo =
                    "AUTOMOBIL";
            }
            else
            {
                korisnik.DostavnoSredstvo =
                    "BICIKL";
            }

            await _context.SaveChangesAsync();

            return Json(new
            {
                uspjeh = true,
                novoStanje =
                    korisnik.DostavnoSredstvo
            });
        }
        private bool DostavaExists(int id)
        {
            return _context.Dostave.Any(e => e.Id == id);
        }
    }
}