using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodrskaController : ControllerBase
    {
        private readonly DataContext _context;

        public PodrskaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetZahtjevi()
        {
            return Ok(await _context.ZahtjeviPodrske.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PosaljiZahtjev(ZahtjevPodrske zahtjev)
        {
            _context.ZahtjeviPodrske.Add(zahtjev);
            await _context.SaveChangesAsync();

            return Ok(zahtjev);
        }
    }
}