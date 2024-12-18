using Microsoft.AspNetCore.Mvc;
using WebOdevi.Models;

namespace WebOdevi.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Çalışanları listele
        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        // Yeni çalışan eklemek için GET metodu
        public IActionResult Create()
        {
            return View();
        }

        // Yeni çalışan eklemek için POST metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Add(calisan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(calisan);
        }
    }
}
