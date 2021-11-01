using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blogMakale.Models;

namespace blogMakale.Controllers
{
    public class KategoriController : Controller
    {

        private readonly BlogMakaleContext _db;

        public KategoriController(BlogMakaleContext db)
        {
            _db = db;
        }

        // Kategorileri listeleme işlemi.
        public IActionResult Listele()
        {
            var liste = _db.Kategori.ToList();
            return View(liste);
        }

        public IActionResult KategoriEkle()
        {
            return View();
        }

        // Kategori ekleme işlemi.
        [HttpPost]
        public async Task<IActionResult> KategoriEkle(KategoriModel kat)
        {
            if (ModelState.IsValid)
            {
                _db.Add(kat);
                await _db.SaveChangesAsync();
                return RedirectToAction("Listele","Kategori");
            }

            return View(kat);
        }

        public async Task<IActionResult> KategoriGuncelle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele", "Kategori");
            }

            var kategori = await _db.Kategori.FindAsync(id); 
            return View(kategori);
        }

        // Kategori güncelleme işlemi.
        [HttpPost]  
        public async Task<IActionResult> KategoriGuncelle(KategoriModel kat)
        {
           
            if (ModelState.IsValid)
            {
                   _db.Update(kat);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Listele","Kategori");             
            }
            return View(kat);
        }

        public async Task<IActionResult> KategoriSil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele", "Kategori");
            }

            var kategori = await _db.Kategori.FindAsync(id);
            return View(kategori);
        }

        // Kategori güncelleme işlemi.
        [HttpPost]
        public async Task<IActionResult> KategoriSil(int id)
        {

            var kategori = await _db.Kategori.FindAsync(id);
            _db.Kategori.Remove(kategori);
            await _db.SaveChangesAsync();

            return RedirectToAction("Listele", "Kategori");
        }



    }
}