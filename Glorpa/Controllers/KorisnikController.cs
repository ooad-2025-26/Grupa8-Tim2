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
    public class KorisnikController : Controller
    {
        private readonly DataContext _context;

        public KorisnikController(DataContext context)
        {
            _context = context;
        }

        // GET: Korisnik
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Korisnici
                .Include(k => k.Dug);

            return View(await dataContext.ToListAsync());
        }

        // GET: Korisnik/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnici
                .Include(k => k.Dug)
                .Include(k => k.Narudzbe)
                .Include(k => k.ZahtjeviPodrske)
                .Include(k => k.Rasporedi)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Korisnik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisnik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Ime,Prezime,Email,PhoneNumber,Uloga")]
            Korisnik korisnik)
        {
            Console.WriteLine("USAO U CREATE");

            // Identity zahtijeva UserName
            korisnik.UserName = korisnik.Email;

            if (ModelState.IsValid)
            {
                Console.WriteLine("MODEL VALID");

                _context.Add(korisnik);

                await _context.SaveChangesAsync();

                Console.WriteLine("KORISNIK SACUVAN");

                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("MODEL NIJE VALID");

            return View(korisnik);
        }

        // GET: Korisnik/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnici.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            [Bind("Id,Ime,Prezime,Email,PhoneNumber,Uloga")]
            Korisnik korisnik)
        {
            if (id != korisnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var korisnikIzBaze = await _context.Korisnici.FindAsync(id);

                if (korisnikIzBaze == null)
                {
                    return NotFound();
                }

                korisnikIzBaze.Ime = korisnik.Ime;
                korisnikIzBaze.Prezime = korisnik.Prezime;
                korisnikIzBaze.Email = korisnik.Email;
                korisnikIzBaze.UserName = korisnik.Email;
                korisnikIzBaze.PhoneNumber = korisnik.PhoneNumber;
                korisnikIzBaze.Uloga = korisnik.Uloga;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(korisnik);
        }

        // GET: Korisnik/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(m => m.Id == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _context.Korisnici.FindAsync(id);

            if (korisnik != null)
            {
                _context.Korisnici.Remove(korisnik);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(string id)
        {
            return _context.Korisnici.Any(e => e.Id == id);
        }
    }
}