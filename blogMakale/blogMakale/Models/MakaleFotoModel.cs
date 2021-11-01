using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class MakaleFotoModel
    {
        [Key]
        public int id_MakaleFoto { get; set; }

        public int id_Makale { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string FotoURL { get; set; }

        public virtual MakaleModel Makale { get; set; }

    }
}
