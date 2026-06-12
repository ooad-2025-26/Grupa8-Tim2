using Glorpa.Data;
using Glorpa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glorpa.Controllers
{
    public class RestoranController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public RestoranController(
            DataContext context,
            UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var restorani =
                await _context.Restorani
                    .Include(r => r.Jela)
                    .ToListAsync();

            return View(restorani);
        }

        public async Task<IActionResult> Jela(int id)
        {
            var jela =
                await _context.Jela
                    .Where(j => j.RestoranId == id)
                    .ToListAsync();

            return View(jela);
        }

        [HttpPost]
        public async Task<IActionResult> DodajUKorpu(int jeloId)
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var jelo =
                await _context.Jela
                    .FirstOrDefaultAsync(x => x.Id == jeloId);

            if (jelo == null)
            {
                return NotFound();
            }

            var korpa =
                await _context.KorpaStavke
                    .Include(x => x.Jelo)
                    .Where(x => x.KorisnikId == korisnik.Id)
                    .ToListAsync();

            if (korpa.Any())
            {
                int restoranUKorpi =
                    korpa.First().Jelo.RestoranId;

                if (restoranUKorpi != jelo.RestoranId)
                {
                    TempData["Greska"] =
                        "Možete naručivati samo iz jednog restorana. Prvo ispraznite korpu.";

                    return RedirectToAction(
                        nameof(Jela),
                        new { id = jelo.RestoranId });
                }
            }

            var postojecaStavka =
                await _context.KorpaStavke
                    .FirstOrDefaultAsync(x =>
                        x.KorisnikId == korisnik.Id &&
                        x.JeloId == jeloId);

            if (postojecaStavka != null)
            {
                postojecaStavka.Kolicina++;

                await _context.SaveChangesAsync();

                return RedirectToAction(
                    "Index",
                    "Korpa");
            }

            var stavka =
                new KorpaStavka
                {
                    KorisnikId = korisnik.Id,
                    JeloId = jeloId,
                    Kolicina = 1
                };

            _context.KorpaStavke.Add(stavka);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Index",
                "Korpa");
        }
    }
}