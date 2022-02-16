using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Res.Data.Entities;
using Res.DataAccess;

namespace Res.Web.Controllers
{
    
    public class BookingStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BookingStatus
        public async Task<IActionResult> GetAll()
        {
            return View(await _context.BookingStatus.ToListAsync());
        }

        // GET: Admin/BookingStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // GET: Admin/BookingStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BookingStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusName,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] BookingStatus bookingStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookingStatus);
        }

        // GET: Admin/BookingStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus.FindAsync(id);
            if (bookingStatus == null)
            {
                return NotFound();
            }
            return View(bookingStatus);
        }

        // POST: Admin/BookingStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusName,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] BookingStatus bookingStatus)
        {
            if (id != bookingStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingStatusExists(bookingStatus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookingStatus);
        }

        // GET: Admin/BookingStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // POST: Admin/BookingStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingStatus = await _context.BookingStatus.FindAsync(id);
            _context.BookingStatus.Remove(bookingStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingStatusExists(int id)
        {
            return _context.BookingStatus.Any(e => e.Id == id);
        }
    }
}
