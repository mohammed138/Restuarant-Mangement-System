using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Res.Data;
using Res.Data.Entities;
using Res.Data.ViewModels;
using Res.DataAccess;
using Res.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
 
using Utility;

namespace Res.Web.Controllers
{
    //[Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            HomeVM model = new HomeVM()
            {
                ProductbyMeals = _context.Products.Where(d => d.CategoryId == 1).ToList(),
                ProductbySandwiches = _context.Products.Where(d => d.CategoryId == 2).ToList(),
                ProductbyDrinks = _context.Products.Where(d => d.CategoryId == 4).ToList(),
                ProductbyHookah = _context.Products.Where(d => d.CategoryId == 5).ToList(),
                ProductbyDessert = _context.Products.Where(d => d.CategoryId == 3).ToList(),
                ProductSpecialRequests = _context.Products.Where(d => d.CategoryId == 6).ToList(),
                ProductbyAppetizer = _context.Products.Where(d => d.CategoryId == 1002).ToList(),
                ProductbySoups = _context.Products.Where(d => d.CategoryId == 1003).ToList(),
                ProductbyPastries = _context.Products.Where(d => d.CategoryId == 1004).ToList(),

                CategoryList = _context.Categories,
                ProductList = _context.Products.Include(s => s.Category),

                ShoppingCartList = new List<ShoppingCart>(),

            };
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = model.ShoppingCartList.Select(i => i.ProductId).ToList();
            List<int> prodQInCart = model.ShoppingCartList.Select(i => i.Quantity).ToList();
            model.ProductList = _context.Products.Where(u => prodInCart.Contains(u.Id)).Include(d => d.Category);




            foreach (var obj in model.ProductList)
            {
                foreach (var item in model.ShoppingCartList)
                {
                    if (item.ProductId == obj.Id)
                    {

                        int p = Convert.ToInt32(obj.Price);
                        model.TotalAmount += ( p* item.Quantity);
                    }
                }
            } 

            return View(model);
        }

        public IActionResult Checkout()
        {
            HomeVM model = new HomeVM()
            {
                ProductbyMeals = _context.Products.Where(d => d.CategoryId == 1).ToList(),
                ProductbySandwiches = _context.Products.Where(d => d.CategoryId == 2).ToList(),
                ProductbyDrinks = _context.Products.Where(d => d.CategoryId == 4).ToList(),
                ProductbyHookah = _context.Products.Where(d => d.CategoryId == 5).ToList(),
                ProductbyDessert = _context.Products.Where(d => d.CategoryId == 3).ToList(),
                ProductSpecialRequests = _context.Products.Where(d => d.CategoryId == 6).ToList(),
                ProductbyAppetizer = _context.Products.Where(d => d.CategoryId == 1002).ToList(),
                ProductbySoups = _context.Products.Where(d => d.CategoryId == 1003).ToList(),
                ProductbyPastries = _context.Products.Where(d => d.CategoryId == 1004).ToList(),

                CategoryList = _context.Categories,
                ProductList = _context.Products.Include(s => s.Category),

                ShoppingCartList = new List<ShoppingCart>(),

            };
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = model.ShoppingCartList.Select(i => i.ProductId).ToList();
             List<int> prodQInCart = model.ShoppingCartList.Select(i => i.Quantity).ToList();
            model.ProductList = _context.Products.Where(u => prodInCart.Contains(u.Id)).Include(d => d.Category);
             

            foreach (var obj in model.ProductList)
            {
                foreach (var item in model.ShoppingCartList)
                {
                    if (item.ProductId == obj.Id)
                    {

                        int p = Convert.ToInt32(obj.Price);
                        model.TotalAmount += (p * item.Quantity);
                    }
                }
            }

            
          



            return View(model);
        }

        public IActionResult Submit(HomeVM model)
        {
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = model.ShoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _context.Products.Where(u => prodInCart.Contains(u.Id));
            model.ProductList = prodList.ToList();

            foreach (var item in model.ShoppingCartList)
            {
                double ProductPrice = _context.Products.Where(d => d.Id == item.ProductId).Select(s => s.Price).FirstOrDefault();
                var Total = ProductPrice * item.Quantity;
                decimal OrderTotal = (decimal)(Total);
                _context.Orders.Add(new Orders
                {
                    ProductId = item.ProductId,
                    DateCreate = DateTime.Now,
                    OrdersStatusId = 1,
                    IsDelete = false,
                    Qun = item.Quantity,
                    OrderTotal = OrderTotal,
                    Description = model.OrderDes,
                    OrderDate = model.OrderDate,
                    FirstName = model.FName,
                    LastName = model.LName,
                    PhoneNo = model.PhoneNo,
                    UserAdd = model.FullName,
                    AddressName = model.AddressName
                });

                //_context.Orders.Add(new Orders { ProductId = item.ProductId, DateCreate = DateTime.Now, OrdersStatusId = 1, IsDelete = false, Qun = item.Quantity, OrderTotal = OrderTotal, Description = item.OrderDes, OrderDate = item.OrderDate, FirstName = item.FName, LastName = item.LName, PhoneNo = item.PhoneNo });
                _context.SaveChanges();
            }



            HttpContext.Session.Clear();
            return RedirectToAction("HomePage", "Home");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
