using Glorpa.Data;
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
    public class ZahtjevPodrskeController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ZahtjevPodrskeController(
    DataContext context,
    UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: ZahtjevPodrske
        public async Task<IActionResult> Index()
        {
            var zahtjevi =
                await _context.ZahtjeviPodrske

                    .Include(z => z.Korisnik)

                    .Where(z => z.Status != "OBRADJEN")

                    .OrderByDescending(z =>
                        z.Prioritet == "VISOK")

                    .ThenByDescending(z =>
                        z.Prioritet == "SREDNJI")

                    .ThenByDescending(z =>
                        z.Datum)

                    .ToListAsync();

            return View(zahtjevi);
        }
        // GET: ZahtjevPodrske/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevPodrske = await _context.ZahtjeviPodrske
                .Include(z => z.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zahtjevPodrske == null)
            {
                return NotFound();
            }

            return View(zahtjevPodrske);
        }

        // GET: ZahtjevPodrske/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici, "Id", "Ime");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
      [Bind("TipZahtjeva,Opis")]
    ZahtjevPodrske zahtjevPodrske)
        {
            Console.WriteLine("USAO U CREATE");

            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                Console.WriteLine("KORISNIK NULL");

                return RedirectToAction(
                    "Login",
                    "Account");
            }

            switch (zahtjevPodrske.TipZahtjeva)
            {
                case "NEZGODA_U_RADU":
                case "UNIŠTENA_DOSTAVA":

                    zahtjevPodrske.Prioritet = "VISOK";

                    break;

                case "KUPAC_PROBLEM":

                    zahtjevPodrske.Prioritet = "SREDNJI";

                    break;

                default:

                    zahtjevPodrske.Prioritet = "NIZAK";

                    break;
            }

            zahtjevPodrske.Datum =
                DateTime.Now;

            zahtjevPodrske.Status =
                "OTVOREN";

            zahtjevPodrske.KorisnikId =
                korisnik.Id;

            Console.WriteLine("POKUSAVAM SACUVATI");

            _context.ZahtjeviPodrske.Add(zahtjevPodrske);

            await _context.SaveChangesAsync();

            Console.WriteLine("ZAHTJEV SACUVAN");

            return RedirectToAction(
                "Index",
                "Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> OznaciObradjen(int id)
        {
            var zahtjev =
                await _context.ZahtjeviPodrske
                .FindAsync(id);

            if (zahtjev == null)
            {
                return NotFound();
            }

            zahtjev.Status = "OBRADJEN";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ZahtjevPodrske/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevPodrske =
                await _context.ZahtjeviPodrske.FindAsync(id);

            if (zahtjevPodrske == null)
            {
                return NotFound();
            }

            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    zahtjevPodrske.KorisnikId);

            return View(zahtjevPodrske);
        }

        // POST: ZahtjevPodrske/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,TipZahtjeva,Prioritet,Opis,Status,Datum,KorisnikId")]
            ZahtjevPodrske zahtjevPodrske)
        {
            if (id != zahtjevPodrske.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zahtjevPodrske);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZahtjevPodrskeExists(zahtjevPodrske.Id))
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
                    zahtjevPodrske.KorisnikId);

            return View(zahtjevPodrske);
        }

        // GET: ZahtjevPodrske/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevPodrske = await _context.ZahtjeviPodrske
                .Include(z => z.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zahtjevPodrske == null)
            {
                return NotFound();
            }

            return View(zahtjevPodrske);
        }

        // POST: ZahtjevPodrske/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zahtjevPodrske =
                await _context.ZahtjeviPodrske.FindAsync(id);

            if (zahtjevPodrske != null)
            {
                _context.ZahtjeviPodrske.Remove(zahtjevPodrske);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ZahtjevPodrskeExists(int id)
        {
            return _context.ZahtjeviPodrske.Any(e => e.Id == id);
        }
    }
}