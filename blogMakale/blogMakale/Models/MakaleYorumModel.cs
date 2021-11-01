using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class MakaleYorumModel
    {
        [Key]
        public int id_MakaleYorum { get; set; }

        public int id_Makale { get; set; }

        public int id_Kullanici { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string YorumText { get; set; }

        public DateTime YorumTarihi { get; set; }

     
        public virtual MakaleModel Makale { get; set; }
        public virtual KullaniciModel Kullanici { get; set; }

    }
}
