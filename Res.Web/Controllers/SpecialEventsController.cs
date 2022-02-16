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
    public class SpecialEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SpecialEvents
        public async Task<IActionResult> GetAll()
        {
            return View(await _context.SpecialEvent.ToListAsync());
        }

        // GET: SpecialEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialEvent = await _context.SpecialEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialEvent == null)
            {
                return NotFound();
            }

            return View(specialEvent);
        }
 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialEvent = await _context.SpecialEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialEvent == null)
            {
                return NotFound();
            }

            return View(specialEvent);
        }

        // POST: SpecialEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialEvent = await _context.SpecialEvent.FindAsync(id);
            _context.SpecialEvent.Remove(specialEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialEventExists(int id)
        {
            return _context.SpecialEvent.Any(e => e.Id == id);
        }
    }
}
