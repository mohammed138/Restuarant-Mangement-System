using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Res.Data;
using Res.Data.Entities;
using Res.Data.ViewModels;
using Res.DataAccess;

namespace Res.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Booking.Include(b => b.BookingStatus).Include(b => b.BookingType).Include(b => b.NumberOfPeople).Include(b => b.Product).Include(b => b.SpecialEvent).Include(b => b.Tables);
            return View(await applicationDbContext.ToListAsync());
        }


        public IActionResult GetAll()
        {
            BookingVM model = new BookingVM()
            {
                BookingList = _context.Booking.Include(b => b.BookingStatus).Include(b => b.BookingType).Include(b => b.NumberOfPeople).Include(b => b.Product).Include(b => b.SpecialEvent).Include(b => b.Tables)
            };
       
            return View(model);
        }



        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.BookingStatus)
                .Include(b => b.BookingType)
                .Include(b => b.NumberOfPeople)
                .Include(b => b.Product)
                .Include(b => b.SpecialEvent)
                .Include(b => b.Tables)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }


        public IActionResult BookingByUser()
        {
            BookingVM model = new BookingVM()
            {
                Booking = new Booking(),
                SpecialEventDrop = _context.SpecialEvent.Select(s => new SelectListItem { Text = s.EventName.ToString(), Value = s.Id.ToString() }),
                BookingTypeDrop = _context.BookingType.Select(s => new SelectListItem { Text = s.BookingTypeName.ToString(), Value = s.Id.ToString() }),
                NumberOfPeopleDrop = _context.NumberOfPeople.Select(s => new SelectListItem { Text = s.Number.ToString(), Value = s.Id.ToString() }),
                BookingStatusDrop = _context.BookingStatus.Select(s => new SelectListItem { Text = s.StatusName.ToString(), Value = s.Id.ToString() }),
                ProductDrop = _context.Products.Select(s => new SelectListItem { Text = s.Name.ToString(), Value = s.Id.ToString() }),
                TablesDrop = _context.Tables.Select(s => new SelectListItem { Text = s.TableName.ToString(), Value = s.Id.ToString() })
            };
 
                return View(model);
             
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookingByUser(BookingVM model)
        { 
            //Creating
            model.Booking.BookingStatusId = 1;
            model.Booking.DateCreate = DateTime.Now;
            model.Booking.IsDelete = false;
            _context.Booking.Add(model.Booking);

            _context.SaveChanges();
            return RedirectToAction("Index" , "Home"); 
        }

        public IActionResult Upsert(int? id)
        {
            BookingVM model = new BookingVM()
            {
                Booking = new Booking(),
                SpecialEventDrop = _context.SpecialEvent.Select(s => new SelectListItem { Text = s.EventName.ToString(), Value = s.Id.ToString() }),
                BookingTypeDrop = _context.BookingType.Select(s => new SelectListItem { Text = s.BookingTypeName.ToString(), Value = s.Id.ToString() }),
                NumberOfPeopleDrop = _context.NumberOfPeople.Select(s => new SelectListItem { Text = s.Number.ToString(), Value = s.Id.ToString() }),
                BookingStatusDrop = _context.BookingStatus.Select(s => new SelectListItem { Text = s.StatusName.ToString(), Value = s.Id.ToString() }),
                ProductDrop = _context.Products.Select(s => new SelectListItem { Text = s.Name.ToString(), Value = s.Id.ToString() }),
                TablesDrop = _context.Tables.Select(s => new SelectListItem { Text = s.TableName.ToString(), Value = s.Id.ToString() })
            };

            if (id == null)
            {
                //this is for create
                return View(model);
            }
            else
            {
                model.Booking = _context.Booking.Find(id);
                if (model.Booking == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }
         
        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookingVM model)
        {
            if (ModelState.IsValid)
            { 

                if (model.Booking.Id == 0)
                {
                    //Creating
                    model.Booking.BookingStatusId = 1;
                    model.Booking.DateCreate = DateTime.Now;
                    model.Booking.IsDelete = false;

                    _context.Booking.Add(model.Booking);
                }
                else
                {
                    //updating
                     model.Booking.DateEdit = DateTime.Now;
                    _context.Booking.Update(model.Booking);
                }


                _context.SaveChanges();
                return RedirectToAction("Index");
            }
         
            return View(model);

        }
     
        
       
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.BookingStatus)
                .Include(b => b.BookingType)
                .Include(b => b.NumberOfPeople)
                .Include(b => b.Product)
                .Include(b => b.SpecialEvent)
                .Include(b => b.Tables)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}
