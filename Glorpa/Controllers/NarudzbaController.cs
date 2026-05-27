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

namespace Glorpa.Controllers
{
    public class NarudzbaController : Controller
    {
        private readonly DataContext _context;

        public NarudzbaController(DataContext context)
        {
            _context = context;
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
                new SelectList(_context.Korisnici,
                    "Id",
                    "Ime",
                    narudzba.KorisnikId);

            return View(narudzba);
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

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzbe.Any(e => e.Id == id);
        }
    }
}