using Glorpa.Enums;
using Glorpa.Models;
using Glorpa.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Glorpa.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;

        public AccountController(
            UserManager<Korisnik> userManager,
            SignInManager<Korisnik> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ====================================
        // REGISTRACIJA KUPCA
        // ====================================

        [HttpGet]
        public IActionResult RegisterKupac()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterKupac(
            RegisterViewModel model)
        {
            return await Registruj(
                model,
                TipKorisnika.Kupac,
                "RegisterKupac");
        }

        // ====================================
        // REGISTRACIJA DOSTAVLJAČA
        // ====================================

        [HttpGet]
        public IActionResult RegisterDostavljac()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDostavljac(
            RegisterViewModel model)
        {
            return await Registruj(
                model,
                TipKorisnika.Dostavljac,
                "RegisterDostavljac");
        }

        // ====================================
        // REGISTRACIJA ADMINISTRATORA
        // ====================================

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(
            RegisterViewModel model)
        {
            return await Registruj(
                model,
                TipKorisnika.Administrator,
                "RegisterAdmin");
        }

        // ====================================
        // ZAJEDNIČKA METODA REGISTRACIJE
        // ====================================

        private async Task<IActionResult> Registruj(
            RegisterViewModel model,
            TipKorisnika uloga,
            string viewName)
        {
            if (!ModelState.IsValid)
            {
                return View(viewName, model);
            }

            var korisnik = new Korisnik
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.Telefon,
                Uloga = uloga
            };

            var result =
                await _userManager.CreateAsync(
                    korisnik,
                    model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(
                    korisnik,
                    false);

                return RedirectToAction(
                    "Index",
                    "Dashboard");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    "",
                    error.Description);
            }

            return View(viewName, model);
        }

        // ====================================
        // LOGIN
        // ====================================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result =
                await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    false,
                    false);

            if (result.Succeeded)
            {
                return RedirectToAction(
                    "Index",
                    "Dashboard");
            }

            ModelState.AddModelError(
                "",
                "Pogrešan email ili lozinka.");

            return View(model);
        }

        // ====================================
        // LOGOUT
        // ====================================

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(
                "Index",
                "Home");
        }
    }
}