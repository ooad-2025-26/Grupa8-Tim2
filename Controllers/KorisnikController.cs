using Microsoft.AspNetCore.Mvc;
using Glorpa.Data;
using Glorpa.Models;
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

        [HttpGet("kupci")]
        public async Task<IActionResult> GetKupci()
        {
            return Ok(await _context.Kupci.ToListAsync());
        }

        [HttpGet("dostavljaci")]
        public async Task<IActionResult> GetDostavljaci()
        {
            return Ok(await _context.Dostavljaci.ToListAsync());
        }
    }
}