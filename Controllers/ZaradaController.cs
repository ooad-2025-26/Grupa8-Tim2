using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZaradaController : ControllerBase
    {
        private readonly DataContext _context;

        public ZaradaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetZarade()
        {
            return Ok(await _context.Zarade.ToListAsync());
        }

        [HttpGet("dugovi")]
        public async Task<IActionResult> GetDugovi()
        {
            return Ok(await _context.Dugovi.ToListAsync());
        }
    }
}