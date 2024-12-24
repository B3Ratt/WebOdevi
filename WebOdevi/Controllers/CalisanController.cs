using Microsoft.AspNetCore.Authorization;
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

        // Herkes görebilir
        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        // Sadece Admin yetkisi olanlar için: Ekleme işlemi
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        // Sadece Admin yetkisi olanlar için: Güncelleme işlemi
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan == null)
            {
                return NotFound();
            }
            return View(calisan);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Update(calisan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(calisan);
        }

        // Sadece Admin yetkisi olanlar için: Silme işlemi
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan == null)
            {
                return NotFound();
            }
            _context.Calisanlar.Remove(calisan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
