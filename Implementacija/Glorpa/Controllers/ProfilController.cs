using Glorpa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Glorpa.Controllers
{
    public class ProfilController : Controller
    {
        private readonly UserManager<Korisnik> _userManager;

        public ProfilController(
            UserManager<Korisnik> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MojProfil()
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
                return RedirectToAction(
                    "Login",
                    "Account");

            return View(korisnik);
        }



        [HttpPost]
        public async Task<IActionResult> SacuvajProfil(
    Korisnik model)
        {
            var korisnik =
                await _userManager.GetUserAsync(User);

            if (korisnik == null)
                return RedirectToAction(
                    "Login",
                    "Account");

            korisnik.Ime = model.Ime;
            korisnik.Prezime = model.Prezime;
            korisnik.Email = model.Email;
            korisnik.UserName = model.Email;
            korisnik.PhoneNumber = model.PhoneNumber;
            korisnik.Adresa = model.Adresa;
            await _userManager.UpdateAsync(korisnik);

            return RedirectToAction(nameof(MojProfil));
        }
    }


}