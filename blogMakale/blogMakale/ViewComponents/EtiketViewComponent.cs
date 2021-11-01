using blogMakale.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.ViewComponents
{
    public class EtiketViewComponent : ViewComponent
    {
        private readonly BlogMakaleContext _db;

        public EtiketViewComponent(BlogMakaleContext db)
        {
            _db = db;

        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Etiketler = _db.Etiket.ToList();         
            return View();
        }
    }
}
