using Microsoft.AspNetCore.Mvc.Rendering;
using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels
{
   public class CartVM
    {

        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
        public  Orders  Orders { get; set; }

        //________________________________________
        public List<ShoppingCart> ShoppingCartList { get; set; } 
        public IEnumerable<Product> ProductList { get; set; }
        public List<Orders> OrdersList { get; set; }
        //________________________________________


         

        public string? OrderDes { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public int PhoneNo { get; set; }
        public DateTime? OrderDate { get; set; }


    }
}
