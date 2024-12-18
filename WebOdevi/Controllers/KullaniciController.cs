using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebOdevi.Models;

namespace WebOdevi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;

        public KullaniciController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Kullanıcı kaydı (GET)
        public IActionResult Register()
        {
            return View();
        }

        // Kullanıcı kaydı (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Kullanici { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // Kullanıcı giriş (GET)
        public IActionResult Login()
        {
            return View();
        }

        // Kullanıcı giriş (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz giriş.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                }
            }
            return View(model);
        }

        // Kullanıcı çıkış
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
