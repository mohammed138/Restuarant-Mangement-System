using Microsoft.AspNetCore.Mvc.Rendering;
using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; } 
         public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
          


    }
}
