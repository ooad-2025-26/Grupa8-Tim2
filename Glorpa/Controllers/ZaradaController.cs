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
    public class ZaradaController : Controller
    {
        private readonly DataContext _context;

        public ZaradaController(DataContext context)
        {
            _context = context;
        }

        // GET: Zarada
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Zarade
                .Include(z => z.Dostava);

            return View(await dataContext.ToListAsync());
        }

        // GET: Zarada/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarada = await _context.Zarade
                .Include(z => z.Dostava)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zarada == null)
            {
                return NotFound();
            }

            return View(zarada);
        }

        // GET: Zarada/Create
        public IActionResult Create()
        {
            ViewData["DostavaId"] =
                new SelectList(_context.Dostave, "Id", "Id");

            return View();
        }

        // POST: Zarada/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Iznos,Datum,TipZarade,Sat,DostavaId")]
            Zarada zarada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zarada);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DostavaId"] =
                new SelectList(_context.Dostave,
                    "Id",
                    "Id",
                    zarada.DostavaId);

            return View(zarada);
        }

        // GET: Zarada/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarada = await _context.Zarade.FindAsync(id);

            if (zarada == null)
            {
                return NotFound();
            }

            ViewData["DostavaId"] =
                new SelectList(_context.Dostave,
                    "Id",
                    "Id",
                    zarada.DostavaId);

            return View(zarada);
        }

        // POST: Zarada/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Iznos,Datum,TipZarade,Sat,DostavaId")]
            Zarada zarada)
        {
            if (id != zarada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zarada);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaradaExists(zarada.Id))
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

            ViewData["DostavaId"] =
                new SelectList(_context.Dostave,
                    "Id",
                    "Id",
                    zarada.DostavaId);

            return View(zarada);
        }

        // GET: Zarada/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarada = await _context.Zarade
                .Include(z => z.Dostava)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zarada == null)
            {
                return NotFound();
            }

            return View(zarada);
        }

        // POST: Zarada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zarada = await _context.Zarade.FindAsync(id);

            if (zarada != null)
            {
                _context.Zarade.Remove(zarada);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ZaradaExists(int id)
        {
            return _context.Zarade.Any(e => e.Id == id);
        }
    }
}