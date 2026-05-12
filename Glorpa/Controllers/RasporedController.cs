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
    public class RasporedController : Controller
    {
        private readonly DataContext _context;

        public RasporedController(DataContext context)
        {
            _context = context;
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

        private bool RasporedExists(int id)
        {
            return _context.Rasporedi.Any(e => e.Id == id);
        }
    }
}