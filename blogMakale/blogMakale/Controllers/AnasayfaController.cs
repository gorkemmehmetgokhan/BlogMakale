using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using blogMakale.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Dynamic;

namespace blogMakale.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly BlogMakaleContext _db;
        private IWebHostEnvironment _env;

        //class constructor
        public AnasayfaController(BlogMakaleContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
    

        //Anasayfada makaleleri listeler.
        public IActionResult Index()
        {
            MakaleListeViewModel model = new MakaleListeViewModel();
            
            List<MakaleModel> makale = new List<MakaleModel>();
            List<MakaleFotoModel> makaleFoto = new List<MakaleFotoModel>();

            makale = _db.Makale.OrderByDescending(x => x.id_Makale).ToList();
            makaleFoto = _db.MakaleFoto.ToList();

            model.Makale = makale;
            model.MakaleFoto =  makaleFoto;
         
            return View(model);
        }

        //Kullanıcı oturum açma işlemi.
        [HttpGet]     
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {              
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(KullaniciModel login)
        {
            var kullanici = _db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == login.KullaniciAdi && x.Sifre == login.Sifre);

            if (kullanici != null)
            {
                var claims = new List<Claim>
                {
                 new Claim(ClaimTypes.Name, login.KullaniciAdi),
                  new Claim(ClaimTypes.NameIdentifier, kullanici.id_Kullanici.ToString())
                };             
                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
           
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["mesaj"] = "Kullanıcı Adı ya da Sifre yanlış!!";
            }
            return View();
        }

        // Kullanıcı oturum kapatma işlemi.
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
       

        // Kullanıcı yeni üye olma işlemi.
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(KullaniciViewModel model)
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
                return RedirectToAction("Index", "Anasayfa");
            }

            return View(model);
        }

        // Site içi makale arama.
        [HttpGet]
        public IActionResult SiteIciArama(string search)
        {         
            ViewData["Makale"] = _db.Makale.Where(x => x.MakaleBaslik.Contains(search)).ToList();        
            return View();
        }


    }
}