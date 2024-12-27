using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdevi.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebOdevi.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Çalışan listesi (Herkes görüntüleyebilir)
        [AllowAnonymous]
        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.Include(c => c.Berber).ToList();
            return View(calisanlar);
        }

        // Çalışan detayları (Herkes görüntüleyebilir)
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var calisan = _context.Calisanlar.Include(c => c.Berber).FirstOrDefault(c => c.Id == id);

            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // Yeni çalışan oluşturma sayfası (Sadece Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BerberId"] = new SelectList(_context.Berberler, "Id", "Ad");
            return View();
        }

        // Yeni çalışan oluşturma işlemi (Sadece Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Add(calisan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BerberId"] = new SelectList(_context.Berberler, "Id", "Ad", calisan.BerberId);
            return View(calisan);
        }

        // Çalışan düzenleme sayfası (Sadece Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var calisan = _context.Calisanlar.Find(id);

            if (calisan == null)
            {
                return NotFound();
            }

            ViewData["BerberId"] = new SelectList(_context.Berberler, "Id", "Ad", calisan.BerberId);
            return View(calisan);
        }

        // Çalışan düzenleme işlemi (Sadece Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Update(calisan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BerberId"] = new SelectList(_context.Berberler, "Id", "Ad", calisan.BerberId);
            return View(calisan);
        }

        // Çalışan silme işlemi (Sadece Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var calisan = _context.Calisanlar.Include(c => c.Berber).FirstOrDefault(c => c.Id == id);

            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan != null)
            {
                _context.Calisanlar.Remove(calisan);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}