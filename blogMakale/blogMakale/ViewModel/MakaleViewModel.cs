using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;

namespace blogMakale.ViewModel
{
    public class MakaleViewModel
    {          
        public MakaleModel Makale { get; set; }
        public IEnumerable<SelectListItem> KategoriList { get; set; }
        public IEnumerable<SelectListItem> EtiketList { get; set; } 
        [NotMapped]
        public List<IFormFile> Photos { get; set; }

    }
}
