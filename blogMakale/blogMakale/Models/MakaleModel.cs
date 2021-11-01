using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class MakaleModel
    {
        [Key]
        public int id_Makale { get; set; }
 
        public int id_Kullanici { get; set; }

        [Required]
        [Display(Name ="Makale Başlığı")]
        [Column(TypeName = "nvarchar(250)")]
        public string MakaleBaslik { get; set; }

        [Required]
        [Display(Name = "Makale İçeriği")]
        [Column(TypeName = "nvarchar(MAX)")]
        public string MakaleIcerik { get; set; }

        public DateTime MakaleTarihi { get; set; }

     
        // Bire-Çok ilişki, Her makalenin birden fazla fotoğrafı olabilir
        public virtual ICollection<MakaleFotoModel> MakaleFoto { get; set; }


        // Çoka-Çok ilişki, Her makalenin birden fazla kategorisi olabilir. Her kategorinin de birden fazla makalesi olabilir. 
        public virtual ICollection<MakaleKategoriModel> MakaleKategori { get; set; }


        //Çoka-Çok ilişki, Her makalenin birden fazla etiketi olabilir. Her etiketin de birden fazla makalesi olabilir.
        public virtual ICollection<MakaleEtiketModel> MakaleEtiket { get; set; }


        // Bire-Çok ilişki, Her makalenin birden fazla yorumu olabilir.
        public virtual ICollection<MakaleYorumModel> MakaleYorum { get; set; }

        // Çoka-Çok ilişki, Her makalenin birden fazla favorisi olabilir. Her kullanıcının da birden fazla favorisi olabilir.
        public virtual ICollection<FavoriModel> Favori { get; set; }
   
        public virtual KullaniciModel Kullanici { get; set; }

       

    }
}
