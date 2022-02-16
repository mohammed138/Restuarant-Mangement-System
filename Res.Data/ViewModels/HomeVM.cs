using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels
{
   public class HomeVM
    {

        public IEnumerable<Product> ProductbyMeals { get; set; }
        public IEnumerable<Product> ProductbySandwiches { get; set; }
        public IEnumerable<Product> ProductbyDrinks { get; set; } 
        public IEnumerable<Product> ProductbyHookah { get; set; }
        public IEnumerable<Product> ProductbyDessert { get; set; }
        public IEnumerable<Product> ProductSpecialRequests { get; set; }
        public IEnumerable<Product> ProductbyAppetizer { get; set; }
        public IEnumerable<Product> ProductbySoups { get; set; }
        public IEnumerable<Product> ProductbyPastries { get; set; }
        public IEnumerable<Product> ProductbyShawarma { get; set; }


        
        public decimal TotalAmount { get; set; }


        public IEnumerable<Product> ProductList { get; set; }
         public IEnumerable<Category> CategoryList { get; set; }



        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
        public Orders Orders { get; set; }

        //________________________________________
        public List<ShoppingCart> ShoppingCartList { get; set; }
         public List<Orders> OrdersList { get; set; }



        public string AddressName { get; set; }
        public string? OrderDes { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string PhoneNo { get; set; }

        public string FullName => FName + LName;
        public DateTime? OrderDate { get; set; }
    }
}
