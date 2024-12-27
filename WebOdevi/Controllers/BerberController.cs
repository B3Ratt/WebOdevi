using Microsoft.AspNetCore.Authorization;
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

        // Berber listesi (Herkes görüntüleyebilir)
        [AllowAnonymous]
        public IActionResult Index()
        {
            var berberler = _context.Berberler.ToList();
            return View(berberler);
        }

        // Berber detayları (Herkes görüntüleyebilir)
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var berber = _context.Berberler.Find(id);

            if (berber == null)
            {
                return NotFound();
            }

            return View(berber);
        }

        // Yeni berber oluşturma sayfası (Sadece Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni berber oluşturma işlemi (Sadece Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        // Berber düzenleme sayfası (Sadece Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var berber = _context.Berberler.Find(id);

            if (berber == null)
            {
                return NotFound();
            }

            return View(berber);
        }

        // Berber düzenleme işlemi (Sadece Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Berber berber)
        {
            if (ModelState.IsValid)
            {
                _context.Berberler.Update(berber);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(berber);
        }

        // Berber silme işlemi (Sadece Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var berber = _context.Berberler.Find(id);

            if (berber == null)
            {
                return NotFound();
            }

            return View(berber);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var berber = _context.Berberler.Find(id);
            if (berber != null)
            {
                _context.Berberler.Remove(berber);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
