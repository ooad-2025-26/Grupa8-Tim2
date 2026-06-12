using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;
using Microsoft.AspNetCore.Identity;
namespace Glorpa.Controllers
{
    public class RasporedController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public RasporedController(
      DataContext context,
      UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Raspored
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Rasporedi
                .Include(r => r.Korisnik);

            return View(await dataContext.ToListAsync());
        }

        // GET: Raspored/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raspored = await _context.Rasporedi
                .Include(r => r.Korisnik)
                .Include(r => r.Termini)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (raspored == null)
            {
                return NotFound();
            }

            return View(raspored);
        }

        // GET: Raspored/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici, "Id", "Ime");

            return View();
        }

        // POST: Raspored/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Sedmica,KorisnikId")]
            Raspored raspored)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raspored);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    raspored.KorisnikId);

            return View(raspored);
        }

        // GET: Raspored/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raspored = await _context.Rasporedi.FindAsync(id);

            if (raspored == null)
            {
                return NotFound();
            }

            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    raspored.KorisnikId);

            return View(raspored);
        }

        // POST: Raspored/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Sedmica,KorisnikId")]
            Raspored raspored)
        {
            if (id != raspored.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raspored);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RasporedExists(raspored.Id))
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
                    raspored.KorisnikId);

            return View(raspored);
        }

        // GET: Raspored/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raspored = await _context.Rasporedi
                .Include(r => r.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (raspored == null)
            {
                return NotFound();
            }

            return View(raspored);
        }

        // POST: Raspored/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raspored = await _context.Rasporedi.FindAsync(id);

            if (raspored != null)
            {
                _context.Rasporedi.Remove(raspored);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MojRaspored()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);
            var trenutnaSedmica =
    PocetakSedmice(DateTime.Now);
            var raspored =
           await _context.Rasporedi

               .Include(r => r.Termini)

               .FirstOrDefaultAsync(r =>
                   r.KorisnikId == korisnik.Id &&
                   r.Sedmica == trenutnaSedmica);
            if (raspored == null)
            {
                raspored = new Raspored
                {
                    KorisnikId = korisnik.Id,
                    Sedmica = trenutnaSedmica
                };

                _context.Rasporedi.Add(raspored);

                await _context.SaveChangesAsync();
            }
            Console.WriteLine(
    "RASPORED ID = " +
    raspored.Id);

            if (korisnik == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            ViewBag.Dani = new[]
            {
        "PONEDJELJAK",
        "UTORAK",
        "SRIJEDA",
        "CETVRTAK",
        "PETAK",
        "SUBOTA",
        "NEDJELJA"
    };

            ViewBag.Sati =
                Enumerable.Range(8, 16).ToList();
            var termini =
                raspored.Termini?
                    .ToList()
                ?? new List<Termin>();


            var zakljucaniDani =
    termini
        .Select(t => t.Dan)
        .Distinct()
        .ToList();

            ViewBag.ZakljucaniDani =
                zakljucaniDani;

         
            ViewBag.Termini = termini;
            ViewBag.UkupnoSati =
    termini.Sum(t => t.Trajanje);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SacuvajDan(
     [FromBody] DanRequest request)
        {


         
       
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return Unauthorized();
            }

            var pocetakSedmice =
                PocetakSedmice(DateTime.Now);

            var raspored =
                await _context.Rasporedi
                    .Include(r => r.Termini)
                    .FirstOrDefaultAsync(r =>
                        r.KorisnikId == korisnik.Id &&
                        r.Sedmica == pocetakSedmice);
       
          
            if (raspored == null)
            {
                raspored = new Raspored
                {
                    KorisnikId = korisnik.Id,
                    Sedmica = pocetakSedmice
                };

                _context.Rasporedi.Add(raspored);

                await _context.SaveChangesAsync();
            }

            request.Sati.Sort();

            int pocetak = request.Sati[0];
            int prethodni = request.Sati[0];

            foreach (var sat in request.Sati.Skip(1))
            {
                if (sat != prethodni + 1)
                {
                    var termin = new Termin
                    {
                        Dan = request.Dan,
                        Pocetak = DateTime.Today.AddHours(pocetak),
                        Kraj = DateTime.Today.AddHours(prethodni + 1),
                        Trajanje = (prethodni + 1) - pocetak,
                        RasporedId = raspored.Id
                    };

                    _context.Termini.Add(termin);

                    pocetak = sat;
                }

                prethodni = sat;
            }

            var zadnjiTermin = new Termin
            {
                Dan = request.Dan,
                Pocetak = DateTime.Today.AddHours(pocetak),
                Kraj = DateTime.Today.AddHours(prethodni + 1),
                Trajanje = (prethodni + 1) - pocetak,
                RasporedId = raspored.Id
            };

            _context.Termini.Add(zadnjiTermin);

            await _context.SaveChangesAsync();

            return Ok();
        }
        private DateTime PocetakSedmice(DateTime datum)
        {
            int razlika =
                (7 + ((int)datum.DayOfWeek -
                (int)DayOfWeek.Monday)) % 7;

            return datum.Date.AddDays(-razlika);
        }
        private bool RasporedExists(int id)
        {
            return _context.Rasporedi.Any(e => e.Id == id);
        }
        public class DanRequest
        {
            public string Dan { get; set; }

            public List<int> Sati { get; set; }
        }
    }
}