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
    public class BookingTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingTypes
        public async Task<IActionResult> GetAll()
        {
            return View(await _context.BookingType.ToListAsync());
        }

        // GET: BookingTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingType = await _context.BookingType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingType == null)
            {
                return NotFound();
            }

            return View(bookingType);
        }

        // GET: BookingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingTypeName,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] BookingType bookingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookingType);
        }

        // GET: BookingTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingType = await _context.BookingType.FindAsync(id);
            if (bookingType == null)
            {
                return NotFound();
            }
            return View(bookingType);
        }

        // POST: BookingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingTypeName,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] BookingType bookingType)
        {
            if (id != bookingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingTypeExists(bookingType.Id))
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
            return View(bookingType);
        }

        // GET: BookingTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingType = await _context.BookingType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingType == null)
            {
                return NotFound();
            }

            return View(bookingType);
        }

        // POST: BookingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingType = await _context.BookingType.FindAsync(id);
            _context.BookingType.Remove(bookingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingTypeExists(int id)
        {
            return _context.BookingType.Any(e => e.Id == id);
        }
    }
}
