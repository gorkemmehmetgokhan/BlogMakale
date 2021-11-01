using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class KategoriModel
    {
       
        [Key]
        public int id_Kategori { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Kategori Adı")]
        public string KategoriAd { get; set; }


        //Çoka-Çok ilişki, Her kategorinin birden fazla makalesi olabilir. Her makalenin de birden fazla kategorisi olabilir.
        public virtual ICollection<MakaleKategoriModel> MakaleKategori { get; set; }

    }
}
