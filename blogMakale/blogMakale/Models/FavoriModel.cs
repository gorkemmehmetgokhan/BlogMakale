using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class FavoriModel
    {
        // Junction tablo. her makale ve kullanıcının birden fazla favorisi olabilir.
        public int id_Kullanici { get; set; }
     
        public int id_Makale { get; set; }
    
        public DateTime FavoriTarihi { get; set; }

        [ForeignKey("id_Kullanici")]
        public virtual  KullaniciModel Kullanici { get; set; }
        
        [ForeignKey("id_Makale")]
        public virtual MakaleModel Makale { get; set; }
    }
}
