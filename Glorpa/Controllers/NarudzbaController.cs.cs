using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NarudzbaController : ControllerBase
    {
        private readonly DataContext _context;

        public NarudzbaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNarudzbe()
        {
            return Ok(await _context.Narudzbe.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> KreirajNarudzbu(Narudzba narudzba)
        {
            _context.Narudzbe.Add(narudzba);
            await _context.SaveChangesAsync();

            return Ok(narudzba);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ObrisiNarudzbu(int id)
        {
            var narudzba = await _context.Narudzbe.FindAsync(id);

            if (narudzba == null)
                return NotFound();

            _context.Narudzbe.Remove(narudzba);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}