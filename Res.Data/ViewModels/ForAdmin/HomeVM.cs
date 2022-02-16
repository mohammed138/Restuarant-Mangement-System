using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels.ForAdmin
{
   public class HomeVM
    {

     


        
        public decimal TotalAmount { get; set; }


        public IEnumerable<Product> ProductList { get; set; }
         public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Booking> BookingList { get; set; }
        public List<ShoppingCart> ShoppingCartList { get; set; }
        public List<Orders> OrdersList { get; set; }


        public int ProductCount { get; set; }
        public int BookingCount { get; set; }
        public int OrdersCount { get; set; }




        public Booking Booking { get; set; }
        public Product Product { get; set; }
        public Orders Orders { get; set; }

        //________________________________________
        



    }
}
//public IEnumerable<Product> ProductbyMeals { get; set; }
//public IEnumerable<Product> ProductbySandwiches { get; set; }
//public IEnumerable<Product> ProductbyDrinks { get; set; }
//public IEnumerable<Product> ProductbyHookah { get; set; }
//public IEnumerable<Product> ProductbyDessert { get; set; }
//public IEnumerable<Product> ProductSpecialRequests { get; set; }
//public IEnumerable<Product> ProductbyAppetizer { get; set; }
//public IEnumerable<Product> ProductbySoups { get; set; }
//public IEnumerable<Product> ProductbyPastries { get; set; }
//public IEnumerable<Product> ProductbyShawarma { get; set; }