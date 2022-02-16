using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels
{
   public class OrdersVM
    {
        public Product Product { get; set; }
        public Orders Orders { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        public List<Orders> OrdersList { get; set; }
    }
}
