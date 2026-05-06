
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifikacijaController : ControllerBase
    {
        private readonly DataContext _context;

        public NotifikacijaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifikacije()
        {
            return Ok(await _context.Notifikacije.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PosaljiNotifikaciju(Notifikacija notifikacija)
        {
            _context.Notifikacije.Add(notifikacija);
            await _context.SaveChangesAsync();

            return Ok(notifikacija);
        }
    }
}