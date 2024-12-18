using Microsoft.AspNetCore.Mvc;
using WebOdevi.Models;

namespace WebOdevi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Randevuları listele
        public IActionResult Index()
        {
            var randevular = _context.Randevular.ToList(); // Randevular tablosunu al
            return View(randevular);
        }

        // Yeni randevu eklemek için GET metodu
        public IActionResult Create()
        {
            return View();
        }

        // Yeni randevu eklemek için POST metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Randevular.Add(randevu);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Başarıyla ekledikten sonra Index sayfasına yönlendir
            }
            return View(randevu); // Hata olursa aynı sayfayı yeniden göster
        }
    }
}
