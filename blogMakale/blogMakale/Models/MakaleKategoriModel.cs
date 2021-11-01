using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blogMakale.Models
{
    public class MakaleKategoriModel
    {         
        //Juntion tablo, Her makalenin birden fazla kategorisi , her kategorinin de birden fazla makalesi olabilir.
        public int id_Makale { get; set; }

        public int id_Kategori { get; set; }

        [ForeignKey("id_Makale")]
        public virtual MakaleModel Makale { get; set; }

        [ForeignKey("id_Kategori")]
        public virtual KategoriModel Kategori { get; set; }
    }
}
