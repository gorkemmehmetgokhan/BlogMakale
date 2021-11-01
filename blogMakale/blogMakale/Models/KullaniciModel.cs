using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class KullaniciModel
    {
        [Key]
        public int id_Kullanici { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string AdSoyad { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string KullaniciAdi { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string EMail { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Sifre { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Foto { get; set; }


        //Bire-Çok ilişki, Her kullanıcının birden fazla makalesi olabilir.
        public virtual ICollection<MakaleModel> Makale { get; set; }

        // Bire-Çok ilişki , Her kullanıcı bir makaleye birden fazla yorum yapabilir.
        public virtual ICollection<MakaleYorumModel> MakaleYorum { get; set; }

        //Çok-Çok ilişki, her kullanıcın birden fazla favorisi olabilir, her makalenin de birden fazla favorisi olabilir.
        public virtual ICollection<FavoriModel> Favori { get; set; }
    }
}
