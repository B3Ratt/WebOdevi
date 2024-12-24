using Microsoft.AspNetCore.Mvc;
using WebOdevi.Models;

namespace WebOdevi.Controllers
{
    public class BerberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BerberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Berber bilgilerini görüntüle
        public IActionResult Index()
        {
            var berberler = _context.Berberler.ToList();
            return View(berberler);
        }

        // Yeni berber eklemek için GET metodu
        public IActionResult Create()
        {
            return View();
        }

        // Yeni berber eklemek için POST metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Berber berber)
        {
            if (ModelState.IsValid)
            {
                _context.Berberler.Add(berber);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(berber);
        }
    }
}