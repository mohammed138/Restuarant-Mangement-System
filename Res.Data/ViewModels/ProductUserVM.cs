using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels
{
   public class ProductUserVM
    {
        public List<Product> ProductList { get; set; }
        public List<Orders> OrdersList { get; set; }
        public   Orders Orders  { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<ShoppingCart> ShoppingCartList { get; set; } 

    }
}
