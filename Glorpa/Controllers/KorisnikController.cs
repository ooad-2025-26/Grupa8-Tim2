using Glorpa.Data;
using Glorpa.Enums;
using Glorpa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly DataContext _context;

        public KorisnikController(DataContext context)
        {
            _context = context;
        }

        // svi korisnici
        [HttpGet]
        public async Task<IActionResult> GetKorisnici()
        {
            return Ok(await _context.Korisnici.ToListAsync());
        }

        // samo kupci
        [HttpGet("kupci")]
        public async Task<IActionResult> GetKupci()
        {
            var kupci = await _context.Korisnici
                .Where(k => k.Uloga == TipKorisnika.Kupac)
                .ToListAsync();

            return Ok(kupci);
        }

        // samo dostavljaci
        [HttpGet("dostavljaci")]
        public async Task<IActionResult> GetDostavljaci()
        {
            var dostavljaci = await _context.Korisnici
                .Where(k => k.Uloga == TipKorisnika.Dostavljac)
                .ToListAsync();

            return Ok(dostavljaci);
        }

        // samo administratori
        [HttpGet("administratori")]
        public async Task<IActionResult> GetAdministratori()
        {
            var administratori = await _context.Korisnici
                .Where(k => k.Uloga == TipKorisnika.Administrator)
                .ToListAsync();

            return Ok(administratori);
        }
    }
}