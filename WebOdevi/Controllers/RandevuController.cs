using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdevi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebOdevi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var randevular = await _context.Randevular
                .Include(r => r.Berber)
                .Include(r => r.Kullanici)
                .ToListAsync();
            return View(randevular);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Berberler = _context.Berberler.ToList();
            ViewBag.Kullanicilar = _context.Kullanicilar.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                var bitisSaati = randevu.BaslangicSaati.Add(new TimeSpan(1, 0, 0)); // Örnek olarak 1 saatlik randevu süresi
                randevu.BitisSaati = bitisSaati;

                // Çakışan randevuları kontrol et
                var mevcutRandevular = await _context.Randevular
                    .Where(r => r.BerberId == randevu.BerberId &&
                                r.Tarih.Date == randevu.Tarih.Date &&
                                ((r.BaslangicSaati <= randevu.BaslangicSaati && r.BitisSaati > randevu.BaslangicSaati) ||
                                 (r.BaslangicSaati < randevu.BitisSaati && r.BitisSaati >= randevu.BitisSaati)))
                    .ToListAsync();

                if (mevcutRandevular.Any())
                {
                    ModelState.AddModelError("", "Seçtiğiniz saatte çalışan meşgul. Lütfen başka bir zaman seçin.");
                    ViewBag.Berberler = _context.Berberler.ToList();
                    ViewBag.Kullanicilar = _context.Kullanicilar.ToList();
                    return View(randevu);
                }

                randevu.Durum = "Onay Bekliyor";
                _context.Randevular.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Berberler = _context.Berberler.ToList();
            ViewBag.Kullanicilar = _context.Kullanicilar.ToList();
            return View(randevu);
        }

        [HttpPost]
        public async Task<IActionResult> Onayla(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.Durum = "Onaylandı";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> IptalEt(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.Durum = "İptal Edildi";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Berber)
                .Include(r => r.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Berber)
                .Include(r => r.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }

            ViewBag.Berberler = _context.Berberler.ToList();
            ViewBag.Kullanicilar = _context.Kullanicilar.ToList();
            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bitisSaati = randevu.BaslangicSaati.Add(new TimeSpan(1, 0, 0)); // Örnek olarak 1 saatlik randevu süresi
                    randevu.BitisSaati = bitisSaati;

                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Berberler = _context.Berberler.ToList();
            ViewBag.Kullanicilar = _context.Kullanicilar.ToList();
            return View(randevu);
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.Id == id);
        }
    }
}