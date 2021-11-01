using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;

namespace blogMakale.ViewModel
{
    public class MakaleListeViewModel
    { 
        public IEnumerable<MakaleModel> Makale { get; set; }
        public IEnumerable<MakaleFotoModel> MakaleFoto { get; set; }
    }
}
