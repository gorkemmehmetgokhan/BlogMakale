using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;

namespace blogMakale.ViewComponents
{
    public class KategoriViewComponent :ViewComponent
    {
        private readonly BlogMakaleContext _db;
     
        public KategoriViewComponent(BlogMakaleContext db)
        {
            _db = db;
         
        }

        public IViewComponentResult Invoke()
        {      
            ViewBag.Kategoriler = _db.Kategori.ToList();
            return View();
        }
    }
}
