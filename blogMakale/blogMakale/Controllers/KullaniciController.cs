using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using blogMakale.ViewModel;

namespace blogMakale.Controllers
{
    public class KullaniciController : Controller
    {
      
        private IWebHostEnvironment _env;
        private string _dir;

        private readonly BlogMakaleContext _db;
     
        public KullaniciController(BlogMakaleContext db , IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
            _dir = _env.ContentRootPath;
        }
     
         
        // Kullanıcı Listeleme
        public IActionResult Listele()
        {
            var liste = _db.Kullanici.ToList();
            return View(liste);
        }

        // Kullanıcı Ekleme işlemi.
        public IActionResult KullaniciEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciEkle(KullaniciViewModel model)
        {
            if (ModelState.IsValid)
            {

                string filename = null;

                if (model.Foto != null)
                {                
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "images");

                    filename = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                    string filePath = Path.Combine(uploadsFolder, filename);       
                    model.Foto.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                KullaniciModel kullanici = new KullaniciModel
                {
                    AdSoyad = model.AdSoyad,
                    EMail = model.EMail,
                    KullaniciAdi = model.KullaniciAdi,
                    Sifre = model.Sifre,
                 
                    Foto = filename
                };

                _db.Add(kullanici);
                await _db.SaveChangesAsync();
                return RedirectToAction("Listele", "Kullanici");
            }

            return View(model);
        }

        // Kullanıcı güncelleme işlemi.
        public async Task<IActionResult> KullaniciGuncelle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele", "Kullanici");
            }

            var kullanici = await _db.Kullanici.FindAsync(id);
            return View(kullanici);
        }


        [HttpPost]
        public async Task<IActionResult> KullaniciGuncelle(KullaniciModel kul)
        {

            if (ModelState.IsValid)
            {
                           
                _db.Update(kul);
                await _db.SaveChangesAsync();
                return RedirectToAction("Listele", "Kullanici");
            }
            return View(kul);
        }

        // Kullanıcı silme işlemi.
        public async Task<IActionResult> KullaniciSil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele", "Kullanici");
            }

            var kullanici = await _db.Kullanici.FindAsync(id);
            return View(kullanici);
        }


        [HttpPost]
        public async Task<IActionResult> KullaniciSil(int id)
        {

            var kullanici = await _db.Kullanici.FindAsync(id);
            _db.Kullanici.Remove(kullanici);
            await _db.SaveChangesAsync();

            return RedirectToAction("Listele", "Kullanici");
        }

    }
}