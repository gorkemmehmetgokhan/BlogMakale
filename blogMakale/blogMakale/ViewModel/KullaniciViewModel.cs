using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.ViewModel
{
    public class KullaniciViewModel
    {
        public string AdSoyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string EMail { get; set; }
        public IFormFile Foto { get; set; }
    }
}
