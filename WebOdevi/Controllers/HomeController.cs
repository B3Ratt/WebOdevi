using Microsoft.AspNetCore.Mvc;

namespace WebOdevi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Berberin adı ve hoş geldiniz mesajını ViewData ile gönderiyoruz
            ViewData["BerberAd"] = "X-Sir";  // Berberin adı
            ViewData["WelcomeMessage"] = "Hoşgeldiniz!";  // Hoşgeldiniz mesajı

            // İlgili görselin yolu
            ViewData["ImageUrl"] = "/images/berber.jpg";

            return View();
        }
    }
}
