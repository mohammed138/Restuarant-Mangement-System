using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Res.Data;
using Res.Data.Entities;
using Res.Data.ViewModels;
using Res.DataAccess;
using Utility;

namespace Res.Web.Controllers
{
    //[Area("Customer")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult GetAll()
        {
            HomeVM model = new HomeVM()
            {
                CategoryList = _context.Categories,
                ProductList = _context.Products.Include(s => s.Category),
            }; 
            return View(model);
        }

         
        public IActionResult MenuItem()
        {
            HomeVM model = new HomeVM()
            {
                ProductbyMeals = _context.Products.Where(d=>d.CategoryId == 1).ToList(),
                ProductbySandwiches = _context.Products.Where(d => d.CategoryId == 2).ToList(),
                ProductbyDrinks = _context.Products.Where(d => d.CategoryId == 4).ToList(),
                ProductbyHookah = _context.Products.Where(d => d.CategoryId == 5).ToList(),
                ProductbyDessert = _context.Products.Where(d => d.CategoryId == 3).ToList(),
                ProductSpecialRequests = _context.Products.Where(d => d.CategoryId == 6).ToList(),

                CategoryList = _context.Categories,
                ProductList = _context.Products.Include(s => s.Category),
            };

            return View(model);
        }



        public IActionResult Upsert(int? id)
        { 

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(), 
            };
            productVM.CategoryList = _context.Categories.Select(i => new SelectListItem { Text = i.CategoryName, Value = i.Id.ToString() });

            if (id == null)
            {
                //this is for create
                return View(productVM);
            }
            else
            {
                productVM.Product = _context.Products.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }


        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.Id == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.ProductImg = fileName + extension;

                    _context.Products.Add(productVM.Product);
                }
                else
                {
                    //updating
                    var objFromDb = _context.Products.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.ProductImg);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.ProductImg = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.ProductImg = objFromDb.ProductImg;
                    }
                    _context.Products.Update(productVM.Product);
                }


                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }
      
            return View(productVM);

        }


 


        public async Task<IActionResult> Details(int? id)
        {
            HomeVM model = new HomeVM()
            {
                ShoppingCartList = new List<ShoppingCart>(),
                ShoppingCart = new ShoppingCart(),
            };

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(d => d.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            model.Product = product;
            return View(model);
        }
















        public IActionResult Create()
        {
            ProductVM model = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _context.Categories.Select(s => new SelectListItem { Text = s.CategoryName.ToString(), Value = s.Id.ToString() }),
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ShortDes,LongDes,ProductImg,Price,CategoryId,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetAll));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAll));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
