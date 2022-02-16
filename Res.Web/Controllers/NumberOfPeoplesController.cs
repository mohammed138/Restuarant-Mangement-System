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
     
    public class NumberOfPeoplesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NumberOfPeoplesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/NumberOfPeoples
        public async Task<IActionResult> GetAll()
        {
            return View(await _context.NumberOfPeople.ToListAsync());
        }

        // GET: Admin/NumberOfPeoples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberOfPeople = await _context.NumberOfPeople
                .FirstOrDefaultAsync(m => m.Id == id);
            if (numberOfPeople == null)
            {
                return NotFound();
            }

            return View(numberOfPeople);
        }

        // GET: Admin/NumberOfPeoples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NumberOfPeoples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] NumberOfPeople numberOfPeople)
        {
            if (ModelState.IsValid)
            {
                _context.Add(numberOfPeople);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(numberOfPeople);
        }

        // GET: Admin/NumberOfPeoples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberOfPeople = await _context.NumberOfPeople.FindAsync(id);
            if (numberOfPeople == null)
            {
                return NotFound();
            }
            return View(numberOfPeople);
        }

        // POST: Admin/NumberOfPeoples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Number,Id,IsDelete,UserAdd,UserEdit,DateCreate,DateEdit")] NumberOfPeople numberOfPeople)
        {
            if (id != numberOfPeople.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(numberOfPeople);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NumberOfPeopleExists(numberOfPeople.Id))
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
            return View(numberOfPeople);
        }

        // GET: Admin/NumberOfPeoples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberOfPeople = await _context.NumberOfPeople
                .FirstOrDefaultAsync(m => m.Id == id);
            if (numberOfPeople == null)
            {
                return NotFound();
            }

            return View(numberOfPeople);
        }

        // POST: Admin/NumberOfPeoples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var numberOfPeople = await _context.NumberOfPeople.FindAsync(id);
            _context.NumberOfPeople.Remove(numberOfPeople);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NumberOfPeopleExists(int id)
        {
            return _context.NumberOfPeople.Any(e => e.Id == id);
        }
    }
}
