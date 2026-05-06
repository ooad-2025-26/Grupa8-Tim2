using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;
using Glorpa.Models;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DostavaController : ControllerBase
    {
        private readonly DataContext _context;

        public DostavaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDostave()
        {
            return Ok(await _context.Dostave.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AzurirajDostavu(int id, Dostava novaDostava)
        {
            var dostava = await _context.Dostave.FindAsync(id);

            if (dostava == null)
                return NotFound();

            dostava.Status = novaDostava.Status;

            await _context.SaveChangesAsync();

            return Ok(dostava);
        }
    }
}