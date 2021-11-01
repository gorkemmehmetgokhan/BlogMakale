using blogMakale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.ViewModel
{
    public class FavoriMakaleViewModel
    {
        public KullaniciModel Kullanici { get; set; }
        public IEnumerable<FavoriModel> Favori { get; set; }
   
    }
}
