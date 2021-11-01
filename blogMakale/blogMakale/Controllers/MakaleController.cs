using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using blogMakale.ViewModel;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace blogMakale.Controllers
{
    public class MakaleController : Controller
    {

        private readonly BlogMakaleContext _db;
        private IWebHostEnvironment _env;
        private string _dir;


        //class constructor
        public MakaleController(BlogMakaleContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
            _dir = _env.ContentRootPath;
        }


        // Makalenin Detay İçeriğini gösterir.
        [HttpGet]    
        public IActionResult MakaleDetay(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            MakaleDetayViewModel model = new MakaleDetayViewModel();

            MakaleModel makale = new MakaleModel();
            KullaniciModel kullanici = new KullaniciModel();
            makale = _db.Makale.Where(x => x.id_Makale == id).FirstOrDefault();
            kullanici = _db.Kullanici.Where(x => x.id_Kullanici == Convert.ToInt32(userId)).FirstOrDefault();   
            model.Makale = makale;
            model.Kullanici = kullanici;     
            return View(model);
        }

        //Makale Ekleme işlemi
        [Authorize]
        [HttpGet]
        public IActionResult MakaleEkle()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Anasayfa");
            }

            MakaleViewModel model = new MakaleViewModel();
            List<SelectListItem> KategoriList = new List<SelectListItem>();
            List<SelectListItem> EtiketList = new List<SelectListItem>();

            foreach (var item in _db.Kategori.ToList())
            {
                KategoriList.Add(new SelectListItem { Value = item.id_Kategori.ToString(), Text = item.KategoriAd });
            }

            foreach (var item in _db.Etiket.ToList())
            {
                EtiketList.Add(new SelectListItem { Value = item.id_Etiket.ToString(), Text = item.EtiketAd });
            }


            model.KategoriList = KategoriList;
            model.EtiketList = EtiketList;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakaleEkle(MakaleViewModel model, string[] Kategori , string[] Etiket)
        {

            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                string filename = null;

                MakaleModel makale = new MakaleModel()
                {
                    id_Kullanici = Convert.ToInt32(userId),
                    MakaleIcerik = model.Makale.MakaleIcerik,
                    MakaleBaslik = model.Makale.MakaleBaslik,
                    MakaleTarihi = DateTime.Now
                };
                _db.Add(makale);
                await _db.SaveChangesAsync();


                if (model.Photos != null && model.Photos.Count > 0)
                {

                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(_env.WebRootPath, "images");

                        filename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, filename);

                        photo.CopyTo(new FileStream(filePath, FileMode.Create));

                        MakaleFotoModel makaleFoto = new MakaleFotoModel
                        {
                            id_Makale = makale.id_Makale,
                            FotoURL = filename
                        };

                        _db.Add(makaleFoto);
                        await _db.SaveChangesAsync();
                    }
                }

                MakaleKategoriModel kategoriler = new MakaleKategoriModel();
                
                foreach (var item in Kategori)
                {
                    kategoriler.id_Makale = makale.id_Makale;
                    kategoriler.id_Kategori = Convert.ToInt32(item);
                                 
                    _db.Add(kategoriler);
                    await _db.SaveChangesAsync();
                }

                MakaleEtiketModel etiketler = new MakaleEtiketModel();

                foreach (var item in Etiket)
                {
                    etiketler.id_Makale = makale.id_Makale;
                    etiketler.id_Etiket = Convert.ToInt32(item);
                  
                    _db.Add(etiketler);
                    await _db.SaveChangesAsync();
                }
                                       
                return RedirectToAction("Index", "Anasayfa");
            }
            return View(model);
        }


        // Makale Güncelleme İşlemi
        [HttpGet]
        [Authorize]
        public IActionResult MakaleGuncelle(int id)
        {
           
            MakaleViewModel model = new MakaleViewModel();
            List<SelectListItem> KategoriList = new List<SelectListItem>();
            List<SelectListItem> EtiketList = new List<SelectListItem>();
             
            foreach (var item in _db.Kategori.ToList())
            {
                KategoriList.Add(new SelectListItem { Value = item.id_Kategori.ToString(), Text = item.KategoriAd });
            }

            foreach (var item in _db.Etiket.ToList())
            {
                EtiketList.Add(new SelectListItem { Value = item.id_Etiket.ToString(), Text = item.EtiketAd });
            }
              
            model.KategoriList = KategoriList;
            model.EtiketList = EtiketList;
            model.Makale = _db.Makale.Where(x => x.id_Makale == id).FirstOrDefault();         
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MakaleGuncelle(MakaleViewModel model, string[] Kategori, string[] Etiket)
        {
        
            if (ModelState.IsValid)
            {
                model.Makale.MakaleTarihi = DateTime.Now;             
                _db.Update(model.Makale);
                await _db.SaveChangesAsync();

                _db.MakaleEtiket.RemoveRange(_db.MakaleEtiket.Where(x => x.id_Makale == model.Makale.id_Makale));
                _db.MakaleKategori.RemoveRange(_db.MakaleKategori.Where(x => x.id_Makale == model.Makale.id_Makale));
                _db.MakaleFoto.RemoveRange(_db.MakaleFoto.Where(x => x.id_Makale == model.Makale.id_Makale));

                string filename = null;

                if (model.Photos != null && model.Photos.Count > 0)
                {

                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(_env.WebRootPath, "images");

                        filename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, filename);

                        photo.CopyTo(new FileStream(filePath, FileMode.Create));

                        MakaleFotoModel makaleFoto = new MakaleFotoModel
                        {
                            id_Makale = model.Makale.id_Makale,
                            FotoURL = filename
                        };

                        _db.Add(makaleFoto);
                        await _db.SaveChangesAsync();
                    }
                }

                MakaleKategoriModel kategoriler = new MakaleKategoriModel();

                foreach (var item in Kategori)
                {
                    kategoriler.id_Makale = model.Makale.id_Makale;
                    kategoriler.id_Kategori = Convert.ToInt32(item);

                    _db.Add(kategoriler);
                    await _db.SaveChangesAsync();
                }

                MakaleEtiketModel etiketler = new MakaleEtiketModel();

                foreach (var item in Etiket)
                {
                    etiketler.id_Makale = model.Makale.id_Makale;
                    etiketler.id_Etiket = Convert.ToInt32(item);

                    _db.Add(etiketler);
                    await _db.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Anasayfa");
            }
            return View(model);
        }


        // Kullanıcıya ait makaleleri listeler.
        [Authorize]
        public IActionResult MakaleListele()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var listele = _db.Makale.Where(x => x.id_Kullanici == Convert.ToInt32(userId)).ToList();
            return View(listele);
        }

        // Favoriye alınan makaleleri listeler.
        [Authorize]
        public IActionResult Favorilerim()
        {
         
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            FavoriMakaleViewModel model = new FavoriMakaleViewModel();
            KullaniciModel kullanici = new KullaniciModel();           
            List<FavoriModel> favori = new List<FavoriModel>();
        
            kullanici = _db.Kullanici.Where(x => x.id_Kullanici == Convert.ToInt32(userId)).FirstOrDefault();
            favori = _db.Favori.Where(x => x.id_Kullanici == Convert.ToInt32(userId)).ToList();
           
            model.Favori = favori;         
            model.Kullanici = kullanici;
            return View(model);
        }

        // Makale silme işlemi
        public async Task<IActionResult> MakaleSil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            var makale = await _db.Makale.FindAsync(id);
            return View(makale);
        }


        [HttpPost]
        public async Task<IActionResult> MakaleSil(int id)
        {            
            var favori = await _db.Favori.Where(x => x.id_Makale == id).FirstOrDefaultAsync();         
            var makale = await _db.Makale.FindAsync(id);
         
            if (favori != null)
            {
                _db.Favori.Remove(favori);
                await _db.SaveChangesAsync();
            }
           
            _db.MakaleYorum.RemoveRange(_db.MakaleYorum.Where(x => x.id_Makale == id));
            await _db.SaveChangesAsync();
            
            
            _db.Makale.Remove(makale);
            await _db.SaveChangesAsync();

            return RedirectToAction("MakaleListele", "Makale");
        }

        
        // Makale için yorum yapma özelliğini içeren method.
        [HttpPost]
        public async Task<IActionResult> MakaleYorum(MakaleYorumModel yorum)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            MakaleYorumModel makaleYorum = new MakaleYorumModel()
            {
                id_Makale = yorum.id_Makale,
                id_Kullanici = Convert.ToInt32(userId),
                YorumText = yorum.YorumText,
                YorumTarihi = DateTime.Now
            };

                _db.Add(makaleYorum);
                await _db.SaveChangesAsync();
               
                return RedirectToAction("MakaleDetay", new RouteValueDictionary( new { Id = yorum.id_Makale }));
        }

        // Favoriye alma durumunu inceler. (Makaleyi favoriye ekleme ya da favoriden çıkarma)
        [HttpPost]
        public async Task<IActionResult> FavoriDurum(FavoriModel model)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var favori = await _db.Favori.Where(x => x.id_Kullanici == Convert.ToInt32(userId) && x.id_Makale == model.id_Makale).FirstOrDefaultAsync();

            FavoriModel fav = new FavoriModel();

            if (favori == null)
            {
                 fav.id_Kullanici = Convert.ToInt32(userId);
                 fav.id_Makale = model.id_Makale;
                 fav.FavoriTarihi = DateTime.Now;
                
                _db.Add(fav);             
            }
            else
            {          
                _db.Remove(favori);
            }
            await _db.SaveChangesAsync();
          
            return RedirectToAction("MakaleDetay", new RouteValueDictionary(new { Id = model.id_Makale }));
        }

        // Etiketlere Göre Makaleleri Listeler.
        public IActionResult MakalelerByEtiket(int id)
        {            
            var makaleEtiket = _db.MakaleEtiket.Where(x => x.id_Etiket == id).ToList();
            return View(makaleEtiket);
        }

        // Kategorilere Göre Makaleleri Listeler.
        public IActionResult MakalelerByKategori(int id)
        {
            var makaleKategori = _db.MakaleKategori.Where(x => x.id_Kategori == id).ToList();
            return View(makaleKategori);
        }
    }
}