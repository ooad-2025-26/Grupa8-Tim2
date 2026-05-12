using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    public class ZahtjevPodrskeController : Controller
    {
        private readonly DataContext _context;

        public ZahtjevPodrskeController(DataContext context)
        {
            _context = context;
        }

        // GET: ZahtjevPodrske
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ZahtjeviPodrske
                .Include(z => z.Korisnik);

            return View(await dataContext.ToListAsync());
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

        // POST: ZahtjevPodrske/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,TipZahtjeva,Prioritet,Opis,Status,Datum,KorisnikId")]
            ZahtjevPodrske zahtjevPodrske)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zahtjevPodrske);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["KorisnikId"] =
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    zahtjevPodrske.KorisnikId);

            return View(zahtjevPodrske);
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