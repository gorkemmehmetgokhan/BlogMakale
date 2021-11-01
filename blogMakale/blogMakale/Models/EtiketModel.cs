using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class EtiketModel
    {
        
        [Key]
        public int id_Etiket { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Etiket Adı")]
        public string EtiketAd { get; set; }

        //Çoka-Çok ilişki, Her etiketin birden fazla makalesi olabilir. Her makalenin de birden fazla etiketi olabilir.
        public virtual ICollection<MakaleEtiketModel> MakaleEtiket { get; set; }
    }
}
