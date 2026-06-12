using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;
using Glorpa.Enums;
using Microsoft.AspNetCore.Authorization;
namespace Glorpa.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly DataContext _context;

        public KorisnikController(DataContext context)
        {
            _context = context;
        }
        private async Task<bool> AdminPristup()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
                return false;

            var korisnik =
                await _context.Korisnici
                    .FirstOrDefaultAsync(x => x.Email == email);

            return korisnik != null &&
                   korisnik.Uloga == TipKorisnika.Administrator;
        }
        // GET: Korisnik/Kupci
        public async Task<IActionResult> Kupci()
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var korisnici = await _context.Korisnici
                .Include(k => k.Dug)
                .Where(k => k.Uloga == TipKorisnika.Kupac)
                .ToListAsync();

            return View("Index", korisnici);
        }

        // GET: Korisnik/Dostavljaci
        public async Task<IActionResult> Dostavljaci()
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var korisnici = await _context.Korisnici
                .Include(k => k.Dug)
                .Where(k => k.Uloga == TipKorisnika.Dostavljac)
                .ToListAsync();

            return View("Index", korisnici);
        }

        // GET: Korisnik/Admini
        public async Task<IActionResult> Admini()
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var korisnici = await _context.Korisnici
                .Include(k => k.Dug)
                .Where(k => k.Uloga == TipKorisnika.Administrator)
                .ToListAsync();

            return View("Index", korisnici);
        }
        // GET: Korisnik
        public async Task<IActionResult> Index()
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var dataContext = _context.Korisnici
                .Include(k => k.Dug);

            return View(await dataContext.ToListAsync());
        }

        // GET: Korisnik/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
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
        public async Task<IActionResult> Create()
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        // POST: Korisnik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Ime,Prezime,Email,PhoneNumber,Uloga")]
            Korisnik korisnik)
        {
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
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
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
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
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
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
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
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
            if (!await AdminPristup())
            {
                return RedirectToAction("Index", "Dashboard");
            }
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