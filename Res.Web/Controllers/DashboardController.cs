using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Res.Data.ViewModels.ForAdmin;
using Res.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Res.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ApplicationDbContext _context;


        public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM model = new HomeVM()
            {
                CategoryList = _context.Categories,
                ProductList = _context.Products.Include(s => s.Category),
                BookingList = _context.Booking,
             };

            if (model.CategoryList  != null && model.ProductList != null && model.BookingList != null && model.OrdersList != null)
            {
                model.BookingCount = model.BookingList.Count();
                model.ProductCount = model.ProductList.Count();
                model.OrdersCount = model.OrdersList.Count();
            }
            else
            {
                model.BookingCount = 0;
                model.ProductCount = 0;
                model.OrdersCount = 0;
            }

            
            return View();
        }
    }
}
