using Glorpa.Enums;
using Glorpa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Glorpa.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<Korisnik> _userManager;

        public DashboardController(
            UserManager<Korisnik> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
                return RedirectToAction(
                    "Login",
                    "Account");
            ViewBag.Ime = korisnik.Ime;
            if (korisnik.Uloga == TipKorisnika.Kupac)
            {
                return View("KupacDashboard");
            }

            if (korisnik.Uloga == TipKorisnika.Dostavljac)
            {
                return View("DostavljacDashboard");
            }

            return View("AdminDashboard");
        }
    }
}