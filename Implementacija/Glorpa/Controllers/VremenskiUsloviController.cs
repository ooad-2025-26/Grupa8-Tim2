using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    public class VremenskiUsloviController : Controller
    {
        private readonly DataContext _context;

        public VremenskiUsloviController(DataContext context)
        {
            _context = context;
        }

        // GET: VremenskiUslovi
        public async Task<IActionResult> Index()
        {
            return View(await _context.VremenskiUslovi.ToListAsync());
        }

        // GET: VremenskiUslovi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vremenskiUslovi = await _context.VremenskiUslovi
                .Include(v => v.Dostave)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vremenskiUslovi == null)
            {
                return NotFound();
            }

            return View(vremenskiUslovi);
        }

        // GET: VremenskiUslovi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VremenskiUslovi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Tip,Opis,JeLiUslov")]
            VremenskiUslovi vremenskiUslovi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vremenskiUslovi);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vremenskiUslovi);
        }

        // GET: VremenskiUslovi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vremenskiUslovi =
                await _context.VremenskiUslovi.FindAsync(id);

            if (vremenskiUslovi == null)
            {
                return NotFound();
            }

            return View(vremenskiUslovi);
        }

        // POST: VremenskiUslovi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Tip,Opis,JeLiUslov")]
            VremenskiUslovi vremenskiUslovi)
        {
            if (id != vremenskiUslovi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vremenskiUslovi);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VremenskiUsloviExists(vremenskiUslovi.Id))
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

            return View(vremenskiUslovi);
        }

        // GET: VremenskiUslovi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vremenskiUslovi = await _context.VremenskiUslovi
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vremenskiUslovi == null)
            {
                return NotFound();
            }

            return View(vremenskiUslovi);
        }

        // POST: VremenskiUslovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vremenskiUslovi =
                await _context.VremenskiUslovi.FindAsync(id);

            if (vremenskiUslovi != null)
            {
                _context.VremenskiUslovi.Remove(vremenskiUslovi);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool VremenskiUsloviExists(int id)
        {
            return _context.VremenskiUslovi.Any(e => e.Id == id);
        }
    }
}