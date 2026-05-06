using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glorpa.Data;

namespace Glorpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("dostavljaci")]
        public async Task<IActionResult> GetDostavljaci()
        {
            return Ok(await _context.Dostavljaci.ToListAsync());
        }
    }
}