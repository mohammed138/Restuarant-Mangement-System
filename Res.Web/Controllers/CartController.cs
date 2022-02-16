using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Res.Data;
using Res.Data.Entities;
using Res.Data.ViewModels;
using Res.DataAccess;
 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Res.Web.Controllers
{
 
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        public CartController(ApplicationDbContext context , IHttpContextAccessor _HttpContextAccessor, IEmailSender emailSender , IWebHostEnvironment webHostEnvironment )
        {
            HttpContextAccessor = _HttpContextAccessor;
               _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        

        public IActionResult Index()
        {
            CartVM model = new CartVM() {
               ShoppingCartList = new List<ShoppingCart>(), 

            };
             
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            } 
            List<int> prodInCart = model.ShoppingCartList.Select(i => i.ProductId).ToList();
            List<int> prodQInCart = model.ShoppingCartList.Select(i => i.Quantity).ToList(); 
            model.ProductList = _context.Products.Where(u => prodInCart.Contains(u.Id)).Include(d=>d.Category); 
            return View(model);
        } 
        public IActionResult Buy(int id , CartVM model )
        {
            model.ShoppingCartList = new List<ShoppingCart>();
             
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

             
            model.ShoppingCartList.Add(new ShoppingCart { ProductId = model.Product.Id, Quantity = model.ShoppingCart.Quantity });
            HttpContext.Session.Set(WC.SessionCart, model.ShoppingCartList);
            return RedirectToAction("HomePage", "Home");
        }
         



        public IActionResult Summary()
        { 
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claimsIdentity != null && claim != null)
            {
                ProductUserVM model = new ProductUserVM()
                {
                    ApplicationUser = _context.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value),
                    ShoppingCartList = new List<ShoppingCart>()
                };
                if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
                {
                    model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
                }
                List<int> prodInCart = model.ShoppingCartList.Select(i => i.ProductId).ToList();
                IEnumerable<Product> prodList = _context.Products.Where(u => prodInCart.Contains(u.Id));
                model.ProductList = prodList.ToList();
                 
                return View(model);
            }
            else
            {
                ProductUserVM model = new ProductUserVM()
                { 
                    ShoppingCartList = new List<ShoppingCart>()
                };

                if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
                {
                    model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
                }
                List<int> prodInCart = model.ShoppingCartList.Select(i => i.ProductId).ToList();
                IEnumerable<Product> prodList = _context.Products.Where(u => prodInCart.Contains(u.Id));
                model.ProductList = prodList.ToList(); 
                return View(model);
            } 
        }
          
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "templates" + Path.DirectorySeparatorChar.ToString() +
                "Inquiry.html";

            var subject = "New Inquiry";
            string HtmlBody = "";
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }
            //Name: { 0}
            //Email: { 1}
            //Phone: { 2}
            //Products: {3}

            StringBuilder productListSB = new StringBuilder();
            foreach (var prod in ProductUserVM.ProductList)
            {
                productListSB.Append($" - Name: { prod.Name} <span style='font-size:14px;'> (ID: {prod.Id})</span><br />");
            }
             
            if (claimsIdentity != null && claim != null) {
                string messageBody = string.Format(HtmlBody,
                   ProductUserVM.ApplicationUser.FullName,
                   ProductUserVM.ApplicationUser.Email,
                   ProductUserVM.ApplicationUser.PhoneNumber,
                   productListSB.ToString());

                await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messageBody);
            }
            else
            {
                string messageBody = string.Format(HtmlBody,
              
              productListSB.ToString());
                

                await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messageBody);

            } 

            return RedirectToAction(nameof(InquiryConfirmation));
        } 
        public IActionResult InquiryConfirmation(CartVM model)
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
                _context.Orders.Add(new Orders { ProductId = item.ProductId , DateCreate = DateTime.Now , OrdersStatusId = 1 , IsDelete=false , Qun = item.Quantity , OrderTotal = OrderTotal , /*Description = item.OrderDes , OrderDate = item.OrderDate*/ });
                 _context.SaveChanges();
            }
            


            HttpContext.Session.Clear();
            return View();
        } 
   
        public IActionResult Remove(int id)
        {

            CartVM model = new CartVM()
            {
                ShoppingCartList = new List<ShoppingCart>()  
            };
             
             if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                model.ShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var itemToRemove = model.ShoppingCartList.SingleOrDefault(r => r.ProductId == id);
            if (itemToRemove != null)
            {
                model.ShoppingCartList.Remove(itemToRemove);
            } 
            HttpContext.Session.Set(WC.SessionCart, model.ShoppingCartList); 
            return RedirectToAction("HomePage", "Home");
        }



         


    }
}
