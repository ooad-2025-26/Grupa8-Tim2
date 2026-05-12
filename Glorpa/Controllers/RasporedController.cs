using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RasporedController : ControllerBase
    {
        private readonly DataContext _context;

        public RasporedController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRasporedi()
        {
            return Ok(await _context.Rasporedi.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DodajTermin(Termin termin)
        {
            _context.Termini.Add(termin);
            await _context.SaveChangesAsync();

            return Ok(termin);
        }
    }
}