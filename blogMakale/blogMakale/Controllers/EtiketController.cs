using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogMakale.Controllers
{
    public class EtiketController : Controller
    {
        private readonly BlogMakaleContext _db;

        public EtiketController(BlogMakaleContext db)
        {
            _db = db;
        }

        // Etiketleri listeleme işlemi.
        public IActionResult Listele()
        {
            var liste = _db.Etiket.ToList();
            return View(liste);
        }

        // Etiket ekleme işlemi.
        public IActionResult EtiketEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EtiketEkle(EtiketModel kat)
        {
            if (ModelState.IsValid)
            {
                _db.Add(kat);
                await _db.SaveChangesAsync();
                return RedirectToAction("Listele", "Etiket");
            }

            return View(kat);
        }

        // Etiket güncelleme işlemi.
        public async Task<IActionResult> EtiketGuncelle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele", "Etiket");
            }

            var etiket = await _db.Etiket.FindAsync(id);
            return View(etiket);
        }


        [HttpPost]
        public async Task<IActionResult> EtiketGuncelle(EtiketModel et)
        {

            if (ModelState.IsValid)
            {
                _db.Update(et);
                await _db.SaveChangesAsync();
                return RedirectToAction("Listele", "Etiket");
            }
            return View(et);
        }

        // Etiket silme işlemi
        public async Task<IActionResult> EtiketSil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele", "Etiket");
            }

            var etiket = await _db.Etiket.FindAsync(id);
            return View(etiket);
        }


        [HttpPost]
        public async Task<IActionResult> EtiketSil(int id)
        {

            var etiket = await _db.Etiket.FindAsync(id);
            _db.Etiket.Remove(etiket);
            await _db.SaveChangesAsync();

            return RedirectToAction("Listele", "Etiket");
        }


    }
}