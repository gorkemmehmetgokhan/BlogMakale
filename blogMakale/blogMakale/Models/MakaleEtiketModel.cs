using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class MakaleEtiketModel
    {
        // Juction tablo, her makalenin birden fazla etiketi , her etiketin de birden fazla makalesi olabilir.
       public int id_Makale { get; set; }
    
        public int id_Etiket { get; set; }

        [ForeignKey("id_Makale")]
        public virtual MakaleModel Makale { get; set; }

        [ForeignKey("id_Etiket")]
        public virtual EtiketModel Etiket { get; set; }
       
    }
}
